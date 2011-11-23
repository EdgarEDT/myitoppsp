namespace ItopVector.Core.Paint
{
    using ItopVector.Core.Figure;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

	/// <summary>
	/// »­Ë¢½Ó¿Ú
	/// </summary>
    public interface ISvgBrush
    {
        // Methods
        ISvgBrush Clone();

        bool IsEmpty();

        void Paint(GraphPath graph, Graphics g, int time);

        void Paint(GraphicsPath path, Graphics g, int time);

        void Paint(GraphicsPath graph, Graphics g, int time, float opacity);

        void Stroke(GraphicsPath path, Graphics g, int time, float opacity);


        // Properties
        System.Drawing.Pen Pen { get;set; }

    }
}

