using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manulife.TopFiveWebsites.Repository
{
    public interface IWriteableRepository
    {
        int SaveChanges();
        void AddEntity<T>(T entity) where T : class;
        void DeleteEntry<T>(T entity) where T : class;
        void EditEntry<T>(T entity) where T : class;
        void TruncateStore<T>() where T : class;
    }
}
