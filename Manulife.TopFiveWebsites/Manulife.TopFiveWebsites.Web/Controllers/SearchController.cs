using DataTables.Mvc;
using Manulife.TopFiveWebsites.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manulife.TopFiveWebsites.Web.Controllers
{
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchServie)
        {
            _searchService = searchServie;
        }

        public ActionResult ListTopWebsites()
        {
            return View();
        }

        public ActionResult GetTopWebsites([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest request)
        {
            var result = _searchService.AggregateByDate(DateTime.Now.Date, request.Length);
            return Json(new DataTablesResponse(request.Draw, result, result.Count, result.Count)
                , JsonRequestBehavior.AllowGet);
        }
    }
}