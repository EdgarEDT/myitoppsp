//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-8-17 8:34:05
//
//********************************************************************************/
using System;
namespace Itop.Domain.RightManager
{
	/// <summary>
	/// 实体类Smmgroup 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Smmgroup
	{
		public Smmgroup()
		{}
		#region Domain
		private string _groupno;
		private string _groupname;
		private string _remark;
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
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Domain
	}
}

