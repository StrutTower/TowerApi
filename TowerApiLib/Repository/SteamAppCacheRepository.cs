using System;
using System.Collections.Generic;
using System.Text;
using TowerApiLib.Domain;
using TowerSoft.Repository;

namespace TowerApiLib.Repository {
    public class SteamAppCacheRepository : Repository<SteamAppCache> {
        public SteamAppCacheRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public SteamAppCache GetByID(long id) {
            return GetSingleEntity(WhereEqual(x => x.ID, id));
        }
    }
}
