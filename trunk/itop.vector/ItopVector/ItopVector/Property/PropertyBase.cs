namespace ItopVector.Property
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using ItopVector.Core;
    using ItopVector.Core.Figure;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Core.Func;
    using ItopVector.Design;
    using System.Drawing.Design;
    using System.Xml;

    internal class PropertyBase //
    {
        // Methods
        public PropertyBase(SvgElement render)
        {
            //this.svgElement = null;
            this.vectorControl = null;
            this.svgElement = render;
        }
        protected void SetAttributeValue(string attributeName, string attributeValue)
        {

            if ((svgElement as IGraph).IsLock) return;
            this.svgElement.OwnerDocument.AcceptChanges = true;

            this.svgElement.OwnerDocument.ChangeElements.Add(svgElement);

            bool flag1 = false;
            //			if(attributeName=="fill" || attributeName=="stroke")
            {
                if (this.svgElement is IGraphPath)
                {
                    flag1 = true;
                    (this.svgElement as IGraphPath).UpdateAttribute = false;
                }
            }

            AttributeFunc.SetAttributeValue(svgElement, attributeName, attributeValue);

            this.svgElement.OwnerDocument.NotifyUndo();


            if (flag1)
            {
                (this.svgElement as IGraphPath).UpdateAttribute = true;
            }
        }

        // Properties
        [Browsable(false)]
        public SvgElement SvgElement
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

        public ItopVector.ItopVectorControl VectorControl
        {
            set
            {
                this.vectorControl = value;
            }
        }

        [Category("常规"), Browsable(true), Description("图元的唯一编号"), ReadOnly(true)]
        public string ID
        {
            get
            {
                if (this.svgElement != null)
                {
                    return this.svgElement.GetAttribute("id");
                }
                return string.Empty;
            }
        }
        [Category("常规"), Browsable(true), Editor(typeof(LabelTextEditor), typeof(UITypeEditor)), Description("元件的名称")]
        public string 名称
        {
            get
            {
                string text1 = string.Empty;
                if (svgElement.SvgAttributes.ContainsKey("info-name"))
                {
                    text1 = svgElement.SvgAttributes["info-name"].ToString();
                }
                return text1;
            }
            set
            {
                string text1 = value.ToString();
                AttributeFunc.SetAttributeValue(svgElement, "info-name", text1);

            }
        }
        [Category("常规"), Browsable(true), Editor(typeof(LayerEdit), typeof(UITypeEditor)), Description("元件所在图层")]
        public SvgElement 图层
        {
            get
            {
                if (svgElement == null) return null;
                string text1 = svgElement.GetAttribute("layer");
                if (text1 == string.Empty)
                {
                    return svgElement;
                }
                XmlNode node = svgElement.OwnerDocument.SelectSingleNode("//*[@id='" + text1 + "']");
                if (node is SvgElement)
                {
                    return node as SvgElement;
                }
                return svgElement;
            }
            set
            {
                if (!(value is Layer)) return;

                (svgElement as IGraph).Layer = value as Layer;
            }
        }
        public bool 加锁
        {
            get
            {
                return (svgElement as IGraph).IsLock;

            }
            set
            {

                (svgElement as IGraph).IsLock = value;

            }

        }

        // Fields
        protected ItopVector.ItopVectorControl vectorControl;
        protected SvgElement svgElement;
    }
}

