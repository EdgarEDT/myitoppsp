//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-11-29 16:07:48
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
	/// <summary>
	/// 实体类PSP_Types 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PSP_Types
	{
		public PSP_Types()
		{}
		#region 字段
		private int _id;
		private string _title="";
		private int _flag;
		private int _flag2;
		private int _parentid;
        private string _projectid;
		#endregion 字段

		#region 属性
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Flag2
		{
			set{ _flag2=value;}
			get{return _flag2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
        public string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
		#endregion 属性
	}
}

