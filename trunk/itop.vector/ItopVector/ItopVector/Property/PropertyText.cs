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
        [Category("����"), Description("������ʾ����������"), Editor(typeof(LabelTextEditor), typeof(UITypeEditor))]
        public string ����
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
        [Category("����"), Description("������ʾ�����ַ���")]
        public bool ��ֱ
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
        [Category("����"), Description("ȷ���ı��Ƿ���ô������")]
        public bool ����
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

        [Category("����"), Description("ȷ���ı��Ƿ�߱��»���")]
        public bool �»���
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

        [Description("ȷ���ı��Ƿ����б�����"), Category("����")]
        public bool б��
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

        [Editor(typeof(FontEditor), typeof(UITypeEditor)), Category("����"), Description("���û����ı�����������")]
        public string ����
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
                return "����";
            }
            set
            {
                if (base.svgElement != null)
                {
                    base.SetAttributeValue("font-family", value);
                }
            }
        }

        [Editor(typeof(ListFontSize), typeof(UITypeEditor)), Description("�������������ı�������Ĵ�С"), Category("����")]
        public float �����С
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
                    throw new Exception("��Ч�ĳߴ�ֵ���ߴ�Ӧ�ý���0��" + single1.ToString() + "֮��.");
                }
                if (base.svgElement != null)
                {
                    base.SetAttributeValue("font-size", value.ToString());
                }
            }
        }
        public bool ������С
        {
            get { return (base.svgElement as Text).LimitSize; }
            set { (base.svgElement as Text).LimitSize = value; }
        }
    }
}

