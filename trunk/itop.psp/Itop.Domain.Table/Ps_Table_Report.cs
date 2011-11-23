using System;

namespace Itop.Domain.Table
{
    [Serializable]
    public class Ps_Table_Report
    {
        /// <summary>
	/// 实体类Ps_Table_Report 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>

	
		public Ps_Table_Report()
		{}
		#region 字段
        private string _id =  Guid.NewGuid().ToString();
		private string _tableid="";
		private string _tableoldname="";
		private string _tablenewname="";
		private string _years="";
		private int _num1;
		private int _num2;
		private int _num3;
		private int _num4;
		private string _bc1="";
		private string _bc2="";
		private string _bc3="";
		private string _remark="";
		private string _remark2="";
		private byte[] _image1;
		private byte[] _image2;
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
		/// 
		/// </summary>
		public string TableID
		{
			set{ _tableid=value;}
			get{return _tableid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TableOldName
		{
			set{ _tableoldname=value;}
			get{return _tableoldname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TableNewName
		{
			set{ _tablenewname=value;}
			get{return _tablenewname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Years
		{
			set{ _years=value;}
			get{return _years;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Num1
		{
			set{ _num1=value;}
			get{return _num1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Num2
		{
			set{ _num2=value;}
			get{return _num2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Num3
		{
			set{ _num3=value;}
			get{return _num3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Num4
		{
			set{ _num4=value;}
			get{return _num4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProjectID
		{
			set{ _bc1=value;}
			get{return _bc1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BC2
		{
			set{ _bc2=value;}
			get{return _bc2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BC3
		{
			set{ _bc3=value;}
			get{return _bc3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark2
		{
			set{ _remark2=value;}
			get{return _remark2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] image1
		{
			set{ _image1=value;}
			get{return _image1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] image2
		{
			set{ _image2=value;}
			get{return _image2;}
		}
		#endregion 属性

    }
}
