using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.OAuth.Business {
    public class CodeResponse {

        public string code { get; set; }
        public double expires { get; set; }

        public CodeResponse() { }
    }
}
