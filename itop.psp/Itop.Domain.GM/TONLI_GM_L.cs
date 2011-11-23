//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-6-19 9:33:41
//
//********************************************************************************/
using System;
namespace Itop.Domain.GM
{
	/// <summary>
	/// 实体类Common_Type 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Common_Type
	{
		public Common_Type()
		{}
		#region 字段
		private string _id="";
		private string _title="";
		private string _content="";
		private string _remark="";
		private DateTime _createtime;
		private string _type="";
		#endregion 字段

		#region 属性
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 类型
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		#endregion 属性
	}
}

