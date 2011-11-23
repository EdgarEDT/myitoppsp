namespace ItopVector.Core.Types
{
    using ItopVector.Core.Config;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface.Types;
    using System;
    using System.Text.RegularExpressions;

    public class SvgTime : ISvgTime
    {
        // Methods
        public SvgTime()
        {
            this.floatvalue = 0f;
            this.valueasStr = string.Empty;
        }

        public SvgTime(string text)
        {
            this.floatvalue = 0f;
            this.valueasStr = string.Empty;
            text = text.Trim();
            this.valueasStr = text;
            string text1 = @"\A[0-9]+\.*[0-9]*[{min}{ms}sh]*\Z";
            string text2 = @"\A\S+\s*.\s*(begin|end){1}[\;\s]*\Z";
            string text3 = @"\A\S+\s*.\s*(click|mouseover|focusin|focusout|mousedown|mouseup|activate|load|mouseout|mousemove){1}[\;\s]*\Z";
            string text4 = @"\AaccessKey\s*\(\s*\S{1}\s*\)\Z";
            string text5 = @"\A\S+\s*.\s*repeat\([0-9]\)\Z";
            string text6 = @"\Awallback\s*\(\s*\S+\s*\)\s*\Z";
            if (new Regex(text1).Match(text).Success)
            {
                this.timeType = ItopVector.Core.Interface.Types.SvgTimeType.OffsetValue;
            }
            else if (new Regex(text2).Match(text).Success)
            {
                this.timeType = ItopVector.Core.Interface.Types.SvgTimeType.SyncbaseValue;
            }
            else if (new Regex(text3).Match(text).Success)
            {
                this.timeType = ItopVector.Core.Interface.Types.SvgTimeType.EventValue;
            }
            else if (new Regex(text4).Match(text).Success)
            {
                this.timeType = ItopVector.Core.Interface.Types.SvgTimeType.AccessKeyValue;
            }
            else if (new Regex(text5).Match(text).Success)
            {
                this.timeType = ItopVector.Core.Interface.Types.SvgTimeType.RepeatValue;
            }
            else if (new Regex(text6).Match(text).Success)
            {
                this.timeType = ItopVector.Core.Interface.Types.SvgTimeType.WallClockValue;
            }
            else if (text == "indefinite")
            {
                this.timeType = ItopVector.Core.Interface.Types.SvgTimeType.Indefinite;
            }
            if (this.timeType == ItopVector.Core.Interface.Types.SvgTimeType.OffsetValue)
            {
                this.floatvalue = this.ParseTime(text);
            }
        }

        private float ParseSingleTime(string timestr)
        {
            timestr = timestr.Trim();
            string text1 = timestr;
            short num1 = 0;
            if (timestr.EndsWith("h"))
            {
                text1 = timestr.Substring(0, timestr.Length - 1);
                num1 = 3;
            }
            else if (timestr.EndsWith("min"))
            {
                text1 = timestr.Substring(0, timestr.Length - 3);
                num1 = 2;
            }
            else if (timestr.EndsWith("ms"))
            {
                text1 = timestr.Substring(0, timestr.Length - 2);
                num1 = 1;
            }
            else if (timestr.EndsWith("s"))
            {
                text1 = timestr.Substring(0, timestr.Length - 1);
                num1 = 0;
            }
            float single1 = 0f;
            try
            {
                single1 = ItopVector.Core.Func.Number.ParseFloatStr(text1);
                switch (num1)
                {
                    case 1:
                    {
                        return (single1 / 1000f);
                    }
                    case 2:
                    {
                        return (single1 * 60f);
                    }
                    case 3:
                    {
                        break;
                    }
                    default:
                    {
                        return single1;
                    }
                }
                single1 *= 360f;
            }
            catch (Exception exception1)
            {
                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidtimeformat") + timestr, exception1);
            }
            return single1;
        }

        private float ParseTime(string timestr)
        {
            timestr = timestr.Trim();
            string text1 = timestr;
            Regex regex1 = new Regex(@"\s*\:\s*");
            Match match1 = regex1.Match(timestr);
            string[] textArray1 = regex1.Split(timestr);
            float single1 = 0f;
            if (textArray1.Length > 3)
            {
                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidtimeformat") + timestr);
            }
            try
            {
                if (textArray1.Length > 1)
                {
                    for (int num1 = 0; num1 < textArray1.Length; num1++)
                    {
                        string text2 = textArray1[(textArray1.Length - num1) - 1].Trim();
                        switch (num1)
                        {
                            case 0:
                            {
                                text2 = text2 + "s";
                                goto Label_00AD;
                            }
                            case 1:
                            {
                                text2 = text2 + "min";
                                goto Label_00AD;
                            }
                            case 2:
                            {
                                break;
                            }
                            default:
                            {
                                goto Label_00AD;
                            }
                        }
                        text2 = text2 + "h";
                    Label_00AD:
                        single1 += this.ParseSingleTime(text2);
                    }
                    return single1;
                }
                if (textArray1.Length == 1)
                {
                    return this.ParseSingleTime(timestr);
                }
            }
            catch (Exception)
            {
                throw new Exception(ItopVector.Core.Config.Config.GetLabelForName("invalidtimeformat") + timestr);
            }
            return single1;
        }

        public override string ToString()
        {
            return this.ValueAsStr;
        }


        // Properties
        public ItopVector.Core.Interface.Types.SvgTimeType SvgTimeType
        {
            get
            {
                return this.timeType;
            }
            set
            {
                this.timeType = value;
            }
        }

        public float Value
        {
            get
            {
                return this.floatvalue;
            }
            set
            {
                this.floatvalue = value;
            }
        }

        public string ValueAsStr
        {
            get
            {
                return this.valueasStr;
            }
            set
            {
                this.valueasStr = value;
            }
        }


        // Fields
        private float floatvalue;
        private ItopVector.Core.Interface.Types.SvgTimeType timeType;
        private string valueasStr;
    }
}

