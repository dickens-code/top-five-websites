using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Manulife.TopFiveWebsites.Service.Interface;

namespace Manulife.TopFiveWebsites.Web.Controllers
{
    [Authorize]
    public class ResetController : ControllerBase
    {
        protected readonly IVisitLogService _visitLogService;

        public ResetController(IVisitLogService visitLogService)
        {
            _visitLogService = visitLogService;
        }

        public ActionResult ResetVisitLog()
        {
            _visitLogService.ResetVisitLog();

            ViewBag.Message = "DONE reset visit log !";

            return View("Completed");
        }

        public ActionResult ResetVisitLogExclusion()
        {
            _visitLogService.ResetVisitLogExclusion();

            ViewBag.Message = "DONE reset visit log exclusion !";

            return View("Completed");
        }

        public ActionResult ResetVisitLogAndExclusion()
        {
            _visitLogService.ResetVisitLog();
            _visitLogService.ResetVisitLogExclusion();

            ViewBag.Message = "DONE reset visit log AND exclusion !";

            return View("Completed");
        }
    }
}