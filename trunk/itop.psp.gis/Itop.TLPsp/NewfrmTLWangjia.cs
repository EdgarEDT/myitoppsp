﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Collections;
using System.Xml;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Configuration;
using Itop.Client;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using ItopVector.DrawArea;
using ItopVector.Core;
using ItopVector.Core.Func;
using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using ItopVector.Core.Interface.Figure;
using Itop.Domain.Stutistic;
using Itop.Client.Base;
using System.IO;
using System.Threading;
using ItopVector.Tools;
using ItopVector.Core.Interface;
using System.Xml.XPath;
using ItopVector.Core.Types;
using System.Diagnostics;
using Itop.MapView;
using System.Runtime.InteropServices;
using NR_PowerFlow;
using PQ_POWERFLOWLib;
using Gauss_Seidel;
using ZYZ_POWER;
using shortcir_dll;
using N1Test;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
namespace Itop.TLPsp
{
    //public delegate void OnCloseDocumenthandler(object sender, string svgUid, string pid);
    //public enum CustomOperation
    //{
    //    OP_Default = 0,
    //    OP_MeasureGT,
    //    OP_MeasureDistance,
    //    OP_AreaEdit,
    //    OP_AreaCount
    //}
    //enum MouseEventFlag : uint
    //{
    //    Move = 0x0001,
    //    LeftDown = 0x0002,
    //    LeftUp = 0x0004,
    //    RightDown = 0x0008,
    //    RightUp = 0x0010,
    //    MiddleDown = 0x0020,
    //    MiddleUp = 0x0040,
    //    XDown = 0x0080,
    //    XUp = 0x0100,
    //    Wheel = 0x0800,
    //    VirtualDesk = 0x4000,
    //    Absolute = 0x8000
    //}

    public partial class NewfrmTLWangjia : FormBase
    {
        #region 对象声明
        SVGFILE svg = new SVGFILE();
        SvgDocument sdoc = new SvgDocument();
        glebeProperty gPro = new glebeProperty();

        DevComponents.DotNetBar.ToolTip tip;


        private ItopVector.Selector.SymbolSelector symbolSelector;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private DevComponents.DotNetBar.ComboBoxItem scaleBox;
        public Itop.MapView.IMapViewObj mapview;

        //private ButtonItem operationButton;
        private ButtonItem orderButton;
        private ButtonItem alignButton;
        private ButtonItem rotateButton;
        private bool fileType = true;
        private bool fk = true;
        private int bangbang = 0;

        public frmLayerManager frmlar = new frmLayerManager();
        public string yearID = "";

        private string SVGUID = "";
        private string SelUseArea = "";
        private string LineLen = "";
        private string rzb = "1";
        private string selLar = "";
        private int LayerCount = 0;
        private string str_power = "";
        public int il = 0;
        private bool LoadImage = true;
        public bool SubPrint = false;
        string str_selID = "";

        ArrayList ChangeLayerList = new ArrayList();
        XmlNode img = null;

        string oldsid = "";
        public event OnCloseDocumenthandler OnCloseSvgDocument;

        #endregion
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        public NewfrmTLWangjia()
        {


            //Itop.Client.MIS.GetProgRight("4a535c01-3b40-4323-9a6e-f2cae00358cf", "admin");
            object ert;

            //ert=System.Configuration.ConfigurationSettings.GetConfig("lastLoginUserNumber");
            this.propertyGrid = new PropertyGrid();
            tip = new DevComponents.DotNetBar.ToolTip();
            ItopVector.SpecialCursors.LoadCursors();
            InitializeComponent();
            tlVectorControl1.CanEdit = true;
            //tlVectorControl1.DrawArea.FreeSelect = true;

            this.dotNetBarManager1.Images = ItopVector.Resource.ResourceHelper.LoadBitmapStrip(base.GetType(), "Itop.TLPsp.ToolbarImages1.bmp", new Size(16, 16), new System.Drawing.Point(0, 0));


            Pen pen1 = new Pen(Brushes.Cyan, 3);
            tlVectorControl1.TempPen = pen1;
            //tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.BackColor = Color.White;
            tlVectorControl1.OperationChanged += new EventHandler(tlVectorControl1_OperationChanged);
            tlVectorControl1.FullDrawMode = true;
            tlVectorControl1.DrawArea.ViewMargin = new Size(5000, 5000);
            tlVectorControl1.DrawMode = DrawModeType.MemoryImage;
            tlVectorControl1.DrawArea.OnAddElement += new AddSvgElementEventHandler(tlVectorControl1_AddElement);
            tlVectorControl1.DrawArea.ViewChanged += new ItopVector.DrawArea.ViewChangedEventHandler(DrawArea_ViewChanged);
            tlVectorControl1.ScaleChanged += new EventHandler(tlVectorControl1_ScaleChanged);
            tlVectorControl1.CurrentOperation = ToolOperation.Select;
            tlVectorControl1.LeftClick += new SvgElementEventHandler(tlVectorControl1_LeftClick);
            tlVectorControl1.RightClick += new SvgElementEventHandler(tlVectorControl1_RightClick);
            tlVectorControl1.DoubleLeftClick += new SvgElementEventHandler(tlVectorControl1_DoubleLeftClick);
            //tlVectorControl1.DrawArea.OnMouseMove += new MouseEventHandler(DrawArea_OnMouseMove);
            tlVectorControl1.DrawArea.OnMouseDown += new MouseEventHandler(DrawArea_OnMouseDown);
            tlVectorControl1.DocumentChanged += new OnDocumentChangedEventHandler(tlVectorControl1_DocumentChanged);
            tlVectorControl1.DrawArea.OnElementMove += new ElementMoveEventHandler(DrawArea_OnElementMove);
            tlVectorControl1.DrawArea.OnMouseMove += new MouseEventHandler(DrawArea_OnMouseMove);

            oldsid = ConfigurationSettings.AppSettings.Get("SvgID");
        }

        public void frmshow()
        {

            this.Text = "网架优化";

            //ctrlSvgView1.comboSel.Visible = false;
            this.Show();
        }
        void DrawArea_OnMouseMove(object sender, MouseEventArgs e)
        {
            //Topology2();
            if (tlVectorControl1.Operation == ToolOperation.PolyLine)
            {
                Topology2();
                XmlNodeList motherLineCollection = tlVectorControl1.SVGDocument.GetElementsByTagName("use");
                foreach (ISvgElement element in motherLineCollection)
                {
                    if ((element as XmlElement).GetAttribute("xlink:href").Contains("motherlinenode"))
                    {
                        RectangleF t = ((IGraph)element).GetBounds();
                        Point pt = this.tlVectorControl1.PointToView(new Point(e.X, e.Y));
                        Point mt = this.tlVectorControl1.PointToScreen(new Point(e.X, e.Y));
                        if (!t.Contains(pt.X, pt.Y))
                        {
                            RectangleF temp = new RectangleF();
                            temp = t;
                            temp.X = t.X - 5;
                            temp.Y = t.Y - 5;
                            temp.Height = t.Height + 10;
                            temp.Width = t.Width + 10;
                            if (temp.Contains(pt.X, pt.Y))
                            {
                                Point ad = new Point(0, 0);
                                if ((pt.X < t.X) || (pt.X > (t.X + t.Width)))
                                {
                                    ad.X = (int)(pt.X - t.X - t.Width / 2);
                                }
                                if ((pt.Y < t.Y) || (pt.Y > (t.Y + t.Height)))
                                {
                                    ad.Y = (int)(pt.Y - t.Y - t.Height / 2);
                                }
                                //Point ads = this.tlVectorControl1.PointToScreen(new Point(ad.X, ad.Y));
                                SetCursorPos(mt.X - ad.X, mt.Y - ad.Y);
                                Thread.Sleep(15);
                            }
                        }
                    }
                }
            }
        }


        void tlVectorControl1_DocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            string larid = tlVectorControl1.SVGDocument.CurrentLayer.ID;

            if (!ChangeLayerList.Contains(larid))
            {
                ChangeLayerList.Add(larid);
            }
            if (tlVectorControl1.Operation == ToolOperation.InterEnclosurePrint)
            {
                tlVectorControl1.Operation = ToolOperation.Select;
            }
        }



        private void RenderTo(Graphics g)
        {
            SvgDocument svgdoc = tlVectorControl1.SVGDocument;
            Matrix matrix1 = new Matrix();
            Matrix matrix2 = new Matrix();
            matrix1 = ((SVG)svgdoc.RootElement).GraphTransform.Matrix;
            matrix2.Multiply(matrix1);
            matrix1.Reset();
            matrix1.Multiply(g.Transform);
            g.ResetTransform();
            try
            {

                SVG svg1 = svgdoc.DocumentElement as SVG;
                svgdoc.BeginPrint = true;
                SmoothingMode mode1 = svgdoc.SmoothingMode;
                svgdoc.SmoothingMode = g.SmoothingMode;
                svg1.Draw(g, svgdoc.ControlTime);
                svgdoc.SmoothingMode = mode1;
                svgdoc.BeginPrint = false;
            }
            finally
            {
                g.Transform = matrix1.Clone();
                matrix1.Reset();
                matrix1.Multiply(matrix2);
            }
        }
        public void jxtbar2(int jxt)
        {
#if Debug || Release
            dotNetBarManager1.Bars["mainmenu"].GetItem("mjxt").Visible = true;
#endif
            if (jxt == 1)
            {
                //dotNetBarManager1.Bars["bar2"].GetItem("PowerLoss").Visible = true;
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCal").Visible = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").ShowSubItems = true;
                dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;

                //Refresh();
            }
            else if (jxt == 2)
            {
                try
                {
                    dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = false;
                }
                catch { }
                //dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCal").Visible = false;
                //dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").ShowSubItems = true;
                dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                //Refresh();
            }
            else
            {
                try
                {
                    dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = false;
                }
                catch { }
                //dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCal").Visible = false;
                //dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").ShowSubItems = true;
                //dotNetBarManager1.Bars["bar2"].GetItem("PowerLoss").Visible = true;
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCal").Visible = false;
            }

        }
        public void jxtbar(int jxt)
        {
#if Debug || Release
            dotNetBarManager1.Bars["mainmenu"].GetItem("mjxt").Visible = true;
#endif
            if (jxt == 1)
            {
                dotNetBarManager1.Bars["mainmenu"].GetItem("mConvert").Enabled = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem9").Visible = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("mjxt").Enabled = true;
                dotNetBarManager1.Bars["bar2"].GetItem("mDLR").Enabled = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").Enabled = true;
                dotNetBarManager1.Bars["bar2"].GetItem("powerFactor").Enabled = true;
                dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Enabled = true;
                dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Enabled = true;
                //dotNetBarManager1.Bars["bar2"].GetItem("PowerLoss").Enabled = true;
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCal").Enabled = true;
                dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").ShowSubItems = true;
                dotNetBarManager1.Bars["bar2"].GetItem("mConnectLine").Visible = false;

                //Refresh();
            }
            else
            {
                dotNetBarManager1.Bars["mainmenu"].GetItem("mConvert").Enabled = true;
                dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem9").Visible = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("mjxt").Visible = true;
                dotNetBarManager1.Bars["bar2"].GetItem("mDLR").Enabled = true;
                dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("powerFactor").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Enabled = false;
                //dotNetBarManager1.Bars["bar2"].GetItem("PowerLoss").Enabled = true;
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCal").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mConnectLine").Visible = true;
                //Refresh();
            }

            //if (jxt == 1)
            //{
            //    dotNetBarManager1.Bars["mainmenu"].GetItem("mConvert").Visible = false;
            //    dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem9").Visible = false;

            //    dotNetBarManager1.Bars["bar2"].GetItem("mDLR").Visible = false;
            //    dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").Visible = true;
            //    dotNetBarManager1.Bars["bar2"].GetItem("powerFactor").Visible = true;
            //    dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = true;
            //    dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = true;
            //    dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").ShowSubItems = true;
            //    //Refresh();
            //}
            //else
            //{
            //    dotNetBarManager1.Bars["mainmenu"].GetItem("mConvert").Visible = true;
            //    dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem9").Visible = true;

            //    dotNetBarManager1.Bars["bar2"].GetItem("mDLR").Visible = true;
            //    dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").Visible = false;
            //    dotNetBarManager1.Bars["bar2"].GetItem("powerFactor").Visible = false;
            //    dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = false;
            //    dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
            //    //Refresh();
            //}
        }
        private void setTJhide()
        {
            SvgElementCollection sc = (tlVectorControl1.SVGDocument.RootElement as SVG).ChildList;
            foreach (SvgElement se in sc)
            {
                try
                {
                    (se as IGraph).DrawVisible = se.GetAttribute("print") != "no";
                }
                catch { }
            }
        }
        private void setTJshow()
        {
            SvgElementCollection sc = (tlVectorControl1.SVGDocument.RootElement as SVG).ChildList;
            foreach (SvgElement se in sc)
            {
                try
                {
                    (se as IGraph).DrawVisible = true;
                }
                catch { }
            }
        }
        /// <summary>
        /// 导出区域图片
        /// </summary>
        private void ExportImage()
        {
            GraphPath rt1 = tlVectorControl1.SVGDocument.CurrentElement as GraphPath;
            //GraphPath rt1 = tlVectorControl1.SVGDocument.DocumentElement as GraphPath;
            //RectangleF rtf1 = new RectangleF(0,-100,5000,5000);
            if (rt1 == null) return;
            RectangleF rtf1 = rt1.GetBounds();
            //int width = (int)Math.Round(tlVectorControl1.DocumentSize.Height * tlVectorControl1.ScaleRatio,0);
            //int height = (int)Math.Round(tlVectorControl1.DocumentSize.Width * tlVectorControl1.ScaleRatio,0);      
            int width = (int)Math.Round(rtf1.Width * tlVectorControl1.ScaleRatio, 0);
            int height = (int)Math.Round(rtf1.Height * tlVectorControl1.ScaleRatio, 0);
            System.Drawing.Image image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            Color color = ColorTranslator.FromHtml("#EBEAE8");
            //image.SetColorKey(color, color);

            g.Clear(Color.White);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            Matrix matrix1 = new Matrix();
            matrix1.Scale(tlVectorControl1.ScaleRatio, tlVectorControl1.ScaleRatio);
            matrix1.Translate(-rtf1.X, -rtf1.Y);
            g.Transform = matrix1;
            setTJhide();//屏蔽T接点
            RenderTo(g);
            setTJshow();
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.RestoreDirectory = true;
            dlg.Filter = "图像文件(*.png)|*.png|图像文件(*.jpg)|*.jpg|图像文件(*.gif)|*.gif|Bitmap文件(*.bmp)|*.bmp|Jpeg文件(*.jpeg)|*.jpeg|所有文件(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string str = Path.GetExtension(dlg.FileName);
                str = str.Substring(1);
                if (string.IsNullOrEmpty(str)) str = "Bmp";
                ImageFormat iformat = ImageFormat.Bmp;
                switch (str.ToLower())
                {
                    case "bmp":
                        iformat = ImageFormat.Bmp;
                        break;
                    case "jpeg":
                    case "jpg":
                        iformat = ImageFormat.Jpeg;
                        break;
                    case "png":
                        iformat = ImageFormat.Png;
                        break;
                    case "gif":
                        iformat = ImageFormat.Gif;
                        break;
                }
                image.Save(dlg.FileName, iformat);
                image.Dispose();
            }
        }
        void DrawArea_OnElementMove(object sender, MoveEventArgs e)
        {
            SvgElementCollection list = tlVectorControl1.SVGDocument.SelectCollection;
            ISvgElement element = e.SvgElement;
            PointF beforeMove = e.BeforeMove;
            PointF afterMove = e.AfterMove;
            XmlNodeList listFirstNode = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@FirstNode='" + element.ID + "']");
            RectangleF t = ((IGraph)element).GetBounds();
            PointF[] ptt = new PointF[] { beforeMove, afterMove };
            Transf tran = (element as Graph).Transform;
            tran.Matrix.TransformPoints(ptt);
            beforeMove = ptt[0];
            afterMove = ptt[1];
            foreach (XmlNode node in listFirstNode)
            {
                if (list.Contains((ISvgElement)(node as XmlElement)))
                {
                    continue;
                }
                PointF[] first = (node as Polyline).Points;
                XmlElement line = node as XmlElement;
                PointF pt1 = new PointF(first[0].X, first[0].Y);
                first[0].X = first[0].X + afterMove.X - beforeMove.X;
                first[0].Y = first[0].Y + afterMove.Y - beforeMove.Y;
                string temp = null;
                (node as Polyline).GPath.Reset();
                (node as Polyline).GPath.AddLines(first);
                foreach (PointF pt in first)
                {
                    if (temp == null)
                    {
                        temp += pt.X + " " + pt.Y;
                    }
                    else
                    {
                        temp += "," + pt.X + " " + pt.Y;
                    }
                }
                if (first[0] != pt1)
                {
                    (node as XmlElement).SetAttribute("points", temp);
                }
            }

            XmlNodeList listLastNode = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@LastNode='" + element.ID + "']");
            foreach (XmlNode node in listLastNode)
            {
                if (list.Contains((ISvgElement)(node as XmlElement)))
                {
                    continue;
                }
                PointF[] first = (node as Polyline).Points;
                XmlElement line = node as XmlElement;
                PointF pt1 = new PointF(first[first.Length - 1].X, first[first.Length - 1].Y);
                first[first.Length - 1].X = first[first.Length - 1].X + afterMove.X - beforeMove.X;
                first[first.Length - 1].Y = first[first.Length - 1].Y + afterMove.Y - beforeMove.Y;
                string temp = null;
                (node as Polyline).GPath.Reset();
                (node as Polyline).GPath.AddLines(first);
                foreach (PointF pt in first)
                {
                    if (temp == null)
                    {
                        temp += pt.X + " " + pt.Y;
                    }
                    else
                    {
                        temp += "," + pt.X + " " + pt.Y;
                    }
                }
                if (first[first.Length - 1] != pt1)
                {
                    (node as XmlElement).SetAttribute("points", temp);
                }
            }
        }


        private void Topology2()
        {
            PSPDEV pspDev = new PSPDEV();
            XmlNode tempd = null;
            XmlElement templei = tempd as XmlElement;
            XmlNodeList nodeList2 = tlVectorControl1.SVGDocument.GetElementsByTagName("polyline");
            foreach (XmlNode node in nodeList2)
            {

                XmlElement element = node as XmlElement;
                templei = element;

                if ((element.GetAttribute("flag") == "1") || (!element.HasAttributes) || element.GetAttribute("id") == "")
                {
                    break;
                }
                PointF[] t = ((Polyline)element).Pt;
                pspDev.EleID = element.GetAttribute("id");
                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                pspDev.X1 = t[0].X;
                pspDev.Y1 = t[0].Y;
                pspDev.X2 = t[1].X;
                pspDev.Y2 = t[1].Y;
                pspDev.FirstNode = -1;
                pspDev.LastNode = -1;
                pspDev.Number = -1;
                IList list11 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandEleID", pspDev);
                foreach (PSPDEV psp in list11)
                {
                    if (psp.LineStatus == "断开")
                        element.SetAttribute("stroke", "#FF0000");
                    if (psp.LineStatus == "运行")
                        element.SetAttribute("stroke", "#000000");
                    if (psp.LineStatus == "待选")
                        element.SetAttribute("stroke", "#000FFF");
                    if (psp.LineStatus == "等待")
                        element.SetAttribute("stroke", "#00FF00");
                }
                //Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
            }
            //pspDev.Type = "Use";
            //pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            //IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            //for (int i = 1; i <= list1.Count; i++)
            //{
            //    pspDev = (PSPDEV)list1[i - 1];
            //    pspDev.Number = i;
            //    Services.BaseService.Update<PSPDEV>(pspDev);
            //}
            //pspDev.Type = "Polyline";
            //pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            //IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            //int j = 0;
            //for (int i = 1; i <= list2.Count; i++)
            //{
            //    pspDev = (PSPDEV)list2[i - 1];
            //    if (pspDev.LineStatus == "断开")
            //    {
            //        //templei.SetAttribute("stroke", "#FF0000");
            //        j += 1;
            //        pspDev.Number = -1;
            //        Services.BaseService.Update<PSPDEV>(pspDev);
            //        continue;
            //    }
            //    pspDev.Number = (i - j);
            //    Services.BaseService.Update<PSPDEV>(pspDev);
            //}
            //pspDev.Type = "Use";
            //pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            //list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            //pspDev.Type = "Polyline";
            //pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            //list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            //foreach (PSPDEV dev in list1)
            //{
            //    double devx = Convert.ToDouble(dev.X1);
            //    double devy = Convert.ToDouble(dev.Y1);
            //    XmlElement temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + dev.EleID + "']") as XmlElement;
            //    if (temp.GetAttribute("xlink:href").Contains("Substation"))
            //    {
            //        RectangleF t = ((IGraph)temp).GetBounds();
            //        foreach (PSPDEV psp in list2)
            //        {
            //            //if (psp.LineStatus== "断开")
            //                //temp.SetAttribute("stroke", "#FF0000");

            //            double x1 = psp.X1;
            //            double x2 = psp.X2;
            //            double y1 = psp.Y1;
            //            double y2 = psp.Y2;
            //            if (Math.Abs(devx - x1) < ((t.Height) / 2) && Math.Abs(devy - y1) < ((t.Height) / 2))
            //            {
            //                psp.FirstNode = dev.Number;
            //                Services.BaseService.Update<PSPDEV>(psp);
            //            }
            //            if (Math.Abs(devx - x2) < ((t.Height) / 2) && Math.Abs(devy - y2) < ((t.Height) / 2))
            //            {
            //                psp.LastNode = dev.Number;
            //                Services.BaseService.Update<PSPDEV>(psp);
            //            }
            //        }
            //    }
            //}
        }
        void DrawArea_OnMouseDown(object sender, MouseEventArgs e)
        {
            //Topology2();
            XmlNodeList elementCollection = tlVectorControl1.SVGDocument.GetElementsByTagName("use");
            if (elementCollection.Count > 0)
            {
                foreach (ISvgElement element in elementCollection)
                {
                    if ((element as XmlElement) is Use)
                    {
                        RectangleF t = ((IGraph)element).GetBounds();

                        PointF uset = new PointF((float)(t.X + t.Width / 2), (float)(t.Y + t.Height / 2));
                        XmlNodeList linea = tlVectorControl1.SVGDocument.GetElementsByTagName("polyline");
                        foreach (XmlNode pol in linea)
                        {

                            PointF[] tt = ((Polyline)(pol as XmlElement)).Pt;
                            double x1 = tt[0].X;
                            double x2 = tt[1].X;
                            double y1 = tt[0].Y;
                            double y2 = tt[1].Y;

                            if ((element as XmlElement).GetAttribute("xlink:href").Contains("Substation"))
                            {
                                //(element as XmlElement).SetAttribute("stroke", "#FF0000");
                                if (Math.Abs(uset.X - x1) < ((t.Height) / 2) && Math.Abs(uset.Y - y1) < ((t.Height) / 2))
                                {

                                    (pol as XmlElement).SetAttribute("FirstNode", element.ID);

                                }
                                else
                                {
                                    if ((pol as XmlElement).GetAttribute("FirstNode") == element.ID)
                                    {
                                        (pol as XmlElement).RemoveAttribute("FirstNode");
                                    }
                                }
                                if (Math.Abs(uset.X - x2) < ((t.Height) / 2) && Math.Abs(uset.Y - y2) < ((t.Height) / 2))
                                {

                                    (pol as XmlElement).SetAttribute("LastNode", element.ID);

                                }
                                else
                                {
                                    if ((pol as XmlElement).GetAttribute("LastNode") == element.ID)
                                    {
                                        (pol as XmlElement).RemoveAttribute("LastNode");
                                    }
                                }
                            }
                            else if ((element as XmlElement).GetAttribute("xlink:href").Contains("Power") || (element as XmlElement).GetAttribute("xlink:href").Contains("motherlinenode"))
                            {
                                if ((x1 - t.X) < t.Width && (y1 - t.Y) < t.Height && x1 > t.X && y1 > t.Y)
                                {

                                    (pol as XmlElement).SetAttribute("FirstNode", element.ID);

                                }
                                else
                                {
                                    if ((pol as XmlElement).GetAttribute("FirstNode") == element.ID)
                                    {
                                        (pol as XmlElement).RemoveAttribute("FirstNode");
                                    }
                                }
                                if ((x2 - t.X) < t.Width && (y2 - t.Y) < t.Height && x2 > t.X && y2 > t.Y)
                                {

                                    (pol as XmlElement).SetAttribute("LastNode", element.ID);

                                }
                                else
                                {
                                    if ((pol as XmlElement).GetAttribute("LastNode") == element.ID)
                                    {
                                        (pol as XmlElement).RemoveAttribute("LastNode");
                                    }
                                }
                            }

                        }

                    }
                }

            }
        }
        Thread oThread;
        System.Threading.Timer time;
        //private void Form_Load(object sender, EventArgs e)
        //{
        //    t = new Thread(new ThreadStart(start));
        //    t.Start();

        //    try
        //    {
        //        //TimerCallback是一个委托类型,第三个参数是开始计时,每四参数是间隔长(以ms为单位). 
        //        time = new System.Threading.Timer(new TimerCallback(method), null, 0, 40);

        //    }
        //    catch { }
        //}
        void method(object o) //注意参数. 
        {
            oThread.Abort();

            while (!oThread.IsAlive)
            {//终止成功. 
                //终止计时器 
                time.Change(Timeout.Infinite, Timeout.Infinite);
                //MessageBox.Show("数据不收敛，计算超时");
                bangbang = 1;


                Thread.Sleep(10000);
                oThread = null;

                //workerObject.RequestStop(); 
            }//终止线程. 
        }

        void start()
        {
            MessageBox.Show("start");
        }
        //void DrawArea_OnMouseMove(object sender, MouseEventArgs e)
        //{


        //}    


        void tlVectorControl1_DoubleLeftClick(object sender, SvgElementSelectedEventArgs e)
        {
            if (!Check())
            {
                return;
            }
            XmlElement element = tlVectorControl1.SVGDocument.CurrentElement;
            if (element is Use)
            {
                if (element.GetAttribute("xlink:href").Contains("Substation") || element.GetAttribute("xlink:href").Contains("motherlinenode"))
                {

                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                    frmSubstation dlg;
                    if (pspDev != null)
                    {
                        dlg = new frmSubstation(pspDev);
                    }
                    else
                    {
                        pspDev = new PSPDEV();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = -1;
                        pspDev.Type = "Use";
                        if (element.GetAttribute("xlink:href").Contains("Substation"))
                        {
                            pspDev.Lable = "变电站";
                        }
                        else if (element.GetAttribute("xlink:href").Contains("motherlinenode"))
                        {
                            pspDev.Lable = "母线节点";
                        }
                        else if (element.GetAttribute("xlink:href").Contains("Power"))
                        {
                            pspDev.Lable = "电厂";
                        }
                        //Services.BaseService.Create<PSPDEV>(pspDev);
                        dlg = new frmSubstation(pspDev);
                    }
                    dlg.TYear = tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("year");
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspName.Type = "Use";
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                        if (text != null)
                        {
                            (text as Text).InnerText = dlg.Name;
                        }
                        PSPDEV powerfactor = new PSPDEV();
                        powerfactor.Type = "Power";
                        powerfactor.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        powerfactor = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", powerfactor);
                        pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                        pspDev.Burthen = Convert.ToDecimal(dlg.Burthen);

                        //if (powerfactor!=null && (Convert.ToDecimal(dlg.Change)==2))
                        //pspDev.InPutP = Convert.ToDouble(dlg.Burthen) * powerfactor.BigP;
                        //if (Convert.ToDecimal(dlg.Change)==1)
                        pspDev.InPutP = Convert.ToDouble(dlg.InPutP);
                        pspDev.InPutQ = Convert.ToDouble(dlg.InPutQ);
                        pspDev.OutP = Convert.ToDouble(dlg.OutP);
                        pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                        pspDev.ReferenceVolt = Convert.ToDouble(dlg.ReferenceVolt);

                        if (dlg.NodeType == "是")
                        {
                            pspDev.NodeType = "0";
                        }
                        else
                        {
                            pspDev.NodeType = "1";
                        }
                        Services.BaseService.Update<PSPDEV>(pspDev);
                    }
                }
                else if (element.GetAttribute("xlink:href").Contains("Power"))
                {
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                    frmSubstation dlg;
                    if (pspDev != null)
                    {
                        dlg = new frmSubstation(pspDev);
                    }
                    else
                    {
                        pspDev = new PSPDEV();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = -1;
                        pspDev.Type = "Use";
                        if (element.GetAttribute("xlink:href").Contains("Substation"))
                        {
                            pspDev.Lable = "变电站";
                        }
                        else if (element.GetAttribute("xlink:href").Contains("motherlinenode"))
                        {
                            pspDev.Lable = "母线节点";
                        }
                        else if (element.GetAttribute("xlink:href").Contains("Power"))
                        {
                            pspDev.Lable = "电厂";
                        }
                        Services.BaseService.Create<PSPDEV>(pspDev);
                        dlg = new frmSubstation(pspDev);
                        dlg.TYear = tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("year");
                    }

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspName.Type = "Use";
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                        if (text != null)
                        {
                            (text as Text).InnerText = dlg.Name;
                        }
                        pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                        pspDev.Burthen = Convert.ToDecimal(dlg.Burthen);
                        pspDev.OutP = Convert.ToDouble(dlg.OutP);
                        pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                        pspDev.InPutP = Convert.ToDouble(dlg.InPutP);
                        pspDev.InPutQ = Convert.ToDouble(dlg.InPutQ);
                        pspDev.ReferenceVolt = Convert.ToDouble(dlg.ReferenceVolt);

                        if (dlg.NodeType == "是")
                        {
                            pspDev.NodeType = "0";
                        }
                        else
                        {
                            pspDev.NodeType = "2";
                        }
                        Services.BaseService.Update<PSPDEV>(pspDev);
                    }
                }
                else if (element.GetAttribute("xlink:href").Contains("dynamotorline"))
                {
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                    frmFadejie dlg;
                    if (pspDev != null)
                    {
                        dlg = new frmFadejie(pspDev, pspDev.SvgUID);
                    }
                    else
                    {
                        pspDev = new PSPDEV();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = 0;
                        pspDev.Type = "dynamotorline";
                        if (element.GetAttribute("xlink:href").Contains("dynamotorline"))
                        {
                            pspDev.Lable = "发电厂支路";
                        }
                        else if (element.GetAttribute("xlink:href").Contains("gndline"))
                        {
                            pspDev.Lable = "接地支路";
                        }
                        Services.BaseService.Create<PSPDEV>(pspDev);
                        dlg = new frmFadejie(pspDev, pspDev.SvgUID);
                    }
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.Type = "dynamotorline";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                        if (text != null)
                        {
                            (text as Text).InnerText = dlg.Name;
                        }
                        pspDev.HuganLine1 = dlg.FirstNodeName;
                        pspDev.HuganLine3 = dlg.SwitchStatus;
                        if (dlg.OutP != "")
                            pspDev.OutP = Convert.ToDouble(dlg.OutP);
                        if (dlg.OutQ != "")
                            pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                        if (dlg.VoltR != "")
                            pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                        if (dlg.VoltV != "")
                            pspDev.VoltV = Convert.ToDouble(dlg.VoltV);
                        if (dlg.PositiveTQ != "")
                            pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                        if (dlg.NegativeTQ != "")
                            pspDev.ZeroTQ = Convert.ToDouble(dlg.NegativeTQ);
                        Services.BaseService.Update<PSPDEV>(pspDev);
                    }
                }
                else if (element.GetAttribute("xlink:href").Contains("gndline"))
                {
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                    frmFadejie dlg;
                    if (pspDev != null)
                    {
                        dlg = new frmFadejie(pspDev, pspDev.SvgUID);
                    }
                    else
                    {
                        pspDev = new PSPDEV();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = 0;
                        pspDev.Type = "gndline";
                        if (element.GetAttribute("xlink:href").Contains("dynamotorline"))
                        {
                            pspDev.Lable = "发电厂支路";
                        }
                        else if (element.GetAttribute("xlink:href").Contains("gndline"))
                        {
                            pspDev.Lable = "接地支路";
                        }
                        Services.BaseService.Create<PSPDEV>(pspDev);
                        dlg = new frmFadejie(pspDev, pspDev.SvgUID);
                    }
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.Type = "gndline";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                        if (text != null)
                        {
                            (text as Text).InnerText = dlg.Name;
                        }
                        pspDev.HuganLine1 = dlg.FirstNodeName;
                        pspDev.HuganLine3 = dlg.SwitchStatus;
                        if (dlg.OutP != "")
                            pspDev.OutP = Convert.ToDouble(dlg.OutP);
                        if (dlg.OutQ != "")
                            pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                        if (dlg.VoltR != "")
                            pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                        if (dlg.VoltV != "")
                            pspDev.VoltV = Convert.ToDouble(dlg.VoltV);
                        if (dlg.PositiveTQ != "")
                            pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                        if (dlg.NegativeTQ != "")
                            pspDev.ZeroTQ = Convert.ToDouble(dlg.NegativeTQ);
                        Services.BaseService.Update<PSPDEV>(pspDev);
                    }
                }
                else if (element.GetAttribute("xlink:href").Contains("loadline"))
                {
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                    frmLoad dlg;
                    if (pspDev != null)
                    {
                        dlg = new frmLoad(pspDev);
                    }
                    else
                    {
                        pspDev = new PSPDEV();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = 0;
                        pspDev.Type = "loadline";

                        pspDev.Lable = "负荷支路";

                        Services.BaseService.Create<PSPDEV>(pspDev);
                        dlg = new frmLoad(pspDev);
                    }
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.Type = "loadline";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                        if (text != null)
                        {
                            (text as Text).InnerText = dlg.Name;
                        }
                        pspDev.HuganLine1 = dlg.FirstNodeName;

                        pspDev.HuganLine3 = dlg.LoadSwitchState;
                        if (dlg.InPutP != "")
                            pspDev.InPutP = Convert.ToDouble(dlg.InPutP);
                        if (dlg.InPutQ != "")
                            pspDev.InPutQ = Convert.ToDouble(dlg.InPutQ);
                        if (dlg.VoltR != "")
                            pspDev.VoltR = Convert.ToDouble(dlg.VoltR);

                        Services.BaseService.Update<PSPDEV>(pspDev);
                    }
                }
                else if (element.GetAttribute("xlink:href").Contains("串联电容电抗器"))
                {
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                    frmCapacity dlg;


                    if (pspDev != null)
                    {
                        dlg = new frmCapacity(pspDev, pspDev.SvgUID);
                        dlg.SetEnable(true);
                    }
                    else
                    {
                        return;
                    }
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.Type = "串联电容电抗器";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                        if (text != null)
                        {
                            (text as Text).InnerText = dlg.Name;
                        }
                        pspDev.HuganLine1 = dlg.FirstNodeName;

                        // pspDev.HuganLine2 = dlg.LastNodeName;
                        if (dlg.PositiveTQ != "")
                            pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);


                        Services.BaseService.Update<PSPDEV>(pspDev);
                    }
                }
                else if (element.GetAttribute("xlink:href").Contains("并联电容电抗器"))
                {
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                    frmCapacity dlg;


                    if (pspDev != null)
                    {
                        dlg = new frmCapacity(pspDev, pspDev.SvgUID);
                        dlg.SetEnable(false);
                    }
                    else
                    {
                        return;
                    }
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.Type = "并联电容电抗器";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                        if (text != null)
                        {
                            (text as Text).InnerText = dlg.Name;
                        }
                        pspDev.HuganLine1 = dlg.FirstNodeName;
                        // pspDev.HuganLine2 = dlg.LastNodeName;
                        if (dlg.PositiveTQ != "")
                            pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);


                        Services.BaseService.Update<PSPDEV>(pspDev);
                    }
                }
                else if (element.GetAttribute("xlink:href").Contains("transformerthirdzu"))
                {
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                    frmThridTra dlg;

                    if (pspDev != null)
                    {
                        dlg = new frmThridTra(pspDev, pspDev.SvgUID);
                    }
                    else
                    {
                        return;
                    }
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.Type = "transformerthirdzu";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                        if (text != null)
                        {
                            (text as Text).InnerText = dlg.Name;
                        }

                        pspDev.HuganLine1 = dlg.IName;
                        pspDev.HuganLine2 = dlg.JName;
                        pspDev.HuganLine3 = dlg.ISwitchState;
                        pspDev.HuganLine4 = dlg.JSwitchState;
                        pspDev.LineLevel = dlg.IType;
                        pspDev.LineType = dlg.JType;
                        pspDev.LineStatus = dlg.KType;
                        pspDev.KName = dlg.KName;
                        pspDev.KSwitchStatus = dlg.KSwitchState;
                        if (dlg.IK != "")
                        {
                            pspDev.K = Convert.ToDouble(dlg.KK);
                        }
                        if (dlg.JK != "")
                        {
                            pspDev.G = Convert.ToDouble(dlg.JK);
                        }
                        if (dlg.KK != "")
                        {
                            pspDev.BigP = Convert.ToDouble(dlg.KK);
                        }
                        if (dlg.IR != "")
                        {
                            pspDev.HuganTQ1 = Convert.ToDouble(dlg.IR);
                        }
                        if (dlg.JR != "")
                        {
                            pspDev.HuganTQ2 = Convert.ToDouble(dlg.JR);
                        }
                        if (dlg.KR != "")
                        {
                            pspDev.HuganTQ3 = Convert.ToDouble(dlg.KR);
                        }
                        if (dlg.ITQ != "")
                        {
                            pspDev.HuganTQ4 = Convert.ToDouble(dlg.ITQ);
                        }
                        if (dlg.JTQ != "")
                        {
                            pspDev.HuganTQ5 = Convert.ToDouble(dlg.JTQ);
                        }
                        if (dlg.KTQ != "")
                        {
                            pspDev.SmallTQ = Convert.ToDouble(dlg.KTQ);
                        }
                        if (dlg.ZeroTQ != "")
                            pspDev.ZeroTQ = Convert.ToDouble(dlg.ZeroTQ);

                        if (dlg.NeutralNodeTQ != "")
                            pspDev.BigTQ = Convert.ToDouble(dlg.NeutralNodeTQ);
                        Services.BaseService.Update<PSPDEV>(pspDev);
                    }
                }
                else if (element.GetAttribute("xlink:href").Contains("transformertwozu"))
                {
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                    frmTwoTra dlg;

                    if (pspDev != null)
                    {
                        dlg = new frmTwoTra(pspDev, pspDev.SvgUID);
                    }
                    else
                    {
                        return;
                    }
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.Type = "transformertwozu";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                        if (text != null)
                        {
                            (text as Text).InnerText = dlg.Name;
                        }

                        pspDev.HuganLine1 = dlg.FirstName;
                        pspDev.HuganLine2 = dlg.LastName;
                        pspDev.HuganLine3 = dlg.FirstSwitchState;
                        pspDev.HuganLine4 = dlg.LastSwitchState;
                        pspDev.LineLevel = dlg.FirstType;
                        pspDev.LineType = dlg.LastType;

                        if (dlg.PositiveR != "")
                        {
                            pspDev.PositiveR = Convert.ToDouble(dlg.PositiveR);
                        }
                        if (dlg.PositiveTQ != "")
                        {
                            pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                        }

                        if (dlg.ZeroR != "")
                        {
                            pspDev.ZeroR = Convert.ToDouble(dlg.ZeroR);
                        }

                        if (dlg.ZeroTQ != "")
                        {
                            pspDev.ZeroTQ = Convert.ToDouble(dlg.ZeroTQ);
                        }

                        if (dlg.K != "")
                            pspDev.K = Convert.ToDouble(dlg.K);

                        if (dlg.NeutralNodeTQ != "")
                            pspDev.BigTQ = Convert.ToDouble(dlg.NeutralNodeTQ);
                        Services.BaseService.Update<PSPDEV>(pspDev);
                    }
                }
                else if (element.GetAttribute("xlink:href").Contains("1/2母联开关"))
                {
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                    frmMuLian dlg;

                    if (pspDev != null)
                    {
                        dlg = new frmMuLian(pspDev, pspDev.SvgUID);
                    }
                    else
                    {
                        return;
                    }
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.Type = "1/2母联开关";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                        if (text != null)
                        {
                            (text as Text).InnerText = dlg.Name;
                        }

                        pspDev.HuganLine1 = dlg.FirstNodeName;
                        pspDev.HuganLine2 = dlg.LastNodeName;
                        pspDev.HuganLine3 = dlg.SwitchStatus;



                        Services.BaseService.Update<PSPDEV>(pspDev);
                    }
                }
                else if (element.GetAttribute("xlink:href").Contains("2/3母联开关"))
                {
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                    frmMuLian2 dlg;

                    if (pspDev != null)
                    {
                        dlg = new frmMuLian2(pspDev, pspDev.SvgUID);
                    }
                    else
                    {
                        return;
                    }
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.Type = "2/3母联开关";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                        if (text != null)
                        {
                            (text as Text).InnerText = dlg.Name;
                        }

                        pspDev.HuganLine1 = dlg.INodeName;
                        pspDev.HuganLine2 = dlg.JNodeName;
                        pspDev.HuganLine3 = dlg.ILineName;
                        pspDev.HuganLine4 = dlg.JLineName;
                        pspDev.KName = dlg.ILoadName;
                        pspDev.KSwitchStatus = dlg.JLoadName;
                        pspDev.LineLevel = dlg.SwitchStatus1;
                        pspDev.LineType = dlg.SwitchStatus2;
                        pspDev.LineStatus = dlg.SwitchStatus3;


                        Services.BaseService.Update<PSPDEV>(pspDev);
                    }
                }
            }
            else if ((element is Polyline) && element.GetAttribute("flag") != "1" && fileType == true)//潮流下线路
            {

                PSPDEV pspDev = new PSPDEV();
                pspDev.EleID = element.GetAttribute("id");
                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);

                frmLinenew dlg2;
                if (pspDev != null)
                {
                    dlg2 = new frmLinenew(pspDev);
                    dlg2.derefucelineflag = Reducelineflag;
                }
                else
                {
                    pspDev = new PSPDEV();
                    pspDev.SUID = Guid.NewGuid().ToString();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.Number = -1;
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = -1;
                    pspDev.Type = "Polyline";
                    pspDev.Lable = "支路";
                    Services.BaseService.Create<PSPDEV>(pspDev);
                    dlg2 = new frmLinenew(pspDev);
                    dlg2.derefucelineflag = Reducelineflag;
                }
                dlg2.TYear = tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("year");
                dlg2.linevalue = tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("linevalue");   //获得线路投资
                if (dlg2.ShowDialog() == DialogResult.OK)
                {
                    if (dlg2.Name == null)
                    {
                        MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    PSPDEV pspName = new PSPDEV();
                    pspName.Name = dlg2.Name;
                    pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspName.Type = "Polyline";
                    IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                    if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                    {
                        MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    pspDev.Name = dlg2.Name;
                    pspDev.LineLength = Convert.ToDouble(dlg2.LineLength);
                    pspDev.LineR = Convert.ToDouble(dlg2.LineR);
                    pspDev.LineTQ = Convert.ToDouble(dlg2.LineTQ);
                    pspDev.LineGNDC = Convert.ToDouble(dlg2.LineGNDC);
                    pspDev.LineLevel = dlg2.LineLevel;
                    pspDev.LineType = dlg2.LineType;
                    pspDev.LineStatus = dlg2.LineStatus;
                    pspDev.ReferenceVolt = Convert.ToDouble(dlg2.ReferenceVolt);
                    if (dlg2.linevalue != "")
                    {
                        pspDev.BigP = Convert.ToDouble(dlg2.linevalue);
                    }
                    WireCategory wirewire = new WireCategory();
                    wirewire.WireType = dlg2.LineType;
                    WireCategory wirewire2 = new WireCategory();
                    wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKey", wirewire);
                    //if (pspDev.LineR == 0)
                    //    pspDev.LineR = Convert.ToDouble(dlg2.LineLength)*wirewire2.WireR ;
                    //if (pspDev.LineTQ == 0)
                    //    pspDev.LineTQ = Convert.ToDouble(dlg2.LineLength) * wirewire2.WireTQ;
                    //if (pspDev.LineGNDC == 0)
                    //    pspDev.LineGNDC = Convert.ToDouble(dlg2.LineLength) * wirewire2.WireGNDC;
                    if (wirewire2 != null)
                        pspDev.LineChange =(double) wirewire2.WireChange;
                    string tempp = dlg2.LineLev;
                    int tel = tempp.Length;
                    if (tempp.Contains("kV") || tempp.Contains("KV") || tempp.Contains("kv") || tempp.Contains("Kv"))
                    {
                        tempp = tempp.Substring(0, tel - 2);
                    }  
                    pspDev.VoltR = Convert.ToDouble(tempp);
                    tlVectorControl1.SVGDocument.CurrentElement.SetAttribute("year", dlg2.TYear);
                    tlVectorControl1.SVGDocument.CurrentElement.SetAttribute("linevalue", dlg2.linevalue);   //获得线路投资
                    //switch (dlg2.LineType)
                    //{
                    //    case "2*LGJ-400":
                    //        {
                    //            if (pspDev.LineR==0)
                    //            pspDev.LineR = Convert.ToDouble(dlg2.LineLength) * 0.04;
                    //            if (pspDev.LineTQ == 0)
                    //            pspDev.LineTQ = Convert.ToDouble(dlg2.LineLength) * 0.303;
                    //            if (pspDev.LineGNDC == 0)
                    //            pspDev.LineGNDC = Convert.ToDouble(dlg2.LineLength) * 17.9;
                    //            pspDev.LineChange = 1690;
                    //        } break;
                    //    case "2*LGJ-300":
                    //        {
                    //            if (pspDev.LineR == 0)
                    //            pspDev.LineR = Convert.ToDouble(dlg2.LineLength) * 0.054;
                    //            if (pspDev.LineTQ == 0)
                    //            pspDev.LineTQ = Convert.ToDouble(dlg2.LineLength) * 0.308;
                    //            if (pspDev.LineGNDC == 0)
                    //            pspDev.LineGNDC = Convert.ToDouble(dlg2.LineLength) * 17.7;
                    //            pspDev.LineChange = 1400;
                    //        } break;
                    //    case "2*LGJ-240":
                    //        {
                    //            if (pspDev.LineR == 0)
                    //            pspDev.LineR = Convert.ToDouble(dlg2.LineLength) * 0.066;
                    //            if (pspDev.LineTQ == 0)
                    //            pspDev.LineTQ = Convert.ToDouble(dlg2.LineLength) * 0.310;
                    //            if (pspDev.LineGNDC == 0)
                    //            pspDev.LineGNDC = Convert.ToDouble(dlg2.LineLength) * 17.5;
                    //            pspDev.LineChange = 1220;
                    //        } break;
                    //    case "LGJ-400":
                    //        {
                    //            if (pspDev.LineR == 0)
                    //            pspDev.LineR = Convert.ToDouble(dlg2.LineLength) * 0.08;
                    //            if (pspDev.LineTQ == 0)
                    //            pspDev.LineTQ = Convert.ToDouble(dlg2.LineLength) * 0.417;
                    //            if (pspDev.LineGNDC == 0)
                    //            pspDev.LineGNDC = Convert.ToDouble(dlg2.LineLength) * 13.2;
                    //            pspDev.LineChange = 845;
                    //        } break;


                    //}
                    Services.BaseService.Update<PSPDEV>(pspDev);
                    Topology2();
                }
            }
            else if ((element is Polyline) && element.GetAttribute("flag") != "1" && fileType != true)//短路下线路
            {

                PSPDEV pspDev = new PSPDEV();
                pspDev.EleID = element.GetAttribute("id");
                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);

                frmLine dlg;
                if (pspDev != null)
                {
                    dlg = new frmLine(pspDev);
                }
                else
                {
                    pspDev = new PSPDEV();
                    pspDev.SUID = Guid.NewGuid().ToString();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.Number = -1;
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = -1;
                    pspDev.Type = "Polyline";
                    pspDev.Lable = "支路";
                    Services.BaseService.Create<PSPDEV>(pspDev);
                    dlg = new frmLine(pspDev);
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.Name == null)
                    {
                        MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    PSPDEV pspName = new PSPDEV();
                    pspName.Name = dlg.Name;
                    pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspName.Type = "Polyline";
                    IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                    if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                    {
                        MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    pspDev.Name = dlg.Name;
                    pspDev.LineLength = Convert.ToDouble(dlg.LineLength);
                    pspDev.LineLevel = dlg.LineLevel;
                    pspDev.LineType = dlg.LineType;
                    pspDev.LineStatus = dlg.LineStatus;
                    pspDev.PositiveR = Convert.ToDouble(dlg.PositiveR);
                    pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                    pspDev.ZeroR = Convert.ToDouble(dlg.ZeroR);
                    pspDev.ZeroTQ = Convert.ToDouble(dlg.ZeroTQ);
                    if (dlg.HuganFirst == "是")
                        pspDev.HuganFirst = 1;
                    else
                        pspDev.HuganFirst = 0;
                    pspDev.HuganLine1 = dlg.HuganLine1;
                    pspDev.HuganLine2 = dlg.HuganLine2;
                    pspDev.HuganLine3 = dlg.HuganLine3;
                    pspDev.HuganLine4 = dlg.HuganLine4;
                    pspDev.HuganTQ1 = Convert.ToDouble(dlg.HuganTQ1);
                    pspDev.HuganTQ2 = Convert.ToDouble(dlg.HuganTQ2);
                    pspDev.HuganTQ3 = Convert.ToDouble(dlg.HuganTQ3);
                    pspDev.HuganTQ4 = Convert.ToDouble(dlg.HuganTQ4);
                    pspDev.HuganTQ5 = Convert.ToDouble(dlg.HuganTQ5);

                    string tempp = dlg.LineLev;
                    int tel = tempp.Length;
                    if (tel == 1)
                        pspDev.VoltR = 0;
                    else
                    {
                        if (tempp.Contains("kV") || tempp.Contains("KV") || tempp.Contains("kv") || tempp.Contains("Kv"))
                        {
                            tempp = tempp.Substring(0, tel - 2);
                        }  
                        pspDev.VoltR = Convert.ToDouble(tempp);
                    }
                    //switch (dlg.LineType)
                    //{
                    //    case "2*LGJ-400":
                    //        {
                    //            pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.04;
                    //            pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.303;
                    //            pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.9;
                    //            pspDev.LineChange = 1690;
                    //        } break;
                    //    case "2*LGJ-300":
                    //        {
                    //            pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.054;
                    //            pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.308;
                    //            pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.7;
                    //            pspDev.LineChange = 1400;
                    //        } break;
                    //    case "2*LGJ-240":
                    //        {
                    //            pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.066;
                    //            pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.310;
                    //            pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.5;
                    //            pspDev.LineChange = 1220;
                    //        } break;
                    //    case "LGJ-400":
                    //        {
                    //            pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.08;
                    //            pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.417;
                    //            pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 13.2;
                    //            pspDev.LineChange = 845;
                    //        } break;


                    //}
                    Services.BaseService.Update<PSPDEV>(pspDev);
                    Topology2();
                }
            }
        }



        void tlVectorControl1_RightClick(object sender, SvgElementSelectedEventArgs e)
        {
            
            if ((tlVectorControl1.SVGDocument.CurrentElement is Use || tlVectorControl1.SVGDocument.CurrentElement is Polyline) && tlVectorControl1.Operation != ToolOperation.PolyLine)
            {
                contextMenuStrip1.Show();
                //if (fileType == true)
                //{
                //    moveMenuItem.Enabled = false;
                //}
                //else
                //{
                //    moveMenuItem.Enabled = true;
                //}

                if (fileType == true)
                {
                    printToolStripMenuItem.Visible = false;
                    moveMenuItem.Visible = false;
                }
                else
                {
                    printToolStripMenuItem.Visible = false;
                    moveMenuItem.Visible = true;
                }
                //if (tlVectorControl1.SVGDocument.CurrentElement
            }
            else
            {
                //contextMenuStrip1.Hide();
                if ((tlVectorControl1.SVGDocument.CurrentElement is RectangleElement))
                {
                    contextMenuStrip1.Show();
                    printToolStripMenuItem.Visible = true;
                    moveMenuItem.Visible = false;
                }
            }

            if (tlVectorControl1.Operation == ToolOperation.PolyLine && tlVectorControl1.SVGDocument.CurrentElement is Polyline && fileType == true)
            {
                tlVectorControl1.Operation = ToolOperation.Select;
                tlVectorControl1.ChangeLevel(LevelType.Bottom);
                PointF[] t = (tlVectorControl1.SVGDocument.CurrentElement as Polyline).Pt;
                PSPDEV pspDev = new PSPDEV();
                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                pspDev.Number = -1;
                pspDev.Type = "Polyline";
                pspDev.FirstNode = -1;
                pspDev.LastNode = -1;
                frmLinenew dlg = new frmLinenew(pspDev);
                dlg.derefucelineflag = Reducelineflag;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                    PSPDEV pspName = new PSPDEV();
                    pspName.Name = dlg.Name;
                    pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspName.Type = "Polyline";
                    IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                    if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                    {
                        MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.Delete();
                        return;
                    }
                    //if (element.Count > 0)
                    //{
                    //    foreach (XmlNode node in element)
                    //    {
                    //        if (node.InnerText == dlg.TextInput)
                    //        {
                    //            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            tlVectorControl1.Delete();
                    //            return;
                    //        }
                    //    }
                    //}
                    pspDev.Name = dlg.Name;
                    pspDev.Number = -1;
                    pspDev.Type = "Polyline";
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = -1;
                    pspDev.EleID = tlVectorControl1.SVGDocument.CurrentElement.ID;
                    pspDev.SUID = Guid.NewGuid().ToString();
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.X1 = t[0].X;
                    pspDev.Y1 = t[0].Y;
                    pspDev.X2 = t[1].X;
                    pspDev.Y2 = t[1].Y;
                    pspDev.Lable = "支路";
                    if (dlg.LineLength != "")
                        pspDev.LineLength = Convert.ToDouble(dlg.LineLength);
                    string tempp = dlg.LineLev;
                    int tel = tempp.Length;
                    if (tempp.Contains("kV") || tempp.Contains("KV") || tempp.Contains("kv") || tempp.Contains("Kv"))
                    {
                        tempp = tempp.Substring(0, tel - 2);
                    }  
                    pspDev.VoltR = Convert.ToDouble(tempp);
                    if (dlg.LineR != "")
                        pspDev.LineR = Convert.ToDouble(dlg.LineR);
                    if (dlg.LineTQ != "")
                        pspDev.LineTQ = Convert.ToDouble(dlg.LineTQ);
                    if (dlg.LineGNDC != "")
                        pspDev.LineGNDC = Convert.ToDouble(dlg.LineGNDC);
                    if (dlg.LineLevel != "")
                        pspDev.LineLevel = dlg.LineLevel;
                    if (dlg.LineType != "")
                        pspDev.LineType = dlg.LineType;
                    if (dlg.LineStatus != "")
                        pspDev.LineStatus = dlg.LineStatus;
                    if (dlg.linevalue != "")
                    {
                        pspDev.BigP = Convert.ToDouble(dlg.linevalue);
                    }
                    if (dlg.ReferenceVolt != "")
                    {
                        pspDev.ReferenceVolt = Convert.ToDouble(dlg.ReferenceVolt);
                    }
                    if (dlg.LineType != "")
                    {
                        WireCategory wirewire = new WireCategory();
                        wirewire.WireType = dlg.LineType;
                        WireCategory wirewire2 = new WireCategory();
                        wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKey", wirewire);
                        if (wirewire2 != null)
                            pspDev.LineChange = (double)wirewire2.WireChange;

                        //switch (dlg.LineType)
                        //{
                        //    case "2*LGJ-400":
                        //        {
                        //            if (pspDev.LineR == 0 && dlg.LineLength != "")
                        //                pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.04;
                        //            if (pspDev.LineTQ == 0 && dlg.LineLength != "")
                        //                pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.303;
                        //            if (pspDev.LineGNDC == 0 && dlg.LineLength != "")
                        //                pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.9;
                        //            pspDev.LineChange = 1690;
                        //        } break;
                        //    case "2*LGJ-300":
                        //        {
                        //            if (pspDev.LineR == 0 && dlg.LineLength != "")
                        //                pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.054;
                        //            if (pspDev.LineTQ == 0 && dlg.LineLength != "")
                        //                pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.308;
                        //            if (pspDev.LineGNDC == 0 && dlg.LineLength != "")
                        //                pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.7;
                        //            pspDev.LineChange = 1400;
                        //        } break;
                        //    case "2*LGJ-240":
                        //        {
                        //            if (pspDev.LineR == 0 && dlg.LineLength != "")
                        //                pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.066;
                        //            if (pspDev.LineTQ == 0 && dlg.LineLength != "")
                        //                pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.310;
                        //            if (pspDev.LineGNDC == 0 && dlg.LineLength != "")
                        //                pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.5;
                        //            pspDev.LineChange = 1220;
                        //        } break;
                        //    case "LGJ-400":
                        //        {
                        //            if (pspDev.LineR == 0 && dlg.LineLength != "")
                        //                pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.08;
                        //            if (pspDev.LineTQ == 0 && dlg.LineLength != "")
                        //                pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.417;
                        //            if (pspDev.LineGNDC == 0 && dlg.LineLength != "")
                        //                pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 13.2;
                        //            pspDev.LineChange = 845;
                        //        } break;


                        //}
                    }
                    Services.BaseService.Create<PSPDEV>(pspDev);
                    tlVectorControl1.SVGDocument.CurrentElement.SetAttribute("year", dlg.TYear);
                    tlVectorControl1.SVGDocument.CurrentElement.SetAttribute("linevalue", dlg.linevalue);

                    Topology2();
                }
                else
                {
                    tlVectorControl1.Delete();
                }

                return;
            }
            if (tlVectorControl1.Operation == ToolOperation.PolyLine && tlVectorControl1.SVGDocument.CurrentElement is Polyline && fileType != true)
            {
                tlVectorControl1.Operation = ToolOperation.Select;
                tlVectorControl1.ChangeLevel(LevelType.Bottom);
                PointF[] t = (tlVectorControl1.SVGDocument.CurrentElement as Polyline).Pt;
                PSPDEV pspDev = new PSPDEV();
                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                pspDev.Number = -1;
                pspDev.Type = "Polyline";
                pspDev.FirstNode = -1;
                pspDev.LastNode = -1;
                frmLine dlg = new frmLine(pspDev);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                    PSPDEV pspName = new PSPDEV();
                    pspName.Name = dlg.Name;
                    pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspName.Type = "Polyline";
                    IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                    if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                    {
                        MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.Delete();
                        return;
                    }
                    //if (element.Count > 0)
                    //{
                    //    foreach (XmlNode node in element)
                    //    {
                    //        if (node.InnerText == dlg.TextInput)
                    //        {
                    //            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            tlVectorControl1.Delete();
                    //            return;
                    //        }
                    //    }
                    //}
                    pspDev.Name = dlg.Name;
                    pspDev.Number = -1;
                    pspDev.Type = "Polyline";
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = -1;
                    pspDev.EleID = tlVectorControl1.SVGDocument.CurrentElement.ID;
                    pspDev.SUID = Guid.NewGuid().ToString();
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.X1 = t[0].X;
                    pspDev.Y1 = t[0].Y;
                    pspDev.X2 = t[1].X;
                    pspDev.Y2 = t[1].Y;
                    pspDev.Lable = "支路";
                    if (dlg.LineLength != "")
                        pspDev.LineLength = Convert.ToDouble(dlg.LineLength);

                    if (dlg.LineLevel != "")
                        pspDev.LineLevel = dlg.LineLevel;
                    if (dlg.LineType != "")
                        pspDev.LineType = dlg.LineType;
                    if (dlg.LineStatus != "")
                        pspDev.LineStatus = dlg.LineStatus;
                    if (dlg.PositiveR != "")
                        pspDev.PositiveR = Convert.ToDouble(dlg.PositiveR);
                    if (dlg.PositiveTQ != "")
                        pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                    if (dlg.PositiveES != "")
                    {
                        pspDev.SmallTQ = Convert.ToDouble(dlg.PositiveES);
                    }
                    if (dlg.ZeroR != "")
                        pspDev.ZeroR = Convert.ToDouble(dlg.ZeroR);
                    if (dlg.ZeroTQ != "")
                        pspDev.ZeroTQ = Convert.ToDouble(dlg.ZeroTQ);
                    if (dlg.ZeroES != "")
                    {
                        pspDev.BigTQ = Convert.ToDouble(dlg.ZeroES);
                    }
                    if (dlg.HuganFirst == "是")
                        pspDev.HuganFirst = 1;
                    else
                        pspDev.HuganFirst = 0;
                    if (dlg.HuganLine1 != "")
                        pspDev.HuganLine1 = dlg.HuganLine1;
                    if (dlg.HuganLine2 != "")
                        pspDev.HuganLine2 = dlg.HuganLine2;
                    if (dlg.HuganLine3 != "")
                        pspDev.HuganLine3 = dlg.HuganLine3;
                    if (dlg.HuganLine4 != "")
                        pspDev.HuganLine4 = dlg.HuganLine4;
                    if (dlg.HuganTQ1 != "")
                        pspDev.HuganTQ1 = Convert.ToDouble(dlg.HuganTQ1);
                    if (dlg.HuganTQ2 != "")
                        pspDev.HuganTQ2 = Convert.ToDouble(dlg.HuganTQ2);
                    if (dlg.HuganTQ3 != "")
                        pspDev.HuganTQ3 = Convert.ToDouble(dlg.HuganTQ3);
                    if (dlg.HuganTQ4 != "")
                        pspDev.HuganTQ4 = Convert.ToDouble(dlg.HuganTQ4);
                    if (dlg.HuganTQ5 != "")
                        pspDev.HuganTQ5 = Convert.ToDouble(dlg.HuganTQ5);

                    pspDev.KName = dlg.ISwitchStatus;
                    pspDev.KSwitchStatus = dlg.JSwitchStatus;

                    if (dlg.LineType != "")
                    {
                        WireCategory wirewire = new WireCategory();
                        wirewire.WireType = dlg.LineType;
                        WireCategory wirewire2 = new WireCategory();
                        wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKey", wirewire);
                        if (wirewire2 != null)
                            pspDev.LineChange = (double)wirewire2.WireChange;
                        //switch (dlg.LineType)
                        //{
                        //    case "2*LGJ-400":
                        //        {
                        //            if (pspDev.LineR == 0 && dlg.LineLength != "")
                        //                pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.04;
                        //            if (pspDev.LineTQ == 0 && dlg.LineLength != "")
                        //                pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.303;
                        //            if (pspDev.LineGNDC == 0 && dlg.LineLength != "")
                        //                pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.9;
                        //            pspDev.LineChange = 1690;
                        //        } break;
                        //    case "2*LGJ-300":
                        //        {
                        //            if (pspDev.LineR == 0 && dlg.LineLength != "")
                        //                pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.054;
                        //            if (pspDev.LineTQ == 0 && dlg.LineLength != "")
                        //                pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.308;
                        //            if (pspDev.LineGNDC == 0 && dlg.LineLength != "")
                        //                pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.7;
                        //            pspDev.LineChange = 1400;
                        //        } break;
                        //    case "2*LGJ-240":
                        //        {
                        //            if (pspDev.LineR == 0 && dlg.LineLength != "")
                        //                pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.066;
                        //            if (pspDev.LineTQ == 0 && dlg.LineLength != "")
                        //                pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.310;
                        //            if (pspDev.LineGNDC == 0 && dlg.LineLength != "")
                        //                pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.5;
                        //            pspDev.LineChange = 1220;
                        //        } break;
                        //    case "LGJ-400":
                        //        {
                        //            if (pspDev.LineR == 0 && dlg.LineLength != "")
                        //                pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.08;
                        //            if (pspDev.LineTQ == 0 && dlg.LineLength != "")
                        //                pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.417;
                        //            if (pspDev.LineGNDC == 0 && dlg.LineLength != "")
                        //                pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 13.2;
                        //            pspDev.LineChange = 845;
                        //        } break;


                        //}
                    }
                    string tempp = dlg.LineLev;
                    int tel = tempp.Length;
                    if (tel == 1)
                        pspDev.VoltR = 0;
                    else
                    {
                        if (tempp.Contains("kV") || tempp.Contains("KV") || tempp.Contains("kv") || tempp.Contains("Kv"))
                        {
                            tempp = tempp.Substring(0, tel - 2);
                        }  
                        pspDev.VoltR = Convert.ToDouble(tempp);
                    }
                    if (pspDev.PositiveTQ == 0)
                        pspDev.PositiveTQ = pspDev.LineTQ;
                    Services.BaseService.Create<PSPDEV>(pspDev);
                    Topology2();
                }
                else
                {
                    tlVectorControl1.Delete();
                }

                return;
            }
        }

        void tlVectorControl1_LeftClick(object sender, SvgElementSelectedEventArgs e)
        {
            if (tlVectorControl1.Operation == ToolOperation.Text)
            {
                frmTextInput ft = new frmTextInput();
                if (ft.ShowDialog() == DialogResult.OK)
                {
                    string txt = ft.Content;
                    XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                    System.Drawing.Point point1 = tlVectorControl1.PointToView(new System.Drawing.Point(e.Mouse.X, e.Mouse.Y));
                    n1.SetAttribute("x", point1.X.ToString());
                    n1.SetAttribute("y", point1.Y.ToString());
                    n1.InnerText = txt;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
            }
            else if (tlVectorControl1.Operation == ToolOperation.Symbol)
            {
                XmlElement temp = e.SvgElement as XmlElement;

                if (temp is Use && (temp.GetAttribute("xlink:href").Contains("Substation") || temp.GetAttribute("xlink:href").Contains("Power") || temp.GetAttribute("xlink:href").Contains("motherlinenode")))
                {
                    PSPDEV pspDev23 = new PSPDEV();
                    //pspDev.EleID = element.GetAttribute("id");
                    pspDev23.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    if (temp.GetAttribute("xlink:href").Contains("Substation"))
                    {
                        pspDev23.Lable = "变电站";
                    }
                    else if (temp.GetAttribute("xlink:href").Contains("motherlinenode"))
                    {
                        pspDev23.Lable = "母线节点";
                    }
                    else if (temp.GetAttribute("xlink:href").Contains("Power"))
                    {
                        pspDev23.Lable = "电厂";
                    }
                    frmSubstation dlg = new frmSubstation(pspDev23);
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {

                        //XmlElement temp = tlVectorControl1.SVGDocument.CurrentElement;                
                        if (temp != null)
                        {
                            PSPDEV pspDev2 = new PSPDEV();
                            XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "Use";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 1)
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tlVectorControl1.Delete();
                                return;
                            }
                            if (true)
                            {

                                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                if (temp is Polyline)
                                {
                                    double x1 = Convert.ToDouble(temp.GetAttribute("x1"));
                                    double y1 = Convert.ToDouble(temp.GetAttribute("y1"));
                                    double x2 = Convert.ToDouble(temp.GetAttribute("x2"));
                                    double y2 = Convert.ToDouble(temp.GetAttribute("y2"));

                                    tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                    n1.SetAttribute("x", Convert.ToString(x1 + (x2 - x1) / 2));
                                    n1.SetAttribute("y", Convert.ToString(y1 + (y2 - y1) / 2));

                                }
                                else
                                {
                                    n1.SetAttribute("x", temp.GetAttribute("x"));
                                    n1.SetAttribute("y", temp.GetAttribute("y"));
                                    RectangleF t = ((IGraph)temp).GetBounds();
                                    n1.SetAttribute("x", t.X.ToString());
                                    n1.SetAttribute("y", t.Y.ToString());
                                }


                                n1.InnerText = dlg.Name;
                                n1.SetAttribute("layer", SvgDocument.currentLayer);
                                n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                tlVectorControl1.Operation = ToolOperation.Select;
                            }
                            else
                            {
                                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                if (temp is Polyline)
                                {
                                    double x1 = Convert.ToDouble(temp.GetAttribute("x1"));
                                    double y1 = Convert.ToDouble(temp.GetAttribute("y1"));
                                    double x2 = Convert.ToDouble(temp.GetAttribute("x2"));
                                    double y2 = Convert.ToDouble(temp.GetAttribute("y2"));

                                    tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                    n1.SetAttribute("x", Convert.ToString(x1 + (x2 - x1) / 2));
                                    n1.SetAttribute("y", Convert.ToString(y1 + (y2 - y1) / 2));
                                }
                                else
                                {
                                    RectangleF t = ((IGraph)temp).GetBounds();
                                    n1.SetAttribute("x", (t.X - 8).ToString());
                                    n1.SetAttribute("y", (t.Y - 8).ToString());
                                }

                                n1.InnerText = dlg.Name;
                                n1.SetAttribute("layer", SvgDocument.currentLayer);
                                n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                tlVectorControl1.Operation = ToolOperation.Select;

                            }
                            PSPDEV pspDev = new PSPDEV();
                            if (temp is Use)
                            {
                                RectangleF t = ((IGraph)temp).GetBounds();
                                pspDev.SUID = Guid.NewGuid().ToString();
                                pspDev.EleID = temp.GetAttribute("id");
                                pspDev.Name = dlg.Name;
                                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                pspDev.X1 = t.X;
                                pspDev.Y1 = t.Y;
                                pspDev.Number = -1;
                                pspDev.FirstNode = -1;
                                pspDev.LastNode = -1;
                                pspDev.Type = "Use";
                                if (temp.GetAttribute("xlink:href").Contains("Substation"))
                                {
                                    pspDev.Lable = "变电站";
                                }
                                else if (temp.GetAttribute("xlink:href").Contains("motherlinenode"))
                                {
                                    pspDev.Lable = "母线节点";
                                }
                                else if (temp.GetAttribute("xlink:href").Contains("Power"))
                                {
                                    pspDev.Lable = "电厂";
                                }
                                if (dlg.VoltR != "")
                                    pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                                if (dlg.Burthen != "")
                                    pspDev.Burthen = Convert.ToDecimal(dlg.Burthen);

                                //if (dlg.InPutP!="")
                                //    pspDev.InPutP = Convert.ToDouble(dlg.InPutP);
                                //if (dlg.InPutQ!="")
                                //    pspDev.InPutQ = Convert.ToDouble(dlg.InPutQ);
                                if (dlg.InPutP != "")
                                    pspDev.InPutP = Convert.ToDouble(dlg.InPutP);
                                if (dlg.InPutQ != "")
                                    pspDev.InPutQ = Convert.ToDouble(dlg.InPutQ);
                                if (dlg.OutP != "")
                                    pspDev.OutP = Convert.ToDouble(dlg.OutP);
                                if (dlg.OutQ != "")
                                    pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                                if (dlg.NodeType == "是")
                                {
                                    pspDev.NodeType = "0";
                                }
                                else
                                {
                                    pspDev.NodeType = "1";
                                }
                                Services.BaseService.Create<PSPDEV>(pspDev);
                            }
                            else if (temp is Polyline)
                            {
                                pspDev.SUID = Guid.NewGuid().ToString();
                                pspDev.EleID = temp.GetAttribute("id");
                                pspDev.Name = dlg.Name;
                                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                pspDev.X1 = Convert.ToDouble(temp.GetAttribute("x1"));
                                pspDev.Y1 = Convert.ToDouble(temp.GetAttribute("y1"));
                                pspDev.X2 = Convert.ToDouble(temp.GetAttribute("x2"));
                                pspDev.Y2 = Convert.ToDouble(temp.GetAttribute("y2"));
                                pspDev.Number = -1;
                                pspDev.FirstNode = -1;
                                pspDev.LastNode = -1;
                                pspDev.Type = "Polyline";
                                Services.BaseService.Create<PSPDEV>(pspDev);
                            }
                        }

                    }
                    else
                    {
                        tlVectorControl1.Delete();
                    }
                }
                else
                {
                    if (temp is Use && (temp.GetAttribute("xlink:href").Contains("dynamotorline")))//接地支路
                    {
                        frmFadejie dlg = new frmFadejie(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        dlg.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {

                            //XmlElement temp = tlVectorControl1.SVGDocument.CurrentElement;                
                            if (temp != null)
                            {
                                PSPDEV pspDev2 = new PSPDEV();
                                XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                                PSPDEV pspName = new PSPDEV();
                                pspName.Name = dlg.Name;
                                pspName.Type = "dynamotorline";
                                pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                                if (listName.Count >= 1)
                                {
                                    MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tlVectorControl1.Delete();
                                    return;
                                }
                                if (true)
                                {
                                    tlVectorControl1.Operation = ToolOperation.Select;
                                    PSPDEV pspDev = new PSPDEV();
                                    tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                    RectangleF t = ((IGraph)temp).GetBounds();
                                    pspDev.SUID = Guid.NewGuid().ToString();
                                    pspDev.EleID = temp.GetAttribute("id");
                                    pspDev.Name = dlg.Name;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    pspDev.X1 = t.X;
                                    pspDev.Y1 = t.Y;
                                    pspDev.Number = -1;
                                    pspDev.FirstNode = -1;
                                    pspDev.LastNode = 0;
                                    pspDev.Type = "dynamotorline";
                                    if (temp.GetAttribute("xlink:href").Contains("dynamotorline"))
                                    {
                                        pspDev.Lable = "发电厂支路";
                                    }
                                    else if (temp.GetAttribute("xlink:href").Contains("gndline"))
                                    {
                                        pspDev.Lable = "接地支路";
                                    }

                                    pspDev.HuganLine1 = dlg.FirstNodeName;
                                    pspDev.HuganLine3 = dlg.SwitchStatus;
                                    if (dlg.OutP != "")
                                        pspDev.OutP = Convert.ToDouble(dlg.OutP);
                                    if (dlg.OutQ != "")
                                        pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                                    if (dlg.VoltR != "")
                                        pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                                    if (dlg.VoltV != "")
                                        pspDev.VoltV = Convert.ToDouble(dlg.VoltV);
                                    if (dlg.PositiveTQ != "")
                                        pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                                    if (dlg.NegativeTQ != "")
                                        pspDev.ZeroTQ = Convert.ToDouble(dlg.NegativeTQ);
                                    Services.BaseService.Create<PSPDEV>(pspDev);
                                }
                            }
                        }
                        else
                        {
                            tlVectorControl1.Delete();
                        }
                    }
                    else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("gndline")))//接地支路
                    {
                        frmFadejie dlg = new frmFadejie(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        dlg.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {

                            //XmlElement temp = tlVectorControl1.SVGDocument.CurrentElement;                
                            if (temp != null)
                            {
                                PSPDEV pspDev2 = new PSPDEV();
                                XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                                PSPDEV pspName = new PSPDEV();
                                pspName.Name = dlg.Name;
                                pspName.Type = "gndline";
                                pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                                if (listName.Count >= 1)
                                {
                                    MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tlVectorControl1.Delete();
                                    return;
                                }
                                if (true)
                                {
                                    tlVectorControl1.Operation = ToolOperation.Select;
                                    PSPDEV pspDev = new PSPDEV();
                                    tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                    RectangleF t = ((IGraph)temp).GetBounds();
                                    pspDev.SUID = Guid.NewGuid().ToString();
                                    pspDev.EleID = temp.GetAttribute("id");
                                    pspDev.Name = dlg.Name;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    pspDev.X1 = t.X;
                                    pspDev.Y1 = t.Y;
                                    pspDev.Number = -1;
                                    pspDev.FirstNode = -1;
                                    pspDev.LastNode = 0;
                                    pspDev.Type = "gndline";
                                    if (temp.GetAttribute("xlink:href").Contains("dynamotorline"))
                                    {
                                        pspDev.Lable = "发电厂支路";
                                    }
                                    else if (temp.GetAttribute("xlink:href").Contains("gndline"))
                                    {
                                        pspDev.Lable = "接地支路";
                                    }

                                    pspDev.HuganLine1 = dlg.FirstNodeName;
                                    pspDev.HuganLine3 = dlg.SwitchStatus;
                                    if (dlg.OutP != "")
                                        pspDev.OutP = Convert.ToDouble(dlg.OutP);
                                    if (dlg.OutQ != "")
                                        pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                                    if (dlg.VoltR != "")
                                        pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                                    if (dlg.VoltV != "")
                                        pspDev.VoltV = Convert.ToDouble(dlg.VoltV);
                                    if (dlg.PositiveTQ != "")
                                        pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                                    if (dlg.NegativeTQ != "")
                                        pspDev.ZeroTQ = Convert.ToDouble(dlg.NegativeTQ);
                                    Services.BaseService.Create<PSPDEV>(pspDev);
                                }
                            }
                        }
                        else
                        {
                            tlVectorControl1.Delete();
                        }
                    }
                    else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("loadline")))
                    {
                        frmLoad dlgLoad = new frmLoad();
                        dlgLoad.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        if (dlgLoad.ShowDialog(this) == DialogResult.OK)
                        {
                            if (temp != null)
                            {
                                PSPDEV pspDev2 = new PSPDEV();
                                XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                                PSPDEV pspName = new PSPDEV();
                                pspName.Name = dlgLoad.Name;
                                pspName.Type = "loadline";
                                pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                                if (listName.Count >= 1)
                                {
                                    MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tlVectorControl1.Delete();
                                    return;
                                }

                                tlVectorControl1.Operation = ToolOperation.Select;
                                PSPDEV pspDev = new PSPDEV();
                                tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                RectangleF t = ((IGraph)temp).GetBounds();
                                pspDev.SUID = Guid.NewGuid().ToString();
                                pspDev.EleID = temp.GetAttribute("id");
                                pspDev.Name = dlgLoad.Name;
                                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                pspDev.X1 = t.X;
                                pspDev.Y1 = t.Y;
                                pspDev.Number = -1;
                                pspDev.FirstNode = -1;
                                pspDev.LastNode = 0;
                                pspDev.Type = "loadline";

                                pspDev.Lable = "负荷支路";

                                pspDev.HuganLine1 = dlgLoad.FirstNodeName;
                                if (dlgLoad.InPutP != "")
                                {
                                    pspDev.InPutP = Convert.ToDouble(dlgLoad.InPutP);
                                }
                                if (dlgLoad.InPutQ != "")
                                {
                                    pspDev.InPutQ = Convert.ToDouble(dlgLoad.InPutQ);
                                }
                                if (dlgLoad.VoltR != "")
                                {
                                    pspDev.VoltR = Convert.ToDouble(dlgLoad.VoltR);
                                }


                                pspDev.HuganLine3 = dlgLoad.LoadSwitchState;

                                Services.BaseService.Create<PSPDEV>(pspDev);
                            }
                        }
                        else
                        {
                            tlVectorControl1.Delete();
                        }
                    }
                    else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("transformertwozu")))
                    {
                        frmTwoTra dlgTra = new frmTwoTra(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        dlgTra.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        if (dlgTra.ShowDialog(this) == DialogResult.OK)
                        {
                            PSPDEV pspDev2 = new PSPDEV();
                            XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlgTra.Name;
                            pspName.Type = "transformertwozu";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 1)
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tlVectorControl1.Delete();
                                return;
                            }

                            tlVectorControl1.Operation = ToolOperation.Select;
                            PSPDEV pspDev = new PSPDEV();
                            tlVectorControl1.ChangeLevel(LevelType.Bottom);
                            RectangleF t = ((IGraph)temp).GetBounds();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = temp.GetAttribute("id");
                            pspDev.Name = dlgTra.Name;
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.X1 = t.X;
                            pspDev.Y1 = t.Y;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = 0;
                            pspDev.Type = "transformertwozu";

                            pspDev.Lable = "二绕组变压器";
                            pspDev.HuganLine1 = dlgTra.FirstName;
                            pspDev.HuganLine2 = dlgTra.LastName;
                            pspDev.HuganLine3 = dlgTra.FirstSwitchState;
                            pspDev.HuganLine4 = dlgTra.LastSwitchState;
                            pspDev.LineLevel = dlgTra.FirstType;
                            pspDev.LineType = dlgTra.LastType;

                            if (dlgTra.K != "")
                            {
                                pspDev.K = Convert.ToDouble(dlgTra.K);
                            }
                            if (dlgTra.PositiveR != "")
                            {
                                pspDev.PositiveR = Convert.ToDouble(dlgTra.PositiveR);
                            }
                            if (dlgTra.PositiveTQ != "")
                            {
                                pspDev.PositiveTQ = Convert.ToDouble(dlgTra.PositiveTQ);
                            }
                            if (dlgTra.ZeroR != "")
                            {
                                pspDev.ZeroR = Convert.ToDouble(dlgTra.ZeroR);
                            }
                            if (dlgTra.ZeroTQ != "")
                            {
                                pspDev.ZeroTQ = Convert.ToDouble(dlgTra.ZeroTQ);
                            }
                            if (dlgTra.NeutralNodeTQ != "")
                            {
                                pspDev.BigTQ = Convert.ToDouble(dlgTra.NeutralNodeTQ);
                            }



                            Services.BaseService.Create<PSPDEV>(pspDev);
                        }
                        else
                        {
                            tlVectorControl1.Delete();
                        }
                    }
                    else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("transformerthirdzu")))
                    {
                        frmThridTra dlgThridTra = new frmThridTra(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        dlgThridTra.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        if (dlgThridTra.ShowDialog(this) == DialogResult.OK)
                        {
                            PSPDEV pspDev2 = new PSPDEV();
                            XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlgThridTra.Name;
                            pspName.Type = "transformerthirdzu";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 1)
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tlVectorControl1.Delete();
                                return;
                            }

                            tlVectorControl1.Operation = ToolOperation.Select;
                            PSPDEV pspDev = new PSPDEV();
                            tlVectorControl1.ChangeLevel(LevelType.Bottom);
                            RectangleF t = ((IGraph)temp).GetBounds();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = temp.GetAttribute("id");
                            pspDev.Name = dlgThridTra.Name;
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.X1 = t.X;
                            pspDev.Y1 = t.Y;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = 0;
                            pspDev.Type = "transformerthirdzu";

                            pspDev.Lable = "三绕组变压器";
                            pspDev.HuganLine1 = dlgThridTra.IName;
                            pspDev.HuganLine2 = dlgThridTra.JName;
                            pspDev.HuganLine3 = dlgThridTra.ISwitchState;
                            pspDev.HuganLine4 = dlgThridTra.JSwitchState;
                            pspDev.LineLevel = dlgThridTra.IType;
                            pspDev.LineType = dlgThridTra.JType;
                            pspDev.LineStatus = dlgThridTra.KType;
                            pspDev.KName = dlgThridTra.KName;
                            pspDev.KSwitchStatus = dlgThridTra.KSwitchState;

                            if (dlgThridTra.IK != "")
                            {
                                pspDev.K = Convert.ToDouble(dlgThridTra.IK);
                            }
                            if (dlgThridTra.JK != "")
                            {
                                pspDev.G = Convert.ToDouble(dlgThridTra.JK);
                            }
                            if (dlgThridTra.KK != "")
                            {
                                pspDev.BigP = Convert.ToDouble(dlgThridTra.KK);
                            }
                            if (dlgThridTra.IR != "")
                            {
                                pspDev.HuganTQ1 = Convert.ToDouble(dlgThridTra.IR);
                            }
                            if (dlgThridTra.JR != "")
                            {
                                pspDev.HuganTQ2 = Convert.ToDouble(dlgThridTra.JR);
                            }
                            if (dlgThridTra.KR != "")
                            {
                                pspDev.HuganTQ3 = Convert.ToDouble(dlgThridTra.KR);
                            }
                            if (dlgThridTra.ITQ != "")
                            {
                                pspDev.HuganTQ4 = Convert.ToDouble(dlgThridTra.ITQ);
                            }
                            if (dlgThridTra.JTQ != "")
                            {
                                pspDev.HuganTQ5 = Convert.ToDouble(dlgThridTra.JTQ);
                            }
                            if (dlgThridTra.KTQ != "")
                            {
                                pspDev.SmallTQ = Convert.ToDouble(dlgThridTra.KTQ);
                            }
                            if (dlgThridTra.ZeroTQ != "")
                            {
                                pspDev.ZeroTQ = Convert.ToDouble(dlgThridTra.ZeroTQ);
                            }
                            if (dlgThridTra.NeutralNodeTQ != "")
                            {
                                pspDev.BigTQ = Convert.ToDouble(dlgThridTra.NeutralNodeTQ);
                            }


                            Services.BaseService.Create<PSPDEV>(pspDev);
                        }
                        else
                        {
                            tlVectorControl1.Delete();
                        }
                    }
                    else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("串联电容电抗器")))
                    {
                        frmCapacity dlgCapacity = new frmCapacity(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        dlgCapacity.SetEnable(true);
                        dlgCapacity.Text = "串联电容电抗器";
                        dlgCapacity.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        if (dlgCapacity.ShowDialog(this) == DialogResult.OK)
                        {
                            PSPDEV pspDev2 = new PSPDEV();
                            XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlgCapacity.Name;
                            pspName.Type = "串联电容电抗器";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 1)
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tlVectorControl1.Delete();
                                return;
                            }

                            tlVectorControl1.Operation = ToolOperation.Select;
                            PSPDEV pspDev = new PSPDEV();
                            tlVectorControl1.ChangeLevel(LevelType.Bottom);
                            RectangleF t = ((IGraph)temp).GetBounds();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = temp.GetAttribute("id");
                            pspDev.Name = dlgCapacity.Name;
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.X1 = t.X;
                            pspDev.Y1 = t.Y;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = 0;
                            pspDev.Type = "串联电容电抗器";

                            pspDev.Lable = dlgCapacity.Lable;

                            if (dlgCapacity.PositiveTQ != "")
                            {
                                pspDev.PositiveTQ = Convert.ToDouble(dlgCapacity.PositiveTQ);
                            }
                            pspDev.HuganLine1 = dlgCapacity.FirstNodeName;
                            //pspDev.HuganLine2 = dlgCapacity.LastNodeName;

                            Services.BaseService.Create<PSPDEV>(pspDev);
                        }
                        else
                        {
                            tlVectorControl1.Delete();
                        }

                    }
                    else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("并联电容电抗器")))
                    {
                        frmCapacity dlgCapacity = new frmCapacity(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        dlgCapacity.SetEnable(false);
                        dlgCapacity.Text = "并联电容电抗器";
                        dlgCapacity.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        if (dlgCapacity.ShowDialog(this) == DialogResult.OK)
                        {
                            PSPDEV pspDev2 = new PSPDEV();
                            XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlgCapacity.Name;
                            pspName.Type = "并联电容电抗器";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 1)
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tlVectorControl1.Delete();
                                return;
                            }

                            tlVectorControl1.Operation = ToolOperation.Select;
                            PSPDEV pspDev = new PSPDEV();
                            tlVectorControl1.ChangeLevel(LevelType.Bottom);
                            RectangleF t = ((IGraph)temp).GetBounds();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = temp.GetAttribute("id");
                            pspDev.Name = dlgCapacity.Name;
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.X1 = t.X;
                            pspDev.Y1 = t.Y;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = 0;
                            pspDev.Type = "并联电容电抗器";

                            pspDev.Lable = dlgCapacity.Lable;

                            if (dlgCapacity.PositiveTQ != "")
                            {
                                pspDev.PositiveTQ = Convert.ToDouble(dlgCapacity.PositiveTQ);
                            }
                            pspDev.HuganLine1 = dlgCapacity.FirstNodeName;
                            //pspDev.HuganLine2 = dlgCapacity.LastNodeName;

                            Services.BaseService.Create<PSPDEV>(pspDev);
                        }
                        else
                        {
                            tlVectorControl1.Delete();
                        }
                    }
                    else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("1/2母联开关")))
                    {
                        frmMuLian dlgmulian = new frmMuLian(tlVectorControl1.SVGDocument.CurrentLayer.ID);

                        dlgmulian.Text = "1/2母联开关";
                        dlgmulian.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        if (dlgmulian.ShowDialog(this) == DialogResult.OK)
                        {
                            PSPDEV pspDev2 = new PSPDEV();
                            XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlgmulian.Name;
                            pspName.Type = "1/2母联开关";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 1)
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tlVectorControl1.Delete();
                                return;
                            }

                            tlVectorControl1.Operation = ToolOperation.Select;
                            PSPDEV pspDev = new PSPDEV();
                            tlVectorControl1.ChangeLevel(LevelType.Bottom);
                            RectangleF t = ((IGraph)temp).GetBounds();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = temp.GetAttribute("id");
                            pspDev.Name = dlgmulian.Name;
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.X1 = t.X;
                            pspDev.Y1 = t.Y;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = 0;
                            pspDev.Type = "1/2母联开关";

                            pspDev.Lable = "1/2母联开关";


                            pspDev.HuganLine1 = dlgmulian.FirstNodeName;
                            pspDev.HuganLine2 = dlgmulian.LastNodeName;
                            pspDev.HuganLine3 = dlgmulian.SwitchStatus;

                            Services.BaseService.Create<PSPDEV>(pspDev);
                        }
                        else
                        {
                            tlVectorControl1.Delete();
                        }
                    }
                    else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("2/3母联开关")))
                    {
                        frmMuLian2 dlgmulian = new frmMuLian2(tlVectorControl1.SVGDocument.CurrentLayer.ID);

                        dlgmulian.Text = "2/3母联开关";
                        dlgmulian.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        if (dlgmulian.ShowDialog(this) == DialogResult.OK)
                        {
                            PSPDEV pspDev2 = new PSPDEV();
                            XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlgmulian.Name;
                            pspName.Type = "2/3母联开关";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 1)
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tlVectorControl1.Delete();
                                return;
                            }

                            tlVectorControl1.Operation = ToolOperation.Select;
                            PSPDEV pspDev = new PSPDEV();
                            tlVectorControl1.ChangeLevel(LevelType.Bottom);
                            RectangleF t = ((IGraph)temp).GetBounds();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = temp.GetAttribute("id");
                            pspDev.Name = dlgmulian.Name;
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.X1 = t.X;
                            pspDev.Y1 = t.Y;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = 0;
                            pspDev.Type = "2/3母联开关";

                            pspDev.Lable = "2/3母联开关";


                            pspDev.HuganLine1 = dlgmulian.INodeName;
                            pspDev.HuganLine2 = dlgmulian.JNodeName;
                            pspDev.HuganLine3 = dlgmulian.ILineName;
                            pspDev.HuganLine4 = dlgmulian.JLineName;
                            pspDev.KName = dlgmulian.ILoadName;
                            pspDev.KSwitchStatus = dlgmulian.JLoadName;
                            pspDev.LineLevel = dlgmulian.SwitchStatus1;
                            pspDev.LineType = dlgmulian.SwitchStatus2;
                            pspDev.LineStatus = dlgmulian.SwitchStatus3;

                            Services.BaseService.Create<PSPDEV>(pspDev);
                        }
                        else
                        {
                            tlVectorControl1.Delete();
                        }
                    }
                    //temp.RemoveAll();
                }
            }

        }

        void tlVectorControl1_AddElement(object sender, AddSvgElementEventArgs e)
        {
            //MessageBox.Show(e.SvgElement.ID);
            string larid = tlVectorControl1.SVGDocument.CurrentLayer.ID;

            if (!ChangeLayerList.Contains(larid))
            {
                ChangeLayerList.Add(larid);
            }
            XmlElement temp = e.SvgElement as XmlElement;
            if (temp is Polyline)
            {
                XmlNodeList list2 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline");
                foreach (XmlNode node in list2)
                {
                    PSPDEV dev = new PSPDEV();
                    //(node as Text).InnerText = dev.Name;
                    //XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");

                    PSPDEV dlg11 = new PSPDEV();
                    XmlElement element = node as XmlElement;
                    dev.EleID = element.GetAttribute("id");
                    PSPDEV psp = new PSPDEV();
                    dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    dlg11 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", dev);
                    if (dlg11 == null)
                    {
                        SvgElement element2 = node as SvgElement;
                        tlVectorControl1.SVGDocument.CurrentElement = element2;
                        tlVectorControl1.Delete();
                    }
                }
            }

            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("Substation") || temp.GetAttribute("xlink:href").Contains("Power") || temp.GetAttribute("xlink:href").Contains("motherlinenode")))
            {
                PSPDEV pspDev22 = new PSPDEV();
                //pspDev.EleID = element.GetAttribute("id");
                str_power = getPower(temp.GetAttribute("xlink:href"));
                pspDev22.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                if (temp.GetAttribute("xlink:href").Contains("Substation"))
                {
                    pspDev22.Lable = "变电站";

                }
                else if (temp.GetAttribute("xlink:href").Contains("motherlinenode"))
                {
                    pspDev22.Lable = "母线节点";
                }
                else if (temp.GetAttribute("xlink:href").Contains("Power"))
                {
                    pspDev22.Lable = "电厂";
                }
                frmSubstation dlg = new frmSubstation(pspDev22);
                dlg.Str_Power = str_power;
                if (tlVectorControl1.SVGDocument.FileName.Length > 5)
                {
                    dlg.Str_year = tlVectorControl1.SVGDocument.FileName.Substring(0, 4);
                }


                if (dlg.ShowDialog(this) == DialogResult.OK)
                {

                    //XmlElement temp = tlVectorControl1.SVGDocument.CurrentElement;                
                    if (temp != null)
                    {
                        PSPDEV pspDev2 = new PSPDEV();
                        XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.Type = "Use";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 1)
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tlVectorControl1.Delete();
                            return;
                        }


                        //if (pspName.Name == "")
                        //{
                        //    MessageBox.Show("名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //    tlVectorControl1.Delete();
                        //    return;
                        //}
                        if (true)
                        {

                            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            if (temp is Polyline)
                            {
                                double x1 = Convert.ToDouble(temp.GetAttribute("x1"));
                                double y1 = Convert.ToDouble(temp.GetAttribute("y1"));
                                double x2 = Convert.ToDouble(temp.GetAttribute("x2"));
                                double y2 = Convert.ToDouble(temp.GetAttribute("y2"));

                                tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                n1.SetAttribute("x", Convert.ToString(x1 + (x2 - x1) / 2));
                                n1.SetAttribute("y", Convert.ToString(y1 + (y2 - y1) / 2));

                            }
                            else
                            {
                                n1.SetAttribute("x", temp.GetAttribute("x"));
                                n1.SetAttribute("y", temp.GetAttribute("y"));
                                RectangleF t = ((IGraph)temp).GetBounds();
                                n1.SetAttribute("x", (t.X - 10).ToString());
                                n1.SetAttribute("y", (t.Y - 10).ToString());
                            }


                            n1.InnerText = dlg.Name;
                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                            n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            tlVectorControl1.Operation = ToolOperation.Select;
                        }
                        else
                        {
                            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            if (temp is Polyline)
                            {
                                double x1 = Convert.ToDouble(temp.GetAttribute("x1"));
                                double y1 = Convert.ToDouble(temp.GetAttribute("y1"));
                                double x2 = Convert.ToDouble(temp.GetAttribute("x2"));
                                double y2 = Convert.ToDouble(temp.GetAttribute("y2"));

                                tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                n1.SetAttribute("x", Convert.ToString(x1 + (x2 - x1) / 2));
                                n1.SetAttribute("y", Convert.ToString(y1 + (y2 - y1) / 2));
                            }
                            else
                            {
                                RectangleF t = ((IGraph)temp).GetBounds();
                                n1.SetAttribute("x", (t.X - 10).ToString());
                                n1.SetAttribute("y", (t.Y - 10).ToString());
                            }

                            n1.InnerText = dlg.Name;
                            n1.SetAttribute("print", dlg.IsTJ ? "no" : "yes");
                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                            n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            tlVectorControl1.Operation = ToolOperation.Select;

                        }
                        PSPDEV pspDev = new PSPDEV();
                        if (temp is Use)
                        {
                            RectangleF t = ((IGraph)temp).GetBounds();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = temp.GetAttribute("id");
                            pspDev.Name = dlg.Name;
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.X1 = t.X;
                            pspDev.Y1 = t.Y;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = -1;
                            pspDev.Type = "Use";
                            if (temp.GetAttribute("xlink:href").Contains("Substation"))
                            {
                                pspDev.Lable = "变电站";
                                temp.SetAttribute("print", dlg.IsTJ ? "no" : "yes");
                            }
                            else if (temp.GetAttribute("xlink:href").Contains("motherlinenode"))
                            {
                                pspDev.Lable = "母线节点";
                            }
                            else if (temp.GetAttribute("xlink:href").Contains("Power"))
                            {
                                pspDev.Lable = "电厂";
                            }
                            if (dlg.VoltR != "")
                                pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                            if (dlg.Burthen != "")
                                pspDev.Burthen = Convert.ToDecimal(dlg.Burthen);
                            if (dlg.ReferenceVolt != "")
                            {
                                pspDev.ReferenceVolt = Convert.ToDouble(dlg.ReferenceVolt);
                            }
                            //if ((dlg.InPutP == ""))
                            //{
                            //    PSPDEV powerfactor = new PSPDEV();
                            //    powerfactor.Type = "Power";
                            //    powerfactor.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            //    powerfactor = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", powerfactor);
                            //    pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                            //    pspDev.Burthen = Convert.ToDecimal(dlg.Burthen);

                            //    //if (powerfactor!=null && (Convert.ToDecimal(dlg.Change)==2))
                            //    pspDev.InPutP = Convert.ToDouble(dlg.Burthen) * powerfactor.BigP;
                            //}

                            //if (pspDev.InPutP == 0 && dlg.Burthen!="")
                            //    pspDev.InPutP = Convert.ToDouble(dlg.Burthen) * 0.65;
                            if (dlg.InPutP != "")
                                pspDev.InPutP = Convert.ToDouble(dlg.InPutP);
                            if (dlg.InPutQ != "")
                                pspDev.InPutQ = Convert.ToDouble(dlg.InPutQ);
                            if (dlg.OutP != "")
                                pspDev.OutP = Convert.ToDouble(dlg.OutP);
                            if (dlg.OutQ != "")
                                pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                            if (dlg.NodeType == "是")
                            {
                                pspDev.NodeType = "0";
                            }
                            else
                            {
                                pspDev.NodeType = "1";
                            }
                            Services.BaseService.Create<PSPDEV>(pspDev);
                            tlVectorControl1.SVGDocument.CurrentElement.SetAttribute("year", dlg.TYear);

                        }
                        else if (temp is Polyline)
                        {
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = temp.GetAttribute("id");
                            pspDev.Name = dlg.Name;
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.X1 = Convert.ToDouble(temp.GetAttribute("x1"));
                            pspDev.Y1 = Convert.ToDouble(temp.GetAttribute("y1"));
                            pspDev.X2 = Convert.ToDouble(temp.GetAttribute("x2"));
                            pspDev.Y2 = Convert.ToDouble(temp.GetAttribute("y2"));
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = -1;
                            pspDev.Type = "Polyline";
                            Services.BaseService.Create<PSPDEV>(pspDev);
                        }
                    }
                    //*************
                    string fyear = tlVectorControl1.SVGDocument.FileName.Substring(0, 4);
                    LayerGrade lag = new LayerGrade();
                    lag.Name = fyear + "%";
                    IList laglist = Services.BaseService.GetList("SelectLayerGradeByYear", lag);
                    string power = getPower(temp.GetAttribute("xlink:href"));
                    if (laglist.Count > 0)
                    {
                        LineInfo line = new LineInfo();
                        line.LineName = " Voltage='" + power + "' and length<>'' and LayerID in (select SUID from SVG_LAYER where yearid='" + ((LayerGrade)laglist[0]).SUID + "') and ObligateField6 ='" + dlg.Name + "' order by LineName";
                        IList linList = Services.BaseService.GetList("SelectLineInfoByWhere", line);
                        for (int j = 0; j < linList.Count; j++)
                        {
                            if (((LineInfo)linList[j]).ObligateField7 != "")
                            {
                                PSPDEV p1 = new PSPDEV();
                                p1.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                p1.Name = ((LineInfo)linList[j]).ObligateField6;
                                p1.Type = "Use";
                                IList p1list = Services.BaseService.GetList("SelectPSPDEVByName", p1);
                                PSPDEV p2 = new PSPDEV();
                                p2.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                p2.Name = ((LineInfo)linList[j]).ObligateField7;
                                p2.Type = "Use";
                                IList p2list = Services.BaseService.GetList("SelectPSPDEVByName", p2);
                                if (p1list.Count > 0 && p2list.Count > 0)
                                {
                                    PointF TempPoint = TLMath.getUseOffset(temp.GetAttribute("xlink:href"));
                                    PointF[] ptt = new PointF[] { new PointF(((Use)temp).X + TempPoint.X, ((Use)temp).Y + TempPoint.Y) };
                                    Transf tran = (temp as Graph).Transform;
                                    tran.Matrix.TransformPoints(ptt);
                                    XmlNode n2 = tlVectorControl1.SVGDocument.SelectSingleNode("//*[@id='" + ((PSPDEV)p2list[0]).EleID + "']");
                                    PointF TempPoint2 = TLMath.getUseOffset(((XmlElement)n2).GetAttribute("xlink:href"));
                                    PointF[] ptt2 = new PointF[] { new PointF(((Use)n2).X + TempPoint2.X, ((Use)n2).Y + TempPoint2.Y) };
                                    Transf tran2 = (n2 as Graph).Transform;
                                    tran2.Matrix.TransformPoints(ptt2);
                                    XmlElement line1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                                    line1.SetAttribute("stroke", "#000000");
                                    line1.SetAttribute("layer", ((Layer)tlVectorControl1.SVGDocument.getLayerList()[0]).ID);
                                    line1.SetAttribute("points", ptt[0].X.ToString() + " " + ptt[0].Y.ToString() + "," + ptt2[0].X.ToString() + " " + ptt2[0].Y.ToString());
                                    line1.SetAttribute("FirstNode", ((PSPDEV)p1list[0]).EleID);
                                    line1.SetAttribute("LastNode", ((PSPDEV)p2list[0]).EleID);
                                    XmlNode fn = tlVectorControl1.SVGDocument.RootElement.AppendChild(line1);
                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.SUID = Guid.NewGuid().ToString();
                                    pspDev.EleID = ((Polyline)fn).ID;
                                    pspDev.Name = ((LineInfo)linList[j]).LineName;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    pspDev.X1 = ptt[0].X;
                                    pspDev.Y1 = ptt[0].Y;
                                    pspDev.X2 = ptt2[0].X;
                                    pspDev.Y2 = ptt2[0].Y;
                                    pspDev.LineLength = Convert.ToDouble(((LineInfo)linList[j]).Length);
                                    pspDev.Number = -1;
                                    pspDev.FirstNode = -1;
                                    pspDev.LastNode = -1;
                                    pspDev.Type = "Polyline";
                                    pspDev.LineType = ((LineInfo)linList[j]).LineType;
                                    pspDev.VoltR = Convert.ToDouble(((LineInfo)linList[j]).Voltage);
                                    Services.BaseService.Create<PSPDEV>(pspDev);
                                    tlVectorControl1.SVGDocument.CurrentElement = n2 as SvgElement;
                                    tlVectorControl1.ChangeLevel(LevelType.Top);
                                }
                            }
                        }
                    }
                    if (laglist.Count > 0)
                    {
                        LineInfo line = new LineInfo();
                        line.LineName = " Voltage='" + power + "' and length<>'' and LayerID in (select SUID from SVG_LAYER where yearid='" + ((LayerGrade)laglist[0]).SUID + "') and ObligateField7 ='" + dlg.Name + "' order by LineName";
                        IList linList = Services.BaseService.GetList("SelectLineInfoByWhere", line);
                        for (int j = 0; j < linList.Count; j++)
                        {
                            if (((LineInfo)linList[j]).ObligateField6 != "")
                            {
                                PSPDEV p1 = new PSPDEV();
                                p1.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                p1.Name = ((LineInfo)linList[j]).ObligateField6;
                                p1.Type = "Use";
                                IList p1list = Services.BaseService.GetList("SelectPSPDEVByName", p1);
                                PSPDEV p2 = new PSPDEV();
                                p2.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                p2.Name = ((LineInfo)linList[j]).ObligateField7;
                                p2.Type = "Use";
                                IList p2list = Services.BaseService.GetList("SelectPSPDEVByName", p2);
                                if (p1list.Count > 0 && p2list.Count > 0)
                                {
                                    PointF TempPoint = TLMath.getUseOffset(temp.GetAttribute("xlink:href"));
                                    PointF[] ptt = new PointF[] { new PointF(((Use)temp).X + TempPoint.X, ((Use)temp).Y + TempPoint.Y) };
                                    Transf tran = (temp as Graph).Transform;
                                    tran.Matrix.TransformPoints(ptt);
                                    XmlNode n2 = tlVectorControl1.SVGDocument.SelectSingleNode("//*[@id='" + ((PSPDEV)p1list[0]).EleID + "']");
                                    PointF TempPoint2 = TLMath.getUseOffset(((XmlElement)n2).GetAttribute("xlink:href"));
                                    PointF[] ptt2 = new PointF[] { new PointF(((Use)n2).X + TempPoint2.X, ((Use)n2).Y + TempPoint2.Y) };
                                    Transf tran2 = (n2 as Graph).Transform;
                                    tran2.Matrix.TransformPoints(ptt2);
                                    XmlElement line1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                                    line1.SetAttribute("stroke", "#000000");
                                    line1.SetAttribute("layer", ((Layer)tlVectorControl1.SVGDocument.getLayerList()[0]).ID);
                                    line1.SetAttribute("points", ptt2[0].X.ToString() + " " + ptt2[0].Y.ToString() + "," + ptt[0].X.ToString() + " " + ptt[0].Y.ToString());
                                    line1.SetAttribute("FirstNode", ((PSPDEV)p2list[0]).EleID);
                                    line1.SetAttribute("LastNode", ((PSPDEV)p1list[0]).EleID);
                                    XmlNode fn = tlVectorControl1.SVGDocument.RootElement.AppendChild(line1);
                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.SUID = Guid.NewGuid().ToString();
                                    pspDev.EleID = ((Polyline)fn).ID;
                                    pspDev.Name = ((LineInfo)linList[j]).LineName;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    pspDev.X1 = ptt2[0].X;
                                    pspDev.Y1 = ptt2[0].Y;
                                    pspDev.X2 = ptt[0].X;
                                    pspDev.Y2 = ptt[0].Y;
                                    pspDev.LineLength = Convert.ToDouble(((LineInfo)linList[j]).Length);
                                    pspDev.Number = -1;
                                    pspDev.FirstNode = -1;
                                    pspDev.LastNode = -1;
                                    pspDev.Type = "Polyline";
                                    pspDev.LineType = ((LineInfo)linList[j]).LineType;
                                    pspDev.VoltR = Convert.ToDouble(((LineInfo)linList[j]).Voltage);
                                    Services.BaseService.Create<PSPDEV>(pspDev);
                                    tlVectorControl1.SVGDocument.CurrentElement = n2 as SvgElement;
                                    tlVectorControl1.ChangeLevel(LevelType.Top);
                                }
                            }
                        }

                    }
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.ChangeLevel(LevelType.Top);

                }
                else
                {
                    //tlVectorControl1.Delete();
                    //tlVectorControl1.Dispose();
                    //tlVectorControl1.Undo();
                    XmlNodeList list2 = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
                    foreach (XmlNode node in list2)
                    {
                        PSPDEV dev = new PSPDEV();
                        //(node as Text).InnerText = dev.Name;
                        //XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");

                        PSPDEV dlg11 = new PSPDEV();
                        XmlElement element = node as XmlElement;
                        dev.EleID = element.GetAttribute("id");
                        PSPDEV psp = new PSPDEV();
                        dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        dlg11 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", dev);
                        if (dlg11 == null)
                        {
                            SvgElement element2 = node as SvgElement;
                            tlVectorControl1.SVGDocument.CurrentElement = element2;
                            tlVectorControl1.Delete();
                        }
                    }

                }
            }
            else
            {
                if (temp is Use && (temp.GetAttribute("xlink:href").Contains("dynamotorline")))//接地支路
                {
                    frmFadejie dlg = new frmFadejie(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                    dlg.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {

                        //XmlElement temp = tlVectorControl1.SVGDocument.CurrentElement;                
                        if (temp != null)
                        {
                            PSPDEV pspDev2 = new PSPDEV();
                            XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "dynamotorline";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 1)
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tlVectorControl1.Delete();
                                return;
                            }
                            if (true)
                            {
                                tlVectorControl1.Operation = ToolOperation.Select;
                                PSPDEV pspDev = new PSPDEV();
                                tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                RectangleF t = ((IGraph)temp).GetBounds();
                                pspDev.SUID = Guid.NewGuid().ToString();
                                pspDev.EleID = temp.GetAttribute("id");
                                pspDev.Name = dlg.Name;
                                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                pspDev.X1 = t.X;
                                pspDev.Y1 = t.Y;
                                pspDev.Number = -1;
                                pspDev.FirstNode = -1;
                                pspDev.LastNode = 0;
                                pspDev.Type = "dynamotorline";
                                if (temp.GetAttribute("xlink:href").Contains("dynamotorline"))
                                {
                                    pspDev.Lable = "发电厂支路";
                                }
                                else if (temp.GetAttribute("xlink:href").Contains("gndline"))
                                {
                                    pspDev.Lable = "接地支路";
                                }

                                pspDev.HuganLine1 = dlg.FirstNodeName;
                                pspDev.HuganLine3 = dlg.SwitchStatus;
                                if (dlg.OutP != "")
                                    pspDev.OutP = Convert.ToDouble(dlg.OutP);
                                if (dlg.OutQ != "")
                                    pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                                if (dlg.VoltR != "")
                                    pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                                if (dlg.VoltV != "")
                                    pspDev.VoltV = Convert.ToDouble(dlg.VoltV);
                                if (dlg.PositiveTQ != "")
                                    pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                                if (dlg.NegativeTQ != "")
                                    pspDev.ZeroTQ = Convert.ToDouble(dlg.NegativeTQ);
                                Services.BaseService.Create<PSPDEV>(pspDev);
                            }
                        }
                    }
                    else
                    {
                        tlVectorControl1.Delete();
                    }
                }
                else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("gndline")))//接地支路
                {
                    frmFadejie dlg = new frmFadejie(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                    dlg.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {

                        //XmlElement temp = tlVectorControl1.SVGDocument.CurrentElement;                
                        if (temp != null)
                        {
                            PSPDEV pspDev2 = new PSPDEV();
                            XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "gndline";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 1)
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tlVectorControl1.Delete();
                                return;
                            }
                            if (true)
                            {
                                tlVectorControl1.Operation = ToolOperation.Select;
                                PSPDEV pspDev = new PSPDEV();
                                tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                RectangleF t = ((IGraph)temp).GetBounds();
                                pspDev.SUID = Guid.NewGuid().ToString();
                                pspDev.EleID = temp.GetAttribute("id");
                                pspDev.Name = dlg.Name;
                                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                pspDev.X1 = t.X;
                                pspDev.Y1 = t.Y;
                                pspDev.Number = -1;
                                pspDev.FirstNode = -1;
                                pspDev.LastNode = 0;
                                pspDev.Type = "gndline";
                                if (temp.GetAttribute("xlink:href").Contains("dynamotorline"))
                                {
                                    pspDev.Lable = "发电厂支路";
                                }
                                else if (temp.GetAttribute("xlink:href").Contains("gndline"))
                                {
                                    pspDev.Lable = "接地支路";
                                }

                                pspDev.HuganLine1 = dlg.FirstNodeName;
                                pspDev.HuganLine3 = dlg.SwitchStatus;
                                if (dlg.OutP != "")
                                    pspDev.OutP = Convert.ToDouble(dlg.OutP);
                                if (dlg.OutQ != "")
                                    pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                                if (dlg.VoltR != "")
                                    pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                                if (dlg.VoltV != "")
                                    pspDev.VoltV = Convert.ToDouble(dlg.VoltV);
                                if (dlg.PositiveTQ != "")
                                    pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                                if (dlg.NegativeTQ != "")
                                    pspDev.ZeroTQ = Convert.ToDouble(dlg.NegativeTQ);
                                Services.BaseService.Create<PSPDEV>(pspDev);
                            }
                        }
                    }
                    else
                    {
                        tlVectorControl1.Delete();
                    }
                }
                else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("loadline")))
                {
                    frmLoad dlgLoad = new frmLoad();
                    dlgLoad.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    if (dlgLoad.ShowDialog(this) == DialogResult.OK)
                    {
                        if (temp != null)
                        {
                            PSPDEV pspDev2 = new PSPDEV();
                            XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlgLoad.Name;
                            pspName.Type = "loadline";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 1)
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tlVectorControl1.Delete();
                                return;
                            }

                            tlVectorControl1.Operation = ToolOperation.Select;
                            PSPDEV pspDev = new PSPDEV();
                            tlVectorControl1.ChangeLevel(LevelType.Bottom);
                            RectangleF t = ((IGraph)temp).GetBounds();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = temp.GetAttribute("id");
                            pspDev.Name = dlgLoad.Name;
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.X1 = t.X;
                            pspDev.Y1 = t.Y;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = 0;
                            pspDev.Type = "loadline";

                            pspDev.Lable = "负荷支路";

                            pspDev.HuganLine1 = dlgLoad.FirstNodeName;
                            if (dlgLoad.InPutP != "")
                            {
                                pspDev.InPutP = Convert.ToDouble(dlgLoad.InPutP);
                            }
                            if (dlgLoad.InPutQ != "")
                            {
                                pspDev.InPutQ = Convert.ToDouble(dlgLoad.InPutQ);
                            }
                            if (dlgLoad.VoltR != "")
                            {
                                pspDev.VoltR = Convert.ToDouble(dlgLoad.VoltR);
                            }


                            pspDev.HuganLine3 = dlgLoad.LoadSwitchState;

                            Services.BaseService.Create<PSPDEV>(pspDev);
                        }
                    }
                    else
                    {
                        tlVectorControl1.Delete();
                    }
                }
                else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("transformertwozu")))
                {
                    frmTwoTra dlgTra = new frmTwoTra(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                    dlgTra.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    if (dlgTra.ShowDialog(this) == DialogResult.OK)
                    {
                        PSPDEV pspDev2 = new PSPDEV();
                        XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlgTra.Name;
                        pspName.Type = "transformertwozu";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 1)
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tlVectorControl1.Delete();
                            return;
                        }

                        tlVectorControl1.Operation = ToolOperation.Select;
                        PSPDEV pspDev = new PSPDEV();
                        tlVectorControl1.ChangeLevel(LevelType.Bottom);
                        RectangleF t = ((IGraph)temp).GetBounds();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = temp.GetAttribute("id");
                        pspDev.Name = dlgTra.Name;
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.X1 = t.X;
                        pspDev.Y1 = t.Y;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = 0;
                        pspDev.Type = "transformertwozu";

                        pspDev.Lable = "二绕组变压器";
                        pspDev.HuganLine1 = dlgTra.FirstName;
                        pspDev.HuganLine2 = dlgTra.LastName;
                        pspDev.HuganLine3 = dlgTra.FirstSwitchState;
                        pspDev.HuganLine4 = dlgTra.LastSwitchState;
                        pspDev.LineLevel = dlgTra.FirstType;
                        pspDev.LineType = dlgTra.LastType;

                        if (dlgTra.K != "")
                        {
                            pspDev.K = Convert.ToDouble(dlgTra.K);
                        }
                        if (dlgTra.PositiveR != "")
                        {
                            pspDev.PositiveR = Convert.ToDouble(dlgTra.PositiveR);
                        }
                        if (dlgTra.PositiveTQ != "")
                        {
                            pspDev.PositiveTQ = Convert.ToDouble(dlgTra.PositiveTQ);
                        }
                        if (dlgTra.ZeroR != "")
                        {
                            pspDev.ZeroR = Convert.ToDouble(dlgTra.ZeroR);
                        }
                        if (dlgTra.ZeroTQ != "")
                        {
                            pspDev.ZeroTQ = Convert.ToDouble(dlgTra.ZeroTQ);
                        }
                        if (dlgTra.NeutralNodeTQ != "")
                        {
                            pspDev.BigTQ = Convert.ToDouble(dlgTra.NeutralNodeTQ);
                        }



                        Services.BaseService.Create<PSPDEV>(pspDev);
                    }
                    else
                    {
                        tlVectorControl1.Delete();
                    }
                }
                else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("transformerthirdzu")))
                {
                    frmThridTra dlgThridTra = new frmThridTra(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                    dlgThridTra.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    if (dlgThridTra.ShowDialog(this) == DialogResult.OK)
                    {
                        PSPDEV pspDev2 = new PSPDEV();
                        XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlgThridTra.Name;
                        pspName.Type = "transformerthirdzu";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 1)
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tlVectorControl1.Delete();
                            return;
                        }

                        tlVectorControl1.Operation = ToolOperation.Select;
                        PSPDEV pspDev = new PSPDEV();
                        tlVectorControl1.ChangeLevel(LevelType.Bottom);
                        RectangleF t = ((IGraph)temp).GetBounds();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = temp.GetAttribute("id");
                        pspDev.Name = dlgThridTra.Name;
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.X1 = t.X;
                        pspDev.Y1 = t.Y;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = 0;
                        pspDev.Type = "transformerthirdzu";

                        pspDev.Lable = "三绕组变压器";
                        pspDev.HuganLine1 = dlgThridTra.IName;
                        pspDev.HuganLine2 = dlgThridTra.JName;
                        pspDev.HuganLine3 = dlgThridTra.ISwitchState;
                        pspDev.HuganLine4 = dlgThridTra.JSwitchState;
                        pspDev.LineLevel = dlgThridTra.IType;
                        pspDev.LineType = dlgThridTra.JType;
                        pspDev.LineStatus = dlgThridTra.KType;
                        pspDev.KName = dlgThridTra.KName;
                        pspDev.KSwitchStatus = dlgThridTra.KSwitchState;

                        if (dlgThridTra.IK != "")
                        {
                            pspDev.K = Convert.ToDouble(dlgThridTra.IK);
                        }
                        if (dlgThridTra.JK != "")
                        {
                            pspDev.G = Convert.ToDouble(dlgThridTra.JK);
                        }
                        if (dlgThridTra.KK != "")
                        {
                            pspDev.BigP = Convert.ToDouble(dlgThridTra.KK);
                        }
                        if (dlgThridTra.IR != "")
                        {
                            pspDev.HuganTQ1 = Convert.ToDouble(dlgThridTra.IR);
                        }
                        if (dlgThridTra.JR != "")
                        {
                            pspDev.HuganTQ2 = Convert.ToDouble(dlgThridTra.JR);
                        }
                        if (dlgThridTra.KR != "")
                        {
                            pspDev.HuganTQ3 = Convert.ToDouble(dlgThridTra.KR);
                        }
                        if (dlgThridTra.ITQ != "")
                        {
                            pspDev.HuganTQ4 = Convert.ToDouble(dlgThridTra.ITQ);
                        }
                        if (dlgThridTra.JTQ != "")
                        {
                            pspDev.HuganTQ5 = Convert.ToDouble(dlgThridTra.JTQ);
                        }
                        if (dlgThridTra.KTQ != "")
                        {
                            pspDev.SmallTQ = Convert.ToDouble(dlgThridTra.KTQ);
                        }
                        if (dlgThridTra.ZeroTQ != "")
                        {
                            pspDev.ZeroTQ = Convert.ToDouble(dlgThridTra.ZeroTQ);
                        }
                        if (dlgThridTra.NeutralNodeTQ != "")
                        {
                            pspDev.BigTQ = Convert.ToDouble(dlgThridTra.NeutralNodeTQ);
                        }


                        Services.BaseService.Create<PSPDEV>(pspDev);
                    }
                    else
                    {
                        tlVectorControl1.Delete();
                    }
                }
                else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("串联电容电抗器")))
                {
                    frmCapacity dlgCapacity = new frmCapacity(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                    dlgCapacity.SetEnable(true);
                    dlgCapacity.Text = "串联电容电抗器";
                    dlgCapacity.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    if (dlgCapacity.ShowDialog(this) == DialogResult.OK)
                    {
                        PSPDEV pspDev2 = new PSPDEV();
                        XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlgCapacity.Name;
                        pspName.Type = "串联电容电抗器";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 1)
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tlVectorControl1.Delete();
                            return;
                        }

                        tlVectorControl1.Operation = ToolOperation.Select;
                        PSPDEV pspDev = new PSPDEV();
                        tlVectorControl1.ChangeLevel(LevelType.Bottom);
                        RectangleF t = ((IGraph)temp).GetBounds();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = temp.GetAttribute("id");
                        pspDev.Name = dlgCapacity.Name;
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.X1 = t.X;
                        pspDev.Y1 = t.Y;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = 0;
                        pspDev.Type = "串联电容电抗器";

                        pspDev.Lable = dlgCapacity.Lable;

                        if (dlgCapacity.PositiveTQ != "")
                        {
                            pspDev.PositiveTQ = Convert.ToDouble(dlgCapacity.PositiveTQ);
                        }
                        pspDev.HuganLine1 = dlgCapacity.FirstNodeName;
                        //pspDev.HuganLine2 = dlgCapacity.LastNodeName;

                        Services.BaseService.Create<PSPDEV>(pspDev);
                    }
                    else
                    {
                        tlVectorControl1.Delete();
                    }

                }
                else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("并联电容电抗器")))
                {
                    frmCapacity dlgCapacity = new frmCapacity(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                    dlgCapacity.SetEnable(false);
                    dlgCapacity.Text = "并联电容电抗器";
                    dlgCapacity.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    if (dlgCapacity.ShowDialog(this) == DialogResult.OK)
                    {
                        PSPDEV pspDev2 = new PSPDEV();
                        XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlgCapacity.Name;
                        pspName.Type = "并联电容电抗器";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 1)
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tlVectorControl1.Delete();
                            return;
                        }

                        tlVectorControl1.Operation = ToolOperation.Select;
                        PSPDEV pspDev = new PSPDEV();
                        tlVectorControl1.ChangeLevel(LevelType.Bottom);
                        RectangleF t = ((IGraph)temp).GetBounds();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = temp.GetAttribute("id");
                        pspDev.Name = dlgCapacity.Name;
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.X1 = t.X;
                        pspDev.Y1 = t.Y;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = 0;
                        pspDev.Type = "并联电容电抗器";

                        pspDev.Lable = dlgCapacity.Lable;

                        if (dlgCapacity.PositiveTQ != "")
                        {
                            pspDev.PositiveTQ = Convert.ToDouble(dlgCapacity.PositiveTQ);
                        }
                        pspDev.HuganLine1 = dlgCapacity.FirstNodeName;
                        //pspDev.HuganLine2 = dlgCapacity.LastNodeName;

                        Services.BaseService.Create<PSPDEV>(pspDev);
                    }
                    else
                    {
                        tlVectorControl1.Delete();
                    }
                }
                else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("1/2母联开关")))
                {
                    frmMuLian dlgmulian = new frmMuLian(tlVectorControl1.SVGDocument.CurrentLayer.ID);

                    dlgmulian.Text = "1/2母联开关";
                    dlgmulian.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    if (dlgmulian.ShowDialog(this) == DialogResult.OK)
                    {
                        PSPDEV pspDev2 = new PSPDEV();
                        XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlgmulian.Name;
                        pspName.Type = "1/2母联开关";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 1)
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tlVectorControl1.Delete();
                            return;
                        }

                        tlVectorControl1.Operation = ToolOperation.Select;
                        PSPDEV pspDev = new PSPDEV();
                        tlVectorControl1.ChangeLevel(LevelType.Bottom);
                        RectangleF t = ((IGraph)temp).GetBounds();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = temp.GetAttribute("id");
                        pspDev.Name = dlgmulian.Name;
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.X1 = t.X;
                        pspDev.Y1 = t.Y;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = 0;
                        pspDev.Type = "1/2母联开关";

                        pspDev.Lable = "1/2母联开关";


                        pspDev.HuganLine1 = dlgmulian.FirstNodeName;
                        pspDev.HuganLine2 = dlgmulian.LastNodeName;
                        pspDev.HuganLine3 = dlgmulian.SwitchStatus;

                        Services.BaseService.Create<PSPDEV>(pspDev);
                    }
                    else
                    {
                        tlVectorControl1.Delete();
                    }
                }
                else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("2/3母联开关")))
                {
                    frmMuLian2 dlgmulian = new frmMuLian2(tlVectorControl1.SVGDocument.CurrentLayer.ID);

                    dlgmulian.Text = "2/3母联开关";
                    dlgmulian.svgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    if (dlgmulian.ShowDialog(this) == DialogResult.OK)
                    {
                        PSPDEV pspDev2 = new PSPDEV();
                        XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlgmulian.Name;
                        pspName.Type = "2/3母联开关";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 1)
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tlVectorControl1.Delete();
                            return;
                        }

                        tlVectorControl1.Operation = ToolOperation.Select;
                        PSPDEV pspDev = new PSPDEV();
                        tlVectorControl1.ChangeLevel(LevelType.Bottom);
                        RectangleF t = ((IGraph)temp).GetBounds();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = temp.GetAttribute("id");
                        pspDev.Name = dlgmulian.Name;
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.X1 = t.X;
                        pspDev.Y1 = t.Y;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = 0;
                        pspDev.Type = "2/3母联开关";

                        pspDev.Lable = "2/3母联开关";


                        pspDev.HuganLine1 = dlgmulian.INodeName;
                        pspDev.HuganLine2 = dlgmulian.JNodeName;
                        pspDev.HuganLine3 = dlgmulian.ILineName;
                        pspDev.HuganLine4 = dlgmulian.JLineName;
                        pspDev.KName = dlgmulian.ILoadName;
                        pspDev.KSwitchStatus = dlgmulian.JLoadName;
                        pspDev.LineLevel = dlgmulian.SwitchStatus1;
                        pspDev.LineType = dlgmulian.SwitchStatus2;
                        pspDev.LineStatus = dlgmulian.SwitchStatus3;

                        Services.BaseService.Create<PSPDEV>(pspDev);
                    }
                    else
                    {
                        tlVectorControl1.Delete();
                    }
                }
                //temp.RemoveAll();
            }

        }
        public string getPower(string str)
        {
            if (str.Contains("500"))
            {
                return "500";
            }
            if (str.Contains("220"))
            {
                return "220";
            }
            if (str.Contains("110"))
            {
                return "110";
            }
            if (str.Contains("66"))
            {
                return "66";
            }
            if (str.Contains("35"))
            {
                return "35";
            }
            else
                return "";
        }
        void DrawArea_ViewChanged(object sender, ItopVector.DrawArea.ViewChangedEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            //float a = e.Bounds.Bottom;
        }
        CustomOperation csOperation = CustomOperation.OP_Default;
        bool Reducelineflag = false;
        public void Start()
        {
            Reducelineflag = true;
            this.Show();
            string svguid = ConfigurationSettings.AppSettings.Get("SvgID");
            LoadShape("symbol22.xml");
            jxtbar2(3);
            Open();
            tlVectorControl1.PropertyGrid = propertyGrid;

            tlVectorControl1.ContextMenuStrip = contextMenuStrip1;
            LayerManagerShow();

        }
        public void PowerLossStart()
        {
            this.Show();
            this.Text = "线损计算";
            LoadShape("symbol20.xml");
            jxtbar2(2);
            NewFile(fileType, DialogResult.Ignore);
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.ContextMenuStrip = contextMenuStrip1;
        }
        public void VoltStart()
        {
            this.Show();
            this.Text = "电压质量评估";
            LoadShape("symbol20.xml");
            jxtbar2(1);
            NewFile(fileType, DialogResult.Ignore);
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.ContextMenuStrip = contextMenuStrip1;

        }
        void tlVectorControl1_OperationChanged(object sender, EventArgs e)
        {
            if (csOperation == CustomOperation.OP_MeasureDistance)
            {
                resetOperation();
            }
        }
        void resetOperation()
        {
            csOperation = CustomOperation.OP_Default;
            ItopVector.Core.Figure.Polyline obj = (ItopVector.Core.Figure.Polyline)tlVectorControl1.SVGDocument.CurrentElement;
            if (obj != null)
            {
                obj.ParentNode.RemoveChild(obj);
                tlVectorControl1.SVGDocument.CurrentElement = tlVectorControl1.SVGDocument.RootElement;
                label1.Hide();
                //tlVectorControl1.SetToolTip("");
            }
        }
        void symbolSelector_SelectedChanged(object sender, EventArgs e)
        {
            tlVectorControl1.CurrentOperation = ToolOperation.Select;
            if (symbolSelector.SelectedItem != null)
            {
                tlVectorControl1.DrawArea.PreGraph = symbolSelector.SelectedItem.CloneNode(true) as IGraph;
            }
            else
            {
                tlVectorControl1.DrawArea.PreGraph = null;
            }
        }
        public void LoadShape(string filename)
        {
            DockContainerItem dockitem = dotNetBarManager1.GetItem("DockContainersx") as DockContainerItem;
            dockitem.Control = this.propertyGrid;
            dockitem.Refresh();
            dockitem = dotNetBarManager1.GetItem("DockContainerty") as DockContainerItem;
            symbolSelector = null;
            this.symbolSelector = new ItopVector.Selector.SymbolSelector(System.Windows.Forms.Application.StartupPath + "\\symbol\\" + filename);
            this.symbolSelector.Dock = DockStyle.Fill;
            tlVectorControl1.SymbolSelector = this.symbolSelector;
            dockitem.Control = this.symbolSelector;
            dockitem.Refresh();

            //symbolSelector.SelectedChanged += new EventHandler(symbolSelector_SelectedChanged);
            //symbolSelector.Selected += new EventHandler(symbolSelector_Selected);
            tlVectorControl1.Location = new System.Drawing.Point(176, 90);

            //tlVectorControl1.Size = new Size((Screen.PrimaryScreen.Bounds.Width - 176), (Screen.PrimaryScreen.Bounds.Height - 158));
        }
        void frmlar_OnClickLayer(object sender, Layer lar)
        {
            tlVectorControl1.SVGDocument.SelectCollection.Clear();
            ArrayList a = tlVectorControl1.SVGDocument.getLayerList();
            SvgDocument.currentLayer = lar.ID;
            string larid = lar.ID;

            if (!ChangeLayerList.Contains(larid))
            {
                ChangeLayerList.Add(larid);
            }
        }
        public void Init()
        {
            // progtype = stype;
            ArrayList layerlist = tlVectorControl1.SVGDocument.getLayerList();
            ArrayList tmplaylist = new ArrayList();
            DevExpress.XtraEditors.Controls.CheckedListBoxItem[] chkItems = null;
            //this.checkedListBoxControl1.Items.Clear();

            //if (progtype == "地理信息层")
            //{
            //    for (int i = 0; i < layerlist.Count; i++)
            //    {
            //        Layer lar = (Layer)layerlist[i];
            //        if (lar.GetAttribute("layerType") == progtype)
            //        {
            //            tmplaylist.Add(layerlist[i]);
            //        }
            //        else
            //        {
            //            lar.Visible = false;
            //        }
            //    }
            //}
            //if (progtype == "城市规划层")
            //{
            //    for (int i = 0; i < layerlist.Count; i++)
            //    {
            //        Layer lar = (Layer)layerlist[i];
            //        if (lar.GetAttribute("layerType") == "城市规划层" || lar.GetAttribute("layerType") == "地理信息层")
            //        {
            //            tmplaylist.Add(layerlist[i]);
            //        }
            //        else
            //        {
            //            lar.Visible = false;
            //        }
            //    }
            //}
            //if (progtype == "电网规划层")
            //{
            for (int i = 0; i < layerlist.Count; i++)
            {
                Layer lar = (Layer)layerlist[i];
                tmplaylist.Add(layerlist[i]);
            }
            //}
            //if (MapType == "所内接线图")
            //{
            //    CreateComboBox();
            //    ButtonEnb(true);
            //    LoadImage = false;
            //    bk1.Visible = false;
            //    selLar = "";
            //}
            //chkItems = new DevExpress.XtraEditors.Controls.CheckedListBoxItem[tmplaylist.Count];
            for (int j = 0; j < tmplaylist.Count; j++)
            {
                chkItems.SetValue(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(((Layer)tmplaylist[j]).Label), j);
                if (((Layer)tmplaylist[j]).Visible)
                {
                    chkItems[j].CheckState = CheckState.Checked;
                }
            }
            //this.checkedListBoxControl1.Items.AddRange(chkItems);

            if (tmplaylist.Count > 0)
            {
                Layer lar = (Layer)tmplaylist[0];
                SvgDocument.currentLayer = lar.ID;
                //popupContainerEdit1.Text = lar.Label;
                selLar = lar.Label;
            }
            tlVectorControl1.FullDrawMode = true;
            int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
            if (chose == 1)
            {
                tlVectorControl1.ScaleRatio = 0.01f;

            }
            if (chose == 2)
            {
                tlVectorControl1.ScaleRatio = 0.03125f;

            }
            //tlVectorControl1.ScaleRatio = 0.01f;
            tlVectorControl1.CurrentOperation = ToolOperation.Roam;

            //tlVectorControl1.Refresh();
        }
        public void LayerManagerShow()
        {
            frmlar.Readonly();
            frmlar.Key = "ALL";
            frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
            //if (MapType == "所内接线图")
            //{
            //    frmlar.Progtype = MapType;
            //}
            //else
            //{
            //    frmlar.Progtype = progtype;
            //}
            // frmlar.Owner = this.ParentForm;
            frmlar.OnClickLayer += new ItopVector.Tools.OnClickLayerhandler(frmlar_OnClickLayer);
            //frmlar.OnClickLayer += new OnClickLayerhandler(frmlar_OnClickLayer);
            //frmlar.OnDeleteLayer += new OnDeleteLayerhandler(frmlar_OnDeleteLayer);
            //Init();
            //frmlar.Readonly();
            frmlar.Progtype = "电网规划层";
            frmlar.InitData();
            frmlar.ShowInTaskbar = true;

            frmlar.Owner = this;
            frmlar.Top = 100;//Screen.PrimaryScreen.WorkingArea.Height - 500;
            frmlar.Left = Screen.PrimaryScreen.WorkingArea.Width - frmlar.Width;
            frmlar.Show();
        }
        void symbolSelector_Selected(object sender, EventArgs e)
        {
            tlVectorControl1.CurrentOperation = ToolOperation.Select;
            if (symbolSelector.SelectedItem != null)
            {
                tlVectorControl1.DrawArea.PreGraph = symbolSelector.SelectedItem.CloneNode(true) as IGraph;
            }
            else
            {
                tlVectorControl1.DrawArea.PreGraph = null;
            }
        }
        public void NewFile(bool type, DialogResult result)
        {
            if (result != DialogResult.Ignore && result != DialogResult.No)
            {
                if (result == DialogResult.OK)
                {
                    type = true;
                    fileType = true;
                }
                else
                {
                    type = false;
                    fileType = false;
                }
                //type = false;
                tlVectorControl1.NewFile();
                if (type == true)
                {
                    LoadShape("symbol20.xml");
                    jxtbar(1);
                }
                else
                {
                    LoadShape("symbol21.xml");
                    jxtbar(0);
                }
                tlVectorControl1.SVGDocument.CurrentLayer.ID = Guid.NewGuid().ToString();
                SvgDocument.currentLayer = Layer.CreateNew("默认层", tlVectorControl1.SVGDocument).ID;
                tlVectorControl1.IsModified = false;
                frmElementName dlgnew = new frmElementName();
                dlgnew.TextInput = tlVectorControl1.SVGDocument.FileName;
                if (dlgnew.ShowDialog() == DialogResult.OK)
                {
                    tlVectorControl1.SVGDocument.FileName = dlgnew.TextInput;
                    this.Text = tlVectorControl1.SVGDocument.FileName;
                    this.Refresh();
                    Save();
                }

                if (type == true)
                {
                    //MessageBox.Show();
                    //start st=new start();
                    //if (st.ShowDialog()==DialogResult.OK)
                    //{}
                    if (MessageBox.Show("新建的潮流计算，需要设置基准值，是否立即设置？？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        PSPDEV pspDev2 = new PSPDEV();


                        //pspDev2.SUID = Guid.NewGuid().ToString();
                        pspDev2.Type = "Power";
                        pspDev2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        pspDev2 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", pspDev2);
                        if (pspDev2 != null)
                        {
                        }
                        else
                        {
                            pspDev2 = new PSPDEV();
                            pspDev2.SUID = Guid.NewGuid().ToString();
                            pspDev2.Type = "Power";
                            pspDev2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            Services.BaseService.Create<PSPDEV>(pspDev2);
                        }
                        powerf pp = new powerf(pspDev2);
                        if (pp.ShowDialog() == DialogResult.OK)
                        {
                            pspDev2.PowerFactor = Convert.ToDouble(pp.powerfactor);
                            pspDev2.StandardVolt = Convert.ToDouble(pp.standardvolt);
                            pspDev2.StandardCurrent = Convert.ToDouble(pp.standardcurrent);
                            pspDev2.BigP = Convert.ToDouble(pp.bigP);
                            Services.BaseService.Update<PSPDEV>(pspDev2);
                            PSPDEV voltall = new PSPDEV();
                            voltall.Type = "Use";
                            voltall.Lable = "电厂";
                            voltall.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList allvolt = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", voltall);
                            foreach (PSPDEV dev in allvolt)
                            {
                                dev.OutP = Convert.ToDouble(dev.Burthen) * pspDev2.BigP;
                                //dev.InPutP=dev.Burthen*pspDev2.BigP;
                                dev.OutQ = dev.OutP * Math.Tan(Math.Acos(pspDev2.PowerFactor));
                                Services.BaseService.Update<PSPDEV>(dev);
                            }
                            voltall.Lable = "变电站";
                            allvolt = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", voltall);
                            foreach (PSPDEV dev in allvolt)
                            {
                                dev.InPutP = Convert.ToDouble(dev.Burthen) * pspDev2.BigP;
                                //dev.InPutP=dev.Burthen*pspDev2.BigP;
                                dev.InPutQ = dev.InPutP * pspDev2.BigP * Math.Tan(Math.Acos(pspDev2.PowerFactor));
                                Services.BaseService.Update<PSPDEV>(dev);
                            }

                        }
                        //powerf pf=new powerf()
                    }

                }
            }
            //if (dg.ShowDialog() == DialogResult.Ignore)
            else if (result == DialogResult.Ignore)
            {
                OpenFile dlgOpenFile = new OpenFile(1);
                if (dlgOpenFile.ShowDialog() == DialogResult.OK)
                {
                    if (dlgOpenFile.FileGUID != null)
                    {
                        Open();
                        PSPDEV psp = new PSPDEV();
                        psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUID", psp);
                        foreach (PSPDEV dev in list1)
                        {
                            if (dev.Type != "Power" && dev.Type != "TransformLine")
                            {
                                XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + dev.EleID + "']");
                                if (temp == null)
                                {
                                    Services.BaseService.Delete<PSPDEV>(dev);
                                }
                            }
                        }
                        XmlNodeList list2 = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
                        foreach (XmlNode node in list2)
                        {
                            PSPDEV dev = new PSPDEV();
                            //(node as Text).InnerText = dev.Name;
                            //XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");

                            PSPDEV dlg = new PSPDEV();
                            XmlElement element = node as XmlElement;
                            dev.EleID = element.GetAttribute("id");
                            dev.SvgUID = psp.SvgUID;
                            dlg = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", dev);
                            if (dlg == null)
                            {
                                SvgElement element2 = node as SvgElement;
                                tlVectorControl1.SVGDocument.CurrentElement = element2;
                                tlVectorControl1.Delete();
                            }
                        }
                        XmlNodeList list3 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline");
                        foreach (XmlNode node in list3)
                        {

                            PSPDEV dev = new PSPDEV();
                            //(node as Text).InnerText = dev.Name;
                            //XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");

                            PSPDEV dlg = new PSPDEV();
                            XmlElement element = node as XmlElement;

                            dev.EleID = element.GetAttribute("id");
                            if (element.GetAttribute("flag") == "1")
                            { }
                            else
                            {
                                dev.SvgUID = psp.SvgUID;
                                dlg = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", dev);
                                if (dlg == null)
                                {
                                    SvgElement element2 = node as SvgElement;
                                    tlVectorControl1.SVGDocument.CurrentElement = element2;
                                    tlVectorControl1.Delete();
                                }
                            }
                        }
                    }
                    if (dlgOpenFile.FileType == "短路")
                    {
                        fileType = false;
                        LoadShape("symbol21.xml");
                        jxtbar(0);
                    }
                    else
                    {
                        fileType = true;
                        LoadShape("symbol20.xml");
                        jxtbar(1);
                    }
                }
            }
            else if (result == DialogResult.No)
            {
                OpenFile dlgOpenFile = new OpenFile(1);
                if (dlgOpenFile.ShowDialog() == DialogResult.OK)
                {
                    if (dlgOpenFile.FileGUID != null)
                    {
                        SVGFILE svgFile = new SVGFILE();
                        svgFile.SUID = dlgOpenFile.FileGUID;
                        IList svgList = Services.BaseService.GetList("SelectSVGFILEByKey", svgFile);
                        if (svgList.Count > 0)
                        {
                            svgFile = (SVGFILE)svgList[0];
                            SVGFILE svgNew = new SVGFILE();
                            svgNew.SVGDATA = svgFile.SVGDATA;
                            svgNew.PARENTID = svgFile.PARENTID;
                            svgNew.SUID = Guid.NewGuid().ToString();
                            frmElementName dlgnew = new frmElementName();

                            dlgnew.TextInput = tlVectorControl1.SVGDocument.FileName;
                            if (dlgnew.ShowDialog() == DialogResult.OK)
                            {
                                svgNew.FILENAME = dlgnew.TextInput;
                            }

                            Services.BaseService.Create<SVGFILE>(svgNew);
                            PSPDIR pspDir = new PSPDIR();
                            pspDir.CreateTime = System.DateTime.Now.ToString();
                            pspDir.FileGUID = svgNew.SUID;
                            pspDir.FileName = svgNew.FILENAME;
                            pspDir.FileType = dlgOpenFile.FileType;
                            Services.BaseService.Create<PSPDIR>(pspDir);
                            PSPDEV pspDev = new PSPDEV();
                            pspDev.SvgUID = svgFile.SUID;
                            IList pspList = Services.BaseService.GetList("SelectPSPDEVBySvgUID", pspDev);
                            foreach (PSPDEV dev in pspList)
                            {
                                dev.SvgUID = svgNew.SUID;
                                dev.SUID = Guid.NewGuid().ToString();
                                Services.BaseService.Create<PSPDEV>(dev);
                            }
                            Open();
                            PSPDEV psp = new PSPDEV();
                            psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUID", psp);
                            foreach (PSPDEV dev in list1)
                            {
                                if (dev.Type != "Power" && dev.Type != "TransformLine")
                                {
                                    XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + dev.EleID + "']");
                                    if (temp == null)
                                    {
                                        Services.BaseService.Delete<PSPDEV>(dev);
                                    }
                                }
                            }
                            if (dlgOpenFile.FileType == "短路")
                            {
                                fileType = false;
                                LoadShape("symbol21.xml");
                                jxtbar(0);
                            }
                            else
                            {
                                fileType = true;
                                LoadShape("symbol20.xml");
                                jxtbar(1);
                            }
                        }
                    }
                }
            }
        }

        public void NewFile(bool type)
        {
            start dg = new start();
            //if (dg.ShowDialog() == DialogResult.OK)
            //{

            //    MessageBox.Show("shit");
            //}
            DialogResult result = dg.ShowDialog();
            if (result != DialogResult.Ignore && result != DialogResult.No)
            {
                if (result == DialogResult.OK)
                {
                    type = true;
                    fileType = true;
                }
                else
                {
                    type = false;
                    fileType = false;
                }
                //type = false;
                tlVectorControl1.NewFile();
                if (type == true)
                {
                    LoadShape("symbol20.xml");
                    jxtbar(1);
                }
                else
                {
                    LoadShape("symbol21.xml");
                    jxtbar(0);
                }
                tlVectorControl1.SVGDocument.CurrentLayer.ID = Guid.NewGuid().ToString();
                SvgDocument.currentLayer = Layer.CreateNew("默认层", tlVectorControl1.SVGDocument).ID;
                tlVectorControl1.IsModified = false;
                frmElementName dlgnew = new frmElementName();
                dlgnew.TextInput = tlVectorControl1.SVGDocument.FileName;
                if (dlgnew.ShowDialog() == DialogResult.OK)
                {
                    tlVectorControl1.SVGDocument.FileName = dlgnew.TextInput;
                    this.Text = tlVectorControl1.SVGDocument.FileName;
                    this.Refresh();
                    Save();
                }

                if (type == true)
                {
                    //MessageBox.Show();
                    //start st=new start();
                    //if (st.ShowDialog()==DialogResult.OK)
                    //{}
                    if (MessageBox.Show("新建的潮流计算，需要设置基准值，是否立即设置？？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        PSPDEV pspDev2 = new PSPDEV();


                        //pspDev2.SUID = Guid.NewGuid().ToString();
                        pspDev2.Type = "Power";
                        pspDev2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        pspDev2 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", pspDev2);
                        if (pspDev2 != null)
                        {
                        }
                        else
                        {
                            pspDev2 = new PSPDEV();
                            pspDev2.SUID = Guid.NewGuid().ToString();
                            pspDev2.Type = "Power";
                            pspDev2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            Services.BaseService.Create<PSPDEV>(pspDev2);
                        }
                        powerf pp = new powerf(pspDev2);
                        if (pp.ShowDialog() == DialogResult.OK)
                        {
                            pspDev2.PowerFactor = Convert.ToDouble(pp.powerfactor);
                            pspDev2.StandardVolt = Convert.ToDouble(pp.standardvolt);
                            pspDev2.StandardCurrent = Convert.ToDouble(pp.standardcurrent);
                            pspDev2.BigP = Convert.ToDouble(pp.bigP);
                            Services.BaseService.Update<PSPDEV>(pspDev2);
                            PSPDEV voltall = new PSPDEV();
                            voltall.Type = "Use";
                            voltall.Lable = "电厂";
                            voltall.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList allvolt = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", voltall);
                            foreach (PSPDEV dev in allvolt)
                            {
                                dev.OutP = Convert.ToDouble(dev.Burthen) * pspDev2.BigP;
                                //dev.InPutP=dev.Burthen*pspDev2.BigP;
                                dev.OutQ = dev.OutP * Math.Tan(Math.Acos(pspDev2.PowerFactor));
                                Services.BaseService.Update<PSPDEV>(dev);
                            }
                            voltall.Lable = "变电站";
                            allvolt = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", voltall);
                            foreach (PSPDEV dev in allvolt)
                            {
                                dev.InPutP = Convert.ToDouble(dev.Burthen) * pspDev2.BigP;
                                //dev.InPutP=dev.Burthen*pspDev2.BigP;
                                dev.InPutQ = dev.InPutP * pspDev2.BigP * Math.Tan(Math.Acos(pspDev2.PowerFactor));
                                Services.BaseService.Update<PSPDEV>(dev);
                            }

                        }
                        //powerf pf=new powerf()
                    }

                }
            }
            //if (dg.ShowDialog() == DialogResult.Ignore)
            else if (result == DialogResult.Ignore)
            {
                OpenFile dlgOpenFile = new OpenFile(1);
                if (dlgOpenFile.ShowDialog() == DialogResult.OK)
                {
                    if (dlgOpenFile.FileGUID != null)
                    {
                        Open();
                        PSPDEV psp = new PSPDEV();
                        psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUID", psp);
                        foreach (PSPDEV dev in list1)
                        {
                            if (dev.Type != "Power" && dev.Type != "TransformLine")
                            {
                                XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + dev.EleID + "']");
                                if (temp == null)
                                {
                                    Services.BaseService.Delete<PSPDEV>(dev);
                                }
                            }
                        }
                        XmlNodeList list2 = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
                        foreach (XmlNode node in list2)
                        {
                            PSPDEV dev = new PSPDEV();
                            //(node as Text).InnerText = dev.Name;
                            //XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");

                            PSPDEV dlg = new PSPDEV();
                            XmlElement element = node as XmlElement;
                            dev.EleID = element.GetAttribute("id");
                            dev.SvgUID = psp.SvgUID;
                            dlg = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", dev);
                            if (dlg == null)
                            {
                                SvgElement element2 = node as SvgElement;
                                tlVectorControl1.SVGDocument.CurrentElement = element2;
                                tlVectorControl1.Delete();
                            }
                        }
                        XmlNodeList list3 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline");
                        foreach (XmlNode node in list3)
                        {

                            PSPDEV dev = new PSPDEV();
                            //(node as Text).InnerText = dev.Name;
                            //XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");

                            PSPDEV dlg = new PSPDEV();
                            XmlElement element = node as XmlElement;

                            dev.EleID = element.GetAttribute("id");
                            if (element.GetAttribute("flag") == "1")
                            { }
                            else
                            {
                                dev.SvgUID = psp.SvgUID;
                                dlg = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", dev);
                                if (dlg == null)
                                {
                                    SvgElement element2 = node as SvgElement;
                                    tlVectorControl1.SVGDocument.CurrentElement = element2;
                                    tlVectorControl1.Delete();
                                }
                            }
                        }
                    }
                    if (dlgOpenFile.FileType == "短路")
                    {
                        fileType = false;
                        LoadShape("symbol21.xml");
                        jxtbar(0);
                    }
                    else
                    {
                        fileType = true;
                        LoadShape("symbol20.xml");
                        jxtbar(1);
                    }
                }
            }
            else if (result == DialogResult.No)
            {
                OpenFile dlgOpenFile = new OpenFile(1);
                if (dlgOpenFile.ShowDialog() == DialogResult.OK)
                {
                    if (dlgOpenFile.FileGUID != null)
                    {
                        SVGFILE svgFile = new SVGFILE();
                        svgFile.SUID = dlgOpenFile.FileGUID;
                        IList svgList = Services.BaseService.GetList("SelectSVGFILEByKey", svgFile);
                        if (svgList.Count > 0)
                        {
                            svgFile = (SVGFILE)svgList[0];
                            SVGFILE svgNew = new SVGFILE();
                            svgNew.SVGDATA = svgFile.SVGDATA;
                            svgNew.PARENTID = svgFile.PARENTID;
                            svgNew.SUID = Guid.NewGuid().ToString();
                            frmElementName dlgnew = new frmElementName();

                            dlgnew.TextInput = tlVectorControl1.SVGDocument.FileName;
                            if (dlgnew.ShowDialog() == DialogResult.OK)
                            {
                                svgNew.FILENAME = dlgnew.TextInput;
                            }

                            Services.BaseService.Create<SVGFILE>(svgNew);
                            PSPDIR pspDir = new PSPDIR();
                            pspDir.CreateTime = System.DateTime.Now.ToString();
                            pspDir.FileGUID = svgNew.SUID;
                            pspDir.FileName = svgNew.FILENAME;
                            pspDir.FileType = dlgOpenFile.FileType;
                            Services.BaseService.Create<PSPDIR>(pspDir);
                            PSPDEV pspDev = new PSPDEV();
                            pspDev.SvgUID = svgFile.SUID;
                            IList pspList = Services.BaseService.GetList("SelectPSPDEVBySvgUID", pspDev);
                            foreach (PSPDEV dev in pspList)
                            {
                                dev.SvgUID = svgNew.SUID;
                                dev.SUID = Guid.NewGuid().ToString();
                                Services.BaseService.Create<PSPDEV>(dev);
                            }
                            Open();
                            PSPDEV psp = new PSPDEV();
                            psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUID", psp);
                            foreach (PSPDEV dev in list1)
                            {
                                if (dev.Type != "Power" && dev.Type != "TransformLine")
                                {
                                    XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + dev.EleID + "']");
                                    if (temp == null)
                                    {
                                        Services.BaseService.Delete<PSPDEV>(dev);
                                    }
                                }
                            }
                            if (dlgOpenFile.FileType == "短路")
                            {
                                fileType = false;
                                LoadShape("symbol21.xml");
                                jxtbar(0);
                            }
                            else
                            {
                                fileType = true;
                                LoadShape("symbol20.xml");
                                jxtbar(1);
                            }
                        }
                    }
                }
            }
        }
        public void NewFile(bool type, string str)
        {
            start dg = new start();
            dg.Text = str;
            //if (dg.ShowDialog() == DialogResult.OK)
            //{

            //    MessageBox.Show("shit");
            //}
            DialogResult result = dg.ShowDialog();
            if (result != DialogResult.Ignore && result != DialogResult.No)
            {
                if (result == DialogResult.OK)
                {
                    type = true;
                    fileType = true;
                }
                else
                {
                    type = false;
                    fileType = false;
                }
                //type = false;
                tlVectorControl1.NewFile();
                if (type == true)
                {
                    LoadShape("symbol20.xml");
                    jxtbar(1);
                }
                else
                {
                    LoadShape("symbol21.xml");
                    jxtbar(0);
                }
                tlVectorControl1.SVGDocument.CurrentLayer.ID = Guid.NewGuid().ToString();
                SvgDocument.currentLayer = Layer.CreateNew("默认层", tlVectorControl1.SVGDocument).ID;
                tlVectorControl1.IsModified = false;
                frmElementName dlgnew = new frmElementName();
                dlgnew.TextInput = tlVectorControl1.SVGDocument.FileName;
                if (dlgnew.ShowDialog() == DialogResult.OK)
                {
                    tlVectorControl1.SVGDocument.FileName = dlgnew.TextInput;
                    this.Text = tlVectorControl1.SVGDocument.FileName;
                    this.Refresh();
                    Save();
                }

                if (type == true)
                {
                    //MessageBox.Show();
                    //start st=new start();
                    //if (st.ShowDialog()==DialogResult.OK)
                    //{}
                    if (MessageBox.Show("新建的潮流计算，需要设置基准值，是否立即设置？？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        PSPDEV pspDev2 = new PSPDEV();


                        //pspDev2.SUID = Guid.NewGuid().ToString();
                        pspDev2.Type = "Power";
                        pspDev2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        pspDev2 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", pspDev2);
                        if (pspDev2 != null)
                        {
                        }
                        else
                        {
                            pspDev2 = new PSPDEV();
                            pspDev2.SUID = Guid.NewGuid().ToString();
                            pspDev2.Type = "Power";
                            pspDev2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            Services.BaseService.Create<PSPDEV>(pspDev2);
                        }
                        powerf pp = new powerf(pspDev2);
                        if (pp.ShowDialog() == DialogResult.OK)
                        {
                            pspDev2.PowerFactor = Convert.ToDouble(pp.powerfactor);
                            pspDev2.StandardVolt = Convert.ToDouble(pp.standardvolt);
                            pspDev2.StandardCurrent = Convert.ToDouble(pp.standardcurrent);
                            pspDev2.BigP = Convert.ToDouble(pp.bigP);
                            Services.BaseService.Update<PSPDEV>(pspDev2);
                            PSPDEV voltall = new PSPDEV();
                            voltall.Type = "Use";
                            voltall.Lable = "电厂";
                            voltall.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList allvolt = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", voltall);
                            foreach (PSPDEV dev in allvolt)
                            {
                                dev.OutP = Convert.ToDouble(dev.Burthen) * pspDev2.BigP;
                                //dev.InPutP=dev.Burthen*pspDev2.BigP;
                                dev.OutQ = dev.OutP * Math.Tan(Math.Acos(pspDev2.PowerFactor));
                                Services.BaseService.Update<PSPDEV>(dev);
                            }
                            voltall.Lable = "变电站";
                            allvolt = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", voltall);
                            foreach (PSPDEV dev in allvolt)
                            {
                                dev.InPutP = Convert.ToDouble(dev.Burthen) * pspDev2.BigP;
                                //dev.InPutP=dev.Burthen*pspDev2.BigP;
                                dev.InPutQ = dev.InPutP * pspDev2.BigP * Math.Tan(Math.Acos(pspDev2.PowerFactor));
                                Services.BaseService.Update<PSPDEV>(dev);
                            }

                        }
                        //powerf pf=new powerf()
                    }

                }
            }
            //if (dg.ShowDialog() == DialogResult.Ignore)
            else if (result == DialogResult.Ignore)
            {
                OpenFile dlgOpenFile = new OpenFile(1);
                if (dlgOpenFile.ShowDialog() == DialogResult.OK)
                {
                    if (dlgOpenFile.FileGUID != null)
                    {
                        Open();
                        PSPDEV psp = new PSPDEV();
                        psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUID", psp);
                        foreach (PSPDEV dev in list1)
                        {
                            if (dev.Type != "Power" && dev.Type != "TransformLine")
                            {
                                XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + dev.EleID + "']");
                                if (temp == null)
                                {
                                    Services.BaseService.Delete<PSPDEV>(dev);
                                }
                            }
                        }
                        XmlNodeList list2 = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
                        foreach (XmlNode node in list2)
                        {
                            PSPDEV dev = new PSPDEV();
                            //(node as Text).InnerText = dev.Name;
                            //XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");

                            PSPDEV dlg = new PSPDEV();
                            XmlElement element = node as XmlElement;
                            dev.EleID = element.GetAttribute("id");
                            dev.SvgUID = psp.SvgUID;
                            dlg = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", dev);
                            if (dlg == null)
                            {
                                SvgElement element2 = node as SvgElement;
                                tlVectorControl1.SVGDocument.CurrentElement = element2;
                                tlVectorControl1.Delete();
                            }
                        }
                        XmlNodeList list3 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline");
                        foreach (XmlNode node in list3)
                        {

                            PSPDEV dev = new PSPDEV();
                            //(node as Text).InnerText = dev.Name;
                            //XmlNodeList element = tlVectorControl1.SVGDocument.GetElementsByTagName("text");

                            PSPDEV dlg = new PSPDEV();
                            XmlElement element = node as XmlElement;

                            dev.EleID = element.GetAttribute("id");
                            if (element.GetAttribute("flag") == "1")
                            { }
                            else
                            {
                                dev.SvgUID = psp.SvgUID;
                                dlg = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", dev);
                                if (dlg == null)
                                {
                                    SvgElement element2 = node as SvgElement;
                                    tlVectorControl1.SVGDocument.CurrentElement = element2;
                                    tlVectorControl1.Delete();
                                }
                            }
                        }
                    }
                    if (dlgOpenFile.FileType == "短路")
                    {
                        fileType = false;
                        LoadShape("symbol21.xml");
                        jxtbar(0);
                    }
                    else
                    {
                        fileType = true;
                        LoadShape("symbol20.xml");
                        jxtbar(1);
                    }
                }
            }
            else if (result == DialogResult.No)
            {
                OpenFile dlgOpenFile = new OpenFile(1);
                if (dlgOpenFile.ShowDialog() == DialogResult.OK)
                {
                    if (dlgOpenFile.FileGUID != null)
                    {
                        SVGFILE svgFile = new SVGFILE();
                        svgFile.SUID = dlgOpenFile.FileGUID;
                        IList svgList = Services.BaseService.GetList("SelectSVGFILEByKey", svgFile);
                        if (svgList.Count > 0)
                        {
                            svgFile = (SVGFILE)svgList[0];
                            SVGFILE svgNew = new SVGFILE();
                            svgNew.SVGDATA = svgFile.SVGDATA;
                            svgNew.PARENTID = svgFile.PARENTID;
                            svgNew.SUID = Guid.NewGuid().ToString();
                            frmElementName dlgnew = new frmElementName();

                            dlgnew.TextInput = tlVectorControl1.SVGDocument.FileName;
                            if (dlgnew.ShowDialog() == DialogResult.OK)
                            {
                                svgNew.FILENAME = dlgnew.TextInput;
                            }

                            Services.BaseService.Create<SVGFILE>(svgNew);
                            PSPDIR pspDir = new PSPDIR();
                            pspDir.CreateTime = System.DateTime.Now.ToString();
                            pspDir.FileGUID = svgNew.SUID;
                            pspDir.FileName = svgNew.FILENAME;
                            pspDir.FileType = dlgOpenFile.FileType;
                            Services.BaseService.Create<PSPDIR>(pspDir);
                            PSPDEV pspDev = new PSPDEV();
                            pspDev.SvgUID = svgFile.SUID;
                            IList pspList = Services.BaseService.GetList("SelectPSPDEVBySvgUID", pspDev);
                            foreach (PSPDEV dev in pspList)
                            {
                                dev.SvgUID = svgNew.SUID;
                                dev.SUID = Guid.NewGuid().ToString();
                                Services.BaseService.Create<PSPDEV>(dev);
                            }
                            Open();
                            PSPDEV psp = new PSPDEV();
                            psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUID", psp);
                            foreach (PSPDEV dev in list1)
                            {
                                if (dev.Type != "Power" && dev.Type != "TransformLine")
                                {
                                    XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + dev.EleID + "']");
                                    if (temp == null)
                                    {
                                        Services.BaseService.Delete<PSPDEV>(dev);
                                    }
                                }
                            }
                            if (dlgOpenFile.FileType == "短路")
                            {
                                fileType = false;
                                LoadShape("symbol21.xml");
                                jxtbar(0);
                            }
                            else
                            {
                                fileType = true;
                                LoadShape("symbol20.xml");
                                jxtbar(1);
                            }
                        }
                    }
                }
            }
        }
        private void deltall(string svguid)
        {
            //删除掉原来的所有元素


            XmlNode svgnode = tlVectorControl1.SVGDocument.SelectSingleNode("svg");
            XmlNodeList x2 = svgnode.SelectNodes("* [@layer='" + svguid + "']");
            for (int i = 0; i < x2.Count; i++)
            {
                XmlNode xnode = x2[i];
                svgnode.RemoveChild(xnode);
            }
            PSPDEV psd = new PSPDEV();
            psd.SvgUID = svguid;
            IList listps = Services.BaseService.GetList("SelectPSPDEVBySvgUID", psd);
            for (int i = 0; i < listps.Count; i++)
            {
                Services.BaseService.Delete<PSPDEV>((PSPDEV)listps[i]);
            }
        }
        public bool zhengtiflag = false, jiqiflag = false, zhongqiflag = false, yuanqiflag = false;    //记录操作流程进行减线法的时候必须先整体、近期、中期、远期的顺序进行
        private void dotNetBarManager1_ItemClick(object sender, EventArgs e)
        {
            FileStream dh;
            StreamReader readLine;
            char[] charSplit;
            string strLine;
            string[] array1;
            string output = null;
            string[] array2;

            string strLine2;

            char[] charSplit2 = new char[] { ' ' };
            FileStream op;
            StreamWriter str1;
            FileStream dh2;
            StreamReader readLine2;
            Excel.Application ex;
            Excel.Worksheet xSheet;
            Excel.Application result1;
            Excel.Worksheet tempSheet;
            Excel.Worksheet newWorksheet;
            DevComponents.DotNetBar.ButtonItem btItem = sender as DevComponents.DotNetBar.ButtonItem;
            //Layer layer1 = (Layer)LayerBox.ComboBoxEx.SelectedItem;
            if (btItem != null)
            {
                switch (btItem.Name)
                {
                    #region 文件操作
                    case "mNew":                     
                        break;
                    case "mOpen":
                        if (tlVectorControl1.IsModified == true)
                        {
                            DialogResult a;
                            a = MessageBox.Show("图形已修改，是否保存?", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                            if (a == DialogResult.Yes)
                            {
                                Save();
                            }
                            else if (a == DialogResult.No)
                            {
                            }
                            else if (a == DialogResult.Cancel)
                            {
                                return;
                            }

                        }
                        Open();

                        break;
                    case "btExSymbol":
                        tlVectorControl1.ExportSymbol();
                        break;
                    case "mjxt"://导入接线图
                        string _svguid = ConfigurationSettings.AppSettings.Get("SvgID");
                        frmYear f = new frmYear();
                        f.uid = _svguid;
                        f.Show();
                        //ImportJxt jxt = new ImportJxt(tlVectorControl1);
                        //jxt.Import();
                        break;
                    case "mSave":
                        SaveAllLayer();

                        break;
                    case "mExit":
                        this.Close();
                        break;
                    case "bt1":
                        //InitTK();
                        break;
                    case "mFin":
                        frmGProList p = new frmGProList();
                        p.Show();
                        p.LoadData(LoadData());
                        break;
                    case "bt2":
                        break;
                    case "mPriSet":
                        this.tlVectorControl1.Operation = ToolOperation.InterEnclosurePrint;
                        break;
                    case "mPrint":
                        tlVectorControl1.Print();
                        break;
                    case "mImport":
                        ExportImage();
                        break;
                    case "mView":
                        //frmSvgView fView = new frmSvgView();
                        //fView.Open(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        //fView.Show();

                        break;


                    //case "mIncreaseView":
                    //    tlVectorControl1.Operation = ToolOperation.IncreaseView;
                    //    break;
                    case "mRzb":
                        frmRatio fRat = new frmRatio();
                        string viewRat = tlVectorControl1.SVGDocument.getRZBRatio();
                        if (viewRat != "")
                        {
                            fRat.InitData(viewRat);
                        }
                        if (fRat.ShowDialog() == DialogResult.OK)
                        {
                            viewRat = fRat.ViewScale;
                            tlVectorControl1.SVGDocument.setRZBRatio(viewRat);
                        }
                        break;

                    case "mAbout":

                        frmAbout frma = new frmAbout();
                        frma.ShowDialog();
                        break;

                    case "ButtonItem10":
                        int temp411 = 10;
                        frmConvert frmc = new frmConvert();
                        frmc.ShowDialog();
                        temp411++;
                        break;

                    //基础操作
                    case "mFreeTransform":
                        tlVectorControl1.Operation = ToolOperation.FreeTransform;

                        break;
                    case "mCJ":
                        tlVectorControl1.Operation = ToolOperation.PolyLine;
                        csOperation = CustomOperation.OP_MeasureDistance;
                        break;
                    //case "ButtonItem2":
                    //    break;
                    #endregion
                    #region 基础图元
                    case "mDecreaseView":
                        tlVectorControl1.Operation = ToolOperation.DecreaseView;

                        break;
                    case "mIncreaseView":

                        tlVectorControl1.Operation = ToolOperation.IncreaseView;
                        break;
                    case "mRoam":
                        tlVectorControl1.Operation = ToolOperation.Roam;

                        break;
                    case "mSelect":
                        tlVectorControl1.Operation = ToolOperation.Select;
                        break;
                    case "mSel":

                        tlVectorControl1.Operation = ToolOperation.FreeTransform;
                        break;
                    //case "mFreeTransform":
                    //    tlVectorControl1.Operation = ToolOperation.FreeTransform;

                    //    break;
                    case "mFreeLines"://锁套
                        tlVectorControl1.Operation = ToolOperation.FreeLines;

                        break;
                    case "mFreePath":
                        tlVectorControl1.Operation = ToolOperation.FreePath;

                        break;
                    case "mShapeTransform":
                        tlVectorControl1.Operation = ToolOperation.ShapeTransform;

                        break;
                    case "mAngleRectangle":
                        tlVectorControl1.Operation = ToolOperation.AngleRectangle;

                        break;
                    case "mEllipse":
                        tlVectorControl1.Operation = ToolOperation.Ellipse;

                        break;
                    case "mLine":
                        tlVectorControl1.Operation = ToolOperation.ConnectLine_Polyline;

                        break;
                    case "mPolyline":
                        tlVectorControl1.Operation = ToolOperation.PolyLine;

                        break;
                    case "mConnectLine":
                        tlVectorControl1.Operation = ToolOperation.ConnectLine;
                        break;

                    case "mPolygon":
                        tlVectorControl1.Operation = ToolOperation.Polygon;

                        break;
                    case "mImage":
                        tlVectorControl1.Operation = ToolOperation.Image;
                        break;
                    case "mText":
                        tlVectorControl1.Operation = ToolOperation.Text;
                        break;
                    case "mBezier":
                        tlVectorControl1.Operation = ToolOperation.Bezier;

                        break;
                    case "ButtonItem2":
                        fileType = true;
                        if (fileType == true)
                        {
                            LoadShape("symbol20.xml");
                            //jxtbar(1);
                        }
                        else
                        {
                            LoadShape("symbol21.xml");
                            //jxtbar(0);
                        }
                        tlVectorControl1.SVGDocument.CurrentLayer.ID = Guid.NewGuid().ToString();
                        SvgDocument.currentLayer = Layer.CreateNew("默认层", tlVectorControl1.SVGDocument).ID;
                        tlVectorControl1.IsModified = false;
                        frmElementName dlgnew2 = new frmElementName();
                        dlgnew2.TextInput = tlVectorControl1.SVGDocument.FileName;
                        if (dlgnew2.ShowDialog() == DialogResult.OK)
                        {
                            tlVectorControl1.SVGDocument.FileName = dlgnew2.TextInput;
                            Save();
                        }
                        //NewFile(fileType);
                        break;
                    case "ButtonItem8":
                        fileType = false;
                        //NewFile(fileType);
                        tlVectorControl1.NewFile();
                        if (fileType == true)
                        {
                            LoadShape("symbol20.xml");
                            //jxtbar(1);
                        }
                        else
                        {
                            LoadShape("symbol21.xml");
                            //jxtbar(0);
                        }
                        tlVectorControl1.SVGDocument.CurrentLayer.ID = Guid.NewGuid().ToString();
                        SvgDocument.currentLayer = Layer.CreateNew("默认层", tlVectorControl1.SVGDocument).ID;
                        tlVectorControl1.IsModified = false;
                        frmElementName dlgnew3 = new frmElementName();
                        dlgnew3.TextInput = tlVectorControl1.SVGDocument.FileName;
                        if (dlgnew3.ShowDialog() == DialogResult.OK)
                        {
                            tlVectorControl1.SVGDocument.FileName = dlgnew3.TextInput;
                            Save();
                        }
                        break;
                    case "mCheck":
                        Check();
                        break;
                    case "niula":
                        //MessageBox.Show(Directory.GetCurrentDirectory());
                        //frmTLpsp el = new frmTLpsp();
                        PspNIULA();
                        //oThread = new Thread(new ThreadStart(el.PspNIULA));
                        //oThread.Start();                        
                        //try
                        //{
                        //    time = new System.Threading.Timer(new TimerCallback(method), null, 50000, 60000);
                        //}
                        //catch { }
                        break;
                    case "pq":
                        //frmTLpsp e2 = new frmTLpsp();
                        //PspPQ();
                        break;
                    //case "ShortCut":
                    //    ShortCutCheck();
                    //    break;
                    case "GaussSeidel":
                       // PspGaussSeidel();
                        break;
                    case "PowerLossCal":
                        //PspPowerLossCal();
                        break;
                    case "N_RZYz":
                        PspN_RZYz();
                        break;
                    case "WebRela":                        //进行网络N-1检验
                        //WebCalAndPrint();
                        break;
                    case "TransRela":                       //进行变压器N-1检验
                        break;
                    case "DuanluResult":
                        break;
                    case "dd":
                        //SubPrint = true;
                        tlVectorControl1.Operation = ToolOperation.InterEnclosurePrint;
                        break;

                    case "NiulaResult":
                        try
                        {
                            //{
                            if (!Check())
                            {
                                return;
                            }
                            NIULA pspniula = new NIULA();
                            pspniula.CurrentCal();

                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "牛拉法计算结果.xls"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "牛拉法计算结果.xls");
                                //OpenRead(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + ".xls");
                            }

                            double yinzi = 0, capability = 0, volt = 0, current = 0, standvolt = 0, Rad_to_Deg = 57.29577951;
                            PSPDEV benchmark = new PSPDEV();
                            benchmark.Type = "power";
                            benchmark.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
                            if (list3 == null)
                            {
                                MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            foreach (PSPDEV dev in list3)
                            {
                                yinzi = Convert.ToDouble(dev.PowerFactor);
                                capability = Convert.ToDouble(dev.StandardCurrent);
                                volt = Convert.ToDouble(dev.StandardVolt);
                                if (dev.PowerFactor == 0)
                                {
                                    yinzi = 1;
                                }
                                if (dev.StandardCurrent == 0)
                                {
                                    capability = 1;
                                }
                                if (dev.StandardVolt == 0)
                                {
                                    volt = 1;
                                }
                                standvolt = volt;
                                current = capability / (Math.Sqrt(3) * volt);

                            }
                            capability = 100;

                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF1.txt"))
                            {
                            }
                            else
                            {
                                return;
                            }
                            dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF1.txt", FileMode.Open);
                            readLine = new StreamReader(dh);

                            charSplit = new char[] { ' ' };
                            strLine = readLine.ReadLine();

                            output += ("全网母线(发电、负荷)结果报表 " + "\r\n" + "\r\n");
                            output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
                            output += ("母线名" + "," + "电压幅值" + "," + "电压相角" + "," + "有功发电" + "," + "无功发电" + "," + "有功负荷" + "," + "无功负荷" + "," + "越限标志" + "," + "过载标志" + "\r\n");
                            int count = 0;
                            while (strLine != null && strLine != "")
                            {
                                array1 = strLine.Split(charSplit);
                                string[] dev = new string[9];
                                dev.Initialize();
                                int i = 0;
                                count++;
                                PSPDEV CR = new PSPDEV();
                                CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

                                foreach (string str in array1)
                                {
                                    if (str != "")
                                    {
                                        if (str != "NaN")
                                        {
                                            dev[i++] = Convert.ToDouble(str).ToString();
                                        }
                                        else
                                        {
                                            dev[i++] = str;
                                        }

                                    }

                                }

                                CR.Number = Convert.ToInt32(dev[0]);
                                CR.Type = "Use";
                                CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByNumberAndSvgUIDAndType", CR);
                                if (CR.ReferenceVolt != null && CR.ReferenceVolt != 0)
                                {
                                    volt = CR.ReferenceVolt;
                                }
                                else
                                    volt = standvolt;
                                current = capability / (Math.Sqrt(3) * volt);
                                double vTemp = Convert.ToDouble(dev[1]) * volt;
                                double vTemp1 = volt * 95 / 100;
                                double vTemp2 = volt * 105 / 100;

                                if (vTemp >= vTemp1 && vTemp <= vTemp2)
                                {
                                    dev[5] = "0";
                                }
                                else
                                {
                                    dev[5] = "1";
                                }
                                if (Convert.ToDouble(dev[3]) * capability > Convert.ToDouble(CR.Burthen))
                                {
                                    dev[6] = "1";
                                }
                                else
                                {
                                    dev[6] = "0";
                                }

                                if (Convert.ToDouble(dev[3]) < 0)
                                {
                                    output += CR.Name + "," + (Convert.ToDouble(dev[1]) * volt).ToString() + "," + (Convert.ToDouble(dev[2]) * Rad_to_Deg).ToString() + "," + "0" + "," + "0" + "," + (Convert.ToDouble(dev[3]) * capability).ToString() + "," + (Convert.ToDouble(dev[4]) * capability).ToString() + "," + dev[5] + "," + dev[6] + "\r\n";
                                }
                                else
                                {
                                    output += CR.Name + "," + (Convert.ToDouble(dev[1]) * volt).ToString() + "," + (Convert.ToDouble(dev[2]) * Rad_to_Deg).ToString() + "," + (Convert.ToDouble(dev[3]) * capability).ToString() + "," + (Convert.ToDouble(dev[4]) * capability).ToString() + "," + "0" + "," + "0" + "," + dev[5] + "," + dev[6] + "\r\n";
                                }
                                strLine = readLine.ReadLine();
                            }
                            PSPDEV ct = new PSPDEV();
                            ct.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            ct.Type = "Use";
                            IList cont = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", ct);
                            if (count < cont.Count)
                            {
                                MessageBox.Show("请进行潮流计算后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                readLine.Close();
                                return;

                            }
                            readLine.Close();
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                            }
                            op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
                            str1 = new StreamWriter(op, Encoding.GetEncoding("GB2312"));
                            str1.Write(output);
                            str1.Close();

                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                            {
                            }
                            else
                            {
                                return;
                            }
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH1.txt"))
                            {
                            }
                            else
                            {
                                return;
                            }
                            dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);
                            dh2 = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH1.txt", FileMode.Open);
                            readLine2 = new StreamReader(dh2);
                            readLine = new StreamReader(dh);
                            charSplit = new char[] { ' ' };
                            strLine = readLine.ReadLine();
                            strLine2 = readLine2.ReadLine();

                            output = null;

                            output += ("全网交流线结果报表" + "\r\n" + "\r\n");
                            output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
                            output += ("支路名称" + "," + "支路有功" + "," + "支路无功" + "," + "有功损耗" + "," + "无功损耗" + "," + "电流幅值" + "," + "电流相角" + "," + "越限标志" + "," + "\r\n");
                            while (strLine != null && strLine2 != null && strLine != "" && strLine2 != "")
                            {
                                array1 = strLine.Split(charSplit);
                                array2 = strLine2.Split(charSplit2);

                                string[] dev = new string[20];
                                dev.Initialize();
                                int i = 0;
                                PSPDEV CR = new PSPDEV();
                                CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

                                foreach (string str in array1)
                                {
                                    if (str != "")
                                    {
                                        if (i == 0)
                                        {
                                            dev[i++] = str.ToString();
                                        }
                                        else
                                        {
                                            if (str != "NaN")
                                            {
                                                dev[i++] = Convert.ToDouble(str).ToString();
                                            }
                                            else
                                            {
                                                dev[i++] = str;
                                            }

                                        }
                                    }

                                }
                                i = 7;
                                for (int j = 3; j < 5; j++)
                                {
                                    if (array2[j] != "")
                                    {
                                        if (array2[j] != "NaN")
                                        {
                                            dev[i++] = Convert.ToDouble(array2[j]).ToString();
                                        }
                                        else
                                        {
                                            dev[i++] = array2[j];
                                        }
                                    }

                                }
                                CR.Name = dev[0];
                                CR.Type = "Polyline";
                                CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", CR);
                                if (CR.ReferenceVolt != null && CR.ReferenceVolt != 0)
                                {
                                    volt = CR.ReferenceVolt;
                                }
                                else
                                    volt = standvolt;
                                current = capability / (Math.Sqrt(3) * volt);
                                if (CR != null)
                                {
                                    if (Convert.ToDouble(dev[7]) * current * 1000 > CR.LineChange)
                                    {
                                        dev[11] = "1";
                                    }
                                    else
                                    {
                                        dev[11] = "0";
                                    }
                                    output += CR.Name + "," + (Convert.ToDouble(dev[3]) * capability).ToString() + "," + (Convert.ToDouble(dev[4]) * capability).ToString() + "," + (Convert.ToDouble(dev[5]) * capability).ToString() + "," + (Convert.ToDouble(dev[6]) * capability).ToString() + "," + (Convert.ToDouble(dev[7]) * current).ToString() + "," + (Convert.ToDouble(dev[8]) * Rad_to_Deg).ToString() + "," + dev[11] + "," + "\r\n";
                                }
                                else
                                {
                                    CR = new PSPDEV();
                                    CR.Name = dev[0];
                                    CR.Type = "TransformLine";
                                    CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", CR);
                                    if (CR != null)
                                    {
                                        if (Convert.ToDouble(dev[7]) * current * 1000 > CR.LineChange)
                                        {
                                            dev[11] = "1";
                                        }
                                        else
                                        {
                                            dev[11] = "0";
                                        }
                                        output += CR.Name + "," + (Convert.ToDouble(dev[3]) * capability).ToString() + "," + (Convert.ToDouble(dev[4]) * capability).ToString() + "," + (Convert.ToDouble(dev[5]) * capability).ToString() + "," + (Convert.ToDouble(dev[6]) * capability).ToString() + "," + (Convert.ToDouble(dev[7]) * current).ToString() + "," + (Convert.ToDouble(dev[8]) * Rad_to_Deg).ToString() + "," + dev[11] + "," + "\r\n";
                                    }
                                }

                                strLine = readLine.ReadLine();
                                strLine2 = readLine2.ReadLine();
                            }
                            readLine.Close();
                            readLine2.Close();
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                            }
                            op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                            str1 = new StreamWriter(op, Encoding.GetEncoding("GB2312"));
                            str1.Write(output);
                            str1.Close();


                            ex = new Excel.Application();
                            ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                            xSheet = (Excel.Worksheet)ex.Worksheets[1];
                            ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);

                            result1 = new Excel.Application();
                            result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                            tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
                            newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
                            newWorksheet.Name = "线路电流";
                            xSheet.Name = "母线潮流";
                            ex.Visible = true;

                            tempSheet.Cells.Select();
                            tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                            newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                            xSheet.Rows.AutoFit();
                            xSheet.Columns.AutoFit();
                            newWorksheet.Rows.AutoFit();
                            newWorksheet.Columns.AutoFit();
                            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 9]).MergeCells = true;
                            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
                            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            xSheet.get_Range(xSheet.Cells[5, 1], xSheet.Cells[5, 9]).Interior.ColorIndex = 45;
                            xSheet.get_Range(xSheet.Cells[6, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                            xSheet.get_Range(xSheet.Cells[6, 2], xSheet.Cells[xSheet.UsedRange.Rows.Count, 9]).NumberFormat = "0.0000_ ";
                            xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 9]).Font.Name = "楷体_GB2312";

                            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 9]).MergeCells = true;
                            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            newWorksheet.get_Range(newWorksheet.Cells[5, 1], newWorksheet.Cells[5, 8]).Interior.ColorIndex = 45;
                            newWorksheet.get_Range(newWorksheet.Cells[6, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                            newWorksheet.get_Range(newWorksheet.Cells[6, 2], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).NumberFormat = "0.0000_ ";
                            newWorksheet.get_Range(newWorksheet.Cells[3, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).Font.Name = "楷体_GB2312";

                            //op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fck.excel"), FileMode.OpenOrCreate);
                            //str1 = new StreamWriter(op, Encoding.GetEncoding("GB2312"));

                            string fn = tlVectorControl1.SVGDocument.FileName;

                            //result1.Save(System.Windows.Forms.Application.StartupPath + "\\fck.xls");

                            newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + fn + "牛拉法计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);



                            //str1.Write();
                            //op.Close();



                            System.Windows.Forms.Clipboard.Clear();
                            result1.Workbooks.Close();
                            result1.Quit();


                        }
                        catch (System.Exception e1)
                        {
                            MessageBox.Show("请进行潮流计算后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        //}
                        break;
                    case "GaussSeidelResult":

                        break;
                    case "N_RZYzResult":
                        break;
                    case "VoltEvaluation":
                       // PspVoltEvaluation();
                        break;
                    case "PowerLoss":

                        break;
                    case "ZLPResult1":
                        break;

                    case "ZLAResult1":

                        break;

                    case "PQResult":
                       
                        break;


                    case "mDLR":

                        break;

                    case "mEnclosure":
                        tlVectorControl1.Operation = ToolOperation.Enclosure;

                        break;

                    case "mGroup":
                        tlVectorControl1.Group();
                        break;
                    case "mUnGroup":
                        tlVectorControl1.UnGroup();
                        break;
                    case "mlinelx":
                        tlVectorControl1.Operation = ToolOperation.ConnectLine_Line;
                        break;
                    case "mzxlx":
                        tlVectorControl1.Operation = ToolOperation.ConnectLine_Rightangle;
                        break;
                    case "mqxlx":
                        tlVectorControl1.Operation = ToolOperation.ConnectLine_Spline;
                        break;
                    case "mqzlx":
                        tlVectorControl1.Operation = ToolOperation.ConnectLine_Polyline;
                        break;
                    case "mCJ1":
                        tlVectorControl1.Operation = ToolOperation.PolyLine;
                        csOperation = CustomOperation.OP_MeasureDistance;
                        break;
                    case "powerFactor":
                        PSPDEV pspDev21 = new PSPDEV();


                        //pspDev2.SUID = Guid.NewGuid().ToString();
                        pspDev21.Type = "Power";
                        pspDev21.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        pspDev21 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", pspDev21);
                        if (pspDev21 != null)
                        {
                        }
                        else
                        {
                            pspDev21 = new PSPDEV();
                            pspDev21.SUID = Guid.NewGuid().ToString();
                            pspDev21.Type = "Power";
                            pspDev21.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            Services.BaseService.Create<PSPDEV>(pspDev21);
                        }
                        powerf ppz = new powerf(pspDev21);
                        if (ppz.ShowDialog() == DialogResult.OK)
                        {
                            pspDev21.PowerFactor = Convert.ToDouble(ppz.powerfactor);
                            pspDev21.StandardVolt = Convert.ToDouble(ppz.standardvolt);
                            pspDev21.StandardCurrent = Convert.ToDouble(ppz.standardcurrent);
                            pspDev21.BigP = Convert.ToDouble(ppz.bigP);
                            Services.BaseService.Update<PSPDEV>(pspDev21);
                            PSPDEV voltall = new PSPDEV();
                            voltall.Type = "Use";
                            voltall.Lable = "电厂";
                            voltall.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList allvolt = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", voltall);
                            foreach (PSPDEV dev in allvolt)
                            {
                                dev.OutP = Convert.ToDouble(dev.Burthen) * pspDev21.BigP;
                                //dev.InPutP=dev.Burthen*pspDev2.BigP;
                                dev.OutQ = dev.OutP * Math.Tan(Math.Acos(pspDev21.PowerFactor));
                                Services.BaseService.Update<PSPDEV>(dev);
                            }
                            voltall.Lable = "变电站";
                            allvolt = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", voltall);
                            foreach (PSPDEV dev in allvolt)
                            {
                                dev.InPutP = Convert.ToDouble(dev.Burthen) * pspDev21.BigP;
                                //dev.InPutP=dev.Burthen*pspDev2.BigP;
                                dev.InPutQ = dev.InPutP * pspDev21.BigP * Math.Tan(Math.Acos(pspDev21.PowerFactor));
                                Services.BaseService.Update<PSPDEV>(dev);
                            }

                        }

                        break;
                    //if (=null)
                    //{

                    #endregion

                    #region 视图
                    case "mLayer":
                        frmlar.Show();
                        break;
                    case "mOption":
                        tlVectorControl1.SetOption();
                        break;
                    case "mAirscape":
                        frmAirscape fAir = new frmAirscape();
                        fAir.InitData(tlVectorControl1);
                        fAir.Owner = this;
                        fAir.ShowInTaskbar = false;
                        fAir.Top = Screen.PrimaryScreen.WorkingArea.Height - 250;
                        fAir.Left = Screen.PrimaryScreen.WorkingArea.Width - 300;
                        fAir.Show();
                        break;
                    case "btTL":
                        frmGlebeTypeList fgle = new frmGlebeTypeList();
                        fgle.Show();
                        break;
                    #endregion

                    #region 布局，对齐，顺序
                    case "mRotate":
                        if (btItem.Tag is ButtonItem)
                        {
                            btItem = btItem.Tag as ButtonItem;
                            tlVectorControl1.FlipX();


                        }
                        else
                        {
                            tlVectorControl1.FlipX();
                        }
                        break;
                    case "mToH":

                        tlVectorControl1.FlipX();
                        //this.rotateButton.Tag = btItem;
                        //this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToV":
                        tlVectorControl1.FlipY();
                        //this.rotateButton.Tag = btItem;
                        //this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToLeft":
                        tlVectorControl1.RotateSelection(-90f);
                        ////this.rotateButton.Tag = btItem;
                        ////this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToRight":
                        tlVectorControl1.RotateSelection(90f);
                        //this.rotateButton.Tag = btItem;
                        //this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mAlign":
                        if (btItem.Tag is ButtonItem)
                        {
                            btItem = btItem.Tag as ButtonItem;
                            tlVectorControl1.Align(AlignType.Left);

                        }
                        else
                        {
                            tlVectorControl1.Align(AlignType.Left);

                        }
                        tlVectorControl1.Refresh();
                        break;
                    case "mAlignLeft":
                        tlVectorControl1.Align(AlignType.Left);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mAlignRight":
                        tlVectorControl1.Align(AlignType.Right);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mAlignTop":
                        tlVectorControl1.Align(AlignType.Top);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mAlignBottom":
                        tlVectorControl1.Align(AlignType.Bottom);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mAlignHorizontalCenter":
                        tlVectorControl1.Align(AlignType.HorizontalCenter);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mAlignVerticalCenter":
                        tlVectorControl1.Align(AlignType.VerticalCenter);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mOrder":
                        if (btItem.Tag is ButtonItem)
                        {
                            btItem = btItem.Tag as ButtonItem;
                            tlVectorControl1.ChangeLevel(LevelType.Top);

                        }
                        else
                        {
                            tlVectorControl1.ChangeLevel(LevelType.Top);
                        }

                        break;
                    case "mGoTop":
                        tlVectorControl1.ChangeLevel(LevelType.Top);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mGoUp":
                        tlVectorControl1.ChangeLevel(LevelType.Up);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mGoDown":
                        tlVectorControl1.ChangeLevel(LevelType.Down);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mGoBottom":
                        tlVectorControl1.ChangeLevel(LevelType.Bottom);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    #endregion
                    #region 图元操作
                    case "mCopy":
                        tlVectorControl1.Copy();
                        break;
                    case "mCut":
                        tlVectorControl1.Cut();
                        break;
                    case "mPaste":
                        tlVectorControl1.Paste();
                        break;
                    case "mDelete":
                        if (tlVectorControl1.SVGDocument.CurrentElement is SVG)
                        {
                        }
                        else
                        {
                            if (MessageBox.Show("确定要删除么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                SvgElementCollection collection = tlVectorControl1.SVGDocument.SelectCollection;
                                foreach (XmlElement element in collection)
                                {
                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.EleID = element.GetAttribute("id");
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                                    Services.BaseService.Delete<PSPDEV>(pspDev);
                                }
                                tlVectorControl1.Delete();
                            }

                        }
                        break;
                    case "mUodo":
                        tlVectorControl1.Undo();
                        break;
                    case "mRedo":
                        tlVectorControl1.Redo();
                        break;
                    #endregion
                    #region 业务操作

                    case "mXLine":
                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.XPolyLine;
                        break;
                    case "mYLine":
                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.YPolyLine;
                        break;

                    case "mSaveGroup":
                        if (tlVectorControl1.SVGDocument.SelectCollection.Count > 1)
                        {
                            string content = "<svg>";
                            SvgElementCollection col = tlVectorControl1.SVGDocument.SelectCollection;
                            for (int i = 0; i < col.Count; i++)
                            {
                                SvgElement _e = (SvgElement)col[i];
                                if (_e.ID != "svg")
                                {
                                    content = content + _e.OuterXml;
                                }
                            }
                            RectangleF rect = tlVectorControl1.DrawArea.viewer.SelectedViewRectangle;

                            content = content + "</svg>";
                            frmSaveGroup fm = new frmSaveGroup();
                            fm.rect = rect;
                            fm.Content = content;
                            fm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("请至少选择2个图元。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        break;

                    case "mInsert":
                        frmUseGroup fg = new frmUseGroup();
                        if (fg.ShowDialog() == DialogResult.OK)
                        {
                            UseGroup u = fg.SelectedUseGroup;
                            if (u != null)
                            {
                                frmXY xy = new frmXY();
                                if (xy.ShowDialog() == DialogResult.OK)
                                {
                                    decimal x = xy.GetX();
                                    decimal y = xy.GetY();
                                    string content = u.Content;
                                    XmlDocument doc = new XmlDocument();
                                    doc.LoadXml(u.Content);
                                    XmlNodeList list = doc.ChildNodes;
                                    XmlNode _node = list[0];
                                    XmlNodeList sonlist = _node.ChildNodes;
                                    XmlElement ele = tlVectorControl1.SVGDocument.CreateElement("g");
                                    ele.SetAttribute("layer", SvgDocument.currentLayer);
                                    for (int i = 0; i < sonlist.Count; i++)
                                    {
                                        XmlNode _sonnode = sonlist[i];
                                        //string str = _sonnode.OuterXml;
                                        if (_sonnode.Name == "use")
                                        {
                                            string sid = ((XmlElement)_sonnode).GetAttribute("xlink:href");
                                            XmlNode _snode = symbolSelector.SymbolDoc.SelectSingleNode("//*[@id='" + sid.Substring(1) + "']");
                                            tlVectorControl1.SVGDocument.AddDefsElement((SvgElement)_snode);
                                        }
                                        ele.AppendChild(_sonnode);
                                        string ss = ele.OuterXml;
                                    }
                                    //RectangleF r=((Group)ele).GetBounds();
                                    string tr = "matrix(1,0,0,1,";

                                    tr = tr + Convert.ToString(x - Convert.ToDecimal(u.X)) + ",";
                                    tr = tr + Convert.ToString(y - Convert.ToDecimal(u.Y)) + ")";

                                    ele.SetAttribute("transform", tr);
                                    // transform="matrix(1,0,0,1,2558.82,-352.94)"
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(ele);
                                    tlVectorControl1.SVGDocument.SelectCollection.Clear();
                                    tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)ele);
                                    tlVectorControl1.UnGroup();
                                    // tlVectorControl1.Refresh();
                                }
                            }
                        }
                        break;
                    #endregion
                    #region 参数维护
                    case "mNodeParam":
                        frmNodeParam dlgNodeParam = new frmNodeParam(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        dlgNodeParam.ShowDialog();
                        break;
                    case "mLineParam":
                        frmLineParamWJ dlgLineParam = new frmLineParamWJ(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        dlgLineParam.ShowDialog();
                        break;
                    case "mWire":
                        wireTypeParam wirewire = new wireTypeParam();
                        wirewire.ShowDialog();
                        break;
                    case "nTransformLineParam":
                        frmTransformLineParam frmTransfor = new frmTransformLineParam(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        frmTransfor.ShowDialog();
                        break;
                    case "nGNDLineParam":
                        break;
                    case "mLineDL":
                        frmLineParamDL dlgLineParamDL = new frmLineParamDL(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        dlgLineParamDL.ShowDialog();
                        break;
                    case "mFadianDL":
                        frmFadejieDL dlgFadeDL = new frmFadejieDL(tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        dlgFadeDL.ShowDialog();
                        break;
                    case "mTest":
                        tlVectorControl1.Operation = ToolOperation.AreaSelect;
                        break;
                    case "mjianLine":

                        if (tlVectorControl1.SVGDocument.CurrentLayer.ID == tlVectorControl1.SVGDocument.SvgdataUid + "4")
                        {
                            zhengtiflag = true;
                            jiqiflag = false;
                            zhongqiflag = false;
                            yuanqiflag = false;
                            jianxiancheck();

                            MessageBox.Show("整体数据减线成功。");
                            Topology2();             //颜色发生变化
                        }
                        if (tlVectorControl1.SVGDocument.CurrentLayer.ID == tlVectorControl1.SVGDocument.SvgdataUid + "1" && zhengtiflag)
                        {
                            jiqiflag = true;
                            zhengtiflag = false;
                            zhongqiflag = false;
                            yuanqiflag = false;
                            jianxiancheck();

                            MessageBox.Show("近期数据减线成功。");
                            Topology2();             //颜色发生变化
                        }
                        if (tlVectorControl1.SVGDocument.CurrentLayer.ID == tlVectorControl1.SVGDocument.SvgdataUid + "2" && jiqiflag)
                        {
                            zhongqiflag = true;
                            jiqiflag = false;
                            yuanqiflag = false;
                            jianxiancheck();

                            MessageBox.Show("中期数据减线成功。");
                            Topology2();             //颜色发生变化
                        }
                        if (tlVectorControl1.SVGDocument.CurrentLayer.ID == tlVectorControl1.SVGDocument.SvgdataUid + "3" && zhongqiflag)
                        {
                            yuanqiflag = true;
                            jiqiflag = false;
                            zhongqiflag = false;
                            jianxiancheck();

                            MessageBox.Show("远期数据减线成功，请查看结果");
                            Topology2();             //颜色发生变化
                        }
                        if (!zhengtiflag && !jiqiflag && !zhongqiflag && !yuanqiflag)
                        {
                            string msg2 = "减线法：\r\n 1、选中整体图层进行网架优化。\r\n 2、将整体图层拷贝到近期图层，然后选中近期图层进行网架优化。\r\n 3、拷贝到中期图层，然后选中中期图层进行网架优化\r\n 4、拷贝到远期图层，然后选中远期图层进行网架优化。";
                            MessageBox.Show(msg2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        break;
                    case "mjiaLine":

                        if (tlVectorControl1.SVGDocument.CurrentLayer.ID == tlVectorControl1.SVGDocument.SvgdataUid + "4")
                        {

                            MessageBox.Show("整体图层采用的是减线法！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        if (tlVectorControl1.SVGDocument.CurrentLayer.ID == tlVectorControl1.SVGDocument.SvgdataUid + "1" && zhengtiflag)
                        {
                            jiqiflag = true;
                            zhengtiflag = false;
                            zhongqiflag = false;
                            yuanqiflag = false;
                            addlinecheck();
                            addrightcheck();
                            for (int i = 0; i < ercilinedengdai.Count; i++)
                            {
                                ercilinedengdai[i].LineStatus = "运行";
                                Services.BaseService.Update<PSPDEV>(ercilinedengdai[i]);
                            }
                            for (int i = 0; i < lineyiyou.Count; i++)
                            {
                                lineyiyou[i].LineStatus = "待选";
                                Services.BaseService.Update<PSPDEV>(lineyiyou[i]);
                            }
                            MessageBox.Show("近期数据加线成功。");
                            Topology2();             //颜色发生变化
                        }
                        if (tlVectorControl1.SVGDocument.CurrentLayer.ID == tlVectorControl1.SVGDocument.SvgdataUid + "2" && jiqiflag)
                        {
                            zhongqiflag = true;
                            jiqiflag = false;
                            yuanqiflag = false;
                            addlinecheck();
                            addrightcheck();
                            for (int i = 0; i < ercilinedengdai.Count; i++)
                            {
                                ercilinedengdai[i].LineStatus = "运行";
                                Services.BaseService.Update<PSPDEV>(ercilinedengdai[i]);
                            }
                            for (int i = 0; i < lineyiyou.Count; i++)
                            {
                                lineyiyou[i].LineStatus = "待选";
                                Services.BaseService.Update<PSPDEV>(lineyiyou[i]);
                            }
                            MessageBox.Show("中期数据加线成功。");
                            Topology2();             //颜色发生变化
                        }
                        if (tlVectorControl1.SVGDocument.CurrentLayer.ID == tlVectorControl1.SVGDocument.SvgdataUid + "3" && zhongqiflag)
                        {
                            yuanqiflag = true;
                            jiqiflag = false;
                            zhongqiflag = false;
                            addlinecheck();
                            addrightcheck();
                            for (int i = 0; i < ercilinedengdai.Count; i++)
                            {
                                ercilinedengdai[i].LineStatus = "运行";
                                Services.BaseService.Update<PSPDEV>(ercilinedengdai[i]);
                            }
                            for (int i = 0; i < lineyiyou.Count; i++)
                            {
                                lineyiyou[i].LineStatus = "待选";
                                Services.BaseService.Update<PSPDEV>(lineyiyou[i]);
                            }
                            MessageBox.Show("远期数据加线成功。请查看优化结果");
                            Topology2();             //颜色发生变化
                        }
                        if (!zhengtiflag && !jiqiflag && !zhongqiflag && !yuanqiflag)
                        {
                            string msg2 = "加线法：\r\n 1、选中整体图层进行网架优化，此时采用的是减线法。\r\n 2、将整体图层拷贝到近期图层，然后选中近期图层进行网架优化。\r\n 3、拷贝到中期图层，然后选中中期图层进行网架优化\r\n 4、拷贝到远期图层，然后选中远期图层进行网架优化。";
                            MessageBox.Show(msg2, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        break;

                    case "YhResult":       //优化结果
                        if (yuanqiflag)
                        {
                            frmGProList p1 = new frmGProList();
                            p1.Show();
                            p1.LoadData(LoadData());
                        }
                        else
                            MessageBox.Show("请依次做完各个时期的优化，再看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    #endregion
                    #region 参数维护
                    case "m_in":
                        if (tlVectorControl1.SVGDocument.CurrentLayer.ID != tlVectorControl1.SVGDocument.SvgdataUid + "5")
                        {
                            MessageBox.Show("请选择背景参考层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        StringBuilder txt = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><svg id=\"svg\" width=\"1500\" height=\"1000\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:itop=\"http://www.Itop.com/itop\" transform=\"matrix(1 0 0 1 0 1)\"><defs>");
                        StringBuilder Allcontent = new StringBuilder();
                        string svgdefs = "";
                        string layertxt = "";


                        frmLayerSel sel = new frmLayerSel();
                        if (sel.ShowDialog() == DialogResult.OK)
                        {
                            ArrayList tlist = sel.LayList;
                            SVG_SYMBOL sym = new SVG_SYMBOL();
                            sym.svgID = oldsid;
                            IList<SVG_SYMBOL> symlist = Services.BaseService.GetList<SVG_SYMBOL>("SelectSVG_SYMBOLBySvgID", sym);
                            foreach (SVG_SYMBOL _sym in symlist)
                            {
                                svgdefs = svgdefs + _sym.XML;
                            }
                            txt.Append(svgdefs + "</defs>");
                            for (int i = 0; i < tlist.Count; i++)
                            {
                                SVG_LAYER lar = new SVG_LAYER();
                                lar.svgID = oldsid;
                                lar.SUID = ((SVG_LAYER)tlist[i]).SUID;
                                lar = (SVG_LAYER)Services.BaseService.GetObject("SelectSVG_LAYERByKey", lar);
                                layertxt = layertxt + "<layer id=\"" + lar.SUID + "\" label=\"" + lar.NAME + "\" layerType=\"" + lar.layerType + "\" visibility=\"" + lar.visibility + "\" ParentID=\"" + lar.YearID + "\" IsSelect=\"" + lar.IsSelect + "\" />";
                                Allcontent.Append(lar.XML);
                            }
                            txt.Append(layertxt);
                            txt.Append(Allcontent.ToString() + "</svg>");
                            SvgDocument document = new SvgDocument();
                            document.LoadXml(txt.ToString());
                            document.SvgdataUid = oldsid;
                            XmlNodeList xlist = document.SelectNodes("svg/polyline [@IsLead='1']");
                            for (int i = 0; i < xlist.Count; i++)
                            {
                                SvgElement gra = xlist[i] as SvgElement;
                                gra.SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "5");
                                ((IGraph)gra).Layer = tlVectorControl1.SVGDocument.CurrentLayer;
                                tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)gra);

                            }
                            XmlNodeList xlist2 = document.SelectNodes("svg/use");
                            for (int i = 0; i < xlist2.Count; i++)
                            {
                                SvgElement gra = xlist2[i] as SvgElement;
                                gra.SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "5");
                                ((IGraph)gra).Layer = tlVectorControl1.SVGDocument.CurrentLayer;
                                tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)gra);

                            }
                            tlVectorControl1.Refresh();
                            MessageBox.Show("导入数据成功。");
                        }


                        break;
                    case "m_1to2"://近期

                        //删除掉原来元素


                        deltall(tlVectorControl1.SVGDocument.SvgdataUid + "1");
                        //********
                        ArrayList sel_line1 = new ArrayList();
                        LayerGrade l1 = new LayerGrade();
                        l1.Type = "1";
                        l1.SvgDataUid = oldsid;
                        IList ttlist = Services.BaseService.GetList("SelectLayerGradeList5", l1);
                        if (ttlist.Count > 0)
                        {
                            LayerGrade n1 = (LayerGrade)ttlist[0];
                            try
                            {
                                int yy = Convert.ToInt32(n1.Name.Substring(0, 4));
                                XmlNodeList list1to2 = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "4" + "' and @year<='" + yy + "']");
                                for (int i = 0; i < list1to2.Count; i++)
                                {
                                    SvgElement gra = list1to2[i] as SvgElement;

                                    XmlNode temp = gra.Clone();
                                    ((SvgElement)temp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "1");
                                    XmlNode newnode = tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)temp);
                                    //加入节点的名称


                                    XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + gra.GetAttribute("id") + "']");
                                    XmlNode textemp = text.Clone();
                                    ((SvgElement)textemp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "1");
                                    ((SvgElement)textemp).SetAttribute("ParentID", ((SvgElement)temp).GetAttribute("id"));
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)textemp);
                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.EleID = gra.ID;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
                                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                                    if (pspDev != null)
                                    {
                                        pspDev.SUID = Guid.NewGuid().ToString();
                                        pspDev.EleID = ((SvgElement)newnode).ID;
                                        pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "1";
                                        Services.BaseService.Create<PSPDEV>(pspDev);
                                    }
                                }
                                XmlNodeList listline = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "4']");
                                //for (int i = 0; i < list1to2.Count; i++)
                                //{
                                //    SvgElement temp = list1to2[i] as SvgElement;
                                //    RectangleF ff = ((IGraph)temp).GetBounds();
                                //    Region r = new Region(((IGraph)temp).GetBounds());
                                //    for (int j = 0; j < listline.Count;j++ )
                                //    {
                                //        Polyline pln = listline[j] as Polyline;
                                //        if (r.IsVisible(pln.Points[0]))
                                //        {
                                //            if (!sel_line1.Contains(pln))
                                //            {
                                //                sel_line1.Add(pln);
                                //            }

                                //        }
                                //    }

                                //}
                                for (int i = 0; i < listline.Count; i++)
                                {
                                    bool firstnodeflag = false; bool lastnodeflag = false;
                                    Polyline pln = listline[i] as Polyline;
                                    PSPDEV psp = new PSPDEV();
                                    psp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
                                    psp.EleID = pln.ID;
                                    psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", psp);
                                    if (psp.LineStatus != "等待")
                                    {
                                        for (int j = 0; j < list1to2.Count; j++)
                                        {
                                            SvgElement temp = list1to2[j] as SvgElement;
                                            Region ff = new Region(((IGraph)temp).GetBounds());
                                            if (ff.IsVisible(pln.Points[0]))
                                            {
                                                firstnodeflag = true;
                                            }
                                            if (ff.IsVisible(pln.Points[1]))
                                            {
                                                lastnodeflag = true;
                                            }
                                        }
                                        if (firstnodeflag && lastnodeflag && !sel_line1.Contains(pln))
                                        {
                                            sel_line1.Add(pln);
                                        }

                                    }

                                }
                                for (int i = 0; i < sel_line1.Count; i++)
                                {
                                    SvgElement gra = sel_line1[i] as SvgElement;

                                    XmlNode temp = gra.Clone();
                                    ((SvgElement)temp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "1");
                                    XmlNode newnode = tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)temp);
                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.EleID = gra.ID;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
                                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                                    if (pspDev != null)
                                    {
                                        pspDev.SUID = Guid.NewGuid().ToString();
                                        pspDev.EleID = ((SvgElement)newnode).ID;
                                        pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "1";
                                        Services.BaseService.Create<PSPDEV>(pspDev);
                                    }
                                }
                                MessageBox.Show("数据处理成功。");
                                Topology2();             //颜色发生变化
                            }
                            catch
                            {
                                MessageBox.Show("选择的年份前4位不是数字。");
                            }
                        }
                        break;
                    case "m_2to3"://中期
                        deltall(tlVectorControl1.SVGDocument.SvgdataUid + "2");
                        ArrayList sel_line2 = new ArrayList();
                        LayerGrade l1_2 = new LayerGrade();
                        l1_2.Type = "1";
                        l1_2.SvgDataUid = oldsid;
                        l1_2 = (LayerGrade)Services.BaseService.GetObject("SelectLayerGradeList5", l1_2);
                        LayerGrade l2_2 = new LayerGrade();
                        l2_2.Type = "2";
                        l2_2.SvgDataUid = oldsid;
                        l2_2 = (LayerGrade)Services.BaseService.GetObject("SelectLayerGradeList5", l2_2);
                        if (l1_2 != null && l2_2 != null)
                        {

                            try
                            {
                                //整体规划层里的中期数据





                                int yy1 = Convert.ToInt32(l1_2.Name.Substring(0, 4));
                                int yy2 = Convert.ToInt32(l2_2.Name.Substring(0, 4));

                                XmlNodeList list1to2 = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "4" + "' and @year>'" + yy1 + "' and @year<='" + yy2 + "']");
                                for (int i = 0; i < list1to2.Count; i++)
                                {
                                    SvgElement gra = list1to2[i] as SvgElement;
                                    XmlNode temp = gra.Clone();
                                    ((SvgElement)temp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "2");
                                    XmlNode newnode = tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)temp);
                                    //加入节点的名称


                                    XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + gra.GetAttribute("id") + "']");
                                    XmlNode textemp = text.Clone();
                                    ((SvgElement)textemp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "2");
                                    ((SvgElement)textemp).SetAttribute("ParentID", ((SvgElement)temp).GetAttribute("id"));
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)textemp);
                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.EleID = gra.ID;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
                                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                                    if (pspDev != null)
                                    {
                                        pspDev.SUID = Guid.NewGuid().ToString();
                                        pspDev.EleID = ((SvgElement)newnode).ID;
                                        pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "2";
                                        Services.BaseService.Create<PSPDEV>(pspDev);
                                    }
                                }

                                XmlNodeList listline2 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "4']");
                                XmlNodeList list1to1 = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "4" + "' and @year<='" + yy1 + "']");
                                for (int i = 0; i < listline2.Count; i++)
                                {
                                    bool firstnodeflag = false; bool lastnodeflag = false; bool jinqifirstnodeflag = false; bool jinqilastnodeflag = false;
                                    Polyline pln = listline2[i] as Polyline;
                                    PSPDEV psp = new PSPDEV();
                                    psp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
                                    psp.EleID = pln.ID;
                                    psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", psp);
                                    if (psp.LineStatus != "等待")
                                    {
                                        for (int j = 0; j < list1to2.Count; j++)
                                        {
                                            SvgElement temp = list1to2[j] as SvgElement;
                                            Region ff = new Region(((IGraph)temp).GetBounds());
                                            if (ff.IsVisible(pln.Points[0]))
                                            {
                                                firstnodeflag = true;
                                            }
                                            if (ff.IsVisible(pln.Points[1]))
                                            {
                                                lastnodeflag = true;
                                            }
                                        }
                                        for (int j = 0; j < list1to1.Count; j++)
                                        {
                                            SvgElement temp = list1to1[j] as SvgElement;
                                            Region ff = new Region(((IGraph)temp).GetBounds());
                                            if (ff.IsVisible(pln.Points[0]))
                                            {
                                                jinqifirstnodeflag = true;
                                            }
                                            if (ff.IsVisible(pln.Points[1]))
                                            {
                                                jinqilastnodeflag = true;
                                            }
                                        }
                                        if (firstnodeflag && lastnodeflag && !sel_line2.Contains(pln))
                                        {
                                            sel_line2.Add(pln);
                                        }
                                        if (firstnodeflag && jinqilastnodeflag && !sel_line2.Contains(pln))
                                        {
                                            sel_line2.Add(pln);
                                        }
                                        if (lastnodeflag && jinqifirstnodeflag && !sel_line2.Contains(pln))
                                        {
                                            sel_line2.Add(pln);
                                        }

                                    }

                                }
                                for (int i = 0; i < sel_line2.Count; i++)
                                {
                                    SvgElement gra = sel_line2[i] as SvgElement;

                                    XmlNode temp = gra.Clone();
                                    ((SvgElement)temp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "2");
                                    XmlNode newnode = tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)temp);
                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.EleID = gra.ID;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
                                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                                    if (pspDev != null)
                                    {
                                        pspDev.SUID = Guid.NewGuid().ToString();
                                        pspDev.EleID = ((SvgElement)newnode).ID;
                                        pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "2";
                                        Services.BaseService.Create<PSPDEV>(pspDev);
                                    }
                                }
                                //近期数据
                                XmlNodeList list_1 = tlVectorControl1.SVGDocument.SelectNodes("svg/* [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "1']");
                                for (int i = 0; i < list_1.Count; i++)
                                {
                                    SvgElement gra = list_1[i] as SvgElement;
                                    XmlNode temp = gra.Clone();
                                    ((SvgElement)temp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "2");
                                    XmlNode newnode = tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)temp);
                                    //加入节点的名称


                                    XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + gra.GetAttribute("id") + "']");
                                    if (text != null)
                                    {
                                        XmlNode textemp = text.Clone();
                                        ((SvgElement)textemp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "2");
                                        ((SvgElement)textemp).SetAttribute("ParentID", ((SvgElement)temp).GetAttribute("id"));
                                        tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)textemp);
                                    }

                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.EleID = gra.ID;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "1";
                                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                                    if (pspDev != null)
                                    {
                                        pspDev.SUID = Guid.NewGuid().ToString();
                                        pspDev.EleID = ((SvgElement)newnode).ID;
                                        pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "2";
                                        if (pspDev.LineStatus == "待选")
                                        {
                                            pspDev.LineStatus = "运行";
                                        }
                                        if (pspDev.LineStatus == "等待")
                                        {
                                            pspDev.LineStatus = "待选";
                                        }
                                        Services.BaseService.Create<PSPDEV>(pspDev);
                                    }
                                }
                                MessageBox.Show("数据处理成功。");
                                Topology2();             //颜色发生变化
                            }
                            catch
                            {
                                MessageBox.Show("选择的年份前4位不是数字。");
                            }
                        }
                        break;
                    case "m_3to4"://远期
                        deltall(tlVectorControl1.SVGDocument.SvgdataUid + "3");
                        ArrayList sel_line3 = new ArrayList();
                        LayerGrade l1_3 = new LayerGrade();
                        l1_3.Type = "2";
                        l1_3.SvgDataUid = oldsid;
                        l1_3 = (LayerGrade)Services.BaseService.GetObject("SelectLayerGradeList5", l1_3);
                        LayerGrade l2_3 = new LayerGrade();
                        l2_3.Type = "3";
                        l2_3.SvgDataUid = oldsid;
                        l2_3 = (LayerGrade)Services.BaseService.GetObject("SelectLayerGradeList5", l2_3);
                        if (l1_3 != null && l2_3 != null)
                        {

                            try
                            {
                                //整体规划层里的远期数据





                                int yy1 = Convert.ToInt32(l1_3.Name.Substring(0, 4));
                                int yy2 = Convert.ToInt32(l2_3.Name.Substring(0, 4));
                                XmlNodeList list1to2 = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "4" + "' and @year>'" + yy1 + "' and @year<='" + yy2 + "']");
                                for (int i = 0; i < list1to2.Count; i++)
                                {
                                    SvgElement gra = list1to2[i] as SvgElement;
                                    XmlNode temp = gra.Clone();
                                    ((SvgElement)temp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "3");
                                    XmlNode newnode = tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)temp);
                                    XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + gra.GetAttribute("id") + "']");
                                    XmlNode textemp = text.Clone();
                                    ((SvgElement)textemp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "3");
                                    ((SvgElement)textemp).SetAttribute("ParentID", ((SvgElement)temp).GetAttribute("id"));
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)textemp);
                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.EleID = gra.ID;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
                                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                                    if (pspDev != null)
                                    {
                                        pspDev.SUID = Guid.NewGuid().ToString();
                                        pspDev.EleID = ((SvgElement)newnode).ID;
                                        pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "3";
                                        Services.BaseService.Create<PSPDEV>(pspDev);
                                    }
                                }

                                XmlNodeList listline2 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "4']");
                                XmlNodeList list1to1 = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "4" + "' and @year<='" + yy1 + "']");
                                for (int i = 0; i < listline2.Count; i++)
                                {
                                    bool firstnodeflag = false; bool lastnodeflag = false; bool jinqifirstnodeflag = false; bool jinqilastnodeflag = false;
                                    Polyline pln = listline2[i] as Polyline;
                                    PSPDEV psp = new PSPDEV();
                                    psp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
                                    psp.EleID = pln.ID;
                                    psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", psp);
                                    if (psp.LineStatus != "等待")
                                    {
                                        for (int j = 0; j < list1to2.Count; j++)
                                        {
                                            SvgElement temp = list1to2[j] as SvgElement;
                                            Region ff = new Region(((IGraph)temp).GetBounds());
                                            if (ff.IsVisible(pln.Points[0]))
                                            {
                                                firstnodeflag = true;
                                            }
                                            if (ff.IsVisible(pln.Points[1]))
                                            {
                                                lastnodeflag = true;
                                            }
                                        }
                                        for (int j = 0; j < list1to1.Count; j++)
                                        {
                                            SvgElement temp = list1to1[j] as SvgElement;
                                            Region ff = new Region(((IGraph)temp).GetBounds());
                                            if (ff.IsVisible(pln.Points[0]))
                                            {
                                                jinqifirstnodeflag = true;
                                            }
                                            if (ff.IsVisible(pln.Points[1]))
                                            {
                                                jinqilastnodeflag = true;
                                            }
                                        }
                                        if (firstnodeflag && lastnodeflag && !sel_line3.Contains(pln))
                                        {
                                            sel_line3.Add(pln);
                                        }
                                        if (firstnodeflag && jinqilastnodeflag && !sel_line3.Contains(pln))
                                        {
                                            sel_line3.Add(pln);
                                        }
                                        if (lastnodeflag && jinqifirstnodeflag && !sel_line3.Contains(pln))
                                        {
                                            sel_line3.Add(pln);
                                        }

                                    }

                                }
                                for (int i = 0; i < sel_line3.Count; i++)
                                {
                                    SvgElement gra = sel_line3[i] as SvgElement;

                                    XmlNode temp = gra.Clone();
                                    ((SvgElement)temp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "3");
                                    XmlNode newnode = tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)temp);
                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.EleID = gra.ID;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
                                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                                    if (pspDev != null)
                                    {
                                        pspDev.SUID = Guid.NewGuid().ToString();
                                        pspDev.EleID = ((SvgElement)newnode).ID;
                                        pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "3";
                                        Services.BaseService.Create<PSPDEV>(pspDev);
                                    }
                                }
                                //中期数据
                                XmlNodeList list_1 = tlVectorControl1.SVGDocument.SelectNodes("svg/* [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "2']");
                                for (int i = 0; i < list_1.Count; i++)
                                {
                                    SvgElement gra = list_1[i] as SvgElement;
                                    XmlNode temp = gra.Clone();
                                    ((SvgElement)temp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "3");
                                    XmlNode newnode = tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)temp);
                                    //加入节点的名称


                                    XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + gra.GetAttribute("id") + "']");
                                    if (text != null)
                                    {
                                        XmlNode textemp = text.Clone();
                                        ((SvgElement)textemp).SetAttribute("layer", tlVectorControl1.SVGDocument.SvgdataUid + "3");
                                        ((SvgElement)textemp).SetAttribute("ParentID", ((SvgElement)temp).GetAttribute("id"));
                                        tlVectorControl1.SVGDocument.RootElement.AppendChild((XmlNode)textemp);
                                    }
                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.EleID = gra.ID;
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "2";
                                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                                    if (pspDev != null)
                                    {
                                        pspDev.SUID = Guid.NewGuid().ToString();
                                        pspDev.EleID = ((SvgElement)newnode).ID;
                                        pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "3";
                                        if (pspDev.LineStatus == "待选")
                                        {
                                            pspDev.LineStatus = "运行";
                                        }
                                        if (pspDev.LineStatus == "等待")
                                        {
                                            pspDev.LineStatus = "待选";
                                        }
                                        Services.BaseService.Create<PSPDEV>(pspDev);
                                    }
                                }
                                MessageBox.Show("数据处理成功。");
                                Topology2();             //颜色发生变化
                            }
                            catch
                            {
                                MessageBox.Show("选择的年份前4位不是数字。");
                            }
                        }
                        break;
                    #endregion
                }
            }
        }
        private void jianxiancheck()
        {
            FileStream dh;
            StreamReader readLine;
            char[] charSplit;
            string strLine;
            string[] array1;
            string output = null;
            string[] array2;

            string strLine2;

            char[] charSplit2 = new char[] { ' ' };
            FileStream op;
            StreamWriter str1;
            FileStream dh2;
            StreamReader readLine2;

            if (!Check())
            {
                return;
            }
            //NIULA pspniula = new NIULA();
            //pspniula.CurrentCal();
            N1Test.NBcal zl = new NBcal();
            //zl.Show_KmRelia(1);
            zl.ZLpsp();


            double yinzi = 0, capability = 0, volt = 0, current = 0, standvolt = 0, Rad_to_Deg = 57.29577951;
            PSPDEV benchmark = new PSPDEV();
            benchmark.Type = "power";
            benchmark.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
            if (list3 == null)
            {
                MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (PSPDEV dev in list3)
            {
                yinzi = Convert.ToDouble(dev.PowerFactor);
                capability = Convert.ToDouble(dev.StandardCurrent);
                volt = Convert.ToDouble(dev.StandardVolt);
                if (dev.PowerFactor == 0)
                {
                    yinzi = 1;
                }
                if (dev.StandardCurrent == 0)
                {
                    capability = 1;
                }
                if (dev.StandardVolt == 0)
                {
                    volt = 1;
                }
                standvolt = volt;
                current = capability / (Math.Sqrt(3) * volt);

            }
            capability = 100;
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
            {
            }
            else
            {
                return;
            }
            dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);

            readLine = new StreamReader(dh);
            charSplit = new char[] { ' ' };
            strLine = readLine.ReadLine();
            output = null;
            while (!string.IsNullOrEmpty(strLine))
            {
                array1 = strLine.Split(charSplit);


                string[] dev = new string[2];
                dev.Initialize();
                int i = 0;
                PSPDEV CR = new PSPDEV();
                CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

                foreach (string str in array1)
                {
                    if (str != "")
                    {
                        if (i == 0)
                        {
                            dev[i++] = str.ToString();
                        }
                        else
                        {
                            if (str != "NaN")
                            {
                                dev[i++] = Convert.ToDouble(str).ToString();
                            }
                            else
                            {
                                dev[i++] = str;
                            }

                        }
                    }

                }

                CR.Name = dev[0];
                CR.Type = "Polyline";
                CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", CR);
                if (CR != null && CR.ReferenceVolt != 0)
                {
                    volt = CR.ReferenceVolt;
                }
                else
                    volt = standvolt;
                current = capability / (Math.Sqrt(3) * volt);
                if (CR != null)
                {
                    if (CR.LineStatus == "待选")
                    {
                        for (int n = 0; n < waitlinecoll.Count; n++)            //加入线路功率
                        {
                            if (waitlinecoll[n].linename == CR.Name)
                            {
                                double linepij = System.Math.Abs(Convert.ToDouble(dev[1])) * capability;
                                XmlNode el = tlVectorControl1.SVGDocument.SelectSingleNode("svg/polyline[@layer='" + tlVectorControl1.SVGDocument.CurrentLayer.ID + "' and @id='" + CR.EleID + "']");
                                double linevalue = 0;
                                if (!string.IsNullOrEmpty(((XmlElement)el).GetAttribute("linevalue")))
                                {
                                    linevalue = Convert.ToDouble(((XmlElement)el).GetAttribute("linevalue"));
                                }
                                else
                                    linevalue = 1;
                                waitlinecoll[n].Suid = CR.SUID;
                                waitlinecoll[n].linepij = linepij;
                                waitlinecoll[n].linevalue = linevalue;
                                waitlinecoll[n].linetouzilv = linepij / linevalue;
                            }
                        }
                    }
                    //output += "'" + CR.Name.ToString() + "," + (Convert.ToDouble(dev[3]) * capability).ToString() + "," + (Convert.ToDouble(dev[4]) * capability).ToString() + "," + (Convert.ToDouble(dev[5]) * capability).ToString() + "," + (Convert.ToDouble(dev[6]) * capability).ToString() + "," + (Convert.ToDouble(dev[7]) * current).ToString() + "," + (Convert.ToDouble(dev[8]) * Rad_to_Deg).ToString() + "," + dev[11] + "," + "\r\n";
                }
                strLine = readLine.ReadLine();
            }
            readLine.Close();
            waitlinecoll.Sort();         //进行大小排序  然后进行直流方法的检验


            bool lineflag = true;      //只要有一个不合格则就为不合格
            bool jielieflag = true;    //判断有没有线路解裂


            for (int i = 0; i < waitlinecoll.Count; i++)
            {
                N1Test.NBcal kk = new N1Test.NBcal();
                kk.Show_KmRelia(waitlinecoll[i].linenum);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ZL.txt"))
                {
                }
                else
                {
                    return;
                }
                dh2 = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ZL.txt", FileMode.Open);

                readLine2 = new StreamReader(dh2);
                charSplit2 = new char[] { ' ' };
                strLine2 = readLine2.ReadLine();
                output = null;
                while (!string.IsNullOrEmpty(strLine2))
                {
                    array2 = strLine2.Split(charSplit2);


                    string[] dev = new string[2 * brchcount + 1];
                    dev.Initialize();

                    PSPDEV CR = new PSPDEV();
                    CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    int m = 0;
                    foreach (string str in array2)
                    {

                        if (str != "")
                        {

                            dev[m++] = str.ToString();

                        }
                    }
                    //进行解裂和负荷判断



                    if (dev[1] != "-1")
                    {
                        for (int j = 0; j < brchcount; j++)
                        {

                            double pij = System.Math.Abs(Convert.ToDouble(dev[j * 2 + 2])) * capability;

                            PSPDEV psp = new PSPDEV();

                            psp.Name = dev[j * 2 + 1];
                            psp.Type = "Polyline";
                            psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", psp);
                            PSPDEV pspline = (PSPDEV)listName[0];
                            double voltR = pspline.VoltR;
                            WireCategory wirewire = new WireCategory();
                            wirewire.WireType = pspline.LineType;
                            if (string.IsNullOrEmpty(pspline.LineType))
                            {
                                MessageBox.Show(pspline.Name + "的线路类型没有输入，无法进行可靠性检验", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            WireCategory listware = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKey", wirewire);
                            double Ichange = (double)listware.WireChange;
                            double linXij = System.Math.Sqrt(3) * voltR * Ichange / 1000;
                            if (pij >= linXij)
                            {
                                lineflag = false;
                                //lineclass _line = new lineclass(n, j);
                                //Overlinp.Add(_line);
                                // OverPhege[n] = j;
                            }

                        }

                    }

                    else
                    {
                        jielieflag = false;
                    }

                    if (jielieflag && lineflag)
                    {
                        readLine2.Close();           //关闭读取的文件


                        PSPDEV psp = new PSPDEV();

                        psp.Name = dev[0];
                        psp.Type = "Polyline";
                        psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", psp);
                        PSPDEV pspline = (PSPDEV)listName[0];
                        pspline.LineStatus = "等待";
                        Services.BaseService.Update<PSPDEV>(pspline);
                        jianxiancheck();       //进行下一轮的减线处理
                        return;
                    }
                    /*
                          else
                                                 break;*/

                    strLine2 = readLine2.ReadLine();
                }
                readLine2.Close();

            }
            // readLine2.Close();

        }
        List<PSPDEV> lineyiyou = new List<PSPDEV>();             //将待选的线路合格的加入到已有线路中


        List<PSPDEV> linedengdai = new List<PSPDEV>();          //记录所有待选的线路（一次运行以后就会发生变化）
        List<linedaixuan> waitlineindex = new List<linedaixuan>();   //记录待选线路中 投资指标的大小后来并且经过排序


        List<PSPDEV> ercilinedengdai = new List<PSPDEV>();      //记录加线法右边的算法中 当某条线路断开出现过负荷 我们就可以将其线路的状态改为等待 并且放入此容器中 后来在程序输出时再改为已有

        List<linedaixuan> fuheline = new List<linedaixuan>();   //记录出现负荷的线路 为建立投资指标用          
        private void addlinecheck()
        {
            FileStream dh;
            StreamReader readLine;
            char[] charSplit;
            string strLine;
            string[] array1;
            string output = null;
            string[] array2;

            string strLine2;

            char[] charSplit2 = new char[] { ' ' };
            FileStream op;
            StreamWriter str1;
            FileStream dh2;
            StreamReader readLine2;


            PSPDEV pspdev = new PSPDEV();
            pspdev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            pspdev.LineStatus = "待选";
            pspdev.Type = "Polyline";
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndLineStatus", pspdev);
            for (int i = 0; i < list1.Count; i++)
            {
                pspdev = list1[i] as PSPDEV;
                pspdev.LineStatus = "等待";
                linedengdai.Add(pspdev);
                Services.BaseService.Update<PSPDEV>(pspdev);

            }
            lineyiyou.Clear();            //清空上一次的记录  为下一个时期的应用做准备


           // linedengdai.Clear();          //清空上一次记录待选线路 为下一个时期的应用做准备



            AA:
          if (!fuhecheck())
          {

              for (int j = 0; j < linedengdai.Count; j++)
              {
                  if (!Checkadd(linedengdai[j].EleID))
                      return;
                  N1Test.NBcal zl = new NBcal();
                  //zl.Show_KmRelia(1);
                  zl.ZLpsp();



                  double yinzi = 0, capability = 0, volt = 0, current = 0, standvolt = 0, Rad_to_Deg = 57.29577951;
                  PSPDEV benchmark = new PSPDEV();
                  benchmark.Type = "power";
                  benchmark.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                  IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
                  if (list3 == null)
                  {
                      MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      return;
                  }
                  foreach (PSPDEV dev in list3)
                  {
                      yinzi = Convert.ToDouble(dev.PowerFactor);
                      capability = Convert.ToDouble(dev.StandardCurrent);
                      volt = Convert.ToDouble(dev.StandardVolt);
                      if (dev.PowerFactor == 0)
                      {
                          yinzi = 1;
                      }
                      if (dev.StandardCurrent == 0)
                      {
                          capability = 1;
                      }
                      if (dev.StandardVolt == 0)
                      {
                          volt = 1;
                      }
                      standvolt = volt;
                      current = capability / (Math.Sqrt(3) * volt);

                  }
                  capability = 100;
                  if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                  {
                  }
                  else
                  {
                      return;
                  }
                  dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);

                  readLine = new StreamReader(dh);
                  charSplit = new char[] { ' ' };
                  strLine = readLine.ReadLine();
                  output = null;
                  double sumpij = 0.0;
                  bool lineflag = true;
                  while (!string.IsNullOrEmpty(strLine))
                  {
                      array1 = strLine.Split(charSplit);


                      string[] dev = new string[2];
                      dev.Initialize();
                      int i = 0;
                      PSPDEV CR = new PSPDEV();
                      CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

                      foreach (string str in array1)
                      {
                          if (str != "")
                          {
                              if (i == 0)
                              {
                                  dev[i++] = str.ToString();
                              }
                              else
                              {
                                  if (str != "NaN")
                                  {
                                      dev[i++] = Convert.ToDouble(str).ToString();
                                  }
                                  else
                                  {
                                      dev[i++] = str;
                                  }

                              }
                          }

                      }

                      CR.Name = dev[0];
                      CR.Type = "Polyline";
                      CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", CR);
                      if (CR != null && CR.ReferenceVolt != 0)
                      {
                          volt = CR.ReferenceVolt;
                      }
                      else
                          volt = standvolt;
                      current = capability / (Math.Sqrt(3) * volt);
                      if (CR != null && !CR.Name.Contains("虚拟线路"))
                      {


                          double linepij = System.Math.Abs(Convert.ToDouble(dev[1])) * capability;
                          double voltR = CR.VoltR;
                          WireCategory wirewire = new WireCategory();
                          wirewire.WireType = CR.LineType;
                          if (string.IsNullOrEmpty(CR.LineType))
                          {
                              MessageBox.Show(CR.Name + "的线路类型没有输入，无法进行可靠性检验", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                              return;
                          }
                          WireCategory listware = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKey", wirewire);
                          double Ichange = (double)listware.WireChange;
                          double linXij = System.Math.Sqrt(3) * voltR * Ichange / 1000;
                          if (linepij >= linXij)
                          {
                              lineflag = false;

                          }
                          for (int k = 0; k < fuheline.Count; k++)
                          {
                              if (CR.SUID == fuheline[k].Suid)
                              {
                                  sumpij += System.Math.Abs(fuheline[k].linepij - linepij);
                              }
                          }
                          //  
                          //    //output += "'" + CR.Name.ToString() + "," + (Convert.ToDouble(dev[3]) * capability).ToString() + "," + (Convert.ToDouble(dev[4]) * capability).ToString() + "," + (Convert.ToDouble(dev[5]) * capability).ToString() + "," + (Convert.ToDouble(dev[6]) * capability).ToString() + "," + (Convert.ToDouble(dev[7]) * current).ToString() + "," + (Convert.ToDouble(dev[8]) * Rad_to_Deg).ToString() + "," + dev[11] + "," + "\r\n";
                      }
                      strLine = readLine.ReadLine();
                  }
                  readLine.Close();
                  //if (lineflag)              //如果没有出现过负荷现象 就停止进行加线
                  //{
                  //    PSPDEV pspb = (PSPDEV)linedengdai[j];
                  //    pspb.LineStatus = "运行";
                  //    Services.BaseService.Update<PSPDEV>(pspb);
                  //    lineyiyou.Add(pspb);
                  //    for (int i = 0; i < linedengdai.Count; i++)
                  //    {
                  //        if (linedengdai[i].SUID == pspb.SUID)
                  //        {
                  //            linedengdai.RemoveAt(j);
                  //        }
                  //    }
                  //    return;
                  //}
                  //else
                  //{
                  XmlNode el = tlVectorControl1.SVGDocument.SelectSingleNode("svg/polyline[@layer='" + tlVectorControl1.SVGDocument.CurrentLayer.ID + "' and @id='" + linedengdai[j].EleID + "']");
                  double linevalue = 0;
                  if (!string.IsNullOrEmpty(((XmlElement)el).GetAttribute("linevalue")))
                  {
                      linevalue = Convert.ToDouble(((XmlElement)el).GetAttribute("linevalue"));
                  }
                  else
                      linevalue = 1;
                  linedaixuan linedai = new linedaixuan(linedengdai[j].Number, linedengdai[j].SUID, linedengdai[j].Name);
                  linedai.linepij = sumpij;
                  linedai.linevalue = linevalue;
                  linedai.linetouzilv = sumpij / linevalue;

                  waitlineindex.Add(linedai);
                  //}

              }
              waitlineindex.Sort();
              //在此处获得指标最大的线路 将其线路的状态变为 运行并且在运行的集合里面记录 在等待的集合里将其线路去掉 重新进行下一轮的操作
              PSPDEV pspbianhua = new PSPDEV();
              if (waitlineindex.Count > 0)
              {
                  pspbianhua.SUID = waitlineindex[waitlineindex.Count - 1].Suid;
              }
              else
              {
                  //MessageBox.Show("没有出现过负荷的线路集，请查看一下线路参数是否设定正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  return;
              }

              pspbianhua = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", pspbianhua);
              pspbianhua.LineStatus = "运行";
              Services.BaseService.Update<PSPDEV>(pspbianhua);
              lineyiyou.Add(pspbianhua);
              for (int i = 0; i < linedengdai.Count; i++)
              {
                  if (linedengdai[i].SUID == pspbianhua.SUID)
                  {
                      linedengdai.RemoveAt(i);
                  }
              }
              waitlineindex.Clear();
              goto AA;
          }
         
        }
        private bool fuhecheck()                 //记录负荷的线路
        {
            FileStream dh;
            StreamReader readLine;
            char[] charSplit;
            string strLine;
            string[] array1;
            string output = null;
            string[] array2;

            string strLine2;

            char[] charSplit2 = new char[] { ' ' };
            FileStream op;
            StreamWriter str1;
            FileStream dh2;
            StreamReader readLine2;
            fuheline.Clear();            //清空上一次的记录  为下一个时期的应用做准备


            // linedengdai.Clear();          //清空上一次记录待选线路 为下一个时期的应用做准备


            if (!Checkfuhe())
                return true;
            N1Test.NBcal zl = new NBcal();
            //zl.Show_KmRelia(1);
            zl.ZLpsp();



            double yinzi = 0, capability = 0, volt = 0, current = 0, standvolt = 0, Rad_to_Deg = 57.29577951;
            PSPDEV benchmark = new PSPDEV();
            benchmark.Type = "power";
            benchmark.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
            if (list3 == null)
            {
                MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            foreach (PSPDEV dev in list3)
            {
                yinzi = Convert.ToDouble(dev.PowerFactor);
                capability = Convert.ToDouble(dev.StandardCurrent);
                volt = Convert.ToDouble(dev.StandardVolt);
                if (dev.PowerFactor == 0)
                {
                    yinzi = 1;
                }
                if (dev.StandardCurrent == 0)
                {
                    capability = 1;
                }
                if (dev.StandardVolt == 0)
                {
                    volt = 1;
                }
                standvolt = volt;
                current = capability / (Math.Sqrt(3) * volt);

            }
            capability = 100;
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
            {
            }
            else
            {
                return true;
            }
            dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);

            readLine = new StreamReader(dh);
            charSplit = new char[] { ' ' };
            strLine = readLine.ReadLine();
            output = null;
            //double sumpij = 0.0;
            bool lineflag = true;
            while (!string.IsNullOrEmpty(strLine))
            {
                array1 = strLine.Split(charSplit);


                string[] dev = new string[2];
                dev.Initialize();
                int i = 0;
                PSPDEV CR = new PSPDEV();
                CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

                foreach (string str in array1)
                {
                    if (str != "")
                    {
                        if (i == 0)
                        {
                            dev[i++] = str.ToString();
                        }
                        else
                        {
                            if (str != "NaN")
                            {
                                dev[i++] = Convert.ToDouble(str).ToString();
                            }
                            else
                            {
                                dev[i++] = str;
                            }

                        }
                    }

                }

                CR.Name = dev[0];
                CR.Type = "Polyline";
                CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", CR);
                if (CR != null && CR.ReferenceVolt != 0)
                {
                    volt = CR.ReferenceVolt;
                }
                else
                    volt = standvolt;
                current = capability / (Math.Sqrt(3) * volt);
                if (CR != null && !CR.Name.Contains("虚拟线路"))
                {


                    double linepij = System.Math.Abs(Convert.ToDouble(dev[1])) * capability;
                    double voltR = CR.VoltR;
                    WireCategory wirewire = new WireCategory();
                    wirewire.WireType = CR.LineType;
                    if (string.IsNullOrEmpty(CR.LineType))
                    {
                        MessageBox.Show(CR.Name + "的线路类型没有输入，无法进行可靠性检验", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    WireCategory listware = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKey", wirewire);
                    double Ichange = (double)listware.WireChange;
                    double linXij = System.Math.Sqrt(3) * voltR * Ichange / 1000;
                    if (linepij >= linXij)
                    {
                        lineflag = false;
                        linedaixuan ld = new linedaixuan(CR.Number, CR.SUID, CR.Name);
                        ld.linepij = linepij;
                        fuheline.Add(ld);

                    }
                    
                    //  
                    //    //output += "'" + CR.Name.ToString() + "," + (Convert.ToDouble(dev[3]) * capability).ToString() + "," + (Convert.ToDouble(dev[4]) * capability).ToString() + "," + (Convert.ToDouble(dev[5]) * capability).ToString() + "," + (Convert.ToDouble(dev[6]) * capability).ToString() + "," + (Convert.ToDouble(dev[7]) * current).ToString() + "," + (Convert.ToDouble(dev[8]) * Rad_to_Deg).ToString() + "," + dev[11] + "," + "\r\n";
                }
                strLine = readLine.ReadLine();
            }
            readLine.Close();
            return lineflag;
        }
        private void addrightcheck()
        {
            FileStream dh;
            StreamReader readLine;
            char[] charSplit;
            string strLine;
            string[] array1;
            string output = null;
            string[] array2;

            string strLine2;

            char[] charSplit2 = new char[] { ' ' };
            FileStream op;
            StreamWriter str1;
            FileStream dh2;
            StreamReader readLine2;
            //List<PSPDEV> linedengdai = new List<PSPDEV>();          //记录所有待选的线路（一次运行以后就会发生变化）
            if (!Check())
            {
                return;
            }
            PSPDEV pspdev = new PSPDEV();
            pspdev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            pspdev.LineStatus = "运行";
            pspdev.Type = "Polyline";
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndLineStatus", pspdev);
            double yinzi = 0, capability = 0, volt = 0, current = 0, standvolt = 0, Rad_to_Deg = 57.29577951;
            PSPDEV benchmark = new PSPDEV();
            benchmark.Type = "power";
            benchmark.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
            if (list3 == null)
            {
                MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (PSPDEV dev in list3)
            {
                yinzi = Convert.ToDouble(dev.PowerFactor);
                capability = Convert.ToDouble(dev.StandardCurrent);
                volt = Convert.ToDouble(dev.StandardVolt);
                if (dev.PowerFactor == 0)
                {
                    yinzi = 1;
                }
                if (dev.StandardCurrent == 0)
                {
                    capability = 1;
                }
                if (dev.StandardVolt == 0)
                {
                    volt = 1;
                }
                standvolt = volt;
                current = capability / (Math.Sqrt(3) * volt);

            }
            capability = 100;
            ercilinedengdai.Clear();//清空之前的记录


            for (int i = 0; i < list1.Count; i++)
            {
                N1Test.NBcal kk = new N1Test.NBcal();
                kk.Show_ZLKMPSP(((PSPDEV)list1[i]).Number);
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ZL.txt"))
                {
                }
                else
                {
                    return;
                }
                dh2 = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ZL.txt", FileMode.Open);

                readLine2 = new StreamReader(dh2);
                charSplit2 = new char[] { ' ' };
                strLine2 = readLine2.ReadLine();
                output = null;
                bool lineflag = true;
                bool jielieflag = true;
                while (!string.IsNullOrEmpty(strLine2))
                {
                    array2 = strLine2.Split(charSplit2);


                    string[] dev = new string[2 * brchcount + 1];
                    dev.Initialize();

                    PSPDEV CR = new PSPDEV();
                    CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    int m = 0;
                    foreach (string str in array2)
                    {

                        if (str != "")
                        {

                            dev[m++] = str.ToString();

                        }
                    }
                    //进行解裂和负荷判断



                    if (dev[1] != "-1")
                    {
                        for (int j = 0; j < brchcount; j++)
                        {

                            double pij = System.Math.Abs(Convert.ToDouble(dev[j * 2 + 2])) * capability;

                            PSPDEV psp = new PSPDEV();

                            psp.Name = dev[j * 2 + 1];
                            psp.Type = "Polyline";
                            psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", psp);
                            PSPDEV pspline = (PSPDEV)listName[0];
                            if (pspline != null && !pspline.Name.Contains("虚拟线路"))
                            {
                                double voltR = pspline.VoltR;
                                WireCategory wirewire = new WireCategory();
                                wirewire.WireType = pspline.LineType;
                                if (pspline.LineType == null || pspline.LineType == "")
                                {
                                    MessageBox.Show(pspline.Name + "的线路类型没有输入，无法进行可靠性检验", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                WireCategory listware = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKey", wirewire);
                                double Ichange = (double)listware.WireChange;
                                double linXij = System.Math.Sqrt(3) * voltR * Ichange / 1000;
                                if (pij >= linXij)
                                {
                                    lineflag = false;
                                    //lineclass _line = new lineclass(n, j);
                                    //Overlinp.Add(_line);
                                    // OverPhege[n] = j;
                                }
                            }


                        }

                    }

                    else
                    {
                        jielieflag = false;
                    }


                    strLine2 = readLine2.ReadLine();
                }
                readLine2.Close();
                if (!lineflag)
                {
                    PSPDEV psperci = new PSPDEV();
                    psperci = (PSPDEV)list1[i];
                    psperci.LineStatus = "等待";
                    Services.BaseService.Update<PSPDEV>(psperci);
                    ercilinedengdai.Add(psperci);
                    break;
                }
                else                  //没有出现过负荷
                    continue;
            }
            //新添加的 如果记录二次等待的线路没有则停止下面的运行 表示断开任意一条也没有出现过负荷 
            if (ercilinedengdai.Count == 0)
            {
                return;
            }
            //此过程是添加一条线路使其不出现过负荷

            fuhecheck();
            for (int j = 0; j < linedengdai.Count; j++)
            {
                if (!Checkadd(linedengdai[j].EleID))
                    return;
                N1Test.NBcal zl = new NBcal();
                //zl.Show_KmRelia(1);
                zl.ZLpsp();




                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                {
                }
                else
                {
                    return;
                }
                dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);

                readLine = new StreamReader(dh);
                charSplit = new char[] { ' ' };
                strLine = readLine.ReadLine();
                output = null;
                double sumpij = 0.0;
                bool lineflag = true;
                while (!string.IsNullOrEmpty(strLine))
                {
                    array1 = strLine.Split(charSplit);


                    string[] dev = new string[2];
                    dev.Initialize();
                    int i = 0;
                    PSPDEV CR = new PSPDEV();
                    CR.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

                    foreach (string str in array1)
                    {
                        if (str != "")
                        {
                            if (i == 0)
                            {
                                dev[i++] = str.ToString();
                            }
                            else
                            {
                                if (str != "NaN")
                                {
                                    dev[i++] = Convert.ToDouble(str).ToString();
                                }
                                else
                                {
                                    dev[i++] = str;
                                }

                            }
                        }

                    }

                    CR.Name = dev[0];
                    CR.Type = "Polyline";
                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", CR);
                    if (CR != null && CR.ReferenceVolt != 0)
                    {
                        volt = CR.ReferenceVolt;
                    }
                    else
                        volt = standvolt;
                    current = capability / (Math.Sqrt(3) * volt);
                    if (CR != null && !CR.Name.Contains("虚拟线路"))
                    {


                        double linepij = Convert.ToDouble(dev[1]) * capability;
                        double voltR = CR.VoltR;
                        WireCategory wirewire = new WireCategory();
                        wirewire.WireType = CR.LineType;
                        if (string.IsNullOrEmpty(CR.LineType))
                        {
                            MessageBox.Show(CR.Name + "的线路类型没有输入，无法进行可靠性检验", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        WireCategory listware = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKey", wirewire);
                        double Ichange = (double)listware.WireChange;
                        double linXij = System.Math.Sqrt(3) * voltR * Ichange / 1000;
                        if (linepij >= linXij)
                        {
                            lineflag = false;
                            
                            //sumpij += linepij;
                        }
                        for (int k = 0; k < fuheline.Count; k++)
                        {
                            if (CR.SUID == fuheline[k].Suid)
                            {
                                sumpij += System.Math.Abs(fuheline[k].linepij - linepij);
                            }
                        }
                        //  
                        //    //output += "'" + CR.Name.ToString() + "," + (Convert.ToDouble(dev[3]) * capability).ToString() + "," + (Convert.ToDouble(dev[4]) * capability).ToString() + "," + (Convert.ToDouble(dev[5]) * capability).ToString() + "," + (Convert.ToDouble(dev[6]) * capability).ToString() + "," + (Convert.ToDouble(dev[7]) * current).ToString() + "," + (Convert.ToDouble(dev[8]) * Rad_to_Deg).ToString() + "," + dev[11] + "," + "\r\n";
                    }
                   
                    strLine = readLine.ReadLine();
                }
                readLine.Close();
                //if (lineflag)              //如果没有出现过负荷现象 就停止进行加线
                //{
                //    PSPDEV pspb = (PSPDEV)linedengdai[j];
                //    pspb.LineStatus = "运行";
                //    Services.BaseService.Update<PSPDEV>(pspb);
                //    lineyiyou.Add(pspb);
                //    for (int i = 0; i < linedengdai.Count; i++)
                //    {
                //        if (linedengdai[i].SUID == pspb.SUID)
                //        {
                //            linedengdai.RemoveAt(j);
                //        }
                //    }
                //    return;
                //}
                //else
                //{
                    XmlNode el = tlVectorControl1.SVGDocument.SelectSingleNode("svg/polyline[@layer='" + tlVectorControl1.SVGDocument.CurrentLayer.ID + "' and @id='" + linedengdai[j].EleID + "']");
                    double linevalue = 0;
                    if (!string.IsNullOrEmpty(((XmlElement)el).GetAttribute("linevalue")))
                    {
                        linevalue = Convert.ToDouble(((XmlElement)el).GetAttribute("linevalue"));
                    }
                    else
                        linevalue = 1;
                    linedaixuan linedai = new linedaixuan(linedengdai[j].Number, linedengdai[j].SUID, linedengdai[j].Name);
                    linedai.linepij = sumpij;
                    linedai.linevalue = linevalue;
                    linedai.linetouzilv = sumpij / linevalue;

                    waitlineindex.Add(linedai);
                //}

            }
            waitlineindex.Sort();
            //在此处获得指标最大的线路 将其线路的状态变为 运行并且在运行的集合里面记录 在等待的集合里将其线路去掉 重新进行下一轮的操作
            PSPDEV pspbianhua = new PSPDEV();
            if (waitlineindex.Count > 0)
            {
                pspbianhua.SUID = waitlineindex[waitlineindex.Count - 1].Suid;
            }
            else
            {
                //MessageBox.Show("没有出现过负荷的线路集，请查看一下线路参数是否设定正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            pspbianhua = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", pspbianhua);
            pspbianhua.LineStatus = "运行";
            Services.BaseService.Update<PSPDEV>(pspbianhua);
            lineyiyou.Add(pspbianhua);
            for (int i = 0; i < linedengdai.Count; i++)
            {
                if (linedengdai[i].SUID == pspbianhua.SUID)
                {
                    linedengdai.RemoveAt(i);
                }
            }
            waitlineindex.Clear();

        }
        void ComboBoxScaleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text1 = this.scaleBox.SelectedItem.ToString();
            tlVectorControl1.ScaleRatio = ItopVector.Core.Func.Number.ParseFloatStr(text1);

        }
        void tlVectorControl1_ScaleChanged(object sender, EventArgs e)
        {
            string text1 = (((ItopVector.ItopVectorControl)sender).ScaleRatio * 100) + "%";
            scaleBox.ComboBoxEx.SelectedIndexChanged -= new EventHandler(ComboBoxScaleBox_SelectedIndexChanged);
            this.scaleBox.ComboBoxEx.Text = text1;
            scaleBox.ComboBoxEx.SelectedIndexChanged += new EventHandler(ComboBoxScaleBox_SelectedIndexChanged);
            if (tlVectorControl1.ScaleRatio < 0.006f)
            {
                tlVectorControl1.ScaleRatio = 0.006f;
                scaleBox.ComboBoxEx.Text = "0.6%";
                //scaleBox.SelectedText = "10%";
            }
        }
        private void AddCombolScale()
        {
            //缩放大小
            scaleBox = this.dotNetBarManager1.GetItem("ScaleBox") as DevComponents.DotNetBar.ComboBoxItem;
            if (scaleBox != null)
            {
                scaleBox.Items.AddRange(ScaleRange());
                scaleBox.ComboBoxEx.Text = "100%";
                scaleBox.ComboBoxEx.SelectedIndexChanged += new EventHandler(ComboBoxScaleBox_SelectedIndexChanged);

            }
        }
        public string[] ScaleRange()
        {
            string[] range1 = new string[] { "400%", "200%", "100%", "40%", "20%", "10%", "4%", "2%", "1%" };
            return range1;
        }
        private void tlVectorControl1_Load(object sender, EventArgs e)
        {
            AddCombolScale();
            LoadShape("symbol20.xml");
            Topology2();

        }

        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            Process[] ps = Process.GetProcessesByName("ChaoLiu");
            if (ps.Length > 0)
            {
                foreach (Process pre in ps)
                {
                    pre.Kill();
                }
            }
        }
          private void PspN_RZYz()
        {
            if (!Check())
            {
                return;
            }
            try
            {
                XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@flag='" + "1" + "']");

                foreach (XmlNode node in list)
                {
                    SvgElement element = node as SvgElement;
                    tlVectorControl1.SVGDocument.CurrentElement = element;
                    tlVectorControl1.Delete();
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF4.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF4.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH4.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH4.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH4.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH4.txt");
                }

                ZYZ zyz = new ZYZ();

                zyz.CurrentCal();

                double yinzi = 0, capability = 0, volt = 0, current = 0;
                PSPDEV benchmark = new PSPDEV();
                benchmark.Type = "power";
                benchmark.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
                if (list3 == null)
                {
                    MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (PSPDEV dev in list3)
                {
                    yinzi = Convert.ToDouble(dev.PowerFactor);
                    capability = Convert.ToDouble(dev.StandardCurrent);
                    volt = Convert.ToDouble(dev.StandardVolt);
                    if (dev.PowerFactor == 0)
                    {
                        yinzi = 1;
                    }
                    if (dev.StandardCurrent == 0)
                    {
                        capability = 1;
                    }
                    if (dev.StandardVolt == 0)
                    {
                        volt = 1;
                    }
                    current = capability / (Math.Sqrt(3) * volt);
                };
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF4.txt") && File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH4.txt") && File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH4.txt"))
                {
                }
                else
                {
                    MessageBox.Show("数据不收敛，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF4.txt", FileMode.Open);
                StreamReader readLine = new StreamReader(dh);
                string strLine;
                string[] array1;
                char[] charSplit = new char[] { ' ' };
                strLine = readLine.ReadLine();
                string octor = "节点电压 ";

                while (strLine != null)
                {
                    array1 = strLine.Split(charSplit);
                    string[] dev = new string[8];
                    dev.Initialize();
                    int i = 0;
                    foreach (string str in array1)
                    {
                        if (str != "")
                        {
                            dev[i++] = str;
                        }
                        if (str.Contains("NAN"))
                        {
                            MessageBox.Show("参数错误，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.Number = Convert.ToInt32(Convert.ToDouble(dev[0]));
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.Type = "Use";
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByNumberAndSvgUIDAndType", pspDev);
                    if (pspDev != null)
                    {
                        if (pspDev.Burthen == 0)//如果容量为０当作T接点跳过
                        {
                            strLine = readLine.ReadLine();
                            continue;
                        }
                        XmlElement element = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + pspDev.EleID + "']") as XmlElement;
                        if (element != null)
                        {
                            RectangleF bound = ((IGraph)element).GetBounds();
                            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            n1.SetAttribute("x", Convert.ToString(bound.X));
                            n1.SetAttribute("y", Convert.ToString(bound.Y - 20));

                            n1.InnerText = (Convert.ToDouble(dev[1]) * volt).ToString("N2");
                            octor += " ";
                            octor += Convert.ToString(n1.InnerText);
                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                            //MessageBox.Show(Convert.ToString(n1.InnerText));
                            n1.SetAttribute("flag", "1");

                            if (Convert.ToDouble(dev[1]) > 1.05 || Convert.ToDouble(dev[1]) < 0.95)//电压越限，需修改
                                n1.SetAttribute("stroke", "#FF0000");
                            if (pspDev.NodeType == "0")
                            {
                                if (Convert.ToDouble(dev[4]) >= 0)
                                {
                                    double tempb = Convert.ToDouble(pspDev.Burthen);
                                    n2.SetAttribute("x", Convert.ToString(bound.X));
                                    n2.SetAttribute("y", Convert.ToString(bound.Y + bound.Height + 20));
                                    n2.InnerText = ((Convert.ToDouble(dev[3]) * capability).ToString("N2") + "  + " + "j" + (Convert.ToDouble(dev[4]) * capability).ToString("N2"));
                                    n2.SetAttribute("layer", SvgDocument.currentLayer);
                                    n2.SetAttribute("flag", "1");
                                    double tempi = Convert.ToDouble(dev[3]) * capability;
                                    double tempj = Convert.ToDouble(dev[4]) * capability;
                                    double temptotal = Math.Sqrt(tempi * tempi + tempj * tempj);
                                    if (temptotal > Convert.ToDouble(pspDev.Burthen))
                                    {
                                        n2.SetAttribute("stroke", "#FF0000");
                                    }
                                }
                                else
                                {
                                    double tempb = Convert.ToDouble(pspDev.Burthen);
                                    n2.SetAttribute("x", Convert.ToString(bound.X));
                                    n2.SetAttribute("y", Convert.ToString(bound.Y + bound.Height + 15));
                                    n2.InnerText = (Convert.ToDouble(dev[3]) * capability).ToString("N2") + " - " + "j" + (Math.Abs(Convert.ToDouble(dev[4]) * capability)).ToString("N2");
                                    n2.SetAttribute("layer", SvgDocument.currentLayer);
                                    n2.SetAttribute("flag", "1");
                                    double tempi = Convert.ToDouble(dev[3]) * capability;
                                    double tempj = Convert.ToDouble(dev[4]) * capability;
                                    double temptotal = Math.Sqrt(tempi * tempi + tempj * tempj);
                                    if (temptotal > Convert.ToDouble(pspDev.Burthen))
                                    {
                                        n2.SetAttribute("stroke", "#FF0000");
                                    }
                                }
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);
                            }
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            tlVectorControl1.Operation = ToolOperation.Select;
                            tlVectorControl1.Refresh();

                        }

                    }

                    strLine = readLine.ReadLine();
                }
                readLine.Close();

                //MessageBox.Show(octor);
                octor = "线路电流 ";

                FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH4.txt", FileMode.Open);
                StreamReader ihLine = new StreamReader(ih);
                FileStream dhdh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH4.txt", FileMode.Open);
                StreamReader dhLine = new StreamReader(dhdh);
                string strIH;
                string strDH;
                string[] array2;
                string[] array3;
                strIH = ihLine.ReadLine();
                strDH = dhLine.ReadLine();
                while (strIH != null && strDH != null)
                {

                    array2 = strIH.Split(charSplit);
                    array3 = strDH.Split(charSplit);
                    string[] dev = new string[8];
                    string[] devDH = new string[13];
                    dev.Initialize();
                    devDH.Initialize();
                    int i = 0;
                    foreach (string str in array2)
                    {
                        if (str != "")
                        {
                            dev[i++] = str;
                        }
                        if (str.Contains("NAN"))
                        {
                            MessageBox.Show("参数错误，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    int j = 0;
                    foreach (string str in array3)
                    {
                        if (str != "")
                        {
                            devDH[j++] = str;
                        }
                    }
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.Name = dev[0];
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.Type = "Polyline";
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", pspDev);
                    if (pspDev != null && pspDev.LineStatus == "运行")
                    {
                        XmlElement element = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + pspDev.EleID + "']") as XmlElement;
                        if (element != null)
                        {
                            PointF[] t = ((Polyline)element).Points;

                            PointF[] t2 = ((Polyline)element).FirstTwoPoint;
                            t = t2;

                            PointF midt = new PointF((float)((t2[0].X + t2[1].X) / 2), (float)((t2[0].Y + t2[1].Y) / 2));
                            float angel = 0f;
                            angel = (float)(180 * Math.Atan2((t2[1].Y - t2[0].Y), (t2[1].X - t2[0].X)) / Math.PI);

                            string l3 = Convert.ToString(midt.X);
                            string l4 = Convert.ToString(midt.Y);

                            string tran = ((Polyline)element).Transform.ToString();

                            PointF center = new PointF((float)(t[0].X + (t[1].X - t[0].X) / 2), (float)(t[0].Y + (t[1].Y - t[0].Y) / 2));
                            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                            XmlElement n3 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;

                            PointF pStart = new PointF(center.X + (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(15 * Math.Cos((angel) * Math.PI / 180)));
                            PointF pStart2 = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                            PSPDEV psp = new PSPDEV();
                            psp.FirstNode = pspDev.FirstNode;
                            psp.LastNode = pspDev.LastNode;
                            psp.SvgUID = pspDev.SvgUID;
                            PSPDEV tempss = new PSPDEV();
                            IList listParallel = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandFirstOrLastNode", psp);
                            foreach (PSPDEV devP in listParallel)
                            {
                                if ((angel > 10 && angel < 90) || (angel < 0 && Math.Abs(angel) < 90) || (angel > 180 && angel < 350))
                                {
                                    if (((devP.X1) > (pspDev.X1)))
                                    {
                                        pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                        pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    }
                                }
                                else if ((angel >= 0 && angel <= 10) || (angel >= 350 && angel <= 360) || (angel < 0 && Math.Abs(angel) <= 90))
                                {
                                    if (((devP.Y1) > (pspDev.Y1)))
                                    {
                                        pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                        pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    }
                                }
                                else if ((angel < 0 && Math.Abs(angel) > 90) || (angel >= 90 && angel <= 180))
                                {
                                    if (((devP.Y1) > (pspDev.Y1)))
                                    {
                                        pStart = new PointF(center.X - (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                                        pStart2 = new PointF(center.X + (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                                    }
                                }

                                //if ((Math.Abs(angel) > 90))
                                //{

                                //    if (((devP.X1 + devP.Y1) > (pspDev.X1 + pspDev.Y1)))
                                //    {
                                //        pStart = new PointF(center.X - (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(15 * Math.Cos((angel) * Math.PI / 180)));
                                //    }
                                //    else
                                //    {
                                //        pStart = new PointF(center.X + (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(15 * Math.Cos((angel) * Math.PI / 180)));

                                //    }
                                //}
                            }

                            PointF newp1 = new PointF(t[0].X + (t[1].X - t[0].X) / 2 - (float)(15 * Math.Sin(angel)), t[0].Y + (t[1].Y - t[0].Y) / 2 - (float)(15 * Math.Cos(angel)));

                            n1.SetAttribute("x", Convert.ToString(pStart.X));
                            n1.SetAttribute("y", Convert.ToString(pStart.Y));
                            //n3.SetAttribute("x", Convert.ToString(pStart2.X));
                            //n3.SetAttribute("y", Convert.ToString(pStart2.Y));
                            //double temp = (Convert.ToDouble(devDH[6]) + Convert.ToDouble(devDH[10]) * volt * volt / 1000000) * capability;
                            //if (temp >= 0)
                            //{
                            //    n3.InnerText = ((Convert.ToDouble(devDH[5]) + Convert.ToDouble(devDH[9]) * volt * volt / 1000000) * capability).ToString() + " + " + "j" + temp.ToString();
                            //}
                            //else
                            //{
                            //    n3.InnerText = ((Convert.ToDouble(devDH[5]) + Convert.ToDouble(devDH[9]) * volt * volt / 1000000) * capability).ToString() + " - " + "j" + (Math.Abs(temp)).ToString();
                            //}

                            //n3.SetAttribute("layer", SvgDocument.currentLayer);
                            //n3.SetAttribute("flag", "1");
                            //n3.SetAttribute("stroke", "#0000FF");
                            //n1.SetAttribute("x", Convert.ToString(t[0].X + (t[1].X - t[0].X) / 2));
                            //n1.SetAttribute("y", Convert.ToString(t[0].Y + (t[1].Y - t[0].Y) / 2));
                            if (Convert.ToDouble(devDH[4]) >= 0)
                            {
                                n1.InnerText = (Math.Abs(Convert.ToDouble(devDH[3]) * capability)).ToString("N2") + " + j" + (Math.Abs(Convert.ToDouble(devDH[4]) * capability)).ToString("N2");
                            }
                            else
                            {
                                n1.InnerText = (Math.Abs(Convert.ToDouble(devDH[3]) * capability)).ToString("N2") + " - j" + (Math.Abs(Convert.ToDouble(devDH[4]) * capability)).ToString("N2");
                            }
                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                            n1.SetAttribute("flag", "1");
                            if (listParallel != null)
                            {
                                if (Convert.ToDouble(dev[3]) > ((PSPDEV)listParallel[0]).LineChange)//电流越限，需修改。









                                    n1.SetAttribute("stroke", "#FF0000");
                            }


                            PointF p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 25) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 25) * Math.PI / 180)));
                            PointF p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 335) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 335) * Math.PI / 180)));

                            if (Convert.ToDouble(devDH[3]) < 0)
                            {
                                p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 155) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 155) * Math.PI / 180)));
                                p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 205) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 205) * Math.PI / 180)));
                            }


                            string l1 = Convert.ToString(p1.X);
                            string l2 = Convert.ToString(p1.Y);
                            string l5 = Convert.ToString(p2.X);
                            string l6 = Convert.ToString(p2.Y);

                            //n2.SetAttribute("stroke", "#FF0000");
                            n2.SetAttribute("points", l1 + " " + l2 + "," + l3 + " " + l4 + "," + l5 + " " + l6);
                            n2.SetAttribute("fill-opacity", "1");
                            n2.SetAttribute("layer", SvgDocument.currentLayer);
                            n2.SetAttribute("flag", "1");
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);
                            tlVectorControl1.SVGDocument.CurrentElement = n2 as SvgElement;

                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            tlVectorControl1.Operation = ToolOperation.Select;

                            tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;

                            //if (Convert.ToDouble(dev[3]) <= 0)
                            RectangleF ttt = ((Polyline)element).GetBounds();

                            tlVectorControl1.RotateSelection(angel, pStart);
                            if (Math.Abs(angel) > 90)
                                tlVectorControl1.RotateSelection(180, pStart);
                            //tlVectorControl1.RotateSelection((float)(Math.Atan((t[1].Y - t[0].Y) / (t[1].X - t[0].X)) * 180 / Math.PI), pt4[0]);
                            //tlVectorControl1.RotateSelection(-10, (new PointF(center.X+10,center.Y+10)));
                            //tlVectorControl1.SVGDocument.RootElement.AppendChild(n3);
                            //tlVectorControl1.Operation = ToolOperation.Select;
                            //tlVectorControl1.SVGDocument.CurrentElement = n3 as SvgElement;
                            //tlVectorControl1.RotateSelection(360 + angel, pStart2);
                            //if (Math.Abs(angel) > 90)
                            //    tlVectorControl1.RotateSelection(-180, pStart2);

                            PointF newp = new PointF(center.X + 10, center.Y + 10);


                            tlVectorControl1.Refresh();

                        }

                    }
                    strIH = ihLine.ReadLine();
                    strDH = dhLine.ReadLine();
                }

                ihLine.Close();
                dhLine.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("参数错误，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void PspNIULA()
        {
            if (!Check())
            {
                return;
            }
            try
            {
                XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@flag='" + "1" + "']");

                foreach (XmlNode node in list)
                {
                    SvgElement element = node as SvgElement;
                    tlVectorControl1.SVGDocument.CurrentElement = element;
                    tlVectorControl1.Delete();
                }

                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF1.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\PF1.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\DH1.txt");
                }
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH1.txt"))
                {
                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\IH1.txt");
                }


                NIULA niulaP = new NIULA();

                niulaP.CurrentCal();

                double yinzi = 0, capability = 0, volt = 0, standvolt = 0, current = 0;
                PSPDEV benchmark = new PSPDEV();
                benchmark.Type = "power";
                benchmark.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
                if (list3 == null)
                {
                    MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (PSPDEV dev in list3)
                {
                    yinzi = Convert.ToDouble(dev.PowerFactor);
                    capability = Convert.ToDouble(dev.StandardCurrent);
                    volt = Convert.ToDouble(dev.StandardVolt);
                    if (dev.PowerFactor == 0)
                    {
                        yinzi = 1;
                    }
                    if (dev.StandardCurrent == 0)
                    {
                        capability = 1;
                    }
                    if (dev.StandardVolt == 0)
                    {
                        volt = 1;
                    }
                    standvolt = volt;
                    current = capability / (Math.Sqrt(3) * volt);
                };
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\PF1.txt") && File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt") && File.Exists(System.Windows.Forms.Application.StartupPath + "\\IH1.txt"))
                {
                }
                else
                {
                    MessageBox.Show("数据不收敛，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\PF1.txt", FileMode.Open);
                StreamReader readLine = new StreamReader(dh);
                string strLine;
                string[] array1;
                char[] charSplit = new char[] { ' ' };
                strLine = readLine.ReadLine();
                string octor = "节点电压 ";

                while (strLine != null)
                {
                    array1 = strLine.Split(charSplit);
                    string[] dev = new string[8];
                    dev.Initialize();
                    int i = 0;
                    foreach (string str in array1)
                    {
                        if (str != "")
                        {
                            dev[i++] = str;
                        }
                        if (str.Contains("NAN"))
                        {
                            MessageBox.Show("线路和节点参数错误，输入的数据形成的导纳矩阵没有逆矩阵，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.Number = Convert.ToInt32(Convert.ToDouble(dev[0]));
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.Type = "Use";
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByNumberAndSvgUIDAndType", pspDev);

                    if (pspDev != null)
                    {
                        if (pspDev.ReferenceVolt != null && pspDev.ReferenceVolt != 0)
                        {
                            volt = pspDev.ReferenceVolt;
                        }
                        else
                            volt = standvolt;
                        current = capability / (Math.Sqrt(3) * volt);
                        if (pspDev.Name.Substring(0, 2) == "T_")//如果容量为０当作T接点跳过
                        {
                            strLine = readLine.ReadLine();
                            continue;
                        }
                        XmlElement element = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + pspDev.EleID + "']") as XmlElement;

                        if (element != null)
                        {
                            RectangleF bound = ((IGraph)element).GetBounds();
                            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            n1.SetAttribute("x", Convert.ToString(bound.X));
                            n1.SetAttribute("y", Convert.ToString(bound.Y - 20));
                            n1.SetAttribute("font-size", "6");
                            n1.InnerText = (Convert.ToDouble(dev[1]) * volt).ToString("N2");
                            octor += " ";
                            octor += Convert.ToString(n1.InnerText);
                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                            //MessageBox.Show(Convert.ToString(n1.InnerText));
                            n1.SetAttribute("flag", "1");

                            if (Convert.ToDouble(dev[1]) > 1.10 || Convert.ToDouble(dev[1]) < 0.90)//电压越限，需修改
                                n1.SetAttribute("stroke", "#FF0000");
                            if (pspDev.NodeType == "0")
                            {
                                if (Convert.ToDouble(dev[4]) >= 0)
                                {
                                    double tempb = Convert.ToDouble(pspDev.Burthen);
                                    n2.SetAttribute("x", Convert.ToString(bound.X));
                                    n2.SetAttribute("y", Convert.ToString(bound.Y + bound.Height + 20));
                                    n2.InnerText = ((Convert.ToDouble(dev[3]) * capability).ToString("N2") + "  + " + "j" + (Convert.ToDouble(dev[4]) * capability).ToString("N2"));
                                    n2.SetAttribute("layer", SvgDocument.currentLayer);
                                    n2.SetAttribute("flag", "1");
                                    n2.SetAttribute("font-size", "6");
                                    double tempi = Convert.ToDouble(dev[3]) * capability;
                                    double tempj = Convert.ToDouble(dev[4]) * capability;
                                    double temptotal = Math.Sqrt(tempi * tempi + tempj * tempj);
                                    if (temptotal > Convert.ToDouble(pspDev.Burthen))
                                    {
                                        n2.SetAttribute("stroke", "#FF0000");
                                    }
                                }
                                else
                                {
                                    double tempb = Convert.ToDouble(pspDev.Burthen);
                                    n2.SetAttribute("x", Convert.ToString(bound.X));
                                    n2.SetAttribute("y", Convert.ToString(bound.Y + bound.Height + 15));
                                    n2.InnerText = (Convert.ToDouble(dev[3]) * capability).ToString("N2") + " - " + "j" + (Math.Abs(Convert.ToDouble(dev[4]) * capability)).ToString("N2");
                                    n2.SetAttribute("layer", SvgDocument.currentLayer);
                                    n2.SetAttribute("flag", "1");
                                    n2.SetAttribute("font-size", "6");
                                    double tempi = Convert.ToDouble(dev[3]) * capability;
                                    double tempj = Convert.ToDouble(dev[4]) * capability;
                                    double temptotal = Math.Sqrt(tempi * tempi + tempj * tempj);
                                    if (temptotal > Convert.ToDouble(pspDev.Burthen))
                                    {
                                        n2.SetAttribute("stroke", "#FF0000");
                                    }
                                }
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);
                            }
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            tlVectorControl1.Operation = ToolOperation.Select;
                            tlVectorControl1.Refresh();

                        }

                    }

                    strLine = readLine.ReadLine();
                }
                readLine.Close();

                //MessageBox.Show(octor);
                octor = "线路电流 ";

                FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\IH1.txt", FileMode.Open);
                StreamReader ihLine = new StreamReader(ih);
                FileStream dhdh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);
                StreamReader dhLine = new StreamReader(dhdh);
                string strIH;
                string strDH;
                string[] array2;
                string[] array3;
                strIH = ihLine.ReadLine();
                strDH = dhLine.ReadLine();
                while (strIH != null && strDH != null)
                {

                    array2 = strIH.Split(charSplit);
                    array3 = strDH.Split(charSplit);
                    string[] dev = new string[8];
                    string[] devDH = new string[13];
                    dev.Initialize();
                    devDH.Initialize();
                    int i = 0;
                    foreach (string str in array2)
                    {
                        if (str != "")
                        {
                            dev[i++] = str;
                        }
                        if (str.Contains("NAN"))
                        {
                            MessageBox.Show("参数错误，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    int j = 0;
                    foreach (string str in array3)
                    {
                        if (str != "")
                        {
                            devDH[j++] = str;
                        }
                        if (str.Contains("NAN"))
                        {
                            MessageBox.Show("参数错误，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    PSPDEV pspDev = new PSPDEV();
                    pspDev.Name = dev[0];
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.Type = "Polyline";
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", pspDev);
                    if (pspDev != null && pspDev.LineStatus == "运行")
                    {
                        if (pspDev.ReferenceVolt != null && pspDev.ReferenceVolt != 0)
                        {
                            volt = pspDev.ReferenceVolt;
                        }
                        else
                            volt = standvolt;
                        XmlElement element = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + pspDev.EleID + "']") as XmlElement;
                        if (element != null)
                        {
                            PointF[] t = ((Polyline)element).Points;

                            PointF[] t2 = ((Polyline)element).FirstTwoPoint;
                            t = t2;

                            PointF midt = new PointF((float)((t2[0].X + t2[1].X) / 2), (float)((t2[0].Y + t2[1].Y) / 2));
                            float angel = 0f;
                            angel = (float)(180 * Math.Atan2((t2[1].Y - t2[0].Y), (t2[1].X - t2[0].X)) / Math.PI);

                            string l3 = Convert.ToString(midt.X);
                            string l4 = Convert.ToString(midt.Y);

                            string tran = ((Polyline)element).Transform.ToString();

                            PointF center = new PointF((float)(t[0].X + (t[1].X - t[0].X) / 2), (float)(t[0].Y + (t[1].Y - t[0].Y) / 2));
                            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                            //XmlElement n3 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;

                            PointF pStart = new PointF(center.X + (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(15 * Math.Cos((angel) * Math.PI / 180)));
                            PointF pStart2 = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                            PSPDEV psp = new PSPDEV();
                            psp.FirstNode = pspDev.FirstNode;
                            psp.LastNode = pspDev.LastNode;
                            psp.SvgUID = pspDev.SvgUID;
                            PSPDEV tempss = new PSPDEV();
                            IList listParallel = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandFirstOrLastNode", psp);
                            foreach (PSPDEV devP in listParallel)
                            {
                                if ((angel > 10 && angel < 90) || (angel < 0 && Math.Abs(angel) < 90) || (angel > 180 && angel < 350))
                                {
                                    if (((devP.X1) > (pspDev.X1)))
                                    {
                                        pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                        pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    }
                                }
                                else if ((angel >= 0 && angel <= 10) || (angel >= 350 && angel <= 360) || (angel < 0 && Math.Abs(angel) <= 90))
                                {
                                    if (((devP.Y1) > (pspDev.Y1)))
                                    {
                                        pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                        pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    }
                                }
                                else if ((angel < 0 && Math.Abs(angel) > 90) || (angel >= 90 && angel <= 180))
                                {
                                    if (((devP.Y1) > (pspDev.Y1)))
                                    {
                                        pStart = new PointF(center.X - (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                                        pStart2 = new PointF(center.X + (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                                    }
                                }

                                //if ((Math.Abs(angel) > 90))
                                //{

                                //    if (((devP.X1 + devP.Y1) > (pspDev.X1 + pspDev.Y1)))
                                //    {
                                //        pStart = new PointF(center.X - (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(15 * Math.Cos((angel) * Math.PI / 180)));
                                //    }
                                //    else
                                //    {
                                //        pStart = new PointF(center.X + (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(15 * Math.Cos((angel) * Math.PI / 180)));

                                //    }
                                //}
                            }

                            PointF newp1 = new PointF(t[0].X + (t[1].X - t[0].X) / 2 - (float)(15 * Math.Sin(angel)), t[0].Y + (t[1].Y - t[0].Y) / 2 - (float)(15 * Math.Cos(angel)));

                            n1.SetAttribute("x", Convert.ToString(pStart.X));
                            n1.SetAttribute("y", Convert.ToString(pStart.Y));
                            //n3.SetAttribute("x", Convert.ToString(pStart2.X));
                            //n3.SetAttribute("y", Convert.ToString(pStart2.Y));
                            //double temp=(Convert.ToDouble(devDH[6])+Convert.ToDouble(devDH[10])*volt*volt/1000000)*capability;
                            //if (temp>=0)
                            //{
                            //    n3.InnerText = ((Convert.ToDouble(devDH[5]) + Convert.ToDouble(devDH[9]) * volt * volt/1000000)*capability).ToString() + " + "+"j"+temp.ToString();
                            //}
                            //else
                            //{
                            //    n3.InnerText = ((Convert.ToDouble(devDH[5]) + Convert.ToDouble(devDH[9]) * volt * volt/1000000)*capability).ToString() +" - "+"j"+ (Math.Abs(temp)).ToString();
                            //}

                            //n3.SetAttribute("layer", SvgDocument.currentLayer);
                            //n3.SetAttribute("flag", "1");
                            //n3.SetAttribute("stroke", "#0000FF");
                            //n1.SetAttribute("x", Convert.ToString(t[0].X + (t[1].X - t[0].X) / 2));
                            //n1.SetAttribute("y", Convert.ToString(t[0].Y + (t[1].Y - t[0].Y) / 2));
                            if (Convert.ToDouble(devDH[4]) >= 0)
                            {
                                n1.InnerText = (Math.Abs(Convert.ToDouble(devDH[3]) * capability)).ToString("N2") + " + j" + (Math.Abs(Convert.ToDouble(devDH[4]) * capability)).ToString("N2");
                            }
                            else
                            {
                                n1.InnerText = (Math.Abs(Convert.ToDouble(devDH[3]) * capability)).ToString("N2") + " - j" + (Math.Abs(Convert.ToDouble(devDH[4]) * capability)).ToString("N2");
                            }
                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                            n1.SetAttribute("flag", "1");
                            n1.SetAttribute("font-size", "6");
                            if (listParallel != null)
                            {
                                if (Convert.ToDouble(dev[3]) > ((PSPDEV)listParallel[0]).LineChange)//电流越限，需修改。






                                    n1.SetAttribute("stroke", "#FF0000");
                            }


                            PointF p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 25) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 25) * Math.PI / 180)));
                            PointF p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 335) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 335) * Math.PI / 180)));

                            if (Convert.ToDouble(devDH[3]) < 0)
                            {
                                p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 155) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 155) * Math.PI / 180)));
                                p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 205) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 205) * Math.PI / 180)));
                            }

                            string l1 = Convert.ToString(p1.X);
                            string l2 = Convert.ToString(p1.Y);
                            string l5 = Convert.ToString(p2.X);
                            string l6 = Convert.ToString(p2.Y);

                            //n2.SetAttribute("stroke", "#FF0000");
                            n2.SetAttribute("points", l1 + " " + l2 + "," + l3 + " " + l4 + "," + l5 + " " + l6);
                            n2.SetAttribute("fill-opacity", "1");
                            n2.SetAttribute("layer", SvgDocument.currentLayer);
                            n2.SetAttribute("flag", "1");
                            n2.SetAttribute("font-size", "6");
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);
                            tlVectorControl1.SVGDocument.CurrentElement = n2 as SvgElement;

                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            tlVectorControl1.Operation = ToolOperation.Select;

                            tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;

                            //if (Convert.ToDouble(dev[3]) <= 0)
                            RectangleF ttt = ((Polyline)element).GetBounds();

                            tlVectorControl1.RotateSelection(angel, pStart);
                            if (Math.Abs(angel) > 90)
                                tlVectorControl1.RotateSelection(180, pStart);
                            //tlVectorControl1.RotateSelection((float)(Math.Atan((t[1].Y - t[0].Y) / (t[1].X - t[0].X)) * 180 / Math.PI), pt4[0]);
                            //tlVectorControl1.RotateSelection(-10, (new PointF(center.X+10,center.Y+10)));
                            //tlVectorControl1.SVGDocument.RootElement.AppendChild(n3);
                            //tlVectorControl1.Operation = ToolOperation.Select;
                            //tlVectorControl1.SVGDocument.CurrentElement = n3 as SvgElement;
                            //tlVectorControl1.RotateSelection(360+angel, pStart2);
                            //if (Math.Abs(angel) > 90)
                            //    tlVectorControl1.RotateSelection(-180, pStart2);

                            PointF newp = new PointF(center.X + 10, center.Y + 10);


                            tlVectorControl1.Refresh();

                        }

                    }
                    strIH = ihLine.ReadLine();
                    strDH = dhLine.ReadLine();
                }

                ihLine.Close();
                dhLine.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("参数错误，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


        }
        private void Biubiu()
        {

        }
        public void SaveAllLayer()
        {

            //SaveAllItem();
            string txt = "";
            //Image

            /* XmlNodeList imglist = tlVectorControl1.SVGDocument.SelectNodes("svg/image");
             foreach (XmlNode node in imglist)
             {
                 string tran = ((XmlElement)node).GetAttribute("xlink:href");
                 if (tran.Substring(0, tran.Length - 4) != ((SvgElement)node).ID)
                 {
                     string[] sp = tran.Split(".".ToCharArray());
                     FileStream fs = new FileStream(tran, FileMode.Open, FileAccess.Read);
                     byte[] img = new byte[fs.Length];
                     fs.Read(img, 0, (int)fs.Length);
                     fs.Close();
                     SVG_IMAGE svg_img = new SVG_IMAGE();
                     svg_img.SUID = ((SvgElement)node).ID;
                     svg_img.svgID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                     svg_img.layerID = ((SvgElement)node).GetAttribute("layer");
                     svg_img.image = img;
                     svg_img.MDATE = System.DateTime.Now;
                     svg_img.col1 = sp[sp.Length - 1];
                     Services.BaseService.Create<SVG_IMAGE>(svg_img);
                     ((SvgElement)node).SetAttribute("xlink:href", ((SvgElement)node).ID + "." + sp[sp.Length - 1]);

                 }
                 else
                 {
                     SVG_IMAGE svg_img = new SVG_IMAGE();
                     svg_img.SUID = ((SvgElement)node).ID;
                     svg_img.layerID = ((SvgElement)node).GetAttribute("layer");
                     svg_img.XML = ((SvgElement)node).GetAttribute("transform");
                     Services.BaseService.Update("UpdateSVG_IMAGEXML", svg_img);

                 }
             }*/
            //string tems_id = "";
            //string[] stryearid = yearID.Split(",".ToCharArray());
            //if (stryearid.Length > 1)
            //{
            //    tems_id = stryearid[1].Replace("'", "");
            //    tems_id = tems_id.Replace("\"", "");
            //}
            //layer
            ArrayList list = tlVectorControl1.SVGDocument.getLayerList();
            for (int i = 0; i < list.Count; i++)
            {
                txt = "";
                Layer lar = list[i] as Layer;
                //bool IsSave = false;

                //for (int m = 0; m < SaveID.Count; m++)
                //{
                //    if (lar.GetAttribute("ParentID") == SaveID[m].ToString())
                //    {
                //        IsSave = true;
                //    }
                //}
                //for (int j = 0; j<frmlar.NoSave.Count; j++)
                //{
                //    if (lar.ID == ((Layer)frmlar.NoSave[j]).ID)
                //    {
                //        IsSave = false;
                //    }
                //}
                //for (int j = 0; j < ChangeLayerList.Count; j++)
                //{
                //if (lar.ID == ChangeLayerList[j].ToString())
                //if (IsSave) 
                //{
                XmlNodeList nn1 = tlVectorControl1.SVGDocument.SelectNodes("svg/* [@layer='" + lar.ID + "']");
                foreach (XmlNode node in nn1)
                {
                    txt = txt + node.OuterXml;
                }
                SVG_LAYER _svg = new SVG_LAYER();
                _svg.SUID = lar.ID;
                _svg.svgID = tlVectorControl1.SVGDocument.SvgdataUid;
                _svg = (SVG_LAYER)Services.BaseService.GetObject("SelectSVG_LAYERByKey", _svg);
                int ny = 0;
                try { ny = int.Parse(lar.Label.Substring(0, 4)); }
                catch { }
                if (_svg != null)
                {
                    _svg.XML = txt;
                    _svg.NAME = lar.Label;
                    _svg.MDATE = System.DateTime.Now;
                    _svg.OrderID = ny * 100 + list.IndexOf(lar);
                    // _svg.YearID = lar.GetAttribute("ParentID");
                    _svg.IsChange = lar.GetAttribute("IsChange");
                    _svg.visibility = lar.GetAttribute("visibility");
                    _svg.layerType = lar.GetAttribute("layerType");
                    _svg.IsSelect = lar.GetAttribute("IsSelect");
                    Services.BaseService.Update<SVG_LAYER>(_svg);
                }
                else
                {
                    _svg = new SVG_LAYER();
                    _svg.SUID = lar.ID;
                    _svg.NAME = lar.Label;
                    _svg.svgID = tlVectorControl1.SVGDocument.SvgdataUid;
                    _svg.XML = txt;
                    _svg.MDATE = System.DateTime.Now;
                    _svg.OrderID = ny * 100 + list.IndexOf(lar);
                    _svg.YearID = "";// lar.GetAttribute("ParentID");
                    _svg.IsChange = lar.GetAttribute("IsChange");
                    _svg.visibility = lar.GetAttribute("visibility");
                    _svg.layerType = lar.GetAttribute("layerType");
                    _svg.IsSelect = lar.GetAttribute("IsSelect");
                    Services.BaseService.Create<SVG_LAYER>(_svg);
                }
                //continue;
                //    }
                //}
            }
            //symbol
            XmlNodeList symlist = tlVectorControl1.SVGDocument.SelectNodes("svg/defs/symbol");
            foreach (XmlNode node in symlist)
            {
                string suid = ConfigurationSettings.AppSettings.Get("SvgID");
                SVG_SYMBOL _sym = new SVG_SYMBOL();
                _sym.EleID = ((XmlElement)node).GetAttribute("id");
                _sym.svgID = suid;
                _sym = (SVG_SYMBOL)Services.BaseService.GetObject("SelectSVG_SYMBOLByEleID", _sym);
                if (_sym != null)
                {
                    _sym.XML = node.OuterXml;
                    _sym.MDATE = System.DateTime.Now;
                    Services.BaseService.Update<SVG_SYMBOL>(_sym);
                }
                else
                {
                    _sym = new SVG_SYMBOL();
                    _sym.SUID = Guid.NewGuid().ToString();
                    _sym.EleID = ((XmlElement)node).GetAttribute("id");
                    _sym.NAME = ((XmlElement)node).GetAttribute("label");
                    _sym.svgID = suid;
                    _sym.XML = node.OuterXml;
                    _sym.MDATE = System.DateTime.Now;
                    Services.BaseService.Create<SVG_SYMBOL>(_sym);
                }
            }
        }
        public void Save()
        {
            //XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@flag='" + "1" + "']");
            //foreach (XmlNode node in list)
            //{
            //    SvgElement element = node as SvgElement;
            //    tlVectorControl1.SVGDocument.CurrentElement = element;
            //    tlVectorControl1.Delete();
            //}

            if (tlVectorControl1.SVGDocument.CurrentLayer.ID != string.Empty)
            {
                IList svglist = Services.BaseService.GetList("SelectSVGFILEByKey", tlVectorControl1.SVGDocument.CurrentLayer.ID);
                if (svglist.Count > 0)
                {
                    svg = (SVGFILE)svglist[0];
                    svg.SVGDATA = tlVectorControl1.SVGDocument.OuterXml;
                    svg.FILENAME = tlVectorControl1.SVGDocument.FileName;
                    Services.BaseService.Update<SVGFILE>(svg);
                    PSPDIR pspDir = new PSPDIR();
                    pspDir.FileGUID = svg.SUID;
                    pspDir.FileName = tlVectorControl1.SVGDocument.FileName;
                    if (fileType == true)
                    {
                        pspDir.FileType = "潮流";
                    }
                    else
                    {
                        pspDir.FileType = "短路";
                    }
                    Services.BaseService.Update<PSPDIR>(pspDir);
                }
                else
                {
                    svg.SUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    svg.FILENAME = tlVectorControl1.SVGDocument.FileName;
                    svg.SVGDATA = tlVectorControl1.SVGDocument.OuterXml;
                    Services.BaseService.Create<SVGFILE>(svg);
                    PSPDIR pspDir = new PSPDIR();
                    pspDir.FileGUID = svg.SUID;
                    pspDir.FileName = svg.FILENAME;
                    if (fileType == true)
                    {
                        pspDir.FileType = "潮流";
                    }
                    else
                    {
                        pspDir.FileType = "短路";
                    }
                    pspDir.CreateTime = System.DateTime.Now.ToString();
                    Services.BaseService.Create<PSPDIR>(pspDir);
                }
            }
            else
            {
                svg.SUID = Guid.NewGuid().ToString();
                svg.FILENAME = tlVectorControl1.SVGDocument.FileName;
                svg.SVGDATA = tlVectorControl1.SVGDocument.OuterXml;
                Services.BaseService.Create<SVGFILE>(svg);
                tlVectorControl1.SVGDocument.CurrentLayer.ID = svg.SUID;
                PSPDIR pspDir = new PSPDIR();
                pspDir.FileGUID = svg.SUID;
                pspDir.FileName = svg.FILENAME;
                if (fileType == true)
                {
                    pspDir.FileType = "潮流";
                }
                else
                {
                    pspDir.FileType = "短路";
                }
                pspDir.CreateTime = System.DateTime.Now.ToString();
                Services.BaseService.Create<PSPDIR>(pspDir);
            }
            tlVectorControl1.IsModified = false;
        }
        public void Open2(string _SvgUID, string yearID)
        {
            try
            {
                string uid = ConfigurationSettings.AppSettings.Get("SvgID");

                StringBuilder txt = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><svg id=\"svg\" width=\"1500\" height=\"1000\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:itop=\"http://www.Itop.com/itop\" transform=\"matrix(1 0 0 1 0 1)\"><defs>");
                string svgdefs = "";
                string layertxt = "";
                StringBuilder content = new StringBuilder();

                if (string.IsNullOrEmpty(_SvgUID)) return;
                SVG_LAYER lar = new SVG_LAYER();
                lar.svgID = uid;
                lar.YearID = "'" + yearID + "'";
                IList<SVG_LAYER> larlist = Services.BaseService.GetList<SVG_LAYER>("SelectSVG_LAYERByYearID", lar);
                foreach (SVG_LAYER _lar in larlist)
                {
                    //layertxt = layertxt + "<layer id=\"" + _lar.SUID + "\" label=\"" + _lar.NAME + "\" layerType=\"" + _lar.layerType + "\" visibility=\"" + _lar.visibility + "\" ParentID=\"" + _lar.YearID + "\" IsSelect=\"" + _lar.IsSelect + "\" />";
                    content.Append(_lar.XML);
                }
                txt.Append("<layer id=\"layer6666\" label=\"默认层\" />");
                txt.Append(layertxt);


                SVG_SYMBOL sym = new SVG_SYMBOL();
                sym.svgID = uid;
                IList<SVG_SYMBOL> symlist = Services.BaseService.GetList<SVG_SYMBOL>("SelectSVG_SYMBOLBySvgID", sym);
                foreach (SVG_SYMBOL _sym in symlist)
                {
                    svgdefs = svgdefs + _sym.XML;
                }
                txt.Append(svgdefs + "</defs>");
                txt.Append(content.ToString() + "</svg>");

                SvgDocument document = new SvgDocument();
                document.LoadXml(txt.ToString());
                //document.FileName = SvgName;
                document.SvgdataUid = _SvgUID;
                SVGUID = _SvgUID;

                this.Text = document.FileName;
                if (document.RootElement == null)
                {
                    tlVectorControl1.NewFile();
                    Layer.CreateNew("背景层", tlVectorControl1.SVGDocument);
                    Layer.CreateNew("城市规划层", tlVectorControl1.SVGDocument);
                    Layer.CreateNew("供电区域层", tlVectorControl1.SVGDocument);
                }
                else
                {
                    tlVectorControl1.SVGDocument = document;
                }
                tlVectorControl1.SVGDocument.CurrentLayer.ID = SVGUID;
                tlVectorControl1.SVGDocument.FileName = this.Text;
                tlVectorControl1.DocumentbgColor = Color.White;
                tlVectorControl1.BackColor = Color.White;

                foreach (XmlElement ele in document.RootElement.ChildNodes)
                {
                    ele.SetAttribute("layer", "layer6666");
                    if (((SvgElement)ele).LocalName == "polyline")
                    {
                        ele.SetAttribute("flag", "1");
                    }
                }

                SvgDocument.currentLayer = "layer6666";

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //tlVectorControl1.SVGDocument.CurrentLayer.ID = "";
        }
        public void Open()
        {
            string id = "''";
            /*   frmLayerGrade fgrade = new frmLayerGrade();
               fgrade.SymbolDoc = tlVectorControl1.SVGDocument;
               fgrade.InitData(_SvgUID);

               if (fgrade.ShowDialog() == DialogResult.OK)
               {
                   id = fgrade.GetSelectNode();
                   yearID = id; 
               }
               else
               {
                   id = fgrade.GetSelectNode();
                   yearID = id;
               }*/
            string _SvgUID = "";
            string svgkey = ConfigurationSettings.AppSettings.Get("SvgID").ToString();
            bool create = false;
            frmGProg ff = new frmGProg();

            if (ff.ShowDialog() == DialogResult.OK)
            {
                _SvgUID = ff.Key;
                string strName = ff.Name1;
                StringBuilder txt = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><svg id=\"svg\" width=\"1500\" height=\"1000\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:itop=\"http://www.Itop.com/itop\" transform=\"matrix(1 0 0 1 0 1)\"><defs>");
                string svgdefs = "";
                string layertxt = "";
                StringBuilder content = new StringBuilder();

                //if (string.IsNullOrEmpty(_SvgUID)) return;
                try
                {
                    SVGFILE svgFile = new SVGFILE();
                    svgFile.SUID = _SvgUID;
                    svgFile = (SVGFILE)Services.BaseService.GetObject("SelectSVGFILEByKey", svgFile);
                    if (svgFile == null)
                    {
                        create = true;
                        svgFile = new SVGFILE();
                        svgFile.SUID = _SvgUID;
                        svgFile.FILENAME = strName;
                        Services.BaseService.Create<SVGFILE>(svgFile);
                    }
                    //SvgDocument document = CashSvgDocument;
                    //if (document == null) {
                    SVG_LAYER lar = new SVG_LAYER();
                    lar.svgID = _SvgUID;
                    lar.YearID = id;
                    IList<SVG_LAYER> larlist = Services.BaseService.GetList<SVG_LAYER>("SelectSVG_LAYERByYearID", lar);
                    foreach (SVG_LAYER _lar in larlist)
                    {
                        layertxt = layertxt + "<layer id=\"" + _lar.SUID + "\" label=\"" + _lar.NAME + "\" layerType=\"" + _lar.layerType + "\" visibility=\"" + _lar.visibility + "\" ParentID=\"" + _lar.YearID + "\" IsSelect=\"" + _lar.IsSelect + "\" />";
                        content.Append(_lar.XML);
                    }

                    if (larlist.Count == 0)
                    {
                        layertxt = layertxt + "<layer id=\"" + svgFile.SUID + "1" + "\" label=\"近期网架规划层\" layerType=\"电网规划层\" visibility=\"visible\" ParentID=\"\" IsSelect=\"true\" />\r\n";
                        layertxt = layertxt + "<layer id=\"" + svgFile.SUID + "2" + "\" label=\"中期网架规划层\" layerType=\"电网规划层\" visibility=\"visible\" ParentID=\"\" IsSelect=\"true\" />\r\n";
                        layertxt = layertxt + "<layer id=\"" + svgFile.SUID + "3" + "\" label=\"远期网架规划层\" layerType=\"电网规划层\" visibility=\"visible\" ParentID=\"\" IsSelect=\"true\" />\r\n";
                        layertxt = layertxt + "<layer id=\"" + svgFile.SUID + "4" + "\" label=\"整体规划层\" layerType=\"电网规划层\" visibility=\"visible\" ParentID=\"\" IsSelect=\"true\" />\r\n";
                        layertxt = layertxt + "<layer id=\"" + svgFile.SUID + "5" + "\" label=\"背景参考层\" layerType=\"电网规划层\" visibility=\"visible\" ParentID=\"\" IsSelect=\"true\" />\r\n";
                        content.Append("<text x=\"1\" y=\"1\" layer=\"" + svgFile.SUID + "1" + "\" id=\"text4611ec7b9f37\" font-size=\"1\" >1</text>");
                    }
                    txt.Append(layertxt);


                    SVG_SYMBOL sym = new SVG_SYMBOL();
                    sym.svgID = svgkey;
                    IList<SVG_SYMBOL> symlist = Services.BaseService.GetList<SVG_SYMBOL>("SelectSVG_SYMBOLBySvgID", sym);
                    foreach (SVG_SYMBOL _sym in symlist)
                    {
                        svgdefs = svgdefs + _sym.XML;
                    }

                    txt.Append(svgdefs + "</defs>");
                    txt.Append(content.ToString() + "</svg>");


                    SvgDocument document = SvgDocumentFactory.CreateDocument();
                    //if (txt.ToString() != "1")
                    //{
                    string filename = Path.GetTempFileName();
                    if (File.Exists("tmp080321.temp"))
                    {
                        filename = "tmp080321.temp";
                    }
                    else
                    {
                        StreamWriter sw = new StreamWriter(filename);
                        sw.Write(txt.ToString());
                        sw.Close();
                    }
                    tlVectorControl1.OpenFile(filename);
                    document = tlVectorControl1.SVGDocument;
                    //    int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));

                    //}
                    //else
                    //{
                    //    //NullFile(_SvgUID);
                    //    return;
                    //}
                    document.FileName = svgFile.FILENAME;
                    document.SvgdataUid = svgFile.SUID;

                    this.Text = document.FileName;
                    //if(create){
                    //    Layer lay = Layer.CreateNew("规划层", document);
                    //   lay.SetAttribute("layerType", "电网规划层");
                    //}

                    tlVectorControl1.SVGDocument = document;

                    tlVectorControl1.SVGDocument.FileName = this.Text;
                    AddCombolScale();
                    ClearDEV();
                }

                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }

        }
        public void ClearDEV()
        {
            PSPDEV p1 = new PSPDEV();
            p1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "1";
            IList n1 = Services.BaseService.GetList("SelectPSPDEVBySvgUID", p1);
            XmlNodeList x1 = tlVectorControl1.SVGDocument.SelectNodes("svg/* [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "1']");
            for (int i = 0; i < n1.Count; i++)
            {
                bool ck = false;
                PSPDEV pp = (PSPDEV)n1[i];
                for (int j = 0; j < x1.Count; j++)
                {
                    SvgElement s = x1[j] as SvgElement;
                    if (pp.EleID == s.ID)
                    {
                        ck = true;
                        break;
                    }
                }
                if (!ck)
                {
                    Services.BaseService.Delete<PSPDEV>(pp);
                }
            }
            PSPDEV p2 = new PSPDEV();
            p2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "2";
            IList n2 = Services.BaseService.GetList("SelectPSPDEVBySvgUID", p2);
            XmlNodeList x2 = tlVectorControl1.SVGDocument.SelectNodes("svg/* [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "2']");
            for (int i = 0; i < n2.Count; i++)
            {
                bool ck = false;
                PSPDEV pp = (PSPDEV)n2[i];
                for (int j = 0; j < x2.Count; j++)
                {
                    SvgElement s = x2[j] as SvgElement;
                    if (pp.EleID == s.ID)
                    {
                        ck = true;
                        break;
                    }
                }
                if (!ck)
                {
                    Services.BaseService.Delete<PSPDEV>(pp);
                }
            }
            PSPDEV p3 = new PSPDEV();
            p3.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "3";
            IList n3 = Services.BaseService.GetList("SelectPSPDEVBySvgUID", p3);
            XmlNodeList x3 = tlVectorControl1.SVGDocument.SelectNodes("svg/* [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "3']");
            for (int i = 0; i < n3.Count; i++)
            {
                bool ck = false;
                PSPDEV pp = (PSPDEV)n3[i];
                for (int j = 0; j < x3.Count; j++)
                {
                    SvgElement s = x3[j] as SvgElement;
                    if (pp.EleID == s.ID)
                    {
                        ck = true;
                        break;
                    }
                }
                if (!ck)
                {
                    Services.BaseService.Delete<PSPDEV>(pp);
                }
            }
            PSPDEV p4 = new PSPDEV();
            p4.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
            IList n4 = Services.BaseService.GetList("SelectPSPDEVBySvgUID", p4);
            XmlNodeList x4 = tlVectorControl1.SVGDocument.SelectNodes("svg/* [@layer='" + tlVectorControl1.SVGDocument.SvgdataUid + "4']");
            for (int i = 0; i < n4.Count; i++)
            {
                bool ck = false;
                PSPDEV pp = (PSPDEV)n4[i];
                for (int j = 0; j < x4.Count; j++)
                {
                    SvgElement s = x4[j] as SvgElement;
                    if (pp.EleID == s.ID)
                    {
                        ck = true;
                        break;
                    }
                }
                if (!ck)
                {
                    Services.BaseService.Delete<PSPDEV>(pp);
                }
            }
        }
        public int SwitchStatus(string str)
        {
            if (str == "开")
            {
                return 1;
            }
            else if (str == "合")
            {
                return 0;
            }
            else
            {
                return 0;
            }
        }
        private bool CheckN()                            //生成N-1检验的数据
        {
            string outParam1 = null;
            string outParam2 = null;
            double yinzi = 0;
            double volt = 0;
            double current = 0;
            double sandvolt = 0;
            // PSPDEV pow = new PSPDEV();
            //pow.Type = "Power";
            // pow.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            // pow = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", pow);

            //     yinzi = pow.PowerFactor;
            //     volt = pow.StandardVolt;
            //     current = pow.StandardCurrent;

            Topology();
            PSPDEV pspDev = new PSPDEV();
            pspDev.Type = "power";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            if (list3 == null)
            {
                MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            foreach (PSPDEV dev in list3)
            {
                yinzi = Convert.ToDouble(dev.PowerFactor);
                current = Convert.ToDouble(dev.StandardCurrent);
                volt = Convert.ToDouble(dev.StandardVolt);
                if (dev.PowerFactor == 0)
                {
                    yinzi = 1;
                }
                if (dev.StandardCurrent == 0)
                {
                    current = 1;
                }
                if (dev.StandardVolt == 0)
                {
                    volt = 1;
                }
                sandvolt = volt;
            };
            current = 100;
            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "TransformLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list4 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);

            int num_Gen = 0;
            int num_Load = 0;
            foreach (PSPDEV dev in list1)
            {
                if (dev.Lable == "电厂")
                {
                    num_Gen++;
                }
                else if (dev.Lable == "变电站")
                {
                    num_Load++;
                }
            }
            //outParam1 += (list2.Count+list4.Count).ToString() + " " + num_Gen.ToString() + " " + num_Load.ToString() + " " + "0.00001" + " " + "100" + " " + "1" + " " + "0" + "\r\n";

            foreach (PSPDEV dev in list2)
            {
                if (dev.FirstNode < 0 || dev.LastNode < 0)
                {
                    string temp = "拓朴分析失败,";
                    temp += dev.Name;
                    temp += "没有正确连接,请进行处理！。";
                    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = sandvolt;
                if (outParam1 != null && dev.LineStatus == "运行")
                {
                    outParam1 += "\r\n";
                }
                if (dev.LineStatus == "运行")
                    outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + (dev.LineGNDC * volt * volt / (current * 1000000)).ToString() + " " + "0" + " " + dev.G.ToString());
            }
            foreach (PSPDEV dev in list4)
            {
                if (dev.FirstNode < 0 || dev.LastNode < 0)
                {
                    string temp = "拓朴分析失败,";
                    temp += dev.Name;
                    temp += "没有正确连接,请进行处理！。";
                    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (outParam1 != null)
                {
                    outParam1 += "\r\n";
                }
                outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + (dev.LineGNDC * volt * volt / (current * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString());
            }
            foreach (PSPDEV dev in list1)
            {
                if (dev.Number < 0)
                {

                    MessageBox.Show("拓朴分析失败，请进行处理!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //if (dev.InPutQ == 0)
                //    dev.InPutQ = dev.InPutP * Math.Tan(Math.Acos(yinzi));
                //if (dev.OutQ == 0)
                //    dev.OutQ = dev.OutP * Math.Tan(Math.Acos(yinzi));
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = sandvolt;
                if (dev.NodeType == "0")
                {
                    dev.OutP = 0;
                    dev.OutQ = 0;
                    dev.InPutP = 0;
                    dev.InPutQ = 0;
                }
                if (dev.Lable == "电厂")
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "2";
                    }
                }
                else if (dev.Lable == "变电站")
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "1";
                    }
                }
                if (dev.NodeType == "0")
                {
                    dev.NodeType = "3";
                }
                if (outParam2 != null)
                {
                    outParam2 += "\r\n";
                }
                outParam2 += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / volt).ToString() + " " + dev.VoltV.ToString() + " " + ((dev.InPutP - dev.OutP) / current).ToString() + " " + ((dev.InPutQ - dev.OutQ) / current).ToString());
            }

            //outParam1 += (volt + " " + current + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + ";" + "\r\n");
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\branch.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\branch.txt");
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
            }
            //if (File.Exists("c:\\L9.txt"))
            //{
            //    File.Delete("c:\\L9.txt");
            //}
            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\branch.txt"), FileMode.OpenOrCreate);
            StreamWriter str1 = new StreamWriter(VK);
            str1.Write(outParam1);
            str1.Close();
            FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
            StreamWriter str2 = new StreamWriter(L);
            str2.Write(outParam2);
            str2.Close();
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\VandTheta.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\VandTheta.txt");
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\lineP.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\lineP.txt");
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\transP.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\transP.txt");
            }
            return true;
            return true;
        }
        //进行第一次负荷计算

        private bool Checkfuhe()
        {
            string outParam1 = null;
            string outParam2 = null;
            double yinzi = 0;
            double volt = 0;
            double current = 0;
            double standvolt = 0;
            // PSPDEV pow = new PSPDEV();
            //pow.Type = "Power";
            // pow.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            // pow = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", pow);

            //     yinzi = pow.PowerFactor;
            //     volt = pow.StandardVolt;
            //     current = pow.StandardCurrent;

            Topologyfuhe();
            PSPDEV pspDev = new PSPDEV();
            pspDev.Type = "power";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            if (list3 == null)
            {
                MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            foreach (PSPDEV dev in list3)
            {
                yinzi = Convert.ToDouble(dev.PowerFactor);
                current = Convert.ToDouble(dev.StandardCurrent);
                volt = Convert.ToDouble(dev.StandardVolt);
                standvolt = volt;
                if (dev.PowerFactor == 0)
                {
                    yinzi = 1;
                }
                if (dev.StandardCurrent == 0)
                {
                    current = 1;
                }
                if (dev.StandardVolt == 0)
                {
                    volt = 1;
                    standvolt = 1;
                }
            }
            current = 100;      //额定电容都设为100
            //首先去掉之前的虚拟线路


            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            pspDev.KName = "虚拟线路";
            IList list0 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndKName", pspDev);
            for (int i = 0; i < list0.Count; i++)
            {
                pspDev = (PSPDEV)list0[i];
                Services.BaseService.Delete<PSPDEV>(pspDev);
            }
            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "TransformLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list4 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "GNDLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list5 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);

            int num_Gen = 0;
            int num_Load = 0;
            foreach (PSPDEV dev in list1)
            {
                if (dev.Lable == "电厂")
                {
                    num_Gen++;
                }
                else if (dev.Lable == "变电站")
                {
                    num_Load++;
                }
            }
            //outParam1 += (list2.Count+list4.Count).ToString() + " " + num_Gen.ToString() + " " + num_Load.ToString() + " " + "0.00001" + " " + "100" + " " + "1" + " " + "0" + "\r\n";
            //如果母线节点独立出现这样的提醒


            List<string> busname = new List<string>();
            foreach (PSPDEV dev in list1)
            {
                bool flag = false;
                foreach (PSPDEV devline in list2)
                {
                    if ((dev.Number == devline.LastNode || dev.Number == devline.FirstNode) && (devline.LineStatus == "运行" || devline.LineStatus == "待选"))
                    {
                        flag = true;
                        break;
                    }
                }
                foreach (PSPDEV devtrans in list4)
                {
                    if (dev.Number == devtrans.LastNode || dev.Number == devtrans.FirstNode)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    busname.Add(dev.Name);
                }
            }
            //在此处加入虚拟线路 其中它的电阻为999
            for (int i = 0; i < busname.Count; i++)
            {
                PSPDEV psp = new PSPDEV();
                psp.Name = busname[i];
                psp.Type = "Use";
                psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", psp);
                if (psp.Number != 1)
                {

                    PSPDEV pspline = new PSPDEV();
                    pspline.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspline.FirstNode = psp.Number;
                    pspline.LastNode = psp.Number - 1;
                    pspline.Type = "Polyline";
                    pspline.Lable = "支路";
                    pspline.Name = "虚拟线路" + i;
                    pspline.KName = "虚拟线路";
                    pspline.LineStatus = "运行";
                    pspline.Number = brchcount + i + 1;
                    pspline.SUID = Guid.NewGuid().ToString();
                    pspline.LineLength = 100;
                    pspline.VoltR = psp.ReferenceVolt;
                    pspline.ReferenceVolt = psp.ReferenceVolt;
                    pspline.LineR = 9999;
                    pspline.LineTQ = 9999;
                    Services.BaseService.Create<PSPDEV>(pspline);
                }
                else
                {

                    PSPDEV pspline = new PSPDEV();
                    pspline.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspline.FirstNode = psp.Number;
                    pspline.LastNode = psp.Number + 1;
                    pspline.Type = "Polyline";
                    pspline.Lable = "支路";
                    pspline.Name = "虚拟线路" + i;
                    pspline.KName = "虚拟线路";
                    pspline.LineStatus = "运行";
                    pspline.Number = brchcount + i + 1;
                    pspline.SUID = Guid.NewGuid().ToString();
                    pspline.LineLength = 100;
                    pspline.VoltR = psp.ReferenceVolt;
                    pspline.ReferenceVolt = psp.ReferenceVolt;
                    pspline.LineR = 9999;
                    pspline.LineTQ = 9999;
                    Services.BaseService.Create<PSPDEV>(pspline);
                }
            }
            //if (busname.Count > 0)
            //{
            //    string temp = "拓扑分析失败";
            //    for (int i = 0; i < busname.Count; i++)
            //    {
            //        temp += "，" + busname[i];

            //    }
            //    temp += "为孤立的节点！";
            //    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            foreach (PSPDEV dev in list2)
            {
                if (dev.FirstNode < 0 || dev.LastNode < 0)
                {
                    string temp = "拓朴分析失败,";
                    temp += dev.Name;
                    temp += "没有正确连接,请进行处理！。";
                    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                if (outParam1 != null && (dev.LineStatus == "运行" || dev.LineStatus == "待选"))
                {
                    outParam1 += "\r\n";
                }
                if (dev.LineStatus == "运行" || dev.LineStatus == "待选")
                    outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + (dev.LineGNDC * volt * volt / (current * 1000000)).ToString() + " " + "0" + " " + dev.G.ToString());
            }
            foreach (PSPDEV dev in list4)
            {
                if (dev.FirstNode < 0 || dev.LastNode < 0)
                {
                    string temp = "拓朴分析失败,";
                    temp += dev.Name;
                    temp += "没有正确连接,请进行处理！。";
                    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (outParam1 != null)
                {
                    outParam1 += "\r\n";
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + (dev.LineGNDC * volt * volt / (current * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString());
            }
            foreach (PSPDEV dev in list5)
            {
                if (outParam1 != null)
                {
                    outParam1 += "\r\n";
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "3" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + (dev.LineGNDC * volt * volt / (current * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString());
            }
            foreach (PSPDEV dev in list1)
            {
                if (dev.Number < 0)
                {

                    MessageBox.Show("拓朴分析失败，请进行处理!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //if (dev.InPutQ == 0)
                //    dev.InPutQ = dev.InPutP * Math.Tan(Math.Acos(yinzi));
                //if (dev.OutQ == 0)
                //    dev.OutQ = dev.OutP * Math.Tan(Math.Acos(yinzi));
                if (dev.NodeType == "0")
                {
                    //dev.OutP = 0;
                    //dev.OutQ = 0;
                    //dev.InPutP = 0;
                    //dev.InPutQ = 0;
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                if (dev.Lable == "电厂")
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "2";
                    }
                }
                else if (dev.Lable == "变电站")
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "1";
                    }
                }
                if (dev.NodeType == "0")
                {
                    dev.NodeType = "3";
                }
                if (outParam2 != null)
                {
                    outParam2 += "\r\n";
                }
                outParam2 += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / volt).ToString() + " " + dev.VoltV.ToString() + " " + ((dev.InPutP - dev.OutP) / current).ToString() + " " + ((dev.InPutQ - dev.OutQ) / current).ToString());
            }

            //outParam1 += (volt + " " + current + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + ";" + "\r\n");
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\branch.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\branch.txt");
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
            }
            //if (File.Exists("c:\\L9.txt"))
            //{
            //    File.Delete("c:\\L9.txt");
            //}
            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\branch.txt"), FileMode.OpenOrCreate);
            StreamWriter str1 = new StreamWriter(VK);
            str1.Write(outParam1);
            str1.Close();
            FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
            StreamWriter str2 = new StreamWriter(L);
            str2.Write(outParam2);
            str2.Close();
            return true;
        }
        //根据加入不同的线路形成不同的线路和节点集合


        private bool Checkadd(string elid)
        {
            string outParam1 = null;
            string outParam2 = null;
            double yinzi = 0;
            double volt = 0;
            double current = 0;
            double standvolt = 0;
            // PSPDEV pow = new PSPDEV();
            //pow.Type = "Power";
            // pow.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            // pow = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", pow);

            //     yinzi = pow.PowerFactor;
            //     volt = pow.StandardVolt;
            //     current = pow.StandardCurrent;

            Topologyadd(elid);
            PSPDEV pspDev = new PSPDEV();
            pspDev.Type = "power";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            if (list3 == null)
            {
                MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            foreach (PSPDEV dev in list3)
            {
                yinzi = Convert.ToDouble(dev.PowerFactor);
                current = Convert.ToDouble(dev.StandardCurrent);
                volt = Convert.ToDouble(dev.StandardVolt);
                standvolt = volt;
                if (dev.PowerFactor == 0)
                {
                    yinzi = 1;
                }
                if (dev.StandardCurrent == 0)
                {
                    current = 1;
                }
                if (dev.StandardVolt == 0)
                {
                    volt = 1;
                    standvolt = 1;
                }
            }
            current = 100;      //额定电容都设为100
            //首先去掉之前的虚拟线路


            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            pspDev.KName = "虚拟线路";
            IList list0 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndKName", pspDev);
            for (int i = 0; i < list0.Count; i++)
            {
                pspDev = (PSPDEV)list0[i];
                Services.BaseService.Delete<PSPDEV>(pspDev);
            }
            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "TransformLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list4 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "GNDLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list5 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);

            int num_Gen = 0;
            int num_Load = 0;
            foreach (PSPDEV dev in list1)
            {
                if (dev.Lable == "电厂")
                {
                    num_Gen++;
                }
                else if (dev.Lable == "变电站")
                {
                    num_Load++;
                }
            }
            //outParam1 += (list2.Count+list4.Count).ToString() + " " + num_Gen.ToString() + " " + num_Load.ToString() + " " + "0.00001" + " " + "100" + " " + "1" + " " + "0" + "\r\n";
            //如果母线节点独立出现这样的提醒


            List<string> busname = new List<string>();
            foreach (PSPDEV dev in list1)
            {
                bool flag = false;
                foreach (PSPDEV devline in list2)
                {
                    if ((dev.Number == devline.LastNode || dev.Number == devline.FirstNode) && (devline.LineStatus == "运行" || devline.LineStatus == "待选" || devline.EleID == elid))
                    {
                        flag = true;
                        break;
                    }
                }
                foreach (PSPDEV devtrans in list4)
                {
                    if (dev.Number == devtrans.LastNode || dev.Number == devtrans.FirstNode)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    busname.Add(dev.Name);
                }
            }
            //在此处加入虚拟线路 其中它的电阻为999
            for (int i = 0; i < busname.Count; i++)
            {
                PSPDEV psp = new PSPDEV();
                psp.Name = busname[i];
                psp.Type = "Use";
                psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", psp);
                if (psp.Number != 1)
                {

                    PSPDEV pspline = new PSPDEV();
                    pspline.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspline.FirstNode = psp.Number;
                    pspline.LastNode = psp.Number - 1;
                    pspline.Type = "Polyline";
                    pspline.Lable = "支路";
                    pspline.Name = "虚拟线路" + i;
                    pspline.KName = "虚拟线路";
                    pspline.LineStatus = "运行";
                    pspline.Number = brchcount + i + 1;
                    pspline.SUID = Guid.NewGuid().ToString();
                    pspline.LineLength = 100;
                    pspline.VoltR = psp.ReferenceVolt;
                    pspline.ReferenceVolt = psp.ReferenceVolt;
                    pspline.LineR = 999;
                    pspline.LineTQ = 9999;
                    Services.BaseService.Create<PSPDEV>(pspline);
                }
                else
                {

                    PSPDEV pspline = new PSPDEV();
                    pspline.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspline.FirstNode = psp.Number;
                    pspline.LastNode = psp.Number + 1;
                    pspline.Type = "Polyline";
                    pspline.Lable = "支路";
                    pspline.Name = "虚拟线路" + i;
                    pspline.KName = "虚拟线路";
                    pspline.LineStatus = "运行";
                    pspline.Number = brchcount + i + 1;
                    pspline.SUID = Guid.NewGuid().ToString();
                    pspline.LineLength = 100;
                    pspline.VoltR = psp.ReferenceVolt;
                    pspline.ReferenceVolt = psp.ReferenceVolt;
                    pspline.LineR = 999;
                    pspline.LineTQ = 9999;
                    Services.BaseService.Create<PSPDEV>(pspline);
                }
            }
            //if (busname.Count > 0)
            //{
            //    string temp = "拓扑分析失败";
            //    for (int i = 0; i < busname.Count; i++)
            //    {
            //        temp += "，" + busname[i];

            //    }
            //    temp += "为孤立的节点！";
            //    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            foreach (PSPDEV dev in list2)
            {
                if (dev.FirstNode < 0 || dev.LastNode < 0)
                {
                    string temp = "拓朴分析失败,";
                    temp += dev.Name;
                    temp += "没有正确连接,请进行处理！。";
                    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                if (outParam1 != null && (dev.LineStatus == "运行" || dev.LineStatus == "待选" || dev.EleID == elid))
                {
                    outParam1 += "\r\n";
                }
                if (dev.LineStatus == "运行" || dev.LineStatus == "待选" || dev.EleID == elid)
                    outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + (dev.LineGNDC * volt * volt / (current * 1000000)).ToString() + " " + "0" + " " + dev.G.ToString());
            }
            foreach (PSPDEV dev in list4)
            {
                if (dev.FirstNode < 0 || dev.LastNode < 0)
                {
                    string temp = "拓朴分析失败,";
                    temp += dev.Name;
                    temp += "没有正确连接,请进行处理！。";
                    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (outParam1 != null)
                {
                    outParam1 += "\r\n";
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + (dev.LineGNDC * volt * volt / (current * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString());
            }
            foreach (PSPDEV dev in list5)
            {
                if (outParam1 != null)
                {
                    outParam1 += "\r\n";
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "3" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + (dev.LineGNDC * volt * volt / (current * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString());
            }
            foreach (PSPDEV dev in list1)
            {
                if (dev.Number < 0)
                {

                    MessageBox.Show("拓朴分析失败，请进行处理!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //if (dev.InPutQ == 0)
                //    dev.InPutQ = dev.InPutP * Math.Tan(Math.Acos(yinzi));
                //if (dev.OutQ == 0)
                //    dev.OutQ = dev.OutP * Math.Tan(Math.Acos(yinzi));
                if (dev.NodeType == "0")
                {
                    //dev.OutP = 0;
                    //dev.OutQ = 0;
                    //dev.InPutP = 0;
                    //dev.InPutQ = 0;
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                if (dev.Lable == "电厂")
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "2";
                    }
                }
                else if (dev.Lable == "变电站")
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "1";
                    }
                }
                if (dev.NodeType == "0")
                {
                    dev.NodeType = "3";
                }
                if (outParam2 != null)
                {
                    outParam2 += "\r\n";
                }
                outParam2 += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / volt).ToString() + " " + dev.VoltV.ToString() + " " + ((dev.InPutP - dev.OutP) / current).ToString() + " " + ((dev.InPutQ - dev.OutQ) / current).ToString());
            }

            //outParam1 += (volt + " " + current + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + ";" + "\r\n");
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\branch.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\branch.txt");
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
            }
            //if (File.Exists("c:\\L9.txt"))
            //{
            //    File.Delete("c:\\L9.txt");
            //}
            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\branch.txt"), FileMode.OpenOrCreate);
            StreamWriter str1 = new StreamWriter(VK);
            str1.Write(outParam1);
            str1.Close();
            FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
            StreamWriter str2 = new StreamWriter(L);
            str2.Write(outParam2);
            str2.Close();
            return true;
        }
        private bool Check()
        {
            string outParam1 = null;
            string outParam2 = null;
            double yinzi = 0;
            double volt = 0;
            double current = 0;
            double standvolt = 0;
            // PSPDEV pow = new PSPDEV();
            //pow.Type = "Power";
            // pow.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            // pow = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", pow);

            //     yinzi = pow.PowerFactor;
            //     volt = pow.StandardVolt;
            //     current = pow.StandardCurrent;

            Topology();
            PSPDEV pspDev = new PSPDEV();
            pspDev.Type = "power";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            if (list3 == null)
            {
                MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            foreach (PSPDEV dev in list3)
            {
                yinzi = Convert.ToDouble(dev.PowerFactor);
                current = Convert.ToDouble(dev.StandardCurrent);
                volt = Convert.ToDouble(dev.StandardVolt);
                standvolt = volt;
                if (dev.PowerFactor == 0)
                {
                    yinzi = 1;
                }
                if (dev.StandardCurrent == 0)
                {
                    current = 1;
                }
                if (dev.StandardVolt == 0)
                {
                    volt = 1;
                    standvolt = 1;
                }
            }
            current = 100;      //额定电容都设为100
            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "TransformLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list4 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "GNDLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list5 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);

            int num_Gen = 0;
            int num_Load = 0;
            foreach (PSPDEV dev in list1)
            {
                if (dev.Lable == "电厂")
                {
                    num_Gen++;
                }
                else if (dev.Lable == "变电站")
                {
                    num_Load++;
                }
            }
            //outParam1 += (list2.Count+list4.Count).ToString() + " " + num_Gen.ToString() + " " + num_Load.ToString() + " " + "0.00001" + " " + "100" + " " + "1" + " " + "0" + "\r\n";
            //如果母线节点独立出现这样的提醒


            List<string> busname = new List<string>();
            foreach (PSPDEV dev in list1)
            {
                bool flag = false;
                foreach (PSPDEV devline in list2)
                {
                    if (dev.Number == devline.LastNode || dev.Number == devline.FirstNode)
                    {
                        flag = true;
                        break;
                    }
                }
                foreach (PSPDEV devtrans in list4)
                {
                    if (dev.Number == devtrans.LastNode || dev.Number == devtrans.FirstNode)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    busname.Add(dev.Name);
                }
            }
            if (busname.Count > 0)
            {
                string temp = "拓扑分析失败";
                for (int i = 0; i < busname.Count; i++)
                {
                    temp += "，" + busname[i];

                }
                temp += "为孤立的节点！";
                MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            foreach (PSPDEV dev in list2)
            {
                if (dev.FirstNode < 0 || dev.LastNode < 0)
                {
                    string temp = "拓朴分析失败,";
                    temp += dev.Name;
                    temp += "没有正确连接,请进行处理！。";
                    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                if (outParam1 != null && (dev.LineStatus == "运行" || dev.LineStatus == "待选"))
                {
                    outParam1 += "\r\n";
                }
                if (dev.LineStatus == "运行" || dev.LineStatus == "待选")
                    outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "1" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + (dev.LineGNDC * volt * volt / (current * 1000000)).ToString() + " " + "0" + " " + dev.G.ToString());
            }
            foreach (PSPDEV dev in list4)
            {
                if (dev.FirstNode < 0 || dev.LastNode < 0)
                {
                    string temp = "拓朴分析失败,";
                    temp += dev.Name;
                    temp += "没有正确连接,请进行处理！。";
                    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (outParam1 != null)
                {
                    outParam1 += "\r\n";
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "2" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + (dev.LineGNDC * volt * volt / (current * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString());
            }
            foreach (PSPDEV dev in list5)
            {
                if (outParam1 != null)
                {
                    outParam1 += "\r\n";
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.Name.ToString() + " " + "3" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + (dev.LineGNDC * volt * volt / (current * 1000000)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString());
            }
            foreach (PSPDEV dev in list1)
            {
                if (dev.Number < 0)
                {

                    MessageBox.Show("拓朴分析失败，请进行处理!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //if (dev.InPutQ == 0)
                //    dev.InPutQ = dev.InPutP * Math.Tan(Math.Acos(yinzi));
                //if (dev.OutQ == 0)
                //    dev.OutQ = dev.OutP * Math.Tan(Math.Acos(yinzi));
                if (dev.NodeType == "0")
                {
                    //dev.OutP = 0;
                    //dev.OutQ = 0;
                    //dev.InPutP = 0;
                    //dev.InPutQ = 0;
                }
                if (dev.ReferenceVolt != null && dev.ReferenceVolt != 0)
                {
                    volt = dev.ReferenceVolt;
                }
                else
                    volt = standvolt;
                if (dev.Lable == "电厂")
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "2";
                    }
                }
                else if (dev.Lable == "变电站")
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "1";
                    }
                }
                if (dev.NodeType == "0")
                {
                    dev.NodeType = "3";
                }
                if (outParam2 != null)
                {
                    outParam2 += "\r\n";
                }
                outParam2 += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / volt).ToString() + " " + dev.VoltV.ToString() + " " + ((dev.InPutP - dev.OutP) / current).ToString() + " " + ((dev.InPutQ - dev.OutQ) / current).ToString());
            }

            //outParam1 += (volt + " " + current + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + ";" + "\r\n");
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\branch.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\branch.txt");
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
            }
            //if (File.Exists("c:\\L9.txt"))
            //{
            //    File.Delete("c:\\L9.txt");
            //}
            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\branch.txt"), FileMode.OpenOrCreate);
            StreamWriter str1 = new StreamWriter(VK);
            str1.Write(outParam1);
            str1.Close();
            FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
            StreamWriter str2 = new StreamWriter(L);
            str2.Write(outParam2);
            str2.Close();
            return true;
        }
        private bool ShortCutCheck(string bigsmall)
        {
            string outParam1 = null;
            string outParam2 = null;
            double yinzi = 0;
            double volt = 0;
            double current = 0;
            string bigs = bigsmall;
            // PSPDEV pow = new PSPDEV();
            //pow.Type = "Power";
            // pow.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            // pow = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", pow);

            //     yinzi = pow.PowerFactor;
            //     volt = pow.StandardVolt;
            //     current = pow.StandardCurrent;

            Topology();
            PSPDEV pspDev = new PSPDEV();
            pspDev.Type = "power";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            if (list3 == null)
            {
                MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            foreach (PSPDEV dev in list3)
            {
                yinzi = Convert.ToDouble(dev.PowerFactor);
                current = Convert.ToDouble(dev.StandardCurrent);
                volt = Convert.ToDouble(dev.StandardVolt);
                if (dev.PowerFactor == 0)
                {
                    yinzi = 1;
                }
                if (dev.StandardCurrent == 0)
                {
                    current = 1;
                }
                if (dev.StandardVolt == 0)
                {
                    volt = 1;
                }
            };
            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "Polyline";
            //pspDev.Lable = "支路";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            pspDev.Lable = "接地支路";
            IList list6 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", pspDev);
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            pspDev.Lable = "发电厂支路";
            IList list7 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", pspDev);
            ArrayList list8 = new ArrayList();

            foreach (PSPDEV dev in list2)
            {
                if ((dev.HuganLine1 == "" || dev.HuganLine1 == null) && (dev.HuganLine4 == "" || dev.HuganLine4 == null)
                       && (dev.HuganLine2 == "" || dev.HuganLine2 == null) && (dev.HuganLine3 == "" || dev.HuganLine3 == null))
                {
                    list8.Add(dev);
                }

            }



            outParam1 += (list1.Count + " " + (list2.Count - list6.Count) + " " + "-1" + " " + "-1" + " " + "-1" + ";" + "\r\n");
            outParam2 += (list1.Count + " " + (list8.Count - list7.Count) + " " + "-1" + " " + "-1" + " " + "-1" + ";" + "\r\n");
            //foreach (PSPDEV dev in list1)
            //{
            //    if (dev.Number < 0)
            //    {

            //        MessageBox.Show("拓朴分析失败，请进行处理!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //    dev.InPutQ = dev.InPutP * Math.Tan(Math.Acos(yinzi));
            //    dev.OutQ = dev.OutP * Math.Tan(Math.Acos(yinzi));
            //    outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.OutP.ToString() + " " + dev.OutQ.ToString() + " " + dev.InPutP.ToString() + " " + dev.InPutQ.ToString() + " " + dev.VoltR.ToString() + " " + dev.Burthen + ";" + "\r\n");
            //}
            foreach (PSPDEV dev in list2)
            {
                if (dev.FirstNode < 0 || dev.LastNode < 0)
                {
                    string temp = "拓朴分析失败,";
                    temp += dev.Name;
                    temp += "没有正确连接,请进行处理！。";
                    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (true)
                {
                    if (dev.Lable != "接地支路")
                    {
                        if (dev.Lable == "发电厂支路")
                        {
                            //PSPDEV pspDuanlu = new PSPDEV();
                            //pspDuanlu.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

                            //frmDuanlu dudu = new frmDuanlu(pspDuanlu);
                            if (bigs == "大方式电抗")
                                dev.PositiveTQ = dev.BigTQ;
                            else
                                dev.PositiveTQ = dev.SmallTQ;
                        }
                        outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.PositiveR.ToString() + " " + dev.PositiveTQ.ToString() + " " + dev.Number.ToString() + ";" + "\r\n");
                    }
                    if (dev.Lable != "发电厂支路" && (dev.HuganLine1 == "" || dev.HuganLine1 == null)
                        && (dev.HuganLine2 == "" || dev.HuganLine2 == null) && (dev.HuganLine3 == "" || dev.HuganLine3 == null)
                        && (dev.HuganLine4 == "" || dev.HuganLine4 == null))
                    {
                        if (dev.Lable == "接地支路")
                        {
                            PSPDEV pspDuanlu = new PSPDEV();
                            pspDuanlu.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

                            frmDuanlu dudu = new frmDuanlu(pspDuanlu);
                            if (bigs == "大方式电抗")
                                dev.ZeroTQ = dev.BigTQ;
                            else
                                dev.ZeroTQ = dev.SmallTQ;
                        }
                        outParam2 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.ZeroR.ToString() + " " + dev.ZeroTQ.ToString() + " " + dev.Number.ToString() + ";" + "\r\n");
                    }
                }

            }

            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\datazx.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\datazx.txt");
            }
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\datalx.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\datalx.txt");
            }
            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\datazx.txt"), FileMode.OpenOrCreate);
            StreamWriter str1 = new StreamWriter(VK);
            str1.Write(outParam1);
            str1.Close();
            str1.Dispose();

            FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\datalx.txt"), FileMode.OpenOrCreate);
            StreamWriter str2 = new StreamWriter(L);
            str2.Write(outParam2);
            str2.Close();
            str2.Dispose();


            outParam1 = null;
            outParam2 = null;
            PSPDEV psp = new PSPDEV();
            psp.HuganFirst = 1;
            psp.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

            IList list4 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandHuganFirst", psp);
            outParam1 += (list4.Count + " " + "-1" + " " + "-1" + " " + "-1" + " " + "-1" + " " + "-1" + " " + "-1" + " " + "-1" + ";" + "\r\n");
            foreach (PSPDEV dev in list4)
            {
                ArrayList list5 = new ArrayList();
                PSPDEV pspName = new PSPDEV();
                pspName.Name = dev.HuganLine1;
                pspName.Type = "Polyline";
                pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                PSPDEV hg1 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", pspName);
                if (hg1 != null)
                {
                    list5.Add(hg1);
                }

                pspName.Name = dev.HuganLine2;
                pspName.Type = "Polyline";
                pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                PSPDEV hg2 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", pspName);
                if (hg2 != null)
                {
                    list5.Add(hg2);
                }

                pspName.Name = dev.HuganLine3;
                pspName.Type = "Polyline";
                pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                PSPDEV hg3 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", pspName);
                if (hg3 != null)
                {
                    list5.Add(hg3);
                }

                pspName.Name = dev.HuganLine4;
                pspName.Type = "Polyline";
                pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                PSPDEV hg4 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", pspName);
                if (hg4 != null)
                {
                    list5.Add(hg4);
                }
                outParam1 += ((list5.Count + 1) + " " + "-1" + " " + "-1" + " " + "-1" + " " + "-1" + " " + "-1" + " " + "-1" + " " + "-1" + ";" + "\r\n");
                outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + dev.HuganTQ1.ToString() + " " + dev.HuganTQ2.ToString() + " " + dev.HuganTQ3.ToString() + " " + dev.HuganTQ4.ToString() + " " + dev.HuganTQ5.ToString() + " " + dev.Number.ToString() + ";" + "\r\n");
                foreach (PSPDEV devic in list5)
                {
                    outParam1 += (devic.FirstNode.ToString() + " " + devic.LastNode.ToString() + " " + devic.HuganTQ1.ToString() + " " + devic.HuganTQ2.ToString() + " " + devic.HuganTQ3.ToString() + " " + devic.HuganTQ4.ToString() + " " + devic.HuganTQ5.ToString() + " " + devic.Number.ToString() + ";" + "\r\n");
                }
            }

            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\datahg.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\datahg.txt");
            }
            FileStream hg = new FileStream((System.Windows.Forms.Application.StartupPath + "\\datahg.txt"), FileMode.OpenOrCreate);
            StreamWriter str3 = new StreamWriter(hg);
            str3.Write(outParam1);
            str3.Close();
            str3.Dispose();

            return true;
        }
        private bool CheckPQ()
        {
            string outParam1 = null;
            string outParam2 = null;
            double yinzi = 0;
            double volt = 0;
            double current = 0;
            // PSPDEV pow = new PSPDEV();
            //pow.Type = "Power";
            // pow.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            // pow = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDAndType", pow);

            //     yinzi = pow.PowerFactor;
            //     volt = pow.StandardVolt;
            //     current = pow.StandardCurrent;

            Topology();
            PSPDEV pspDev = new PSPDEV();
            pspDev.Type = "power";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            if (list3 == null)
            {
                MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            foreach (PSPDEV dev in list3)
            {
                yinzi = Convert.ToDouble(dev.PowerFactor);
                current = Convert.ToDouble(dev.StandardCurrent);
                volt = Convert.ToDouble(dev.StandardVolt);
                if (dev.PowerFactor == 0)
                {
                    yinzi = 1;
                }
                if (dev.StandardCurrent == 0)
                {
                    current = 1;
                }
                if (dev.StandardVolt == 0)
                {
                    volt = 1;
                }
            };
            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "TransformLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list4 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);

            int num_Gen = 0;
            int num_Load = 0;
            int num_Line = 0;

            foreach (PSPDEV dev in list1)
            {
                if (dev.Lable.Contains("电厂"))
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "2";
                    }
                }
                else if (dev.Lable.Contains("变电站"))
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "1";
                    }
                }
                if (dev.OutP != 0 || dev.OutQ != 0 || dev.NodeType == "0")
                {
                    num_Gen++;
                }
                if (dev.InPutP != 0 || dev.InPutQ != 0)
                {
                    num_Load++;
                }
                if (dev.OutP == 0 && dev.OutQ == 0 && dev.InPutP == 0 && dev.InPutQ == 0)
                {
                    if (dev.NodeType == "2")
                    {
                        num_Gen++;
                    }
                    else if (dev.NodeType == "1")
                    {
                        num_Load++;
                    }
                }
            }
            foreach (PSPDEV dev in list2)
            {
                if (dev.LineStatus == "运行")
                {
                    num_Line++;
                }
            }
            outParam1 += (num_Line + list4.Count).ToString() + " " + list1.Count.ToString() + " " + list1.Count.ToString() + " " + "0.00001" + " " + "100" + " " + "0" + " " + "0";

            foreach (PSPDEV dev in list2)
            {
                if (dev.FirstNode < 0 || dev.LastNode < 0)
                {
                    string temp = "拓朴分析失败,";
                    temp += dev.Name;
                    temp += "没有正确连接,请进行处理！。";
                    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (outParam1 != null && dev.LineStatus == "运行")
                {
                    outParam1 += "\r\n";
                }
                if (dev.LineStatus == "运行")
                    outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "0" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + ((dev.LineGNDC) * volt * volt / (current * 1000000)).ToString() + " " + "0" + " " + dev.Name.ToString());
            }
            foreach (PSPDEV dev in list4)
            {
                if (dev.FirstNode < 0 || dev.LastNode < 0)
                {
                    string temp = "拓朴分析失败,";
                    temp += dev.Name;
                    temp += "没有正确连接,请进行处理！。";
                    MessageBox.Show(temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (outParam1 != null)
                {
                    outParam1 += "\r\n";
                }
                if (dev.G == null)
                {
                    dev.G = 0;
                }
                outParam1 += (dev.FirstNode.ToString() + " " + dev.LastNode.ToString() + " " + "1" + " " + (dev.LineR * current / (volt * volt)).ToString() + " " + (dev.LineTQ * current / (volt * volt)).ToString() + " " + dev.K.ToString() + " " + dev.G.ToString() + " " + dev.Name.ToString());
            }
            foreach (PSPDEV dev in list1)
            {
                if (dev.Number < 0)
                {

                    MessageBox.Show("拓朴分析失败，请进行处理!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //if (dev.InPutQ == 0)
                //    dev.InPutQ = dev.InPutP * Math.Tan(Math.Acos(yinzi));
                //if (dev.OutQ == 0)
                //    dev.OutQ = dev.OutP * Math.Tan(Math.Acos(yinzi));
                if (dev.NodeType == "0")
                {
                    //dev.OutP = 0;
                    //dev.OutQ = 0;
                    //dev.InPutP = 0;
                    //dev.InPutQ = 0;
                }
                if (dev.Lable == "电厂")
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "2";
                    }
                }
                else if (dev.Lable == "变电站")
                {
                    if (dev.NodeType != "0")
                    {
                        dev.NodeType = "1";
                    }
                }
                if (true)
                {
                    if (outParam1 != null)
                    {
                        outParam1 += "\r\n";
                    }
                    if (dev.NodeType == "1")
                    {
                        outParam1 += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP) / current).ToString() + " " + ((dev.OutQ) / current).ToString());
                    }
                    else if (dev.NodeType == "2")
                    {
                        outParam1 += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP) / current).ToString() + " " + (dev.VoltR / volt).ToString());
                    }
                    else if (dev.NodeType == "0")
                    {
                        outParam1 += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + (dev.VoltR / volt).ToString() + " " + "0");
                    }
                }
                //else if (dev.OutP == 0 && dev.OutQ == 0 && dev.NodeType == "2" && dev.InPutP == 0 && dev.InPutQ == 0)
                //{
                //    if (outParam1 != null)
                //    {
                //        outParam1 += "\r\n";
                //    }
                //    outParam1 += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + dev.NodeType + " " + ((dev.OutP) / current).ToString() + " " + (dev.VoltR / volt).ToString());
                //}
            }
            foreach (PSPDEV dev in list1)
            {
                if (dev.Number < 0)
                {

                    MessageBox.Show("拓朴分析失败，请进行处理!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //if (dev.InPutQ == 0)
                //    dev.InPutQ = dev.InPutP * Math.Tan(Math.Acos(yinzi));
                //if (dev.OutQ == 0)
                //    dev.OutQ = dev.OutP * Math.Tan(Math.Acos(yinzi));
                if (dev.NodeType == "0")
                {
                    //dev.OutP = 0;
                    //dev.OutQ = 0;
                    //dev.InPutP = 0;
                    //dev.InPutQ = 0;
                }
                if (true)
                {
                    if (outParam1 != null)
                    {
                        outParam1 += "\r\n";
                    }
                    outParam1 += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + ((dev.InPutP) / current).ToString() + " " + ((dev.InPutQ) / current).ToString());
                }
                //else if (dev.OutP == 0 && dev.OutQ == 0&&dev.NodeType == "1" && dev.InPutP == 0 && dev.InPutQ == 0)
                //{
                //    if (outParam1 != null)
                //    {
                //        outParam1 += "\r\n";
                //    }
                //    outParam1 += (dev.Number.ToString() + " " + dev.Name.ToString() + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + "0" + " " + ((dev.InPutP) / current).ToString() + " " + ((dev.InPutQ) / current).ToString());
                //}
            }
            //outParam1 += (volt + " " + current + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + " " + "-2" + ";" + "\r\n");
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\data.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\data.txt");
            }
            //if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\bus.txt"))
            //{
            //    File.Delete(System.Windows.Forms.Application.StartupPath + "\\bus.txt");
            //}
            //if (File.Exists("c:\\L9.txt"))
            //{
            //    File.Delete("c:\\L9.txt");
            //}
            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\data.txt"), FileMode.OpenOrCreate);
            StreamWriter str1 = new StreamWriter(VK);
            str1.Write(outParam1);
            str1.Close();
            //FileStream L = new FileStream((System.Windows.Forms.Application.StartupPath + "\\bus.txt"), FileMode.OpenOrCreate);
            //StreamWriter str2 = new StreamWriter(L);
            //str2.Write(outParam2);
            //str2.Close();
            return true;
        }

        public int brchcount, buscount, transcount;        //记录全网参与潮流计算的支路数和母线数目
        private void Topologyfuhe()
        {
            brchcount = 0; buscount = 0; transcount = 0;
            //XPathNavigator nav = tlVectorControl1.SVGDocument.CreateNavigator();
            //XPathExpression exp = nav.Compile("svg/use");
            //exp.AddSort("x", XmlSortOrder.Ascending, XmlCaseOrder.None, "", XmlDataType.Number);
            XmlNodeList nodeList1 = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@layer='" + tlVectorControl1.SVGDocument.CurrentLayer.ID + "']");
            //XPathNodeIterator nodeList1 = nav.Select(exp);            
            PSPDEV pspDev = new PSPDEV();
            foreach (XmlNode node in nodeList1)
            {
                XmlElement element = node as XmlElement;
                RectangleF t = ((IGraph)element).GetBounds();
                XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                //if (temp == null)
                //    return;
                if (element.GetAttribute("xlink:href").Contains("Power") || element.GetAttribute("xlink:href").Contains("motherlinenode"))
                {
                    pspDev.EleID = element.GetAttribute("id");
                    if (temp != null)
                        pspDev.Name = temp.InnerText;
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.X1 = t.X;
                    pspDev.Y1 = t.Y;
                    pspDev.X2 = t.X + t.Width;
                    pspDev.Y2 = t.Y + t.Height;
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = -1;
                    pspDev.Number = -1;
                    Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                }
                else if (element.GetAttribute("xlink:href").Contains("Substation"))
                {
                    pspDev.EleID = element.GetAttribute("id");
                    if (temp != null)
                        pspDev.Name = temp.InnerText;
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.X1 = t.X + t.Width / 2;
                    pspDev.Y1 = t.Y + t.Height / 2;
                    pspDev.X2 = 0;
                    pspDev.Y2 = 0;
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = -1;
                    pspDev.Number = -1;
                    Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                }
                else if (element.GetAttribute("xlink:href").Contains("dynamotorline") || element.GetAttribute("xlink:href").Contains("gndline"))
                {
                    Transf transfElement = (element as Use).Transform;
                    RectangleF tt = (element as Use).GetRectangle();
                    float x = tt.X;
                    float y = tt.Y + tt.Height / 2;
                    PointF[] startPoint = new PointF[] { new PointF(x, y) };
                    transfElement.Matrix.TransformPoints(startPoint);
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

                    pspDev.X1 = startPoint[0].X;
                    pspDev.Y1 = startPoint[0].Y;
                    pspDev.X2 = t.X + t.Width;
                    pspDev.Y2 = t.Y + t.Height;
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = 0;
                    pspDev.Number = -1;
                    Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                }
            }
            XmlNodeList nodeList2 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@layer='" + tlVectorControl1.SVGDocument.CurrentLayer.ID + "']");
            foreach (XmlNode node in nodeList2)
            {
                XmlElement element = node as XmlElement;
                if ((element.GetAttribute("flag") == "1") || (!element.HasAttributes) || element.GetAttribute("id") == "")
                {
                    break;
                }
                PointF[] t = ((Polyline)element).Pt;
                //XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                pspDev.EleID = element.GetAttribute("id");
                //pspDev.Name = temp.InnerText;
                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                pspDev.X1 = t[0].X;
                pspDev.Y1 = t[0].Y;
                pspDev.X2 = t[1].X;
                pspDev.Y2 = t[1].Y;
                pspDev.FirstNode = -1;
                pspDev.LastNode = -1;
                pspDev.Number = -1;
                //PSPDEV psp = new PSPDEV();
                //psp.EleID = element.GetAttribute("id");
                //psp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                //psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", psp);
                //if (psp == null)
                //{
                //    pspDev.Number = -1;
                //    pspDev.Name = null;
                //    pspDev.Type = "Polyline";
                //    pspDev.FirstNode = -1;
                //    pspDev.LastNode = -1;
                //    pspDev.SUID = Guid.NewGuid().ToString();
                //    Services.BaseService.Create<PSPDEV>(pspDev);

                //}
                //else
                //{
                Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                //}

            }
            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            for (int i = 1; i <= list1.Count; i++)
            {
                pspDev = (PSPDEV)list1[i - 1];
                pspDev.Number = i;
                Services.BaseService.Update<PSPDEV>(pspDev);
                buscount += 1;                            //记录母线数


            }
            // waitlinecoll.Clear();               //清空原来的线路


            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            int j = 0;
            for (int i = 1; i <= list2.Count; i++)
            {
                pspDev = (PSPDEV)list2[i - 1];
                if (pspDev.LineStatus == "断开" || (pspDev.LineStatus == "等待"))
                {
                    j += 1;
                    pspDev.Number = -1;
                    Services.BaseService.Update<PSPDEV>(pspDev);
                    continue;

                }

                pspDev.Number = (i - j);
                brchcount += 1;
                /*
                if (pspDev.LineStatus == "待选" || pspDev.EleID == elid)
                                {
                                    linedaixuan ll = new linedaixuan(brchcount, pspDev.SUID, pspDev.Name);
                                    waitlinecoll.Add(ll);
                                }*/

                Services.BaseService.Update<PSPDEV>(pspDev);
            }
            pspDev.Type = "TransformLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            for (int i = 1; i <= list3.Count; i++)
            {
                pspDev = (PSPDEV)list3[i - 1];
                pspDev.Number = list2.Count - j + i;
                transcount += 1;
                //取首末节点


                PSPDEV dev = new PSPDEV();
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine1;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.FirstNode = dev.Number;
                }
                else
                {
                    pspDev.FirstNode = -1;
                    dev = new PSPDEV();
                }
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine2;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.LastNode = dev.Number;
                }
                else
                {
                    pspDev.LastNode = -1;
                }
                Services.BaseService.Update<PSPDEV>(pspDev);
            }

            pspDev.Type = "GNDLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list13 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            for (int i = 1; i <= list13.Count; i++)
            {
                pspDev = (PSPDEV)list13[i - 1];
                pspDev.Number = list2.Count + list3.Count - j + i;
                transcount += 1;
                //取首末节点


                PSPDEV dev = new PSPDEV();
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine1;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.FirstNode = dev.Number;
                }
                else
                {
                    pspDev.FirstNode = -1;
                    dev = new PSPDEV();
                }
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine2;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.LastNode = dev.Number;
                }
                else
                {
                    pspDev.LastNode = -1;
                }
                Services.BaseService.Update<PSPDEV>(pspDev);
            }

            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID; ;
            list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            foreach (PSPDEV dev in list1)
            {
                double devx = Convert.ToDouble(dev.X1);
                double devy = Convert.ToDouble(dev.Y1);
                XmlElement temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + dev.EleID + "']") as XmlElement;

                if (temp.GetAttribute("xlink:href").Contains("Substation"))
                {
                    RectangleF t = ((IGraph)temp).GetBounds();
                    foreach (PSPDEV psp in list2)
                    {
                        double x1 = psp.X1;
                        double x2 = psp.X2;
                        double y1 = psp.Y1;
                        double y2 = psp.Y2;
                        if (Math.Abs(devx - x1) <= ((t.Height) / 2) && Math.Abs(devy - y1) <= ((t.Height) / 2))
                        {
                            psp.FirstNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                        if (Math.Abs(devx - x2) <= ((t.Height) / 2) && Math.Abs(devy - y2) <= ((t.Height) / 2))
                        {
                            psp.LastNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                    }
                }
                else if (temp.GetAttribute("xlink:href").Contains("Power") || temp.GetAttribute("xlink:href").Contains("motherlinenode"))
                {
                    RectangleF t = ((IGraph)temp).GetBounds();
                    foreach (PSPDEV psp in list2)
                    {
                        double x1 = psp.X1;
                        double x2 = psp.X2;
                        double y1 = psp.Y1;
                        double y2 = psp.Y2;
                        if ((x1 - devx) <= t.Width && (y1 - devy) <= t.Height && x1 >= devx && y1 >= devy)
                        {
                            psp.FirstNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                        if ((x2 - devx) <= t.Width && (y2 - devy) <= t.Height && x2 >= devx && y2 >= devy)
                        {
                            psp.LastNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                    }
                }
            }
        }
        private void Topologyadd(string elid)
        {
            brchcount = 0; buscount = 0; transcount = 0;
            //XPathNavigator nav = tlVectorControl1.SVGDocument.CreateNavigator();
            //XPathExpression exp = nav.Compile("svg/use");
            //exp.AddSort("x", XmlSortOrder.Ascending, XmlCaseOrder.None, "", XmlDataType.Number);
            XmlNodeList nodeList1 = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@layer='" + tlVectorControl1.SVGDocument.CurrentLayer.ID + "']");
            //XPathNodeIterator nodeList1 = nav.Select(exp);            
            PSPDEV pspDev = new PSPDEV();
            foreach (XmlNode node in nodeList1)
            {
                XmlElement element = node as XmlElement;
                RectangleF t = ((IGraph)element).GetBounds();
                XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                //if (temp == null)
                //    return;
                if (element.GetAttribute("xlink:href").Contains("Power") || element.GetAttribute("xlink:href").Contains("motherlinenode"))
                {
                    pspDev.EleID = element.GetAttribute("id");
                    if (temp != null)
                        pspDev.Name = temp.InnerText;
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.X1 = t.X;
                    pspDev.Y1 = t.Y;
                    pspDev.X2 = t.X + t.Width;
                    pspDev.Y2 = t.Y + t.Height;
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = -1;
                    pspDev.Number = -1;
                    Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                }
                else if (element.GetAttribute("xlink:href").Contains("Substation"))
                {
                    pspDev.EleID = element.GetAttribute("id");
                    if (temp != null)
                        pspDev.Name = temp.InnerText;
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.X1 = t.X + t.Width / 2;
                    pspDev.Y1 = t.Y + t.Height / 2;
                    pspDev.X2 = 0;
                    pspDev.Y2 = 0;
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = -1;
                    pspDev.Number = -1;
                    Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                }
                else if (element.GetAttribute("xlink:href").Contains("dynamotorline") || element.GetAttribute("xlink:href").Contains("gndline"))
                {
                    Transf transfElement = (element as Use).Transform;
                    RectangleF tt = (element as Use).GetRectangle();
                    float x = tt.X;
                    float y = tt.Y + tt.Height / 2;
                    PointF[] startPoint = new PointF[] { new PointF(x, y) };
                    transfElement.Matrix.TransformPoints(startPoint);
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

                    pspDev.X1 = startPoint[0].X;
                    pspDev.Y1 = startPoint[0].Y;
                    pspDev.X2 = t.X + t.Width;
                    pspDev.Y2 = t.Y + t.Height;
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = 0;
                    pspDev.Number = -1;
                    Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                }
            }
            XmlNodeList nodeList2 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@layer='" + tlVectorControl1.SVGDocument.CurrentLayer.ID + "']");
            foreach (XmlNode node in nodeList2)
            {
                XmlElement element = node as XmlElement;
                if ((element.GetAttribute("flag") == "1") || (!element.HasAttributes) || element.GetAttribute("id") == "")
                {
                    break;
                }
                PointF[] t = ((Polyline)element).Pt;
                //XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                pspDev.EleID = element.GetAttribute("id");
                //pspDev.Name = temp.InnerText;
                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                pspDev.X1 = t[0].X;
                pspDev.Y1 = t[0].Y;
                pspDev.X2 = t[1].X;
                pspDev.Y2 = t[1].Y;
                pspDev.FirstNode = -1;
                pspDev.LastNode = -1;
                pspDev.Number = -1;
                //PSPDEV psp = new PSPDEV();
                //psp.EleID = element.GetAttribute("id");
                //psp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                //psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", psp);
                //if (psp == null)
                //{
                //    pspDev.Number = -1;
                //    pspDev.Name = null;
                //    pspDev.Type = "Polyline";
                //    pspDev.FirstNode = -1;
                //    pspDev.LastNode = -1;
                //    pspDev.SUID = Guid.NewGuid().ToString();
                //    Services.BaseService.Create<PSPDEV>(pspDev);

                //}
                //else
                //{
                Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                //}

            }
            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            for (int i = 1; i <= list1.Count; i++)
            {
                pspDev = (PSPDEV)list1[i - 1];
                pspDev.Number = i;
                Services.BaseService.Update<PSPDEV>(pspDev);
                buscount += 1;                            //记录母线数


            }
            // waitlinecoll.Clear();               //清空原来的线路


            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            int j = 0;
            for (int i = 1; i <= list2.Count; i++)
            {
                pspDev = (PSPDEV)list2[i - 1];
                if (pspDev.LineStatus == "断开" || (pspDev.LineStatus == "等待" && pspDev.EleID != elid))
                {
                    j += 1;
                    pspDev.Number = -1;
                    Services.BaseService.Update<PSPDEV>(pspDev);
                    continue;

                }

                pspDev.Number = (i - j);
                brchcount += 1;
                /*
                if (pspDev.LineStatus == "待选" || pspDev.EleID == elid)
                                {
                                    linedaixuan ll = new linedaixuan(brchcount, pspDev.SUID, pspDev.Name);
                                    waitlinecoll.Add(ll);
                                }*/

                Services.BaseService.Update<PSPDEV>(pspDev);
            }
            pspDev.Type = "TransformLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            for (int i = 1; i <= list3.Count; i++)
            {
                pspDev = (PSPDEV)list3[i - 1];
                pspDev.Number = list2.Count - j + i;
                transcount += 1;
                //取首末节点


                PSPDEV dev = new PSPDEV();
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine1;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.FirstNode = dev.Number;
                }
                else
                {
                    pspDev.FirstNode = -1;
                    dev = new PSPDEV();
                }
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine2;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.LastNode = dev.Number;
                }
                else
                {
                    pspDev.LastNode = -1;
                }
                Services.BaseService.Update<PSPDEV>(pspDev);
            }

            pspDev.Type = "GNDLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list13 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            for (int i = 1; i <= list13.Count; i++)
            {
                pspDev = (PSPDEV)list13[i - 1];
                pspDev.Number = list2.Count + list3.Count - j + i;
                transcount += 1;
                //取首末节点


                PSPDEV dev = new PSPDEV();
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine1;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.FirstNode = dev.Number;
                }
                else
                {
                    pspDev.FirstNode = -1;
                    dev = new PSPDEV();
                }
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine2;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.LastNode = dev.Number;
                }
                else
                {
                    pspDev.LastNode = -1;
                }
                Services.BaseService.Update<PSPDEV>(pspDev);
            }

            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID; ;
            list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            foreach (PSPDEV dev in list1)
            {
                double devx = Convert.ToDouble(dev.X1);
                double devy = Convert.ToDouble(dev.Y1);
                XmlElement temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + dev.EleID + "']") as XmlElement;

                if (temp.GetAttribute("xlink:href").Contains("Substation"))
                {
                    RectangleF t = ((IGraph)temp).GetBounds();
                    foreach (PSPDEV psp in list2)
                    {
                        double x1 = psp.X1;
                        double x2 = psp.X2;
                        double y1 = psp.Y1;
                        double y2 = psp.Y2;
                        if (Math.Abs(devx - x1) <= ((t.Height) / 2) && Math.Abs(devy - y1) <= ((t.Height) / 2))
                        {
                            psp.FirstNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                        if (Math.Abs(devx - x2) <= ((t.Height) / 2) && Math.Abs(devy - y2) <= ((t.Height) / 2))
                        {
                            psp.LastNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                    }
                }
                else if (temp.GetAttribute("xlink:href").Contains("Power") || temp.GetAttribute("xlink:href").Contains("motherlinenode"))
                {
                    RectangleF t = ((IGraph)temp).GetBounds();
                    foreach (PSPDEV psp in list2)
                    {
                        double x1 = psp.X1;
                        double x2 = psp.X2;
                        double y1 = psp.Y1;
                        double y2 = psp.Y2;
                        if ((x1 - devx) <= t.Width && (y1 - devy) <= t.Height && x1 >= devx && y1 >= devy)
                        {
                            psp.FirstNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                        if ((x2 - devx) <= t.Width && (y2 - devy) <= t.Height && x2 >= devx && y2 >= devy)
                        {
                            psp.LastNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                    }
                }
            }
        }
        public List<linedaixuan> waitlinecoll = new List<linedaixuan>();
        private void Topology()
        {

            brchcount = 0; buscount = 0; transcount = 0;
            //XPathNavigator nav = tlVectorControl1.SVGDocument.CreateNavigator();
            //XPathExpression exp = nav.Compile("svg/use");
            //exp.AddSort("x", XmlSortOrder.Ascending, XmlCaseOrder.None, "", XmlDataType.Number);
            XmlNodeList nodeList1 = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@layer='" + tlVectorControl1.SVGDocument.CurrentLayer.ID + "']");
            //XPathNodeIterator nodeList1 = nav.Select(exp);            
            PSPDEV pspDev = new PSPDEV();
            foreach (XmlNode node in nodeList1)
            {
                XmlElement element = node as XmlElement;
                RectangleF t = ((IGraph)element).GetBounds();
                XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                //if (temp == null)
                //    return;
                if (element.GetAttribute("xlink:href").Contains("Power") || element.GetAttribute("xlink:href").Contains("motherlinenode"))
                {
                    pspDev.EleID = element.GetAttribute("id");
                    if (temp != null)
                        pspDev.Name = temp.InnerText;
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.X1 = t.X;
                    pspDev.Y1 = t.Y;
                    pspDev.X2 = t.X + t.Width;
                    pspDev.Y2 = t.Y + t.Height;
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = -1;
                    pspDev.Number = -1;
                    Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                }
                else if (element.GetAttribute("xlink:href").Contains("Substation"))
                {
                    pspDev.EleID = element.GetAttribute("id");
                    if (temp != null)
                        pspDev.Name = temp.InnerText;
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev.X1 = t.X + t.Width / 2;
                    pspDev.Y1 = t.Y + t.Height / 2;
                    pspDev.X2 = 0;
                    pspDev.Y2 = 0;
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = -1;
                    pspDev.Number = -1;
                    Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                }
                else if (element.GetAttribute("xlink:href").Contains("dynamotorline") || element.GetAttribute("xlink:href").Contains("gndline"))
                {
                    Transf transfElement = (element as Use).Transform;
                    RectangleF tt = (element as Use).GetRectangle();
                    float x = tt.X;
                    float y = tt.Y + tt.Height / 2;
                    PointF[] startPoint = new PointF[] { new PointF(x, y) };
                    transfElement.Matrix.TransformPoints(startPoint);
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;

                    pspDev.X1 = startPoint[0].X;
                    pspDev.Y1 = startPoint[0].Y;
                    pspDev.X2 = t.X + t.Width;
                    pspDev.Y2 = t.Y + t.Height;
                    pspDev.FirstNode = -1;
                    pspDev.LastNode = 0;
                    pspDev.Number = -1;
                    Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                }
            }
            XmlNodeList nodeList2 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@layer='" + tlVectorControl1.SVGDocument.CurrentLayer.ID + "']");
            foreach (XmlNode node in nodeList2)
            {
                XmlElement element = node as XmlElement;
                if ((element.GetAttribute("flag") == "1") || (!element.HasAttributes) || element.GetAttribute("id") == "")
                {
                    break;
                }
                PointF[] t = ((Polyline)element).Pt;
                //XmlNode temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                pspDev.EleID = element.GetAttribute("id");
                //pspDev.Name = temp.InnerText;
                pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                pspDev.X1 = t[0].X;
                pspDev.Y1 = t[0].Y;
                pspDev.X2 = t[1].X;
                pspDev.Y2 = t[1].Y;
                pspDev.FirstNode = -1;
                pspDev.LastNode = -1;
                pspDev.Number = -1;
                //PSPDEV psp = new PSPDEV();
                //psp.EleID = element.GetAttribute("id");
                //psp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                //psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", psp);
                //if (psp == null)
                //{
                //    pspDev.Number = -1;
                //    pspDev.Name = null;
                //    pspDev.Type = "Polyline";
                //    pspDev.FirstNode = -1;
                //    pspDev.LastNode = -1;
                //    pspDev.SUID = Guid.NewGuid().ToString();
                //    Services.BaseService.Create<PSPDEV>(pspDev);

                //}
                //else
                //{
                Services.BaseService.Update("UpdatePSPDEVByEleID", pspDev);
                //}

            }
            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            for (int i = 1; i <= list1.Count; i++)
            {
                pspDev = (PSPDEV)list1[i - 1];
                pspDev.Number = i;
                Services.BaseService.Update<PSPDEV>(pspDev);
                buscount += 1;                            //记录母线数


            }
            waitlinecoll.Clear();               //清空原来的线路


            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            int j = 0;
            for (int i = 1; i <= list2.Count; i++)
            {
                pspDev = (PSPDEV)list2[i - 1];
                if (pspDev.LineStatus == "断开" || pspDev.LineStatus == "等待")
                {
                    j += 1;
                    pspDev.Number = -1;
                    Services.BaseService.Update<PSPDEV>(pspDev);
                    continue;

                }
                pspDev.Number = (i - j);
                brchcount += 1;
                if (pspDev.LineStatus == "待选")
                {
                    linedaixuan ll = new linedaixuan(brchcount, pspDev.SUID, pspDev.Name);
                    waitlinecoll.Add(ll);
                }
                Services.BaseService.Update<PSPDEV>(pspDev);
            }
            pspDev.Type = "TransformLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            for (int i = 1; i <= list3.Count; i++)
            {
                pspDev = (PSPDEV)list3[i - 1];
                pspDev.Number = list2.Count - j + i;
                transcount += 1;
                //取首末节点


                PSPDEV dev = new PSPDEV();
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine1;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.FirstNode = dev.Number;
                }
                else
                {
                    pspDev.FirstNode = -1;
                    dev = new PSPDEV();
                }
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine2;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.LastNode = dev.Number;
                }
                else
                {
                    pspDev.LastNode = -1;
                }
                Services.BaseService.Update<PSPDEV>(pspDev);
            }

            pspDev.Type = "GNDLine";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            IList list13 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            for (int i = 1; i <= list13.Count; i++)
            {
                pspDev = (PSPDEV)list13[i - 1];
                pspDev.Number = list2.Count + list3.Count - j + i;
                transcount += 1;
                //取首末节点


                PSPDEV dev = new PSPDEV();
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine1;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.FirstNode = dev.Number;
                }
                else
                {
                    pspDev.FirstNode = -1;
                    dev = new PSPDEV();
                }
                dev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                dev.Name = pspDev.HuganLine2;
                dev.Type = "Use";
                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", dev);
                if (dev != null)
                {
                    pspDev.LastNode = dev.Number;
                }
                else
                {
                    pspDev.LastNode = -1;
                }
                Services.BaseService.Update<PSPDEV>(pspDev);
            }

            pspDev.Type = "Use";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            pspDev.Type = "Polyline";
            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID; ;
            list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            foreach (PSPDEV dev in list1)
            {
                double devx = Convert.ToDouble(dev.X1);
                double devy = Convert.ToDouble(dev.Y1);
                XmlElement temp = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + dev.EleID + "']") as XmlElement;
                if (temp == null)
                    continue;

                if (temp.GetAttribute("xlink:href").Contains("Substation"))
                {
                    RectangleF t = ((IGraph)temp).GetBounds();
                    foreach (PSPDEV psp in list2)
                    {
                        double x1 = psp.X1;
                        double x2 = psp.X2;
                        double y1 = psp.Y1;
                        double y2 = psp.Y2;
                        if (Math.Abs(devx - x1) <= ((t.Height) / 2) && Math.Abs(devy - y1) <= ((t.Height) / 2))
                        {
                            psp.FirstNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                        if (Math.Abs(devx - x2) <= ((t.Height) / 2) && Math.Abs(devy - y2) <= ((t.Height) / 2))
                        {
                            psp.LastNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                    }
                }
                else if (temp.GetAttribute("xlink:href").Contains("Power") || temp.GetAttribute("xlink:href").Contains("motherlinenode"))
                {
                    RectangleF t = ((IGraph)temp).GetBounds();
                    foreach (PSPDEV psp in list2)
                    {
                        double x1 = psp.X1;
                        double x2 = psp.X2;
                        double y1 = psp.Y1;
                        double y2 = psp.Y2;
                        if ((x1 - devx) <= t.Width && (y1 - devy) <= t.Height && x1 >= devx && y1 >= devy)
                        {
                            psp.FirstNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                        if ((x2 - devx) <= t.Width && (y2 - devy) <= t.Height && x2 >= devx && y2 >= devy)
                        {
                            psp.LastNode = dev.Number;
                            Services.BaseService.Update<PSPDEV>(psp);
                        }
                    }
                }
            }
        }

        private void frmTLpsp_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            try
            {
                if (tlVectorControl1.IsModified)
                {
                    string a;


                    if ((a = Convert.ToString(MessageBox.Show("图形已修改，是否保存?", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))) == "Yes")
                    {

                        //frmElementName dlg = new frmElementName();

                        //dlg.TextInput = tlVectorControl1.SVGDocument.FileName;

                        //if (dlg.ShowDialog() == DialogResult.OK)
                        //{
                        //    tlVectorControl1.SVGDocument.FileName = dlg.TextInput;
                        //}
                        SaveAllLayer();
                        e.Cancel = false;
                    }
                    //else if (a == "Cancel")
                    //{
                    //    e.Cancel = true;
                    //    return;
                    //}
                    //else e.Cancel = true;
                    //else
                    //{
                    //a = Convert.ToString((MessageBox.Show("图形已修改，是否保存?", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)));
                    //if (ShowDialog == DialogResult.Cancel)
                    //{
                    //return;
                    //}
                    //}
                    //else
                    //{
                    //    PSPDEV pspDev = new PSPDEV();
                    //    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    //    IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUID", pspDev);
                    //    foreach (PSPDEV dev in list)
                    //    {
                    //        Services.BaseService.Delete<PSPDEV>(dev);
                    //    } 
                    //}
                    //}
                }
                //else
                //    return;
            }
            catch (Exception e1) { }
        }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == "短路计算")
            {
               
            }

            if (e.ClickedItem.Text == "区域打印")
            {
                PrintHelper ph = new PrintHelper(tlVectorControl1, mapview);
                frmPrinter dlg = new frmPrinter();
                dlg.printHelper = ph;
                dlg.ShowDialog();
                return;
                ArrayList idlist = new ArrayList();
                ArrayList symlist = new ArrayList();

                SvgDocument _doc = new SvgDocument();

                Graph poly1 = tlVectorControl1.SVGDocument.CurrentElement as Graph;
                if (poly1 == null || poly1.GetAttribute("id") == "svg")
                {
                    return;
                }

                GraphicsPath gr1 = new GraphicsPath();
                gr1.AddPolygon(TLMath.getPolygonPoints(poly1));
                gr1 = (GraphicsPath)poly1.GPath.Clone();
                gr1.Transform((poly1 as IGraph).Transform.Matrix);

                RectangleF ef1 = gr1.GetBounds();
                ef1 = PathFunc.GetBounds(gr1);
                StringBuilder svgtxt = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><svg id=\"svg\" width=\"" + ef1.Width + "\" height=\"" + ef1.Height + "\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:itop=\"http://www.Itop.com/itop\">");

                XmlNodeList nlist = tlVectorControl1.SVGDocument.GetElementsByTagName("defs");
                if (nlist.Count > 0)
                {
                    XmlNode node = nlist[0];
                    svgtxt.AppendLine(node.OuterXml);
                }
                SvgElementCollection.ISvgElementEnumerator enumerator1 = tlVectorControl1.DrawArea.ElementList.GetEnumerator();// mouseAreaControl.PicturePanel.ElementList.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    IGraph graph1 = (IGraph)enumerator1.Current;

                    GraphicsPath path1 = (GraphicsPath)graph1.GPath.Clone();
                    if (!graph1.Visible || !graph1.DrawVisible || !graph1.Layer.Visible) continue;

                    GraphicsPath path2 = (GraphicsPath)graph1.GPath.Clone();
                    path2.Transform(graph1.Transform.Matrix);
                    RectangleF ef2 = PathFunc.GetBounds(path2);

                    if (ef1.Contains(ef2) || RectangleF.Intersect(ef1, ef2) != RectangleF.Empty)
                    {
                        SvgElement ele = (SvgElement)graph1;
                        svgtxt.AppendLine(ele.OuterXml);
                        if (graph1 is Use)
                        {
                            string symid = ((XmlElement)graph1).GetAttribute("xlink:href");
                            if (!symlist.Contains(symid))
                            {
                                symlist.Add(symid);
                            }
                        }
                        if (graph1.GetType().FullName == "ItopVector.Core.Figure.Polyline")
                        {
                            string IsLead = ((XmlElement)graph1).GetAttribute("IsLead");
                            if (IsLead != "")
                            {
                                if (ef1.Contains(ef2))
                                {
                                    idlist.Add(graph1.ID);
                                }
                            }
                        }
                    }

                }
                //symlist = ResetList(symlist);
                svgtxt.AppendLine("</svg>");
                _doc.LoadXml(svgtxt.ToString());
                _doc.SvgdataUid = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                frmPrintF pri = new frmPrintF();
                pri.Init(tlVectorControl1.SVGDocument.CurrentElement.ID, tlVectorControl1.SVGDocument.CurrentLayer.ID);
                if (pri.ShowDialog() == DialogResult.OK)
                {
                    frmSubPrint s = new frmSubPrint();
                    s.Vector = tlVectorControl1;
                    s.InitImg(pri.strzt, pri.strgs, pri.pri, idlist, symlist);
                    s.Open(_doc, ef1);
                    s.Show();
                }
            }


            if (e.ClickedItem.Text == "属性")
            {
                //if (!Check())
                //{
                //    return;
                //}
                XmlElement element = tlVectorControl1.SVGDocument.CurrentElement;
                if (element is Use)
                {
                    if (element.GetAttribute("xlink:href").Contains("Substation") || element.GetAttribute("xlink:href").Contains("motherlinenode"))
                    {

                        string str_power = getPower(element.GetAttribute("xlink:href"));

                        PSPDEV pspDev = new PSPDEV();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                        frmSubstation dlg;
                        if (pspDev != null)
                        {
                            dlg = new frmSubstation(pspDev);

                        }
                        else
                        {
                            pspDev = new PSPDEV();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = element.GetAttribute("id");
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = -1;
                            pspDev.Type = "Use";
                            if (element.GetAttribute("xlink:href").Contains("Substation"))
                            {
                                pspDev.Lable = "变电站";
                            }
                            else if (element.GetAttribute("xlink:href").Contains("motherlinenode"))
                            {
                                pspDev.Lable = "母线节点";
                            }
                            else if (element.GetAttribute("xlink:href").Contains("Power"))
                            {
                                pspDev.Lable = "电厂";
                            }
                            Services.BaseService.Create<PSPDEV>(pspDev);
                            dlg = new frmSubstation(pspDev);
                        }
                        dlg.Str_Power = str_power;

                        dlg.TYear = tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("year");

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (dlg.Name == null)
                            {
                                MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "Use";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            pspDev.Name = dlg.Name;
                            XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                            if (text != null)
                            {
                                (text as Text).InnerText = dlg.Name;
                                (text as Text).SetAttribute("print", dlg.IsTJ ? "no" : "yes");
                            }
                            pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                            pspDev.ReferenceVolt = Convert.ToDouble(dlg.ReferenceVolt);
                            pspDev.Burthen = Convert.ToDecimal(dlg.Burthen);
                            element.SetAttribute("print", dlg.IsTJ ? "no" : "yes");

                            pspDev.OutP = Convert.ToDouble(dlg.OutP);
                            pspDev.OutQ = Convert.ToDouble(dlg.OutQ); ;
                            //if (pspDev.InPutP==0)
                            pspDev.InPutP = Convert.ToDouble(dlg.InPutP);
                            pspDev.InPutQ = Convert.ToDouble(dlg.InPutQ);
                            pspDev.ReferenceVolt = Convert.ToDouble(dlg.ReferenceVolt);

                            if (dlg.NodeType == "是")
                            {
                                pspDev.NodeType = "0";
                            }
                            else
                            {
                                pspDev.NodeType = "1";
                            }
                            Services.BaseService.Update<PSPDEV>(pspDev);
                            tlVectorControl1.SVGDocument.CurrentElement.SetAttribute("year", dlg.TYear);
                        }
                    }
                    else if (element.GetAttribute("xlink:href").Contains("Power"))
                    {
                        PSPDEV pspDev = new PSPDEV();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                        frmSubstation dlg;
                        if (pspDev != null)
                        {
                            dlg = new frmSubstation(pspDev);
                        }
                        else
                        {
                            pspDev = new PSPDEV();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = element.GetAttribute("id");
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = -1;
                            pspDev.Type = "Use";
                            if (element.GetAttribute("xlink:href").Contains("Substation"))
                            {
                                pspDev.Lable = "变电站";
                            }
                            else if (element.GetAttribute("xlink:href").Contains("motherlinenode"))
                            {
                                pspDev.Lable = "母线节点";
                            }
                            else if (element.GetAttribute("xlink:href").Contains("Power"))
                            {
                                pspDev.Lable = "电厂";
                            }
                            Services.BaseService.Create<PSPDEV>(pspDev);
                            dlg = new frmSubstation(pspDev);
                            dlg.TYear = tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("year");
                        }

                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (dlg.Name == null)
                            {
                                MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "Use";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            pspDev.Name = dlg.Name;
                            XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                            if (text != null)
                            {
                                (text as Text).InnerText = dlg.Name;
                            }
                            pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                            pspDev.Burthen = Convert.ToDecimal(dlg.Burthen);
                            pspDev.OutP = Convert.ToDouble(dlg.OutP);
                            pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                            pspDev.InPutP = Convert.ToDouble(dlg.InPutP);
                            pspDev.InPutQ = Convert.ToDouble(dlg.InPutQ);
                            pspDev.ReferenceVolt = Convert.ToDouble(dlg.ReferenceVolt);

                            if (dlg.NodeType == "是")
                            {
                                pspDev.NodeType = "0";
                            }
                            else
                            {
                                pspDev.NodeType = "2";
                            }
                            Services.BaseService.Update<PSPDEV>(pspDev);
                        }
                    }
                    else if (element.GetAttribute("xlink:href").Contains("dynamotorline"))
                    {
                        PSPDEV pspDev = new PSPDEV();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                        frmFadejie dlg;
                        if (pspDev != null)
                        {
                            dlg = new frmFadejie(pspDev, pspDev.SvgUID);
                        }
                        else
                        {
                            pspDev = new PSPDEV();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = element.GetAttribute("id");
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = 0;
                            pspDev.Type = "dynamotorline";
                            if (element.GetAttribute("xlink:href").Contains("dynamotorline"))
                            {
                                pspDev.Lable = "发电厂支路";
                            }
                            else if (element.GetAttribute("xlink:href").Contains("gndline"))
                            {
                                pspDev.Lable = "接地支路";
                            }
                            Services.BaseService.Create<PSPDEV>(pspDev);
                            dlg = new frmFadejie(pspDev, pspDev.SvgUID);
                        }
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (dlg.Name == null)
                            {
                                MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "dynamotorline";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            pspDev.Name = dlg.Name;
                            XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                            if (text != null)
                            {
                                (text as Text).InnerText = dlg.Name;
                            }
                            pspDev.HuganLine1 = dlg.FirstNodeName;
                            pspDev.HuganLine3 = dlg.SwitchStatus;
                            if (dlg.OutP != "")
                                pspDev.OutP = Convert.ToDouble(dlg.OutP);
                            if (dlg.OutQ != "")
                                pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                            if (dlg.VoltR != "")
                                pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                            if (dlg.VoltV != "")
                                pspDev.VoltV = Convert.ToDouble(dlg.VoltV);
                            if (dlg.PositiveTQ != "")
                                pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                            if (dlg.NegativeTQ != "")
                                pspDev.ZeroTQ = Convert.ToDouble(dlg.NegativeTQ);
                            Services.BaseService.Update<PSPDEV>(pspDev);
                        }
                    }
                    else if (element.GetAttribute("xlink:href").Contains("gndline"))
                    {
                        PSPDEV pspDev = new PSPDEV();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                        frmFadejie dlg;
                        if (pspDev != null)
                        {
                            dlg = new frmFadejie(pspDev, pspDev.SvgUID);
                        }
                        else
                        {
                            pspDev = new PSPDEV();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = element.GetAttribute("id");
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = 0;
                            pspDev.Type = "gndline";
                            if (element.GetAttribute("xlink:href").Contains("dynamotorline"))
                            {
                                pspDev.Lable = "发电厂支路";
                            }
                            else if (element.GetAttribute("xlink:href").Contains("gndline"))
                            {
                                pspDev.Lable = "接地支路";
                            }
                            Services.BaseService.Create<PSPDEV>(pspDev);
                            dlg = new frmFadejie(pspDev, pspDev.SvgUID);
                        }
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (dlg.Name == null)
                            {
                                MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "gndline";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            pspDev.Name = dlg.Name;
                            XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                            if (text != null)
                            {
                                (text as Text).InnerText = dlg.Name;
                            }
                            pspDev.HuganLine1 = dlg.FirstNodeName;
                            pspDev.HuganLine3 = dlg.SwitchStatus;
                            if (dlg.OutP != "")
                                pspDev.OutP = Convert.ToDouble(dlg.OutP);
                            if (dlg.OutQ != "")
                                pspDev.OutQ = Convert.ToDouble(dlg.OutQ);
                            if (dlg.VoltR != "")
                                pspDev.VoltR = Convert.ToDouble(dlg.VoltR);
                            if (dlg.VoltV != "")
                                pspDev.VoltV = Convert.ToDouble(dlg.VoltV);
                            if (dlg.PositiveTQ != "")
                                pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                            if (dlg.NegativeTQ != "")
                                pspDev.ZeroTQ = Convert.ToDouble(dlg.NegativeTQ);
                            Services.BaseService.Update<PSPDEV>(pspDev);
                        }
                    }
                    else if (element.GetAttribute("xlink:href").Contains("loadline"))
                    {
                        PSPDEV pspDev = new PSPDEV();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                        frmLoad dlg;
                        if (pspDev != null)
                        {
                            dlg = new frmLoad(pspDev);
                        }
                        else
                        {
                            pspDev = new PSPDEV();
                            pspDev.SUID = Guid.NewGuid().ToString();
                            pspDev.EleID = element.GetAttribute("id");
                            pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            pspDev.Number = -1;
                            pspDev.FirstNode = -1;
                            pspDev.LastNode = 0;
                            pspDev.Type = "loadline";

                            pspDev.Lable = "负荷支路";

                            Services.BaseService.Create<PSPDEV>(pspDev);
                            dlg = new frmLoad(pspDev);
                        }
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (dlg.Name == null)
                            {
                                MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "loadline";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            pspDev.Name = dlg.Name;
                            XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                            if (text != null)
                            {
                                (text as Text).InnerText = dlg.Name;
                            }
                            pspDev.HuganLine1 = dlg.FirstNodeName;

                            pspDev.HuganLine3 = dlg.LoadSwitchState;
                            if (dlg.InPutP != "")
                                pspDev.InPutP = Convert.ToDouble(dlg.InPutP);
                            if (dlg.InPutQ != "")
                                pspDev.InPutQ = Convert.ToDouble(dlg.InPutQ);
                            if (dlg.VoltR != "")
                                pspDev.VoltR = Convert.ToDouble(dlg.VoltR);

                            Services.BaseService.Update<PSPDEV>(pspDev);
                        }
                    }
                    else if (element.GetAttribute("xlink:href").Contains("串联电容电抗器"))
                    {
                        PSPDEV pspDev = new PSPDEV();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                        frmCapacity dlg;


                        if (pspDev != null)
                        {
                            dlg = new frmCapacity(pspDev, pspDev.SvgUID);
                            dlg.SetEnable(true);
                        }
                        else
                        {
                            return;
                        }
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (dlg.Name == null)
                            {
                                MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "串联电容电抗器";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            pspDev.Name = dlg.Name;
                            XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                            if (text != null)
                            {
                                (text as Text).InnerText = dlg.Name;
                            }
                            pspDev.HuganLine1 = dlg.FirstNodeName;

                            // pspDev.HuganLine2 = dlg.LastNodeName;
                            if (dlg.PositiveTQ != "")
                                pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);


                            Services.BaseService.Update<PSPDEV>(pspDev);
                        }
                    }
                    else if (element.GetAttribute("xlink:href").Contains("并联电容电抗器"))
                    {
                        PSPDEV pspDev = new PSPDEV();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                        frmCapacity dlg;


                        if (pspDev != null)
                        {
                            dlg = new frmCapacity(pspDev, pspDev.SvgUID);
                            dlg.SetEnable(false);
                        }
                        else
                        {
                            return;
                        }
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (dlg.Name == null)
                            {
                                MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "并联电容电抗器";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            pspDev.Name = dlg.Name;
                            XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                            if (text != null)
                            {
                                (text as Text).InnerText = dlg.Name;
                            }
                            pspDev.HuganLine1 = dlg.FirstNodeName;
                            //pspDev.HuganLine2 = dlg.LastNodeName;
                            if (dlg.PositiveTQ != "")
                                pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);


                            Services.BaseService.Update<PSPDEV>(pspDev);
                        }
                    }
                    else if (element.GetAttribute("xlink:href").Contains("transformerthirdzu"))
                    {
                        PSPDEV pspDev = new PSPDEV();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                        frmThridTra dlg;

                        if (pspDev != null)
                        {
                            dlg = new frmThridTra(pspDev, pspDev.SvgUID);
                        }
                        else
                        {
                            return;
                        }
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (dlg.Name == null)
                            {
                                MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "transformerthirdzu";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            pspDev.Name = dlg.Name;
                            XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                            if (text != null)
                            {
                                (text as Text).InnerText = dlg.Name;
                            }

                            pspDev.HuganLine1 = dlg.IName;
                            pspDev.HuganLine2 = dlg.JName;
                            pspDev.HuganLine3 = dlg.ISwitchState;
                            pspDev.HuganLine4 = dlg.JSwitchState;
                            pspDev.LineLevel = dlg.IType;
                            pspDev.LineType = dlg.JType;
                            pspDev.LineStatus = dlg.KType;
                            pspDev.KName = dlg.KName;
                            pspDev.KSwitchStatus = dlg.KSwitchState;
                            if (dlg.IK != "")
                            {
                                pspDev.K = Convert.ToDouble(dlg.KK);
                            }
                            if (dlg.JK != "")
                            {
                                pspDev.G = Convert.ToDouble(dlg.JK);
                            }
                            if (dlg.KK != "")
                            {
                                pspDev.BigP = Convert.ToDouble(dlg.KK);
                            }
                            if (dlg.IR != "")
                            {
                                pspDev.HuganTQ1 = Convert.ToDouble(dlg.IR);
                            }
                            if (dlg.JR != "")
                            {
                                pspDev.HuganTQ2 = Convert.ToDouble(dlg.JR);
                            }
                            if (dlg.KR != "")
                            {
                                pspDev.HuganTQ3 = Convert.ToDouble(dlg.KR);
                            }
                            if (dlg.ITQ != "")
                            {
                                pspDev.HuganTQ4 = Convert.ToDouble(dlg.ITQ);
                            }
                            if (dlg.JTQ != "")
                            {
                                pspDev.HuganTQ5 = Convert.ToDouble(dlg.JTQ);
                            }
                            if (dlg.KTQ != "")
                            {
                                pspDev.SmallTQ = Convert.ToDouble(dlg.KTQ);
                            }
                            if (dlg.ZeroTQ != "")
                                pspDev.ZeroTQ = Convert.ToDouble(dlg.ZeroTQ);

                            if (dlg.NeutralNodeTQ != "")
                                pspDev.BigTQ = Convert.ToDouble(dlg.NeutralNodeTQ);
                            Services.BaseService.Update<PSPDEV>(pspDev);
                        }
                    }
                    else if (element.GetAttribute("xlink:href").Contains("transformertwozu"))
                    {
                        PSPDEV pspDev = new PSPDEV();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                        frmTwoTra dlg;

                        if (pspDev != null)
                        {
                            dlg = new frmTwoTra(pspDev, pspDev.SvgUID);
                        }
                        else
                        {
                            return;
                        }
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (dlg.Name == null)
                            {
                                MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "transformertwozu";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            pspDev.Name = dlg.Name;
                            XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                            if (text != null)
                            {
                                (text as Text).InnerText = dlg.Name;
                            }

                            pspDev.HuganLine1 = dlg.FirstName;
                            pspDev.HuganLine2 = dlg.LastName;
                            pspDev.HuganLine3 = dlg.FirstSwitchState;
                            pspDev.HuganLine4 = dlg.LastSwitchState;
                            pspDev.LineLevel = dlg.FirstType;
                            pspDev.LineType = dlg.LastType;

                            if (dlg.PositiveR != "")
                            {
                                pspDev.PositiveR = Convert.ToDouble(dlg.PositiveR);
                            }
                            if (dlg.PositiveTQ != "")
                            {
                                pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                            }

                            if (dlg.ZeroR != "")
                            {
                                pspDev.ZeroR = Convert.ToDouble(dlg.ZeroR);
                            }

                            if (dlg.ZeroTQ != "")
                            {
                                pspDev.ZeroTQ = Convert.ToDouble(dlg.ZeroTQ);
                            }

                            if (dlg.K != "")
                                pspDev.K = Convert.ToDouble(dlg.K);

                            if (dlg.NeutralNodeTQ != "")
                                pspDev.BigTQ = Convert.ToDouble(dlg.NeutralNodeTQ);
                            Services.BaseService.Update<PSPDEV>(pspDev);
                        }
                    }
                    else if (element.GetAttribute("xlink:href").Contains("1/2母联开关"))
                    {
                        PSPDEV pspDev = new PSPDEV();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                        frmMuLian dlg;

                        if (pspDev != null)
                        {
                            dlg = new frmMuLian(pspDev, pspDev.SvgUID);
                        }
                        else
                        {
                            return;
                        }
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (dlg.Name == null)
                            {
                                MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "1/2母联开关";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            pspDev.Name = dlg.Name;
                            XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                            if (text != null)
                            {
                                (text as Text).InnerText = dlg.Name;
                            }

                            pspDev.HuganLine1 = dlg.FirstNodeName;
                            pspDev.HuganLine2 = dlg.LastNodeName;
                            pspDev.HuganLine3 = dlg.SwitchStatus;



                            Services.BaseService.Update<PSPDEV>(pspDev);
                        }
                    }
                    else if (element.GetAttribute("xlink:href").Contains("2/3母联开关"))
                    {
                        PSPDEV pspDev = new PSPDEV();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);
                        frmMuLian2 dlg;

                        if (pspDev != null)
                        {
                            dlg = new frmMuLian2(pspDev, pspDev.SvgUID);
                        }
                        else
                        {
                            return;
                        }
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            if (dlg.Name == null)
                            {
                                MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PSPDEV pspName = new PSPDEV();
                            pspName.Name = dlg.Name;
                            pspName.Type = "2/3母联开关";
                            pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                            if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                            {
                                MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            pspDev.Name = dlg.Name;
                            XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + pspDev.EleID + "']");
                            if (text != null)
                            {
                                (text as Text).InnerText = dlg.Name;
                            }

                            pspDev.HuganLine1 = dlg.INodeName;
                            pspDev.HuganLine2 = dlg.JNodeName;
                            pspDev.HuganLine3 = dlg.ILineName;
                            pspDev.HuganLine4 = dlg.JLineName;
                            pspDev.KName = dlg.ILoadName;
                            pspDev.KSwitchStatus = dlg.JLoadName;
                            pspDev.LineLevel = dlg.SwitchStatus1;
                            pspDev.LineType = dlg.SwitchStatus2;
                            pspDev.LineStatus = dlg.SwitchStatus3;

                            Services.BaseService.Update<PSPDEV>(pspDev);
                        }
                    }

                }
                else if ((element is Polyline) && element.GetAttribute("flag") != "1" && fileType == true)
                {

                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);

                    frmLinenew dlg2;
                    if (pspDev != null)
                    {
                        dlg2 = new frmLinenew(pspDev);
                        dlg2.derefucelineflag = Reducelineflag;
                    }
                    else
                    {
                        pspDev = new PSPDEV();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = -1;
                        pspDev.Type = "Polyline";
                        pspDev.Lable = "支路";
                        Services.BaseService.Create<PSPDEV>(pspDev);
                        dlg2 = new frmLinenew(pspDev);
                        dlg2.derefucelineflag = Reducelineflag;
                    }
                    dlg2.TYear = tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("year");
                    dlg2.linevalue = tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("linevalue");   //获得线路投资
                    if (dlg2.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg2.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg2.Name;
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspName.Type = "Polyline";
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg2.Name;
                        pspDev.LineLength = Convert.ToDouble(dlg2.LineLength);
                        pspDev.LineR = Convert.ToDouble(dlg2.LineR);
                        pspDev.LineTQ = Convert.ToDouble(dlg2.LineTQ);
                        pspDev.LineGNDC = Convert.ToDouble(dlg2.LineGNDC);
                        pspDev.LineLevel = dlg2.LineLevel;
                        pspDev.ReferenceVolt = Convert.ToDouble(dlg2.ReferenceVolt);
                        pspDev.LineType = dlg2.LineType;
                        pspDev.LineStatus = dlg2.LineStatus;
                        WireCategory wirewire = new WireCategory();
                        wirewire.WireType = dlg2.LineType;
                        if (dlg2.linevalue != "")
                        {
                            pspDev.BigP = Convert.ToDouble(dlg2.linevalue);
                        }
                        if (dlg2.ReferenceVolt != "")
                        {
                            pspDev.ReferenceVolt = Convert.ToDouble(dlg2.ReferenceVolt);
                        }
                        WireCategory wirewire2 = new WireCategory();
                        wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKey", wirewire);
                        //if (pspDev.LineR == 0)
                        //    pspDev.LineR = Convert.ToDouble(dlg2.LineLength)*wirewire2.WireR ;
                        //if (pspDev.LineTQ == 0)
                        //    pspDev.LineTQ = Convert.ToDouble(dlg2.LineLength) * wirewire2.WireTQ;
                        //if (pspDev.LineGNDC == 0)
                        //    pspDev.LineGNDC = Convert.ToDouble(dlg2.LineLength) * wirewire2.WireGNDC;
                        if (wirewire2 != null)
                            pspDev.LineChange = (double)wirewire2.WireChange;
                        string tempp = dlg2.LineLev;
                        int tel = tempp.Length;
                        if (tempp.Contains("kV") || tempp.Contains("KV") || tempp.Contains("kv") || tempp.Contains("Kv"))
                        {
                            tempp = tempp.Substring(0, tel - 2);
                        }  
                        pspDev.VoltR = Convert.ToDouble(tempp);
                        tlVectorControl1.SVGDocument.CurrentElement.SetAttribute("year", dlg2.TYear);
                        tlVectorControl1.SVGDocument.CurrentElement.SetAttribute("linevalue", dlg2.linevalue);   //获得线路投资

                        //switch (dlg2.LineType)
                        //{
                        //    case "2*LGJ-400":
                        //        {
                        //            if (pspDev.LineR==0)
                        //            pspDev.LineR = Convert.ToDouble(dlg2.LineLength) * 0.04;
                        //            if (pspDev.LineTQ == 0)
                        //            pspDev.LineTQ = Convert.ToDouble(dlg2.LineLength) * 0.303;
                        //            if (pspDev.LineGNDC == 0)
                        //            pspDev.LineGNDC = Convert.ToDouble(dlg2.LineLength) * 17.9;
                        //            pspDev.LineChange = 1690;
                        //        } break;
                        //    case "2*LGJ-300":
                        //        {
                        //            if (pspDev.LineR == 0)
                        //            pspDev.LineR = Convert.ToDouble(dlg2.LineLength) * 0.054;
                        //            if (pspDev.LineTQ == 0)
                        //            pspDev.LineTQ = Convert.ToDouble(dlg2.LineLength) * 0.308;
                        //            if (pspDev.LineGNDC == 0)
                        //            pspDev.LineGNDC = Convert.ToDouble(dlg2.LineLength) * 17.7;
                        //            pspDev.LineChange = 1400;
                        //        } break;
                        //    case "2*LGJ-240":
                        //        {
                        //            if (pspDev.LineR == 0)
                        //            pspDev.LineR = Convert.ToDouble(dlg2.LineLength) * 0.066;
                        //            if (pspDev.LineTQ == 0)
                        //            pspDev.LineTQ = Convert.ToDouble(dlg2.LineLength) * 0.310;
                        //            if (pspDev.LineGNDC == 0)
                        //            pspDev.LineGNDC = Convert.ToDouble(dlg2.LineLength) * 17.5;
                        //            pspDev.LineChange = 1220;
                        //        } break;
                        //    case "LGJ-400":
                        //        {
                        //            if (pspDev.LineR == 0)
                        //            pspDev.LineR = Convert.ToDouble(dlg2.LineLength) * 0.08;
                        //            if (pspDev.LineTQ == 0)
                        //            pspDev.LineTQ = Convert.ToDouble(dlg2.LineLength) * 0.417;
                        //            if (pspDev.LineGNDC == 0)
                        //            pspDev.LineGNDC = Convert.ToDouble(dlg2.LineLength) * 13.2;
                        //            pspDev.LineChange = 845;
                        //        } break;


                        //}
                        Services.BaseService.Update<PSPDEV>(pspDev);
                        Topology2();
                    }
                }
                else if ((element is Polyline) && element.GetAttribute("flag") != "1" && fileType != true)
                {

                    PSPDEV pspDev = new PSPDEV();
                    pspDev.EleID = element.GetAttribute("id");
                    pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    pspDev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", pspDev);

                    frmLine dlg;
                    if (pspDev != null)
                    {
                        dlg = new frmLine(pspDev);
                    }
                    else
                    {
                        pspDev = new PSPDEV();
                        pspDev.SUID = Guid.NewGuid().ToString();
                        pspDev.EleID = element.GetAttribute("id");
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        pspDev.Number = -1;
                        pspDev.FirstNode = -1;
                        pspDev.LastNode = -1;
                        pspDev.Type = "Polyline";
                        pspDev.Lable = "支路";
                        Services.BaseService.Create<PSPDEV>(pspDev);
                        dlg = new frmLine(pspDev);
                    }

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (dlg.Name == null)
                        {
                            MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        PSPDEV pspName = new PSPDEV();
                        pspName.Name = dlg.Name;
                        pspName.Type = "Polyline";
                        pspName.SvgUID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                        IList listName = Services.BaseService.GetList("SelectPSPDEVByName", pspName);
                        if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).EleID != pspDev.EleID))
                        {
                            MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        pspDev.Name = dlg.Name;
                        pspDev.LineLength = Convert.ToDouble(dlg.LineLength);
                        pspDev.LineLevel = dlg.LineLevel;
                        pspDev.LineType = dlg.LineType;
                        pspDev.LineStatus = dlg.LineStatus;
                        pspDev.PositiveR = Convert.ToDouble(dlg.PositiveR);
                        pspDev.PositiveTQ = Convert.ToDouble(dlg.PositiveTQ);
                        pspDev.ZeroR = Convert.ToDouble(dlg.ZeroR);
                        pspDev.ZeroTQ = Convert.ToDouble(dlg.ZeroTQ);
                        if (dlg.HuganFirst == "是")
                            pspDev.HuganFirst = 1;
                        else
                            pspDev.HuganFirst = 0;
                        pspDev.HuganLine1 = dlg.HuganLine1;
                        pspDev.HuganLine2 = dlg.HuganLine2;
                        pspDev.HuganLine3 = dlg.HuganLine3;
                        pspDev.HuganLine4 = dlg.HuganLine4;
                        pspDev.HuganTQ1 = Convert.ToDouble(dlg.HuganTQ1);
                        pspDev.HuganTQ2 = Convert.ToDouble(dlg.HuganTQ2);
                        pspDev.HuganTQ3 = Convert.ToDouble(dlg.HuganTQ3);
                        pspDev.HuganTQ4 = Convert.ToDouble(dlg.HuganTQ4);
                        pspDev.HuganTQ5 = Convert.ToDouble(dlg.HuganTQ5);
                        string tempp = dlg.LineLev;
                        int tel = tempp.Length;
                        if (tel == 1)
                            pspDev.VoltR = 0;
                        else
                        {
                            if (tempp.Contains("kV") || tempp.Contains("KV") || tempp.Contains("kv") || tempp.Contains("Kv"))
                            {
                                tempp = tempp.Substring(0, tel - 2);
                            }  
                            pspDev.VoltR = Convert.ToDouble(tempp);
                        }
                        //switch (dlg.LineType)
                        //{
                        //    case "2*LGJ-400":
                        //        {
                        //            pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.04;
                        //            pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.303;
                        //            pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.9;
                        //            pspDev.LineChange = 1690;
                        //        } break;
                        //    case "2*LGJ-300":
                        //        {
                        //            pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.054;
                        //            pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.308;
                        //            pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.7;
                        //            pspDev.LineChange = 1400;
                        //        } break;
                        //    case "2*LGJ-240":
                        //        {
                        //            pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.066;
                        //            pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.310;
                        //            pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 17.5;
                        //            pspDev.LineChange = 1220;
                        //        } break;
                        //    case "LGJ-400":
                        //        {
                        //            pspDev.LineR = Convert.ToDouble(dlg.LineLength) * 0.08;
                        //            pspDev.LineTQ = Convert.ToDouble(dlg.LineLength) * 0.417;
                        //            pspDev.LineGNDC = Convert.ToDouble(dlg.LineLength) * 13.2;
                        //            pspDev.LineChange = 845;
                        //        } break;


                        //}
                        Services.BaseService.Update<PSPDEV>(pspDev);
                        Topology2();
                    }
                }
            }
            tlVectorControl1.Operation = ToolOperation.Select;
            //contextMenuStrip1.Hide();
        }

        public List<LineInfo> LoadData()
        {
            //在处理时各个图层的待选就是待建属性，待选和等待属性就是没处理前的待选


            List<LineInfo> clist = new List<LineInfo>();
            int c1 = 0;
            int c2 = 0;
            int c3 = 0;
            int d1 = 0;
            int d2 = 0;
            int d3 = 0;
            int e1 = 0;
            int e2 = 0;
            int e3 = 0;
            int f1 = 0;
            int f2 = 0;
            int f3 = 0;
            PSPDEV zhengtipsp = new PSPDEV();
            zhengtipsp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
            zhengtipsp.Type = "Polyline";
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", zhengtipsp);
            for (int i = 0; i < list1.Count; i++)
            {
                PSPDEV psp = list1[i] as PSPDEV;
                if (psp.LineStatus == "运行")
                {
                    f1++;
                }
                if (psp.LineStatus == "待选")
                {
                    f2++;
                }
            }
            PSPDEV jiqipsp = new PSPDEV();
            jiqipsp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "1";
            jiqipsp.Type = "Polyline";
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", jiqipsp);
            for (int i = 0; i < list2.Count; i++)
            {
                PSPDEV psp = list2[i] as PSPDEV;
                if (psp.LineStatus == "等待" || psp.LineStatus == "待选")
                {
                    c2++;
                }
                if (psp.LineStatus == "待选")
                {
                    c3++;
                }
            }
            PSPDEV zhongqipsp = new PSPDEV();
            zhongqipsp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "2";
            zhongqipsp.Type = "Polyline";
            IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", zhongqipsp);
            for (int i = 0; i < list3.Count; i++)
            {
                PSPDEV psp = list3[i] as PSPDEV;
                if (psp.LineStatus == "等待" || psp.LineStatus == "待选")
                {
                    d2++;
                }
                if (psp.LineStatus == "待选")
                {
                    d3++;
                }
            }
            PSPDEV yuanqipsp = new PSPDEV();
            yuanqipsp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "3";
            yuanqipsp.Type = "Polyline";
            IList list4 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", yuanqipsp);
            for (int i = 0; i < list4.Count; i++)
            {
                PSPDEV psp = list4[i] as PSPDEV;
                if (psp.LineStatus == "等待" || psp.LineStatus == "待选")
                {
                    e2++;
                }
                if (psp.LineStatus == "待选")
                {
                    e3++;
                }
            }
            //Layer lar1 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/defs/*[@id='" + tlVectorControl1.SVGDocument.SvgdataUid + "1" + "']") as Layer;
            //Layer lar2 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/defs/*[@id='" + tlVectorControl1.SVGDocument.SvgdataUid + "2" + "']") as Layer;
            //Layer lar3 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/defs/*[@id='" + tlVectorControl1.SVGDocument.SvgdataUid + "3" + "']") as Layer;
            //Layer lar4 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/defs/*[@id='" + tlVectorControl1.SVGDocument.SvgdataUid + "3" + "']") as Layer;

            //PSPDEV p = new PSPDEV();
            //p.SvgUID = "'" + lar1.ID + "','" + lar2.ID + "','" + lar3.ID + "','" + lar4.ID + "'";
            //p.LineStatus = "运行";
            //IList<PSPDEV> list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVBySvgUIDandLineStatusList", p);
            //p.SvgUID = "'" + lar1.ID + "','" + lar2.ID + "','" + lar3.ID + "','" + lar4.ID + "'";
            //p.LineStatus = "待选";
            //IList<PSPDEV> list2 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVBySvgUIDandLineStatusList", p);
            //p.SvgUID = "'" + lar1.ID + "','" + lar2.ID + "','" + lar3.ID + "','" + lar4.ID + "'";
            //p.LineStatus = "等待";
            //IList<PSPDEV> list3 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVBySvgUIDandLineStatusList", p);

            //for (int i = 0; i < lar4.GraphList.Count; i++)
            //{
            //    ISvgElement ele1 = lar1.GraphList[i];
            //    for (int j = 0; j < list1.Count; j++)
            //    {
            //        if (ele1.ID == list1[j].EleID)
            //        {
            //            f1 = f1 + 1;
            //        }
            //    }
            //    for (int j = 0; j < list2.Count; j++)
            //    {
            //        if (ele1.ID == list2[j].EleID)
            //        {
            //            f2 = f2 + 1;
            //        }
            //    }
            //    for (int j = 0; j < list3.Count; j++)
            //    {
            //        if (ele1.ID == list3[j].EleID)
            //        {
            //            f3 = f3 + 1;
            //        }
            //    }
            //}

            //for (int i = 0; i < lar1.GraphList.Count;i++ )
            //{
            //    ISvgElement ele1 = lar1.GraphList[i];
            //    for (int j = 0; j < list1.Count; j++)
            //    {
            //        if (ele1.ID == list1[j].EleID)
            //        {
            //            c1 = c1 + 1;
            //        }
            //    }
            //    for (int j = 0; j < list2.Count; j++)
            //    {
            //        if (ele1.ID == list2[j].EleID)
            //        {
            //            c2 = c2 + 1;
            //        }
            //    }
            //    for (int j = 0; j < list3.Count; j++)
            //    {
            //        if (ele1.ID == list3[j].EleID)
            //        {
            //            c3 = c3 + 1;
            //        }
            //    }
            //}
            //for (int i = 0; i < lar2.GraphList.Count; i++)
            //{
            //    ISvgElement ele1 = lar2.GraphList[i];
            //    for (int j = 0; j < list1.Count; j++)
            //    {
            //        if (ele1.ID == list1[j].EleID)
            //        {
            //            d1 = d1 + 1;
            //        }
            //    }
            //    for (int j = 0; j < list2.Count; j++)
            //    {
            //        if (ele1.ID == list2[j].EleID)
            //        {
            //            d2 = d2 + 1;
            //        }
            //    }
            //    for (int j = 0; j < list3.Count; j++)
            //    {
            //        if (ele1.ID == list3[j].EleID)
            //        {
            //            d3 = d3 + 1;
            //        }
            //    }
            //}
            //for (int i = 0; i < lar3.GraphList.Count; i++)
            //{
            //    ISvgElement ele1 = lar3.GraphList[i];
            //    for (int j = 0; j < list1.Count; j++)
            //    {
            //        if (ele1.ID == list1[j].EleID)
            //        {
            //            e1 = e1 + 1;
            //        }
            //    }
            //    for (int j = 0; j < list2.Count; j++)
            //    {
            //        if (ele1.ID == list2[j].EleID)
            //        {
            //            e2 = e2 + 1;
            //        }
            //    }
            //    for (int j = 0; j < list3.Count; j++)
            //    {
            //        if (ele1.ID == list3[j].EleID)
            //        {
            //            e3 = e3 + 1;
            //        }
            //    }
            //}
            LineInfo l4 = new LineInfo();
            l4.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "4";
            l4.ObligateField1 = "整体网架规划";
            l4.ObligateField2 = f1.ToString();
            l4.ObligateField3 = f2.ToString();
            //l1.ObligateField4 = c3.ToString();
            LineInfo l1 = new LineInfo();
            l1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "1";
            l1.ObligateField1 = "近期网架规划";
            l1.ObligateField2 = f1.ToString();
            l1.ObligateField3 = c2.ToString();
            l1.ObligateField4 = c3.ToString();
            LineInfo l2 = new LineInfo();
            l2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "2";
            l2.ObligateField1 = "中期网架规划";
            l2.ObligateField2 = f1.ToString();
            l2.ObligateField3 = d2.ToString();
            l2.ObligateField4 = d3.ToString();
            LineInfo l3 = new LineInfo();
            l3.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid + "3";
            l3.ObligateField1 = "远期网架规划";
            l3.ObligateField2 = f1.ToString();
            l3.ObligateField3 = e3.ToString();
            l3.ObligateField4 = e3.ToString();
            clist.Add(l4);
            clist.Add(l1);
            clist.Add(l2);
            clist.Add(l3);
            return clist;
        }

        private void FirstStart()
        {
            string strData = (1 + " " + 1 + " " + 0 + " " + 0 + " " + 234 + " " + 76.912 + " " + 230 + " " + 360 + ";" + "\r\n");
            strData += (2 + " " + 0 + " " + 0 + " " + 0 + " " + 0 + " " + 0 + " " + 230 + " " + 360 + ";" + "\r\n");
            strData += (1 + " " + 1 + " " + 2 + " " + 0.432 + " " + 2.464 + " " + 141.6 + " " + 1400 + " " + -1 + ";" + "\r\n");
            strData += (230 + " " + 100 + " " + -2 + " " + -2 + " " + -2 + " " + -2 + " " + -2 + " " + -2 + ";" + "\r\n");
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\data.txt"))
            {
                File.Delete(System.Windows.Forms.Application.StartupPath + "\\data.txt");
            }
            //if (File.Exists("c:\\L9.txt"))
            //{
            //    File.Delete("c:\\L9.txt");
            //}
            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\data.txt"), FileMode.OpenOrCreate);
            StreamWriter str1 = new StreamWriter(VK);
            str1.Write(strData);
            str1.Close();

            try
            {
                Process ps = Process.Start("ChaoLiu.exe", "0");
            }
            catch (System.Exception e)
            {

            }
        }
        private void frmTLpsp_Load(object sender, EventArgs e)
        {
            this.alignButton = this.dotNetBarManager1.GetItem("mAlign") as ButtonItem;
            this.orderButton = this.dotNetBarManager1.GetItem("mOrder") as ButtonItem;
            this.rotateButton = this.dotNetBarManager1.GetItem("mRotate") as ButtonItem;



        }
    }
    //public class linedaixuan : IComparable
    //{
    //    private int _linenum;
    //    private string _suid;
    //    private string _linename;
    //    private double _linepij;
    //    private double _linevalue;
    //    private double _linetouzilv;
    //    public int linenum
    //    {
    //        get { return _linenum; }
    //        set { _linenum = value; }
    //    }
    //    public string Suid
    //    {
    //        get { return _suid; }
    //        set { _suid = value; }
    //    }
    //    public string linename
    //    {
    //        get { return _linename; }
    //        set { _linename = value; }
    //    }
    //    public double linepij
    //    {
    //        get { return _linepij; }
    //        set { _linepij = value; }
    //    }

    //    public double linevalue
    //    {
    //        get { return _linevalue; }
    //        set { _linevalue = value; }
    //    }
    //    public double linetouzilv
    //    {
    //        get { return _linetouzilv; }
    //        set { _linetouzilv = value; }
    //    }
    //    public linedaixuan(int _linenum, string suid, string _linename)
    //    {
    //        this.linenum = _linenum;
    //        Suid = suid;
    //        this.linename = _linename;
    //    }
    //    public int CompareTo(object obj)
    //    {
    //        int res = 0;
    //        try
    //        {
    //            linedaixuan sObj = (linedaixuan)obj;
    //            if (this.linetouzilv > sObj.linetouzilv)
    //            {
    //                res = 1;
    //            }
    //            else if (this.linetouzilv < sObj.linetouzilv)
    //            {
    //                res = -1;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("比较异常", ex.InnerException);
    //        }
    //        return res;

    //    }
    //}
}