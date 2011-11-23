//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-6-17 9:13:38
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// 实体类LayerGrade 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class LayerGrade
    {
        public LayerGrade()
        { }
        #region 字段
        private string _suid = "";
        private string _name = "";
        private string _svgdatauid = "";
        private string _parentid = "";
        private string _type = "";
        #endregion 字段

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string SUID
        {
            set { _suid = value; }
            get { return _suid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SvgDataUid
        {
            set { _svgdatauid = value; }
            get { return _svgdatauid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        #endregion 属性
    }
}

