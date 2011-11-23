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
using ItopVector.Core;
using ItopVector.Core.Func;
using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using ItopVector.Core.Interface.Figure;
using Itop.Domain.Stutistic;
using Itop.Client.Base;
using Itop.MapView;
using System.IO;



namespace ItopVector.Tools {

    public partial class frmMain2 : FormBase {
        #region 对象声明
        SVGFILE svg = new SVGFILE();
        SvgDocument sdoc = new SvgDocument();
        glebeProperty gPro = new glebeProperty();
        CtrlFileManager ctlfile = new CtrlFileManager();
        DevComponents.DotNetBar.ToolTip tip;

        XLTProcessor xltProcessor;
        public static string MapType = "接线图";
        static string ParentUID = "";
        public static string progtype = "";
        private ItopVector.Selector.SymbolSelector symbolSelector;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private DevComponents.DotNetBar.ComboBoxItem LayerBox;
        private DevComponents.DotNetBar.ComboBoxItem scaleBox;
        //private ButtonItem operationButton;
        private ButtonItem orderButton;
        private ButtonItem alignButton;
        private ButtonItem rotateButton;




        //frmLayerManager frmlar = new frmLayerManager();
        private string SVGUID = "";
        private string SelUseArea = "";
        private string LineLen = "";
        private string rzb = "1";
        private string selLar = "";
        private int LayerCount = 0;

        private bool LoadImage = true;
        public bool SubPrint = false;



        XmlNode img = null;
        frmInfo fInfo = new frmInfo();
        Itop.MapView.MapViewObj mapview = new Itop.MapView.MapViewObj();
        //public event OnCloseDocumenthandler OnCloseSvgDocument;
        #endregion

