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
	/// MapControl ��ժҪ˵����
	/// </summary>
	public class MapControl : System.Windows.Forms.UserControl
	{
		public ItopVector.ItopVectorControl tlVectorControl1;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MapControl()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();
			this.tlVectorControl1.AfterPaintPage+=new ItopVector.DrawArea.PaintMapEventHandler(tlVectorControl1_AfterPaintPage);
this.tlVectorControl1.Operation = ToolOperation.Roam;

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��

		}

		/// <summary> 
		/// ������������ʹ�õ���Դ��
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

		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
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
			//�������ĵ㾭γ��

			int offsetY = (nScale-10 )*25;

            longlat = mapview.OffSet(mapview.ZeroLongLat, nScale, -(int)(e.CenterPoint.X), -(int)(e.CenterPoint.Y));

			//������ͼ
			System.Drawing.Image image = mapview.CreateMap(e.Bounds.Width,e.Bounds.Height ,nScale,longlat.Longitude,longlat.Latitude);

			ImageAttributes imageAttributes=new ImageAttributes();
			ColorMatrix matrix1 = new ColorMatrix();		
			matrix1.Matrix00 = 1f;
			matrix1.Matrix11 = 1f;
			matrix1.Matrix22 = 1f;
			matrix1.Matrix33 = 0.9f;//��ͼ͸����
			matrix1.Matrix44 = 1f;
			//���õ�ͼ͸����
			imageAttributes.SetColorMatrix(matrix1, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

			//���Ƶ�ͼ
			e.G.DrawImage((Bitmap)image, e.Bounds, 0f, 0f, (float) image.Width, (float) image.Height, GraphicsUnit.Pixel,imageAttributes);

			//�������ĵ�
			e.G.DrawEllipse(Pens.Red,e.Bounds.Width/2 -2 ,e.Bounds.Height/2 -2 ,4,4);
			e.G.DrawEllipse(Pens.Red,e.Bounds.Width/2 -1 ,e.Bounds.Height/2 -1 ,2,2);

		{//���Ʊ�����
			Point p1=new Point(20,e.Bounds.Height -30);
			Point p2= new Point(20,e.Bounds.Height -20);
			Point p3=new Point(80,e.Bounds.Height -20);
			Point p4=new Point(80,e.Bounds.Height -30);

			e.G.DrawLines(new Pen(Color.Black,2),new Point[4]{p1,p2,p3,p4});
            string str1 = string.Format("{0}����", mapview.GetMiles(nScale));
			e.G.DrawString(str1, new Font("����", 10), Brushes.Black, 30,e.Bounds.Height -40);
		}
			//			string s = string.Format("{0}��{1}��", nRows, nCols);
			string s = string.Format("��{0}��γ{1}", longlat.Longitude, longlat.Latitude);
			//			//��ʾ���ĵ㾭γ��
			e.G.DrawString(s, new Font("����", 10), Brushes.Red, 20, 40);

		}
	}
}
