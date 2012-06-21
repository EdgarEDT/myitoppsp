//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2009-03-24 16:39:54
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// 实体类SVG_LAYER 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SVG_LAYER
    {
        public SVG_LAYER()
        { }
        #region 字段
        private string _suid = "";
        private string _svgid = "";
        private string _name = "";
        private string _xml = "";
        private DateTime _mdate;
        private int _orderid;
        private string _yearid = "";
        private string _layertype = "";
        private string _visibility = "";
        private string _isselect = "";
        private string _ischange = "";
        private string _parentid = "";
        #endregion 字段

        #region 属性
        /// <summary>
        /// LayerID
        /// </summary>
        public string SUID
        {
            set { _suid = value; }
            get { return _suid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string svgID
        {
            set { _svgid = value; }
            get { return _svgid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NAME
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XML
        {
            set { _xml = value; }
            get { return _xml; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime MDATE
        {
            set { _mdate = value; }
            get { return _mdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string YearID
        {
            set { _yearid = value; }
            get { return _yearid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string layerType
        {
            set { _layertype = value; }
            get { return _layertype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string visibility
        {
            set { _visibility = value; }
            get { return _visibility; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsSelect
        {
            set { _isselect = value; }
            get { return _isselect; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsChange
        {
            set { _ischange = value; }
            get { return _ischange; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ParentID {
            set { _parentid = value; }
            get { return _parentid; }
        }
        #endregion 属性
    }
}

