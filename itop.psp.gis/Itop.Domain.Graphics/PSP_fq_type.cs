//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-12-29 15:13:38
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类PSP_fq_type 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PSP_fq_type
	{
		public PSP_fq_type()
		{}
		#region 字段
		private int _id;
		private string _name="";
		private string _col1="";
		private string _col2="";
		#endregion 字段

		#region 属性
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
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
		public string col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		#endregion 属性
	}
}

