//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-11-1 16:01:40
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类UseRelating 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class UseRelating
	{
		public UseRelating()
		{}
		#region 字段
		private string _uid="";
		private string _useid="";
		private string _linkuid="";
		private string _usepropertyuid="";
		private string _svguid="";
		private string _menuuid="";
        private string _obligate1 = "";

        private string _obligate2 = "";
       
        private string _obligate3 = "";
     
        private string _obligate4 = "";

      
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
		public string UseID
		{
			set{ _useid=value;}
			get{return _useid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LinkUID
		{
			set{ _linkuid=value;}
			get{return _linkuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UsePropertyUID
		{
			set{ _usepropertyuid=value;}
			get{return _usepropertyuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SvgUid
		{
			set{ _svguid=value;}
			get{return _svguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MenuUID
		{
			set{ _menuuid=value;}
			get{return _menuuid;}
		}
        public string Obligate3
        {
            get { return _obligate3; }
            set { _obligate3 = value; }
        }
        public string Obligate2
        {
            get { return _obligate2; }
            set { _obligate2 = value; }
        }
        public string Obligate1
        {
            get { return _obligate1; }
            set { _obligate1 = value; }
        }
        public string Obligate4
        {
            get { return _obligate4; }
            set { _obligate4 = value; }
        }
		#endregion 属性
	}
}

