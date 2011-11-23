namespace ItopVector.Core.Func
{
    using System;
    using System.Collections;

    public class ToHexNumber
    {
        // Methods
        public ToHexNumber()
        {
            this.numberList = new ArrayList();
        }

        public int ToDecimal(string Number)
        {
            int num1 = 0;
            for (int num2 = 0; num2 < Number.Length; num2++)
            {
                string text2;
                string text1 = Number.Substring(num2, 1);
                if ((text2 = text1) == null)
                {
                    goto Label_0100;
                }
                text2 = string.IsInterned(text2);
                if (text2 != "A")
                {
                    if (text2 == "B")
                    {
                        goto Label_0097;
                    }
                    if (text2 == "C")
                    {
                        goto Label_00AC;
                    }
                    if (text2 == "D")
                    {
                        goto Label_00C1;
                    }
                    if (text2 == "E")
                    {
                        goto Label_00D6;
                    }
                    if (text2 == "F")
                    {
                        goto Label_00EB;
                    }
                    goto Label_0100;
                }
                this.numberList.Add(10);
                goto Label_0119;
            Label_0097:
                this.numberList.Add(11);
                goto Label_0119;
            Label_00AC:
                this.numberList.Add(12);
                goto Label_0119;
            Label_00C1:
                this.numberList.Add(13);
                goto Label_0119;
            Label_00D6:
                this.numberList.Add(14);
                goto Label_0119;
            Label_00EB:
                this.numberList.Add(15);
                goto Label_0119;
            Label_0100:
                this.numberList.Add(Convert.ToInt32(text1));
            Label_0119:;
            }
            for (int num3 = 0; num3 < this.numberList.Count; num3++)
            {
                num1 += Convert.ToInt32((double) (((int) this.numberList[num3]) * Math.Pow((double) Convert.ToInt64(0x10), (double) Convert.ToInt64((int) ((this.numberList.Count - 1) - num3)))));
            }
            this.numberList.Clear();
            return num1;
        }

        public string ToHex(int Number)
        {
            string text1 = "";
            int num3 = Number;
            int num1 = num3 / 0x10;
            int num2 = num3 % 0x10;
            if (num1 == 0)
            {
                this.numberList.Add(0);
                this.numberList.Add(num2);
            }
            else
            {
                while (num1 != 0)
                {
                    this.numberList.Add(num2);
                    num3 = num1;
                    num1 = num3 / 0x10;
                    num2 = num3 % 0x10;
                }
                this.numberList.Add(num2);
            }
            for (int num4 = this.numberList.Count - 1; num4 >= 0; num4--)
            {
                int num5 = (int) this.numberList[num4];
                switch (num5)
                {
                    case 10:
                    {
                        text1 = text1 + "A";
                        break;
                    }
                    case 11:
                    {
                        text1 = text1 + "B";
                        break;
                    }
                    case 12:
                    {
                        text1 = text1 + "C";
                        break;
                    }
                    case 13:
                    {
                        text1 = text1 + "D";
                        break;
                    }
                    case 14:
                    {
                        text1 = text1 + "E";
                        break;
                    }
                    case 15:
                    {
                        text1 = text1 + "F";
                        break;
                    }
                    default:
                    {
                        text1 = text1 + num5;
                        break;
                    }
                }
            }
            this.numberList.Clear();
            return text1;
        }


        // Fields
        private ArrayList numberList;
    }
}

