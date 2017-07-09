using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manulife.TopFiveWebsites.Web.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult RaiseError()
        {
            throw new ApplicationException($"Raise unhandled error at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}