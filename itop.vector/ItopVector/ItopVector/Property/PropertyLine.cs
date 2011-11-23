namespace ItopVector.Property
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Text;
    using System.Text.RegularExpressions;
    using ItopVector.Core;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Core.Func;
    using ItopVector.Design;
    using System.Windows.Forms;

    [Browsable(true)]
    internal class PropertyLine : PropertyBase
    {
        // Methods
        static PropertyLine()
        {
            PropertyLine._regex = new Regex(@"[\s\,]+");
        }

        public PropertyLine(SvgElement render)
            : base(render)
        {
        }

        private string GetString(float[] singles)
        {
            if ((singles == null) || (singles.Length == 0))
            {
                return "none";
            }
            StringBuilder builder1 = new StringBuilder(100);
            for (int num1 = 0; num1 < singles.Length; num1++)
            {
                builder1.Append(singles[num1].ToString() + " ");
            }
            return builder1.ToString().Trim();
        }


        // Properties
        [Editor(typeof(StrokeWidthEditor), typeof(UITypeEditor)), Category("����"), Browsable(true), Description("ָ������ͼ�α�Ե���������")]

        public float �������
        {
            get
            {
                if (base.svgElement != null)
                {
                    IGraphPath igraphpath = svgElement as IGraphPath;
                    if (igraphpath != null)
                    {
                        float single2 = 1f;
                        object obj1 = AttributeFunc.FindAttribute("stroke-width", base.svgElement);
                        if (obj1 is float)
                        {
                            single2 = (float)obj1;
                        }
                        return single2;
                    }
                }
                return 1;
            }
            set
            {
                string text1 = value.ToString();

                base.SetAttributeValue("stroke-width", text1);

            }
        }
        [Category("����"), Browsable(true), Description("Ԫ��������")]
        public int ���� {
            get {
                string text1 = string.Empty;
                if (svgElement.SvgAttributes.ContainsKey("linetype")) {
                    text1 = svgElement.SvgAttributes["linetype"].ToString();
                }
                if (string.IsNullOrEmpty(text1)) text1 = "0";
                int nn = int.Parse(text1);
                return nn;
            }
            set {
                string text1 = value.ToString();
                if (text1 == "1" || text1 == "2" || text1 == "3" || text1 == "4")
                    AttributeFunc.SetAttributeValue(svgElement, "linetype", text1);
                else
                    svgElement.RemoveAttribute("linetype");
                

            }
        }
        [Browsable(true), Category("����"), Description("ָ�����ƶ����Ե��������ɫ")]
        public Color ����ɫ
        {
            get
            {
                if (base.svgElement != null)
                {
                    IGraphPath igraphpath = svgElement as IGraphPath;
                    if (igraphpath != null)
                    {
                        return igraphpath.GraphStroke.StrokeColor;
                    }
                }
                return Color.Black;
            }
            set
            {
                if (base.svgElement != null)
                {
                    string text1 = ColorFunc.GetColorString(value);

                    base.SetAttributeValue("stroke", text1);
                }
            }
        }

        [Category("����"), Description("ָ������ͼ�α�Ե������͸���ȣ���(0-1)���ֱ�ʾ"), Browsable(true), Editor(typeof(NumberEditor), typeof(UITypeEditor))]
        public Struct.Float ����͸����
        {
            get
            {
                if (base.svgElement != null)
                {
                    //					IGraphPath igraphpath=svgElement as IGraphPath;
                    //					if (igraphpath!=null)
                    //					{
                    //						return igraphpath.StrokeOpacity;
                    //					}
                    float single2 = 1f;
                    object obj1 = AttributeFunc.FindAttribute("stroke-opacity", base.svgElement);
                    if (obj1 is float)
                    {
                        single2 = (float)obj1;
                    }
                    //return single2;
                    return new Struct.Float(single2);

                }
                return new Struct.Float(1f);
            }
            set
            {

                if (base.svgElement != null)
                {
                    string text1 = value.F.ToString();

                    base.SetAttributeValue("stroke-opacity", text1);
                }
            }
        }

        [Browsable(true), Description("�ƶ�����ͼ�α�Ե��������ʽ��ֵ��һ���ÿո�\" \"�ָ�������ַ�������\"3 1 3\"�������Ӧ����ʽ����Ϊ\"none\""), Editor(typeof(StrokeStyleEditor), typeof(UITypeEditor)), Category("����")]
        public string ������ʽ
        {
            get
            {
                if (base.svgElement != null)
                {
                    if (base.svgElement.SvgAttributes.ContainsKey("stroke-dasharray"))
                        return base.svgElement.SvgAttributes["stroke-dasharray"].ToString();
                }
                return string.Empty;
            }
            set
            {
                if (base.svgElement != null)
                {
                    string text1 = value;
                    if (text1 == "none")
                    {
                        base.SetAttributeValue("stroke-dasharray", "");
                        return;
                    }
                    if (!(CheckStyle(text1)))
                    {
                        MessageBox.Show("���зǷ��ַ������������ֻ�ո�!", "��Ч������ֵ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    base.SetAttributeValue("stroke-dasharray", text1);
                }
            }
        }

        private bool CheckStyle(string text)
        {
            Regex regex1 = new Regex(@"[\s]+");
            Regex regex2 = new Regex(@"[\D]+");
            string str1 = regex1.Replace(text, "");
            Match match1 = regex2.Match(str1);
            return !match1.Success;
        }

        // Fields
        private static Regex _regex;
    }
}

