namespace ItopVector.Core.Func
{
    using System;
    using System.Drawing.Drawing2D;

    public class TransformFunc
    {
        // Methods
        public TransformFunc()
        {
        }

        public static Matrix RoundMatrix(Matrix matrix, int validNumber)
        {
            return new Matrix((float) Math.Round((double) matrix.Elements[0], validNumber), (float) Math.Round((double) matrix.Elements[1], validNumber), (float) Math.Round((double) matrix.Elements[2], validNumber), (float) Math.Round((double) matrix.Elements[3], validNumber), (float) Math.Round((double) matrix.Elements[4], validNumber), (float) Math.Round((double) matrix.Elements[5], validNumber));
        }

    }
}

