namespace ItopVector.Property
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
	using ItopVector.Core;
	using ItopVector.Core.Func;
	using ItopVector.Design;
	using ItopVector.Core.Figure;

    internal class PropertyRoundRect : PropertyFill
    {
        // Methods
		
        public PropertyRoundRect(SvgElement element) : base(element)
        {
        }
		[Category("Ô²½Ç"), Browsable(true), Editor(typeof(Number180Editor), typeof(UITypeEditor)),Description("ÉèÖÃÍÖÔ²»¡µÄ³¤Öá(xÖá·½Ïò)")]
		public Struct.Float Rx
		{
			get
			{
				RectangleElement element1 =base.svgElement as RectangleElement;
				if(element1!=null)
				{
					return new Struct.Float(element1.RX);
				}
				
				return new Struct.Float(0f);
			}
			set
			{

				RectangleElement element1 =base.svgElement as RectangleElement;
				if(element1!=null)
				{
					if(value.F==0f)
					{
						element1.RX=0f;
						element1.RY = 0f;
						element1.pretime = -1;
						return;
					}
					if(value.F>element1.Width)
					{
						value.F=element1.Width;
					}
					element1.pretime = -1;
					element1.RX = value.F;
					if(this.Ry.F==0f)
					{
						this.Ry=value;
					}
				}
			}
		}
		[Category("Ô²½Ç"), Browsable(true),Editor(typeof(Number180Editor), typeof(UITypeEditor)), Description("ÉèÖÃÍÖÔ²»¡µÄ¶ÌÖá(yÖá·½Ïò)")]
		public Struct.Float Ry
		{
			get
			{
				RectangleElement element1 =base.svgElement as RectangleElement;
				if(element1!=null)
				{
					return new Struct.Float(element1.RY);
				}
				return new Struct.Float(0f);
			}
			set
			{
				
				RectangleElement element1 =base.svgElement as RectangleElement;
				if(element1!=null)
				{
					if(value.F==0f)
					{
						element1.RX=0f;
						element1.RY = 0f;
						element1.pretime = -1;
						return;
					}
					if(value.F>element1.Height)
					{
						value.F=element1.Height;
					}
					element1.pretime = -1;
					element1.RY = value.F;
					if(this.Rx.F==0f)
					{
						this.Rx=value;
					}
				}
			}
		}
	}
}

