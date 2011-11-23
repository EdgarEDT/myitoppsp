using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using ItopVector.Core.Document;
using ItopVector.Core.Interface.Figure;
using DevComponents.DotNetBar;
using System.Drawing.Imaging;
using Itop.MapView;
using System.IO;
using ItopVector.Core.Win32;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using ItopVector.Core.Figure;
using System.Collections.Generic;
using Itop.MapView.Tables;
using System.Text;
using System.Configuration;
namespace ItopVectorDraw
{
	/// <summary>
	/// MainFrame 的摘要说明。
	/// </summary>
	public class frmDocument : System.Windows.Forms.Form
	{
		private DevComponents.DotNetBar.TabItem tabItem1;
		public ItopVector.ItopVectorControl documentControl1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 合并成像ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem 更新图像库ToolStripMenuItem;
        private FolderBrowserDialog folderBrowserDialog1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem 图片另存ToolStripMenuItem;
        private ToolStripMenuItem 图片更换ToolStripMenuItem;
        private ToolStripMenuItem 重新下载ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem 批量更换ToolStripMenuItem;
        private ToolStripMenuItem 显示地图ToolStripMenuItem;
        private ToolStripMenuItem 地图ToolStripMenuItem;
        private ToolStripMenuItem 卫星ToolStripMenuItem;
        private ToolStripMenuItem 地形ToolStripMenuItem;
		//private ItopVector.DrawArea.DrawArea drawArea1;
		private System.ComponentModel.IContainer components;

		public frmDocument()
		{
			
			InitializeComponent();
            double jd = double.Parse(ConfigurationManager.AppSettings["jd"]);
            double wd = double.Parse(ConfigurationManager.AppSettings["wd"]);
            int viewwidth = int.Parse(ConfigurationManager.AppSettings["mapview"]);
            string maptype = ConfigurationManager.AppSettings["maptype"];
            if (maptype == "baidu") mapview = new MapViewObj();
            else
                mapview = new Itop.MapView.MapViewGoogle();
			this.documentControl1.DocumentChanged +=new OnDocumentChangedEventHandler(documentControl1_DocumentChanged);
			this.documentControl1.UpdateChanged +=new EventHandler(documentControl1_UpdateChanged);
			this.documentControl1.LeftClick+=new ItopVector.DrawArea.SvgElementEventHandler(documentControl1_LeftClick);
			this.documentControl1.DragAndDrop+=new DragEventHandler(documentControl1_DragAndDrop);
			this.documentControl1.RightClick+=new ItopVector.DrawArea.SvgElementEventHandler(documentControl1_RightClick);
			this.documentControl1.PaintMap+=new ItopVector.DrawArea.PaintMapEventHandler(documentControl1_PaintMap);
            this.documentControl1.AfterPaintPage += new ItopVector.DrawArea.PaintMapEventHandler(documentControl1_AfterPaintPage);
			//			SvgDocument.currentLayer="layer53860";
			//			documentControl1.SVGDocument.getLayerList
			//			documentControl1.FullDrawMode=true;
			//			documentControl1.BackColor=Color.Purple;
			
//			this.documentControl1.DrawArea.OnBeforeRenderTo+=new ItopVector.DrawArea.PaintMapEventHandler(DrawArea_OnBeforeRenderTo);
            
            this.documentControl1.DrawArea.ViewMargin = new Size(viewwidth, viewwidth);
            mapview.ZeroLongLat = new LongLat(jd,wd);
			this.documentControl1.DrawArea.FreeSelect =true;
			this.documentControl1.FullDrawMode =true;
            SvgDocument.BkImageLoad = true;
//			this.documentControl1.BackColor = Color.BurlyWood;
//			this.documentControl1.ScaleRatio=2;
            this.documentControl1.DrawMode = DrawModeType.MemoryImage;
            this.documentControl1.CanEdit = true;
			this.documentControl1.MoveIn+=new ItopVector.DrawArea.SvgElementEventHandler(documentControl1_MoveIn);
			this.documentControl1.MoveOut+=new ItopVector.DrawArea.SvgElementEventHandler(documentControl1_MoveOut);
			this.documentControl1.DoubleLeftClick+=new ItopVector.DrawArea.SvgElementEventHandler(documentControl1_DoubleLeftClick);
			this.documentControl1.DoubleClick+=new EventHandler(documentControl1_DoubleClick);
            this.documentControl1.DrawArea.MouseDown += new MouseEventHandler(DrawArea_MouseDown);
            this.documentControl1.DrawArea.MouseUp += new MouseEventHandler(DrawArea_MouseUp);
		}

