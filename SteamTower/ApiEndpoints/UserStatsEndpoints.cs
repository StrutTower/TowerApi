using SteamTower.Models;
using SteamTower.Utilities;

namespace SteamTower.ApiEndpoints {
    internal static class UserStatsEndpoints {
        private static readonly string BaseUrl = "http://api.steampowered.com/ISteamUserStats";

        public static GameStatsSchema GetSchemaForGame(string apiKey, long appID) {
            return ApiRequester.GetResponse<GameStatsSchema>($"{BaseUrl}/GetSchemaForGame/v2?key={apiKey}&appid={appID}", "game", "availableGameStats");
        }

        public static UserGameStats GetUserStatsForGame(string apiKey, long steamID, long appID) {
            return ApiRequester.GetResponse<UserGameStats>($"{BaseUrl}/GetUserStatsForGame/v2?key={apiKey}&steamid={steamID}&appid={appID}", "playerstats");
        }
    }
}
