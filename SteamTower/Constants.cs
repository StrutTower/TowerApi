using System;
using System.Collections.Generic;
using System.Text;

namespace SteamTower {
    internal class Constants {
        internal const string ProfileUrlVanityRegex = @"https?:\/\/steamcommunity.com\/id\/(.*?)(\Z|\/)";
        internal const string RecentGamesUrl = "https://steamcommunity.com/id/[PROFILENAME]/games?tab=recent";
        internal const string WishlistUrl = "https://store.steampowered.com/wishlist/id/[PROFILENAME]/";
    }
}
