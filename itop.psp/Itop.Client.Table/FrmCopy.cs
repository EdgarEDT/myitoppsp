using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Domain.RightManager;
using System.Reflection;
using Itop.Domain.Table;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmCopy : FormBase
    {
        public FrmCopy()
        {
            InitializeComponent();
        }

        private string curId;
        private string assName="Itop.Domain.Table";
        private string className;
        public string idName="ID";
        public string parentName = "ParentID";
        public string projectName = "ProjectID";
        private string selectString;
        private string insertString;
        private bool existChild = false;
        private IList<string> childClassName=new List<string>();
        private IList<string> childInsertString = new List<string>();
        private IList<string> childSelectString = new List<string>();
        public string CurID
        {
            set { curId = value; }
        }
        public string AssName
        {
            set { assName = value; }
        }
        public string ClassName
        {
            set { className = value; }
        }
        public string SelectString
        {
            set { selectString = value; }
        }
        public string InsertString
        {
            set { insertString = value; }
        }
        public bool ExistChild
        {
            set { existChild = value; }
        }
        public IList<string> ChildClassName
        {
            set { childClassName = value; }
            get { return childClassName; }
        }
        public IList<string> ChildInsertName
        {
            set { childInsertString = value; }
            get { return childInsertString; }
        }
        public IList<string> ChildSelectString
        {
            set { childSelectString = value; }
            get { return childSelectString; }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string uid=treeList1.FocusedNode.GetValue("UID").ToString();
            if ( uid== curId)
            {
                MessageBox.Show("不能复制到同一个项目");
                return;
            }
            CopyData(uid, curId, className,insertString,selectString);
            if (childClassName.Count != childInsertString.Count)
            {
                MessageBox.Show("由于参数不对，子表没有复制成功");
                return;
            }
            for(int i=0;i<childClassName.Count;i++)// str in childClassName)
            {
                try
                {
                    CopyData(uid, curId, childClassName[i], childInsertString[i], childSelectString[i]);
                }
                catch { }
            }
            DialogResult = DialogResult.OK;
        }

        private void FrmCopy_Load(object sender, EventArgs e)
        {
            LoadData();
         //   CopyData("4c8aa9a3-51d5-41c6-904e-e1f5daaa3e86", "2", "Ps_Table_ElecPH");
        }
        public bool bPare = true;
        public void CopyData(string oldId,string newId,string className,string istr,string sstr)
        {
            try
            {
                Assembly assem = Assembly.LoadFrom(Application.StartupPath + "\\"+assName+".dll");
                Type type;
                if (bPare)
                    type = assem.GetType(assName + "." + className, true);
                else
                    type = assem.GetType(className, true);
                
                string conn = projectName + " = '" + oldId + "'";
                IList oldList = Common.Services.BaseService.GetList(sstr, conn);
                for (int i = 0; i < oldList.Count; i++)
                {
                    try
                    {
                        object newObj = Activator.CreateInstance(type);
                        newObj = oldList[i];
                        //string str = oldList[i].GetType().GetProperty("Title").GetValue(oldList[i], null).ToString();
                        IList<string> strList = GetNewID(type.GetProperty(idName).GetValue(newObj, null).ToString(),
                            bPare ? type.GetProperty(parentName).GetValue(newObj, null).ToString() : "", oldId, newId);
                        type.GetProperty(idName).SetValue(newObj, strList[0], null);
                        if (bPare)
                            type.GetProperty(parentName).SetValue(newObj, strList[1], null);
                        type.GetProperty(projectName).SetValue(newObj, newId, null);
                        Common.Services.BaseService.Create(istr, newObj);
                    }
                    catch { }
                }
            }
            catch (Exception e) { }
        }

        public IList<string> GetNewID(string oldid, string oldparentid, string oldprojectid, string newprojectid)
        {
            IList<string> list=new List<string>();
            string newid = "", newparentid = oldparentid;
            if (oldid.IndexOf(oldprojectid) != -1)
            {
                newid=oldid.Replace(oldprojectid, newprojectid);
            }
            if (oldparentid.IndexOf(oldprojectid) != -1)
            {
                newparentid=oldparentid.Replace(oldprojectid, newprojectid);
            }
            list.Add(newid); list.Add(newparentid);
            return list;
        }

        public void LoadData()
        {
            string conn = "IsGuiDang != '是'";
            IList list = Common.Services.BaseService.GetList("SelectProjectByWhere", conn);
            DataTable dataTable = Itop.Common.DataConverter.ToDataTable(list, typeof(Project));

            treeList1.DataSource = dataTable;
            for (int i = 0; i < treeList1.Columns.Count; i++)
            {
                treeList1.Columns[i].VisibleIndex = -1;
            }

            treeList1.Columns["ProjectName"].Caption = "项目名称";
            treeList1.Columns["ProjectName"].Width = 250;
            treeList1.Columns["ProjectName"].VisibleIndex = 1;
            treeList1.Columns["ProjectName"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["ProjectName"].OptionsColumn.AllowSort = false;
            treeList1.Columns["ProjectName"].VisibleIndex = 0;
            treeList1.Columns["SortID"].SortOrder = SortOrder.Ascending;

        }
    }
}