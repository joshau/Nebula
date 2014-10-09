using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.BazaarVoice.Business {
    public class Response {

        public List<Exceptions.BazaarVoiceException> Errors { get; set; }
        public List<Exceptions.BazaarVoiceFieldException> FormErrors { get; set; }

        public string ID { get; set; }
        public string SubmissionID { get; set; }
        public bool? IsRecommended { get; set; }
        public string Title { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime SubmissionTime { get; set; }

        public Response() {
            this.Errors = new List<Exceptions.BazaarVoiceException>();
            this.FormErrors = new List<Exceptions.BazaarVoiceFieldException>();
        }
    }
}
