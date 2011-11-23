namespace ItopVector.Core.UnDo
{
    using ItopVector.Core;
    using System;
    using System.Collections;

    public class KeyUndoOperation : IUndoOperation
    {
        // Methods
        public KeyUndoOperation()
        {
            this.ChangeElement = null;
            this.OldList = null;
            this.NewList = null;
            this.OldControlTime = 0;
            this.NewControlTime = 0;
        }

        public KeyUndoOperation(SvgElement element, ArrayList oldlist, ArrayList newlist)
        {
            this.ChangeElement = null;
            this.OldList = null;
            this.NewList = null;
            this.OldControlTime = 0;
            this.NewControlTime = 0;
            this.ChangeElement = element;
            element.pretime = -1;
            this.OldList = oldlist;
            this.NewList = newlist;
            this.OldControlTime = element.OwnerDocument.OldControlTime;
            this.NewControlTime = element.OwnerDocument.ControlTime;
        }

        public void Redo()
        {
            if ((this.NewList != null) && (this.ChangeElement != null))
            {
                this.ChangeElement.InfoList.Clear();
                this.ChangeElement.InfoList.AddRange(this.NewList);
                this.ChangeElement.OwnerDocument.ControlTime = this.NewControlTime;
            }
        }

        public void Undo()
        {
            if ((this.OldList != null) && (this.ChangeElement != null))
            {
                this.ChangeElement.InfoList.Clear();
                this.ChangeElement.InfoList.AddRange(this.OldList);
                this.ChangeElement.OwnerDocument.ControlTime = this.OldControlTime;
            }
        }


        // Fields
        public SvgElement ChangeElement;
        public int NewControlTime;
        public ArrayList NewList;
        public int OldControlTime;
        public ArrayList OldList;
    }
}

