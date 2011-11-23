//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-11-29 16:07:48
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// 实体类PSP_Types 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PowerStuffTypes
	{
        public PowerStuffTypes()
		{}
		#region 字段
		private int _id;
		private string _title="";
		private int _flag;
		private string _flag2;
		private int _parentid;
        private string code;
        private string remark;

        //public double Jingtai
        //{
        //    get { return jingtai; }
        //    set { jingtai = value; }
        //}
		#endregion 字段

		#region 属性
		/// <summary>
		/// 
		/// </summary>
        /// 
        //public double Lixi
        //{
        //    set { lixi = value; }
        //    get { return lixi; }
        //}

        public string Code
        {
            set { code = value; }
            get { return code; }
        }

        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }

		public int ID
		{
			set{ _id=value;}
			get{return _id;}
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
		public int Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Flag2
		{
			set{ _flag2=value;}
			get{return _flag2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		#endregion 属性
	}
}

