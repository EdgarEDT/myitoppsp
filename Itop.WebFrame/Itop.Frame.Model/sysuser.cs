/**********************************************
这是代码自动生成的，如果重新生成，所做的改动将会丢失
系统:Itop隐患排查
模块:系统平台
Itop.com 版权所有
生成者：Rabbit
生成时间:2011-12-9 16:41:22
***********************************************/

using System;
using System.Threading;
using System.ComponentModel;
namespace Itop.Frame.Model
{
    /// <summary>
    ///[sysuser]业务实体类
    ///对应表名:sysuser
    /// </summary>
    [Serializable]
    public class sysuser
    {
        
        #region Private 成员
        private string _id=Newid(); 
        private string _dwid=String.Empty; 
        private int _orderid=0; 
        private string _loginid=String.Empty; 
        private string _username=String.Empty; 
        private string _isadmin=String.Empty; 
        private string _isuse=String.Empty; 
        private DateTime _lastdate=new DateTime(1900,1,1); 
        private string _lastip=String.Empty; 
        private long _lasttimes=0; 
        private string _sex=String.Empty; 
        private string _job=String.Empty; 
        private string _post=String.Empty; 
        private string _grade=String.Empty; 
        private string _phone1=String.Empty; 
        private string _phone2=String.Empty; 
        private string _mail=String.Empty; 
        private string _adress=String.Empty; 
        private string _education=String.Empty; 
        private string _pwd=String.Empty;   
        #endregion
  
  
        #region Public 成员
   
