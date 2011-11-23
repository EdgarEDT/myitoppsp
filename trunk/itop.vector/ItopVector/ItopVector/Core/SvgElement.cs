namespace ItopVector.Core
{
    using ItopVector.Core.Animate;
    using ItopVector.Core.ClipAndMask;
    using ItopVector.Core.Document;
    using ItopVector.Core.Figure;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Core.Types;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text.RegularExpressions;
    using System.Xml;
	using System.Windows.Forms;

    public class SvgElement :XmlElement, ISvgElement
    {
        // Methods
        internal SvgElement(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
            this.timeLineVisible = true;
            KeyInfo[] infoArray1 = new KeyInfo[1] { new KeyInfo(0, 0) } ;
            this.infoList = new ArrayList(infoArray1);
            this.showParticular = false;
            this.inKey = false;
            this.animatelist = new SvgElementCollection();
            this.svgAttributes = new Hashtable(0x10);
            this.svgAnimAttributes = new Hashtable(0x10);
            this.svgStyleAttributes = new Hashtable(0x10);
            this.beforeChangeValueStr = string.Empty;
            this.showchild = true;
            this.pretime = -1;
            this.AnimateNameValues = new Hashtable(0x10);
            this.AttributePos = new Hashtable();
            this.FormatOutXml = true;
            this.UseElement = null;
            this.CreateParse = true;
            this.ownerDocument = doc;
			this.FormatElement = false;
			this.AllowRename = true;
        }

        public void AdaptKey(int index, int delta)
        {
            KeyInfo info1 = (KeyInfo) this.infoList[index];
            SvgElementCollection.ISvgElementEnumerator enumerator1 = this.AnimateList.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                ItopVector.Core.Animate.Animate animate1 = (ItopVector.Core.Animate.Animate) enumerator1.Current;
                animate1.AdaptKeyTime(info1.keytime, delta);
            }
            this.infoList[index] = new KeyInfo(info1.keytime + delta, info1.keytype);
        }

        public void AdaptKeys(int index, int delta)
        {
            for (int num1 = index; num1 < this.infoList.Count; num1++)
            {
                this.AdaptKey(num1, delta);
            }
        }

        public void AdaptKeys(int keytime, int dur, int delta)
        {
            if (Math.Abs(delta) >= 1)
            {
                bool flag1 = false;
                KeyInfo info1 = (KeyInfo) this.InfoList[this.infoList.Count - 1];
                int num1 = info1.keytime;
                int num2 = info1.keytime + info1.controlLength;
                if (keytime <= (info1.keytime + info1.controlLength))
                {
                    flag1 = true;
                }
                byte num3 = 1;
                ArrayList list1 = new ArrayList(0x10);
                ArrayList list2 = new ArrayList(0x10);
                bool flag2 = false;
                for (int num4 = 0; num4 < this.infoList.Count; num4++)
                {
                    KeyInfo info2 = (KeyInfo) this.InfoList[num4];
                    KeyInfo info6 = (KeyInfo) this.InfoList[num4];
                    byte num5 = info6.keytype;
                    int num6 = info2.keytime;
                    if ((num6 >= keytime) && !flag2)
                    {
                        if ((num4 - 1) >= 0)
                        {
                            KeyInfo info7 = (KeyInfo) this.InfoList[num4 - 1];
                            num5 = info7.keytype;
                        }
                        num3 = num5;
                        flag2 = true;
                    }
                    if ((num6 < keytime) || (num6 > (keytime + dur)))
                    {
                        if ((num6 > (keytime + delta)) && (num6 < ((keytime + dur) + delta)))
                        {
                            list1.Add(info2);
                        }
                    }
                    else
                    {
                        list2.Add(info2);
                        if (num6 == keytime)
                        {
                            flag1 = false;
                        }
                    }
                    if (num6 > Math.Max((int) (keytime + dur), (int) ((keytime + dur) + delta)))
                    {
                        break;
                    }
                }
                foreach (KeyInfo info3 in list1)
                {
                    this.RemoveKeyTime(info3.keytime);
                }
                foreach (KeyInfo info4 in list2)
                {
                    int num7 = this.infoList.IndexOf(info4);
                    if (num7 >= 0)
                    {
                        this.AdaptKey(num7, delta);
                    }
                }
                if (flag1)
                {
                    this.InsertKey((int) (keytime + delta), num3);
                }
                KeyInfo info5 = (KeyInfo) this.InfoList[this.InfoList.Count - 1];
                if (((keytime + dur) + delta) > info5.keytime)
                {
                    this.InsertKey((int) ((keytime + dur) + delta), (byte) 0);
                }
            }
        }

        public void AddAnim(ItopVector.Core.Animate.Animate anim)
        {
            this.AppendChild(anim);
        }

        public virtual void AddAttribute(XmlAttribute newattr)
        {
            if (newattr != null)
            {
                Point point1 = Point.Empty;
                if (this.ownerDocument.XmlTextReader != null)
                {
                    int num1 = this.ownerDocument.XmlTextReader.LineNumber;
                    int num2 = this.ownerDocument.XmlTextReader.LinePosition;
                    if (!this.AttributePos.ContainsKey(newattr))
                    {
                        this.AttributePos.Add(newattr, new Point(num1, num2));
                    }
                    point1 = new Point(num1, num2);
                }
                this.ParseAttribute(newattr.Name.Trim(), newattr.Value, true, point1);
            }
        }

        private void AddFlowElement(SvgElement element)
        {
            if (element is ItopVector.Core.Animate.Animate)
            {
                int num1 = this.OwnerDocument.FlowChilds.IndexOf(this);
                if ((num1 >= 0) && this.showParticular)
                {
                    num1 += this.animatelist.Count;
                    if ((num1 + 1) < this.OwnerDocument.FlowChilds.Count)
                    {
                        this.OwnerDocument.InsertFlowElement(num1 + 1, element);
                    }
                    else
                    {
                        this.OwnerDocument.AddFlowElement(element);
                    }
                }
            }
        }

        public override XmlNode AppendChild(XmlNode newChild)
        {
            if (newChild == null)
            {
                return null;
            }
            bool flag1 = this.ownerDocument.AcceptChanges;
			if (newChild.OwnerDocument != this.OwnerDocument)
			{
				XmlNode node1 = newChild.Clone();
				this.ownerDocument.AcceptChanges = false;
				XmlNode node2 = this.ownerDocument.ImportNode(node1, true);
				newChild = node2;
				this.ownerDocument.AcceptChanges = flag1;
			}
			else if (!(newChild is Symbol))
			{
				
				this.CreateID(newChild);
			}
            
            XmlNode node3 = base.AppendChild(newChild);
            if ((node3 is ItopVector.Core.Animate.Animate) && !this.animatelist.Contains((SvgElement) node3))
            {
                if (this.showParticular)
                {
                    this.AddFlowElement((SvgElement) node3);
                }
                this.animatelist.Add((SvgElement) node3);
            }
            if (node3 is SvgElement)
            {
				CodeFunc.FormatElement((SvgElement) node3);
            }
            return node3;
        }

        public override XmlNode Clone()
        {
            bool flag1 = this.ownerDocument.AcceptChanges;
            this.ownerDocument.AcceptChanges = false;
            SvgElement element1 = (SvgElement) base.Clone();
            this.ownerDocument.AcceptChanges = flag1;
            return element1;
        }

        public virtual SvgElement[] CloneElements()
        {
            return new SvgElement[1] { ((SvgElement) this.Clone()) } ;
        }

        private void CreateID(XmlNode child)
        {
            if (child is SvgElement && (child as SvgElement).AllowRename)
            {
                string text1 = child.LocalName;
//                if (((SvgElement) child).GetAttribute("id").Length > 0)
//                {
//                    text1 = ((SvgElement) child).GetAttribute("id").Trim();
//                }
                bool flag1 = this.ownerDocument.AcceptChanges;
                this.ownerDocument.AcceptChanges = false;
                text1 = CodeFunc.CreateString(this, text1);
                AttributeFunc.SetAttributeValue((SvgElement) child, "id", text1);
				
                this.ownerDocument.AcceptChanges = flag1;

            }
        }

        public virtual ElementType GetElementType()
        {
            return ElementType.SvgElement;
        }

        public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
        {
            if (newChild.OwnerDocument != this.OwnerDocument)
            {
                XmlNode node1 = newChild.Clone();
                bool flag1 = this.ownerDocument.AcceptChanges;
                this.ownerDocument.AcceptChanges = false;
                XmlNode node2 = this.ownerDocument.ImportNode(node1, true);
                newChild = node2;
                this.ownerDocument.AcceptChanges = flag1;
            }
			
            this.CreateID(newChild);
            XmlNode node3 = base.InsertAfter(newChild, refChild);
            if (node3 is ItopVector.Core.Animate.Animate)
            {
                XmlNode node4 = refChild;
                while (!(node4 is ItopVector.Core.Animate.Animate))
                {
                    if (node4 == null)
                    {
                        break;
                    }
                    node4 = node4.PreviousSibling;
                }
                if ((node4 is ItopVector.Core.Animate.Animate) && !this.animatelist.Contains((SvgElement) node3))
                {
                    int num1 = this.animatelist.IndexOf((SvgElement) node4);
                    if ((num1 + 1) < this.animatelist.Count)
                    {
                        this.animatelist.Add((SvgElement) node3);
                    }
                    else
                    {
                        this.animatelist.Insert(num1 + 1, (SvgElement) node3);
                    }
                }
            }
            if (node3 is SvgElement)
            {
                CodeFunc.FormatElement((SvgElement) node3);
            }
            return node3;
        }

        public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
        {
            bool flag1 = this.ownerDocument.AcceptChanges;
            if (newChild.OwnerDocument != this.OwnerDocument)
            {
                XmlNode node1 = newChild.Clone();
                this.ownerDocument.AcceptChanges = false;
                XmlNode node2 = this.ownerDocument.ImportNode(node1, true);
                newChild = node2;
                this.ownerDocument.AcceptChanges = flag1;
            }
            this.CreateID(newChild);
            XmlNode node3 = base.InsertBefore(newChild, refChild);
            if (node3 is ItopVector.Core.Animate.Animate)
            {
                XmlNode node4 = refChild;
                while (!(node4 is ItopVector.Core.Animate.Animate))
                {
                    if (node4 == null)
                    {
                        break;
                    }
                    node4 = node4.NextSibling;
                }
                if (node4 is ItopVector.Core.Animate.Animate)
                {
                    if (!this.animatelist.Contains((SvgElement) node3))
                    {
                        int num1 = this.animatelist.IndexOf((SvgElement) node4);
                        if (num1 >= 0)
                        {
                            this.animatelist.Insert(num1, (SvgElement) node3);
                            num1 = this.ownerDocument.FlowChilds.IndexOf((SvgElement) node4);
                            this.InsertFlowElment(num1 + this.animatelist.IndexOf((SvgElement) node3), (SvgElement) node3);
                            goto Label_016B;
                        }
                        this.animatelist.Insert(0, (SvgElement) node3);
                        this.InsertFlowElment(0, (SvgElement) node3);
                    }
                }
                else if (!this.animatelist.Contains((SvgElement) node3))
                {
                    this.animatelist.Insert(0, (SvgElement) node3);
                    this.InsertFlowElment(0, (SvgElement) node3);
                }
            }
        Label_016B:
            if (node3 is SvgElement)
            {
                CodeFunc.FormatElement((SvgElement) node3);
            }
            return node3;
        }

        private void InsertFlowElment(int index, SvgElement element)
        {
            if (element is ItopVector.Core.Animate.Animate)
            {
                int num1 = this.OwnerDocument.FlowChilds.IndexOf(this);
                if (this.showParticular)
                {
                    num1 += (index + 1);
                    if ((num1 >= 0) && (num1 < this.OwnerDocument.FlowChilds.Count))
                    {
                        this.OwnerDocument.InsertFlowElement(num1, element);
                    }
                    else
                    {
                        this.OwnerDocument.AddFlowElement(element);
                    }
                }
            }
        }

        public int InsertKey(int keytime, byte keytype)
        {
            ArrayList list1 = (ArrayList) this.infoList.Clone();
            int num1 = 0;
            KeyInfo info1 = new KeyInfo(keytime, keytype);
            num1 = this.infoList.IndexOf(info1);
            if (num1 >= 0)
            {
                this.infoList[num1] = info1;
                return num1;
            }
            if (this.infoList.Count == 0)
            {
                this.infoList.Add(info1);
                return 0;
            }
            bool flag1 = true;
            int num2 = 0;
            foreach (KeyInfo info2 in this.infoList)
            {
                if (info1 < info2)
                {
                    if ((num2 - 1) >= 0)
                    {
                        KeyInfo info3 = (KeyInfo) this.infoList[num2 - 1];
                        info1.keytype = info3.keytype;
                    }
                    this.infoList.Insert(num2, info1);
                    flag1 = false;
                    num1 = num2;
                    break;
                }
                num2++;
                num1++;
            }
            if (flag1)
            {
                KeyInfo info4 = (KeyInfo) this.infoList[this.infoList.Count - 1];
                info1.keytype = info4.keytype;
                this.infoList.Add(info1);
            }
            return num1;
        }

        public void InsertKey(int keytime, int delta)
        {
            ArrayList list1 = (ArrayList) this.infoList.Clone();
            int num1 = 0;
            for (int num2 = 0; num2 < this.infoList.Count; num2++)
            {
                KeyInfo info1 = (KeyInfo) this.InfoList[num2];
                if (info1.keytime >= keytime)
                {
                    num1 = num2;
                    break;
                }
            }
            for (int num3 = num1; num3 < this.InfoList.Count; num3++)
            {
                this.AdaptKey(num1, delta);
            }
        }

        public void ParseAttribute(string localname, string valuestr, bool add)
        {
            this.ParseAttribute(localname, valuestr, add, Point.Empty);
        }

        public void ParseAttribute(string localname, string valuestr, bool add, Point p)
        {
            if (this.CreateParse)
            {
                if (localname == "clip-path")
                {
                    int num1 = this.OwnerDocument.FlowChilds.IndexOf(this);
                    XmlNode node1 = NodeFunc.GetRefNode(valuestr, this.ownerDocument);
                    if (((node1 is ClipPath) && (this is IGraph)) && !this.ownerDocument.FlowChilds.Contains((SvgElement) node1))
                    {
                        if ((num1 + 1) < this.OwnerDocument.FlowChilds.Count)
                        {
                            this.OwnerDocument.InsertFlowElement(num1 + 1, (SvgElement) node1);
                        }
                        else
                        {
                            this.OwnerDocument.AddFlowElement((SvgElement) node1);
                        }
                    }
                }
                localname = localname.Trim();
//				if(localname!="id")
				valuestr = valuestr.Trim();
                if (localname == "style")
                {
                    string text1 = valuestr.Trim();
                    string text2 = @"[\t\n;\t\n]+";
                    Regex regex1 = new Regex(text2, RegexOptions.IgnoreCase);
                    regex1.Replace(text1, ";");
                    text2 = ";";
                    string[] textArray2 = regex1.Split(text1);

					ArrayList list1 =new ArrayList();

                    for (int num3 = 0; num3 < textArray2.Length; num3++)
                    {
                        string text3 = textArray2[num3];
                        int num2 = text3.IndexOf(":");
                        if (num2 >= 0)
                        {
							list1.Add(text3.Substring(0, num2));
                            this.ParseAttribute(text3.Substring(0, num2), text3.Substring(num2 + 1, (text3.Length - num2) - 1), add, p);
                        }
                    }

					ArrayList list2 =new ArrayList(this.svgAnimAttributes.Keys);
					foreach(object key in list2)					
					{
						
						if(AttributeFunc.StyleAttributes.Contains(key)&&!list1.Contains(key))
						{
							this.svgAnimAttributes.Remove(key);
						}
					}
                }
                else
                {
                    try
                    {
                        GraphicsPath path1;
                        Matrix matrix1;
                        SvgTime time1;
                        PointF[] tfArray1;
                        if (!add)
                        {
                            goto Label_0627;
                        }
                        switch (DomTypeFunc.GetTypeOfAttributeName(localname))
                        {
                            case DomType.SvgMatrix:
                            {
                                matrix1 = new Matrix();
                                matrix1 = new Transf(valuestr).Matrix;
                                if (!this.svgAttributes.ContainsKey(localname))
                                {
                                    goto Label_0301;
                                }
                                this.svgAttributes[localname] = matrix1;
                                goto Label_030F;
                            }
                            case DomType.SvgNumber:
                            {
                                float single1 = 0f;
                                ItopVector.Core.Func.SvgLengthDirection direction1 = ItopVector.Core.Func.SvgLengthDirection.Horizontal;
                                if ((localname == "height") || (localname == "y"))
                                {
                                    direction1 = ItopVector.Core.Func.SvgLengthDirection.Vertical;
                                }
                                single1 = ItopVector.Core.Func.Number.parseToFloat(valuestr, this, direction1);
                                if (this.svgAttributes.ContainsKey(localname))
                                {
                                    this.svgAttributes[localname] = single1;
                                }
                                else
                                {
                                    this.svgAttributes.Add(localname, single1);
                                }
                                if (this.svgAttributes.ContainsKey(localname))
                                {
                                    this.svgAnimAttributes[localname] = single1;
                                    goto Label_0625;
                                }
                                this.svgAnimAttributes.Add(localname, single1);
                                return;
                            }
                            case DomType.SvgString:
                            case DomType.SvgColor:
                            {
                                goto Label_04F1;
                            }
                            case DomType.SvgPath:
                            {
                                path1 = new GraphicsPath();
                                if (!(this is IPath))
                                {
                                    break;
                                }
                                PointF tf1 = PointF.Empty;
                                path1 = PathFunc.PathDataParse(valuestr, ((IPath) this).PointsInfo);
                                goto Label_01CF;
                            }
                            case DomType.SvgPoints:
                            {
                                tfArray1 = null;
                                if (!(this is IPath))
                                {
                                    goto Label_03CA;
                                }
                                tfArray1 = PointsFunc.PointsParse(valuestr, ((IPath) this).PointsInfo);
                                goto Label_03D2;
                            }
                            case DomType.SvgTime:
                            {
								if (this is ConnectLine)goto Label_04F1;
                                time1 = new SvgTime(valuestr.Trim());
                                if (!this.svgAttributes.ContainsKey(localname))
                                {
                                    goto Label_036B;
                                }
                                this.svgAttributes[localname] = time1;
                                goto Label_0379;
                            }
                            case DomType.SvgLink:
                            {
                                if (!(this is ItopVector.Core.Figure.Image))
                                {
                                    goto Label_0454;
                                }
								
								if(SvgDocument.BkImageLoad)
								{
									Bitmap bitmap1;
									if(valuestr=="")
									{
										bitmap1=null;
									}
									else
									{
                                        bitmap1 = ImageFunc.GetImageForURL(valuestr.Trim(),this);
									}
									((ItopVector.Core.Figure.Image) this).RefImage = bitmap1;
								}
								else
								{
									Bitmap bitmap1 = ImageFunc.GetImageForURL(valuestr.Trim(), this);//wlwl
									((ItopVector.Core.Figure.Image) this).RefImage = bitmap1;
								}
                                goto Label_0498;
                            }
                            default:
                            {
                                goto Label_04F1;
                            }
                        }
                        path1 = PathFunc.PathDataParse(valuestr);
                    Label_01CF:
                        if (this.svgAttributes.ContainsKey(localname))
                        {
                            this.svgAttributes[localname] = path1;
                        }
                        else
                        {
                            this.svgAttributes.Add(localname, path1);
                        }
                        if (this.svgAttributes.ContainsKey(localname))
                        {
                            this.svgAnimAttributes[localname] = path1;
                            goto Label_0625;
                        }
                        this.svgAnimAttributes.Add(localname, path1);
                        return;
                    Label_0301:
                        this.svgAttributes.Add(localname, matrix1);
                    Label_030F:
                        if (this.svgAttributes.ContainsKey(localname))
                        {
                            this.svgAnimAttributes[localname] = matrix1;
                            goto Label_0625;
                        }
                        this.svgAnimAttributes.Add(localname, matrix1);
                        return;
                    Label_036B:
                        this.svgAttributes.Add(localname, time1);
                    Label_0379:
                        if (this.svgAttributes.ContainsKey(localname))
                        {
                            this.svgAnimAttributes[localname] = time1;
                            goto Label_0625;
                        }
                        this.svgAnimAttributes.Add(localname, time1);
                        return;
                    Label_03CA:
                        tfArray1 = PointsFunc.PointsParse(valuestr);
                    Label_03D2:
                        if (this.svgAttributes.ContainsKey(localname))
                        {
                            this.svgAttributes[localname] = tfArray1;
                        }
                        else
                        {
                            this.svgAttributes.Add(localname, tfArray1);
                        }
                        if (this.svgAttributes.ContainsKey(localname))
                        {
                            this.svgAnimAttributes[localname] = tfArray1;
                            goto Label_0625;
                        }
                        this.svgAnimAttributes.Add(localname, tfArray1);
                        return;
                    Label_0454:
                        if (this is Use)
                        {
                            XmlNode node2 = NodeFunc.GetRefNode(valuestr.Trim(), this.ownerDocument);
                            if (node2 is SvgElement)
                            {
                                ((Use) this).RefElement = (SvgElement) node2;
                            }
                            else
                            {
                                ((Use) this).RefElement = null;
                            }
                        }
                    Label_0498:
                        if (this.svgAttributes.ContainsKey(localname))
                        {
                            this.svgAttributes[localname] = valuestr;
                        }
                        else
                        {
                            this.svgAttributes.Add(localname, valuestr);
                        }
                        if (this.svgAnimAttributes.ContainsKey(localname))
                        {
                            this.svgAnimAttributes[localname] = valuestr;
                            goto Label_0625;
                        }
                        this.svgAnimAttributes.Add(localname, valuestr);
                        return;
                    Label_04F1:
                        if (this.svgAttributes.ContainsKey(localname))
                        {
                            string text4 = this.svgAttributes[localname].ToString();
                            this.svgAttributes[localname] = valuestr;
                            if (((this is IGraphPath) && (text4 != valuestr)) && (this.ParentNode != null))
                            {
                                if (localname == "stroke")
                                {
                                    ((IGraphPath) this).GraphStroke.Brush = BrushManager.Parsing(valuestr, this.ownerDocument);
                                    goto Label_05F9;
                                }
                                if (localname == "fill")
                                {
                                    ((IGraphPath) this).GraphBrush = BrushManager.Parsing(valuestr, this.ownerDocument);
                                }
                            }
//							if (localname == "layer")
//							{
//                                ISvgElement element1= ownerDocument.Layers[text4] ;
//								if(element1!=null )
//								{
//									(element1 as ILayer).Remove(this);
//								}
//								element1= ownerDocument.Layers[valuestr] ;
//								if(element1!=null )
//								{
//									(element1 as ILayer).Add(this);
//								}
//							}
                        }
                        else
                        {
                            this.svgAttributes.Add(localname, valuestr);
                            if ((this is IGraphPath) && (this.ParentNode != null))
                            {
                                if (localname == "stroke")
                                {
                                    ((IGraphPath) this).GraphStroke.Brush = BrushManager.Parsing(valuestr, this.ownerDocument);
                                }
                                else if (localname == "fill")
                                {
                                    ((IGraphPath) this).GraphBrush = BrushManager.Parsing(valuestr, this.ownerDocument);
                                }
                            }
//							if (localname == "layer")
//							{
//								ISvgElement 	element1= ownerDocument.Layers[valuestr] ;
//								if(element1!=null )
//								{
//									(element1 as ILayer).Add(this);
//								}
//							}
                        }
                    Label_05F9:
						
                        if (this.svgAnimAttributes.ContainsKey(localname))
                        {
                            this.svgAnimAttributes[localname] = valuestr;
                        }
                        else
                        {
                            this.svgAnimAttributes.Add(localname, valuestr);
                        }/**/
                    Label_0625:
                        return;
                    Label_0627:
                        this.svgAnimAttributes.Remove(localname);
                        this.svgAttributes.Remove(localname);
                    }
                    catch (Exception exception1)
                    {
                        if (!p.IsEmpty)
                        {
                            string[] textArray3 = new string[6] { exception1.Message, "¡£(", p.X.ToString(), "£¬", p.Y.ToString(), ")´¦·¢Éú´íÎó¡£" } ;
                            this.ownerDocument.ErrorInfos.Add(new SvgException(string.Concat(textArray3), p.X, p.Y));
                        }
                    }
                }
            }
        }

        public void PreAppendAnim(ItopVector.Core.Animate.Animate anim)
        {
            this.PrependChild(anim);
        }

        public override XmlNode PrependChild(XmlNode newChild)
        {
            if (newChild.OwnerDocument != this.OwnerDocument)
            {
                XmlNode node1 = newChild.Clone();
                bool flag1 = this.ownerDocument.AcceptChanges;
                this.ownerDocument.AcceptChanges = false;
                XmlNode node2 = this.ownerDocument.ImportNode(node1, true);
                newChild = node2;
                this.ownerDocument.AcceptChanges = flag1;
            }
            XmlNode node3 = base.PrependChild(newChild);
            if ((node3 is ItopVector.Core.Animate.Animate) && !this.animatelist.Contains((SvgElement) node3))
            {
                if (this.animatelist.Count == 0)
                {
                    this.animatelist.Add((SvgElement) node3);
                }
                else
                {
                    this.animatelist.Insert(0, (SvgElement) node3);
                }
                if (this.showParticular)
                {
                    this.InsertFlowElment(0, (SvgElement) node3);
                }
            }
            return node3;
        }

        public override void RemoveAll()
        {
            base.RemoveAll();
            this.animatelist.Clear();
            this.svgAttributes.Clear();
        }

        public override void RemoveAllAttributes()
        {
            base.RemoveAllAttributes();
            this.svgAttributes.Clear();
            this.svgAnimAttributes.Clear();
            if (this is IPath)
            {
                ((IPath) this).PointsInfo.Clear();
            }
        }

        public override void RemoveAttribute(string name)
        {
            base.RemoveAttribute(name);
            if (this.svgAttributes.ContainsKey(name))
            {
                this.svgAttributes.Remove(name);
            }
            if (this.svgAnimAttributes.ContainsKey(name))
            {
                this.svgAnimAttributes.Remove(name);
            }
            if (((name == "d") || (name == "path")) && (this is IPath))
            {
                ((IPath) this).PointsInfo.Clear();
            }
        }

        public virtual void RemoveAttribute(XmlAttribute oldattr)
        {
            if (oldattr != null)
            {
                this.ParseAttribute(oldattr.Name.Trim(), oldattr.Value.Trim(), false);
            }
        }

        public override void RemoveAttribute(string localname, string namespaceURI)
        {
            base.RemoveAttribute(localname, namespaceURI);
            if (this.svgAttributes.ContainsKey(localname))
            {
                this.svgAttributes.Remove(localname);
            }
            if (this.svgAnimAttributes.ContainsKey(localname))
            {
                this.svgAnimAttributes.Remove(localname);
            }
        }

        public override XmlNode RemoveAttributeAt(int i)
        {
            XmlNode node1 = base.RemoveAttributeAt(i);
            if (node1 != null)
            {
                if (this.svgAttributes.ContainsKey(node1.Name))
                {
                    this.svgAttributes.Remove(node1.Name);
                }
                if (this.svgAnimAttributes.ContainsKey(node1.Name))
                {
                    this.svgAnimAttributes.Remove(node1.Name);
                }
            }
            return node1;
        }

        public override XmlAttribute RemoveAttributeNode(XmlAttribute oldAttr)
        {
            XmlAttribute attribute1 = base.RemoveAttributeNode(oldAttr);
            if (attribute1 != null)
            {
                if (this.svgAttributes.ContainsKey(attribute1.Name))
                {
                    this.svgAttributes.Remove(attribute1.Name);
                }
                if (this.svgAnimAttributes.ContainsKey(attribute1.Name))
                {
                    this.svgAnimAttributes.Remove(attribute1.Name);
                }
            }
            return attribute1;
        }

        public override XmlAttribute RemoveAttributeNode(string localname, string ns)
        {
            XmlAttribute attribute1 = base.RemoveAttributeNode(localname, ns);
            if (attribute1 != null)
            {
                if (this.svgAttributes.ContainsKey(attribute1.Name))
                {
                    this.svgAttributes.Remove(attribute1.Name);
                }
                if (this.svgAnimAttributes.ContainsKey(attribute1.Name))
                {
                    this.svgAnimAttributes.Remove(attribute1.Name);
                }
                if ((attribute1.Name != "d") && (attribute1.Name != "path"))
                {
                    return attribute1;
                }
                if (this is IPath)
                {
                    ((IPath) this).PointsInfo.Clear();
                }
            }
            return attribute1;
        }

        public override XmlNode RemoveChild(XmlNode oldChild)
        {
            if (oldChild == this.ownerDocument.RootElement)
            {
                if (this.ownerDocument.EditRoots.Contains((SvgElement) oldChild))
                {
                    this.ownerDocument.EditRoots.Remove((SvgElement) oldChild);
                }
                this.ownerDocument.RootElement = (SvgElement) this.ownerDocument.DocumentElement;
            }
            SvgDocument document1 = this.ownerDocument;
            bool flag1 = false;
            int num1 = this.NodeDepth;
            bool flag2 = document1.AcceptChanges;
            if ((oldChild is SvgElement) && this.FormatOutXml)
            {
                XmlNode node2;
                document1.AcceptChanges = false;
                flag1 = true;
                for (XmlNode node1 = oldChild.PreviousSibling; node1 is XmlWhitespace; node1 = node2)
                {
                    if (((XmlWhitespace) node1).Value.IndexOf("\n") >= 0)
                    {
                        base.RemoveChild(node1);
                        break;
                    }
                    node2 = node1.PreviousSibling;
                    base.RemoveChild(node1);
                }
            }
            document1.AcceptChanges = flag2;
            XmlNode node3 = oldChild.NextSibling;
            XmlNode node4 = base.RemoveChild(oldChild);
            int num2 = 0;
            if (flag1)
            {
                document1.AcceptChanges = false;
                bool flag3 = false;
                while (node3 is XmlWhitespace)
                {
                    if (num2 >= this.NodeDepth)
                    {
                        break;
                    }
                    string text2 = ((XmlWhitespace) node3).Value;
                    if (text2.IndexOf("\n") >= 0)
                    {
                        flag3 = true;
                    }
                    if (flag3)
                    {
                        num2 += text2.Length;
                    }
                    XmlNode node5 = node3.NextSibling;
                    base.RemoveChild(node3);
                    node3 = node5;
                }
            }
            document1.AcceptChanges = flag2;
            if (node4 is ItopVector.Core.Animate.Animate)
            {
                if (this.animatelist.Contains((SvgElement) node4))
                {
                    this.animatelist.Remove((SvgElement) node4);
                }
                this.svgAnimAttributes = (Hashtable) this.svgAttributes.Clone();
            }
            if ((node4 is SvgElement) && this.ownerDocument.FlowChilds.Contains((SvgElement) node4))
            {
                this.ownerDocument.RemoveFlowElement((SvgElement) node4);
            }
            return node4;
        }

        public void RemoveKey(int index)
        {
            if ((index >= 0) && (index < this.infoList.Count))
            {
                for (int num1 = 0; num1 < this.animatelist.Count; num1++)
                {
                    ItopVector.Core.Animate.Animate animate1 = (ItopVector.Core.Animate.Animate) this.animatelist[num1];
                    int num2 = this.animatelist.Count;
                    KeyInfo info1 = (KeyInfo) this.infoList[index];
                    animate1.DelKeyTime(info1.keytime);
                    num1 -= (num2 - this.animatelist.Count);
                }
                this.infoList.RemoveAt(index);
            }
        }

        public void RemoveKeyTime(int keytime)
        {
            int num1 = 0;
            for (int num2 = 0; num2 < this.infoList.Count; num2++)
            {
                KeyInfo info1 = (KeyInfo) this.infoList[num2];
                if (info1.keytime == keytime)
                {
                    num1 = num2;
                    break;
                }
            }
            this.infoList.RemoveAt(num1);
        }

        public override void SetAttribute(string name, string strvalue)
        {
            base.SetAttribute(name, strvalue);
			Point point1 = Point.Empty;
/*
            XmlAttribute attribute1 = this.Attributes[name];            
            if ((this.ownerDocument.XmlTextReader != null) && (attribute1 != null))
            {
                int num1 = this.ownerDocument.XmlTextReader.LineNumber;
                int num2 = this.ownerDocument.XmlTextReader.LinePosition;
                if (!this.AttributePos.ContainsKey(attribute1))
                {
                    this.AttributePos.Add(attribute1, new Point(num1, num2));
                }
                point1 = new Point(num1, num2);
            }
*/
            this.ParseAttribute(name, strvalue, true, point1);
        }

        public override string SetAttribute(string localName, string namespaceURI, string strvalue)
        {
            string text1 = base.SetAttribute(localName, namespaceURI, strvalue);
            Point point1 = Point.Empty;
//			XmlAttribute attribute1 = this.Attributes[localName];
//            
//            if ((this.ownerDocument.XmlTextReader != null) && (attribute1 != null))
//            {
//                int num1 = this.ownerDocument.XmlTextReader.LineNumber;
//                int num2 = this.ownerDocument.XmlTextReader.LinePosition;
//                if (!this.AttributePos.ContainsKey(attribute1))
//                {
//                    this.AttributePos.Add(attribute1, new Point(num1, num2));
//                }
//                point1 = new Point(num1, num2);
//            }
            this.ParseAttribute(localName, strvalue, true, point1);
            return text1;
        }

        public override XmlAttribute SetAttributeNode(XmlAttribute newAttr)
        {
            XmlAttribute attribute1 = base.SetAttributeNode(newAttr);
            Point point1 = Point.Empty;
//            if (this.ownerDocument.XmlTextReader != null)
//            {
//                int num1 = this.ownerDocument.XmlTextReader.LineNumber;
//                int num2 = this.ownerDocument.XmlTextReader.LinePosition;
//                if (!this.AttributePos.ContainsKey(attribute1))
//                {
//                    this.AttributePos.Add(attribute1, new Point(num1, num2));
//                }
//                point1 = new Point(num1, num2);
//            }
            this.ParseAttribute(attribute1.Name, attribute1.Value, true, point1);
            return attribute1;
        }

        public override XmlAttribute SetAttributeNode(string localName, string namespaceURI)
        {
            XmlAttribute attribute1 = base.SetAttributeNode(localName, namespaceURI);
            Point point1 = Point.Empty;
//            if (this.ownerDocument.XmlTextReader != null)
//            {
//                int num1 = this.ownerDocument.XmlTextReader.LineNumber;
//                int num2 = this.ownerDocument.XmlTextReader.LinePosition;
//                if (!this.AttributePos.ContainsKey(attribute1))
//                {
//                    this.AttributePos.Add(attribute1, new Point(num1, num2));
//                }
//                point1 = new Point(num1, num2);
//            }
            this.ParseAttribute(attribute1.Name, attribute1.Value, true, point1);
            return attribute1;
        }

        public void SortKeys()
        {
            KeyInfo[] infoArray1 = new KeyInfo[this.infoList.Count];
            this.infoList.CopyTo(infoArray1);
            int[] numArray1 = new int[this.infoList.Count];
            for (int num1 = 0; num1 < infoArray1.Length; num1++)
            {
                KeyInfo info1 = infoArray1[num1];
                numArray1[num1] = info1.keytime;
            }
            Array.Sort(numArray1, infoArray1);
            this.infoList = new ArrayList(infoArray1);
        }


        // Properties
        public SvgElementCollection AnimateList
        {
            get
            {
                return this.animatelist;
            }
        }

        public string BeforeChangeValueStr
        {
            get
            {
                return this.beforeChangeValueStr;
            }
            set
            {
                this.beforeChangeValueStr = value;
            }
        }

        public virtual bool CanAnimate
        {
            get
            {
                return true;
            }
        }

        public string ID
        {
            get
            {
                return AttributeFunc.FindAttribute("id", this).ToString();
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "id", value);
            }
        }

        public ArrayList InfoList
        {
            get
            {
                return this.infoList;
            }
            set
            {
                this.infoList = value;
            }
        }

        public bool InKey
        {
            get
            {
                return this.inKey;
            }
            set
            {
                if (this.inKey != value)
                {
                    this.inKey = value;
                    this.ownerDocument.PostInkeyChanged(this);
                }
            }
        }

        public int NodeDepth
        {
            get
            {
                int num1 = 0;
                XmlNode node1 = this.ParentNode;
				while(true)
				{
					if ((node1 == null) || (node1 is XmlDocument))
					{
						return num1;
					}
					num1++;
					node1 = node1.ParentNode;
				}
            }
        }

        public new SvgDocument OwnerDocument
        {
            get
            {
                return this.ownerDocument;
            }
        }

        public bool ShowChilds
        {
            get
            {
                return this.showchild;
            }
            set
            {
                this.showchild = value;
            }
        }

        public bool ShowParticular
        {
            get
            {
                return this.showParticular;
            }
            set
            {
                if (this.showParticular != value)
                {
                    bool flag1 = this.showParticular;
                    this.showParticular = value;
                    this.OwnerDocument.ChangeElementExpand(this, flag1, true);
                }
            }
        }

        public Hashtable SvgAnimAttributes
        {
            get
            {
                return this.svgAnimAttributes;
            }
            set
            {
                this.svgAnimAttributes = value;
            }
        }

        public Hashtable SvgAttributes
        {
            get
            {
                return this.svgAttributes;
            }
            set
            {
                this.svgAttributes = value;
            }
        }

        public bool TimeLineVisible
        {
            get
            {
                if (this.ParentNode is SvgElement)
                {
                    SvgElement element1 = (SvgElement) this.ParentNode;
                    bool flag1 = element1.timeLineVisible;
                    return (this.timeLineVisible && flag1);
                }
                return this.timeLineVisible;
            }
        }

        public RectangleF ViewPort
        {
            get
            {
                XmlNode node1 = this;
                if (this.ViewportElement != null)
                {
                    IViewportElement element1 = this.ViewportElement;
                    if (element1.ViewBox != null)
                    {
                        float single1 = element1.ViewBox.min_x;
                        float single2 = element1.ViewBox.min_y;
                        float single3 = element1.ViewBox.width;
                        float single4 = element1.ViewBox.height;
                        return new RectangleF(single1, single2, single3, single4);
                    }
                    return new RectangleF(element1.X, element1.Y, element1.Width, element1.Height);
                }
                return RectangleF.Empty;
            }
        }

        public IViewportElement ViewportElement
        {
            get
            {
                for (XmlNode node1 = this; node1 != null; node1 = node1.ParentNode)
                {
                    if (node1 is IViewportElement)
                    {
                        return (IViewportElement) node1;
                    }
                }
                return null;
            }
        }

        public string XmlBase
        {
            get
            {
                return this.BaseURI.Substring(0, this.BaseURI.LastIndexOf("/"));
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public string Xmllang
        {
            get
            {
                string text1 = this.GetAttribute("xml:lang");
                if (text1 != "")
                {
                    return text1;
                }
                if (this.ParentNode is SvgElement)
                {
                    SvgElement element1 = (SvgElement) this.ParentNode;
                    return element1.Xmllang;
                }
                return "";
            }
        }

        public string XmlLang
        {
            get
            {
                string text1 = this.GetAttribute("xml:lang");
                if (text1 != "")
                {
                    return text1;
                }
                if (this.ParentNode is SvgElement)
                {
                    SvgElement element1 = (SvgElement) this.ParentNode;
                    return element1.XmlLang;
                }
                return "";
            }
        }

        public string Xmlspace
        {
            get
            {
                string text1 = this.GetAttribute("xml:space");
                if (text1 != "")
                {
                    return text1;
                }
                if (this.ParentNode is SvgElement)
                {
                    SvgElement element1 = (SvgElement) this.ParentNode;
                    return element1.Xmlspace;
                }
                return "default";
            }
        }

        public string XmlSpace
        {
            get
            {
                string text1 = this.GetAttribute("xml:space");
                if (text1 != "")
                {
                    return text1;
                }
                if (this.ParentNode is SvgElement)
                {
                    SvgElement element1 = (SvgElement) this.ParentNode;
                    return element1.XmlSpace;
                }
                return "default";
            }
        }
		public override string ToString()
		{
			return string.Empty;
		}



        // Fields
        private SvgElementCollection animatelist;
        public Hashtable AnimateNameValues;
        public Hashtable AttributePos;
        private string beforeChangeValueStr;
        public bool CreateParse;
        public bool FormatOutXml;
        private ArrayList infoList;
        private bool inKey;
        private SvgDocument ownerDocument;
        public int pretime;
        private bool showchild;
        private bool showParticular;
        protected Hashtable svgAnimAttributes;
        protected Hashtable svgAttributes;
        protected Hashtable svgStyleAttributes;
        private bool timeLineVisible;
        public SvgElement UseElement;
		public bool FormatElement;
		public bool AllowRename;
    }
}

