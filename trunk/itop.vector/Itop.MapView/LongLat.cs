using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.MapView
{
    public struct LongLat
    {
        public double Longitude;
        public double Latitude;
        public LongLat(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
        public LongLat(decimal longitude, decimal latitude) {
            Longitude =(double)longitude;
            Latitude = (double)latitude;
        }
        static public LongLat Empty
        {
            get
            {
                return new LongLat(0.0, 0.0);
            }
        }
    }
}
