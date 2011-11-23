using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Server.Interface;
using Itop.Common;
using Itop.Domain.RightManager;
using Itop.Client.Resources;
namespace Itop.RightManager.UI { 
    public partial class FrmSmmprogTree : Itop.Client.Base.FormBase {
        public FrmSmmprogTree() {
            InitializeComponent();
        }
        public bool Execute() {

            this.ShowInTaskbar = false;
            ShowDialog();
            return true;
        }       

        //public bool Execute(string projectUID)
        //{
        //    this.ShowInTaskbar = false;
        //    this.StartPosition = FormStartPosition.CenterParent;
        //    ShowDialog();
        //    return true;
        //}
        private IBaseService smmprogService;

        public IBaseService SmmprogService {
            get {
                if (smmprogService == null) {
                    smmprogService = RemotingHelper.GetRemotingService<IBaseService>();
                }
                if (smmprogService == null) MsgBox.Show("IBaseService服务没有注册");
                return smmprogService; }
        }
        DataTable smmprogTable;
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            IList list2 = SmmprogService.GetList("SelectSmmprogByMeIcoall", null);

            DataTable dt_list = DataConverter.ToDataTable(list2);

            treeView1.ImageList = ImageListRes.GetimageList(16, dt_list);
            treeView1.ImageList.Images.Add("forep", imageList1.Images[0]);
            treeView1.ImageList.Images.Add("Icsclient", imageList1.Images[1]);
            treeView1.ImageList.Images.Add("ruit_appile", imageList1.Images[2]);

            TreeNode parentNode = treeView1.Nodes.Add("顶层菜单");
            parentNode.ImageKey = "Icsclient";
            parentNode.SelectedImageKey = "Icsclient";

            IList list = SmmprogService.GetList<Smmprog>();
            if (list != null && list.Count > 0) {
                smmprogTable = DataConverter.ToDataTable(list, typeof(Smmprog));
                ExpandNode(parentNode, string.Empty);
            }
        }
        private void ExpandNode(TreeNode parentNode ,string parentid)
        {            
            DataRow[] rows = smmprogTable.Select(string.Format("parentid='{0}'",parentid));
            foreach(DataRow row in rows)
            {
                TreeNode node= parentNode.Nodes.Add(row["progname"].ToString());
                
                node.Tag = DataConverter.RowToObject<Smmprog>(row);
                node.Name = row["progid"].ToString();
                if (row["progname"].ToString()=="回收")
                {
                    node.ImageKey = "ruit_appile";
                    node.SelectedImageKey = "ruit_appile";
                }
                else
                {
                  node.ImageKey = row["ProgIco"].ToString();
                  node.SelectedImageKey = row["ProgIco"].ToString();
                }
                node.Nodes.Add("");
            }
        }

        private void btAdd_Click(object sender, EventArgs e) {
            Smmprog prog = new Smmprog();
            prog.ProgModuleType = "0";
            
            if (treeView1.SelectedNode.Level == 0) {
                prog.ParentId = "";
            }
            else if(treeView1.SelectedNode.Tag is Smmprog)
            {
                prog.ParentId = (treeView1.SelectedNode.Tag as Smmprog).ProgId;                
            }
            FrmSmmprogEdit dlg = new FrmSmmprogEdit();
            dlg.Smmprog = prog;
            dlg.ShowInTaskbar = false;
            if (dlg.ShowDialog() == DialogResult.OK) {
                try {
                    //SmmprogService.Create<Smmprog>(prog);
                    SmmprogService.Create("InsertSmmprog", prog);

                    TreeNode node = treeView1.SelectedNode.Nodes.Add(prog.ProgName);
                    node.Tag = prog;
                } catch { }
            }
        }

        private void btEdit_Click(object sender, EventArgs e) {
            TreeNode node = treeView1.SelectedNode;
            
            if (node != null && node.Tag is Smmprog) {
                FrmSmmprogEdit dlg = new FrmSmmprogEdit();
                dlg.Smmprog = node.Tag as Smmprog;
                if (dlg.ShowDialog() == DialogResult.OK) {
                    try {
                        SmmprogService.Update<Smmprog>(dlg.Smmprog);
                        node.Text = dlg.Smmprog.ProgName;
                    } catch (Exception err) { MessageBox.Show(err.Message); }
                }
            }
        }

