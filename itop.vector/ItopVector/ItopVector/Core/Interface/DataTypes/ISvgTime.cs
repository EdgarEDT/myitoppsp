namespace ItopVector.Core.Interface.Types
{
    using System;

    public interface ISvgTime
    {
        // Properties
        ItopVector.Core.Interface.Types.SvgTimeType SvgTimeType { get; set; }

        float Value { get; }

        string ValueAsStr { get; set; }

    }
}

