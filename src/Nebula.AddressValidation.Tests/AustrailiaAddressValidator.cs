using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nebula.AddressValidation;
using NUnit.Framework;

namespace Nebula.AddressValidation.Tests {
  [TestFixture]
  public class AustrailiaAddressValidator {
    Interface.IValidationList list;
    AddressValidation.Interface.IAddressValidator validator;
    
    [SetUp]
    public void SetUp() {
      this.list = new ValidationListFile(@"..\..\var\AustrailiaAddressValidation.csv");
      this.validator = new AddressValidation.AustrailiaAddressValidator(this.list);
    }

    #region Sanitize

    #region LongRegex

    [Test]
    public void Sanitize_LongRegex_Period() {
      string address_dirty = "Unit 1, 23 Somewhere st., Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("unit 1, 23 somewhere st, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_LongRegex_Padding_NoComma() {
      string address_dirty = "  Unit    1   23   Somewhere, Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("unit 1, 23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_LongRegex_Padding_Comma() {
      string address_dirty = "  Unit    1,   23   Somewhere, Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("unit 1, 23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_LongRegex_NoPadding_NoComma() {
      string address_dirty = "Unit 1 23 Somewhere, Someplace 2000";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("unit 1, 23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_LongRegex_NoPadding_Comma() {
      string address_dirty = "Unit 1,23 Somewhere, Someplace 2000";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("unit 1, 23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_LongRegex_NoPadding_CommaPadding() {
      string address_dirty = "Unit 1,  23 Somewhere, Someplace 2000";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("unit 1, 23 somewhere, someplace 2000", address_clean);
    }

    #endregion

    #region MidRegex

    [Test]
    public void Sanitize_MidRegex_Period() {
      string address_dirty = "U1, 23 Somewhere st., Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("u1, 23 somewhere st, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_MidRegex_Padding_NoComma() {
      string address_dirty = "  U    1   23   Somewhere, Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("u1, 23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_MidRegex_Padding_Comma() {
      string address_dirty = "  U    1,   23   Somewhere, Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("u1, 23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_MidRegex_NoPadding_NoComma() {
      string address_dirty = "U1 23 Somewhere, Someplace 2000";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("u1, 23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_MidRegex_NoPadding_Comma() {
      string address_dirty = "U1,23 Somewhere, Someplace 2000";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("u1, 23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_MidRegex_NoPadding_CommaPadding() {
      string address_dirty = "U1,  23 Somewhere, Someplace 2000";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("u1, 23 somewhere, someplace 2000", address_clean);
    }

    #endregion

    #region ShortRegex

    [Test]
    public void Sanitize_ShortRegex_Period() {
      string address_dirty = "1 / 23 Somewhere st., Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("1/23 somewhere st, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_ShortRegex_Padding_NoSlash() {
      string address_dirty = "  1   23   Somewhere, Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("1/23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_ShortRegex_Padding_Slash() {
      string address_dirty = "      1  /   23   Somewhere, Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("1/23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_ShortRegex_NoPadding_NoSlash() {
      string address_dirty = "1 23 Somewhere, Someplace 2000";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("1/23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_ShortRegex_NoPadding_Slash() {
      string address_dirty = "1/23 Somewhere, Someplace 2000";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("1/23 somewhere, someplace 2000", address_clean);
    }

    [Test]
    public void Sanitize_ShortRegex_NoPadding_SlashPadding() {
      string address_dirty = "1  /  23 Somewhere, Someplace 2000";
      string address_clean = this.validator.Sanitize(address_dirty);

      Assert.AreEqual("1/23 somewhere, someplace 2000", address_clean);
    }

    #endregion

    #endregion

    #region Matches

    [Test]
    public void Matches_ProvidedExample() {
      string address_dirty = "Unit 1, 36 Clarence Street, Sydney 2000";
      string address_clean = this.validator.Sanitize(address_dirty);

      string[] matches = this.validator.GetMatches(address_clean);

      Assert.Contains("unit 1, 36 clarence st, sydney 2000", matches);
    }

    [Test]
    public void Matches_MultipleKeyValues() {
      string address_dirty = "Unit 1, 36 Clarence Ct, Sydney 2000";
      string address_clean = this.validator.Sanitize(address_dirty);

      string[] matches = this.validator.GetMatches(address_clean);

      Assert.Contains("unit 1, 36 clarence ct, sydney 2000", matches);
      Assert.Contains("unit 1, 36 clarence court, sydney 2000", matches);
      Assert.Contains("unit 1, 36 clarence courts, sydney 2000", matches);

      Assert.Contains("u1, 36 clarence ct, sydney 2000", matches);
      Assert.Contains("u1, 36 clarence court, sydney 2000", matches);
      Assert.Contains("u1, 36 clarence courts, sydney 2000", matches);

      Assert.Contains("1/36 clarence ct, sydney 2000", matches);
      Assert.Contains("1/36 clarence court, sydney 2000", matches);
      Assert.Contains("1/36 clarence courts, sydney 2000", matches);
    }

    #region LongRegex

    [Test]
    public void Matches_Unit_LongRegex() {
      string address_dirty = "Unit 1, 23 Somewhere st., Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      string[] matches = this.validator.GetMatches(address_clean);

      Assert.Contains("unit 1, 23 somewhere st, someplace 2000", matches, "Missing Long");
      Assert.Contains("u1, 23 somewhere st, someplace 2000", matches, "Missing Mid");
      Assert.Contains("1/23 somewhere st, someplace 2000", matches, "Missing Short");
      Assert.Contains("unit 1, 23 somewhere street, someplace 2000", matches, "Missing Long - Replaced");
      Assert.Contains("u1, 23 somewhere street, someplace 2000", matches, "Missing Mid - Replaced");
      Assert.Contains("1/23 somewhere street, someplace 2000", matches, "Missing Short - Replaced");
    }

    #endregion

    #region MidRegex

    [Test]
    public void Matches_Unit_MidRegex() {
      string address_dirty = "U1, 23 Somewhere st., Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      string[] matches = this.validator.GetMatches(address_clean);

      Assert.Contains("unit 1, 23 somewhere st, someplace 2000", matches, "Missing Long");
      Assert.Contains("u1, 23 somewhere st, someplace 2000", matches, "Missing Mid");
      Assert.Contains("1/23 somewhere st, someplace 2000", matches, "Missing Short");
      Assert.Contains("unit 1, 23 somewhere street, someplace 2000", matches, "Missing Long - Replaced");
      Assert.Contains("u1, 23 somewhere street, someplace 2000", matches, "Missing Mid - Replaced");
      Assert.Contains("1/23 somewhere street, someplace 2000", matches, "Missing Short - Replaced");
    }

    #endregion

    #region ShortRegex

    [Test]
    public void Matches_Unit_ShortRegex() {
      string address_dirty = "1 / 23 Somewhere st., Someplace 2000   ";
      string address_clean = this.validator.Sanitize(address_dirty);

      string[] matches = this.validator.GetMatches(address_clean);

      Assert.Contains("unit 1, 23 somewhere st, someplace 2000", matches, "Missing Long");
      Assert.Contains("u1, 23 somewhere st, someplace 2000", matches, "Missing Mid");
      Assert.Contains("1/23 somewhere st, someplace 2000", matches, "Missing Short");
      Assert.Contains("unit 1, 23 somewhere street, someplace 2000", matches, "Missing Long - Replaced");
      Assert.Contains("u1, 23 somewhere street, someplace 2000", matches, "Missing Mid - Replaced");
      Assert.Contains("1/23 somewhere street, someplace 2000", matches, "Missing Short - Replaced");
    }

    #endregion

    #endregion
  }
}
