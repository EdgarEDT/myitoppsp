//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2009-12-8 16:56:38
//
//********************************************************************************/
using System;
namespace Itop.Domain.Forecast
{
	/// <summary>
	/// 实体类Base_Data 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Base_Data
	{
		public Base_Data()
		{}
		#region 字段
		private string _uid="";
		private string _title="";
		private string _remark="";
		private int _sort;
		private DateTime _createtime;
		private DateTime _updatetime;
		private string _projectuid="";
		private string _type="";
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProjectUID
		{
			set{ _projectuid=value;}
			get{return _projectuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		#endregion 属性
	}
}

