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
        [Editor(typeof(StrokeWidthEditor), typeof(UITypeEditor)), Category("线条"), Browsable(true), Description("指定绘制图形边缘的线条宽度")]

        public float 线条宽度
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
        [Category("线条"), Browsable(true), Description("元件的名称")]
        public int 线型 {
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
        [Browsable(true), Category("线条"), Description("指定绘制对象边缘线条的颜色")]
        public Color 线条色
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

        [Category("线条"), Description("指定绘制图形边缘的线条透明度，用(0-1)数字表示"), Browsable(true), Editor(typeof(NumberEditor), typeof(UITypeEditor))]
        public Struct.Float 线条透明度
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

        [Browsable(true), Description("制定绘制图形边缘的线条样式，值是一个用空格\" \"分割的数字字符串，如\"3 1 3\"，如果不应用样式，则为\"none\""), Editor(typeof(StrokeStyleEditor), typeof(UITypeEditor)), Category("线条")]
        public string 线条样式
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
                        MessageBox.Show("含有非法字符，请输入数字或空格!", "无效的属性值", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

