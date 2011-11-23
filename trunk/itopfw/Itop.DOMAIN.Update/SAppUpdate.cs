//********************************************************************************/
//
//此代码由Itop.NET代码生成器自动生成.
//生成时间:2007-3-2 13:52:28
//
//********************************************************************************/
using System;
namespace Itop.Domain.Update {
    /// <summary>
    /// 文件实体类
    /// </summary>
    [Serializable]
    public class SAppUpdate {
        public SAppUpdate() { }
        public SAppUpdate(string fileName) {
            _filename = fileName;
        }
        #region 字段
        private string _uid =Guid.NewGuid().ToString().Substring(24);
        private string _filename="";
        private string _filepath="";
        private decimal _filesize = 0;
        private byte[] _fileblob=null;
        private string _rq="";
        private string _sysid="";
        private string _sysver="";
        private string _iszip="0"; //0不压缩1压缩
        #endregion 字段

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string UID {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FILENAME {
            set { _filename = value; }
            get { return _filename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FILEPATH {
            set { _filepath = value; }
            get { return _filepath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal FILESIZE {
            set { _filesize = value; }
            get { return _filesize; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] FILEBLOB {
            set { _fileblob = value; }
            get { return _fileblob; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RQ {
            set { _rq = value; }
            get { return _rq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SYSID {
            set { _sysid = value; }
            get { return _sysid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SYSVER {
            set { _sysver = value; }
            get { return _sysver; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ISZIP {
            set { _iszip = value; }
            get { return _iszip; }
        }
        #endregion 属性
    }
}

