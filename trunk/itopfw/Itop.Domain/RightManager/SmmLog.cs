//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2006-9-5 14:58:50
//
//********************************************************************************/
using System;
namespace Itop.Domain.RightManager
{
    /// <summary>
    /// 实体类SMMLOG 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SMMLOG
    {
        public SMMLOG()
        { }
        #region Domain
        private string _uid=Guid.NewGuid().ToString();
        private string _rq;
        private string _userid;
        private string _czprog;
        private string _cznotes;
        private string _czstate;
        private string _czcompute;
        private string _czip;
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
        public string RQ
        {
            set { _rq = value; }
            get { return _rq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string USERID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CZPROG
        {
            set { _czprog = value; }
            get { return _czprog; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CZNOTES
        {
            set { _cznotes = value; }
            get { return _cznotes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CZSTATE
        {
            set { _czstate = value; }
            get { return _czstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CZCOMPUTE
        {
            set { _czcompute = value; }
            get { return _czcompute; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CZIP
        {
            set { _czip = value; }
            get { return _czip; }
        }
        #endregion Domain
    }
}

