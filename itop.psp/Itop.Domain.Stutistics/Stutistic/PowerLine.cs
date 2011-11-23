//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-12-8 8:51:36
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// 实体类PowerLine 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PowerLine
	{
		public PowerLine()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _powername="";
		private DateTime _createdate;
		private string _remark="";
		private string _parentid="";
		private string _col1="";
		private string _col2="";
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
		/// 线路名称
		/// </summary>
		public string PowerName
		{
			set{ _powername=value;}
			get{return _powername;}
		}
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 说明
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		#endregion 属性
	}
}

