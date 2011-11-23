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
    using ItopVector.Core.Figure;

    [Browsable(true)]
    internal class PropertyPolyline : PropertyLineMarker
    {        

        public PropertyPolyline(SvgElement render)
            : base(render)
        {
        }
        public string ����ͼƬ {
            get {
                Polyline obj = this.svgElement as Polyline;
                if (obj != null)
                    return obj.BackgroundImageFile;
                else
                    return string.Empty;
            }
            set {
                Polyline obj = this.svgElement as Polyline;
                if (obj != null)
                    obj.BackgroundImageFile = value;
            }
        }
        [Category("����"), Browsable(true), Description("���ù�·���ߵ���ɫ")]
        public Color ����ɫ {
            get {
                if (base.svgElement != null) {
                    if (base.svgElement.SvgAttributes.ContainsKey("fill"))
                        return ColorFunc.ParseColor(base.svgElement.SvgAttributes["fill"].ToString());

                }
                return Color.Empty;
            }
            set {
                if (base.svgElement != null) {
                    string text1 = ColorFunc.GetColorString(value);

                    base.SetAttributeValue("fill", text1);
                    text1 = null;
                }
            }
        }
    }
}

