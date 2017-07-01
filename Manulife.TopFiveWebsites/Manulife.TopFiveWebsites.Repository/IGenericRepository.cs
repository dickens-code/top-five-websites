using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manulife.TopFiveWebsites.Repository
{
    public interface IGenericRepository
    {
        int SaveChanges();
        IQueryable<T> GetEntities<T>() where T : class;
        IEnumerable<T> GetEntities<T>(string sql, params object[] parameters) where T : class;
        void AddEntity<T>(T entity) where T : class;
        void DeleteEntry<T>(T entity) where T : class;
        void EditEntry<T>(T entity) where T : class;
    }
}
