using SteamTower;
using SteamTower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerApiLib.Logic;
using TowerApiLib.Models;
using TowerApiLib.Repository;

namespace Website.ViewModels {
    public class OwnedAppInfoViewModel {
        public SteamUserDetails SteamUserDetails { get; set; }

        public UserAppPlaytime UserAppPlaytime { get; set; }

        public OwnedApp OwnedApp { get; set; }

        public SteamUserGameStats Stats { get; set; }

        public OwnedAppInfoViewModel Load(long steamID, long appID, LogicService logicService) {
            SteamUserDetails = logicService.SteamLogic.GetByUserID(steamID, reloadWishlistPrices: true);
            List<UserAppPlaytime> recents = logicService.SteamLogic.GetRecentlyPlayedGames(steamID);
            if (recents.Any()) {
                UserAppPlaytime recent = recents.SingleOrDefault(x => x.AppID == appID);
                UserAppPlaytime = recent;
            }
            OwnedApp = SteamUserDetails.OwnedApps.SingleOrDefault(x => x.AppID == appID);
            Stats = logicService.SteamLogic.GetByUserAndAppID(steamID, appID);
            return this;
        }
    }
}
