using Newtonsoft.Json;
using SteamTower.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamTower.Models {
    public class AchievementSchema {
        public string Name { get; set; }

        public string DefaultValue { get; set; }

        public string DisplayName { get; set; }

        public bool Hidden { get; set; }

        public string Description { get; set; }

        [JsonProperty("Icon")]
        public string IconUrl { get; set; }

        [JsonProperty("IconGray")]
        public string IconGreyUrl { get; set; }


        public override string ToString() {
            return DisplayName;
        }
    }
}
