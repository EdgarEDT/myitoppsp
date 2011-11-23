using System;
using System.Collections.Generic;
using System.Text;
using ItopVector.Tools;
using ItopVector;
using System.Configuration;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using System.Drawing;
using System.Xml;
using ItopVector.Core;
using System.Collections;
using ItopVector.Core.Interface.Figure;
using System.IO;
using System.Drawing.Drawing2D;

namespace Itop.TLPsp
{
    /// <summary>
    /// 导入地理接线图转化为潮流图
    /// 完成度100%
    /// </summary>
    public class ImportJxt
    {
        ItopVector.ItopVectorControl tlVectorControl1;
        public ImportJxt(ItopVectorControl vc) {
            tlVectorControl1 = vc;

        }
        string svgid;
        string fileid;
        public void Import( ) {
            svgid = tlVectorControl1.SVGDocument.SvgdataUid;
            if (string.IsNullOrEmpty(svgid)) return;
            frmLayerSel sel = new frmLayerSel();
            if (sel.ShowDialog() == DialogResult.OK) {
                if (sel.LayList.Count == 0) return;
                Import( sel.LayList);
                
            }
        }
        Hashtable bdzlist = new Hashtable();
        Hashtable linelist = new Hashtable();
        private void Import(IList larlist) {
            try {
                bdzlist.Clear();
                linelist.Clear();
                string uid = ConfigurationManager.AppSettings["SvgID"];//地理图形文件ID
                fileid = uid;
                StringBuilder svgxml = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><svg id=\"svg\" width=\"1500\" height=\"1000\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:itop=\"http://www.Itop.com/itop\" >");

                svgxml.AppendLine("<defs>");             
                svgxml.AppendLine("<layer id=\"layer0\" label=\"默认层\" />");
                SVG_SYMBOL sym = new SVG_SYMBOL();
                sym.svgID = uid;
                IList<SVG_SYMBOL> symlist = Services.BaseService.GetList<SVG_SYMBOL>("SelectSVG_SYMBOLBySvgID", sym);

                foreach (SVG_SYMBOL _sym in symlist) {
                    svgxml.AppendLine(_sym.XML);
                }
                svgxml.AppendLine("</defs>");
                foreach (SVG_LAYER _lar in larlist) {
                    svgxml.AppendLine(_lar.XML);
                }
                svgxml.AppendLine("</svg>");

                SvgDocument document = new SvgDocument();
                document.LoadXml(svgxml.ToString());
                string SVGUID = svgid;

                if (document.RootElement == null) {
                    MessageBox.Show("生成失败！");
                    return;
                }
                document.FileName = tlVectorControl1.SVGDocument.FileName;
                tlVectorControl1.SVGDocument = document;
                tlVectorControl1.SVGDocument.SvgdataUid = SVGUID;
                tlVectorControl1.DocumentbgColor = Color.White;
                tlVectorControl1.BackColor = Color.White;
                SVG svg =document.RootElement as SVG;
                Layer layer = document.GetLayerByID("layer0");
                document.CurrentLayer = layer;

                tlVectorControl1.DrawArea.RenderTo(tlVectorControl1.CreateGraphics());
                bool flag1 = document.AcceptChanges;
                document.AcceptChanges = false;
                //查找线路和变电站
                foreach (SvgElement ele in svg.ChildList) {
                    IGraph g = ele as IGraph;
                    g.Layer = layer;                    
                    if (ele.LocalName == "polyline") {
                        if (ele.GetAttribute("IsLead") == "1") {//线路
                            linelist.Add(ele.ID, ele);
                        }
                    } else if(ele is Use){// (ele.GetAttribute("xlink:href").Contains("Substation")) {//节点
                        bdzlist.Add(ele.ID, ele);
                    }
                }

                List<SvgElement> list1 = new List<SvgElement>();
#region 查找线路首尾节点
                foreach (Polyline pl in linelist.Values) {
                    PointF[] ps = new PointF[2];
                    getNode(pl);
                    string s1 = pl.GetAttribute("FirstNode");
                    string s2 = pl.GetAttribute("LastNode");
                    bool b1 = pl.GetAttribute("ftj") == "true";//首节点T接
                    bool b2 = pl.GetAttribute("ltj") == "true";//尾节点T接
                    if (!((!string.IsNullOrEmpty(s1)||b1) && (!string.IsNullOrEmpty(s2)||b2))) {
                        pl.ParentNode.RemoveChild(pl);
                        continue;
                    }                    
                    
                    Use use1 = bdzlist[s1] as Use;
                    Use use2 = bdzlist[s2] as Use;
                    ps[0] = pl.Points[0];
                    ps[1] = pl.Points[pl.Points.Length - 1];
                    pl.Points = ps;
                    list1.Add(pl);
                    //pl.Transform.Matrix.TransformPoints(ps);
                    if (b1) {
                        //createT(pl);
                    }
                    else if (use1 != null) {
                        //ps[0] = use1.CenterPoint;
                        list1.Add(use1);
                    }
                    if (b2) {
                        //createT(pl);
                    } else if (use2 != null) {
                        //ps[1] = use2.CenterPoint;
                        list1.Add(use2);
                    }


                }
#endregion

#region 移除没用Graph
                for (int i=svg.ChildList.Count-1;i>=0;i--){
                    SvgElement graph = svg.ChildList[i] as SvgElement;
                    if (!list1.Contains(graph) ) {
                        graph.ParentNode.RemoveChild(graph);
                    }
                }
                
#endregion

#region debug
                //StreamWriter sw = new StreamWriter("c:\\importjxt.txt");
                //sw.Write(ItopVector.Core.Func.CodeFunc.FormatXmlDocumentString(document));
                //sw.Flush();
                //sw.Close();
#endregion
                Matrix mx = getMatrix(list1);//获取缩放矩阵
#region 缩放变电站节点
                foreach (IGraph graph in svg.ChildList) {
                    if (graph is Use) {
                        bool flag = graph.LimitSize;
                        if (flag) graph.LimitSize = false;
                        using (Matrix matrix1 = graph.Transform.Matrix.Clone()) {
                            Use use = graph as Use;
                            PointF p1 = graph.CenterPoint;
                            PointF p2 = (use.RefElement as IGraph).CenterPoint;
                            PointF[] pts = new PointF[1] { p1};
                            mx.TransformPoints(pts);
                            p1 = pts[0];
                            use.X = 0;
                            use.Y = 0;
                            matrix1.Reset();
                            matrix1.Translate(p1.X - p2.X, p1.Y - p2.Y);
                            graph.Transform = new ItopVector.Core.Types.Transf(matrix1);
                            graph.GPath.Reset();
                        }
                        graph.LimitSize = flag;
                        //createName(document, graph);
                    }
                }
#endregion

#region 缩放线路
                foreach (IGraph graph in svg.ChildList) {
                    if (graph is Polyline) {
                        using (Matrix matrix1 = graph.Transform.Matrix.Clone()) {
                            Polyline pl = graph as Polyline;
                            PointF[] ps = pl.Points.Clone() as PointF[];
                            string s1 = pl.GetAttribute("FirstNode");
                            string s2 = pl.GetAttribute("LastNode");
                            bool b1 = pl.GetAttribute("ftj")=="true";//首节点T接
                            bool b2 = pl.GetAttribute("ltj") == "true";//尾节点T接

                            Use use1 = bdzlist[s1] as Use;
                            Use use2 = bdzlist[s2] as Use;
                            ps[0] = pl.Points[0];
                            ps[1] = pl.Points[pl.Points.Length - 1];
                            
                            matrix1.Multiply(mx,MatrixOrder.Append);
                            matrix1.TransformPoints(ps);
                            if (use1 != null && !b1) {
                                ps[0] = use1.CenterPoint;
                            }
                            if (use2 != null && !b2) {
                                ps[1] = use2.CenterPoint;
                            }
                            pl.Points = ps;
                            pl.RemoveAttribute("transform");  
                        }
                    }
                }
#endregion

#region 处理T接
                foreach (SvgElement e in list1) {
                    if (e is Polyline) {

                        Polyline pl = e as Polyline;
                        bool b1 = pl.GetAttribute("ftj") == "true";//首节点T接
                        bool b2 = pl.GetAttribute("ltj") == "true";//尾节点T接
                        //处理T接,增加T节点
                        if (b1) {
                            SvgElement se = createT(pl, 0);
                            if (se != null) {
                                pl.SetAttribute("FirstNode", se.ID);
                                createsub(se);
                            }
                        }
                        if (b2) {
                            SvgElement se = createT(pl, 1);
                            if (se != null) {
                                pl.SetAttribute("LastNode", se.ID);
                                createsub(se);
                            }
                        }
                        PSPDEV dev = createline(e);
                    } else {
                        createsub(e);
                        createName(document, e as IGraph);
                    }
                }
#endregion
                document.AcceptChanges = flag1;
                
#region debug
                //StreamWriter sw2 = new StreamWriter("c:\\importjxt2.txt");
                //sw2.Write(ItopVector.Core.Func.CodeFunc.FormatXmlDocumentString(document));                
                //sw2.Flush();
                //sw2.Close();
#endregion
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }
        private void createT(PointF location) {
            SvgElement se = null;
            try {
                se =tlVectorControl1.CreateBySymbolID("Substation-t", location);                

            } catch { }
        }
        private SvgElement createT(Polyline pl, int index) {
            SvgElement se = null;
            try {
                se = tlVectorControl1.CreateBySymbolID("Substation-t", pl.Points[index]);
                (se as IGraph).Layer = tlVectorControl1.SVGDocument.CurrentLayer;
                string name = "T" + index + "_" + pl.ID;
                se.SetAttribute("info-name", name);
                se.SetAttribute("print", "no");
                
                tlVectorControl1.SVGDocument.RootElement.AppendChild(se);
                se.ID = name;

            } catch { }
            return se;
        }
        private void delDev(SvgElement element) {
            PSPDEV pspDev = new PSPDEV();
            pspDev.EleID = element.ID;
            pspDev.SvgUID = svgid;
            PSPDEV pspDev2 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
            if (pspDev2 != null)
                Services.BaseService.Delete<PSPDEV>(pspDev2);
        }
        //查找线路首末节点
        private void getNode(Polyline pl) {
            string s1 = pl.GetAttribute("FirstNode");
            string s2 = pl.GetAttribute("LastNode");
            LineInfo line = new LineInfo();
            line.EleID = pl.ID;
            line.SvgUID = fileid;
            IList svglist = Services.BaseService.GetList("SelectLineInfoByEleID", line);
            if (svglist.Count == 0) return;
            line = svglist[0] as LineInfo;
            if (line==null) {
                return;
            }
            
            if (string.IsNullOrEmpty(s1) && line!=null) {
                s1 = getIdByName( line.ObligateField6);
                if (!string.IsNullOrEmpty(s1))
                    pl.SetAttribute("FirstNode", s1);
            }
            if (string.IsNullOrEmpty(s2) && line != null) {
                s2 = getIdByName(line.ObligateField7);
                if (!string.IsNullOrEmpty(s2))
                    pl.SetAttribute("LastNode", s2);
            }

        }
        private string getIdByName(string name) {
            string id="";
            foreach (Use use in bdzlist.Values) {
                if (use.GetAttribute("info-name") == name) {
                    id = use.ID;
                    break;
                }                
            }
            return id;
        }
        private void createName(SvgDocument doc ,IGraph temp) {
            Text n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
            if (temp is Polyline) {
                PointF[] ps = (temp as Polyline).Points;
                float x1 = ps[0].X;
                float y1 = ps[0].Y;
                float x2 = ps[1].X;
                float y2 = ps[1].Y;
                n1.SetAttribute("x", Convert.ToString(x1 + (x2 - x1) / 2));
                n1.SetAttribute("y", Convert.ToString(y1 + (y2 - y1) / 2));
            } else {                
                RectangleF t = ((IGraph)temp).GetBounds();
                n1.SetAttribute("x", t.X.ToString());
                n1.SetAttribute("y", t.Y.ToString());                
            }
            n1.InnerText = (temp as SvgElement).GetAttribute("info-name");
            n1.Layer = doc.CurrentLayer;
            //doc.CurrentLayer.Add(n1 as SvgElement);
            n1.SetAttribute("ParentID", temp.ID);
            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
        }
        private Matrix getMatrix(ICollection graphs) {
            RectangleF rf = RectangleF.Empty;
            RectangleF rf0 = RectangleF.Empty;

            foreach (IGraph graph in graphs) {
                rf0 = graph.GetBounds();
                if (rf == RectangleF.Empty)
                    rf = rf0;
                else
                    rf = RectangleF.Union(rf,rf0 );
            }
            float f1 = 1500 / rf.Width;
            float f2 = 1000 / rf.Height;
            float f3 = Math.Min(1, Math.Min(f1, f2));
            PointF centPt = new PointF(-rf.Left, -rf.Top);
            Matrix mx = new Matrix();
            mx.Translate(centPt.X, centPt.Y);
            float offx = 0, offy = 0;

            if (f1 < f2) {
                offy = Math.Abs(1000 - f3 * rf.Height) / 2;

            } else {
                offx = Math.Abs(1500 - f3 * rf.Width) / 2;
            }
            
            mx.Scale(f3, f3, MatrixOrder.Append);
            mx.Translate(offx, offy,MatrixOrder.Append);
            return mx;
        }
        private PSPDEV createline(SvgElement element) {
            PSPDEV pspDev = new PSPDEV();
            pspDev.SUID = Guid.NewGuid().ToString();
            pspDev.EleID = element.ID;
            pspDev.SvgUID = svgid;
            PSPDEV pspDev2 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
            if (pspDev2 == null) {
                pspDev.Number = -1;
                pspDev.FirstNode = -1;
                pspDev.LastNode = -1;
                pspDev.Type = "Polyline";
                pspDev.Lable = "支路";
                pspDev.Name = element.GetAttribute("info-name");
                IList list = Services.BaseService.GetList("SelectLineInfoByWhere", "eleid='" + pspDev.EleID + "'");
                if (list.Count > 0) {
                    LineInfo line = list[0] as LineInfo;
                    pspDev.Name = line.LineName;
                    pspDev.VoltR = double.Parse(line.Voltage);
                    pspDev.LineStatus = line.ObligateField1;
                    try {
                        pspDev.LineLength = double.Parse(line.Length);
                    } catch { }
                    pspDev.LineType = line.LineType;
                }
                Services.BaseService.Create<PSPDEV>(pspDev);
            } else {
                pspDev = pspDev2;
            }
            return pspDev;
        }
        private PSPDEV createsub(SvgElement element) {
            PSPDEV pspDev = new PSPDEV();
            pspDev.SUID = Guid.NewGuid().ToString();
            pspDev.EleID = element.ID;
            pspDev.SvgUID = svgid;
            PSPDEV pspDev2 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
            if (pspDev2 == null) {
                pspDev.Number = -1;
                pspDev.FirstNode = -1;
                pspDev.LastNode = -1;
                pspDev.Type = "Use";
                pspDev.Lable = "变电站";
                 pspDev.Name = element.GetAttribute("info-name");
                IList list = Services.BaseService.GetList("SelectsubstationByWhere", "eleid='" + pspDev.EleID + "'");
                if (list.Count > 0) {
                    substation sub = list[0] as substation;
                    pspDev.Name = sub.EleName;
                    pspDev.VoltR = double.Parse(sub.ObligateField1);//电压
                    pspDev.Burthen = sub.Number;//容量
                }
                Services.BaseService.Create<PSPDEV>(pspDev);
            } else {
                pspDev = pspDev2;
            }
            return pspDev;
        }
    }
}
