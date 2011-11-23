using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using ItopVector.Core;
using System.Drawing.Drawing2D;
namespace ItopVector
{
	/// <summary>
	/// 图形缩略图
	/// </summary>
	/// 
	[ToolboxItem(true)]
	public class MiniatureView : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label viewPic;

		public MiniatureView()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();
			this.viewPic.Cursor = Cursors.SizeAll;

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
            this.viewPic = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // viewPic
            // 
            this.viewPic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.viewPic.BackColor = System.Drawing.SystemColors.ControlDark;
            this.viewPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewPic.Location = new System.Drawing.Point(0, 0);
            this.viewPic.Name = "viewPic";
            this.viewPic.Size = new System.Drawing.Size(255, 220);
            this.viewPic.TabIndex = 5;
            this.viewPic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.viewPic_MouseDown);
            this.viewPic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.viewPic_MouseMove);
            this.viewPic.Paint += new System.Windows.Forms.PaintEventHandler(this.viewPic_Paint);
            this.viewPic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.viewPic_MouseUp);
            // 
            // MiniatureView
            // 
            this.Controls.Add(this.viewPic);
            this.Name = "MiniatureView";
            this.Size = new System.Drawing.Size(256, 224);
            this.ResumeLayout(false);

		}
		#endregion

		


		#region 字段

		private ItopVectorControl vectorControl;
		Point startPoint; 
		private int margin=4;//缩进大小
		private float scale;//缩放比例
		private float offX;
		private float offY;
		private RectangleF viewBounds;

		private ItopVector.DrawArea.Viewer view;

		#endregion

		#region 属性
		public ItopVectorControl VectorControl
		{
			get
			{
				return vectorControl;
			}
			set
			{
				if(value == null || value == vectorControl)return;

				if(vectorControl!=null)
				{
					vectorControl.DocumentChanged-=new OnDocumentChangedEventHandler(vectorControl_DocumentChanged);
					vectorControl.DrawArea.ViewChanged-=new ItopVector.DrawArea.ViewChangedEventHandler(DrawArea_ViewChanged);
				}
				vectorControl = value;
				vectorControl.DocumentChanged+=new OnDocumentChangedEventHandler(vectorControl_DocumentChanged);
				vectorControl.DrawArea.ViewChanged+=new ItopVector.DrawArea.ViewChangedEventHandler(DrawArea_ViewChanged);
				view = vectorControl.DrawArea.viewer;
				create();
				this.viewPic.Invalidate();

			}
		}
		#endregion

		#region 方法
		private void create()
		{
			float fWidth=0f;
			float fHeight=0f;
			float fX=0f;
			float fY=0f;

			float scale = this.view.ScaleUnit;
			float left = 484 -this.view.VirtualLeft ;
			float top = 484 -this.view.VirtualTop ;
			float width =0,height=0;
			if(left>0)
			{
				width =Math.Max(0, this.view.Width -16 - left);				
			}
			else
			{
				width = this.view.Width -32;
			}
			if(top>0)
			{
				height = Math.Max(0,this.view.Height -16- top);
			}
			else
			{
				height = this.view.Height -32;
			}		
		
			 fWidth = width/scale;
			 fHeight = height/scale;

			if(left<0)
			{
				fX = -left;
			}
			if(top<0)
			{
				fY = -top;
			}
			fX /=scale;
			fY /=scale;
			viewBounds=new RectangleF(fX,fY,fWidth,fHeight);
		}
		/// <summary>
		/// 计算缩放比例
		/// </summary>
		/// <param name="rtf1"></param>
		/// <param name="rtf2"></param>
		/// <returns></returns>
		protected float GetScaleSingle(SizeF rtf1,SizeF rtf2)
		{
			float single1=1f;

			
			float single2 = (rtf1.Width -0)/rtf2.Width;
			float single3 = (rtf1.Height -0)/rtf2.Height;
			single1 = Math.Min(single2,single3);

			if(single2<single3)
			{
				offX = 0;
                offY = 0;// Math.Max(0, (rtf1.Height - 12 - single1 * rtf2.Height) / 2);
			}
			else
			{
                offX = 0;// Math.Max(0, (rtf1.Width - 12 - single1 * rtf2.Width) / 2);
				offY = 0;
			}
            //offX+=margin;
            //offY+=margin;

			return single1;
		}	
	
		/// <summary>
		/// 绘制缩略图
		/// </summary>
		/// <param name="g"></param>
		/// <param name="doc"></param>
		private void DrawPic(Graphics g ,SvgDocument doc)
		{
			if (doc==null&& doc.RootElement ==null) return;
			SVG svgroot = doc.RootElement as SVG;
			if(svgroot == null)return;
			RectangleF rtf1;
			
            rtf1 = svgroot.ContentBounds;
            SizeF sf1 = vectorControl.DocumentSize;
            if (!rtf1.IsEmpty)
                sf1 = rtf1.Size;
            else
                rtf1.Size = sf1;
			SizeF sf2 = this.viewPic.Size;

            scale = GetScaleSingle(sf2, rtf1.Size);
			
			g.DrawRectangle(Pens.Black,offX,offY,scale*sf1.Width+1 ,scale*sf1.Height+1 );
			g.FillRectangle(Brushes.White,offX,offY,scale*sf1.Width,scale*sf1.Height);

			using(Matrix matrix1 =new Matrix())
			{
                matrix1.Translate(-rtf1.X, -rtf1.Y, MatrixOrder.Prepend);
				matrix1.Scale(scale,scale,MatrixOrder.Append);
                matrix1.Translate(offX, offY, MatrixOrder.Append);
				Matrix matrix2 = svgroot.GraphTransform.Matrix.Clone();
				svgroot.GraphTransform.Matrix=matrix1;

				if(svgroot!=null)
				{
					svgroot.Draw(g,0);
				}
				svgroot.GraphTransform.Matrix = matrix2;
			}
			RectangleF rtf3 =this.viewBounds;
            rtf3.Width *= scale;
            rtf3.Height *= scale;
            //rtf3.Offset(-rtf1.X, -rtf1.Y);
            rtf3.X *= scale;
            rtf3.Y *= scale;
            rtf3.Offset(offX, offY);
            
			Pen tempPen=new Pen(Color.Blue,2);
            g.TranslateTransform(-rtf1.X * scale, -rtf1.Y * scale);
			g.DrawRectangle(tempPen,Rectangle.Round(rtf3));
			
		}


		#endregion

		#region 响应事件
        Point startPoint2 = Point.Empty;
		private void viewPic_MouseDown(object sender, MouseEventArgs e)
		{
			this.startPoint = Point.Empty;
			if (e.Button == MouseButtons.Left)
			{
				this.startPoint = new Point(e.X, e.Y);
                startPoint2 = startPoint;
			}
			
		}

		private void viewPic_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
                int num3 = this.startPoint.X - e.X;
                int num4 = this.startPoint.Y - e.Y;
				if(view !=null && (Math.Abs(num3)>0 || Math.Abs(num4)>0))
				{
                    num3 = (int)(num3 / scale * view.ScaleUnit);
                    num4 = (int)(num4 / scale * view.ScaleUnit);
                    RectangleF rf1 = oldViewBounds;
                    rf1.Offset(-num3, -num4);
                    viewBounds = rf1;
				}
                this.viewPic.Invalidate();
			}
		}
		

		private void viewPic_Paint(object sender, PaintEventArgs e)
		{
			if(vectorControl==null)
			{
				StringFormat formate1 =new StringFormat();
				formate1.LineAlignment =StringAlignment.Center;
				formate1.Alignment = StringAlignment.Center;
				e.Graphics.Clear(Color.White);
				e.Graphics.DrawString("缩略图",new Font("宋体",15),new SolidBrush(Color.Blue),new RectangleF(0,0,viewPic.Width,viewPic.Height),formate1);
				return;
			}
			this.DrawPic(e.Graphics,this.vectorControl.SVGDocument);

		}		

		private void vectorControl_DocumentChanged(object sender, DocumentChangedEventArgs e)
		{
			this.viewPic.Invalidate();
		}
        RectangleF oldViewBounds = RectangleF.Empty;
		private void DrawArea_ViewChanged(object sender, ItopVector.DrawArea.ViewChangedEventArgs e)
		{
			view = e.View;
			viewBounds =oldViewBounds= e.Bounds;
			this.viewPic.Invalidate();
		}
	    
		#endregion

        private void viewPic_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                int num3 = this.startPoint.X - e.X;
                int num4 = this.startPoint.Y - e.Y;
                if (view != null && (Math.Abs(num3) > 0 || Math.Abs(num4) > 0)) {
                    num3 = (int)(num3 / scale * view.ScaleUnit);
                    num4 = (int)(num4 / scale * view.ScaleUnit);

                    this.vectorControl.DrawArea.MovePicture(view.VirtualLeft - num3, view.VirtualTop - num4, false);
                    this.vectorControl.Update();            
                    this.vectorControl.DrawArea.SetScrollDelta(-num3, -num4);
                }
            }
            if(this.vectorControl!=null)
            this.vectorControl.Invalidate(true);
        }
	}
}
