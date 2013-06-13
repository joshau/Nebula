using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using NUnit.Framework;

namespace MvcAddons.Test.Extensions {
    public static class ViewResultExtensions {

        public static void VerifyView(this ViewResult result, string ExpectedView) {

            Assert.IsInstanceOf<ViewResult>(result, "Result was not a view");
            Assert.AreEqual(ExpectedView.ToLower(), result.ViewName.ToLower(), "View was incorrect");
        }
    }
}
