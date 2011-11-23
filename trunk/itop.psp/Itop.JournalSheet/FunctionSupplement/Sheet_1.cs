using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Table;


/******************************************************************************************************
 *  ClassName：Sheet_1
 *  Action：表7-13  XX市高压配电网三相短路电流计算结果（调度所）表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表 PSP_Substation_Info,有手写部分
 * 年份：2010-10-11
 *********************************************************************************************************/
namespace Itop.JournalSheet.FunctionSupplement
{
    class Sheet_1
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值。
        private IList list = null;
        private struct SSheet_1
        {
            public string strTitle;//变电站标题
            public string strVC;//变电站电压
            public object[,] data;//数据

        }
        List<SSheet_1> SaveList = new List<SSheet_1>();

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_1Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title, bool IsTrue)
        {
            SelectValue(FB);
            int IntColCount = 5;
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


            strTitle = " 变电站名称";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电压等级(kV)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2012年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2015年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "备     注";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            SetLeftTitle(obj, IntRow, IntCol);
            if(!IsTrue)
            {
                WriteData(obj, IntRow, IntCol);
            }
            else//更新
            {
                ComparisonData(obj, 5, 2);
                WriteData(obj, IntRow, IntCol);
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
            PSP_Substation_Info psi=null;
            for(int i=0;i<list.Count;++i)
            {
                psi = (PSP_Substation_Info)list[i];
                obj.SetValue(i+IntRow,0,psi.Title);
                obj.SetValue(i+IntRow,1,psi.L1);
            }
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
                for (int i = 0; i < obj.RowCount - IntRow; ++i)
                {
                    for (int j = 2; j < obj.ColumnCount; ++j)
                    {
                        obj.Cells[IntRow + i, j].Locked = false;
                    }
                }
        }
        private void SelectValue(Itop.Client.Base.FormBase FB)
        {
            string sql=" AreaID='"+FB.ProjectUID+"' and L1<= '35'";
            try
            {
                list = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void ComparisonData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {

            for (int i = IntRow; i < obj.RowCount; i += 1)//行从第IntRow行开始载入
            {
                for (int l = 0; l < SaveList.Count; ++l)
                {
                    if ((SaveList[l].strTitle == PF.ReturnStr(obj, i, 0).ToString()) && (SaveList[l].strVC == PF.ReturnStr(obj, i, 1).ToString()))
                    {
                        for (int j = IntCol; j < obj.ColumnCount; ++j)//
                        {
                            obj.SetValue(i, j, SaveList[l].data[0, j - IntCol]);
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
            for (int i = IntRow; i < (obj.RowCount); i++)//行从第IntRow行开始保存
            {
                SSheet_1 SS_1 = new SSheet_1();
                SS_1.strTitle = null;
                SS_1.strVC = null;
                SS_1.data = new object[indexX, indexY];
                for (int j = IntCol; j < obj.ColumnCount; ++j)//列从第IntCol列开始保存
                {
                    SS_1.strTitle = PF.ReturnStr(obj, i, 0).ToString();
                    SS_1.strVC = PF.ReturnStr(obj, i, 1).ToString();
                    SS_1.data[0, j - IntCol] = obj.Cells[i, j].Value;
                }
                SaveList.Add(SS_1);
            }

        }

    }
}
