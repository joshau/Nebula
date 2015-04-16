using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nebula.Geocoding;
using NUnit.Framework;

namespace Nebula.Geocoding.Tests
{
  [TestFixture]
  public class GeocodingGoogleTests
  {
    private const string GOOGLE_API_KEY = "";

    private Interface.IGeocodingProvider geocoding;
    private Interface.IGeometry geometry;

    [SetUp]
    public void SetUp() {
      this.geocoding = new Providers.Google(GOOGLE_API_KEY);
    }

    [Test]
    public void AddressSearch_GeometryValid() {
      this.geometry = this.geocoding.Geocode("kalamazoo, michigan");

      Assert.AreNotEqual(0.0d, this.geometry.latitude);
      Assert.AreNotEqual(0.0d, this.geometry.longitude);
    }
  }
}
