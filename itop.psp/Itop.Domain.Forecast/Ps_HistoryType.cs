//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2011-1-12 13:17:10
//
//********************************************************************************/
using System;
namespace Itop.Domain.Forecast
{
	/// <summary>
	/// 实体类 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Ps_HistoryType
	{
		public Ps_HistoryType ()
		{}
		#region 字段
		private string _id="";
		private string _typename="";
		private int _sort;
		private string _parentid="";
		private string _units="";
		private string _flag="";
		private string _remark="";
		private double _num1;
		private double _num2;
		private string _col1="";
		private string _col2="";
		private string _col3="";
		#endregion 字段

		#region 属性
		/// <summary>
		/// 类别标识
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 类别名称
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 类别序号
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 父类标识
		/// </summary>
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 类别单位（多个单位以#分隔）
		/// </summary>
		public string Units
		{
			set{ _units=value;}
			get{return _units;}
		}
		/// <summary>
		/// 类别标志 
		/// </summary>
		public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 类别备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public double Num1
		{
			set{ _num1=value;}
			get{return _num1;}
		}
		/// <summary>
        /// 备用
		/// </summary>
		public double Num2
		{
			set{ _num2=value;}
			get{return _num2;}
		}
		/// <summary>
        /// 备用
		/// </summary>
		public string Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
        /// 备用
		/// </summary>
		public string Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		/// <summary>
        /// 备用
		/// </summary>
		public string Col3
		{
			set{ _col3=value;}
			get{return _col3;}
		}
		#endregion 属性
	}
}

