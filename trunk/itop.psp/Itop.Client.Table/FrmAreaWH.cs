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
    public partial class FrmAreaWH : FormBase
    {
        public FrmAreaWH()
        {
            InitializeComponent();
            barButtonItem7.Glyph = Itop.ICON.Resource.打回重新编;
            barSubItem3.Glyph = Itop.ICON.Resource.新建; 
             barButtonItem9.Glyph = Itop.ICON.Resource.修改;
             barButtonItem11.Glyph = Itop.ICON.Resource.关闭; 
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
            IList<PS_Table_AreaWH> list = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            this.gridControl2.DataSource = list;
            
        }

        
        //添加地区
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAddPN frm = new FrmAddPN();
            frm.SetFrmName = "添加地区";
            frm.SetLabelName = "地区名称";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                PS_Table_AreaWH data = new PS_Table_AreaWH();
                data.ID += "|" + ProjectID;
                data.ProjectID = ProjectID;
                data.Title = frm.ParentName;
                data.Col1 = frm.Col1;
                data.Col2 = frm.Col2;
                Common.Services.BaseService.Create<PS_Table_AreaWH>(data);
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
                PS_Table_AreaWH _data = this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as PS_Table_AreaWH;// new Ps_Table_GDP();
                frm.Col2 = _data.Col2;
                frm.ParentName = this.gridView2.GetRowCellValue(this.gridView2.FocusedRowHandle, "Title").ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    PS_Table_AreaWH data = this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as PS_Table_AreaWH;// new Ps_Table_GDP();
                    data.Title = frm.ParentName;
                    data.Col1 = frm.Col1;
                    data.Col2 = frm.Col2;
                    Common.Services.BaseService.Update<PS_Table_AreaWH>(data);
                }
            }
            InitGrid2();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView2.FocusedRowHandle >= 0)
                if (MessageBox.Show("确定删除该地区？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    PS_Table_AreaWH data = this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as PS_Table_AreaWH;// new Ps_Table_GDP();
                    Common.Services.BaseService.Delete<PS_Table_AreaWH>(data);
                }
            InitGrid2();
        }

        

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FrmCopy frm = new FrmCopy();
                frm.CurID = ProjectID;
                frm.ClassName = "PS_Table_AreaWH";
                frm.SelectString = "SelectPS_Table_AreaWHByConn";
                frm.InsertString = "InsertPS_Table_AreaWH";
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
                Common.Services.BaseService.Update("DeletePS_Table_AreaWHByConn", conn);
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
                            PS_Table_AreaWH area = new PS_Table_AreaWH();
                            area.ID += "|" + ProjectID;
                            area.ProjectID = ProjectID;
                            foreach (DataColumn col in table.Columns)
                            {
                                if(col.ColumnName=="Yearf")
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, int.Parse(table.Rows[i][col].ToString()), null);
                                else
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, table.Rows[i][col].ToString(), null);
                            }
                            Common.Services.BaseService.Create<PS_Table_AreaWH>(area);
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
                ExportExcel.ExportToExcelOld(this.gridControl2, name + "地区", "");
            }
            catch { }
        }

    }
}