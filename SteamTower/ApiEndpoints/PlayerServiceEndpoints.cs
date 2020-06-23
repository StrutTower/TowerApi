using SteamTower.Models;
using SteamTower.Utilities;
using System.Collections.Generic;

namespace SteamTower.ApiEndpoints {
    internal class PlayerServiceEndpoints {
        private static readonly string BaseUrl = "http://api.steampowered.com/IPlayerService";

        public static List<OwnedApp> GetOwnedApps(string apiKey, long steamID, bool includeAppInfo = false, bool includePlayedFreeGames = false) {
            string url = $"{BaseUrl}/GetOwnedGames/v0001?key={apiKey}&steamid={steamID}&include_appinfo={includeAppInfo}&include_played_free_games={includePlayedFreeGames}";

            return ApiRequester.GetResponse<List<OwnedApp>>(url, "response", "games");
        }

        public static int GetSteamLevel(string apiKey, long steamID) {
            return ApiRequester.GetResponse<int>($"{BaseUrl}/GetSteamLevel/v1?key={apiKey}&steamid={steamID}", "response", "player_level");
        }

        public static List<UserAppPlaytime> GetRecentlyPlayedGames(string apiKey, long steamID, int count = 10) {
            return ApiRequester.GetResponse<List<UserAppPlaytime>>($"{BaseUrl}/GetRecentlyPlayedGames/v1?key={apiKey}&steamid={steamID}&count={count}", "response", "games");
        }
    }
}