        private void btDelete_Click(object sender, EventArgs e) {
            TreeNode node = treeView1.SelectedNode;
            if (node != null && node.Tag is Smmprog) {
                try {
                    Smmprog prog =node.Tag as Smmprog;
                    Hashtable hashtable1 = new Hashtable();
                    
                    if (smmprogService.GetRowCount<Smmprog>(prog)>0) {
                        MsgBox.Show("有下级目录或者模块,不可删除!");
                        return;
                    }
                    if (MsgBox.ShowYesNo("是否确认删除[" + prog.ProgName + "]") == DialogResult.Yes) {
                        SmmprogService.Delete<Smmprog>(node.Tag as Smmprog);
                        
                        treeView1.SelectedNode.Remove();
                    }
                } catch { }
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
          
            if (e.Node.Name != string.Empty) {
                e.Node.Nodes.Clear();
                ExpandNode(e.Node, e.Node.Name);
                e.Node.Name = string.Empty;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            //Itop.Client.MIS.MainFormInterface.RefreshMainMenu();
            Itop.Client.MIS.MFrmConsole.InitData();
            


        
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e) {
            TreeNode node = e.Data.GetData("TreeNode") as TreeNode;
            Point point1 = this.treeView1.PointToClient(new Point(e.X, e.Y));
            TreeNode node2 = this.treeView1.GetNodeAt(point1.X, point1.Y) as TreeNode;
            if (CheckNode(node2,node)) {
                Smmprog prog2 = node2.Tag as Smmprog;
                Smmprog prog1 = node.Tag as Smmprog;
                string parentid = string.Empty;
                if (prog2 != null) parentid = prog2.ProgId;
                try {
                    prog1.ParentId = parentid;
                    ArrayList list = new ArrayList();
                    list.Add(prog1);
                    SmmprogService.Update<Smmprog>(prog1);
                    node.Remove();
                    node2.Nodes.Add(node);
                } catch (Exception err) { MessageBox.Show(err.Message); }
            }
        }
        private bool CheckNode(TreeNode parentNode, TreeNode childNode) {
            bool flag = true;
            if (childNode == null || parentNode == null || childNode.Parent == parentNode || childNode == parentNode)
                return false;

            flag = !ExistChildNode(childNode, parentNode);

            return flag;
        }
        private bool ExistChildNode(TreeNode parent, TreeNode child) {

            if (parent.Nodes.Contains(child)) return true;

            foreach (TreeNode node in parent.Nodes) {
                if(ExistChildNode(node,child))return true;                
            }
            return false;
        }
        

        private void treeView1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent("TreeNode")) {
                e.Effect = DragDropEffects.Move;
                draging = true;
            } else {
                e.Effect = DragDropEffects.None;
            }
        }
        bool draging = false;
        bool beginDrag = false;
        private void treeView1_DragOver(object sender, DragEventArgs e) {

            if (this.draging) {
                Point point1 = this.treeView1.PointToClient(new Point(e.X, e.Y));
                TreeNode node1 = this.treeView1.GetNodeAt(point1.X, point1.Y) as TreeNode;
                if (node1 != null ) {
                    this.treeView1.SelectedNode = node1;
                }
            }
        }

        private void treeView1_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left && this.beginDrag) {
                TreeNode node2 = this.treeView1.GetNodeAt(e.X, e.Y) as TreeNode;
                if (node2 != null) {
                    this.treeView1.DoDragDrop(new DataObject("TreeNode", node2), DragDropEffects.Move);
                    draging = true;
                }
                this.beginDrag = false;
            }
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e) {
            draging = false;
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.beginDrag = false;

                TreeNode node2 =  this.treeView1.GetNodeAt(e.X, e.Y) as TreeNode;
                if (node2 != null && node2.Level!=0) {
                this.beginDrag = true;
            }
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}