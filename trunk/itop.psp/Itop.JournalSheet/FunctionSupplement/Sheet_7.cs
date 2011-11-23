﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
using Itop.Domain.Graphics;
/******************************************************************************************************
 *  ClassName：Sheet_7
 *  Action：附表18  XX县（区）中压配电网开关、开闭站、环网柜及电缆分支箱规模明细表（2009年）（生技部、四县公司）表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表pspDEV，PSP_Substation_Info，
 * 年份：2010-10-11。
 *********************************************************************************************************/

namespace Itop.JournalSheet.FunctionSupplement
{
    class Sheet_7
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList list = null;//线路数据
        public string strYear = null;
        private PSP_Substation_Info BDZ = null;//变电站数据JName
        private PSP_Substation_Info BDZ1 = null;//变电站数据IName


        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_7Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            SelectLine(FB, strYear);
            int IntColCount = 10;
            int IntRowCount = list.Count + 2 + 3;//标题占3行，分区类型占2行，1是其它用
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


            strTitle = " 序     号";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 中压线路名";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 变电站名";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电压等级(kV) ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "线路属性";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 分段开关数(台)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 联络开关数(台)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 开闭站数(座)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 环网柜数(座) ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电缆分支箱(台）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);


            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            WriteData(FB, obj, IntRow, IntCol);
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            PSPDEV psDev = null;
            Object Value = null;
            for (int i = 0; i < list.Count; ++i)
            {
                psDev = (PSPDEV)list[i];
                SelectBDZ(FB, psDev.Name);
                for (int j = 0; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 0:
                            Value = i + 1;//序号
                            break;
                        case 1:
                            Value = psDev.Name;//中压线路名
                            break;
                        case 2:
                            if (BDZ == null)
                            {
                                Value = "";//BDZ1.Title;
                            }
                            else if (BDZ1 == null)
                            {
                                Value = "";// BDZ.Title;
                            }
                            else
                            {
                                Value = BDZ.Title + "," + BDZ1.Title;//变电站名
                            } break;
                        case 3:
                            Value = psDev.RateVolt;//电压等级(kV)
                            break;
                        case 4:
                            Value = psDev.LineType2;//线路属性
                            break;
                        case 5://***************************************
                           Value=psDev.SwitchNum; //分段开关数(台)就是柱上开关数
                            break;
                        case 6:
                           //联络开关数(台)手写
                            break;
                        case 7:
                            Value = SelectLineOfNumber(FB, psDev.SUID, "55");//开闭站数(座)
                            break;
                        case 8:
                            Value = SelectLineOfNumber(FB, psDev.SUID, "56");//环网柜数(座) 
                            break;
                        case 9:
                            Value = SelectLineOfNumber(FB, psDev.SUID, "58");//电缆分支箱(台）
                            break;

                        default:
                            break;
                    }
                    if(j==6)
                    {
                        obj.Cells[IntRow + i, j].Locked = false;
                    }
                    else
                    {
                        obj.SetValue(IntRow + i, j, Value);//
                    }
                }
            }
        }
    
        private void SelectLine(Itop.Client.Base.FormBase FB, string year)
        {
            string con = " where IName!=''and JName!=''and RateVolt<'35' and Type='05'  and ProjectID='" + FB.ProjectUID + "' and OperationYear <='" + year + "'";
            try
            {
                list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 查询变电站数据
        /// </summary>
        private void SelectBDZ(Itop.Client.Base.FormBase FB, string strName)
        {
            string sql = " AreaID='" + FB.ProjectUID + "'and uid in(select svguid from pspDEV where  ProjectID='" + FB.ProjectUID + "' and  Type='01'and Name in(select JName from pspdev where Name='" + strName + "'))";
            string sql1 = " AreaID='" + FB.ProjectUID + "'and uid in(select svguid from pspDEV where  ProjectID='" + FB.ProjectUID + "' and  Type='01'and Name in(select IName from pspdev where Name='" + strName + "'))";
            try
            {
                BDZ = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoListByWhere", sql);
                BDZ1 = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoListByWhere", sql1);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 查询配电室(座），箱变(座），柱上变(台）的个数
        /// </summary>
        /// <returns></returns>
        private int SelectLineOfNumber(Itop.Client.Base.FormBase FB, string AreaID, string strType)
        {
            int count = 0;
            string sql = " AreaID='" + AreaID + "' and  ProjectID='" + FB.ProjectUID + "'  and Type='" + strType + "'";
            try
            {
                count = (int)Services.BaseService.GetObject("SelectPSPDEVOfNumber", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return count;
        }
    }
}
