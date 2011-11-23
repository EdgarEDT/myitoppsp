
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
using Itop.Domain.RightManager;
using System.Configuration;
using System.Runtime.InteropServices;
#endregion

namespace Itop.Client.Layouts
{
	public partial class CtrlLayout : UserControl
	{
		#region ���췽��
		public CtrlLayout()
		{
			InitializeComponent();
		}
		#endregion

		#region �ֶ�
		protected bool _bAllowUpdate = true;
        private VsmdgroupProg vp = new VsmdgroupProg();
        private bool visu = false;
		#endregion

		#region ��������




        string Path = Application.StartupPath + "\\setting.ini";

        [DllImport("kernel32")]

        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        [DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //дINI�ļ� 
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.Path);

        }

        //��ȡINI�ļ�ָ�� 
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.Path);
            return temp.ToString();
        } 



        public VsmdgroupProg RightObject
        {
            set { vp = value; }
        }

        public bool Visu
        {
            set { visu = value; }
        }

		/// <summary>
		/// ��ȡ������"˫�������޸�"��־
		/// </summary>
		public bool AllowUpdate
		{
			get { return _bAllowUpdate; }
			set { _bAllowUpdate = value; }
		}

		/// <summary>
		/// ��ȡGridControl����
		/// </summary>
		public GridControl GridControl
		{
			get { return gridControl; }
		}

		/// <summary>
		/// ��ȡGridView����
		/// </summary>
		public GridView GridView
		{
			get { return gridView; }
		}

		/// <summary>
		/// ��ȡGridControl������Դ����������б�
		/// </summary>
		public IList<Layout> ObjectList
		{
			get { return this.gridControl.DataSource as IList<Layout>; }
		}

		/// <summary>
		/// ��ȡ������󣬼�FocusedRow
		/// </summary>
		public Layout FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as Layout; }
		}
		#endregion

		#region �¼�����
        //˫���޸��¼�
		private void gridView_DoubleClick(object sender, EventArgs e)
		{
			// �ж�"˫�������޸�"��־ 
            //if (!AllowUpdate)
            //{
            //    return;
            //}

            ////���������ڵ�Ԫ���У���༭�������
            //Point point = this.gridControl.PointToClient(Control.MousePosition);
            //if (GridHelper.HitCell(this.gridView, point))
            //{
            //    UpdateObject();
            //}
            if (FocusedObject != null)
            {
                FrmLayoutContents dlg = new FrmLayoutContents();
                dlg.RightObject = vp;
                dlg.LayoutUID = FocusedObject.UID;
                dlg.ShowDialog();
            }

		}
		#endregion

		#region ��������
		/// <summary>
		/// ��ӡԤ��
		/// </summary>
		public void PrintPreview()
		{
			ComponentPrint.ShowPreview(this.gridControl, this.gridView.GroupPanelText);
		}

		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
		public bool RefreshData()
		{
			try
			{
                IList<Layout> list = Services.BaseService.GetList<Layout>("SelectLayoutListByCreater",Itop.Client.MIS.ProgUID);
				this.gridControl.DataSource = list;
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
			if (ObjectList == null)
			{
				return;
			}
			//�½�����
			Layout obj = new Layout();
            obj.UID = Guid.NewGuid().ToString() + "|" + vp.ProjectUID;
            obj.Creater = vp.ProjectUID;// Itop.Client.MIS.UserNumber;
            //obj.CreaterName = Itop.Client.MIS.UserName;
            try
            {
                obj.CreateDate = (DateTime)Services.BaseService.GetObject("SelectSysData", null);
            }
            catch { }
			//ִ����Ӳ���
			using (FrmLayoutDialog dlg = new FrmLayoutDialog())
			{
				dlg.IsCreate = true;    //�����½���־
				dlg.Object = obj;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//���¶�����뵽������
			ObjectList.Add(obj);
            //////LayoutType lt1 = Services.BaseService.GetOneByKey<LayoutType>(obj.UID);
            //////if (lt1 == null)
            //////{
            //////    lt1 = new LayoutType();
            //////    lt1.UID = obj.UID;
            //////    LayoutType lt2 = Services.BaseService.GetOneByKey<LayoutType>("LayoutModule");
            //////    lt1.ExcelData = lt2.ExcelData;
            //////    Services.BaseService.Create<LayoutType>(lt1);
            //////}




			//ˢ�±�񣬲��������ж�λ���¶����ϡ�
			gridControl.RefreshDataSource();
			GridHelper.FocuseRow(this.gridView, obj);
		}

		/// <summary>
		/// �޸Ľ������
		/// </summary>
		public void UpdateObject()
		{
			//��ȡ�������
			Layout obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//���������һ������
			Layout objCopy = new Layout();
			DataConverter.CopyTo<Layout>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmLayoutDialog dlg = new FrmLayoutDialog())
			{
				dlg.Object = objCopy;   //�󶨸���
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//�ø������½������
			DataConverter.CopyTo<Layout>(objCopy, obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// ɾ���������
		/// </summary>
		public void DeleteObject()
		{
			//��ȡ�������
			Layout obj = FocusedObject;
			if (obj == null)
			{
				return;
			}



            IList<LayoutContent> Li=  Services.BaseService.GetList<LayoutContent>("SelectLayoutContentByLayoutID",obj.UID);

            if (Li.Count > 0)
            {
                MsgBox.Show("�ù滮�����Ѿ������½ڣ��޷�ɾ��");
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
				Services.BaseService.Delete<Layout>(obj);


                Services.BaseService.Update("DeleteLayoutType", obj.UID);
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return;
			}

			this.gridView.BeginUpdate();
			//��ס��ǰ����������
			int iOldHandle = this.gridView.FocusedRowHandle;
			//��������ɾ��
			ObjectList.Remove(obj);
			//ˢ�±��
			gridControl.RefreshDataSource();
			//�����µĽ���������
			GridHelper.FocuseRowAfterDelete(this.gridView, iOldHandle);
			this.gridView.EndUpdate();
		}
		#endregion

      

        private void gridControl_Load(object sender, EventArgs e)
        {
            //RefreshData();

            if (!visu)
            {
                colCreater.Visible = false;
                colRemark.Visible = false;
            
            }


            try 
            { 
                //string fileName = Application.StartupPath + @"\Itop.exe.config";
                //if (!System.IO.File.Exists(fileName)) return;

                

                //Configuration config = null;
                //config = ConfigurationManager.OpenExeConfiguration(fileName);

                //string serverAddress = config.AppSettings.Settings["serverAddress"].Value;
                //string serverPort = config.AppSettings.Settings["serverPort"].Value;
                //string serverProtocol = config.AppSettings.Settings["serverProtocol"].Value;

                Itop.Server.Interface.IConfigService ic = RemotingHelper.GetRemotingService<Itop.Server.Interface.IConfigService>();
                Itop.Domain.DataConfig dc = ic.GetDataConfig();

                IniWriteValue("���ݿ�", "����", "Provider=SQLOLEDB.1;Password=" + dc.Password + ";Persist Security Info=True;User ID=" + dc.Userid + ";Initial Catalog=" + dc.Database + ";Data Source=" + dc.Datasource + ";");
                ;

            }
            catch { }
        }
	}
}
