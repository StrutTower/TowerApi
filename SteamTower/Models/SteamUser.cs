using Newtonsoft.Json;
using SteamTower.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteamTower.Models {
    /// <summary>
    /// Steam User Info
    /// </summary>
    public class SteamUser {
        #region Public Data
        [JsonProperty("SteamID")]
        public long ID { get; set; }
        public string PersonaName { get; set; }

        public string ProfileUrl { get; set; }
        public string Avatar { get; set; }
        public string AvatarMedium { get; set; }
        public string AvatarFull { get; set; }
        public PersonaState PersonaState { get; set; }
        public CommunityVisibilityState CommunityVisibilityState { get; set; }
        public int ProfileState { get; set; }
        [JsonProperty("LastLogOff")]
        public long LastLogOffEpoch { get; set; }
        public int CommentPermission { get; set; }
        #endregion

        #region Private Data
        public string RealName { get; set; }
        public long PrimaryClanID { get; set; }
        [JsonProperty("TimeCreated")]
        public long TimeCreatedEpoch { get; set; }
        public long? GameID { get; set; }
        public string GameServerIP { get; set; }
        public string GameExtraInfo { get; set; }
        public string LocCountryCode { get; set; }
        public string LocStateCode { get; set; }
        public string LocCityID { get; set; }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        public List<OwnedApp> OwnedApps { get; set; }

        #region Calculated Properties
        public DateTime LastLoggedOff {
            get {
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                date = date.AddSeconds(LastLogOffEpoch).ToLocalTime();
                return date;
            }
        }

        public DateTime CreatedOn {
            get {
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                date = date.AddSeconds(TimeCreatedEpoch).ToLocalTime();
                return date;
            }
        }
        #endregion

        #region Overrides
        public override string ToString() {
            return string.Format($"{PersonaName}: {PersonaState}");
        }
        #endregion
    }
}
