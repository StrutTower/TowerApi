using Newtonsoft.Json;
using SteamTower.Utilities;

namespace SteamTower.Models {
    public class OwnedApp {
        public int AppID { get; set; }

        public string Name { get; set; }

        [JsonProperty("playtime_forever")]
        public int PlaytimeForever { get; set; }

        [JsonProperty("img_icon_url")]
        public string IconHash { get; set; }

        [JsonProperty("img_logo_url")]
        public string LogoHash { get; set; }

        [JsonProperty("has_community_visible_stats")]
        public bool HasVisibleStats { get; set; }

        public string IconUrl {
            get {
                return "http://media.steampowered.com/steamcommunity/public/images/apps/" + AppID + "/" + IconHash + ".jpg";
            }
        }

        public string LogoUrl {
            get {
                return "http://media.steampowered.com/steamcommunity/public/images/apps/" + AppID + "/" + LogoHash + ".jpg";
            }
        }

        public string StoreLogoUrl {
            get {
                return "http://cdn.akamai.steamstatic.com/steam/apps/" + AppID + "/header.jpg";
            }
        }

        public  string BackgroundImageUrl {
            get {
                return $"https://steamcdn-a.akamaihd.net/steam/apps/{AppID}/page_bg_generated_v6b.jpg";
            }
        }

        public override string ToString() {
            return Name;
        }
    }
}
