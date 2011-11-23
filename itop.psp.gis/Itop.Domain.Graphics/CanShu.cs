//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2010-8-5 15:56:56
//
//********************************************************************************/
using System;
namespace Itop.Domain.Graphics
{
    /// <summary>
    /// 实体类CanShu 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class CanShu
    {
        public CanShu()
        {
            ID = Guid.NewGuid().ToString();
        }
        #region 字段
        private string _id = "";
        private string _title = "";
        private double _value;
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
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            set { _value = value; }
            get { return _value; }
        }
        #endregion 属性
    }
}

