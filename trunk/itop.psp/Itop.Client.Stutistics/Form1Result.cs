using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class Form1Result : FormBase
    {
        private DataTable gridDataTable;
        private int _colTitleWidth = 150;
        private DevExpress.Utils.HorzAlignment _colTitleAlign = DevExpress.Utils.HorzAlignment.Near;
        private string _unitHeader = "单位：万元、小时、万千瓦时、万千瓦、万人";

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
        public Form1Result()
        {
            InitializeComponent();
        }

        private void Form1Result_Load(object sender, EventArgs e)
        {
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
                    if (gridCol.FieldName.IndexOf("年") > 0)
                    {
                        gridCol.Width = 80;
                    }
                    else if (gridCol.Caption.IndexOf("增长率") > 0)
                    {
                        gridCol.Caption = "增长率";
                        gridCol.DisplayFormat.FormatString = "p2";
                        gridCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        gridCol.Width = 80;
                    }
                }
            }
        }
    }
}