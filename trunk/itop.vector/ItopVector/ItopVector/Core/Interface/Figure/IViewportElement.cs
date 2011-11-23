namespace ItopVector.Core.Interface.Figure
{
    using ItopVector.Core.Interface;
    using ItopVector.Core.Types;
    using System;

    public interface IViewportElement : ISvgElement
    {
        // Properties
        float Height { get; set; }

        ItopVector.Core.Types.ViewBox ViewBox { get; set; }

        float Width { get; set; }

        float X { get; set; }

        float Y { get; set; }

    }
}

