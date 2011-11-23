//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-12-28 10:22:24
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类LineInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class LineInfo
	{
		public LineInfo()
		{}
		#region 字段
		private string _uid="";
		private string _eleid="";
		private string _linename="";
		private string _length="";
		private string _linetype="";
		private string _voltage="";
		private string _svguid="";
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
		/// 
		/// </summary>
		public string EleID
		{
			set{ _eleid=value;}
			get{return _eleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LineName
		{
			set{ _linename=value;}
			get{return _linename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Length
		{
			set{ _length=value;}
			get{return _length;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LineType
		{
			set{ _linetype=value;}
			get{return _linetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Voltage
		{
			set{ _voltage=value;}
			get{return _voltage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SvgUID
		{
			set{ _svguid=value;}
			get{return _svguid;}
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

