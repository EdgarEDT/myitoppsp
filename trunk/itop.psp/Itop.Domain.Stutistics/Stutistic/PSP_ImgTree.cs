//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-9-8 10:16:28
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistics
{
	/// <summary>
	/// 实体类PSP_ImgTree 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PSP_ImgTree
	{
		public PSP_ImgTree()
		{}
		#region 字段
		private string _uid="";
		private string _name="";
		private string _pid="";
		private string _remark="";
		#endregion 字段

		#region 属性
		/// <summary>
		/// 
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion 属性
	}
}

