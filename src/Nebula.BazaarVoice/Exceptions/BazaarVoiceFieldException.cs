using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.BazaarVoice.Exceptions {
    public class BazaarVoiceFieldException : Exception {

        public string Code { get; set; }
        public string Field { get; set; }

        public BazaarVoiceFieldException(string message, string code, string field)
            : base(message) {
            this.Code = code;
            this.Field = field;
        }
    }
}
