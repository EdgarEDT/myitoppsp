//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2009-10-13 14:23:51
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// 实体类Substation_Info 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Substation_Info
	{
		public Substation_Info()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _areaid="";
		private string _areaname="";
		private string _title="";
		private int? _l1;
		private double _l2;
		private int? _l3;
		private string _l4="";
		private string _l5="";
		private string _l6="";
		private string _l7="";
		private string _l8="";
		private double? _l9;
		private double? _l10;
		private string _s1="";
		private string _s2="";
		private string _s3="";
		private string _s4="";
		private string _s5="";
		private string _s6="";
		private string _s7="";
		private string _s8="";
		private string _s9="";
		private string _s10="";
		private string _flag="";
		private string _code="";
		private string _isconn="";
		private string _l11="";
		private string _l12="";
		private string _l13="";
		private string _l14="";
		private DateTime? _createdate=DateTime.Now;
		private string _l15="";
		private string _l16="";
		private string _l17="";
		private string _l18="";
		private string _l19="";
		private string _l20="";
		private string _l21="";
		private string _l22="";
		private string _l23="";
		private string _l24="";
		private string _l25="";
		private string _l26="";
		private string _l27="";
		private string _l28="";
		private string _l29="";
		private string _l30="";
        private string _DQ = "";
		private double _m1;
		private double _m2;
		private double _m3;
		private double _m4;
		private double _m5;
		private double _m6;
		private double _m7;
		private double _m8;
		private double _m9;
		private double _m10;
		private double _m11;
		private double _m12;
		private double _m13;
		private double _m14;
		private double _m15;
		private double _m16;
		private double _m17;
		private double _m18;
		private double _m19;
		private double _m20;
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
		public string AreaID
		{
			set{ _areaid=value;}
			get{return _areaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaName
		{
			set{ _areaname=value;}
			get{return _areaname;}
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
		/// 电压等级
		/// </summary>
		public int? L1
		{
			set{ _l1=value;}
			get{return _l1;}
		}
		/// <summary>
		/// 容量(MVA)
		/// </summary>
		public double L2
		{
			set{ _l2=value;}
			get{return _l2;}
		}
		/// <summary>
		/// 主变台数(台)
		/// </summary>
		public int? L3
		{
			set{ _l3=value;}
			get{return _l3;}
		}
		/// <summary>
		/// 容量构成
		/// </summary>
		public string L4
		{
			set{ _l4=value;}
			get{return _l4;}
		}
		/// <summary>
		/// 无功总容量(Mvar)
		/// </summary>
		public string L5
		{
			set{ _l5=value;}
			get{return _l5;}
		}
		/// <summary>
		/// 无功补偿容量构成(Mvar)
		/// </summary>
		public string L6
		{
			set{ _l6=value;}
			get{return _l6;}
		}
		/// <summary>
		/// 10KV可出线间隔总数
		/// </summary>
		public string L7
		{
			set{ _l7=value;}
			get{return _l7;}
		}
		/// <summary>
		/// 10kV已用出线间隔数
		/// </summary>
		public string L8
		{
			set{ _l8=value;}
			get{return _l8;}
		}
		/// <summary>
		/// 最大负荷(MW)
		/// </summary>
		public double? L9
		{
			set{ _l9=value;}
			get{return _l9;}
		}
		/// <summary>
		/// 负载率(%)
		/// </summary>
		public double? L10
		{
			set{ _l10=value;}
			get{return _l10;}
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
		public string S3
		{
			set{ _s3=value;}
			get{return _s3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S4
		{
			set{ _s4=value;}
			get{return _s4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S5
		{
			set{ _s5=value;}
			get{return _s5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S6
		{
			set{ _s6=value;}
			get{return _s6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S7
		{
			set{ _s7=value;}
			get{return _s7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S8
		{
			set{ _s8=value;}
			get{return _s8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S9
		{
			set{ _s9=value;}
			get{return _s9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string S10
		{
			set{ _s10=value;}
			get{return _s10;}
		}
		/// <summary>
		/// 是否为现状 1为现状，2为规划
		/// </summary>
		public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 电网规划设备ID
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 是否连接
		/// </summary>
		public string IsConn
		{
			set{ _isconn=value;}
			get{return _isconn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L11
		{
			set{ _l11=value;}
			get{return _l11;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L12
		{
			set{ _l12=value;}
			get{return _l12;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L13
		{
			set{ _l13=value;}
			get{return _l13;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L14
		{
			set{ _l14=value;}
			get{return _l14;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L15
		{
			set{ _l15=value;}
			get{return _l15;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L16
		{
			set{ _l16=value;}
			get{return _l16;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L17
		{
			set{ _l17=value;}
			get{return _l17;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L18
		{
			set{ _l18=value;}
			get{return _l18;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L19
		{
			set{ _l19=value;}
			get{return _l19;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L20
		{
			set{ _l20=value;}
			get{return _l20;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L21
		{
			set{ _l21=value;}
			get{return _l21;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L22
		{
			set{ _l22=value;}
			get{return _l22;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L23
		{
			set{ _l23=value;}
			get{return _l23;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L24
		{
			set{ _l24=value;}
			get{return _l24;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L25
		{
			set{ _l25=value;}
			get{return _l25;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L26
		{
			set{ _l26=value;}
			get{return _l26;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L27
		{
			set{ _l27=value;}
			get{return _l27;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L28
		{
			set{ _l28=value;}
			get{return _l28;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L29
		{
			set{ _l29=value;}
			get{return _l29;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string L30
		{
			set{ _l30=value;}
			get{return _l30;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string DQ
        {
            set { _DQ = value; }
            get { return _DQ; }
        }
		/// <summary>
		/// 
		/// </summary>
		public double M1
		{
			set{ _m1=value;}
			get{return _m1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M2
		{
			set{ _m2=value;}
			get{return _m2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M3
		{
			set{ _m3=value;}
			get{return _m3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M4
		{
			set{ _m4=value;}
			get{return _m4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M5
		{
			set{ _m5=value;}
			get{return _m5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M6
		{
			set{ _m6=value;}
			get{return _m6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M7
		{
			set{ _m7=value;}
			get{return _m7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M8
		{
			set{ _m8=value;}
			get{return _m8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M9
		{
			set{ _m9=value;}
			get{return _m9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M10
		{
			set{ _m10=value;}
			get{return _m10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M11
		{
			set{ _m11=value;}
			get{return _m11;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M12
		{
			set{ _m12=value;}
			get{return _m12;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M13
		{
			set{ _m13=value;}
			get{return _m13;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M14
		{
			set{ _m14=value;}
			get{return _m14;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M15
		{
			set{ _m15=value;}
			get{return _m15;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M16
		{
			set{ _m16=value;}
			get{return _m16;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M17
		{
			set{ _m17=value;}
			get{return _m17;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M18
		{
			set{ _m18=value;}
			get{return _m18;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M19
		{
			set{ _m19=value;}
			get{return _m19;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double M20
		{
			set{ _m20=value;}
			get{return _m20;}
		}
		#endregion 属性
	}
}

