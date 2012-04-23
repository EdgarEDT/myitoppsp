using System;
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
using Microsoft.Office.Interop;
using Itop.TLPSP.DEVICE;

namespace Itop.TLPsp.Graphical
{
    public delegate void OnCloseDocumenthandler(object sender, string svgUid, string pid);
    public enum CustomOperation
    {
        OP_Default = 0,
        OP_MeasureGT,
        OP_MeasureDistance,
        OP_AreaEdit,
        OP_AreaCount
    }
    enum MouseEventFlag : uint
    {
        Move = 0x0001,
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        RightDown = 0x0008,
        RightUp = 0x0010,
        MiddleDown = 0x0020,
        MiddleUp = 0x0040,
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,
        VirtualDesk = 0x4000,
        Absolute = 0x8000
    }
    public partial class frmTLpspGraphical : FormBase
    {
        #region 对象声明
        SVGFILE svg = new SVGFILE();
        SvgDocument sdoc = new SvgDocument();
        glebeProperty gPro = new glebeProperty();
        bool JXTFlat = false;
        DevComponents.DotNetBar.ToolTip tip;
        

        private ItopVector.Selector.SymbolSelector symbolSelector;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private DevComponents.DotNetBar.ComboBoxItem scaleBox;
        public Itop.MapView.IMapViewObj mapview;
        private int jxb = 0;
        //private ButtonItem operationButton;
        private ButtonItem orderButton;
        private ButtonItem alignButton;
        private ButtonItem rotateButton;
        private bool fileType = true;
        private bool Relaflag = false;      //是可靠性分析的开始

        private bool fk = true;
        private int bangbang = 0;
        
       // private duluqiflag=false;

        double TLPSPVmin = 0.95, TLPSPVmax = 1.05;
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

        frmInfo fInfo = new frmInfo();

        XmlNode img = null;


        public event OnCloseDocumenthandler OnCloseSvgDocument;
        
        #endregion
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        public frmTLpspGraphical()
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

            this.dotNetBarManager1.Images = ItopVector.Resource.ResourceHelper.LoadBitmapStrip(base.GetType(), "Itop.TLPsp.Graphical.ToolbarImages1.bmp", new Size(16, 16), new System.Drawing.Point(0, 0));
            //mapview = new Itop.MapView.MapViewObj(); 

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
        }

        void DrawArea_OnMouseMove(object sender, MouseEventArgs e)
        {         
            
        }
     
