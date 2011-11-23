//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-12-4 12:57:05
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
	/// <summary>
	/// 实体类PSP_ForecastReports 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PSP_ForecastReports
	{
		public PSP_ForecastReports()
		{}
		#region 字段
		private int _id;
		private string _title="";
		private int _startyear;
		private int _endyear;
		private int _historyyears;
		private int _flag;
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
		public int StartYear
		{
			set{ _startyear=value;}
			get{return _startyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int EndYear
		{
			set{ _endyear=value;}
			get{return _endyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int HistoryYears
		{
			set{ _historyyears=value;}
			get{return _historyyears;}
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
        public string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
		#endregion 属性
	}
}

