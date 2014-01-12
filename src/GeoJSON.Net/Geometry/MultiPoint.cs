// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiPoint.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the MultiPoint type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using GeoJSON.Net.Converters;

    /// <summary>
    /// Contains an array of <see cref="Point"/>s.
    /// </summary>
    /// <seealso cref="http://geojson.org/geojson-spec.html#multipoint"/>
    public class MultiPoint : GeoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPoint"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public MultiPoint(List<IGeographicPosition> coordinates)
        {
            if (coordinates == null)
            {
                this.Coordinates = new List<IGeographicPosition>();
            }
            else
            {
                this.Coordinates = coordinates;
            }
            this.Type = GeoJSONObjectType.MultiPoint;
        }
        
        /// <summary>
        /// Gets the Coordinates.
        /// </summary>
        /// <value>The Coordinates.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PositionsListConverter))]
        public List<IGeographicPosition> Coordinates { get; private set; }
    }
}
