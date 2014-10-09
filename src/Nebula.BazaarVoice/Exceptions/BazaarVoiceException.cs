using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.BazaarVoice.Exceptions {
    public class BazaarVoiceException : Exception {

        public string Code { get; set; }

        public BazaarVoiceException(string message, string code)
            : base(message) {
            this.Code = code;
        }
    }
}
