namespace ItopVector.Core.Interface.Paint
{
    using ItopVector.Core.Interface;
    using ItopVector.Core.Paint;
    using ItopVector.Core.Types;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public interface ITransformBrush : ISvgBrush, ISvgElement
    {
        // Properties
        PointF[] BoundsPoints { get; }

        Matrix Coord { get; }

        GraphicsPath GradientPath { get; }

        Transf Transform { get; set; }

        Units Units { get; set; }

    }
}

