using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manulife.TopFiveWebsites.Service
{
    public interface ISearchService
    {
        IList<AggregateRecord> AggregateByDate(DateTime date, int topX);
        IList<AggregateRecord> AggregateByWebsite(string website, int topX);
    }
}
