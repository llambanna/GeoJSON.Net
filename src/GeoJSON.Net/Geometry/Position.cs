namespace GeoJSON.Net.Geometry
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// A position is the fundamental geometry construct. 
    /// The "coordinates" member of a geometry object is composed of one position (in the case of a Point geometry)
    /// , an array of positions (LineString or MultiPoint geometries), 
    /// an array of arrays of positions (Polygons, MultiLineStrings), 
    /// or a multidimensional array of positions (MultiPolygon).
    /// <see cref="http://geojson.org/geojson-spec.html#positions">Positions</see>.
    /// </summary>
    public class Position : IPosition
    {
        protected static readonly NullableDoubleTenDecimalPlaceComparer DoubleComparer = new NullableDoubleTenDecimalPlaceComparer();

        /// <summary>
        /// instantiates the coordinates array
        /// </summary>
        protected Position()
        {
            Coordinates = new double?[3];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class.
        /// </summary>
        /// <param name="coordinates">must be at least a 2 value array</param>
        public Position(double[] coordinates)
            : this()
        {

            if (coordinates.Length != 2 && coordinates.Length != 3)
            {
                throw new ArgumentNullException(string.Format("Expected 2 or 3 coordinates but received {0}", coordinates));
            }

            X = coordinates[0];
            Y = coordinates[1];
            
            if (coordinates.Length == 3)
            {
                Altitude = coordinates[2];
            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="altitude">The altitude in m(eter).</param>
        public Position(double x, double y, double? altitude)
            : this()
        {
            X = x;
            Y = y;
            Altitude = altitude;
        }

        /// <summary>
        ///     Gets the Y.
        /// </summary>
        /// <value>The Y or latitude or northing.</value>
        public double Y
        {
            get { return Coordinates[1].GetValueOrDefault(); }
            protected set { Coordinates[1] = value; }
        }

        /// <summary>
        ///     Gets the X.
        /// </summary>
        /// <value>The X or longitude or easting.</value>
        public double X
        {
            get { return Coordinates[0].GetValueOrDefault(); }
            protected set { Coordinates[0] = value; }
        }

        /// <summary>
        /// Gets the Latitude
        /// </summary>
        public double Latitude
        {
            get { return Y; }
        }

        /// <summary>
        /// Gets the Longitude
        /// </summary>
        public double Longitude
        {
            get { return X; }
        }

        /// <summary>
        ///     Gets or sets the coordinates, is a 2-size array
        /// </summary>
        /// <value>
        ///     The coordinates.
        /// </value>
        public double?[] Coordinates { get; set; }

        /// <summary>
        /// Gets the altitude.
        /// </summary>
        public double? Altitude
        {
            get { return Coordinates[2]; }
            protected set { Coordinates[2] = value; }
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Coordinates != null ? Coordinates.GetHashCode() : 0;
        }


        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                // check whether obj and this are either:
                // of type position OR of a type that is a subclass of position
                bool objIsTypePositionOrSubclassOfTypePosition = obj.GetType() == typeof(Position) || obj.GetType().IsSubclassOf(typeof(Position));
                bool thisIsTypePositionOrSubclassOfTypePosition = GetType() == typeof(Position) || GetType().IsSubclassOf(typeof(Position));

                // if neither this or obj is of type position or subclass
                if (!(objIsTypePositionOrSubclassOfTypePosition && thisIsTypePositionOrSubclassOfTypePosition))
                    return false;
            }

            return Equals((Position)obj);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Position left, Position right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Position left, Position right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Position" />, is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Position" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Position" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        protected bool Equals(Position other)
        {
            return Coordinates.SequenceEqual(other.Coordinates, DoubleComparer);
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
                string.Format(CultureInfo.InvariantCulture, "X: {0}, Y: {1}", this.X, this.Y) :
                string.Format(CultureInfo.InvariantCulture, "X: {0}, Y: {1}, Altitude: {2}", this.X, this.Y, this.Altitude);
        }


    }
}