namespace ItopVector.Core.Func
{
    using ItopVector.Core;
    using ItopVector.Core.Animate;
    using ItopVector.Core.Figure;
    using ItopVector.Core.Interface;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Core.Interface.Paint;
    using ItopVector.Core.Types;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;

    public class AnimFunc
    {
        // Methods
        public AnimFunc()
        {
        }

        public static void CreateAnimateValues(ISvgElement svgelement, int time, int maxbegin)
        {
            if (svgelement is SvgElement)
            {
                SvgElement element1 = (SvgElement) svgelement;
                element1.AnimateNameValues.Clear();
                Hashtable hashtable1 = new Hashtable(0x10);
                int num1 = 0;
                int num2 = 0;
                int num3 = 0;
                SvgElementCollection.ISvgElementEnumerator enumerator1 = element1.AnimateList.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    ItopVector.Core.Animate.Animate animate1 = (ItopVector.Core.Animate.Animate) enumerator1.Current;
                    if (animate1.Begin < maxbegin)
                    {
                        string text1 = animate1.GetAttribute("additive").Trim();
                        if (((text1 == null) || (text1 == string.Empty)) && (animate1 is MotionAnimate))
                        {
                            text1 = "sum";
                        }
                        bool flag1 = text1 == "sum";
                        string text2 = animate1.AttributeName;
                        int num4 = animate1.Begin;
                        DomType type1 = DomTypeFunc.GetTypeOfAttributeName(text2);
                        object obj1 = animate1.GetAnimateResult((float) time, type1);
                        if ((obj1 != null) && (obj1.ToString() != string.Empty))
                        {
                            if (element1.AnimateNameValues.ContainsKey(text2))
                            {
                                AnimateInfo info1 = (AnimateInfo) element1.AnimateNameValues[text2];
                                info1.Add(obj1, num4, flag1);
                            }
                            else
                            {
                                AnimateInfo info2 = new AnimateInfo();
                                info2.Add(obj1, num4, flag1);
                                element1.AnimateNameValues.Add(text2, info2);
                            }
                        }
                        if (num1 == 0)
                        {
                            num2 = num4;
                            num3 = num4 + animate1.Duration;
                            continue;
                        }
                        num2 = Math.Min(animate1.Begin, num2);
                        num3 = Math.Max((int) (num4 + animate1.Duration), num3);
                    }
                }
                foreach (string text3 in element1.AnimateNameValues.Keys)
                {
                    DomType type2 = DomTypeFunc.GetTypeOfAttributeName(text3);
                    if (element1.SvgAttributes.ContainsKey(text3))
                    {
                        object obj2 = element1.SvgAttributes[text3];
                        object obj3 = AnimFunc.GetAnimateValue(element1, text3, type2, obj2);
                        if (element1.SvgAnimAttributes.ContainsKey(text3))
                        {
                            element1.SvgAnimAttributes[text3] = obj3;
                        }
                        else
                        {
                            element1.SvgAnimAttributes.Add(text3, obj3);
                        }
                        continue;
                    }
                    object obj4 = null;
                    object obj5 = AnimFunc.GetAnimateValue(element1, text3, type2, obj4);
                    if (element1.SvgAnimAttributes.ContainsKey(text3))
                    {
                        element1.SvgAnimAttributes[text3] = obj5;
                        continue;
                    }
                    element1.SvgAnimAttributes.Add(text3, obj5);
                }
            }
        }

