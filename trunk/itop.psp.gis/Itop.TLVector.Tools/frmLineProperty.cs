using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using Itop.Client;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Stutistic;
using System.Xml;
using ItopVector.Core;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmLineProperty : FormBase
    {
        private LineInfo line = new LineInfo();

      
        DataTable dt = new DataTable();
        Line_Info p;
        public bool IsCreate = false;
        string rzb = "1";
        private bool isReadonly = false;
        private string layerID = "";
        //string lineType = "";
        public string OldUID = "";
        public bool SubUpdate = false;
        public string LineWidth = "1";
        public string FsNode = "";
        public string LsNode = "";
        public SvgElement LineNode = null;
        /// <summary>
        /// 首节点T接
        /// </summary>
        public bool Ftj {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }
        /// <summary>
        /// 尾节点T接
        /// </summary>
        public bool Ltj {
            get { return checkBox2.Checked; }
            set { checkBox2.Checked = value; }
        }
        public bool IsReadonly
        {
            get { return isReadonly; }
            set { isReadonly = value; }
        }
        public LineInfo Line
        {
            get { return line; }
            set { line = value; }
        }
        public frmLineProperty()
        {
            InitializeComponent();
            object[] oj = new object[60];
            for (int o = 0; o < 60; o++)
            {
                oj[o] = 1970 + o;
            }
            this.comboTcnf.Properties.Items.AddRange(oj);
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (LineNode != null) {
                Ftj = LineNode.GetAttribute("ftj") == "true";
                Ltj = LineNode.GetAttribute("ltj") == "true";
            }
        }
        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            if (DialogResult == DialogResult.OK && LineNode != null) {
            //保存T接标志
            if (Ftj)
                LineNode.SetAttribute("ftj", "true");
            else
                LineNode.RemoveAttribute("ftj");
            if (Ltj)
                LineNode.SetAttribute("ltj", "true");
            else
                LineNode.RemoveAttribute("ltj");
            }
            //保存首末节点ID
            if (FsNode != "") {
                LineNode.SetAttribute("FirstNode", FsNode);                
            }
            if (LsNode != "") {
                LineNode.SetAttribute("LastNode", LsNode);
            }
        }
        public void InitData(string useid,string svguid,string len,string layID)
        {
            
            try
            {
                layerID = layID;
                line.EleID = useid;
                line.SvgUID = svguid;
                line.Length = len;
                IList svglist = Services.BaseService.GetList("SelectLineInfoByEleID", line);
                if (svglist.Count > 0)
                {
                    line = (LineInfo)svglist[0];
                    if(line.Length==""){
                        line.Length = len;
                    }
                    if(line.ObligateField1==""){
                        line.ObligateField1 = "运行";
                    }                    
                    line.Voltage = line.Voltage + "kV";
                    IsCreate = false;
                }
                else
                {
                    IsCreate = true;
                    line.UID = Guid.NewGuid().ToString();
                    line.LayerID = layerID;
                    line.ObligateField1 = "运行";
                }
                mc.DataBindings.Add("Text", line, "LineName");
                cd.DataBindings.Add("Text", line, "Length");
                xh.DataBindings.Add("Text", line, "LineType");
                comboBox1.DataBindings.Add("Text", line, "ObligateField1");
                dj.DataBindings.Add("EditValue", line, "Voltage");
                //comboTcnf.DataBindings.Add("Text", line, "ObligateField3");
                jsdd.DataBindings.Add("Text", line, "ObligateField4");
                dxxh.DataBindings.Add("Text", line, "ObligateField5");
                FNode.DataBindings.Add("Text", line, "ObligateField6");
                LNode.DataBindings.Add("Text", line, "ObligateField7");
                if (line.ObligateField3 != null)
                {
                    if(line.ObligateField3.Length==4){
                        comboTcnf.Text = line.ObligateField3;
                    }
                    string[] s = line.ObligateField3.Split("-".ToCharArray());
                    if (s.Length > 1)
                    {
                        comboTcnf.Text = s[0];
                        comy.Text = s[1];
                    }
                }
            }
            catch(Exception e){
                MessageBox.Show(e.Message);
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (line.LineName == "")
            {
                MessageBox.Show("线路名称不能为空。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if(line.Length==""){
                MessageBox.Show("线路长度不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dj.Text == "")
            {
                MessageBox.Show("电压等级不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (comboBox1.Text == "")
            {
                MessageBox.Show("线路状态不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (comboTcnf.Text != "" && comy.Text != "")
            {
                line.ObligateField3 = comboTcnf.Text;// +"-" + comy.Text;
            }
            if (IsCreate)
            {
                LineInfo _l = new LineInfo();
                _l.LineName = line.LineName;
                _l.SvgUID = line.SvgUID;
                IList mlist = Services.BaseService.GetList("SelectLineInfoByLineNameCK", _l);
                if (mlist.Count > 0) {
                    MessageBox.Show("线路名称重复。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                }                
                line.Voltage=line.Voltage.ToLower().Replace("kv", "");
                line.ObligateField6 = FNode.Text;
                line.ObligateField7 = LNode.Text;
                Services.BaseService.Create<LineInfo>(line);
            }
            else
            {
                line.LayerID = layerID;
                line.Voltage = line.Voltage.ToLower().Replace("kv", "");
                line.ObligateField1 = comboBox1.Text;
                line.ObligateField6 = FNode.Text;
                line.ObligateField7 = LNode.Text;
                Services.BaseService.Update<LineInfo>(line);
            }
          
            string uid = dj.EditValue.ToString();
            DataRowView rowView = (DataRowView)dj.Properties.GetDataSourceRowByKeyValue(uid);
            if (rowView != null)
            {
                line.ObligateField2 = rowView.Row["Color"].ToString();
                LineWidth = rowView.Row["ObligateField1"].ToString();
                //line.ObligateField2 = lineType;
            }

            if(SubUpdate){              
                Line_Info temp = (Line_Info)Services.BaseService.GetObject("SelectLine_InfoByCode", line.UID);
                if (temp != null)
                {
                    temp.Code = "";
                    Services.BaseService.Update<Line_Info>(temp);
                }
                LineInfo _s = new LineInfo();
                _s.UID = OldUID;
                LineInfo _temps = Services.BaseService.GetOneByKey<LineInfo>(_s);
                if (_temps != null)
                {
                    if (_temps.EleID == "")
                    {
                        Services.BaseService.Update("DeleteLineInfo", _temps);
                    }
                }
                Services.BaseService.Update<Line_Info>(p);
            }
            Line_Info ppt = null;
            try{
                ppt=(Line_Info)Services.BaseService.GetObject("SelectLine_InfoByCode", line);
            }catch{}
            if (ppt != null)
            {
                ppt.DY =Convert.ToInt32( line.Voltage.ToLower().Replace("kv", ""));
                ppt.Title = line.LineName;//名称
                //ppt.L1 = "";//台数
                //ppt.L2 = "";//容量

                try
                {
                    ppt.K5 = Convert.ToDouble(line.Length);//长度
                    ppt.L5 = Convert.ToDouble(line.Length);//长度
              
                ppt.L4 = line.LineType;//型号
                ppt.K2 = line.LineType;//型号
                ppt.L6 = line.ObligateField3;
                //ppt.L5 = "";//负荷率
                //ppt.L6 = "";//最大负荷

                Services.BaseService.Update<Line_Info>(ppt);
                }
                catch { }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          
        }

        private void frmProperty_Load(object sender, EventArgs e)
        {
            mc.Focus();
            if(IsReadonly){
                mc.Properties.ReadOnly = true;
                cd.Properties.ReadOnly = true;
                xh.Properties.ReadOnly = true;
                dj.Properties.ReadOnly = true;
                simpleButton1.Visible = false;          
                 
                simpleButton2.Text = "关闭";
            }
            LineInfo line=new LineInfo();            
            IList list = Services.BaseService.GetList<LineType>();
            IList list1 = Services.BaseService.GetList("SelectLineInfoLineType","");
            IList list2 = Services.BaseService.GetList("SelectLineInfoLineTypeObj5", "");
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(LineType));            
            dj.Properties.DataSource = dt;
            foreach (LineInfo str in list1)
            {
                xh.Properties.Items.Add(str.LineType);
            }
            foreach (LineInfo str2 in list2)
            {
                dxxh.Properties.Items.Add(str2.ObligateField5);
            }
               
        }

        private void dj_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string uid = dj.EditValue.ToString();
                DataRowView rowView = (DataRowView)dj.Properties.GetDataSourceRowByKeyValue(uid);
                if (rowView != null)
                {
                    line.ObligateField2 = rowView.Row["Color"].ToString();
                    LineWidth = rowView.Row["ObligateField1"].ToString();                   
                    //line.ObligateField2 = lineType;
                }
            }
            catch { }
        }      
    

        private void mc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmSubSel f = new frmSubSel();
            string strKv = dj.Text.Replace("kV", "");
            if (strKv == "") { strKv = "0"; }
            if (comboBox1.Text == "规划")
            {
                f.ReDate(layerID, false, true, strKv);
            }
            else
            {
                f.ReDate("", true, true, strKv);
            }
            if (f.ShowDialog() == DialogResult.OK)
            {
                p = f.ctrlLine_Info1.FocusedObject;
                line.LineName = p.Title;
                line.Length = p.K5.ToString();
                line.LineType = p.K2;
                mc.Text = line.LineName;
                cd.Text = line.Length;
                xh.Text = line.LineType;
                OldUID = p.Code;
                p.Code = line.UID;
                SubUpdate = true;
            }
        }

        private void mc_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) {
                frmSelectSubAllByYear frm = new frmSelectSubAllByYear();
                frm.ShowDialog();
                if (frm.substat != null) {
                    FNode.Text = frm.substat.EleName;
                    FsNode = frm.substat.EleID;
                }
            } else if (e.Button.Index == 1) {
                FNode.Text = "";
                FsNode = "";
                LineNode.RemoveAttribute("FirstNode");
            }
        }

        private void buttonEdit2_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) {
                frmSelectSubAllByYear frm = new frmSelectSubAllByYear();
                frm.ShowDialog();
                if (frm.substat != null) {
                    LNode.Text = frm.substat.EleName;
                    LsNode = frm.substat.EleID;
                }
            } else if (e.Button.Index == 1) {
                LNode.Text = "";
                LsNode = "";
                LineNode.RemoveAttribute("LastNode");
            }
        }

    }
}