//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2008-6-27 15:13:55
//
//********************************************************************************/
using System;
namespace Itop.Domain.HistoryValue
{
    /// <summary>
    /// 实体类PSP_GDPBaseYear 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PSP_GDPBaseYear
    {
        public PSP_GDPBaseYear()
        { }
        #region 字段
        private int _id;
        private string _baseyear = "";
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
        /// 
        /// </summary>
        public string BaseYear
        {
            set { _baseyear = value; }
            get { return _baseyear; }
        }
        #endregion 属性
    }
}

