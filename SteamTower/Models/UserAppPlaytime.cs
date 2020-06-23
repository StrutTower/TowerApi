using Newtonsoft.Json;
using SteamTower.Utilities;
using System;

namespace SteamTower.Models {
    public class UserAppPlaytime {
        public uint AppID { get; set; }

        public string Name { get; set; }

        [JsonProperty("PlayTime_2Weeks")]
        public int PlayTimeMinutes2Weeks { get; set; }

        [JsonProperty("PlayTime_Forever")]
        public int PlayTimeMinutesForever { get; set; }

        [JsonProperty("Img_Icon_Url")]
        public string ImgIconHash { get; set; }

        [JsonProperty("Img_Logo_Url")]
        public string ImgLogoHash { get; set; }

        public TimeSpan PlayTime2Weeks {
            get {
                return new TimeSpan(0, PlayTimeMinutes2Weeks, 0);
            }
        }

        public TimeSpan PlayTimeForever {
            get {
                return new TimeSpan(0, PlayTimeMinutesForever, 0);
            }
        }

        public string ImgIconUrl {
            get {
                return $"http://media.steampowered.com/steamcommunity/public/images/apps/{AppID}/{ImgIconHash}.jpg";
            }
        }

        public string ImgLogoUrl {
            get {
                return $"http://media.steampowered.com/steamcommunity/public/images/apps/{AppID}/{ImgLogoHash}.jpg";
            }
        }

        public override string ToString() {
            return $"{Name} - Total Playtime: {PlayTimeForever}";
        }
    }
}
