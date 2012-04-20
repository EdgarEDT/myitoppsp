using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Itop.Common;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Client.Base;

namespace ItopVector.Tools
{
    public partial class frmImgManager : FormBase
    {
        private string uid;
        private Image pic = null;
        private string strName;


        private string strRemark;

        public string StrRemark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }

        public Image Pic
        {
            get { return pic; }
            set { pic = value; }
        }
        public string StrName
        {
            get { return strName; }
            set { strName = value; }
        }

        public frmImgManager()
        {
            InitializeComponent();
        }

        public void GetTree(string pid, TreeNode root)
        {
            try
            {
                PSP_ImgTree tree=new PSP_ImgTree();
                tree.PID=pid;
                IList list = Services.BaseService.GetList("SelectPSP_ImgTreeListByPID", tree);
                for (int i = 0; i < list.Count;i++ )
                {
                    PSP_ImgTree nd = (PSP_ImgTree)list[i];
                    TreeNode node = new TreeNode();
                    node.Tag = nd.UID;
                    node.Text = nd.Name;
                    GetTree(nd.UID,node);
                    root.Nodes.Add(node);
                }
                
            }
            catch (Exception exc)
            {
               
            }

        }

        private void frmImgManager_Load(object sender, EventArgs e)
        {
            PSP_ImgTree tree = new PSP_ImgTree();
            tree.PID = "0";
            IList list = Services.BaseService.GetList("SelectPSP_ImgTreeListByPID", tree);
            for (int i = 0; i < list.Count; i++)
            {
                PSP_ImgTree nd = (PSP_ImgTree)list[i];
                TreeNode node=new TreeNode();
                node.Tag= nd.UID;
                node.Text= nd.Name;
                GetTree(nd.UID, node);
                treeView1.Nodes.Add(node);
            }
            if(pic==null){
                btSave.Enabled = false;
            }
        }

        private void addMenuItem_Click(object sender, EventArgs e)
        {
            if(uid==""){
                MessageBox.Show("请选择节点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmInputDialog frm = new frmInputDialog();
            if (frm.ShowDialog()==DialogResult.OK)
            {
                string str= frm.InputStr;
                TreeNode node = new TreeNode();
                node.Tag = Guid.NewGuid().ToString();
                node.Text = str;
                treeView1.SelectedNode.Nodes.Add(node);
                PSP_ImgTree t=new PSP_ImgTree();
                t.UID=node.Tag.ToString();
                t.Name=node.Text;
                t.PID=treeView1.SelectedNode.Tag.ToString();
                Services.BaseService.Create<PSP_ImgTree>(t);
            }
        }

        private void delMenuItem_Click(object sender, EventArgs e)
        {
            if (uid == "")
            {
                MessageBox.Show("请选择目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(treeView1.SelectedNode.Nodes.Count>0){
                MessageBox.Show("选择的目录包含子目录，不能删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PSP_ImgInfo img = new PSP_ImgInfo();
            img.TreeID = uid;
            IList<PSP_ImgInfo> list = Services.BaseService.GetList<PSP_ImgInfo>("SelectPSP_ImgInfoList", img);
            if(list.Count>0){
                MessageBox.Show("选择的目录包含文件，不能删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(MessageBox.Show("确定要删除么？","提示",MessageBoxButtons.OK,MessageBoxIcon.Information)==DialogResult.OK){
                Services.BaseService.DeleteByKey<PSP_ImgTree>(treeView1.SelectedNode.Tag.ToString());
                treeView1.Nodes.Remove(treeView1.SelectedNode);
                uid = "";
            }
        }

        private void editMenuItem_Click(object sender, EventArgs e)
        {

            frmInputDialog frm = new frmInputDialog();
            frm.InputStr = treeView1.SelectedNode.Text;
            if (frm.ShowDialog()==DialogResult.OK)
            {
                treeView1.SelectedNode.Text = frm.InputStr;
                PSP_ImgTree node = Services.BaseService.GetOneByKey<PSP_ImgTree>(treeView1.SelectedNode.Tag.ToString());
                node.Name = frm.InputStr;
                Services.BaseService.Update<PSP_ImgTree>(node);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            uid=treeView1.SelectedNode.Tag.ToString();
            RefreshData(uid);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            
            if(txtName.Text==""){
               
                MessageBox.Show("请输入名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return;
            }
            StrName =txtName.Text;
            StrRemark =txtRemark.Text;
            
            if (uid == "")
            {
                MessageBox.Show("请选择目录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (pic == null)
            {
                MessageBox.Show("没有选择图片。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MemoryStream Ms = new MemoryStream(); 
            pic.Save(Ms, System.Drawing.Imaging.ImageFormat.Jpeg); 
            byte[] img = new byte[Ms.Length]; 
            Ms.Position = 0; 
            Ms.Read(img, 0, Convert.ToInt32(Ms.Length)); 
            Ms.Close();
            
            PSP_ImgInfo imgInfo = new PSP_ImgInfo();
            imgInfo.UID = Guid.NewGuid().ToString();
            imgInfo.TreeID = uid;
            imgInfo.Image = img;
            imgInfo.Name = strName;
            imgInfo.Remark = strRemark;
            Services.BaseService.Create<PSP_ImgInfo>(imgInfo);
            pic = null;
            RefreshData(uid);
            txtName.Text = "";
            txtRemark.Text = "";
        }
        public PSP_ImgInfo FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_ImgInfo; }
        }
        public bool RefreshData(string id)
        {
            try
            {
                PSP_ImgInfo img=new PSP_ImgInfo();
                img.TreeID=id;
                IList<PSP_ImgInfo> list = Services.BaseService.GetList<PSP_ImgInfo>("SelectPSP_ImgInfoList",img);
       
                this.gridControl.DataSource = list;
            }
            catch (Exception exc)
            {
                HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }



        private void btDel_Click(object sender, EventArgs e)
        {
            //获取焦点对象
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
            //ObjectList.Remove(obj);
            gridView.DeleteRow(iOldHandle);
            //刷新表格
            gridControl.RefreshDataSource();
            //设置新的焦点行索引

            GridHelper.FocuseRowAfterDelete(this.gridView, iOldHandle);
            this.gridView.EndUpdate();
        }

        private void btView_Click(object sender, EventArgs e)
        {
            PSP_ImgInfo obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
            obj=Services.BaseService.GetOneByKey<PSP_ImgInfo>(obj.UID);
            BinaryWriter bw;
            FileStream fs;
            try
            {
                byte[] bt = obj.Image;
                string filename = obj.Name;
                fs = new FileStream("C:\\" + filename+".jpg", FileMode.Create, FileAccess.Write);
                bw = new BinaryWriter(fs);
                bw.Write(bt);
                bw.Flush();
                bw.Close();
                fs.Close();
                System.Diagnostics.Process.Start("C:\\" + filename + ".jpg");
         
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }

        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            PSP_ImgInfo obj = FocusedObject;
            frmImgInfoInput f = new frmImgInfoInput();
            f.StrName = obj.Name;
            f.StrRemark = obj.Remark;
            if (f.ShowDialog() == DialogResult.OK)
            {
                obj = Services.BaseService.GetOneByKey<PSP_ImgInfo>(obj.UID);
                obj.Name = f.StrName;
                obj.Remark = f.StrRemark;
                Services.BaseService.Update<PSP_ImgInfo>(obj);
                RefreshData(uid);
            }
        }
    }
}