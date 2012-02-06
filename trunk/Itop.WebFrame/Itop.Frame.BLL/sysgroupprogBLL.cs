
/**********************************************
这是代码自动生成的，如果重新生成，所做的改动将会丢失
系统:Itop隐患排查
模块:系统平台
Itop.com 版权所有
生成者：Rabbit
生成时间:2011-11-29 14:18:33
***********************************************/
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Ebada.Components;
using Itop.Frame.Model;
namespace Itop.Frame.BLL
{
    /// <summary>
    /// [sysgroupprog]的业务逻辑
    /// 该BLL已经生成了基本的业务操作
    /// </summary>
    public class sysgroupprogBLL
    {
        private IBaseSqlMapDao _sysgroupprogDao;
        /// <summary>
        /// 方法名称: sysgroupprogBLL
        /// 内容摘要: 构造函数进行初始化
        /// </summary>
        public sysgroupprogBLL(IBaseSqlMapDao dao)
        {
            _sysgroupprogDao = dao;
        }

        #region 基本业务公开函数
        /// <summary>
        /// 方法名称: Get
        /// 内容摘要: 由键值获取一个实体对象
        /// </summary>
        /// <returns>sysgroupprog</returns>
        public virtual sysgroupprog Get(string PK)
        {
            return _sysgroupprogDao.GetOneByKey<sysgroupprog>(PK);
        }

        /// <summary>
        /// 方法名称: Select
        /// 内容摘要: 基本查询，不带任何条件的查询
        /// </summary>
        /// <returns>IList</returns>
        public virtual IList<sysgroupprog> Select()
        {
            return _sysgroupprogDao.GetList<sysgroupprog>(null);
        }
        /// <summary>
        /// 方法名称: Select
        /// 内容摘要: 带where条件的查询
        /// </summary>
        /// <param name="where">条件sql</param>
        /// <returns></returns>
        public virtual IList<sysgroupprog> SelectByWhere(string where)
        {
            return _sysgroupprogDao.GetListByWhere<sysgroupprog>(where);
        }
        /// <summary>
        /// 方法名称: Insert
        /// 内容摘要: 插入一条新纪录
        /// </summary>
        /// <returns>int</returns>
        public virtual object Insert(sysgroupprog obj)
        {
			object result = null;
            try
            {
                result=_sysgroupprogDao.Create<sysgroupprog>(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
			return result;
        }

        /// <summary>
        /// 方法名称: Update
        /// 内容摘要: 更新一条新纪录
        /// </summary>
        /// <returns>int</returns>
        public virtual int Update(sysgroupprog obj)
        {
            int result = 0;
            try
            {
                result = _sysgroupprogDao.Update<sysgroupprog>(obj);
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        /// <summary>
        /// 方法名称: Delete
        /// 内容摘要: 删除一条新纪录
        /// </summary>
        /// <returns>int</returns>
        public virtual int Delete(string PK)
        {
            int result = 0;
            try
            {
                result = _sysgroupprogDao.DeleteByKey<sysgroupprog>(PK);
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
		
        /// <summary>
        /// 方法名称: 条件查询
        /// 内容摘要: 条件查询
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable SelectByHashtable(Hashtable ht)
        {
            DataTable result = null;
            try
            {
                result = _sysgroupprogDao.GetDataTable("SelectsysgroupprogByHash", ht);
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }		

        #endregion

    }
}

 