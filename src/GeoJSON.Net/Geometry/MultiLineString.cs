// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiLineString.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#multilinestring">MultiLineString</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using GeoJSON.Net.Converters;

    /// <summary>
    /// Defines the <see cref="http://geojson.org/geojson-spec.html#multilinestring">MultiLineString</see> type.
    /// </summary>
    public class MultiLineString : GeoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiLineString"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public MultiLineString(List<List<IGeographicPosition>> coordinates)
        {
            this.Coordinates = coordinates ?? new List<List<IGeographicPosition>>();
            this.Type = GeoJSONObjectType.MultiLineString;
        }
        
        /// <summary>
        /// Gets the Coordinates.
        /// </summary>
        /// <value>The Coordinates.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(MultiPositionConverter))]
        public List<List<IGeographicPosition>> Coordinates { get; private set; }
    }
}
