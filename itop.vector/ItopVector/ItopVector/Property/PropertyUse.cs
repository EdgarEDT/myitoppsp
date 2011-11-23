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
    using ItopVector.Core.Document;
    using System.IO;

    internal class PropertyUse : PropertyFill
    {
        // Methods
        public PropertyUse(SvgElement render)
            : base(render)
        {

        }
        [Category("����"), Browsable(true), Description("����ͼ�ε���ɫ�������Զ��塰�ǡ�������Ԥ���塰�񡱡�")]
        public bool ʹ���Զ�����ɫ
        {
            get
            {
                if (base.svgElement != null)
                {
                    if (base.svgElement.SvgAttributes.ContainsKey("usestyle"))
                        return base.svgElement.SvgAttributes["usestyle"].ToString() == "true" ? true : false;
                }
                return false;

            }
            set
            {
                if (base.svgElement != null)
                {
                    string text1 = value ? "true" : "false";

                    base.SetAttributeValue("usestyle", text1);
                    text1 = null;
                }

            }
        }
        [Category("ͼģ����"), Browsable(true), Description("����ͼ���Ƿ�����ԭʼ�ߴ��С�����ǡ����������񡱲�������")]
        [DisplayName("������С")]
        public bool Limitsize {
            get {
                Use obj = base.svgElement as Use;
                return obj.LimitSize;
            }
            set {
                Use obj = base.svgElement as Use;
                bool b1 = this.svgElement.OwnerDocument.AcceptChanges;
                this.svgElement.OwnerDocument.AcceptChanges = true;
                obj.LimitSize = value;
                this.svgElement.OwnerDocument.AcceptChanges = b1;
            }
        }
        [Category("ͼģ����"), Browsable(true), Description("����ԭʼͼģ�����Ŵ�С,��Χ(0.1-10)��")]
        [DisplayName("����")]
        public float Scale {
            get {
                Use obj = base.svgElement as Use;
                return obj.Scale;
            }
            set {
                if (value < 0.1 || value>10) return;
                Use obj = base.svgElement as Use;
                bool b1 = this.svgElement.OwnerDocument.AcceptChanges;
                this.svgElement.OwnerDocument.AcceptChanges = true;
                obj.Scale = value;
                this.svgElement.OwnerDocument.AcceptChanges = b1;
            }
        }
        [Category("ͼģ����"), Browsable(true), Editor(typeof(SymbleEditor), typeof(UITypeEditor)), Description("ģ����ϸ��Ϣ��ע����רҵ��Ա��׼�޸Ĵ������ݡ�")]
        [DisplayName("ģ��")]
        public string Symble {
            get {
                Use obj = base.svgElement as Use;
                string text1 = obj.RefElement.InnerXml;
                
                return text1;
            }
            set {
                setSymbol(value);
            }
        }

        private void setSymbol(string xml) {
            string tempfile = Path.GetTempFileName();
            StreamWriter stw = File.CreateText(tempfile);
            stw.AutoFlush = true;

            string hd = "<?xml version=\"1.0\" encoding=\"utf-8\"?><!----><!DOCTYPE svg PUBLIC \"-/W3C/DTD SVG 1.1/EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">" +
                "\r\n<svg id=\"svg\" width=\"" + 400 + "\" height=\"" + 300 + "\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:tonli=\"http://www.tonli.com/tonli\">";
            stw.Write(hd);
            stw.Write(xml);
            stw.Write("</svg>");
            stw.Flush(); stw.Close();
            SvgDocument doc = ItopVector.Core.Document.SvgDocumentFactory.CreateDocumentFromFile(tempfile);
            
            File.Delete(tempfile);
            SVG svg = doc.RootElement as SVG;
            if (svg.ChildList.Count == 0) return;
            Use obj = base.svgElement as Use;
            ItopVector.Core.Figure.Symbol symbol = obj.RefElement as ItopVector.Core.Figure.Symbol;
            bool flag = doc.AcceptChanges;
            doc.AcceptChanges = false;
            for(int i=symbol.ChildList.Count-1;i>=0;i--){
                SvgElement se = symbol.ChildList[i] as SvgElement;                
                symbol.RemoveChild(se);                
            }
            
            foreach (SvgElement se in svg.ChildList) {
                SvgElement se2= symbol.AppendChild(se) as SvgElement;
                se2.RemoveAttribute("layer");
            }
            doc.AcceptChanges = flag;
        }
    }
}

