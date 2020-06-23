using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace YoutubeWrapper.Utilities {
    internal class ApiRequestor {
        internal static T GetResponse<T>(string url, IDictionary<string, string> query) {
            return GetResponse<T>(url, query.Select(x => x.Key + "=" + x.Value).ToArray());
        }

        internal static T GetResponse<T>(string url, params string[] queryParts) {
            string fullUrl = url + "?" + string.Join("&", queryParts);

            using (WebClient client = new WebClient()) {
                string response = client.DownloadString(fullUrl);
                JObject obj = JsonConvert.DeserializeObject<JObject>(response);

                //if (obj.ContainsKey("nextPageToken")) {

                //}


                return obj["items"].ToObject<T>();
            }
        }
    }
}
