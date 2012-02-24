using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Base;
using Itop.Client.Common;
using System.Collections;

namespace ItopVector.Tools
{
    public partial class frmPengFenLine : FormModuleBase
    {
        
        public IList<PSP_SubstationUserNum> list = null;
        public IList<PSP_SubstationUserNum> list2 = null;
        public bool Create1 = false;
        public bool Create2 = false;
        public string PID = "";
        public string sss = "";
        private ArrayList selList = new ArrayList();

        public ArrayList SelList
        {
            get { return selList; }
            set { selList = value; }
        }

        float[] f = null;
        public frmPengFenLine()
        {
            InitializeComponent();
        }

        private void frmPengFen_Load(object sender, EventArgs e)
        {
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barEdit.Caption = "保存";
            barAdd.Caption = "计算权值";
            barPrint.Caption = "评分结果";
            LoadTree();
           // LoadData();
            DataTable dt = new DataTable();
            dt.Columns.Add("A",typeof(string));
            dt.Columns.Add("B", typeof(string));
            DataRow dr = dt.NewRow();
            dr["A"] = "很好";
            dr["B"] = "很好";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["A"] = "好";
            dr["B"] = "好";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["A"] = "一般";
            dr["B"] = "一般";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["A"] = "差";
            dr["B"] = "差";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["A"] = "很差";
            dr["B"] = "很差";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["A"] = "极差";
            dr["B"] = "极差";
            dt.Rows.Add(dr);
            repositoryItemLookUpEdit1.DisplayMember = "A";
            repositoryItemLookUpEdit1.NullText = "";
            repositoryItemLookUpEdit1.ValueMember = "B";
            repositoryItemLookUpEdit1.DataSource = dt;

        }
     
