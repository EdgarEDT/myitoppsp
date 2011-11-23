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
        [Category("���"), Browsable(true), Description("����ͼ�ε������Ϣ")]
        public Color ���ɫ
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

        [Category("���"), Browsable(true), Description("ָ���������ͼ���ڲ��Ļ���͸����"), Editor(typeof(NumberEditor), typeof(UITypeEditor))]
        public Struct.Float ���͸����
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

        [Browsable(true), Description("ָ���������ͼ���ڲ���ͼ������"), Category("���"), Editor(typeof(HatchEditor), typeof(UITypeEditor))]
        public Struct.Pattern ͼ��
        {
            get
            {
                if (base.svgElement == null)
                {
                    return new Struct.Pattern(Color.White, PatternType.None, Color.Black);
                }

                Color color1 = this.���ɫ;

                PatternType patterntype = PatternType.None;
                try
                {
                    patterntype = (PatternType)System.Enum.Parse(typeof(PatternType), base.svgElement.SvgAttributes["hatch-style"].ToString(), false);
                }
                catch
                {
                }
                Color color2 = this.ͼ����ɫ;
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

        [Category("���"), Browsable(true), Description("�����������ڲ���ͼ����ǰ��ɫ")]
        public Color ͼ����ɫ
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
        public string ����ͼƬ {
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

