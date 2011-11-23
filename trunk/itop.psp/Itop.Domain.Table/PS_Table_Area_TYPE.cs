//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2010-6-12 9:37:38
//
//********************************************************************************/
using System;
namespace Itop.Domain.Table
{
    /// <summary>
    /// 实体类PS_Table_Area_TYPE 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PS_Table_Area_TYPE
    {
        public PS_Table_Area_TYPE()
        { }
        #region 字段
        private string _id = Guid.NewGuid().ToString();
        private string _parentid = "";
        private string _projectid = "";
        private int _sort;
        private string _title = "";
        private string _voltage = "";
        private double _volumn;
        private string _col1 = "";
        private string _col2 = "";
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
        public string ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
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
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
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
        public string Voltage
        {
            set { _voltage = value; }
            get { return _voltage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Volumn
        {
            set { _volumn = value; }
            get { return _volumn; }
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
        #endregion 属性
    }
}

