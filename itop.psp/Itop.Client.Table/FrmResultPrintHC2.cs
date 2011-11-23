using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Client.Chen;
using DevExpress.XtraGrid.Columns;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmResultPrintHC2 : FormBase
    {
        private string title = "";
        //private bool isselect = false;
        public DataTable dt = new DataTable();
        public bool UseDataFalg = false;
        public DataTable dtsouse = new DataTable();
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                this.Text = value;
            }
        }
        public string WritTitle = "";
        public DevExpress.XtraTreeList.TreeList treelist;

        private DataTable gridDataTable;
        public DataTable GridDataTable
        {
            get { return gridDataTable; }
            set { gridDataTable = value; }
        }

       

        public FrmResultPrintHC2()
        {
            InitializeComponent();
        }
       
        private void Form1Print_Load(object sender, EventArgs e)
        {
            if (UseDataFalg)
            {
                gridControl1.DataSource = dtsouse;
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Visible = false;
                }
                for (int j = 0; j < treelist.Columns.Count; j++)
                {
                    if (treelist.Columns[j].VisibleIndex > -1)
                    {
                        GridColumn gridColumn = new GridColumn();
                        gridColumn.Caption = treelist.Columns[j].Caption;
                        gridColumn.FieldName = treelist.Columns[j].FieldName;
                        gridColumn.Visible = true;
                        gridColumn.VisibleIndex = treelist.Columns[j].VisibleIndex;
                        gridColumn.Width = treelist.Columns[j].Width + 20;
                        if (treelist.Columns[j].FieldName == "AllVolumn" || treelist.Columns[j].FieldName == "Length" || treelist.Columns[j].FieldName == "Volumn")
                        {
                            gridColumn.DisplayFormat.FormatString = "#####################0.##";
                            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        }
                        gridView1.Columns.Add(gridColumn);
                    }
                }
                return;
            }
           
            if (dt.Rows.Count>0)
            {
                gridControl1.DataSource = treelist.DataSource;
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Visible = false;
                }
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    if (dt.Rows[k]["Sort"].ToString()=="1")
                    {
                        GridColumn gridColumn = new GridColumn();
                        gridColumn.Caption = dt.Rows[k]["Title"].ToString();
                        gridColumn.FieldName = dt.Rows[k]["ID"].ToString();
                        gridColumn.Visible = true;
                        gridColumn.VisibleIndex = k;
                        gridColumn.Width = int.Parse(dt.Rows[k]["width"].ToString()) + 20;
                        if (dt.Rows[k]["ID"].ToString() == "AllVolumn" || dt.Rows[k]["ID"].ToString() == "Length" || dt.Rows[k]["ID"].ToString() == "Volumn")
                        {
                            gridColumn.DisplayFormat.FormatString =  "#####################0.##";
                            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        }
                        gridView1.Columns.Add(gridColumn);
                 
                    }
                }
            }
            else
            {
                gridControl1.DataSource = treelist.DataSource;
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Visible = false;
                }
                for (int j = 0; j < treelist.Columns.Count; j++)
                {
                    if (treelist.Columns[j].VisibleIndex > -1)
                    {
                        GridColumn gridColumn = new GridColumn();
                        gridColumn.Caption = treelist.Columns[j].Caption;
                        gridColumn.FieldName = treelist.Columns[j].FieldName;
                        gridColumn.Visible = true;
                        gridColumn.VisibleIndex = treelist.Columns[j].VisibleIndex;
                        gridColumn.Width = treelist.Columns[j].Width + 20;
                        if (treelist.Columns[j].FieldName == "AllVolumn" || treelist.Columns[j].FieldName == "Length" || treelist.Columns[j].FieldName == "Volumn")
                        {
                            gridColumn.DisplayFormat.FormatString =  "#####################0.##";
                            gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        }
                        gridView1.Columns.Add(gridColumn);
                    }
                }
            }
          
          
        }
       
       
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ComponentPrint.ShowPreview(this.gridControl1, WritTitle);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            title = this.gridView1.GroupPanelText;
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        string dw1 = "";
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // FileClass.ExportExcel(this.gridControl1);
            ExportExcel.ExportToExcelOld(this.gridControl1,this.Text,dw1);
        }
    }
}