using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nebula.Cryptography;

using NUnit.Framework;

namespace Nebula.Cryptography.Tests {

  [TestFixture]
  public class PBKDF2 {

    Interface.ICryptography pbkdf2;

    [SetUp]
    public void SetUp() {
      this.pbkdf2 = new Nebula.Cryptography.PBKDF2();
    }

    [Test]
    public void Compare() {

      string text_to_hash = "test_word_to_hash";
      string password = "p@ssw0rd.";

      string salt = this.pbkdf2.GenerateSalt(16);
      salt += password;

      string hash = this.pbkdf2.CreateHash(text_to_hash, salt);
      string hash_to_compare = this.pbkdf2.CreateHash("test_word_to_hash", salt);

      Assert.AreEqual(hash, hash_to_compare);
    }
  }
}
