//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-11-29 15:03:50
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// 实体类Values 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PowerValues
    {
        public PowerValues()
        { }
        #region 字段
        private int _id;
        private int _typeid;
        private int _year;
        private double _value;
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
		public int TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
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
		public double Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		#endregion 属性
	}
}

