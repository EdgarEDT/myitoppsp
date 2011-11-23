using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ItopVector.Resource;
using Itop.Domain.Graphics;
using Itop.Client;

using ItopVector.Core.Document;
using Itop.Client.Common;

namespace ItopVector.Tools
{
    public delegate void OnOpenDocumenthandler(object sender, string _svgUid);
    public partial class CtrlFileManager : UserControl
    {
        
        TreeNode root = new TreeNode();
        private ImageList imageList1;
        SVGFOLDER folder = new SVGFOLDER();
        SVGFILE svgFile = new SVGFILE();
        public TreeNode CurTreeNode = null;
        private string svgUid = "";
        public event OnOpenDocumenthandler OnOpenSvgDocument;
        public bool Add=false;
        public bool Edit=false;
        public bool Del=false;

        public CtrlFileManager()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch(e.ClickedItem.Text){
                case "打开":
                    Open();
                    break;
                case "删除":
                    if (!Del)
                    {
                        MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    DelTreeNode();
                    break;
              
                case "修改":
                    if (!Edit)
                    {
                        MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    ReName();
                    break;
                case "复制":
                    Copy();
                    break;
            }
        }
        public void Open()
        {
            if (CurTreeNode.ImageIndex == 2)
            {
                InitSvgServer();
            }
            if (CurTreeNode.ImageIndex == 8)
            {
                OpenSvgFolder();
            }
            if (CurTreeNode.ImageIndex == 9)
            {
                OpenSvgFile();
            }
        }
        public void InitSvgServer()
        {
            if(root.Nodes.Count>0){
                return;
            }
            try
            {
                ReLoadData();
                /*IList folderList = Services.BaseService.GetList("SelectSVGFOLDERByParent", folder);
                for (int i = 0; i < folderList.Count; i++)
                {
                    SVGFOLDER f = (SVGFOLDER)folderList[i];
                    TreeNode treeNode = new TreeNode();
                    treeNode.Tag = f;
                    treeNode.ImageIndex = 8;
                    treeNode.Text = f.FOLDERNAME;
                    root.Nodes.Add(treeNode);
                }*/
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ReLoadData()
        {
            if(CurTreeNode!=null){
                CurTreeNode.Nodes.Clear();
            }
            folder.PARENTID = ((SVGFOLDER)CurTreeNode.Tag).SUID;
            IList folderList = Services.BaseService.GetList("SelectSVGFOLDERByParent", folder);
            for (int i = 0; i < folderList.Count; i++)
            {
                SVGFOLDER f = (SVGFOLDER)folderList[i];
                TreeNode treeNode = new TreeNode();
                treeNode.Tag = f;
                treeNode.ImageIndex = 8;
                treeNode.Text = f.FOLDERNAME;
                CurTreeNode.Nodes.Add(treeNode);
            }

            svgFile.PARENTID = ((SVGFOLDER)CurTreeNode.Tag).SUID;
            IList svgList = Services.BaseService.GetList("SelectSVGFILEByParent", svgFile);
            for (int i = 0; i < svgList.Count; i++)
            {
                SVGFILE f = (SVGFILE)svgList[i];
                TreeNode treeNode = new TreeNode();
                treeNode.Tag = f;
                treeNode.ImageIndex = 9;
                treeNode.Text = f.FILENAME;
                CurTreeNode.Nodes.Add(treeNode);
            }
            CurTreeNode.Expand();
        }
        public void AddSvgFile()
        {
            try
            {
                frmInputDialog f = new frmInputDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    SVGFILE _svgFile = new SVGFILE();
                    _svgFile.SUID = Guid.NewGuid().ToString();
                    _svgFile.FILENAME = f.InputStr;
                    _svgFile.PARENTID = ((SVGFOLDER)CurTreeNode.Tag).SUID;
                    Services.BaseService.Create<SVGFILE>(_svgFile);
                    ReLoadData();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void AddSvgFolder()
        {
            try
            {
                frmInputDialog f = new frmInputDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    SVGFOLDER _svgFolder = new SVGFOLDER();
                    _svgFolder.SUID = Guid.NewGuid().ToString();
                    _svgFolder.FOLDERNAME = f.InputStr;
                    _svgFolder.PARENTID = ((SVGFOLDER)CurTreeNode.Tag).SUID;
                    Services.BaseService.Create<SVGFOLDER>(_svgFolder);
                    ReLoadData();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void DelTreeNode()
        {
            if (CurTreeNode == null)
            {
                return;
            }
            if (CurTreeNode.Text == "规划图")
            {
                MessageBox.Show("顶级目录不能删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (CurTreeNode.ImageIndex == 8)
            {
                folder.PARENTID = ((SVGFOLDER)CurTreeNode.Tag).SUID;
                IList folderList = Services.BaseService.GetList("SelectSVGFOLDERByParent", folder);
                if (folderList.Count > 0)
                {
                    MessageBox.Show("选择目录包含子文件夹,不能删除.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                svgFile.PARENTID = ((SVGFOLDER)CurTreeNode.Tag).SUID;
                IList svgList = Services.BaseService.GetList("SelectSVGFILEByParent", svgFile);
                if (svgList.Count > 0)
                {
                    MessageBox.Show("选择目录包含文件,不能删除.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("确定要删除么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Services.BaseService.Delete<SVGFOLDER>((SVGFOLDER)CurTreeNode.Tag);
                    treeView1.Nodes.Remove(CurTreeNode);
                }
            }
                if (CurTreeNode.ImageIndex == 9)
                {
                    if (((SVGFILE)CurTreeNode.Tag).SUID == "ccd27085-ddb6-445b-8e00-44d784f0932c")
                    {                                       
                        return;
                    }
                    if (MessageBox.Show("确定要删除么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Services.BaseService.Delete<SVGFILE>((SVGFILE)CurTreeNode.Tag);
                        treeView1.Nodes.Remove(CurTreeNode);
                    }
                }
        
        }
        public void OpenSvgFolder()
        {
            if(CurTreeNode.Nodes.Count>0){
                return;
            }
            ReLoadData();
        }
        public void OpenSvgFile()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                svgFile.SUID = ((SVGFILE)CurTreeNode.Tag).SUID;
                IList svgList = Services.BaseService.GetList("SelectSVGFILEByKey", svgFile);
                svgFile=(SVGFILE)svgList[0];
                SvgDocument doc = new SvgDocument();
                if(!string.IsNullOrEmpty(svgFile.SVGDATA)){

                    if (svgFile.SUID == "ccd27085-ddb6-445b-8e00-44d784f0932c")
                    {
                        SvgDocument.BkImageLoad = true;
                    }
                    else
                    {
                        SvgDocument.BkImageLoad = false;
                    }
                    doc.LoadXml(svgFile.SVGDATA);
                }
                
                doc.FileName = svgFile.FILENAME;
                doc.SvgdataUid = svgFile.SUID;
                if (this.OnOpenSvgDocument != null)
                {
                    OnOpenSvgDocument(this, svgFile.SUID);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "打开失败");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        public void ReName()
        {
            if (CurTreeNode==null)
            {
                return;
            }
            if (CurTreeNode.Text == "规划图")
            {
                return;
            }
            frmInputDialog f = new frmInputDialog();
            if (CurTreeNode.ImageIndex == 8)
            {
                f.InputStr = ((SVGFOLDER)CurTreeNode.Tag).FOLDERNAME;
            }
            if (CurTreeNode.ImageIndex == 9)
            {
                f.InputStr = ((SVGFILE)CurTreeNode.Tag).FILENAME;
            }
            if(f.ShowDialog()==DialogResult.OK){
                if (CurTreeNode.ImageIndex == 8)
                {
                    ((SVGFOLDER)CurTreeNode.Tag).FOLDERNAME = f.InputStr;
                    Services.BaseService.Update<SVGFOLDER>((SVGFOLDER)CurTreeNode.Tag);
                }
                if (CurTreeNode.ImageIndex == 9)
                {
                    ((SVGFILE)CurTreeNode.Tag).FILENAME = f.InputStr;
                    Services.BaseService.Update("UpdateSVGFILETitle",((SVGFILE)CurTreeNode.Tag));
                }
                CurTreeNode.Text = f.InputStr;
            }
            
        }
        public void Copy()
        {
            if (CurTreeNode.ImageIndex == 2 || CurTreeNode.ImageIndex == 8)
            {
                return;
            }
            frmFileCopy frmFile = new frmFileCopy();
            frmFile.InitData(CurTreeNode.Text);
            if(frmFile.ShowDialog()==DialogResult.OK){
               // CurTreeNode.Text = frmFile.NewFileName;
                string old_uid=((SVGFILE)(CurTreeNode.Tag)).SUID;
                string new_uid = Guid.NewGuid().ToString();
                SVGFILE _svg= Services.BaseService.GetOneByKey<SVGFILE>(old_uid);
                _svg.SUID = new_uid;
                _svg.FILENAME = frmFile.NewFileName;
                Services.BaseService.Create<SVGFILE>(_svg);

                IList list1= Services.BaseService.GetList("SelectglebePropertyBySvgUID",old_uid);
                for (int i = 0; i < list1.Count;i++ )
                {
                    glebeProperty _gle = (glebeProperty)list1[i];
                    _gle.UID = Guid.NewGuid().ToString();
                    _gle.SvgUID = new_uid;
                    Services.BaseService.Create<glebeProperty>(_gle);
                }
                IList list2 = Services.BaseService.GetList("SelectLineInfoBySvgUID", old_uid);
                for (int i = 0; i < list2.Count; i++)
                {
                    LineInfo _line = (LineInfo)list2[i];
                    _line.UID = Guid.NewGuid().ToString();
                    _line.SvgUID = new_uid;
                    Services.BaseService.Create<LineInfo>(_line);
                }
                IList list3 = Services.BaseService.GetList("SelectsubstationBySvgUID", old_uid);
                for (int i = 0; i < list3.Count; i++)
                {
                    substation _sub = (substation)list3[i];
                    _sub.UID = Guid.NewGuid().ToString();
                    _sub.SvgUID = new_uid;
                    Services.BaseService.Create<substation>(_sub);
                }
                TreeNode treeNode = new TreeNode();
                treeNode.Tag = _svg;
                treeNode.ImageIndex = 9;
                treeNode.Text = _svg.FILENAME;
                root.Nodes.Add(treeNode);
                MessageBox.Show("文件拷贝成功。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        public void IninData()
        {
            try
            {
                this.imageList1 = ItopVector.Resource.ResourceHelper.LoadBitmapStrip(base.GetType(), "Itop.ItopVector.Tools.Database.bmp", new Size(16, 16), new Point(0, 0));
                this.treeView1.ImageList = imageList1;
                folder.PARENTID = "1";

                CurTreeNode = root;


                folder.PARENTID = "1";
                IList folderList = Services.BaseService.GetList("SelectSVGFOLDERByParent", folder);
                for (int i = 0; i < folderList.Count; i++)
                {
                    SVGFOLDER f = (SVGFOLDER)folderList[i];
                    TreeNode treeNode = new TreeNode();
                    treeNode.Tag = f;
                    treeNode.ImageIndex = 8;
                    treeNode.Text = f.FOLDERNAME;
                    CurTreeNode.Nodes.Add(treeNode);
                }

                svgFile.PARENTID = "1";
                IList svgList = Services.BaseService.GetList("SelectSVGFILEByParent", svgFile);
                for (int i = 0; i < svgList.Count; i++)
                {
                    SVGFILE f = (SVGFILE)svgList[i];
                    TreeNode treeNode = new TreeNode();
                    treeNode.Tag = f;
                    treeNode.ImageIndex = 9;
                    treeNode.Text = f.FILENAME;
                    CurTreeNode.Nodes.Add(treeNode);
                }
                CurTreeNode.Expand();
            }
            catch(Exception e){
                MessageBox.Show(e.Message);
            }
          
        }
        private void frmFileManager_Load(object sender, EventArgs e)
        {
            SVGFOLDER svgRoot = new SVGFOLDER();
            svgRoot.SUID = "1";
            root.Tag = svgRoot;
            root.Text = "规划图";
            root.ImageIndex = 2;
            treeView1.Nodes.Add(root);
            IninData();
            //InitSvgServer();
            //treeView1.ContextMenuStrip = null;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CurTreeNode = e.Node;
            if(e.Node.ImageIndex==9){
                svgUid = ((SVGFILE)CurTreeNode.Tag).SUID;
            }
        }

        private void 添加文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Add)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddSvgFile();
        }

        private void 添加目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Add)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddSvgFolder();
        }
        public void MenuClose()
        {
            treeView1.ContextMenuStrip = null;
        }
        public string SvgUid
        {
            set { svgUid = value; }
            get { return svgUid; }
        }
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Right){
                if(CurTreeNode.ImageIndex==2){
                    toolStripMenuItem2.Enabled = true;
                    toolStripMenuItem3.Enabled = false;
                    toolStripMenuItem4.Enabled = false;
                    toolStripMenuItem5.Enabled = false;
                    return;
                }
                if (CurTreeNode.ImageIndex == 9)
                {
                    toolStripMenuItem2.Enabled = false;
                    toolStripMenuItem3.Enabled = true;
                    toolStripMenuItem4.Enabled = true;
                    toolStripMenuItem5.Enabled = true;
                    return;
                }
                else
                {
                    toolStripMenuItem2.Enabled = true;
                    toolStripMenuItem3.Enabled = true;
                    toolStripMenuItem4.Enabled = true;
                    toolStripMenuItem5.Enabled = false;
                    return;
                }
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            Open();
        }
    }
}