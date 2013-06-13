using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace MvcAddons.Controllers {

    public abstract class BaseController : Controller {

        // Snatched from http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
        protected string RenderPartialViewToString(string viewName, object model) {

            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter()) {
                ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
