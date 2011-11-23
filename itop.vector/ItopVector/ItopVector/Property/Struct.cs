namespace ItopVector
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Runtime.InteropServices;
	using ItopVector.Core.Figure;
	

	internal enum PatternType
	{
		// Fields
		BackwardDiagonal = 1,
		Center = 0x38,
		Cross = 2,
		DarkDownwardDiagonal = 3,
		DarkHorizontal = 4,
		DarkUpwardDiagonal = 5,
		DarkVertical = 6,
		DashedDownwardDiagonal = 7,
		DashedHorizontal = 8,
		DashedUpwardDiagonal = 9,
		DashedVertical = 10,
		DiagonalBrick = 11,
		DiagonalCross = 12,
		DiagonalLeft = 0x39,
		DiagonalRight = 0x3a,
		Divot = 13,
		DottedDiamond = 14,
		DottedGrid = 15,
		ForwardDiagonal = 0x10,
		Horizontal = 0x11,
		HorizontalBrick = 0x12,
		HorizontalCenter = 0x3b,
		LargeCheckerBoard = 0x13,
		LargeConfetti = 20,
		LargeGrid = 0x15,
		LeftRight = 60,
		LightDownwardDiagonal = 0x16,
		LightHorizontal = 0x17,
		LightUpwardDiagonal = 0x18,
		LightVertical = 0x19,
		NarrowHorizontal = 0x1a,
		NarrowVertical = 0x1b,
		None = 0,
		OutlinedDiamond = 0x1d,
		Percent05 = 30,
		Percent10 = 0x1f,
		Percent20 = 0x20,
		Percent25 = 0x21,
		Percent30 = 0x22,
		Percent40 = 0x23,
		Percent50 = 0x24,
		Percent60 = 0x25,
		Percent70 = 0x26,
		Percent75 = 0x27,
		Percent80 = 40,
		Percent90 = 0x29,
		Plaid = 0x2a,
		Shingle = 0x2b,
		SmallCheckerBoard = 0x2c,
		SmallConfetti = 0x2d,
		SmallGrid = 0x2e,
		SolidDiamond = 0x2f,
		Sphere = 0x30,
		TopBottom = 0x3d,
		Trellis = 0x31,
		Vertical = 50,
		VerticalCenter = 0x3e,
		Wave = 0x33,
		Weave = 0x34,
		WideDownwardDiagonal = 0x35,
		WideUpwardDiagonal = 0x36,
		ZigZag = 0x37
	}
    public class Struct
    {
        // Methods
        public Struct()
        {
        }


        // Nested Types
        [StructLayout(LayoutKind.Sequential)]
        internal struct PropertyLineMarker
        {
			
            private bool isendarrow;
            private Arrow arrow;
            private Marker svgElement;
            private string eaf1b27180c0557b;
            public PropertyLineMarker(Arrow arrow, Marker marker, bool endarrow, string id)
            {
                this.isendarrow = endarrow;
                this.svgElement = marker;
                this.arrow = arrow;
                this.eaf1b27180c0557b = id;
            }
            public string Id
            {
                get
                {
                    return this.eaf1b27180c0557b;
                }
                set
                {
                    this.eaf1b27180c0557b = value;
                }
            }
            public bool IsEndArrow
            {
                get
                {
                    return this.isendarrow;
                }
                set
                {
                    this.isendarrow = value;
                }
            }
            public Arrow Arrow
            {
                get
                {
                    return this.arrow;
                }
                set
                {
                    this.arrow = value;
                }
            }
            public Marker SvgElement
            {
                get
                {
                    return this.svgElement;
                }
                set
                {
                    this.svgElement = value;
                }
            }
            public override string ToString()
            {
                return this.eaf1b27180c0557b;
            }
			
        }

		[StructLayout(LayoutKind.Sequential)]
		internal struct Float
		{
			private float f;
			public Float(float _f)
			{
				this.f=_f;
			}
			public float F
			{
				get {return this.f;}
				set{this.f=value;}
			}
			public override string ToString()
			{
				return f.ToString ();
			}

		}
        [StructLayout(LayoutKind.Sequential)]
        internal struct Pattern
        {
            private Color backColor;
            private PatternType patternType;
            private Color foreColor;
            public Pattern(Color backcolor, PatternType style, Color forecolor)
            {
                this.backColor = backcolor;
                this.patternType = style;
                this.foreColor = forecolor;
            }
            public Color BackColor
            {
                get
                {
                    return this.backColor;
                }
                set
                {
                    this.backColor = value;
                }
            }
            public Color ForeColor
            {
                get
                {
                    return this.foreColor;
                }
                set
                {
                    this.foreColor = value;
                }
            }
            public PatternType PatternType
            {
                get
                {
                    return this.patternType;
                }
                set
                {
                    this.patternType = value;
                }
            }
			public float Opacity
			{
				set
				{
					backColor=Color.FromArgb((int)(255f*value),backColor.R,backColor.G,backColor.B);
					foreColor=Color.FromArgb((int)(255f*value),foreColor.R,foreColor.G,foreColor.B);
				}
			}
//            public bool IsHatchStyle
//            {
//                get
//                {
//                    if (this.patternType != PatternType.None)
//                    {
//                        return (this.patternType < PatternType.Center);
//                    }
//                    return false;
//                }
//            }
            public static bool operator ==(Struct.Pattern hatch, Struct.Pattern hatch1)
            {
                if (hatch.backColor == hatch1.backColor)
                {
                    return (hatch.patternType == hatch1.patternType);
                }
                return false;
            }
            public static bool operator !=(Struct.Pattern hatch, Struct.Pattern hatch1)
            {
                if (hatch.backColor == hatch1.backColor)
                {
                    return (hatch.patternType != hatch1.patternType);
                }
                return true;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                if (obj is Struct.Pattern)
                {
                    return (this == ((Struct.Pattern) obj));
                }
                return false;
            }
            public override string ToString()
            {
                return this.patternType.ToString();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Fill
        {
            private System.Drawing.Color fillColor;
            private float fillOpacity;
            public Fill(System.Drawing.Color color, float opacity)
            {
                this.fillColor = color;
                this.fillOpacity = opacity;
            }
            public System.Drawing.Color Color
            {
                get
                {
                    return this.fillColor;
                }
                set
                {
                    this.fillColor = value;
                }
            }
            public float Opacity
            {
                get
                {
                    return this.fillOpacity;
                }
                set
                {
                    this.fillOpacity = value;
                }
            }
            public static bool operator ==(Struct.Fill fill1, Struct.Fill fill2)
            {
                if (fill1.fillColor == fill2.fillColor)
                {
                    return (fill1.fillOpacity == fill2.fillOpacity);
                }
                return false;
            }
            public static bool operator !=(Struct.Fill fill1, Struct.Fill fill2)
            {
                if (fill1.fillColor == fill2.fillColor)
                {
                    return (fill1.fillOpacity != fill2.fillOpacity);
                }
                return true;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                if (obj is Struct.Fill)
                {
                    return (this == ((Struct.Fill) obj));
                }
                return false;
            }
        }

        [Serializable, StructLayout(LayoutKind.Sequential),/* Editor("YPSoft.VectorControl.Design.GridEditor", typeof(UITypeEditor)), TypeConverter(typeof(GridConverter)),*/ ComVisible(true)]
        public struct Grid
        {
            private bool isVisible;
            private System.Drawing.Size _size;
            private System.Drawing.Color fillColor;
            private bool isSnap;
            public bool Visible
            {
                get
                {
                    return this.isVisible;
                }
                set
                {
                    this.isVisible = value;
                }
            }
            public System.Drawing.Size Size
            {
                get
                {
                    return this._size;
                }
                set
                {
                    this._size = value;
                }
            }
            public System.Drawing.Color Color
            {
                get
                {
                    return this.fillColor;
                }
                set
                {
                    this.fillColor = value;
                }
            }
            public bool Snap
            {
                get
                {
                    return this.isSnap;
                }
                set
                {
                    this.isSnap = value;
                }
            }
            public Grid(bool visible, System.Drawing.Size size, System.Drawing.Color color, bool snap)
            {
                this._size = size;
                this.fillColor = color;
                this.isSnap = snap;
                this.isVisible = visible;
            }
            public static bool operator ==(Struct.Grid grid1, Struct.Grid grid2)
            {
                if ((grid1.Visible == grid2.Visible) && (grid1.Color == grid2.Color))
                {
                    return (grid1.Size == grid2.Size);
                }
                return false;
            }
            public static bool operator !=(Struct.Grid grid1, Struct.Grid grid2)
            {
                if ((grid1.Visible == grid2.Visible) && (grid1.Color == grid2.Color))
                {
                    return (grid1.Size != grid2.Size);
                }
                return true;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                if (obj is Struct.Grid)
                {
                    return (this == ((Struct.Grid) obj));
                }
                return false;
            }
        }

        [Serializable, StructLayout(LayoutKind.Sequential), ComVisible(true), /*Editor("YPSoft.VectorControl.Design.GuideEditor", typeof(UITypeEditor)), TypeConverter(typeof(GuideConverter))*/]
        public struct Guide
        {
            private bool isVisible;
            private bool isLock;
            private System.Drawing.Color _color;
            private bool isSnap;
            public bool Visible
            {
                get
                {
                    return this.isVisible;
                }
                set
                {
                    this.isVisible = value;
                }
            }
            public bool Lock
            {
                get
                {
                    return this.isLock;
                }
                set
                {
                    this.isLock = value;
                }
            }
            public System.Drawing.Color Color
            {
                get
                {
                    return this._color;
                }
                set
                {
                    this._color = value;
                }
            }
            public bool Snap
            {
                get
                {
                    return this.isSnap;
                }
                set
                {
                    this.isSnap = value;
                }
            }
            public Guide(bool visible, bool islock, System.Drawing.Color color, bool snap)
            {
                this._color = color;
                this.isLock = islock;
                this.isSnap = snap;
                this.isVisible = visible;
            }
            public static bool operator ==(Struct.Guide guide1, Struct.Guide guide2)
            {
                if (((guide1.Visible == guide2.Visible) && (guide1.Lock == guide2.Lock)) && (guide1.Color == guide2.Color))
                {
                    return (guide1.Snap == guide2.Snap);
                }
                return false;
            }
            public static bool operator !=(Struct.Guide guide1, Struct.Guide guide2)
            {
                if (((guide1.Visible == guide2.Visible) && (guide1.Lock == guide2.Lock)) && (guide1.Color == guide2.Color))
                {
                    return (guide1.Snap != guide2.Snap);
                }
                return true;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                if (obj is Struct.Guide)
                {
                    return (this == ((Struct.Guide) obj));
                }
                return false;
            }
        }

        [Serializable, StructLayout(LayoutKind.Sequential), ComVisible(true), /*TypeConverter(typeof(RuleConverter))*/]
        public struct Rule
        {
            private ItopVector.Enums.UnitType unitType;
            private bool isVisible;
            public ItopVector.Enums.UnitType UnitType
            {
                get
                {
                    return this.unitType;
                }
                set
                {
                    this.unitType = value;
                }
            }
            public bool Visible
            {
                get
                {
                    return this.isVisible;
                }
                set
                {
                    this.isVisible = value;
                }
            }
            public Rule(bool visible, ItopVector.Enums.UnitType type)
            {
                this.isVisible = visible;
                this.unitType = type;
            }
            public static bool operator ==(Struct.Rule rule1, Struct.Rule rule2)
            {
                if (rule1.Visible == rule2.Visible)
                {
                    return (rule1.UnitType == rule2.UnitType);
                }
                return false;
            }
            public static bool operator !=(Struct.Rule rule1, Struct.Rule rule2)
            {
                if (rule1.Visible == rule2.Visible)
                {
                    return (rule1.UnitType != rule2.UnitType);
                }
                return true;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                if (obj is Struct.Rule)
                {
                    return (this == ((Struct.Rule) obj));
                }
                return false;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Stroke
        {
            public System.Drawing.Color Color;
            public float[] DashPattern;
            public float Width;
            public float Opacity;
            public Stroke(System.Drawing.Color color)
            {
                this.Color = color;
                this.DashPattern = null;
                this.Opacity = 1f;
                this.Width = 1f;
            }
            public static bool operator ==(Struct.Stroke stroke1, Struct.Stroke stroke2)
            {
                if (((stroke1.Color == stroke2.Color) && (stroke1.Opacity == stroke2.Opacity)) && (stroke1.Width == stroke2.Width))
                {
                    return (stroke1.DashPattern == stroke2.DashPattern);
                }
                return false;
            }
            public static bool operator !=(Struct.Stroke stroke1, Struct.Stroke stroke2)
            {
                if (((stroke1.Color == stroke2.Color) && (stroke1.Opacity == stroke2.Opacity)) && (stroke1.Width == stroke2.Width))
                {
                    return (stroke1.DashPattern != stroke2.DashPattern);
                }
                return true;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                if (obj is Struct.Stroke)
                {
                    return (this == ((Struct.Stroke) obj));
                }
                return false;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TextStyle
        {
            private string fontName;
            private float _size;
            private bool isBold;
            private bool isItalic;
            private bool isUnderline;
            public string FontName
            {
                get
                {
                    return this.fontName;
                }
                set
                {
                    this.fontName = value;
                }
            }
            public float Size
            {
                get
                {
                    return this._size;
                }
                set
                {
                    this._size = value;
                }
            }
            public bool Bold
            {
                get
                {
                    return this.isBold;
                }
                set
                {
                    this.isBold = value;
                }
            }
            public bool Italic
            {
                get
                {
                    return this.isItalic;
                }
                set
                {
                    this.isItalic = value;
                }
            }
            public bool Underline
            {
                get
                {
                    return this.isUnderline;
                }
                set
                {
                    this.isUnderline = value;
                }
            }
            public TextStyle(string fontname, float size, bool bold, bool italic, bool underline)
            {
                this.fontName = fontname;
                this._size = size;
                this.isBold = bold;
                this.isItalic = italic;
                this.isUnderline = underline;
            }
            public static bool operator ==(Struct.TextStyle text1, Struct.TextStyle text2)
            {
                if (((text1.isItalic == text2.isItalic) && (text1.fontName == text2.fontName)) && (text1.isBold == text2.isBold) && text1._size==text2._size)
                {
                    return (text1.isUnderline == text2.isUnderline);
                }
                return false;
            }
            public static bool operator !=(Struct.TextStyle text1, Struct.TextStyle text2)
            {
                if (((text1.isItalic == text2.isItalic) && (text1.fontName == text2.fontName)) && (text1.isBold == text2.isBold) && text1._size==text2._size)
                {
                    return (text1.isUnderline != text2.isUnderline);
                }
                return true;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                if (obj is Struct.TextStyle)
                {
                    return (this == ((Struct.TextStyle) obj));
                }
                return false;
            }
        }
    }
}

