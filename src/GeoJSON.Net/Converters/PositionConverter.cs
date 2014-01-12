// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PositionConverter.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the PolygonConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Converters
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using GeoJSON.Net.Exceptions;
    using GeoJSON.Net.Geometry;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Diagnostics;

    /// <summary>
    /// Converter to read and write the <see cref="GeographicPosition" /> type.
    /// </summary>
    public abstract class PositionConverter : JsonConverter
    {
        /// <summary>
        /// This method performs actual JArray parsing. It should be overriden in order to handle different array dimensions for different geometry types.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public abstract object ParseCoordinates(JArray coordinates);

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param><param name="objectType">Type of the object.</param><param name="existingValue">The existing value of object being read.</param><param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var coordinates = serializer.Deserialize<JArray>(reader);
            if (coordinates == null)
            {
                throw new ParsingException(
                    string.Format(
                        "Point geometry coordinates could not be parsed. Expected something like '[-122.428938,37.766713]' ([lon,lat]), what we received however was: {0}", 
                        coordinates));
            }
            return ParseCoordinates(coordinates);
        }        
  
        /// <summary>
        /// Parses a plain array of coordinates (e.g. [103.0, 1.0] ([lon,lat])) and returns <typeparamref name="GeographicPosition"/> object
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        protected static object ParseCoordinatesPair(JArray coordinates)
        {
            string latitude;
            string longitude;
            try
            {
                longitude = coordinates.First.ToString();
                latitude = coordinates.Last.ToString();
            }
            catch (Exception ex)
            {
                throw new ParsingException("Could not parse GeoJSON Response. (Latitude or Longitude missing from Point geometry?)", ex);
            }

            return new GeographicPosition(latitude, longitude);
        }
    }
}
