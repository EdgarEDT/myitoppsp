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
    public partial class frmLayerManagerYear : FormBase
    {
        FlashWindow f = new FlashWindow();
        private ItopVector.Core.Document.SvgDocument symbolDoc;
        private string progtype = "";
        public string Key = "";
        public string docname = "";
        private ArrayList Layerlist;
        public frmLayerManagerYear(SvgDocument symbold, string prog)
        {
            symbolDoc = symbold;
            progtype = prog;
            InitializeComponent();
        }

        private void frmLayerManagerYear_Load(object sender, EventArgs e)
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
            //foreach (SvgElement g in layer.GraphList)
            //{
            //    layer2.GraphList.Add(g);
            //}

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
                IGraph graph = (IGraph)layer2.AddElement(temp);
                graph.Layer = layer2;

                LineInfo _line = new LineInfo();
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
                }
            }
            this.SymbolDoc.NotifyUndo();
            return layer2;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        public static bool IsNum(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < '0' || str[i] > '9')
                    return false;
            }
            return true;
        }

        private void frmLayerManagerYear_Load_1(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            if (symbolDoc != null)
            {
                XmlNodeList list1 = symbolDoc.GetElementsByTagName("layer");
                for (int num1 = 0; num1 < list1.Count; num1++)
                {
                    Layer element1 = list1[num1] as Layer;
                    string elename = element1.Label.ToString();
                    string elenameyear="";
                    if (elename.Length>=4)
                    elenameyear = elename.Substring(0, 4);
                    if (progtype == "电网规划层")
                    {
                        if ((!IsNum(elenameyear))||(elenameyear==""))
                        { }
                        else
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
                                    if (element1.GetAttribute("visibility") == "visible" || (element1.GetAttribute("ParentID") == ""))
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
                    }
                }
                Layerlist = symbolDoc.getLayerList();
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            string text1 = textEdit1.Text;
            string text2 = textEdit2.Text;
            if(textEdit1.Text==""){
                MessageBox.Show("请指定要复制的年份。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textEdit1.Focus();
                return;
            }
            if (textEdit2.Text == "")
            {
                MessageBox.Show("请指定要复制到的目标年份。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textEdit2.Focus();
                return;
            }
            LayerGrade gra = new LayerGrade();
            gra.Name = text2+"%";
            IList <LayerGrade> list= Services.BaseService.GetList<LayerGrade>("SelectLayerGradeByYear", gra);
            if(list.Count==0){
                MessageBox.Show("没有找到指定的年份信息，请先建立对应年份的图层分级。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            int itemcount = checkedListBox1.Items.Count;
            if (checkedListBox1.Items != null)
            {
                f.SetText("复制中，请等待......");
                f.Show();
                for (int num = 0; num < itemcount; num++)
                {
                    
                    
                    string itemname = checkedListBox1.Items[num].ToString();
                    string item4 = itemname.Substring(0, 4);
                    if ((item4 == text1) && (text2 != "") && checkedListBox1.GetItemChecked(num)==true)
                    {
                        Layer layer = this.checkedListBox1.Items[num] as Layer;
                        f.SetText("共选择" + checkedListBox1.CheckedItems.Count + "层，正在复制" + layer.Label + "层。");
                        string str = layer.GetAttribute("layerType");
                        Layer layer2 = CopyLayer2(layer, text2);
                        if (layer2 != null)
                        {
                            if (layer.Visible)
                            {
                                layer.Visible = false;
                                layer.Visible = true;
                            }
                            layer2.SetAttribute("layerType", str);
                            this.checkedListBox1.Items.Add(layer2);
                            layer2.Visible = false;

                            string xml = "";
                            XmlNodeList oldList = symbolDoc.SelectNodes("//*[@layer=\"" + layer2.ID + "\"]");
                            for (int i = 0; i < oldList.Count; i++)
                            {
                                xml = xml+((SvgElement)oldList[i]).OuterXml;
                            }
                            SVG_LAYER obj = new SVG_LAYER();
                            obj.SUID = layer2.ID;
                            obj.svgID = symbolDoc.SvgdataUid;
                            obj.NAME = layer2.Label;
                            obj.MDATE = System.DateTime.Now;
                            obj.OrderID = num + 10;
                            obj.YearID = ((LayerGrade)list[0]).SUID;
                            obj.layerType = "电网规划层";
                            obj.visibility = "visible";
                            obj.IsSelect = "true";
                            obj.XML = xml;
                            Services.BaseService.Create<SVG_LAYER>(obj);
                        }
                    }
                   
                }
                f.Hide();
            }
        }
        private Layer CopyLayer2(Layer layer,string textname)
        {
            string layerlabelf = layer.Label.Substring(4);
            string layer2name = textname + layerlabelf;
            int j=0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.Items[i].ToString() == layer2name)
                    j = 1;
                //else
                //    j = 0;
            }
            Layer layer2=null;
            if (j == 0)
            {
                layer2= Layer.CreateNew(textname + layerlabelf, this.SymbolDoc);
                this.SymbolDoc.NumberOfUndoOperations = (2 * layer2.GraphList.Count) + 200;
                SvgElementCollection sc = layer.GraphList;
                for (int i = layer.GraphList.Count - 1; i >= 0; i--)
                {
                    SvgElement element = sc[i] as SvgElement;
                    SvgElement temp = element.Clone() as SvgElement;
                    if (temp.GetAttribute("CopyOf") == "")
                    {
                        temp.SetAttribute("CopyOf", temp.ID);
                    }
                    IGraph graph = (IGraph)layer2.AddElement(temp);
                    graph.Layer = layer2;

                    //LineInfo _line = new LineInfo();
                    //_line.EleID = element.ID;
                    //_line.SvgUID = this.SymbolDoc.SvgdataUid;
                    //IList lineInfoList = Services.BaseService.GetList("SelectLineInfoByEleID", _line);
                    PSPDEV _line = new PSPDEV();
                    _line.EleID = element.ID;
                    _line.SvgUID = this.SymbolDoc.SvgdataUid;
                    IList lineInfoList = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandEleID", _line);

                    foreach (PSPDEV line in lineInfoList)
                    {
                        line.SUID = Guid.NewGuid().ToString();
                        line.LayerID = layer2.ID;
                        line.EleID = temp.ID;
                        Services.BaseService.Create<PSPDEV>(line);
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
                    }
                }
            }
            this.SymbolDoc.NotifyUndo();
            return layer2;
        }
    }
}