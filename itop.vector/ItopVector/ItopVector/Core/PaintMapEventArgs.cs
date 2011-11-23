namespace ItopVector.Core
{
    using ItopVector.Core.Interface;
    using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;

    public class PaintMapEventArgs
    {
        // Methods
        public PaintMapEventArgs(Graphics g,PointF offSet,Rectangle bounds)
        {
			G = g;
			OffSet = offSet;
			Bounds = bounds;
			CenterPoint = PointF.Empty;
        }       
        // Fields
        public Graphics G;
		public PointF OffSet;
		public Rectangle Bounds;
		public PointF CenterPoint;
    }
}

