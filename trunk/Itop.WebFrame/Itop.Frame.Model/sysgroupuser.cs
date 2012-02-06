/**********************************************
这是代码自动生成的，如果重新生成，所做的改动将会丢失
系统:Itop隐患排查
模块:系统平台
Itop.com 版权所有
生成者：Rabbit
生成时间:2011-12-5 14:19:29
***********************************************/

using System;
using System.Threading;
using System.ComponentModel;
namespace Itop.Frame.Model
{
    /// <summary>
    ///[sysgroupuser]业务实体类
    ///对应表名:sysgroupuser
    /// </summary>
    [Serializable]
    public class sysgroupuser
    {
        
        #region Private 成员
        private string _groupid=Newid(); 
        private string _userid=Newid();
        private string _id;
        private string _groupname;
        #endregion
  
  
        #region Public 成员
   
        /// <summary>
        /// 属性名称：GroupID
        /// 属性描述：组ID
        /// 字段信息：[GroupID],nvarchar
        /// </summary>
        [Browsable(false)]
        [DisplayNameAttribute("组ID")]
        public string GroupID
        {
            get { return _groupid; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[组ID]长度不能大于50!");
                if (_groupid as object == null || !_groupid.Equals(value))
                {
                    _groupid = value;
                }
            }			 
        }
        [Browsable(false)]
        [DisplayNameAttribute("ID")]
        public string id {
            get { return _id; }
            set {
                if (value == null) return;
                if (value.ToString().Length > 50)
                    throw new Exception("[ID]长度不能大于50!");
                if (_id as object == null || !_id.Equals(value)) {
                    _id = value;
                }
            }
        }
        /// <summary>
        /// 属性名称：UserID
        /// 属性描述：用户ID
        /// 字段信息：[UserID],nvarchar
        /// </summary>
        [Browsable(false)]
        [DisplayNameAttribute("用户ID")]
        public string UserID
        {
            get { return _userid; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[用户ID]长度不能大于50!");
                if (_userid as object == null || !_userid.Equals(value))
                {
                    _userid = value;
                }
            }			 
        }
        [DisplayNameAttribute("组名称")]
        public string GroupName {
            get { return _groupname; }
            set {
                if (value == null) return;
                if (value.ToString().Length > 50)
                    throw new Exception("[组名称]长度不能大于50!");
                if (_groupname as object == null || !_groupname.Equals(value)) {
                    _groupname = value;
                }
            }
        }
        #endregion 
  
        #region 方法
        public static string Newid(){
            return DateTime.Now.ToString("yyyyMMddHHmmssffffff");
        }
        public string CreateID(){
            Thread.Sleep(new TimeSpan(100000));//0.1毫秒
            return DateTime.Now.ToString("yyyyMMddHHmmssffffff");
        }
        #endregion		
    }	
}
