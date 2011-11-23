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
    public partial class FrmArea_TYPE : FormBase
    {
        public FrmArea_TYPE()
        {
            InitializeComponent();
        }

        private void FrmAreaData_Load(object sender, EventArgs e)
        {
            InitGrid2();
        }

       

        public string ProjectID
        {
            get { return ProjectUID; }
        }

        public void InitGrid2()
        { 
            string conn = "ProjectID='" + ProjectID + "' order by Sort";
            IList<PS_Table_Area_TYPE> list = Common.Services.BaseService.GetList<PS_Table_Area_TYPE>("SelectPS_Table_Area_TYPEByConn", conn);
            this.gridControl2.DataSource = list;
            
        }

        
        //添加类型
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAddPN_TYPE frm = new FrmAddPN_TYPE();
            frm.SetFrmName = "添加类型";
            frm.SetLabelName = "类型名称";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PS_Table_Area_TYPE data = new PS_Table_Area_TYPE();
                data.ID += "|" + ProjectID;
                data.ProjectID = ProjectID;
                data.Title = frm.ParentName;
                data.Col1 = frm.Col1;
                Common.Services.BaseService.Create<PS_Table_Area_TYPE>(data);
            }
            InitGrid2();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
            {
                FrmAddPN_TYPE frm = new FrmAddPN_TYPE();
                frm.SetFrmName = "添加类型";
                frm.SetLabelName = "类型名称";
                frm.ParentName = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Title").ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    PS_Table_Area_TYPE data = this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as PS_Table_Area_TYPE;// new Ps_Table_GDP();
                    data.Title = frm.ParentName;
                    data.Col1 = frm.Col1;
                    Common.Services.BaseService.Update<PS_Table_Area_TYPE>(data);
                }
            }
            InitGrid2();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
                if (MessageBox.Show("确定删除该类型？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    PS_Table_Area_TYPE data = this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as PS_Table_Area_TYPE;// new Ps_Table_GDP();
                    Common.Services.BaseService.Delete<PS_Table_Area_TYPE>(data);
                }
            InitGrid2();
        }

        

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FrmCopy frm = new FrmCopy();
                frm.CurID = ProjectID;
                frm.ClassName = "PS_Table_Area_TYPE";
                frm.SelectString = "SelectPS_Table_Area_TYPEByConn";
                frm.InsertString = "InsertPS_Table_Area_TYPE";
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
                Common.Services.BaseService.Update("DeletePS_Table_Area_TYPEByConn", conn);
                this.gridControl2.DataSource = null;
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
                IList<string> filedList = new List<string>();
                IList<string> capList = new List<string>();
                for (int i = 0; i < gridView2.Columns.Count; i++)
                {
                    if (gridView2.Columns[i].Visible == true)
                    {
                        capList.Add(gridView2.Columns[i].Caption);
                        filedList.Add(gridView2.Columns[i].FieldName);
                    }
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
                            if (table.Rows[i][0].ToString().IndexOf("建表时间") >= 0 )
                                continue;
                            PS_Table_Area_TYPE area = new PS_Table_Area_TYPE();
                            area.ID += "|" + ProjectID;
                            area.ProjectID = ProjectID;
                            foreach (DataColumn col in table.Columns)
                            {
                                if(col.ColumnName=="Yearf")
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, int.Parse(table.Rows[i][col].ToString()), null);
                                else
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, table.Rows[i][col].ToString(), null);
                            }
                            Common.Services.BaseService.Create<PS_Table_Area_TYPE>(area);
                        }
                    }
                    catch { }
                    InitGrid2();
                }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string name = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Title").ToString();
                ExportExcel.ExportToExcelOld(this.gridControl2, name + "类型", "");
            }
            catch { }
        }

    }
}