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

namespace ItopVector.Tools
{
    public partial class frmPengFen : FormModuleBase
    {
        public IList<PSP_SubstationUserNum> list = null;
        public IList<PSP_SubstationUserNum> list2 = null;
        public bool Create1 = false;
        public bool Create2 = false;
        public frmPengFen()
        {
            InitializeComponent();
        }

        private void frmPengFen_Load(object sender, EventArgs e)
        {
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barEdit.Caption = "保存";
            
            LoadTree();
           // LoadData();
        }
     
        public void LoadTree()
        {
            PSP_SubstationMng sel1 = new PSP_SubstationMng();
            IList<PSP_SubstationMng> list1 = Services.BaseService.GetList<PSP_SubstationMng>("SelectPSP_SubstationMngList", sel1);
            for (int i = 0; i < list1.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Text = list1[i].SName;
                node.Tag = list1[i].UID;
                PSP_SubstationSelect sel2 = new PSP_SubstationSelect();
                sel2.col2 = list1[i].UID;
                IList<PSP_SubstationSelect> list2 = Services.BaseService.GetList<PSP_SubstationSelect>("SelectPSP_SubstationSelectList", sel2);
                for (int j = 0; j < list2.Count; j++)
                {
                    TreeNode _node = new TreeNode();
                    _node.Text = list2[j].SName;
                    _node.Tag = list2[j].UID;
                    node.Nodes.Add(_node);
                }
                treeView1.Nodes.Add(node);
            }
        }
        public void LoadData(string id)
        {

            PSP_SubstationUserNum num1 = new PSP_SubstationUserNum();
            num1.userID = Itop.Client.MIS.UserNumber;
            num1.SubStationID = id;
            num1.num = 1;
            list = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationUserNumByUser", num1);
            if (list.Count == 0)
            {
                Create1 = true;
                PSP_SubstationPar par1 = new PSP_SubstationPar();
                par1.type = 1;
                IList<PSP_SubstationPar> _list = Services.BaseService.GetList<PSP_SubstationPar>("SelectPSP_SubstationParByType", par1);
                for (int i = 0; i < _list.Count; i++)
                {
                    PSP_SubstationUserNum n1 = new PSP_SubstationUserNum();
                    n1.Remark = _list[i].InfoName;
                    n1.col1 = "是";
                    n1.col2 = "是";
                    n1.col4 = _list[i].UID;
                    list.Add(n1);
                }
            }
            else
            {
                Create1 = false;
            }
            gridControl.DataSource = list;
            num1.num = 2;
            list2 = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationUserNumByUser", num1);
            if (list2.Count == 0)
            {
                Create2 = true;
                PSP_SubstationPar par1 = new PSP_SubstationPar();
                par1.type = 2;
                IList<PSP_SubstationPar> _list = Services.BaseService.GetList<PSP_SubstationPar>("SelectPSP_SubstationParByType", par1);
                for (int i = 0; i < _list.Count; i++)
                {
                    PSP_SubstationUserNum n2 = new PSP_SubstationUserNum();
                    n2.Remark = _list[i].InfoName;
                    n2.num = 0;
                    n2.col1 = _list[i].col1;
                    n2.col4 = _list[i].UID;
                    list2.Add(n2);
                }
            }
            else
            {
                Create2 = false;
            }
            gridControl1.DataSource = list2;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadData(treeView1.SelectedNode.Tag.ToString());
        }
        protected override void Edit()
        {
            decimal a = 0;
            for (int i = 0; i < list2.Count; i++)
            {
                PSP_SubstationUserNum p = list2[i];
                decimal t=0;
                if (p.col1 != "")
                {
                    a = a +Convert.ToDecimal(p.col1);
                }
              //  a = p.col1;
                //Services.BaseService.Update<PSP_SubstationUserNum>(list[i]);
            }
            if (a!=1){
                MessageBox.Show("权重之和不等于1。当前权重之和为" + a.ToString());
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
                Create1 = false;
            }
            else{
                for (int i = 0; i < list.Count; i++)
                {
                    PSP_SubstationUserNum p = list[i];
                    //p.SubStationID = p.col4;
                    Services.BaseService.Update<PSP_SubstationUserNum>(list[i]);
                }
            }

            /////////////////////////////////////////////////////


            if (Create2)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    PSP_SubstationUserNum p = list2[i];
                    p.UID = Guid.NewGuid().ToString();
                    p.userID = Itop.Client.MIS.UserNumber;
                    p.SubStationID = treeView1.SelectedNode.Tag.ToString();
                    p.SubParID = p.col4;
                    Services.BaseService.Create<PSP_SubstationUserNum>(p);
                }
                Create2 = false;
            }
            else
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    PSP_SubstationUserNum p = list2[i];
                    //p.SubStationID = p.col4;
                    Services.BaseService.Update<PSP_SubstationUserNum>(list2[i]);
                }
            }
            for (int i = 0; i < list2.Count;i++ )
            {
                PSP_SubstationUserNum _u = list2[i];
                PSP_SubstationPar _p = Services.BaseService.GetOneByKey<PSP_SubstationPar>(_u.SubParID);
                _p.col1 = _u.col1;
                Services.BaseService.Update<PSP_SubstationPar>(_p);

            }

            //PSP_SubstationPar par1 = new PSP_SubstationPar();
            //par1.type = 2;
            //IList<PSP_SubstationPar> _list = Services.BaseService.GetList<PSP_SubstationPar>("SelectPSP_SubstationParByType", par1);
            MessageBox.Show("保存成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}