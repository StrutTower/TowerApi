using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamTower.Utilities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SteamTower.Models {
    public class StoreApp {
        [JsonProperty("steam_appid")]
        public long ID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        [JsonProperty("Required_Age")]
        public int RequiredAge { get; set; }

        [JsonProperty("Is_Free")]
        public bool IsFree { get; set; }

        [JsonProperty("Detailed_Description")]
        public string DetailedDescription { get; set; }

        [JsonProperty("About_The_Game")]
        public string AboutTheGame { get; set; }

        [JsonProperty("Short_Description")]
        public string ShortDescription { get; set; }

        [JsonProperty("controller_support")]
        public string ControllerSupport { get; set; }

        [JsonProperty("Supported_Languages")]
        public string SupportedLanguages { get; set; }

        [JsonProperty("Header_Image")]
        public string HeaderImage { get; set; }

        public string Website { get; set; }

        [JsonProperty("Release_Date")]
        public ReleaseDate ReleaseDate { get; set; }

        public Platforms Platforms { get; set; }

        public List<string> Developers { get; set; }

        public List<string> Publishers { get; set; }

        [JsonProperty("price_overview")]
        public Pricing Pricing { get; set; }

        [JsonIgnore]
        public List<string> Categories { get; set; } = new List<string>();

        [JsonIgnore]
        public List<string> Genres { get; set; } = new List<string>();

        [JsonProperty("background")]
        public string BackgroundImageUrl { get; set; }

        public int RecommendationCount { get; set; }

        public int MetacriticScore { get; set; }

        public override string ToString() {
            return $"{Name} - Type: {Type}";
        }

        public string CurrentPrice {
            get {
                if (Pricing != null) {
                    return Pricing.FinalDisplay;
                }
                if (ReleaseDate != null && ReleaseDate.IsComingSoon) {
                    return "TBA";
                }
                return "Unknown";
            }
        }

        [JsonExtensionData]
        private IDictionary<string, JToken> _additionalData;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            foreach (var token in _additionalData["categories"]) {
                Categories.Add(token["description"].ToString());
            }

            foreach (var token in _additionalData["genres"]) {
                Genres.Add(token["description"].ToString());
            }

            if (_additionalData.ContainsKey("recommendations")) {
                var token = _additionalData["recommendations"];
                if (token["total"] != null)
                    RecommendationCount = token["total"].ToObject<int>();
            }

            if (_additionalData.ContainsKey("metacritic")) {
                var token = _additionalData["metacritic"];
                if (token["total"] != null)
                    MetacriticScore = token["score"].ToObject<int>();
            }

        }
    }
}