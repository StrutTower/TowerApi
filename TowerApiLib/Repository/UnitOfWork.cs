using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using TowerSoft.Repository;
using TowerSoft.Repository.Interfaces;
using TowerSoft.Repository.MySql;

namespace TowerApiLib.Repository {
    public class UnitOfWork : IRepositoryUnitOfWork {
        public UnitOfWork(IConfiguration config) {
            DbAdapter = new MySqlDbAdapter(config.GetConnectionString("default"));
        }

        public IDbAdapter DbAdapter { get; }

        public void BeginTransaction() {
            DbAdapter.BeginTransaction();
        }

        public void CommitTransaction() {
            DbAdapter.CommitTransaction();
        }

        public void RollbackTransaction() {
            DbAdapter.RollbackTransaction();
        }

        public void Dispose() {
            DbAdapter.Dispose();
        }


        private readonly Dictionary<Type, object> _repos = new Dictionary<Type, object>();

        public TRepo GetRepo<TRepo>() {
            Type type = typeof(TRepo);

            if (!_repos.ContainsKey(type)) _repos[type] = Activator.CreateInstance(type, this);
            return (TRepo)_repos[type];
        }
    }
}
