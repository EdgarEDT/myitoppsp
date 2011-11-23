//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-5-12 10:48:10
//
//********************************************************************************/
using System;
using System.Drawing;
namespace Itop.Domain.Forecast
{
	/// <summary>
	/// 实体类FORBaseColor 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class FORBaseColor
	{
		public FORBaseColor()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private int _color;
		private double _maxvalue;
		private double _minvalue;
		private string _remark="";
        private Color _color1 =new Color();
		private DateTime _createtime;
		private DateTime _updatetime;
		#endregion 字段

		#region 属性
		/// <summary>
		/// 主键
		/// </summary>
		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 颜色
		/// </summary>
		public int Color
		{
			set{ _color=value;}
			get{return _color;}
		}

        public Color Color1
        {
            set { _color1 = value; }
            get { return _color1; }
        }
		/// <summary>
		/// 最大值
		/// </summary>
		public double MaxValue
		{
			set{ _maxvalue=value;}
			get{return _maxvalue;}
		}
		/// <summary>
		/// 最小值
		/// </summary>
		public double MinValue
		{
			set{ _minvalue=value;}
			get{return _minvalue;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
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
		/// 修改时间
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion 属性
	}
}

