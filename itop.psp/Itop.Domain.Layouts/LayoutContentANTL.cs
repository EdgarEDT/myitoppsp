//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-11-16 14:22:54
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// 实体类LayoutContentANTL 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class LayoutContentANTL
	{
        public LayoutContentANTL()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _layoutid="";
		private string _chaptername="";
		private string _parentid="";
		private byte[] _contents;
		private string _contenttype="";
		private string _remark="";
        private DateTime _createDate = DateTime.Now;
		#endregion 字段

		#region 属性
		/// <summary>
		/// 章节标识
		/// </summary>
        /// 
        public DateTime CreateDate
        {
            set { _createDate = value; }
            get { return _createDate; }
        }

		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 规划标识
		/// </summary>
		public string LayoutID
		{
			set{ _layoutid=value;}
			get{return _layoutid;}
		}
		/// <summary>
		/// 章节名称
		/// </summary>
		public string ChapterName
		{
			set{ _chaptername=value;}
			get{return _chaptername;}
		}
		/// <summary>
		/// 上级章节
		/// </summary>
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 章节内容
		/// </summary>
		public byte[] Contents
		{
			set{ _contents=value;}
			get{return _contents;}
		}
		/// <summary>
		/// 数据类型
		/// </summary>
		public string ContentType
		{
			set{ _contenttype=value;}
			get{return _contenttype;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion 属性
	}
}

