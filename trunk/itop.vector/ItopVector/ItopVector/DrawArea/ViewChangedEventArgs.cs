using System;
using ItopVector.Core.Interface;
using ItopVector.Core;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace ItopVector.DrawArea
{
	/// <summary>
	/// 视图变化事件数据
	/// </summary>
	public class ViewChangedEventArgs:EventArgs
	{
		public ViewChangedEventArgs(Viewer view)
		{
            View = view;
			Bounds =RectangleF.Empty;
			this.create();
		}

		private void create()
		{
            float scale = this.View.ScaleUnit;
			SizeF margin = View.margin;
			float left =margin.Width -16f -this.View.VirtualLeft ;
			float top = margin.Height -16f -this.View.VirtualTop ;
			float width =0,height=0;
            //if(left>0)
            //{
            //    width =Math.Max(0, this.View.Width -16 - left);				
            //}
            //else
			{
				width = this.View.Width -32;
			}
            //if(top>0)
            //{
            //    height = Math.Max(0,this.View.Height -16- top);
            //}
            //else
			{
				height = this.View.Height -32;
			}		
		
			Bounds.Width = width/scale;
			Bounds.Height = height/scale;

            //if(left<0)
			{
				Bounds.X = -left;
			}
            //if(top<0)
			{
				Bounds.Y = -top;
			}
			Bounds.X /=scale;
			Bounds.Y /=scale;
		}

		public Viewer View;

		/// <summary>
		/// 当前视图可见范围
		/// </summary>
		public RectangleF Bounds;
	}
}
