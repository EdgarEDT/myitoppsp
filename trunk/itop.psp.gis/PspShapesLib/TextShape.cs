using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization.Formatters.Soap;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Netron.GraphLib.Attributes;
using Netron.GraphLib.UI;
using Netron.GraphLib.Interfaces;
using Netron.GraphLib;
namespace ShapesLib
{
	
	[Serializable]
    [Description("文字")]
    [NetronGraphShape("文字", "57AF94BA-4129-45dc-B8FD-000000000003", "其它", "ShapesLib.TextShape",
      "文字")]
    public class TextShape : Shape, ISerializable
	{		
		#region Constructors
		
        public TextShape()  {
            Rectangle = new RectangleF(0, 0, 70, 20);
            Font =new Font("宋体",9);
            Text = "文字";
            ShapeColor = Color.Transparent;
        }
		
		public TextShape(IGraphSite site) : base(site){}
		
        protected TextShape(SerializationInfo info, StreamingContext context) : base(info, context) { }
		#endregion	

		#region Methods
        public override Bitmap GetThumbnail() {
            Bitmap bmp = null;
            try {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ShapesLib.Resources.TextShape.bmp");

                bmp = Bitmap.FromStream(stream) as Bitmap;
                stream.Close();
                stream = null;
            } catch (Exception exc) {
                Trace.WriteLine(exc.Message, "TextShape.GetThumbnail");
            }
            return bmp;
        }
        public override void AddProperties() {
            base.AddProperties();
            Bag.Properties.Remove("URL");
            Bag.Properties.Add(new PropertySpec("Font","字体", typeof(System.Drawing.Font),"文字",""));
            Bag.Properties.Add(new PropertySpec("TextColor", "颜色", typeof(System.Drawing.Color), "文字", ""));
        }
        protected override void GetPropertyBagValue(object sender, PropertySpecEventArgs e) {
            base.GetPropertyBagValue(sender, e);
            switch (e.Property.Name){
                case "Font":
                    e.Value = this.Font;
                    break;
                case "TextColor":
                    e.Value = this.TextColor;
                    break;
            }
        }
        protected override void SetPropertyBagValue(object sender, PropertySpecEventArgs e) {
            base.SetPropertyBagValue(sender, e);
            switch (e.Property.Name){
                case "Font":
                    this.Font= e.Value as Font;
                    break;
                case "TextColor":
                    this.TextColor = (Color)e.Value ;
                    break;
            }
        }
		
		public override void Paint(Graphics g)
		{
            if (RecalculateSize) {
                Rectangle = new RectangleF(new PointF(Rectangle.X, Rectangle.Y),
                    g.MeasureString(this.Text, Font));
                Rectangle = System.Drawing.RectangleF.Inflate(Rectangle, 10, 10);
                RecalculateSize = false; //very important!
            }
            g.FillRectangle(new SolidBrush(ShapeColor), Rectangle);
            if (ShowLabel) {
                g.DrawString(Text, Font, TextBrush, Rectangle.X,Rectangle.Y);
            }		
			
		}
		
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context);
		}
		protected  override Brush BackgroundBrush
		{
			get
			{
				return new LinearGradientBrush(Rectangle,Color.WhiteSmoke, this.ShapeColor,LinearGradientMode.Vertical);
			}
		}
		#endregion
	}

}







		
