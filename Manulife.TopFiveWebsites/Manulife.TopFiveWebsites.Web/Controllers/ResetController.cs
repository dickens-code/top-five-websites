using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Manulife.TopFiveWebsites.Service;

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
            try
            {
                _visitLogService.ResetVisitLog();

                ViewBag.Message = "DONE reset visit log !";

                return View("Completed");
            }
            catch(Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        public ActionResult ResetVisitLogExclusion()
        {
            try
            {
                _visitLogService.ResetVisitLogExclusion();

                ViewBag.Message = "DONE reset visit log exclusion !";

                return View("Completed");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        public ActionResult ResetVisitLogAndExclusion()
        {
            try
            {
                _visitLogService.ResetVisitLog();
                _visitLogService.ResetVisitLogExclusion();

                ViewBag.Message = "DONE reset visit log AND exclusion !";

                return View("Completed");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }
    }
}