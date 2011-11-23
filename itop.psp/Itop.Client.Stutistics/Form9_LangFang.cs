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
    public partial class Form9_LangFang : FormBase
    {
        string  updat = "";
        string title = "";
        string unit = "兆伏安、公里";
        bool isSelect = false;
        string type = "JSXM_LangFang";

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
        PSP_PowerProValues_LangFang obj = new PSP_PowerProValues_LangFang();
        private string typeFlag2 = "";

        public Form9_LangFang()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ctrlPowerEachList1.LangFang=this;
            this.ctrlPowerEachList1.IsJSXM = true;
            this.ctrlPowerEachList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlPowerEachList1.RefreshData(type);

            Show();
            Application.DoEvents();
            InitRight();
            this.ctrlPowerEachList1.colListName.Caption = "规划名称";
            ctrlPowerEachList1.colListName.Width = 100;
            ctrlPowerEachList1.colRemark.Visible =false;
            //InitPicData()
        }
        private void InitPicData()
        {
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
            ddd = DateTime.Now;
            sss = "1:" + ddd.ToString();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            WaitDialogForm wait = null;

            try
            {
                wait = new WaitDialogForm("", "正在计算数据, 请稍候...");
                InitSodata();
                wait.Close();
               // MsgBox.Show("计算成功");

            }
            catch
            {
                wait.Close();
            }
            //LoadData();
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
                ddd = DateTime.Now;
                sss += " 10:" + ddd.ToString();
                PSP_PowerProValues_LangFang psp_Type = new PSP_PowerProValues_LangFang();
                psp_Type.Flag2 = typeFlag2;
                IList listTypes = new ArrayList();
                try
                {
                    listTypes = Common.Services.BaseService.GetList("SelectPSP_PowerProValues_LangFangByFlag2", psp_Type);

                }
                catch (Exception ex)
                { MsgBox.Show(ex.Message); }
                ddd = DateTime.Now;
                sss += " 11:" + ddd.ToString();
                Project_Sum ps = new Project_Sum();
                ps.S5 = "1";
                IList<Project_Sum> sumline = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);
                ps.S5 = "2";
                IList<Project_Sum> sumsubsation = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);

                foreach (PSP_PowerProValues_LangFang psplf in listTypes)
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
                                break;
                            }
 
                        }
                    }
                    if (psplf.Flag == 2)
                    {
                        double sumvaluedata = 0;
                        double sumvalueLine = 0;
                        if (psplf.L6 != "" && psplf.L6 != null && psplf.L5 != "" && psplf.L5 != null)
                        {
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

                ddd = DateTime.Now;
                sss += " 12:" + ddd.ToString();//测试运算时间，可删除

                sss += " 13:" + ddd.ToString();//测试运算时间，可删除
                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_PowerProValues_LangFang));
                //dataTable.DefaultView.Sort = "L4 desc";
                //dataTable.DefaultView.Sort = "Title desc";
                //dataTable.DefaultView.Sort = "CreateTime desc";
                ////foreach (DataRow rw in dataTable.Rows)
                ////{
                ////    string ss = rw["Code"].ToString();
                ////    LineInfo li = Common.Services.BaseService.GetOneByKey<LineInfo>(ss);
                    ////if (li != null && li.EleID != "")
                    ////{
                    ////    rw["L3"] = li.LineName;
                    ////    rw["L8"] = li.Length;
                    ////    rw["L9"] = li.LineType;
                    ////}
                    ////substation li1 = Common.Services.BaseService.GetOneByKey<substation>(ss);
                    ////if (li1 != null && li1.EleID != "")
                    ////{
                    ////    rw["L3"] = li1.EleName;
                    ////    //rw["L5"] = li1.ObligateField2;
                    ////    //rw["L2"] = li1.Number;
                    ////    //rw["L6"] = li1.Burthen;
                    ////}

                ////}
                treeList1.DataSource = dataTable;

                treeList1.Columns["L3"].Caption = "工程名称";
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


                treeList1.Columns["L1"].VisibleIndex = -1;
                treeList1.Columns["L1"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L2"].VisibleIndex = -1;
                treeList1.Columns["L2"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L11"].VisibleIndex = -1;
                treeList1.Columns["L11"].OptionsColumn.ShowInCustomizationForm = false;


                treeList1.Columns["L12"].VisibleIndex = -1;
                treeList1.Columns["L12"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L13"].VisibleIndex = -1;
                treeList1.Columns["L13"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L14"].VisibleIndex = -1;
                treeList1.Columns["L14"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L15"].VisibleIndex = -1;
                treeList1.Columns["L15"].OptionsColumn.ShowInCustomizationForm = false;


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
                ddd = DateTime.Now;
                sss += " 10:" + ddd.ToString();
                PSP_PowerProValues_LangFang psp_Type = new PSP_PowerProValues_LangFang();
                psp_Type.Flag2 = typeFlag2;
                IList listTypes = new ArrayList();
                try
                {
                    listTypes = Common.Services.BaseService.GetList("SelectPSP_PowerProValues_LangFangByFlag2", psp_Type);

                }
                catch (Exception ex)
                { MsgBox.Show(ex.Message); }
                ddd = DateTime.Now;
                sss += " 11:" + ddd.ToString();
                Project_Sum ps = new Project_Sum();
                ps.S5 = "1";
                IList<Project_Sum> sumline = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);
                ps.S5 = "2";
                IList<Project_Sum> sumsubsation = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);

                foreach (PSP_PowerProValues_LangFang psplf in listTypes)
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
                                break;
                            }

                        }
                    }
                    if (psplf.Flag == 2)
                    {
                        double sumvaluedata = 0;
                        double sumvalueLine = 0;
                        if (psplf.L6 != "" && psplf.L6 != null && psplf.L5 != "" && psplf.L5 != null)//变电站内的电站造价计算
                        {
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

                ddd = DateTime.Now;
                sss += " 12:" + ddd.ToString();//测试运算时间，可删除

                sss += " 13:" + ddd.ToString();//测试运算时间，可删除
                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_PowerProValues_LangFang));
                //dataTable.DefaultView.Sort = "L4 desc";
                //dataTable.DefaultView.Sort = "Title desc";
                //dataTable.DefaultView.Sort = "CreateTime desc";
                ////foreach (DataRow rw in dataTable.Rows)
                ////{
                ////    string ss = rw["Code"].ToString();
                ////    LineInfo li = Common.Services.BaseService.GetOneByKey<LineInfo>(ss);
                ////if (li != null && li.EleID != "")
                ////{
                ////    rw["L3"] = li.LineName;
                ////    rw["L8"] = li.Length;
                ////    rw["L9"] = li.LineType;
                ////}
                ////substation li1 = Common.Services.BaseService.GetOneByKey<substation>(ss);
                ////if (li1 != null && li1.EleID != "")
                ////{
                ////    rw["L3"] = li1.EleName;
                ////    //rw["L5"] = li1.ObligateField2;
                ////    //rw["L2"] = li1.Number;
                ////    //rw["L6"] = li1.Burthen;
                ////}

                ////}
                treeList1.DataSource = dataTable;

                treeList1.Columns["L3"].Caption = "工程名称";
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


                treeList1.Columns["L1"].VisibleIndex = -1;
                treeList1.Columns["L1"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L2"].VisibleIndex = -1;
                treeList1.Columns["L2"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L11"].VisibleIndex = -1;
                treeList1.Columns["L11"].OptionsColumn.ShowInCustomizationForm = false;


                treeList1.Columns["L12"].VisibleIndex = -1;
                treeList1.Columns["L12"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L13"].VisibleIndex = -1;
                treeList1.Columns["L13"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L14"].VisibleIndex = -1;
                treeList1.Columns["L14"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L15"].VisibleIndex = -1;
                treeList1.Columns["L15"].OptionsColumn.ShowInCustomizationForm = false;


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
                ddd = DateTime.Now;
                sss += " 10:" + ddd.ToString();
                PSP_PowerProValues_LangFang psp_Type = new PSP_PowerProValues_LangFang();
                psp_Type.Flag2 = typeFlag2;
                IList listTypes = new ArrayList();
                try
                {
                    listTypes = Common.Services.BaseService.GetList("SelectPSP_PowerProValues_LangFangByFlag2", psp_Type);

                }
                catch (Exception ex)
                { MsgBox.Show(ex.Message); }
                ddd = DateTime.Now;
                sss += " 11:" + ddd.ToString();
                Project_Sum ps = new Project_Sum();
                ps.S5 = "1";
                IList<Project_Sum> sumline = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);
                ps.S5 = "2";
                IList<Project_Sum> sumsubsation = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", ps);
                Hashtable ha = new Hashtable();
                ArrayList al = new ArrayList();

                foreach (PSP_PowerProValues_LangFang psplf in listTypes)
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
                            foreach (Project_Sum psum in sumsubsation)
                            {
                                if (psum.S1 == psplf.L4)
                                {
                                    try
                                    {
                                        double mva = double.Parse(psplf.IsConn.ToString());
                                        double t5 = Convert.ToDouble(psum.T5);//单台容量
                                        int ta = Convert.ToInt32(psum.T1);//主变台数
                                        if (mva == (t5 * ta))
                                        {
                                            ha.Add(t5, ta);
                                            al.Add(t5);

                                        }
                                    }
                                    catch { }
                                    if (al.Count >0)
                                    {
                                        double va = Convert.ToDouble(al[0].ToString());
                                        for (int ii = 0; ii < al.Count; ii++)
                                        {
                                            if (va < Convert.ToDouble(al[ii].ToString()))
                                                va = Convert.ToDouble(al[ii].ToString());
                                        }
                                        psplf.L5 = ha[va].ToString();
                                        psplf.L6 = va.ToString();
                                    }

                                    else
                                    {
                                        psplf.L5 = "";
                                        psplf.L6 = "";
 
                                    }
                                }
                            }
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
                        Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangByid",psplf);
                    }
                }

                ddd = DateTime.Now;
                sss += " 12:" + ddd.ToString();//测试运算时间，可删除

                sss += " 13:" + ddd.ToString();//测试运算时间，可删除
                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_PowerProValues_LangFang));

                treeList1.DataSource = dataTable;

                treeList1.Columns["L3"].Caption = "工程名称";
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


                treeList1.Columns["L1"].VisibleIndex = -1;
                treeList1.Columns["L1"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L2"].VisibleIndex = -1;
                treeList1.Columns["L2"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L11"].VisibleIndex = -1;
                treeList1.Columns["L11"].OptionsColumn.ShowInCustomizationForm = false;


                treeList1.Columns["L12"].VisibleIndex = -1;
                treeList1.Columns["L12"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L13"].VisibleIndex = -1;
                treeList1.Columns["L13"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L14"].VisibleIndex = -1;
                treeList1.Columns["L14"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["L15"].VisibleIndex = -1;
                treeList1.Columns["L15"].OptionsColumn.ShowInCustomizationForm = false;


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
                column.Width = 70;
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
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex =-1;
            //column.ColumnEdit = repositoryItemTextEdit1;
            column.ColumnEdit = this.repositoryItemMemoEdit1;
           
            column.OptionsColumn.AllowEdit = false;

        }

        private void AddColumn2()//建设期间贷款利息
        {
            TreeListColumn column = treeList1.Columns["L4"];
            //column.Caption = "设备名称";
            //column.Width = 70;
            //column.OptionsColumn.AllowSort = false;
            //column.VisibleIndex = 800;
            //column.ColumnEdit = repositoryItemTextEdit1;
            //column.OptionsColumn.AllowEdit = false;

            //column = treeList1.Columns["L2"];
            //column.Caption = "台数";
            //column.Width = 70;
            //column.OptionsColumn.AllowSort = false;
            //column.VisibleIndex = 801;
            //column.ColumnEdit = repositoryItemTextEdit1;
            //column.OptionsColumn.AllowEdit = false;

            ////column = treeList1.Columns["L3"];
            ////column.Caption = "工程名称";
            ////column.Width = 70;
            ////column.OptionsColumn.AllowSort = false;
            ////column.VisibleIndex = 802;
            //////column.VisibleIndex = -1;
            ////column.ColumnEdit = repositoryItemTextEdit1;
            ////column.OptionsColumn.AllowEdit = false;


            column = treeList1.Columns["L4"];
            column.Caption = " 电压等级";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 803;
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

            column = treeList1.Columns["L7"];
            column.Caption = "建设形式"; 
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 806;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

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
        private int GetInsertIndex(int year)
        {
            int nFixedColumns = typeof(PowerTypes).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中

            int nColumns = treeList1.Columns.Count;
            int nIndex = nFixedColumns;

            //还没有年份

            if (nColumns == nFixedColumns)
            {
            }
            else//已经有年份

            {
                bool bFind = false;

                //查找此年的位置

                for (int i = nFixedColumns + 1; i < nColumns; i++)
                {
                    //Tag存放年份
                    int tagYear1 = (int)treeList1.Columns[i - 1].Tag;
                    int tagYear2 = (int)treeList1.Columns[i].Tag;
                    if (tagYear1 < year && tagYear2 > year)
                    {
                        nIndex = i;
                        bFind = true;
                        break;
                    }
                }

                //不在最大和最小之间

                if (!bFind)
                {
                    int tagYear1 = (int)treeList1.Columns[nFixedColumns].Tag;

                    if (tagYear1 > year)//比第一个年份小
                    {
                        nIndex = nFixedColumns;
                    }
                    else
                    {
                        nIndex = nColumns;
                    }
                }
            }

            return nIndex;
        }


        private int GetInsertIndex2(int year)
        {
            int nFixedColumns = typeof(PowerTypes).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中

            int nColumns = treeList1.Columns.Count - 4;
            int nIndex = nFixedColumns;

            //还没有年份

            if (nColumns == nFixedColumns)
            {
            }
            else//已经有年份

            {
                bool bFind = false;

                //查找此年的位置

                for (int i = nFixedColumns + 1; i < nColumns; i++)
                {
                    //Tag存放年份
                    int tagYear1 = (int)treeList1.Columns[i - 1].Tag;
                    int tagYear2 = (int)treeList1.Columns[i].Tag;
                    if (tagYear1 < year && tagYear2 > year)
                    {
                        nIndex = i;
                        bFind = true;
                        break;
                    }
                }

                //不在最大和最小之间

                if (!bFind)
                {
                    int tagYear1 = (int)treeList1.Columns[nFixedColumns].Tag;
                    int tagYear2 = (int)treeList1.Columns[nColumns - 1].Tag;

                    if (tagYear1 > year)//比第一个年份小
                    {
                        nIndex = nFixedColumns;
                    }
                    else
                    {
                        nIndex = nColumns;
                    }
                }
            }

            return nIndex;
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
            PSP_PowerProValues_LangFang ppt = new PSP_PowerProValues_LangFang();
            ppt.ID = typeID;
            ppt.Flag2=typeFlag2;


            PSP_PowerProValues_LangFang ps = Services.BaseService.GetOneByKey<PSP_PowerProValues_LangFang>(ppt);//<PowerProTypes>(typeID);

            ps.Remark = value;
            try
            {
                Common.Services.BaseService.Update<PSP_PowerProValues_LangFang>(ps);
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

        //根据选择的统计年份，生成统计结果数据表

        private DataTable ResultDataTable(DataTable sourceDataTable, List<ChoosedYears> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Title", typeof(string));
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                dtReturn.Columns.Add(choosedYear.Year + "年", typeof(double));
                if(choosedYear.WithIncreaseRate)
                {
                    dtReturn.Columns.Add(choosedYear.Year + "增长率", typeof(double)).Caption = "增长率";
                }
            }

            int nRowTotalPower = 0;//总用电量所在行
            int nRowMaxLoad = 0;//最高负荷所在行
            int nRowPopulation = 0;//人口所在行
            int nRowDenizen = 0;//居民用电量所在行

            #region 填充数据，获取总用电量所在行、最高负荷所在行、人口所在行
            for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            {
                DataRow newRow = dtReturn.NewRow();
                DataRow sourceRow = sourceDataTable.Rows[i];
                foreach(DataColumn column in dtReturn.Columns)
                {
                    if(column.Caption != "增长率")
                    {
                        newRow[column.ColumnName] = sourceRow[column.ColumnName];
                    }
                }
                dtReturn.Rows.Add(newRow);

                if (sourceRow["Flag"] != DBNull.Value)
                {
                    if((int)sourceRow["ParentID"] == 0)
                    {
                        switch((int)sourceRow["Flag"])
                        {
                            case 2://总用电量
                                nRowTotalPower = i;
                                break;

                            case 4://最高负荷，后面加一行Tmax
                                nRowMaxLoad = i;
                                dtReturn.Rows.Add(new object[] { "Tmax" });
                                break;

                            case 7://总人口

                                nRowPopulation = i + 1;//由于之前加了一行TMax，此处行加1
                                dtReturn.Rows.Add(new object[] { "人均用电量" });
                                dtReturn.Rows.Add(new object[] { "人均生活用电量" });
                                break;

                            default:
                                break;
                        }
                    }
                    else if (sourceRow["Title"].ToString().IndexOf("居民") > -1)
                    {
                        nRowDenizen = i;
                    }
                }
            }
            #endregion

            #region 计算TMax和人均用电量
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                object numerator = dtReturn.Rows[nRowTotalPower][choosedYear.Year + "年"];
                object denominator = dtReturn.Rows[nRowMaxLoad][choosedYear.Year + "年"];
                if(numerator != DBNull.Value)
                {
                    if(denominator != DBNull.Value)
                    {
                        try
                        {
                            dtReturn.Rows[nRowMaxLoad + 1][choosedYear.Year + "年"] = (int)((double)numerator / (double)denominator);
                        }
                        catch
                        {
                        }
                    }

                    denominator = dtReturn.Rows[nRowPopulation][choosedYear.Year + "年"];
                    if (denominator != DBNull.Value)
                    {
                        try
                        {
                            dtReturn.Rows[nRowPopulation + 1][choosedYear.Year + "年"] = System.Math.Round((double)numerator / (double)denominator, 3);
                        }
                        catch
                        {
                        }
                    }
                }

                numerator = dtReturn.Rows[nRowDenizen][choosedYear.Year + "年"];
                denominator = dtReturn.Rows[nRowPopulation][choosedYear.Year + "年"];
                if (denominator != DBNull.Value && numerator != DBNull.Value)
                {
                    try
                    {
                        dtReturn.Rows[nRowPopulation + 2][choosedYear.Year + "年"] = System.Math.Round((double)numerator / (double)denominator, 3);
                    }
                    catch
                    {
                    }
                }
            }
            #endregion

            #region 计算增长率

            DataColumn columnBegin = null;
            foreach(DataColumn column in dtReturn.Columns)
            {
                if(column.ColumnName.IndexOf("年") > 0)
                {
                    if(columnBegin == null)
                    {
                        columnBegin = column;
                    }
                }
                else if(column.ColumnName.IndexOf("增长率") > 0)
                {
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 1];

                    foreach(DataRow row in dtReturn.Rows)
                    {
                        if(row["Title"].ToString() == "Tmax")
                        {
                            continue;
                        }

                        object numerator = row[columnEnd];
                        object denominator = row[columnBegin];

                        if(numerator != DBNull.Value && denominator != DBNull.Value)
                        {
                            try
                            {
                                int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("年", ""))
                                    - Convert.ToInt32(columnBegin.ColumnName.Replace("年", ""));
                                row[column] = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
                            }
                            catch
                            {
                            }
                        }
                    }

                    //本次计算增长率的列作为下次的起始列

                    columnBegin = columnEnd;
                }

            }
            #endregion

            return dtReturn;
        }



        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            updat = "true";
            this.ctrlPowerEachList1.AddObjecta(type);
           //InitSodata();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            updat = "true";
            this.ctrlPowerEachList1.UpdateObject();
           InitSodata();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            updat = "true";
            this.ctrlPowerEachList1.DeleteObject("gusuan");
            InitSodata();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FormAddInfo_LangFang fep = new FormAddInfo_LangFang();
            fep.FlagId = typeFlag2;
            fep.ParentID = "0";
            fep.Text = "增加项目";
            fep.Isupdate = false;
            if (fep.ShowDialog() == DialogResult.OK)
            {
                WaitDialogForm wait = null;

                try
                {
                    wait = new WaitDialogForm("", "正在计算数据, 请稍候...");
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

            FormAddInfo_LangFang frm = new FormAddInfo_LangFang();
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
                    wait = new WaitDialogForm("", "正在计算数据, 请稍候...");
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
            PSP_PowerProValues_LangFang PowerValues = new PSP_PowerProValues_LangFang();
            PowerValues.ID = UID;
            try
            {
                PowerValues = (PSP_PowerProValues_LangFang)Common.Services.BaseService.GetObject("SelectPSP_PowerProValues_LangFangByID", PowerValues);
                PowerValues.L10 =double.Parse(value);
                Common.Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangByID", PowerValues);
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
            FormAddInfo_LangFang frm = new FormAddInfo_LangFang();
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

                ////    PSP_PowerProValues_LangFang pptss = new PSP_PowerProValues_LangFang();
                ////    pptss.ID = id;
                ////    pptss.Flag2 = flag22;


                ////    PSP_PowerProValues_LangFang psp_Type = Services.BaseService.GetOneByKey<PSP_PowerProValues_LangFang>(pptss);
                ////    psp_Type.L3 = frm.TypeTitle;
                ////psp_Type.Flag = frm.PowerType;
                ////Common.Services.BaseService.Update<PSP_PowerProValues_LangFang>(psp_Type);

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

                    PSP_PowerProValues_LangFang ppss = new PSP_PowerProValues_LangFang();
                    ppss.ID = treeList1.FocusedNode["ID"].ToString();
                    ppss.Flag2 = typeFlag2;

                    PSP_PowerProValues_LangFang ppss1 = Services.BaseService.GetOneByKey<PSP_PowerProValues_LangFang>(ppss);
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
                        Common.Services.BaseService.Update("DeletePSP_PowerProValues_LangFangByIDFlag2", ppss);
                        treeList1.DeleteNode(treeList1.FocusedNode);
                        WaitDialogForm wait = null;

                        try
                        {
                            wait = new WaitDialogForm("", "正在计算数据, 请稍候...");
                            LoadDatadata();
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

                PSP_PowerProValues_LangFang ppss = new PSP_PowerProValues_LangFang();
                ppss.ID=treeList1.FocusedNode["ID"].ToString();
                ppss.Flag2=typeFlag2;

                PSP_PowerProValues_LangFang ppss1 = Services.BaseService.GetOneByKey<PSP_PowerProValues_LangFang>(ppss);
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
                    Common.Services.BaseService.Update("DeletePSP_PowerProValues_LangFangByIDFlag2",ppss);
                    TreeListColumn column = treeList1.Columns["L10"];
                    treeList1.DeleteNode(treeList1.FocusedNode);
                    if(flag==false)
                       CalculateSum(treeList1.FocusedNode.ParentNode, column, 0,false);
                    else
                       CalculateSum(treeList1.FocusedNode, column, 0,false);
                   WaitDialogForm wait = null;

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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FormNewType4_LangFang frm = new FormNewType4_LangFang();
                
                frm.Flag2 = typeFlag2;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    AddColumn(frm.Type);
                }

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }




        private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedColumn == null)
            {
                return;
            }


            if (treeList1.FocusedColumn.FieldName == "Title" || treeList1.FocusedColumn.FieldName == "Remark" || treeList1.FocusedColumn.FieldName.IndexOf("L")==0)
            {
                return;
            }

            try
            {
                FormNewType4_LangFang frm = new FormNewType4_LangFang();


                frm.IsUpdate = true;
                frm.Type = treeList1.FocusedColumn.Caption;
                frm.Flag2 = typeFlag2;
                frm.Type1 = ctrlPowerEachList1.FocusedObject.UID;

                

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    treeList1.FocusedColumn.Caption = frm.Type;
                }

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedColumn == null)
            {
                return;
            }

            string fieldName = treeList1.FocusedColumn.FieldName;

            if (fieldName == "Title" || fieldName == "Remark" || fieldName.IndexOf("L") == 0)
            {
                return;
            }


            if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedColumn.Caption+ " 及该类别数据？") != DialogResult.Yes)
            {
                return;
            }

            Hashtable hs = new Hashtable();
            hs.Add("ID", typeFlag2);
            hs.Add("Year", treeList1.FocusedColumn.Caption.ToString());

            try
            {
                //DeletePowerValuesByYear删除数据和年份

                int colIndex = treeList1.FocusedColumn.AbsoluteIndex;
                Common.Services.BaseService.Update("DeletePowerProValuesByYear_LangFang", hs);
                dataTable.Columns.Remove(treeList1.FocusedColumn.FieldName);
                treeList1.Columns.Remove(treeList1.FocusedColumn);
                if (colIndex >= treeList1.Columns.Count)
                {
                    colIndex--;
                }
                treeList1.FocusedColumn = treeList1.Columns[colIndex];
            }
            catch (Exception ex)
            {
                MsgBox.Show("删除出错：" + ex.Message);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();

        }

        private void barButtonItem5_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //if (treeList1.FocusedNode.ParentNode == null)
            //    return;
            FrmEditProject_LangFang fep = new FrmEditProject_LangFang();
            fep.FlagId = typeFlag2;
            fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
            string uid1 = treeList1.FocusedNode["ID"].ToString();
            int flag = int.Parse(treeList1.FocusedNode["Flag"].ToString());
            fep.IsLineFlag = flag.ToString();


            Itop.Domain.Graphics.LineInfo li = Common.Services.BaseService.GetOneByKey<Itop.Domain.Graphics.LineInfo>(treeList1.FocusedNode["Code"].ToString());
            if (li != null)
                fep.IsLine = true;
            substation li1 = Common.Services.BaseService.GetOneByKey<substation>(treeList1.FocusedNode["ID"].ToString());
            if (li1 != null)
                fep.IsPower = true;
            if (fep.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                treeList1.ExpandAll();
                FoucsLocation(uid1, treeList1.Nodes);
            }
        }
           
        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            if (!EditRight)
                return;
            if (treeList1.FocusedNode == null)
                return;
            FormAddInfo_LangFang frm = new FormAddInfo_LangFang();
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
            //FormAddInfo_LangFang fep = new FormAddInfo_LangFang();
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

            string sid = typeFlag2= ctrlPowerEachList1.FocusedObject.UID;
      
            Hashtable hs = new Hashtable();
            Hashtable hs1 = new Hashtable();
            ddd = DateTime.Now;
            sss += "2:" + ddd.ToString();
            IList<Itop.Domain.Graphics.LineInfo> listLineInfo = Services.BaseService.GetList<Itop.Domain.Graphics.LineInfo>("SelectLineInfoByPowerID", sid);
            foreach (Itop.Domain.Graphics.LineInfo l1 in listLineInfo)
            {
                hs.Add(Guid.NewGuid().ToString(), l1.UID);
            }
            ddd = DateTime.Now;
            sss += "3:" + ddd.ToString();
            IList<substation> listsubstation = Services.BaseService.GetList<substation>("SelectsubstationByPowerID2", sid);
            foreach (substation s1 in listsubstation)
            {
                hs.Add(Guid.NewGuid().ToString(), s1.UID);
            }
            ddd = DateTime.Now;
            sss += "4:" + ddd.ToString();
            PSP_PowerProValues_LangFang psp_Type = new PSP_PowerProValues_LangFang();
            psp_Type.Flag2 = sid;
            IList<PSP_PowerProValues_LangFang> listProTypes = Common.Services.BaseService.GetList<PSP_PowerProValues_LangFang>("SelectPSP_PowerProValues_LangFangByFlag2", psp_Type);
            foreach (PSP_PowerProValues_LangFang ps in listProTypes)
            {
                hs1.Add(Guid.NewGuid().ToString(), ps.Code);
            }
            ddd = DateTime.Now;
            sss += "5:" + ddd.ToString();
            foreach (PSP_PowerProValues_LangFang p1 in listProTypes)
            {

                if (p1.Code != "" && !hs.ContainsValue(p1.Code) && p1.ParentID != "0")
                {
                    //删除
                    Services.BaseService.Delete<PSP_PowerProValues_LangFang>(p1);
                }
            }
            ddd = DateTime.Now;
            sss += "6:" + ddd.ToString();

            foreach (Itop.Domain.Graphics.LineInfo l2 in listLineInfo)
            {
                if (!hs1.ContainsValue(l2.UID) && l2.Voltage != "")
                {
                    //添加
                    try
                    {
                        PSP_PowerProValues_LangFang ps2 = new PSP_PowerProValues_LangFang();
                        ps2.ParentID = l2.Voltage.ToUpper().Replace("KV", "");
                        ps2.L3 = l2.LineName;
                        ps2.Code = l2.UID;
                        ps2.Flag = 1;
                        ps2.Flag2 = sid;
                        ps2.L4 = l2.Voltage;
                        ps2.L8 = double.Parse(l2.Length).ToString(); ;
                        ps2.L9 = l2.LineType;
                        ps2.ID = Guid.NewGuid().ToString();
                        Services.BaseService.Create<PSP_PowerProValues_LangFang>(ps2);
                    }
                    catch( Exception ex){
                        System.Console.WriteLine(ex.Message);
                    }

                }

                if (hs1.ContainsValue(l2.UID) && l2.Voltage != "")
                {
                    //更新
                    try
                    {
                        PSP_PowerProValues_LangFang p2 =   new PSP_PowerProValues_LangFang();
                        p2.Code = l2.UID;
                        p2.Flag2 = sid;
                        PSP_PowerProValues_LangFang ps2=(PSP_PowerProValues_LangFang)Services.BaseService.GetObject("SelectPSP_PowerProValues_LangFangByselectObject", p2);
                        ps2.ParentID = l2.Voltage.ToUpper().Replace("KV", "");
                        ps2.L3 = l2.LineName;
                        ps2.Flag = 1;
                        ps2.L4 = l2.Voltage;
                        ps2.L8 = double.Parse(l2.Length).ToString(); ;
                        ps2.L9 = l2.LineType;
                      
                        Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangByCode", ps2);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }

                }
            }
            ddd = DateTime.Now;
            sss += "7:" + ddd.ToString();


            //ps.Type = OBJ.L7;
            Project_Sum psp = new Project_Sum();
            psp.S5 = "2";
            IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", psp);

            Hashtable ha = new Hashtable();
            ArrayList al=new ArrayList();
            foreach (substation s2 in listsubstation)
            {
               
                if (!hs1.ContainsValue(s2.UID) && s2.ObligateField1 != "")
                {

                    ha.Clear();
                    al.Clear();
                    int kk = 0;
                    //添加
                    try
                    {
                        Substation_Info sub = new Substation_Info();
                        sub.Code = s2.UID;
                        Substation_Info station = (Substation_Info)Common.Services.BaseService.GetObject("SelectSubstation_InfoByCode", sub);


                        PSP_PowerProValues_LangFang ps3 = new PSP_PowerProValues_LangFang();
                        ps3.ParentID = s2.ObligateField1;
                        ps3.L3 = s2.EleName;
                        ps3.Code = s2.UID;
                        ps3.Flag = 2;
                        ps3.Flag2 = sid;
                        ps3.L4 = s2.ObligateField1.ToString();
                        if (station != null)
                        {
                            ps3.L5 = station.L3.ToString();
                        }
                        foreach (Project_Sum ps1 in sum)
                        {
                            if (s2.ObligateField1.ToString() == ps1.S1.ToString())
                            {
                                try
                                {
                                    double mva = double.Parse(s2.Number.ToString());
                                    double t5 = Convert.ToDouble(ps1.T5);//单台容量
                                    int ta = Convert.ToInt32(ps1.T1);//主变台数
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
                            ps3.L5 = ha[va].ToString();
                            ps3.L6 = va.ToString();
                        }
                        else
                        {
                            ps3.L5 = "";
                            ps3.L6 = "";
                        }
                        ps3.IsConn = double.Parse(s2.Number.ToString()).ToString();//总容量
                        ps3.ID = Guid.NewGuid().ToString();
                        Services.BaseService.Create<PSP_PowerProValues_LangFang>(ps3);
                    }
                    catch { }

                }
                if (hs1.ContainsValue(s2.UID) && s2.ObligateField1 != "")
                {
                    ha.Clear();
                    al.Clear();
                    int kk = 0;
                    //更新
                    try
                    {
                        Substation_Info sub = new Substation_Info();
                        sub.Code = s2.UID;
                        Substation_Info station = (Substation_Info)Common.Services.BaseService.GetObject("SelectSubstation_InfoByCode", sub);

                        PSP_PowerProValues_LangFang p3 = new PSP_PowerProValues_LangFang();
                        p3.Code = s2.UID;
                        p3.Flag2 = sid;
                        PSP_PowerProValues_LangFang ps3 = (PSP_PowerProValues_LangFang)Services.BaseService.GetObject("SelectPSP_PowerProValues_LangFangByselectObject", p3);
                        ps3.ParentID = s2.ObligateField1;
                        ps3.L3 = s2.EleName;
                        ps3.Flag = 2;
                        ps3.L4 = s2.ObligateField1.ToString();
                        if (station != null)
                        {
                            ps3.L5 = station.L3.ToString();
                        }
                        foreach (Project_Sum ps1 in sum)
                        {
                            if (s2.ObligateField1.ToString() == ps1.S1.ToString())
                            {
                                try
                                {
                                    double mva = double.Parse(s2.Number.ToString());
                                    double t5 = Convert.ToDouble(ps1.T5);//单台容量
                                    int ta = Convert.ToInt32(ps1.T1);//主变台数
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
                            ps3.L5 = ha[va].ToString();
                            ps3.L6 = va.ToString();
                        }
                        else
                        {
                            ps3.L5 = "";
                            ps3.L6 = "";
                        }
                        ps3.IsConn = double.Parse(s2.Number.ToString()).ToString();//总容量
                        Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangByCode", ps3);
                    }
                    catch { }
                }
            }
            ddd = DateTime.Now;
            sss += "8:" + ddd.ToString();
            if (updat == "true")
            {
                LoadDatadata();
            }
            else if (updat == "false")
            {

                UpdataLoadDatadata();
                updat = "true";
            }
            else
            {
                LoadData();
                updat = "true";
            }
            ddd = DateTime.Now;
            sss += "9:" + ddd.ToString();
        
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
        
            PSP_PowerProValues_LangFang z = new PSP_PowerProValues_LangFang();
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
                                case "项目名称":
                                        z.Title = dts.Rows[i][dc.ColumnName].ToString(); 
                                    break;
                                case "工程名称":
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
                                default:
                                    j.Value = dts.Rows[i][dc.ColumnName].ToString();
                                    j.TypeID = z.ID;
                                    j.Year = dc.ColumnName;
                                    j.TypeID1 = flag2;
                                    Services.BaseService.Update<PowerProValues>(j);
                                    break;
                            }
                        }
                        catch { }
                        }   
                        z.Flag2 = flag2;
                        z.ParentID = "0";
                        //z.Flag = 1;
                   PSP_PowerProValues_LangFang lf=(PSP_PowerProValues_LangFang)Services.BaseService.GetObject("SelectPSP_PowerProValues_LangFangByObject",z);
                   if (lf == null)
                   {
                       Services.BaseService.Create<PSP_PowerProValues_LangFang>(z);
                   }
                 else
                   {
                       z.Code = lf.Code;
                       z.Flag = lf.Flag;
                       Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangBytable", z);
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

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        
            if (this.ctrlPowerEachList1.FocusedObject == null)
                return;
            FormClass fc = new FormClass();
            gridControl1.DataSource = fc.ConvertTreeListToDataTable(treeList1);
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
            Services.BaseService.Update("DeletePSP_PowerProValues_LangFangByFlag2", Flag2);
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
               // MsgBox.Show("计算成功");
               
            }
            catch
            {
                wait.Close();
            }
         
            //LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }
    }
}