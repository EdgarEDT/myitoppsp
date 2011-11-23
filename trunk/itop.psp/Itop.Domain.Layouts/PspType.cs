//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2007-1-4 14:08:32
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// ʵ����PspType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PspType
	{
		public PspType()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private string _parentid="";
		private DateTime _createdate;
		private byte[] _contents;
		private string _remark="";
        private string _col1 = "";
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// 
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// �����ID
		/// </summary>
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
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
		/// ����
		/// </summary>
		public byte[] Contents
		{
			set{ _contents=value;}
			get{return _contents;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}

        public string Col1
        {
            set { _col1 = value; }
            get { return _col1; }
        }
		#endregion ����
	}
}

