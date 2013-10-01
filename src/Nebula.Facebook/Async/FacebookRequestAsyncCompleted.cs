using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.Facebook.Async {
    
    public delegate void FacebookRequestAsyncCompletedEventHandler (object sender, FacebookRequestAsyncCompletedEventArgs e);

    public class FacebookRequestAsyncCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        public dynamic Result { get; set; }

        internal FacebookRequestAsyncCompletedEventArgs(Exception exception, bool cancelled, object userState, dynamic result) :
            base(exception, cancelled, userState) {
                this.Result = result;
        }
    }
}