        /// <summary>
        /// 属性名称：id
        /// 属性描述：用户ID
        /// 字段信息：[id],nvarchar
        /// </summary>
        [Browsable(false)]
        [DisplayNameAttribute("用户ID")]
        public string id
        {
            get { return _id; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[用户ID]长度不能大于50!");
                if (_id as object == null || !_id.Equals(value))
                {
                    _id = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：dwID
        /// 属性描述：单位ID
        /// 字段信息：[dwID],nvarchar
        /// </summary>
        [DisplayNameAttribute("单位ID")]
        public string dwID
        {
            get { return _dwid; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[单位ID]长度不能大于50!");
                if (_dwid as object == null || !_dwid.Equals(value))
                {
                    _dwid = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：orderID
        /// 属性描述：序号
        /// 字段信息：[orderID],int
        /// </summary>
        [DisplayNameAttribute("序号")]
        public int orderID
        {
            get { return _orderid; }
            set
            {			
                if (_orderid as object == null || !_orderid.Equals(value))
                {
                    _orderid = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：LoginID
        /// 属性描述：用户工号
        /// 字段信息：[LoginID],nvarchar
        /// </summary>
        [DisplayNameAttribute("用户工号")]
        public string LoginID
        {
            get { return _loginid; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[用户工号]长度不能大于50!");
                if (_loginid as object == null || !_loginid.Equals(value))
                {
                    _loginid = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：UserName
        /// 属性描述：用户名称
        /// 字段信息：[UserName],nvarchar
        /// </summary>
        [DisplayNameAttribute("用户名称")]
        public string UserName
        {
            get { return _username; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[用户名称]长度不能大于50!");
                if (_username as object == null || !_username.Equals(value))
                {
                    _username = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：isadmin
        /// 属性描述：是否管理员
        /// 字段信息：[isadmin],nvarchar
        /// </summary>
        [DisplayNameAttribute("是否管理员")]
        public string isadmin
        {
            get { return _isadmin; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[是否管理员]长度不能大于50!");
                if (_isadmin as object == null || !_isadmin.Equals(value))
                {
                    _isadmin = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：IsUse
        /// 属性描述：是否有效
        /// 字段信息：[IsUse],nvarchar
        /// </summary>
        [DisplayNameAttribute("是否有效")]
        public string IsUse
        {
            get { return _isuse; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[是否有效]长度不能大于50!");
                if (_isuse as object == null || !_isuse.Equals(value))
                {
                    _isuse = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：LastDate
        /// 属性描述：最近登录日期
        /// 字段信息：[LastDate],datetime
        /// </summary>
        [DisplayNameAttribute("最近登录日期")]
        public DateTime LastDate
        {
            get { return _lastdate; }
            set
            {			
                if (_lastdate as object == null || !_lastdate.Equals(value))
                {
                    _lastdate = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：LastIP
        /// 属性描述：登录IP
        /// 字段信息：[LastIP],nvarchar
        /// </summary>
        [DisplayNameAttribute("登录IP")]
        public string LastIP
        {
            get { return _lastip; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[登录IP]长度不能大于50!");
                if (_lastip as object == null || !_lastip.Equals(value))
                {
                    _lastip = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：LastTimes
        /// 属性描述：登录时间
        /// 字段信息：[LastTimes],bigint
        /// </summary>
        [DisplayNameAttribute("登录时间")]
        public long LastTimes
        {
            get { return _lasttimes; }
            set
            {			
                if (_lasttimes as object == null || !_lasttimes.Equals(value))
                {
                    _lasttimes = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：sex
        /// 属性描述：性别
        /// 字段信息：[sex],nvarchar
        /// </summary>
        [DisplayNameAttribute("性别")]
        public string sex
        {
            get { return _sex; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[性别]长度不能大于50!");
                if (_sex as object == null || !_sex.Equals(value))
                {
                    _sex = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：job
        /// 属性描述：职务
        /// 字段信息：[job],nvarchar
        /// </summary>
        [DisplayNameAttribute("职务")]
        public string job
        {
            get { return _job; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[职务]长度不能大于50!");
                if (_job as object == null || !_job.Equals(value))
                {
                    _job = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：post
        /// 属性描述：岗位
        /// 字段信息：[post],nvarchar
        /// </summary>
        [DisplayNameAttribute("岗位")]
        public string post
        {
            get { return _post; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[岗位]长度不能大于50!");
                if (_post as object == null || !_post.Equals(value))
                {
                    _post = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：grade
        /// 属性描述：级别
        /// 字段信息：[grade],nvarchar
        /// </summary>
        [DisplayNameAttribute("级别")]
        public string grade
        {
            get { return _grade; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[级别]长度不能大于50!");
                if (_grade as object == null || !_grade.Equals(value))
                {
                    _grade = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：phone1
        /// 属性描述：电话
        /// 字段信息：[phone1],nvarchar
        /// </summary>
        [DisplayNameAttribute("电话")]
        public string phone1
        {
            get { return _phone1; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[电话]长度不能大于50!");
                if (_phone1 as object == null || !_phone1.Equals(value))
                {
                    _phone1 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：phone2
        /// 属性描述：手机
        /// 字段信息：[phone2],nvarchar
        /// </summary>
        [DisplayNameAttribute("手机")]
        public string phone2
        {
            get { return _phone2; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[手机]长度不能大于50!");
                if (_phone2 as object == null || !_phone2.Equals(value))
                {
                    _phone2 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：mail
        /// 属性描述：邮箱
        /// 字段信息：[mail],nvarchar
        /// </summary>
        [DisplayNameAttribute("邮箱")]
        public string mail
        {
            get { return _mail; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[邮箱]长度不能大于50!");
                if (_mail as object == null || !_mail.Equals(value))
                {
                    _mail = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：adress
        /// 属性描述：住址
        /// 字段信息：[adress],nvarchar
        /// </summary>
        [DisplayNameAttribute("住址")]
        public string adress
        {
            get { return _adress; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[住址]长度不能大于50!");
                if (_adress as object == null || !_adress.Equals(value))
                {
                    _adress = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：education
        /// 属性描述：学历
        /// 字段信息：[education],nvarchar
        /// </summary>
        [DisplayNameAttribute("学历")]
        public string education
        {
            get { return _education; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[学历]长度不能大于50!");
                if (_education as object == null || !_education.Equals(value))
                {
                    _education = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：pwd
        /// 属性描述：
        /// 字段信息：[pwd],nvarchar
        /// </summary>
        [DisplayNameAttribute("")]
        public string pwd
        {
            get { return _pwd; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[]长度不能大于50!");
                if (_pwd as object == null || !_pwd.Equals(value))
                {
                    _pwd = value;
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
