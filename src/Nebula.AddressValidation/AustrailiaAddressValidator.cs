using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nebula.AddressValidation {
  public class AustrailiaAddressValidator : Interface.IAddressValidator {
    const string REGEX_UNIT_LONG = @"(unit\s*)(\d+),?\s*(\d+)(.*)";
    const string REGEX_UNIT_MID = @"(u\s*)(\d+),?\s*(\d+)(.*)";
    const string REGEX_UNIT_SHORT = @"(\d+)(\s*/?\s*)(\d+)(.*)";

    const string FORMAT_UNIT_LONG = "unit {0}, {1} {2}";
    const string FORMAT_UNIT_MID = "u{0}, {1} {2}";
    const string FORMAT_UNIT_SHORT = "{0}/{1} {2}";

    Dictionary<string, List<string>> list;

    public AustrailiaAddressValidator(Interface.IValidationList validation_list) {
      this.list = validation_list.GetValidationList();
    }

    public string Sanitize(string address_concat) {
      Regex regex = new Regex(REGEX_UNIT_LONG);
      Match match;

      address_concat = address_concat.ToLower().Trim();
      match = regex.Match(address_concat);

      if (match.Success) {
        address_concat = string.Format(FORMAT_UNIT_LONG, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value.Trim());
      }
      else {
        regex = new Regex(REGEX_UNIT_MID);
        match = regex.Match(address_concat);

        if (match.Success) {
          address_concat = string.Format(FORMAT_UNIT_MID, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value.Trim());
        }
        else {
          regex = new Regex(REGEX_UNIT_SHORT);
          match = regex.Match(address_concat);

          if (match.Success) {
            address_concat = string.Format(FORMAT_UNIT_SHORT, match.Groups[1].Value, match.Groups[3].Value, match.Groups[4].Value.Trim());
          }
        }
      }

      address_concat = address_concat.Replace(".", "");

      return address_concat;
    }

    public string[] GetMatches(string address_concat) {
      List<string> matches = new List<string>();

      List<string> matches_street = new List<string>();
      List<string> matches_unit = new List<string>();

      matches_street.AddRange(this.StreetVariations(address_concat));
      foreach (string s in matches_street) {
        matches_unit.AddRange(this.UnitVariations(s));
      }

      matches.AddRange(matches_street);
      matches.AddRange(matches_unit);

      return matches.ToArray();
    }

    private List<string> UnitVariations(string address_concat) {
      List<string> matches = new List<string>();
      Regex regex = new Regex(REGEX_UNIT_LONG);
      Match match;

      address_concat = address_concat.ToLower().Trim();
      match = regex.Match(address_concat);

      if (match.Success) {
        matches.Add(string.Format(FORMAT_UNIT_MID, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value.Trim()));
        matches.Add(string.Format(FORMAT_UNIT_SHORT, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value.Trim()));
      }
      else {
        regex = new Regex(REGEX_UNIT_MID);
        match = regex.Match(address_concat);

        if (match.Success) {
          matches.Add(string.Format(FORMAT_UNIT_LONG, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value.Trim()));
          matches.Add(string.Format(FORMAT_UNIT_SHORT, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value.Trim()));
        }
        else {
          regex = new Regex(REGEX_UNIT_SHORT);
          match = regex.Match(address_concat);

          if (match.Success) {
            matches.Add(string.Format(FORMAT_UNIT_LONG, match.Groups[1].Value, match.Groups[3].Value, match.Groups[4].Value.Trim()));
            matches.Add(string.Format(FORMAT_UNIT_MID, match.Groups[1].Value, match.Groups[3].Value, match.Groups[4].Value.Trim()));
          }
        }
      }

      return matches;
    }

    private List<string> StreetVariations(string address_concat) {
      Regex regex;
      Match match;
      List<string> matches = new List<string>();
      address_concat = address_concat.ToLower();

      matches.Add(address_concat);

      foreach (KeyValuePair<string, List<string>> kvp in this.list) {
        regex = new Regex(string.Format(@"(\W){0}(\W)", kvp.Key.ToLower()));
        match = regex.Match(address_concat);

        if (match.Success) {
          this.list[kvp.Key].ForEach(x => matches.Add(regex.Replace(address_concat, string.Format("{0}{1}{2}", match.Groups[1].Value, x.ToLower(), match.Groups[2].Value))));
        }
      }

      return matches;
    }
  }
}
