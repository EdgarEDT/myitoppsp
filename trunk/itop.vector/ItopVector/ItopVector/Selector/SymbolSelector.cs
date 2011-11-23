namespace ItopVector.Selector
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.IO;
	using System.Windows.Forms;
	using System.Xml;
	using ItopVector.Core.Document;
	using ItopVector.Core;
	using ItopVector.Core.Figure;

	[ToolboxItem(false)]
	public class SymbolSelector : OutlookBar
	{
		SvgDocument document;
		
		// Methods
		public SymbolSelector()
		{

			base.itemHeight = 50;
			this.titleColor = SystemColors.ControlLight;
			this.contentColor =Color.FromArgb(116,194,172);
			//			this.contentColor =Color.FromArgb(140,220,185);

			base.showToolTip = true;
			base.ibDrag = true;
			this.AllowDrop = true;
		}
		public SymbolSelector(string configpath):this()
		{			

			if (File.Exists(configpath))
			{
				try
				{
					SvgDocument doc = SymbolGroup.LoadSymbol(configpath);
					this.InitData(doc);
				}
				catch(Exception e)
				{
					MessageBox.Show(e.Message);
				}
				
				base.CreateShapePanel();				
			}
		}

		public void ReLoad()
		{
			if(this.document!=null )
			{
				try
				{
					SvgDocument doc = SymbolGroup.LoadSymbol(this.document.FilePath);

					this.InitData(doc);
					base.LayoutShapePanel();
					if(this.selectorGroups.Count>0)
					{
						base.GroupItems=this.selectorGroups[0] as IOutlookBarBand;
					}
				}
				catch(Exception e)
				{
					MessageBox.Show(e.Message);
				}

			}
		}
		public new void Load(string filepath)
		{

			try
			{
				SvgDocument doc = SymbolGroup.LoadSymbol(filepath);

				this.InitData(doc);
				if (shapePanel==null)
					base.CreateShapePanel();
				else
				{
					base.LayoutShapePanel();
					if(this.selectorGroups.Count>0)
					{
						base.GroupItems=this.selectorGroups[0] as IOutlookBarBand;
					}
				}
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message);
			}

		}
		public SvgDocument SymbolDoc
		{
			get
			{
				return this.document;
			}
		}
		public void AddSymbol(ItopVector.Core.Figure.Symbol symbol,string parentGroupId)
		{
            if(symbol==null)return;
			XmlNode xmlnode =ItopVector.Core.Func.NodeFunc.GetRefNode(parentGroupId,this.document);
			if(xmlnode!=null)
			{				
				ItopVector.Core.Figure.Symbol newSymbol= xmlnode.AppendChild(this.document.ImportNode(symbol,true)) as ItopVector.Core.Figure.Symbol;
				OutlookBarItemCollection curItem=null;
				foreach( OutlookBarItemCollection item in base.selectorGroups)
				{
					if(item.Id==parentGroupId)
					{
						curItem=item;
						break;
					}
				}
				if(curItem!=null)
				{
					curItem.Add(new Symbol(newSymbol));
					if(curItem==(base.currentGroup as OutlookBarItemCollection))
					{						
						base.shapePanel.Invalidate();
						base.Invalidate();
						base.UpdateAutoScroll();
					}
				}
			}

		}
		public void DeleteGroup(XmlElement element)
		{
			if(element==null)return;
			string id = element.GetAttribute("id");
			if(id!=string.Empty)
			{
				OutlookBarItemCollection curItem=null;
				foreach( OutlookBarItemCollection item in base.selectorGroups)
				{
					if(item.Id==id)
					{
						curItem=item;
						break;
					}
				}
				if(curItem!=null)
				{
					base.selectorGroups.Remove(curItem);
					this.document.DocumentElement.RemoveChild(element);
					base.Invalidate();
				}
			}
		}
		public void AddGroup(XmlElement element)
		{
            if(element==null)return;
			string id=element.GetAttribute("id");
			XmlElement element1 = this.document.ImportNode(element,false) as XmlElement;
			
			this.document.DocumentElement.AppendChild(element1);
			OutlookBarItemCollection items = new OutlookBarItemCollection();
			items.Id = element1.GetAttribute("id");
						
			base.selectorGroups.Add(items);
			base.Invalidate();
		}
		
		public SvgElement SelectedItem
		{
			get{if (this.SelectedObject==null)return null;
				return ((IShape)this.SelectedObject).OwnerElement;}

		}
		private void InitData(SvgDocument svgDocument)
		{
			if (svgDocument != null)
			{
				this.document = svgDocument;
				base.selectorGroups.Clear();
				base.currentGroup=null;
				XmlNodeList list1 = svgDocument.GetElementsByTagName("group");
				for (int num1 = 0; num1 < list1.Count; num1++)
				{
					XmlElement element1 = list1[num1] as XmlElement;
					string text1 = element1.GetAttribute("enabled").Trim().ToLower();
					if ((element1 != null) && (text1 != "false"))
					{
						OutlookBarItemCollection items = new OutlookBarItemCollection();
						items.Id = element1.GetAttribute("id");
						base.selectorGroups.Add(items);
						XmlNodeList list2 = element1.GetElementsByTagName("symbol");
						if(list2.Count==0)
							list2=element1.GetElementsByTagName("connectline",SvgDocument.TonliNamespace);
						for (int num2 = 0; num2 < list2.Count; num2++)
						{
							ItopVector.Core.Figure.Symbol fdbdec1 = list2[num2] as ItopVector.Core.Figure.Symbol;
							if (fdbdec1 != null)
							{
								if(!fdbdec1.Visible)
								{
									//fdbdec1.Attributes.RemoveNamedItem("visibility");
									continue;
								}
								items.Add(new ItopVector.Selector.Symbol(fdbdec1));
								continue;
							}
							ItopVector.Core.Figure.ConnectLine fdbdec2 = list2[num2] as ItopVector.Core.Figure.ConnectLine;
							if (fdbdec2 != null)
							{
								items.Add(new ItopVector.Selector.ConnectShape(fdbdec2));
							}
						}
					}
				}
			}
		}

		protected override void shapePanel_Paint(object sender, PaintEventArgs e)
		{
			int num1 = base.SelectedIndex;
			if (num1 >= 0)
			{
				e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
				OutlookBarItemCollection items = base.selectorGroups[num1] as OutlookBarItemCollection;
				Point point1 = base.shapePanel.AutoScrollPosition;
				num1 = Math.Max(0, (int) (-point1.Y / base.itemHeight));
				int num2 = base.shapePanel.Height / base.itemHeight;
				int num3 = Math.Min(items.Count, (int) ((num1 + num2) + 2));
				e.Graphics.TranslateTransform((float) point1.X, (float) point1.Y);
				using (Matrix matrix1 = new Matrix())
				{
					for (int num4 = num1; num4 < num3; num4++)
					{
						IShape shape1 = items[num4];
						if (shape1 != null)
						{
							Color color1 = base.contentColor;
							Color color2 = Color.Black;
							bool flag1 = false;
							if (shape1 == (base.SelectedObject as IShape))
							{
								color1 = SystemColors.Highlight;
								color2 = SystemColors.HighlightText;
								flag1 = true;
							}
							Rectangle rectangle1 = base.GetItemRect(num4);
							int num5 = 5;
							Rectangle rectangle2 = new Rectangle(rectangle1.X + (2 * num5), rectangle1.Y + (2 * num5), rectangle1.Height - (4 * num5), rectangle1.Height - (4 * num5));
							using (Pen pen1 = new Pen(Color.Black))
							{
								using (Brush brush1 = new SolidBrush(color1))
								{
									GraphicsPath path1 = null;
									if (shape1 is ConnectShape)//Á¬½ÓÏß
									{
										path1 = (shape1 as ConnectShape).GraphPath;
									}
									else if (shape1 is Symbol)
									{
										path1 = (shape1 as Symbol).GraphPath;
										//path1.AddRectangle(new Rectangle(20,20,40,40));
										//(shape1 as Symbol).SymbolElemnet.Draw(e.Graphics,0);
									}
									if ((path1 != null) && (path1.PointCount > 1))
									{
										matrix1.Reset();
										using (StringFormat format1 = new StringFormat(StringFormat.GenericTypographic))
										{
											format1.LineAlignment = StringAlignment.Far;
											using (GraphicsPath path2 = (path1.Clone() as GraphicsPath))
											{
												path2.Flatten();
												RectangleF ef1 = path2.GetBounds();
												if (ef1.Width == 0f)
												{
													ef1.Width = rectangle2.Width;
												}
												if (ef1.Height == 0f)
												{
													ef1.Height = rectangle2.Height;
												}
												if (!ef1.IsEmpty)
												{
													float single1 = ef1.X + (ef1.Width / 2f);
													float single2 = ef1.Y + (ef1.Height / 2f);
													using (Brush brush2 = new SolidBrush(color2))
													{
														using (Pen pen2 = new Pen(color2))
														{
															if (flag1)
															{
																Rectangle rectangle3 = new Rectangle(rectangle1.X, rectangle1.Y, rectangle1.Height, rectangle1.Height);
																e.Graphics.DrawRectangle(pen1, rectangle3);
																path2.Reset();
																path2.AddRectangle(rectangle3);
																path2.AddRectangle(new Rectangle(rectangle1.X + num5, rectangle1.Y + num5, rectangle1.Height - (2 * num5), rectangle1.Height - (2 * num5)));
																path2.FillMode = FillMode.Alternate;
																path2.Transform(matrix1);
																e.Graphics.FillPath(brush2, path2);
															}
															matrix1.Translate((rectangle2.X + (((float) rectangle2.Width) / 2f)) - single1, (rectangle2.Y + (((float) rectangle2.Height) / 2f)) - single2);
															matrix1.Translate(single1, single2);
															float single3 = Math.Min((float) (((float) rectangle2.Height) / ef1.Width), (float) (((float) rectangle2.Height) / ef1.Height));
															matrix1.Scale(single3, single3);
															matrix1.Translate(-single1, -single2);
															if (shape1 is Symbol)
															{
																(shape1 as Symbol).Draw(e.Graphics, matrix1);
															}
															else if (shape1 is ConnectShape)
															{
																using (GraphicsPath path3 = (path1.Clone() as GraphicsPath))
																{
																	path3.Transform(matrix1);
																	e.Graphics.DrawPath(pen2, path3);
																}
															}
														}
														float single4 = Math.Max(1, (int) (((rectangle1.Width - rectangle1.Height) - 10) - 10));
														RectangleF ef2 = new RectangleF((float) (((rectangle1.X + rectangle1.Height) + 5) + 10), (float) (rectangle1.Bottom - 40), single4, 20f);
														e.Graphics.FillRectangle(brush1, ef2);
														if (flag1)
														{
															pen1.Color = color2;
															pen1.DashStyle = DashStyle.Dash;
															e.Graphics.DrawRectangle(pen1, ef2.X, ef2.Y, ef2.Width, ef2.Height);
														}
														if (shape1 is Symbol)
														{
															e.Graphics.DrawString((shape1 as Symbol).Label, SystemInformation.MenuFont, brush2, ef2, format1);
														}
														else if (shape1 is ConnectShape)
														{
															e.Graphics.DrawString((shape1 as ConnectShape).Label, SystemInformation.MenuFont, brush2, ef2, format1);
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}

