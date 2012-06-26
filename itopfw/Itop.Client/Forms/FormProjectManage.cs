using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Projects;
using Itop.Domain.RightManager;
using System.Reflection;
using Itop.Client.Forms;
using System.Diagnostics;
using Itop.Common;
using System.Collections;
using System.Globalization;

namespace Itop.Client
{
    public partial class FormProjectManage : FormBase
    {
        private DataTable dt = new DataTable();
        public FormProjectManage()
        {
            InitializeComponent();
        }
        private void FormProjectManage_Load(object sender, EventArgs e)
        {
            InitRightControl();
        }
        private void barAddDir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeAdd();
        }

        private void barAddProj_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeAdd1();
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeEdit();
        }

        private void barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeDel();
        }

        private void barCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeCopy();
        }

        private void barUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrevMoveNode();
        }

        private void barDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NextMoveNode();
        }

        private void barUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProjUser();
        }


        //添加目录
        private void TreeAdd()
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
        //添加卷
        private void TreeAdd1()
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }

            if (tln.ParentNode != null)
            {
                MessageBox.Show("请先选择目录！");
                return;
            }


            string uid = tln["UID"].ToString();

            Project obj = new Project();
            obj.UID = Guid.NewGuid().ToString();
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
            using (FrmProjectDialog dlg = new FrmProjectDialog(""))
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                dt.Rows.Add(Itop.Common.DataConverter.ObjectToRow(obj, dt.NewRow()));
            }
            //设置授权模块的权限
            VsmdgroupProg tempvp = new VsmdgroupProg();

            tempvp.Progid = "b9b2acb7-e093-4721-a92f-749c731b016e";
            tempvp.Groupno = "SystemManage";

            IList<Smugroup> listUsergroup = Services.BaseService.GetList<Smugroup>("SelectSmugroupByWhere", "Userid='" + MIS.UserNumber + "'");
            if (listUsergroup.Count > 0)
            {
                tempvp.Groupno = listUsergroup[0].Groupno;
            }

            tempvp.ProjectUID = obj.UID;
            tempvp.run = "1";
            try
            {
                Services.BaseService.Create("InsertVsmdgroupProgwithvalue", tempvp);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }
        //编辑卷
        private void TreeEdit()
        {

            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }

            string uid = tln["UID"].ToString();

            Project pj = Services.BaseService.GetOneByKey<Project>(uid);
            //修改卷或项目
            if (tln.ParentNode == null)
            {
                //执行修改操作 项目
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
            else
            {
                //执行修改操作 卷
                using (FrmProjectDialog dlg = new FrmProjectDialog(""))
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



        }
        //删除卷
        private void TreeDel()
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }
            if (!IsSystemUser())
            {
                MsgBox.Show("您无权删除目录！");
                return;
            }
            if (tln.Nodes.Count > 0)
            {
                MsgBox.Show("该目录不为空，不能删除，请先删除该目录下的项目！");
                return;
            }
            string uid = tln["UID"].ToString();
            if (MsgBox.ShowYesNo("是否删除？") != DialogResult.Yes)
            {
                return;
            }




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
        //恢复项目卷
        private void TreeGD()
        {
            FrmProjectGD fp = new FrmProjectGD();
            if (fp.ShowDialog() == DialogResult.OK)
            {
                InitRightControl();

            }
        }
        //关联图层
        private void TreeGL()
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
        //拷贝卷宗
        private void TreeCopy()
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
                    //拷贝后设置授权模块的权限
                    VsmdgroupProg tempvp = new VsmdgroupProg();
                    tempvp.Progid = "b9b2acb7-e093-4721-a92f-749c731b016e";
                    tempvp.Groupno = "SystemManage";
                    tempvp.ProjectUID = id;
                    tempvp.run = "1";
                    try
                    {
                        Services.BaseService.Create("InsertVsmdgroupProgwithvalue", tempvp);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);

                    }
                }

            }
            catch (Exception e1) { MsgBox.Show("错误提示:" + e1.Message); }
        }
        //项目用户
        private void ProjUser()
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
            {
                return;
            }
            if (tln.ParentNode == null)
            {
                return;
            }

            string uid = tln["UID"].ToString();
            string userid = tln["GuiDangName"].ToString();
            frmProjUser frm = new frmProjUser(uid, userid);
            frm.ShowDialog();
        }

        //上移
        private void PrevMoveNode()
        {

            if (treeList1.FocusedNode == null)
            {
                return;
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.PrevNode != null)
            {
                string struid = treeList1.FocusedNode.PrevNode["UID"].ToString();
                string struid2 = treeList1.FocusedNode["UID"].ToString();
                if (struid != struid2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.PrevNode["SortID"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["SortID"]);
                    if (sortj2 == sortj)
                    {
                        sortj = sortj - 1;
                    }
                    
                    Project pj = Services.BaseService.GetOneByKey<Project>(struid);
                    pj.SortID = sortj2;


                    i = dt.Rows.IndexOf(dt.Select("UID='" + pj.UID + "'")[0]);
                    dt.Rows[i]["SortID"] = pj.SortID;
                    Services.BaseService.Update<Project>(pj);
                    pj = Services.BaseService.GetOneByKey<Project>(struid2);
                    pj.SortID = sortj;


                    i = dt.Rows.IndexOf(dt.Select("UID='" + pj.UID + "'")[0]);
                    dt.Rows[i]["SortID"] = pj.SortID;
                    Services.BaseService.Update<Project>(pj);
                    treeList1.Refresh();

                }

            }


        }
        //下移
        private void NextMoveNode()
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;
            if (treeList1.FocusedNode.NextNode != null)
            {
                if (treeList1.FocusedNode["UID"].ToString() != treeList1.FocusedNode.NextNode["UID"].ToString())
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.NextNode["SortID"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["SortID"]);
                    if (sortj2 == sortj)
                    {
                        sortj = sortj + 1;
                    }
                    Project pj = Services.BaseService.GetOneByKey<Project>(treeList1.FocusedNode.NextNode["UID"].ToString());
                    pj.SortID = sortj2;


                    i = dt.Rows.IndexOf(dt.Select("UID='" + pj.UID + "'")[0]);
                    dt.Rows[i]["SortID"] = pj.SortID;
                    Services.BaseService.Update<Project>(pj);
                    pj = Services.BaseService.GetOneByKey<Project>(treeList1.FocusedNode["UID"].ToString());
                    pj.SortID = sortj;


                    i = dt.Rows.IndexOf(dt.Select("UID='" + pj.UID + "'")[0]);
                    dt.Rows[i]["SortID"] = pj.SortID;
                    Services.BaseService.Update<Project>(pj);
                    treeList1.Refresh();

                }

            }

        }


        //判断当前用户是否是管理员
        private bool IsSystemUser()
        {
            bool isadmin = false;
            //系统管理员
            string strsql = "Groupno='SystemManage' and Userid='" + MIS.UserNumber + "'";
            IList list = Services.BaseService.GetList("SelectSmugroupByWhere", strsql);
            if (list.Count > 0)
            {
                isadmin = true;
            }
            return isadmin;
        }
        private void InitRightControl()
        {
            string s = "  IsGuiDang!='是' order by SortID";
            IList<Project> list = Services.BaseService.GetList<Project>("SelectProjectByWhere", s);
            dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Project));
            //过滤项目用户
            //非系统管理员

            if (!IsSystemUser())
            {
                IList<ProjectUser> listuser = Services.BaseService.GetList<ProjectUser>("SelectProjectUserbyWhere", " UserID='" + MIS.UserNumber + "'");
                Hashtable hasproj = new Hashtable();
                for (int i = 0; i < listuser.Count; i++)
                {
                    hasproj.Add(listuser[i].UID, listuser[i].UserID);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ProjectManager"].ToString() != "" && dt.Rows[i]["GuiDangName"].ToString() != MIS.UserNumber && !hasproj.ContainsKey(dt.Rows[i]["UID"].ToString()))
                    {
                        dt.Rows.Remove(dt.Rows[i]);
                        i--;
                    }
                }
            }

            this.treeList1.DataSource = dt;
        }

        private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            if (e.Node.ParentNode == null)
            {
                e.Node.StateImageIndex = 1;

            }
            else
            {
                e.Node.StateImageIndex = 0;
            }
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            ChangeButton(IsSystemUser());
            if (treeList1.FocusedNode.ParentNode == null)
            {
                return;
            }
            if (treeList1.FocusedNode.Nodes.Count == 0 && treeList1.FocusedNode.ParentNode == null)
            {
                return;
            }
          
        }
        //改变按钮状态
        private void ChangeButton(bool istrue)
        {

            bool cnnrun = false;
            //选择为目录
            if (treeList1.FocusedNode.ParentNode == null)
            {
                // 管理员

                SetBarButtonEnabled(barAddDir, true);
                SetBarButtonEnabled(barAddProj, true);
                SetBarButtonEnabled(barEdit, true);
                SetBarButtonEnabled(barDel, istrue);
                //SetBarButtonEnabled(bbtngl, false);
                //SetBarButtonEnabled(bbtnRecor, false);
                SetBarButtonEnabled(barCopy, false);
                SetBarButtonEnabled(barUser, false);

              

            }
            //选择为项目
            else
            {
                // 管理员
                if (istrue)
                {
                    SetBarButtonEnabled(barAddDir, true);
                    SetBarButtonEnabled(barAddProj, true);
                    SetBarButtonEnabled(barEdit, true);
                    SetBarButtonEnabled(barDel, true);
                    //SetBarButtonEnabled(bbtngl, true);
                    //SetBarButtonEnabled(bbtnRecor, true);
                    SetBarButtonEnabled(barCopy, true);
                    SetBarButtonEnabled(barUser, true);

                  
                }
                else
                {
                    //项目创建人，则用户管理部分不可用
                    if (treeList1.FocusedNode["GuiDangName"].ToString() == MIS.UserNumber)
                    {
                        SetBarButtonEnabled(barAddDir, true);
                        SetBarButtonEnabled(barAddProj, false);
                        SetBarButtonEnabled(barEdit, true);
                        SetBarButtonEnabled(barDel, true);
                        //SetBarButtonEnabled(bbtngl, false);
                        //SetBarButtonEnabled(bbtnRecor, false);
                        SetBarButtonEnabled(barCopy, true);
                        SetBarButtonEnabled(barUser, true);

                      
                    }
                    else
                    {

                        SetBarButtonEnabled(barAddDir, true);
                        SetBarButtonEnabled(barAddProj, false);
                        SetBarButtonEnabled(barEdit, false);
                        SetBarButtonEnabled(barDel, false);
                        //SetBarButtonEnabled(bbtngl, false);
                        //SetBarButtonEnabled(bbtnRecor, false);
                        SetBarButtonEnabled(barCopy, false);
                        SetBarButtonEnabled(barUser, false);

                       
                    }


                }


            }

        }
        private void SetBarButtonEnabled(DevExpress.XtraBars.BarButtonItem bar, bool can)
        {
            if (can)
            {
                bar.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                bar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            bar.Enabled = can;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void FormProjectManage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Itop.Client.MIS.MFrmConsole.InitProject();
        }
      
    }
}
