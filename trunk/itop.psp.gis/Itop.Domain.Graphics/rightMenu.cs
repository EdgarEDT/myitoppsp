//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-10-31 15:42:31
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类rightMenu 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class rightMenu
	{
		public rightMenu()
		{}
		#region 字段
		private string _uid="";
		private string _itemname="";
		private int _orderid;
		private string _typeuid="";
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
		public string itemName
		{
			set{ _itemname=value;}
			get{return _itemname;}
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
		#endregion 属性
	}
}

