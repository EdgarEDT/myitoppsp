namespace ItopVector.Core.Func
{
    using System;
    using System.Text.RegularExpressions;

    public class StringListFunc
    {
        // Methods
        public StringListFunc()
        {
        }

        public static string[] SplitString(string str, char[] splits)
        {
            if (str.Trim().EndsWith(";"))
            {
                str = str.Trim().Substring(0, str.Trim().Length - 1);
            }
            string text1 = @"\s?[";
            char[] chArray1 = splits;
            for (int num1 = 0; num1 < chArray1.Length; num1++)
            {
                char ch1 = chArray1[num1];
                if (ch1 == ' ')
                {
                    text1 = text1 + @"\s";
                }
                else
                {
                    text1 = text1 + ch1.ToString();
                }
            }
            text1 = text1 + @"]+\s?";
            Regex regex1 = new Regex(text1);
            str = regex1.Replace(str, ";");
            char[] chArray2 = new char[1] { ';' } ;
            return str.Split(chArray2);
        }

    }
}

