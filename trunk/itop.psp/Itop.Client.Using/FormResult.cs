using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using Itop.Common;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Base;
namespace Itop.Client.Using
{
    public partial class FormResult : FormBase
    {
        private DataTable gridDataTable;
        private DataTable DT_ToExcel;
        private int _colTitleWidth = 150;
        private DevExpress.Utils.HorzAlignment _colTitleAlign = DevExpress.Utils.HorzAlignment.Near;
        private string _unitHeader = "";
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

        TreeList xTreeList = new TreeList();
        public TreeList LI
        {
            get { return xTreeList; }
            set { xTreeList = value; }
        }



        public FormResult()
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

        private void Form1Result_Load(object sender, EventArgs e)
        {
            //HideToolBarButton();

            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "项目";
            dt.Columns.Add("ParentID", typeof(string));
           
            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("y") >= 0)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(double));
                }
                //else
                //    if (column.FieldName == "ParentID")
                //    {
                //        dt.Columns.Add("ParentID", typeof(string));
                //        listColID.Add("ParentID");
                //        dt.Columns["ParentID"].Caption = "父ID";
                //    }
            }
            listColID.Add("ParentID");
            dt.Columns["ParentID"].Caption = "父ID";

            int itemp =-4;
            int jtemp = -4;
            foreach (TreeListNode node in xTreeList.Nodes)
            {
                jtemp = itemp;
                AddNodeDataToDataTable(dt, node, listColID, ref itemp,   jtemp);
               // itemp++;
            }
            //gridControl1.DataSource = dt;
            //if(gridView1.Columns.Count > 0)
            //{
            //    gridView1.Columns["Title"].Width = _colTitleWidth;
            //    gridView1.Columns["Title"].AppearanceCell.TextOptions.HAlignment = _colTitleAlign;
            //    gridView1.Columns["Title"].Caption = "项目";
            //    gridView1.Columns["ParentID"].Caption = "父ID";
            //    for(int i=0; i<gridView1.Columns.Count; i++)
            //    {
            //        GridColumn gridCol = gridView1.Columns[i];
            //        gridCol.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //        if (gridCol.FieldName.IndexOf("y") >= 0)
            //        {
            //            gridCol.Width = 80;
            //            gridCol.Caption = gridCol.FieldName.Replace("y", "") + "年";
            //            gridCol.DisplayFormat.FormatString = "n2";
            //            gridCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //        }
            //        else if (gridCol.Caption.IndexOf("增长率") >= 0)
            //        {
            //            gridCol.Caption = "增长率";
            //            gridCol.DisplayFormat.FormatString = "p2";
            //            gridCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //            gridCol.Width = 80;
            //        }
            //    }
            
            if (dt.Columns.Count>0)
            {
                GridColumn gridCol2 = new GridColumn();
                gridCol2.Caption = "项目";
                gridCol2.Visible = true;
                gridCol2.FieldName = "Title";
                gridCol2.Width = _colTitleWidth;
                gridCol2.VisibleIndex = 0;
                gridCol2.AppearanceCell.TextOptions.HAlignment = _colTitleAlign;
                GridColumn gridCol3 = new GridColumn();
                gridCol3.Caption = "父ID";
                gridCol3.FieldName = "ParentID";
                gridCol3.Visible = true;
                gridCol3.VisibleIndex = dt.Columns.Count + 3;
                gridView1.Columns.Add(gridCol2);
                gridView1.Columns.Add(gridCol3);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    GridColumn gridCol = new GridColumn();
                    gridCol.Visible = true;
                    gridCol.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    if (dt.Columns[i].ColumnName.IndexOf("y") >= 0)
                    {
                        gridCol.Width = 80;
                        gridCol.Caption = dt.Columns[i].ColumnName.Replace("y", "") + "年";
                        gridCol.FieldName = dt.Columns[i].ColumnName;
                        gridCol.VisibleIndex = i + 1;
                        gridCol.DisplayFormat.FormatString = "n2";
                        gridCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    }
                    else if (dt.Columns[i].ColumnName.IndexOf("增长率") >= 0)
                    {
                        gridCol.Caption = "增长率";
                        gridCol.FieldName = dt.Columns[i].ColumnName;
                        gridCol.VisibleIndex = i + 1;
                        gridCol.DisplayFormat.FormatString = "p2";
                        gridCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        gridCol.Width = 80;
                    }
                    
                    gridView1.Columns.Add(gridCol);
                }
                //gridView1.Columns["ParentID"].VisibleIndex = gridView1.Columns.Count;
                gridView1.Columns["ParentID"].Visible = false;
                gridControl1.DataSource = dt;
                DT_ToExcel = dt;
            }
        }

        //打印
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //////if (!CanPrint)
            //////{
            //////    MsgBox.Show("您没有打印权限！");
            //////    return;
            //////}
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
            if (gridView1.Columns.ColumnByFieldName("ParentID")!=null)
            gridView1.Columns["ParentID"].Visible = true;
        FileClass.ExportExcel(this.Text, _unitHeader, this.gridControl1);
        if (gridView1.Columns.ColumnByFieldName("ParentID") != null)
            gridView1.Columns["ParentID"].Visible = false;
        }


        private static void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID, ref int i, int j)
        {
            DataRow newRow = dt.NewRow();
           
            foreach (string colID in listColID)
            {
                //分类名，第二层及以后在前面加空格
                if (colID == "Title" && node.ParentNode != null)
                {
                    newRow[colID] = "　　" + node[colID];
                }
                else if(colID == "ParentID"&& node.ParentNode != null)
                {
                    newRow[colID]=j;
                }
                else
                {
                    newRow[colID] = node[colID];
                }
            }

            ////根分类结束后加空行

            //if (node.ParentNode == null && dt.Rows.Count > 0)
            //{
            //    dt.Rows.Add(new object[] { });
            //}
            
            dt.Rows.Add(newRow);
            j = i;
            i--;
            foreach (TreeListNode nd in node.Nodes)
            {

               
                AddNodeDataToDataTable(dt, nd, listColID, ref  i, j);
               // i++;
            }
          
        }




    }
}