using SteamTower;
using SteamTower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TowerApiLib.Domain;
using TowerApiLib.Repository;

namespace Website.ViewModels {
    //public class SteamUserViewModel {
        //public SteamUser User { get; set; }

        //public StoreApp LastPlayedGame { get; set; }

        //public List<StoreApp> Wishlist { get; set; } = new List<StoreApp>();

        //public int SteamLevel { get; set; }

        //public List<OwnedApp> OwnedApps { get; set; }

        //public int? OwnedAppCount {
        //    get {
        //        if (OwnedApps != null) return OwnedApps.Count;
        //        return null;
        //    }
        //}

        //public int FriendCount { get; set; }

        //public int WishlistCount {
        //    get {
        //        return Wishlist.Count;
        //    }
        //}

        //public int WishlistOnSaleCount {
        //    get {
        //        return Wishlist.Where(x => x.Pricing != null && x.Pricing.IsOnSale).Count();
        //    }
        //}


        //internal SteamUserViewModel Load(SteamUser user, UnitOfWork uow, SteamApiClient steamApi) {
        //    SteamUserCacheRepository repo = uow.GetRepo<SteamUserCacheRepository>();
        //    User = user;

        //    SteamUserCache cache = repo.GetByID(user.ID);
        //    if (cache != null && cache.StoredOn > DateTime.Now.AddDays(-1)) {
        //        SteamUserViewModel viewModel = JsonSerializer.Deserialize<SteamUserViewModel>(cache.JsonData);
        //        LoadWishlistPrices(viewModel, steamApi);
        //        viewModel.Wishlist = viewModel.Wishlist.OrderByDescending(x => x.Pricing.IsOnSale).ToList();
        //        return viewModel; // Return a cached results
        //    }

        //    OwnedApps = steamApi.GetOwnedApps(user.ID);

        //    SteamScraper scraper = new SteamScraper();
        //    long? lastPlayedID = scraper.GetLastPlayedGame(user.ProfileUrl);
        //    if (lastPlayedID.HasValue)
        //        LastPlayedGame = steamApi.GetAppStoreDetails(lastPlayedID.Value);

        //    List<long> wishlistIDs = scraper.GetWishlist(user.ProfileUrl);
        //    foreach (long wishlistID in wishlistIDs) {
        //        Wishlist.Add(steamApi.GetAppStoreDetails(wishlistID));
        //    }
        //    Wishlist = Wishlist.OrderByDescending(x => x.Pricing != null && x.Pricing.IsOnSale).ToList();

        //    SteamLevel = steamApi.GetSteamLevel(user.ID);
        //    FriendCount = steamApi.GetFriendList(user.ID).Count;

        //    bool isNew = false;
        //    if (cache == null)
        //        isNew = true;

        //    cache = new SteamUserCache {
        //        SteamID = user.ID,
        //        StoredOn = DateTime.Now,
        //        JsonData = JsonSerializer.Serialize(this)
        //    };

        //    if (isNew) repo.Add(cache);
        //    else repo.Update(cache);

        //    return this;
        //}

        
    //}
}
