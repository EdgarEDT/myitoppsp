namespace ItopVector.Core
{
    using ItopVector.Core.Interface;
    using System;

    public class CollectionChangedEventArgs
    {
        // Methods
        public CollectionChangedEventArgs(ISvgElement element)
        {
            ISvgElement[] elementArray1 = new ISvgElement[1] { element } ;
            this.ChangeElements = elementArray1;
        }

        public CollectionChangedEventArgs(SvgElementCollection list)
        {
            if (list != null)
            {
                this.ChangeElements = new ISvgElement[list.Count];
                list.CopyTo(this.ChangeElements, 0);
            }
        }

        public CollectionChangedEventArgs(ISvgElement[] list)
        {
            this.ChangeElements = list;
        }


        // Fields
        public ISvgElement[] ChangeElements;
    }
}

