namespace ItopVector.Core.Func
{
    using ItopVector.Core.Config;
    using ItopVector.Core.Types;
    using System;
    using System.Globalization;
    using System.Xml;

    public class TypeFunc
    {
        // Methods
        public TypeFunc()
        {
        }

        public static ComponentTransferType ParsComponentType(string type)
        {
            string text1;
            if ((text1 = type.Trim().ToLower()) != null)
            {
                text1 = string.IsInterned(text1);
                if (text1 != "discrete")
                {
                    if (text1 == "gamma")
                    {
                        return ComponentTransferType.gamma;
                    }
                    if (text1 == "identity")
                    {
                        return ComponentTransferType.identity;
                    }
                    if (text1 == "linear")
                    {
                        return ComponentTransferType.linear;
                    }
                    if (text1 == "table")
                    {
                        return ComponentTransferType.table;
                    }
                }
                else
                {
                    return ComponentTransferType.discrete;
                }
            }
            return ComponentTransferType.identity;
        }

        public static ViewBox ParseViewBox(XmlElement node)
        {
            ViewBox box1 = null;
            XmlAttribute attribute1 = node.Attributes["viewBox"];
            if (attribute1 != null)
            {
                string text1 = attribute1.Value.Trim();
                char[] chArray1 = new char[2] { ',', ' ' } ;
                string[] textArray1 = text1.Trim().Split(chArray1);
                if (textArray1.Length >= 4)
                {
                    NumberFormatInfo info1 = new NumberFormatInfo();
                    info1.NumberDecimalSeparator = ".";
                    try
                    {
                        float single1 = float.Parse(textArray1[0], info1);
                        float single2 = float.Parse(textArray1[1], info1);
                        float single3 = float.Parse(textArray1[2], info1);
                        float single4 = float.Parse(textArray1[3], info1);
                        box1 = new ViewBox(single1, single2, single3, single4);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            attribute1 = node.Attributes["preserveAspectRatio"];
            if ((attribute1 != null) && (box1 != null))
            {
                string text5;
                string text2 = attribute1.Value.Trim();
                char[] chArray2 = new char[2] { ' ', ',' } ;
                string[] textArray2 = text2.Trim().Split(chArray2);
                string text3 = string.Empty;
                string text4 = string.Empty;
                PreserveAspectRatio ratio1 = null;
                parAlign align1 = parAlign.none;
                parMeetOrSlice slice1 = parMeetOrSlice.meet;
                if (textArray2.Length >= 1)
                {
                    text3 = textArray2[0].Trim();
                }
                else if (textArray2.Length >= 2)
                {
                    text4 = textArray2[1].Trim();
                }
                if ((text3 != string.Empty) && ((text5 = text3.Trim().ToLower()) != null))
                {
                    text5 = string.IsInterned(text5);
                    if (text5 != "xminymin")
                    {
                        if (text5 == "xmidymin")
                        {
                            align1 = parAlign.xMidYMin;
                        }
                        else if (text5 == "xminymid")
                        {
                            align1 = parAlign.xMinYMid;
                        }
                        else if (text5 == "xmidymid")
                        {
                            align1 = parAlign.xMidYMid;
                        }
                        else if (text5 == "xmaxymid")
                        {
                            align1 = parAlign.xMaxYMid;
                        }
                        else if (text5 == "xminymax")
                        {
                            align1 = parAlign.xMinYMax;
                        }
                        else if (text5 == "xmidymax")
                        {
                            align1 = parAlign.xMidYMax;
                        }
                        else if (text5 == "xmaxymax")
                        {
                            align1 = parAlign.xMaxYMax;
                        }
                    }
                    else
                    {
                        align1 = parAlign.xMaxYMin;
                    }
                }
                if (text4 != string.Empty)
                {
                    if (text4.Trim().ToLower() == "meet")
                    {
                        slice1 = parMeetOrSlice.meet;
                    }
                    else if (text4.Trim().ToLower() == "slice")
                    {
                        slice1 = parMeetOrSlice.slice;
                    }
                }
                ratio1 = new PreserveAspectRatio(align1, slice1);
                box1.psr = ratio1;
            }
            return box1;
        }

        public static FeFuncType ParsFeFuncType(string type)
        {
            string text1;
            if ((text1 = type.Trim().ToLower()) != null)
            {
                text1 = string.IsInterned(text1);
                if (text1 != "fefuncb")
                {
                    if (text1 == "fefuncr")
                    {
                        return FeFuncType.FeFuncR;
                    }
                    if (text1 == "fefuncg")
                    {
                        return FeFuncType.FeFuncG;
                    }
                }
                else
                {
                    return FeFuncType.FeFuncB;
                }
            }
            return FeFuncType.FeFuncB;
        }

        public static MatrixType ParsMatrixType(string type)
        {
            string text1;
            if ((text1 = type.Trim().ToLower()) != null)
            {
                text1 = string.IsInterned(text1);
                if (text1 != "matrix")
                {
                    if (text1 == "saturate")
                    {
                        return MatrixType.saturate;
                    }
                    if (text1 == "luminancetoalpha")
                    {
                        return MatrixType.luminanceToAlpha;
                    }
                    if (text1 == "huerotate")
                    {
                        return MatrixType.hueRotate;
                    }
                }
                else
                {
                    return MatrixType.matrix;
                }
            }
            throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidtype"));
        }

        public static Units ParsUnits(string units)
        {
            string text1;
            if ((text1 = units.Trim().ToLower()) != null)
            {
                text1 = string.IsInterned(text1);
                if (text1 != "objectboundingbox")
                {
                    if (text1 == "userspace")
                    {
                        return Units.UserSpace;
                    }
                    if (text1 == "userspaceonuse")
                    {
                        return Units.UserSpaceOnUse;
                    }
                }
                else
                {
                    return Units.ObjectBoundingBox;
                }
            }
            return Units.ObjectBoundingBox;
        }

    }
}

