using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Table;
using Itop.Client.Stutistics;

namespace Itop.Client.Table
{
    public partial class FrmPowerState : FormBase
    {
        public FrmPowerState()
        {
            InitializeComponent();
        }

        private void FrmAreaData_Load(object sender, EventArgs e)
        {
            InitGrid2();
            InitGrid1();
            this.gridView2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridView2_FocusedRowChanged);
        }

        void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            InitGrid1();
        }

        public string ProjectID
        {
            get { return ProjectUID; }
        }

        public void InitGrid2()
        {
            IList<PSP_PowerSubstationInfo> list = new List<PSP_PowerSubstationInfo>();
            PSP_PowerSubstationInfo info = new PSP_PowerSubstationInfo();
            info.Title = "现状";
            list.Add(info);
            PSP_PowerSubstationInfo info1 = new PSP_PowerSubstationInfo();
            info1.Title = "规划";
            list.Add(info1);
            this.gridControl2.DataSource = list;
            
        }

        public void InitGrid1()
        {
            if (this.gridView2.FocusedRowHandle >= 0)
            {
                string id = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Title").ToString();
                if (id == "现状")
                { id = "1"; this.gridView1.Columns["S3"].Visible = false; }
                else if (id == "规划")
                { id = "2"; this.gridView1.Columns["S3"].Visible = true; }
                string conn = " AreaID='" + ProjectID + "' and Flag='" + id + "' order by CreateDate";
                IList<PSP_PowerSubstationInfo> list1 = Common.Services.BaseService.GetList<PSP_PowerSubstationInfo>("SelectPSP_PowerSubstationInfoByConn", conn);
                this.gridControl1.DataSource = list1;
            }
        }
        //添加地区
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        public void DeleteChild(string id)
        {
            
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
            {
                FrmAddPowerState frm = new FrmAddPowerState();
                frm.Area = this;
                frm.Text = "添加记录";
                frm.IsCreate = true;
                frm.ProjectID = ProjectID;
                string f = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Title").ToString();
                if (f == "现状")
                    f = "1";
                else if (f == "规划")
                    f = "2";
                frm.Flag = f;
                PSP_PowerSubstationInfo obj = new PSP_PowerSubstationInfo();
                obj.UID = Guid.NewGuid().ToString();
                frm.Object = obj;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    frm.Close();
                    InitGrid1();
                }
            }
            else
            {
                MessageBox.Show("请先选择规划或现状", "添加", MessageBoxButtons.OK);
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                FrmAddPowerState frm = new FrmAddPowerState();
                frm.Text = "修改记录";
                frm.ProjectID = ProjectID;
                string f = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Title").ToString();
                if (f == "现状")
                    f = "1";
                else if (f == "规划")
                    f = "2";
                frm.Flag = f;
                PSP_PowerSubstationInfo obj = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as PSP_PowerSubstationInfo;
                frm.Object = obj;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    frm.Close();
                    InitGrid1();
                }
            }
            else
            {
                MessageBox.Show("请先选择一条记录", "修改", MessageBoxButtons.OK);
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
                if (MessageBox.Show("确定删除该记录？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    PSP_PowerSubstationInfo data = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as PSP_PowerSubstationInfo;// new PSP_PowerSubstationInfo();
                    string conn = "UID='" + data.UID + "'";
                    Common.Services.BaseService.Update("DeletePSP_PowerSubstationInfoByConn", conn);
                }
            InitGrid1();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FrmCopy frm = new FrmCopy();
                frm.CurID = ProjectID;
                frm.idName = "UID";
                frm.projectName = "AreaID";
                frm.bPare = false;
                frm.ClassName = "Itop.Client.Stutistics.PSP_PowerSubstationInfo";
                frm.AssName = "Itop.Domain.Stutistics";
                frm.SelectString = "SelectPSP_PowerSubstationInfoByConn";
                frm.InsertString = "InsertPSP_PowerSubstationInfo";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("导入成功");
                    InitGrid2();
                }
            }
            catch { }
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("该操作将清除当前项目的所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string conn = "AreaID='" + ProjectID + "'";
                Common.Services.BaseService.Update("DeletePSP_PowerSubstationInfoByConn", conn);
                this.gridControl1.DataSource = null;
                this.gridControl2.DataSource = null;
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView2.FocusedRowHandle > -1)
            {
                IList<string> filedList = new List<string>();
                IList<string> capList = new List<string>();
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    capList.Add(gridView1.Columns[i].Caption);
                    filedList.Add(gridView1.Columns[i].FieldName);
                }
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DataTable table = OperTable.GetExcel(op.FileName, filedList, capList);
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if (table.Rows[i][0].ToString().IndexOf("合计") > 0 || table.Rows[i][1].ToString().IndexOf("合计") > 0)
                                continue;
                            PSP_PowerSubstationInfo area = new PSP_PowerSubstationInfo();
                            area.UID =Guid.NewGuid().ToString()+ "|" + ProjectID;
                            area.AreaID = ProjectID;
                            area.CreateDate = DateTime.Now;
                            string f = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Title").ToString();
                            if (f == "现状")
                                f = "1";
                            else if (f == "规划")
                                f = "2";
                            area.Flag = f;
                            foreach (DataColumn col in table.Columns)
                            {
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, table.Rows[i][col].ToString(), null);
                            }
                            Common.Services.BaseService.Create<PSP_PowerSubstationInfo>(area);
                        }
                    }
                    catch (Exception a) { MessageBox.Show(a.Message); }
                    InitGrid1();
                }
            }
            else
                MessageBox.Show("没有选择规划或现状","导入EXCEL",MessageBoxButtons.OK);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string name = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Title").ToString();
                ExportExcel.ExportToExcelOld(this.gridControl1, name + "电源数据", "");
            }
            catch { }
        }

    }
}