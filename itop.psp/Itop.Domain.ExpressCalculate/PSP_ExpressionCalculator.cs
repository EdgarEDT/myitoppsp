//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2009-4-2 10:05:37
//
//********************************************************************************/
using System;
namespace Itop.Domain.ExpressCalculate
{
	/// <summary>
	/// 实体类PSP_ExpressionCalculator 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PSP_ExpressionCalculator
	{
		public PSP_ExpressionCalculator()
		{}
		#region 字段
		private string _id="";
		private string _expression="";
        private string _flag;
		private string _filedname="";
		private string _s1="";
		private string _s2="";
		private string _s3="";
		private string _s4="";
		private string _s5="";
		private DateTime _creattime;
		private int _savedecimalpoint;
		#endregion 字段

		#region 属性
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 表达式
		/// </summary>
		public string Expression
		{
			set{ _expression=value;}
			get{return _expression;}
		}
		/// <summary>
		/// TypeID
		/// </summary>
        public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// Colimn的FiledName
		/// </summary>
		public string FiledName
		{
			set{ _filedname=value;}
			get{return _filedname;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public string S1
		{
			set{ _s1=value;}
			get{return _s1;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public string S2
		{
			set{ _s2=value;}
			get{return _s2;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public string S3
		{
			set{ _s3=value;}
			get{return _s3;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public string S4
		{
			set{ _s4=value;}
			get{return _s4;}
		}
		/// <summary>
		/// 备用
		/// </summary>
		public string S5
		{
			set{ _s5=value;}
			get{return _s5;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatTime
		{
			set{ _creattime=value;}
			get{return _creattime;}
		}
		/// <summary>
		/// 保留小数点位数
		/// </summary>
		public int SaveDecimalPoint
		{
			set{ _savedecimalpoint=value;}
			get{return _savedecimalpoint;}
		}
		#endregion 属性
	}
}

