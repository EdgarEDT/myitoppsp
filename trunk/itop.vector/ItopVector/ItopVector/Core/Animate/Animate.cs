using ItopVector.Core;
using ItopVector.Core.Document;
using ItopVector.Core.Func;
using ItopVector.Core.Interface;
using ItopVector.Core.Types;
using System;
using System.Collections;
using System.Xml;

namespace ItopVector.Core.Animate
{
 
    public class Animate : SvgElement
    {
        // Methods
        internal Animate(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
            this.values = new ArrayList();
            this.keyTimes = new ArrayList();
            this.realdur = 0f;
            this.Additive = Additive.replace;
            this.Accumulate = Accumulate.none;
            this.Speed = 10;
        }

        public void AdaptKeys(int delta)
        {
            for (int num1 = 0; num1 < this.KeyTimes.Count; num1++)
            {
                int num2 = (int) float.Parse((string) this.KeyTimes[num1]);
                num2 += delta;
                this.KeyTimes[num1] = num2.ToString();
            }
            string text1 = "";
            if ((this.KeyTimes != null) && (this.KeyTimes.Count > 0))
            {
                for (int num3 = 0; num3 < this.KeyTimes.Count; num3++)
                {
                    float single1 = (float.Parse((string) this.KeyTimes[num3]) - this.Begin) / ((float) this.Duration);
                    if (num3 == (this.KeyTimes.Count - 1))
                    {
                        text1 = text1 + single1.ToString();
                    }
                    else
                    {
                        text1 = text1 + single1.ToString() + ";";
                    }
                }
            }
        }

        public void AdaptKeyTime(int keytime, int delta)
        {
            int num1 = this.Begin;
            int num2 = this.Duration;
            int num3 = keytime + delta;
            object[] objArray1 = new object[this.Values.Count];
            this.values.CopyTo(objArray1);
            int[] numArray1 = new int[this.VirtualTimes.Count];
            ArrayList list1 = (ArrayList) this.VirtualTimes.Clone();
            int num4 = 0;
            int num5 = 0;
            for (int num6 = 0; num6 < numArray1.Length; num6++)
            {
                int num7 = (int) ItopVector.Core.Func.Number.ParseFloatStr((string) list1[num6]);
                if (num7 == keytime)
                {
                    numArray1[num6] = num3;
                }
                else
                {
                    numArray1[num6] = num7;
                }
                if (num6 == 0)
                {
                    num4 = num5 = numArray1[num6];
                }
                else
                {
                    num4 = Math.Min(num4, numArray1[num6]);
                    num5 = Math.Max(num5, numArray1[num6]);
                }
            }
            if (this.values.Count > 0)
            {
                if (numArray1.Length == objArray1.Length)
                {
                    Array.Sort(numArray1, objArray1);
                }
                num1 = num4;
                num2 = num5 - num4;
                this.values = new ArrayList(objArray1);
                this.keyTimes = new ArrayList(numArray1);
                string text1 = "";
                if ((numArray1.Length > 0) && (num2 > 0))
                {
                    for (int num8 = 0; num8 < numArray1.Length; num8++)
                    {
                        float single1 = ((float) (numArray1[num8] - num1)) / ((float) num2);
                        if (num8 == (numArray1.Length - 1))
                        {
                            text1 = text1 + single1.ToString();
                        }
                        else
                        {
                            text1 = text1 + single1.ToString() + ";";
                        }
                    }
                    AttributeFunc.SetAttributeValue(this, "keyTimes", text1);
                }
                text1 = "";
                if (objArray1.Length > 0)
                {
                    for (int num9 = 0; num9 < objArray1.Length; num9++)
                    {
                        if (num9 == (objArray1.Length - 1))
                        {
                            text1 = text1 + objArray1[num9].ToString();
                        }
                        else
                        {
                            text1 = text1 + objArray1[num9].ToString() + ";";
                        }
                    }
                    AttributeFunc.SetAttributeValue(this, "values", text1);
                }
            }
            this.Begin = num4;
            this.Duration = num5 - num4;
        }

