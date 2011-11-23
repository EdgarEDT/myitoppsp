//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2010-01-07 9:16:07
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类Psp_ProgLayerList 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Psp_ProgLayerList
	{
		public Psp_ProgLayerList()
		{}
		#region 字段
		private string _uid="";
		private string _proguid="";
		private string _layergradeid="";
		private string _col1="";
		private string _col2="";
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
		public string ProgUID
		{
			set{ _proguid=value;}
			get{return _proguid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LayerGradeID
		{
			set{ _layergradeid=value;}
			get{return _layergradeid;}
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
		#endregion 属性
	}
}

