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
using Itop.MapView;
using System.IO;
using System.Threading;
using Itop.CADLib;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ItopVector.Core.Interface;
using ItopVector.Core.Types;
using System.Data.OleDb;
using Itop.TLPSP.DEVICE;
using Itop.Client.Stutistics;
using DevExpress.Utils;
using Itop.Domain.RightManager;
using TinVoronoi;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;


namespace ItopVector.Tools
{

    public partial class frmMain_wj : FormBase
    {
        #region 对象声明
        SVGFILE svg = new SVGFILE();
        SvgDocument sdoc = new SvgDocument();
        glebeProperty gPro = new glebeProperty();
        CtrlFileManager ctlfile = new CtrlFileManager();
        DevComponents.DotNetBar.ToolTip tip;
        public ArrayList SaveID = new ArrayList();
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
        double TLPSPVmin = 0.95, TLPSPVmax = 1.05;
        string strj1 = "";
        string strw1 = "";
        string strd1 = "";
        string strj2 = "";
        string strw2 = "";
        string strd2 = "";
        string sel_sym = "";
        string sel_start_point = "";
        string bdz_xz = "";
        string str_dhx = "";
        ArrayList ChangeLayerList = new ArrayList();
        frmLayerTreeManager frmlar = new frmLayerTreeManager();
        private string SVGUID = "";
        private string SelUseArea = "";
        private string LineLen = "";
        private string rzb = "1";
        private string selLar = "";
        private int LayerCount = 0;

        private string str_num = "";
        private string str_dy = "";
        private string str_jj = "";
        private double dbl_rzb=1.9;
        private double dbl_rl = 0;
        private string str_djcl = "";
        private string str_outjwd = "";
        Delaynay D_TIN = new Delaynay();
        private bool LoadImage = true;
        public bool SubPrint = false;
        private bool Wjghboolflag = false;
        public string ghType = "";
        int chose;
        decimal areaoption = 1;
        decimal jd = 0;
        decimal wd = 0;
        int show3d = 0;
        XmlNode img = null;
        frmInfo fInfo = new frmInfo();
        public Itop.MapView.IMapViewObj mapview;
        //public Itop.MapView.IMapViewObj mapview1;
        //public Itop.MapView.IMapViewObj mapview2;
        public event OnCloseDocumenthandler OnCloseSvgDocument;
        int mapOpacity = 100;
        public int MapOpacity
        {
            get
            {
                return mapOpacity;
            }
            set
            {
                mapOpacity = value;
                tlVectorControl1.Invalidate(true);
            }
        }
        #endregion

        public frmMain_wj()
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
            tlVectorControl1.DrawArea.OnElementMove += new ElementMoveEventHandler(DrawArea_OnElementMove);
            tlVectorControl1.DrawArea.OnMouseDown += new MouseEventHandler(DrawArea_OnMouseDown);
            toolDel.Click += delegate { tlVectorControl1.Delete(); };
            SvgDocument.BkImageLoad = true;
            Wjghboolflag = false;
            Pen pen1 = new Pen(Brushes.Cyan, 3);
            tlVectorControl1.TempPen = pen1;
            tlVectorControl1.PropertyGrid = propertyGrid;
            tlVectorControl1.BackColor = Color.White;
            tlVectorControl1.OperationChanged += new EventHandler(tlVectorControl1_OperationChanged);
            tlVectorControl1.FullDrawMode = true;
            int ViewMargin = 0;
            try
            {
                ViewMargin = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("viewmargin"));
            }
            catch { }

            if (ViewMargin < 1000) ViewMargin = 50000;
            tlVectorControl1.DrawArea.ViewMargin = new Size(ViewMargin, ViewMargin);
            tlVectorControl1.DrawMode = DrawModeType.MemoryImage;
            jd = Convert.ToDecimal(ConfigurationSettings.AppSettings.Get("jd"));
            wd = Convert.ToDecimal(ConfigurationSettings.AppSettings.Get("wd"));
            ghType = ConfigurationSettings.AppSettings.Get("ghType");
            //mapview.ZeroLongLat = new LongLat(117.6787m, 31.0568m);
            //mapview.ZeroLongLat = new LongLat(108.1m, 24.75m);
            chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
            show3d = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("show3d"));

            try
            {
                areaoption = Convert.ToDecimal(ConfigurationSettings.AppSettings.Get("AreaOption"));
            }
            catch { }
            if (areaoption < 1) { areaoption = 1; }


            if (show3d == 1)
            {
                checkEdit1.Visible = true;
            }
            else if (show3d == 0)
            {
                checkEdit1.Visible = false;
            }
            if (chose == 1) { mapview = new Itop.MapView.MapViewObj(); }
            else if (chose == 2)
            {
                mapview = new Itop.MapView.MapViewGoogle();
                mapview.OnDownCompleted += new DownCompleteEventHandler(mapview_OnDownCompleted);
            }
            mapview.ZeroLongLat = new LongLat(jd, wd);
            tlVectorControl1.CurrentOperation = ToolOperation.Select;

        }

        void mapview_OnDownCompleted(ClassImage mapclass)
        {
            if (mapclass.PicImage != null)
                tlVectorControl1.DrawArea.InvadateRect(mapclass.Bounds);
        }
        
        void DrawArea_OnElementMove(object sender, MoveEventArgs e)
        {

            //string a = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            //MessageBox.Show(a);
            //变电站选址移动时起相关的辐射线也跟着移动
            try {
                ISvgElement element = e.SvgElement;
                XmlElement el = (XmlElement)tlVectorControl1.SVGDocument.SelectSingleNode("svg/use [@id='" + element.ID + "']");
                if ((!string.IsNullOrEmpty(el.GetAttribute("xzflag").ToString()) && el.GetAttribute("xzflag").ToString() == "1") || (!string.IsNullOrEmpty(el.GetAttribute("ghyk")) && el.GetAttribute("ghyk").ToString() == "1"))
                {
                    SvgElementCollection list = tlVectorControl1.SVGDocument.SelectCollection;
                    PointF beforeMove = e.BeforeMove;
                    PointF afterMove = e.AfterMove;
                    XmlNodeList listFirstNode = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline[@FirstNode='" + element.ID + "']");
                    RectangleF t = ((IGraph)element).GetBounds();
                    PointF[] ptt = new PointF[] { beforeMove, afterMove };
                    Transf tran = (element as Graph).Transform;
                    tran.Matrix.TransformPoints(ptt);
                    beforeMove = ptt[0];
                    afterMove = ptt[1];
                    foreach (XmlNode node in listFirstNode) {
                        if (list.Contains((ISvgElement)(node as XmlElement))) {
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
                        temp = first[0].X + " " + first[0].Y + "," + first[1].X + " " + first[1].Y;
                        //foreach (PointF pt in first) {
                        //    if (temp == null) {
                        //        temp += pt.X + " " + pt.Y;
                        //    } else {
                        //        temp += "," + pt.X + " " + pt.Y;
                        //    }
                        //}
                        //if (first[0] != pt1) {
                        (node as XmlElement).SetAttribute("points", temp);
                        // }
                    }

                    //XmlNode listText = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.ID + "']");
                    //(listText as Text).SetAttribute("x", (Convert.ToDouble((listText as Text).GetAttribute("x")) + afterMove.X - beforeMove.X).ToString());
                    //(listText as Text).SetAttribute("y", (Convert.ToDouble((listText as Text).GetAttribute("y")) + afterMove.Y - beforeMove.Y).ToString());
                }
            }
            catch(Exception e1)
            {

            }
           
            

        }

        void DrawArea_GraphChanged(object sender, EventArgs e)
        {


        }

        void SVGDocument_OnDocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            //string a = tlVectorControl1.SVGDocument.CurrentLayer.ID;
            //MessageBox.Show(a);
        }
        void DrawArea_OnMouseDown(object sender, MouseEventArgs e)
        {

        }
        void DrawArea_OnAddElement(object sender, AddSvgElementEventArgs e)
        {
            string larid = tlVectorControl1.SVGDocument.CurrentLayer.ID;

            if (!ChangeLayerList.Contains(larid))
            {
                ChangeLayerList.Add(larid);
            }
            //MessageBox.Show(e.SvgElement.ID);
            if (XZ_bdz.Length > 5)
            {
            lb11:

                frmInputDialog2 input = new frmInputDialog2();
                if (input.ShowDialog() == DialogResult.OK)
                {

                    PSP_SubstationSelect s = new PSP_SubstationSelect();
                    s.SName = input.InputStr;
                    s.SvgID = tlVectorControl1.SVGDocument.SvgdataUid;
                    PSP_SubstationSelect ss1 = (PSP_SubstationSelect)Services.BaseService.GetObject("SelectPSP_SubstationSelectByName", s);
                    if (ss1 != null)
                    {
                        MessageBox.Show("名称重复。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        goto lb11;
                    }
                    else
                    {
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
            mapview.Paint(e.G, (int)rt.Width, (int)rt.Height, 11, longlat.Longitude, longlat.Latitude, imageA);
        }
        bool isIn = false;
        System.Drawing.Image backbmp = null;
        void tlVectorControl1_AfterPaintPage(object sender, PaintMapEventArgs e)
        {
            try
            {
                if (LoadImage)
                {
                    int nScale = mapview.Getlevel(tlVectorControl1.ScaleRatio);
                    //if (nScale == -1)
                    //    return;
                    LongLat longlat = LongLat.Empty;
                    float nn = 1;
                    if (mapview is MapViewObj)
                    {
                        if (isIn)
                            nScale = mapview.Getlevel(tlVectorControl1.ScaleRatio, out nn);
                        else
                            nScale = mapview.Getlevel2(tlVectorControl1.ScaleRatio, out nn);
                    }
                    if (nScale == -1) return;
                    //计算中心点经纬度

                    e.G.Clear(Color.White);

                    longlat = mapview.OffSet(mapview.ZeroLongLat, nScale, (int)(e.CenterPoint.X / nn), (int)(e.CenterPoint.Y / nn));

                    //创建地图
                    //System.Drawing.Image image = mapview.CreateMap(e.Bounds.Width, e.Bounds.Height, nScale, longlat.Longitude, longlat.Latitude);
                    System.Drawing.Image image = backbmp;
                    if (nn >= 1)
                        image = mapview.CreateMap(e.Bounds.Width, e.Bounds.Height, nScale, longlat.Longitude, longlat.Latitude);
                    else
                        image = mapview.CreateMap((int)(e.Bounds.Width / nn), (int)(e.Bounds.Height / nn), nScale, longlat.Longitude, longlat.Latitude);
                    //backbmp = image

                    string newnScale = mapview.GetMiles(nScale);
                    ImageAttributes imageAttributes = new ImageAttributes();
                    ColorMatrix matrix1 = new ColorMatrix();
                    matrix1.Matrix00 = 1f;
                    matrix1.Matrix11 = 1f;
                    matrix1.Matrix22 = 1f;
                    matrix1.Matrix33 = this.mapOpacity / 100f;//地图透明度


                    matrix1.Matrix44 = 1f;
                    //设置地图透明度

                    imageAttributes.SetColorMatrix(matrix1, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);



                    //e.G.FillRectangle( brush,e.G.VisibleClipBounds);

                    int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
                    string Transparent = ConfigurationSettings.AppSettings.Get("Transparent");
                    if (string.IsNullOrEmpty(Transparent)) Transparent = "#EBEAE8";
                    if (chose == 1)
                    {
                        Color color = ColorTranslator.FromHtml(Transparent);//旧Mapbar新Mapbar#F4F1EC
                        imageAttributes.SetColorKey(color, color);
                    }
                    else if (chose == 2)
                    {

                        Color color = ColorTranslator.FromHtml("#F4F4FB");
                        Color color2 = ColorTranslator.FromHtml("#EFF0F1");
                        imageAttributes.SetColorKey(color2, color);
                    }
                    //绘制地图
                    //e.G.DrawImage((Bitmap)image, e.Bounds, 0f, 0f, (float)image.Width, (float)image.Height, GraphicsUnit.Pixel, imageAttributes);

                    if (nn > 1)
                    {
                        int w1 = (int)(e.Bounds.Width * ((nn - 1) / 2));
                        int h1 = (int)(e.Bounds.Height * ((nn - 1) / 2));

                        Rectangle rt1 = e.Bounds;
                        rt1.Inflate(w1, h1);
                        e.G.CompositingQuality = CompositingQuality.HighQuality;
                        e.G.DrawImage((Bitmap)image, rt1, 0f, 0f, (float)image.Width, (float)image.Height, GraphicsUnit.Pixel, imageAttributes);
                    }
                    else
                        e.G.DrawImage((Bitmap)image, e.Bounds, 0f, 0f, (float)image.Width, (float)image.Height, GraphicsUnit.Pixel, imageAttributes);
                    //  SolidBrush brush = new SolidBrush(Color.FromArgb(220, 75, 75, 75));
                    //绘制中心点


                    e.G.DrawEllipse(Pens.Red, e.Bounds.Width / 2 - 2, e.Bounds.Height / 2 - 2, 4, 4);
                    e.G.DrawEllipse(Pens.Red, e.Bounds.Width / 2 - 1, e.Bounds.Height / 2 - 1, 2, 2);

                    //绘制比例尺


                    if (tlVectorControl1.CurrentOperation != ToolOperation.Roam)
                    {
                        Point p1 = new Point(20, e.Bounds.Height - 30);
                        Point p2 = new Point(20, e.Bounds.Height - 20);
                        Point p3 = new Point(80, e.Bounds.Height - 20);
                        Point p4 = new Point(80, e.Bounds.Height - 30);
                        e.G.DrawLines(new Pen(Color.Black, 2), new Point[4] { p1, p2, p3, p4 });
                        //if (mapview is MapViewGoogle) {
                        //    PointF pf1 = new PointF(0, 0);
                        //    PointF pf2 = new PointF((float)100 / tlVectorControl1.ScaleRatio, 0);
                        //    newnScale +="\t"+ Math.Round(mapview.CountLength(pf1, pf2), 3) + "";
                        //}
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
            str_outjwd = "";
            str_djcl = "";
            sel_sym = "";
            sel_start_point = "";
            if (mapview is MapViewGoogle)
                mapMenu.Visible = true;
            try
            {
                if (csOperation == CustomOperation.OP_MeasureDistance)
                {
                    tlVectorControl1.Operation = ToolOperation.Select;
                    contextMenuStrip1.Hide();
                    return;
                }

                if (tlVectorControl1.Operation == ToolOperation.LeadLine && str_dhx == "1")
                {
                    SvgElement _x = tlVectorControl1.SVGDocument.CurrentElement;
                    _x.SetAttribute("dhx_key", "1");

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
                        printToolStripMenuItem.Visible = false;
                        toolDel.Visible = true;
                        sToolStripMenuItem.Visible = true;
                        清除关联ToolStripMenuItem.Visible = true;
                        tmjxt.Visible = true;
                        关联设备ToolStripMenuItem.Visible = true;
                        SubToolStripMenuItem.Visible = true;
                        saveImg.Visible = true;
                    }
                    else
                    {
                        if (tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("xlink:href").Contains("Substation"))
                        {
                            moveMenuItem.Visible = true;
                            jxtToolStripMenuItem.Visible = true;
                            w3MenuItem.Visible = true;
                            printToolStripMenuItem.Visible = false;
                            toolDel.Visible = true;
                            sToolStripMenuItem.Visible = true;
                            清除关联ToolStripMenuItem.Visible = true;
                            tmjxt.Visible = true;
                            关联设备ToolStripMenuItem.Visible = true;
                            SubToolStripMenuItem.Visible = true;
                            saveImg.Visible = true;
                        }
                    }
                    if (show3d == 0)
                    {
                        w3MenuItem.Visible = false;
                    }
                    if (tlVectorControl1.SVGDocument.CurrentElement != null &&
                      tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() != "ItopVector.Core.Figure.RectangleElement" &&
                      tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() != "ItopVector.Core.Figure.Polygon")
                    {
                        printToolStripMenuItem.Visible = false;
                        toolDel.Visible = false;
                        SubToolStripMenuItem.Visible = false;
                    
                        sToolStripMenuItem.Visible = true;
                        清除关联ToolStripMenuItem.Visible = true;
                        tmjxt.Visible = true;
                        关联设备ToolStripMenuItem.Visible = true;
                       
                        saveImg.Visible = true;
                    }
                    else
                    {
                        if (tlVectorControl1.SVGDocument.CurrentElement != null && tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.RectangleElement" && SubPrint == true)
                        {
                            printToolStripMenuItem.Visible = true;
                            toolDel.Visible = true;
                            sToolStripMenuItem.Visible = false;
                            清除关联ToolStripMenuItem.Visible = false;
                            tmjxt.Visible = false;
                            关联设备ToolStripMenuItem.Visible = false;
                            SubToolStripMenuItem.Visible = false;
                            saveImg.Visible = false;
                            
                        }
                        else
                        {
                            printToolStripMenuItem.Visible = false;
                            toolDel.Visible = false;
                            SubToolStripMenuItem.Visible = false;

                            sToolStripMenuItem.Visible = true;
                            清除关联ToolStripMenuItem.Visible = true;
                            tmjxt.Visible = true;
                            关联设备ToolStripMenuItem.Visible = true;
                            
                            saveImg.Visible = true;
                        }

                    }
                    if (tlVectorControl1.SVGDocument.CurrentElement != null && tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline")
                    {
                        mUpdateMenuItem.Visible = true;
                        tmloctaionjxt.Visible = true;
                        printToolStripMenuItem.Visible = false;
                        toolDel.Visible = true;
                        sToolStripMenuItem.Visible = true;
                        清除关联ToolStripMenuItem.Visible = true;
                        tmjxt.Visible = true;
                        关联设备ToolStripMenuItem.Visible = true;
                        SubToolStripMenuItem.Visible = true;
                        saveImg.Visible = true;
                        toolrelanalyst.Visible = true;
                    }
                    else
                    {
                        mUpdateMenuItem.Visible = false;
                        tmloctaionjxt.Visible = false;
                        toolrelanalyst.Visible = false;

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
                    if (tlVectorControl1.Operation == ToolOperation.InterEnclosure && bdz_xz == "yes")
                    {
                        D_TIN.Clear();
                        ArrayList polylist = new ArrayList();
                        ArrayList useList = new ArrayList();
                        System.Collections.SortedList list = new SortedList();
                        ItopVector.Core.SvgElementCollection selCol = tlVectorControl1.SVGDocument.SelectCollection;
                        int t = 0;
                    Lab009:
                        t = t + 1;
                        XmlElement poly1 = (XmlElement)selCol[selCol.Count - t];
                        if (poly1.GetType().FullName != "ItopVector.Core.Figure.Polygon")
                        {
                            goto Lab009;
                        }

                        SvgElement se = null;
                        for (int i = 0; i < selCol.Count; i++)
                        {
                            string tem = selCol[i].GetType().FullName;
                            if (tem == "ItopVector.Core.Figure.Polygon")
                            {

                                string IsArea = ((XmlElement)selCol[i]).GetAttribute("IsArea");
                                if (IsArea == "")
                                {
                                    se = (SvgElement)selCol[i];   //大范围
                                }
                                else
                                {
                                    polylist.Add((SvgElement)selCol[i]);
                                }
                            }
                            if (tem == "ItopVector.Core.Figure.Use")
                            {
                                useList.Add((SvgElement)selCol[i]);
                            }
                        }
                        decimal sumss = 0;
                        for (int m = 0; m < polylist.Count; m++)
                        {
                            XmlElement _x = (XmlElement)polylist[m];

                            string sid = _x.GetAttribute("id");
                            glebeProperty pl = new glebeProperty();
                            pl.EleID = sid;
                            pl.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            pl = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", pl);
                            if (pl != null)
                            {
                                sumss = sumss + pl.Burthen;//区域负荷
                               pl.ObligateField7 = "";  //将区域内的地块顺序全部清空
                               Services.BaseService.Update<glebeProperty>(pl);

                            }
                            //将其加入一个规划和非规划的标志
                            _x.SetAttribute("xz","0");
                            _x.SetAttribute("Burthen", pl.Burthen.ToString());
                            _x.SetAttribute("glbdz", "");  //为 （_sub1.EleID，多少负荷）；
                        }
                        double sumSub = 0;
                        for (int m = 0; m < useList.Count; m++)
                        {
                            XmlElement _x = (XmlElement)useList[m];

                            string sid = _x.GetAttribute("Deviceid");
                            PSP_Substation_Info pl = new PSP_Substation_Info();
                            pl.UID = sid;
                            //pl.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            pl = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoByKey", pl);
                            if (pl != null)
                            {
                                sumSub = sumSub + pl.L2;  //现有的容量
                            }
                            _x.SetAttribute("rl",pl.L2.ToString());
                        }
                        ArrayList extsublist = new ArrayList();
                         //创建新的图层放置显示的链接线路
                        Layer lar = null;
                        string FileName = "变电站地块连接线";
                        if (Layer.CkLayerExist(FileName, tlVectorControl1.SVGDocument))
                        {
                            ArrayList layercol = tlVectorControl1.SVGDocument.getLayerList();
                            for (int i = 0; i < layercol.Count; i++)
                            {
                                if (FileName == (layercol[i] as Layer).GetAttribute("label"))
                                {
                                    lar = (Layer)layercol[i];
                                    break;
                                }
                            }
                        }
                        else
                        {
                            lar = Layer.CreateNew(FileName, tlVectorControl1.SVGDocument);

                            lar.SetAttribute("layerType", progtype);
                            lar.SetAttribute("ParentID", tlVectorControl1.SVGDocument.CurrentLayer.GetAttribute("ParentID"));
                            //this.frmlar.checkedListBox1.SelectedIndex = -1;
                            //this.frmlar.checkedListBox1.Items.Add(lar, true);
                        }

                        FrmSet f_set = new FrmSet();
                        f_set.s = sumss;
                        f_set.sub_s = sumSub;
                        if (f_set.ShowDialog() == DialogResult.OK)
                        {
                            str_dy = f_set.Str_dj;   //电压等级 
                            str_num = f_set.Str_num;  //所需建的数目
                            str_jj = f_set.Str_jj;//变电站最小距离
                            if (!string.IsNullOrEmpty(f_set.Str_rzb))
                            {
                                dbl_rzb = Convert.ToDouble(f_set.Str_rzb);
                            }
                            if (!string.IsNullOrEmpty(f_set.Str_rl))
                            {
                                dbl_rl= Convert.ToDouble(f_set.Str_rl);
                            }
                            
                            tlVectorControl1.SVGDocument.SelectCollection.Clear();

                            if (Convert.ToDecimal(str_num) < 0)
                            {
                                MessageBox.Show("区域内供电满足要求，不需要新建变电站。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            Polygon pp = se as Polygon;

                            if (useList.Count < 3)
                            {
                                for (int i = 0; i < pp.Points.Length; i++)
                                {
                                    //加点            
                                    D_TIN.DS.Vertex[D_TIN.DS.VerticesNum].x = (long)pp.Points[i].X;
                                    D_TIN.DS.Vertex[D_TIN.DS.VerticesNum].y = (long)pp.Points[i].Y;
                                    D_TIN.DS.Vertex[D_TIN.DS.VerticesNum].ID = D_TIN.DS.VerticesNum;
                                    D_TIN.DS.vetexboundary[D_TIN.DS.VerticesNum].pointid = D_TIN.DS.VerticesNum;//填写各个节点泰森多边形影响区域
                                    D_TIN.DS.VerticesNum++;
                                }
                            }
                            else
                            {
                                for (int m = 0; m < useList.Count; m++)
                                {
                                    Use _p = useList[m] as Use;
                                    string str_t = _p.GetAttribute("xlink:href");

                                    PointF of = TLMath.getUseOffset(str_t);
                                    PointF p1 = new PointF(_p.X, _p.Y);
                                    PointF[] pnt = new PointF[1];
                                    pnt[0] = p1;
                                    _p.Transform.Matrix.TransformPoints(pnt);
                                    D_TIN.DS.Vertex[D_TIN.DS.VerticesNum].x = (long)pnt[0].X + (long)of.X;
                                    D_TIN.DS.Vertex[D_TIN.DS.VerticesNum].y = (long)pnt[0].Y + (long)of.Y;
                                    D_TIN.DS.Vertex[D_TIN.DS.VerticesNum].ID = D_TIN.DS.VerticesNum;
                                    D_TIN.DS.vetexboundary[D_TIN.DS.VerticesNum].pointid = D_TIN.DS.VerticesNum;//填写各个节点泰森多边形影响区域
                                    D_TIN.DS.VerticesNum++;
                                }
                            }
                            if ((Convert.ToInt32(str_num) > 1) && (sumss == 0) && (useList.Count > Convert.ToInt32(str_num) + 2))
                            {
                                if ((MessageBox.Show("区域内负荷为0，无法进行负荷分布自动选址。\r\n 是否进行最小覆盖圆自动选址？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information)) == DialogResult.Yes)
                                {
                                   
                                    int n = D_TIN.DS.VerticesNum;
                                    D_TIN.DS.set_cnt = D_TIN.DS.VerticesNum; D_TIN.DS.pos_cnt = 0;
                                    for (int i = 0; i < n; i++)
                                        D_TIN.DS.curset[i] = i;

                                    D_TIN.mindisk();
                                    Pen p2 = new Pen(Color.Red, 2);
                                    Rectangle rec = new Rectangle((int)(D_TIN.DS.maxcic.x - D_TIN.DS.radius), (int)(D_TIN.DS.maxcic.y - D_TIN.DS.radius), (int)(2 * D_TIN.DS.radius), (int)(2 * D_TIN.DS.radius));
                                    string str_sub = getSubName(str_dy);
                                    PointF pf = getOff(str_dy);

                                    XmlElement e0 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;
                                    e0.SetAttribute("x", Convert.ToString(D_TIN.DS.maxcic.x - pf.X));
                                    e0.SetAttribute("y", Convert.ToString(D_TIN.DS.maxcic.y - pf.Y));
                                    e0.SetAttribute("xzflag", "1");
                                    e0.SetAttribute("xlink:href", str_sub);
                                    e0.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                    e0.SetAttribute("layer", SvgDocument.currentLayer);
                                    e0.SetAttribute("subname", "1号");
                                    e0.SetAttribute("rl", dbl_rl.ToString());
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(e0);
                                    tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)e0);
                                    //获得此变电站的最小半径的圆 生成辐射线
                                    XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                                    n1.SetAttribute("cx", e0.GetAttribute("x").ToString());
                                    n1.SetAttribute("cy", e0.GetAttribute("y").ToString());
                                    n1.SetAttribute("r", (TLMath.getdcNumber(Convert.ToDecimal(str_jj), tlVectorControl1.ScaleRatio)).ToString());
                                  
                                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                                    n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                                    SubandFHcollect sf = new SubandFHcollect(GetsubFhk((Circle)n1, polylist), e0);
                                    CreateSubline1(sf, true, lar);
                                    extsublist.Add(e0);
                                    //
                                    XmlElement t0 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                    t0.SetAttribute("x", Convert.ToString(D_TIN.DS.maxcic.x));
                                    t0.SetAttribute("y", Convert.ToString(D_TIN.DS.maxcic.y));

                                    t0.SetAttribute("layer", SvgDocument.currentLayer);
                                    t0.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                    t0.SetAttribute("font-famliy", "宋体");
                                    t0.SetAttribute("font-size", "48");
                                    t0.InnerText = "1号";
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(t0);

                                    PSP_SubstationSelect s = new PSP_SubstationSelect();
                                    s.UID = Guid.NewGuid().ToString();
                                    s.EleID = e0.GetAttribute("id");
                                    s.SName = "1号";
                                    s.Remark = "";
                                    s.col2 = XZ_bdz;
                                    s.SvgID = tlVectorControl1.SVGDocument.SvgdataUid;
                                    Services.BaseService.Create<PSP_SubstationSelect>(s);

                                    decimal c = 360 / (Convert.ToInt32(str_num));
                                    for (int k = 2; k <= Convert.ToInt32(str_num); k++)
                                    {

                                        decimal x = Convert.ToDecimal(D_TIN.DS.maxcic.x + D_TIN.DS.radius * Math.Cos(Convert.ToDouble(c * k) * Math.PI / 180));
                                        decimal y = Convert.ToDecimal(D_TIN.DS.maxcic.y + D_TIN.DS.radius * Math.Sin(Convert.ToDouble(c * k) * Math.PI / 180));

                                        XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;
                                        e1.SetAttribute("x", Convert.ToString(Convert.ToSingle(x) - pf.X));
                                        e1.SetAttribute("y", Convert.ToString(Convert.ToSingle(y) - pf.Y));
                                        e1.SetAttribute("xzflag", "1");
                                        e1.SetAttribute("xlink:href", str_sub);
                                        e1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                        e1.SetAttribute("layer", SvgDocument.currentLayer);
                                        e1.SetAttribute("subname", Convert.ToString(k) + "号");
                                        e1.SetAttribute("rl", dbl_rl.ToString());
                                        tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);
                                        tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)e1);
                                        //获得此变电站的最小半径的圆 生成辐射线
                                        n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                                        n1.SetAttribute("cx", e1.GetAttribute("x").ToString());
                                        n1.SetAttribute("cy", e1.GetAttribute("y").ToString());
                                        n1.SetAttribute("r", (TLMath.getdcNumber(Convert.ToDecimal(str_jj), tlVectorControl1.ScaleRatio)).ToString());

                                        n1.SetAttribute("layer", SvgDocument.currentLayer);
                                        n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                                         sf = new SubandFHcollect(GetsubFhk((Circle)n1, polylist), e1);
                                         CreateSubline1(sf, true, lar);
                                        extsublist.Add(e1);
                                        //
                                        PSP_SubstationSelect s2 = new PSP_SubstationSelect();
                                        s2.UID = Guid.NewGuid().ToString();
                                        s2.EleID = e1.GetAttribute("id");
                                        s2.SName = Convert.ToString((k)) + "号";
                                        s2.Remark = "";
                                        s2.col2 = XZ_bdz;
                                        s2.SvgID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        Services.BaseService.Create<PSP_SubstationSelect>(s2);


                                        XmlElement t1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                        t1.SetAttribute("x", Convert.ToString(x));
                                        t1.SetAttribute("y", Convert.ToString(y));

                                        t1.SetAttribute("layer", SvgDocument.currentLayer);
                                        t1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                        t1.SetAttribute("font-famliy", "宋体");
                                        t1.SetAttribute("font-size", "54");
                                        t1.InnerText = Convert.ToString((k)) + "号";
                                        tlVectorControl1.SVGDocument.RootElement.AppendChild(t1);
                                    }
                                    //给已有变电站创建
                                    for (int m = 0; m< useList.Count; m++)
                                    {
                                        Use _p = useList[m] as Use;
                                        (useList[m] as XmlElement).SetAttribute("ghyk", "1");
                                        string str_t = _p.GetAttribute("xlink:href");

                                        PointF of = TLMath.getUseOffset(str_t);
                                        PointF p1 = new PointF(_p.X, _p.Y);
                                        PointF[] pnt = new PointF[1];
                                        pnt[0] = p1;
                                        _p.Transform.Matrix.TransformPoints(pnt);
                                        XmlElement e2 = (XmlElement)useList[m];
                                        PSP_Substation_Info _sub1 = new PSP_Substation_Info();


                                        XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                                        n2.SetAttribute("cx", ((long)pnt[0].X + (long)of.X).ToString());
                                        n2.SetAttribute("cy", ((long)pnt[0].Y + (long)of.Y).ToString());
                                        n2.SetAttribute("r", (TLMath.getdcNumber(Convert.ToDecimal(str_jj), tlVectorControl1.ScaleRatio)).ToString());

                                        n2.SetAttribute("layer", SvgDocument.currentLayer);
                                        n2.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                                        _sub1.EleID = e2.GetAttribute("id"); ;
                                        _sub1.AreaID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        _sub1 = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoListByEleID", _sub1);
                                        if (_sub1 != null)
                                        {
                                            n2.SetAttribute("subname", _sub1.Title);
                                            n2.SetAttribute("id", _sub1.EleID);
                                            n2.SetAttribute("rl", _sub1.L2.ToString());
                                        }

                                        SubandFHcollect sf1 = new SubandFHcollect(GetsubFhk((Circle)n1, polylist), n2);
                                        CreateSubline1(sf1, false, lar);
                                        extsublist.Add(n2);
                                    }
                                    Extbdzreport(extsublist);
                                    return;
                                }
                            }
                            if ((Convert.ToInt32(str_num) > 1) && (useList.Count < Convert.ToInt32(str_num) + 2))
                            {
                                if ((MessageBox.Show("当自动选址变电站个数等于" + str_num + "时，区域内必须至少有" + Convert.ToString(Convert.ToInt32(str_num) + 2) + "座已有变电站，否则无法进行负荷分布自动选址。\r\n 是否进行最小覆盖圆自动选址？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information)) == DialogResult.Yes)
                                {

                                    int n = D_TIN.DS.VerticesNum;
                                    D_TIN.DS.set_cnt = D_TIN.DS.VerticesNum; D_TIN.DS.pos_cnt = 0;
                                    for (int i = 0; i < n; i++)
                                        D_TIN.DS.curset[i] = i;

                                    D_TIN.mindisk();
                                    Pen p2 = new Pen(Color.Red, 2);
                                    Rectangle rec = new Rectangle((int)(D_TIN.DS.maxcic.x - D_TIN.DS.radius), (int)(D_TIN.DS.maxcic.y - D_TIN.DS.radius), (int)(2 * D_TIN.DS.radius), (int)(2 * D_TIN.DS.radius));
                                    string str_sub = getSubName(str_dy);

                                    //XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                                    //// Point point1 = tlVectorControl1.PointToView(new Point((int)D_TIN.DS.maxcic.x, (int)D_TIN.DS.maxcic.y));
                                    //n1.SetAttribute("cx", D_TIN.DS.maxcic.x.ToString());
                                    //n1.SetAttribute("cy", D_TIN.DS.maxcic.y.ToString());
                                    //n1.SetAttribute("r", D_TIN.DS.radius.ToString());
                                    //n1.SetAttribute("r", D_TIN.DS.radius.ToString());
                                    //n1.SetAttribute("layer", SvgDocument.currentLayer);
                                    //n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                                    //tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                    //str_sub = getSubName(str_dy);
                                    PointF pf = getOff(str_dy);

                                    XmlElement e0 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;
                                    e0.SetAttribute("x", Convert.ToString(D_TIN.DS.maxcic.x - pf.X));
                                    e0.SetAttribute("y", Convert.ToString(D_TIN.DS.maxcic.y - pf.Y));
                                    e0.SetAttribute("xzflag", "1");
                                    e0.SetAttribute("xlink:href", str_sub);
                                    e0.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                    e0.SetAttribute("layer", SvgDocument.currentLayer);
                                    e0.SetAttribute("subname","1号");
                                    e0.SetAttribute("rl", dbl_rl.ToString());
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(e0);
                                    tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)e0);
                                    //获得此变电站的最小半径的圆 生成辐射线
                                    XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                                    n1.SetAttribute("cx", e0.GetAttribute("x").ToString());
                                    n1.SetAttribute("cy", e0.GetAttribute("y").ToString());
                                    n1.SetAttribute("r", (TLMath.getdcNumber(Convert.ToDecimal(str_jj), tlVectorControl1.ScaleRatio)).ToString());

                                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                                    n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                                    SubandFHcollect sf = new SubandFHcollect(GetsubFhk((Circle)n1, polylist), e0);
                                    CreateSubline1(sf, true, lar);
                                    extsublist.Add(e0);
                                    //
                                    XmlElement t0 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                    t0.SetAttribute("x", Convert.ToString(D_TIN.DS.maxcic.x));
                                    t0.SetAttribute("y", Convert.ToString(D_TIN.DS.maxcic.y));

                                    t0.SetAttribute("layer", SvgDocument.currentLayer);
                                    t0.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                    t0.SetAttribute("font-famliy", "宋体");
                                    t0.SetAttribute("font-size", "48");
                                    t0.InnerText = "1号";
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(t0);

                                    PSP_SubstationSelect s = new PSP_SubstationSelect();
                                    s.UID = Guid.NewGuid().ToString();
                                    s.EleID = e0.GetAttribute("id");
                                    s.SName = "1号";
                                    s.Remark = "";
                                    s.col2 = XZ_bdz;
                                    s.SvgID = tlVectorControl1.SVGDocument.SvgdataUid;
                                    Services.BaseService.Create<PSP_SubstationSelect>(s);

                                    decimal c = 360 / (Convert.ToInt32(str_num));
                                    for (int k = 2; k <= Convert.ToInt32(str_num); k++)
                                    {

                                        decimal x = Convert.ToDecimal(D_TIN.DS.maxcic.x + D_TIN.DS.radius * Math.Cos(Convert.ToDouble(c * k) * Math.PI / 180));
                                        decimal y = Convert.ToDecimal(D_TIN.DS.maxcic.y + D_TIN.DS.radius * Math.Sin(Convert.ToDouble(c * k) * Math.PI / 180));

                                        XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;
                                        e1.SetAttribute("x", Convert.ToString(Convert.ToSingle(x) - pf.X));
                                        e1.SetAttribute("y", Convert.ToString(Convert.ToSingle(y) - pf.Y));
                                        e1.SetAttribute("xzflag", "1");
                                        e1.SetAttribute("xlink:href", str_sub);
                                        e1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                        e1.SetAttribute("layer", SvgDocument.currentLayer);
                                        e1.SetAttribute("subname", Convert.ToString(k) + "号");
                                        e1.SetAttribute("rl", dbl_rl.ToString());
                                        tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);
                                        tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)e1);
                                        //获得此变电站的最小半径的圆 生成辐射线
                                          n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                                        n1.SetAttribute("cx", e1.GetAttribute("x").ToString());
                                        n1.SetAttribute("cy", e1.GetAttribute("y").ToString());
                                        n1.SetAttribute("r", (TLMath.getdcNumber(Convert.ToDecimal(str_jj), tlVectorControl1.ScaleRatio)).ToString());

                                        n1.SetAttribute("layer", SvgDocument.currentLayer);
                                        n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                                       sf = new SubandFHcollect(GetsubFhk((Circle)n1, polylist), e1);
                                       CreateSubline1(sf, true, lar);
                                        extsublist.Add(e1);
                                        //
                                        PSP_SubstationSelect s2 = new PSP_SubstationSelect();
                                        s2.UID = Guid.NewGuid().ToString();
                                        s2.EleID = e1.GetAttribute("id");
                                        s2.SName = Convert.ToString((k)) + "号";
                                        s2.Remark = "";
                                        s2.col2 = XZ_bdz;
                                        s2.SvgID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        Services.BaseService.Create<PSP_SubstationSelect>(s2);


                                        XmlElement t1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                        t1.SetAttribute("x", Convert.ToString(x));
                                        t1.SetAttribute("y", Convert.ToString(y));

                                        t1.SetAttribute("layer", SvgDocument.currentLayer);
                                        t1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                        t1.SetAttribute("font-famliy", "宋体");
                                        t1.SetAttribute("font-size", "54");
                                        t1.InnerText = Convert.ToString((k)) + "号";
                                        tlVectorControl1.SVGDocument.RootElement.AppendChild(t1);
                                    }
                                    //给已有变电站创建
                                    for (int m = 0; m < useList.Count; m++)
                                    {
                                        Use _p = useList[m] as Use;
                                        (useList[m] as XmlElement).SetAttribute("ghyk", "1");
                                        string str_t = _p.GetAttribute("xlink:href");

                                        PointF of = TLMath.getUseOffset(str_t);
                                        PointF p1 = new PointF(_p.X, _p.Y);
                                        PointF[] pnt = new PointF[1];
                                        pnt[0] = p1;
                                        _p.Transform.Matrix.TransformPoints(pnt);
                                        XmlElement e2 = (XmlElement)useList[m];
                                        PSP_Substation_Info _sub1 = new PSP_Substation_Info();


                                        XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                                        n2.SetAttribute("cx", ((long)pnt[0].X + (long)of.X).ToString());
                                        n2.SetAttribute("cy", ((long)pnt[0].Y + (long)of.Y).ToString());
                                        n2.SetAttribute("r", (TLMath.getdcNumber(Convert.ToDecimal(str_jj), tlVectorControl1.ScaleRatio)).ToString());

                                        n2.SetAttribute("layer", SvgDocument.currentLayer);
                                        n2.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                                        _sub1.EleID = e2.GetAttribute("id"); ;
                                        _sub1.AreaID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        _sub1 = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoListByEleID", _sub1);
                                        if (_sub1 != null)
                                        {
                                            n2.SetAttribute("subname", _sub1.Title);
                                            n2.SetAttribute("id", _sub1.EleID);
                                            n2.SetAttribute("rl", _sub1.L2.ToString());
                                        }

                                        SubandFHcollect sf1 = new SubandFHcollect(GetsubFhk((Circle)n1, polylist), n2);
                                        CreateSubline1(sf1, false, lar);
                                        extsublist.Add(n2);
                                    }
                                    Extbdzreport(extsublist);
                                    return;
                                }
                                else
                                {
                                    bdz_xz = "";
                                    return;
                                }
                            }


                            if (str_num == "1" && useList.Count < 3)
                            {
                                int n = D_TIN.DS.VerticesNum;
                                D_TIN.DS.set_cnt = D_TIN.DS.VerticesNum; D_TIN.DS.pos_cnt = 0;
                                for (int i = 0; i < n; i++)
                                    D_TIN.DS.curset[i] = i;

                                D_TIN.mindisk();
                                Pen p2 = new Pen(Color.Red, 2);
                                Rectangle rec = new Rectangle((int)(D_TIN.DS.maxcic.x - D_TIN.DS.radius), (int)(D_TIN.DS.maxcic.y - D_TIN.DS.radius), (int)(2 * D_TIN.DS.radius), (int)(2 * D_TIN.DS.radius));

                                // g.DrawEllipse(p2, rec);
                                //string ele_uid = "";
                                string str_sub = getSubName(str_dy);

                                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                                // Point point1 = tlVectorControl1.PointToView(new Point((int)D_TIN.DS.maxcic.x, (int)D_TIN.DS.maxcic.y));
                                n1.SetAttribute("cx", D_TIN.DS.maxcic.x.ToString());
                                n1.SetAttribute("cy", D_TIN.DS.maxcic.y.ToString());
                                n1.SetAttribute("r", D_TIN.DS.radius.ToString());
                                n1.SetAttribute("r", D_TIN.DS.radius.ToString());
                                n1.SetAttribute("layer", SvgDocument.currentLayer);
                              ;
                                n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);

                                
                                PointF pf = getOff(str_dy);
                                XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;
                                e1.SetAttribute("x", Convert.ToString(D_TIN.DS.maxcic.x - pf.X));
                                e1.SetAttribute("y", Convert.ToString(D_TIN.DS.maxcic.y - pf.Y));
                                e1.SetAttribute("xzflag", "1");
                                e1.SetAttribute("xlink:href", str_sub);
                                e1.SetAttribute("rl", dbl_rl.ToString());
                                e1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                e1.SetAttribute("layer", SvgDocument.currentLayer);
                                e1.SetAttribute("subname", "1号");
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);
                                tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)e1);
                                //获得此变电站的最小半径的圆 生成辐射线
                               
                                SubandFHcollect sf = new SubandFHcollect(GetsubFhk((Circle)n1, polylist), e1);
                                CreateSubline1(sf, true, lar);
                                extsublist.Add(e1);
                                //
                                PSP_SubstationSelect s2 = new PSP_SubstationSelect();
                                s2.UID = Guid.NewGuid().ToString();
                                s2.EleID = e1.GetAttribute("id");
                                s2.SName = "1号";
                                s2.Remark = "";
                                s2.col2 = XZ_bdz;
                                s2.SvgID = tlVectorControl1.SVGDocument.SvgdataUid;
                                Services.BaseService.Create<PSP_SubstationSelect>(s2);

                                tlVectorControl1.Refresh();
                                // Thread.Sleep(3);
                                tlVectorControl1.SVGDocument.RootElement.RemoveChild(n1);

                                XmlElement t1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                t1.SetAttribute("x", Convert.ToString(D_TIN.DS.maxcic.x));
                                t1.SetAttribute("y", Convert.ToString(D_TIN.DS.maxcic.y));

                                t1.SetAttribute("layer", SvgDocument.currentLayer);
                                t1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                t1.SetAttribute("font-famliy", "宋体");
                                t1.SetAttribute("font-size", "54");
                                t1.InnerText = "1号";
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(t1);


                                bdz_xz = "";
                                //给已有变电站创建
                                for (int m= 0; m < useList.Count; m++)
                                {
                                    Use _p = useList[m] as Use;
                                    (useList[m] as XmlElement).SetAttribute("ghyk", "1");
                                    string str_t = _p.GetAttribute("xlink:href");

                                    PointF of = TLMath.getUseOffset(str_t);
                                    PointF p1 = new PointF(_p.X, _p.Y);
                                    PointF[] pnt = new PointF[1];
                                    pnt[0] = p1;
                                    _p.Transform.Matrix.TransformPoints(pnt);
                                    XmlElement e2 = (XmlElement)useList[m];
                                    PSP_Substation_Info _sub1 = new PSP_Substation_Info();


                                    XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                                    n2.SetAttribute("cx", ((long)pnt[0].X + (long)of.X).ToString());
                                    n2.SetAttribute("cy", ((long)pnt[0].Y + (long)of.Y).ToString());
                                    n2.SetAttribute("r", (TLMath.getdcNumber(Convert.ToDecimal(str_jj), tlVectorControl1.ScaleRatio)).ToString());

                                    n2.SetAttribute("layer", SvgDocument.currentLayer);
                                    n2.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                                    _sub1.EleID = e2.GetAttribute("id"); ;
                                    _sub1.AreaID = tlVectorControl1.SVGDocument.SvgdataUid;
                                    _sub1 = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoListByEleID", _sub1);
                                    if (_sub1 != null)
                                    {
                                        n2.SetAttribute("subname", _sub1.Title);
                                        n2.SetAttribute("id", _sub1.EleID);
                                        n2.SetAttribute("rl", _sub1.L2.ToString());
                                    }

                                    SubandFHcollect sf1 = new SubandFHcollect(GetsubFhk((Circle)n1, polylist), n2);
                                    CreateSubline1(sf1, false,lar);
                                    extsublist.Add(n2);
                                   
                                }
                                Extbdzreport(extsublist);
                                return;
                            }
                            else
                            {
                               // ShowTriangle(polylist, poly1);    //王哥写的
                                ShowTriangle1(polylist, poly1,ref extsublist,lar);
                                bdz_xz = "";
                                //给已有变电站创建
                                for (int m = 0; m < useList.Count; m++)
                                {
                                    Use _p = useList[m] as Use;
                                    (useList[m] as XmlElement).SetAttribute("ghyk", "1");
                                    string str_t = _p.GetAttribute("xlink:href");

                                    PointF of = TLMath.getUseOffset(str_t);
                                    PointF p1 = new PointF(_p.X, _p.Y);
                                    PointF[] pnt = new PointF[1];
                                    pnt[0] = p1;
                                    _p.Transform.Matrix.TransformPoints(pnt);
                                    XmlElement e1 = (XmlElement)useList[m];
                                    PSP_Substation_Info _sub1 = new PSP_Substation_Info();

                                   
                                    XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                                    n1.SetAttribute("cx", ((long)pnt[0].X + (long)of.X).ToString());
                                    n1.SetAttribute("cy", ((long)pnt[0].Y + (long)of.Y).ToString());
                                    n1.SetAttribute("r", (TLMath.getdcNumber(Convert.ToDecimal(str_jj), tlVectorControl1.ScaleRatio)).ToString());

                                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                                    n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                                    _sub1.EleID = e1.GetAttribute("id"); ;
                                    _sub1.AreaID = tlVectorControl1.SVGDocument.SvgdataUid;
                                    _sub1 = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoListByEleID", _sub1);
                                    if (_sub1 != null)
                                    {
                                        n1.SetAttribute("subname", _sub1.Title);
                                        n1.SetAttribute("id", _sub1.EleID);
                                        n1.SetAttribute("rl", _sub1.L2.ToString());
                                    }

                                    SubandFHcollect sf = new SubandFHcollect(GetsubFhk((Circle)n1, polylist), n1);
                                    CreateSubline1(sf,false,lar);
                                    extsublist.Add(n1);
                                }
                                Extbdzreport(extsublist);
                                return;
                            }
                           
                        }
                        else
                        {
                            bdz_xz = "";
                            return;
                        }
                      
                    }
                    if (tlVectorControl1.Operation == ToolOperation.InterEnclosure && !SubPrint)
                    {

                        System.Collections.SortedList list = new SortedList();
                        decimal s = 0;
                        ItopVector.Core.SvgElementCollection selCol = tlVectorControl1.SVGDocument.SelectCollection;
                        if (selCol.Count > 1)
                        {
                            decimal ViewScale = 1;
                            string str_Scale = Convert.ToString(tlVectorControl1.ScaleRatio);//SVGDocument.getViewScale();
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

                                        decimal sub_s = TLMath.getInterPolygonArea(region, rect, 1);
                                        sub_s = TLMath.getNumber2(sub_s, tlVectorControl1.ScaleRatio) / Convert.ToDecimal(areaoption);
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

                                    // PSP_Substation_Info _sub1 = new PSP_Substation_Info();
                                    //// substation _sub1 = new substation();
                                    // _sub1.EleID = str_id;
                                    // _sub1.AreaID = tlVectorControl1.SVGDocument.SvgdataUid;
                                    // _sub1 = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoListByEleID", _sub1);
                                    // if (_sub1 != null)
                                    // {
                                    //     _sub1.glebeEleID = guid;
                                    //     Services.BaseService.Update<PSP_Substation_Info>( _sub1);
                                    // }

                                }

                            }
                            decimal nullpoly = TLMath.getNumber2(TLMath.getPolygonArea(TLMath.getPolygonPoints(poly1), 1), tlVectorControl1.ScaleRatio) / Convert.ToDecimal(areaoption) - s;

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
                MessageBox.Show(e1.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tlVectorControl1.SVGDocument.SelectCollection.Clear();
            }
            finally
            {
                tlVectorControl1.Operation = ToolOperation.Select;
                tlVectorControl1.Operation = ToolOperation.FreeTransform;

            }
        }
        public PointF getOff(string str_dy)
        {
            string ele_uid = getSubName(str_dy); ;

            PointF pf = TLMath.getUseOffset(ele_uid);
            return pf;
        }
        public string getDY(string str)
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
            {
                return "0";
            }
        }
        public string getSubName(string str_dy)
        {
            string str_sub = "";
            if (str_dy == "500")
            {
                str_sub = "#gh-Substation500-28";
            }
            else if (str_dy == "220")
            {
                //str_sub = "#gh-Substation220-48";
                str_sub = "#Substation220-1412";
            }
            else if (str_dy == "110")
            {
                //str_sub = "#gh-Substation110-61";
                str_sub = "#Substation110-1422";
            }
            else if (str_dy == "66")
            {
                str_sub = "#gh-Substation66-1";
            }
            else if (str_dy == "35")
            {
                //str_sub = "#gh-Substation35-51";
                str_sub = "#Substation35-1622";
            }
            return str_sub;
        }
        public string getHCSubName(string str_dy)
        {
            string str_sub = "";
            if (str_dy == "500")
            {
                str_sub = "#Substation500-27";
            }
            else if (str_dy == "220")
            {
                str_sub = "#Substation220-45";
            }
            else if (str_dy == "110")
            {
                str_sub = "#Substation110-58";
            }
            else if (str_dy == "66")
            {
                str_sub = "#Substation35-52";
            }
            else if (str_dy == "35")
            {
                str_sub = "#Substation35-52";
            }
            return str_sub;
        }
#region  原先的选址方法
 private void ShowTriangle(ArrayList _polylist, XmlElement _poly)
        {
            string aaa = yearID;
            if (D_TIN.DS.VerticesNum > 2)  //构建三角网
                D_TIN.CreateTIN();

            //输出三角形
            for (int i = 0; i < D_TIN.DS.TriangleNum; i++)
            {
                Point point1 = new Point(Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V1Index].x), Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V1Index].y));
                Point point2 = new Point(Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V2Index].x), Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V2Index].y));
                Point point3 = new Point(Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V3Index].x), Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V3Index].y));

                string str_points = "";
                str_points = point1.X.ToString() + " " + point1.Y.ToString() + "," + point2.X.ToString() + " " + point2.Y.ToString() + "," + point3.X.ToString() + " " + point3.Y.ToString();
                //XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as XmlElement;
                //e1.SetAttribute("points", str_points);
                //e1.SetAttribute("style", "stroke-width:1;stroke:#0000FF;stroke-opacity:1;");
                //e1.SetAttribute("IsTin", "1");
                //e1.SetAttribute("layer", SvgDocument.currentLayer);
                //tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);

            }
            D_TIN.CalculateBC();
            D_TIN.CreateVoronoi();

            ArrayList CirList = new ArrayList();
            ArrayList polyCentriodList = new ArrayList();
            for (int n = 0; n < D_TIN.DS.Barycenters.Length; n++)
            {

                Barycenter bar = D_TIN.DS.Barycenters[n];
                if (bar.X == 0 && bar.Y == 0)
                {
                    break;
                }
                Triangle tri = D_TIN.DS.Triangle[n];
                Vertex ver = D_TIN.DS.Vertex[tri.V1Index];
                Vertex ver2 = D_TIN.DS.Vertex[tri.V2Index];
                Vertex ver3 = D_TIN.DS.Vertex[tri.V3Index];
                Decimal r = Convert.ToDecimal(Math.Abs(Math.Sqrt(Math.Pow(Convert.ToDouble(ver.x - bar.X), 2.0) + Math.Pow(Convert.ToDouble(ver.y - bar.Y), 2.0))));

                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                n1.SetAttribute("cx", bar.X.ToString());
                n1.SetAttribute("cy", bar.Y.ToString());
                n1.SetAttribute("r", r.ToString());
                n1.SetAttribute("r", r.ToString());
                n1.SetAttribute("layer", SvgDocument.currentLayer);
                n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                //tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                CirList.Add(n1);

                PointF[] Flist = new PointF[3];
                Flist[0] = new PointF(ver.x, ver.y);
                Flist[1] = new PointF(ver2.x, ver2.y);
                Flist[2] = new PointF(ver3.x, ver3.y);
                polyCentriodList.Add(TLMath.polyCentriod(Flist));
            }
            System.Collections.SortedList clist = new SortedList();
            decimal sum = 0;
            for (int n = 0; n < CirList.Count; n++)
            {
                int k = 0;
                Circle cir = (Circle)CirList[n];
                GraphicsPath gr1 = new GraphicsPath();
                gr1.AddPath(cir.GPath, true);
                gr1.CloseFigure();
                for (int m = 0; m < _polylist.Count; m++)
                {
                    XmlElement _x = (XmlElement)_polylist[m];
                    PointF _f = TLMath.polyCentriod(_x);
                    if (gr1.IsVisible(_f))    //外接圆包括那些负荷中心点
                    {
                        k = k + 1;   //求和的过程
                        string sid = _x.GetAttribute("id");
                        glebeProperty pl = new glebeProperty();
                        pl.EleID = sid;
                        pl.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        pl = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", pl);
                        if (pl != null)
                        {
                            sum = sum + pl.Burthen;
                        }
                    }
                }
                clist.Add(sum + n, cir);  
                string aa = "";

            }

            string str_sub = getSubName(str_dy);


            for (int k = 0; k < Convert.ToInt32(str_num); k++)
            {
                GraphicsPath gr1 = new GraphicsPath();
                gr1.AddPolygon(TLMath.getPolygonPoints(_poly));
                gr1.CloseFigure();

                Circle c1 = clist.GetByIndex(k) as Circle;
                PointF pf = TLMath.getUseOffset(str_sub);
                float X = 0f;
                float Y = 0f;
                float ox = 0f;
                float oy = 0f;
                if (gr1.IsVisible(new PointF(c1.CX, c1.CY)))
                {
                    X = c1.CenterPoint.X - pf.X;
                    Y = c1.CenterPoint.Y - pf.Y;
                    ox = c1.CenterPoint.X + 8;
                    oy = c1.CenterPoint.Y;
                }
                else
                {
                    X = ((PointF)polyCentriodList[k]).X - pf.X;
                    Y = ((PointF)polyCentriodList[k]).Y - pf.Y;
                    ox = ((PointF)polyCentriodList[k]).X + 8;
                    oy = ((PointF)polyCentriodList[k]).Y;
                }
                XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;

                e1.SetAttribute("x", Convert.ToString(X));
                e1.SetAttribute("y", Convert.ToString(Y));

                e1.SetAttribute("xlink:href", str_sub);
                e1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                e1.SetAttribute("layer", SvgDocument.currentLayer);
                tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);
                tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)e1);

                XmlElement t1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                t1.SetAttribute("x", Convert.ToString(ox));
                t1.SetAttribute("y", Convert.ToString(oy));

                t1.SetAttribute("layer", SvgDocument.currentLayer);
                t1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                t1.SetAttribute("font-famliy", "宋体");
                t1.SetAttribute("font-size", "54");
                t1.InnerText = Convert.ToString(k + 1) + "号";
                tlVectorControl1.SVGDocument.RootElement.AppendChild(t1);

                decimal temp1 = TLMath.getNumber((decimal)c1.R, tlVectorControl1.ScaleRatio);
                //MessageBox.Show(temp1.ToString());
                if (Convert.ToDecimal(str_jj) > temp1)
                {
                    MessageBox.Show(Convert.ToString(k + 1) + "号变电站候选站址小于最小供电半径，请手动进行调整。");
                }

                PSP_SubstationSelect s = new PSP_SubstationSelect();
                s.UID = Guid.NewGuid().ToString();
                s.EleID = t1.GetAttribute("id");
                s.SName = Convert.ToString(k + 1) + "号";
                s.Remark = "";
                s.col2 = XZ_bdz;
                s.SvgID = tlVectorControl1.SVGDocument.SvgdataUid;
                Services.BaseService.Create<PSP_SubstationSelect>(s);


            }


        }
      
