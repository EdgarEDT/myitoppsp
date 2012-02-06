/**********************************************
这是代码自动生成的，如果重新生成，所做的改动将会丢失
系统:Itop隐患排查
模块:系统平台
Itop.com 版权所有
生成者：Rabbit
生成时间:2011-12-5 14:19:30
***********************************************/

using System;
using System.Threading;
using System.ComponentModel;
namespace Itop.Frame.Model
{
    /// <summary>
    ///[sysprog]业务实体类
    ///对应表名:sysprog
    /// </summary>
    [Serializable]
    public class sysprog
    {
        
        #region Private 成员
        private string _id=Newid(); 
        private string _parentid=String.Empty; 
        private int _orderid=0; 
        private string _progcode=String.Empty; 
        private string _progname=String.Empty; 
        private string _progclass=String.Empty; 
        private string _progprop=String.Empty; 
        private string _progicon1=String.Empty; 
        private string _progicon2=String.Empty; 
        private string _isgroup=String.Empty; 
        private string _funs=String.Empty; 
        private string _isvisible=String.Empty; 
        private string _isuse=String.Empty; 
        private string _iscore=String.Empty;   
        #endregion
  
  
        #region Public 成员
   
        /// <summary>
        /// 属性名称：id
        /// 属性描述：模块ID
        /// 字段信息：[id],nvarchar
        /// </summary>
        [Browsable(false)]
        [DisplayNameAttribute("模块ID")]
        public string id
        {
            get { return _id; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[模块ID]长度不能大于50!");
                if (_id as object == null || !_id.Equals(value))
                {
                    _id = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：ParentID
        /// 属性描述：ParentID
        /// 字段信息：[ParentID],nvarchar
        /// </summary>
        [DisplayNameAttribute("ParentID")]
        public string ParentID
        {
            get { return _parentid; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[ParentID]长度不能大于50!");
                if (_parentid as object == null || !_parentid.Equals(value))
                {
                    _parentid = value;
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
        /// 属性名称：ProgCode
        /// 属性描述：模块代码
        /// 字段信息：[ProgCode],nvarchar
        /// </summary>
        [DisplayNameAttribute("模块代码")]
        public string ProgCode
        {
            get { return _progcode; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[模块代码]长度不能大于50!");
                if (_progcode as object == null || !_progcode.Equals(value))
                {
                    _progcode = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：ProgName
        /// 属性描述：模块名称
        /// 字段信息：[ProgName],nvarchar
        /// </summary>
        [DisplayNameAttribute("模块名称")]
        public string ProgName
        {
            get { return _progname; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 150)
                throw new Exception("[模块名称]长度不能大于150!");
                if (_progname as object == null || !_progname.Equals(value))
                {
                    _progname = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：ProgClass
        /// 属性描述：模块入口类
        /// 字段信息：[ProgClass],nvarchar
        /// </summary>
        [DisplayNameAttribute("模块入口类")]
        public string ProgClass
        {
            get { return _progclass; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 250)
                throw new Exception("[模块入口类]长度不能大于250!");
                if (_progclass as object == null || !_progclass.Equals(value))
                {
                    _progclass = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：ProgProp
        /// 属性描述：入口参数
        /// 字段信息：[ProgProp],nvarchar
        /// </summary>
        [DisplayNameAttribute("入口参数")]
        public string ProgProp
        {
            get { return _progprop; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[入口参数]长度不能大于50!");
                if (_progprop as object == null || !_progprop.Equals(value))
                {
                    _progprop = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：ProgIcon1
        /// 属性描述：小图标
        /// 字段信息：[ProgIcon1],nvarchar
        /// </summary>
        [DisplayNameAttribute("小图标")]
        public string ProgIcon1
        {
            get { return _progicon1; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[小图标]长度不能大于50!");
                if (_progicon1 as object == null || !_progicon1.Equals(value))
                {
                    _progicon1 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：ProgIcon2
        /// 属性描述：大图标
        /// 字段信息：[ProgIcon2],nvarchar
        /// </summary>
        [DisplayNameAttribute("大图标")]
        public string ProgIcon2
        {
            get { return _progicon2; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[大图标]长度不能大于50!");
                if (_progicon2 as object == null || !_progicon2.Equals(value))
                {
                    _progicon2 = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：IsGroup
        /// 属性描述：IsGroup
        /// 字段信息：[IsGroup],nvarchar
        /// </summary>
        [DisplayNameAttribute("IsGroup")]
        public string IsGroup
        {
            get { return _isgroup; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[IsGroup]长度不能大于50!");
                if (_isgroup as object == null || !_isgroup.Equals(value))
                {
                    _isgroup = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：funs
        /// 属性描述：功能串
        /// 字段信息：[funs],nvarchar
        /// </summary>
        [DisplayNameAttribute("功能串")]
        public string funs
        {
            get { return _funs; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[功能串]长度不能大于50!");
                if (_funs as object == null || !_funs.Equals(value))
                {
                    _funs = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：IsVisible
        /// 属性描述：IsVisible
        /// 字段信息：[IsVisible],nvarchar
        /// </summary>
        [DisplayNameAttribute("IsVisible")]
        public string IsVisible
        {
            get { return _isvisible; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[IsVisible]长度不能大于50!");
                if (_isvisible as object == null || !_isvisible.Equals(value))
                {
                    _isvisible = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：IsUse
        /// 属性描述：IsUse
        /// 字段信息：[IsUse],nvarchar
        /// </summary>
        [DisplayNameAttribute("IsUse")]
        public string IsUse
        {
            get { return _isuse; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[IsUse]长度不能大于50!");
                if (_isuse as object == null || !_isuse.Equals(value))
                {
                    _isuse = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：IsCore
        /// 属性描述：IsCore
        /// 字段信息：[IsCore],nvarchar
        /// </summary>
        [DisplayNameAttribute("IsCore")]
        public string IsCore
        {
            get { return _iscore; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[IsCore]长度不能大于50!");
                if (_iscore as object == null || !_iscore.Equals(value))
                {
                    _iscore = value;
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
