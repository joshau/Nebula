using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace State {

    public class Cache : Interfaces.IState {
        
        public void Abandon() {
            throw new NotImplementedException();
        }

        public T GetObject<T>(string key) {
            return this.ObjectExists(key) ? (T)HttpContext.Current.Cache[key] : default(T);
        }

        public void SetObject(string key, object value) {
            HttpContext.Current.Cache.Insert(key, value);
        }

        public void SetObject(string key, object value, DateTime expirationDate) {
            HttpContext.Current.Cache.Insert(key, value, null, expirationDate, TimeSpan.Zero);
        }

        public bool ObjectExists(string key) {
            return HttpContext.Current.Cache[key] != null;
        }

        public void RemoveObject(string key) {
            HttpContext.Current.Cache.Remove(key);
        }
    }
}