        protected void CreateValues()
        {
            if ((this.keyTimes.Count == 0) && (this.values.Count > 0))
            {
                int num1 = this.Begin;
                int num2 = this.Duration;
                if (this.values.Count == 1)
                {
                    this.keyTimes.Add(num1);
                }
                else
                {
                    for (int num3 = 0; num3 < this.values.Count; num3++)
                    {
                        float single1 = num1 + ((((float) num3) / ((float) (this.values.Count - 1))) * num2);
                        this.keyTimes.Add(single1.ToString());
                    }
                }
            }
        }

        public void DelKeyTime(int time)
        {
            if (this.values.Count == 0)
            {
                if ((this.Begin == time) || ((this.Duration + this.Begin) == time))
                {
                    this.ParentNode.RemoveChild(this);
                }
            }
            else
            {
                ArrayList list1 = (ArrayList) this.KeyTimes.Clone();
                int num1 = 0;
                int num2 = 0;
                bool flag1 = false;
                ArrayList list2 = new ArrayList(0x10);
                for (int num3 = 0; num3 < list1.Count; num3++)
                {
                    float single1 = float.Parse((string) list1[num3]);
                    if (single1 == time)
                    {
                        list1.RemoveAt(num3);
                        this.values.RemoveAt(num3);
                        num3--;
                    }
                    else
                    {
                        if (!flag1)
                        {
                            num1 = num2 = (int) single1;
                            flag1 = true;
                        }
                        else
                        {
                            num1 = (int) Math.Min((float) num1, single1);
                            num2 = (int) Math.Max((float) num2, single1);
                        }
                        list2.Add((int) single1);
                    }
                }
                int[] numArray1 = new int[list2.Count];
                list2.CopyTo(numArray1);
                object[] objArray1 = new object[this.values.Count];
                this.values.CopyTo(objArray1);
                Array.Sort(numArray1, objArray1);
                if (num1 == num2)
                {
                    if (this.values.Count > 0)
                    {
                        this.FromWhat = this.values[0].ToString();
                        this.ToWhat = this.values[this.values.Count - 1].ToString();
                    }
                    this.RemoveAttribute("keyTimes");
                    this.RemoveAttribute("values");
                    this.Duration = 0;
                    this.Begin = num1;
                }
                else
                {
                    string text1 = string.Empty;
                    for (int num4 = 0; num4 < numArray1.Length; num4++)
                    {
                        float single2 = ((float) numArray1[num4]) / ((float) (num2 - num1));
                        text1 = text1 + single2.ToString();
                        if (num4 != (numArray1.Length - 1))
                        {
                            text1 = text1 + ";";
                        }
                    }
                    AttributeFunc.SetAttributeValue(this, "keyTimes", text1);
                    text1 = string.Empty;
                    for (int num5 = 0; num5 < objArray1.Length; num5++)
                    {
                        text1 = text1 + objArray1[num5].ToString();
                        if (num5 != (objArray1.Length - 1))
                        {
                            text1 = text1 + ";";
                        }
                    }
                    AttributeFunc.SetAttributeValue(this, "values", text1);
                    this.Begin = num1;
                    this.Duration = num2 - num1;
                }
            }
        }

        private object FCumT(float time, DomType domtype)
        {
            if (time <= (this.Begin + this.Duration))
            {
                return this.FT(time, domtype);
            }
            int num1 = (int) Math.Floor((double) ((time - this.Begin) / ((float) this.Duration)));
            float single1 = (time - (num1 * this.Duration)) - this.Begin;
            string text1 = string.Empty;
            if (this.GetAttribute("accumulate").Trim() == "sum")
            {
                return this.FT(this.Begin + single1, domtype);
            }
            return this.FT(this.Begin + single1, domtype);
        }

        private object FFT(float time, DomType domtype)
        {
            if (time <= (this.Begin + this.RealDur))
            {
                return this.FCumT(time, domtype);
            }
            if (this.GetAttribute("fill").Trim() == "freeze")
            {
                return this.FCumT(this.Begin + this.RealDur, domtype);
            }
            return null;
        }

        private object FRT(float time, DomType domtype)
        {
            float single1 = this.Remainder(time, (float) this.Duration);
            return this.FT(single1, domtype);
        }

