using System.Linq;
using System.Web.Mvc;

namespace Nebula.MvcAddons.ViewEngines {

    //Snatched from http://coding-in.net/mvc-3-organize-your-partial-views/
    //
    //Register this view engine on Application-Start()
    //ViewEngines.Engines.Add(new RazorCustom());
    public class RazorCustom : RazorViewEngine {

        public RazorCustom() {

            string[] newLocationFormat = new[] {
                "~/Views/{1}/Partial/{0}.cshtml",
                "~/Views/{1}/Partial/{0}.vbhtml"                
            };

            string[] newAreaLocationFormat = new[] {
                "~/Areas/{2}/Views/{1}/Partial/{0}.cshtml",
                "~/Areas/{2}/Views/{1}/Partial/{0}.vbhtml"                
            };

            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
            AreaPartialViewLocationFormats = AreaPartialViewLocationFormats.Union(newAreaLocationFormat).ToArray();
        }
    }
}
