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
using System.Text.RegularExpressions;

using Itop.Client.Base;

namespace ItopVector.Tools
{
    public delegate void OnClickLayerhandler(object sender, Layer lar);
    public delegate void OnDeleteLayerhandler(object sender);
    public delegate void OnCheckhandler(object sender);     
    public partial class frmLayerManager : FormBase
    {
        public bool spatialflag=true;
        FlashWindow f = new FlashWindow();
        public event OnClickLayerhandler OnClickLayer;
        public event OnDeleteLayerhandler OnDeleteLayer;
        public event OnCheckhandler OnCheck;
        private ItopVector.Core.Document.SvgDocument symbolDoc;
        private ItopVector.Core.Document.SvgDocument symbolDoc2;
        private ArrayList Layerlist;
        private string progtype = "";
        public string LayerName = "";
        public string Key = "";
        public static ArrayList ilist = new ArrayList();
        public ArrayList NoSave = new ArrayList();
        public string Progtype
        {
            get { return progtype; }
            set { progtype = value; }
        }

        public string StrYear
        {
            set { 
                dateEdit1.Text = value;
                dateEdit2.Text = value; 
            }
        }
        public frmLayerManager()
        {
            InitializeComponent();

        }
        public void Readonly()
        {
            btAdd.Enabled=false;
            btEdit.Enabled=false;
            btDel.Enabled=false;
            simpleButton3.Enabled=false;
            simpleButton4.Enabled=false;
            simpleButton1.Enabled=false;
            simpleButton2.Enabled = false;
            button1.Enabled = false;
            checkEdit1.Enabled = false;
            dateEdit1.Enabled = false;
            dateEdit2.Enabled = false;
            button2.Enabled = false;
        }
        public void InitData()
        {
            checkedListBox1.Items.Clear();
            if (symbolDoc != null)
            {
                symbolDoc2 = symbolDoc;
                XmlNodeList list1 = symbolDoc.GetElementsByTagName("layer");
                for (int num1 = 0; num1 < list1.Count; num1++)
                {
                    Layer element1 = list1[num1] as Layer;
                    if (progtype == "������Ϣ��")
                    {
                        if (element1.GetAttribute("layerType")==progtype)
                        {
                            string strLayerID = element1.GetAttribute("id");
                            int n=this.checkedListBox1.Items.Add(element1, element1.Visible);                          
                            if (element1.Visible)
                            {
                                checkedListBox1.SetItemChecked(n, true);
                            }
                        }
                    }
                    if (progtype == "���й滮��")
                    {
                        if (spatialflag)
                        {
                            if (element1.GetAttribute("layerType") == progtype || element1.GetAttribute("layerType") == "������Ϣ��") {
                                string strLayerID = element1.GetAttribute("id");
                                int n = this.checkedListBox1.Items.Add(element1, element1.Visible);
                                if (element1.Visible) {
                                    checkedListBox1.SetItemChecked(n, true);
                                }
                            }

                        }
                        else
                        {
                            string strLayerID = element1.GetAttribute("id");
                            int n = this.checkedListBox1.Items.Add(element1, element1.Visible);
                            if (element1.Visible) {
                                checkedListBox1.SetItemChecked(n, true);
                            }
                        }
                        
                    }
                    if (progtype == "�����滮��")
                    {
                        
                                    bool ck=false;
                                    if ((element1.GetAttribute("visibility") == "visible"))
                                    {
                                        ck=true;
                                    }
                                    string strLayerID = element1.GetAttribute("id");
                                    int n = this.checkedListBox1.Items.Add(element1, element1.Visible);
                                    if (element1.Visible)
                                    {
                                        checkedListBox1.SetItemChecked(n,ck);
                                    }
                         
                    }
                    if (progtype == "���ڽ���ͼ")
                    {
                        if (element1.GetAttribute("layerType") == progtype)
                        {
                        string strLayerID = element1.GetAttribute("id");
                        int n = this.checkedListBox1.Items.Add(element1, element1.Visible);
                        if (element1.Visible)
                        {
                            checkedListBox1.SetItemChecked(n, true);
                        }
                        //if (strLayerID == SvgDocument.currentLayer)
                        //{
                        //    this.checkedListBox1.SelectedIndex = num1;
                        //}
                        }
                    }
                }
                Layerlist = symbolDoc.getLayerList();
            }
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (this.Owner is frmMain)
                tmClearLink.Visible = false;
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

        private void btAdd_Click(object sender, EventArgs e)
        {
            frmInput dlg = new frmInput();
            dlg.InputType = progtype;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (Layer.CkLayerExist(dlg.InputString, this.SymbolDoc))
                {
                    MessageBox.Show("�ĵ����Ѿ�����ͬ��ͼ�㡣", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Layer lar = Layer.CreateNew(dlg.InputString, this.SymbolDoc);
                lar.SetAttribute("layerType", dlg.InputType);
                if (ilist.Count>0){
                    lar.SetAttribute("ParentID", ilist[0].ToString());
                }
                this.checkedListBox1.Items.Add(lar, true);
                checkedListBox1.SelectedIndex = -1;
                //string guid=Guid.NewGuid().ToString();
                //GraPowerRelation gra = new GraPowerRelation();
                //gra.UID = Guid.NewGuid().ToString();
                //gra.PowerEachID = guid;
                //gra.LayerID = lar.ID;
                //Services.BaseService.Create<GraPowerRelation>(gra);

                //SVG_LAYER layer = new SVG_LAYER();
                //layer.SUID = lar.ID;
                //layer.svgID = symbolDoc.SvgdataUid;
                //layer.NAME = lar.Label;
                //layer.XML = txt;
                //layer.MDATE = System.DateTime.Now;
                //layer.OrderID = list.IndexOf(lar);
                //layer.YearID = lar.GetAttribute("ParentID");
                //layer.IsChange = lar.GetAttribute("IsChange");
                //layer.visibility = lar.GetAttribute("visibility");
                //layer.layerType = lar.GetAttribute("layerType");
                //layer.IsSelect = lar.GetAttribute("IsSelect");
                //Services.BaseService.Create<SVG_LAYER>(_svg);

            }		
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (this.checkedListBox1.SelectedIndex >= 0 && this.checkedListBox1.SelectedIndex < this.checkedListBox1.Items.Count)
            {
                Layer layer = this.checkedListBox1.Items[this.checkedListBox1.SelectedIndex] as Layer;
                if (!CkRight(layer))
                {
                    MessageBox.Show("����ͼ�㲻�ܸ�����ɾ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("ɾ��ͼ���ʹ��ǰͼ�����������ݶ�ʧ���Ҳ��ɻָ���ȷ��Ҫɾ����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                //    return;
                //}
                //if (MessageBox.Show(this, "�Ƿ�ɾ��ͼ�㣺" + layer.Label + "?", "��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{                 
                    XmlNode node = this.SymbolDoc.SelectSingleNode("//*[@layer='" + layer.ID + "']");
                    if (node != null)
                    {

                        if ((MessageBox.Show(this, "��ͼ������ͼԪ,�Ƿ�ɾ��ͼ�㣺" + layer.Label + "?", "��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes))
                        {
                            PSPDEV _line = new PSPDEV();
                            _line.LayerID = layer.ID;
                            Services.BaseService.Update("DeletePSPDEVbyLayerID", _line);
                            glebeProperty gle = new glebeProperty();
                            gle.LayerID = layer.ID;
                            Services.BaseService.Update("DeleteglebePropertyByLayerID", gle);
                            PSP_Substation_Info _sub = new PSP_Substation_Info();
                            _sub.LayerID = layer.ID;
                            Services.BaseService.Update("DeletePSP_Substation_InfoByLayerID", _sub);

                            SVG_LAYER lar = new SVG_LAYER();
                            lar.SUID=layer.ID;
                            Services.BaseService.Update("DeleteSVG_LAYER", lar);
                            XmlNodeList list = this.SymbolDoc.SelectNodes("//*[@layer='" + layer.ID + "']");
                            foreach (XmlNode elNode in list)
                            {
                                this.SymbolDoc.RootElement.RemoveChild(elNode);
                            }
                            // Services.BaseService.Update("UpdateGraPowerRelationByLayerID", layer.ID);
                            //���ĵ����Ƴ�
                            layer.Remove();
                            //���б����Ƴ�
                            this.checkedListBox1.Items.Remove(layer);
                            layer = null;
                            LayerName = "";
                        }
                        //layer.Attributes.RemoveAll();
                    }
                    else
                    {
                        // Services.BaseService.Update("UpdateGraPowerRelationByLayerID", layer.ID);
                        //���ĵ����Ƴ�
                        SVG_LAYER lar = new SVG_LAYER();
                        lar.SUID = layer.ID;
                        Services.BaseService.Update("DeleteSVG_LAYER", lar);
                        try
                        {
                            layer.Remove();
                        }
                        catch { }
                        //���б����Ƴ�
                        this.checkedListBox1.Items.Remove(layer);
                        layer = null;
                        LayerName = "";
                    }                        
                }
                //if(this.checkedListBox1.Items.Count<1){
                    if(OnDeleteLayer!=null){
                        OnDeleteLayer(sender);
                    }
                //}
            }
        }

        private void btEdit_Click(object sender, EventArgs e)//
        {
            if (this.checkedListBox1.SelectedIndex >= 0 && this.checkedListBox1.SelectedIndex < this.checkedListBox1.Items.Count)
            {
                Layer layer = this.checkedListBox1.Items[this.checkedListBox1.SelectedIndex] as Layer;
                if (!CkRight(layer))
                {
                    MessageBox.Show("����ͼ�㲻�ܸ�����ɾ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmInput dlg = new frmInput();
                dlg.id = symbolDoc.SvgdataUid;
                dlg.symbolDoc = symbolDoc;
                dlg.InputString = layer.Label;
                dlg.InputType = layer.GetAttribute("layerType");

                DialogResult d=dlg.ShowDialog(this);
                if (d == DialogResult.OK)
                {
                    layer.Label = dlg.InputString;            
                    layer.SetAttribute("layerType", dlg.InputType);
                    InitData();
                }
                if(d==DialogResult.Retry){
                    if (dlg.list.Count > 1)
                    {
                        layer.SetAttribute("ParentID", dlg.list[1].ToString());
                        SVG_LAYER temp = new SVG_LAYER();
                        temp.SUID= layer.ID;
                        temp.svgID=symbolDoc.SvgdataUid;
                        SVG_LAYER lar =(SVG_LAYER) Services.BaseService.GetObject("SelectSVG_LAYERByKey",temp);
                        lar.YearID = dlg.list[1].ToString();
                        Services.BaseService.Update<SVG_LAYER>(lar);
                    }
                }
                
            }
        }
        private bool CkRight(Layer lar)
        {
            if (lar.Label == "������0"  /*|| lar.Label=="�滮��" || lar.Label=="ͳ�Ʋ�"*/)
            {
                return false;
            }
#if(!CITY)
            if (progtype == "�����滮��") {
                string ltype = lar.GetAttribute("layerType");
                if (ltype == "������Ϣ��" || ltype == "���й滮��")
                    return false;
            }           
#endif      
            return true;
            
        }

        private void frmLayerManager_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                (this.checkedListBox1.Items[i] as Layer).Visible = this.checkedListBox1.GetItemChecked(i);
            }
            if (checkedListBox1.SelectedItem != null)
            {
                SvgDocument.currentLayer = (checkedListBox1.SelectedItem as Layer).ID;
                LayerName = (checkedListBox1.SelectedItem as Layer).Label;
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            int i = this.checkedListBox1.SelectedIndex;
            if (i == 0) { return; }
            Layer layer = this.checkedListBox1.Items[this.checkedListBox1.SelectedIndex] as Layer;            
        }

        private void btDown_Click(object sender, EventArgs e)
        {

        }
        public void LayerUpdate()
        {
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public string getSelectedLayer()
        {
            string strLayer = "";
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    Layer layer = checkedListBox1.Items[i] as Layer;
                    strLayer = strLayer + layer.ID + ",";
                }
            }
            return strLayer;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
  
            if (this.checkedListBox1.SelectedIndex!=-1)
            {
                Layer layer = this.checkedListBox1.Items[this.checkedListBox1.SelectedIndex] as Layer;
                if (checkedListBox1.GetItemCheckState(checkedListBox1.SelectedIndex) == CheckState.Checked)
                {
                    layer.Visible = false;
                }
                else
                {
                    layer.Visible = true;
                }               
            }
           
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(checkedListBox1.SelectedIndex!=-1){
                Layer layer = this.checkedListBox1.Items[this.checkedListBox1.SelectedIndex] as Layer;
                if (OnClickLayer != null)
                {
                    OnClickLayer(sender, layer);
                }
            }
        }

        private void frmLayerManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            int index = this.checkedListBox1.SelectedIndex;
            if (index > 0) {
                Layer layer = this.checkedListBox1.Items[index] as Layer;                
                layer.GoUp();
                this.checkedListBox1.Items.RemoveAt(index);
                this.checkedListBox1.Items.Insert(index - 1, layer);
                this.checkedListBox1.SetSelected(index - 1, true);
                if (layer.Visible) {
                    this.checkedListBox1.SetItemChecked(index - 1, true);
                    layer.Visible = false;
                    layer.Visible = true;
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e) {
            int index = this.checkedListBox1.SelectedIndex;
            if ( index>=0 && index <checkedListBox1.Items.Count -1) {
                Layer layer = this.checkedListBox1.Items[index] as Layer;
                layer.GoDown();
                this.checkedListBox1.Items.RemoveAt(index);
                this.checkedListBox1.Items.Insert(index + 1, layer);
                this.checkedListBox1.SetSelected(index + 1, true);
                if (layer.Visible) {
                    this.checkedListBox1.SetItemChecked(index + 1, true);
                    layer.Visible = false;
                    layer.Visible = true;
                }
            }
        }
       
        private void simpleButton3_Click(object sender, EventArgs e) {//���ư�ť
            if (checkedListBoxControl2.Visible == false)
            {
                //checkedListBoxControl2.
                checkedListBoxControl2.Items[0].CheckState = CheckState.Unchecked;
                checkedListBoxControl2.Items[1].CheckState = CheckState.Unchecked;
                checkedListBoxControl2.Items[2].CheckState = CheckState.Unchecked;
                checkedListBoxControl2.Visible = true;
            }
            else
                checkedListBoxControl2.Visible = false;
            //int index = this.checkedListBox1.SelectedIndex;
            // if (index > 0) {
            //     Layer layer = this.checkedListBox1.Items[index] as Layer;
            //     string str = layer.GetAttribute("layerType");
            //     Layer layer2 = CopyLayer(layer);
            //     if (layer.Visible) {
            //         layer.Visible = false;
            //         layer.Visible = true; 
            //     }
            //     layer2.SetAttribute("layerType", str);
            //     this.checkedListBox1.Items.Add(layer2);
            //     layer2.Visible = false;
            // }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            int index = this.checkedListBox1.SelectedIndex;
            if (index > 0)
            {
                ArrayList layerlist1 =  this.SymbolDoc.getLayerList();
                string str1 = null;
                string str2 = null;
                foreach (Layer l in layerlist1)
                {
                    if (l.Visible)
                    {
                        str1 += l.Label;
                        str2 += l.Label + "��";
                    }
                }                
                if (MessageBox.Show(this, "�˲������ϲ��ɼ�ͼ�㣺" + str2 +"���Ҳ��ɻָ����Ƿ������", "��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Layer layer = this.checkedListBox1.Items[index] as Layer;
                    string str = layer.GetAttribute("layerType");
                    Layer layer2=Layer.CreateNew(str1,this.SymbolDoc);
                    if (ilist.Count > 0) {
                        layer2.SetAttribute("ParentID", ilist[0].ToString());
                    }
                    foreach (Layer layer1 in layerlist1)
                    {
                        if (layer1.Visible)
                        {
                            //foreach (SvgElement g in layer1.GraphList)
                            //{
                            //    layer2.GraphList.Add(g);
                            //}
                            this.SymbolDoc.NumberOfUndoOperations = (2 * layer1.GraphList.Count) + 200;
                            SvgElementCollection sc = layer1.GraphList;
                            for (int i = layer1.GraphList.Count - 1; i >= 0; i--)
                            {
                                SvgElement element = sc[i] as SvgElement;
                                SvgElement temp = element.Clone() as SvgElement;
                                IGraph graph = (IGraph)layer2.AddElement(temp);
                                graph.Layer = layer2;

                                PSPDEV _line = new PSPDEV();
                                _line.EleID = element.ID;
                                _line.SvgUID = this.SymbolDoc.SvgdataUid;
                                IList lineInfoList = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandEleID", _line);
                                foreach (PSPDEV line in lineInfoList)
                                {
                                    line.LayerID = layer2.ID;
                                    line.EleID = temp.ID;
                                    Services.BaseService.Update<PSPDEV>(line);
                                }
                                glebeProperty gle = new glebeProperty();
                                gle.EleID = element.ID;
                                gle.SvgUID = this.SymbolDoc.SvgdataUid;
                                IList gleProList = Services.BaseService.GetList("SelectglebePropertyByEleID", gle);
                                foreach (glebeProperty gleP in gleProList)
                                {
                                    gleP.LayerID = layer2.ID;
                                    gleP.EleID = temp.ID;
                                    Services.BaseService.Update("UpdateglebeProperty", gle);
                                }
                                PSP_Substation_Info _sub = new PSP_Substation_Info();
                                _sub.EleID = element.ID;
                                _sub.AreaID = this.SymbolDoc.SvgdataUid;
                                IList substationList = Services.BaseService.GetList("SelectPSP_Substation_InfoListByEleID", _sub);
                                foreach (PSP_Substation_Info sub in substationList)
                                {
                                    sub.LayerID = layer2.ID;
                                    sub.EleID = temp.ID;
                                    Services.BaseService.Update<PSP_Substation_Info> (sub);
                                }
                            } 
                        }
                    }

                    this.SymbolDoc.NotifyUndo();
                    layer2.SetAttribute("layerType", str); 
                    //layer2.Label = str1;
                    this.checkedListBox1.Items.Add(layer2);
                    layer2.Visible = false;
                    foreach (Layer layer3 in layerlist1)
                    {
                        if (layer3.Visible) 
                        {
                            DeleteLayer(layer3); 
                        }
                    }
                }
            }
        }  
        //private void simpleButton5_Click(object sender, EventArgs e)
        //{
        //    int index = this.checkedListBox1.SelectedIndex;
        //    if (index>0)
        //    {
        //        Layer layer = this.checkedListBox1.Items[index] as Layer;
        //        LayerFile temp = new LayerFile();
        //        temp.SvgDataUid = this.symbolDoc.SvgdataUid;
        //        IList lList = Services.BaseService.GetList("SelectLayerFileBySvgDataUid", temp);
        //        foreach (LayerFile lay in lList)
        //        {
        //            if (lay.LayerFileName==layer.Label)
        //            {
        //                MessageBox.Show("�ĵ����Ѿ�����ͬ��ͼ��,���޸�ͼ�����ƺ󵼳���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }
        //        }
        //        string strsvgData = this.symbolDoc.SelectNodesToString("svg/*[@layer='" + layer.ID + "']|svg/defs");
        //        LayerFile layerFile = new LayerFile();
        //        layerFile.SUID = Guid.NewGuid().ToString();
        //        layerFile.LayerID = layer.ID;
        //        layerFile.LayerFileName = layer.Label;
        //        layerFile.SvgDataUid = this.symbolDoc.SvgdataUid;
        //        layerFile.LayerOuterXml = strsvgData;                
        //        Services.BaseService.Create<LayerFile>(layerFile);
        //        DeleteLayer(layer);                
        //    }
           
        //    //StreamWriter sw = new StreamWriter("c:\\1.xml");
        //    //sw.Write(str1);
        //    //sw.Close();
        //    //sw = new StreamWriter("c:\\2.txt");
        //    //sw.Write(str2);
        //    //sw.Close();
        //    //sw = new StreamWriter("c:\\3.xml");
        //    //sw.Write(str3);
        //    //sw.Close();
        //    //sw = new StreamWriter("c:\\4.txt");
        //    //sw.Write(str4);
        //    //sw.Close();
        //    //sw = new StreamWriter("c:\\5.xml");
        //    //sw.Write(newOutXml);
        //    //sw.Close();
        //    //string str2 = this.SymbolDoc.SvgdataUid;
        //    //SVGFILE svg = new SVGFILE();
        //    //svg = (SVGFILE)svglist[0];
        //    //svg.SVGDATA = this.SymbolDoc.OuterXml;
        //    //Services.BaseService.Update<SVGFILE>(svg);
        //}       
        private Layer CopyLayer(Layer layer)
        {
            Layer layer2 = Layer.CreateNew(layer.Label + " ����", this.SymbolDoc);
            layer2.SetAttribute("ParentID", layer.GetAttribute("ParentID"));
            //foreach (SvgElement g in layer.GraphList)
            //{
            //    layer2.GraphList.Add(g);
            //}
            SVG_LAYER la = new SVG_LAYER();
            la.SUID = layer.ID;
            la.svgID = symbolDoc.SvgdataUid;
            la=(SVG_LAYER) Services.BaseService.GetObject("SelectSVG_LAYERByKey", la);
            if(la!=null){
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

                //LineInfo _line = new LineInfo();
                //_line.EleID = element.ID;
                //_line.SvgUID = this.SymbolDoc.SvgdataUid;
                //IList lineInfoList = Services.BaseService.GetList("SelectLineInfoByEleID", _line);
              /*  PSPDEV _line = new PSPDEV();
                _line.EleID = element.ID;
                _line.SvgUID = this.SymbolDoc.SvgdataUid;
                IList lineInfoList = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandEleID", _line);

                foreach (PSPDEV line in lineInfoList)
                {
                    line.SUID = Guid.NewGuid().ToString();
                    line.LayerID = layer2.ID;
                    line.EleID = temp.ID;
                    line.Type = "05";
                    Services.BaseService.Create<PSPDEV>(line);
                }
                glebeProperty gle = new glebeProperty();
                gle.EleID = element.ID;
                gle.SvgUID = this.SymbolDoc.SvgdataUid;
                IList gleProList=Services.BaseService.GetList("SelectglebePropertyByEleID", gle);
                foreach (glebeProperty gleP  in gleProList)
                {
                    gleP.UID = Guid.NewGuid().ToString(); 
                    gleP.LayerID = layer2.ID;
                    gleP.EleID = temp.ID;
                    Services.BaseService.Create<glebeProperty>(gleP);
                }
                PSP_Substation_Info _sub = new PSP_Substation_Info();
                _sub.EleID = element.ID;
                _sub.AreaID = this.SymbolDoc.SvgdataUid;
                IList substationList = Services.BaseService.GetList("SelectPSP_Substation_InfoListByEleID", _sub);
                foreach (PSP_Substation_Info sub in substationList)
                {
                    sub.UID = Guid.NewGuid().ToString();
                    sub.LayerID = layer2.ID;
                    sub.EleID = temp.ID;
                    Services.BaseService.Create<PSP_Substation_Info>(sub);
                }
               */
            }
            this.SymbolDoc.NotifyUndo();
            return layer2;
        }
        public void DeleteLayer(Layer layer)
        {
            if (!CkRight(layer))
            {
                MessageBox.Show("����ͼ�㲻�ܸ�����ɾ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        
            //if (true) {
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
            XmlNodeList list = this.SymbolDoc.SelectNodes("//*[@layer='" + layer.ID + "']");
            foreach (XmlNode elNode in list)
            {
                this.SymbolDoc.RootElement.RemoveChild(elNode);
            }
       
             //Services.BaseService.Update("UpdateGraPowerRelationByLayerID", layer.ID);
            //���ĵ����Ƴ�
            SVG_LAYER lar = new SVG_LAYER();
            lar.SUID = layer.ID;
            Services.BaseService.Update("DeleteSVG_LAYER", lar);
            layer.Remove();
            //���б����Ƴ�
            this.checkedListBox1.Items.Remove(layer);
            layer = null;
            LayerName = "";            
        }

        private void checkedListBoxControl2_Leave(object sender, EventArgs e)
        {
            //checkedListBoxControl2.CheckedItems
        }
        private Layer CopyLayer2(Layer layer, string textname)
        {
            LayerGrade la = Services.BaseService.GetOneByKey<LayerGrade>(textname);

            string layerlabelf = layer.Label.Substring(4);
            string layer2name = la.Name.Substring(0,4) + layerlabelf;
            int j = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
               // if (checkedListBox1.Items[i].ToString() == layer2name)
                    //j = 1;
                //else
                //    j = 0;
            }
            Layer layer2 = null;
            if (j == 0)
            {
                layer2 = Layer.CreateNew(la.Name.Substring(0, 4) + layerlabelf, this.SymbolDoc);
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
                 /*   PSPDEV _line = new PSPDEV();
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
                   */
                }
            }
            this.SymbolDoc.NotifyUndo();
            return layer2;
        }
        public static bool IsInt(string inString)
        {
            Regex regex = new Regex("^[0-9]*[1-9][0-9]*$");
            return regex.IsMatch(inString.Trim());
        }
        private void simpleB(string id)
        {
          

            int itemcount = checkedListBox1.Items.Count;
            if (checkedListBox1.Items != null)
            {
                f.SetText("�����У���ȴ�......");
                f.Show();
                for (int num = 0; num < itemcount; num++)
                {


                    string itemname = checkedListBox1.Items[num].ToString();
                    if (itemname.Length > 4)
                   
                    
                    {
                        string item4 = itemname.Substring(0, 4);
                        if ((IsInt(item4)) && checkedListBox1.GetItemChecked(num) == true)
                        {
                            Layer layer = this.checkedListBox1.Items[num] as Layer;
                            f.SetText("��ѡ��" + checkedListBox1.CheckedItems.Count + "�㣬���ڸ���" + layer.Label + "�㡣");
                            string str = layer.GetAttribute("layerType");
                            Layer layer2 = CopyLayer2(layer, id);
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
                                    xml = xml + ((SvgElement)oldList[i]).OuterXml;
                                }
                                SVG_LAYER obj = new SVG_LAYER();
                                obj.SUID = layer2.ID;
                                obj.svgID = symbolDoc.SvgdataUid;
                                obj.NAME = layer2.Label;
                                obj.MDATE = System.DateTime.Now;
                                obj.OrderID = num + 10;
                                obj.YearID = id;//((LayerGrade)list[0]).SUID;
                                obj.layerType = "�����滮��";
                                obj.visibility = "visible";
                                obj.IsSelect = "true";
                                obj.XML = xml;
                                Services.BaseService.Create<SVG_LAYER>(obj);
                            }
                        }
                    }

                }
                f.Hide();
            }
        }
        private void checkedListBoxControl2_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            checkedListBoxControl2.Visible = false;
            if (checkedListBoxControl2.SelectedValue.ToString() == "��㸴��")
            {
              
                frmLayerManager2 temp = new frmLayerManager2(symbolDoc2,progtype);
                if (temp.ShowDialog(this) == DialogResult.Cancel)
                {
                    InitData();
                    checkedListBox1.Refresh();
                }
          
            }
            if (checkedListBoxControl2.SelectedValue.ToString() == "��ͼ�㸴��")
            {
                frmLayerGradeSave2 temp = new frmLayerGradeSave2();
                temp.SymbolDoc = symbolDoc;
                temp.InitData2(symbolDoc.SvgdataUid);
                //frmLayerManagerYear temp = new frmLayerManagerYear(symbolDoc2, progtype);
                if (temp.ShowDialog(this) == DialogResult.OK)
                {
                    ArrayList ll=temp.GetSelectNode2();
                    if (ll.Count>1){
                        for (int j = 1; j < ll.Count; j++)
                        {
                            simpleB(ll[j].ToString());
                            InitData();
                            checkedListBox1.Refresh();
                        }
                    }
                }
            }
            if (checkedListBoxControl2.SelectedValue.ToString() == "���㸴��")
            {
                int index = this.checkedListBox1.SelectedIndex;
               
                if (index > 0)
                {
                    DevExpress.XtraEditors.Controls.CheckedListBoxItem t = (DevExpress.XtraEditors.Controls.CheckedListBoxItem)checkedListBoxControl2.SelectedItem;
                    if (t.CheckState == CheckState.Checked)
                    {
                        f.SetText("�����У���ȴ�......");
                        f.Show();
                        Layer layer = this.checkedListBox1.Items[index] as Layer;
                        string str = layer.GetAttribute("layerType");
                        Layer layer2 = CopyLayer(layer);
                        if (layer.Visible)
                        {
                            layer.Visible = false;
                            layer.Visible = true;
                        }
                        layer2.SetAttribute("layerType", str);
                        this.checkedListBox1.Items.Add(layer2);
                        layer2.Visible = false;
                        f.Hide();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(checkedListBox1.SelectedIndex==-1){
                MessageBox.Show("��ѡ��ͼ�㡣","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            frmList l = new frmList();
            Layer layer = this.checkedListBox1.Items[checkedListBox1.SelectedIndex] as Layer;
            if (!NoSave.Contains(layer))
            {
                NoSave.Add(layer);
            }
            l.list = NoSave;
            l.Show();
        }

        private void ȫѡToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count;i++ )
            {
                checkedListBox1.SetItemChecked(i, true);
                Layer layer = this.checkedListBox1.Items[i] as Layer;
                layer.Visible = true;
            }
        }

        private void ȫ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
                Layer layer = this.checkedListBox1.Items[i] as Layer;
                layer.Visible = false;
            }
        }
        public void Sel(object sender, EventArgs e)
        {
            ȫ��ToolStripMenuItem_Click(sender,e);
        }
        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            XmlNodeList n1=  symbolDoc.SelectNodes("svg/polyline  [@layer='"+symbolDoc.CurrentLayer.ID+"']");
            XmlNodeList n2 = symbolDoc.SelectNodes("svg/use  [@layer='" + symbolDoc.CurrentLayer.ID + "']");

            for (int i = 0; i < n1.Count;i++ )
            {
                XmlNode x1 = n1[i];
                ((XmlElement)x1).RemoveAttribute("Deviceid");
            }
            for (int j = 0; j < n2.Count; j++)
            {
                XmlNode x2 = n2[j];
                ((XmlElement)x2).RemoveAttribute("Deviceid");
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheck(sender);
         
        }

        //private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        //{
        //    ChangeCheck(sender);
        //}

        //private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        //{
        //    ChangeCheck(sender);
        //}
        public void ChangeCheck(object sender)
        {
            if (dateEdit1.Text == "" || dateEdit2.Text == "") return;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                Layer layer = this.checkedListBox1.Items[i] as Layer;
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    for (int j = 0; j < layer.GraphList.Count; j++)
                    {
                        IGraph gp = (IGraph)layer.GraphList[j];
                        if (gp.GetType().ToString() == "ItopVector.Core.Figure.Polyline")
                        {
                            if (checkEdit1.Checked == true)
                            {
                                PSPDEV line = new PSPDEV();
                                line.SUID = ((XmlElement)gp).GetAttribute("Deviceid");
                                line = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", line);
                                if (line != null)
                                {
                                    if (line.Date1 == null || line.Date1 == "" || line.Date2 == null || line.Date2 == "")
                                    {
                                        gp.Visible = false;
                                        continue;
                                    }
                                    if ((Convert.ToInt32(dateEdit1.Text) == Convert.ToInt32(line.Date1)) && (Convert.ToInt32(dateEdit2.Text) == Convert.ToInt32(line.Date2)))
                                    {
                                        gp.Visible = true;
                                        continue;
                                    }
                                    else
                                    {
                                        gp.Visible = false;
                                    }
                                    for(int k=0;k<=(Convert.ToInt32(dateEdit2.Text)-Convert.ToInt32(dateEdit1.Text));k++){
                                        if ((Convert.ToInt32(dateEdit1.Text)+k >= Convert.ToInt32(line.Date1)) && (Convert.ToInt32(dateEdit1.Text)+k <= Convert.ToInt32(line.Date2))){
                                  
                                            gp.Visible = true;
                                            break;
                                        }
                                        else
                                        {
                                            gp.Visible = false;
                                        }
                                    }
                                    
                                }
                                else
                                {
                                    gp.Visible = false;
                                }
                            }
                            else
                            {
                                gp.Visible = true;
                            }
                        }
                        if (gp.GetType().ToString() == "ItopVector.Core.Figure.Use")
                        {
                            if (checkEdit1.Checked == true)
                            {
                                PSP_Substation_Info sub = new PSP_Substation_Info();
                                sub.UID = ((XmlElement)gp).GetAttribute("Deviceid");
                                sub = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoByKey", sub);
                                if (sub != null)
                                {
                                   
                                   
                                    if (sub.L28 == null || sub.L28 == "" || sub.L29 == null || sub.L29 == "")
                                    {
                                        gp.Visible = false;
                                        continue;
                                    }
                                    if ((Convert.ToInt32(dateEdit1.Text) == Convert.ToInt32(sub.L28)) && (Convert.ToInt32(dateEdit2.Text) == Convert.ToInt32(sub.L29)))
                                    {
                                        gp.Visible = true;
                                        continue;
                                    }
                                    else
                                    {
                                        gp.Visible = false;
                                    }
                                    for (int k = 0; k <= (Convert.ToInt32(dateEdit2.Text) - Convert.ToInt32(dateEdit1.Text)); k++)
                                    {
                                        if ((Convert.ToInt32(dateEdit1.Text) + k >= Convert.ToInt32(sub.L28)) && (Convert.ToInt32(dateEdit1.Text) + k <= Convert.ToInt32(sub.L29)))
                                        {

                                            gp.Visible = true;
                                            break;
                                        }
                                        else
                                        {
                                            gp.Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    gp.Visible = false;
                                }
                            }
                            else
                            {
                                gp.Visible = true;
                            }
                        }
                    }

                }

            }
            if (OnCheck != null)
            {
                OnCheck(sender);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeCheck(sender);
        }

        private void dateEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            //int i = e.KeyValue;
            //if ((i < 96) ||( i > 105))  //96--105  //
            //{
            //    e.SuppressKeyPress = false;
            //}
        }
    }
}