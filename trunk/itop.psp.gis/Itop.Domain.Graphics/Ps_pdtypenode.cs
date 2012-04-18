//********************************************************************************/
//
//此代码由TONLI.NET代码生成器自动生成.
//生成时间:2012-4-18 14:50:25
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类Ps_pdtypenode 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Ps_pdtypenode
	{
		public Ps_pdtypenode()
		{
            ID = Guid.NewGuid().ToString();
		}
		#region 字段
		private string _id="";
		private string _parentid="";
		private string _deviceid="";
		private string _pdreltypeid="";
		private string _devicetype="";
		private string _title="";
		private string _code="";
		private string _s1="";
		private string _s2="";
		private double _d1;
		private double _d2;
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
		public string DeviceID
		{
			set{ _deviceid=value;}
			get{return _deviceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pdreltypeid
		{
			set{ _pdreltypeid=value;}
			get{return _pdreltypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string devicetype
		{
			set{ _devicetype=value;}
			get{return _devicetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S1
		{
			set{ _s1=value;}
			get{return _s1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S2
		{
			set{ _s2=value;}
			get{return _s2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double D1
		{
			set{ _d1=value;}
			get{return _d1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double D2
		{
			set{ _d2=value;}
			get{return _d2;}
		}
		#endregion 属性
	}
}

