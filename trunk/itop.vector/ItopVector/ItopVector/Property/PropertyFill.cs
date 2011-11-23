namespace ItopVector.Property
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using ItopVector.Core;
    using ItopVector;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Design;
    using ItopVector.Core.Figure;

    internal class PropertyFill : PropertyLine
    {
        // Methods
        public PropertyFill(SvgElement render)
            : base(render)
        {
        }


        // Properties
        [Category("填充"), Browsable(true), Description("设置图形的填充信息")]
        public Color 填充色
        {
            get
            {
                if (base.svgElement != null)
                {
                    if (base.svgElement.SvgAttributes.ContainsKey("fill"))
                        return ColorFunc.ParseColor(base.svgElement.SvgAttributes["fill"].ToString());

                }
                return Color.Empty;
            }
            set
            {
                if (base.svgElement != null)
                {
                    string text1 = ColorFunc.GetColorString(value);

                    base.SetAttributeValue("fill", text1);
                    text1 = null;
                }
            }
        }

        [Category("填充"), Browsable(true), Description("指定用来填充图形内部的画笔透明度"), Editor(typeof(NumberEditor), typeof(UITypeEditor))]
        public Struct.Float 填充透明度
        {
            get
            {
                if (base.svgElement != null)
                {
                    //					return Number.ParseFloatStr(base.svgElement.SvgAttributes["fill-opacity"].ToString());
                    //					IGraphPath igraphpath=svgElement as IGraphPath;
                    //					if (igraphpath!=null)
                    //					{
                    //						return igraphpath.FillOpacity;
                    //					}
                    float single2 = 1f;
                    object obj1 = AttributeFunc.ParseAttribute("fill-opacity", base.svgElement, false);
                    if (obj1 is float)
                    {
                        single2 = (float)obj1;
                    }
                    return new Struct.Float(single2);
                }
                return new Struct.Float(1f);
            }
            set
            {

                if (base.svgElement != null)
                {
                    string text1 = value.F.ToString();

                    base.SetAttributeValue("fill-opacity", text1);
                }
            }
        }

        [Browsable(true), Description("指定用来填充图形内部的图案类型"), Category("填充"), Editor(typeof(HatchEditor), typeof(UITypeEditor))]
        public Struct.Pattern 图案
        {
            get
            {
                if (base.svgElement == null)
                {
                    return new Struct.Pattern(Color.White, PatternType.None, Color.Black);
                }

                Color color1 = this.填充色;

                PatternType patterntype = PatternType.None;
                try
                {
                    patterntype = (PatternType)System.Enum.Parse(typeof(PatternType), base.svgElement.SvgAttributes["hatch-style"].ToString(), false);
                }
                catch
                {
                }
                Color color2 = this.图案颜色;
                return new Struct.Pattern(color1, patterntype, color2);
            }
            set
            {
                if (base.svgElement != null)
                {
                    string text1 = value.PatternType.ToString();

                    base.SetAttributeValue("hatch-style", text1);

                }
            }
        }

        [Category("填充"), Browsable(true), Description("设置填充对象内部的图案的前景色")]
        public Color 图案颜色
        {
            get
            {
                if (base.svgElement != null)
                {
                    Color color1 = Color.Empty;
                    if (base.svgElement.SvgAttributes.ContainsKey("hatch-color"))
                    {
                        color1 = ColorFunc.ParseColor(base.svgElement.SvgAttributes["hatch-color"].ToString());
                    }

                    if (color1.IsEmpty)
                        color1 = Color.Black;

                    return color1;
                }
                return Color.Black;
            }
            set
            {
                if (base.svgElement != null)
                {
                    string text1 = ColorFunc.GetColorString(value);


                    base.SetAttributeValue("hatch-color", text1);

                }
            }
        }
        public string 背景图片 {
            get {
                Polygon obj = this.svgElement as Polygon;
                if(obj!=null)
                    return obj.BackgroundImageFile;
                else
                    return string.Empty;
            }
            set {
                Polygon obj = this.svgElement as Polygon;
                if (obj != null)
                    obj.BackgroundImageFile = value;
            }
        }
    }
}

