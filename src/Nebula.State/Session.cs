using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace State {

    public class Session : Interfaces.IState {
    
        public void Abandon() {
            HttpContext.Current.Session.Abandon();
        }

        public T GetObject<T>(string key) {
            return this.ObjectExists(key) ? (T)HttpContext.Current.Session[key] : default(T);
        }

        public void SetObject(string key, object value) {
            HttpContext.Current.Session.Add(key, value);
        }

        public void SetObject(string key, object value, DateTime expirationDate) {
            throw new NotImplementedException();
        }

        public bool ObjectExists(string key) {
            return HttpContext.Current.Session[key] != null;
        }

        public void RemoveObject(string key) {
            HttpContext.Current.Session.Remove(key);
        }
    }
}
