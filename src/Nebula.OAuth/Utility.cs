using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.OAuth {
    public class Utility {

        public enum ResponseType {
            Code,
            Token,
            Unknown
        }

        public enum Scope {
            OpenId,
            Unknown
        }

        public static ResponseType ConvertStringToResponseType(string responseType) {

            responseType = (responseType ?? "").Trim().ToLower();

            switch (responseType) {
                case "code":
                    return ResponseType.Code;
                case "token":
                    return ResponseType.Token;
                default:
                    return ResponseType.Unknown;
            }            
        }

        public static Scope ConvertStringToScope(string scope) {

            scope = (scope ?? "").Trim().ToLower();

            switch (scope) {
                case "openid":
                    return Scope.OpenId;
                default:
                    return Scope.Unknown;
            }   
        }

        public static Business.CodeResponse CreateCodeResponse(int seconds_to_expire) {
            double expires = (DateTime.UtcNow.AddSeconds(seconds_to_expire) - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            return new Business.CodeResponse() { code = "test_code", expires = expires };
        }
    }
}
