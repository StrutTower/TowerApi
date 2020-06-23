using Newtonsoft.Json;
using SteamTower;
using SteamTower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerApiLib.Domain;
using TowerApiLib.Models;
using TowerApiLib.Repository;
using TowerApiLib.Services;
using WesternStateHospital.WSHUtilities;

namespace TowerApiLib.Logic {
    public class SteamLogic {
        private readonly UnitOfWork _uow;
        private readonly SteamApiClient _steamApi;
        private readonly MemoryCacheService _memoryCache;

        public SteamLogic(UnitOfWork uow, SteamApiClient steamApi, MemoryCacheService memoryCache) {
            _uow = uow;
            _steamApi = steamApi;
            _memoryCache = memoryCache;
        }

        public long? ResolveVanityUrl(string url) {
            return _steamApi.ResolveVanityUrl(url);
        }

        public SteamUserDetails GetByUserID(long id, bool reloadWishlistPrices = false) {
            DataCacheRepository repo = _uow.GetRepo<DataCacheRepository>();
            DataCache cache = repo.Get(id, CacheType.SteamUserDetails);
            if (cache != null && cache.StoredOn > DateTime.Now.AddHours(-1)) {
                SteamUserDetails user = cache.Deserialize<SteamUserDetails>();
                if (reloadWishlistPrices) {
                    LoadWishlistPrices(user);
                    user.Wishlist = user.Wishlist.OrderByDescending(x => x.Pricing.IsOnSale).ToList();
                }
                return user;
            }
            SteamUserDetails userDetails = new SteamUserDetails {
                User = _steamApi.GetPlayerSummary(id),
                OwnedApps = _steamApi.GetOwnedApps(id)
            };

            SteamScraper scraper = new SteamScraper();
            long? lastPlayedID = scraper.GetLastPlayedGame(userDetails.User.ProfileUrl);
            if (lastPlayedID.HasValue)
                userDetails.LastPlayedGame = _steamApi.GetAppStoreDetails(lastPlayedID.Value);

            List<long> wishlistIDs = scraper.GetWishlist(userDetails.User.ProfileUrl);
            foreach (long wishlistID in wishlistIDs) {
                userDetails.Wishlist.Add(_steamApi.GetAppStoreDetails(wishlistID));
            }
            userDetails.Wishlist = userDetails.Wishlist.OrderByDescending(x => x.Pricing != null && x.Pricing.IsOnSale).ToList();

            userDetails.SteamLevel = _steamApi.GetSteamLevel(userDetails.User.ID);
            userDetails.Friends = _steamApi.GetFriendList(userDetails.User.ID);

            bool isNew = cache == null;
            cache = new DataCache {
                EntityID = userDetails.User.ID.ToString(),
                CacheTypeID = CacheType.SteamUserDetails,
                StoredOn = DateTime.Now,
                JsonData = JsonConvert.SerializeObject(userDetails)
            };

            if (isNew) repo.Add(cache);
            else repo.Update(cache);

            return userDetails;
        }

        public Dictionary<long, string> GetAllApps() {
            return _memoryCache.GetAllSteamApps();
        }

        public List<SteamUser> GetBasicDetailsByIDs(IEnumerable<long> ids) {
            List<SteamUser> output = new List<SteamUser>();
            List<long> allIdsToUpdate = new List<long>();
            List<long> missingUserIDs = new List<long>();
            DataCacheRepository repo = _uow.GetRepo<DataCacheRepository>();
            Dictionary<string, DataCache> dictionary = repo.Get(ids.ToList(), CacheType.SteamUserBasicDetails).ToDictionary(x => x.EntityID);
            foreach (long id in ids) {
                DataCache cache = null;
                if (dictionary.ContainsKey(id.ToString()))
                    cache = dictionary[id.ToString()];
                if (cache != null && cache.StoredOn > DateTime.Now.AddHours(-1)) {
                    SteamUser user = cache.Deserialize<SteamUser>();
                    output.Add(user);
                } else {
                    allIdsToUpdate.Add(id);
                    if (cache == null)
                        missingUserIDs.Add(id);
                }
            }
            if (allIdsToUpdate.SafeAny()) {
                List<SteamUser> missingUsers = _steamApi.GetPlayerSummaries(allIdsToUpdate);
                foreach (SteamUser user in missingUsers) {
                    DataCache cache = new DataCache {
                        EntityID = user.ID.ToString(),
                        CacheTypeID = CacheType.SteamUserBasicDetails,
                        StoredOn = DateTime.Now,
                        JsonData = JsonConvert.SerializeObject(user)
                    };
                    if (missingUserIDs.Contains(user.ID))
                        repo.Add(cache);
                    else
                        repo.Update(cache);
                }
                output.AddRange(missingUsers);
            }
            return output;
        }

        public SteamUserGameStats GetByUserAndAppID(long userID, long appID) {
            DataCacheRepository repo = _uow.GetRepo<DataCacheRepository>();
            DataCache cache = repo.Get(userID, appID, CacheType.SteamUserGameStats);
            if (cache != null && cache.StoredOn > DateTime.Now.AddDays(-1)) {
                SteamUserGameStats user = cache.Deserialize<SteamUserGameStats>();
                return user;
            }

            SteamUserGameStats stats = new SteamUserGameStats {
                UserGameStats = _steamApi.GetUserStatsForGame(userID, appID),
                GameStatsSchema = _steamApi.GetSchemaForGame(appID)
            };

            bool isNew = cache == null;
            cache = new DataCache {
                EntityID = userID + "|" + appID,
                CacheTypeID = CacheType.SteamUserGameStats,
                StoredOn = DateTime.Now,
                JsonData = JsonConvert.SerializeObject(stats)
            };

            if (isNew) repo.Add(cache);
            else repo.Update(cache);

            return stats;
        }

        public List<UserAppPlaytime> GetRecentlyPlayedGames(long userID) {
            DataCacheRepository repo = _uow.GetRepo<DataCacheRepository>();
            DataCache cache = repo.Get(userID, CacheType.SteamUserAppPlaytime);
            if (cache != null && cache.StoredOn > DateTime.Now.AddDays(-1)) {
                return cache.Deserialize<List<UserAppPlaytime>>();
            }

            List<UserAppPlaytime> recentGames = _steamApi.GetRecentlyPlayedGames(userID);

            bool isNew = cache == null;
            cache = new DataCache {
                EntityID = userID.ToString(),
                CacheTypeID = CacheType.SteamUserGameStats,
                StoredOn = DateTime.Now,
                JsonData = JsonConvert.SerializeObject(recentGames)
            };

            if (isNew) repo.Add(cache);
            else repo.Update(cache);

            return recentGames;
        }

        public StoreApp GetStoreAppDetails(long appID) {
            return _steamApi.GetAppStoreDetails(appID);
        }

        private void LoadWishlistPrices(SteamUserDetails userDetails) {
            List<Pricing> prices = _steamApi.GetAppPrices(userDetails.Wishlist.Select(x => x.ID));
            foreach (StoreApp app in userDetails.Wishlist) {
                app.Pricing = prices.SingleOrDefault(x => x.AppID == app.ID);
            }
        }
    }
}
