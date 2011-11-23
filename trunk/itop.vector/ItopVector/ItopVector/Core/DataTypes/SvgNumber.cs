namespace ItopVector.Core.Types
{
    using ItopVector.Core.Func;
    using System;
    using System.Text.RegularExpressions;

    public class SvgNumber
    {
        // Methods
        static SvgNumber()
        {
            SvgNumber.numberPattern = @"(?<number>(\+|-)?\d*\.?\d+((e|E)(\+|-)?\d+)?)";
            SvgNumber.reNumber = new Regex("^" + SvgNumber.numberPattern + "$");
        }

        public SvgNumber(float val)
        {
            this.value = val;
        }

        public SvgNumber(string str)
        {
            this.value = ItopVector.Core.Func.Number.ParseFloatStr(str);
        }


        // Properties
        public float Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }


        // Fields
        internal static string numberPattern;
        internal static Regex reNumber;
        private float value;
    }
}

