//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-10-19 14:53:47
//
//********************************************************************************/
using System;
using System.Drawing;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类Itop.Planning.glebeType 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class glebeType
	{
		public glebeType()
		{}
		#region 字段
		private string _uid="";
		private string _typeid="";
		private string _typename="";
		private string _typestyle="";
        private string _obligatefield1 = "";
        private string _obligatefield2 = "";
        private string _obligatefield3 = "";
        private string _obligatefield4 = "";
        private string _obligatefield5 = "";
        private string _obligatefield6 = "";
        private Color _objcolor;
       

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
		public string TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeStyle
		{
			set{ _typestyle=value;}
			get{return _typestyle;}
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
            set { _obligatefield6= value; }
        }
        public Color ObjColor
        {
            get { return _objcolor; }
            set { _objcolor = value; }
        }
		#endregion 属性
	}
}

