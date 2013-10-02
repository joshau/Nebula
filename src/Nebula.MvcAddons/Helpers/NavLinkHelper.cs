using System.Web;
using System.Web.Mvc;

namespace Nebula.MvcAddons.Helpers {
    public static partial class NavLinkHelper {

        public static IHtmlString NavLink(this HtmlHelper helper, string action, string controller, string text, bool matchControllerOnly = false) {

            string currentAction = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            string currentController = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();

            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            return MvcHtmlString.Create(string.Format("<a class=\"{0}\" href=\"{1}\">{2}</a>",
                (matchControllerOnly || action.ToLower() == currentAction.ToLower()) && controller.ToLower() == currentController.ToLower() ? "active" : "",
                url.Action(action, controller),
                text));
        }

        public static IHtmlString NavLinkListItem(this HtmlHelper helper, string action, string controller, string text, bool matchControllerOnly = false) {

            string currentArea = helper.ViewContext.RouteData.DataTokens["area"] == null ? "" : helper.ViewContext.RouteData.DataTokens["area"].ToString();
            string currentAction = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            string currentController = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();

            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            return MvcHtmlString.Create(string.Format("<li class=\"{0}\"><a href=\"{1}\">{2}</a></li>",
                (matchControllerOnly || action.ToLower() == currentAction.ToLower()) && controller.ToLower() == currentController.ToLower() ? "active" : "",
                url.Action(action, controller),
                text));
        }   
    }
}
