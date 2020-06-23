using Microsoft.Extensions.Options;
using SteamTower;
using System;
using System.Collections.Generic;
using System.Text;
using TowerApiLib.Config;
using TowerApiLib.Repository;
using TowerApiLib.Services;

namespace TowerApiLib.Logic {
    public class LogicService {
        private readonly UnitOfWork _uow;
        private readonly SteamApiClient _steamApi;
        private readonly MemoryCacheService _memoryCache;

        public LogicService(UnitOfWork uow, SteamApiClient steamApiClient, MemoryCacheService memoryCache) {
            _uow = uow;
            _steamApi = steamApiClient;
            _memoryCache = memoryCache;
        }

        private SteamLogic _steamLogic;
        public SteamLogic SteamLogic {
            get {
                if (_steamLogic == null) _steamLogic = new SteamLogic(_uow, _steamApi, _memoryCache);
                return _steamLogic;
            }
        }
    }
}
