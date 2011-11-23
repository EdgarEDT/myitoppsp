namespace ItopVector.Core.Func
{
    using ItopVector.Core.Config;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public class ColorFunc
    {
        // Methods
        public ColorFunc()
        {
        }
		public static string GetColorString(Color color)
		{
			if (color.IsEmpty || (color == Color.Transparent))
			{
				return "none";
			}
			int num1 = color.R;
			int num2 = color.G;
			int num3 = color.B;
			string text1 = "#";
			string text2 = num1.ToString("X");
			if (text2.Length < 2)
			{
				text2 = "0" + text2;
			}
			text1 = text1 + text2;
			text2 = num2.ToString("X");
			if (text2.Length < 2)
			{
				text2 = "0" + text2;
			}
			text1 = text1 + text2;
			text2 = num3.ToString("X");
			if (text2.Length < 2)
			{
				text2 = "0" + text2;
			}
			return (text1 + text2);
		}

        public static string GetColorStringRgb(Color color)
        {
            string text1 = string.Empty;
            if ((color.IsKnownColor || color.IsNamedColor) || color.IsSystemColor)
            {
                return (text1 + color.Name.ToLower());
            }
            string text3 = text1;
            string[] textArray1 = new string[8] { text3, "rgb(", color.R.ToString(), ",", color.G.ToString(), ",", color.B.ToString(), ")" } ;
            return string.Concat(textArray1);
        }

        public static Color ParseColor(string str)
        {
            Color color1;
            str = str.Trim();
            if (str == "none")
            {
                return Color.Empty;
            }
            if (str.StartsWith("rgb("))
            {
                Regex regex1 = new Regex(@"[\d\.\%]+");
                NumberFormatInfo info1 = new NumberFormatInfo();
                info1.NumberDecimalSeparator = ".";
                try
                {
                    int num1;
                    int num2;
                    int num3;
                    Match match1 = regex1.Match(str);
                    string text1 = match1.Value;
                    if (text1.EndsWith("%"))
                    {
                        text1 = text1.Remove(text1.Length - 1, 1);
                        num1 = Convert.ToInt32((double) (float.Parse(text1, info1) * 2.55));
                    }
                    else
                    {
                        num1 = Convert.ToInt32(float.Parse(text1, info1));
                    }
                    match1 = match1.NextMatch();
                    text1 = match1.Value;
                    if (text1.EndsWith("%"))
                    {
                        text1 = text1.Remove(text1.Length - 1, 1);
                        num2 = Convert.ToInt32((double) (float.Parse(text1, info1) * 2.55));
                    }
                    else
                    {
                        num2 = Convert.ToInt32(float.Parse(text1, info1));
                    }
                    match1 = match1.NextMatch();
                    text1 = match1.Value;
                    if (text1.EndsWith("%"))
                    {
                        text1 = text1.Remove(text1.Length - 1, 1);
                        num3 = Convert.ToInt32((double) (float.Parse(text1, info1) * 2.55));
                    }
                    else
                    {
                        num3 = Convert.ToInt32(float.Parse(text1, info1));
                    }
                    return Color.FromArgb(num1, num2, num3);
                }
                catch (Exception exception1)
                {
                    throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidcolorformat") + str + ":" + exception1.Message);
                }
            }
            try
            {
                color1 = ColorTranslator.FromHtml(str);
            }
            catch
            {
                color1 = Color.Empty;
            }
            return color1;
        }

    }
}

