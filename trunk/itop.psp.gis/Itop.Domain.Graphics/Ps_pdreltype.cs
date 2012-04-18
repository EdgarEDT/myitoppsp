//********************************************************************************/
//
//此代码由TONLI.NET代码生成器自动生成.
//生成时间:2012-4-18 14:50:48
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类Ps_pdreltype 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Ps_pdreltype
	{
		public Ps_pdreltype()
		{
            ID = Guid.NewGuid().ToString();
        }
		#region 字段
		private string _id="";
		private string _projectid="";
		private string _title="";
		private DateTime _createtime;
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
		public string ProjectID
		{
			set{ _projectid=value;}
			get{return _projectid;}
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
		public DateTime Createtime
		{
			set{ _createtime=value;}
			get{return _createtime;}
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

