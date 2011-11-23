using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using Itop.Common;
using Itop.Client.Using;
using Itop.Client.Base;

namespace Itop.Client.History
{
    /// <summary>
    /// 现有大工业用户产能及用电情况
    /// </summary>
    public partial class Formtj432 : FormBase
    {
        private DataTable gridDataTable;
        private int _colTitleWidth = 150;
        private DevExpress.Utils.HorzAlignment _colTitleAlign = DevExpress.Utils.HorzAlignment.Near;
        private string _unitHeader = "单位：亿千瓦时、万千瓦";
        private bool _canPrint = true;

        bool _isSelect = false;

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

        public DevExpress.Utils.HorzAlignment ColTitleAlign
        {
            get { return _colTitleAlign; }
            set { _colTitleAlign = value; }
        }

        public int ColTitleWidth
        {
            get { return _colTitleWidth; }
            set { _colTitleWidth = value; }
        }

        public DataTable GridDataTable
        {
            get { return gridDataTable; }
            set { gridDataTable = value; }
        }
        public Formtj432()
        {
            InitializeComponent();
            barButtonItem3.Glyph = Itop.ICON.Resource.关闭;
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

        private void Form1Result_Load(object sender, EventArgs e)
        {
            HideToolBarButton();

            gridControl1.DataSource = GridDataTable;
            
            if(gridView1.Columns.Count > 0)
            {
                gridView1.Columns["Title"].Width = _colTitleWidth;
                gridView1.Columns["Title"].AppearanceCell.TextOptions.HAlignment = _colTitleAlign;
                gridView1.Columns["Title"].Caption = "";
                for(int i=0; i<gridView1.Columns.Count; i++)
                {
                    GridColumn gridCol = gridView1.Columns[i];
                    gridCol.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridCol.Caption = gridDataTable.Columns[gridCol.FieldName].Caption;
                    if (gridCol.Caption == "") gridCol.Visible = false;
                    if (gridCol.FieldName.IndexOf("年") > 0) {
                        gridCol.Width = 80;
                        gridCol.DisplayFormat.FormatString = "n2";
                    } else if (gridCol.Caption.IndexOf("增长率") >= 0) {
                        gridCol.Caption = "增长率";
                        gridCol.DisplayFormat.FormatString = "p2";
                        gridCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        gridCol.Width = 80;
                    }
                }
            }
        }

        //打印
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CanPrint)
            {
                MsgBox.Show("您没有打印权限！");
                return;
            }
            Common.ComponentPrint.ShowPreview(gridControl1, this.Text, true, new Font("宋体", 16, FontStyle.Bold));
        }

        //导出
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(IsSelect)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.Text, _unitHeader, this.gridControl1);
        }
    }
}