//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-2-28 17:49:15
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类substation 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class substation
	{
		public substation()
		{}
		#region 字段
		private string _uid="";
		private string _eleid="";
		private string _elename="";
        private decimal _burthen;
        private decimal _number;
		private string _glebeeleid="";
		private string _svguid="";
        private string _remark = "";
        private string _layerid = "";
        private string _obligatefield1 = "";
        private string _obligatefield2 = "";
        private string _obligatefield3 = "";
        private string _obligatefield4 = "";
        private string _obligatefield5 = "";
        private string _obligatefield6 = "";
        private string _obligatefield7 = "";



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
		/// 图元ID
		/// </summary>
		public string EleID
		{
			set{ _eleid=value;}
			get{return _eleid;}
		}
		/// <summary>
		/// 变电站名称
		/// </summary>
		public string EleName
		{
			set{ _elename=value;}
			get{return _elename;}
		}
		/// <summary>
		/// 负荷
		/// </summary>
        public decimal Burthen
		{
			set{ _burthen=value;}
			get{return _burthen;}
		}
		/// <summary>
		/// 电量
		/// </summary>
        public decimal Number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// 所属区域图元ID
		/// </summary>
		public string glebeEleID
		{
			set{ _glebeeleid=value;}
			get{return _glebeeleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SvgUID
		{
			set{ _svguid=value;}
			get{return _svguid;}
		}
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        public string LayerID
        {
            get { return _layerid; }
            set { _layerid = value; }
        }
        public string ObligateField1
        {
            get { return _obligatefield1; }
            set { _obligatefield1 = value; }
        }
        public string ObligateField2
        {
            get { return _obligatefield2; }
            set { _obligatefield2 = value; }
        }
        public string ObligateField3
        {
            get { return _obligatefield3; }
            set { _obligatefield3 = value; }
        }
        public string ObligateField4
        {
            get { return _obligatefield4; }
            set { _obligatefield4 = value; }
        }
        public string ObligateField5
        {
            get { return _obligatefield5; }
            set { _obligatefield5 = value; }
        }
        public string ObligateField6
        {
            get { return _obligatefield6; }
            set { _obligatefield6 = value; }
        }
        public string ObligateField7
        {
            get { return _obligatefield7; }
            set { _obligatefield7 = value; }
        }
		#endregion 属性
	}
}

