//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-10-19 14:54:11
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类Itop.Planning.glebeProperty 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class sonGlebeProperty
	{
        public sonGlebeProperty()
		{}
		#region 字段
		private string _uid="";
        private string _eleid = "";
		private string _useid="";
		private string _typeuid="";
        private decimal _area;
        private decimal _burthen;
        private decimal _number;
		private string _remark="";
        private string _sonuid = "";
        private string _selsonarea = "";
        private string _svguid = "";
        private string _parenteleid = "";
        private string _layerid = "";
        private string _obligatefield1 = "";
        private string _obligatefield2 = "";
      
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
        public string EleID
        {
            set { _eleid = value; }
            get { return _eleid; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string UseID
		{
			set{ _useid=value;}
			get{return _useid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeUID
		{
			set{ _typeuid=value;}
			get{return _typeuid;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Area
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Burthen
		{
			set{ _burthen=value;}
			get{return _burthen;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        public string SonUid
        {
            set { _sonuid = value; }
            get { return _sonuid; }
        }
        public string SelSonArea
        {
            get { return _selsonarea; }
            set { _selsonarea = value; }
        }
        public string SvgUID
        {
            get { return _svguid; }
            set { _svguid = value; }
        }
        public string ParentEleID
        {
            get { return _parenteleid; }
            set { _parenteleid = value; }
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
		#endregion 属性
	}
}

