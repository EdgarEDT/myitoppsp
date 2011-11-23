using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Itop.Common;

namespace Itop.Client.Common
{
    public class GetTreeInstru
    {
        public GetTreeInstru()
        {
 
        }
        /// <summary>
        /// 树排序
        /// </summary>
        /// <param name="list"></param>
        /// <param name="RootId"></param>根结节ＩＤ
        /// <returns></returns>
        public ArrayList TreeSort(IList list,ArrayList TableSort,string RootId)
        {
            //转成datatable
            DataTable table = DataConverter.ToDataTable(list);

            //按条件过滤
            string ls_filte = "ParentID = '" + RootId + "'";
            string ls_sort = "SortID ASC";
            DataRow[] rows = table.Select(ls_filte, ls_sort);

            //DataRow[] rowss =new DataRow[rows.Length]; 
            //rows.CopyTo(rowss, 0);
            //
            if (TableSort.Count==list.Count) return TableSort;
            //
            foreach (DataRow row in rows)
            {
                //Object obj = new Object(); 
                //obj=row as Object;
                //TableSort.Rows.Add(obj as DataRow);
                string ls_childid = row["UID"].ToString();
                TableSort.Add(ls_childid);
                TreeSort(list,TableSort,ls_childid);
            }

            return TableSort;
        }

        //protected static void BuildChildNodes(MyTreeNode node,IList<T> deptList)
        //{
        //    foreach (DbTreeNode dept in deptList)
        //    {
        //        if (dept.ParentID == node.ID)
        //        {
        //            MyTreeNode nodeChild = new MyTreeNode(dept.ID);

        //            node.Children.Add(nodeChild);

        //            BuildChildNodes(nodeChild, deptList);
        //        }
        //    }
        //}

        //public class MyTreeNode
        //{
        //    public string ID = "";
        //    public IList<MyTreeNode> Children;

        //    public MyTreeNode(string id)
        //    {
        //        ID = id;
        //        Children = new List<MyTreeNode>();
        //    }
        //    public virtual void Output(IList<string> list)
        //    {
        //        list.Add(ID);
        //        foreach (MyTreeNode node in Children)
        //        {
        //            node.Output(list);
        //        }
        //    }
        //}
    }
}