        public static void CreateAnimateValues(ISvgElement svgelement, int time, out int totalbegin, out int totalend)
        {
            if (!(svgelement is SvgElement))
            {
                int num5;
                totalend = num5 = 0;
                totalbegin = num5;
            }
            else
            {
                SvgElement element1 = (SvgElement) svgelement;
                element1.AnimateNameValues.Clear();
                Hashtable hashtable1 = new Hashtable(0x10);
                int num1 = 0;
                int num2 = 0;
                int num3 = 0;
                SvgElementCollection.ISvgElementEnumerator enumerator1 = element1.AnimateList.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    ItopVector.Core.Animate.Animate animate1 = (ItopVector.Core.Animate.Animate) enumerator1.Current;
                    string text1 = animate1.GetAttribute("additive").Trim();
                    if (((text1 == null) || (text1 == string.Empty)) && (animate1 is MotionAnimate))
                    {
                        text1 = "sum";
                    }
                    bool flag1 = text1 == "sum";
                    string text2 = animate1.AttributeName;
                    int num4 = animate1.Begin;
                    DomType type1 = DomTypeFunc.GetTypeOfAttributeName(text2);
                    object obj1 = animate1.GetAnimateResult((float) time, type1);
                    if (element1.AnimateNameValues.ContainsKey(text2))
                    {
                        AnimateInfo info1 = (AnimateInfo) element1.AnimateNameValues[text2];
                        info1.Add(obj1, num4, flag1);
                    }
                    else
                    {
                        AnimateInfo info2 = new AnimateInfo();
                        info2.Add(obj1, num4, flag1);
                        element1.AnimateNameValues.Add(text2, info2);
                    }
                    if (num1 == 0)
                    {
                        num2 = num4;
                        num3 = num4 + animate1.Duration;
                        continue;
                    }
                    num2 = Math.Min(animate1.Begin, num2);
                    num3 = Math.Max((int) (num4 + animate1.Duration), num3);
                }
                totalbegin = num2;
                totalend = num3;
                foreach (string text3 in element1.AnimateNameValues.Keys)
                {
                    DomType type2 = DomTypeFunc.GetTypeOfAttributeName(text3);
                    if (element1.SvgAttributes.ContainsKey(text3))
                    {
                        object obj2 = element1.SvgAttributes[text3];
                        object obj3 = AnimFunc.GetAnimateValue(element1, text3, type2, obj2);
                        if (element1.SvgAnimAttributes.ContainsKey(text3))
                        {
                            element1.SvgAnimAttributes[text3] = obj3;
                        }
                        else
                        {
                            element1.SvgAnimAttributes.Add(text3, obj3);
                        }
                        continue;
                    }
                    object obj4 = null;
                    object obj5 = AnimFunc.GetAnimateValue(element1, text3, type2, obj4);
                    if (element1.SvgAnimAttributes.ContainsKey(text3))
                    {
                        element1.SvgAnimAttributes[text3] = obj5;
                        continue;
                    }
                    element1.SvgAnimAttributes.Add(text3, obj5);
                }
            }
        }

        public static object GetAnimateValue(SvgElement element, string attributename, DomType domtype, object orivalue)
        {
            PointF[] tfArray6;
            PointF[] tfArray7;
            PointF[] tfArray8;
            int num8;
            Matrix matrix1 = new Matrix();
            string text1 = string.Empty;
            GraphicsPath path1 = null;
            string text2 = string.Empty;
            PointF[] tfArray1 = null;
            bool flag1 = true;
            if (element.AnimateNameValues.ContainsKey(attributename))
            {
                AnimateInfo info1 = (AnimateInfo) element.AnimateNameValues[attributename];
                object[] objArray1 = info1.AnimateValues;
                bool[] flagArray1 = info1.ValueAdds;
                int num1 = 0;
                if ((domtype == DomType.SvgString) || (domtype == DomType.SvgLink))
                {
                    for (int num2 = objArray1.Length - 1; num2 >= 0; num2--)
                    {
                        if ((objArray1[num2] is string) && (objArray1[num2].ToString() != string.Empty))
                        {
                            if (element is ItopVector.Core.Figure.Image)
                            {
                                ((ItopVector.Core.Figure.Image) element).RefImage = ImageFunc.GetImageForURL(objArray1[num2].ToString(), element);
                            }
                            return objArray1[num2].ToString();
                        }
                    }
                    return orivalue;
                }
                object[] objArray2 = objArray1;
                for (int num10 = 0; num10 < objArray2.Length; num10++)
                {
                    PointF[] tfArray2;
                    float single3;
                    GraphicsPath path2;
                    PointF[] tfArray3;
                    PointF[] tfArray4;
                    PointF[] tfArray5;
                    object obj1 = objArray2[num10];
                    bool flag2 = flagArray1[num1];
                    switch (domtype)
                    {
                        case DomType.SvgMatrix:
                        {
                            Matrix matrix2 = new Matrix();
                            if ((obj1 != null) && (obj1.ToString() != string.Empty))
                            {
                                matrix2 = ((Matrix) obj1).Clone();
                            }
                            if (flag2)
                            {
                                matrix1.Multiply(matrix2);
                                goto Label_046F;
                            }
                            matrix1 = matrix2;
                            goto Label_046F;
                        }
                        case DomType.SvgNumber:
                        {
                            single3 = 0f;
                            if ((obj1 != null) && (obj1.ToString() != string.Empty))
                            {
                                single3 = (float) obj1;
                                if (!flag2 || (text1 == string.Empty))
                                {
                                    goto Label_0246;
                                }
                                float single9 = float.Parse(text1) + single3;
                                text1 = single9.ToString();
                            }
                            goto Label_046F;
                        }
                        case DomType.SvgString:
                        {
                            goto Label_046F;
                        }
                        case DomType.SvgColor:
                        {
                            string text3 = string.Empty;
                            if ((obj1 != null) && (obj1.ToString() != string.Empty))
                            {
                                text3 = (string) obj1;
                            }
                            if (text3 != string.Empty)
                            {
                                if ((flag2 && (text2 != string.Empty)) && (!text2.Trim().StartsWith("url") && !text3.Trim().StartsWith("url")))
                                {
                                    Color color1 = ColorFunc.ParseColor(text3);
                                    Color color2 = ColorFunc.ParseColor(text2);
                                    int num4 = (color1.R + color2.R) / 2;
                                    int num5 = (color1.G + color2.G) / 2;
                                    int num6 = (color1.B + color2.B) / 2;
                                    string[] textArray1 = new string[7] { "rgb(", num4.ToString(), ",", num5.ToString(), ",", num6.ToString(), ")" } ;
                                    text2 = string.Concat(textArray1);
                                    goto Label_046F;
                                }
                                text2 = text3;
                            }
                            goto Label_046F;
                        }
                        case DomType.SvgPath:
                        {
                            if ((obj1 != null) && (obj1.ToString() != string.Empty))
                            {
                                path2 = (GraphicsPath) obj1;
                                if (!flag2 || (path1 == null))
                                {
                                    goto Label_0460;
                                }
                                tfArray3 = path2.PathPoints;
                                tfArray4 = path1.PathPoints;
                                if (tfArray3.Length == tfArray4.Length)
                                {
                                    goto Label_03B5;
                                }
                            }
                            goto Label_046F;
                        }
                        case DomType.SvgPoints:
                        {
                            tfArray2 = new PointF[0];
                            if (obj1 is PointF[])
                            {
                                tfArray2 = (PointF[]) obj1;
                            }
                            if (!flag2)
                            {
                                break;
                            }
                            if (tfArray1.Length == tfArray2.Length)
                            {
                                for (int num3 = 0; num3 < tfArray2.Length; num3++)
                                {
                                    PointF tf1 = tfArray1[num3];
                                    PointF tf2 = tfArray2[num3];
                                    float single1 = (tf1.X + tf2.X) / 2f;
                                    float single2 = (tf1.Y + tf2.Y) / 2f;
                                    tfArray1[num3] = new PointF(single1, single2);
                                }
                            }
                            goto Label_046F;
                        }
                        default:
                        {
                            goto Label_046F;
                        }
                    }
                    tfArray1 = (PointF[]) tfArray2.Clone();
                    goto Label_046F;
                Label_0246:
                    text1 = single3.ToString();
                    goto Label_046F;
                Label_03B5:
                    tfArray5 = new PointF[tfArray4.Length];
                    Array.Copy(tfArray3, tfArray1, tfArray5.Length);
                    byte[] buffer1 = path2.PathTypes;
                    byte[] buffer2 = path1.PathTypes;
                    for (int num7 = 0; num7 < Math.Min(tfArray3.Length, tfArray4.Length); num7++)
                    {
                        PointF tf3 = tfArray3[num7];
                        PointF tf4 = tfArray4[num7];
                        float single4 = tf3.X + tf4.X;
                        float single5 = tf3.Y + tf4.Y;
                        tfArray5[num7] = new PointF(single4, single5);
                    }
                    path1 = new GraphicsPath(tfArray5, path2.PathTypes);
                    goto Label_046D;
                Label_0460:
                    path1 = (GraphicsPath) path2.Clone();
                Label_046D:;
                Label_046F:;
                }
                if (flagArray1.Length > 0)
                {
                    flag1 = flagArray1[flagArray1.Length - 1];
                }
            }
            switch (domtype)
            {
                case DomType.SvgMatrix:
                {
                    Matrix matrix3 = new Matrix();
                    if (orivalue != null)
                    {
                        matrix3 = ((Matrix) orivalue).Clone();
                    }
                    if (flag1)
                    {
                        matrix3.Multiply(matrix1);
                    }
                    else
                    {
                        matrix3 = matrix1.Clone();
                    }
                    return matrix3.Clone();
                }
                case DomType.SvgNumber:
                {
                    if ((flag1 && (orivalue != null)) && (orivalue.ToString() != string.Empty))
                    {
                        float single6 = (float) orivalue;
                        if (text1 == string.Empty)
                        {
                            text1 = single6.ToString();
                            break;
                        }
                        float single10 = float.Parse(text1) + single6;
                        text1 = single10.ToString();
                    }
                    break;
                }
                case DomType.SvgString:
                {
                    return orivalue;
                }
                case DomType.SvgColor:
                {
                    if (text2 == string.Empty)
                    {
                        return orivalue;
                    }
                    if ((flag1 && (orivalue != null)) && (!text2.Trim().StartsWith("url") && !((string) orivalue).Trim().StartsWith("url")))
                    {
                        Color color3 = ColorFunc.ParseColor((string) orivalue);
                        Color color4 = ColorFunc.ParseColor(text2);
                        string[] textArray2 = new string[7];
                        textArray2[0] = "rgb(";
                        int num11 = (color3.R + color4.R) / 2;
                        textArray2[1] = num11.ToString();
                        textArray2[2] = ",";
                        int num12 = (color3.G + color4.G) / 2;
                        textArray2[3] = num12.ToString();
                        textArray2[4] = ",";
                        int num13 = (color3.B + color4.B) / 2;
                        textArray2[5] = num13.ToString();
                        textArray2[6] = ")";
                        text2 = string.Concat(textArray2);
                    }
                    return text2;
                }
                case DomType.SvgPath:
                {
                    if (path1 == null)
                    {
                        return orivalue;
                    }
                    if (!flag1 || (orivalue == null))
                    {
                        return path1;
                    }
                    tfArray6 = ((GraphicsPath) orivalue).PathPoints;
                    tfArray7 = path1.PathPoints;
                    tfArray8 = new PointF[tfArray6.Length];
                    Array.Copy(tfArray6, tfArray1, tfArray8.Length);
                    num8 = 0;
                    goto Label_0738;
                }
                case DomType.SvgPoints:
                {
                    if (tfArray1.Length > 0)
                    {
                        PointF[] tfArray9 = new PointF[0];
                        if (!(orivalue is PointF[]) || !flag1)
                        {
                            return tfArray1;
                        }
                        tfArray9 = (PointF[]) orivalue;
                        if (tfArray9.Length != tfArray1.Length)
                        {
                            return tfArray1;
                        }
                        for (int num9 = 0; num9 < tfArray1.Length; num9++)
                        {
                            tfArray1[num9] = new PointF((tfArray1[num9].X + tfArray9[num9].X) / 2f, (tfArray1[num9].Y + tfArray9[num9].Y) / 2f);
                        }
                    }
                    return tfArray1;
                }
                default:
                {
                    return string.Empty;
                }
            }
            if (text1 != string.Empty)
            {
                return float.Parse(text1);
            }
            if ((orivalue.ToString() == string.Empty) || (orivalue == null))
            {
                return (float) AttributeFunc.GetDefaultValue(element, attributename);
            }
            return (float) orivalue;
        Label_0738:
            if (num8 >= Math.Min(tfArray6.Length, tfArray7.Length))
            {
                return new GraphicsPath(tfArray8, path1.PathTypes);
            }
            PointF tf5 = tfArray6[num8];
            PointF tf6 = tfArray7[num8];
            float single7 = tf5.X + tf6.X;
            float single8 = tf5.Y + tf6.Y;
            tfArray8[num8] = new PointF(single7, single8);
            num8++;
            goto Label_0738;
        }

        public static int GetKeyIndex(SvgElement element, int keytime, bool equal)
        {
            if (element != null)
            {
                int num1 = 0;
                foreach (KeyInfo info1 in element.InfoList)
                {
                    if ((keytime == info1.keytime) || (!equal && (keytime < info1.keytime)))
                    {
                        return num1;
                    }
                    num1++;
                }
            }
            return -1;
        }

        public static float[] GetKeys(SvgElement element)
        {
            ArrayList list1 = new ArrayList(0x10);
            list1.Add(0f);
            SvgElementCollection.ISvgElementEnumerator enumerator1 = element.AnimateList.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                ItopVector.Core.Animate.Animate animate1 = (ItopVector.Core.Animate.Animate) enumerator1.Current;
                foreach (string text1 in animate1.VirtualTimes)
                {
                    float single1 = (float) Math.Round((double) ItopVector.Core.Func.Number.ParseFloatStr(text1), 5);
                    single1 = (float) Math.Round((double) (animate1.Begin + (single1 * animate1.Duration)), 1);
                    if (!list1.Contains(single1))
                    {
                        list1.Add(single1);
                    }
                }
            }
            float[] singleArray1 = new float[list1.Count];
            list1.CopyTo(singleArray1, 0);
            Array.Sort(singleArray1, (Array) null, (IComparer) null);
            return singleArray1;
        }

        public static Matrix GetMatrixForTime(IGraph graph, int time)
        {
            int num1 = 0;
            int num2 = 0;
            ((SvgElement) graph).SvgAnimAttributes = (Hashtable) ((SvgElement) graph).SvgAttributes.Clone();
            AnimFunc.CreateAnimateValues(graph, time, out num1, out num2);
            return graph.Transform.Matrix.Clone();
        }

        public static Matrix GetMatrixForTime(ITransformBrush brush, int time)
        {
            int num1 = 0;
            int num2 = 0;
            ((SvgElement) brush).SvgAnimAttributes = (Hashtable) ((SvgElement) brush).SvgAttributes.Clone();
            AnimFunc.CreateAnimateValues(brush, time, out num1, out num2);
            return brush.Transform.Matrix.Clone();
        }

        public static Matrix GetMatrixForTime(IGraph graph, int time, int begin)
        {
            ((SvgElement) graph).SvgAnimAttributes = (Hashtable) ((SvgElement) graph).SvgAttributes.Clone();
            AnimFunc.CreateAnimateValues(graph, time, begin);
            return graph.Transform.Matrix.Clone();
        }

        public static Matrix GetMatrixForTime(ITransformBrush brush, int time, int begin)
        {
            ((SvgElement) brush).SvgAnimAttributes = (Hashtable) ((SvgElement) brush).SvgAttributes.Clone();
            AnimFunc.CreateAnimateValues(brush, time, begin);
            return brush.Transform.Matrix.Clone();
        }

        public static float GetNearestKey(SvgElement element, float time, bool on)
        {
            float[] singleArray1 = AnimFunc.GetKeys(element);
            Array.Sort(singleArray1, (Array) null, (IComparer) null);
            for (int num1 = 0; num1 < singleArray1.Length; num1++)
            {
                float single1 = singleArray1[num1];
                if ((single1 > time) || (Math.Abs((float) (single1 - time)) <= Math.Pow(10, -2)))
                {
                    if (on)
                    {
                        if ((Math.Abs((float) (single1 - time)) <= Math.Pow(10, -2)) && ((num1 + 1) < singleArray1.Length))
                        {
                            return singleArray1[num1 + 1];
                        }
                        return single1;
                    }
                    if ((num1 - 1) >= 0)
                    {
                        return singleArray1[num1 - 1];
                    }
                    return 0f;
                }
            }
            return singleArray1[singleArray1.Length - 1];
        }

        public static int GetNearestKeyIndex(SvgElement element, float time, bool on)
        {
            float[] singleArray1 = AnimFunc.GetKeys(element);
            Array.Sort(singleArray1, (Array) null, (IComparer) null);
            for (int num1 = 0; num1 < singleArray1.Length; num1++)
            {
                float single1 = singleArray1[num1];
                if ((single1 > time) || (Math.Abs((float) (single1 - time)) <= Math.Pow(10, -2)))
                {
                    if (on)
                    {
                        if ((Math.Abs((float) (single1 - time)) <= Math.Pow(10, -2)) && ((num1 + 1) < singleArray1.Length))
                        {
                            return (num1 + 1);
                        }
                        return num1;
                    }
                    if (Math.Abs((float) (single1 - time)) <= Math.Pow(10, -2))
                    {
                        if (num1 >= 2)
                        {
                            return (num1 - 2);
                        }
                    }
                    else
                    {
                        if ((num1 - 1) >= 0)
                        {
                            return (num1 - 1);
                        }
                        return 0;
                    }
                }
            }
            if (on)
            {
                return (singleArray1.Length - 1);
            }
            if (singleArray1.Length >= 2)
            {
                return (singleArray1.Length - 2);
            }
            return (singleArray1.Length - 1);
        }

        public static bool InKey(SvgElement element, int time)
        {
            foreach (KeyInfo info1 in element.InfoList)
            {
                if (info1.keytime == time)
                {
                    return true;
                }
            }
            return false;
        }

        public static float Linear(float a1, float b1, float a2, float b2, float b)
        {
            float single1 = (a2 - a1) / (b2 - b1);
            return (a1 + (single1 * (b - b1)));
        }

        public static string[] Linear(string startvalue, float starttime, string endvalue, float endtime, DomType domtype, int time)
        {
            int num2;
            int num5;
            char[] chArray1 = new char[2] { ' ', ',' } ;
            string[] textArray1 = startvalue.Split(chArray1);
            char[] chArray2 = new char[2] { ' ', ',' } ;
            string[] textArray2 = endvalue.Split(chArray2);
            string[] textArray3 = new string[Math.Min(textArray1.Length, textArray2.Length)];
            switch (domtype)
            {
                case DomType.SvgMatrix:
                case DomType.SvgNumber:
                {
                    num2 = 0;
                    goto Label_0217;
                }
                case DomType.SvgString:
                {
                    goto Label_0762;
                }
                case DomType.SvgColor:
                {
                    if ((!startvalue.Trim().StartsWith("url") && !endvalue.Trim().StartsWith("url")) && ((endvalue != "none") && (startvalue != "none")))
                    {
                        Color color1 = ColorFunc.ParseColor(startvalue.Trim());
                        Color color2 = ColorFunc.ParseColor(endvalue.Trim());
                        float single5 = (float) Math.Round((double) AnimFunc.Linear((float) color1.R, starttime, (float) color2.R, endtime, (float) time), 2);
                        float single6 = (float) Math.Round((double) AnimFunc.Linear((float) color1.G, starttime, (float) color2.G, endtime, (float) time), 2);
                        float single7 = (float) Math.Round((double) AnimFunc.Linear((float) color1.B, starttime, (float) color2.B, endtime, (float) time), 2);
                        string[] textArray11 = new string[1];
                        string[] textArray12 = new string[7] { "rgb(", single5.ToString(), ",", single6.ToString(), ",", single7.ToString(), ")" } ;
                        textArray11[0] = string.Concat(textArray12);
                        return textArray11;
                    }
                    if (time < ((starttime / 2f) + (endtime / 2f)))
                    {
                        return new string[1] { startvalue } ;
                    }
                    return new string[1] { endvalue } ;
                }
                case DomType.SvgPath:
                {
                    PointInfoCollection collection1 = new PointInfoCollection();
                    PointInfoCollection collection2 = new PointInfoCollection();
                    GraphicsPath path1 = PathFunc.PathDataParse(startvalue, collection1);
                    GraphicsPath path2 = PathFunc.PathDataParse(endvalue, collection2);
                    if (collection1.Count == collection2.Count)
                    {
                        string text4 = string.Empty;
                        for (int num3 = 0; num3 < collection1.Count; num3++)
                        {
                            PointInfo info1 = collection1[num3];
                            PointInfo info2 = collection2[num3];
                            if (((info1.Command.Trim().ToLower() != info2.Command.Trim().ToLower()) || (info1.IsStart != info2.IsStart)) || (info1.IsEnd != info2.IsEnd))
                            {
                                if ((time > ((starttime + endtime) / 2f)) && (time <= endtime))
                                {
                                    return new string[1] { endvalue } ;
                                }
                                if ((time <= ((starttime + endtime) / 2f)) && (time >= starttime))
                                {
                                    return new string[1] { startvalue } ;
                                }
                                return new string[1] { string.Empty } ;
                            }
                            text4 = text4 + info1.Command.Trim().ToUpper();
                            string text5 = info1.Command.Trim().ToUpper();
                            if (((text5 == "C") || (text5 == "Q")) || (((text5 == "A") || (text5 == "T")) || (text5 == "S")))
                            {
                                float single8 = AnimFunc.Linear(info1.FirstControl.X, starttime, info2.FirstControl.X, endtime, (float) time);
                                float single9 = AnimFunc.Linear(info1.FirstControl.Y, starttime, info2.FirstControl.Y, endtime, (float) time);
                                string text8 = text4;
                                string[] textArray19 = new string[5] { text8, single8.ToString(), " ", single9.ToString(), " " } ;
                                text4 = string.Concat(textArray19);
                                single8 = AnimFunc.Linear(info1.SecondControl.X, starttime, info2.SecondControl.X, endtime, (float) time);
                                single9 = AnimFunc.Linear(info1.SecondControl.Y, starttime, info2.SecondControl.Y, endtime, (float) time);
                                string text9 = text4;
                                string[] textArray20 = new string[5] { text9, single8.ToString(), " ", single9.ToString(), " " } ;
                                text4 = string.Concat(textArray20);
                            }
                            float single10 = AnimFunc.Linear(info1.MiddlePoint.X, starttime, info2.MiddlePoint.X, endtime, (float) time);
                            float single11 = AnimFunc.Linear(info1.MiddlePoint.Y, starttime, info2.MiddlePoint.Y, endtime, (float) time);
                            string text10 = text4;
                            string[] textArray21 = new string[5] { text10, single10.ToString(), " ", single11.ToString(), " " } ;
                            text4 = string.Concat(textArray21);
                            if (info1.IsEnd)
                            {
                                text4 = text4 + "Z";
                            }
                        }
                        return new string[1] { text4 } ;
                    }
                    if ((time > ((starttime + endtime) / 2f)) && (time <= endtime))
                    {
                        return new string[1] { endvalue } ;
                    }
                    if ((time <= ((starttime + endtime) / 2f)) && (time >= starttime))
                    {
                        return new string[1] { startvalue } ;
                    }
                    return new string[1] { string.Empty } ;
                }
                case DomType.SvgPoints:
                {
                    PointF[] tfArray1 = PointsFunc.PointsParse(startvalue);
                    PointF[] tfArray2 = PointsFunc.PointsParse(endvalue);
                    if (tfArray1.Length == tfArray2.Length)
                    {
                        string text1 = string.Empty;
                        for (int num1 = 0; num1 < tfArray1.Length; num1++)
                        {
                            PointF tf1 = tfArray1[num1];
                            PointF tf2 = tfArray2[num1];
                            float single1 = AnimFunc.Linear(tf1.X, starttime, tf2.X, endtime, (float) time);
                            float single2 = AnimFunc.Linear(tf1.Y, starttime, tf2.Y, endtime, (float) time);
                            text1 = text1 + single1.ToString() + " " + single2.ToString();
                            if (num1 < (tfArray1.Length - 1))
                            {
                                text1 = text1 + ",";
                            }
                        }
                        return new string[1] { text1 } ;
                    }
                    if ((time > ((starttime + endtime) / 2f)) && (time <= endtime))
                    {
                        return new string[1] { endvalue } ;
                    }
                    if ((time <= ((starttime + endtime) / 2f)) && (time >= starttime))
                    {
                        return new string[1] { startvalue } ;
                    }
                    return new string[1] { string.Empty } ;
                }
                default:
                {
                    goto Label_0762;
                }
            }
        Label_0217:
            if (num2 >= textArray3.Length)
            {
                return textArray3;
            }
            string text2 = textArray1[num2];
            string text3 = textArray2[num2];
            float single3 = 0f;
            float single4 = 0f;
            try
            {
                single3 = ItopVector.Core.Func.Number.ParseFloatStr(text2);
                single4 = ItopVector.Core.Func.Number.ParseFloatStr(text3);
                double num6 = Math.Round((double) AnimFunc.Linear(single3, starttime, single4, endtime, (float) time), 2);
                textArray3[num2] = num6.ToString();
            }
            catch (Exception)
            {
            }
            num2++;
            goto Label_0217;
        Label_0762:
            num5 = 0;
            while (num5 < textArray3.Length)
            {
                string text6 = textArray1[num5];
                string text7 = textArray2[num5];
                if (time >= ((endtime / 2f) + (starttime / 2f)))
                {
                    textArray3[num5] = text7;
                }
                else if (time >= starttime)
                {
                    textArray3[num5] = text6;
                }
                else
                {
                    textArray3[num5] = string.Empty;
                }
                num5++;
            }
            return textArray3;
        }

        public static float Remainder(float t, float d)
        {
            return (float) (d * Math.Floor((double) (t / d)));
        }

    }
}

