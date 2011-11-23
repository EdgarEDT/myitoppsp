namespace ItopVector.Core.UnDo
{
    using System;

    public class StackUndoOperation : IUndoOperation
    {
        // Methods
        public StackUndoOperation()
        {
            this.UndoStack = null;
            this.dispose = false;
        }

        public void Redo()
        {
            if (this.UndoStack != null)
            {
                this.UndoStack.Redo();
            }
        }

        public void Undo()
        {
            if (this.UndoStack != null)
            {
                this.UndoStack.Undo();
            }
        }


        // Fields
        public bool dispose;
        public ItopVector.Core.UnDo.UndoStack UndoStack;
    }
}

