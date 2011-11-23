//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-11-10 9:01:43
//
//********************************************************************************/
using System;
namespace Itop.Domain.Chen
{
	/// <summary>
	/// 实体类PSP_VolumeBalance_Calc 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PSP_VolumeBalance_Calc
	{
		public PSP_VolumeBalance_Calc()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private string _lx1="";
		private string _lx2="";
		private double _vol=0.0;
		private string _type="";
		private string _flag="";
		private DateTime _createtime;
		private int _sort;
		private string _col1="";
		private string _col2="";
		private string _col3="";
		private string _col4="";
		private string _col5="";
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
		/// 项目名称
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 类型
		/// </summary>
		public string LX1
		{
			set{ _lx1=value;}
			get{return _lx1;}
		}
		/// <summary>
		/// 种类
		/// </summary>
		public string LX2
		{
			set{ _lx2=value;}
			get{return _lx2;}
		}
		/// <summary>
		/// 容量
		/// </summary>
		public double Vol
		{
			set{ _vol=value;}
			get{return _vol;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
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
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 序号
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col3
		{
			set{ _col3=value;}
			get{return _col3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col4
		{
			set{ _col4=value;}
			get{return _col4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Col5
		{
			set{ _col5=value;}
			get{return _col5;}
		}
		#endregion 属性
	}
}

