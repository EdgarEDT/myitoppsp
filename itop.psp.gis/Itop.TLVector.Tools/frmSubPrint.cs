using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using ItopVector.Core.Func;
using Itop.Client.Base;
namespace ItopVector.Tools {
    public partial class frmSubPrint : FormBase {
        private SvgDocument svgdoc;
        private PageSettings pageSetting;
        private int margin = 10;
        private Point pos;
        private float scalex;
        private float scaley;
        RectangleF rect = new RectangleF();
        PrintInfo pri;
        string zt;
        string gs;
        ArrayList idlist;
        ArrayList symlist;

        Font ft = new Font("宋体", 11f);
        Font sft = new Font("宋体", 8f);
        SolidBrush bkbru = new SolidBrush(Color.Black);
        Pen p1 = new Pen(Color.Black);
        Bitmap b = new Bitmap(1654, 1169);
        ItopVectorControl vector;

        public ItopVectorControl Vector {
            get { return vector; }
            set { vector = value; }
        }

        public SvgDocument Svgdoc {
            get { return svgdoc; }
            set { svgdoc = value; }
        }
        public void InitImg(string _zt, string _gs, PrintInfo _pri, ArrayList _idlist, ArrayList _symlist) {
            zt = _zt;
            gs = _gs;
            pri = _pri;
            idlist = _idlist;
            symlist = _symlist;
            //if(gs=="横向"){
            //this.pictureBox1.Size = new System.Drawing.Size(1191, 842);
            //this.pictureBox1.Size = new System.Drawing.Size(1488, 1052);
            this.pictureBox1.Size = new System.Drawing.Size(1610, 1122);
            //}
            //if(gs=="纵向"){
            //    this.pictureBox1.Size = new System.Drawing.Size(842, 1191);
            //}

        }
        public frmSubPrint() {
            InitializeComponent();
            printDocument1.BeginPrint += new PrintEventHandler(printDocument1_BeginPrint);
            printDocument1.QueryPageSettings += new QueryPageSettingsEventHandler(printDocument1_QueryPageSettings);
            this.pos = Point.Empty;

        }

        void printDocument1_QueryPageSettings(object sender, QueryPageSettingsEventArgs e) {
            
        }

        void printDocument1_BeginPrint(object sender, PrintEventArgs e) {
            
            
        }
        float viewscale = .85f;
        public void Draw(Graphics g1) {
            g1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            RectangleF f = svgdoc.RootElement.ViewPort;
            float offx = Convert.ToSingle(f.Width * viewscale);
            float offy = Convert.ToSingle(f.Height * viewscale);

            RectangleF rectangle1 = new RectangleF();
            //PictureBox label1 = sender as PictureBox;

            //			if(vectorControl.FullDrawMode==true)
            //			{
            //				rectangle1 = vectorControl.Bounds;
            //			}
            //			else
            //			{
            //rectangle1 = this.pageSetting.Bounds;
            //			}
            g1.FillRectangle(Brushes.White, 0, 0, 1610, 1169);
            rectangle1.X = 0f;
            rectangle1.Y = 0f;
            rectangle1.Width = (float)pictureBox1.Width;// 1654f;
            rectangle1.Height = (float)pictureBox1.Height;// 1169f;

            //111
            //Pen pen11 = new Pen(Color.Blue, 1f);
            //pen11.Color = Color.Black;
            //Pen pt = new Pen(Color.Black);
            //pt.Width = 3;

            //g1.DrawRectangle(pen11, (float)(10), (float)(10), pictureBox1.Width - 16, pictureBox1.Height - 16);
            //g1.DrawRectangle(pt, (float)(25), (float)(20), pictureBox1.Width - 36, pictureBox1.Height - 36);
            //g1.DrawRectangle(pen11, (float)(25), (float)(20), pictureBox1.Width - 36, pictureBox1.Height - 36 - 122);
            //g1.DrawLine(pen11, pictureBox1.Width - 300, (float)(20), pictureBox1.Width - 300, (float)(-this.pos.X + 95));


            int num1 = pictureBox1.Width;
            float single1 = ((float)(pictureBox1.Height - (2 * this.margin))) / ((float)rectangle1.Height);//y缩放
            float single2 = ((float)(num1 - (2 * this.margin))) / ((float)rectangle1.Width);//x缩放
            Rectangle rectangle2 = new Rectangle(this.margin, this.margin, (int)(single2 * rectangle1.Width), (int)(single1 * rectangle1.Height));
            g1.FillRectangle(Brushes.White, rectangle2);
            //e.Graphics.DrawRectangle(Pens.Black, rectangle2);

            Margins margins1 = new Margins();//this.pageSetting.Margins.Clone() as Margins;
            margins1.Left = 10;// (int)(margins1.Left * .9f);
            margins1.Right = 10;// (int)(margins1.Right * .9f);
            margins1.Top = 10;// (int)(margins1.Top * .9f);
            margins1.Bottom = 10;// (int)(margins1.Bottom * .9f);

            int num3 = this.margin + ((int)(single2 * margins1.Left));
            int num4 = this.margin + ((int)(single1 * margins1.Top));
            num1 = (int)(((single2 * rectangle1.Width) - (single2 * margins1.Left)) - (single2 * margins1.Right));
            int num2 = (int)(((single1 * rectangle1.Height) - (single1 * margins1.Top)) - (single1 * margins1.Bottom));
            rectangle2 = new Rectangle(num3, num4, num1, num2);
            using (Pen pen1 = new Pen(Color.Blue, 1f)) {
                g1.SmoothingMode = SmoothingMode.HighQuality;

                SizeF size1 = new SizeF(Convert.ToSingle(svgdoc.RootElement.GetAttribute("width")), Convert.ToSingle(svgdoc.RootElement.GetAttribute("height")));//svgdoc.DocumentSize;

                PointF offset;

                offset = new PointF(this.margin, this.margin);
                g1.SetClip(new RectangleF(-this.pos.X + offset.X + 25, 105, pictureBox1.Width - 36, pictureBox1.Height - 255));

                g1.TranslateTransform((float)-this.pos.X, (float)-this.pos.Y);

                g1.TranslateTransform(offset.X, offset.Y);

                g1.ScaleTransform(Convert.ToSingle(scalex * viewscale), Convert.ToSingle(scaley * viewscale));

                g1.TranslateTransform(-rect.Left, -rect.Top);

                g1.TranslateTransform(200 / scalex, 150 / scaley);
                //if (offx < 1610)
                //{
                //    g1.TranslateTransform((1610-offx)/ scalex, 0);
                //}
                //if(offy<1170){
                //    g1.TranslateTransform(0, (1170-offy)/scaley);
                //}

                this.RenderTo(g1);

                g1.ResetTransform();
                g1.ResetClip();
                float[] singleArray1 = new float[] { 2f, 2f };
                //pen1.DashPattern = singleArray1;

                pen1.Color = Color.Black;
                Pen pt = new Pen(Color.Black);
                pt.Width = 3;

                g1.DrawRectangle(pen1, (float)(-this.pos.X + offset.X + 20), (float)(-this.pos.Y + offset.Y - 5), pictureBox1.Width - 26, pictureBox1.Height - 26);
                g1.DrawRectangle(pt, (float)(-this.pos.X + offset.X + 25), (float)(-this.pos.Y + offset.Y), pictureBox1.Width - 36, pictureBox1.Height - 36);
                g1.DrawRectangle(pen1, (float)(-this.pos.X + offset.X + 25), (float)(-this.pos.Y + offset.Y), pictureBox1.Width - 36, pictureBox1.Height - 36 - 122);
                g1.DrawLine(pen1, pictureBox1.Width - 300, (float)(-this.pos.X + offset.X), pictureBox1.Width - 300, (float)(-this.pos.X + offset.X + 95));

            }
            if (zt == "区域打印") {
                g1.DrawString("区域打印", new Font("宋体",30), new SolidBrush(Color.Red), 200, 200);
            }
            if (zt == "负荷密度主题" || zt == "用地规划图") {
                InitTK(g1);
            }
            if (zt == "地理接线图") {
                InitLineTK(g1);
            }
            if (zt == "地理接线规划图") {
                InitSymTK(g1);
            }
            if (zt == "负荷分布图") {
                InitFHTK(g1);
            }
            if (zt == "供电范围图") {
                InitGHTK(g1);
            }
            if (zt == "低压地理接线图") {
                InitDYTK(g1);
            }
            if (b != null) {
                pictureBox1.Image = b;
            }
        }