        public frmMain2() {

            propertyGrid = new PropertyGrid();
            tip = new DevComponents.DotNetBar.ToolTip();
            ItopVector.SpecialCursors.LoadCursors();
            InitializeComponent();
            tlVectorControl2.CanEdit = true;
            tlVectorControl2.DrawArea.FreeSelect = true;
            this.dotNetBarManager2.Images = ItopVector.Resource.ResourceHelper.LoadBitmapStrip(base.GetType(), "ItopVector.Tools.ToolbarImages.bmp", new Size(16, 16), new Point(0, 0));
            tlVectorControl2.LeftClick += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_LeftClick);
            //tlVectorControl1.RightClick += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_RightClick);
            tlVectorControl2.DoubleLeftClick += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_DoubleLeftClick);
            tlVectorControl2.MoveIn += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_MoveIn);
            tlVectorControl2.MoveOut += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_MoveOut);
            tlVectorControl2.OnTipEvent += new ItopVector.Core.Interface.OnTipEventHandler(tlVectorControl1_OnTipEvent);
            tlVectorControl2.ScaleChanged += new EventHandler(tlVectorControl1_ScaleChanged);
            tlVectorControl2.DragAndDrop += new DragEventHandler(tlVectorControl1_DragAndDrop);
            tlVectorControl2.DrawArea.ViewChanged += new ItopVector.DrawArea.ViewChangedEventHandler(DrawArea_ViewChanged);
            //tlVectorControl1.AfterPaintPage += new ItopVector.DrawArea.PaintMapEventHandler(tlVectorControl1_AfterPaintPage);
            //tlVectorControl1.DrawArea.OnBeforeRenderTo += new ItopVector.DrawArea.PaintMapEventHandler(DrawArea_OnBeforeRenderTo);
            LoadImage = false;
            Pen pen1 = new Pen(Brushes.Cyan, 3);
            tlVectorControl2.TempPen = pen1;
            tlVectorControl2.PropertyGrid = propertyGrid;
            tlVectorControl2.BackColor = Color.White;
            tlVectorControl2.OperationChanged += new EventHandler(tlVectorControl1_OperationChanged);
            //tlVectorControl2.FullDrawMode = true;
            tlVectorControl2.DrawMode = DrawModeType.MemoryImage;
            tlVectorControl2.DrawArea.ViewMargin = new Size(1000, 1000);
            //decimal jd = Convert.ToDecimal(ConfigurationSettings.AppSettings.Get("jd"));
            //decimal wd = Convert.ToDecimal(ConfigurationSettings.AppSettings.Get("wd"));
            //mapview.ZeroLongLat = new LongLat(117.6787m, 31.0568m);
            //mapview.ZeroLongLat = new LongLat(jd, wd);
            tlVectorControl2.CurrentOperation = ToolOperation.Select;
        }
        public frmMain2(IList svglist, string eleid)
        {
            propertyGrid = new PropertyGrid();
            tip = new DevComponents.DotNetBar.ToolTip();
            ItopVector.SpecialCursors.LoadCursors();
            InitializeComponent();
            tlVectorControl2.CanEdit = true;
            //tlVectorControl2.DrawArea.FreeSelect = true;
            this.dotNetBarManager2.Images = ItopVector.Resource.ResourceHelper.LoadBitmapStrip(base.GetType(), "ItopVector.Tools.ToolbarImages.bmp", new Size(16, 16), new Point(0, 0));
            tlVectorControl2.LeftClick += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_LeftClick);
            //tlVectorControl2.RightClick += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_RightClick);
            //tlVectorControl2.DoubleLeftClick += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_DoubleLeftClick);
            tlVectorControl2.MoveIn += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_MoveIn);
            tlVectorControl2.MoveOut += new ItopVector.DrawArea.SvgElementEventHandler(tlVectorControl1_MoveOut);
            tlVectorControl2.OnTipEvent += new ItopVector.Core.Interface.OnTipEventHandler(tlVectorControl1_OnTipEvent);
            tlVectorControl2.ScaleChanged += new EventHandler(tlVectorControl1_ScaleChanged);
            tlVectorControl2.DragAndDrop += new DragEventHandler(tlVectorControl1_DragAndDrop);
            tlVectorControl2.DrawArea.ViewChanged += new ItopVector.DrawArea.ViewChangedEventHandler(DrawArea_ViewChanged);
            //tlVectorControl2.AfterPaintPage += new ItopVector.DrawArea.PaintMapEventHandler(tlVectorControl1_AfterPaintPage);
            //tlVectorControl2.DrawArea.OnBeforeRenderTo += new ItopVector.DrawArea.PaintMapEventHandler(DrawArea_OnBeforeRenderTo);
            Pen pen1 = new Pen(Brushes.Cyan, 3);
            tlVectorControl2.TempPen = pen1;
            tlVectorControl2.PropertyGrid = propertyGrid;
            tlVectorControl2.BackColor = Color.White;
            tlVectorControl2.OperationChanged += new EventHandler(tlVectorControl1_OperationChanged);
            //tlVectorControl2.FullDrawMode = true;
            tlVectorControl2.DrawMode = DrawModeType.MemoryImage;
            tlVectorControl2.DrawArea.ViewMargin = new Size(10000, 10000);
            //decimal jd = Convert.ToDecimal(ConfigurationSettings.AppSettings.Get("jd"));
            //decimal wd = Convert.ToDecimal(ConfigurationSettings.AppSettings.Get("wd"));
            //mapview.ZeroLongLat = new LongLat(117.6787m, 31.0568m);
            //mapview.ZeroLongLat = new LongLat(jd, wd);
            tlVectorControl2.CurrentOperation = ToolOperation.Select;
            LoadImage = false;
           
            //SVGFILE svg_temp = new SVGFILE();
            //if (svglist.Count > 0)
            //{
            //    svg_temp = (SVGFILE)svglist[0];
            //    sdoc = null;
            //    sdoc = new SvgDocument();
            //    sdoc.LoadXml(svg_temp.SVGDATA);                
            //    tlVectorControl2.SVGDocument = sdoc;
            //    tlVectorControl2.SVGDocument.SvgdataUid = svg_temp.SUID;
            //}
        }       
        void tlVectorControl1_OperationChanged(object sender, EventArgs e) {
            if (csOperation == CustomOperation.OP_MeasureDistance) {
                resetOperation();
            } 
        }

        void symbolSelector_SelectedChanged(object sender, EventArgs e) {
            tlVectorControl2.CurrentOperation = ToolOperation.Select;
            if (symbolSelector.SelectedItem != null) {
                tlVectorControl2.DrawArea.PreGraph = symbolSelector.SelectedItem.CloneNode(true) as IGraph;
            } else {
                tlVectorControl2.DrawArea.PreGraph = null;
            }
        }

        void DrawArea_OnBeforeRenderTo(object sender, PaintMapEventArgs e) {
            PointF pf = tlVectorControl2.DrawArea.ContentBounds.Location;
            Rectangle rt = Rectangle.Round(e.G.VisibleClipBounds);
            int offsetX = (int)(rt.Width / 2 + pf.X);
            int offsetY = (int)(rt.Height / 2 + pf.Y);
            LongLat longlat = mapview.OffSet(mapview.ZeroLongLat, mapview.Getlevel(1), -offsetX, -offsetY);
            e.G.Clear(ColorTranslator.FromHtml("#EBEAE8"));
            ImageAttributes imageA=new ImageAttributes();
            mapview.Paint(e.G, (int)rt.Width, (int)rt.Height, mapview.Getlevel(1), longlat.Longitude, longlat.Latitude, imageA);
        }

        void tlVectorControl1_AfterPaintPage(object sender, PaintMapEventArgs e) {
            try {
                if (LoadImage) {
                    int nScale = 0;
                    switch ((int)(decimal)(this.tlVectorControl2.DrawArea.ScaleUnit * 1000)) {
                        case 10:
                            nScale = 5;
                            break;
                        case 20:
                            nScale = 6;
                            break;
                        case 40:
                            nScale = 7;
                            break;
                        case 100:
                            nScale = 8;
                            break;
                        case 200:
                            nScale = 9;
                            break;
                        case 400:
                            nScale = 10;
                            break;
                        case 1000:
                            nScale = 11;
                            break;
                        case 2000:
                            nScale = 12;
                            break;
                        case 4000:
                            nScale = 13;
                            break;
                        default:
                            return;
                    }
                    LongLat longlat = LongLat.Empty;
                    //计算中心点经纬度

                    
                    longlat = mapview.OffSet(mapview.ZeroLongLat, nScale, (int)(e.CenterPoint.X), (int)(e.CenterPoint.Y));

                    //创建地图
                    System.Drawing.Image image = mapview.CreateMap(e.Bounds.Width, e.Bounds.Height, nScale, longlat.Longitude, longlat.Latitude);

                    ImageAttributes imageAttributes = new ImageAttributes();
                    ColorMatrix matrix1 = new ColorMatrix();
                    matrix1.Matrix00 = 1f;
                    matrix1.Matrix11 = 1f;
                    matrix1.Matrix22 = 1f;
                    matrix1.Matrix33 = 1f;//地图透明度
                    matrix1.Matrix44 = 1f;
                    //设置地图透明度
                    imageAttributes.SetColorMatrix(matrix1, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

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
                    if (tlVectorControl2.CurrentOperation != ToolOperation.Roam) {
                        e.G.DrawLines(new Pen(Color.Black, 2), new Point[4] { p1, p2, p3, p4 });
                        string str1 = string.Format("{0}公里", mapview.GetMiles(nScale));
                        e.G.DrawString(str1, new Font("宋体", 10), Brushes.Black, 30, e.Bounds.Height - 40);
                    }
                }
            } catch (Exception e1) { }
            //string s = string.Format("{0}行{1}列", nRows, nCols);
            //string s = string.Format("经{0}：纬{1}", longlat.Longitude, longlat.Latitude);
            //显示中心点经纬度
            // e.G.DrawString(s, new Font("宋体", 10), Brushes.Red, 20, 40);
        }

        void DrawArea_ViewChanged(object sender, ItopVector.DrawArea.ViewChangedEventArgs e) {
            //throw new Exception("The method or operation is not implemented.");
            //float a = e.Bounds.Bottom;
        }

        void tlVectorControl1_DragAndDrop(object sender, DragEventArgs e) {
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
        void tlVectorControl1_RightClick(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e) {
        //    try {
        //        if (csOperation == CustomOperation.OP_MeasureDistance) {
        //            tlVectorControl1.Operation = ToolOperation.Select;
        //            //contextMenuStrip1.Hide();
        //            return;
        //        }
        //        //MessageBox.Show(MapType);
        //        //tmLineConnect.Visible = false;
        //        SvgElementCollection elements = tlVectorControl1.SVGDocument.SelectCollection;
        //        if (elements.Count == 2) {
        //            Polyline pl1 = elements[0] as Polyline;
        //            Polyline pl2 = elements[1] as Polyline;
        //            //if (pl1 != null && pl2 != null && pl1.GetAttribute("IsLead") != ""  && pl2.GetAttribute("IsLead") != "") {
                        
        //            //    //tmLineConnect.Visible = true;
        //            //}
        //        }
        //        if (MapType == "接线图") {
        //            tip.Hide();
        //            //if (getlayer(SvgDocument.currentLayer, "背景层", tlVectorControl1.SVGDocument.getLayerList())) {
        //            //    contextMenuStrip1.Enabled = false;
        //            //} else {
        //            //    contextMenuStrip1.Enabled = true;
        //            //}
                    
        //            if (tlVectorControl1.SVGDocument.CurrentElement == null ||
        //               tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() != "ItopVector.Core.Figure.Use") {
        //                //moveMenuItem.Visible = false;
        //                //jxtToolStripMenuItem.Visible = false;
        //            } else {
        //                if (tlVectorControl1.SVGDocument.CurrentElement.GetAttribute("xlink:href").Contains("Substation")) {
        //                    //moveMenuItem.Visible = true;
        //                    //jxtToolStripMenuItem.Visible = true;
        //                }
        //            }
        //            if (tlVectorControl1.SVGDocument.CurrentElement == null ||
        //              tlVectorControl1.SVGDocument.CurrentElement.GetType().ToString() != "ItopVector.Core.Figure.Polygon") {
        //                ////printToolStripMenuItem.Visible = false;
        //                //SubToolStripMenuItem.Visible = false;
        //            } else {
        //                //printToolStripMenuItem.Visible = true;
        //                //SubToolStripMenuItem.Visible = true;

        //            }
        //            string guid = Guid.NewGuid().ToString();
        //            if (tlVectorControl1.Operation == ToolOperation.InterEnclosure && !SubPrint) {
        //                System.Collections.SortedList list = new SortedList();
        //                decimal s = 0;
        //                ItopVector.Core.SvgElementCollection selCol = tlVectorControl1.SVGDocument.SelectCollection;
        //                if (selCol.Count > 1) {
        //                    decimal ViewScale = 1;
        //                    string str_Scale = tlVectorControl1.SVGDocument.getViewScale();
        //                    if (str_Scale != "") {
        //                        ViewScale = Convert.ToDecimal(str_Scale);
        //                    }
        //                    string str_remark = "";
        //                    string str_selArea = "";
        //                    //string Elements = "";
        //                    Hashtable SelAreaCol = new Hashtable();
        //                    this.Cursor = Cursors.WaitCursor;
        //                    XmlElement poly1 = (XmlElement)selCol[selCol.Count - 1];

        //                    frmWaiting wait = new frmWaiting();

        //                    wait.Show(this);
        //                    wait.Refresh();

        //                    GraphicsPath gr1 = new GraphicsPath();
        //                    gr1.AddPolygon(TLMath.getPolygonPoints(poly1));
        //                    gr1.CloseFigure();

        //                    for (int i = 0; i < selCol.Count - 1; i++) {
        //                        if (selCol[i].GetType().FullName == "ItopVector.Core.Figure.Polygon") {

        //                            string IsArea = ((XmlElement)selCol[i]).GetAttribute("IsArea");
        //                            if (IsArea != "") {
        //                                XmlElement polyn = (XmlElement)selCol[i];
        //                                GraphicsPath gr2 = new GraphicsPath();
        //                                gr2.AddPolygon(TLMath.getPolygonPoints(polyn));
        //                                gr2.CloseFigure();
        //                                Region region = new Region(gr1);
        //                                region.Intersect(gr2);

        //                                RectangleF rect = new RectangleF();
        //                                rect = tlVectorControl1.SelectedRectangle(region);

        //                                decimal sub_s = TLMath.getInterPolygonArea(region, rect, ViewScale);
        //                                sub_s = TLMath.getNumber2(sub_s, tlVectorControl1.ScaleRatio);
        //                                SelAreaCol.Add(polyn.GetAttribute("id"), sub_s);
        //                                glebeProperty _gleProp = new glebeProperty();
        //                                _gleProp.EleID = polyn.GetAttribute("id");
        //                                _gleProp.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
        //                                IList gList = Services.BaseService.GetList("SelectglebePropertyByEleID", _gleProp);

        //                                if (gList.Count > 0) {
        //                                    _gleProp = (glebeProperty)gList[0];
        //                                    list.Add(_gleProp.UseID, sub_s.ToString("#####.####"));
        //                                    str_selArea = str_selArea + _gleProp.EleID + "," + sub_s.ToString("#####.####") + ";";
        //                                    //str_remark = str_remark + "地块" + _gleProp.UseID + "选中面积为：" + sub_s.ToString() + "\r\n";
        //                                    s += sub_s;
        //                                }
        //                            }
        //                        }
        //                        if (selCol[i].GetType().FullName == "ItopVector.Core.Figure.Use") {
        //                            XmlElement e1 = (XmlElement)selCol[i];
        //                            string str_id = e1.GetAttribute("id");

        //                            substation _sub1 = new substation();
        //                            _sub1.EleID = str_id;
        //                            _sub1.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
        //                            _sub1 = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _sub1);
        //                            if (_sub1 != null) {
        //                                _sub1.glebeEleID = guid;
        //                                Services.BaseService.Update("Updatesubstation", _sub1);
        //                            }

        //                        }

        //                    }
        //                    decimal nullpoly = TLMath.getNumber2(TLMath.getPolygonArea(TLMath.getPolygonPoints(poly1), 1), tlVectorControl1.ScaleRatio) - s;

        //                    for (int j = 0; j < list.Count; j++) {
        //                        if (Convert.ToString(list.GetByIndex(j)) != "") {
        //                            if (Convert.ToDecimal(list.GetByIndex(j)) < 1) {
        //                                str_remark = str_remark + "地块" + list.GetKey(j).ToString() + "选中面积为：" + "0" + list.GetByIndex(j).ToString() + "（KM²）\r\n";
        //                            } else {
        //                                str_remark = str_remark + "地块" + list.GetKey(j).ToString() + "选中面积为：" + list.GetByIndex(j).ToString() + "（KM²）\r\n";
        //                            }
        //                        }
        //                    }
        //                    XmlElement x1 = (XmlElement)selCol[selCol.Count - 1];

        //                    gPro.UID = guid;
        //                    gPro.EleID = x1.GetAttribute("id");
        //                    gPro.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
        //                    gPro.ParentEleID = "0";
        //                    if (s != 0) {
        //                        gPro.Area = Convert.ToDecimal(s.ToString("#####.####"));
        //                    } else {
        //                        gPro.Area = 0;
        //                    }
        //                    gPro.SelSonArea = str_selArea;

        //                    if (nullpoly < 1) {
        //                        gPro.ObligateField10 = "0" + nullpoly.ToString("#####.####");
        //                    } else {
        //                        gPro.ObligateField10 = nullpoly.ToString("#####.####");
        //                    }

        //                    str_remark = str_remark + "\r\n 空白区域面积" + gPro.ObligateField10 + "（KM²）\r\n";
        //                    if (str_remark != "") {
        //                        str_remark = str_remark.Substring(0, str_remark.Length - 2);
        //                    }

        //                    gPro.Remark = str_remark;
        //                    wait.Close();
        //                    this.Cursor = Cursors.Default;
        //                    if (s < 1) {
        //                        MessageBox.Show("选中区域面积:" + "0" + s.ToString("#####.####") + "（KM²）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    } else {
        //                        MessageBox.Show("选中区域面积:" + s.ToString("#####.####") + "（KM²）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    }


        //                    Services.BaseService.Create<glebeProperty>(gPro);

        //                    IDictionaryEnumerator ISelList = SelAreaCol.GetEnumerator();
        //                    while (ISelList.MoveNext()) {
        //                        glebeProperty sub_gle = new glebeProperty();
        //                        sub_gle.EleID = ISelList.Key.ToString();
        //                        sub_gle.ParentEleID = gPro.EleID;
        //                        sub_gle.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
        //                        Services.BaseService.Update("UpdateglebePropertySelArea", sub_gle);
        //                    }

        //                    tlVectorControl1.SVGDocument.SelectCollection.Clear();
        //                    tlVectorControl1.SVGDocument.CurrentElement = (SvgElement)x1;
        //                }
        //                SubPrint = false;
        //            }
        //            if (tlVectorControl1.CurrentOperation == ToolOperation.InterEnclosure && SubPrint) {
        //                //ItopVector.Core.SvgElementCollection selCol = tlVectorControl1.SVGDocument.SelectCollection;
        //                //if(selCol.Count>2){
        //                //    XmlElement selArea = (SvgElement)selCol[selCol.Count - 1];

        //                //    GraphicsPath gr1 = new GraphicsPath();
        //                //    gr1.AddPolygon(TLMath.getPolygonPoints(selArea));
        //                //    gr1.CloseFigure();
        //                //    RectangleF rect= gr1.GetBounds();

        //                //    SvgDocument _doc = new SvgDocument();
        //                //    string svgtxt = "<?xml version=\"1.0\" encoding=\"utf-8\"?><svg id=\"svg\" width=\""+rect.Width+"\" height=\""+rect.Height+"\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:itop=\"http://www.Itop.com/itop\">";

        //                //    for (int n = 0; n < selCol.Count-1;n++ )
        //                //    {
        //                //        //_doc.AppendChild((XmlNode)selCol[n]);
        //                //        svgtxt=svgtxt+((XmlElement)selCol[n]).OuterXml+"\r\n";
        //                //    }
        //                //    svgtxt = svgtxt + "</svg>";
        //                //    _doc.LoadXml(svgtxt);
        //                //    frmSubPrint s = new frmSubPrint();
        //                //    s.Show();
        //                //    s.Open(_doc, rect);
        //                ItopVector.Core.SvgElementCollection selCol = tlVectorControl1.SVGDocument.SelectCollection;
        //                XmlElement x1 = (XmlElement)selCol[selCol.Count - 1];
        //                tlVectorControl1.SVGDocument.SelectCollection.Clear();
        //                tlVectorControl1.SVGDocument.CurrentElement = (SvgElement)x1;
        //                SubPrint = false;
        //                //}
        //            }
        //            if (tlVectorControl1.Operation == ToolOperation.Enclosure) {

        //                string Elements = "";
        //                ItopVector.Core.SvgElementCollection selCol = tlVectorControl1.SVGDocument.SelectCollection;

        //                for (int i = 0; i < selCol.Count - 1; i++) {
        //                    XmlElement e1 = (XmlElement)selCol[i];
        //                    Elements = Elements + "'" + e1.GetAttribute("id") + "',";
        //                }
        //                if (Elements.Length > 0) {
        //                    Elements = Elements.Substring(0, Elements.Length - 1);
        //                }
        //                XmlElement x1 = (XmlElement)selCol[selCol.Count - 1];

        //                gPro.UID = Guid.NewGuid().ToString();
        //                gPro.EleID = x1.GetAttribute("id");

        //                gPro.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
        //                gPro.SonUid = Elements;
        //                Services.BaseService.Create<glebeProperty>(gPro);

        //                tlVectorControl1.SVGDocument.SelectCollection.Clear();
        //                tlVectorControl1.SVGDocument.CurrentElement = (SvgElement)x1;


        //            }
        //            if (tlVectorControl1.CurrentOperation == ToolOperation.LeadLine) {
        //                sgt1.Visible = false;
        //            }
        //        }
        //    } catch (Exception e1) {
        //        MessageBox.Show(e1.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        tlVectorControl1.SVGDocument.SelectCollection.Clear();
        //    } finally {
        //        tlVectorControl1.Operation = ToolOperation.Select;
        //        tlVectorControl1.Operation = ToolOperation.FreeTransform;

        //    }
        }

        void tlVectorControl1_MoveOut(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e) {
            //fInfo.Hide();
            //tip.Hide();
            //tlVectorControl1.Refresh();
        }

        void tlVectorControl1_MoveIn(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e) {
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

        void tlVectorControl1_DoubleLeftClick(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e) {
            XmlElement xml1 = ((XmlElement)(e.Elements[0]));
            string str_name = xml1.GetAttribute("xlink:href");
            if (MapType == "接线图") {

                if (str_name.Contains("Substation")) {
                    string infoname = xml1.GetAttribute("info-name");
                    ParentUID = tlVectorControl2.SVGDocument.SvgdataUid;
                    Save();
                    ParentUID = tlVectorControl2.SVGDocument.SvgdataUid;
                    SVGFILE svg_temp = new SVGFILE();
                    svg_temp.SUID = xml1.GetAttribute("id");
                    svg_temp.FILENAME = infoname;
                    IList svglist = Services.BaseService.GetList("SelectSVGFILEByKey", svg_temp);
                    OpenJXT(svglist, svg_temp);
                    //this.Text = infoname;
                    //tlVectorControl1.SVGDocument.FileName = infoname;
                    //frmlar.SymbolDoc = tlVectorControl2.SVGDocument;
                    //frmlar.Progtype = MapType;
                    //frmlar.InitData();
                    //JxtBar();
                }

            }
        }
        /// <summary>
        /// 测距
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tlVectorControl1_LeftClick2(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e) {
            
        }
        CustomOperation csOperation= CustomOperation.OP_Default;

        void resetOperation() {
            csOperation = CustomOperation.OP_Default;
            ItopVector.Core.Figure.Polyline obj = (ItopVector.Core.Figure.Polyline)tlVectorControl2.SVGDocument.CurrentElement;
            if (obj != null) {
                obj.ParentNode.RemoveChild(obj);
                tlVectorControl2.SVGDocument.CurrentElement = tlVectorControl2.SVGDocument.RootElement;
                label1.Hide();
                //tlVectorControl1.SetToolTip("");
            }
        }
        void tlVectorControl1_LeftClick(object sender, ItopVector.DrawArea.SvgElementSelectedEventArgs e) {
            if (csOperation == CustomOperation.OP_MeasureDistance) {
                Polyline pl = tlVectorControl2.SVGDocument.CurrentElement as Polyline;
                if (pl.Points.Length > 1) {
                    double d = 0;
                    for (int i = 1; i < pl.Points.Length; i++) {
                        d += this.mapview.CountLength(pl.Points[i - 1], pl.Points[i]);
                    }
                    label1.Text = d + "公里";

                    Point pt = new Point(e.Mouse.X, e.Mouse.Y);
                    pt = PointToClient(tlVectorControl2.PointToScreen(pt));
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
            if (tlVectorControl2.Operation == ToolOperation.Text) {
                frmTextInput ft = new frmTextInput();
                if (ft.ShowDialog() == DialogResult.OK) {
                    string txt = ft.Content;
                    XmlElement n1 = tlVectorControl2.SVGDocument.CreateElement("text") as Text;
                    Point point1 = tlVectorControl2.PointToView(new Point(e.Mouse.X, e.Mouse.Y));
                    n1.SetAttribute("x", point1.X.ToString());
                    n1.SetAttribute("y", point1.Y.ToString());
                    n1.InnerText = txt;
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    tlVectorControl2.SVGDocument.RootElement.AppendChild(n1);
                    tlVectorControl2.Operation = ToolOperation.Select;
                }
            }
            if (MapType == "规划统计") {
                try {
                    if (e.SvgElement.ID == "svg") {
                        MapType = "接线图";
                        return;
                    }
                    if(((Polygon)e.SvgElement )==null)return;
                    XmlElement n1 = tlVectorControl2.SVGDocument.CreateElement("circle") as Circle;
                    Point point1 = tlVectorControl2.PointToView(new Point(e.Mouse.X, e.Mouse.Y));
                    n1.SetAttribute("cx", point1.X.ToString());
                    n1.SetAttribute("cy", point1.Y.ToString());
                    n1.SetAttribute("r", "42.5");
                    n1.SetAttribute("r", "42.5");
                    //n1.SetAttribute("layer", getlayer("供电区域层", tlVectorControl1.SVGDocument.getLayerList()).ID);
                    n1.SetAttribute("layer", SvgDocument.currentLayer);
                    n1.SetAttribute("style", "fill:#FFFFC0;fill-opacity:0.5;stroke:#000000;stroke-opacity:1;");
                    tlVectorControl2.SVGDocument.RootElement.AppendChild(n1);

                    frmMainProperty fmain = new frmMainProperty();
                    glebeProperty gp = new glebeProperty();
                    gp.EleID = ((XmlElement)e.SvgElement).GetAttribute("id");
                    gp.SvgUID = tlVectorControl2.SVGDocument.SvgdataUid;
                    //fmain.InitData(gp); ///////////////////////////////////

                    XmlNodeList nlist = tlVectorControl2.SVGDocument.GetElementsByTagName("use");

                    PointF[] tfArray1 = TLMath.getPolygonPoints((XmlElement)e.SvgElement);
                    string str220 = "";
                    string str110 = "";
                    string str66 = "";
                    GraphicsPath selectAreaPath = new GraphicsPath();
                    selectAreaPath.AddLines(tfArray1);
                    selectAreaPath.CloseFigure();
                    //Matrix x=new Matrix(
                    //Region region1 = new Region(selectAreaPath);
                    for (int i = 0; i < nlist.Count; i++) {
                        float OffX = 0f;
                        float OffY = 0f;
                        Use use = (Use)nlist[i];
                        if (use.GetAttribute("xlink:href").Contains("Substation")) {
                            string strMatrix = use.GetAttribute("transform");
                            if (strMatrix != "") {
                                strMatrix = strMatrix.Replace("matrix(", "");
                                strMatrix = strMatrix.Replace(")", "");
                                string[] mat = strMatrix.Split(',');
                                if (mat.Length > 5) {
                                    OffX = Convert.ToSingle(mat[4]);
                                    OffY = Convert.ToSingle(mat[5]);
                                }
                            }
                            PointF TempPoint = TLMath.getUseOffset(use.GetAttribute("xlink:href"));
                            if (selectAreaPath.IsVisible(use.X + TempPoint.X + OffX, use.Y + TempPoint.Y + OffY)) {
                                if (use.GetAttribute("xlink:href").Contains("220")) {
                                    str220 = str220 + "'" + use.GetAttribute("id") + "',";
                                }
                                if (use.GetAttribute("xlink:href").Contains("110")) {
                                    str110 = str110 + "'" + use.GetAttribute("id") + "',";
                                }
                                if (use.GetAttribute("xlink:href").Contains("66"))
                                {
                                    str66 = str66 + "'" + use.GetAttribute("id") + "',";
                                }
                            }
                        }
                    }
                    if (str220.Length > 1) {
                        str220 = str220.Substring(0, str220.Length - 1);
                    }
                    if (str110.Length > 1) {
                        str110 = str110.Substring(0, str110.Length - 1);
                    }
                    if (str66.Length > 1)
                    {
                        str66 = str66.Substring(0, str66.Length - 1);
                    }

                    fmain.InitData(gp, str220, str110,str66);

                    XmlElement t1 = tlVectorControl2.SVGDocument.CreateElement("text") as Text;
                    Point point2 = tlVectorControl2.PointToView(new Point(e.Mouse.X, e.Mouse.Y));
                    t1.SetAttribute("x", Convert.ToString(point2.X - 20));
                    t1.SetAttribute("y", Convert.ToString(point2.Y - 10));
                    // t1.SetAttribute("layer", getlayer("供电区域层", tlVectorControl1.SVGDocument.getLayerList()).ID);
                    t1.SetAttribute("layer", SvgDocument.currentLayer);
                    t1.SetAttribute("style", "fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;");
                    t1.SetAttribute("font-famliy", "宋体");
                    t1.SetAttribute("font-size", "14");
                    t1.InnerText = fmain.glebeProp.Area + "\r\n" + fmain.glebeProp.Burthen; //+"\r\n" + fmain.glebeProp.Number;
                    tlVectorControl2.SVGDocument.RootElement.AppendChild(t1);
                    tlVectorControl2.Refresh();
                    fmain.Dispose();
                    MapType = "接线图";
                } catch (Exception e2) { MapType = "接线图"; }
            }
            if (tlVectorControl2.SVGDocument.SelectCollection.Count > 1) {
                return;
            }
            decimal ViewScale = 1;
            string str_Scale = tlVectorControl2.SVGDocument.getViewScale();
            if (str_Scale != "") {
                ViewScale = Convert.ToDecimal(str_Scale);
            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Polygon") {
                string IsArea = ((XmlElement)e.SvgElement).GetAttribute("IsArea");
                if (IsArea != "") {

                    XmlElement n1 = (XmlElement)e.SvgElement;
                    string str_points = n1.GetAttribute("points");
                    string[] points = str_points.Split(",".ToCharArray());
                    PointF[] pnts = new PointF[points.Length];

                    for (int i = 0; i < points.Length; i++) {
                        string[] pointsXY = points[i].Split(" ".ToCharArray());
                        pnts[i].X = Convert.ToSingle(pointsXY[0]);
                        pnts[i].Y = Convert.ToSingle(pointsXY[1]);
                    }
                    decimal temp1 = TLMath.getPolygonArea(pnts, 1);

                    temp1 = TLMath.getNumber2(temp1, tlVectorControl2.ScaleRatio);
                    SelUseArea = temp1.ToString("#####.####");
                    //tip.Text = "区域面积：" + SelUseArea;
                    if (SelUseArea != "") {
                        if (Convert.ToDecimal(SelUseArea) >= 1) {
                            fInfo.Info = "区域面积：" + SelUseArea + "（KM²）";
                        } else {
                            fInfo.Info = "区域面积： 0" + SelUseArea + "（KM²）";
                        }
                    }
                    fInfo.Top = e.Mouse.Y;
                    fInfo.Left = e.Mouse.X;
                    if (SelUseArea != "") {
                        fInfo.Show();

                    }
                    //tip.ShowToolTip();
                }

            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Line") {
                string IsLead = ((XmlElement)e.SvgElement).GetAttribute("IsLead");
                if (IsLead != "") {
                    Line line = (Line)e.SvgElement;
                    decimal temp1 = TLMath.getLineLength(line, 1);
                    temp1 = TLMath.getNumber(temp1, tlVectorControl2.ScaleRatio);
                    string len = temp1.ToString("#####.####");
                    LineLen = len;
                    if (len != "") {
                        if (Convert.ToDecimal(len) >= 1) {
                            fInfo.Info = "线路长度：" + len + "（KM）";
                        } else {
                            fInfo.Info = "线路长度： 0" + len + "（KM）";
                        }
                    }
                    fInfo.Top = e.Mouse.Y;
                    fInfo.Left = e.Mouse.X;
                    if (len != "") {
                        fInfo.Show();

                    }

                }
            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline") {
                string IsLead = ((XmlElement)e.SvgElement).GetAttribute("IsLead");
                if (IsLead != "") {
                    Polyline polyline = (Polyline)e.SvgElement;
                    decimal temp1 = TLMath.getPolylineLength(polyline, 1);
                    temp1 = TLMath.getNumber(temp1, tlVectorControl2.ScaleRatio);
                    string len = temp1.ToString("#####.####");
                    LineLen = len;
                    if (len != "") {
                        if (Convert.ToDecimal(len) >= 1) {
                            fInfo.Info = "线路长度：" + len + "（KM）";
                        } else {
                            fInfo.Info = "线路长度： 0" + len + "（KM）";
                        }
                    }
                    fInfo.Top = e.Mouse.Y;
                    fInfo.Left = e.Mouse.X;
                    if (len != "") {
                        fInfo.Show();
                    }

                }
            }
            if (e.SvgElement.GetType().ToString() == "ItopVector.Core.Figure.ConnectLine") {
                ConnectLine cline = (ConnectLine)tlVectorControl2.SVGDocument.CurrentElement;
                if (cline.StartGraph != null) {
                    string code = ((XmlElement)cline.StartGraph).GetAttribute("devxldm");

                    if (code != "") {
                        xltProcessor.SelectLine(code);
                        tlVectorControl2.CurrentOperation = ToolOperation.Select;
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

        void tlVectorControl1_ScaleChanged(object sender, EventArgs e) {
            string text1 = (((ItopVector.ItopVectorControl)sender).ScaleRatio * 100) + "%";
            scaleBox.ComboBoxEx.SelectedIndexChanged -= new EventHandler(ComboBoxScaleBox_SelectedIndexChanged);
            this.scaleBox.ComboBoxEx.Text = text1;
            scaleBox.ComboBoxEx.SelectedIndexChanged += new EventHandler(ComboBoxScaleBox_SelectedIndexChanged);
            if (tlVectorControl2.ScaleRatio < 0.006f) {
                tlVectorControl2.ScaleRatio = 0.006f;
                scaleBox.ComboBoxEx.Text = "0.6%";
                //scaleBox.SelectedText = "10%";
            }
        }

        void tlVectorControl1_OnTipEvent(object sender, string tooltip, byte TipType) {
            string[] tip ={ "" };
            if (TipType == 0) {
                LabelItem item = this.dotNetBarManager2.GetItem("plCoord") as LabelItem;
                if (item != null)
                    item.Text = "坐标 " + tooltip;
                LabelItem jwditem = this.dotNetBarManager2.GetItem("jwd") as LabelItem;
                if (jwditem != null)

                    tip = tooltip.Split(',');
                //if (tip.Length > 1) {
                //    try {
                //        LongLat temp = mapview.OffSetZero(-(int)(Convert.ToInt32(tip[0]) * tlVectorControl2.ScaleRatio), -(int)(Convert.ToInt32(tip[1]) * tlVectorControl2.ScaleRatio));

                //        string[] jd = temp.Longitude.ToString("####.####").Split('.');
                //        int d1 = Convert.ToInt32(jd[0]);
                //        string[] df1 = Convert.ToString(Convert.ToDecimal("0." + jd[1]) * 60).Split('.');
                //        int f1 = Convert.ToInt32(df1[0]);
                //        decimal m1 = Convert.ToDecimal("0." + df1[1]) * 60;

                //        string[] wd = temp.Latitude.ToString("####.####").Split('.');
                //        int d2 = Convert.ToInt32(wd[0]);
                //        string[] df2 = Convert.ToString(Convert.ToDecimal("0." + wd[1]) * 60).Split('.');
                //        int f2 = Convert.ToInt32(df2[0]);
                //        decimal m2 = Convert.ToDecimal("0." + df2[1]) * 60;
                //        jwditem.Text = "经纬度: " + d1.ToString() + "°" + f1.ToString() + "′" + m1.ToString("##.#") + "″," + d2.ToString() + "°" + f2.ToString() + "′" + m2.ToString("##.#") + "″";
                //    } catch { }
                //}
            } else if (TipType == 1) {
                LabelItem item = (LabelItem)this.dotNetBarManager2.GetItem("plTip") ;
                if (item != null)
                    item.Text = tooltip;
            } else if (TipType == 2) {
                LabelItem item = (LabelItem)this.dotNetBarManager2.GetItem("plColumn") ;
                if (item != null)
                    item.Text = tooltip;
            }
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
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            try {
                if (e.ClickedItem.Text == "属性") {
                    XmlElement xml1 = (XmlElement)tlVectorControl2.SVGDocument.CurrentElement;
                    //PointF[] pf = TLMath.getPolygonPoints(xml1);

                    //((Polygon)xml1).Transform.Matrix.TransformPoints(pf);
                    // 规划
                    if (getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl2.SVGDocument.getLayerList())) {



                        if (xml1 == null || tlVectorControl2.SVGDocument.CurrentElement.ID == "svg") {
                            MessageBox.Show("请先选择规划区域。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (tlVectorControl2.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polygon") {
                            XmlNodeList n1 = tlVectorControl2.SVGDocument.GetElementsByTagName("use");
                            PointF[] tfArray1 = TLMath.getPolygonPoints(xml1);
                            string str220 = "";
                            string str110 = "";
                            string str66 = "";
                            GraphicsPath selectAreaPath = new GraphicsPath();
                            selectAreaPath.AddLines(tfArray1);
                            selectAreaPath.CloseFigure();
                            //Matrix x=new Matrix(
                            //Region region1 = new Region(selectAreaPath);
                            for (int i = 0; i < n1.Count; i++) {
                                float OffX = 0f;
                                float OffY = 0f;
                                Use use = (Use)n1[i];
                                if (use.GetAttribute("xlink:href").Contains("Substation")) {
                                    string strMatrix = use.GetAttribute("transform");
                                    if (strMatrix != "") {
                                        strMatrix = strMatrix.Replace("matrix(", "");
                                        strMatrix = strMatrix.Replace(")", "");
                                        string[] mat = strMatrix.Split(',');
                                        if (mat.Length > 5) {
                                            OffX = Convert.ToSingle(mat[4]);
                                            OffY = Convert.ToSingle(mat[5]);
                                        }
                                    }
                                    PointF TempPoint = TLMath.getUseOffset(use.GetAttribute("xlink:href"));
                                    if (selectAreaPath.IsVisible(use.X + TempPoint.X + OffX, use.Y + TempPoint.Y + OffY)) {
                                        if (use.GetAttribute("xlink:href").Contains("220")) {
                                            str220 = str220 + "'" + use.GetAttribute("id") + "',";
                                        }
                                        if (use.GetAttribute("xlink:href").Contains("110")) {
                                            str110 = str110 + "'" + use.GetAttribute("id") + "',";
                                        }
                                        if (use.GetAttribute("xlink:href").Contains("66"))
                                        {
                                            str66 = str66 + "'" + use.GetAttribute("id") + "',";
                                        }
                                    }
                                }
                            }
                            if (str220.Length > 1) {
                                str220 = str220.Substring(0, str220.Length - 1);
                            }
                            if (str110.Length > 1) {
                                str110 = str110.Substring(0, str110.Length - 1);
                            }
                            if (str66.Length > 1)
                            {
                                str66 = str66.Substring(0, str66.Length - 1);
                            }

                            glebeProperty _gle = new glebeProperty();
                            _gle.EleID = tlVectorControl2.SVGDocument.CurrentElement.ID;
                            _gle.SvgUID = tlVectorControl2.SVGDocument.SvgdataUid;

                            IList<glebeProperty> UseProList = Services.BaseService.GetList<glebeProperty>("SelectglebePropertyByEleID", _gle);
                            if (UseProList.Count > 0) {
                                _gle = UseProList[0];
                                _gle.LayerID = SvgDocument.currentLayer;
                                frmMainProperty f = new frmMainProperty();

                                f.InitData(_gle, str220, str110,str66);

                                f.ShowDialog();

                            }
                            //}
                        }

                    }
                    if (getlayer(SvgDocument.currentLayer, "城市规划层", tlVectorControl2.SVGDocument.getLayerList())) {

                        if (tlVectorControl2.SVGDocument.getRZBRatio() != "") {
                            rzb = tlVectorControl2.SVGDocument.getRZBRatio();
                        }


                        if (xml1 == null || tlVectorControl2.SVGDocument.CurrentElement.ID == "svg") {
                            MessageBox.Show("请先选择地块。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (tlVectorControl2.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polygon") {
                            string IsArea = ((XmlElement)tlVectorControl2.SVGDocument.CurrentElement).GetAttribute("IsArea");
                            if (IsArea != "") {
                                frmProperty f = new frmProperty();

                                f.InitData(xml1.GetAttribute("id"), tlVectorControl2.SVGDocument.SvgdataUid, SelUseArea, rzb, SvgDocument.currentLayer);
                                //f.ShowDialog();
                                if (progtype != "城市规划层") {
                                    f.IsReadonly = true;
                                }
                                if (f.ShowDialog() == DialogResult.OK) {
                                    if (f.gPro.ObligateField1 != "") {
                                        string color1 = ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(f.gPro.ObligateField1)));
                                        color1 = "fill:" + color1 + ";";
                                        ((XmlElement)tlVectorControl2.SVGDocument.CurrentElement).SetAttribute("style", color1);
                                        tlVectorControl2.UpdateProperty();
                                    }

                                }
                            }
                        }
                    }

                    if (tlVectorControl2.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Line" || tlVectorControl2.SVGDocument.CurrentElement.GetType().ToString() == "ItopVector.Core.Figure.Polyline") {
                        string lineWidth = "2";
                        string IsLead = ((XmlElement)tlVectorControl2.SVGDocument.CurrentElement).GetAttribute("IsLead");
                        if (IsLead != "") {
                            frmLineProperty fl = new frmLineProperty();
                            fl.InitData(tlVectorControl2.SVGDocument.CurrentElement.GetAttribute("id"), tlVectorControl2.SVGDocument.SvgdataUid, LineLen, SvgDocument.currentLayer);
                            if (fl.ShowDialog() == DialogResult.OK) {
                                //Value="stroke-dasharray:8 8;stroke-width:2;stroke:#00C000;"
                                lineWidth = fl.LineWidth;
                                string styleValue = "";
                                if (fl.Line.ObligateField1 == "规划") {
                                    styleValue = "stroke-dasharray:6 1;stroke-width:" + lineWidth + ";";
                                } else {
                                    styleValue = "stroke-width:" + lineWidth + ";";
                                }
                                //string aa= ColorTranslator.ToHtml(Color.Black);
                                styleValue = styleValue + "stroke:" + ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(fl.Line.ObligateField2)));
                                ((XmlElement)tlVectorControl2.SVGDocument.CurrentElement).RemoveAttribute("style");
                                ((XmlElement)tlVectorControl2.SVGDocument.CurrentElement).SetAttribute("style", styleValue);
                                // tlVectorControl1.UpdateProperty();
                                // xml1.SetAttribute("style", styleValue);

                            }
                        }
                    }
                    if (xml1.GetAttribute("xlink:href").Contains("Substation")) {
                        string lab = xml1.GetAttribute("xlink:href");
                        frmSubstationProperty frmSub = new frmSubstationProperty();
                        frmSub.InitData(xml1.GetAttribute("id"), tlVectorControl2.SVGDocument.SvgdataUid, SvgDocument.currentLayer, lab);
                        frmSub.ShowDialog();
                    }
                    if (xml1.GetAttribute("xlink:href").Contains("SB_GT")) {
                        string lineWidth = "2";

                        string Code = xltProcessor.GetCurrentLineCode();
                        string _len = xltProcessor.GetWholeLineLength(Code).ToString("#####.####");

                        frmLineProperty fl = new frmLineProperty();
                        fl.InitData(Code, tlVectorControl2.SVGDocument.SvgdataUid, _len, SvgDocument.currentLayer);
                        if (fl.ShowDialog() == DialogResult.OK) {
                            //Value="stroke-dasharray:8 8;stroke-width:2;stroke:#00C000;"
                            lineWidth = fl.LineWidth;
                            string styleValue = "";
                            if (fl.Line.ObligateField1 == "规划") {
                                styleValue = "stroke-dasharray:4 4;stroke-width:" + lineWidth + ";";
                            } else {
                                styleValue = "stroke-width:" + lineWidth + ";";
                            }

                            styleValue = styleValue + "stroke:" + ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(fl.Line.ObligateField2)));
                            ((XmlElement)tlVectorControl2.SVGDocument.CurrentElement).RemoveAttribute("style");
                            ((XmlElement)tlVectorControl2.SVGDocument.CurrentElement).SetAttribute("style", styleValue);

                            xltProcessor.SetWholeLineAttribute(Code, "style", styleValue);
                        }
                    }

                }
                if (e.ClickedItem.Text == "移动") {
                    if (tlVectorControl2.SVGDocument.CurrentElement == null || tlVectorControl2.SVGDocument.CurrentElement.ID == "svg") {
                        return;
                    }
                    XmlElement xmln = (XmlElement)tlVectorControl2.SVGDocument.CurrentElement;
                    frmMove fm = new frmMove();
                    if (fm.ShowDialog() == DialogResult.OK) {
                        string strValue = fm.StrValue;
                        string[] str = strValue.Split(',');
                        string[] JWD1 = str[0].Split(' ');
                        Double J1 = Convert.ToDouble(JWD1[0]);
                        Double W1 = Convert.ToDouble(JWD1[1]);
                        Double D1 = Convert.ToDouble(JWD1[2]);
                        string[] JWD2 = str[1].Split(' ');
                        Double J2 = Convert.ToDouble(JWD2[0]);
                        Double W2 = Convert.ToDouble(JWD2[1]);
                        Double D2 = Convert.ToDouble(JWD2[2]);

                        Double JD = J1 + W1 / 60 + D1 / 3600;
                        Double WD = J2 + W2 / 60 + D2 / 3600;
                        IntXY xy = mapview.getXY(JD, WD);
                        PointF OffPoint = TLMath.getUseOffset(xmln.GetAttribute("xlink:href"));

                        xmln.RemoveAttribute("transform");
                        //xmln.RemoveAttribute("x");
                        xmln.SetAttribute("x", ((Double)(-xy.X / (Double)tlVectorControl2.ScaleRatio) - (Double)OffPoint.X).ToString("#####.####"));
                        // xmln.RemoveAttribute("y");
                        xmln.SetAttribute("y", ((Double)(-xy.Y / (Double)tlVectorControl2.ScaleRatio) - (Double)OffPoint.Y).ToString("#####.####"));
                        tlVectorControl2.Refresh();
                    }
                }
                if (e.ClickedItem.Text == "接线图") {
                    if (tlVectorControl2.SVGDocument.CurrentElement == null || tlVectorControl2.SVGDocument.CurrentElement.ID == "svg") {
                        return;
                    }
                    ParentUID = tlVectorControl2.SVGDocument.SvgdataUid;
                    Save();
                    ParentUID = tlVectorControl2.SVGDocument.SvgdataUid;
                    SVGFILE svg_temp = new SVGFILE();
                    svg_temp.SUID = ((XmlElement)tlVectorControl2.SVGDocument.CurrentElement).GetAttribute("id");
                    svg_temp.FILENAME = ((XmlElement)tlVectorControl2.SVGDocument.CurrentElement).GetAttribute("info-name");
                    IList svglist = Services.BaseService.GetList("SelectSVGFILEByKey", svg_temp);
                    OpenJXT(svglist, svg_temp);
                    //frmlar.SymbolDoc = tlVectorControl2.SVGDocument;
                    //frmlar.Progtype = MapType;
                    //frmlar.InitData();
                    //JxtBar();
                }
                if (e.ClickedItem.Text == "打开") {
                    if (tlVectorControl2.SVGDocument.CurrentElement.ID == "svg") {
                        MessageBox.Show("请选择地块。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    UseRelating UseRel = new UseRelating();
                    UseRel.UseID = tlVectorControl2.SVGDocument.CurrentElement.ID;
                    IList<UseRelating> UseRelList = Services.BaseService.GetList<UseRelating>("SelectUseRelatingByUseID", UseRel);
                    if (UseRelList.Count < 1) {
                        MessageBox.Show("选择的地块还没有关联到其他地图，请先设置关联地图", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    UseRel = UseRelList[0];
                    SVGFILE svgFile = new SVGFILE();
                    svgFile.SUID = UseRel.LinkUID;
                    IList svgList = Services.BaseService.GetList("SelectSVGFILEByKey", svgFile);
                    if (svgList.Count < 1) {
                        MessageBox.Show("被关联的地图已经被删除，请重新设置关联地图", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    svgFile = (SVGFILE)svgList[0];
                    //SvgDocument doc = new SvgDocument();

                    if (!string.IsNullOrEmpty(svgFile.SVGDATA)) {
                        //doc.LoadXml(svgFile.SVGDATA);
                        ctlfile_OnOpenSvgDocument(sender, svgFile.SUID);
                    }

                }
                if (e.ClickedItem.Text == "区域打印") {
                    PrintHelper ph = new PrintHelper(tlVectorControl2, mapview);
                    frmPrinter dlg = new frmPrinter();
                    dlg.printHelper = ph;
                    dlg.ShowDialog();
                    return;
                    ArrayList idlist = new ArrayList();
                    ArrayList symlist = new ArrayList();

                    SvgDocument _doc = new SvgDocument();

                    Graph poly1 = tlVectorControl2.SVGDocument.CurrentElement as Graph;
                    if (poly1 == null || poly1.GetAttribute("id") == "svg") {
                        return;
                    }
                    GraphicsPath gr1 = new GraphicsPath();
                    gr1.AddPolygon(TLMath.getPolygonPoints(poly1));
                    //gr1.CloseFigure();
                    gr1 = (GraphicsPath)poly1.GPath.Clone();

                    gr1.Transform((poly1 as IGraph).Transform.Matrix);

                    RectangleF ef1 = gr1.GetBounds();
                    ef1 = PathFunc.GetBounds(gr1);
                    StringBuilder svgtxt = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><svg id=\"svg\" width=\"" + ef1.Width + "\" height=\"" + ef1.Height + "\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" xmlns:itop=\"http://www.Itop.com/itop\">");

                    XmlNodeList nlist = tlVectorControl2.SVGDocument.GetElementsByTagName("defs");
                    if (nlist.Count > 0) {
                        XmlNode node = nlist[0];
                        svgtxt.AppendLine(node.OuterXml);
                    }
                    SvgElementCollection.ISvgElementEnumerator enumerator1 = tlVectorControl2.DrawArea.ElementList.GetEnumerator();// mouseAreaControl.PicturePanel.ElementList.GetEnumerator();
                    while (enumerator1.MoveNext()) {
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

                        if (ef1.Contains(ef2) || RectangleF.Intersect(ef1, ef2) != RectangleF.Empty) {

                            SvgElement ele = (SvgElement)graph1;
                            svgtxt.AppendLine(ele.OuterXml);
                            //tlVectorControl1.SVGDocument.AddSelectElement(graph1);
                            if (graph1 is Use) {
                                //PointF offset = TLMath.getUseOffset(((XmlElement)graph1).GetAttribute("xlink:href"));
                                //if (ef1.Contains(new PointF(((Use)graph1).X + offset.X, ((Use)graph1).Y + offset.Y))) {
                                    //SvgElement ele = (SvgElement)graph1;
                                    //svgtxt.AppendLine(ele.OuterXml);

                                    string symid = ((XmlElement)graph1).GetAttribute("xlink:href");
                                    if (!symlist.Contains(symid)) {
                                        symlist.Add(symid);
                                    }
                                //}
                            }
                            if (graph1.GetType().FullName == "ItopVector.Core.Figure.Polyline") {
                                string IsLead = ((XmlElement)graph1).GetAttribute("IsLead");
                                if (IsLead != "") {
                                    if (ef1.Contains(ef2)) {
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
                    _doc.SvgdataUid = tlVectorControl2.SVGDocument.SvgdataUid;
                    frmPrintF pri = new frmPrintF();
                    pri.Init(tlVectorControl2.SVGDocument.CurrentElement.ID, tlVectorControl2.SVGDocument.SvgdataUid);
                    if (pri.ShowDialog() == DialogResult.OK) {
                        frmSubPrint s = new frmSubPrint();
                        s.Vector = tlVectorControl2;
                        s.InitImg(pri.strzt, pri.strgs, pri.pri, idlist, symlist);
                        s.Open(_doc, ef1);
                        s.Show();
                    }
                }
                if (e.ClickedItem.Text == "分类统计报表") {
                    if (tlVectorControl2.SVGDocument.CurrentElement == null || tlVectorControl2.SVGDocument.CurrentElement.ID == "svg") {
                        return;
                    }
                    IGraph poly1 = (IGraph)tlVectorControl2.SVGDocument.CurrentElement;
                    frmPloyPrint p = new frmPloyPrint();

                    p.InitDate(poly1.ID, tlVectorControl2.SVGDocument.SvgdataUid);
                    p.ShowDialog();
                }
            } catch (Exception e1) {
                MessageBox.Show(e1.Message);
            }
        }
        public ArrayList ResetList(ArrayList list) {
            SortedList slist = new SortedList();
            ArrayList list2 = new ArrayList();
            for (int i = 0; i < list.Count; i++) {
                if (list[i].ToString() == "#Substation500-1") {
                    slist.Add(1, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#Substation220-1") {
                    slist.Add(2, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#user-Substation220-2") {
                    slist.Add(3, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#Substation110-1") {
                    slist.Add(4, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#user-Substation110-2") {
                    slist.Add(5, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#Substation66-1") {
                    slist.Add(6, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#user-Substation66-2") {
                    slist.Add(7, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#Substation35-1") {
                    slist.Add(8, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#user-Substation35-2") {
                    slist.Add(9, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-Substation500-1") {
                    slist.Add(11, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-Substation220-1") {
                    slist.Add(12, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-user-Substation220-2") {
                    slist.Add(13, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-Substation110-1") {
                    slist.Add(14, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-user-Substation110-2") {
                    slist.Add(15, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-Substation66-1") {
                    slist.Add(16, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-user-Substation66-2") {
                    slist.Add(17, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-Substation35-1") {
                    slist.Add(18, list[i]);
                    continue;
                }
                if (list[i].ToString() == "#gh-user-Substation35-2") {
                    slist.Add(19, list[i]);
                    continue;
                } else {
                    list2.Add(list[i]);
                }
            }
            ArrayList rlist = new ArrayList();
            IEnumerator n = slist.Values.GetEnumerator();
            while (n.MoveNext()) {
                rlist.Add(n.Current);
            }
            for (int j = 0; j < list2.Count; j++) {
                rlist.Add(list2[j]);
            }
            return rlist;
        }
        private void dotNetBarManager1_ItemClick(object sender, EventArgs e) {
            DevComponents.DotNetBar.ButtonItem btItem = sender as DevComponents.DotNetBar.ButtonItem;
            //Layer layer1 = (Layer)LayerBox.ComboBoxEx.SelectedItem;
            if (btItem != null) {
                //if (btItem.Name == "mRoam") {
                //    frmlar.Hide();
                //} else {
                //    frmlar.Show();
                //}
                switch (btItem.Name) {
                    #region 文件操作
                    case "mNew":
                        tlVectorControl2.NewFile();

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
                        tlVectorControl2.ExportSymbol();
                        break;
                    case "mSave":
                        Save();
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
                        //tlVectorControl1.OpenFile("C:\\a1.svg");
                        //tlVectorControl1.SVGDocument.SvgdataUid = aa;
                        this.Close();
                        //frmAddLine ff = new frmAddLine();
                        //ff.Show();
                        //tlVectorControl1.ExportSymbol();

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
                    case "bt2":
                        break;
                    case "mPriSet":
                        tlVectorControl2.PaperSetup();
                        break;
                    case "mPrint":
                        tlVectorControl2.Print();
                        break;
                    case "mView":
                        //frmSvgView fView = new frmSvgView();
                        //fView.Open(tlVectorControl1.SVGDocument.SvgdataUid);
                        //fView.Show();

                        break;
                    case "mViewScale":
                        if (img != null) {
                            frmtempViewScale fscale1 = new frmtempViewScale();
                            fscale1.ShowDialog();
                        } else {
                            frmViewScale fScale = new frmViewScale();
                            string viewScale = tlVectorControl2.SVGDocument.getViewScale();
                            if (viewScale != "") {
                                fScale.InitData(viewScale);
                            }
                            if (fScale.ShowDialog() == DialogResult.OK) {

                                //viewScale = fScale.ViewScale;
                                string _viewScale = fScale.ViewScale;
                                tlVectorControl2.SVGDocument.setViewScale(_viewScale);
                                if (viewScale == "") {
                                    viewScale = "1";
                                }
                                Recalculate(Convert.ToDecimal(_viewScale) / Convert.ToDecimal(viewScale));
                            }
                        }
                        break;
                    case "mRzb":
                        frmRatio fRat = new frmRatio();
                        string viewRat = tlVectorControl2.SVGDocument.getRZBRatio();
                        if (viewRat != "") {
                            fRat.InitData(viewRat);
                        }
                        if (fRat.ShowDialog() == DialogResult.OK) {
                            viewRat = fRat.ViewScale;
                            tlVectorControl2.SVGDocument.setRZBRatio(viewRat);
                        }
                        break;
                    case "mEdit":
                        if (MapType == "所内接线图") {
                            Save();
                            svg.SUID = ParentUID;
                            IList svglist = Services.BaseService.GetList("SelectSVGFILEByKey", svg);
                            svg = (SVGFILE)svglist[0];
                            sdoc = null;
                            sdoc = new SvgDocument();
                            sdoc.LoadXml(svg.SVGDATA);
                            tlVectorControl2.SVGDocument = sdoc;
                            tlVectorControl2.SVGDocument.SvgdataUid = svg.SUID;
                            MapType = "接线图";
                            CtrlSvgView.MapType = "接线图";
                            LoadShape("symbol_3.xml");
                            Init(progtype);
                            //ButtonEnb(true);
                            //frmlar.SymbolDoc = tlVectorControl2.SVGDocument;
                            //frmlar.Progtype = progtype;
                            //frmlar.InitData();

                            bk1.Enabled = true;
                            LoadImage = true;
                            tlVectorControl2.Refresh();
                        }
                        //tlVectorControl1.ContextMenuStrip = contextMenuStrip1;
                        MapType = "接线图";
                        break;

                    case "mAbout":

                        frmAbout frma = new frmAbout();
                        frma.ShowDialog();
                        break;
                    #endregion
                    #region 基础图元
                    case "mDecreaseView":
                        tlVectorControl2.Operation = ToolOperation.DecreaseView;

                        break;
                    case "mIncreaseView":
                        tlVectorControl2.Operation = ToolOperation.IncreaseView;

                        break;
                    case "mRoam":
                        tlVectorControl2.Operation = ToolOperation.Roam;

                        break;
                    case "mSelect":
                    case "mSel":
                        //tlVectorControl1.Operation = ToolOperation.Select;
                        tlVectorControl2.Operation = ToolOperation.FreeTransform;
                        break;
                    case "mFreeTransform":
                        tlVectorControl2.Operation = ToolOperation.FreeTransform;

                        break;
                    case "mFreeLines"://锁套
                        tlVectorControl2.Operation = ToolOperation.FreeLines;

                        break;
                    case "mFreePath":
                        tlVectorControl2.Operation = ToolOperation.FreePath;

                        break;
                    case "mShapeTransform":
                        tlVectorControl2.Operation = ToolOperation.ShapeTransform;
                        break;
                    case "mShapeTransform1":  //截断
                        tlVectorControl2.Operation = ToolOperation.Custom11;                                             
                        break;
                    case "mShapeTransform2":         //延长（e）               
                        tlVectorControl2.Operation = ToolOperation.Custom12;                        
                        break;
                    case "mShapeTransform3":   //延长（b）
                        tlVectorControl2.Operation = ToolOperation.Custom13;
                        break;
                    case "mShapeTransform4":   //增加
                        tlVectorControl2.Operation = ToolOperation.Custom15;
                        break;
                    case "mShapeTransform5":  //删除
                        tlVectorControl2.Operation = ToolOperation.Custom14;
                        break;
                    case "mAngleRectangle":
                        tlVectorControl2.Operation = ToolOperation.AngleRectangle;                        
                        break;
                    case "mEllipse":
                        tlVectorControl2.Operation = ToolOperation.Ellipse;
                        
                        break;
                    case "mLine":
                        tlVectorControl2.Operation = ToolOperation.Line;

                        break;
                    case "mPolyline":
                        tlVectorControl2.Operation = ToolOperation.PolyLine;

                        break;
                    case "mPolygon":
                        tlVectorControl2.Operation = ToolOperation.Polygon;

                        break;
                    case "mImage":
                        tlVectorControl2.Operation = ToolOperation.Image;

                        break;
                    case "mText":
                        tlVectorControl2.Operation = ToolOperation.Text;

                        break;
                    case "mBezier":
                        tlVectorControl2.Operation = ToolOperation.Bezier;

                        break;
                    case "mEnclosure":
                        tlVectorControl2.Operation = ToolOperation.Enclosure;

                        break;

                    case "mGroup":
                        tlVectorControl2.Group();
                        break;
                    case "mUnGroup":
                        tlVectorControl2.UnGroup();
                        break;
                    case "mlinelx":
                        tlVectorControl2.Operation = ToolOperation.ConnectLine_Line;
                        break;
                    case "mzxlx":
                        tlVectorControl2.Operation = ToolOperation.ConnectLine_Rightangle;
                        break;
                    case "mqxlx":
                        tlVectorControl2.Operation = ToolOperation.ConnectLine_Spline;
                        break;
                    case "mqzlx":
                        tlVectorControl2.Operation = ToolOperation.ConnectLine_Polyline;
                        break;
                    case "mCJ":                        
                        tlVectorControl2.Operation = ToolOperation.PolyLine;
                        csOperation = CustomOperation.OP_MeasureDistance;
                        break;
                    #endregion
                    #region 视图
                    case "mOption":
                        tlVectorControl2.SetOption();
                        break;
                    case "mLayer":
                        LayerManagerShow();
                        //tlVectorControl1.LayerManager();
                        break;
                    case "mAirscape":
                        frmAirscape fAir = new frmAirscape();
                        fAir.InitData(tlVectorControl2);
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
                    #region 查看
                    case "mDklb":
                        frmLayerList lay = new frmLayerList();
                        lay.InitData(tlVectorControl2.SVGDocument.getLayerList(), "1");
                        if (lay.ShowDialog() == DialogResult.OK) {
                            frmglebePropertyList flist1 = new frmglebePropertyList();
                            flist1.InitDataSub(tlVectorControl2.SVGDocument.SvgdataUid, lay.str_sid);
                            flist1.Show();
                        }
                        break;
                    case "mGhlb":
                        frmLayerList lay2 = new frmLayerList();
                        lay2.InitData(tlVectorControl2.SVGDocument.getLayerList(), "2");
                        if (lay2.ShowDialog() == DialogResult.OK) {
                            frmglebePropertyList flist2 = new frmglebePropertyList();
                            flist2.InitData(tlVectorControl2.SVGDocument.SvgdataUid, lay2.str_sid);
                            flist2.Show();
                        }
                        break;
                    case "mLineList":
                        frmLayerList lay3 = new frmLayerList();
                        lay3.InitData(tlVectorControl2.SVGDocument.getLayerList(), "2");
                        if (lay3.ShowDialog() == DialogResult.OK) {
                            frmLinePropertyList flist3 = new frmLinePropertyList();
                            flist3.InitData(tlVectorControl2.SVGDocument.SvgdataUid, lay3.str_sid);
                            flist3.Show();
                        }
                        break;
                    case "mDlph":
                        frmLayerList lay4 = new frmLayerList();
                        lay4.InitData(tlVectorControl2.SVGDocument.getLayerList(), "3");
                        if (lay4.ShowDialog() == DialogResult.OK) {
                            frmSubstationPropertyList fSub = new frmSubstationPropertyList();
                            fSub.InitData(tlVectorControl2.SVGDocument.SvgdataUid, lay4.str_sid);
                            fSub.Show();
                        }
                        break;
                    #endregion
                    #region 布局，对齐，顺序
                    case "mRotate":
                        if (btItem.Tag is ButtonItem) {
                            btItem = btItem.Tag as ButtonItem;
                            tlVectorControl2.FlipX();


                        } else {
                            tlVectorControl2.FlipX();
                        }
                        break;
                    case "mToH":

                        tlVectorControl2.FlipX();
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToV":
                        tlVectorControl2.FlipY();
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToLeft":
                        tlVectorControl2.RotateSelection(-90f);
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mToRight":
                        tlVectorControl2.RotateSelection(90f);
                        this.rotateButton.Tag = btItem;
                        this.rotateButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mAlign":
                        if (btItem.Tag is ButtonItem) {
                            btItem = btItem.Tag as ButtonItem;
                            tlVectorControl2.Align(AlignType.Left);

                        } else {
                            tlVectorControl2.Align(AlignType.Left);

                        }
                        tlVectorControl2.Refresh();
                        break;
                    case "mAlignLeft":
                        tlVectorControl2.Align(AlignType.Left);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl2.Refresh();
                        break;
                    case "mAlignRight":
                        tlVectorControl2.Align(AlignType.Right);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl2.Refresh();
                        break;
                    case "mAlignTop":
                        tlVectorControl2.Align(AlignType.Top);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl2.Refresh();
                        break;
                    case "mAlignBottom":
                        tlVectorControl2.Align(AlignType.Bottom);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl2.Refresh();
                        break;
                    case "mAlignHorizontalCenter":
                        tlVectorControl2.Align(AlignType.HorizontalCenter);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl2.Refresh();
                        break;
                    case "mAlignVerticalCenter":
                        tlVectorControl2.Align(AlignType.VerticalCenter);
                        this.alignButton.ImageIndex = btItem.ImageIndex;
                        this.alignButton.Tag = btItem;
                        tlVectorControl2.Refresh();
                        break;
                    case "mOrder":
                        if (btItem.Tag is ButtonItem) {
                            btItem = btItem.Tag as ButtonItem;
                            tlVectorControl2.ChangeLevel(LevelType.Top);

                        } else {
                            tlVectorControl2.ChangeLevel(LevelType.Top);
                        }

                        break;
                    case "mGoTop":
                        tlVectorControl2.ChangeLevel(LevelType.Top);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mGoUp":
                        tlVectorControl2.ChangeLevel(LevelType.Up);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mGoDown":
                        tlVectorControl2.ChangeLevel(LevelType.Down);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    case "mGoBottom":
                        tlVectorControl2.ChangeLevel(LevelType.Bottom);
                        this.orderButton.Tag = btItem;
                        this.orderButton.ImageIndex = btItem.ImageIndex;
                        break;
                    #endregion
                    #region 图元操作
                    case "mCopy":
                        tlVectorControl2.Copy();
                        break;
                    case "mCut":
                        tlVectorControl2.Cut();
                        break;
                    case "mPaste":
                        tlVectorControl2.Paste();
                        break;
                    case "mDelete":
                        if (tlVectorControl2.SVGDocument.CurrentElement != null && tlVectorControl2.SVGDocument.CurrentElement.ID != "svg") {
                            frmMessageBox msg = new frmMessageBox();
                            if (msg.ShowDialog() == DialogResult.OK) {
                                if (msg.ck) {
                                    // if(MessageBox.Show("确认删除么？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes){
                                    for (int i = 0; i < tlVectorControl2.SVGDocument.SelectCollection.Count; i++) {
                                        if (tlVectorControl2.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Polygon") {
                                            glebeProperty gle = new glebeProperty();
                                            gle.SvgUID = tlVectorControl2.SVGDocument.SvgdataUid;
                                            gle.EleID = tlVectorControl2.SVGDocument.SelectCollection[i].ID;
                                            Services.BaseService.Update("DeleteglebePropertyByEleID", gle);
                                        }
                                        if (tlVectorControl2.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Polyline" || tlVectorControl2.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Line") {
                                            LineInfo _line = new LineInfo();
                                            _line.SvgUID = tlVectorControl2.SVGDocument.SvgdataUid;
                                            _line.EleID = tlVectorControl2.SVGDocument.SelectCollection[i].ID;
                                            LineInfo temp = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", _line);
                                            if (temp != null) {
                                                Services.BaseService.Update("DeleteLinePropertyByEleID", _line);

                                                Services.BaseService.Update("DeleteLine_InfoByCode", temp.UID);
                                            }
                                        }
                                        if (tlVectorControl2.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Use") {
                                            string str_name = ((XmlElement)(tlVectorControl2.SVGDocument.SelectCollection[i])).GetAttribute("xlink:href");
                                            if (str_name.Contains("Substation")) {
                                                substation _sub = new substation();
                                                _sub.SvgUID = tlVectorControl2.SVGDocument.SvgdataUid;
                                                _sub.EleID = tlVectorControl2.SVGDocument.SelectCollection[i].ID;

                                                substation temp = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _sub);

                                                if (temp != null) {
                                                    Services.BaseService.Update("DeletesubstationByEleID", _sub);

                                                    Services.BaseService.Update("DeleteSubstation_InfoByCode", temp.UID);
                                                }
                                            }
                                        }
                                        if (tlVectorControl2.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.ConnectLine") {
                                            ConnectLine cline = (ConnectLine)tlVectorControl2.SVGDocument.SelectCollection[i];
                                            if (cline.StartGraph != null) {
                                                SvgElement ele = (SvgElement)cline.StartGraph;
                                                if (!ele.GetAttribute("xlink:href").Contains("Substation")) {
                                                    tlVectorControl2.SVGDocument.SelectCollection.Add(cline.StartGraph);
                                                }
                                            }
                                            if (cline.EndGraph != null) {
                                                tlVectorControl2.SVGDocument.SelectCollection.Add(cline.EndGraph);
                                            }
                                        }


                                    }
                                } else {
                                    for (int i = 0; i < tlVectorControl2.SVGDocument.SelectCollection.Count; i++) {

                                        if (tlVectorControl2.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Polyline" || tlVectorControl2.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Line") {
                                            LineInfo _line = new LineInfo();
                                            _line.SvgUID = tlVectorControl2.SVGDocument.SvgdataUid;
                                            _line.EleID = tlVectorControl2.SVGDocument.SelectCollection[i].ID;

                                            LineInfo linetemp = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", _line);
                                            if (linetemp != null) {
                                                PowerProTypes temp = (PowerProTypes)Services.BaseService.GetObject("SelectPowerProTypesByCode", linetemp.UID);
                                                if (temp != null) {
                                                    linetemp.EleID = "";
                                                    Services.BaseService.Update<LineInfo>(linetemp);
                                                } else {
                                                    Services.BaseService.Update("DeleteLineInfo", linetemp);
                                                }
                                            }

                                        }
                                        if (tlVectorControl2.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.Use") {
                                            string str_name = ((XmlElement)(tlVectorControl2.SVGDocument.SelectCollection[i])).GetAttribute("xlink:href");
                                            if (str_name.Contains("Substation")) {
                                                substation _sub = new substation();
                                                _sub.SvgUID = tlVectorControl2.SVGDocument.SvgdataUid;
                                                _sub.EleID = tlVectorControl2.SVGDocument.SelectCollection[i].ID;

                                                substation subtemp = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _sub);
                                                if (subtemp != null) {
                                                    PowerProTypes temp = (PowerProTypes)Services.BaseService.GetObject("SelectPowerProTypesByCode", subtemp.UID);
                                                    if (temp != null) {
                                                        subtemp.EleID = "";
                                                        Services.BaseService.Update<substation>(subtemp);
                                                    } else {
                                                        Services.BaseService.Update("Deletesubstation", subtemp);
                                                    }
                                                }
                                            }
                                        }
                                        if (tlVectorControl2.SVGDocument.SelectCollection[i].GetType().ToString() == "ItopVector.Core.Figure.ConnectLine") {
                                            ConnectLine cline = (ConnectLine)tlVectorControl2.SVGDocument.SelectCollection[i];
                                            if (cline.StartGraph != null) {
                                                SvgElement ele = (SvgElement)cline.StartGraph;
                                                if (!ele.GetAttribute("xlink:href").Contains("Substation")) {
                                                    tlVectorControl2.SVGDocument.SelectCollection.Add(cline.StartGraph);
                                                }
                                            }
                                            if (cline.EndGraph != null) {
                                                tlVectorControl2.SVGDocument.SelectCollection.Add(cline.EndGraph);
                                            }
                                        }
                                    }

                                }

                                tlVectorControl2.Delete();
                            }
                        }
                        //tlVectorControl1.Operation = ToolOperation.Select;
                        break;
                    case "mUodo":
                        tlVectorControl2.Undo();
                        break;
                    case "mRedo":
                        tlVectorControl2.Redo();
                        break;
                    #endregion
                    #region 业务操作

                    case "mSubPrint":
                        //tlVectorControl1.CurrentOperation = ToolOperation.InterEnclosure;
                        //SubPrint = true;
                        //frmSubPrint s = new frmSubPrint();
                        ////s.Open(tlVectorControl1.SVGDocument);
                        //s.Show();
                        Hashtable HashTable1 = new Hashtable();
                        HashTable1.Add("SUID", tlVectorControl2.SVGDocument.SvgdataUid);
                        Services.BaseService.Update("UpdateGlebePropertyAll", HashTable1);
                        break;
                    case "mJQLeadLine":
                        tlVectorControl2.Operation = ToolOperation.Select;
                        frmAddLine aLine = new frmAddLine();
                        if (aLine.ShowDialog() == DialogResult.OK) {
                            string points = "";
                            ArrayList list = aLine.list;
                            LineInfo line = aLine.line;
                            string lineWidth = aLine.LineWidth;
                            //ICollection Ilist = list.Values;
                            //IEnumerator IEnum=Ilist.GetEnumerator();
                            for (int n = 0; n < list.Count; n++) {
                                //while (IEnum.MoveNext())
                                //{
                                string[] str = ((string)list[n]).Split(',');
                                //string[] str = ((string)IEnum.Current).Split(',');
                                string[] JWD1 = str[0].Split(' ');
                                Double J1 = Convert.ToDouble(JWD1[0]);
                                Double W1 = Convert.ToDouble(JWD1[1]);
                                Double D1 = Convert.ToDouble(JWD1[2]);
                                string[] JWD2 = str[1].Split(' ');
                                Double J2 = Convert.ToDouble(JWD2[0]);
                                Double W2 = Convert.ToDouble(JWD2[1]);
                                Double D2 = Convert.ToDouble(JWD2[2]);

                                Double JD = J1 + W1 / 60 + D1 / 3600;
                                Double WD = J2 + W2 / 60 + D2 / 3600;
                                IntXY xy = mapview.getXY(JD, WD);
                                points = points + (-xy.X / (Double)tlVectorControl2.ScaleRatio) + " " + (-xy.Y / (Double)tlVectorControl2.ScaleRatio) + ",";
                                //}
                            }
                            if (points.Length > 1) {
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

                            XmlElement n1 = tlVectorControl2.SVGDocument.CreateElement("polyline") as Polyline;
                            n1.SetAttribute("IsLead", "1");
                            n1.SetAttribute("points", points);
                            n1.SetAttribute("layer", SvgDocument.currentLayer);
                            // n1.SetAttribute("style", styleValue);
                            tlVectorControl2.SVGDocument.RootElement.AppendChild(n1);
                            line.UID = Guid.NewGuid().ToString();
                            line.EleID = n1.GetAttribute("id");
                            line.SvgUID = tlVectorControl2.SVGDocument.SvgdataUid;
                            Services.BaseService.Create<LineInfo>(line);

                        }
                        break;
                    case "mLeadLine":
                        //tlVectorControl1.DrawArea.BackgroundImage = System.Drawing.Image.FromFile("f:\\back11.jpg");
                        //tlVectorControl1.DrawArea.BackgroundImageLayout = ImageLayout.Center;

                        if (!getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl2.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("电网规划层"))
                        {
                            MessageBox.Show("请选择电网规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        // sgt1.Visible = true;
                        tlVectorControl2.Operation = ToolOperation.Select;
                        tlVectorControl2.Operation = ToolOperation.LeadLine;
                        break;
                    case "mAreaPoly":
                        if (!getlayer(SvgDocument.currentLayer, "城市规划层", tlVectorControl2.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("城市规划层"))
                        {
                            MessageBox.Show("请选择城市规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        tlVectorControl2.Operation = ToolOperation.Select;
                        tlVectorControl2.Operation = ToolOperation.AreaPolygon;

                        break;
                    case "mFx":
                        SubPrint = false;
                        bool ck = false;
                        //CheckedListBox.CheckedItemCollection ckcol = frmlar.checkedListBox1.CheckedItems;
                        //for (int i = 0; i < ckcol.Count; i++) {
                        //    Layer _lar = ckcol[i] as Layer;
                        //    if (_lar.GetAttribute("layerType") == "城市规划层") {
                        //        ck = true;
                        //    }
                        //}
                        //if (!ck) {
                        //    MessageBox.Show("请打开城市规划层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}
                        if (!getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl2.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("供电区域层"))
                        {
                            MessageBox.Show("请选择电网规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        tlVectorControl2.Operation = ToolOperation.Select;
                        tlVectorControl2.Operation = ToolOperation.InterEnclosure;
                        MapType = "接线图";
                        break;
                    case "mGhfx":
                        if (!getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl2.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("供电区域层"))
                        {
                            MessageBox.Show("请选择电网规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        tlVectorControl2.Operation = ToolOperation.Enclosure;
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
                        if (!getlayer(SvgDocument.currentLayer, "电网规划层", tlVectorControl2.SVGDocument.getLayerList()))
                        //if (!layer1.Label.Contains("供电区域层"))
                        {
                            MessageBox.Show("请选择电网规划层作为当前图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        //LayerBox.ComboBoxEx.SelectedIndex = 2;
                        tlVectorControl2.Operation = ToolOperation.Select;
                        MapType = "规划统计";
                        break;
                    case "mDkwh":　//地块维护
                        frmPropertyClass frmProp = new frmPropertyClass();
                        frmProp.ShowDialog();
                        break;
                    case "mDkfl": //地块分类
                        if (tlVectorControl2.SVGDocument.CurrentElement == null || tlVectorControl2.SVGDocument.CurrentElement.ID == "svg") {
                            MessageBox.Show("请选择地块。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        frmUsePropertySelect frmUseSel = new frmUsePropertySelect();
                        frmUseSel.InitData(tlVectorControl2.SVGDocument.CurrentElement.ID, tlVectorControl2.SVGDocument.SvgdataUid);
                        frmUseSel.ShowDialog();
                        break;
                    case "mGldt": //关联地图
                        if (tlVectorControl2.SVGDocument.CurrentElement == null || tlVectorControl2.SVGDocument.CurrentElement.ID == "svg") {
                            MessageBox.Show("请选择地块。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        frmFileSelect frmSel = new frmFileSelect();
                        frmSel.InitData(tlVectorControl2.SVGDocument.CurrentElement.ID, tlVectorControl2.SVGDocument.SvgdataUid, true);
                        frmSel.ShowDialog();
                        break;
                    case "mPriQu":
                        SubPrint = true;
                        tlVectorControl2.Operation = ToolOperation.InterEnclosure;
                        break;

                    case "mReCompute":
                        if (MessageBox.Show("确认要重新计算全图的电量和负荷么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                            string scale = tlVectorControl2.SVGDocument.getViewScale();
                            if (scale != "") {
                                Recalculate(Convert.ToDecimal(scale));
                            } else {
                                Recalculate(1);
                            }

                            MessageBox.Show("重新计算完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case "mXLine":
                        tlVectorControl2.Operation = ToolOperation.Select;
                        tlVectorControl2.Operation = ToolOperation.XPolyLine;
                        break;
                    case "mYLine":
                        tlVectorControl2.Operation = ToolOperation.Select;
                        tlVectorControl2.Operation = ToolOperation.YPolyLine;
                        break;

                    case "mFhbz":
                        if (MessageBox.Show("是否生成负荷标注?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
                            Fhbz();
                        }
                        break;
                    case "mSaveGroup":
                        if (tlVectorControl2.SVGDocument.SelectCollection.Count > 1) {
                            string content = "<svg>";
                            SvgElementCollection col = tlVectorControl2.SVGDocument.SelectCollection;
                            for (int i = 0; i < col.Count; i++) {
                                SvgElement _e = (SvgElement)col[i];
                                if (_e.ID != "svg") {
                                    content = content + _e.OuterXml;
                                }
                            }
                            RectangleF rect = tlVectorControl2.DrawArea.viewer.SelectedViewRectangle;

                            content = content + "</svg>";
                            frmSaveGroup fm = new frmSaveGroup();
                            fm.rect = rect;
                            fm.Content = content;
                            fm.ShowDialog();
                        } else {
                            MessageBox.Show("请至少选择2个图元。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        break;

                    case "mInsert":
                        frmUseGroup fg = new frmUseGroup();
                        if (fg.ShowDialog() == DialogResult.OK) {
                            UseGroup u = fg.SelectedUseGroup;
                            if (u != null) {
                                frmXY xy = new frmXY();
                                if (xy.ShowDialog() == DialogResult.OK) {
                                    decimal x = xy.GetX();
                                    decimal y = xy.GetY();
                                    string content = u.Content;
                                    XmlDocument doc = new XmlDocument();
                                    doc.LoadXml(u.Content);
                                    XmlNodeList list = doc.ChildNodes;
                                    XmlNode _node = list[0];
                                    XmlNodeList sonlist = _node.ChildNodes;
                                    XmlElement ele = tlVectorControl2.SVGDocument.CreateElement("g");
                                    ele.SetAttribute("layer", SvgDocument.currentLayer);
                                    for (int i = 0; i < sonlist.Count; i++) {
                                        XmlNode _sonnode = sonlist[i];
                                        //string str = _sonnode.OuterXml;
                                        if (_sonnode.Name == "use") {
                                            string sid = ((XmlElement)_sonnode).GetAttribute("xlink:href");
                                            XmlNode _snode = symbolSelector.SymbolDoc.SelectSingleNode("//*[@id='" + sid.Substring(1) + "']");
                                            tlVectorControl2.SVGDocument.AddDefsElement((SvgElement)_snode);
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
                                    tlVectorControl2.SVGDocument.RootElement.AppendChild(ele);
                                    tlVectorControl2.SVGDocument.SelectCollection.Clear();
                                    tlVectorControl2.SVGDocument.SelectCollection.Add((SvgElement)ele);
                                    tlVectorControl2.UnGroup();
                                    // tlVectorControl1.Refresh();
                                }
                            }
                        }
                        break;
                    #endregion
                }
            }
        }
        /// <summary>
        /// 导出区域图片
        /// </summary>
        private void ExportImage() {
            Polygon rt1 = tlVectorControl2.SVGDocument.CurrentElement as Polygon;
            if (rt1 == null) return;
            RectangleF rtf1 = rt1.GetBounds();
            int width =(int)Math.Round(rtf1.Width * tlVectorControl2.ScaleRatio,0);
            int height = (int)Math.Round(rtf1.Height * tlVectorControl2.ScaleRatio, 0);
            //if (width > 7000 || height > 7000) return;
            System.Drawing.Image image = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format16bppRgb565);
            Graphics g = Graphics.FromImage(image);

            g.SmoothingMode = SmoothingMode.HighQuality;
            
            g.CompositingQuality = CompositingQuality.HighQuality;
            PointF ptf = new PointF((rtf1.X + rtf1.Right) / 2, (rtf1.Y + rtf1.Bottom) / 2);
            Point pt = new Point((int)ptf.X, (int)ptf.Y);
            g.Clear(Color.White);
            drawMap(g, width, height, pt);
            Matrix matrix1 = new Matrix();
            
            matrix1.Scale(tlVectorControl2.ScaleRatio, tlVectorControl2.ScaleRatio);
            matrix1.Translate(-rtf1.X, -rtf1.Y);

            g.Transform = matrix1;

            RenderTo(g);
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "图像文件(*.png)|*.png|图像文件(*.jpg)|*.jpg|图像文件(*.gif)|*.gif|Bitmap文件(*.bmp)|*.bmp|Jpeg文件(*.jpeg)|*.jpeg|所有文件(*.*)|*.*"; 
            if (dlg.ShowDialog() == DialogResult.OK) {
                string str = Path.GetExtension(dlg.FileName);
                str = str.Substring(1);
                if (string.IsNullOrEmpty(str)) str = "Bmp";
                
                ImageFormat iformat = ImageFormat.Bmp;
                switch (str.ToLower()) {
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
        private void RenderTo(Graphics g) {
            SvgDocument svgdoc = tlVectorControl2.SVGDocument;
            Matrix matrix1 = new Matrix();
            Matrix matrix2 = new Matrix();
            matrix1 = ((SVG)svgdoc.RootElement).GraphTransform.Matrix;
            matrix2.Multiply(matrix1);
            matrix1.Reset();
            matrix1.Multiply(g.Transform);
            g.ResetTransform();
            try {

                SVG svg1 = svgdoc.DocumentElement as SVG;
                svgdoc.BeginPrint = true;
                SmoothingMode mode1 = svgdoc.SmoothingMode;
                svgdoc.SmoothingMode = g.SmoothingMode;
                svg1.Draw(g, svgdoc.ControlTime);
                svgdoc.SmoothingMode = mode1;
                svgdoc.BeginPrint = false;
            } finally {
                g.Transform = matrix1.Clone();
                matrix1.Reset();
                matrix1.Multiply(matrix2);
            }
        }
        void drawMap(Graphics g,int width,int height,Point center) {
            try {
                    int nScale = 0;
                    switch ((int)(decimal)(this.tlVectorControl2.DrawArea.ScaleUnit * 1000)) {
                        case 10:
                            nScale = 5;
                            break;
                        case 20:
                            nScale = 6;
                            break;
                        case 40:
                            nScale = 7;
                            break;
                        case 100:
                            nScale = 8;
                            break;
                        case 200:
                            nScale = 9;
                            break;
                        case 400:
                            nScale = 10;
                            break;
                        case 1000:
                            nScale = 11;
                            break;
                        case 2000:
                            nScale = 12;
                            break;
                        case 4000:
                            nScale = 13;
                            break;
                        default:
                            return;
                    }
                    LongLat longlat = LongLat.Empty;
                    //计算中心点经纬度

                    longlat = mapview.OffSet(mapview.ZeroLongLat, mapview.Getlevel(1), center.X, center.Y);

                    longlat = mapview.OffSetZero(-(int)(center.X * tlVectorControl2.ScaleRatio), -(int)(center.Y * tlVectorControl2.ScaleRatio));

                    
                    //g.Clear(ColorTranslator.FromHtml("#EBEAE8"));
                    ImageAttributes imageA = new ImageAttributes();
                    Color color = ColorTranslator.FromHtml("#EBEAE8");
                    imageA.SetColorKey(color, color);

                    mapview.Paint(g, width, height, nScale, longlat.Longitude, longlat.Latitude,imageA);

                    //绘制比例尺
                    Point p1 = new Point(20, height - 30);
                    Point p2 = new Point(20, height - 20);
                    Point p3 = new Point(80, height - 20);
                    Point p4 = new Point(80, height - 30);

                    g.DrawLines(new Pen(Color.Black, 2), new Point[4] { p1, p2, p3, p4 });
                    string str1 = string.Format("{0}公里", mapview.GetMiles(nScale));
                    g.DrawString(str1, new Font("宋体", 10), Brushes.Black, 30, height - 40);
                
            } catch (Exception e1) {
            }

        }

        void frmlar_OnClickLayer(object sender, Layer lar) {
            ArrayList a = tlVectorControl2.SVGDocument.getLayerList();
            SvgDocument.currentLayer = lar.ID;

            if (lar.GetAttribute("layerType") == progtype) {
                tlVectorControl2.CanEdit = true;
                if (progtype == "地理信息层") {
                    DlBarVisible(true);
                }
                if (progtype == "城市规划层") {
                    DkBarVisible(true);
                }
                if (progtype == "电网规划层") {
                    DwBarVisible(true);
                }
            } else {
                tlVectorControl2.CanEdit = false;
                if (progtype == "地理信息层") {
                    DlBarVisible(false);
                }
                if (progtype == "城市规划层") {
                    DkBarVisible(false);
                }
                if (progtype == "电网规划层") {
                    DwBarVisible(false);
                }
            }
            if (lar.GetAttribute("layerType") == "所内接线图") {
                JxtBar();
                return;
            }
        }
        public void InitTK1() {
            XmlElement root = tlVectorControl2.SVGDocument.RootElement;
            LineInfo line = new LineInfo();
            line.SvgUID = tlVectorControl2.SVGDocument.SvgdataUid;
            IList<LineInfo> list = Services.BaseService.GetList<LineInfo>("SelectLineInfoBySvgIDAll", line);
            for (int n = 0; n < list.Count; n++) {

            }
        }
        public void InitTK() {
            XmlElement root = tlVectorControl2.SVGDocument.RootElement;

            IList<glebeType> list = Services.BaseService.GetStrongList<glebeType>();
            for (int i = 0; i < list.Count; i++) {
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

            for (int n = 0; n < list.Count; n++) {

                XmlElement n1 = tlVectorControl2.SVGDocument.CreateElement("rect") as RectangleElement;
                XmlElement t1 = tlVectorControl2.SVGDocument.CreateElement("text") as Text;
                t1.InnerText = list[n].TypeStyle;
                t1.SetAttribute("layer", SvgDocument.currentLayer);

                rem = Math.DivRem(n, 6, out res);
                if (rem < 1) {
                    n1.SetAttribute("x", Convert.ToString(offx));
                    n1.SetAttribute("y", Convert.ToString(dec_height + 100));
                    t1.SetAttribute("x", Convert.ToString(offx + 220));
                    t1.SetAttribute("y", Convert.ToString(dec_height + 100 + 35));
                    t1.SetAttribute("font-size", "30");
                } else {
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
                tlVectorControl2.SVGDocument.RootElement.AppendChild(n1);
                tlVectorControl2.SVGDocument.RootElement.AppendChild(t1);
                offx = offx + 400;
            }
            SvgElement tk = tlVectorControl2.SVGDocument.CreateElement("rect") as RectangleElement;
            tk.SetAttribute("x", "0");
            tk.SetAttribute("y", "0");
            tk.SetAttribute("width", "4200");
            tk.SetAttribute("height", "3000");
            tk.SetAttribute("style", "fill:none;fill-opacity:1;stroke:#000000;stroke-opacity:1;stroke-width:3;");
            tk.SetAttribute("layer", SvgDocument.currentLayer);
            tlVectorControl2.SVGDocument.RootElement.AppendChild(tk);
            tlVectorControl2.SVGDocument.SelectCollection.Clear();
            tlVectorControl2.SVGDocument.CurrentElement = tk;
            tlVectorControl2.ChangeLevel(LevelType.Bottom);
            root.SetAttribute("height", "3000");

            SvgElement ntk = tlVectorControl2.SVGDocument.CreateElement("rect") as RectangleElement;
            ntk.SetAttribute("x", "0");
            ntk.SetAttribute("y", "0");
            ntk.SetAttribute("width", "4200");
            ntk.SetAttribute("height", "2500");
            ntk.SetAttribute("style", "fill:none;fill-opacity:1;stroke:#000000;stroke-opacity:1;stroke-width:1;");
            ntk.SetAttribute("layer", SvgDocument.currentLayer);
            tlVectorControl2.SVGDocument.RootElement.AppendChild(ntk);
            tlVectorControl2.SVGDocument.SelectCollection.Clear();
            tlVectorControl2.SVGDocument.CurrentElement = ntk;
            tlVectorControl2.ChangeLevel(LevelType.Bottom);
            tlVectorControl2.Refresh();
            tlVectorControl2.SVGDocument.SelectCollection.Clear();

            XmlElement t01 = tlVectorControl2.SVGDocument.CreateElement("text") as Text;
            t01.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.65));
            t01.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + 500 * 0.2));
            t01.InnerText = "规划设计";
            t01.SetAttribute("font-size", "30");
            tlVectorControl2.SVGDocument.RootElement.AppendChild(t01);

            XmlElement t02 = tlVectorControl2.SVGDocument.CreateElement("text") as Text;
            t02.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.65));
            t02.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + 500 * 0.4));
            t02.InnerText = "哈尔滨通力软件有限公司";
            t02.SetAttribute("font-size", "30");
            tlVectorControl2.SVGDocument.RootElement.AppendChild(t02);

            XmlElement t03 = tlVectorControl2.SVGDocument.CreateElement("text") as Text;
            t03.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.65));
            t03.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + +500 * 0.6));
            t03.InnerText = "委托单位";
            t03.SetAttribute("font-size", "30");
            tlVectorControl2.SVGDocument.RootElement.AppendChild(t03);

            XmlElement t04 = tlVectorControl2.SVGDocument.CreateElement("text") as Text;
            t04.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.65));
            t04.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + 500 * 0.8));
            t04.InnerText = "伊 春 电 业 局";
            t04.SetAttribute("font-size", "30");
            tlVectorControl2.SVGDocument.RootElement.AppendChild(t04);

            XmlElement t05 = tlVectorControl2.SVGDocument.CreateElement("text") as Text;
            t05.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.8));
            t05.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + 500 * 0.1));
            t05.InnerText = "工程名称     伊春城区“十一五”中压配电网发展规划";
            t05.SetAttribute("font-size", "30");
            tlVectorControl2.SVGDocument.RootElement.AppendChild(t05);

            XmlElement t06 = tlVectorControl2.SVGDocument.CreateElement("text") as Text;
            t06.SetAttribute("x", Convert.ToString(Convert.ToDouble(dec_width) * 0.8));
            t06.SetAttribute("y", Convert.ToString(Convert.ToDouble(dec_height) + 500 * 0.25));
            t06.InnerText = "图名     伊春市负荷密度主题";
            t06.SetAttribute("font-size", "30");
            tlVectorControl2.SVGDocument.RootElement.AppendChild(t06);



        }

        public void Fhbz() {
            XmlNodeList list = tlVectorControl2.SVGDocument.SelectNodes("//*[@IsArea=\"1\"]");

            Layer oldLar = getlayer("负荷标注", tlVectorControl2.SVGDocument.getLayerList());
            if (oldLar != null) {
                XmlNodeList oldList = tlVectorControl2.SVGDocument.SelectNodes("//*[@layer=\"" + oldLar.ID + "\"]");
                for (int i = 0; i < oldList.Count; i++) {
                    tlVectorControl2.SVGDocument.RootElement.RemoveChild((SvgElement)oldList[i]);
                }
                tlVectorControl2.SVGDocument.GetElementsByTagName("defs")[0].RemoveChild(oldLar);
                //tlVectorControl1.SVGDocument.RootElement.RemoveChild(oldLar);
            }


            Layer zjLar = Layer.CreateNew("负荷标注", tlVectorControl2.SVGDocument);
            zjLar.SetAttribute("layerType", "城市规划层");
            for (int i = 0; i < list.Count; i++) {
                IGraph graph1 = (IGraph)list[i];
                RectangleF rect = graph1.GetBounds();
                glebeProperty gle = new glebeProperty();
                gle.EleID = graph1.ID;
                gle.SvgUID = tlVectorControl2.SVGDocument.SvgdataUid;
                gle = (glebeProperty)Services.BaseService.GetObject("SelectglebePropertyByEleID", gle);

                if (gle != null) {
                    XmlElement n1 = tlVectorControl2.SVGDocument.CreateElement("text") as Text;
                    //Point point1 = tlVectorControl1.PointToView(new Point(e.Mouse.X, e.Mouse.Y));
                    n1.SetAttribute("x", Convert.ToString(rect.X + rect.Width / 2));
                    n1.SetAttribute("y", Convert.ToString(rect.Y + rect.Height / 2));
                    n1.SetAttribute("font-famliy", "宋体");
                    n1.SetAttribute("font-size", "14");
                    n1.InnerText = gle.Burthen.ToString();
                    n1.SetAttribute("layer", zjLar.ID);
                    tlVectorControl2.SVGDocument.RootElement.AppendChild(n1);
                }
            }
            //frmlar.SymbolDoc = tlVectorControl2.SVGDocument;
            //frmlar.InitData();
            tlVectorControl2.Refresh();

        }

        private void dotNetBarManager1_PopupContainerLoad(object sender, EventArgs e) {

        }

        private void dotNetBarManager1_ContainerLoadControl(object sender, EventArgs e) {
            BaseItem item = sender as BaseItem;
            DockContainerItem dockitem = null;
            if (item == null)
                return;
            if (item.Name == "DockContainerty") {
                dockitem = item as DockContainerItem;
                this.symbolSelector = new ItopVector.Selector.SymbolSelector(Application.StartupPath + "\\symbol\\symbol2.xml");
                this.symbolSelector.Dock = DockStyle.Fill;
                dockitem.Control = this.symbolSelector;
                tlVectorControl2.SymbolSelector = this.symbolSelector;
            }
            if (item.Name == "DockContainersx") {
                dockitem = item as DockContainerItem;
                dockitem.Control = this.propertyGrid;
            }
            if (item.Name == "DockContainerwj") {
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
        public void Open(string _SvgUID, string pid) {
            ParentUID = pid;
            //MapType = "所内接线图";
            Open(_SvgUID);
            this.tlVectorControl2.Size = new Size((Screen.PrimaryScreen.WorkingArea.Height - 258), (Screen.PrimaryScreen.WorkingArea.Width - 176));
        }

        public void LayerManagerShow() {
            //frmlar.SymbolDoc = tlVectorControl2.SVGDocument;
            //if (MapType == "所内接线图") {
            //    frmlar.Progtype = MapType;
            //} else {
            //    frmlar.Progtype = progtype;
            //}
            //frmlar.Owner = this;
            //frmlar.OnClickLayer += new OnClickLayerhandler(frmlar_OnClickLayer);
            //frmlar.OnDeleteLayer += new OnDeleteLayerhandler(frmlar_OnDeleteLayer);
            //// Init(progtype);
            //frmlar.ShowInTaskbar = false;
            //frmlar.Top = 25;//Screen.PrimaryScreen.WorkingArea.Height - 500;
            //frmlar.Left = Screen.PrimaryScreen.WorkingArea.Width - frmlar.Width;
            //frmlar.Show();
        }

        void frmlar_OnDeleteLayer(object sender) {
            SvgDocument.currentLayer = "";
            SetBarEnabed(false);
        }
        public void Open(string _SvgUID) {
            try {

                if (_SvgUID.Length < 20) {
                    JxtBar();
                    tlVectorControl2.ContextMenuStrip = null;
                }
                SVGFILE svgFile = new SVGFILE();
                svgFile.SUID = _SvgUID;
                SvgDocument document =CtrlSvgView.CashSvgDocument;
                if (document == null) {
                    IList svgList = Services.BaseService.GetList("SelectSVGFILEByKey", svgFile);
                    if (svgList.Count > 0) {
                        svgFile = (SVGFILE)svgList[0];
                    }

                    document = new SvgDocument();
                    if (!string.IsNullOrEmpty(svgFile.SVGDATA)) {
                        document.LoadXml(svgFile.SVGDATA);
                    }

                    document.FileName = svgFile.FILENAME;
                    document.SvgdataUid = svgFile.SUID;
                }
                SVGUID = document.SvgdataUid;

                img = document.SelectSingleNode("//*[@TLGH=\"1\"]");
                if (img != null) {
                    ((XmlElement)img).SetAttribute("xlink:href", " ");
                }
                this.Text = document.FileName;
                if (document.RootElement == null) {
                    tlVectorControl2.NewFile();
                    Layer.CreateNew("背景层", tlVectorControl2.SVGDocument);
                    Layer.CreateNew("城市规划层", tlVectorControl2.SVGDocument);
                    Layer.CreateNew("供电区域层", tlVectorControl2.SVGDocument);
                } else {
                    tlVectorControl2.SVGDocument = document;
                }
                tlVectorControl2.SVGDocument.SvgdataUid = SVGUID;
                tlVectorControl2.SVGDocument.FileName = this.Text;
                tlVectorControl2.DocumentbgColor = Color.White;
                tlVectorControl2.BackColor = Color.White;
                //tlVectorControl1.ForeColor = Color.White;
                CreateComboBox();
                xltProcessor = new XLTProcessor(tlVectorControl2);
                xltProcessor.MapView = mapview;
                xltProcessor.OnNewLine += new NewLineDelegate(xltProcessor_OnNewLine);
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        public void InitGroup() {
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
            SVGFILE svg_temp = new SVGFILE();
            string eleid = svg.SUID;
            string filename = svg.FILENAME;
            if (svglist.Count > 0)
            {
                svg_temp = (SVGFILE)svglist[0];
                sdoc = null;
                sdoc = new SvgDocument();
                sdoc.LoadXml(svg_temp.SVGDATA);                
                tlVectorControl2.SVGDocument = sdoc;
                tlVectorControl2.SVGDocument.SvgdataUid = svg_temp.SUID;
                
                MapType = "所内接线图";
            }
            else
            {
                tlVectorControl2.NewFile();
                tlVectorControl2.SVGDocument.SvgdataUid = eleid;
                //InitGroup();
                MapType = "所内接线图";
            }
            substation _s = new substation();
            _s.EleID = eleid;
            _s.SvgUID = ParentUID;
            substation _s1 = (substation)Services.BaseService.GetObject("SelectsubstationByEleID", _s);
            if (_s1 != null)
            {
                tlVectorControl2.SVGDocument.FileName = _s1.EleName;// +"主接线图";
                this.Text = _s1.EleName + "主接线图";
            }
            else
            {
                tlVectorControl2.SVGDocument.FileName = filename;
                this.Text =filename+ "主接线图";
            }
            ArrayList a = tlVectorControl2.SVGDocument.getLayerList();
            if (tlVectorControl2.SVGDocument.getLayerList().Count == 0)
            {
                Layer _lar = ItopVector.Core.Figure.Layer.CreateNew("接线图", tlVectorControl2.SVGDocument);
                _lar.SetAttribute("layerType", "所内接线图");
                _lar.Visible = true;
                SvgDocument.currentLayer = ((Layer)tlVectorControl2.SVGDocument.getLayerList()[0]).ID;
            }
            tlVectorControl2.ContextMenuStrip = null;
            CreateComboBox();
            InitJXT();
            LoadShape("symbol4.xml");
            LoadImage = false;
            bk1.Enabled = false;
            tlVectorControl2.ScaleRatio = 0.1F;
            tlVectorControl2.Refresh();
            // ButtonEnb(false);
        }
        public void InitJXT() {
            ArrayList layerlist = tlVectorControl2.SVGDocument.getLayerList();
            DevExpress.XtraEditors.Controls.CheckedListBoxItem[] chkItems = null;
            this.checkedListBoxControl1.Items.Clear();
            chkItems = new DevExpress.XtraEditors.Controls.CheckedListBoxItem[layerlist.Count];
            for (int j = 0; j < layerlist.Count; j++) {
                chkItems.SetValue(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(((Layer)layerlist[j]).Label), j);
                if (((Layer)layerlist[j]).Visible) {
                    chkItems[j].CheckState = CheckState.Checked;
                }
            }
            this.checkedListBoxControl1.Items.AddRange(chkItems);

            if (layerlist.Count > 0) {
                Layer lar = (Layer)layerlist[0];
                SvgDocument.currentLayer = lar.ID;
                popupContainerEdit1.Text = lar.Label;
                selLar = lar.Label;
            }
            this.popupContainerEdit1.Properties.PopupControl = this.popupContainerControl1;

        }
        public void Init(string stype) {
            ArrayList layerlist = tlVectorControl2.SVGDocument.getLayerList();
            ArrayList tmplaylist = new ArrayList();
            //DevExpress.XtraEditors.Controls.CheckedListBoxItem[] chkItems = null;
            //this.checkedListBoxControl1.Items.Clear();

            if (progtype == "地理信息层") {
                this.Text = "地理信息";
                for (int i = 0; i < layerlist.Count; i++) {
                    Layer lar = (Layer)layerlist[i];
                    if (lar.GetAttribute("layerType") == stype) {
                        tmplaylist.Add(layerlist[i]);
                    } else {
                        lar.Visible = false;
                    }
                }
                if (tmplaylist.Count > 0) {
                    DlBarVisible(true);
                } else {
                    DlBarVisible(false);
                }
            }
            if (progtype == "城市规划层") {
                this.Text = "城市规划";
                for (int i = 0; i < layerlist.Count; i++) {
                    Layer lar = (Layer)layerlist[i];
                    if (lar.GetAttribute("layerType") == "城市规划层" || lar.GetAttribute("layerType") == "地理信息层") {
                        tmplaylist.Add(layerlist[i]);
                    } else {
                        lar.Visible = false;
                    }
                }
                if (layerlist.Count > 0) {
                    Layer _nLayer = (Layer)layerlist[0];
                    if (_nLayer.GetAttribute("layerType") == progtype) {
                        DkBarVisible(true);
                    } else {
                        DkBarVisible(false);
                    }

                } else {
                    DkBarVisible(false);
                }
            }
            if (progtype == "电网规划层") {
                this.Text = "电网规划";
                for (int i = 0; i < layerlist.Count; i++) {
                    Layer lar = (Layer)layerlist[i];
                    tmplaylist.Add(layerlist[i]);
                }
                DwBarVisible(false);
                tlVectorControl2.CanEdit = false;
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
            if (MapType == "所内接线图") {
                JxtBar();
                LoadImage = false;
                bk1.Visible = false;
            }
        }
        void ctlfile_OnOpenSvgDocument(object sender, string _svgUid) {
            Open(_svgUid);
        }

        private void frmMain_Load(object sender, EventArgs e) {

            //System.Environment.
            //InitData();
            CreateComboBox();
            AddCombolScale();
            this.alignButton = this.dotNetBarManager2.GetItem("mAlign") as ButtonItem;
            this.orderButton = this.dotNetBarManager2.GetItem("mOrder") as ButtonItem;
            this.rotateButton = this.dotNetBarManager2.GetItem("mRotate") as ButtonItem;

            //ButtonItem b7 = dotNetBarManager2.GetItem("mEdit") as ButtonItem;
            //b7.Enabled = false;

            //tabStrip1.MdiForm = this;

        }
        string xltProcessor_OnNewLine(List<string> existLineCode) {

            return DateTime.Now.Minute.ToString();
        }
        public void InitScaleRatio() {
            tlVectorControl2.ScaleRatio = 0.1f;
            this.scaleBox.Text = "10%";
        }
        void ComboBoxEx_SelectedIndexChanged(object sender, EventArgs e) {
            Layer lar = (Layer)LayerBox.ComboBoxEx.SelectedItem;

            SvgDocument.currentLayer = lar.ID;
            lar.Visible = true;
            tlVectorControl2.SVGDocument.ClearSelects();
        }
        void ComboBoxScaleBox_SelectedIndexChanged(object sender, EventArgs e) {
            string text1 = this.scaleBox.SelectedItem.ToString();
            tlVectorControl2.ScaleRatio = ItopVector.Core.Func.Number.ParseFloatStr(text1);

        }
        #endregion



        #region 公用方法
        public void CreateComboBox() {
            ArrayList Layerlist = tlVectorControl2.SVGDocument.getLayerList();
            ArrayList tmplaylist = new ArrayList();
            DevExpress.XtraEditors.Controls.CheckedListBoxItem[] chkItems = null;
            this.checkedListBoxControl1.Items.Clear();

            for (int i = 0; i < Layerlist.Count; i++) {
                Layer lar = (Layer)Layerlist[i];
                if (lar.GetAttribute("layerType") == progtype || lar.GetAttribute("layerType") == MapType) {
                    tmplaylist.Add(Layerlist[i]);
                } else {
                    lar.Visible = false;
                }
            }
            chkItems = new DevExpress.XtraEditors.Controls.CheckedListBoxItem[tmplaylist.Count];
            for (int i = 0; i < tmplaylist.Count; i++) {
                chkItems.SetValue(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(((Layer)tmplaylist[i]).Label), i);
                if (((Layer)tmplaylist[i]).Visible) {
                    chkItems[i].CheckState = CheckState.Checked;
                }
            }
            if (tmplaylist.Count > 0) {
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

        void LayerBox_GotFocus(object sender, EventArgs e) {
            if (LayerCount != tlVectorControl2.SVGDocument.getLayerList().Count)
            {
                LayerBox = dotNetBarManager2.GetItem("ComLayer") as DevComponents.DotNetBar.ComboBoxItem;
                if (LayerBox != null)
                {
                    LayerBox.ComboBoxEx.Items.Clear();
                    ArrayList Layerlist = tlVectorControl2.SVGDocument.getLayerList();

                    LayerBox.ComboBoxEx.Items.Clear();


                    for (int i = 0; i < Layerlist.Count; i++)
                    {
                        LayerBox.Items.Add(Layerlist[i]);
                    }
                    if (Layerlist.Count > 0)
                    {
                        LayerBox.ComboBoxEx.SelectedItem = Layerlist[0];
                    }
                    LayerCount = tlVectorControl2.SVGDocument.getLayerList().Count;
                }
            }
        }
        private void AddCombolScale() {
            //缩放大小
            scaleBox = this.dotNetBarManager2.GetItem("ScaleBox") as DevComponents.DotNetBar.ComboBoxItem;
            if (scaleBox != null) {
                scaleBox.Items.AddRange(mapview.ScaleRange());
                scaleBox.ComboBoxEx.Text = "100%";
                scaleBox.ComboBoxEx.SelectedIndexChanged += new EventHandler(ComboBoxScaleBox_SelectedIndexChanged);

            }
        }

        public void LoadShape(string filename) {
            DockContainerItem dockitem = dotNetBarManager2.GetItem("DockContainerty") as DockContainerItem;
            symbolSelector = null;
            this.symbolSelector = new ItopVector.Selector.SymbolSelector(Application.StartupPath + "\\symbol\\" + filename);
            this.symbolSelector.Dock = DockStyle.Fill;
            tlVectorControl2.SymbolSelector = this.symbolSelector;
            dockitem.Control = this.symbolSelector;
            dockitem.Refresh();
            if (getlayer(selLar, tlVectorControl2.SVGDocument.getLayerList()) == null || getlayer(selLar, tlVectorControl2.SVGDocument.getLayerList()).GetAttribute("layerType") != progtype)
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
            tlVectorControl2.Location = new Point(176, 90);

            tlVectorControl2.Size = new Size((Screen.PrimaryScreen.Bounds.Width - 176), (Screen.PrimaryScreen.Bounds.Height - 158));


        }
        public void InitShape() {
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
        public void Save() {
            try {
                if (!tlVectorControl2.IsModified) return;
                tlVectorControl2.SVGDocument.Changed = true;
                if (tlVectorControl2.SVGDocument.SvgdataUid != string.Empty) {
                    IList svglist = Services.BaseService.GetList("SelectSVGFILEByKey", tlVectorControl2.SVGDocument.SvgdataUid);
                    if (svglist.Count > 0) {
                        svg = (SVGFILE)svglist[0];
                        svg.SVGDATA = tlVectorControl2.SVGDocument.OuterXml;
                        svg.FILENAME = tlVectorControl2.SVGDocument.FileName;
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

                    } else {
                        svg.SUID = tlVectorControl2.SVGDocument.SvgdataUid;
                        svg.FILENAME = tlVectorControl2.SVGDocument.FileName;
                        svg.SVGDATA = tlVectorControl2.SVGDocument.OuterXml;
                        if (MapType == "所内接线图") {
                            svg.PARENTID = "";
                        }
                        Services.BaseService.Create<SVGFILE>(svg);
                    }

                } else {
                    svg.SUID = Guid.NewGuid().ToString();
                    svg.FILENAME = tlVectorControl2.SVGDocument.FileName;
                    svg.SVGDATA = tlVectorControl2.SVGDocument.OuterXml;
                    Services.BaseService.Create<SVGFILE>(svg);
                    tlVectorControl2.SVGDocument.SvgdataUid = svg.SUID;

                }
                //this.Text = tlVectorControl1.SVGDocument.FileName;
                tlVectorControl2.IsModified = false;
                //CtrlSvgView.CashSvgDocument = tlVectorControl1.SVGDocument;
            } catch (Exception e1) {
                MessageBox.Show(e1.Message);
            }
        }
        public void InitData(IList svglist) {
            //svg.SUID = "1";
            //IList svglist = Services.BaseService.GetList("SelectSVGFILEByKey", svg);
            svg = (SVGFILE)svglist[0];
            sdoc = null;
            sdoc = new SvgDocument();
            //    sdoc.LoadXml(svg_temp.SVGDATA);
            sdoc.LoadXml(svg.SVGDATA);
            tlVectorControl2.SVGDocument = sdoc;
            ItopVector.SpecialCursors.LoadCursors();
            tlVectorControl2.PropertyGrid = propertyGrid;
            tlVectorControl2.SVGDocument.SvgdataUid = svg.SUID;
           // LoadShape("symbol2.xml");
        }
        public Layer getlayer(string LayerName, ArrayList LayerList) {
            Layer layer = null;
            for (int i = 0; i < LayerList.Count; i++) {
                if (LayerName == ((Layer)LayerList[i]).Label) {
                    layer = (Layer)LayerList[i];
                    break;
                }
            }
            return layer;
        }
        public static bool getlayer(string CurrLayerID, string LayerName, ArrayList LayerList) {
            Layer layer = null;
            string layerType = "";
            for (int i = 0; i < LayerList.Count; i++) {
                if (CurrLayerID == ((Layer)LayerList[i]).ID) {
                    layer = (Layer)LayerList[i];
                    layerType = layer.GetAttribute("layerType");
                    if (layerType == LayerName) {
                        return true;
                    }
                }
            }
            return false;
        }
        private void ImportDxf()
        {
            string strGUID = tlVectorControl2.SVGDocument.SvgdataUid;
            openFileDialog2.Filter = "AUTOCAD DXF文件 (*.dxf)(*.dwg)|*.dxf;*.dwg";
            if (openFileDialog2.ShowDialog(this) == DialogResult.OK)
            {
                // fileName = dlg.FileName;openFileDialog1.FileName
                // Itop.CADConvert.CADConvertHelper().ConvertToSvg(dlg.FileName)
                //string str = progtype;
                SvgDocument svg = new Itop.CADConvert.CADConvertHelper().ConvertToSvg(openFileDialog2.FileName);
                //frmMain.progtype = "";
                //tlVectorControl2.FullDrawMode = true;
                tlVectorControl2.SVGDocument = svg;
                tlVectorControl2.SVGDocument.SvgdataUid = strGUID;
                tlVectorControl2.SVGDocument.FileName = this.Text;
                CreateComboBox();
                tlVectorControl2.IsModified = true;
                bk1.Enabled = true;
                tlVectorControl2.Refresh(); 
                
                //progtype = str;
            }
        }

        public void Recalculate(decimal _viewScale) {
            decimal sum = 0;
            string svguid = tlVectorControl2.SVGDocument.SvgdataUid;
            XmlNodeList n1 = tlVectorControl2.SVGDocument.SelectNodes("svg/polygon [@IsArea='1']");

            Hashtable hs = new Hashtable();
            // hs.Add("ParentEleID", "1");
            hs.Add("SvgUID", svguid);
            IList dkList = Services.BaseService.GetList("SelectglebePropertParentIDSubAll", hs);
            Hashtable dkHs = new Hashtable(dkList.Count);
            for (int i = 0; i < dkList.Count; i++) {
                glebeProperty _gle = (glebeProperty)dkList[i];
                dkHs.Add(_gle.EleID, _gle.ObligateField10);
            }

            for (int j = 0; j < n1.Count; j++) {
                XmlElement _node1 = (XmlElement)n1.Item(j);
                string t = (string)dkHs[_node1.GetAttribute("id")];
                if (t != null) {
                    string color1 = ColorTranslator.ToHtml(Color.FromArgb(Convert.ToInt32(t)));
                    color1 = "fill:" + color1 + ";";
                    _node1.SetAttribute("style", color1);
                }
            }
            // 重新统计地块负荷
            Hashtable HashTable1 = new Hashtable();
            HashTable1.Add("SUID", svguid);
            Services.BaseService.Update("UpdateGlebePropertyAll", HashTable1);

            tlVectorControl2.Refresh();
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

        private void tlVectorControl1_MouseDown(object sender, MouseEventArgs e) {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = false;
            try {
                fInfo.Close();
                if (tlVectorControl2.IsModified) {
                    //if (MessageBox.Show("是否保存已做的修改?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    //{
                    Save();
                    //}
                }
              //  CtrlSvgView.MapType = MapType;

            } catch (Exception e1) { }
            
        }

        private void popupContainerEdit1_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e) {
            popupContainerEdit1.Text = selLar;
        }

        private void popupContainerEdit1_Click(object sender, EventArgs e) {
        }

        private void checkedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e) {
            try {
                selLar = checkedListBoxControl1.SelectedValue.ToString();
                Layer lar = getlayer(selLar, tlVectorControl2.SVGDocument.getLayerList());
                SvgDocument.currentLayer = lar.ID;
                if (checkedListBoxControl1.GetItemChecked(e.Index)) {
                    lar.Visible = true;
                } else {
                    lar.Visible = false;
                }
                if (lar.GetAttribute("layerType") == progtype) {
                    tlVectorControl2.CanEdit = true;
                } else {
                    tlVectorControl2.CanEdit = false;
                }
            } catch (Exception e1) { }
        }

        private void checkedListBoxControl1_Click(object sender, EventArgs e) {
            //try
            //{
            //    if (checkedListBoxControl1.SelectedValue != null)
            //    {
            //        selLar = checkedListBoxControl1.SelectedValue.ToString();
            //    }

            //}
            //catch (Exception e1) { }
        }

        public void SetBarEnabed(bool b) {
            if (progtype == "地理信息层") {
                DlBarVisible(b);
            }
            if (progtype == "城市规划层") {
                DkBarVisible(b);
            }
            if (progtype == "电网规划层") {
                DwBarVisible(b);
            }
            if (MapType == "所内接线图") {
                JxtBar();
            }
        }
        public void JxtBar() {
            dotNetBarManager2.Bars["bar6"].Visible = true;
            dotNetBarManager2.Bars["bar6"].Enabled = true;
            dotNetBarManager2.Bars["bar7"].Visible = false;
            dotNetBarManager2.Bars["bar8"].Visible = true;
            dotNetBarManager2.Bars["bar8"].GetItem("mEdit").Enabled = true;

            dotNetBarManager2.Bars["bar2"].GetItem("mFreeTransform").Enabled = true;
            dotNetBarManager2.Bars["bar2"].GetItem("mShapeTransform").Enabled = true;
            dotNetBarManager2.Bars["bar2"].GetItem("mLine").Enabled = true;
            dotNetBarManager2.Bars["bar2"].GetItem("mPolyline").Enabled = true;
            dotNetBarManager2.Bars["bar2"].GetItem("mAngleRectangle").Enabled = true;
            dotNetBarManager2.Bars["bar2"].GetItem("mEllipse").Enabled = true;
            dotNetBarManager2.Bars["bar2"].GetItem("mPolygon").Enabled = true;
            dotNetBarManager2.Bars["bar2"].GetItem("mBezier").Enabled = true;
            dotNetBarManager2.Bars["bar2"].GetItem("mImage").Enabled = true;
            dotNetBarManager2.Bars["bar2"].GetItem("mText").Enabled = true;
            propertyGrid.Enabled = true;
            symbolSelector.Enabled = true;
            bk1.Visible = false;
        }
        public void DlBarVisible(bool b) {
            dotNetBarManager2.Bars["bar6"].Enabled = b;
            dotNetBarManager2.Bars["bar8"].Visible = false;
            dotNetBarManager2.Bars["bar7"].Visible = false;
            dotNetBarManager2.Bars["bar2"].GetItem("mFreeTransform").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mShapeTransform").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mLine").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mPolyline").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mAngleRectangle").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mEllipse").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mPolygon").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mBezier").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mImage").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mText").Enabled = b;
            propertyGrid.Enabled = b;
            symbolSelector.Enabled = b;
            bk1.Visible = true;
        }
        public void DkBarVisible(bool b) {
            dotNetBarManager2.Bars["bar7"].Visible = true;
            dotNetBarManager2.Bars["bar6"].Enabled = b;
            dotNetBarManager2.Bars["bar8"].Visible = false;
            //dotNetBarManager1.Bars["bar7"].Enabled = b;
            dotNetBarManager2.Bars["bar7"].GetItem("mAreaPoly").Enabled = b;
            dotNetBarManager2.Bars["bar7"].GetItem("mLeadLine").Visible = false;
            dotNetBarManager2.Bars["bar7"].GetItem("mJQLeadLine").Visible = false;
            dotNetBarManager2.Bars["bar7"].GetItem("mFx").Visible = false;
            dotNetBarManager2.Bars["bar7"].GetItem("mFzzj").Visible = false;
            dotNetBarManager2.Bars["bar7"].GetItem("mReCompute").Visible = false;
            dotNetBarManager2.Bars["bar7"].GetItem("mPriQu").Visible = false;
            dotNetBarManager2.Bars["bar7"].GetItem("mFhbz").Visible = true;
            dotNetBarManager2.Bars["bar7"].GetItem("mFhbz").Enabled = b;

            dotNetBarManager2.Bars["bar2"].GetItem("mFreeTransform").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mShapeTransform").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mLine").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mPolyline").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mAngleRectangle").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mEllipse").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mPolygon").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mBezier").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mImage").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mText").Enabled = b;
            propertyGrid.Enabled = b;
            symbolSelector.Enabled = b;
            bk1.Visible = true;
        }
        public void DwBarVisible(bool b) {
            dotNetBarManager2.Bars["bar7"].Visible = true;
            dotNetBarManager2.Bars["bar8"].Visible = false;
            //dotNetBarManager1.Bars["bar8"].GetItem("mEdit").Enabled = false;
            dotNetBarManager2.Bars["bar6"].Enabled = b;
            dotNetBarManager2.Bars["bar7"].Enabled = b;
            dotNetBarManager2.Bars["bar7"].GetItem("mAreaPoly").Visible = false;
            dotNetBarManager2.Bars["bar7"].GetItem("mLeadLine").Enabled = b;
            dotNetBarManager2.Bars["bar7"].GetItem("mJQLeadLine").Enabled = b;
            dotNetBarManager2.Bars["bar7"].GetItem("mFx").Enabled = b;
            dotNetBarManager2.Bars["bar7"].GetItem("mFzzj").Enabled = b;
            dotNetBarManager2.Bars["bar7"].GetItem("mAreaPoly").Enabled = b;
            dotNetBarManager2.Bars["bar7"].GetItem("mReCompute").Enabled = b;
            dotNetBarManager2.Bars["bar7"].GetItem("mFhbz").Visible = false;

            dotNetBarManager2.Bars["bar2"].GetItem("mFreeTransform").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mShapeTransform").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mLine").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mPolyline").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mAngleRectangle").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mEllipse").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mPolygon").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mBezier").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mImage").Enabled = b;
            dotNetBarManager2.Bars["bar2"].GetItem("mText").Enabled = b;
            propertyGrid.Enabled = b;
            symbolSelector.Enabled = b;
            bk1.Visible = true;
        }
        public void ViewMenu() {
            dotNetBarManager2.Bars["mainmenu"].GetItem("ButtonItem2").Visible = false;
        }

        private void bk1_CheckedChanged(object sender, EventArgs e) {
            if (bk1.Checked == true) {
                LoadImage = true;
            } else {
                LoadImage = false;
            }
            //tlVectorControl1.BackColor = ColorTranslator.FromHtml("#C7CBE2");
            tlVectorControl2.Refresh();
        }

        private void jxtToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void tmLineConnect_Click(object sender, EventArgs e) {
            SvgElementCollection elements= tlVectorControl2.SVGDocument.SelectCollection;
            if (elements.Count == 2) {
                Polyline pl1 = elements[0] as Polyline;
                Polyline pl2 = elements[1] as Polyline;
                if (pl1 != null && pl2 != null && pl1.GetAttribute("IsLead") != "" && pl2.GetAttribute("IsLead") != "") {
                    PointF[] pfs1 = new PointF[2] { pl1.Points[0], pl1.Points[pl1.Points.Length - 1] };
                    PointF[] pfs2 = new PointF[2] { pl2.Points[0], pl2.Points[pl2.Points.Length - 1] };
                    PointF[] pfs3 = (PointF[])pl1.Points.Clone();
                    PointF[] pfs4 = (PointF[])pl2.Points.Clone();
                    pl1.Transform.Matrix.TransformPoints(pfs3);
                    pl2.Transform.Matrix.TransformPoints(pfs4);
                    ArrayList  list = new ArrayList();
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
                    double max = d[0];

                    for (int i = 1; i < 4; i++) {
                        if (max > d[i]) {
                            num = i;
                            max = d[i];
                        }
                    }
                    list.Clear();
                    switch (num) {
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
                    pl1.SetAttribute("Name",pl1.Name + "连接" + pl2.Name);
                    pl1.Points = (PointF[])list.ToArray(typeof(PointF));
                    ItopVector.Core.Types.Transf transf =new ItopVector.Core.Types.Transf(new Matrix());
                    pl1.Transform = transf;
                    pl2.ParentNode.RemoveChild(pl2);
                    elements.Remove(pl2);
                }
            }
        }
        
    }
}