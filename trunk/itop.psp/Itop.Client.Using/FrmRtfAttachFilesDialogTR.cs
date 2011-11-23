
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Itop.Domain.Layouts;
using Itop.Common;
using Itop.Client.Common;
using System.IO;
using Itop.Client.Base;
namespace Itop.Client.Using
{
	public partial class FrmRtfAttachFilesDialogTR : FormBase
	{

		#region �ֶ�
		protected bool _isCreate = false;
		protected RtfAttachFiles _obj = null;
        private string fname = "";
        string filePath = "";
        public string ParentID
        {
            set
            {
                if (_obj!=null)
                {
                    _obj.ParentID = value;
                }

            }

        }
		#endregion

		#region ���췽��
		public FrmRtfAttachFilesDialogTR()
		{
			InitializeComponent();
		}
		#endregion

		#region ��������
		/// <summary>
		/// true:��������false:�޸Ķ���
		/// </summary>
		public bool IsCreate
		{
			get { return _isCreate; }
			set { _isCreate = value; }
		}

		/// <summary>
		/// ����Ķ���
		/// </summary>
		public RtfAttachFiles Object
		{
			get { return _obj; }
			set { _obj = value; }
		}
		#endregion

		#region �¼�����
		private void FrmRtfAttachFilesDialogTR_Load(object sender, EventArgs e)
		{
			IList<RtfAttachFiles> list = new List<RtfAttachFiles>();
			list.Add(Object);
			this.vGridControl.DataSource = list;

            if (!IsCreate)
            {
                fname = _obj.FileName;
            }
		}

		private void btnOK_Click(object sender, EventArgs e)
		{

			if (!InputCheck())
			{
				return;
			}

			if (SaveRecord())
			{
				DialogResult = DialogResult.OK;
			}
		}
		#endregion

		#region ��������
		protected bool InputCheck()
		{
            if (_obj.Des == "")
            {
                MsgBox.Show("���ⲻ��Ϊ�գ�");
                return false;
            }

            if (_obj.FileName == "")
            {
                MsgBox.Show("�ļ�û��ѡ��");
                return false;
            }

			return true;
		}

		protected bool SaveRecord()
		{
			//����/�޸� ����
			try
			{
				if (IsCreate)
				{

                    FileStream fsBLOBFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, System.IO.FileShare.ReadWrite);
                    Byte[] bytBLOBData = new Byte[fsBLOBFile.Length];
                    int flength = bytBLOBData.Length;
                    fsBLOBFile.Read(bytBLOBData, 0, flength);
                    fsBLOBFile.Close();
                    _obj.FileByte = bytBLOBData;
                    Services.BaseService.Create<RtfAttachFiles>(_obj);
					
				}
				else
				{
                    if (fname != _obj.FileName)
                    {
                        FileStream fsBLOBFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, System.IO.FileShare.ReadWrite);
                        Byte[] bytBLOBData = new Byte[fsBLOBFile.Length];
                        int flength = bytBLOBData.Length;
                        fsBLOBFile.Read(bytBLOBData, 0, flength);
                        fsBLOBFile.Close();
                        _obj.FileByte = bytBLOBData;
                        Services.BaseService.Update<RtfAttachFiles>(_obj);
                    }
                    else
                    {
                        Services.BaseService.Update("UpdateRtfAttachFilesByDes", _obj);                 
                    }
				}
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return false ;
			}

			//�����ѳɹ�
			return true;
		}
		#endregion

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            filePath="";
            string fileName = "";
            string extName = "";
            FileStream fsBLOBFile=null;
			Byte[] bytBLOBData=null;
            int flength = 0;


            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                extName = Path.GetExtension(filePath);
                if (extName.Length > 0)
                    extName = extName.Substring(1, extName.Length - 1);


                fileName = Path.GetFileName(filePath);
                fsBLOBFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, System.IO.FileShare.ReadWrite);
                bytBLOBData = new Byte[fsBLOBFile.Length];
                flength = bytBLOBData.Length;
                //fsBLOBFile.Read(bytBLOBData, 0, flength);
                fsBLOBFile.Close();

                _obj.FileName = fileName;
                _obj.FileSize = flength;
                _obj.FileType = extName;
            }

            repositoryItemButtonEdit1.NullText = fileName;

            textBox1.Focus();

        }
	}
}
