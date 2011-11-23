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
        /// ���ñ��Ľ����е�ָ���Ķ�����
        /// </summary>
        /// <param name="gridView">����GridView</param>
        /// <param name="row">ָ���Ķ���</param>
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
        /// ����point���Ƿ��ڱ��ؼ��ĵ�Ԫ����
        /// </summary>
        /// <param name="gridView">����GridView</param>
        /// <param name="point">����㣺�����GridView������</param>
        /// <returns>�Ƿ������˵�Ԫ����</returns>
        public static bool HitCell(GridView gridView, Point point)
        {
            GridHitInfo hi = gridView.CalcHitInfo(point);
            return (hi.Column != null && hi.RowHandle != -1);
        }

        /// <summary>
        /// ��ɾ��һ�к������½����С�
        /// </summary>
        /// <param name="gridView">����GridView</param>
        /// <param name="oldFoucsedRowHandle">ɾ������ǰ�Ľ���������</param>
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