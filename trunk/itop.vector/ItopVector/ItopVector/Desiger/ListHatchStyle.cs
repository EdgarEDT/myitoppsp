namespace ItopVector.Design
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
	using ItopVector;

    internal class ListHatchStyle : ListBox
    {
        // Methods
        public ListHatchStyle()
        {
            this.stringFormate = new StringFormat(StringFormat.GenericTypographic);
            this.backColor = Color.White;
            this.foreColor = Color.Black;
            this.DrawMode = DrawMode.OwnerDrawFixed;
            base.BorderStyle = BorderStyle.None;
            this.stringFormate.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
            this.stringFormate.LineAlignment = StringAlignment.Center;
            Array array1 = Enum.GetValues(typeof(PatternType));
            foreach (object obj1 in array1)
            {
                base.Items.Add(obj1.ToString());
            }
            this.ItemHeight = 0x12;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (e.Index >= 0)
            {
                PatternType pattern1 = (PatternType) Enum.Parse(typeof(PatternType), base.Items[e.Index].ToString(), false);
                Color color1 = this.backColor;
//                DrawItemState state1 = e.State;
                Rectangle rectangle1 = e.Bounds;
                rectangle1.Inflate(-1, -1);
                rectangle1.Height--;
                if (e.State == DrawItemState.Selected)
                {
                    e.Graphics.DrawRectangle(SystemPens.Highlight, rectangle1);
                }
                if (pattern1 == PatternType.None)
                {
                    this.stringFormate.Alignment = StringAlignment.Center;
                    e.Graphics.DrawString("нч", e.Font, Brushes.Black, (RectangleF) e.Bounds, this.stringFormate);
                }
                else
                {
                    if (pattern1 < PatternType.Center)
                    {
                        this.stringFormate.Alignment = StringAlignment.Near;
                        HatchStyle style1 = (HatchStyle) Enum.Parse(typeof(HatchStyle), pattern1.ToString(), false);
                        using (Brush brush1 = new HatchBrush(style1, this.foreColor, color1))
                        {
                            rectangle1.Width = 30;
                            e.Graphics.RenderingOrigin = rectangle1.Location;
                            e.Graphics.FillRectangle(brush1, rectangle1);
                            e.Graphics.DrawRectangle(Pens.Black, rectangle1);
                            e.Graphics.DrawString(pattern1.ToString(), e.Font, Brushes.Black, new RectangleF((float) (rectangle1.Right + 6), (float) rectangle1.Y, (float) e.Bounds.Width, (float) rectangle1.Height), this.stringFormate);
                            return;
                        }
                    }
                    this.stringFormate.Alignment = StringAlignment.Near;
                    using (GraphicsPath path1 = new GraphicsPath())
                    {
                        float[] singleArray1;
                        Color[] colorArray1;
                        switch (pattern1)
                        {
                            case PatternType.Center:
                            {
                                break;
                            }
                            case PatternType.DiagonalLeft:
                            {
                                goto Label_05AC;
                            }
                            case PatternType.DiagonalRight:
                            {
                                goto Label_06D0;
                            }
                            case PatternType.HorizontalCenter:
                            {
                                goto Label_0488;
                            }
                            case PatternType.LeftRight:
                            {
                                goto Label_07F4;
                            }
                            case PatternType.TopBottom:
                            {
                                goto Label_08FC;
                            }
                            case PatternType.VerticalCenter:
                            {
                                goto Label_0364;
                            }
                            default:
                            {
                                return;
                            }
                        }
                        path1.AddEllipse(rectangle1.X, rectangle1.Y, 30, rectangle1.Height);
                        using (PathGradientBrush brush2 = new PathGradientBrush(path1))
                        {
                            rectangle1.Width = 30;
                            ColorBlend blend1 = new ColorBlend();
                            singleArray1 = new float[2];
                            singleArray1[1] = 1f;
                            blend1.Positions = singleArray1;
                            colorArray1 = new Color[] { this.backColor, this.foreColor } ;
                            blend1.Colors = colorArray1;
                            brush2.InterpolationColors = blend1;
                            brush2.CenterPoint = new PointF(rectangle1.X + (((float) rectangle1.Width) / 2f), rectangle1.Y + (((float) rectangle1.Height) / 2f));
                            e.Graphics.FillRectangle(new SolidBrush(this.backColor), rectangle1);
                            e.Graphics.FillRectangle(brush2, rectangle1);
                            e.Graphics.DrawRectangle(Pens.Black, rectangle1);
                            blend1 = null;
                            e.Graphics.DrawString(pattern1.ToString(), e.Font, Brushes.Black, new RectangleF((float) (rectangle1.Right + 6), (float) rectangle1.Y, (float) e.Bounds.Width, (float) rectangle1.Height), this.stringFormate);
                            return;
                        }
                    Label_0364:
                        rectangle1.Width = 30;
                        using (LinearGradientBrush brush3 = new LinearGradientBrush(rectangle1, this.backColor, this.foreColor, LinearGradientMode.Vertical))
                        {
                            ColorBlend blend2 = new ColorBlend();
                            singleArray1 = new float[3];
                            singleArray1[1] = 0.5f;
                            singleArray1[2] = 1f;
                            blend2.Positions = singleArray1;
                            colorArray1 = new Color[] { this.backColor, this.foreColor, this.backColor } ;
                            blend2.Colors = colorArray1;
                            brush3.InterpolationColors = blend2;
                            e.Graphics.FillRectangle(brush3, rectangle1);
                            e.Graphics.DrawRectangle(Pens.Black, rectangle1);
                            blend2 = null;
                            e.Graphics.DrawString(pattern1.ToString(), e.Font, Brushes.Black, new RectangleF((float) (rectangle1.Right + 6), (float) rectangle1.Y, (float) e.Bounds.Width, (float) rectangle1.Height), this.stringFormate);
                            return;
                        }
                    Label_0488:
                        rectangle1.Width = 30;
                        using (LinearGradientBrush brush4 = new LinearGradientBrush(rectangle1, this.backColor, this.foreColor, LinearGradientMode.Horizontal))
                        {
                            ColorBlend blend3 = new ColorBlend();
                            singleArray1 = new float[3];
                            singleArray1[1] = 0.5f;
                            singleArray1[2] = 1f;
                            blend3.Positions = singleArray1;
                            colorArray1 = new Color[] { this.backColor, this.foreColor, this.backColor } ;
                            blend3.Colors = colorArray1;
                            brush4.InterpolationColors = blend3;
                            e.Graphics.FillRectangle(brush4, rectangle1);
                            e.Graphics.DrawRectangle(Pens.Black, rectangle1);
                            blend3 = null;
                            e.Graphics.DrawString(pattern1.ToString(), e.Font, Brushes.Black, new RectangleF((float) (rectangle1.Right + 6), (float) rectangle1.Y, (float) e.Bounds.Width, (float) rectangle1.Height), this.stringFormate);
                            return;
                        }
                    Label_05AC:
                        rectangle1.Width = 30;
                        using (LinearGradientBrush brush5 = new LinearGradientBrush(rectangle1, this.backColor, this.foreColor, LinearGradientMode.ForwardDiagonal))
                        {
                            ColorBlend blend4 = new ColorBlend();
                            singleArray1 = new float[3];
                            singleArray1[1] = 0.5f;
                            singleArray1[2] = 1f;
                            blend4.Positions = singleArray1;
                            colorArray1 = new Color[] { this.backColor, this.foreColor, this.backColor } ;
                            blend4.Colors = colorArray1;
                            brush5.InterpolationColors = blend4;
                            e.Graphics.FillRectangle(brush5, rectangle1);
                            e.Graphics.DrawRectangle(Pens.Black, rectangle1);
                            blend4 = null;
                            e.Graphics.DrawString(pattern1.ToString(), e.Font, Brushes.Black, new RectangleF((float) (rectangle1.Right + 6), (float) rectangle1.Y, (float) e.Bounds.Width, (float) rectangle1.Height), this.stringFormate);
                            return;
                        }
                    Label_06D0:
                        rectangle1.Width = 30;
                        using (LinearGradientBrush brush6 = new LinearGradientBrush(rectangle1, this.backColor, this.foreColor, LinearGradientMode.BackwardDiagonal))
                        {
                            ColorBlend blend5 = new ColorBlend();
                            singleArray1 = new float[3];
                            singleArray1[1] = 0.5f;
                            singleArray1[2] = 1f;
                            blend5.Positions = singleArray1;
                            colorArray1 = new Color[] { this.backColor, this.foreColor, this.backColor } ;
                            blend5.Colors = colorArray1;
                            brush6.InterpolationColors = blend5;
                            e.Graphics.FillRectangle(brush6, rectangle1);
                            e.Graphics.DrawRectangle(Pens.Black, rectangle1);
                            blend5 = null;
                            e.Graphics.DrawString(pattern1.ToString(), e.Font, Brushes.Black, new RectangleF((float) (rectangle1.Right + 6), (float) rectangle1.Y, (float) e.Bounds.Width, (float) rectangle1.Height), this.stringFormate);
                            return;
                        }
                    Label_07F4:
                        rectangle1.Width = 30;
                        using (LinearGradientBrush brush7 = new LinearGradientBrush(rectangle1, this.backColor, this.foreColor, LinearGradientMode.Horizontal))
                        {
                            ColorBlend blend6 = new ColorBlend();
                            singleArray1 = new float[2];
                            singleArray1[1] = 1f;
                            blend6.Positions = singleArray1;
                            colorArray1 = new Color[] { this.backColor, this.foreColor } ;
                            blend6.Colors = colorArray1;
                            brush7.InterpolationColors = blend6;
                            e.Graphics.FillRectangle(brush7, rectangle1);
                            e.Graphics.DrawRectangle(Pens.Black, rectangle1);
                            blend6 = null;
                            e.Graphics.DrawString(pattern1.ToString(), e.Font, Brushes.Black, new RectangleF((float) (rectangle1.Right + 6), (float) rectangle1.Y, (float) e.Bounds.Width, (float) rectangle1.Height), this.stringFormate);
                            return;
                        }
                    Label_08FC:
                        rectangle1.Width = 30;
                        using (LinearGradientBrush brush8 = new LinearGradientBrush(rectangle1, this.backColor, this.foreColor, LinearGradientMode.Vertical))
                        {
                            ColorBlend blend7 = new ColorBlend();
                            singleArray1 = new float[2];
                            singleArray1[1] = 1f;
                            blend7.Positions = singleArray1;
                            colorArray1 = new Color[] { this.backColor, this.foreColor } ;
                            blend7.Colors = colorArray1;
                            brush8.InterpolationColors = blend7;
                            e.Graphics.FillRectangle(brush8, rectangle1);
                            e.Graphics.DrawRectangle(Pens.Black, rectangle1);
                            blend7 = null;
                            e.Graphics.DrawString(pattern1.ToString(), e.Font, Brushes.Black, new RectangleF((float) (rectangle1.Right + 6), (float) rectangle1.Y, (float) e.Bounds.Width, (float) rectangle1.Height), this.stringFormate);
                        }
                    }
                }
            }
        }


        // Properties
        public new Color BackColor
        {
            get
            {
                return this.backColor;
            }
            set
            {
                if (this.backColor != value)
                {
                    this.backColor = value;
                    base.Invalidate();
                }
            }
        }

        public new  Color ForeColor
        {
            get
            {
                return this.foreColor;
            }
            set
            {
                this.foreColor = value;
            }
        }

        public PatternType PatternType
        {
            get
            {
                string text1 = base.SelectedItem.ToString();
                PatternType pattern1 = PatternType.None;
                try
                {
                    pattern1 = (PatternType) Enum.Parse(typeof(PatternType), text1, false);
                }
                catch
                {
                }
                return pattern1;
            }
            set
            {
                int num1 = base.FindString(value.ToString());
                if (num1 >= 0)
                {
                    this.SelectedIndex = num1;
                }
            }
        }


        // Fields
        private Color backColor;
        private Color foreColor;

        private StringFormat stringFormate;
    }
}

