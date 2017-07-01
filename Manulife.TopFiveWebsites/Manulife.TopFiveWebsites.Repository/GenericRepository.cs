using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Manulife.TopFiveWebsites.Entity;
using System.Data.Entity;

namespace Manulife.TopFiveWebsites.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly TopFiveWebsitesEntities _entities;

        public GenericRepository(TopFiveWebsitesEntities entities)
        {
            _entities = entities;
            _entities.Database.CommandTimeout = 60;
        }

        protected TopFiveWebsitesEntities Entities
        {
            get { return _entities; }
        }

        public int SaveChanges()
        {
            return _entities.SaveChanges();
        }

        public IQueryable<T> GetEntities<T>() where T : class
        {
            return _entities.Set<T>();
        }

        public IEnumerable<T> GetEntities<T>(string sql, params object[] parameters) where T : class
        {
            return _entities.Set<T>().SqlQuery(sql, parameters).ToList();
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
    }
}
