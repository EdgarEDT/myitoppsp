using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Table;

namespace Itop.Client.Table
{
    public partial class FrmAreaData : FormBase
    {
        public FrmAreaData()
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
            string conn = " ParentID='0' and ProjectID='" + ProjectID + "' order by SortID";
            IList<Ps_Table_AreaData> list = Common.Services.BaseService.GetList<Ps_Table_AreaData>("SelectPs_Table_AreaDataByConn", conn);
            this.gridControl2.DataSource = list;
            
        }

        public void InitGrid1()
        {
            if (this.gridView2.FocusedRowHandle >= 0)
            {
                string id = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ID").ToString();
                string conn = " ProjectID='" + ProjectID + "' and ParentID='" + id + "' order by SortID";
                IList<Ps_Table_AreaData> list1 = Common.Services.BaseService.GetList<Ps_Table_AreaData>("SelectPs_Table_AreaDataByConn", conn);
                this.gridControl1.DataSource = list1;
            }
        }
        //添加地区
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAddPN frm = new FrmAddPN();
            frm.SetFrmName = "添加地区";
            frm.SetLabelName = "地区名称";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_AreaData data = new Ps_Table_AreaData();
                data.ID += "|" + ProjectID;
                data.ParentID = "0";
                data.ProjectID = ProjectID;
                data.SortID = OperTable.GetAreaMaxSort()+1;
                data.Area = frm.ParentName;
                Common.Services.BaseService.Create<Ps_Table_AreaData>(data);
            }
            InitGrid2();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
            {
                FrmAddPN frm = new FrmAddPN();
                frm.SetFrmName = "修改地区";
                frm.SetLabelName = "地区名称";
                frm.ParentName = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Area").ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    Ps_Table_AreaData data = this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as Ps_Table_AreaData;// new Ps_Table_AreaData();
                    data.Area = frm.ParentName;
                    Common.Services.BaseService.Update<Ps_Table_AreaData>(data);
                }
            }
            InitGrid2();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
                if (MessageBox.Show("确定删除该地区？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Ps_Table_AreaData data = this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as Ps_Table_AreaData;// new Ps_Table_AreaData();
                    DeleteChild(data.ID);
                    Common.Services.BaseService.Delete<Ps_Table_AreaData>(data);
                }
            InitGrid2();
        }

        public void DeleteChild(string id)
        {
            IList<Ps_Table_AreaData> list = Common.Services.BaseService.GetList<Ps_Table_AreaData>("SelectPs_Table_AreaDataByConn", " ParentID='" + id + "' and ProjectID='" + ProjectID + "'");
            foreach (Ps_Table_AreaData area in list)
            {
                Common.Services.BaseService.Delete<Ps_Table_AreaData>(area);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
            {
                FrmAddPopu frm = new FrmAddPopu();
                frm.Area = this;
                frm.Text = "添加记录";
                frm.IsCreate = true;
                frm.ProjectID = ProjectID;
                frm.ParentID = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ID").ToString();
                Ps_Table_AreaData obj = new Ps_Table_AreaData();
                frm.Object = obj;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    frm.Close();
                    InitGrid1();
                }
            }
            else
            {
                MessageBox.Show("请先选择一个地区", "添加", MessageBoxButtons.OK);
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                FrmAddPopu frm = new FrmAddPopu();
                frm.Text = "修改记录";
                frm.ProjectID = ProjectID;
                frm.ParentID = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ID").ToString();
                Ps_Table_AreaData obj = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Ps_Table_AreaData;
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
                    Ps_Table_AreaData data = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Ps_Table_AreaData;// new Ps_Table_AreaData();
                    Common.Services.BaseService.Delete<Ps_Table_AreaData>(data);
                }
            InitGrid1();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FrmCopy frm = new FrmCopy();
                frm.CurID = ProjectID;
                frm.ClassName = "Ps_Table_AreaData";
                frm.SelectString = "SelectPs_Table_AreaDataByConn";
                frm.InsertString = "InsertPs_Table_AreaData";
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
                string conn = "ProjectID='" + ProjectID + "'";
                Common.Services.BaseService.Update("DeletePs_Table_AreaDataByConn", conn);
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
                            Ps_Table_AreaData area = new Ps_Table_AreaData();
                            area.ID += "|" + ProjectID;
                            area.SortID = OperTable.GetAreaMaxSort()+1;
                            area.ParentID = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "ID").ToString();
                            area.ProjectID = ProjectID;
                            foreach (DataColumn col in table.Columns)
                            {
                                if(col.ColumnName=="Yearf")
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, int.Parse(table.Rows[i][col].ToString()), null);
                                else
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, double.Parse(table.Rows[i][col].ToString()), null);
                            }
                            Common.Services.BaseService.Create<Ps_Table_AreaData>(area);
                        }
                    }
                    catch { }
                    InitGrid1();
                }
            }
            else
                MessageBox.Show("没有选择地区（如果没有请添加一个）","导入EXCEL",MessageBoxButtons.OK);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string name = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Area").ToString();
                ExportExcel.ExportToExcelOld(this.gridControl1, name + "土地面积和人口数据", "");
            }
            catch { }
        }

    }
}