        public virtual object FT(float time, DomType domtype)
        {
            if ((time < this.Begin) || (time > (this.Begin + this.Duration)))
            {
                return null;
            }
            ArrayList list1 = (ArrayList) this.Values.Clone();
            ArrayList list2 = (ArrayList) this.KeyTimes.Clone();
            float single1 = 0f;
            float single2 = 1f;
            float single3 = this.Begin;
            float single4 = this.Duration;
            if (single4 == 0f)
            {
                single4 = 0.1f;
            }
            if (list1.Count == 0)
            {
                list1.Add(this.FromWhat);
                list2.Add(single3.ToString());
                list1.Add(this.ToWhat);
                float single7 = single3 + single4;
                list2.Add(single7.ToString());
            }
            int num1 = -1;
            float single5 = 0f;
            float single6 = 0f;
            for (int num2 = 1; num2 < list2.Count; num2++)
            {
                single5 = float.Parse(((string) list2[num2 - 1]).Trim());
                single6 = float.Parse(((string) list2[num2]).Trim());
                if ((time >= single5) && (time <= single6))
                {
                    num1 = num2 - 1;
                    break;
                }
            }
            string text1 = (string) list1[num1];
            string text2 = (string) list1[num1 + 1];
            if (list2[num1] != null)
            {
                single1 = float.Parse((string) list2[num1]);
            }
            if (list2[num1 + 1] != null)
            {
                single2 = float.Parse((string) list2[num1 + 1]);
            }
            string text3 = string.Empty;
            string[] textArray1 = AnimFunc.Linear(text1.Trim(), single1, text2.Trim(), single2, domtype, (int) time);
            string[] textArray2 = textArray1;
            for (int num3 = 0; num3 < textArray2.Length; num3++)
            {
                string text4 = textArray2[num3];
                text3 = text3 + text4 + " ";
            }
            switch (domtype)
            {
                case DomType.SvgMatrix:
                {
                    return new Transf(this.Type + "(" + text3.Trim() + ")").Matrix;
                }
                case DomType.SvgNumber:
                {
                    return ItopVector.Core.Func.Number.parseToFloat(text3.Trim(), (SvgElement) this.ParentNode, ItopVector.Core.Func.SvgLengthDirection.Horizontal);
                }
                case DomType.SvgColor:
                {
                    return text3.Trim();
                }
                case DomType.SvgPath:
                {
                    SvgElement element1 = this.RefElement;
                    if (element1 is IPath)
                    {
                        return PathFunc.PathDataParse(text3, ((IPath) element1).PointsInfo);
                    }
                    return PathFunc.PathDataParse(text3);
                }
                case DomType.SvgPoints:
                {
                    return PointsFunc.PointsParse(text3);
                }
            }
            return text3.Trim();
        }

        public virtual object GetAnimateResult(float time, DomType domtype)
        {
            if (time < this.Begin)
            {
                return string.Empty;
            }
            object obj1 = this.FFT(time, domtype);
            this.pretime = (int) time;
            return obj1;
        }

        public virtual void InsertKey(int time, string keyvalues)
        {
            if ((time >= this.Begin) && (time <= (this.Begin + this.Duration)))
            {
                int num1 = this.pretime;
                this.pretime++;
                bool flag1 = false;
                if (this.KeyTimes.Count == 0)
                {
                    if (time == this.Begin)
                    {
                        this.FromWhat = keyvalues;
                        return;
                    }
                    if (time == (this.Begin + this.Duration))
                    {
                        this.ToWhat = keyvalues;
                        return;
                    }
                    this.keyTimes = (ArrayList) this.VirtualTimes.Clone();
                    this.values.Add(this.FromWhat);
                    this.values.Add(this.ToWhat);
                }
                for (int num2 = 0; num2 < this.KeyTimes.Count; num2++)
                {
                    float single1 = float.Parse((string) this.KeyTimes[num2]);
                    float single2 = time;
                    if ((single1 - single2) >= 1f)
                    {
                        this.KeyTimes.Insert(num2, time.ToString());
                        this.values.Insert(num2, keyvalues);
                        flag1 = true;
                        break;
                    }
                    if (Math.Abs((float) (single1 - single2)) < 1f)
                    {
                        this.values[num2] = keyvalues;
                        flag1 = true;
                        break;
                    }
                }
                if (!flag1)
                {
                    this.KeyTimes.Add(time.ToString());
                    this.values.Add(keyvalues);
                }
                if (base.OwnerDocument.AcceptChanges)
                {
                    SvgDocument document1 = base.OwnerDocument;
                    document1.NumberOfUndoOperations++;
                }
                string text1 = "";
                for (int num3 = 0; num3 < this.KeyTimes.Count; num3++)
                {
                    float single3 = (float.Parse((string) this.KeyTimes[num3]) - this.Begin) / ((float) this.Duration);
                    if (num3 == (this.KeyTimes.Count - 1))
                    {
                        text1 = text1 + single3.ToString();
                    }
                    else
                    {
                        text1 = text1 + single3.ToString() + ";";
                    }
                }
                AttributeFunc.SetAttributeValue(this, "keyTimes", text1);
                text1 = "";
                if (this.values != null)
                {
                    for (int num4 = 0; num4 < this.values.Count; num4++)
                    {
                        if (num4 == (this.values.Count - 1))
                        {
                            text1 = text1 + this.values[num4].ToString();
                        }
                        else
                        {
                            text1 = text1 + this.values[num4].ToString() + ";";
                        }
                    }
                    AttributeFunc.SetAttributeValue(this, "values", text1);
                }
                this.pretime = num1;
            }
        }

