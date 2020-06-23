using Newtonsoft.Json;
using SteamTower.Utilities;
using System;

namespace SteamTower.Models {
    public class Friend {
        [JsonProperty("steamid")]
        public long FriendID { get; set; }

        [JsonProperty("friend_since")]
        public long FriendSinceEpoch { get; set; }


        public DateTime FriendSince {
            get {
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                date = date.AddSeconds(FriendSinceEpoch).ToLocalTime();
                return date;
            }
        }

        public override string ToString() {
            return $"SteamID: {FriendID}";
        }
    }
}
