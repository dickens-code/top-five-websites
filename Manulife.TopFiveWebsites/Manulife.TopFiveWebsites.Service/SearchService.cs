using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Manulife.TopFiveWebsites.Entity;
using Manulife.TopFiveWebsites.Repository;

namespace Manulife.TopFiveWebsites.Service
{
    public class SearchService : ISearchService
    {
        protected IGenericRepository _repository;

        public SearchService(IGenericRepository repository)
        {
            _repository = repository;
        }

        public IList<WebsiteStatistics> AggregateByDate(DateTime date, int topX)
        {
            //render as sql: SUM(...) GROUP BY ...
            return _repository.GetEntities<VisitLog>().Where(l => l.date <= date.Date)
                .GroupBy(k => k.website.ToLower())
                .Select(g => new WebsiteStatistics { Website = g.Key, TotalVisits = g.Sum(v => v.visits) })
                .OrderByDescending(r => r.TotalVisits)
                .Take(topX)
                .ToList();
        }

        public IList<WebsiteStatistics> AggregateByWebsite(string website, int topX)
        {
            //render as sql: SUM(...) GROUP BY ...
            return _repository.GetEntities<VisitLog>().Where(l => string.Compare(l.website, website, true) == 0)
                .GroupBy(k => k.date.Date)
                .Select(g => new WebsiteStatistics { Date = g.Key, TotalVisits = g.Sum(v => v.visits) })
                .OrderByDescending(r => r.TotalVisits)
                .Take(topX)
                .ToList();
        }
    }
}
