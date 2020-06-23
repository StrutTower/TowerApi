using Newtonsoft.Json;
using SteamTower.Utilities;
using System.Collections.Generic;

namespace SteamTower.Models {
    public class Platforms {

        public bool Windows { get; set; }

        public bool Mac { get; set; }

        public bool Linux { get; set; }
        
        public override string ToString() {
            return $"Windows:{Windows},Mac:{Mac},Linux:{Linux}";
        }
    }
}