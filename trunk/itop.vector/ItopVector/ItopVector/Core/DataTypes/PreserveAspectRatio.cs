namespace ItopVector.Core.Types
{
    using System;

    public class PreserveAspectRatio
    {
        // Methods
        public PreserveAspectRatio()
        {
            this.Align = parAlign.none;
            this.Mos = parMeetOrSlice.meet;
        }

        public PreserveAspectRatio(parAlign align, parMeetOrSlice mos)
        {
            this.Align = align;
            this.Mos = mos;
        }

        public override string ToString()
        {
            string[] textArray1 = new string[5] { " preserveasectRatio=\"", this.Align.ToString(), " ", this.Mos.ToString(), "\"" } ;
            return string.Concat(textArray1);
        }


        // Fields
        public parAlign Align;
        public parMeetOrSlice Mos;
    }
}

