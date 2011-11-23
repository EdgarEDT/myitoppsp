using System;
using System.Collections;
using ItopVector.Core.Func;
using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using CADImport;

namespace Itop.CADConvert
{	
	public class CADConvertHelper:IDisposable
	{
		SvgDocument svg;
		CADImage img=new CADImage();
		GraphicsPath grPath=new GraphicsPath();
		private string color;
		private ArrayList intPoints=new ArrayList();
		double pageHeight;
		double pageWidth;
		double offsetX=0;
		double offsetY=0;
		string layerID;
		bool multiView;//是否多视图(布局Layout)
		Hashtable layerlist =new Hashtable();

		CADLayoutCollection layouts = null;
		StreamWriter stw;
		int paperspace;
		private FlashWindow flashwindow;
		string fileName =string.Empty;

		static bool useCache =false;
		static CADImage cacheCAD =null;
		static string cacheFile=string.Empty;
		public static bool UseCache
		{
			get{return useCache;}
			set
			{
				useCache=value;
				if (!useCache)
				{
					cacheCAD=null;
					cacheFile =string.Empty;
				}
			}

		}
		public CADConvertHelper()
		{
		}
		private void execut(FlashWindow flash)
		{
			Application.DoEvents();
			svg = getSvg(fileName);
		}
		public ItopVector.Core.Document.SvgDocument ConvertToSvg(string filename)
		{
			flashwindow =new FlashWindow();
			flashwindow.OnRefleshStatus = new RefleshStatusEventHandler(execut);
			fileName = filename;
			flashwindow.SplashData();	
		
			return svg;
		}
		private ItopVector.Core.Document.SvgDocument getSvg(string filename)
		{
			flashwindow.RefleshStatus("正在分析CAD文件...");
			string exten=Path.GetExtension(filename);
			string svgFile = filename;
			double pWidth =800; 
			double pHeight =600; 
			ArrayList selectLayout = new ArrayList();
			CADImage cadimage=null;
			if (exten.ToLower()==".dwg")
			{
				if(useCache && filename==cacheFile)
				{
					cadimage = cacheCAD;
				}
				else
				{
					DWGImage dwg =new DWGImage();
					dwg.LoadFromFile(filename);
					cadimage = dwg;
					
				}
				svgFile = filename +".svg";
			}
			else if(exten.ToLower()==".dxf")
			{
				if(useCache && filename==cacheFile)
				{
					cadimage = cacheCAD;
				}
				else
				{
					cadimage =new CADImage();
					cadimage.LoadFromFile(filename);
				}
				svgFile = filename +".svg";
			}
			else if(exten.ToLower()==".svg")
			{
				flashwindow.RefleshStatus("正在分析SVG文件...");

				goto label_01;
			}
			else 
			{
				return null;
			}
			if(useCache)
			{
				cacheFile = filename;
				cacheCAD = cadimage;
			}
			checkLayout(cadimage);
			if (multiView)
			{
				paperspace = cadimage.converter.Entities[0].PaperSpace;
				cadimage.CurrentLayout = cadimage.Layouts[paperspace];
				using(frmLayout dlg =new frmLayout(cadimage.Layouts))
					  {
					dlg.Layouts = selectLayout;
					dlg.ShowDialog();
				}
			}
			pWidth = cadimage.AbsWidth;
			pHeight = cadimage.AbsHeight;
			//			cadimage.Layouts[0].PaperSpaceBlock!=null;

			flashwindow.RefleshStatus(string.Format("正在重新生成模型{0}...",""));
			//			cadimage.converter.Layers
			stw =File.CreateText(svgFile);
			stw.AutoFlush=true;

			string hd = "<?xml version=\"1.0\" encoding=\"utf-8\"?><!----><!DOCTYPE svg PUBLIC \"-/W3C/DTD SVG 1.1/EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">" +
				"\r\n<svg id=\"svg\" width=\"" +pWidth + "\" height=\"" + pHeight + "\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:tonli=\"http://www.tonli.com/tonli\">";

			stw.Write(hd);
			
			string lar = "\r\n<defs id=\"defs00001\">"+createLayer(cadimage.converter.Layers) +
				"\r\n</defs>";
			stw.Write(lar);

			if(!multiView || selectLayout.Contains(paperspace))
			{

				Parse(cadimage.converter.Entities,cadimage.AbsHeight,cadimage.AbsWidth,cadimage.xMin,cadimage.yMin,"");
			}

			if (multiView)
			{
				paperspace=-1;
				foreach(CADLayout layout in cadimage.Layouts)
				{
					paperspace++;				
					if(!selectLayout.Contains(paperspace))continue;
					if(layout.PaperSpaceBlock !=null)
					{
						cadimage.CurrentLayout = cadimage.Layouts[paperspace];

						DRect rect = layout.PaperSpaceBlock.Box;
						pWidth = Math.Max(pWidth,cadimage.AbsWidth);
						pHeight = Math.Max(pHeight,cadimage.AbsHeight);
						Parse(layout.PaperSpaceBlock.Entities,cadimage.AbsHeight,cadimage.AbsWidth,cadimage.xMin,cadimage.yMin,"");
					}
				}
				
			}
			
			stw.Write("\r\n</svg>");

			stw.Flush();
			stw.Close();

			label_01:
			ItopVector.Core.Document.SvgDocument doc= ItopVector.Core.Document.SvgDocumentFactory.CreateDocumentFromFile(svgFile);
//			doc.PreserveWhitespace = true;
//			DateTime dt1 =DateTime.Now;
//			doc.Load(svgFile);

			if (doc==null)return null;

			bool flag = doc.AcceptChanges;
			doc.AcceptChanges=false;

			if(multiView)
			{
				doc.RootElement.SetAttribute("width",""+pWidth);
				doc.RootElement.SetAttribute("height",""+pHeight);
			}
			for(int i=doc.Layers.Count-1;i>=0;i--)
			{
				Layer layer = doc.Layers[i] as Layer;
				if(layer.GraphList.Count==0)layer.Remove();
			}
			if(multiView && doc.Layers.Count>0 )
			{
				(doc.Layers[0] as Layer).Visible=true;
			}

#if !DEBUG
			File.Delete(filename+".svg");
#endif
			doc.FilePath="";
			doc.FileName=Path.GetFileNameWithoutExtension(filename);		
			doc.AcceptChanges=flag;

			layouts =null;
			cadimage =null;
			return doc;
		}
		void checkLayout(CADImage cadimage)
		{
			multiView =false;
			foreach(CADLayout layout in cadimage.Layouts)
			{	
				
				if(layout.PaperSpaceBlock!=null)
				{
					multiView =true;
					break;
				}
			}
			if(multiView)layouts = cadimage.Layouts;
		}
		string createLayer(CADEntityCollection layers)
		{
			layerlist =new Hashtable();
			StringBuilder outxml=new StringBuilder();
			if(multiView)
			{
				int i=0;
				string strVisible= "visibility=\"visible\"";
				string strHidden= "visibility=\"hidden\"";
				foreach(CADLayout layout in layouts)
				{

					string layerid ="layer"+Guid.NewGuid().ToString().Substring(24);
					layerlist.Add(i,layerid);
					
					outxml.Append(string .Format("\r\n<layer id=\"{0}\" label=\"{1}\" "+strHidden+" />",layerid,layout.Name));	
					//MessageBox.Show(i.ToString());
					i++;
				}
			}
			else
			{
				foreach(CADLayer layer in layers)
				{
					string layerid ="layer"+Guid.NewGuid().ToString().Substring(24);

					if(layerlist.ContainsKey(layer.Name))
						layerid = layerlist[layer.Name].ToString();
					else
						layerlist.Add(layer.Name,layerid);
					outxml.Append(string .Format("\r\n<layer id=\"{0}\" label=\"{1}\" />",layerid,layer.Name));				
				}
			}
			return outxml.ToString();
		}
		string getLayer(object layername)
		{
			string layerid =string.Empty;
			try
			{
				layerid =layerlist[layername].ToString();
			}
			catch{}

			return  layerid;
		}
		string getLayer(CADEntity cadObj)
		{
			string layerid =string.Empty;
			try
			{
				if(multiView)
				{
					layerid = getLayer(paperspace);
				}
				else
				{
					layerid = getLayer(cadObj.Layer.Name);
				}
			}
			catch{}

			return  layerid;
		}
		internal  float Conversion_Angle(float Val)
		{
			
			for (; Val < 0.0F; Val += 360.0F)
			{
			}
			return Val;
		}
		internal  void CreateIntList(CADDPointCollection DottedSingPts, bool Closed)
		{
			DPoint dPoint1 = new DPoint();
			intPoints.Clear();
			for (int i = 0; i < DottedSingPts.Count; i++)
			{
				DPoint dPoint2 = dPoint1;
				DPoint dPoint3 = DottedSingPts[i];
				intPoints.Add(dPoint3.X);
				intPoints.Add(dPoint3.Y);
			}
			if (Closed)
			{		
			}
		}
		private void DrawPolyPolyLineInsert(ArrayList IntPoints)
		{
			ArrayList list1 = new ArrayList();
			int num2 = IntPoints.Count >> 2;
			if (num2 != 0)
			{
				int num3 = IntPoints.Count;
				for (int num1 = 0; num1 < num2; num1++)
				{
					list1.Add(2);
				}
				PolyPolylineInsert(IntPoints, list1, num2);
			}
		}
		private  void DrawPolyPolyLine(ArrayList IntPoints)
		{
			ArrayList list1 = new ArrayList();
			int num2 = IntPoints.Count >> 2;
			if (num2 != 0)
			{
				int num3 = IntPoints.Count;
				for (int num1 = 0; num1 < num2; num1++)
				{
					list1.Add(2);
				}
				PolyPolyline(IntPoints, list1, num2);
			}
		}
		private  void PolyPolyline(ArrayList a1, ArrayList a2, int cn)
		{
			int j = 0;
			for (int k = 0; k < cn; k++)
			{
				if (j >= a1.Count)
				{
					return;
				}
				PointF[] pointFs = new PointF[(int)a2[k]];
				int i1 = 0;
				for (int i2 = 0; i2 < (int)a2[k] * 2; i2 += 2)
				{
					pointFs[i1].X =OffSetX(Convert.ToDouble( a1[j]));
					pointFs[i1].Y = OffSetY(Convert.ToDouble(a1[j + 1]));
					i1++;
					j += 2;
				}
				grPath.AddLines(pointFs);
			}
		}
		private void PolyPolylineInsert(ArrayList a1, ArrayList a2, int cn)
		{
			int j = 0;
			for (int k = 0; k < cn; k++)
			{
				if (j >= a1.Count)
				{
					return;
				}
				PointF[] pointFs = new PointF[(int)a2[k]];
				int i1 = 0;
				for (int i2 = 0; i2 < (int)a2[k] * 2; i2 += 2)
				{
					pointFs[i1].X = OffSetX(Convert.ToDouble(a1[j]));
					pointFs[i1].Y = Convert.ToSingle(a1[j + 1]);
					i1++;
					j += 2;
				}
				grPath.AddLines(pointFs);
			}
		}
	    
