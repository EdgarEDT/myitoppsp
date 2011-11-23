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
using Itop.Common;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public delegate void OnClickLayerhandler(object sender, Layer lar);
    public delegate void OnDeleteLayerhandler(object sender); 
    public partial class frmConvert : FormBase
    {
        private ItopVector.Core.Document.SvgDocument symbolDoc;
        SVGFILE svg = new SVGFILE();
        SvgDocument sdoc = new SvgDocument();
        private string progtype = "";
        public string LayerName = "";
        public string Key = "";
        public string Progtype
        {
            get { return progtype; }
            set { progtype = value; }
        }
        public frmConvert()
        {
            InitializeComponent();
        }
        public void InitData()
        {
            IList svglist = Services.BaseService.GetList("SelectLayerFileList", svg);
            LayerGrade lg = new LayerGrade();
            IList ilist = Services.BaseService.GetList("SelectLayerGradeList", lg);
            DataTable dataTable = DataConverter.ToDataTable(ilist, typeof(LayerGrade));
            SVG_LAYER sl = new SVG_LAYER();
            IList list = Services.BaseService.GetList("SelectSVG_LAYERList", sl);
            treeList1.DataSource = dataTable;
            string ide = treeList1.Nodes[0].GetValue(0).ToString();//获取某一节点的值
            foreach (SVG_LAYER lay in list)
            {
                if (lay.layerType == "电网规划层")
                {
                    LayerGrade obj = new LayerGrade();
                    obj.SUID = lay.SUID;
                    obj.Name = lay.NAME;
                    obj.ParentID = lay.YearID;
                    string tempobj = "";
                    string tempyear = "";
                    if (obj.Name.ToString().Length >= 3)
                    {
                        tempobj = obj.Name.ToString().Substring(obj.Name.ToString().Length - 3, 3);
                    }
                    if (obj.Name.ToString().Length >= 4)
                    {
                        tempyear = obj.Name.ToString().Substring(0, 4);
                    }
                    if (tempobj == "变电站")
                    {
                        for (int j = 0; j < treeList1.Nodes.Count; j++)
                        {
                            if (treeList1.Nodes[j].GetValue(0).ToString().Length > 3)
                            {
                                if (treeList1.Nodes[j].GetValue(0).ToString().Substring(0, 4) == tempyear)
                                {
                                    substation _substat = new substation();
                                    _substat.LayerID = obj.SUID;
                                    _substat.ObligateField1 = "220";
                                    IList subList = Services.BaseService.GetList("SelectsubstationByLayerIDandObligateField1", _substat);

                                    int templ = treeList1.Nodes[j].Nodes.Count;
                                    treeList1.AppendNode(obj.Name, treeList1.Nodes[j]);
                                    treeList1.Nodes[j].Nodes[templ].SetValue(0, obj.Name);
                                    if (subList.Count != 0)
                                    {
                                        int jj = 0;
                                        foreach (substation temps in subList)
                                        {
                                            treeList1.AppendNode(temps, treeList1.Nodes[j].Nodes[templ]);
                                            treeList1.Nodes[j].Nodes[templ].Nodes[jj].SetValue(0, temps.EleName);
                                            jj++;
                                        }
                                    }
                                    _substat.ObligateField1 = "110";
                                    subList = Services.BaseService.GetList("SelectsubstationByLayerIDandObligateField1", _substat);

                                    templ = treeList1.Nodes[j].Nodes.Count;
                                    treeList1.AppendNode(obj.Name, treeList1.Nodes[j]);
                                    treeList1.Nodes[j].Nodes[templ].SetValue(0, obj.Name);
                                    if (subList.Count != 0)
                                    {
                                        int jj = 0;
                                        foreach (substation temps in subList)
                                        {
                                            treeList1.AppendNode(temps, treeList1.Nodes[j].Nodes[templ]);
                                            treeList1.Nodes[j].Nodes[templ].Nodes[jj].SetValue(0, temps.EleName);
                                            jj++;
                                        }
                                    }
                                    _substat.ObligateField1 = "66";
                                    subList = Services.BaseService.GetList("SelectsubstationByLayerIDandObligateField1", _substat);

                                    templ = treeList1.Nodes[j].Nodes.Count;
                                    treeList1.AppendNode(obj.Name, treeList1.Nodes[j]);
                                    treeList1.Nodes[j].Nodes[templ].SetValue(0, obj.Name);
                                    if (subList.Count != 0)
                                    {
                                        int jj = 0;
                                        foreach (substation temps in subList)
                                        {
                                            treeList1.AppendNode(temps, treeList1.Nodes[j].Nodes[templ]);
                                            treeList1.Nodes[j].Nodes[templ].Nodes[jj].SetValue(0, temps.EleName);
                                            jj++;
                                        }
                                    }
                                    _substat.ObligateField1 = "500";
                                    subList = Services.BaseService.GetList("SelectsubstationByLayerIDandObligateField1", _substat);

                                    templ = treeList1.Nodes[j].Nodes.Count;
                                    treeList1.AppendNode(obj.Name, treeList1.Nodes[j]);
                                    treeList1.Nodes[j].Nodes[templ].SetValue(0, obj.Name);
                                    if (subList.Count != 0)
                                    {
                                        int jj = 0;
                                        foreach (substation temps in subList)
                                        {
                                            treeList1.AppendNode(temps, treeList1.Nodes[j].Nodes[templ]);
                                            treeList1.Nodes[j].Nodes[templ].Nodes[jj].SetValue(0, temps.EleName);
                                            jj++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } 
            Application.DoEvents();
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
        private void frmConvert_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void frmConvert_Load_1(object sender, EventArgs e)
        {
            InitData();
        }
    }
}