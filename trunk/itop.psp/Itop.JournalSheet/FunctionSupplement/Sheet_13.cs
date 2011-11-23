using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Table;
/******************************************************************************************************
 *  ClassName：Sheet_13
 *  Action：附表43  XX市新增配变容量需求估算 单位：MVA（生技部、四县公司）表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_Table_TZGS，
 * 年份：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.FunctionSupplement
{
    class Sheet_13
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList[] AreaName = new IList[AreaArray];//0为市辖1为县级
        private IList AreaValue = null;//数据
        private int[] AreaCount = new int[AreaArray];//市辖数据个数，县级数据个数
        private int AreaVCount=1;//电压等级个数每个分区
        private const int AreaArray=2;

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_13Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {

            selectAreaName(FB);
            selectAreaCount(FB);
            int IntColCount = 9;
            int IntRowCount = AreaCount[0] + AreaCount[1] + 2 + 3;//标题占3行，分区类型占2行，1是其它用
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

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            SetLeftTitle(FB,obj, IntRow, IntCol);
            WriteData(FB, obj, IntRow, IntCol);
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 左侧标题
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void SetLeftTitle(Itop.Client.Base.FormBase FB,FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            IList list = null;
            string strTitle = null;
            int index = 0;
            PF.CreateSheetView(obj, AreaCount[0], 1, IntRow, IntCol, "市辖供电区");
            PF.CreateSheetView(obj, AreaCount[1], 1, IntRow + AreaCount[0], IntCol, "县级供电区");
            list = AreaName[0];
            for (int i = 0; i < AreaCount[0]; i += AreaVCount)
            {
                strTitle = (string)PF.ReturnStr(obj, IntRow+i, 0);

                selectAreaVoletile(FB, strTitle, list[index].ToString());
                PF.CreateSheetView(obj, AreaVCount, 1, IntRow + i, 1, list[index].ToString());
                index++;
            }
            list = AreaName[1];
            index = 0;
            for (int i = 0; i < AreaCount[1]; i += AreaVCount)
            {
                strTitle = (string)PF.ReturnStr(obj, IntRow + AreaCount[0] + i, 0);
                selectAreaVoletile(FB, strTitle, list[index].ToString());
                PF.CreateSheetView(obj, AreaVCount, 1, IntRow + AreaCount[0] + i, 1, list[index].ToString());
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
            Object Value = null;
            for (int i = 0; i < obj.RowCount - IntRow; i += AreaValue.Count)
            {
                strTitle = PF.ReturnStr(obj, IntRow + i, 0).ToString();//县级还是市辖
                strDQ = PF.ReturnStr(obj, IntRow + i, 1).ToString();//分区
                SelectValue(FB, strTitle, strDQ);
                for (int v=0; v < AreaValue.Count; ++v)
                {
                    pTT = (Ps_Table_TZGS)AreaValue[v];
                    for (int j = 2; j < obj.ColumnCount; ++j)
                    {
                        switch (j)
                        {
                            case 2:
                                Value = pTT.BianInfo.Substring(0,pTT.BianInfo.IndexOf("@",0,pTT.BianInfo.Length));//电压等级
                                break;
                            case 3:
                                Value = pTT.y2010;//
                                break;
                            case 4:
                                Value = pTT.y2011;//
                                break;
                            case 5://***************************************
                                Value = pTT.y2012;//
                                break;
                            case 6:
                                Value = pTT.y2013;//
                                break;
                            case 7:
                                Value = pTT.y2014;//
                                break;
                            case 8:
                                Value = pTT.y2015;//
                                break;

                            default:
                                break;
                        }
                        obj.SetValue(IntRow + i+v, j, Value);//
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
        private void selectAreaVoletile(Itop.Client.Base.FormBase FB,string strTitle,string strDQ)
        {
            string sql = null;
            if (strTitle == "市辖供电区")
            {
                 sql = "select count(*) FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                  "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                  "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                  "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                                "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.AreaName='"+strDQ+"';";
            }
            else
            {
                 sql = "select count(*) FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                  "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                  "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                  "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                                "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.AreaName='"+strDQ+"';";
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
            string sql = "select count(*) FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d "+
                              "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' "+
                              "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' "+
                              "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ='市辖供电区' and c.projectid='"+FB.ProjectUID+"' "+
                            "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 ;";
            string sql1 = "select count(*) FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                              "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                              "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                              "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                            "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 ;";
            try
            {
                AreaCount[0] = (int)Services.BaseService.GetObject("SelectTZGSEvery", sql);
                AreaCount[1] =(int) Services.BaseService.GetObject("SelectTZGSEvery", sql1);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
