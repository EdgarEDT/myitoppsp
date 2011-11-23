//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-11-16 10:34:18
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// 实体类Layout 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Layout
	{
		public Layout()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _layoutname="";
		private DateTime _createdate;
		private string _creater="";
		private string _remark="";
        private string _createrName = "";
		#endregion 字段

		#region 属性
		/// <summary>
		/// 规划标识
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 规划名称
		/// </summary>
		public string LayoutName
		{
			set{ _layoutname=value;}
			get{return _layoutname;}
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
		/// 创建人
		/// </summary>
		public string Creater
		{
			set{ _creater=value;}
			get{return _creater;}
		}
        /// <summary>
        /// 创建人名
        /// </summary>
        public string CreaterName
        {
            set { _createrName = value; }
            get { return _createrName; }
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

