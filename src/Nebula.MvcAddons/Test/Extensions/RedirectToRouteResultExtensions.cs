using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using NUnit.Framework;

namespace MvcAddons.Test.Extensions {
    public static class RedirectToRouteResultExtensions {

        public static void VerifyRedirectToRoute(this RedirectToRouteResult result, string ExpectedAction, string ExpectedController = null) {

            Assert.IsInstanceOf<RedirectToRouteResult>(result, "Result was not a redirect to an action");

            Assert.AreEqual(ExpectedAction.ToLower(), result.RouteValues["action"].ToString().ToLower(), "Action was incorrect");

            if (!string.IsNullOrEmpty(ExpectedController)) {
                Assert.IsNotNull(result.RouteValues["controller"], "Controller name was not specified by the result");
                Assert.AreEqual(ExpectedController.ToLower(), result.RouteValues["controller"].ToString().ToLower(), "Controller was incorrect");
            }            
        }
    }
}
