//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-8-25 8:51:13
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// 实体类PSPDIR 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PSPDIR
    {
        public PSPDIR()
        { }
        #region 字段
        private string _fileguid = "";
        private string _filename = "";
        private string _filetype = "";
        private string _createtime = "";
        #endregion 字段

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string FileGUID
        {
            set { _fileguid = value; }
            get { return _fileguid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FileType
        {
            set { _filetype = value; }
            get { return _filetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion 属性
    }
}

