//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-8-17 8:34:05
//
//********************************************************************************/
using System;
namespace Itop.Domain.RightManager
{
	/// <summary>
	/// 实体类Smdgroup 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Smdgroup
	{
		public Smdgroup()
		{}
		#region Domain
		private string _groupno;
		private string _progid;
        private string _progname;
		private string _run;
		private string _ins;
		private string _upd;
		private string _del;
		private string _qry;
		private string _pro;
		private string _prn;
		private string _filterstring;
		private string _hiddencols;
		private string _spec1;
		private string _spec2;
		private string _spec3;
        private string _userid;
        private string _projectuid="";
		/// <summary>
		/// 
        /// 
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
		/// </summary>
		public string Groupno
		{
			set{ _groupno=value;}
			get{return _groupno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Progid
		{
			set{ _progid=value;}
			get{return _progid;}
		}

        public string ProgName
        {
            set { _progname = value; }
            get { return _progname; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string run
		{
			set{ _run=value;}
			get{return _run;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ins
		{
			set{ _ins=value;}
			get{return _ins;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string upd
		{
			set{ _upd=value;}
			get{return _upd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string del
		{
			set{ _del=value;}
			get{return _del;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string qry
		{
			set{ _qry=value;}
			get{return _qry;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pro
		{
			set{ _pro=value;}
			get{return _pro;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string prn
		{
			set{ _prn=value;}
			get{return _prn;}
		}
		/// <summary>
		/// 
		/// </summary>
        /// 
        public string ProjectUID
        {
            set { _projectuid = value; }
            get { return _projectuid; }
        }

		public string filterstring
		{
			set{ _filterstring=value;}
			get{return _filterstring;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string hiddencols
		{
			set{ _hiddencols=value;}
			get{return _hiddencols;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string spec1
		{
			set{ _spec1=value;}
			get{return _spec1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string spec2
		{
			set{ _spec2=value;}
			get{return _spec2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string spec3
		{
			set{ _spec3=value;}
			get{return _spec3;}
		}

        public bool HasInsert()
        {
            return _ins != "0";  
        }

        public bool HasUpdate()
        {
            return _upd != "0"; 
        }

        public bool HasDelete()
        {
            return _del != "0"; 
        }

        public bool HasQuery()
        {
            return _qry != "0"; 
        }

        public bool HasPrint()
        {
            return _prn != "0";
        }

		#endregion Domain
	}
}

