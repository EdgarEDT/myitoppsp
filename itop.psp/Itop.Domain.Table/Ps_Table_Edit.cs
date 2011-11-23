//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2009-12-4 8:20:39
//
//********************************************************************************/
using System;
namespace Itop.Domain.Table
{
    /// <summary>
    /// 实体类Ps_Table_Edit 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Ps_Table_Edit
    {
        public Ps_Table_Edit()
        { }
        #region 字段
        private string _id = Guid.NewGuid().ToString();
        private string _projectid = "";
        private string _parentid = "";
        private int _sort;
        private string _status = "";
        private string _startyear = "";
        private string _finishyear = "";
        private string _volume = "";
        private string _col1 = "";
        private string _col2 = "";
        private string _col3 = "";
        private string _col4 = "";
        private string _col5 = "";
        #endregion 字段

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StartYear
        {
            set { _startyear = value; }
            get { return _startyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FinishYear
        {
            set { _finishyear = value; }
            get { return _finishyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Volume
        {
            set { _volume = value; }
            get { return _volume; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Col1
        {
            set { _col1 = value; }
            get { return _col1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Col2
        {
            set { _col2 = value; }
            get { return _col2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Col3
        {
            set { _col3 = value; }
            get { return _col3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Col4
        {
            set { _col4 = value; }
            get { return _col4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Col5
        {
            set { _col5 = value; }
            get { return _col5; }
        }
        #endregion 属性
    }
}

