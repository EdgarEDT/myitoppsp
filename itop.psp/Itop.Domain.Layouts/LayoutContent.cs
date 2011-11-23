//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-11-16 14:22:54
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// ʵ����LayoutContent ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class LayoutContent
	{
		public LayoutContent()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _layoutid="";
		private string _chaptername="";
		private string _parentid="";
		private byte[] _contents;
		private string _contenttype="";
		private string _remark="";
        private DateTime _createDate = DateTime.Now;
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// �½ڱ�ʶ
		/// </summary>
        /// 
        public DateTime CreateDate
        {
            set { _createDate = value; }
            get { return _createDate; }
        }

		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// �滮��ʶ
		/// </summary>
		public string LayoutID
		{
			set{ _layoutid=value;}
			get{return _layoutid;}
		}
		/// <summary>
		/// �½�����
		/// </summary>
		public string ChapterName
		{
			set{ _chaptername=value;}
			get{return _chaptername;}
		}
		/// <summary>
		/// �ϼ��½�
		/// </summary>
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// �½�����
		/// </summary>
		public byte[] Contents
		{
			set{ _contents=value;}
			get{return _contents;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string ContentType
		{
			set{ _contenttype=value;}
			get{return _contenttype;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion ����
	}
}

