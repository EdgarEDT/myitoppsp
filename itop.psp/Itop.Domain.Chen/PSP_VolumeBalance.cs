//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-11-4 14:41:11
//
//********************************************************************************/
using System;
namespace Itop.Domain.Chen
{
	/// <summary>
	/// 实体类PSP_VolumeBalance 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PSP_VolumeBalance
	{
		public PSP_VolumeBalance()
		{}
		#region 字段
        private string _uid = Guid.NewGuid().ToString();
		private double _l1=0;
		private double _l2=0;
		private double _l3=0;
		private double _l4=0;
		private double _l5=0;
		private double _l6=0;
		private double _l7=0;
		private double _l8=0;
		private double _l9=0;
		private double _l10=0;
		private double _l11=0;
		private double _l12=0;
		private double _l13=0;
		private string _l14="";
		private string _s1="";
		private string _s2="";
		private string _s3="";
		private string _s4="";
		private string _s5="";
		private string _s6="";
		private string _typeid="";
		private string _flag="";
		private DateTime _creattime;
		private int _sort;
		private int  _year=0;
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
		/// 分区综合最高负荷
		/// </summary>
		public double L1
		{
			set{ _l1=value;}
			get{return _l1;}
		}
		/// <summary>
		/// 220kV主变35kV侧可供负荷
		/// </summary>
		public double L2
		{
			set{ _l2=value;}
			get{return _l2;}
		}
		/// <summary>
		/// 小电厂需备用容量
		/// </summary>
		public double L3
		{
			set{ _l3=value;}
			get{return _l3;}
		}
		/// <summary>
		/// 110kV及以下小电源直接供电负荷
		/// </summary>
		public double L4
		{
			set{ _l4=value;}
			get{return _l4;}
		}
		/// <summary>
		/// 需110kV降压供电负荷
		/// </summary>
		public double L5
		{
			set{ _l5=value;}
			get{return _l5;}
		}
		/// <summary>
		/// 现有110kV降压变电容量
		/// </summary>
		public double L6
		{
			set{ _l6=value;}
			get{return _l6;}
		}
		/// <summary>
		/// 110kV容载比
		/// </summary>
		public double L7
		{
			set{ _l7=value;}
			get{return _l7;}
		}
		/// <summary>
		/// 需110kV变电容量
		/// </summary>
		public double L8
		{
			set{ _l8=value;}
			get{return _l8;}
		}
		/// <summary>
		/// 变电容量盈亏
		/// </summary>
		public double L9
		{
			set{ _l9=value;}
			get{return _l9;}
		}
		/// <summary>
		/// 已立项的变电容量
		/// </summary>
		public double L10
		{
			set{ _l10=value;}
			get{return _l10;}
		}
		/// <summary>
		/// 规划新增变电容量
		/// </summary>
		public double L11
		{
			set{ _l11=value;}
			get{return _l11;}
		}
		/// <summary>
		/// 变电容量合计
		/// </summary>
		public double L12
		{
			set{ _l12=value;}
			get{return _l12;}
		}
		/// <summary>
		/// 容载比
		/// </summary>
		public double L13
		{
			set{ _l13=value;}
			get{return _l13;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string L14
		{
			set{ _l14=value;}
			get{return _l14;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public string S1
		{
			set{ _s1=value;}
			get{return _s1;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public string S2
		{
			set{ _s2=value;}
			get{return _s2;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public string S3
		{
			set{ _s3=value;}
			get{return _s3;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public string S4
		{
			set{ _s4=value;}
			get{return _s4;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public string S5
		{
			set{ _s5=value;}
			get{return _s5;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public string S6
		{
			set{ _s6=value;}
			get{return _s6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 时间
		/// </summary>
		public DateTime CreatTime
		{
			set{ _creattime=value;}
			get{return _creattime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
        public int Year
		{
			set{ _year=value;}
			get{return _year;}
		}
		#endregion 属性
	}
}

