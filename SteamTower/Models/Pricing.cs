using Newtonsoft.Json;
using SteamTower.Utilities;

namespace SteamTower.Models {
    public class Pricing {
        public long AppID { get; set; }

        public string Currency { get; set; }

        public int? Initial { get; set; }

        public int? Final { get; set; }

        [JsonProperty("discount_percent")]
        public int? DiscountPercentage { get; set; }

        [JsonProperty("initial_formatted")]
        public string InitialDisplay { get; set; }

        [JsonProperty("final_formatted")]
        public string FinalDisplay { get; set; }

        public string DiscountPercentageDisplay {
            get {
                if (DiscountPercentage.HasValue) {
                    return string.Format("{0}%", DiscountPercentage);
                }
                return string.Empty;
            }
        }

        public bool IsOnSale {
            get {
                return DiscountPercentage.HasValue && DiscountPercentage != 0;
            }
        }

        public override string ToString() {
            if (string.IsNullOrWhiteSpace(FinalDisplay)) return base.ToString();
            return FinalDisplay;
        }
    }
}
