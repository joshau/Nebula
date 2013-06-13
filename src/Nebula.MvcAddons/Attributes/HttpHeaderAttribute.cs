using System.Web;
using System.Web.Mvc;

namespace MvcAddons.Attributes {

    public class HttpHeaderAttribute : ActionFilterAttribute {
    
        string name;
        string value;

        public HttpHeaderAttribute(string name, string value) {
            this.name = name;
            this.value = value;
        }

        public override void  OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.AppendHeader(this.name, this.value);
 	         base.OnResultExecuted(filterContext);
        }
    }
}
