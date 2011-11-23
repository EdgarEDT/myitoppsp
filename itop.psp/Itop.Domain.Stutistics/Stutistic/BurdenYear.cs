//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-4-23 8:38:49
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// 实体类BurdenMonth 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class BurdenYear
	{
        public BurdenYear()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private int _burdenyear=0;
        private DateTime _burdendate = DateTime.Now;
		private double _values=0.0;
        private string _areaid = "";
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
		public int BurdenYears
		{
			set{ _burdenyear=value;}
			get{return _burdenyear;}
		}

        public DateTime BurdenDate
        {
            set { _burdendate = value; }
            get { return _burdendate; }
        }

		/// <summary>
		/// 
		/// </summary>
		public double Values
		{
            set { _values = value; }
            get { return _values; }
		}
        public string AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
		#endregion 属性
	}
}

