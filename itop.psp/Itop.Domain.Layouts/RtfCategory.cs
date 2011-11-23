//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-11-17 13:52:32
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// 实体类RtfCategory 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class RtfCategory
	{
		public RtfCategory()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private string _parentid="";
		private decimal _sortno;
		private byte[] _rtfcontents=new byte [0];
		private int _ifparent;
		#endregion 字段

		#region 属性
		/// <summary>
		/// 目录标识
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 目录名称
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 上级目录标识
		/// </summary>
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 目录顺序号
		/// </summary>
		public decimal SortNo
		{
			set{ _sortno=value;}
			get{return _sortno;}
		}
		/// <summary>
		/// 文字内容
		/// </summary>
		public byte[] RtfContents
		{
			set{ _rtfcontents=value;}
			get{return _rtfcontents;}
		}
		/// <summary>
		/// 是否有下级目录
		/// </summary>
		public int IfParent
		{
			set{ _ifparent=value;}
			get{return _ifparent;}
		}
		#endregion 属性
	}
}

