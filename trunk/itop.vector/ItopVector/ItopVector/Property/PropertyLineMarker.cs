namespace ItopVector.Property
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using ItopVector.Core.Figure;
    using ItopVector.Design;
    using ItopVector.Core.Func;
    using System.Windows.Forms;

    internal class PropertyLineMarker : PropertyLine
    {
        // Methods
        public PropertyLineMarker(SvgElement render)
            : base(render)
        {
        }

        private void AddMarker(Marker svgElement, string attributeName, bool isendarrow)
        {
            if (svgElement != null)
            {
                SvgDocument fadde1 = base.svgElement.OwnerDocument;
                bool flag1 = fadde1.AcceptChanges;
                string text1 = svgElement.GetAttribute("id").Trim();
                string text2 = string.Empty;
                if (text1.Length > 0)
                {
                    if (isendarrow)
                    {
                        text1 = "end" + text1;
                    }
                    else
                    {
                        text1 = "start" + text1;
                    }
                }
                text2 = "url(#" + text1 + ")";
                if (text1.Length == 0)
                {
                    text2 = string.Empty;
                }
                string text3 = (base.svgElement as SvgElement).GetAttribute(attributeName);
                if (text2 != text3)
                {
                    string[] textArray1 = new string[] { "marker" };
                    Marker cfb1 = NodeFunc.GetRefNode(text1, fadde1) as Marker;
                    if (cfb1 == null)
                    {
                        cfb1 = svgElement;

                        if (isendarrow)
                        {
                            //fadde1.AcceptChanges = false;
                            for (int num1 = 0; num1 < cfb1.GraphList.Count; num1++)
                            {
                                SvgElement facbce1 = cfb1.GraphList[num1] as SvgElement;
                                if (facbce1 != null)
                                {
                                    string text4 = facbce1.GetAttribute("transform");
                                    text4 = "matrix(-1,0,0,1,0,0) " + text4;
                                    facbce1.SetAttribute("transform", text4);

                                    text4 = null;
                                }
                            }
                        }
                        cfb1.SetAttribute("id", text1);
                        fadde1.AddDefsElement(cfb1);

                    }

                    AttributeFunc.SetAttributeValue(base.svgElement, attributeName, text2);
                }
                fadde1.AcceptChanges = flag1;
            }
        }


        // Properties
        [Description("指定对象的开始箭头"), Browsable(true), Category("箭头"), Editor(typeof(ArrowEditor), typeof(UITypeEditor))]
        public Struct.PropertyLineMarker 开始箭头
        {
            get
            {
                if (base.svgElement != null)
                {
                    string str1 = base.svgElement.GetAttribute("marker-start");
                    Marker cfb1 = NodeFunc.GetRefNode(str1, base.svgElement.OwnerDocument) as Marker;
                    if (cfb1 != null)
                    {
                        cfb1 = cfb1.Clone() as Marker;
                        cfb1.GraphTransform.Matrix.Reset();
                        return new Struct.PropertyLineMarker(null, cfb1, false, cfb1.Id);
                    }
                }
                return new Struct.PropertyLineMarker(null, null, false, string.Empty);
            }
            set
            {

                if (base.svgElement != null)
                {
                    base.svgElement.OwnerDocument.AcceptChanges = true;
                    Arrow arrow1 = value.Arrow;
                    if (arrow1 == null)
                    {

                        (base.svgElement as SvgElement).RemoveAttribute("marker-start");
                    }
                    else
                    {
                        this.AddMarker(arrow1.SvgElement as Marker, "marker-start", false);

                    }

                    base.svgElement.OwnerDocument.AcceptChanges = false;
                }
            }
        }

        [Editor(typeof(ArrowEditor), typeof(UITypeEditor)), Category("箭头"), Description("指定对象的结束箭头"), Browsable(true)]
        public Struct.PropertyLineMarker 结束箭头
        {
            get
            {
                if (base.svgElement != null)
                {
                    string str1 = base.svgElement.GetAttribute("marker-end");
                    Marker cfb1 = (NodeFunc.GetRefNode(str1, base.svgElement.OwnerDocument) as Marker);


                    if (cfb1 != null)
                    {
                        cfb1 = cfb1.Clone() as Marker;
                        cfb1.GraphTransform.Matrix.Reset();
                        return new Struct.PropertyLineMarker(null, cfb1, true, cfb1.Id);
                    }
                }
                return new Struct.PropertyLineMarker(null, null, true, string.Empty);
            }
            set
            {
                if (base.svgElement != null)
                {
                    base.svgElement.OwnerDocument.AcceptChanges = true;
                    Arrow arrow1 = value.Arrow;
                    if (arrow1 == null)
                    {

                        (base.svgElement as SvgElement).RemoveAttribute("marker-end");
                    }
                    else
                    {
                        this.AddMarker(arrow1.SvgElement as Marker, "marker-end", true);

                    }

                    base.svgElement.OwnerDocument.AcceptChanges = false;
                }
            }
        }


        [Description("指定绘制标记文本的颜色"), Category("标记"), Browsable(false)]
        public Color 文本颜色
        {
            get
            {
                return Color.Black;
            }
            set
            {
                //                if (base.svgElement!=null)
                //                {
                //                    string text1 = ParseColor.Parse(value);
                //                    base.svgElement.SetAttribute("labelColor", text1);
                //                    text1 = null;
                //                }
            }
        }

    }
}

