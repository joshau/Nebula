using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nebula.Cryptography {

  public class PBKDF2 : Interface.ICryptography {

    struct Salt {
      public int Iterations;
      public string Text;
    }

    public PBKDF2() { }

    public string GenerateSalt(int saltSize, int hashIterations = 10000) {

      if (saltSize < 1)
        throw new Exceptions.InvalidSaltException("Invalid Salt Size. Example: 16");

      string salt;
      RandomNumberGenerator rand = RandomNumberGenerator.Create();
      byte[] rand_bytes = new byte[saltSize];

      rand.GetBytes(rand_bytes);
      salt = string.Format("{0}.{1}", hashIterations, Convert.ToBase64String(rand_bytes));

      return salt;
    }

    public string CreateHash(string text, string salt) {

      Salt salt_struct = this.ParseSaltString(salt);

      byte[] salt_bytes = Encoding.UTF8.GetBytes(salt_struct.Text);
      Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(text, salt_bytes, salt_struct.Iterations);
      byte[] key = pbkdf2.GetBytes(64);

      return Convert.ToBase64String(key);
    }

    private Salt ParseSaltString(string salt) {

      Salt salt_struct = new Salt();

      try {
        string[] salt_split = salt.Split(new char[] { '.' }, 2);
        
        salt_struct.Iterations = Int32.Parse(salt_split[0]);
        salt_struct.Text = salt_split[1];
      }
      catch (Exception ex){
        throw new Exceptions.InvalidSaltException(ex.Message);
      }      

      return salt_struct;
    }
  }
}
