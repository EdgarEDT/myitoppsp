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
	/// 实体类LayoutBookMark 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class LayoutBookMark
	{
		public LayoutBookMark()
		{}
		#region 字段
        private string _uid = Guid.NewGuid().ToString().Replace("-","_");
		private string _layoutid="";
		private string _MarkName="";
		private string _MarkDisc="";
        private string _MarkText="";
		private string _MarkType="";
		private int _StartP=0;
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
		public string MarkName
		{
			set{ _MarkName=value;}
			get{return _MarkName;}
		}
		/// <summary>
		/// 上级章节
		/// </summary>
		public string MarkDisc
		{
			set{ _MarkDisc=value;}
			get{return _MarkDisc;}
		}
		/// <summary>
		/// 章节内容
		/// </summary>
        public string MarkText
		{
			set{ _MarkText=value;}
			get{return _MarkText;}
		}
		/// <summary>
		/// 数据类型
		/// </summary>
		public string MarkType
		{
			set{ _MarkType=value;}
			get{return _MarkType;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public int StartP
		{
			set{ _StartP=value;}
			get{return _StartP;}
		}
		#endregion 属性
	}
}

