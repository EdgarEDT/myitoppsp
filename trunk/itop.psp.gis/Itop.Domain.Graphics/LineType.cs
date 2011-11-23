//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-4-2 10:22:45
//
//********************************************************************************/
using System;
using System.Drawing;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类LineType 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class LineType
	{
		public LineType()
		{}
		#region 字段
		private string _uid="";
		private string _typename="";
		private string _color="";
		private string _obligatefield1="";
        private Color _objcolor ;

       
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
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Color
		{
			set{ _color=value;}
			get{return _color;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ObligateField1
		{
			set{ _obligatefield1=value;}
			get{return _obligatefield1;}
		}
        public Color ObjColor
        {
            get { return _objcolor; }
            set { _objcolor = value; }
        }
		#endregion 属性
	}
}

