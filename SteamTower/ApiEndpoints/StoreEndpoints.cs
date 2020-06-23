using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamTower.Models;
using SteamTower.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SteamTower.ApiEndpoints {
    internal static class StoreEndpoints {
        private static readonly string BaseUrl = "http://store.steampowered.com/api/appdetails";

        public static StoreApp GetAppDetails(long id, string countryCode) {
            return ApiRequester.GetResponse<StoreApp>($"{BaseUrl}?appids={id}&cc={countryCode}", id.ToString(), "data");
        }

        public static List<Pricing> GetAppPrices(IEnumerable<long> appIDs, string countryCode) {
            if (appIDs == null || !appIDs.Any()) return null;
            string url = $"{BaseUrl}?appids={string.Join(",", appIDs)}&cc={countryCode}&filters=price_overview";

            string json = ApiRequester.GetJson(url);
            JObject tokens = JsonConvert.DeserializeObject<JObject>(json);

            List<Pricing> prices = new List<Pricing>();
            foreach (JProperty prop in tokens.Properties()) {
                var data = tokens[prop.Name]["data"];
                Pricing pricing;
                if (data.HasValues) {
                    pricing = data["price_overview"].ToObject<Pricing>();
                } else {
                    pricing = new Pricing {
                        Initial = null,
                        Final = null,
                        DiscountPercentage = null
                    };
                }
                pricing.AppID = Convert.ToInt64(prop.Name);
                prices.Add(pricing);
            }
            return prices;
        }
    }
}
