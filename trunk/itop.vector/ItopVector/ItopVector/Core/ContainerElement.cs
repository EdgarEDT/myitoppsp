namespace ItopVector.Core
{
    using ItopVector.Core.ClipAndMask;
    using ItopVector.Core.Document;
    using ItopVector.Core.Figure;
    using ItopVector.Core.Interface;
    using System;
    using System.Xml;

    public class ContainerElement : SvgElement, IContainer
    {
        // Methods
        internal ContainerElement(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
            this.expand = true;
        }

        private void AddFlowElement(SvgElement element)
        {
            if (!(element is Text) || !((Text) element).EditMode)
            {
                int num1 = base.OwnerDocument.FlowChilds.IndexOf(this);
                if ((num1 < 0) || !this.Expand)
                {
                    return;
                }
                XmlNode node1 = this.NextSibling;
                while ((node1 != null) && !(node1 is SvgElement))
                {
                    node1 = node1.NextSibling;
                }
                if ((node1 != null) || !(this is ClipPath))
                {
                    if (node1 is SvgElement)
                    {
                        num1 = base.OwnerDocument.FlowChilds.IndexOf((SvgElement) node1);
                        num1 = Math.Max(0, num1);
                        base.OwnerDocument.InsertFlowElement(num1, element);
                    }
                    else
                    {
                        base.OwnerDocument.AddFlowElement(element);
                    }
                    return;
                }
                if (((num1 - 1) < 0) || (num1 >= base.OwnerDocument.FlowChilds.Count))
                {
                    base.OwnerDocument.AddFlowElement(element);
                    return;
                }
                SvgElement element1 = (SvgElement) base.OwnerDocument.FlowChilds[num1 - 1];
                node1 = element1.NextSibling;
                while ((node1 != null) && !(node1 is SvgElement))
                {
                    node1 = node1.NextSibling;
                }
                if (node1 is SvgElement)
                {
                    num1 = base.OwnerDocument.FlowChilds.IndexOf((SvgElement) node1);
                    if (num1 >= 0)
                    {
                        base.OwnerDocument.FlowChilds.Insert(num1, element);
                        goto Label_01AA;
                    }
                    base.OwnerDocument.FlowChilds.Add(element);
                    return;
                }
                if (element1 is ContainerElement)
                {
                    if (((ContainerElement) element1).ChildList.Count > 0)
                    {
                        num1 = base.OwnerDocument.FlowChilds.IndexOf(((ContainerElement) element1).ChildList[0]);
                        if (num1 >= 0)
                        {
                            base.OwnerDocument.FlowChilds.Insert(num1, element);
                            goto Label_01AA;
                        }
                        base.OwnerDocument.FlowChilds.Add(element);
                        goto Label_01AA;
                    }
                    base.OwnerDocument.FlowChilds.Add(element);
                    return;
                }
                base.OwnerDocument.AddFlowElement(element);
            }
            return;
        Label_01AA:
            return;
        }

        public override XmlNode AppendChild(XmlNode newChild)
        {
            XmlNode node1 = base.AppendChild(newChild);
            if (this.IsValidChild(node1))
            {
                if (!this.ChildList.Contains((SvgElement) node1))
                {
                    this.AddFlowElement((SvgElement) node1);
                    this.ChildList.Add((SvgElement) node1);
                }
                if (this is ClipPath)
                {
                    ((ClipPath) this).UpdateChild(node1);
                }
            }
            return node1;
        }

        public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
        {
            XmlNode node1 = base.InsertAfter(newChild, refChild);
            if (this.IsValidChild(node1))
            {
                XmlNode node2 = refChild;
                while (!this.IsValidChild(node2))
                {
                    if (node2 == null)
                    {
                        break;
                    }
                    node2 = node2.PreviousSibling;
                }
                int num1 = -1;
                if (node2 is SvgElement)
                {
                    num1 = this.ChildList.IndexOf((SvgElement) node2);
                }
                num1 = Math.Min((int) (num1 + 1), this.ChildList.Count);
                if (!this.ChildList.Contains((SvgElement) node1))
                {
                    if (num1 < this.ChildList.Count)
                    {
                        this.ChildList.Insert(num1, (SvgElement) node1);
                        this.InsertFlowElment(num1, (SvgElement) node1);
                    }
                    else
                    {
                        this.ChildList.Add((SvgElement) node1);
                        this.AddFlowElement((SvgElement) node1);
                    }
                }
                if (this is ClipPath)
                {
                    ((ClipPath) this).UpdateChild(node1);
                }
            }
            return node1;
        }

        public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
        {
            XmlNode node1 = base.InsertBefore(newChild, refChild);
            if (this.IsValidChild(node1))
            {
                XmlNode node2 = refChild;
                while (!this.IsValidChild(node2))
                {
                    if (node2 == null)
                    {
                        break;
                    }
                    node2 = node2.NextSibling;
                }
                int num1 = -1;
                if (node2 is SvgElement)
                {
                    num1 = this.ChildList.IndexOf((SvgElement) node2);
                }
                if (!this.ChildList.Contains((SvgElement) node1))
                {
                    num1 = Math.Max(0, num1);
                    this.ChildList.Insert(num1, (SvgElement) node1);
                    this.InsertFlowElment(num1, (SvgElement) node1);
                }
                if (this is ClipPath)
                {
                    ((ClipPath) this).UpdateChild(node1);
                }
            }
            return node1;
        }

        private void InsertFlowElment(int index, SvgElement element)
        {
            if (!(element is Text) || !((Text) element).EditMode)
            {
                int num1 = base.OwnerDocument.FlowChilds.IndexOf(this);
                if (this.Expand && (num1 >= 0))
                {
                    num1 += (index + 1);
                    if ((num1 >= 0) && (num1 < base.OwnerDocument.FlowChilds.Count))
                    {
                        base.OwnerDocument.InsertFlowElement(num1, element);
                    }
                    else
                    {
                        base.OwnerDocument.AddFlowElement(element);
                    }
                }
            }
        }

        public virtual bool IsValidChild(XmlNode element)
        {
            return true;
        }

        public override XmlNode PrependChild(XmlNode newChild)
        {
            XmlNode node1 = base.PrependChild(newChild);
            if (this.IsValidChild(node1))
            {
                int num1 = base.OwnerDocument.FlowChilds.IndexOf(this);
                if (!this.ChildList.Contains((SvgElement) node1))
                {
                    this.ChildList.Insert(0, (SvgElement) node1);
                }
                if (this.Expand)
                {
                    this.InsertFlowElment(0, (SvgElement) node1);
                }
                if (this is ClipPath)
                {
                    ((ClipPath) this).UpdateChild(node1);
                }
            }
            return node1;
        }

        public override void RemoveAll()
        {
            base.RemoveAll();
            this.ChildList.Clear();
            SvgElementCollection.ISvgElementEnumerator enumerator1 = this.ChildList.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                SvgElement element1 = (SvgElement) enumerator1.Current;
                base.OwnerDocument.RemoveFlowElement(element1);
            }
        }

        public override XmlNode RemoveChild(XmlNode oldChild)
        {
            XmlNode node1 = base.RemoveChild(oldChild);
            if (this.IsValidChild(node1))
            {
                if (this.ChildList.Contains((SvgElement) node1))
                {
                    this.ChildList.Remove((SvgElement) node1);
                }
                base.OwnerDocument.RemoveFlowElement((SvgElement) node1);
            }
            return node1;
        }


        // Properties
        public virtual SvgElementCollection ChildList
        {
            get
            {
                return new SvgElementCollection();
            }
        }

        public bool Expand
        {
            get
            {
                return this.expand;
            }
            set
            {
                if (this.expand != value)
                {
                    bool flag1 = this.expand;
                    this.expand = value;
                    base.OwnerDocument.ChangeElementExpand(this, flag1, false);
                }
            }
        }


        // Fields
        private bool expand;
    }
}

