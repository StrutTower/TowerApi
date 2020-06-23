using Newtonsoft.Json;
using SteamTower.Utilities;
using System;

namespace SteamTower.Models {
    internal class WishlistItem : IComparable<WishlistItem> {
        public long AppID { get; set; }
        public int Priority { get; set; }

        public  int CompareTo(WishlistItem other) {
            if (Priority == other.Priority) {
                return 0;
            }
            if (other.Priority == 0) return -1;
            if (Priority == 0) return 1;
            return Priority - other.Priority;
        }

        public override string ToString() {
            return AppID + ": " + Priority;
        }
    }
}