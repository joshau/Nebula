using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Helpers;

namespace Nebula.BazaarVoice.Business {
    public class BazaarVoice : Interface.IBazaarVoice {

        public const string API_POST_REVIEW = "http://{0}/data/submitreview.{1}";
        public const string API_POST_REVIEW_DATA = "Action={0}&ApiVersion={1}&ProductId={2}&Rating={3}&ReviewText={4}&Title={5}&UserNickname={6}&UserEmail={7}&UserId={8}&PassKey={9}";

        public const string API_GET_PRODUCT = "http://{0}/data/products.{1}?apiversion={2}&stats=reviews&passkey={3}&sort=Id:asc&stats=reviews";
        public const string API_GET_REVIEW = "http://{0}/data/reviews.{1}?apiversion={2}&stats=reviews&limit={3}&offset={4}&passkey={5}";

        #region IBazaarVoice Members

        public Response ReviewPreview(Review review, string hostname, string apiVersion, string passkey) {

            Response response = new Response();

            string url = string.Format(API_POST_REVIEW, hostname, "json");
            string data = string.Format(API_POST_REVIEW_DATA, "Preview", apiVersion, review.ProductID, review.Rating.ToString(), review.Text, review.Title, review.NickName, review.UserEmail, review.UserID, passkey);

            return this._ParseResponse(Json.Decode(this._GetRequestResponse(url, true, data)));
        }

        public Response ReviewSubmit(Review review, string hostname, string apiVersion, string passkey) {

            string url = string.Format(API_POST_REVIEW, hostname, "json");
            string data = string.Format(API_POST_REVIEW_DATA, "submit", apiVersion, review.ProductID, review.Rating.ToString(), review.Text, review.Title, review.NickName, review.UserEmail, review.UserID, passkey);

            return this._ParseResponse(Json.Decode(this._GetRequestResponse(url, true, data)));
        }

        public string GetProductJson(int count, int offset, string hostname, string apiVersion, string passkey) {

            string url = string.Format(API_GET_PRODUCT, hostname, "json", apiVersion, passkey);

            url += string.Format("&limit={0}&offset={1}", count, offset);

            return _GetRequestResponse(url);
        }

        public string GetProductJson(string productID, string hostname, string apiVersion, string passkey) {

            return _GetProductJson(productID, hostname, apiVersion, passkey);
        }

        public Product GetProduct(string productID, string hostname, string apiVersion, string passkey) {

            Product product = new Product();
            dynamic obj = Json.Decode(_GetProductJson(productID, hostname, apiVersion, passkey));

            if (obj != null && obj.Results != null && obj.Results.Length > 0) {

                product.Id = obj.Results[0].Id;
                product.Name = obj.Results[0].Name;
                product.Description = obj.Results[0].Description;
                product.Url = obj.Results[0].ProductPageUrl;
                product.ImageUrl = obj.Results[0].ImageUrl;
                product.CategoryId = obj.Results[0].CategoryId;
                product.AverageOverallRating = obj.Results[0].ReviewStatistics.AverageOverallRating == null ? 0D : (double)obj.Results[0].ReviewStatistics.AverageOverallRating;
            }

            return product;
        }

        public string GetReviewJson(int id, string hostname, string apiVersion, string passkey) {

            //http://stg.api.bazaarvoice.com/data/reviews.json?apiversion=5.4&passkey=kuy3zj9pr3n7i0wxajrzj04xo&filter=id:192612
            string url = _GetReviewJsonUrl(1, 0, 0, hostname, apiVersion, passkey);
            url += string.Format("&filter=id:{0}", id);

            return _GetRequestResponse(url);
        }

        public string GetReviewJson(int count, int offset, int sort, string hostname, string apiVersion, string passkey) {

            string url = _GetReviewJsonUrl(count, offset, sort, hostname, apiVersion, passkey);

            return _GetRequestResponse(url);
        }

        public string GetReviewJson(string productID, int count, int offset, int sort, string hostname, string apiVersion, string passkey) {

            string url = _GetReviewJsonUrl(count, offset, sort, hostname, apiVersion, passkey);
            url += string.Format("&filter=ProductId:{0}", productID);

            return _GetRequestResponse(url);
        }

        #endregion

        private string _GetRequestResponse(string url, bool post = false, string data = null) {

            string jsonResponse;

            WebRequest request = this._BuildWebRequest(url, post, data);
            WebResponse response;
            StreamReader reader;

            try {

                response = request.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                jsonResponse = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex) {
                throw;
            }

            return jsonResponse;
        }

        private WebRequest _BuildWebRequest(string url, bool post = false, string data = null) {

            if (post && string.IsNullOrEmpty(data))
                throw new Exception("Must provide data to POST.");

            WebRequest request = WebRequest.Create(url);

            request.Method = post ? "POST" : "GET";

            if (post) {

                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(data);

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = buffer.Length;

                Stream postStream = request.GetRequestStream();
                postStream.Write(buffer, 0, buffer.Length);
                postStream.Close();
            }

            return request;
        }

        private Response _ParseResponse(dynamic d) {

            Response response = new Response();

            if (d.HasErrors) {

                if (d.Errors != null) {
                    foreach (dynamic e in d.Errors) {
                        response.Errors.Add(new Exceptions.BazaarVoiceException(e.Message, e.Code));
                    }
                }

                if (d.FormErrors.FieldErrors != null) {
                    foreach (KeyValuePair<string, dynamic> e in d.FormErrors.FieldErrors) {
                        response.FormErrors.Add(new Exceptions.BazaarVoiceFieldException(e.Value.Message, e.Value.Code, e.Value.Field));
                    }
                }
            }

            if (d.Review != null) {

                response.ID = d.Review.Id;
                response.SubmissionID = d.SubmissionId;
                response.IsRecommended = d.Review.IsRecommended;
                response.Title = d.Review.Title;
                response.ReviewText = d.Review.ReviewText;
                response.Rating = d.Review.Rating;
                response.SubmissionTime = DateTime.Parse(d.Review.SubmissionTime);
            }

            return response;
        }

        private string _GetProductJson(string productID, string hostname, string apiVersion, string passkey) {

            string url = string.Format(API_GET_PRODUCT, hostname, "json", apiVersion, passkey);
            url += string.Format("&filter=id:{0}", productID);

            return _GetRequestResponse(url);
        }

        private string _GetReviewJsonUrl(int count, int offset, int sort, string hostname, string apiVersion, string passkey) {

            string url = string.Format(API_GET_REVIEW, hostname, "json", apiVersion, count, offset, passkey);
            url += "&Stats=Reviews&Include=Products";

            if (sort == 1 || sort == 2) {
                url += string.Format("&Sort=Rating:{0},SubmissionTime:desc", sort == 1 ? "desc" : "asc");
            }

            return url;
        }
    }
}
