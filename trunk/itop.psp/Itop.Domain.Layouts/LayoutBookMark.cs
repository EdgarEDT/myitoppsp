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
	/// ʵ����LayoutBookMark ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class LayoutBookMark
	{
		public LayoutBookMark()
		{}
		#region �ֶ�
        private string _uid = Guid.NewGuid().ToString().Replace("-","_");
		private string _layoutid="";
		private string _MarkName="";
		private string _MarkDisc="";
        private string _MarkText="";
		private string _MarkType="";
		private int _StartP=0;
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
		public string MarkName
		{
			set{ _MarkName=value;}
			get{return _MarkName;}
		}
		/// <summary>
		/// �ϼ��½�
		/// </summary>
		public string MarkDisc
		{
			set{ _MarkDisc=value;}
			get{return _MarkDisc;}
		}
		/// <summary>
		/// �½�����
		/// </summary>
        public string MarkText
		{
			set{ _MarkText=value;}
			get{return _MarkText;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string MarkType
		{
			set{ _MarkType=value;}
			get{return _MarkType;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public int StartP
		{
			set{ _StartP=value;}
			get{return _StartP;}
		}
		#endregion ����
	}
}