        public void Open(SvgDocument _doc, RectangleF _rect) {
            svgdoc = _doc;
            //if (symlist.Count > 0)
            //{
            //    for (int i = 0; i < symlist.Count; i++)
            //    {
            //        XmlElement u1 = svgdoc.CreateElement("use") as Use;
            //        PointF off = TLMath.getUseOffset((string)symlist[i]);
            //        Point point1 = vector.PointToView(new Point(500, 500));
            //        //Point point1 =// tlVectorControl1.PointToView(new Point(e.Mouse.X, e.Mouse.Y));
            //        u1.SetAttribute("xlink:href", (string)symlist[i]);
            //        u1.SetAttribute("x", "790");
            //        u1.SetAttribute("y", "600");
            //        u1.SetAttribute("layer", SvgDocument.currentLayer);
            //        u1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
            //        svgdoc.RootElement.AppendChild(u1);
            //    }
            //}
            rect = _rect;
            CalculateScale();
        }
        private void frmSubPrint_Load(object sender, EventArgs e) {

            Graphics g1 = Graphics.FromImage(b);
            Draw(g1);
            //pictureBox1.Image = b;
        }
        private void RenderTo(Graphics g) {
            Matrix matrix1 = new Matrix();
            Matrix matrix2 = new Matrix();
            matrix1 = ((SVG)svgdoc.RootElement).GraphTransform.Matrix;
            matrix2.Multiply(matrix1);
            matrix1.Reset();
            matrix1.Multiply(g.Transform);
            g.ResetTransform();
            try {

                //this.vectorControl.RenderTo(g);
                //if ((g != null) && ((this.svgDocument != null) && (this.svgDocument.DocumentElement != null)))

                SVG svg1 = svgdoc.DocumentElement as SVG;
                svgdoc.BeginPrint = true;
                SmoothingMode mode1 = svgdoc.SmoothingMode;
                svgdoc.SmoothingMode = g.SmoothingMode;
                svg1.Draw(g, svgdoc.ControlTime);
                svgdoc.SmoothingMode = mode1;
                svgdoc.BeginPrint = false;
            } finally {
                g.Transform = matrix1.Clone();
                matrix1.Reset();
                matrix1.Multiply(matrix2);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            //RectangleF rectangle1=new RectangleF();
            //PictureBox label1 = sender as PictureBox;

            ////			if(vectorControl.FullDrawMode==true)
            ////			{
            ////				rectangle1 = vectorControl.Bounds;
            ////			}
            ////			else
            ////			{
            ////rectangle1 = this.pageSetting.Bounds;
            ////			}
            //rectangle1.X = 0f;
            //rectangle1.Y = 0f;
            //rectangle1.Width =(float) pictureBox1.Width;// 1654f;
            //rectangle1.Height = (float)pictureBox1.Height;// 1169f;
            //int num1 = label1.Width;
            //float single1 = ((float)(label1.Height - (2 * this.margin))) / ((float)rectangle1.Height);//y缩放
            //float single2 = ((float)(num1 - (2 * this.margin))) / ((float)rectangle1.Width);//x缩放
            //Rectangle rectangle2 = new Rectangle(this.margin, this.margin, (int)(single2 * rectangle1.Width), (int)(single1 * rectangle1.Height));
            //e.Graphics.FillRectangle(Brushes.White, rectangle2);
            ////e.Graphics.DrawRectangle(Pens.Black, rectangle2);

            //Margins margins1 = new Margins();//this.pageSetting.Margins.Clone() as Margins;
            //margins1.Left = 10;// (int)(margins1.Left * .9f);
            //margins1.Right = 10;// (int)(margins1.Right * .9f);
            //margins1.Top = 10;// (int)(margins1.Top * .9f);
            //margins1.Bottom = 10;// (int)(margins1.Bottom * .9f);

            //int num3 = this.margin + ((int)(single2 * margins1.Left));
            //int num4 = this.margin + ((int)(single1 * margins1.Top));
            //num1 = (int)(((single2 * rectangle1.Width) - (single2 * margins1.Left)) - (single2 * margins1.Right));
            //int num2 = (int)(((single1 * rectangle1.Height) - (single1 * margins1.Top)) - (single1 * margins1.Bottom));
            //rectangle2 = new Rectangle(num3, num4, num1, num2);
            //using (Pen pen1 = new Pen(Color.Blue, 1f))
            //{
            //    e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            //    //if (this.rdoHighSpeed.Checked)
            //    //{
            //    //    e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            //    //}
            //    SizeF size1 = new SizeF(Convert.ToSingle(svgdoc.RootElement.GetAttribute("width")), Convert.ToSingle(svgdoc.RootElement.GetAttribute("height")));//svgdoc.DocumentSize;

            //    PointF offset;
            //    //if (this.radioFit.Checked)
            //    //{
            //    //    offset = new PointF(rectangle2.X, rectangle2.Y);
            //    //}
            //    //else
            //    //{
            //        offset = new PointF(this.margin, this.margin);
            //    //}

            //    e.Graphics.TranslateTransform((float)-this.pos.X, (float)-this.pos.Y);

            //    e.Graphics.TranslateTransform(offset.X, offset.Y);

            //    e.Graphics.ScaleTransform(this.scalex, this.scaley);

            //    e.Graphics.TranslateTransform(-rect.Left, -rect.Top);
            //    this.RenderTo(e.Graphics);

            //    e.Graphics.ResetTransform();
            //    e.Graphics.ResetClip();
            //    float[] singleArray1 = new float[] { 2f, 2f };
            //    //pen1.DashPattern = singleArray1;
            //    pen1.Color = Color.Black;

            //    e.Graphics.DrawRectangle(pen1,(float)(-this.pos.X + offset.X), (float)(-this.pos.Y + offset.Y),pictureBox1.Width-26,pictureBox1.Height-36);
            //    e.Graphics.DrawRectangle(pen1, (float)(-this.pos.X + offset.X), (float)(-this.pos.Y + offset.Y), pictureBox1.Width - 26, pictureBox1.Height - 36-122);
            //    //e.Graphics.DrawRectangle(pen1, (float)(-this.pos.X + offset.X), (float)(-this.pos.Y + offset.Y), (float)(size1.Width * this.scalex), (float)(size1.Height * this.scaley));
            //    //e.Graphics.DrawRectangle(pen1, (float)(-this.pos.X + offset.X), (float)(-this.pos.Y + offset.Y), (float)(size1.Width * this.scalex), (float)(size1.Height * this.scaley+123f));

            //}
            //InitTK(e.Graphics);



        }
        private void CalculateScale() {
            RectangleF rectangle1 = new RectangleF();

            rectangle1.X = 0f;
            rectangle1.Y = 0f;
            rectangle1.Width = (float)pictureBox1.Width;//1654f;
            rectangle1.Height = (float)pictureBox1.Height;// 1169f;

            int num1 = this.pictureBox1.Width;
            int num2 = this.pictureBox1.Height - 120;
            this.scaley = ((float)(num2 - (2 * this.margin))) / ((float)rectangle1.Height);
            this.scalex = ((float)(num1 - (2 * this.margin))) / ((float)rectangle1.Width);
            Margins margins1 = new Margins();//this.pageSetting.Margins.Clone() as Margins;
            margins1.Left = 10;//(int)(margins1.Left * .9f);
            margins1.Right = 10;// (int)(margins1.Right * .9f);
            margins1.Top = 10;//(int)(margins1.Top * .9f);
            margins1.Bottom = 10;//(int)(margins1.Bottom * .9f);

            int num3 = this.margin + ((int)(this.scalex * margins1.Left));
            int num4 = this.margin + ((int)(this.scaley * margins1.Top));
            num1 = (int)(((this.scalex * rectangle1.Width) - (this.scalex * margins1.Left)) - (this.scalex * margins1.Right));
            num2 = (int)(((this.scaley * rectangle1.Height) - (this.scaley * margins1.Top)) - (this.scaley * margins1.Bottom));
            Rectangle rectangle2 = new Rectangle(num3, num4, num1, num2);
            //SizeF size1 = this.vectorControl.DocumentSize;
            SizeF size1 = new SizeF(Convert.ToSingle(svgdoc.RootElement.GetAttribute("width")), Convert.ToSingle(svgdoc.RootElement.GetAttribute("height")));
            //if (this.radioFit.Checked)
            //{
            this.scalex = ((float)rectangle2.Width) / ((float)size1.Width);
            this.scaley = ((float)rectangle2.Height) / ((float)size1.Height);
            if (scalex > scaley) {
                scalex = scaley;

            } else {
                scaley = scalex;
            }
            //}

        }
        public void DrawOther(Graphics g) {
            //g.FillRectangle
            g.DrawLine(p1, new Point(pictureBox1.Width - 540, pictureBox1.Height - 146), new Point(pictureBox1.Width - 540, pictureBox1.Height - 26));
            g.DrawLine(p1, new Point(pictureBox1.Width - 330, pictureBox1.Height - 146), new Point(pictureBox1.Width - 330, pictureBox1.Height - 26));
            g.DrawLine(p1, new Point(pictureBox1.Width - 270, pictureBox1.Height - 146), new Point(pictureBox1.Width - 270, pictureBox1.Height - 26));

            g.DrawLine(p1, new Point(pictureBox1.Width - 330, pictureBox1.Height - 47), new Point(pictureBox1.Width, pictureBox1.Height - 47));
            g.DrawLine(p1, new Point(pictureBox1.Width - 330, pictureBox1.Height - 73), new Point(pictureBox1.Width, pictureBox1.Height - 73));
            g.DrawLine(p1, new Point(pictureBox1.Width - 330, pictureBox1.Height - 99), new Point(pictureBox1.Width, pictureBox1.Height - 99));
            g.DrawLine(p1, new Point(pictureBox1.Width - 330, pictureBox1.Height - 125), new Point(pictureBox1.Width, pictureBox1.Height - 125));

            g.DrawString("规划设计", ft, bkbru, new PointF(pictureBox1.Width - 535, pictureBox1.Height - 140));
            g.DrawString("", ft, bkbru, new PointF(pictureBox1.Width - 535, pictureBox1.Height - 110));//哈尔滨通力软件开发有限公司
            g.DrawString("委托单位", ft, bkbru, new PointF(pictureBox1.Width - 535, pictureBox1.Height - 75));
            g.DrawString("", ft, bkbru, new PointF(pictureBox1.Width - 535, pictureBox1.Height - 50));//哈尔滨第二电业局

            g.DrawString("工程名称", sft, bkbru, new PointF(pictureBox1.Width - 325, pictureBox1.Height - 138));
            g.DrawString("图名", sft, bkbru, new PointF(pictureBox1.Width - 320, pictureBox1.Height - 115));
            g.DrawString("批准", sft, bkbru, new PointF(pictureBox1.Width - 320, pictureBox1.Height - 88));
            g.DrawString("审核", sft, bkbru, new PointF(pictureBox1.Width - 320, pictureBox1.Height - 63));
            g.DrawString("日期", sft, bkbru, new PointF(pictureBox1.Width - 320, pictureBox1.Height - 40));

            g.DrawString(pri.Col1, sft, bkbru, new PointF(pictureBox1.Width - 320 + 55, pictureBox1.Height - 138));
            StringFormat format1 = new StringFormat();
            //format1.Alignment = StringAlignment.Center;
            format1.LineAlignment = StringAlignment.Center;
            g.DrawString(pri.Col2, sft, bkbru, new RectangleF( pictureBox1.Width - 320 + 55, pictureBox1.Height - 122,260,24),format1);
            g.DrawString(pri.Col3, sft, bkbru, new PointF(pictureBox1.Width - 320 + 55, pictureBox1.Height - 86));
            g.DrawString(pri.Col4, sft, bkbru, new PointF(pictureBox1.Width - 320 + 55, pictureBox1.Height - 63));
            g.DrawString(pri.Col9, sft, bkbru, new PointF(pictureBox1.Width - 320 + 55, pictureBox1.Height - 40));




            g.DrawLine(p1, new Point(pictureBox1.Width - 220, pictureBox1.Height - 47), new Point(pictureBox1.Width - 220, pictureBox1.Height - 99));
            g.DrawLine(p1, new Point(pictureBox1.Width - 170, pictureBox1.Height - 47), new Point(pictureBox1.Width - 170, pictureBox1.Height - 99));
            g.DrawLine(p1, new Point(pictureBox1.Width - 120, pictureBox1.Height - 26), new Point(pictureBox1.Width - 120, pictureBox1.Height - 99));
            g.DrawLine(p1, new Point(pictureBox1.Width - 70, pictureBox1.Height - 47), new Point(pictureBox1.Width - 70, pictureBox1.Height - 99));
            g.DrawLine(p1, new Point(pictureBox1.Width - 180, pictureBox1.Height - 47), new Point(pictureBox1.Width - 180, pictureBox1.Height - 26));

            g.DrawString("校核", sft, bkbru, new PointF(pictureBox1.Width - 210, pictureBox1.Height - 88));
            g.DrawString("设计", sft, bkbru, new PointF(pictureBox1.Width - 210, pictureBox1.Height - 63));
            g.DrawString("项目编号", sft, bkbru, new PointF(pictureBox1.Width - 120, pictureBox1.Height - 88));
            g.DrawString("图号", sft, bkbru, new PointF(pictureBox1.Width - 110, pictureBox1.Height - 63));
            g.DrawString("项目负责人", sft, bkbru, new PointF(pictureBox1.Width - 180, pictureBox1.Height - 40));

            g.DrawString(pri.Col5, sft, bkbru, new PointF(pictureBox1.Width - 210 + 45, pictureBox1.Height - 88));
            g.DrawString(pri.Col6, sft, bkbru, new PointF(pictureBox1.Width - 210 + 45, pictureBox1.Height - 60));
            g.DrawString(pri.Col7, sft, bkbru, new PointF(pictureBox1.Width - 115 + 45, pictureBox1.Height - 88));
            g.DrawString(pri.Col8, sft, bkbru, new PointF(pictureBox1.Width - 115 + 45, pictureBox1.Height - 60));
            g.DrawString(pri.Col10, sft, bkbru, new PointF(pictureBox1.Width - 170 + 50, pictureBox1.Height - 38));

            float s = 0f;
            //s = Convert.ToSingle(500 * 60 / scalex / 1000);
            s = 400 / 60 / (scalex * viewscale);

            float f1 = 40;

            int num1 = 1000;

            if (s * f1 > 1000) {
                f1 = num1 / s;
                while (f1 < 40) {
                    num1 += 50;
                    f1 = num1 / s;
                }
            } else {
                num1 -= 50;
                f1 = num1 / s;
                while ( f1 > 45) {
                    num1 -= 50;
                    f1 = num1 / s;
                }
            }
            //MessageBox.Show(s + "," + f1 + "," + num1);
            g.FillRectangle(bkbru, pictureBox1.Width - 240, 80f, f1, 5);
            g.DrawRectangle(p1, pictureBox1.Width - 240 + f1 * 1, 80f, f1, 5);
            g.FillRectangle(bkbru, pictureBox1.Width - 240 + f1 * 2, 80f, f1, 5);
            g.DrawRectangle(p1, pictureBox1.Width - 240 + f1 * 3, 80f, f1, 5);

            g.DrawRectangle(p1, pictureBox1.Width - 240, 85f, f1, 5);
            g.FillRectangle(bkbru, pictureBox1.Width - 240 + f1 * 1, 85f, f1, 5);
            g.DrawRectangle(p1, pictureBox1.Width - 240 + f1 * 2, 85f, f1, 5);
            g.FillRectangle(bkbru, pictureBox1.Width - 240 + f1 * 3, 85f, f1, 5);

            g.DrawString("0", ft, bkbru, new PointF(pictureBox1.Width - 280 + 40, 90f));
            g.DrawString(Convert.ToString(Convert.ToInt32(num1)) + "m", ft, bkbru, new PointF(pictureBox1.Width - 240 + f1, 90f));
            g.DrawString(Convert.ToString(Convert.ToInt32(num1 * 4)) + "m", ft, bkbru, new PointF(pictureBox1.Width - 240 + f1 * 3 + 20, 90f));
            //,120 40,120 100
            PointF[] pt1 = new PointF[3];
            PointF[] pt2 = new PointF[3];

            pt1[0] = new PointF(pictureBox1.Width - 280 + f1 * 2 + 20, 70);
            pt1[1] = new PointF(pictureBox1.Width - 260 + f1 * 2 + 20, 30);
            pt1[2] = new PointF(pictureBox1.Width - 260 + f1 * 2 + 20, 50);
            g.FillPolygon(bkbru, pt1);

            pt2[0] = new PointF(pictureBox1.Width - 260 + f1 * 2 + 20, 30);
            pt2[1] = new PointF(pictureBox1.Width - 260 + f1 * 2 + 20, 50);
            pt2[2] = new PointF(pictureBox1.Width - 240 + f1 * 2 + 20, 70);
            g.DrawPolygon(p1, pt2);

            Pen p2 = new Pen(Color.Green);
            g.DrawEllipse(p2, pictureBox1.Width - 272 + f1 * 2 + 20, 35, 25, 25);

            g.DrawString("N", ft, bkbru, pictureBox1.Width - 268 + f1 * 2 + 20, 15);
            g.DrawLine(p1, 37, 105, pictureBox1.Width, 105);  //顶部横线

            Font f2 = new Font("宋体", 26, FontStyle.Bold);

            g.DrawString(pri.Col2, f2, bkbru, new PointF(1588 * 3 / 2 / 4 -300, 50));
        }
        public void InitTK(Graphics g) {
            //用地规划图
            IList<glebeType> list = Services.BaseService.GetStrongList<glebeType>();
            for (int i = 0; i < list.Count; i++) {
                list[i].ObjColor = Color.FromArgb(Convert.ToInt32(list[i].ObligateField1));
            }
            int rem = 0;
            int res = 0;
            int h = pictureBox1.Height - 120;
            int offx = 100;
            int rowcount = Math.DivRem(list.Count, 6, out res);
            if (res > 0) { rowcount = rowcount + 1; }

            decimal hoff = 120 / (rowcount + 3);

            g.DrawString("图例", ft, bkbru, 40f, Convert.ToSingle(h + 20));
            int num1 = 0;
            for (int n = 0; n < list.Count; n++) {
                SolidBrush bru = new SolidBrush(((glebeType)list[n]).ObjColor);
                rem = Math.DivRem(num1, 6, out res);
                if (res == 0) { offx = 100; }
                Rectangle rect = new Rectangle();
                rect.X = offx;
                rect.Y = Convert.ToInt32(h + hoff * rem);
                rect.Width = 20;
                rect.Height = 8;
                
                if (zt == "负荷密度主题") {
                    g.FillRectangle(bru, rect);
                    g.DrawString(((glebeType)list[n]).TypeStyle, ft, bkbru, Convert.ToSingle(offx + 30), Convert.ToSingle(h + hoff * rem));
                }
                if (zt == "用地规划图") {
                    if (((glebeType)list[n]).TypeName == "最低负荷密度") continue;
                    g.FillRectangle(bru, rect);
                    g.DrawString(((glebeType)list[n]).TypeName, ft, bkbru, Convert.ToSingle(offx + 30), Convert.ToSingle(h + hoff * rem));
                }
                num1++;
                offx = offx + 150;
            }
            DrawOther(g);

        }

        public void InitLineTK(Graphics g) {
            Font ft = new Font("宋体", 11f);
            Font sft = new Font("宋体", 8f);
            SolidBrush bkbru = new SolidBrush(Color.Black);
            Pen p1 = new Pen(Color.Black);

            LineInfo line = new LineInfo();
            line.SvgUID = svgdoc.SvgdataUid;
            IList<LineInfo> list = Services.BaseService.GetList<LineInfo>("SelectLineInfoBySvgIDAll", line);

            int rem = 0;
            int res = 0;
            int h = pictureBox1.Height - 120;
            int offx = 100;
            int rowcount = Math.DivRem(idlist.Count, 4, out res);
            if (res > 0) { rowcount = rowcount + 1; }
            int sel = 0;
            decimal hoff = 15;// 120 / (rowcount + 3);

            g.DrawString("图例", ft, bkbru, 40f, Convert.ToSingle(h + 20));
            //for (int n = 0; n < list.Count; n++)
            //{
            for (int m = 0; m < idlist.Count; m++) {
                LineInfo Li = getLine(list, (string)idlist[m]);
                if (Li != null) {

                    Pen pen = new Pen(Color.FromArgb(Convert.ToInt32(Li.ObligateField2)));
                    SolidBrush bru = new SolidBrush(Color.White);
                    //SolidBrush bru = new SolidBrush(((glebeType)list[n]).ObjColor);
                    rem = Math.DivRem(sel, 4, out res);
                    if (res == 0) { offx = 100; }
                    Rectangle rect = new Rectangle();
                    rect.X = offx;
                    rect.Y = Convert.ToInt32(h + hoff * rem);
                    rect.Width = 20;
                    rect.Height = 8;
                    g.DrawLine(pen, offx, Convert.ToInt32(h + hoff * rem), offx + 40, Convert.ToInt32(h + hoff * rem));
                    g.DrawEllipse(pen, offx + 10, Convert.ToInt32(h + hoff * rem) - 3, 6, 6);
                    g.DrawEllipse(pen, offx + 30, Convert.ToInt32(h + hoff * rem) - 3, 6, 6);
                    g.FillEllipse(bru, offx + 10, Convert.ToSingle(Convert.ToSingle(h + hoff * rem) - 2.5), 5, 5);
                    g.FillEllipse(bru, offx + 30, Convert.ToSingle(Convert.ToSingle(h + hoff * rem) - 2.5), 5, 5);
                    //g.FillRectangle(bru, rect);
                    g.DrawString(Li.LineName, ft, bkbru, Convert.ToSingle(offx + 45), Convert.ToSingle(h + hoff * rem - 5));
                    sel += 1;
                    offx = offx + 150;
                    //break;
                }

            }
            //}
            DrawOther(g);
        }
        public LineInfo getLine(IList<LineInfo> l, string id) {
            for (int n = 0; n < l.Count; n++) {
                if ((l[n]).EleID == id) {
                    return l[n];
                }
            }
            return null;
        }
        /// <summary>
        /// 地理接线规划图
        /// </summary>
        /// <param name="g"></param>
        public void InitSymTK(Graphics g) {

            int rem = 0;
            int res = 0;
            int h = pictureBox1.Height - 120;
            int offx = 100;
            int rowcount = Math.DivRem(symlist.Count, 4, out res);
            if (res > 0) { rowcount = rowcount + 1; }
            float hoff = 120 / (rowcount + 3);
            g.DrawString("图例", ft, bkbru, 40f, Convert.ToSingle(h + 20));

            int temp = 0;
            int num1 = 0;
            for (int i = 0; i < symlist.Count; i++) {

                rem = Math.DivRem(num1, 4, out res);
                if (res == 0) { offx = 100; }

                //XmlNode node = NodeFunc.GetRefNode(symlist[i] as string, svgdoc);

                //if (node == null || !(node is Symbol)) continue;

                //Symbol symbol = node as Symbol;
                //RectangleF rf = new RectangleF(offx, h + hoff * rem - 5, 22, 22);

                //symbol.DrawInBox(g, rf);
                //// SolidBrush bru = new SolidBrush(Color.White);
                //g.DrawString(symbol.GetAttribute("label"), ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));

                switch ((string)symlist[i]) {
                    case "#Substation500-1":
                        SolidBrush bru = new SolidBrush(Color.Red);
                        Pen p1 = new Pen(Color.Red);
                        g.FillEllipse(bru, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p1, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawEllipse(p1, offx - 5, Convert.ToSingle(h + hoff * rem - 5), 22, 22);
                        g.DrawString("500kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation220-1":
                        SolidBrush bru1 = new SolidBrush(ColorTranslator.FromHtml("#EF4FED"));
                        Pen p2 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.FillEllipse(bru1, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p2, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawString("220kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation220-2":
                        Pen p3 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.DrawEllipse(p3, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p3, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawLine(p3, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 9, Convert.ToSingle(h + hoff * rem + 9));
                        g.DrawString("220kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation110-1":
                        SolidBrush bru4 = new SolidBrush(ColorTranslator.FromHtml("#00C000"));
                        g.FillEllipse(bru4, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("110kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation110-2":
                        Pen p5 = new Pen(ColorTranslator.FromHtml("#00C000"));
                        g.DrawEllipse(p5, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p5, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("110kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation66-1":
                        SolidBrush bru8 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        g.FillEllipse(bru8, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("66kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation66-2":
                        Pen pp = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(pp, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(pp, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("66kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation35-1":
                        SolidBrush bru6 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        g.FillEllipse(bru6, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("35kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation35-2":
                        Pen p7 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p7, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p7, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("35kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;

                    case "#gh-Substation500-1":

                        Pen p8 = new Pen(Color.Red);
                        g.DrawEllipse(p8, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p8, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawEllipse(p8, offx - 5, Convert.ToSingle(h + hoff * rem - 5), 22, 22);
                        g.DrawString("规划500kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation220-1":
                        //SolidBrush bru1 = new SolidBrush(ColorTranslator.FromHtml("#EF4FED"));
                        Pen p9 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.DrawEllipse(p9, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p9, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawString("规划220kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation220-2":
                        Pen p10 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.DrawEllipse(p10, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p10, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawLine(p10, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 9, Convert.ToSingle(h + hoff * rem + 9));
                        g.DrawLine(p10, offx + 9, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 9));
                        g.DrawString("规划220kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation110-1":
                        //SolidBrush bru11 = new SolidBrush(ColorTranslator.FromHtml("#00C000"));
                        Pen p11 = new Pen(ColorTranslator.FromHtml("#00C000"));
                        g.DrawEllipse(p11, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("规划110kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation110-2":
                        Pen p12 = new Pen(ColorTranslator.FromHtml("#00C000"));
                        g.DrawEllipse(p12, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p12, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawLine(p12, offx + 15, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("规划110kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation66-1":
                        //SolidBrush bru13 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        Pen p13 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p13, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("规划66kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation35-1":
                        //SolidBrush bru13 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        Pen p14 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p14, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("规划35kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation66-2":
                        Pen p15 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p15, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p15, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawLine(p15, offx + 15, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("规划66kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation35-2":
                        Pen p16 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p16, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p16, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawLine(p16, offx + 15, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("规划35kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    default:
                        num1--;
                        offx -= 200;
                        break;

                }
                num1++;
                offx += 200;
                temp = rem;
            }
            int j = 0;
            
            for (int i = num1; i < num1 + 4; i++) {
                rem = Math.DivRem(i, 4, out res);
                if (res == 0) { offx = 100; }
                PointF pf1, pf2;
                pf1 = new PointF(offx - 2, h + hoff * rem - 5 + 11);
                pf2 = new PointF(offx + 24, h + hoff * rem - 5 + 11);
                Pen p1 = new Pen(Color.Black, 4);
                string text1 = "现有220kV线路";
                j++;
                switch (j) {
                    case 0:
                        //p1 = new Pen(Color.Black,4);
                        p1.DashStyle = DashStyle.Dash;
                        text1 = "联络线";
                        break;
                    case 1:
                        text1 = "现有220kV线路";
                        p1 = new Pen(ColorTranslator.FromHtml("#EF4FED"), 4);
                        break;
                    case 2:
                        p1 = new Pen(ColorTranslator.FromHtml("#EF4FED"), 4);
                        p1.DashStyle = DashStyle.Dash;
                        text1 = "规划220kV线路";
                        break;
                    case 3:
                        text1 = "现有66kV线路";
                        p1 = new Pen(ColorTranslator.FromHtml("#0000FF"), 4);
                        break;
                    case 4:
                        p1 = new Pen(ColorTranslator.FromHtml("#0000FF"), 4);
                        p1.DashStyle = DashStyle.Dash;
                        text1 = "规划66kV线路";
                        break;
                }

                g.DrawLine(p1, pf1, pf2);
                //if (j == 3) {
                //    pf1.Y += 2; pf1.X += 2;
                //    g.DrawString("彩色", new Font("宋体", 6), bkbru, pf1);
                //}
                g.DrawString(text1, ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                offx += 200;
            }
            /*
            ArrayList typelist = new ArrayList();
            ArrayList objtypelist = new ArrayList();

            LineInfo line = new LineInfo();
            line.SvgUID = svgdoc.SvgdataUid;
            IList<LineInfo> list = Services.BaseService.GetList<LineInfo>("SelectLineInfoBySvgIDAll2", line);
            for (int i = 0; i < list.Count;i++ )
            {
                for (int m = 0; m < idlist.Count; m++)
                {
                    if (list[i].EleID == (string)idlist[m])
                    {
                        if (!typelist.Contains(list[i].LineType))
                        {
                            typelist.Add(list[i].LineType);
                            objtypelist.Add(list[i]);
                        }
                    }
                }
            }
            temp += 1;
            for (int j = 0; j < objtypelist.Count;j++ )
            {
                rem = Math.DivRem(j, 4, out res);
                if (res == 0) { offx = 100; }
                Pen pen = new Pen(Color.FromArgb(Convert.ToInt32( ((LineInfo)objtypelist[j]).ObligateField2)));
                g.DrawLine(pen, offx, Convert.ToSingle(h + hoff * (temp+rem)), offx+30, Convert.ToSingle(h + hoff * (temp+rem) ));
                g.DrawString(((LineInfo)objtypelist[j]).LineType+"线路", ft, bkbru, offx + 40, Convert.ToSingle(h + hoff * (temp + rem)));
                offx += 200;
            }
            */
            DrawOther(g);
        }
        public void InitGHTK(Graphics g) {
            int rem = 0;
            int res = 0;
            int h = pictureBox1.Height - 120;
            int offx = 100;
            int rowcount = Math.DivRem(symlist.Count, 4, out res);
            if (res > 0) { rowcount = rowcount + 1; }
            decimal hoff = 120 / (rowcount + 3);
            g.DrawString("图例", ft, bkbru, 40f, Convert.ToSingle(h + 20));

            int temp = 0;

            for (int i = 0; i < symlist.Count; i++) {

                rem = Math.DivRem(i, 4, out res);
                if (res == 0) { offx = 100; }

                switch ((string)symlist[i]) {
                    case "#Substation500-1":
                        SolidBrush bru = new SolidBrush(Color.Red);
                        Pen p1 = new Pen(Color.Red);
                        g.FillEllipse(bru, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p1, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawEllipse(p1, offx - 5, Convert.ToSingle(h + hoff * rem - 5), 22, 22);
                        g.DrawString("500kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation220-1":
                        SolidBrush bru1 = new SolidBrush(ColorTranslator.FromHtml("#EF4FED"));
                        Pen p2 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.FillEllipse(bru1, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p2, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawString("220kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation220-2":
                        Pen p3 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.DrawEllipse(p3, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p3, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawLine(p3, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 9, Convert.ToSingle(h + hoff * rem + 9));
                        g.DrawString("220kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation110-1":
                        SolidBrush bru4 = new SolidBrush(ColorTranslator.FromHtml("#00C000"));
                        g.FillEllipse(bru4, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("110kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation110-2":
                        Pen p5 = new Pen(ColorTranslator.FromHtml("#00C000"));
                        g.DrawEllipse(p5, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p5, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("110kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation66-1":
                        SolidBrush bru8 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        g.FillEllipse(bru8, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("66kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation66-2":
                        Pen pp = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(pp, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(pp, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("66kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation35-1":
                        SolidBrush bru6 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        g.FillEllipse(bru6, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("35kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation35-2":
                        Pen p7 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p7, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p7, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("35kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;

                    case "#gh-Substation500-1":

                        Pen p8 = new Pen(Color.Red);
                        g.DrawEllipse(p8, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p8, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawEllipse(p8, offx - 5, Convert.ToSingle(h + hoff * rem - 5), 22, 22);
                        g.DrawString("规划500kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation220-1":
                        //SolidBrush bru1 = new SolidBrush(ColorTranslator.FromHtml("#EF4FED"));
                        Pen p9 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.DrawEllipse(p9, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p9, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawString("规划220kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation220-2":
                        Pen p10 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.DrawEllipse(p10, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p10, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawLine(p10, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 9, Convert.ToSingle(h + hoff * rem + 9));
                        g.DrawLine(p10, offx + 9, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 9));
                        g.DrawString("规划220kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation110-1":
                        //SolidBrush bru11 = new SolidBrush(ColorTranslator.FromHtml("#00C000"));
                        Pen p11 = new Pen(ColorTranslator.FromHtml("#00C000"));
                        g.DrawEllipse(p11, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("规划110kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation110-2":
                        Pen p12 = new Pen(ColorTranslator.FromHtml("#00C000"));
                        g.DrawEllipse(p12, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p12, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawLine(p12, offx + 15, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("规划110kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation66-1":
                        //SolidBrush bru13 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        Pen p13 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p13, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("规划66kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation35-1":
                        //SolidBrush bru13 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        Pen p14 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p14, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("规划35kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation66-2":
                        Pen p15 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p15, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p15, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawLine(p15, offx + 15, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("规划66kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation35-2":
                        Pen p16 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p16, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p16, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawLine(p16, offx + 15, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("规划35kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;

                }
                offx += 200;
                temp = rem;
            }
            DrawOther(g);
        }
        public void InitFHTK(Graphics g) {
            Font ft = new Font("宋体", 11f);
            Font sft = new Font("宋体", 8f);
            SolidBrush bkbru = new SolidBrush(Color.Black);
            Pen p1 = new Pen(Color.Black);

            int rem = 0;
            int res = 0;
            int h = pictureBox1.Height - 120;
            int offx = 100;

            //decimal hoff = 120 / (rowcount + 3);

            g.DrawString("图例", ft, bkbru, 30f, Convert.ToSingle(h + 20));
            g.DrawEllipse(p1, 100, h, 20, 20);
            g.DrawString("E01", ft, bkbru, 100, h - 20);
            g.DrawString("中区编号", ft, bkbru, 180, h - 10);

            g.DrawEllipse(p1, 400, h - 20, 20, 20);
            g.DrawString("5206.12", ft, bkbru, 400, h + 10);
            g.DrawString("中区负荷", ft, bkbru, 280, h - 10);

            DrawOther(g);

        }
        public void InitDYTK(Graphics g) {
            int rem = 0;
            int res = 0;
            int h = pictureBox1.Height - 120;
            int offx = 100;
            int rowcount = Math.DivRem(symlist.Count, 4, out res);
            if (res > 0) { rowcount = rowcount + 1; }
            float hoff = 120 / (rowcount + 3);
            g.DrawString("图例", ft, bkbru, 40f, Convert.ToSingle(h + 20));

            int temp = 0;
            int num1 = 0;
            for (int i = 0; i < symlist.Count; i++) {

                rem = Math.DivRem(i, 4, out res);
                if (res == 0) { offx = 100; }

                //XmlNode node = NodeFunc.GetRefNode(symlist[i] as string, svgdoc);

                //if (node == null || !(node is Symbol)) continue;

                //Symbol symbol = node as Symbol;
                //RectangleF rf = new RectangleF(offx, h + hoff * rem - 5, 22, 22);

                //symbol.DrawInBox(g, rf);
                //g.DrawString(symbol.GetAttribute("label"), ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                switch ((string)symlist[i]) {
                    case "#Substation500-1":
                        SolidBrush bru = new SolidBrush(Color.Red);
                        Pen p1 = new Pen(Color.Red);
                        g.FillEllipse(bru, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p1, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawEllipse(p1, offx - 5, Convert.ToSingle(h + hoff * rem - 5), 22, 22);
                        g.DrawString("500kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation220-1":
                        SolidBrush bru1 = new SolidBrush(ColorTranslator.FromHtml("#EF4FED"));
                        Pen p2 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.FillEllipse(bru1, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p2, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawString("220kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation220-2":
                        Pen p3 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.DrawEllipse(p3, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p3, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawLine(p3, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 9, Convert.ToSingle(h + hoff * rem + 9));
                        g.DrawString("220kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation110-1":
                        SolidBrush bru4 = new SolidBrush(ColorTranslator.FromHtml("#00C000"));
                        g.FillEllipse(bru4, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("110kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation110-2":
                        Pen p5 = new Pen(ColorTranslator.FromHtml("#00C000"));
                        g.DrawEllipse(p5, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p5, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("110kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation66-1":
                        SolidBrush bru8 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        g.FillEllipse(bru8, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("66kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation66-2":
                        Pen pp = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(pp, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(pp, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("66kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#Substation35-1":
                        SolidBrush bru6 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        g.FillEllipse(bru6, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("35kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#user-Substation35-2":
                        Pen p7 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p7, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p7, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("35kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;

                    case "#gh-Substation500-1":

                        Pen p8 = new Pen(Color.Red);
                        g.DrawEllipse(p8, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p8, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawEllipse(p8, offx - 5, Convert.ToSingle(h + hoff * rem - 5), 22, 22);
                        g.DrawString("规划500kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation220-1":
                        //SolidBrush bru1 = new SolidBrush(ColorTranslator.FromHtml("#EF4FED"));
                        Pen p9 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.DrawEllipse(p9, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p9, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawString("规划220kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation220-2":
                        Pen p10 = new Pen(ColorTranslator.FromHtml("#EF4FED"));
                        g.DrawEllipse(p10, offx, Convert.ToSingle(h + hoff * rem), 12, 12);
                        g.DrawEllipse(p10, offx - 3, Convert.ToSingle(h + hoff * rem - 3), 18, 18);
                        g.DrawLine(p10, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 9, Convert.ToSingle(h + hoff * rem + 9));
                        g.DrawLine(p10, offx + 9, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 9));
                        g.DrawString("规划220kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation110-1":
                        //SolidBrush bru11 = new SolidBrush(ColorTranslator.FromHtml("#00C000"));
                        Pen p11 = new Pen(ColorTranslator.FromHtml("#00C000"));
                        g.DrawEllipse(p11, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("规划110kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation110-2":
                        Pen p12 = new Pen(ColorTranslator.FromHtml("#00C000"));
                        g.DrawEllipse(p12, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p12, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawLine(p12, offx + 15, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("规划110kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation66-1":
                        //SolidBrush bru13 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        Pen p13 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p13, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("规划66kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-Substation35-1":
                        //SolidBrush bru13 = new SolidBrush(ColorTranslator.FromHtml("#8080FF"));
                        Pen p14 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p14, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawString("规划35kV变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation66-2":
                        Pen p15 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p15, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p15, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawLine(p15, offx + 15, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("规划66kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#gh-user-Substation35-2":
                        Pen p16 = new Pen(ColorTranslator.FromHtml("#8080FF"));
                        g.DrawEllipse(p16, offx, Convert.ToSingle(h + hoff * rem), 18, 18);
                        g.DrawLine(p16, offx + 3, Convert.ToSingle(h + hoff * rem + 3), offx + 15, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawLine(p16, offx + 15, Convert.ToSingle(h + hoff * rem + 3), offx + 3, Convert.ToSingle(h + hoff * rem + 15));
                        g.DrawString("规划35kV用户变电站", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#kbs":
                        Pen p17 = new Pen(ColorTranslator.FromHtml("#000000"));
                        g.DrawRectangle(p17, offx, Convert.ToSingle(h + hoff * rem), 20, 14);
                        g.DrawString("K", sft, bkbru, new PointF(offx + 5, Convert.ToSingle(h + hoff * rem)));
                        g.DrawString("开闭所", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#llkg":
                        Pen p18 = new Pen(ColorTranslator.FromHtml("#000000"));
                        g.DrawRectangle(p18, offx, Convert.ToSingle(h + hoff * rem), 20, 14);
                        g.DrawString("联络开关", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#hwg":
                        Pen p19 = new Pen(ColorTranslator.FromHtml("#000000"));
                        g.DrawRectangle(p19, offx, Convert.ToSingle(h + hoff * rem), 20, 14);
                        g.DrawString("H", sft, bkbru, new PointF(offx + 5, Convert.ToSingle(h + hoff * rem)));
                        g.DrawString("环网柜", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    case "#fzx":
                        Pen p20 = new Pen(ColorTranslator.FromHtml("#000000"));
                        g.DrawRectangle(p20, offx, Convert.ToSingle(h + hoff * rem), 20, 14);
                        g.DrawString("F", sft, bkbru, new PointF(offx + 5, Convert.ToSingle(h + hoff * rem)));
                        g.DrawString("分支箱", ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                        break;
                    default:
                        num1--;
                        break;
                }
                num1++;
                offx += 200;
                temp = rem;
            }
            int j = 0;
            
            for (int i = num1; i < num1 + 3; i++) {
                rem = Math.DivRem(i, 4, out res);
                if (res == 0) { offx = 100; }
                PointF pf1, pf2;
                pf1 = new PointF(offx - 2, h + hoff * rem - 5 + 11);
                pf2 = new PointF(offx + 24, h + hoff * rem - 5 + 11);
                Pen p1 = new Pen(Color.Black, 4);
                string text1 = "现有10kV线路";
                j++;
                if (j == 2) {
                    text1 = "现有10kV线路";
                }
                if (j == 3) {
                    p1 = new Pen(Color.Green, 4);
                    text1 = "规划10kV线路";
                }
                if (j == 1) {
                    //p1 = new Pen(Color.Black,4);
                    p1.DashStyle = DashStyle.Dash;
                    text1 = "联络线";
                }
                g.DrawLine(p1, pf1, pf2);
                if (j == 3) {
                    pf1.Y += 2; pf1.X += 2;
                    g.DrawString("彩色", new Font("宋体", 6), bkbru, pf1);
                }
                g.DrawString(text1, ft, bkbru, offx + 30, Convert.ToSingle(h + hoff * rem));
                offx += 200;
            }
            DrawOther(g);
        }
        private void button1_Click(object sender, EventArgs e) {
           
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {

            //b.Dispose();
            //g1.Dispose();
        }

        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e) {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e) {
            
            Draw(e.Graphics);
        }

        private void button2_Click(object sender, EventArgs e) {
           
        }

        private void button3_Click(object sender, EventArgs e) {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // printDocument1.Print();
            //printPreviewDialog1.Document = printDocument1;
            pageSetupDialog1.Document = printDocument1;
            pageSetupDialog1.ShowDialog();
            //pictureBox1.Image.Save("D:\\123.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //printDocument1.PrinterSettings.
            printPreviewDialog1.Document = printDocument1;

            printPreviewDialog1.ShowDialog();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "bmp文件 (*.bmp)|*.bmp|jpg文件 (*.jpg)|*.jpg|png文件 (*.png)|*.png|gif文件 (*.gif)|*.gif|tif文件 (*.tif)|*.tif";
            saveFileDialog1.ShowDialog();
            string fname = saveFileDialog1.FileName;
            if (fname != "")
            {
                string type = fname.Substring(fname.LastIndexOf(".") + 1).ToLower();
                switch (type)
                {
                    case "bmp":
                        pictureBox1.Image.Save(fname, ImageFormat.Bmp);
                        break;
                    case "jpg":
                        pictureBox1.Image.Save(fname, ImageFormat.Jpeg);
                        break;
                    case "png":
                        pictureBox1.Image.Save(fname, ImageFormat.Png);
                        break;
                    case "gif":
                        pictureBox1.Image.Save(fname, ImageFormat.Gif);
                        break;
                    case "tif":
                        pictureBox1.Image.Save(fname, ImageFormat.Tiff);
                        break;
                }

            }
        }
    }
}