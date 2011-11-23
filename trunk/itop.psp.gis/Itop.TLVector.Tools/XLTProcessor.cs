using System;
using System.Collections.Generic;
using System.Text;
using ItopVector.Core.Document;
using ItopVector.Core;
using ItopVector;
using System.Collections;
using ItopVector.Core.Figure;
using System.Reflection;
using ItopVector.Selector;
using System.Xml;
using System.Drawing;
using ItopVector.Core.Func;
using System.Windows.Forms;
using ItopVector.Core.Interface.Figure;
using Itop.MapView;
using Itop.Domain.Graphics;
using Itop.Common;
using Itop.Client.Common;

namespace ItopVector.Tools
{
    public delegate string NewLineDelegate(List<string> existLineCode);

    public enum NodeDevType
    {
        SB_NONE = 0,
        SB_BDZ,//变电站
        SB_GT,//杆塔
        SB_FZX,//分支箱
        SB_XSB,//箱式变
        SB_HWG,//环网柜
        SB_KBS,//开闭锁
        SB_XNJD,//虚拟节点
        SB_GT1//拆除标记
    }

    public class XLTProcessor
    {
        private SvgDocument xltDocument = null;
        private ItopVectorControl xltVectorCtrl = null;
        private List<string> nodeSymbolIDList = new List<string>();

        private SvgElement currentElement = null;//控件中当前选中的设备
        private Use symbolElement = null;//拖进来的图元
        private string newElementGuid = "";//选择图元后，记录GUID在此变量 
        private string newElementName = "";//选择图元后，记录名称在此变量
        private string newElementXLDM = "";//选择图元后，记录线路代码在此变量
        private bool isNewElementBranch = false;//新添加的节点是否是分支

        private readonly string SymbolTag = "xlink:href";
        private readonly string NodeNameTag = "info-name";//"devname";
        private readonly string NodeGuidTag = "devguid";
        private readonly string BranchNodeGuidTag = "branchnodeguid";//分支线路出口节点GUID
        private readonly string PreNodeGuidTag = "predevguid";
        private readonly string NodeXLDMTag = "devxldm";
        private readonly string ConnectionTag = "connected";
        private readonly string BDZTag = "Substation";

        private Timer timer;
        private Color lastSplashColor;
        private IMapViewObj mapView = null;

        public event NewLineDelegate OnNewLine;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tlVectCtrl"></param>
        public XLTProcessor(ItopVectorControl tlVectCtrl)
        {
            if (tlVectCtrl == null)
            {
                throw new Exception("null ItopVectorControl!");
            }
            xltVectorCtrl = tlVectCtrl;

            SetSymbolID();

            BindEvents();

            timer = new Timer();
            timer.Interval = 1000;
            //timer.Start();
            timer.Tick += new EventHandler(timer_Tick);
        }

        /// <summary>
        /// 绑定事件，
        /// </summary>
        public void BindEvents()
        {
            xltDocument = xltVectorCtrl.SVGDocument;
            xltVectorCtrl.DrawArea.BeforeAddSvgElement += new ItopVector.DrawArea.AddSvgElementEventHandler(DrawArea_BeforeAddSvgElement);
            xltVectorCtrl.DoubleLeftClick += new ItopVector.DrawArea.SvgElementEventHandler(xltVectorCtrl_DoubleLeftClick);
            xltDocument.NodeInserted += new System.Xml.XmlNodeChangedEventHandler(xltDocument_NodeInserted);
        }

        public void UnBindEvents()
        {
            xltVectorCtrl.DrawArea.BeforeAddSvgElement -= new ItopVector.DrawArea.AddSvgElementEventHandler(DrawArea_BeforeAddSvgElement);
            xltVectorCtrl.DoubleLeftClick -= new ItopVector.DrawArea.SvgElementEventHandler(xltVectorCtrl_DoubleLeftClick);
            xltDocument.NodeInserted -= new System.Xml.XmlNodeChangedEventHandler(xltDocument_NodeInserted);
        }

        /// <summary>
        /// 重设事件，当SvgDocument或者VectorControl发生改变时需要调用此方法
        /// </summary>
        public void ResetEvents()
        {
            UnBindEvents();
            BindEvents();
        }


        /// <summary>
        /// 测量2个节点之间的距离，包含之间的节点
        /// </summary>
        /// <param name="e1">节点1</param>
        /// <param name="e2">节点2</param>
        /// <returns>总距离</returns>
        public Double MeasureGT(SvgElement e1, SvgElement e2)
        {
            //if (e1.GetAttribute(NodeXLDMTag) == e2.GetAttribute(NodeXLDMTag))
            //{
            //    return MeasureGTSameLine(e1, e2);
            //}
            //else
            //{
            //    return MeasureGTDifferentLine(e1, e2);
            //}
            return XX(e1, e2, null);
        }

        private double MeasureGTSameLine(SvgElement e1, SvgElement e2)
        {
            double d = TryMeasureGT(e1, e2);

            if (d.Equals(double.NaN))
            {
                d = TryMeasureGT(e2, e1);
            }

            return d;
        }

        private double MeasureGTDifferentLine(SvgElement e1, SvgElement e2)
        {
            double d = double.NaN;
            string xldm1 = e1.GetAttribute(NodeXLDMTag);
            string xldm2 = e2.GetAttribute(NodeXLDMTag);
            int nSubLen = 3;

            while (xldm1.Length >= nSubLen && xldm2.Length >= nSubLen
                && xldm1.Substring(0, nSubLen) == xldm2.Substring(0, nSubLen))
            {
                nSubLen += 3;
            }

            nSubLen -= 3;

            //两节点汇集的线路代码
            string sameXldm = xldm1.Substring(0, nSubLen);

            SvgElement rtE1;
            SvgElement rtE2;
            double d1 = MeasurePathToParent(e1, sameXldm, out rtE1);
            double d2 = MeasurePathToParent(e2, sameXldm, out rtE2);

            d = d1 + d2;
            if (rtE2 != rtE1)
            {
                d += MeasureGTSameLine(rtE1, rtE2);
            }

            return d;
        }

