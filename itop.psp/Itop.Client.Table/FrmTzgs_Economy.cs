using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Chen;
using Itop.Domain.Table;
using Itop.Client.Table;
using Itop.Client.Forecast;
using Itop.Client.Stutistics;
using Itop.Domain.Stutistics;
using DevExpress.XtraTreeList;
namespace Itop.Client.Table
{
    public partial class FrmTzgs_Economy : FormBase
    {
        DataTable dataTable;
        public DataRow nowrow =null;
        public Hashtable hs = new Hashtable();
        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        private DataCommon dc = new DataCommon();
        TreeListNode treenode;
        private int typeFlag2 = 1;
        private OperTable oper = new OperTable();
        public Ps_YearRange yAnge = new Ps_YearRange();
        public Ps_YearRange yAngeXs = new Ps_YearRange();
        private bool _isSelect = false;
        bool isdel = false;
        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }
        private string _title = "";

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _unit = "";

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        DevExpress.XtraGrid.GridControl _gcontrol = null;

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return _gcontrol; }
            set { _gcontrol = value; }
        }
        string tong = "", g_col4 = "";
       
        public FrmTzgs_Economy()
        {
            InitializeComponent();
        }

        private void HideToolBarButton()
        {
            if (!base.AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.DeleteRight)
            {
            }
        }

        public string GetProjectID
        {
            get { return MIS.ProgUID; }
        }

        public int[] GetYears()
        {
            Ps_YearRange yr = yAnge;
            int[] year = new int[4] { yr.BeginYear, yr.StartYear, yr.FinishYear, yr.EndYear };
            return year;
        }

       
        private IList listTypes;
        public void CalcYearVol()
        {
            if (yAnge.StartYear > 2008)
            {
                for (int i = 0; i < listTypes.Count; i++)
                {
                    for (int j = 2009; j < yAnge.StartYear + 1; j++)
                    {
                        ((Ps_Table_TZGS)listTypes[i]).BefVolumn += double.Parse(listTypes[i].GetType().GetProperty("y" + j).GetValue(listTypes[i], null).ToString());
                        ((Ps_Table_TZGS)listTypes[i]).AftVolumn -= double.Parse(listTypes[i].GetType().GetProperty("y" + j).GetValue(listTypes[i], null).ToString());
                    }
                }
            }
        }
        private void LoadData()
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }

            string con = "(Col4='bian' or Col4='line' or Col4='sbd') and " + "ProjectID='" + GetProjectID + "' and ParentID='0'";
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_TZGSByConn", con);
            CalcYearVol();
        
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_TZGS));
            
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataTable.Rows[i]["Sort"] = CheckState.Unchecked;
            }
            treeList1.DataSource = dataTable;
            Ps_YearRange yr = yAnge;
          
            treeList1.Columns["Title"].Caption = "项目名称";
            treeList1.Columns["Title"].Width = 250;
            treeList1.Columns["Title"].MinWidth = 250;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Title"].VisibleIndex = 0;
            treeList1.Columns["BuildYear"].Caption = "开工年限";
            treeList1.Columns["BuildYear"].Width = 100;
            treeList1.Columns["BuildYear"].MinWidth = 100;
            treeList1.Columns["BuildYear"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["BuildYear"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BuildYear"].VisibleIndex = 1;
            treeList1.Columns["BuildEd"].Caption = "竣工年限";
            treeList1.Columns["BuildEd"].Width = 100;
            treeList1.Columns["BuildEd"].MinWidth = 100;
            treeList1.Columns["BuildEd"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["BuildEd"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BuildEd"].VisibleIndex = 2;
           

            treeList1.Columns["AreaName"].VisibleIndex = -1;
            treeList1.Columns["AllVolumn"].Caption = "总投资";
            treeList1.Columns["AllVolumn"].Width = 100;
            treeList1.Columns["AllVolumn"].MinWidth = 100;
            treeList1.Columns["AllVolumn"].Format.FormatString = "n2";
            treeList1.Columns["AllVolumn"].OptionsColumn.AllowEdit = true;
            treeList1.Columns["AllVolumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["AllVolumn"].VisibleIndex = 5;
            treeList1.Columns["BefVolumn"].Caption = Convert.ToString(yr.StartYear)+"年底投资";
            treeList1.Columns["BefVolumn"].Width = 100;
            treeList1.Columns["BefVolumn"].MinWidth = 100;
            treeList1.Columns["BefVolumn"].Format.FormatString = "n2";
            treeList1.Columns["BefVolumn"].OptionsColumn.AllowEdit = true;
            treeList1.Columns["BefVolumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BefVolumn"].VisibleIndex = 6;
            treeList1.Columns["AftVolumn"].Caption = Convert.ToString(yr.StartYear + 1) + "~" + Convert.ToString(yr.StartYear + 5) + "投资合计";
            treeList1.Columns["AftVolumn"].Width = 150;
            treeList1.Columns["AftVolumn"].MinWidth = 150;
            treeList1.Columns["AftVolumn"].Format.FormatString = "n2"; ;
            treeList1.Columns["AftVolumn"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["AftVolumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["AftVolumn"].VisibleIndex = 7;
            CalcYearColumn();
            for (int i = 2; i <= 4; i++)
            {
                treeList1.Columns["Col" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["Col" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }

            treeList1.Columns["Col1"].Caption = "备注";
            treeList1.Columns["Col1"].Width = 300;
            treeList1.Columns["Col1"].MinWidth = 300;
            treeList1.Columns["Col1"].OptionsColumn.AllowEdit = true;
            treeList1.Columns["Col1"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Col1"].VisibleIndex = -1;
            treeList1.Columns["Sort"].VisibleIndex = -1;
            treeList1.Columns["ProjectID"].VisibleIndex = -1;
            treeList1.Columns["FromID"].VisibleIndex = -1;
            treeList1.Columns["BianInfo"].VisibleIndex = -1;
            treeList1.Columns["LineInfo"].VisibleIndex = -1;

            treeList1.Columns["Num1"].VisibleIndex = -1;
            treeList1.Columns["Num2"].VisibleIndex = -1;
            treeList1.Columns["Num3"].VisibleIndex = -1;
            treeList1.Columns["Num4"].VisibleIndex = -1;
            treeList1.Columns["Num5"].VisibleIndex = -1;
            treeList1.Columns["Num6"].VisibleIndex = -1;
            treeList1.Columns["DQ"].VisibleIndex = -1;
            treeList1.Columns["WGNum"].VisibleIndex = -1;
            treeList1.Columns["Amount"].VisibleIndex = -1;
            treeList1.Columns["JGNum"].VisibleIndex = -1;
            treeList1.Columns["ProgType"].VisibleIndex = -1;
            treeList1.Columns["Length2"].VisibleIndex = -1;
            treeList1.Columns["Length"].VisibleIndex = -1;
            treeList1.Columns["Volumn"].VisibleIndex = -1;
            //treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            Application.DoEvents();
           // SetValueNull();
            //treeList1.ExpandAll();
            treeList1.CollapseAll();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.tzgs + "'");
            Show();
           
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }
        public void CalcYearColumn()
        {
            int[] year = GetYears();
            for (int i = year[0]; i < year[1] + 1; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }
            for (int i = year[1] + 6; i <= year[3]; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }
            for (int i = year[1] + 1; i <= year[1] + 5; i++)
            {
                treeList1.Columns["y" + i.ToString()].Caption = i.ToString() + "年";
                treeList1.Columns["y" + i.ToString()].VisibleIndex = i;
                treeList1.Columns["y" + i.ToString()].Width = 70;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowEdit = true;
                if (year[1] + 1 == i)
                    treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowEdit = false;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowSort = false;
            }
        }

       
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
          //  nowrow = NodetoDataRow(e.Node);
        }
        private DataRow NodetoDataRow(TreeListNode node)
        {
            int[] year = GetYears();
            DataRow temprow = dataTable.NewRow();
            for (int i = year[1]+1; i <= year[1]+5; i++)
            {
                temprow["y" + i] = node.GetValue("y" + i);
            }
            return temprow;
        }
        
       

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            this.Close();
            
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < treeList1.Nodes.Count; i++)
            {
                if (treeList1.Nodes[i]["Sort"].ToString()=="1")
                {
                    hs.Add(i, NodetoDataRow(treeList1.Nodes[i]));
                }
            }

          this.DialogResult=DialogResult.OK;
            this.Close();
           
        }

        private void treeList1_FocusedNodeChanged_1(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            nowrow = NodetoDataRow(e.Node);
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            CheckState check = (CheckState)e.Node.GetValue("Sort");
            if (check == CheckState.Unchecked)
                e.NodeImageIndex = 0;
            else if (check == CheckState.Checked)
                e.NodeImageIndex = 1;
            else e.NodeImageIndex = 2;
        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
                if (hInfo.HitInfoType == HitInfoType.StateImage)
                {
                    CheckState check = (CheckState)hInfo.Node.GetValue("Sort");
                    if (check == CheckState.Indeterminate || check == CheckState.Unchecked) check = CheckState.Checked;
                    else check = CheckState.Unchecked;
                    treeList1.BeginUpdate();
                    hInfo.Node["Sort"] = check;
                    treeList1.Refresh();
                    treeList1.EndUpdate();
                }
            }
        }

      


       

    }
}