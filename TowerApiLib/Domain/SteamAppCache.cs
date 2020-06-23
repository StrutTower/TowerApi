using System;
using System.Collections.Generic;
using System.Text;

namespace TowerApiLib.Domain {
    public class SteamAppCache {
        public long ID { get; set; }

        public DateTime StoredOn { get; set; }

        public string JsonData { get; set; }
    }
}
