using GeoJSON.Net.Geometry;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoJSON.Net.Converters
{
    public class MultiPositionConverter : PositionConverter
    {
        public override object ParseCoordinates(JArray coordinates)
        {
            var result = new List<List<IGeographicPosition>>();
            var positionListConverter = new PositionsListConverter();
            foreach (var item in coordinates)
            {
                var subcoordinates = positionListConverter.ParseCoordinates(item as JArray);
                if (!(subcoordinates is List<IGeographicPosition>))
                    throw new Exceptions.ParsingException();
                result.Add(subcoordinates as List<IGeographicPosition>);
            }
            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetType().IsAssignableFrom(typeof(IEnumerable<IEnumerable<IGeographicPosition>>));
        }
    }
}
