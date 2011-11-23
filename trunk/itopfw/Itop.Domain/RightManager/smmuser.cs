//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-8-17 8:34:06
//
//********************************************************************************/
using System;
namespace Itop.Domain.RightManager
{
	/// <summary>
	/// 实体类Smmuser 。(属性说明自动提取数据库字段的描述信息)
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
		/// 用户号唯一
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
		/// 用户名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 过期时间
		/// </summary>
		public string ExpireDate
		{
			set{ _expiredate=value;}
			get{return _expiredate;}
		}
		/// <summary>
		/// 是否可用
		/// </summary>
		public string Disableflg
		{
			set{ _disableflg=value;}
			get{return _disableflg;}
		}
		/// <summary>
		/// 最后一次登录时间
		/// </summary>
		public string Lastlogon
		{
			set{ _lastlogon=value;}
			get{return _lastlogon;}
		}
		#endregion Domain
	}
}

