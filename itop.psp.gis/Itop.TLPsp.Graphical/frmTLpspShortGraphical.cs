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
using shortcir_dll;
using ShortBuscir_dll;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using DevExpress.Utils;
using Itop.TLPsp;
namespace Itop.TLPsp.Graphical
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
    public partial class frmTLpspShortGraphical : FormBase
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

        public frmTLpspShortGraphical()
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
            if (jxt == 1)
            {
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLoss").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCal").Visible = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").ShowSubItems = true;
                dotNetBarManager1.Bars["mainmenu"].GetItem("Dlqibutt").Enabled = true;
                dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = true;
                dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = true;
                dotNetBarManager1.Bars["bar2"].GetItem("Rela").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("Dlqicheck").Visible = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("Idle").Visible = JXTFlat;
                dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = JXTFlat;
                dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = false;

            }
            else if (jxt == 4)
            {
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLoss").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCal").Visible = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").ShowSubItems = true;
                dotNetBarManager1.Bars["mainmenu"].GetItem("Dlqibutt").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("Rela").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("Dlqicheck").Visible = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("Idle").Visible = JXTFlat;
                dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = JXTFlat;
                dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = true;

            }
            else if (jxt == 2)
            {
                try
                {
                    dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = false;
                }
                catch { }

               // dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = false;
                //dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                //dotNetBarManager1.Bars["bar2"].GetItem("Rela").Enabled = false;
                //dotNetBarManager1.Bars["mainmenu"].GetItem("Dlqibutt").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("Dlqicheck").Enabled = true;
                dotNetBarManager1.Bars["bar2"].GetItem("Shortibut").Visible = true;
                dotNetBarManager1.Bars["bar2"].GetItem("ZLcheck").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("Jiaoliucheck").Enabled = true;
                //dotNetBarManager1.Bars["mainmenu"].GetItem("Idle").Visible = JXTFlat;
                //dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = JXTFlat;

            }
            else if (jxt == 3)
            {
                dotNetBarManager1.Bars["bar2"].GetItem("VoltEvaluation").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLoss").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCal").Visible = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").ShowSubItems = true;
                dotNetBarManager1.Bars["mainmenu"].GetItem("Dlqibutt").Enabled = true;
                dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("Rela").Visible = Relaflag;
                dotNetBarManager1.Bars["bar2"].GetItem("Dlqicheck").Visible = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("Idle").Visible = JXTFlat;
                dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = JXTFlat;
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
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLoss").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("PowerLossCal").Visible = false;
                dotNetBarManager1.Bars["mainmenu"].GetItem("Dlqibutt").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("Dlqicheck").Enabled = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mTlpsp").Visible = false;
                dotNetBarManager1.Bars["bar2"].GetItem("mChaoliuResult").Visible = false;
                if (Relaflag)
                {
                    dotNetBarManager1.Bars["bar2"].GetItem("Rela").Visible = true;

                }
                else
                    dotNetBarManager1.Bars["bar2"].GetItem("PSPIdleOptimize").Visible = JXTFlat;
                dotNetBarManager1.Bars["mainmenu"].GetItem("Idle").Visible = JXTFlat;
                dotNetBarManager1.Bars["bar2"].GetItem("Rela").Visible = false;
            }

        }
        public void jxtbar(int jxt)
        {
            if (jxt == 1)
            {


            }
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

            if (rt1 == null) return;
            RectangleF rtf1 = rt1.GetBounds();

            int width = (int)Math.Round(rtf1.Width * tlVectorControl1.ScaleRatio, 0) + 1;
            int height = (int)Math.Round(rtf1.Height * tlVectorControl1.ScaleRatio, 0) + 1;
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
            elementProperty();
        }
        //判断所有的添加情况
        private bool addcheck()         
        {
            
          
                bool flag = true;
                foreach (eleclass el in buscol)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有母线节点没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }                
                flag = true;
                foreach (eleclass el in loadcol)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有负荷没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }
           
               flag = true;
                foreach (eleclass el in trans2col)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有两绕组变压器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }
           
               flag = true;
                foreach (eleclass el in trans3col)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有三绕组变压器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }
                flag = true;
                foreach (eleclass el in cldkcol)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有串联电抗器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }
                flag = true;
                foreach (eleclass el in cldrcol)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有串联电容器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }
            
  
                flag = true;
                foreach (eleclass el in bldrcol)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有并联电容器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }
   
 
                flag = true;
                foreach (eleclass el in bldkcol)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有并联电抗器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }

   
              flag = true;
                foreach (eleclass el in gencol)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有发电机没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }
  
  
                flag = true;
                foreach (eleclass el in MLcol)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有1/2母联开关没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }
 
  
                 flag = true;
                foreach (eleclass el in ML2col)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有2/3母联开关没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }
  
       
               flag = true;
                foreach (eleclass el in HGcol)
                {
                    if (el.selectflag == false)
                    {
                        MessageBox.Show("还有线路互感没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        flag = false;
                        return false;
                    }
                }
 
             return true;
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
                    Duluqi.Visible = false;
                }
                else
                {
                    printToolStripMenuItem.Visible = false;
                    moveMenuItem.Visible = true;
                }
                //if (tlVectorControl1.SVGDocument.CurrentElement
                XmlElement temp = tlVectorControl1.SVGDocument.CurrentElement as XmlElement;
                if (temp.GetAttribute("xlink:href").Contains("motherlinenode"))
                {
                    Ssubstation.Visible = false;
                    Duluqi.Visible = false;
                }
                if (temp.GetAttribute("xlink:href").Contains("Polyline") || temp.GetAttribute("xlink:href").Contains("dynamotorline") || temp.GetAttribute("xlink:href").Contains("gndline") || temp.GetAttribute("xlink:href").Contains("loadline") || temp.GetAttribute("xlink:href").Contains("串联电容电抗器") || temp.GetAttribute("xlink:href").Contains("并联电容电抗器") || temp.GetAttribute("xlink:href").Contains("transformerthirdzu") || temp.GetAttribute("xlink:href").Contains("transformertwozu") || temp.GetAttribute("xlink:href").Contains("1/2母联开关") || temp.GetAttribute("xlink:href").Contains("2/3母联开关"))
                {
                    moveMenuItem.Visible = false;
                    Duluqi.Visible = false;
                    Ssubstation.Visible = false;
                }
            }
            
        }
     
        void tlVectorControl1_LeftClick(object sender, SvgElementSelectedEventArgs e)
        {

        }

        void tlVectorControl1_AddElement(object sender, AddSvgElementEventArgs e)
        {
            XmlElement temp = e.SvgElement as XmlElement;
            intdata(tlVectorControl1.SVGDocument.SvgdataUid);
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("motherlinenode")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("01", tlVectorControl1.SVGDocument.SvgdataUid,this.ProjectUID,buscol);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {
                  
                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "01");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }
                
                foreach (eleclass ele in buscol)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag==true)
                    {
                        MessageBox.Show("已经选择此母线，请重新选择其他的母线！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        buscol.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        buscol.Add(ele1);
                        AddLine(temp, ((PSPDEV)subID).SUID);
                    }

                }
            }
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("transformertwozu")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("02", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID,trans2col);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {

                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "02");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }

                foreach (eleclass ele in trans2col)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                    {
                        MessageBox.Show("已经选择此变压器，请重新选择其他的变压器！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        trans2col.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        trans2col.Add(ele1);
                    }

                }
            }
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("transformerthirdzu")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("03", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID,trans3col);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {

                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "03");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }

                foreach (eleclass ele in trans3col)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                    {
                        MessageBox.Show("已经选择此两绕组变压器，请重新选择其他的两绕组变压器！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        trans3col.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        trans3col.Add(ele1);
                    }

                }
            }
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("loadline")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("12", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID,loadcol);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {

                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "12");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }

                foreach (eleclass ele in loadcol)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                    {
                        MessageBox.Show("已经选择此负荷，请重新选择其他的负荷！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        loadcol.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        loadcol.Add(ele1);
                    }

                }
            }
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("dynamotorline")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("04", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID,gencol);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {

                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "04");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }

                foreach (eleclass ele in gencol)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                    {
                        MessageBox.Show("已经选择此发电厂，请重新选择其他的发电厂！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        gencol.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        gencol.Add(ele1);
                    }

                }
            }
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("串联电抗器")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("10", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID,cldkcol);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {

                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "10");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }

                foreach (eleclass ele in cldkcol)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                    {
                        MessageBox.Show("已经选择此串联电抗器，请重新选择其他的串联电抗器！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        cldkcol.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        cldkcol.Add(ele1);
                    }

                }
            }
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("串联电容器")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("08", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID,cldrcol);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {

                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "08");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }

                foreach (eleclass ele in cldrcol)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                    {
                        MessageBox.Show("已经选择此串联电容器，请重新选择其他的串联电容器！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        cldrcol.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        cldrcol.Add(ele1);
                    }

                }
            }
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("并联电容器")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("09", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID,bldrcol);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {

                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "09");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }

                foreach (eleclass ele in bldrcol)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                    {
                        MessageBox.Show("已经选择此并联电容器，请重新选择其他的并联电容器！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        bldrcol.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        bldrcol.Add(ele1);
                    }

                }
            }
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("并联电抗器")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("11", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID,bldkcol);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {

                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "11");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }

                foreach (eleclass ele in bldkcol)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                    {
                        MessageBox.Show("已经选择此并联电抗器，请重新选择其他的并联电抗器！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        bldkcol.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        bldkcol.Add(ele1);
                    }

                }
            }
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("1/2母联开关")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("13", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID,MLcol);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {

                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "13");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }

                foreach (eleclass ele in MLcol)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                    {
                        MessageBox.Show("已经选择此1/2母联开关，请重新选择其他的1/2母联！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        MLcol.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        MLcol.Add(ele1);
                    }

                }
            }
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("2/3母联开关")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("14", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID,ML2col);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {

                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "14");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }

                foreach (eleclass ele in ML2col)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                    {
                        MessageBox.Show("已经选择此3/2母联开关，请重新选择其他的3/2母联开关！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        ML2col.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        ML2col.Add(ele1);
                    }

                }
            }
            if (temp is Use && (temp.GetAttribute("xlink:href").Contains("线路互感")))
            {
                //frmSubstation dlgSubstation = new frmSubstation();
                //dlgSubstation.ProjectID = this.ProjectUID;
                //dlgSubstation.InitData();
                object subID = DeviceHelper.SelectProjectDevice("15", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID,HGcol);
                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                if (subID != null)
                {

                    temp.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                    temp.SetAttribute("Type", "15");
                    RectangleF t = ((IGraph)temp).GetBounds();
                    n1.SetAttribute("x", (t.X - 10).ToString());
                    n1.SetAttribute("y", (t.Y - 10).ToString());
                    n1.InnerText = ((PSPDEV)subID).Name;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
                else
                {
                    tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                    tlVectorControl1.Delete();
                    tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                    tlVectorControl1.Delete();
                }

                foreach (eleclass ele in HGcol)
                {
                    if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                    {
                        MessageBox.Show("已经选择此线路互感，请重新选择其他的线路互感！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tlVectorControl1.SVGDocument.CurrentElement = temp as SvgElement;
                        tlVectorControl1.Delete();
                        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                        tlVectorControl1.Delete();
                    }
                    else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                    {
                        HGcol.Remove(ele);
                        eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                        HGcol.Add(ele1);
                    }

                }
            }
        }
        protected void AddLine(XmlElement device, string devicSUID)
        {
            string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SUID = '" + devicSUID + "' AND Type = '01'";
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
            string motherlinenode = "01";
            XmlNodeList list2 = tlVectorControl1.SVGDocument.SelectNodes("svg/use[@Type='"+motherlinenode+"']");
            foreach (PSPDEV dev in list)
            {
                foreach (XmlNode node in list2)
                {
                    XmlElement element = node as XmlElement;
                    //XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                    string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND SUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01'";
                    IList list3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon1);
                    foreach (PSPDEV pd in list3)
                    {
                        if (dev.Number != pd.Number)
                        {
                            string strCon2 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Type = '05' AND FirstNode = '" + dev.Number + "' AND LastNode = '" + pd.Number + "'";
                            IList list4 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon2);
                            for (int i = 0; i < list4.Count; i++)
                            {
                                PSPDEV linepsp=list4[i] as PSPDEV;
                                string temp = "";
                                if (i == 0)
                                {
                                    temp = ((IGraph)device).CenterPoint.X.ToString() + " " + ((IGraph)device).CenterPoint.Y.ToString() + "," + ((IGraph)element).CenterPoint.X + " " + ((IGraph)element).CenterPoint.Y.ToString();
                                }
                                else if (OddEven.IsEven(i))
                                {
                                    if (i == 1)
                                    {
                                        temp = (((IGraph)device).CenterPoint.X + tlVectorControl1.ScaleRatio * 10 * i).ToString() + " " + (((IGraph)device).CenterPoint.Y + tlVectorControl1.ScaleRatio * 10 * i).ToString() + "," + (((IGraph)element).CenterPoint.X + tlVectorControl1.ScaleRatio * 10 * i).ToString() + " " + (((IGraph)element).CenterPoint.Y + tlVectorControl1.ScaleRatio * 10 * i).ToString();
                                    }
                                    else
                                    {
                                        temp = (((IGraph)device).CenterPoint.X + tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + " " + (((IGraph)device).CenterPoint.Y + tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + "," + (((IGraph)element).CenterPoint.X + tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + " " + (((IGraph)element).CenterPoint.Y + tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString();
                                    }
                                }
                                else if (OddEven.IsOdd(i))
                                {
                                    temp = (((IGraph)device).CenterPoint.X - tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + " " + (((IGraph)device).CenterPoint.Y - tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + "," + (((IGraph)element).CenterPoint.X - tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + " " + (((IGraph)element).CenterPoint.Y - tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString();
                                }
                                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;

                                n1.SetAttribute("points", temp);
                                n1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                n1.SetAttribute("layer", SvgDocument.currentLayer);
                                n1.SetAttribute("FirstNode", device.GetAttribute("id"));
                                n1.SetAttribute("LastNode", element.GetAttribute("id"));
                                n1.SetAttribute("Deviceid", linepsp.SUID);
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                                tlVectorControl1.ChangeLevel(LevelType.Bottom);
                                //n1.RemoveAttribute("layer");
                                tlVectorControl1.Operation = ToolOperation.Select;
                                tlVectorControl1.SVGDocument.CurrentElement = element as SvgElement;
                            }

                            string strCon3 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Type = '05' AND FirstNode = '" + pd.Number + "' AND LastNode = '" + dev.Number + "'";
                            IList list5 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon3);
                            for (int i = 0; i < list5.Count; i++)
                            {
                                PSPDEV linpsp = list5[i] as PSPDEV;
                                string temp = "";
                                if (i == 0)
                                {
                                    temp = ((IGraph)element).CenterPoint.X.ToString() + " " + ((IGraph)element).CenterPoint.Y.ToString() + "," + ((IGraph)device).CenterPoint.X + " " + ((IGraph)device).CenterPoint.Y.ToString();
                                }
                                else if (OddEven.IsEven(i))
                                {
                                    if (i == 1)
                                    {
                                        temp = (((IGraph)element).CenterPoint.X + tlVectorControl1.ScaleRatio * 10 * i).ToString() + " " + (((IGraph)element).CenterPoint.Y + tlVectorControl1.ScaleRatio * 10 * i).ToString() + "," + (((IGraph)device).CenterPoint.X + tlVectorControl1.ScaleRatio * 10 * i).ToString() + " " + (((IGraph)device).CenterPoint.Y + tlVectorControl1.ScaleRatio * 10 * i).ToString();
                                    }
                                    else
                                    {
                                        temp = (((IGraph)element).CenterPoint.X + tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + " " + (((IGraph)element).CenterPoint.Y + tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + "," + (((IGraph)device).CenterPoint.X + tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + " " + (((IGraph)device).CenterPoint.Y + tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString();
                                    }
                                }
                                else if (OddEven.IsOdd(i))
                                {
                                    temp = (((IGraph)element).CenterPoint.X - tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + " " + (((IGraph)element).CenterPoint.Y - tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + "," + (((IGraph)device).CenterPoint.X - tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString() + " " + (((IGraph)device).CenterPoint.Y - tlVectorControl1.ScaleRatio * 10 * (i / 2)).ToString();
                                }
                                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                                n1.SetAttribute("points", temp);
                                n1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                n1.SetAttribute("layer", SvgDocument.currentLayer);
                                n1.SetAttribute("Deviceid", linpsp.SUID);
                                n1.SetAttribute("Type", "05");
                                n1.SetAttribute("FirstNode", element.GetAttribute("id"));
                                n1.SetAttribute("LastNode", device.GetAttribute("id"));
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
        private void Allshortcheck(int caozuoi)  //根据操作的次序依次显示

        {
            if (!addcheck())
            {
                return;
            }
            ElectricShorti elc = new ElectricShorti();
            if (!elc.CheckDL(tlVectorControl1.SVGDocument.SvgdataUid,this.ProjectUID,100))
            {
                return;
            }
            Dictionary<int, double> nodeshorti = new Dictionary<int, double>();      //记录母线有没有进行过短路 
            KeyValuePair<int, double> maxshorti = new KeyValuePair<int, double>(); //取出短路的最大短路电流

            string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" +tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
            PSPDEV pspDev = new PSPDEV();
            IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);

            con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid+ "'AND PSPDEV.type='05'AND PSPDEV.KSwitchStatus = '0'order by PSPDEV.number";
            PSPDEV psp = new PSPDEV();
            IList list2 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);

            shortbuscir shortCutCal = new shortbuscir(0);
            for (int i = 0; i < list1.Count; i++)
            {
                pspDev = list1[i] as PSPDEV;
                bool flag = false;
                string dlr = null;
                for (int j = 0; j < list2.Count; j++)
                {
                    psp = list2[j] as PSPDEV;
                    con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" +this.ProjectUID + "'" + "AND Type='07'";
                    IList listiswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                    con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + this.ProjectUID + "'" + "AND Type='07'";
                    IList listjswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                    PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                    PSPDEV pspjswitch = (PSPDEV)listjswitch[0];

                    if (pspDev.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                    {

                        flag = true;
                        dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0 " + " " + "0 ";

                    }
                    if (pspDev.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                    {
                        flag = true;
                        dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1 " + " " + "0 ";
                    }
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\fault.txt"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\fault.txt");
                    }
                    if (flag)
                    {
                        FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                        StreamWriter str11 = new StreamWriter(VK);
                        str11.Write(dlr);
                        str11.Close();
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                        {
                            File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                        }
                       // shortcir shortCutCal = new shortcir();
                        shortCutCal.Show_shortcir(0,0,1);
                        //bool matrixflag=true;                //用来判断是否导纳矩阵的逆矩阵是否存在逆矩阵

                        string matrixstr = null;
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                        {
                            matrixstr = "正序导纳矩阵";
                            // matrixflag = false;
                        }

                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                        {
                            // matrixflag = false;
                            matrixstr += "负序导纳矩阵";
                        }

                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                        {
                            //matrixflag = false;
                            matrixstr += "零序导纳矩阵";
                        }
                        if (matrixstr != null)
                        {
                            MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                        {
                        }
                        else
                        {
                            return;
                        }

                        FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                        StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                        string strLineGU;
                        string[] arrayGU;
                        char[] charSplitGU = new char[] { ' ' };

                        while ((strLineGU = readLineGU.ReadLine()) != null)
                        {

                            while ((strLineGU = readLineGU.ReadLine()) != null)
                            {
                                arrayGU = strLineGU.Split(charSplitGU);
                                string[] shorti = new string[4];
                                shorti.Initialize();
                                int m = 0;
                                foreach (string str in arrayGU)
                                {

                                    if (str != "")
                                    {

                                        shorti[m++] = str.ToString();

                                    }
                                }

                                nodeshorti[pspDev.Number] = Convert.ToDouble(shorti[3]) * 100 / (Math.Sqrt(3) * pspDev.ReferenceVolt);
                            }
                        }
                        readLineGU.Close();
                        break;                 //跳出本循环 进行母线的另外一个母线短路

                    }
                    if (!flag)
                        continue;
                    //写入错误中

                }
                //如果在一般线路中没有则在两绕组中进行
                if (!flag)
                {
                    con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.type='02'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                    IList list3 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                    for (int j = 0; j < list3.Count; j++)
                    {
                        dlr = null;
                        psp = list3[j] as PSPDEV;
                        PSPDEV devFirst = new PSPDEV();

                        con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + this.ProjectUID + "'" + "AND Type='01'";
                        devFirst = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                        PSPDEV devLast = new PSPDEV();


                        con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + this.ProjectUID + "'" + "AND Type='01'";
                        devLast = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                        con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + this.ProjectUID + "'" + "AND Type='07'";
                        IList listiswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + this.ProjectUID + "'" + "AND Type='07'";
                        IList listjswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                        PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                        if (pspDev.Number == devFirst.Number && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                        {

                            flag = true;
                            dlr = "0" + " " + devFirst.Number + " " + devLast.Number + " " + psp.Number + " " + "0" + " " + "0";

                        }
                        if (pspDev.Number == devLast.Number && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                        {
                            flag = true;
                            dlr = "0" + " " + devFirst.Number + " " + devLast.Number + " " + psp.Number + " " + "1" + " " + "0";
                        }
                        if (flag)
                        {
                            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                            StreamWriter str11 = new StreamWriter(VK);
                            str11.Write(dlr);
                            str11.Close();
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                            }
                           // shortcir shortCutCal = new shortcir();
                            shortCutCal.Show_shortcir(0,0,1);
                            //bool matrixflag=true;                //用来判断是否导纳矩阵的逆矩阵是否存在逆矩阵

                            string matrixstr = null;
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                            {
                                matrixstr = "正序导纳矩阵";
                                // matrixflag = false;
                            }

                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                            {
                                // matrixflag = false;
                                matrixstr += "负序导纳矩阵";
                            }

                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                            {
                                //matrixflag = false;
                                matrixstr += "零序导纳矩阵";
                            }
                            if (matrixstr != null)
                            {
                                MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                            {
                            }
                            else
                            {
                                return;
                            }

                            FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                            StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                            string strLineGU;
                            string[] arrayGU;
                            char[] charSplitGU = new char[] { ' ' };

                            while ((strLineGU = readLineGU.ReadLine()) != null)
                            {

                                while ((strLineGU = readLineGU.ReadLine()) != null)
                                {
                                    arrayGU = strLineGU.Split(charSplitGU);
                                    string[] shorti = new string[4];
                                    shorti.Initialize();
                                    int m = 0;
                                    foreach (string str in arrayGU)
                                    {

                                        if (str != "")
                                        {

                                            shorti[m++] = str.ToString();

                                        }
                                    }

                                    nodeshorti[pspDev.Number] = Convert.ToDouble(shorti[3]) * 100 / (Math.Sqrt(3) * pspDev.ReferenceVolt);
                                }
                            }
                            readLineGU.Close();
                            break;                 //跳出本循环 进行母线的另外一个母线短路

                        }
                        if (!flag)
                            continue;
                        //写入错误中

                    }
                }
                if (!flag)
                {
                    con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.type='03'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                    IList list4 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                    for (int j = 0; j < list4.Count; j++)
                    {
                        dlr = null;
                        psp = list4[j] as PSPDEV;
                        PSPDEV devINode = new PSPDEV();

                        con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + this.ProjectUID+ "'" + "AND Type='01'";
                        devINode = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                        PSPDEV devJNode = new PSPDEV();

                        con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + this.ProjectUID + "'" + "AND Type='01'";
                        devJNode = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                        PSPDEV devKNode = new PSPDEV();

                        con = " WHERE Name='" + psp.KName + "' AND ProjectID = '" + this.ProjectUID + "'" + "AND Type='01'";
                        devKNode = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);

                        con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + this.ProjectUID + "'" + "AND Type='07'";
                        IList listiswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + this.ProjectUID + "'" + "AND Type='07'";
                        IList listjswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        con = " WHERE Name='" + psp.HuganLine1 + "' AND ProjectID = '" + this.ProjectUID + "'" + "AND Type='07'";
                        IList listkswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                        PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                        PSPDEV pspkswitch = (PSPDEV)listkswitch[0];
                        if (pspDev.Number == devINode.Number && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                        {

                            flag = true;
                            dlr = "0" + " " + devINode.Number + " " + devJNode.Number + " " + psp.Number + " " + "0" + " " + "0";

                        }
                        if (pspDev.Number == devJNode.Number && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                        {
                            flag = true;
                            dlr = "0" + " " + devINode.Number + " " + devJNode.Number + " " + psp.Number + " " + "1" + " " + "0";
                        }
                        if (pspDev.Number == devKNode.Number && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                        {
                            flag = true;
                            dlr = "0" + " " + devINode.Number + " " + devKNode.Number + " " + psp.Number + " " + "1" + " " + "0";
                        }


                        if (flag)
                        {
                            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                            StreamWriter str11 = new StreamWriter(VK);
                            str11.Write(dlr);
                            str11.Close();
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt");
                            }
                          //  shortcir shortCutCal = new shortcir();
                            shortCutCal.Show_shortcir(0,0,1);
                            //bool matrixflag=true;                //用来判断是否导纳矩阵的逆矩阵是否存在逆矩阵

                            string matrixstr = null;
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                            {
                                matrixstr = "正序导纳矩阵";
                                // matrixflag = false;
                            }

                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                            {
                                // matrixflag = false;
                                matrixstr += "负序导纳矩阵";
                            }

                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                            {
                                //matrixflag = false;
                                matrixstr += "零序导纳矩阵";
                            }
                            if (matrixstr != null)
                            {
                                MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                            {
                            }
                            else
                            {
                                return;
                            }

                            FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                            StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                            string strLineGU;
                            string[] arrayGU;
                            char[] charSplitGU = new char[] { ' ' };

                            while ((strLineGU = readLineGU.ReadLine()) != null)
                            {

                                while ((strLineGU = readLineGU.ReadLine()) != null)
                                {
                                    arrayGU = strLineGU.Split(charSplitGU);
                                    string[] shorti = new string[4];
                                    shorti.Initialize();
                                    int m = 0;
                                    foreach (string str in arrayGU)
                                    {

                                        if (str != "")
                                        {

                                            shorti[m++] = str.ToString();

                                        }
                                    }

                                    nodeshorti[pspDev.Number] = Convert.ToDouble(shorti[3]) * 100 / (Math.Sqrt(3) * pspDev.ReferenceVolt);
                                }
                            }
                            readLineGU.Close();
                            break;                 //跳出本循环 进行母线的另外一个母线短路

                        }
                        if (!flag)
                            continue;
                        //写入错误中

                    }
                }
            }
            //找出短路电流最大的值

            //maxshorti.Key = 1;
            //maxshorti.Value = nodeshorti[1];
            foreach (KeyValuePair<int, double> keyvalue in nodeshorti)
            {
                if (keyvalue.Value > maxshorti.Value)
                {
                    maxshorti = keyvalue;
                }
            }

            //首先取出断路器 判断它的母线在不在 如果不在就将其删除 然后与额定电压进行比较 
            con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
            IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
             
            for (int i = 0; i < list.Count; i++)
            {
                bool flag = false;
                pspDev = list[i] as PSPDEV;
                for (int j = 0; j < list1.Count; j++)
                {
                    psp = list1[j] as PSPDEV;
                    if (pspDev.IName == psp.Name)
                        flag = true;

                }
                if (!flag)
                {
                    Services.BaseService.Delete<PSPDEV>(pspDev);
                }
            }
            con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
            list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            for (int i = 0; i < list.Count; i++)
            {
                pspDev = list[i] as PSPDEV;
                pspDev.HuganLine3 = "";
                pspDev.KName = "";
                if (pspDev.KSwitchStatus == "0")
                {
                    pspDev.OutP = maxshorti.Value;
                    if (maxshorti.Value > pspDev.HuganTQ1)
                    {
                        pspDev.HuganLine3 = "不合格";
                    }
                    else
                    {
                        pspDev.HuganLine3 = "合格";
                    }
                    pspDev.HuganLine4 = "";
                    if (pspDev.HuganLine3 == "合格")
                    {
                        pspDev.KName = "合格";
                    }
                    else
                        pspDev.KName = "不合格";
                }

                Services.BaseService.Update<PSPDEV>(pspDev);
            }
            switch (caozuoi)
            {
                case 1:           //全部短路检验

                    {
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        pspDev.Type = "06";
                         DlqiCheckform dlqicheckform = new DlqiCheckform(pspDev);
                        dlqicheckform.getusercltr.gridView.GroupPanelText = "断路器开断能力评估初步结果表";
                        dlqicheckform.ShowDialog();
                        break;
                    }
                case 2:             //最大短路检验        
                    {
                        con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                        list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        for (int i = 0; i < list.Count; i++)
                        {
                            pspDev = list[i] as PSPDEV;
                            if (pspDev.KSwitchStatus == "0")
                            {
                               con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid+ "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0'AND PSPDEV.Name='"+pspDev.IName+"'";
                               
                               IList list4 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);      
                                psp = list4[0] as PSPDEV;
                                try
                                {
                                    pspDev.OutQ = nodeshorti[psp.Number];
                                    if (pspDev.HuganLine3 == "不合格")
                                    {
                                        if (pspDev.OutQ <= pspDev.HuganTQ1)
                                        {
                                            pspDev.HuganLine3 = "合格";
                                        }
                                    }
                                    pspDev.HuganLine4 = "";

                                    if (pspDev.HuganLine3 == "合格")
                                    {
                                        pspDev.KName = "合格";
                                    }
                                    else
                                        pspDev.KName = "不合格";
                                    Services.BaseService.Update<PSPDEV>(pspDev);
                                }
                                catch (System.Exception ex)
                                {
                                    MessageBox.Show("短路数据不完整");
                                }
                            }


                        }
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        pspDev.Type = "06";
                        DlqiCheckform dlqicheckform = new DlqiCheckform(pspDev);
                        dlqicheckform.getusercltr.gridView.GroupPanelText = "最大短路校核结果表";
                        dlqicheckform.ShowDialog();
                        break;
                    }
                case 3:          //断路器直流检验

                    {
                        con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.type='06'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                        list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                        for (int i = 0; i < list.Count; i++)
                        {
                            pspDev = list[i] as PSPDEV;
                            if (pspDev.KSwitchStatus == "0")
                            {
                                double tx = 0.0;
                                if (pspDev.HuganLine2 == "自脱扣断路器")
                                {
                                    tx = 0.0;
                                }
                                else if (pspDev.HuganLine2 == "辅助动力脱扣的断路器")
                                {
                                    tx = 10;
                                }
                                pspDev.HuganTQ4 = (pspDev.OutP / pspDev.HuganTQ1) * Math.Exp((-pspDev.HuganTQ2 - tx) / 45) * 100;
                                pspDev.HuganTQ5 = (pspDev.OutQ / pspDev.HuganTQ1) * Math.Exp((-pspDev.HuganTQ2 - tx) / 45) * 100;
                                if (pspDev.HuganTQ3 >= pspDev.HuganTQ4)
                                {
                                    pspDev.HuganLine4 = "合格";
                                }
                                if (pspDev.HuganTQ3 >= pspDev.HuganTQ5)
                                {
                                    pspDev.HuganLine4 = "合格";
                                }
                                else if (pspDev.HuganTQ3 < pspDev.HuganTQ5)
                                {
                                    pspDev.HuganLine4 = "不合格";
                                }
                                if (pspDev.HuganLine3 == "合格" && pspDev.HuganLine4 == "合格")
                                {
                                    pspDev.KName = "合格";
                                }
                                else
                                {
                                    pspDev.KName = "不合格";
                                }

                                Services.BaseService.Update<PSPDEV>(pspDev);
                            }

                        }
                        pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        pspDev.Type = "06";
                        DlqiCheckform dlqicheckform = new DlqiCheckform(pspDev);
                        dlqicheckform.getusercltr.gridView.GroupPanelText = "断路器开端能力最终评估表";
                        dlqicheckform.ShowDialog();
                        break;
                    }

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
        }
        CustomOperation csOperation = CustomOperation.OP_Default;
        public void RelStart()
        {


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
        public void NewFile(bool type, DialogResult result)
        {
            tlVectorControl1.NewFile();
            LoadShape("symbol20.xml");
            jxtbar(1);
            tlVectorControl1.SVGDocument.SvgdataUid = Guid.NewGuid().ToString();
            SvgDocument.currentLayer = Layer.CreateNew("默认层", tlVectorControl1.SVGDocument).ID;
            tlVectorControl1.IsModified = false;
        }

        public void NewFile(bool type)
        {

        }
        public void NewFile(bool type, string str)
        {
            OpenProject op = new OpenProject();
            op.ProjectID = this.ProjectUID;
            op.Initdata(true);
            if (op.ShowDialog() == DialogResult.OK)
            {
                if (op.FileSUID != null)
                {
                    Open(op.FileSUID);
                    intdata(op.FileSUID);
                }
                this.Show();
                jxtbar2(2);
                LoadShape("symbol23.xml");

            }
            //tlVectorControl1.NewFile();
            //tlVectorControl1.SVGDocument.SvgdataUid = Guid.NewGuid().ToString();
            //SvgDocument.currentLayer = Layer.CreateNew("默认层", tlVectorControl1.SVGDocument).ID;
            tlVectorControl1.IsModified = false;
        }
        //此操作获得项目中的所有的短路计算元件
        private IList listMX = null;
        private IList listXL = null;
        private IList listBYQ2 = null;
        private IList listBYQ3 = null;
        private IList listGen = null;
        private IList listCLDR = null;
        private IList listBlDR = null;
        private IList listCLDK = null;
        private IList listBLDK = null;
        private IList listFH = null;
        private IList listML = null;
        private IList listML2 = null;
        private IList listHG = null;
        private List<eleclass> buscol = new List<eleclass>();
        private List<eleclass> linecol = new List<eleclass>();
        private List<eleclass> trans2col = new List<eleclass>();
        private List<eleclass> trans3col = new List<eleclass>();
        private List<eleclass> gencol = new List<eleclass>();
        private List<eleclass> cldrcol = new List<eleclass>();
        private List<eleclass> bldrcol = new List<eleclass>();
        private List<eleclass> cldkcol = new List<eleclass>();
        private List<eleclass> bldkcol = new List<eleclass>();
        private List<eleclass> loadcol = new List<eleclass>();
        private List<eleclass> MLcol = new List<eleclass>();
        private List<eleclass> ML2col = new List<eleclass>();
        private List<eleclass> HGcol = new List<eleclass>();
        private void intdata(string filesuid)
        {
            buscol.Clear();
            linecol.Clear();
            trans2col.Clear();
            trans3col.Clear();
            gencol.Clear();
            cldrcol.Clear();
            bldrcol.Clear();
            bldkcol.Clear();
            cldkcol.Clear();
            loadcol.Clear();
            ML2col.Clear();
            MLcol.Clear();
            HGcol.Clear();
            string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + filesuid + "'";
            string strCon2 = null;
            string strCon = null;
            {
                strCon2 = "AND PSPDEV.Type = '01' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '05' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listXL = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '02' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listBYQ2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '03' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listBYQ3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '04' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listGen = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '08' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listCLDR = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '09' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listBlDR = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '10' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listCLDK = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '11' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listBLDK = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '12' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listFH = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '13' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listML = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '14' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listML2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                strCon2 = "AND PSPDEV.Type = '15' ORDER BY PSPDEV.Number";
                strCon = strCon1 + strCon2;
                listHG = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
            }
            foreach (PSPDEV dev in listMX)
            {
                if (dev.KSwitchStatus=="0")
                {
                    XmlNode element = tlVectorControl1.SVGDocument.SelectSingleNode("//*[@Deviceid='" + dev.SUID + "']");
                    bool selectflag = false;
                    if (element!=null)
                    {
                        selectflag=true;
                    }
                    eleclass li = new eleclass(dev.Name, dev.SUID, dev.Type, selectflag);
                    buscol.Add(li);
                }
            }
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
            foreach (PSPDEV dev in listBYQ2)
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
                    trans2col.Add(li);
                }
            }
            foreach (PSPDEV dev in listBYQ3)
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
                    trans3col.Add(li);
                }
            }
            foreach (PSPDEV dev in listCLDR)
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
                    cldrcol.Add(li);
                }
            }
            foreach (PSPDEV dev in listBlDR)
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
                    bldrcol.Add(li);
                }
            }
            foreach (PSPDEV dev in listCLDK)
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
                    cldkcol.Add(li);
                }
            }
            foreach (PSPDEV dev in listBLDK)
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
                    bldkcol.Add(li);
                }
            }
            foreach (PSPDEV dev in listGen)
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
                    gencol.Add(li);
                }
            }
            foreach (PSPDEV dev in listFH)
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
                    loadcol.Add(li);
                }
            }
            foreach (PSPDEV dev in listML)
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
                    MLcol.Add(li);
                }
            }
            foreach (PSPDEV dev in listML2)
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
                    ML2col.Add(li);
                }
            }
            foreach (PSPDEV dev in listHG)
            {

                XmlNode element = tlVectorControl1.SVGDocument.SelectSingleNode("//*[@Deviceid='" + dev.SUID + "']");
                bool selectflag = false;
                if (element != null)
                {
                    selectflag = true;
                }
                eleclass li = new eleclass(dev.Name, dev.SUID, dev.Type, selectflag);
                    HGcol.Add(li);
                
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

                        //NewFile(fileType, this.Text);
                        frmNewProject frmprojectDLG = new frmNewProject();
                        frmprojectDLG.Name = "";
                        frmprojectDLG.FileType = "短路";
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
                                this.Text = frmname + "-" + pd.Name;
                            }
                            this.Show();
                            jxtbar2(2);
                            LoadShape("symbol23.xml");
                        }                       
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
                        OpenProject op = new OpenProject();
                        op.ProjectID = this.ProjectUID;
                        op.Initdata(true);
                        if (op.ShowDialog() == DialogResult.OK)
                        {
                            if (op.FileSUID != null)
                            {
                                Open(op.FileSUID);
                                intdata(op.FileSUID);
                                this.Text = frmname + "-" + op.FileName;
                            }
                            this.Show();
                            jxtbar2(2);
                            LoadShape("symbol23.xml");
                        }
                        break;
                    case "btExSymbol":
                        tlVectorControl1.ExportSymbol();
                        break;
                    case "mjxt"://导入接线图



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
                        printToolStripMenuItem.Visible = true;
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

                    case "deviceparam":
                        frmProjectManager frmPdlg = new frmProjectManager();
                        frmPdlg.SetMode(tlVectorControl1.SVGDocument.SvgdataUid);
                        frmPdlg.ShowDialog();
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
                    case "mPolyline":
                        tlVectorControl1.Operation = ToolOperation.PolyLine;

                        break;
                    case "mConnectLine":
                      tlVectorControl1.Operation = ToolOperation.ConnectLine;
                        //tlVectorControl1.Operation = ToolOperation.ConnectLine_Polyline;
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
                    case "niula":
                        break;
                    case "pq":

                        break;

                    case "GaussSeidel":

                        break;
                    case "PowerLossCal":

                        break;
                    case "N_RZYz":

                        break;
                    case "WebRela":                        //进行网络N-1检验



                        break;
                    case "TransRela":                       //进行变压器N-1检验


                        break;
                    case "Shortibut":
                        if (!addcheck())  //检验是不是还有元件需要添加

                            return;
                        ElectricShorti elc = new ElectricShorti();
                       string strID =tlVectorControl1.SVGDocument.SvgdataUid;
                        //elc.CheckDL(strID, 100);
                        int n3 = 0;
                        ShortTform shorttype = new ShortTform();
                        frnReport wduanlu = new frnReport();
                        wduanlu.Owner = this;
                        wduanlu.Text = this.Text;
                        wduanlu.Show();
                        wduanlu.ShowText += "进行相关设置\t" + System.DateTime.Now.ToString();
                        shorttype.ShowDialog();
                        if (shorttype.DialogResult == DialogResult.OK)
                        {
                            switch (shorttype.DuanluType)
                            {
                                case "单相接地":
                                    n3 = 1;
                                    break;

                                case "两相接地":
                                    n3 = 3;
                                    break;
                                case "两相故障":
                                    n3 = 2;
                                    break;
                                case "三相故障":
                                    n3 = 0;
                                    break;

                            }
                            elc.OutType = shorttype.Mathindex;
                        }
                        //elc.P1 = strID; elc.P2 = this.ProjectUID; elc.P3 = n3; elc.P4 = 100;
                        //Thread wait = new Thread(new ThreadStart(elc.temp));
                        //wait.Start();
                       // WaitDialogForm wait = null;
                        try
                        {
                            //wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                            elc.AllShort(strID, this.ProjectUID, n3, 100,wduanlu);
                           // wait.Close();
                        }
                        catch (Exception exc)
                        {
                            Debug.Fail(exc.Message);
                            HandleException.TryCatch(exc);
                            //wait.Close();
                            wduanlu.ShowText += "\r\n短路计算失败" + System.DateTime.Now.ToString();
                            return;
                        }
                       
                        break;
                    case "ZLcheck":

                       elc = new ElectricShorti();

                        strID = tlVectorControl1.SVGDocument.SvgdataUid;
                        WaitDialogForm wait = null;
                        try
                        {
                            wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                            wait.Close();
                            elc.Allshortcheck(strID, this.ProjectUID, 100, 3);

                        }
                        catch (Exception exc)
                        {
                            Debug.Fail(exc.Message);
                            Itop.Client.Common.HandleException.TryCatch(exc);
                            wait.Close();
                            return;
                        }

                       // Allshortcheck(3);
                        dotNetBarManager1.Bars["bar2"].GetItem("ZLcheck").Enabled = false;
                        dotNetBarManager1.Bars["bar2"].GetItem("Jiaoliucheck").Enabled = true;
                        //dotNetBarManager1.Bars["bar2"].GetItem("DLqiOutResult").Enabled =true;
                        break;
                    case "Jiaoliucheck":
                       elc = new ElectricShorti();


                        strID = tlVectorControl1.SVGDocument.SvgdataUid;
                     wait = null;
                        try
                        {
                            wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                            wait.Close();
                            elc.Allshortcheck(strID, this.ProjectUID, 100, 2);

                        }
                        catch (Exception exc)
                        {
                            Debug.Fail(exc.Message);
                            Itop.Client.Common.HandleException.TryCatch(exc);
                            wait.Close();
                            return;


                        }
                       // Allshortcheck(2);
                        dotNetBarManager1.Bars["bar2"].GetItem("Jiaoliucheck").Enabled = false;
                        dotNetBarManager1.Bars["bar2"].GetItem("ZLcheck").Enabled = true;
                        break;
                    case "dd":
                        //SubPrint = true;
                        tlVectorControl1.Operation = ToolOperation.InterEnclosurePrint;
                        break;

                    case "NiulaResult":

                        break;
                    case "GaussSeidelResult":

                        break;
                    case "N_RZYzResult":

                        break;
                    case "VoltEvaluation":
                        break;
                    case "PowerLoss":


                        break;
                    case "ZLPResult1":


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
                                    PSPDEV pspDev = new PSPDEV();
                                    pspDev.EleID = element.GetAttribute("id");
                                    pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
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
                    case "PSPIdleOptimize":
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
            if (ps.Length > 0)
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
            NewFile(fileType, DialogResult.Ignore);
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.ContextMenuStrip = contextMenuStrip1;
        }
        private string frmname = "";
        public void ShortCutStart()
        {
            this.Show();
            this.Text = "短路计算";
            LoadShape("symbol23.xml");
            JXTFlat = false;
            jxtbar2(2);
            fileType = false;
            NewFile(fileType, "短路计算");
            // NewFile(fileType, DialogResult.Ignore);
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.ContextMenuStrip = contextMenuStrip1;
            frmname = this.Text;

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
                    PSP_ELCPROJECT pspDir = new PSP_ELCPROJECT();
                    pspDir.ID = svg.SUID;
                    pspDir.ProjectID = this.ProjectUID;
                    pspDir.Name = tlVectorControl1.SVGDocument.FileName;
                    if (fileType == true)
                    {
                        pspDir.FileType = "潮流";
                    }
                    else
                    {
                        pspDir.FileType = "短路";
                    }
                    Services.BaseService.Update<PSP_ELCPROJECT>(pspDir);
                }
                else
                {
                    svg.SUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    svg.FILENAME = tlVectorControl1.SVGDocument.FileName;
                    svg.SVGDATA = tlVectorControl1.SVGDocument.OuterXml;
                    Services.BaseService.Create<SVGFILE>(svg);
                    PSP_ELCPROJECT pspDir = new PSP_ELCPROJECT();
                    pspDir.ID = svg.SUID;
                    pspDir.ProjectID = this.ProjectUID;
                    pspDir.Name = tlVectorControl1.SVGDocument.FileName;
                    if (fileType == true)
                    {
                        pspDir.FileType = "潮流";
                    }
                    else
                    {
                        pspDir.FileType = "短路";
                    }
                    Services.BaseService.Update<PSP_ELCPROJECT>(pspDir);
                }
            }
            else
            {
                svg.SUID = Guid.NewGuid().ToString();
                svg.FILENAME = tlVectorControl1.SVGDocument.FileName;
                svg.SVGDATA = tlVectorControl1.SVGDocument.OuterXml;
                Services.BaseService.Create<SVGFILE>(svg);
                tlVectorControl1.SVGDocument.SvgdataUid = svg.SUID;
                PSP_ELCPROJECT pspDir = new PSP_ELCPROJECT();
                pspDir.ID = svg.SUID;
                pspDir.ProjectID = this.ProjectUID;
                pspDir.Name = tlVectorControl1.SVGDocument.FileName;
                if (fileType == true)
                {
                    pspDir.FileType = "潮流";
                }
                else
                {
                    pspDir.FileType = "短路";
                }
                Services.BaseService.Create<PSP_ELCPROJECT>(pspDir);
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
                tlVectorControl1.SVGDocument.SvgdataUid = SVGUID;
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
                    else
                    {
                        PSP_ELCPROJECT pr = new PSP_ELCPROJECT();
                        pr.ID= _SvgUID;
                        pr = (PSP_ELCPROJECT)Services.BaseService.GetObject("SelectPSP_ELCPROJECTByKey", pr);
                        svgFile.FILENAME = pr.Name;
                        svgFile.SUID = _SvgUID;
                        Services.BaseService.Create<SVGFILE>(svgFile);
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

                this.Text = document.FileName;
                if (document.RootElement == null)
                {
                    tlVectorControl1.NewFile();
                    tlVectorControl1.SVGDocument.SvgdataUid = _SvgUID;
                    SvgDocument.currentLayer = Layer.CreateNew("默认层", tlVectorControl1.SVGDocument).ID;
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void frmTLpspShortGraphical_FormClosing(object sender, FormClosingEventArgs e)
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
        private bool addcheck(frnReport wForm)
       {
           bool flag = true;
           foreach (eleclass el in buscol)
           {
               if (el.selectflag == false)
               {
                   wForm.ShowText += "\r\n还有母线节点没有添加，请继续添加！" + System.DateTime.Now.ToString();
                  // MessageBox.Show("还有母线节点没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }
           flag = true;
           foreach (eleclass el in loadcol)
           {
               if (el.selectflag == false)
               {
                   wForm.ShowText += "\r\n还有负荷没有添加，请继续添加！" + System.DateTime.Now.ToString();
                   //MessageBox.Show("还有负荷没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }

           flag = true;
           foreach (eleclass el in trans2col)
           {
               if (el.selectflag == false)
               {
                   wForm.ShowText += "\r\n还有两绕组变压器没有添加，请继续添加！" + System.DateTime.Now.ToString();
                  // MessageBox.Show("还有两绕组变压器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }

           flag = true;
           foreach (eleclass el in trans3col)
           {
               if (el.selectflag == false)
               {
                   wForm.ShowText += "\r\n还有三绕组变压器没有添加，请继续添加！" + System.DateTime.Now.ToString();
                   //MessageBox.Show("还有三绕组变压器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }
           flag = true;
           foreach (eleclass el in cldkcol)
           {
               if (el.selectflag == false)
               {
                   MessageBox.Show("还有串联电抗器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }
           flag = true;
           foreach (eleclass el in cldrcol)
           {
               if (el.selectflag == false)
               {
                   wForm.ShowText += "\r\n还有串联电容器没有添加，请继续添加！" + System.DateTime.Now.ToString();
                   //MessageBox.Show("还有串联电容器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }


           flag = true;
           foreach (eleclass el in bldrcol)
           {
               if (el.selectflag == false)
               {
                   wForm.ShowText += "\r\n还有并联电容器没有添加，请继续添加！" + System.DateTime.Now.ToString();
                   //MessageBox.Show("还有并联电容器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }


           flag = true;
           foreach (eleclass el in bldkcol)
           {
               if (el.selectflag == false)
               {
                   wForm.ShowText += "\r\n还有并联电抗器没有添加，请继续添加！" + System.DateTime.Now.ToString();
                   //MessageBox.Show("还有并联电抗器没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }


           flag = true;
           foreach (eleclass el in gencol)
           {
               if (el.selectflag == false)
               {
                   wForm.ShowText += "\r\n还有发电机没有添加，请继续添加！" + System.DateTime.Now.ToString();
                  // MessageBox.Show("还有发电机没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }


           flag = true;
           foreach (eleclass el in MLcol)
           {
               if (el.selectflag == false)
               {
                   wForm.ShowText += "\r\n还有1/2母联开关没有添加，请继续添加！" + System.DateTime.Now.ToString();
                   //MessageBox.Show("还有1/2母联开关没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }


           flag = true;
           foreach (eleclass el in ML2col)
           {
               if (el.selectflag == false)
               {
                   wForm.ShowText += "\r\n还有2/3母联开关没有添加，请继续添加！" + System.DateTime.Now.ToString();
                   //MessageBox.Show("还有2/3母联开关没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }


           flag = true;
           foreach (eleclass el in HGcol)
           {
               if (el.selectflag == false)
               {
                   wForm.ShowText += "\r\n还有线路互感没有添加，请继续添加！" + System.DateTime.Now.ToString();
                   //MessageBox.Show("还有线路互感没有添加，请继续添加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   flag = false;
                   return false;
               }
           }

           return true;
       }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            frnReport wFrom = new frnReport();
            if (e.ClickedItem.Text == "属性")
            {
                elementProperty();
            }
            tlVectorControl1.Operation = ToolOperation.Select;
            if (e.ClickedItem.Text == "短路计算")
            {
               
                wFrom.Owner = this;
                wFrom.Show();
                wFrom.Text = this.Text + "短路计算";
                wFrom.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
                if (!addcheck(wFrom))
                {

                    return;
                }
                string duanluname = null;        //记录短路点的名字 如果是发生在支路上短路点的名字为线路连接的第一个母线名
                int tuxing = 0;
                int baobiao = 0;
                PSPDEV pspDuanlu = new PSPDEV();
                XmlElement element = tlVectorControl1.SVGDocument.CurrentElement;
                pspDuanlu.SUID = element.GetAttribute("Deviceid");
                pspDuanlu = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", pspDuanlu);
                if (pspDuanlu == null)
                    return;
                if (pspDuanlu.Type != "05" && pspDuanlu.Type != "01")
                    return;
                frmDuanlu dudu = new frmDuanlu(pspDuanlu);
                dudu.projectsuid = tlVectorControl1.SVGDocument.SvgdataUid;
                ElectricShorti lec = new ElectricShorti();
                int n11 = 0, n2 = 0, n3 = 0;
                double n4 = 0;

                if (dudu.ShowDialog() == DialogResult.OK)
                {
                    //int bigsmall=Convert.ToInt32(dudu.DuanluBigsmall);
                    try
                    {
                        if (!lec.CheckDL(tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID, 100))
                            return;
                        string nodeType;
                        if (dudu.DuanluBaobiao == "是")
                            baobiao = 1;
                        if (dudu.DuanluTuxing == "图形输出节点电压")
                            tuxing = 1;
                        if (dudu.DuanluTuxing == "图形输出短路电流")
                            tuxing = 2;
                        //Duanlu.Name = dudu.DuanluPoint;
                        //Duanlu = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByName", Duanlu);
                        n4 = (double)Convert.ToInt32(dudu.hscool) / 100;
                        nodeType = pspDuanlu.Type;
                        if (pspDuanlu.Type == "01")
                        {
                            n11 = 0;
                            n2 = pspDuanlu.Number;
                            switch (dudu.DuanluType)
                            {
                                case "单相接地":
                                    n3 = 1;
                                    break;

                                case "两相接地":
                                    n3 = 3;
                                    break;
                                case "两相故障":
                                    n3 = 2;
                                    break;
                                case "三相故障":
                                    n3 = 0;
                                    break;
                                default:
                                    n3 = 1;
                                    break;
                            }
                            
                            string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid+ "'AND PSPDEV.type='05'AND PSPDEV.KSwitchStatus = '0'order by PSPDEV.number";
                            PSPDEV psp = new PSPDEV();
                            IList list2 = Services.BaseService.GetList("SelectPSPDEVByCondition", con); ;
                            string dlr = null;
                            bool flag = false;　　　　　　　　　　　　　//记录读的是一般线路还是两绕组变压器上的母线还是三绕组上面的

                            string projectid = this.ProjectUID;
                            for (int i = 0; i < list2.Count; i++)
                            {
                                psp = list2[i] as PSPDEV;
                                con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                                IList listiswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                                IList listjswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                                PSPDEV pspjswitch = (PSPDEV)listjswitch[0];

                                if (pspDuanlu.Number == psp.FirstNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                                {

                                    flag = true;
                                    dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "0 " + " " + n3.ToString();

                                }
                                if (pspDuanlu.Number == psp.LastNode && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                                {
                                    flag = true;
                                    dlr = "0" + " " + psp.FirstNode + " " + psp.LastNode + " " + psp.Number + " " + "1 " + " " + n3.ToString();
                                }
                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\fault.txt"))
                                {
                                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\fault.txt");
                                }
                                if (flag)
                                {

                                    break;                 //跳出本循环 进行母线的另外一个母线短路

                                }
                                if (!flag)
                                    continue;
                                //写入错误中

                            }
                            if (!flag)
                            {
                                  con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.type='02'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                                IList list3 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                for (int i = 0; i < list3.Count; i++)
                                {
                                    dlr = null;
                                    psp = list3[i] as PSPDEV;
                                    PSPDEV devFirst = new PSPDEV();

                                    con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                    devFirst = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                    PSPDEV devLast = new PSPDEV();


                                    con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                    devLast = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                    con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                                    IList listiswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                    con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                                    IList listjswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                    PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                                    PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                                    if (pspDuanlu.Number == devFirst.Number && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                                    {

                                        flag = true;
                                        dlr = "0" + " " + devFirst.Number + " " + devLast.Number + " " + psp.Number + " " + "0" + " " + n3.ToString();

                                    }
                                    if (pspDuanlu.Number == devLast.Number && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0")
                                    {
                                        flag = true;
                                        dlr = "0" + " " + devFirst.Number + " " + devLast.Number + " " + psp.Number + " " + "1" + " " + n3.ToString();
                                    }
                                    if (flag)
                                    {
                                        break;                 //跳出本循环 进行母线的另外一个母线短路

                                    }
                                    if (!flag)
                                        continue;
                                    //写入错误中

                                }
                            }
                            if (!flag)
                            {
                                con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.type='03'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";
                                IList list4 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                for (int i = 0; i < list4.Count; i++)
                                {
                                    dlr = null;
                                    psp = list4[i] as PSPDEV;
                                    PSPDEV devINode = new PSPDEV();

                                    con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                    devINode = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                    PSPDEV devJNode = new PSPDEV();

                                    con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                    devJNode = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                    PSPDEV devKNode = new PSPDEV();

                                    con = " WHERE Name='" + psp.KName + "' AND ProjectID = '" + projectid + "'" + "AND Type='01'";
                                    devKNode = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);

                                    con = " WHERE Name='" + psp.ISwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                                    IList listiswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                    con = " WHERE Name='" + psp.JSwitch + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                                    IList listjswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                    con = " WHERE Name='" + psp.HuganLine1 + "' AND ProjectID = '" + projectid + "'" + "AND Type='07'";
                                    IList listkswitch = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                    PSPDEV pspiswitch = (PSPDEV)listiswitch[0];
                                    PSPDEV pspjswitch = (PSPDEV)listjswitch[0];
                                    PSPDEV pspkswitch = (PSPDEV)listkswitch[0];
                                    if (pspDuanlu.Number == devINode.Number && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                                    {

                                        flag = true;
                                        dlr = "0" + " " + devINode.Number + " " + devJNode.Number + " " + psp.Number + " " + "0" + " " + n3.ToString();

                                    }
                                    if (pspDuanlu.Number == devJNode.Number && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                                    {
                                        flag = true;
                                        dlr = "0" + " " + devINode.Number + " " + devJNode.Number + " " + psp.Number + " " + "1" + " " + n3.ToString();
                                    }
                                    if (pspDuanlu.Number == devKNode.Number && pspiswitch.KSwitchStatus == "0" && pspjswitch.KSwitchStatus == "0" && pspkswitch.KSwitchStatus == "0")
                                    {
                                        flag = true;
                                        dlr = "0" + " " + devINode.Number + " " + devKNode.Number + " " + psp.Number + " " + "1" + " " + n3.ToString();
                                    }

                                    if (flag)
                                    {
                                        break;                 //跳出本循环 进行母线的另外一个母线短路

                                    }
                                    if (!flag)
                                        continue;
                                    //写入错误中

                                }
                            }
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\fault.txt"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\fault.txt");
                            }

                            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                            StreamWriter str11 = new StreamWriter(VK);
                            str11.Write(dlr);
                            str11.Close();
                            n4 = 0;



                        }
                        else if (pspDuanlu.Type == "05")
                        {
                            //n11 = pspDuanlu.FirstNode;
                            //n2 = pspDuanlu.LastNode;
                            n11 = pspDuanlu.Number;
                            n2 = n11;
                            switch (dudu.DuanluType)
                            {
                                case "单相接地":
                                    n3 = 1;
                                    break;

                                case "两相接地":
                                    n3 = 3;
                                    break;
                                case "两相故障":
                                    n3 = 2;
                                    break;
                                case "三相故障":
                                    n3 = 0;
                                    break;
                                default:
                                    n3 = 0;
                                    break;
                            }
                            string dlr = null;
                            if (n4 < 1 && n4 > 0)
                            {
                                duanluname = pspDuanlu.Name;
                            }
                            dlr = "0" + " " + pspDuanlu.FirstNode + " " + pspDuanlu.LastNode + " " + pspDuanlu.Number + " " + n4 + " " + n3.ToString();

                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\fault.txt"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\fault.txt");
                            }

                            FileStream VK = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fault.txt"), FileMode.OpenOrCreate);
                            StreamWriter str11 = new StreamWriter(VK);
                            str11.Write(dlr);
                            str11.Close();
                        }
                        else
                        {
                            return;
                        }
                        XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@flag='" + "1" + "']");
                        foreach (XmlNode node in list)
                        {
                            SvgElement elementde = node as SvgElement;
                            tlVectorControl1.SVGDocument.CurrentElement = elementde;
                            tlVectorControl1.Delete();
                        }
                        wFrom.ShowText += "\r\n进行短路计算！" + System.DateTime.Now.ToString();
                        shortcir shortCutCal = new shortcir();
                        shortCutCal.Show_shortcir(0,0);
                        //bool matrixflag=true;                //用来判断是否导纳矩阵的逆矩阵是否存在逆矩阵

                        string matrixstr = null;
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Zmatrixcheck.txt"))
                        {
                            matrixstr = "正序导纳矩阵";
                            // matrixflag = false;
                        }
                        if (matrixstr != null)
                        {
                            matrixstr += ",";
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Fmatrixcheck.txt"))
                        {
                            // matrixflag = false;
                            matrixstr += "负序导纳矩阵";
                        }
                        if (matrixstr != null)
                        {
                            matrixstr += ",";
                        }
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Lmatrixcheck.txt"))
                        {
                            //matrixflag = false;
                            matrixstr += "零序导纳矩阵";
                        }
                        if (matrixstr != null)
                        {
                            wFrom.ShowText += "\r\n" + matrixstr + "不存在逆矩阵，请调整参数后再进行计算！" + System.DateTime.Now.ToString();
                            //MessageBox.Show(matrixstr + "不存在逆矩阵，请调整参数后再进行计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        string duanResult = null;
                        duanResult += "短路电流简表" + "\r\n" + "\r\n";
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                        {
                        }
                        else
                        {
                            return;
                        }
                         wFrom.ShowText += "\r\n结果显示！" + System.DateTime.Now.ToString();
                        FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                        StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                        string strLineGU;
                        string[] arrayGU;
                        char[] charSplitGU = new char[] { ' ' };
                        strLineGU = readLineGU.ReadLine();
                         int j = 0;
                        while (strLineGU != null)
                        {
                            arrayGU = strLineGU.Split(charSplitGU);
                            int i = 0;
                            string[] dev = new string[9];
                            dev.Initialize();
                            foreach (string str in arrayGU)
                            {
                                if (str != "")
                                {
                                    dev[i++] = str;
                                }
                            }
                            if (tuxing == 2 && j != 0)
                            {
                                PSPDEV CR = new PSPDEV();
                              
                                if (dev[1] != "du")
                                {
                                   string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.Name='" + dev[1] + "'";
                                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                }
                                else
                                {
                                    string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.Name='" + duanluname + "'AND PSPDEV.Type='05'";
                                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                }

                                if (CR != null)
                                {
                                    if (CR.Type != "05")
                                    {
                                        XmlElement elementdl = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@Deviceid='" + CR.SUID + "']") as XmlElement;

                                        if (elementdl != null)
                                        {
                                            RectangleF bound = ((IGraph)elementdl).GetBounds();
                                            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                            n1.SetAttribute("x", Convert.ToString(bound.X));
                                            n1.SetAttribute("y", Convert.ToString(bound.Y - 20));
                                            n1.InnerText = (Convert.ToDouble(dev[3]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt)).ToString("N4");
                                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                                            n1.SetAttribute("flag", "1");
                                            n1.SetAttribute("stroke", "#FF0000");
                                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                            tlVectorControl1.Operation = ToolOperation.Select;
                                            tlVectorControl1.Refresh();
                                        }
                                    }
                                    else
                                    {
                                        XmlElement elementdl = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@Deviceid='" + CR.SUID + "']") as XmlElement;

                                        if (elementdl != null)
                                        {
                                            PointF[] t = ((Polyline)elementdl).Points;

                                            PointF[] t2 = ((Polyline)elementdl).FirstTwoPoint;
                                            t = t2;
                                            PointF midt = new PointF((float)((t2[0].X + t2[1].X) / 2), (float)((t2[0].Y + t2[1].Y) / 2));
                                            float angel = 0f;
                                            angel = (float)(180 * Math.Atan2((t2[1].Y - t2[0].Y), (t2[1].X - t2[0].X)) / Math.PI);

                                            string l3 = Convert.ToString(midt.X);
                                            string l4 = Convert.ToString(midt.Y);

                                            string tran = ((Polyline)elementdl).Transform.ToString();

                                            PointF center = new PointF((float)(t[0].X + (t[1].X - t[0].X) / 2), (float)(t[0].Y + (t[1].Y - t[0].Y) / 2));
                                            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                            XmlElement n2dl = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                                            PointF pStart = new PointF(center.X + (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(15 * Math.Cos((angel) * Math.PI / 180)));
                                            PSPDEV psp = new PSPDEV();
                                            psp.FirstNode = CR.FirstNode;
                                            psp.LastNode = CR.LastNode;
                                            string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.FirstNode='" + CR.FirstNode + "'AND PSPDEV.LastNode='"+CR.LastNode+"'";
                                            IList listParallel = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                            
                                            PSPDEV tempss = new PSPDEV();
                                            
                                            foreach (PSPDEV devP in listParallel)
                                            {
                                                if ((angel > 10 && angel < 90) || (angel < 0 && Math.Abs(angel) < 90) || (angel > 180 && angel < 350))
                                                {
                                                    if (((devP.X1) > (CR.X1)))
                                                    {
                                                        pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));

                                                    }
                                                }
                                                else if ((angel >= 0 && angel <= 10) || (angel >= 350 && angel <= 360) || (angel < 0 && Math.Abs(angel) <= 90))
                                                {
                                                    if (((devP.Y1) > (CR.Y1)))
                                                    {
                                                        pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));

                                                    }
                                                }
                                                else if ((angel < 0 && Math.Abs(angel) > 90) || (angel >= 90 && angel <= 180))
                                                {
                                                    if (((devP.Y1) > (CR.Y1)))
                                                    {
                                                        pStart = new PointF(center.X - (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(7 * Math.Cos((angel) * Math.PI / 180)));

                                                    }
                                                }
                                            }

                                            PointF newp1 = new PointF(t[0].X + (t[1].X - t[0].X) / 2 - (float)(15 * Math.Sin(angel)), t[0].Y + (t[1].Y - t[0].Y) / 2 - (float)(15 * Math.Cos(angel)));

                                            n1.SetAttribute("x", Convert.ToString(pStart.X));
                                            n1.SetAttribute("y", Convert.ToString(pStart.Y));

                                            //if (Convert.ToDouble(dev[4]) >= 0)
                                            //{
                                            n1.InnerText = (Math.Abs(Convert.ToDouble(dev[3]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt))).ToString("N4");
                                            //}
                                            //else
                                            //{
                                            //    n1.InnerText = (Math.Abs(Convert.ToDouble(dev[3]))).ToString("N4");
                                            //}
                                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                                            n1.SetAttribute("flag", "1");

                                            //if (Convert.ToDouble(dev[5]) == 1)
                                            //    n1.SetAttribute("stroke", "#FF0000");

                                            PointF p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 25) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 25) * Math.PI / 180)));
                                            PointF p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 335) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 335) * Math.PI / 180)));

                                            if (Convert.ToDouble(dev[3]) < 0)
                                            {
                                                p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 155) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 155) * Math.PI / 180)));
                                                p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 205) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 205) * Math.PI / 180)));
                                            }

                                            string l1 = Convert.ToString(p1.X);
                                            string l2 = Convert.ToString(p1.Y);
                                            string l5 = Convert.ToString(p2.X);
                                            string l6 = Convert.ToString(p2.Y);

                                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                            tlVectorControl1.Operation = ToolOperation.Select;

                                            tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;

                                            RectangleF ttt = ((Polyline)elementdl).GetBounds();

                                            tlVectorControl1.RotateSelection(angel, pStart);
                                            if (Math.Abs(angel) > 90)
                                                tlVectorControl1.RotateSelection(180, pStart);
                                            PointF newp = new PointF(center.X + 10, center.Y + 10);

                                            tlVectorControl1.Refresh();

                                        }
                                    }
                                }
                                if (CR.NodeType == "05")
                                {
                                    PSPDEV fl = new PSPDEV();
                                    string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.Number='" + CR.FirstNode +"'";
                                    fl = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                    duanResult += dev[0] + "," + dev[1] + "," + Convert.ToDouble(dev[3]) * 100 / (Math.Sqrt(3) * fl.ReferenceVolt) + "\r\n";
                                }
                                else
                                {
                                    duanResult += dev[0] + "," + dev[1] + "," + Convert.ToDouble(dev[3]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "\r\n";
                                }

                            }
                            else if (tuxing==1&&j!=0)
                            {
                                PSPDEV CR = new PSPDEV();

                                if (dev[1] != "du")
                                {
                                    string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.Name='" + dev[1] + "'";
                                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                }
                                else
                                {
                                    string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.Name='" + duanluname + "'AND PSPDEV.Type='05'";
                                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                }

                                if (CR.NodeType == "05")
                                {
                                    PSPDEV fl = new PSPDEV();
                                    string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.Number='" + CR.FirstNode + "'";
                                    fl = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                    duanResult += dev[0] + "," + dev[1] + "," + Convert.ToDouble(dev[3]) * 100 / (Math.Sqrt(3) * fl.ReferenceVolt) + "\r\n";
                                }
                                else
                                {
                                    duanResult += dev[0] + "," + dev[1] + "," + Convert.ToDouble(dev[3]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "\r\n";
                                }
                            }
                            else if (j==0)
                            {
                                duanResult += dev[0] + "," + dev[1] + "," + dev[3]+ "\r\n";
                            }
                            strLineGU = readLineGU.ReadLine();
                            j++;
                        }
                        readLineGU.Close();
                        string dianYaResult = null;
                        dianYaResult += "母线电压结果" + "\r\n" + "\r\n";
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt"))
                        {
                        }
                        else
                        {
                            return;
                        }
                        FileStream dianYa = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianya.txt", FileMode.Open);
                        StreamReader readLineDY = new StreamReader(dianYa, System.Text.Encoding.Default);
                        string strLineDY;
                        string[] arrayDY;
                        char[] charSplitDY = new char[] { ' ' };
                        strLineDY = readLineDY.ReadLine();
                        j = 0;
                        while (strLineDY != null)
                        {
                            arrayDY = strLineDY.Split(charSplitDY);
                            int i = 0;
                            string[] dev = new string[14];
                            dev.Initialize();
                            foreach (string str in arrayDY)
                            {
                                if (str != "")
                                {
                                    dev[i++] = str;
                                }
                            }
                            if (j == 0)
                            {
                                dianYaResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
         dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "\r\n";
                            }
                            else
                            {
                                bool flag = true;     //判断此母线是短路点母线还是一般的母线
                                PSPDEV CR = new PSPDEV();
                                
                                if (dev[1] != "du")
                                {
                                    string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.Name='" + dev[1] + "'";
                                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con); ;
                                }
                                else
                                {
                                    flag = false;
                                    string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.Name='" + duanluname + "'AND PSPDEV.Type='05'";
                                    CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                }


                                //CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByNameANDSVG", CR);
                                if (tuxing == 1)
                                {
                                    XmlElement elementdl = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@Deviceid='" + CR.SUID + "']") as XmlElement;

                                    if (elementdl != null)
                                    {
                                        RectangleF bound = ((IGraph)elementdl).GetBounds();
                                        XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                        XmlElement n22 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                        XmlElement n33 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                        n1.SetAttribute("x", Convert.ToString(bound.X));
                                        n1.SetAttribute("y", Convert.ToString(bound.Y - 60));
                                        n1.InnerText = "A相:" + (Convert.ToDouble(dev[2]) * CR.ReferenceVolt).ToString("N4") + "kV/" + (Convert.ToDouble(dev[3])).ToString("N4") + "°";
                                        n1.SetAttribute("layer", SvgDocument.currentLayer);
                                        n1.SetAttribute("flag", "1");
                                        tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                        tlVectorControl1.Operation = ToolOperation.Select;
                                        tlVectorControl1.Refresh();
                                        n22.SetAttribute("x", Convert.ToString(bound.X));
                                        n22.SetAttribute("y", Convert.ToString(bound.Y - 40));
                                        n22.InnerText = "B相:" + (Convert.ToDouble(dev[4]) * CR.ReferenceVolt).ToString("N4") + "kV/" + (Convert.ToDouble(dev[5])).ToString("N4") + "°";
                                        n22.SetAttribute("layer", SvgDocument.currentLayer);
                                        n22.SetAttribute("flag", "1");
                                        tlVectorControl1.SVGDocument.RootElement.AppendChild(n22);
                                        tlVectorControl1.Operation = ToolOperation.Select;
                                        tlVectorControl1.Refresh();
                                        n33.SetAttribute("x", Convert.ToString(bound.X));
                                        n33.SetAttribute("y", Convert.ToString(bound.Y - 20));
                                        n33.InnerText = "C相:" + (Convert.ToDouble(dev[6]) * CR.ReferenceVolt).ToString("N4") + "kV/" + (Convert.ToDouble(dev[7])).ToString("N4") + "°";
                                        n33.SetAttribute("layer", SvgDocument.currentLayer);
                                        n33.SetAttribute("flag", "1");
                                        tlVectorControl1.SVGDocument.RootElement.AppendChild(n33);
                                        tlVectorControl1.Operation = ToolOperation.Select;
                                        tlVectorControl1.Refresh();
                                    }
                                }
                                if (flag)
                                    dianYaResult += dev[0] + "," + dev[1] + "," + Convert.ToDouble(dev[2]) * CR.ReferenceVolt + "," + dev[3] + "," + Convert.ToDouble(dev[4]) * CR.ReferenceVolt + "," + dev[5] + "," + Convert.ToDouble(dev[6]) * CR.ReferenceVolt + "," + dev[7] + "," + Convert.ToDouble(dev[8]) * CR.ReferenceVolt + "," +
                                        dev[9] + "," + Convert.ToDouble(dev[10]) * CR.ReferenceVolt + "," + dev[11] + "," + Convert.ToDouble(dev[12]) * CR.ReferenceVolt + "," + dev[13] + "\r\n";
                                else
                                    dianYaResult += dev[0] + "," + duanluname + "上短路点" + "," + Convert.ToDouble(dev[2]) * CR.ReferenceVolt + "," + dev[3] + "," + Convert.ToDouble(dev[4]) * CR.ReferenceVolt + "," + dev[5] + "," + Convert.ToDouble(dev[6]) * CR.ReferenceVolt + "," + dev[7] + "," + Convert.ToDouble(dev[8]) * CR.ReferenceVolt + "," +
                                       dev[9] + "," + Convert.ToDouble(dev[10]) * CR.ReferenceVolt + "," + dev[11] + Convert.ToDouble(dev[12]) * CR.ReferenceVolt + "," + dev[13] + "\r\n";

                            }
                            strLineDY = readLineDY.ReadLine();
                            j++;
                        }
                        readLineDY.Close();
                        string dianLiuResult = null;
                        dianLiuResult += "支路电流结果" + "\r\n" + "\r\n";
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt"))
                        {
                        }
                        else
                        {
                            return;
                        }
                        FileStream dianLiu = new FileStream(System.Windows.Forms.Application.StartupPath + "\\Sxdianliu.txt", FileMode.Open);
                        StreamReader readLineDL = new StreamReader(dianLiu, System.Text.Encoding.Default);
                        string strLineDL;
                        string[] arrayDL;
                        char[] charSplitDL = new char[] { ' ' };
                        strLineDL = readLineDL.ReadLine();
                        j = 0;
                        while (strLineDL != null)
                        {
                            arrayDL = strLineDL.Split(charSplitDL);
                            int i = 0;
                            string[] dev = new string[15];
                            dev.Initialize();
                            foreach (string str in arrayDL)
                            {
                                if (str != "")
                                {
                                    dev[i++] = str;
                                }
                            }
                            if (j == 0)
                            {
                                dianLiuResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + dev[3] + "," + dev[4] + "," + dev[5] + "," + dev[6] + "," + dev[7] + "," + dev[8] + "," +
                                             dev[9] + "," + dev[10] + "," + dev[11] + "," + dev[12] + "," + dev[13] + "," + dev[14] + "\r\n";
                            }
                            else
                            {
                                //PSPDEV CR = new PSPDEV();
                                //CR.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                //CR.Number = Convert.ToInt32(dev[2]);
                                //CR.Type = "Polyline";
                                //CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByNumberAndSvgUIDAndType", CR);

                                //因为在线路电流输出时既有一般线路的电流、两绕组和三绕组线路的电流还有接地电容器和电抗器的电流，因此只将电流输出就行了

                                PSPDEV CR = new PSPDEV();
                               // CR.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                
                                if (dev[0] != "du")
                                {
                                    CR.Name = dev[0];
                                }
                                else
                                    CR.Name = dev[1];
                                string con = " ,PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "'AND PSPDEV.Name='" + CR.Name + "'AND PSPDEV.Type='01'";
                                CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                if (tuxing == 1)
                                {
                                    //    XmlElement elementdl = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + CR.EleID + "']") as XmlElement;

                                    //    if (elementdl != null)
                                    //    {
                                    //        PointF[] t = ((Polyline)elementdl).Points;

                                    //        PointF[] t2 = ((Polyline)elementdl).FirstTwoPoint;
                                    //        t = t2;
                                    //        PointF midt = new PointF((float)((t2[0].X + t2[1].X) / 2), (float)((t2[0].Y + t2[1].Y) / 2));
                                    //        float angel = 0f;
                                    //        angel = (float)(180 * Math.Atan2((t2[1].Y - t2[0].Y), (t2[1].X - t2[0].X)) / Math.PI);

                                    //        string l3 = Convert.ToString(midt.X);
                                    //        string l4 = Convert.ToString(midt.Y);

                                    //        string tran = ((Polyline)elementdl).Transform.ToString();

                                    //        PointF center = new PointF((float)(t[0].X + (t[1].X - t[0].X) / 2), (float)(t[0].Y + (t[1].Y - t[0].Y) / 2));
                                    //        XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                    //        XmlElement n2dl = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                                    //        PointF pStart = new PointF(center.X + (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(15 * Math.Cos((angel) * Math.PI / 180)));
                                    //        PSPDEV psp = new PSPDEV();
                                    //        psp.FirstNode = CR.FirstNode;
                                    //        psp.LastNode = CR.LastNode;
                                    //        psp.SvgUID = CR.SvgUID;
                                    //        PSPDEV tempss = new PSPDEV();
                                    //        IList listParallel = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandFirstOrLastNode", psp);
                                    //        foreach (PSPDEV devP in listParallel)
                                    //        {
                                    //            if ((angel > 10 && angel < 90) || (angel < 0 && Math.Abs(angel) < 90) || (angel > 180 && angel < 350))
                                    //            {
                                    //                if (((devP.X1) > (CR.X1)))
                                    //                {
                                    //                    pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));

                                    //                }
                                    //            }
                                    //            else if ((angel >= 0 && angel <= 10) || (angel >= 350 && angel <= 360) || (angel < 0 && Math.Abs(angel) <= 90))
                                    //            {
                                    //                if (((devP.Y1) > (CR.Y1)))
                                    //                {
                                    //                    pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));

                                    //                }
                                    //            }
                                    //            else if ((angel < 0 && Math.Abs(angel) > 90) || (angel >= 90 && angel <= 180))
                                    //            {
                                    //                if (((devP.Y1) > (CR.Y1)))
                                    //                {
                                    //                    pStart = new PointF(center.X - (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(7 * Math.Cos((angel) * Math.PI / 180)));

                                    //                }
                                    //            }
                                    //        }

                                    //        PointF newp1 = new PointF(t[0].X + (t[1].X - t[0].X) / 2 - (float)(15 * Math.Sin(angel)), t[0].Y + (t[1].Y - t[0].Y) / 2 - (float)(15 * Math.Cos(angel)));

                                    //        n1.SetAttribute("x", Convert.ToString(pStart.X));
                                    //        n1.SetAttribute("y", Convert.ToString(pStart.Y));

                                    //        //if (Convert.ToDouble(dev[4]) >= 0)
                                    //        //{
                                    //        n1.InnerText = (Math.Abs(Convert.ToDouble(dev[3]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt))).ToString("N4");
                                    //        //}
                                    //        //else
                                    //        //{
                                    //        //    n1.InnerText = (Math.Abs(Convert.ToDouble(dev[3]))).ToString("N4");
                                    //        //}
                                    //        n1.SetAttribute("layer", SvgDocument.currentLayer);
                                    //        n1.SetAttribute("flag", "1");

                                    //        //if (Convert.ToDouble(dev[3]) == 1)
                                    //        //    n1.SetAttribute("stroke", "#FF0000");

                                    //        PointF p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 25) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 25) * Math.PI / 180)));
                                    //        PointF p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 335) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 335) * Math.PI / 180)));

                                    //        if (Convert.ToDouble(dev[3]) < 0)
                                    //        {
                                    //            p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 155) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 155) * Math.PI / 180)));
                                    //            p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 205) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 205) * Math.PI / 180)));
                                    //        }

                                    //        string l1 = Convert.ToString(p1.X);
                                    //        string l2 = Convert.ToString(p1.Y);
                                    //        string l5 = Convert.ToString(p2.X);
                                    //        string l6 = Convert.ToString(p2.Y);

                                    //        tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                    //        tlVectorControl1.Operation = ToolOperation.Select;

                                    //        tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;

                                    //        RectangleF ttt = ((Polyline)elementdl).GetBounds();

                                    //        tlVectorControl1.RotateSelection(angel, pStart);
                                    //        if (Math.Abs(angel) > 90)
                                    //            tlVectorControl1.RotateSelection(180, pStart);
                                    //        PointF newp = new PointF(center.X + 10, center.Y + 10);

                                    //        tlVectorControl1.Refresh();



                                }
                                dianLiuResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + Convert.ToDouble(dev[3]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[4] + "," + Convert.ToDouble(dev[5]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[6] + "," + Convert.ToDouble(dev[7]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[8] + "," +
                                  Convert.ToDouble(dev[9]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[10] + "," + Convert.ToDouble(dev[11]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[12] + "," + Convert.ToDouble(dev[13]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[14] + "\r\n";

                                //if (CR.NodeType == "Polyline")
                                //{
                                //    PSPDEV fl = new PSPDEV();
                                //    fl.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                //    fl.Number = CR.FirstNode;
                                //    fl.Type = nodeType;
                                //    fl = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByNumberAndSvgUIDAndType", CR);
                                //    dianLiuResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + Convert.ToDouble(dev[3]) * 100 / (Math.Sqrt(3) * fl.ReferenceVolt) + "," + dev[4] + "," + Convert.ToDouble(dev[5]) * 100 / (Math.Sqrt(3) * fl.ReferenceVolt) + "," + dev[6] + "," + Convert.ToDouble(dev[7]) * 100 / (Math.Sqrt(3) * fl.ReferenceVolt) + "," + dev[8] + "," +
                                //    Convert.ToDouble(dev[9]) * 100 / (Math.Sqrt(3) * fl.ReferenceVolt) + "," + dev[10] + "," + Convert.ToDouble(dev[11]) * 100 / (Math.Sqrt(3) * fl.ReferenceVolt) + dev[12] + "," + Convert.ToDouble(dev[13]) * 100 / (Math.Sqrt(3) * fl.ReferenceVolt) + "," + dev[14] + "\r\n";
                                //}
                                //else
                                //{
                                //    dianLiuResult += dev[0] + "," + dev[1] + "," + dev[2] + "," + Convert.ToDouble(dev[3]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[4] + "," + Convert.ToDouble(dev[5]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[6] + "," + Convert.ToDouble(dev[7]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[8] + "," +
                                //    Convert.ToDouble(dev[9]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[10] + "," + Convert.ToDouble(dev[11]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + dev[12] + "," + Convert.ToDouble(dev[13]) * 100 / (Math.Sqrt(3) * CR.ReferenceVolt) + "," + dev[14] + "\r\n";
                                //}


                            }

                            strLineDL = readLineDL.ReadLine();
                            j++;
                        }
                        readLineDL.Close();
                        if (baobiao == 1)
                        {
                             wFrom.ShowText += "\r\n形成报表！" + System.DateTime.Now.ToString();
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                            }
                            FileStream tempGU = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
                            StreamWriter strGU = new StreamWriter(tempGU, Encoding.GetEncoding("GB2312"));
                            strGU.Write(duanResult);
                            strGU.Close();
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                            }
                            FileStream tempDY = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                            StreamWriter strDY = new StreamWriter(tempDY, Encoding.GetEncoding("GB2312"));
                            strDY.Write(dianYaResult);
                            strDY.Close();
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result2.csv"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                            }
                            FileStream tempDL = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result2.csv"), FileMode.OpenOrCreate);
                            StreamWriter strDL = new StreamWriter(tempDL, Encoding.GetEncoding("GB2312"));
                            strDL.Write(dianLiuResult);
                            strDL.Close();
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "短路计算结果.xls"))
                            {
                                File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "短路计算结果.xls");
                            }

                            Excel.Application ex;
                            Excel.Worksheet xSheet;
                            Excel.Application result1;
                            Excel.Application result2;
                            Excel.Worksheet tempSheet;
                            Excel.Worksheet tempSheet1;
                            Excel.Worksheet newWorksheet;
                            Excel.Worksheet newWorkSheet1;

                            object oMissing = System.Reflection.Missing.Value;
                            ex = new Excel.Application();
                            ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv");

                            xSheet = (Excel.Worksheet)ex.Worksheets[1];
                            ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                            xSheet = (Excel.Worksheet)ex.Worksheets[2];
                            ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                            xSheet = (Excel.Worksheet)ex.Worksheets[1];
                            result1 = new Excel.Application();
                            result1.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                            result2 = new Excel.Application();
                            result2.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                            tempSheet = (Excel.Worksheet)result1.Worksheets.get_Item(1);
                            tempSheet1 = (Excel.Worksheet)result2.Worksheets.get_Item(1);
                            newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
                            newWorkSheet1 = (Excel.Worksheet)ex.Worksheets.get_Item(3);
                            newWorksheet.Name = "母线电压";
                            newWorkSheet1.Name = "支路电流";
                            xSheet.Name = "短路电流";
                            ex.Visible = true;

                            tempSheet.Cells.Select();
                            tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                            newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                            tempSheet1.Cells.Select();
                            tempSheet1.Cells.Copy(System.Reflection.Missing.Value);
                            newWorkSheet1.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                            xSheet.UsedRange.Font.Name = "楷体_GB2312";
                            newWorksheet.UsedRange.Font.Name = "楷体_GB2312";
                            newWorkSheet1.UsedRange.Font.Name = "楷体_GB2312";

                            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 3]).MergeCells = true;
                            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
                            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                            xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[3, 3]).Interior.ColorIndex = 45;
                            xSheet.get_Range(xSheet.Cells[4, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                            xSheet.get_Range(xSheet.Cells[4, 3], xSheet.Cells[xSheet.UsedRange.Rows.Count, 13]).NumberFormat = "0.0000_ ";

                            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 14]).MergeCells = true;
                            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                            newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            newWorksheet.get_Range(newWorksheet.Cells[3, 1], newWorksheet.Cells[3, 14]).Interior.ColorIndex = 45;
                            newWorksheet.get_Range(newWorksheet.Cells[4, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                            newWorksheet.get_Range(newWorksheet.Cells[4, 3], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 13]).NumberFormat = "0.0000_ ";

                            newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 15]).MergeCells = true;
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Size = 20;
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).Font.Name = "黑体";
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[1, 1], newWorkSheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[3, 1], newWorkSheet1.Cells[3, 15]).Interior.ColorIndex = 45;
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[4, 1], newWorkSheet1.Cells[newWorkSheet1.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[4, 2], newWorkSheet1.Cells[newWorkSheet1.UsedRange.Rows.Count, 2]).Interior.ColorIndex = 6;
                            newWorkSheet1.get_Range(newWorkSheet1.Cells[4, 4], newWorkSheet1.Cells[newWorkSheet1.UsedRange.Rows.Count, 14]).NumberFormat = "0.0000_ ";
                            xSheet.Rows.AutoFit();
                            xSheet.Columns.AutoFit();
                            newWorksheet.Rows.AutoFit();
                            newWorksheet.Columns.AutoFit();
                            newWorkSheet1.Rows.AutoFit();
                            newWorkSheet1.Columns.AutoFit();
                            newWorksheet.SaveAs(System.Windows.Forms.Application.StartupPath + "\\" + tlVectorControl1.SVGDocument.FileName + "短路计算结果.xls", Excel.XlFileFormat.xlXMLSpreadsheet, null, null, false, false, false, null, null, null);
                            System.Windows.Forms.Clipboard.Clear();
                            result1.Workbooks.Close();
                            result1.Quit();
                            result2.Workbooks.Close();
                            result2.Quit();
                             wFrom.ShowText += "\r\n结果成功！" + System.DateTime.Now.ToString();
                        }

                    }
                    catch (Exception e1)
                    {
                         wFrom.ShowText += "\r\n计算失败，短路数据有问题，请调整后再计算！" + System.DateTime.Now.ToString();
                       // MessageBox.Show("短路数据有问题，请调整后再计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void elementProperty()
        {
           XmlElement element = tlVectorControl1.SVGDocument.CurrentElement;
           if (element is Use)
           {
               if (element.GetAttribute("xlink:href").Contains("motherlinenode"))
               {
                   string suid = element.GetAttribute("Deviceid");
                  // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmMXdlg dlg = new frmMXdlg();
                   if (suid != null)
                   {
                       PSPDEV dev =(PSPDEV) DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name==null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" +dlg.Name+ "' AND Type = '01'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                         if (text != null)
                         {
                           (text as Text).InnerText = dev.Name;
                         }
                           Services.BaseService.Update<PSPDEV>(dev);
                          
                       }
                   }
                   
               }
               if (element.GetAttribute("xlink:href").Contains("transformertwozu"))
               {
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmBYQ2dlg dlg = new frmBYQ2dlg();
                   if (suid != null)
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '02'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }

               }
               if (element.GetAttribute("xlink:href").Contains("transformerthirdzu"))
               {
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmBYQ3dlg dlg = new frmBYQ3dlg();
                   if (suid != null)
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '03'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }

               }
               if (element.GetAttribute("xlink:href").Contains("dynamotorline"))
               {
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmFDJdlg dlg = new frmFDJdlg();
                   if (suid != null)
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '04'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }

               }
               if (element.GetAttribute("xlink:href").Contains("loadline"))
               {
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmFHdlg dlg = new frmFHdlg();
                   if (suid != null)
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '12'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }

               }
               if (element.GetAttribute("xlink:href").Contains("串联电抗器"))
               {
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmCLDKdlg dlg = new frmCLDKdlg();
                   if (suid != null)
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '10'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }

               }
               if (element.GetAttribute("xlink:href").Contains("串联电容器"))
               {
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmCLDRdlg dlg = new frmCLDRdlg();
                   if (suid != null)
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '08'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }

               }
               if (element.GetAttribute("xlink:href").Contains("并联电容器"))
               {
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmBLDRdlg dlg = new frmBLDRdlg();
                   if (suid != null)
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '09'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }

               }
               if (element.GetAttribute("xlink:href").Contains("并联电抗器"))
               {
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmBLDKdlg dlg = new frmBLDKdlg();
                   if (suid != null)
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '11'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }

               }
               if (element.GetAttribute("xlink:href").Contains("1/2母联开关"))
               {
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmMLdlg dlg = new frmMLdlg();
                   if (suid != null)
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '13'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }

               }
               if (element.GetAttribute("xlink:href").Contains("2/3母联开关"))
               {
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmML2dlg dlg = new frmML2dlg();
                   if (suid != null)
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '14'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }

               }
               if (element.GetAttribute("xlink:href").Contains("线路互感"))
               {
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmML2dlg dlg = new frmML2dlg();
                   if (suid != null)
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '15'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }
　　　　　　　　　
               }
           }
           else if (element is Polyline)
           {
              
                   string suid = element.GetAttribute("Deviceid");
                   // DeviceHelper.ShowDeviceDlg(DeviceType.MX, suid, false);
                   frmXLdlg dlg = new frmXLdlg();
                   if (!string.IsNullOrEmpty(suid))
                   {
                       PSPDEV dev = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(suid);
                       dlg.DeviceMx = dev;
                       if (dlg.ShowDialog() == DialogResult.OK)
                       {
                           if (dlg.Name == null)
                           {
                               MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           string strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + tlVectorControl1.SVGDocument.SvgdataUid + "' AND Name = '" + dlg.Name + "' AND Type = '05'";
                           IList listName = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                           if (listName.Count >= 2 || (listName.Count == 1 && (listName[0] as PSPDEV).SUID != dev.SUID))
                           {
                               MessageBox.Show("名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               return;
                           }
                           dev = dlg.DeviceMx;
                           dev.ProjectID = this.ProjectUID;
                           XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                           if (text != null)
                           {
                               (text as Text).InnerText = dev.Name;
                           }
                           Services.BaseService.Update<PSPDEV>(dev);

                       }
                   }
                   //else if (suid=="")
                   //{
                   //    //dlgSubstation.InitData();
                   //    object subID = DeviceHelper.SelectProjectDevice("05", tlVectorControl1.SVGDocument.SvgdataUid, this.ProjectUID, linecol);
                   //    XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                   //    if (subID != null)
                   //    {

                   //        element.SetAttribute("Deviceid", ((PSPDEV)subID).SUID);
                   //        element.SetAttribute("Type", "05");
                   //        element.SetAttribute("layer", SvgDocument.currentLayer);
                          
                           
                   //        tlVectorControl1.SVGDocument.RootElement.AppendChild(element);
                   //        tlVectorControl1.SVGDocument.CurrentElement = element as SvgElement;
                   //        //RectangleF t = ((IGraph)temp).GetBounds();
                   //        //n1.SetAttribute("x", (t.X - 10).ToString());
                   //        //n1.SetAttribute("y", (t.Y - 10).ToString());
                   //        //n1.InnerText = ((PSPDEV)subID).Name;
                   //        //n1.SetAttribute("layer", SvgDocument.currentLayer);
                   //        //n1.SetAttribute("ParentID", temp.GetAttribute("id"));
                   //        //tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                   //        tlVectorControl1.Operation = ToolOperation.Select;
                   //    }
                   //    else
                   //    {
                   //        tlVectorControl1.SVGDocument.CurrentElement = element as SvgElement;
                   //        tlVectorControl1.Delete();
                   //        return;
                   //        //tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                   //        //tlVectorControl1.Delete();
                   //    }

                   //    foreach (eleclass ele in linecol)
                   //    {
                   //        if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == true)
                   //        {
                   //            MessageBox.Show("已经有此线路！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   //            tlVectorControl1.SVGDocument.CurrentElement = element as SvgElement;
                   //            tlVectorControl1.Delete();
                   //            //tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;
                   //            //tlVectorControl1.Delete();
                   //        }
                   //        else if (ele.suid == ((PSPDEV)subID).SUID && ele.selectflag == false)
                   //        {
                   //            linecol.Remove(ele);
                   //            eleclass ele1 = new eleclass(((PSPDEV)subID).Name, ((PSPDEV)subID).SUID, ((PSPDEV)subID).Type, true);
                   //            linecol.Add(ele1);
                   //        }

                   //    }
                   //}

           }
        }


        private void frmTLpspShortGraphical_Load(object sender, EventArgs e)
        {
            this.alignButton = this.dotNetBarManager1.GetItem("mAlign") as ButtonItem;
            this.orderButton = this.dotNetBarManager1.GetItem("mOrder") as ButtonItem;
            this.rotateButton = this.dotNetBarManager1.GetItem("mRotate") as ButtonItem;



        }
    }
    //public struct eleclass
    //{
    //    public string name;                  //记录元件的名称

    //    public string suid;               //记录元件的主健

    //    public string type;              //记录元件类型
    //    public bool selectflag;           //是否被选中

    //    public eleclass(string  _name, string _suid,string _type,bool _selectflag)
    //    {
    //        name = _name;
    //        suid = _suid;
    //        type = _type;
    //        selectflag = _selectflag;
    //    }
    //}
}