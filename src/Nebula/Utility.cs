using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nebula {
    public class Utility {

      public enum Method {
        GET,
        POST
      }

        public static string GetWebResponse(string url) {
          return GetWebResponse(url, new WebHeaderCollection());
        }

        public static string GetWebResponse(string url, WebHeaderCollection headers) {

          string text;

          using (WebClient client = new WebClient()) {
            client.Headers = headers;
            text = client.DownloadString(url);
          }

          return text;
        }

        public static void GetWebResponseAsync(string url, EventHandler<DownloadStringCompletedEventArgs> callback) {
          GetWebResponseAsync(url, new WebHeaderCollection(), callback);
        }

        public static void GetWebResponseAsync(string url, WebHeaderCollection headers, EventHandler<DownloadStringCompletedEventArgs> callback) {
          using (WebClient client = new WebClient()) {
            client.Headers = headers;
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(callback);
            client.DownloadStringAsync(new Uri(url));
          }
        } 

        public static string GetWebResponsePost(string url, NameValueCollection content) {
          return GetWebResponsePost(url, new WebHeaderCollection(), content);
        }

        public static string GetWebResponsePost(string url, WebHeaderCollection headers, NameValueCollection content) {
          return GetWebResponsePost(url, "POST", headers, content);
        }

        public static string GetWebResponsePost(string url, string method, WebHeaderCollection headers, NameValueCollection content) {
          return GetWebResponsePost(url, "POST", headers, content, null);
        }

        public static string GetWebResponsePost(string url, string method, WebHeaderCollection headers, NameValueCollection content, ICredentials credentials) {

          string text;

          using(WebClient client = new WebClient()) {
            client.Headers = headers;

            if(credentials != null)
              client.Credentials = credentials;

            byte[] response = client.UploadValues(url, method, content);

            using (StreamReader reader = new StreamReader(new MemoryStream(response))) {
              text = reader.ReadToEnd();
            }
          }

          return text;
        }

        public static void GetWebResponsePostAsync(string url, NameValueCollection content, EventHandler<UploadValuesCompletedEventArgs> callback) {
          GetWebResponsePostAsync(url, "POST", new WebHeaderCollection(), content, callback);
        }

        public static void GetWebResponsePostAsync(string url, WebHeaderCollection headers, NameValueCollection content, EventHandler<UploadValuesCompletedEventArgs> callback) {
          GetWebResponsePostAsync(url, "POST", headers, content, callback);
        }

        public static void GetWebResponsePostAsync(string url, string method, WebHeaderCollection headers, NameValueCollection content, EventHandler<UploadValuesCompletedEventArgs> callback) {
          using (WebClient client = new WebClient()) {
            client.Headers = headers;
            client.UploadValuesCompleted += new UploadValuesCompletedEventHandler(callback);
            client.UploadValuesAsync(new Uri(url), method, content);
          }
        }

        public static void GetWebResponsePostAsync(string url, string method, WebHeaderCollection headers, NameValueCollection content, ICredentials credentials, EventHandler<UploadValuesCompletedEventArgs> callback) {
          using (WebClient client = new WebClient()) {
            client.Headers = headers;
            client.Credentials = credentials;
            client.UploadValuesCompleted += new UploadValuesCompletedEventHandler(callback);
            client.UploadValuesAsync(new Uri(url), method, content);
          }
        }
    }
}