        public void LoadTree()
        {
            PSP_PlanList sel1 = new PSP_PlanList();
            sel1.col1 = PID;
            IList<LineList1> list1 = Services.BaseService.GetList<LineList1>("SelectLineList1ByRefLineEleID", sel1);
            for (int i = 0; i < list1.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Text = list1[i].LineName;
                node.Tag = list1[i].UID;
                //LineList1 sel2 = new LineList1();
                //sel2.col1 = list1[i].UID;
                //IList<LineList1> list2 = Services.BaseService.GetList<LineList1>("SelectLineList1ByRefLineEleID", sel2);
                //for (int j = 0; j < list2.Count; j++)
                //{
                //    TreeNode _node = new TreeNode();
                //    _node.Text = list2[j].LineName;
                //    _node.Tag = list2[j].UID;
                //    node.Nodes.Add(_node);
                //}
                treeView1.Nodes.Add(node);
            }
        }
        public void LoadData(string id)
        {

            PSP_SubstationUserNum num1 = new PSP_SubstationUserNum();
            num1.userID = Itop.Client.MIS.UserNumber;
            num1.SubStationID = id;
            num1.num = 3;
            list = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationUserNumByUser", num1);
            if (list.Count == 0)
            {
                Create1 = true;
                PSP_SubstationPar par1 = new PSP_SubstationPar();
                par1.type = 3;
                IList<PSP_SubstationPar> _list = Services.BaseService.GetList<PSP_SubstationPar>("SelectPSP_SubstationParByType", par1);
                for (int i = 0; i < _list.Count; i++)
                {
                    PSP_SubstationUserNum n1 = new PSP_SubstationUserNum();
                    n1.SubStationID = _list[i].InfoName;
                    n1.col1 = _list[i].col1;
                    //n1.col2 = "是";
                    n1.Remark = _list[i].InfoName;
                    n1.col4 = _list[i].UID;
                    list.Add(n1);
                }
            }
            else
            {
                list.Clear();
                PSP_SubstationPar par1 = new PSP_SubstationPar();
                par1.type = 3;
                IList<PSP_SubstationPar> _list = Services.BaseService.GetList<PSP_SubstationPar>("SelectPSP_SubstationParByType", par1);
                for (int i = 0; i < _list.Count; i++)
                {
                    PSP_SubstationUserNum n1 = new PSP_SubstationUserNum();
                    n1.SubStationID = _list[i].InfoName;
                    n1.col1 = _list[i].col1;
                    //n1.col2 = "是";
                    n1.Remark = _list[i].InfoName;
                    n1.col4 = _list[i].UID;
                    n1.SubParID = _list[i].UID;
                    list.Add(n1);
                }
                Create1 = false;
            }
            gridControl1.DataSource = list;
            //num1.num = 2;
            //list2 = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationUserNumByUser", num1);
            //if (list2.Count == 0)
            //{
            //    Create2 = true;
            //    PSP_SubstationPar par1 = new PSP_SubstationPar();
            //    par1.type = 3;
            //    IList<PSP_SubstationPar> _list = Services.BaseService.GetList<PSP_SubstationPar>("SelectPSP_SubstationParByType", par1);
            //    for (int i = 0; i < _list.Count; i++)
            //    {
            //        PSP_SubstationUserNum n2 = new PSP_SubstationUserNum();
            //        n2.SubStationID = _list[i].InfoName;
            //        n2.num = 0;
            //        n2.col1 = "1";
            //        n2.col4 = _list[i].UID;
            //        list2.Add(n2);
            //    }
            //}
            //else
            //{
            //    Create2 = false;
            //}
            //gridControl1.DataSource = list2;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            sss = treeView1.SelectedNode.Tag.ToString();
            LoadData(treeView1.SelectedNode.Tag.ToString());
        }
        protected override void Add()
        {
            //if(treeView1.SelectedNode==null || treeView1.SelectedNode.Parent!=null){
            //    MessageBox.Show("请选择线路规划方案（第一级树目录）。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

         

            string uid = PID; //treeView1.SelectedNode.Tag.ToString();
            LineList1 line = new LineList1();
            line.col1 = uid;
            IList<LineList1> linelist = Services.BaseService.GetList<LineList1>("SelectLineList1ByRefLineEleID", line);
            ArrayList val = new ArrayList();

         

            for (int i = 0; i < linelist.Count;i++ )
            {
                PSP_SubstationUserNum num1 = new PSP_SubstationUserNum();
                num1.userID = Itop.Client.MIS.UserNumber;
                num1.SubStationID = linelist[i].UID;
                num1.num = 3;
                IList<PSP_SubstationUserNum>  sublist = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationUserNumByUser", num1);
                if(sublist.Count==0){
                    MessageBox.Show("线路"+linelist[i].LineName +"还没有评分完成，不能自动计算权值。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                f = new float[sublist.Count];
                float sum = 0f;
                float[] subnum=new float[sublist.Count];
                for (int j = 0; j < sublist.Count;j++ )
                {
                    sum = sum + getNumber(sublist[j].col2);
                }
                for (int j = 0; j < sublist.Count; j++)
                {
                    if (getNumber(sublist[j].col2) == 0)
                    {
                        //subnum[j] = 1 / sum;
                        subnum[j] = 0;
                    }
                    else
                    {
                        subnum[j] = getNumber(sublist[j].col2) / sum;
                    }
                }
                val.Add(subnum);
            }
            for (int i = 0; i < val.Count;i++ )
            {
                float[] t = val[i] as float[];
                for (int j = 0; j < f.Length;j++ )
                {
                    f[j] = f[j] + t[j];
                }

            }
            for (int i = 0; i < f.Length;i++ )
            {
                f[i] = Convert.ToSingle(Math.Round((f[i] / val.Count), 3));
            }
            for (int i = 0; i < linelist.Count; i++)
            {
                PSP_SubstationUserNum num1 = new PSP_SubstationUserNum();
                num1.userID = Itop.Client.MIS.UserNumber;
                num1.SubStationID = linelist[i].UID;
                num1.num = 3;
                IList<PSP_SubstationUserNum> sublist = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationUserNumByUser", num1);
                for (int j = 0; j < sublist.Count;j++ )
                {
                    PSP_SubstationUserNum t = sublist[j];
                    t = Services.BaseService.GetOneByKey<PSP_SubstationUserNum>(t);
                    t.col1 = Convert.ToString(f[j]);
                    Services.BaseService.Update<PSP_SubstationUserNum>(t);
                }
            }
            f = null;
            MessageBox.Show("更新完成。");
            LoadData(sss);
        }
        public int getNumber(string str)
        {
            int i = 0;
            if (str == "很好")
            {
                i = 9;
                return i;
            }
            if (str == "好")
            {
                i = 7;
                return i;
            }
            if (str == "一般")
            {
                i = 5;
                return i;
            }
            if (str == "差")
            {
                i = 3;
                return i;
            }
            if (str == "很差")
            {
                i = 1;
                return i;
            }
            if (str == "极差" || str == "")
            {
                i = 0;
                return i;
            }
            else
            {
                i = Convert.ToInt32(str);
                return i;
            }
           
        }
        protected override void Print()
        {
            frmTongJi2 f = new frmTongJi2();
            f.LoadData(PID);
            f.ShowDialog();
        }
        protected override void Edit()
        {
            //if(f==null){
            //    MessageBox.Show("请先计算权值。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            decimal a = 0;
            for (int i = 0; i < list.Count; i++)
            {
                PSP_SubstationUserNum p = list[i];
                decimal t = 0;
                if (p.col1 != "")
                {
                    a = a + Convert.ToDecimal(p.col1);
                }
                //  a = p.col1;
                //Services.BaseService.Update<PSP_SubstationUserNum>(list[i]);
            }
            if (a != 1)
            {
                MessageBox.Show("权重之和不等于1。当前权重之和为"+a.ToString());
                return;
            }
            if (Create1)
            {
                for (int i = 0; i < list.Count;i++ )
                {
                    PSP_SubstationUserNum p = list[i];
                    p.UID = Guid.NewGuid().ToString();
                    p.userID = Itop.Client.MIS.UserNumber;
                    p.SubStationID = treeView1.SelectedNode.Tag.ToString();
                    p.SubParID = p.col4;
                    Services.BaseService.Create<PSP_SubstationUserNum>(p);
                }
            }
            else{
                for (int i = 0; i < list.Count; i++)
                {
                    PSP_SubstationUserNum p = list[i];
                    p.SubStationID = p.col4;
                    Services.BaseService.Update<PSP_SubstationUserNum>(list[i]);
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                PSP_SubstationUserNum _u = list[i];
                PSP_SubstationPar _p = Services.BaseService.GetOneByKey<PSP_SubstationPar>(_u.SubParID);
                _p.col1 = _u.col1;
                Services.BaseService.Update<PSP_SubstationPar>(_p);

            }
            MessageBox.Show("更新完成。");
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}