//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-5-17 14:51:51
//
//********************************************************************************/
using System;
namespace Itop.Domain.BaseDatas
{
	/// <summary>
	/// 实体类ReportFormat 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class ReportFormat
	{
		public ReportFormat()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private byte[] _bytereport;
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] ByteReport
		{
			set{ _bytereport=value;}
			get{return _bytereport;}
		}
		#endregion 属性
	}
}

