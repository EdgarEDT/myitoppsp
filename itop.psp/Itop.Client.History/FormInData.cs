using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Windows.Forms.Design;
using Itop.Client.Base;
using System.Collections;
using Itop.Common;
using DevExpress.XtraTreeList.Nodes;
using Itop.Domain.Forecast;
using DevExpress.Utils;
using Itop.Client.Common;
using System.IO;
using DevExpress.XtraTreeList.Columns;
using System.Reflection;
using Itop.Domain.Table;
using DevExpress.XtraEditors.Repository;
using Itop.TLPSP.DEVICE;
using DevExpress.XtraGrid.Columns;

namespace Itop.Client.History
{
    public partial class FormInData :FormBase
    {
        public FormInData()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            AddFixeData();
            
        }
        private DataTable DT = new DataTable();
        private Hashtable FileTable = new Hashtable();
        private List<InDataClass> InList = new List<InDataClass>();
        private void AddFixeData()
        {
            InList.Clear();
            InDataClass JI = new InDataClass();
            JI.Name = "经济人口数据";
            JI.FileName = "经济人口数据";
            JI.Flag = 1;
            JI.YearFlag = "电力发展实绩经济";
            JI.SType = "5";
            JI.IType = 5;
            InList.Add(JI);

            InDataClass DL = new InDataClass();
            DL.Name = "电量数据";
            DL.FileName = "电量数据";
            DL.Flag = 1;
            DL.YearFlag = "电力发展实绩电量";
            DL.SType = "6";
            DL.IType = 6;
            InList.Add(DL);


            InDataClass FH = new InDataClass();
            FH.Name = "负荷数据";
            FH.FileName = "负荷数据";
            FH.Flag = 1;
            FH.YearFlag = "电力发展实绩负荷";
            FH.SType = "7";
            FH.IType = 7;
            InList.Add(FH);

            InDataClass DY = new InDataClass();
            DY.Name = "电源";
            DY.FileName = "电源";
            DY.Flag = 2;
            DY.YearFlag = "";
            DY.Sclass = "Itop.TLPSP.DEVICE.UCDeviceDY";
            DY.SType = "30";
            InList.Add(DY);


            InDataClass BDZ = new InDataClass();
            BDZ.Name = "变电站";
            BDZ.FileName = "变电站";
            BDZ.Flag = 2;
            BDZ.YearFlag = "";
            BDZ.Sclass = "Itop.TLPSP.DEVICE.UCDeviceBDZ";
            BDZ.SType = "20";
            InList.Add(BDZ);


            InDataClass XL= new InDataClass();
            XL.Name = "线路";
            XL.FileName = "线路";
            XL.Flag = 2;
            XL.YearFlag = "";
            XL.Sclass = "Itop.TLPSP.DEVICE.UCDeviceXL";
            XL.SType = "05";
            InList.Add(XL);



        }
        private UCDeviceBase GetDevice(string strID, string dtype)
        {
              UCDeviceBase device = null;
              device = createInstance(dtype);
              device.ID = strID;
              device.ProjectID = ProjectUID;
              return device;
        }
        #region 私有方法
        /// <summary>
        /// 实例化类接口
        /// </summary>
        /// <param name="classname"></param>
        /// <returns></returns>
        private UCDeviceBase createInstance(string classname) {
            return Assembly.LoadFile(Application .StartupPath+"\\Itop.TLPSP.DEVICE.dll").CreateInstance(classname, false) as UCDeviceBase;
        }
	    #endregion
        private void sbtnInData_Click(object sender, EventArgs e)
        {
            if (hasfiles)
            {
                DT.Rows.Clear();
                int m = 1;
                foreach (InDataClass idc in InList)
                {
                    DataRow row = DT.NewRow();
                    row["num"] = m++;
                    row["modelname"] = idc.Name;
                    row["filename"] = idc.Key;
                   
                    if (idc.Key != string.Empty)
                    {
                        if (idc.Flag == 1)
                        {
                         row["success"] = InsertdataHistory(idc, FileTable[idc.Key].ToString()); 
                        }
                        else
                        {
                          row["success"] = InsertDataPsp(idc, FileTable[idc.Key].ToString());
                        }
                    }
                    DT.Rows.Add(row);
                }
                gridControl1.DataSource = DT;
                gridControl1.Refresh();

                MessageBox.Show("已完成操作！");
            }
            else
            {
                MessageBox.Show("没有可用的导入文件！");
            }

        }
        bool hasfiles=false;
        private Hashtable GetFiles(string path,string filter)
        {
            hasfiles = false;
            Hashtable ht=new Hashtable();
            string[] fn = Directory.GetFiles(path); 
            foreach (string s in fn)  
            {
                string filename =s.Replace(path+"\\","");
                if (filename.Contains(filter))
	            {
                    if (HasFiles(filename)&&!ht.ContainsKey(filename))
                    {
                        hasfiles = true;
                        ht.Add(filename, s);  
                    }
                 
	            }
            }
            return ht;
        }
        private bool HasFiles(string filename)
        {
            bool result = false;
            foreach (InDataClass idc in InList)
            {
                if (idc.Key==string.Empty&&filename.Contains(idc.FileName))
                {
                    idc.Key = filename;
                    result= true;
                    break;
                }
            }
            return result;
        }
        int firstyear = 2000;
        int endyear = 2012;
        private void DealTreelist(InDataClass idc)
        {
            SetYear(idc.YearFlag);
            AddFixColumn();
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = idc.IType;
            psp_Type.Col4 = ProjectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            treeList1.DataSource = listTypes;
        }
        private void SetYear(string yearflag)
        {
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = yearflag;
            py.Col5 = ProjectUID;

            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {
                firstyear = 2000;
                endyear = 2008;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }
           

        }
        //添加固定列
        private void AddFixColumn()
        {
            treeList1.Columns.Clear();
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 0;
            column.Width = 210;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "ParentID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "Col10";
            column.VisibleIndex = -1;

            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            for (int i = firstyear; i <= endyear; i++)
            {
                AddColumn(i);
            }
        }
        private void AddColumn(int year)
        {
            TreeListColumn column = new TreeListColumn();

            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = year + "年";
            column.Name = year.ToString();
            column.Width = 80;
            //column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.VisibleIndex = year;//有两列隐藏列

            // 
            // repositoryItemTextEdit1
            //
            RepositoryItemTextEdit repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.DisplayFormat.FormatString = "n2";
            repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            repositoryItemTextEdit1.Mask.EditMask = "n2";
            repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            column.ColumnEdit = repositoryItemTextEdit1;
            //column.DisplayFormat.FormatString = "#####################0.##";
            //column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.Format.FormatString = "#####################0.##";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

        }
        private bool InsertDataPsp(InDataClass idc, string filename)
        {
            bool result = false;
            UCDeviceBase curDevice= GetDevice(idc.SType, idc.Sclass);

            IList<string> filedList = new List<string>();
            IList<string> capList = new List<string>();
            WaitDialogForm wait = new WaitDialogForm("", "正在导入" + idc.Name + "数据, 请稍候...");
            for (int i = 0; i < curDevice.gridView1.Columns.Count; i++)
            {
                capList.Add(curDevice.gridView1.Columns[i].Caption);
                filedList.Add(curDevice.gridView1.Columns[i].FieldName);
            }
            try
            {
                

                DataTable table = DeviceHelper.GetExcel(filename, filedList, capList);
                curDevice.UpdateIn(table);
                wait.Caption="已成功导入" + idc.Name + "数据！";
                wait.Close();
                result = true;
                return result;
               
            }
            catch (Exception)
            {
                wait.Close();
                return result;
                
            }
         

           
        }
        private bool  InsertdataHistory( InDataClass idc,string filename)
        {
            bool result = false;
            string columnname = "";
            DealTreelist(idc);
            DataTable dts = GetExcel(filename);
            IList<Ps_History> lii = new List<Ps_History>();
            DateTime s8 = DateTime.Now;
            int x = 0;
            WaitDialogForm wait = new WaitDialogForm("", "正在导入" + idc.Name + "数据, 请稍候...");
            try
            {
                for (int i = 0; i < dts.Rows.Count; i++)
                {


                    this.Cursor = Cursors.WaitCursor;
                    Ps_History l1 = new Ps_History();
                    foreach (DataColumn dc in dts.Columns)
                    {
                        columnname = dc.ColumnName;
                        if (dts.Rows[i][dc.ColumnName].ToString() == "")
                            continue;

                        switch (l1.GetType().GetProperty(dc.ColumnName).PropertyType.Name)
                        {
                            case "Double":
                                if (dts.Rows[i][dc.ColumnName] == null || dts.Rows[i][dc.ColumnName] == DBNull.Value || dts.Rows[i][dc.ColumnName].ToString() == "")
                                {
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, 0, null);
                                    break;
                                }
                                l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, Convert.ToDouble(dts.Rows[i][dc.ColumnName]), null);
                                break;
                            case "Int32":
                                if (dts.Rows[i][dc.ColumnName] == null || dts.Rows[i][dc.ColumnName] == DBNull.Value || dts.Rows[i][dc.ColumnName].ToString() == "")
                                {
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, 0, null);
                                    break;
                                }
                                l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, Convert.ToInt32(dts.Rows[i][dc.ColumnName]), null);
                                break;

                            default:
                                l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName], null);
                                break;
                        }
                    }

                    l1.Forecast = idc.IType;
                    l1.Col4 = ProjectUID;
                    l1.ForecastID = idc.SType;
                    lii.Add(l1);
                }
                int parenti = -4;
                Ps_History psl1;
                for (int i = 0; i < lii.Count; i++)
                {
                    psl1 = lii[i];
                    psl1.Sort = i;
                    string con = "Col4='" + ProjectUID + "' and Title='" + psl1.Title + "' and Forecast='" + idc.SType + "' and ParentID='" + psl1.ParentID + "'";
                    object obj = Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
                    if (obj != null)
                    {
                        psl1.ID = ((Ps_History)obj).ID;

                        if (psl1.ParentID.Contains("-"))
                            psl1.ParentID = ((Ps_History)obj).ParentID;
                        Services.BaseService.Update<Ps_History>(psl1);
                    }
                    else
                    {
                        psl1.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;


                        Services.BaseService.Create<Ps_History>(psl1);
                    }
                    for (int j = i + 1; j < lii.Count; j++)
                    {
                        if (lii[j].ParentID == parenti.ToString())
                        {
                            lii[j].ParentID = psl1.ID;
                        }
                    }

                    parenti--;
                }
                DealTreelist(idc);
                wait.Close();
                wait = new WaitDialogForm("", "正在重新计算" + idc.Name + "数据, 请稍候...");

                for (int i = 0; i < lii.Count; i++)
                {
                    TreeListNode nd = treeList1.FindNodeByKeyID(lii[i].ID);
                    if (nd != null)
                    {
                        foreach (TreeListColumn tr in treeList1.Columns)
                            if (tr.FieldName.IndexOf("y") >= 0)
                            {
                                CalculateSum(nd, tr);

                            }
                    }

                }
                this.Cursor = Cursors.Default;
                wait.Caption = "已成功导入" + idc.Name + "数据！";
                wait.Close();
                result = true;
                return result;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                wait.Close();
                
                MsgBox.Show(columnname + ex.Message); MsgBox.Show("导入格式不正确！");
                return result;
            }
           

        }
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            Ps_History v = Services.BaseService.GetOneByKey<Ps_History>(node["ID"].ToString());
            TreeNodeToDataObject<Ps_History>(v, node);

            Common.Services.BaseService.Update<Ps_History>(v);


            TreeListNode parentNode = node.ParentNode;

            if (parentNode == null)
            {
                return;
            }

            double sum = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                parentNode.SetValue(column.FieldName, sum);
                v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
                TreeNodeToDataObject<Ps_History>(v, parentNode);

                Common.Services.BaseService.Update<Ps_History>(v);
            }
            else
                parentNode.SetValue(column.FieldName, 0);
            CalculateSum(parentNode, column);
        }
        static public void TreeNodeToDataObject<T>(T dataObject, DevExpress.XtraTreeList.Nodes.TreeListNode treeNode)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.Name.Substring(0, 1) == "y")
                    pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
            }
        }
        private DataTable GetExcel(string filepach)
        {

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
            Hashtable h1 = new Hashtable();
            int aa = 0;

            IList<string> filedList = new List<string>();
            IList<string> capList = new List<string>();
            for (int i = 0; i < treeList1.Columns.Count; i++)
            {

                if (treeList1.Columns[i].VisibleIndex < 0)
                {
                    if (treeList1.Columns[i].FieldName == "ParentID")
                        capList.Add("父ID");
                    else
                    {
                        capList.Add(treeList1.Columns[i].FieldName);
                    }
                }
                else
                {
                    if (treeList1.Columns[i].FieldName != "Title")
                        capList.Add(treeList1.Columns[i].Caption);
                    else
                    {
                        capList.Add("项目");
                    }
                }

                filedList.Add(treeList1.Columns[i].FieldName);
            }


            int c = 0;

            IList<string> fie = new List<string>();
           
          
            int m = 3;
            
            for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
            {
                if (capList.Contains(fpSpread1.Sheets[0].Cells[2, j].Text))

                    fie.Add(filedList[capList.IndexOf(fpSpread1.Sheets[0].Cells[2, j].Text)]);

            }

            for (int k = 0; k < fie.Count; k++)
            {
                dt.Columns.Add(fie[k]);
            }
            for (int i = m; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            {

                DataRow dr = dt.NewRow();

                for (int j = 0, fiej = 0; fiej < fie.Count && j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {

                   
                    if (capList.IndexOf(fpSpread1.Sheets[0].Cells[2, j].Text) < 0)
                    {
                      
                        continue;

                    }
               dr[fie[fiej]] = fpSpread1.Sheets[0].Cells[i, j].Text.Trim();
                    fiej++;

                }
                         dt.Rows.Add(dr);
            }
            fpSpread1.Dispose();
            return dt;
        }
        public class FolderDialog : FolderNameEditor 
        {
            FolderNameEditor.FolderBrowser fDialog = new System.Windows.Forms.Design.FolderNameEditor.FolderBrowser(); 
            public FolderDialog() { } 
            public DialogResult DisplayDialog() 
            { return DisplayDialog("请选择一个文件夹"); }
            public DialogResult DisplayDialog(string description) 
            { fDialog.Description = description; return fDialog.ShowDialog(); }
            public string Path { get { return fDialog.DirectoryPath; } } 
            ~FolderDialog() { fDialog.Dispose(); } 
        }
        private  class InDataClass
        {
            public string Name=string.Empty;
            public string FileName=string.Empty;
            public string YearFlag=string.Empty;
            public string SType=string.Empty;
            public int IType=0;
            public int Flag=0;
            public string Sclass = string.Empty;
            public string Key=string.Empty;
        }

        private void FormInData_Load(object sender, EventArgs e)
        {

            AddDTColumn();
           
        }
        private void AddDTColumn()
        {
            gridView1.GroupPanelText = "导入数据模块与文件信息";
            DT.Columns.Add("num");
            DT.Columns.Add("modelname");
            DT.Columns.Add("filename");
            DT.Columns.Add("success",typeof(bool));

        }
        //选择文件夹
        private void sbtnSelectDir_Click(object sender, EventArgs e)
        {
            string dirpath = string.Empty;
            FolderDialog openFolder = new FolderDialog();
            if (openFolder.DisplayDialog() == DialogResult.OK)
            {
                FileTable.Clear();
                dirpath = openFolder.Path.ToString();
                txtfilename.Text = dirpath;
                FileTable = GetFiles(dirpath, ".xls");
                ShowFiles();
            }
        }
        private void ShowFiles()
        {
            int m = 1;
            DT.Rows.Clear();
            foreach (InDataClass idc in InList)
            {
                DataRow row = DT.NewRow();
                row["num"] = m++;
                row["modelname"] = idc.Name;
                row["filename"] = idc.Key;
              
                DT.Rows.Add(row);
            }
            gridControl1.DataSource = DT;
        }

    }
}