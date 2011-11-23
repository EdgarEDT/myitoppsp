//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-12-7 15:17:11
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// 实体类BurdenLineForecast 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class BurdenLineForecast
	{
		public BurdenLineForecast()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private int _burdenyear;
		private double _summerdayaverage;
		private double _summerminaverage;
		private double _winterdayaverage;
		private double _winterminaverage;
        private double _summerdata;
        private double _winterdata;
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
		/// 年度
		/// </summary>
		public int BurdenYear
		{
			set{ _burdenyear=value;}
			get{return _burdenyear;}
		}
		/// <summary>
		/// 夏季日平均负荷率
		/// </summary>
		public double SummerDayAverage
		{
			set{ _summerdayaverage=value;}
			get{return _summerdayaverage;}
		}
		/// <summary>
		/// 夏季日最小负荷率
		/// </summary>
		public double SummerMinAverage
		{
			set{ _summerminaverage=value;}
			get{return _summerminaverage;}
		}
		/// <summary>
		/// 冬季日平均负荷率
		/// </summary>
		public double WinterDayAverage
		{
			set{ _winterdayaverage=value;}
			get{return _winterdayaverage;}
		}
		/// <summary>
		/// 冬季日最小负荷率
		/// </summary>
		public double WinterMinAverage
		{
			set{ _winterminaverage=value;}
			get{return _winterminaverage;}
		}



        /// <summary>
        /// 
        /// </summary>
        public double SummerData
        {
            set { _summerdata = value; }
            get { return _summerdata; }
        }
        /// <summary>
        /// 冬季日平均负荷率
        /// </summary>
        public double WinterData
        {
            set { _winterdata = value; }
            get { return _winterdata; }
        }
		#endregion 属性
	}
}

