using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.RightManager;
using Itop.Server.Interface;
using Itop.Common;
using Itop.Client;
using System.Collections;
namespace Itop.RightManager.UI
{
    public partial class FrmProjectSelect : Itop.Client.Base.FormBase
    {
        public FrmProjectSelect()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        public string groupno = string.Empty;
        public string groupname = string.Empty;
        private IBaseService sysService;
        public IBaseService SysService
        {
            get
            {
                if (sysService == null)
                {
                    sysService = RemotingHelper.GetRemotingService<IBaseService>();
                }
                if (sysService == null) MsgBox.Show("IBaseService����û��ע��");
                return sysService;
            }
        }
        private void FrmProjectSelect_Load(object sender, EventArgs e)
        {
            this.Text="ѡ����� ["+groupname+"] ��Ȩ�ľ�";
            InitData();
        }
        private void InitData()
        {
            string s = "  IsGuiDang!='��' order by SortID";
            IList<Project> list = SysService.GetList<Project>("SelectProjectByWhere", s);
            for (int i = 0; i < list.Count; i++)
            {
                VsmdgroupProg smdgroup = new VsmdgroupProg();
                //��Ȩ����ģ���IDΪb9b2acb7-e093-4721-a92f-749c731b016e
                smdgroup = MIS.GetProgRight("b9b2acb7-e093-4721-a92f-749c731b016e", list[i].UID);
                if (Convert.ToInt32(smdgroup.run)==0&&list[i].ProjectManager!="")
                {
                    list.Remove(list[i]);
                    i--;
                }
            }
            dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Project));
            this.treeList1.DataSource = dt;
            
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            if (treeList1.FocusedNode["ProjectManager"] == null)
            {
                MessageBox.Show("��ѡ�����Ȩ��");
                return;
            }

            FrmGroupRights dlg = new FrmGroupRights();
            dlg.Groupno = groupno;
            dlg.ProjectUID = treeList1.FocusedNode["UID"].ToString();
            dlg.ProjectName = treeList1.FocusedNode["ProjectName"].ToString(); ;
            dlg.ShowDialog();
        
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}