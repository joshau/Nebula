using System.Web;
using System.Web.Mvc;

namespace MvcAddons.Helpers {
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
    }
}
