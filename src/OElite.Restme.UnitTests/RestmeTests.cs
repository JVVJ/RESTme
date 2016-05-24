﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OElite.Restme.UnitTests
{
    public class RestmeTests
    {
        [Fact]
        public void GetTests()
        {
            var rest = new Restme(new Uri("http://freegeoip.net"));
            var result1 = rest.Get<string>("/json/github.com");
            Assert.True(result1.Length > 0 && result1.Contains("region_code"));

            var result2 = rest.Get<GeoResult>("/json/github.com");
            Assert.True(result2?.Latitude != 0);

            rest.Add("q", "github.com");
            var result3 = rest.Get<GeoResult>("/json");
            Assert.True(result3?.Latitude != null);
        }


        private class GeoResult
        {
            public string IP { get; set; }
            public string Country_Code { get; set; }
            public string Country_Name { get; set; }
            public string Region_Code { get; set; }
            public string Region_Name { get; set; }
            public string City { get; set; }
            public string Zip_Code { get; set; }
            public string Time_Zone { get; set; }
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
            public string Metro_Code { get; set; }
        }

    }
}