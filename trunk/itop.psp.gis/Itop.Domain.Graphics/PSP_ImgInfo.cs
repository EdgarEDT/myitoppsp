//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-09-05 16:40:22
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// 实体类PSP_ImgInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PSP_ImgInfo
    {
        public PSP_ImgInfo()
        { }
        #region 字段
        private string _uid = "";
        private string _name = "";
        private string _remark = "";
        private byte[] _image;
        private int _orderid;
        private string _treeid = "";
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
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] Image
        {
            set { _image = value; }
            get { return _image; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int orderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TreeID
        {
            set { _treeid = value; }
            get { return _treeid; }
        }
        #endregion 属性
    }
}

