using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraBars;
using System.Collections;

namespace Itop.Client.Common
{
    public class GridHelper
    {
        /// <summary>
        /// 设置表格的焦点行到指定的对象上
        /// </summary>
        /// <param name="gridView">表格的GridView</param>
        /// <param name="row">指定的对象</param>
        public static void FocuseRow(GridView gridView, object row)
        {
            int count = gridView.RowCount;
            for (int i = 0; i < count; i++) 
            {
                object obj = gridView.GetRow(i);
                if (obj == row)
                {
                    gridView.FocusedRowHandle = i;
                    break;
                }
            }
        }

        /******************lichnagcun2007-04-21*********************/
        public static void FocuseRow(GridView gridView,ArrayList list, string uid)
        {
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                object obj = list[i];

                if ((string)obj == uid)
                {
                    gridView.FocusedRowHandle = i;
                    break;
                }
            }
        }
        /***********************************************************/

        /// <summary>
        /// 测试point点是否在表格控件的单元各上
        /// </summary>
        /// <param name="gridView">表格的GridView</param>
        /// <param name="point">点击点：相对于GridView的坐标</param>
        /// <returns>是否点击在了单元格上</returns>
        public static bool HitCell(GridView gridView, Point point)
        {
            GridHitInfo hi = gridView.CalcHitInfo(point);
            return (hi.Column != null && hi.RowHandle != -1);
        }

        /// <summary>
        /// 在删除一行后，设置新焦点行。
        /// </summary>
        /// <param name="gridView">表格的GridView</param>
        /// <param name="oldFoucsedRowHandle">删除操作前的焦点行索引</param>
        public static void FocuseRowAfterDelete(GridView gridView, int oldFoucsedRowHandle)
        {
            int newFoucsedRowHandle = oldFoucsedRowHandle;
            int gridMaxRowHandle = gridView.RowCount - 1;
            if (oldFoucsedRowHandle > gridMaxRowHandle)
            {
                newFoucsedRowHandle = gridMaxRowHandle;
            }
            else 
            {
                newFoucsedRowHandle = oldFoucsedRowHandle;
            }

            if (newFoucsedRowHandle >= 0)
            {
                gridView.FocusedRowHandle = newFoucsedRowHandle;
            }
        }

        public static BarItemVisibility GetVisible(bool visible)
        {
            return visible ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
    }
}