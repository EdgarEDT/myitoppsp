using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Stutistics;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.Xml;
using System.IO;
using Itop.Domain.Stutistics;
using DevExpress.XtraTreeList;
using DevExpress.Utils;


namespace Itop.Client.Stutistics
{
    public partial class Form9_TouZiGuSuan : FormBase
    {
        string  updat = "";
        string title = "";
        string unit = "兆伏安、公里";
        bool isSelect = false;
        string type = "JSXM";

        DevExpress.XtraGrid.GridControl gcontrol = null;
        DateTime ddd = DateTime.Now;
        string sss = "";

        public string Title
        {
            get { return title; }
        }

        public string Unit
        {
            get { return unit; }
        }

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return gcontrol; }
        }

        public bool IsSelect
        {
            set { isSelect = value; }
        }

        DataTable dt = new DataTable();


        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        PSP_Project_List obj = new PSP_Project_List();
         
        private string typeFlag2 = "";

        public Form9_TouZiGuSuan()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            type = ProjectUID;
            ////this.ctrlPowerEachList1.LangFang = this;
            this.ctrlPowerEachList1.IsJSXM = true;
            this.ctrlPowerEachList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlPowerEachList1.RefreshData(type);

            Show();
            Application.DoEvents();
            InitRight();
            this.ctrlPowerEachList1.colListName.Caption = "规划名称";
            ctrlPowerEachList1.colListName.Width = 100;
            ctrlPowerEachList1.colRemark.Visible = false;
            //////InitPicData()
        }
        private void InitPicData()
        { PSP_Project_List psp_Type = new PSP_Project_List();
        string str = "";

        try
        {
            str = System.Configuration.ConfigurationSettings.AppSettings["SvgID"];

        }
        catch { }




        dt.Columns.Add("A");
        dt.Columns.Add("B");
        dt.Columns.Add("C", typeof(bool));
        dt.Columns.Add("D");

        IList<LayerGrade> li = Services.BaseService.GetList<LayerGrade>("SelectLayerGradeListBySvgDataUid", str);
        IList<SVG_LAYER> li1 = Services.BaseService.GetList<SVG_LAYER>("SelectSVG_LAYERBySvgID", str);

        DataTable dt1 = Itop.Common.DataConverter.ToDataTable((IList)li1);

        foreach (LayerGrade node in li)
        {
            DataRow row = dt.NewRow();
            row["A"] = node.SUID;
            row["B"] = node.Name;
            row["C"] = false;
            row["D"] = node.ParentID;
            dt.Rows.Add(row);
            DataRow[] rows = dt1.Select("YearID='" + node.SUID + "'");
            foreach (DataRow row1 in rows)
            {
                DataRow row2 = dt.NewRow();
                row2["A"] = row1["SUID"].ToString();
                row2["B"] = row1["NAME"].ToString();
                row2["C"] = false;
                row2["D"] = node.SUID;
                dt.Rows.Add(row2);
            }
        }

            //SVGFILE sf = Services.BaseService.GetOneByKey<SVGFILE>("26474eb6-cd92-4e84-a579-2f33946acf1a");
            //XmlDocument xd = new XmlDocument();
            //xd.LoadXml(sf.SVGDATA);
            //dt.Columns.Add("A");
            //dt.Columns.Add("B");
            //dt.Columns.Add("C", typeof(bool));
            //XmlNodeList nli = xd.GetElementsByTagName("layer");
            //foreach (XmlNode node in nli)
            //{
            //    DataRow row =dt.NewRow();
            //    XmlElement xe=(XmlElement)node;
            //    row["A"] =xe.GetAttribute("id");
            //    row["B"] = xe.GetAttribute("label");
            //    row["C"] = false;
            //    dt.Rows.Add(row);
            //}
        }
        private void InitRight()
        {
            if (!AddRight)
            {
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!EditRight)
            {
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem13.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem16.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                ctrlPowerEachList1.editright = false;
            }
            if (!AddRight && !EditRight)
             {
                 barSubItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
             }         
            if (!DeleteRight)
            {
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!PrintRight)
            {
                barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }
        void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.ctrlPowerEachList1.FocusedObject == null)
                return;
            typeFlag2 = this.ctrlPowerEachList1.FocusedObject.UID;
           this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            WaitDialogForm wait = null;
             try
            {
                wait = new WaitDialogForm("", "正在统计数据, 请稍候...");
                InitSodata();
                wait.Close();
            }
            catch
            {
                wait.Close();
            }
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }


        //加载设备情况表列字段
        private void LoadData()
        {
            try
            {
                if (dataTable != null)
                {
                    dataTable.Columns.Clear();
                    treeList1.Columns.Clear();
                }
                PSP_Project_List psp_Type = new PSP_Project_List();
                psp_Type.Flag2 = typeFlag2;
                IList listTypes = new ArrayList();
                try
                {
                    listTypes = Common.Services.BaseService.GetList("SelectPSP_Project_ListByFlag2", psp_Type);

                }
                catch (Exception ex)
                { MsgBox.Show(ex.Message); }

                Project_Sum ps = new Project_Sum();
                ps.S5 = "1";
                IList<Project_Sum> sumline = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);
                ps.S5 = "2";
                IList<Project_Sum> sumsubsation = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);
                Hashtable ha = new Hashtable();
                ArrayList al = new ArrayList();
                foreach (PSP_Project_List psplf in listTypes)
                {

                    if (psplf.Flag == 1)
                    {
                        if (psplf.L8 == ""|| psplf.L8 == null)
                        {
                            psplf.L8 = "0";
                           
                        }
                        foreach (Project_Sum psum in sumline)
                        {
                            if (psum.L1 == psplf.L9 && psum.S1 == psplf.L4)
                            {
                                if (psum.Num.ToString() == null || psum.Num.ToString() == "")
                                    psum.Num = 0;

                                psplf.L10 = Math.Round(psum.Num * double.Parse(psplf.L8), 2);

                                if (psplf.L11 == "" || psplf.L11 == null)
                                {
                                    psplf.L11 = "0";
                                }
                                psplf.L12 = Math.Round(psplf.L10 + double.Parse(psplf.L11), 2);
                                ////    Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangByid", psplf);
        
                                break;
                            }
                                    
                        }
                    }
                    if (psplf.Flag == 2)
                    {
                        ha.Clear();
                        al.Clear();
                        double sumvaluedata = 0;
                        double sumvalueLine = 0;
                        if (psplf.L6 != "" && psplf.L6 != null && psplf.L5 != "" && psplf.L5 != null)
                        {
                            if (psplf.IsConn.ToString() == "" || psplf.IsConn.ToString() == null )
                                psplf.IsConn = "0";
                            double mva = double.Parse(psplf.IsConn.ToString());
                            double t5 = 0;//单台容量
                            int ta = 0;//主变台数
                            string l5 = "";
                            string l6 = "";
                            foreach (Project_Sum psum in sumsubsation)
                            {
                                if (psum.S1 == psplf.L4)
                                {
                                    try
                                    {
                                        t5 = Convert.ToDouble(psum.T5);//单台容量
                                        ta = Convert.ToInt32(psum.T1);//主变台数
                                        if (mva == (t5 * ta))
                                        {
                                            ha.Add(t5, ta);
                                            al.Add(t5);
                                        }
                                    }
                                    catch { }
                                }
                            }
                            if (al.Count > 0)
                            {
                                double va = Convert.ToDouble(al[0].ToString());
                                for (int ii = 0; ii < al.Count; ii++)
                                {
                                    if (va < Convert.ToDouble(al[ii].ToString()))
                                        va = Convert.ToDouble(al[ii].ToString());
                                }
                                l5 = ha[va].ToString();
                                l6 = va.ToString();
                            }
                            else
                            {
                                l5 = "";
                                l6 = "";

                            }
                            psplf.L5 = l5;
                            psplf.L6 = l6;
                            ////if (psplf.L5 != l5 && psplf.L6 != l6)
                            ////{
                            ////    psplf.l5 = l5;
                            ////    psplf.L6 = l6;
                            ////    Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangByid", psplf);
                            ////}
                            foreach (Project_Sum psum in sumsubsation)
                            {

                                if (psum.T1 == psplf.L5 && psum.T5 == psplf.L6 && psum.S1 == psplf.L4)
                                {
                                    if (psum.Num.ToString() == null || psum.Num.ToString() == "")
                                        psum.Num = 0;

                                    sumvaluedata = psum.Num;
                                    break;
                                }
                            }
                        }
      
                            if (psplf.L8 != "" && psplf.L8 != null)
                            {
                                foreach (Project_Sum psum in sumline)
                                {
                                    if (psum.L1 == psplf.L9 && psum.S1 == psplf.L4)
                                    {
                                        if (psum.Num.ToString() == null || psum.Num.ToString() == "")
                                            psum.Num = 0;

                                        sumvalueLine = psum.Num * double.Parse(psplf.L8);
                                        break;
                                    }
                                }
                            }
                            psplf.L10 = Math.Round((sumvaluedata + sumvalueLine), 2);
                            if (psplf.L11 == "" || psplf.L11 == null)
                            {
                                psplf.L11 = "0";
                            }
                            psplf.L12 = Math.Round(psplf.L10 + double.Parse(psplf.L11), 2);
     
                    }
                }
                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Project_List));
                //dataTable.DefaultView.Sort = "L4 desc";
                //dataTable.DefaultView.Sort = "Title desc";
                //dataTable.DefaultView.Sort = "CreateTime desc";
            
                treeList1.DataSource = dataTable;

                treeList1.Columns["L3"].Caption = "项目名称";
                treeList1.Columns["L3"].Width = 80;
                treeList1.Columns["L3"].OptionsColumn.AllowEdit = false;
                treeList1.Columns["L3"].OptionsColumn.AllowSort = false;
                treeList1.Columns["Title"].VisibleIndex = -1;
                treeList1.Columns["Title"].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["Flag"].VisibleIndex = -1;
                treeList1.Columns["Flag"].SortOrder = System.Windows.Forms.SortOrder.None;
                treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["Flag2"].VisibleIndex = -1;
                treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["CreateTime"].VisibleIndex = -1;
                treeList1.Columns["CreateTime"].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["CreateTime"].SortOrder = System.Windows.Forms.SortOrder.None;


                treeList1.Columns["UpdateTime"].VisibleIndex = -1;
                treeList1.Columns["UpdateTime"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["UpdateTime"].VisibleIndex = -1;
                treeList1.Columns["UpdateTime"].OptionsColumn.ShowInCustomizationForm = false;


                treeList1.Columns["Code"].VisibleIndex = -1;
                treeList1.Columns["Code"].OptionsColumn.ShowInCustomizationForm = false;


                treeList1.Columns["L7"].VisibleIndex = -1;
                treeList1.Columns["L7"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L2"].VisibleIndex = -1;
                ////treeList1.Columns["L2"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L11"].VisibleIndex = -1;
                treeList1.Columns["L11"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L12"].VisibleIndex = -1;
                treeList1.Columns["L12"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L13"].VisibleIndex = -1;
                treeList1.Columns["L13"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L14"].VisibleIndex = -1;
                treeList1.Columns["L14"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L15"].VisibleIndex = -1;
                ////treeList1.Columns["L15"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L16"].VisibleIndex = -1;
                treeList1.Columns["L16"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L17"].VisibleIndex = -1;
                treeList1.Columns["L17"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L18"].VisibleIndex = -1;
                treeList1.Columns["L18"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L19"].VisibleIndex = -1;
                treeList1.Columns["L19"].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["Remark"].VisibleIndex = -1;
                treeList1.Columns["Remark"].OptionsColumn.ShowInCustomizationForm = false;
                AddColumn2();
                PowerProYears psp_Year = new PowerProYears();
                psp_Year.Flag = typeFlag2;
                IList<PowerProYears> listYears = Common.Services.BaseService.GetList<PowerProYears>("SelectPowerProYearsListByFlag", psp_Year);

                foreach (PowerProYears item in listYears)
                {
                    AddColumn(item.Year);
                }
                AddColumn1();
                Application.DoEvents();

                LoadValues();
                TreeListColumn column = treeList1.Columns["L10"];
                TreeListColumn column1 = treeList1.Columns["L12"];
                for (int i = 0; i < treeList1.Nodes.Count; i++)
                {
                    CalculateNodesValue(treeList1.Nodes[i], column, column1);
                } 
                treeList1.ExpandAll();
            }
            catch
            {
            }
        }
        //加载数据时，查找有子分类节点的父节点，进行计算


        private void CalculateNodesValue(TreeListNode node, TreeListColumn column, TreeListColumn column1)
        {

            for (int i = 0; i < node.Nodes.Count; i++)
            {
                CalculateNodesValue(node.Nodes[i], column, column1);
            }
            if (node.HasChildren)
                CalculateSumValue(node, column, column1);
            return;
        }

        //加载数据时，计算其父分类的值


        private void CalculateSumValue(TreeListNode node, TreeListColumn column, TreeListColumn column1)
        {
            TreeListNode parentNode = node;

            if (parentNode == null)
            {
                return;
            }

            double sum = 0;
            double sum1 = 0;
                foreach (TreeListNode nd in parentNode.Nodes)
                {
                    object value = nd.GetValue(column.FieldName);
                    if (value != null && value != DBNull.Value)
                    {
                        sum += Convert.ToDouble(value);
                    }
                    value = nd.GetValue(column1.FieldName);
                    if (value != null && value != DBNull.Value)
                    {
                        sum1 += Convert.ToDouble(value);
                    }
                }

            parentNode.SetValue(column.FieldName, sum);
            parentNode.SetValue(column1.FieldName, sum1);
            //if (!SaveCellValue((string)parentNode.GetValue("ID"), Convert.ToString(sum)))
            //{
            //    return;
            //}
            ////if (parentNode.ParentNode != null)
            ////    CalculateSum(parentNode.ParentNode, column, 0, flag);
        }
        private void LoadDatadata()
        {
            try
            {
                if (dataTable != null)
                {
                    dataTable.Columns.Clear();
                    treeList1.Nodes.Clear();
                }
                PSP_Project_List psp_Type = new PSP_Project_List();
                psp_Type.Flag2 = typeFlag2;
                IList listTypes = new ArrayList();
                try
                {
                    listTypes = Common.Services.BaseService.GetList("SelectPSP_Project_ListByFlag2", psp_Type);

                }
                catch (Exception ex)
                { MsgBox.Show(ex.Message); }
                Project_Sum ps = new Project_Sum();
                ps.S5 = "1";
                IList<Project_Sum> sumline = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);
                ps.S5 = "2";
                IList<Project_Sum> sumsubsation = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);
                Hashtable ha = new Hashtable();
                ArrayList al = new ArrayList();
                foreach (PSP_Project_List psplf in listTypes)
                {
                    if (psplf.Flag == 1)//纯线路造价计算
                    {
                        if (psplf.L8 == "" || psplf.L8 == null)
                        {
                            psplf.L8 = "0";

                        }
                        foreach (Project_Sum psum in sumline)
                        {
                            if (psum.L1 == psplf.L9 && psum.S1 == psplf.L4)
                            {
                                if (psum.Num.ToString() == null || psum.Num.ToString() == "")
                                    psum.Num = 0;

                                psplf.L10 = Math.Round(psum.Num * double.Parse(psplf.L8), 2);

                                if (psplf.L11 == "" || psplf.L11 == null)
                                {
                                    psplf.L11 = "0";
                                }
                                psplf.L12 = Math.Round(psplf.L10 + double.Parse(psplf.L11), 2);
                                ////    Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangByid", psplf);
                   
                                break;
                            }

                        }
                    }
                    if (psplf.Flag == 2)
                    {
                        ha.Clear();
                        al.Clear();
                        double sumvaluedata = 0;
                        double sumvalueLine = 0;
                        if (psplf.L6 != "" && psplf.L6 != null && psplf.L5 != "" && psplf.L5 != null)//变电站内的电站造价计算
                        {
                            if (psplf.IsConn.ToString() == "" || psplf.IsConn.ToString() == null )
                                psplf.IsConn = "0";
                            double mva = double.Parse(psplf.IsConn.ToString());
                            double t5 = 0;//单台容量
                            int ta = 0;//主变台数
                            string l5 = "";
                            string l6 = "";
                            foreach (Project_Sum psum in sumsubsation)
                            {
                                if (psum.S1 == psplf.L4)
                                {
                                    try
                                    {
                                        t5 = Convert.ToDouble(psum.T5);//单台容量
                                        ta = Convert.ToInt32(psum.T1);//主变台数
                                        if (mva == (t5 * ta))
                                        {
                                            ha.Add(t5, ta);
                                            al.Add(t5);
                                        }
                                    }
                                    catch { }
                                }
                            }
                            if (al.Count > 0)
                            {
                                double va = Convert.ToDouble(al[0].ToString());
                                for (int ii = 0; ii < al.Count; ii++)
                                {
                                    if (va < Convert.ToDouble(al[ii].ToString()))
                                        va = Convert.ToDouble(al[ii].ToString());
                                }
                                l5 = ha[va].ToString();
                                l6 = va.ToString();
                            }
                            else
                            {
                                l5 = "";
                                l6 = "";

                            }
                            psplf.L5 = l5;
                            psplf.L6 = l6;
                            ////if (psplf.L5 != l5 && psplf.L6 != l6)
                            ////{
                            ////    psplf.l5 = l5;
                            ////    psplf.L6 = l6;
                            ////    Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangByid", psplf);
                            ////}
                            foreach (Project_Sum psum in sumsubsation)
                            {
                                if (psum.T1 == psplf.L5 && psum.T5 == psplf.L6 && psum.S1 == psplf.L4)
                                {
                                    if (psum.Num.ToString() == null || psum.Num.ToString() == "")
                                        psum.Num = 0;

                                    sumvaluedata = psum.Num;
                                    break;
                                }
                            }
                        }
                        if (psplf.L8 != "" && psplf.L8 != null)//变电站内的线路造价计算
                        {
                            foreach (Project_Sum psum in sumline)
                            {
                                if (psum.L1 == psplf.L9 && psum.S1 == psplf.L4)
                                {
                                    if (psum.Num.ToString() == null || psum.Num.ToString() == "")
                                        psum.Num = 0;

                                    sumvalueLine = psum.Num * double.Parse(psplf.L8);
                                    break;
                                }
                            }
                        }
                        psplf.L10 = Math.Round(sumvaluedata + sumvalueLine, 2);
                        if (psplf.L11 == "" || psplf.L11 == null)
                        {
                            psplf.L11 = "0";
                        }
                        psplf.L12 = Math.Round(psplf.L10 + double.Parse(psplf.L11), 2);
                    }
                }

                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Project_List));
                //dataTable.DefaultView.Sort = "L4 desc";
                //dataTable.DefaultView.Sort = "Title desc";
                //dataTable.DefaultView.Sort = "CreateTime desc";
               
                treeList1.DataSource = dataTable;

                ////treeList1.Columns["L3"].Caption = "工程名称";
                ////treeList1.Columns["L3"].Width = 80;
                ////treeList1.Columns["L3"].OptionsColumn.AllowEdit = false;
                ////treeList1.Columns["L3"].OptionsColumn.AllowSort = false;
                ////treeList1.Columns["Title"].VisibleIndex = -1;
                ////treeList1.Columns["Title"].OptionsColumn.ShowInCustomizationForm = false;
                ////treeList1.Columns["Flag"].VisibleIndex = -1;
                ////treeList1.Columns["Flag"].SortOrder = System.Windows.Forms.SortOrder.None;
                ////treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
                ////treeList1.Columns["Flag2"].VisibleIndex = -1;
                ////treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["CreateTime"].VisibleIndex = -1;
                ////treeList1.Columns["CreateTime"].OptionsColumn.ShowInCustomizationForm = false;
                ////treeList1.Columns["CreateTime"].SortOrder = System.Windows.Forms.SortOrder.None;

                ////treeList1.Columns["UpdateTime"].VisibleIndex = -1;
                ////treeList1.Columns["UpdateTime"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["UpdateTime"].VisibleIndex = -1;
                ////treeList1.Columns["UpdateTime"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["Code"].VisibleIndex = -1;
                ////treeList1.Columns["Code"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L7"].VisibleIndex = -1;
                treeList1.Columns["L7"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L2"].VisibleIndex = -1;
                ////treeList1.Columns["L2"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L11"].VisibleIndex = -1;
                ////treeList1.Columns["L11"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L12"].VisibleIndex = -1;
                ////treeList1.Columns["L12"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L13"].VisibleIndex = -1;
                ////treeList1.Columns["L13"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L14"].VisibleIndex = -1;
                ////treeList1.Columns["L14"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L15"].VisibleIndex = -1;
                ////treeList1.Columns["L15"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L16"].VisibleIndex = -1;
                ////treeList1.Columns["L16"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L17"].VisibleIndex = -1;
                treeList1.Columns["L17"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L18"].VisibleIndex = -1;
                ////treeList1.Columns["L18"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L19"].VisibleIndex = -1;
                ////treeList1.Columns["L19"].OptionsColumn.ShowInCustomizationForm = false;
                ////treeList1.Columns["Remark"].VisibleIndex = -1;
                ////treeList1.Columns["Remark"].OptionsColumn.ShowInCustomizationForm = false;
                AddColumn2();
                PowerProYears psp_Year = new PowerProYears();
                psp_Year.Flag = typeFlag2;

                Application.DoEvents();

                LoadValues();
                TreeListColumn column = treeList1.Columns["L10"];
                TreeListColumn column1 = treeList1.Columns["L12"];
                for (int i = 0; i < treeList1.Nodes.Count; i++)
                {

                    CalculateNodesValue(treeList1.Nodes[i], column, column1);
                } 
                treeList1.ExpandAll();
            }
            catch
            {
            }
        }
        private void UpdataLoadDatadata()//重新计算造价
        {
            try
            {
                if (dataTable != null)
                {
                    dataTable.Columns.Clear();
                    treeList1.Nodes.Clear();
                }
                PSP_Project_List psp_Type = new PSP_Project_List();
                psp_Type.Flag2 = typeFlag2;
                IList listTypes = new ArrayList();
                try
                {
                    listTypes = Common.Services.BaseService.GetList("SelectPSP_Project_ListByFlag2", psp_Type);

                }
                catch (Exception ex)
                { MsgBox.Show(ex.Message); }
                Project_Sum ps = new Project_Sum();
                ps.S5 = "1";
                IList<Project_Sum> sumline = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);
                ps.S5 = "2";
                IList<Project_Sum> sumsubsation = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);
                Hashtable ha = new Hashtable();
                ArrayList al = new ArrayList();

                foreach (PSP_Project_List psplf in listTypes)
                {
                    if (psplf.Flag == 1)//纯线路造价计算
                    {
                        if (psplf.L8 == "" || psplf.L8 == null)
                        {
                            psplf.L8 = "0";

                        }
                        foreach (Project_Sum psum in sumline)
                        {
                            if (psum.L1 == psplf.L9 && psum.S1 == psplf.L4)
                            {
                                if (psum.Num.ToString() == null || psum.Num.ToString() == "")
                                    psum.Num = 0;

                                psplf.L10 =Math.Round(psum.Num * double.Parse(psplf.L8),2);
              
                                if (psplf.L11 == "" || psplf.L11 == null)
                                {
                                    psplf.L11 = "0";
                                }
                                psplf.L12 = Math.Round(psplf.L10 + double.Parse(psplf.L11), 2);
                                ////    Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangByid", psplf);
        
                                break;
                            }

                        }
                    }
                    if (psplf.Flag == 2)
                    {
                        ha.Clear();
                        al.Clear();
                        double sumvaluedata = 0;
                        double sumvalueLine = 0;
                        if (psplf.L6 != "" && psplf.L6 != null && psplf.L5 != "" && psplf.L5 != null)//变电站内的电站造价计算
                        {
                            if (psplf.IsConn.ToString() == "" || psplf.IsConn.ToString() == null )
                                psplf.IsConn = "0";
                            double mva = double.Parse(psplf.IsConn.ToString());
                            double t5 =0;//单台容量
                            int ta =0;//主变台数
                            string l5="";
                            string l6 = "";
                            foreach (Project_Sum psum in sumsubsation)
                            {
                                if (psum.S1 == psplf.L4)
                                {
                                    try
                                    {
                                        t5 = Convert.ToDouble(psum.T5);//单台容量
                                        ta = Convert.ToInt32(psum.T1);//主变台数
                                        if (mva == (t5 * ta))
                                        {
                                            ha.Add(t5, ta);
                                            al.Add(t5);
                                        }
                                    }
                                    catch { }
                                }
                            }
                                    if (al.Count >0)
                                    {
                                        double va = Convert.ToDouble(al[0].ToString());
                                        for (int ii = 0; ii < al.Count; ii++)
                                        {
                                            if (va < Convert.ToDouble(al[ii].ToString()))
                                                va = Convert.ToDouble(al[ii].ToString());
                                        }
                                        l5 = ha[va].ToString();
                                        l6 = va.ToString();
                                    }
                                    else
                                    {
                                        l5 = "";
                                        l6 = "";
 
                                    }
                                    psplf.L5 = l5;
                                    psplf.L6 = l6;
                                    ////if (psplf.L5 != l5 && psplf.L6 != l6)
                                    ////{
                                    ////    psplf.l5 = l5;
                                    ////    psplf.L6 = l6;
                                    ////    Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangByid", psplf);
                                    ////}
                             foreach (Project_Sum psum in sumsubsation)
                             {
                                 if (psum.T1 == psplf.L5 && psum.T5 == psplf.L6 && psum.S1 == psplf.L4)
                                 {
                                     if (psum.Num.ToString() == null || psum.Num.ToString() == "")
                                         psum.Num = 0;

                                     sumvaluedata = psum.Num;
                                     break;
                                 }
                            }

                        }
                        if (psplf.L8 != "" && psplf.L8 != null)//变电站内的线路造价计算
                        {
                            foreach (Project_Sum psum in sumline)
                            {
                                if (psum.L1 == psplf.L9 && psum.S1 == psplf.L4)
                                {
                                    if (psum.Num.ToString() == null || psum.Num.ToString() == "")
                                        psum.Num = 0;

                                    sumvalueLine = psum.Num * double.Parse(psplf.L8);
                                    break;
                                }
                            }
                        }
                         psplf.L10 =Math.Round(sumvaluedata + sumvalueLine,2);
                        
                        if (psplf.L11== "" || psplf.L11 == null)
                        {
                            psplf.L11 = "0";
                        }
                        psplf.L12 =Math.Round(psplf.L10 + double.Parse(psplf.L11),2);
                       
                    }
                }
                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Project_List));

                treeList1.DataSource = dataTable;
                ////treeList1.Columns["L3"].Caption = "工程名称";
                ////treeList1.Columns["L3"].Width = 80;
                ////treeList1.Columns["L3"].OptionsColumn.AllowEdit = false;
                ////treeList1.Columns["L3"].OptionsColumn.AllowSort = false;
                ////treeList1.Columns["Title"].VisibleIndex = -1;
                ////treeList1.Columns["Title"].OptionsColumn.ShowInCustomizationForm = false;
                ////treeList1.Columns["Flag"].VisibleIndex = -1;
                ////treeList1.Columns["Flag"].SortOrder = System.Windows.Forms.SortOrder.None;
                ////treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
                ////treeList1.Columns["Flag2"].VisibleIndex = -1;
                ////treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["CreateTime"].VisibleIndex = -1;
                ////treeList1.Columns["CreateTime"].OptionsColumn.ShowInCustomizationForm = false;
                ////treeList1.Columns["CreateTime"].SortOrder = System.Windows.Forms.SortOrder.None;

                ////treeList1.Columns["UpdateTime"].VisibleIndex = -1;
                ////treeList1.Columns["UpdateTime"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["UpdateTime"].VisibleIndex = -1;
                ////treeList1.Columns["UpdateTime"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["Code"].VisibleIndex = -1;
                ////treeList1.Columns["Code"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L1"].VisibleIndex = -1;
                ////treeList1.Columns["L1"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L7"].VisibleIndex = -1;
                treeList1.Columns["L7"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L11"].VisibleIndex = -1;
                ////treeList1.Columns["L11"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L12"].VisibleIndex = -1;
                ////treeList1.Columns["L12"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L13"].VisibleIndex = -1;
                ////treeList1.Columns["L13"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L14"].VisibleIndex = -1;
                ////treeList1.Columns["L14"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L15"].VisibleIndex = -1;
                ////treeList1.Columns["L15"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L16"].VisibleIndex = -1;
                ////treeList1.Columns["L16"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L17"].VisibleIndex = -1;
                treeList1.Columns["L17"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L18"].VisibleIndex = -1;
                ////treeList1.Columns["L18"].OptionsColumn.ShowInCustomizationForm = false;

                ////treeList1.Columns["L19"].VisibleIndex = -1;
                ////treeList1.Columns["L19"].OptionsColumn.ShowInCustomizationForm = false;
                ////treeList1.Columns["Remark"].VisibleIndex = -1;
                ////treeList1.Columns["Remark"].OptionsColumn.ShowInCustomizationForm = false;
                AddColumn2();
                PowerProYears psp_Year = new PowerProYears();
                psp_Year.Flag = typeFlag2;
                Application.DoEvents();
                LoadValues();
                TreeListColumn column = treeList1.Columns["L10"];
                TreeListColumn column1 = treeList1.Columns["L12"];
                for (int i = 0; i < treeList1.Nodes.Count; i++)
                {
                    CalculateNodesValue(treeList1.Nodes[i], column, column1);
                } 
                treeList1.ExpandAll();
            }
            catch
            {
            }
        }
        //读取数据
        private void LoadValues()
        {
            IList<PowerProValues> listValues = Common.Services.BaseService.GetList<PowerProValues>("SelectPowerProValuesListByFlag2_LangFang", typeFlag2);
            foreach (PowerProValues value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    node.SetValue(value.Year, value.Value);
                }
            }

        }
        //添加年份后，新增一列


        private void AddColumn(string year)
        {
            try
            {
                dataTable.Columns.Add(year , typeof(string));
                TreeListColumn column = treeList1.Columns.Insert(100);

                column.OptionsColumn.AllowEdit = false;
                column.FieldName = year ;
                column.Tag = year;
                column.Caption = year ;
                column.Width = 120;
                column.OptionsColumn.AllowSort = false;
                column.VisibleIndex = 100;//有两列隐藏列

                column.ColumnEdit = this.repositoryItemMemoEdit2;
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
            treeList1.RefreshDataSource();
        }

        private void AddColumn1()//静态总投资


        {
            TreeListColumn column = treeList1.Columns["Remark"];
            column = treeList1.Columns["Remark"];
            column.Caption = "备注";
            column.Width = 120;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex =-1;
            //column.ColumnEdit = repositoryItemTextEdit1;
            column.ColumnEdit = this.repositoryItemMemoEdit1;
           
            column.OptionsColumn.AllowEdit = false;

        }

        private void AddColumn2()//建设期间贷款利息
        {
            TreeListColumn column = treeList1.Columns["L4"];
            column = treeList1.Columns["L4"];
            column.Caption = " 电压等级";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 803;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L1"];
            column.Caption = " 区域";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 804;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L2"];
            column.Caption = " 建设性质";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 804;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L5"];
            column.Caption = "主变台数";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 804;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;
            column.Format.FormatString = "n2";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.AllNodesSummary = false;
            column.SummaryFooter = SummaryItemType.Sum;
            //column.SummaryFooterStrFormat = "{0:c}";

            column = treeList1.Columns["L6"];
            column.Caption = "单台容量(MVA)"; 
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 805;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;
            column.Format.FormatString = "n2";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.AllNodesSummary = false;
            column.SummaryFooter = SummaryItemType.Sum;
            //column.SummaryFooterStrFormat = "{0:c}";


            column = treeList1.Columns["IsConn"];
            column.Caption = "总容量(MVA)";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 805;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;
            column.Format.FormatString = "n2";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.AllNodesSummary = false;
            column.SummaryFooter = SummaryItemType.Sum;
            //column.SummaryFooterStrFormat = "{0:c}";

            ////column = treeList1.Columns["L7"];
            ////column.Caption = "建设形式"; 
            ////column.Width = 70;
            ////column.OptionsColumn.AllowSort = false;
            ////column.VisibleIndex = 806;
            ////column.ColumnEdit = repositoryItemTextEdit1;
            ////column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L8"];
            column.Caption = "线路长度(KM)"; 
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 807;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;
            column.Format.FormatString = "n2";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.AllNodesSummary = false;
            column.SummaryFooter = SummaryItemType.Sum;
            //column.SummaryFooterStrFormat = "{0:c}";

            column = treeList1.Columns["L9"];
            column.Caption = "导线型号"; 
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 808;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L10"];
            column.Caption = "标准造价(万元)";
            column.Width = 110;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 809;
            //column.ColumnEdit = repositoryItemTextEdit3;
            column.OptionsColumn.AllowEdit = false;
            column.Format.FormatString = "n2";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.AllNodesSummary = false;
            column.SummaryFooter = SummaryItemType.Sum;
            column.SummaryFooterStrFormat = "{0:c}";

            column = treeList1.Columns["L11"];
            column.Caption = "造价调整(万元)";
            column.Width = 110;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 810;
            //column.ColumnEdit = repositoryItemTextEdit3;
            column.OptionsColumn.AllowEdit = false;
            column.Format.FormatString = "n2";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.AllNodesSummary = false;
            column.SummaryFooter = SummaryItemType.Sum;
            column.SummaryFooterStrFormat = "{0:c}";

            column = treeList1.Columns["L12"];
            column.Caption = "工程总价(万元)";
            column.Width = 110;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 811;
            //column.ColumnEdit = repositoryItemTextEdit3;
            column.OptionsColumn.AllowEdit = false;

            column.Format.FormatString = "n2";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            ////repositoryItemTextEdit2.DisplayFormat.FormatString = "n2";
            ////    repositoryItemTextEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            ////    repositoryItemTextEdit2.EditFormat.FormatString = "n2";
            ////    repositoryItemTextEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            ////    repositoryItemTextEdit2.Mask.EditMask = "n2";
            ////    repositoryItemTextEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            column.AllNodesSummary = false;
            column.SummaryFooter = SummaryItemType.Sum;
            column.SummaryFooterStrFormat = "{0:c}";


            column = treeList1.Columns["L15"];
            column.Caption = "投产年限";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 812;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;


           
            //column.RowFooterSummary = SummaryItemType.Sum;
            //column.RowFooterSummaryStrFormat = "{0:c}";
            //col.Format.FormatString = "c";
       //col.RowFooterSummary = col.SummaryFooter = SummaryItemType.Sum;
       //     col.RowFooterSummaryStrFormat = col.SummaryFooterStrFormat = "Sum={0:c}";
       //     col.AllNodesSummary = true;
            //col = treeList1.Columns["Check"];
            //col.RowFooterSummary = SummaryItemType.Sum;
            //col.RowFooterSummaryStrFormat = "Checked: {0}";

        }

        //增加年份
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //if (e.Column.FieldName == "L10")
            //{
            //    SaveRemarkValue(treeList1.FocusedNode.GetValue("ID").ToString(), e.Value.ToString());
            //}


            //if (e.Column.FieldName != "Remark" && e.Column.FieldName != "Title")
            //{

            //    SaveCellValue(treeList1.FocusedColumn.Tag.ToString(), treeList1.FocusedNode.GetValue("ID").ToString(), e.Value.ToString());
            //}
        }

        private bool SaveRemarkValue(string typeID, string value)
        {
            PSP_Project_List ppt = new PSP_Project_List();
            ppt.ID = typeID;
            ppt.Flag2=typeFlag2;


            PSP_Project_List ps = Services.BaseService.GetOneByKey<PSP_Project_List>(ppt);//<PowerProTypes>(typeID);

            ps.Remark = value;
            try
            {
                Common.Services.BaseService.Update<PSP_Project_List>(ps);
            }
            catch (Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }



        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            //if (treeList1.FocusedNode.HasChildren)
            //{
            //    e.Cancel = true;
            //}
           // e.Cancel = false;
            if (treeList1.FocusedNode.ParentNode==null
                  || !base.EditRight)
            {
                e.Cancel = true;
            }

        }

        private void treeList1_ShownEditor(object sender, EventArgs e)
        {
            lastEditColumn = treeList1.FocusedColumn;
            lastEditNode = treeList1.FocusedNode;
            lastEditValue = treeList1.FocusedNode.GetValue(lastEditColumn.FieldName).ToString();
        }

        //统计
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form9Print_LangFang frm = new Form9Print_LangFang();
            frm.IsSelect = isSelect;
            frm.TZGS = "tzgs";
            frm.Text = this.ctrlPowerEachList1.FocusedObject.ListName;
            frm.GridDataTable = ConvertTreeListToDataTable(treeList1);
            
            if (frm.ShowDialog() == DialogResult.OK && isSelect)
            {
                gcontrol = frm.gridControl1;
                title = frm.Title;
                unit = "单位：万元";
                DialogResult = DialogResult.OK;
            }


        }

        //把树控件内容按显示顺序生成到DataTable中



        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();

            foreach (TreeListColumn column in xTreeList.Columns)
            {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(string));
            }
            foreach (TreeListNode node in xTreeList.Nodes)
            {
                AddNodeDataToDataTable(dt, node, listColID,"");
            }

            return dt;
        }

        private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID,string strtemp)
        {
            DataRow newRow = dt.NewRow();
            foreach(string colID in listColID)
            {
                //分类名，第二层及以后在前面加空格
                if (colID == "L3" && node.ParentNode != null)
                {
                    strtemp += "　";
                    newRow[colID] =strtemp+ node[colID];
                }
                else
                {
                        newRow[colID] =node[colID];
                }

             }


            //if(node.Nodes.Count>0 || node.ParentNode!=null)
             dt.Rows.Add(newRow);

            foreach(TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID,strtemp);
            }
        }
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            FormAddInfo_TouZiGuSuan2 fep = new FormAddInfo_TouZiGuSuan2();

            fep.FlagId = typeFlag2;
            fep.ParentID = "0";
            fep.Text = "增加项目";
            fep.Isupdate = false;
            if (fep.ShowDialog() == DialogResult.OK)
            {
                WaitDialogForm wait = null;

                try
                {
                    wait = new WaitDialogForm("", "正在重新统计数据, 请稍候...");
                    LoadDatadata();
                    wait.Close();
                  //  MsgBox.Show("计算成功");

                }
                catch
                {
                    wait.Close();
                }
            }
 
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            FormAddInfo_TouZiGuSuan frm = new FormAddInfo_TouZiGuSuan();
            frm.Text = "增加" + focusedNode.GetValue("L3") + "的子项目";
            frm.FlagId = typeFlag2;
            frm.ParentID = focusedNode.GetValue("ID").ToString();
            frm.Isupdate = false;
            if (frm.ShowDialog() == DialogResult.OK)
            {

                obj = frm.OBJ;
                TreeListColumn column = treeList1.Columns["L10"];
                CalculateSum(focusedNode, column, obj.L10, false);
              ////      LoadData();
              ////      FoucsLocation(psp_Type.ID, treeList1.Nodes); 
                    //treeList1.RefreshDataSource();
                    //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                WaitDialogForm wait = null;

                try
                {
                    wait = new WaitDialogForm("", "正在重新统计数据, 请稍候...");
                    LoadDatadata();
                    wait.Close();
                   // MsgBox.Show("计算成功");

                }
                catch
                {
                    wait.Close();
                }

           //treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
            }
        }
        private bool SaveCellValue(string UID, string value)
        {
            PSP_Project_List PowerValues = new PSP_Project_List();
            PowerValues.ID = UID;
            try
            {
                PowerValues = (PSP_Project_List)Common.Services.BaseService.GetObject("SelectPSP_PowerProValues_LangFangByID", PowerValues);
                PowerValues.L10 =double.Parse(value);
                Common.Services.BaseService.Update("UpdatePSP_Project_ListBy", PowerValues);
            }
            catch (Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }
        //当子分类数据改变时，计算其父分类的值



        private void CalculateSum(TreeListNode node, TreeListColumn column, double val,bool flag)
        {
            TreeListNode parentNode = node;

            if (parentNode == null)
            {
                return;
            }

            double sum = 0;
            if (flag == false)
            {
                foreach (TreeListNode nd in parentNode.Nodes)
                {
                    object value = nd.GetValue(column.FieldName);
                    if (value != null && value != DBNull.Value)
                    {
                        sum += Convert.ToDouble(value);
                    }
                }
            }
            else
            {
                foreach (TreeListNode nd in parentNode.Nodes)
                {
                    object value = nd.GetValue(column.FieldName);
                    if (value != null && value != DBNull.Value)
                    {
                        sum += Convert.ToDouble(value);
                    }
                }
            }

            parentNode.SetValue(column.FieldName, sum+val);

            if (!SaveCellValue((string)parentNode.GetValue("ID"), Convert.ToString(sum + val)))
            {
                return;
            }
            if (parentNode.ParentNode!=null)
                CalculateSum(parentNode.ParentNode, column, 0, flag);
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            //if (treeList1.FocusedNode.ParentNode == null)
            //{
            //    MsgBox.Show("一级项目名称不能修改！");
            //    return;
            //}
            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此项目下有子项目，不允许修改此记录！");
                return;
            }
            FormAddInfo_TouZiGuSuan2 frm = new FormAddInfo_TouZiGuSuan2();
            frm.FlagId = typeFlag2;
            frm.PowerUId =this.treeList1.FocusedNode.GetValue("ID").ToString();
            frm.Text = "修改项目";
            frm.Isupdate = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int i = this.treeList1.FocusedNode.Id;
               
                WaitDialogForm wait = null;

                try
                {
                    wait = new WaitDialogForm("", "正在重新统计数据, 请稍候...");
                    LoadDatadata();
                    wait.Close();
                    //MsgBox.Show("计算成功");

                }
                catch
                {
                    wait.Close();
                }
               
                //obj = frm.OBJ;
                TreeListColumn column = treeList1.Columns["L10"];
                CalculateSum(treeList1.FindNodeByID(i).ParentNode, column, 0,true);
                
                ////try
                ////{
                ////    string id = treeList1.FocusedNode["ID"].ToString();
                ////    string flag22 = typeFlag2;

                ////    PSP_Project_List pptss = new PSP_Project_List();
                ////    pptss.ID = id;
                ////    pptss.Flag2 = flag22;


                ////    PSP_Project_List psp_Type = Services.BaseService.GetOneByKey<PSP_Project_List>(pptss);
                ////    psp_Type.L3 = frm.TypeTitle;
                ////psp_Type.Flag = frm.PowerType;
                ////Common.Services.BaseService.Update<PSP_Project_List>(psp_Type);

                ////    if (psp_Type.Code != "")
                ////    {
                ////        LineInfo li3 = Services.BaseService.GetOneByKey<LineInfo>(psp_Type.Code);
                ////        if (li3 != null)
                ////        {
                ////            li3.LineName = psp_Type.L3;
                ////            Common.Services.BaseService.Update<LineInfo>(li3);
                ////        }

                ////        substation sb3 = Services.BaseService.GetOneByKey<substation>(psp_Type.Code);
                ////        if (sb3 != null)
                ////        {
                ////            sb3.EleName = psp_Type.L3;
                ////            Common.Services.BaseService.Update<substation>(sb3);
                ////        }
                ////    }

                ////    treeList1.FocusedNode.SetValue("L3", frm.TypeTitle);
                ////    FoucsLocation(id, treeList1.Nodes);
                ////}
                ////catch (Exception ex)
                ////{
                ////    //MsgBox.Show("修改出错：" + ex.Message);
                ////}
            }
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此项目下有子项目，请先删除子项目！");
                return;
            }
            if (treeList1.FocusedNode.ParentNode == null)
            {
                if (MsgBox.ShowYesNo("是否删除项目 " + treeList1.FocusedNode.GetValue("L3") + "？") == DialogResult.Yes)
                {
                    PowerProValues PowerValues = new PowerProValues();
                    PowerValues.TypeID = treeList1.FocusedNode["ID"].ToString();
                    PowerValues.Year = typeFlag2;

                    PSP_Project_List ppss = new PSP_Project_List();
                    ppss.ID = treeList1.FocusedNode["ID"].ToString();
                    ppss.Flag2 = typeFlag2;

                    PSP_Project_List ppss1 = Services.BaseService.GetOneByKey<PSP_Project_List>(ppss);
                    PowerValues.TypeID1 = ppss1.Code;

                    try
                    {
                        //DeletePowerValuesByType里面删除数据和分类



                        Common.Services.BaseService.Update("DeletePowerProValuesByType", PowerValues);

                        TreeListNode brotherNode = null;
                        try
                        {
                            if (treeList1.FocusedNode.ParentNode.Nodes.Count > 1)
                            {
                                brotherNode = treeList1.FocusedNode.PrevNode;
                                if (brotherNode == null)
                                {
                                    brotherNode = treeList1.FocusedNode.NextNode;
                                }
                            }
                        }
                        catch { }
                        Common.Services.BaseService.Update("DeletePSP_Project_ListByIDFlag2", ppss);
                        treeList1.DeleteNode(treeList1.FocusedNode);
                        WaitDialogForm wait = null;

                        try
                        {
                            wait = new WaitDialogForm("", "正在删除数据, 请稍候...");
                         //   LoadDatadata();
                            wait.Close();
                        //    MsgBox.Show("计算成功");

                        }
                        catch
                        {
                            wait.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("删除出错：" + ex.Message);
                    }
                }
                return;
            }
            else {

            if (MsgBox.ShowYesNo("是否删除项目 " + treeList1.FocusedNode.GetValue("L3") + "？") == DialogResult.Yes)
            {
                bool flag = false;
                PowerProValues PowerValues = new PowerProValues();
                PowerValues.TypeID = treeList1.FocusedNode["ID"].ToString();
                PowerValues.Year = typeFlag2;

                PSP_Project_List ppss = new PSP_Project_List();
                ppss.ID=treeList1.FocusedNode["ID"].ToString();
                ppss.Flag2=typeFlag2;

                PSP_Project_List ppss1 = Services.BaseService.GetOneByKey<PSP_Project_List>(ppss);
                PowerValues.TypeID1 = ppss1.Code;

                try
                {
                    //DeletePowerValuesByType里面删除数据和分类



                    Common.Services.BaseService.Update("DeletePowerProValuesByType", PowerValues);

                    TreeListNode brotherNode = null;
                    try
                    {
                        if (treeList1.FocusedNode.ParentNode.Nodes.Count > 1)
                        {
                            flag = false;
                            brotherNode = treeList1.FocusedNode.PrevNode;
                            if (brotherNode == null)
                            {
                                brotherNode = treeList1.FocusedNode.NextNode;
                            }
                        }
                        if (treeList1.FocusedNode.ParentNode.Nodes.Count == 1)
                        {
                            flag = true;
                        }
                     }
                    catch { }
                     Common.Services.BaseService.Update("DeletePSP_Project_ListByIDFlag2", ppss);
                    TreeListColumn column = treeList1.Columns["L10"];
                    treeList1.DeleteNode(treeList1.FocusedNode);
                    if(flag==false)
                       CalculateSum(treeList1.FocusedNode.ParentNode, column, 0,false);
                    else
                       CalculateSum(treeList1.FocusedNode, column, 0,false);
                   WaitDialogForm wait = null;

                   try
                   {
                       wait = new WaitDialogForm("", "正在删除数据, 请稍候...");
                      // LoadDatadata();
                       wait.Close();
                       //MsgBox.Show("计算成功");

                   }
                   catch
                   {
                       wait.Close();
                   }
                  
                }
                catch (Exception ex)
                {
                    MsgBox.Show("删除出错：" + ex.Message);
                }
            }
            }
          //            InitSodata();
          //obj= frm.OBJ;

            
          // CalculateSum(focusedNode, column, double.Parse(obj.L10));


        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();

        }
        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            if (!EditRight)
                return;
            if (treeList1.FocusedNode == null)
                return;
            FormAddInfo_TouZiGuSuan2 frm = new FormAddInfo_TouZiGuSuan2();
            frm.FlagId = typeFlag2;
            frm.PowerUId =this.treeList1.FocusedNode.GetValue("ID").ToString();
            frm.Text = "修改项目";
            frm.Isupdate = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int i = this.treeList1.FocusedNode.Id;
                WaitDialogForm wait = null;

                try
                {
                    wait = new WaitDialogForm("", "正在计算数据, 请稍候...");
                    LoadDatadata();
                    wait.Close();
                   // MsgBox.Show("计算成功");

                }
                catch
                {
                    wait.Close();
                }
                TreeListColumn column = treeList1.Columns["L10"];
                CalculateSum(treeList1.FindNodeByID(i).ParentNode, column, 0, true);
            }
            //FormAddInfo_TouZiGuSuan fep = new FormAddInfo_TouZiGuSuan();
            //fep.FlagId = typeFlag2;
            //fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
            //string uid1 = treeList1.FocusedNode["ID"].ToString();
            //LineInfo li = Common.Services.BaseService.GetOneByKey<LineInfo>(treeList1.FocusedNode["Code"].ToString());
            //if (li != null)
            //    fep.IsLine = true;
            //substation li1 = Common.Services.BaseService.GetOneByKey<substation>(treeList1.FocusedNode["ID"].ToString());
            //if (li1 != null)
            //    fep.IsPower = true;

            //if (fep.ShowDialog() == DialogResult.OK)
            //{
            //    LoadData();
            //    treeList1.ExpandAll();
            //    FoucsLocation(uid1, treeList1.Nodes);
            //}
        }

        private void 关联图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //////if (ctrlPowerEachList1.FocusedObject == null)
            //////    return;


            //////foreach (DataRow rws in dt.Rows)
            //////{
            //////    rws["C"] = false;
            //////}

            //////string sid = ctrlPowerEachList1.FocusedObject.UID;

            //////PowerPicSelect ppsn = new PowerPicSelect();
            //////ppsn.EachListID = sid;

            //////IList<PowerPicSelect> liss = Services.BaseService.GetList<PowerPicSelect>("SelectPowerPicSelectList", ppsn);

            //////foreach (PowerPicSelect pps in liss)
            //////{
            //////    foreach (DataRow rw in dt.Rows)
            //////    {
            //////        if (pps.PicSelectID == rw["A"].ToString())
            //////            rw["C"] = true;
            //////    }
            //////}
            //////FrmPicTypeSelect fpt = new FrmPicTypeSelect();
            //////fpt.DT = dt;
            //////if (fpt.ShowDialog() == DialogResult.OK)
            //////{
            //////    dt = fpt.DT;

            //////    int c=0;
            //////    foreach (PowerPicSelect pps1 in liss)
            //////    {
            //////        c=0;
            //////        foreach (DataRow rw in dt.Rows)
            //////        {
            //////            if (pps1.PicSelectID == rw["A"].ToString() && (bool)rw["C"])
            //////                c = 1;    
            //////        }
            //////        if (c == 0)
            //////        {
            //////            Services.BaseService.Delete<PowerPicSelect>(pps1);
            //////        }
            //////    }


            //////    foreach (DataRow rw1 in dt.Rows)
            //////    {
            //////        c = 0;
            //////        if ((bool)rw1["C"])
            //////        {
            //////            foreach (PowerPicSelect pps2 in liss)
            //////            {
            //////                if (pps2.PicSelectID == rw1["A"].ToString())
            //////                    c = 1;
            //////            }
            //////            if (c == 0)
            //////            {
            //////                PowerPicSelect pp3 = new PowerPicSelect();
            //////                pp3.EachListID = sid;
            //////                pp3.PicSelectID = rw1["A"].ToString();

            //////                Services.BaseService.Create<PowerPicSelect>(pp3);
            //////            }
            //////        }
            //////    }
            //////    InitSodata();  
            //////}
        }
        public void InitSodata()
        {
            if (ctrlPowerEachList1.FocusedObject == null)
                return;
           ////if (updat == "true")
           //// {
           ////     LoadDatadata();
           //// }
           //// else 
               
            if (updat == "false")
            {

                UpdataLoadDatadata();
             　 updat = "true";
            }
            else
            {
                LoadData();
                updat = "true";
            }       
        }

        private void FoucsLocation(string id)
        {
            foreach (TreeListNode tln in treeList1.Nodes)
            {
                if (tln["ID"].ToString() == id)
                    treeList1.FocusedNode = tln;
                    tln.Selected = true;
            }
        
        
        }
        private void FoucsLocation(string id, TreeListNodes tlns)
        {
            foreach (TreeListNode tln in tlns)
            {
                if (tln["ID"].ToString() == id)
                {
                    treeList1.FocusedNode = tln;
                    return;
                }
                FoucsLocation(id, tln.Nodes);
            }
          
        }
        private InputLanguage oldInput = null;
        private void treeList1_FocusedColumnChanged(object sender, DevExpress.XtraTreeList.FocusedColumnChangedEventArgs e)
        {
            try
            {
                DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edit = e.Column.ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
                if (edit != null && edit.Mask.MaskType == DevExpress.XtraEditors.Mask.MaskType.Numeric)
                {
                    oldInput = InputLanguage.CurrentInputLanguage;
                    InputLanguage.CurrentInputLanguage = null;
                }
                else
                {
                    if (oldInput != null && oldInput != InputLanguage.CurrentInputLanguage)
                    {
                        InputLanguage.CurrentInputLanguage = oldInput;
                    }
                }
            }
            catch { }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PowerEachList pel = this.ctrlPowerEachList1.FocusedObject;
            if (pel == null)
            {
                MsgBox.Show("请先添加项目规划");
                return;
            }
                  InsertLineData1();                  
        }

        private void InsertLineData1()
        {
            PowerEachList pel = this.ctrlPowerEachList1.FocusedObject;
            bool bl = true;
            string parentid = "";
            string id = "";
            string flag2 = "";
            TreeListNode tln = treeList1.FocusedNode;
            //if (tln != null)
            //{
            //    bl = false;
            //    try
            //    {
            //         parentid = tln["ParentID"].ToString();
            //         id = tln["ID"].ToString();
            //         flag2 = tln["Flag2"].ToString();
            //    }
            //    catch
            //    { }
            //}
            //else
           // {
                id = "0";
                flag2 = pel.UID;
 
            //}
        
            PSP_Project_List z = new PSP_Project_List();
            PowerProYears h = new PowerProYears();
            PowerProValues j = new PowerProValues();
          

            object obj = Services.BaseService.GetObject("SelectPowerProTypesList", "");
          
            try
            {               
                DataTable dts = new DataTable();
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    dts = GetExcel(op.FileName); 
              
                    for (int i = 1; i < dts.Rows.Count; i++)
                    {
                        if (dts.Rows[i][1].ToString().IndexOf("合计") >= 0)
                            continue;
                        z.ID = Guid.NewGuid().ToString();
                        string strflag = "";
                        foreach (DataColumn dc in dts.Columns)
                        {
                           strflag = dc.Caption.ToString();
                            try {
                            switch (strflag)
                            {
                                ////case "项目名称":
                                ////        z.Title = dts.Rows[i][dc.ColumnName].ToString(); 
                                ////    break;
                                case "区域":
                                    z.L1 = dts.Rows[i][dc.ColumnName].ToString();
                                    break;
                                case "建设性质":
                                    z.L2 = dts.Rows[i][dc.ColumnName].ToString();
                                    break;
                                case "项目名称":
                                        z.L3 = dts.Rows[i][dc.ColumnName].ToString();
                                    break;
                                case "电压等级":
                                    z.L4 = dts.Rows[i][dc.ColumnName].ToString(); 
                                    break;
                                case "主变台数":
                                    z.L5 = dts.Rows[i][dc.ColumnName].ToString();           
                                    break;
                                case "单台容量(MVA)":
                                    z.L6 = dts.Rows[i][dc.ColumnName].ToString(); 
                                    break;
                                case "总容量(MVA)":
                                    z.IsConn = dts.Rows[i][dc.ColumnName].ToString();
                                    break;
                                case "建设形式":
                                     z.L7 = dts.Rows[i][dc.ColumnName].ToString();
                                    break;
                                
                                case "线路长度(KM)":
                                    z.L8 = dts.Rows[i][dc.ColumnName].ToString(); 
                                    break;
                                case "导线型号":
                                    z.L9 = dts.Rows[i][dc.ColumnName].ToString();
                                    break;
                                //case "投资造价":
                                //    z.L11 = dts.Rows[i][dc.ColumnName].ToString();
                                //    break;
                                //case "造价比例":
                                //    z.L12 = dts.Rows[i][dc.ColumnName].ToString();
                                //    break;
                                case "标准造价(万元)":
                                    z.L10 =double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                                    break;
                                case "造价调整(万元)":
                                    z.L11 = dts.Rows[i][dc.ColumnName].ToString();
                                    break;
                                case "工程总价(万元)":
                                    z.L12 = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                                    break;
                                case "投产年限":
                                    z.L15 = dts.Rows[i][dc.ColumnName].ToString();
                                    break;
                                default:
                                    ////j.Value = dts.Rows[i][dc.ColumnName].ToString();
                                    ////j.TypeID = z.ID;
                                    ////j.Year = dc.ColumnName;
                                    ////j.TypeID1 = flag2;
                                    ////Services.BaseService.Update<PowerProValues>(j);
                                    break;
                            }
                        }
                        catch { }
                        }   
                        z.Flag2 = flag2;
                        z.ParentID = "0";
                        //z.Flag = 1;
                        PSP_Project_List lf = (PSP_Project_List)Services.BaseService.GetObject("SelectPSP_Project_ListByObject", z);
                   if (lf == null)
                   {
                       Services.BaseService.Create<PSP_Project_List>(z);
                   }
                 else
                   {
                       z.Code = lf.Code;
                       z.Flag = lf.Flag;
                       Services.BaseService.Update("UpdatePSP_Project_ListBytable", z);
                   }
                       
                    }
            }
            ReLoadData();
           } 
            catch { MsgBox.Show("导入格式不正确！"); }
        }

        private DataTable GetExcel(string filepach)
        {
            string str;
            FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

            try
            {
                fpSpread1.OpenExcel(filepach);
            }
            catch
            {
                string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
                File.Copy(filepach, filepath1);
                fpSpread1.OpenExcel(filepath1);
                File.Delete(filepath1);
            }
            DataTable dt = new DataTable();
            for (int k = 0; k < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            {
                //dt.Columns.Add("col" + k.ToString());
                string aa = fpSpread1.Sheets[0].Cells[2, k].Value.ToString();
                dt.Columns.Add(aa);

            }


            for (int i = 2; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data)-1; i++)
            {
                DataRow dr = dt.NewRow();
                str = "";
                for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {
                    str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
                    dr[j] = fpSpread1.Sheets[0].Cells[i, j].Text;
                }
                if (str != "")
                    dt.Rows.Add(dr);

            }
            return dt;
        }
        private void ReLoadData()
        {
            if (this.ctrlPowerEachList1.FocusedObject == null)
                return;

            typeFlag2 = this.ctrlPowerEachList1.FocusedObject.UID;

            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            WaitDialogForm wait = null;
            updat = "true";
            try
            {
                wait = new WaitDialogForm("", "正在计算数据, 请稍候...");
                LoadDatadata();
                wait.Close();
                //MsgBox.Show("计算成功");

            }
            catch
            {
                wait.Close();
            }
            //LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ctrlPowerEachList1.FocusedObject == null)
            {
                MsgBox.Show("没有要删除的数据！");
                return;
            }
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;

            string Flag2 = ctrlPowerEachList1.FocusedObject.UID;
            Services.BaseService.Update("DeletePSP_Project_ListByFlag2", Flag2);
            MsgBox.Show("清除成功！");

            LoadDatadata();
            //this.Cursor = Cursors.WaitCursor;
            //treeList1.BeginUpdate();
            //WaitDialogForm wait = null;

            //try
            //{
            //    wait = new WaitDialogForm("", "正在计算数据, 请稍候...");
            //    InitSodata();
            //    wait.Close();
            //  //  MsgBox.Show("计算成功");

            //}
            //catch
            //{
            //    wait.Close();
            //}
            //treeList1.EndUpdate();
            //this.Cursor = Cursors.Default;

        }

        private void 投资估算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProject_Sum fps = new FrmProject_Sum();
            fps.ShowDialog();
            //if (this.ctrlPowerEachList1.FocusedObject == null)
            //    return;

            //typeFlag2 = this.ctrlPowerEachList1.FocusedObject.UID;
            //ddd = DateTime.Now;
            //sss = "1:" + ddd.ToString();
            //this.Cursor = Cursors.WaitCursor;
            //treeList1.BeginUpdate();
            //InitSodata();
            ////LoadData();
            //treeList1.EndUpdate();
            //this.Cursor = Cursors.Default;
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FrmProject_Sum fsum = new FrmProject_Sum();
            fsum.Type ="2";
            fsum.Text= "变电站造价信息";
            fsum.ShowDialog();
    
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmProject_Sum fsum = new FrmProject_Sum();
            fsum.Type = "1";
            fsum.Text = "线路造价信息";
            fsum.ShowDialog();
    
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlPowerEachList1.FocusedObject == null)
                return;

            typeFlag2 = this.ctrlPowerEachList1.FocusedObject.UID;
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            WaitDialogForm wait = null;
            updat = "false";
            try
            {
                wait = new WaitDialogForm("", "正在计算数据, 请稍候...");
                InitSodata();
                wait.Close();              
            }
            catch
            {
                wait.Close();
            }
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {

        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList1.AddObjecta(type);
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList1.UpdateObject();
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList1.DeleteObject();
        }
    }
}