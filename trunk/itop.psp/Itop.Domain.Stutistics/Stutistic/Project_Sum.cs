//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-9-4 11:04:29
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistics
{
	/// <summary>
	/// 实体类Project_Sum 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Project_Sum
	{
		public Project_Sum()
		{}
		#region 字段
		private string _uid="";
		private string _type="";
		private string _name="";
		private string _t1="";
		private string _t2="";
		private string _t3="";
        private string _t4 = "";
        private string _t5 = "";
        private string _l1 = "";
        private string _l2 = "";
        private string _l3 = "";
        private string _l4 = "";
        private string _l5 = "";
		private double _num;
		private string _s1="";
		private string _s2="";
		private string _s3="";
		private string _s4="";
		private string _s5="";
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
		/// 类型
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 典型方案
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 主变台数
		/// </summary>
		public string T1
		{
			set{ _t1=value;}
			get{return _t1;}
		}
		/// <summary>
		/// 出线规模
		/// </summary>
		public string T2
		{
			set{ _t2=value;}
			get{return _t2;}
		}
		/// <summary>
		/// 接线形式
		/// </summary>
		public string T3
		{
			set{ _t3=value;}
			get{return _t3;}
		}

        /// <summary>
        ///无功配置
        /// </summary>
        public string T4
        {
            set { _t4 = value; }
            get { return _t4; }
        }

        /// <summary>
        ///  容量
        /// </summary>
        public string T5
        {
            set { _t5 = value; }
            get { return _t5; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string L1
        {
            set { _l1 = value; }
            get { return _l1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L2
        {
            set { _l2 = value; }
            get { return _l2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string L3
        {
            set { _l3 = value; }
            get { return _l3; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string L4
        {
            set { _l4 = value; }
            get { return _l4; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string L5
        {
            set { _l5 = value; }
            get { return _l5; }
        }










		/// <summary>
		/// 静态投资
		/// </summary>
		public double Num
		{
			set{ _num=value;}
			get{return _num;}
		}
		/// <summary>
		/// 自定义列
		/// </summary>
		public string S1
		{
			set{ _s1=value;}
			get{return _s1;}
		}
		/// <summary>
		/// 自定义列
		/// </summary>
		public string S2
		{
			set{ _s2=value;}
			get{return _s2;}
		}
		/// <summary>
		/// 自定义列
		/// </summary>
		public string S3
		{
			set{ _s3=value;}
			get{return _s3;}
		}
		/// <summary>
		/// 自定义列
		/// </summary>
		public string S4
		{
			set{ _s4=value;}
			get{return _s4;}
		}
		/// <summary>
		/// 自定义列
		/// </summary>
		public string S5
		{
			set{ _s5=value;}
			get{return _s5;}
		}
		#endregion 属性
	}
}

