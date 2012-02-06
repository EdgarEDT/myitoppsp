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
    ///[sysgroupprog]业务实体类
    ///对应表名:sysgroupprog
    /// </summary>
    [Serializable]
    public class sysgroupprog
    {
        
        #region Private 成员
        private string _id=Newid(); 
        private string _groupid=String.Empty; 
        private string _progid=String.Empty;
        private string _progname = String.Empty; 
        private string _fun1=("0"); 
        private string _fun2=("0"); 
        private string _fun3=("0"); 
        private string _fun4=("0"); 
        private string _fun5=("0"); 
        private string _fun6=("0"); 
        private string _fun7=("0"); 
        private string _fun8=("0"); 
        private string _fun9=("0");   
        #endregion
  
  
        #region Public 成员
   
        /// <summary>
        /// 属性名称：id
        /// 属性描述：
        /// 字段信息：[id],nvarchar
        /// </summary>
        [Browsable(false)]
        [DisplayNameAttribute("")]
        public string id
        {
            get { return _id; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[]长度不能大于50!");
                if (_id as object == null || !_id.Equals(value))
                {
                    _id = value;
                }
            }			 
        }
  
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
  
        /// <summary>
        /// 属性名称：ProgID
        /// 属性描述：模块ID
        /// 字段信息：[ProgID],nvarchar
        /// </summary>
        [Browsable(false)]
        [DisplayNameAttribute("模块ID")]
        public string ProgID
        {
            get { return _progid; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[模块ID]长度不能大于50!");
                if (_progid as object == null || !_progid.Equals(value))
                {
                    _progid = value;
                }
            }			 
        }

        [DisplayNameAttribute("模块名")]
        public string ProgName {
            get { return _progname; }
            set {
                if (value == null) return;
                if (value.ToString().Length > 50)
                    throw new Exception("[模块名]长度不能大于50!");
                if (_progname as object == null || !_progname.Equals(value)) {
                    _progname = value;
                }
            }
        }
        /// <summary>
        /// 属性名称：fun1
        /// 属性描述：增加
        /// 字段信息：[fun1],nvarchar
        /// </summary>
        [DisplayNameAttribute("增加")]
        public string fun1
        {
            get { return _fun1; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[增加]长度不能大于50!");
                if (_fun1 as object == null || !_fun1.Equals(value))
                {
                    _fun1 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：fun2
        /// 属性描述：修改
        /// 字段信息：[fun2],nvarchar
        /// </summary>
        [DisplayNameAttribute("修改")]
        public string fun2
        {
            get { return _fun2; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[修改]长度不能大于50!");
                if (_fun2 as object == null || !_fun2.Equals(value))
                {
                    _fun2 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：fun3
        /// 属性描述：删除
        /// 字段信息：[fun3],nvarchar
        /// </summary>
        [DisplayNameAttribute("删除")]
        public string fun3
        {
            get { return _fun3; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[删除]长度不能大于50!");
                if (_fun3 as object == null || !_fun3.Equals(value))
                {
                    _fun3 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：fun4
        /// 属性描述：fun4
        /// 字段信息：[fun4],nvarchar
        /// </summary>
        [DisplayNameAttribute("fun4")]
        public string fun4
        {
            get { return _fun4; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[fun4]长度不能大于50!");
                if (_fun4 as object == null || !_fun4.Equals(value))
                {
                    _fun4 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：fun5
        /// 属性描述：fun5
        /// 字段信息：[fun5],nvarchar
        /// </summary>
        [DisplayNameAttribute("fun5")]
        public string fun5
        {
            get { return _fun5; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[fun5]长度不能大于50!");
                if (_fun5 as object == null || !_fun5.Equals(value))
                {
                    _fun5 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：fun6
        /// 属性描述：fun6
        /// 字段信息：[fun6],nvarchar
        /// </summary>
        [DisplayNameAttribute("fun6")]
        public string fun6
        {
            get { return _fun6; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[fun6]长度不能大于50!");
                if (_fun6 as object == null || !_fun6.Equals(value))
                {
                    _fun6 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：fun7
        /// 属性描述：fun7
        /// 字段信息：[fun7],nvarchar
        /// </summary>
        [DisplayNameAttribute("fun7")]
        public string fun7
        {
            get { return _fun7; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[fun7]长度不能大于50!");
                if (_fun7 as object == null || !_fun7.Equals(value))
                {
                    _fun7 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：fun8
        /// 属性描述：fun8
        /// 字段信息：[fun8],nvarchar
        /// </summary>
        [DisplayNameAttribute("fun8")]
        public string fun8
        {
            get { return _fun8; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[fun8]长度不能大于50!");
                if (_fun8 as object == null || !_fun8.Equals(value))
                {
                    _fun8 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：fun9
        /// 属性描述：fun9
        /// 字段信息：[fun9],nvarchar
        /// </summary>
        [DisplayNameAttribute("fun9")]
        public string fun9
        {
            get { return _fun9; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[fun9]长度不能大于50!");
                if (_fun9 as object == null || !_fun9.Equals(value))
                {
                    _fun9 = value;
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
