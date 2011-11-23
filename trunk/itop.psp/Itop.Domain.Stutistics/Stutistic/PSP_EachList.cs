//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-9-15 11:57:20
//
//********************************************************************************/
using System;
namespace Itop.Domain.Stutistic
{
    /// <summary>
    /// 实体类PSP_EachList 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PSP_EachList
    {
        public PSP_EachList()
        { }
        #region 字段
        private int _id;
        private string _listname = "";
        private string _remark = "";
        private DateTime _createdate;
        private string _parentid = "";
        private string _types = "";
        #endregion 字段

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ListName
        {
            set { _listname = value; }
            get { return _listname; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Types
        {
            set { _types = value; }
            get { return _types; }
        }
        #endregion 属性
    }
}