        private float Remainder(float t, float dur)
        {
            float single1 = dur * ((float) Math.Floor((double) (t / dur)));
            return (t - single1);
        }


        // Properties
        public string AttributeName
        {
            get
            {
                if (this is MotionAnimate)
                {
                    return "transform";
                }
                return this.GetAttribute("attributeName");
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "attributeName", value);
            }
        }

        public int Begin
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("begin") && (this.svgAnimAttributes["begin"] is SvgTime))
                {
                    float single1 = ((SvgTime) this.svgAnimAttributes["begin"]).Value * this.Speed;
                    return (int) single1;
                }
                return 0;
            }
            set
            {
                double num1 = Math.Round((double) (((float) value) / ((float) this.Speed)), 2);
                AttributeFunc.SetAttributeValue(this, "begin", num1.ToString() + "s");
            }
        }

        public override bool CanAnimate
        {
            get
            {
                return false;
            }
        }

        public int Duration
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("dur") && (this.svgAnimAttributes["dur"] is SvgTime))
                {
                    float single1 = ((SvgTime) this.svgAnimAttributes["dur"]).Value * this.Speed;
                    return (int) Math.Max((float) 0f, single1);
                }
                return 0;
            }
            set
            {
                float single1 = Math.Max(0, value);
                double num1 = Math.Round((double) (single1 / ((float) this.Speed)), 2);
                AttributeFunc.SetAttributeValue(this, "dur", num1.ToString() + "s");
            }
        }

        public string FromWhat
        {
            get
            {
                return this.GetAttribute("from").Trim();
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "from", value);
            }
        }

        public bool IsTransformCenter
        {
            get
            {
                if (this.Values.Count > 0)
                {
                    string text1 = (string) this.values[0];
                    int num1 = this.values.LastIndexOf(text1);
                    if (num1 > 0)
                    {
                        return true;
                    }
                }
                else if (this.FromWhat == this.ToWhat)
                {
                    return true;
                }
                return false;
            }
        }

        public ArrayList KeyTimes
        {
            get
            {
                if (this.pretime == -1)
                {
                    string text1 = this.GetAttribute("keyTimes");
                    if ((text1 != null) && (text1 != string.Empty))
                    {
                        ArrayList list1 = new ArrayList(StringListFunc.SplitString(this.GetAttribute("keyTimes"), new char[3] { ';', ',', ' ' } ));
                        this.keyTimes.Clear();
                        int num1 = this.Begin;
                        int num2 = this.Duration;
                        foreach (string text2 in list1)
                        {
                            float single1 = num1 + (ItopVector.Core.Func.Number.ParseFloatStr(text2) * num2);
                            this.keyTimes.Add(single1.ToString());
                        }
                    }
                    if ((this.Values.Count > 0) && (this.keyTimes.Count == 0))
                    {
                        this.CreateValues();
                    }
                }
                return this.keyTimes;
            }
            set
            {
                if (this.keyTimes != value)
                {
                    this.keyTimes = value;
                    string text1 = string.Empty;
                    for (int num1 = 0; num1 < this.keyTimes.Count; num1++)
                    {
                        if (num1 == this.keyTimes.Count)
                        {
                            text1 = text1 + this.keyTimes[num1].ToString();
                        }
                        else
                        {
                            text1 = text1 + this.keyTimes[num1].ToString() + ";";
                        }
                    }
                    AttributeFunc.SetAttributeValue(this, "keyTimes", text1);
                }
            }
        }

        public float RealDur
        {
            get
            {
                if (this.pretime == base.OwnerDocument.ControlTime)
                {
                    goto Label_00C2;
                }
                float single1 = this.Duration;
                try
                {
                    string text1 = this.GetAttribute("repeatCount");
                    string text2 = this.GetAttribute("repeatDur");
                    if ((text1 != null) && (text1 != string.Empty))
                    {
                        if (text1 == "indefinite")
                        {
                            single1 = 2.047484E+09f;
                            goto Label_00BB;
                        }
                        float single2 = 1f;
                        if (text1 == "none")
                        {
                            single2 = 1f;
                        }
                        else
                        {
                            single2 = ItopVector.Core.Func.Number.ParseFloatStr(text1);
                        }
                        single1 *= single2;
                    }
                    else if ((text2 != null) && (text2 != string.Empty))
                    {
                        if (text2 == "indefinite")
                        {
                            single1 = 2.047484E+09f;
                        }
                        else
                        {
                            float single3 = ItopVector.Core.Func.Number.ParseFloatStr(text2);
                            single1 = single3;
                        }
                    }
                }
                catch (Exception)
                {
                }
            Label_00BB:
                this.realdur = single1;
            Label_00C2:
                return this.realdur;
            }
        }

        public SvgElement RefElement
        {
            get
            {
                string text1 = this.GetAttribute("href", "xlink");
                if (text1.Length > 0)
                {
                    bool flag1 = base.OwnerDocument.AcceptChanges;
                    base.OwnerDocument.AcceptChanges = false;
                    XmlNode node1 = base.OwnerDocument.SelectSingleNode("//*[@id='" + text1.Trim() + "']");
                    if (node1 is SvgElement)
                    {
                        return (SvgElement) node1;
                    }
                }
                if (this.ParentNode is SvgElement)
                {
                    return (SvgElement) this.ParentNode;
                }
                return null;
            }
        }

        public string ToWhat
        {
            get
            {
                return this.GetAttribute("to").Trim();
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "to", value);
            }
        }

        public virtual string Type
        {
            get
            {
                if (this is MotionAnimate)
                {
                    return "translate";
                }
                return null;
            }
            set
            {
            }
        }

        public virtual ArrayList Values
        {
            get
            {
                if (this.pretime == -1)
                {
                    string text1 = this.GetAttribute("values");
                    if ((text1 != null) && (text1 != string.Empty))
                    {
                        char[] chArray1 = new char[1] { ';' } ;
                        this.values = new ArrayList(StringListFunc.SplitString(text1, chArray1));
                    }
                    if (this.values.Count == 1)
                    {
                        this.values.Add(this.values[0]);
                    }
                }
                return this.values;
            }
            set
            {
                if (this.values != value)
                {
                    this.values = value;
                    string text1 = string.Empty;
                    for (int num1 = 0; num1 < this.keyTimes.Count; num1++)
                    {
                        if (num1 == this.keyTimes.Count)
                        {
                            text1 = text1 + this.values[num1].ToString();
                        }
                        else
                        {
                            text1 = text1 + this.values[num1].ToString() + ";";
                        }
                    }
                    AttributeFunc.SetAttributeValue(this, "values", text1);
                }
            }
        }

        public ArrayList VirtualTimes
        {
            get
            {
                ArrayList list1 = new ArrayList(0x10);
                if (this.KeyTimes.Count == 0)
                {
                    list1.Add(this.Begin.ToString());
                    int num2 = this.Begin + this.Duration;
                    list1.Add(num2.ToString());
                    return list1;
                }
                list1.AddRange(this.KeyTimes);
                return list1;
            }
        }


        // Fields
        public Accumulate Accumulate;
        public Additive Additive;
        public AttributeType AttributeType;
        public string By;
        public CalcMode CalcMode;
        public ArrayList KeySplines;
        protected ArrayList keyTimes;
        protected string max;
        protected string min;
        private float realdur;
        public string repeatCount;
        public string RepeatDur;
        public Restart Restart;
        public int Speed;
        protected ArrayList values;
        public string Xlink;
    }
}

