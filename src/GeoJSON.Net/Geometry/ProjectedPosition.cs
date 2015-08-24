// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectedPosition.cs" company="Joerg Battermann">
//   Copyright © Joerg Battermann 2014
// </copyright>
// <summary>
//   Defines the Projected Position type a.k.a. <see cref="http://geojson.org/geojson-spec.html#positions">Projected Coordinate Reference System</see>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Defines the Projected Position type a.k.a. <see cref="http://geojson.org/geojson-spec.html#positions">Projected Coordinate Reference System</see>.
    /// </summary>
    public class ProjectedPosition : Position
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectedPosition"/> class.
        /// </summary>
        /// <param name="easting">The easting.</param>
        /// <param name="northing">The northing.</param>
        /// <param name="altitude">The altitude in m(eter).</param>
        public ProjectedPosition(double easting, double northing, double? altitude)
            : base(easting, northing, altitude)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectedPosition"/> class.
        /// </summary>
        /// <param name="easting">The easting, e.g. '1707819.223'.</param>
        /// <param name="northing">The northing, e.g. '5439650.751'.</param>
        /// <param name="altitude">The altitude in m(eter).</param>
        public ProjectedPosition(string easting, string northing, string altitude)
            : base()
        {
            if (northing == null)
            {
                throw new ArgumentNullException("northing");
            }

            if (easting == null)
            {
                throw new ArgumentNullException("easting");
            }

            if (string.IsNullOrWhiteSpace(northing))
            {
                throw new ArgumentOutOfRangeException("northing", "May not be empty.");
            }

            if (string.IsNullOrWhiteSpace(easting))
            {
                throw new ArgumentOutOfRangeException("easting", "May not be empty.");
            }

            double north;
            double east;

            if (!double.TryParse(northing, NumberStyles.Float, CultureInfo.InvariantCulture, out north))
            {
                throw new ArgumentOutOfRangeException("northing", "Northing must be a proper easting (+/- double) value.");
            }

            if (!double.TryParse(easting, NumberStyles.Float, CultureInfo.InvariantCulture, out east))
            {
                throw new ArgumentOutOfRangeException("easting", "Easting must be a proper northing (+/- double) value.");
            }

            Northing = north;
            Easting = east;

            if (altitude != null)
            {
                double alt;
                if (!double.TryParse(altitude, NumberStyles.Float, CultureInfo.InvariantCulture, out alt))
                {
                    throw new ArgumentOutOfRangeException("altitude", "Altitude must be a proper altitude (m(eter) as double) value, e.g. '6500'.");
                }

                Altitude = alt;
            }
        }

        ///// <summary>
        ///// Prevents a default instance of the <see cref="ProjectedPosition"/> class from being created.
        ///// </summary>
        //private ProjectedPosition()
        //{
        //    Coordinates = new double?[3];
        //}

        /// <summary>
        /// Gets the easting.
        /// </summary>
        /// <value>The easting.</value>
        public double Easting
        {
            get { return X; }
            private set { X = value; }
        }
        
        /// <summary>
        /// Gets the northing.
        /// </summary>
        /// <value>The northing.</value>
        public double Northing
        {
            get { return Y; }
            private set { Y = value; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Altitude == null ? 
                string.Format(CultureInfo.InvariantCulture, "Easting: {0}, Northing: {1}", this.Easting, this.Northing) : 
                string.Format(CultureInfo.InvariantCulture, "Easting: {0}, Northing: {1}, Altitude: {2}", this.Easting, this.Northing, this.Altitude);
        }

    }
}
