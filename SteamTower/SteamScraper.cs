using AngleSharp;
using AngleSharp.Dom;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamTower.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace SteamTower {
    /// <summary>
    /// Provides access to Steam info that is not accessible in the API
    /// </summary>
    public class SteamScraper {
        /// <summary>
        /// Do not change unless not working. Regex statement for extracting the vanity 
        /// profile name. The first capture group must be the vanity profile name.
        /// </summary>
        public string ProfileUrlVanityRegex { get; set; } = Constants.ProfileUrlVanityRegex;

        /// <summary>
        /// Do not change unless not working. URL for recent games in Steam profiles.
        /// [PROFILENAME] will be replaced with the vanity profile name.
        /// </summary>
        public string RecentGamesUrl { get; set; } = Constants.RecentGamesUrl;

        /// <summary>
        /// Do not change unless not working. URL for wishlist in Steam profiles.
        /// [PROFILENAME] will be replaced with the vanity profile name.
        /// </summary>
        public string WishlistUrl { get; set; } = Constants.WishlistUrl;

        /// <summary>
        /// Returns the Steam AppID for the last played game for the profile provided
        /// </summary>
        /// <param name="profileUrl">URL for the user's Steam profile</param>
        /// <returns></returns>
        public int? GetLastPlayedGame(string profileUrl) {
            Match profileMatch = Regex.Match(profileUrl, ProfileUrlVanityRegex);
            string profileName = profileMatch.Groups[1].Value;
            string url = RecentGamesUrl.Replace("[PROFILENAME]", profileName);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (WebClient client = new WebClient()) {
                client.UseDefaultCredentials = true;

                string page = client.DownloadString(url);

                Match match = Regex.Match(page, "var rgGames = (.*?);");
                string json = match.Groups[1].Value;

                JArray array = JsonConvert.DeserializeObject<JArray>(json);

                if (array != null && array.First != null) {
                    return Convert.ToInt32(array.First["appid"].ToString());
                }
                return null;
            }
        }

        /// <summary>
        /// Returns a list of Steam AppIDs from the wishlist of the profile provided.
        /// List is order by the user's wishlist ranking
        /// </summary>
        /// <param name="profileUrl">URL for the user's Steam profile</param>
        /// <returns></returns>
        public List<long> GetWishlist(string profileUrl) {
            List<long> appIDs = new List<long>();
            Match profileMatch = Regex.Match(profileUrl, ProfileUrlVanityRegex);
            string profileName = profileMatch.Groups[1].Value;
            string url = WishlistUrl.Replace("[PROFILENAME]", profileName);

            IBrowsingContext context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            IDocument doc = context.OpenAsync(address: url).Result;

            IElement element = doc.DocumentElement.QuerySelector(".responsive_page_template_content");

            if (element != null) {
                var scriptNodes = element.QuerySelectorAll("script");
                var scriptNode = scriptNodes.Where(x => x.TextContent.Contains("g_rgWishlistData")).FirstOrDefault();
                var script = scriptNode.TextContent;

                Match match = Regex.Match(script, @"g_rgWishlistData = (.*?);");

                List<WishlistItem> wishlist = JsonConvert.DeserializeObject<List<WishlistItem>>(match.Groups[1].Value);
                wishlist.Sort();
                appIDs = wishlist.Select(x => x.AppID).ToList();
            }
            return appIDs;
        }
    }
}