        private double MeasurePathToParent(SvgElement e, string pXLDM, out SvgElement pEle)
        {
            double d = 0;
            if (pXLDM == e.GetAttribute(NodeXLDMTag))
            {
                pEle = e;
                return d;
            }

            SvgElement preE = GetPreNode2(e);
            while (preE != null)
            {
                IGraph gh1 = preE as IGraph;
                IGraph gh2 = e as IGraph;

                if (mapView == null)
                {
                    d = 0;
                }
                else
                {
                    d += mapView.CountLength(gh1.CenterPoint, gh2.CenterPoint);
                }

                e = preE;

                if (pXLDM == preE.GetAttribute(NodeXLDMTag))
                {
                    pEle = e;
                    return d;
                }
                preE = GetPreNode2(e);
            }
            pEle = e;
            return d;
        }


        /// <summary>
        /// 计算同一分支2点之间距离，包含中之间的结点，如果没有匹配上，则返回Double.NaN
        /// </summary>
        /// <param name="e1">起始点</param>
        /// <param name="e2">结束点</param>
        /// <returns>长度，如果没匹配上，则返回Double.NaN</returns>
        private Double TryMeasureGT(SvgElement e1, SvgElement e2)
        {
            Double d = 0;
            bool bMeet = false;
            SvgElement e = GetNextNode(e1);
            SvgElement oldE = e1;
            while (e != null)
            {
                IGraph gh1 = oldE as IGraph;
                IGraph gh2 = e as IGraph;

                if (mapView == null)
                {
                    d = 0;
                }
                else
                {
                    d += mapView.CountLength(gh1.CenterPoint, gh2.CenterPoint);
                }

                oldE = e;

                if (e == e2)
                {
                    bMeet = true;
                    break;
                }

                e = GetNextNode(e);
            }

            if (bMeet)
            {
                return d;
            }
            else
            {
                return double.NaN;
            }
        }

        private double XX(SvgElement e1, SvgElement e2, ConnectLine cl)
        {
            IGraph gh1 = e1 as IGraph;
            foreach (ConnectLine connectLine in gh1.ConnectLines)
            {
                if (cl == connectLine)
                {
                    continue;
                }

                SvgElement e = null;
                if (connectLine.StartGraph == gh1)
                {
                    e = connectLine.EndGraph as SvgElement;
                }
                else if (connectLine.EndGraph == gh1)
                {
                    e = connectLine.StartGraph as SvgElement;
                }

                if (e != null && e != e1 && nodeSymbolIDList.IndexOf(GetElementDevType(e).ToString()) > -1)
                {
                    if (e == e2)
                    {
                        IGraph gh2 = e2 as IGraph;
                        if (mapView == null)
                        {
                            return 0;
                        }
                        else
                        {
                            return mapView.CountLength(gh1.CenterPoint, gh2.CenterPoint);
                        }
                    }
                    else
                    {
                        double d = XX(e, e2, connectLine);
                        if (d >= 0)
                        {
                            IGraph gh = e as IGraph;

                            if (mapView == null)
                            {
                                return 0;
                            }
                            else
                            {
                                return mapView.CountLength(gh1.CenterPoint, gh.CenterPoint) + d;
                            }
                        }
                    }
                }
            }

            return -1;
        }


        void timer_Tick(object sender, EventArgs e)
        {
            foreach (XmlNode x in xltDocument.SelectNodes("/svg/connectline"))
            {
                ConnectLine cl = x as ConnectLine;
                if (cl != null && cl.GetAttribute(ConnectionTag) == "0")
                {
                    string clrStr = ColorFunc.GetColorString(Color.Red);
                    if (cl.SvgAttributes.ContainsKey("stroke"))
                    {
                        clrStr = cl.SvgAttributes["stroke"].ToString();
                    }
                    if (clrStr == ColorFunc.GetColorString(Color.Red))
                    {
                        lastSplashColor = Color.Red;
                        AttributeFunc.SetAttributeValue(cl, "stroke", ColorFunc.GetColorString(Color.Yellow));
                    }
                    else if (clrStr == ColorFunc.GetColorString(Color.Green))
                    {
                        lastSplashColor = Color.Green;
                        AttributeFunc.SetAttributeValue(cl, "stroke", ColorFunc.GetColorString(Color.Yellow));
                    }
                    else if (clrStr == ColorFunc.GetColorString(Color.Yellow))
                    {
                        if (lastSplashColor == Color.Green)
                        {
                            AttributeFunc.SetAttributeValue(cl, "stroke", ColorFunc.GetColorString(Color.Red));
                        }
                        else
                        {
                            AttributeFunc.SetAttributeValue(cl, "stroke", ColorFunc.GetColorString(Color.Green));
                        }
                    }
                }
            }
        }

        void xltVectorCtrl_DoubleLeftClick(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e)
        {
            //currentElement = xltDocument.CurrentElement;
            //if (IsNodeElement(currentElement))
            //{
            //    SelectWholeLine(currentElement.GetAttribute(NodeXLDMTag));

            //    //Delete();
            //    //MessageBox.Show(GetWholeLineLength(currentElement.GetAttribute(NodeXLDMTag)).ToString(), "");
            //    //DeleteWholeLine(currentElement.GetAttribute(NodeXLDMTag));
            //    //SetWholeLineAttribute(currentElement.GetAttribute(NodeXLDMTag), "stroke", "#FF0000");
            //}
        }

