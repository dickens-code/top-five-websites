using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Manulife.TopFiveWebsites.Entity;
using Manulife.TopFiveWebsites.Repository.Interface;

namespace Manulife.TopFiveWebsites.Repository
{
    public class DataStoreRepository : IDataStoreRepository
    {
        protected readonly TopFiveWebsitesEntities _entities;

        public DataStoreRepository(TopFiveWebsitesEntities entities)
        {
            //fix as suggested in https://social.msdn.microsoft.com/Forums/en-US/b348a0c2-94d9-4db5-a041-b81a97e76b3f/entityframeworksqlserver-not-deploying?forum=adodotnetentityframework
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            _entities = entities;
            _entities.Database.CommandTimeout = 60;
        }

        public int SaveChanges()
        {
            return _entities.SaveChanges();
        }

        public IQueryable<T> GetEntities<T>() where T : class
        {
            return _entities.Set<T>();
        }

        public void AddEntity<T>(T entity) where T : class
        {
            _entities.Set<T>().Add(entity);
        }

        public void DeleteEntry<T>(T entity) where T : class
        {
            _entities.Set<T>().Remove(entity);
        }

        public void EditEntry<T>(T entity) where T : class
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public void TruncateStore<T>() where T : class
        {
            _entities.Database.ExecuteSqlCommand($"TRUNCATE TABLE {typeof(T).Name}");
        }
    }
}
