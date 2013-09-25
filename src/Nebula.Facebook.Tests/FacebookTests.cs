using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Nebula.Facebook;

namespace Nebula.Facebook.Tests {
    [TestFixture]
    public class FacebookTests {

        private const long APPLICATION_ID = 0;
        private const string APPLICATION_SECRET = "";

        private Facebook facebook;

        [Test]
        public void ValidAccessToken() {

            this.facebook = new Facebook(APPLICATION_ID, APPLICATION_SECRET);
            string access_token = facebook.GetAccessToken();

            Assert.IsNotNull(access_token);
        }
    }
}
