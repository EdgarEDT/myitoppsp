using System;
using System.Collections.Generic;
using System.Text;
using Itop.TLPSP.DEVICE;
using System.Xml;
using ItopVector.Core;
using ItopVector.Core.Interface.Figure;
using System.Windows.Forms;
using ItopVector.Core.Figure;
using System.Drawing;
using ItopVector.Core.Document;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;

namespace ItopVector.Tools
{
    /// <summary>
    /// 系统接线图线路定位
    /// </summary>
    partial class frmMain_wj
    {
        class OddEven {
            static private int s = 1;
            static public bool IsEven(int a) {
                return ((a & s) == 0);
            }
            static public bool IsOdd(int a) {
                return !IsEven(a);
            }
        }
        private void openAutojxt() {
            if (dlg == null) {
                dlg = new frmAutojxt();
                dlg.OnLoactionXL += new EventHandler(dlg_OnLoactionXL);
                dlg.FormClosed += new System.Windows.Forms.FormClosedEventHandler(dlg_FormClosed);
                dlg.ShowIcon = false;
                dlg.Owner = this;
                dlg.Show();
            } else {
                dlg.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
            
        }
        void locationJxtXL() {
            openAutojxt();
            
            Application.DoEvents();
            Polyline pol = tlVectorControl1.SVGDocument.CurrentElement as Polyline;
            if(pol!=null){
                string id = pol.GetAttribute("Deviceid");
                dlg.LocationXlbyId(id);
            }
        }
        void dlg_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e) {
            dlg = null;
        }
        frmAutojxt dlg = null;
        void dlg_OnLoactionXL(object sender, EventArgs e) {
            if (dlg != null) dlg.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            string deviceid = sender as string;
            ItopVectorControl xltVectorCtrl = tlVectorControl1;
            XmlNodeList elelist = xltVectorCtrl.SVGDocument.SelectNodes("svg/*[@Deviceid='" + deviceid + "']");
            if (elelist.Count == 1) {
                SvgElement ele = (SvgElement)elelist[0];
                xltVectorCtrl.GoLocation((IGraph)ele);
                xltVectorCtrl.Refresh();
                xltVectorCtrl.SVGDocument.CurrentElement = ele;
            } else {
                xltVectorCtrl.SVGDocument.SelectCollection.Clear();
                for (int i = 0; i < elelist.Count; i++) {
                    SvgElement ee = (SvgElement)elelist[i];

                    xltVectorCtrl.SVGDocument.SelectCollection.Add(ee);
                    xltVectorCtrl.Refresh();
                }
            }
        }
        /// <summary>
        /// 创建相关变电站图层的文字
        /// </summary>
        /// <param name="layer"></param>
        void createBdzInfo(Layer layer) {
            if (layer == null)
                layer = tlVectorControl1.SVGDocument.CurrentLayer;
            if (layer == null) return;
            if (!layer.Label.Contains("变电站")) return;

            string name = layer.Label + "名称文字";
            string name2 = layer.Label + "容量文字";
            Layer lay1=null;
            Layer lay2=null;
            foreach(Layer lay in tlVectorControl1.SVGDocument.Layers){
                if (lay.Label == name)
                    lay1 = lay;
                else if (lay.Label == name2)
                    lay2 = lay;
            }
            //tlVectorControl1.SVGDocument.AcceptChanges = false;
            if (lay1 == null) {
                lay1 = Layer.CreateNew(name, tlVectorControl1.SVGDocument);
                lay1.SetAttribute("layerType", "电网规划层");
            } else {
                for (int i = lay1.GraphList.Count - 1; i > 0; i--)
                    lay1.GraphList.RemoveAt(i);
            }
            if (lay2 == null) {
                lay2 = Layer.CreateNew(name2, tlVectorControl1.SVGDocument);
                lay2.SetAttribute("layerType", "电网规划层");
            } else {
                for (int i = lay2.GraphList.Count-1; i > 0; i--) 
                    lay2.GraphList.RemoveAt(i);
            }
            foreach (SvgElement ele in layer.GraphList) {
                if(!(ele is Use))continue;
                PSP_Substation_Info bdz = Services.BaseService.GetOneByKey<PSP_Substation_Info>(ele.GetAttribute("Deviceid"));
                if (bdz == null) continue;
                createName(ele as IGraph, lay1, bdz.Title);
                createName2(ele as IGraph, lay2, "("+bdz.L4+")");//容量组成

            }
            //tlVectorControl1.SVGDocument.AcceptChanges = true;
            lay1.Visible = false; 
            lay2.Visible = false; 
            frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
            frmlar.InitData();
            tlVectorControl1.Refresh();
        }
        private void createName(IGraph temp,Layer layer,string text) {
            Text n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
            
            RectangleF t = ((IGraph)temp).GetBounds();
            n1.SetAttribute("x", (t.X + t.Width ).ToString());
            n1.SetAttribute("y", (t.Y + t.Height / 2+150).ToString());
            string name = (temp as SvgElement).GetAttribute("info-name");
            n1.InnerText = string.IsNullOrEmpty(name)?text:name;
            n1.Layer = layer;
            //doc.CurrentLayer.Add(n1 as SvgElement);
            //n1.SetAttribute("ParentID", temp.ID);
            n1.SetAttribute("limitsize", "true");
            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
        }
        private void createName2(IGraph temp, Layer layer, string text) {
            Text n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;

            RectangleF t = ((IGraph)temp).GetBounds();
            n1.SetAttribute("x", (t.X + t.Width).ToString());
            n1.SetAttribute("y", (t.Y + t.Height / 2 + 350).ToString());

            n1.InnerText = text;
            n1.Layer = layer;
            //doc.CurrentLayer.Add(n1 as SvgElement);
            //n1.SetAttribute("ParentID", temp.ID);
            n1.SetAttribute("limitsize", "true");
            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
        }
        /// <summary>
        /// 根据设备关系创建线路
        /// </summary>
        /// <param name="device">变电站图元</param>
        /// <param name="devicSUID">变电站设备ID</param>
        void createLine(XmlElement device, string devicSUID) {
            string projectid=Itop.Client.MIS.ProgUID;
            string strCon = string.Format(" where Type = '01' and projectid='{0}' and svguid='{1}'",projectid,devicSUID);
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
            SvgElementCollection list2 = tlVectorControl1.SVGDocument.CurrentLayer.GraphList.Clone();// tlVectorControl1.SVGDocument.SelectNodes("svg/use");
            float scale = tlVectorControl1.ScaleRatio;
            //scale = 1;
            foreach (PSPDEV dev in list) {
                foreach (SvgElement element in list2) {
                    if (!(element is Use)) continue;
                    ///XmlElement element = node as XmlElement;
                    //XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                    string deviceid = (element).GetAttribute("Deviceid");
                    if (devicSUID == deviceid) continue;
                    string strCon1 = string.Format(" where Type = '01' and projectid='{0}' and svguid='{1}'", projectid, deviceid); //" where projectid='" + projectid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01'";
                    string label = element.GetAttribute("info-name");
                    IList list3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon1);
                    foreach (PSPDEV pd in list3) {
                        if (dev.Number != pd.Number) {
                            string strCon2 = " where projectid = '" + projectid + "' AND Type = '05' AND FirstNode = '" + dev.Number + "' AND LastNode = '" + pd.Number + "'";
                            IList list4 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);

                            string strCon3 = "where projectid = '" + projectid + "' AND Type = '05' AND FirstNode = '" + pd.Number + "' AND LastNode = '" + dev.Number + "'";
                            IList list5 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                            float width =  ((IGraph)element).GetBounds().Width/3;
                            for (int i = 0; i < list4.Count; i++) {
                                PointF[] t2 = new PointF[] { ((IGraph)device).CenterPoint, ((IGraph)element).CenterPoint };
                                float angel = 0f;
                                
                                angel = (float)(180 * Math.Atan2((t2[1].Y - t2[0].Y), (t2[1].X - t2[0].X)) / Math.PI);
                                PointF pStart1 = new PointF(((IGraph)device).CenterPoint.X + (float)(width * ((i + 1) / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)device).CenterPoint.Y - (float)(width * ((i + 1) / 2) * Math.Cos((angel) * Math.PI / 180)));
                                PointF pStart2 = new PointF(((IGraph)device).CenterPoint.X - (float)(width * (i / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)device).CenterPoint.Y + (float)(width * (i / 2) * Math.Cos((angel) * Math.PI / 180)));

                                PointF pStart3 = new PointF(((IGraph)element).CenterPoint.X + (float)(width * ((i + 1) / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)element).CenterPoint.Y - (float)(width * ((i + 1) / 2) * Math.Cos((angel) * Math.PI / 180)));
                                PointF pStart4 = new PointF(((IGraph)element).CenterPoint.X - (float)(width * (i / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)element).CenterPoint.Y + (float)(width * (i / 2) * Math.Cos((angel) * Math.PI / 180)));

                                string temp = "";
                                if (i == 0) {
                                    temp = ((IGraph)device).CenterPoint.X.ToString() + " " + ((IGraph)device).CenterPoint.Y.ToString() + "," + ((IGraph)element).CenterPoint.X + " " + ((IGraph)element).CenterPoint.Y.ToString();
                                } else if (OddEven.IsOdd(i)) {
                                    temp = pStart1.X.ToString() + " " + pStart1.Y.ToString() + "," + pStart3.X.ToString() + " " + pStart3.Y.ToString();
                                } else if (OddEven.IsEven(i)) {
                                    temp = pStart2.X.ToString() + " " + pStart2.Y.ToString() + "," + pStart4.X.ToString() + " " + pStart4.Y.ToString();
                                }
                                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;

                                n1.SetAttribute("points", temp);
                                n1.SetAttribute("IsLead", "1");

                                n1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                n1.SetAttribute("layer", SvgDocument.currentLayer);
                                n1.SetAttribute("FirstNode", device.GetAttribute("id"));
                                n1.SetAttribute("LastNode", element.GetAttribute("id"));
                                n1.SetAttribute("Deviceid", ((PSPDEV)list4[i]).SUID);
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                                tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                //n1.RemoveAttribute("layer");
                                tlVectorControl1.Operation = ToolOperation.Select;
                                tlVectorControl1.SVGDocument.CurrentElement = element as SvgElement;
                            }
                            int j = 0;
                            if (list4 != null) {
                                j = list4.Count;
                            }
                            for (int i = j; i < j + list5.Count; i++) {
                                PointF[] t2 = new PointF[] { ((IGraph)element).CenterPoint, ((IGraph)device).CenterPoint };
                                float angel = 0f;
                                angel = (float)(180 * Math.Atan2((t2[1].Y - t2[0].Y), (t2[1].X - t2[0].X)) / Math.PI);
                                PointF pStart1 = new PointF(((IGraph)element).CenterPoint.X + (float)( width * ((i + 1) / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)element).CenterPoint.Y - (float)(width * ((i + 1) / 2) * Math.Cos((angel) * Math.PI / 180)));
                                PointF pStart2 = new PointF(((IGraph)element).CenterPoint.X - (float)(width * (i / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)element).CenterPoint.Y + (float)(width * (i / 2) * Math.Cos((angel) * Math.PI / 180)));

                                PointF pStart3 = new PointF(((IGraph)device).CenterPoint.X + (float)( width * ((i + 1) / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)device).CenterPoint.Y - (float)(width * ((i + 1) / 2) * Math.Cos((angel) * Math.PI / 180)));
                                PointF pStart4 = new PointF(((IGraph)device).CenterPoint.X - (float)(width * (i / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)device).CenterPoint.Y + (float)(width * (i / 2) * Math.Cos((angel) * Math.PI / 180)));
                                string temp = "";
                                if (i == 0) {
                                    temp = ((IGraph)element).CenterPoint.X.ToString() + " " + ((IGraph)element).CenterPoint.Y.ToString() + "," + ((IGraph)device).CenterPoint.X + " " + ((IGraph)device).CenterPoint.Y.ToString();
                                } else if (OddEven.IsOdd(i)) {
                                    temp = pStart1.X.ToString() + " " + pStart1.Y.ToString() + "," + pStart3.X.ToString() + " " + pStart3.Y.ToString();
                                } else if (OddEven.IsEven(i)) {
                                    temp = pStart2.X.ToString() + " " + pStart2.Y.ToString() + "," + pStart4.X.ToString() + " " + pStart4.Y.ToString();
                                }
                                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                                n1.SetAttribute("points", temp);
                                n1.SetAttribute("IsLead", "1");
                                n1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                n1.SetAttribute("layer", SvgDocument.currentLayer);
                                n1.SetAttribute("FirstNode", element.GetAttribute("id"));
                                n1.SetAttribute("LastNode", device.GetAttribute("id"));
                                n1.SetAttribute("Deviceid", ((PSPDEV)list5[i - j]).SUID);
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                                tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                //n1.RemoveAttribute("layer");
                                tlVectorControl1.Operation = ToolOperation.Select;
                                tlVectorControl1.SVGDocument.CurrentElement = element as SvgElement;
                            }
                        }
                    }
                }
            }
        }
    }
}
