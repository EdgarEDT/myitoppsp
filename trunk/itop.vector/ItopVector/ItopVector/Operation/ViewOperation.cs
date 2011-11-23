namespace ItopVector.DrawArea
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;
	using System.Drawing.Drawing2D;

	internal class ViewOperation : IOperation
	{
		// Methods
		public ViewOperation()
		{
			this.mouseAreaControl = null;
			this.startPoint = Point.Empty;
			this.oldleft = 0f;
			this.oldtop = 0f;
			this.reversePath =new GraphicsPath();
			this.win32 = new Win32();
		}

		internal ViewOperation(MouseArea mousearea):this()
		{			
			this.mouseAreaControl = mousearea;
			this.win32 = mousearea.win32;
		}

		public void Dispose()
		{
			this.mouseAreaControl.ViewOperation = null;
		}
		int ncount=0;
		public void OnMouseWheel(MouseEventArgs e)
		{			
			ncount+=e.Delta;

			if(Math.Abs(ncount)>60)
			{
				if (e.Delta > 0)
				{
					this.mouseAreaControl.PicturePanel.ScaleRatio*=1.2f;
				}
				else
				{
					this.mouseAreaControl.PicturePanel.ScaleRatio*=0.9f;

				}
				ncount=0;
			}
		}
		public void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button== MouseButtons.Right)return;
            if (this.mouseAreaControl.CurrentOperation == ToolOperation.Roam)
			{
				this.mouseAreaControl.PicturePanel.viewer.BeginMove();
			}			
			this.mouseAreaControl.PicturePanel.UpdateRule = false;
			this.startPoint = new Point(e.X,e.Y);
			this.oldleft = this.mouseAreaControl.PicturePanel.VirtualLeft;
			this.oldtop = this.mouseAreaControl.PicturePanel.VirtualTop;
		}

		public void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (this.mouseAreaControl.CurrentOperation == ToolOperation.Roam)
				{
					Point point1 = new Point(e.X,e.Y);
					int num1 = point1.X - this.startPoint.X;
					int num2 = point1.Y - this.startPoint.Y;

					this.mouseAreaControl.PicturePanel.MovePicture(this.oldleft - num1,this.oldtop - num2,true);
					this.mouseAreaControl.PicturePanel.viewer.MovePic(num1,num2);
				}
				else if (this.mouseAreaControl.CurrentOperation==ToolOperation.IncreaseView )
				{
                    this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
                    this.win32.W32SetROP2(7);
                    this.win32.W32PolyDraw(this.reversePath);
                    this.reversePath.Reset();

					PointF pf2 = new PointF((float) e.X, (float) e.Y);
					float single9 = Math.Min(this.startPoint.X, pf2.X);
					float single10 = Math.Min(this.startPoint.Y, pf2.Y);
					float single11 = Math.Max(this.startPoint.X, pf2.X);
					float single12 = Math.Max(this.startPoint.Y, pf2.Y);
					this.reversePath.AddRectangle(new RectangleF(single9, single10, single11 - single9, single12 - single10));
                    this.win32.W32PolyDraw(this.reversePath);
                    this.win32.ReleaseDC();
				}
			}
		}

		public void OnMouseUp(MouseEventArgs e)
		{
            
			if(e.Button==MouseButtons.Right)return;
			this.mouseAreaControl.PicturePanel.UpdateRule = true;
			if (this.mouseAreaControl.CurrentOperation == ToolOperation.Roam)
			{
				this.mouseAreaControl.PicturePanel.viewer.EndMove();
				Point point1 = new Point(e.X,e.Y);
				int num1 = point1.X - this.startPoint.X;
				int num2 = point1.Y - this.startPoint.Y;
				if (num1==0 && num2==0)return;
				this.mouseAreaControl.PicturePanel.MovePicture(this.oldleft - num1,this.oldtop - num2,false);
				this.mouseAreaControl.PicturePanel.SetScrollDelta(-num1, -num2);

			}
			else
			{
                float scale=1.2f;
                float num1=0;
                float num2=0;
                
				switch (this.mouseAreaControl.CurrentOperation)
				{
					case ToolOperation.IncreaseView:
					{
                        this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
                        this.win32.W32SetROP2(7);
                        this.win32.W32PolyDraw(this.reversePath);
                        this.reversePath.Reset();
                        this.win32.ReleaseDC();
                        
                        PointF pf2 = new PointF((float)e.X, (float)e.Y);
                        float single9 = Math.Min(this.startPoint.X, pf2.X);
                        float single10 = Math.Min(this.startPoint.Y, pf2.Y);
                        float single11 = Math.Max(this.startPoint.X, pf2.X);
                        float single12 = Math.Max(this.startPoint.Y, pf2.Y);
                        PointF point3 = new PointF(this.mouseAreaControl.PicturePanel.Width / 2, this.mouseAreaControl.PicturePanel.Height / 2);
                        PointF point2 = new PointF((single9 + (single11 - single9) / 2),(single10 + (single12 - single10) / 2));
                        
                        num1 = (point3.X - point2.X);
                        num2 = (point3.Y - point2.Y);
                        //this.mouseAreaControl.PicturePanel.GoLocation(point2.X, point2.Y);

                        this.mouseAreaControl.PicturePanel.MovePicture(this.oldleft - num1, this.oldtop - num2, false);
                        ////this.mouseAreaControl.PicturePanel.viewer.MovePic(num1, num2);
                        this.mouseAreaControl.PicturePanel.SetScrollDelta(-(int)num1, -(int)num2);
                       
                        this.mouseAreaControl.PicturePanel.Update();
                        
                        //}  
                        float f1 = single11 - single9;
                        float f2 = single12 - single10;
                        float rectArea = Math.Abs((single11-single9)*(single12-single10));
                        float rectLen = (this.mouseAreaControl.PicturePanel.Width - f1) < (this.mouseAreaControl.PicturePanel.Height - f2) ? f1 : f2;
                        if (rectArea<=10)
                        {
                            scale = 1.2f;
                        } 
                        else
                        {
                            if (rectLen==f1)
                            {
                                scale =(this.mouseAreaControl.PicturePanel.Width-200) / f1;
                            } 
                            else if (rectLen==f2)
                            {
                                scale = (this.mouseAreaControl.PicturePanel.Height-160) / f2;
                            }
                            
                        }
						if (e.Button==MouseButtons.Left)
						{
							goto Label_IncreaseView;
						}                      
                        break;
					}
					case ToolOperation.DecreaseView:
					{
						if (e.Button==MouseButtons.Left)
						{
							goto Label_DecreaseView;
						}					
						break;
					}
				}
				goto Label_Root;
            Label_IncreaseView:           
				ItopVector.DrawArea.DrawArea area1 = this.mouseAreaControl.PicturePanel;
                area1.ScaleUnit *= scale;
                goto Label_Root;
			Label_DecreaseView:
				ItopVector.DrawArea.DrawArea area2 = this.mouseAreaControl.PicturePanel;
				area2.ScaleUnit *= 0.9f;
			Label_Root:                
				return;            
			}
		}

		public void OnPaint(PaintEventArgs e)
		{
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
		private MouseArea mouseAreaControl;
		private float oldleft;
		private float oldtop;
		private Point startPoint;
		private GraphicsPath reversePath;
		private Win32 win32;
	}
}

