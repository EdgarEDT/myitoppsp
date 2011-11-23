using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using System.Text.RegularExpressions;
using Itop.Domain.Stutistic;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.IO;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmAddTzgsWH2pw : FormBase
    {
        public FrmAddTzgsWH2pw()
        {
            InitializeComponent(); 
            string conn = "Col5='" + projectID + "' and Col4='" + OperTable.tzgs + "'";
            yAnge = oper.GetYearRange(conn);
            InitCom();
        }
        private Ps_YearRange yAnge = new Ps_YearRange();
        OperTable oper = new OperTable();
        private string projectID;
        private string stat="no";
        private bool line = false;
        private string strtype = "";
        public string tzgspwid = "";
        public string StrType
        {
            get {
            //    if(comboBoxEdit12.Text=="变电站")
            //    return "bian";
            //if (comboBoxEdit12.Text == "线路")
            //    return "line";
            //if (comboBoxEdit12.Text == "开关")
            //    return "kg";
            //else
            //{
                return "";
            //}
            }
            set { strtype = value; }
        }



        public bool Line
        {
            get { return line; }
            set { line = value; }
        }
        public string Stat
        {
            get { return stat; }
            set { stat = value; }
        }
        private int v = 110;
        public int V
        {
            get { return v; }
            set { v = value; }
        }
        public string Col3
        {
            get { return comboBoxEdit9.Text; }
            set {
                string str = value;
                if (str.IndexOf("扩建") != -1)
                    comboBoxEdit9.SelectedIndex = 1;
                else if (str.IndexOf("改造") != -1)
                    comboBoxEdit9.SelectedIndex = 2;
                else
                    comboBoxEdit9.Text = "新建";
            }
        }
        public string StartYear
        {
            get { return comboBoxEdit1.Text; }
            set { comboBoxEdit1.Text = value; }
        }
        public string FinishYear
        {
            get {
                return comboBoxEdit2.Text;
            }
            set { comboBoxEdit2.Text = value; }
        }
        public string BieZhu
        {
            get { return memoEdit1.Text; }
            set { memoEdit1.Text = value; }
        }
        public string ParentName
        {
            get {
                //if (strtype=="line")
                //    return textEdit1.Text;
                //else
                    return comboBoxEdit8.Text;
            }
            set
            {
                //if (strtype=="line")
                //    textEdit1.Text = value;
                //else
                    comboBoxEdit8.Text = value;
            }
        }
        public string ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }
        public double LineLen
        {
            get { return double.Parse(spinEdit1.Text); }
            set { spinEdit1.Value = Convert.ToDecimal(value); }
        }
        public double LineLen2
        {
            get { return double.Parse(spinEdit3.Text); }
            set { spinEdit3.Value = Convert.ToDecimal(value); }
        }
        public string GetFromID
        {
            get { 
                if(Line) 
                    return comboBoxEdit5.Text;
                else
                    return comboBoxEdit5.Text;
            }
        }
        private double tzgsXs=0.03;
        public double TzgsXs
        {
            set { tzgsXs = value; }
        }
        private int syear = 2010;
        public int Syear
        {
            set { syear = value; }
        }
      
        public string AreaName
        {
            get { return comboBoxEdit10.Text; }
            set { areaname = value; }
        }
        public string DQ
        {
            get { return comboBoxEdit11.Text; }
            set { comboBoxEdit11.Text = value; }
        }

        public double Num1
        {
            get
            {
                if (strtype=="pw-line")
                    return Convert.ToDouble(txtnr.Text);
                if (strtype == "pw-kg")
                    return Convert.ToDouble(k1.Text);
                if (strtype == "pw-pb")
                    return Convert.ToDouble(txtnum1.Text);
                else
                {
                    return 0;
                }
            }
            set
            {
                if (strtype == "pw-line")
                    txtnr.Text = Convert.ToString(value);
                if (strtype == "pw-kg")
                    k1.Text = Convert.ToString(value);
                if (strtype == "pw-pb")
                    txtnum1.Text = Convert.ToString(value);
            }
        }
        public double Num2
        {
            get
            {
                if (strtype == "pw-line")
                    return Convert.ToDouble(txtgt.Text);
                if (strtype == "pw-kg")
                    return Convert.ToDouble(k2.Text);
                if (strtype == "pw-pb")
                    return Convert.ToDouble(txtnum2.Text);
                else
                {
                    return 0;
                }
            }
            set
            {
                if (strtype == "pw-line")
                    txtgt.Text = Convert.ToString(value);
                if (strtype == "pw-kg")
                    k2.Text = Convert.ToString(value);
                if (strtype == "pw-pb")
                    txtnum2.Text = Convert.ToString(value);
            }
        }
        public double Num3
        {
            get {
                if (strtype == "pw-kg")
                {
                    return Convert.ToDouble(k3.Text);
                }
                if (strtype == "pw-pb")
                {
                    return Convert.ToDouble(txtnum3.Text);
                }
                if (strtype == "pw-line")
                {
                    return Convert.ToDouble(jknum.Text);
                }
                else
                {
                    return 0;
                }
            }
            set {
                if (strtype == "pw-kg")
                {
                    k3.Text = Convert.ToString(value);
                }
                if (strtype == "pw-pb")
                {
                    txtnum3.Text = Convert.ToString(value);
                }
                if (strtype == "pw-line")
                {
                    jknum.Text=Convert.ToString(value);
                }
            }
        }
        public double Num4
        {
            get
            {
                if (strtype == "pw-kg")
                {
                    return Convert.ToDouble(k4.Text);
                }
                if (strtype == "pw-pb")
                {
                    return Convert.ToDouble(txtnum4.Text);
                }
                if (strtype == "pw-line")
                {
                    return Convert.ToDouble(dlnum.Text);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (strtype == "pw-kg")
                {
                    k4.Text = Convert.ToString(value);
                }
                if (strtype == "pw-pb")
                {
                    txtnum4.Text = Convert.ToString(value);
                }
                if (strtype == "pw-line")
                {
                    dlnum.Text = Convert.ToString(value);
                }
            }
        }

        public double Num5
        {
            get
            {
                if (strtype == "pw-line")
                    return Convert.ToDouble(txtts.Text);
                if (strtype == "pw-pb")
                    return Convert.ToDouble(txtnum5.Text);
                else
                {
                    return 0;
                }
            }
            set
            {
                if (strtype == "pw-line")
                    txtts.Text = Convert.ToString(value);
                if (strtype == "pw-pb")
                    txtnum5.Text = Convert.ToString(value);           
            }
        }
        public double Num6
        {
            get {
                if (strtype == "pw-line")
                {
                    return Convert.ToDouble(txtgd.Text);
                }
                if (strtype == "pw-pb")
                {
                    return Convert.ToDouble(txtnum6.Text);
                }
                else
                    return 0;
            }
            set
            {
                if (strtype == "pw-line")
                {
                    txtgd.Text = Convert.ToString(value);
                }
                if (strtype == "pw-pb")
                {
                    txtnum6.Text = Convert.ToString(value);
                }
            }
        }
        public double WGNum
        {
            get { return Convert.ToDouble(txtwg.Text); }
            set { txtwg.Text = Convert.ToString(value); }
        }
        public double Amount
        {
            get {
                if (strtype == "pw")
                {
                    return Convert.ToDouble(txttz.Text);
                }
                if (strtype == "pw-line")
                {
                    return Convert.ToDouble(lineje.Text);
                }
                if (strtype == "pw-pb")
                {
                    return Convert.ToDouble(pbje.Text);
                }
                if (strtype == "pw-kg")
                {
                    return Convert.ToDouble(kgje.Text);
                }
                else
                {
                    return 0;
                }
            }
            set {
                if (strtype == "pw")
                {
                    txttz.Text = Convert.ToString(value);
                }
                if (strtype == "pw-line")
                {
                    lineje.Text = Convert.ToString(value);
                }
                if (strtype == "pw-pb")
                {
                    pbje.Text = Convert.ToString(value);
                }
                if (strtype == "pw-kg")
                {
                    kgje.Text = Convert.ToString(value);
                }
                
            }
        }
        public int JGNum
        {
            get { return Convert.ToInt32(txtjg.Text); }
            set { txtjg.Text = Convert.ToString(value); }
        }
        public string ProgType
        {
            get { return txtProg.Text; }
            set { txtProg.Text = Convert.ToString(value); }
        }


        private string areaname = "";
        public string LineInfo
        {
            get { return comboBoxEdit5.Text + "@@" + comboBoxEdit7.Text; }
            set {
                try
                {
                    string str = value;
                    string[] ary = str.Split('@');
                
                    if (ary.Length == 3)
                    {
                        comboBoxEdit5.Text = ary[0];
                       // comboBoxEdit3.Text = ary[1];
                        comboBoxEdit7.Text = ary[1];
                    }
                }
                catch { }
            }
        }
        public string GetFlag
        {
            get { return comboBoxEdit7.Text; }
        }
        public string BianInfo
        {
            get { return comboBoxEdit5.Text + "@@"; }
            set
            {
               try
                { 
                    string str = value;
                    string[] ary = str.Split('@');
                    if (ary.Length == 3)
                    {
                        comboBoxEdit5.Text = ary[0];
                        
                    }
                }
                catch { }
            }
        }
     

        public void InitCom()
        {
            comboBoxEdit9.Properties.Items.Add("新建");
            comboBoxEdit9.Properties.Items.Add("扩建");
            comboBoxEdit9.Properties.Items.Add("改造");
            comboBoxEdit9.SelectedIndex = 0;
            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            {
                comboBoxEdit1.Properties.Items.Add(i.ToString());
                comboBoxEdit2.Properties.Items.Add(i.ToString());
            }
            comboBoxEdit1.Text = DateTime.Now.Year.ToString();
            comboBoxEdit2.Text = DateTime.Now.AddYears(1).Year.ToString() ;
       
        }

        private void FrmAddTzgs_Load(object sender, EventArgs e)
        {
         
            string conn = "ProjectID='" + Itop.Client.MIS.ProgUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list)
            {
                comboBoxEdit10.Properties.Items.Add(area.Title);
            }
           conn = "ProjectID='" + Itop.Client.MIS.ProgUID + "' order by Sort";
            IList<PS_Table_Area_TYPE> list1 = Common.Services.BaseService.GetList<PS_Table_Area_TYPE>("SelectPS_Table_Area_TYPEByConn", conn);
            foreach (PS_Table_Area_TYPE area in list1)
            {
                comboBoxEdit11.Properties.Items.Add(area.Title);
            }

            if (areaname != "")
                comboBoxEdit10.Text = areaname;
            else if (comboBoxEdit10.Properties.Items.Count > 0)
                comboBoxEdit10.SelectedIndex = 0;
            RefreshData(tzgspwid);
      
        }

        private void comboBoxEdit5_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void comboBoxEdit6_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

       

      

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if ((line && (textEdit1.Text == "" || textEdit1.Text == null)) || (!line && (comboBoxEdit8.Text=="" || comboBoxEdit8.Text==null)))
            {
                MessageBox.Show("项目名称不能为空", "");
                return; 
            }
            try
            {
                if (int.Parse(comboBoxEdit2.Text) < int.Parse(comboBoxEdit1.Text))
                {
                    MessageBox.Show("竣工年必须大于开工年", "");
                    return;
                }
            }
            catch { }
            DialogResult = DialogResult.OK;
        }

        private void comboBoxEdit10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit9.Text != "新建")
            {
                //comboBoxEdit8.Properties.Items.Clear();
                string conn = "AreaID='" + projectID + "' and  L1=" + V + " and AreaName='" + comboBoxEdit10.Text + "'";
                IList<Substation_Info> list = Common.Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon", conn);
                //foreach (Substation_Info area in list)
                //{
                //    comboBoxEdit8.Properties.Items.Add(area.Title);
                //}
                if (comboBoxEdit8.Properties.Items.Count > 0)
                    comboBoxEdit8.SelectedIndex = 0;
            }
        }

        private void comboBoxEdit9_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            //else
            //{
                //comboBoxEdit8.Properties.Items.Clear();
                string conn = "AreaID='" + projectID + "' and  L1=" + V + " and AreaName='" + comboBoxEdit10.Text + "'";
                IList<Substation_Info> list = Common.Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon", conn);
                foreach (Substation_Info area in list)
                {
                    comboBoxEdit8.Properties.Items.Add(area.Title);
                }
                if (comboBoxEdit8.Properties.Items.Count > 0)
                    comboBoxEdit8.SelectedIndex = 0;
            //}
        }

        private void txtdlq_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxEdit12_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void lineje_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txttz.Text = Convert.ToString(Convert.ToDecimal(lineje.Text) + Convert.ToDecimal(pbje.Text) + Convert.ToDecimal(kgje.Text));
            }
            catch
            {
                MessageBox.Show("值应为数字。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pbje_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txttz.Text = Convert.ToString(Convert.ToDecimal(lineje.Text) + Convert.ToDecimal(pbje.Text) + Convert.ToDecimal(kgje.Text));
            }
            catch
            {
                MessageBox.Show("值应为数字。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void kgje_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txttz.Text = Convert.ToString(Convert.ToDecimal(lineje.Text) + Convert.ToDecimal(pbje.Text) + Convert.ToDecimal(kgje.Text));
            }
            catch
            {
                MessageBox.Show("值应为数字。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void jknum_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtts.Text = Convert.ToString(Convert.ToDecimal(jknum.Text) + Convert.ToDecimal(dlnum.Text) );
            }
            catch
            {
                MessageBox.Show("值应为数字。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dlnum_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtts.Text = Convert.ToString(Convert.ToDecimal(jknum.Text) + Convert.ToDecimal(dlnum.Text));
            }
            catch
            {
                MessageBox.Show("值应为数字。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FormTzgsWH2pwXX fxx = new FormTzgsWH2pwXX();
            fxx.type = "pw-line";
            fxx.pid = tzgspwid;
            fxx.buildyear = StartYear;
            fxx.buildend = FinishYear;
            if (fxx.ShowDialog()==DialogResult.OK)
            {
                IList list1 = fxx.ObjectList;
                foreach (Ps_Table_TZMX pt in list1)
                {
                    Ps_Table_TZMX pm = Services.BaseService.GetOneByKey<Ps_Table_TZMX>(pt);
                    if (pm != null)
                    {
                        Services.BaseService.Update<Ps_Table_TZMX>(pt);
                    }
                    else
                        Services.BaseService.Create<Ps_Table_TZMX>(pt);
                }
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            FormTzgsWH2pwXX fxx = new FormTzgsWH2pwXX();
            fxx.type = "pw-pb";
            fxx.pid = tzgspwid;
            fxx.buildyear = StartYear;
            fxx.buildend = FinishYear;
            if (fxx.ShowDialog() == DialogResult.OK)
            {
                IList list1 = fxx.ObjectList;
                foreach (Ps_Table_TZMX pt in list1)
                {
                    Ps_Table_TZMX pm = Services.BaseService.GetOneByKey<Ps_Table_TZMX>(pt);
                    if (pm != null)
                    {
                        Services.BaseService.Update<Ps_Table_TZMX>(pt);
                    }
                    else
                        Services.BaseService.Create<Ps_Table_TZMX>(pt);
                }
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            FormTzgsWH2pwXX fxx = new FormTzgsWH2pwXX();
            fxx.type = "pw-kg";
            fxx.pid = tzgspwid;
            fxx.buildyear = StartYear;
            fxx.buildend = FinishYear;
            if (fxx.ShowDialog() == DialogResult.OK)
            {
                IList list1 = fxx.ObjectList;
                foreach (Ps_Table_TZMX pt in list1)
                {
                    Ps_Table_TZMX pm = Services.BaseService.GetOneByKey<Ps_Table_TZMX>(pt);
                    if (pm != null)
                    {
                        Services.BaseService.Update<Ps_Table_TZMX>(pt);
                    }
                    else
                        Services.BaseService.Create<Ps_Table_TZMX>(pt);
                }
            }
        }

        private void 增加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImgAdd f = new frmImgAdd();
            f.Uid = tzgspwid;
            if (f.ShowDialog() == DialogResult.OK)
            {
                RefreshData(tzgspwid);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PSP_ImgInfo obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
            if (MessageBox.Show("确定要删除么？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Services.BaseService.Update("DeletePSP_ImgInfo", obj);
                RefreshData(tzgspwid);

            }
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        public PSP_ImgInfo FocusedObject
        {
            get { return this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as PSP_ImgInfo; }
        }
        public bool RefreshData(string id)
        {
            try
            {
                PSP_ImgInfo img = new PSP_ImgInfo();
                img.TreeID = id;
                IList<PSP_ImgInfo> list = Services.BaseService.GetList<PSP_ImgInfo>("SelectPSP_ImgInfoList", img);

                this.gridControl.DataSource = list;
            }
            catch (Exception exc)
            {
                //HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }

        public void OpenFile()
        {
            PSP_ImgInfo obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
            obj = Services.BaseService.GetOneByKey<PSP_ImgInfo>(obj.UID);
            BinaryWriter bw;
            FileStream fs;
            try
            {
                byte[] bt = obj.Image;
                string filename = obj.Name;
                fs = new FileStream("C:\\" + filename, FileMode.Create, FileAccess.Write);
                bw = new BinaryWriter(fs);
                bw.Write(bt);
                bw.Flush();
                bw.Close();
                fs.Close();
                System.Diagnostics.Process.Start("C:\\" + filename);

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
    }
}