//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-11-18 8:48:47
//
//********************************************************************************/
using System;
namespace Itop.Domain.Layouts
{
	/// <summary>
	/// 实体类RtfAttachFiles 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class RtfAttachFiles
	{
		public RtfAttachFiles()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _c_uid="";
		private string _des="";
		private string _filetype="";
        private string _filename = "";
		private decimal _filesize;
		private byte[] _filebyte;
		private DateTime _createdate;
        private string _ParentID = "";
		#endregion 字段

		#region 属性
		/// <summary>
		/// 附件标识
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string C_UID
		{
			set{ _c_uid=value;}
			get{return _c_uid;}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Des
		{
			set{ _des=value;}
			get{return _des;}
		}

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
		/// <summary>
		/// 文件类型
		/// </summary>
		public string FileType
		{
			set{ _filetype=value;}
			get{return _filetype;}
		}
		/// <summary>
		/// 文件大小
		/// </summary>
		public decimal FileSize
		{
			set{ _filesize=value;}
			get{return _filesize;}
		}
		/// <summary>
		/// 流文件
		/// </summary>
		public byte[] FileByte
		{
			set{ _filebyte=value;}
			get{return _filebyte;}
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
        /// 创建日期
        /// </summary>
        public string ParentID
        {
            set { _ParentID = value; }
            get { return _ParentID; }
        }
		#endregion 属性
	}
}

