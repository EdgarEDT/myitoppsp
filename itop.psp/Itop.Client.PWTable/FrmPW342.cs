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
    public partial class FrmPW342 :Itop.Client.Base.FormBase
    {
        string type = "";
        public bool addright = true;
        public bool editright = true;
        public bool deletetright = true;
        public bool printright = true;
        /// <summary>
        /// 获取GridControl对象
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        public FrmPW342()
        {
            InitializeComponent();
        }

        private void FrmProject_Sum_Load(object sender, EventArgs e)
        {
            InitRight();
            this.gridBand10.Columns.Add(this.bandedGridColumn8);
            this.gridBand11.Columns.Add(this.bandedGridColumn9);
            this.gridBand12.Columns.Add(this.bandedGridColumn10);
            try
            {
                PW_tb3a p = new PW_tb3a();
                p.col1 = "'局属'";
                p.col2 = Itop.Client.MIS.ProgUID;
                IList<PW_tb3a> list = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy342", p);
              
               
                this.gridControl.DataSource = GetList(list);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        public IList<PW_tb3a> GetList(IList<PW_tb3a> list)
        {
            IList<PW_tb3a> list2 = new List<PW_tb3a>();
            for (int i = 0; i < list.Count; i++)
            {
                decimal nump = 0;
                PW_tb3a a = list[i];
                if (a.PQName != "")
                {

                    if (a.PQName.Contains("+") || a.PQName.Contains("＋"))
                    {
                        a.PQName = a.PQName.Replace("＋", "+");
                        string[] str = a.PQName.Split("+".ToCharArray());
                        for (int k = 0; k < str.Length; k++)
                        {
                            str[k] = str[k].Replace("×", "*");
                        }
                        decimal[] num = new decimal[str.Length];
                        for (int n = 0; n < num.Length; n++)
                        {
                            num[n] = Convert.ToDecimal(new DataTable().Compute(str[n], null));
                        }
                        if (num[0] > num[1])
                        {
                            a.Num4 =Convert.ToDecimal( Convert.ToDouble( num[1]) * 1.235);
                        }
                        else
                        {
                            a.Num4 =Convert.ToDecimal( Convert.ToDouble(num[0]) * 1.235);
                        }
                    }
                    if (a.PQName.Contains("×"))
                    {
                        string[] str = a.PQName.Split("×".ToCharArray());
                        decimal[] num = new decimal[str.Length];
                        for (int n = 0; n < num.Length; n++)
                        {
                            num[n] = Convert.ToDecimal(new DataTable().Compute(str[n], null));
                        }
                        if (num[0] == 1)
                        {
                            a.Num4 = 0;
                        }
                        else
                        {
                            a.Num4 = Convert.ToDecimal(Convert.ToDouble(num[1]) * 1.235);
                        }
                    }
                    else
                    {
                        a.Num4 = 0;
                    }

                }
                else
                {
                    a.Num4 = 0;
                }
                ///////////////////////////////////////////////////////////////
                if (a.LLXMC.Trim() != "")
                {
                    string[] str = a.LLXMC.Split(",".ToCharArray());
                    for (int j = 0; j < str.Length; j++)
                    {
                        if (str[j] != "")
                        {
                            PW_tb3a pp = new PW_tb3a();
                            pp.LineName = str[j];
                            pp.col2 = Itop.Client.MIS.ProgUID;
                            pp = (PW_tb3a)Services.BaseService.GetObject("SelectPW_tb3aByLineName", pp);
                            nump = nump + (pp.SafeFH - pp.MaxFH);
                        }
                    }
                }
                if (nump > a.MaxFH)
                {
                    a.Num6 = a.MaxFH;
                }
                else
                {
                    a.Num6 = nump;
                }

                //if (a.Num3 == 0)
                //{
                //    a.col1 = "0";
                //}
                //else
                //{
                //    a.col1 = Convert.ToDecimal((a.Num3-(a.Num4 + a.Num6)) / a.Num3 * 100).ToString("##.##") + "%";
                //}

                //if (a.Num4 + a.Num6 > a.Num3)
                //{
                //    a.col2 = "是";
                //    a.col1 = "0";
                //}
                //else
                //{
                //    if ((a.Num4 + a.Num6) / a.Num3 * 100 < 100)
                //    {
                //        a.col2 = "否";
                //    }
                //    else
                //    {
                //        a.col2 = "是";
                //    }
                //}
                list2.Add(a);
            }
            /////////////////////////////
            IList<PW_tb3a> list3 = new List<PW_tb3a>();
            if (list2.Count > 0)
            {
                PW_tb3a pp = list[0];
                for (int i = 1; i < list2.Count; i++)
                {
                    PW_tb3a pp2 = list2[i];
                    if (pp2.SubName == pp.SubName)
                    {
                        pp.Num6 = pp.Num6  + pp2.Num6;
                    }
                    else
                    {
                        list3.Add(pp);
                        pp = list2[i];
                    }
                }
                list3.Add(pp);
            }
            for (int n = 0; n < list3.Count; n++)
            {
                PW_tb3a a = list3[n];
                if (a.Num3 == 0)
                {
                    a.col1 = "0";
                }
                else
                {
                    a.col1 = Convert.ToDecimal((a.Num3 - (a.Num4 + a.Num6)) / a.Num3 * 100).ToString("##.##") + "%";
                }

                if (a.Num4 + a.Num6 > a.Num3)
                {
                    a.col2 = "是";
                    a.col1 = "0";
                }
                else
                {
                    if ((a.Num4 + a.Num6) / a.Num3 * 100 < 100)
                    {
                        a.col2 = "否";
                    }
                    else
                    {
                        a.col2 = "是";
                    }
                }
            }

            return list3;
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
            ComponentPrint.ShowPreview(this.gridControl, "市区110kV主变“N-1”校验结果");
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
            if(comboBox1.Text=="局属"){
                str = "'局属'";
            }
            if (comboBox1.Text == "专用")
            {
                str = "'专用'";
            }
            if (comboBox1.Text == "全部")
            {
                str = "'局属','专用'";
            }
            try
            {
                PW_tb3a p = new PW_tb3a();
                p.col1 = str;
                p.col2 = Itop.Client.MIS.ProgUID;
                IList<PW_tb3a> list = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy342", p);
               
                this.gridControl.DataSource = GetList(list);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
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
        //        op.Filter = "Excel文件(*.xls)|*.xls";
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
        //        MsgBox.Show("导入格式不正确！"); 
                                
        //    }
        //}

    }
}