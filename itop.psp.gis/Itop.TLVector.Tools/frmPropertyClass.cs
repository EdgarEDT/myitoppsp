using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Itop.Client;

using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Common;

using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmPropertyClass : FormBase
    {
        property _prop = new property();
        UsepropertyType _propType = new UsepropertyType();
        public bool IsCreate = false;
        private DataTable propdt;
        private DataTable dt;
        private string TypeUid = Guid.NewGuid().ToString();
        
        public frmPropertyClass()
        {
            InitializeComponent();
        }
        public void InitData()
        {
            propdt =Itop.Common.DataConverter.ToDataTable( Services.BaseService.GetList("SelectUsepropertyTypeList", _propType),typeof(UsepropertyType));
            propdt.AcceptChanges();
            gridControl2.DataSource = propdt;
            if(propdt.Rows.Count>0){
                _prop.TypeUID = propdt.Rows[0]["UID"].ToString();
                dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectpropertyByTypeUID", _prop), typeof(property));
                gridControl1.DataSource = dt;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                    _propType.UID = TypeUid;
                    foreach(DataRow row1 in propdt.Rows){
                       if(row1.RowState==DataRowState.Added){
                           Services.BaseService.Create<UsepropertyType>(Itop.Common.DataConverter.RowToObject<UsepropertyType>(row1));
                       }
                       if(row1.RowState==DataRowState.Modified){
                           Services.BaseService.Update<UsepropertyType>(Itop.Common.DataConverter.RowToObject<UsepropertyType>(row1));
                       }
                  
                    }
                    foreach (DataRow row2 in dt.Rows)
                    {
                        if (row2.RowState == DataRowState.Added)
                        {
                            Services.BaseService.Create<property>(Itop.Common.DataConverter.RowToObject<property>(row2));
                        }
                        if (row2.RowState == DataRowState.Modified)
                        {
                            Services.BaseService.Update<property>(Itop.Common.DataConverter.RowToObject<property>(row2));
                        }
                       
                    }
                this.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void gridControl1_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
   
            if (e.Button.Tag.ToString() == "1")
            {
                xtraTabControl1.Focus();
                gridView1.AddNewRow();
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "UID", Guid.NewGuid().ToString());
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "UseUID", "0");
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TypeUID", TypeUid);

                gridView1.UpdateCurrentRow();
            }
            if(e.Button.Tag.ToString()=="2"){
                if (gridView1.RowCount < 1) return;
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                Services.BaseService.Delete<property>(Itop.Common.DataConverter.RowToObject<property>(row));
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frmUsePropertySelect f = new frmUsePropertySelect();
            f.Show();
           // this.Close();
        }

      
        private void gridControl2_Click(object sender, EventArgs e)
        {
            
             
        }

        private void gridControl2_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            
            if (e.Button.Tag.ToString() == "1")
            {
                xtraTabControl1.Focus();
                gridView2.AddNewRow();
                TypeUid = Guid.NewGuid().ToString();
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "UID", TypeUid);
                gridView2.UpdateCurrentRow();
            }
            if (e.Button.Tag.ToString() == "2")
            {

                if (gridView2.RowCount < 1) return;
                DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                Services.BaseService.Delete<UsepropertyType>(Itop.Common.DataConverter.RowToObject<UsepropertyType>(row));
                gridView2.DeleteRow(gridView2.FocusedRowHandle);
            }
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr == null)
                return;
            _prop.TypeUID = dr["UID"].ToString();
            TypeUid = dr["UID"].ToString();
            dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectpropertyByTypeUID", _prop), typeof(property));
            dt.AcceptChanges();
            gridControl1.DataSource = dt;
        }

        private void frmPropertyClass_Load(object sender, EventArgs e)
        {
            InitData();
        }
    }
}