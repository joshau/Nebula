using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.State.Interfaces {
    public interface IState {

        void Abandon();
        T GetObject<T>(string key);
        void SetObject(string key, object value);
        void SetObject(string key, object value, DateTime expirationDate);
        bool ObjectExists(string key);
        void RemoveObject(string key);   
    }
}
