    using ItopVector.Core.Interface;
    using ItopVector.Core.Paint;
    using System;
namespace ItopVector.Core.Interface.Figure
{

    public interface IGraphPath : IGraph
    {
        // Properties
        float FillOpacity { get; set; }

        ISvgBrush GraphBrush { get; set; }

        Stroke GraphStroke { get; set; }

        float Opacity { get; set; }

        float StrokeOpacity { get; set; }

        bool UpdateAttribute { get; set; }

    }
}

