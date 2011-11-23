namespace ItopVector.Core.Interface.Figure
{
    using ItopVector.Core.Interface;
    using System;

    public interface IText : IGraphPath
    {
        // Properties
        float Dx { get; set; }

        float Dy { get; set; }

        float Rotate { get; set; }

        float X { get; set; }

        float Y { get; set; }

    }
}

