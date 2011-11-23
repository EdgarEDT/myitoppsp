//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-8-21 15:02:30
//
//********************************************************************************/
using System;
namespace Itop.Domain
{
	/// <summary>
	/// 实体类SAppProps 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class SAppProps
	{
		public SAppProps()
		{}
		#region Domain
		private int _propid;
		private string _propname;
		private string _propvalue;
		private string _proptype;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int PropId
		{
			set{ _propid=value;}
			get{return _propid;}
		}
		/// <summary>
		/// 参数名
		/// </summary>
		public string PropName
		{
			set{ _propname=value;}
			get{return _propname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PropValue
		{
			set{ _propvalue=value;}
			get{return _propvalue;}
		}
		/// <summary>
		/// 参数类型
		/// </summary>
		public string PropType
		{
			set{ _proptype=value;}
			get{return _proptype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Domain
	}
}

