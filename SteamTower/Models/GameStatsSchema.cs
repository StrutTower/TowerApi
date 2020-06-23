using Newtonsoft.Json;
using SteamTower.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamTower.Models {
    public class GameStatsSchema {
        public List<AchievementSchema> Achievements { get; set; } = new List<AchievementSchema>();
        public List<StatsSchema> Stats { get; set; } = new List<StatsSchema>();
    }
}
