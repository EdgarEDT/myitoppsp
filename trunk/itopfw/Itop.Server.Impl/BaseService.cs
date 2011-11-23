using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.IO;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper;
using System.Collections;
using Itop.Server;
using System.Data.Common;
using System.Data;
using Itop.Server.Interface;

namespace Itop.Server.Impl {
    public class BaseService : MarshalByRefObject, IBaseService {


        #region IBaseService 成员

        public IList<T> GetStrongList<T>() {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.QueryForList<T>(string.Format("Select{0}List", t.Name), null);

        }
        public IList<T> GetStrongList<T>(T data) {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.QueryForList<T>(string.Format("Select{0}List", t.Name), data);

        }

        public IList GetList(string statementname, object dataobject) {
            return SqlMapHelper.DefaultSqlMap.QueryForList(statementname, dataobject);
        }

        public object GetObject(string statementname, object dataobject) {
            return SqlMapHelper.DefaultSqlMap.QueryForObject(statementname, dataobject);
        }
        public IList<T> GetList<T>(string statementname, object dataobject)
        {
            return SqlMapHelper.DefaultSqlMap.QueryForList<T>(statementname, dataobject);
        }
        public IList GetList<T>() {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.QueryForList(string.Format("Select{0}List", t.Name), null);
        }
        public IList GetList<T>(T data) {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.QueryForList(string.Format("Select{0}List", t.Name), data);
        }
        public  IList<T> GetListByWhere<T>(string where) {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.QueryForList<T>(string.Format("Select{0}ListByWhere", t.Name), where);
        }
        public  IList<T> GetListByParentid<T>(string parentid) {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.QueryForList<T>(string.Format("Select{0}ListByParentid", t.Name), parentid);
        }
        public T GetOneByKey<T>(int key) {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.QueryForObject<T>(string.Format("Select{0}ByKey", t.Name), key);
        }

        public T GetOneByKey<T>(string key) {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.QueryForObject<T>(string.Format("Select{0}ByKey", t.Name), key);
        }

        public T GetOneByKey<T>(object data) {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.QueryForObject<T>(string.Format("Select{0}ByKey", t.Name), data);
        }


        public object Create<T>(object data)
        {
            Type t = typeof(T);

            return SqlMapHelper.DefaultSqlMap.Insert(string.Format("Insert{0}", t.Name), data);
        }


        public object Create(string statementname, object dataobject)
        {
            return SqlMapHelper.DefaultSqlMap.Insert(statementname, dataobject);
        }

        public int Update<T>(object data) {
            Type t = data.GetType();
           
            return SqlMapHelper.DefaultSqlMap.Update(string.Format("Update{0}", t.Name), data);
            //System.Windows.Forms.MessageBox.Show(t.Name);
        }

        public int Update(string statementname, object dataobject)
        {

            return SqlMapHelper.DefaultSqlMap.Update(statementname, dataobject);
            //System.Windows.Forms.MessageBox.Show(t.Name);
        }

        public string Update<T>(IList list) {

            Type t = typeof(T);
            //System.Windows.Forms.MessageBox.Show(t.Name);
            string statementName = string.Format("Update{0}", t.Name);
            SqlMapHelper.DefaultSqlMap.BeginTransaction();

            try {
                IEnumerator ie = list.GetEnumerator();
                while (ie.MoveNext()) {
                    SqlMapHelper.DefaultSqlMap.Update(statementName, ie.Current);
                }
            } catch {
                SqlMapHelper.DefaultSqlMap.RollBackTransaction();
                return "更新失败,数据已回滚!";
            }
            SqlMapHelper.DefaultSqlMap.CommitTransaction();
            return string.Empty;
        }


        public string Update(string statementname, IList list)
        {

            SqlMapHelper.DefaultSqlMap.BeginTransaction();

            try
            {
                IEnumerator ie = list.GetEnumerator();
                while (ie.MoveNext())
                {
                    SqlMapHelper.DefaultSqlMap.Update(statementname, ie.Current);
                }
            }
            catch
            {
                SqlMapHelper.DefaultSqlMap.RollBackTransaction();
                return "更新失败,数据已回滚!";
            }
            SqlMapHelper.DefaultSqlMap.CommitTransaction();
            return string.Empty;
        }

        public int Delete<T>(T data) {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.Delete(string.Format("Delete{0}", t.Name), data);
        }

        public int DeleteByKey<T>(int key) {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.Delete(string.Format("Delete{0}ByKey", t.Name), key);
        }

        public int DeleteByKey<T>(string key) {
            Type t = typeof(T);
            return SqlMapHelper.DefaultSqlMap.Delete(string.Format("Delete{0}ByKey", t.Name), key);
        }

        public int GetRowCount<T>(T data) {
            Type t = typeof(T);
            return (int)SqlMapHelper.DefaultSqlMap.QueryForObject(string.Format("Select{0}CountByObject", t.Name), data);
        }

        #endregion
    }
}
