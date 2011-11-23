//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-10-19 14:54:59
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类Itop.Planning.SVGFILE 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class SVGFILE
	{
		public SVGFILE()
		{}
		#region 字段
		private string _suid="";
		private string _filename="";
		private string _parentid="";
		private string _svgdata="";
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
		public string FILENAME
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PARENTID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SVGDATA
		{
			set{ _svgdata=value;}
			get{return _svgdata;}
		}
		#endregion 属性
	}
}

