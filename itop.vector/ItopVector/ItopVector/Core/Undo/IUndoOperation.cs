namespace ItopVector.Core.UnDo
{
    using System;

    public interface IUndoOperation
    {
        // Methods
        void Redo();

        void Undo();

    }
}

