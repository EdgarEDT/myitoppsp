using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Table;
/******************************************************************************************************
 *  ClassName：Sheet_14
 *  Action：附表44  XX市中压配变建设改造工程量统计（生技部、四县公司）表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_Table_TZGS，
 * 年份：2010-10-11。
 *********************************************************************************************************/

namespace Itop.JournalSheet.FunctionSupplement
{
    class Sheet_14
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList[] AreaName = new IList[AreaArray];//0为市辖1为县级
        private IList AreaValue = null;//数据
        private int[] AreaCount = new int[AreaArray];//市辖数据个数，县级数据个数
        private int AreaVCount = 1;//电压等级个数每个分区
        private const int AreaArray = 2;
        private string[] strProject = new string[projectVC];
        private const int projectVC = 2;//项目内容数量

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_14Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            strProject[0] = "台数";
            strProject[1] = "容量(MVA)";
            selectAreaName(FB);
            selectAreaCount(FB);
            int IntColCount = 11;
            int IntRowCount = AreaCount[0] * projectVC + AreaCount[1] * projectVC + 2 + 3;//标题占3行，分区类型占2行，1是其它用
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

            strTitle = " 项      目";
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

            strTitle = " “十二五”合计";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            SetLeftTitle(FB, obj, IntRow, IntCol);
            WriteData(FB, obj, IntRow, IntCol);
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 左侧标题
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void SetLeftTitle(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            IList list = null;
            string strTitle = null;
            int index = 0;
            PF.CreateSheetView(obj, AreaCount[0] * projectVC, 1, IntRow, IntCol, "市辖供电区");
            PF.CreateSheetView(obj, AreaCount[1] * projectVC, 1, IntRow + AreaCount[0] * projectVC, IntCol, "县级供电区");
            list = AreaName[0];
            for (int i = 0; i < AreaCount[0] * projectVC; i += AreaVCount * projectVC)
            {
                strTitle = (string)PF.ReturnStr(obj, IntRow + i, 0);

                selectAreaVoletile(FB, strTitle, list[index].ToString());
                PF.CreateSheetView(obj, AreaVCount * projectVC, 1, IntRow + i, 1, list[index].ToString());
                index++;
            }
            list = AreaName[1];
            index = 0;
            for (int i = 0; i < AreaCount[1] * projectVC; i += AreaVCount * projectVC)
            {
                strTitle = (string)PF.ReturnStr(obj, IntRow + AreaCount[0] * projectVC + i, 0);
                selectAreaVoletile(FB, strTitle, list[index].ToString());
                PF.CreateSheetView(obj, AreaVCount * projectVC, 1, IntRow + AreaCount[0] * projectVC + i, 1, list[index].ToString());
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
            Ps_Table_TZGS pTT = null;
            string strTitle = null;
            string strDQ = null;
            string temp = null;
            Object Value = null;
            int index = 0;
            for (int i = 0; i < obj.RowCount - IntRow; i+= AreaValue.Count * projectVC)
            {
                strTitle = PF.ReturnStr(obj, IntRow + i, 0).ToString();//县级还是市辖
                strDQ = PF.ReturnStr(obj, IntRow + i, 1).ToString();//分区
                SelectValue(FB, strTitle, strDQ);
                index = 0;
                for (int v = 0; v < AreaValue.Count * projectVC; v+= projectVC)
                {
                    pTT = (Ps_Table_TZGS)AreaValue[index];
                    temp = pTT.BianInfo.Substring(0, pTT.BianInfo.IndexOf("@", 0, pTT.BianInfo.Length));//电压等级
                    PF.CreateSheetView(obj, projectVC, 1, IntRow + i + v, 2, temp);
                    index++;
                    for (int n = 0; n < projectVC; ++n)//项目
                    {
                        PF.CreateSheetView(obj, 1, 1, IntRow + i+v + n, 3, strProject[n]);
                        for (int j = 4; j < obj.ColumnCount; ++j)
                        {
                            Value = 0;
                            switch (j)
                            {
                                case 4:
                                    if(pTT.BuildYear =="2010"&&n==0)
                                    {
                                        Value = pTT.Num1+pTT.Num3 +pTT.Num5;//
                                    }
                                    if (pTT.BuildYear == "2010" && n == 1)
                                    {
                                        Value = pTT.Num2 + pTT.Num4 + pTT.Num6;//
                                    }
                                    break;
                                case 5://***************************************
                                    if (pTT.BuildYear == "2011" && n == 0)
                                    {
                                        Value = pTT.Num1 + pTT.Num3 + pTT.Num5;//
                                    }
                                    if (pTT.BuildYear == "2011" && n == 1)
                                    {
                                        Value = pTT.Num2 + pTT.Num4 + pTT.Num6;//
                                    }
                                    break;
                                case 6:
                                    if (pTT.BuildYear == "2012" && n == 0)
                                    {
                                        Value = pTT.Num1 + pTT.Num3 + pTT.Num5;//
                                    }
                                    if (pTT.BuildYear == "2012" && n == 1)
                                    {
                                        Value = pTT.Num2 + pTT.Num4 + pTT.Num6;//
                                    }
                                    break;
                                case 7:
                                    if (pTT.BuildYear == "2013" && n == 0)
                                    {
                                        Value = pTT.Num1 + pTT.Num3 + pTT.Num5;//
                                    }
                                    if (pTT.BuildYear == "2013" && n == 1)
                                    {
                                        Value = pTT.Num2 + pTT.Num4 + pTT.Num6;//
                                    }
                                    break;
                                case 8:
                                    if (pTT.BuildYear == "2014" && n == 0)
                                    {
                                        Value = pTT.Num1 + pTT.Num3 + pTT.Num5;//
                                    }
                                    if (pTT.BuildYear == "2014" && n == 1)
                                    {
                                        Value = pTT.Num2 + pTT.Num4 + pTT.Num6;//
                                    }
                                    break;
                                case 9:
                                    if (pTT.BuildYear == "2015" && n == 0)
                                    {
                                        Value = pTT.Num1 + pTT.Num3 + pTT.Num5;//
                                    }
                                    if (pTT.BuildYear == "2015" && n == 1)
                                    {
                                        Value = pTT.Num2 + pTT.Num4 + pTT.Num6;//
                                    }
                                    break;
                                case 10:
                                    obj.Cells[IntRow + i + v + n, j].Formula = "SUM(F" + (IntRow + i + v + n + 1) + ":J" + (IntRow + i + v + n + 1) + ")";//
                                    break;
                                default:
                                    break;
                            }
                            if(j!=10)
                            {
                                obj.SetValue(IntRow + i+v+n, j, Value);//
                            }
                        }
                    }
                }
            }
        }
        private void SelectValue(Itop.Client.Base.FormBase FB, string strTitle, string strDQ)
        {
            string sql = null;
            if (strTitle == "市辖供电区")
            {
                sql = "select c.* FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.AreaName='" + strDQ + "';";
            }
            else
            {
                sql = "select c.* FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.AreaName='" + strDQ + "';";
            }
            try
            {
                AreaValue = Services.BaseService.GetList("SelectTZGSEveryField", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 返回电压等级个数，每个地区
        /// </summary>
        private void selectAreaVoletile(Itop.Client.Base.FormBase FB, string strTitle, string strDQ)
        {
            string sql = null;
            if (strTitle == "市辖供电区")
            {
                sql = "select count(*) FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.AreaName='" + strDQ + "';";
            }
            else
            {
                sql = "select count(*) FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.AreaName='" + strDQ + "';";
            }
            try
            {
                AreaVCount = (int)Services.BaseService.GetObject("SelectTZGSEvery", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void selectAreaName(Itop.Client.Base.FormBase FB)
        {
            string sql = "select c.AreaName FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                              "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                              "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                              "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                            "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 group by c.AreaName;";
            string sql1 = "select c.AreaName FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                              "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                              "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                              "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                            "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 group by c.AreaName;";
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
        private void selectAreaCount(Itop.Client.Base.FormBase FB)
        {
            string sql = "select count(*) FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                              "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                              "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                              "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                            "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 ;";
            string sql1 = "select count(*) FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                              "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                              "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                              "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                            "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 ;";
            try
            {
                AreaCount[0] = (int)Services.BaseService.GetObject("SelectTZGSEvery", sql);
                AreaCount[1] = (int)Services.BaseService.GetObject("SelectTZGSEvery", sql1);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
