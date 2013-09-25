using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Nebula.Facebook;

namespace Nebula.Facebook.Tests {
    [TestFixture]
    public class FacebookTabTests {

        private const long APPLICATION_ID = 0;
        private const string APPLICATION_SECRET = "";
        private const string SIGNED_REQUEST = "";

        private Business.SignedRequest signed_request;
        private FacebookTab facebook_tab;

        [Test]
        public void SignedRequest_IsValid() {

            this.signed_request = Business.SignedRequest.Create(SIGNED_REQUEST, APPLICATION_SECRET);
            this.facebook_tab = new FacebookTab(this.signed_request);

            Assert.IsTrue(this.facebook_tab.IsAccessible());
        }
    }
}
