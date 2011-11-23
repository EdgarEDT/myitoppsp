
#region ���������ռ�
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
		#region ���췽��
		public CtrlRtfAttachFilesTR()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        private string catygory = "";
        private string fixstr = "";
		#endregion

		#region ��������
        /// <summary>
        /// ��������
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
		/// ��ȡ������"˫�������޸�"��־
		/// </summary>
        /// 
		public bool AllowUpdate
		{
			get { return _bAllowUpdate; }
			set { _bAllowUpdate = value; }
		}

		
	

		/// <summary>
		/// ��ȡGridControl������Դ����������б�
		/// </summary>
		public IList<RtfAttachFiles> ObjectList
		{
			get { return this.treeList1.DataSource as IList<RtfAttachFiles>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public RtfAttachFiles FocusedObject
		{
            get { return treeList1.FocusedNode.TreeList.GetDataRecordByNode(treeList1.FocusedNode)  as RtfAttachFiles; }
		}
		#endregion

		#region �¼�����

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


            WaitDialogForm wait = new WaitDialogForm("", "������������, ���Ժ�...");
            try
            {
                System.Diagnostics.Process.Start(filepath);
            }
            catch { System.Diagnostics.Process.Start(path); }
            wait.Close();
        }
		
		#endregion

		#region ��������
       
		/// <summary>
		/// ��ӡԤ��
		/// </summary>
		public void PrintPreview()
		{
			ComponentPrint.ShowPreview(this.treeList1, "������Ϣ");
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
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
		/// ��Ӷ���
		/// </summary>
		public void AddObject()
		{
			//�����������Ƿ��Ѿ�����
			//if (ObjectList == null)
			//{
			//	return;
			//}
			//�½�����
            if (catygory == "")
            {
                return;
            }
			RtfAttachFiles obj = new RtfAttachFiles();
            obj.C_UID = catygory;
            obj.CreateDate = (DateTime)Services.BaseService.GetObject("SelectSysData", null);

			//ִ����Ӳ���
            using (FrmRtfAttachFilesDialogTR dlg = new FrmRtfAttachFilesDialogTR())
			{
                string parentid = "0";
                if (treeList1.FocusedNode!=null&&treeList1.FocusedNode.GetValue("ParentID").ToString()!="0")
                {
                    MessageBox.Show("ֻ����һ��Ŀ¼����Ӹ���");
                    return;
                }
                else 
                {
                    parentid = FocusedObject.UID;
                }

				dlg.IsCreate = true;    //�����½���־
				dlg.Object = obj;
                dlg.ParentID = parentid;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            //Common.Services.BaseService.Create<RtfAttachFiles>(obj);
            RefreshData();
            ////���¶�����뵽������
            //ObjectList.Add(obj);

            ////ˢ�±�񣬲��������ж�λ���¶����ϡ�
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
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{
            if (catygory == "")
            {
                return;
            }
			//��ȡ�������
			RtfAttachFiles obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            if (obj.ParentID== "0")
            {
               return;
            }
			//���������һ������
			RtfAttachFiles objCopy = new RtfAttachFiles();
			DataConverter.CopyTo<RtfAttachFiles>(obj, objCopy);

			//ִ���޸Ĳ���
            using (FrmRtfAttachFilesDialogTR dlg = new FrmRtfAttachFilesDialogTR())
			{
				dlg.Object = objCopy;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            //Common.Services.BaseService.Update<RtfAttachFiles>(objCopy);
            RefreshData();

            ////�ø������½������
            DataConverter.CopyTo<RtfAttachFiles>(objCopy, obj);
            ////ˢ�±��
            //treeList1.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
            if (catygory == "")
            {
                return;
            }
			//��ȡ�������
			RtfAttachFiles obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            if (obj.ParentID=="0")
            {
                return;
            }

			//����ȷ��
			if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
			{
				return;
			}

			//ִ��ɾ������
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
            ////��ס��ǰ����������
            //int iOldHandle = this.treeList1.FocusedNode.Id;
            ////��������ɾ��
            //ObjectList.Remove(obj);
            ////ˢ�±��
            //treeList1.RefreshDataSource();
            ////�����µĽ���������

            //this.treeList1.EndUpdate();
		}
		#endregion

        private void gridControl_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            switch (e.Button.Hint)
            { 
                case "���":
                    AddObject();
                    break;


                case "ɾ��":
                    DeleteObject();
                    break;


                case "�޸�":
                    UpdateObject();
                    break;


                case "��ӡ":
                    PrintPreview();
                    break;
            }
        }

        private void ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddObject();
        }

        private void �޸�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateObject();
        }

        private void ɾ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteObject();
        }

        private void ��ӡToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPreview();
        }



	}
}
