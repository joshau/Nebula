using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebula.AddressValidation {
  public class ValidationListFile : Interface.IValidationList {
    Dictionary<string, List<string>> list;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="file_location">CSV file location.</param>
    public ValidationListFile(string file_location) {
      list = new Dictionary<string, List<string>>();
      using (StreamReader fs = new StreamReader(file_location)) {
        string line;
        string[] validation_pair;

        while (!fs.EndOfStream) {
          line = fs.ReadLine();
          validation_pair = line.Split(',');

          if (validation_pair.Count() != 2)
            continue;

          this.AddPair(validation_pair[0].ToLower(), validation_pair[1].ToLower());
          this.AddPair(validation_pair[1].ToLower(), validation_pair[0].ToLower());
        }
      }
    }

    private void AddPair(string k, string v) {
      if (this.list.ContainsKey(k)) {
        this.list[k].Add(v);
      }
      else {
        this.list.Add(k, new List<string>() { v });
      }
    }

    public Dictionary<string, List<string>> GetValidationList() {
      return this.list;
    }
  }
}
