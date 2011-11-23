namespace ItopVector.DrawArea
{
    using System;
    using System.Windows.Forms;

    internal class FlipOperation : IOperation
    {
        // Methods
        public FlipOperation(MouseArea mc)
        {
            this.mouseAreaControl = null;
            this.mouseAreaControl = mc;
            this.win32 = mc.win32;
        }

        public void Dispose()
        {
            this.mouseAreaControl.FlipOperation = null;
        }
		public void OnMouseWheel(MouseEventArgs e)
		{
			// TODO:  ÃÌº” BezierOperation.DealMouseWheel  µœ÷
		}
        public void OnMouseDown(MouseEventArgs e)
        {
        }

        public void OnMouseMove(MouseEventArgs e)
        {
        }

        public void OnMouseUp(MouseEventArgs e)
        {
        }

        public void OnPaint(PaintEventArgs e)
        {
        }

        public bool ProcessDialogKey(Keys keydate)
        {
            return false;
        }

        public bool Redo()
        {
            return false;
        }

        public bool Undo()
        {
            return false;
        }


        // Properties
        public bool CanRedo
        {
            get
            {
                return false;
            }
        }

        public bool CanUndo
        {
            get
            {
                return false;
            }
        }


        // Fields
        private MouseArea mouseAreaControl;
        private Win32 win32;
    }
}

