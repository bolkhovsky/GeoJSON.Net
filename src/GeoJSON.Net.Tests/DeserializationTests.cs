using GeoJSON.Net.Converters;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace GeoJSON.Net.Tests
{
    public class DeserializationTests
    {
        public DeserializationTests()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }

        [Fact]
        public void ParseGeoJsonShouldReturnFeature()
        {
            // Arrange
            var json = Properties.Resources.SampleFeature;
            // Act
            var geoJsonObject = JsonConvert.DeserializeObject(
                json,
                typeof(Feature.Feature));
            // Assert
            Assert.IsType<Feature.Feature>(geoJsonObject);
        }

        [Fact]
        public void ParseGeoJsonShouldReturnFeatureCollection()
        {
            // Arrange
            var json = Properties.Resources.SampleFeatureCollection;
            // Act
            var geoJsonObject = JsonConvert.DeserializeObject(
                json,
                typeof(FeatureCollection));
            // Assert
            Assert.IsType<FeatureCollection>(geoJsonObject);
        }
    }
}
