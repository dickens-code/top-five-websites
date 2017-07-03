using DataTables.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manulife.TopFiveWebsites.Web.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult ListTopWebsites()
        {
            return View();
        }

        public ActionResult GetTopWebsites([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest request)
        {
            return Json(new DataTablesResponse(request.Draw, new[] { new { Date = "2016-01-07", Website = "www.google.com", TotalVisits = 123456 } }, 13, 13000)
                , JsonRequestBehavior.AllowGet);
        }
    }
}