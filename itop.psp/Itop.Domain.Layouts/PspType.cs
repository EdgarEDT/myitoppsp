//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-1-4 14:08:32
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// 实体类PspType 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PspType
	{
		public PspType()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private string _parentid="";
		private DateTime _createdate;
		private byte[] _contents;
		private string _remark="";
        private string _col1 = "";
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
		/// 名称
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 父结点ID
		/// </summary>
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
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
		/// 附件
		/// </summary>
		public byte[] Contents
		{
			set{ _contents=value;}
			get{return _contents;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}

        public string Col1
        {
            set { _col1 = value; }
            get { return _col1; }
        }
		#endregion 属性
	}
}

