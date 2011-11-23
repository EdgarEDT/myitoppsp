//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-6-23 9:09:49
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
    /// <summary>
    /// 实体类Line_beicong 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Line_beicong
    {
        public Line_beicong()
        { }
        #region 字段
        private string _uid = Guid.NewGuid().ToString();
        private string _title = "";
        private string _type = "";
        private string _type2 = "";
        private DateTime _createtime = DateTime.Now;
        private string _classtype = "";
        private string _flag = "";
        #endregion 字段

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string UID
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type2
        {
            set { _type2 = value; }
            get { return _type2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassType
        {
            set { _classtype = value; }
            get { return _classtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }
        #endregion 属性
    }
}

