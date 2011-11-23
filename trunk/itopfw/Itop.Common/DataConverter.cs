using System; 
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Reflection;
using Itop.Common;
using System.Drawing;

namespace Itop.Common { 
    public class DataConverter {

        private static Hashtable types = new Hashtable();
        Color r = Color.Black;
        private DataConverter() {
        }




        /**/
        /// <summary>
        /// 把 IList 转换为 datatable.
        /// </summary>
        /// <param name="list">对象列表. 第1个item不能为null</param>
        /// <returns></returns>
        public static IList<T> ToIList<T>(DataTable dt)
        {

            IList<T> li = new List<T>();
            Type t = typeof(T);

            foreach (DataRow row in dt.Rows)
            {
                T obj = Activator.CreateInstance<T>();
                foreach (DataColumn dc in dt.Columns)
                {
                    try
                    {
                        t.GetProperty(dc.ColumnName).SetValue(obj, row[dc.ColumnName], null);
                    }
                    catch { }
                }

                li.Add(obj);
            }
            return li;
        }









        /**/
        /// <summary>
        /// 把 IList 转换为 datatable.
        /// </summary>
        /// <param name="list">对象列表. 第1个item不能为null</param>
        /// <returns></returns>
        public static DataTable ToDataTable(IList list) {
            if (list == null)
                throw new ArgumentNullException("list", "List不能为NULL。");
            
            if (list.Count == 0)
                throw new ArgumentOutOfRangeException("list", "List不能为Empty。");

            object obj = list[0];
            if (obj == null)
                throw new ArgumentOutOfRangeException("list", "第一个值不能为NUll");
            if (obj is Hashtable)
                return HashTablesToDataTable(list);
            else
                return ToDataTable(list, obj.GetType());
        }

        static private DataTable HashTablesToDataTable(IList list)
        {
            Hashtable obj =(Hashtable)list[0];
           
            DataTable dt = CreateShell(obj);

            foreach (Hashtable item in list)
            {
                if (item != null)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        try
                        {
                            dr[dc.ColumnName] = item[dc.ColumnName];
                        }
                        catch { }
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
        /// <summary>
        /// 将DataRow转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T RowToObject<T>(DataRow row){
            
            T obj = Activator.CreateInstance<T>();

            Type t = typeof(T);
            foreach (DataColumn dc in row.Table.Columns) {
                try {
                    t.GetProperty(dc.ColumnName).SetValue(obj, row[dc.ColumnName],null);
                } catch { }
            }

            return obj;
        }
        /// <summary>
        /// 将对象转换为DataRow
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static DataRow ObjectToRow(object obj,DataRow row) {

            Type t = obj.GetType();
            foreach (DataColumn dc in row.Table.Columns) {
                try {
                    row[dc.ColumnName] = t.GetProperty(dc.ColumnName).GetValue(obj, null);
                } catch { }
            }

            return row;
        }
        /// <summary>
        /// 相同对象复制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Source">源对象</param>
        /// <param name="Target">目标对象</param>
        /// <returns></returns>
        public static void CopyTo<T>(T Source, T Target) {

            Type t = typeof(T);
            foreach (PropertyInfo p in t.GetProperties()) {
                p.SetValue(Target, p.GetValue(Source, null), null);                
            }
        }
        /**/
        /// <summary>
        /// 把 IList 转换为 datatable.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="t">表结构信息</param>
        /// <returns></returns>
        public static DataTable ToDataTable(IList list, Type t) {
            return ToDataTable(list, t, true);
        }
       /// <summary>
        /// 把 IList 转换为 datatable.
       /// </summary>
       /// <param name="list"></param>
       /// <param name="t"></param>
       /// <param name="isReset">是否将已有表结构清空</param>
       /// <returns></returns>
        public static DataTable ToDataTable(IList list, Type t, bool isReset)
        {
            
            DataTable dt = types[t] as DataTable;

            if (isReset) dt = null;

            if (dt == null)
                dt = CreateShell(t);
            else
                dt = dt.Clone();

            if (list == null || list.Count == 0)
                return dt;

            foreach (object item in list)
            {
                if (item != null)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        try
                        {
                            dr[dc.ColumnName] = t.GetProperty(dc.ColumnName).GetValue(item, null);
                        }
                        catch { }
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        /**/
        /// <summary>
        /// 创建表结构
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        protected static DataTable CreateShell(Type t) {
            DataTable dt = new DataTable(t.Name);
            PropertyInfo[] pia = t.GetProperties();
            foreach (PropertyInfo pi in pia) {
                if (pi.CanRead) {
                    string st = pi.PropertyType.ToString();
                    switch (st) {
                        case "System.String":
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Boolean":
                        case "System.Double":
                        case "System.DateTime":
                        case "System.Guid":
                        //case "System.Nullable`1[System.Double]":
                            dt.Columns.Add(pi.Name, pi.PropertyType);
                            break;
                        default: {
                                if (st.StartsWith("System.Nullable")) {
                                    st = st.Substring(st.IndexOf("[") +1);
                                    st = st.Substring(0, st.IndexOf("]"));
                                }
                                try {
                                    dt.Columns.Add(pi.Name, Type.GetType(st, true, true));
                                } catch (Exception err) {
                                   // MsgBox.Show(err.Message +st);
                                }
                            break;
                            }
                    }
                }
            }

            types[t] = dt;

            return dt;
        }
        protected static DataTable CreateShell(Hashtable t)
        {

            DataTable dt = new DataTable();
            foreach (string p in t.Keys)
            {
                object obj = t[p];
                if ( obj!=null)
                {
                    string st = obj.GetType().ToString();
                    switch (st)
                    {
                        case "System.String":
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Boolean":
                        case "System.Double":
                        case "System.DateTime":
                        case "System.Guid":
                            //case "System.Nullable`1[System.Double]":
                            dt.Columns.Add(p, obj.GetType());
                            break;
                        default:
                            {
                                if (st.StartsWith("System.Nullable"))
                                {
                                    st = st.Substring(st.IndexOf("[") + 1);
                                    st = st.Substring(0, st.IndexOf("]"));
                                }
                                try
                                {
                                    dt.Columns.Add(p, Type.GetType(st, true, true));
                                }
                                catch (Exception err)
                                {
                                    MsgBox.Show(err.Message + st);
                                }
                                break;
                            }
                    }
                }
            }
           

            return dt;
        }
    }
}
