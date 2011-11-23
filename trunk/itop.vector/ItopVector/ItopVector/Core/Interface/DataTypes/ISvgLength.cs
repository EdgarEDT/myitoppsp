namespace ItopVector.Core.Interface.Types
{
    using System;

    public interface ISvgLength
    {
        // Methods
        void ConvertToSpecifiedUnits(SvgLengthType unitType);

        void NewValueSpecifiedUnits(SvgLengthType unitType, float valueInSpecifiedUnits);


        // Properties
        SvgLengthType UnitType { get; set; }

        float Value { get; set; }

        string ValueAsString { get; set; }

        float ValueInSpecifiedUnits { get; set; }

    }
}

