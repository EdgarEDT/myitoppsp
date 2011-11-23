//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-10-31 15:41:52
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类property 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class property
	{
		public property()
		{}
		#region 字段
		private string _uid="";
		private string _propertyname="";
		private string _propertyvalue="";
		private int _orderid;
		private string _typeuid="";
		private string _useuid="";
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
		public string propertyName
		{
			set{ _propertyname=value;}
			get{return _propertyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string propertyValue
		{
			set{ _propertyvalue=value;}
			get{return _propertyvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int orderID
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeUID
		{
			set{ _typeuid=value;}
			get{return _typeuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseUID
		{
			set{ _useuid=value;}
			get{return _useuid;}
		}
		#endregion 属性
	}
}

