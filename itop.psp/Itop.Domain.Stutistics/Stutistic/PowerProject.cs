//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-12-8 16:25:05
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// 实体类PowerProject 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PowerProject
	{
		public PowerProject()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _stuffname="";
		private string _total="";
		private string _volume="";
		private string _lengths="";
		private string _type="";
		private string _remark="";
		private DateTime _createdate;
		private string _powerlineuid="";
		private string _planstartyear="";
		private string _planendyear="";
        private string _parentid = "";
        private int _sortid = 0;
		#endregion 字段

		#region 属性



        /// <summary>
        /// 
        /// </summary>
        /// 
        public int SortID
        {
            set { _sortid = value; }
            get { return _sortid; }
        }

        public string ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
		/// <summary>
		/// 
		/// </summary>
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
		/// 台数
		/// </summary>
		public string Total
		{
			set{ _total=value;}
			get{return _total;}
		}
		/// <summary>
		/// 容量
		/// </summary>
		public string Volume
		{
			set{ _volume=value;}
			get{return _volume;}
		}
		/// <summary>
		/// 长度
		/// </summary>
		public string Lengths
		{
			set{ _lengths=value;}
			get{return _lengths;}
		}
		/// <summary>
		/// 型号
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
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
		/// 计划开工时间
		/// </summary>
		public string PlanStartYear
		{
			set{ _planstartyear=value;}
			get{return _planstartyear;}
		}
		/// <summary>
		/// 预计投产时间
		/// </summary>
		public string PlanEndYear
		{
			set{ _planendyear=value;}
			get{return _planendyear;}
		}
		#endregion 属性
	}
}

