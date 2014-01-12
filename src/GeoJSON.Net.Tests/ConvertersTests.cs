using GeoJSON.Net.Converters;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Extensions;

namespace GeoJSON.Net.Tests
{
    public class ConvertersTests
    {
        public ConvertersTests()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }

        [Fact]
        public void PlainArrayOfCoordinatesShouldBeParsedIntoGeographicPosition()
        {
            // Arrange
            var coordinates = JArray.Parse("[102.0, 0.5]");
            var sut = new SinglePositionConverter();
            // Act
            var result = sut.ParseCoordinates(coordinates);
            // Assert
            Assert.IsAssignableFrom<IGeographicPosition>(result);
        }

        [Fact]
        public void ArrayOfArraysOfCoordinatesShouldBeParsedIntoListOfGeographicPositions()
        {
            // Arrange
            var coordinates = JArray.Parse("[ [102.0, 0.0], [103.0, 1.0], [104.0, 0.0], [105.0, 1.0] ]");
            var sut = new PositionsListConverter();
            // Act
            var result = sut.ParseCoordinates(coordinates);
            // Assert
            Assert.IsType<List<IGeographicPosition>>(result);
        }

        [Fact]
        public void MultiArrayOfCoordinatesShouldBeParsedIntoListOfListsOfGeographicPositions()
        {
            // Arrange
            var coordinates = JArray.Parse("[ [ [100.0, 0.0], [101.0, 0.0], [101.0, 1.0], [100.0, 1.0], [100.0, 0.0] ] ]");
            var sut = new MultiPositionConverter();
            // Act
            var result = sut.ParseCoordinates(coordinates);
            // Assert
            Assert.IsType<List<List<IGeographicPosition>>>(result);
        }
    }
}
