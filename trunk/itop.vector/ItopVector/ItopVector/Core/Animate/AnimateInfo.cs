namespace ItopVector.Core.Animate
{
    using System;
    using System.Collections;

    public class AnimateInfo
    {
        // Methods
        public AnimateInfo()
        {
            this.Values = new ArrayList(0x10);
            this.Times = new ArrayList(0x10);
            this.Adds = new ArrayList(0x10);
        }

        public void Add(object result, int time, bool add)
        {
            int num1 = 0;
            foreach (int num2 in this.Times)
            {
                if (time < num2)
                {
                    break;
                }
                num1++;
            }
            if (num1 >= this.Times.Count)
            {
                this.Values.Add(result);
                this.Times.Add(time);
                this.Adds.Add(add);
            }
            else
            {
                this.Values.Insert(num1, result);
                this.Times.Insert(num1, time);
                this.Adds.Insert(num1, add);
            }
        }


        // Properties
        public object[] AnimateValues
        {
            get
            {
                object[] objArray1 = new object[this.Values.Count];
                this.Values.CopyTo(objArray1);
                return objArray1;
            }
        }

        public bool[] ValueAdds
        {
            get
            {
                bool[] flagArray1 = new bool[this.Adds.Count];
                this.Adds.CopyTo(flagArray1);
                return flagArray1;
            }
        }


        // Fields
        private ArrayList Adds;
        private ArrayList Times;
        private ArrayList Values;
    }
}

