//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-2-28 17:49:15
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
	/// <summary>
	/// 实体类substation 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Dlph
	{
        public Dlph()
		{}
		#region 字段
		private string _uid="";
		private string _eleid="";
        private decimal _ydburthen;
      
        private decimal _sdburthen;

        private decimal _number1;

        private decimal _ydnumber;

        private decimal _number2;

        private string _svguid = "";

        private string _notes = "";

       
       
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
		/// 图元ID
		/// </summary>
		public string EleID
		{
			set{ _eleid=value;}
			get{return _eleid;}
		}
        public decimal Ydnumber
        {
            get { return _ydnumber; }
            set { _ydnumber = value; }
        }
        private decimal _sdnumber;

        public decimal Sdnumber
        {
            get { return _sdnumber; }
            set { _sdnumber = value; }
        }
        public decimal Number1
        {
            get { return _number1; }
            set { _number1 = value; }
        }
        public decimal Sdburthen
        {
            get { return _sdburthen; }
            set { _sdburthen = value; }
        }
        public decimal Ydburthen
        {
            get { return _ydburthen; }
            set { _ydburthen = value; }
        }
        public decimal Number2
        {
            get { return _number2; }
            set { _number2 = value; }
        }
        public string SvgUID
        {
            get { return _svguid; }
            set { _svguid = value; }
        }
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
		#endregion 属性
	}
}

