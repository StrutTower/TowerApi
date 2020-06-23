using Newtonsoft.Json.Linq;
using SteamTower.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamTower.ApiEndpoints {
    internal static class AppsEndpoints {
        private static readonly string BaseUrl = "http://api.steampowered.com/ISteamApps";

        public static Dictionary<long, string> GetAppList() {
            Dictionary<long, string> output = new Dictionary<long, string>();
            var jarray = ApiRequester.GetResponse<JArray>($"{BaseUrl}/GetAppList/v2", "applist", "apps");

            foreach(var token in jarray) {
                output.Add(token["appid"].ToObject<long>(), token["name"].ToString());
            }
            return output;
        }
    }
}
