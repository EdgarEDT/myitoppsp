namespace ItopVector.Property
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using ItopVector.Core;
    using ItopVector.Core.Func;
    using ItopVector.Design;
    using ItopVector.Core.Figure;

    internal class PropertyText : PropertyFill
    {
        // Methods

        public PropertyText(SvgElement element)
            : base(element)
        {
        }
        [Category("内容"), Description("设置显示的文字内容"), Editor(typeof(LabelTextEditor), typeof(UITypeEditor))]
        public string 文字
        {
            get
            {
                return ((Text)base.svgElement).TextString;
            }
            set
            {
                if (value == string.Empty) return;
                ((Text)base.svgElement).TextString = value;
            }
        }
        [Category("内容"), Description("设置显示的文字方向")]
        public bool 垂直
        {
            get
            {
                return (base.svgElement as Text).Vertical;
            }
            set
            {
                string text1 = value ? "tb" : "lr-tb";
                base.SetAttributeValue("writing-mode", text1);
            }
        }


        // Properties
        [Category("字体"), Description("确定文本是否采用粗体绘制")]
        public bool 粗体
        {
            get
            {
                if (base.svgElement is Text)
                {
                    string text1 = string.Empty;
                    if (base.svgElement.SvgAttributes.ContainsKey("font-weight"))
                    {
                        text1 = base.svgElement.SvgAttributes["font-weight"].ToString();
                    }

                    bool flag1 = (text1 == "bold") || (text1 == "bolder");
                    text1 = null;
                    return flag1;
                }
                return false;
            }
            set
            {
                if (base.svgElement != null)
                {
                    string text1 = value ? "bold" : "normal";
                    base.SetAttributeValue("font-weight", text1);
                    text1 = null;
                }
            }
        }

        [Category("字体"), Description("确定文本是否具备下划线")]
        public bool 下划线
        {
            get
            {
                if (base.svgElement is Text)
                {
                    string text1 = string.Empty;
                    if (base.svgElement.SvgAttributes.ContainsKey("text-decoration"))
                    {
                        text1 = base.svgElement.SvgAttributes["text-decoration"].ToString();
                    }

                    bool flag1 = (text1 == "underline");
                    text1 = null;
                    return flag1;
                }
                return false;
            }
            set
            {
                if (base.svgElement != null)
                {
                    string text1 = value ? "underline" : "normal";
                    base.SetAttributeValue("text-decoration", text1);
                    text1 = null;
                }
            }
        }

        [Description("确定文本是否采用斜体绘制"), Category("字体")]
        public bool 斜体
        {
            get
            {
                if (base.svgElement is Text)
                {
                    string text1 = string.Empty;
                    if (base.svgElement.SvgAttributes.ContainsKey("font-style"))
                    {
                        text1 = base.svgElement.SvgAttributes["font-style"].ToString();
                    }

                    bool flag1 = (text1 == "italic");
                    text1 = null;
                    return flag1;
                }
                return false;
            }
            set
            {
                if (base.svgElement != null)
                {
                    string text1 = value ? "italic" : "normal";
                    base.SetAttributeValue("font-style", text1);
                    text1 = null;
                }
            }
        }

        [Editor(typeof(FontEditor), typeof(UITypeEditor)), Category("字体"), Description("设置绘制文本的字体名称")]
        public string 字体
        {
            get
            {
                if (base.svgElement is Text)
                {

                    if (base.svgElement.SvgAttributes.ContainsKey("font-family"))
                    {

                        return base.svgElement.SvgAttributes["font-family"].ToString();
                    }
                }
                return "宋体";
            }
            set
            {
                if (base.svgElement != null)
                {
                    base.SetAttributeValue("font-family", value);
                }
            }
        }

        [Editor(typeof(ListFontSize), typeof(UITypeEditor)), Description("设置用来绘制文本的字体的大小"), Category("字体")]
        public float 字体大小
        {
            get
            {
                if (base.svgElement is Text)
                {

                    if (base.svgElement.SvgAttributes.ContainsKey("font-size"))
                    {
                        //return new Struct.Float(Number.ParseFloatStr(base.svgElement.SvgAttributes["font-size"].ToString()));
                        return Number.ParseFloatStr(base.svgElement.SvgAttributes["font-size"].ToString());
                    }
                }
                return 12f;
                //return new Struct.Float(12f);
            }
            set
            {
                if ((value < 0f) || (value > float.MaxValue)) {
                    float single1 = float.MaxValue;
                    throw new Exception("无效的尺寸值，尺寸应该介于0和" + single1.ToString() + "之间.");
                }
                if (base.svgElement != null)
                {
                    base.SetAttributeValue("font-size", value.ToString());
                }
            }
        }
        public bool 锁定大小
        {
            get { return (base.svgElement as Text).LimitSize; }
            set { (base.svgElement as Text).LimitSize = value; }
        }
    }
}

