using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using Itop.MapView;
using ItopVector.Core.Figure;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using ItopVector.Core.Document;
using System.Configuration;
using System.Drawing.Printing;
using ItopVector;
using ItopVector.Core;
using ItopVector.Core.Interface.Figure;

namespace Itop.TLPsp
{
    public class PrintHelper
    {
        private List<ClassImage> listImages = new List<ClassImage>();
        private ItopVectorControl tlVectorControl1;
        private Itop.MapView.IMapViewObj mapview;
        private PrintDocument pdoc;
        private PageSettings pageSetting;
        private float pageScale = 1;

        static public bool ShowMap = true;
        static public bool ShowCenter = true;
        static public bool ShowPolygon = true;
        public PrintHelper(ItopVectorControl tc, Itop.MapView.IMapViewObj map)
        {
            //int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
            //if (chose == 1)
            //{ map = new Itop.MapView.MapViewObj(); }
            //else if (chose == 2)
            //{ map = new Itop.MapView.MapViewObj2(); }
            tlVectorControl1 = tc;
            mapview = map;
            pdoc = new PrintDocument();
            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            pageSetting = pdoc.PrinterSettings.DefaultPageSettings;
        }
        private void CalculateScale()
        {

        }
        void drawN(Graphics g, Rectangle lei)
        {
            GraphicsContainer gc = g.BeginContainer();

            g.CompositingQuality = CompositingQuality.HighQuality;
            float s = 0f;
            //s = Convert.ToSingle(500 * 60 / scalex / 1000);
            s = 400 / 60 / (pageScale);

            float f1 = 40;

            int num1 = 1000;

            if (s * f1 > 1000)
            {
                f1 = num1 / s;
                while (f1 < 40)
                {
                    num1 += 50;
                    f1 = num1 / s;
                }
            }
            else
            {
                num1 -= 50;
                f1 = num1 / s;
                while (f1 > 45)
                {
                    num1 -= 50;
                    f1 = num1 / s;
                }
            }
            //MessageBox.Show(s + "," + f1 + "," + num1);
            Brush bkbru = Brushes.Black;

            Pen p1 = new Pen(Color.Black);
            Font ft = new Font("宋体", 15f);
            Font sft = new Font("宋体", 8f);
            //Polygon rt1 = tlVectorControl1.SVGDocument.CurrentElement as Polygon;
            //RectangleF pictureBox1 = rt1.GetBounds();

            //Rectangle pictureBox1 = pageSetting.Bounds;

            //g.FillRectangle(Brushes.White, lei.X , lei.Y + lei.Height - 37, 90, 37);
            //g.DrawRectangle(p1, lei.X, lei.Y + lei.Height-37, 90, 37);

            g.DrawString("1", ft, bkbru, new PointF(lei.X + 45, lei.Y + lei.Height - 60));
            g.DrawString(":", ft, bkbru, new PointF(lei.X + 57, lei.Y + lei.Height - 60));
            g.DrawString(Convert.ToString(Convert.ToInt32(num1) / 100), ft, bkbru, new PointF(lei.X + 69, lei.Y + lei.Height - 60));
            g.DrawString(" 万", ft, bkbru, new PointF(lei.X + 105, lei.Y + lei.Height - 60));
            //,120 40,120 100
            PointF leto = new PointF(lei.X, lei.Y);
            PointF lebo = new PointF(lei.X, lei.Y + lei.Height);
            PointF rito = new PointF(lei.X + lei.Width, lei.Y);
            PointF ribo = new PointF(lei.X + lei.Width, lei.Y + lei.Height);
            //Pen bkb = true;
            g.DrawLine(new Pen(Color.Black, 1.0f), leto, lebo);
            g.DrawLine(new Pen(Color.Black, 0.8f), leto, rito);
            g.DrawLine(new Pen(Color.Black, 1.0f), lebo, ribo);
            g.DrawLine(new Pen(Color.Black, 0.8f), rito, ribo);
            g.EndContainer(gc);
        }
        public void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {

            this.pageSetting = e.PageSettings;
            GraphPath rt1 = tlVectorControl1.SVGDocument.CurrentElement as GraphPath;
            if (rt1 == null) return;
            RectangleF rtf1 = rt1.GetBounds();
            int width = (int)Math.Round(rtf1.Width * tlVectorControl1.ScaleRatio, 0);
            int height = (int)Math.Round(rtf1.Height * tlVectorControl1.ScaleRatio, 0);
            if (width > 7000 || height > 7000)
            {
                MessageBox.Show("超出图片限定大小，请重新选定区域。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //PrinterResolution pr = e.PageSettings.PrinterResolution;
            float d1 = 0.9f;
            float d2 = 0.9f;
            int margin = 40;
            Rectangle rectangle1;
            rectangle1 = e.MarginBounds;
            //rectangle1.Width = (int)Math.Round(rectangle1.Width *d1);
            //rectangle1.Height = (int)Math.Round(rectangle1.Height * d1);
            //rectangle1.Inflate(-margin, -margin);
            Rectangle rectandle2 = e.PageBounds;
            //rectangle1.X = (int)Math.Round(rectangle1.X * d1);
            //rectangle1.Y = (int)Math.Round(rectangle1.Y * d2);
            //rectangle1.Width = (int)Math.Round(rectangle1.Width * d1);
            //rectangle1.Height = (int)Math.Round(rectangle1.Height * d2);
            float single1 = tlVectorControl1.ScaleRatio;
            Margins margins1 = this.pageSetting.Margins.Clone() as Margins;
            //margins1 = new Margins(margin, margin, margin, margin);
            //margins1.Left = rectangle1.X;
            //margins1.Top = rectangle1.Y;
            //margins1.Right = rectandle2.Width - rectangle1.X - rectangle1.Width;
            //margins1.Bottom = rectandle2.Height - rectangle1.Y - rectangle1.Height;
            //margins1.Left = (int)Math.Round(margins1.Left * d1);
            //margins1.Right = (int)Math.Round(margins1.Right * d1);
            //margins1.Top = (int)Math.Round(margins1.Top * d2);
            //margins1.Bottom = (int)Math.Round(margins1.Bottom * d2);
            float f1 = (float)rectangle1.Width / (float)width;
            float f2 = (float)rectangle1.Height / (float)height;

            float f3 = f1 > f2 ? f2 : f1;
            float f4 = 1;
            //if (f3 > 1) { f4 = f3; f3 = 1; }
            pageScale = single1 * f3;
            Graphics g = e.Graphics;

            RectangleF rtf2 = rtf1;
            rtf2.Offset(margins1.Left, margins1.Top);
            rtf2.Width -= margins1.Left + margins1.Right;
            rtf2.Height -= margins1.Top + margins1.Bottom;
            if (ShowCenter)
            {
                float f11 = (rectangle1.Width - width * f3);
                float f12 = (rectangle1.Height - height * f3);

                if (f1 > f2)
                {
                    
                    rtf1.X -= f11 / 2 / single1 / f3;
                }
                else
                {
                    
                    rtf1.Y -= f12 / 2 / single1 / f3;
                }
            }
            if (ShowCenter && ShowPolygon)
            {
                float f11 = (rectangle1.Width - width * f3);
                float f12 = (rectangle1.Height - height * f3);

                if (f1 > f2)
                {
                    rectangle1.X += (int)Math.Round(f11 / 2, 0);
                }
                else
                {
                    rectangle1.Y += (int)Math.Round(f12 / 2, 0);
                }
            }
            if (ShowPolygon)
            {
                float f11 = (rectangle1.Width - width * f3);
                float f12 = (rectangle1.Height - height * f3);
                if (f1 > f2)
                {
                    
                    rectangle1.Width -= (int)Math.Round(f11, 0);
                }
                else
                {
                    
                    rectangle1.Height -= (int)Math.Round(f12, 0);
                }
            }
            //偏移页边距
            rtf1.Offset(-margins1.Left / single1 / f3, -margins1.Top / single1 / f3);
            g.SetClip(rectangle1);//剪切区域
            //g.DrawRectangle(Pens.Blue, Rectangle.Ceiling(rtf2));
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.CompositingQuality = CompositingQuality.HighQuality;
            PointF ptf = new PointF((rtf1.X + rtf1.Right) / 2, (rtf1.Y + rtf1.Bottom) / 2);
            Point pt = new Point((int)ptf.X, (int)ptf.Y);
            //pt.X = 0;

            g.Clear(Color.White);
            if (ShowMap)
            {
                g.ScaleTransform(f3, f3);
                drawMap(g, width, height, pt);

            }
            Matrix matrix1 = new Matrix();

            matrix1.Scale(tlVectorControl1.ScaleRatio * f3, tlVectorControl1.ScaleRatio * f3);
            //matrix1.Scale(f3, f3);
            matrix1.Translate(-rtf1.X, -rtf1.Y);
            //matrix1.Translate(margins1.Left, margins1.Right);
            g.ResetTransform();
            g.Transform = matrix1;

            RenderTo(g);
            g.ResetClip();

            g.ResetTransform();
            //比例尺
            //drawN(g, rectangle1);

        }

        public void PrintPreview()
        {
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = pdoc;

            dlg.ShowDialog();
        }
        /// <summary>
        /// 导出区域图片
        /// </summary>
        public void ExportImage()
        {
            Polygon rt1 = tlVectorControl1.SVGDocument.CurrentElement as Polygon;
            if (rt1 == null) return;
            RectangleF rtf1 = rt1.GetBounds();
            int width = (int)Math.Round(rtf1.Width * tlVectorControl1.ScaleRatio, 0);
            int height = (int)Math.Round(rtf1.Height * tlVectorControl1.ScaleRatio, 0);
            if (width > 7000 || height > 7000)
            {
                MessageBox.Show("超出图片限定大小，请重新选定区域。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            System.Drawing.Image image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);

            g.SmoothingMode = SmoothingMode.HighQuality;

            g.CompositingQuality = CompositingQuality.HighQuality;
            PointF ptf = new PointF((rtf1.X + rtf1.Right) / 2, (rtf1.Y + rtf1.Bottom) / 2);
            Point pt = new Point((int)ptf.X, (int)ptf.Y);
            g.Clear(Color.White);

            if (ShowMap)
                drawMap(g, width, height, pt);
            Matrix matrix1 = new Matrix();

            matrix1.Scale(tlVectorControl1.ScaleRatio, tlVectorControl1.ScaleRatio);
            matrix1.Translate(-rtf1.X, -rtf1.Y);

            g.Transform = matrix1;
            
            RenderTo(g);          
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.RestoreDirectory = true;
            dlg.Filter = "图像文件|*.png;*.jpg;*.bmp;*.gif";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string str = Path.GetExtension(dlg.FileName);
                str = str.Substring(1);
                if (string.IsNullOrEmpty(str)) str = "Bmp";

                ImageFormat iformat = ImageFormat.Bmp;

                switch (str.ToLower())
                {
                    case "bmp":
                        iformat = ImageFormat.Bmp;
                        break;
                    case "jpeg":
                    case "jpg":
                        iformat = ImageFormat.Jpeg;
                        break;
                    case "png":
                        iformat = ImageFormat.Png;
                        break;
                    case "gif":
                        iformat = ImageFormat.Gif;
                        break;

                }

                image.Save(dlg.FileName, iformat);
            }
            image.Dispose();


        }
        public System.Drawing.Image getImage()
        {
            RectangleElement rt1 = tlVectorControl1.SVGDocument.CurrentElement as RectangleElement;
            if (rt1 == null) return null;
            RectangleF rtf1 = rt1.GetBounds();
            int width = (int)Math.Round(rtf1.Width * tlVectorControl1.ScaleRatio, 0);
            int height = (int)Math.Round(rtf1.Height * tlVectorControl1.ScaleRatio, 0);
            if (width > 7000 || height > 7000)
            {
                MessageBox.Show("超出图片限定大小，请重新选定区域。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            System.Drawing.Image image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);

            g.SmoothingMode = SmoothingMode.HighQuality;

            g.CompositingQuality = CompositingQuality.HighQuality;
            PointF ptf = new PointF((rtf1.X + rtf1.Right) / 2, (rtf1.Y + rtf1.Bottom) / 2);
            Point pt = new Point((int)ptf.X, (int)ptf.Y);
            g.Clear(Color.White);

            if (ShowMap)
                drawMap(g, width, height, pt);
            Matrix matrix1 = new Matrix();

            matrix1.Scale(tlVectorControl1.ScaleRatio, tlVectorControl1.ScaleRatio);
            matrix1.Translate(-rtf1.X, -rtf1.Y);

            g.Transform = matrix1;

            RenderTo(g);
            return image;
            //SaveFileDialog dlg = new SaveFileDialog();
            //dlg.Filter = "图像文件|*.png;*.jpg;*.bmp;*.gif";
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
                //string str = Path.GetExtension(dlg.FileName);
                //str = str.Substring(1);
                //if (string.IsNullOrEmpty(str)) str = "Bmp";

                //ImageFormat iformat = ImageFormat.Bmp;

                //switch (str.ToLower())
                //{
                //    case "bmp":
                //        iformat = ImageFormat.Bmp;
                //        break;
                //    case "jpeg":
                //    case "jpg":
                //        iformat = ImageFormat.Jpeg;
                //        break;
                //    case "png":
                //        iformat = ImageFormat.Png;
                //        break;
                //    case "gif":
                //        iformat = ImageFormat.Gif;
                //        break;

                //}

                //image.Save(dlg.FileName, iformat);
            //}
            //image.Dispose();


        }
        private void setTJhide() {
            SvgElementCollection sc = (tlVectorControl1.SVGDocument.RootElement as SVG).ChildList;
            foreach (SvgElement se in sc) {
                try {
                    (se as IGraph).DrawVisible = se.GetAttribute("print") != "no";
                } catch { }
            }
        }
        private void setTJshow() {
            SvgElementCollection sc = (tlVectorControl1.SVGDocument.RootElement as SVG).ChildList;
            foreach (SvgElement se in sc) {
                try {
                    (se as IGraph).DrawVisible = true;
                } catch { }
            }
        }
        private void RenderTo(Graphics g) {
            setTJhide();
            SvgDocument svgdoc = tlVectorControl1.SVGDocument;
            Matrix matrix1 = new Matrix();
            Matrix matrix2 = new Matrix();
            matrix1 = ((SVG)svgdoc.RootElement).GraphTransform.Matrix;
            matrix2.Multiply(matrix1);
            matrix1.Reset();
            matrix1.Multiply(g.Transform);
            g.ResetTransform();
            try
            {

                SVG svg1 = svgdoc.DocumentElement as SVG;
                svgdoc.BeginPrint = true;
                SmoothingMode mode1 = svgdoc.SmoothingMode;
                svgdoc.SmoothingMode = g.SmoothingMode;
                svg1.Draw(g, svgdoc.ControlTime);
                svgdoc.SmoothingMode = mode1;
                svgdoc.BeginPrint = false;
                
            }
            finally
            {
                g.Transform = matrix1.Clone();
                matrix1.Reset();
                matrix1.Multiply(matrix2);
                
            }
            setTJshow();
        }
        string getMapImagesUrl(int iw, int ow, int pw)
        {
            pw += 8 - 8;
            int aw = (int)Math.Ceiling((12 - pw) / 4d);
            int sw = 0, dw = 0, fw = 0;
            string gw = "";
            for (int hw = 0; hw < aw; hw++)
            {
                int jw = 1 << (4 * (aw - hw));
                int kw = ((iw - sw * fw) / jw);
                int lw = ((ow - dw * fw) / jw);
                gw += ((kw > 9) ? kw.ToString() : "0" + kw.ToString()) + "" + ((lw > 9) ? lw.ToString() : "0" + lw.ToString()) + "/";
                sw = kw;
                dw = lw;
                fw = jw;

            };
            string zw = "http://dimg.51ditu.com/";
            double xw = (((iw) & ((1 << 20) - 1)) + (((ow) & ((1 << 20) - 1)) * Math.Pow(2, 20)) + (((pw) & ((1 << 8) - 1)) * Math.Pow(2, 40)));
            return pw + "/" + gw + xw + ".png";
        }
        int[] toMapId(int[] iw, int ow)
        {
            int pw = 256 * (int)Math.Pow(2, ow);
            int aw = (int)(iw[0] / pw);
            int sw = (int)(iw[1] / pw);
            return new int[] { aw, sw, (iw[0] - aw * pw) * 200 / pw, (iw[1] - sw * pw) * 200 / pw };

        }
        public void Paint(Graphics g, int width, int height, int nScaleLevel, double fLongitude, double fLatitude, ImageAttributes imageAttributes)
        {
            double longgitude = fLongitude;
            double latitude = fLatitude;
            int scaleLevel = nScaleLevel;
            //switch (nScaleLevel)
            //{
            //    case 10:
            //        latitude -= Convert.ToDecimal(1.585394);
            //        longgitude += Convert.ToDecimal(1.385394);
            //        fLatitude-=Convert.ToDecimal(1.445394);
            //        fLongitude += Convert.ToDecimal(1.012394);
            //        break;
            //    case 9:
            //        latitude -= Convert.ToDecimal(1.585394);
            //        longgitude += Convert.ToDecimal(1.385394);
            //        fLatitude -= Convert.ToDecimal(1.440394)/2 ;
            //        fLongitude += Convert.ToDecimal(1.022394)/2;
            //        break;
            //    case 8:
            //        latitude -= Convert.ToDecimal(1.585394);
            //        longgitude += Convert.ToDecimal(1.385394);
            //        fLatitude -= Convert.ToDecimal(1.3200394)/4;
            //        fLongitude += Convert.ToDecimal(1.10739394)/4;
            //        break;
            //    case 7:
            //        latitude -= Convert.ToDecimal(1.585394);
            //        longgitude += Convert.ToDecimal(1.385394);
            //        fLatitude -= Convert.ToDecimal(1.300394)/8;
            //        fLongitude += Convert.ToDecimal(0.802394)/8;
            //        break;
            //    case 6:
            //        latitude -= Convert.ToDecimal(1.585394);
            //        longgitude += Convert.ToDecimal(1.385394);
            //        fLatitude -= Convert.ToDecimal(1.265394)/16;
            //        fLongitude += Convert.ToDecimal(0.8792394)/16;
            //        break;
            //    case 5:
            //        latitude -= Convert.ToDecimal(1.585394);
            //        longgitude += Convert.ToDecimal(1.385394);
            //        fLatitude -= Convert.ToDecimal(0.900194)/32;
            //        fLongitude += Convert.ToDecimal(1.159994)/32;
            //        break;
            //    case 4:
            //        latitude -= Convert.ToDecimal(1.585394);
            //        longgitude += Convert.ToDecimal(1.385394);
            //        fLatitude -= Convert.ToDecimal(0.300194) / 64;
            //        fLongitude += Convert.ToDecimal(1.359994) / 64;
            //        break;
            //    case 3:
            //        latitude -= Convert.ToDecimal(1.585394);
            //        longgitude += Convert.ToDecimal(1.385394);
            //        fLatitude -= -Convert.ToDecimal(1.036194) / 128;
            //        fLongitude += Convert.ToDecimal(1.692099) / 128;
            //        break;
            //    case 2:
            //        latitude -= Convert.ToDecimal(1.585394);
            //        longgitude += Convert.ToDecimal(1.385394);
            //        fLatitude -= -Convert.ToDecimal(3.4690194) / 256;
            //        fLongitude += Convert.ToDecimal(2.61009) / 256;
            //        break;
            //    case 1:
            //        latitude -= Convert.ToDecimal(1.585394);
            //        longgitude += Convert.ToDecimal(1.385394);
            //        fLatitude -= -Convert.ToDecimal(5.37394) / 512;
            //        fLongitude += Convert.ToDecimal(4.47394) / 512;
            //        break;
            //    case 0:
            //        latitude -= Convert.ToDecimal(1.585394);
            //        longgitude += Convert.ToDecimal(1.385394);
            //        fLatitude -= Convert.ToDecimal(1.440394) / 1024;
            //        fLongitude += Convert.ToDecimal(1.022394) / 1024;
            //        break;
            //}

            int[] iw = new int[] { Convert.ToInt32(fLongitude * 100000), Convert.ToInt32(fLatitude * 100000) };
            double sw = Math.Pow(2, scaleLevel) * 256 / 200;
            int[] gw = toMapId(iw, scaleLevel);
            int hw = 200;
            int jw = gw[0] - Convert.ToInt32(Math.Ceiling((width / 2d - gw[2]) / hw));
            int kw = gw[1] - Convert.ToInt32(Math.Ceiling((height / 2d - gw[3]) / hw));
            int lw = gw[0] + Convert.ToInt32(Math.Ceiling((width / 2d + gw[2]) / hw) - 1);
            int zw = gw[1] + Convert.ToInt32(Math.Ceiling((height / 2d + gw[3]) / hw) - 1);

            double[] cw = new double[] { -iw[0] / sw, iw[1] / sw };




            for (int mw = jw - 30; mw <= lw+10; mw++)
            {
                for (int _w = kw - 30; _w <= zw+10; _w++)
                {

                    int nPicLeft = (mw * 200) + Convert.ToInt32(cw[0]) + width / 2;
                    int nPicTop = (-1 - _w) * 200 + Convert.ToInt32(cw[1]) + height / 2;

                    string strPic = getMapImagesUrl(mw, _w, scaleLevel);
                    //不在显示范围内
                    //if (nPicLeft < -nPerWidth || nPicLeft > width || nPicTop > height || nPicTop < -nPerWidth) {
                    //    continue;
                    //}
                    ClassImage img = FindImage(strPic);

                    if (img == null)
                    {
                        img = mapview.DataHelper.GetImage(strPic);
                        if (img == null)
                        {
                            img = new ClassImage();
                            img.PicUrl = strPic;
                        }
                        listImages.Add(img);
                    }
                    img.IsDiscard = false;
                    img.Left = nPicLeft;
                    img.Top = nPicTop;
                    Rectangle rect = new Rectangle(img.Left, img.Top, 1000, 1000);
                    if (img.PicImage != null && g != null)
                        //g.DrawImage(img.PicImage, img.Left, img.Top, nPerWidth, nPerWidth);
                        g.DrawImage(img.PicImage, rect, 0, 0, 1000, 1000, GraphicsUnit.Pixel, imageAttributes);

                }

            }


            double f1 = 0.00582;
            double f2 = 0.00205;
            //d1 = d1 - Convert.ToDecimal(f1);
            //d2 = d2 - Convert.ToDecimal(f2);
            double loggitude1 = longgitude - Convert.ToDouble(f1);
            double latitude1 = latitude - Convert.ToDouble(f2);

            //string s = string.Format("经{0}：纬{1}", loggitude1, latitude1);
            //g.DrawString(s, new Font("宋体", 10), Brushes.Red, 0, 0);

        }
        private ClassImage FindImage(string strPic)
        {
            foreach (ClassImage img in listImages)
            {
                if (img.PicUrl == strPic)
                {
                    return img;
                }
            }
            return null;
        }
        public void drawMap(Graphics g, int width, int height, Point center)
        {
            try
            {
                int nScale = mapview.Getlevel(tlVectorControl1.ScaleRatio);
                if (nScale == -1)
                    return;
                LongLat longlat = LongLat.Empty;
                //计算中心点经纬度

                longlat = mapview.OffSet(mapview.ZeroLongLat, mapview.Getlevel(1), center.X, center.Y);
                ImageAttributes imageA = new ImageAttributes();
                longlat = mapview.OffSetZero(-(int)(center.X * tlVectorControl1.ScaleRatio), -(int)(center.Y * tlVectorControl1.ScaleRatio));

                int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
                if (chose == 1)
                {
                    g.Clear(Color.White);//ColorTranslator.FromHtml("#EBEAE8")
                    
                    Color color = ColorTranslator.FromHtml("#EBEAE8");
                    imageA.SetColorKey(color, color);
                }
                else if (chose == 2)
                {
                    
                    Color color = ColorTranslator.FromHtml("#F4F4FB");
                    Color color2 = ColorTranslator.FromHtml("#EFF0F1");//EFF0F1
                    //imageAttributes2.SetColorKey(color, color);
                    //imageAttributes2.SetColorKey(color2, color2);
                    imageA.SetColorKey(color2, color);
                }
                //imageA.
                //g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                
                    mapview.Paint(g, width, height, nScale, longlat.Longitude, longlat.Latitude, imageA);
                //绘制比例尺
                Point p1 = new Point(20, height - 30);
                Point p2 = new Point(20, height - 20);
                Point p3 = new Point(80, height - 20);
                Point p4 = new Point(80, height - 30);

                //g.DrawLines(new Pen(Color.Black, 2), new Point[4] { p1, p2, p3, p4 });
                //string str1 = string.Format("{0}公里", MapViewObj.GetMiles(nScale));
                //g.DrawString(str1, new Font("宋体", 10), Brushes.Black, 30, height - 40);

            }
            catch (Exception e1)
            {
            }

        }

    }

}
