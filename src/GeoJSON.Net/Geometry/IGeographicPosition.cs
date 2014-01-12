namespace GeoJSON.Net.Geometry
{
    public interface IGeographicPosition
    {
        double Latitude { get; }
        double Longitude { get; }
        bool Equals(IGeographicPosition otherPosition);
    }
}