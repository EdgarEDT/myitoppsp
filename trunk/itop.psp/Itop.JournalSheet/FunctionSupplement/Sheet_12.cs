﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Table;
/******************************************************************************************************
 *  ClassName：Sheet_12
 *  Action：附表42  XX市中压配电网分区分年度网供负荷计算结果 单位：MW（生技部、四县公司）表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表ps_Table_AreaWH是个手写表
 * 年份：2010-10-11。
 *********************************************************************************************************/

namespace Itop.JournalSheet.FunctionSupplement
{
    class Sheet_12
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList[] AreaName = new IList[2];//0为市辖1为县级
        private const int intVC = 3;
        private string[] VoltageClass = new string[intVC];//电压等级有，10,6,20
        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private struct SSheet_1
        {
            public string strType;//分区类型
            public string strDQ;//分区名称
            public object[,] data;//数据

        }
        List<SSheet_1> SaveList = new List<SSheet_1>();

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初始化电压等级
        /// </summary>
        private void InitVoltageClass()
        {
            VoltageClass[0] = "20";
            VoltageClass[1] = "10";
            VoltageClass[2] = "6";
        }
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_12Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title,bool IsTrue)
        {
            InitVoltageClass();
            
            selectAreaName(FB);
            int IntColCount = 10;
            int IntRowCount = AreaName[0].Count * intVC + AreaName[1].Count * intVC + 2 + 3;//标题占3行，分区类型占2行，1是其它用
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

            strTitle = " 电压等级(kV)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2010年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2011年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2012年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2013年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2014年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2015年 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);


            strTitle = " 2020年 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            SetLeftTitle(obj, IntRow, IntCol);
            if(!IsTrue)
            {
                WriteData(FB, obj, IntRow, IntCol);
            }else
            {
                ComparisonData(obj,5,3);
                WriteData(FB, obj, IntRow, IntCol);
            }
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 左侧标题
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            IList list = null;
            int index = 0;
            PF.CreateSheetView(obj, AreaName[0].Count*intVC , 1, IntRow, IntCol, "市辖供电区");
            PF.CreateSheetView(obj, AreaName[1].Count*intVC , 1, IntRow + AreaName[0].Count*intVC , IntCol, "县级供电区");
            list = AreaName[0];
            for (int i = 0; i < AreaName[0].Count*intVC; i += intVC)
            {
                PF.CreateSheetView(obj, intVC, 1, IntRow + i, 1, list[index].ToString());
                for (int j = 0; j < intVC;++j )
                {
                    PF.CreateSheetView(obj, 1, 1, IntRow + i+j, 2, VoltageClass[j]);
                }
                index++;
            }
            list = AreaName[1];
            index = 0;
            for (int i = 0; i < AreaName[1].Count*intVC ; i+=intVC)
            {
                PF.CreateSheetView(obj, intVC, 1, IntRow + AreaName[0].Count * intVC + i, 1, list[index].ToString());
                for (int j = 0; j < intVC; ++j)
                {
                    PF.CreateSheetView(obj, 1, 1, IntRow + AreaName[0].Count*intVC + i+j, 2, VoltageClass[j]);
                }
                index++;
            }
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            //Ps_Table_TZGS pTT = null;
            //string strTitle = null;
            //string strDQ = null;
            //string strVoleitle = null;
            //Object Value = null;
            for (int i = 0; i < obj.RowCount - IntRow; ++i)
            {
                //strTitle = PF.ReturnStr(obj, IntRow+i, 0).ToString();//县级还是市辖
                //strDQ = PF.ReturnStr(obj, IntRow + i, 1).ToString();//分区
                //strVoleitle = (string)PF.ReturnStr(obj, IntRow + i, 2);//电压等级
                for (int j = 3; j < obj.ColumnCount; ++j)
                {
                    obj.Cells[IntRow + i, j].Locked = false;
                    //switch (j)
                    //{
                    //    case 3:
                    //        Value = SelectValue(FB,strTitle,strDQ,strVoleitle,"2010");//
                    //        break;
                    //    case 4:
                    //        Value = SelectValue(FB,strTitle,strDQ,strVoleitle,"2011");//
                    //        break;
                    //    case 5://***************************************
                    //        Value = SelectValue(FB,strTitle,strDQ,strVoleitle,"2012");//
                    //        break;
                    //    case 6:
                    //        Value = SelectValue(FB,strTitle,strDQ,strVoleitle,"2013");//
                    //        break;
                    //    case 7:
                    //        Value = SelectValue(FB,strTitle,strDQ,strVoleitle,"2014");//
                    //        break;
                    //    case 8:
                    //        Value = SelectValue(FB,strTitle,strDQ,strVoleitle,"2015");//
                    //        break;
                    //    case 9:
                    //        Value = SelectValue(FB,strTitle,strDQ,strVoleitle,"2020");//
                    //        break;

                    //    default:
                    //        break;
                    //}
                    //obj.SetValue(IntRow + i, j, Value);//
                }
            }
        }
        /// <summary>
        /// 查询符合条件的分区
        /// </summary>
        private void selectAreaName(Itop.Client.Base.FormBase FB)
        {
            //string sql = "select AreaName  from ps_table_tzgs where (BianInfo like '110@%'or BianInfo like '66@%' or BianInfo like '35@%')  and DQ='市辖供电区' and ProjectID='" + FB.ProjectUID + "' group by AreaName";
            //string sql1 = "select AreaName  from ps_table_tzgs where (BianInfo like '110@%'or BianInfo like '66@%' or BianInfo like '35@%')  and DQ!='市辖供电区' and ProjectID='" + FB.ProjectUID + "' group by AreaName";
            string sql = "select Title from ps_Table_AreaWH where Col1='市区'and ProjectID='" + FB.ProjectUID + "' group by title";
            string sql1 = "select Title from ps_Table_AreaWH where Col1='县级'and ProjectID='" + FB.ProjectUID + "' group by title";
            try
            {
                AreaName[0] = Services.BaseService.GetList("SelectTZGSEvery", sql);
                AreaName[1] = Services.BaseService.GetList("SelectTZGSEvery", sql1);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void ComparisonData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            for (int i = IntRow; i < obj.RowCount; i += 3)//行从第IntRow行开始载入
            {
                for (int l = 0; l < SaveList.Count; ++l)
                {
                    if ((SaveList[l].strType == PF.ReturnStr(obj, i, 0).ToString()) && (SaveList[l].strDQ == PF.ReturnStr(obj, i, 1).ToString()))
                    {
                        for (int j = IntCol; j < obj.ColumnCount; ++j)//
                        {
                            obj.SetValue(i, j, SaveList[l].data[0, j - IntCol]);
                            obj.SetValue(i + 1, j, SaveList[l].data[1, j - IntCol]);
                            obj.SetValue(i + 2, j, SaveList[l].data[2, j - IntCol]);
                        }
                        SaveList.Remove(SaveList[l]);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 保存手写的数据，为了和新更新的数据比较
        /// 点击更新时把旧表保存在IList中
        /// </summary>
        /// <param name="BeginCol">要保存的起始行</param>
        /// <param name="BeginRow">要保存的起始列</param>
        /// <param name="indexX">二维数组的一维个数</param>
        /// <param name="indexY">二维数组的二维个数</param>
        public void SaveData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol, int indexX, int indexY)
        {
            SaveList.Clear();
            for (int i = IntRow; i < (obj.RowCount); i += 3)//行从第IntRow行开始保存
            {
                SSheet_1 SS_1 = new SSheet_1();
                SS_1.strType = null;
                SS_1.strDQ = null;
                SS_1.data = new object[indexX, indexY];
                for (int j = IntCol; j < obj.ColumnCount; ++j)//列从第IntCol列开始保存
                {
                    SS_1.strType = PF.ReturnStr(obj, i, 0).ToString();
                    SS_1.strDQ = PF.ReturnStr(obj, i, 1).ToString();
                    SS_1.data[0, j - IntCol] = obj.Cells[i, j].Value;
                    SS_1.data[1, j - IntCol] = obj.Cells[i + 1, j].Value;
                    SS_1.data[2, j - IntCol] = obj.Cells[i + 2, j].Value;
                }
                SaveList.Add(SS_1);
            }

        }
    }
}
