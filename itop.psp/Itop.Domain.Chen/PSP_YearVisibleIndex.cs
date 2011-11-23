//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-6-28 11:12:34
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
    /// <summary>
    /// 实体类PSP_YearVisibleIndex 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PSP_YearVisibleIndex
    {
        public PSP_YearVisibleIndex()
        { }
        #region 字段
        private int _id;
        private int _visibleindex;
        private string _year;
        private string _moduleflag = "";
        #endregion 字段

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int VisibleIndex
        {
            set { _visibleindex = value; }
            get { return _visibleindex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Year
        {
            set { _year = value; }
            get { return _year; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModuleFlag
        {
            set { _moduleflag = value; }
            get { return _moduleflag; }
        }
        #endregion 属性
    }
}

