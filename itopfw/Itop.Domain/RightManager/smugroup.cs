//********************************************************************************/
//
//�˴�����Itop.NET�����������Զ�����.
//����ʱ��:2006-8-17 8:34:06
//
//********************************************************************************/
using System;
namespace Itop.Domain.RightManager
{
	/// <summary>
	/// ʵ����Smugroup ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Smugroup
	{
		public Smugroup()
		{}
		#region Domain
		private string _suid=Guid.NewGuid().ToString();
		private string _groupno;
		private string _userid;
        private string _groupname;
		/// <summary>
		/// Ψһ��ʶ
		/// </summary>
		public string Suid
		{
			set{ _suid=value;}
			get{return _suid;}
		}
		/// <summary>
		/// ��ID
		/// </summary>
		public string Groupno
		{
			set{ _groupno=value;}
			get{return _groupno;}
		}
        /// <summary>
        /// ������
        /// </summary>
        public string Groupname
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
		/// <summary>
		/// �û�ID
		/// </summary>
		public string Userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		#endregion Domain
	}
}

