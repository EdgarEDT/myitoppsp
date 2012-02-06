
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
    /// [sysgroup]的业务逻辑
    /// 该BLL已经生成了基本的业务操作
    /// </summary>
    public class sysgroupBLL
    {
        private IBaseSqlMapDao _sysgroupDao;
        /// <summary>
        /// 方法名称: sysgroupBLL
        /// 内容摘要: 构造函数进行初始化
        /// </summary>
        public sysgroupBLL(IBaseSqlMapDao dao)
        {
            _sysgroupDao = dao;
        }

        #region 基本业务公开函数
        /// <summary>
        /// 方法名称: Get
        /// 内容摘要: 由键值获取一个实体对象
        /// </summary>
        /// <returns>sysgroup</returns>
        public virtual sysgroup Get(string PK)
        {
            return _sysgroupDao.GetOneByKey<sysgroup>(PK);
        }

        /// <summary>
        /// 方法名称: Select
        /// 内容摘要: 基本查询，不带任何条件的查询
        /// </summary>
        /// <returns>IList</returns>
        public virtual IList<sysgroup> Select()
        {
            return _sysgroupDao.GetList<sysgroup>(null);
        }
        /// <summary>
        /// 方法名称: Select
        /// 内容摘要: 带where条件的查询
        /// </summary>
        /// <param name="where">条件sql</param>
        /// <returns></returns>
        public virtual IList<sysgroup> SelectByWhere(string where)
        {
            return _sysgroupDao.GetListByWhere<sysgroup>(where);
        }
        /// <summary>
        /// 方法名称: Insert
        /// 内容摘要: 插入一条新纪录
        /// </summary>
        /// <returns>int</returns>
        public virtual object Insert(sysgroup obj)
        {
			object result = null;
            try
            {
                result=_sysgroupDao.Create<sysgroup>(obj);
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
        public virtual int Update(sysgroup obj)
        {
            int result = 0;
            try
            {
                result = _sysgroupDao.Update<sysgroup>(obj);
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
                result = _sysgroupDao.DeleteByKey<sysgroup>(PK);
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
                result = _sysgroupDao.GetDataTable("SelectsysgroupByHash", ht);
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

 