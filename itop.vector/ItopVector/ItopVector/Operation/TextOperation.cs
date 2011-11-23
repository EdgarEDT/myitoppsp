namespace ItopVector.DrawArea
{
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using ItopVector.Core.Figure;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Core.Paint;
    using ItopVector.Core.UnDo;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    internal class TextOperation : IOperation
    {
        // Methods
        public TextOperation(MouseArea mc)
        {
            this.currrentText = null;
            this.caretvisible = true;
            this.selectionstart = 0;
            this.selectionlength = 0;
            this.hmargin = 5;
            this.vmargin = 5;
            this.doc = null;
            this.activeText = null;
            this.sf = StringFormat.GenericTypographic;
            this.paint = Color.Black;
            this.stroke = Color.Black;
            this.font = new Font("Arial", 12f);
            this.caretChar = null;
            this.caretRect = RectangleF.Empty;
            this.startindex = 0;
            this.scale = 1f;
            this.scaleMatrix = new Matrix();
            this.mouseAreaControl = null;
            this.editmode = false;
            this.preText = null;
            this.recordanim = true;
            this.undostack = new UndoStack();
            this.enterUpdate = false;
            this.chars = new ArrayList(0x10);
            this.caretindex = 0;
            this.mouseAreaControl = mc;
            mc.Focus();
            this.caretthread = new Thread(new ThreadStart(this.CaretMethod));
            this.caretthread.IsBackground = true;
            this.caretthread.Start();
            this.mouseAreaControl.Cursor = Cursors.IBeam;
            this.mouseAreaControl.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
            this.sf.LineAlignment = StringAlignment.Near;
        }

        private void CaretMethod()
        {
            goto Label_0002;
        Label_0002:
            this.caretvisible = !this.caretvisible;
            PointF tf1 = this.PointToView(this.caretRect.Location);
            this.mouseAreaControl.Invalidate();
            Thread.Sleep(400);
            goto Label_0002;
        }

        public void Dispose()
        {
            if (this.currrentText != null)
            {
                this.PostElement();
            }
            this.mouseAreaControl.KeyPress -= new KeyPressEventHandler(this.OnKeyPress);
            this.caretthread.Abort();
            this.mouseAreaControl.TextOperation = null;
        }

        private int GetIndexForPoint(PointF point)
        {
            int num1 = 0;
            if (this.chars.Count > 0)
            {
                ItopVector.DrawArea.CharInfo info1 = (ItopVector.DrawArea.CharInfo) this.chars[0];
                if (point.X <= info1.Position.X)
                {
                    return 0;
                }
                info1 = (ItopVector.DrawArea.CharInfo) this.chars[this.chars.Count - 1];
                if (point.X >= (info1.Position.X + info1.CharSize.Width))
                {
                    return this.chars.Count;
                }
                for (int num2 = 0; num2 < this.chars.Count; num2++)
                {
                    info1 = (ItopVector.DrawArea.CharInfo) this.chars[num2];
                    if ((point.X > info1.Position.X) && (point.X < (info1.Position.X + info1.CharSize.Width)))
                    {
                        if (point.X > (info1.Position.X + (info1.CharSize.Width / 2f)))
                        {
                            num1 = num2 + 1;
                        }
                        else
                        {
                            num1 = num2;
                        }
                        break;
                    }
                    num1++;
                }
            }
            return num1;
        }

        private PointF GetPointForOffset(int index)
        {
            PointF tf1 = PointF.Empty;
            if (this.currrentText.Chars.Count > 0)
            {
                index = Math.Max(0, Math.Min((int) (this.chars.Count - 1), index));
                ItopVector.DrawArea.CharInfo info1 = (ItopVector.DrawArea.CharInfo) this.chars[index];
                tf1 = info1.Position;
            }
            return tf1;
        }

        private void InsertChar(char ch)
        {
            if (((this.doc != null) && (this.activeText != null)) && ((ch >= ' ') || (ch == '\t')))
            {
                ItopVector.DrawArea.CharInfo[] infoArray1 = this.RemoveSelection();
                TextUndoOperation operation1 = null;
                if (infoArray1 != null)
                {
                    operation1 = new TextUndoOperation(TextAction.Delete, this.selectionstart, infoArray1, this);
                }
                bool flag1 = this.doc.AcceptChanges;
                this.doc.AcceptChanges = false;
                string text1 = ch.ToString();
                XmlText text2 = this.doc.CreateTextNode(text1);
                this.doc.AcceptChanges = false;
                ItopVector.DrawArea.CharInfo info1 = new ItopVector.DrawArea.CharInfo();
                info1.Font = this.font;
                info1.TextSvgElement = this.activeText;
                info1.TextNode = text2;
                info1.StringFormat = this.sf;
                info1.StringFormat.LineAlignment = StringAlignment.Near;
                info1.Paint = this.paint;
                info1.Stroke = this.stroke;
                if (this.caretindex >= this.chars.Count)
                {
                    this.activeText.AppendChild(text2);
                    this.chars.Add(info1);
                }
                else if ((this.caretindex >= 0) && (this.caretindex < this.chars.Count))
                {
                    this.chars.Insert(this.caretindex, info1);
                    XmlNode node1 = this.activeText.ChildNodes[this.caretindex];
                    this.activeText.InsertBefore(text2, node1);
                }
                ItopVector.DrawArea.CharInfo[] infoArray2 = new ItopVector.DrawArea.CharInfo[1] { info1 } ;
                TextUndoOperation operation2 = new TextUndoOperation(TextAction.Insert, this.chars.IndexOf(info1), infoArray2, this);
                info1.PreSibling = info1.TextNode.PreviousSibling;
                bool flag2 = this.undostack.AcceptChanges;
                this.undostack.AcceptChanges = true;
                if (operation1 != null)
                {
                    TextUndoOperation[] operationArray1 = new TextUndoOperation[2] { operation1, operation2 } ;
                    this.undostack.Push(operationArray1);
                }
                else
                {
                    TextUndoOperation[] operationArray2 = new TextUndoOperation[1] { operation2 } ;
                    this.undostack.Push(operationArray2);
                }
                this.undostack.AcceptChanges = flag2;
                this.caretindex++;
                this.mouseAreaControl.Invalidate();
                this.doc.AcceptChanges = flag1;
            }
        }

        private void InvadateIndex(int index)
        {
        }

        private bool OnDialogKey(Keys keydata)
        {
            int num1 = this.caretindex;
            bool flag1 = false;
            Keys keys1 = keydata;
            if (keys1 <= Keys.Right)
            {
                switch (keys1)
                {
                    case Keys.Back:
                    {
                        if (this.selectionlength > 0)
                        {
                            int num3 = this.selectionstart;
                            ItopVector.DrawArea.CharInfo[] infoArray2 = this.RemoveSelection();
                            if (infoArray2 != null)
                            {
                                TextUndoOperation operation2 = new TextUndoOperation(TextAction.Delete, num3, infoArray2, this);
                                bool flag3 = this.undostack.AcceptChanges;
                                this.undostack.AcceptChanges = true;
                                TextUndoOperation[] operationArray2 = new TextUndoOperation[1] { operation2 } ;
                                this.undostack.Push(operationArray2);
                                this.undostack.AcceptChanges = flag3;
                            }
                            this.mouseAreaControl.Invalidate();
                            return flag1;
                        }
                        if (((this.caretindex - 1) >= 0) && ((this.caretindex - 1) < this.chars.Count))
                        {
                            this.caretindex--;
                            this.Remove(this.caretindex);
                            this.mouseAreaControl.Invalidate();
                        }
                        return true;
                    }
                    case Keys.Tab:
                    {
                        this.InsertChar(' ');
                        return true;
                    }
                    case Keys.Return:
                    {
                        this.enterUpdate = true;
                        this.PostElement();
                        this.enterUpdate = false;
                        return true;
                    }
                    case Keys.End:
                    {
                        this.selectionlength = 0;
                        this.CaretIndex = this.chars.Count;
                        this.mouseAreaControl.Invalidate();
                        return true;
                    }
                    case Keys.Home:
                    {
                        this.CaretIndex = 0;
                        this.selectionlength = 0;
                        this.mouseAreaControl.Invalidate();
                        return true;
                    }
                    case Keys.Left:
                    {
                        num1--;
                        this.CaretIndex = Math.Max(0, Math.Min(num1, this.chars.Count));
                        this.selectionlength = 0;
                        this.mouseAreaControl.Invalidate();
                        return true;
                    }
                    case Keys.Up:
                    {
                        goto Label_04AB;
                    }
                    case Keys.Right:
                    {
                        num1++;
                        this.CaretIndex = Math.Max(0, Math.Min(num1, this.chars.Count));
                        this.selectionlength = 0;
                        this.mouseAreaControl.Invalidate();
                        return true;
                    }
                }
            }
            else if (keys1 != Keys.Delete)
            {
                switch (keys1)
                {
                    case (Keys.Shift | Keys.End):
                    {
                        if (this.selectionlength == 0)
                        {
                            this.selectionstart = this.caretindex;
                        }
                        this.caretindex = this.chars.Count;
                        this.selectionlength = this.caretindex - this.selectionstart;
                        this.mouseAreaControl.Invalidate();
                        return true;
                    }
                    case (Keys.Shift | Keys.Home):
                    {
                        if (this.selectionlength == 0)
                        {
                            this.selectionstart = this.caretindex;
                        }
                        this.caretindex = 0;
                        this.selectionlength = this.selectionstart;
                        this.selectionstart = 0;
                        this.mouseAreaControl.Invalidate();
                        return true;
                    }
                    case (Keys.Shift | Keys.Left):
                    {
                        if (this.selectionlength == 0)
                        {
                            this.selectionstart = this.caretindex;
                        }
                        num1--;
                        num1 = Math.Max(0, Math.Min(num1, this.chars.Count));
                        if (num1 > this.selectionstart)
                        {
                            this.selectionlength--;
                        }
                        else
                        {
                            this.selectionlength = (this.selectionstart + this.selectionlength) - num1;
                            this.selectionstart = num1;
                        }
                        this.caretindex = num1;
                        this.mouseAreaControl.Invalidate();
                        return true;
                    }
                    case (Keys.Shift | Keys.Up):
                    {
                        goto Label_04AB;
                    }
                    case (Keys.Shift | Keys.Right):
                    {
                        if (this.selectionlength == 0)
                        {
                            this.selectionstart = this.caretindex;
                        }
                        num1++;
                        this.caretindex = Math.Max(0, Math.Min(num1, this.chars.Count));
                        if (this.caretindex > this.selectionstart)
                        {
                            this.selectionlength = this.caretindex - this.selectionstart;
                        }
                        else
                        {
                            this.selectionlength = (this.selectionstart + this.selectionlength) - this.caretindex;
                            this.selectionstart = this.caretindex;
                        }
                        this.mouseAreaControl.Invalidate();
                        return true;
                    }
                    case (Keys.Control | Keys.A):
                    {
                        int num4;
                        this.caretindex = num4 = 0;
                        this.selectionstart = num4;
                        this.selectionlength = this.chars.Count;
                        this.mouseAreaControl.Invalidate();
                        return true;
                    }
                }
            }
            else
            {
                if (this.selectionlength > 0)
                {
                    int num2 = this.selectionstart;
                    ItopVector.DrawArea.CharInfo[] infoArray1 = this.RemoveSelection();
                    if (infoArray1 != null)
                    {
                        TextUndoOperation operation1 = new TextUndoOperation(TextAction.Delete, num2, infoArray1, this);
                        bool flag2 = this.undostack.AcceptChanges;
                        this.undostack.AcceptChanges = true;
                        TextUndoOperation[] operationArray1 = new TextUndoOperation[1] { operation1 } ;
                        this.undostack.Push(operationArray1);
                        this.undostack.AcceptChanges = flag2;
                    }
                    this.mouseAreaControl.Invalidate();
                    return flag1;
                }
                if (this.caretindex < this.chars.Count)
                {
                    this.Remove(this.caretindex);
                    this.mouseAreaControl.Invalidate();
                }
                return true;
            }
        Label_04AB:
            if ((keydata >= Keys.Space) || (keydata == Keys.Tab))
            {
                flag1 = false;
            }
            else
            {
                flag1 = true;
            }
            return flag1;
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            this.InsertChar(e.KeyChar);
        }
		public void OnMouseWheel(MouseEventArgs e)
		{
			// TODO:  Ìí¼Ó BezierOperation.DealMouseWheel ÊµÏÖ
		}
        public void OnMouseDown(MouseEventArgs e)
        {
            if (((e.Button != MouseButtons.None) || !this.editmode) && (e.Button == MouseButtons.Left))
            {
                if (this.editmode)
                {
                    int num4;
                    int num1 = this.GetIndexForPoint(this.PointToView(new PointF((float) e.X, (float) e.Y)));
                    this.CaretIndex = num4 = num1;
                    this.startindex = num4;
                    this.caretvisible = true;
                    this.selectionlength = 0;
                    this.mouseAreaControl.Invalidate();
                }
                else
                {
                    Text text1 = this.preText;
                    if (text1 == null)
                    {
                        SvgElement element1 = (SvgElement) this.mouseAreaControl.PicturePanel.PreGraph;
                        if (element1 is Text)
                        {
                            bool flag1 = element1.OwnerDocument.AcceptChanges;
                            element1.OwnerDocument.AcceptChanges = false;
                            text1 = (Text) element1.Clone();
                            ISvgBrush brush1 = ((IGraphPath) element1).GraphBrush;
                            if (brush1 is SvgElement)
                            {
                                ISvgBrush brush2 = (ISvgBrush) ((SvgElement) brush1).Clone();
                                text1.GraphBrush = brush2;
                                ((SvgElement) brush2).pretime = -1;
                            }
                            else
                            {
                                text1.GraphBrush = brush1;
                            }
                            brush1 = ((IGraphPath) element1).GraphStroke.Brush;
                            if (brush1 is SvgElement)
                            {
                                ISvgBrush brush3 = (ISvgBrush) ((SvgElement) brush1).Clone();
                                text1.GraphStroke = new Stroke(brush3);
                                ((SvgElement) brush3).pretime = -1;
                            }
                            else
                            {
                                text1.GraphStroke.Brush = brush1;
                            }
                            SizeF ef1 = this.mouseAreaControl.PicturePanel.GridSize;
                            float single1 = ef1.Height;
                            float single2 = ef1.Width;
                            FontFamily family1 = TextFunc.GetGDIFontFamily(text1);
                            float single3 = (((float) family1.GetCellAscent(FontStyle.Regular)) / ((float) family1.GetEmHeight(FontStyle.Regular))) * text1.Size;
                            PointF tf1 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
                            if (this.mouseAreaControl.PicturePanel.SnapToGrid)
                            {
                                int num2 = (int) ((tf1.X + (single2 / 2f)) / single2);
                                int num3 = (int) ((tf1.Y + (single1 / 2f)) / single1);
                                tf1 = (PointF) new Point((int) (num2 * single2), (int) (num3 * single1));
                            }
                            text1.X = tf1.X;
                            text1.Y = tf1.Y;
                            text1.GraphTransform.Matrix = this.mouseAreaControl.PicturePanel.CoordTransform.Clone();
                            element1.OwnerDocument.AcceptChanges = flag1;
                        }
                    }
                    if (text1 != null)
                    {
                        this.recordanim = this.mouseAreaControl.SVGDocument.RecordAnim;
                        this.CurrentText = text1;
                        this.editmode = true;
                        if ((this.caretthread != null) && (this.caretthread.ThreadState == ThreadState.Suspended))
                        {
                            this.caretthread.Resume();
                        }
                        this.mouseAreaControl.Focus();
                    }
                }
            }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.None) && !this.editmode)
            {
				
                this.preText = null;
                /*SvgElementCollection.ISvgElementEnumerator enumerator1 = this.mouseAreaControl.PicturePanel.ElementList.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    IGraph graph1 = (IGraph) enumerator1.Current;
                    if (graph1 is Text)
                    {
                        Text text1 = (Text) graph1;
                        PointF tf1 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
                        GraphicsPath path1 = (GraphicsPath) text1.GPath.Clone();
                        path1.Transform(text1.GraphTransform.Matrix);
                        RectangleF ef1 = path1.GetBounds();
                        if (ef1.Contains(new PointF((float) e.X, (float) e.Y)))
                        {
                            this.preText = text1;
                            return;
                        }
                    }
                }
				*/
            }
            if (e.Button == MouseButtons.Left)
            {
                int num1 = this.GetIndexForPoint(this.PointToView(new PointF((float) e.X, (float) e.Y)));
                if (num1 > this.startindex)
                {
                    this.selectionstart = this.startindex;
                    this.selectionlength = num1 - this.startindex;
                }
                else
                {
                    this.selectionstart = num1;
                    this.selectionlength = this.startindex - num1;
                }
                this.CaretIndex = num1;
                this.mouseAreaControl.Invalidate();
            }
        }

        public void OnMouseUp(MouseEventArgs e)
        {
        }

        public void OnPaint(PaintEventArgs e)
        {
            if (this.mouseAreaControl.SVGDocument.RecordAnim && this.editmode)
            {
                this.mouseAreaControl.SVGDocument.RecordAnim = false;
            }
            if ((this.currrentText != null) && this.editmode)
            {
                if (this.activeText == null)
                {
                    this.activeText = this.currrentText;
                }
                this.scaleMatrix = this.mouseAreaControl.PicturePanel.CoordTransform.Clone();
                this.scaleMatrix.Multiply(this.currrentText.Transform.Matrix);
                FontFamily family1 = this.font.FontFamily;
                float single1 = (((float) family1.GetCellAscent(FontStyle.Regular)) / ((float) family1.GetEmHeight(FontStyle.Regular))) * this.currrentText.Size;
                this.scaleMatrix.Translate(this.currrentText.X, this.currrentText.Y - single1);
                
				e.Graphics.Transform = this.scaleMatrix;
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                this.PaintSelect(e.Graphics);
                GraphicsPath path1 = this.PaintText(e.Graphics);
                this.PaintCaret(e.Graphics);
            }
        }

        private void PaintCaret(Graphics g)
        {
            if (((this.doc != null) && this.caretvisible) && (this.currrentText != null))
            {
                this.caretChar = null;
                FontFamily family1 = this.font.FontFamily;
                float single1 = (((float) family1.GetLineSpacing(FontStyle.Regular)) / ((float) family1.GetEmHeight(FontStyle.Regular))) * this.font.Size;
                PointF tf1 = PointF.Empty;
                if (this.chars.Count > 0)
                {
                    if (this.caretindex >= this.chars.Count)
                    {
                        ItopVector.DrawArea.CharInfo info1 = (ItopVector.DrawArea.CharInfo) this.chars[this.chars.Count - 1];
                        tf1 = new PointF(info1.Position.X + info1.CharSize.Width, info1.Position.Y);
                        single1 = info1.CharSize.Height;
                    }
                    else
                    {
                        int num1 = Math.Max(0, Math.Min(this.caretindex, (int) (this.chars.Count - 1)));
                        ItopVector.DrawArea.CharInfo info2 = (ItopVector.DrawArea.CharInfo) this.chars[num1];
                        tf1 = info2.Position;
                        single1 = info2.CharSize.Height;
                        this.caretChar = info2;
                    }
                }
                this.caretRect = new RectangleF(tf1.X, tf1.Y, 1f, single1);
				GraphicsPath path1=new GraphicsPath();
//				Matrix matrix1=g.Transform.Clone();
//				Matrix matrix2=new Matrix();
//				matrix2.Translate(g.Transform.OffsetX,g.Transform.OffsetY);

//				matrix1.Invert();
//				matrix1.Translate(-matrix1.OffsetX,-matrix1.OffsetY);
				
				path1.AddRectangle(caretRect);
//				path1.Transform(matrix2);
				GraphicsContainer container1=g.BeginContainer();
//				g.Transform=matrix1;

                g.FillPath(new SolidBrush(ControlPaint.Light(Color.Black)), path1);

				g.EndContainer(container1);

            }
        }

        private void PaintSelect(Graphics g)
        {
            if (this.doc != null)
            {
                for (int num1 = this.selectionstart; num1 < (this.selectionstart + this.selectionlength); num1++)
                {
                    if (num1 >= this.chars.Count)
                    {
                        return;
                    }
                    ItopVector.DrawArea.CharInfo info1 = (ItopVector.DrawArea.CharInfo) this.chars[num1];
                    PointF tf1 = info1.Position;
                    SizeF ef1 = info1.CharSize;
                    g.FillRectangle(Brushes.Navy, tf1.X, tf1.Y, ef1.Width, ef1.Height);
                }
            }
        }

        private GraphicsPath PaintText(Graphics g)
        {
            float single1 = 0f;
            FontFamily family1 = TextFunc.GetGDIFontFamily(this.currrentText);
            float single2 = (((float) family1.GetCellAscent(FontStyle.Regular)) / ((float) family1.GetEmHeight(FontStyle.Regular))) * this.currrentText.Size;
            float single3 = single2;
            GraphicsPath path1 = new GraphicsPath();
            int num1 = 0;
			Matrix matrix1 =new Matrix();
			Matrix matrix2 =new Matrix();
			matrix2.Multiply(g.Transform);
			matrix1.Multiply(g.Transform);
			matrix1.Invert();

			GraphicsContainer container1 =g.BeginContainer();
			g.SmoothingMode=SmoothingMode.HighQuality;
			g.Transform=matrix1;
            foreach (ItopVector.DrawArea.CharInfo info1 in this.chars)
            {
                if (info1.TextNode == null)
                {
                    continue;
                }
                Font font1 = info1.Font;
                Color color1 = info1.Paint;
                Color color2 = info1.Paint;
                FontFamily family2 = font1.FontFamily;
                if (info1.valueX)
                {
                    single1 = info1.X;
                }
                if (info1.valueY)
                {
                    single3 = info1.Y;
                }
                single1 += info1.Dx;
                single3 += info1.Dy;
                float single4 = font1.Size;
                string text1 = info1.TextNode.Value.Trim();
                info1.StringFormat.LineAlignment = StringAlignment.Near;
                float single5 = (((float) family2.GetCellAscent(FontStyle.Regular)) / ((float) family2.GetEmHeight(FontStyle.Regular))) * single4;
                SizeF ef2 = g.MeasureString(text1, font1, new PointF(single1, single3 - single5), info1.StringFormat);
                float single6 = ef2.Width;
				using (GraphicsPath path2 = new GraphicsPath())
				{
					path2.AddString(text1, family2, (int) font1.Style, font1.Size, new PointF(single1, single3 - single5), info1.StringFormat);
					path1.AddString(text1, family2, (int) font1.Style, font1.Size, new PointF(single1, single3 - single5), info1.StringFormat);
					info1.Position = new PointF(single1 - info1.Dx, single3 - single5);
					path2.Transform( matrix2);
					RectangleF ef1 = path2.GetBounds();				

					float single7 = (((float) family2.GetLineSpacing(FontStyle.Regular)) / ((float) family2.GetEmHeight(FontStyle.Regular))) * single4;
					info1.CharSize = new SizeF(((single6 * 3f) / 4f) + info1.Dx, single7);
					single1 += ((single6 * 3f) / 4f);
					float single8 = (((float) family2.GetCellDescent(FontStyle.Regular)) / ((float) family2.GetEmHeight(FontStyle.Regular))) * single4;
					color1 = info1.Paint;
					color2 = info1.Stroke;
					if ((num1 >= this.selectionstart) && (num1 < (this.selectionstart + this.selectionlength)))
					{
						color1 = color2 = Color.White;
					}
					g.FillPath(new SolidBrush(color1), path2);
					g.DrawPath(new Pen(color2), path2);
				}
                num1++;
            }
			g.EndContainer(container1);

            return path1;
        }

        private void ParseText(Text text)
        {
            if (text != null)
            {
                if (this.chars == null)
                {
                    this.chars = new ArrayList(0x10);
                }
                Color color1 = Color.Black;
                Color color2 = Color.Black;
                Font font1 = new Font("Arial", 20f);
                StringFormat format1 = TextFunc.GetGDIStringFormat(text);
                format1.LineAlignment = StringAlignment.Near;
                FontFamily family1 = TextFunc.GetGDIFontFamily(text);
                int num1 = TextFunc.GetGDIStyle(text);
                font1 = new Font(family1.Name, text.Size, (FontStyle) num1);
                float single1 = text.Dx;
                float single2 = text.Dy;
                ISvgBrush brush1 = text.GraphBrush;
                if (brush1 is SolidColor)
                {
                    color1 = ((SolidColor) brush1).Color;
                }
                bool flag1 = true;
                this.doc.AcceptChanges = false;
                for (int num2 = 0; num2 < text.ChildNodes.Count; num2++)
                {
                    XmlNode node1 = text.ChildNodes[num2];
                    if (node1 is Text)
                    {
                        this.ParseText((Text) node1);
                    }
                    else if (node1 is XmlText)
                    {
                        string text1 = TextFunc.TrimText(node1.Value, text);
                        CharEnumerator enumerator1 = text1.GetEnumerator();
                        while (enumerator1.MoveNext())
                        {
                            char ch1 = enumerator1.Current;
                            XmlText text2 = this.doc.CreateTextNode(ch1.ToString());
                            ItopVector.DrawArea.CharInfo info1 = new ItopVector.DrawArea.CharInfo();
                            info1.TextNode = text2;
                            info1.TextSvgElement = text;
                            info1.Paint = color1;
                            info1.StringFormat = format1;
                            info1.StringFormat.LineAlignment = StringAlignment.Near;
                            info1.Font = font1;
                            if (flag1 && (text.ChildNodes[0] == node1))
                            {
                                info1.Dx = single1;
                                info1.Dy = single2;
                                info1.firstChild = true;
                            }
                            this.chars.Add(info1);
                            text.InsertBefore(text2, node1);
                            num2++;
                        }
                        text.RemoveChild(node1);
                        num2--;
                    }
                }
            }
        }

        private PointF PointToView(PointF point)
        {
            if (this.scaleMatrix != null)
            {
                Matrix matrix1 = this.scaleMatrix.Clone();
                matrix1.Invert();
                PointF[] tfArray2 = new PointF[1] { point } ;
                PointF[] tfArray1 = tfArray2;
                matrix1.TransformPoints(tfArray1);
                return tfArray1[0];
            }
            return point;
        }

        private void PostEdit(object sender, string attributename, string attributevalue)
        {
            if (this.editmode)
            {
                string text2;
                Text text1 = sender as Text;
                if ((text2 = attributename) != null)
                {
                    text2 = string.IsInterned(text2);
                    if (text2 != "fill")
                    {
                        if (text2 == "font-size")
                        {
                            float single1 = ItopVector.Core.Func.Number.ParseFloatStr(attributevalue);
                            for (int num2 = this.selectionstart; num2 < (this.selectionstart + this.selectionlength); num2++)
                            {
                                ItopVector.DrawArea.CharInfo info2 = (ItopVector.DrawArea.CharInfo) this.chars[num2];
                                Font font1 = info2.Font;
                                font1 = new Font(font1.FontFamily.Name, single1, font1.Style);
                                info2.Font = font1;
                            }
                            this.font = new Font(this.font.FontFamily.Name, single1, this.font.Style);
                            this.UpdateSelect(attributename, attributevalue);
                        }
                        else if (text2 == "font-family")
                        {
                            for (int num3 = this.selectionstart; num3 < (this.selectionstart + this.selectionlength); num3++)
                            {
                                ItopVector.DrawArea.CharInfo info3 = (ItopVector.DrawArea.CharInfo) this.chars[num3];
                                Font font2 = info3.Font;
                                font2 = new Font(attributevalue.Trim(), font2.Size, font2.Style);
                                info3.Font = font2;
                            }
                            this.font = new Font(attributevalue.Trim(), this.font.Size, this.font.Style);
                            this.UpdateSelect(attributename, attributevalue);
                        }
                        else if (text2 == "dx")
                        {
                            float single2 = ItopVector.Core.Func.Number.ParseFloatStr(attributevalue);
                            if (((this.selectionlength > 0) && (this.selectionstart >= 0)) && (this.selectionstart < this.chars.Count))
                            {
                                ((ItopVector.DrawArea.CharInfo) this.chars[this.selectionstart]).Dx = single2;
                            }
                            this.UpdateSelect(attributename, attributevalue);
                        }
                        else if (text2 == "dy")
                        {
                            float single3 = ItopVector.Core.Func.Number.ParseFloatStr(attributevalue);
                            if (((this.selectionlength > 0) && (this.selectionstart >= 0)) && (this.selectionstart < this.chars.Count))
                            {
                                ((ItopVector.DrawArea.CharInfo) this.chars[this.selectionstart]).Dy = single3;
                            }
                            this.UpdateSelect(attributename, attributevalue);
                        }
                        else if (text2 == "font-weight")
                        {
                            for (int num4 = this.selectionstart; num4 < (this.selectionstart + this.selectionlength); num4++)
                            {
                                ItopVector.DrawArea.CharInfo info4 = (ItopVector.DrawArea.CharInfo) this.chars[num4];
                                Font font3 = info4.Font;
                                FontStyle style1 = font3.Style;
                                if (attributevalue.Trim() == "bold")
                                {
                                    style1 |= FontStyle.Bold;
                                }
                                else
                                {
                                    style1 = style1;
                                }
                                font3 = new Font(font3.FontFamily.Name, font3.Size, style1);
                                info4.Font = font3;
                            }
                            if (attributevalue.Trim() == "bold")
                            {
                                this.font = new Font(this.font.FontFamily.Name, this.font.Size, this.font.Style | FontStyle.Bold);
                            }
                            else
                            {
                                this.font = new Font(this.font.FontFamily.Name, this.font.Size, this.font.Style);
                            }
                            this.UpdateSelect(attributename, attributevalue);
                        }
                        else if (text2 == "font-style")
                        {
                            for (int num5 = this.selectionstart; num5 < (this.selectionstart + this.selectionlength); num5++)
                            {
                                ItopVector.DrawArea.CharInfo info5 = (ItopVector.DrawArea.CharInfo) this.chars[num5];
                                Font font4 = info5.Font;
                                FontStyle style2 = font4.Style;
                                if (attributevalue.Trim() == "normal")
                                {
                                    style2 = style2;
                                }
                                else
                                {
                                    style2 |= FontStyle.Italic;
                                }
                                font4 = new Font(font4.FontFamily.Name, font4.Size, style2);
                                info5.Font = font4;
                            }
                            if (attributevalue.Trim() == "normal")
                            {
                                this.font = new Font(this.font.FontFamily.Name, this.font.Size, this.font.Style);
                            }
                            else
                            {
                                this.font = new Font(this.font.FontFamily.Name, this.font.Size, this.font.Style | FontStyle.Italic);
                            }
                            this.UpdateSelect(attributename, attributevalue);
                        }
                        else if (text2 == "text-decoration")
                        {
                            for (int num6 = this.selectionstart; num6 < (this.selectionstart + this.selectionlength); num6++)
                            {
                                ItopVector.DrawArea.CharInfo info6 = (ItopVector.DrawArea.CharInfo) this.chars[num6];
                                Font font5 = info6.Font;
                                FontStyle style3 = font5.Style;
                                if (attributevalue == "line-through")
                                {
                                    style3 |= FontStyle.Strikeout;
                                }
                                else if (attributevalue == "underline")
                                {
                                    style3 |= FontStyle.Underline;
                                }
                                font5 = new Font(font5.FontFamily.Name, font5.Size, style3);
                                info6.Font = font5;
                            }
                            if (attributevalue == "line-through")
                            {
                                this.font = new Font(this.font.FontFamily.Name, this.font.Size, this.font.Style | FontStyle.Strikeout);
                            }
                            else if (attributevalue == "underline")
                            {
                                this.font = new Font(this.font.FontFamily.Name, this.font.Size, this.font.Style | FontStyle.Underline);
                            }
                            this.UpdateSelect(attributename, attributevalue);
                        }
                    }
                    else
                    {
                        Color color1 = ColorFunc.ParseColor(attributevalue);
                        for (int num1 = this.selectionstart; num1 < (this.selectionstart + this.selectionlength); num1++)
                        {
                            ItopVector.DrawArea.CharInfo info1 = (ItopVector.DrawArea.CharInfo) this.chars[num1];
                            info1.Paint = color1;
                        }
                        this.paint = color1;
                        this.UpdateSelect(attributename, attributevalue);
                    }
                }
                this.mouseAreaControl.Invalidate();
                this.mouseAreaControl.Focus();
            }
        }

        public void PostElement()
        {
            int num1;
            int num2;
            bool flag1 = this.doc.AcceptChanges;
            if (this.chars.Count == 0)
            {
                this.doc.AcceptChanges = true;
                if (this.preText != null)
                {
                    this.preText.ParentNode.RemoveChild(this.preText);
                }
                this.currrentText = null;
            }
            if (this.currrentText != null)
            {
                this.doc.AcceptChanges = false;
                this.currrentText.Normalize();
                this.currrentText.EditMode = false;
                this.doc.AcceptChanges = true;
                this.chars.Clear();
                this.mouseAreaControl.SVGDocument.ClearSelects();
				this.currrentText.pretime=-1;
                if (this.currrentText.ParentNode == null)
                {
                    this.mouseAreaControl.PicturePanel.AddElement(this.currrentText);
                }
            }
            this.preText = null;
            this.caretindex = num1 = 0;
            this.selectionlength = num2 = num1;
            this.selectionstart = num2;
            this.currrentText = null;
            this.doc.AcceptChanges = flag1;
            this.editmode = false;
            this.mouseAreaControl.Invalidate();
            this.mouseAreaControl.SVGDocument.RecordAnim = true;
            this.undostack.ClearAll();
            if (this.enterUpdate)
            {
                this.mouseAreaControl.PicturePanel.Operation = ToolOperation.Select;
            }
        }

        public bool ProcessDialogKey(Keys keydata)
        {
            return this.OnDialogKey(keydata);
        }

        public bool Redo()
        {
            if (this.doc == null)
            {
                return false;
            }
            if (!this.editmode)
            {
                return false;
            }
            bool flag1 = this.doc.AcceptChanges;
            this.doc.AcceptChanges = false;
            if (this.undostack.CanRedo)
            {
                this.undostack.Redo();
                this.mouseAreaControl.Invalidate();
                this.doc.AcceptChanges = flag1;
                return true;
            }
            this.doc.AcceptChanges = flag1;
            return true;
        }

        private void Remove(int index)
        {
            bool flag1 = this.doc.AcceptChanges;
            this.doc.AcceptChanges = false;
            if ((index >= 0) && (index < this.chars.Count))
            {
                ItopVector.DrawArea.CharInfo info1 = (ItopVector.DrawArea.CharInfo) this.chars[index];
                XmlNode node1 = info1.TextNode.PreviousSibling;
                info1.TextNode.ParentNode.RemoveChild(info1.TextNode);
                if ((info1.firstChild && (info1.TextSvgElement != this.currrentText)) && (((info1.Dx != 0f) || (info1.Dy != 0f)) || ((info1.X != 0f) || (info1.Y != 0f))))
                {
                    for (Text text1 = info1.TextSvgElement; text1 != this.currrentText; text1 = (Text) text1.ParentNode)
                    {
                        float single1;
                        float single2;
                        float single3;
                        text1.Y = single1 = 0f;
                        text1.X = single2 = single1;
                        text1.Dy = single3 = single2;
                        text1.Dx = single3;
                        if ((text1.Dx == info1.Dx) && (info1.Dy == info1.Dy))
                        {
                            break;
                        }
                    }
                    if ((index + 1) < this.chars.Count)
                    {
                        ((ItopVector.DrawArea.CharInfo) this.chars[index + 1]).firstChild = true;
                    }
                }
                this.chars.Remove(info1);
                ItopVector.DrawArea.CharInfo[] infoArray1 = new ItopVector.DrawArea.CharInfo[1] { info1 } ;
                TextUndoOperation operation1 = new TextUndoOperation(TextAction.Delete, index, infoArray1, this);
                info1.PreSibling = node1;
                bool flag2 = this.undostack.AcceptChanges;
                this.undostack.AcceptChanges = true;
                TextUndoOperation[] operationArray1 = new TextUndoOperation[1] { operation1 } ;
                this.undostack.Push(operationArray1);
                this.undostack.AcceptChanges = flag2;
            }
            this.doc.AcceptChanges = flag1;
        }

        private ItopVector.DrawArea.CharInfo[] RemoveSelection()
        {
            ArrayList list1 = new ArrayList(0x10);
            if (this.doc == null)
            {
                return null;
            }
            bool flag1 = this.doc.AcceptChanges;
            this.doc.AcceptChanges = false;
            for (int num1 = this.selectionstart; num1 < (this.selectionstart + this.selectionlength); num1++)
            {
                if (this.selectionstart >= this.chars.Count)
                {
                    break;
                }
                ItopVector.DrawArea.CharInfo info1 = (ItopVector.DrawArea.CharInfo) this.chars[this.selectionstart];
                list1.Add(info1);
                info1.TextSvgElement.RemoveChild(info1.TextNode);
                this.chars.Remove(info1);
                this.caretindex = this.selectionstart;
            }
            this.selectionlength = 0;
            this.doc.AcceptChanges = flag1;
            if (list1.Count == 0)
            {
                return null;
            }
            ItopVector.DrawArea.CharInfo[] infoArray1 = new ItopVector.DrawArea.CharInfo[list1.Count];
            list1.CopyTo(infoArray1);
            return infoArray1;
        }

        public bool Undo()
        {
            if (this.doc == null)
            {
                return false;
            }
            if (!this.editmode)
            {
                return false;
            }
            bool flag1 = this.doc.AcceptChanges;
            this.doc.AcceptChanges = false;
            if (this.undostack.CanUndo)
            {
                this.undostack.Undo();
                this.doc.AcceptChanges = flag1;
                this.mouseAreaControl.Invalidate();
                return true;
            }
            this.doc.AcceptChanges = flag1;
            return true;
        }

        private void UpdateSelect(string attributename, string valuestr)
        {
            bool flag1 = this.doc.AcceptChanges;
            int num1 = this.selectionstart;
            int num2 = this.selectionstart;
            this.doc.AcceptChanges = false;
            XmlNode node1 = null;
            XmlNode node2 = null;
            for (int num3 = this.selectionstart; num3 < (this.selectionstart + this.selectionlength); num3++)
            {
                ItopVector.DrawArea.CharInfo info1 = (ItopVector.DrawArea.CharInfo) this.chars[num3];
                XmlText text1 = info1.TextNode;
                if (num3 == num1)
                {
                    node1 = text1.PreviousSibling;
                }
                node2 = text1.NextSibling;
                if (!(node2 is XmlText) || (num3 == ((this.selectionstart + this.selectionlength) - 1)))
                {
                    if ((node1 == null) && (node2 == null))
                    {
                        AttributeFunc.SetAttributeValue(info1.TextSvgElement, attributename, valuestr);
                    }
                    else
                    {
                        TSpan span1 = (TSpan) this.doc.CreateElement(this.doc.Prefix, "tspan", this.doc.NamespaceURI);
                        AttributeFunc.SetAttributeValue(span1, attributename, valuestr);
                        if (node2 == null)
                        {
                            info1.TextSvgElement.AppendChild(span1);
                        }
                        else
                        {
                            info1.TextSvgElement.InsertBefore(span1, node2);
                        }
                        for (int num4 = num1; num4 <= num3; num4++)
                        {
                            info1.firstChild = num4 == num1;
                            ItopVector.DrawArea.CharInfo info2 = (ItopVector.DrawArea.CharInfo) this.chars[num4];
                            span1.AppendChild(info2.TextNode);
                            info2.TextSvgElement = span1;
                        }
                    }
                    num1 = num3 + 1;
                }
            }
            this.doc.AcceptChanges = flag1;
        }


        // Properties
        private Text ActiveText
        {
            set
            {
                if (this.activeText != value)
                {
                    this.activeText = value;
                    ISvgBrush brush1 = this.activeText.GraphBrush;
                    if (brush1 is SolidColor)
                    {
                        this.paint = ((SolidColor) brush1).Color;
                    }
                    else
                    {
                        this.paint = Color.Black;
                    }
                    FontFamily family1 = TextFunc.GetGDIFontFamily(this.activeText);
                    float single1 = this.activeText.Size;
                    this.sf = TextFunc.GetGDIStringFormat(this.activeText);
                    int num1 = TextFunc.GetGDIStyle(this.activeText);
                    if (single1 <= 0f)
                    {
                        single1 = this.activeText.OwnerTextElement.Size;
                    }
                    this.font = new Font(family1, single1, (FontStyle) num1);
                }
            }
        }

        public bool CanRedo
        {
            get
            {
                return this.undostack.CanRedo;
            }
        }

        public bool CanUndo
        {
            get
            {
                return this.undostack.CanUndo;
            }
        }

        private int CaretIndex
        {
            set
            {
                if (this.caretindex != value)
                {
                    this.caretindex = value;
                    if (this.chars.Count == 0)
                    {
                        this.ActiveText = this.currrentText;
                    }
                    else
                    {
                        int num1 = Math.Max(0, Math.Min((int) (value - 1), (int) (this.chars.Count - 1)));
                        ItopVector.DrawArea.CharInfo info1 = (ItopVector.DrawArea.CharInfo) this.chars[num1];
                        this.doc.SelectCollection.Clear();
                        SvgElementCollection collection1 = new SvgElementCollection();
                        for (int num2 = this.selectionstart; num2 < (this.selectionstart + this.selectionlength); num2++)
                        {
                            Text text1 = ((ItopVector.DrawArea.CharInfo) this.chars[num2]).TextSvgElement;
                            if (!collection1.Contains(text1))
                            {
                                collection1.Add(text1);
                            }
                            if (num2 == 0)
                            {
                                text1.OnPostTextEdit += new PostTextEditEventHandler(this.PostEdit);
                            }
                        }
                        if (collection1.Count > 0)
                        {
                            this.doc.SelectCollection.AddRange(collection1);
                        }
                        else
                        {
                            info1.TextSvgElement.OnPostTextEdit += new PostTextEditEventHandler(this.PostEdit);
                            this.doc.CurrentElement = info1.TextSvgElement;
                        }
                        this.ActiveText = info1.TextSvgElement;
                    }
                }
            }
        }

        public Text CurrentText
        {
            get
            {
                return this.currrentText;
            }
            set
            {
                if (this.currrentText != value)
                {
                    this.currrentText = value;
                    this.doc = value.OwnerDocument;
                    this.chars.Clear();
                    this.ParseText(value);
                    this.mouseAreaControl.Invalidate();
                    this.ActiveText = value;
                    value.EditMode = true;
                }
            }
        }

        public SizeF Margin
        {
            get
            {
                return new SizeF((float) this.hmargin, (float) this.vmargin);
            }
        }

        public float ScaleUnit
        {
            set
            {
                if (this.scale != value)
                {
                    this.scale = value;
                    this.mouseAreaControl.Invalidate();
                }
            }
        }


        // Fields
        private Text activeText;
        private ItopVector.DrawArea.CharInfo caretChar;
        public int caretindex;
        private RectangleF caretRect;
        private Thread caretthread;
        private bool caretvisible;
        public ArrayList chars;
        private Text currrentText;
        private SvgDocument doc;
        private bool editmode;
        private bool enterUpdate;
        private Font font;
        private int hmargin;
        private MouseArea mouseAreaControl;
        private Color paint;
        private Text preText;
        private bool recordanim;
        private float scale;
        private Matrix scaleMatrix;
        private int selectionlength;
        private int selectionstart;
        private StringFormat sf;
        private int startindex;
        private Color stroke;
        private UndoStack undostack;
        private int vmargin;
    }
}

