using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using Itop.Common;
using Itop.Domain.PWTable;
using Itop.Client.Base;
using Itop.Client.Common;

namespace Itop.Client.PWTable
{
    public partial class FrmPW3212 :Itop.Client.Base.FormBase
    {
        string type = "";
        public bool addright = true;
        public bool editright = true;
        public bool deletetright = true;
        public bool printright = true;
        /// <summary>
        /// ��ȡGridControl����
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        public FrmPW3212()
        {
            InitializeComponent();
        }

        private void FrmProject_Sum_Load(object sender, EventArgs e)
        {
            //this.gridBand7.Columns.Add(this.bandedGridColumn7);
            try
            {
                InitRight();
                PW_tb3a p = new PW_tb3a();
                p.col1 = "'����'";
                p.col2 = Itop.Client.MIS.ProgUID;
                IList<PW_tb3a> list = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy3212", p);
                this.gridControl.DataSource = SumList(list);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        public IList<PW_tb3a> SumList(IList<PW_tb3a> _list)
        {
            IList<PW_tb3a> l = _list;
            if (_list.Count > 0)
            {
                PW_tb3a sum = new PW_tb3a();
                sum.PQName = "�����ϼ�";
                for (int i = 0; i < _list.Count; i++)
                {
                    sum.Num1 = sum.Num1 + _list[i].Num1;
                    sum.Num2 = sum.Num2 + _list[i].Num2;
                    sum.Num3 = sum.Num3 + _list[i].Num3;
                    sum.Num4 = sum.Num4 + _list[i].Num4;
                    sum.Num5 = sum.Num5 + _list[i].Num5;
                    sum.Num6 = sum.Num6 + _list[i].Num6;
                    sum.Num7 = sum.Num7 + _list[i].Num7;
                    sum.Num8 = sum.Num8 + _list[i].Num8;
                    sum.FZL = sum.FZL + _list[i].FZL;
                    sum.KBS = sum.KBS + _list[i].KBS;
                    sum.KG = sum.KG + _list[i].KG;
                    sum.LineLength = sum.LineLength + _list[i].LineLength;
                    sum.MaxFH = sum.MaxFH + _list[i].MaxFH;
                    sum.SafeFH = sum.SafeFH + _list[i].SafeFH;
                    sum.WG1 = sum.WG1 + _list[i].WG1;
                    sum.WG2 = sum.WG2 + _list[i].WG2;


                }
                _list.Add(sum);
            }
            return l;
        }
        private void InitRight()
        {
            if (!AddRight)
            {
                barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!EditRight)
            {
                barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //ctrlProject_Sum1.editright = false;
            }

            if (!DeleteRight)
            {
                barButtonDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!PrintRight)
            {
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                
            }
        }
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ctrlProject_Sum1.AddObject();
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ctrlProject_Sum1.UpdateObject();
        }

        private void barButtonDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ctrlProject_Sum1.DeleteObject();
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ctrlProject_Sum1.PrintPreview();
            ComponentPrint.ShowPreview(this.gridControl, "��������10kV��·����ģʽͳ��");
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.gridControl);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //InsertSubstation_Info();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            if(comboBox1.Text=="����"){
                str = "'����'";
            }
            if (comboBox1.Text == "ר��")
            {
                str = "'ר��'";
            }
            if (comboBox1.Text == "ȫ��")
            {
                str = "'����','ר��'";
            }
            try
            {
                PW_tb3a p = new PW_tb3a();
                p.col1 = str;
                p.col2 = Itop.Client.MIS.ProgUID;
                IList<PW_tb3a> list = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy3212", p);
                this.gridControl.DataSource =SumList( list);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void gridControl_Click(object sender, EventArgs e)
        {

        }

        //private DataTable GetExcel(string filepach) 
        //{
        //    string str;
        //    FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

        //    try
        //    {
        //        fpSpread1.OpenExcel(filepach);
        //    }
        //    catch
        //    {
        //        string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
        //        File.Copy(filepach, filepath1);
        //        fpSpread1.OpenExcel(filepath1);
        //        File.Delete(filepath1);
        //    }
        //    DataTable dt = new DataTable();
        //    Hashtable h1 = new Hashtable();
        //    int aa = 0;
        //    for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
        //    {
        //        bool bl = false;
        //        GridColumn gc = this.ctrlProject_Sum1.GridView.VisibleColumns[k - 1];
        //        dt.Columns.Add(gc.FieldName);
        //        h1.Add(aa.ToString(), gc.FieldName);
        //        aa++;
        //    }

        //    int m = 1;
        //    for (int i = m; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; i++)
        //    {
        //        DataRow dr = dt.NewRow();
        //        str = "";
        //        for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
        //        {
        //            str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
        //            dr[h1[j.ToString()].ToString()] = fpSpread1.Sheets[0].Cells[i, j].Text;
        //        }
        //        if (str != "")
        //            dt.Rows.Add(dr);

        //    }
        //    return dt;
        //}
        //private void InsertSubstation_Info()
        //{
        //    string columnname = "";

        //    try
        //    {
        //        DataTable dts = new DataTable();
        //        OpenFileDialog op = new OpenFileDialog();
        //        op.Filter = "Excel�ļ�(*.xls)|*.xls";
        //        if (op.ShowDialog() == DialogResult.OK)
        //        {
        //            dts = GetExcel(op.FileName);
        //            IList<Project_Sum> lii = new List<Project_Sum>();
        //            DateTime s8 = DateTime.Now;
        //            for (int i = 0; i < dts.Rows.Count; i++)
        //            {
                     

        //                Project_Sum l1 = new Project_Sum();
        //                foreach (DataColumn dc in dts.Columns)
        //                {
        //                    columnname = dc.ColumnName;
        //                    //if (dts.Rows[i][dc.ColumnName].ToString() == "")
        //                    //    continue;

        //                      switch (dc.ColumnName)
        //                      {
        //                          //case "L2":
        //                          //case "L9":
        //                          case "Num":
        //                              double LL2 = 0;
        //                              try
        //                              {
        //                                  LL2 = Convert.ToDouble(dts.Rows[i][dc.ColumnName].ToString());
        //                              }
        //                              catch { }
        //                              l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, LL2, null);
        //                              break;

        //                          //case "L1":
        //                          //case "L3":
        //                          //    int LL3 = 0;
        //                          //    try
        //                          //    {
        //                          //        LL3 = Convert.ToInt32(dts.Rows[i][dc.ColumnName].ToString());
        //                          //    }
        //                          //    catch { }
        //                          //    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, LL3, null);
        //                          //    break;

        //                          default:
        //                              l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName].ToString(), null);
        //                              break;
        //                      }
        //                  }
        //                  l1.S5 = type;
        //                  //l1.CreateDate = s8.AddSeconds(i);
        //                  lii.Add(l1);
        //              }

        //              foreach (Project_Sum lll in lii)
        //              {
        //                  ////////if (lll.Name == "")
        //                  ////////    continue;
        //                  Project_Sum l1 = new Project_Sum();
        //                  ////////l1.Name = lll.Name;

        //                  ////////l1.S5 = type;
        //                  ////////object obj = Services.BaseService.GetObject("SelectProject_SumByNameandS5", l1);

        //                  IList<Project_Sum> list = new List<Project_Sum>();
        //                  if (type == "1")
        //                  {
        //                      l1.S1 = lll.S1;
        //                      l1.L1 = lll.L1;
        //                      l1.S5 = type;
        //                      list = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue2", l1);

        //                  }
        //                  else if (type == "2")
        //                  {
        //                      l1.S1 = lll.S1;
        //                      l1.T1 = lll.T1;
        //                      l1.T5 = lll.T5;
        //                      l1.S5 = type;
        //                      list = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue3", l1);
        //                  }


        //                  if (list.Count>0)
        //                  {
        //                      lll.UID = list[0].UID;   
        //                      Services.BaseService.Update<Project_Sum>(lll);
        //                  }
        //                  else
        //                  {
        //                      lll.UID = Guid.NewGuid().ToString();
        //                      Services.BaseService.Create<Project_Sum>(lll);
        //                  }
        //              }
        //              this.ctrlProject_Sum1.RefreshData();
        //        }
        //    }
        //    catch (Exception ex) 
        //    { 
        //        MsgBox.Show(columnname + ex.Message); 
        //        MsgBox.Show("�����ʽ����ȷ��"); 
                                
        //    }
        //}

    }
}