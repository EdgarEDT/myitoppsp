using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Table;
/******************************************************************************************************
 *  ClassName：Sheet_11
 *  Action：附表41  XX市高压配电网各变电站计算负荷（发策部、生技部、四县公司）表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表PSP_Substation_Info
 * 年份：2010-10-11。
 *********************************************************************************************************/

namespace Itop.JournalSheet.FunctionSupplement
{
    class Sheet_11
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList[] AreaName = new IList[2];//0为市辖1为县级
        //private IList[] AreaNameValue = new IList[2];//0为市辖数据1为县级数据

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_11Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {

            selectAreaName(FB);
            int IntColCount = 7;
            int IntRowCount = AreaName[0].Count  + AreaName[1].Count  + 2 + 3;//标题占3行，分区类型占2行，1是其它用
            string title = null;

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            S10_1.ColReadOnly(obj, IntColCount);
            //obj.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;

            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;


            strTitle = " 分区类型";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 分区名称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 变电站名称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电压等级(kV)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2015年变电容量(MVA)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2015年计算负荷(MW)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 备     注";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;

            SetLeftTitle(obj, IntRow, IntCol);
            WriteData(FB, obj, IntRow, IntCol);
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        private void SetLeftTitle( FarPoint.Win.Spread.SheetView obj,int IntRow,int IntCol)
        {
            IList list=null;
            if(AreaName[0].Count!=0)
            {
                PF.CreateSheetView(obj, AreaName[0].Count, 1, IntRow, 0, "市辖供电区");
            }
            if(AreaName[1].Count!=0)
            {
                PF.CreateSheetView(obj, AreaName[1].Count, 1, IntRow + AreaName[0].Count, 0, "县级供电区");
            }
            list=AreaName[0];
            PSP_Substation_Info psi=null;
            for (int i = 0; i < AreaName[0].Count; ++i)
            {
                psi = (PSP_Substation_Info)list[i];
                obj.SetValue(IntRow + i, 1, psi.AreaName);
            }
            list = AreaName[1];
            for(int i=0;i<AreaName[1].Count;++i)
            {
                psi = (PSP_Substation_Info)list[i];
                obj.SetValue(IntRow + i + AreaName[0].Count, 1, psi.AreaName);
            }
        }
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            //string strTitle = null;
            //string strDQ = null;
            Object value = null;
            IList list = null;
            PSP_Substation_Info psi = null;
            list = AreaName[0];
            for (int i = 0; i < AreaName[0].Count; ++i)//市辖
            {
                psi = (PSP_Substation_Info)list[i];
                for (int j = 2; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 2:
                            value = psi.Title;//变电站名称
                            break;
                        case 3:
                            value = psi.L1;//电压等级
                            break;
                        case 4:
                            value = psi.L2;//2015年变电容量(MVA)
                            break;
                        case 5:
                            value = psi.L9;//2015年计算负荷(MW)
                            break;

                        default:
                            break;
                    }
                    if (j == obj.ColumnCount - 1)
                    {
                        obj.Cells[IntRow + i, j].Locked = false;
                    }
                    else
                    {
                        obj.SetValue(IntRow + i, j, value);
                    }
                }
            }

            list = AreaName[1];
            for (int i = 0; i < AreaName[1].Count; ++i)//县级
            {
                psi = (PSP_Substation_Info)list[i];
                for (int j = 2; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 2:
                            value = psi.Title;//变电站名称
                            break;
                        case 3:
                            value = psi.L1;//电压等级
                            break;
                        case 4:
                            value = psi.L2;//2015年变电容量(MVA)
                            break;
                        case 5:
                            value = psi.L9;//2015年计算负荷(MW)
                            break;

                        default:
                            break;
                    }
                    if (j == obj.ColumnCount - 1)
                    {
                        obj.Cells[IntRow + AreaName[0].Count + i, j].Locked = false;
                    }
                    else
                    {
                        obj.SetValue(IntRow + AreaName[0].Count + i, j, value);
                    }
                }
            }
        }
        //private void SelectValue(Itop.Client.Base.FormBase FB,string strTitle,string DQ)
        //{
        //    string sql = "select * from PSP_Substation_Info where AreaID='"+FB.ProjectUID+"' and L1>= '35' and DQ='市辖供电区' and AreaName='"+DQ+"' and S2='2015'";
        //    string sql1 = "select * from PSP_Substation_Info where AreaID='" + FB.ProjectUID + "' and L1>= '35' and DQ!='市辖供电区' and AreaName='" + DQ + "' and S2='2015'";
        //    try
        //    {
        //        Are[0]=Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere",sql);
        //        Are[1] = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql1);
        //    }
        //    catch (System.Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}
        /// <summary>
        /// 查询地区名
        /// </summary>
        /// <param name="FB"></param>
        private void selectAreaName(Itop.Client.Base.FormBase FB)
        {
            string sql = "  AreaID='"+FB.ProjectUID+"' and L1>= '35' and DQ='市辖供电区'  and S2='2015'";
            string sql1 = " AreaID='" + FB.ProjectUID + "' and L1>= '35' and DQ!='市辖供电区' and S2='2015' ";
            try
            {
                AreaName[0] = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql);
                AreaName[1] = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql1);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
