using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;
using Itop.Common;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;
using Itop.Client.Stutistics;
using System.Xml;
using ItopVector.Tools;
using Itop.Client.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
namespace Itop.TLPsp.Graphical {
    public partial class UcPddate : DevExpress.XtraEditors.XtraUserControl {
        public UcPddate() {
            InitializeComponent();
        }
        private DataTable datatable = new DataTable();
        private string parentID;
        public string ParentID {
            get { return parentID; }
            set {
                parentID = value;
                if (!string.IsNullOrEmpty(value)) {
                    RefreshData("ParentID='" + value + "' order by TDdatetime");
                }
            }
        }
        PDrelregion parentObj = new PDrelregion();
        public PDrelregion ParentObj {
            get { return parentObj; }
            set {

                parentObj = value;
                if (value == null) {
                    parentID = null;
                } else {
                    ParentID = value.ID;
                }
            }
        }
        private void RefreshData(string con)
        {
            if (datatable != null) {
                datatable.Columns.Clear();
                gridView1.Columns.Clear();
            }
            AddFixColumn();
            IList<PDrelcontent> pl = Itop.Client.Common.Services.BaseService.GetList<PDrelcontent>("SelectPDrelcontentByWhere",con);
            datatable = Itop.Common.DataConverter.ToDataTable((IList)pl, typeof(PDrelcontent));
            gridControl1.DataSource = datatable;
        }
          private void AddFixColumn()
        {

            GridColumn column = new GridColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;  
            this.gridView1.Columns.Add(column);

            column = new GridColumn();
            column.FieldName = "TDdatetime";
            column.Caption = "停电日期";
            column.VisibleIndex = 0;
            column.Width = 120;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "TDtime";
            column.Caption = "停电持续时间";
            column.VisibleIndex = 1;
            column.Width = 100;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "PeopleRegion";
            column.Caption = "用户范围";
            column.VisibleIndex = 2;
            column.Width = 70;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "TDtype";
            column.Caption = "停电类型";
            column.VisibleIndex = 3;
            column.Width = 120;
            this.gridView1.Columns.Add(column);
            column = new GridColumn();
            column.FieldName = "AvgFH";
            column.Caption = "平均符合（KW）";
            column.VisibleIndex = 4;
            column.Width = 120;
            this.gridView1.Columns.Add(column);
        }
        
          private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
              PdDateEdit PDT = new PdDateEdit();
              PDT.parentobj = ParentObj;
              PDrelcontent pdr = new PDrelcontent();
              pdr.ParentID = ParentObj.ID;
              PDT.RowData = pdr;
              if (PDT.ShowDialog() == DialogResult.OK) {

                  pdr = PDT.RowData;
                  if (pdr.TDdatetime.Year!=ParentObj.Year)
                  {
                      Itop.Common.MsgBox.ShowYesNo("停电日期和年份不符！");
                      return;
                  }
                  Itop.Client.Common.Services.BaseService.Create<PDrelcontent>(pdr);
                  
                  //datatable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                  ((DataTable)gridControl1.DataSource).Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                  //gridControl1.DataSource = datatable;
              }
          }

          private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
              if (row != null) {
                  PDrelcontent PD = Itop.Common.DataConverter.RowToObject<PDrelcontent>(row);
                  PdDateEdit PDT = new PdDateEdit();
                  PDT.parentobj = ParentObj;
                  PDT.RowData = PD;
                  if (PDT.ShowDialog() == DialogResult.OK) {

                     PD = PDT.RowData;
                     if (PD.TDdatetime.Year != ParentObj.Year) {
                         Itop.Common.MsgBox.ShowYesNo("停电日期和年份不符！");
                         return;
                     }
                      Itop.Client.Common.Services.BaseService.Update<PDrelcontent>(PD);
                      parentID = ParentObj.ID;
                      //datatable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                      //((DataTable)gridControl1.DataSource).Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, datatable.NewRow()));
                      //gridControl1.DataSource = datatable;
                  }
              }
          }
  

          private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
              DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
              if (row != null) {
                  PDrelcontent dev = Itop.Common.DataConverter.RowToObject<PDrelcontent>(row);
                  if (Itop.Common.MsgBox.ShowYesNo("是否确认删除?") == DialogResult.Yes) {
                      Itop.Client.Common.Services.BaseService.Delete<PDrelcontent>(dev);
                      ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                  }
              }
          }

          private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
              FileClass.ExportExcel(this.gridControl1);
          }

          private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
              InsertInfo();
          }

          private DataTable GetExcel(string filepach) {
              string str;
              FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

              try {
                  fpSpread1.OpenExcel(filepach);
              } catch {
                  string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
                  File.Copy(filepach, filepath1);
                  fpSpread1.OpenExcel(filepath1);
                  File.Delete(filepath1);
              }
              DataTable dt = new DataTable();
              Hashtable h1 = new Hashtable();
              int aa = 0;
              for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++) {
                  bool bl = false;
                  GridColumn gc = gridView1.VisibleColumns[k - 1];
                  dt.Columns.Add(gc.FieldName);
                  h1.Add(aa.ToString(), gc.FieldName);
                  aa++;
              }

              int m = 1;
              for (int i = m; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; i++) {
                  DataRow dr = dt.NewRow();
                  str = "";
                  for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++) {
                      str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
                      dr[h1[j.ToString()].ToString()] = fpSpread1.Sheets[0].Cells[i, j].Text;
                  }
                  if (str != "")
                      dt.Rows.Add(dr);

              }
              return dt;
          }
          private void InsertInfo() {
              string columnname = "";

              try {
                  DataTable dts = new DataTable();
                  OpenFileDialog op = new OpenFileDialog();
                  op.Filter = "Excel文件(*.xls)|*.xls";
                  if (op.ShowDialog() == DialogResult.OK) {
                      dts = GetExcel(op.FileName);
                      IList<PDrelcontent> lii = new List<PDrelcontent>();
                      DateTime s8 = DateTime.Now;
                      for (int i = 0; i < dts.Rows.Count; i++) {

                    
                          PDrelcontent l1 = new PDrelcontent();
                          foreach (DataColumn dc in dts.Columns) {
                              columnname = dc.ColumnName;
                              //if (dts.Rows[i][dc.ColumnName].ToString() == "")
                              //    continue;
                              if (columnname == "TDdatetime" && Convert.ToDateTime(dts.Rows[i][dc.ColumnName]).Year != ParentObj.Year)
                              {
                                  MessageBox.Show("第'" + (i + 1) + "'行停电日期和年份不符！");
                                  break;
                              }
                              l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName].ToString(), null);


                          }
                          
                          lii.Add(l1);
                      }

                      foreach (PDrelcontent lll in lii) {

                         PDrelcontent l1 = new PDrelcontent();

                          IList<PDrelcontent> list = new List<PDrelcontent>();


                          //{
                          lll.ID = Guid.NewGuid().ToString();
                          lll.ParentID = parentID;
                          Services.BaseService.Create<PDrelcontent>(lll);
                          //}
                      }
                      ParentID = ParentObj.ID;  //重新显示数据

                  }
              } catch (Exception ex) {
                  MsgBox.Show(columnname + ex.Message);
                  MsgBox.Show("导入格式不正确！");

              }
          }
    }
}
