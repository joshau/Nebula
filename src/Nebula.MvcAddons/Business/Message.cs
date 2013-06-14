using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.MvcAddons.Business {
    public class Message {

        public enum MessageType {
            Success,
            Warn,
            Error,
            Info
        }

        public string Text { get; set; }
        public MessageType Type { get; set; }

        public string CssClass {
            get {
                switch (this.Type) {
                    case MessageType.Success:
                        return "alert-success";
                    case MessageType.Warn:
                        return "alert-block";
                    case MessageType.Error:
                        return "alert-error";
                    case MessageType.Info:
                        return "alert-info";
                    default:
                        return "alert-info";
                }
            }
        }
    }
}
