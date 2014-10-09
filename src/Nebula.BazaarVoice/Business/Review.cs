using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.BazaarVoice.Business {
    public class Review {

        public string ProductID { get; set; }
        public string NickName { get; set; }
        public string UserID { get; set; }
        public string UserEmail { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
