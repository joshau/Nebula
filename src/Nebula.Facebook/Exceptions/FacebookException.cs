using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.Facebook.Exceptions {
    public class FacebookException : Exception {
        
        private int code;
        private string type;

        public FacebookException() : base() { }
        public FacebookException(string message) : base(message) {}

        public FacebookException(string message, string type, int code)
            : base(message) {
        }

        public override string Message {
            get {
                if (code > 0 && type != null) {
                    return string.Format("{0} - {1} : {2}", this.code.ToString(), this.type, this.Message);
                }
                else {
                    return base.Message;
                }                
            }
        }
    }
}
