using DataTables.Mvc;
using Manulife.TopFiveWebsites.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manulife.TopFiveWebsites.Web.Controllers
{
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly IVisitLogService _visitLogService;

        public SearchController(ISearchService searchServie, IVisitLogService visitLogService)
        {
            _searchService = searchServie;
            _visitLogService = visitLogService;
        }

        public ActionResult ListTopWebsites()
        {
            return View();
        }

        public ActionResult GetTopWebsites([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest request)
        {
            //parse search input
            DateTime searchDate;
            if (!DateTime.TryParse(request.Search.Value, out searchDate))
                searchDate = DateTime.Now.Date;

            //persist exclusion entries to db so as to leverage db-side optimization
            _visitLogService.PersistExclusionEntries(false);

            //search top websites
            var result = _searchService.AggregateByDate(searchDate, request.Length);

            //sort result if user choose to
            var sortedColumn = request.Columns.FirstOrDefault(c => c.IsOrdered);
            result = SortResult(result, sortedColumn);

            //return data in json format
            return Json(new DataTablesResponse(request.Draw, result, result.Count, result.Count)
                , JsonRequestBehavior.AllowGet);
        }

        private IList<WebsiteStatistics> SortResult(IList<WebsiteStatistics> result, Column sortedColumn)
        {
            if (sortedColumn == null)
                return result;

            if (sortedColumn.Data == "Website" && sortedColumn.SortDirection == Column.OrderDirection.Ascendant)
                return result.OrderBy(r => r.Website).ToList();
            else if (sortedColumn.Data == "Website" && sortedColumn.SortDirection == Column.OrderDirection.Descendant)
                return result.OrderByDescending(r => r.Website).ToList();
            else if (sortedColumn.Data == "TotalVisits" && sortedColumn.SortDirection == Column.OrderDirection.Ascendant)
                return result.OrderBy(r => r.TotalVisits).ToList();
            else if (sortedColumn.Data == "TotalVisits" && sortedColumn.SortDirection == Column.OrderDirection.Descendant)
                return result.OrderByDescending(r => r.TotalVisits).ToList();

            return result;
        }
    }
}