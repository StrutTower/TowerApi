using SteamTower.ApiEndpoints;
using SteamTower.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SteamTower {
    /// <summary>
    /// Steam API wrapper class
    /// </summary>
    public class SteamApiClient {
        private string ApiKey { get; set; }

        /// <summary>
        /// Country code. Used for the Steam store pricing. Default = us
        /// </summary>
        public string CountyCode { get; set; } = "us";

        /// <summary>
        /// Create a new instance of the Steam API wrapper class
        /// </summary>
        /// <param name="apiKey">Your Steam API key</param>
        public SteamApiClient(string apiKey = "") {
            if (!string.IsNullOrWhiteSpace(apiKey)) ApiKey = apiKey;
        }

        /// <summary>
        /// Sets the API key value if it wasn't set during construction
        /// </summary>
        /// <param name="apiKey">API key to set</param>
        public void SetApiKey(string apiKey) {
            ApiKey = apiKey;
        }

        #region SteamStore
        /// <summary>
        /// Get details about an app from the Steam store. Does not require an API key.
        /// </summary>
        /// <param name="appID">Steam app ID</param>
        public StoreApp GetAppStoreDetails(long appID) {
            return StoreEndpoints.GetAppDetails(appID, CountyCode);
        }

        /// <summary>
        /// Get the pricing details for multiple apps. Does not require an API key.
        /// </summary>
        /// <param name="appIDs">List of IDs for the games</param>
        public List<Pricing> GetAppPrices(IEnumerable<long> appIDs) {
            return StoreEndpoints.GetAppPrices(appIDs, CountyCode);
        }
        #endregion

        #region ISteamApps
        /// <summary>
        /// Get a basic list of all Steam apps
        /// </summary>
        public Dictionary<long, string> GetAppList() {
            ThrowMissingApiKey();
            return AppsEndpoints.GetAppList();
        }
        #endregion

        #region ISteamNews
        /// <summary>
        /// Get recent news about an app
        /// </summary>
        /// <param name="appID">Steam app ID</param>
        /// <param name="returnCount">Optional. Number of results to return. Default is 5.</param>
        //public List<AppNews> GetNewsForApp(uint appID, int returnCount = 5) {
        //    return new SteamNewsInterface().GetNewsForApp(appID, returnCount);
        //}
        #endregion

        #region ISteamUser
        /// <summary>
        /// Get a player's information. Some information is not available if the player's 
        /// profile is private or friends only. Requires an API key.
        /// </summary>
        /// <param name="steamID">Player's Steam ID</param>
        public SteamUser GetPlayerSummary(long steamID) {
            ThrowMissingApiKey();
            return GetPlayerSummaries(new[] { steamID }).FirstOrDefault();
        }

        /// <summary>
        /// Get player information for multiple players. Some information is not available if 
        /// the player's profile is private or friends only. Requires an API key.
        /// </summary>
        /// <param name="steamIDs">List of player's Steam IDs</param>
        public List<SteamUser> GetPlayerSummaries(IEnumerable<long> steamIDs) {
            ThrowMissingApiKey();
            return UserEndpoints.GetPlayerSummaries(ApiKey, steamIDs);
        }

        /// <summary>
        /// Converts a Steam user's vanity profile URL to their Steam ID
        /// </summary>
        /// <param name="profileUrl">URL to the users profile homepage</param>
        /// <returns></returns>
        public long? ResolveVanityUrl(string profileUrl) {
            ThrowMissingApiKey();
            return UserEndpoints.ResolveVanityUrl(ApiKey, profileUrl);
        }

        /// <summary>
        /// Returns a list of the user's friends
        /// </summary>
        /// <param name="steamID">Steam ID of the user</param>
        /// <returns></returns>
        public List<Friend> GetFriendList(long steamID) {
            ThrowMissingApiKey();
            return UserEndpoints.GetFriendList(ApiKey, steamID);
        }
        #endregion

        #region IPlayerService
        /// <summary>
        /// Returns a list of the apps owned by the user
        /// </summary>
        /// <param name="steamID">Steam ID of the user</param>
        /// <param name="includeAppInfo">Include info about the apps such as name</param>
        /// <param name="includePlayedFreeGames">Include free games that have been played by the user</param>
        /// <returns></returns>
        public List<OwnedApp> GetOwnedApps(long steamID, bool includeAppInfo = true, bool includePlayedFreeGames = false) {
            ThrowMissingApiKey();
            return PlayerServiceEndpoints.GetOwnedApps(ApiKey, steamID, includeAppInfo, includePlayedFreeGames);
        }

        /// <summary>
        /// Get the Steam level for the user
        /// </summary>
        /// <param name="steamID">Steam ID of the user</param>
        /// <returns></returns>
        public int GetSteamLevel(long steamID) {
            ThrowMissingApiKey();
            return PlayerServiceEndpoints.GetSteamLevel(ApiKey, steamID);
        }

        /// <summary>
        /// Get a list of the games the user has recently played
        /// </summary>
        /// <param name="steamID">Steam ID of the user</param>
        /// <param name="count">Number of recent games to get</param>
        /// <returns></returns>
        public List<UserAppPlaytime> GetRecentlyPlayedGames(long steamID, int count = 10) {
            ThrowMissingApiKey();
            return PlayerServiceEndpoints.GetRecentlyPlayedGames(ApiKey, steamID, count);
        }
        #endregion

        #region ISteamUserStats
        /// <summary>
        /// Get the schema info of a game for achievement and stats
        /// </summary>
        /// <param name="appID">Id of the game</param>
        /// <returns></returns>
        public GameStatsSchema GetSchemaForGame(long appID) {
            ThrowMissingApiKey();
            return UserStatsEndpoints.GetSchemaForGame(ApiKey, appID);
        }

        /// <summary>
        /// Get the user's achievement and stats for a game
        /// </summary>
        /// <param name="steamID">Steam ID of the user</param>
        /// <param name="appID">ID of the game</param>
        /// <returns></returns>
        public UserGameStats GetUserStatsForGame(long steamID, long appID) {
            ThrowMissingApiKey();
            return UserStatsEndpoints.GetUserStatsForGame(ApiKey, steamID, appID);
        }
        #endregion

        /// <summary>
        /// Throws an error if the API key is blank
        /// </summary>
        private void ThrowMissingApiKey() {
            if (string.IsNullOrWhiteSpace(ApiKey)) throw new Exception("Steam API key is missing");
        }
    }
}
