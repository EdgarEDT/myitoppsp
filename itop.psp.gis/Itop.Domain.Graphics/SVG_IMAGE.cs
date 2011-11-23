//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2009-04-09 16:58:26
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// 实体类SVG_IMAGE 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SVG_IMAGE
    {
        public SVG_IMAGE()
        { }
        #region 字段
        private string _suid = "";
        private string _svgid = "";
        private string _name = "";
        private string _xml = "";
        private DateTime _mdate;
        private string _layerid = "";
        private byte[] _image;
        private string _col1 = "";
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
        public string layerID
        {
            set { _layerid = value; }
            get { return _layerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] image
        {
            set { _image = value; }
            get { return _image; }
        }
        /// <summary>
        /// 文件格式
        /// </summary>
        public string col1
        {
            set { _col1 = value; }
            get { return _col1; }
        }
        #endregion 属性
    }
}

