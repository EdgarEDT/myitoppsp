namespace ItopVector.Core.Func
{
	using ItopVector.Core;
	using ItopVector.Core.Document;
	using ItopVector.Core.Paint;
	using ItopVector.Core.Interface.Figure;
	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Xml;

	internal class BrushManager
	{
		// Methods
		public BrushManager()
		{
		}

		public static Brush GetGDIBrushFromSvgBrush(ISvgBrush brush)
		{
			if (brush is SolidColor)
			{
				return new SolidBrush(((SolidColor) brush).Color);
			}
			SvgElementCollection collection1 = null;
			if (brush is LinearGradient)
			{
				collection1 = ((LinearGradient) brush).Stops;
			}
			else
			{
				collection1 = ((RadialGradients) brush).Stops;
			}
			ColorBlend blend1 = new ColorBlend(collection1.Count);
			Color[] colorArray1 = new Color[collection1.Count];
			float[] singleArray1 = new float[collection1.Count];
			SvgElementCollection collection2 = collection1;
			for (int num1 = 0; num1 < collection2.Count; num1++)
			{
				GradientStop stop1 = (GradientStop) collection2[num1];
				int num2 = 0xff;
				if ((stop1.Opacity >= 0f) && (stop1.Opacity <= 255f))
				{
					if (stop1.Opacity <= 1f)
					{
						num2 = (int) (stop1.Opacity * 255f);
					}
					else
					{
						num2 = (int) stop1.Opacity;
					}
				}
				Color color1 = stop1.Color;
				float single1 = Math.Min((float) 1f, Math.Max((float) 0f, stop1.ColorOffset));
				colorArray1[num1] = color1;
				singleArray1[num1] = single1;
			}
			float[] singleArray2 = (float[]) singleArray1.Clone();
			Color[] colorArray2 = (Color[]) colorArray1.Clone();
			Array.Sort(singleArray2, colorArray2);
			Color color2 = colorArray2[0];
			Color color3 = colorArray2[colorArray2.Length - 1];
			if (singleArray2[0] != 0f)
			{
				float[] singleArray3 = (float[]) singleArray2.Clone();
				Color[] colorArray3 = (Color[]) colorArray2.Clone();
				singleArray2 = new float[singleArray2.Length + 1];
				colorArray2 = new Color[colorArray2.Length + 1];
				colorArray3.CopyTo(colorArray2, 1);
				singleArray3.CopyTo(singleArray2, 1);
				singleArray2[0] = 0f;
				colorArray2[0] = color2;
			}
			if (singleArray2[singleArray2.Length - 1] != 1f)
			{
				float[] singleArray4 = (float[]) singleArray2.Clone();
				Color[] colorArray4 = (Color[]) colorArray2.Clone();
				singleArray2 = new float[singleArray2.Length + 1];
				singleArray4.CopyTo(singleArray2, 0);
				singleArray2[singleArray2.Length - 1] = 1f;
				colorArray2 = new Color[colorArray2.Length + 1];
				colorArray4.CopyTo(colorArray2, 0);
				colorArray2[colorArray2.Length - 1] = color3;
			}
			if (brush is RadialGradients)
			{
				Array.Reverse(colorArray2);
			}
			Brush brush1 = null;
			blend1.Colors = colorArray2;
			blend1.Positions = singleArray2;
			if (brush is LinearGradient)
			{
				brush1 = new LinearGradientBrush(new Point(0, 0), new Point(1, 0), color2, color3);
				((LinearGradientBrush) brush1).WrapMode = WrapMode.Tile;
				((LinearGradientBrush) brush1).InterpolationColors = blend1;
			}
			else
			{
				GraphicsPath path1 = new GraphicsPath();
				path1.AddEllipse(0, 0, 1, 1);
				brush1 = new PathGradientBrush(path1);
				((PathGradientBrush) brush1).WrapMode = WrapMode.Tile;
				((PathGradientBrush) brush1).InterpolationColors = blend1;
			}
			return brush1;
		}

		public static ISvgBrush GetSvgBrushFromGDIBrush(Brush brush, SvgDocument doc, string id)
		{
			if (brush == null)
			{
				return new SolidColor(Color.Empty);
			}
			if (brush is SolidBrush)
			{
				Color color1 = ((SolidBrush) brush).Color;
				SolidColor color2 = new SolidColor(Color.FromArgb(color1.R, color1.G, color1.B));
				color2.Opacity = ((float) color1.A) / 255f;
				return color2;
			}
			ColorBlend blend1 = null;
			ISvgBrush brush1 = null;
			if (brush is PathGradientBrush)
			{
				blend1 = ((PathGradientBrush) brush).InterpolationColors;
				brush1 = (RadialGradients) doc.CreateElement(doc.Prefix, "radialGradient", doc.NamespaceURI);
			}
			else
			{
				blend1 = ((LinearGradientBrush) brush).InterpolationColors;
				brush1 = (LinearGradient) doc.CreateElement(doc.Prefix, "linearGradient", doc.Name);
			}
			int num1 = 0;
			Color[] colorArray1 = blend1.Colors;
			for (int num2 = 0; num2 < colorArray1.Length; num2++)
			{
				Color color3 = colorArray1[num2];
				GradientStop stop1 = (GradientStop) doc.CreateElement(doc.Prefix, "stop", doc.NamespaceURI);
				stop1.Color = Color.FromArgb(color3.R, color3.G, color3.B);
				stop1.Opacity = ((float) color3.A) / 255f;
				stop1.ColorOffset = blend1.Positions[num1];
				((SvgElement) brush1).AppendChild(stop1);
				num1++;
			}
			((SvgElement) brush1).ID = id;
			return brush1;
		}
		public static Brush GetGDIBrushFromPatten(ItopVector.Struct.Pattern pattern, RectangleF rect)
		{
			if (pattern.PatternType != PatternType.None)
			{
				if (pattern.PatternType < PatternType.Center)
				{
					HatchStyle style1 = (HatchStyle) System.Enum.Parse(typeof(HatchStyle), pattern.PatternType.ToString(), false);
					return new HatchBrush(style1, pattern.ForeColor, pattern.BackColor);
				}
				Brush brush1 = null;
				ColorBlend blend1 = new ColorBlend();
				using (GraphicsPath path1 = new GraphicsPath())
				{
					float[] singleArray1;
					Color[] colorArray1;
					switch (pattern.PatternType)
					{
						case PatternType.Center:
						{
							path1.AddEllipse(rect.X, rect.Y, rect.Width, rect.Height);
							brush1 = new PathGradientBrush(path1);
							singleArray1 = new float[2];
							singleArray1[1] = 1f;
							blend1.Positions = singleArray1;
							colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor } ;
							blend1.Colors = colorArray1;
							((PathGradientBrush) brush1).InterpolationColors = blend1;
							((PathGradientBrush) brush1).CenterPoint = new PointF(rect.X + (((float) rect.Width) / 2f), rect.Y + (((float) rect.Height) / 2f));
							goto Label_0496;
						}
						case PatternType.DiagonalLeft:
						{
							brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.ForwardDiagonal);
							singleArray1 = new float[3];
							singleArray1[1] = 0.5f;
							singleArray1[2] = 1f;
							blend1.Positions = singleArray1;
							colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor, pattern.BackColor } ;
							blend1.Colors = colorArray1;
							((LinearGradientBrush) brush1).InterpolationColors = blend1;
							goto Label_0496;
						}
						case PatternType.DiagonalRight:
						{
							brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.BackwardDiagonal);
							singleArray1 = new float[3];
							singleArray1[1] = 0.5f;
							singleArray1[2] = 1f;
							blend1.Positions = singleArray1;
							colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor, pattern.BackColor } ;
							blend1.Colors = colorArray1;
							((LinearGradientBrush) brush1).InterpolationColors = blend1;
							goto Label_0496;
						}
						case PatternType.HorizontalCenter:
						{
							brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.Horizontal);
							singleArray1 = new float[3];
							singleArray1[1] = 0.5f;
							singleArray1[2] = 1f;
							blend1.Positions = singleArray1;
							colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor, pattern.BackColor } ;
							blend1.Colors = colorArray1;
							((LinearGradientBrush) brush1).InterpolationColors = blend1;
							goto Label_0496;
						}
						case PatternType.LeftRight:
						{
							brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.Horizontal);
							singleArray1 = new float[2];
							singleArray1[1] = 1f;
							blend1.Positions = singleArray1;
							colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor } ;
							blend1.Colors = colorArray1;
							((LinearGradientBrush) brush1).InterpolationColors = blend1;
							goto Label_0496;
						}
						case PatternType.TopBottom:
						{
							break;
						}
						case PatternType.VerticalCenter:
						{
							brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.Vertical);
							singleArray1 = new float[3];
							singleArray1[1] = 0.5f;
							singleArray1[2] = 1f;
							blend1.Positions = singleArray1;
							colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor, pattern.BackColor } ;
							blend1.Colors = colorArray1;
							((LinearGradientBrush) brush1).InterpolationColors = blend1;
							goto Label_0496;
						}
						default:
						{
							goto Label_0496;
						}
					}
					brush1 = new LinearGradientBrush(rect, pattern.BackColor, pattern.ForeColor, LinearGradientMode.Vertical);
					singleArray1 = new float[2];
					singleArray1[1] = 1f;
					blend1.Positions = singleArray1;
					colorArray1 = new Color[] { pattern.BackColor, pattern.ForeColor } ;
					blend1.Colors = colorArray1;
					((LinearGradientBrush) brush1).InterpolationColors = blend1;
				Label_0496:
					blend1 = null;
					return brush1;
				}
			}
			return null;

		}

		public static ISvgBrush Parsing(string text, SvgDocument doc)
		{
			while (text.Trim().EndsWith(";"))
			{
				text = text.Substring(0, text.Length - 1);
			}
			if (text.Trim().StartsWith("url"))
			{
				int num1 = text.Trim().IndexOf("#", 0, text.Trim().Length);
				int num2 = text.Trim().IndexOf(")", 0, text.Trim().Length);
				string text1 = text.Trim().Substring(num1 + 1, (num2 - num1) - 1);
				XmlNode node1 = NodeFunc.GetRefNode(text1, doc);
				if (node1 != null)
				{
					if (node1.NodeType != XmlNodeType.Element)
					{
						return null;
					}
					XmlElement element1 = (XmlElement) node1;
					if (element1 != null)
					{
						return (ISvgBrush) element1;
					}
				}
				return null;
			}
			
			return new SolidColor(ColorFunc.ParseColor(text));
		}
		public static ISvgBrush Parsing(string text, SvgDocument doc,IGraphPath path)
		{
			while (text.Trim().EndsWith(";"))
			{
				text = text.Substring(0, text.Length - 1);
			}
			if (text.Trim().StartsWith("url"))
			{
				int num1 = text.Trim().IndexOf("#", 0, text.Trim().Length);
				int num2 = text.Trim().IndexOf(")", 0, text.Trim().Length);
				string text1 = text.Trim().Substring(num1 + 1, (num2 - num1) - 1);
				XmlNode node1 = NodeFunc.GetRefNode(text1, doc);
				if (node1 != null)
				{
					if (node1.NodeType != XmlNodeType.Element)
					{
						return null;
					}
					XmlElement element1 = (XmlElement) node1;
					if (element1 != null)
					{
						return (ISvgBrush) element1;
					}
				}
				return null;
			}
			string text7 ="none";// AttributeFunc.ParseAttribute("hatch-style",(SvgElement) path,false).ToString();
			if(((SvgElement)path).SvgAttributes.ContainsKey("hatch-style"))
			{
				text7=((SvgElement)path).SvgAttributes["hatch-style"].ToString();
			}
			if((text7 != "") && (text7.ToLower() != "none") && (text!="none"))
			{
				Color color1=ColorFunc.ParseColor(text);
				Color color2=ColorFunc.ParseColor(AttributeFunc.ParseAttribute("hatch-color",(SvgElement)path,false).ToString());

				Struct.Pattern pattern =new ItopVector.Struct.Pattern(color1,(ItopVector.PatternType) Enum.Parse(typeof(ItopVector.PatternType),text7,false),color2);

				return new RatHatchBrush(pattern);

			}
			
			return new SolidColor(ColorFunc.ParseColor(text));
		}
	}
}

