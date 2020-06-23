using SteamTower.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerApiLib.Models {
    public class SteamUserGameStats {
        public GameStatsSchema GameStatsSchema { get; set; }

        public UserGameStats UserGameStats { get; set; }
    }
}
