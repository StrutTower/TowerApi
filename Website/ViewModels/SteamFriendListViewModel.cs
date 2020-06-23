using SteamTower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerApiLib.Models;

namespace Website.ViewModels {
    public class SteamFriendListViewModel {
        public SteamUserDetails User { get; set; }

        public List<SteamUser> Friends { get; set; }

        public Dictionary<long, string> AllApps { get; set; }

        public SteamFriendListViewModel Load(SteamUserDetails user, List<SteamUser> friends, Dictionary<long, string> allApps) {
            User = user;
            Friends = friends;
            AllApps = allApps;
            return this;
        }
    }
}
