using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ItopVector.Core;
using ItopVector.Core.Figure;
using ItopVector.Core.Interface.Figure;
using ItopVector.Core.Document;
using System.Xml;
using Itop.Domain.Graphics;
using Itop.Domain.Stutistic;
using Itop.Client.Common;
using System.IO;
using System.Xml.XPath;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmLayerManager2 : FormBase
    {
        FlashWindow f = new FlashWindow();
        private ItopVector.Core.Document.SvgDocument symbolDoc;
        private string progtype = "";
        public string Key = "";
        public string docname = "";
        private ArrayList Layerlist;
        public frmLayerManager2(SvgDocument symbold,string prog)
        {
            symbolDoc = symbold;
            progtype = prog;
            InitializeComponent();
        }

           
        private void frmLayerManager2_Load(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            if (symbolDoc != null)
            {
                XmlNodeList list1 = symbolDoc.GetElementsByTagName("layer");
                for (int num1 = 0; num1 < list1.Count; num1++)
                {
                    Layer element1 = list1[num1] as Layer;
                    if (progtype == "地理信息层")
                    {
                        if (element1.GetAttribute("layerType") == progtype)
                        {
                            string strLayerID = element1.GetAttribute("id");
                            int n = this.checkedListBox1.Items.Add(element1, element1.Visible);
                            if (element1.Visible)
                            {
                                checkedListBox1.SetItemChecked(n, true);
                            }
                        }
                    }
                    if (progtype == "城市规划层")
                    {
                        if (element1.GetAttribute("layerType") == progtype || element1.GetAttribute("layerType") == "地理信息层")
                        {
                            string strLayerID = element1.GetAttribute("id");
                            int n = this.checkedListBox1.Items.Add(element1, element1.Visible);
                            if (element1.Visible)
                            {
                                checkedListBox1.SetItemChecked(n, true);
                            }
                        }
                    }
                    if (progtype == "电网规划层")
                    {
                        if (element1.GetAttribute("layerType") == progtype)
                        {
                            if (Key == "ALL")
                            {
                                string strLayerID = element1.GetAttribute("id");
                                int n = this.checkedListBox1.Items.Add(element1, element1.Visible);
                                if (element1.Visible)
                                {
                                    checkedListBox1.SetItemChecked(n, true);
                                }

                            }
                            else
                            {
                                if ((element1.GetAttribute("visibility") == "visible") || (element1.GetAttribute("ParentID") == ""))
                                {
                                    string strLayerID = element1.GetAttribute("id");
                                    int n = this.checkedListBox1.Items.Add(element1, element1.Visible);
                                    if (element1.Visible)
                                    {
                                        checkedListBox1.SetItemChecked(n, true);
                                    }
                                }
                            }
                        }
                        else
                        {
                            string strLayerID = element1.GetAttribute("id");
                            int n = this.checkedListBox1.Items.Add(element1, element1.Visible);
                            if (element1.Visible)
                            {
                                checkedListBox1.SetItemChecked(n, true);
                            }
                        }
                    }
                    if (progtype == "所内接线图")
                    {
                        if (element1.GetAttribute("layerType") == progtype)
                        {
                            string strLayerID = element1.GetAttribute("id");
                            int n = this.checkedListBox1.Items.Add(element1, element1.Visible);
                            if (element1.Visible)
                            {
                                checkedListBox1.SetItemChecked(n, true);
                            }
                        }
                    }
                }
                Layerlist = symbolDoc.getLayerList();
            }
        }
       

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }
        public ItopVector.Core.Document.SvgDocument SymbolDoc
        {
            get
            {
                return this.symbolDoc;
            }
            set
            {
                this.symbolDoc = value;
            }
        }
        private Layer CopyLayer(Layer layer)
        {
            Layer layer2 = Layer.CreateNew(layer.Label + " 副本", this.SymbolDoc);
            layer2.SetAttribute("ParentID", layer.GetAttribute("ParentID"));
            //foreach (SvgElement g in layer.GraphList)
            //{
            //    layer2.GraphList.Add(g);
            //}
            SVG_LAYER la = new SVG_LAYER();
            la.SUID = layer.ID;
            la.svgID = symbolDoc.SvgdataUid;
            la = (SVG_LAYER)Services.BaseService.GetObject("SelectSVG_LAYERByKey", la);
            if (la != null)
            {
                la.SUID = layer2.ID;
                la.NAME = layer2.Label;
                Services.BaseService.Create<SVG_LAYER>(la);
            }
            this.SymbolDoc.NumberOfUndoOperations = (2 * layer2.GraphList.Count) + 200;
            SvgElementCollection sc = layer.GraphList;
            for (int i = layer.GraphList.Count - 1; i >= 0; i--)
            {
                SvgElement element = sc[i] as SvgElement;
                SvgElement temp = element.Clone() as SvgElement;
                //if (temp.Name=="use"){
                if (temp.GetAttribute("CopyOf") == "")
                {
                    temp.SetAttribute("CopyOf", temp.ID);
                }
                //else
                //{
                //    temp.SetAttribute("CopyOf", temp.ID);
                //}
                //}
                IGraph graph = (IGraph)layer2.AddElement(temp);
                graph.Layer = layer2;

               /* LineInfo _line = new LineInfo();
                _line.EleID = element.ID;
                _line.SvgUID = this.SymbolDoc.SvgdataUid;
                IList lineInfoList = Services.BaseService.GetList("SelectLineInfoByEleID", _line);
                foreach (LineInfo line in lineInfoList)
                {
                    line.UID = Guid.NewGuid().ToString();
                    line.LayerID = layer2.ID;
                    line.EleID = temp.ID;
                    Services.BaseService.Create<LineInfo>(line);
                }
                glebeProperty gle = new glebeProperty();
                gle.EleID = element.ID;
                gle.SvgUID = this.SymbolDoc.SvgdataUid;
                IList gleProList = Services.BaseService.GetList("SelectglebePropertyByEleID", gle);
                foreach (glebeProperty gleP in gleProList)
                {
                    gleP.UID = Guid.NewGuid().ToString();
                    gleP.LayerID = layer2.ID;
                    gleP.EleID = temp.ID;
                    Services.BaseService.Create<glebeProperty>(gleP);
                }
                substation _sub = new substation();
                _sub.EleID = element.ID;
                _sub.SvgUID = this.SymbolDoc.SvgdataUid;
                IList substationList = Services.BaseService.GetList("SelectsubstationByEleID", _sub);
                foreach (substation sub in substationList)
                {
                    sub.UID = Guid.NewGuid().ToString();
                    sub.LayerID = layer2.ID;
                    sub.EleID = temp.ID;
                    Services.BaseService.Create<substation>(sub);
                }*/
            }
            this.SymbolDoc.NotifyUndo();
            return layer2;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //checkedListBox1.CheckedItems
            checkedListBox1.Focus();
            //int index = this.checkedListBox1.SelectedIndex;
            //if (index > 0)
            //{
                f.SetText("复制中，请等待......");
                f.Show();
                for (int temp = 0; temp < checkedListBox1.CheckedItems.Count; temp++)
                {
                    //Layer layer = this.checkedListBox1.Items[index] as Layer;
                    
                    Layer layer = this.checkedListBox1.CheckedItems[temp] as Layer;
                    string str = layer.GetAttribute("layerType");
                    Layer layer2 = CopyLayer(layer);
                    f.SetText("共选择"+checkedListBox1.CheckedItems.Count+"层，正在复制" + layer.Label + "层。");
                    if (layer.Visible)
                    {
                        layer.Visible = false;
                        layer.Visible = true;
                    }
                    layer2.SetAttribute("layerType", str);
                    this.checkedListBox1.Items.Add(layer2);
                    layer2.Visible = false;
                }
                f.Hide();
            //}
        }
    }
}