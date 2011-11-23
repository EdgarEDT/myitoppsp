//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-7-9 15:48:20
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
	/// <summary>
	/// 实体类PowerSubstationLine 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class PowerSubstationLine
	{
		public PowerSubstationLine()
		{}
		#region 字段
		private string _uid=Guid.NewGuid().ToString();
		private string _title="";
		private string _type="";
        private string _type2 = "";
        private string _classtype = "";
		private string _flag="";
		private DateTime _createtime;
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
		/// 字段S
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}

        /// <summary>
        /// 字段S
        /// </summary>
        public string Type2
        {
            set { _type2 = value; }
            get { return _type2; }
        }


        /// <summary>
        /// 字段S
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }

		/// <summary>
		/// 标记 1变电站 2线路
		/// </summary>
		public string Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion 属性
	}
}

