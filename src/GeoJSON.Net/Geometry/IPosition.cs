using Newtonsoft.Json;

namespace GeoJSON.Net.Geometry
{
    /// <summary>
    /// inherited from for either geographic or projected positions
    /// </summary>
    public interface IPosition
    {
        /// <summary>
        ///     Gets the latitude.
        /// </summary>
        /// <value>The latitude or Y or northing.</value>
        double Y { get; }

        /// <summary>
        ///     Gets the longitude.
        /// </summary>
        /// <value>The longitude or X or nothing.</value>
        double X { get; }

        /// <summary>
        /// or Y
        /// </summary>
        double Latitude { get; }

        /// <summary>
        /// or X
        /// </summary>
        double Longitude { get; }

        /// <summary>
        /// Gets the altitude.
        /// </summary>
        double? Altitude { get; }

        /// <summary>
        ///     Gets or sets the coordinates, is a 2-size array
        /// </summary>
        /// <value>
        ///     The coordinates.
        /// </value>
        double?[] Coordinates { get; set; }
    }
}