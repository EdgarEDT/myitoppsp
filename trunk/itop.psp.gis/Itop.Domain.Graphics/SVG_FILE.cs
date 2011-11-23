//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2009-03-23 9:56:25
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类SVG_FILE 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class SVG_FILE
	{
		public SVG_FILE()
		{}
		#region 字段
		private string _suid="";
		private string _name="";
		private string _xml="";
		private DateTime _mdate;
		#endregion 字段

		#region 属性
		/// <summary>
		/// 
		/// </summary>
		public string SUID
		{
			set{ _suid=value;}
			get{return _suid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NAME
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string XML
		{
			set{ _xml=value;}
			get{return _xml;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime MDATE
		{
			set{ _mdate=value;}
			get{return _mdate;}
		}
		#endregion 属性
	}
}