		internal float OffSetX(double x)
		{
			return Convert.ToSingle( x - offsetX);
		}
		internal float OffSetY(double y)
		{
			return Convert.ToSingle( pageHeight - y + offsetY);
		}
		private DPoint GetPoint(DPoint point)
		{
			return new DPoint(point.X - offsetX,pageHeight - point.Y + offsetY,0);
		}
		internal  void ParseInsert(CADEntityCollection cad,double pHeight,double pWidth)
		{
			pageHeight=pHeight;
			pageWidth=pWidth;
			//svg.AcceptChanges=true;		    
			for(int i=0;i<cad.Count;i++)
			{				
				string layerid = getLayer(cad[i]);
				if(layerid =="")continue;
				switch(cad[i].GetType().Name)
				{
					case "CADArc":
					{
						double d1 = ((CADArc)(cad[i])).Radius;
						DPoint dPoint = ((CADArc)(cad)[i]).Point;
						dPoint.X -= d1;
						dPoint.Y += d1;
						double d3 = ((CADArc)(cad[i])).StartAngle;
						double d4 = ((CADArc)(cad[i])).EndAngle;
						if (((CADArc)(cad[i])).EndAngle < ((CADArc)(cad[i])).StartAngle)
						{
							d4 = Conversion_Angle((float)d4);
						}

						if (d4 > d3)
						{
							double ag = d4 - d3;
							if (d3 > 180 || d4 > 180)
							{
								d4 = 360 - d4;
							}
							grPath.AddArc(Convert.ToSingle(dPoint.X), Convert.ToSingle( dPoint.Y), (float)d1 * 2.0F, (float)d1 * 2.0F, (float)d4, (float)ag);
						}
						else
						{
							double eg = 360 - d4;
							d4 += 360;
							double ag = d4 - d3;
							grPath.AddArc(Convert.ToSingle(dPoint.X),Convert.ToSingle( dPoint.Y), (float)d1 * 2.0F, (float)d1 * 2.0F, (float)eg, (float)ag);
						}
						string c_path = PathFunc.GetPathString(grPath);
						color = ColorFunc.GetColorString(getColor(cad[i]));
						GraphPath p1 = (GraphPath)svg.CreateElement("", "path", "");
						p1.SetAttribute("d", c_path);
						p1.SetAttribute("fill", "none");
						p1.SetAttribute("stroke", color);

						p1.SetAttribute("layer", layerid);
						stw.Write("\r\n");
						stw.Write(p1.OuterXml);
						grPath.Reset();
						break;
					}
					case "CADCircle":
					{
						double y = ((CADCircle)(cad[i])).Point.Y;
						color = ColorFunc.GetColorString(getColor(cad[i]));
						Circle c1 = (Circle)svg.CreateElement("", "circle", "");
						c1.SetAttribute("cx", (((CADCircle)(cad[i])).Point.X).ToString("00.00"));
						c1.SetAttribute("cy", y.ToString("00.00"));
						c1.SetAttribute("r", ((CADCircle)(cad[i])).Radius.ToString("00.00"));
						c1.SetAttribute("fill", "none");
						//c1.SetAttribute("fill-opacity", "1");
						c1.SetAttribute("stroke", color);
						//c1.SetAttribute("stroke-opacity", "1");
						c1.SetAttribute("layer", layerid);
						stw.Write("\r\n");
						stw.Write(c1.OuterXml);
						//svg.RootElement.AppendChild(c1);
						break;
					}
					case "CADEllipse":
					{
						double y = ((CADEllipse)(cad[i])).Point.Y;
						color = ColorFunc.GetColorString(getColor(cad[i]));
						Ellips e1 = (Ellips)svg.CreateElement("", "ellipse", "");
						e1.SetAttribute("cx", (((CADEllipse)(cad[i])).Point.X).ToString("00.00"));
						e1.SetAttribute("cy", y.ToString("00.00"));
						e1.SetAttribute("rx", ((CADEllipse)(cad[i])).A.ToString("00.00"));
						e1.SetAttribute("ry", ((CADEllipse)(cad[i])).B.ToString("00.00"));
						e1.SetAttribute("fill", "none");
						//e1.SetAttribute("fill-opacity", "1");
						e1.SetAttribute("stroke", color);
						//e1.SetAttribute("stroke-opacity", "1");
						e1.SetAttribute("layer", layerid);
						stw.Write("\r\n");
						stw.Write(e1.OuterXml);
						break;
					}
					case "CADLine":
					{
						double y1 = ((CADLine)(cad[i])).Point.Y;
						double y2 = ((CADLine)(cad[i])).Point1.Y;
						color = ColorFunc.GetColorString(getColor(cad[i]));
						Line l1 = (Line)svg.CreateElement("", "line", "");
						l1.SetAttribute("x1", (((CADLine)(cad[i])).Point.X).ToString("00.00"));
						l1.SetAttribute("y1", y1.ToString("00.00"));
						l1.SetAttribute("x2", (((CADLine)(cad[i])).Point1.X).ToString("00.00"));
						l1.SetAttribute("y2", y2.ToString("00.00"));
						l1.SetAttribute("fill", "none");
						//l1.SetAttribute("fill-opacity", "1");
						l1.SetAttribute("stroke", color);
						//l1.SetAttribute("stroke-opacity", "1");
						l1.SetAttribute("layer", layerid);
						stw.Write("\r\n");
						stw.Write(l1.OuterXml);
						break;
					}
					case "CADLWPolyLine":
					{
						string pointsXY = "";
						color = ColorFunc.GetColorString(getColor(cad[i]));
						int k = ((CADPolyLine)(cad[i])).PolyPoints.Count;
						if (((CADPolyLine)(cad[i])).IsMeshMClosed)
						{
							pointsXY = pointsXY + " " + (((CADPolyLine)(cad[i])).PolyPoints[k - 1].X).ToString("00.00") + " " + ((CADPolyLine)(cad[i])).PolyPoints[k - 1].Y.ToString("00.00");
						}
						for (int m = 0; m < ((CADPolyLine)(cad[i])).PolyPoints.Count; m++)
						{
							pointsXY = pointsXY + " " + (((CADPolyLine)(cad[i])).PolyPoints[m].X).ToString("00.00") + " " + ((CADPolyLine)(cad[i])).PolyPoints[m].Y.ToString("00.00");
						}

						Polyline p1 = (Polyline)svg.CreateElement("", "polyline", "");
						p1.SetAttribute("points", pointsXY);
						p1.SetAttribute("fill", "none");
						//p1.SetAttribute("fill-opacity", "1");
						p1.SetAttribute("stroke", color);
						//p1.SetAttribute("stroke-opacity", "1");

						p1.SetAttribute("layer", layerid);
						stw.Write("\r\n");
						stw.Write(p1.OuterXml);
						break;
					}
					case "CADPatternPolygon":
					{

						CADPolyPolygon graph = cad[i] as CADPolyPolygon;

						color=ColorFunc.GetColorString(getColor(cad[i]));
						
						int k = 0;
						ArrayList list = ((CADPolyPolygon)(cad[i])).Polylines;
						for (int n = 0; n < list.Count; n++)
						{
							for (int m = 0; m < ((ArrayList)list[n]).Count; m++)
							{
								k = k + 1;
							}
						}
						PointF[] pt = new PointF[k];
						int count = 0;
						for (int n = 0; n < list.Count; n++)
						{
							for (int m = 0; m < ((ArrayList)list[n]).Count; m++)
							{
								pt[count].X = (((sgHElem)((ArrayList)list[n])[m]).X);
								pt[count].Y = ((sgHElem)((ArrayList)list[n])[m]).Y;
								count += 1;
							}
						}
						grPath.AddPolygon(pt);
						string c_path = PathFunc.GetPathString(grPath);
						GraphPath p1 = (GraphPath)svg.CreateElement("", "path", "");
						p1.SetAttribute("d", c_path);
						p1.SetAttribute("fill", color);
						p1.SetAttribute("stroke", color);
						p1.SetAttribute("layer", layerid);
						stw.Write("\r\n");
						stw.Write(p1.OuterXml);
						grPath.Reset();
						break;
					}
					case "CADPoint":
					{
						double y = ((CADPoint)(cad[i])).Point.Y;
						color = ColorFunc.GetColorString(getColor(cad[i]));
						Circle c1 = (Circle)svg.CreateElement("", "circle", "");
						c1.SetAttribute("cx", (((CADPoint)(cad[i])).Point.X).ToString("00.00"));
						c1.SetAttribute("cy", y.ToString("00.00"));
						c1.SetAttribute("r", "1");
						c1.SetAttribute("fill", "#FFFFFF");
						//c1.SetAttribute("fill-opacity", "1");
						c1.SetAttribute("stroke", color);
						//c1.SetAttribute("stroke-opacity", "1");
						c1.SetAttribute("layer", layerid);
						stw.Write("\r\n");
						stw.Write(c1.OuterXml);
						break;
					}
					case "CADSpline":
					{
						CADSpline cADSpline = (CADSpline)(cad[i]);
						CADDPointCollection cADDPointCollection = cADSpline.DottedSingPts;
						CreateIntList(cADDPointCollection, false);
						color = ColorFunc.GetColorString(getColor(cad[i]));
						DrawPolyPolyLineInsert(intPoints);
						string c_path = PathFunc.GetPathString(grPath);
						GraphPath p1 = (GraphPath)svg.CreateElement("", "path", "");
						p1.SetAttribute("d", c_path);
						p1.SetAttribute("fill", "none");
						p1.SetAttribute("stroke", color);
						p1.SetAttribute("layer", layerid);
						stw.Write("\r\n");
						stw.Write(p1.OuterXml);
						grPath.Reset();
						break;
					}

					case "CADText":
					{
						CADText cadObj=(CADText)(cad[i]);
						if (cadObj.Text.Trim()==string.Empty)continue;
						double y = cadObj.Point.Y;
						color = ColorFunc.GetColorString(getColor(cad[i]));
						Text t1 = (Text)svg.CreateElement("text");
						t1.SetAttribute("x", cadObj.Point.X.ToString("00.00"));
						t1.SetAttribute("y", y.ToString("00.00"));
						t1.SetAttribute("rotation", cadObj.rotation.ToString());

						t1.SetAttribute("font-family",cadObj.winFont?cadObj.fontName:"Arial");//((CADText)(cad[i])).fontName);
						t1.SetAttribute("font-size",""+(cadObj.winFont?cadObj.height:cadObj.height -1));
						t1.TextString = cadObj.Text;
						t1.SetAttribute("transform","matrix(1,0,0,-1,0,"+ (0) +")");
//						t1.SetAttribute("fill", "none");
						//t1.SetAttribute("fill-opacity", "1");
						t1.SetAttribute("stroke", color);
						//t1.SetAttribute("stroke-opacity", "1");
						t1.SetAttribute("layer", layerid);
						stw.Write("\r\n");
						stw.Write(t1.OuterXml);
						break;
					}
					case "CADMText":
					{
						try
						{
							if (((CADText)(cad[i])).Text.Trim()==string.Empty)continue;

							double y = ((CADMText)(cad[i])).Point.Y;
							color = ColorFunc.GetColorString(getColor(cad[i]));
							Text t1 = (Text)svg.CreateElement("", "text", "");
							t1.SetAttribute("x", (((CADMText)(cad[i])).Point.X).ToString("00.00"));
							t1.SetAttribute("y", y.ToString("00.00"));
							t1.SetAttribute("rotation", ((CADMText)(cad[i])).Angle.ToString("00.00"));
							t1.SetAttribute("font-family", "Arial");
							t1.SetAttribute("font-size", ((CADMText)(cad[i])).Height.ToString());
							t1.TextString = ((CADMText)(cad[i])).Text;
							t1.SetAttribute("fill", "none");
							//t1.SetAttribute("fill-opacity", "1");
							t1.SetAttribute("stroke", color);
							//t1.SetAttribute("stroke-opacity", "1");
							t1.SetAttribute("layer", layerid);
							stw.Write("\r\n");
							stw.Write(t1.OuterXml);

						}
						catch { }
						break;
                          
					}
					case "CADSolid":
					{

						CADSolid so = ((CADSolid)cad[i]);
						color=ColorFunc.GetColorString(getColor(cad[i]));
						string points = (so.Point.X).ToString() + " " + so.Point.Y.ToString() + ",";
						points = points + (so.Point1.X).ToString() + " " + so.Point1.Y.ToString() + ",";
						points = points + (so.Point3.X).ToString() + " " + so.Point3.Y.ToString() + ",";
						points = points + (so.Point2.X).ToString() + " " + so.Point2.Y.ToString();

						Polygon p1 = (Polygon)svg.CreateElement("", "polygon", "");
						p1.SetAttribute("points", points);
						p1.SetAttribute("fill", color);
						p1.SetAttribute("stroke", color);
						p1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(p1.OuterXml);
						break;
					}
					case "CADInsert":
					{
						CADMatrix matr=((CADInsert)cad[i]).Matrix;				
						string strMatr;
						if(matr.data[0,0]!=1)
						{
							strMatr="<g transform=\"matrix("+matr.data[0,0]+","+(matr.data[0,1])+","+matr.data[1,0]+","+(matr.data[1,1])+","+matr.data[3,0]+","+(matr.data[3,1])+")\" id=\""+((CADInsert)cad[i]).EntName+"\">";
						}
						else
						{
							strMatr="<g transform=\"matrix("+matr.data[0,0]+","+0+","+0+","+(matr.data[1,1])+","+matr.data[3,0]+","+(matr.data[3,1])+")\" id=\""+((CADInsert)cad[i]).EntName+"\">";
						}
						stw.Write("\r\n"+strMatr);
						ParseInsert(((CADInsert)cad[i]).Block.Entities,pHeight,pWidth);
						stw.Write("\r\n</g>");
						break;
					}
                   
				}
			}
		}

