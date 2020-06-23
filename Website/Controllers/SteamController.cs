using TowerApiLib.Config;
using Website.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SteamTower.Models;
using Website.ViewModels;
using TowerApiLib.Repository;
using TowerApiLib.Logic;
using TowerApiLib.Models;

namespace Website.Controllers {
    public class SteamController : CustomController {
        private readonly AppSettings _appSettings;
        private readonly UnitOfWork _uow;
        private readonly LogicService _logicService;

        public SteamController(IOptions<AppSettings> appSettings, UnitOfWork uow, LogicService logicService) {
            _appSettings = appSettings.Value;
            _uow = uow;
            _logicService = logicService;
        }

        public IActionResult Index() {
            var app = _logicService.SteamLogic.GetStoreAppDetails(218980);

            return View();
        }

        public IActionResult ResolveVanityUrl(string profileUrl) {
            long? id = _logicService.SteamLogic.ResolveVanityUrl(profileUrl);
            if (id.HasValue) {
                return RedirectToAction("SteamUser", new { id = id.Value });
            }
            return Message($"Unable to find a user for the steam profile URL {profileUrl}");
        }

        public IActionResult SteamUser(long id) {
            return View("User", _logicService.SteamLogic.GetByUserID(id, reloadWishlistPrices: true));
        }

        public IActionResult OwnedApps(long id) {
            return View(_logicService.SteamLogic.GetByUserID(id));
        }

        public IActionResult OwnedAppInfo(long steamID, long appID) {
            return View(new OwnedAppInfoViewModel().Load(steamID, appID, _logicService));
        }

        public IActionResult App(long id) {
            StoreApp app = _logicService.SteamLogic.GetStoreAppDetails(id);
            return View(app);
        }

        public IActionResult Friends(long id) {
            SteamUserDetails user = _logicService.SteamLogic.GetByUserID(id);
            List<SteamUser> friends = _logicService.SteamLogic.GetBasicDetailsByIDs(user.Friends.Select(x => x.FriendID));
            var allGames = _logicService.SteamLogic.GetAllApps();
            return View(new SteamFriendListViewModel().Load(user, friends, allGames));
        }
    }
}
