using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SteamTower.Utilities {
    internal static class ApiRequester {
        /// <summary>
        /// Get the JSON stringresponse from a URL
        /// </summary>
        /// <param name="url">URL to get from</param>
        /// <returns></returns>
        public static string GetJson(string url) {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (WebClient client = new WebClient()) {
                client.UseDefaultCredentials = true;
                return client.DownloadString(url);
            }
        }

        /// <summary>
        /// Get the response from an API and convert to an object.
        /// </summary>
        /// <typeparam name="T">Object to convert to</typeparam>
        /// <param name="url">URL to download from</param>
        /// <param name="jsonResponseToken">JSON token name to drill down to before converting</param>
        /// <param name="jsonSecondResponseToken">Second JSON token name to drill down to before converting</param>
        /// <returns></returns>
        public static T GetResponse<T>(string url, string jsonResponseToken, string jsonSecondResponseToken = null) {
            var type = typeof(T);

            string json = GetJson(url);

            JToken token = JsonConvert.DeserializeObject<JObject>(json)[jsonResponseToken];

            if (!string.IsNullOrWhiteSpace(jsonSecondResponseToken))
                token = token[jsonSecondResponseToken];

            if (token == null) return default;

            return token.ToObject<T>();
        }
    }
}