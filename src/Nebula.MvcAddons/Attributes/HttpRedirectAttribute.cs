using System.Web;
using System.Web.Mvc;

namespace MvcAddons.Attributes {

    public class HttpRedirectAttribute : ActionFilterAttribute {

        int statusCode;
        string url;

        public HttpRedirectAttribute(int statusCode, string url) {
            this.statusCode = statusCode;
            this.url = url;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext) {

            HttpResponseBase response = filterContext.Controller.ControllerContext.HttpContext.Response;

            response.StatusCode = this.statusCode;
            response.RedirectLocation = this.url;
            response.End();

            base.OnResultExecuted(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {

            filterContext.Result = null;
            base.OnActionExecuted(filterContext);
        }
    }
}
