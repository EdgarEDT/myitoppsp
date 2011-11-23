//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2010-3-3 13:29:41
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// 实体类PSP_ELCPROJECT 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PSP_ELCPROJECT
    {
        public PSP_ELCPROJECT()
        {
            ID = Guid.NewGuid().ToString();
        }
        #region 字段
        private string _id = "";
        private string _name = "";
        private string _class = "";
        private string _projectid = "";
        private string _filetype = "";
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
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Class
        {
            set { _class = value; }
            get { return _class; }
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
        public string FileType
        {
            set { _filetype = value; }
            get { return _filetype; }
        }
        #endregion 属性
    }
}

