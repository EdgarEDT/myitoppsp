using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;

namespace ItopVector.Design
{
	internal class HatchEditor : DropDownEditor
    {
        // Methods
        public HatchEditor()
        {
            this.changed = false;
        }

        private Brush GetLinearGradientBrush(Struct.Pattern pattern, Rectangle rect)
        {
            if (pattern.PatternType != PatternType.None)
            {
                if (pattern.PatternType < PatternType.Center)
                {
                	HatchStyle style1 = (HatchStyle) Enum.Parse(typeof(HatchStyle), pattern.PatternType.ToString(), false);
                    return new HatchBrush(style1, pattern.ForeColor, pattern.BackColor);
                }
                Brush brush1 = null;
                ColorBlend blend1 = new ColorBlend();
                using (GraphicsPath path1 = new GraphicsPath())
                {
                    float[] singleArray1;
                    Color[] colorArray1;
                    switch (pattern.PatternType)
                    {
                        case PatternType.Center:
                        {
                            path1.AddEllipse(rect.X, rect.Y, 30, rect.Height);
                            brush1 = new PathGradientBrush(path1);
                            singleArray1 = new float[2];
                            singleArray1[1] = 1f;
                            blend1.Positions = singleArray1;
                            colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor } ;
                            blend1.Colors = colorArray1;
                            ((PathGradientBrush) brush1).InterpolationColors = blend1;
                            ((PathGradientBrush) brush1).CenterPoint = new PointF(rect.X + (((float) rect.Width) / 2f), rect.Y + (((float) rect.Height) / 2f));
                            goto Label_0496;
                        }
                        case PatternType.DiagonalLeft:
                        {
                            brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.ForwardDiagonal);
                            singleArray1 = new float[3];
                            singleArray1[1] = 0.5f;
                            singleArray1[2] = 1f;
                            blend1.Positions = singleArray1;
                            colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor, pattern.BackColor } ;
                            blend1.Colors = colorArray1;
                            ((LinearGradientBrush) brush1).InterpolationColors = blend1;
                            goto Label_0496;
                        }
                        case PatternType.DiagonalRight:
                        {
                            brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.BackwardDiagonal);
                            singleArray1 = new float[3];
                            singleArray1[1] = 0.5f;
                            singleArray1[2] = 1f;
                            blend1.Positions = singleArray1;
                            colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor, pattern.BackColor } ;
                            blend1.Colors = colorArray1;
                            ((LinearGradientBrush) brush1).InterpolationColors = blend1;
                            goto Label_0496;
                        }
                        case PatternType.HorizontalCenter:
                        {
                            brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.Horizontal);
                            singleArray1 = new float[3];
                            singleArray1[1] = 0.5f;
                            singleArray1[2] = 1f;
                            blend1.Positions = singleArray1;
                            colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor, pattern.BackColor } ;
                            blend1.Colors = colorArray1;
                            ((LinearGradientBrush) brush1).InterpolationColors = blend1;
                            goto Label_0496;
                        }
                        case PatternType.LeftRight:
                        {
                            brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.Horizontal);
                            singleArray1 = new float[2];
                            singleArray1[1] = 1f;
                            blend1.Positions = singleArray1;
                            colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor } ;
                            blend1.Colors = colorArray1;
                            ((LinearGradientBrush) brush1).InterpolationColors = blend1;
                            goto Label_0496;
                        }
                        case PatternType.TopBottom:
                        {
                            break;
                        }
                        case PatternType.VerticalCenter:
                        {
                            brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.Vertical);
                            singleArray1 = new float[3];
                            singleArray1[1] = 0.5f;
                            singleArray1[2] = 1f;
                            blend1.Positions = singleArray1;
                            colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor, pattern.BackColor } ;
                            blend1.Colors = colorArray1;
                            ((LinearGradientBrush) brush1).InterpolationColors = blend1;
                            goto Label_0496;
                        }
                        default:
                        {
                            goto Label_0496;
                        }
                    }
                    brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.Vertical);
                    singleArray1 = new float[2];
                    singleArray1[1] = 1f;
                    blend1.Positions = singleArray1;
                    colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor } ;
                    blend1.Colors = colorArray1;
                    ((LinearGradientBrush) brush1).InterpolationColors = blend1;
                Label_0496:
                    blend1 = null;
                    return brush1;
                }
            }
            return null;
        }

        private void style1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.changed = true;
            base.editorService.CloseDropDown();
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (((context != null) && (context.Instance != null)) && (provider != null))
            {
                base.editorService = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
                if (base.editorService == null)
                {
                    return value;
                }
                ListHatchStyle style1 = new ListHatchStyle();
                if (value is Struct.Pattern)
                {
                	Struct.Pattern pattern1 = (Struct.Pattern) value;
                    style1.BackColor = pattern1.BackColor;
                    pattern1 = (Struct.Pattern) value;
                    style1.PatternType = pattern1.PatternType;
                    pattern1 = (Struct.Pattern) value;
                    style1.ForeColor = pattern1.ForeColor;
                }
                style1.Height = 150;
                style1.SelectedIndexChanged += new EventHandler(this.style1_SelectedIndexChanged);
                base.editorService.DropDownControl(style1);
                if (this.changed)
                {
                    value = new Struct.Pattern(style1.BackColor, style1.PatternType, style1.ForeColor);
                }
                this.changed = false;
            }
            return value;
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override void PaintValue(PaintValueEventArgs e)
        {
            if (e.Value is Struct.Pattern)
            {
            	Struct.Pattern pattern1 = (Struct.Pattern) e.Value;
                using (Brush brush1 = this.GetLinearGradientBrush(pattern1, e.Bounds))
                {
                    if (brush1 != null)
                    {
                        e.Graphics.RenderingOrigin = new Point(0, 0);
                        e.Graphics.FillRectangle(brush1, e.Bounds);
                    }
                    return;
                }
            }
            base.PaintValue(e);
        }


        // Fields
        private bool changed;
    }
}

