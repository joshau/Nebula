using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Nebula.Geocoding.Abstract {
  public abstract class Geocoding {
    protected T Deserialize<T>(string json) {
      var instance = Activator.CreateInstance<T>();
      using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json))) {
        var serializer = new DataContractJsonSerializer(instance.GetType());
        return (T)serializer.ReadObject(ms);
      }
    }
  }
}
