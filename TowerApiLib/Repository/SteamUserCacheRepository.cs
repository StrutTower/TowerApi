using System;
using System.Collections.Generic;
using System.Text;
using TowerApiLib.Domain;
using TowerSoft.Repository;

namespace TowerApiLib.Repository {
    public class SteamUserCacheRepository : Repository<SteamUserCache> {
        public SteamUserCacheRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public SteamUserCache GetByID(long steamID) {
            return GetSingleEntity(WhereEqual(x => x.SteamID, steamID));
        }
    }
}
