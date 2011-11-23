namespace ItopVector.Core.Func
{
    using ItopVector.Core;
    using ItopVector.Core.Config;
    using ItopVector.Core.Interface;
    using ItopVector.Core.Interface.Paint;
    using ItopVector.Core.Paint;
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public class Number
    {
        // Methods
        public Number()
        {
        }

        public static float ParseFloatStr(string str)
        {
            float single1;
            if ((str == null) || (str == string.Empty))
            {
                return 0f;
            }
            str = str.Trim();
            NumberFormatInfo info1 = new NumberFormatInfo();
            info1.NumberDecimalSeparator = ".";
            try
            {
                bool flag1 = false;
                if (str.EndsWith("%"))
                {
                    str = str.Substring(0, str.Length - 1);
                    flag1 = true;
                }
                single1 = float.Parse(str, NumberStyles.Any, info1);
                if (flag1)
                {
                    single1 /= 100f;
                }
            }
            catch (Exception)
            {
                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidnumberformat") + str);
            }
            return single1;
        }

        public static float parseToFloat(string str, SvgElement ownerElement, SvgLengthDirection direction)
        {
            float single1 = 0f;
            string text1 = str.Trim();
            if (text1 == "")
            {
                return 0f;
            }
            Regex regex1 = new Regex(@"^\-{0,1}[0-9\.]+$");
            if (regex1.IsMatch(text1))
            {
                return ItopVector.Core.Func.Number.ParseFloatStr(text1);
            }
            regex1 = new Regex(@"^(\-{0,1}[0-9\.]+)\s*([a-z\%]+)$");
            Match match1 = regex1.Match(text1);
            if (match1.Success)
            {
                single1 = ItopVector.Core.Func.Number.ParseFloatStr(match1.Groups[1].Value);
                string text2 = match1.Groups[2].Value;
                short num1 = -1;
                if (text2 == "%")
                {
                    num1 = 2;
                }
                else if (text2 == "em")
                {
                    num1 = 3;
                }
                else if (text2 == "ex")
                {
                    num1 = 4;
                }
                else if (text2 == "px")
                {
                    num1 = 5;
                }
                else if (text2 == "cm")
                {
                    num1 = 6;
                }
                else if (text2 == "mm")
                {
                    num1 = 7;
                }
                else if (text2 == "in")
                {
                    num1 = 8;
                }
                else if (text2 == "pt")
                {
                    num1 = 9;
                }
                else if (text2 == "pc")
                {
                    num1 = 10;
                }
                if (num1 == 0)
                {
                    single1 = 0f;
                    goto Label_02CF;
                }
                if (num1 == 1)
                {
                    single1 = single1;
                    goto Label_02CF;
                }
                if (num1 == 2)
                {
                    if ((ownerElement is IGradientBrush) || (ownerElement is GradientStop))
                    {
                        single1 *= 0.01f;
                        goto Label_02CF;
                    }
                    if (ownerElement != null)
                    {
                        ISvgElement element1;
                        if (ownerElement.ViewportElement != null)
                        {
                            element1 = ownerElement.ViewportElement;
                        }
                        else
                        {
                            element1 = ownerElement;
                        }
                        float single2 = element1.ViewPort.Width;
                        float single3 = element1.ViewPort.Height;
                        if (direction == SvgLengthDirection.Horizontal)
                        {
                            single1 = (single1 * single2) / 100f;
                            goto Label_02CF;
                        }
                        if (direction == SvgLengthDirection.Vertical)
                        {
                            single1 = (single1 * single3) / 100f;
                            goto Label_02CF;
                        }
                        single1 = (Convert.ToSingle((double) (Math.Sqrt((double) ((single2 * single2) + (single3 * single3))) / Math.Sqrt(2))) * single1) / 100f;
                        goto Label_02CF;
                    }
                    single1 /= 100f;
                    goto Label_02CF;
                }
                if (num1 == 3)
                {
                    single1 *= 10f;
                }
                else
                {
                    if (num1 == 4)
                    {
                        single1 *= 5f;
                        goto Label_02CF;
                    }
                    if (num1 == 5)
                    {
                        single1 = single1;
                        goto Label_02CF;
                    }
                    if (num1 == 6)
                    {
                        single1 *= 35.43307f;
                        goto Label_02CF;
                    }
                    if (num1 == 7)
                    {
                        single1 *= 3.543307f;
                        goto Label_02CF;
                    }
                    if (num1 == 8)
                    {
                        single1 *= 90f;
                        goto Label_02CF;
                    }
                    if (num1 == 9)
                    {
                        single1 *= 1.25f;
                    }
                    else if (num1 == 10)
                    {
                        single1 *= 15f;
                    }
                    else
                    {
                        single1 = 0f;
                        throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidnumberformat") + text1);
                    }
                }
                return single1;
            }
            single1 = 0f;
            throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidnumberformat") + text1);
        Label_02CF:
            return single1;
        }

    }
}

