using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Itop.Server.Interface {
    public interface IBaseService<T> {
        IList<T> GetStrongList();
        IList GetList();
        T GetOneByKey(int id);
        T GetOneByKey(string id);
        T GetOneByKey(T data);
        object Create(T data);
        int Update(T data);
        string Update(IList list);
        int Delete(T data);
        int DeleteByKey(int key);
        int DeleteByKey(string key);
        int GetRowCount(T data);
    }
}