        void DrawArea_MouseUp(object sender, MouseEventArgs e) {
            isdown = false;
            beginPoint = Point.Empty;
            backbmp = null;
        }
        bool isdown = false;
        Point beginPoint = Point.Empty;
        void DrawArea_MouseDown(object sender, MouseEventArgs e) {
            isdown = true;
            beginPoint = Control.MousePosition;
        }

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.documentControl1 = new ItopVector.ItopVectorControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.合并成像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.更新图像库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.图片另存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图片更换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新下载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.批量更换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.卫星ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabItem1
            // 
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "tabItem1";
            // 
            // documentControl1
            // 
            this.documentControl1.CanEdit = false;
            this.documentControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentControl1.FullDrawMode = false;
            this.documentControl1.IsPasteGrid = false;
            this.documentControl1.IsShowGrid = true;
            this.documentControl1.IsShowRule = true;
            this.documentControl1.IsShowTip = false;
            this.documentControl1.Location = new System.Drawing.Point(0, 0);
            this.documentControl1.Name = "documentControl1";
            this.documentControl1.Size = new System.Drawing.Size(616, 373);
            this.documentControl1.TabIndex = 0;
            this.documentControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.documentControl1_MouseMove);
            this.documentControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.documentControl1_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.合并成像ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.更新图像库ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 54);
            // 
            // 合并成像ToolStripMenuItem
            // 
            this.合并成像ToolStripMenuItem.Name = "合并成像ToolStripMenuItem";
            this.合并成像ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.合并成像ToolStripMenuItem.Text = "合并图像";
            this.合并成像ToolStripMenuItem.Click += new System.EventHandler(this.合并成像ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(131, 6);
            // 
            // 更新图像库ToolStripMenuItem
            // 
            this.更新图像库ToolStripMenuItem.Name = "更新图像库ToolStripMenuItem";
            this.更新图像库ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.更新图像库ToolStripMenuItem.Text = "更新图像库";
            this.更新图像库ToolStripMenuItem.Click += new System.EventHandler(this.更新图像库ToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图片另存ToolStripMenuItem,
            this.图片更换ToolStripMenuItem,
            this.重新下载ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.批量更换ToolStripMenuItem,
            this.显示地图ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(153, 142);
            // 
            // 图片另存ToolStripMenuItem
            // 
            this.图片另存ToolStripMenuItem.Name = "图片另存ToolStripMenuItem";
            this.图片另存ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.图片另存ToolStripMenuItem.Text = "图片另存";
            this.图片另存ToolStripMenuItem.Click += new System.EventHandler(this.图片另存ToolStripMenuItem_Click);
            // 
            // 图片更换ToolStripMenuItem
            // 
            this.图片更换ToolStripMenuItem.Name = "图片更换ToolStripMenuItem";
            this.图片更换ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.图片更换ToolStripMenuItem.Text = "图片更换";
            this.图片更换ToolStripMenuItem.Click += new System.EventHandler(this.图片更换ToolStripMenuItem_Click);
            // 
            // 重新下载ToolStripMenuItem
            // 
            this.重新下载ToolStripMenuItem.Name = "重新下载ToolStripMenuItem";
            this.重新下载ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.重新下载ToolStripMenuItem.Text = "重新下载";
            this.重新下载ToolStripMenuItem.Click += new System.EventHandler(this.重新下载ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // 批量更换ToolStripMenuItem
            // 
            this.批量更换ToolStripMenuItem.Name = "批量更换ToolStripMenuItem";
            this.批量更换ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.批量更换ToolStripMenuItem.Text = "批量更换...";
            this.批量更换ToolStripMenuItem.Visible = false;
            // 
            // 显示地图ToolStripMenuItem
            // 
            this.显示地图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.地图ToolStripMenuItem,
            this.卫星ToolStripMenuItem,
            this.地形ToolStripMenuItem});
            this.显示地图ToolStripMenuItem.Name = "显示地图ToolStripMenuItem";
            this.显示地图ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.显示地图ToolStripMenuItem.Text = "显示地图";
            // 
            // 地图ToolStripMenuItem
            // 
            this.地图ToolStripMenuItem.Name = "地图ToolStripMenuItem";
            this.地图ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.地图ToolStripMenuItem.Text = "地图";
            this.地图ToolStripMenuItem.Click += new System.EventHandler(this.地图ToolStripMenuItem_Click);
            // 
            // 卫星ToolStripMenuItem
            // 
            this.卫星ToolStripMenuItem.Name = "卫星ToolStripMenuItem";
            this.卫星ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.卫星ToolStripMenuItem.Text = "卫星";
            this.卫星ToolStripMenuItem.Click += new System.EventHandler(this.卫星ToolStripMenuItem_Click);
            // 
            // 地形ToolStripMenuItem
            // 
            this.地形ToolStripMenuItem.Name = "地形ToolStripMenuItem";
            this.地形ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.地形ToolStripMenuItem.Text = "地形";
            this.地形ToolStripMenuItem.Click += new System.EventHandler(this.地形ToolStripMenuItem_Click);
            // 
            // frmDocument
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(616, 373);
            this.Controls.Add(this.documentControl1);
            this.Name = "frmDocument";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            this.WindowState = FormWindowState.Maximized;
            if (mapview is MapViewGoogle) {

                MapViewGoogle gd = mapview as MapViewGoogle;
                gd.OnDownCompleted += new DownCompleteEventHandler(MapViewGoogle_OnDownCompleted);
                gd.IsDownMap = true;
            }
            documentControl1.ScaleRatio = .25f;
        }

        void MapViewGoogle_OnDownCompleted(ClassImage mapclass) {
            if(mapclass.PicImage!=null)
            documentControl1.DrawArea.InvadateRect(mapclass.Bounds);
        }
		protected override void OnClosing(CancelEventArgs e)
		{
//			documentControl1.SVGDocument.AcceptChanges =false;
//			XmlNodeList list = documentControl1.SVGDocument.SelectNodes("svg/use[@href=\"#实心杆塔]");
//
//			foreach(XmlNode node in list)
//			{
//				node.ParentNode.RemoveChild(node);
//			}
//			if (list.Count>0)
//			{
//				this.Save();
//				e.Cancel =true;
//			}
			if(this.documentControl1.IsModified)
			{

				string str =string.Format("文件\"{0}\"已经发生修改，是否保存？",this.documentControl1.SVGDocument.FileName);
				DialogResult result= MessageBox.Show(str,"保存",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
				if(result==DialogResult.Yes)
				{
					this.Save();
				}
				else if(result==DialogResult.Cancel)
				{
					e.Cancel =true;
				}
			}
//			XmlNode node = this.documentControl1.SVGDocument.SelectSingleNode("svg/*[@xlink:href='#knife1.0']");
//			if(node !=null)
//			{
//
//				MessageBox.Show(node.Attributes["href","xlink"].ToString());
//			}

//			StreamWriter sw =new StreamWriter("c:\\svgtest.svg");
//			string str1=this.documentControl1.SVGDocument.SelectNodesToString("svg/defs | svg/rect");
//			sw.Write(SvgDocument.Union(str1,str1,str1));
//			sw.Close();
			
			base.OnClosing (e);
		}
		public void Save()
		{
			if(this.MdiParent is MainFrame)
			{
				((MainFrame)this.MdiParent).Save(this);
			}
		}
		public string FileName
		{
			get
			{
				return this.documentControl1.SVGDocument.FileName;
			}
		}
		public string FilePath
		{
			get
			{
				return this.documentControl1.SVGDocument.FilePath;
			}
		}

		private void documentControl1_DocumentChanged(object sender, DocumentChangedEventArgs e)
		{
			//			if(this.documentControl1.SVGDocument!=null && this.documentControl1.SVGDocument.FileName!=string.Empty)
			//			{
			//				//MessageBox.Show(e.ChangeElements.);
			//				if(documentControl1.SVGDocument.SelectCollection.Count>0)
			//				{
			//					MessageBox.Show(this.documentControl1.SVGDocument.SelectCollection[0].ID);
			//				}
			//			}
		}

		private void documentControl1_UpdateChanged(object sender, EventArgs e)
		{
			if(this.documentControl1.SVGDocument.FileName !=string.Empty)
			{
				this.Text = FileName;
			}
		}
        private void swap(ref float value1, ref float value2) {
            float temp = value1;
            value1 = value2;
            value2 = temp;
        }
        private bool LineAndRect(PointF p1,PointF p2,RectangleF r1) {
            return LineAndRect(p1.X, p1.Y, p2.X, p2.Y, r1.Left, r1.Top, r1.Right, r1.Bottom);
        }
        private bool LineAndRect(float xs,float ys,float xe,float ye,float xleft,float ytop,float xr,float yb) {

            float a = ys - ye, b = xe - xs, c = xs * ye - xe * ys;
            if ((a * xleft + b * ytop + c >= 0 && a * xr + b * yb + c <= 0) ||
                (a * xleft + b * ytop + c <= 0 && a * xr + b * yb + c >= 0) ||
                (a * xleft + b * yb + c >= 0 && a * xr + b * ytop + c <= 0) ||
                (a * xleft + b * yb + c <= 0 && a * xr + b * ytop + c >= 0)) {
                if (xleft > xr)
                    swap(ref xleft,ref xr);
                if (ytop < yb)
                    swap(ref ytop, ref yb);
                if ((xs < xleft && xe < xleft) ||
                    (xs > xr && xe > xr) ||
                    (ys > ytop && ye > ytop) ||
                    (ys < yb && ye < yb))
                    return false;
                else
                    return true;
            } else
                return false;

        }
        ClassImage curImage=null;
        private void documentControl1_RightClick(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e) {
            documentControl1.Operation = ToolOperation.Select;
            curImage = null;
            if (documentControl1.SVGDocument.SelectCollection.Count > 0 && !(documentControl1.SVGDocument.CurrentElement is SVG)) {
                contextMenuStrip1.Show(documentControl1, e.Mouse.Location);
            } else {
                curImage = mapview.FindImage(e.Mouse.Location);
                contextMenuStrip2.Show(documentControl1, e.Mouse.Location);
            }
             
            //			Region r=new Region();
            //			documentControl1.SelectedRectangle(r);
            //			this.Text=e.SvgElement.ID+"right";
            //			if(documentControl1.Operation==ToolOperation.Enclosure)
            //			{
            //				ArrayList aList=new ArrayList();
            //				System.Collections.CollectionBase col=documentControl1.SVGDocument.SelectCollection;
            //				IEnumerator cl=col.GetEnumerator();
            //				while(cl.MoveNext())
            //				{
            //					XmlElement xl=(XmlElement)cl.Current;
            //					aList.Add(xl.GetAttribute("info-name"));
            //				}
            //				frmSelCol frm=new frmSelCol();
            //				frm.list=aList;
            //				frm.ShowDialog(this);
            //			}
            //			documentControl1.Operation=ToolOperation.FreeTransform;
            //			((MainFrame)this.MdiParent).UpdateToolBottom(ToolOperation.FreeTransform);

            
        }
		private void documentControl1_LeftClick(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e)
		{
			//MessageBox.Show(documentControl1.SVGDocument.CurrentElement.ID);
		}

		private void documentControl1_DragAndDrop(object sender, DragEventArgs e)
		{
			/*object obj1 = e.Data.GetData("SvgElement");
			if (obj1 is IGraph)
			{
				IGraph graph1 = (IGraph) obj1;
				MessageBox.Show(graph1.ID);
			}*/
		}

		private void documentControl1_PaintMap(object sender, ItopVector.Core.PaintMapEventArgs e)
		{

		}

        //Itop.MapView.IMapViewObj mapview=new Itop.MapView.MapViewObj();
        Itop.MapView.IMapViewObj mapview = null;

        public Itop.MapView.IMapViewObj Mapview {
            get { return mapview; }
            set { mapview = value; }
        }
        
        System.Drawing.Image backbmp = null;
        float curScale = 1;
        int curLevel = -1;
        float mapScale = 1;
		private void documentControl1_AfterPaintPage(object sender, ItopVector.Core.PaintMapEventArgs e)
		{

            (mapview as MapViewBase).ShowMapInfo = true;
			int nScale=0;
            float nn = 1;
            nScale = mapview.Getlevel(documentControl1.ScaleRatio);
            //if(isIn)
            //nScale = mapview.Getlevel(documentControl1.ScaleRatio, out nn);
            //else
            //nScale = mapview.Getlevel2(documentControl1.ScaleRatio, out nn);
            if (nScale == -1) return;
            
            
			LongLat longlat=LongLat.Empty;
			//计算中心点经纬度

            longlat = mapview.OffSet(mapview.ZeroLongLat, nScale, (int)(e.CenterPoint.X/nn), (int)(e.CenterPoint.Y/nn));

			//创建地图

            System.Drawing.Image image = backbmp;
            //if (image != null && isdown) { } else
            if(nn>=1)
                //for (double i=-0.1;i<=0.1;i=i+0.01)
                //{
                //    for (double j = -0.1; j <= 0.1; j = j + 0.01)
                //    {
                //        image = mapview.CreateMap(e.Bounds.Width, e.Bounds.Height, nScale, longlat.Longitude + i, longlat.Latitude+j);
                //    }                   
                //}
                //image = mapview.CreateMap(1000, 1000, nScale, longlat.Longitude, longlat.Latitude);
               image = mapview.CreateMap(e.Bounds.Width, e.Bounds.Height, nScale, longlat.Longitude, longlat.Latitude);
            else
             image = mapview.CreateMap((int)(e.Bounds.Width/nn), (int)(e.Bounds.Height/nn), nScale, longlat.Longitude, longlat.Latitude);
            //backbmp = image;
			ImageAttributes imageAttributes=new ImageAttributes();
			ColorMatrix matrix1 = new ColorMatrix();		
			matrix1.Matrix00 = 1f;
			matrix1.Matrix11 = 1f;
			matrix1.Matrix22 = 1f;
			matrix1.Matrix33 = 0.9f;//地图透明度
			matrix1.Matrix44 = 1f;
			//设置地图透明度
			imageAttributes.SetColorMatrix(matrix1, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            //int offx = 0;
            //int offy = 0;
            //if (isdown) {
            //    offx = Control.MousePosition.X - beginPoint.X;
            //    offy = Control.MousePosition.Y - beginPoint.Y;
            //}
            //e.G.TranslateTransform(offx, offy, MatrixOrder.Append);
			//绘制地图

             if (nn > 1) {
                 int w1 =(int)( e.Bounds.Width*((nn-1) / 2));
                 int h1 = (int)(e.Bounds.Height * ((nn - 1) / 2));

                 Rectangle rt1 = e.Bounds;
                 rt1.Inflate(w1,h1);
                 e.G.CompositingQuality = CompositingQuality.HighQuality;
                 e.G.DrawImage((Bitmap)image, rt1, 0f, 0f, (float)image.Width, (float)image.Height, GraphicsUnit.Pixel, imageAttributes);
            }else
			e.G.DrawImage((Bitmap)image, e.Bounds, 0f, 0f, (float) image.Width, (float) image.Height, GraphicsUnit.Pixel,imageAttributes);
            SolidBrush brush = new SolidBrush(Color.FromArgb(220, 75, 75, 75));
            //e.G.FillRectangle( brush,e.G.VisibleClipBounds);
			//绘制中心点
			e.G.DrawEllipse(Pens.Red,e.Bounds.Width/2 -2 ,e.Bounds.Height/2 -2 ,4,4);
			e.G.DrawEllipse(Pens.Red,e.Bounds.Width/2 -1 ,e.Bounds.Height/2 -1 ,2,2);
            ///107,159
		{//绘制比例尺
			Point p1=new Point(20,e.Bounds.Height -30);
			Point p2= new Point(20,e.Bounds.Height -20);
			Point p3=new Point(80,e.Bounds.Height -20);
			Point p4=new Point(80,e.Bounds.Height -30);

			e.G.DrawLines(new Pen(Color.Black,2),new Point[4]{p1,p2,p3,p4});
            string str1 = string.Format("{0}公里", mapview.GetMiles(nScale));
			e.G.DrawString(str1, new Font("宋体", 10), Brushes.Black, 30,e.Bounds.Height -40);
		}
//			string s = string.Format("{0}行{1}列", nRows, nCols);
			string s = string.Format("经{0}：纬{1}", longlat.Longitude, longlat.Latitude);
//			//显示中心点经纬度
			e.G.DrawString(s, new Font("宋体", 10), Brushes.Red, 20, 40);

//			IntPtr hDC = e.G.GetHdc();
//
//			
//			LOGBRUSH brush;
//			brush.lbColor = 255;
//			brush.lbHatch = 10;
//			brush.lbStyle = 1;//BS_NULL
//			IntPtr hBursh = CreateBrushIndirect(ref brush);
//			IntPtr hPen = CreatePen(2, 1, 255);
//			IntPtr nOldPen = SelectObject(hDC, hPen);
//			IntPtr nOldBrush = SelectObject(hDC, hBursh);
//			SetROP2(hDC,10);
//			Rectangle((int)hDC, 100, 100, 50, 50);
////			SetROP2(hDC,10);
//			Rectangle((int)hDC, 150, 150, 50, 50);
//			
//			SelectObject(hDC, nOldPen);
//			SelectObject(hDC, nOldBrush);
//			e.G.ReleaseHdc(hDC);
		}

		private void DrawArea_OnBeforeRenderTo(object sender, ItopVector.Core.PaintMapEventArgs e)
		{
//			PointF pf = documentControl1.DrawArea.ContentBounds.Location;
//			Rectangle rt =  Rectangle.Round(e.G.VisibleClipBounds);//+rt.X+rt.Y
//			int offsetX = (int)(rt.Width/2 + pf.X );
//			int offsetY = (int)(rt.Height/2 + pf.Y );
//			LongLat longlat = MapViewObj.OffSet(mapview.ZeroLongLat,11,-offsetX,-offsetY);
//		    mapview.Paint(e.G,(int)rt.Width,(int)rt.Height ,11,longlat.Longitude,longlat.Latitude);
//			System.Drawing.Image image = mapview.CreateMap((int)rt.Width,(int)rt.Height ,11,longlat.Longitude,longlat.Latitude);
//			//绘制地图
//			e.G.DrawImage((Bitmap)image,rt , 0f, 0f, (float) image.Width, (float) image.Height, GraphicsUnit.Pixel);

		}
		[DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
		public static extern int SetROP2(IntPtr n, int i);
		[DllImport("gdi32.dll", EntryPoint = "Rectangle")]
		public static extern int Rectangle(
			int hdc,
			int X1,
			int Y1,
			int X2,
			int Y2
			);
		[DllImport("gdi32.dll", EntryPoint = "ExtCreatePen")]
		public static extern int ExtCreatePen(
			int dwPenStyle,
			int dwWidth,
			ref LOGBRUSH lplb,
			int dwStyleCount,
			ref int lpStyle
			);
		[DllImport("gdi32.dll", ExactSpelling=true)]
		public static extern IntPtr SelectObject(
			IntPtr hdc,
			IntPtr hObject
			);
		[DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
		public static extern int DeleteObject(
			int hObject
			);
		[DllImport("gdi32.dll",  CharSet=CharSet.Auto)]
		public static extern IntPtr CreatePen(
			int nPenStyle,
			int nWidth,
			int crColor
			);
		[DllImport("gdi32.dll", EntryPoint = "CreateBrushIndirect")]
		public static extern IntPtr CreateBrushIndirect(
			ref LOGBRUSH lpLogBrush
			);

		private void documentControl1_MoveIn(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e)
		{
			//(e.SvgElement as IGraph).CanSelect=false;
            this.documentControl1.SetToolTip(((ItopVector.Core.SvgElement)e.SvgElement).GetAttribute("info-name"));

		}

		private void documentControl1_MoveOut(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e)
		{
			this.documentControl1.SetToolTip("");

		}

		private void documentControl1_DoubleLeftClick(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e)
		{
//			IGraph graph1 = this.documentControl1.DrawArea.CurrentElement as IGraph;
//			if (graph1 !=null)
//			{
//				PointF pf1= graph1.CenterPoint;
//				LongLat ll = mapview.ParseToLongLat((int)pf1.X,(int)pf1.Y);
//				PointF pf2 = mapview.ParseToPoint(ll.Longitude,ll.Latitude);
//				graph1.CenterPoint = new PointF(744f,525f);
//			}
//			this.documentControl1.CurrentOperation = ToolOperation.EqualPolygon;
//			this.documentControl1.DrawArea.ExportImageToClipboard( ExportType.Selected);
//			documentControl1.GoLocation(documentControl1.PointToView( documentControl1.PointToClient(Control.MousePosition)));
            isIn = !isIn;
			//documentControl1.SVGDocument.BeginPrint=!documentControl1.SVGDocument.BeginPrint;
            //MapViewGoogle mvg = mapview as MapViewGoogle;
            //if (mvg != null) {
            //    if (!mvg.Showmap1) {
            //        mvg.Showmap1 = true;
            //    } else {
            //        mvg.Showmap3 = true;
            //    }
            //}
		}
        bool isIn = false;
		private void documentControl1_DoubleClick(object sender, EventArgs e)
		{
            
		}
        private IList getImages(bool update){
            IList retlist = new ArrayList();
            int nWidth = mapview.PicWidth;
            Dictionary<string, ClassImage> table = new Dictionary<string, ClassImage>();
            
            foreach (IGraph graph in documentControl1.SVGDocument.SelectCollection) {
                if (graph != null) {
                    if (graph is ItopVector.Core.Figure.Line) {
                        Line ll = (graph as Line);
                        PointF[] pts = (PointF[])ll.Points.Clone();
                        graph.Transform.Matrix.TransformPoints(pts);
                        documentControl1.DrawArea.PointToSystem(pts);
                        //PointF pf2 = ll.CenterPoint;
                        //pf2 = documentControl1.DrawArea.PointToSystem(pf2);
                        PointF pf3 = ll.CenterPoint;// new PointF((pts[0].X + pts[1].X) / 2, (pts[1].Y + pts[1].Y) / 2);

                        //this.documentControl1.SetToolTip(LineAndRect(pts[0],pts[1],new RectangleF(100,100,100,100)).ToString());
                        int width = Convert.ToInt32(Math.Abs(pts[0].X - pts[1].X));
                        int height = Convert.ToInt32(Math.Abs(pts[0].Y - pts[1].Y));
                        IList<ClassImage> list = mapview.GetMapList(width, height, new Point((int)pf3.X, (int)pf3.Y));
                        StringBuilder sb = new StringBuilder();

                        Point off1 = new Point((int)Math.Min(pts[0].X, pts[1].X), (int)Math.Min(pts[0].Y, pts[1].Y));
                        pts[0].X -= off1.X;
                        pts[0].Y -= off1.Y;
                        pts[1].X -= off1.X;
                        pts[1].Y -= off1.Y;
                        //GraphicsPath gp = graph.GPath.Clone() as GraphicsPath;
                        //using (Matrix matrix1 = graph.Transform.Matrix.Clone()) {
                        //    matrix1.Multiply(graph.GraphTransform.Matrix);
                        //    matrix1.Translate(-off1.X, -off1.Y);

                        //    gp.Transform(matrix1);
                        //}
                        Graphics g = documentControl1.CreateGraphics();
                        foreach (ClassImage mc in list) {
                            Rectangle rt = mc.Bounds;
                            //rt.Location = documentControl1.DrawArea.PointToView(rt.Location);
                            //rt.Location.Offset(off1);
                            string pic = mc.PicUrl;
                            if (LineAndRect(pts[0], pts[1], rt)) {
                                sb.AppendLine(string.Format("{0}_{1}_{2}", mc.PicUrl, rt.Left, rt.Top));
                                if (table.ContainsKey(pic)) {
                                    mc.PicImage = table[pic].PicImage;
                                    table[pic] = mc;
                                } else {
                                    table.Add(pic, mc);
                                }
                                System.Drawing.Image image = new Bitmap(nWidth, nWidth);
                                Graphics g1 = Graphics.FromImage(image);
                                g1.Clear(Color.White);
                                if (mc.PicImage == null) {

                                } else {
                                    //g = Graphics.FromImage(mc.PicImage);
                                    g1.DrawImage(mc.PicImage, 0, 0, nWidth, nWidth);
                                }
                                Matrix matrix2 = graph.GraphTransform.Matrix;
                                PointF pf22 = off1;
                                pf22.X += mc.Left;
                                pf22.Y += mc.Top;

                                pf22 = documentControl1.DrawArea.PointToView(pf22);
                                matrix2.Translate(-matrix2.OffsetX, -matrix2.OffsetY, MatrixOrder.Append);
                                //Matrix matrix2
                                matrix2.Translate(-pf22.X * documentControl1.ScaleRatio, -pf22.Y * documentControl1.ScaleRatio, MatrixOrder.Append);
                                //matrix2.Translate(
                                //g1.TranslateTransform(pf22.X, pf22.Y);
                                //graph.GraphTransform.Matrix=
                                graph.Draw(g1, 0);
                                mc.PicImage = image;

                                g1.Dispose();
                                
                                image.Save(Application.StartupPath + "\\png\\" + mc.PicUrl.Replace('/', '~'), ImageFormat.Png);
                                
                            }
                        }
                        //this.documentControl1.SetToolTip(sb.ToString());

                    } else
                        if (!(graph is SVG)) {

                            RectangleF rf = graph.GetBounds();
                            GraphicsPath path2 = new GraphicsPath();
                            path2.AddRectangle(rf);

                           
                            if (!(graph is Use))
                                rf = graph.GPath.GetBounds(graph.Transform.Matrix);
                                //graph.Transform.Matrix.TransformPoints(pts);

                            PointF[] pts = new PointF[2]{rf.Location,new PointF(rf.Right, rf.Bottom) };

                            PointF[] pts2=(PointF[])pts.Clone(); 
                            documentControl1.DrawArea.PointToSystem(pts);
                            PointF pf3 = graph.CenterPoint;

                            int width = Convert.ToInt32(Math.Abs(pts[0].X - pts[1].X));
                            int height = Convert.ToInt32(Math.Abs(pts[0].Y - pts[1].Y));
                            IList<ClassImage> list = mapview.GetMapList(width, height, new Point((int)pf3.X, (int)pf3.Y));
                            StringBuilder sb = new StringBuilder();
                            PointF off1 = pts[0];

                            GraphicsPath gp = graph.GPath.Clone() as GraphicsPath;
                            using (Matrix matrix1 = graph.Transform.Matrix.Clone()) {
                                matrix1.Multiply(documentControl1.DrawArea.CoordTransform, MatrixOrder.Append);
                                matrix1.Translate(-off1.X, -off1.Y, MatrixOrder.Append);
                                gp.Transform(matrix1);
                            }

                            Graphics g = documentControl1.CreateGraphics();
                            Graphics g1 = null;
                            foreach (ClassImage mc in list) {
                                string pic = mc.PicUrl;
                                RectangleF rt = mc.Bounds;
                                Region r = new Region(rt);
                                rt = r.GetBounds(g);
                                r.Intersect(gp);
                                if (!r.IsEmpty(g)) {

                                    if (table.ContainsKey(pic)) {
                                        mc.PicImage = table[pic].PicImage;
                                        table[pic] = mc;
                                    } else {
                                        table.Add(pic, mc);
                                    }
                                    sb.AppendLine(string.Format("{0}_{1}_{2}", mc.PicUrl, rt.Left, rt.Top));
                                    System.Drawing.Image image = new Bitmap(nWidth, nWidth);
                                    
                                    g1 = Graphics.FromImage(image);
                                    g1.Clear(Color.White);
                                    if (mc.PicImage == null) {

                                    } else {
                                        //g = Graphics.FromImage(mc.PicImage);
                                        g1.DrawImage(mc.PicImage, 0, 0, nWidth, nWidth);
                                    }
                                    Matrix matrix2 = new Matrix();
                                    matrix2.Multiply(documentControl1.DrawArea.CoordTransform);
                                    PointF pf22 = off1;
                                    if (graph is Text || graph is Use) {//文字特殊处理
                                        matrix2 = graph.GraphTransform.Matrix;
                                        if (graph.LimitSize) {//文字固定大小
                                            float f1 = graph.Transform.Matrix.Elements[0] - 1;
                                            matrix2 = graph.Transform.Matrix.Clone();
                                            matrix2.Invert();
                                            graph.LimitSize = false;
                                            if (graph is Text) {
                                                matrix2.Translate(
                                                    -mc.Left - pts2[0].X - rf.Width * f1 / 2 + graph.Transform.Matrix.OffsetX,
                                                    -mc.Top - pts2[0].Y - rf.Height * f1 / 2 + graph.Transform.Matrix.OffsetY, MatrixOrder.Append);
                                            } else {
                                                matrix2.Translate(
                                                -mc.Left - pts2[0].X - rf.Width / 2 + graph.Transform.Matrix.OffsetX,
                                                -mc.Top - pts2[0].Y - rf.Width  / 2 + graph.Transform.Matrix.OffsetY, MatrixOrder.Append);
                                            
                                            }
                                            graph.LimitSize = true;
                                            graph.GraphTransform.Matrix = matrix2;
                                        } else {
                                            pf22.X += mc.Left;
                                            pf22.Y += mc.Top;
                                            pf22 = documentControl1.DrawArea.PointToView(pf22);
                                            matrix2.Translate(-matrix2.OffsetX, -matrix2.OffsetY, MatrixOrder.Append);
                                            matrix2.Translate(-pf22.X * documentControl1.ScaleRatio, -pf22.Y * documentControl1.ScaleRatio, MatrixOrder.Append);                                        
                                        }                                       

                                    } else {                                        
                                        pf22.X += mc.Left;
                                        pf22.Y += mc.Top;
                                        pf22 = documentControl1.DrawArea.PointToView(pf22);
                                        matrix2.Translate(-matrix2.OffsetX, -matrix2.OffsetY, MatrixOrder.Append);                                    
                                        matrix2.Translate(-pf22.X * documentControl1.ScaleRatio, -pf22.Y * documentControl1.ScaleRatio, MatrixOrder.Append);
                                    }
                                    graph.GraphTransform.Matrix = matrix2;
                                    graph.Draw(g1, 0);
                                    mc.PicImage = image;
                                    g1.Dispose();
                                    if(update)
                                        mapview.SetImage(mc);
                                    else
                                        image.Save(Application.StartupPath + "\\png\\" + mc.PicUrl.Replace('/', '~'), ImageFormat.Png);
                                    
                                }
                            }
                            g.Dispose();
                            GC.Collect();
                            //this.documentControl1.SetToolTip(sb.ToString());
                        }
                }
            }
            foreach (string key in table.Keys) {
                retlist.Add(table[key]);
            }

            return retlist;
        }
        private void union() {
            
        }
        private void 合并成像ToolStripMenuItem_Click(object sender, EventArgs e) {
            getImages(false);
        }

        private void 更新图像库ToolStripMenuItem_Click(object sender, EventArgs e) {
            getImages(true);
            //foreach (ClassImage image in list) {
            //    mapview.SetImage(image);
            //}
            mapview.ClearBuff();
            documentControl1.Invalidate(true);
            documentControl1.Update();
        }

        private void 图片另存ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (curImage == null) return;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "图像文件(*.png)|*.png;";
            dlg.FileName = curImage.PicUrl.Replace('/', '~');
            if (dlg.ShowDialog() == DialogResult.OK) {
                curImage.PicImage.Save(dlg.FileName, curImage.PicImage.RawFormat);
            }
        }

        private void 图片更换ToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "图像文件(*.png)|*.png;";
            dlg.FileName = curImage.PicUrl.Replace('/', '~');
            if (dlg.ShowDialog() == DialogResult.OK) {
                Stream stream = dlg.OpenFile();
                curImage.PicImage = Bitmap.FromStream(stream);
                stream.Close();
                stream.Dispose();
                mapview.SetImage(curImage);
                documentControl1.Invalidate(curImage.GetBounds());
            }
        }

        private void documentControl1_MouseMove(object sender, MouseEventArgs e) {
            int nn = 0;
            nn++;
        }

        private void documentControl1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F7) {
                MessageBox.Show(e.KeyCode.ToString());
            }
        }

        private void 地图ToolStripMenuItem_Click(object sender, EventArgs e) {
            (mapview as MapViewGoogle).SetTileType(1);
            documentControl1.Refresh();
        }

        private void 卫星ToolStripMenuItem_Click(object sender, EventArgs e) {
            (mapview as MapViewGoogle).SetTileType(2);
            documentControl1.Refresh();
        }

        private void 地形ToolStripMenuItem_Click(object sender, EventArgs e) {
            (mapview as MapViewGoogle).SetTileType(8);
            documentControl1.Refresh();
        }

        private void 重新下载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (mapview as MapViewBase).ShowMapInfo = true;
            int nScale = 0;
            float nn = 1;
            nScale = mapview.Getlevel(documentControl1.ScaleRatio);
            //if(isIn)
            //nScale = mapview.Getlevel(documentControl1.ScaleRatio, out nn);
            //else
            //nScale = mapview.Getlevel2(documentControl1.ScaleRatio, out nn);
            if (nScale == -1) return;


            LongLat longlat = LongLat.Empty;
            //计算中心点经纬度
            //longlat = mapview.OffSet(mapview.ZeroLongLat, nScale, (int)(-200 / nn), (int)(-500 / nn));

            //downFlag = true;
            //for (double j = -1.8; j < 1.8; j = j + 0.03)
            //{
            //    for (double i = -1.3; i < 1.3; i = i + 0.01)
            //    {

            //        (mapview as MapViewGoogle).DownImages(14400, 9000, nScale, 107.8+j, 24.3+i);
            //        downFlag = true;
            //        (mapview as MapViewGoogle).OnDownCompleted += new DownCompleteEventHandler(DownImag);
            //        while (downFlag)
            //        { 
            //        }

            //    }
            //}
            //(mapview as MapViewGoogle).DownImages(320000, 140000, nScale, 107.79, 24.5);

        }
	}
}
