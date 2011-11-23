namespace ItopVector.Core.Interface
{
    using ItopVector.Core.Types;
    using System;
    using System.Drawing.Drawing2D;

    public interface IPath
    {
        // Properties
        GraphicsPath GPath { get; set; }

        PointInfoCollection PointsInfo { get; }

    }
}

