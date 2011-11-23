//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-11-30 9:20:08
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
	/// <summary>
	/// 实体类PSP_Years 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PSP_P_Years
	{
        public PSP_P_Years()
		{}
		#region 字段
		private int _id;
		private int _year;
		private int _flag;
        private int _flag2;
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
		public int Year
		{
			set{ _year=value;}
			get{return _year;}
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
            set { _flag2 = value; }
            get { return _flag2; }
        }
		#endregion 属性
	}
}

