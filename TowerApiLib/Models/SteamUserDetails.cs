using SteamTower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerApiLib.Models {
    public class SteamUserDetails {
        public SteamUser User { get; set; }
        public int SteamLevel { get; set; }

        public StoreApp LastPlayedGame { get; set; }


        public List<OwnedApp> OwnedApps { get; set; }

        public int? OwnedAppCount {
            get {
                if (OwnedApps != null) return OwnedApps.Count;
                return null;
            }
        }

        public List<Friend> Friends { get; set; }

        public int FriendCount { 
            get {
                return Friends.Count;
            }
        }

        public List<StoreApp> Wishlist { get; set; } = new List<StoreApp>();

        public int WishlistCount {
            get {
                return Wishlist.Count;
            }
        }

        public int WishlistOnSaleCount {
            get {
                return Wishlist.Where(x => x.Pricing != null && x.Pricing.IsOnSale).Count();
            }
        }
    }
}
