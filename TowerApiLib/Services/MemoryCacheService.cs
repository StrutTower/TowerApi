using Microsoft.Extensions.DependencyInjection;
using SteamTower;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerApiLib.Services {
    public class MemoryCacheService {
        private readonly IServiceProvider _serviceProvider;

        public MemoryCacheService(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }


        private DateTime? _allSteamAppsCachedOn = null;
        private Dictionary<long, string> _allSteamApps;

        public Dictionary<long,string> GetAllSteamApps() {
            if (_allSteamApps != null && _allSteamAppsCachedOn != null && _allSteamAppsCachedOn > DateTime.Now.AddDays(-1)) {
                return _allSteamApps;
            }
            SteamApiClient steamApiClient = _serviceProvider.GetService<SteamApiClient>();
            _allSteamApps = steamApiClient.GetAppList();
            _allSteamAppsCachedOn = DateTime.Now;
            return _allSteamApps;
        }
    }
}
