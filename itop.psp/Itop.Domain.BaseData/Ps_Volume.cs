//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2009-9-28 9:50:25
//
//********************************************************************************/
using System;
namespace Itop.Domain.BaseData
{
	/// <summary>
	/// 实体类Ps_Volume 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Ps_Volume
	{
		public Ps_Volume()
		{}
		#region 字段
		private string _id="";
		private int _years;
		private double _maxpw;
		private double _yearendvolume;
		private double _watervolume;
		private double _firevolume;
		private double _backupvolume;
		private double _toolsvolume;
		private double _maxvolume;
		private double _balkvolume;
		private double _balkwatervolume;
		private double _balkfirevolume;
		private double _balancevolume;
		private double _feedpw;
		private double _getpw;
		private double _breakpw;
		private double _getps;
		private string _iswaterfire="";
		private double _iswaterfirepst;
		private double _isgetpwpst;
		private DateTime _createtime;
		private string _createuser="";
		private string _col1="";
		private string _col2="";
		private string _col3="";
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
		/// 年份
		/// </summary>
		public int Years
		{
			set{ _years=value;}
			get{return _years;}
		}
		/// <summary>
		/// 最大供电负荷需求
		/// </summary>
		public double MaxPw
		{
			set{ _maxpw=value;}
			get{return _maxpw;}
		}
		/// <summary>
		/// 年末装机容量
		/// </summary>
		public double YearEndVolume
		{
			set{ _yearendvolume=value;}
			get{return _yearendvolume;}
		}
		/// <summary>
		/// 水电装机容量
		/// </summary>
		public double WaterVolume
		{
			set{ _watervolume=value;}
			get{return _watervolume;}
		}
		/// <summary>
		/// 火电装机容量
		/// </summary>
		public double FireVolume
		{
			set{ _firevolume=value;}
			get{return _firevolume;}
		}
		/// <summary>
		/// 装机备用容量
		/// </summary>
		public double BackupVolume
		{
			set{ _backupvolume=value;}
			get{return _backupvolume;}
		}
		/// <summary>
		/// 机组容量
		/// </summary>
		public double ToolsVolume
		{
			set{ _toolsvolume=value;}
			get{return _toolsvolume;}
		}
		/// <summary>
		/// 最大单机容量
		/// </summary>
		public double MaxVolume
		{
			set{ _maxvolume=value;}
			get{return _maxvolume;}
		}
		/// <summary>
		/// 受阻容量
		/// </summary>
		public double balkVolume
		{
			set{ _balkvolume=value;}
			get{return _balkvolume;}
		}
		/// <summary>
		/// 水电受阻容量
		/// </summary>
		public double balkWaterVolume
		{
			set{ _balkwatervolume=value;}
			get{return _balkwatervolume;}
		}
		/// <summary>
		/// 火电受阻容量
		/// </summary>
		public double balkFireVolume
		{
			set{ _balkfirevolume=value;}
			get{return _balkfirevolume;}
		}
		/// <summary>
		/// 平衡容量
		/// </summary>
		public double BalanceVolume
		{
			set{ _balancevolume=value;}
			get{return _balancevolume;}
		}
		/// <summary>
		/// 可供电力
		/// </summary>
		public double FeedPw
		{
			set{ _feedpw=value;}
			get{return _feedpw;}
		}
		/// <summary>
		/// 电网外受电力
		/// </summary>
		public double GetPw
		{
			set{ _getpw=value;}
			get{return _getpw;}
		}
		/// <summary>
		/// 电力盈（+）亏（-）平衡
		/// </summary>
		public double BreakPw
		{
			set{ _breakpw=value;}
			get{return _breakpw;}
		}
		/// <summary>
		/// 外受电比例
		/// </summary>
		public double GetPs
		{
			set{ _getps=value;}
			get{return _getps;}
		}
		/// <summary>
		/// 是否水力火力
		/// </summary>
		public string IsWaterFire
		{
			set{ _iswaterfire=value;}
			get{return _iswaterfire;}
		}
		/// <summary>
		/// 水力火力百分比
		/// </summary>
		public double IsWaterFirePst
		{
			set{ _iswaterfirepst=value;}
			get{return _iswaterfirepst;}
		}
		/// <summary>
		/// 可供电力百分比
		/// </summary>
		public double IsGetPwPst
		{
			set{ _isgetpwpst=value;}
			get{return _isgetpwpst;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 添加人
		/// </summary>
		public string CreateUser
		{
			set{ _createuser=value;}
			get{return _createuser;}
		}
		/// <summary>
		/// 备用1
		/// </summary>
		public string Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// 备用2
		/// </summary>
		public string Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		/// <summary>
		/// 备用3
		/// </summary>
		public string Col3
		{
			set{ _col3=value;}
			get{return _col3;}
		}
		#endregion 属性
	}
}

