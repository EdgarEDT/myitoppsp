namespace ItopVector.DrawArea
{
    using ItopVector;
    using ItopVector.Core;
    using ItopVector.Core.Animate;
    using ItopVector.Core.Document;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Core.Interface.Paint;
    using ItopVector.Core.Paint;
    using ItopVector.Core.Types;
    using ItopVector.Resource;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class ColorOperation : IOperation
    {
        // Methods
        public ColorOperation()
        {
            this.mouseAreaControl = null;
            this.translatePath = new GraphicsPath();
            this.scalePath = new GraphicsPath();
            this.rotatePath = new GraphicsPath();
            this.equalPath = new GraphicsPath();
            this.skewxPath = new GraphicsPath();
            this.skewyPath = new GraphicsPath();
            this.anglescalePath = new GraphicsPath();
            this.vscalPath = new GraphicsPath();
            this.controlPoints = null;
            this.reversePath = new GraphicsPath();
            this.startPoint = PointF.Empty;
            this.win32 = new Win32();
            this.ratiomatrix = new Matrix();
            this.graphMatrix = new Matrix();
            this.selectMatrix = new Matrix();
            this.gradientMatrix = new Matrix();
            this.gradientPath = new GraphicsPath();
            this.gradientheight = 1f;
            this.centerPoint = PointF.Empty;
            this.gradientstr = string.Empty;
            this.colorpickerstr = string.Empty;
            this.gradientstr = DrawAreaConfig.GetLabelForName("gradienttransform").Trim();
            this.colorpickerstr = DrawAreaConfig.GetLabelForName("colorpicker").Trim();
        }

        internal ColorOperation(MouseArea mousecontrol)
        {
            this.mouseAreaControl = null;
            this.translatePath = new GraphicsPath();
            this.scalePath = new GraphicsPath();
            this.rotatePath = new GraphicsPath();
            this.equalPath = new GraphicsPath();
            this.skewxPath = new GraphicsPath();
            this.skewyPath = new GraphicsPath();
            this.anglescalePath = new GraphicsPath();
            this.vscalPath = new GraphicsPath();
            this.controlPoints = null;
            this.reversePath = new GraphicsPath();
            this.startPoint = PointF.Empty;
            this.win32 = new Win32();
            this.ratiomatrix = new Matrix();
            this.graphMatrix = new Matrix();
            this.selectMatrix = new Matrix();
            this.gradientMatrix = new Matrix();
            this.gradientPath = new GraphicsPath();
            this.gradientheight = 1f;
            this.centerPoint = PointF.Empty;
            this.gradientstr = string.Empty;
            this.colorpickerstr = string.Empty;
            this.mouseAreaControl = mousecontrol;
            this.win32 = mousecontrol.win32;
            this.gradientstr = DrawAreaConfig.GetLabelForName("gradienttransform").Trim();
            this.colorpickerstr = DrawAreaConfig.GetLabelForName("colorpicker").Trim();
        }

        public void Dispose()
        {
            this.mouseAreaControl.ColorOperation = null;
        }
		public void OnMouseWheel(MouseEventArgs e)
		{
			// TODO:  Ìí¼Ó BezierOperation.DealMouseWheel ÊµÏÖ
		}
        public void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.mouseAreaControl.Cursor == this.mouseAreaControl.DefaultCursor)
                {
                    PointF tf1 = new PointF((float) e.X, (float) e.Y);
                    SvgElementCollection collection1 = this.mouseAreaControl.SVGDocument.SelectCollection;
                    if (collection1 != null)
                    {
                        for (int num1 = collection1.Count - 1; num1 >= 0; num1--)
                        {
                            if (collection1[num1] is IGraphPath)
                            {
                                IGraph graph1 = (IGraphPath) collection1[num1];
                                GraphicsPath path1 = (GraphicsPath) graph1.GPath.Clone();
                                Matrix matrix1 = graph1.GraphTransform.Matrix.Clone();
                                path1.Transform(matrix1);
                                Pen pen1 = new Pen(Color.Blue, 4f);
                                pen1.Alignment = PenAlignment.Center;
                                bool flag1 = false;
                                flag1 = path1.IsVisible(tf1);
                                if (path1.IsOutlineVisible(tf1, pen1))
                                {
                                    flag1 = true;
                                }
                                if (flag1)
                                {
                                    this.ActiveGraph = (IGraphPath) graph1;
                                    return;
                                }
                            }
                        }
                        collection1 = this.mouseAreaControl.PicturePanel.ElementList;
                        for (int num2 = collection1.Count - 1; num2 >= 0; num2--)
                        {
                            if (collection1[num2] is IGraphPath)
                            {
                                IGraph graph2 = (IGraphPath) collection1[num2];
                                GraphicsPath path2 = (GraphicsPath) graph2.GPath.Clone();
                                Matrix matrix2 = graph2.GraphTransform.Matrix.Clone();
                                path2.Transform(matrix2);
                                Pen pen2 = new Pen(Color.Blue, 4f);
                                pen2.Alignment = PenAlignment.Center;
                                bool flag2 = false;
                                flag2 = path2.IsVisible(tf1);
                                if (path2.IsOutlineVisible(tf1, pen2))
                                {
                                    flag2 = true;
                                }
                                if (flag2)
                                {
                                    this.ActiveGraph = (IGraphPath) graph2;
                                    return;
                                }
                            }
                        }
                        this.activeGraph = null;
                        this.mouseAreaControl.Invalidate();
                    }
                }
                else
                {
                    this.startPoint = new PointF((float) e.X, (float) e.Y);
                    this.reversePath.Reset();
                }
            }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            SizeF ef1 = this.mouseAreaControl.PicturePanel.GridSize;
            float single1 = ef1.Width;
            float single2 = ef1.Height;
            ToolOperation operation1 = this.mouseAreaControl.CurrentOperation;
            if (operation1 == ToolOperation.GradientTransform)
            {
                this.mouseAreaControl.PicturePanel.ToolTip(this.gradientstr, 1);
            }
            else if (operation1 == ToolOperation.ColorPicker)
            {
                this.mouseAreaControl.PicturePanel.ToolTip(this.colorpickerstr, 1);
            }
            if ((operation1 == ToolOperation.GradientTransform) && (this.activeGraph != null))
            {
                ISvgBrush brush1 = this.activeGraph.GraphBrush;
                if (!(brush1 is SolidColor))
                {
                    ITransformBrush brush2 = (ITransformBrush) brush1;
                    bool flag1 = brush2.Units == Units.UserSpaceOnUse;
                    if (e.Button == MouseButtons.None)
                    {
                        Pen pen1 = new Pen(Color.Blue, 4f);
                        pen1.Alignment = PenAlignment.Center;
                        PointF tf1 = new PointF((float) e.X, (float) e.Y);
                        if (this.translatePath.IsVisible(tf1) || this.translatePath.IsOutlineVisible(tf1, pen1))
                        {
                            this.mouseAreaControl.Cursor = SpecialCursors.GradientTranslateCursor;
                        }
                        else if (this.scalePath.IsVisible(tf1) || this.scalePath.IsOutlineVisible(tf1, pen1))
                        {
                            this.mouseAreaControl.Cursor = SpecialCursors.VScaleCursor;
                        }
                        else if (this.rotatePath.IsOutlineVisible(tf1, pen1) || this.rotatePath.IsVisible(tf1))
                        {
                            this.mouseAreaControl.Cursor = SpecialCursors.RotateCursor;
                        }
                        else if (this.equalPath.IsOutlineVisible(tf1, pen1) || this.equalPath.IsVisible(tf1))
                        {
                            this.mouseAreaControl.Cursor = SpecialCursors.EqualScaleCursor;
                        }
                        else if (this.vscalPath.IsOutlineVisible(tf1, pen1) || this.vscalPath.IsVisible(tf1))
                        {
                            this.mouseAreaControl.Cursor = SpecialCursors.HScaleCursor;
                        }
                        else if (this.skewxPath.IsOutlineVisible(tf1, pen1) || this.skewxPath.IsVisible(tf1))
                        {
                            this.mouseAreaControl.Cursor = SpecialCursors.SkewXCursor;
                        }
                        else if (this.skewyPath.IsOutlineVisible(tf1, pen1) || this.skewyPath.IsVisible(tf1))
                        {
                            this.mouseAreaControl.Cursor = SpecialCursors.SkewYCursor;
                        }
                        else if (this.anglescalePath.IsOutlineVisible(tf1, pen1) || this.anglescalePath.IsVisible(tf1))
                        {
                            this.mouseAreaControl.Cursor = SpecialCursors.TopRightScaleCursor;
                        }
                        else
                        {
                            this.mouseAreaControl.Cursor = this.mouseAreaControl.DefaultCursor;
                        }
                    }
                    else if ((e.Button == MouseButtons.Left) && (this.mouseAreaControl.Cursor != this.mouseAreaControl.DefaultCursor))
                    {
                        PointF tf2 = this.mouseAreaControl.PicturePanel.PointToView(this.startPoint);
                        PointF tf3 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
                        if (this.mouseAreaControl.PicturePanel.SnapToGrid)
                        {
                            int num1 = (int) ((tf2.X + (single1 / 2f)) / single1);
                            int num2 = (int) ((tf2.Y + (single2 / 2f)) / single2);
                            tf2 = (PointF) new Point((int) (num1 * single1), (int) (num2 * single2));
                            num1 = (int) ((tf3.X + (single1 / 2f)) / single1);
                            num2 = (int) ((tf3.Y + (single2 / 2f)) / single2);
                            tf3 = (PointF) new Point((int) (num1 * single1), (int) (single2 * num2));
                        }
                        tf2 = this.mouseAreaControl.PicturePanel.PointToSystem(tf2);
                        tf3 = this.mouseAreaControl.PicturePanel.PointToSystem(tf3);
                        this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
                        this.win32.W32SetROP2(6);
                        this.win32.W32PolyDraw(this.reversePath);
                        Matrix matrix1 = this.gradientMatrix.Clone();
                        if (((this.mouseAreaControl.Cursor == SpecialCursors.VScaleCursor) || (this.mouseAreaControl.Cursor == SpecialCursors.HScaleCursor)) || (this.mouseAreaControl.Cursor == SpecialCursors.TopRightScaleCursor))
                        {
                            PointF[] tfArray6 = new PointF[5] { tf2, tf3, this.controlPoints[0], this.controlPoints[1], this.controlPoints[5] } ;
                            PointF[] tfArray1 = tfArray6;
                            Matrix matrix2 = this.graphMatrix.Clone();
                            matrix2.Multiply(this.gradientMatrix);
                            matrix2.Invert();
                            matrix2.TransformPoints(tfArray1);
                            float single3 = 1f;
                            if (tfArray1[3].X != tfArray1[2].X)
                            {
                                single3 = 1f + ((tfArray1[1].X - tfArray1[0].X) / (tfArray1[3].X - tfArray1[2].X));
                            }
                            float single4 = 1f;
                            if (tfArray1[4].Y != tfArray1[2].Y)
                            {
                                single4 = 1f + ((tfArray1[1].Y - tfArray1[0].Y) / (tfArray1[4].Y - tfArray1[2].Y));
                            }
                            if (flag1)
                            {
                                matrix1.Translate(this.centerPoint.X, this.centerPoint.Y);
                            }
                            else
                            {
                                matrix1.Translate(0.5f, 0.5f);
                            }
                            if (this.mouseAreaControl.Cursor == SpecialCursors.VScaleCursor)
                            {
                                single4 = 1f;
                            }
                            else if (this.mouseAreaControl.Cursor == SpecialCursors.HScaleCursor)
                            {
                                single3 = 1f;
                            }
                            matrix1.Scale(single3, single4);
                            if (flag1)
                            {
                                matrix1.Translate(-this.centerPoint.X, -this.centerPoint.Y);
                            }
                            else
                            {
                                matrix1.Translate(-0.5f, -0.5f);
                            }
                        }
                        else if ((this.mouseAreaControl.Cursor == SpecialCursors.SkewXCursor) || (this.mouseAreaControl.Cursor == SpecialCursors.SkewYCursor))
                        {
                            PointF[] tfArray7 = new PointF[5] { tf2, tf3, this.controlPoints[0], this.controlPoints[3], this.controlPoints[4] } ;
                            PointF[] tfArray2 = tfArray7;
                            Matrix matrix3 = this.graphMatrix.Clone();
                            matrix3.Multiply(this.gradientMatrix);
                            matrix3.Invert();
                            matrix3.TransformPoints(tfArray2);
                            float single5 = 0f;
                            if (tfArray2[3].Y != tfArray2[2].Y)
                            {
                                single5 = ((tfArray2[1].X - tfArray2[0].X) / (tfArray2[3].Y - tfArray2[2].Y)) * 0.5f;
                            }
                            float single6 = 0f;
                            if (tfArray2[4].X != tfArray2[2].X)
                            {
                                single6 = ((tfArray2[1].Y - tfArray2[0].Y) / (tfArray2[4].X - tfArray2[2].X)) * 0.5f;
                            }
                            if (this.mouseAreaControl.Cursor == SpecialCursors.SkewXCursor)
                            {
                                single6 = 0f;
                            }
                            else if (this.mouseAreaControl.Cursor == SpecialCursors.SkewYCursor)
                            {
                                single5 = 0f;
                            }
                            if (flag1)
                            {
                                matrix1.Translate(this.centerPoint.X, this.centerPoint.Y);
                            }
                            else
                            {
                                matrix1.Translate(0.5f, 0.5f);
                            }
                            matrix1.Shear(single5, single6);
                            if (flag1)
                            {
                                matrix1.Translate(-this.centerPoint.X, -this.centerPoint.Y);
                            }
                            else
                            {
                                matrix1.Translate(-0.5f, -0.5f);
                            }
                        }
                        else if (this.mouseAreaControl.Cursor == SpecialCursors.RotateCursor)
                        {
                            PointF tf4 = this.controlPoints[0];
                            float single7 = (float) Math.Atan((double) ((tf2.Y - tf4.Y) / (tf2.X - tf4.X)));
                            float single8 = (float) Math.Atan((double) ((tf3.Y - tf4.Y) / (tf3.X - tf4.X)));
                            float single9 = ((float) (((double) (single8 - single7)) / 3.1415926535897931)) * 180f;
                            if (((tf3.X - tf4.X) * (tf2.X - tf4.X)) < 0f)
                            {
                                single9 += 180f;
                            }
                            matrix1 = this.gradientMatrix.Clone();
                            PointF[] tfArray8 = new PointF[1] { new PointF(0.5f, 0.5f) } ;
                            PointF[] tfArray3 = tfArray8;
                            if (flag1)
                            {
                                PointF[] tfArray9 = new PointF[1] { this.centerPoint } ;
                                tfArray3 = tfArray9;
                            }
                            this.gradientMatrix.TransformPoints(tfArray3);
                            matrix1.RotateAt(single9, tfArray3[0], MatrixOrder.Append);
                        }
                        else if (this.mouseAreaControl.Cursor == SpecialCursors.GradientTranslateCursor)
                        {
                            PointF[] tfArray10 = new PointF[5] { tf2, tf3, this.controlPoints[0], this.controlPoints[1], this.controlPoints[4] } ;
                            PointF[] tfArray4 = tfArray10;
                            bool flag2 = tfArray4[4].IsEmpty;
                            Matrix matrix4 = this.graphMatrix.Clone();
                            matrix4.Multiply(this.gradientMatrix);
                            matrix4.Invert();
                            matrix4.TransformPoints(tfArray4);
                            float single10 = tfArray4[1].X - tfArray4[0].X;
                            if (!flag1)
                            {
                                single10 /= (tfArray4[3].X - tfArray4[2].X);
                            }
                            else
                            {
                                single10 *= 2f;
                            }
                            float single11 = tfArray4[1].Y - tfArray4[0].Y;
                            if (!flag1)
                            {
                                single11 /= (tfArray4[4].Y - tfArray4[2].Y);
                            }
                            else
                            {
                                single11 *= 2f;
                            }
                            single10 /= 2f;
                            single11 /= 2f;
                            matrix1.Translate(single10, single11);
                        }
                        else if (this.mouseAreaControl.Cursor == SpecialCursors.EqualScaleCursor)
                        {
                            PointF[] tfArray11 = new PointF[5] { tf2, tf3, this.controlPoints[0], this.controlPoints[1], this.controlPoints[4] } ;
                            PointF[] tfArray5 = tfArray11;
                            Matrix matrix5 = this.graphMatrix.Clone();
                            matrix5.Multiply(this.gradientMatrix);
                            matrix5.Invert();
                            matrix5.TransformPoints(tfArray5);
                            float single12 = 1f + ((tfArray5[1].X - tfArray5[0].X) / (tfArray5[3].X - tfArray5[2].X));
                            if (flag1)
                            {
                                matrix1.Translate(this.centerPoint.X, this.centerPoint.Y);
                            }
                            else
                            {
                                matrix1.Translate(0.5f, 0.5f);
                            }
                            matrix1.Scale(single12, single12);
                            if (flag1)
                            {
                                matrix1.Translate(-this.centerPoint.X, -this.centerPoint.Y);
                            }
                            else
                            {
                                matrix1.Translate(-0.5f, -0.5f);
                            }
                        }
                        if (this.mouseAreaControl.Cursor == SpecialCursors.RotateCursor)
                        {
                            this.reversePath = (GraphicsPath) this.gradientPath.Clone();
                            Matrix matrix6 = this.graphMatrix.Clone();
                            matrix6.Multiply(matrix1);
                            this.reversePath.Transform(matrix6);
                        }
                        else
                        {
                            this.reversePath = (GraphicsPath) this.gradientPath.Clone();
                            this.reversePath.Transform(matrix1);
                            this.reversePath.Transform(this.graphMatrix);
                        }
                        this.win32.W32PolyDraw(this.reversePath);
                        this.win32.ReleaseDC();
                    }
                }
            }
        }

        public void OnMouseUp(MouseEventArgs e)
        {
            if ((this.activeGraph == null) || (this.mouseAreaControl.Cursor == this.mouseAreaControl.DefaultCursor))
            {
                return;
            }
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            if (!(this.mouseAreaControl.SVGDocument.CurrentElement is ITransformBrush))
            {
                return;
            }
            ISvgBrush brush1 = (ISvgBrush) this.mouseAreaControl.SVGDocument.CurrentElement;
            if (brush1 is SolidColor)
            {
                return;
            }
            SizeF ef1 = this.mouseAreaControl.PicturePanel.GridSize;
            float single1 = ef1.Width;
            float single2 = ef1.Height;
            this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
            this.win32.W32SetROP2(6);
            this.win32.W32PolyDraw(this.reversePath);
            this.win32.ReleaseDC();
            float single3 = this.mouseAreaControl.PicturePanel.ScaleUnit;
            PointF tf1 = this.mouseAreaControl.PicturePanel.PointToView(this.startPoint);
            PointF tf2 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
            if (this.mouseAreaControl.PicturePanel.SnapToGrid)
            {
                int num1 = (int) ((tf1.X + (single1 / 2f)) / single1);
                int num2 = (int) ((tf1.Y + (single2 / 2f)) / single2);
                tf1 = (PointF) new Point((int) (num1 * single1), (int) (num2 * single2));
                num1 = (int) ((tf2.X + (single1 / 2f)) / single1);
                num2 = (int) ((tf2.Y + (single2 / 2f)) / single2);
                tf2 = (PointF) new Point((int) (num1 * single1), (int) (single2 * num2));
            }
            tf1 = this.mouseAreaControl.PicturePanel.PointToSystem(tf1);
            tf2 = this.mouseAreaControl.PicturePanel.PointToSystem(tf2);
            this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
            this.win32.W32SetROP2(6);
            this.win32.W32PolyDraw(this.reversePath);
			this.win32.ReleaseDC();
            Transf transf1 = new Transf();
            transf1 = ((ITransformBrush) brush1).Transform;
            SvgElement element1 = (SvgElement) brush1;
            ITransformBrush brush2 = (ITransformBrush) brush1;
            bool flag1 = brush2.Units == Units.UserSpaceOnUse;
            SvgDocument document1 = this.mouseAreaControl.SVGDocument;
            if (((((this.mouseAreaControl.Cursor == SpecialCursors.VScaleCursor) | (this.mouseAreaControl.Cursor == SpecialCursors.HScaleCursor)) | (this.mouseAreaControl.Cursor == SpecialCursors.TopRightScaleCursor)) | (this.mouseAreaControl.Cursor == SpecialCursors.EqualScaleCursor)) && (this.controlPoints.Length >= 6))
            {
                //string text11;
                PointF[] tfArray5 = new PointF[5] { tf1, tf2, this.controlPoints[0], this.controlPoints[1], this.controlPoints[5] } ;
                PointF[] tfArray1 = tfArray5;
                Matrix matrix1 = this.graphMatrix.Clone();
                matrix1.Multiply(this.gradientMatrix);
                matrix1.Invert();
                matrix1.TransformPoints(tfArray1);
                float single4 = 1f;
                if (tfArray1[3].X != tfArray1[2].X)
                {
                    single4 = 1f + ((tfArray1[1].X - tfArray1[0].X) / (tfArray1[3].X - tfArray1[2].X));
                }
                float single5 = 1f;
                if (tfArray1[4].Y != tfArray1[2].Y)
                {
                    single5 = 1f + ((tfArray1[1].Y - tfArray1[0].Y) / (tfArray1[4].Y - tfArray1[2].Y));
                }
                if (this.mouseAreaControl.Cursor == SpecialCursors.VScaleCursor)
                {
                    single5 = 1f;
                }
                else if (this.mouseAreaControl.Cursor == SpecialCursors.HScaleCursor)
                {
                    single4 = 1f;
                }
                else if (this.mouseAreaControl.Cursor == SpecialCursors.EqualScaleCursor)
                {
                    single5 = single4;
                }
                if ((element1.InfoList.Count == 1))
                {
                    if (flag1)
                    {
                        transf1.setTranslate(this.centerPoint.X, this.centerPoint.Y);
                    }
                    else
                    {
                        transf1.setTranslate(0.5f, 0.5f);
                    }
                    transf1.setScale(single4, single5);
                    if (flag1)
                    {
                        transf1.setTranslate(-this.centerPoint.X, -this.centerPoint.Y);
                    }
                    else
                    {
                        transf1.setTranslate(-0.5f, -0.5f);
                    }
                    string text1 = transf1.ToString();
                    string text2 = "gradientTransform";
                    if (brush2 is Pattern)
                    {
                        text2 = "patternTransform";
                    }
                    bool flag2 = document1.AcceptChanges;
                    document1.AcceptChanges = true;
                    document1.NumberOfUndoOperations = 1;
                    AttributeFunc.SetAttributeValue(element1, text2, text1);
                    document1.AcceptChanges = flag2;
                    element1.pretime = -1;
                    goto Label_1771;
                }
                
            }
            if ((this.mouseAreaControl.Cursor == SpecialCursors.SkewXCursor) || (this.mouseAreaControl.Cursor == SpecialCursors.SkewYCursor))
            {
                //string text16;
                PointF[] tfArray6 = new PointF[5] { tf1, tf2, this.controlPoints[0], this.controlPoints[3], this.controlPoints[4] } ;
                PointF[] tfArray2 = tfArray6;
                Matrix matrix2 = this.graphMatrix.Clone();
                matrix2.Multiply(this.gradientMatrix);
                matrix2.Invert();
                matrix2.TransformPoints(tfArray2);
                float single6 = 0f;
                if (tfArray2[3].Y != tfArray2[2].Y)
                {
                    single6 = ((tfArray2[1].X - tfArray2[0].X) / (tfArray2[3].Y - tfArray2[2].Y)) * 0.5f;
                }
                float single7 = 0f;
                if (tfArray2[4].X != tfArray2[2].X)
                {
                    single7 = ((tfArray2[1].Y - tfArray2[0].Y) / (tfArray2[4].X - tfArray2[2].X)) * 0.5f;
                }
                if (this.mouseAreaControl.Cursor == SpecialCursors.SkewXCursor)
                {
                    single7 = 0f;
                }
                else if (this.mouseAreaControl.Cursor == SpecialCursors.SkewYCursor)
                {
                    single6 = 0f;
                }
                if ( (element1.InfoList.Count == 1))
                {
                    if (flag1)
                    {
                        transf1.setTranslate(this.centerPoint.X, this.centerPoint.Y);
                    }
                    else
                    {
                        transf1.setTranslate(0.5f, 0.5f);
                    }
                    transf1.Matrix.Shear(single6, single7);
                    if (flag1)
                    {
                        transf1.setTranslate(-this.centerPoint.X, -this.centerPoint.Y);
                    }
                    else
                    {
                        transf1.setTranslate(-0.5f, -0.5f);
                    }
                    string text3 = transf1.ToString();
                    string text4 = "gradientTransform";
                    if (brush2 is Pattern)
                    {
                        text4 = "patternTransform";
                    }
                    bool flag4 = document1.AcceptChanges;
                    document1.AcceptChanges = true;
                    document1.NumberOfUndoOperations = 1;
                    AttributeFunc.SetAttributeValue(element1, text4, text3);
                    document1.AcceptChanges = flag4;
                    element1.pretime = -1;
                    goto Label_1771;
                }
                
            }
            if (this.mouseAreaControl.Cursor == SpecialCursors.RotateCursor)
            {
                //string text19;
                PointF tf3 = this.controlPoints[0];
                float single8 = (float) Math.Atan((double) ((tf1.Y - tf3.Y) / (tf1.X - tf3.X)));
                float single9 = (float) Math.Atan((double) ((tf2.Y - tf3.Y) / (tf2.X - tf3.X)));
                float single10 = ((float) (((double) (single9 - single8)) / 3.1415926535897931)) * 180f;
                if (((tf2.X - tf3.X) * (tf1.X - tf3.X)) < 0f)
                {
                    single10 += 180f;
                }
                if (single10 < 0f)
                {
                    single10 += 360f;
                }
                PointF[] tfArray7 = new PointF[1] { new PointF(0.5f, 0.5f) } ;
                PointF[] tfArray3 = tfArray7;
                if (flag1)
                {
                    PointF[] tfArray8 = new PointF[1] { this.centerPoint } ;
                    tfArray3 = tfArray8;
                }
                this.gradientMatrix.TransformPoints(tfArray3);
                if ( (element1.InfoList.Count == 1))
                {
                    transf1.Matrix.RotateAt(single10, tfArray3[0], MatrixOrder.Append);
                    string text5 = transf1.ToString();
                    string text6 = "gradientTransform";
                    if (brush2 is Pattern)
                    {
                        text6 = "patternTransform";
                    }
                    bool flag6 = document1.AcceptChanges;
                    document1.AcceptChanges = true;
                    AttributeFunc.SetAttributeValue(element1, text6, text5);
                    document1.AcceptChanges = flag6;
                    element1.pretime = -1;
                    goto Label_1771;
                }
                
            }
            else if (this.mouseAreaControl.Cursor == SpecialCursors.GradientTranslateCursor)
            {
                PointF[] tfArray9 = new PointF[5] { tf1, tf2, this.controlPoints[0], this.controlPoints[1], this.controlPoints[4] } ;
                PointF[] tfArray4 = tfArray9;
                Matrix matrix3 = this.graphMatrix.Clone();
                matrix3.Multiply(this.gradientMatrix);
                matrix3.Invert();
                matrix3.TransformPoints(tfArray4);
                float single11 = tfArray4[1].X - tfArray4[0].X;
                if (!flag1)
                {
                    single11 /= (tfArray4[3].X - tfArray4[2].X);
                }
                else
                {
                    single11 *= 2f;
                }
                float single12 = tfArray4[1].Y - tfArray4[0].Y;
                if (!flag1)
                {
                    single12 /= (tfArray4[4].Y - tfArray4[2].Y);
                }
                else
                {
                    single12 *= 2f;
                }
                single11 /= 2f;
                single12 /= 2f;
                if (element1.InfoList.Count == 1)
                {
                    transf1.setTranslate(single11, single12);
                    string text7 = transf1.ToString();
                    string text8 = "gradientTransform";
                    if (brush2 is Pattern)
                    {
                        text8 = "patternTransform";
                    }
                    bool flag7 = document1.AcceptChanges;
                    document1.AcceptChanges = true;
                    AttributeFunc.SetAttributeValue(element1, text8, text7);
                    document1.AcceptChanges = flag7;
                    element1.pretime = -1;
                }
                
            }
        Label_1771:
            this.mouseAreaControl.Invalidate();
        }

        public void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            ToolOperation operation1 = this.mouseAreaControl.CurrentOperation;
            SvgElement element1 = this.mouseAreaControl.SVGDocument.CurrentElement;
            if (((operation1 == ToolOperation.GradientTransform) && (element1 is ISvgBrush)) && ((this.activeGraph != null) && !this.mouseAreaControl.SVGDocument.PlayAnim))
            {
                ISvgBrush brush1 = (ISvgBrush) this.mouseAreaControl.SVGDocument.CurrentElement;
                RectangleF ef1 = RectangleF.Empty;
                if (!(brush1 is SolidColor))
                {
                    GraphicsPath path1 = new GraphicsPath();
                    PointF tf1 = PointF.Empty;
                    PointF tf2 = PointF.Empty;
                    PointF tf3 = PointF.Empty;
                    PointF tf4 = PointF.Empty;
                    PointF tf5 = PointF.Empty;
                    PointF tf6 = PointF.Empty;
                    PointF tf7 = PointF.Empty;
                    PointF tf8 = PointF.Empty;
                    PointF[] tfArray1 = new PointF[1];
                    this.ratiomatrix.Reset();
                    this.gradientPath.Reset();
                    if (brush1 is LinearGradient)
                    {
                        LinearGradient gradient1 = (LinearGradient) brush1;
                        this.gradientPath = (GraphicsPath) gradient1.GradientPath.Clone();
                        this.controlPoints = (PointF[]) gradient1.BoundsPoints.Clone();
                        if (this.controlPoints.Length <= 0)
                        {
                            return;
                        }
                        this.graphMatrix = this.activeGraph.GraphTransform.Matrix.Clone();
                        this.graphMatrix.Multiply(gradient1.Coord);
                        this.gradientMatrix = gradient1.Transform.Matrix.Clone();
                        this.gradientPath.Transform(this.gradientMatrix);
                        this.gradientPath.Transform(this.graphMatrix);
                        e.Graphics.DrawPath(Pens.Black, this.gradientPath);
                        this.centerPoint = this.controlPoints[0];
                        this.gradientMatrix.TransformPoints(this.controlPoints);
                        this.graphMatrix.TransformPoints(this.controlPoints);
                        tf3 = this.controlPoints[0];
                        tf2 = this.controlPoints[1];
                        tf1 = this.controlPoints[2];
                        this.gradientPath = (GraphicsPath) gradient1.GradientPath.Clone();
                    }
                    else if (brush1 is RadialGradients)
                    {
                        RadialGradients gradients1 = (RadialGradients) brush1;
                        this.gradientPath = (GraphicsPath) gradients1.GradientPath.Clone();
                        this.controlPoints = (PointF[]) gradients1.BoundsPoints.Clone();
                        this.graphMatrix = this.activeGraph.GraphTransform.Matrix.Clone();
                        this.graphMatrix.Multiply(gradients1.Coord);
                        this.gradientMatrix = gradients1.Transform.Matrix.Clone();
                        this.gradientPath.Transform(this.gradientMatrix);
                        this.gradientPath.Transform(this.graphMatrix);
                        e.Graphics.DrawPath(Pens.Black, this.gradientPath);
                        RectangleF ef3 = PathFunc.GetBounds(this.gradientPath);
                        this.gradientheight = ef3.Height;
                        this.centerPoint = this.controlPoints[0];
                        this.gradientMatrix.TransformPoints(this.controlPoints);
                        this.graphMatrix.TransformPoints(this.controlPoints);
                        tf3 = this.controlPoints[0];
                        tf2 = this.controlPoints[2];
                        tf1 = this.controlPoints[3];
                        tf4 = this.controlPoints[1];
                        this.gradientPath = (GraphicsPath) gradients1.GradientPath.Clone();
                    }
                    else if (brush1 is Pattern)
                    {
                        Pattern pattern1 = (Pattern) brush1;
                        this.gradientPath = (GraphicsPath) pattern1.GradientPath.Clone();
                        RectangleF ef2 = this.gradientPath.GetBounds();
                        this.controlPoints = (PointF[]) pattern1.BoundsPoints.Clone();
                        this.graphMatrix = this.activeGraph.GraphTransform.Matrix.Clone();
                        this.gradientMatrix = pattern1.Transform.Matrix.Clone();
                        this.gradientPath.Transform(this.gradientMatrix);
                        this.gradientPath.Transform(this.graphMatrix);
                        e.Graphics.DrawPath(Pens.Black, this.gradientPath);
                        RectangleF ef4 = PathFunc.GetBounds(this.gradientPath);
                        this.gradientheight = ef4.Height;
                        this.centerPoint = this.controlPoints[0];
                        this.gradientMatrix.TransformPoints(this.controlPoints);
                        this.graphMatrix.TransformPoints(this.controlPoints);
                        tf3 = this.controlPoints[0];
                        tf1 = this.controlPoints[2];
                        tf7 = this.controlPoints[6];
                        tf5 = this.controlPoints[3];
                        tf6 = this.controlPoints[4];
                        tf2 = this.controlPoints[1];
                        tf8 = this.controlPoints[5];
                        this.gradientPath = (GraphicsPath) pattern1.GradientPath.Clone();
                    }
                    this.translatePath.Reset();
                    PointF[] tfArray2 = new PointF[4] { new PointF(tf3.X, tf3.Y - 4f), new PointF(tf3.X - 4f, tf3.Y), new PointF(tf3.X, tf3.Y + 4f), new PointF(tf3.X + 4f, tf3.Y) } ;
                    this.translatePath.AddPolygon(tfArray2);
                    e.Graphics.FillPath(Brushes.White, this.translatePath);
                    e.Graphics.DrawPath(Pens.Black, this.translatePath);
                    this.rotatePath.Reset();
                    this.rotatePath.AddEllipse((float) (tf1.X - 3f), (float) (tf1.Y - 3f), (float) 6f, (float) 6f);
                    this.scalePath.Reset();
                    this.scalePath.AddRectangle(new RectangleF(tf2.X - 3f, tf2.Y - 3f, 6f, 6f));
                    this.equalPath.Reset();
                    this.equalPath.AddEllipse((float) (tf4.X - 3f), (float) (tf4.Y - 3f), (float) 6f, (float) 6f);
                    this.skewxPath.Reset();
                    this.skewxPath.AddEllipse((float) (tf5.X - 3f), (float) (tf5.Y - 3f), (float) 6f, (float) 6f);
                    this.skewyPath.Reset();
                    this.skewyPath.AddEllipse((float) (tf6.X - 3f), (float) (tf6.Y - 3f), (float) 6f, (float) 6f);
                    this.anglescalePath.Reset();
                    this.anglescalePath.AddRectangle(new RectangleF(tf7.X - 3f, tf7.Y - 3f, 6f, 6f));
                    this.vscalPath.Reset();
                    this.vscalPath.AddRectangle(new RectangleF(tf8.X - 3f, tf8.Y - 3f, 6f, 6f));
                    e.Graphics.FillPath(Brushes.White, this.rotatePath);
                    e.Graphics.DrawPath(Pens.Black, this.rotatePath);
                    e.Graphics.FillPath(Brushes.White, this.scalePath);
                    e.Graphics.DrawPath(Pens.Black, this.scalePath);
                    e.Graphics.FillPath(Brushes.White, this.equalPath);
                    e.Graphics.DrawPath(Pens.Black, this.equalPath);
                    e.Graphics.FillPath(Brushes.White, this.skewxPath);
                    e.Graphics.DrawPath(Pens.Black, this.skewxPath);
                    e.Graphics.FillPath(Brushes.White, this.skewyPath);
                    e.Graphics.DrawPath(Pens.Black, this.skewyPath);
                    e.Graphics.FillPath(Brushes.White, this.anglescalePath);
                    e.Graphics.DrawPath(Pens.Black, this.anglescalePath);
                    e.Graphics.FillPath(Brushes.White, this.vscalPath);
                    e.Graphics.DrawPath(Pens.Black, this.vscalPath);
                }
            }
        }

        public bool ProcessDialogKey(Keys keydate)
        {
            return false;
        }

        public bool Redo()
        {
            return false;
        }

        public bool Undo()
        {
            return false;
        }


        // Properties
        private IGraphPath ActiveGraph
        {
            set
            {
                if (this.activeGraph != value)
                {
                    this.activeGraph = value;
                    switch (this.mouseAreaControl.CurrentOperation)
                    {
                        case ToolOperation.GradientTransform:
                        {
                            this.mouseAreaControl.Invalidate();
                            if (this.activeGraph != null)
                            {
                                ISvgBrush brush1 = this.activeGraph.GraphBrush;
                                bool flag1 = false;
                                if (brush1 is ITransformBrush)
                                {
                                    this.mouseAreaControl.SVGDocument.RootElement = (SvgElement) brush1;
                                    this.mouseAreaControl.SVGDocument.OnlyShowCurrent = false;
                                    this.mouseAreaControl.SVGDocument.CurrentElement = (SvgElement) brush1;
                                    flag1 = true;
                                }
                                brush1 = this.activeGraph.GraphStroke.Brush;
                                if (brush1 is ITransformBrush)
                                {
                                    this.mouseAreaControl.SVGDocument.OnlyShowCurrent = false;
                                    if (flag1)
                                    {
                                        this.mouseAreaControl.SVGDocument.EditRoots.Add((SvgElement) brush1);
                                    }
                                    else
                                    {
                                        this.mouseAreaControl.SVGDocument.RootElement = (SvgElement) brush1;
                                        this.mouseAreaControl.SVGDocument.CurrentElement = (SvgElement) brush1;
                                    }
                                }
                            }
                            break;
                        }
                        case ToolOperation.ColorPicker:
                        {
                            this.mouseAreaControl.PicturePanel.PostBrush(value.GraphBrush.Clone());
                            break;
                        }
                    }
                    this.controlPoints = null;
                }
            }
        }

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


        // Fields
        private IGraphPath activeGraph;
        private GraphicsPath anglescalePath;
        private PointF centerPoint;
        private string colorpickerstr;
        private PointF[] controlPoints;
        private GraphicsPath equalPath;
        private float gradientheight;
        private Matrix gradientMatrix;
        private GraphicsPath gradientPath;
        private string gradientstr;
        private Matrix graphMatrix;
        private MouseArea mouseAreaControl;
        private Matrix ratiomatrix;
        private GraphicsPath reversePath;
        private GraphicsPath rotatePath;
        private GraphicsPath scalePath;
        private Matrix selectMatrix;
        private GraphicsPath skewxPath;
        private GraphicsPath skewyPath;
        private PointF startPoint;
        private GraphicsPath translatePath;
        private GraphicsPath vscalPath;
        private Win32 win32;
    }
}

