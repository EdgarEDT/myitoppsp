//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2009-7-27 9:43:57
//
//********************************************************************************/
using System;
namespace Itop.Domain.Forecast
{
	/// <summary>
	/// 实体类Ps_forecast_list 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Ps_forecast_list
	{
		public Ps_forecast_list()
		{}
		#region 字段
		private string _id="";
		private string _title="";
		private int _startyear;
		private int _endyear;
		private string _userid="";
		private string _col1="";
		private string _col2="";
        private int _ycstartyear;
        private int _ycendyear;

		#endregion 字段

		#region 属性
		/// <summary>
		/// GUID
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 预测名称
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 起始年份
		/// </summary>
		public int StartYear
		{
			set{ _startyear=value;}
			get{return _startyear;}
		}
		/// <summary>
		/// 结束年份
		/// </summary>
		public int EndYear
		{
			set{ _endyear=value;}
			get{return _endyear;}
		}
		/// <summary>
		/// 用户ID
		/// </summary>
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 备用1
		/// </summary>
		public string Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// 备用2
		/// </summary>
		public string Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
        /// <summary>
        /// 预测起始年份
        /// </summary>
        public int YcStartYear
        {
            set { _ycstartyear = value; }
            get { return _ycstartyear; }
        }
        /// <summary>
        /// 预测结束年份
        /// </summary>
        public int YcEndYear
        {
            set { _ycendyear = value; }
            get { return _ycendyear; }
        }

		#endregion 属性
	}
}

