using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Nebula {
    public class Utility {

        public static string GetWebResponse(string url) {

            string text = null;
            WebRequest request = WebRequest.Create(url);
            WebResponse response = null;

            try {
                response = request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
                    text = reader.ReadToEnd();
                }
            }
            catch (Exception ex) {
                throw new Exception(string.Format("Error fetching data at {0}.", url), ex);
            }
            finally {
                if (response != null)
                    response.Close();
            }
            
            return text;
        }

        public void GetWebResponseAsync(string url, EventHandler<DownloadStringCompletedEventArgs> callback) {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(callback);
            client.DownloadStringAsync(new Uri(url));
        }        
    }
}
