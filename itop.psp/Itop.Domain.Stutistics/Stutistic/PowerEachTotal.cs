//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-12-11 15:21:28
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// 实体类PowerEachTotal 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PowerEachTotal
	{
		public PowerEachTotal()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _stuffname="";
		private string _lengths="";
		private string _lcount="";
		private string _total="";
		private string _volume="";
		private string _type="";
		private double _issum;
		private double _itsum;
		private string _remark="";
		private DateTime _createdate;
		private string _powerlineuid="";
        private string parentid = "";
		private int _sortid;
		#endregion 字段

		#region 属性
		/// <summary>
		/// 
		/// </summary>
        /// 
        public string ParentID
        {
            set { parentid = value; }
            get { return parentid; }
        }

		public string UID
		{
			set{ _uid=value;}
			get{return _uid;}
		}
		/// <summary>
		/// 线路名称
		/// </summary>
		public string StuffName
		{
			set{ _stuffname=value;}
			get{return _stuffname;}
		}
		/// <summary>
		/// 线路总长度
		/// </summary>
		public string Lengths
		{
			set{ _lengths=value;}
			get{return _lengths;}
		}
		/// <summary>
		/// 座数
		/// </summary>
		public string LCount
		{
			set{ _lcount=value;}
			get{return _lcount;}
		}
		/// <summary>
		/// 主变台数
		/// </summary>
		public string Total
		{
			set{ _total=value;}
			get{return _total;}
		}
		/// <summary>
		/// 主变总容量
		/// </summary>
		public string Volume
		{
			set{ _volume=value;}
			get{return _volume;}
		}
		/// <summary>
		/// 其他
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 十一五静态投资合计
		/// </summary>
		public double IsSum
		{
			set{ _issum=value;}
			get{return _issum;}
		}
		/// <summary>
		/// 十一五动态投资合计
		/// </summary>
		public double ItSum
		{
			set{ _itsum=value;}
			get{return _itsum;}
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
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PowerLineUID
		{
			set{ _powerlineuid=value;}
			get{return _powerlineuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SortID
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		#endregion 属性
	}
}

