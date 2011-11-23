namespace ItopVector.Core.Document
{
    using ItopVector.Core;
    using ItopVector.Core.Animate;
    using ItopVector.Core.ClipAndMask;
    using ItopVector.Core.Interface;
    using ItopVector.Core.Interface.Figure;
    using System;

    public class SerializeDocument
    {
        // Methods
        public SerializeDocument(SvgDocument doc)
        {
            this.svgDocument = null;
            this.flowChilds = new SvgElementCollection();
            this.filterType = typeof(SvgElement);
            this.svgDocument = doc;
        }

        public void AddElement(SvgElement element)
        {
            if (!this.flowChilds.Contains(element))
            {
                this.flowChilds.Add(element);
            }
            if (element.ShowParticular)
            {
                this.flowChilds.AddRange(element.AnimateList);
            }
            if (element is IGraph)
            {
                ClipPath path1 = ((IGraph) element).ClipPath;
                if (path1 != null)
                {
                    this.AddElement(path1);
                }
            }
            if ((element is IContainer) && element.ShowChilds)
            {
                SvgElementCollection.ISvgElementEnumerator enumerator1 = ((IContainer) element).ChildList.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    SvgElement element1 = (SvgElement) enumerator1.Current;
                    this.AddElement(element1);
                }
            }
        }

        public void FlowDocument()
        {
            this.flowChilds.Clear();
            SvgElement element1 = this.svgDocument.RootElement;
            if (element1 != null)
            {
                this.AddElement(element1);
            }
        }

        public void Insert(int index, SvgElement element)
        {
            if (!this.flowChilds.Contains(element))
            {
                this.flowChilds.Insert(index, element);
            }
            index++;
            if (element.ShowParticular)
            {
                SvgElementCollection.ISvgElementEnumerator enumerator1 = element.AnimateList.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    ItopVector.Core.Animate.Animate animate1 = (ItopVector.Core.Animate.Animate) enumerator1.Current;
                    if (!this.flowChilds.Contains(animate1))
                    {
                        this.flowChilds.Insert(index, animate1);
                        index++;
                    }
                }
            }
            if (element is IGraph)
            {
                ClipPath path1 = ((IGraph) element).ClipPath;
                if (path1 != null)
                {
                    this.Insert(index, path1);
                }
            }
            if (element is IContainer)
            {
                SvgElementCollection.ISvgElementEnumerator enumerator2 = ((IContainer) element).ChildList.GetEnumerator();
                while (enumerator2.MoveNext())
                {
                    SvgElement element1 = (SvgElement) enumerator2.Current;
                    this.Insert(index, element1);
                }
            }
        }

        public void RemoveElement(SvgElement element)
        {
            if (this.flowChilds.Contains(element))
            {
                this.flowChilds.Remove(element);
            }
            if (element is IContainer)
            {
                SvgElementCollection.ISvgElementEnumerator enumerator1 = ((IContainer) element).ChildList.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    SvgElement element1 = (SvgElement) enumerator1.Current;
                    this.RemoveElement(element1);
                }
            }
            if ((element is IGraph) && (((IGraph) element).ClipPath != null))
            {
                this.RemoveElement(((IGraph) element).ClipPath);
            }
            SvgElementCollection.ISvgElementEnumerator enumerator2 = element.AnimateList.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                ItopVector.Core.Animate.Animate animate1 = (ItopVector.Core.Animate.Animate) enumerator2.Current;
                if (this.flowChilds.Contains(animate1))
                {
                    this.flowChilds.Remove(animate1);
                }
            }
        }

        public void UpdateElementChilds(SvgElement element, bool oldexpand, SvgElementCollection list)
        {
            if (oldexpand)
            {
                SvgElementCollection.ISvgElementEnumerator enumerator1 = list.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    SvgElement element1 = (SvgElement) enumerator1.Current;
                    this.RemoveElement(element1);
                }
            }
            else
            {
                int num1 = this.flowChilds.IndexOf(element);
                if ((num1 + 1) < this.flowChilds.Count)
                {
                    for (int num2 = list.Count - 1; num2 >= 0; num2--)
                    {
                        this.Insert(num1 + 1, (SvgElement) list[num2]);
                    }
                }
                else
                {
                    SvgElementCollection.ISvgElementEnumerator enumerator2 = list.GetEnumerator();
                    while (enumerator2.MoveNext())
                    {
                        SvgElement element2 = (SvgElement) enumerator2.Current;
                        this.AddElement(element2);
                    }
                }
            }
        }


        // Properties
        public Type FilterType
        {
            set
            {
                this.filterType = value;
            }
        }

        public SvgElementCollection FlowChilds
        {
            get
            {
                return this.flowChilds;
            }
        }


        // Fields
        private Type filterType;
        private SvgElementCollection flowChilds;
        private SvgDocument svgDocument;
    }
}

