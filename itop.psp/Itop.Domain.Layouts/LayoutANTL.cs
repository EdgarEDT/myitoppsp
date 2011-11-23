//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-11-16 10:34:18
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// ʵ����LayoutANTL ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class LayoutANTL
	{
        public LayoutANTL()
		{}
		#region �ֶ�
		private string _uid=Guid.NewGuid().ToString();
		private string _layoutname="";
		private DateTime _createdate;
		private string _creater="";
		private string _remark="";
        private string _createrName = "";
		#endregion �ֶ�

		#region ����
		/// <summary>
		/// �滮��ʶ
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// �滮����
		/// </summary>
		public string LayoutName
		{
			set{ _layoutname=value;}
			get{return _layoutname;}
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
		/// ������
		/// </summary>
		public string Creater
		{
			set{ _creater=value;}
			get{return _creater;}
		}
        /// <summary>
        /// ��������
        /// </summary>
        public string CreaterName
        {
            set { _createrName = value; }
            get { return _createrName; }
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

