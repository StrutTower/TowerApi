using Newtonsoft.Json;
using SteamTower.Utilities;

namespace SteamTower.Models {
    public class StatsSchema {
        public string Name { get; set; }

        public ulong DefaultValue { get; set; }

        public string DisplayName { get; set; }

        public override string ToString() {
            return DisplayName;
        }
    }
}
