//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-10-19 15:19:53
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类SVGFOLDER 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class SVGFOLDER
	{
		public SVGFOLDER()
		{}
		#region 字段
		private string _suid="";
		private string _foldername="";
		private string _parentid="";
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
		public string FOLDERNAME
		{
			set{ _foldername=value;}
			get{return _foldername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PARENTID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		#endregion 属性
	}
}

