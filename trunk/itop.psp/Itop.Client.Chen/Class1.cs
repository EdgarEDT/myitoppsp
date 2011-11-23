﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Itop.Client.Chen
{
    public class Class1
    {
        static public void TreeNodeToDataObject<T>(T dataObject, DevExpress.XtraTreeList.Nodes.TreeListNode treeNode)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (treeNode.GetValue(pi.Name)!=System.DBNull.Value)
                {
                    pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
                }
                
            }
        }
    }

    public class ChoosedYears
    {
        public int Year = 0;
        public bool WithIncreaseRate = false;
    }
}
