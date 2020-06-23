using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TowerApiLib.Domain;
using TowerSoft.Repository;

namespace TowerApiLib.Repository {
    public class DataCacheRepository : Repository<DataCache> {
        public DataCacheRepository(UnitOfWork uow) : base(uow.DbAdapter) { }

        public DataCache Get(string id, CacheType cacheType) {
            return GetSingleEntity(new[] {
                WhereEqual(x => x.EntityID, id),
                WhereEqual(x => x.CacheTypeID, cacheType)
            });
        }

        public List<DataCache> Get(List<string> ids, CacheType cacheType) {
            QueryBuilder query = GetQueryBuilder();
            List<string> inStatements = new List<string>();
            for (int i = 0; i < ids.Count(); i++) {
                inStatements.Add($"@{i}");
                query.AddParameter($"@{i}", ids[i]);
            }
            query.SqlQuery += $"WHERE EntityID IN ({string.Join(",", inStatements)})";
            return GetEntities(query);
        }

        public DataCache Get(string id1, string id2, CacheType cacheType) {
            return GetSingleEntity(new[] {
                WhereEqual(x => x.EntityID, id1 + "|" + id2),
                WhereEqual(x => x.CacheTypeID, cacheType)
            });
        }

        public DataCache Get(long id, CacheType cacheType) {
            return Get(id.ToString(), cacheType);
        }

        public List<DataCache> Get(List<long> ids, CacheType cacheType) {
            List<string> idStrings = new List<string>();
            ids.ForEach(x => idStrings.Add(x.ToString()));
            return Get(idStrings, cacheType);
        }

        public DataCache Get(long id1, long id2, CacheType cacheType) {
            return Get(id1.ToString(), id2.ToString(), cacheType);
        }
    }
}
