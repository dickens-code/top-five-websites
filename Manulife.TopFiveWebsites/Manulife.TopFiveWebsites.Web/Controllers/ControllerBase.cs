using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Manulife.TopFiveWebsites.Web.Utils;
using log4net;

namespace Manulife.TopFiveWebsites.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonDotNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //get exception object
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            //log error
            LogManager.GetLogger(this.GetType()).Error(ex.Message, ex);

            //error details
            var model = new HandleErrorInfo(filterContext.Exception, filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"].ToString());

            //pass to error page
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model), 
            };

        }
    }
}