		Color byLayer = Color.FromName(""+0x2fffffff);
		private Color getColor(CADEntity cadEntity)
		{
			Color color1 = cadEntity.Color;			
			if(color1 ==byLayer && (cadEntity.Layer.Name != "0"))
			{
				color1 = cadEntity.Layer.Pen.Color;
			}
			return color1;
		}
		string CreateId(string key)
		{
			return CodeFunc.CreateString(svg,key);
		}
		internal  void Parse(CADEntityCollection cad,double pHeight,double pWidth,double setX,double setY,string layer)
		{
			layerID = layer;
			pageHeight=pHeight;
			pageWidth=pWidth;

			offsetX = setX;
			offsetY = setY;

			if(svg==null)
			{
				svg=SvgDocumentFactory.CreateDocument(new SizeF(800,600));
			}
			//svg.AcceptChanges=true;
			int totalNum;
			totalNum = cad.Count;
			svg.AcceptChanges=false;
			for(int i=0;i<cad.Count;i++)
			{
				flashwindow.RefleshStatus(string.Format("正在重新生成模型{0}%...{1}",(int)(i*100/totalNum),paperspace));

				if(cad[i].Layer==null)continue;
				string layerid = getLayer(cad[i]);
				if (layerid=="")continue;

				string svgType=string.Empty;
				switch(cad[i].GetType().Name)
				{
					case "CADArc":
					{
						CADArc cadEntity = cad[i] as CADArc;
						double d1 = cadEntity.Radius;
						DPoint dPoint = cadEntity.Point;
						dPoint.X -= d1;
						dPoint.Y += d1;
						double d3 = cadEntity.StartAngle;
						double d4 = cadEntity.EndAngle;
						if (cadEntity.EndAngle < cadEntity.StartAngle)
						{
							d4 = Conversion_Angle((float)d4);
						}

						if(d4>d3)
						{
							double ag=d4-d3;
							if(d3>180 || d4>180)
							{
								d4=360-d4;
							}
							grPath.AddArc(OffSetX(dPoint.X), OffSetY(dPoint.Y), (float)d1 * 2.0F, (float)d1 * 2.0F, (float)d4, (float)ag);
							
						}
						else
						{
							double eg=360-d4;
							d4+=360;
							double ag=d4-d3;
							grPath.AddArc(OffSetX(dPoint.X), OffSetY(dPoint.Y), (float)d1 * 2.0F, (float)d1 * 2.0F, (float)eg, (float)ag);
						}
						string c_path=PathFunc.GetPathString(grPath);
						color=ColorFunc.GetColorString(getColor(cadEntity));
						GraphPath p1=(GraphPath)svg.CreateElement("","path","");
						p1.SetAttribute("id",CreateId("path"));
						p1.SetAttribute("d",c_path);
//						p1.SetAttribute("fill","none");
						p1.SetAttribute("stroke",color);
						p1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(p1.OuterXml);
						grPath.Reset();
						break;
					}
					case "CADCircle":
					{
						double y=((CADCircle)(cad[i])).Point.Y;
						svgType="circle";
						color=ColorFunc.GetColorString(getColor(cad[i]));
						Circle c1=(Circle)svg.CreateElement("",svgType,"");
						c1.SetAttribute("id",CreateId(svgType));
						c1.SetAttribute("cx", OffSetX(((CADCircle)(cad[i])).Point.X).ToString("00.00"));
						c1.SetAttribute("cy", OffSetY(y).ToString("00.00"));
						c1.SetAttribute("r",((CADCircle)(cad[i])).Radius.ToString("00.00"));
//						c1.SetAttribute("fill","none");
//						c1.SetAttribute("fill-opacity","1");
						c1.SetAttribute("stroke",color);
//						c1.SetAttribute("stroke-opacity","1");
						c1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(c1.OuterXml);
						//svg.RootElement.AppendChild(c1);
						break;
					}
					case "CADEllipse":
					{
						double y=((CADEllipse)(cad[i])).Point.Y;
						color=ColorFunc.GetColorString(getColor(cad[i]));
						svgType="ellipse";
						Ellips e1=(Ellips)svg.CreateElement("",svgType,"");
						e1.SetAttribute("id",CreateId(svgType));
						e1.SetAttribute("cx",OffSetX(((CADEllipse)(cad[i])).Point.X).ToString("00.00"));
						e1.SetAttribute("cy",OffSetY(y).ToString("00.00"));
						e1.SetAttribute("rx",((CADEllipse)(cad[i])).A.ToString("00.00"));
						e1.SetAttribute("ry",((CADEllipse)(cad[i])).B.ToString("00.00"));
//						e1.SetAttribute("fill","none");
						//e1.SetAttribute("fill-opacity","1");
						e1.SetAttribute("stroke",color);
//						e1.SetAttribute("stroke-opacity","1");
						e1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(e1.OuterXml);
						break;
					}
					case "CADLine":
					{
						double y1=((CADLine)(cad[i])).Point.Y;
						double y2=((CADLine)(cad[i])).Point1.Y;
						color=ColorFunc.GetColorString(getColor(cad[i]));
						svgType ="line";
						Line l1=(Line)svg.CreateElement("","line","");
						l1.SetAttribute("id",CreateId(svgType));
						l1.SetAttribute("x1", OffSetX(((CADLine)(cad[i])).Point.X).ToString("00.00"));
						l1.SetAttribute("y1", OffSetY(y1).ToString("00.00"));
						l1.SetAttribute("x2", OffSetX(((CADLine)(cad[i])).Point1.X).ToString("00.00"));
						l1.SetAttribute("y2", OffSetY(y2).ToString("00.00"));
//						l1.SetAttribute("fill","none");
						//l1.SetAttribute("fill-opacity","1");
						l1.SetAttribute("stroke",color);
						//l1.SetAttribute("stroke-opacity","1");
						l1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(l1.OuterXml);
						break;
					}
					case "CADLWPolyLine":
					{
						string pointsXY="";
						color=ColorFunc.GetColorString(getColor(cad[i]));
						int k=((CADPolyLine)(cad[i])).PolyPoints.Count;
						if(((CADPolyLine)(cad[i])).IsMeshMClosed)
						{
							pointsXY = pointsXY + " " + OffSetX(((CADPolyLine)(cad[i])).PolyPoints[k - 1].X).ToString("00.00") + " " + OffSetY(((CADPolyLine)(cad[i])).PolyPoints[k - 1].Y ).ToString("00.00");
						}
						for(int m=0;m<((CADPolyLine)(cad[i])).PolyPoints.Count;m++)
						{
							pointsXY = pointsXY + " " + OffSetX(((CADPolyLine)(cad[i])).PolyPoints[m].X).ToString("00.00") + " " + OffSetY(((CADPolyLine)(cad[i])).PolyPoints[m].Y).ToString("00.00");
						}
						svgType = "polyline";
						Polyline p1=(Polyline)svg.CreateElement("","polyline","");
						p1.SetAttribute("id",CreateId(svgType));
						p1.SetAttribute("points",pointsXY);
//						p1.SetAttribute("fill","none");
						//p1.SetAttribute("fill-opacity","1");
						p1.SetAttribute("stroke",color);
						//p1.SetAttribute("stroke-opacity","1");
						p1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(p1.OuterXml);
						break;
					}
					case "CADPatternPolygon":
					{					
						CADPolyPolygon graph = cad[i] as CADPolyPolygon;

						color=ColorFunc.GetColorString(getColor(cad[i]));
						if(graph.Color==Color.Black)
						{
							int nn=0;
							nn++;

						}
						int k=0;			
						ArrayList list=((CADPolyPolygon)(cad[i])).Polylines;
						for(int n=0;n<list.Count;n++)
						{
							for(int m=0;m<((ArrayList)list[n]).Count;m++)
							{
								k=k+1;
							}
						}
						PointF [] pt=new PointF[k];
						int count=0;
						for(int n=0;n<list.Count;n++)
						{
							for(int m=0;m<((ArrayList)list[n]).Count;m++)
							{
								pt[count].X = OffSetX(((sgHElem)((ArrayList)list[n])[m]).X);
								pt[count].Y =OffSetY(((sgHElem)((ArrayList)list[n])[m]).Y);
								count+=1;
							}
						}
						grPath.AddPolygon(pt);
						string c_path=PathFunc.GetPathString(grPath);
						svgType = "path";
						GraphPath p1=(GraphPath)svg.CreateElement("","path","");
						p1.SetAttribute("id",CreateId(svgType));
						p1.SetAttribute("d",c_path);
//						p1.SetAttribute("fill",color);
						p1.SetAttribute("stroke","none");
						p1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(p1.OuterXml);
						grPath.Reset();
						break;
					}
					case "CADPoint":
					{
						double y=((CADPoint)(cad[i])).Point.Y;
						color=ColorFunc.GetColorString(getColor(cad[i]));
						svgType = "circle";
						Circle c1=(Circle)svg.CreateElement("","circle","");
						c1.SetAttribute("id",CreateId(svgType));
						c1.SetAttribute("cx", OffSetX(((CADPoint)(cad[i])).Point.X ).ToString("00.00"));
						c1.SetAttribute("cy", OffSetY( y).ToString("00.00"));
						c1.SetAttribute("r","1");
						c1.SetAttribute("fill","#FFFFFF");
						//c1.SetAttribute("fill-opacity","1");
						c1.SetAttribute("stroke",color);
						//c1.SetAttribute("stroke-opacity","1");
						c1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(c1.OuterXml);
						break;
					}
					case "CADSpline":
					{
						CADSpline cADSpline=(CADSpline)(cad[i]);
						CADDPointCollection cADDPointCollection = cADSpline.DottedSingPts;
						CreateIntList(cADDPointCollection, false);
						color=ColorFunc.GetColorString(getColor(cad[i]));
						DrawPolyPolyLine(intPoints);
						string c_path=PathFunc.GetPathString(grPath);
						svgType = "path";
						GraphPath p1=(GraphPath)svg.CreateElement("","path","");
						p1.SetAttribute("id",CreateId(svgType));
						p1.SetAttribute("d",c_path);
//						p1.SetAttribute("fill","none");
						p1.SetAttribute("stroke",color);
						p1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(p1.OuterXml);
						grPath.Reset();
						break;
					}
					
					case "CADText":
					{
						CADText cadObj=(CADText)(cad[i]);
						if (cadObj.Text.Trim()==string.Empty)continue;
						float x = OffSetX(cadObj.Point.X);
						float y= OffSetY( cadObj.Point.Y);
						color=ColorFunc.GetColorString(getColor(cad[i]));
						svgType ="text";

						string transform =string.Empty;
						if(Math.Abs( cadObj.rotation)>0)
						{
							float angle =-cadObj.rotation;
							for(;angle<0;angle+=360){}

							double height = cadObj.height;
							Matrix matrix1 =new Matrix();
							matrix1.RotateAt(angle,new PointF(x,y));
							ItopVector.Core.Types.Transf transf = new ItopVector.Core.Types.Transf(matrix1);
							transform = transf.ToString();
						}

						Text t1=(Text)svg.CreateElement("","text","");
						t1.SetAttribute("id",CreateId(svgType));
						t1.SetAttribute("x", x.ToString("00.00"));
						t1.SetAttribute("y", y.ToString("00.00"));
						if(transform!=string.Empty)
							t1.SetAttribute("transform",transform);
						t1.SetAttribute("font-family",cadObj.winFont?cadObj.fontName:"Arial");//((CADText)(cad[i])).fontName);
						t1.SetAttribute("font-size",""+((cadObj.winFont || cadObj.height>5)?cadObj.height:cadObj.height -1));
						t1.TextString=((CADText)(cad[i])).Text;
						
						//t1.SetAttribute("fill","none");
						//t1.SetAttribute("fill-opacity","1");
						t1.SetAttribute("stroke",color);
						//t1.SetAttribute("stroke-opacity","1");
						t1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(t1.OuterXml);
						break;
					}
					case "CADMText":
					{
						try
						{
							//if (((CADText)(cad[i])).Text.Trim()==string.Empty)continue;
							CADMText mText = (CADMText)(cad[i]);
							if(mText.Block!=null)
							{
								DPoint begin = GetPoint(mText.Point);
#if DEBUG
								if(mText.Block.Entities.Count>1)
								{
                                    int nn=0;
									nn++;
								}
#endif
								string transform =string.Empty;
								if(Math.Abs( mText.Angle)>0)
								{
									float angle =-mText.Angle;
									for(;angle<0;angle+=360){}

									double height = (mText.Block.Entities[0] as CADText).height;
									Matrix matrix1 =new Matrix();
									matrix1.RotateAt(angle,new PointF((float)begin.X,(float)(begin.Y -height)));
									ItopVector.Core.Types.Transf transf = new ItopVector.Core.Types.Transf(matrix1);
									transform = transf.ToString();
								}


								svgType ="text";
								foreach(CADText obj in mText.Block.Entities)
								{
									if (obj.Text.Trim()==string.Empty)continue;
									double y=obj.point1.Y -obj.height;
									color=ColorFunc.GetColorString(getColor(obj));
									Text t1=(Text)svg.CreateElement("","text","");
									t1.SetAttribute("id",CreateId(svgType));
									t1.SetAttribute("x", (begin.X +obj.point1.X).ToString("00.00"));
									t1.SetAttribute("y", (begin.Y -y).ToString("00.00"));
//									t1.SetAttribute("rotation",mText.Angle.ToString());
									t1.SetAttribute("font-family",obj.winFont?obj.fontName:"Arial");//((CADText)(cad[i])).fontName);
									t1.SetAttribute("font-size",obj.height.ToString());
									t1.TextString=obj.Text;						
									//t1.SetAttribute("fill",color);
									//t1.SetAttribute("fill-opacity","1");
									t1.SetAttribute("stroke",color);
									//t1.SetAttribute("stroke-opacity","1");
									t1.SetAttribute("layer",layerid);
									if(transform!=string.Empty)
										t1.SetAttribute("transform",transform);
									stw.Write("\r\n");
									stw.Write(t1.OuterXml);

								}

								break;
							}
							else
							{
//								double y=((CADMText)(cad[i])).Point.Y;
//								color=ColorFunc.GetColorString(getColor(cad[i]));
//								Text t1=(Text)svg.CreateElement("","text","");
//								t1.SetAttribute("x", OffSetX(((CADMText)(cad[i])).Point.X).ToString("00.00"));
//								t1.SetAttribute("y", OffSetY( y ).ToString("00.00"));
//								t1.SetAttribute("rotation",((CADMText)(cad[i])).Angle.ToString("00.00"));
//								t1.SetAttribute("font-family","Arial");
//								t1.SetAttribute("font-size",((CADMText)(cad[i])).Height.ToString());
//								t1.TextString=((CADMText)(cad[i])).Text.Replace("\\P","\r");
//								t1.SetAttribute("fill",color);
//								//t1.SetAttribute("fill-opacity","1");
//								t1.SetAttribute("stroke","none");
//								//t1.SetAttribute("stroke-opacity","1");
//								t1.SetAttribute("layer",layerid);
//								stw.Write("\r\n");
//								stw.Write(t1.OuterXml);
							}
							
						}
						catch{}
						break;		
					}
					case "CADSolid":
					{
						CADSolid so=((CADSolid)cad[i]);
						color=ColorFunc.GetColorString(getColor(cad[i]));
						string points = OffSetX(so.Point.X).ToString() + " " + OffSetY(so.Point.Y ).ToString() + ",";
						points = points + OffSetX(so.Point1.X).ToString() + " " + OffSetY(so.Point1.Y).ToString() + ",";
						points = points + OffSetX(so.Point3.X).ToString() + " " + OffSetY(so.Point3.Y).ToString() + ",";
						points = points + OffSetX(so.Point2.X).ToString() + " " + OffSetY(so.Point2.Y).ToString();

						svgType = "polygon";
						Polygon p1 = (Polygon)svg.CreateElement("", "polygon", "");
						p1.SetAttribute("id",CreateId(svgType));
						p1.SetAttribute("points",points);
//						p1.SetAttribute("fill", color);
						p1.SetAttribute("stroke",color);
						p1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(p1.OuterXml);
						break;
					}
					case "CADInsert":
					{
						CADMatrix matr=((CADInsert)cad[i]).Matrix;				
						string strMatr;
						if(matr.data[0,0]!=1)
						{
							strMatr="<g transform=\"matrix("+matr.data[0,0]+","+(-matr.data[0,1])+","+matr.data[1,0]+","+(-matr.data[1,1])+","+OffSetX(matr.data[3,0])+","+OffSetY(matr.data[3,1])+")\" id=\""+CreateId("group")+"\""+" layer=\""+layerid+"\">";
						}
						else
						{
							strMatr="<g transform=\"matrix("+matr.data[0,0]+","+matr.data[0,1]+","+matr.data[1,0]+","+(-matr.data[1,1])+","+OffSetX(matr.data[3,0])+","+OffSetY(matr.data[3,1])+")\" id=\""+CreateId("group")+"\""+" layer=\""+layerid+"\">";
						}
						stw.Write("\r\n"+strMatr);
						
						CADBlock block= ((CADInsert)cad[i]).Block;
						if(block!=null)
							ParseInsert(block.Entities,pHeight,pWidth);
						stw.Write("\r\n</g>");
						break;
					}
					case "CADViewPort":
					{
                        CADViewPort port1 = (CADViewPort)cad[i];
						DRect rect1 = port1.Rect;
						DPoint point1 = GetPoint(rect1.TopLeft);
						DPoint point2 = GetPoint(rect1.BottomRight);
						DPoint point3 = GetPoint(new DPoint(rect1.right, rect1.top, 0));
						DPoint point4 = GetPoint(new DPoint(rect1.left, rect1.bottom, 0));
						
						color=ColorFunc.GetColorString(getColor(cad[i]));
						string points = point1.X.ToString() + " " + point1.Y.ToString() + ",";
						points += point3.X.ToString() + " " + point3.Y.ToString() + ",";
						points += point2.X.ToString() + " " + point2.Y.ToString() + ",";
						points += point4.X.ToString() + " " + point4.Y.ToString() + ",";
						points += point1.X.ToString() + " " + point1.Y.ToString();

						Polyline p1 = (Polyline)svg.CreateElement("", "polyline", "");
						p1.SetAttribute("points",points);
//						p1.SetAttribute("fill", "none");
						p1.SetAttribute("stroke",color);
						p1.SetAttribute("layer",layerid);
						stw.Write("\r\n");
						stw.Write(p1.OuterXml);


						break;
					}
				}
			}
		}
		#region IDisposable 成员

		public void Dispose()
		{
			this.img=null;
			this.grPath.Dispose();
			this.intPoints=null;
			this.svg=null;
		}

		#endregion
	}
}
