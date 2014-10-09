using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nebula.BazaarVoice.Business;

namespace Nebula.BazaarVoice.Interface {
    public interface IBazaarVoice {
        Response ReviewPreview(Review review, string hostname, string apiVersion, string passkey);
        Response ReviewSubmit(Review review, string hostname, string apiVersion, string passkey);
        string GetProductJson(int count, int offset, string hostname, string apiVersion, string passkey);
        string GetProductJson(string productID, string hostname, string apiVersion, string passkey);
        Product GetProduct(string productID, string hostname, string apiVersion, string passkey);
        string GetReviewJson(int id, string hostname, string apiVersion, string passkey);
        string GetReviewJson(int count, int offset, int sort, string hostname, string apiVersion, string passkey);
        string GetReviewJson(string productID, int count, int offset, int sort, string hostname, string apiVersion, string passkey);
    }
}
