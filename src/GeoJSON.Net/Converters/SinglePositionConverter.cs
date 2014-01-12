using GeoJSON.Net.Exceptions;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoJSON.Net.Converters
{
    public class SinglePositionConverter : PositionConverter
    {
        public override object ParseCoordinates(JArray coordinates)
        {
            if (coordinates.Count != 2 || coordinates.Any(c => c.GetType() == typeof(JArray)))
                throw new ParsingException(
                     string.Format(
                         "Point geometry coordinates could not be parsed. Expected something like '[-122.428938,37.766713]' ([lon,lat]), what we received however was: {0}",
                         coordinates));
            return ParseCoordinatesPair(coordinates);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(IGeographicPosition));
        }
    }
}
