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
	/// 实体类Smugroup 。(属性说明自动提取数据库字段的描述信息)
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
		/// 唯一标识
		/// </summary>
		public string Suid
		{
			set{ _suid=value;}
			get{return _suid;}
		}
		/// <summary>
		/// 组ID
		/// </summary>
		public string Groupno
		{
			set{ _groupno=value;}
			get{return _groupno;}
		}
        /// <summary>
        /// 组名称
        /// </summary>
        public string Groupname
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
		/// <summary>
		/// 用户ID
		/// </summary>
		public string Userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		#endregion Domain
	}
}

