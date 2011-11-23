using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using Itop.Common;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class Form14Result : FormBase
    {
        private DataTable gridDataTable;
        private string _unitHeader = "单位：万千瓦、万千伏安";

        private bool _canPrint = true;

        private bool _isSelect = false;

        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }

        public bool CanPrint
        {
            get { return _canPrint; }
            set { _canPrint = value; }
        }

        public string UnitHeader
        {
            get { return _unitHeader; }
            set { _unitHeader = value; }
        }


        public DataTable GridDataTable
        {
            get { return gridDataTable; }
            set { gridDataTable = value; }
        }
        public Form14Result()
        {
            InitializeComponent();
        }

        private void HideToolBarButton()
        {
            if (!CanPrint)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!IsSelect)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void Form14Result_Load(object sender, EventArgs e)
        {
            HideToolBarButton();

            gridControl1.DataSource = GridDataTable;
            
            if(gridView1.Columns.Count > 0)
            {
                gridView1.Columns["Year"].Width = 40;
                gridView1.Columns["Year"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridView1.Columns["Year"].Caption = "";
                gridView1.Columns["Year"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;

                gridView1.Columns["Title"].Width = 120;
                gridView1.Columns["Title"].Caption = "";
                for(int i=0; i<gridView1.Columns.Count; i++)
                {
                    GridColumn gridCol = gridView1.Columns[i];
                    gridCol.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    if (gridCol.FieldName.IndexOf("Col") == 0)
                    {
                        gridCol.Caption = gridDataTable.Columns[gridCol.FieldName].Caption;
                        //gridCol.Width = 120;
                        gridCol.BestFit();
                        gridCol.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
            }
        }

        //打印
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(!CanPrint)
            {
                MsgBox.Show("您没有打印权限！");
                return;
            }

            Common.ComponentPrint.ShowPreview(gridControl1, this.Text, true, new Font("宋体", 16, FontStyle.Bold));
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsSelect)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.gridControl1);
        }
    }
}