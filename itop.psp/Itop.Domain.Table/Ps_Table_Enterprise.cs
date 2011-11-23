//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2010-09-21 14:12:47
//
//********************************************************************************/
using System;
namespace Itop.Domain.Table
{
	/// <summary>
	/// 实体类Ps_Table_Enterprise 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Ps_Table_Enterprise
	{
		public Ps_Table_Enterprise()
		{}
		#region 字段
		private string _uid="";
		private string _sname="";
		private string _stype="";
		private string _dq="";
		private string _col1="";
        private string _projectid = "";
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
		public string SName
		{
			set{ _sname=value;}
			get{return _sname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SType
		{
			set{ _stype=value;}
			get{return _stype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DQ
		{
			set{ _dq=value;}
			get{return _dq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
        public string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
		#endregion 属性
	}
}

