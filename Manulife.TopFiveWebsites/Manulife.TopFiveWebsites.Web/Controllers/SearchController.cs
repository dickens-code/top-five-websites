using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manulife.TopFiveWebsites.Web.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult ListTopFiveWebsites()
        {
            return View();
        }
    }
}