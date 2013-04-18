using System.Collections.Generic;

namespace Geography
{
    public static class Polygon
    {
        public static bool PointInPolygon(LatLong p, List<LatLong> polygonData)
        {
            int n = polygonData.Count;

            polygonData.Add(new LatLong { Lat = polygonData[0].Lat, Lon = polygonData[0].Lon });
            var v = polygonData.ToArray();

            int wn = 0;    // the winding number counter

            // loop through all edges of the polygon
            for (int i = 0; i < n; i++)
            {   // edge from V[i] to V[i+1]
                if (v[i].Lat <= p.Lat)
                {         // start y <= P.y
                    if (v[i + 1].Lat > p.Lat)      // an upward crossing
                        if (IsLeft(v[i], v[i + 1], p) > 0)  // P left of edge
                            ++wn;            // have a valid up intersect
                }
                else
                {                       // start y > P.y (no test needed)
                    if (v[i + 1].Lat <= p.Lat)     // a downward crossing
                        if (IsLeft(v[i], v[i + 1], p) < 0)  // P right of edge
                            --wn;            // have a valid down intersect
                }
            }
            if (wn != 0)
                return true;
            return false;
        }

        private static int IsLeft(LatLong p0, LatLong p1, LatLong p2)
        {
            double calc = ((p1.Lon - p0.Lon) * (p2.Lat - p0.Lat) - (p2.Lon - p0.Lon) * (p1.Lat - p0.Lat));
            if (calc > 0)
                return 1;
            if (calc < 0)
                return -1;
            return 0;
        }
    }

    public class LatLong
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}