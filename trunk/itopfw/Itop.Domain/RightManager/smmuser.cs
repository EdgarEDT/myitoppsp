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
	/// ʵ����Smmuser ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Smmuser 
	{
		public Smmuser()
		{
            _disableflg = "0";
        }
		#region Domain
		private string _userid;
		private string _username;
		private string _password;
		private string _expiredate;
		private string _disableflg;
		private string _lastlogon;
        private string _remark;
		/// <summary>
		/// �û���Ψһ
		/// </summary>
        /// 

        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }


		public string Userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// �û���
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string ExpireDate
		{
			set{ _expiredate=value;}
			get{return _expiredate;}
		}
		/// <summary>
		/// �Ƿ����
		/// </summary>
		public string Disableflg
		{
			set{ _disableflg=value;}
			get{return _disableflg;}
		}
		/// <summary>
		/// ���һ�ε�¼ʱ��
		/// </summary>
		public string Lastlogon
		{
			set{ _lastlogon=value;}
			get{return _lastlogon;}
		}
		#endregion Domain
	}
}

