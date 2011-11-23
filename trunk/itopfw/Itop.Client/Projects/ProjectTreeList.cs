using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.RightManager;
using System.Collections;
using DevExpress.XtraTreeList.Nodes;
using Itop.Common;
using System.Diagnostics;
using Itop.Client.Forms;
using DevExpress.XtraTreeList;
using System.Reflection;
using System.Globalization;
using Itop.Client.Base;

namespace Itop.Client.Projects
{
    public partial class ProjectTreeList : FormBase
    {
        DataTable dt = new DataTable();
        public Project PJ
        {
            get
            {
                Project pj = null;
                TreeListNode tln = treeList1.FocusedNode;
                if (tln != null)
                {
                    pj = new Project();
                    pj.UID = tln["UID"].ToString();
                    pj.ProjectName = tln["ProjectName"].ToString();
                }

                return pj;
            }

        }


        public Project PJ1
        {
            get
            {
                Project pj1 = null;
                TreeListNode tln = treeList1.FocusedNode.ParentNode;
                if (tln != null)
                {
                    pj1 = new Project();
                    pj1.UID = tln["UID"].ToString();
                    pj1.ProjectName = tln["ProjectName"].ToString();
                }

                return pj1;
            }

        }


        public ProjectTreeList()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Project obj = new Project();
            obj.UID = Guid.NewGuid().ToString();
            obj.CreateDate = DateTime.Now;
            obj.StartDate = DateTime.Now.Date;
            obj.PlanCompleteDate = DateTime.Now;
            obj.CompleteDate = DateTime.Now;
            obj.QualityDate = DateTime.Now;
            obj.BecomeEffective = DateTime.Now;
            obj.GuiDangTime = DateTime.Now;
            obj.GuiDangName = Itop.Client.MIS.UserNumber;

