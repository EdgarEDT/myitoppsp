//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2009-08-28 11:08:08
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类PSP_SubstationPar 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PSP_SubstationPar
	{
		public PSP_SubstationPar()
		{}
		#region 字段
		private string _uid="";
		private string _infoname="";
		private int _type;
		private string _col1="";
		private string _col2="";
		private string _col3="";
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
		public string InfoName
		{
			set{ _infoname=value;}
			get{return _infoname;}
		}
		/// <summary>
		/// (1表示否决因素，2表示一般因素） 
		/// </summary>
		public int type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string col1
		{
			set{ _col1=value;}
			get{return _col1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string col2
		{
			set{ _col2=value;}
			get{return _col2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string col3
		{
			set{ _col3=value;}
			get{return _col3;}
		}
		#endregion 属性
	}
}

