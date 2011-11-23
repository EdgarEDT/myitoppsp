using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Common;
using System.Threading;
using System.IO;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmSubstatinYXInfo : FormBase
    {
        string uid = "";
        public frmSubstatinYXInfo()
        {
            InitializeComponent();
        }
        public IList<PSP_ImgInfo> ObjectList
        {
            get { return this.gridControl.DataSource as IList<PSP_ImgInfo>; }
        }
        private void frmSubstatinYXInfo_Load(object sender, EventArgs e)
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
                for (int j = 0; j < list2.Count;j++ )
                {
                    TreeNode _node = new TreeNode();
                    _node.Text = list2[j].SName;
                    _node.Tag = list2[j].UID;
                    node.Nodes.Add(_node);
                }
                treeView1.Nodes.Add(node);
            }

            
          
        }
        public PSP_ImgInfo FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_ImgInfo; }
        }
        public void LoadData(string id)
        {
            PSP_ImgInfo info = new PSP_ImgInfo();
            info.TreeID = id;
            gridControl.DataSource= Services.BaseService.GetList<PSP_ImgInfo>("SelectPSP_ImgInfoList", info);
            gridControl.RefreshDataSource();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                uid = treeView1.SelectedNode.Tag.ToString();
                LoadData(treeView1.SelectedNode.Tag.ToString());
            }
        }

        private void 增加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(uid==""){
                MessageBox.Show("请先选择变电站","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            frmImgAdd f = new frmImgAdd();
            f.Uid = uid;
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadData(uid);
            }
        }
        public void View()
        {
            BinaryWriter bw;
            FileStream fs;
            PSP_ImgInfo info = FocusedObject;
            info = (PSP_ImgInfo)Services.BaseService.GetObject("SelectPSP_ImgInfoByKey", info);
            fs = new FileStream(Application.StartupPath + info.Name, FileMode.Create, FileAccess.Write);
            bw = new BinaryWriter(fs);
            bw.Write(info.Image);
            bw.Flush();
            bw.Close();
            fs.Close();
            System.Diagnostics.Process.Start(Application.StartupPath + info.Name);
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PSP_ImgInfo obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
        
            //请求确认
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }

            //执行删除操作
            try
            {
                Services.BaseService.Delete<PSP_ImgInfo>(obj);
            }
            catch (Exception exc)
            {
            
                HandleException.TryCatch(exc);
                return;
            }

            this.gridView.BeginUpdate();
            //记住当前焦点行索引
            int iOldHandle = this.gridView.FocusedRowHandle;
            //从链表中删除
            ObjectList.Remove(obj);
            //刷新表格
            gridControl.RefreshDataSource();
            //设置新的焦点行索引
            GridHelper.FocuseRowAfterDelete(this.gridView, iOldHandle);
            this.gridView.EndUpdate();
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FocusedObject==null)
            {
                MessageBox.Show("请先选择记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            View();
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {
            if (FocusedObject == null)
            {
                MessageBox.Show("请先选择记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            View();
        }
      
    }
}