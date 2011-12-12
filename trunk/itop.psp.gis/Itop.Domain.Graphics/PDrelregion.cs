//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2010-3-3 13:29:41
//
//********************************************************************************/
using System;
using System.ComponentModel;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// 实体类PSP_ELCPROJECT 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PDrelregion
    {
        public PDrelregion()
        {
            ID = Guid.NewGuid().ToString();
        }
        #region 字段
        private string _id = "";
        private string _areaname = "";
        private int _PeopleSum = 0;
        private string _projectid = "";
        private int _Year = 2000;
        private string _Title = "";
        private string _S1 = "";
        private string _S2 = "";
        private string _S3 = "";
        private string _S4 = "";


        #endregion 字段

        #region 属性

        /// <summary>
       
        /// 属性描述：记录ID
        /// 字段信息：[gzrjID],nvarchar
        /// </summary>
        [Browsable(false)]
        [DisplayNameAttribute("ID")]
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
         /// <summary>
       
        /// 属性描述：记录ID
        /// 字段信息：[gzrjID],nvarchar
        /// </summary>
        [DisplayNameAttribute("地区")]
        public string AreaName
        {
            set { _areaname = value; }
            get { return _areaname; }
        }
        /// <summary>
        /// 
        /// </summary>
		[DisplayNameAttribute("人口数")]
        public int PeopleSum
        {
            set { _PeopleSum = value; }
            get { return _PeopleSum; }
        }
        /// <summary>
        /// 
        /// </summary>
		[Browsable(false)]
        [DisplayNameAttribute("ProjectID")]
        public string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
		 /// <summary>
        /// 
        /// </summary>
		[DisplayNameAttribute("年份")]
        public int Year
        {
            set { _Year = value; }
            get { return _Year; }
        }
        /// <summary>
        /// 
        /// </summary>
       [Browsable(false)]
        [DisplayNameAttribute("标题")]
        public string Title
        {
            set { _Title = value; }
            get { return _Title; }
        }
         /// <summary>
        /// 
        /// </summary>
       [Browsable(false)]
        [DisplayNameAttribute("S1")]
        public string S1
        {
            set { _S1 = value; }
            get { return _S1; }
        }
       /// <summary>
        /// 
        /// </summary>
       [Browsable(false)]
        [DisplayNameAttribute("S2")]
        public string S2
        {
            set { _S2 = value; }
            get { return _S2; }
        }
        /// <summary>
        /// 
        /// </summary>
       [Browsable(false)]
        [DisplayNameAttribute("S3")]
        public string S3
        {
            set { _S3 = value; }
            get { return _S3; }
        }
        /// <summary>
        /// 
        /// </summary>
       [Browsable(false)]
        [DisplayNameAttribute("S4")]
        public string S4
        {
            set { _S4 = value; }
            get { return _S4; }
        }

        #endregion 属性

    }
}

