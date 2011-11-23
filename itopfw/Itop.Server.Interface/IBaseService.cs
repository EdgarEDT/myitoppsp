using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Itop.Server.Interface {
    public interface IBaseService {
        IList<T> GetStrongList<T>();
        IList<T> GetStrongList<T>(T data);
        IList GetList<T>();
        IList GetList<T>(T data);
        IList GetList(string statementname, object dataobject);
        IList<T> GetList<T>(string statementname, object dataobject);
        IList<T> GetListByWhere<T>(string where);
        IList<T> GetListByParentid<T>(string parentid);
        object GetObject(string statementname, object dataobject);

        T GetOneByKey<T>(int id);
        T GetOneByKey<T>(string id);
        T GetOneByKey<T>(object data);
        object Create<T>(object data);
        object Create(string statementname, object data);
        int Update<T>(object data);
        int Update(string statementname, object data);
        string Update<T>(IList list);
        int Delete<T>(T data);
        int DeleteByKey<T>(int key);
        int DeleteByKey<T>(string key);
        int GetRowCount<T>(T data);
    }
}
