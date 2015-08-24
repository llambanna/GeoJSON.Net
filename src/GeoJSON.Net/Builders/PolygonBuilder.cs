using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoJSON.Net.Builders
{
    /// <summary>
    /// used to build up <see cref="Polygon" />
    /// </summary>
    public class PolygonBuilder
    {
        Polygon _polygon = null;

        public PolygonBuilder() { }

        /// <summary>
        /// closes any rings
        /// </summary>
        /// <param name="points"></param>
        public PolygonBuilder(List<IPosition> outerRingPoints)
        {
            if (outerRingPoints.Count <= 3)
                throw new ArgumentException("Must have more than 2 points for a polygon", "points");

            LineString ring = new LineString(outerRingPoints);

            if (!ring.IsClosed())
                ring.Coordinates.Add(ring.Coordinates[0]);

            List<LineString> lines = new List<LineString>();

            lines.Add(ring);
            
            _polygon = new Polygon(lines);

        }

        public Polygon ToGeometry()
        {
            return _polygon;
        }

    }
}
