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
	public class BurdenMonth
	{
		public BurdenMonth()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private int _burdenyear;
		private double _month1;
		private double _month2;
		private double _month3;
		private double _month4;
		private double _month5;
		private double _month6;
		private double _month7;
		private double _month8;
		private double _month9;
		private double _month10;
		private double _month11;
		private double _month12;
		private DateTime _createdate;
		private DateTime _updatedate;
		private string _title="";
		private string _remark="";
        private string _areaid;
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
		public int BurdenYear
		{
			set{ _burdenyear=value;}
			get{return _burdenyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month1
		{
			set{ _month1=value;}
			get{return _month1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month2
		{
			set{ _month2=value;}
			get{return _month2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month3
		{
			set{ _month3=value;}
			get{return _month3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month4
		{
			set{ _month4=value;}
			get{return _month4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month5
		{
			set{ _month5=value;}
			get{return _month5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month6
		{
			set{ _month6=value;}
			get{return _month6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month7
		{
			set{ _month7=value;}
			get{return _month7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month8
		{
			set{ _month8=value;}
			get{return _month8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month9
		{
			set{ _month9=value;}
			get{return _month9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month10
		{
			set{ _month10=value;}
			get{return _month10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month11
		{
			set{ _month11=value;}
			get{return _month11;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double Month12
		{
			set{ _month12=value;}
			get{return _month12;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
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
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}

        public string AreaID
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
		#endregion 属性
	}
}