        void DrawArea_BeforeAddSvgElement(object sender, ItopVector.DrawArea.AddSvgElementEventArgs e)
        {
            symbolElement = e.SvgElement as Use;

            if (symbolElement == null)
            {
                return;
            }

            isNewElementBranch = false;

            currentElement = xltDocument.CurrentElement;

            if (symbolElement.GraphId.IndexOf(BDZTag) < 0 && !CheckCanInsert())
            {
                xltVectorCtrl.CurrentOperation = ToolOperation.Select;
                xltVectorCtrl.DrawArea.PreGraph = null;

                e.Cancel = true;
                return;
            }

            if (currentElement != null)
            {
                newElementXLDM = currentElement.GetAttribute(NodeXLDMTag);
            }

            newElementGuid = Guid.NewGuid().ToString();

            //要插入的为变电站
            if (symbolElement.GraphId.IndexOf(BDZTag) > -1)
            {
                newElementName = "变电站";
                newElementXLDM = "";
            }
            else//要插入的为其它设备
            {
                newElementName = "节点";
                if (IsBDZElement(currentElement) && OnNewLine != null)
                {
                    List<string> listLineCode = GetBDZConnectLines(currentElement);
                    newElementXLDM = OnNewLine(listLineCode);
                }
            }
            if (IsBDZElement(currentElement))
            {
                List<string> listLineCode = GetBDZConnectLines(currentElement);
                string strEleID="'m0y4'";
                for(int i=0;i<listLineCode.Count;i++){
                    strEleID = strEleID + ",'" + listLineCode[i]+"'";
                }
                if (listLineCode.Count > 0)
                {
                    LineInfo l = new LineInfo();
                    l.SvgUID = xltVectorCtrl.SVGDocument.SvgdataUid;
                    l.EleID=strEleID;
                    IList lineList= Services.BaseService.GetList("SelectLineInfoByEleIDList",l);

                    frmAddLineSel f = new frmAddLineSel();
                    f.StrList = listLineCode;
                    f.LineList = lineList;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        if (f.newLine)
                        {
                            newElementXLDM = OnNewLine(listLineCode);
                        }
                        else
                        {
                            newElementXLDM = f.LineCode;
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        void xltDocument_NodeInserted(object sender, System.Xml.XmlNodeChangedEventArgs e)
        {
            SvgElement newSvgElement = e.Node as SvgElement;
            if (newSvgElement == null || newSvgElement.GetAttribute(SymbolTag) == "")
            {
                return;
            }

            if (symbolElement == null)
            {
                return;
            }

            newSvgElement.SetAttribute(NodeGuidTag, newElementGuid);
            newSvgElement.SetAttribute(NodeNameTag, newElementName);
            newSvgElement.SetAttribute(NodeXLDMTag, newElementXLDM);
            newSvgElement.SetAttribute(PreNodeGuidTag, "");
            newSvgElement.SetAttribute(BranchNodeGuidTag, "");

            if (IsNodeElement(newSvgElement))
            {
                if (IsBDZElement(newSvgElement))//变电站
                {

                }
                else//其它节点设备
                {
                    AddNewNodeDev(newSvgElement, isNewElementBranch);
                }
            }

            symbolElement = null;
        }


        /// <summary>
        /// 添加新节点设备，添加时根据连接情况设置连接线
        /// </summary>
        /// <param name="newElement">新节点设备</param>
        private void AddNewNodeDev(SvgElement newElement, bool isBranch)
        {
            SvgElement nextElement = GetNextNode(currentElement);
            if (IsBDZElement(currentElement))
            {
                nextElement = GetNextNode(currentElement, newElementXLDM);
            }

            if (nextElement != null)//如果当前节点有下一节点
            {
                nextElement.SetAttribute(PreNodeGuidTag, newElement.GetAttribute(NodeGuidTag));
                ConnectLine connectLine = GetConnectLine(nextElement, false);
                if (connectLine != null)
                {
                    connectLine.SetAttribute("devguid2", newElement.GetAttribute(NodeGuidTag));

                    connectLine.EndGraph = newElement as IGraph;
                }
                AddConnectline(newElement, nextElement);
            }
            else
            {
                AddConnectline(currentElement, newElement);
            }

            if (isBranch)
            {
                newElement.SetAttribute(BranchNodeGuidTag, currentElement.GetAttribute(NodeGuidTag));
            }
            else
            {
                newElement.SetAttribute(PreNodeGuidTag, currentElement.GetAttribute(NodeGuidTag));
            }
        }

        private void AddNewAccDev(SvgElement newElement)
        {
            AddConnectline(currentElement, newElement);
        }

        private void AddConnectline(SvgElement e1, SvgElement e2)
        {
            AddConnectline(e1, e2, null);
        }

        /// <summary>
        /// 在2个元件之间添加连接线
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <param name="sampleCL"></param>
        private void AddConnectline(SvgElement e1, SvgElement e2, ConnectLine sampleCL)
        {
            if (e1 == null || e2 == null)
            {
                return;
            }

            ConnectLine newConnectLine = xltDocument.CreateElement("connectline") as ConnectLine;
            newConnectLine.SetAttribute("type", "line");
            newConnectLine.IsLock = false;
            newConnectLine.SetAttribute("layer", SvgDocument.currentLayer);
            newConnectLine.SetAttribute("devguid1", e1.GetAttribute(NodeGuidTag));
            newConnectLine.SetAttribute("devguid2", e2.GetAttribute(NodeGuidTag));
            newConnectLine.SetAttribute(NodeXLDMTag, e2.GetAttribute(NodeXLDMTag));
            //AttributeFunc.SetAttributeValue(newConnectLine, "start", "#" + e1.ID + ".4");
            //AttributeFunc.SetAttributeValue(newConnectLine, "end", "#" + e2.ID + ".4");
            newConnectLine.StartGraph = e1 as IGraph;
            newConnectLine.EndGraph = e2 as IGraph;

            ConnectLine cl = GetConnectLine(e1, false);
            if (cl != null && (!cl.Visible || cl.GetAttribute(ConnectionTag) == "0"))
            {
                newConnectLine.SetAttribute(ConnectionTag, "0");
            }

            if (sampleCL != null)
            {

            }

            (xltDocument.RootElement.AppendChild(newConnectLine) as ConnectLine).CanSelect = true;
        }

        /// <summary>
        /// 删除当前选中的图元，如果选中多个，则一起删除
        /// </summary>
        public void Delete()
        {
            foreach (SvgElement e in xltDocument.SelectCollection)
            {
                if (IsNodeElement(e) && !IsBDZElement(e))
                {
                    DeleteNodeElement(e);
                }
            }
        }

        /// <summary>
        /// 删除当前选中的图元，多选情况下只删除一个，如果想删除当前所选的所有，请调用Delete方法
        /// </summary>
        public void DeleteElement()
        {
            currentElement = xltDocument.CurrentElement;
            if (currentElement == null || (currentElement as ConnectLine) != null)
            {
                return;
            }

            if (IsNodeElement(currentElement))
            {
                if (IsBDZElement(currentElement))
                {
                    IGraph gh = currentElement as IGraph;
                    if (gh.ConnectLines.Count == 0)
                    {
                        xltVectorCtrl.Delete();
                    }
                }
                else
                {
                    DeleteNodeElement(currentElement);
                }
            }
            else
            {
                xltVectorCtrl.Delete();
            }
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="e"></param>
        private void DeleteNodeElement(SvgElement e)
        {
            if (!IsNodeElement(e) || IsBDZElement(e))
            {
                return;
            }

            ConnectLine beforeLine = GetConnectLine(e, false);
            ConnectLine afterLine = GetConnectLine(e, true);

            if (beforeLine != null)
            {
                SvgElement preElement = beforeLine.StartGraph as SvgElement;
                if (afterLine != null)
                {
                    SvgElement nextElement = afterLine.EndGraph as SvgElement;
                    if (nextElement != null)
                    {
                        nextElement.SetAttribute(PreNodeGuidTag, preElement.GetAttribute(NodeGuidTag));

                        beforeLine.SetAttribute("devguid2", nextElement.GetAttribute(NodeGuidTag));
                        beforeLine.EndGraph = nextElement as IGraph;
                    }
                    xltDocument.CurrentElement = afterLine;
                    xltVectorCtrl.Delete();
                }
                else
                {
                    xltDocument.CurrentElement = beforeLine;
                    xltVectorCtrl.Delete();
                }
            }
            xltDocument.CurrentElement = e;
            xltVectorCtrl.Delete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="devType"></param>
        private void DeleteNodeDevElement(NodeDevType devType)
        {
            if (devType == NodeDevType.SB_BDZ && GetNextNode(currentElement) != null)
            {
                MsgBox.Show("删除变电站前请先删除与变电站相连的节点！");
                return;
            }

            List<ConnectLine> listConnectLine = GetNoneTrunkConnectLines(currentElement);
            if (listConnectLine.Count > 0)
            {
                MsgBox.Show("删除此节点前请先删除与此节点相连的设备或分支线路！");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除：" + currentElement.GetAttribute(NodeNameTag)) == DialogResult.No)
            {
                return;
            }

            if (devType != NodeDevType.SB_BDZ)
            {
                ConnectLine beforeLine = GetConnectLine(currentElement, false);
                ConnectLine afterLine = GetConnectLine(currentElement, true);

                if (beforeLine != null)
                {
                    SvgElement preElement = beforeLine.StartGraph as SvgElement;
                    if (afterLine != null)
                    {
                        SvgElement nextElement = afterLine.EndGraph as SvgElement;
                        if (nextElement != null)
                        {
                            if (preElement != null)
                            {
                                nextElement.SetAttribute(PreNodeGuidTag, preElement.GetAttribute(NodeGuidTag));

                                beforeLine.SetAttribute("devguid2", nextElement.GetAttribute(NodeGuidTag));
                                beforeLine.EndGraph = nextElement as IGraph;
                            }
                            else
                            {
                                nextElement.SetAttribute(PreNodeGuidTag, "");
                            }
                        }
                        xltDocument.CurrentElement = afterLine;
                        xltVectorCtrl.Delete();
                    }
                    else
                    {
                        xltDocument.CurrentElement = beforeLine;
                        xltVectorCtrl.Delete();
                    }
                }

            }
            xltDocument.CurrentElement = currentElement;
            xltVectorCtrl.Delete();
        }

        private void DeleteAccDevElement(NodeDevType devType)
        {
            if (MsgBox.ShowYesNo("是否删除：" + currentElement.GetAttribute(NodeNameTag)) == DialogResult.No)
            {
                return;
            }
            ConnectLine cl = GetConnectLine(currentElement, false);
            if (cl != null)
            {
                xltDocument.CurrentElement = cl;
                xltVectorCtrl.Delete();
            }
            xltDocument.CurrentElement = currentElement;
            xltVectorCtrl.Delete();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisconnectLine()
        {
            currentElement = xltDocument.CurrentElement;
            if (currentElement != null && currentElement.GetAttribute("layer") == SvgDocument.currentLayer
                && nodeSymbolIDList.IndexOf(GetElementDevType(currentElement).ToString()) > -1)
            {
                ConnectLine cl = GetConnectLine(currentElement, false);
                if (cl != null)
                {
                    cl.Visible = false;
                    xltVectorCtrl.DrawArea.InvalidateElement(cl as SvgElement);
                    ChangeSubLinesAttribute(currentElement as IGraph, Color.Red, "0");
                }
            }
        }

        /// <summary>
        /// 恢复连接
        /// </summary>
        public void ReConnectLine()
        {
            currentElement = xltDocument.CurrentElement;
            if (currentElement != null && currentElement.GetAttribute("layer") == SvgDocument.currentLayer
                && nodeSymbolIDList.IndexOf(GetElementDevType(currentElement).ToString()) > -1)
            {
                ConnectLine cl = GetConnectLine(currentElement, false);
                if (cl != null)
                {
                    cl.Visible = true;
                    xltVectorCtrl.DrawArea.InvalidateElement(cl as SvgElement);

                    //if (!cl.SvgAttributes.ContainsKey("stroke") || (cl.SvgAttributes.ContainsKey("stroke") && cl.SvgAttributes["stroke"].ToString() != ColorFunc.GetColorString(Color.Red) && cl.SvgAttributes["stroke"].ToString() != ColorFunc.GetColorString(Color.Yellow) && cl.SvgAttributes["stroke"].ToString() != ColorFunc.GetColorString(Color.Green)))
                    if (cl.GetAttribute(ConnectionTag) != "0")
                    {
                        ChangeSubLinesAttribute(currentElement as IGraph, Color.Black, "1");
                    }
                }
            }
        }

        /// <summary>
        /// 改变与指定节点相连的后面的连接线的颜色
        /// </summary>
        /// <param name="eg">节点设备</param>
        /// <param name="clr">颜色</param>
        private void ChangeSubLinesAttribute(IGraph eg, Color clr, string isConnect)
        {
            if (eg == null)
            {
                return;
            }

            foreach (ConnectLine connectLine in eg.ConnectLines)
            {
                if (connectLine.StartGraph == eg && connectLine.Visible)
                {
                    AttributeFunc.SetAttributeValue(connectLine, "stroke", ColorFunc.GetColorString(clr));
                    connectLine.SetAttribute(ConnectionTag, isConnect);
                    ChangeSubLinesAttribute(connectLine.EndGraph, clr, isConnect);
                }
            }
        }

        /// <summary>
        /// 取得与变电站相连的线路代码
        /// </summary>
        /// <param name="e">变电站</param>
        /// <returns>线路代码</returns>
        private List<string> GetBDZConnectLines(SvgElement e)
        {
            List<string> rtList = new List<string>();
            if (!IsBDZElement(e))
            {
                return rtList;
            }

            IGraph eg = e as IGraph;

            if (eg == null)
            {
                return rtList;
            }

            foreach (ConnectLine connectLine in eg.ConnectLines)
            {
                SvgElement endEle = connectLine.EndGraph as SvgElement;
                if (endEle != null)
                {
                    rtList.Add(endEle.GetAttribute(NodeXLDMTag));
                }
            }
            return rtList;
        }

        /// <summary>
        /// 取节点的非主线路连接线，包括连设备的连接线、连分支的连接线
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private List<ConnectLine> GetNoneTrunkConnectLines(SvgElement e)
        {
            List<ConnectLine> rtList = new List<ConnectLine>();

            IGraph eg = e as IGraph;

            if (eg == null)
            {
                return rtList;
            }

            foreach (ConnectLine connectLine in eg.ConnectLines)
            {
                if (connectLine.StartGraph == eg)
                {
                    SvgElement endEle = connectLine.EndGraph as SvgElement;
                    if (endEle != null)
                    {
                        //同一线路，如果不是节点则满足条件
                        if (endEle.GetAttribute(NodeXLDMTag) == e.GetAttribute(NodeXLDMTag))
                        {
                        }
                        else//非同一线路，表示分支，满足条件
                        {
                            rtList.Add(connectLine);
                        }
                    }
                }
            }
            return rtList;
        }

        /// <summary>
        /// 取得当前可选择的设备的起始节点GUID和结束节点ID
        /// </summary>
        /// <param name="startDevGUID"></param>
        /// <param name="endDevGUID"></param>
        private void GetStartAndEndGuid(ref string startDevGUID, ref  string endDevGUID)
        {
            startDevGUID = currentElement.Attributes[NodeGuidTag].Value;

            SvgElement nextNVNode = GetNextNotVirtualNode(currentElement);
            if (nextNVNode != null)
            {
                endDevGUID = nextNVNode.Attributes[NodeGuidTag].Value;
            }

            if (GetElementDevType(currentElement) == NodeDevType.SB_XNJD)
            {
                SvgElement preNVNode = GetPreNotVirtualNode(currentElement);
                if (preNVNode != null)
                {
                    startDevGUID = preNVNode.Attributes[NodeGuidTag].Value;
                }
            }
        }

        /// <summary>
        /// 得到指定设备的类型枚举值
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private NodeDevType GetElementDevType(SvgElement element)
        {
            if (element != null)
            {
                return GetElementDevType(element.GetAttribute(SymbolTag).Replace("#", ""));//前面有个#号，去掉
            }

            return NodeDevType.SB_NONE;
        }

        /// <summary>
        /// 把枚举名字转换成相应的枚举
        /// </summary>
        /// <param name="symbolTag"></param>
        /// <returns></returns>
        private NodeDevType GetElementDevType(string symbolTag)
        {
            if (nodeSymbolIDList.IndexOf(symbolTag) > -1)
            {
                return (NodeDevType)Enum.Parse(typeof(NodeDevType), symbolTag);
            }

            return NodeDevType.SB_NONE;
        }

        /// <summary>
        /// 查找指定节点同一线路的下一个非虚拟节点
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private SvgElement GetNextNotVirtualNode(SvgElement e)
        {
            SvgElement nextElement = GetNextNode(e);
            while (nextElement != null)
            {
                if (GetElementDevType(nextElement) != NodeDevType.SB_XNJD)
                {
                    return nextElement;
                }
                nextElement = GetNextNode(nextElement);
            }

            return null;
        }


        /// <summary>
        /// 查找指定节点同一线路的上一个非虚拟节点
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private SvgElement GetPreNotVirtualNode(SvgElement e)
        {
            SvgElement preElement = GetPreNode(e);
            while (preElement != null)
            {
                if (GetElementDevType(preElement) != NodeDevType.SB_XNJD)
                {
                    return preElement;
                }
                preElement = GetPreNode(preElement);
            }

            return null;
        }

        /// <summary>
        /// 查找指定节点同一线路的下一个节点
        /// </summary>
        /// <param name="e">指定的节点</param>
        /// <returns>同一线路的下一个节点</returns>
        private SvgElement GetNextNode(SvgElement e)
        {
            IGraph eg = e as IGraph;
            foreach (ConnectLine connectLine in eg.ConnectLines)
            {
                if (connectLine.StartGraph == eg)
                {
                    SvgElement endEle = connectLine.EndGraph as SvgElement;
                    if (endEle != null && IsNodeElement(endEle)
                         && (endEle.GetAttribute(NodeXLDMTag) == e.GetAttribute(NodeXLDMTag) || IsBDZElement(e)))
                    {
                        return endEle;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 查找变电站的指定线路的下一个节点
        /// </summary>
        /// <param name="e">指定的节点</param>
        /// <param name="strLineCode">线路代码</param>
        /// <returns>下一个节点</returns>
        private SvgElement GetNextNode(SvgElement e, string strLineCode)
        {
            IGraph eg = e as IGraph;
            foreach (ConnectLine connectLine in eg.ConnectLines)
            {
                if (connectLine.StartGraph == eg)
                {
                    SvgElement endEle = connectLine.EndGraph as SvgElement;
                    if (endEle != null && IsNodeElement(endEle)
                         && endEle.GetAttribute(NodeXLDMTag) == strLineCode)
                    {
                        return endEle;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 查找指定节点同一线路的上一个节点
        /// </summary>
        /// <param name="e">指定的节点</param>
        /// <returns>同一线路的上一个节点</returns>
        private SvgElement GetPreNode(SvgElement e)
        {
            IGraph eg = e as IGraph;
            foreach (ConnectLine connectLine in eg.ConnectLines)
            {
                if (connectLine.EndGraph == eg)
                {
                    SvgElement startEle = connectLine.StartGraph as SvgElement;
                    if (startEle != null && IsNodeElement(startEle)
                        && (startEle.GetAttribute(NodeXLDMTag) == e.GetAttribute(NodeXLDMTag) || IsBDZElement(startEle)))
                    {
                        return startEle;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 查找指定节点上一个节点，如果是分支第一个节点，则返回分支节点
        /// </summary>
        /// <param name="e">指定的节点</param>
        /// <returns>上一个节点</returns>
        private SvgElement GetPreNode2(SvgElement e)
        {
            IGraph eg = e as IGraph;
            foreach (ConnectLine connectLine in eg.ConnectLines)
            {
                if (connectLine.EndGraph == eg)
                {
                    SvgElement endEle = connectLine.StartGraph as SvgElement;
                    if (endEle != null
                        && nodeSymbolIDList.IndexOf(GetElementDevType(endEle).ToString()) > -1)
                    {
                        return endEle;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 根据设备GUID查找设备图形
        /// </summary>
        /// <param name="strGUID"></param>
        /// <returns></returns>
        private SvgElement GetElementByDevGUID(string strGUID)
        {
            return xltDocument.SelectSingleNode("svg/*[@layer='" + SvgDocument.currentLayer + "' and @" + NodeGuidTag + "='" + strGUID + "']") as SvgElement;
        }
        public void GoLocation(string svguid,string strSelLayer)
        {
            string strGUID = "";
            frmGoLocation frm = new frmGoLocation();
            frm.SvgUid = svguid;
            frm.StrSelLayer = strSelLayer;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                strGUID = frm.EleID;
                XmlNodeList elelist = xltDocument.SelectNodes("svg/*[@Deviceid='" + strGUID + "']") ;
                if (elelist.Count == 1)
                {
                    SvgElement ele = (SvgElement)elelist[0];
                    xltVectorCtrl.GoLocation((IGraph)ele);
                    xltVectorCtrl.Refresh();
                    xltVectorCtrl.SVGDocument.CurrentElement = ele;
                }
                else
                {
                    xltVectorCtrl.SVGDocument.SelectCollection.Clear();
                    for (int i = 0; i < elelist.Count;i++ )
                    {
                        SvgElement ee = (SvgElement)elelist[i];
                        
                        xltVectorCtrl.SVGDocument.SelectCollection.Add(ee);
                        xltVectorCtrl.Refresh();
                    }
                }
            }
        }
        /// <summary>
        /// 取指定节点分支线路代码
        /// </summary>
        /// <param name="e">节点</param>
        /// <returns>分支线路代码集合</returns>
        private ArrayList GetNodeBranch(SvgElement e)
        {
            ArrayList arList = new ArrayList();

            IGraph eg = e as IGraph;
            foreach (ConnectLine connectLine in eg.ConnectLines)
            {
                if (connectLine.StartGraph == eg)
                {
                    SvgElement endEle = connectLine.EndGraph as SvgElement;
                    //与节点相连的另一节点线路编号如果大于节点的线路编号，则表示是分支线路
                    if (endEle != null && endEle.Attributes[NodeXLDMTag].Value.Length > e.Attributes[NodeXLDMTag].Value.Length)
                    {
                        arList.Add(endEle.Attributes[NodeXLDMTag].Value);
                    }
                }
            }

            return arList;
        }

        /// <summary>
        /// 取与指定节点与其它节点相连的连接线，只查找本线路的，分支不算
        /// </summary>
        /// <param name="e">指定的节点</param>
        /// <param name="bStart">指定节点是否为起始点</param>
        /// <returns>查找结果，如果没有，则为null</returns>
        private ConnectLine GetConnectLine(SvgElement e, bool bStart)
        {
            IGraph eg = e as IGraph;

            if (eg == null)
            {
                return null;
            }

            foreach (ConnectLine connectLine in eg.ConnectLines)
            {
                if (bStart)
                {
                    if (connectLine.StartGraph == eg)
                    {
                        SvgElement endEle = connectLine.EndGraph as SvgElement;
                        if (endEle != null && IsNodeElement(endEle)
                             && (endEle.GetAttribute(NodeXLDMTag) == e.GetAttribute(NodeXLDMTag) || IsBDZElement(e)))
                        {
                            return connectLine;
                        }
                    }
                }
                else
                {
                    if (connectLine.EndGraph == eg)
                    {
                        return connectLine;
                    }
                }

            }

            return null;
        }

        /// <summary>
        /// 判断是当前状态下是否可插入新设备
        /// </summary>
        /// <returns></returns>
        private bool CheckCanInsert()
        {
            if (nodeSymbolIDList.IndexOf(symbolElement.GraphId) > -1 || symbolElement.GraphId.IndexOf(BDZTag) > -1)
            {
                if (!IsNodeElement(currentElement))
                {
                    MsgBox.Show("请选择节点设备！");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断是否节点设备
        /// </summary>
        /// <param name="element">图形</param>
        /// <returns>是否是线路节点</returns>
        public bool IsNodeElement(SvgElement element)
        {
            if (element != null && element.GetAttribute(SymbolTag) != "")
            {
                if (nodeSymbolIDList.IndexOf(GetElementDevType(element).ToString()) > -1 || IsBDZElement(element))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断是否变电站
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool IsBDZElement(SvgElement element)
        {
            if (element != null && element.GetAttribute(SymbolTag).IndexOf(BDZTag) > -1)
            {
                return true;
            }
            return false;
        }

        private void SetSymbolID()
        {
            Type t = typeof(NodeDevType);
            foreach (string s in Enum.GetNames(t))
            {
                if ((int)Enum.Parse(t, s) < 100)//节点设备
                {
                    if ((int)Enum.Parse(t, s) > 0)//节点设备
                    {
                        nodeSymbolIDList.Add(s);
                    }
                }
            }
        }

        /// <summary>
        /// 根据设备类型创建新设备
        /// </summary>
        /// <param name="devType">设备类型</param>
        /// <returns>创建的新设备</returns>
        private SvgElement CreateNewElementByType(NodeDevType devType, PointF pt)
        {
            SvgElement devElement = xltVectorCtrl.CreateBySymbolID(devType.ToString(), pt);

            return devElement;
        }


        /// <summary>
        /// 通过设备的DevClass得到DevType枚举
        /// </summary>
        /// <param name="devClass"></param>
        /// <returns></returns>
        private NodeDevType GetDevTypeByDevClass(string devClass)
        {
            if (devClass == "02")
            {
                return NodeDevType.SB_KBS;
            }
            else if (devClass == "03")
            {
                return NodeDevType.SB_HWG;
            }
            else if (devClass == "04")
            {
                return NodeDevType.SB_FZX;
            }
            else if (devClass == "05")
            {
                return NodeDevType.SB_XSB;
            }

            return NodeDevType.SB_GT;
        }

        /// <summary>
        /// 转换以12位长度字符串表示的经纬度为数字
        /// </summary>
        /// <param name="obj">字符串对象</param>
        /// <returns>经纬度数字</returns>
        public static decimal ParseText2Decimal(object obj)
        {
            if (obj == null)
            {
                return decimal.Zero;
            }

            string textValue = obj.ToString();

            if (textValue.Length < 12)
            {
                return decimal.Zero;
            }

            decimal d = decimal.Zero;

            try
            {
                String s = textValue.Substring(0, 3);
                s.Replace(" ", "0");
                d += int.Parse(s);

                s = textValue.Substring(3, 3);
                d += decimal.Parse(s) / 60;

                s = textValue.Substring(6, 3) + "." + textValue.Substring(9, 3);
                d += decimal.Parse(s) / 3600;
            }
            catch
            {

            }

            return d;
        }


        /// <summary>
        /// 当前选中的节点设备，如果选中的不是节点设备，则返回null
        /// </summary>
        //public SvgElement CurrentNodeElement
        //{
        //    get
        //    {
        //        currentElement = xltDocument.CurrentElement;
        //        if(currentElement != null && nodeSymbolIDList.IndexOf(GetElementDevType(currentElement).ToString()) > -1)
        //        {
        //            return currentElement;
        //        }
        //        return null;
        //    }
        //}



        //public Timer Timer
        //{
        //    get { return timer; }
        //    set { timer = value; }
        //}

        /// <summary>
        /// 取当前选中线路的线路代码
        /// </summary>
        public string GetCurrentLineCode()
        {
            currentElement = xltDocument.CurrentElement;
            if (IsNodeElement(currentElement))
            {
                return currentElement.GetAttribute(NodeXLDMTag);
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 取指定线路的变电站图形
        /// </summary>
        /// <param name="strLineCode">线路代码</param>
        /// <returns>变电站图形</returns>
        public SvgElement GetBDZElement(string strLineCode)
        {
            foreach (XmlNode x in xltDocument.SelectNodes("/svg/use"))
            {
                SvgElement e = x as SvgElement;
                if (e != null && IsNodeElement(e) && e.GetAttribute(NodeXLDMTag) == strLineCode)
                {
                    SvgElement preElement = GetPreNode(e);
                    if (IsBDZElement(preElement))
                    {
                        return preElement;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取指定线路的总长度
        /// </summary>
        /// <param name="strlineCode">线路代码，如果代码为空，则取当前选择的线路</param>
        /// <returns>总长度</returns>
        public double GetWholeLineLength(string strLineCode)
        {
            currentElement = xltDocument.CurrentElement;
            if (strLineCode == "" && (currentElement == null || !IsNodeElement(currentElement)))
            {
                return 0;
            }
            else if (strLineCode == "")
            {
                strLineCode = currentElement.GetAttribute(NodeXLDMTag);
            }

            SvgElement bdzElement = GetBDZElement(strLineCode);
            if (bdzElement == null)
            {
                return 0;
            }

            IGraph eg = bdzElement as IGraph;
            SvgElement nextElement = null;
            foreach (ConnectLine connectLine in eg.ConnectLines)
            {
                SvgElement endEle = connectLine.EndGraph as SvgElement;
                if (endEle != null && IsNodeElement(endEle)
                     && endEle.GetAttribute(NodeXLDMTag) == strLineCode)
                {
                    nextElement = endEle;
                    break;
                }
            }

            if (nextElement == null)
            {
                return 0;
            }

            SvgElement tempElemet = GetNextNode(nextElement);
            SvgElement endElement = null;
            while (tempElemet != null)
            {
                endElement = tempElemet;
                tempElemet = GetNextNode(endElement);
            }

            IGraph e2Next = nextElement as IGraph;

            return mapView.CountLength(eg.CenterPoint, e2Next.CenterPoint) + TryMeasureGT(nextElement, endElement);
        }

        /// <summary>
        /// 选中整条线路 
        /// </summary>
        /// <param name="strLineCode">线路代码</param>
        public void SelectWholeLine(string strLineCode)
        {
            xltDocument.SelectCollection.Clear();
            foreach (XmlNode x in xltDocument.SelectNodes("/svg/use"))
            {
                SvgElement e = x as SvgElement;
                if (e != null && IsNodeElement(e) && e.GetAttribute(NodeXLDMTag) == strLineCode)
                {
                    xltDocument.SelectCollection.Add(e);
                }
            }
        }
        public void SelectLine(string strLineCode)
        {
            xltDocument.SelectCollection.Clear();
            foreach (XmlNode x in xltDocument.SelectNodes("/svg/connectline"))
            {
                SvgElement e = x as SvgElement;
                if (e != null  && e.GetAttribute(NodeXLDMTag) == strLineCode)
                {
                    xltDocument.SelectCollection.Add(e);
                }
            }
        }
        /// <summary>
        /// 设置整条线路属性，颜色，线宽，线型等
        /// </summary>
        /// <param name="strLineCode">线路代码</param>
        /// <param name="attrName">属性名</param>
        /// <param name="attrValue">属性值</param>
        public void SetWholeLineAttribute(string strLineCode, string attrName, string attrValue)
        {
            foreach (XmlNode x in xltDocument.SelectNodes("/svg/use"))
            {
                SvgElement e = x as SvgElement;
                if (e != null && IsNodeElement(e) && e.GetAttribute(NodeXLDMTag) == strLineCode)
                {
                    AttributeFunc.SetAttributeValue(GetConnectLine(e, false), attrName, attrValue);
                }
            }
        }
        
        /// <summary>
        /// 删除整条线路
        /// </summary>
        /// <param name="strLineCode">要删除的线路代码</param>
        public void DeleteWholeLine(string strLineCode)
        {
            SvgElement bdzElement = GetBDZElement(strLineCode);
            foreach (XmlNode x in xltDocument.SelectNodes("/svg/use"))
            {
                SvgElement e = x as SvgElement;
                if (e != null && IsNodeElement(e) && e.GetAttribute(NodeXLDMTag) == strLineCode && e != bdzElement)
                {
                    //DeleteNodeElement(e);
                    xltDocument.CurrentElement = GetConnectLine(e, false);
                    xltVectorCtrl.Delete();

                    xltDocument.CurrentElement = e;
                    xltVectorCtrl.Delete();
                }
            }

            xltDocument.CurrentElement = bdzElement;
            //DeleteElement();
        }



        /// <summary>
        /// MapView对象，此对象用于计算距离
        /// </summary>
        public IMapViewObj MapView
        {
            get { return mapView; }
            set { mapView = value; }
        }
    }
}
