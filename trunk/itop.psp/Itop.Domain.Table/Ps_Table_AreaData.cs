//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2010-3-15 11:03:12
//
//********************************************************************************/
using System;
namespace Itop.Domain.Table
{
    /// <summary>
    /// 实体类Ps_Table_AreaData 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Ps_Table_AreaData
    {
        public Ps_Table_AreaData()
        { }
        #region 字段
        private string _id = Guid.NewGuid().ToString();
        private string _area = "";
        private string _parentid = "";
        private string _projectid = "";
        private int _sortid;
        private int _yearf;
        private double _population;
        private double _totalarea;
        private double _cityarea;
        private string _col1 = "";
        private string _col2 = "";
        private string _col3 = "";
        private string _col4 = "";
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
        public string Area
        {
            set { _area = value; }
            get { return _area; }
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
        public int SortID
        {
            set { _sortid = value; }
            get { return _sortid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Yearf
        {
            set { _yearf = value; }
            get { return _yearf; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Population
        {
            set { _population = value; }
            get { return _population; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double TotalArea
        {
            set { _totalarea = value; }
            get { return _totalarea; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double CityArea
        {
            set { _cityarea = value; }
            get { return _cityarea; }
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
        #endregion 属性
    }
}

