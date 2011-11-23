//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2010-03-02 9:14:09
//
//********************************************************************************/
using System;
namespace Itop.Domain.PWTable
{
	/// <summary>
	/// 实体类PW_tb3a 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PW_tb3a
	{
		public PW_tb3a()
		{}
		#region 字段
		private string _uid="";
		private string _pqname="";
		private string _pqtype="";
		private string _subname="";
		private string _linename="";
		private string _linetype="";
        private decimal _linelength;
        private decimal _num1;
        private decimal _num2;
        private decimal _num3;
        private decimal _num4;
		private int _num5;
        private decimal _num6;
		private int _num7;
        private decimal _num8;
		private string _linesx="";
		private int _kbs;
		private int _kg;
		private string _jxms="";
		private string _llxmc="";
        private decimal _maxfh;
        private decimal _safefh;
        private decimal _fzl ;
		private string _col1="";
		private string _col2="";
		private string _col3="";
        private decimal _wg1;
        private decimal _wg2;
        private decimal _wg3;
        private decimal _wg4;
        private int _kggNum;
        private int _kgzHnNum;
        private int _kgzHwNum;
        private int _gsbNum;
        private string _operDate = "";
        private string _col4 = "";
        private string _col5 = "";
        private string _col6 = "";
        private string _col7 = "";
        private int _num10;
        private int _num11;
        private decimal _num12;
        private decimal _num13;

        public int KggNum
        {
            get { return _kggNum; }
            set { _kggNum = value; }
        }
        public int KgzHnNum
        {
            get { return _kgzHnNum; }
            set { _kgzHnNum = value; }
        }
        public int KgzHwNum
        {
            get { return _kgzHwNum; }
            set { _kgzHwNum = value; }
        }
        public int GsbNum
        {
            get { return _gsbNum; }
            set { _gsbNum = value; }
        }
        public string OperDate
        {
            get { return _operDate; }
            set { _operDate = value; }
        }
        public string col4
        {
            get { return _col4; }
            set { _col4 = value; }
        }
        public string col5
        {
            get { return _col5; }
            set { _col5 = value; }
        }
        public string col6
        {
            get { return _col6; }
            set { _col6 = value; }
        }
        public string col7
        {
            get { return _col7; }
            set { _col7 = value; }
        }
        public int Num10
        {
            get { return _num10; }
            set { _num10 = value; }
        }
        public int Num11
        {
            get { return _num11; }
            set { _num11 = value; }
        }
        public decimal Num12
        {
            get { return _num12; }
            set { _num12 = value; }
        }
        public decimal Num13
        {
            get { return _num13; }
            set { _num13 = value; }
        }
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
		public string PQName
		{
			set{ _pqname=value;}
			get{return _pqname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PQtype
		{
			set{ _pqtype=value;}
			get{return _pqtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SubName
		{
			set{ _subname=value;}
			get{return _subname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LineName
		{
			set{ _linename=value;}
			get{return _linename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LineType
		{
			set{ _linetype=value;}
			get{return _linetype;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal LineLength
		{
			set{ _linelength=value;}
			get{return _linelength;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Num1
		{
			set{ _num1=value;}
			get{return _num1;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Num2
		{
			set{ _num2=value;}
			get{return _num2;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Num3
		{
			set{ _num3=value;}
			get{return _num3;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Num4
		{
			set{ _num4=value;}
			get{return _num4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Num5
		{
			set{ _num5=value;}
			get{return _num5;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Num6
		{
			set{ _num6=value;}
			get{return _num6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Num7
		{
			set{ _num7=value;}
			get{return _num7;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal Num8
		{
			set{ _num8=value;}
			get{return _num8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LineSX
		{
			set{ _linesx=value;}
			get{return _linesx;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int KBS
		{
			set{ _kbs=value;}
			get{return _kbs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int KG
		{
			set{ _kg=value;}
			get{return _kg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JXMS
		{
			set{ _jxms=value;}
			get{return _jxms;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LLXMC
		{
			set{ _llxmc=value;}
			get{return _llxmc;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal MaxFH
		{
			set{ _maxfh=value;}
			get{return _maxfh;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal SafeFH
		{
			set{ _safefh=value;}
			get{return _safefh;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal FZL
		{
			set{ _fzl=value;}
			get{return _fzl;}
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

        public decimal WG1
        {
            get { return _wg1; }
            set { _wg1 = value; }
        }
        public decimal WG2
        {
            get { return _wg2; }
            set { _wg2 = value; }
        }
        public decimal WG3
        {
            get { return _wg3; }
            set { _wg3 = value; }
        }
        public decimal WG4
        {
            get { return _wg4; }
            set { _wg4 = value; }
        }
		#endregion 属性
	}
}

