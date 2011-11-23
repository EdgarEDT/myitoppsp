using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.MapView
{
    public struct IntXY
    {
        public double X;
        public double Y;
        public IntXY(double _x, double _y)
        {
            X = _x;
            Y = _y;
        }
        static public IntXY Empty
        {
            get
            {
                return new IntXY(0, 0);
            }
        }
    }
}
