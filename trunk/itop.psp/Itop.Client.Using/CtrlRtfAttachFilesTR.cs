
#region 引用命名空间
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Itop.Common;
using Itop.Client.Common;
using Itop.Domain.Layouts;
using System.IO;
using DevExpress.Utils;
using DevExpress.XtraTreeList.Nodes;
#endregion

namespace Itop.Client.Using
{
	public partial class CtrlRtfAttachFilesTR : UserControl
	{
		#region 构造方法
		public CtrlRtfAttachFilesTR()
		{
			InitializeComponent();
		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        private string catygory = "";
        private string fixstr = "";
		#endregion

		#region 公共属性
        /// <summary>
        /// 返回行数
        /// </summary>
        public int RowCount
        {
            get
            {
                int count = 0;
                string conn=" C_UID='"+catygory+"' and ParentID!='0' and ParentID!='' and ParentID is not null";
                IList<RtfAttachFiles> list = Services.BaseService.GetList<RtfAttachFiles>("SelectRtfAttachFilesByWhere", conn);
                if (list!=null)
                {
                    count = list.Count;
                }
                return count;
            }
        }
        
        public string Category
        {
            get { return catygory; }
            set { catygory = value; }
        }
        public string FixStr
        {
            get { return fixstr; }
            set { fixstr = value; }
        }

		/// <summary>
		/// 获取或设置"双击允许修改"标志
		/// </summary>
        /// 
		public bool AllowUpdate
		{
			get { return _bAllowUpdate; }
			set { _bAllowUpdate = value; }
		}

		
	

		/// <summary>
		/// 获取GridControl的数据源，即对象的列表
		/// </summary>
		public IList<RtfAttachFiles> ObjectList
		{
			get { return this.treeList1.DataSource as IList<RtfAttachFiles>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public RtfAttachFiles FocusedObject
		{
            get { return treeList1.FocusedNode.TreeList.GetDataRecordByNode(treeList1.FocusedNode)  as RtfAttachFiles; }
		}
		#endregion

		#region 事件处理

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            if (FocusedObject == null)
                return;
            if (FocusedObject.ParentID=="0")
            {
                return;
            }

            string path = Application.StartupPath + "\\BlogData";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            RtfAttachFiles raf = Services.BaseService.GetOneByKey<RtfAttachFiles>(FocusedObject.UID);
            string filepath = path + "\\" + raf.FileName;
            FrmCommon.getDoc(raf.FileByte, filepath);


            WaitDialogForm wait = new WaitDialogForm("", "正在下载数据, 请稍候...");
            try
            {
                System.Diagnostics.Process.Start(filepath);
            }
            catch { System.Diagnostics.Process.Start(path); }
            wait.Close();
        }
		
		#endregion

		#region 公共方法
       
		/// <summary>
		/// 打印预览
		/// </summary>
		public void PrintPreview()
		{
			ComponentPrint.ShowPreview(this.treeList1, "附件信息");
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool RefreshData()
		{
			try
			{
				//IList<RtfAttachFiles> list = Services.BaseService.GetStrongList<RtfAttachFiles>();

                if (catygory=="")
                {
                    this.treeList1.DataSource = null;
                    return false;
                }
                string conn = " C_UID='" + FixStr + "' or  C_UID='" + catygory + "'";
                IList<RtfAttachFiles> list = Services.BaseService.GetList<RtfAttachFiles>("SelectRtfAttachFilesByWhere", conn);
          
                //IList<RtfAttachFiles> list = Services.BaseService.GetList<RtfAttachFiles>("SelectRtfAttachFilesByCategory", catygory);
				this.treeList1.DataSource = list;
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return false;
			}

			return true;
		}

		/// <summary>
		/// 添加对象
		/// </summary>
		public void AddObject()
		{
			//检查对象链表是否已经加载
			//if (ObjectList == null)
			//{
			//	return;
			//}
			//新建对象
            if (catygory == "")
            {
                return;
            }
			RtfAttachFiles obj = new RtfAttachFiles();
            obj.C_UID = catygory;
            obj.CreateDate = (DateTime)Services.BaseService.GetObject("SelectSysData", null);

			//执行添加操作
            using (FrmRtfAttachFilesDialogTR dlg = new FrmRtfAttachFilesDialogTR())
			{
                string parentid = "0";
                if (treeList1.FocusedNode!=null&&treeList1.FocusedNode.GetValue("ParentID").ToString()!="0")
                {
                    MessageBox.Show("只能在一级目录下添加附件");
                    return;
                }
                else 
                {
                    parentid = FocusedObject.UID;
                }

				dlg.IsCreate = true;    //设置新建标志
				dlg.Object = obj;
                dlg.ParentID = parentid;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            //Common.Services.BaseService.Create<RtfAttachFiles>(obj);
            RefreshData();
            ////将新对象加入到链表中
            //ObjectList.Add(obj);

            ////刷新表格，并将焦点行定位到新对象上。
            //treeList1.RefreshDataSource();
            //Set_Focus(obj.UID);
			
		}
        private void Set_Focus(string ID)
        {
            if (ID != null)
            {
                TreeListNode tempnode = treeList1.FindNodeByKeyID(ID);
                treeList1.FocusedNode = tempnode;
            }

        }
		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
            if (catygory == "")
            {
                return;
            }
			//获取焦点对象
			RtfAttachFiles obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            if (obj.ParentID== "0")
            {
               return;
            }
			//创建对象的一个副本
			RtfAttachFiles objCopy = new RtfAttachFiles();
			DataConverter.CopyTo<RtfAttachFiles>(obj, objCopy);

			//执行修改操作
            using (FrmRtfAttachFilesDialogTR dlg = new FrmRtfAttachFilesDialogTR())
			{
				dlg.Object = objCopy;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            //Common.Services.BaseService.Update<RtfAttachFiles>(objCopy);
            RefreshData();

            ////用副本更新焦点对象
            DataConverter.CopyTo<RtfAttachFiles>(objCopy, obj);
            ////刷新表格
            //treeList1.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
            if (catygory == "")
            {
                return;
            }
			//获取焦点对象
			RtfAttachFiles obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            if (obj.ParentID=="0")
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
				Services.BaseService.Delete<RtfAttachFiles>(obj);
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return;
			}
            RefreshData();
            //this.treeList1.BeginUpdate();
            ////记住当前焦点行索引
            //int iOldHandle = this.treeList1.FocusedNode.Id;
            ////从链表中删除
            //ObjectList.Remove(obj);
            ////刷新表格
            //treeList1.RefreshDataSource();
            ////设置新的焦点行索引

            //this.treeList1.EndUpdate();
		}
		#endregion

        private void gridControl_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            switch (e.Button.Hint)
            { 
                case "添加":
                    AddObject();
                    break;


                case "删除":
                    DeleteObject();
                    break;


                case "修改":
                    UpdateObject();
                    break;


                case "打印":
                    PrintPreview();
                    break;
            }
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddObject();
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateObject();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteObject();
        }

        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreview();
        }



	}
}
