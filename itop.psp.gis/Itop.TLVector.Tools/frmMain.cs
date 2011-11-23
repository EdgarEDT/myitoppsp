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
using Itop.Domain.RightManager;
using Itop.Client.Base;
using Itop.MapView;
using System.IO;
using System.Threading;
using Itop.CADLib;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ItopVector.Core.Interface;
using ItopVector.Core.Types;
using System.Data.OleDb;



namespace ItopVector.Tools
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
    public partial class frmMain : FormBase
    {
        #region 对象声明
        SVGFILE svg = new SVGFILE();
        SvgDocument sdoc = new SvgDocument();
        glebeProperty gPro = new glebeProperty();
        CtrlFileManager ctlfile = new CtrlFileManager();
        DevComponents.DotNetBar.ToolTip tip;
        public ArrayList SaveID=new ArrayList();
        XLTProcessor xltProcessor;
        public static string MapType = "接线图";
        public string yearID = "";
        public string SvgName = "";
        static string ParentUID = "";
        public static string progtype = "";
        public string linekey = "";
        public string XZ_bdz = "";
        private ItopVector.Selector.SymbolSelector symbolSelector;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private DevComponents.DotNetBar.ComboBoxItem LayerBox;
        private DevComponents.DotNetBar.ComboBoxItem scaleBox;
        //private ButtonItem operationButton;
        private ButtonItem orderButton;
        private ButtonItem alignButton;
        private ButtonItem rotateButton;

        string strj1 = "";
        string strw1 = "";
        string strd1 = "";
        string strj2 = "";
        string strw2 = "";
        string strd2 = "";
        string sel_sym = "";
        string sel_start_point = "";

        ArrayList ChangeLayerList = new ArrayList();
        frmLayerManager frmlar = new frmLayerManager();
        private string SVGUID = "";
        private string SelUseArea = "";
        private string LineLen = "";
        private string rzb = "1";
        private string selLar = "";
        private int LayerCount = 0;

        private bool LoadImage = true;
        public bool SubPrint = false;

        public string ghType = "";
        int chose;
        double jd = 0;
        double wd = 0;
        int show3d = 0;
        XmlNode img = null;
        frmInfo fInfo = new frmInfo();
        public Itop.MapView.IMapViewObj mapview;
        //public Itop.MapView.IMapViewObj mapview1;
        //public Itop.MapView.IMapViewObj mapview2;
        public event OnCloseDocumenthandler OnCloseSvgDocument;
        int mapOpacity =70;
        public int MapOpacity {
            get {
                return mapOpacity;             
            }
            set {
                mapOpacity = value;
                tlVectorControl1.Invalidate(true);
            }
        }
        #endregion

        public frmMain()
        {
            propertyGrid = new PropertyGrid();
            tip = new DevComponents.DotNetBar.ToolTip();
            ItopVector.SpecialCursors.LoadCursors();
            InitializeComponent();
            tlVectorControl1.CanEdit = true;
            //tlVectorControl1.DrawArea.FreeSelect = true;
            this.dotNetBarManager1.Images = ItopVector.Resource.ResourceHelper.LoadBitmapStrip(base.GetType(), "ItopVector.Tools.ToolbarImages.bmp", new Size(16, 16), new Point(0, 0));
            tlVectorControl1.LeftClick += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_LeftClick);
            tlVectorControl1.RightClick += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_RightClick);
            tlVectorControl1.DoubleLeftClick += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_DoubleLeftClick);
            tlVectorControl1.MoveIn += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_MoveIn);
            tlVectorControl1.MoveOut += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_MoveOut);
            tlVectorControl1.OnTipEvent += new ItopVector.Core.Interface.OnTipEventHandler(tlVectorControl1_OnTipEvent);
            tlVectorControl1.ScaleChanged += new EventHandler(tlVectorControl1_ScaleChanged);
            tlVectorControl1.DragAndDrop += new DragEventHandler(tlVectorControl1_DragAndDrop);
            tlVectorControl1.DrawArea.ViewChanged += new ItopVector.DrawArea.ViewChangedEventHandler(DrawArea_ViewChanged);
            tlVectorControl1.AfterPaintPage += new ItopVector.DrawArea.PaintMapEventHandler(tlVectorControl1_AfterPaintPage);
            tlVectorControl1.DrawArea.OnBeforeRenderTo += new ItopVector.DrawArea.PaintMapEventHandler(DrawArea_OnBeforeRenderTo);
            //tlVectorControl1.ScaleChanged += new EventHandler(tlVectorControl1_Move);
            //tlVectorControl1.MouseUp +=new MouseEventHandler(tlVectorControl1_MouseUp);
            tlVectorControl1.DrawArea.OnPolyLineBreak += new PolyLineBreakEventHandler(DrawArea_OnPolyLineBreak);
            tlVectorControl1.DocumentChanged += new OnDocumentChangedEventHandler(tlVectorControl1_DocumentChanged);
            tlVectorControl1.DrawArea.BeforeAddSvgElement += new AddSvgElementEventHandler(DrawArea_BeforeAddSvgElement);
            tlVectorControl1.DrawArea.OnAddElement += new AddSvgElementEventHandler(DrawArea_OnAddElement);
            tlVectorControl1.SVGDocument.OnDocumentChanged += new OnDocumentChangedEventHandler(SVGDocument_OnDocumentChanged);
            //tlVectorControl1.DrawArea.OnElementMove += new ElementMoveEventHandler(DrawArea_OnElementMove);
            toolDel.Click += delegate { tlVectorControl1.Delete(); };
            SvgDocument.BkImageLoad = true;

            Pen pen1 = new Pen(Brushes.Cyan, 3);
            tlVectorControl1.TempPen = pen1;
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.BackColor = Color.White;
            tlVectorControl1.OperationChanged += new EventHandler(tlVectorControl1_OperationChanged);
            tlVectorControl1.FullDrawMode = true;
            tlVectorControl1.DrawArea.ViewMargin = new Size(50000, 50000);
            tlVectorControl1.DrawMode = DrawModeType.MemoryImage;
            jd = Convert.ToDouble(ConfigurationSettings.AppSettings.Get("jd"));
            wd = Convert.ToDouble(ConfigurationSettings.AppSettings.Get("wd"));
            ghType = ConfigurationSettings.AppSettings.Get("ghType");
            //mapview.ZeroLongLat = new LongLat(117.6787m, 31.0568m);
            //mapview.ZeroLongLat = new LongLat(108.1m, 24.75m);
            chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
            show3d = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("show3d"));
            if (show3d == 1)
            {
                checkEdit1.Visible = true;
            }
            else if (show3d == 0)
            {
                checkEdit1.Visible = false;
            }
            if (chose == 1)
            { mapview = new Itop.MapView.MapViewObj(); }
            else if (chose == 2)
            { mapview = new Itop.MapView.MapViewGoogle(); }
            mapview.ZeroLongLat = new LongLat(jd, wd);
            tlVectorControl1.CurrentOperation = ToolOperation.Select;
            frmlar.Owner = this;
            frmlar.Height = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Height * .7);
            frmlar.Top = tlVectorControl1.PointToScreen(tlVectorControl1.Location).Y - 50;
            frmlar.Left = Screen.PrimaryScreen.WorkingArea.Width - frmlar.Width - 16;
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            
        }
        void DrawArea_OnElementMove(object sender, MoveEventArgs e)
        {

            //string a = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            //MessageBox.Show(a);
        }

        void DrawArea_GraphChanged(object sender, EventArgs e)
        {

         
        }

        void SVGDocument_OnDocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            //string a = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            //MessageBox.Show(a);
        }

        void DrawArea_OnAddElement(object sender, AddSvgElementEventArgs e)
        {
            string larid = tlVectorControl1.SVGDocument.CurrentLayer.ID;

            if (!ChangeLayerList.Contains(larid))
            {
                ChangeLayerList.Add(larid);
            }
            //MessageBox.Show(e.SvgElement.ID);
            if (XZ_bdz.Length >5)
            {
            lb11:

                frmInputDialog2 input = new frmInputDialog2();
                if (input.ShowDialog() == DialogResult.OK)
                {

                    PSP_SubstationSelect s = new PSP_SubstationSelect();
                    s.SName=input.InputStr;
                    s.SvgID=tlVectorControl1.SVGDocument.SvgdataUid;
                    PSP_SubstationSelect ss1 = (PSP_SubstationSelect)Services.BaseService.GetObject("SelectPSP_SubstationSelectByName", s);
                    if(ss1!=null){
                        MessageBox.Show("名称重复。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        goto lb11;
                    }
                    else{
                        s.UID = Guid.NewGuid().ToString();
                        s.EleID = e.SvgElement.ID;
                        s.SName = input.InputStr;
                        s.Remark = input.strRemark;
                        s.col2 = XZ_bdz;
                        s.SvgID = tlVectorControl1.SVGDocument.SvgdataUid;
                        Services.BaseService.Create<PSP_SubstationSelect>(s);
                    }
                }
                else
                {
                    tlVectorControl1.Delete();
                }
                if (MessageBox.Show("是否继续选择新的变电站址？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {

                }
                else
                {
                    XZ_bdz = "";
                }
            }
        }

        void DrawArea_BeforeAddSvgElement(object sender, AddSvgElementEventArgs e)
        {
            //MessageBox.Show( e.SvgElement.ID);
        }

        void tlVectorControl1_DocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            if (e.ChangeElements.Length > 0)
            {
                string larid = "";
                if (((SvgElement)e.ChangeElements[0]) != null)
                {
                    larid = ((SvgElement)e.ChangeElements[0]).GetAttribute("layer");
                    if (!ChangeLayerList.Contains(larid))
                    {
                        ChangeLayerList.Add(larid);
                    }
                }
            }
        }

        void DrawArea_OnPolyLineBreak(object sender, BreakElementEventArgs e)
        {
            string currentElementID = (sender as SvgElement).ID;
            string copyElementID = e.SvgElement.ID;
            if (currentElementID != null && copyElementID != null)
            {
                LineInfo temp = new LineInfo();
                temp.EleID = currentElementID;
                temp.SvgUID = SVGUID;
                LineInfo lineCurrent = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", temp);
                if (lineCurrent != null)
                {
                    string lineName = lineCurrent.LineName;

                    lineCurrent.LineName = lineName + "-1";
                    lineCurrent.Length = "";
                    Services.BaseService.Update("UpdateLineInfo", lineCurrent);
                    LineInfo copyElement = new LineInfo();
                    copyElement.UID = Guid.NewGuid().ToString();
                    copyElement.EleID = copyElementID;// +"1";
                    copyElement.LayerID = lineCurrent.LayerID;

                    copyElement.Length = "";
                    copyElement.LineName = lineName + "-2";
                    copyElement.LineType = lineCurrent.LineType;
                    copyElement.ObligateField1 = lineCurrent.ObligateField1;
                    copyElement.ObligateField2 = lineCurrent.ObligateField2;
                    copyElement.SvgUID = lineCurrent.SvgUID;
                    copyElement.Voltage = lineCurrent.Voltage;
                    Services.BaseService.Create<LineInfo>(copyElement);
                }
            }
        }



        void tlVectorControl1_OperationChanged(object sender, EventArgs e)
        {
            if (csOperation == CustomOperation.OP_MeasureDistance)
            {
                resetOperation();
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

        void DrawArea_OnBeforeRenderTo(object sender, PaintMapEventArgs e)
        {
            PointF pf = tlVectorControl1.DrawArea.ContentBounds.Location;
            Rectangle rt = Rectangle.Round(e.G.VisibleClipBounds);
            int offsetX = (int)(rt.Width / 2 + pf.X);
            int offsetY = (int)(rt.Height / 2 + pf.Y);
            LongLat longlat = mapview.OffSet(mapview.ZeroLongLat, mapview.Getlevel(1), -offsetX, -offsetY);
            e.G.Clear(ColorTranslator.FromHtml("#EBEAE8"));
            ImageAttributes imageA = new ImageAttributes();
            int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
            string Transparent = ConfigurationSettings.AppSettings.Get("Transparent");
            if (string.IsNullOrEmpty(Transparent)) Transparent = "#EBEAE8";
            if (chose == 1) {
                Color color = ColorTranslator.FromHtml(Transparent);
                imageA.SetColorKey(color, color);
            } else if (chose == 2) {

                Color color = ColorTranslator.FromHtml("#F4F4FB");
                Color color2 = ColorTranslator.FromHtml("#EFF0F1");
                imageA.SetColorKey(color2, color);
            } 
            mapview.Paint(e.G, (int)rt.Width, (int)rt.Height, 11, longlat.Longitude, longlat.Latitude, imageA);
        }

        void tlVectorControl1_AfterPaintPage(object sender, PaintMapEventArgs e)
        {
            try
            {
                if (LoadImage)
                {
                    int nScale = mapview.Getlevel(tlVectorControl1.ScaleRatio);
                    if (nScale == -1)
                        return;
                    LongLat longlat = LongLat.Empty;
                    //计算中心点经纬度
                    Color color3 = ColorTranslator.FromHtml("#FFFFFF");
                    e.G.Clear(color3);

                    longlat = mapview.OffSet(mapview.ZeroLongLat, nScale, (int)(e.CenterPoint.X), (int)(e.CenterPoint.Y));

                    //创建地图
                    System.Drawing.Image image = mapview.CreateMap(e.Bounds.Width, e.Bounds.Height, nScale, longlat.Longitude, longlat.Latitude);
                    string newnScale = mapview.GetMiles(nScale);
                    ImageAttributes imageAttributes = new ImageAttributes();
                    ColorMatrix matrix1 = new ColorMatrix();
                    matrix1.Matrix00 = 1f;
                    matrix1.Matrix11 = 1f;
                    matrix1.Matrix22 = 1f;
                    matrix1.Matrix33 =  this.mapOpacity / 100f;//地图透明度
                    matrix1.Matrix44 = 1f;
                    //设置地图透明度

                    imageAttributes.SetColorMatrix(matrix1, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    //Color color = ColorTranslator.FromHtml("#F4F4FB");
                    //Color color2 = ColorTranslator.FromHtml("#EFF0F1");//EFF0F1
                    ////imageAttributes2.SetColorKey(color, color);
                    ////imageAttributes2.SetColorKey(color2, color2);
                    //imageAttributes.SetColorKey(color2, color);
                    int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
                    string Transparent = ConfigurationSettings.AppSettings.Get("Transparent");
                    if (string.IsNullOrEmpty(Transparent)) Transparent = "#EBEAE8";
                    if (chose == 1) {
                        Color color = ColorTranslator.FromHtml(Transparent);//旧Mapbar新Mapbar#F4F1EC
                        imageAttributes.SetColorKey(color, color);
                    } else if (chose == 2) {

                        Color color = ColorTranslator.FromHtml("#F4F4FB");
                        Color color2 = ColorTranslator.FromHtml("#EFF0F1");
                        imageAttributes.SetColorKey(color2, color);
                    } 
                    //绘制地图
                    e.G.DrawImage((Bitmap)image, e.Bounds, 0f, 0f, (float)image.Width, (float)image.Height, GraphicsUnit.Pixel, imageAttributes);

                    //绘制中心点
                    e.G.DrawEllipse(Pens.Red, e.Bounds.Width / 2 - 2, e.Bounds.Height / 2 - 2, 4, 4);
                    e.G.DrawEllipse(Pens.Red, e.Bounds.Width / 2 - 1, e.Bounds.Height / 2 - 1, 2, 2);

                    //绘制比例尺
                    Point p1 = new Point(20, e.Bounds.Height - 30);
                    Point p2 = new Point(20, e.Bounds.Height - 20);
                    Point p3 = new Point(80, e.Bounds.Height - 20);
                    Point p4 = new Point(80, e.Bounds.Height - 30);
                    if (tlVectorControl1.CurrentOperation != ToolOperation.Roam)
                    {
                        e.G.DrawLines(new Pen(Color.Black, 2), new Point[4] { p1, p2, p3, p4 });
                        string str1 = string.Format("{0}公里", newnScale);
                        e.G.DrawString(str1, new Font("宋体", 10), Brushes.Black, 30, e.Bounds.Height - 40);
                    }
                }
            }
            catch (Exception e1) { }
            //string s = string.Format("{0}行{1}列", nRows, nCols);
            //string s = string.Format("经{0}：纬{1}", longlat.Longitude, longlat.Latitude);
            //显示中心点经纬度
            // e.G.DrawString(s, new Font("宋体", 10), Brushes.Red, 20, 40);
        }

        void DrawArea_ViewChanged(object sender, ItopVector.DrawArea.ViewChangedEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            float a = e.Bounds.Bottom;
        }

        void tlVectorControl1_DragAndDrop(object sender, DragEventArgs e)
        {
            object obj1 = e.Data.GetData("SvgElement");
            /*object obj1 = e.Data.GetData("SvgElement");
             if (obj1 is IGraph)
             {
                 IGraph graph1 = (IGraph) obj1;
                 if (graph1.ID.Contains("Substation"))
                 {
                   
                     substation sub = new substation();
                     sub.EleID = tlVectorControl1.SVGDocument.CurrentElement.ID;
                     sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                     Services.BaseService.Create<substation>(sub);
                 }
             }*/
        }



        #region 事件处理
        void tlVectorControl1_RightClick(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e)
        {
            sel_sym = "";
            sel_start_point = "";
            try
            {
                if (csOperation == CustomOperation.OP_MeasureDistance)
                {
                    tlVectorControl1.Operation = ToolOperation.Select;
                    contextMenuStrip1.Hide();
                    return;
                }
                //tlVectorControl1.DocumentSize = new SizeF(3170f, 2540f);
                //MessageBox.Show(MapType);
                tmLineConnect.Visible = false;
                SvgElementCollection elements = tlVectorControl1.SVGDocument.SelectCollection;
                if (elements.Count == 2)
                {
                    Polyline pl1 = elements[0] as Polyline;
                    Polyline pl2 = elements[1] as Polyline;
                    if (pl1 != null && pl2 != null && pl1.GetAttribute("IsLead") != "" && pl2.GetAttribute("IsLead") != "")
                    {

                        tmLineConnect.Visible = true;
                    }
                }
                if (MapType == "接线图")
                {
                    tip.Hide();
                    if (getlayer(SvgDocument.currentLayer, "背景层", tlVectorControl1.SVGDocument.getLayerList()))
                    {
                        contextMenuStrip1.Enabled = false;
                    }
                    else
                    {
                        contextMenuStrip1.Enabled = true;
                    }

                    if (tlVectorControl1.SVGDocument.CurrentElement == null ||
                       tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() != "ItopVector.Core.Figure.Use")
                    {
                        moveMenuItem.Visible = false;
                        jxtToolStripMenuItem.Visible = false;
                        w3MenuItem.Visible = false;
                        
                    }
                    else
                    {
                        if (tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("xlink:href").Contains("Substation"))
                        {
                            moveMenuItem.Visible = true;
                            jxtToolStripMenuItem.Visible = true;
                            w3MenuItem.Visible = true;
                        }
                    }
                    if (show3d == 0)
                    {
                        w3MenuItem.Visible = false;
                    }
                    if (tlVectorControl1.SVGDocument.CurrentElement == null &&
                      tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() != "ItopVector.Core.Figure.RectangleElement" &&
                      tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() !="ItopVector.Core.Figure.Polygon")
                    {
                        printToolStripMenuItem.Visible = false;
                        toolDel.Visible = false;
                        SubToolStripMenuItem.Visible = false;
                    }
                    else
                    {
                        printToolStripMenuItem.Visible = true;
                        toolDel.Visible = true; 
                        SubToolStripMenuItem.Visible = false;
                        saveImg.Visible = true;

                    }
                    if (tlVectorControl1.SVGDocument.CurrentElement!=null && tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline")
                    {
                        mUpdateMenuItem.Visible = true;
                    }
                    else
                    {
                        mUpdateMenuItem.Visible = false;
                    }
                    string guid = Guid.NewGuid().ToString();
                    if (tlVectorControl1.Operation == ToolOperation.LeadLine && linekey != "")
                    {
                        string str = "";

                        LineList1 line1 = new LineList1();
                        line1.UID = Guid.NewGuid().ToString();
                        line1.LineEleID = tlVectorControl1.SVGDocument.CurrentElement.ID;
                        line1.PointNum = ((Polyline)(tlVectorControl1.SVGDocument.CurrentElement)).Points.Length - 2;
                        line1.Coefficient = (decimal)(1.02);
                        line1.Length = TLMath.getPolylineLength(((Polyline)(tlVectorControl1.SVGDocument.CurrentElement)), Convert.ToDecimal(tlVectorControl1.ScaleRatio));
                        line1.Length2 = TLMath.getPolylineLength(((Polyline)(tlVectorControl1.SVGDocument.CurrentElement)), Convert.ToDecimal(tlVectorControl1.ScaleRatio)) * Convert.ToDecimal(1.02);
                        PointF[] pnt = ((Polyline)(tlVectorControl1.SVGDocument.CurrentElement)).Points;
                        if (pnt.Length < 3) return;
                        for (int i = 0; i < pnt.Length; i++)
                        {
                            double ang = TLMath.getLineAngle(pnt[i], pnt[i + 1], pnt[i + 2]);
                            if (ang * 57.3 > 60)
                            {
                                MessageBox.Show("线路转角不能大于60度。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tlVectorControl1.Delete();
                                return;
                            }
                            str = str + "第" + (i + 1) + "转角：" + Convert.ToDouble(ang * 57.3).ToString("##.##") + "度。\r\n";
                            if (i == pnt.Length - 3)
                            {
                                break;
                            }
                        }
                        line1.TurnAngle = str;
                        line1.col1 = linekey;
                        frmInputDialog input = new frmInputDialog();
                        if (input.ShowDialog() == DialogResult.OK)
                        {
                            line1.LineName = input.InputStr;
                            Services.BaseService.Create<LineList1>(line1);
                        }
                        else
                        {
                            tlVectorControl1.Delete();
                        }
                        linekey = "";

                    }
                    if (tlVectorControl1.Operation == ToolOperation.InterEnclosure && !SubPrint)
                    {
                       
                        System.Collections.SortedList list = new SortedList();
                        decimal s = 0;
                        ItopVector.Core.SvgElementCollection selCol = tlVectorControl1.SVGDocument.SelectCollection;
                        if (selCol.Count > 1)
                        {
                            decimal ViewScale = 1;
                            string str_Scale = tlVectorControl1.SVGDocument.getViewScale();
                            if (str_Scale != "")
                            {
                                ViewScale = Convert.ToDecimal(str_Scale);
                            }
                            string str_remark = "";
                            string str_selArea = "";
                            //string Elements = "";
                            Hashtable SelAreaCol = new Hashtable();
                            this.Cursor = Cursors.WaitCursor;
                            int t = 0;
                        Lab001:
                            t = t + 1;
                            XmlElement poly1 = (XmlElement)selCol[selCol.Count - t];
                            if (poly1.GetType().FullName != "ItopVector.Core.Figure.Polygon")
                            {
                                // selCol.Remove(selCol[selCol.Count-1]);
                                goto Lab001;
                            }
                            frmWaiting wait = new frmWaiting();
                            wait.Show(this);
                            wait.Refresh();

                            GraphicsPath gr1 = new GraphicsPath();
                            //gr1.AddRectangle(TLMath.getRectangle(poly1));
                            gr1.AddPolygon(TLMath.getPolygonPoints(poly1));
                            gr1.CloseFigure();

                            for (int i = 0; i < selCol.Count - 1; i++)
                            {
                                if (selCol[i].GetType().FullName == "ItopVector.Core.Figure.Polygon")
                                {

                                    string IsArea = ((XmlElement)selCol[i]).GetAttribute("IsArea");
                                    if (IsArea != "")
                                    {
                                        XmlElement polyn = (XmlElement)selCol[i];
                                        GraphicsPath gr2 = new GraphicsPath();
                                        //gr2.AddRectangle(TLMath.getRectangle(polyn));
                                        gr2.AddPolygon(TLMath.getPolygonPoints(polyn));
                                        gr2.CloseFigure();
                                        Region region = new Region(gr1);
                                        region.Intersect(gr2);

                                        RectangleF rect = new RectangleF();
                                        rect = tlVectorControl1.SelectedRectangle(region);

                                        decimal sub_s = TLMath.getInterPolygonArea(region, rect, ViewScale);
                                        sub_s = TLMath.getNumber2(sub_s, tlVectorControl1.ScaleRatio);
                                        SelAreaCol.Add(polyn.GetAttribute("id"), sub_s);
                                        glebeProperty _gleProp = new glebeProperty();
                                        _gleProp.EleID = polyn.GetAttribute("id");
                                        _gleProp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        IList gList = Services.BaseService.GetList("SelectglebePropertyByEleID", _gleProp);

                                        if (gList.Count > 0)
                                        {
                                            _gleProp = (glebeProperty)gList[0];
                                            list.Add(_gleProp.UseID, sub_s.ToString("#####.####"));
                                            str_selArea = str_selArea + _gleProp.EleID + "," + sub_s.ToString("#####.####") + ";";
                                            //str_remark = str_remark + "地块" + _gleProp.UseID + "选中面积为：" + sub_s.ToString() + "\r\n";
                                            s += sub_s;
                                        }
                                    }
                                }
                                if (selCol[i].GetType().FullName == "ItopVector.Core.Figure.Use")
                                {
                                    XmlElement e1 = (XmlElement)selCol[i];
                                    string str_id = e1.GetAttribute("id");

                                    substation _sub1 = new substation();
                                    _sub1.EleID = str_id;
                                    _sub1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                    _sub1 = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _sub1);
                                    if (_sub1 != null)
                                    {
                                        _sub1.glebeEleID = guid;
                                        Services.BaseService.Update("Updatesubstation", _sub1);
                                    }

                                }

                            }
                            decimal nullpoly = TLMath.getNumber2(TLMath.getPolygonArea(TLMath.getPolygonPoints(poly1), 1), tlVectorControl1.ScaleRatio) - s;

                            for (int j = 0; j < list.Count; j++)
                            {
                                if (Convert.ToString(list.GetByIndex(j)) != "")
                                {
                                    if (Convert.ToDecimal(list.GetByIndex(j)) < 1)
                                    {
                                        str_remark = str_remark + "地块" + list.GetKey(j).ToString() + "选中面积为：" + "0" + list.GetByIndex(j).ToString() + "（KM²）\r\n";
                                    }
                                    else
                                    {
                                        str_remark = str_remark + "地块" + list.GetKey(j).ToString() + "选中面积为：" + list.GetByIndex(j).ToString() + "（KM²）\r\n";
                                    }
                                }
                            }
                            XmlElement x1 = poly1;// (XmlElement)selCol[selCol.Count - 1];

                            gPro.UID = guid;
                            gPro.EleID = x1.GetAttribute("id");
                            gPro.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            gPro.ParentEleID = "0";
                            if (s != 0)
                            {
                                gPro.Area = Convert.ToDecimal(s.ToString("#####.####"));
                            }
                            else
                            {
                                gPro.Area = 0;
                            }
                            gPro.SelSonArea = str_selArea;

                            if (nullpoly < 1)
                            {
                                gPro.ObligateField10 = "0" + nullpoly.ToString("#####.####");
                            }
                            else
                            {
                                gPro.ObligateField10 = nullpoly.ToString("#####.####");
                            }

                            str_remark = str_remark + "\r\n 空白区域面积" + gPro.ObligateField10 + "（KM²）\r\n";
                            if (str_remark != "")
                            {
                                str_remark = str_remark.Substring(0, str_remark.Length - 2);
                            }

                            gPro.Remark = str_remark;
                            wait.Close();
                            this.Cursor = Cursors.Default;
                            if (s < 1)
                            {
                                MessageBox.Show("选中区域面积:" + "0" + s.ToString("#####.####") + "（KM²）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("选中区域面积:" + s.ToString("#####.####") + "（KM²）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }


                            Services.BaseService.Create<glebeProperty>(gPro);

                            IDictionaryEnumerator ISelList = SelAreaCol.GetEnumerator();
                            while (ISelList.MoveNext())
                            {
                                glebeProperty sub_gle = new glebeProperty();
                                sub_gle.EleID = ISelList.Key.ToString();
                                sub_gle.ParentEleID = gPro.EleID;
                                sub_gle.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                Services.BaseService.Update("UpdateglebePropertySelArea", sub_gle);
                            }

                            tlVectorControl1.SVGDocument.SelectCollection.Clear();
                            tlVectorControl1.SVGDocument.CurrentElement = (SvgElement)x1;
                        }
                        SubPrint = false;
                    }
                    if (tlVectorControl1.CurrentOperation == ToolOperation.InterEnclosure && SubPrint)
                    {
                        //ItopVector.Core.SvgElementCollection selCol = tlVectorControl1.SVGDocument.SelectCollection;
                        //if(selCol.Count>2){
                        //    XmlElement selArea = (SvgElement)selCol[selCol.Count - 1];

                        //    GraphicsPath gr1 = new GraphicsPath();
                        //    gr1.AddPolygon(TLMath.getPolygonPoints(selArea));
                        //    gr1.CloseFigure();
                        //    RectangleF rect= gr1.GetBounds();

                        //    SvgDocument _doc = new SvgDocument();
                        //    string svgtxt = "<?xml version=\"1.0\" encoding=\"utf-8\"?><svg id=\"svg\" width=\""+rect.Width+"\" height=\""+rect.Height+"\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:itop=\"http://www.Itop.com/itop\">";

                        //    for (int n = 0; n < selCol.Count-1;n++ )
                        //    {
                        //        //_doc.AppendChild((XmlNode)selCol[n]);
                        //        svgtxt=svgtxt+((XmlElement)selCol[n]).OuterXml+"\r\n";
                        //    }
                        //    svgtxt = svgtxt + "</svg>";
                        //    _doc.LoadXml(svgtxt);
                        //    frmSubPrint s = new frmSubPrint();
                        //    s.Show();
                        //    s.Open(_doc, rect);
                        ItopVector.Core.SvgElementCollection selCol = tlVectorControl1.SVGDocument.SelectCollection;
                        XmlElement x1 = (XmlElement)selCol[selCol.Count - 1];
                        tlVectorControl1.SVGDocument.SelectCollection.Clear();
                        tlVectorControl1.SVGDocument.CurrentElement = (SvgElement)x1;
                        SubPrint = false;
                        //}
                    }
                    if (tlVectorControl1.Operation == ToolOperation.Enclosure)
                    {

                        string Elements = "";
                        ItopVector.Core.SvgElementCollection selCol = tlVectorControl1.SVGDocument.SelectCollection;

                        for (int i = 0; i < selCol.Count - 1; i++)
                        {
                            XmlElement e1 = (XmlElement)selCol[i];
                            Elements = Elements + "'" + e1.GetAttribute("id") + "',";
                        }
                        if (Elements.Length > 0)
                        {
                            Elements = Elements.Substring(0, Elements.Length - 1);
                        }
                        XmlElement x1 = (XmlElement)selCol[selCol.Count - 1];

                        gPro.UID = Guid.NewGuid().ToString();
                        gPro.EleID = x1.GetAttribute("id");

                        gPro.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        gPro.SonUid = Elements;
                        Services.BaseService.Create<glebeProperty>(gPro);

                        tlVectorControl1.SVGDocument.SelectCollection.Clear();
                        tlVectorControl1.SVGDocument.CurrentElement = (SvgElement)x1;


                    }
                    if (tlVectorControl1.CurrentOperation == ToolOperation.LeadLine)
                    {
                        sgt1.Visible = false;
                    }
                }
            }
            catch (Exception e1)
            {
                //MessageBox.Show(e1.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tlVectorControl1.SVGDocument.SelectCollection.Clear();
            }
            finally
            {
                tlVectorControl1.Operation = ToolOperation.Select;
                tlVectorControl1.Operation = ToolOperation.FreeTransform;

            }
        }

        void tlVectorControl1_MoveOut(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e)
        {
            //fInfo.Hide();
            //tip.Hide();
            //tlVectorControl1.Refresh();
        }

        void tlVectorControl1_MoveIn(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e)
        {
            //try
            //{
            //    gPro.UID = e.SvgElement.ID;
            //    if (MapType == "接线图")
            //    {
            //        int ViewScale = 1;
            //        string str_Scale = tlVectorControl1.SVGDocument.getViewScale();
            //        if (str_Scale != "")
            //        {
            //            ViewScale = Convert.ToInt32(str_Scale);
            //        }


            //    }
            //}
            //catch (Exception e1)
            //{
            //    MessageBox.Show(e1.Message);
            //}
        }

        void tlVectorControl1_DoubleLeftClick(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e)
        {
            XmlElement xml1 = ((XmlElement)(e.Elements[0]));
            string str_name = xml1.GetAttribute("xlink:href");
            if (MapType == "接线图")
            {

                if (str_name.Contains("Substation"))
                {
                    string infoname = xml1.GetAttribute("info-name");
                    ParentUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    Save();
                    ParentUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    SVGFILE svg_temp = new SVGFILE();
                    svg_temp.SUID = xml1.GetAttribute("id");
                    svg_temp.FILENAME = getBdzName(svg_temp.SUID);// infoname;
                    //svg_temp.FILENAME = ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).GetAttribute("info-name");
                    string strWhere = string.Format("suid='{0}' or filename='{1}' ", svg_temp.SUID, svg_temp.FILENAME);
                    IList svglist = Services.BaseService.GetList("SelectSVGFILEByWhere", strWhere);
                    OpenJXT(svglist, svg_temp);

                    //this.Text = infoname;
                    //tlVectorControl1.SVGDocument.FileName = infoname;
                    //frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
                    //frmlar.Progtype = MapType;
                    //frmlar.InitData();
                    //JxtBar();
                    tlVectorControl1.SVGDocument.SelectCollection.Clear();
                    //tlVectorControl1.Delete();
                    tlVectorControl1.Refresh();


                    if (tlVectorControl1.SVGDocument.CurrentElement == null || tlVectorControl1.SVGDocument.CurrentElement.ID == "svg")
                    {
                        return;
                    }
                    ParentUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    Save();
                    ParentUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    //SVGFILE svg_temp = new SVGFILE();
                    svg_temp.SUID = ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).GetAttribute("id");
                    svg_temp.FILENAME = getBdzName(svg_temp.SUID);// ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).GetAttribute("info-name");
                    string strWhere2 = string.Format("suid='{0}' or filename='{1}' ", svg_temp.SUID, svg_temp.FILENAME);
                    IList svglist2 = Services.BaseService.GetList("SelectSVGFILEByWhere", strWhere2);
                    OpenJXT(svglist2, svg_temp);

                }
            }
        }
        /// <summary>
        /// 测距
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tlVectorControl1_LeftClick2(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e)
        {

        }
        CustomOperation csOperation = CustomOperation.OP_Default;

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
        void tlVectorControl1_LeftClick(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e)
        {
            //SvgElement ele2 = tlVectorControl1.CreateBySymbolID("kbs-8", (tlVectorControl1.DrawArea.PointToView(e.Mouse.Location)));
            //ele2 = tlVectorControl1.AddShape(ele2, Point.Empty);
            if (tlVectorControl1.SVGDocument.CurrentElement != null &&
                tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("xlink:href").Contains("XL_GT_1"))
            {
                sel_start_point = tlVectorControl1.SVGDocument.CurrentElement.ID;
            }

            if (sel_sym == "bt_start")
            {
                SvgElement ele = tlVectorControl1.CreateBySymbolID("XL_GT_1", (tlVectorControl1.DrawArea.PointToView(e.Mouse.Location)));
                ele = tlVectorControl1.AddShape(ele, Point.Empty);
                ele.SetAttribute("order", "0");
                ele.SetAttribute("start_point", ele.ID);
            }
            if (sel_sym == "bt_end")
            {
                SvgElement ele = tlVectorControl1.CreateBySymbolID("XL_GT_2", (tlVectorControl1.DrawArea.PointToView(e.Mouse.Location)));
                ele.SetAttribute("start_point", sel_start_point);
                ele.SetAttribute("order", "999");
                tlVectorControl1.AddShape(ele, Point.Empty);
            }
            if (sel_sym == "bt_must")
            {
                SvgElement ele = tlVectorControl1.CreateBySymbolID("XL_GT_3", (tlVectorControl1.DrawArea.PointToView(e.Mouse.Location)));
                ele.SetAttribute("start_point", sel_start_point);
                tlVectorControl1.AddShape(ele, Point.Empty);
                frmInputNum num = new frmInputNum();
                num.ShowDialog();
                ele.SetAttribute("order", num.InputStrSEL);
            }
            if (sel_sym == "bt_point")
            {
                SvgElement ele = tlVectorControl1.CreateBySymbolID("XL_GT_4", (tlVectorControl1.DrawArea.PointToView(e.Mouse.Location)));
                ele.SetAttribute("start_point", sel_start_point);
                tlVectorControl1.AddShape(ele, Point.Empty);
                frmInputNum num = new frmInputNum();
                num.ShowDialog();
                ele.SetAttribute("order", num.InputStrSEL);
            }
            //if (sel_sym != "" && sel_start_point!="")
            //{
            //    XmlNodeList il= tlVectorControl1.SVGDocument.SelectNodes("svg/use [@start_point='" + sel_start_point + "']");
            //    if (il.Count < 3)
            //    {
            //        return;
            //    }
            //    for (int i = 0; i < il.Count;i++ )
            //    {
            //        Use u = (Use)il[i];
            //        PointF f = u.CenterPoint;
                    
            //    }
            //    //PointF[] pnt = ((Polyline)(tlVectorControl1.SVGDocument.CurrentElement)).Points;
            //    //if (pnt.Length < 3) return;
            //    for (int i = 0; i < il.Count; i++)
            //    {
            //        double ang = TLMath.getLineAngle(((Use)il[i]).CenterPoint, ((Use)il[i + 1]).CenterPoint, ((Use)il[i + 2]).CenterPoint);
            //        if (ang * 57.3 > 60)
            //        {
            //            MessageBox.Show("线路转角不能大于60度。\r\n "+"第" + (i + 1) + "转角：" + Convert.ToDouble(ang * 57.3).ToString("##.##") + "度。\r\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //           // tlVectorControl1.Delete();
            //            return;
            //        }
                    
            //        if (i == il.Count - 3)
            //        {
            //            break;
            //        }
            //    }
            //}
            if (csOperation == CustomOperation.OP_MeasureDistance)
            {
                Polyline pl = tlVectorControl1.SVGDocument.CurrentElement as Polyline;
                if (pl.Points.Length > 1)
                {
                    double d = 0;
                    for (int i = 1; i < pl.Points.Length; i++)
                    {
                        d += this.mapview.CountLength(pl.Points[i - 1], pl.Points[i]);
                    }
                    label1.Text = d + "公里";

                    Point pt = new Point(e.Mouse.X, e.Mouse.Y);
                    pt = PointToClient(tlVectorControl1.PointToScreen(pt));
                    //tlVectorControl1.SetToolTip(label1.Text);
                    label1.Left = pt.X;
                    label1.Top = pt.Y;

                    label1.Visible = true;
                }
                return;
            }
            //System.IO.FileInfo ff = new System.IO.FileInfo("c:\\1111.txt");
            //System.IO.StreamWriter wt= ff.CreateText();
            //wt.Write(tlVectorControl1.SVGDocument.OuterXml);
            //wt.Close();
            //SortedList l = new SortedList();
            //l.Add("1", "aaa");
            //l.Add("5", "bbb");
            //l.Add("2", "ccc");


            //if (tlVectorControl1.ScaleRatio < 0.1f) {
            //    tlVectorControl1.ScaleRatio = 0.1f;
            //    scaleBox.ComboBoxEx.Text = "";
            //    scaleBox.SelectedText = "10%";
            //}
            tip.Hide();
            fInfo.Hide();

            if (tlVectorControl1.Operation == ToolOperation.Text)
            {
                frmTextInput ft = new frmTextInput();
                if (ft.ShowDialog() == DialogResult.OK)
                {
                    string txt = ft.Content;
                    XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                    Point point1 = tlVectorControl1.PointToView(new Point(e.Mouse.X, e.Mouse.Y));
                    n1.SetAttribute("x", point1.X.ToString());
                    n1.SetAttribute("y", point1.Y.ToString());
                    n1.InnerText = txt;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.Operation = ToolOperation.Select;
                }
            }
            if (MapType == "规划统计")
            {
                try
                {
                    if (e.SvgElement.ID == "svg")
                    {
                        MapType = "接线图";
                        return;
                    }
                    if (((Polygon)e.SvgElement) == null) return;
                    XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                    Point point1 = tlVectorControl1.PointToView(new Point(e.Mouse.X, e.Mouse.Y));
                    n1.SetAttribute("cx", point1.X.ToString());
                    n1.SetAttribute("cy", point1.Y.ToString());
                    n1.SetAttribute("r", "42.5");
                    n1.SetAttribute("r", "42.5");
                    //n1.SetAttribute("layer", getlayer("供电区域层", tlVectorControl1.SVGDocument.getLayerList()).ID);
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);

                    frmMainProperty fmain = new frmMainProperty();
                    glebeProperty gp = new glebeProperty();
                    gp.EleID = ((XmlElement)e.SvgElement).GetAttribute("id");
                    gp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    //fmain.InitData(gp); ///////////////////////////////////

                    XmlNodeList nlist = tlVectorControl1.SVGDocument.GetElementsByTagName("use");

                    PointF[] tfArray1 = TLMath.getPolygonPoints((XmlElement)e.SvgElement);
                    string str220 = "";
                    string str110 = "";
                    string str66 = "";
                    GraphicsPath selectAreaPath = new GraphicsPath();
                    selectAreaPath.AddLines(tfArray1);
                    selectAreaPath.CloseFigure();
                    //Matrix x=new Matrix(
                    //Region region1 = new Region(selectAreaPath);
                    for (int i = 0; i < nlist.Count; i++)
                    {
                        float OffX = 0f;
                        float OffY = 0f;
                        Use use = (Use)nlist[i];
                        if (use.GetAttribute("xlink:href").Contains("Substation"))
                        {
                            string strMatrix = use.GetAttribute("transform");
                            if (strMatrix != "")
                            {
                                strMatrix = strMatrix.Replace("matrix(", "");
                                strMatrix = strMatrix.Replace(")", "");
                                string[] mat = strMatrix.Split(',');
                                if (mat.Length > 5)
                                {
                                    OffX = Convert.ToSingle(mat[4]);
                                    OffY = Convert.ToSingle(mat[5]);
                                }
                            }
                            PointF TempPoint = TLMath.getUseOffset(use.GetAttribute("xlink:href"));
                            if (selectAreaPath.IsVisible(use.X + TempPoint.X + OffX, use.Y + TempPoint.Y + OffY))
                            {
                                if (use.GetAttribute("xlink:href").Contains("220"))
                                {
                                    str220 = str220 + "'" + use.GetAttribute("id") + "',";
                                }
                                if (use.GetAttribute("xlink:href").Contains("110"))
                                {
                                    str110 = str110 + "'" + use.GetAttribute("id") + "',";
                                }
                                if (use.GetAttribute("xlink:href").Contains("66"))
                                {
                                    str66 = str66 + "'" + use.GetAttribute("id") + "',";
                                }
                            }
                        }
                    }
                    if (str220.Length > 1)
                    {
                        str220 = str220.Substring(0, str220.Length - 1);
                    }
                    if (str110.Length > 1)
                    {
                        str110 = str110.Substring(0, str110.Length - 1);
                    }
                    if (str66.Length > 1)
                    {
                        str66 = str66.Substring(0, str66.Length - 1);
                    }
                    fmain.InitData(gp, str220, str110, str66);

                    XmlElement t1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                    Point point2 = tlVectorControl1.PointToView(new Point(e.Mouse.X, e.Mouse.Y));
                    t1.SetAttribute("x", Convert.ToString(point2.X - 20));
                    t1.SetAttribute("y", Convert.ToString(point2.Y - 10));
                    // t1.SetAttribute("layer", getlayer("供电区域层", tlVectorControl1.SVGDocument.getLayerList()).ID);
                    t1.SetAttribute("layer", SvgDocument.currentLayer);
                    t1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                    t1.SetAttribute("font-famliy", "宋体");
                    t1.SetAttribute("font-size", "14");
                    t1.InnerText = fmain.glebeProp.Area + "\r\n" + fmain.glebeProp.Burthen; //+"\r\n" + fmain.glebeProp.Number;
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(t1);
                    tlVectorControl1.Refresh();
                    fmain.Dispose();
                    MapType = "接线图";
                }
                catch (Exception e2) { MapType = "接线图"; }
            }
            if (tlVectorControl1.SVGDocument.SelectCollection.Count > 1)
            {
                return;
            }
            decimal ViewScale = 1;
            string str_Scale = tlVectorControl1.SVGDocument.getViewScale();
            if (str_Scale != "")
            {
                ViewScale = Convert.ToDecimal(str_Scale);
            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Polygon")
            {
                string IsArea = ((XmlElement)e.SvgElement).GetAttribute("IsArea");
                if (IsArea != "")
                {

                    XmlElement n1 = (XmlElement)e.SvgElement;
                    string str_points = n1.GetAttribute("points");
                    string[] points = str_points.Split(",".ToCharArray());
                    PointF[] pnts = new PointF[points.Length];

                    for (int i = 0; i < points.Length; i++)
                    {
                        string[] pointsXY = points[i].Split(" ".ToCharArray());
                        pnts[i].X = Convert.ToSingle(pointsXY[0]);
                        pnts[i].Y = Convert.ToSingle(pointsXY[1]);
                    }
                    decimal temp1 = TLMath.getPolygonArea(pnts, 1);

                    temp1 = TLMath.getNumber2(temp1, 1);
                    SelUseArea = temp1.ToString("#####.####");
                    //tip.Text = "区域面积：" + SelUseArea;
                    if (SelUseArea != "")
                    {
                        if (Convert.ToDecimal(SelUseArea) >= 1)
                        {
                            fInfo.Info = "区域面积：" + SelUseArea + "（KM²）";
                        }
                        else
                        {
                            fInfo.Info = "区域面积： 0" + SelUseArea + "（KM²）";
                        }
                    }
                    fInfo.Top = e.Mouse.Y;
                    fInfo.Left = e.Mouse.X;
                    fInfo.Width = (fInfo.Info.Length) * 12;
                    if (SelUseArea != "")
                    {
                        fInfo.Show();

                    }
                    //tip.ShowToolTip();
                }

            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Line")
            {
                string IsLead = ((XmlElement)e.SvgElement).GetAttribute("IsLead");
                if (IsLead != "")
                {
                    Line line = (Line)e.SvgElement;
                    decimal temp1 = TLMath.getLineLength(line, 1);
                    temp1 = TLMath.getNumber(temp1, tlVectorControl1.ScaleRatio);
                    string len = temp1.ToString("#####.####");
                    LineLen = len;
                    LineInfo lineInfo = new LineInfo();
                    lineInfo.EleID = e.SvgElement.ID;
                    lineInfo.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    LineInfo _lineTemp = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", lineInfo);

                    if ((len != "") && (_lineTemp != null))
                    {
                        if (Convert.ToDecimal(len) >= 1)
                        {
                            fInfo.Info = "线路名称：" + _lineTemp.LineName + " 线路长度：" + len + "（KM）\r\n" + "导线型号：" + _lineTemp.LineType + " 电压等级：" + _lineTemp.Voltage + "kV 投产年限：" + _lineTemp.ObligateField3;
                        }
                        else
                        {
                            fInfo.Info = "线路名称：" + _lineTemp.LineName + " 线路长度： 0" + len + "（KM）\r\n" + "导线型号：" + _lineTemp.LineType + " 电压等级：" + _lineTemp.Voltage + "kV 投产年限：" + _lineTemp.ObligateField3;
                        }
                    }
                    else if (len != "")
                    {
                        if (Convert.ToDecimal(len) >= 1)
                        {
                            fInfo.Info = "线路名称：" + " " + "线路长度：" + len + "（KM）\r\n" + "导线型号：" + " " + " 电压等级：" + " " + " 投产年限：" + " ";
                        }
                        else
                        {
                            fInfo.Info = "线路名称：" + " " + "线路长度： 0" + len + "（KM）\r\n" + "导线型号：" + " " + " 电压等级：" + " " + " 投产年限：" + " ";
                        }
                    }
                    fInfo.Top = e.Mouse.Y;
                    fInfo.Left = e.Mouse.X;
                    fInfo.Width = (fInfo.Info.Length) * 7;
                    fInfo.Height = 50;
                    if (len != "")
                    {
                        fInfo.Show();

                    }

                }
            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline")
            {
                string IsLead = ((XmlElement)e.SvgElement).GetAttribute("IsLead");
                if (IsLead != "")
                {
                    Polyline polyline = (Polyline)e.SvgElement;
                    decimal temp1 = TLMath.getPolylineLength(polyline, 1);
                    temp1 = TLMath.getNumber(temp1, tlVectorControl1.ScaleRatio);
                    string len = temp1.ToString("#####.####");
                    LineLen = len;
                    LineInfo lineInfo = new LineInfo();
                    lineInfo.EleID = e.SvgElement.ID;
                    lineInfo.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    LineInfo _lineTemp = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", lineInfo);

                    if ((len != "") && (_lineTemp != null))
                    {
                        if (Convert.ToDecimal(len) >= 1)
                        {
                            fInfo.Info = "线路名称：" + _lineTemp.LineName + " 线路长度：" + len + "（KM）\r\n" + "导线型号：" + _lineTemp.LineType + " 电压等级：" + _lineTemp.Voltage + "kV 投产年限：" + _lineTemp.ObligateField3;
                        }
                        else
                        {
                            fInfo.Info = "线路名称：" + _lineTemp.LineName + " 线路长度： 0" + len + "（KM）\r\n" + "导线型号：" + _lineTemp.LineType + " 电压等级：" + _lineTemp.Voltage + "kV 投产年限：" + _lineTemp.ObligateField3;
                        }
                    }
                    else if (len != "")
                    {
                        if (Convert.ToDecimal(len) >= 1)
                        {
                            fInfo.Info = "线路名称：" + " " + "线路长度：" + len + "（KM）\r\n" + "导线型号：" + " " + " 电压等级：" + " " + " 投产年限：" + " ";
                        }
                        else
                        {
                            fInfo.Info = "线路名称：" + " " + "线路长度： 0" + len + "（KM）\r\n" + "导线型号：" + " " + " 电压等级：" + " " + " 投产年限：" + " ";
                        }
                    }
                    fInfo.Top = e.Mouse.Y;
                    fInfo.Left = e.Mouse.X;
                    fInfo.Width = (fInfo.Info.Length) * 7;
                    fInfo.Height = 50;
                    //fInfo.Right = fInfo.Left+fInfo.Info.Length*10;
                    if (len != "")
                    {
                        fInfo.Show();
                    }
                }
            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Use")
            {
                string aaa = ((Use)e.SvgElement).RefElement.ID;
                //if (!aaa.Contains("Substation"))
                //{
                //    return;
                //}

                string IsLead = ((XmlElement)e.SvgElement).GetAttribute("IsLead");

                if (aaa.Contains("Substation"))
                {
                    substation sub = new substation();
                    sub.EleID = e.SvgElement.ID;
                    sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    substation _subTemp = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", sub);
                    if (_subTemp != null)
                    {
                        fInfo.Info = "变电站名称：" + _subTemp.EleName + " 容量：" + _subTemp.Number + "MVA\r\n" + " 电压等级：" + _subTemp.ObligateField1 + "kV 最大负荷：" + _subTemp.Burthen + "MW \r\n 负荷率：" + _subTemp.ObligateField2 + " 投产年限：" + _subTemp.ObligateField5;
                    }
                    else
                    {
                        fInfo.Info = "变电站名称：" + " " + " 容量：0" + "MVA" + "\r\n 电压等级： 最大负荷： \r\n 负荷率： 投产年限：";
                    }
                    fInfo.Top = e.Mouse.Y;
                    fInfo.Left = e.Mouse.X;
                    fInfo.Width = (fInfo.Info.Length) * 5;
                    fInfo.Height = 60;
                    fInfo.Show();
                }
                if (aaa.Contains("kbs") || aaa.Contains("fjx") || aaa.Contains("byq"))
                {
                    string s_name = "";
                    if (aaa.Contains("kbs"))
                    {
                        s_name = "开闭所";
                    }
                    if (aaa.Contains("fjx"))
                    {
                        s_name = "分接箱";
                    }
                    if (aaa.Contains("byq"))
                    {
                        s_name = "变压器";
                    }
                    PSP_Gra_item sub = new PSP_Gra_item();
                    sub.EleID = e.SvgElement.ID;
                    sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    sub.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    PSP_Gra_item _subTemp = (PSP_Gra_item)Services.BaseService.GetObject("SelectPSP_Gra_itemByEleIDKey", sub);
                    if (_subTemp != null)
                    {
                        fInfo.Info = s_name + "编号：" + _subTemp.EleKeyID + "\r\n 名称：" + _subTemp.EleName;
                    }
                    else
                    {
                        fInfo.Info = s_name + "编号：   \r\n 名称： ";
                    }
                    fInfo.Top = e.Mouse.Y;
                    fInfo.Left = e.Mouse.X;
                    fInfo.Width = (fInfo.Info.Length) * 8;
                    fInfo.Height = 60;
                    fInfo.Show();
                }
            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.ConnectLine")
            {
                ConnectLine cline = (ConnectLine)tlVectorControl1.SVGDocument.CurrentElement;
                if (cline.StartGraph != null)
                {
                    string code = ((XmlElement)cline.StartGraph).GetAttribute("devxldm");

                    if (code != "")
                    {
                        xltProcessor.SelectLine(code);
                        tlVectorControl1.CurrentOperation = ToolOperation.Select;
                    }
                }
            }
            /*  if (tlVectorControl1.Operation == ToolOperation.LeadLine)
              {
                  string gt = sgt1.Text;
                  XmlElement u1 = tlVectorControl1.SVGDocument.CreateElement("use") as Use;
                  Point point1 = tlVectorControl1.PointToView(new Point(e.Mouse.X, e.Mouse.Y));
                  u1.SetAttribute("xlink:href", "#" + gt + "0");
                  u1.SetAttribute("x", point1.X.ToString());
                  u1.SetAttribute("y", point1.Y.ToString());
                  u1.SetAttribute("layer", SvgDocument.currentLayer);
                  u1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                  tlVectorControl1.SVGDocument.RootElement.AppendChild(u1);
              }*/
            //tlVectorControl1.SVGDocument.CurrentElement = null;
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
        void tlVectorControl1_Move(object sender, EventArgs e)
        {
            //SvgElement element = tlVectorControl1.SVGDocument.CurrentLayer as SvgElement;
            //ItopVector.DrawArea.SvgElementSelectedEventArgs eClick = new ItopVector.DrawArea.SvgElementSelectedEventArgs(element);           
            //tlVectorControl1_LeftClick(sender, eClick);
        }
        void tlVectorControl1_OnTipEvent(object sender, string tooltip, byte TipType)
        {
            try
            {
                string[] tip ={ "" };
                if (TipType == 0)
                {
                    LabelItem item = this.dotNetBarManager1.GetItem("plCoord") as LabelItem;
                    if (item != null)
                        item.Text = "坐标 " + tooltip;
                    LabelItem jwditem = this.dotNetBarManager1.GetItem("jwd") as LabelItem;
                    if (jwditem != null)

                        tip = tooltip.Split(',');
                    if (tip.Length > 1)
                    {
                        try
                        {
                            LongLat temp = mapview.OffSetZero(-(int)(Convert.ToInt32(tip[0]) * tlVectorControl1.ScaleRatio), -(int)(Convert.ToInt32(tip[1]) * tlVectorControl1.ScaleRatio));

                            string[] jd = temp.Longitude.ToString("####.####").Split('.');
                            int d1 = Convert.ToInt32(jd[0]);
                            string[] df1 = Convert.ToString(Convert.ToDecimal("0." + jd[1]) * 60).Split('.');
                            int f1 = Convert.ToInt32(df1[0]);
                            decimal m1 = Convert.ToDecimal("0." + df1[1]) * 60;

                            string[] wd = temp.Latitude.ToString("####.####").Split('.');
                            int d2 = Convert.ToInt32(wd[0]);
                            string[] df2 = Convert.ToString(Convert.ToDecimal("0." + wd[1]) * 60).Split('.');
                            int f2 = Convert.ToInt32(df2[0]);
                            decimal m2 = Convert.ToDecimal("0." + df2[1]) * 60;
                           
                            jwditem.Text = "经纬度: " + d1.ToString() + "°" + f1.ToString() + "′" + m1.ToString("##.#") + "″," + d2.ToString() + "°" + f2.ToString() + "′" + m2.ToString("##.#") + "″";
                        }
                        catch { }
                    }
                }
                else if (TipType == 1)
                {
                    LabelItem item = (LabelItem)this.dotNetBarManager1.GetItem("plTip");
                    if (item != null)
                        item.Text = tooltip;
                }
                else if (TipType == 2)
                {
                    LabelItem item = (LabelItem)this.dotNetBarManager1.GetItem("plColumn");
                    if (item != null)
                        item.Text = tooltip;
                }
            }
            catch (Exception ee) { string aa = ee.Message; }
        }

        /*public void ResetPoly()
        {
            SvgElementCollection col= tlVectorControl1.SVGDocument.SelectCollection;
            if(col.Count<1){
                return;
            }
            SvgElementCollection.ISvgElementEnumerator enumerator1 = tlVectorControl1.DrawArea.ElementList.GetEnumerator();
            foreach(SvgElement ele in col){
                if (ele.GetType().ToString() == "ItopVector.Core.Figure.Polygon")
                {
                    glebeProperty p=new glebeProperty();
                    p.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    p.EleID = ((IGraph)ele).ID;
                    p = Services.BaseService.GetObject("SelectglebePropertyByEleID", p);
                    if(p!=null){
                        PointF[] tfArray1 = TLMath.getPolygonPoints(ele);
                        GraphicsPath selectAreaPath = new GraphicsPath();
                        selectAreaPath.AddLines(tfArray1);
                        selectAreaPath.CloseFigure();
                        Region region1 = new Region(selectAreaPath);
                        while (enumerator1.MoveNext())
                        {
                            IGraph graph1 = (IGraph)enumerator1.Current;
                            GraphicsPath path1 = (GraphicsPath)graph1.GPath.Clone();
                            path1.Transform(graph1.GraphTransform.Matrix);
                            Region region2 = new Region(path1);
                            region2.Intersect(region1);
                            if (!region2.GetBounds(Graphics.FromHwnd(IntPtr.Zero)).IsEmpty)
                            {
                                glebeProperty p1 = new glebeProperty();
                                p1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                p1.EleID = graph1.ID;
                                p1 = Services.BaseService.GetObject("SelectglebePropertyByEleID", p1);
                                if(p1!=null){
                                    p1.ParentEleID = p.UID;
                                    Services.BaseService.Update("UpdateglebePropertyAreaAll", p1);
                                }
                            }
                        }
                    }
                }
            }
            MessageBox.Show("更新完成。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }*/
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try    
            {
                if (e.ClickedItem.Text == "属性")
                {
                    XmlElement xml1 = (XmlElement)tlVectorControl1.SVGDocument.CurrentElement;
                    //PointF[] pf = TLMath.getPolygonPoints(xml1);

                    //((Polygon)xml1).Transform.Matrix.TransformPoints(pf);
                    // 规划
                    if (getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl1.SVGDocument.getLayerList()))
                    {



                        if (xml1 == null || tlVectorControl1.SVGDocument.CurrentElement.ID == "svg")
                        {
                            MessageBox.Show("请先选择规划区域。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.RectangleElement")
                        {
                            //frmImgManager frm = new frmImgManager();
                            //frm.Show();
                        }
                        if (tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polygon")
                        {
                            XmlNodeList n1 = tlVectorControl1.SVGDocument.GetElementsByTagName("use");
                            PointF[] tfArray1 = TLMath.getPolygonPoints(xml1);
                            string str220 = "";
                            string str110 = "";
                            string str66 = "";

                            GraphicsPath selectAreaPath = new GraphicsPath();
                            selectAreaPath.AddLines(tfArray1);
                            selectAreaPath.CloseFigure();
                            //Matrix x=new Matrix(
                            //Region region1 = new Region(selectAreaPath);
                            for (int i = 0; i < n1.Count; i++)
                            {
                                float OffX = 0f;
                                float OffY = 0f;
                                bool ck = false;
                                Use use = (Use)n1[i];
                                if (use.GetAttribute("xlink:href").Contains("Substation"))
                                {
                                    string strMatrix = use.GetAttribute("transform");
                                    if (strMatrix != "")
                                    {
                                        strMatrix = strMatrix.Replace("matrix(", "");
                                        strMatrix = strMatrix.Replace(")", "");
                                        string[] mat = strMatrix.Split(',');
                                        if (mat.Length > 5)
                                        {
                                            OffX = Convert.ToSingle(mat[4]);
                                            OffY = Convert.ToSingle(mat[5]);
                                        }
                                    }
                                    if (frmlar.getSelectedLayer().Contains(use.GetAttribute("layer")))
                                    {
                                        ck = true;
                                    }
                                    PointF TempPoint = TLMath.getUseOffset(use.GetAttribute("xlink:href"));
                                    if (selectAreaPath.IsVisible(use.X + TempPoint.X + OffX, use.Y + TempPoint.Y + OffY) && ck)
                                    {
                                        if (use.GetAttribute("xlink:href").Contains("220"))
                                        {
                                            str220 = str220 + "'" + use.GetAttribute("id") + "',";
                                        }
                                        if (use.GetAttribute("xlink:href").Contains("110"))
                                        {
                                            str110 = str110 + "'" + use.GetAttribute("id") + "',";
                                        }
                                        if (use.GetAttribute("xlink:href").Contains("66"))
                                        {
                                            str66 = str66 + "'" + use.GetAttribute("id") + "',";
                                        }
                                    }
                                }
                            }
                            if (str220.Length > 1)
                            {
                                str220 = str220.Substring(0, str220.Length - 1);
                            }
                            if (str110.Length > 1)
                            {
                                str110 = str110.Substring(0, str110.Length - 1);
                            }
                            if (str66.Length > 1)
                            {
                                str66 = str66.Substring(0, str66.Length - 1);
                            }
                            glebeProperty _gle = new glebeProperty();
                            _gle.EleID = tlVectorControl1.SVGDocument.CurrentElement.ID;
                            _gle.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;

                            IList<glebeProperty> UseProList = Services.BaseService.GetList<glebeProperty>("SelectglebePropertyByEleID", _gle);
                            if (UseProList.Count > 0)
                            {
                                _gle = UseProList[0];
                                _gle.LayerID = SvgDocument.currentLayer;
                                frmMainProperty f = new frmMainProperty();

                                f.InitData(_gle, str220, str110, str66);

                                f.ShowDialog();
                                if (f.checkBox1.Checked == false)
                                {
                                    tlVectorControl1.SVGDocument.RootElement.RemoveChild(tlVectorControl1.SVGDocument.CurrentElement);
                                }
                                //tlVectorControl1.Refresh();
                            }
                            //}
                        }

                    }
                    if (getlayer(SvgDocument.currentLayer, "城市规划层", tlVectorControl1.SVGDocument.getLayerList()))
                    {

                        if (tlVectorControl1.SVGDocument.getRZBRatio() != "")
                        {
                            rzb = tlVectorControl1.SVGDocument.getRZBRatio();
                        }


                        if (xml1 == null || tlVectorControl1.SVGDocument.CurrentElement.ID == "svg")
                        {
                            MessageBox.Show("请先选择地块。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polygon")
                        {
                            string IsArea = ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).GetAttribute("IsArea");
                            if (IsArea != "")
                            {
                                frmProperty f = new frmProperty();
                                if (SelUseArea == "") { SelUseArea = "0"; }
                                f.InitData(xml1.GetAttribute("id"), tlVectorControl1.SVGDocument.SvgdataUid, SelUseArea, rzb, SvgDocument.currentLayer);
                                //f.ShowDialog();
                                if (progtype != "城市规划层")
                                {
                                    f.IsReadonly = true;
                                }
                                if (f.ShowDialog() == DialogResult.OK)
                                {
                                    SVG_ENTITY ent = new SVG_ENTITY();
                                    if (f.IsCreate)
                                    {
                                        ent.SUID = Guid.NewGuid().ToString();
                                        ent.EleID = f.gPro.EleID;
                                        ent.layerID = f.gPro.LayerID;
                                        ent.MDATE = System.DateTime.Now;
                                        ent.NAME = f.gPro.UseID;
                                        ent.svgID = f.gPro.SvgUID;
                                        ent.TYPE = "polygon-dk";
                                        //ent.voltage = f.gPro.Voltage;
                                        Services.BaseService.Create<SVG_ENTITY>(ent);
                                    }
                                    else
                                    {
                                        ent.NAME = f.gPro.UseID;
                                        ent.layerID = f.gPro.LayerID;
                                        ent.MDATE = System.DateTime.Now;
                                        //ent.voltage = fl.Line.Voltage;
                                        Services.BaseService.Update<SVG_ENTITY>(ent);
                                    }
                                    if (f.gPro.ObligateField1 != "")
                                    {
                                        string color1 = ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(f.gPro.ObligateField1)));
                                        color1 = "fill:" + color1 + ";";
                                        ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).SetAttribute("style", color1);
                                        tlVectorControl1.UpdateProperty();
                                    }

                                }
                            }
                        }
                    }

                    if (tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Line" || tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline")
                    {
                        string lineWidth = "2";
                        string IsLead = ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).GetAttribute("IsLead");
                        if (IsLead != "")
                        {
                            frmLineProperty fl = new frmLineProperty();
                            fl.LineNode = tlVectorControl1.SVGDocument.CurrentElement;
                            fl.InitData(tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("id"), tlVectorControl1.SVGDocument.SvgdataUid, LineLen, SvgDocument.currentLayer);
                            if (fl.ShowDialog() == DialogResult.OK)
                            {
                                SVG_ENTITY ent = new SVG_ENTITY();
                                if (fl.IsCreate)
                                {
                                    ent.SUID = Guid.NewGuid().ToString();
                                    ent.EleID = fl.Line.EleID;
                                    ent.layerID = fl.Line.LayerID;
                                    ent.MDATE = System.DateTime.Now;
                                    ent.NAME = fl.Line.LineName;
                                    ent.svgID = fl.Line.SvgUID;
                                    ent.TYPE = "line";
                                    ent.voltage =Convert.ToInt32(fl.Line.Voltage);
                                    Services.BaseService.Create<SVG_ENTITY>(ent);
                                }
                                else
                                {
                                    ent.NAME = fl.Line.LineName;
                                    ent.layerID = fl.Line.LayerID;
                                    ent.MDATE = System.DateTime.Now;
                                    ent.voltage =Convert.ToInt32( fl.Line.Voltage);
                                    Services.BaseService.Update<SVG_ENTITY>(ent);
                                }

                                lineWidth = fl.LineWidth;
                                string styleValue = "";
                                if (fl.Line.ObligateField1 == "规划")
                                {
                                    styleValue = "stroke-dasharray:" + ghType + ";stroke-width:" + lineWidth + ";";
                                }
                                else
                                {
                                    styleValue = "stroke-width:" + lineWidth + ";";
                                }
                                //string aa= ColorTranslator.ToHtml(Color.Black);
                                styleValue = styleValue + "stroke:" + ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(fl.Line.ObligateField2)));
                                SvgElement se = tlVectorControl1.SVGDocument.CurrentElement;
                                se.RemoveAttribute("style");
                                se.SetAttribute("style", styleValue);
                                se.SetAttribute("info-name", fl.Line.LineName);
                                
                            }
                        }
                    }
                    if (xml1.GetAttribute("xlink:href").Contains("Substation"))
                    {
                        string lab = xml1.GetAttribute("xlink:href");

                        float x = 0f;   
                         float y = 0f;

                         x = ((Use)xml1).X;

                         y = ((Use)xml1).Y;
                         

                        PointF p1 = new PointF(x, y);
                        PointF[] pnt = new PointF[1];
                        pnt[0] = p1;
                        Use temp = xml1.Clone() as Use;
                        temp.Transform.Matrix.TransformPoints(pnt);

                        LongLat templat = mapview.OffSetZero(-(int)(pnt[0].X * tlVectorControl1.ScaleRatio), -(int)(pnt[0].Y * tlVectorControl1.ScaleRatio));

                        string[] jd = templat.Longitude.ToString("####.####").Split('.');
                        int d1 = Convert.ToInt32(jd[0]);
                        string[] df1 = Convert.ToString(Convert.ToDecimal("0." + jd[1]) * 60).Split('.');
                        int f1 = Convert.ToInt32(df1[0]);
                        decimal m1 = Convert.ToDecimal("0." + df1[1]) * 60;

                        string[] wd = templat.Latitude.ToString("####.####").Split('.');
                        int d2 = Convert.ToInt32(wd[0]);
                        string[] df2 = Convert.ToString(Convert.ToDecimal("0." + wd[1]) * 60).Split('.');
                        int f2 = Convert.ToInt32(df2[0]);
                        decimal m2 = Convert.ToDecimal("0." + df2[1]) * 60;

                        string strjwd= "经纬度: " + d1.ToString() + "°" + f1.ToString() + "′" + m1.ToString("##.#") + "″," + d2.ToString() + "°" + f2.ToString() + "′" + m2.ToString("##.#") + "″";
                        frmSubstationProperty frmSub = new frmSubstationProperty();
                        frmSub.jwstr = strjwd;
                        frmSub.InitData(xml1.GetAttribute("id"), tlVectorControl1.SVGDocument.SvgdataUid, SvgDocument.currentLayer, lab);
                        if (frmSub.ShowDialog() == DialogResult.OK)
                        {
                            xml1.SetAttribute("info-name", frmSub.Sub.EleName);
                            SVG_ENTITY ent = new SVG_ENTITY();
                            if (frmSub.IsCreate)
                            {
                                ent.SUID = Guid.NewGuid().ToString();
                                ent.EleID = frmSub.Sub.EleID;
                                ent.layerID = frmSub.Sub.LayerID;
                                ent.MDATE = System.DateTime.Now;
                                ent.NAME = frmSub.Sub.EleName;
                                ent.svgID = frmSub.Sub.SvgUID;
                                ent.TYPE = "substation";
                                ent.voltage = Convert.ToInt32(frmSub.Sub.ObligateField1);
                                Services.BaseService.Create<SVG_ENTITY>(ent);
                            }
                            else
                            {
                                ent.NAME = frmSub.Sub.EleName;
                                ent.layerID = frmSub.Sub.LayerID;
                                ent.MDATE = System.DateTime.Now;
                                //ent.voltage = fl.Line.Voltage;
                                Services.BaseService.Update<SVG_ENTITY>(ent);
                            }
                        }
                    }
                    if (xml1.GetAttribute("xlink:href").Contains("XL_GT_3") || xml1.GetAttribute("xlink:href").Contains("XL_GT_4"))
                    {
                        frmInputNum num = new frmInputNum();
                        num.InputStr = xml1.GetAttribute("order");
                        num.ShowDialog();
                        xml1.SetAttribute("order",num.InputStrSEL);
                    }
                    //if (xml1.GetAttribute("xlink:href").Contains("bdz") || xml1.GetAttribute("xlink:href").Contains("hwg") ||
                    //    xml1.GetAttribute("xlink:href").Contains("fjx") || xml1.GetAttribute("xlink:href").Contains("kbs") ||
                    //    xml1.GetAttribute("xlink:href").Contains("byq") || xml1.GetAttribute("xlink:href").Contains("kg"))
                    //{
                    //    frmInputDialog n1 = new frmInputDialog();
                    //    n1.InputStr = xml1.GetAttribute("info-name").ToString();
                    //    if (n1.ShowDialog() == DialogResult.OK)
                    //    {
                    //        xml1.SetAttribute("info-name",n1.InputStr);
                    //    }

                    //}
                    if (xml1.GetAttribute("xlink:href").Contains("kbs") || xml1.GetAttribute("xlink:href").Contains("hwg"))
                    {
                        frmkbsProperty num = new frmkbsProperty();
                        num.InitData(((SvgElement)xml1).ID,tlVectorControl1.SVGDocument.SvgdataUid,tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        num.ShowDialog();
                    }
                    if (xml1.GetAttribute("xlink:href").Contains("fjx"))
                    {
                        frmfjxProperty num = new frmfjxProperty();
                        num.InitData(((SvgElement)xml1).ID, tlVectorControl1.SVGDocument.SvgdataUid, tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        num.ShowDialog();
                    }
                    if (xml1.GetAttribute("xlink:href").Contains("byq"))
                    {
                        frmbyqProperty num = new frmbyqProperty();
                        num.InitData(((SvgElement)xml1).ID, tlVectorControl1.SVGDocument.SvgdataUid, tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        num.ShowDialog();
                    }
                    if (xml1.GetAttribute("xlink:href").Contains("SB_GT"))
                    {
                        string lineWidth = "2";

                        string Code = xltProcessor.GetCurrentLineCode();
                        string _len = xltProcessor.GetWholeLineLength(Code).ToString("#####.####");

                        frmLineProperty fl = new frmLineProperty();
                        fl.LineNode = tlVectorControl1.SVGDocument.CurrentElement;
                        fl.InitData(Code, tlVectorControl1.SVGDocument.SvgdataUid, _len, SvgDocument.currentLayer);
                        if (fl.ShowDialog() == DialogResult.OK)
                        {
                            //Value="stroke-dasharray:8 8;stroke-width:2;stroke:#00C000;"
                            lineWidth = fl.LineWidth;
                            string styleValue = "";
                            if (fl.Line.ObligateField1 == "规划")
                            {
                                styleValue = "stroke-dasharray:4 4;stroke-width:" + lineWidth + ";";
                            }
                            else
                            {
                                styleValue = "stroke-width:" + lineWidth + ";";
                            }

                            styleValue = styleValue + "stroke:" + ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(fl.Line.ObligateField2)));
                            ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).RemoveAttribute("style");
                            ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).SetAttribute("style", styleValue);

                            xltProcessor.SetWholeLineAttribute(Code, "style", styleValue);
                        }
                    }

                }
                if (e.ClickedItem.Text == "移动")
                {
                    if (tlVectorControl1.SVGDocument.CurrentElement == null || tlVectorControl1.SVGDocument.CurrentElement.ID == "svg")
                    {
                        return;
                    }
                    XmlElement xmln = (XmlElement)tlVectorControl1.SVGDocument.CurrentElement;
                    frmMove fm = new frmMove();
                    PointF pf11 = ((Use)xmln).CenterPoint;
                    LongLat temp = mapview.ParseToLongLat((int)pf11.X, (int)pf11.Y);
                    string[] jd = temp.Longitude.ToString("####.####").Split('.');
                    int d1 = Convert.ToInt32(jd[0]);
                    string[] df1 = Convert.ToString(Convert.ToDecimal("0." + jd[1]) * 60).Split('.');
                    int f1 = Convert.ToInt32(df1[0]);
                    decimal m1 = Convert.ToDecimal("0." + df1[1]) * 60;

                    string[] wd = temp.Latitude.ToString("####.####").Split('.');
                    int d2 = Convert.ToInt32(wd[0]);
                    string[] df2 = Convert.ToString(Convert.ToDecimal("0." + wd[1]) * 60).Split('.');
                    int f2 = Convert.ToInt32(df2[0]);
                    decimal m2 = Convert.ToDecimal("0." + df2[1]) * 60;
                    strj1 = d1.ToString();
                    strw1 = f1.ToString();
                    strd1 = m1.ToString();
                    strj2 = d2.ToString();
                    strw2 = f2.ToString();
                    strd2 = m2.ToString();
                    fm.Init(strj1,strw1,strd1,strj2,strw2,strd2);
                    if (fm.ShowDialog() == DialogResult.OK)
                    {
                        string strValue = fm.StrValue;
                        string[] str = strValue.Split(',');
                        string[] JWD1 = str[0].Split(' ');
                        double J1 = Convert.ToDouble(JWD1[0]);
                        double W1 = Convert.ToDouble(JWD1[1]);
                        double D1 = Convert.ToDouble(JWD1[2]);
                        string[] JWD2 = str[1].Split(' ');
                        double J2 = Convert.ToDouble(JWD2[0]);
                        double W2 = Convert.ToDouble(JWD2[1]);
                        double D2 = Convert.ToDouble(JWD2[2]);

                        double JD = J1 + W1 / 60 + D1 / 3600;
                        double WD = J2 + W2 / 60 + D2 / 3600;

                  
                        PointF pf1 = mapview.ParseToPoint(JD, WD);

                        PointF p1 = ((Use)xmln).CenterPoint;
                       
                        SvgElement e1 = xmln as SvgElement;
                        Matrix matrix2 = ((IGraph)e1).GraphTransform.Matrix.Clone();
                        Matrix matrix3 = tlVectorControl1.DrawArea.CoordTransform.Clone();
                        matrix3.Invert();
                        matrix2.Multiply(matrix3, MatrixOrder.Append);
                        matrix2.Invert();
                        PointF[] pfArray1 = new PointF[] { new PointF(pf1.X / tlVectorControl1.ScaleRatio, pf1.Y / tlVectorControl1.ScaleRatio), p1 };
                        matrix2.TransformPoints(pfArray1);

                        float single1 = pfArray1[0].X - pfArray1[1].X;
                        float single2 = pfArray1[0].Y - pfArray1[1].Y;
                        
                        Matrix matrix6 = ((IGraph)e1).Transform.Matrix.Clone();
                        if (e1.SvgAttributes.ContainsKey("transform")) {
                            Matrix matrix7 = ((Matrix)e1.SvgAttributes["transform"]).Clone();
                            matrix7.Invert();
                            matrix6.Multiply(matrix7, MatrixOrder.Append);
                        }
                        Matrix matrix5 = new Matrix();
                        matrix5.Translate(single1, single2);
                        Matrix matrix8 = ((IGraph)e1).Transform.Matrix.Clone();
                        matrix8.Multiply(matrix5);
                        matrix6.Invert();
                        matrix6.Multiply(matrix8, MatrixOrder.Append);
                        Transf tf = new Transf();
                        tf.setMatrix(matrix6);
                        (xmln as Use).Transform = tf;
                        
                    }
                }
                if (e.ClickedItem.Text == "接线图")
                {
                    if (tlVectorControl1.SVGDocument.CurrentElement == null || tlVectorControl1.SVGDocument.CurrentElement.ID == "svg")
                    {
                        return;
                    }
                    ParentUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    Save();
                    ParentUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    SVGFILE svg_temp = new SVGFILE();
                    //XmlElement xml1 = ((XmlElement)(e.Elements[0]));xml1.GetAttribute("id");
                    svg_temp.SUID = ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).GetAttribute("id");
                    svg_temp.FILENAME = getBdzName(svg_temp.SUID);// ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).GetAttribute("info-name");
                    string strWhere = string.Format("suid='{0}' or filename='{1}' ", svg_temp.SUID, svg_temp.FILENAME);
                    IList svglist = Services.BaseService.GetList("SelectSVGFILEByWhere", strWhere);
                    OpenJXT(svglist, svg_temp);
                    //frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
                    //frmlar.Progtype = MapType;
                    //frmlar.InitData();
                    //JxtBar();              
                }
                if (e.ClickedItem.Text == "打开")
                {
                    if (tlVectorControl1.SVGDocument.CurrentElement.ID == "svg")
                    {
                        MessageBox.Show("请选择地块。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    UseRelating UseRel = new UseRelating();
                    UseRel.UseID = tlVectorControl1.SVGDocument.CurrentElement.ID;
                    IList<UseRelating> UseRelList = Services.BaseService.GetList<UseRelating>("SelectUseRelatingByUseID", UseRel);
                    if (UseRelList.Count < 1)
                    {
                        MessageBox.Show("选择的地块还没有关联到其他地图，请先设置关联地图", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    UseRel = UseRelList[0];
                    SVGFILE svgFile = new SVGFILE();
                    svgFile.SUID = UseRel.LinkUID;
                    IList svgList = Services.BaseService.GetList("SelectSVGFILEByKey", svgFile);
                    if (svgList.Count < 1)
                    {
                        MessageBox.Show("被关联的地图已经被删除，请重新设置关联地图", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    svgFile = (SVGFILE)svgList[0];
                    //SvgDocument doc = new SvgDocument();

                    if (!string.IsNullOrEmpty(svgFile.SVGDATA))
                    {
                        //doc.LoadXml(svgFile.SVGDATA);
                        ctlfile_OnOpenSvgDocument(sender, svgFile.SUID);
                    }

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
                    //gr1.AddRectangle(TLMath.getRectangle(poly1));
                    gr1.AddPolygon(TLMath.getPolygonPoints(poly1));
                    //gr1.CloseFigure();                
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
                        //path1.Transform(graph1.GraphTransform.Matrix);
                        //path1.Transform(graph1.Transform.Matrix);                  
                        // RectangleF ef2 = path1.GetBounds();// PathFunc.GetBounds(path1); 

                        //for (int n = 0; n < selCol.Count - 1; n++)
                        //{
                        //    //_doc.AppendChild((XmlNode)selCol[n]);
                        //    svgtxt = svgtxt + ((XmlElement)selCol[n]).OuterXml + "\r\n";
                        //}
                        if (!graph1.Visible || !graph1.DrawVisible || !graph1.Layer.Visible) continue;

                        GraphicsPath path2 = (GraphicsPath)graph1.GPath.Clone();
                        path2.Transform(graph1.Transform.Matrix);
                        RectangleF ef2 = PathFunc.GetBounds(path2);

                        if (ef1.Contains(ef2) || RectangleF.Intersect(ef1, ef2) != RectangleF.Empty)
                        {

                            SvgElement ele = (SvgElement)graph1;
                            svgtxt.AppendLine(ele.OuterXml);
                            //tlVectorControl1.SVGDocument.AddSelectElement(graph1);
                            if (graph1 is Use)
                            {
                                //PointF offset = TLMath.getUseOffset(((XmlElement)graph1).GetAttribute("xlink:href"));
                                //if (ef1.Contains(new PointF(((Use)graph1).X + offset.X, ((Use)graph1).Y + offset.Y))) {
                                //SvgElement ele = (SvgElement)graph1;
                                //svgtxt.AppendLine(ele.OuterXml);

                                string symid = ((XmlElement)graph1).GetAttribute("xlink:href");
                                if (!symlist.Contains(symid))
                                {
                                    symlist.Add(symid);
                                }
                                //}
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
                        //if (ef1.Contains(ef2)|| RectangleF.Intersect(ef1,ef2) !=RectangleF.Empty )
                        //{
                        //    SvgElement ele = (SvgElement)graph1;
                        //    svgtxt.AppendLine(ele.OuterXml);
                        //    //tlVectorControl1.SVGDocument.AddSelectElement(graph1);
                        //    if (graph1 is Use ) {
                        //        PointF offset = TLMath.getUseOffset(((XmlElement)graph1).GetAttribute("xlink:href"));
                        //        if (ef1.Contains(new PointF(((Use)graph1).X + offset.X, ((Use)graph1).Y + offset.Y))) {
                        //            //SvgElement ele = (SvgElement)graph1;
                        //            //svgtxt.AppendLine(ele.OuterXml);

                        //            string symid = ((XmlElement)graph1).GetAttribute("xlink:href");
                        //            if (!symlist.Contains(symid)) {
                        //                symlist.Add(symid);
                        //            }
                        //        }
                        //    } 
                        //    if (graph1.GetType().FullName == "ItopVector.Core.Figure.Polyline") {
                        //        string IsLead = ((XmlElement)graph1).GetAttribute("IsLead");
                        //        if (IsLead != "") {
                        //            if (ef1.Contains(ef2)) {
                        //                idlist.Add(graph1.ID);
                        //            }
                        //        }
                        //    }
                        //}


                    }
                    symlist = ResetList(symlist);
                    svgtxt.AppendLine("</svg>");
                    _doc.LoadXml(svgtxt.ToString());
                    _doc.SvgdataUid = tlVectorControl1.SVGDocument.SvgdataUid;
                    frmPrintF pri = new frmPrintF();
                    pri.Init(tlVectorControl1.SVGDocument.CurrentElement.ID, tlVectorControl1.SVGDocument.SvgdataUid);
                    if (pri.ShowDialog() == DialogResult.OK)
                    {
                        frmSubPrint s = new frmSubPrint();
                        s.Vector = tlVectorControl1;
                        s.InitImg(pri.strzt, pri.strgs, pri.pri, idlist, symlist);
                        s.Open(_doc, ef1);
                        s.Show();
                    }
                }
                if (e.ClickedItem.Text == "分类统计报表")
                {
                    if (tlVectorControl1.SVGDocument.CurrentElement == null || tlVectorControl1.SVGDocument.CurrentElement.ID == "svg")
                    {
                        return;
                    }
                    IGraph poly1 = (IGraph)tlVectorControl1.SVGDocument.CurrentElement;
                    frmPloyPrint p = new frmPloyPrint();

                    p.InitDate(poly1.ID, tlVectorControl1.SVGDocument.SvgdataUid);
                    p.ShowDialog();
                }
                if (e.ClickedItem.Text == "保存图片")
                {
                    if (tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.RectangleElement")
                    {
                        //frmImgInfoInput finput = new frmImgInfoInput();
                        //if (finput.ShowDialog() == DialogResult.OK)
                        //{
                        PrintHelper ph = new PrintHelper(tlVectorControl1, mapview);
                        frmImgManager frm = new frmImgManager();
                        //frm.StrName = finput.StrName;
                        //frm.StrRemark = finput.StrRemark;
                        frm.Pic = ph.getImage();
                        frm.ShowDialog();
                        //}
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            if (e.ClickedItem.Text == "三维变电站")
            {
                try
                {
                    string strid = tlVectorControl1.SVGDocument.CurrentElement.ID;
                    substation s = new substation();
                    s.EleID = strid;
                    s.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    s = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", s);
                    ProcessStartInfo p = new ProcessStartInfo();
                    p.FileName = Application.StartupPath + "\\" + s.EleName + "\\bdz.exe";
                    p.WorkingDirectory = Application.StartupPath + "\\" + s.EleName;
                    Process.Start(p);
                }
                catch (Exception e1) { }
            }
            if(e.ClickedItem.Text =="更新关联变电站"){
                UpdateLine();
            }
            if (e.ClickedItem.Text == "删除")
            {
                Delete();
            }
        }

        
        public void UpdateLine()
        {
            XmlNodeList elementCollection = tlVectorControl1.SVGDocument.GetElementsByTagName("use");
            if (elementCollection.Count > 0)
            {
                foreach (ISvgElement element in elementCollection)
                {
                    if ((element as XmlElement) is Use)
                    {
                        RectangleF t = ((IGraph)element).GetBounds();

                        PointF uset = new PointF((float)(t.X + t.Width / 2), (float)(t.Y + t.Height / 2));
                        //XmlNodeList linea = tlVectorControl1.SVGDocument.GetElementsByTagName("polyline");
                        //foreach (XmlNode pol in linea)
                        //{
                        Polyline pol = tlVectorControl1.SVGDocument.CurrentElement as Polyline;
                            PointF[] tt = pol.Pt;
                            double x1 = tt[0].X;
                            double x2 = tt[tt.Length-1].X;
                            double y1 = tt[0].Y;
                            double y2 = tt[tt.Length-1].Y;

                            if ((element as XmlElement).GetAttribute("xlink:href").Contains("Substation"))
                            {
                                //(element as XmlElement).SetAttribute("stroke", "#FF0000");
                                if (Math.Abs(uset.X - x1) < ((t.Height) / 2) && Math.Abs(uset.Y - y1) < ((t.Height) / 2))
                                {

                                    (pol as XmlElement).SetAttribute("FirstNode", element.ID);
                                    LineInfo info = new LineInfo();
                                    info.EleID = pol.ID;
                                    info.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                    info=(LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", info);
                                    if(info!=null){
                                        substation _subinfo = new substation();
                                        _subinfo.EleID = element.ID;
                                        _subinfo.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        _subinfo = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _subinfo);
                                        if(_subinfo!=null){
                                            info.ObligateField6 = _subinfo.EleName;
                                            Services.BaseService.Update<substation>(info);
                                        }
                                    }
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
                                    LineInfo info = new LineInfo();
                                    info.EleID = pol.ID;
                                    info.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                    info = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", info);
                                    if (info != null)
                                    {
                                        substation _subinfo = new substation();
                                        _subinfo.EleID = element.ID;
                                        _subinfo.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        _subinfo = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _subinfo);
                                        if (_subinfo != null)
                                        {
                                            info.ObligateField7 = _subinfo.EleName;
                                            Services.BaseService.Update<substation>(info);
                                        }
                                    }

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
                MessageBox.Show("更新完成。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
        }
        private string getBdzName(string id)
        {
            substation sub = new substation();
            sub.EleID = id;
            sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList svglist20 = Services.BaseService.GetList("SelectsubstationByEleID", sub);
            sub = null;
            string ret = string.Empty;
            if (svglist20.Count > 0)
            {
                sub = (substation)svglist20[0];
                ret = sub.EleName;
            }
            return ret;
        }
        public ArrayList ResetList(ArrayList list)
        {
            SortedList slist = new SortedList();
            ArrayList list2 = new ArrayList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ToString() == "#Substation500-1")
                {
                    slist.Add(1, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#Substation220-1")
                {
                    slist.Add(2, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#user-Substation220-2")
                {
                    slist.Add(3, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#Substation110-1")
                {
                    slist.Add(4, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#user-Substation110-2")
                {
                    slist.Add(5, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#Substation66-1")
                {
                    slist.Add(6, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#user-Substation66-2")
                {
                    slist.Add(7, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#Substation35-1")
                {
                    slist.Add(8, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#user-Substation35-2")
                {
                    slist.Add(9, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-Substation500-1")
                {
                    slist.Add(11, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-Substation220-1")
                {
                    slist.Add(12, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-user-Substation220-2")
                {
                    slist.Add(13, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-Substation110-1")
                {
                    slist.Add(14, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-user-Substation110-2")
                {
                    slist.Add(15, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-Substation66-1")
                {
                    slist.Add(16, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-user-Substation66-2")
                {
                    slist.Add(17, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-Substation35-1")
                {
                    slist.Add(18, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-user-Substation35-2")
                {
                    slist.Add(19, list[i]);
                    continue;
                }
                else
                {
                    list2.Add(list[i]);
                }
            }
            ArrayList rlist = new ArrayList();
            IEnumerator n = slist.Values.GetEnumerator();
            while (n.MoveNext())
            {
                rlist.Add(n.Current);
            }
            for (int j = 0; j < list2.Count; j++)
            {
                rlist.Add(list2[j]);
            }
            return rlist;
        }
        private void dotNetBarManager1_ItemClick(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem btItem = sender as DevComponents.DotNetBar.ButtonItem;
            //Layer layer1 = (Layer)LayerBox.ComboBoxEx.SelectedItem;
            if (btItem != null)
            {
                if (btItem.Name == "mRoam")
                {
                    frmlar.Hide();
                }
                else
                {
                    frmlar.Show();
                }
                switch (btItem.Name)
                {
                    #region 文件操作
                    case "mNew":
                        tlVectorControl1.NewFile();

                        break;
                    case "mOpen":

                        break;
                    case "mImport":
                        ExportImage();
                        //ImportDxf();
                        break;
                    case "ImportDxf":
                        ImportDxf();
                        break;
                    case "btExSymbol":
                        tlVectorControl1.ExportSymbol();
                        break;
                    case "mSave":
                        SaveButton();
                        break;
                    case "mSaveSVG":
                        tlVectorControl1.SaveAs();
                        break;
                    case "mExit":

                        //XmlElement s= (XmlElement)tlVectorControl1.SVGDocument.SelectCollection[0];
                        //Polygon p = (Polygon)s;
                        //tlVectorControl1.SymbolSelector = this.symbolSelector;
                        //tlVectorControl1.ExportSymbol();
                        //tlVectorControl1.CurrentOperation = ToolOperation.WindowZoom;
                        //frmLayerList ff = new frmLayerList();
                        //ff.InitData(tlVectorControl1.SVGDocument.getLayerList());
                        //ff.Show();
                        //string aa = tlVectorControl1.SVGDocument.SvgdataUid;
                        ////tlVectorControl1.NewFile();
                        //tlVectorControl1.OpenFile("d:\\2.svg");
                        //tlVectorControl1.SVGDocument.SvgdataUid = aa;

                        //IList<LineInfo> list = Services.BaseService.GetList<LineInfo>("SelectLineInfoBySvgIDAll", line);

                        this.Close();
                        //frmAddLine ff = new frmAddLine();
                        //ff.Show();
                        //tlVectorControl1.ExportSymbol();
                        //XmlNodeList n1111 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1']");

                        //for (int i = 0; i < n1111.Count; i++)
                        //{
                        //    XmlNode n1 = n1111[i];
                        //    string na = ((XmlElement)n1).GetAttribute("info-name");
                        //    string la = ((XmlElement)n1).GetAttribute("layer");
                        //    string id = ((XmlElement)n1).GetAttribute("id");
                        //    LineInfo ll = new LineInfo();
                        //    ll.LineName = " LineName='" + na + "' and LayerID='" + la + "' and SvgUID='" + tlVectorControl1.SVGDocument.SvgdataUid + "'";
                        //    ll = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByWhere", ll);
                        //    if (ll != null)
                        //    {

                        //        ll.EleID = id;
                        //        if (ll.Length == "")
                        //        {
                        //            ll.Length = "0";
                        //        }
                        //        Services.BaseService.Update<LineInfo>(ll);
                        //    }
                        //}
                        //XmlNodeList list1 = tlVectorControl1.SVGDocument.SelectNodes("//*[@href=\"#实心杆塔0\"]");
                        //XmlNodeList list2 = tlVectorControl1.SVGDocument.SelectNodes("//*[@href=\"#实心杆塔1\"]");

                        //for (int i = 0; i < list1.Count; i++)
                        //{
                        //    tlVectorControl1.SVGDocument.RootElement.RemoveChild(list1[i]);
                        //}
                        //for (int j = 0; j < list2.Count; j++)
                        //{
                        //    tlVectorControl1.SVGDocument.RootElement.RemoveChild(list2[j]);
                        //}
                        //InitTK();
                        //XmlElement a = tlVectorControl1.SVGDocument.RootElement;
                        //a.SetAttribute("height","3000");
                        //tlVectorControl1.Refresh();
                        //XmlElement b = tlVectorControl1.SVGDocument.GetElementById("circle49171");
                        //string a = tlVectorControl1.SVGDocument.OuterXml;
                        //Hashtable hs = new Hashtable();
                        //XmlNodeList list = tlVectorControl1.SVGDocument.GetElementsByTagName("symbol");
                        //for (int i = 0; i < list.Count; i++)
                        //{
                        //    XmlNode node = list[i];
                        //    hs.Add(i, node);
                        //    //((XmlElement)node).SetAttribute("id", "xxx" + i.ToString());
                        //}
                        break;
                    case "bt1":
                        InitTK();
                        break;
                    case "callCAD":
                        try
                        {
                            //Autodesk.AutoCAD.Interop.
                            if (MessageBox.Show("此操作需要花费较长时间，确认导出么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                CAD cad = new CAD();
                                cad.tlVectorControl1 = tlVectorControl1;
                                cad.WriteDwg("ALL");
                            }

                        }
                        catch
                        {
                            MessageBox.Show("请安装AutoCAD2006或以上版本。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "callCADSub":
                        try
                        {
                            //Autodesk.AutoCAD.Interop.AcadApplicationClass a = new Autodesk.AutoCAD.Interop.AcadApplicationClass();
                            if (MessageBox.Show("此操作需要花费较长时间，确认导出么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                CAD cad = new CAD();
                                cad.tlVectorControl1 = tlVectorControl1;
                                string strlar = frmlar.getSelectedLayer();
                                cad.WriteDwg(strlar);
                            }

                        }

                        catch
                        {
                            MessageBox.Show("请安装AutoCAD2006或以上版本。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "mPriSet":
                        tlVectorControl1.PaperSetup();
                        break;
                    case "mPrint":
                        tlVectorControl1.Print();
                        break;
                    case "mView":
                        //frmSvgView fView = new frmSvgView();
                        //fView.Open(tlVectorControl1.SVGDocument.SvgdataUid);
                        //fView.Show();

                        break;

                    case "mViewScale":
                        if (img != null)
                        {
                            frmtempViewScale fscale1 = new frmtempViewScale();
                            fscale1.ShowDialog();
                        }
                        else
                        {
                            frmViewScale fScale = new frmViewScale();
                            string viewScale = tlVectorControl1.SVGDocument.getViewScale();
                            if (viewScale != "")
                            {
                                fScale.InitData(viewScale);
                            }
                            if (fScale.ShowDialog() == DialogResult.OK)
                            {

                                //viewScale = fScale.ViewScale;
                                string _viewScale = fScale.ViewScale;
                                tlVectorControl1.SVGDocument.setViewScale(_viewScale);
                                if (viewScale == "")
                                {
                                    viewScale = "1";
                                }
                                Recalculate(Convert.ToDecimal(_viewScale) / Convert.ToDecimal(viewScale));
                            }
                        }
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
                    case "mEdit":
                        if (MapType == "所内接线图")
                        {
                            Save();
                            dotNetBarManager1.Bars["mainmenu"].GetItem("ImportDxf").Visible = false;
                            svg.SUID = ParentUID;
                            IList svglist = Services.BaseService.GetList("SelectSVGFILEByKey", svg);
                            svg = (SVGFILE)svglist[0];
                            sdoc = null;
                            sdoc = new SvgDocument();
                            sdoc.LoadXml(svg.SVGDATA);
                            tlVectorControl1.SVGDocument = sdoc;
                            tlVectorControl1.SVGDocument.SvgdataUid = svg.SUID;
                            MapType = "接线图";
                            CtrlSvgView.MapType = "接线图";
                            LoadShape("symbol_3.xml");
                            Init(progtype);
                            //ButtonEnb(true);
                            frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
                            frmlar.Progtype = progtype;
                            frmlar.InitData();
                            dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem2").Enabled = true;
                            dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").Enabled = true;
                            bk1.Enabled = true;
                            LoadImage = true;
                            tlVectorControl1.Refresh();
                        }
                        tlVectorControl1.ContextMenuStrip = contextMenuStrip1;
                        MapType = "接线图";
                        break;

                    case "mAbout":

                        frmAbout frma = new frmAbout();
                        frma.ShowDialog();
                        break;

                    //基础操作
                    case "mFreeTransform":
                        tlVectorControl1.Operation = ToolOperation.FreeTransform;

                        break;
                    case "mRoam1":
                        tlVectorControl1.Operation = ToolOperation.Roam;

                        break;
                    case "mShapeTransform1":
                        tlVectorControl1.Operation = ToolOperation.Custom11;

                        break;
                    case "mShapeTransform2":
                        tlVectorControl1.Operation = ToolOperation.Custom12;
                        break;
                    case "mShapeTransform3":
                        tlVectorControl1.Operation = ToolOperation.Custom13;
                        break;
                    case "mShapeTransform4":
                        tlVectorControl1.Operation = ToolOperation.Custom15;
                        break;
                    case "mShapeTransform5":
                        tlVectorControl1.Operation = ToolOperation.Custom14;
                        break;
                    case "m_ljxl":
                        ConnLine();
                        break;
                    case "mAngleRectangle1":
                        tlVectorControl1.Operation = ToolOperation.AngleRectangle;

                        break;
                    case "mSelect1":
                    case "mSel1":
                        //tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.FreeTransform;
                        break;
                    case "mLine1":
                        tlVectorControl1.Operation = ToolOperation.Line;

                        break;
                    case "mPolyline1":
                        tlVectorControl1.Operation = ToolOperation.PolyLine;

                        break;
                    case "mImage1":
                        tlVectorControl1.Operation = ToolOperation.Image;

                        break;
                    case "mText1":
                        tlVectorControl1.Operation = ToolOperation.Text;

                        break;
                    case "mEllipse1":
                        tlVectorControl1.Operation = ToolOperation.Ellipse;

                        break;
                    case "mBezier1":
                        tlVectorControl1.Operation = ToolOperation.Bezier;

                        break;

                    //图元操作
                    case "mCopy1":
                        tlVectorControl1.Copy();
                        break;
                    case "mCut1":
                        tlVectorControl1.Cut();
                        break;
                    case "mPaste1":
                        PasteWithProperty();
                        break;
                    case "mDelete1":
                        if (tlVectorControl1.SVGDocument.CurrentElement != null && tlVectorControl1.SVGDocument.CurrentElement.ID != "svg")
                        {
                            frmMessageBox msg = new frmMessageBox();
                            if (msg.ShowDialog() == DialogResult.OK)
                            {
                                
                                if (msg.ck)
                                {
                                    // if(MessageBox.Show("确认删除么？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes){
                                    for (int i = 0; i < tlVectorControl1.SVGDocument.SelectCollection.Count; i++)
                                    {
                                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Polygon")
                                        {
                                            glebeProperty gle = new glebeProperty();
                                            gle.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                            gle.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                                            Services.BaseService.Update("DeleteglebePropertyByEleID", gle);
                                        }
                                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Polyline" || tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Line")
                                        {
                                            LineInfo _line = new LineInfo();
                                            _line.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                            _line.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                                            LineInfo temp = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", _line);
                                            if (temp != null)
                                            {
                                                Services.BaseService.Update("DeleteLinePropertyByEleID", _line);

                                                Services.BaseService.Update("DeleteLine_InfoByCode", temp.UID);
                                            }
                                        }
                                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Use")
                                        {
                                            string str_name = ((XmlElement)(tlVectorControl1.SVGDocument.SelectCollection[i])).GetAttribute("xlink:href");
                                            if (str_name.Contains("Substation"))
                                            {
                                                substation _sub = new substation();
                                                _sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                                _sub.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;

                                                substation temp = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _sub);

                                                if (temp != null)
                                                {
                                                    Services.BaseService.Update("DeletesubstationByEleID", _sub);

                                                    Services.BaseService.Update("DeleteSubstation_InfoByCode", temp.UID);
                                                }
                                            }
                                        }
                                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.ConnectLine")
                                        {
                                            ConnectLine cline = (ConnectLine)tlVectorControl1.SVGDocument.SelectCollection[i];
                                            if (cline.StartGraph != null)
                                            {
                                                SvgElement ele = (SvgElement)cline.StartGraph;
                                                if (!ele.GetAttribute("xlink:href").Contains("Substation"))
                                                {
                                                    tlVectorControl1.SVGDocument.SelectCollection.Add(cline.StartGraph);
                                                }
                                            }
                                            if (cline.EndGraph != null)
                                            {
                                                tlVectorControl1.SVGDocument.SelectCollection.Add(cline.EndGraph);
                                            }
                                        }


                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < tlVectorControl1.SVGDocument.SelectCollection.Count; i++)
                                    {
                                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Polyline" || tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Line")
                                        {
                                            LineInfo _line = new LineInfo();
                                            _line.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                            _line.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;

                                            LineInfo linetemp = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", _line);
                                            if (linetemp != null)
                                            {
                                                PowerProTypes temp = (PowerProTypes)Services.BaseService.GetObject("SelectPowerProTypesByCode", linetemp.UID);
                                                if (temp != null)
                                                {
                                                    linetemp.EleID = "";
                                                    Services.BaseService.Update<LineInfo>(linetemp);
                                                }
                                                else
                                                {
                                                    Services.BaseService.Update("DeleteLineInfo", linetemp);
                                                }
                                            }

                                        }
                                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Use")
                                        {
                                            string str_name = ((XmlElement)(tlVectorControl1.SVGDocument.SelectCollection[i])).GetAttribute("xlink:href");
                                            if (str_name.Contains("Substation"))
                                            {
                                                substation _sub = new substation();
                                                _sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                                _sub.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;

                                                substation subtemp = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _sub);
                                                if (subtemp != null)
                                                {
                                                    PowerProTypes temp = (PowerProTypes)Services.BaseService.GetObject("SelectPowerProTypesByCode", subtemp.UID);
                                                    if (temp != null)
                                                    {
                                                        subtemp.EleID = "";
                                                        Services.BaseService.Update<substation>(subtemp);
                                                    }
                                                    else
                                                    {
                                                        Services.BaseService.Update("Deletesubstation", subtemp);
                                                    }
                                                }
                                            }
                                        }
                                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.ConnectLine")
                                        {
                                            ConnectLine cline = (ConnectLine)tlVectorControl1.SVGDocument.SelectCollection[i];
                                            if (cline.StartGraph != null)
                                            {
                                                SvgElement ele = (SvgElement)cline.StartGraph;
                                                if (!ele.GetAttribute("xlink:href").Contains("Substation"))
                                                {
                                                    tlVectorControl1.SVGDocument.SelectCollection.Add(cline.StartGraph);
                                                }
                                            }
                                            if (cline.EndGraph != null)
                                            {
                                                tlVectorControl1.SVGDocument.SelectCollection.Add(cline.EndGraph);
                                            }
                                        }
                                    }

                                }

                                tlVectorControl1.Delete();
                            }
                        }
                        //tlVectorControl1.Operation = ToolOperation.Select;
                        break;
                    case "mUodo1":
                        tlVectorControl1.Undo();
                        break;
                    case "mRedo1":
                        tlVectorControl1.Redo();
                        break;
                    case "mAlign1":
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
                    case "mAlignLeft1":
                        tlVectorControl1.Align(AlignType.Left);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mAlignRight1":
                        tlVectorControl1.Align(AlignType.Right);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mAlignTop1":
                        tlVectorControl1.Align(AlignType.Top);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mAlignBottom1":
                        tlVectorControl1.Align(AlignType.Bottom);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mAlignHorizontalCenter1":
                        tlVectorControl1.Align(AlignType.HorizontalCenter);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mAlignVerticalCenter1":
                        tlVectorControl1.Align(AlignType.VerticalCenter);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl1.Refresh();
                        break;
                    case "mOrder1":
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
                    case "mGoTop1":
                        tlVectorControl1.ChangeLevel(LevelType.Top);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mGoUp1":
                        tlVectorControl1.ChangeLevel(LevelType.Up);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mGoDown1":
                        tlVectorControl1.ChangeLevel(LevelType.Down);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mGoBottom1":
                        tlVectorControl1.ChangeLevel(LevelType.Bottom);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mRotate1":
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
                    case "mToH1":

                        tlVectorControl1.FlipX();
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToV1":
                        tlVectorControl1.FlipY();
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToLeft1":
                        tlVectorControl1.RotateSelection(-90f);
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToRight1":
                        tlVectorControl1.RotateSelection(90f);
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;

                    //图形操作

                    case "mLeadLine1":
                        //tlVectorControl1.DrawArea.BackgroundImage = System.Drawing.Image.FromFile("f:\\back11.jpg");
                        //tlVectorControl1.DrawArea.BackgroundImageLayout = ImageLayout.Center;

                        if (!getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl1.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("电网规划层"))
                        {
                            MessageBox.Show("请选择电网规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        // sgt1.Visible = true;
                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.LeadLine;
                        break;
                    case "mAreaPoly1":
                        if (!getlayer(SvgDocument.currentLayer, "城市规划层", tlVectorControl1.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("城市规划层"))
                        {
                            MessageBox.Show("请选择城市规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.AreaPolygon;

                        break;

                    case "mFzzj1":　//放置注记
                        if (!getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl1.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("供电区域层"))
                        {
                            MessageBox.Show("请选择电网规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        //LayerBox.ComboBoxEx.SelectedIndex = 2;
                        tlVectorControl1.Operation = ToolOperation.Select;
                        MapType = "规划统计";
                        break;

                    case "mPriQu1":
                        SubPrint = true;
                        tlVectorControl1.Operation = ToolOperation.InterEnclosurePrint;
                        break;

                    case "mReCompute1":
                        if (MessageBox.Show("确认要重新计算全图的电量和负荷么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string scale = tlVectorControl1.SVGDocument.getViewScale();
                            if (scale != "")
                            {
                                Recalculate(Convert.ToDecimal(scale));
                            }
                            else
                            {
                                Recalculate(1);
                            }

                            MessageBox.Show("重新计算完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;


                    case "mFhbz1":
                        if (MessageBox.Show("是否生成负荷标注?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Fhbz();
                        }
                        break;
                    case "mCJ":
                        tlVectorControl1.Operation = ToolOperation.PolyLine;
                        csOperation = CustomOperation.OP_MeasureDistance;
                        break;
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
                    case "mSel":
                        //tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.FreeTransform;
                        sel_sym = "";
                        sel_start_point = "";
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
                        tlVectorControl1.Operation = ToolOperation.Line;

                        break;
                    case "mPolyline":
                        tlVectorControl1.Operation = ToolOperation.PolyLine;

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
                    #endregion

                    #region 视图
                    case "mOption":
                        tlVectorControl1.SetOption();
                        break;
                    case "mLayer":
                        LayerManagerShow();
                        //tlVectorControl1.LayerManager();
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
                    case "mMapOpacity"://地图透明度
                        frmMapSetup dlg = new frmMapSetup();
                        dlg.MapOpacity = this.MapOpacity;
                        if (dlg.ShowDialog() == DialogResult.OK) {
                            this.MapOpacity = dlg.MapOpacity;
                        }                        
                        break;
                    #endregion
                    #region 查看
                    case "mDklb":
                        //SaveAllLayer();
                        frmLayerList lay = new frmLayerList();
                        lay.InitData(tlVectorControl1.SVGDocument.getLayerList(), "1");
                        if (lay.ShowDialog() == DialogResult.OK)
                        {
                            frmglebePropertyList flist1 = new frmglebePropertyList();
                            flist1.InitDataSub(tlVectorControl1.SVGDocument.SvgdataUid, lay.str_sid);
                            flist1.Show();
                        }
                        break;
                    case "m_dktj":
                        frmLayerList layn = new frmLayerList();
                        layn.InitData(tlVectorControl1.SVGDocument.getLayerList(), "1");
                        if (layn.ShowDialog() == DialogResult.OK)
                        {
                            frmglebePropertyZHList flist1 = new frmglebePropertyZHList();
                            flist1.InitDataSub(tlVectorControl1.SVGDocument.SvgdataUid, layn.str_sid);
                            flist1.Show();
                        }
                        break;
                    case "mGhlb":
                        frmLayerList lay2 = new frmLayerList();
                        lay2.InitData(tlVectorControl1.SVGDocument.getLayerList(), "2");
                        if (lay2.ShowDialog() == DialogResult.OK)
                        {
                            frmglebePropertyList flist2 = new frmglebePropertyList();
                            flist2.InitData(tlVectorControl1.SVGDocument.SvgdataUid, lay2.str_sid);
                            flist2.Show();
                        }
                        break;
                    case "mLineList":
                        frmLayerList lay3 = new frmLayerList();
                        lay3.InitData(tlVectorControl1.SVGDocument.getLayerList(), "2");
                        if (lay3.ShowDialog() == DialogResult.OK)
                        {
                            frmLinePropertyList flist3 = new frmLinePropertyList();
                            flist3.InitData(tlVectorControl1.SVGDocument.SvgdataUid, lay3.str_sid);
                            flist3.Show();
                        }
                        break;
                    case "mDlph":
                        frmLayerList lay4 = new frmLayerList();
                        lay4.InitData(tlVectorControl1.SVGDocument.getLayerList(), "3");
                        if (lay4.ShowDialog() == DialogResult.OK)
                        {
                            frmSubstationPropertyList fSub = new frmSubstationPropertyList();
                            fSub.InitData(tlVectorControl1.SVGDocument.SvgdataUid, lay4.str_sid);
                            fSub.Show();
                        }
                        break;
                    case "ButtonJXT":
                        if (tlVectorControl1.SVGDocument.CurrentElement == null || tlVectorControl1.SVGDocument.CurrentElement.ID == "svg" || (tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString()) != "ItopVector.Core.Figure.Use")
                        {
                            MessageBox.Show("没有选择变电站！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        ParentUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        Save();
                        ParentUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        SVGFILE svg_temp = new SVGFILE();
                        svg_temp.SUID = ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).GetAttribute("id");
                        svg_temp.FILENAME = getBdzName(svg_temp.SUID);//
                        IList svglist1 = Services.BaseService.GetList("SelectSVGFILEByKey", svg_temp);
                        OpenJXT(svglist1, svg_temp);
                        //frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
                        //frmlar.Progtype = MapType;
                        //frmlar.InitData();
                        //JxtBar(); 
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
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToV":
                        tlVectorControl1.FlipY();
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToLeft":
                        tlVectorControl1.RotateSelection(-90f);
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToRight":
                        tlVectorControl1.RotateSelection(90f);
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
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
                        //tlVectorControl1.Paste();
                        PasteWithProperty();
                        break;
                    case "mDelete":
                        Delete();
                        //tlVectorControl1.Operation = ToolOperation.Select;
                        break;
                    case "mUodo":
                        tlVectorControl1.Undo();
                        break;
                    case "mRedo":
                        tlVectorControl1.Redo();
                        break;
                    #endregion
                    #region 业务操作

                    case "m_line1": //线路走廊优化
                        frmPlanList f = new frmPlanList();
                        if (f.ShowDialog() == DialogResult.Yes)
                        {
                            linekey = f.Key;
                            tlVectorControl1.Operation = ToolOperation.Select;
                            //tlVectorControl1.Operation = ToolOperation.LeadLine;

                        }
                        break;
                    case "m_subxz": //变电站选址
                        frmSubstationManager mng = new frmSubstationManager();
                        DialogResult dia = mng.ShowDialog();
                        if (dia == DialogResult.OK)
                        {
                            XZ_bdz = mng.code;
                            MessageBox.Show("请选择变电站拖放置到希望的位置。");

                        }
                        if (dia == DialogResult.Ignore)
                        {
                            string keyid = mng.KeyID;
                            string suid = mng.SUID;
                            PSP_SubstationUserNum n1=new PSP_SubstationUserNum();
                            n1.col2 = keyid;
                            IList<PSP_SubstationUserNum>  list1 = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationNum2", n1);
                            for (int i = 0; i < list1.Count; i++)
                            {
                                if (suid != list1[i].SubStationID)
                                {
                                    PSP_SubstationSelect s = new PSP_SubstationSelect();
                                    s.UID=list1[i].SubStationID;
                                    s.EleID = list1[i].userID;
                                    XmlNodeList nnn1= tlVectorControl1.SVGDocument.SelectNodes("//* [@id='"+s.EleID+"']");
                                    foreach(XmlNode node1 in nnn1){
                                        tlVectorControl1.SVGDocument.RootElement.RemoveChild(node1);
                                    }
                                    Services.BaseService.Update("DeletePSP_SubstationSelect",s);

                                }
                            }
                            tlVectorControl1.Refresh();

                        }
                        break;
                    case "mSubPrint":
                        //tlVectorControl1.CurrentOperation = ToolOperation.InterEnclosure;
                        //SubPrint = true;
                        //frmSubPrint s = new frmSubPrint();
                        ////s.Open(tlVectorControl1.SVGDocument);
                        //s.Show();
                        Hashtable HashTable1 = new Hashtable();
                        HashTable1.Add("SUID", tlVectorControl1.SVGDocument.SvgdataUid);
                        Services.BaseService.Update("UpdateGlebePropertyAll", HashTable1);
                        break;
                    case "mJQLeadLine":
                        tlVectorControl1.Operation = ToolOperation.Select;
                        frmAddLine aLine = new frmAddLine();
                        if (aLine.ShowDialog() == DialogResult.OK)
                        {
                            string points = "";
                            ArrayList list = aLine.list;
                            LineInfo line = aLine.line;
                            string lineWidth = aLine.LineWidth;
                            //ICollection Ilist = list.Values;
                            //IEnumerator IEnum=Ilist.GetEnumerator();
                            for (int n = 0; n < list.Count; n++)
                            {
                                //while (IEnum.MoveNext())
                                //{
                                string[] str = ((string)list[n]).Split(',');
                                //string[] str = ((string)IEnum.Current).Split(',');
                                string[] JWD1 = str[0].Split(' ');
                                double J1 = Convert.ToDouble(JWD1[0]);
                                double W1 = Convert.ToDouble(JWD1[1]);
                                double D1 = Convert.ToDouble(JWD1[2]);
                                string[] JWD2 = str[1].Split(' ');
                                double J2 = Convert.ToDouble(JWD2[0]);
                                double W2 = Convert.ToDouble(JWD2[1]);
                                double D2 = Convert.ToDouble(JWD2[2]);

                                double JD = J1 + W1 / 60 + D1 / 3600;
                                double WD = J2 + W2 / 60 + D2 / 3600;
                                IntXY xy = mapview.getXY(JD, WD);
                                points = points + (-xy.X / (double)tlVectorControl1.ScaleRatio) + " " + (-xy.Y / (double)tlVectorControl1.ScaleRatio) + ",";
                                //}
                            }
                            if (points.Length > 1)
                            {
                                points = points.Substring(0, points.Length - 1);
                            }

                            //string styleValue = "";
                            //if (line.ObligateField1 == "规划")
                            //{
                            //    styleValue = "stroke-dasharray:4 4;stroke-width:" + lineWidth + ";";
                            //}
                            //else
                            //{
                            //    styleValue = "stroke-width:" + lineWidth + ";";
                            //}
                            //styleValue = styleValue + "stroke:" + ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(line.ObligateField2)));

                            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                            n1.SetAttribute("IsLead", "1");
                            n1.SetAttribute("points", points);
                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                            // n1.SetAttribute("style", styleValue);
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                            line.UID = Guid.NewGuid().ToString();
                            line.EleID = n1.GetAttribute("id");
                            line.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            Services.BaseService.Create<LineInfo>(line);

                        }
                        break;
                    case "mLeadLine":
                        //tlVectorControl1.DrawArea.BackgroundImage = System.Drawing.Image.FromFile("f:\\back11.jpg");
                        //tlVectorControl1.DrawArea.BackgroundImageLayout = ImageLayout.Center;

                        if (!getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl1.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("电网规划层"))
                        {
                            MessageBox.Show("请选择电网规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        // sgt1.Visible = true;
                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.LeadLine;
                        break;
                    case "mAreaPoly":
                        
                        if (!getlayer(SvgDocument.currentLayer, "城市规划层", tlVectorControl1.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("城市规划层"))
                        {
                            MessageBox.Show("请选择城市规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }


                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.AreaPolygon;

                        break;
                    case "mFx":
                        SubPrint = false;
                        bool ck = false;
                        CheckedListBox.CheckedItemCollection ckcol = frmlar.checkedListBox1.CheckedItems;
                        for (int i = 0; i < ckcol.Count; i++)
                        {
                            Layer _lar = ckcol[i] as Layer;
                            if (_lar.GetAttribute("layerType") == "城市规划层")
                            {
                                ck = true;
                            }
                        }
                        if (!ck)
                        {
                            MessageBox.Show("请打开城市规划层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (!getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl1.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("供电区域层"))
                        {
                            MessageBox.Show("请选择电网规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.InterEnclosure;
                        MapType = "接线图";
                        break;
                    case "mGhfx":
                        if (!getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl1.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("供电区域层"))
                        {
                            MessageBox.Show("请选择电网规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        tlVectorControl1.Operation = ToolOperation.Enclosure;
                        // SvgDocument.currentLayer = getlayer("供电区域层", tlVectorControl1.SVGDocument.getLayerList()).ID;
                        MapType = "接线图";
                        //bar2.Visible = false;
                        break;
                    //case "mEdit":
                    //    //bar2.Visible = true;
                    //    SvgDocument.currentLayer = "layer97052";
                    //    MapType = "接线图";
                    //    break;
                    case "mFzzj":　//放置注记
                        if (!getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl1.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("供电区域层"))
                        {
                            MessageBox.Show("请选择电网规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        //LayerBox.ComboBoxEx.SelectedIndex = 2;
                        tlVectorControl1.Operation = ToolOperation.Select;
                        MapType = "规划统计";
                        break;
                    case "mDkwh":　//地块维护
                        frmPropertyClass frmProp = new frmPropertyClass();
                        frmProp.ShowDialog();
                        break;
                    case "mDkfl": //地块分类
                        if (tlVectorControl1.SVGDocument.CurrentElement == null || tlVectorControl1.SVGDocument.CurrentElement.ID == "svg")
                        {
                            MessageBox.Show("请选择地块。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        frmUsePropertySelect frmUseSel = new frmUsePropertySelect();
                        frmUseSel.InitData(tlVectorControl1.SVGDocument.CurrentElement.ID, tlVectorControl1.SVGDocument.SvgdataUid);
                        frmUseSel.ShowDialog();
                        break;
                    case "mGldt": //关联地图
                        if (tlVectorControl1.SVGDocument.CurrentElement == null || tlVectorControl1.SVGDocument.CurrentElement.ID == "svg")
                        {
                            MessageBox.Show("请选择地块。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        frmFileSelect frmSel = new frmFileSelect();
                        frmSel.InitData(tlVectorControl1.SVGDocument.CurrentElement.ID, tlVectorControl1.SVGDocument.SvgdataUid, true);
                        frmSel.ShowDialog();
                        break;
                    case "mPriQu":
                        SubPrint = true;
                        tlVectorControl1.Operation = ToolOperation.InterEnclosurePrint;
                        break;

                    case "mReCompute":
                        if (MessageBox.Show("确认要重新计算全图的电量和负荷么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string scale = tlVectorControl1.SVGDocument.getViewScale();
                            if (scale != "")
                            {
                                Recalculate(Convert.ToDecimal(scale));
                            }
                            else
                            {
                                Recalculate(1);
                            }

                            MessageBox.Show("重新计算完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case "mXLine":
                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.XPolyLine;
                        break;
                    case "mYLine":
                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.YPolyLine;
                        break;

                    case "mFhbz":
                        if (MessageBox.Show("是否生成负荷标注?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Fhbz();
                        }
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
                    case "m_tp":
                        frmImgManager frm = new frmImgManager();
                        frm.StrName = "";
                        frm.StrRemark = "";

                        frm.ShowDialog();
                        break;
                    case "m_reDraw":
                        string svguid = tlVectorControl1.SVGDocument.SvgdataUid;
                        XmlNodeList nn1 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1']");
                        LineType lt = new LineType();
                        IList tpList = Services.BaseService.GetList("SelectLineTypeList", lt);
                        Hashtable dkHs = new Hashtable();

                        for (int i = 0; i < tpList.Count; i++)
                        {
                            LineType _gle = (LineType)tpList[i];
                            dkHs.Add(_gle.TypeName, _gle.Color);
                        }
                        bool bo = tlVectorControl1.SVGDocument.Update;
                        tlVectorControl1.SVGDocument.Update = false;
                        for (int j = 0; j < nn1.Count; j++)
                        {
                            XmlElement _node1 = (XmlElement)nn1.Item(j);
                            LineInfo line = new LineInfo();
                            line.SvgUID = svguid;
                            line.EleID = _node1.GetAttribute("id");
                            line = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", line);
                            if (line != null)
                            {
                                string t = (string)dkHs[line.Voltage + "kV"];
                                string color1 = ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(t)));
                                ItopVector.Core.Func.AttributeFunc.SetAttributeValue(_node1, "stroke", color1);
                            }
                        }
                        tlVectorControl1.SVGDocument.Update = bo;
                        break;
                    case "m_subColor":
                        string svguid1 = tlVectorControl1.SVGDocument.SvgdataUid;
                        XmlNodeList nn2 = tlVectorControl1.SVGDocument.SelectNodes("svg/defs/symbol");
                        LineType lt1 = new LineType();
                        IList tpList1 = Services.BaseService.GetList("SelectLineTypeList", lt1);
                        Hashtable dkHs1 = new Hashtable();
                        for (int i = 0; i < tpList1.Count; i++)
                        {
                            LineType _gle = (LineType)tpList1[i];
                            dkHs1.Add(_gle.TypeName.ToLower(), _gle.Color);
                        }
                        bool bo1 = tlVectorControl1.SVGDocument.Update;
                        tlVectorControl1.SVGDocument.Update = false;
                        Regex regex = new Regex(@"\d{2,3}(?=kv)");
                        foreach (Symbol _node1 in nn2)
                        {
                            string subName = _node1.GetAttribute("label").ToLower();

                            Match match1 = regex.Match(subName);
                            if (match1.Success)
                            {
                                try
                                {
                                    string t = (string)dkHs1[match1.Value + "kv"];
                                    //if (match1.Value == "220")
                                    //{
                                    //    t = t;
                                    //}
                                    string color1 = ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(t)));
                                    foreach (SvgElement element in _node1.GraphList)
                                    {

                                        if (element.SvgAttributes.ContainsKey("stroke") && element.SvgAttributes["stroke"].ToString() != "#FFFFFF")
                                        {
                                            ItopVector.Core.Func.AttributeFunc.SetAttributeValue(element, "stroke", color1);
                                        }
                                        if (element.SvgAttributes.ContainsKey("fill") && element.SvgAttributes["fill"].ToString() != "#FFFFFF")
                                        {
                                            ItopVector.Core.Func.AttributeFunc.SetAttributeValue(element, "fill", color1);
                                        }
                                        if (element.SvgAttributes.ContainsKey("hatch-color") && element.SvgAttributes["hatch-color"].ToString() != "#FFFFFF")
                                        {
                                            ItopVector.Core.Func.AttributeFunc.SetAttributeValue(element, "hatch-color", color1);
                                        }

                                    }
                                }
                                catch { }
                            }
                            //XmlElement _node1 = (XmlElement)nn2.Item(j);
                            //substation line = new substation();
                            //line.SvgUID = svguid1;
                            //line.EleID = _node1.GetAttribute("id");
                            //line = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", line);
                            //if (line != null)
                            //{
                            //    string t = (string)dkHs1[line.ObligateField1 + "kV"];
                            //    string ut = _node1.GetAttribute("usestyle");
                            //    string color1 = ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(t)));
                            //    ItopVector.Core.Func.AttributeFunc.SetAttributeValue(_node1, "stroke", color1);
                            //    if (ut != "true")
                            //    {
                            //        ItopVector.Core.Func.AttributeFunc.SetAttributeValue(_node1, "usestyle", "true");
                            //    }
                            //    else
                            //    {
                            //        ItopVector.Core.Func.AttributeFunc.SetAttributeValue(_node1, "usestyle", "false");
                            //    }
                            //}
                        }
                        tlVectorControl1.SVGDocument.Update = bo1;
                        break;
                    #endregion
                    #region 图层操作
                    case "layerImport":
                        tlVectorControl1.Operation = ToolOperation.FreePath;
                        layerImport();
                        break;
                    case "layerExport":
                        layerExport();
                        break;
                    case "m_kbsText":
                        openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.Filter = "Excel文件(*.xls)|*.xls";
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            DataSet ds= ImportExcel(openFileDialog1.FileName);
                            DataTable dt1 = ds.Tables[0];
                            foreach (DataRow r1 in dt1.Rows)
                            {
                                if (r1[0].ToString() != "")
                                {
                                    SvgElement ele = null;
                                    double JD = 0;
                                    double WD = 0;
                                    JD = Convert.ToDouble(r1[12]);
                                    WD = Convert.ToDouble(r1[13]);
                                    PointF fnt= mapview.ParseToPoint(JD,WD);
                                    XmlElement t1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;  
                                    t1.SetAttribute("x",Convert.ToString( fnt.X / (float)tlVectorControl1.ScaleRatio+8));
                                    t1.SetAttribute("y",Convert.ToString( fnt.Y / (float)tlVectorControl1.ScaleRatio));
                                    
                                    t1.SetAttribute("layer", SvgDocument.currentLayer);
                                    t1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                    t1.SetAttribute("font-famliy", "宋体");
                                    t1.SetAttribute("font-size", "6");
                                    t1.InnerText = r1[2].ToString();
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(t1);
                                }
                            }
                            tlVectorControl1.Refresh();
                        }
                        break;
                    case "m_fText":
                         openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.Filter = "Excel文件(*.xls)|*.xls";
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            DataSet ds = ImportExcel(openFileDialog1.FileName);
                            DataTable dt1 = ds.Tables[0];
                            foreach (DataRow r1 in dt1.Rows)
                            {
                                if (r1[0].ToString() != "")
                                {
                                    SvgElement ele = null;
                                    double JD = 0;
                                    double WD = 0;
                                    JD = Convert.ToDouble(r1[9]);
                                    WD = Convert.ToDouble(r1[10]);
                                    PointF fnt = mapview.ParseToPoint(JD, WD);
                                    XmlElement t1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                    t1.SetAttribute("x", Convert.ToString( fnt.X / (float)tlVectorControl1.ScaleRatio+8));
                                    t1.SetAttribute("y", Convert.ToString( fnt.Y / (float)tlVectorControl1.ScaleRatio));

                                    t1.SetAttribute("layer", SvgDocument.currentLayer);
                                    t1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                    t1.SetAttribute("font-famliy", "宋体");
                                    t1.SetAttribute("font-size", "6");
                                    t1.InnerText = r1[1].ToString();
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(t1);
                                }
                            }
                            tlVectorControl1.Refresh();
                        }
                        break;
                    case "m_inkbs":
                        openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.Filter = "Excel文件(*.xls)|*.xls";
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            DataSet ds= ImportExcel(openFileDialog1.FileName);
                            DataTable dt1 = ds.Tables[0];
                            foreach (DataRow r1 in dt1.Rows)
                            {
                                if (r1[0].ToString() != "")
                                {
                                    SvgElement ele = null;
                                    double JD = 0;
                                    double WD = 0;
                                    JD = Convert.ToDouble(r1[12]);
                                    WD = Convert.ToDouble(r1[13]);
                                    //IntXY xy = mapview.getXY(JD, WD);
                                    PointF fnt= mapview.ParseToPoint(JD,WD);
                                    if (r1[7].ToString() == "运行")
                                    {
                                        ele = tlVectorControl1.CreateBySymbolID("kbs-111", new PointF((float)(fnt.X / (float)tlVectorControl1.ScaleRatio), (float)(fnt.Y / (float)tlVectorControl1.ScaleRatio)));

                                    }
                                    else
                                    {
                                        ele = tlVectorControl1.CreateBySymbolID("kbs-112", new PointF((float)(fnt.X / (float)tlVectorControl1.ScaleRatio), (float)(fnt.Y / (float)tlVectorControl1.ScaleRatio)));
                                    }
                                    ele = tlVectorControl1.AddShape(ele, Point.Empty);
                                    ele.SetAttribute("layer", tlVectorControl1.SVGDocument.CurrentLayer.ID);

                                    //points = points + (-xy.X / (decimal)tlVectorControl1.ScaleRatio) + " " + (-xy.Y / (decimal)tlVectorControl1.ScaleRatio) + ",";
                                    PSP_Gra_item item = new PSP_Gra_item();
                                    item.UID = Guid.NewGuid().ToString();
                                    item.EleKeyID = r1[1].ToString();
                                    item.EleName = r1[2].ToString();
                                    item.EleID = ele.ID;
                                    item.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    item.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;

                                    item.col1 = r1[7].ToString();
                                    item.col2 = r1[3].ToString();
                                    item.col3 = r1[4].ToString();
                                    item.col4 = r1[5].ToString();
                                    item.col5 = r1[6].ToString();
                                    item.col6 = r1[8].ToString();
                                    item.col7 = r1[9].ToString();
                                    item.col8 = "kbs";
                                    Services.BaseService.Create<PSP_Gra_item>(item);
                                }
                            }
                        }
                        break;
                    case "m_infjx":
                        openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.Filter = "Excel文件(*.xls)|*.xls";
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            DataSet ds = ImportExcel(openFileDialog1.FileName);
                            DataTable dt1 = ds.Tables[0];
                            foreach (DataRow r1 in dt1.Rows)
                            {
                                if (r1[0].ToString() != "")
                                {
                                    SvgElement ele = null;
                                    double JD = 0;
                                    double WD = 0;
                                    JD = Convert.ToDouble(r1[9]);
                                    WD = Convert.ToDouble(r1[10]);
                                    //IntXY xy = mapview.getXY(JD, WD);
                                    PointF fnt = mapview.ParseToPoint(JD, WD);
                                    if (r1[4].ToString() == "运行")
                                    {
                                        ele = tlVectorControl1.CreateBySymbolID("fjx-111", new PointF((float)(fnt.X / (float)tlVectorControl1.ScaleRatio), (float)(fnt.Y / (float)tlVectorControl1.ScaleRatio)));

                                    }
                                    else
                                    {
                                        ele = tlVectorControl1.CreateBySymbolID("fjx-112", new PointF((float)(fnt.X / (float)tlVectorControl1.ScaleRatio), (float)(fnt.Y / (float)tlVectorControl1.ScaleRatio)));
                                    }
                                    ele = tlVectorControl1.AddShape(ele, Point.Empty);
                                    ele.SetAttribute("layer", tlVectorControl1.SVGDocument.CurrentLayer.ID);

                                    //points = points + (-xy.X / (decimal)tlVectorControl1.ScaleRatio) + " " + (-xy.Y / (decimal)tlVectorControl1.ScaleRatio) + ",";
                                    PSP_Gra_item item = new PSP_Gra_item();
                                    item.UID = Guid.NewGuid().ToString();
                                    item.EleKeyID = r1[2].ToString();
                                    item.EleName = r1[1].ToString();
                                    item.EleID = ele.ID;
                                    item.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    item.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;

                                    item.col1 = r1[4].ToString();
                                    item.col2 = r1[3].ToString();
                                    item.col3 = r1[5].ToString();
                                    item.col4 = r1[6].ToString();
                                    //item.col5 = r1[6].ToString();
                                    //item.col6 = r1[8].ToString();
                                    //item.col7 = r1[9].ToString();
                                    item.col8 = "fjx";
                                    Services.BaseService.Create<PSP_Gra_item>(item);
                                }
                            }
                        }
                        break;
                    case "m_inbyq":
                        openFileDialog1 = new OpenFileDialog();
                        openFileDialog1.Filter = "Excel文件(*.xls)|*.xls";
                        if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            DataSet ds = ImportExcel(openFileDialog1.FileName);
                            DataTable dt1 = ds.Tables[0];
                            foreach (DataRow r1 in dt1.Rows)
                            {
                                if (r1[0].ToString() != "")
                                {
                                    SvgElement ele = null;
                                    double JD = 0;
                                    double WD = 0;
                                    JD = Convert.ToDouble(r1[9]);
                                    WD = Convert.ToDouble(r1[10]);
                                    //IntXY xy = mapview.getXY(JD, WD);
                                    PointF fnt = mapview.ParseToPoint(JD, WD);
                                    if (r1[4].ToString() == "运行")
                                    {
                                        ele = tlVectorControl1.CreateBySymbolID("byq-111", new PointF((float)(fnt.X / (float)tlVectorControl1.ScaleRatio), (float)(fnt.Y / (float)tlVectorControl1.ScaleRatio)));

                                    }
                                    else
                                    {
                                        ele = tlVectorControl1.CreateBySymbolID("byq-112", new PointF((float)(fnt.X / (float)tlVectorControl1.ScaleRatio), (float)(fnt.Y / (float)tlVectorControl1.ScaleRatio)));
                                    }
                                    ele = tlVectorControl1.AddShape(ele, Point.Empty);
                                    ele.SetAttribute("layer", tlVectorControl1.SVGDocument.CurrentLayer.ID);

                                    //points = points + (-xy.X / (decimal)tlVectorControl1.ScaleRatio) + " " + (-xy.Y / (decimal)tlVectorControl1.ScaleRatio) + ",";
                                    PSP_Gra_item item = new PSP_Gra_item();
                                    item.UID = Guid.NewGuid().ToString();
                                    item.EleKeyID = r1[2].ToString();
                                    item.EleName = r1[1].ToString();
                                    item.EleID = ele.ID;
                                    item.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    item.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;

                                    item.col1 = r1[4].ToString();
                                    item.col2 = r1[3].ToString();
                                    item.col3 = r1[5].ToString();
                                    item.col4 = r1[6].ToString();
                                    item.col8 = "byq";
                                    Services.BaseService.Create<PSP_Gra_item>(item);
                                }
                            }
                        }
                        break;
                    #endregion
                    #region 未来联动
                    case "m_bxz":

                        //tlVectorControl1.GoLocation();
                        Gh_BXZ();
                        break;
                    case "m_jp":
                        tlVectorControl1.ClipScreen(true);
                        break;
                    case "m_ld":
                        if (SvgDocument.currentLayer == "")
                        {
                            MessageBox.Show("请选择图层", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (MessageBox.Show("确认要以当前选中年份为准调整以后年度的变电站及线路位置么？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Linkage();
                        }
                        break;
                    case "m_fz":
                        if (tlVectorControl1.SVGDocument.CurrentElement == null || tlVectorControl1.SVGDocument.CurrentElement.ID == "svg")
                        {
                            MessageBox.Show("请选择图元", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        CopyEle();
                        break;
                    case "m_cx":

                        xltProcessor.GoLocation(tlVectorControl1.SVGDocument.SvgdataUid, frmlar.getSelectedLayer());
                        //XmlNodeList n1111 = tlVectorControl1.SVGDocument.SelectNodes("svg/polygon [@IsArea='1']");
                        //string a = "1";
                        break;
                    #endregion
                    #region 线路优选
                    case "bt_edit":

                        break;
                    case "bt_start":
                        if (linekey == "")
                        {
                            MessageBox.Show("请选择线路所属的方案。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        sel_sym = "bt_start";
                        tlVectorControl1.Operation = ToolOperation.Symbol;
                        break;
                    case "bt_end":
                        if (sel_start_point=="")
                        {
                            MessageBox.Show("请选择线路起点。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            return;
                        }
                        sel_sym = "bt_end";
                        tlVectorControl1.Operation = ToolOperation.Symbol;
                        break;
                    case "bt_must":
                        if (sel_start_point == "")
                        {
                            MessageBox.Show("请选择线路起点。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        sel_sym = "bt_must";
                        tlVectorControl1.Operation = ToolOperation.Symbol;
                        break;
                    case "bt_point":
                        if (sel_start_point == "")
                        {
                            MessageBox.Show("请选择线路起点。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        sel_sym = "bt_point";
                        tlVectorControl1.Operation = ToolOperation.Symbol;
                        break;
                    case "bt_make":
                        if (sel_start_point == "")
                        {
                            MessageBox.Show("请选择线路起点。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        frmInputDialog frm_input = new frmInputDialog();
                        if( frm_input.ShowDialog()==DialogResult.OK){

                        SortedList orderlist = new SortedList();
                        
                        XmlNodeList XLlist = tlVectorControl1.SVGDocument.SelectNodes("//*[@start_point=\"" + sel_start_point + "\"]");
                        try
                        {
                            for (int i = 0; i < XLlist.Count; i++)
                            {
                                XmlElement node = (XmlElement)XLlist[i];
                                orderlist.Add(Convert.ToInt32(node.GetAttribute("order")), node);
                            }
                        }
                        catch(Exception ex1){
                            MessageBox.Show("存在相同的节点顺序号，请修改。\n\r"+ex1.Message,"提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            return;
                        }
                        PointF[] _points = new PointF[XLlist.Count];
                        for (int i = 0; i < orderlist.Count;i++ )
                        {
                            PointF[] f1 = new PointF[1] { new PointF(((Use)orderlist.GetByIndex(i)).X+6, ((Use)orderlist.GetByIndex(i)).Y+6) };
                            ((Use)orderlist.GetByIndex(i)).Transform.Matrix.TransformPoints(f1);
                            _points[i] = f1[0];
                        }
                        string str_points = "";
                        for(int i=0;i<_points.Length;i++){
                            str_points = str_points + _points[i].X + " " + _points[i].Y + ",";
                        }
                        if (str_points.Length > 1)
                        {
                            str_points = str_points.Substring(0, str_points.Length - 1);
                        }

                        XmlElement _templine = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                        _templine.SetAttribute("IsLead", "1");
                        _templine.SetAttribute("points", str_points);
                        _templine.SetAttribute("layer", SvgDocument.currentLayer);
                        Random dom = new Random();
                        int int_d=dom.Next(99999);
                        string styleValue = "stroke:" + ColorTranslator.ToHtml(Color.FromArgb(int_d));
                        _templine.SetAttribute("style", styleValue);
                        XmlNode tt_node= tlVectorControl1.SVGDocument.RootElement.AppendChild(_templine);
                            LineInfo gh_line = new LineInfo();
                            gh_line.UID = Guid.NewGuid().ToString();
                            gh_line.EleID = _templine.GetAttribute("id");
                            gh_line.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            Services.BaseService.Create<LineInfo>(gh_line);
                            tlVectorControl1.Refresh();
                            if (MessageBox.Show("是否删除该线路的参考点？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                tlVectorControl1.SVGDocument.SelectCollection.Clear();
                                for (int i = 0; i < XLlist.Count; i++)
                                {
                                    if (((SvgElement)XLlist[i]).GetAttribute("xlink:href").Contains("XL_GT_4") || ((SvgElement)XLlist[i]).GetAttribute("xlink:href").Contains("XL_GT_3"))
                                    {
                                        tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)XLlist[i]);
                                    }

                                }
                                tlVectorControl1.Delete();
                            }
                         string str = "";
                         LineList1 line1 = new LineList1();
                         line1.UID = Guid.NewGuid().ToString();
                         //line1.LineEleID = tlVectorControl1.SVGDocument.CurrentElement.ID;
                         line1.PointNum = ((Polyline)(tt_node)).Points.Length - 2;
                         line1.Coefficient = (decimal)(1.02);
                         line1.Length = TLMath.getPolylineLength(((Polyline)(tt_node)), Convert.ToDecimal(tlVectorControl1.ScaleRatio));
                         line1.Length2 = TLMath.getPolylineLength(((Polyline)(tt_node)), Convert.ToDecimal(tlVectorControl1.ScaleRatio)) * Convert.ToDecimal(1.02);
                         PointF[] pnt = ((Polyline)(tt_node)).Points;
                         if (pnt.Length < 3) return;
                         for (int i = 0; i < pnt.Length; i++)
                         {
                             double ang = TLMath.getLineAngle(pnt[i], pnt[i + 1], pnt[i + 2]);
                             if (ang * 57.3 > 60)
                             {
                                 MessageBox.Show("线路转角不能大于60度。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                 tlVectorControl1.Delete();
                                 return;
                             }
                             str = str + "第" + (i + 1) + "转角：" + Convert.ToDouble(ang * 57.3).ToString("##.##") + "度。\r\n";
                             if (i == pnt.Length - 3)
                             {
                                 break;
                             }
                         }
                         line1.TurnAngle = str;
                         line1.col1 = linekey;
   
                         line1.LineName = frm_input.InputStr;
                         line1.LineEleID = ((SvgElement)tt_node).ID;
                         Services.BaseService.Create<LineList1>(line1);
                         sel_start_point = "";
                         tlVectorControl1.Operation = ToolOperation.Select;
                        }
                        break;
                    #endregion
                }
            }
        }
        public void Delete()
        {
            if (tlVectorControl1.SVGDocument.CurrentElement != null && tlVectorControl1.SVGDocument.CurrentElement.ID != "svg")
            {
                frmMessageBox msg = new frmMessageBox();
                if (msg.ShowDialog() == DialogResult.OK)
                {
                    //if (msg.ck)
                    //{
                    // if(MessageBox.Show("确认删除么？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes){
                    for (int i = 0; i < tlVectorControl1.SVGDocument.SelectCollection.Count; i++)
                    {
                        string larid = "";
                        larid = ((SvgElement)tlVectorControl1.SVGDocument.SelectCollection[i]).GetAttribute("layer");
                        if (!ChangeLayerList.Contains(larid))
                        {
                            ChangeLayerList.Add(larid);
                        }
                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Image")
                        {
                            SVG_IMAGE s_img = new SVG_IMAGE();

                            s_img.SUID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                            Services.BaseService.Delete<SVG_IMAGE>(s_img);
                        }
                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Polygon")
                        {
                            glebeProperty gle = new glebeProperty();
                            gle.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            gle.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                            Services.BaseService.Update("DeleteglebePropertyByEleID", gle);
                            SVG_ENTITY pro = new SVG_ENTITY();
                            pro.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                            pro.svgID = tlVectorControl1.SVGDocument.SvgdataUid;
                            Services.BaseService.Update("DeleteSVG_ENTITYByEleID", pro);
                        }
                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Polyline" || tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Line")
                        {
                            LineInfo _line = new LineInfo();
                            _line.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            _line.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                            LineInfo temp = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", _line);
                            if (temp != null)
                            {
                                Services.BaseService.Update("DeleteLinePropertyByEleID", _line);

                                Services.BaseService.Update("DeleteLine_InfoByCode", temp.UID);
                            }
                            SVG_ENTITY pro = new SVG_ENTITY();
                            pro.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                            pro.svgID = tlVectorControl1.SVGDocument.SvgdataUid;
                            Services.BaseService.Update("DeleteSVG_ENTITYByEleID", pro);
                        }
                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Use")
                        {
                            string str_name = ((XmlElement)(tlVectorControl1.SVGDocument.SelectCollection[i])).GetAttribute("xlink:href");
                            if (str_name.Contains("Substation"))
                            {
                                substation _sub = new substation();
                                _sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                _sub.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;

                                substation temp = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _sub);

                                if (temp != null)
                                {
                                    Services.BaseService.Update("DeletesubstationByEleID", _sub);

                                    Services.BaseService.Update("DeleteSubstation_InfoByCode", temp.UID);
                                }
                                SVG_ENTITY pro = new SVG_ENTITY();
                                pro.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                                pro.svgID = tlVectorControl1.SVGDocument.SvgdataUid;
                                Services.BaseService.Update("DeleteSVG_ENTITYByEleID", pro);

                                substation p = new substation();
                                p.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                                p.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                Services.BaseService.Update("DeletesubstationByEleID", p);

                                PSP_SubstationSelect sel = new PSP_SubstationSelect();
                                sel.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                                sel.SvgID = tlVectorControl1.SVGDocument.SvgdataUid;
                                Services.BaseService.Update("DeletePSP_SubstationByEleID", sel);
                            }
                        }
                        if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.ConnectLine")
                        {
                            ConnectLine cline = (ConnectLine)tlVectorControl1.SVGDocument.SelectCollection[i];
                            if (cline.StartGraph != null)
                            {
                                SvgElement ele = (SvgElement)cline.StartGraph;
                                if (!ele.GetAttribute("xlink:href").Contains("Substation"))
                                {
                                    tlVectorControl1.SVGDocument.SelectCollection.Add(cline.StartGraph);
                                }
                            }
                            if (cline.EndGraph != null)
                            {
                                tlVectorControl1.SVGDocument.SelectCollection.Add(cline.EndGraph);
                            }
                        }


                    }
                    //}
                    /* else
                     {
                         for (int i = 0; i < tlVectorControl1.SVGDocument.SelectCollection.Count; i++)
                         {
                             string larid = "";
                             larid = ((SvgElement)tlVectorControl1.SVGDocument.SelectCollection[i]).GetAttribute("layer");
                             if (!ChangeLayerList.Contains(larid))
                             {
                                 ChangeLayerList.Add(larid);
                             }
                             if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Image")
                             {
                                 SVG_IMAGE s_img = new SVG_IMAGE();

                                 s_img.SUID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                                 Services.BaseService.Delete<SVG_IMAGE>(s_img);
                             }
                             if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Polyline" || tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Line")
                             {
                                 LineInfo _line = new LineInfo();
                                 _line.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                 _line.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;

                                 LineInfo linetemp = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", _line);
                                 if (linetemp != null)
                                 {
                                     PowerProTypes temp = (PowerProTypes)Services.BaseService.GetObject("SelectPowerProTypesByCode", linetemp.UID);
                                     if (temp != null)
                                     {
                                         linetemp.EleID = "";
                                         Services.BaseService.Update<LineInfo>(linetemp);
                                     }
                                     else
                                     {
                                         Services.BaseService.Update("DeleteLineInfo", linetemp);
                                     }
                                 }

                             }
                             if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Use")
                             {
                                 string str_name = ((XmlElement)(tlVectorControl1.SVGDocument.SelectCollection[i])).GetAttribute("xlink:href");
                                 if (str_name.Contains("Substation"))
                                 {
                                     substation _sub = new substation();
                                     _sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                     _sub.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;

                                     substation subtemp = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _sub);
                                     if (subtemp != null)
                                     {
                                         PowerProTypes temp = (PowerProTypes)Services.BaseService.GetObject("SelectPowerProTypesByCode", subtemp.UID);
                                         if (temp != null)
                                         {
                                             subtemp.EleID = "";
                                             Services.BaseService.Update<substation>(subtemp);
                                         }
                                         else
                                         {
                                             Services.BaseService.Update("Deletesubstation", subtemp);
                                         }
                                     }
                                 }
                             }
                             if (tlVectorControl1.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.ConnectLine")
                             {
                                 ConnectLine cline = (ConnectLine)tlVectorControl1.SVGDocument.SelectCollection[i];
                                 if (cline.StartGraph != null)
                                 {
                                     SvgElement ele = (SvgElement)cline.StartGraph;
                                     if (!ele.GetAttribute("xlink:href").Contains("Substation"))
                                     {
                                         tlVectorControl1.SVGDocument.SelectCollection.Add(cline.StartGraph);
                                     }
                                 }
                                 if (cline.EndGraph != null)
                                 {
                                     tlVectorControl1.SVGDocument.SelectCollection.Add(cline.EndGraph);
                                 }
                             }
                         }

                     }*/

                    tlVectorControl1.Delete();
                }
            }
        }
        public void SaveButton()
        {
            string[] strn = yearID.Split(",".ToCharArray());
            ArrayList ilist = new ArrayList();
            for (int i = 0; i < strn.Length; i++)
            {
                if (strn[i] != "''")
                {
                    ilist.Add(strn[i].Replace("'", ""));
                }
            }
            if (ilist.Count == 1)
            {
                //SaveID.Clear();
                SaveID = ilist;
                SaveAllLayer();
                Save();
            }
            else
            {
                frmLayerGradeSave frmSave = new frmLayerGradeSave();
                frmSave.SymbolDoc = tlVectorControl1.SVGDocument;
                frmSave.savelist = ilist;
                frmSave.InitData(tlVectorControl1.SVGDocument.SvgdataUid);

                if (frmSave.ShowDialog() == DialogResult.OK)
                {
                    //SaveID.Clear();
                    SaveID = frmSave.GetSelectNode2();
                    SaveAllLayer();
                    Save();
                }
            }
        }
        /// <summary>
        /// 规划变现状
        /// </summary>
        public void Gh_BXZ()
        {
            Layer lay1 = null;
            string stype = "";
            int year = 0;
            SortedList LineList = new SortedList();
            SortedList subList = new SortedList();

            lay1 = tlVectorControl1.SVGDocument.CurrentLayer;
            try
            {
                year = Convert.ToInt32(lay1.Label.Substring(0, 4));
            }
            catch (Exception e1)
            {
                MessageBox.Show("选择图层的图层名称不包含年份信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (year > System.DateTime.Today.Year)
            {
                MessageBox.Show("选择图层信息大于当前年份，不能变成现状数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (SvgElement ele in lay1.GraphList)
            {
                if (ele.Name == "use")
                {
                    if (((Use)ele).GraphId.Contains("gh"))
                    {
                        string elename;
                        //if (((Use)ele).GraphId == "gh-Substation500")
                        //{
                        //     elename= "Substation500-1";
                        //}
                        //else
                        //{
                        //    elename = ((Use)ele).GraphId.Substring(3);
                        //}
                        //PointF pnt1 = TLMath.getUseOffset("#" + ((Use)ele).GraphId);
                        //PointF pnt2 = TLMath.getUseOffset("#" + elename);
                        //float x = pnt2.X - pnt1.X;
                        //float y = pnt2.Y - pnt1.Y;

                        //ele.SetAttribute("xlink:href", elename);

                        //Matrix matrix2 = ((Use)ele).Transform.Matrix.Clone();
                        //matrix2.Translate(-x, -y, MatrixOrder.Append);
                        //((Use)ele).Transform = new ItopVector.Core.Types.Transf(matrix2);

                        substation sub1 = new substation();
                        sub1.EleID = ele.ID;
                        sub1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        sub1 = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", sub1);
                        if (sub1 != null)
                        {
                            Substation_Info sub = new Substation_Info();
                            sub.Code = ele.ID;
                            sub = (Substation_Info)Services.BaseService.GetObject("SelectSubstation_InfoByCode", sub);
                            if (sub == null)
                            {
                                sub = new Substation_Info();
                                sub.UID = Guid.NewGuid().ToString();
                                sub.Code = sub1.EleID;
                                sub.Flag = "1";
                                sub.Title = sub1.EleName;
                                sub.L1 = Convert.ToInt32(sub1.ObligateField1);
                                sub.L2 = Convert.ToDouble(sub1.Number);
                                sub.L9 = Convert.ToDouble(sub1.Burthen);
                                sub.L10 = Convert.ToDouble(sub1.ObligateField2.Replace("%", ""));
                                sub.IsConn = "是";
                                Services.BaseService.Create<Substation_Info>(sub);
                            }
                        }

                    }
                }
                if (ele.Name == "polyline" && ele.GetAttribute("IsLead") == "1")
                {
                    string str = ele.GetAttribute("style");
                    str = str.Replace("stroke-dasharray:" + ghType + ";", "");
                    ele.RemoveAttribute("style");
                    ele.SetAttribute("style", str);
                    LineInfo line1 = new LineInfo();
                    line1.EleID = ele.ID;
                    line1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    line1 = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", line1);
                    if (line1 != null)
                    {
                        line1.ObligateField1 = "运行";
                        Services.BaseService.Update<LineInfo>(line1);
                        Line_Info line = new Line_Info();
                        line.Code = ele.ID;
                        line = (Line_Info)Services.BaseService.GetObject("SelectLine_InfoByCode", line);
                        if (line == null)
                        {
                            line = new Line_Info();
                            line.UID = Guid.NewGuid().ToString();
                            line.L1 = Convert.ToInt32(line1.Voltage);
                            line.L4 = line1.LineType;
                            line.L5 = Convert.ToDouble(line1.Length);
                            line.Code = line1.EleID;
                            line.IsConn = "是";
                            line.L6 = DateTime.Today.Year.ToString();
                            Services.BaseService.Create<Line_Info>(line);
                        }

                    }
                }
            }
            tlVectorControl1.Refresh();
        }
        public void CopyEle()
        {
            Layer lay1 = null;
            string stype = "";
            int year = 0;
            SortedList LineList = new SortedList();
            SortedList subList = new SortedList();

            lay1 = tlVectorControl1.SVGDocument.CurrentLayer;
            try
            {

                year = Convert.ToInt32(lay1.Label.Substring(0, 4));
                //if (lay1.Label.Contains("变电站"))
                //{
                //    stype = "变电站";
                //}
                //if (lay1.Label.Contains("线路"))
                //{
                //    stype = "线路";
                //}

                //if (stype == "")
                //{
                //    MessageBox.Show("选择的图层名称不包含线路或变电站信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    stype = "";
                //    return;
                //}

            }
            catch (Exception e1)
            {
                MessageBox.Show("选择图层的图层名称不包含年份信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //SvgElement ele = tlVectorControl1.SVGDocument.CurrentElement;
            frmSelLayer flar = new frmSelLayer();
            flar.list = getAfterLayer(year);
            if (flar.ShowDialog() == DialogResult.OK)
            {
                for (int k = 0; k < tlVectorControl1.SVGDocument.SelectCollection.Count; k++)
                //for (int k = 0; k < lay1.GraphList.Count; k++)
                {
                    //SvgElement ele = (SvgElement)lay1.GraphList[k];
                    SvgElement ele = (SvgElement)tlVectorControl1.SVGDocument.SelectCollection[k];
                    if (ele.Name == "use")
                    {
                        substation sub = new substation();
                        sub.EleID = ele.ID;
                        sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        sub = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", sub);
                        if (sub == null)
                        {
                            if (MessageBox.Show("选择的图元没有对应的属性信息，是否继续复制？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                //frmSelLayer flar = new frmSelLayer();
                                //flar.list = getAfterLayer(year);
                                //if (flar.ShowDialog() == DialogResult.OK)
                                //{
                                ArrayList _slist = flar.list2;
                                for (int i = 0; i < _slist.Count; i++)
                                {
                                    XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;
                                    e1.SetAttribute("x", ele.GetAttribute("x"));
                                    e1.SetAttribute("y", ele.GetAttribute("y"));
                                    e1.GetAttribute("transform", ele.GetAttribute("transform"));
                                    e1.SetAttribute("xlink:href", ele.GetAttribute("xlink:href"));
                                    e1.SetAttribute("style", ele.GetAttribute("style"));
                                    e1.SetAttribute("CopyOf", ele.ID);
                                    e1.SetAttribute("layer", getlayer(_slist[i].ToString(), tlVectorControl1.SVGDocument.getLayerList()).ID);
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);
                                }
                                tlVectorControl1.Refresh();
                                //}
                            }
                        }
                        if (sub != null)
                        {
                            //frmSelLayer flar = new frmSelLayer();
                            //flar.list = getAfterLayer(year);
                            //if (flar.ShowDialog() == DialogResult.OK)
                            //{
                            ArrayList _slist = flar.list2;
                            for (int i = 0; i < _slist.Count; i++)
                            {
                                XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;
                                e1.SetAttribute("x", ele.GetAttribute("x"));
                                e1.SetAttribute("y", ele.GetAttribute("y"));
                                e1.GetAttribute("transform", ele.GetAttribute("transform"));
                                e1.SetAttribute("xlink:href", ele.GetAttribute("xlink:href"));
                                e1.SetAttribute("style", ele.GetAttribute("style"));
                                e1.SetAttribute("CopyOf", ele.ID);
                                e1.SetAttribute("layer", getlayer(_slist[i].ToString(), tlVectorControl1.SVGDocument.getLayerList()).ID);
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);

                                substation _sub = new substation();
                                _sub.UID = Guid.NewGuid().ToString();
                                _sub.EleID = e1.GetAttribute("id");
                                _sub.LayerID = e1.GetAttribute("layer");
                                _sub.Burthen = sub.Burthen;
                                _sub.EleName = sub.EleName;
                                _sub.glebeEleID = sub.glebeEleID;
                                _sub.Number = sub.Number;
                                _sub.ObligateField1 = sub.ObligateField1;
                                _sub.ObligateField2 = sub.ObligateField2;
                                _sub.ObligateField3 = sub.ObligateField3;
                                _sub.ObligateField4 = sub.ObligateField4;
                                _sub.Remark = sub.Remark;
                                _sub.SvgUID = sub.SvgUID;
                                Services.BaseService.Create<substation>(_sub);
                            }
                            tlVectorControl1.Refresh();
                            //}
                        }
                    }
                    if (ele.Name == "polyline")
                    {
                        LineInfo line = new LineInfo();
                        line.EleID = ele.ID;
                        line.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        line = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", line);
                        if (line == null)
                        {
                            if (MessageBox.Show("选择的图元没有对应的属性信息，是否继续复制？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                //frmSelLayer flar = new frmSelLayer();
                                //flar.list = getAfterLayer(year);
                                //if (flar.ShowDialog() == DialogResult.OK)
                                //{
                                ArrayList _slist = flar.list2;
                                for (int i = 0; i < _slist.Count; i++)
                                {
                                    XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as XmlElement;
                                    e1.SetAttribute("points", ele.GetAttribute("points"));
                                    e1.GetAttribute("transform", ele.GetAttribute("transform"));
                                    e1.SetAttribute("style", ele.GetAttribute("style"));
                                    e1.SetAttribute("CopyOf", ele.ID);
                                    e1.SetAttribute("IsLead", ele.GetAttribute("IsLead"));
                                    e1.SetAttribute("layer", getlayer(_slist[i].ToString(), tlVectorControl1.SVGDocument.getLayerList()).ID);
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);
                                }
                                tlVectorControl1.Refresh();
                                //}
                            }
                        }
                        if (line != null)
                        {
                            //frmSelLayer flar = new frmSelLayer();
                            //flar.list = getAfterLayer(year);
                            //if (flar.ShowDialog() == DialogResult.OK)
                            //{
                            ArrayList _slist = flar.list2;
                            for (int i = 0; i < _slist.Count; i++)
                            {
                                XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as XmlElement;
                                e1.SetAttribute("points", ele.GetAttribute("points"));
                                e1.GetAttribute("transform", ele.GetAttribute("transform"));
                                e1.SetAttribute("style", ele.GetAttribute("style"));
                                e1.SetAttribute("CopyOf", ele.ID);
                                e1.SetAttribute("IsLead", ele.GetAttribute("IsLead"));
                                e1.SetAttribute("layer", getlayer(_slist[i].ToString(), tlVectorControl1.SVGDocument.getLayerList()).ID);
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);

                                LineInfo _line = new LineInfo();
                                _line.UID = Guid.NewGuid().ToString();
                                _line.EleID = e1.GetAttribute("id");
                                _line.LayerID = e1.GetAttribute("layer");
                                _line.Length = line.Length;
                                _line.LineName = line.LineName;
                                _line.LineType = line.LineType;
                                _line.ObligateField1 = line.ObligateField1;
                                _line.ObligateField2 = line.ObligateField2;
                                _line.Voltage = line.Voltage;
                                _line.SvgUID = line.SvgUID;
                                Services.BaseService.Create<LineInfo>(_line);
                            }
                            tlVectorControl1.Refresh();
                            //}
                        }
                    }
                }
            }
        }
        public ArrayList getAfterLayer(int _year)
        {
            ArrayList list1 = tlVectorControl1.SVGDocument.getLayerList();
            ArrayList list2 = new ArrayList();
            for (int i = 0; i < list1.Count; i++)
            {
                Layer lar = (Layer)list1[i];
                if (lar.Label.Length > 4)
                {
                    if (IsNumber(lar.Label.Substring(0, 4)))
                    {
                        if (Convert.ToInt32(lar.Label.Substring(0, 4)) > _year)
                        {
                            list2.Add(lar);
                        }
                    }

                }
            }
            return list2;

        }
        public bool IsNumber(string num)
        {
            try
            {
                Int32.Parse(num);
                return true;
            }
            catch
            {
                bool flag = false;
                return flag;
            }

        }
        public void Linkage2()
        {
            //Layer lay1 = null;
            //int year = 0;
            //lay1 = tlVectorControl1.SVGDocument.CurrentLayer;
            //try
            //{
            //    year = Convert.ToInt32(lay1.Label.Substring(0, 4));
            //}
            //catch (Exception e1)
            //{
            //    MessageBox.Show("选择图层的图层名称不包含年份信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            string id = "";
            frmLayerGrade fgrade = new frmLayerGrade();
            fgrade.SymbolDoc = tlVectorControl1.SVGDocument;
            fgrade.InitData(SVGUID);
            if (fgrade.ShowDialog() == DialogResult.OK)
            {
                id = fgrade.GetSelectNode();
            }
            if(id.Length>4){
               string[] strid= id.Split(",".ToCharArray());
               //for (int i = 0; i < strid.Length;i++ )
               //{
               //    Services.BaseService.
               //}
            }
        }
        public void Linkage()
        {

            Layer lay1 = null;
            string stype = "";
            int year = 0;
            SortedList LineList = new SortedList();
            SortedList subList = new SortedList();

            lay1 = tlVectorControl1.SVGDocument.CurrentLayer;
            try
            {

                year = Convert.ToInt32(lay1.Label.Substring(0, 4));
                //if(lay1.Label.Contains("变电站")){
                //    stype = "变电站";
                //}
                //if (lay1.Label.Contains("线路"))
                //{
                //    stype = "线路";
                //}

                //if(stype==""){
                //      MessageBox.Show("选择的图层名称不包含线路或变电站信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //      stype = "";
                //      return;
                //}

            }
            catch (Exception e1)
            {
                MessageBox.Show("选择图层的图层名称不包含年份信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (SvgElement ele in lay1.GraphList)
            {
                if (ele.Name == "use")
                {
                    subList.Add(ele.ID, ele);
                }
                if (ele.Name == "polyline" && ele.GetAttribute("IsLead") == "1")
                {
                    LineList.Add(ele.ID, ele);
                }
            }
            foreach (Layer layer in tlVectorControl1.SVGDocument.Layers)
            {
                string str_lb = layer.Label;
                if (str_lb.Length > 4)
                {
                    if (IsNumber(str_lb.Substring(0, 4)))
                    {
                        if (Convert.ToInt32(str_lb.Substring(0, 4)) > year/* && str_lb.Contains(stype)*/)
                        {
                            foreach (SvgElement ele in layer.GraphList)
                            {
                                if (ele.Name == "use")
                                {
                                    SvgElement sub = (SvgElement)subList[ele.GetAttribute("CopyOf")];
                                    if (sub != null)
                                    {
                                        ele.SetAttribute("x", sub.GetAttribute("x"));
                                        ele.SetAttribute("y", sub.GetAttribute("y"));
                                        ele.SetAttribute("transform", sub.GetAttribute("transform"));

                                        string larid = "";
                                        larid = ((SvgElement)ele).GetAttribute("layer");
                                        if (!ChangeLayerList.Contains(larid))
                                        {
                                            ChangeLayerList.Add(larid);
                                        }
                                        substation sub1 = new substation();
                                        sub1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sub1.EleID = sub.GetAttribute("id");
                                        sub1 = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", sub1);
                                        if (sub1 != null)
                                        {
                                            substation sub2 = new substation();
                                            sub2.EleID = ele.ID;
                                            sub2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                            sub2 = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", sub2);
                                            if (sub2 != null)
                                            {
                                                string uid = sub2.UID;
                                                string eleid = sub2.EleID;
                                                sub2 = sub1;
                                                sub2.UID = uid;
                                                sub2.EleID = eleid;
                                                Services.BaseService.Update<substation>(sub2);
                                            }
                                        }
                                    }
                                }
                                if (ele.Name == "polyline" && ele.GetAttribute("IsLead") == "1")
                                {
                                    SvgElement line = (SvgElement)LineList[ele.GetAttribute("CopyOf")];
                                    if (line != null)
                                    {

                                        ((Polyline)ele).Points = ((Polyline)line).Points.Clone() as PointF[];
                                        if (line.GetAttribute("transform") != "")
                                        {
                                            ele.SetAttribute("transform", line.GetAttribute("transform"));
                                        }
                                        string larid = "";
                                        larid = ((SvgElement)ele).GetAttribute("layer");
                                        if (!ChangeLayerList.Contains(larid))
                                        {
                                            ChangeLayerList.Add(larid);
                                        }
                                        //((Polyline)ele).Transform.Matrix.TransformPoints(((Polyline)ele).Points);
                                        LineInfo line1 = new LineInfo();
                                        line1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        line1.EleID = line.GetAttribute("id");
                                        line1 = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", line1);
                                        if (line1 != null)
                                        {
                                            LineInfo line2 = new LineInfo();
                                            line2.EleID = ele.ID;
                                            line2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                            line2 = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", line2);
                                            if (line2 != null)
                                            {
                                                string uid = line2.UID;
                                                string eleid = line2.EleID;
                                                line2 = line1;
                                                line2.UID = uid;
                                                line2.EleID = eleid;
                                                Services.BaseService.Update<LineInfo>(line2);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            stype = "";
            subList.Clear();
            LineList.Clear();
            tlVectorControl1.Refresh();


        }

      

        /// <summary>
        /// 导出区域图片
        /// </summary>
        private void ExportImage()
        {
            GraphPath rt1 = tlVectorControl1.SVGDocument.CurrentElement as GraphPath;
            if (rt1 == null) return;
            RectangleF rtf1 = rt1.GetBounds();
            int width = (int)Math.Round(rtf1.Width * tlVectorControl1.ScaleRatio,0)+1;
            int height = (int)Math.Round(rtf1.Height * tlVectorControl1.ScaleRatio,0)+1;
            //if (width < 7000 && height < 7000)
            {
                System.Drawing.Image image = new Bitmap(width , height,System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
                Graphics g = Graphics.FromImage(image);

                g.SmoothingMode = SmoothingMode.HighSpeed;//.HighQuality;

                //g.CompositingQuality = CompositingQuality.HighSpeed;//.HighQuality;
                PointF ptf = new PointF((rtf1.X + rtf1.Right) / 2, (rtf1.Y + rtf1.Bottom) / 2);
                Point pt = new Point((int)ptf.X, (int)ptf.Y);
                g.Clear(Color.White);
                drawMap(g, width, height, pt);
                Matrix matrix1 = new Matrix();
                
                matrix1.Scale(tlVectorControl1.ScaleRatio,tlVectorControl1.ScaleRatio);
                matrix1.Translate(-rtf1.X, -rtf1.Y);

                g.Transform = matrix1;

                RenderTo(g);
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "图像文件(*.png)|*.png|图像文件(*.jpg)|*.jpg|图像文件(*.gif)|*.gif|Bitmap文件(*.bmp)|*.bmp|Jpeg文件(*.jpeg)|*.jpeg|所有文件(*.*)|*.*";
                //dlg.Filter = "图像文件|*.png;*.jpg;*.bmp;*.gif";
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
                }
                image.Dispose();


            }
            //if (width > 7000 || height > 7000)
            //{
            //    MessageBox.Show("超出图片限定大小，请重新选定区域。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
        /*    height = 6000;
            if (width > 7000 || height > 7000)
            {
                if ((width >= 7000) && (height < 7000))
                {
                    while (width > 7000)
                    {

                        System.Drawing.Image image = new Bitmap(7000, height);
                        Graphics g = Graphics.FromImage(image);
                        width -= 7000;
                        g.SmoothingMode = SmoothingMode.HighQuality;

                        g.CompositingQuality = CompositingQuality.HighQuality;
                        PointF ptf = new PointF((rtf1.X + rtf1.Right) / 2, (rtf1.Y + rtf1.Bottom) / 2);
                        Point pt = new Point((int)ptf.X, (int)ptf.Y);
                        g.Clear(Color.White);


                        drawMap(g, width, height, pt);
                        Matrix matrix1 = new Matrix();

                        matrix1.Scale(tlVectorControl1.ScaleRatio, tlVectorControl1.ScaleRatio);
                        matrix1.Translate(-rtf1.X, -rtf1.Y);

                        g.Transform = matrix1;

                        RenderTo(g);
                        SaveFileDialog dlg = new SaveFileDialog();
                        dlg.Filter = "图像文件|*.png;*.jpg;*.bmp;*.gif";
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
                        }
                        image.Dispose();


                    }



                }


            

            }*/
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
        void drawMap(Graphics g, int width, int height, Point center)
        {
            try
            {
                int nScale = mapview.Getlevel(tlVectorControl1.ScaleRatio);
                if (nScale == -1)
                    return;
                LongLat longlat = LongLat.Empty;
                //计算中心点经纬度

                longlat = mapview.OffSet(mapview.ZeroLongLat, mapview.Getlevel(1), center.X, center.Y);

                longlat = mapview.OffSetZero(-(int)(center.X * tlVectorControl1.ScaleRatio), -(int)(center.Y * tlVectorControl1.ScaleRatio));


                //g.Clear(ColorTranslator.FromHtml("#EBEAE8"));

                ImageAttributes imageA = new ImageAttributes();
                ColorMatrix matrix1 = new ColorMatrix();
                matrix1.Matrix00 = 1f;
                matrix1.Matrix11 = 1f;
                matrix1.Matrix22 = 1f;
                matrix1.Matrix33 = this.mapOpacity / 100f;//地图透明度
                matrix1.Matrix44 = 1f;
                //设置地图透明度

                imageA.SetColorMatrix(matrix1, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                Color color4 = ColorTranslator.FromHtml("#FFFFFF");
                int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
                string Transparent = ConfigurationSettings.AppSettings.Get("Transparent");
                if (string.IsNullOrEmpty(Transparent)) Transparent = "#EBEAE8";
                //g.Clear(color4);
                g.Clear(Color.White);
                if (chose == 1)
                {
                    Color color = ColorTranslator.FromHtml(Transparent);
                    imageA.SetColorKey(color, color);
                }
                else if (chose == 2)
                {

                    Color color = ColorTranslator.FromHtml("#F4F4FB");
                    Color color2 = ColorTranslator.FromHtml("#EFF0F1");
                    imageA.SetColorKey(color2, color);
                } 

                mapview.Paint(g, width, height, nScale, longlat.Longitude, longlat.Latitude, imageA);

                //绘制比例尺
                Point p1 = new Point(20, height - 30);
                Point p2 = new Point(20, height - 20);
                Point p3 = new Point(80, height - 20);
                Point p4 = new Point(80, height - 30);

                g.DrawLines(new Pen(Color.Black, 2), new Point[4] { p1, p2, p3, p4 });
                string str1 = string.Format("{0}公里", mapview.GetMiles(nScale));
                g.DrawString(str1, new Font("宋体", 10), Brushes.Black, 30, height - 40);

            }
            catch (Exception e1)
            {
            }

        }

        void frmlar_OnClickLayer(object sender, Layer lar)
        {
            //tlVectorControl1.SVGDocument.SelectCollection.Clear();
            ArrayList a = tlVectorControl1.SVGDocument.getLayerList();
            SvgDocument.currentLayer = lar.ID;
            //tlVectorControl1.SVGDocument.CurrentElement = null;

            string larid = lar.ID;

            if (!ChangeLayerList.Contains(larid))
            {
                ChangeLayerList.Add(larid);
            }

            if (lar.GetAttribute("layerType") == progtype)
            {
                tlVectorControl1.CanEdit = true;
                if (progtype == "地理信息层")
                {
                    DlBarVisible(true);
                }
                if (progtype == "城市规划层")
                {
                    DkBarVisible(true);
                }
                if (progtype == "电网规划层")
                {
                    DwBarVisible(true);
                }
            }
            else
            {
                tlVectorControl1.CanEdit = false;
                if (progtype == "地理信息层")
                {
                    DlBarVisible(false);
                }
                if (progtype == "城市规划层")
                {
                    DkBarVisible(false);
                }
                if (progtype == "电网规划层")
                {
                    DwBarVisible(false);
                }
            }
            if (lar.GetAttribute("layerType") == "所内接线图")
            {
                JxtBar();
                return;
            }
        }
        public void InitTK1()
        {
            XmlElement root = tlVectorControl1.SVGDocument.RootElement;
            LineInfo line = new LineInfo();
            line.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList<LineInfo> list = Services.BaseService.GetList<LineInfo>("SelectLineInfoBySvgIDAll", line);
            for (int n = 0; n < list.Count; n++)
            {

            }
        }
        public void InitTK()
        {
            XmlElement root = tlVectorControl1.SVGDocument.RootElement;

            IList<glebeType> list = Services.BaseService.GetStrongList<glebeType>();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].ObjColor = Color.FromArgb(Convert.ToInt32(list[i].ObligateField1));
            }
            string width = root.GetAttribute("width");
            string height = root.GetAttribute("height");
            height = Convert.ToString(Convert.ToDecimal(height) + 50);

            decimal dec_width = Convert.ToDecimal(width);
            decimal dec_height = Convert.ToDecimal(height);
            root.SetAttribute("height", height);
            int res = 0;
            int rem = 0;
            int offx = 50;
            int offy = 100;

            for (int n = 0; n < list.Count; n++)
            {

                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("rect") as RectangleElement;
                XmlElement t1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                t1.InnerText = list[n].TypeStyle;
                t1.SetAttribute("layer", SvgDocument.currentLayer);

                rem = Math.DivRem(n, 6, out res);
                if (rem < 1)
                {
                    n1.SetAttribute("x", Convert.ToString(offx));
                    n1.SetAttribute("y", Convert.ToString(dec_height + 100));
                    t1.SetAttribute("x", Convert.ToString(offx + 220));
                    t1.SetAttribute("y", Convert.ToString(dec_height + 100 + 35));
                    t1.SetAttribute("font-size", "30");
                }
                else
                {
                    if (res == 0) { offx = 50; }
                    n1.SetAttribute("x", Convert.ToString(offx));
                    n1.SetAttribute("y", Convert.ToString(dec_height + (rem + 1) * offy));
                    t1.SetAttribute("x", Convert.ToString(offx + 220));
                    t1.SetAttribute("y", Convert.ToString(dec_height + (rem + 1) * offy + 35));
                    t1.SetAttribute("font-size", "30");

                }
                n1.SetAttribute("width", "200");
                n1.SetAttribute("height", "70");

                string sty = "fill:" + ColorTranslator.ToHtml(list[n].ObjColor) + ";fill-opacity:1;stroke:#000000;stroke-opacity:1;";
                n1.SetAttribute("style", sty);
                n1.SetAttribute("layer", SvgDocument.currentLayer);
                tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                tlVectorControl1.SVGDocument.RootElement.AppendChild(t1);
                offx = offx + 400;
            }
            SvgElement tk = tlVectorControl1.SVGDocument.CreateElement("rect") as RectangleElement;
            tk.SetAttribute("x", "0");
            tk.SetAttribute("y", "0");
            tk.SetAttribute("width", "4200");
            tk.SetAttribute("height", "3000");
            tk.SetAttribute("style", "fill:none;fill-opacity:1;stroke:#000000;stroke-opacity:1;stroke-width:3;");
            tk.SetAttribute("layer", SvgDocument.currentLayer);
            tlVectorControl1.SVGDocument.RootElement.AppendChild(tk);
            tlVectorControl1.SVGDocument.SelectCollection.Clear();
            tlVectorControl1.SVGDocument.CurrentElement = tk;
            tlVectorControl1.ChangeLevel(LevelType.Bottom);
            root.SetAttribute("height", "3000");

            SvgElement ntk = tlVectorControl1.SVGDocument.CreateElement("rect") as RectangleElement;
            ntk.SetAttribute("x", "0");
            ntk.SetAttribute("y", "0");
            ntk.SetAttribute("width", "4200");
            ntk.SetAttribute("height", "2500");
            ntk.SetAttribute("style", "fill:none;fill-opacity:1;stroke:#000000;stroke-opacity:1;stroke-width:1;");
            ntk.SetAttribute("layer", SvgDocument.currentLayer);
            tlVectorControl1.SVGDocument.RootElement.AppendChild(ntk);
            tlVectorControl1.SVGDocument.SelectCollection.Clear();
            tlVectorControl1.SVGDocument.CurrentElement = ntk;
            tlVectorControl1.ChangeLevel(LevelType.Bottom);
            tlVectorControl1.Refresh();
            tlVectorControl1.SVGDocument.SelectCollection.Clear();

            XmlElement t01 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
            t01.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.65));
            t01.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + 500 * 0.2));
            t01.InnerText = "规划设计";
            t01.SetAttribute("font-size", "30");
            tlVectorControl1.SVGDocument.RootElement.AppendChild(t01);

            XmlElement t02 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
            t02.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.65));
            t02.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + 500 * 0.4));
            t02.InnerText = "哈尔滨通力软件有限公司";
            t02.SetAttribute("font-size", "30");
            tlVectorControl1.SVGDocument.RootElement.AppendChild(t02);

            XmlElement t03 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
            t03.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.65));
            t03.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + +500 * 0.6));
            t03.InnerText = "委托单位";
            t03.SetAttribute("font-size", "30");
            tlVectorControl1.SVGDocument.RootElement.AppendChild(t03);

            XmlElement t04 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
            t04.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.65));
            t04.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + 500 * 0.8));
            t04.InnerText = "伊 春 电 业 局";
            t04.SetAttribute("font-size", "30");
            tlVectorControl1.SVGDocument.RootElement.AppendChild(t04);

            XmlElement t05 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
            t05.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.8));
            t05.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + 500 * 0.1));
            t05.InnerText = "工程名称     伊春城区“十一五”中压配电网发展规划";
            t05.SetAttribute("font-size", "30");
            tlVectorControl1.SVGDocument.RootElement.AppendChild(t05);

            XmlElement t06 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
            t06.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.8));
            t06.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + 500 * 0.25));
            t06.InnerText = "图名     伊春市负荷密度主题";
            t06.SetAttribute("font-size", "30");
            tlVectorControl1.SVGDocument.RootElement.AppendChild(t06);



        }

        public void Fhbz()
        {
            XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("//*[@IsArea=\"1\"]");

            Layer oldLar = getlayer("负荷标注", tlVectorControl1.SVGDocument.getLayerList());
            if (oldLar != null)
            {
                XmlNodeList oldList = tlVectorControl1.SVGDocument.SelectNodes("//*[@layer=\"" + oldLar.ID + "\"]");
                for (int i = 0; i < oldList.Count; i++)
                {
                    tlVectorControl1.SVGDocument.RootElement.RemoveChild((SvgElement)oldList[i]);
                }
                tlVectorControl1.SVGDocument.GetElementsByTagName("defs")[0].RemoveChild(oldLar);
                //tlVectorControl1.SVGDocument.RootElement.RemoveChild(oldLar);
            }


            Layer zjLar = Layer.CreateNew("负荷标注", tlVectorControl1.SVGDocument);
            zjLar.SetAttribute("layerType", "城市规划层");
            for (int i = 0; i < list.Count; i++)
            {
                IGraph graph1 = (IGraph)list[i];
                RectangleF rect = graph1.GetBounds();
                glebeProperty gle = new glebeProperty();
                gle.EleID = graph1.ID;
                gle.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                gle = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", gle);

                if (gle != null)
                {
                    XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                    //Point point1 = tlVectorControl1.PointToView(new Point(e.Mouse.X, e.Mouse.Y));
                    n1.SetAttribute("x", Convert.ToString(rect.X + rect.Width / 2));
                    n1.SetAttribute("y", Convert.ToString(rect.Y + rect.Height / 2));
                    n1.SetAttribute("font-famliy", "宋体");
                    n1.SetAttribute("font-size", "14");
                    n1.InnerText = gle.Burthen.ToString();
                    n1.SetAttribute("layer", zjLar.ID);
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                }
            }
            frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
            frmlar.InitData();
            tlVectorControl1.Refresh();

        }

        private void dotNetBarManager1_PopupContainerLoad(object sender, EventArgs e)
        {

        }

        private void dotNetBarManager1_ContainerLoadControl(object sender, EventArgs e)
        {
            BaseItem item = sender as BaseItem;
            DockContainerItem dockitem = null;
            if (item == null)
                return;
            if (item.Name == "DockContainerty")
            {
                dockitem = item as DockContainerItem;
                this.symbolSelector = new ItopVector.Selector.SymbolSelector(Application.StartupPath + "\\symbol\\symbol2.xml");
                this.symbolSelector.Dock = DockStyle.Fill;
                dockitem.Control = this.symbolSelector;
                tlVectorControl1.SymbolSelector = this.symbolSelector;
            }
            if (item.Name == "DockContainersx")
            {
                dockitem = item as DockContainerItem;
                dockitem.Control = this.propertyGrid;
            }
            if (item.Name == "DockContainerwj")
            {
                dockitem = item as DockContainerItem;
                ctlfile.Dock = DockStyle.Fill;
                dockitem.Control = ctlfile;
                ctlfile.OnOpenSvgDocument += new OnOpenDocumenthandler(ctlfile_OnOpenSvgDocument);
            }
        }
        //public void Open(SvgDocument svg)
        //{
        //    ctlfile_OnOpenSvgDocument(null, svg);
        //}
        public void Open(string _SvgUID, string pid)
        {
            ParentUID = pid;
            //MapType = "所内接线图";
            //Open(_SvgUID);
            if (progtype == "电网规划层")
            {
                Open(_SvgUID);
            }
            else
            {
                Open2(_SvgUID);
            }
            this.tlVectorControl1.Size = new Size((Screen.PrimaryScreen.WorkingArea.Height - 258), (Screen.PrimaryScreen.WorkingArea.Width - 176));
        }

        public void LayerManagerShow()
        {
            frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
            if (MapType == "所内接线图")
            {
                frmlar.Progtype = MapType;
            }
            else
            {
                frmlar.Progtype = progtype;
            }
            frmlar.Owner = this;
            frmlar.OnClickLayer += new OnClickLayerhandler(frmlar_OnClickLayer);
            frmlar.OnDeleteLayer += new OnDeleteLayerhandler(frmlar_OnDeleteLayer);
            // Init(progtype);
            frmlar.ShowInTaskbar = false;
            //frmlar.Top = 25;//Screen.PrimaryScreen.WorkingArea.Height - 500;
            //frmlar.Left = Screen.PrimaryScreen.WorkingArea.Width - frmlar.Width;
            frmlar.Show();
        }

        void frmlar_OnDeleteLayer(object sender)
        {
            SvgDocument.currentLayer = "";
            SetBarEnabed(false);
        }
       
        public void Open(string _SvgUID)
        {
            try
            {

                if (_SvgUID.Length < 20)
                {
                    JxtBar();
                    tlVectorControl1.ContextMenuStrip = null;
                }

                StringBuilder txt = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><svg id=\"svg\" width=\"1500\" height=\"1000\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:itop=\"http://www.Itop.com/itop\" transform=\"matrix(1 0 0 1 0 1)\"><defs>");
                string svgdefs = "";
                string layertxt = "";
                StringBuilder content = new StringBuilder();

                if (string.IsNullOrEmpty(_SvgUID)) return;
                SVG_LAYER lar = new SVG_LAYER();
                lar.svgID = _SvgUID;
                lar.YearID = yearID;
                IList<SVG_LAYER> larlist = Services.BaseService.GetList<SVG_LAYER>("SelectSVG_LAYERByYearID", lar);
                foreach (SVG_LAYER _lar in larlist)
                {
                    layertxt = layertxt + "<layer id=\"" + _lar.SUID + "\" label=\"" + _lar.NAME + "\" layerType=\"" + _lar.layerType + "\" visibility=\"" + _lar.visibility + "\" ParentID=\"" + _lar.YearID + "\" IsSelect=\"" + _lar.IsSelect + "\" />";
                    content.Append(_lar.XML);
                }
                txt.Append(layertxt);

                SVG_SYMBOL sym = new SVG_SYMBOL();
                sym.svgID = _SvgUID;
                IList<SVG_SYMBOL> symlist = Services.BaseService.GetList<SVG_SYMBOL>("SelectSVG_SYMBOLBySvgID", sym);
                foreach (SVG_SYMBOL _sym in symlist)
                {
                    svgdefs = svgdefs + _sym.XML;
                }
                txt.Append(svgdefs + "</defs>");
                txt.Append(content.ToString() + "</svg>");
                SvgDocument document = new SvgDocument();
                document.LoadXml(txt.ToString());
                document.FileName =SvgName;
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
                //tlVectorControl1.ForeColor = Color.White;
                CreateComboBox();
                xltProcessor = new XLTProcessor(tlVectorControl1);
                xltProcessor.MapView = mapview;
                xltProcessor.OnNewLine += new NewLineDelegate(xltProcessor_OnNewLine);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //tlVectorControl1.SVGDocument.SvgdataUid = "";
        } 
     
        
     /*  public void Open(string _SvgUID)
        {
            try
            {

                if (_SvgUID.Length < 20)
                {
                    JxtBar();
                    tlVectorControl1.ContextMenuStrip = null;
                }
                SVGFILE svgFile = new SVGFILE();
                svgFile.SUID = _SvgUID;
                SvgDocument document = CtrlSvgView.CashSvgDocument;
                if (document == null)
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

                img = document.SelectSingleNode("//*[@TLGH=\"1\"]");
                if (img != null)
                {
                    ((XmlElement)img).SetAttribute("xlink:href", " ");
                }
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
                //tlVectorControl1.ForeColor = Color.White;
                CreateComboBox();
                xltProcessor = new XLTProcessor(tlVectorControl1);
                xltProcessor.MapView = mapview;
                xltProcessor.OnNewLine += new NewLineDelegate(xltProcessor_OnNewLine);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }*/
        public void Open2(string _SvgUID)
        {

            StringBuilder txt = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><svg id=\"svg\" width=\"1500\" height=\"1000\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:itop=\"http://www.Itop.com/itop\" transform=\"matrix(1 0 0 1 0 1)\"><defs>");
            string svgdefs = "";
            string layertxt = "";
            StringBuilder content = new StringBuilder();
            string where = "";
            if (string.IsNullOrEmpty(_SvgUID)) return;
            try
            {
                if (progtype == "城市规划层")
                {
                    where = " (layerType = '城市规划层') OR (layerType = '地理信息层') ";
                }
                else
                {
                    where = " (layerType = '地理信息层') ";
                }
                SVGFILE svgFile = new SVGFILE();
                svgFile.SUID = _SvgUID;
                svgFile = (SVGFILE)Services.BaseService.GetObject("SelectSVGFILEByKey", svgFile);
                //SvgDocument document = CashSvgDocument;
                //if (document == null) {
                SVG_LAYER lar = new SVG_LAYER();
                lar.svgID = _SvgUID;
                lar.YearID = where;
                IList<SVG_LAYER> larlist = Services.BaseService.GetList<SVG_LAYER>("SelectSVG_LAYERByWhere", lar);
                foreach (SVG_LAYER _lar in larlist)
                {
                    layertxt = layertxt + "<layer id=\"" + _lar.SUID + "\" label=\"" + _lar.NAME + "\" layerType=\"" + _lar.layerType + "\" visibility=\"" + _lar.visibility + "\" ParentID=\"" + _lar.YearID + "\" IsSelect=\"" + _lar.IsSelect + "\" />";
                    content.Append(_lar.XML);
                }
                txt.Append(layertxt);


                SVG_SYMBOL sym = new SVG_SYMBOL();
                sym.svgID = _SvgUID;
                IList<SVG_SYMBOL> symlist = Services.BaseService.GetList<SVG_SYMBOL>("SelectSVG_SYMBOLBySvgID", sym);
                foreach (SVG_SYMBOL _sym in symlist)
                {
                    svgdefs = svgdefs + _sym.XML;
                }

                txt.Append(svgdefs + "</defs>");
                txt.Append(content.ToString() + "</svg>");

                SvgDocument document = new SvgDocument();
                document.LoadXml(txt.ToString());
                document.FileName = SvgName;
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
                tlVectorControl1.SVGDocument.SvgdataUid = _SvgUID;
                tlVectorControl1.SVGDocument.FileName = this.Text;
                tlVectorControl1.DocumentbgColor = Color.White;
                tlVectorControl1.BackColor = Color.White;
                //tlVectorControl1.ForeColor = Color.White;
                CreateComboBox();
                xltProcessor = new XLTProcessor(tlVectorControl1);
                xltProcessor.MapView = mapview;
                xltProcessor.OnNewLine += new NewLineDelegate(xltProcessor_OnNewLine);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void InitGroup()
        {
            // Create a resolver with default credentials.
            //XmlUrlResolver resolver = new XmlUrlResolver();
            //resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;

            //// Set the reader settings object to use the resolver.
            //settings.XmlResolver = resolver;

            //// Create the XmlReader object.
            //XmlReader reader = XmlReader.Create(Application.StartupPath + "\\symbol\\symbol4.xml", settings);

            //System.IO.StreamReader sr = new System.IO.StreamReader(Application.StartupPath + "\\symbol\\symbol4.xml", Encoding.Default);
            //xdoc.Load(sr);
            //sr.Close();
            //XmlNodeList nlist= doc.GetElementsByTagName("group");
            //XmlNode nnode = nlist[0];
        }
        public void OpenJXT(IList svglist, SVGFILE svg)
        {
            if (string.IsNullOrEmpty(svg.FILENAME)) return;
            frmMain2 frm = new frmMain2(svglist, svg.SUID);
            frm.Owner = this.ParentForm;
            frm.Show();
            frm.LoadShape("symbol4.xml");
            frm.OpenJXT(svglist, svg);
            //SVGFILE svg_temp = new SVGFILE();
            //if (svglist.Count > 0)
            //{
            //    svg_temp = (SVGFILE)svglist[0];
            //    sdoc = null;
            //    sdoc = new SvgDocument();
            //    //tlVectorControl1.NewFile();
            //    sdoc.LoadXml(svg_temp.SVGDATA);
            //    tlVectorControl1.SVGDocument = sdoc;
            //    tlVectorControl1.SVGDocument.SvgdataUid = svg_temp.SUID;

            //    MapType = "所内接线图";
            //}
            //else
            //{
            //    tlVectorControl1.NewFile();
            //    tlVectorControl1.SVGDocument.SvgdataUid = eleid;
            //    //InitGroup();
            //    MapType = "所内接线图";
            //}
            //substation _s = new substation();
            //_s.EleID = eleid;
            //_s.SvgUID = ParentUID;
            //substation _s1 = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _s);
            //if (_s1 != null)
            //{
            //    tlVectorControl1.SVGDocument.FileName = _s1.EleName + "主接线图";
            //    this.Text = _s1.EleName + "主接线图";
            //}
            //ArrayList a = tlVectorControl1.SVGDocument.getLayerList();
            //if (tlVectorControl1.SVGDocument.getLayerList().Count == 0)
            //{
            //    Layer _lar = ItopVector.Core.Figure.Layer.CreateNew("接线图", tlVectorControl1.SVGDocument);
            //    _lar.SetAttribute("layerType", "所内接线图");
            //    _lar.Visible = true;
            //    SvgDocument.currentLayer = ((Layer)tlVectorControl1.SVGDocument.getLayerList()[0]).ID;
            //}
            //tlVectorControl1.ContextMenuStrip = null;
            ////CreateComboBox();
            //InitJXT();
            //LoadShape("symbol4.xml");
            //LoadImage = false;
            //bk1.Enabled = false;
            //tlVectorControl1.ScaleRatio = 1F;
            //tlVectorControl1.Refresh();
            //ButtonEnb(false);
        }
        public void InitJXT()
        {
            ArrayList layerlist = tlVectorControl1.SVGDocument.getLayerList();
            DevExpress.XtraEditors.Controls.CheckedListBoxItem[] chkItems = null;
            this.checkedListBoxControl1.Items.Clear();
            chkItems = new DevExpress.XtraEditors.Controls.CheckedListBoxItem[layerlist.Count];
            for (int j = 0; j < layerlist.Count; j++)
            {
                chkItems.SetValue(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(((Layer)layerlist[j]).Label), j);
                if (((Layer)layerlist[j]).Visible)
                {
                    chkItems[j].CheckState = CheckState.Checked;
                }
            }
            this.checkedListBoxControl1.Items.AddRange(chkItems);

            if (layerlist.Count > 0)
            {
                Layer lar = (Layer)layerlist[0];
                SvgDocument.currentLayer = lar.ID;
                popupContainerEdit1.Text = lar.Label;
                selLar = lar.Label;
            }
            this.popupContainerEdit1.Properties.PopupControl = this.popupContainerControl1;

        }
        public void Init(string stype)
        {
            ArrayList layerlist = tlVectorControl1.SVGDocument.getLayerList();
            ArrayList tmplaylist = new ArrayList();
            //DevExpress.XtraEditors.Controls.CheckedListBoxItem[] chkItems = null;
            //this.checkedListBoxControl1.Items.Clear();

            if (progtype == "地理信息层")
            {
                this.Text = "地理信息";
                for (int i = 0; i < layerlist.Count; i++)
                {
                    Layer lar = (Layer)layerlist[i];
                    if (lar.GetAttribute("layerType") == stype)
                    {
                        tmplaylist.Add(layerlist[i]);
                    }
                    else
                    {
                        lar.Visible = false;
                    }
                }
                if (tmplaylist.Count > 0)
                {
                    DlBarVisible(true);
                }
                else
                {
                    DlBarVisible(false);
                }
                string title = " ";
                string t = "";
                getProjName(MIS.ProgUID, ref title);

                this.Text = title + " " + t + " 地理信息";
            }
            if (progtype == "城市规划层")
            {
                this.Text = "城市规划";
                for (int i = 0; i < layerlist.Count; i++)
                {
                    Layer lar = (Layer)layerlist[i];
                    if (lar.GetAttribute("layerType") == "城市规划层" || lar.GetAttribute("layerType") == "地理信息层")
                    {
                        tmplaylist.Add(layerlist[i]);
                    }
                    else
                    {
                        lar.Visible = false;
                    }
                }
                if (layerlist.Count > 0)
                {
                    Layer _nLayer = (Layer)layerlist[0];
                    if (_nLayer.GetAttribute("layerType") == progtype)
                    {
                        DkBarVisible(true);
                    }
                    else
                    {
                        DkBarVisible(false);
                    }

                }
                else
                {
                    DkBarVisible(false);
                }
            }
            if (progtype == "电网规划层")
            {
                string title = " ";
                string t = "";
                getProjName(MIS.ProgUID,ref title);

                string[] str=yearID.Split(",".ToCharArray());
                if (str.Length > 1)
                {
                    for (int i = 1; i < str.Length; i++)
                    {
                        LayerGrade ll=Services.BaseService.GetOneByKey<LayerGrade>(str[i].Replace("'",""));
                        if(ll!=null){
                            if (ll.ParentID!="SUID")
                            {
                                LayerGrade ll2 = Services.BaseService.GetOneByKey<LayerGrade>(ll.ParentID);
                                t = t + ll2.Name + " " + ll.Name+"， ";
                            }
                        }
                    }
                }

                this.Text =title+" "+t+" 电网规划";
                for (int i = 0; i < layerlist.Count; i++)
                {
                    Layer lar = (Layer)layerlist[i];
                    tmplaylist.Add(layerlist[i]);
                }
                DwBarVisible(false);
                //tlVectorControl1.CanEdit = false;
            }

            //chkItems = new DevExpress.XtraEditors.Controls.CheckedListBoxItem[tmplaylist.Count];
            //for (int j = 0; j < tmplaylist.Count; j++)
            //{
            //    chkItems.SetValue(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(((Layer)tmplaylist[j]).Label), j);
            //    if (((Layer)tmplaylist[j]).Visible)
            //    {
            //        chkItems[j].CheckState = CheckState.Checked;
            //    }
            //}
            //this.checkedListBoxControl1.Items.AddRange(chkItems);

            //if (tmplaylist.Count > 0)
            //{
            //    Layer lar = (Layer)tmplaylist[0];
            //    SvgDocument.currentLayer = lar.ID;
            //    popupContainerEdit1.Text = lar.Label;
            //    //selLar = lar.Label;
            //}
            //this.popupContainerEdit1.Properties.PopupControl = this.popupContainerControl1;
            if (MapType == "所内接线图")
            {
                JxtBar();
                LoadImage = false;
                bk1.Visible = false;
            }
        }
        public void getProjName(string uid,ref string title)
        {
            Project sm = Services.BaseService.GetOneByKey<Project>(uid);
            if(sm!=null){
                title =sm.ProjectName+" "+ title;
                if (sm.ProjectManager == sm.UID) { return; }
                getProjName(sm.ProjectManager,ref title);
            }
            //return title;
        }
        void ctlfile_OnOpenSvgDocument(object sender, string _svgUid)
        {
            Open(_svgUid);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            //System.Environment.
            //InitData();
            CreateComboBox();
            AddCombolScale();
            this.alignButton = this.dotNetBarManager1.GetItem("mAlign") as ButtonItem;
            this.orderButton = this.dotNetBarManager1.GetItem("mOrder") as ButtonItem;
            this.rotateButton = this.dotNetBarManager1.GetItem("mRotate") as ButtonItem;

            ButtonItem b7 = dotNetBarManager1.GetItem("mEdit") as ButtonItem;
            b7.Enabled = false;

            //tabStrip1.MdiForm = this;
            string[] strn = yearID.Replace("'","").Split(",".ToCharArray());
          
            for (int i = 0; i < strn.Length; i++)
            {
                if (strn[i] != "")
                {
                    SaveID.Add(strn[i]);
                }
            }
            
            frmLayerManager.ilist = SaveID;
        }
        string xltProcessor_OnNewLine(List<string> existLineCode)
        {

            return DateTime.Now.Minute.ToString();
        }
        public void InitScaleRatio()
        {
            int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
            //if (chose == 1)
            //{
               
            //    tlVectorControl1.ScaleRatio = 0.1f;
            //    this.scaleBox.Text = "10%";
            //}
            if (chose == 2)
            {
                tlVectorControl1.ScaleRatio = 0.03125f;
                this.scaleBox.Text = "3.125%";
            }
        }
        public void resetScale()
        {
            Layer layer = frmlar.checkedListBox1.Items[0] as Layer;
            layer.Visible = true;
            tlVectorControl1.ScaleRatio = 0.04f;
            this.scaleBox.Text = "4%";
        }
        void ComboBoxEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            Layer lar = (Layer)LayerBox.ComboBoxEx.SelectedItem;

            SvgDocument.currentLayer = lar.ID;
            lar.Visible = true;
            tlVectorControl1.SVGDocument.ClearSelects();
        }
        void ComboBoxScaleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text1 = this.scaleBox.SelectedItem.ToString();
            tlVectorControl1.ScaleRatio = ItopVector.Core.Func.Number.ParseFloatStr(text1);

        }
        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion



        #region 公用方法
        public void CreateComboBox()
        {
            ArrayList Layerlist = tlVectorControl1.SVGDocument.getLayerList();
            ArrayList tmplaylist = new ArrayList();
            DevExpress.XtraEditors.Controls.CheckedListBoxItem[] chkItems = null;
            this.checkedListBoxControl1.Items.Clear();

            for (int i = 0; i < Layerlist.Count; i++)
            {
                Layer lar = (Layer)Layerlist[i];
                if (lar.GetAttribute("layerType") == progtype || lar.GetAttribute("layerType") == MapType)
                {
                    tmplaylist.Add(Layerlist[i]);
                }
                else
                {
                    lar.Visible = false;
                }
            }
            chkItems = new DevExpress.XtraEditors.Controls.CheckedListBoxItem[tmplaylist.Count];
            for (int i = 0; i < tmplaylist.Count; i++)
            {
                chkItems.SetValue(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(((Layer)tmplaylist[i]).Label), i);
                if (((Layer)tmplaylist[i]).Visible)
                {
                    chkItems[i].CheckState = CheckState.Checked;
                }
            }
            if (tmplaylist.Count > 0)
            {
                Layer lar1 = (Layer)tmplaylist[0];
                SvgDocument.currentLayer = lar1.ID;
                popupContainerEdit1.Text = lar1.Label;
                selLar = lar1.Label;
            }

            this.checkedListBoxControl1.Items.AddRange(chkItems);
            this.checkedListBoxControl1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.popupContainerEdit1.Properties.PopupControl = this.popupContainerControl1;

            //LayerBox = dotNetBarManager1.GetItem("ComLayer") as DevComponents.DotNetBar.ComboBoxItem;


            //if (LayerBox != null)
            //{
            //    LayerBox.ComboBoxEx.Items.Clear();
            //    ArrayList Layerlist = tlVectorControl1.SVGDocument.getLayerList();

            //    LayerBox.ComboBoxEx.Items.Clear();

            //    for (int i = 0; i < Layerlist.Count; i++)
            //    {
            //        LayerBox.Items.Add(Layerlist[i]);
            //    }
            //    if (Layerlist.Count > 0)
            //    {
            //        LayerBox.ComboBoxEx.SelectedItem = Layerlist[0];
            //        selLar = ((Layer)Layerlist[0]).Label;
            //    }
            //    LayerCount = Layerlist.Count;
            //    LayerBox.ComboBoxEx.SelectedIndexChanged += new EventHandler(ComboBoxEx_SelectedIndexChanged);
            //    LayerBox.GotFocus += new EventHandler(LayerBox_GotFocus);
            //}

        }

        void LayerBox_GotFocus(object sender, EventArgs e)
        {
            if (LayerCount != tlVectorControl1.SVGDocument.getLayerList().Count)
            {
                LayerBox = dotNetBarManager1.GetItem("ComLayer") as DevComponents.DotNetBar.ComboBoxItem;
                if (LayerBox != null)
                {
                    LayerBox.ComboBoxEx.Items.Clear();
                    ArrayList Layerlist = tlVectorControl1.SVGDocument.getLayerList();

                    LayerBox.ComboBoxEx.Items.Clear();


                    for (int i = 0; i < Layerlist.Count; i++)
                    {
                        LayerBox.Items.Add(Layerlist[i]);
                    }
                    if (Layerlist.Count > 0)
                    {
                        LayerBox.ComboBoxEx.SelectedItem = Layerlist[0];
                    }
                    LayerCount = tlVectorControl1.SVGDocument.getLayerList().Count;
                }
            }
        }
        private void AddCombolScale()
        {
            //缩放大小
            scaleBox = this.dotNetBarManager1.GetItem("ScaleBox") as DevComponents.DotNetBar.ComboBoxItem;
            if (scaleBox != null)
            {
                scaleBox.Items.AddRange(mapview.ScaleRange());
                scaleBox.ComboBoxEx.Text = "100%";
                scaleBox.ComboBoxEx.SelectedIndexChanged += new EventHandler(ComboBoxScaleBox_SelectedIndexChanged);

            }
        }

        public void LoadShape(string filename)
        {
            DockContainerItem dockitem = dotNetBarManager1.GetItem("DockContainerty") as DockContainerItem;
            symbolSelector = null;
            this.symbolSelector = new ItopVector.Selector.SymbolSelector(Application.StartupPath + "\\symbol\\" + filename);
            this.symbolSelector.Dock = DockStyle.Fill;
            tlVectorControl1.SymbolSelector = this.symbolSelector;
            dockitem.Control = this.symbolSelector;
            dockitem.Refresh();
            if (getlayer(selLar, tlVectorControl1.SVGDocument.getLayerList()) == null || getlayer(selLar, tlVectorControl1.SVGDocument.getLayerList()).GetAttribute("layerType") != progtype)
            {
                symbolSelector.Enabled = false;
            }
            if (selLar == "接线图")
            {
                symbolSelector.Enabled = true;
            }
            if (progtype == "电网规划层")
            {
                symbolSelector.Enabled = false;
            }
            symbolSelector.SelectedChanged += new EventHandler(symbolSelector_SelectedChanged);
            tlVectorControl1.Location = new Point(176, 90);

            tlVectorControl1.Size = new Size((Screen.PrimaryScreen.Bounds.Width - 176), (Screen.PrimaryScreen.Bounds.Height - 158));


        }
        public void InitShape()
        {
            if (progtype == "地理信息层")
            {
                //LoadShape("symbol.xml");
                LoadShape("symbol_1.xml");
            }
            if (progtype == "城市规划层")
            {
                LoadShape("symbol_2.xml");
            }
            if (progtype == "电网规划层")
            {
                LoadShape("symbol_3.xml");
            }
            if (MapType == "所内接线图")
            {
                LoadShape("symbol4.xml");
            }
        }
        public void SaveAllLayer()
        {
            
            SaveAllItem();
            string txt = "";
            //Image

            XmlNodeList imglist = tlVectorControl1.SVGDocument.SelectNodes("svg/image");
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
                    svg_img.svgID = tlVectorControl1.SVGDocument.SvgdataUid;
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
            }
            string tems_id="";
            string[] stryearid = yearID.Split(",".ToCharArray());
            if (stryearid.Length > 1)
            {
                tems_id = stryearid[1].Replace("'","");
                tems_id = tems_id.Replace("\"", "");
            }
            //layer
            ArrayList list = tlVectorControl1.SVGDocument.getLayerList();
            for (int i = 0; i < list.Count; i++)
            {
                txt = "";
                Layer lar = list[i] as Layer;
                bool IsSave = false;

                for (int m = 0; m < SaveID.Count;m++ )
                {
                    if (lar.GetAttribute("ParentID") == SaveID[m].ToString())
                    {
                        IsSave = true;
                    }
                }
                //for (int j = 0; j<frmlar.NoSave.Count; j++)
                //{
                //    if (lar.ID == ((Layer)frmlar.NoSave[j]).ID)
                //    {
                //        IsSave = false;
                //    }
                //}
                //for (int j = 0; j < ChangeLayerList.Count; j++)
                {
                    //if (lar.ID == ChangeLayerList[j].ToString() && IsSave)
                    if (IsSave) 
                    {
                        XmlNodeList nn1 = tlVectorControl1.SVGDocument.SelectNodes("svg/* [@layer='" + lar.ID + "']");
                        foreach (XmlNode node in nn1)
                        {
                            txt = txt + node.OuterXml;
                        }
                        SVG_LAYER _svg = new SVG_LAYER();
                        _svg.SUID = lar.ID;
                        _svg.svgID = SVGUID;
                        _svg = (SVG_LAYER)Services.BaseService.GetObject("SelectSVG_LAYERByKey", _svg);
                        int ny=0;
                        try { ny = int.Parse(lar.Label.Substring(0, 4)); } catch { }
                        if (_svg != null)
                        {
                            _svg.XML = txt;
                            _svg.NAME = lar.Label;
                            _svg.MDATE = System.DateTime.Now;
                            _svg.OrderID = ny*100+list.IndexOf(lar);
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
                            _svg.svgID = SVGUID;
                            _svg.XML = txt;
                            _svg.MDATE = System.DateTime.Now;
                            _svg.OrderID = ny*100 + list.IndexOf(lar);
                            _svg.YearID = tems_id;// lar.GetAttribute("ParentID");
                            _svg.IsChange = lar.GetAttribute("IsChange");
                            _svg.visibility = lar.GetAttribute("visibility");
                            _svg.layerType = lar.GetAttribute("layerType");
                            _svg.IsSelect = lar.GetAttribute("IsSelect");
                            Services.BaseService.Create<SVG_LAYER>(_svg);
                        }
                        continue;
                    }
                }
            }
            //symbol
            XmlNodeList symlist = tlVectorControl1.SVGDocument.SelectNodes("svg/defs/symbol");
            foreach (XmlNode node in symlist)
            {
                SVG_SYMBOL _sym = new SVG_SYMBOL();
                _sym.EleID = ((XmlElement)node).GetAttribute("id");
                _sym.svgID = SVGUID;
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
                    _sym.svgID = SVGUID;
                    _sym.XML = node.OuterXml;
                    _sym.MDATE = System.DateTime.Now;
                    Services.BaseService.Create<SVG_SYMBOL>(_sym);
                }
            }           
        }
        public void SaveAllItem()
        {
            int ck = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("SaveAllItem"));
            if (ck == 0)
            {
                SVG_ENTITY ent = new SVG_ENTITY();
                Services.BaseService.Update("DeleteSVG_ENTITYAll", ent);
                XmlNodeList linelist = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1']");
                for (int i = 0; i < linelist.Count;i++ )
                {
                    LineInfo _line = new LineInfo();
                    _line.EleID = ((SvgElement)linelist[i]).ID;
                    _line.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    _line=(LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID",_line);
                    if (_line != null)
                    {
                        SVG_ENTITY s_ent = new SVG_ENTITY();
                        s_ent.EleID = _line.EleID;
                        s_ent.NAME = _line.LineName;
                        s_ent.SUID = Guid.NewGuid().ToString();
                        s_ent.svgID = _line.SvgUID;
                        s_ent.MDATE = System.DateTime.Now;
                        if (_line.Voltage != "")
                        {
                            s_ent.voltage = Convert.ToInt32(_line.Voltage);
                        }
                        s_ent.TYPE = "line";
                        s_ent.layerID = ((SvgElement)linelist[i]).GetAttribute("layer");
                        Services.BaseService.Create<SVG_ENTITY>(s_ent);
                    }
                }
                XmlNodeList sublist = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
                for (int i = 0; i < sublist.Count; i++)
                {
                    substation _sub = new substation();
                    _sub.EleID = ((SvgElement)sublist[i]).ID;
                    _sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    _sub = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _sub);
                    if (_sub != null)
                    {
                        SVG_ENTITY s_ent = new SVG_ENTITY();
                        s_ent.EleID = _sub.EleID;
                        s_ent.NAME = _sub.EleName;
                        s_ent.SUID = Guid.NewGuid().ToString();
                        s_ent.svgID = _sub.SvgUID;
                        s_ent.MDATE = System.DateTime.Now;
                        if (_sub.ObligateField1 != "")
                        {
                            s_ent.voltage = Convert.ToInt32(_sub.ObligateField1);
                        }
                        s_ent.TYPE = "substation";
                        s_ent.layerID = ((SvgElement)sublist[i]).GetAttribute("layer");
                        Services.BaseService.Create<SVG_ENTITY>(s_ent);
                    }
                }
                ConfigurationSettings.AppSettings.Set("SaveAllItem", "1");
            }
        }
        public void Save()
        {
            try
            {
                //if (!tlVectorControl1.IsModified) return;
                tlVectorControl1.SVGDocument.Changed = true;
                if (tlVectorControl1.SVGDocument.SvgdataUid != string.Empty)
                {
                    IList svglist = Services.BaseService.GetList("SelectSVGFILEByKey", tlVectorControl1.SVGDocument.SvgdataUid);
                    if (svglist.Count > 0)
                    {
                        svg = (SVGFILE)svglist[0];
                        svg.SVGDATA = tlVectorControl1.SVGDocument.OuterXml;

                        Services.BaseService.Update<SVGFILE>(svg);

                        //XmlNodeList list= tlVectorControl1.SVGDocument.SelectNodes("//*[@IsLead='1']");
                        //for (int n = 0; n < list.Count;n++ )
                        //{
                        //    XmlNode _n = list[n];
                        //    LineInfo _temp=new LineInfo();
                        //    _temp.EleID=((XmlElement)_n).GetAttribute("id");
                        //    _temp.SvgUID=tlVectorControl1.SVGDocument.SvgdataUid;
                        //    LineInfo _l =(LineInfo) Services.BaseService.GetObject("SelectLineInfoByEleID",_temp);
                        //    if(_l!=null){
                        //        _l.LayerID = ((XmlElement)_n).GetAttribute("layer");
                        //        Services.BaseService.Update<LineInfo>(_l);
                        //    }
                        //}
                        //XmlNodeList list2 = tlVectorControl1.SVGDocument.SelectNodes("//*[@IsArea='1']");
                        //for (int n = 0; n < list2.Count;n++ )
                        //{
                        //    XmlNode _n2 = list2[n];
                        //    glebeProperty _temp2 = new glebeProperty();
                        //    _temp2.EleID=((XmlElement)_n2).GetAttribute("id");
                        //    _temp2.SvgUID=tlVectorControl1.SVGDocument.SvgdataUid;
                        //    glebeProperty _g = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", _temp2);
                        //    if(_g!=null){
                        //        _g.LayerID = ((XmlElement)_n2).GetAttribute("layer");
                        //        Services.BaseService.Update<glebeProperty>(_g);
                        //    }
                        //}

                    }
                    else
                    {
                        svg.SUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        svg.FILENAME = tlVectorControl1.SVGDocument.FileName;
                        svg.SVGDATA = tlVectorControl1.SVGDocument.OuterXml;
                        if (MapType == "所内接线图")
                        {
                            svg.PARENTID = "";
                        }
                        Services.BaseService.Create<SVGFILE>(svg);
                    }

                }
                else
                {
                    svg.SUID = Guid.NewGuid().ToString();
                    svg.FILENAME = tlVectorControl1.SVGDocument.FileName;
                    svg.SVGDATA = tlVectorControl1.SVGDocument.OuterXml;
                    Services.BaseService.Create<SVGFILE>(svg);
                    tlVectorControl1.SVGDocument.SvgdataUid = svg.SUID;

                }
                //this.Text = tlVectorControl1.SVGDocument.FileName;
                tlVectorControl1.IsModified = false;
                //CtrlSvgView.CashSvgDocument = tlVectorControl1.SVGDocument;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        public void InitData()
        {
            svg.SUID = "1";
            IList svglist = Services.BaseService.GetList("SelectSVGFILEByKey", svg);
            svg = (SVGFILE)svglist[0];

            sdoc.LoadXml(svg.SVGDATA);
            tlVectorControl1.SVGDocument = sdoc;
            ItopVector.SpecialCursors.LoadCursors();
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.SVGDocument.SvgdataUid = svg.SUID;
            //LoadShape("symbol2.xml");
        }
        public Layer getlayer(string LayerName, ArrayList LayerList)
        {
            Layer layer = null;
            for (int i = 0; i < LayerList.Count; i++)
            {
                if (LayerName == ((Layer)LayerList[i]).Label)
                {
                    layer = (Layer)LayerList[i];
                    break;
                }
            }
            return layer;
        }
        public static bool getlayer(string CurrLayerID, string LayerName, ArrayList LayerList)
        {
            Layer layer = null;
            string layerType = "";
            for (int i = 0; i < LayerList.Count; i++)
            {
                if (CurrLayerID == ((Layer)LayerList[i]).ID)
                {
                    layer = (Layer)LayerList[i];
                    layerType = layer.GetAttribute("layerType");
                    if (layerType == LayerName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void ImportDxf()
        {
            string strGUID = tlVectorControl1.SVGDocument.SvgdataUid;
            openFileDialog1.Filter = "AUTOCAD DXF文件 (*.dxf)(*.dwg)|*.dxf;*.dwg";
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                // fileName = dlg.FileName;openFileDialog1.FileName
                // Itop.CADConvert.CADConvertHelper().ConvertToSvg(dlg.FileName)
                string str = progtype;
                SvgDocument svg = new Itop.CADConvert.CADConvertHelper().ConvertToSvg(openFileDialog1.FileName);
                frmMain.progtype = "";
                tlVectorControl1.FullDrawMode = true;
                tlVectorControl1.SVGDocument = svg;
                tlVectorControl1.SVGDocument.SvgdataUid = strGUID;
                tlVectorControl1.SVGDocument.FileName = this.Text;
                CreateComboBox();
                progtype = str;
            }
        }

        public void Recalculate(decimal _viewScale)
        {
            decimal sum = 0;
            string svguid = tlVectorControl1.SVGDocument.SvgdataUid;
            XmlNodeList n1 = tlVectorControl1.SVGDocument.SelectNodes("svg/polygon [@IsArea='1']");

            Hashtable hs = new Hashtable();
            // hs.Add("ParentEleID", "1");
            hs.Add("SvgUID", svguid);
            IList dkList = Services.BaseService.GetList("SelectglebePropertParentIDSubAll", hs);
            Hashtable dkHs = new Hashtable(dkList.Count);
            for (int i = 0; i < dkList.Count; i++)
            {
                glebeProperty _gle = (glebeProperty)dkList[i];
                dkHs.Add(_gle.EleID, _gle.ObligateField10);
            }

            for (int j = 0; j < n1.Count; j++)
            {
                XmlElement _node1 = (XmlElement)n1.Item(j);
                string t = (string)dkHs[_node1.GetAttribute("id")];
                if (t != null)
                {
                    string color1 = ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(t)));
                    color1 = "fill:" + color1 + ";";
                    _node1.SetAttribute("style", color1);
                }
            }
            // 重新统计地块负荷
            Hashtable HashTable1 = new Hashtable();
            HashTable1.Add("SUID", svguid);
            Services.BaseService.Update("UpdateGlebePropertyAll", HashTable1);

            tlVectorControl1.Refresh();
            // 重新统计供电区域负荷
            //Hashtable hs2 = new Hashtable();
            //hs2.Add("ParentEleID", "0");
            //hs2.Add("SvgUID", svguid);
            //IList glist=Services.BaseService.GetList("SelectglebePropertParentIDTopAll",hs2);
            //for (int n = 0; n < glist.Count;n++ )
            //{
            //    glebeProperty _gle = (glebeProperty)glist[n];
            //    string selGle = _gle.SelSonArea;
            //    if (selGle != null && selGle != "")
            //    {
            //       string[] gleList=selGle.Split(";".ToCharArray());
            //       for (int m = 0; m < gleList.Length;m++ )
            //       {
            //           string[] str = gleList[m].Split(",".ToCharArray());
            //           Hashtable _hs = new Hashtable();
            //           _hs.Add("SvgUID", svguid);
            //           _hs.Add("EleID", str[0]);
            //           glebeProperty _songle = (glebeProperty)Services.BaseService.GetObject("SelectglebeTypeValueByEleID",_hs);
            //           if(_songle!=null){
            //              sum=sum+Convert.ToDecimal(_songle.TypeUID) * Convert.ToDecimal(str[1]);
            //           }
            //       }
            //       _gle.Burthen = sum;
            //       Services.BaseService.Update<glebeProperty>(_gle);
            //    }

            //}

            //try
            //{
            //    string _svgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            //    IList gleList2 = Services.BaseService.GetList("SelectglebePropertParentIDSubForBatchUpdate2", _svgUID);
            //    for (int i = 0; i < gleList2.Count; i++)
            //    {
            //        glebeProperty gle = (glebeProperty)gleList2[i];
            //        gle.Area = gle.Area * _viewScale;
            //        if (gle.TypeUID != null && gle.TypeUID != "")
            //        {
            //            gle.Burthen = Convert.ToDecimal(gle.Area) * Convert.ToDecimal(gle.TypeUID) * _viewScale;
            //        }
            //        else
            //        {
            //            gle.Burthen = Convert.ToDecimal(gle.Area) * _viewScale; //* Convert.ToDecimal( gle.TypeUID);
            //        }
            //        if (gle.SonUid != null && gle.SonUid != "")
            //        {
            //            gle.Number = Convert.ToDecimal(gle.Area) * Convert.ToDecimal(gle.TypeUID) * Convert.ToDecimal(gle.SonUid) * _viewScale;
            //        }
            //        else
            //        {
            //            gle.Number = Convert.ToDecimal(gle.Area) * Convert.ToDecimal(gle.TypeUID) * Convert.ToDecimal(rzb) * _viewScale;
            //        }
            //        gle.SonUid = "";
            //        Services.BaseService.Update("UpdateglebePropertyBatch", gle);
            //    }
            //    //计算地块负荷和电量
            //    IList gleList = Services.BaseService.GetList("SelectglebePropertParentIDSubForBatchUpdate", _svgUID);
            //    for (int i = 0; i < gleList.Count; i++)
            //    {
            //        glebeProperty gle = (glebeProperty)gleList[i];
            //        gle.Area = gle.Area * _viewScale;
            //        if (gle.TypeUID != null && gle.TypeUID != "")
            //        {
            //            gle.Burthen = Convert.ToDecimal(gle.Area) * Convert.ToDecimal(gle.TypeUID) * _viewScale;
            //        }
            //        else
            //        {
            //            gle.Burthen = Convert.ToDecimal(gle.Area) * _viewScale; //* Convert.ToDecimal( gle.TypeUID);
            //        }
            //        if (gle.SonUid != null && gle.SonUid != "")
            //        {
            //            gle.Number = Convert.ToDecimal(gle.Area) * Convert.ToDecimal(gle.TypeUID) * Convert.ToDecimal(gle.SonUid) * _viewScale;
            //        }
            //        else
            //        {
            //            gle.Number = Convert.ToDecimal(gle.Area) * Convert.ToDecimal(gle.TypeUID) * Convert.ToDecimal(rzb) * _viewScale;
            //        }
            //        gle.SonUid = "";
            //        Services.BaseService.Update("UpdateglebePropertyBatch", gle);
            //    }
            //    //计算区域负荷和电量
            //    Hashtable hs = new Hashtable();
            //    hs.Add("ParentEleID", "0");
            //    hs.Add("SvgUID", _svgUID);
            //    IList polylist = Services.BaseService.GetList("SelectglebePropertParentIDTop", hs);
            //    for (int n = 0; n < polylist.Count; n++)
            //    {
            //        decimal _number = 0;
            //        decimal _burthen = 0;
            //        glebeProperty poly = (glebeProperty)polylist[n];
            //        string SelSonArea = poly.SelSonArea;
            //        string[] strArea = SelSonArea.Split(";".ToCharArray());
            //        for (int m = 0; m < strArea.Length; m++)
            //        {
            //            if (strArea[m] != "")
            //            {
            //                string[] _sonstr = strArea[m].Split(",".ToCharArray());
            //                glebeProperty _gle = new glebeProperty();
            //                _gle.SvgUID = _svgUID;
            //                _gle.EleID = _sonstr[0];
            //                _gle = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", _gle);
            //                if (_gle != null)
            //                {
            //                    _number = _number + _gle.Number * (Convert.ToDecimal(_sonstr[1]) / _gle.Area);
            //                    _burthen = _burthen + _gle.Burthen * (Convert.ToDecimal(_sonstr[1]) / _gle.Area);
            //                }
            //            }

            //        }
            //        poly.Area = poly.Area * _viewScale;
            //        poly.Burthen = _burthen * _viewScale;
            //        poly.Number = _number * _viewScale;
            //        Services.BaseService.Update("UpdateglebePropertyBatch", poly);
            //    }
            //}
            //catch(Exception ex){
            //    MessageBox.Show("比例尺过大，请重新设定。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //}
        }

        #endregion

        private void tlVectorControl1_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void tlVectorControl1_MouseUp(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("ok");
            //if (tlVectorControl1.Operation == ToolOperation.Custom11 || (tlVectorControl1.Operation == ToolOperation.ShapeTransform && ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)))
            //{

            //    LineInfo temp = new LineInfo();
            //    temp.EleID = tlVectorControl1.SVGDocument.CurrentElement.ID;
            //    temp.SvgUID = SVGUID;
            //    IList svglist = Services.BaseService.GetList("SelectLineInfoByEleID", temp);
            //    if (svglist.Count > 0)
            //    {
            //        temp = (LineInfo)svglist[0];
            //        Polyline li = (Polyline)tlVectorControl1.SVGDocument.CurrentElement;
            //        decimal temp1 = TLMath.getPolylineLength(li, 1);
            //        temp1 = TLMath.getNumber(temp1, tlVectorControl1.ScaleRatio);
            //        string len = temp1.ToString("#####.####");
            //        temp.Length = len;
            //        temp.EleID = tlVectorControl1.SVGDocument.CurrentElement.ID + "A";
            //        Services.BaseService.Update<LineInfo>(temp);
            //        li = (Polyline)tlVectorControl1.SVGDocument.GetElementById(tlVectorControl1.SVGDocument.CurrentElement.ID + "B");
            //        temp1 = TLMath.getPolylineLength(li, 1);
            //        temp1 = TLMath.getNumber(temp1, tlVectorControl1.ScaleRatio);
            //        len = temp1.ToString("#####.####");
            //        temp.EleID = tlVectorControl1.SVGDocument.CurrentElement.ID + "B";
            //        temp.Length = len;
            //        temp.UID = Guid.NewGuid().ToString();
            //        Services.BaseService.Create<LineInfo>(temp);
            //    }
            //}

        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            try
            {
                fInfo.Close();
                if (tlVectorControl1.IsModified)
                {
                    if (MessageBox.Show("是否保存已做的修改?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        SaveButton();
                    }
                }
                // CtrlSvgView.MapType = MapType;

                if (MapType == "所内接线图")
                {
                    MapType = "接线图";
                }
                //if (OnCloseSvgDocument != null) {
                //    OnCloseSvgDocument(sender, tlVectorControl1.SVGDocument.SvgdataUid, ParentUID);
                //}
                tlVectorControl1.SVGDocument.SelectCollection.Clear();

            }
            catch (Exception e1) { }
        }

        private void popupContainerEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            popupContainerEdit1.Text = selLar;
        }

        private void popupContainerEdit1_Click(object sender, EventArgs e)
        {
        }

        private void checkedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            try
            {
                selLar = checkedListBoxControl1.SelectedValue.ToString();
                Layer lar = getlayer(selLar, tlVectorControl1.SVGDocument.getLayerList());
                SvgDocument.currentLayer = lar.ID;
                if (checkedListBoxControl1.GetItemChecked(e.Index))
                {
                    lar.Visible = true;
                }
                else
                {
                    lar.Visible = false;
                }
                if (lar.GetAttribute("layerType") == progtype)
                {
                    tlVectorControl1.CanEdit = true;
                }
                else
                {
                    tlVectorControl1.CanEdit = false;
                }
            }
            catch (Exception e1) { }
        }

        private void checkedListBoxControl1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (checkedListBoxControl1.SelectedValue != null)
            //    {
            //        selLar = checkedListBoxControl1.SelectedValue.ToString();
            //    }

            //}
            //catch (Exception e1) { }
        }

        public void SetBarEnabed(bool b)
        {
            if (progtype == "地理信息层")
            {
                DlBarVisible(b);
            }
            if (progtype == "城市规划层")
            {
                DkBarVisible(b);
            }
            if (progtype == "电网规划层")
            {
                DwBarVisible(b);
            }
            if (MapType == "所内接线图")
            {
                JxtBar();
            }
        }
        public void JxtBar()
        {
            dotNetBarManager1.Bars["bar6"].Visible = true;
            dotNetBarManager1.Bars["bar6"].Enabled = true;
            dotNetBarManager1.Bars["bar7"].Visible = false;
            dotNetBarManager1.Bars["bar8"].Visible = true;
            dotNetBarManager1.Bars["bar8"].GetItem("mEdit").Enabled = true;

            dotNetBarManager1.Bars["bar2"].GetItem("mFreeTransform").Enabled = true;
            dotNetBarManager1.Bars["bar2"].GetItem("mShapeTransform").Enabled = true;
            dotNetBarManager1.Bars["bar2"].GetItem("mLine").Enabled = true;
            dotNetBarManager1.Bars["bar2"].GetItem("mPolyline").Enabled = true;
            dotNetBarManager1.Bars["bar2"].GetItem("mAngleRectangle").Enabled = true;
            dotNetBarManager1.Bars["bar2"].GetItem("mEllipse").Enabled = true;
            dotNetBarManager1.Bars["bar2"].GetItem("mPolygon").Enabled = true;
            dotNetBarManager1.Bars["bar2"].GetItem("mBezier").Enabled = true;
            dotNetBarManager1.Bars["bar2"].GetItem("mImage").Enabled = true;
            dotNetBarManager1.Bars["bar2"].GetItem("mText").Enabled = true;
            dotNetBarManager1.Bars["mainmenu"].GetItem("ImportDxf").Visible = true;

            dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem2").Enabled = false;
            dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem7").Enabled = false;
            propertyGrid.Enabled = true;
            symbolSelector.Enabled = true;
            bk1.Visible = false;
        }
        public void DlBarVisible(bool b)
        {
            dotNetBarManager1.Bars["bar6"].Enabled = b;
            dotNetBarManager1.Bars["bar8"].Visible = false;
            dotNetBarManager1.Bars["bar88"].Visible = false;
            dotNetBarManager1.Bars["bar7"].Visible = false;
            dotNetBarManager1.GetItem("ButtonItem7").Visible = false;
            dotNetBarManager1.Bars["bar2"].GetItem("mFreeTransform").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mShapeTransform").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mLine").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mPolyline").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mAngleRectangle").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mEllipse").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mPolygon").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mBezier").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mImage").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mText").Enabled = b;
            propertyGrid.Enabled = b;
            symbolSelector.Enabled = b;
            bk1.Visible = true;
        }
        public void DkBarVisible(bool b)
        {
            dotNetBarManager1.Bars["bar7"].Visible = true;
            dotNetBarManager1.Bars["bar6"].Enabled = b;
            dotNetBarManager1.Bars["bar8"].Visible = false;
            //dotNetBarManager1.Bars["bar7"].Enabled = b;
            //dotNetBarManager1.GetItem("ButtonItem7").Visible = false;
            dotNetBarManager1.GetItem("m_ld").Enabled = false;
            dotNetBarManager1.GetItem("m_fz").Enabled = false;
            dotNetBarManager1.GetItem("m_bxz").Enabled = false;
            dotNetBarManager1.Bars["bar7"].GetItem("mAreaPoly").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mLeadLine").Visible = false;
            dotNetBarManager1.Bars["bar7"].GetItem("mJQLeadLine").Visible = false;
            dotNetBarManager1.Bars["bar7"].GetItem("mFx").Visible = false;
            dotNetBarManager1.Bars["bar7"].GetItem("mFzzj").Visible = false;
            dotNetBarManager1.Bars["bar7"].GetItem("mReCompute").Visible = false;
            dotNetBarManager1.Bars["bar7"].GetItem("mPriQu").Visible = false;
            dotNetBarManager1.Bars["bar7"].GetItem("mFhbz").Visible = true;
            dotNetBarManager1.Bars["bar7"].GetItem("mFhbz").Enabled = b;

            dotNetBarManager1.Bars["bar2"].GetItem("mFreeTransform").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mShapeTransform").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mLine").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mPolyline").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mAngleRectangle").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mEllipse").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mPolygon").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mBezier").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mImage").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mText").Enabled = b;
            propertyGrid.Enabled = b;
            symbolSelector.Enabled = b;
            bk1.Visible = true;
        }
        public void DwBarVisible(bool b)
        {
            dotNetBarManager1.Bars["bar7"].Visible = true;
            dotNetBarManager1.Bars["bar8"].Visible = false;
            //dotNetBarManager1.Bars["bar8"].GetItem("mEdit").Enabled = false;
            dotNetBarManager1.Bars["bar6"].Enabled = b;
            dotNetBarManager1.Bars["bar7"].Enabled = b;
            dotNetBarManager1.GetItem("ButtonItem7").Enabled = b;
            dotNetBarManager1.GetItem("m_ld").Enabled = b;
            dotNetBarManager1.GetItem("m_fz").Enabled = b;
            dotNetBarManager1.GetItem("m_bxz").Enabled = b;
            dotNetBarManager1.GetItem("ButtonJXT").Enabled = b;

            dotNetBarManager1.GetItem("ButtonItem7").Visible = true;

            dotNetBarManager1.Bars["bar7"].GetItem("mAreaPoly").Visible = false;

            dotNetBarManager1.Bars["bar7"].GetItem("mLeadLine").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mJQLeadLine").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mFx").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mFzzj").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mAreaPoly").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mReCompute").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mFhbz").Visible = false;

            dotNetBarManager1.Bars["bar2"].GetItem("mFreeTransform").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mShapeTransform").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mLine").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mPolyline").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mAngleRectangle").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mEllipse").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mPolygon").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mBezier").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mImage").Enabled = b;
            dotNetBarManager1.Bars["bar2"].GetItem("mText").Enabled = b;
            propertyGrid.Enabled = b;
            symbolSelector.Enabled = b;
            bk1.Visible = true;
        }
        public void ViewMenu()
        {
            dotNetBarManager1.Bars["mainmenu"].GetItem("ButtonItem2").Visible = false;
        }

        private void bk1_CheckedChanged(object sender, EventArgs e)
        {
            if (bk1.Checked == true)
            {
                LoadImage = true;
            }
            else
            {
                LoadImage = false;
            }
            //tlVectorControl1.BackColor = ColorTranslator.FromHtml("#C7CBE2");
            tlVectorControl1.Refresh();
        }

        private void jxtToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        internal ItopVector.DrawArea.DrawArea PicturePanel
        {
            get { return this.picturePanel; }
            set { this.picturePanel = value; }
        }

        private void PasteWithProperty()
        {
            DataFormats.Format format1 = DataFormats.GetFormat("SvgElement");
            IDataObject obj1 = Clipboard.GetDataObject();
            try
            {
                if (!obj1.GetDataPresent(format1.Name))
                {
                    return;
                }
                object obj2 = obj1.GetData(format1.Name);
                if (!(obj2 is CopyData))
                {
                    return;
                }
                string text1 = ((CopyData)obj2).XmlStr;

                SvgDocument document1 = tlVectorControl1.DrawArea.SVGDocument;
                bool flag1 = document1.AcceptChanges;
                document1.AcceptChanges = false;
                XmlDocumentFragment fragment1 = document1.CreateDocumentFragment();
                bool flag2 = document1.firstload;
                document1.firstload = true;
                fragment1.InnerXml = text1;
                //document1.DealLast();
                document1.firstload = flag2;
                XmlNode node1 = fragment1.FirstChild;
                document1.AcceptChanges = true;
                if (!(node1 is SVG))
                {
                    return;
                }
                document1.NumberOfUndoOperations = (2 * node1.ChildNodes.Count) + 200;
                DateTime dt1 = DateTime.Now;
                tlVectorControl1.DrawArea.BeginInsert();
                //				for (int num1 = 0; num1 < node1.ChildNodes.Count; num1++)
                //				{
                //					XmlNode node2 = node1.ChildNodes[num1];
                //					if (node2 is IGraph)
                //					{此方法count在减少，内部XML功能，不好控制，所以撇了
                //						this.picturePanel.AddElement((SvgElement) node2);
                //						num1--;
                //					}
                //				}
                foreach (XmlNode node2 in node1.ChildNodes)
                {
                    if (node2 is IGraph)
                    {
                        SvgElement element = node2 as SvgElement;
                        SvgElement temp = node2.CloneNode(true) as SvgElement;
                        tlVectorControl1.DrawArea.AddElement(temp);


                        LineInfo _line = new LineInfo();
                        _line.EleID = element.ID;
                        _line.SvgUID = tlVectorControl1.DrawArea.SVGDocument.SvgdataUid;
                        IList lineInfoList = Services.BaseService.GetList("SelectLineInfoByEleID", _line);
                        foreach (LineInfo line in lineInfoList)
                        {
                            line.UID = Guid.NewGuid().ToString();
                            line.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            line.EleID = temp.ID;
                            Services.BaseService.Create<LineInfo>(line);
                        }
                        glebeProperty gle = new glebeProperty();
                        gle.EleID = element.ID;
                        gle.SvgUID = tlVectorControl1.DrawArea.SVGDocument.SvgdataUid;
                        IList gleProList = Services.BaseService.GetList("SelectglebePropertyByEleID", gle);
                        foreach (glebeProperty gleP in gleProList)
                        {
                            gleP.UID = Guid.NewGuid().ToString();
                            gleP.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            gleP.EleID = temp.ID;
                            Services.BaseService.Create<glebeProperty>(gleP);
                        }
                        substation _sub = new substation();
                        _sub.EleID = element.ID;
                        _sub.SvgUID = tlVectorControl1.DrawArea.SVGDocument.SvgdataUid;
                        IList substationList = Services.BaseService.GetList("SelectsubstationByEleID", _sub);
                        foreach (substation sub in substationList)
                        {
                            sub.UID = Guid.NewGuid().ToString();
                            sub.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                            sub.EleID = temp.ID;
                            Services.BaseService.Create<substation>(sub);
                        }
                    }
                }
                tlVectorControl1.DrawArea.EndInsert();

                document1.AcceptChanges = flag1;
                document1.NotifyUndo();

                //				DateTime dt2=DateTime.Now;
                //				TimeSpan tsp=dt2 -dt1;
                //				this.picturePanel.ToolTip(tsp.ToString()+tsp.Milliseconds.ToString(),1);

            }
            catch (Exception e)
            {
                MessageBox.Show("粘贴对象失败!");
            }
        }
        private void layerExport()
        {
            if (tlVectorControl1.SVGDocument.CurrentLayer != null)
            {
                Layer layer = tlVectorControl1.SVGDocument.CurrentLayer;
                XmlNode node = tlVectorControl1.SVGDocument.SelectSingleNode("//*[@layer='" + layer.ID + "']");
                if (node == null)
                {
                    MessageBox.Show("图层为空图层，不能进行导出操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                LayerFile temp = new LayerFile();
                temp.SvgDataUid = tlVectorControl1.SVGDocument.SvgdataUid;
                IList lList = Services.BaseService.GetList("SelectLayerFileBySvgDataUid", temp);
                foreach (LayerFile lay in lList)
                {
                    if (lay.LayerFileName == layer.Label)
                    {
                        MessageBox.Show("文档中已经存在同名图层,请修改图层名称后导出。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                string strsvgData = tlVectorControl1.SVGDocument.SelectNodesToString("svg/*[@layer='" + layer.ID + "']|svg/defs");
                LayerFile layerFile = new LayerFile();
                layerFile.SUID = Guid.NewGuid().ToString();
                layerFile.LayerID = layer.ID;
                layerFile.LayerFileName = layer.Label;
                layerFile.SvgDataUid = SVGUID;
                layerFile.LayerOuterXml = strsvgData;
                Services.BaseService.Create<LayerFile>(layerFile);
                MessageBox.Show("图层已成功导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //frmlar.DeleteLayer(layer);
                //frmlar.InitData();
            }
        }
        private void layerImport()
        {
            frmLayerInPut dlgInPut = new frmLayerInPut();
            dlgInPut.InitData(SVGUID);
            string layerOutXml = null;
            if (dlgInPut.ShowDialog(this) == DialogResult.OK)
            {
                FlashWindow frmLoad = new FlashWindow();
                foreach (LayerFile layer in dlgInPut.InputLayerList)
                {
                    if (Layer.CkLayerExist(layer.LayerFileName, tlVectorControl1.SVGDocument))
                    {
                        if (MessageBox.Show("文档中已经存在同名图层,是否覆盖原图层。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {

                            if (layerOutXml == null)
                            {
                                layerOutXml = layer.LayerOuterXml;
                            }
                            else
                            {
                                layerOutXml = SvgDocument.Union(layerOutXml, layer.LayerOuterXml);
                            }

                            Services.BaseService.Update("DeleteLayerFile", layer);
                            frmlar.DeleteLayer(getlayer(layer.LayerFileName, tlVectorControl1.SVGDocument.getLayerList()));
                        }
                    }
                    else
                    {
                        if (layerOutXml == null)
                        {
                            layerOutXml = layer.LayerOuterXml;
                        }
                        else
                        {
                            layerOutXml = SvgDocument.Union(layerOutXml, layer.LayerOuterXml);
                        }
                        Services.BaseService.Update("DeleteLayerFile", layer);
                        //frmlar.DeleteLayer(getlayer(layer.LayerFileName, tlVectorControl1.SVGDocument.getLayerList()));
                    }
                    //FileStream a = new FileStream("c:\\1.xml",FileMode.OpenOrCreate);
                    //StreamWriter str= new StreamWriter(a);
                    //str.Write(layerOutXml);
                    //str.Close();
                }
                dlgInPut.Close();
                dlgInPut.Dispose();
                if (layerOutXml != null)
                {
                    string svgName = tlVectorControl1.SVGDocument.FileName;
                    string svgUid = tlVectorControl1.SVGDocument.SvgdataUid;
                    ItopVector.SpecialCursors.LoadCursors();
                    frmLoad.Show();
                    frmLoad.RefleshStatus("正在导入图层...");
                    Application.DoEvents();
                    frmLoad.SplashData();
                    frmLoad.Owner = tlVectorControl1.ParentForm;
                    frmLoad.Refresh();

                    //tlVectorControl1.SVGDocument.LoadXml(SvgDocument.Union(tlVectorControl1.SVGDocument.OuterXml, lay.LayerOuterXml));
                    tlVectorControl1.SVGDocument = LoadXMLToCtrl(SvgDocument.Union(tlVectorControl1.SVGDocument.OuterXml, layerOutXml));
                    tlVectorControl1.PropertyGrid = propertyGrid;
                    tlVectorControl1.SVGDocument.SvgdataUid = svg.SUID;
                    //tlVectorControl1.SVGDocument.SvgdataUid = svgUid;
                    tlVectorControl1.SVGDocument.FileName = svgName;
                    tlVectorControl1.IsModified = true;

                    frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
                    frmlar.InitData();
                    MessageBox.Show("图层已成功导入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //if (dlgInPut.InputText != "")
                //{


                //    LayerFile temp = new LayerFile();
                //    temp.LayerFileName = dlgInPut.InputText;
                //    temp.SvgDataUid = tlVectorControl1.SVGDocument.SvgdataUid;
                //    LayerFile lay = (LayerFile)Services.BaseService.GetObject("SelectLayerFileByNameAndSvgDataUid", temp);

                //if (lay != null)
                //{
                //    if (Layer.CkLayerExist(dlgInPut.InputText, tlVectorControl1.SVGDocument))
                //    {
                //        if (MessageBox.Show("文档中已经存在同名图层,是否覆盖原图层。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //        {
                //            frmlar.DeleteLayer(getlayer(dlgInPut.InputText, tlVectorControl1.SVGDocument.getLayerList()));
                //            string svgName = tlVectorControl1.SVGDocument.FileName;
                //            string svgUid = tlVectorControl1.SVGDocument.SvgdataUid;
                //            ItopVector.SpecialCursors.LoadCursors();
                //            frmLoad.Show();
                //            frmLoad.RefleshStatus("正在导入图层...");  
                //            Application.DoEvents();                             
                //            frmLoad.SplashData();
                //            frmLoad.Owner = tlVectorControl1.ParentForm;
                //            frmLoad.Refresh();

                //            //tlVectorControl1.SVGDocument.LoadXml(SvgDocument.Union(tlVectorControl1.SVGDocument.OuterXml, lay.LayerOuterXml));
                //            tlVectorControl1.SVGDocument = LoadXMLToCtrl(SvgDocument.Union(tlVectorControl1.SVGDocument.OuterXml, lay.LayerOuterXml));
                //            tlVectorControl1.PropertyGrid = propertyGrid;
                //            tlVectorControl1.SVGDocument.SvgdataUid = svg.SUID;
                //            //tlVectorControl1.SVGDocument.SvgdataUid = svgUid;
                //            tlVectorControl1.SVGDocument.FileName = svgName;                                
                //            tlVectorControl1.IsModified = true;

                //            frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
                //            frmlar.InitData();
                //            Services.BaseService.Update("DeleteLayerFile", lay);
                //            MessageBox.Show("图层已成功导入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);                                
                //        } 

                //    }
                //    else
                //    {   
                //        string svgName = tlVectorControl1.SVGDocument.FileName;
                //        string svgUid = tlVectorControl1.SVGDocument.SvgdataUid;
                //        ItopVector.SpecialCursors.LoadCursors();

                //        frmLoad.Show();
                //        frmLoad.RefleshStatus("正在导入图层...");
                //        Application.DoEvents();
                //        frmLoad.SplashData();
                //        frmLoad.Owner = tlVectorControl1.ParentForm;
                //        frmLoad.Refresh();

                //        tlVectorControl1.SVGDocument = LoadXMLToCtrl(SvgDocument.Union(tlVectorControl1.SVGDocument.OuterXml, lay.LayerOuterXml));
                //        tlVectorControl1.PropertyGrid = propertyGrid;
                //        tlVectorControl1.SVGDocument.SvgdataUid = svgUid;
                //        tlVectorControl1.SVGDocument.FileName = svgName;
                //        tlVectorControl1.IsModified = true;
                //        frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
                //        frmlar.InitData();
                //        Services.BaseService.Update("DeleteLayerFile", lay);
                //        MessageBox.Show("图层已成功导入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }                       
                //}
                //}
            }
        }
        private SvgDocument LoadXMLToCtrl(string strXML)
        {
            SvgDocument x = new SvgDocument();
            x.LoadXml(strXML);
            return x;
            //symbolDoc.NodeInserted += new System.Xml.XmlNodeChangedEventHandler(SVGDocument_NodeInserted);
        }
        private void DeleteLayer(Layer layer)
        {
            //if (!CkRight(layer))
            //{
            //    MessageBox.Show("基础图层不能改名或删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (true)
            //{
            //    LineInfo _line = new LineInfo();
            //    _line.LayerID = layer.ID;
            //    Services.BaseService.Update("DeleteLineInfoByLayerID", _line);
            //    glebeProperty gle = new glebeProperty();
            //    gle.LayerID = layer.ID;
            //    Services.BaseService.Update("DeleteglebePropertyByLayerID", gle);
            //    substation _sub = new substation();
            //    _sub.LayerID = layer.ID;
            //    Services.BaseService.Update("DeletesubstationByLayerID", _sub);
            //}
            XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("//*[@layer='" + layer.ID + "']");
            foreach (XmlNode elNode in list)
            {
                tlVectorControl1.SVGDocument.RootElement.RemoveChild(elNode);
            }

            //Services.BaseService.Update("UpdateGraPowerRelationByLayerID", layer.ID);
            //在文档中移除
            layer.Remove();
            //在列表中移除

        }
        public bool Modifier
        {
            get
            {
                return tlVectorControl1.IsModified;
            }
            set
            {
                tlVectorControl1.IsModified = value;
            }
        }
        public void ConnLine()
        {
            try
            {
                SvgElementCollection elements = tlVectorControl1.SVGDocument.SelectCollection;
                if (elements.Count == 2)
                {
                    Polyline pl1 = elements[0] as Polyline;
                    Polyline pl2 = elements[1] as Polyline;
                    if (pl1 != null && pl2 != null && pl1.GetAttribute("IsLead") != "" && pl2.GetAttribute("IsLead") != "")
                    {

                        PointF[] pfs1 = new PointF[2] { pl1.Points[0], pl1.Points[pl1.Points.Length - 1] };
                        PointF[] pfs2 = new PointF[2] { pl2.Points[0], pl2.Points[pl2.Points.Length - 1] };
                        PointF[] pfs3 = (PointF[])pl1.Points.Clone();
                        PointF[] pfs4 = (PointF[])pl2.Points.Clone();
                        pl1.Transform.Matrix.TransformPoints(pfs3);
                        pl2.Transform.Matrix.TransformPoints(pfs4);
                        ArrayList list = new ArrayList();
                        list.AddRange(pfs3);
                        list.Reverse();
                        PointF[] pfs5 = (PointF[])list.ToArray(typeof(PointF));
                        list.Clear();
                        list.AddRange(pfs4);
                        list.Reverse();
                        PointF[] pfs6 = (PointF[])list.ToArray(typeof(PointF));

                        pl1.Transform.Matrix.TransformPoints(pfs1);
                        pl2.Transform.Matrix.TransformPoints(pfs2);

                        double[] d = new double[4];

                        d[0] = TLMath.GetPointsLen(pfs1[0], pfs2[0]);
                        d[1] = TLMath.GetPointsLen(pfs1[0], pfs2[1]);
                        d[2] = TLMath.GetPointsLen(pfs1[1], pfs2[0]);
                        d[3] = TLMath.GetPointsLen(pfs1[1], pfs2[1]);

                        int num = 0;
                        double min = d[0];

                        for (int i = 1; i < 4; i++)
                        {
                            if (min > d[i])
                            {
                                num = i;
                                min = d[i];
                            }
                        }
                        list.Clear();
                        switch (num)
                        {
                            case 0:
                                list.AddRange(pfs5);
                                list.AddRange(pfs4);
                                break;
                            case 1:
                                list.AddRange(pfs5);
                                list.AddRange(pfs6);
                                break;
                            case 2:
                                list.AddRange(pfs3);
                                list.AddRange(pfs4);
                                break;
                            case 3:
                                list.AddRange(pfs3);
                                list.AddRange(pfs6);
                                break;
                        }
                        pl1.Points = (PointF[])list.ToArray(typeof(PointF));
                        LineInfo lpl1 = new LineInfo();
                        LineInfo lpl2 = new LineInfo();
                        lpl1.EleID = pl1.ID;
                        lpl1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        lpl2.EleID = pl2.ID;
                        lpl2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        lpl1 = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", lpl1);
                        lpl2 = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", lpl2);
                        if (lpl1 != null && lpl2 != null)
                        {
                            lpl1.LineName = lpl1.LineName + "连接" + lpl2.LineName;
                            decimal temp1 = TLMath.getPolylineLength(pl1, 1);
                            temp1 = TLMath.getNumber(temp1, tlVectorControl1.ScaleRatio);
                            string len = temp1.ToString("#####.####");
                            lpl1.Length = len;
                            Services.BaseService.Update<LineInfo>(lpl1);
                            Services.BaseService.Update("DeleteLineInfo", lpl2);
                        }
                        ItopVector.Core.Types.Transf transf = new ItopVector.Core.Types.Transf(new Matrix());
                        pl1.Transform = transf;
                        pl2.ParentNode.RemoveChild(pl2);
                        elements.Remove(pl2);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("提示", "请选择两条线路。", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
     
        private static DataSet ImportExcel(string strFileName)
        {
            if (strFileName.Length < 0) return null;

            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            conn.Close();

            IList<string> tblNames = new List<string>();
            foreach (DataRow dr in dtSchema.Rows)
            {
                tblNames.Add((string)dr["TABLE_NAME"]);
            }

            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [" + tblNames[0] + "]", strConn);

            DataSet ds = new DataSet();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message.ToString());
                System.Console.ReadLine();
            }

            return ds;
        }

        private void tmLineConnect_Click(object sender, EventArgs e)
        {
            ConnLine();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (chose == 1)
            {
                if (checkEdit1.Checked)
                {
                    mapview = new Itop.MapView.MapViewObj("MapData3d.yap");
                }
                else
                {
                    mapview = new Itop.MapView.MapViewObj();
                }
                mapview.ZeroLongLat = new LongLat(jd, wd);
                tlVectorControl1.Refresh();
            }
        }

        private void 城市ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (mapview as MapViewGoogle).SetTileType(1);
            (mapview as MapViewGoogle).IsDownMap = true;

            tlVectorControl1.Refresh();
        }

        private void 地形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (mapview as MapViewGoogle).SetTileType(8);
            (mapview as MapViewGoogle).IsDownMap = true;

            tlVectorControl1.Refresh();
        }

        private void 卫星ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (mapview as MapViewGoogle).SetTileType(2);
            (mapview as MapViewGoogle).IsDownMap = true;

            tlVectorControl1.Refresh();
        }


    }
}