private void ShowTriangle1(ArrayList _polylist, XmlElement _poly,ref ArrayList arraylist,Layer lay)
{
     try {
         Dictionary<XmlElement, PointF> fhkcollect = new Dictionary<XmlElement, PointF>();
         PointF fhcenter = new PointF();

         string aaa = yearID;
         if (D_TIN.DS.VerticesNum > 2)  //构建三角网
             D_TIN.CreateTIN();

         //输出三角形
         for (int i = 0; i < D_TIN.DS.TriangleNum; i++) {
             Point point1 = new Point(Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V1Index].x), Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V1Index].y));
             Point point2 = new Point(Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V2Index].x), Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V2Index].y));
             Point point3 = new Point(Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V3Index].x), Convert.ToInt32(D_TIN.DS.Vertex[D_TIN.DS.Triangle[i].V3Index].y));

             string str_points = "";
             str_points = point1.X.ToString() + " " + point1.Y.ToString() + "," + point2.X.ToString() + " " + point2.Y.ToString() + "," + point3.X.ToString() + " " + point3.Y.ToString();
             //XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as XmlElement;
             //e1.SetAttribute("points", str_points);
             //e1.SetAttribute("style", "stroke-width:1;stroke:#0000FF;stroke-opacity:1;");
             //e1.SetAttribute("IsTin", "1");
             //e1.SetAttribute("layer", SvgDocument.currentLayer);
             //tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);

         }
         D_TIN.CalculateBC();
         D_TIN.CreateVoronoi();

         ArrayList CirList = new ArrayList();
         ArrayList polyCentriodList = new ArrayList();
         for (int n = 0; n < D_TIN.DS.Barycenters.Length; n++) {

             Barycenter bar = D_TIN.DS.Barycenters[n];
             if (bar.X == 0 && bar.Y == 0) {
                 break;
             }
             Triangle tri = D_TIN.DS.Triangle[n];
             Vertex ver = D_TIN.DS.Vertex[tri.V1Index];
             Vertex ver2 = D_TIN.DS.Vertex[tri.V2Index];
             Vertex ver3 = D_TIN.DS.Vertex[tri.V3Index];
             Decimal r = Convert.ToDecimal(Math.Abs(Math.Sqrt(Math.Pow(Convert.ToDouble(ver.x - bar.X), 2.0) + Math.Pow(Convert.ToDouble(ver.y - bar.Y), 2.0))));

             XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
             n1.SetAttribute("cx", bar.X.ToString());
             n1.SetAttribute("cy", bar.Y.ToString());
             n1.SetAttribute("r", r.ToString());

             n1.SetAttribute("layer", SvgDocument.currentLayer);
             n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
             //tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
             CirList.Add(n1);

             PointF[] Flist = new PointF[3];
             Flist[0] = new PointF(ver.x, ver.y);
             Flist[1] = new PointF(ver2.x, ver2.y);
             Flist[2] = new PointF(ver3.x, ver3.y);
             polyCentriodList.Add(TLMath.polyCentriod(Flist));
         }
         System.Collections.SortedList clist = new SortedList();
         System.Collections.SortedList CtoFHlist = new SortedList();
         decimal sum = 0;
         for (int n = 0; n < CirList.Count; n++) {
             fhkcollect = new Dictionary<XmlElement, PointF>();
             int k = 0;
             Circle cir = (Circle)CirList[n];
             GraphicsPath gr1 = new GraphicsPath();
             gr1.AddPath(cir.GPath, true);
             gr1.CloseFigure();
             for (int m = 0; m < _polylist.Count; m++) {
                 XmlElement _x = (XmlElement)_polylist[m];
                 PointF _f = TLMath.polyCentriod(_x);
                 if (gr1.IsVisible(_f))    //外接圆包括那些负荷中心点
                    {
                     k = k + 1;   //求和的过程
                     string sid = _x.GetAttribute("id");
                     glebeProperty pl = new glebeProperty();
                     pl.EleID = sid;
                     pl.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                     pl = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", pl);
                     if (pl != null) {
                         sum = sum + pl.Burthen;
                     }
                     fhkcollect.Add(_x, _f);//记录外包圆相交的负荷块
                 }
             }
             clist.Add(sum + n, cir);
             CtoFHlist.Add(sum + n, fhkcollect);
             string aa = "";

         }

         string str_sub = getSubName(str_dy);


         for (int k = 0; k < Convert.ToInt32(str_num); k++) {
             GraphicsPath gr1 = new GraphicsPath();
             gr1.AddPolygon(TLMath.getPolygonPoints(_poly));
             gr1.CloseFigure();

             Circle c1 = clist.GetByIndex(k) as Circle;
             Dictionary<XmlElement, PointF> FHandPointF = CtoFHlist.GetByIndex(k) as Dictionary<XmlElement, PointF>;
             PointF pf = TLMath.getUseOffset(str_sub);
             float X = 0f;
             float Y = 0f;
             float ox = 0f;
             float oy = 0f;
             if (gr1.IsVisible(new PointF(c1.CX, c1.CY))) {
                 X = c1.CenterPoint.X - pf.X;
                 Y = c1.CenterPoint.Y - pf.Y;
                 ox = c1.CenterPoint.X + 8;
                 oy = c1.CenterPoint.Y;
             } else {
                 X = ((PointF)polyCentriodList[k]).X - pf.X;
                 Y = ((PointF)polyCentriodList[k]).Y - pf.Y;
                 ox = ((PointF)polyCentriodList[k]).X + 8;
                 oy = ((PointF)polyCentriodList[k]).Y;
             }
             XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;

             e1.SetAttribute("x", Convert.ToString(X));
             e1.SetAttribute("y", Convert.ToString(Y));
             e1.SetAttribute("xzflag", "1");
             e1.SetAttribute("xlink:href", str_sub);
             e1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
             e1.SetAttribute("layer", SvgDocument.currentLayer);
             e1.SetAttribute("subname", Convert.ToString(k + 1) + "号");
             e1.SetAttribute("rl", dbl_rl.ToString());
             SubandFHcollect _subandfh = new SubandFHcollect(FHandPointF, e1);
             tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);
             tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)e1);

             XmlElement t1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
             t1.SetAttribute("x", Convert.ToString(ox));
             t1.SetAttribute("y", Convert.ToString(oy));

             t1.SetAttribute("layer", SvgDocument.currentLayer);
             t1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
             t1.SetAttribute("font-famliy", "宋体");
             t1.SetAttribute("font-size", "54");
             t1.InnerText = Convert.ToString(k + 1) + "号";
             tlVectorControl1.SVGDocument.RootElement.AppendChild(t1);

             decimal temp1 = TLMath.getNumber((decimal)c1.R, tlVectorControl1.ScaleRatio);
             //MessageBox.Show(temp1.ToString());
             if (Convert.ToDecimal(str_jj) > temp1) {
                 MessageBox.Show(Convert.ToString(k + 1) + "号变电站候选站址小于最小供电半径，请手动进行调整。");
             }

             PSP_SubstationSelect s = new PSP_SubstationSelect();
             s.UID = Guid.NewGuid().ToString();
             s.EleID = t1.GetAttribute("id");
             s.SName = Convert.ToString(k + 1) + "号";
             s.Remark = "";
             s.col2 = XZ_bdz;
             s.SvgID = tlVectorControl1.SVGDocument.SvgdataUid;
             Services.BaseService.Create<PSP_SubstationSelect>(s);
             CreateSubline1(_subandfh,true,lay);    //生成负荷中心与变电站的连接线
             arraylist.Add(e1);
         }
     } 
     catch (Exception e1) { }
            
}
#endregion
        private void Extbdzreport(ArrayList extsublist)
        {
              ExcelAccess ex = new ExcelAccess();
            try
            {
              
                string fname = Application.StartupPath + "\\xls\\tempt.xls";
                ex.Open(fname);
                //ex.ActiveSheet(1);
                ex.SetCellValue("变电站名称", 1, 1);
                ex.SetCellValue("容量", 1, 2);
                ex.SetCellValue("容载比", 1, 3);

                ex.AlignmentCells(1, 1, 1, 3, ExcelStyle.ExcelHAlign.居中, ExcelStyle.ExcelVAlign.居中);
                ex.SetFontStyle(1, 1, 1, 3, true, false, ExcelStyle.UnderlineStyle.无下划线);
                ex.CellsBackColor(1, 1, 1, 3, ExcelStyle.ColorIndex.黄色);

                  //输出地块负荷情况
                ex.SetCellValue("变电站供应负荷情况", 3 + extsublist.Count, 1);
                ex.UnitCells(3 + extsublist.Count, 1, 3 + extsublist.Count, 3);
                ex.SetCellValue("变电站名称", 4 + extsublist.Count, 1);
                ex.SetCellValue("地块编号", 4 + extsublist.Count, 2);
                ex.SetCellValue("供应负荷", 4 + extsublist.Count, 3);
                ex.AlignmentCells(3 + extsublist.Count, 1, 4 + extsublist.Count, 3, ExcelStyle.ExcelHAlign.居中, ExcelStyle.ExcelVAlign.居中);
                ex.SetFontStyle(3 + extsublist.Count, 1, 4 + extsublist.Count, 3, true, false, ExcelStyle.UnderlineStyle.无下划线);
                ex.CellsBackColor(3 + extsublist.Count, 1, 4 + extsublist.Count, 3, ExcelStyle.ColorIndex.黄色);

                int rowcount = 0;
                for (int i = 0; i < extsublist.Count; i++)
                {
                    XmlElement xe = extsublist[i] as XmlElement;
                    if (!string.IsNullOrEmpty(xe.GetAttribute("subname")))
                    {
                        ex.SetCellValue(xe.GetAttribute("subname"), 2 + i, 1);
                        ex.AlignmentCells(2 + i, 1, 2 + i, 1, ExcelStyle.ExcelHAlign.居中, ExcelStyle.ExcelVAlign.居中);
                        ex.SetFontStyle(2 + i, 1, 2 + i, 1, true, false, ExcelStyle.UnderlineStyle.无下划线);
                        ex.CellsBackColor(2 + i, 1, 2 + i, 1, ExcelStyle.ColorIndex.绿色);
                    }
                    if (!string.IsNullOrEmpty(xe.GetAttribute("rl")))
                    {
                        ex.SetCellValue(xe.GetAttribute("rl"), 2 + i, 2);
                        ex.AlignmentCells(2 + i, 2, 2 + i, 2, ExcelStyle.ExcelHAlign.居中, ExcelStyle.ExcelVAlign.居中);
                        ex.SetFontStyle(2 + i, 2, 2 + i, 2, true, false, ExcelStyle.UnderlineStyle.无下划线);

                    }
                    if (!string.IsNullOrEmpty(xe.GetAttribute("yfcrzb")))
                    {
                        ex.SetCellValue(xe.GetAttribute("yfcrzb"), 2 + i, 3);
                        ex.AlignmentCells(2 + i, 3, 2 + i, 3, ExcelStyle.ExcelHAlign.居中, ExcelStyle.ExcelVAlign.居中);
                        ex.SetFontStyle(2 + i, 3, 2 + i, 3, true, false, ExcelStyle.UnderlineStyle.无下划线);

                    }
                    //地块负荷供应情况
                    string fhdk = xe.GetAttribute("fhdk");
                    string[] dkqk = (fhdk.Substring(0, fhdk.LastIndexOf(";"))).Split(';');

                    for (int j = 0; j < dkqk.Length;j++ )
                    {
                        string[] dk = dkqk[j].Split(',');
                        if (!string.IsNullOrEmpty(xe.GetAttribute("subname")))
                        {
                            ex.SetCellValue(xe.GetAttribute("subname"), 5 + extsublist.Count + rowcount, 1);
                            ex.AlignmentCells(5 + extsublist.Count + rowcount, 1, 5 + extsublist.Count + rowcount, 1, ExcelStyle.ExcelHAlign.居中, ExcelStyle.ExcelVAlign.居中);
                            ex.SetFontStyle(5 + extsublist.Count + rowcount, 1, 5 + extsublist.Count + rowcount, 1, true, false, ExcelStyle.UnderlineStyle.无下划线);
                            ex.CellsBackColor(5 + extsublist.Count + rowcount, 1, 5 + extsublist.Count + rowcount, 1, ExcelStyle.ColorIndex.绿色);

                        }
                       
                        if (dk.Length>0)
                        {
                            glebeProperty pl = new glebeProperty();
                            pl.EleID = dk[0];
                            pl.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            pl = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", pl);

                            ex.SetCellValue(pl.UseID, 5 + extsublist.Count + rowcount, 2);
                            ex.SetCellValue((Convert.ToDouble(dk[1]) / dbl_rzb).ToString("N2"), 5 + extsublist.Count + rowcount, 3);
                            ex.AlignmentCells(5 + extsublist.Count + rowcount, 2, 5 + extsublist.Count + rowcount, 3, ExcelStyle.ExcelHAlign.居中, ExcelStyle.ExcelVAlign.居中);
                            ex.SetFontStyle(5 + extsublist.Count + rowcount, 2, 5 + extsublist.Count + rowcount, 3, true, false, ExcelStyle.UnderlineStyle.无下划线);
                        }
                        rowcount++;
                    }
                }

              
               
                ex.ShowExcel();
            }
            catch (System.Exception e)
            {
                ex.DisPoseExcel();
            }
          
        }
       //flag 为false时为已有变电站
       //flag为ture时变电站为规划的
        private void CreateSubline1(SubandFHcollect _subandfh,bool flag,Layer lay)
        {
            XmlElement sub = _subandfh.Sub;
            double subrl=0;
            double yfcrl=0;
            if (!string.IsNullOrEmpty(sub.GetAttribute("rl")))
            {
                subrl = Convert.ToDouble(sub.GetAttribute("rl"));
            }
            double sumrl = 0;
            string fhdk = "";
            foreach (KeyValuePair<XmlElement, PointF> kv in _subandfh.FHcollect)
            {
                XmlElement _x = kv.Key;
                PointF pf = kv.Value;
                PointF _f = TLMath.polyCentriod(_x);
                XmlNode xnode = tlVectorControl1.SVGDocument.SelectSingleNode("svg/polyline[@xz='1' and FirstNode='" + sub.GetAttribute("id").ToString() + "'and LastNode='" + _x.GetAttribute("id").ToString() + "']");
                if (xnode != null)
                {
                    tlVectorControl1.SVGDocument.RootElement.RemoveChild(xnode);
                }
                glebeProperty pl = new glebeProperty();
               
                pl.EleID = _x.GetAttribute("id");         
                pl.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                pl = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", pl);

                double dkrl = 0;
                string ygdflag = "0";    //0表示还没有供电，1表示完全有已知的变电站供，2表示部分符合已有供并且写上变电站供给了多少
                if (!string.IsNullOrEmpty(_x.GetAttribute("ygdflag")))
                {
                    ygdflag = _x.GetAttribute("ygdflag");
                }
                if (!string.IsNullOrEmpty(_x.GetAttribute("Burthen")))
                {
                    dkrl = Convert.ToDouble(_x.GetAttribute("Burthen")) * dbl_rzb;

                }
                sumrl += dkrl;
                if (ygdflag=="0")  //说明还没有供给的
                {
                    if (subrl * 1.15>=sumrl)
                    {
                        fhdk += _x.GetAttribute("id") + "," + dkrl.ToString() + ";";
                        _x.SetAttribute("ygdflag", "1");
                        yfcrl=sumrl;
                    }
                    else if(subrl*1.15<sumrl&&subrl>=(sumrl-dkrl))
                    {
                        double gyrl = ((subrl * 1.15 + dkrl) - sumrl);
                        double syrl =sumrl-subrl*1.15;
                        fhdk += _x.GetAttribute("id") + "," + gyrl.ToString() + ";";
                        _x.SetAttribute("ygdflag", "2");
                        _x.SetAttribute("syrl", syrl.ToString());
                        yfcrl=subrl*1.15;
                    }
                }
                else if (ygdflag == "2")   //有一部分剩余的还需要来供
                {
                    double syrl = 0;
                    if (!string.IsNullOrEmpty(_x.GetAttribute("syrl")))
                    {
                        syrl = Convert.ToDouble(_x.GetAttribute("syrl"));
                    }
                    if ((subrl * 1.15-yfcrl)>syrl)
                    {
                        fhdk += _x.GetAttribute("id") + "," + syrl.ToString() + ";";
                        _x.SetAttribute("ygdflag", "1");
                        _x.SetAttribute("syrl", "0");
                        yfcrl = sumrl;
                    }
                    else
                    {
                        syrl=syrl-(subrl*1.15-yfcrl);
                        fhdk += _x.GetAttribute("id") + "," + (subrl * 1.15 - yfcrl).ToString() + ";";
                        _x.SetAttribute("ygdflag", "2");
                        _x.SetAttribute("syrl", syrl.ToString());
                        yfcrl = subrl * 1.15;
                    }
                }
                else if (ygdflag == "1")  //已经有供应的了
                {
                    continue;
                }
              
                if (pl != null)
                {
                    string sname = sub.GetAttribute("subname");
                    if (!string.IsNullOrEmpty(sname))
                    {
                       
                        //if (_x.GetAttribute("xz") == "0")
                        //{
                        //    pl.ObligateField7 = sname;
                        //    Services.BaseService.Update<glebeProperty>(pl);
                        //}
                        if (string.IsNullOrEmpty(pl.ObligateField7))
                        {
                            pl.ObligateField7 = sname;
                        }
                        else
                        {
                            if (!pl.ObligateField7.Contains(sname))
                            {
                                pl.ObligateField7 += "," + sname;
                            }
                        }
                            

                        Services.BaseService.Update<glebeProperty>(pl);
                    }
                }
                //if (!flag)
                //{
                //    if (_x.GetAttribute("xz")=="1")
                //    {
                //        return;
                //    }
                //}
              
                string temp = "";
                if (!flag)
                {
                    temp = sub.GetAttribute("cx").ToString() + " " + sub.GetAttribute("cy").ToString() + "," + _f.X.ToString() + " " + _f.Y.ToString();
                }
                else
                    temp = sub.GetAttribute("x").ToString() + " " + sub.GetAttribute("y").ToString() + "," + _f.X.ToString() + " " + _f.Y.ToString();

               

                XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;

                n1.SetAttribute("points", temp);
                //如果是规划
                if (flag)
                {
                    _x.SetAttribute("xz", "1");
                    n1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#990033;stroke-opacity:1;");

                }
                else
                {
                    if (_x.GetAttribute("xz") != "1")
                    {
                        _x.SetAttribute("xz", "0");
                    }

                    n1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#330099;stroke-opacity:1;");
                     
                }
                n1.SetAttribute("layer",lay.ID);
                n1.SetAttribute("IsLead", "1");
                n1.SetAttribute("FirstNode", sub.GetAttribute("id").ToString());
                n1.SetAttribute("LastNode", _x.GetAttribute("id").ToString());
                n1.SetAttribute("xz", "1");
                tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)n1);


            }
            sub.SetAttribute("fhdk", fhdk);
            if (yfcrl!=0)
            {
                sub.SetAttribute("yfcrzb", (subrl / ((yfcrl / dbl_rzb))).ToString());
            }
            else
            {
                sub.SetAttribute("yfcrzb", (dbl_rzb).ToString());
            }
            
        }
        
        private void CreateSubline(SubandFHcollect _subandfh,bool flag)
        {

               XmlElement sub = _subandfh.Sub ;
            
                 foreach (KeyValuePair<XmlElement, PointF> kv in _subandfh.FHcollect )
                 {
                     XmlElement _x = kv.Key;
                     
                     //if (!flag)
                     //{
                     //    if (_x.GetAttribute("xz")=="1")
                     //    {
                     //        return;
                     //    }
                     //}
                     PointF pf = kv.Value;
                     PointF _f = TLMath.polyCentriod(_x);
                     XmlNode xnode = tlVectorControl1.SVGDocument.SelectSingleNode("svg/polyline[@xz='1' and FirstNode='" + sub.GetAttribute("id").ToString() + "'and LastNode='" + _x.GetAttribute("id").ToString() + "']");
                   if (xnode!=null)
                   {
                       tlVectorControl1.SVGDocument.RootElement.RemoveChild(xnode);
                   }
                   string temp = "";
                    if (!flag)
                    {
                        temp = sub.GetAttribute("cx").ToString() + " " + sub.GetAttribute("cy").ToString() + "," + _f.X.ToString() + " " + _f.Y.ToString();                                        
                    }
                     else
                    temp = sub.GetAttribute("x").ToString() + " " + sub.GetAttribute("y").ToString() + "," + _f.X.ToString() + " " + _f.Y.ToString();

                    glebeProperty pl = new glebeProperty();
                    pl.EleID = _x.GetAttribute("id");
                    pl.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    pl = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", pl);
                    if (pl != null)
                    {
                        string sname = sub.GetAttribute("subname");
                        if (!string.IsNullOrEmpty(sname))
                        {
                            
                            if (_x.GetAttribute("xz")=="0")
                            {
                                pl.ObligateField7 = sname;
                                Services.BaseService.Update<glebeProperty>(pl);
                            }
                           
                        }
                    } 
  
                    XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                    
                    n1.SetAttribute("points",temp);
                    //如果是规划
                    if (flag)
                    {
                        _x.SetAttribute("xz", "1");
                        n1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#990033;stroke-opacity:1;");

                    }
                    else
                    {
                        if (_x.GetAttribute("xz")!="1")
                        {
                            _x.SetAttribute("xz", "0");
                        }
                        
                        n1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#330099;stroke-opacity:1;");

                    }
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("IsLead", "1");
                    n1.SetAttribute("FirstNode", sub.GetAttribute("id").ToString());
                    n1.SetAttribute("LastNode", _x.GetAttribute("id").ToString());
                    n1.SetAttribute("xz", "1");
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)n1);
                   
                  
                 }

                 

        //    XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;

        //    e1.SetAttribute("x", Convert.ToString(X));
        //    e1.SetAttribute("y", Convert.ToString(Y));

        //    e1.SetAttribute("xlink:href", str_sub);
        //    e1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
        //    e1.SetAttribute("layer", SvgDocument.currentLayer);
        //    SubandFHcollect _subandfh = new SubandFHcollect(FHandPointF, e1);
        //    tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);
        //    tlVectorControl1.SVGDocument.SelectCollection.Add((SvgElement)e1);
        }

        //返回某变电站最小覆盖原相交的地块
        private Dictionary<XmlElement,PointF> GetsubFhk(Circle cir,ArrayList _polylist)
        {
            Dictionary<XmlElement, PointF> fhkcollect = new Dictionary<XmlElement, PointF>();
         
                
                int k = 0;
                GraphicsPath gr1 = new GraphicsPath();
                gr1.AddPath(cir.GPath, true);
                gr1.CloseFigure();
                for (int m = 0; m < _polylist.Count; m++) {
                    XmlElement _x = (XmlElement)_polylist[m];
                    PointF _f = TLMath.polyCentriod(_x);
                    if (gr1.IsVisible(_f))    //外接圆包括那些负荷中心点
                    {
                        //k = k + 1;   //求和的过程
                        //string sid = _x.GetAttribute("id");
                        //glebeProperty pl = new glebeProperty();
                        //pl.EleID = sid;
                        //pl.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                        //pl = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", pl);
                        //if (pl != null) {
                        //    sum = sum + pl.Burthen;
                        //}
                        fhkcollect.Add(_x, _f);//记录外包圆相交的负荷块
                    }
                }
                //clist.Add(sum + n, cir);
                //CtoFHlist.Add(sum + n, fhkcollect);
                //string aa = "";
                return fhkcollect;
           
        }
        public void InputBDZFile(string filename, bool create)
        {

            Excel.Application ex = new Excel.Application();
            Excel.Workbook workBook = ex.Workbooks.Open(filename, Type.Missing, Type.Missing,
                                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            ex.Visible = false;
            FlashWindow frmLoad = new FlashWindow();
            frmLoad.Show();

            try
            {
                ArrayList alldata = new ArrayList();
                bool right = true;
                frmLoad.RefleshStatus("共有" + ex.Worksheets.Count.ToString() + "张工作表。");
                for (int n = 1; n <= ex.Worksheets.Count; n++)
                {
                    Excel.Worksheet xSheet1 = (Excel.Worksheet)ex.Worksheets[n];
                    int c = xSheet1.Columns.Count;
                    int r = xSheet1.Rows.Count;
                    ArrayList jwlist = new ArrayList();
                    ArrayList mclist = new ArrayList();
                    ArrayList dylist = new ArrayList();
                    int int_jd = 0;
                    int int_wd = 0;
                    int int_dy = 0;
                    int int_mc = 0;
                    string kv = "";
                    string s = "";


                    for (int j = 1; j < xSheet1.Columns.Count; j++)
                    {
                        frmLoad.RefleshStatus("正在导入第" + n.ToString() + "张工作表,第" + j.ToString() + "行记录。");
                        if (((Excel.Range)xSheet1.Cells[1, j]).Value2 == null)
                        {
                            break;
                        }
                        Excel.Range range1 = (Excel.Range)xSheet1.Cells[1, j];
                        if (range1.Value2.ToString().Contains("经度"))
                        {
                            int_jd = j;
                        }
                        if (range1.Value2.ToString().Contains("纬度"))
                        {
                            int_wd = j;
                        }
                        if (range1.Value2.ToString().Contains("电压"))
                        {
                            int_dy = j;
                        }
                        if (range1.Value2.ToString().Contains("名称"))
                        {
                            int_mc = j;
                        }
                    }
                    if (int_jd == 0 || int_wd == 0)
                    {
                        MessageBox.Show("第" + n.ToString() + "张工作表中不包含经纬度信息，请核对。");
                        right = false;
                        break;
                    }
                    if (int_dy == 0)
                    {
                        MessageBox.Show("第" + n.ToString() + "张工作表中不包含电压信息，请核对。");
                        right = false;
                        break;
                    }
                    if (int_mc == 0)
                    {
                        MessageBox.Show("第" + n.ToString() + "张工作表中不包含变电站名称信息，请核对。");
                        right = false;
                        break;
                    }
                    //   fInfo.Info = "正在导入第" + n.ToString() + "张工作表......";
                    //line_name = xSheet1.Name;
                    for (int i = 2; i < xSheet1.Rows.Count; i++)
                    {
                        if (((Excel.Range)xSheet1.Cells[i, 1]).Value2 == null)
                        {
                            goto LabReadEnd;
                        }
                        Excel.Range range_J = (Excel.Range)xSheet1.Cells[i, int_jd];
                        Excel.Range range_W = (Excel.Range)xSheet1.Cells[i, int_wd];
                        Excel.Range range_mc = (Excel.Range)xSheet1.Cells[i, int_mc];
                        Excel.Range range_kv = (Excel.Range)xSheet1.Cells[i, int_dy];
                        if (range_J.Value2 == null || range_W.Value2 == null)
                        {
                            MessageBox.Show("第" + n.ToString() + "张工作表中第" + i.ToString() + "行经纬度数据格式错误，请核对。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            right = false;
                            break;
                        }
                        if (range_mc.Value2 == null)
                        {
                            MessageBox.Show("第" + n.ToString() + "张工作表中第" + i.ToString() + "行名称数据格式错误，请核对。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            right = false;
                            break;
                        }
                        if (range_kv.Value2 == null)
                        {
                            MessageBox.Show("第" + n.ToString() + "张工作表中第" + i.ToString() + "行电压数据格式错误，请核对。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            right = false;
                            break;
                        }
                        if (range_kv.Value2.ToString() != "500" && range_kv.Value2.ToString() != "220" && range_kv.Value2.ToString() != "110" && range_kv.Value2.ToString() != "35")
                        {
                            MessageBox.Show("第" + n.ToString() + "张工作表中第" + i.ToString() + "行中电压在图元中不存在，请核对。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            right = false;
                            break;
                        }

                        else
                        {
                            bool ck_r = true;
                            //gt = gt + range_gt.Value2.ToString() + ",";
                            s = s + range_J.Value2.ToString() + "," + range_W.Value2.ToString() + ";";
                            string s2 = range_J.Value2.ToString().Trim() + "," + range_W.Value2.ToString().Trim();

                        labLoop:
                            if (!alldata.Contains(s2))
                            {
                                alldata.Add(s2);
                                ck_r = true;
                            }
                            else
                            {
                                ck_r = false;
                            }

                            if (ck_r)
                            {
                                jwlist.Add(s2);
                            }
                            else
                            {
                                s2 = "S" + s2;
                                goto labLoop;
                                //jwlist.Add("S" + s2);
                            }
                            mclist.Add(range_mc.Value2.ToString());
                            dylist.Add(range_kv.Value2.ToString());
                        }
                    }
                LabReadEnd:
                    if (right == true)
                    {
                        StringBuilder bpts = new StringBuilder();
                        string jwd = "";
                        string points = "";
                        for (int k = 0; k < jwlist.Count; k++)
                        {
                            bool ck_s = true;
                            string str_jwdlist = ((string)jwlist[k]);
                            int int_s = 0;
                            if (str_jwdlist.Contains("S"))
                            {
                                ck_s = false;
                                int_s = str_jwdlist.LastIndexOfAny("S".ToCharArray());
                                if (int_s > -1)
                                {
                                    int_s = int_s + 1;
                                }
                                str_jwdlist = str_jwdlist.Replace("S", "");
                            }

                            jwd = jwd + str_jwdlist + ";";

                            double JD = 0;
                            double WD = 0;
                            if (str_jwdlist.LastIndexOfAny(" ".ToCharArray()) > 3)
                            {
                                string[] str = str_jwdlist.Split(',');
                                string[] JWD1 = str[0].Trim().Split(' ');
                                double J1 = Convert.ToDouble(JWD1[0]);
                                Double W1 = Convert.ToDouble(JWD1[1]);
                                Double D1 = Convert.ToDouble(JWD1[2]);
                                string[] JWD2 = str[1].Trim().Split(' ');
                                Double J2 = Convert.ToDouble(JWD2[0]);
                                Double W2 = Convert.ToDouble(JWD2[1]);
                                Double D2 = Convert.ToDouble(JWD2[2]);

                                JD = J1 + W1 / 60 + D1 / 3600;
                                WD = J2 + W2 / 60 + D2 / 3600;
                            }
                            else
                            {
                                string[] str = str_jwdlist.Split(',');
                                JD = Convert.ToDouble(str[0]);
                                WD = Convert.ToDouble(str[1]);
                            }

                            IntXY xy = mapview.getXY(JD, WD);
                            double _x = xy.X;
                            double _y = xy.Y;
                            if (ck_s)
                            {
                                if (mapview is MapViewGoogle)
                                {
                                    _x = xy.X;
                                    _y = xy.Y;
                                }
                                else
                                {
                                    _x = xy.X / (double)tlVectorControl1.ScaleRatio;
                                    _y = xy.Y / (double)tlVectorControl1.ScaleRatio;
                                }
                            }
                            else
                            {
                                if (mapview is MapViewGoogle)
                                {
                                    _x = xy.X + 5 * int_s;
                                    _y = xy.Y + 5 * int_s;
                                }
                                else
                                {
                                    _x = xy.X / (double)tlVectorControl1.ScaleRatio + 5 * int_s;
                                    _y = xy.Y / (double)tlVectorControl1.ScaleRatio + 5 * int_s;
                                }

                            }

                            string pspid = "";
                            if (create)
                            {
                                PSP_Substation_Info sub = new PSP_Substation_Info();
                                sub.UID = Guid.NewGuid().ToString();
                                sub.Title = mclist[k].ToString();
                                sub.AreaID = MIS.ProgUID;
                                sub.L1 = Convert.ToInt32(dylist[k]);
                                Services.BaseService.Create<Substation_Info>(sub);
                                pspid = sub.UID;
                            }

                            string str_sub = getHCSubName(dylist[k].ToString());
                            //PointF pf = getOff(str_dy);
                            XmlElement e0 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;
                            e0.SetAttribute("x", Convert.ToString(_x));
                            e0.SetAttribute("y", Convert.ToString(_y));
                            e0.SetAttribute("info-name", mclist[k].ToString());
                            e0.SetAttribute("jwd-info", str_jwdlist);
                            e0.SetAttribute("xlink:href", str_sub);
                            e0.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                            e0.SetAttribute("layer", SvgDocument.currentLayer);
                            if (pspid != "")
                            {
                                e0.SetAttribute("Deviceid", pspid);
                            }
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(e0);

                        }

                    }
                }
                //fInfo.Hide();
                ex.Quit();
                frmLoad.Close();
            }
            catch
            {
                MessageBox.Show(" 数据格式错误，请检查。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ex.Quit();
                frmLoad.Close();
            }
        }

        public void InputFile(string filename, bool create)
        {
            int LineOffSize = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("LineOffSize"));
            Excel.Application ex = new Excel.Application();
            Excel.Workbook workBook = ex.Workbooks.Open(filename, Type.Missing, Type.Missing,
                                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            ex.Visible = false;
            FlashWindow frmLoad = new FlashWindow();
            frmLoad.Show();
            try
            {
                ArrayList alldata = new ArrayList();
                bool right = true;

                frmLoad.RefleshStatus("共有" + ex.Worksheets.Count.ToString() + "张工作表。");
                //fInfo.Info = "共有" + ex.Worksheets.Count.ToString()+ "张工作表。";
                //fInfo.Show();
                for (int n = 1; n <= ex.Worksheets.Count; n++)
                {
                    Excel.Worksheet xSheet1 = (Excel.Worksheet)ex.Worksheets[n];
                    int c = xSheet1.Columns.Count;
                    int r = xSheet1.Rows.Count;
                    string line_name = "";
                    ArrayList jwlist = new ArrayList();
                    ArrayList gtlist = new ArrayList();
                    int int_jd = 0;
                    int int_wd = 0;
                    int int_dy = 0;
                    int int_gt = 0;
                    int int_gtxh = 0;
                    string kv = "";
                    string s = "";
                    string gt = "";
                    string s_gtxh = "";
                    for (int j = 1; j < xSheet1.Columns.Count; j++)
                    {
                        if (((Excel.Range)xSheet1.Cells[1, j]).Value2 == null)
                        {
                            break;
                        }
                        Excel.Range range1 = (Excel.Range)xSheet1.Cells[1, j];
                        if (range1.Value2.ToString().Contains("经度"))
                        {
                            int_jd = j;
                        }
                        if (range1.Value2.ToString().Contains("纬度"))
                        {
                            int_wd = j;
                        }
                        if (range1.Value2.ToString().Contains("电压"))
                        {
                            int_dy = j;
                        }
                        if (range1.Value2.ToString().Contains("杆塔号"))
                        {
                            int_gt = j;
                        }
                        if (range1.Value2.ToString().Contains("杆塔型号"))
                        {
                            int_gtxh = j;
                        }
                    }
                    if (int_jd == 0 || int_wd == 0)
                    {
                        MessageBox.Show("第" + n.ToString() + "张工作表中不包含经纬度信息，请核对。");
                        right = false;
                        break;
                    }
                    if (int_dy == 0)
                    {
                        MessageBox.Show("第" + n.ToString() + "张工作表中不包含电压信息，请核对。");
                        right = false;
                        break;
                    }
                    if (int_gt == 0)
                    {
                        MessageBox.Show("第" + n.ToString() + "张工作表中不包含杆塔号信息，请核对。");
                        right = false;
                        break;
                    }
                    frmLoad.RefleshStatus("正在导入第" + n.ToString() + "张工作表......");
                    //   fInfo.Info = "正在导入第" + n.ToString() + "张工作表......";
                    line_name = xSheet1.Name;
                    for (int i = 2; i < xSheet1.Rows.Count; i++)
                    {
                        frmLoad.RefleshStatus("正在导入第" + n.ToString() + "张工作表,第" + i.ToString() + "行记录。");
                        if (((Excel.Range)xSheet1.Cells[i, 1]).Value2 == null)
                        {
                            goto LabReadEnd;
                        }
                        int gtxh = 0;
                        Excel.Range range_J = (Excel.Range)xSheet1.Cells[i, int_jd];
                        Excel.Range range_W = (Excel.Range)xSheet1.Cells[i, int_wd];
                        Excel.Range range_gt = (Excel.Range)xSheet1.Cells[i, int_gt];
                        Excel.Range range_kv = (Excel.Range)xSheet1.Cells[i, int_dy];
                        if (int_gtxh != 0)
                        {
                            Excel.Range range_gtxh = (Excel.Range)xSheet1.Cells[i, int_gtxh];
                            string str_xh = range_gtxh.Value2.ToString();
                            if (str_xh.Contains("T"))
                            {
                                gtxh = 1;
                            }
                        }
                        if (range_J.Value2 == null || range_W.Value2 == null)
                        {
                            MessageBox.Show("第" + n.ToString() + "张工作表中第" + i.ToString() + "行数据格式错误，请核对。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            right = false;
                            break;
                        }

                        else
                        {
                            bool ck_r = true;
                            gt = gt + range_gt.Value2.ToString() + ",";
                            s_gtxh = s_gtxh + gtxh.ToString() + ",";
                            s = s + range_J.Value2.ToString() + "," + range_W.Value2.ToString() + ";";
                            string s2 = range_J.Value2.ToString().Trim() + "," + range_W.Value2.ToString().Trim();

                        labLoop:
                            if (!alldata.Contains(s2))
                            {
                                alldata.Add(s2);
                                ck_r = true;
                            }
                            else
                            {
                                ck_r = false;
                            }

                            if (ck_r)
                            {
                                jwlist.Add(s2);
                            }
                            else
                            {
                                s2 = "S" + s2;
                                goto labLoop;
                                //jwlist.Add("S" + s2);
                            }
                        }
                        //line_name = range_nm.Value2.ToString();
                        if (range_kv.Value2 != null)
                        {
                            kv = range_kv.Value2.ToString();
                        }
                    }
                LabReadEnd:
                    if (right == true)
                    {
                        StringBuilder bpts = new StringBuilder();
                        string jwd = "";
                        string points = "";
                        for (int k = 0; k < jwlist.Count; k++)
                        {
                            bool ck_s = true;
                            string str_jwdlist = ((string)jwlist[k]);
                            int int_s = 0;
                            if (str_jwdlist.Contains("S"))
                            {
                                ck_s = false;
                                int_s = str_jwdlist.LastIndexOfAny("S".ToCharArray());
                                if (int_s > -1)
                                {
                                    int_s = int_s + 1;
                                }
                                str_jwdlist = str_jwdlist.Replace("S", "");
                            }

                            jwd = jwd + str_jwdlist + ";";

                            double JD = 0;
                            double WD = 0;
                            if (str_jwdlist.LastIndexOfAny(" ".ToCharArray()) > 3)
                            {
                                string[] str = str_jwdlist.Split(',');
                                string[] JWD1 = str[0].Trim().Split(' ');
                                double J1 = Convert.ToDouble(JWD1[0]);
                                Double W1 = Convert.ToDouble(JWD1[1]);
                                Double D1 = Convert.ToDouble(JWD1[2]);
                                string[] JWD2 = str[1].Trim().Split(' ');
                                Double J2 = Convert.ToDouble(JWD2[0]);
                                Double W2 = Convert.ToDouble(JWD2[1]);
                                Double D2 = Convert.ToDouble(JWD2[2]);

                                JD = J1 + W1 / 60 + D1 / 3600;
                                WD = J2 + W2 / 60 + D2 / 3600;
                            }
                            else
                            {
                                string[] str = str_jwdlist.Split(',');
                                JD = Convert.ToDouble(str[0]);
                                WD = Convert.ToDouble(str[1]);
                            }

                            IntXY xy = mapview.getXY(JD, WD);
                            if (ck_s)
                            {
                                if (mapview is MapViewGoogle)
                                    bpts.Append(xy.X + " " + xy.Y + ",");
                                else
                                    bpts.Append((-xy.X / (double)tlVectorControl1.ScaleRatio) + " " + (-xy.Y / (double)tlVectorControl1.ScaleRatio) + ",");
                            }
                            else
                            {
                                if (mapview is MapViewGoogle)
                                    bpts.Append(xy.X + LineOffSize * int_s + " " + xy.Y + LineOffSize * int_s + ",");
                                else
                                {
                                    bpts.Append((-xy.X / (double)tlVectorControl1.ScaleRatio) + LineOffSize * int_s + " " + (-xy.Y / (double)tlVectorControl1.ScaleRatio) + LineOffSize * int_s + ",");
                                }

                            }
                        }
                        if (bpts.Length > 0)
                            points = bpts.ToString(0, bpts.Length - 1);


                        if (jwd.Length > 1)
                        {
                            jwd = jwd.Substring(0, jwd.Length - 1);
                        }

                        if (gt.Length > 0)
                        {
                            gt = gt.Substring(0, gt.Length - 1);
                        }
                        if (s_gtxh.Length > 0)
                        {
                            s_gtxh = s_gtxh.Substring(0, s_gtxh.Length - 1);
                        }
                        string styleValue = "";
                        LineType lt = new LineType();
                        if (!kv.Contains("kV"))
                        {
                            kv = kv + "kV";
                        }
                        lt.TypeName = kv;
                        lt = (LineType)Services.BaseService.GetObject("SelectLineTypeByTypeName", lt);
                        if (lt != null)
                        {
                            styleValue = "stroke-width:" + lt.ObligateField1 + ";";
                            styleValue = styleValue + "stroke:" + ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(lt.Color)));
                        }
                        string pspid = "";
                        if (create)
                        {
                            PSPDEV psp = new PSPDEV();
                            psp.SUID = Guid.NewGuid().ToString();
                            psp.Name = line_name;
                            psp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;

                            psp.Type = "05";
                            psp.RateVolt = Convert.ToDouble(kv.Replace("kV", ""));
                            psp.ProjectID = MIS.ProgUID;
                            Services.BaseService.Create<PSPDEV>(psp);
                            pspid = psp.SUID;
                        }

                        XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                        n1.SetAttribute("IsLead", "1");
                        n1.SetAttribute("points", points);
                        n1.SetAttribute("layer", SvgDocument.currentLayer);
                        n1.SetAttribute("info-name", line_name);
                        n1.SetAttribute("jwd-info", jwd);
                        n1.SetAttribute("gt-info", gt);
                        n1.SetAttribute("dy-info", kv.Replace("kV", ""));
                        n1.SetAttribute("gtxh-info", s_gtxh);
                        if (pspid != "")
                        {
                            n1.SetAttribute("Deviceid", pspid);
                        }
                        if (styleValue != "")
                        {
                            n1.SetAttribute("style", styleValue);
                        }
                        tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);

                    }
                }
                //fInfo.Hide();
                ex.Quit();
                frmLoad.Close();
            }
            catch
            {
                MessageBox.Show(" 数据格式错误，请检查。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ex.Quit();
                frmLoad.Close();
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
                    label1.Text = Math.Round(d, 3) + "公里";

                    Point pt = new Point(e.Mouse.X, e.Mouse.Y);
                    pt = PointToClient(tlVectorControl1.PointToScreen(pt));
                    //tlVectorControl1.SetToolTip(label1.Text);
                    label1.Left = pt.X;
                    label1.Top = pt.Y;

                    label1.Visible = true;
                }
                return;
            }

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
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Text")
            {
                string showlineflag = ((XmlElement)e.SvgElement).GetAttribute("Showline");
                if (showlineflag == "1")
                {
                    XmlElement n1 = (XmlElement)e.SvgElement;
                    string suid = n1.GetAttribute("ParentID");
                    PSPDEV psp = new PSPDEV();
                    psp.SUID = suid;
                    psp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", psp);
                    if (psp != null)
                    {
                        fInfo.Info = "线路名称：" + psp.Name;
                    }
                    fInfo.Top = e.Mouse.Y;
                    fInfo.Left = e.Mouse.X;
                    fInfo.Width = (fInfo.Info.Length) * 20;
                    fInfo.Show();

                }
            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Polygon")
            {
                string IsArea = ((XmlElement)e.SvgElement).GetAttribute("IsArea");
                if (IsArea != "")
                {
                    SelUseArea = "";
                    //XmlElement n1 = (XmlElement)e.SvgElement;
                    PointF[] pnts = ((Polygon)e.SvgElement).Points.Clone() as PointF[];
                    ((Polygon)e.SvgElement).Transform.Matrix.TransformPoints(pnts);
                    decimal temp1 = TLMath.getPolygonArea(pnts, 1);

                    temp1 = TLMath.getNumber2(temp1, tlVectorControl1.ScaleRatio) / Convert.ToDecimal(areaoption);
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
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline" && str_outjwd == "1")
            {
                PointF[] pf = ((Polyline)e.SvgElement).Points;
                ((Polyline)e.SvgElement).Transform.Matrix.TransformPoints(pf);

                string strM = "";
                for (int i = 0; i < pf.Length; i++)
                {
                    strM = strM + getJWD(mapview.ParseToLongLat(pf[i].X, pf[i].Y));
                }
                System.IO.FileInfo ff = new System.IO.FileInfo("c:\\temp.txt");
                System.IO.StreamWriter wt = ff.CreateText();
                wt.Write(strM);
                wt.Close();
                System.Diagnostics.Process.Start("c:\\temp.txt");
            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline" && str_djcl == "1")
            {
                Layer lar = null;
                Layer lar2 = null;
                bool create = true;
                int TempObjRadius = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("TempObjRadius"));
                int TempTextSize = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("TempTextSize"));

                for (int n = 0; n < tlVectorControl1.SVGDocument.Layers.Count; n++)
                {
                    Layer l = tlVectorControl1.SVGDocument.Layers[n] as Layer;
                    if (l.Label == "临时显示层-杆塔号")
                    {
                        lar = l;
                    }
                    if (l.Label == "临时显示层-公里数")
                    {
                        lar2 = l;
                    }
                }
                if (!Layer.CkLayerExist("临时显示层-杆塔号", tlVectorControl1.SVGDocument))
                {
                    lar = Layer.CreateNew("临时显示层-杆塔号", tlVectorControl1.SVGDocument);
                    lar.SetAttribute("layerType", progtype);
                    lar.SetAttribute("ParentID", tlVectorControl1.SVGDocument.CurrentLayer.GetAttribute("ParentID"));
                    //this.frmlar.checkedListBox1.SelectedIndex = -1;
                    //this.frmlar.checkedListBox1.Items.Add(lar, true);
                    lar2 = Layer.CreateNew("临时显示层-公里数", tlVectorControl1.SVGDocument);
                    lar2.SetAttribute("layerType", progtype);
                    lar2.SetAttribute("ParentID", tlVectorControl1.SVGDocument.CurrentLayer.GetAttribute("ParentID"));
                    //this.frmlar.checkedListBox1.SelectedIndex = -1;
                    //this.frmlar.checkedListBox1.Items.Add(lar2, true);
                }
                string gt = ((XmlElement)e.SvgElement).GetAttribute("gt-info");
                string _gtxh = ((XmlElement)e.SvgElement).GetAttribute("gtxh-info");

                string[] gtid = gt.Split(",".ToCharArray());
                string[] strgtxh = _gtxh.Split(",".ToCharArray());

                XmlNodeList useList = tlVectorControl1.SVGDocument.SelectNodes("//* [@layer='" + lar.ID + "'] [@parentobj='" + ((Polyline)e.SvgElement).ID + "']");

                for (int kk = 0; kk < useList.Count; kk++)
                {
                    tlVectorControl1.SVGDocument.RootElement.RemoveChild(useList[kk]);
                }

                PointF[] pf = ((Polyline)e.SvgElement).Points;
                ((Polyline)e.SvgElement).Transform.Matrix.TransformPoints(pf);
                if (create)
                {
                    for (int i = 0; i < pf.Length; i++)
                    {

                        if (_gtxh == "")
                        {
                            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                            n1.SetAttribute("cx", Convert.ToString(pf[i].X));
                            n1.SetAttribute("cy", Convert.ToString(pf[i].Y));
                            n1.SetAttribute("r", TempObjRadius.ToString());
                            n1.SetAttribute("layer", lar.ID);
                            n1.SetAttribute("parentobj", ((Polyline)e.SvgElement).ID);
                            n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                            tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                        }
                        else
                        {
                            if (strgtxh.Length > i)
                            {
                                if (strgtxh[i] == "0")
                                {
                                    XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("circle") as Circle;
                                    n1.SetAttribute("cx", Convert.ToString(pf[i].X));
                                    n1.SetAttribute("cy", Convert.ToString(pf[i].Y));
                                    n1.SetAttribute("r", TempObjRadius.ToString());
                                    n1.SetAttribute("layer", lar.ID);
                                    n1.SetAttribute("parentobj", ((Polyline)e.SvgElement).ID);
                                    n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                }
                                else
                                {
                                    XmlElement tk = tlVectorControl1.SVGDocument.CreateElement("rect") as RectangleElement;
                                    tk.SetAttribute("x", Convert.ToString(pf[i].X - 10));
                                    tk.SetAttribute("y", Convert.ToString(pf[i].Y - 10));
                                    tk.SetAttribute("width", Convert.ToString(TempObjRadius * 2));
                                    tk.SetAttribute("height", Convert.ToString(TempObjRadius * 2));
                                    tk.SetAttribute("parentobj", ((Polyline)e.SvgElement).ID);
                                    tk.SetAttribute("style", "fill:#FFFFC0;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                                    tk.SetAttribute("layer", lar.ID);
                                    tlVectorControl1.SVGDocument.RootElement.AppendChild(tk);
                                }
                            }
                        }
                        XmlElement t0 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                        t0.SetAttribute("x", Convert.ToString(pf[i].X));
                        t0.SetAttribute("y", Convert.ToString(pf[i].Y));
                        t0.SetAttribute("layer", lar.ID);
                        t0.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                        t0.SetAttribute("font-famliy", "宋体");
                        t0.SetAttribute("parentobj", ((Polyline)e.SvgElement).ID);
                        t0.SetAttribute("font-size", TempTextSize.ToString());

                        if (gtid.Length > i)
                        {
                            t0.InnerText = gtid[i] + "号";
                        }
                        else
                        {
                            t0.InnerText = Convert.ToString(i + 1) + "号";
                        }
                        tlVectorControl1.SVGDocument.RootElement.AppendChild(t0);
                    }
                    for (int i = 0; i < pf.Length - 1; i++)
                    {
                        double s = this.mapview.CountLength(pf[i], pf[i + 1]);
                        //   TLMath.getLength(pf[i], pf[i + 1], (decimal)tlVectorControl1.ScaleRatio);
                        XmlElement t0 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                        t0.SetAttribute("x", Convert.ToString((pf[i].X + pf[i + 1].X) / 2));
                        t0.SetAttribute("y", Convert.ToString((pf[i].Y + pf[i + 1].Y) / 2));
                        t0.SetAttribute("layer", lar2.ID);
                        t0.SetAttribute("parentobj", ((Polyline)e.SvgElement).ID);
                        t0.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                        t0.SetAttribute("font-famliy", "宋体");
                        t0.SetAttribute("font-size", TempTextSize.ToString());
                        t0.InnerText = s.ToString("0.##") + "km";
                        tlVectorControl1.SVGDocument.RootElement.AppendChild(t0);
                    }
                }
                str_djcl = "";
            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline")
            {
                string IsLead = ((XmlElement)e.SvgElement).GetAttribute("IsLead");
                if (IsLead != "")
                {
                    Polyline polyline = (Polyline)e.SvgElement;
                    double temp1 = 0;
                    for (int i = 1; i < polyline.Points.Length; i++)
                    {
                        temp1 += this.mapview.CountLength(polyline.Points[i - 1], polyline.Points[i]);
                    }


                    //decimal temp1 = TLMath.getPolylineLength(polyline, 1);
                    //temp1 = TLMath.getNumber(temp1, tlVectorControl1.ScaleRatio);
                    string len = temp1.ToString("#####.####");
                    LineLen = len;
                    PSPDEV lineInfo = new PSPDEV();
                    lineInfo.SUID = ((XmlElement)e.SvgElement).GetAttribute("Deviceid");
                    // lineInfo.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    PSPDEV _lineTemp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", lineInfo);

                    if ((len != "") && (_lineTemp != null))
                    {
                        if (Convert.ToDecimal(len) >= 1)
                        {
                            fInfo.Info = "线路名称：" + _lineTemp.Name + " 线路长度：" + len + "（KM）\r\n" + "导线型号：" + _lineTemp.LineType + " 电压等级：" + _lineTemp.RateVolt.ToString() + "kV 投产年限：" + _lineTemp.OperationYear;
                        }
                        else
                        {
                            fInfo.Info = "线路名称：" + _lineTemp.Name + " 线路长度： 0" + len + "（KM）\r\n" + "导线型号：" + _lineTemp.LineType + " 电压等级：" + _lineTemp.RateVolt.ToString() + "kV 投产年限：" + _lineTemp.OperationYear;
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
                    PSP_Substation_Info sub = new PSP_Substation_Info();

                    string deviceid = ((XmlElement)e.SvgElement).GetAttribute("Deviceid");
                    //sub.AreaID = tlVectorControl1.SVGDocument.SvgdataUid;
                    PSP_Substation_Info _subTemp = DeviceHelper.GetDevice<PSP_Substation_Info>(deviceid);
                    // PSP_Substation_Info _subTemp = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoListByEleID", sub);
                    if (_subTemp != null)
                    {
                        fInfo.Info = "变电站名称：" + _subTemp.Title + " 容量：" + _subTemp.L2.ToString("##.##") + "MVA\r\n" + " 电压等级：" + _subTemp.L1.ToString("##.##") + "kV 最大负荷：" + _subTemp.L9.ToString("##.##") + "MW \r\n 投产年限：" + _subTemp.S2;
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
                if (aaa.Contains("kbs") || aaa.Contains("fjx") || aaa.Contains("pds") || aaa.Contains("byq") || aaa.Contains("hwg") || aaa.Contains("kg"))
                {
                    string deviceid = ((XmlElement)e.SvgElement).GetAttribute("Deviceid");
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
                    if (aaa.Contains("hwg"))
                    {
                        s_name = "环网柜";
                    }
                    if (aaa.Contains("kg"))
                    {
                        s_name = "柱上开关";
                    }
                    if (aaa.Contains("pds"))
                    {
                        s_name = "配电室";
                    }
                    PSPDEV _subTemp = new PSPDEV();
                    _subTemp.SUID = deviceid;
                    _subTemp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", _subTemp);
                    //PSP_Gra_item sub = new PSP_Gra_item();
                    //sub.EleID = e.SvgElement.ID;
                    //sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    //sub.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                    //PSP_Gra_item _subTemp = (PSP_Gra_item)Services.BaseService.GetObject("SelectPSP_Gra_itemByEleIDKey", sub);
                    if (_subTemp != null)
                    {
                        fInfo.Info = s_name + " 编号：" + _subTemp.EleID + " 名称：" + _subTemp.Name + " 电压等级：" + _subTemp.RateVolt + "kV";
                    }
                    else
                    {
                        fInfo.Info = s_name; // +"编号：   \r\n 名称： ";
                    }
                    fInfo.Top = e.Mouse.Y;
                    fInfo.Left = e.Mouse.X;
                    fInfo.Width = (fInfo.Info.Length) * 15;
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
            try
            {
                scaleBox.ComboBoxEx.SelectedIndexChanged -= new EventHandler(ComboBoxScaleBox_SelectedIndexChanged);
                this.scaleBox.ComboBoxEx.Text = text1;
                scaleBox.ComboBoxEx.SelectedIndexChanged += new EventHandler(ComboBoxScaleBox_SelectedIndexChanged);
            }
            catch { }
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
                    item.Width = 150;
                    if (item != null)
                        item.Text = "屏幕坐标 " + tooltip;
                    LabelItem jwditem = this.dotNetBarManager1.GetItem("jwd") as LabelItem;
                    if (jwditem != null)
                        tip = tooltip.Split(',');

                    LabelItem tipInfo = (LabelItem)this.dotNetBarManager1.GetItem("plTip");
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
                            ((MapViewBase)mapview).ZoneWide = 3;
                            double[] s = ((MapViewBase)mapview).LBtoXY54(Convert.ToDouble(temp.Longitude), Convert.ToDouble(temp.Latitude));
                            //jwditem.Width = 450;
                            //tipInfo.Text = temp.Longitude.ToString("###.######") + "," + temp.Latitude.ToString("###.######");
                            tipInfo.Text = "经纬度: " + d1 + "°" + f1 + "′" + m1.ToString("##.#") + "″," + d2 + "°" + f2 + "′" + m2.ToString("##.#") + "″";
                            jwditem.Text = "北京54坐标： " + Convert.ToInt32(s[0]) + "," + Convert.ToInt32(s[1]);
                            // jwditem.Text = "经纬度: " + d1.ToString() + "°" + f1.ToString() + "′" + m1.ToString("##.#") + "″," + d2.ToString() + "°" + f2.ToString() + "′" + m2.ToString("##.#") + "″";
                        }
                        catch { }
                    }
                }
                //else if (TipType == 1)
                //{
                //    LabelItem item = (LabelItem)this.dotNetBarManager1.GetItem("plTip");
                //    if (item != null)
                //        item.Text = tooltip;
                //}
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
                if (e.ClickedItem.Text=="可靠性分析")
                {
                    XmlElement xml1 = (XmlElement)tlVectorControl1.SVGDocument.CurrentElement;
                    if (tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline")
                    {
                        string deviceid = xml1.GetAttribute("Deviceid");
                        if (!string.IsNullOrEmpty(deviceid))
                            {
                                DeviceHelper.uid = tlVectorControl1.SVGDocument.SvgdataUid;
                                DeviceHelper.layerid = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                DeviceHelper.eleid = tlVectorControl1.SVGDocument.CurrentElement.ID;

                               PSPDEV obj = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(deviceid);
                                if (obj != null&&obj.Type=="73")
                                {
                                    //更换为元件可靠性
                                    Itop.TLPSP.DEVICE.FrmpdrelProject xf = new Itop.TLPSP.DEVICE.FrmpdrelProject();
                                    xf.init(obj);
                                    xf.ShowDialog();
                                }
           
                              }
                       }
                       
                       }
                if (e.ClickedItem.Text == "属性")
                {
                    XmlElement xml1 = (XmlElement)tlVectorControl1.SVGDocument.CurrentElement;
                    //PointF[] pf = TLMath.getPolygonPoints(xml1);
                    DeviceHelper.xml1 = xml1;
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

                            string str_id = "";
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
                                if (use.GetAttribute("xlink:href").Contains("byq") || use.GetAttribute("xlink:href").Contains("pds"))
                                {
                                    if (selectAreaPath.IsVisible(use.CenterPoint))
                                    {
                                        if (use.GetAttribute("Deviceid") != "")
                                        {
                                            str_id = str_id + "'" + use.GetAttribute("Deviceid") + "',";
                                        }
                                    }
                                }
                                if (use.GetAttribute("xlink:href").Contains("Substation"))
                                {
                                    //string strMatrix = use.GetAttribute("transform");
                                    //if (strMatrix != "")
                                    //{
                                    //    strMatrix = strMatrix.Replace("matrix(", "");
                                    //    strMatrix = strMatrix.Replace(")", "");
                                    //    string[] mat = strMatrix.Split(',');
                                    //    if (mat.Length > 5)
                                    //    {
                                    //        OffX = Convert.ToSingle(mat[4]);
                                    //        OffY = Convert.ToSingle(mat[5]);
                                    //    }
                                    //}
                                    if (frmlar.getSelectedLayer().Contains(use.GetAttribute("layer")))
                                    {
                                        ck = true;
                                    }
                                    PointF TempPoint = TLMath.getUseOffset(use.GetAttribute("xlink:href"));
                                    //if (selectAreaPath.IsVisible(use.X + TempPoint.X + OffX, use.Y + TempPoint.Y + OffY) && ck)
                                    if (selectAreaPath.IsVisible(use.CenterPoint) && ck)
                                    {
                                        if (use.GetAttribute("xlink:href").Contains("220"))
                                        {
                                            str220 = str220 + "'" + use.GetAttribute("Deviceid") + "',";
                                        }
                                        if (use.GetAttribute("xlink:href").Contains("110"))
                                        {
                                            str110 = str110 + "'" + use.GetAttribute("Deviceid") + "',";
                                        }
                                        if (use.GetAttribute("xlink:href").Contains("66"))
                                        {
                                            str66 = str66 + "'" + use.GetAttribute("Deviceid") + "',";
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
                            if (str_id.Length > 1)
                            {
                                str_id = str_id.Substring(0, str_id.Length - 1);
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
                                f.strID = str_id;
                                f.InitData(_gle, str220, str110, str66);
                                PointF[] pn = (PointF[])((Polygon)xml1).Points.Clone();
                                ((Polygon)xml1).Transform.Matrix.TransformPoints(pn);
                                string s1 = "";
                                for (int p = 0; p < pn.Length; p++)
                                {
                                    s1 = s1 + pn[p].X.ToString() + " " + pn[p].Y.ToString() + ",";
                                }
                                f.Str = s1;
                                f.ShowDialog();
                                if (f.checkBox1.Checked == false)
                                {
                                    tlVectorControl1.SVGDocument.RootElement.RemoveChild(tlVectorControl1.SVGDocument.CurrentElement);
                                }
                                //tlVectorControl1.Refresh();
                            }
                            else
                            {
                                _gle = new glebeProperty();
                                _gle.LayerID = SvgDocument.currentLayer;
                                frmMainProperty f = new frmMainProperty();
                                f.strID = str_id;
                                f.InitData(_gle, str220, str110, str66);
                                PointF[] pn = (PointF[])((Polygon)xml1).Points.Clone();
                                ((Polygon)xml1).Transform.Matrix.TransformPoints(pn);
                                string s1 = "";
                                for (int p = 0; p < pn.Length; p++)
                                {
                                    s1 = s1 + pn[p].X.ToString() + " " + pn[p].Y.ToString() + ",";
                                }
                                f.Str = s1;
                                f.ShowDialog();
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


                                frmProperty f = new frmProperty();//地块属性
                                if (SelUseArea == "") { SelUseArea = "0"; }
                                f.InitData(xml1.GetAttribute("id"), tlVectorControl1.SVGDocument.SvgdataUid, SelUseArea, rzb, SvgDocument.currentLayer);
                                //f.ShowDialog();
#if(!CITY)
                                //将其中心点保存在XML中
                                PointF p = TLMath.polyCentriod((XmlElement)tlVectorControl1.SVGDocument.CurrentElement);
                                string title = p.X.ToString() + "," + p.Y.ToString();
                                ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).SetAttribute("centrpoint", title);
                                if (progtype != "城市规划层")
                                {
                                    f.IsReadonly = true;
                                }
#endif
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
                    if (!Wjghboolflag)
                    {
                        if (tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Line" || tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline")
                        {
                            string lineWidth = "2";
                            string IsLead = ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).GetAttribute("IsLead");
                            //if (IsLead != "")       //原先导线的属性添加情况



                            if (IsLead != "")       //修改后的导线的属性添加情况
                            {
                                XmlNodeList n11 = tlVectorControl1.SVGDocument.SelectNodes("svg/polygon [@IsArea='1']");
                                using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                                {
                                    List<glebeProperty> glist = new List<glebeProperty>();
                                    for (int i = 0; i < n11.Count; i++)
                                    {
                                        IGraph graph1 = (IGraph)n11[i];
                                        GraphicsPath path1 = (GraphicsPath)graph1.GPath.Clone();
                                        //path1.Transform(graph1.GraphTransform.Matrix);
                                        Region ef1 = new Region(path1);

                                        Polyline line = tlVectorControl1.SVGDocument.CurrentElement as Polyline;
                                        GraphicsPath gr2 = new GraphicsPath();
                                        //gr2.AddLines(pfs);
                                        gr2.AddPath(line.GPath, true);
                                        gr2.CloseFigure();
                                        Region region = new Region(gr2);
                                        ef1.Intersect(region);
                                        if (!ef1.GetBounds(g).IsEmpty)
                                        {
                                            glebeProperty gy = new glebeProperty();
                                            gy.EleID = graph1.ID;
                                            gy.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                            gy = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", gy);
                                            if (gy != null)
                                            {
                                                glebeType gt = new glebeType();
                                                gt.UID = gy.TypeUID;
                                                gt = (glebeType)Services.BaseService.GetObject("SelectglebeTypeByKey", gt);
                                                gy.TypeUID = gt.TypeName;
                                                glist.Add(gy);
                                            }
                                        }
                                    }
                                    DeviceHelper.glist = glist;
                                }
                                string dhx = xml1.GetAttribute("dhx_key");
                                if (dhx == "1" && checkEdit2.Checked == false)
                                {
                                    frmDHXdlg d = new frmDHXdlg();
                                    d.uid = xml1.GetAttribute("id");
                                    d.Show();
                                    return;
                                }

                                PSPDEV obj = new PSPDEV();
                                string deviceid = xml1.GetAttribute("Deviceid");
                                DeviceHelper.pspflag = false;
                                DeviceHelper.Wjghflag = false;
                                if (string.IsNullOrEmpty(deviceid))
                                {
                                    string[] deviceType = new string[] { "05", "73", "75" };
                                    string xlwhere = " where SUID not in (";
                                    XmlNodeList lslist = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1'] [@Deviceid!=''] [@layer='" + SvgDocument.currentLayer + "']");
                                    for (int x1 = 0; x1 < lslist.Count; x1++)
                                    {
                                        XmlElement _node = lslist[x1] as XmlElement;
                                        xlwhere = xlwhere + "'" + _node.GetAttribute("Deviceid") + "',";
                                    }
                                    if (xlwhere.Length > 20)
                                    {
                                        xlwhere = xlwhere.Substring(0, xlwhere.Length - 1);
                                        xlwhere = xlwhere + ") and ";
                                    }
                                    else
                                    {
                                        xlwhere = "";
                                    }
                                    DeviceHelper.xlwhere = xlwhere;
                                    obj = (PSPDEV)DeviceHelper.SelectDevice(Itop.Client.MIS.ProgUID, deviceType);
                                    DeviceHelper.xlwhere = "";
                                    if (obj is PSPDEV)
                                    {
                                        deviceid = ((PSPDEV)obj).SUID;
                                        xml1.SetAttribute("Deviceid", deviceid);
                                        obj.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        obj.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        obj.EleID = ((SvgElement)xml1).ID;
                                        Services.BaseService.Update<PSPDEV>(obj);

                                        //将其设备加入到计算方案中
                                        //获得方案名称
                                        PSP_ELCPROJECT pd = new PSP_ELCPROJECT();
                                        pd.ID = frmlar.FAID;
                                        pd = Services.BaseService.GetOneByKey<PSP_ELCPROJECT>(pd);
                                        if (pd != null)
                                        {
                                            bool operflag = false, dataflag = false;
                                            if (!string.IsNullOrEmpty(((PSPDEV)obj).OperationYear) && ((PSPDEV)obj).OperationYear.Length == 4 && pd.BelongYear.Length == 4)
                                            {
                                                if (Convert.ToInt32(((PSPDEV)obj).OperationYear) < Convert.ToInt32(pd.BelongYear))
                                                {
                                                    operflag = true;
                                                }
                                            }
                                            if (!string.IsNullOrEmpty(((PSPDEV)obj).Date2) && ((PSPDEV)obj).Date2.Length == 4 && pd.BelongYear.Length == 4)
                                            {
                                                if (Convert.ToInt32(((PSPDEV)obj).Date2) > Convert.ToInt32(pd.BelongYear))
                                                {
                                                    dataflag = true;
                                                }
                                            }
                                            if (operflag && dataflag)
                                            {
                                                PSP_ElcDevice elcDevice = new PSP_ElcDevice();
                                                elcDevice.DeviceSUID = deviceid;
                                                elcDevice.ProjectSUID = frmlar.FAID;
                                                elcDevice = UCDeviceBase.DataService.GetOneByKey<PSP_ElcDevice>(elcDevice);
                                                if (elcDevice == null)
                                                {
                                                    elcDevice = new PSP_ElcDevice();
                                                    elcDevice.DeviceSUID = deviceid;
                                                    elcDevice.ProjectSUID = frmlar.FAID;
                                                    UCDeviceBase.DataService.Create<PSP_ElcDevice>(elcDevice);
                                                }
                                            }

                                        }
                                     
                                      
                                    }
                                }
                                if (!string.IsNullOrEmpty(deviceid))
                                {
                                    DeviceHelper.uid = tlVectorControl1.SVGDocument.SvgdataUid;
                                    DeviceHelper.layerid = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    DeviceHelper.eleid = tlVectorControl1.SVGDocument.CurrentElement.ID;

                                    obj = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(deviceid);
                                    if (obj != null)//------------wwwMX
                                    {
                                        xml1.SetAttribute("info-name", ((PSPDEV)obj).Name);
                                        if (obj.Type == "05")
                                        {
                                            DeviceHelper.ShowDeviceDlg(DeviceType.XL, deviceid, false);
                                        }
                                        if (obj.Type == "01")
                                        {
                                            DeviceHelper.ShowDeviceDlg(DeviceType.MX, deviceid, false);
                                        }
                                        if (obj.Type == "73")
                                        {
                                            DeviceHelper.ShowDeviceDlg(DeviceType.PDXL, deviceid, false);
                                        }
                                        if (obj.Type == "75")
                                        {
                                            DeviceHelper.ShowDeviceDlg(DeviceType.LUX, deviceid, false);
                                        }
                                    }



                                    //***** ********添加FistNode和LastNode
                                    XmlNodeList useList = tlVectorControl1.SVGDocument.SelectNodes("svg/use");

                                    foreach (XmlNode element in useList)
                                    {
                                        if (!string.IsNullOrEmpty((element as XmlElement).GetAttribute("Deviceid")))
                                        {
                                            string con = "WHERE SvgUID='" + (element as XmlElement).GetAttribute("Deviceid") + "'AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                                            IList pspMX = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                            if (pspMX != null)
                                            {
                                                foreach (PSPDEV pspmx in pspMX)
                                                {
                                                    if (obj.IName == pspmx.Name)
                                                    {
                                                        (xml1 as XmlElement).SetAttribute("FirstNode", (element as XmlElement).GetAttribute("id"));

                                                    }
                                                    else if (obj.JName == pspmx.Name)
                                                    {
                                                        (xml1 as XmlElement).SetAttribute("LastNode", (element as XmlElement).GetAttribute("id"));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //**

                            }
                        }

                        if (xml1.GetAttribute("xlink:href").Contains("Substation") || xml1.GetAttribute("xlink:href").Contains("Power"))
                        {//变电站属性
                            string lab = xml1.GetAttribute("xlink:href");

                            float x = 0f;
                            float y = 0f;
                           //判断电压等级
                              int   dyinfo = Convert.ToInt32(getDY(lab));
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

                            string strjwd = "经纬度: " + d1.ToString() + "°" + f1.ToString() + "′" + m1.ToString("##.#") + "″," + d2.ToString() + "°" + f2.ToString() + "′" + m2.ToString("##.#") + "″";
                            object obj = null;
                            string deviceid = xml1.GetAttribute("Deviceid");
                            DeviceHelper.pspflag = false;
                            DeviceHelper.Wjghflag = false;
                            if (dyinfo >= 66)
                            {
                                DeviceHelper.subflag = false;
                            }
                            else
                                DeviceHelper.subflag = true;
                        Lab1://变电站、电源属性
                            if (string.IsNullOrEmpty(deviceid))
                            {
                                //XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                if (xml1.GetAttribute("xlink:href").Contains("Power"))
                                {
                                    obj = DeviceHelper.SelectDevice(DeviceType.DY, Itop.Client.MIS.ProgUID);
                                    //if (obj == null)
                                    //{
                                    //    tlVectorControl1.SVGDocument.CurrentElement = xml1 as SvgElement;
                                    //    tlVectorControl1.Delete();
                                    //}
                                    if (obj is PSP_PowerSubstation_Info)
                                    {
                                        deviceid = ((PSP_PowerSubstation_Info)obj).UID;
                                        ((PSP_PowerSubstation_Info)obj).LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        //((PSP_PowerSubstation_Info)obj). = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        Services.BaseService.Update<PSP_PowerSubstation_Info>(((PSP_PowerSubstation_Info)obj));
                                        xml1.SetAttribute("Deviceid", deviceid);
                                        xml1.SetAttribute("info-name", ((PSP_PowerSubstation_Info)obj).Title);

                                        //获得方案名称
                                        PSP_ELCPROJECT pd = new PSP_ELCPROJECT();
                                        pd.ID = frmlar.FAID;
                                        pd = Services.BaseService.GetOneByKey<PSP_ELCPROJECT>(pd);
                                        if (pd != null)
                                        {
                                            string where = "where projectid='" + Itop.Client.MIS.ProgUID + "'and SvgUID='" + ((PSP_PowerSubstation_Info)obj).UID + "'";
                                            IList<PSPDEV> list = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", where);
                                            //根据年份进行筛选
                                            if (!string.IsNullOrEmpty(pd.BelongYear))   //根据参与计算设备属于那一年先进行一次筛选
                                            {
                                                for (int i = 0; i < list.Count; i++)
                                                {
                                                    if (!string.IsNullOrEmpty((list[i] as PSPDEV).OperationYear) && (list[i] as PSPDEV).OperationYear.Length == 4 && pd.BelongYear.Length == 4)
                                                    {
                                                        if (Convert.ToInt32((list[i] as PSPDEV).OperationYear) > Convert.ToInt32(pd.BelongYear))
                                                        {
                                                            list.RemoveAt(i);
                                                            i--;
                                                            continue;
                                                        }
                                                    }
                                                    if (!string.IsNullOrEmpty((list[i] as PSPDEV).Date2) && (list[i] as PSPDEV).Date2.Length == 4 && pd.BelongYear.Length == 4)
                                                    {
                                                        if (Convert.ToInt32((list[i] as PSPDEV).Date2) < Convert.ToInt32(pd.BelongYear))
                                                        {
                                                            list.RemoveAt(i);
                                                            i--;
                                                            continue;
                                                        }
                                                    }
                                                }
                                            }


                                            foreach (PSPDEV pv in list)
                                            {
                                                //将其设备加入到计算方案中
                                                PSP_ElcDevice elcDevice = new PSP_ElcDevice();
                                                elcDevice.DeviceSUID = pv.SUID;
                                                elcDevice.ProjectSUID = frmlar.FAID;
                                                elcDevice = UCDeviceBase.DataService.GetOneByKey<PSP_ElcDevice>(elcDevice);
                                                if (elcDevice == null)
                                                {
                                                    elcDevice = new PSP_ElcDevice();
                                                    elcDevice.DeviceSUID = pv.SUID;
                                                    elcDevice.ProjectSUID = frmlar.FAID;
                                                    UCDeviceBase.DataService.Create<PSP_ElcDevice>(elcDevice);
                                                }
                                            }
                                        }
                                    }

                                    substation sb = new substation();
                                    sb.UID = ((PSP_PowerSubstation_Info)obj).UID;
                                    sb = (substation)Services.BaseService.GetObject("SelectsubstationByKey", sb);
                                    if (sb != null)
                                    {
                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        sb.EleID = xml1.GetAttribute("id");
                                        if (((PSP_PowerSubstation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_PowerSubstation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Update<substation>(sb);
                                    }
                                    else
                                    {
                                        sb = new substation();
                                        sb.UID = ((PSP_PowerSubstation_Info)obj).UID;
                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.EleID = xml1.GetAttribute("id");
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        if (((PSP_PowerSubstation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_PowerSubstation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Create<substation>(sb);


                                    }
                                }
                                else
                                {
                                    DeviceHelper.uid = tlVectorControl1.SVGDocument.SvgdataUid;
                                    DeviceHelper.eleid = tlVectorControl1.SVGDocument.CurrentElement.ID;
                                    DeviceHelper.layerid = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    //wwww
                                    string bdzwhere = " UID not in (";

                                    XmlNodeList useList = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@Deviceid!=''] [@layer='" + SvgDocument.currentLayer + "']");

                                    for (int x2 = 0; x2 < useList.Count; x2++)
                                    {
                                        XmlElement _node = useList[x2] as XmlElement;
                                        bdzwhere = bdzwhere + "'" + _node.GetAttribute("Deviceid") + "',";
                                    }
                                    if (bdzwhere.Length > 13)
                                    {
                                        bdzwhere = bdzwhere.Substring(0, bdzwhere.Length - 1);
                                        bdzwhere = bdzwhere + ") and ";
                                    }
                                    else
                                    {
                                        bdzwhere = "";
                                    }

                                    DeviceHelper.bdzwhere = bdzwhere;
                                    obj = DeviceHelper.SelectDevice(DeviceType.BDZ, Itop.Client.MIS.ProgUID);
                                    DeviceHelper.bdzwhere = "";
                                    //if (obj == null)
                                    //{
                                    //    tlVectorControl1.SVGDocument.CurrentElement = xml1 as SvgElement;
                                    //    tlVectorControl1.Delete();
                                    //}
                                    if (obj is PSP_Substation_Info)
                                    {
                                        deviceid = ((PSP_Substation_Info)obj).UID;
                                        ((PSP_Substation_Info)obj).LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        ((PSP_Substation_Info)obj).EleID = tlVectorControl1.SVGDocument.CurrentElement.ID;
                                       
                                        Services.BaseService.Update<PSP_Substation_Info>(((PSP_Substation_Info)obj));
                                        xml1.SetAttribute("Deviceid", deviceid);
                                        xml1.SetAttribute("info-name", ((PSP_Substation_Info)obj).Title);

                                        //获得方案名称
                                        PSP_ELCPROJECT pd = new PSP_ELCPROJECT();
                                        pd.ID = frmlar.FAID;
                                        pd = Services.BaseService.GetOneByKey<PSP_ELCPROJECT>(pd);
                                        if (pd != null)
                                        {
                                            string where = "where projectid='" + Itop.Client.MIS.ProgUID + "'and SvgUID='" + ((PSP_Substation_Info)obj).UID + "'";
                                            IList<PSPDEV> list = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", where);
                                            //根据年份进行筛选
                                            if (!string.IsNullOrEmpty(pd.BelongYear))   //根据参与计算设备属于那一年先进行一次筛选
                                            {
                                                for (int i = 0; i < list.Count; i++)
                                                {
                                                    if (!string.IsNullOrEmpty((list[i] as PSPDEV).OperationYear) && (list[i] as PSPDEV).OperationYear.Length == 4 && pd.BelongYear.Length == 4)
                                                    {
                                                        if (Convert.ToInt32((list[i] as PSPDEV).OperationYear) > Convert.ToInt32(pd.BelongYear))
                                                        {
                                                            list.RemoveAt(i);
                                                            i--;
                                                            continue;
                                                        }
                                                    }
                                                    if (!string.IsNullOrEmpty((list[i] as PSPDEV).Date2) && (list[i] as PSPDEV).Date2.Length == 4 && pd.BelongYear.Length == 4)
                                                    {
                                                        if (Convert.ToInt32((list[i] as PSPDEV).Date2) < Convert.ToInt32(pd.BelongYear))
                                                        {
                                                            list.RemoveAt(i);
                                                            i--;
                                                            continue;
                                                        }
                                                    }
                                                }
                                            }

                                            foreach (PSPDEV pv in list)
                                            {
                                                //将其设备加入到计算方案中
                                                PSP_ElcDevice elcDevice = new PSP_ElcDevice();
                                                elcDevice.DeviceSUID = pv.SUID;
                                                elcDevice.ProjectSUID = frmlar.FAID;
                                                elcDevice = UCDeviceBase.DataService.GetOneByKey<PSP_ElcDevice>(elcDevice);
                                                if (elcDevice == null)
                                                {
                                                    elcDevice = new PSP_ElcDevice();
                                                    elcDevice.DeviceSUID = pv.SUID;
                                                    elcDevice.ProjectSUID = frmlar.FAID;
                                                    UCDeviceBase.DataService.Create<PSP_ElcDevice>(elcDevice);
                                                }
                                            }
                                        }

                                        //return;
                                        //根据变站创建线路
                                        createLine(xml1, deviceid);
                                    }
                                    /*
                                    substation sb = new substation();
                                    sb.UID = ((PSP_Substation_Info)obj).UID;
                                    sb = (substation)Services.BaseService.GetObject("SelectsubstationByKey", sb);
                                    if (sb != null)
                                    {

                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        sb.EleID = xml1.GetAttribute("id");
                                        if (((PSP_Substation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_Substation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Update<substation>(sb);
                                    }
                                    else
                                    {
                                        sb = new substation();
                                        sb.UID = ((PSP_Substation_Info)obj).UID;
                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.EleID = xml1.GetAttribute("id");
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        if (((PSP_Substation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_Substation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Create<substation>(sb);
                                    }*/
                                }
                            }
                            if (!string.IsNullOrEmpty(deviceid))
                            {
                                if (xml1.GetAttribute("xlink:href").Contains("Power"))
                                {
                                    DeviceHelper.uid = tlVectorControl1.SVGDocument.SvgdataUid;
                                    DeviceHelper.eleid = tlVectorControl1.SVGDocument.CurrentElement.ID;
                                    DeviceHelper.layerid = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(deviceid);
                                    if (obj != null)
                                    {
                                        DeviceHelper.StartYear = startyear;
                                        //XmlElement n1 = tlVectorControl1.SVGDocument.SelectSingleNode("/text[@ParentUID='" + xml1.GetAttribute("id") + "']");
                                        if (DeviceHelper.ShowDeviceDlg(DeviceType.DY, deviceid, false))
                                        {
                                            obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(deviceid);
                                            xml1.SetAttribute("info-name", ((PSP_PowerSubstation_Info)obj).Title);
                                        }
                                    }
                                    else
                                    {
                                        deviceid = ""; goto Lab1;
                                    }

                                    // re
                                    substation sb = new substation();
                                    sb.UID = ((PSP_PowerSubstation_Info)obj).UID;
                                    sb = (substation)Services.BaseService.GetObject("SelectsubstationByKey", sb);
                                    if (sb != null)
                                    {
                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        sb.EleID = xml1.GetAttribute("id");
                                        if (((PSP_PowerSubstation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_PowerSubstation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Update<substation>(sb);
                                    }
                                    else
                                    {
                                        sb = new substation();
                                        sb.UID = ((PSP_PowerSubstation_Info)obj).UID;
                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        sb.EleID = xml1.GetAttribute("id");
                                        if (((PSP_PowerSubstation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_PowerSubstation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Create<substation>(sb);
                                    }
                                }
                                else
                                {
                                    obj = DeviceHelper.GetDevice<PSP_Substation_Info>(deviceid);
                                    if (obj != null)
                                    {
                                        DeviceHelper.StartYear = startyear;
                                        //XmlElement n1 = tlVectorControl1.SVGDocument.SelectSingleNode("/text[@ParentUID='" + xml1.GetAttribute("id") + "']");
                                        if (DeviceHelper.ShowDeviceDlg(DeviceType.BDZ, deviceid, false))
                                        {
                                            obj = DeviceHelper.GetDevice<PSP_Substation_Info>(deviceid);
                                            xml1.SetAttribute("info-name", ((PSP_Substation_Info)obj).Title);
                                        }
                                    }
                                    else
                                    {
                                        deviceid = ""; goto Lab1;
                                    }
                                    substation sb = new substation();
                                    sb.UID = ((PSP_Substation_Info)obj).UID;
                                    sb = (substation)Services.BaseService.GetObject("SelectsubstationByKey", sb);
                                    if (sb != null)
                                    {
                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        sb.EleID = xml1.GetAttribute("id");
                                        if (((PSP_Substation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_Substation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Update<substation>(sb);
                                    }
                                    else
                                    {
                                        sb = new substation();
                                        sb.UID = ((PSP_Substation_Info)obj).UID;
                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.EleID = xml1.GetAttribute("id");
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        if (((PSP_Substation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_Substation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Create<substation>(sb);
                                    }
                                }

                            }

                        }
                        if (xml1.GetAttribute("xlink:href").Contains("XL_GT_3") || xml1.GetAttribute("xlink:href").Contains("XL_GT_4"))
                        {
                            frmInputNum num = new frmInputNum();
                            num.InputStr = xml1.GetAttribute("order");
                            num.ShowDialog();
                            xml1.SetAttribute("order", num.InputStrSEL);
                        }
                        if (xml1.GetAttribute("xlink:href").Contains("hwg") || xml1.GetAttribute("xlink:href").Contains("pds") ||
                            xml1.GetAttribute("xlink:href").Contains("fjx") || xml1.GetAttribute("xlink:href").Contains("kbs") ||
                            xml1.GetAttribute("xlink:href").Contains("byq") || xml1.GetAttribute("xlink:href").Contains("kg") ||
                             xml1.GetAttribute("xlink:href").Contains("gt"))
                        {
                            //frmInputDialog n1 = new frmInputDialog();
                            //n1.InputStr = xml1.GetAttribute("info-name").ToString();
                            //if (n1.ShowDialog() == DialogResult.OK)
                            //{
                            //    xml1.SetAttribute("info-name", n1.InputStr);
                            //}
                            PSPDEV obj = new PSPDEV();
                            string deviceid = xml1.GetAttribute("Deviceid");
                            DeviceHelper.pspflag = false;
                            DeviceHelper.Wjghflag = false;
                            if (string.IsNullOrEmpty(deviceid))
                            {
                                if (xml1.GetAttribute("xlink:href").Contains("kbs"))
                                {
                                    obj = (PSPDEV)DeviceHelper.SelectDevice(DeviceType.KBS, Itop.Client.MIS.ProgUID);
                                }
                                if (xml1.GetAttribute("xlink:href").Contains("fjx"))
                                {
                                    obj = (PSPDEV)DeviceHelper.SelectDevice(DeviceType.FZX, Itop.Client.MIS.ProgUID);
                                }
                                if (xml1.GetAttribute("xlink:href").Contains("hwg"))
                                {
                                    obj = (PSPDEV)DeviceHelper.SelectDevice(DeviceType.HWG, Itop.Client.MIS.ProgUID);
                                }
                                if (xml1.GetAttribute("xlink:href").Contains("kg"))
                                {
                                    obj = (PSPDEV)DeviceHelper.SelectDevice(DeviceType.ZSKG, Itop.Client.MIS.ProgUID);
                                }
                                if (xml1.GetAttribute("xlink:href").Contains("pds"))
                                {
                                    obj = (PSPDEV)DeviceHelper.SelectDevice(DeviceType.PDS, Itop.Client.MIS.ProgUID);
                                }
                                if (xml1.GetAttribute("xlink:href").Contains("byq"))
                                {
                                    obj = (PSPDEV)DeviceHelper.SelectDevice(Itop.Client.MIS.ProgUID, "51", "52");
                                }
                                //if (xml1.GetAttribute("xlink:href").Contains("gt"))
                                //{
                                //    obj = (PSPDEV)DeviceHelper.SelectDevice(DeviceType.GT, Itop.Client.MIS.ProgUID);
                                //}
                                if (obj is PSPDEV)
                                {
                                    deviceid = ((PSPDEV)obj).SUID;
                                    xml1.SetAttribute("Deviceid", deviceid);
                                    obj.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                    obj.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                    // obj.EleID = ((SvgElement)xml1).ID;
                                    Services.BaseService.Update<PSPDEV>(obj);
                                }
                            }
                            if (!string.IsNullOrEmpty(deviceid))
                            {
                                DeviceHelper.uid = tlVectorControl1.SVGDocument.SvgdataUid;
                                DeviceHelper.layerid = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                DeviceHelper.eleid = tlVectorControl1.SVGDocument.CurrentElement.ID;

                                obj = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(deviceid);
                                if (obj != null)//------------wwwMX
                                {
                                    xml1.SetAttribute("info-name", ((PSPDEV)obj).Name);
                                    DeviceHelper.ShowDeviceDlg((DeviceType)int.Parse(obj.Type), deviceid, false);
                                    //if (obj.Type == "54")
                                    //{
                                    //    DeviceHelper.ShowDeviceDlg(DeviceType.KBS, deviceid, false);
                                    //}
                                    //if (obj.Type == "56")
                                    //{
                                    //    DeviceHelper.ShowDeviceDlg(DeviceType.HWG, deviceid, false);
                                    //}
                                    //if (obj.Type == "58")
                                    //{
                                    //    DeviceHelper.ShowDeviceDlg(DeviceType.FZX, deviceid, false);
                                    //}
                                    //if (obj.Type == "51" )
                                    //{
                                    //    DeviceHelper.ShowDeviceDlg(DeviceType.FZX, deviceid, false);
                                    //}
                                    //if (obj.Type == "52")
                                    //{
                                    //    DeviceHelper.ShowDeviceDlg(DeviceType.FZX, deviceid, false);
                                    //}
                                }
                            }

                        }
                        /* if (xml1.GetAttribute("xlink:href").Contains("kbs") || xml1.GetAttribute("xlink:href").Contains("hwg"))
                         {
                             //frmkbsProperty num = new frmkbsProperty();
                             //num.InitData(((SvgElement)xml1).ID, tlVectorControl1.SVGDocument.SvgdataUid, tlVectorControl1.SVGDocument.CurrentLayer.ID);
                             //num.ShowDialog();
                           
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
                         } */
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
                    #region 网架优化的属性输入


                    if (Wjghboolflag)
                    {
                        checkwjghelement();
                        if (tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Line" || tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline")
                        {
                            string lineWidth = "2";
                            string IsLead = ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).GetAttribute("IsLead");
                            if (IsLead != "")       //修改后的导线的属性添加情况
                            {
                                PSPDEV obj = new PSPDEV();
                                string deviceid = xml1.GetAttribute("Deviceid");
                                DeviceHelper.pspflag = false;
                                DeviceHelper.Wjghflag = true;
                                DeviceHelper.wjghuid = ff.Key;
                                if (string.IsNullOrEmpty(deviceid))
                                {
                                    obj = (PSPDEV)DeviceHelper.SelectDevice(DeviceType.XL, Itop.Client.MIS.ProgUID);
                                    if (obj == null)
                                    {
                                        tlVectorControl1.SVGDocument.CurrentElement = xml1 as SvgElement;
                                        tlVectorControl1.Delete();
                                    }
                                    if (obj is PSPDEV)
                                    {
                                        deviceid = ((PSPDEV)obj).SUID;
                                        xml1.SetAttribute("Deviceid", deviceid);
                                        obj.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        obj.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        obj.EleID = ((SvgElement)xml1).ID;
                                        Services.BaseService.Update<PSPDEV>((PSPDEV)obj);
                                    }
                                }
                                if (!string.IsNullOrEmpty(deviceid))
                                {
                                    obj = (PSPDEV)DeviceHelper.GetDevice<PSPDEV>(deviceid);
                                    if (obj != null)
                                    {
                                        xml1.SetAttribute("info-name", ((PSPDEV)obj).Name);
                                        DeviceHelper.ShowDeviceDlg(DeviceType.XL, deviceid, false);
                                    }

                                    LineInfo li = new LineInfo();
                                    li.UID = obj.SUID;
                                    li = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByKey", li);
                                    if (li != null)
                                    {
                                        li.LayerID = SvgDocument.currentLayer;
                                        li.EleID = xml1.GetAttribute("id");
                                        li.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        LineType lt = new LineType();
                                        lt.TypeName = li.Voltage.ToString() + "kV";
                                        lt = (LineType)Services.BaseService.GetObject("SelectLineTypeByTypeName", lt);
                                        li.ObligateField3 = obj.OperationYear;
                                        li.ObligateField2 = lt.Color;
                                        lineWidth = lt.ObligateField1;
                                        if (!string.IsNullOrEmpty(li.ObligateField3))
                                        {
                                            if (Convert.ToInt32(obj.OperationYear) > DateTime.Now.Year)
                                            {
                                                li.ObligateField1 = "规划";
                                            }
                                            else
                                                li.ObligateField1 = "运行";
                                        }
                                        Services.BaseService.Update<LineInfo>(li);
                                    }
                                    else
                                    {
                                        li = new LineInfo();
                                        li.UID = obj.SUID;
                                        li.LineName = obj.Name;
                                        li.Length = obj.LineLength.ToString();
                                        li.LineType = obj.LineType;
                                        li.Voltage = obj.ReferenceVolt.ToString();
                                        li.EleID = xml1.GetAttribute("id");
                                        li.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        LineType lt = new LineType();
                                        lt.TypeName = obj.ReferenceVolt.ToString() + "kV";
                                        lt = (LineType)Services.BaseService.GetObject("SelectLineTypeByTypeName", lt);
                                        li.ObligateField3 = obj.OperationYear;
                                        li.ObligateField2 = lt.Color;
                                        lineWidth = lt.ObligateField1;
                                        if (!string.IsNullOrEmpty(li.ObligateField3))
                                        {
                                            if (Convert.ToInt32(obj.OperationYear) > DateTime.Now.Year)
                                            {
                                                li.ObligateField1 = "规划";
                                            }
                                            else
                                                li.ObligateField1 = "运行";
                                        }

                                        Services.BaseService.Create<LineInfo>(li);
                                    }

                                    string styleValue = "";
                                    if (li.ObligateField1 == "规划")
                                    {
                                        styleValue = "stroke-dasharray:" + ghType + ";stroke-width:" + lineWidth + ";";
                                    }
                                    else
                                    {
                                        styleValue = "stroke-width:" + lineWidth + ";";
                                    }
                                    //string aa= ColorTranslator.ToHtml(Color.Black);
                                    styleValue = styleValue + "stroke:" + ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(li.ObligateField2)));
                                    SvgElement se = tlVectorControl1.SVGDocument.CurrentElement;
                                    se.RemoveAttribute("style");
                                    se.SetAttribute("style", styleValue);
                                    se.SetAttribute("info-name", li.LineName);
                                    //***** ********添加FistNode和LastNode
                                    XmlNodeList useList = tlVectorControl1.SVGDocument.SelectNodes("svg/use");

                                    foreach (XmlNode element in useList)
                                    {
                                        if (!string.IsNullOrEmpty((element as XmlElement).GetAttribute("Deviceid")))
                                        {
                                            string con = "WHERE SvgUID='" + (element as XmlElement).GetAttribute("Deviceid") + "'AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                                            IList pspMX = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                                            if (pspMX != null)
                                            {
                                                foreach (PSPDEV pspmx in pspMX)
                                                {
                                                    if (obj.IName == pspmx.Name)
                                                    {
                                                        (xml1 as XmlElement).SetAttribute("FirstNode", (element as XmlElement).GetAttribute("id"));

                                                    }
                                                    else if (obj.JName == pspmx.Name)
                                                    {
                                                        (xml1 as XmlElement).SetAttribute("LastNode", (element as XmlElement).GetAttribute("id"));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //**

                            }
                        }

                        if (xml1.GetAttribute("xlink:href").Contains("Substation") || xml1.GetAttribute("xlink:href").Contains("Power"))
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

                            string strjwd = "经纬度: " + d1.ToString() + "°" + f1.ToString() + "′" + m1.ToString("##.#") + "″," + d2.ToString() + "°" + f2.ToString() + "′" + m2.ToString("##.#") + "″";
                            object obj = null;
                            string deviceid = xml1.GetAttribute("Deviceid");
                            DeviceHelper.pspflag = false;
                            DeviceHelper.Wjghflag = true;
                            if (string.IsNullOrEmpty(deviceid))
                            {
                                //XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                if (xml1.GetAttribute("xlink:href").Contains("Power"))
                                {
                                    obj = DeviceHelper.SelectDevice(DeviceType.DY, Itop.Client.MIS.ProgUID);
                                    if (obj == null)
                                    {
                                        tlVectorControl1.SVGDocument.CurrentElement = xml1 as SvgElement;
                                        tlVectorControl1.Delete();
                                    }
                                    if (obj is PSP_PowerSubstation_Info)
                                    {
                                        deviceid = ((PSP_PowerSubstation_Info)obj).UID;
                                        ((PSP_PowerSubstation_Info)obj).LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;


                                        Services.BaseService.Update<PSP_PowerSubstation_Info>(((PSP_PowerSubstation_Info)obj));
                                        xml1.SetAttribute("Deviceid", deviceid);
                                    }
                                    substation sb = new substation();
                                    sb.UID = ((PSP_PowerSubstation_Info)obj).UID;
                                    sb = (substation)Services.BaseService.GetObject("SelectsubstationByKey", sb);
                                    if (sb != null)
                                    {
                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        sb.EleID = xml1.GetAttribute("id");
                                        if (((PSP_PowerSubstation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_PowerSubstation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Update<substation>(sb);
                                    }
                                    else
                                    {
                                        sb = new substation();
                                        sb.UID = ((PSP_PowerSubstation_Info)obj).UID;
                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.EleID = xml1.GetAttribute("id");
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        if (((PSP_PowerSubstation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_PowerSubstation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Create<substation>(sb);
                                    }
                                }
                                else
                                {
                                    obj = DeviceHelper.SelectDevice(DeviceType.BDZ, Itop.Client.MIS.ProgUID);
                                    if (obj == null)
                                    {
                                        tlVectorControl1.SVGDocument.CurrentElement = xml1 as SvgElement;
                                        tlVectorControl1.Delete();
                                    }
                                    if (obj is PSP_Substation_Info)
                                    {
                                        deviceid = ((PSP_Substation_Info)obj).UID;
                                        ((PSP_Substation_Info)obj).LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        ((PSP_Substation_Info)obj).EleID = tlVectorControl1.SVGDocument.CurrentElement.ID;
                                        Services.BaseService.Update<PSP_Substation_Info>(((PSP_Substation_Info)obj));
                                        xml1.SetAttribute("Deviceid", deviceid);

                                    }
                                    substation sb = new substation();
                                    sb.UID = ((PSP_Substation_Info)obj).UID;
                                    sb = (substation)Services.BaseService.GetObject("SelectsubstationByKey", sb);
                                    if (sb != null)
                                    {

                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        sb.EleID = xml1.GetAttribute("id");
                                        if (((PSP_Substation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_Substation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Update<substation>(sb);
                                    }
                                    else
                                    {
                                        sb = new substation();
                                        sb.UID = ((PSP_Substation_Info)obj).UID;
                                        sb.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        sb.EleID = xml1.GetAttribute("id");
                                        sb.LayerID = tlVectorControl1.SVGDocument.CurrentLayer.ID;
                                        if (((PSP_Substation_Info)obj).Flag == "2")
                                        {
                                            sb.ObligateField3 = "规划";
                                        }
                                        else if (((PSP_Substation_Info)obj).Flag == "1")
                                        {
                                            sb.ObligateField3 = "现行";
                                        }
                                        Services.BaseService.Create<substation>(sb);
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(deviceid))
                            {
                                if (xml1.GetAttribute("xlink:href").Contains("Power"))
                                {
                                    obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(deviceid);
                                    if (obj != null)
                                    {
                                        xml1.SetAttribute("info-name", ((PSP_PowerSubstation_Info)obj).Title);
                                        //XmlElement n1 = tlVectorControl1.SVGDocument.SelectSingleNode("/text[@ParentUID='" + xml1.GetAttribute("id") + "']");
                                        DeviceHelper.ShowDeviceDlg(DeviceType.DY, deviceid, false);
                                    }

                                }
                                else
                                {
                                    obj = DeviceHelper.GetDevice<PSP_Substation_Info>(deviceid);
                                    if (obj != null)
                                    {
                                        xml1.SetAttribute("info-name", ((PSP_Substation_Info)obj).Title);
                                        //XmlElement n1 = tlVectorControl1.SVGDocument.SelectSingleNode("/text[@ParentUID='" + xml1.GetAttribute("id") + "']");
                                        DeviceHelper.ShowDeviceDlg(DeviceType.BDZ, deviceid, false);
                                    }

                                }

                            }

                        }
                        if (xml1.GetAttribute("xlink:href").Contains("XL_GT_3") || xml1.GetAttribute("xlink:href").Contains("XL_GT_4"))
                        {
                            frmInputNum num = new frmInputNum();
                            num.InputStr = xml1.GetAttribute("order");
                            num.ShowDialog();
                            xml1.SetAttribute("order", num.InputStrSEL);
                        }

                        //if (xml1.GetAttribute("xlink:href").Contains("kbs") || xml1.GetAttribute("xlink:href").Contains("hwg"))
                        //{
                        //    frmkbsProperty num = new frmkbsProperty();
                        //    num.InitData(((SvgElement)xml1).ID, tlVectorControl1.SVGDocument.SvgdataUid, tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        //    num.ShowDialog();
                        //}
                        //if (xml1.GetAttribute("xlink:href").Contains("fjx"))
                        //{
                        //    frmfjxProperty num = new frmfjxProperty();
                        //    num.InitData(((SvgElement)xml1).ID, tlVectorControl1.SVGDocument.SvgdataUid, tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        //    num.ShowDialog();
                        //}
                        //if (xml1.GetAttribute("xlink:href").Contains("byq"))
                        //{
                        //    frmbyqProperty num = new frmbyqProperty();
                        //    num.InitData(((SvgElement)xml1).ID, tlVectorControl1.SVGDocument.SvgdataUid, tlVectorControl1.SVGDocument.CurrentLayer.ID);
                        //    num.ShowDialog();
                        //}
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
                    #endregion
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
                    fm.Init(strj1, strw1, strd1, strj2, strw2, strd2);
                    if (fm.ShowDialog() == DialogResult.OK)
                    {
                        string strValue = fm.StrValue;
                        string[] str = strValue.Split(',');
                        string[] JWD1 = str[0].Split(' ');
                        decimal J1 = Convert.ToDecimal(JWD1[0]);
                        decimal W1 = Convert.ToDecimal(JWD1[1]);
                        decimal D1 = Convert.ToDecimal(JWD1[2]);
                        string[] JWD2 = str[1].Split(' ');
                        decimal J2 = Convert.ToDecimal(JWD2[0]);
                        decimal W2 = Convert.ToDecimal(JWD2[1]);
                        decimal D2 = Convert.ToDecimal(JWD2[2]);

                        decimal JD = J1 + W1 / 60 + D1 / 3600;
                        decimal WD = J2 + W2 / 60 + D2 / 3600;


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
                        if (e1.SvgAttributes.ContainsKey("transform"))
                        {
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

                        PrintHelper ph = new PrintHelper(tlVectorControl1, mapview);
                        frmImgManager frm = new frmImgManager();
                        frm.Pic = ph.getImage();
                        frm.ShowDialog();
                    }
                }

            }
            catch (Exception e1)
            {
                //MessageBox.Show(e1.Message);
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
            if (e.ClickedItem.Text == "清除关联")
            {
                ((XmlElement)tlVectorControl1.SVGDocument.CurrentElement).RemoveAttribute("Deviceid");
            }
            if (e.ClickedItem.Text == "更新关联变电站")
            {
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

                        Polyline pol = tlVectorControl1.SVGDocument.CurrentElement as Polyline;
                        PointF[] tt = pol.Pt;
                        double x1 = tt[0].X;
                        double x2 = tt[tt.Length - 1].X;
                        double y1 = tt[0].Y;
                        double y2 = tt[tt.Length - 1].Y;

                        if ((element as XmlElement).GetAttribute("xlink:href").Contains("Substation"))
                        {
                            //(element as XmlElement).SetAttribute("stroke", "#FF0000");
                            if (Math.Abs(uset.X - x1) < ((t.Height) / 2) && Math.Abs(uset.Y - y1) < ((t.Height) / 2))
                            {

                                (pol as XmlElement).SetAttribute("FirstNode", element.ID);
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
                MessageBox.Show("更新完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private string getBdzName(string id)
        {

            PSP_Substation_Info sub = new PSP_Substation_Info();
            sub.EleID = id;
            //sub.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
            IList svglist20 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByEleID", sub);
            sub = null;
            string ret = string.Empty;
            if (svglist20.Count > 0)
            {
                sub = (PSP_Substation_Info)svglist20[0];
                ret = sub.Title;
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
                        this.Close();

                        //System.Data.OleDb.OleDbConnection c = new OleDbConnection("Provider=SQLOLEDB;Data source=192.168.0.30;initial catalog=tlpsp_tzkq;user id=sa;password=sa");
                        //OleDbCommand cmd = c.CreateCommand();
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
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
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
                        Wjghboolflag = false;
                        frmPlanList f = new frmPlanList();
                        if (f.ShowDialog() == DialogResult.Yes)
                        {
                            linekey = f.Key;
                            tlVectorControl1.Operation = ToolOperation.Select;
                            //tlVectorControl1.Operation = ToolOperation.LeadLine;

                        }
                        break;
                    case "m_subxz": //变电站选址
                        Wjghboolflag = false;
                        Services.BaseService.GetList<PSP_SubstationSelect>();

                        frmSubstationManager mng = new frmSubstationManager();
                        mng.OnOpen += new OnOpenSubhandler(mng_OnOpen);
                        DialogResult dia = mng.ShowDialog();
                        if (dia == DialogResult.OK)
                        {
                            XZ_bdz = mng.code;
                            MessageBox.Show("请选择变电站拖放置到希望的位置或者进行变电站自动选址。");
                            PSP_SubstationSelect sel = new PSP_SubstationSelect();
                            sel.col2 = XZ_bdz;
                            IList<PSP_SubstationSelect> _plist = Services.BaseService.GetList<PSP_SubstationSelect>("SelectPSP_SubstationSelectList", sel);
                            for (int n = 0; n < _plist.Count; n++)
                            {
                                XmlNodeList _nlist = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@id='" + _plist[n].EleID + "']");
                                if (_nlist.Count < 1)
                                {
                                    Services.BaseService.Delete<PSP_SubstationSelect>(_plist[n]);
                                }
                            }

                        }
                        if (dia == DialogResult.Ignore)
                        {
                            string keyid = mng.KeyID;
                            string suid = mng.SUID;
                            PSP_SubstationUserNum n1 = new PSP_SubstationUserNum();
                            n1.col2 = keyid;
                            IList<PSP_SubstationUserNum> list1 = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationNum2", n1);
                            for (int i = 0; i < list1.Count; i++)
                            {
                                if (suid == list1[i].SubStationID)
                                {
                                    PSP_SubstationSelect s = new PSP_SubstationSelect();
                                    s.UID = list1[i].SubStationID;
                                    s.EleID = list1[i].userID;
                                    XmlNodeList nnn1 = tlVectorControl1.SVGDocument.SelectNodes("//* [@id='" + s.EleID + "']");
                                    foreach (XmlNode node1 in nnn1)
                                    {
                                        tlVectorControl1.SVGDocument.RootElement.RemoveChild(node1);
                                    }
                                    Services.BaseService.Update("DeletePSP_SubstationSelect", s);

                                }
                            }
                            tlVectorControl1.Refresh();

                        }
                        break;
                    case "mSubPrint":

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
                            StringBuilder bpts = new StringBuilder();
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
                                Double W1 = Convert.ToDouble(JWD1[1]);
                                Double D1 = Convert.ToDouble(JWD1[2]);
                                string[] JWD2 = str[1].Split(' ');
                                Double J2 = Convert.ToDouble(JWD2[0]);
                                Double W2 = Convert.ToDouble(JWD2[1]);
                                Double D2 = Convert.ToDouble(JWD2[2]);

                                Double JD = J1 + W1 / 60 + D1 / 3600;
                                Double WD = J2 + W2 / 60 + D2 / 3600;
                                IntXY xy = mapview.getXY(JD, WD);
                                if (mapview is MapViewGoogle)
                                    bpts.Append(xy.X + " " + xy.Y + ",");
                                else
                                    bpts.Append((-xy.X / (double)tlVectorControl1.ScaleRatio) + " " + (-xy.Y / (double)tlVectorControl1.ScaleRatio) + ",");
                                //}
                            }
                            if (bpts.Length > 0)
                                points = bpts.ToString(0, bpts.Length - 1);

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
                    case "m_dhx":
                        if (!getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl1.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("电网规划层"))
                        {
                            MessageBox.Show("请选择电网规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        // sgt1.Visible = true;
                        str_dhx = "1";
                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.LeadLine;
                        break;
                    case "mAreaPoly":
#if(!CITY)
                        if (!getlayer(SvgDocument.currentLayer, "城市规划层", tlVectorControl1.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("城市规划层"))
                        {
                            MessageBox.Show("请选择城市规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
#endif

                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.AreaPolygon;

                        break;
                    case "sjsz":
                        frmCS cs = new frmCS();
                        cs.ShowDialog();
                        break;
                    case "shjg":
                        GHWPG();
                        break;
                    case "mFx":
                        SubPrint = false;
                        bool ck = false;
                        //CheckedListBox.CheckedItemCollection ckcol = frmlar.checkedListBox1.CheckedItems;
                        //for (int i = 0; i < ckcol.Count; i++)
                        //{
                        //    Layer _lar = ckcol[i] as Layer;
                        //    if (_lar.GetAttribute("layerType") == "城市规划层")
                        //    {
                        //        ck = true;
                        //    }
                        //}
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
                    case "m_djcl":
                        tlVectorControl1.Operation = ToolOperation.Select;
                        str_djcl = "1";
                        MessageBox.Show("请选择线路进行档距测量。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "m_inxljwd":
                        frmInJWD f_in = new frmInJWD();
                        if (f_in.ShowDialog() == DialogResult.OK)
                        {

                            InputFile(f_in.GetFileName(), f_in.GetCheck());
                        }
                        break;
                    case "m_inbdzjwd":
                        frmInJWD f_in2 = new frmInJWD();
                        if (f_in2.ShowDialog() == DialogResult.OK)
                        {

                            InputBDZFile(f_in2.GetFileName(), f_in2.GetCheck());
                        }
                        break;
                    case "m_outsubjwd":

                        if (MessageBox.Show("确定要导出当前图层所有变电站坐标吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            string str_dy = "";
                            XmlNodeList nn0 = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@layer='" + SvgDocument.currentLayer + "']");
                            string lab = tlVectorControl1.SVGDocument.CurrentLayer.Label;


                            Excel.Application ex = new Excel.Application();
                            Excel.Workbook workBook = ex.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                            //workBook.Worksheets.Add(Type.Missing, workBook.ActiveSheet, 1, Type.Missing);
                            Excel.Worksheet xSheet1 = (Excel.Worksheet)ex.Worksheets[1];
                            int c = xSheet1.Columns.Count;
                            int r = xSheet1.Rows.Count;
                            ((Excel.Range)xSheet1.Cells[1, 1]).Value2 = "序号";
                            ((Excel.Range)xSheet1.Cells[1, 2]).Value2 = "变电站名称";
                            ((Excel.Range)xSheet1.Cells[1, 3]).Value2 = "电压等级";
                            ((Excel.Range)xSheet1.Cells[1, 4]).Value2 = "经度";
                            ((Excel.Range)xSheet1.Cells[1, 5]).Value2 = "纬度";
                            for (int n = 0; n < nn0.Count; n++)
                            {
                                XmlElement _xele = (XmlElement)nn0[n];

                                string jwd_info = _xele.GetAttribute("jwd-info");
                                string infoname = _xele.GetAttribute("info-name");
                                string dyinfo = _xele.GetAttribute("xlink:href");
                                dyinfo = getDY(dyinfo);
                                if (jwd_info != "")
                                {
                                    string[] jwd = jwd_info.Split(",".ToCharArray());

                                    ((Excel.Range)xSheet1.Cells[n + 2, 1]).Value2 = n + 1;
                                    ((Excel.Range)xSheet1.Cells[n + 2, 2]).Value2 = infoname;
                                    ((Excel.Range)xSheet1.Cells[n + 2, 3]).Value2 = dyinfo;
                                    ((Excel.Range)xSheet1.Cells[n + 2, 4]).Value2 = jwd[0].Trim();
                                    ((Excel.Range)xSheet1.Cells[n + 2, 5]).Value2 = jwd[1].Trim();

                                }
                                else
                                {

                                    // LongLat lat = mapview.ParseToLongLat(((Use)_xele).CenterPoint.X, ((Use)_xele).CenterPoint.Y);
                                    LongLat lat = mapview.OffSetZero(-(int)(Convert.ToInt32(((Use)_xele).CenterPoint.X) * tlVectorControl1.ScaleRatio), -(int)(Convert.ToInt32(((Use)_xele).CenterPoint.Y) * tlVectorControl1.ScaleRatio));
                                    ((Excel.Range)xSheet1.Cells[n + 2, 1]).Value2 = n + 1;
                                    ((Excel.Range)xSheet1.Cells[n + 2, 2]).Value2 = infoname;
                                    ((Excel.Range)xSheet1.Cells[n + 2, 3]).Value2 = dyinfo;
                                    ((Excel.Range)xSheet1.Cells[n + 2, 4]).Value2 = lat.Longitude;
                                    ((Excel.Range)xSheet1.Cells[n + 2, 5]).Value2 = lat.Latitude;

                                }

                            }

                            ex.Visible = true;
                        }
                        break;
                    case "m_outxljwd":
                        tlVectorControl1.Operation = ToolOperation.Select;

                        bool ckright = true;
                        if (MessageBox.Show("确定要导出当前图层所有线路坐标吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            string str_dy = "";
                            XmlNodeList nn0 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1'] [@layer='" + SvgDocument.currentLayer + "']");
                            string lab = tlVectorControl1.SVGDocument.CurrentLayer.Label;
                            for (int n = 0; n < nn0.Count; n++)
                            {
                                str_dy = "";
                                XmlElement x = nn0[n] as XmlElement;
                                str_dy = x.GetAttribute("dy-info");
                                string devid = x.GetAttribute("Deviceid");
                                if (str_dy == "")
                                {
                                    if (devid != "")
                                    {
                                        PSPDEV dev = Services.BaseService.GetOneByKey<PSPDEV>(devid);
                                        if (dev != null)
                                        {
                                            if (dev.RateVolt != 0)
                                            {
                                                str_dy = dev.RateVolt.ToString("###");
                                                ckright = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (nn0.Count==0)
                            {
                                MessageBox.Show("此层中不包含线路！请重新选择图层");
                                return;
                            }
                            if (str_dy == "")
                            {
                                if (MessageBox.Show("选择图层线路不包含电压等级信息，是否继续导出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    ckright = true;
                                }
                                else
                                {
                                    ckright = false;
                                }
                            }
                           
                            if (ckright)
                            {
                                Excel.Application ex = new Excel.Application();
                                Excel.Workbook workBook = ex.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                                for (int n = 0; n < nn0.Count; n++)
                                {
                                    XmlElement _xele = (XmlElement)nn0[n];
                                    workBook.Worksheets.Add(Type.Missing, workBook.ActiveSheet, 1, Type.Missing);
                                    Excel.Worksheet xSheet1 = (Excel.Worksheet)ex.Worksheets[n + 1];
                                    int c = xSheet1.Columns.Count;
                                    int r = xSheet1.Rows.Count;
                                    ((Excel.Range)xSheet1.Cells[1, 1]).Value2 = "杆塔号";
                                    ((Excel.Range)xSheet1.Cells[1, 2]).Value2 = "电压等级";
                                    ((Excel.Range)xSheet1.Cells[1, 3]).Value2 = "经度";
                                    ((Excel.Range)xSheet1.Cells[1, 4]).Value2 = "纬度";
                                    string jwd_info = _xele.GetAttribute("jwd-info");
                                    string gt_info = _xele.GetAttribute("gt-info");
                                    if (jwd_info != "")
                                    {
                                        string[] gt = gt_info.Split(",".ToCharArray());
                                        string[] jwd = jwd_info.Split(";".ToCharArray());
                                        for (int m = 0; m < jwd.Length; m++)
                                        {
                                            string[] jw_str = jwd[m].Split(",".ToCharArray());
                                            ((Excel.Range)xSheet1.Cells[m + 2, 1]).Value2 = gt[m];
                                            ((Excel.Range)xSheet1.Cells[m + 2, 2]).Value2 = str_dy;
                                            ((Excel.Range)xSheet1.Cells[m + 2, 3]).Value2 = jw_str[0].Trim();
                                            ((Excel.Range)xSheet1.Cells[m + 2, 4]).Value2 = jw_str[1].Trim();
                                        }
                                    }
                                    else
                                    {
                                        PointF[] pt = TLMath.getPolygonPoints(_xele);
                                        for (int k = 0; k < pt.Length; k++)
                                        {
                                            LongLat lat = mapview.OffSetZero(-(int)(Convert.ToInt32(pt[k].X) * tlVectorControl1.ScaleRatio), -(int)(Convert.ToInt32(pt[k].Y) * tlVectorControl1.ScaleRatio));
                                            //LongLat lat= mapview.ParseToLongLat(pt[k].X, pt[k].Y);
                                            ((Excel.Range)xSheet1.Cells[k + 2, 1]).Value2 = Convert.ToString(k + 1);
                                            ((Excel.Range)xSheet1.Cells[k + 2, 2]).Value2 = str_dy;
                                            ((Excel.Range)xSheet1.Cells[k + 2, 3]).Value2 = lat.Longitude;
                                            ((Excel.Range)xSheet1.Cells[k + 2, 4]).Value2 = lat.Latitude;
                                        }
                                    }
                                    string info_name = _xele.GetAttribute("info-name");
                                    if (info_name != "")
                                    {
                                        for (int k = 1; k < workBook.Worksheets.Count; k++)
                                        {
                                            if (((Excel.Worksheet)workBook.Worksheets[k]).Name == info_name)
                                            {
                                                info_name = info_name + k.ToString();
                                                break;
                                            }
                                        }
                                        xSheet1.Name = info_name;
                                    }

                                }
                                Excel.Worksheet xSheett = (Excel.Worksheet)ex.Worksheets[ex.Worksheets.Count];
                                xSheett.Activate();
                                xSheett.Delete();

                                ex.Visible = true;
                            }
                        }
                        break;
                    case "m_unsel":
                        string bdzwhere = " UID not in (";
                        string xlwhere = " where SUID not in (";
                        PSPDEV obj = new PSPDEV();
                        DeviceHelper.pspflag = false;
                        DeviceHelper.Wjghflag = false;
                        string[] deviceType = new string[] { "05", "20" };
                        XmlNodeList lslist = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1'] [@Deviceid!='']");
                        XmlNodeList useList = tlVectorControl1.SVGDocument.SelectNodes("svg/use [@Deviceid!='']");
                        //XmlNodeList useList = tlVectorControl1.SVGDocument.SelectNodes("svg/use [contains(use,'Sub')]");
                        for (int x1 = 0; x1 < lslist.Count; x1++)
                        {
                            XmlElement _node = lslist[x1] as XmlElement;
                            xlwhere = xlwhere + "'" + _node.GetAttribute("Deviceid") + "',";
                        }
                        for (int x2 = 0; x2 < useList.Count; x2++)
                        {
                            XmlElement _node = useList[x2] as XmlElement;
                            bdzwhere = bdzwhere + "'" + _node.GetAttribute("Deviceid") + "',";
                        }
                        if (bdzwhere.Length > 15)
                        {
                            bdzwhere = bdzwhere.Substring(0, bdzwhere.Length - 1);
                        }
                        if (xlwhere.Length > 15)
                        {
                            xlwhere = xlwhere.Substring(0, xlwhere.Length - 1);
                        }
                        bdzwhere = bdzwhere + ") and ";
                        xlwhere = xlwhere + ") and ";

                        DeviceHelper.bdzwhere = bdzwhere;
                        DeviceHelper.xlwhere = xlwhere;
                        DeviceHelper.SelectDeviceDLG(Itop.Client.MIS.ProgUID, deviceType);
                        DeviceHelper.bdzwhere = "";
                        DeviceHelper.xlwhere = "";
                        break;
                    case "ORP":
                        OpenProject orp = new OpenProject();
                        orp.ProjectID = Itop.Client.MIS.ProgUID;
                        orp.Initdata(false);
                        if (orp.ShowDialog() == DialogResult.OK)
                        {
                            if (orp.FileSUID != null)
                            {
                                ElectricLoadCal elcORP = new ElectricLoadCal();
                                elcORP.ORP(orp.FileSUID, 100);
                            }

                        }

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
                        //if (MessageBox.Show("是否生成负荷标注?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        //{
                        //    Fhbz();
                        //}

                        //FrmSet f_set = new FrmSet();
                        //if (f_set.ShowDialog()==DialogResult.OK)
                        //{
                        if (XZ_bdz == "")
                        {
                            MessageBox.Show("请选择一个变电站选址方案。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl1.Operation = ToolOperation.InterEnclosure;
                        bdz_xz = "yes";
                        //    str_dy = f_set.Str_dj;
                        //    str_num = f_set.Str_num;
                        //    str_jj = f_set.Str_jj;
                        //}
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
                            DataSet ds = ImportExcel(openFileDialog1.FileName);
                            DataTable dt1 = ds.Tables[0];
                            foreach (DataRow r1 in dt1.Rows)
                            {
                                if (r1[0].ToString() != "")
                                {
                                    SvgElement ele = null;
                                    decimal JD = 0;
                                    decimal WD = 0;
                                    JD = Convert.ToDecimal(r1[12]);
                                    WD = Convert.ToDecimal(r1[13]);
                                    PointF fnt = mapview.ParseToPoint(JD, WD);
                                    XmlElement t1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                    t1.SetAttribute("x", Convert.ToString(fnt.X / (float)tlVectorControl1.ScaleRatio + 8));
                                    t1.SetAttribute("y", Convert.ToString(fnt.Y / (float)tlVectorControl1.ScaleRatio));

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
                                    decimal JD = 0;
                                    decimal WD = 0;
                                    JD = Convert.ToDecimal(r1[9]);
                                    WD = Convert.ToDecimal(r1[10]);
                                    PointF fnt = mapview.ParseToPoint(JD, WD);
                                    XmlElement t1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                    t1.SetAttribute("x", Convert.ToString(fnt.X / (float)tlVectorControl1.ScaleRatio + 8));
                                    t1.SetAttribute("y", Convert.ToString(fnt.Y / (float)tlVectorControl1.ScaleRatio));

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
                            DataSet ds = ImportExcel(openFileDialog1.FileName);
                            DataTable dt1 = ds.Tables[0];
                            foreach (DataRow r1 in dt1.Rows)
                            {
                                if (r1[0].ToString() != "")
                                {
                                    SvgElement ele = null;
                                    decimal JD = 0;
                                    decimal WD = 0;
                                    JD = Convert.ToDecimal(r1[12]);
                                    WD = Convert.ToDecimal(r1[13]);
                                    //IntXY xy = mapview.getXY(JD, WD);
                                    PointF fnt = mapview.ParseToPoint(JD, WD);
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
                                    decimal JD = 0;
                                    decimal WD = 0;
                                    JD = Convert.ToDecimal(r1[9]);
                                    WD = Convert.ToDecimal(r1[10]);
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
                                    decimal JD = 0;
                                    decimal WD = 0;
                                    JD = Convert.ToDecimal(r1[9]);
                                    WD = Convert.ToDecimal(r1[10]);
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
                    case "m_createbdzinfo":
                        createBdzInfo(null);
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
                    #region  网架优化
                    case "OpenWJ":
                        ff = new frmGProg();
                        if (ff.ShowDialog() == DialogResult.OK)
                        {
                            Wjghboolflag = true;
                            checkwjghelement();
                            dotNetBarManager1.Bars["mainmenu"].GetItem("YHoperator").Enabled = true;
                            dotNetBarManager1.Bars["mainmenu"].GetItem("YHresult").Enabled = true;
                            dotNetBarManager1.Bars["mainmenu"].GetItem("ZTBut").Enabled = true;
                            dotNetBarManager1.Bars["mainmenu"].GetItem("JQBut").Enabled = false;
                            dotNetBarManager1.Bars["mainmenu"].GetItem("ZQBut").Enabled = false;
                            dotNetBarManager1.Bars["mainmenu"].GetItem("YQBut").Enabled = false;
                        }
                        else
                        {
                            Wjghboolflag = false;

                        }
                        break;
                    case "YHoperator":

                        break;
                    case "ghwj":
                        GHWPG();
                        break;
                    case "ZTBut":
                        dotNetBarManager1.Bars["mainmenu"].GetItem("ZTBut").Enabled = false;
                        dotNetBarManager1.Bars["mainmenu"].GetItem("JQBut").Enabled = true;
                        ElectricWjgh wjgh = new ElectricWjgh();
                        wjgh.initdat(ff.Key);             //恢复数据的原来面貌


                        WaitDialogForm wait = null;
                        try
                        {
                            wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                            wjgh.jianxiancheck(ff.Key, 1, 100);
                            wait.Close();
                            MessageBox.Show("整体数据优化成功。");
                            string con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + ff.Key + "'and type='线路')";
                            IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                            foreach (PSPDEV dev in list1)
                            {
                                PSP_GprogElevice pg = new PSP_GprogElevice();
                                pg.GprogUID = ff.Key;
                                pg.DeviceSUID = dev.SUID;
                                //先找到再修改
                                pg = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByKey", pg);
                                if (pg != null)
                                {
                                    if (dev.LineStatus == "运行")
                                    {



                                        pg.ZTstatus = "运行";
                                        pg.JQstatus = "运行";
                                        pg.ZQstatus = "运行";
                                        pg.YQstatus = "运行";
                                        Services.BaseService.Update<PSP_GprogElevice>(pg);


                                    }
                                    else if (dev.LineStatus == "等待")
                                    {

                                        pg.ZTstatus = "等待";
                                        pg.JQstatus = "等待";
                                        pg.ZQstatus = "等待";
                                        pg.YQstatus = "等待";

                                        Services.BaseService.Update<PSP_GprogElevice>(pg);
                                    }
                                    else if (dev.LineStatus == "待选")
                                    {

                                        pg.ZTstatus = "待选";
                                        Services.BaseService.Update<PSP_GprogElevice>(pg);
                                    }
                                }
                            }

                            wjghmapview(1);
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("数据存在问题，请检查后再继续！");
                            wait.Close();
                            return;
                        }
                        break;
                    case "JQBut":
                        dotNetBarManager1.Bars["mainmenu"].GetItem("JQBut").Enabled = false;
                        dotNetBarManager1.Bars["mainmenu"].GetItem("ZQBut").Enabled = true;
                        wjgh = new ElectricWjgh();
                        wjgh.JDlinecheck(ff.Key, 2);
                        JorJform selectmethod = new JorJform();
                        selectmethod.Text = "近期网架优化方法";
                        if (selectmethod.ShowDialog() == DialogResult.OK)
                        {
                            wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                            if (selectmethod.Mathindex == 0)
                            {

                                wjgh.jianxiancheck(ff.Key, 2, 100);

                                MessageBox.Show("近期数据减线优化成功。");
                            }
                            else if (selectmethod.Mathindex == 1)
                            {
                                wjgh.addlinecheck(ff.Key, 2, 100);
                                wjgh.addrightcheck(ff.Key, 2, 100);
                                for (int i = 0; i < wjgh.ercilinedengdai.Count; i++)
                                {
                                    wjgh.ercilinedengdai[i].LineStatus = "运行";
                                    Services.BaseService.Update<PSPDEV>(wjgh.ercilinedengdai[i]);
                                }
                                for (int i = 0; i < wjgh.lineyiyou.Count; i++)
                                {
                                    wjgh.lineyiyou[i].LineStatus = "待选";
                                    Services.BaseService.Update<PSPDEV>(wjgh.lineyiyou[i]);
                                }

                                MessageBox.Show("近期数据加线成功。");
                            }
                            wait.Close();
                            //此处写项目jq变化状况
                            string conjq = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + ff.Key + "'and type='线路'and ZTstatus in('待选'))";
                            IList list1jq = Services.BaseService.GetList("SelectPSPDEVByCondition", conjq);

                            foreach (PSPDEV dev in list1jq)
                            {
                                PSP_GprogElevice pg = new PSP_GprogElevice();
                                pg.GprogUID = ff.Key;
                                pg.DeviceSUID = dev.SUID;
                                //先找到再修改
                                pg = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByKey", pg);
                                if (pg != null)
                                {
                                    if (dev.LineStatus == "等待")
                                    {

                                        pg.JQstatus = "待选";
                                        Services.BaseService.Update<PSP_GprogElevice>(pg);
                                        //dev.LineStatus = "待选";
                                        //Services.BaseService.Update<PSPDEV>(dev);
                                    }
                                    else if (dev.LineStatus == "待选")
                                    {
                                        bool flag = false;
                                        foreach (eleclass el in wjgh.JDlinecol)
                                        {
                                            if (dev.SUID == el.suid)
                                            {
                                                flag = true;
                                                dev.LineStatus = "运行";
                                                Services.BaseService.Update<PSPDEV>(dev);
                                            }

                                        }
                                        if (flag)
                                        {
                                            pg.JQstatus = "投放";
                                            pg.ZQstatus = "运行";
                                            pg.YQstatus = "运行";
                                        }
                                        else
                                        {
                                            pg.JQstatus = "待选";
                                        }
                                        Services.BaseService.Update<PSP_GprogElevice>(pg);
                                    }
                                }

                            }
                            wjghmapview(2);
                        }
                        break;
                    case "ZQBut":
                        dotNetBarManager1.Bars["mainmenu"].GetItem("ZQBut").Enabled = false;
                        dotNetBarManager1.Bars["mainmenu"].GetItem("YQBut").Enabled = true;
                        wjgh = new ElectricWjgh();
                        wjgh.JDlinecheck(ff.Key, 3);
                        selectmethod = new JorJform();
                        selectmethod.Text = "中期网架优化方法";
                        if (selectmethod.ShowDialog() == DialogResult.OK)
                        {
                            wjghmapview(5);
                            wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                            if (selectmethod.Mathindex == 0)
                            {
                                wjgh.jianxiancheck(ff.Key, 3, 100);

                                MessageBox.Show("中期数据减线优化成功。");

                            }
                            else if (selectmethod.Mathindex == 1)
                            {
                                wjgh.addlinecheck(ff.Key, 3, 100);
                                wjgh.addrightcheck(ff.Key, 3, 100);
                                for (int i = 0; i < wjgh.ercilinedengdai.Count; i++)
                                {
                                    wjgh.ercilinedengdai[i].LineStatus = "运行";
                                    Services.BaseService.Update<PSPDEV>(wjgh.ercilinedengdai[i]);
                                }
                                for (int i = 0; i < wjgh.lineyiyou.Count; i++)
                                {
                                    wjgh.lineyiyou[i].LineStatus = "待选";
                                    Services.BaseService.Update<PSPDEV>(wjgh.lineyiyou[i]);
                                }

                                MessageBox.Show("中期数据加线成功。");
                            }
                            wait.Close();
                            //此处写项目jq变化状况
                            string conzq = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + ff.Key + "'and type='线路'and JQstatus in('待选'))";
                            IList list1zq = Services.BaseService.GetList("SelectPSPDEVByCondition", conzq);
                            foreach (PSPDEV dev in list1zq)
                            {
                                PSP_GprogElevice pg = new PSP_GprogElevice();
                                pg.GprogUID = ff.Key;
                                pg.DeviceSUID = dev.SUID;
                                //先找到再修改
                                pg = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByKey", pg);
                                if (pg != null)
                                {
                                    if (dev.LineStatus == "等待")
                                    {

                                        pg.ZQstatus = "待选";
                                        Services.BaseService.Update<PSP_GprogElevice>(pg);
                                        //dev.LineStatus = "待选";
                                        //Services.BaseService.Update<PSPDEV>(dev);
                                    }
                                    else if (dev.LineStatus == "待选")
                                    {
                                        bool flag = false;
                                        foreach (eleclass el in wjgh.JDlinecol)
                                        {
                                            if (el.suid == dev.SUID)
                                            {
                                                flag = true;
                                                dev.LineStatus = "运行";
                                                Services.BaseService.Update<PSPDEV>(dev);
                                            }

                                        }
                                        if (flag)
                                        {
                                            pg.ZQstatus = "投放";
                                            pg.YQstatus = "运行";
                                        }
                                        else
                                            pg.ZQstatus = "待选";
                                        Services.BaseService.Update<PSP_GprogElevice>(pg);
                                    }
                                }

                            }
                            wjghmapview(3);
                        }

                        break;
                    case "YQBut":
                        dotNetBarManager1.Bars["mainmenu"].GetItem("YQBut").Enabled = false;
                        dotNetBarManager1.Bars["mainmenu"].GetItem("ZTBut").Enabled = true;
                        wjgh = new ElectricWjgh();
                        wjgh.JDlinecheck(ff.Key, 4);
                        wjghmapview(6);
                        selectmethod = new JorJform();
                        selectmethod.Text = "远期网架优化方法";
                        if (selectmethod.ShowDialog() == DialogResult.OK)
                        {
                            wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                            if (selectmethod.Mathindex == 0)
                            {

                                wjgh.jianxiancheck(ff.Key, 4, 100);

                                MessageBox.Show("远期数据减线优化成功。");
                            }
                            else if (selectmethod.Mathindex == 1)
                            {
                                wjgh.addlinecheck(ff.Key, 4, 100);
                                wjgh.addrightcheck(ff.Key, 4, 100);
                                for (int i = 0; i < wjgh.ercilinedengdai.Count; i++)
                                {
                                    wjgh.ercilinedengdai[i].LineStatus = "运行";
                                    Services.BaseService.Update<PSPDEV>(wjgh.ercilinedengdai[i]);
                                }
                                for (int i = 0; i < wjgh.lineyiyou.Count; i++)
                                {
                                    wjgh.lineyiyou[i].LineStatus = "待选";
                                    Services.BaseService.Update<PSPDEV>(wjgh.lineyiyou[i]);
                                }

                                MessageBox.Show("远期数据加线成功。");
                            }
                            wait.Close();
                            //此处写项目jq变化状况
                            string conyq = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + ff.Key + "'and type='线路'and ZQstatus in('待选'))";
                            IList list1yq = Services.BaseService.GetList("SelectPSPDEVByCondition", conyq);
                            // wjgh = new ElectricWjgh();
                            foreach (PSPDEV dev in list1yq)
                            {
                                PSP_GprogElevice pg = new PSP_GprogElevice();
                                pg.GprogUID = ff.Key;
                                pg.DeviceSUID = dev.SUID;
                                //先找到再修改
                                pg = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByKey", pg);
                                if (pg != null)
                                {
                                    if (dev.LineStatus == "等待")
                                    {
                                        bool flag = false;
                                        foreach (eleclass el in wjgh.JDlinecol)
                                        {
                                            if (el.suid == dev.SUID)
                                            {
                                                flag = true;
                                                dev.LineStatus = "运行";
                                                Services.BaseService.Update<PSPDEV>(dev);
                                            }
                                        }
                                        if (flag)
                                            pg.YQstatus = "投放";
                                        else
                                            pg.YQstatus = "待选";
                                        Services.BaseService.Update<PSP_GprogElevice>(pg);
                                        //pg.YQstatus = "待选";
                                        //Services.BaseService.Update<PSP_GprogElevice>(pg);
                                    }
                                    else if (dev.LineStatus == "待选")
                                    {
                                        bool flag = false;
                                        foreach (eleclass el in wjgh.JDlinecol)
                                        {
                                            if (el.suid == dev.SUID)
                                            {
                                                flag = true;
                                                dev.LineStatus = "运行";
                                                Services.BaseService.Update<PSPDEV>(dev);
                                            }
                                        }
                                        if (flag)
                                            pg.YQstatus = "投放";
                                        else
                                            pg.YQstatus = "待选";
                                        Services.BaseService.Update<PSP_GprogElevice>(pg);
                                    }
                                }

                            }
                            wjghmapview(4);
                        }
                        break;
                    case "YHresult":
                        wjgh = new ElectricWjgh();
                        frmGProList p1 = new frmGProList();
                        p1.Show();
                        p1.LoadData(wjgh.LoadData(ff.Key));
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
                        if (sel_start_point == "")
                        {
                            MessageBox.Show("请选择线路起点。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        if (frm_input.ShowDialog() == DialogResult.OK)
                        {

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
                            catch (Exception ex1)
                            {
                                MessageBox.Show("存在相同的节点顺序号，请修改。\n\r" + ex1.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            PointF[] _points = new PointF[XLlist.Count];
                            for (int i = 0; i < orderlist.Count; i++)
                            {
                                PointF[] f1 = new PointF[1] { new PointF(((Use)orderlist.GetByIndex(i)).X + 6, ((Use)orderlist.GetByIndex(i)).Y + 6) };
                                ((Use)orderlist.GetByIndex(i)).Transform.Matrix.TransformPoints(f1);
                                _points[i] = f1[0];
                            }
                            string str_points = "";
                            for (int i = 0; i < _points.Length; i++)
                            {
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
                            int int_d = dom.Next(99999);
                            string styleValue = "stroke:" + ColorTranslator.ToHtml(Color.FromArgb(int_d));
                            _templine.SetAttribute("style", styleValue);
                            XmlNode tt_node = tlVectorControl1.SVGDocument.RootElement.AppendChild(_templine);
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
                    case "Niula":
                        Wjghboolflag = false;
                        OpenProject op = new OpenProject();
                        op.ProjectID = Itop.Client.MIS.ProgUID;
                        op.Initdata(false);
                        if (op.ShowDialog() == DialogResult.OK)
                        {
                            if (op.FileSUID != null)
                            {

                                Psptypeform pt = new Psptypeform();
                                if (pt.ShowDialog() == DialogResult.OK)
                                {
                                    frnReport wFrom = new frnReport();
                                    wFrom.Owner = this;
                                    wFrom.Show();
                                    wFrom.Text = this.Text + "—牛拉法潮流计算";
                                    wFrom.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
                                    ElectricLoadCal elc = new ElectricLoadCal();
                                    elc.LFC(op.FileSUID, 1, 100, wFrom);
                                    ShowResult(0, op.FileSUID, op.FileName, pt.PspOuttype, wFrom);
                                }


                            }

                        }

                        break;
                    case "pq":
                        Wjghboolflag = false;
                        op = new OpenProject();
                        op.ProjectID = Itop.Client.MIS.ProgUID;
                        op.Initdata(false);
                        if (op.ShowDialog() == DialogResult.OK)
                        {
                            if (op.FileSUID != null)
                            {
                                Psptypeform pt = new Psptypeform();
                                if (pt.ShowDialog() == DialogResult.OK)
                                {
                                    frnReport wFrom = new frnReport();
                                    wFrom.Owner = this;
                                    wFrom.Show();
                                    wFrom.Text = this.Text + "—PQ法潮流计算";
                                    wFrom.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
                                    ElectricLoadCal elc = new ElectricLoadCal();
                                    ElectricLoadCal elcPQ = new ElectricLoadCal();
                                    elcPQ.LFC(op.FileSUID, 2, 100, wFrom);
                                    // elcPQ.LFCER(op.FileSUID, 2, 100);
                                    ShowResult(1, op.FileSUID, op.FileName, pt.PspOuttype, wFrom);
                                }
                            }

                        }


                        break;

                    case "GausSeidel":
                        Wjghboolflag = false;
                        op = new OpenProject();
                        op.ProjectID = Itop.Client.MIS.ProgUID;
                        op.Initdata(false);
                        if (op.ShowDialog() == DialogResult.OK)
                        {
                            if (op.FileSUID != null)
                            {
                                Psptypeform pt = new Psptypeform();
                                if (pt.ShowDialog() == DialogResult.OK)
                                {
                                    frnReport wFrom = new frnReport();
                                    wFrom.Owner = this;
                                    wFrom.Show();
                                    wFrom.Text = this.Text + "—高斯赛德尔迭代法潮流计算";
                                    wFrom.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
                                    ElectricLoadCal elc = new ElectricLoadCal();
                                    ElectricLoadCal elcGS = new ElectricLoadCal();
                                    elcGS.LFC(op.FileSUID, 3, 100, wFrom);
                                    ShowResult(2, op.FileSUID, op.FileName, pt.PspOuttype, wFrom);
                                }
                            }

                        }


                        break;
                    case "N_RZYz":
                        Wjghboolflag = false;
                        op = new OpenProject();
                        op.ProjectID = Itop.Client.MIS.ProgUID;
                        op.Initdata(false);
                        if (op.ShowDialog() == DialogResult.OK)
                        {
                            if (op.FileSUID != null)
                            {
                                Psptypeform pt = new Psptypeform();
                                if (pt.ShowDialog() == DialogResult.OK)
                                {
                                    frnReport wFromZYZ = new frnReport();
                                    wFromZYZ.Owner = this;
                                    wFromZYZ.Show();
                                    wFromZYZ.Text = this.Text + "—最有乘子法潮流计算";
                                    wFromZYZ.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
                                    ElectricLoadCal elcN_RZYz = new ElectricLoadCal();
                                    elcN_RZYz.LFC(op.FileSUID, 4, 100, wFromZYZ);
                                    ShowResult(3, op.FileSUID, op.FileName, pt.PspOuttype, wFromZYZ);
                                }
                            }

                        }

                        break;
                    case "NLnFH":
                        Wjghboolflag = false;
                        op = new OpenProject();
                        op.ProjectID = Itop.Client.MIS.ProgUID;
                        op.Initdata(false);
                        if (op.ShowDialog() == DialogResult.OK)
                        {
                            if (op.FileSUID != null)
                            {
                                Psptypeform pt = new Psptypeform();
                                if (pt.ShowDialog() == DialogResult.OK)
                                {
                                    frnReport wFromZYZ = new frnReport();
                                    wFromZYZ.Owner = this;
                                    wFromZYZ.Show();
                                    wFromZYZ.Text = this.Text + "—最有乘子法潮流计算";
                                    wFromZYZ.ShowText += "正在收集信息\t" + System.DateTime.Now.ToString();
                                    ElectricLoadCal elcN_RZYz = new ElectricLoadCal();
                                    elcN_RZYz.LFCS(op.FileSUID, 4, 100);
                                    ShowResult(3, op.FileSUID, op.FileName, pt.PspOuttype, wFromZYZ);
                                }
                            }

                        }

                        break;
                    #endregion
                }
            }
        }

        void mng_OnOpen(object sender, string sid)
        {
            XmlNodeList nn = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@id='" + sid + "']");
            if (nn.Count > 0)
            {
                tlVectorControl1.GoLocation((IGraph)(nn[0]));
                tlVectorControl1.Refresh();
                tlVectorControl1.SVGDocument.SelectCollection.Clear();
                tlVectorControl1.SVGDocument.SelectCollection.Add((IGraph)(nn[0]));
            }
        }
        frmGProg ff = null;
        //规划以后图显示 当整体的时候 去掉图上等待的线路 其他时期时 投放为蓝色 等待为绿色

        private void wjghmapview(int type)
        {
            XmlNodeList linelist = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1']");
            if (type == 1)
            {
                string con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + ff.Key + "'and type='线路')";
                IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                foreach (PSPDEV dev in list1)
                {
                    foreach (XmlNode element in linelist)
                    {
                        if (!string.IsNullOrEmpty((element as XmlElement).GetAttribute("Deviceid")))
                        {
                            if ((element as XmlElement).GetAttribute("Deviceid") == dev.SUID && dev.LineStatus == "等待")
                            {
                                (element as XmlElement).SetAttribute("visibility", "hidden");
                            }
                        }
                    }
                }
            }
            //近期操作后出现的结果
            if (type == 2)
            {
                string con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + ff.Key + "'and type='线路'and ZTstatus in('待选'))";
                IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                foreach (XmlNode element in linelist)
                {
                    if (!string.IsNullOrEmpty((element as XmlElement).GetAttribute("Deviceid")))
                    {

                        foreach (PSPDEV dev in list1)
                        {

                            if ((element as XmlElement).GetAttribute("Deviceid") == dev.SUID && dev.LineStatus == "等待")
                            {
                                (element as XmlElement).SetAttribute("stroke", "#00FF00");
                            }
                            else if ((element as XmlElement).GetAttribute("Deviceid") == dev.SUID && dev.LineStatus == "待选")
                                (element as XmlElement).SetAttribute("stroke", "#000FFF");

                        }
                    }

                }
            }
            //中期操作前出现的结果
            if (type == 5)
            {
                string con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + ff.Key + "'and type='线路'and ZTstatus in('待选'))";
                IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                foreach (XmlNode element in linelist)
                {
                    if (!string.IsNullOrEmpty((element as XmlElement).GetAttribute("Deviceid")))
                    {
                        foreach (PSPDEV dev in list1)
                        {

                            if ((element as XmlElement).GetAttribute("Deviceid") == dev.SUID && dev.LineStatus == "等待")
                            {
                                (element as XmlElement).SetAttribute("stroke", "#00FF00");
                            }
                            else if ((element as XmlElement).GetAttribute("Deviceid") == dev.SUID && dev.LineStatus == "待选")
                                (element as XmlElement).SetAttribute("stroke", "#000FFF");

                        }
                    }

                }
            }
            //中期操作后

            if (type == 3)
            {
                string con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + ff.Key + "'and type='线路'and JQstatus in('待选'))";
                IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                foreach (XmlNode element in linelist)
                {
                    if (!string.IsNullOrEmpty((element as XmlElement).GetAttribute("Deviceid")))
                    {
                        foreach (PSPDEV dev in list1)
                        {

                            if ((element as XmlElement).GetAttribute("Deviceid") == dev.SUID && dev.LineStatus == "等待")
                            {
                                (element as XmlElement).SetAttribute("stroke", "#00FF00");
                            }
                            else if ((element as XmlElement).GetAttribute("Deviceid") == dev.SUID && dev.LineStatus == "待选")
                                (element as XmlElement).SetAttribute("stroke", "#000FFF");

                        }
                    }

                }
            }
            //远期操作后的变化情况
            if (type == 6)
            {
                string con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + ff.Key + "'and type='线路'and JQstatus in('待选'))";
                IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                foreach (XmlNode element in linelist)
                {
                    if (!string.IsNullOrEmpty((element as XmlElement).GetAttribute("Deviceid")))
                    {
                        foreach (PSPDEV dev in list1)
                        {

                            if ((element as XmlElement).GetAttribute("Deviceid") == dev.SUID && dev.LineStatus == "等待")
                            {
                                (element as XmlElement).SetAttribute("stroke", "#00FF00");
                            }
                            else if ((element as XmlElement).GetAttribute("Deviceid") == dev.SUID && dev.LineStatus == "待选")
                                (element as XmlElement).SetAttribute("stroke", "#000FFF");

                        }
                    }

                }
            }
            //远期操作后的变化
            if (type == 4)
            {
                string con = "WHERE Type='05' AND SUID in (select DeviceSUID from PSP_GprogElevice where GprogUID='" + ff.Key + "'and type='线路'and ZQstatus in('待选'))";
                IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                foreach (XmlNode element in linelist)
                {
                    if (!string.IsNullOrEmpty((element as XmlElement).GetAttribute("Deviceid")))
                    {
                        foreach (PSPDEV dev in list1)
                        {

                            if ((element as XmlElement).GetAttribute("Deviceid") == dev.SUID && dev.LineStatus == "等待")
                            {
                                (element as XmlElement).SetAttribute("stroke", "#00FF00");
                            }
                            else if ((element as XmlElement).GetAttribute("Deviceid") == dev.SUID && dev.LineStatus == "待选")
                                (element as XmlElement).SetAttribute("stroke", "#000FFF");

                        }
                    }

                }
            }
        }
        private List<eleclass> wjghelement = new List<eleclass>();
        private void checkwjghelement()
        {
            //判断图元有没有 

            wjghelement.Clear();
            XmlNodeList useList = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
            XmlNodeList linelist = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1']");
            string con = "GprogUID='" + ff.Key + "'";
            IList list = Services.BaseService.GetList("SelectPSP_GprogEleviceByCondition", con);
            try
            {
                foreach (PSP_GprogElevice pg in list)
                {
                    bool flag = false;
                    if (pg.Type == "变电站" || pg.Type == "电源")
                    {
                        foreach (XmlNode element in useList)
                        {
                            if (!string.IsNullOrEmpty((element as XmlElement).GetAttribute("Deviceid")))
                            {
                                if ((element as XmlElement).GetAttribute("Deviceid") == pg.DeviceSUID)
                                {
                                    flag = true;
                                    if (pg.L2 == "0")
                                    {
                                        pg.L2 = "1";
                                        Services.BaseService.Update<PSP_GprogElevice>(pg);
                                    }
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        foreach (XmlNode element in linelist)
                        {
                            if (!string.IsNullOrEmpty((element as XmlElement).GetAttribute("Deviceid")))
                            {
                                if ((element as XmlElement).GetAttribute("Deviceid") == pg.DeviceSUID)
                                {
                                    //显示隐藏的图元

                                    (element as XmlElement).SetAttribute("visibility", "show");
                                    flag = true;
                                    if (pg.L2 == "0")
                                    {
                                        pg.L2 = "1";
                                        Services.BaseService.Update<PSP_GprogElevice>(pg);
                                    }

                                    break;
                                }
                            }

                        }
                    }
                    if (!flag)
                    {
                        if (pg.L2 == "1")
                        {
                            pg.L2 = "0";
                            Services.BaseService.Update<PSP_GprogElevice>(pg);
                        }

                    }
                    if (pg.Type == "变电站")
                    {
                        PSP_Substation_Info sb = new PSP_Substation_Info();
                        sb.UID = pg.DeviceSUID;
                        sb = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoByKey", sb);
                        eleclass el = new eleclass(sb.Title, sb.UID, "20", flag);
                        wjghelement.Add(el);
                    }
                    if (pg.Type == "电源")
                    {
                        PSP_PowerSubstation_Info sb = new PSP_PowerSubstation_Info();
                        sb.UID = pg.DeviceSUID;
                        sb = (PSP_PowerSubstation_Info)Services.BaseService.GetObject("SelectPSP_PowerSubstation_InfoByKey", sb);
                        eleclass el = new eleclass(sb.Title, sb.UID, "30", flag);
                        wjghelement.Add(el);
                    }
                    if (pg.Type == "线路")
                    {
                        PSPDEV sb = new PSPDEV();
                        sb.SUID = pg.DeviceSUID;
                        sb = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", sb);
                        eleclass el = new eleclass(sb.Name, sb.SUID, "05", flag);
                        wjghelement.Add(el);
                    }
                }

            }
            catch (System.Exception ex)
            {

            }

        }
        //找出折线最长的一段的两端节点
        private List<PointF> CheckLenth(Polyline multiline)
        {
            PointF[] pointcol = multiline.Points;
            int count = pointcol.Length;
            double maxlenth = 0;
            double lenth = 0;
            List<PointF> listpoint = new List<PointF>();
            for (int i = 0; i < count - 1; i++)
            {
                lenth = Math.Sqrt((pointcol[i].X - pointcol[i + 1].X) * (pointcol[i].X - pointcol[i + 1].X) + (pointcol[i].Y - pointcol[i + 1].Y) * (pointcol[i].Y - pointcol[i + 1].Y));
                if (lenth > maxlenth)
                {
                    maxlenth = lenth;
                    listpoint.Clear();
                    listpoint.Add(pointcol[i]);
                    listpoint.Add(pointcol[i + 1]);
                }
            }
            return listpoint;
        }
        private void ShowResult(int order, string projectsuid, string FileName, int pspouttype, frnReport wFrom)
        {
            try
            {
                //删除原来的text文本
                //XmlNodeList list = tlVectorControl1.SVGDocument.SelectNodes("svg/*[@flag='" + "1" + "']");

                //foreach (XmlNode node in list)
                //{
                //    SvgElement element = node as SvgElement;
                //    tlVectorControl1.SVGDocument.CurrentElement = element;
                //    tlVectorControl1.Delete();
                //}
                wFrom.ShowText += "\r\n开始显示计算结果\t" + System.DateTime.Now.ToString();
                double yinzi = 0, capability = 0, volt = 0, standvolt = 0, current = 0;
                PSPDEV benchmark = new PSPDEV();
                benchmark.Type = "power";
                benchmark.SvgUID = projectsuid;
                IList list3 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", benchmark);
                //if (list3 == null)
                //{
                //    MessageBox.Show("请设置基准后再进行计算!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                foreach (PSPDEV dev in list3)
                {
                    yinzi = Convert.ToDouble(dev.PowerFactor);
                    capability = Convert.ToDouble(dev.StandardCurrent);
                    volt = Convert.ToDouble(dev.StandardVolt);
                    TLPSPVmin = dev.iV;
                    TLPSPVmax = dev.jV;
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
                Layer lar = null;
                if (Layer.CkLayerExist(FileName, tlVectorControl1.SVGDocument))
                {
                    ArrayList layercol = tlVectorControl1.SVGDocument.getLayerList();
                    for (int i = 0; i < layercol.Count; i++)
                    {
                        if (FileName == (layercol[i] as Layer).GetAttribute("label"))
                        {
                            lar = (Layer)layercol[i];
                            break;
                        }
                    }
                }
                else
                {
                    lar = Layer.CreateNew(FileName, tlVectorControl1.SVGDocument);

                    lar.SetAttribute("layerType", progtype);
                    lar.SetAttribute("ParentID", tlVectorControl1.SVGDocument.CurrentLayer.GetAttribute("ParentID"));
                    //this.frmlar.checkedListBox1.SelectedIndex = -1;
                    //this.frmlar.checkedListBox1.Items.Add(lar, true);
                }
                int size = tlVectorControl1.ScaleRatio > 1 ? 12 : (int)(12 / tlVectorControl1.ScaleRatio);
                XmlNodeList useList = tlVectorControl1.SVGDocument.SelectNodes("svg/use");
                foreach (XmlNode node in useList)
                {
                    XmlElement element = node as XmlElement;
                    string strCon = null;
                    IList listMX = null;
                    if (pspouttype == 0)
                    {
                        strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectsuid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01'";
                        listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                    }

                    //XmlNode text = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@ParentID='" + element.GetAttribute("id") + "']");
                    if (pspouttype == 1)
                    {
                        bool pspflag = false;
                        PSP_Substation_Info ps = new PSP_Substation_Info();
                        ps.UID = (element).GetAttribute("Deviceid");
                        ps = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoByKey", ps);
                        if (ps != null)
                        {
                            if (ps.Flag == "2")
                            {
                                strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectsuid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01'";
                                listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                            }

                            pspflag = true;
                        }
                        if (!pspflag)
                        {
                            PSP_PowerSubstation_Info ppi = new PSP_PowerSubstation_Info();
                            ppi.UID = (element).GetAttribute("Deviceid");
                            ppi = (PSP_PowerSubstation_Info)Services.BaseService.GetObject("SelectPSP_PowerSubstation_InfoByKey", ppi);
                            if (ppi != null)
                            {
                                if (ppi.Flag == "2")
                                {
                                    strCon = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectsuid + "' AND SvgUID = '" + (element).GetAttribute("Deviceid") + "' AND Type = '01'";
                                    listMX = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon);
                                }

                            }
                        }

                    }

                    if (listMX != null)
                    {
                        for (int i = 0; i < listMX.Count; i++)
                        {
                            PSPDEV elementDEV = (PSPDEV)(listMX[i]);
                            PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                            elcDEV.ProjectSUID = projectsuid;
                            elcDEV.DeviceSUID = ((PSPDEV)listMX[i]).SUID;
                            elcDEV = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDEV);
                            if (elcDEV != null)
                            {
                                XmlElement elementn1 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/text[@ layer='" + lar.ID + "'and @ParentID1='" + ((PSPDEV)listMX[i]).SUID + "']") as XmlElement;
                                XmlElement elementn2 = tlVectorControl1.SVGDocument.SelectSingleNode("svg/text[@ layer='" + lar.ID + "'and @ParentID2='" + ((PSPDEV)listMX[i]).SUID + "']") as XmlElement;
                                if (elementn1 == null)
                                {
                                    //RectangleF bound = ((IGraph)element).GetBounds();
                                    //XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                    //XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                                    //n1.SetAttribute("x", Convert.ToString(bound.X));
                                    //n1.SetAttribute("y", Convert.ToString(bound.Y - i * 20));
                                    //n1.SetAttribute("font-size", size.ToString());//"12");
                                    //n1.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL2).ToString("N2");
                                    ////Layer la = tlVectorControl1.SVGDocument.GetLayerByID(element.GetAttribute("layer"));
                                    //n1.SetAttribute("layer", lar.ID);
                                    ////MessageBox.Show(Convert.ToString(n1.InnerText));
                                    //n1.SetAttribute("flag", "1");
                                    //n1.SetAttribute("ParentID1", ((PSPDEV)listMX[i]).SUID);
                                    //if (Convert.ToDouble(GetColValue(elcDEV, order).COL2) > TLPSPVmax * elementDEV.RateVolt / elementDEV.ReferenceVolt || Convert.ToDouble(GetColValue(elcDEV, order).COL2) < TLPSPVmin * elementDEV.RateVolt / elementDEV.ReferenceVolt)//电压越限，需修改
                                    //    n1.SetAttribute("stroke", "#FF0000");
                                    //if (elementDEV.NodeType == "0")
                                    //{

                                    //    n2.SetAttribute("x", Convert.ToString(bound.X));
                                    //    n2.SetAttribute("y", Convert.ToString(bound.Y + bound.Height + 20));
                                    //    if (Convert.ToDouble(elcDEV.COL5) >= 0)
                                    //    {
                                    //        n2.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL4).ToString("N2") + "  + " + "j" + Convert.ToDouble(GetColValue(elcDEV, order).COL5).ToString("N2");
                                    //    }
                                    //    else
                                    //    {
                                    //        n2.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL4).ToString("N2") + "  - " + "j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                                    //    }
                                    //    n2.SetAttribute("layer", lar.ID);
                                    //    n2.SetAttribute("flag", "1");
                                    //    n2.SetAttribute("ParentID", ((PSPDEV)listMX[i]).SUID);
                                    //    n2.SetAttribute("font-size", size.ToString());//"12");
                                    //    // n2.SetAttribute("limitsize", "true");

                                    //    double tempi = Convert.ToDouble(GetColValue(elcDEV, order).COL4);
                                    //    double tempj = Convert.ToDouble(GetColValue(elcDEV, order).COL5);
                                    //    double temptotal = Math.Sqrt(tempi * tempi + tempj * tempj);
                                    //    if (temptotal > Convert.ToDouble(elementDEV.Burthen))
                                    //    {
                                    //        n2.SetAttribute("stroke", "#FF0000");
                                    //    }
                                    //    tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);

                                    //}
                                    //tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                    //tlVectorControl1.Operation = ToolOperation.Select;
                                }
                                else
                                {
                                    //elementn1.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL2).ToString("N2");
                                    //if (Convert.ToDouble(GetColValue(elcDEV, order).COL2) > TLPSPVmax * elementDEV.RateVolt / elementDEV.ReferenceVolt || Convert.ToDouble(GetColValue(elcDEV, order).COL2) < TLPSPVmin * elementDEV.RateVolt / elementDEV.ReferenceVolt)//电压越限，需修改
                                    //    elementn1.SetAttribute("stroke", "#FF0000");
                                    //if (elementDEV.NodeType == "0")
                                    //{


                                    //    if (Convert.ToDouble(elcDEV.COL5) >= 0)
                                    //    {
                                    //        elementn2.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL4).ToString("N2") + "  + " + "j" + Convert.ToDouble(GetColValue(elcDEV, order).COL5).ToString("N2");
                                    //    }
                                    //    else
                                    //    {
                                    //        elementn2.InnerText = Convert.ToDouble(GetColValue(elcDEV, order).COL4).ToString("N2") + "  - " + "j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                                    //    }
                                    //}
                                }

                            } tlVectorControl1.Refresh();
                        }
                    }

                }
                List<PSPDEV> listline = new List<PSPDEV>();
                if (pspouttype==1)
                {
                     SelShowlineform selbusfrm = new SelShowlineform();
                    selbusfrm.ProjectSUID = projectsuid;
                    selbusfrm.ProjectID = Itop.Client.MIS.ProgUID;
                    selbusfrm.ShowDialog();
                    if (selbusfrm.DialogResult == DialogResult.OK)
                    {
                      
                        foreach (DataRow row in selbusfrm.DT.Rows)
                        {
                            try
                            {
                                if ((bool)row["C"])
                                {
                                    PSPDEV psp = new PSPDEV();
                                    psp.SUID = row["A"].ToString();

                                    psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByKey", psp);
                                    if (psp != null)
                                    {
                                        listline.Add(psp);
                                    }
                                }

                            }
                            catch (System.Exception ex)
                            {

                            }
                        }
                        if (listline.Count == 0)
                        {
                            MessageBox.Show("没有选择显示的线路！");
                            return;
                        }
                    }
                }
                wFrom.ShowText += "\r\n正在显示线路信息\t" + System.DateTime.Now.ToString();
                XmlNodeList polyLineList = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline");
              
                foreach (XmlNode node in polyLineList)
                {
                    XmlElement element = node as XmlElement;
                    PSP_ElcDevice elcDEV = new PSP_ElcDevice();
                    elcDEV.ProjectSUID = projectsuid;
                    elcDEV.DeviceSUID = element.GetAttribute("Deviceid");
                    elcDEV = (PSP_ElcDevice)Services.BaseService.GetObject("SelectPSP_ElcDeviceByKey", elcDEV);
                    PSPDEV elementDEV = new PSPDEV();
                    if (elcDEV != null)
                    {
                        elementDEV.SUID = elcDEV.DeviceSUID;
                        elementDEV = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", elementDEV);
                        if (pspouttype == 1)                             //如果只显示规划线路的数据
                        {
                            bool flag = false;
                            foreach (PSPDEV showdev in listline)
                            {
                                if (elementDEV.SUID==showdev.SUID)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                //删除不是选中的线路的数据

                                XmlElement elementdl = tlVectorControl1.SVGDocument.SelectSingleNode("svg/text[@ layer='" + lar.ID + "'and @ParentID='" + elementDEV.SUID + "']") as XmlElement;
                                if (elementdl != null)
                                {
                                    tlVectorControl1.SVGDocument.RootElement.RemoveChild(elementdl);
                                }
                                tlVectorControl1.Refresh();
                  
                                continue;
                            }
                            //if (Convert.ToInt32(elementDEV.OperationYear) <= DateTime.Now.Year)
                            //{
                            //    continue;
                            //}
                        }
                    }
                    else
                    {
                        continue;
                    }

                    if (elementDEV != null && elementDEV.KSwitchStatus == "0")
                    {
                        XmlElement elementdl = tlVectorControl1.SVGDocument.SelectSingleNode("svg/text[@ layer='" + lar.ID + "'and @ParentID='" + elementDEV.SUID + "']") as XmlElement;

                        if (elementdl == null)
                        {
                            List<PointF> pcol = CheckLenth((Polyline)element);

                            PointF[] t = ((Polyline)element).Points;
                            PointF[] t2 = ((Polyline)element).Points; t = t2;
                            int lastnum = t2.Length - 1;
                            PointF midt = new PointF((float)((pcol[0].X + pcol[1].X) / 2), (float)((pcol[0].Y + pcol[1].Y) / 2));
                            float angel = 0f;
                            angel = (float)(180 * Math.Atan2((pcol[0].Y - pcol[1].Y), (pcol[1].X - pcol[0].X)) / Math.PI);

                            string l3 = Convert.ToString(midt.X);
                            string l4 = Convert.ToString(midt.Y);

                            string tran = ((Polyline)element).Transform.ToString();

                            PointF center = new PointF((float)(pcol[0].X + (pcol[1].X - pcol[0].X) / 2), (float)(pcol[0].Y + (pcol[1].Y - pcol[0].Y) / 2));
                            XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                            // XmlElement n2 = tlVectorControl1.SVGDocument.CreateElement("polyline") as Polyline;
                            //XmlElement n3 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;

                            PointF pStart = new PointF(center.X + (float)(15 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(15 * Math.Cos((angel) * Math.PI / 180)));
                            PointF pStart2 = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));

                            XmlNode firstNodeElement = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + element.GetAttribute("FirstNode") + "']");
                            XmlNode lastNodeElement = tlVectorControl1.SVGDocument.SelectSingleNode("svg/*[@id='" + element.GetAttribute("LastNode") + "']");
                            //if (firstNodeElement != null)
                            {
                                if ((angel > 10 && angel < 90) || (angel < 0 && Math.Abs(angel) < 90) || (angel > 180 && angel < 350))
                                {
                                    //if (t2[0].X > ((IGraph)firstNodeElement).CenterPoint.X)
                                    //{
                                    pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    //}
                                }
                                else if ((angel >= 0 && angel <= 10) || (angel >= 350 && angel <= 360) || (angel < 0 && Math.Abs(angel) <= 90))
                                {
                                    //if (t2[0].Y > ((IGraph)firstNodeElement).CenterPoint.Y)
                                    //{
                                    pStart = new PointF(center.X - (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    pStart2 = new PointF(center.X + (float)(23 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(23 * Math.Cos((angel) * Math.PI / 180)));
                                    //}
                                }
                                else if ((angel < 0 && Math.Abs(angel) > 90) || (angel >= 90 && angel <= 180))
                                {
                                    //if (t2[0].Y > ((IGraph)firstNodeElement).CenterPoint.Y)
                                    //{
                                    pStart = new PointF(center.X - (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y + (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                                    pStart2 = new PointF(center.X + (float)(7 * Math.Sin((angel) * Math.PI / 180)), center.Y - (float)(7 * Math.Cos((angel) * Math.PI / 180)));
                                    //}
                                }
                                if (Convert.ToDouble(GetColValue(elcDEV, order).COL5) >= 0)
                                {
                                    n1.InnerText = (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL4))).ToString("N2") + " + j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                                }
                                else
                                {
                                    n1.InnerText = (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL4))).ToString("N2") + " - j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                                }
                                Graphics dd = this.CreateGraphics();
                                Font ff = new Font("宋体", 12);
                                SizeF sf = dd.MeasureString(n1.InnerText, ff);
                                double ztlength = Math.Sqrt(sf.Width * sf.Width + sf.Height * sf.Height);
                                PointF newp1 = new PointF(t[0].X + (t[1].X - t[0].X) / 2 - (float)(15 * Math.Sin(angel)), t[0].Y + (t[1].Y - t[0].Y) / 2 - (float)(15 * Math.Cos(angel)));
                                //n1.SetAttribute("x", Convert.ToString(center.X - (float)(Math.Sin((angel) * Math.PI / 180) * ztlength)));
                                //n1.SetAttribute("y", Convert.ToString(center.Y - (float)(Math.Cos((angel) * Math.PI / 180) * ztlength)));

                                n1.SetAttribute("x", Convert.ToString(center.X));
                                n1.SetAttribute("y", Convert.ToString(center.Y));



                                //Layer la = tlVectorControl1.SVGDocument.GetLayerByID(element.GetAttribute("layer"));
                                n1.SetAttribute("layer", lar.ID);
                                n1.SetAttribute("ParentID", elementDEV.SUID);
                                n1.SetAttribute("flag", "1");
                                n1.SetAttribute("Showline", "1");            //为显示哪条线路做标志
                                n1.SetAttribute("font-size", "96");// size.ToString());//"12");
                                // n1.SetAttribute("limitsize", "true");
                                if (Convert.ToDouble(GetColValue(elcDEV, order).COL14) > (elementDEV.LineChange))//电流越限，需修改。




                                    n1.SetAttribute("stroke", "#FF0000");


                                //PointF p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 25) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 25) * Math.PI / 180)));
                                //PointF p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 335) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 335) * Math.PI / 180)));

                                //if (Convert.ToDouble(elcDEV.COL4) < 0)
                                //{
                                //    p1 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 155) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 155) * Math.PI / 180)));
                                //    p2 = new PointF(midt.X - (float)(10 * Math.Cos((angel + 205) * Math.PI / 180)), midt.Y - (float)(10 * Math.Sin((angel + 205) * Math.PI / 180)));
                                //}

                                //string l1 = Convert.ToString(p1.X);
                                //string l2 = Convert.ToString(p1.Y);
                                //string l5 = Convert.ToString(p2.X);
                                //string l6 = Convert.ToString(p2.Y);

                                //n2.SetAttribute("points", l1 + " " + l2 + "," + l3 + " " + l4 + "," + l5 + " " + l6);
                                //n2.SetAttribute("fill-opacity", "1");
                                //n2.SetAttribute("layer", SvgDocument.currentLayer);
                                //n2.SetAttribute("flag", "1");
                                //n2.SetAttribute("font-size", "6");
                                //tlVectorControl1.SVGDocument.RootElement.AppendChild(n2);
                                //tlVectorControl1.SVGDocument.CurrentElement = n2 as SvgElement;

                                tlVectorControl1.SVGDocument.RootElement.AppendChild(n1);
                                tlVectorControl1.Operation = ToolOperation.Select;

                                tlVectorControl1.SVGDocument.CurrentElement = n1 as SvgElement;

                                RectangleF ttt = ((Polyline)element).GetBounds();

                                tlVectorControl1.RotateSelection(angel, pStart);
                                if (Math.Abs(angel) > 90)
                                    tlVectorControl1.RotateSelection(180, pStart);


                            }
                        }
                        else
                        {
                            elementdl.SetAttribute("Showline", "1");            //为显示哪条线路做标志
                            if (Convert.ToDouble(GetColValue(elcDEV, order).COL5) >= 0)
                            {
                                elementdl.InnerText = (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL4))).ToString("N2") + " + j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                            }
                            else
                            {
                                elementdl.InnerText = (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL4))).ToString("N2") + " - j" + (Math.Abs(Convert.ToDouble(GetColValue(elcDEV, order).COL5))).ToString("N2");
                            }
                            if (Convert.ToDouble(GetColValue(elcDEV, order).COL14) > (elementDEV.LineChange))//电流越限，需修改。




                                elementdl.SetAttribute("stroke", "#FF0000");
                        }
                        tlVectorControl1.Refresh();
                    }
                    //删除不投入运行的输出结果
                    else if (elementDEV != null && elementDEV.KSwitchStatus == "1")
                    {
                        XmlElement elementdl = tlVectorControl1.SVGDocument.SelectSingleNode("svg/text[@ layer='" + lar.ID + "'and @ParentID='" + elementDEV.SUID + "']") as XmlElement;
                        if (elementdl != null)
                        {
                            tlVectorControl1.SVGDocument.RootElement.RemoveChild(elementdl);
                        }
                        tlVectorControl1.Refresh();
                    }

                }
                //this.frmlar.checkedListBox1.Items.Add(lar, true);
                //this.frmlar.checkedListBox1.SelectedIndex = -1;
                MessageBox.Show("显示完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("参数错误，请调整参数后重新计算！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private DeviceCOL GetColValue(PSP_ElcDevice elcDEV, int order)
        {
            DeviceCOL devCol = new DeviceCOL();
            if (order == 0)
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
            else if (order == 1)
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
            else if (order == 2)
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
            else if (order == 3)
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

                            //PSPDEV t1 = new PSPDEV();
                            //t1.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                            //t1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                            //Services.BaseService.Update("DeletePSPDEVbySvgUIDAndEleID", t1);

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

                                //if (temp != null)
                                //{
                                //    Services.BaseService.Update("DeletesubstationByEleID", _sub);

                                //    Services.BaseService.Update("DeleteSubstation_InfoByCode", temp.UID);
                                //}

                                //PSP_Substation_Info p2 = new PSP_Substation_Info();
                                //p2.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                                //p2.AreaID = tlVectorControl1.SVGDocument.SvgdataUid;
                                //Services.BaseService.Update("DeletePSP_Substation_InfoByELeID2", p2);

                                SVG_ENTITY pro = new SVG_ENTITY();
                                pro.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                                pro.svgID = tlVectorControl1.SVGDocument.SvgdataUid;
                                Services.BaseService.Update("DeleteSVG_ENTITYByEleID", pro);

                                //substation p = new substation();
                                //p.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                                //p.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                //Services.BaseService.Update("DeletesubstationByEleID", p);

                                //PSP_SubstationSelect sel = new PSP_SubstationSelect();
                                //sel.EleID = tlVectorControl1.SVGDocument.SelectCollection[i].ID;
                                //sel.SvgID = tlVectorControl1.SVGDocument.SvgdataUid;
                                //Services.BaseService.Update("DeletePSP_SubstationByEleID", sel);
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


                    tlVectorControl1.Delete();
                }
            }
        }
        public void GHWPG()
        {
            string output = null;
            frnReport wForm = new frnReport(); ;
            wForm.Owner = this;
            FileStream rs1;
            StreamWriter strRS1;
            Excel.Application mx = new Excel.Application(); ;
            Excel.Application xl = new Excel.Application(); ;
            StringBuilder stResult = new StringBuilder();
            bool N1 = false;
            String chaoliuSUID = null;
            try
            {
                frmOpenProject fop = new frmOpenProject();
                fop.ProjectID = Itop.Client.MIS.ProgUID;
                fop.Initdata();
                String strr = chaoliuSUID;
                stResult.Append("规划网架评估结果" + "\r\n" + "\r\n");
                //OpenProject op = new OpenProject();
                //op.ProjectID = Itop.Client.MIS.ProgUID;
                //op.Initdata();

                if (fop.ShowDialog() == DialogResult.OK)
                {
                    wForm.Text = this.Text + "—规划网评估";
                    wForm.Show();
                    Application.DoEvents();
                    if (fop.ChaoLiuSUID == null)
                    {
                        chaoliuSUID = fop.DuanLuSUID;
                    }
                    else
                    {
                        chaoliuSUID = fop.ChaoLiuSUID;
                    }
                    if (chaoliuSUID == null)
                    {
                        return;
                    }
                    if (chaoliuSUID != null)
                    {
                        wForm.ShowText = "正在进行潮流计算\t" + System.DateTime.Now.ToString();
                        ElectricLoadCal elc = new ElectricLoadCal();
                        bool flag = elc.LFC(chaoliuSUID, 1, 100, wForm);
                        if (flag == true)
                        {
                            wForm.ShowText += "\r\n正在处理潮流计算结果\t" + System.DateTime.Now.ToString();
                            // WaitDialogForm wf = new WaitDialogForm("", "正在处理数据, 请稍候...");
                            stResult.Append("1 潮流计算评价指标" + "\r\n");
                            stResult.Append("1.1 线路负载率" + "\r\n");
                            output = null;

                            output += ("全网交流线结果报表" + "\r\n" + "\r\n");
                            output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
                            output += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                            output += ("支路名称" + "," + "支路有功" + "," + "支路无功" + "," + "有功损耗" + "," + "无功损耗" + "," + "电流幅值" + "," + "电流相角" + "," + "越限标志" + "," + "\r\n");
                            string strCon1 = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + chaoliuSUID + "'" + " AND PSPDEV.Type = '05' order by PSPDEV.RateVolt,PSPDEV.Name";
                            IList list1 = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon1);
                            double fuzail = 0.0;
                            double Amax = 0;
                            double Amin = 0;
                            int i = 0, j = 0;
                            StringBuilder gfzlline = new StringBuilder();
                            foreach (PSP_ElcDevice elcDEV in list1)
                            {
                                PSPDEV dev = new PSPDEV();
                                dev.SUID = elcDEV.DeviceSUID;
                                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);
                                string lineF = "否";
                                if (dev.KSwitchStatus == "1")
                                {
                                    continue;
                                }
                                if (Convert.ToDouble(GetColValue(elcDEV, 0).COL14) * 1000 > (double)dev.Burthen)
                                {
                                    lineF = "是";
                                }
                                //double vTemp = Convert.ToDouble(GetColValue(elcDEV,type-1));
                                //double vTemp1 =  TLPSPVmin * dev.RateVolt;
                                //double vTemp2 =  TLPSPVmax * dev.RateVolt;
                                output += dev.Name.ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, 0).COL4).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, 0).COL5).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, 0).COL6).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, 0).COL7).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, 0).COL14).ToString() + "," + Convert.ToDouble(GetColValue(elcDEV, 0).COL15).ToString() + "," + lineF + "," + "\r\n";
                                double A = 0;
                                if (elcDEV.COL14 != "" || elcDEV.COL20 != "")
                                {
                                    A = (Convert.ToDouble(elcDEV.COL14) * 1000) / Convert.ToDouble(elcDEV.COL20);
                                }

                                if (i == 0)
                                {
                                    Amax = A;
                                    Amin = A;
                                    i++;
                                }
                                fuzail += A;
                                if (A > 1)
                                {
                                    j++;
                                    gfzlline.Append(dev.Name + "负载率" + A.ToString("#####.##") + ",");

                                }
                                if (Amin >= A)
                                {
                                    Amin = A;
                                }
                                else if (Amax < A)
                                {
                                    Amax = A;
                                }
                            }
                            try
                            {
                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result1.csv"))
                                {
                                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                                }
                            }
                            catch (System.Exception ex3)
                            {
                                MessageBox.Show("请关闭相关Excel后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            rs1 = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result1.csv"), FileMode.OpenOrCreate);
                            strRS1 = new StreamWriter(rs1, Encoding.Default);
                            strRS1.Write(output);
                            strRS1.Close();
                            xl.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result1.csv");
                            fuzail = fuzail / list1.Count;
                            stResult.Append("平均负载率：," + fuzail.ToString() + ",");
                            stResult.Append("最小负载率：," + Amin.ToString() + ",");
                            stResult.Append("最大负载率：," + Amax.ToString() + ",");
                            stResult.Append("过载线路条数：," + j.ToString() + "\r\n");
                            if (j > 0)
                            {
                                stResult.Append("过载的线路为：" + gfzlline.ToString() + "\r\n");
                            }

                            strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + chaoliuSUID + "'" + " AND PSPDEV.Type = '02' order by PSPDEV.RateVolt,PSPDEV.Name";
                            IList list2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon1);
                            strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + chaoliuSUID + "'" + " AND PSPDEV.Type = '03' order by PSPDEV.RateVolt,PSPDEV.Name";
                            IList list3 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon1);
                            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                            {
                            }
                            else
                            {
                                return;
                            }
                            FileStream ih = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);
                            StreamReader readLine = new StreamReader(ih, Encoding.Default);
                            string strLine = readLine.ReadLine();
                            char[] charSplit = new char[] { ' ' };
                            double byqFuZaiLv = 0.0;
                            double Bmax = 0;
                            double Bmin = 0;
                            int bi = 0, bj = 0;
                            gfzlline = new StringBuilder();
                            IList listobject = new List<object>();
                            while (strLine != null && strLine != "")
                            {
                                string[] array1 = strLine.Split(charSplit, StringSplitOptions.RemoveEmptyEntries);
                                listobject.Add(array1);
                                foreach (PSPDEV byq in list2)
                                {
                                    if (byq.Name == array1[0])
                                    {
                                        double tempA = Math.Sqrt(Convert.ToDouble(array1[3]) * Convert.ToDouble(array1[3]) + Convert.ToDouble(array1[4]) * Convert.ToDouble(array1[4])) / ((double)byq.Burthen);
                                        byqFuZaiLv += tempA;
                                        if (bi == 0)
                                        {
                                            Bmax = tempA;
                                            Bmin = tempA;
                                            bi++;
                                        }
                                        if (tempA > 1)
                                        {
                                            bj++;
                                            gfzlline.Append(byq.Name + "负载率" + tempA.ToString("#####.##") + ",");
                                        }
                                        if (Bmin >= tempA)
                                        {
                                            Bmin = tempA;
                                        }
                                        else if (Bmax < tempA)
                                        {
                                            Bmax = tempA;
                                        }
                                    }
                                }
                                strLine = readLine.ReadLine();
                            }
                            readLine.Close();
                            foreach (PSPDEV byq3 in list3)
                            {
                                double p = 0;
                                double q = 0;
                                foreach (string[] obj in listobject)
                                {
                                    if (obj[0] == byq3.Name && (obj[1] == byq3.KName || obj[2] == byq3.KName))
                                    {
                                        p += Convert.ToDouble(obj[3]);
                                        q += Convert.ToDouble(obj[4]);
                                    }
                                }
                                double tempA = Math.Sqrt(p * p + q * q) / byq3.SiN;
                                byqFuZaiLv += tempA;
                                if (bi == 0)
                                {
                                    Bmax = tempA;
                                    Bmin = tempA;
                                    bi++;
                                }
                                if (tempA > 1)
                                {
                                    bj++;
                                    gfzlline.Append(byq3.Name + "负载率" + tempA.ToString("#####.##") + ",");
                                }
                                if (Bmin >= tempA)
                                {
                                    Bmin = tempA;
                                }
                                else if (Bmax < tempA)
                                {
                                    Bmax = tempA;
                                }
                            }
                            if (list2.Count + list3.Count == 0)
                            {
                                byqFuZaiLv = 0;
                            }
                            else
                            {
                                byqFuZaiLv = byqFuZaiLv / (list2.Count + list3.Count);
                            }

                            stResult.Append("1.2 变压器负载率" + "\r\n");
                            stResult.Append("平均负载率：," + byqFuZaiLv.ToString() + ",");
                            stResult.Append("最小负载率：," + Bmin.ToString() + ",");
                            stResult.Append("最大负载率：," + Bmax.ToString() + ",");
                            stResult.Append("过载变压器台数：," + bj.ToString() + "\r\n");
                            if (bj > 0)
                            {
                                stResult.Append("过载变压器为：" + gfzlline.ToString() + "\r\n");
                            }
                            string strCon = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + chaoliuSUID + "'" + " AND PSPDEV.Type = '01' order by PSPDEV.RateVolt,PSPDEV.Name";
                            IList list4 = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon);
                            double voltP = 0;
                            double Vmax = 0, Vmin = 0;
                            int vi = 0, vj = 0;
                            output = null;
                            gfzlline = new StringBuilder();
                            output += ("全网母线(发电、负荷)结果报表 " + "\r\n" + "\r\n");
                            output += ("单位：kA\\kV\\MW\\Mvar" + "\r\n" + "\r\n");
                            output += ("计算日期：" + System.DateTime.Now.ToString() + "\r\n" + "\r\n");
                            output += ("母线名" + "," + "电压幅值" + "," + "电压相角" + "," + "有功发电" + "," + "无功发电" + "," + "有功负荷" + "," + "无功负荷" + "," + "越限标志" + "," + "过载标志" + "\r\n");
                            foreach (PSP_ElcDevice busDEV in list4)
                            {
                                string voltF = "否";
                                string pF = "否";
                                PSPDEV dev = new PSPDEV();
                                dev.SUID = busDEV.DeviceSUID;
                                dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);
                                //double vTemp = Convert.ToDouble(GetColValue(elcDEV,type-1));
                                //double vTemp1 =  TLPSPVmin * dev.RateVolt;
                                //double vTemp2 =  TLPSPVmax * dev.RateVolt;
                                if (dev == null)
                                {
                                    continue;
                                }
                                double voltExcursion = 0.0;
                                voltExcursion = (Convert.ToDouble(busDEV.COL2) - dev.RateVolt) / dev.RateVolt;
                                voltP += Math.Abs(voltExcursion);
                                if (vi == 0)
                                {
                                    Vmax = voltExcursion;
                                    Vmin = voltExcursion;
                                    vi++;
                                }
                                if (voltFlag(voltExcursion, dev.RateVolt) == "不合格")
                                {
                                    vj++;
                                    gfzlline.Append(dev.Name + ",");
                                }
                                if (Vmin >= voltExcursion)
                                {
                                    Vmin = voltExcursion;
                                }
                                else if (Vmax < voltExcursion)
                                {
                                    Vmax = voltExcursion;
                                }
                                if (dev.KSwitchStatus == "1")
                                {
                                    continue;
                                }
                                if (Convert.ToDouble(GetColValue(busDEV, 0).COL2) < dev.iV || Convert.ToDouble(GetColValue(busDEV, 0).COL2) > dev.jV)
                                {
                                    voltF = "是";
                                }
                                if (Convert.ToDouble(GetColValue(busDEV, 0).COL2) > (double)dev.Burthen)
                                {
                                    pF = "是";
                                }

                                if (Convert.ToDouble(GetColValue(busDEV, 0).COL4) > 0)
                                {


                                    output += dev.Name + "," + Convert.ToDouble(GetColValue(busDEV, 0).COL2).ToString() + "," + Convert.ToDouble(GetColValue(busDEV, 0).COL3).ToString() + "," + Convert.ToDouble(GetColValue(busDEV, 0).COL4).ToString() + "," + Convert.ToDouble(GetColValue(busDEV, 0).COL5).ToString() + "," + "0" + "," + "0" + "," + voltF + "," + pF + "\r\n";


                                }
                                else
                                {

                                    output += dev.Name + "," + Convert.ToDouble(GetColValue(busDEV, 0).COL2).ToString() + "," + Convert.ToDouble(GetColValue(busDEV, 0).COL3).ToString() + "," + "0" + "," + "0" + "," + Convert.ToDouble(GetColValue(busDEV, 0).COL4).ToString() + "," + Convert.ToDouble(GetColValue(busDEV, 0).COL5).ToString() + "," + voltF + "," + pF + "\r\n";


                                }
                            }
                            try
                            {
                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\result.csv"))
                                {
                                    File.Delete(System.Windows.Forms.Application.StartupPath + "\\result.csv");
                                }
                            }
                            catch (System.Exception ex2)
                            {
                                MessageBox.Show("请关闭相关Excel后再查看结果！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            rs1 = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result.csv"), FileMode.OpenOrCreate);
                            strRS1 = new StreamWriter(rs1, Encoding.Default);
                            strRS1.Write(output);
                            strRS1.Close();
                            mx.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result.csv"); ;
                            voltP = voltP / list4.Count;
                            stResult.Append("1.3 节点电压偏移" + "\r\n");
                            stResult.Append("平均节点电压偏移：," + voltP.ToString() + ",");
                            stResult.Append("最大下偏移：," + Vmin.ToString() + ",");
                            stResult.Append("最大上偏移：," + Vmax.ToString() + ",");
                            stResult.Append("电压越限节点数：," + vj.ToString() + "\r\n");
                            if (vj > 0)
                            {
                                stResult.Append("电压越限的节点为：" + gfzlline.ToString() + "\r\n");
                            }
                            {
                                string strCon11 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + chaoliuSUID + "'";
                                string strCon22 = null;
                                string strCon33 = null;
                                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\DH1.txt"))
                                {
                                }
                                else
                                {
                                    return;
                                }
                                FileStream dh = new FileStream(System.Windows.Forms.Application.StartupPath + "\\DH1.txt", FileMode.Open);
                                StreamReader readLine1 = new StreamReader(dh, Encoding.Default);
                                char[] charSplit1 = new char[] { ' ' };
                                string strLine1 = readLine1.ReadLine();
                                double temp1 = 0;
                                double temp2 = 0;
                                double temp3 = 0; ;
                                double temp4 = 0;
                                double temp5 = 0, temp6 = 0, temp7 = 0, temp8 = 0;
                                string[] array1 = strLine.Split(charSplit);

                                string strCon3 = ",PSPDEV WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + chaoliuSUID + "'" + " AND PSPDEV.Type = '01' order by PSPDEV.RateVolt,PSPDEV.Name";
                                IList list = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", strCon3);
                                double tempAD = 0;
                                foreach (PSP_ElcDevice elcDEV in list)
                                {
                                    PSPDEV dev = new PSPDEV();
                                    dev.SUID = elcDEV.DeviceSUID;
                                    dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", dev);
                                    tempAD += dev.OutP;
                                    if (Convert.ToDouble(GetColValue(elcDEV, 0).COL4) < 0 && dev.NodeType == "0")
                                    {
                                        tempAD += Convert.ToDouble(GetColValue(elcDEV, 0).COL4);
                                    }
                                }
                                if (tempAD == 0)
                                {
                                    tempAD = 1;
                                }
                                while (strLine1 != null && strLine1 != "")
                                {
                                    array1 = strLine1.Split(charSplit);

                                    string[] dev = new string[20];
                                    dev.Initialize();
                                    int ii = 0;


                                    foreach (string str in array1)
                                    {
                                        if (str != "")
                                        {
                                            if (ii == 0)
                                            {
                                                dev[ii++] = str.ToString();
                                            }
                                            else
                                            {
                                                if (str != "NaN")
                                                {
                                                    dev[ii++] = Convert.ToDouble(str).ToString();
                                                }
                                                else
                                                {
                                                    dev[ii++] = str;
                                                }

                                            }
                                        }

                                    }
                                    strCon22 = " AND Type= '05' AND Name = '" + array1[0] + "' AND FirstNode = " + array1[1] + " AND LastNode = " + array1[2];
                                    strCon33 = strCon11 + strCon22;
                                    PSPDEV CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon33);

                                    if (CR != null)
                                    {
                                        temp1 += Math.Abs(Convert.ToDouble(dev[5]) * 100);
                                        temp2 += Math.Abs(Convert.ToDouble(dev[6]) * 100);
                                        temp5 += Math.Abs(Convert.ToDouble(dev[3]) * 100);
                                        temp6 += Math.Abs(Convert.ToDouble(dev[4]) * 100);
                                    }
                                    else
                                    {
                                        strCon22 = " AND Type= '02' AND Name = '" + array1[0] + "'";
                                        strCon33 = strCon11 + strCon22;
                                        CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon33);
                                        if (CR == null)
                                        {
                                            strCon22 = " AND Type= '03' AND Name = '" + array1[0] + "'";
                                            strCon33 = strCon11 + strCon22;
                                            CR = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon33);
                                        }
                                        if (CR != null)
                                        {
                                            temp3 += Math.Abs(Convert.ToDouble(dev[5]) * 100);
                                            temp4 += Math.Abs(Convert.ToDouble(dev[6]) * 100);
                                            temp7 += Math.Abs(Convert.ToDouble(dev[3]) * 100);
                                            temp8 += Math.Abs(Convert.ToDouble(dev[4]) * 100);
                                        }
                                    }
                                    strLine1 = readLine1.ReadLine();
                                }
                                double lineLoss = temp1 / temp5;
                                double bLoss = temp3 / temp7;
                                if (temp5 == 0)
                                {
                                    lineLoss = 0;
                                }
                                if (temp7 == 0)
                                {
                                    bLoss = 0;
                                }
                                readLine1.Close();
                                stResult.Append("2 线损率" + "\r\n");
                                stResult.Append("线路有功损耗：," + lineLoss.ToString() + ",");
                                stResult.Append("变压器有功损耗：," + bLoss.ToString() + "\r\n");
                            }
                            wForm.ShowText += "\r\n正在进行N-1校验\t" + System.DateTime.Now.ToString();
                            ElcRel er = new ElcRel();
                            N1 = er.WebCalAndPrint(chaoliuSUID, Itop.Client.MIS.ProgUID, 100);
                            // wf.Close();
                        }
                        else
                        {
                            wForm.ShowText += "\r\n计算失败\t" + System.DateTime.Now.ToString();
                            MessageBox.Show("数据不收敛，请检查数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }

                    {
                        if (fop.DuanLuSUID != null)
                        {
                            Application.DoEvents();
                            //WaitDialogForm wait = null;
                            //wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                            wForm.ShowText += "\r\n正在进行短路计算\t" + System.DateTime.Now.ToString();
                            ElectricShorti esc = new ElectricShorti();
                            esc.AllShortWJ(fop.DuanLuSUID, fop.ProjectID, 0, 100, null);
                            wForm.ShowText += "\r\n正在处理短路计算结果\t" + System.DateTime.Now.ToString();
                        }
                        else
                        {
                            Application.DoEvents();
                            //WaitDialogForm wait = null;
                            //wait = new WaitDialogForm("", "正在处理数据, 请稍候...");
                            wForm.ShowText += "\r\n正在进行短路计算\t" + System.DateTime.Now.ToString();
                            ElectricShorti esc = new ElectricShorti();
                            esc.AllShortWJ(chaoliuSUID, fop.ProjectID, 0, 100, null);
                            wForm.ShowText += "\r\n正在处理短路计算结果\t" + System.DateTime.Now.ToString();
                        }
                        // wait.Close();
                        if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt"))
                        {
                            StringBuilder dlrl = new StringBuilder();
                            FileStream shorcuit = new FileStream(System.Windows.Forms.Application.StartupPath + "\\ShortcuitI.txt", FileMode.Open);
                            StreamReader readLineGU = new StreamReader(shorcuit, System.Text.Encoding.Default);
                            string strLineGU;
                            string[] arrayGU;
                            char[] charSplitGU = new char[] { ' ' };
                            string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + chaoliuSUID + "'";
                            string strCon2 = null;
                            string strCon = null;
                            double Ik = 0;
                            double Imax = 0;
                            double Imin = 0;
                            int ii = 0, ij = 0;
                            int num = 0;
                            strLineGU = readLineGU.ReadLine();
                            while ((strLineGU = readLineGU.ReadLine()) != null)
                            {
                                arrayGU = strLineGU.Split(charSplitGU, StringSplitOptions.RemoveEmptyEntries);
                                strCon2 = " AND Type= '01' AND Number = " + arrayGU[2] + " AND Name = '" + arrayGU[1] + "'";
                                strCon = strCon1 + strCon2;
                                PSPDEV devMX = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", strCon);
                                if (devMX != null)
                                {
                                    double tempI = Convert.ToDouble(arrayGU[3]);
                                    Ik += tempI;
                                    num++;
                                    if (ii == 0)
                                    {
                                        Imax = tempI;
                                        Imin = tempI;
                                        ii++;
                                    }
                                    if (!shortI(tempI, (int)devMX.RateVolt))
                                    {
                                        ij++;
                                        dlrl.Append(devMX.Name + ",");
                                    }
                                    if (Imin >= tempI)
                                    {
                                        Imin = tempI;
                                    }
                                    else if (Imax < tempI)
                                    {
                                        Imax = tempI;
                                    }

                                }
                                else
                                {
                                    continue;
                                }

                            }
                            readLineGU.Close();
                            Ik = Ik / num;
                            stResult.Append("3 短路电流计算评价指标" + "\r\n");
                            stResult.Append("平均短路电流：," + Ik.ToString() + ",");
                            stResult.Append("最小短路电流：," + Imin.ToString() + ",");
                            stResult.Append("最大短路电流：," + Imax.ToString() + ",");
                            stResult.Append("短路电流超标母线数：," + ij.ToString() + "\r\n");
                            if (ij > 0)
                            {
                                stResult.Append("短路电流超标的母线为：" + dlrl.ToString() + "\r\n");
                            }
                        }
                        else
                        {
                            wForm.ShowText += "\r\n短路计算失败!\t" + System.DateTime.Now.ToString();
                        }

                    }


                    stResult.Append("4 N-1校核" + "\r\n");
                    if (N1)
                    {
                        stResult.Append("通过或不通过：," + "通过");
                    }
                    else
                    {
                        stResult.Append("通过或不通过：," + "不通过");
                    }
                    wForm.ShowText += "\r\n正在形成报表\t" + System.DateTime.Now.ToString();
                    if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\" + "result2.csv"))
                    {
                        File.Delete(System.Windows.Forms.Application.StartupPath + "\\" + "result2.csv");
                    }
                    FileStream re = new FileStream((System.Windows.Forms.Application.StartupPath + "\\result2.csv"), FileMode.OpenOrCreate);
                    StreamWriter str1 = new StreamWriter(re, Encoding.Default);
                    str1.Write(stResult);
                    str1.Close();


                    Excel.Application ex = new Excel.Application();
                    ex.Application.Workbooks.Add(System.Windows.Forms.Application.StartupPath + "\\result2.csv");
                    Excel.Worksheet xSheet1 = (Excel.Worksheet)ex.Worksheets[1];
                    ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet1, 2, System.Reflection.Missing.Value);

                    xSheet1.Name = "规划网评估";
                    xSheet1.get_Range(xSheet1.Cells[1, 1], xSheet1.Cells[1, 8]).MergeCells = true;
                    xSheet1.get_Range(xSheet1.Cells[1, 1], xSheet1.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xSheet1.get_Range(xSheet1.Cells[1, 1], xSheet1.Cells[1, 1]).Font.Size = 16;
                    xSheet1.get_Range(xSheet1.Cells[1, 1], xSheet1.Cells[1, 1]).Font.Name = "黑体";
                    xSheet1.get_Range(xSheet1.Cells[5, 2], xSheet1.Cells[5, 6]).NumberFormatLocal = "0.000%";
                    xSheet1.get_Range(xSheet1.Cells[8, 2], xSheet1.Cells[8, 6]).NumberFormatLocal = "0.000%";
                    xSheet1.get_Range(xSheet1.Cells[12, 2], xSheet1.Cells[12, 6]).NumberFormatLocal = "0.000%";
                    xSheet1.Rows.AutoFit();
                    xSheet1.Columns.AutoFit();

                    Excel.Worksheet xSheet = (Excel.Worksheet)ex.Worksheets.get_Item(2);
                    //ex.Worksheets.Add(System.Reflection.Missing.Value, xSheet, 1, System.Reflection.Missing.Value);
                    Excel.Worksheet newWorksheet = (Excel.Worksheet)ex.Worksheets.get_Item(3);
                    // ex.Worksheets.Add(System.Reflection.Missing.Value, newWorksheet, 3, System.Reflection.Missing.Value);
                    Excel.Worksheet tempSheet = (Excel.Worksheet)mx.Worksheets.get_Item(1);
                    tempSheet.Cells.Select();
                    tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                    xSheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                    System.Windows.Forms.Clipboard.Clear();
                    tempSheet = (Excel.Worksheet)xl.Worksheets.get_Item(1);
                    tempSheet.Cells.Select();
                    tempSheet.Cells.Copy(System.Reflection.Missing.Value);
                    newWorksheet.Paste(System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                    newWorksheet.Name = "线路电流";
                    xSheet.Name = "母线潮流";


                    xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 9]).MergeCells = true;
                    xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Size = 20;
                    xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).Font.Name = "黑体";
                    xSheet.get_Range(xSheet.Cells[1, 1], xSheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xSheet.get_Range(xSheet.Cells[7, 1], xSheet.Cells[7, 9]).Interior.ColorIndex = 45;
                    xSheet.get_Range(xSheet.Cells[8, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                    xSheet.get_Range(xSheet.Cells[8, 2], xSheet.Cells[xSheet.UsedRange.Rows.Count, 9]).NumberFormat = "0.0000_ ";
                    xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 9]).Font.Name = "楷体_GB2312";
                    //xSheet.get_Range(xSheet.Cells[3, 1], xSheet.Cells[xSheet.UsedRange.Rows.Count, 1]).NumberFormatLocal = "@";

                    newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 9]).MergeCells = true;
                    newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Size = 20;
                    newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).Font.Name = "黑体";
                    newWorksheet.get_Range(newWorksheet.Cells[1, 1], newWorksheet.Cells[1, 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    newWorksheet.get_Range(newWorksheet.Cells[7, 1], newWorksheet.Cells[7, 8]).Interior.ColorIndex = 45;
                    newWorksheet.get_Range(newWorksheet.Cells[8, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).Interior.ColorIndex = 6;
                    // newWorksheet.get_Range(newWorksheet.Cells[6, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 1]).NumberFormatLocal = "@";
                    newWorksheet.get_Range(newWorksheet.Cells[8, 2], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).NumberFormat = "0.0000_ ";
                    newWorksheet.get_Range(newWorksheet.Cells[3, 1], newWorksheet.Cells[newWorksheet.UsedRange.Rows.Count, 9]).Font.Name = "楷体_GB2312";

                    //op = new FileStream((System.Windows.Forms.Application.StartupPath + "\\fck.excel"), FileMode.OpenOrCreate);
                    //str1 = new StreamWriter(op, Encoding.Default);
                    xSheet.Rows.AutoFit();
                    xSheet.Columns.AutoFit();
                    newWorksheet.Rows.AutoFit();
                    newWorksheet.Columns.AutoFit();
                    System.Windows.Forms.Clipboard.Clear();
                    ex.DisplayAlerts = false;
                    ex.Visible = true;
                    wForm.ShowText += "\r\n网架评估结束\t" + System.DateTime.Now.ToString();
                }
                //op.Initdata();  //临时修改
                //if (op.ShowDialog() == DialogResult.OK)
                //{

                //}

            }
            catch
            {
                wForm.ShowText += "\r\n网架评估失败\t" + System.DateTime.Now.ToString();
            }
            //wForm.Close();
        }
        private bool shortI(double tempI, int rateVolt)
        {
            switch (rateVolt)
            {
                case 500:
                case 330:
                    if (tempI > 63)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                case 220:
                    if (tempI > 50)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                case 110:
                case 66:
                    if (tempI > 31.5)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                case 35:
                    if (tempI > 25)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                case 20:
                case 10:
                    if (tempI > 16)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                default:
                    if (tempI > 50)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
            }
        }
        private string voltFlag(double voltExcursion, double rateVolt)
        {
            if (rateVolt >= 35)
            {
                if (Math.Abs(voltExcursion) > 0.05)
                {
                    return "不合格";
                }
                else
                {
                    return "合格";
                }

            }
            else if (rateVolt < 20 && rateVolt > 0.22)
            {
                if (Math.Abs(voltExcursion) > 0.07)
                {
                    return "不合格";
                }
                else
                {
                    return "合格";
                }
            }
            else if (rateVolt == 0.22)
            {
                if (voltExcursion > 0.075 || voltExcursion < -0.1)
                {
                    return "不合格";
                }
                else
                {
                    return "合格";
                }
            }
            else
            {
                if (Math.Abs(voltExcursion) > 0.05)
                {
                    return "不合格";
                }
                else
                {
                    return "合格";
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

                            ArrayList _slist = flar.list2;
                            for (int i = 0; i < _slist.Count; i++)
                            {
                                XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("use") as XmlElement;
                                e1.SetAttribute("x", ele.GetAttribute("x"));
                                e1.SetAttribute("y", ele.GetAttribute("y"));
                                e1.GetAttribute("transform", ele.GetAttribute("transform"));
                                e1.SetAttribute("xlink:href", ele.GetAttribute("xlink:href"));
                                e1.SetAttribute("style", ele.GetAttribute("style"));
                                //e1.SetAttribute("CopyOf", ele.ID);
                                e1.SetAttribute("layer", getlayer(_slist[i].ToString(), tlVectorControl1.SVGDocument.getLayerList()).ID);
                                tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);
                                e1.SetAttribute("Deviceid", ele.GetAttribute("Deviceid"));

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
        public void LoadLayers(string layers) {

        }
        public void Linkage2()
        {

            string id = "";
            frmLayerGrade fgrade = new frmLayerGrade();
            fgrade.SymbolDoc = tlVectorControl1.SVGDocument;
            fgrade.InitData(SVGUID);
            if (fgrade.ShowDialog() == DialogResult.OK)
            {
                id = fgrade.GetSelectNode();
            }
            if (id.Length > 4)
            {
                string[] strid = id.Split(",".ToCharArray());

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
                    if (!subList.Contains(ele.ID))
                    {
                        subList.Add(ele.ID, ele);
                    }
                    
                }
                if (ele.Name == "polyline" && ele.GetAttribute("IsLead") == "1")
                {
                    if (!LineList.Contains(ele.ID))
                    {
                        LineList.Add(ele.ID, ele);
                    }
                    
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
                                        PSPDEV p1 = new PSPDEV();
                                        //substation sub1 = new substation();
                                        //sub1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        //sub1.EleID = sub.GetAttribute("id");
                                        //SelectPSPDEVBySvgUIDandEleID
                                        //sub1 = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", sub1);
                                        p1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        p1.EleID = sub.GetAttribute("id");
                                        p1 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", p1);
                                        if (p1 != null)
                                        {
                                            PSPDEV p2 = new PSPDEV();
                                            p2.EleID = ele.ID;
                                            p2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                            p2 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", p2);
                                            if (p2 != null)
                                            {
                                                string uid = p2.SUID;
                                                string eleid = p2.EleID;
                                                p2 = p1;
                                                p2.SUID = uid;
                                                p2.EleID = eleid;
                                                Services.BaseService.Update<PSPDEV>(p2);
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

                                        PSPDEV line1 = new PSPDEV();
                                        line1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                        line1.EleID = line.GetAttribute("id");
                                        line1 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", line1);

                                        if (line1 != null)
                                        {
                                            PSPDEV line2 = new PSPDEV();
                                            line2.EleID = ele.ID;
                                            line2.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                                            line2 = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUIDandEleID", line2);
                                            if (line2 != null)
                                            {
                                                string uid = line2.SUID;
                                                string eleid = line2.EleID;
                                                line2 = line1;
                                                line2.SUID = uid;
                                                line2.EleID = eleid;
                                                Services.BaseService.Update<PSPDEV>(line2);
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
            RectangleF rtf1 = rt1.GPath.GetBounds(rt1.Transform.Matrix);
            int width = (int)Math.Round(rtf1.Width * tlVectorControl1.ScaleRatio, 0) + 1;
            int height = (int)Math.Round(rtf1.Height * tlVectorControl1.ScaleRatio, 0) + 1;
            //if (width < 7000 && height < 7000)
            {
                //System.Drawing.Image image = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
                System.Drawing.Image image = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
                Graphics g = Graphics.FromImage(image);

                g.SmoothingMode = SmoothingMode.HighSpeed;//.HighQuality;

                //g.CompositingQuality = CompositingQuality.HighSpeed;//.HighQuality;
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
                if (mapview is MapViewGoogle)
                    longlat = mapview.OffSet(mapview.ZeroLongLat, mapview.Getlevel(1), -center.X, -center.Y);
                else
                    longlat = mapview.OffSetZero((int)(-center.X * tlVectorControl1.ScaleRatio), (int)(-center.Y * tlVectorControl1.ScaleRatio));


                //g.Clear(ColorTranslator.FromHtml("#EBEAE8"));

                ImageAttributes imageA = new ImageAttributes();
                ColorMatrix matrix1 = new ColorMatrix();
                matrix1.Matrix00 = 1f;
                matrix1.Matrix11 = 1f;
                matrix1.Matrix22 = 1f;
                matrix1.Matrix33 = this.mapOpacity / 100f;//地图透明度


                matrix1.Matrix44 = 1f;
                //设置地图透明度                    
                imageA.SetColorMatrix(matrix1);

                Color color4 = ColorTranslator.FromHtml("#FFFFFF");
                int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
                string Transparent = ConfigurationSettings.AppSettings.Get("Transparent");
                if (string.IsNullOrEmpty(Transparent)) Transparent = "#EBEAE8";
                //g.Clear(color4);
                //g.Clear(Color.White);
                if (chose == 1)
                {
                    Color color = ColorTranslator.FromHtml(Transparent);
                    imageA.SetColorKey(color, color);
                }
                else if (chose == 2)
                {
                    Color color2 = ColorTranslator.FromHtml("#f2efe9");
                    imageA.SetColorKey(color2, color2);
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
                if (progtype == "变电站选址")
                {
                    if (lar.GetAttribute("layerType") =="电网规划层")
                    { 
                        DwBarVisible_SH(false);
                    }
                   
                }

#if(CITY)
                tlVectorControl1.CanEdit = true;
                DwBarVisible(true);
  
#endif
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
            t04.InnerText = "XXXX 电 业 局";
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
                //RectangleF rect = graph1.GetBounds();
                PointF pf1=graph1.CenterPoint;//rabbit edit 2011.11.24
                glebeProperty gle = new glebeProperty();
                gle.EleID = graph1.ID;
                gle.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                gle = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", gle);

                if (gle != null)
                {
                    XmlElement n1 = tlVectorControl1.SVGDocument.CreateElement("text") as Text;
                    //Point point1 = tlVectorControl1.PointToView(new Point(e.Mouse.X, e.Mouse.Y));
                    n1.SetAttribute("x",pf1.X.ToString());// Convert.ToString(rect.X + rect.Width / 2));
                    n1.SetAttribute("y", pf1.Y.ToString());// Convert.ToString(rect.Y + rect.Height / 2));
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
            if (progtype == "电网规划层" || progtype == "变电站选址")
            {
                Open(_SvgUID);
            }
            else
            {
                Open2(_SvgUID);
            }
            this.tlVectorControl1.Size = new Size((Screen.PrimaryScreen.WorkingArea.Height - 258), (Screen.PrimaryScreen.WorkingArea.Width - 176));
        }
        private string startyear="";
        public void LayerManagerShow()
        {
            frmlar.SymbolDoc = tlVectorControl1.SVGDocument;
            frmlar.YearID = yearID;
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
            frmlar.OnCheck += new OnCheckhandler(frmlar_OnCheck);
            // Init(progtype);
            frmlar.ShowInTaskbar = false;
            frmlar.Top = 25;//Screen.PrimaryScreen.WorkingArea.Height - 500;
            frmlar.Left = Screen.PrimaryScreen.WorkingArea.Width - frmlar.Width;

            string myear = yearID.Replace("'", "");
            string[] y = myear.Split(",".ToCharArray());
            if (y.Length > 1)
            {
                LayerGrade lar = new LayerGrade();
                lar.SUID = y[1];
                lar = (LayerGrade)Services.BaseService.GetObject("SelectLayerGradeByKey", lar);
                startyear = frmlar.StrYear = lar.Name.Substring(0, 4);
                
            }
            frmlar.Show();
        }

        void frmlar_OnCheck(object sender)
        {
            tlVectorControl1.Refresh();
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
                //dotNetBarManager1.Bars["mainmenu"].GetItem("WJYHBut").Visible = false; //为版本用
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

        }
        public void OpenJXT(IList svglist, SVGFILE svg)
        {
            if (string.IsNullOrEmpty(svg.FILENAME)) return;
            frmMain2 frm = new frmMain2(svglist, svg.SUID);
            frm.Owner = this.ParentForm;
            frm.Show();
            frm.LoadShape("symbol4.xml");
            frm.OpenJXT(svglist, svg);

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
                string title = " ";
                string t = "";
                getProjName(MIS.ProgUID, ref title);

                this.Text = title + " " + t + " 城市规划";
            }
            if (progtype == "电网规划层")
            {
                //this.Text = "电网规划";
                string title = " ";
                string t = "";
                getProjName(MIS.ProgUID, ref title);

                string[] str = yearID.Split(",".ToCharArray());
                if (str.Length > 1)
                {
                    for (int i = 1; i < str.Length; i++)
                    {
                        LayerGrade ll = Services.BaseService.GetOneByKey<LayerGrade>(str[i].Replace("'", ""));
                        if (ll != null)
                        {
                            if (ll.ParentID != "SUID")
                            {
                                LayerGrade ll2 = Services.BaseService.GetOneByKey<LayerGrade>(ll.ParentID);
                                t = t + ll2.Name + " " + ll.Name + "， ";
                            }
                        }
                    }
                }

                this.Text = title + " " + t + " 电网规划";
                for (int i = 0; i < layerlist.Count; i++)
                {
                    Layer lar = (Layer)layerlist[i];
                    tmplaylist.Add(layerlist[i]);
                }
                DwBarVisible(false);
                tlVectorControl1.CanEdit = false;
            }
            if (progtype == "变电站选址")
            {
                //this.Text = "电网规划";
                string title = " ";
                string t = "";
                getProjName(MIS.ProgUID, ref title);

                string[] str = yearID.Split(",".ToCharArray());
                if (str.Length > 1)
                {
                    for (int i = 1; i < str.Length; i++)
                    {
                        LayerGrade ll = Services.BaseService.GetOneByKey<LayerGrade>(str[i].Replace("'", ""));
                        if (ll != null)
                        {
                            if (ll.ParentID != "SUID")
                            {
                                LayerGrade ll2 = Services.BaseService.GetOneByKey<LayerGrade>(ll.ParentID);
                                t = t + ll2.Name + " " + ll.Name + "， ";
                            }
                        }
                    }
                }

                this.Text = title + " " + t + " 变电站选址";
                for (int i = 0; i < layerlist.Count; i++)
                {
                    Layer lar = (Layer)layerlist[i];
                    tmplaylist.Add(layerlist[i]);
                }
                DwBarVisible_SH(false);
                tlVectorControl1.CanEdit = false;
            }


            if (MapType == "所内接线图")
            {
                JxtBar();
                LoadImage = false;
                bk1.Visible = false;
            }
        }
        void ctlfile_OnOpenSvgDocument(object sender, string _svgUid)
        {
            Open(_svgUid);
        }
        public void getProjName(string uid, ref string title)
        {
            Project sm = Services.BaseService.GetOneByKey<Project>(uid);
            if (sm != null)
            {
                title = sm.ProjectName + " " + title;
                if (sm.ProjectManager == sm.UID) { return; }
                getProjName(sm.ProjectManager, ref title);
            }
            //return title;
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
            string[] strn = yearID.Replace("'", "").Split(",".ToCharArray());

            for (int i = 0; i < strn.Length; i++)
            {
                if (strn[i] != "")
                {
                    SaveID.Add(strn[i]);
                }
            }

            frmLayerManager.ilist = SaveID;
            frmLayerTreeManager.ilist = SaveID;
        }
        string xltProcessor_OnNewLine(List<string> existLineCode)
        {

            return DateTime.Now.Minute.ToString();
        }
        public void InitScaleRatio()
        {
            int chose = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("chose"));
            if (chose == 1)
            {
                tlVectorControl1.ScaleRatio = 0.1f;
                this.scaleBox.Text = "10%";
            }
            if (chose == 2)
            {
                tlVectorControl1.ScaleRatio = 0.015625f;
                this.scaleBox.Text = "1.5625%";
            }
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

                //scaleBox.ComboBoxEx.Text = "100%";
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
            string tems_id = "";
            string[] stryearid = yearID.Split(",".ToCharArray());
            if (stryearid.Length > 1)
            {
                tems_id = stryearid[1].Replace("'", "");
                tems_id = tems_id.Replace("\"", "");
            }
            //layer
            ArrayList list = tlVectorControl1.SVGDocument.getLayerList();
            ArrayList saveList = new ArrayList();
            foreach (Layer layer in list)
            {
                //if(ChangeLayerList.Contains(layer.ID))
                saveList.Add(layer);
            }
            if (progtype == "城市规划层")
            {

                frmLayerSel2 dlg = new frmLayerSel2();
                dlg.Text = "选择要保存的图层";
                dlg.LayList = saveList;
                if (dlg.ShowDialog() == DialogResult.OK) saveList = dlg.LayList;
            }

            for (int i = 0; i < list.Count; i++)
            {
                txt = "";
                Layer lar = list[i] as Layer;
                bool IsSave = false;

                for (int m = 0; m < SaveID.Count; m++)
                {
                    if (lar.GetAttribute("ParentID") == SaveID[m].ToString())
                    {
                        IsSave = true;
                    }
                }
                for (int j = 0; j < frmlar.NoSave.Count; j++) {
                    if (lar.ID == ((Layer)frmlar.NoSave[j]).ID) {
                        IsSave = false;
                    }
                }
                for (int j = 0; j < saveList.Count; j++)
                {
                    if ((lar.ID == (saveList[j] as Layer).ID) && IsSave)
                    //if (IsSave) 
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
                            _svg.svgID = SVGUID;
                            _svg.XML = txt;
                            _svg.MDATE = System.DateTime.Now;
                            _svg.OrderID = ny * 100 + list.IndexOf(lar);
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
                for (int i = 0; i < linelist.Count; i++)
                {
                    LineInfo _line = new LineInfo();
                    _line.EleID = ((SvgElement)linelist[i]).ID;
                    _line.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                    _line = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", _line);
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


        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            try
            {
                fInfo.Close();
                if (tlVectorControl1.IsModified)
                {
                    if (MessageBox.Show("是否保存已做的修改?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
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
            dotNetBarManager1.Bars["mainmenu"].GetItem("ghwj").Enabled = false;
            propertyGrid.Enabled = true;
            symbolSelector.Enabled = true;
            bk1.Visible = false;
        }
        public void DlBarVisible(bool b)
        {
            dotNetBarManager1.Bars["bar6"].Enabled = b;
            dotNetBarManager1.Bars["bar6"].GetItem("mSave").Enabled = true;
            dotNetBarManager1.Bars["bar8"].Visible = false;
            dotNetBarManager1.Bars["bar7"].Visible = false;
            dotNetBarManager1.Bars["bar88"].Visible = false;
            //dotNetBarManager1.GetItem("ButtonItem7").Visible = false;
            //dotNetBarManager1.GetItem("ghwj").Visible = false;
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
            dotNetBarManager1.Bars["bar6"].GetItem("mSave").Enabled = true;
            dotNetBarManager1.Bars["bar8"].Visible = false;
            dotNetBarManager1.Bars["bar88"].Visible = false;
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

            dotNetBarManager1.Bars["bar7"].GetItem("mFhbz").Visible = false;
            dotNetBarManager1.Bars["bar7"].GetItem("mCJ").Visible = false;
            dotNetBarManager1.Bars["bar7"].GetItem("m_dhx").Visible = false;

            //dotNetBarManager1.Bars["bar7"].GetItem("mFhbz").Visible = true;
            //dotNetBarManager1.Bars["bar7"].GetItem("mFhbz").Enabled = true;

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
            dotNetBarManager1.GetItem("ButtonItem7").Visible = false;
           
   
            

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
            dotNetBarManager1.Bars["bar6"].GetItem("mSave").Enabled = true;
            dotNetBarManager1.Bars["bar7"].Enabled = b;
            dotNetBarManager1.GetItem("ButtonItem7").Enabled = b;
            dotNetBarManager1.GetItem("ghwj").Enabled = b;
            dotNetBarManager1.GetItem("m_ld").Enabled = b;
            dotNetBarManager1.GetItem("m_fz").Enabled = b;
            dotNetBarManager1.GetItem("m_bxz").Enabled = b;
            dotNetBarManager1.GetItem("ButtonJXT").Enabled = b;

            dotNetBarManager1.GetItem("ButtonItem7").Visible = true;
    
#if(!CITY)
            dotNetBarManager1.Bars["bar7"].GetItem("mAreaPoly").Visible = false;
#endif

            dotNetBarManager1.Bars["bar7"].GetItem("mLeadLine").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mJQLeadLine").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mFx").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mFzzj").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mAreaPoly").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mReCompute").Enabled = b;
            //dotNetBarManager1.Bars["bar7"].GetItem("mFhbz").Visible = false;
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

        public void DwBarVisible_SH(bool b)
        {
            dotNetBarManager1.Bars["bar7"].Visible = true;
            dotNetBarManager1.Bars["bar8"].Visible = false;
            
            //dotNetBarManager1.Bars["bar8"].GetItem("mEdit").Enabled = false;
            dotNetBarManager1.Bars["bar6"].Enabled = b;
            dotNetBarManager1.Bars["bar6"].GetItem("mSave").Enabled = true;
            dotNetBarManager1.Bars["bar7"].Enabled =true;

            dotNetBarManager1.GetItem("ButtonItem7").Enabled = true;
            dotNetBarManager1.GetItem("ButtonItem2").Visible = false;
            dotNetBarManager1.GetItem("ButtonItem5").Visible = false;
            dotNetBarManager1.GetItem("ButtonItem6").Visible = false;
            dotNetBarManager1.GetItem("mShapeTransform").Visible = false;
            dotNetBarManager1.GetItem("ghwj").Enabled = b;
            dotNetBarManager1.GetItem("m_ld").Enabled = b;


            dotNetBarManager1.GetItem("m_fz").Enabled = b;
            dotNetBarManager1.GetItem("m_bxz").Enabled = b;
            dotNetBarManager1.GetItem("ButtonJXT").Enabled = b;

            dotNetBarManager1.GetItem("ButtonItem7").Visible = true;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["mJQLeadLine"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["mLeadLine"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["mFx"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["mFzzj"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_cx"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_ld"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_subxz"].Visible = true;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_bxz"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_outxljwd"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_outsubjwd"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_fz"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["mReCompute"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_tp"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_reDraw"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_subColor"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["Chaoliujisuan"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["ORP"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["ghwj"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_djcl"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_inxljwd"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_inbdzjwd"].Visible = b;
            dotNetBarManager1.GetItem("ButtonItem7").SubItems["m_unsel"].Visible = b;
            

#if(!CITY)
            dotNetBarManager1.Bars["bar7"].GetItem("mAreaPoly").Visible = false;
#endif

            dotNetBarManager1.Bars["bar7"].GetItem("mLeadLine").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mJQLeadLine").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mFx").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mFzzj").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mAreaPoly").Enabled = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mReCompute").Enabled = b;
            //dotNetBarManager1.Bars["bar7"].GetItem("mFhbz").Visible = false;
            dotNetBarManager1.Bars["bar7"].GetItem("mFhbz").Visible = true;
            dotNetBarManager1.Bars["bar7"].GetItem("mFhbz").Enabled = true;
            dotNetBarManager1.Bars["bar7"].GetItem("mFhbz1").Visible = b;
            dotNetBarManager1.Bars["bar7"].GetItem("mCJ").Visible = b;

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
            string ss2 = "";
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
                //string ss= tlVectorControl1.SVGDocument.CurrentLayer.ID;
                //layerOutXml = layerOutXml.Replace(ss2,ss);
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
        private string getJWD(LongLat temp)
        {
            string JWD = "";
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
            ((MapViewBase)mapview).ZoneWide = 3;
            double[] s = ((MapViewBase)mapview).LBtoXY54(Convert.ToDouble(temp.Longitude), Convert.ToDouble(temp.Latitude));
            JWD = d1 + " " + f1 + " " + m1.ToString("##.#") + " " + d2 + " " + f2 + " " + m2.ToString("##.#") + " \r\n";
            return JWD;
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

        private void 城市地图ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 系统接线图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openAutojxt();
        }
        /// <summary>
        /// 定位接线图中的线路
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmloctaionjxt_Click(object sender, EventArgs e)
        {
            locationJxtXL();
        }

        private void 关联设备ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SvgElement element = tlVectorControl1.SVGDocument.CurrentElement;
            if (tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Line" || tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline")
            {
                string deviceID = element.GetAttribute("Deviceid");
                string con = "where  ProjectID = '" + Itop.Client.MIS.ProgUID + "' and Type in ('50','51','52','53','54','55','56','57','58','61','62','63','64','70','71','72','74','75') and AreaID = '" + deviceID + "' or IName = '" + deviceID + "' or JName = '" + deviceID + "'";
                IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                string[] devicetype = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    devicetype[i] = ((PSPDEV)list[i]).Type;
                }
                if (list.Count > 0)
                {
                    frmRelationDevice frd = new frmRelationDevice();
                    frd.DeviceList = list;
                    frd.InitDeviceType(devicetype);
                    frd.Show();
                }
                else
                {
                    MessageBox.Show("没有关联设备？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked == true)
            {
                XmlNodeList nn0 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1'] [@dhx_key='2']");
                for (int i = 0; i < nn0.Count; i++)
                {
                    XmlElement x = (XmlElement)nn0[i];
                    x.SetAttribute("visibility", "visible");
                }
                XmlNodeList nn1 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1'] [@dhx_key='1']");
                for (int i = 0; i < nn1.Count; i++)
                {
                    Polyline p = ((Polyline)nn1[i]).Clone() as Polyline;

                    XmlNodeList _n = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1'] [@dhx_copy='" + p.ID + "']");
                    if (_n.Count > 0) continue;

                    PointF[] pn = p.Points;
                    p.Transform.Matrix.TransformPoints(pn);
                    double angA = TLMath.Angle(pn[1], pn[0], pn[2]);
                    double ang = TLMath.Angle(pn[1], new PointF(pn[1].X - 10, pn[1].Y), pn[2]);
                    double ang2 = TLMath.Angle(pn[1], new PointF(pn[1].X - 10, pn[1].Y), pn[0]);
                    double ango = (ang - ang2);
                    double C = Math.Abs(50 / (Math.Sin(ango / 2)));
                    double A = 50;
                    double B = Math.Abs(50 / (Math.Tan(ango / 2)));
                    //y3=(x3-x2)(y1-y2)/(x1-x2)+y2
                    double x2 = pn[1].X - B;
                    float fy = Convert.ToSingle((x2 - pn[1].X) * (pn[0].Y - pn[1].Y) / (pn[0].X - pn[1].X) + pn[1].Y);
                    PointF p2 = new PointF(Convert.ToSingle(x2), fy);
                    double X = 0;
                    double Y = 0;
                    if (ang2 * 57.3 > 180)
                    {
                        Y = Math.Sin((180 - ang2 * 180 / Math.PI - 180 - ango / 2 * 180 / Math.PI) * (Math.PI / 180)) * A;
                    }
                    else
                    {
                        Y = Math.Sin((180 - ang2 * 180 / Math.PI - ango / 2 * 180 / Math.PI) * (Math.PI / 180)) * A;
                    }
                    X = Math.Cos((180 - ang2 * 180 / Math.PI - ango / 2 * 180 / Math.PI) * (Math.PI / 180)) * A;
                    //double ang2 = TLMath.getLineAngle(new PointF(0,0), p2, pn[1]);
                    //double X = p2.X + A * Math.Cos((ang2 * 57.3 - 90) / 57.3);
                    //double Y = p2.Y + A * Math.Sin((ang2 * 57.3 - 90) / 57.3);

                    //PointF[] pnf = TLMath.getPoint3(pn[1],p2, A, B, C);
                    //PointF pp = TLMath.getPoint3(p2, pn[1], 90 *(Math.PI/180), ang / 2);

                    //XmlElement e1 = tlVectorControl1.SVGDocument.CreateElement("polyline") as XmlElement;
                    //e1.SetAttribute("points", "0 0,10 10,"+X.ToString()+" "+Y.ToString());
                    //e1.SetAttribute("style", "stroke-width:1;stroke:#0000FF;stroke-opacity:1;");
                    //e1.SetAttribute("IsTin", "1");
                    //e1.SetAttribute("layer", SvgDocument.currentLayer);
                    //tlVectorControl1.SVGDocument.RootElement.AppendChild(e1);


                    ItopVector.Core.Types.Transf transf = new ItopVector.Core.Types.Transf(p.Transform.Matrix);
                    double a = (ang2 + (angA / 2)) * 180 / Math.PI;
                    if (((ang2 + (angA / 2)) * 180 / Math.PI) < 180)
                    {
                        transf.setTranslate((float)X, -(float)Y);
                    }
                    else
                    {
                        transf.setTranslate((float)X, (float)Y);
                    }
                    p.Transform = transf;
                    p.SetAttribute("dhx_key", "2");
                    p.SetAttribute("dhx_copy", p.ID);
                    tlVectorControl1.SVGDocument.RootElement.AppendChild(p);
                }
                tlVectorControl1.Refresh();
            }
            if ((checkEdit2.Checked == false))
            {
                XmlNodeList nn1 = tlVectorControl1.SVGDocument.SelectNodes("svg/polyline [@IsLead='1'] [@dhx_key='2']");
                for (int i = 0; i < nn1.Count; i++)
                {
                    XmlElement x = (XmlElement)nn1[i];
                    x.SetAttribute("visibility", "hidden");
                }
                tlVectorControl1.Refresh();
            }
        }

        private void toolrelanalyst_Click(object sender, EventArgs e)
        {
           
        }
    }
}