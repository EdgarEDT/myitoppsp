namespace ItopVector.Core.UnDo
{
    using ItopVector.Core;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public class UndoStack
    {
        // Events
        public event EventHandler ActionRedone;
        public event EventHandler ActionUndone;

        // Methods
        public UndoStack()
        {
            this.undostack = new Stack();
            this.redostack = new Stack();
            this.tempstack = null;
            this.subundo = false;
            this.AcceptChanges = false;
            this.Dispose = false;
        }

        public void ClearAll()
        {
            this.undostack.Clear();
            this.redostack.Clear();
        }

        public void ClearRedoStack()
        {
            this.redostack.Clear();
        }

        protected void OnActionRedone()
        {
            if (this.ActionRedone != null)
            {
                this.ActionRedone(null, null);
            }
        }

        protected void OnActionUndone()
        {
            if (this.ActionUndone != null)
            {
                this.ActionUndone(null, null);
            }
        }

        public void Push(IUndoOperation[] operations)
        {
            if (operations == null)
            {
                throw new ArgumentNullException("¿Õ¶ÑÕ»£¡");
            }
            if (this.AcceptChanges)
            {
                this.AcceptChanges = false;
                this.undostack.Push(operations);
                this.ClearRedoStack();
                this.AcceptChanges = true;
            }
        }

        public SvgElement[] Redo()
        {
            ArrayList list1 = new ArrayList(0x10);
            if (this.redostack.Count > 0)
            {
                IUndoOperation[] operationArray1 = (IUndoOperation[]) this.redostack.Pop();
                this.AcceptChanges = false;
                IUndoOperation[] operationArray2 = operationArray1;
                for (int num1 = 0; num1 < operationArray2.Length; num1++)
                {
                    IUndoOperation operation1 = operationArray2[num1];
                    SvgElement element1 = null;
                    if (operation1 is UnDoOperation)
                    {
                        element1 = ((UnDoOperation) operation1).changeParent;
                    }
                    else if (operation1 is KeyUndoOperation)
                    {
                        element1 = ((KeyUndoOperation) operation1).ChangeElement;
                    }
                    if (!list1.Contains(element1))
                    {
                        list1.Add(element1);
                    }
                    operation1.Redo();
                }
                Array.Reverse(operationArray1);
                this.undostack.Push(operationArray1);
                this.OnActionRedone();
                this.AcceptChanges = true;
            }
            if (list1.Count > 0)
            {
                SvgElement[] elementArray1 = new SvgElement[list1.Count];
                list1.CopyTo(elementArray1);
                return elementArray1;
            }
            return null;
        }

        public SvgElement[] Undo()
        {
            ArrayList list1 = new ArrayList(0x10);
            if (this.undostack.Count > 0)
            {
                IUndoOperation[] operationArray1 = (IUndoOperation[]) this.undostack.Pop();
                this.AcceptChanges = false;
                IUndoOperation[] operationArray2 = operationArray1;
                for (int num1 = 0; num1 < operationArray2.Length; num1++)
                {
                    IUndoOperation operation1 = operationArray2[num1];
                    SvgElement element1 = null;
                    if (operation1 is UnDoOperation)
                    {
                        element1 = ((UnDoOperation) operation1).changeParent;
                    }
                    else if (operation1 is KeyUndoOperation)
                    {
                        element1 = ((KeyUndoOperation) operation1).ChangeElement;
                    }
                    if (!list1.Contains(element1))
                    {
                        list1.Add(element1);
                    }
                    operation1.Undo();
                }
                Array.Reverse(operationArray1);
                this.redostack.Push(operationArray1);
                this.OnActionUndone();
                this.AcceptChanges = true;
            }
            if (list1.Count > 0)
            {
                SvgElement[] elementArray1 = new SvgElement[list1.Count];
                list1.CopyTo(elementArray1);
                return elementArray1;
            }
            return null;
        }


        // Properties
        internal Stack _UndoStack
        {
            get
            {
                return this.undostack;
            }
        }

        public bool CanRedo
        {
            get
            {
                if (this.subundo)
                {
                    if (this.tempstack != null)
                    {
                        return this.tempstack.CanRedo;
                    }
                    this.subundo = false;
                }
                this.subundo = false;
                return (this.redostack.Count > 0);
            }
        }

        public bool CanUndo
        {
            get
            {
                if ((this.subundo && (this.tempstack != null)) && !this.tempstack.Dispose)
                {
                    return this.tempstack.CanUndo;
                }
                this.subundo = false;
                return (this.undostack.Count > 0);
            }
        }


        // Fields
        public bool AcceptChanges;
        public bool Dispose;
        private Stack redostack;
        private bool subundo;
        private UndoStack tempstack;
        private Stack undostack;
    }
}

