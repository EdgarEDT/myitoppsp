using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using Itop.Common;

namespace Itop.Client.Common
{
    public class TreeListHelperArgs : EventArgs
    {
        public DataRow Row = null;

        public bool Result = false;
    }

    public class TreeListHelper
    {
        #region 构造方法
        private TreeListHelper()
        {
        }

        public TreeListHelper(TreeList tree)
        {
            treeList = tree;
            AddEventHandler();
        }
        #endregion

        #region 事件
        public event EventHandler InsertRow;
        public event EventHandler UpdateRow;
        #endregion

        #region 字段
        protected string _strRootUID = "";
        protected string _strNullBrotherUID = "";
        protected string _strBrotherFileName = "BrotherID";
        protected string _strSortIDFileName = "SortID";
        protected bool _bAllowUpdate = false;
        protected TreeList treeList = null;
        protected DataTable _dataTable = null;
        #endregion

        #region 公共属性
        public DataTable DataSource
        {
            get { return _dataTable; }
            set { _dataTable = value; }
        }

        public bool AllowUpdate
        {
            get { return _bAllowUpdate; }
            set { _bAllowUpdate = value; }
        }

        public string RootUID
        {
            get { return _strRootUID; }
            set { _strRootUID = value; }
        }

        public string NullBrotherUID
        {
            get { return _strNullBrotherUID; }
            set { _strNullBrotherUID = value; }
        }

        public string BrotherFiledName
        {
            get { return _strBrotherFileName; }
            set { _strBrotherFileName = value; }
        }

        public string SortIDFiledName
        {
            get { return _strSortIDFileName; }
            set { _strSortIDFileName = value; }
        }

        public string KeyFieldName
        {
            get { return this.treeList.KeyFieldName; }
        }

        public string ParentFieldName
        {
            get { return this.treeList.ParentFieldName; }
        }
        #endregion

        #region 辅助方法
        protected void AddEventHandler()
        {
            this.treeList.DoubleClick += new EventHandler(treeList_DoubleClick);
        }

        protected DataRow AddRow()
        {
            DataRow rowAdd = this._dataTable.NewRow();
            rowAdd[KeyFieldName] = Guid.NewGuid().ToString();
            //rowAdd[BrotherFiledName] = NullBrotherUID;
            rowAdd[ParentFieldName] = RootUID;
            return rowAdd;
        }

        protected void UpdateTree(DataRow rowAdd)
        {
            //更新树
            this.treeList.BeginUpdate();
            ////弟弟的SortID加1
            //foreach (DataRow row in this._dataTable.Rows)
            //{
            //    if (((string)row[ParentFieldName] == (string)rowAdd[ParentFieldName])
            //        && ((int)row[SortIDFiledName] >= (int)rowAdd[SortIDFiledName]))
            //    {
            //        row[SortIDFiledName] = (int)row[SortIDFiledName] + 1;
            //    }
            //}

            this._dataTable.Rows.Add(rowAdd);
            this.treeList.EndUpdate();
        }
        #endregion

        #region 事件处理
        private void treeList_DoubleClick(object sender, EventArgs e)
        {
            //检查数据是否加载
            if (_dataTable == null)
            {
                return;
            }

            //检查是否允许修改
            if (!AllowUpdate)
            {
                return;
            }

            //有效焦点节点检查
            if (this.treeList.FocusedNode == null)
            {
                return;
            }

            //点击测试，如果点击在单元格中则编辑
            Point point = this.treeList.PointToClient(Control.MousePosition);
            TreeListHitInfo hi = this.treeList.CalcHitInfo(point);
            if (hi.HitInfoType == HitInfoType.Cell)
            {
                UpdateTreeRow();
            }
        }
        #endregion

        #region  公共方法
        public void AddBrother()
        {
            DataRow rowAdd = AddRow();

            DevExpress.XtraTreeList.Nodes.TreeListNode node = this.treeList.FocusedNode;
            if (node != null)
            {
                //rowAdd[BrotherFiledName] = node.GetValue(KeyFieldName) as string;
                rowAdd[ParentFieldName] = node.GetValue(ParentFieldName) as string;
            }
            else
            {
                //rowAdd[BrotherFiledName] = "";
                rowAdd[ParentFieldName] = "root";
            }

            //执行插入操作
            if (InsertRow != null)
            {
                TreeListHelperArgs args = new TreeListHelperArgs();
                args.Row = rowAdd;
                InsertRow(this, args);
                if (args.Result)
                {
                    UpdateTree(rowAdd);
                }
            }           
        }

        public void AddChild()
        {
            DataRow rowAdd = AddRow();

            DevExpress.XtraTreeList.Nodes.TreeListNode node = this.treeList.FocusedNode;
            if (node != null)
            {
                rowAdd[ParentFieldName] = node.GetValue(KeyFieldName) as string;
                //rowAdd[BrotherFiledName] = "";  //新加入的节点作为第一个儿子
            }
            else
            {
                MsgBox.Show("请先添加一级节点！");
                return;
            }

            //获取兄弟ID， 为了将新节点变为最后一个儿子
            /*int brotherSortID = 0;
            //foreach (TreeListNode findNode in node.Nodes)
            //{
            //    int nodeSortID = (int)findNode.GetValue(SortIDFiledName);
            //    if (nodeSortID > brotherSortID && findNode.GetValue(ParentFieldName) == rowAdd[ParentFieldName])
            //    {
            //        brotherSortID = nodeSortID;
            //        rowAdd[BrotherFiledName] = findNode.GetValue(KeyFieldName);
            //    }
            //}
            */
            

            //执行插入操作
            if (InsertRow != null)
            {
                TreeListHelperArgs args = new TreeListHelperArgs();
                args.Row = rowAdd;
                InsertRow(this, args);
                if (args.Result)
                {
                    UpdateTree(rowAdd);
                }
            }
        }

        public void UpdateTreeRow()
        {
            TreeListNode node = this.treeList.FocusedNode;
            if (node == null)
            {
                return;
            }

            string uid = node.GetValue("UID") as string;

            if (UpdateRow != null)
            {
                DataRow focusedRow = null;
                foreach (DataRow row in this._dataTable.Rows)
                {
                    if ((string)row["UID"] == uid)
                    {
                        focusedRow = row;                        
                        break;
                    }
                }

                if (focusedRow != null)
                {
                    TreeListHelperArgs e = new TreeListHelperArgs();
                    e.Row = focusedRow;
                    UpdateRow(treeList, e);
                }
            }
        }

        public void DeleteNode(TreeListNode node)
        {
            //
            string uid = node.GetValue(KeyFieldName) as string;
            this.treeList.Nodes.Remove(node);

            foreach (DataRow rowFind in this._dataTable.Rows)
            {
                if ((string)rowFind[KeyFieldName] == uid)
                {
                    this._dataTable.Rows.Remove(rowFind);
                    break;
                }
            }
        }
        #endregion
    }
}
