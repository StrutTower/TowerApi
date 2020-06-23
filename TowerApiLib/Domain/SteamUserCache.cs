using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TowerApiLib.Domain {
   public class SteamUserCache {
        [Key]
        public long SteamID { get; set; }

        public DateTime StoredOn { get; set; }

        public string JsonData { get; set; }
    }
}
