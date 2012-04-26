//********************************************************************************/
//
//此代码由TONLI.NET代码生成器自动生成.
//生成时间:2012-4-23 10:58:53
//
//********************************************************************************/
using System;
namespace Itop.Domain.RightManager
{
    /// <summary>
    /// 实体类ProjectUser 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ProjectUser
    {
        public ProjectUser()
        { }
        #region 字段
        private string _uid = "";
        private string _userid = "";
        private string _username = "";
        private int _sort;
        private int _used;
        private string _remark = "";
        #endregion 字段

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string UID
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
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
        public int used
        {
            set { _used = value; }
            get { return _used; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion 属性
    }
}

