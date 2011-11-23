//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-5-15 15:55:06
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistics
{
	/// <summary>
	/// 实体类PowerPicSelect 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PowerPicSelect
	{
		public PowerPicSelect()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _eachlistid="";
		private string _picselectid="";
		private string _picselectname="";
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
		public string EachListID
		{
			set{ _eachlistid=value;}
			get{return _eachlistid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PicSelectID
		{
			set{ _picselectid=value;}
			get{return _picselectid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PicSelectName
		{
			set{ _picselectname=value;}
			get{return _picselectname;}
		}
		#endregion 属性
	}
}

