namespace ItopVector.Core.Func
{
    using ItopVector.Core.Config;
    using ItopVector.Core.Types;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Text.RegularExpressions;

    public class PointsFunc
    {
        // Methods
        public PointsFunc()
        {
        }

        public static PointF[] PointsParse(string listString)
        {
            return PointsFunc.PointsParse(listString, new PointInfoCollection());
        }

        public static PointF[] PointsParse(string listString, PointInfoCollection pointsInfo)
        {
            listString = listString.Trim();
            while (listString.EndsWith(";"))
            {
                listString = listString.Substring(0, listString.Length - 1);
            }
            PointInfo info1 = null;
            ArrayList list1 = new ArrayList(0x10);
            try
            {
                Regex regex1 = new Regex(@"\s+,?\s*|,\s*");
                string[] textArray1 = regex1.Split(listString);
                for (int num1 = 0; num1 < textArray1.Length; num1 += 2)
                {
                    if (num1 >= textArray1.Length)
                    {
                        goto Label_0117;
                    }
                    string text1 = textArray1[num1];
                    string text2 = textArray1[num1 + 1];
                    if ((text1.Length == 0) || (text2.Length == 0))
                    {
                        throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpointsformat") + listString);
                    }
                    PointF tf1 = new PointF(ItopVector.Core.Func.Number.ParseFloatStr(text1.Trim()), ItopVector.Core.Func.Number.ParseFloatStr(text2.Trim()));
                    list1.Add(tf1);
                    info1 = new PointInfo(tf1, PointF.Empty, PointF.Empty, text1.ToString() + " " + text2.ToString() + ";");
                }
            }
            catch (Exception exception1)
            {
                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidpointsformat") + listString, exception1);
            }
        Label_0117:
            if (info1 != null)
            {
                info1.Command = "L";
                pointsInfo.Add(info1);
            }
            if (list1.Count > 0)
            {
                PointF[] tfArray1 = new PointF[list1.Count];
                list1.CopyTo(0, tfArray1, 0, list1.Count);
                return tfArray1;
            }
            return null;
        }

    }
}