            //执行添加操作
            using (FrmProjectDialog dlg = new FrmProjectDialog())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(obj, dt.NewRow()));
            }

            //将新对象加入到链表中

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }

            string uid = tln["UID"].ToString();

            Project pj = Services.BaseService.GetOneByKey<Project>(uid);

            //执行修改操作
            using (FrmProjectDialog dlg = new FrmProjectDialog())
            {
                dlg.Object = pj;   //绑定副本
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                tln.SetValue("ProjectName", dlg.Object.ProjectName);
                tln.SetValue("ProjectCode", dlg.Object.ProjectCode);
            }


        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }

            string uid = tln["UID"].ToString();
            if (MsgBox.ShowYesNo("是否删除？") != DialogResult.Yes)
            {
                return;
            }

            //Project pj = Services.BaseService.GetOneByKey<Project>(uid);


            //执行删除操作
            try
            {
                if (tln.Nodes.Count > 0)
                {
                    foreach (TreeListNode tln1 in tln.Nodes)
                    {
                        string uid1 = tln1["UID"].ToString();
                        Services.BaseService.Update("UpdateProjectByGuiDangName", uid1);
                    }
                }


                Services.BaseService.Update("UpdateProjectByGuiDangName", uid);
                treeList1.Nodes.Remove(tln);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                return;
            }

        }

        private void ProjectList_Load(object sender, EventArgs e)
        {

            LoadData();

            if (MIS.UserNumber.ToLower() != "admin")
            {
                simpleButton7.Visible = false;
                simpleButton8.Visible = false;
            }
        }

        private void LoadData()
        {
            //string s = " GuiDangName in ('" + Itop.Client.MIS.UserNumber + "','') and IsGuiDang!='是' order by CreateDate";
            string s = "  IsGuiDang!='是' order by CreateDate";
            IList<Project> list = Services.BaseService.GetList<Project>("SelectProjectByWhere", s);
            dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Project));
            this.treeList1.DataSource = dt;

        }


        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
                return;
            if (tln.ParentNode == null)
                return;
            MIS.ProgUID = tln["UID"].ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }

            if (tln.ParentNode != null)
                return;

            string uid = tln["UID"].ToString();

            Project obj = new Project();
            obj.UID = Guid.NewGuid().ToString().Substring(24);
            obj.CreateDate = DateTime.Now;
            obj.StartDate = DateTime.Now.Date;
            obj.PlanCompleteDate = DateTime.Now;
            obj.CompleteDate = DateTime.Now;
            obj.QualityDate = DateTime.Now;
            obj.BecomeEffective = DateTime.Now;
            obj.GuiDangTime = DateTime.Now;
            obj.ProjectManager = uid;
            obj.GuiDangName = Itop.Client.MIS.UserNumber;

            //执行添加操作
            using (FrmProjectDialog dlg = new FrmProjectDialog())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(obj, dt.NewRow()));
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            FrmProjectGD fp = new FrmProjectGD();
            if (fp.ShowDialog() == DialogResult.OK)
            {
                LoadData();

            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode != null)
            {
                if (treeList1.FocusedNode.ParentNode == null)
                {
                    MessageBox.Show("请选择卷宗。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmProgLayerManager p = new frmProgLayerManager();
                p.progid = treeList1.FocusedNode["UID"].ToString();
                p.Show();
            }
        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
            if (hInfo.Node == null)
                return;

            TreeListNode tln = hInfo.Node;
            TreeListNode tln1 = tln.ParentNode;
            if (tln1 == null)
                return;

            MIS.ProgUID = tln["UID"].ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            try
            {
                TreeListNode tln = treeList1.FocusedNode;
                if (tln == null)
                    return;
                TreeListNode tln1 = tln.ParentNode;
                if (tln1 == null)
                    return;
                object result = null;
                object classInstance = null;


                string id = Guid.NewGuid().ToString();
                Assembly asm = Assembly.LoadFrom(Application.StartupPath + "\\Itop.Client.DataCopy.dll");
                Type type = asm.GetType("Itop.Client.DataCopy.ModuleDataCopy", true);


                Type[] ptypes = new Type[2];
                ptypes[0] = typeof(string);
                ptypes[1] = typeof(string);

                object[] paramValues = new object[2];
                paramValues.SetValue(tln["UID"].ToString(), 0);
                paramValues.SetValue(id, 1);


                MethodInfo method = type.GetMethod("CopyData", ptypes);
                if (method != null)
                {
                    ParameterInfo[] paramInfos = method.GetParameters();
                    if (paramInfos.Length == paramValues.Length)
                    {
                        // 参数个数相同才会执行
                        object[] methodParams = new object[paramValues.Length];

                        for (int i = 0; i < paramValues.Length; i++)
                            methodParams[i] = Convert.ChangeType(paramValues[i], paramInfos[i].ParameterType, CultureInfo.InvariantCulture);
                        if (classInstance == null)
                        {
                            classInstance = (method.IsStatic) ? null : Activator.CreateInstance(type);
                        }
                        result = method.Invoke(classInstance, methodParams);
                    }

                }
                if (result.ToString() == "True")
                {
                    string uid = tln1["UID"].ToString();
                    Project obj = new Project();
                    obj.UID = id;
                    obj.ProjectName = tln["ProjectName"].ToString() + "副本";
                    obj.CreateDate = DateTime.Now;
                    obj.StartDate = DateTime.Now.Date;
                    obj.PlanCompleteDate = DateTime.Now;
                    obj.CompleteDate = DateTime.Now;
                    obj.QualityDate = DateTime.Now;
                    obj.BecomeEffective = DateTime.Now;
                    obj.GuiDangTime = DateTime.Now;
                    obj.ProjectManager = uid;
                    obj.GuiDangName = Itop.Client.MIS.UserNumber;
                    Services.BaseService.Create<Project>(obj);

                    dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(obj, dt.NewRow()));
                }

            }
            catch (Exception e1) { MsgBox.Show("错误提示:"+e1.Message); }
        }
    }

}