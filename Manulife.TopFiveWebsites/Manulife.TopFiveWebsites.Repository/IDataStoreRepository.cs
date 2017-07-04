using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manulife.TopFiveWebsites.Repository
{
    public interface IDataStoreRepository : IReadonlyRepository, IWriteableRepository
    {
    }
}
