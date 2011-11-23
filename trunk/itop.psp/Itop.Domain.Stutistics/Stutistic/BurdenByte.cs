//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-12-6 9:07:37
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
    /// 实体类BurdenByte 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public class BurdenByte
	{
		public BurdenByte()
		{}
		#region 字段
        private int _burdenyear;
		private DateTime _burdendate;
        private string _season;
        private string _areaid;
		#endregion 字段

		#region 属性
		/// <summary>
		/// 年
		/// </summary>
        public int BurdenYear
		{
            set { _burdenyear = value; }
            get { return _burdenyear; }
		}
		/// <summary>
		/// 季节
		/// </summary>
		public string Season
		{
			set{ _season=value;}
			get{return _season;}
		}
		/// <summary>
		/// 日期
		/// </summary>
		public DateTime BurdenDate
		{
			set{ _burdendate=value;}
			get{return _burdendate;}
		}
        /// <summary>
        /// 地区id
        /// </summary>
        public string AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
       
		#endregion 属性
	}
}

