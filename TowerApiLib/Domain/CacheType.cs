using System;
using System.Collections.Generic;
using System.Text;

namespace TowerApiLib.Domain {
    public enum CacheType {
        SteamUserDetails = 1,
        SteamUserGameStats,
        SteamUserBasicDetails,
        SteamGameDictionary,
        SteamUserAppPlaytime
    }
}
