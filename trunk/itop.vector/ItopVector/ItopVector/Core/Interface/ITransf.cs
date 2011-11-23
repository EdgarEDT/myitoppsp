namespace ItopVector.Core.Interface
{
    using System;
    using System.Drawing.Drawing2D;

	/// <summary>
	/// 二维变换接口
	/// </summary>
    public interface ITransf
    {
        // Methods
        void setMatrix(System.Drawing.Drawing2D.Matrix matrix);

        void setRotate(float angle, float cx, float cy);

        void setScale(float sx, float sy);

        void setSkewX(float angle);

        void setSkewY(float angle);

        void setTranslate(float tx, float ty);


        // Properties
        float Angle { get; }

        System.Drawing.Drawing2D.Matrix Matrix { get; }

        short Type { get; }

    }
}

