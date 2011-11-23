using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using System.Drawing.Imaging;
using Itop.MapView;
namespace ItopVectorDraw
{
	/// <summary>
	/// MapControl 的摘要说明。
	/// </summary>
	public class MapControl : System.Windows.Forms.UserControl
	{
		public ItopVector.ItopVectorControl tlVectorControl1;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MapControl()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();
			this.tlVectorControl1.AfterPaintPage+=new ItopVector.DrawArea.PaintMapEventHandler(tlVectorControl1_AfterPaintPage);
this.tlVectorControl1.Operation = ToolOperation.Roam;

			// TODO: 在 InitializeComponent 调用后添加任何初始化

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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MapControl));
			this.tlVectorControl1 = new ItopVector.ItopVectorControl();
			this.SuspendLayout();
			// 
			// tlVectorControl1
			// 
			this.tlVectorControl1.CanEdit = false;
			this.tlVectorControl1.DocumentbgColor = System.Drawing.Color.Empty;
			this.tlVectorControl1.DocumentSize = ((System.Drawing.SizeF)(resources.GetObject("tlVectorControl1.DocumentSize")));
			this.tlVectorControl1.FullDrawMode = false;
			this.tlVectorControl1.IsModified = false;
			this.tlVectorControl1.IsPasteGrid = false;
			this.tlVectorControl1.IsShowGrid = true;
			this.tlVectorControl1.IsShowRule = true;
			this.tlVectorControl1.IsShowTip = false;
			this.tlVectorControl1.Location = new System.Drawing.Point(0, 0);
			this.tlVectorControl1.Name = "tlVectorControl1";
			this.tlVectorControl1.ScaleRatio = 1F;
			this.tlVectorControl1.Scrollable = false;
			this.tlVectorControl1.Size = new System.Drawing.Size(672, 496);
			this.tlVectorControl1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			this.tlVectorControl1.TabIndex = 0;
			// 
			// MapControl
			// 
			this.Controls.Add(this.tlVectorControl1);
			this.Name = "MapControl";
			this.Size = new System.Drawing.Size(480, 368);
			this.ResumeLayout(false);

		}
		#endregion

		public Itop.MapView.IMapViewObj mapview=new Itop.MapView.MapViewObj();
		private void tlVectorControl1_AfterPaintPage(object sender, ItopVector.Core.PaintMapEventArgs e)
		{
			int nScale=0;
			switch((int)(this.tlVectorControl1.DrawArea.ScaleUnit *1000))
			{
				
				case 100:
					nScale =8;
					break;
				case 200:
					nScale =9;
					break;
				case 400:
					nScale =10;
					break;
				case 1000:
					nScale =11;
					break;
				case 2000:
					nScale =12;
					break;
				case 4000:
					nScale =13;
					break;
				default:
					return;
			}
			LongLat longlat=LongLat.Empty;
			//计算中心点经纬度

			int offsetY = (nScale-10 )*25;

            longlat = mapview.OffSet(mapview.ZeroLongLat, nScale, -(int)(e.CenterPoint.X), -(int)(e.CenterPoint.Y));

			//创建地图
			System.Drawing.Image image = mapview.CreateMap(e.Bounds.Width,e.Bounds.Height ,nScale,longlat.Longitude,longlat.Latitude);

			ImageAttributes imageAttributes=new ImageAttributes();
			ColorMatrix matrix1 = new ColorMatrix();		
			matrix1.Matrix00 = 1f;
			matrix1.Matrix11 = 1f;
			matrix1.Matrix22 = 1f;
			matrix1.Matrix33 = 0.9f;//地图透明度
			matrix1.Matrix44 = 1f;
			//设置地图透明度
			imageAttributes.SetColorMatrix(matrix1, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

			//绘制地图
			e.G.DrawImage((Bitmap)image, e.Bounds, 0f, 0f, (float) image.Width, (float) image.Height, GraphicsUnit.Pixel,imageAttributes);

			//绘制中心点
			e.G.DrawEllipse(Pens.Red,e.Bounds.Width/2 -2 ,e.Bounds.Height/2 -2 ,4,4);
			e.G.DrawEllipse(Pens.Red,e.Bounds.Width/2 -1 ,e.Bounds.Height/2 -1 ,2,2);

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

		}
	}
}
