namespace ItopVector.Core.Interface.Paint
{
    using ItopVector.Core;
    using ItopVector.Core.Interface;
    using ItopVector.Core.Paint;
    using System;

	/// <summary>
	/// ÌÝ¶È»­Ë¢½Ó¿Ú
	/// </summary>
    public interface IGradientBrush : ITransformBrush
    {
        // Properties
        SpreadMethods SpreadMethod { get; set; }

        SvgElementCollection Stops { get; set; }

    }
}

