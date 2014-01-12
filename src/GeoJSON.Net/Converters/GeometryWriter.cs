using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.Converters
{
    static class GeometryWriter
    {
        public static void WriteGeometry(JsonWriter writer, IGeometryObject geometryObject)
        {
            if (geometryObject == null)
                throw new ArgumentNullException();
            if (geometryObject is Point)
                WritePoint(writer, geometryObject);
            else if (geometryObject is MultiPoint)
                WriteMultiPoint(writer, geometryObject);
            else if (geometryObject is GeometryCollection)
                WriteGeometryCollection(writer, geometryObject);
            else if (geometryObject is LineString)
                WriteLineString(writer, geometryObject);
            else 
                throw new NotImplementedException(geometryObject.GetType().Name);
        }

        private static void WriteLineString(JsonWriter writer, IGeometryObject geometryObject)
        {
            var line = geometryObject as LineString;
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteValue("LineString");
            writer.WritePropertyName("coordinates");
            writer.WriteStartArray();
            foreach (var coordinate in line.Coordinates)
            {
                writer.WriteStartArray();
                writer.WriteValue((coordinate as GeographicPosition).Longitude);
                writer.WriteValue((coordinate as GeographicPosition).Latitude);
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        private static void WriteGeometryCollection(JsonWriter writer, IGeometryObject geometryObject)
        {
            var collection = geometryObject as GeometryCollection;
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteValue("GeometryCollection");
            writer.WritePropertyName("geometries");
            writer.WriteStartArray();
            foreach (var geometry in collection.Geometries.Where(g => g != null))
            {
                WriteGeometry(writer, geometry);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        private static void WriteMultiPoint(JsonWriter writer, IGeometryObject value)
        {
            var multipoint = value as MultiPoint;
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteValue("MultiPoint");
            writer.WritePropertyName("coordinates");
            writer.WriteStartArray();
            foreach (var position in multipoint.Coordinates)
            {
                writer.WriteStartArray();
                writer.WriteValue(position.Longitude);
                writer.WriteValue(position.Latitude);
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        private static void WritePoint(JsonWriter writer, IGeometryObject value)
        {
            var point = value as Point;
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteValue("Point");
            writer.WritePropertyName("coordinates");
            writer.WriteStartArray();
            writer.WriteValue(point.Coordinates.Longitude);
            writer.WriteValue(point.Coordinates.Latitude);
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
