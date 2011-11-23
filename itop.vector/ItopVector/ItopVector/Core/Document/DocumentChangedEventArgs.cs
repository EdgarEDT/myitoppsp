namespace ItopVector.Core.Document
{
    using ItopVector.Core.Interface;
    using System;

    public class DocumentChangedEventArgs
    {
        // Methods
        public DocumentChangedEventArgs()
        {
            this.ChangeAction = ItopVector.Core.Document.ChangeAction.None;
        }


        // Fields
        public ItopVector.Core.Document.ChangeAction ChangeAction;
        public ISvgElement[] ChangeElements;
    }
}

