namespace ItopVector.Core
{
    using System;

    public class ElementExpandChangedEventArgs
    {
        // Methods
        public ElementExpandChangedEventArgs(SvgElement changeelemnt, SvgElementCollection list, ItopVector.Core.ExpandAction action)
        {
            this.ChangeElement = changeelemnt;
            this.ChangeElements = list;
            this.ExpandAction = action;
        }


        // Fields
        public SvgElement ChangeElement;
        public SvgElementCollection ChangeElements;
        public ItopVector.Core.ExpandAction ExpandAction;
    }
}