        void tlVectorControl1_DocumentChanged(object sender, DocumentChangedEventArgs e)
        {
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
            switch (jxt)
            {
                case 1:
                    dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = true;
                    dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCalList").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("DFS").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("DFSResult").Visible = false;              
	                break;
                case 2:
                    dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCalList").Visible = true;
                    dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("DFS").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("DFSResult").Visible = false;                    
                    break;
                case 3:
                    dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCalList").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = true;
                    dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("DFS").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("DFSResult").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("clearResult").Visible = false;
                    break;
                case 4:
                    dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCalList").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = true;
                    dotNetBarManager1.Bars["bar2"].GetItem("DFS").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("DFSResult").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("clearResult").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("clearResult").Visible = false;
                    break;
                case 5:
                    dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCalList").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("DFS").Visible = true;
                    dotNetBarManager1.Bars["bar2"].GetItem("DFSResult").Visible = true; 
                    break;
                default:
                    dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = true;
                    dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCalList").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = false;
                    dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = false;
                    break;

            }

                        
        }
        public void jxtbar(int jxt)
        {
             if (jxt == 1)
            {
                bool flag = true;
                if (Relaflag)
                    flag = false;
                dotNetBarManager1.Bars["bar2"].GetItem("Rela").Visible = Relaflag;
               // dotNetBarManager1.Bars["bar2"].GetItem("PowerLoss").Visible = flag;
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCal").Visible = flag;
                dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = flag;
                dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = flag;
                dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCalList").Visible = flag;
                dotNetBarManager1.Bars["bar2"].GetItem("clearResult").Visible = flag;
                dotNetBarManager1.Bars["bar2"].GetItem("DFS").Visible = flag;
                dotNetBarManager1.Bars["bar2"].GetItem("DFSResult").Visible = flag;

            }       
        }
        private void setTJhide() {
            SvgElementCollection sc = (tlVectorControl1.SVGDocument.RootElement as SVG).ChildList;
            foreach (SvgElement se in sc) {
                try {
                    (se as IGraph).DrawVisible = se.GetAttribute("print") != "no";
                } catch { }
            }
        }
        private void setTJshow() {
            SvgElementCollection sc = (tlVectorControl1.SVGDocument.RootElement as SVG).ChildList;
            foreach (SvgElement se in sc) {
                try {
                    (se as IGraph).DrawVisible = true;
                } catch { }
            }
        }
        /// <summary>
        /// 导出区域图片
        /// </summary>
        private void ExportImage()
        {
            GraphPath rt1 = tlVectorControl1.SVGDocument.CurrentElement as GraphPath;
            
            if (rt1 == null) return;
            RectangleF rtf1 = rt1.GetBounds();
            
            int width = (int)Math.Round(rtf1.Width * tlVectorControl1.ScaleRatio, 0)+1;
            int height = (int)Math.Round(rtf1.Height * tlVectorControl1.ScaleRatio, 0)+1;
            System.Drawing.Image image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            Color color = ColorTranslator.FromHtml("#EBEAE8");                
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

            XmlNode listText = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.ID + "']");
            (listText as Text).SetAttribute("x", (Convert.ToDouble((listText as Text).GetAttribute("x")) + afterMove.X - beforeMove.X).ToString());
            (listText as Text).SetAttribute("y", (Convert.ToDouble((listText as Text).GetAttribute("y")) + afterMove.Y - beforeMove.Y).ToString());
        }
      
        void DrawArea_OnMouseDown(object sender, MouseEventArgs e)
        {            
          
        }           

        void tlVectorControl1_DoubleLeftClick(object sender, SvgElementSelectedEventArgs e)
        {
            UseProperty();
        }
        protected void UseProperty()
        {
            SvgElement element = tlVectorControl1.SVGDocument.CurrentElement;
            if (element is Use)
            {
                if (element.GetAttribute("xlink:href").Contains("Substation") )
                {
                    DeviceHelper.ShowDeviceDlg(DeviceType.BDZ, element.GetAttribute("Deviceid"));
                }
                else if (element.GetAttribute("xlink:href").Contains("Power"))
                {
                    DeviceHelper.ShowDeviceDlg(DeviceType.DY, element.GetAttribute("Deviceid"));
                }
            }
                    
        }

        void tlVectorControl1_RightClick(object sender, SvgElementSelectedEventArgs e)
        {           
            if (tlVectorControl1.SVGDocument.CurrentElement is Use)
            {
                contextMenuStrip1.Show();
            }
            else if (tlVectorControl1.SVGDocument.CurrentElement is Polyline)
            {
                contextMenuStrip1.Hide();
                tlVectorControl1.Operation = ToolOperation.Select; 
            }
            else if (tlVectorControl1.SVGDocument.CurrentElement is RectangleElement)
            {
                printToolStripMenuItem.Visible = true;
            }

        }

        void tlVectorControl1_LeftClick(object sender, SvgElementSelectedEventArgs e)
        {          

        }        
        void tlVectorControl1_AddElement(object sender, AddSvgElementEventArgs e)
        {
            XmlElement temp = e.SvgElement as XmlElement;
            intdata(tlVectorControl1.SVGDocument.SvgdataUid);
  
             if (temp is Use && (temp.GetAttribute("xlink:href").Contains("Substation")))
             {
                 //frmSubstation dlgSubstation = new frmSubstation();
                 //dlgSubstation.ProjectID = this.ProjectUID;
                 //dlgSubstation.InitData();
                 XmlNodeList listUSE = tlVectorControl1.SVGDocument.GetElementsByTagName("use");
                 IList<object> listID = new List<object>();
                 foreach (XmlNode node in listUSE)
                 {
                    string str = ((XmlElement)node).GetAttribute("Deviceid");     
                    PSP_Substation_Info obj = DeviceHelper.GetDevice<PSP_Substation_Info>(str);
                     if(obj!=null)
                     {
                         listID.Add((object)obj);
                     }         
                 }
                 DeviceHelper.pspflag = true;
                 object subID = DeviceHelper.SelectDevice("20",tlVectorControl1.SVGDocument.SvgdataUid,listID);
                 
                 XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                 if (subID!=null)
                 {
                     
                     RectangleF t = ((IGraph)temp).GetBounds();
                     n1.SetAttribute("x", (t.X +t.Width+10).ToString());
                     n1.SetAttribute("y", (t.Y +t.Height/2).ToString());
                     n1.InnerText = ((PSP_Substation_Info)subID).Title;
                     n1.SetAttribute("layer", SvgDocument.currentLayer);
                     n1.SetAttribute("Deviceid", ((PSP_Substation_Info)subID).UID);
                     temp.SetAttribute("Deviceid", ((PSP_Substation_Info)subID).UID);
                     n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                     tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                     tlVectorControl1.Operation = ToolOperation.Select;
                     //PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                     //elcDEV.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                     //elcDEV.DeviceSUID = ((PSP_Substation_Info)subID).UID;
                     //Services.BaseService.Create<PSP_ElcDevice>(elcDEV);
                     //AddLine(temp, elcDEV.DeviceSUID);
                 } 
                 else
                 {
                     tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                     tlVectorControl1.Delete();
                     return;
                 }
                 bool projectexitflag = false;               //检验此变电站是否在项目中
                 foreach (eleclass ele in Subcol)
                 {
                     if (ele.suid == ((PSP_Substation_Info)subID).UID && ele.selectflag == true)
                     {
                         projectexitflag = true;
                         MessageBox.Show("已经选择此变电站，请重新选择其他的变电站！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                         tlVectorControl1.Delete();
                         tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                         tlVectorControl1.Delete();
                     }
                     else if (ele.suid == ((PSP_Substation_Info)subID).UID && ele.selectflag == false)
                     {
                         projectexitflag = true;
                         Subcol.Remove(ele);
                         eleclass ele1 = new eleclass(((PSP_Substation_Info)subID).Title, ((PSP_Substation_Info)subID).UID, "20", true);
                         Subcol.Add(ele1);
                         PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                         elcDEV.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                         elcDEV.DeviceSUID = ((PSP_Substation_Info)subID).UID;
                         Services.BaseService.Create<PSP_ElcDevice>(elcDEV);
                         AddLine(temp, elcDEV.DeviceSUID);
                     }

                 }
                 if (!projectexitflag)
                 {
                     MessageBox.Show("你所选择的项目中不包括此变电站！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                     tlVectorControl1.Delete();
                     tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                     tlVectorControl1.Delete();
                 }
             }
             else if (temp is Use && (temp.GetAttribute("xlink:href").Contains("Power")))
             {
                 XmlNodeList listUSE = tlVectorControl1.SVGDocument.GetElementsByTagName("use");
                 IList<object> listID = new List<object>();
                 foreach (XmlNode node in listUSE)
                 {
                     string str = ((XmlElement)node).GetAttribute("Deviceid");
                     PSP_PowerSubstation_Info obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(str);
                     if (obj != null)
                     {
                         listID.Add((object)obj);
                     }
                 }
                 DeviceHelper.pspflag = true;
                 object subID = DeviceHelper.SelectDevice("30",tlVectorControl1.SVGDocument.SvgdataUid,listID);
                 XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                 if (subID != null)
                 {
                     
                     RectangleF t = ((IGraph)temp).GetBounds();
                     n1.SetAttribute("x", (t.X + t.Width + 10).ToString());
                     n1.SetAttribute("y", (t.Y + t.Height / 2).ToString());
                     n1.InnerText = ((PSP_PowerSubstation_Info)subID).Title;
                     n1.SetAttribute("layer", SvgDocument.currentLayer);
                     n1.SetAttribute("Deviceid", ((PSP_PowerSubstation_Info)subID).UID);
                     temp.SetAttribute("Deviceid", ((PSP_PowerSubstation_Info)subID).UID);
                     n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                     tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                     tlVectorControl1.Operation = ToolOperation.Select;
                     //PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                     //elcDEV.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                     //elcDEV.DeviceSUID = ((PSP_PowerSubstation_Info)subID).UID;
                     //Services.BaseService.Create<PSP_ElcDevice>(elcDEV);
                     //AddLine(temp, elcDEV.DeviceSUID);
                 }
                 else
                 {
                     tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                     tlVectorControl1.Delete();
                     return;
                 }
                 bool projectexitflag = false;               //检验此变电站是否在项目中
                 foreach (eleclass ele in Powcol)
                 {
                     if (ele.suid == ((PSP_PowerSubstation_Info)subID).UID && ele.selectflag == true)
                     {
                         projectexitflag = true;
                         MessageBox.Show("已经选择此发电厂，请重新选择其他的发电厂！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                         tlVectorControl1.Delete();
                         tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                         tlVectorControl1.Delete();
                     }
                     else if (ele.suid == ((PSP_PowerSubstation_Info)subID).UID && ele.selectflag == false)
                     {
                         projectexitflag = true;
                         Subcol.Remove(ele);
                         eleclass ele1 = new eleclass(((PSP_PowerSubstation_Info)subID).Title, ((PSP_PowerSubstation_Info)subID).UID, "30", true);
                         Subcol.Add(ele1);
                         PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                         elcDEV.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                         elcDEV.DeviceSUID = ((PSP_PowerSubstation_Info)subID).UID;
                         Services.BaseService.Create<PSP_ElcDevice>(elcDEV);
                         AddLine(temp, elcDEV.DeviceSUID);
                     }

                 }
                 if (!projectexitflag)
                 {
                     MessageBox.Show("你所选择的项目中不包括此发电厂！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                     tlVectorControl1.Delete();
                     tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                     tlVectorControl1.Delete();
                 }
             }
        }
        protected void Ref()
        {
            string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND Type = '05'";
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
            foreach (PSPDEV ple in list)
            {
                XmlNode nodeLine = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@Deviceid='"+ple.SUID+"']");
                if (nodeLine==null)
                {
                    string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Number = '" + ple.FirstNode + "' AND Type = '01'";
                    IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon1);
                    strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Number = '" + ple.LastNode + "' AND Type = '01'";
                    IList list2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon1);
                    if (list1.Count>0&&list2.Count>0)
                    {
                        string strCon2 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Type = '05' AND FirstNode = '" + ple.FirstNode + "' AND LastNode = '" + ple.LastNode + "'";
                        IList list3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);

                        string strCon3 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Type = '05' AND FirstNode = '" + ple.LastNode + "' AND LastNode = '" + ple.FirstNode + "'";
                        IList list4 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                        int i = 0;
                        foreach (PSPDEV line in list3)
                        {
                            XmlNode nodeL = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@Deviceid='" + line.SUID + "']");
                            if(nodeL!=null)
                            {
                                i++;
                            }
                           
                        }
                        foreach (PSPDEV line in list4)
                        {
                            XmlNode nodeL = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@Deviceid='" + line.SUID + "']");
                            if (nodeL != null)
                            {
                                i++;
                            }
                        }
                        XmlNode nodeFirst = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@Deviceid='" + ((PSPDEV)list1[0]).SvgUID + "']");
                        XmlNode nodeLast = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@Deviceid='" + ((PSPDEV)list2[0]).SvgUID + "']");
                        if (nodeFirst!=null&&nodeLast!=null)
                        {

                        } 
                        else
                        {
                            return;
                        }
                        PointF[] t2 = new PointF[] { ((IGraph)nodeFirst).CenterPoint, ((IGraph)nodeLast).CenterPoint };
                        float angel = 0f;
                        angel = (float)(180 * Math.Atan2((t2[1].Y - t2[0].Y), (t2[1].X - t2[0].X)) / Math.PI);
                        PointF pStart1 = new PointF(((IGraph)nodeFirst).CenterPoint.X + (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)nodeFirst).CenterPoint.Y - (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Cos((angel) * Math.PI / 180)));
                        PointF pStart2 = new PointF(((IGraph)nodeFirst).CenterPoint.X - (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)nodeFirst).CenterPoint.Y + (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Cos((angel) * Math.PI / 180)));

                        PointF pStart3 = new PointF(((IGraph)nodeLast).CenterPoint.X + (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)nodeLast).CenterPoint.Y - (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Cos((angel) * Math.PI / 180)));
                        PointF pStart4 = new PointF(((IGraph)nodeLast).CenterPoint.X - (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)nodeLast).CenterPoint.Y + (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Cos((angel) * Math.PI / 180)));

                        string temp = "";
                        if (i == 0)
                        {
                            temp = ((IGraph)nodeFirst).CenterPoint.X.ToString() + " " + ((IGraph)nodeFirst).CenterPoint.Y.ToString() + "," + ((IGraph)nodeLast).CenterPoint.X + " " + ((IGraph)nodeLast).CenterPoint.Y.ToString();
                        }
                        else if (OddEven.IsOdd(i))
                        {
                            temp = pStart1.X.ToString() + " " + pStart1.Y.ToString() + "," + pStart3.X.ToString() + " " + pStart3.Y.ToString();
                        }
                        else if (OddEven.IsEven(i))
                        {
                            temp = pStart2.X.ToString() + " " + pStart2.Y.ToString() + "," + pStart4.X.ToString() + " " + pStart4.Y.ToString();
                        }
                        XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;

                        n1.SetAttribute("points", temp);
                        n1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                        tlVectorControl1.SVGDocument.CurrentLayer = ((tlVectorControl1.SVGDocument.getLayerList()[0]) as ItopVector.Core.Figure.Layer);
                        n1.SetAttribute("layer", SvgDocument.currentLayer);
                        n1.SetAttribute("FirstNode", (nodeFirst as XmlElement).GetAttribute("id"));
                        n1.SetAttribute("LastNode", (nodeLast as XmlElement).GetAttribute("id"));
                        n1.SetAttribute("Deviceid", ple.SUID);
                        tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;                        
                        tlVectorControl1.ChangeLevel(LevelType.Bottom);                        
                        //n1.RemoveAttribute("layer");
                        tlVectorControl1.Operation = ToolOperation.Select;                 
                    }                   
                }

            }
        }
        protected void AddLine(XmlElement device,string devicSUID)
        {
            string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SvgUID = '" + devicSUID +"' AND Type = '01'";   
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
            XmlNodeList list2 = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
            foreach (PSPDEV dev in list)
            {
                foreach (XmlNode node in list2)
                {                    
                    XmlElement element = node as XmlElement;
                    //XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                    string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01'";
                    IList list3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon1);
                    foreach (PSPDEV pd in list3)
                    {
                        if (dev.Number!=pd.Number)
                        {
                            string strCon2 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Type = '05' AND FirstNode = '" + dev.Number + "' AND LastNode = '" + pd.Number + "'";
                            IList list4 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);

                            string strCon3 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Type = '05' AND FirstNode = '" + pd.Number + "' AND LastNode = '" + dev.Number + "'";
                            IList list5 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);

                            for (int i = 0; i < list4.Count;i++ )
                            {
                                PointF[] t2 = new PointF[] { ((IGraph)device).CenterPoint, ((IGraph)element).CenterPoint };
                                float angel = 0f;
                                angel = (float)(180 * Math.Atan2((t2[1].Y - t2[0].Y), (t2[1].X - t2[0].X)) / Math.PI);
                                PointF pStart1 = new PointF(((IGraph)device).CenterPoint.X + (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)device).CenterPoint.Y - (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Cos((angel) * Math.PI / 180)));
                                PointF pStart2 = new PointF(((IGraph)device).CenterPoint.X - (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)device).CenterPoint.Y + (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Cos((angel) * Math.PI / 180)));

                                PointF pStart3 = new PointF(((IGraph)element).CenterPoint.X + (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)element).CenterPoint.Y - (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Cos((angel) * Math.PI / 180)));
                                PointF pStart4 = new PointF(((IGraph)element).CenterPoint.X - (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)element).CenterPoint.Y + (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Cos((angel) * Math.PI / 180)));

                                string temp = "";
                                if (i==0)
                                {                                   
                                    temp = ((IGraph)device).CenterPoint.X.ToString() + " " + ((IGraph)device).CenterPoint.Y.ToString() + "," + ((IGraph)element).CenterPoint.X + " " + ((IGraph)element).CenterPoint.Y.ToString();
                                }
                                else if (OddEven.IsOdd(i))
                                {
                                    temp = pStart1.X.ToString() + " " + pStart1.Y.ToString() +"," + pStart3.X.ToString() + " " + pStart3.Y.ToString();         
                                }
                                else if (OddEven.IsEven(i))
                                {
                                    temp = pStart2.X.ToString() + " " + pStart2.Y.ToString() + "," + pStart4.X.ToString() + " " + pStart4.Y.ToString();                                        
                                }
                                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                                
                                n1.SetAttribute("points",temp);
                                n1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                n1.SetAttribute("layer", SvgDocument.currentLayer);    
                                n1.SetAttribute("FirstNode",device.GetAttribute("id"));
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
                            if (list4 != null)
                            {
                                j = list4.Count;
                            }
                            for (int i = j; i < j+ list5.Count; i++)
                            {
                                PointF[] t2 = new PointF[] { ((IGraph)element).CenterPoint, ((IGraph)device).CenterPoint };
                                float angel = 0f;
                                angel = (float)(180 * Math.Atan2((t2[1].Y - t2[0].Y), (t2[1].X - t2[0].X)) / Math.PI);
                                PointF pStart1 = new PointF(((IGraph)element).CenterPoint.X + (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)element).CenterPoint.Y - (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Cos((angel) * Math.PI / 180)));
                                PointF pStart2 = new PointF(((IGraph)element).CenterPoint.X - (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)element).CenterPoint.Y + (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Cos((angel) * Math.PI / 180)));

                                PointF pStart3 = new PointF(((IGraph)device).CenterPoint.X + (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)device).CenterPoint.Y - (float)(tlVectorControl1.ScaleRatio * 10 * ((i + 1) / 2) * Math.Cos((angel) * Math.PI / 180)));
                                PointF pStart4 = new PointF(((IGraph)device).CenterPoint.X - (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Sin((angel) * Math.PI / 180)), ((IGraph)device).CenterPoint.Y + (float)(tlVectorControl1.ScaleRatio * 10 * (i / 2) * Math.Cos((angel) * Math.PI / 180)));
                                string temp = "";
                                if (i == 0)
                                {
                                    temp = ((IGraph)element).CenterPoint.X.ToString() + " " + ((IGraph)element).CenterPoint.Y.ToString() + "," + ((IGraph)device).CenterPoint.X + " " + ((IGraph)device).CenterPoint.Y.ToString();
                                }
                                else if (OddEven.IsOdd(i))
                                {
                                    temp = pStart1.X.ToString() + " " + pStart1.Y.ToString() + "," + pStart3.X.ToString() + " " + pStart3.Y.ToString();
                                }
                                else if (OddEven.IsEven(i))
                                {
                                    temp = pStart2.X.ToString() + " " + pStart2.Y.ToString() + "," + pStart4.X.ToString() + " " + pStart4.Y.ToString();
                                }
                                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                                n1.SetAttribute("points", temp);
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
        public double getVolt(double avolt)
        {
            if (avolt == 525)
            {
                return 500;
            }
            else if (avolt == 230)
            {
                return 220;
            }
            else if (avolt == 115)
            {
                return 110;
            }
            else if (avolt == 69)
            {
                return 66;
            }
            else if (avolt == 37)
            {
                return 35;
            }
            else if (avolt == 10.5)
            {
                return 10;
            }
            else
                return 1;
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
        }
        CustomOperation csOperation = CustomOperation.OP_Default;
        public void RelStart()
        {
            
            this.Show();
            LoadShape("symbol20.xml");
            RelFormdialog reldialog = new RelFormdialog();
            // reldialog.Parent = this;
            reldialog.ShowDialog();
            Relaflag = true;          
                    
            if (reldialog.DialogResult == DialogResult.OK)
            {
                NewFile(fileType, DialogResult.Ignore);
                tlVectorControl1.PropertyGrid = propertyGrid;
                tlVectorControl1.ContextMenuStrip = contextMenuStrip1;
            }
            else if (reldialog.DialogResult == DialogResult.Ignore)
            {
                this.Visible = false;
                //进行变压器检验

                FrmLayoutSubstationInfo layoutSubstation = new FrmLayoutSubstationInfo();
                layoutSubstation.Biandianzhan();

            } 
            else if (reldialog.DialogResult == DialogResult.Yes) {
                this.Visible = false;
                //配电可靠性窗体
                //XtraPDrelfrm xf = new XtraPDrelfrm();
                //xf.init();
                //xf.ShowDialog();
                //更换为元件可靠性
                FrmpdrelProject xf = new FrmpdrelProject();
                xf.init();
                xf.ShowDialog();
            }
            else if (reldialog.DialogResult == DialogResult.Cancel)
            {
                this.Visible = false;
                
            }
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
            DockContainerItem dockitem = dotNetBarManager1.GetItem("DockContainerty") as DockContainerItem;
            symbolSelector = null;
            this.symbolSelector = new ItopVector.Selector.SymbolSelector(System.Windows.Forms.Application.StartupPath + "\\symbol\\" + filename);
            this.symbolSelector.Dock = DockStyle.Fill;
            tlVectorControl1.SymbolSelector = this.symbolSelector;
            dockitem.Control = this.symbolSelector;
            dockitem.Refresh();
            dockitem = dotNetBarManager1.GetItem("DockContainersx") as DockContainerItem;
            dockitem.Control = this.propertyGrid;
            dockitem.Refresh();        
            //symbolSelector.SelectedChanged += new EventHandler(symbolSelector_SelectedChanged);
            //symbolSelector.Selected += new EventHandler(symbolSelector_Selected);
            tlVectorControl1.Location = new System.Drawing.Point(176, 90);

            //tlVectorControl1.Size = new Size((Screen.PrimaryScreen.Bounds.Width - 176), (Screen.PrimaryScreen.Bounds.Height - 158));
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
        private string frmname = "";
        public void NewFile(bool type,DialogResult result)
        {
            tlVectorControl1.NewFile();
            LoadShape("symbol20.xml");
            jxtbar(1);
            tlVectorControl1.SVGDocument.SvgdataUid = Guid.NewGuid().ToString();
            SvgDocument.currentLayer = Layer.CreateNew("默认层", tlVectorControl1.SVGDocument).ID;
            Layer la = tlVectorControl1.SVGDocument.GetLayerByID(SvgDocument.currentLayer);
            la.SetAttribute("layerType", "电网规划层");
            tlVectorControl1.IsModified = false;
            frmname = this.Text;
        }
 　　　　 //此操作获得项目中的所有的短路计算元件
        private IList listSub= null;
        private IList listPow = null;
        private IList listXL = null;
        private List<eleclass> Subcol = new List<eleclass>();
        private List<eleclass> linecol = new List<eleclass>();
        private List<eleclass> Powcol = new List<eleclass>();
       
        private void intdata(string filesuid)
        {
            Subcol.Clear();
            Powcol.Clear();
            linecol.Clear();
            string con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND UID IN (SELECT PSPDEV.SVGUID FROM PSPDEV, PSP_ELCDEVICE WHERE  PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + filesuid + "')";

           listSub = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            foreach (PSP_Substation_Info dev in listSub)
            {
               
                    XmlNode element = tlVectorControl1.SVGDocument.SelectSingleNode("//*[@Deviceid='" + dev.UID + "']");
                    bool selectflag = false;
                    if (element != null)
                    {
                        selectflag = true;
                    }
                    eleclass li = new eleclass(dev.Title, dev.UID, "20", selectflag);
                    Subcol.Add(li);
                    if (!selectflag)
                    {
                        PSP_ElcDevice pg = new PSP_ElcDevice();
                        pg.DeviceSUID = dev.UID;
                        pg.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        // pg = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByKey", pg);
                        Services.BaseService.Delete<PSP_ElcDevice>(pg);
                    }
               
            }
            con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND UID IN (SELECT PSPDEV.SVGUID FROM PSPDEV, PSP_ELCDEVICE WHERE  PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + filesuid + "')";

            listPow= Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
            foreach (PSP_PowerSubstation_Info dev in listPow)
            {

                XmlNode element = tlVectorControl1.SVGDocument.SelectSingleNode("//*[@Deviceid='" + dev.UID + "']");
                bool selectflag = false;
                if (element != null)
                {
                    selectflag = true;
                }
                eleclass li = new eleclass(dev.Title, dev.UID, "30", selectflag);
                Powcol.Add(li);
                if (!selectflag)
                {
                    PSP_ElcDevice pg = new PSP_ElcDevice();
                    pg.DeviceSUID = dev.UID;
                    pg.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                   // pg = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByKey", pg);
                    Services.BaseService.Delete<PSP_ElcDevice>(pg);
                }
            }
            con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + filesuid + "' AND PSPDEV.Type = '05' ORDER BY PSPDEV.Number";
            listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            foreach (PSPDEV dev in listXL)
            {
                if (dev.KSwitchStatus == "0")
                {
                    XmlNode element = tlVectorControl1.SVGDocument.SelectSingleNode("//*[@Deviceid='" + dev.SUID + "']");
                    bool selectflag = false;
                    if (element != null)
                    {
                        selectflag = true;
                    }
                    eleclass li = new eleclass(dev.Name, dev.SUID, dev.Type, selectflag);
                    linecol.Add(li);
                }
            }
        }
        private void dotNetBarManager1_ItemClick(object sender, EventArgs e)
        {      

           
            DevComponents.DotNetBar.ButtonItem btItem = sender as DevComponents.DotNetBar.ButtonItem;
            //Layer layer1 = (Layer)LayerBox.ComboBoxEx.SelectedItem;
            if (btItem != null)
            {
                switch (btItem.Name)
                {
                    #region 文件操作
                    case "mNew":
                        frmNewProject frmprojectDLG = new frmNewProject();
                        frmprojectDLG.Name = "";
                        if (frmprojectDLG.ShowDialog() == DialogResult.OK)
                        {
                            tlVectorControl1.NewFile();
                            PSP_ELCPROJECT pd = new PSP_ELCPROJECT();
                            pd.Name = frmprojectDLG.Name;
                            pd.FileType = frmprojectDLG.FileType;
                            pd.Class = System.DateTime.Now.ToString();
                            pd.ProjectID = this.ProjectUID;
                            tlVectorControl1.SVGDocument.SvgdataUid = pd.ID;
                            SvgDocument.currentLayer = Layer.CreateNew("默认层", tlVectorControl1.SVGDocument).ID;
                            Layer la = tlVectorControl1.SVGDocument.GetLayerByID(SvgDocument.currentLayer);
                            la.SetAttribute("layerType", "电网规划层");
                            Services.BaseService.Create<PSP_ELCPROJECT>(pd);
                            if (pd.ID != null)
                            {
                                Open(pd.ID);
                                intdata(pd.ID);
                                //if (this.Text.Contains())
                                //{
                                //}
                                this.Text = frmname + "-" + pd.Name;
                            }
                            this.Show();
                            jxtbar(1);
                            LoadShape("symbol20.xml");
                        }                       
                        break;
                    case "mOpen":
                        //if (tlVectorControl1.IsModified == true)
                        //{
                            //DialogResult a;
                            //a = MessageBox.Show("图形已修改，是否保存?", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                            //if (a == DialogResult.Yes)
                            //{
                                Save();
                        //    }
                        //    else if (a == DialogResult.No)
                        //    {
                        //    }
                        //    else if (a == DialogResult.Cancel)
                        //    {
                        //        return;
                        //    }

                        //}
                        OpenProject op = new OpenProject();
                        op.ProjectID = this.ProjectUID;
                        op.Initdata(false);
                        if (op.ShowDialog() == DialogResult.OK)
                        {
                            if (op.FileSUID != null)
                            {
                                Open(op.FileSUID);
                                intdata(op.FileSUID);
                                //if (this.Text.Contains())
                                //{
                                //}
                                this.Text = frmname + "-" + op.FileName;
                            }                      
                            this.Show();
                            jxtbar(1);
                            LoadShape("symbol20.xml");
                       
                        }
                        break;
                    case "barDeviceData":
                        frmProjectManager frmPdlg = new frmProjectManager();
                        frmPdlg.SetMode(tlVectorControl1.SVGDocument.SvgdataUid);
                        frmPdlg.ShowDialog();
                        break;
                    case "btExSymbol":
                        tlVectorControl1.ExportSymbol();
                        break;
                    case "mjxt"://导入接线图    
                        ImportJxt2 jxt = new ImportJxt2(tlVectorControl1);
                        jxt.Import();
                        break;
                    case "mSave":
                        Save();
                        //tlVectorControl1.Save();
                        //frmElementName dlg = new frmElementName();
                        //dlg.TextInput = tlVectorControl1.SVGDocument.FileName;
                        //if (dlg.ShowDialog() == DialogResult.OK)
                        //{
                        //    tlVectorControl1.SVGDocument.FileName = dlg.TextInput;
                        //    Save();
                        //}
                        break;
                    case "mSaveAs":
                        tlVectorControl1.SaveAs();
                        break;
                    case "mExit":
                        this.Close();
                        break;
                    case "bt1":
                        //InitTK();
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
                        //fView.Open(tlVectorControl1.SVGDocument.SvgdataUid);
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
                       
                        break;
                    case "Dlqibutt":                       
                        break;
                        //MessageBox.Show("请选中母线点，然后点击右键输入断路器属性" "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //duluqiflag=true;
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
                    #region 无功优化参数维护
                    case "VoltLimit":                       
                        break;
                    case "GeneratorLimit":                     
                        break;
                    case "TransformLimit":                      
                        break;
                    case "SVC":                       
                        break;
                    #endregion
                    #region 基础图元
                    case "mLayer":
                        tlVectorControl1.LayerManager();
                        break;
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
                    case "butRef":
                        Ref();
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
                        
                        break;
                    case "ButtonItem8":                    
                
                        break;
                    case "mCheck":                        
                        break;
                    case "DFSResult":
                        frmSetPower frmSPR = new frmSetPower();
                        frmSPR.ProjectID = this.tlVectorControl1.SVGDocument.SvgdataUid;
                        if (frmSPR.ShowDialog() == DialogResult.OK)
                        {
                            ElectricLoadCal elcDFS = new ElectricLoadCal();
                            //string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'";
                            //string strCon2 = null;
                            //string strCon = null;
                            //strCon2 = " AND Type = '05'";
                            //strCon = strCon1 + strCon2;
                            //IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                            //strCon2 = " AND Type = '01'";
                            //strCon = strCon1 + strCon2;
                            //IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                            IList branchList = new List<PSPDEV>();
                            IList busList = new List<PSPDEV>();
                            //foreach (PSPDEV dev in listXL)
                            //{
                            //    if (dev.Number>=14)
                            //    {
                            //        branchList.Add(dev);
                            //    }
                            //}
                            //foreach (PSPDEV dev in listMX)
                            //{
                            //    if (dev.Number<=3)
                            //    {
                            //        busList.Add(dev);
                            //    }
                            //}
                            busList = frmSPR.ListPower;
                            branchList = frmSPR.ListBranch;
                            if (busList != null)
                            {
                                elcDFS.DFSER(branchList, busList, tlVectorControl1.SVGDocument.SvgdataUid, 100,1);                                
                            }
                        }                      
                        break;
                    case "DFS":
                        frmSetPower frmSP = new frmSetPower();
                        frmSP.ProjectID = this.tlVectorControl1.SVGDocument.SvgdataUid;
                        if (frmSP.ShowDialog()==DialogResult.OK)
                        {
                            ElectricLoadCal elcDFS = new ElectricLoadCal();
                            //string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'";
                            //string strCon2 = null;
                            //string strCon = null;
                            //strCon2 = " AND Type = '05'";
                            //strCon = strCon1 + strCon2;
                            //IList listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                            //strCon2 = " AND Type = '01'";
                            //strCon = strCon1 + strCon2;
                            //IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                            IList branchList = new List<PSPDEV>();
                            IList busList = new List<PSPDEV>();
                            //foreach (PSPDEV dev in listXL)
                            //{
                            //    if (dev.Number>=14)
                            //    {
                            //        branchList.Add(dev);
                            //    }
                            //}
                            //foreach (PSPDEV dev in listMX)
                            //{
                            //    if (dev.Number<=3)
                            //    {
                            //        busList.Add(dev);
                            //    }
                            //}
                            busList = frmSP.ListPower;
                            branchList = frmSP.ListBranch;
                            if (busList != null)
                            {
                                elcDFS.DFS(branchList, busList, tlVectorControl1.SVGDocument.SvgdataUid, 100);
                                ShowResult(0);
                            }
                        }                      
                   
                        break;
                    case "niula":
                        frnReport wFrom = new frnReport();
                        wFrom.Owner = this;
                        wFrom.Show();
                        wFrom.Text = this.Text + "—牛拉法潮流计算";
                        wFrom.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
                        ElectricLoadCal elc = new ElectricLoadCal();
                        elc.LFC(tlVectorControl1.SVGDocument.SvgdataUid, 1, 100,wFrom);
                        ShowResult(0,wFrom);
                        break;
                    case "pq":
                        frnReport wFromPQ = new frnReport();
                        wFromPQ.Owner = this;
                        wFromPQ.Show();
                        wFromPQ.Text = this.Text + "—PQ分解法潮流计算";
                        wFromPQ.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
                        ElectricLoadCal elcPQ = new ElectricLoadCal();
                        elcPQ.LFC(tlVectorControl1.SVGDocument.SvgdataUid, 2, 100, wFromPQ);
                        ShowResult(1, wFromPQ);
                        break;                     

                    case "GaussSeidel":
                        frnReport wFromGS = new frnReport();
                        wFromGS.Owner = this;
                        wFromGS.Show();
                        wFromGS.Text = this.Text + "—高斯赛德尔迭代法潮流计算";
                        wFromGS.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
                        ElectricLoadCal elcGS = new ElectricLoadCal();
                        elcGS.LFC(tlVectorControl1.SVGDocument.SvgdataUid, 3, 100, wFromGS);
                        ShowResult(2, wFromGS);
                        break;           
                    case "N_RZYz":
                        frnReport wFromZYZ = new frnReport();
                        wFromZYZ.Owner = this;
                        wFromZYZ.Show();
                        wFromZYZ.Text = this.Text + "—最有乘子法潮流计算";
                        wFromZYZ.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
                        ElectricLoadCal elcN_RZYz = new ElectricLoadCal();
                        elcN_RZYz.LFC(tlVectorControl1.SVGDocument.SvgdataUid, 4, 100, wFromZYZ);
                        ShowResult(3, wFromZYZ);
                        break;
                    case "niulaNfh":
                        elc = new ElectricLoadCal();
                        elc.LFCS(tlVectorControl1.SVGDocument.SvgdataUid, 4, 100);
                        ShowResult(3);
                        break;
                    case "WebRela":                        //进行网络N-1检验


                        ElectricRelcheck elcRela = new ElectricRelcheck();
                        elcRela.WebCalAndPrint(tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID, 100);
                        break;
                    case "TransRela":                       //进行变压器N-1检验

                        break;
                    case "Shortibut":                       
                        
                        break;
                    case "dd":
                        //SubPrint = true;
                        tlVectorControl1.Operation = ToolOperation.InterEnclosurePrint;
                        break;

                    case "NiulaResult":
                        ElectricLoadCal elcResult = new ElectricLoadCal();
                        elcResult.LFCER(tlVectorControl1.SVGDocument.SvgdataUid, 1, 100);
                        break;
                    case "PQResult":
                        ElectricLoadCal elcPQResult = new ElectricLoadCal();
                        elcPQResult.LFCER(tlVectorControl1.SVGDocument.SvgdataUid, 2, 100);
                        break;
                    case "GaussSeidelResult":
                        ElectricLoadCal elcGSResult = new ElectricLoadCal();
                        elcGSResult.LFCER(tlVectorControl1.SVGDocument.SvgdataUid, 3, 100);
                        break;
                    case "N_RZYzResult":
                        ElectricLoadCal elcNZResult = new ElectricLoadCal();
                        elcNZResult.LFCER(tlVectorControl1.SVGDocument.SvgdataUid,4, 100);
                        break;
                    case "NLnFHresult":
                        elc = new ElectricLoadCal();
                        elc.LFCERS(tlVectorControl1.SVGDocument.SvgdataUid, 4, 100);
                        break;
                    case "VoltEvaluation":
                        ElectricLoadCal elcVE = new ElectricLoadCal();
                        elcVE.VE(tlVectorControl1.SVGDocument.SvgdataUid, 100);
                        break;
                    case "PowerLossCal":
                        ElectricLoadCal elcPLResult = new ElectricLoadCal();
                        elcPLResult.PLE(tlVectorControl1.SVGDocument.SvgdataUid, 2, 100);
                        ShowPowerLoss();
                        break;
                    case "PowerLoss":
                        ElectricLoadCal elcPLE = new ElectricLoadCal();
                        elcPLE.PLE(tlVectorControl1.SVGDocument.SvgdataUid, 2, 100);
                        break;                   
                    case "ZLPResult1":
    

                        break;


                    case "mDLR":


                      
                        break;
                    case "clearResult":
                        try
                        {
                            XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@flag='" + "1" + "']");

                            foreach (XmlNode node in list)
                            {
                                SvgElement element = node as SvgElement;

                                tlVectorControl1.SVGDocument.CurrentElement = element;
                                tlVectorControl1.Delete();

                            }
                        }
                        catch (System.Exception ex)
                        {

                        }
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
                        
                        break;
                    #endregion

                    #region 视图
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
                                    if (element is Text)
                                    {
                                        PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                                        elcDEV.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        elcDEV.DeviceSUID = element.GetAttribute("Deviceid");
                                        Services.BaseService.Delete<PSP_ElcDevice>(elcDEV);
                                    }
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
                    case "PSPIdleOptimize":
                        ElectricLoadCal elcORP = new ElectricLoadCal();
                        elcORP.ORP(tlVectorControl1.SVGDocument.SvgdataUid, 100);
                        break;
                    #endregion
                    #region 参数维护
                    case "mNodeParam":
                        
                        break;
                    case "mLineParam":
                     
                        break;
                    case "mWire":
                                         
                        break;
                    case "nTransformLineParam":
                       
                        break;
                    case "nGNDLineParam":
                       
                        break;
                    case "mLineDL":
                                           
                        break;
                    case "mFadianDL":
                     
                        break;
                    case "mConvert":                          
                        
                        break;

                    #endregion
                }
            }
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
            //Topology2();
            
        }

        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            Process[] ps = Process.GetProcessesByName("ChaoLiu");
            if (ps.Length>0)
            {
                foreach (Process pre in ps)
                {
                    pre.Kill();
                }                
            }
        }
        public void Start()
        {
            this.Show();          
            LoadShape("symbol20.xml");
            jxtbar2(1);
            jxb = 1;
            NewFile(fileType, DialogResult.Ignore);
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.ContextMenuStrip = contextMenuStrip1;            
        }
        public void PowerLossStart()
        {
            this.Show();
            LoadShape("symbol20.xml");
            NewFile(fileType, DialogResult.Ignore);
            jxtbar2(2);
            jxb = 2;
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.ContextMenuStrip = contextMenuStrip1;            
        }
        public void DFSStart()
        {
            this.Show();
            LoadShape("symbol20.xml");
            NewFile(fileType, DialogResult.Ignore);
            jxtbar2(5);
            jxb = 5;
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.ContextMenuStrip = contextMenuStrip1;
        }
        public void VoltEvaluationStart()
        {
            this.Show();
            LoadShape("symbol20.xml");
            NewFile(fileType, DialogResult.Ignore); 
            jxtbar2(3);
            jxb = 3;
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.ContextMenuStrip = contextMenuStrip1;            
        }
        public void ORPStrat()
        {
            this.Show();
            LoadShape("symbol20.xml");
            NewFile(fileType, DialogResult.Ignore);
            jxtbar2(4);
            jxb = 4;
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.ContextMenuStrip = contextMenuStrip1;            
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

            if (tlVectorControl1.SVGDocument.SvgdataUid != string.Empty)
            {
                IList svglist = Services.BaseService.GetList("SelectSVGFILEByKey", tlVectorControl1.SVGDocument.SvgdataUid);
                if (svglist.Count > 0)
                {
                    svg = (SVGFILE)svglist[0];
                    svg.SVGDATA = tlVectorControl1.SVGDocument.OuterXml;
                    svg.FILENAME = tlVectorControl1.SVGDocument.FileName;
                    Services.BaseService.Update<SVGFILE>(svg);
                    //PSPDIR pspDir = new PSPDIR();
                    //pspDir.FileGUID = svg.SUID;
                    //pspDir.FileName = tlVectorControl1.SVGDocument.FileName;
                    //if (fileType == true)
                    //{
                    //    pspDir.FileType = "潮流";
                    //} 
                    //else
                    //{
                    //    pspDir.FileType = "短路";
                    //}
                    //Services.BaseService.Update<PSPDIR>(pspDir);
                }
                else
                {
                    svg.SUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    svg.FILENAME = tlVectorControl1.SVGDocument.FileName;
                    svg.SVGDATA = tlVectorControl1.SVGDocument.OuterXml;
                    Services.BaseService.Create<SVGFILE>(svg);
                    //PSPDIR pspDir = new PSPDIR();
                    //pspDir.FileGUID = svg.SUID;
                    //pspDir.FileName = svg.FILENAME;
                    //if (fileType == true)
                    //{
                    //    pspDir.FileType = "潮流";
                    //}
                    //else
                    //{
                    //    pspDir.FileType = "短路";
                    //}
                    //pspDir.CreateTime = System.DateTime.Now.ToString();
                    //Services.BaseService.Create<PSPDIR>(pspDir);
                }
            }
            else
            {
                svg.SUID = Guid.NewGuid().ToString();
                svg.FILENAME = tlVectorControl1.SVGDocument.FileName;
                svg.SVGDATA = tlVectorControl1.SVGDocument.OuterXml;
                Services.BaseService.Create<SVGFILE>(svg);
                tlVectorControl1.SVGDocument.SvgdataUid = svg.SUID;
                //PSPDIR pspDir = new PSPDIR();
                //pspDir.FileGUID = svg.SUID;
                //pspDir.FileName = svg.FILENAME;
                //if (fileType == true)
                //{
                //    pspDir.FileType = "潮流";
                //}
                //else
                //{
                //    pspDir.FileType = "短路";
                //}
                //pspDir.CreateTime = System.DateTime.Now.ToString();
                //Services.BaseService.Create<PSPDIR>(pspDir);
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
                lar.YearID ="'"+ yearID+"'";
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
                tlVectorControl1.SVGDocument.SvgdataUid = SVGUID;
                tlVectorControl1.SVGDocument.FileName = this.Text;
                tlVectorControl1.DocumentbgColor = Color.White;
                tlVectorControl1.BackColor = Color.White;

                foreach(XmlElement ele in document.RootElement.ChildNodes ){
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
            //tlVectorControl1.SVGDocument.SvgdataUid = "";
        }
        public void Open(string _SvgUID)
        {
            try
            {
                SVGFILE svgFile = new SVGFILE();
                svgFile.SUID = _SvgUID;
                SvgDocument document = new SvgDocument();
                if (document != null)
                {
                    IList svgList = Services.BaseService.GetList("SelectSVGFILEByKey", svgFile);
                    if (svgList.Count > 0)
                    {
                        svgFile = (SVGFILE)svgList[0];
                    }

                    document = new SvgDocument();
                    if (!string.IsNullOrEmpty(svgFile.SVGDATA))
                    {
                        document.LoadXml(svgFile.SVGDATA);
                    }
                    
                    document.FileName = svgFile.FILENAME;
                    document.SvgdataUid = svgFile.SUID;
                }
                SVGUID = document.SvgdataUid;

                //this.Text = document.FileName;
                if (document.RootElement == null)
                {
                    tlVectorControl1.NewFile();
                    tlVectorControl1.SVGDocument.SvgdataUid = _SvgUID;
                    SvgDocument.currentLayer = Layer.CreateNew("默认层", tlVectorControl1.SVGDocument).ID;
                    Layer la = tlVectorControl1.SVGDocument.GetLayerByID(SvgDocument.currentLayer);
                    la.SetAttribute("layerType", "电网规划层");
                   
                }
                else
                {
                    tlVectorControl1.SVGDocument = document;
                    tlVectorControl1.SVGDocument.CurrentLayer = ((tlVectorControl1.SVGDocument.getLayerList()[0]) as ItopVector.Core.Figure.Layer);
                }
                tlVectorControl1.SVGDocument.SvgdataUid = SVGUID;
                tlVectorControl1.SVGDocument.FileName = this.Text;
                tlVectorControl1.DocumentbgColor = Color.White;
                tlVectorControl1.BackColor = Color.White;

                //XmlNodeList listUse = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
                //foreach (XmlNode node in listUse)
                //{
                //    PSP_ElcDevice elcDev = new PSP_ElcDevice();
                //    elcDev.DeviceSUID = (node as XmlElement).GetAttribute("Deviceid");
                //    elcDev.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                //    elcDev = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDev);
                //    if(elcDev==null)
                //    {
                //        tlVectorControl1.SVGDocument.CurrentElement = node as SvgElement;
                //        tlVectorControl1.Delete();
                //        XmlNodeList listFirstNode = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@FirstNode='" + (node as XmlElement).GetAttribute("id") + "']");
                //        XmlNodeList listLastNode = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@LastNode='" + (node as XmlElement).GetAttribute("id") + "']");
                //        foreach (XmlNode line in listFirstNode)
                //        {
                //            tlVectorControl1.SVGDocument.CurrentElement = line as SvgElement;
                //            tlVectorControl1.Delete();
                //        }
                //        foreach (XmlNode line in listLastNode)
                //        {
                //            tlVectorControl1.SVGDocument.CurrentElement = line as SvgElement;
                //            tlVectorControl1.Delete();
                //        }
                //        XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + (node as XmlElement).GetAttribute("id") + "']");
                //        tlVectorControl1.SVGDocument.CurrentElement = text as SvgElement;
                //        tlVectorControl1.Delete();
                //    }
                //}
                try
                {
                    string strCon = ",PSP_Substation_Info WHERE PSP_Substation_Info.UID=PSP_ELCDEVICE.DeviceSUID AND ProjectSUID ='" + tlVectorControl1.SVGDocument.SvgdataUid + "'";
                    //elcDev.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    IList list = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon);
                    foreach (PSP_ElcDevice elcDev in list)
                    {
                        XmlNode useDEV = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@SubstationID='" + elcDev.DeviceSUID + "']");
                        if (useDEV == null)
                        {
                            Services.BaseService.Delete<PSP_ElcDevice>(elcDev);
                        }
                    }
                }
                catch (System.Exception ex)
                {

                }               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }    
              
        private void frmTLpspGraphical_FormClosing(object sender, FormClosingEventArgs e)
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
                        Save();
                    }
                    else if (a == "Cancel")
                    {
                        e.Cancel = true;
                        return;
                    }
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
                        //    pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        //    IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUID", pspDev);
                        //    foreach (PSPDEV dev in list)
                        //    {
                        //        Services.BaseService.Delete<PSPDEV>(dev);
                        //    } 
                        //}
                    //}
                }
            }
            catch (Exception e1) { }
        }
        private  DeviceCOL GetColValue(PSP_ElcDevice elcDEV,int order)
        {
            DeviceCOL devCol = new DeviceCOL();
            if (order==0)
            {
                devCol.COL1 = elcDEV.COL1;
                devCol.COL2 = elcDEV.COL2;
                devCol.COL3 = elcDEV.COL3;
                devCol.COL4 = elcDEV.COL4;
                devCol.COL5 = elcDEV.COL5;
                devCol.COL6 = elcDEV.COL6;
                devCol.COL7 = elcDEV.COL7;
                devCol.COL8 = elcDEV.COL8;
                devCol.COL9 = elcDEV.COL9;
                devCol.COL10 = elcDEV.COL10;
                devCol.COL11 = elcDEV.COL11;
                devCol.COL12 = elcDEV.COL12;
                devCol.COL13 = elcDEV.COL13;
                devCol.COL14 = elcDEV.COL14;
                devCol.COL15 = elcDEV.COL15;
                devCol.COL16 = elcDEV.COL16;
                devCol.COL17 = elcDEV.COL17;
                devCol.COL18 = elcDEV.COL18;
                devCol.COL19 = elcDEV.COL19;
                devCol.COL20 = elcDEV.COL20;
            }
            else if (order==1)
            {
                devCol.COL1 = elcDEV.COL21;
                devCol.COL2 = elcDEV.COL22;
                devCol.COL3 = elcDEV.COL23;
                devCol.COL4 = elcDEV.COL24;
                devCol.COL5 = elcDEV.COL25;
                devCol.COL6 = elcDEV.COL26;
                devCol.COL7 = elcDEV.COL27;
                devCol.COL8 = elcDEV.COL28;
                devCol.COL9 = elcDEV.COL29;
                devCol.COL10 = elcDEV.COL30;
                devCol.COL11 = elcDEV.COL31;
                devCol.COL12 = elcDEV.COL32;
                devCol.COL13 = elcDEV.COL33;
                devCol.COL14 = elcDEV.COL34;
                devCol.COL15 = elcDEV.COL35;
                devCol.COL16 = elcDEV.COL36;
                devCol.COL17 = elcDEV.COL37;
                devCol.COL18 = elcDEV.COL38;
                devCol.COL19 = elcDEV.COL39;
                devCol.COL20 = elcDEV.COL40;
            }
            else if (order==2)
            {
                devCol.COL1 = elcDEV.COL41;
                devCol.COL2 = elcDEV.COL42;
                devCol.COL3 = elcDEV.COL43;
                devCol.COL4 = elcDEV.COL44;
                devCol.COL5 = elcDEV.COL45;
                devCol.COL6 = elcDEV.COL46;
                devCol.COL7 = elcDEV.COL47;
                devCol.COL8 = elcDEV.COL48;
                devCol.COL9 = elcDEV.COL49;
                devCol.COL10 = elcDEV.COL50;
                devCol.COL11 = elcDEV.COL51;
                devCol.COL12 = elcDEV.COL52;
                devCol.COL13 = elcDEV.COL53;
                devCol.COL14 = elcDEV.COL54;
                devCol.COL15 = elcDEV.COL55;
                devCol.COL16 = elcDEV.COL56;
                devCol.COL17 = elcDEV.COL57;
                devCol.COL18 = elcDEV.COL58;
                devCol.COL19 = elcDEV.COL59;
                devCol.COL20 = elcDEV.COL60;
            }
            else if (order==3)
            {
                devCol.COL1 = elcDEV.COL61;
                devCol.COL2 = elcDEV.COL62;
                devCol.COL3 = elcDEV.COL63;
                devCol.COL4 = elcDEV.COL64;
                devCol.COL5 = elcDEV.COL65;
                devCol.COL6 = elcDEV.COL66;
                devCol.COL7 = elcDEV.COL67;
                devCol.COL8 = elcDEV.COL68;
                devCol.COL9 = elcDEV.COL69;
                devCol.COL10 = elcDEV.COL70;
                devCol.COL11 = elcDEV.COL71;
                devCol.COL12 = elcDEV.COL72;
                devCol.COL13 = elcDEV.COL73;
                devCol.COL14 = elcDEV.COL74;
                devCol.COL15 = elcDEV.COL75;
                devCol.COL16 = elcDEV.COL76;
                devCol.COL17 = elcDEV.COL77;
                devCol.COL18 = elcDEV.COL78;
                devCol.COL19 = elcDEV.COL79;
                devCol.COL20 = elcDEV.COL80;
            }
            return devCol;
        }
        private void ShowResult(int order)
        {
            try
            {
                XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@flag='" + "1" + "']");

                foreach (XmlNode node in list)
                {
                    SvgElement element = node as SvgElement;
                    if ((element.GetAttribute("textn1id") == null || element.GetAttribute("textn1id") == "") && (element.GetAttribute("textn2id") == null || element.GetAttribute("textn2id") == ""))
                    {
                        tlVectorControl1.SVGDocument.CurrentElement = element;
                        tlVectorControl1.Delete();
                    }
                }
                double yinzi = 0, capability = 0, volt = 0, standvolt = 0, current = 0;
                //PSPDEV benchmark = new PSPDEV();
                //benchmark.Type = "power";
                //benchmark.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                //IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
                //if (list3 == null)
                //{
                //    MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //foreach (PSPDEV dev in list3)
                //{
                //    yinzi = Convert.ToDouble(dev.PowerFactor);
                //    capability = Convert.ToDouble(dev.StandardCurrent);
                //    volt = Convert.ToDouble(dev.StandardVolt);
                //    TLPSPVmin = dev.iV;
                //    TLPSPVmax = dev.jV;
                //    if (dev.PowerFactor == 0)
                //    {
                //        yinzi = 1;
                //    }
                //    if (dev.StandardCurrent == 0)
                //    {
                //        capability = 1;
                //    }
                //    if (dev.StandardVolt == 0)
                //    {
                //        volt = 1;
                //    }
                //    standvolt = volt;
                //    current = capability / (Math.Sqrt(3) * volt);
                //};
            
                //SvgDocument.currentLayer = Layer.CreateNew("结果显示", tlVectorControl1.SVGDocument).ID;
                tlVectorControl1.SVGDocument.AcceptChanges = true;
                XmlNodeList useList = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
                XmlNodeList layerlist = tlVectorControl1.SVGDocument.GetElementsByTagName("layer");
                Layer layResult;
                bool lb = true;
                foreach (Layer lay in layerlist)
                {
                    if (lay.GetAttribute("label")=="结果显示")
                    {
                        SvgDocument.currentLayer = lay.ID;
                        lb = false;
                    }
                }
                if (lb)
                {
                    SvgDocument.currentLayer = Layer.CreateNew("结果显示", tlVectorControl1.SVGDocument).ID;
                }         
                foreach (XmlNode node in useList)
                {
                    XmlElement element = node as XmlElement;
                    //XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                    string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01'";
                    IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    for (int i = 0; i < listMX.Count; i++)
                    {
                        PSPDEV elementDEV = (PSPDEV)(listMX[i]);
                        PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                        elcDEV.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        elcDEV.DeviceSUID = ((PSPDEV)listMX[i]).SUID;
                        elcDEV = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDEV);
                        if (elcDEV != null)
                        {
                            RectangleF bound = ((IGraph)element).GetBounds();
                            XmlElement n1 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@textn1id='" + element.GetAttribute("Deviceid") + "']") as XmlElement;
                            XmlElement n2 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@textn2id='" + element.GetAttribute("Deviceid") + "']") as XmlElement;
                            Layer la = tlVectorControl1.SVGDocument.GetLayerByID(element.GetAttribute("layer"));
                          
                            if (n1 == null)
                            {
                                n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                n1.SetAttribute("x", Convert.ToString(bound.X));
                                n1.SetAttribute("y", Convert.ToString(bound.Y - (i + 1) * 10 * tlVectorControl1.ScaleRatio));
                                n1.SetAttribute("font-size", "6");
                                n1.SetAttribute("font-family", "楷体_GB2312");
                                n1.SetAttribute("layer", SvgDocument.currentLayer);
                                //MessageBox.Show(Convert.ToString(n1.InnerText));
                            
                                //n1.SetAttribute("layer", la.ID);
                                //MessageBox.Show(Convert.ToString(n1.InnerText));
                                n1.SetAttribute("flag", "1");
                                n1.SetAttribute("textn1id", element.GetAttribute("Deviceid"));
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            }

                            if (elementDEV.KSwitchStatus=="1")
                            {
                                n1.InnerText = "0";
                            } 
                            else
                            {
                                n1.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL2).ToString("N2");
                            }
                            

                            if (Convert.ToDouble(GetColValue(elcDEV, order).COL2) > elementDEV.jV || Convert.ToDouble(GetColValue(elcDEV, order).COL2) <elementDEV.iV)//电压越限，需修改
                                n1.SetAttribute("stroke", "#FF0000");
                            if (elementDEV.NodeType == "0")
                            {
                               
                                if (n2 == null)
                                {
                                    n2 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                    n2.SetAttribute("x", Convert.ToString(bound.X));
                                    n2.SetAttribute("y", Convert.ToString(bound.Y + bound.Height + 20));
                                    n2.SetAttribute("layer", SvgDocument.currentLayer);
                                    n2.SetAttribute("flag", "1");
                                    n2.SetAttribute("font-size", "6");
                                    n2.SetAttribute("font-family", "楷体_GB2312");
                                    n2.SetAttribute("textn2id", element.GetAttribute("Deviceid"));
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);
                                }
                                if (elementDEV.KSwitchStatus=="1")
                                {
                                    n2.InnerText = "0" + "j" + "0";
                                } 
                                else
                                {
                                    if (Convert.ToDouble(GetColValue(elcDEV, order).COL5) >= 0)
                                    {
                                        n2.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL4).ToString("N2") + "  + " + "j" + Convert.ToDouble(GetColValue(elcDEV, order).COL5).ToString("N2");
                                    }
                                    else
                                    {
                                        n2.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL4).ToString("N2") + "  - " + "j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                                    }
                                }

 

                                double tempi = Convert.ToDouble(GetColValue(elcDEV, order).COL4);
                                double tempj = Convert.ToDouble(GetColValue(elcDEV, order).COL5);
                                double temptotal = Math.Sqrt(tempi * tempi + tempj * tempj);
                                if (temptotal > Convert.ToDouble(elementDEV.Burthen))
                                {
                                    n2.SetAttribute("stroke", "#FF0000");
                                }
                                //tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);                           

                            }
                            //tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            //tlVectorControl1.Operation = ToolOperation.Select;
                            tlVectorControl1.Refresh();
                        }
                    }
                }
                XmlNodeList polyLineList = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline");
            
                foreach (XmlNode node in polyLineList)
                {
           
                    XmlElement element = node as XmlElement;                 
                    PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                    elcDEV.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    elcDEV.DeviceSUID = element.GetAttribute("Deviceid");
                    elcDEV = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDEV);                 
                    PSPDEV elementDEV = new PSPDEV();
                    Layer la = tlVectorControl1.SVGDocument.GetLayerByID(element.GetAttribute("layer"));
                    if (elcDEV!=null)
                    {
                        elementDEV.SUID = elcDEV.DeviceSUID;
                        elementDEV = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", elementDEV);
                     
                    }    
                    else
                    {
                        continue;
                    }

                    if (elementDEV != null)
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
                        XmlElement n1 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@textn1id='" + element.GetAttribute("Deviceid") + "']") as XmlElement;
                        //XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                        //XmlElement n3 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;

                        PointF pStart = new PointF(center.X + (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(15 * Math.Cos((angel) * Math.PI / 180)));
                        PointF pStart2 = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));

                        XmlNode firstNodeElement = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + element.GetAttribute("FirstNode") + "']");
                        XmlNode lastNodeElement = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + element.GetAttribute("LastNode") + "']");
                        if(firstNodeElement!=null&&lastNodeElement!=null)
                        {
                            if ((angel > 10 && angel < 90) || (angel < 0 && Math.Abs(angel) < 90) || (angel > 180 && angel < 350))
                            {
                                if (t2[0].X > ((IGraph)firstNodeElement).CenterPoint.X)
                                {
                                    pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                }
                            }
                            else if ((angel >= 0 && angel <= 10) || (angel >= 350 && angel <= 360) || (angel < 0 && Math.Abs(angel) <= 90))
                            {
                                if (t2[0].Y > ((IGraph)firstNodeElement).CenterPoint.Y)
                                {
                                    pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                }
                            }
                            else if ((angel < 0 && Math.Abs(angel) > 90) || (angel >= 90 && angel <= 180))
                            {
                                if (t2[0].Y > ((IGraph)firstNodeElement).CenterPoint.Y)
                                {
                                    pStart = new PointF(center.X - (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                                    pStart2 = new PointF(center.X + (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                                }
                            }
                        }
                       

                        //if (t2[0].X > ((IGraph)firstNodeElement).CenterPoint.X || t2[0].Y < ((IGraph)firstNodeElement).CenterPoint.Y)
                        //{
                        //    pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                        //    pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                        //}
                        //else
                        //{
                        //    pStart = new PointF(center.X - (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                        //    pStart2 = new PointF(center.X + (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                        //}



                        PointF newp1 = new PointF(t[0].X + (t[1].X - t[0].X) / 2 - (float)(15 * Math.Sin(angel)), t[0].Y + (t[1].Y - t[0].Y) / 2 - (float)(15 * Math.Cos(angel)));
                      
                     
                       
                        if (n1 == null)
                        {
                            n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            n1.SetAttribute("x", Convert.ToString(pStart.X));
                            n1.SetAttribute("y", Convert.ToString(pStart.Y));
                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                            n1.SetAttribute("flag", "1");
                            n1.SetAttribute("font-size", "6");
                            n1.SetAttribute("font-family", "楷体_GB2312");
                            n1.SetAttribute("textn1id", element.GetAttribute("Deviceid"));
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            tlVectorControl1.Operation = ToolOperation.Select;

                            tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;

                            RectangleF ttt = ((Polyline)element).GetBounds();

                            tlVectorControl1.RotateSelection(angel, pStart);
                            if (Math.Abs(angel) > 90)
                                tlVectorControl1.RotateSelection(180, pStart);
                        }

                        if (elementDEV.KSwitchStatus=="1")
                        {
                            n1.InnerText = "0" + "j" + "0";
                        } 
                        else
                        {
                            if (Convert.ToDouble(GetColValue(elcDEV, order).COL4) >= 0)
                            {
                                if (Convert.ToDouble(GetColValue(elcDEV, order).COL5) >= 0)
                                {
                                    n1.InnerText = (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL4))).ToString("N2") + " + j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                                }
                                else
                                {
                                    n1.InnerText = (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL4))).ToString("N2") + " - j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                                }
                            } 
                            else
                            {
                                if (Convert.ToDouble(GetColValue(elcDEV, order).COL5) >= 0)
                                {
                                    n1.InnerText = (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL4))).ToString("N2") + " - j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                                }
                                else
                                {
                                    n1.InnerText = (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL4))).ToString("N2") + " + j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                                }
                            }
                           
                        }
                      

              
                        if (Convert.ToDouble(GetColValue(elcDEV, order).COL14) > (double)(elementDEV.Burthen))//电流越限，需修改。
                            n1.SetAttribute("stroke", "#FF0000");               


                        PointF p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 10) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 10) * Math.PI / 180)));
                        PointF p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 350) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 350) * Math.PI / 180)));

                        if (Convert.ToDouble(GetColValue(elcDEV, order).COL4) < 0)
                        {
                            p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 170) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 170) * Math.PI / 180)));
                            p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 190) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 190) * Math.PI / 180)));
                        }

                        string l1 = Convert.ToString(p1.X);
                        string l2 = Convert.ToString(p1.Y);
                        string l5 = Convert.ToString(p2.X);
                        string l6 = Convert.ToString(p2.Y);
                        XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("polygon") as Polygon;
                        n2.SetAttribute("points", l1 + " " + l2 + "," + l3 + " " + l4 + "," + l5 + " " + l6);
                        n2.SetAttribute("fill-opacity", "1");
                        n2.SetAttribute("layer", la.ID);
                        n2.SetAttribute("flag", "1");
                        n2.SetAttribute("font-size", "6");
                        n1.SetAttribute("font-family", "楷体_GB2312");
                        n2.SetAttribute("style", "fill:#000000");
                        tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);
                        tlVectorControl1.SVGDocument.CurrentElement = n2 as SvgElement;

                        //tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                        //tlVectorControl1.Operation = ToolOperation.Select;

                        //tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;

                        //RectangleF ttt = ((Polyline)element).GetBounds();

                        //tlVectorControl1.RotateSelection(angel, pStart);
                        //if (Math.Abs(angel) > 90)
                        //    tlVectorControl1.RotateSelection(180, pStart);
                     
                        tlVectorControl1.Refresh();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("参数错误，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            tlVectorControl1.SVGDocument.CurrentLayer =(Layer)tlVectorControl1.SVGDocument.Layers[0];

        }
        private void ShowResult(int order,frnReport wFrom)
        {
            wFrom.ShowText += "\r\n开始显示计算结果\t" + System.DateTime.Now.ToString();
            try
            {
                XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@flag='" + "1" + "']");

                foreach (XmlNode node in list)
                {
                    SvgElement element = node as SvgElement;
                    if ((element.GetAttribute("textn1id") == null || element.GetAttribute("textn1id") == "") && (element.GetAttribute("textn2id") == null || element.GetAttribute("textn2id") == ""))
                    {
                        tlVectorControl1.SVGDocument.CurrentElement = element;
                        tlVectorControl1.Delete();
                    }
                }
                double yinzi = 0, capability = 0, volt = 0, standvolt = 0, current = 0;
                //PSPDEV benchmark = new PSPDEV();
                //benchmark.Type = "power";
                //benchmark.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                //IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
                //if (list3 == null)
                //{
                //    MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //foreach (PSPDEV dev in list3)
                //{
                //    yinzi = Convert.ToDouble(dev.PowerFactor);
                //    capability = Convert.ToDouble(dev.StandardCurrent);
                //    volt = Convert.ToDouble(dev.StandardVolt);
                //    TLPSPVmin = dev.iV;
                //    TLPSPVmax = dev.jV;
                //    if (dev.PowerFactor == 0)
                //    {
                //        yinzi = 1;
                //    }
                //    if (dev.StandardCurrent == 0)
                //    {
                //        capability = 1;
                //    }
                //    if (dev.StandardVolt == 0)
                //    {
                //        volt = 1;
                //    }
                //    standvolt = volt;
                //    current = capability / (Math.Sqrt(3) * volt);
                //};

                //SvgDocument.currentLayer = Layer.CreateNew("结果显示", tlVectorControl1.SVGDocument).ID;
                tlVectorControl1.SVGDocument.AcceptChanges = true;
                XmlNodeList useList = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
                XmlNodeList layerlist = tlVectorControl1.SVGDocument.GetElementsByTagName("layer");
                Layer layResult;
                bool lb = true;
                foreach (Layer lay in layerlist)
                {
                    if (lay.GetAttribute("label") == "结果显示")
                    {
                        SvgDocument.currentLayer = lay.ID;
                        lb = false;
                    }
                }
                if (lb)
                {
                    SvgDocument.currentLayer = Layer.CreateNew("结果显示", tlVectorControl1.SVGDocument).ID;
                }
                wFrom.ShowText += "\r\n正在显示变电站信息\t" + System.DateTime.Now.ToString();
                int count = 0;
                foreach (XmlNode node in useList)
                {
                    count++;
                    XmlElement element = node as XmlElement;
                    string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01' order by ratevolt desc,referencevolt desc,col2 desc,col22 desc,col42 desc,col62 desc"; 
                    switch(order)
                    {
                        case 0:
                            strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01' order by ratevolt desc,referencevolt desc,col2 desc";
                            break;
                        case 1:
                            strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01' order by ratevolt desc,referencevolt desc,col22 desc";
                            break;
                        case 2:
                            strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01' order by ratevolt desc,referencevolt desc,col42 desc";
                            break;
                        case 3:
                            strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01' order by ratevolt desc,referencevolt desc,col62 desc";
                            break;
                        default:
                            strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01' order by ratevolt desc,referencevolt desc,col2 desc,col22 desc,col42 desc,col62 desc";
                            break;
                    }
                    //XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                    //string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01' order by ratevolt desc,referencevolt desc,col2 desc,col22 desc,col42 desc,col62 desc";
                    IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    if (listMX.Count <= 0)
                        continue;
                  
                    if ((PSPDEV)(listMX[0])!=null)
                    {
                        PSPDEV elementDEV = (PSPDEV)(listMX[0]);
                        PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                        elcDEV.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        elcDEV.DeviceSUID = ((PSPDEV)listMX[0]).SUID;
                        elcDEV = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDEV);
                        if (elcDEV != null)
                        {
                            RectangleF bound = ((IGraph)element).GetBounds();
                            XmlElement n1 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@textn1id='" + element.GetAttribute("Deviceid") + "']") as XmlElement;
                            XmlElement n2 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@textn2id='" + element.GetAttribute("Deviceid") + "']") as XmlElement;
                            Layer la = tlVectorControl1.SVGDocument.GetLayerByID(element.GetAttribute("layer"));

                            if (n1 == null)
                            {
                                n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                n1.SetAttribute("x", Convert.ToString(bound.X));
                                n1.SetAttribute("y", Convert.ToString(bound.Y - (0 + 1) * 10 * tlVectorControl1.ScaleRatio));
                                n1.SetAttribute("font-size", "6");
                                n1.SetAttribute("font-family", "楷体_GB2312");
                                n1.SetAttribute("layer", SvgDocument.currentLayer);
                                //MessageBox.Show(Convert.ToString(n1.InnerText));

                                //n1.SetAttribute("layer", la.ID);
                                //MessageBox.Show(Convert.ToString(n1.InnerText));
                                n1.SetAttribute("flag", "1");
                                n1.SetAttribute("textn1id", element.GetAttribute("Deviceid"));
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            }

                            if (elementDEV.KSwitchStatus == "1")
                            {
                                n1.InnerText = "0";
                            }
                            else
                            {
                                n1.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL2).ToString("N2");
                            }

                            if (elementDEV.KSwitchStatus == "0")
                            {
                                if (Convert.ToDouble(GetColValue(elcDEV, order).COL2 == "" ? "0" : GetColValue(elcDEV, order).COL2) > elementDEV.jV || Convert.ToDouble(GetColValue(elcDEV, order).COL2 == "" ? "0" : GetColValue(elcDEV, order).COL2) < elementDEV.iV)//电压越限，需修改
                                    n1.SetAttribute("stroke", "#FF0000");
                               
                            }
                            if (elementDEV.NodeType == "0")
                            {

                                if (n2 == null)
                                {
                                    n2 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                    n2.SetAttribute("x", Convert.ToString(bound.X));
                                    n2.SetAttribute("y", Convert.ToString(bound.Y + bound.Height + 20));
                                    n2.SetAttribute("layer", SvgDocument.currentLayer);
                                    n2.SetAttribute("flag", "1");
                                    n2.SetAttribute("font-size", "6");
                                    n2.SetAttribute("font-family", "楷体_GB2312");
                                    n2.SetAttribute("textn2id", element.GetAttribute("Deviceid"));
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);
                                }
                                if (elementDEV.KSwitchStatus == "1")
                                {
                                    n2.InnerText = "0" + "j" + "0";
                                }
                                else
                                {
                                    if (Convert.ToDouble(GetColValue(elcDEV, order).COL5) >= 0)
                                    {
                                        n2.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL4).ToString("N2") + "  + " + "j" + Convert.ToDouble(GetColValue(elcDEV, order).COL5).ToString("N2");
                                    }
                                    else
                                    {
                                        n2.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL4).ToString("N2") + "  - " + "j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                                    }
                                }



                                double tempi = Convert.ToDouble(GetColValue(elcDEV, order).COL4);
                                double tempj = Convert.ToDouble(GetColValue(elcDEV, order).COL5);
                                double temptotal = Math.Sqrt(tempi * tempi + tempj * tempj);
                                if (temptotal > Convert.ToDouble(elementDEV.Burthen))
                                {
                                    n2.SetAttribute("stroke", "#FF0000");
                                }
                                //tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);                           

                            }
                            //tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            //tlVectorControl1.Operation = ToolOperation.Select;
                            tlVectorControl1.Refresh();
                        }
                    }
                }
                XmlNodeList polyLineList = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline");
                wFrom.ShowText += "\r\n正在显示线路信息\t" + System.DateTime.Now.ToString();
                foreach (XmlNode node in polyLineList)
                {
                  

                    XmlElement element = node as XmlElement;
                    PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                    elcDEV.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    elcDEV.DeviceSUID = element.GetAttribute("Deviceid");
                    elcDEV = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDEV);
                    PSPDEV elementDEV = new PSPDEV();
                    Layer la = tlVectorControl1.SVGDocument.GetLayerByID(element.GetAttribute("layer"));
                    if (elcDEV != null)
                    {
                        elementDEV.SUID = elcDEV.DeviceSUID;
                        elementDEV = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", elementDEV);

                    }
                    else
                    {
                        continue;
                    }

                    if (elementDEV != null)
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
                        XmlElement n1 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@textn1id='" + element.GetAttribute("Deviceid") + "']") as XmlElement;
                        //XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                        //XmlElement n3 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;

                        PointF pStart = new PointF(center.X + (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(15 * Math.Cos((angel) * Math.PI / 180)));
                        PointF pStart2 = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));

                        XmlNode firstNodeElement = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + element.GetAttribute("FirstNode") + "']");
                        XmlNode lastNodeElement = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + element.GetAttribute("LastNode") + "']");
                        if (firstNodeElement != null && lastNodeElement != null)
                        {
                            if ((angel > 10 && angel < 90) || (angel < 0 && Math.Abs(angel) < 90) || (angel > 180 && angel < 350))
                            {
                                if (t2[0].X > ((IGraph)firstNodeElement).CenterPoint.X)
                                {
                                    pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                }
                            }
                            else if ((angel >= 0 && angel <= 10) || (angel >= 350 && angel <= 360) || (angel < 0 && Math.Abs(angel) <= 90))
                            {
                                if (t2[0].Y > ((IGraph)firstNodeElement).CenterPoint.Y)
                                {
                                    pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                }
                            }
                            else if ((angel < 0 && Math.Abs(angel) > 90) || (angel >= 90 && angel <= 180))
                            {
                                if (t2[0].Y > ((IGraph)firstNodeElement).CenterPoint.Y)
                                {
                                    pStart = new PointF(center.X - (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                                    pStart2 = new PointF(center.X + (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                                }
                            }
                        }


                        //if (t2[0].X > ((IGraph)firstNodeElement).CenterPoint.X || t2[0].Y < ((IGraph)firstNodeElement).CenterPoint.Y)
                        //{
                        //    pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                        //    pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                        //}
                        //else
                        //{
                        //    pStart = new PointF(center.X - (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                        //    pStart2 = new PointF(center.X + (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                        //}



                        PointF newp1 = new PointF(t[0].X + (t[1].X - t[0].X) / 2 - (float)(15 * Math.Sin(angel)), t[0].Y + (t[1].Y - t[0].Y) / 2 - (float)(15 * Math.Cos(angel)));



                        if (n1 == null)
                        {
                            n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            n1.SetAttribute("x", Convert.ToString(pStart.X));
                            n1.SetAttribute("y", Convert.ToString(pStart.Y));
                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                            n1.SetAttribute("flag", "1");
                            n1.SetAttribute("font-size", "6");
                            n1.SetAttribute("font-family", "楷体_GB2312");
                            n1.SetAttribute("textn1id", element.GetAttribute("Deviceid"));
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            tlVectorControl1.Operation = ToolOperation.Select;

                            tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;

                            RectangleF ttt = ((Polyline)element).GetBounds();

                            tlVectorControl1.RotateSelection(angel, pStart);
                            if (Math.Abs(angel) > 90)
                                tlVectorControl1.RotateSelection(180, pStart);
                        }

                        if (elementDEV.KSwitchStatus == "1")
                        {
                            n1.InnerText = "0" + "j" + "0";
                        }
                        else
                        {
                            if (Convert.ToDouble(GetColValue(elcDEV, order).COL5) * Convert.ToDouble(GetColValue(elcDEV, order).COL4) >= 0)
                            {
                                n1.InnerText = (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL4))).ToString("N2") + " + j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                            }
                            else
                            {
                                n1.InnerText = (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL4))).ToString("N2") + " - j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                            }

                            if (Convert.ToDouble(GetColValue(elcDEV, order).COL14) > (double)(elementDEV.Burthen))//电流越限，需修改。
                                n1.SetAttribute("stroke", "#FF0000");
                            PointF p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 10) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 10) * Math.PI / 180)));
                            PointF p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 350) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 350) * Math.PI / 180)));

                            if (Convert.ToDouble(GetColValue(elcDEV, order).COL4) < 0)
                            {
                                p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 170) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 170) * Math.PI / 180)));
                                p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 190) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 190) * Math.PI / 180)));
                            }

                            string l1 = Convert.ToString(p1.X);
                            string l2 = Convert.ToString(p1.Y);
                            string l5 = Convert.ToString(p2.X);
                            string l6 = Convert.ToString(p2.Y);
                            XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("polygon") as Polygon;
                            n2.SetAttribute("points", l1 + " " + l2 + "," + l3 + " " + l4 + "," + l5 + " " + l6);
                            n2.SetAttribute("fill-opacity", "1");
                            n2.SetAttribute("layer", la.ID);
                            n2.SetAttribute("flag", "1");
                            n2.SetAttribute("font-size", "6");
                            n2.SetAttribute("style", "fill:#000000");
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);
                            tlVectorControl1.SVGDocument.CurrentElement = n2 as SvgElement;

                        }
                     
                        n1.SetAttribute("font-family", "楷体_GB2312");
                       
                        //tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                        //tlVectorControl1.Operation = ToolOperation.Select;

                        //tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;

                        //RectangleF ttt = ((Polyline)element).GetBounds();

                        //tlVectorControl1.RotateSelection(angel, pStart);
                        //if (Math.Abs(angel) > 90)
                        //    tlVectorControl1.RotateSelection(180, pStart);

                        tlVectorControl1.Refresh();
                    }
                }
            }
            catch (System.Exception ex)
            {
                wFrom.ShowText += "\r\n结果显示出错，请检查数据和图形是否一致\t" + System.DateTime.Now.ToString();
                MessageBox.Show("参数错误，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            wFrom.ShowText += "\r\n结果显示完毕\t " + System.DateTime.Now.ToString();
            tlVectorControl1.SVGDocument.CurrentLayer = (Layer)tlVectorControl1.SVGDocument.Layers[0];
        }
        private void ShowPowerLoss()
        {
            int order = 1;
            try
            {
                XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@flag='" + "1" + "']");

                foreach (XmlNode node in list)
                {
                    SvgElement element = node as SvgElement;
                    if ((element.GetAttribute("textn1id") == null || element.GetAttribute("textn1id") == "") && (element.GetAttribute("textn2id") == null || element.GetAttribute("textn2id") == ""))
                    {
                        tlVectorControl1.SVGDocument.CurrentElement = element;
                        tlVectorControl1.Delete();
                    }
                }
                double yinzi = 0, capability = 0, volt = 0, standvolt = 0, current = 0;
                //PSPDEV benchmark = new PSPDEV();
                //benchmark.Type = "power";
                //benchmark.ProjectID = Itop.Client.MIS.ProgUID;
                //IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
                //if (list3 == null)
                //{
                //    MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                //foreach (PSPDEV dev in list3)
                //{
                //    yinzi = Convert.ToDouble(dev.PowerFactor);
                //    capability = Convert.ToDouble(dev.StandardCurrent);
                //    volt = Convert.ToDouble(dev.StandardVolt);
                //    TLPSPVmin = dev.iV;
                //    TLPSPVmax = dev.jV;
                //    if (dev.PowerFactor == 0)
                //    {
                //        yinzi = 1;
                //    }
                //    if (dev.StandardCurrent == 0)
                //    {
                //        capability = 1;
                //    }
                //    if (dev.StandardVolt == 0)
                //    {
                //        volt = 1;
                //    }
                //    standvolt = volt;
                //    current = capability / (Math.Sqrt(3) * volt);
                //};

                //XmlNodeList useList = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
                //foreach (XmlNode node in useList)
                //{
                //    XmlElement element = node as XmlElement;
                //    //XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                //    string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01'";
                //    IList listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                //    for (int i = 0; i < listMX.Count; i++)
                //    {
                //        PSPDEV elementDEV = (PSPDEV)(listMX[i]);
                //        PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                //        elcDEV.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                //        elcDEV.DeviceSUID = ((PSPDEV)listMX[i]).SUID;
                //        elcDEV = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDEV);
                //        if (elcDEV != null)
                //        {
                //            RectangleF bound = ((IGraph)element).GetBounds();
                //            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                //            XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                //            n1.SetAttribute("x", Convert.ToString(bound.X));
                //            n1.SetAttribute("y", Convert.ToString(bound.Y - i * 20));
                //            n1.SetAttribute("font-size", "6");
                //            n1.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL2).ToString("N2");
                //            Layer la = tlVectorControl1.SVGDocument.GetLayerByID(element.GetAttribute("layer"));
                //            n1.SetAttribute("layer", la.ID);
                //            //MessageBox.Show(Convert.ToString(n1.InnerText));
                //            n1.SetAttribute("flag", "1");
                //            if (Convert.ToDouble(GetColValue(elcDEV, order).COL2) > TLPSPVmax * elementDEV.RateVolt / elementDEV.ReferenceVolt || Convert.ToDouble(GetColValue(elcDEV, order).COL2) < TLPSPVmin * elementDEV.RateVolt / elementDEV.ReferenceVolt)//电压越限，需修改
                //                n1.SetAttribute("stroke", "#FF0000");
                //            if (elementDEV.NodeType == "0")
                //            {

                //                n2.SetAttribute("x", Convert.ToString(bound.X));
                //                n2.SetAttribute("y", Convert.ToString(bound.Y + bound.Height + 20));
                //                if (Convert.ToDouble(elcDEV.COL5) >= 0)
                //                {
                //                    n2.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL4).ToString("N2") + "  + " + "j" + Convert.ToDouble(GetColValue(elcDEV, order).COL5).ToString("N2");
                //                }
                //                else
                //                {
                //                    n2.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL4).ToString("N2") + "  - " + "j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                //                }
                //                n2.SetAttribute("layer", la.ID);
                //                n2.SetAttribute("flag", "1");
                //                n2.SetAttribute("font-size", "6");
                //                double tempi = Convert.ToDouble(GetColValue(elcDEV, order).COL4);
                //                double tempj = Convert.ToDouble(GetColValue(elcDEV, order).COL5);
                //                double temptotal = Math.Sqrt(tempi * tempi + tempj * tempj);
                //                if (temptotal > Convert.ToDouble(elementDEV.Burthen))
                //                {
                //                    n2.SetAttribute("stroke", "#FF0000");
                //                }
                //                tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);

                //            }
                //            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                //            tlVectorControl1.Operation = ToolOperation.Select;
                //            tlVectorControl1.Refresh();
                //        }
                //    }
                //}
                tlVectorControl1.SVGDocument.AcceptChanges = true;
                XmlNodeList polyLineList = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline");
                XmlNodeList layerlist = tlVectorControl1.SVGDocument.GetElementsByTagName("layer");
                Layer layResult;
                bool lb = true;
                foreach (Layer lay in layerlist)
                {
                    if (lay.GetAttribute("label") == "结果显示")
                    {
                        SvgDocument.currentLayer = lay.ID;
                        lb = false;
                    }
                }
                if (lb)
                {
                    SvgDocument.currentLayer = Layer.CreateNew("结果显示", tlVectorControl1.SVGDocument).ID;
                }
                foreach (XmlNode node in polyLineList)
                {
                    XmlElement element = node as XmlElement;
                    PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                    elcDEV.ProjectSUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    elcDEV.DeviceSUID = element.GetAttribute("Deviceid");
                    elcDEV = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDEV);
                    PSPDEV elementDEV = new PSPDEV();
                    if (elcDEV != null)
                    {
                        elementDEV.SUID = elcDEV.DeviceSUID;
                        elementDEV = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", elementDEV);
                    }
                    else
                    {
                        continue;
                    }

                    if (elementDEV != null&&elementDEV.KSwitchStatus=="0")
                    {
                        PointF[] t = ((Polyline)element).Points;
                        PointF[] t2 = ((Polyline)element).FirstTwoPoint; t = t2;

                        PointF midt = new PointF((float)((t2[0].X + t2[1].X) / 2), (float)((t2[0].Y + t2[1].Y) / 2));
                        float angel = 0f;
                        angel = (float)(180 * Math.Atan2((t2[1].Y - t2[0].Y), (t2[1].X - t2[0].X)) / Math.PI);

                        string l3 = Convert.ToString(midt.X);
                        string l4 = Convert.ToString(midt.Y);

                        string tran = ((Polyline)element).Transform.ToString();

                        PointF center = new PointF((float)(t[0].X + (t[1].X - t[0].X) / 2), (float)(t[0].Y + (t[1].Y - t[0].Y) / 2));
                        XmlElement n1 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@textn1id='" + element.GetAttribute("Deviceid") + "']") as XmlElement;
                       // XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                        //XmlElement n3 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;

                        PointF pStart = new PointF(center.X + (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(15 * Math.Cos((angel) * Math.PI / 180)));
                        PointF pStart2 = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));

                        XmlNode firstNodeElement = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + element.GetAttribute("FirstNode") + "']");
                        XmlNode lastNodeElement = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + element.GetAttribute("LastNode") + "']");
                        if (firstNodeElement == null || lastNodeElement == null)
                            continue;
                        if (t2[0].X > ((IGraph)firstNodeElement).CenterPoint.X || t2[0].Y < ((IGraph)firstNodeElement).CenterPoint.Y)
                        {
                            pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                            pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                        }
                        else
                        {
                            pStart = new PointF(center.X - (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                            pStart2 = new PointF(center.X + (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                        }



                        PointF newp1 = new PointF(t[0].X + (t[1].X - t[0].X) / 2 - (float)(15 * Math.Sin(angel)), t[0].Y + (t[1].Y - t[0].Y) / 2 - (float)(15 * Math.Cos(angel)));
                        if (n1 == null)
                        {
                            n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            n1.SetAttribute("x", Convert.ToString(pStart.X));
                            n1.SetAttribute("y", Convert.ToString(pStart.Y));
                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                            n1.SetAttribute("flag", "1");
                            n1.SetAttribute("font-size", "6");
                            n1.SetAttribute("textn1id", element.GetAttribute("Deviceid"));
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            tlVectorControl1.Operation = ToolOperation.Select;

                            tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;

                            RectangleF ttt = ((Polyline)element).GetBounds();

                            tlVectorControl1.RotateSelection(angel, pStart);
                            if (Math.Abs(angel) > 90)
                                tlVectorControl1.RotateSelection(180, pStart);
                        }


                        if ((Convert.ToDouble(GetColValue(elcDEV, order).COL7) + Convert.ToDouble(GetColValue(elcDEV, order).COL11)) * (Convert.ToDouble(GetColValue(elcDEV, order).COL6) + Convert.ToDouble(GetColValue(elcDEV, order).COL10)) >= 0)
                        {
                            n1.InnerText = (Math.Abs((Convert.ToDouble(GetColValue(elcDEV, order).COL6) + Convert.ToDouble(GetColValue(elcDEV, order).COL10)))).ToString("N2") + " + j" + (Math.Abs((Convert.ToDouble(GetColValue(elcDEV, order).COL7) + Convert.ToDouble(GetColValue(elcDEV, order).COL11)))).ToString("N2");
                        }
                        else
                        {
                            n1.InnerText = (Math.Abs((Convert.ToDouble(GetColValue(elcDEV, order).COL6) + Convert.ToDouble(GetColValue(elcDEV, order).COL10)))).ToString("N2") + " - j" + (Math.Abs((Convert.ToDouble(GetColValue(elcDEV, order).COL7) + Convert.ToDouble(GetColValue(elcDEV, order).COL11)))).ToString("N2");
                        }


                        //if (Convert.ToDouble(GetColValue(elcDEV, order).COL14) > (elementDEV.LineChange))//电流越限，需修改。

                        //    n1.SetAttribute("stroke", "#FF0000");


                        PointF p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 10) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 10) * Math.PI / 180)));
                        PointF p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 350) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 350) * Math.PI / 180)));

                        //if ((Convert.ToDouble(GetColValue(elcDEV, order).COL6) + Convert.ToDouble(GetColValue(elcDEV, order).COL10)) < 0)
                        if (Convert.ToDouble(GetColValue(elcDEV, order).COL4) < 0)
                        {
                            p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 170) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 170) * Math.PI / 180)));
                            p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 190) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 190) * Math.PI / 180)));
                        }

                        string l1 = Convert.ToString(p1.X);
                        string l2 = Convert.ToString(p1.Y);
                        string l5 = Convert.ToString(p2.X);
                        string l6 = Convert.ToString(p2.Y);
                        XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("polygon") as Polygon;
                        n2.SetAttribute("points", l1 + " " + l2 + "," + l3 + " " + l4 + "," + l5 + " " + l6);
                        n2.SetAttribute("fill-opacity", "1");
                        n2.SetAttribute("layer", SvgDocument.currentLayer);
                        n2.SetAttribute("flag", "1");
                        n2.SetAttribute("font-size", "6");
                        n2.SetAttribute("style", "fill:#000000");
                        tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);
                        tlVectorControl1.SVGDocument.CurrentElement = n2 as SvgElement;

                        //tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                        //tlVectorControl1.Operation = ToolOperation.Select;

                        //tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;

                        //RectangleF ttt = ((Polyline)element).GetBounds();

                        //tlVectorControl1.RotateSelection(angel, pStart);
                        //if (Math.Abs(angel) > 90)
                        //    tlVectorControl1.RotateSelection(180, pStart);

                        tlVectorControl1.Refresh();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("参数错误，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


        }      


        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "属性")
            {
                UseProperty();
            }
            else if (e.ClickedItem.Text == "不打印节点")
            {
                SvgElement sub = tlVectorControl1.SVGDocument.CurrentElement;
                if (sub!=null)
                {
                    sub.SetAttribute("print", "no");
                    XmlNodeList listText = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@ParentID='" + sub.ID + "']");
                    foreach (XmlNode node in listText)
                    {
                        (node as SvgElement).SetAttribute("print", "no");
                    }
                }
            }
            else if (e.ClickedItem.Text == "打印节点")
            {
                SvgElement sub = tlVectorControl1.SVGDocument.CurrentElement;
                if (sub!=null)
                {
                    sub.SetAttribute("print", "yes");
                    XmlNodeList listText = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@ParentID='" + sub.ID + "']");
                    foreach (XmlNode node in listText)
                    {
                        (node as SvgElement).SetAttribute("print", "yes");
                    }
                }
            }
            else if (e.ClickedItem.Text == "区域打印")
            {
                setTJhide();
                PrintHelper ph = new PrintHelper(tlVectorControl1, mapview);
                ph.blshowflag = false;
                frmPrinter dlg = new frmPrinter();
                dlg.printHelper = ph;
                dlg.ShowDialog();
                setTJshow();
                return;
            }          
        }

       

      
        private void frmTLpspGraphical_Load(object sender, EventArgs e)
        {          
            this.alignButton = this.dotNetBarManager1.GetItem("mAlign") as ButtonItem;
            this.orderButton = this.dotNetBarManager1.GetItem("mOrder") as ButtonItem;
            this.rotateButton = this.dotNetBarManager1.GetItem("mRotate") as ButtonItem;

            if (!base.EditRight)
            {              
                //dotNetBarManager1.Bars["mainmenu"].Enabled = false;
                dotNetBarManager1.Bars["bar6"].Enabled = false;
                //dotNetBarManager1.Bars["bar2"].Enabled = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("mNew").Enabled = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("mSave").Enabled = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("mjxt").Enabled = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem5").Enabled = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem6").Enabled = false;
                //dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").Enabled = false;
                //dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem9").Enabled = false;
                //dotNetBarManager1.Bars["mainmenu"].GetItem("mConvert").Enabled = false;
                //dotNetBarManager1.Bars["mainmenu"].GetItem("Dlqibutt").Enabled = false;
                //dotNetBarManager1.Bars["mainmenu"].GetItem("Idle").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mSelect").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mShapeTransform").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mFreeTransform").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("powerFactor").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mPolyline").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mConnectLine").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mText").Enabled = false;
                //dotNetBarManager1.Bars["bar2"].GetItem("duanlu").Enabled = false;
                //barLeftDockSite.Enabled = false;
                barLeftDockSite.Enabled = false;
            }
            jxtbar2(jxb);

        }

        private void frmTLpspGraphical_Paint(object sender, PaintEventArgs e)
        {
            jxtbar2(jxb);
        }
    }   
}