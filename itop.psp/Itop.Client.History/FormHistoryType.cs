using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Forecast;
using Itop.Common;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System.Drawing.Drawing2D;
namespace Itop.Client.History
{
    public partial class FormHistoryType : FormBase
    {
        private TreeListNode CurrentNode = null;
        private string OldId = string.Empty;
        private string _flag="";
        private string _title;
        private string _flagvalue;
        string   ProjectID = MIS.ProgUID;
        public string AreaId =string.Empty;
        public string AreaName = string.Empty;
        //用于判断子结点的值是否改变完成
        //用于第一次从列表取值判断
        private bool CheckEnd = false;
        private IList<Ps_HistoryType> P_HTypelist = null;
        //存放其它模块传来的ID值，用于对比类别是否已呢
        public List<string> ValueList = new List<string>();
        //单位列表
        public List<string> UnitsList = new List<string>();

        //public List<string> TypeList = new List<string>();
        //已选中要添加的类别名称
        public List<string> AddList = new List<string>();
        //已选中要添加的类别ID
        public List<string> AddListID = new List<string>();
        //要减少的类别名称
        public List<string> ReduceList = new List<string>();
        //已选中要添加的类别ID
        public List<string> ReduceListID = new List<string>();
        //改变单位的类别名称
        public List<string> changeUnitlist = new List<string>();
        //改变单位的类别ID
        public List<string> changeUnitlistID = new List<string>();
        DataTable dataTable = new DataTable();
        /// <summary>
        /// flag="" 表示管理员使用
        /// flag="1" 表示供电实绩使用
        /// flag="2" 表示分区供电实绩使用
        /// flag="3" 表示分区用电情况使用
        /// </summary>
        public string Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        public string Title
        {
            get
            {
                if (Flag=="1")
                {
                    _title = "电力发展实绩默认类别管理";
                }
                else if (Flag=="2")
                {
                    _title = "分区县供电实绩默认类别管理";
                }
                else if (Flag=="3")
                {
                    _title = "分区 "+AreaName+" 用电情况默认类别管理";
                }
                else
                {
                    _title = "类别管理";
                }
                return _title;
            }
            set { _title = value; }
        }
        /// <summary>
        /// 1为供电实绩
        /// 2为分区供电实绩
        /// 3为分区用电情况
        /// </summary>
        public string FlagValue
        {
            get { return _flagvalue; }
            set { _flagvalue = value; }
        }
        private string[] str ={ "电力发展实绩", "分区供电实绩","分区用电情况" };
        public FormHistoryType()
        {
          
            InitializeComponent();
            treeList1.Columns["nowunits"].VisibleIndex = -1;
          
        }
        public FormHistoryType(string flag)
        {
            InitializeComponent();
            Flag = flag;
            FlagValue = flag;
            treeList1.Columns["Units"].VisibleIndex = -1;
           
        }
        //添加模块管理下拉列表
        private void BarEditItem1_Add(string[] str)
        {
            if (Flag!="")
            {
                return;
            }
            for (int i = 0; i < str.Length; i++)
            {
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)combtype.Edit).Items.Add(str[i].ToString());
            }
            combtype.EditValue = str[0].ToString();
        }
        private void FormHistoryType_Load(object sender, EventArgs e)
        {
            //分区县用电情况，用区县ID代替项目ID，这样有多个区县时类别ID不会重复
            if (Flag == "3")
            {
                ProjectID = AreaId;
            }
            this.Text = Title;
            BarEditItem1_Add(str);
            ButtonShoworHide();
            ButtonShow();
            LoadData();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        private void ButtonShow()
        {
            if (!AddRight)
            {
                barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!EditRight)
            {
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!DeleteRight)
            {
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }
        //根据使用情况确定显示哪些按钮
        private void ButtonShoworHide()
        {
            if (Flag=="")
            {
                treeList1.StateImageList = null;
                barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            
                combtype.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

        }
        //载入数据
        private void LoadData()
        {
            
            string connstr = "  Flag='" + FlagValue + "'  Order by Sort";
            P_HTypelist = Common.Services.BaseService.GetList<Ps_HistoryType>("SelectPs_HistoryTypeBYconn", connstr);
            dataTable = Itop.Common.DataConverter.ToDataTable((IList)P_HTypelist, typeof(Ps_HistoryType));
           //不是管理员对类别进行管理(即供电实绩或分区供电用电情况调用时)
         
            if (Flag!="")
            {
                CheckState check = CheckState.Unchecked;
                //添加单位（为判断单位变化，添加两个单位）
                dataTable.Columns.Add("nowunits", typeof(string));
                dataTable.Columns.Add("oldunits", typeof(string));
                //添加选中，为判断变化情况，添加两个选中
                dataTable.Columns.Add("check", typeof(CheckState));
                dataTable.Columns.Add("oldcheck", typeof(CheckState));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //两个选中保持相同值
                    dataTable.Rows[i]["check"] = ISchecked(ValueList, dataTable.Rows[i]["ID"].ToString(), dataTable.Rows[i]["TypeName"].ToString());
                    dataTable.Rows[i]["oldcheck"] = ISchecked(ValueList, dataTable.Rows[i]["ID"].ToString(), dataTable.Rows[i]["TypeName"].ToString());
                    if ((CheckState)dataTable.Rows[i]["check"] != CheckState.Unchecked)
                    {
                        //如果是选中的，则添加单位且两个单位保持同值
                        dataTable.Rows[i]["nowunits"] = FindUnits(dataTable.Rows[i]["ID"].ToString());
                        dataTable.Rows[i]["oldunits"] = FindUnits(dataTable.Rows[i]["ID"].ToString());
                    }
                }
            }
           
            treeList1.DataSource = dataTable;
            if (OldId!=string.Empty)
            {
                FindCurrentNodeById(treeList1.Nodes, OldId);
            }
           
        }
        //根据ID 找到单位,规则应是 类别名+"("+单位+")"并返回单位
        private string FindUnits(string ID)
        {
            Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID + "|" + ProjectID);
            string tempstr = String.Empty;
            if (ph!=null)
            {
                int starts = ph.Title.IndexOf("(");
                if (starts>0)
                {
                    return ph.Title.Substring(starts + 1, ph.Title.Length - starts - 2);
                }
                else
                {
                    return tempstr;
                }
            }
            else
            {
                return tempstr;
            }
        }
        
        /// <summary>
        /// 根据ID和类名反回是否选中状态
        /// </summary>
        /// <param name="str">已有ID列表</param>
        /// <param name="ID">要判断的ID</param>
        /// <param name="TypeName">要判断的类别名称</param>
        /// <returns></returns>
        private CheckState ISchecked(List<string> str, string ID,string TypeName)
        {
            //叶子结点
            if ((int)Common.Services.BaseService.GetObject("SelectPs_HistoryTypeCountBycon", " ParentID='" + ID + "' and Flag='" + FlagValue + "'") == 0)
            {
                bool checkflag = false;
                for (int j = 0; j < str.Count; j++)
                {
                    if (ID + "|" + ProjectID == str[j].ToString())
                    {
                        checkflag = true;
                        break;
                    }
                }
                if (checkflag)
                {
                    return CheckState.Checked;
                }
                else
                {
                    return CheckState.Unchecked;
                }
            }
            else
            {
                //不是第一类结点
                if ( Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(ID).ParentID!="")
                {
                    if (!HaveIN(str, ID))
                    {
                        return CheckState.Unchecked;
                    }
                    else
                    {
                        if (CheckChild(str, ID))
                        {
                            return CheckState.Checked;
                        }
                        else
                        {
                            return CheckState.Indeterminate;
                        }
                    }
                }
                //第一类结点
                else
                {
                    if (CheckChild(str, ID))
                    {
                        return CheckState.Checked;
                    }
                    else
                    {
                        if (CheckChildFalse(str, ID))
                        {
                            return CheckState.Unchecked;
                        }
                        else
                        {
                            return CheckState.Indeterminate;
                        }
                       
                    }
                }


              
            }
           
        }
        //判断孩子是否都被选中，都选中返回true,否则返回false
        private bool CheckChild(List<string> str, string ID)
        {
            IList<Ps_HistoryType> phtlist = Common.Services.BaseService.GetList<Ps_HistoryType>("SelectPs_HistoryTypeBYconn", "  Flag='" + FlagValue + "'  and ParentID='" + ID + "'");
            bool ChecK=true;
            for (int i = 0; i < phtlist.Count; i++)
            {
                ChecK = ChecK && HaveIN(str, phtlist[i].ID);
            }
            if (ChecK)
            {
                for (int i = 0; i < phtlist.Count; i++)
                {
                    ChecK = ChecK && CheckChild(str,phtlist[i].ID);
                }
            }
            return ChecK;
           
        }
        //判断孩子是否都未被选中，都未选中返回true,否则返回false
        private bool CheckChildFalse(List<string> str, string ID)
        {
            IList<Ps_HistoryType> phtlist = Common.Services.BaseService.GetList<Ps_HistoryType>("SelectPs_HistoryTypeBYconn", "  Flag='" + FlagValue + "'  and ParentID='" + ID + "'");
            bool ChecK = true;
            for (int i = 0; i < phtlist.Count; i++)
            {
                ChecK = ChecK && !HaveIN(str, phtlist[i].ID);
            }
            if (ChecK)
            {
                for (int i = 0; i < phtlist.Count; i++)
                {
                    ChecK = ChecK && CheckChildFalse(str, phtlist[i].ID);
                }
            }
            return ChecK;

        }
        //判断列表中是否包含ID，包含为true,否则为false
        private bool HaveIN(List<string> str, string ID)
        {
            bool have = false;
            for (int i = 0; i < str.Count; i++)
            {
                if (str[i].ToString() == ID.ToString() + "|" + ProjectID)
	            {
                    have = true;
                    break;
	            }
            }
            return have;

        }
        //根据ID找到树结点并设为当前结点
        private void FindCurrentNodeById(TreeListNodes nodes, string ID)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node["ID"].ToString() == ID)
                {
                    treeList1.FocusedNode = node;
                    break;
                }
                else
                {
                    if (node.Nodes.Count != 0)
                    {
                        FindCurrentNodeById(node.Nodes, ID);
                    }
                }
            }
        }
        //模块列表发生改变时修改对应的值
        private void combtype_EditValueChanged(object sender, EventArgs e)
        {
            if (combtype.EditValue == str[0].ToString())
            {
                FlagValue = "1";

            }
            else if (combtype.EditValue == str[1].ToString())
            {
                FlagValue = "2";
            }
            else if (combtype.EditValue == str[2].ToString())
            {
                FlagValue = "3";
            }
            else
            {
                FlagValue = "";
            }
            LoadData();
        }
        //鼠标左键时事件
        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
                if (hInfo.HitInfoType == HitInfoType.StateImage)
                {
                    SetCheckedNode(hInfo.Node);
                    CheckEnd = true;
                }
            }
        }
        //当选中项目时改变结点的状态同时变父结点和子结点的状态
        private void SetCheckedNode(TreeListNode node)
        {
            CheckState check = (CheckState)node.GetValue("check");
            if (check == CheckState.Indeterminate || check == CheckState.Unchecked) check = CheckState.Checked;
            else check = CheckState.Unchecked;
            treeList1.BeginUpdate();
            node["check"] = check;
            //StatusBarDisplayText(treeList1.FocusedNode);
            SetCheckedChildNodes(node, check);
            SetCheckedParentNodes(node, check);
            treeList1.Refresh();
            treeList1.EndUpdate();
        }
        //改变子结点状态
        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                ChangeUnits(node.Nodes[i]);
                node.Nodes[i]["check"] = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }
        //改变父结点状态
        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    ChangeUnits(node.ParentNode.Nodes[i]);
                    if (!CheckState.Checked.Equals((CheckState)node.ParentNode.Nodes[i]["check"]))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode["check"] = b ?  CheckState.Indeterminate:check ;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }
        //根据check列的值来改变图标的值
        private void treeList1_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            if (Flag=="")
            {
                return;
            }
            CheckState check = (CheckState)e.Node.GetValue("check");
            if (check == CheckState.Unchecked)
                e.NodeImageIndex = 0;
            else if (check == CheckState.Checked)
                e.NodeImageIndex = 1;
            else e.NodeImageIndex = 2;
           
        }
        //下移一位
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.NextNode != null)
            {
                string ID1 = treeList1.FocusedNode.NextNode["ID"].ToString();
                string ID2 = treeList1.FocusedNode["ID"].ToString();
                if (ID1 != ID2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.NextNode["Sort"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["Sort"]);
                    if (sortj2 == sortj)
                    {
                        sortj2 = sortj2 + 1;
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }
                    else
                    {
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }

                    Ps_HistoryType pj = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(ID1);
                    pj.Sort = sortj;
                    Ps_HistoryType pj2 = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(ID2);
                    pj2.Sort = sortj2;

                    Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1);
                    Ps_History ph2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2);
                    if (ph != null)
                    {
                        ph.Sort = pj.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph);
                    }
                    if (ph2 != null)
                    {
                        ph2.Sort = pj2.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph2);
                    }


                    Common.Services.BaseService.Update<Ps_HistoryType>(pj);
                    Common.Services.BaseService.Update<Ps_HistoryType>(pj2);
                    treeList1.FocusedNode.NextNode["Sort"] = sortj;
                    treeList1.FocusedNode["Sort"] = sortj2;
                    treeList1.BeginSort();
                    treeList1.EndSort();
                }

            }
        }
        //上移一位
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.PrevNode != null)
            {
                string ID1= treeList1.FocusedNode.PrevNode["ID"].ToString();
                string ID2 = treeList1.FocusedNode["ID"].ToString();
                if (ID1 != ID2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.PrevNode["Sort"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["Sort"]);
                    if (sortj2 == sortj)
                    {
                        sortj2 = sortj2 - 1;
                       
                    }
                    else
                    {
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }
                    Ps_HistoryType pj = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(ID1);
                    pj.Sort = sortj;
                    Ps_HistoryType pj2 = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(ID2);
                    pj2.Sort = sortj2;

                    Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1);
                    Ps_History ph2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2);
                    if (ph!=null)
                    {
                        ph.Sort = pj.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph);
                    }
                    if (ph2 != null)
                    {
                        ph2.Sort = pj2.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph2);
                    }

                    Common.Services.BaseService.Update<Ps_HistoryType>(pj);
                    Common.Services.BaseService.Update<Ps_HistoryType>(pj2);
                    treeList1.FocusedNode.PrevNode["Sort"] = sortj;
                    treeList1.FocusedNode["Sort"] = sortj2;
                    treeList1.BeginSort();
                    treeList1.EndSort();

                }

            }
            
        }
        //添加一级类别
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormHistoryTypeEdit frm = new FormHistoryTypeEdit();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_HistoryType pf = new Ps_HistoryType();
                pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                pf.Flag = FlagValue;
                pf.ParentID = "";
                pf.Sort = (int)Common.Services.BaseService.GetObject("SelectPs_HistoryTypeMaxsort", "") + 1;
                pf.TypeName = frm.TypeTitle;
                pf.Units = frm.Units;
                pf.Remark = frm.Remark;
                // SelectPs_HistoryTypeMaxsort
               
                try
                {
                    Common.Services.BaseService.Create<Ps_HistoryType>(pf);
                    OldId = pf.ID;
                    LoadData();
                }
                catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
            }
        }
        //添加子类
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                MessageBox.Show("请先选择上级类别！");
                return;
            }
            string ParentID = treeList1.FocusedNode["ID"].ToString();
            FormHistoryTypeEdit frm = new FormHistoryTypeEdit();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_HistoryType pf = new Ps_HistoryType();
                pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                pf.Flag = FlagValue;
                pf.ParentID = ParentID;
                pf.Sort = (int)Common.Services.BaseService.GetObject("SelectPs_HistoryTypeMaxsort", "") + 1;
                pf.TypeName = frm.TypeTitle;
                pf.Units = frm.Units;
                pf.Remark = frm.Remark;
                // SelectPs_HistoryTypeMaxsort

                try
                {
                    Common.Services.BaseService.Create<Ps_HistoryType>(pf);
                    OldId = pf.ID;
                    LoadData();
                }
                catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
            }
        }
        //修改类别
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                MessageBox.Show("请先选择结点！");
                return;
            }
            string ParentID = treeList1.FocusedNode["ID"].ToString();
            FormHistoryTypeEdit frm = new FormHistoryTypeEdit();
            frm.Text = "修改分类";
            frm.TypeTitle=treeList1.FocusedNode["TypeName"].ToString();
            frm.Remark = treeList1.FocusedNode["Remark"].ToString();
            frm.Units = treeList1.FocusedNode["Units"].ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_HistoryType pf = new Ps_HistoryType();
                pf.ID = treeList1.FocusedNode["ID"].ToString();
                pf.Flag = FlagValue;
                pf.Sort = int.Parse(treeList1.FocusedNode["Sort"].ToString());
                pf.ParentID = treeList1.FocusedNode["ParentID"].ToString();
                pf.TypeName = frm.TypeTitle;
                pf.Remark = frm.Remark;
                pf.Units = frm.Units;
                // SelectPs_HistoryTypeMaxsort

                try
                {
                    Common.Services.BaseService.Update<Ps_HistoryType>(pf);
                    //修改供电实绩中的名称及分区供电等
                    IList<Ps_History> phlist = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryBYconnstr", " ID like '" + pf.ID+"%'");
                    for (int i = 0; i < phlist.Count; i++)
                    {

                        if (phlist[i].Title.Contains("("))
                        {
                            phlist[i].Title = pf.TypeName + phlist[i].Title.Substring(phlist[i].Title.IndexOf("("), phlist[i].Title.Length - phlist[i].Title.IndexOf("("));
                        }
                        else
                        {
                            phlist[i].Title = pf.TypeName;
                        }

                        Common.Services.BaseService.Update<Ps_History>(phlist[i]);
                        
                    }
                   
                    treeList1.FocusedNode["TypeName"]=frm.TypeTitle;
                    treeList1.FocusedNode["Units"] = frm.Units;
                    treeList1.FocusedNode["Remark"]=frm.Remark;
                }
                catch (Exception ex) { MsgBox.Show("修改分类出错：" + ex.Message); }
            }
        }
        //删除类别
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode != null)
            {
                if (MessageBox.Show("删除结点可能对电力发展实绩、分区供电实绩及报表等产生重大影响！！\n\n                           请确认", "警告", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
                if (treeList1.FocusedNode.Nodes.Count != 0)
                {

                    if (MessageBox.Show(treeList1.FocusedNode["TypeName"].ToString() + "结点下有子结点，是否一起删除?", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DeleteNodes(treeList1.FocusedNode);
                    }
                }
                else
                {
                    if (MessageBox.Show("是否确定删除结点" + treeList1.FocusedNode["TypeName"].ToString() + " ?", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DeleteNodes(treeList1.FocusedNode);
                    }
                }
            }
        }
        //删除结点
        private void DeleteNodes(TreeListNode node)
        {
            if (node.HasChildren)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    DeleteNodes(node.Nodes[i]);
                }
                DeleteNodes(node);
            }
            else
            {
                Ps_HistoryType pht = new Ps_HistoryType();
                pht.ID = node["ID"].ToString();
                Common.Services.BaseService.Delete<Ps_HistoryType>(pht);
                //删除供电实绩中的分类名称及分区供电中的各称
                  IList<Ps_History> phlist = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryBYconnstr", " ID like '" +pht.ID+"%'");
                  for (int i = 0; i < phlist.Count; i++)
                  {
                      Common.Services.BaseService.Delete<Ps_History>(phlist[i]);
                  }
                treeList1.DeleteNode(node);
            }

           
        }
        //单元值改变
        private void treeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (Flag == "")
            {
                return;
            }
            if (e.Column.Caption=="check"&&e.Value.ToString()!=e.Node.GetValue("oldcheck").ToString())
            {
                treeList1.Refresh();
            }
        }
        //绘制时选择发生改变或单位发生改变的用不同颜色标识出来
        private void treeList1_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            if (Flag == "")
            {
                return;
            }
            Brush backBrush, foreBrush;
            if (e.Node.GetValue("oldcheck").ToString() != e.Node.GetValue("check").ToString())
            {
                //Color.Bisque
                backBrush = new LinearGradientBrush(e.Bounds, Color.LightYellow, Color.LightYellow, LinearGradientMode.Horizontal);
                foreBrush = new SolidBrush(Color.Black);
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawString(e.CellText, e.Appearance.Font, foreBrush, e.Bounds, e.Appearance.GetStringFormat());
                e.Handled = true;
            }
            else if ((CheckState)e.Node.GetValue("check") != CheckState.Unchecked && e.Node.GetValue("nowunits").ToString() != e.Node.GetValue("oldunits").ToString())
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.LightSteelBlue, Color.LightSteelBlue, LinearGradientMode.Horizontal);
                foreBrush = new SolidBrush(Color.Black);
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawString(e.CellText, e.Appearance.Font, foreBrush, e.Bounds, e.Appearance.GetStringFormat());
                e.Handled = true;
            }
            else
            {
                backBrush = new SolidBrush(Color.DarkBlue);
                foreBrush = new SolidBrush(Color.PeachPuff);

            }
        }
        //更新选中的模块类别
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //更新电发力发实绩中的类别
            if (Flag=="1")
            {
                RefreshData_DLFZ();
            }
            //更新分区供电实绩中的类别
            if (Flag=="2")
            {
                RefreshData_FQGD();
            }
            //更新分区用电情况类别
            if (Flag == "3")
            {
                RefreshData_FQYD();
            }
      
        }
        //更新电发力发实绩中的类别
        private void RefreshData_DLFZ()
        {
            //改变treelist1的焦点结点，然后再变回来，这样treelist1才会更新值
            if (treeList1.FocusedNode != null)
            {
                TreeListNode node = treeList1.FocusedNode;
                treeList1.FocusedNode = null;
                treeList1.FocusedNode = node;
            }

            //清空添加列表
            AddListID.Clear();
            AddList.Clear();
            //清空减少列表
            ReduceListID.Clear();
            ReduceList.Clear();
            //清空改变单位列表
            changeUnitlist.Clear();
            changeUnitlistID.Clear();
            AddorReduListBynode(treeList1.Nodes, AddList, ReduceList);
            if (AddList.Count == 0 && ReduceList.Count == 0 && changeUnitlist.Count == 0)
            {
                MsgBox.Show("您未做任何修改，不需要更新模块！");
                return;
            }

            FormHistoryTypeEditDeal frm = new FormHistoryTypeEditDeal();
            frm.TypeTitle = "请确认您是否要做如下类别改变？";
            frm.addlist = AddList;
            frm.reducelist = ReduceList;
            frm.changeUnitslist = changeUnitlist;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (AddListID.Count != 0)
                {
                    for (int i = 0; i < AddListID.Count; i++)
                    {
                        //Ps_HistoryType pht = new Ps_HistoryType();
                        //pht.ID = AddListID[i];
                        Ps_HistoryType pht = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(AddListID[i]); ;
                        Ps_History pf = new Ps_History();
                        pf.ID = pht.ID + "|" + ProjectID;
                        pf.Forecast = FormHistory.Historyhome.type;
                        pf.ForecastID = FormHistory.Historyhome.type1;
                        TreeListNode node = treeList1.FindNodeByKeyID(pht.ID);
                        string tempstr = string.Empty;
                        pf.Title = AddList[i].ToString();
                        pf.ParentID = pht.ParentID + "|" + ProjectID;
                        pf.Col4 = MIS.ProgUID;
                        //标识是默认类别生成
                        pf.Col10 = "1";

                        pf.Sort = pht.Sort;

                        try
                        {
                            Common.Services.BaseService.Create<Ps_History>(pf);
                            FormHistory.Historyhome.dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, FormHistory.Historyhome.dataTable.NewRow()));

                        }
                        catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
                        
                        FormHistory.Historyhome.RefreshChart();
                    }

                }
                if (ReduceListID.Count != 0)
                {
                    for (int i = 0; i < ReduceListID.Count; i++)
                    {
                        TreeListNode node = FormHistory.Historyhome.treeList1.FindNodeByKeyID(ReduceListID[i].ToString() + "|" + ProjectID);
                        if (node != null)
                        {
                            FormHistory.Historyhome.DeleteNode(node);
                        }

                    }
                }
                if (changeUnitlistID.Count != 0)
                {
                    for (int i = 0; i < changeUnitlistID.Count; i++)
                    {
                        try
                        {
                            Ps_History pht = Common.Services.BaseService.GetOneByKey<Ps_History>(changeUnitlistID[i] + "|" + ProjectID);
                            pht.Title = changeUnitlist[i].ToString();
                            Common.Services.BaseService.Update<Ps_History>(pht);
                            TreeListNode node = FormHistory.Historyhome.treeList1.FindNodeByKeyID(changeUnitlistID[i].ToString() + "|" + ProjectID);
                            if (node != null)
                            {
                                node["Title"] = changeUnitlist[i].ToString();
                            }

                        }
                        catch (Exception ex) { MsgBox.Show("修改单位出错：" + ex.Message); }

                    }
                }
                FormHistory.Historyhome.treeList1.Refresh();
                EqueValueBynode(treeList1.Nodes);
                treeList1.Refresh();
                MsgBox.Show("更新完成!");
            }
        }
        //更新分区供电实绩中的类别
        private void RefreshData_FQGD()
        {
            //改变treelist1的焦点结点，然后再变回来，这样treelist1才会更新值
            if (treeList1.FocusedNode != null)
            {
                TreeListNode node = treeList1.FocusedNode;
                treeList1.FocusedNode = null;
                treeList1.FocusedNode = node;
            }

            //清空添加列表
            AddListID.Clear();
            AddList.Clear();
            //清空减少列表
            ReduceListID.Clear();
            ReduceList.Clear();
            //清空改变单位列表
            changeUnitlist.Clear();
            changeUnitlistID.Clear();
            AddorReduListBynode(treeList1.Nodes, AddList, ReduceList);
            if (AddList.Count == 0 && ReduceList.Count == 0 && changeUnitlist.Count == 0)
            {
                MsgBox.Show("您未做任何修改，不需要更新模块！");
                return;
            }

            FormHistoryTypeEditDeal frm = new FormHistoryTypeEditDeal();
            frm.TypeTitle = "请确认您是否要做如下类别改变？";
            frm.addlist = AddList;
            frm.reducelist = ReduceList;
            frm.changeUnitslist = changeUnitlist;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (AddListID.Count != 0)
                {
                    for (int i = 0; i < AddListID.Count; i++)
                    {
                        
                        Ps_HistoryType pht = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(AddListID[i]); ;
                        Ps_History pf = new Ps_History();
                        pf.ID = pht.ID + "|" + ProjectID;
                        pf.Forecast = FormFqHistory.Historyhome.type;
                        pf.ForecastID = FormFqHistory.Historyhome.type1;
                        TreeListNode node = treeList1.FindNodeByKeyID(pht.ID);
                        string tempstr = string.Empty;
                        pf.Title = AddList[i].ToString();
                        pf.ParentID = pht.ParentID + "|" + ProjectID;
                        pf.Col4 = MIS.ProgUID;
                        //标识是默认类别生成
                        pf.Col10 = "1";

                        pf.Sort = pht.Sort;

                        try
                        {
                            Common.Services.BaseService.Create<Ps_History>(pf);
                            FormFqHistory.Historyhome.dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, FormFqHistory.Historyhome.dataTable.NewRow()));

                        }
                        catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
                       
                    }

                }
                if (ReduceListID.Count != 0)
                {
                    for (int i = 0; i < ReduceListID.Count; i++)
                    {
                        TreeListNode node = FormFqHistory.Historyhome.treeList1.FindNodeByKeyID(ReduceListID[i].ToString() + "|" + ProjectID);
                        if (node != null)
                        {
                            FormFqHistory.Historyhome.DeleteNode(node);
                        }

                    }
                }
                if (changeUnitlistID.Count != 0)
                {
                    for (int i = 0; i < changeUnitlistID.Count; i++)
                    {
                        try
                        {
                            Ps_History pht = Common.Services.BaseService.GetOneByKey<Ps_History>(changeUnitlistID[i] + "|" + ProjectID);
                            pht.Title = changeUnitlist[i].ToString();
                            Common.Services.BaseService.Update<Ps_History>(pht);
                            TreeListNode node = FormFqHistory.Historyhome.treeList1.FindNodeByKeyID(changeUnitlistID[i].ToString() + "|" + ProjectID);
                            if (node != null)
                            {
                                node["Title"] = changeUnitlist[i].ToString();
                            }

                        }
                        catch (Exception ex) { MsgBox.Show("修改单位出错：" + ex.Message); }

                    }
                }
                FormFqHistory.Historyhome.treeList1.Refresh();
                EqueValueBynode(treeList1.Nodes);
                treeList1.Refresh();
                MsgBox.Show("更新完成!");
            }
        }
        //更新分区用电情况
        private void RefreshData_FQYD()
        {
            //改变treelist1的焦点结点，然后再变回来，这样treelist1才会更新值
            if (treeList1.FocusedNode != null)
            {
                TreeListNode node = treeList1.FocusedNode;
                treeList1.FocusedNode = null;
                treeList1.FocusedNode = node;
            }

            //清空添加列表
            AddListID.Clear();
            AddList.Clear();
            //清空减少列表
            ReduceListID.Clear();
            ReduceList.Clear();
            //清空改变单位列表
            changeUnitlist.Clear();
            changeUnitlistID.Clear();
            AddorReduListBynode(treeList1.Nodes, AddList, ReduceList);
            if (AddList.Count == 0 && ReduceList.Count == 0 && changeUnitlist.Count == 0)
            {
                MsgBox.Show("您未做任何修改，不需要更新模块！");
                return;
            }
            FormHistoryTypeEditDeal frm = new FormHistoryTypeEditDeal();
            frm.TypeTitle = "请确认您是否要做如下类别改变？";
            frm.addlist = AddList;
            frm.reducelist = ReduceList;
            frm.changeUnitslist = changeUnitlist;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (AddListID.Count != 0)
                {
                    for (int i = 0; i < AddListID.Count; i++)
                    {

                        Ps_HistoryType pht = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(AddListID[i]); ;
                        Ps_History pf = new Ps_History();
                        pf.ID = pht.ID + "|" + ProjectID;
                        pf.Forecast = FormFqHistoryDF.Historyhome.type;
                        pf.ForecastID = FormFqHistoryDF.Historyhome.type1;
                        TreeListNode node = treeList1.FindNodeByKeyID(pht.ID);
                        pf.Title = AddList[i].ToString();
                        if (node.Level==1)
                        {
                            pf.ParentID =  ProjectID;
                        }
                        else
                        {
                            pf.ParentID = pht.ParentID + "|" + ProjectID;
                        }
                        pf.Col4 = MIS.ProgUID;
                        //标识是默认类别生成
                        pf.Col10 = "1";
                        pf.Sort = pht.Sort;

                        Ps_History pf2 = new Ps_History();
                        pf2.ID = pht.ID + "|" + ProjectID + FormFqHistoryDF.Historyhome.splitstr;
                        pf2.Forecast = FormFqHistoryDF.Historyhome.type32;
                        pf2.ForecastID = FormFqHistoryDF.Historyhome.type31;
                        pf2.Title = AddList[i].ToString();
                       
                        if (pf2.Title.Contains("用电量"))
                        {
                            pf2.Title = pf2.Title.Replace("用电量", "负荷");
                        }
                        if (pf2.Title.Contains("电量"))
                        {
                            pf2.Title=pf2.Title.Replace("电量", "负荷");
                        }
                        if (pf.Title.Contains("负荷"))
                        {
                            pf.Title = pf.Title.Replace("负荷", "电量");
                        }
                        

                        if (node.Level == 1)
                        {
                            pf2.ParentID = ProjectID + FormFqHistoryDF.Historyhome.splitstr;
                        }
                        else
                        {
                            pf2.ParentID = pht.ParentID + "|" + ProjectID + FormFqHistoryDF.Historyhome.splitstr; ;
                        }
                        pf2.Col4 = MIS.ProgUID;
                        //标识是默认类别生成
                        pf2.Col10 = "1";
                        pf2.Sort = pht.Sort;
                        if (pf2.Title.Contains("同时率"))
                        {
                            pf2.Sort = 0;
                            pf.Sort = 1;
                            for (int j = 1990; j <= 2060; j++)
                            {
                                string y = "y" + j.ToString();
                                pf2.GetType().GetProperty(y).SetValue(pf2, 1.0, null);
                                pf.GetType().GetProperty(y).SetValue(pf, 1, null);
                            }
                        }
                        try
                        {
                            Common.Services.BaseService.Create<Ps_History>(pf);
                            Common.Services.BaseService.Create<Ps_History>(pf2);
                            FormFqHistoryDF.Historyhome.dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, FormFqHistoryDF.Historyhome.dataTable.NewRow()));
                            FormFqHistoryDF.Historyhome.dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf2, FormFqHistoryDF.Historyhome.dataTable2.NewRow()));

                        }
                        catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }

                    }

                }
                if (ReduceListID.Count != 0)
                {
                    for (int i = 0; i < ReduceListID.Count; i++)
                    {
                        TreeListNode node = FormFqHistoryDF.Historyhome.treeList1.FindNodeByKeyID(ReduceListID[i].ToString() + "|" + ProjectID);
                        if (node != null)
                        {
                            FormFqHistoryDF.Historyhome.DeleteNode(node);
                        }
                        TreeListNode node2 = FormFqHistoryDF.Historyhome.treeList2.FindNodeByKeyID(ReduceListID[i].ToString() + "|" + ProjectID + FormFqHistoryDF.Historyhome.splitstr);
                        if (node2 != null)
                        {
                            FormFqHistoryDF.Historyhome.DeleteNode(node2);
                        }
                    }
                }
                if (changeUnitlistID.Count != 0)
                {
                    for (int i = 0; i < changeUnitlistID.Count; i++)
                    {
                        try
                        {
                            Ps_History pht = Common.Services.BaseService.GetOneByKey<Ps_History>(changeUnitlistID[i] + "|" + ProjectID);
                            pht.Title = changeUnitlist[i].ToString();
                            Common.Services.BaseService.Update<Ps_History>(pht);
                            TreeListNode node = FormFqHistoryDF.Historyhome.treeList1.FindNodeByKeyID(changeUnitlistID[i].ToString() + "|" + ProjectID);
                            if (node != null)
                            {
                                node["Title"] = changeUnitlist[i].ToString();
                            }
                            Ps_History pht2 = Common.Services.BaseService.GetOneByKey<Ps_History>(changeUnitlistID[i] + "|" + ProjectID + FormFqHistoryDF.Historyhome.splitstr);
                            pht2.Title = changeUnitlist[i].ToString();
                            if (pht2.Title.Contains("用电量"))
                            {
                                pht2.Title = pht2.Title.Replace("用电量", "负荷");
                            }
                            if (pht2.Title.Contains("电量"))
                            {
                                pht2.Title = pht2.Title.Replace("电量", "负荷");
                            }
                            Common.Services.BaseService.Update<Ps_History>(pht2);
                            TreeListNode node2 = FormFqHistoryDF.Historyhome.treeList1.FindNodeByKeyID(changeUnitlistID[i].ToString() + "|" + ProjectID + FormFqHistoryDF.Historyhome.splitstr);
                            if (node2 != null)
                            {
                                node2["Title"] = pht2.Title;
                            }
                        }
                        catch (Exception ex) { MsgBox.Show("修改单位出错：" + ex.Message); }

                    }
                }
                FormFqHistoryDF.Historyhome.treeList1.Refresh();
                FormFqHistoryDF.Historyhome.treeList2.Refresh();
                EqueValueBynode(treeList1.Nodes);
                treeList1.Refresh();
                MsgBox.Show("更新完成!");
            }
        }
        //将选择类别改变的值挑选出来
        private void AddorReduListBynode(TreeListNodes nodes, List<string> addlist,List<string> redulist)
        {
            if (nodes!= null)
            {
                foreach (TreeListNode node in nodes)
                {
                   
                    if (node.ParentNode!=null&&node.GetValue("check").ToString() != "0" && node.GetValue("oldcheck").ToString() == "0")
                    {

                        AddListID.Add(node.GetValue("ID").ToString());
                        if (node.GetValue("nowunits").ToString()!="")
                        {
                            addlist.Add(node.GetValue("TypeName").ToString() + "(" + node.GetValue("nowunits").ToString()+")");
                        }
                        else
	                    {
                            addlist.Add(node.GetValue("TypeName").ToString());
	                    }
                       
                    }
                    if (node.ParentNode != null && node.GetValue("check").ToString() == "0" && node.GetValue("oldcheck").ToString() != "0")
                    {
                        ReduceListID.Add(node.GetValue("ID").ToString());
                        if (node.GetValue("nowunits").ToString() != "")
                        {
                            redulist.Add(node.GetValue("TypeName").ToString() + "(" + node.GetValue("nowunits").ToString() + ")");
                        }
                         else
	                    {
                            redulist.Add(node.GetValue("TypeName").ToString());
	                    }
                    }
                    bool val1 = node.ParentNode != null && node.GetValue("check").ToString() != "0" && node.GetValue("oldcheck").ToString() == "0";
                    bool val2 = node.ParentNode != null && node.GetValue("check").ToString() == "0" && node.GetValue("oldcheck").ToString() != "0";
                    if (!val1 && !val2 && node.ParentNode != null && node.GetValue("nowunits").ToString() != node.GetValue("oldunits").ToString() && node.GetValue("check").ToString() != "0")
                    {
                        changeUnitlistID.Add(node.GetValue("ID").ToString());
                        if (node.GetValue("nowunits").ToString() != "")
                        {
                             changeUnitlist.Add(node.GetValue("TypeName").ToString() + "(" + node.GetValue("nowunits").ToString() + ")");
                        }
                        else
                        {
                            changeUnitlist.Add(node.GetValue("TypeName").ToString());
                        }
                    }
                    if (node.HasChildren)
                    {
                        AddorReduListBynode(node.Nodes, addlist, redulist);
                    }
                    
                    
                }
                
            }
        }
        //类更新完成后将check与oldcheck设为相对值，以便下次更改时标记
        private void EqueValueBynode(TreeListNodes nodes)
        {
            if (nodes != null)
            {
                foreach (TreeListNode node in nodes)
                {
                    EqueVlaueDataTable(node.GetValue("ID").ToString(), (CheckState)node.GetValue("check"), (CheckState)node.GetValue("check"));
                    if (node.HasChildren)
                    {
                        EqueValueBynode(node.Nodes);
                    }
                }

            }
        }
        private void EqueVlaueDataTable( string ID, CheckState check,CheckState oldcheck)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i]["ID"].ToString()==ID)
                {
                    dataTable.Rows[i]["check"] = check;
                    dataTable.Rows[i]["oldcheck"] = oldcheck;
                    dataTable.Rows[i]["oldunits"] = dataTable.Rows[i]["nowunits"];
                }
            }
        }
        //关闭
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void treeList1_AfterFocusNode(object sender, NodeEventArgs e)
        {
            if (Flag==""||e.Node==null)
            {
                return;
            }
            repositoryItemComboBox2.Items.Clear();
            List<string> str = new List<string>();
            if (e.Node.GetValue("Units").ToString()!="")
            {
                string tempstr = e.Node.GetValue("Units").ToString();
                str = ToList(str, tempstr);
                if (str.Count>1)
                {
                    for (int i = 0; i < str.Count; i++)
                    {
                        repositoryItemComboBox2.Items.Add(str[i]);
                    }
                    if (e.Node.GetValue("nowunits").ToString()=="")
                    {
                        e.Node["nowunits"] = str[0];
                    }
                    treeList1.Columns["nowunits"].OptionsColumn.AllowFocus = true;
                }
                else
                {
                    e.Node["nowunits"] = e.Node.GetValue("Units").ToString();
                    treeList1.Columns["nowunits"].OptionsColumn.AllowFocus = false;
                }
               
            }
            else
            {
                treeList1.Columns["nowunits"].OptionsColumn.AllowFocus = false;
            }
        }
        //当选中结点或取消选中时改变结点的单位
        private void ChangeUnits(TreeListNode node)
        {
            if (node==null)
            {
                return;
            }
            repositoryItemComboBox2.Items.Clear();
            repositoryItemComboBox2.AllowFocused = false;
            List<string> str = new List<string>();
            if (node.GetValue("Units").ToString() != "")
            {
                string tempstr = node.GetValue("Units").ToString();
                str = ToList(str, tempstr);
                if (str.Count > 1)
                {
                    for (int i = 0; i < str.Count; i++)
                    {
                        repositoryItemComboBox2.Items.Add(str[i]);
                    }
                    if (node.GetValue("nowunits").ToString() == "")
                    {
                        node["nowunits"] = str[0];
                    }
                    repositoryItemComboBox2.AllowFocused = true;
                }
                else
                {
                    node["nowunits"] = node.GetValue("Units").ToString();
                }

            }
        }
        //根据"#"把单位一个一个提取出来
        private List<string> ToList(List<string> strlist,string tempstr)
        {
            if (!tempstr.Contains("#"))
            {
                strlist.Add(tempstr);
                return strlist;
            }
            else
            {
                string str = tempstr.Substring(0, tempstr.IndexOf("#"));
                strlist.Add(str);
                tempstr = tempstr.Substring(tempstr.IndexOf("#") + 1, tempstr.Length - tempstr.IndexOf("#") - 1);
                return ToList(strlist, tempstr);
            }
        }

       
       

       
       

    }
}