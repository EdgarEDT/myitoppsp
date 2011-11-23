namespace ItopVector.Core.Types
{
    using ItopVector.Core.Interface.Figure;
    using System;
    using System.Drawing.Drawing2D;

    public class ViewBox
    {
        // Methods
        public ViewBox()
        {
            this.psr = new PreserveAspectRatio();
        }

        public ViewBox(float minx, float miny, float width, float height)
        {
            this.psr = new PreserveAspectRatio();
            this.min_x = minx;
            this.min_y = miny;
            this.width = width;
            this.height = height;
        }

        public ViewBox(float minx, float miny, float width, float height, PreserveAspectRatio PSR)
        {
            this.psr = new PreserveAspectRatio();
            this.min_x = minx;
            this.min_y = miny;
            this.width = width;
            this.height = height;
            this.psr = PSR;
        }

        public Matrix GetViewMatrix(IViewportElement element)
        {
            float single1 = element.X;
            float single2 = element.Y;
            float single3 = element.Width;
            float single4 = element.Height;
            return this.GetViewMatrix(single3, single4, single1, single2);
        }

        public Matrix GetViewMatrix(float width, float height, float x, float y)
        {
            float single5;
            float single6;
            float single7;
            float single8;
            float single11;
            float single12;
            float single13;
            ViewBox box1 = this;
            Matrix matrix1 = new Matrix();
            parAlign align1 = parAlign.none;
            parMeetOrSlice slice1 = parMeetOrSlice.slice;
            float single1 = 0f;
            float single2 = 0f;
            float single3 = 0f;
            float single4 = 0f;
            single1 = box1.width;
            single2 = box1.height;
            single3 = box1.min_x;
            single4 = box1.min_y;
            PreserveAspectRatio ratio1 = box1.psr;
            align1 = ratio1.Align;
            slice1 = ratio1.Mos;
            float single9 = width / single1;
            float single10 = height / single2;
            if (single9 >= single10)
            {
                single12 = single9;
                single13 = single10;
            }
            else
            {
                single12 = single10;
                single13 = single9;
            }
            if (slice1 == parMeetOrSlice.meet)
            {
                single11 = single13;
                single7 = single1 * single11;
                single8 = single2 * single11;
                switch (align1)
                {
                    case parAlign.xMinYMin:
                    {
                        single5 = x - single3;
                        single6 = y - single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMidYMin:
                    {
                        single5 = ((width - single7) / 2f) - single3;
                        single6 = y - single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMaxYMin:
                    {
                        single5 = (width - single7) - single3;
                        single6 = y - single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMinYMid:
                    {
                        single5 = x - single3;
                        single6 = ((height - single8) / 2f) - single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMidYMid:
                    {
                        single5 = ((width - single7) / 2f) - single3;
                        single6 = ((height - single8) / 2f) - single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMaxYMid:
                    {
                        single5 = (width - single7) - single3;
                        single6 = ((height - single8) / 2f) - single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMinYMax:
                    {
                        single5 = x - single3;
                        single6 = (height - single8) - single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMidYMax:
                    {
                        single5 = ((width - single7) / 2f) - single3;
                        single6 = (height - single8) - single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMaxYMax:
                    {
                        single5 = (width - single7) - single3;
                        single6 = (height - single8) - single4;
                        goto Label_02FF;
                    }
                }
                single5 = x - single3;
                single6 = y - single4;
                single7 = single1;
                single8 = single2;
            }
            else
            {
                single11 = single12;
                single7 = width / single11;
                single8 = height / single11;
                switch (align1)
                {
                    case parAlign.xMinYMin:
                    {
                        single5 = x + single3;
                        single6 = y + single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMidYMin:
                    {
                        single5 = ((single1 - single7) / 2f) + single3;
                        single6 = y + single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMaxYMin:
                    {
                        single5 = (single1 - single7) + single3;
                        single6 = y + single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMinYMid:
                    {
                        single5 = x + single3;
                        single6 = ((single2 - single8) / 2f) + single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMidYMid:
                    {
                        single5 = ((single1 - single7) / 2f) + single3;
                        single6 = ((single2 - single8) / 2f) + single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMaxYMid:
                    {
                        single5 = (single1 - single7) + single3;
                        single6 = ((single2 - single8) / 2f) + single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMinYMax:
                    {
                        single5 = x + single3;
                        single6 = (single2 - single8) + single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMidYMax:
                    {
                        single5 = ((single1 - single7) / 2f) + single3;
                        single6 = (single2 - single8) + single4;
                        goto Label_02FF;
                    }
                    case parAlign.xMaxYMax:
                    {
                        single5 = (single1 - single7) + single3;
                        single6 = (single2 - single8) + single4;
                        goto Label_02FF;
                    }
                }
                single5 = x + single3;
                single6 = y + single4;
                single7 = single1;
                single8 = single2;
            }
        Label_02FF:
            if ((width != 0f) && (height != 0f))
            {
                float single14 = width / single7;
                float single15 = height / single8;
                matrix1.Scale(single14, single15);
            }
            matrix1.Translate(single5 - x, single6 - y);
            return matrix1;
        }


        // Fields
        public float height;
        public float min_x;
        public float min_y;
        public PreserveAspectRatio psr;
        public float width;
    }
}

