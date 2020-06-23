using Newtonsoft.Json;
using SteamTower.Utilities;
using System;

namespace SteamTower.Models {
    public class ReleaseDate {
        [JsonProperty("coming_soon")]
        public bool IsComingSoon { get; set; }

        [JsonProperty("date")]
        public string DateString { get; set; }


        public DateTime? Date {
            get {
                if (DateTime.TryParse(DateString, out DateTime parsed)) {
                    return parsed;
                }
                return null;
            }
        }

        public override string ToString() {
            if (Date.HasValue) return Date.Value.ToShortDateString();
            return base.ToString();
        }
    }
}
