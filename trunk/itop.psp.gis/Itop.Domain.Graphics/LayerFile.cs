//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-6-13 16:56:08
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// 实体类LayerFile 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class LayerFile
    {
        public LayerFile()
        { }
        #region 字段
        private string _suid = "";
        private string _layerid = "";
        private string _layerfilename = "";
        private string _svgdatauid = "";
        private string _layerouterxml = "";
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
        public string LayerID
        {
            set { _layerid = value; }
            get { return _layerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LayerFileName
        {
            set { _layerfilename = value; }
            get { return _layerfilename; }
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
        public string LayerOuterXml
        {
            set { _layerouterxml = value; }
            get { return _layerouterxml; }
        }
        #endregion 属性
    }
}

