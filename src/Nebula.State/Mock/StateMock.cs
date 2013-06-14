using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.State.Mock {

    public class StateMock : Interfaces.IState {

        private Dictionary<string, object> _fakeState;

        public StateMock() {
            if (this._fakeState == null) this._fakeState = new Dictionary<string, object>();
        }

        public void Abandon() {
            this._fakeState.Clear();
        }

        public T GetObject<T>(string key) {
            return this.ObjectExists(key) ? (T)this._fakeState[key] : default(T);
        }

        public void SetObject(string key, object value) {
            if (this._fakeState.ContainsKey(key)) {
                if (value == null) {
                    this.RemoveObject(key);
                }
                else {
                    this._fakeState[key] = value;
                }
            }
            else if (value != null) {
                this._fakeState.Add(key, value);
            }
        }

        public void SetObject(string key, object value, DateTime expirationDate) {
            this.SetObject(key, value);
        }

        public bool ObjectExists(string key) {
            return this._fakeState.ContainsKey(key);
        }

        public void RemoveObject(string key) {
            this._fakeState.Remove(key);
        }
    }
}