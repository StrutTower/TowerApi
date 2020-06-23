using SteamTower.Models;
using SteamTower.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SteamTower.ApiEndpoints {
    internal static class UserEndpoints {
        private static readonly string BaseUrl = "http://api.steampowered.com/ISteamUser";

        public static List<SteamUser> GetPlayerSummaries(string apiKey, IEnumerable<long> steamIDs) {
            if (steamIDs.Count() > 100) throw new ArgumentOutOfRangeException("Too many steam IDs provided. 100 ID limit.");

            return ApiRequester.GetResponse<List<SteamUser>>($"{BaseUrl}/GetPlayerSummaries/v2?key={apiKey}&steamids={string.Join(",", steamIDs)}", "response", "players");
        }

        public static long? ResolveVanityUrl(string apiKey, string profileUrl) {
            if (profileUrl.EndsWith("/")) {
                profileUrl = profileUrl.Substring(0, profileUrl.Length - 1);
            }
            string vanity = profileUrl.Split('/').Last();

            return ApiRequester.GetResponse<long?>($"{BaseUrl}/ResolveVanityUrl/v1?key={apiKey}&vanityUrl={vanity}", "response", "steamid");
        }

        public static List<Friend> GetFriendList(string apiKey, long steamID) {
            return ApiRequester.GetResponse<List<Friend>>($"{BaseUrl}/GetFriendList/v1?key={apiKey}&steamid={steamID}", "friendslist", "friends");
        }
    }
}
