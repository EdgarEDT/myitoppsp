using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmPsEdit : FormBase
    {
        public FrmPsEdit()
        {
            InitializeComponent();
        }
        private string mark;
        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }
        public string GetProject
        {
            set { projectid = value; }
            get { return projectid; }
        }
        private string projectid;
        private void FrmPsEdit_Load(object sender, EventArgs e)
        {
            yearRange = oper.GetYearRange("Col5='" + GetProject + "' and Col4='" + mark + "'");
            LoadGridData();
            InitYear();
        }

        public IList<Ps_Table_Edit> list;
        private OperTable oper = new OperTable();
        private Ps_YearRange yearRange =new Ps_YearRange();
        //private string title;
        public IList<Ps_Table_Edit> GridData
        {
            set { list = value; }
        }

        public string Title
        {
            set { textEdit1.Text = value; }
            get { return textEdit1.Text; }
        }

        public Hashtable TextAttr
        {
            get
            {
                return textAttr;
            }
            set { textAttr = value; }
        }
        private IList<string> strResult=new List<string>();
        public IList<string> StrResult
        {
            get { return strResult; }
            set { strResult = value;}
        }
        private Hashtable textAttr;
        public void LoadGridData()
        {
            this.gridControl1.DataSource = list;
           // this.gridView1.Columns["Sort"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
           // label7.Text = list[0].Volume == null ? "0" : list[0].Volume;
            label7.Text=GetCurVolumn();
        }
        private string maxYear;
        public string MaxYear
        {
            set { maxYear = value; }
        }
        public string CurVolumn
        {
            set { label7.Text = value; this.spinEdit1.Value = Convert.ToDecimal(value); }
        }

        public string GetStatus
        {
            get { return comboBoxEdit1.Text; }
        }

        public void InitYear()
        {
            for (int i = DateTime.Now.Year; i <= yearRange.EndYear; i++)
            {
                comboBoxEdit2.Properties.Items.Add(i.ToString());
                comboBoxEdit3.Properties.Items.Add(i.ToString());
            }
            comboBoxEdit1.SelectedIndex = 0;
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text == "已有")
            {
                comboBoxEdit2.Text = "";
                comboBoxEdit3.Text = "";
                comboBoxEdit2.Enabled = false;
                comboBoxEdit3.Enabled = false;
                spinEdit1.Enabled = true;
                spinEdit1.Value = Convert.ToDecimal(label7.Text);
            }
            else if (comboBoxEdit1.Text == "扩建/改造")
            {
                comboBoxEdit2.Enabled = true;
                spinEdit1.Enabled = true;
                comboBoxEdit3.Enabled = true;
                comboBoxEdit2.Text = maxYear == "" ? DateTime.Now.Year.ToString() : maxYear;
                comboBoxEdit3.Text = maxYear == "" ? Convert.ToString(DateTime.Now.Year + 1) :  Convert.ToString(int.Parse(maxYear)+1);
                spinEdit1.Value = Convert.ToDecimal(label7.Text);
            }
            else if (comboBoxEdit1.Text == "拆除")
            {
                comboBoxEdit2.Enabled = true;
                comboBoxEdit3.Enabled = true;
                comboBoxEdit2.Text = maxYear == "" ? DateTime.Now.Year.ToString() : maxYear;
                comboBoxEdit3.Text = maxYear == "" ? Convert.ToString(DateTime.Now.Year + 1) : Convert.ToString(int.Parse(maxYear) + 1);
                spinEdit1.Enabled = false;
                spinEdit1.Value = (decimal)0.0;
            }
        }
        private string parentid;
        public string ParentID
        {
            set { parentid = value; }
        }
        //增加
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit3.Text == "")
            {
                MessageBox.Show("竣工年份不能为空"); return;
            }
            if (comboBoxEdit1.Text != "已有" && comboBoxEdit2.Text!="")
            {
                if (int.Parse(comboBoxEdit3.Text) < int.Parse(comboBoxEdit2.Text))
                {
                    MessageBox.Show("竣工年必须大于开工年!"); return;
                }
               
            }
           // DialogResult = DialogResult.OK;
            Ps_Table_Edit edit = new Ps_Table_Edit();
            edit.ID += "|" + GetProject;
            edit.ParentID = parentid;
            edit.StartYear = comboBoxEdit1.Text == "已有" ? "" : comboBoxEdit2.Text;
            edit.FinishYear = comboBoxEdit1.Text == "已有" ? "" : comboBoxEdit3.Text;
            edit.Status = comboBoxEdit1.Text;
            edit.Volume = this.spinEdit1.Text;
            edit.ProjectID = GetProject;
            edit.Col4 = mark;
            try
            {
                edit.Sort = OperTable.GetChildMaxSort()+1;
            }
            catch { edit.Sort = 4; }
            if (edit.Sort < 4)
                edit.Sort = 4;
            Common.Services.BaseService.Create("InsertPs_Table_Edit", edit);
            strResult.Add(textEdit1.Text);
            strResult.Add(comboBoxEdit3.Text);
            strResult.Add(spinEdit1.Value.ToString());
            string conn = "ParentID='" + parentid + "'";
            list = Common.Services.BaseService.GetList<Ps_Table_Edit>("SelectPs_Table_EditListByConn", conn);
            if (typeTable == "500")
            {
                Ps_Table_500PH table = new Ps_Table_500PH();
                table = Common.Services.BaseService.GetOneByKey<Ps_Table_500PH>(parentid);
                if (comboBoxEdit1.Text == "扩建/改造")
                {

                    for (int i = int.Parse(comboBoxEdit3.Text); i <= yearRange.FinishYear; i++)
                    {
                        string a = table.GetType().GetProperty("y" + i.ToString()).GetValue(table, null).ToString();

                        table.GetType().GetProperty("y" + i.ToString()).SetValue(table, Convert.ToDouble(a) + Convert.ToDouble(spinEdit1.Text), null);
                    }
                    Common.Services.BaseService.Update("UpdatePs_Table_500PH", table);
                }
            }
            else if (typeTable == "200")
            {
                Ps_Table_200PH table = new Ps_Table_200PH();
                table = Common.Services.BaseService.GetOneByKey<Ps_Table_200PH>(parentid);
                if (comboBoxEdit1.Text == "扩建/改造")
                {
                  
                    for (int i = int.Parse(comboBoxEdit3.Text); i <= yearRange.FinishYear; i++)
                    {
                        string a = table.GetType().GetProperty("y" + i.ToString()).GetValue(table, null).ToString();
                        table.GetType().GetProperty("y" + i.ToString()).SetValue(table, Convert.ToDouble(a) + Convert.ToDouble(spinEdit1.Text), null);
                    } 
                    Common.Services.BaseService.Update("UpdatePs_Table_200PH", table);
                }
            }
            else if (typeTable == "100")
            {
                Ps_Table_100PH table = new Ps_Table_100PH();
                table = Common.Services.BaseService.GetOneByKey<Ps_Table_100PH>(parentid);
                if (comboBoxEdit1.Text == "扩建/改造")
                {

                    for (int i = int.Parse(comboBoxEdit3.Text); i <= yearRange.FinishYear; i++)
                    {
                        string a = table.GetType().GetProperty("y" + i.ToString()).GetValue(table, null).ToString();
                        table.GetType().GetProperty("y" + i.ToString()).SetValue(table, Convert.ToDouble(a) + Convert.ToDouble(spinEdit1.Text), null);
                    } 
                    Common.Services.BaseService.Update("UpdatePs_Table_100PH", table);
                }
            }
           
         

            LoadGridData();
            label7.Text = GetCurVolumn();
        }
        //修改
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit3.Text == "")
            {
                MessageBox.Show("竣工年份不能为空"); return;
            }
            if (comboBoxEdit1.Text != "已有" && comboBoxEdit2.Text != "")
            {
                if (int.Parse(comboBoxEdit3.Text) < int.Parse(comboBoxEdit2.Text))
                {
                    MessageBox.Show("竣工年必须大于开工年!"); return;
                }

            }
            if (gridView1.FocusedRowHandle < 0)
            {
                MessageBox.Show("请选择一条记录！"); return;
            }
            string id = this.gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID").ToString();
            
            Ps_Table_Edit edit = Common.Services.BaseService.GetOneByKey<Ps_Table_Edit>(id);

            if (edit.Status == "扩建/改造" || edit.Status == "拆除")
            {
                Ps_Table_Edit edit1 = new Ps_Table_Edit();
                edit1.ID = edit.ID;
                edit1.ParentID = edit.ParentID;
                edit1.Sort = edit.Sort;
                edit1.Status = comboBoxEdit1.Text;
                edit1.StartYear = comboBoxEdit2.Text;
                edit1.FinishYear = comboBoxEdit3.Text;
                edit1.Volume = spinEdit1.Value.ToString();
                edit1.ProjectID = GetProject;
                edit.Col4 = mark;
                Common.Services.BaseService.Update("UpdatePs_Table_Edit", edit1);
                string conn = "ParentID='" + parentid + "'";
                list = Common.Services.BaseService.GetList<Ps_Table_Edit>("SelectPs_Table_EditListByConn", conn);
                LoadGridData();
                label7.Text = GetCurVolumn();
           }
           
           else
               MessageBox.Show("只能修改扩建/改造或拆除的记录。");
        }
        //删除
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                MessageBox.Show("请选择一条记录！"); return;
            }
            string id = this.gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID").ToString();
            
            Ps_Table_Edit edit = Common.Services.BaseService.GetOneByKey<Ps_Table_Edit>(id);

            if (edit.Status == "扩建/改造" || edit.Status == "拆除")
            {
                if (MessageBox.Show("确定删除这条记录吗?", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Common.Services.BaseService.Delete<Ps_Table_Edit>(edit);
                    string conn = "ParentID='" + parentid + "'";
                    list = Common.Services.BaseService.GetList<Ps_Table_Edit>("SelectPs_Table_EditListByConn", conn);
                    LoadGridData();
                    label7.Text = GetCurVolumn();
                    if (typeTable == "500")
                    {
                        Ps_Table_500PH table = new Ps_Table_500PH();
                        table = Common.Services.BaseService.GetOneByKey<Ps_Table_500PH>(parentid);
                        if (comboBoxEdit1.Text == "扩建/改造")
                        {

                            string a = table.GetType().GetProperty("y" + comboBoxEdit3.Text).GetValue(table, null).ToString();
                            for (int i = int.Parse(comboBoxEdit3.Text); i <= yearRange.FinishYear; i++)
                            {
                                table.GetType().GetProperty("y" + i.ToString()).SetValue(table, Convert.ToDouble(a) - Convert.ToDouble(spinEdit1.Text), null);
                            }
                            Common.Services.BaseService.Update("UpdatePs_Table_500PH", table);
                        }
                    }
                    else if (typeTable == "200")
                    {
                        Ps_Table_200PH table = new Ps_Table_200PH();
                        table = Common.Services.BaseService.GetOneByKey<Ps_Table_200PH>(parentid);
                        if (comboBoxEdit1.Text == "扩建/改造")
                        {

                            string a = table.GetType().GetProperty("y" + comboBoxEdit3.Text).GetValue(table, null).ToString();
                            for (int i = int.Parse(comboBoxEdit3.Text); i <= yearRange.FinishYear; i++)
                            {
                                table.GetType().GetProperty("y" + i.ToString()).SetValue(table, Convert.ToDouble(a) - Convert.ToDouble(spinEdit1.Text), null);
                            }
                            Common.Services.BaseService.Update("UpdatePs_Table_200PH", table);
                        }
                    }
                    else if (typeTable == "100")
                    {
                        Ps_Table_100PH table = new Ps_Table_100PH();
                        table = Common.Services.BaseService.GetOneByKey<Ps_Table_100PH>(parentid);
                        if (comboBoxEdit1.Text == "扩建/改造")
                        {

                            string a = table.GetType().GetProperty("y" + comboBoxEdit3.Text).GetValue(table, null).ToString();
                            for (int i = int.Parse(comboBoxEdit3.Text); i <= yearRange.FinishYear; i++)
                            {
                                table.GetType().GetProperty("y" + i.ToString()).SetValue(table, Convert.ToDouble(a) - Convert.ToDouble(spinEdit1.Text), null);
                            }
                            Common.Services.BaseService.Update("UpdatePs_Table_100PH", table);
                        }
                    }
                    
                }
            }
            else
                MessageBox.Show("只能删除扩建/改造或拆除的记录。");
        }
        private string typeTable;
        public string TypeTable
        {
            set { typeTable = value; }
        }
        public string GetCurVolumn()
        {
            Ps_YearRange range=yearRange;
            string conn = "ParentID='" + parentid + "'";
            IList tList = Common.Services.BaseService.GetList("SelectPs_Table_EditListByConn", conn);
            if (typeTable == "500")
            {
                Ps_Table_500PH edit = Common.Services.BaseService.GetOneByKey<Ps_Table_500PH>(parentid);
                for (int j = 0; j < tList.Count; j++)
                {
                    if (((Ps_Table_Edit)tList[j]).Status == "扩建/改造")
                    {
                        for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                        {
                            double old = (double)edit.GetType().GetProperty("y" + k.ToString()).GetValue(edit, null);
                            edit.GetType().GetProperty("y" + k.ToString()).SetValue(edit, double.Parse(((Ps_Table_Edit)tList[j]).Volume) + old, null);
                        }
                    }
                    else if (((Ps_Table_Edit)tList[j]).Status == "拆除")
                    {
                        for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                        {
                            edit.GetType().GetProperty("y" + k.ToString()).SetValue(edit, 0.0, null);
                        }
                    }
                }
                return edit.GetType().GetProperty("y" + range.EndYear).GetValue(edit, null).ToString();
            }
            else if (typeTable == "200")
            {
                Ps_Table_200PH edit = Common.Services.BaseService.GetOneByKey<Ps_Table_200PH>(parentid);
                for (int j = 0; j < tList.Count; j++)
                {
                    if (((Ps_Table_Edit)tList[j]).Status == "扩建/改造")
                    {
                        for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                        {
                            double old = (double)edit.GetType().GetProperty("y" + k.ToString()).GetValue(edit, null);
                            edit.GetType().GetProperty("y" + k.ToString()).SetValue(edit, double.Parse(((Ps_Table_Edit)tList[j]).Volume) + old, null);
                        }
                    }
                    else if (((Ps_Table_Edit)tList[j]).Status == "拆除")
                    {
                        for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                        {
                            edit.GetType().GetProperty("y" + k.ToString()).SetValue(edit, 0.0, null);
                        }
                    }
                }
                return edit.GetType().GetProperty("y" + range.EndYear).GetValue(edit, null).ToString();
            }
            else if(typeTable=="100")
            {
                Ps_Table_100PH edit = Common.Services.BaseService.GetOneByKey<Ps_Table_100PH>(parentid);
                for (int j = 0; j < tList.Count; j++)
                {
                    if (((Ps_Table_Edit)tList[j]).Status == "扩建/改造")
                    {
                        for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                        {
                            double old = (double)edit.GetType().GetProperty("y" + k.ToString()).GetValue(edit, null);
                            edit.GetType().GetProperty("y" + k.ToString()).SetValue(edit, double.Parse(((Ps_Table_Edit)tList[j]).Volume) + old, null);
                        }
                    }
                    else if (((Ps_Table_Edit)tList[j]).Status == "拆除")
                    {
                        for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                        {
                            edit.GetType().GetProperty("y" + k.ToString()).SetValue(edit, 0.0, null);
                        }
                    }
                }
                return edit.GetType().GetProperty("y" + range.EndYear).GetValue(edit, null).ToString();
            }
            else if (typeTable == "35")
            {
                Ps_Table_35PH edit = Common.Services.BaseService.GetOneByKey<Ps_Table_35PH>(parentid);
                for (int j = 0; j < tList.Count; j++)
                {
                    if (((Ps_Table_Edit)tList[j]).Status == "扩建/改造")
                    {
                        for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                        {
                            double old = (double)edit.GetType().GetProperty("y" + k.ToString()).GetValue(edit, null);
                            edit.GetType().GetProperty("y" + k.ToString()).SetValue(edit, double.Parse(((Ps_Table_Edit)tList[j]).Volume) + old, null);
                        }
                    }
                    else if (((Ps_Table_Edit)tList[j]).Status == "拆除")
                    {
                        for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                        {
                            edit.GetType().GetProperty("y" + k.ToString()).SetValue(edit, 0.0, null);
                        }
                    }
                }
                return edit.GetType().GetProperty("y" + range.EndYear).GetValue(edit, null).ToString();
            }
            else 
            {
                Ps_PowerBuild edit = Common.Services.BaseService.GetOneByKey<Ps_PowerBuild>(parentid);
                for (int j = 0; j < tList.Count; j++)
                {
                    if (((Ps_Table_Edit)tList[j]).Status == "扩建/改造")
                    {
                        for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                        {
                            double old = (double)edit.GetType().GetProperty("y" + k.ToString()).GetValue(edit, null);
                            edit.GetType().GetProperty("y" + k.ToString()).SetValue(edit, double.Parse(((Ps_Table_Edit)tList[j]).Volume) + old, null);
                        }
                    }
                    else if (((Ps_Table_Edit)tList[j]).Status == "拆除")
                    {
                        for (int k = int.Parse(((Ps_Table_Edit)tList[j]).FinishYear); k <= range.EndYear; k++)
                        {
                            edit.GetType().GetProperty("y" + k.ToString()).SetValue(edit, 0.0, null);
                        }
                    }
                }
                return edit.GetType().GetProperty("y" + range.EndYear).GetValue(edit, null).ToString();
            }
        }

       
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.FocusedRowHandle == -1)
            {
                MessageBox.Show("请选择一条记录");
                return;
            }
            try
            {
                string id = this.gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID").ToString();
                Ps_Table_Edit edit = Common.Services.BaseService.GetOneByKey<Ps_Table_Edit>(id);
                if (edit.Status == "扩建/改造" || edit.Status == "拆除")
                {
                    comboBoxEdit1.Text = edit.Status;
                    comboBoxEdit2.Text = edit.StartYear;
                    comboBoxEdit3.Text = edit.FinishYear;
                    spinEdit1.Value = Convert.ToDecimal(edit.Volume);
                }
            }
            catch { }
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit2.Text == "")
                return;
            comboBoxEdit3.Properties.Items.Clear();
            for (int i = int.Parse(comboBoxEdit2.Text); i <= yearRange.EndYear; i++)
            {
                comboBoxEdit3.Properties.Items.Add(i.ToString());
            }
            comboBoxEdit3.Text = Convert.ToString(int.Parse(comboBoxEdit2.Text) + 1);
        }


    }
}