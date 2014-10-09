using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nebula.BazaarVoice.Business;
using Nebula.BazaarVoice.Interface;

namespace Nebula.BazaarVoice {
    public class BazaarVoiceManager {

        private IBazaarVoice _bazaarVoice;

        public BazaarVoiceManager(IBazaarVoice bazaarVoice) {
            this._bazaarVoice = bazaarVoice;
        }

        public Response ReviewPreview(Review review, string hostname, string apiVersion, string passKey) {
            return this._bazaarVoice.ReviewPreview(review, hostname, apiVersion, passKey);
        }

        public Response ReviewSubmit(Review review, string hostname, string apiVersion, string passKey) {
            return this._bazaarVoice.ReviewSubmit(review, hostname, apiVersion, passKey);
        }

        public string GetProductJson(int count, int offset, string hostname, string apiVersion, string passkey) {
            return this._bazaarVoice.GetProductJson(count, offset, hostname, apiVersion, passkey);
        }

        public string GetProductJson(string productID, string hostname, string apiVersion, string passkey) {
            return this._bazaarVoice.GetProductJson(productID, hostname, apiVersion, passkey);
        }

        public Product GetProduct(string productID, string hostname, string apiVersion, string passkey) {
            return this._bazaarVoice.GetProduct(productID, hostname, apiVersion, passkey);
        }

        public string GetReviewJson(int id, string hostname, string apiVersion, string passkey) {
            return this._bazaarVoice.GetReviewJson(id, hostname, apiVersion, passkey);
        }

        public string GetReviewJson(int count, int offset, int sort, string hostname, string apiVersion, string passkey) {
            return this._bazaarVoice.GetReviewJson(count, offset, sort, hostname, apiVersion, passkey);
        }

        public string GetReviewJson(string productID, int count, int offset, int sort, string hostname, string apiVersion, string passkey) {
            return this._bazaarVoice.GetReviewJson(productID, count, offset, sort, hostname, apiVersion, passkey);
        }
    }
}
