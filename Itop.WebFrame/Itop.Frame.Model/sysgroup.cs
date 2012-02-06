/**********************************************
这是代码自动生成的，如果重新生成，所做的改动将会丢失
系统:Itop隐患排查
模块:系统平台
Itop.com 版权所有
生成者：Rabbit
生成时间:2011-12-5 14:19:28
***********************************************/

using System;
using System.Threading;
using System.ComponentModel;
namespace Itop.Frame.Model
{
    /// <summary>
    ///[sysgroup]业务实体类
    ///对应表名:sysgroup
    /// </summary>
    [Serializable]
    public class sysgroup
    {
        
        #region Private 成员
        private string _id=Newid(); 
        private int _orderid=0; 
        private string _groupcode=String.Empty; 
        private string _groupname=String.Empty; 
        private string _grouptype=String.Empty; 
        private string _remark=String.Empty;   
        #endregion
  
  
        #region Public 成员
   
        /// <summary>
        /// 属性名称：id
        /// 属性描述：组ID
        /// 字段信息：[id],nvarchar
        /// </summary>
        [Browsable(false)]
        [DisplayNameAttribute("组ID")]
        public string id
        {
            get { return _id; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[组ID]长度不能大于50!");
                if (_id as object == null || !_id.Equals(value))
                {
                    _id = value;
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
        /// 属性名称：GroupCode
        /// 属性描述：组代码
        /// 字段信息：[GroupCode],nvarchar
        /// </summary>
        [DisplayNameAttribute("组代码")]
        public string GroupCode
        {
            get { return _groupcode; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[组代码]长度不能大于50!");
                if (_groupcode as object == null || !_groupcode.Equals(value))
                {
                    _groupcode = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：GroupName
        /// 属性描述：组名称
        /// 字段信息：[GroupName],nvarchar
        /// </summary>
        [DisplayNameAttribute("组名称")]
        public string GroupName
        {
            get { return _groupname; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[组名称]长度不能大于50!");
                if (_groupname as object == null || !_groupname.Equals(value))
                {
                    _groupname = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：GroupType
        /// 属性描述：分类
        /// 字段信息：[GroupType],nvarchar
        /// </summary>
        [DisplayNameAttribute("分类")]
        public string GroupType
        {
            get { return _grouptype; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[分类]长度不能大于50!");
                if (_grouptype as object == null || !_grouptype.Equals(value))
                {
                    _grouptype = value;
                }
            }			 
        }
  
        /// <summary>
        /// 属性名称：Remark
        /// 属性描述：备注
        /// 字段信息：[Remark],nvarchar
        /// </summary>
        [DisplayNameAttribute("备注")]
        public string Remark
        {
            get { return _remark; }
            set
            {			
                if(value==null)return;
                if( value.ToString().Length > 50)
                throw new Exception("[备注]长度不能大于50!");
                if (_remark as object == null || !_remark.Equals(value))
                {
                    _remark = value;
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
