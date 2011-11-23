//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-11-18 8:48:47
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// ʵ����RtfAttachFiles ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class RtfAttachFiles
	{
		public RtfAttachFiles()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _c_uid="";
		private string _des="";
		private string _filetype="";
        private string _filename = "";
		private decimal _filesize;
		private byte[] _filebyte;
		private DateTime _createdate;
        private string _ParentID = "";
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// ������ʶ
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string C_UID
		{
			set{ _c_uid=value;}
			get{return _c_uid;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Des
		{
			set{ _des=value;}
			get{return _des;}
		}

        /// <summary>
        /// �ļ�����
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
		/// <summary>
		/// �ļ�����
		/// </summary>
		public string FileType
		{
			set{ _filetype=value;}
			get{return _filetype;}
		}
		/// <summary>
		/// �ļ���С
		/// </summary>
		public decimal FileSize
		{
			set{ _filesize=value;}
			get{return _filesize;}
		}
		/// <summary>
		/// ���ļ�
		/// </summary>
		public byte[] FileByte
		{
			set{ _filebyte=value;}
			get{return _filebyte;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
        /// <summary>
        /// ��������
        /// </summary>
        public string ParentID
        {
            set { _ParentID = value; }
            get { return _ParentID; }
        }
		#endregion ����
	}
}

