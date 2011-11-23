namespace ItopVector.DrawArea
{
    using ItopVector;
    using ItopVector.Core;
    using ItopVector.Core.Animate;
    using ItopVector.Core.Document;
    using ItopVector.Core.Figure;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Core.Paint;
    using ItopVector.Core.Types;
    using ItopVector.Resource;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;
	using System.Text;

    internal class BezierOperation : IOperation
    {
        // Methods
        public BezierOperation(MouseArea mc)
        {
            this.mouseAreaControl = null;
            this.currentGraph = null;
            this.revertMatrix = new Matrix();
            this.startPoint = PointF.Empty;
            this.currentOperate = BezierOperate.Draw;
            this.activePoints = new PointInfoCollection();
            this.editPath = new GraphicsPath();
            this.preInfo = null;
            this.reversePath = new GraphicsPath();
            this.editpath = new GraphicsPath();
            this.preDrawInfo = null;
            this.starttrend = PointF.Empty;
            this.endtrend = PointF.Empty;
            this.addInfo = null;
            this.moveinfo = null;
            this.movePoint = PointF.Empty;
            this.currentinfo = null;
            this.SubpathList = new Hashtable(0x10);
            this.oldstr = string.Empty;
            this.attributename = string.Empty;
            this.showcontrol = true;
            this.activeindex = new int[0];
            this.oldindex = -1;
            this.centerPoint = PointF.Empty;
            this.incenter = false;
            this.tooltips = new Hashtable(0x10);
            this.mouseAreaControl = mc;
            this.win32 = mc.win32;
            this.mouseAreaControl.DefaultCursor = SpecialCursors.bezierCursor;
        }

        public BezierOperation(MouseArea mc, IPath path)
        {
            this.mouseAreaControl = null;
            this.currentGraph = null;
            this.revertMatrix = new Matrix();
            this.startPoint = PointF.Empty;
            this.currentOperate = BezierOperate.Draw;
            this.activePoints = new PointInfoCollection();
            this.editPath = new GraphicsPath();
            this.preInfo = null;
            this.reversePath = new GraphicsPath();
            this.editpath = new GraphicsPath();
            this.preDrawInfo = null;
            this.starttrend = PointF.Empty;
            this.endtrend = PointF.Empty;
            this.addInfo = null;
            this.moveinfo = null;
            this.movePoint = PointF.Empty;
            this.currentinfo = null;
            this.SubpathList = new Hashtable(0x10);
            this.oldstr = string.Empty;
            this.attributename = string.Empty;
            this.showcontrol = true;
            this.activeindex = new int[0];
            this.oldindex = -1;
            this.centerPoint = PointF.Empty;
            this.incenter = false;
            this.tooltips = new Hashtable(0x10);
            this.centerPoint = PointF.Empty;
            this.mouseAreaControl = mc;
            this.win32 = mc.win32;
            this.currentGraph = path;
            this.mouseAreaControl.Invalidate();
            if (path is IGraph)
            {
                this.revertMatrix = ((IGraph) path).GraphTransform.Matrix.Clone();
            }
            else if (path is MotionAnimate)
            {
                SvgElement element1 = ((MotionAnimate) path).RefElement;
                if (element1 is IGraph)
                {
                    Matrix matrix1 = ((IGraph) element1).Transform.Matrix.Clone();
                    this.revertMatrix = ((IGraph) element1).GraphTransform.Matrix.Clone();
                    matrix1.Invert();
                    this.revertMatrix.Multiply(matrix1);
                    Matrix matrix2 = new Matrix();
                    matrix2 = AnimFunc.GetMatrixForTime((IGraph) element1, this.mouseAreaControl.SVGDocument.ControlTime, ((MotionAnimate) this.currentGraph).Begin);
                    this.revertMatrix.Multiply(matrix2);
                    this.centerPoint = this.PointToView(this.mouseAreaControl.CenterPoint);
                    object obj1 = ((MotionAnimate) path).GetAnimateResult((float) this.mouseAreaControl.SVGDocument.ControlTime, DomType.SvgMatrix);
                    if (obj1 is Matrix)
                    {
                        Matrix matrix3 = (Matrix) obj1;
                        this.centerPoint.X -= matrix3.OffsetX;
                        this.centerPoint.Y -= matrix3.OffsetY;
                    }
                    this.revertMatrix.Translate(this.centerPoint.X, this.centerPoint.Y);
                }
            }
            if (path is GraphPath)
            {
                this.attributename = "d";
            }
            else if (path is MotionAnimate)
            {
                this.attributename = "path";
            }
            this.mouseAreaControl.DefaultCursor = SpecialCursors.bezierCursor;
        }

        public void Dispose()
        {
            this.mouseAreaControl.BezierOperation = null;
        }

        private PointInfo GetLastPointInfoForCurrentInfo(PointInfo info)
        {
            if (info == null)
            {
                return null;
            }
            PointInfoCollection collection1 = this.currentGraph.PointsInfo;
            int num1 = collection1.IndexOf(info);
            if (num1 < 0)
            {
                return null;
            }
            PointInfo info1 = info;
            for (int num2 = num1; num2 < collection1.Count; num2++)
            {
                PointInfo info2 = collection1[num2];
                if (info2.SubPath != info1.SubPath)
                {
                    return info1;
                }
                info1 = info2;
            }
            return info;
        }

        public void OnMouseDown(MouseEventArgs e)
        {
            bool flag1;
            string text2;
            PointInfo info6;
            bool flag2;
            string text3;
            int num10;
            if (this.currentGraph != null)
            {
                if (!this.currentGraph.PointsInfo.Contains(this.currentInfo))
                {
                    this.currentInfo = null;
                }
                for (int num1 = 0; num1 < this.activePoints.Count; num1++)
                {
                    PointInfo info1 = this.activePoints[num1];
                    if (!this.currentGraph.PointsInfo.Contains(info1))
                    {
                        this.activePoints.Remove(info1);
                        num1--;
                    }
                }
                if (this.currentGraph == null)
                {
                    return;
                }
                if (e.Button != MouseButtons.Left)
                {
                    return;
                }
                SizeF ef1 = this.mouseAreaControl.PicturePanel.GridSize;
                this.startPoint = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
                float single1 = ef1.Height;
                float single2 = ef1.Width;
                this.startPoint = this.PointToView(new PointF((float) e.X, (float) e.Y));
                PointInfoCollection.PointInfoEnumerator enumerator1 = this.currentGraph.PointsInfo.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    PointInfo info2 = enumerator1.Current;
                    RectangleF ef2 = new RectangleF(info2.MiddlePoint.X - 3f, info2.MiddlePoint.Y - 3f, 6f, 6f);
                    if (ef2.Contains(this.startPoint))
                    {
                        this.preInfo = info2;
                        break;
                    }
                }
                switch (this.currentOperate)
                {
                    case BezierOperate.Draw:
                    {
                        flag1 = false;
                        if (this.currentGraph != null)
                        {
                            string text1 = ((SvgElement) this.currentGraph).Name;
                            if ((text1 != "path") && (text1 != "animateMotion"))
                            {
                                flag1 = true;
                            }
                            goto Label_01E2;
                        }
                        flag1 = true;
                        goto Label_01E2;
                    }
                    case BezierOperate.AddAnchor:
                    {
                        if (this.addInfo == null)
                        {
                            return;
                        }
                        if (this.addInfo.NextInfo == null)
                        {
                            goto Label_100E;
                        }
                        PointInfo info8 = this.addInfo.NextInfo;
                        text3 = string.Empty;
                        num10 = this.currentGraph.PointsInfo.IndexOf(this.addInfo);
                        if (!info8.FirstControl.IsEmpty)
                        {
                            float single3 = 0f;
                            PointF[] tfArray1 = BezierOperation.SplitBezierAtPoint(this.addInfo.MiddlePoint, info8.FirstControl, info8.SecondControl, info8.MiddlePoint, this.startPoint, out single3);
                            if (tfArray1 == null)
                            {
                                return;
                            }
                            if ((tfArray1.Length == 5) && (single3 >= 0f))
                            {
                                text3 = this.addInfo.PreString + this.addInfo.PointString;
                                string text9 = text3;
                                string[] textArray5 = new string[13] { text9, "C ", tfArray1[0].X.ToString(), " ", tfArray1[0].Y.ToString(), " ", tfArray1[1].X.ToString(), " ", tfArray1[1].Y.ToString(), " ", tfArray1[2].X.ToString(), " ", tfArray1[2].Y.ToString() } ;
                                text3 = string.Concat(textArray5);
                                string text10 = text3;
                                string[] textArray6 = new string[13] { text10, "C ", tfArray1[3].X.ToString(), " ", tfArray1[3].Y.ToString(), " ", tfArray1[4].X.ToString(), " ", tfArray1[4].Y.ToString(), " ", info8.MiddlePoint.X.ToString(), " ", info8.MiddlePoint.Y.ToString() } ;
                                text3 = string.Concat(textArray6);
                                text3 = text3 + info8.NextString;
                            }
                            goto Label_0F3A;
                        }
                        text3 = this.addInfo.PreString + this.addInfo.PointString;
                        string text8 = text3;
                        string[] textArray4 = new string[5] { text8, "L ", this.startPoint.X.ToString(), " ", this.startPoint.Y.ToString() } ;
                        text3 = string.Concat(textArray4);
                        text3 = text3 + this.addInfo.NextString;
                        goto Label_0F3A;
                    }
                    case BezierOperate.DelAnchor:
                    {
                        if (this.preInfo == null)
                        {
                            return;
                        }
                        text2 = this.preInfo.PreString;
                        int num6 = this.currentGraph.PointsInfo.IndexOf(this.preInfo);
                        if (((num6 + 1) < 0) || ((num6 + 1) >= this.currentGraph.PointsInfo.Count))
                        {
                            goto Label_0A75;
                        }
                        info6 = this.currentGraph.PointsInfo[num6 + 1];
                        flag2 = false;
                        if (info6.FirstControl.IsEmpty || this.preInfo.FirstControl.IsEmpty)
                        {
                            if (this.preInfo.IsStart && (this.preInfo.PreInfo != null))
                            {
                                string text6 = text2;
                                string[] textArray2 = new string[5] { text6, "M ", info6.MiddlePoint.X.ToString(), " ", info6.MiddlePoint.Y.ToString() } ;
                                text2 = string.Concat(textArray2);
                                int num7 = this.currentGraph.PointsInfo.IndexOf(this.preInfo.PreInfo);
                                PointInfo info7 = null;
                                for (int num8 = num6 + 2; num8 < num7; num8++)
                                {
                                    info7 = this.currentGraph.PointsInfo[num8];
                                    text2 = text2 + this.currentGraph.PointsInfo[num8].PointString;
                                }
                                if (info7 != null)
                                {
                                    PointF tf1 = PointF.Empty;
                                    PointF tf2 = PointF.Empty;
                                    if (!info7.FirstControl.IsEmpty)
                                    {
                                        tf1 = new PointF((2f * info7.MiddlePoint.X) - info7.SecondControl.X, (2f * info7.MiddlePoint.Y) - info7.SecondControl.Y);
                                    }
                                    if (!info6.NextControl.IsEmpty)
                                    {
                                        tf2 = new PointF((2f * info6.MiddlePoint.X) - info6.NextControl.X, (2f * info6.MiddlePoint.Y) - info6.NextControl.Y);
                                    }
                                    if (!tf1.IsEmpty || !tf2.IsEmpty)
                                    {
                                        tf1 = tf1.IsEmpty ? info7.MiddlePoint : tf1;
                                        tf2 = tf2.IsEmpty ? info6.MiddlePoint : tf2;
                                        string text7 = text2;
                                        string[] textArray3 = new string[13] { text7, "C ", tf1.X.ToString(), " ", tf1.Y.ToString(), " ", tf2.X.ToString(), " ", tf2.Y.ToString(), " ", info6.MiddlePoint.X.ToString(), " ", info6.MiddlePoint.Y.ToString() } ;
                                        text2 = string.Concat(textArray3);
                                    }
                                }
                                if (info7 != null)
                                {
                                    text2 = text2 + "Z";
                                }
                                flag2 = true;
                                if (((num7 + 1) >= 0) && ((num7 + 1) < this.currentGraph.PointsInfo.Count))
                                {
                                    info7 = this.currentGraph.PointsInfo[num7 + 1];
                                    text2 = text2 + info7.PointString + info7.NextString;
                                }
                            }
                            else
                            {
                                text2 = text2 + info6.PointString;
                            }
                            goto Label_0A61;
                        }
                        string text5 = text2;
                        string[] textArray1 = new string[13] { text5, "C ", this.preInfo.FirstControl.X.ToString(), " ", this.preInfo.FirstControl.Y.ToString(), " ", info6.SecondControl.X.ToString(), " ", info6.SecondControl.Y.ToString(), " ", info6.MiddlePoint.X.ToString(), " ", info6.MiddlePoint.Y.ToString() } ;
                        text2 = string.Concat(textArray1);
                        goto Label_0A61;
                    }
                    case BezierOperate.ChangeAnchor:
                    {
                        return;
                    }
                    case BezierOperate.MoveAnchor:
                    {
                        if (!this.activePoints.Contains(this.preInfo))
                        {
                            this.activePoints.Clear();
                            this.activePoints.Add(this.preInfo);
                        }
                        this.currentInfo = this.preInfo;
                        this.oldindex = this.currentGraph.PointsInfo.IndexOf(this.currentInfo);
                        this.activeindex = new int[this.activePoints.Count];
                        int num5 = 0;
                        PointInfoCollection.PointInfoEnumerator enumerator3 = this.activePoints.GetEnumerator();
                        while (enumerator3.MoveNext())
                        {
                            PointInfo info5 = enumerator3.Current;
                            this.activeindex[num5] = this.currentGraph.PointsInfo.IndexOf(info5);
                            num5++;
                        }
                        this.mouseAreaControl.Invalidate();
                        return;
                    }
                    case BezierOperate.ConvertAnchor:
                    {
                        this.currentInfo = this.preInfo;
                        this.oldindex = this.currentGraph.PointsInfo.IndexOf(this.currentinfo);
                        if (!this.activePoints.Contains(this.currentInfo))
                        {
                            int[] numArray1 = new int[1] { this.oldindex } ;
                            this.activeindex = numArray1;
                            this.activePoints.Clear();
                            this.activePoints.Add(this.currentinfo);
                        }
                        this.mouseAreaControl.Invalidate();
                        return;
                    }
                    case BezierOperate.ChangeEndAnchor:
                    {
                        this.currentinfo = this.preInfo;
                        return;
                    }
                    case BezierOperate.CloseFigure:
                    {
                        if (this.currentGraph.PointsInfo.Contains(this.preInfo))
                        {
                            PointInfo info10 = null;
                            if (this.SubpathList.ContainsKey(this.preInfo.SubPath))
                            {
                                PointInfoCollection collection1 = (PointInfoCollection) this.SubpathList[this.preInfo.SubPath];
                                info10 = collection1[collection1.Count - 1];
                            }
                            if (info10 == null)
                            {
                                return;
                            }
                            bool flag5 = this.mouseAreaControl.SVGDocument.AcceptChanges;
                            this.mouseAreaControl.SVGDocument.AcceptChanges = true;
                            this.mouseAreaControl.SVGDocument.NumberOfUndoOperations = 200;
                            string text4 = info10.PreString + info10.PointString;
                            PointF tf3 = info10.NextControl;
                            PointF tf4 = this.preInfo.SecondControl;
                            if (info10.NextControl.IsEmpty && !info10.SecondControl.IsEmpty)
                            {
                                tf3 = new PointF((2f * info10.MiddlePoint.X) - info10.SecondControl.X, (2f * info10.MiddlePoint.Y) - info10.SecondControl.Y);
                            }
                            if (!tf3.IsEmpty || !tf4.IsEmpty)
                            {
                                tf3 = tf3.IsEmpty ? info10.MiddlePoint : tf3;
                                tf4 = tf4.IsEmpty ? this.preInfo.MiddlePoint : tf4;
                                if ((Control.ModifierKeys & Keys.Alt) != Keys.Alt)
                                {
                                    string text11 = text4;
                                    string[] textArray7 = new string[13] { text11, "C ", tf3.X.ToString(), " ", tf3.Y.ToString(), " ", tf4.X.ToString(), " ", tf4.Y.ToString(), " ", this.preInfo.MiddlePoint.X.ToString(), " ", this.preInfo.MiddlePoint.Y.ToString() } ;
                                    text4 = string.Concat(textArray7);
                                }
                                else
                                {
                                    string text12 = text4;
                                    string[] textArray8 = new string[13] { text12, "C ", tf3.X.ToString(), " ", tf3.Y.ToString(), " ", this.preInfo.MiddlePoint.X.ToString(), " ", this.preInfo.MiddlePoint.Y.ToString(), " ", this.preInfo.MiddlePoint.X.ToString(), " ", this.preInfo.MiddlePoint.Y.ToString() } ;
                                    text4 = string.Concat(textArray8);
                                }
                            }
                            text4 = text4 + "Z";
                            if (text4.Length > 0)
                            {
                                this.Update(text4, this.oldstr);
                            }
                            this.mouseAreaControl.SVGDocument.NotifyUndo();
                            this.mouseAreaControl.SVGDocument.AcceptChanges = flag5;
                            this.activeindex = new int[0];
                            this.oldindex = -1;
                            this.activePoints.Clear();
                            this.currentInfo = null;
                        }
                        return;
                    }
                }
            }
            return;
        Label_01E2:
            if (flag1)
            {
                IGraph graph1 = this.mouseAreaControl.PicturePanel.PreGraph;
                if (graph1 == null)
                {
                    return;
                }
                this.currentGraph = (IGraph) ((SvgElement) graph1).Clone();
                this.mouseAreaControl.SVGDocument.AcceptChanges = false;
                if (this.currentGraph != null)
                {
                    ((SvgElement) this.currentGraph).RemoveAttribute("d");
                }
                if (((SvgElement) this.currentGraph) is IGraphPath)
                {
                    if ((((SvgElement) graph1).GetAttribute("style") != string.Empty) && (((SvgElement) graph1).GetAttribute("style") != null))
                    {
                        this.mouseAreaControl.SVGDocument.AcceptChanges = false;
                        AttributeFunc.SetAttributeValue((SvgElement) this.currentGraph, "style", ((SvgElement) this.currentGraph).GetAttribute("style"));
                    }
                    ISvgBrush brush1 = ((IGraphPath) graph1).GraphBrush;
                    if (brush1 is SvgElement)
                    {
                        ISvgBrush brush2 = (ISvgBrush) ((SvgElement) brush1).Clone();
                        ((IGraphPath) this.currentGraph).GraphBrush = brush2;
                        ((SvgElement) brush2).pretime = -1;
                    }
                    else
                    {
                        ((IGraphPath) this.currentGraph).GraphBrush = brush1;
                    }
                    brush1 = ((IGraphPath) graph1).GraphStroke.Brush;
                    if (brush1 is SvgElement)
                    {
                        ISvgBrush brush3 = (ISvgBrush) ((SvgElement) brush1).Clone();
                        ((IGraphPath) this.currentGraph).GraphStroke = new Stroke(brush3);
                        ((SvgElement) brush3).pretime = -1;
                    }
                    else
                    {
                        ((IGraphPath) this.currentGraph).GraphStroke.Brush = brush1;
                    }
                }
            }
            int num2 = 0;
            this.preDrawInfo = null;
            PointInfoCollection.PointInfoEnumerator enumerator2 = this.activePoints.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                PointInfo info3 = enumerator2.Current;
                if (num2 == 0)
                {
                    this.preDrawInfo = info3;
                }
                else if (this.preDrawInfo.SubPath != info3.SubPath)
                {
                    this.preDrawInfo = null;
                    break;
                }
                this.preDrawInfo = info3;
                num2++;
            }
            if (this.preDrawInfo != null)
            {
                int num3 = this.currentGraph.PointsInfo.IndexOf(this.preDrawInfo);
                if ((num3 >= 0) && (num3 < this.currentGraph.PointsInfo.Count))
                {
                    for (int num4 = num3 + 1; num4 < this.currentGraph.PointsInfo.Count; num4++)
                    {
                        PointInfo info4 = this.currentGraph.PointsInfo[num4];
                        if (info4.SubPath != this.preDrawInfo.SubPath)
                        {
                            break;
                        }
                        this.preDrawInfo = info4;
                    }
                }
                else
                {
                    this.preDrawInfo = null;
                }
            }
            this.currentInfo = this.preDrawInfo;
            if ((this.currentinfo != null) && this.currentinfo.IsEnd)
            {
                this.currentinfo = null;
            }
            this.preDrawInfo = this.currentinfo;
            this.oldindex = this.currentGraph.PointsInfo.IndexOf(this.currentinfo);
            return;
        Label_0A61:
            if (!flag2)
            {
                text2 = text2 + info6.NextString;
            }
        Label_0A75:
            if (((this.preInfo.PreInfo != null) && (this.preInfo.NextInfo != null)) && (this.preInfo.PreInfo.IsStart && this.preInfo.NextInfo.IsEnd))
            {
                text2 = this.preInfo.PreInfo.PreString + this.preInfo.PreInfo.PointString;
                int num9 = this.currentGraph.PointsInfo.IndexOf(this.preInfo.NextInfo);
                if ((num9 >= 0) && ((num9 + 1) < this.currentGraph.PointsInfo.Count))
                {
                    text2 = text2 + this.currentGraph.PointsInfo[num9 + 1].PointString + this.currentGraph.PointsInfo[num9 + 1].NextString;
                }
            }
            bool flag3 = this.mouseAreaControl.SVGDocument.AcceptChanges;
            this.mouseAreaControl.SVGDocument.AcceptChanges = true;
            this.mouseAreaControl.SVGDocument.NumberOfUndoOperations = 200;
            this.Update(text2, this.oldstr);
            this.mouseAreaControl.SVGDocument.NotifyUndo();
            this.mouseAreaControl.SVGDocument.AcceptChanges = flag3;
            this.oldindex = -1;
            this.activeindex = new int[0];
            this.activePoints.Clear();
            this.currentInfo = null;
            return;
        Label_0F3A:
            if (text3.Length > 0)
            {
                bool flag4 = this.mouseAreaControl.SVGDocument.AcceptChanges;
                this.mouseAreaControl.SVGDocument.AcceptChanges = true;
                this.mouseAreaControl.SVGDocument.NumberOfUndoOperations = 200;
                this.Update(text3, this.oldstr);
                this.mouseAreaControl.SVGDocument.NotifyUndo();
                this.mouseAreaControl.SVGDocument.AcceptChanges = flag4;
            }
            if (((num10 + 1) >= 0) && ((num10 + 1) < this.currentGraph.PointsInfo.Count))
            {
                this.activePoints.Clear();
                this.activePoints.Add(this.currentGraph.PointsInfo[num10 + 1]);
                this.currentInfo = this.currentGraph.PointsInfo[num10 + 1];
            }
        Label_100E:
            this.activeindex = new int[this.activePoints.Count];
            int num11 = 0;
            PointInfoCollection.PointInfoEnumerator enumerator4 = this.activePoints.GetEnumerator();
            while (enumerator4.MoveNext())
            {
                PointInfo info9 = enumerator4.Current;
                this.activeindex[num11] = this.currentGraph.PointsInfo.IndexOf(info9);
                num11++;
            }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            if (this.currentGraph != null)
            {
                if (e.Button == MouseButtons.None)
                {
                    this.preInfo = null;
                    this.addInfo = null;
                    this.moveinfo = null;
                    Pen pen1 = new Pen(Color.Black, 4f);
                    pen1.Alignment = PenAlignment.Center;
                    if (Control.ModifierKeys == (Keys.Control | Keys.Shift))
                    {
                        this.CurrentOperate = BezierOperate.CenterPoint;
                        this.incenter = false;
                        RectangleF ef1 = new RectangleF(this.mouseAreaControl.CenterPoint.X - 4f, this.mouseAreaControl.CenterPoint.Y - 4f, 8f, 8f);
                        PointF tf1 = new PointF((float) e.X, (float) e.Y);
                        if (ef1.Contains(tf1))
                        {
                            this.incenter = true;
                        }
                        return;
                    }
                    PointF tf2 = new PointF((float) e.X, (float) e.Y);
                    GraphicsPath path1 = new GraphicsPath();
                    PointInfoCollection.PointInfoEnumerator enumerator1 = this.currentGraph.PointsInfo.GetEnumerator();
                    while (enumerator1.MoveNext())
                    {
                        PointInfo info1 = enumerator1.Current;
                        GraphicsPath path2 = new GraphicsPath();
                        path2.AddRectangle(new RectangleF(info1.MiddlePoint.X - 3f, info1.MiddlePoint.Y - 3f, 6f, 6f));
                        path2.Transform(this.revertMatrix);
                        if (path2.IsVisible(tf2) || path2.IsOutlineVisible(tf2, pen1))
                        {
                            this.preInfo = info1;
                            PointInfoCollection collection1 = new PointInfoCollection();
                            if (this.SubpathList.ContainsKey(info1.SubPath))
                            {
                                collection1 = (PointInfoCollection) this.SubpathList[info1.SubPath];
                            }
                            if ((((Control.ModifierKeys & Keys.Control) == Keys.Control) && ((Control.ModifierKeys & Keys.Alt) != Keys.Alt)) || (this.mouseAreaControl.CurrentOperation == ToolOperation.ShapeTransform)
                                   || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom14) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom15))
                            {
                                this.CurrentOperate = BezierOperate.MoveAnchor;
                                return;
                            }
                            if ((info1.IsStart && (info1.PreInfo == null)) && (info1.NextInfo != null))
                            {
                                this.CurrentOperate = BezierOperate.CloseFigure;
                                return;
                            }
                            if ((collection1.IndexOf(info1) == (collection1.Count - 1)) && !info1.IsEnd)
                            {
                                this.CurrentOperate = BezierOperate.ChangeEndAnchor;
                                this.preInfo = info1;
                                return;
                            }
                            if (((Control.ModifierKeys & Keys.Alt) == Keys.Alt) || (this.mouseAreaControl.CurrentOperation == ToolOperation.ConvertAnchor))
                            {
                                this.CurrentOperate = BezierOperate.ConvertAnchor;
                                return;
                            }
                            this.CurrentOperate = BezierOperate.DelAnchor;
                            return;
                        }
                        if (info1.NextInfo != null)
                        {
                            path1.Reset();
                            if (info1.NextInfo.FirstControl.IsEmpty)
                            {
                                path1.AddLine(info1.MiddlePoint, info1.NextInfo.MiddlePoint);
                            }
                            else
                            {
                                path1.AddBezier(info1.MiddlePoint, info1.NextInfo.FirstControl, info1.NextInfo.SecondControl, info1.NextInfo.MiddlePoint);
                            }
                            path1.Transform(this.revertMatrix);
                            if (path1.IsOutlineVisible(tf2, pen1))
                            {
                                if (((Control.ModifierKeys & Keys.Control) == Keys.Control) || (this.mouseAreaControl.CurrentOperation == ToolOperation.ShapeTransform) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom11)
                                    || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom12) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom13) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom14) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom15))
                                {
                                    this.CurrentOperate = BezierOperate.MovePath;
                                    return;
                                }
                                if (((Control.ModifierKeys & Keys.Alt) != Keys.Alt) && (this.mouseAreaControl.CurrentOperation != ToolOperation.ConvertAnchor))
                                {
                                    this.addInfo = info1;
                                    this.CurrentOperate = BezierOperate.AddAnchor;
                                }
                                return;
                            }
                        }
                    }
                    if ((this.currentInfo != null) && (((this.mouseAreaControl.CurrentOperation == ToolOperation.ShapeTransform) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom11) || ((Control.ModifierKeys & Keys.Control) == Keys.Control)) || ((Control.ModifierKeys & Keys.Alt) == Keys.Alt))
                        || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom12) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom13) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom14) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom15))
                    {
                        int num1 = this.currentGraph.PointsInfo.IndexOf(this.currentInfo);
                        if (!this.currentInfo.FirstControl.IsEmpty)
                        {
                            GraphicsPath path3 = new GraphicsPath();
                            path3.AddRectangle(new RectangleF(this.currentInfo.FirstControl.X - 3f, this.currentInfo.FirstControl.Y - 2f, 4f, 4f));
                            path3.Transform(this.revertMatrix);
                            if (path3.IsVisible(tf2) || path3.IsOutlineVisible(tf2, pen1))
                            {
                                this.moveinfo = this.currentInfo.PreInfo;
                                this.movePoint = this.currentInfo.FirstControl;
                                this.CurrentOperate = BezierOperate.MoveControl;
                                return;
                            }
                        }
                        GraphicsPath path4 = new GraphicsPath();
                        GraphicsPath path5 = new GraphicsPath();
                        path4.AddRectangle(new RectangleF(this.currentInfo.SecondControl.X - 2f, this.currentInfo.SecondControl.Y - 2f, 4f, 4f));
                        path5.AddRectangle(new RectangleF(this.currentInfo.NextControl.X - 2f, this.currentInfo.NextControl.Y - 2f, 4f, 4f));
                        path4.Transform(this.revertMatrix);
                        path5.Transform(this.revertMatrix);
                        if ((!this.currentInfo.SecondControl.IsEmpty && (path4.IsVisible(tf2) || path4.IsOutlineVisible(tf2, pen1))) || (!this.currentInfo.NextControl.IsEmpty && (path5.IsVisible(tf2) || path5.IsOutlineVisible(tf2, pen1))))
                        {
                            this.moveinfo = this.currentInfo;
                            this.movePoint = (!this.currentInfo.SecondControl.IsEmpty && (path4.IsVisible(tf2) || path4.IsOutlineVisible(tf2, pen1))) ? this.currentInfo.SecondControl : this.currentInfo.NextControl;
                            this.CurrentOperate = BezierOperate.MoveControl;
                            return;
                        }
                        if (this.currentInfo.NextInfo != null)
                        {
                            path4.Reset();
                            path4.AddRectangle(new RectangleF(this.currentInfo.NextInfo.SecondControl.X - 2f, this.currentInfo.NextInfo.SecondControl.Y - 2f, 4f, 4f));
                            path4.Transform(this.revertMatrix);
                            if (!this.currentInfo.NextInfo.SecondControl.IsEmpty && (path4.IsVisible(tf2) || path4.IsOutlineVisible(tf2, pen1)))
                            {
                                this.moveinfo = this.currentInfo.NextInfo;
                                this.movePoint = this.currentInfo.NextInfo.SecondControl;
                                this.CurrentOperate = BezierOperate.MoveControl;
                                return;
                            }
                        }
                    }
                    if (((Control.ModifierKeys & Keys.Alt) == Keys.Alt) || (this.mouseAreaControl.CurrentOperation == ToolOperation.ConvertAnchor))
                    {
                        this.CurrentOperate = BezierOperate.ConvertAnchor;
                    }
                    else
                    {
                        if (((Control.ModifierKeys & Keys.Control) == Keys.Control) || (this.mouseAreaControl.CurrentOperation == ToolOperation.ShapeTransform) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom11)
                            || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom12) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom13) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom14) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom15))
                        {
                            this.CurrentOperate = BezierOperate.MoveAnchor;
                            return;
                        }
                        this.CurrentOperate = BezierOperate.Draw;
                    }
                    return;
                }
                if (e.Button == MouseButtons.Left)
                {
                    this.mouseAreaControl.Cursor = SpecialCursors.DragCursor;
                    SizeF ef2 = this.mouseAreaControl.PicturePanel.GridSize;
                    PointF tf3 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
                    float single1 = ef2.Height;
                    float single2 = ef2.Width;
                    if (this.mouseAreaControl.PicturePanel.SnapToGrid)
                    {
                        int num2 = (int) ((tf3.X + (single2 / 2f)) / single2);
                        int num3 = (int) ((tf3.Y + (single1 / 2f)) / single1);
                        tf3 = new PointF((float) ((int) (num2 * single2)), (float) ((int) (num3 * single1)));
                    }
                    tf3 = this.mouseAreaControl.PicturePanel.PointToSystem(tf3);
                    tf3 = this.PointToView(tf3);
                    Matrix matrix1 = new Matrix();
                    matrix1.Translate(tf3.X - this.startPoint.X, tf3.Y - this.startPoint.Y);
                    this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
                    this.win32.W32SetROP2(6);
                    this.win32.W32PolyDraw(this.reversePath);
                    this.reversePath.Reset();
                    switch (this.currentOperate)
                    {
                        case BezierOperate.Draw:
                        {
                            PointF tf4 = this.PointToView(new PointF((float) e.X, (float) e.Y));
                            PointF tf5 = new PointF((2f * this.startPoint.X) - tf4.X, (2f * this.startPoint.Y) - tf4.Y);
                            if (this.currentInfo != null)
                            {
                                this.reversePath.AddBezier(this.currentInfo.MiddlePoint, this.currentinfo.NextControl, tf5, this.startPoint);
                                this.reversePath.Transform(this.revertMatrix);
                            }
                            PointF[] tfArray10 = new PointF[3] { this.startPoint, tf4, tf5 } ;
                            PointF[] tfArray1 = tfArray10;
                            this.revertMatrix.TransformPoints(tfArray1);
                            RectangleF[] efArray1 = new RectangleF[3] { new RectangleF(tfArray1[1].X - 2f, tfArray1[1].Y - 2f, 4f, 4f), new RectangleF(tfArray1[2].X - 2f, tfArray1[2].Y - 2f, 4f, 4f), new RectangleF(tfArray1[0].X - 2f, tfArray1[0].Y - 2f, 4f, 4f) } ;
                            this.reversePath.AddRectangles(efArray1);
                            this.reversePath.AddLine(tfArray1[1], tfArray1[2]);
                            this.win32.W32PolyDraw(this.reversePath);
                            this.win32.ReleaseDC();
                            return;
                        }
                        case BezierOperate.AddAnchor:
                        case BezierOperate.DelAnchor:
                        case BezierOperate.ChangeAnchor:
                        case BezierOperate.CloseFigure:
                        {
                            return;
                        }
                        case BezierOperate.MoveAnchor:
                        {
                            PointInfoCollection.PointInfoEnumerator enumerator2 = this.activePoints.GetEnumerator();
                            while (enumerator2.MoveNext())
                            {
                                PointInfo info2 = enumerator2.Current;
                                PointInfo info3 = info2.PreInfo;
                                PointInfo info4 = info2.NextInfo;
                                PointF tf6 = info2.MiddlePoint;
                                PointF tf7 = info2.NextControl;
                                tf7 = tf7.IsEmpty ? info2.MiddlePoint : tf7;
                                PointF[] tfArray11 = new PointF[3] { (info2.SecondControl.IsEmpty && (info3 != null)) ? info3.MiddlePoint : info2.SecondControl, info2.MiddlePoint, tf7 } ;
                                PointF[] tfArray2 = tfArray11;
                                matrix1.TransformPoints(tfArray2);
                                this.reversePath.StartFigure();
                                if (info3 != null)
                                {
                                    PointF tf8 = info3.MiddlePoint;
                                    PointF tf9 = info2.FirstControl;
                                    if ((info2.IsStart && info3.IsEnd) && ((info3.MiddlePoint == info2.MiddlePoint) && (info3.PreInfo != null)))
                                    {
                                        tf8 = info3.PreInfo.MiddlePoint;
                                        tf9 = info3.FirstControl;
                                    }
                                    tf9 = tf9.IsEmpty ? tf8 : tf9;
                                    if (this.activePoints.Contains(info3))
                                    {
                                        PointF[] tfArray12 = new PointF[2] { tf8, tf9 } ;
                                        PointF[] tfArray3 = tfArray12;
                                        matrix1.TransformPoints(tfArray3);
                                        tf8 = tfArray3[0];
                                        tf9 = tfArray3[1];
                                    }
                                    if (tf8 != tfArray2[1])
                                    {
                                        if (info3.FirstControl.IsEmpty && info2.FirstControl.IsEmpty)
                                        {
                                            this.reversePath.AddLine(tf8, tfArray2[1]);
                                        }
                                        else
                                        {
                                            this.reversePath.AddBezier(tf8, tf9, tfArray2[0], tfArray2[1]);
                                        }
                                    }
                                }
                                this.reversePath.StartFigure();
                                if ((info4 != null) && !this.activePoints.Contains(info4))
                                {
                                    PointF tf10 = info4.SecondControl;
                                    tf10 = tf10.IsEmpty ? info4.MiddlePoint : tf10;
                                    if (info2.FirstControl.IsEmpty && info4.FirstControl.IsEmpty)
                                    {
                                        this.reversePath.AddLine(tfArray2[2], info4.MiddlePoint);
                                    }
                                    else
                                    {
                                        this.reversePath.AddBezier(tfArray2[1], tfArray2[2], tf10, info4.MiddlePoint);
                                    }
                                }
                            }
                            this.reversePath.Transform(this.revertMatrix);
                            if (this.currentInfo != null)
                            {
                                if (this.activePoints.Count == 1)
                                {
                                    PointF[] tfArray13 = new PointF[3] { this.currentInfo.SecondControl, this.currentInfo.MiddlePoint, this.currentInfo.NextControl } ;
                                    PointF[] tfArray4 = tfArray13;
                                    matrix1.TransformPoints(tfArray4);
                                    this.revertMatrix.TransformPoints(tfArray4);
                                    RectangleF[] efArray2 = new RectangleF[1] { new RectangleF(tfArray4[1].X - 2f, tfArray4[1].Y - 2f, 4f, 4f) } ;
                                    this.reversePath.AddRectangles(efArray2);
                                    if (!this.currentInfo.SecondControl.IsEmpty)
                                    {
                                        this.reversePath.AddRectangle(new RectangleF(tfArray4[0].X - 2f, tfArray4[0].Y - 2f, 4f, 4f));
                                        this.reversePath.AddLine(tfArray4[0], tfArray4[1]);
                                    }
                                    if (!this.currentInfo.NextControl.IsEmpty)
                                    {
                                        this.reversePath.AddRectangle(new RectangleF(tfArray4[2].X - 2f, tfArray4[2].Y - 2f, 4f, 4f));
                                        this.reversePath.AddLine(tfArray4[1], tfArray4[2]);
                                    }
                                }
                            }
                            else
                            {
                                int num4 = (int) Math.Min(this.startPoint.X, tf3.X);
                                int num5 = (int) Math.Min(this.startPoint.Y, tf3.Y);
                                int num6 = (int) Math.Max(this.startPoint.X, tf3.X);
                                int num7 = (int) Math.Max(this.startPoint.Y, tf3.Y);
                                this.reversePath.AddRectangle(new RectangleF((float) num4, (float) num5, (float) (num6 - num4), (float) (num7 - num5)));
                                this.reversePath.Transform(this.revertMatrix);
                            }
                            this.win32.W32PolyDraw(this.reversePath);
                            this.win32.ReleaseDC();
                            return;
                        }
                        case BezierOperate.ConvertAnchor:
                        {
                            if (this.currentInfo != null)
                            {
                                PointF[] tfArray18 = new PointF[2] { tf3, this.currentInfo.MiddlePoint } ;
                                PointF[] tfArray6 = tfArray18;
                                this.revertMatrix.TransformPoints(tfArray6);
                                PointF tf13 = tfArray6[0];
                                PointF tf14 = tfArray6[1];
                                if (this.currentInfo.NextInfo != null)
                                {
                                    this.reversePath.AddBezier(this.currentInfo.MiddlePoint, tf3, this.currentInfo.NextInfo.SecondControl.IsEmpty ? this.currentInfo.NextInfo.MiddlePoint : this.currentInfo.NextInfo.SecondControl, this.currentInfo.NextInfo.MiddlePoint);
                                }
                                if (this.currentInfo.PreInfo != null)
                                {
                                    this.reversePath.StartFigure();
                                    tf3 = new PointF((2f * this.currentInfo.MiddlePoint.X) - tf3.X, (2f * this.currentInfo.MiddlePoint.Y) - tf3.Y);
                                    PointInfo info5 = this.currentinfo.PreInfo;
                                    PointInfo info6 = this.currentinfo;
                                    if ((this.currentinfo.IsStart && info5.IsEnd) && (info5.MiddlePoint == this.currentinfo.MiddlePoint))
                                    {
                                        info6 = info5;
                                        info5 = info5.PreInfo;
                                    }
                                    if (info5 != null)
                                    {
                                        PointF tf15 = info6.FirstControl.IsEmpty ? info5.MiddlePoint : info6.FirstControl;
                                        this.reversePath.AddBezier(info5.MiddlePoint, tf15, tf3, info6.MiddlePoint);
                                    }
                                }
                                this.reversePath.Transform(this.revertMatrix);
                                this.reversePath.StartFigure();
                                RectangleF[] efArray5 = new RectangleF[1] { new RectangleF(tf14.X - 2f, tf14.Y - 2f, 4f, 4f) } ;
                                this.reversePath.AddRectangles(efArray5);
                                if (((Math.Abs((float) (tf13.X - tf3.X)) >= 1f) || (Math.Abs((float) (tf13.Y - tf3.Y)) >= 1f)) && (this.currentInfo.NextInfo != null))
                                {
                                    PointF tf16 = new PointF((2f * tf14.X) - tf13.X, (2f * tf14.Y) - tf13.Y);
                                    RectangleF[] efArray6 = new RectangleF[2] { new RectangleF(tf13.X - 2f, tf13.Y - 2f, 4f, 4f), new RectangleF(tf16.X - 2f, tf16.Y - 2f, 4f, 4f) } ;
                                    this.reversePath.AddRectangles(efArray6);
                                    this.reversePath.AddLine(tf16, tf14);
                                }
                                this.reversePath.AddLine(tf14, tf13);
                            }
                            this.win32.W32PolyDraw(this.reversePath);
                            this.win32.ReleaseDC();
                            return;
                        }
                        case BezierOperate.ChangeEndAnchor:
                        {
                            if (this.preInfo == null)
                            {
                                return;
                            }
                            PointF tf17 = new PointF((2f * this.preInfo.MiddlePoint.X) - tf3.X, (2f * this.preInfo.MiddlePoint.Y) - tf3.Y);
                            if ((((Control.ModifierKeys & Keys.Alt) != Keys.Alt) && !this.preInfo.FirstControl.IsEmpty) && (this.preInfo.MiddlePoint != this.preInfo.SecondControl))
                            {
                                this.reversePath.AddBezier(this.preInfo.PreInfo.MiddlePoint, this.preInfo.FirstControl, tf3, this.preInfo.MiddlePoint);
                                this.reversePath.Transform(this.revertMatrix);
                                PointF[] tfArray21 = new PointF[3] { tf3, this.preInfo.MiddlePoint, tf17 } ;
                                PointF[] tfArray9 = tfArray21;
                                this.revertMatrix.TransformPoints(tfArray9);
                                RectangleF[] efArray7 = new RectangleF[3] { new RectangleF(tfArray9[0].X - 2f, tfArray9[0].Y - 2f, 4f, 4f), new RectangleF(tfArray9[1].X - 2f, tfArray9[1].Y - 2f, 4f, 4f), new RectangleF(tfArray9[2].X - 2f, tfArray9[2].Y - 2f, 4f, 4f) } ;
                                this.reversePath.AddRectangles(efArray7);
                                this.reversePath.AddLine(tfArray9[0], tfArray9[2]);
                                goto Label_2164;
                            }
                            PointF[] tfArray20 = new PointF[2] { this.preInfo.MiddlePoint, tf3 } ;
                            PointF[] tfArray8 = tfArray20;
                            this.revertMatrix.TransformPoints(tfArray8);
                            this.reversePath.AddLine(tfArray8[0], tfArray8[1]);
                            goto Label_2164;
                        }
                        case BezierOperate.MoveControl:
                        {
                            if (this.moveinfo != null)
                            {
                                PointF[] tfArray14 = new PointF[2] { this.moveinfo.MiddlePoint, tf3 } ;
                                PointF[] tfArray5 = tfArray14;
                                this.revertMatrix.TransformPoints(tfArray5);
                                PointF tf11 = tfArray5[0];
                                PointF tf12 = tfArray5[1];
                                if (this.movePoint == this.moveinfo.SecondControl)
                                {
                                    if (this.moveinfo.PreInfo != null)
                                    {
                                        if (this.moveinfo.IsStart && this.moveinfo.PreInfo.IsEnd)
                                        {
                                            if (this.moveinfo.PreInfo.PreInfo != null)
                                            {												
												this.reversePath.AddBezier(this.moveinfo.PreInfo.PreInfo.MiddlePoint, this.moveinfo.PreInfo.FirstControl, tf3, this.moveinfo.PreInfo.MiddlePoint);
												
                                            }
                                        }
                                        else
										{
											if (this.moveinfo.Command=="A")
											{
												float ry=matrix1.OffsetY+this.moveinfo.Ry;
												ry=ry>5?ry:this.moveinfo.Ry;

												ExtendedGraphicsPath expath1 = new ExtendedGraphicsPath(this.reversePath);
	                            
												expath1.AddArc(this.moveinfo.PreInfo.MiddlePoint,this.moveinfo.MiddlePoint,this.moveinfo.Rx,ry,this.moveinfo.LargeArcFlage==1,this.moveinfo.SweepFlage==1,this.moveinfo.Angle);
											}
											else
											{
												this.reversePath.AddBezier(this.moveinfo.PreInfo.MiddlePoint, this.moveinfo.FirstControl, tf3, this.moveinfo.MiddlePoint);
											}
                                        }
                                    }
                                    float single3 = tf12.X;
                                    float single4 = tf12.Y;
                                    bool flag1 = false;
                                    if (((this.moveinfo.NextInfo != null) && ((Control.ModifierKeys & Keys.Alt) != Keys.Alt)) && !this.moveinfo.Steep)
                                    {
                                        if (!this.moveinfo.NextInfo.FirstControl.IsEmpty)
                                        {
                                            float single5 = (float) Math.Sqrt(Math.Pow((double) (this.moveinfo.SecondControl.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (this.moveinfo.SecondControl.Y - this.moveinfo.MiddlePoint.Y), 2));
                                            float single6 = (float) Math.Sqrt(Math.Pow((double) (tf3.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (tf3.Y - this.moveinfo.MiddlePoint.Y), 2));
                                            if (single5 == 0f)
                                            {
                                                single5 += 0.0001f;
                                            }
                                            float single7 = single6 / single5;
                                            single3 = this.moveinfo.MiddlePoint.X + (single7 * (this.moveinfo.MiddlePoint.X - tf3.X));
                                            single4 = this.moveinfo.MiddlePoint.Y + (single7 * (this.moveinfo.MiddlePoint.Y - tf3.Y));
                                            this.reversePath.AddBezier(this.moveinfo.MiddlePoint, new PointF(single3, single4), this.moveinfo.NextInfo.SecondControl, this.moveinfo.NextInfo.MiddlePoint);
                                            PointF[] tfArray15 = new PointF[1] { new PointF(single3, single4) } ;
                                            tfArray5 = tfArray15;
                                            this.revertMatrix.TransformPoints(tfArray5);
                                            single3 = tfArray5[0].X;
                                            single4 = tfArray5[0].Y;
                                        }
                                        flag1 = this.activePoints.Contains(this.moveinfo.NextInfo);
                                    }
                                    this.reversePath.Transform(this.revertMatrix);
                                    this.reversePath.StartFigure();
                                    RectangleF[] efArray3 = new RectangleF[2] { new RectangleF(tf11.X - 2f, tf11.Y - 2f, 4f, 4f), new RectangleF(tf12.X - 2f, tf12.Y - 2f, 4f, 4f) } ;
                                    this.reversePath.AddRectangles(efArray3);
                                    if ((Math.Abs((float) (single3 - tf12.X)) >= 1f) || (Math.Abs((float) (single4 - tf12.Y)) >= 1f))
                                    {
                                        this.reversePath.AddRectangle(new RectangleF(single3 - 2f, single4 - 2f, 4f, 4f));
                                        this.reversePath.AddLine(new PointF(single3, single4), tf11);
                                    }
                                    this.reversePath.AddLine(tf11, tf12);
                                }
                                else if (this.movePoint == this.moveinfo.NextControl)
                                {
									if (this.moveinfo.Command=="A")
									{
										float rx=matrix1.OffsetX+this.moveinfo.Rx;
										rx=rx>5?rx:this.moveinfo.Rx;

										ExtendedGraphicsPath expath1 = new ExtendedGraphicsPath(this.reversePath);
                                
										expath1.AddArc(this.moveinfo.PreInfo.MiddlePoint,this.moveinfo.MiddlePoint,rx,this.moveinfo.Ry,this.moveinfo.LargeArcFlage==1,this.moveinfo.SweepFlage==1,this.moveinfo.Angle);
									}
									else 
                                    {
										if (this.moveinfo.NextInfo != null)
										{
											this.reversePath.AddBezier(this.moveinfo.MiddlePoint, tf3, this.moveinfo.NextInfo.SecondControl, this.moveinfo.NextInfo.MiddlePoint);
										}
                                    }
                                    float single8 = tf12.X;
                                    float single9 = tf12.Y;
                                    bool flag2 = true;
                                    if (((this.moveinfo.PreInfo != null) && ((Control.ModifierKeys & Keys.Alt) != Keys.Alt)) && !this.moveinfo.Steep)
                                    {
                                        if ((this.moveinfo.IsStart && this.moveinfo.PreInfo.IsEnd) && (this.moveinfo.PreInfo.PreInfo != null))
                                        {
                                            float single10 = (float) Math.Sqrt(Math.Pow((double) (this.moveinfo.NextControl.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (this.moveinfo.NextControl.Y - this.moveinfo.MiddlePoint.Y), 2));
                                            float single11 = (float) Math.Sqrt(Math.Pow((double) (tf3.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (tf3.Y - this.moveinfo.MiddlePoint.Y), 2));
                                            if (single10 == 0f)
                                            {
                                                single10 += 0.0001f;
                                            }
                                            float single12 = single11 / single10;
                                            single8 = this.moveinfo.MiddlePoint.X + (single12 * (this.moveinfo.MiddlePoint.X - tf3.X));
                                            single9 = this.moveinfo.MiddlePoint.Y + (single12 * (this.moveinfo.MiddlePoint.Y - tf3.Y));
                                            this.reversePath.StartFigure();
                                            this.reversePath.AddBezier(this.moveinfo.PreInfo.PreInfo.MiddlePoint, this.moveinfo.PreInfo.FirstControl, new PointF(single8, single9), this.moveinfo.MiddlePoint);
                                            PointF[] tfArray16 = new PointF[1] { new PointF(single8, single9) } ;
                                            tfArray5 = tfArray16;
                                            this.revertMatrix.TransformPoints(tfArray5);
                                            single8 = tfArray5[0].X;
                                            single9 = tfArray5[0].Y;
                                        }
                                        else if (!this.moveinfo.SecondControl.IsEmpty)
                                        {
                                            float single13 = (float) Math.Sqrt(Math.Pow((double) (this.moveinfo.NextControl.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (this.moveinfo.NextControl.Y - this.moveinfo.MiddlePoint.Y), 2));
                                            float single14 = (float) Math.Sqrt(Math.Pow((double) (tf3.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (tf3.Y - this.moveinfo.MiddlePoint.Y), 2));
                                            if (single13 == 0f)
                                            {
                                                single13 += 0.0001f;
                                            }
                                            float single15 = single14 / single13;
                                            single8 = this.moveinfo.MiddlePoint.X + (single15 * (this.moveinfo.MiddlePoint.X - tf3.X));
                                            single9 = this.moveinfo.MiddlePoint.Y + (single15 * (this.moveinfo.MiddlePoint.Y - tf3.Y));
                                            this.reversePath.StartFigure();
                                            this.reversePath.AddBezier(this.moveinfo.PreInfo.MiddlePoint, this.moveinfo.FirstControl, new PointF(single8, single9), this.moveinfo.MiddlePoint);
                                            PointF[] tfArray17 = new PointF[1] { new PointF(single8, single9) } ;
                                            tfArray5 = tfArray17;
                                            this.revertMatrix.TransformPoints(tfArray5);
                                            single8 = tfArray5[0].X;
                                            single9 = tfArray5[0].Y;
                                        }
                                        flag2 = this.activePoints.Contains(this.moveinfo.PreInfo);
                                    }
                                    this.reversePath.Transform(this.revertMatrix);
                                    this.reversePath.StartFigure();
                                    RectangleF[] efArray4 = new RectangleF[2] { new RectangleF(tf11.X - 2f, tf11.Y - 2f, 4f, 4f), new RectangleF(tf12.X - 2f, tf12.Y - 2f, 4f, 4f) } ;
                                    this.reversePath.AddRectangles(efArray4);
                                    if ((Math.Abs((float) (single8 - tf12.X)) >= 1f) || (Math.Abs((float) (single9 - tf12.Y)) >= 1f))
                                    {
                                        this.reversePath.AddRectangle(new RectangleF(single8 - 2f, single9 - 2f, 4f, 4f));
                                        this.reversePath.AddLine(new PointF(single8, single9), tf11);
                                    }
                                    this.reversePath.AddLine(tf11, tf12);
                                }
                                this.win32.W32PolyDraw(this.reversePath);
                                this.win32.ReleaseDC();
                            }
                            return;
                        }
                        case BezierOperate.MovePath:
                        {
                            this.reversePath = (GraphicsPath) this.currentGraph.GPath.Clone();
                            this.reversePath.Transform(matrix1);
                            this.reversePath.Transform(this.revertMatrix);
                            this.win32.W32PolyDraw(this.reversePath);
                            this.win32.ReleaseDC();
                            return;
                        }
                        case BezierOperate.CenterPoint:
                        {
                            PointF[] tfArray19 = new PointF[1] { tf3 } ;
                            PointF[] tfArray7 = tfArray19;
                            this.revertMatrix.TransformPoints(tfArray7);
                            this.reversePath.AddEllipse((float) (tfArray7[0].X - 4f), (float) (tfArray7[0].Y - 4f), (float) 8f, (float) 8f);
                            this.win32.W32PolyDraw(this.reversePath);
                            this.win32.ReleaseDC();
                            return;
                        }
                    }
                }
            }
            return;
        Label_2164:
            this.win32.W32PolyDraw(this.reversePath);
            this.win32.ReleaseDC();
        }

        public void OnMouseUp(MouseEventArgs e)
        {
            string text7;
            if (this.currentGraph == null)
            {
                return;
            }
            if (!this.activePoints.Contains(this.currentinfo))
            {
                this.activePoints.Add(this.currentinfo);
            }
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            SizeF ef1 = this.mouseAreaControl.PicturePanel.GridSize;
            PointF tf1 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
            PointF tf2 = this.PointToView(new PointF((float) e.X, (float) e.Y));
            if (((Math.Abs((float) (tf2.X - this.startPoint.X)) < 1f) && (Math.Abs((float) (tf2.Y - this.startPoint.Y)) < 1f)) && ((this.currentOperate != BezierOperate.ConvertAnchor) && (this.currentOperate != BezierOperate.Draw)))
            {
                this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
                this.win32.W32SetROP2(6);
                this.win32.W32PolyDraw(this.reversePath);
                this.win32.ReleaseDC();
                return;
            }
            float single1 = ef1.Height;
            float single2 = ef1.Width;
            if (this.mouseAreaControl.PicturePanel.SnapToGrid)
            {
                int num1 = (int) ((tf1.X + (single2 / 2f)) / single2);
                int num2 = (int) ((tf1.Y + (single1 / 2f)) / single1);
                tf1 = new PointF((float) ((int) (num1 * single2)), (float) ((int) (num2 * single1)));
            }
            tf1 = this.mouseAreaControl.PicturePanel.PointToSystem(tf1);
            tf1 = this.PointToView(tf1);
            Matrix matrix1 = new Matrix();
            matrix1.Translate(tf1.X - this.startPoint.X, tf1.Y - this.startPoint.Y);
            bool flag1 = this.mouseAreaControl.SVGDocument.AcceptChanges;
            this.mouseAreaControl.SVGDocument.AcceptChanges = true;
            this.mouseAreaControl.SVGDocument.NumberOfUndoOperations = 200;
            if ((((SvgElement) this.currentGraph).Name != "path") && (((SvgElement) this.currentGraph).Name != "animateMotion"))
            {
                IGraph graph1 = this.mouseAreaControl.PicturePanel.PreGraph;
                if (graph1 == null)
                {
                    return;
                }
                this.currentGraph = (IGraph) ((SvgElement) graph1).Clone();
                this.mouseAreaControl.SVGDocument.AcceptChanges = false;
                if (this.currentGraph != null)
                {
                    ((SvgElement) this.currentGraph).RemoveAttribute("d");
                }
                if (((SvgElement) this.currentGraph) is IGraphPath)
                {
                    if ((((SvgElement) graph1).GetAttribute("style") != string.Empty) && (((SvgElement) graph1).GetAttribute("style") != null))
                    {
                        this.mouseAreaControl.SVGDocument.AcceptChanges = false;
                        AttributeFunc.SetAttributeValue((SvgElement) this.currentGraph, "style", ((SvgElement) graph1).GetAttribute("style"));
                    }
                    ISvgBrush brush1 = ((IGraphPath) graph1).GraphBrush;
                    if (brush1 is SvgElement)
                    {
                        ISvgBrush brush2 = (ISvgBrush) ((SvgElement) brush1).Clone();
                        ((IGraphPath) this.currentGraph).GraphBrush = brush2;
                        ((SvgElement) brush2).pretime = -1;
                    }
                    else
                    {
                        ((IGraphPath) this.currentGraph).GraphBrush = brush1;
                    }
                    brush1 = ((IGraphPath) graph1).GraphStroke.Brush;
                    if (brush1 is SvgElement)
                    {
                        ISvgBrush brush3 = (ISvgBrush) ((SvgElement) brush1).Clone();
                        ((IGraphPath) this.currentGraph).GraphStroke = new Stroke(brush3);
                        ((SvgElement) brush3).pretime = -1;
                    }
                    else
                    {
                        ((IGraphPath) this.currentGraph).GraphStroke.Brush = brush1;
                    }
                    this.oldstr = string.Empty;
                }
                this.mouseAreaControl.SVGDocument.AcceptChanges = flag1;
            }
            if ((this.currentGraph is SvgElement) && (((SvgElement) this.currentGraph).ParentNode == null))
            {
                IPath path1 = this.currentGraph;
                if (this.currentGraph is IGraph)
                {
                    this.mouseAreaControl.PicturePanel.AddElement((SvgElement) this.currentGraph);
                }
                else if ((this.currentGraph is MotionAnimate) && (this.mouseAreaControl.SVGDocument.CurrentElement != null))
                {
                    this.mouseAreaControl.SVGDocument.CurrentElement.PrependChild((SvgElement) this.currentGraph);
                }
                this.currentGraph = path1;
            }
            this.activeindex = new int[0];
            PointF tf3 = this.startPoint;
            PointF tf4 = this.endtrend;
            switch (this.currentOperate)
            {
                case BezierOperate.Draw:
                {
                    this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
                    this.win32.W32SetROP2(6);
                    this.win32.W32PolyDraw(this.reversePath);
                    this.win32.ReleaseDC();
                    if ((Math.Abs((float) (tf2.X - this.startPoint.X)) < 2f) && (Math.Abs((float) (tf2.Y - this.startPoint.Y)) < 2f))
                    {
                        tf1 = this.startPoint;
                    }
                    PointF tf5 = tf1;
                    PointF tf6 = new PointF((float) ((int) ((2f * this.startPoint.X) - tf5.X)), (float) ((int) ((2f * this.startPoint.Y) - tf5.Y)));
                    this.activePoints.Clear();
                    if (this.preDrawInfo == null)
                    {
                        string[] textArray1 = new string[5];
                        textArray1[0] = this.oldstr.Trim();
                        textArray1[1] = "M ";
                        float single25 = this.startPoint.X - 0f;
                        textArray1[2] = single25.ToString();
                        textArray1[3] = " ";
                        float single26 = this.startPoint.Y - 0f;
                        textArray1[4] = single26.ToString();
                        string text1 = string.Concat(textArray1);
                        this.Update(text1, this.oldstr);
                        PointInfo info1 = this.currentGraph.PointsInfo[this.currentGraph.PointsInfo.Count - 1];
                        info1.SecondControl = tf6;
                        info1.NextControl = tf5;
                        this.oldindex = this.currentGraph.PointsInfo.Count - 1;
                        int[] numArray1 = new int[1] { this.oldindex } ;
                        this.activeindex = numArray1;
                        this.starttrend = tf6;
                    }
                    else
                    {
                        PointF tf7 = this.startPoint;
                        float single3 = 0f;
                        float single4 = 0f;
                        string[] textArray2 = new string[12];
                        textArray2[0] = "C ";
                        textArray2[1] = this.preDrawInfo.NextControl.X.ToString();
                        textArray2[2] = " ";
                        textArray2[3] = this.preDrawInfo.NextControl.Y.ToString();
                        textArray2[4] = " ";
                        float single29 = tf6.X - single3;
                        textArray2[5] = single29.ToString();
                        textArray2[6] = " ";
                        float single30 = tf6.Y - single4;
                        textArray2[7] = single30.ToString();
                        textArray2[8] = " ";
                        float single31 = this.startPoint.X - single3;
                        textArray2[9] = single31.ToString();
                        textArray2[10] = " ";
                        float single32 = this.startPoint.Y - single4;
                        textArray2[11] = single32.ToString();
                        string text2 = string.Concat(textArray2);
                        if ((Point.Round(tf7) == Point.Round(tf1)) && (this.preDrawInfo.FirstControl.IsEmpty || (Point.Round(this.preDrawInfo.SecondControl) == Point.Round(this.preDrawInfo.MiddlePoint))))
                        {
                            text2 = "L " + this.startPoint.X.ToString() + " " + this.startPoint.Y.ToString();
                        }

                        string text3 = this.preDrawInfo.PreString;
                        text3 = this.preDrawInfo.PreString + this.preDrawInfo.PointString + text2 + this.preDrawInfo.NextString;
                        int num3 = this.currentGraph.PointsInfo.IndexOf(this.preDrawInfo);
                        this.Update(text3, this.oldstr);
                        if ((num3 + 1) < this.currentGraph.PointsInfo.Count)
                        {
                            PointInfo info2 = this.currentGraph.PointsInfo[num3 + 1];
                            this.oldindex = num3 + 1;
                            int[] numArray2 = new int[1] { this.oldindex } ;
                            this.activeindex = numArray2;
                            info2.NextControl = tf5;
                        }
                    }
                    this.endtrend = tf1;
                    goto Label_3EC7;
                }
                case BezierOperate.AddAnchor:
                case BezierOperate.DelAnchor:
                case BezierOperate.ChangeAnchor:
                case BezierOperate.CloseFigure:
                {
                    goto Label_3EC7;
                }
                case BezierOperate.MoveAnchor:
                {
                    this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
                    this.win32.W32SetROP2(6);
                    this.win32.W32PolyDraw(this.reversePath);
                    this.win32.ReleaseDC();
                    if (this.currentInfo != null)
                    {
                        this.activeindex = new int[this.activePoints.Count];
                        PointInfoCollection collection1 = new PointInfoCollection();
                        collection1.AddRange(this.activePoints);
                        if (this.activePoints.Count > 0)
                        {
                            int num8 = this.currentGraph.PointsInfo.IndexOf(this.activePoints[0]);
                            int num9 = this.currentGraph.PointsInfo.IndexOf(this.activePoints[this.activePoints.Count - 1]);
                            
							string text4 = this.activePoints[0].PreString;
                            string text5 = string.Empty;
                            int num10 = 0;
                            for (int num11 = num8; num11 < Math.Min(this.currentGraph.PointsInfo.Count, (int) (num9 + 2)); num11++)
                            {
                                PointInfo info4 = this.currentGraph.PointsInfo[num11];
                                if (this.activePoints.Contains(info4))
                                {
                                    this.activeindex[num10] = num11;
                                    num10++;
                                }
                                if (((info4.IsStart && (info4.PreInfo != null)) && (collection1.Contains(info4) && info4.PreInfo.IsEnd)) && ((info4.MiddlePoint == info4.PreInfo.MiddlePoint) && !collection1.Contains(info4.PreInfo)))
                                {
                                    collection1.Add(info4.PreInfo);
                                    num9 = Math.Max(this.currentGraph.PointsInfo.IndexOf(info4.PreInfo), num9);
                                }
                                if (((num11 - 1) >= 0) && ((num11 - 1) < this.currentGraph.PointsInfo.Count))
                                {
                                    if (collection1.Contains(this.currentGraph.PointsInfo[num11 - 1]))
                                    {
                                        if (collection1.Contains(info4))
                                        {
                                            PointF[] tfArray11 = new PointF[3] { info4.FirstControl, info4.SecondControl, info4.MiddlePoint } ;
                                            PointF[] tfArray1 = tfArray11;
                                            matrix1.TransformPoints(tfArray1);
                                            if (info4.IsStart)
                                            {
                                                text5 = "M " + tfArray1[2].X.ToString() + " " + tfArray1[2].Y.ToString();
                                                goto Label_1325;
                                            }
                                            if (info4.FirstControl.IsEmpty)
                                            {
                                                text5 = "L " + tfArray1[2].X.ToString() + " " + tfArray1[2].Y.ToString();
                                                goto Label_1325;
                                            }
                                            string[] textArray3 = new string[12] { "C ", tfArray1[0].X.ToString(), " ", tfArray1[0].Y.ToString(), " ", tfArray1[1].X.ToString(), " ", tfArray1[1].Y.ToString(), " ", tfArray1[2].X.ToString(), " ", tfArray1[2].Y.ToString() } ;
                                            text5 = string.Concat(textArray3);
                                            goto Label_1325;
                                        }
                                        PointF[] tfArray12 = new PointF[1] { info4.FirstControl } ;
                                        PointF[] tfArray2 = tfArray12;
                                        matrix1.TransformPoints(tfArray2);
                                        if (info4.IsStart || info4.FirstControl.IsEmpty)
                                        {
                                            text5 = info4.PointString;
                                        }
                                        else
                                        {
                                            string[] textArray4 = new string[12] { "C ", tfArray2[0].X.ToString(), " ", tfArray2[0].Y.ToString(), " ", info4.SecondControl.X.ToString(), " ", info4.SecondControl.Y.ToString(), " ", info4.MiddlePoint.X.ToString(), " ", info4.MiddlePoint.Y.ToString() } ;
                                            text5 = string.Concat(textArray4);
                                        }
                                        goto Label_1325;
                                    }
                                    if (collection1.Contains(info4))
                                    {
                                        PointF[] tfArray3 = new PointF[2] { info4.SecondControl, info4.MiddlePoint } ;
                                        
                                        matrix1.TransformPoints(tfArray3);
                                        if (info4.IsStart)
                                        {
                                            text5 = "M " + tfArray3[1].X.ToString() + " " + tfArray3[1].Y.ToString();
                                            goto Label_1325;
                                        }
										if(info4.Command.ToUpper()=="A")
										{
											text5=string.Concat(new string[]{"A",info4.Rx.ToString()," ",info4.Ry.ToString()," ",info4.Angle.ToString()," ",info4.LargeArcFlage.ToString()," ",info4.SweepFlage.ToString()," ",tfArray3[1].X.ToString() + " " + tfArray3[1].Y.ToString()});
											goto Label_1325;
										}
                                        if (info4.FirstControl.IsEmpty)
                                        {
                                            text5 = "L " + tfArray3[1].X.ToString() + " " + tfArray3[1].Y.ToString();
                                            goto Label_1325;
                                        }
										string[] textArray5;										
										textArray5 = new string[12] { "C ", info4.FirstControl.X.ToString(), " ", info4.FirstControl.Y.ToString(), " ", tfArray3[0].X.ToString(), " ", tfArray3[0].Y.ToString(), " ", tfArray3[1].X.ToString(), " ", tfArray3[1].Y.ToString() } ;
										
                                        text5 = string.Concat(textArray5);
                                    }
                                    else
                                    {
                                        text5 = info4.PointString;
                                    }
                                }
                                else if (collection1.Contains(info4))
                                {
                                    PointF[] tfArray14 = new PointF[2] { info4.SecondControl, info4.MiddlePoint } ;
                                    PointF[] tfArray4 = tfArray14;
                                    matrix1.TransformPoints(tfArray4);
                                    if (info4.IsStart)
                                    {
                                        text5 = "M " + tfArray4[1].X.ToString() + " " + tfArray4[1].Y.ToString();
                                        goto Label_1325;
                                    }
                                    if (info4.FirstControl.IsEmpty)
                                    {
                                        text5 = "L " + tfArray4[1].X.ToString() + " " + tfArray4[1].Y.ToString();
                                        goto Label_1325;
                                    }
                                    string[] textArray6 = new string[12] { "C ", info4.FirstControl.X.ToString(), " ", info4.FirstControl.Y.ToString(), " ", tfArray4[0].X.ToString(), " ", tfArray4[0].Y.ToString(), " ", tfArray4[1].X.ToString(), " ", tfArray4[1].Y.ToString() } ;
                                    text5 = string.Concat(textArray6);
                                }
                                else
                                {
                                    text5 = info4.PointString;
                                }
                            Label_1325:
                                if ((num11 < Math.Min((int) (num9 + 1), (int) (this.currentGraph.PointsInfo.Count - 1))) && info4.IsEnd)
                                {
                                    text5 = text5 + "Z";
                                }
                                text4 = text4 + text5;
                                if (num11 == Math.Min((int) (num9 + 1), (int) (this.currentGraph.PointsInfo.Count - 1)))
                                {
                                    text4 = text4 + info4.NextString;
                                }
                                if ((info4.NextControl == this.endtrend) && collection1.Contains(info4))
                                {
                                    PointF[] tfArray15 = new PointF[1] { this.endtrend } ;
                                    PointF[] tfArray5 = tfArray15;
                                    matrix1.TransformPoints(tfArray5);
                                    this.endtrend = tfArray5[0];
                                }
                                else if (info4.SecondControl == this.starttrend)
                                {
                                    PointF[] tfArray16 = new PointF[1] { this.starttrend } ;
                                    PointF[] tfArray6 = tfArray16;
                                    matrix1.TransformPoints(tfArray6);
                                    this.starttrend = tfArray6[0];
                                }
                            }
                            this.oldindex = this.currentGraph.PointsInfo.IndexOf(this.currentInfo);
                            PointF[] tfArray7 = new PointF[0];
                            if (this.currentinfo != null)
                            {
                                PointF[] tfArray17 = new PointF[2] { this.currentinfo.SecondControl, this.currentinfo.NextControl } ;
                                tfArray7 = tfArray17;
                            }
                            this.Update(text4, this.oldstr);
                            this.activePoints.Clear();
                            if ((this.oldindex >= 0) && (this.oldindex < this.currentGraph.PointsInfo.Count))
                            {
                                matrix1.TransformPoints(tfArray7);
                                this.currentInfo = this.currentGraph.PointsInfo[this.oldindex];
                            }
                        }
                        goto Label_3EC7;
                    }
                    this.activePoints.Clear();
                    int num4 = (int) Math.Min(this.startPoint.X, tf1.X);
                    int num5 = (int) Math.Min(this.startPoint.Y, tf1.Y);
                    int num6 = (int) Math.Max(this.startPoint.X, tf1.X);
                    int num7 = (int) Math.Max(this.startPoint.Y, tf1.Y);
                    RectangleF ef2 = new RectangleF((float) num4, (float) num5, (float) (num6 - num4), (float) (num7 - num5));
                    ArrayList list1 = new ArrayList(0x10);
                    PointInfoCollection.PointInfoEnumerator enumerator1 = this.currentGraph.PointsInfo.GetEnumerator();
                    while (enumerator1.MoveNext())
                    {
                        PointInfo info3 = enumerator1.Current;
                        PointF tf8 = info3.MiddlePoint;
                        if (ef2.Contains(tf8))
                        {
                            list1.Add(this.currentGraph.PointsInfo.IndexOf(info3));
                            this.activePoints.Add(info3);
                        }
                    }
                    this.activeindex = new int[list1.Count];
                    list1.CopyTo(this.activeindex, 0);
                    goto Label_3EC7;
                }
                case BezierOperate.ConvertAnchor:
                {
                    this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
                    this.win32.W32SetROP2(6);
                    this.win32.W32PolyDraw(this.reversePath);
                    this.win32.ReleaseDC();
                    this.activeindex = new int[this.activePoints.Count];
                    int num21 = 0;
                    PointInfoCollection.PointInfoEnumerator enumerator3 = this.activePoints.GetEnumerator();
                    while (enumerator3.MoveNext())
                    {
                        PointInfo info8 = enumerator3.Current;
                        this.activeindex[num21] = this.activePoints.IndexOf(info8);
                        num21++;
                    }
                    if (this.currentInfo == null)
                    {
                        goto Label_3EC7;
                    }
                    if ((Math.Abs((float) (tf2.X - this.currentinfo.MiddlePoint.X)) < 2f) && (Math.Abs((float) (tf2.Y - this.currentinfo.MiddlePoint.Y)) < 2f))
                    {
                        tf1 = this.currentinfo.MiddlePoint;
                    }
                    PointF tf9 = new PointF((2f * this.currentInfo.MiddlePoint.X) - tf1.X, (2f * this.currentInfo.MiddlePoint.Y) - tf1.Y);
                    if (this.currentinfo.NextControl == this.endtrend)
                    {
                        this.endtrend = tf1;
                    }
                    text7 = string.Empty;
                    text7 = this.currentInfo.PreString;
                    if (this.currentInfo.IsStart)
                    {
                        if ((this.currentinfo.PreInfo != null) && this.currentinfo.PreInfo.IsEnd)
                        {
                            int num22 = this.currentGraph.PointsInfo.IndexOf(this.currentinfo);
                            int num23 = this.currentGraph.PointsInfo.IndexOf(this.currentinfo.PreInfo);
                            for (int num24 = num22; num24 <= num23; num24++)
                            {
                                PointInfo info9 = this.currentGraph.PointsInfo[num24];
                                if (info9 == this.currentinfo.NextInfo)
                                {
                                    PointF tf10 = info9.SecondControl;
                                    tf10 = tf10.IsEmpty ? info9.MiddlePoint : tf10;
                                    string text18 = text7;
                                    string[] textArray14 = new string[13] { text18, "C ", tf1.X.ToString(), " ", tf1.Y.ToString(), " ", tf10.X.ToString(), " ", tf10.Y.ToString(), " ", info9.MiddlePoint.X.ToString(), " ", info9.MiddlePoint.Y.ToString() } ;
                                    text7 = string.Concat(textArray14);
                                }
                                else if ((info9 == this.currentinfo.PreInfo) && info9.IsEnd)
                                {
                                    if (info9.MiddlePoint == this.currentinfo.MiddlePoint)
                                    {
                                        PointF tf11 = info9.FirstControl;
                                        tf11 = tf11.IsEmpty ? info9.MiddlePoint : tf11;
                                        string text19 = text7;
                                        string[] textArray15 = new string[13] { text19, "C ", tf11.X.ToString(), " ", tf11.Y.ToString(), " ", tf9.X.ToString(), " ", tf9.Y.ToString(), " ", info9.MiddlePoint.X.ToString(), " ", info9.MiddlePoint.Y.ToString() } ;
                                        text7 = string.Concat(textArray15);
                                        text7 = text7 + "Z";
                                        goto Label_2E1B;
                                    }
                                    text7 = text7 + info9.PointString;
                                    string text20 = text7;
                                    string[] textArray16 = new string[13] { text20, "C ", info9.MiddlePoint.X.ToString(), " ", info9.MiddlePoint.Y.ToString(), " ", tf9.X.ToString(), " ", tf9.Y.ToString(), " ", this.currentinfo.MiddlePoint.X.ToString(), " ", this.currentinfo.MiddlePoint.Y.ToString() } ;
                                    text7 = string.Concat(textArray16);
                                    text7 = text7 + "Z";
                                }
                                else
                                {
                                    text7 = text7 + info9.PointString;
                                }
                            Label_2E1B:;
                            }
                            text7 = text7 + this.currentinfo.PreInfo.NextString;
                        }
                        this.starttrend = tf9;
                    }
                    else
                    {
                        PointF tf12 = this.currentinfo.FirstControl.IsEmpty ? this.currentinfo.PreInfo.MiddlePoint : this.currentinfo.FirstControl;
                        string text21 = text7;
                        string[] textArray17 = new string[13] { text21, "C ", tf12.X.ToString(), " ", tf12.Y.ToString(), " ", tf9.X.ToString(), " ", tf9.Y.ToString(), " ", this.currentInfo.MiddlePoint.X.ToString(), " ", this.currentInfo.MiddlePoint.Y.ToString() } ;
                        text7 = string.Concat(textArray17);
                        if (this.currentInfo.NextInfo != null)
                        {
                            if (this.currentinfo.IsEnd && this.currentinfo.FirstControl.IsEmpty)
                            {
                                PointF tf13 = this.currentinfo.NextInfo.SecondControl.IsEmpty ? this.currentinfo.NextInfo.MiddlePoint : this.currentinfo.NextInfo.SecondControl;
                                string text22 = text7;
                                string[] textArray18 = new string[13] { text22, "C ", tf1.X.ToString(), " ", tf1.Y.ToString(), " ", tf13.X.ToString(), " ", tf13.Y.ToString(), " ", this.currentinfo.NextInfo.MiddlePoint.X.ToString(), " ", this.currentinfo.NextInfo.MiddlePoint.Y.ToString() } ;
                                text7 = string.Concat(textArray18);
                                text7 = text7 + this.currentinfo.NextString;
                                break;
                            }
                            PointF tf14 = this.currentinfo.NextInfo.SecondControl.IsEmpty ? this.currentinfo.NextInfo.MiddlePoint : this.currentinfo.NextInfo.SecondControl;
                            string text23 = text7;
                            string[] textArray19 = new string[13] { text23, "C ", tf1.X.ToString(), " ", tf1.Y.ToString(), " ", tf14.X.ToString(), " ", tf14.Y.ToString(), " ", this.currentinfo.NextInfo.MiddlePoint.X.ToString(), " ", this.currentInfo.NextInfo.MiddlePoint.Y.ToString() } ;
                            text7 = string.Concat(textArray19);
                            text7 = text7 + this.currentInfo.NextInfo.NextString;
                        }
                        else
                        {
                            text7 = text7 + this.currentInfo.NextString;
                        }
                    }
                    break;
                }
                case BezierOperate.ChangeEndAnchor:
                {
                    PointF tf17;
                    if (this.preInfo == null)
                    {
                        goto Label_3EC7;
                    }
                    if ((((Control.ModifierKeys & Keys.Alt) != Keys.Alt) && (this.preInfo.MiddlePoint != this.preInfo.SecondControl)) && !this.preInfo.FirstControl.IsEmpty)
                    {
                        string text30 = this.preInfo.PreString;
                        string[] textArray26 = new string[13] { text30, "C ", this.preInfo.FirstControl.X.ToString(), " ", this.preInfo.FirstControl.Y.ToString(), " ", tf1.X.ToString(), " ", tf1.Y.ToString(), " ", this.preInfo.MiddlePoint.X.ToString(), " ", this.preInfo.MiddlePoint.Y.ToString() } ;
                        string text10 = string.Concat(textArray26);
                        text10 = text10 + this.preInfo.NextString;
                        this.Update(text10, this.oldstr);
                        goto Label_3EC5;
                    }
                    this.endtrend = tf17 = tf1;
                    this.preInfo.NextControl = tf17;
                    this.oldindex = this.currentGraph.PointsInfo.IndexOf(this.preInfo);
                    int[] numArray3 = new int[1] { this.oldindex } ;
                    this.activeindex = numArray3;
                    goto Label_3EC5;
                }
                case BezierOperate.MoveControl:
                {
                    if (this.moveinfo == null)
                    {
                        return;
                    }
                    this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
                    this.win32.W32SetROP2(6);
                    this.win32.W32PolyDraw(this.reversePath);
                    this.win32.ReleaseDC();
                    this.activeindex = new int[this.activePoints.Count];
                    int num12 = 0;
                    PointInfoCollection.PointInfoEnumerator enumerator2 = this.activePoints.GetEnumerator();
                    while (enumerator2.MoveNext())
                    {
                        PointInfo info5 = enumerator2.Current;
                        this.activeindex[num12] = this.currentGraph.PointsInfo.IndexOf(info5);
                        num12++;
                    }
                    this.oldindex = this.currentGraph.PointsInfo.IndexOf(this.currentinfo);
                    string text6 = string.Empty;
                    bool flag2 = false;
                    bool flag3 = false;
                    text6 = this.moveinfo.PreString;
                    if (this.movePoint == this.moveinfo.SecondControl)
                    {
                        flag3 = true;
                        bool flag4 = false;
                        if (((this.moveinfo.PreInfo != null) && this.moveinfo.IsStart) && this.moveinfo.PreInfo.IsEnd)
                        {
                            flag4 = true;
                            int num13 = this.currentGraph.PointsInfo.IndexOf(this.moveinfo);
                            int num14 = this.currentGraph.PointsInfo.IndexOf(this.moveinfo.PreInfo);
                            for (int num15 = num13; num15 <= num14; num15++)
                            {
                                PointInfo info6 = this.currentGraph.PointsInfo[num15];
                                if (((info6 == this.moveinfo.NextInfo) && ((Control.ModifierKeys & Keys.Alt) != Keys.Alt)) && !this.moveinfo.Steep)
                                {
                                    float single5 = (float) Math.Sqrt(Math.Pow((double) (this.moveinfo.SecondControl.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (this.moveinfo.SecondControl.Y - this.moveinfo.MiddlePoint.Y), 2));
                                    float single6 = (float) Math.Sqrt(Math.Pow((double) (tf1.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (tf1.Y - this.moveinfo.MiddlePoint.Y), 2));
                                    if (single5 == 0f)
                                    {
                                        single5 += 0.0001f;
                                    }
                                    float single7 = single6 / single5;
                                    float single8 = this.moveinfo.MiddlePoint.X + (single7 * (this.moveinfo.MiddlePoint.X - tf1.X));
                                    float single9 = this.moveinfo.MiddlePoint.Y + (single7 * (this.moveinfo.MiddlePoint.Y - tf1.Y));
                                    string text11 = text6;
                                    string[] textArray7 = new string[13] { text11, "C ", single8.ToString(), " ", single9.ToString(), " ", info6.SecondControl.X.ToString(), " ", info6.SecondControl.Y.ToString(), " ", info6.MiddlePoint.X.ToString(), " ", info6.MiddlePoint.Y.ToString() } ;
                                    text6 = string.Concat(textArray7);
                                }
                                else if ((info6 == this.moveinfo.PreInfo) && info6.IsEnd)
                                {
                                    object obj1 = text6;
                                    object[] objArray1 = new object[13] { obj1, "C ", info6.FirstControl.X.ToString(), " ", info6.FirstControl.Y.ToString(), " ", tf1.X, " ", tf1.Y.ToString(), " ", info6.MiddlePoint.X.ToString(), " ", info6.MiddlePoint.Y.ToString() } ;
                                    text6 = string.Concat(objArray1);
                                }
                                else
                                {
                                    text6 = text6 + info6.PointString;
                                }
                            }
                            text6 = text6 + this.moveinfo.PreInfo.NextString;
                        }
                        if (!flag4)
                        {
                            if (this.moveinfo.IsStart)
                            {
                                text6 = text6 + this.moveinfo.PointString;
                            }
                            else//next
                            {
                                object obj2 = text6;
                                object[] objArray2 ;
								if(this.moveinfo.Command=="A")
								{
									float float1=matrix1.OffsetY+this.moveinfo.Ry;
									if(float1>4)
									{
										this.moveinfo.Ry=float1;
									}
									objArray2= new object[15] { obj2, "A ", this.moveinfo.Rx.ToString(), " ", this.moveinfo.Ry.ToString(), " ", this.moveinfo.Angle.ToString(), " ", this.moveinfo.LargeArcFlage.ToString(), " ", this.moveinfo.SweepFlage.ToString(), " ", this.moveinfo.MiddlePoint.X.ToString(), " ", this.moveinfo.MiddlePoint.Y.ToString() } ;
								}
								else
								{
									objArray2= new object[13] { obj2, "C ", this.moveinfo.FirstControl.X.ToString(), " ", this.moveinfo.FirstControl.Y.ToString(), " ", tf1.X, " ", tf1.Y.ToString(), " ", this.moveinfo.MiddlePoint.X.ToString(), " ", this.moveinfo.MiddlePoint.Y.ToString() } ;
								}
                                text6 = string.Concat(objArray2);
                            }
                            bool flag5 = true;
                            if (((this.moveinfo.NextInfo != null) && ((Control.ModifierKeys & Keys.Alt) != Keys.Alt)) && (!this.moveinfo.Steep && !this.moveinfo.NextInfo.FirstControl.IsEmpty))
                            {
                                float single10 = (float) Math.Sqrt(Math.Pow((double) (this.moveinfo.SecondControl.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (this.moveinfo.SecondControl.Y - this.moveinfo.MiddlePoint.Y), 2));
                                float single11 = (float) Math.Sqrt(Math.Pow((double) (tf1.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (tf1.Y - this.moveinfo.MiddlePoint.Y), 2));
                                if (single10 == 0f)
                                {
                                    single10 += 0.0001f;
                                }
                                float single12 = single11 / single10;
                                float single13 = this.moveinfo.MiddlePoint.X + (single12 * (this.moveinfo.MiddlePoint.X - tf1.X));
                                float single14 = this.moveinfo.MiddlePoint.Y + (single12 * (this.moveinfo.MiddlePoint.Y - tf1.Y));
                                flag5 = false;
                                string text12 = text6;
                                string[] textArray8 = new string[13] { text12, "C ", single13.ToString(), " ", single14.ToString(), " ", this.moveinfo.NextInfo.SecondControl.X.ToString(), " ", this.moveinfo.NextInfo.SecondControl.Y.ToString(), " ", this.moveinfo.NextInfo.MiddlePoint.X.ToString(), " ", this.moveinfo.NextInfo.MiddlePoint.Y.ToString() } ;
                                text6 = string.Concat(textArray8);
                                text6 = text6 + this.moveinfo.NextInfo.NextString;
                            }
                            if (flag5)
                            {
                                text6 = text6 + this.moveinfo.NextString;
                            }
                        }
                    }
                    else if (this.movePoint == this.moveinfo.NextControl)
                    {
                        bool flag6 = true;
                        flag2 = true;
                        bool flag7 = false;
                        if (((this.moveinfo.PreInfo != null) && this.moveinfo.IsStart) && this.moveinfo.PreInfo.IsEnd)
                        {
                            flag7 = true;
                            int num16 = this.currentGraph.PointsInfo.IndexOf(this.moveinfo);
                            int num17 = this.currentGraph.PointsInfo.IndexOf(this.moveinfo.PreInfo);
                            for (int num18 = num16; num18 <= num17; num18++)
                            {
                                PointInfo info7 = this.currentGraph.PointsInfo[num18];
                                if (info7 == this.moveinfo.NextInfo)
                                {
                                    string text13 = text6;
                                    string[] textArray9 = new string[13] { text13, "C ", tf1.X.ToString(), " ", tf1.Y.ToString(), " ", info7.SecondControl.X.ToString(), " ", info7.SecondControl.Y.ToString(), " ", info7.MiddlePoint.X.ToString(), " ", info7.MiddlePoint.Y.ToString() } ;
                                    text6 = string.Concat(textArray9);
                                }
                                else if (((info7 == this.moveinfo.PreInfo) && info7.IsEnd) && (((Control.ModifierKeys & Keys.Alt) != Keys.Alt) && !this.moveinfo.Steep))
                                {
                                    float single15 = (float) Math.Sqrt(Math.Pow((double) (this.moveinfo.NextControl.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (this.moveinfo.NextControl.Y - this.moveinfo.MiddlePoint.Y), 2));
                                    float single16 = (float) Math.Sqrt(Math.Pow((double) (tf1.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (tf1.Y - this.moveinfo.MiddlePoint.Y), 2));
                                    if (single15 == 0f)
                                    {
                                        single15 += 0.0001f;
                                    }
                                    float single17 = single16 / single15;
                                    float single18 = this.moveinfo.MiddlePoint.X + (single17 * (this.moveinfo.MiddlePoint.X - tf1.X));
                                    float single19 = this.moveinfo.MiddlePoint.Y + (single17 * (this.moveinfo.MiddlePoint.Y - tf1.Y));
                                    string text14 = text6;
                                    string[] textArray10 = new string[13] { text14, "C ", info7.FirstControl.X.ToString(), " ", info7.FirstControl.Y.ToString(), " ", single18.ToString(), " ", single19.ToString(), " ", info7.MiddlePoint.X.ToString(), " ", info7.MiddlePoint.Y.ToString() } ;
                                    text6 = string.Concat(textArray10);
                                    this.starttrend = new PointF(single18, single19);
                                }
                                else
                                {
                                    text6 = text6 + info7.PointString;
                                }
                            }
                            text6 = text6 + this.moveinfo.PreInfo.NextString;
                        }
                        if (!flag7)
                        {
                            if (((this.moveinfo.PreInfo != null) && ((Control.ModifierKeys & Keys.Alt) != Keys.Alt)) && (!this.moveinfo.Steep && !this.moveinfo.SecondControl.IsEmpty))
                            {
                                float single20 = (float) Math.Sqrt(Math.Pow((double) (this.moveinfo.NextControl.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (this.moveinfo.NextControl.Y - this.moveinfo.MiddlePoint.Y), 2));
                                float single21 = (float) Math.Sqrt(Math.Pow((double) (tf1.X - this.moveinfo.MiddlePoint.X), 2) + Math.Pow((double) (tf1.Y - this.moveinfo.MiddlePoint.Y), 2));
                                if (single20 == 0f)
                                {
                                    single20 += 0.0001f;
                                }
                                float single22 = single21 / single20;
                                float single23 = this.moveinfo.MiddlePoint.X + (single22 * (this.moveinfo.MiddlePoint.X - tf1.X));
                                float single24 = this.moveinfo.MiddlePoint.Y + (single22 * (this.moveinfo.MiddlePoint.Y - tf1.Y));
                                string text15 = this.moveinfo.PreString;
                                string[] textArray11 = new string[13] { text15, "C ", this.moveinfo.FirstControl.X.ToString(), " ", this.moveinfo.FirstControl.Y.ToString(), " ", single23.ToString(), " ", single24.ToString(), " ", this.moveinfo.MiddlePoint.X.ToString(), " ", this.moveinfo.MiddlePoint.Y.ToString() } ;
                                text6 = string.Concat(textArray11);
                                flag6 = false;
                            }
							if (this.moveinfo.NextInfo != null)
							{
								if (flag6)
								{
									string text16 = this.moveinfo.NextInfo.PreString;
									string[] textArray12 ;
									if(this.moveinfo.Command=="A")
									{
										float float1=matrix1.OffsetX+this.moveinfo.Rx;
										if(float1>4)
										{
											this.moveinfo.Rx=float1;
										}
										textArray12= new string[14] {"A ", this.moveinfo.Rx.ToString(), " ", this.moveinfo.Ry.ToString(), " ", this.moveinfo.Angle.ToString(), " ", this.moveinfo.LargeArcFlage.ToString(), " ", this.moveinfo.SweepFlage.ToString(), " ", this.moveinfo.MiddlePoint.X.ToString(), " ", this.moveinfo.MiddlePoint.Y.ToString() } ;
										text6 = this.moveinfo.PreString+string.Concat(textArray12)+this.moveinfo.NextString;
									}
									else
									{
										textArray12= new string[13] { text16, "C ", tf1.X.ToString(), " ", tf1.Y.ToString(), " ", this.moveinfo.NextInfo.SecondControl.X.ToString(), " ", this.moveinfo.NextInfo.SecondControl.Y.ToString(), " ", this.moveinfo.NextInfo.MiddlePoint.X.ToString(), " ", this.moveinfo.NextInfo.MiddlePoint.Y.ToString() } ;
										text6 = string.Concat(textArray12);
										text6 = text6 + this.moveinfo.NextInfo.NextString;
									}                                        
								}
								else
								{
									string text17 = text6;
									string[] textArray13 = new string[13] { text17, "C ", tf1.X.ToString(), " ", tf1.Y.ToString(), " ", this.moveinfo.NextInfo.SecondControl.X.ToString(), " ", this.moveinfo.NextInfo.SecondControl.Y.ToString(), " ", this.moveinfo.NextInfo.MiddlePoint.X.ToString(), " ", this.moveinfo.NextInfo.MiddlePoint.Y.ToString() } ;
									text6 = string.Concat(textArray13);
									text6 = text6 + this.moveinfo.NextInfo.NextString;
								}
							}
							else
							{
								if(this.moveinfo.Command=="A")
								{
									float float1=matrix1.OffsetX+this.moveinfo.Rx;
									if(float1>4)
									{
										this.moveinfo.Rx=float1;
									}
									string[] textArray12= new string[]{ "A ", this.moveinfo.Rx.ToString(), " ", this.moveinfo.Ry.ToString(), " ", this.moveinfo.Angle.ToString(), " ", this.moveinfo.LargeArcFlage.ToString(), " ", this.moveinfo.SweepFlage.ToString(), " ", this.moveinfo.MiddlePoint.X.ToString(), " ", this.moveinfo.MiddlePoint.Y.ToString() } ;
									text6 = this.moveinfo.PreString+string.Concat(textArray12)+this.moveinfo.NextString;
								}
							}
                        }
                    }
                    if (text6 == this.moveinfo.PreString)
                    {
                        text6 = this.moveinfo.PreString + this.moveinfo.PointString + this.moveinfo.NextString;
                    }
                    if (this.movePoint == this.endtrend)
                    {
                        this.endtrend = tf1;
                    }
                    else if (this.movePoint == this.starttrend)
                    {
                        this.starttrend = tf1;
                    }
                    int num19 = this.currentGraph.PointsInfo.IndexOf(this.currentInfo);
                    int num20 = this.currentGraph.PointsInfo.IndexOf(this.moveinfo);
                    if (text6.Length > 0)
                    {
                        this.Update(text6, this.oldstr);
                    }
                    if ((num20 >= 0) && (num20 < this.currentGraph.PointsInfo.Count))
                    {
                        if (flag2)
                        {
                            this.currentGraph.PointsInfo[num20].NextControl = tf1;
                        }
                        else if (flag3)
                        {
                            this.currentGraph.PointsInfo[num20].SecondControl = tf1;
                        }
                    }
                    this.mouseAreaControl.SVGDocument.NotifyUndo();
                    this.mouseAreaControl.SVGDocument.AcceptChanges = flag1;
                    goto Label_3EC7;
                }
                case BezierOperate.MovePath:
                {
                    this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
                    this.win32.W32SetROP2(6);
                    this.win32.W32PolyDraw(this.reversePath);
                    this.win32.ReleaseDC();
                    PointF[] tfArray18 = new PointF[2] { this.starttrend, this.endtrend } ;
                    PointF[] tfArray8 = tfArray18;
                    matrix1.TransformPoints(tfArray8);
                    this.starttrend = tfArray8[0];
                    this.endtrend = tfArray8[1];
                    string text8 = string.Empty;
                    PointInfoCollection.PointInfoEnumerator enumerator4 = this.currentGraph.PointsInfo.GetEnumerator();
                    while (enumerator4.MoveNext())
                    {
                        PointInfo info10 = enumerator4.Current;
                        PointF[] tfArray19 = new PointF[3] { info10.FirstControl, info10.SecondControl, info10.MiddlePoint } ;
                        tfArray8 = tfArray19;
                        matrix1.TransformPoints(tfArray8);
						if(info10.Command=="A")
						{
							text8 = string.Concat(new string[]{text8,"A ",info10.Rx.ToString()," ",info10.Ry.ToString()," ",info10.Angle.ToString()," ",info10.LargeArcFlage.ToString()," ",info10.SweepFlage.ToString()," ",tfArray8[2].X.ToString(), " ", tfArray8[2].Y.ToString()});
						}
						else if(info10.IsStart)
						{
                            string text24 = text8;
                            string[] textArray20 = new string[5] { text24, "M ", tfArray8[2].X.ToString(), " ", tfArray8[2].Y.ToString() } ;
                            text8 = string.Concat(textArray20);
                        }
                        else if (info10.FirstControl.IsEmpty)
                        {
                            string text25 = text8;
                            string[] textArray21 = new string[5] { text25, "L ", tfArray8[2].X.ToString(), " ", tfArray8[2].Y.ToString() } ;
                            text8 = string.Concat(textArray21);
                        }
                        else
                        {
                            string text26 = text8;
                            string[] textArray22 = new string[13] { text26, "C ", tfArray8[0].X.ToString(), " ", tfArray8[0].Y.ToString(), " ", tfArray8[1].X.ToString(), " ", tfArray8[1].Y.ToString(), " ", tfArray8[2].X.ToString(), " ", tfArray8[2].Y.ToString() } ;
                            text8 = string.Concat(textArray22);
                        }
                        if (info10.IsEnd)
                        {
                            text8 = text8 + "Z";
                        }
                    }
                    if (text8.Length > 0)
                    {
                        this.Update(text8, this.oldstr);
                    }
                    goto Label_3EC7;
                }
                case BezierOperate.CenterPoint:
                {
                    if ((this.currentGraph is MotionAnimate) && (((MotionAnimate) this.currentGraph).RefElement is IGraph))
                    {
                        IGraph graph2 = (IGraph) ((MotionAnimate) this.currentGraph).RefElement;
                        Matrix matrix2 = graph2.GraphTransform.Matrix.Clone();
                        matrix2.Invert();
                        PointF tf15 = this.centerPoint;
                        PointF tf16 = this.centerPoint;
                        PointF[] tfArray20 = new PointF[1] { this.mouseAreaControl.CenterPoint } ;
                        PointF[] tfArray9 = tfArray20;
                        matrix2.TransformPoints(tfArray9);
                        tf16 = tfArray9[0];
                        PointF[] tfArray21 = new PointF[1] { tf1 } ;
                        tfArray9 = tfArray21;
                        this.revertMatrix.TransformPoints(tfArray9);
                        this.mouseAreaControl.CenterPoint = tfArray9[0];
                        PointF[] tfArray22 = new PointF[1] { this.mouseAreaControl.CenterPoint } ;
                        tfArray9 = tfArray22;
                        matrix2.TransformPoints(tfArray9);
                        this.centerPoint = this.PointToView(this.mouseAreaControl.CenterPoint);
                        Matrix matrix3 = new Matrix();
                        matrix3.Translate(tfArray9[0].X - tf16.X, tfArray9[0].Y - tf16.Y);
                        PointF[] tfArray23 = new PointF[3] { this.starttrend, this.endtrend, this.centerPoint } ;
                        PointF[] tfArray10 = tfArray23;
                        matrix3.TransformPoints(tfArray10);
                        this.starttrend = tfArray10[0];
                        this.endtrend = tfArray10[1];
                        string text9 = string.Empty;
                        PointInfoCollection.PointInfoEnumerator enumerator5 = this.currentGraph.PointsInfo.GetEnumerator();
                        while (enumerator5.MoveNext())
                        {
                            PointInfo info11 = enumerator5.Current;
                            PointF[] tfArray24 = new PointF[3] { info11.FirstControl, info11.SecondControl, info11.MiddlePoint } ;
                            tfArray10 = tfArray24;
                            matrix3.TransformPoints(tfArray10);
                            if (info11.IsStart)
                            {
                                string text27 = text9;
                                string[] textArray23 = new string[5] { text27, "M ", tfArray10[2].X.ToString(), " ", tfArray10[2].Y.ToString() } ;
                                text9 = string.Concat(textArray23);
                            }
                            else if (info11.FirstControl.IsEmpty)
                            {
                                string text28 = text9;
                                string[] textArray24 = new string[5] { text28, "L ", tfArray10[2].X.ToString(), " ", tfArray10[2].Y.ToString() } ;
                                text9 = string.Concat(textArray24);
                            }
                            else
                            {
                                string text29 = text9;
                                string[] textArray25 = new string[13] { text29, "C ", tfArray10[0].X.ToString(), " ", tfArray10[0].Y.ToString(), " ", tfArray10[1].X.ToString(), " ", tfArray10[1].Y.ToString(), " ", tfArray10[2].X.ToString(), " ", tfArray10[2].Y.ToString() } ;
                                text9 = string.Concat(textArray25);
                            }
                            if (info11.IsEnd)
                            {
                                text9 = text9 + "Z";
                            }
                        }
                        Matrix matrix4 = graph2.Transform.Matrix.Clone();
                        Matrix matrix5 = graph2.GraphTransform.Matrix.Clone();
                        matrix4.Invert();
                        matrix5.Multiply(matrix4);
                        Matrix matrix6 = matrix5.Clone();
                        Matrix matrix7 = AnimFunc.GetMatrixForTime(graph2, this.mouseAreaControl.SVGDocument.ControlTime, ((MotionAnimate) this.currentGraph).Begin);
                        matrix5.Translate(tf15.X, tf15.Y);
                        matrix5.Multiply(matrix7, MatrixOrder.Prepend);
                        if (text9.Length > 0)
                        {
                            this.Update(text9, this.oldstr);
                        }
                        matrix4 = AnimFunc.GetMatrixForTime(graph2, this.mouseAreaControl.SVGDocument.ControlTime, ((MotionAnimate) this.currentGraph).Begin);
                        matrix4.Invert();
                        matrix4.Multiply(matrix5, MatrixOrder.Append);
                        matrix6.Invert();
                        matrix4.Multiply(matrix6, MatrixOrder.Append);
                        this.centerPoint = new PointF(matrix4.OffsetX, matrix4.OffsetY);
                    }
                    goto Label_3EC7;
                }
                default:
                {
                    goto Label_3EC7;
                }
            }
            if (text7.Length > 0)
            {
                this.oldindex = this.currentGraph.PointsInfo.IndexOf(this.currentInfo);
                this.Update(text7, this.oldstr);
            }
            goto Label_3EC7;
        Label_3EC5:;
        Label_3EC7:
            this.mouseAreaControl.SVGDocument.AcceptChanges = flag1;
            this.reversePath.Reset();
            this.mouseAreaControl.Invalidate();
        }

        public void OnPaint(PaintEventArgs e)
        {
            if (this.currentGraph != null)
            {
                if (this.currentGraph is IGraph)
                {
                    this.revertMatrix = ((IGraph) this.currentGraph).GraphTransform.Matrix.Clone();
                }
                else if (this.currentGraph is MotionAnimate)
                {
                    SvgElement element1 = ((MotionAnimate) this.currentGraph).RefElement;
                    if (element1 is IGraph)
                    {
                        Matrix matrix1 = ((IGraph) element1).Transform.Matrix.Clone();
                        this.revertMatrix = ((IGraph) element1).GraphTransform.Matrix.Clone();
                        matrix1.Invert();
                        this.revertMatrix.Multiply(matrix1);
                        Matrix matrix2 = new Matrix();
                        matrix2 = AnimFunc.GetMatrixForTime((IGraph) element1, this.mouseAreaControl.SVGDocument.ControlTime, ((MotionAnimate) this.currentGraph).Begin);
                        this.revertMatrix.Multiply(matrix2);
                        this.centerPoint = this.PointToView(this.mouseAreaControl.CenterPoint);
                        object obj1 = ((MotionAnimate) this.currentGraph).GetAnimateResult((float) this.mouseAreaControl.SVGDocument.ControlTime, DomType.SvgMatrix);
                        if (obj1 is Matrix)
                        {
                            Matrix matrix3 = (Matrix) obj1;
                            this.centerPoint.X -= matrix3.OffsetX;
                            this.centerPoint.Y -= matrix3.OffsetY;
                        }
                        this.revertMatrix.Translate(this.centerPoint.X, this.centerPoint.Y);
                    }
                }
                GraphicsContainer container1 = e.Graphics.BeginContainer();
                Matrix matrix4 = this.revertMatrix.Clone();
                e.Graphics.Transform = matrix4;
                e.Graphics.DrawPath(Pens.Blue, this.currentGraph.GPath);
                e.Graphics.EndContainer(container1);
                if (this.currentGraph is MotionAnimate)
                {
                    SvgElement element2 = ((MotionAnimate) this.currentGraph).RefElement;
                    if (element2 != null)
                    {
                        element2.pretime = -1;
                    }
                }
                if (this.currentGraph.PointsInfo.Count > 0)
                {
                    PointInfo info1 = this.currentGraph.PointsInfo[0];
                    this.oldstr = info1.PreString + info1.PointString + info1.NextString;
//					StringBuilder strb1=new StringBuilder();
//					foreach(PointInfo info1 in this.currentGraph.PointsInfo)
//					{
//						strb1.Append(info1.PointString);
//					}
//					this.oldstr=strb1.ToString();
					//
                }
                this.showcontrol = true;
                if (this.mouseAreaControl.SVGDocument.RecordAnim)
                {
                    if ((this.currentGraph != null) && !this.mouseAreaControl.SVGDocument.PlayAnim)
                    {
                        this.showcontrol = true;
                    }
                    else
                    {
                        this.showcontrol = false;
                    }
                }
                this.activePoints.Clear();
                int[] numArray1 = this.activeindex;
                for (int num3 = 0; num3 < numArray1.Length; num3++)
                {
                    int num1 = numArray1[num3];
                    if ((num1 >= 0) && (num1 < this.currentGraph.PointsInfo.Count))
                    {
                        this.activePoints.Add(this.currentGraph.PointsInfo[num1]);
                    }
                }
                if ((this.oldindex >= 0) && (this.oldindex < this.currentGraph.PointsInfo.Count))
                {
                    this.currentInfo = this.currentGraph.PointsInfo[this.oldindex];
                }
                else
                {
                    this.currentInfo = null;
                }
                if (this.showcontrol)
                {
                    if (this.currentGraph.PointsInfo.Contains(this.currentInfo))
                    {
                        if (!this.currentInfo.SecondControl.IsEmpty)
                        {
                            PointF[] tfArray1 = new PointF[3] { this.currentInfo.SecondControl, this.currentInfo.MiddlePoint, this.currentInfo.NextControl } ;
                            
                            matrix4.TransformPoints(tfArray1);
                            GraphicsPath path1 = new GraphicsPath();
                            path1.AddRectangle(new RectangleF(tfArray1[1].X - 3f, tfArray1[1].Y - 3f, 4f, 4f));
                            if (!this.currentInfo.SecondControl.IsEmpty)
                            {
                                path1.AddRectangle(new RectangleF(tfArray1[0].X - 3f, tfArray1[0].Y - 3f, 4f, 4f));
                                e.Graphics.DrawLine(Pens.Blue, tfArray1[0], tfArray1[1]);
                            }
                            if (!this.currentInfo.NextControl.IsEmpty)
                            {
                                e.Graphics.DrawLine(Pens.Blue, tfArray1[1], tfArray1[2]);
                                path1.AddRectangle(new RectangleF(tfArray1[2].X - 3f, tfArray1[2].Y - 2f, 4f, 4f));
                            }
                            e.Graphics.FillPath(Brushes.White, path1);
                            e.Graphics.DrawPath(Pens.Blue, path1);
                        }
						/*
                        if (this.currentGraph.PointsInfo.Contains(this.currentInfo.PreInfo) && !this.currentInfo.FirstControl.IsEmpty)
                        {
//                            PointF[] tfArray2 = new PointF[2] { this.currentInfo.PreInfo.MiddlePoint, this.currentInfo.FirstControl } ;
//                           
//                            matrix4.TransformPoints(tfArray2);
//                            RectangleF[] efArray1 = new RectangleF[2] { new RectangleF(tfArray2[0].X - 3f, tfArray2[0].Y - 3f, 4f, 4f), new RectangleF(tfArray2[1].X - 3f, tfArray2[1].Y - 3f, 4f, 4f) } ;
//                            e.Graphics.FillRectangles(Brushes.White, efArray1);
//                            RectangleF[] efArray2 = new RectangleF[2] { new RectangleF(tfArray2[0].X - 3f, tfArray2[0].Y - 3f, 4f, 4f), new RectangleF(tfArray2[1].X - 3f, tfArray2[1].Y - 3f, 4f, 4f) } ;
//                            e.Graphics.DrawRectangles(Pens.Blue, efArray2);
//                            e.Graphics.DrawLine(Pens.Blue, tfArray2[0], tfArray2[1]);
                        }
                        if (this.currentGraph.PointsInfo.Contains(this.currentInfo.NextInfo) && !this.currentInfo.NextInfo.SecondControl.IsEmpty)
                        {
//                            PointF[] tfArray3 = new PointF[2] { this.currentInfo.NextInfo.MiddlePoint, this.currentInfo.NextInfo.SecondControl } ;
//                            
//                            matrix4.TransformPoints(tfArray3);
//                            RectangleF[] efArray3 = new RectangleF[2] { new RectangleF(tfArray3[0].X - 3f, tfArray3[0].Y - 3f, 4f, 4f), new RectangleF(tfArray3[1].X - 3f, tfArray3[1].Y - 3f, 4f, 4f) } ;
//                            e.Graphics.FillRectangles(Brushes.White, efArray3);
//                            RectangleF[] efArray4 = new RectangleF[2] { new RectangleF(tfArray3[0].X - 3f, tfArray3[0].Y - 3f, 4f, 4f), new RectangleF(tfArray3[1].X - 3f, tfArray3[1].Y - 3f, 4f, 4f) } ;
//                            e.Graphics.DrawRectangles(Pens.Blue, efArray4);
//                            e.Graphics.DrawLine(Pens.Blue, tfArray3[0], tfArray3[1]);
                        }*/
                    }
                    this.SubpathList.Clear();
                    PointInfoCollection collection1 = new PointInfoCollection();
                    PointInfo info2 = null;
                    PointInfoCollection.PointInfoEnumerator enumerator1 = this.currentGraph.PointsInfo.GetEnumerator();
                    while (enumerator1.MoveNext())
                    {
                        PointInfo info3 = enumerator1.Current;
                        bool flag1 = info2 != null;
                        if (info2 == null)
                        {
                            info2 = info3;
                        }
                        else if (info2.SubPath != info3.SubPath)
                        {
                            this.SubpathList.Add(info2.SubPath, collection1);
                            collection1 = new PointInfoCollection();
                        }
                        info2 = info3;
                        collection1.Add(info3);
                        if ((this.currentGraph.PointsInfo.IndexOf(info3) == (this.currentGraph.PointsInfo.Count - 1)) && !this.SubpathList.ContainsValue(collection1))
                        {
                            this.SubpathList.Add(info2.SubPath, collection1);
                        }
                        PointF tf1 = info3.MiddlePoint;
                        PointF[] tfArray8 = new PointF[1] { tf1 } ;
                        PointF[] tfArray4 = tfArray8;
                        matrix4.TransformPoints(tfArray4);
                        tf1 = tfArray4[0];
                        if (this.activePoints.Contains(info3))
                        {
                            e.Graphics.FillRectangle(Brushes.Blue, (float) (tf1.X - 3f), (float) (tf1.Y - 3f), (float) 4f, (float) 4f);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(Brushes.White, (float) (tf1.X - 3f), (float) (tf1.Y - 3f), (float) 4f, (float) 4f);
                        }
                        e.Graphics.DrawRectangle(new Pen(ControlPaint.Light(Color.Blue)), (float) (tf1.X - 3f), (float) (tf1.Y - 3f), (float) 4f, (float) 4f);
                    }
                    if (this.currentinfo != null)
                    {
                        int num2 = this.currentinfo.SubPath;
                        if (this.SubpathList.ContainsKey(num2))
                        {
                            PointInfoCollection collection2 = (PointInfoCollection) this.SubpathList[num2];
                            if (collection2.Count > 0)
                            {
                                if (collection2[0].PreInfo == null)
                                {
                                    collection2[0].SecondControl = this.starttrend;
                                }
                                if (collection2[collection2.Count - 1].NextInfo == null)
                                {
                                    collection2[collection2.Count - 1].NextControl = this.endtrend;
                                }
                            }
                        }
                    }
                    if (!this.centerPoint.IsEmpty && !this.mouseAreaControl.SVGDocument.PlayAnim)
                    {
                        PointF tf2 = this.mouseAreaControl.CenterPoint;
                        e.Graphics.FillEllipse(Brushes.White, (float) (tf2.X - 4f), (float) (tf2.Y - 4f), (float) 8f, (float) 8f);
                        e.Graphics.DrawEllipse(new Pen(Color.Black, 0.5f), (float) (tf2.X - 4f), (float) (tf2.Y - 4f), (float) 8f, (float) 8f);
                        e.Graphics.DrawLine(new Pen(Color.Black, 0.5f), (float) (tf2.X - 2f), tf2.Y, (float) (tf2.X + 2f), tf2.Y);
                        e.Graphics.DrawLine(new Pen(Color.Black, 0.5f), tf2.X, (float) (tf2.Y - 2f), tf2.X, (float) (tf2.Y + 2f));
                    }
                }
            }
        }

        private PointF PointToView(PointF p)
        {
            PointF[] tfArray2 = new PointF[1] { p } ;
            PointF[] tfArray1 = tfArray2;
            Matrix matrix1 = this.revertMatrix.Clone();
            matrix1.Invert();
            matrix1.TransformPoints(tfArray1);
            return tfArray1[0];
        }

        public bool ProcessDialogKey(Keys keydate)
        {
            return false;
        }

        public bool Redo()
        {
            PointF tf1;
            PointInfo info1;
            if (this.currentGraph != null)
            {
                bool flag1 = this.mouseAreaControl.SVGDocument.AcceptChanges;
                this.mouseAreaControl.SVGDocument.AcceptChanges = false;
                if (this.currentGraph is IGraph)
                {
                    string text1 = ((SvgElement) this.currentGraph).GetAttribute("d").Trim();
                    if (text1.Length > 0)
                    {
                        AttributeFunc.SetAttributeValue((SvgElement) this.currentGraph, "d", text1);
                    }
                }
                this.mouseAreaControl.SVGDocument.AcceptChanges = flag1;
                ((SvgElement) this.currentGraph).pretime = -1;
            }
            this.activePoints.Clear();
            this.currentInfo = null;
            this.endtrend = tf1 = PointF.Empty;
            this.starttrend = tf1;
            this.preInfo = (PointInfo) (info1 = null);
            this.preDrawInfo = info1;
            return false;
        }

        public static PointF[] SplitBeizierAtT(PointF start, PointF control1, PointF control2, PointF end, float t)
        {
            PointF tf1 = PointF.Empty;
            PointF tf2 = PointF.Empty;
            PointF tf3 = PointF.Empty;
            PointF tf4 = PointF.Empty;
            PointF tf5 = PointF.Empty;
            PointF tf6 = PointF.Empty;
            tf1 = new PointF((start.X * (1f - t)) + (control1.X * t), (start.Y * (1f - t)) + (control1.Y * t));
            tf2 = new PointF((control1.X * (1f - t)) + (control2.X * t), (control2.Y * t) + (control1.Y * (1f - t)));
            tf3 = new PointF((end.X * t) + (control2.X * (1f - t)), (end.Y * t) + (control2.Y * (1f - t)));
            tf4 = new PointF((tf1.X * (1f - t)) + (tf2.X * t), (tf1.Y * (1f - t)) + (tf2.Y * t));
            tf5 = new PointF((tf2.X * (1f - t)) + (tf3.X * t), (tf2.Y * (1f - t)) + (tf3.Y * t));
            tf6 = new PointF((tf4.X * (1f - t)) + (tf5.X * t), (tf5.Y * t) + (tf4.Y * (1f - t)));
            return new PointF[5] { tf1, tf4, tf6, tf5, tf3 } ;
        }

        public static PointF[] SplitBezierAtPoint(PointF start, PointF control1, PointF control2, PointF end, PointF point, out float t)
        {
            Pen pen1 = new Pen(Color.Blue, 2f);
            pen1.Alignment = PenAlignment.Center;
            GraphicsPath path1 = new GraphicsPath();
            path1.AddBezier(start, control1, control2, end);
            if (!path1.IsOutlineVisible(point, pen1))
            {
                t = -1f;
                return null;
            }
            PointF tf1 = PointF.Empty;
            PointF tf2 = PointF.Empty;
            PointF tf3 = PointF.Empty;
            PointF tf4 = PointF.Empty;
            PointF tf5 = PointF.Empty;
            t = 0.5f;
            PointF[] tfArray1 = BezierOperation.SplitBeizierAtT(start, control1, control2, end, t);
            PointF tf6 = tfArray1[2];
            for (float single1 = (float) Math.Sqrt(Math.Pow((double) (tf6.X - point.X), 2) + Math.Pow((double) (tf6.Y - point.Y), 2)); single1 > 3f; single1 = (float) Math.Sqrt(Math.Pow((double) (tf6.X - point.X), 2) + Math.Pow((double) (tf6.Y - point.Y), 2)))
            {
                path1 = new GraphicsPath();
                path1.AddBezier(start, tfArray1[0], tfArray1[1], tfArray1[2]);
                if (path1.IsOutlineVisible(point, pen1))
                {
                    t /= 2f;
                }
                else
                {
                    t += (t / 2f);
                }
                tfArray1 = BezierOperation.SplitBeizierAtT(start, control1, control2, end, t);
                tf6 = tfArray1[2];
            }
            t = t;
            return tfArray1;
        }

        public bool Undo()
        {
            PointF tf1;
            PointInfo info1;
            if (this.currentGraph != null)
            {
                bool flag1 = this.mouseAreaControl.SVGDocument.AcceptChanges;
                this.mouseAreaControl.SVGDocument.AcceptChanges = false;
                if (this.currentGraph is IGraph)
                {
                    string text1 = ((SvgElement) this.currentGraph).GetAttribute("d").Trim();
                    if (text1.Length > 0)
                    {
                        AttributeFunc.SetAttributeValue((SvgElement) this.currentGraph, "d", text1);
                    }
                }
                this.mouseAreaControl.SVGDocument.AcceptChanges = flag1;
                ((SvgElement) this.currentGraph).pretime = -1;
            }
            this.activePoints.Clear();
            this.currentInfo = null;
            this.endtrend = tf1 = PointF.Empty;
            this.starttrend = tf1;
            this.preInfo = (PointInfo) (info1 = null);
            this.preDrawInfo = info1;
            return false;
        }

        private void Update(string newpath, string oldpath)
        {
            if ((newpath.Trim() != oldpath.Trim()) && (this.currentGraph != null))
            {
                //bool flag1 = true;
                SvgDocument document1 = this.mouseAreaControl.SVGDocument;
                bool flag2 = document1.AcceptChanges;
                document1.AcceptChanges = true;
                document1.NumberOfUndoOperations = 200;
                if ((!document1.RecordAnim || ((((SvgElement) this.currentGraph).InfoList.Count == 1) && (document1.ControlTime == 0))) || ((this.currentGraph is MotionAnimate) || (((SvgElement) this.currentGraph).ParentNode == null)))
                {
                    AttributeFunc.SetAttributeValue((SvgElement) this.currentGraph, this.attributename, newpath);
                    //flag1 = false;
                }
//                if (this.currentGraph is MotionAnimate)
//                {
//                    SvgElement element1 = ((MotionAnimate) this.currentGraph).RefElement;
//                    if (element1 != null)
//                    {
//                        element1.pretime = -1;
//                    }
//                }
//                if (flag1)
//                {
//                    SvgElement element2 = (SvgElement) this.currentGraph;
//                    string text1 = this.attributename;
//                    int num1 = AnimFunc.GetKeyIndex(element2, document1.ControlTime, true);
//                    if (num1 == -1)
//                    {
//                        element2.InsertKey(document1.ControlTime, (byte) 0);
//                    }
//                    num1 = AnimFunc.GetKeyIndex(element2, document1.ControlTime, true);
//                    int num2 = num1;
//                    int num3 = num1;
//                    if ((num1 - 1) >= 0)
//                    {
//                        num3 = num1 - 1;
//                    }
//                    string text2 = oldpath;
//                    string text3 = newpath;
//                    if ((num1 + 1) < element2.InfoList.Count)
//                    {
//                        num2 = num1 + 1;
//                    }
//                    KeyInfo info1 = (KeyInfo) element2.InfoList[num3];
//                    int num4 = info1.keytime;
//                    KeyInfo info2 = (KeyInfo) element2.InfoList[num2];
//                    int num5 = info2.keytime - num4;
//                    document1.AcceptChanges = false;
//                    XmlNode node1 = element2.SelectSingleNode("*[name()='animate' and @attributeName='d']");
//                    document1.AcceptChanges = true;
//                    if (node1 is ItopVector.Core.Animate.Animate)
//                    {
//                        ItopVector.Core.Animate.Animate animate1 = (ItopVector.Core.Animate.Animate) node1;
//                        int num6 = num4;
//                        int num7 = num5;
//                        int num8 = Math.Max((int) (num4 + num5), (int) (animate1.Begin + animate1.Duration));
//                        num4 = Math.Min(num4, animate1.Begin);
//                        if ((document1.ControlTime > animate1.Begin) && (document1.ControlTime <= (animate1.Begin + animate1.Duration)))
//                        {
//                            animate1.InsertKey(document1.ControlTime, text3);
//                        }
//                        else if (((num6 + num7) <= animate1.Begin) || (num6 >= (animate1.Begin + animate1.Duration)))
//                        {
//                            animate1.Begin = num4;
//                            animate1.Duration = num8 - num4;
//                            animate1.InsertKey(num6, text2);
//                            animate1.InsertKey(num6 + num7, text2);
//                            animate1.InsertKey(document1.ControlTime, text3);
//                        }
//                        else if (num6 <= animate1.Begin)
//                        {
//                            animate1.Begin = num4;
//                            animate1.Duration = num8 - num4;
//                            animate1.InsertKey(num4, text2);
//                            animate1.InsertKey(document1.ControlTime, text3);
//                        }
//                        else if ((num6 + num7) >= (animate1.Begin + animate1.Duration))
//                        {
//                            animate1.Begin = num4;
//                            animate1.Duration = num8 - num4;
//                            animate1.InsertKey(num6 + num7, text2);
//                            animate1.InsertKey(document1.ControlTime, text3);
//                        }
//                        element2.pretime = -1;
//                        animate1.pretime = -1;
//                    }
//                    else
//                    {
//                        string text4;
//                        document1.AcceptChanges = false;
//                        ItopVector.Core.Animate.Animate animate2 = (ItopVector.Core.Animate.Animate) document1.CreateElement(document1.Prefix, "animate", document1.NamespaceURI);
//                        animate2.AttributeName = text1;
//                        animate2.Begin = num4;
//                        animate2.Duration = num5;
//                        animate2.ToWhat = text4 = text2;
//                        animate2.FromWhat = text4;
//                        animate2.InsertKey(document1.ControlTime, text3);
//                        AttributeFunc.SetAttributeValue(animate2, "fill", "freeze");
//                        document1.AcceptChanges = true;
//                        element2.PreAppendAnim(animate2);
//                        element2.pretime = -1;
//                    }
//                    element2.InKey = true;
//                }
                document1.NotifyUndo();
                document1.AcceptChanges = flag2;
            }
        }


        // Properties
        public bool CanRedo
        {
            get
            {
                return false;
            }
        }

        public bool CanUndo
        {
            get
            {
                return false;
            }
        }

        public IPath CurrentGraph
        {
            get
            {
                return this.currentGraph;
            }
            set
            {
                if (this.currentGraph != value)
                {
                    this.currentGraph = value;
                    this.activePoints.Clear();
                    this.currentinfo = null;
                    if (this.mouseAreaControl != null)
                    {
                        this.mouseAreaControl.Invalidate();
                    }
                    if (value is GraphPath)
                    {
                        this.attributename = "d";
                    }
                    else if (value is MotionAnimate)
                    {
                        this.attributename = "path";
                    }
                    if (value is IGraph)
                    {
                        this.revertMatrix = ((IGraph) value).GraphTransform.Matrix.Clone();
                    }
                    else if (value is MotionAnimate)
                    {
                        SvgElement element1 = ((MotionAnimate) value).RefElement;
                        if (element1 is IGraph)
                        {
                            Matrix matrix1 = ((IGraph) element1).Transform.Matrix.Clone();
                            this.revertMatrix = ((IGraph) element1).GraphTransform.Matrix.Clone();
                            matrix1.Invert();
                            this.revertMatrix.Multiply(matrix1);
                            Matrix matrix2 = new Matrix();
                            matrix2 = AnimFunc.GetMatrixForTime((IGraph) element1, this.mouseAreaControl.SVGDocument.ControlTime, ((MotionAnimate) this.currentGraph).Begin);
                            this.revertMatrix.Multiply(matrix2);
                            this.centerPoint = this.PointToView(this.mouseAreaControl.CenterPoint);
                            object obj1 = ((MotionAnimate) value).GetAnimateResult((float) this.mouseAreaControl.SVGDocument.ControlTime, DomType.SvgMatrix);
                            if (obj1 is Matrix)
                            {
                                Matrix matrix3 = (Matrix) obj1;
                                this.centerPoint.X -= matrix3.OffsetX;
                                this.centerPoint.Y -= matrix3.OffsetY;
                            }
                            this.revertMatrix.Translate(this.centerPoint.X, this.centerPoint.Y);
                        }
                    }
                }
            }
        }

        private PointInfo currentInfo
        {
            get
            {
                return this.currentinfo;
            }
            set
            {
                if (this.currentinfo != value)
                {
                    this.currentinfo = value;
                    if (this.currentinfo != null)
                    {
                        if (this.currentinfo.SecondControl.IsEmpty && !this.currentinfo.NextControl.IsEmpty)
                        {
                            this.currentinfo.SecondControl = new PointF((2f * this.currentinfo.MiddlePoint.X) - this.currentinfo.NextControl.X, (2f * this.currentinfo.MiddlePoint.Y) - this.currentinfo.NextControl.Y);
                            this.starttrend = this.currentinfo.SecondControl;
                        }
                        if (!this.currentinfo.SecondControl.IsEmpty && this.currentinfo.NextControl.IsEmpty)
                        {
                            this.currentinfo.NextControl = new PointF((2f * this.currentinfo.MiddlePoint.X) - this.currentinfo.SecondControl.X, (2f * this.currentinfo.MiddlePoint.Y) - this.currentinfo.SecondControl.Y);
                            this.endtrend = this.currentinfo.NextControl;
                        }
                    }
                    this.mouseAreaControl.Invalidate();
                }
            }
        }

        private BezierOperate CurrentOperate
        {
            set
            {
                this.currentOperate = value;
                string text1 = string.Empty;
                switch (this.currentOperate)
                {
                    case BezierOperate.Draw:
                    {
                        this.mouseAreaControl.Cursor = SpecialCursors.bezierCursor;
                        if (!this.tooltips.ContainsKey("bezierdraw"))
                        {
                            this.tooltips.Add("bezierdraw", DrawAreaConfig.GetLabelForName("bezierdraw").Trim());
                        }
                        text1 = this.tooltips["bezierdraw"].ToString().Trim();
                        break;
                    }
                    case BezierOperate.AddAnchor:
                    {
                        this.mouseAreaControl.Cursor = SpecialCursors.addDotCursor;
                        if (!this.tooltips.ContainsKey("addanchor"))
                        {
                            this.tooltips.Add("addanchor", DrawAreaConfig.GetLabelForName("addanchor").Trim());
                        }
                        text1 = this.tooltips["addanchor"].ToString().Trim();
                        break;
                    }
                    case BezierOperate.DelAnchor:
                    {
                        this.mouseAreaControl.Cursor = SpecialCursors.delDotCursor;
                        if (!this.tooltips.ContainsKey("delanchor"))
                        {
                            this.tooltips.Add("delanchor", DrawAreaConfig.GetLabelForName("delanchor").Trim());
                        }
                        text1 = this.tooltips["delanchor"].ToString().Trim();
                        break;
                    }
                    case BezierOperate.ChangeAnchor:
                    case BezierOperate.ChangeEndAnchor:
                    {
                        this.mouseAreaControl.Cursor = SpecialCursors.ChangeControlCursor;
                        if (this.currentOperate == BezierOperate.ChangeEndAnchor)
                        {
                            this.mouseAreaControl.Cursor = SpecialCursors.ChangeEnd;
                        }
                        if (!this.tooltips.ContainsKey("changeendanchor"))
                        {
                            this.tooltips.Add("changeendanchor", DrawAreaConfig.GetLabelForName("changeendanchor").Trim());
                        }
                        text1 = this.tooltips["changeendanchor"].ToString().Trim();
                        break;
                    }
                    case BezierOperate.MoveAnchor:
                    case BezierOperate.MoveControl:
                    case BezierOperate.SelectPath:
                    {
                        this.mouseAreaControl.Cursor = SpecialCursors.dotCursor;
                        if (!this.tooltips.ContainsKey("moveanchor"))
                        {
                            this.tooltips.Add("moveanchor", DrawAreaConfig.GetLabelForName("moveanchor").Trim());
                        }
                        text1 = this.tooltips["moveanchor"].ToString().Trim();
                        if ((((this.preInfo != null) && (value == BezierOperate.MoveAnchor)) || ((this.moveinfo != null) && (value == BezierOperate.MoveControl))) || ((value == BezierOperate.SelectPath) || (value == BezierOperate.MovePath)))
                        {
                            if ((this.mouseAreaControl.CurrentOperation != ToolOperation.ConvertAnchor) && (Control.ModifierKeys != Keys.Alt))
                            {
                                this.mouseAreaControl.Cursor = SpecialCursors.ShapeDragCursor;
                            }
                            else
                            {
                                this.mouseAreaControl.Cursor = SpecialCursors.AnchorMoveCursor;
                            }
                        }
                        break;
                    }
                    case BezierOperate.ConvertAnchor:
                    {
                        if (!this.tooltips.ContainsKey("convertanchor"))
                        {
                            this.tooltips.Add("convertanchor", DrawAreaConfig.GetLabelForName("convertanchor").Trim());
                        }
                        text1 = this.tooltips["convertanchor"].ToString().Trim();
                        if (this.preInfo != null)
                        {
                            this.mouseAreaControl.Cursor = SpecialCursors.AnchorMoveCursor;
                            break;
                        }
                        this.mouseAreaControl.Cursor = SpecialCursors.AnchorCursor;
                        break;
                    }
                    case BezierOperate.CloseFigure:
                    {
                        this.mouseAreaControl.Cursor = SpecialCursors.CloseBezierCursor;
                        if (!this.tooltips.ContainsKey("closefigure"))
                        {
                            this.tooltips.Add("closefigure", DrawAreaConfig.GetLabelForName("closefigure").Trim());
                        }
                        text1 = this.tooltips["closefigure"].ToString().Trim();
                        break;
                    }
                    case BezierOperate.MovePath:
                    {
                        this.mouseAreaControl.Cursor = SpecialCursors.MovePath;
                        if (!this.tooltips.ContainsKey("movepath"))
                        {
                            this.tooltips.Add("movepath", DrawAreaConfig.GetLabelForName("movepath").Trim());
                        }
                        text1 = this.tooltips["movepath"].ToString().Trim();
                        break;
                    }
                    case BezierOperate.Select:
                    {
                        if (!this.tooltips.ContainsKey("bezierselect"))
                        {
                            this.tooltips.Add("bezierselect", DrawAreaConfig.GetLabelForName("bezierselect").Trim());
                        }
                        text1 = this.tooltips["bezierselect"].ToString().Trim();
                        this.mouseAreaControl.Cursor = SpecialCursors.dotCursor;
                        break;
                    }
                    case BezierOperate.CenterPoint:
                    {
                        if (!this.incenter)
                        {
                            if (!this.tooltips.ContainsKey("bezierselectcenterpoint"))
                            {
                                this.tooltips.Add("bezierselectcenterpoint", DrawAreaConfig.GetLabelForName("bezierselectcenterpoint").Trim());
                            }
                            text1 = this.tooltips["bezierselectcenterpoint"].ToString().Trim();
                            this.mouseAreaControl.Cursor = SpecialCursors.selectCursor;
                            break;
                        }
                        if (!this.tooltips.ContainsKey("beziercenterpoint"))
                        {
                            this.tooltips.Add("beziercenterpoint", DrawAreaConfig.GetLabelForName("beziercenterpoint").Trim());
                        }
                        text1 = this.tooltips["beziercenterpoint"].ToString().Trim();
                        this.mouseAreaControl.Cursor = SpecialCursors.CenterPointCursor;
                        break;
                    }
                    case BezierOperate.None:
                    {
                        this.mouseAreaControl.Cursor = SpecialCursors.bezierCursor;
                        if (!this.tooltips.ContainsKey("bezierdraw"))
                        {
                            this.tooltips.Add("bezierdraw", DrawAreaConfig.GetLabelForName("bezierdraw").Trim());
                        }
                        text1 = this.tooltips["bezierdraw"].ToString().Trim();
                        break;
                    }
                    default:
                    {
                        this.mouseAreaControl.Cursor = SpecialCursors.bezierCursor;
                        break;
                    }
                }
                string text2 = string.Empty;
                if (this.mouseAreaControl.CurrentOperation != ToolOperation.ShapeTransform && this.mouseAreaControl.CurrentOperation != ToolOperation.Custom11 && this.mouseAreaControl.CurrentOperation != ToolOperation.Custom15 
                      && this.mouseAreaControl.CurrentOperation != ToolOperation.Custom12 && this.mouseAreaControl.CurrentOperation != ToolOperation.Custom13 && this.mouseAreaControl.CurrentOperation != ToolOperation.Custom14)
                {
                    Keys keys1 = Control.ModifierKeys;
                    if (keys1 <= Keys.Control)
                    {
                        if (keys1 == Keys.Shift)
                        {
                            if (!this.tooltips.ContainsKey("beziershift"))
                            {
                                this.tooltips.Add("beziershift", DrawAreaConfig.GetLabelForName("beziershift").Trim());
                            }
                            text2 = this.tooltips["beziershift"].ToString().Trim();
                            goto Label_0728;
                        }
                        if (keys1 == Keys.Control)
                        {
                            if (!this.tooltips.ContainsKey("bezierctrl"))
                            {
                                this.tooltips.Add("bezierctrl", DrawAreaConfig.GetLabelForName("bezierctrl").Trim());
                            }
                            text2 = this.tooltips["bezierctrl"].ToString().Trim();
                            goto Label_0728;
                        }
                    }
                    else
                    {
                        if (keys1 == (Keys.Control | Keys.Shift))
                        {
                            text2 = string.Empty;
                            goto Label_0728;
                        }
                        if (keys1 == Keys.Alt)
                        {
                            if (!this.tooltips.ContainsKey("bezieraltl"))
                            {
                                this.tooltips.Add("bezieraltl", DrawAreaConfig.GetLabelForName("bezieraltl").Trim());
                            }
                            text2 = this.tooltips["bezieraltl"].ToString().Trim();
                            goto Label_0728;
                        }
                    }
                    if (!this.tooltips.ContainsKey("beziernokeys"))
                    {
                        this.tooltips.Add("beziernokeys", DrawAreaConfig.GetLabelForName("beziernokeys").Trim());
                    }
                    text2 = this.tooltips["beziernokeys"].ToString().Trim();
                }
            Label_0728:
                if ((this.currentGraph is MotionAnimate) && (Control.ModifierKeys != (Keys.Control | Keys.Shift)))
                {
                    if (!this.tooltips.ContainsKey("beziermotionkey"))
                    {
                        this.tooltips.Add("beziermotionkey", DrawAreaConfig.GetLabelForName("beziermotionkey").Trim());
                    }
                    text2 = text2 + this.tooltips["beziermotionkey"].ToString().Trim();
                }
                this.mouseAreaControl.PicturePanel.ToolTip(text1 + text2, 1);
            }
        }


        // Fields
        private int[] activeindex;
        private PointInfoCollection activePoints;
        private PointInfo addInfo;
        private string attributename;
        private PointF centerPoint;
        private IPath currentGraph;
        private PointInfo currentinfo;
        private BezierOperate currentOperate;
        private GraphicsPath editpath;
        private GraphicsPath editPath;
        private PointF endtrend;
        private bool incenter;
        private MouseArea mouseAreaControl;
        private PointInfo moveinfo;
        private PointF movePoint;
        private int oldindex;
        private string oldstr;
        private PointInfo preDrawInfo;
        private PointInfo preInfo;
        private GraphicsPath reversePath;
        private Matrix revertMatrix;
        private bool showcontrol;
        private PointF startPoint;
        private PointF starttrend;
        private Hashtable SubpathList;
        private Hashtable tooltips;
		private Win32 win32;
		#region IOperation 成员

		public void OnMouseWheel(MouseEventArgs e)
		{
			// TODO:  添加 BezierOperation.DealMouseWheel 实现
		}

		#endregion
	}
}

