namespace ItopVector.Core.UnDo
{
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using System;
    using System.Xml;

    public class UnDoOperation : IUndoOperation
    {
        // Methods
        public UnDoOperation(SvgDocument doc, XmlNode changeelement, XmlNode oldparent, XmlNode newparent, XmlNodeChangedAction action, SvgElement changeparent)
        {
            this.oldvalue = string.Empty;
            this.newvalue = string.Empty;
            this.changeParent = changeparent;
            this.document = doc;
            this.changeAction = action;
            this.oldParent = oldparent;
            this.newParent = newparent;
            this.changeElement = changeelement;
            this.nextSibling = this.changeElement.NextSibling;
            this.preSibling = this.changeElement.PreviousSibling;
            if (this.changeElement is SvgElement)
            {
                while (this.nextSibling != null)
                {
                    if (this.nextSibling is SvgElement)
                    {
                        break;
                    }
                    this.nextSibling = this.nextSibling.NextSibling;
                }
                while (this.preSibling != null)
                {
                    if (this.preSibling is SvgElement)
                    {
                        break;
                    }
                    this.preSibling = this.preSibling.PreviousSibling;
                }
            }
            if (this.changeElement != null)
            {
                this.newvalue = this.changeElement.Value;
            }
            if (this.changeParent != null)
            {
                this.oldvalue = this.changeParent.BeforeChangeValueStr;
            }
        }

        public void Redo()
        {
            switch (this.changeAction)
            {
                case XmlNodeChangedAction.Insert:
                {
                    if (this.changeElement.NodeType != XmlNodeType.Element)
                    {
                        if ((this.changeElement.NodeType == XmlNodeType.Attribute) && (this.newParent is SvgElement))
                        {
                            ((SvgElement) this.newParent).SetAttribute(((XmlAttribute) this.changeElement).Name, ((XmlAttribute) this.changeElement).Value);
                        }
                        break;
                    }
                    if (this.nextSibling == null)
                    {
                        if (this.preSibling != null)
                        {
                            this.newParent.InsertAfter(this.changeElement, this.preSibling);
                            break;
                        }
                        this.newParent.AppendChild(this.changeElement);
                        break;
                    }
                    this.newParent.InsertBefore(this.changeElement, this.nextSibling);
                    break;
                }
                case XmlNodeChangedAction.Remove:
                {
                    if (!(this.changeElement is XmlAttribute))
                    {
                        this.oldParent.RemoveChild(this.changeElement);
                        break;
                    }
                    this.oldParent.Attributes.Remove((XmlAttribute) this.changeElement);
                    break;
                }
                case XmlNodeChangedAction.Change:
                {
                    if (this.newParent.NodeType == XmlNodeType.Attribute)
                    {
                        XmlAttribute attribute1 = (XmlAttribute) this.newParent;
                        if (this.changeParent != null)
                        {
                            this.changeParent.SetAttribute(attribute1.LocalName, attribute1.NamespaceURI, this.newvalue);
                            this.changeParent.pretime = -1;
                        }
                        ((XmlAttribute) this.newParent).Value = this.newvalue;
                    }
                    break;
                }
            }
            if (this.newParent is SvgElement)
            {
                ((SvgElement) this.newParent).pretime = -1;
            }
            if (this.oldParent is SvgElement)
            {
                ((SvgElement) this.oldParent).pretime = -1;
            }
            if (this.changeElement is SvgElement)
            {
                ((SvgElement) this.changeElement).pretime = -1;
            }
        }

        public void Undo()
        {
            switch (this.changeAction)
            {
                case XmlNodeChangedAction.Insert:
                {
                    if (this.changeElement.NodeType != XmlNodeType.Element)
                    {
                        if ((this.changeElement.NodeType == XmlNodeType.Attribute) && (this.newParent is XmlElement))
                        {
                            XmlAttribute attribute1 = (XmlAttribute) this.changeElement;
                            ((XmlElement) this.newParent).RemoveAttributeNode(attribute1.LocalName, attribute1.NamespaceURI);
                        }
                        break;
                    }
                    if (this.changeElement.ParentNode == this.newParent)
                    {
                        this.newParent.RemoveChild(this.changeElement);
                    }
                    break;
                }
                case XmlNodeChangedAction.Remove:
                {
                    if (!(this.changeElement is XmlAttribute))
                    {
                        if (this.changeElement is XmlElement)
                        {
                            if (this.nextSibling != null)
                            {
                                this.oldParent.InsertBefore(this.changeElement, this.nextSibling);
                                break;
                            }
                            if (this.preSibling != null)
                            {
                                this.oldParent.InsertAfter(this.changeElement, this.preSibling);
                                break;
                            }
                            this.oldParent.AppendChild(this.changeElement);
                        }
                        break;
                    }
                    this.oldParent.Attributes.Append((XmlAttribute) this.changeElement);
                    break;
                }
                case XmlNodeChangedAction.Change:
                {
                    if (this.newParent.NodeType == XmlNodeType.Attribute)
                    {
                        XmlAttribute attribute2 = (XmlAttribute) this.newParent;
                        if (this.changeParent != null)
                        {
                            this.changeParent.SetAttribute(attribute2.LocalName, attribute2.NamespaceURI, this.oldvalue);
                            this.changeParent.pretime = -1;
                        }
                        ((XmlAttribute) this.newParent).Value = this.oldvalue;
                    }
                    break;
                }
            }
            if (this.newParent is SvgElement)
            {
                ((SvgElement) this.newParent).pretime = -1;
            }
            if (this.oldParent is SvgElement)
            {
                ((SvgElement) this.oldParent).pretime = -1;
            }
            if (this.changeElement is SvgElement)
            {
                ((SvgElement) this.changeElement).pretime = -1;
            }
        }


        // Fields
        private XmlNodeChangedAction changeAction;
        private XmlNode changeElement;
        public SvgElement changeParent;
        private SvgDocument document;
        private XmlNode newParent;
        private string newvalue;
        private XmlNode nextSibling;
        private XmlNode oldParent;
        private string oldvalue;
        private XmlNode preSibling;
    }
}

