using System;
using System.Collections.Generic;
using System.Text;
using Itop.Server.Interface;
using Itop.Common;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Itop.Client.Projects
{
    public class Services
    {
        private static IBaseService sysService;
        public static IBaseService BaseService
        {
            get
            {
                if (sysService == null)
                {
                    sysService = RemotingHelper.GetRemotingService<IBaseService>();
                }
                if (sysService == null) MsgBox.Show("IBaseService����û��ע��");
                return sysService;
            }
            set {
                sysService = value;
            }
        }




    }

    public class Common
    {
        public class GridHelper
        {
            /// <summary>
            /// ���ñ���Ľ����е�ָ���Ķ�����
            /// </summary>
            /// <param name="gridView">�����GridView</param>
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

            /// <summary>
            /// ����point���Ƿ��ڱ���ؼ��ĵ�Ԫ����
            /// </summary>
            /// <param name="gridView">�����GridView</param>
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
            /// <param name="gridView">�����GridView</param>
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



        }

    }


}