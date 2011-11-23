namespace ItopVector.DrawArea
{
    using System;
    using System.Windows.Forms;

    public interface IOperation
    {
        // Methods
        void Dispose();

        void OnMouseDown(MouseEventArgs e);

        void OnMouseMove(MouseEventArgs e);
     
        void OnMouseUp(MouseEventArgs e);

		void OnMouseWheel(MouseEventArgs e);

        void OnPaint(PaintEventArgs e);

        bool ProcessDialogKey(Keys keydata);

        bool Redo();

        bool Undo();


        // Properties
        bool CanRedo { get; }

        bool CanUndo { get; }

    }
}

