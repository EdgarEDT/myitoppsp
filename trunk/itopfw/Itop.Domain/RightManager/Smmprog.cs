using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Domain.RightManager{
    /// <summary>
    /// 实体类Smmprog 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Smmprog {
        public Smmprog() {
            _progid = Guid.NewGuid().ToString();
            _methodname = "Execute";
            _parentid = string.Empty;
            _assemblyname = string.Empty; 
            _classname = string.Empty;
            _progname = string.Empty;
            _progtype = string.Empty;
            _projectuid = string.Empty;
            _userid = string.Empty;
            _progModuleType = "1";
            _progico = "0";
            
        }
        #region Model
        private string _progid;
        private string _progname;
        private string _remark;
        private string _proglevel;
        private string _assemblyname;
        private string _classname;
        private string _methodname;
        private string _parentid;
        private int _index;
        private string _progtype;
        private string _progModuleType;
        private string _projectuid;
        private string _userid;
        private string _progico;
        /// <summary>
        /// 序号
        /// </summary>
        public int Index {
            get { return _index; }
            set { _index = value; }
        }

        public string ProgIco
        {
            get { return _progico; }
            set { _progico = value; }
        }



        public string ProjectUID
        {
            set { _projectuid = value; }
            get { return _projectuid; }
        }

        public string UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 模块或目录的唯一标识
        /// </summary>
        public string ProgId {
            set { _progid = value; }
            get { return _progid; }
        }
        /// <summary>
        /// 模块或目录的名称
        /// </summary>
        public string ProgName {
            set { _progname = value; }
            get { return _progname; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {
            set { _remark = value; }
            get { return _remark; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string ProgModuleType
        {
            set { _progModuleType = value; }
            get { return _progModuleType; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProgLevel {
            set { _proglevel = value; }
            get { return _proglevel; }
        }
        /// <summary>
        /// 程序集的文件名
        /// </summary>
        public string AssemblyName {
            set { _assemblyname = value; }
            get { return _assemblyname; }
        }
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName {
            set { _classname = value; }
            get { return _classname; }
        }
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName {
            set { _methodname = value; }
            get { return _methodname; }
        }
        /// <summary>
        /// 上级目录唯一标识(如果是顶级目录值为string.empty)
        /// </summary>
        public string ParentId {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 程序类型,暂时分为目录和程序两种
        /// </summary>
        public string ProgType {
            set { _progtype = value; }
            get { return _progtype; }
        }
        #endregion Model
    }
}
