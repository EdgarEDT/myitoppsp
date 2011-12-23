//********************************************************************************/
//
//此代码由TONLI.NET代码生成器自动生成.
//生成时间:2011-12-23 13:43:44
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类glebeYearValue 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class glebeYearValue
	{
		public glebeYearValue()
		{
            _id= Guid.NewGuid().ToString();
		}
		#region 字段
		private string _id="";
		private string _parentid="";
		private int _year;
		private double _burthen;
		private double _avgfhmd;
		private double _fhmdtz;
		private string _s1="";
		private string _s2="";
		private string _s3="";
		private string _s4="";
		private string _s5="";
		private string _s6="";
		#endregion 字段

		#region 属性
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
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
		public double Burthen
		{
			set{ _burthen=value;}
			get{return _burthen;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double AvgFHmd
		{
			set{ _avgfhmd=value;}
			get{return _avgfhmd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double FHmdTz
		{
			set{ _fhmdtz=value;}
			get{return _fhmdtz;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string s1
		{
			set{ _s1=value;}
			get{return _s1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string s2
		{
			set{ _s2=value;}
			get{return _s2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string s3
		{
			set{ _s3=value;}
			get{return _s3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string s4
		{
			set{ _s4=value;}
			get{return _s4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string s5
		{
			set{ _s5=value;}
			get{return _s5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string s6
		{
			set{ _s6=value;}
			get{return _s6;}
		}
		#endregion 属性
	}
}

