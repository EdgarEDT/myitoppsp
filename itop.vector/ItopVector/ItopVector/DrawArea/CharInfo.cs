    
using System.Drawing;
using System.Xml;
using ItopVector.Core.Figure;

namespace ItopVector.DrawArea
{

    public class CharInfo
    {
        // Methods
        public CharInfo()
        {
            this.Paint = Color.Black;
            this.Stroke = Color.Black;
            this.StringFormat = StringFormat.GenericTypographic;
            this.Horizontal = true;
            this.firstChild = false;
            this.valueX = false;
            this.valueY = false;
            this.StringFormat.LineAlignment = StringAlignment.Near;
        }


        // Fields
        public SizeF CharSize;
        public float Dx;
        public float Dy;
        public bool firstChild;
        public Font Font;
        public bool Horizontal;
        public Color Paint;
        public PointF Position;
        public XmlNode PreSibling;
        public StringFormat StringFormat;
        public Color Stroke;
        public XmlText TextNode;
        public Text TextSvgElement;
        public bool valueX;
        public bool valueY;
        public float X;
        public float Y;
    }
}

