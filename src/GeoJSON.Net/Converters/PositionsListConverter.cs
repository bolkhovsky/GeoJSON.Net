using GeoJSON.Net.Geometry;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoJSON.Net.Converters
{
    public class PositionsListConverter : PositionConverter
    {
        public override object ParseCoordinates(JArray coordinates)
        {
            if (coordinates.Count == 2 && coordinates.All(c => c.GetType() != typeof(JArray)))
            {
                return ParseCoordinatesPair(coordinates);
            }
            else
            {
                var positions = new List<IGeographicPosition>();
                foreach (var item in coordinates)
                {
                    var subcoordinates = this.ParseCoordinates(item as JArray);
                    if (subcoordinates is List<IGeographicPosition>)
                    {
                        positions.AddRange(subcoordinates as List<IGeographicPosition>);
                    }
                    else
                    {
                        positions.Add(subcoordinates as IGeographicPosition);
                    }
                }
                return positions;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetType().IsAssignableFrom(typeof(IEnumerable<IGeographicPosition>));
        }
    }
}
