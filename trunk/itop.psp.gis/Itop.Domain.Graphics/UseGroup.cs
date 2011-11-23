//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-6-5 14:22:23
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类UseGroup 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class UseGroup
	{
		public UseGroup()
		{}
		#region 字段
		private string _uid="";
		private string _groupname="";
		private string _content="";
		private string _remark="";
        private string x = "";
        private string y = "";
        private string width = "";
        private string height = "";
      

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
		public string GroupName
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        public string Width
        {
            get { return width; }
            set { width = value; }
        }
        public string Y
        {
            get { return y; }
            set { y = value; }
        }
        public string X
        {
            get { return x; }
            set { x = value; }
        }
        public string Height
        {
            get { return height; }
            set { height = value; }
        }
		#endregion 属性
	}
}

