using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoJSON.Net
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Determines whether this LineString is a <see cref="http://geojson.org/geojson-spec.html#linestring">LinearRing</see>.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if it is a linear ring; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsLinearRing(this IEnumerable<IGeographicPosition> coordinates)
        {
            return coordinates.Count() >= 4 && coordinates.IsClosed();
        }

        /// <summary>
        /// Determines whether this instance has its first and last coordinate at the same position and thereby is closed.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is closed; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsClosed(this IEnumerable<IGeographicPosition> coordinates)
        {
            return coordinates.First().Equals(coordinates.Last());
        }
    }
}
