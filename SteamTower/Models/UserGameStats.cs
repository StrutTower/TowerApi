using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamTower.Utilities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SteamTower.Models {
    public class UserGameStats {
        public long SteamID { get; set; }
        public int AppID { get; set; }
        public string GameName { get; set; }

        [JsonIgnore]
        //public List<UserStat> Stats { get; set; } = new List<UserStat>();
        public Dictionary<string, string> Stats { get; set; } = new Dictionary<string, string>();

        [JsonIgnore]
        // public List<UserAchievement> Achievements { get; set; } = new List<UserAchievement>();
        public Dictionary<string, bool> Achievements { get; set; } = new Dictionary<string, bool>();


        [JsonExtensionData]
        private IDictionary<string, JToken> _additionalData;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context) {
            if (_additionalData.ContainsKey("stats")) {
                foreach (var token in _additionalData["stats"]) {
                    Stats.Add(token["name"].ToString(), token["value"].ToString());
                }
            }

            if (_additionalData.ContainsKey("achievements")) {
                foreach (var token in _additionalData["achievements"]) {
                    Achievements.Add(token["name"].ToString(), token["achieved"].ToObject<bool>());
                }
            }
            Console.WriteLine();
        }

        public override string ToString() {
            return $"{GameName} Achievements and Stats";
        }
    }
}
