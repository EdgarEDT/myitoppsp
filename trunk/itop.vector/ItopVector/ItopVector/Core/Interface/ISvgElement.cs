namespace ItopVector.Core.Interface
{
    using ItopVector.Core.Interface.Figure;
    using System;
    using System.Drawing;

    public interface ISvgElement
    {
        // Properties
        string ID { get; set; }

        bool InKey { get; set; }

        bool ShowChilds { get; set; }

        RectangleF ViewPort { get; }

        IViewportElement ViewportElement { get; }

        string XmlBase { get; set; }
        
    }
}

