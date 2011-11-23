using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;

namespace Itop.Client.Stutistics
{
    public class FormClass
    {
        //�����ؼ����ݰ���ʾ˳�����ɵ�DataTable��
        public DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();
            foreach (TreeListColumn column in xTreeList.Columns)
            {
                listColID.Add(column.FieldName);
                dt.Columns.Add(column.FieldName, column.ColumnType);
            }

            foreach (TreeListNode node in xTreeList.Nodes)
            {
                AddNodeDataToDataTable(dt, node, listColID);
            }

            return dt;
        }

        public void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        {
            DataRow newRow = dt.NewRow();
            foreach (string colID in listColID)
            {
                //���������ڶ��㼰�Ժ���ǰ��ӿո�
                if (colID == "StuffName" && node.ParentNode != null)
                {
                    newRow[colID] = "����" + node[colID];
                }
                else
                {
                    newRow[colID] = node[colID];
                }
            }

            //�����������ӿ���
            if (node.ParentNode == null && dt.Rows.Count > 0)
            {
                dt.Rows.Add(new object[] { });
            }

            dt.Rows.Add(newRow);

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID);
            }
        }
    }
}
