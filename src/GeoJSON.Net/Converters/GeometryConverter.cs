// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeometryConverter.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the GeometryConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using GeoJSON.Net.Feature;

namespace GeoJSON.Net.Converters
{
    using System;

    using GeoJSON.Net.Geometry;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Defines the GeometryObject type. Converts to/from a SimpleGeo 'geometry' field
    /// </summary>
    public class GeometryConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param><param name="value">The value.</param><param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                if (value is IGeometryObject)
                    GeometryWriter.WriteGeometry(writer, value as IGeometryObject);
                else
                    throw new NotSupportedException(value.GetType().Name);
            }
            catch (NotImplementedException)
            {
                serializer.Serialize(writer, value);
            }
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
            // Load JObject from stream
            JObject jObject = JObject.Load(reader); ;
            
            if (jObject["type"] == null)
                throw new ArgumentException("Malformed geojson: cannot find 'type' field");

            return DeserializeObject(jObject);
        }

        public static IGeometryObject DeserializeObject(JObject jObject)
        {
            if (jObject["type"].Value<string>() == "Point")
            {
                var point = JsonConvert.DeserializeObject<Point>(jObject.ToString(), new JsonConverter[] { new SinglePositionConverter() });
                return point;
            }
            else if (jObject["type"].Value<string>() == "LineString")
            {
                var point = JsonConvert.DeserializeObject<LineString>(jObject.ToString());
                return point;
            }
            else if (jObject["type"].Value<string>() == "Polygon")
            {
                var point = JsonConvert.DeserializeObject<Polygon>(jObject.ToString());
                return point;
            }
            else if (jObject["type"].Value<string>() == "MultiPoint")
            {
                var point = JsonConvert.DeserializeObject<MultiPoint>(jObject.ToString());
                return point;
            }
            else if (jObject["type"].Value<string>() == "MultiLineString")
            {
                var point = JsonConvert.DeserializeObject<MultiLineString>(jObject.ToString());
                return point;
            }
            else if (jObject["type"].Value<string>() == "MultiPolygon")
            {
                var point = JsonConvert.DeserializeObject<MultiPolygon>(jObject.ToString());
                return point;
            }
            else if (jObject["type"].Value<string>() == "GeometryCollection")
            {
                var point = JsonConvert.DeserializeObject<GeometryCollection>(jObject.ToString());
                return point;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(IGeometryObject));
        }
    }
}
