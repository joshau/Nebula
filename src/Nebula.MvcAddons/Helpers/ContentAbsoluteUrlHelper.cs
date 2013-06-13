using System.Web;
using System.Web.Mvc;

namespace MvcAddons.Helpers {

    public static partial class ContentAbsoluteUrlHelper {

        public static string ContentAbsolute(this UrlHelper helper, string contentPath, bool isSecure = true) {

            string domain = helper.Action("Index", "Home", null, isSecure ? "https" : "http");

            if (string.IsNullOrEmpty(domain)) return contentPath.Replace("~/", "/");

            if (domain.EndsWith("/")) domain = domain.Remove(domain.Length - 1);
            return string.Format("{0}{1}", domain, helper.Content(contentPath));
        }
    }
}
