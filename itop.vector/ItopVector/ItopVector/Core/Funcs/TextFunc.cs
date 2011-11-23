namespace ItopVector.Core.Func
{
    using ItopVector.Core.Figure;
    using System;
    using System.Drawing;
    using System.Text.RegularExpressions;

    public class TextFunc
    {
        // Methods
        public TextFunc()
        {
        }

        public static float GetComputedFontSize(Text text)
        {
            return text.Size;
        }

        public static FontFamily GetGDIFontFamily(Text text)
        {
            string[] textArray1;
            string text1 = text.FontName;
            if (((text1 == "") || (text1 == null)) || (text1 == string.Empty))
            {
                string[] textArray2 = new string[1] { "Arial" } ;
                textArray1 = textArray2;
            }
            else
            {
                char[] chArray1 = new char[1] { ',' } ;
                textArray1 = text1.Split(chArray1);
            }
            string[] textArray3 = textArray1;
            for (int num1 = 0; num1 < textArray3.Length; num1++)
            {
                FontFamily family2=new FontFamily("Arial");
                string text2 = textArray3[num1];
                try
                {
                    FontFamily family1;
                    char[] chArray2 = new char[2] { ' ', '\'' } ;
                    string text3 = text2.Trim(chArray2).ToLower();
                    if (text3 == "serif")
                    {
                        family1 = FontFamily.GenericSerif;
                    }
                    else if (text3 == "sans-serif")
                    {
                        family1 = FontFamily.GenericSansSerif;
                    }
                    else if (text3 == "monospace")
                    {
                        family1 = FontFamily.GenericMonospace;
                    }
                    else
                    {
                        family1 = new FontFamily(text3);
                    }
                    family2 = family1;
                }
                catch
                {
                }
                return family2;
            }
            return new FontFamily("Arial");
        }

        public static FontFamily GetGDIFontFamily(string fontFamily)
        {
            string[] textArray1;
            if (((fontFamily == "") || (fontFamily == null)) || (fontFamily == string.Empty))
            {
                string[] textArray2 = new string[1] { "Arial" } ;
                textArray1 = textArray2;
            }
            else
            {
                char[] chArray1 = new char[1] { ',' } ;
                textArray1 = fontFamily.Split(chArray1);
            }
            string[] textArray3 = textArray1;
            for (int num1 = 0; num1 < textArray3.Length; num1++)
            {
                FontFamily family2=null;
                string text1 = textArray3[num1];
                try
                {
                    FontFamily family1;
                    char[] chArray2 = new char[2] { ' ', '\'' } ;
                    string text2 = text1.Trim(chArray2).ToLower();
                    if (text2 == "serif")
                    {
                        family1 = FontFamily.GenericSerif;
                    }
                    else if (text2 == "sans-serif")
                    {
                        family1 = FontFamily.GenericSansSerif;
                    }
                    else if (text2 == "monospace")
                    {
                        family1 = FontFamily.GenericMonospace;
                    }
                    else
                    {
                        family1 = new FontFamily(text2);
                    }
                    family2 = family1;
                }
                catch
                {
                }
                return family2;
            }
            return new FontFamily("Arial");
        }

        public static StringFormat GetGDIStringFormat(Text text)
        {
            StringFormat format1 = new StringFormat(StringFormat.GenericTypographic);
            format1.LineAlignment = StringAlignment.Near;
            bool flag1 = true;
            if (text is TSpan)
            {
                flag1 = text.doalign;
            }
            if (flag1)
            {
                string text1 = AttributeFunc.ParseAttribute("text-anchor", text, false).ToString().Trim();
                if (text1 == "middle")
                {
                    format1.Alignment = StringAlignment.Center;
                }
                if (text1 == "end")
                {
                    format1.Alignment = StringAlignment.Far;
                }
            }
            string text2 = AttributeFunc.ParseAttribute("direction", text, false).ToString().Trim();
            if (text2 == "rtl")
            {
                if (format1.Alignment == StringAlignment.Far)
                {
                    format1.Alignment = StringAlignment.Near;
                }
                else if (format1.Alignment == StringAlignment.Near)
                {
                    format1.Alignment = StringAlignment.Far;
                }
                format1.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            }
            text2 = AttributeFunc.ParseAttribute("writing-mode", text, false).ToString().Trim();
            if (text2 == "tb")
            {
                format1.FormatFlags |= StringFormatFlags.DirectionVertical;
            }
            format1.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            return format1;
        }

        public static int GetGDIStyle(Text text)
        {
            int num1 = 0;
            string text1 = AttributeFunc.ParseAttribute("font-weight", text, false).ToString().Trim();
            if ((((text1 == "bold") || (text1 == "bolder")) || ((text1 == "600") || (text1 == "700"))) || ((text1 == "800") || (text1 == "900")))
            {
                num1 |= 1;
            }
            if (AttributeFunc.ParseAttribute("font-style", text, false).ToString().Trim() == "italic")
            {
                num1 |= 2;
            }
            string text2 = AttributeFunc.ParseAttribute("text-decoration", text, false).ToString().Trim();
            if (text2 == "line-through")
            {
                return (num1 | 8);
            }
            if (text2 == "underline")
            {
                num1 |= 4;
            }
            return num1;
        }

        public static string TrimText(string val, Text text)
        {
            Regex regex1 = new Regex(@"[\n\f\t]");
            if (text.XmlSpace != "preserve")
            {
                val = val.Replace("\n", "");
            }
            val = regex1.Replace(val, " ");
            if (text.XmlSpace == "preserve")
            {
                return val;
            }
            return val.Trim();
        }

    }
}

