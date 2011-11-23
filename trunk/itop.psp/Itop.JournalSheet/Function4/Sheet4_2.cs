using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Domain.Forecast;
using Itop.Client.Common;
/******************************************************************************************************
 *  ClassName：Sheet4_2
 *  Action：,sheet4_2主表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_forecast_Math
 * 时间：2010-10-11
 * 修改内容：这个表用数据库表ps_history
 * 修改时间：2010-11-12
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function4
{
    class Sheet4_2
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList City = null;//市辖供电区
        private IList County = null;//县级供电区
        private string[] AreaType = new string[2];//市辖，县级
        private const int IntProg = 3;
        private string projectID = null;//项目id
        private string programID = null;//方案id
 

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function4.Sheet4_3 S4_3 = new Function4.Sheet4_3();

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet4_2Title(FarPoint.Win.Spread.SheetView obj, string Title)
        {
            AreaType[0] = "市辖供电区";
            AreaType[1] = "县级供电区";


            int IntColCount = 10;
            int IntRowCount = (IntProg ) + 2 + 3;//标题占3行，分区类型占2行，
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


            strTitle = " 类     型";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2010年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2011年 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2012年 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2013年 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2014年 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2015年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);


            strTitle = " “十二五”年均增长率";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2020年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "“十三五”年均增长率";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 左侧标题
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow,int IntCol)
        {
            int IntRowCount = (IntProg) + IntRow;//标题占3行，分区类型占2行，
            obj.RowCount = IntRowCount;
            for (int i = IntRow; i < obj.RowCount; ++i)
            {
                if(i==5)
                {
                    PF.CreateSheetView(obj, NextRowMerge, NextColMerge, i, 0, "全地区");
                }
                if(i==6)
                {
                    PF.CreateSheetView(obj, NextRowMerge, NextColMerge, i, 0, "市辖供电区");
                }
                if(i==7)
                {
                    PF.CreateSheetView(obj, NextRowMerge, NextColMerge, i, 0, "县级供电区");
                }
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
            string[] strTitle = new string[2];
            strTitle[0] = "市辖供电区";
            strTitle[1] = "县级供电区";
            for(int i=IntRow;i<obj.RowCount;++i)
            {
                for(int j=1;j<obj.ColumnCount;++j)
                {
                    switch(j)
                    {
                        case 1:
                            if (i == IntRow)//全地区
                            {
                                obj.Cells[i, j].Formula = "B"+(i+1+1)+"+B"+(i+2+1);
                            }
                            if (i == IntRow + 1)//市辖供电区
                            {
                                obj.SetValue(i, j, SelectData(2010, programID, strTitle[0]));
                            }
                            if (i == IntRow + 2)//县级供电区
                            {
                                obj.SetValue(i, j, SelectData(2010, programID, strTitle[1]));
                            }
                            break;
                        case 2:
                            if (i == IntRow)//全地区
                            {
                                obj.Cells[i, j].Formula = "C" + (i + 1 + 1) + "+C" + (i + 2 + 1);
                            }
                            if (i == IntRow + 1)//市辖供电区
                            {
                                obj.SetValue(i, j, SelectData(2011, programID, strTitle[0]));
                            }
                            if (i == IntRow + 2)//县级供电区
                            {
                                obj.SetValue(i, j, SelectData(2011, programID, strTitle[1]));
                            }
                            break;
                        case 3:
                            if (i == IntRow)//全地区
                            {
                                obj.Cells[i, j].Formula = "D" + (i + 1 + 1) + "+D" + (i + 2 + 1);
                            }
                            if (i == IntRow + 1)//市辖供电区
                            {
                                obj.SetValue(i, j, SelectData(2012, programID, strTitle[0]));
                            }
                            if (i == IntRow + 2)//县级供电区
                            {
                                obj.SetValue(i, j, SelectData(2012, programID, strTitle[1]));
                            }
                            break;
                        case 4:
                            if (i == IntRow)//全地区
                            {
                                obj.Cells[i, j].Formula = "E" + (i + 1 + 1) + "+E" + (i + 2 + 1);
                            }
                            if (i == IntRow + 1)//市辖供电区
                            {
                                obj.SetValue(i, j, SelectData(2013, programID, strTitle[0]));
                            }
                            if (i == IntRow + 2)//县级供电区
                            {
                                obj.SetValue(i, j, SelectData(2013, programID, strTitle[1]));
                            }
                            break;
                        case 5:
                            if (i == IntRow)//全地区
                            {
                                obj.Cells[i, j].Formula = "F" + (i + 1 + 1) + "+F" + (i + 2 + 1);
                            }
                            if (i == IntRow + 1)//市辖供电区
                            {
                                obj.SetValue(i, j, SelectData(2014, programID, strTitle[0]));
                            }
                            if (i == IntRow + 2)//县级供电区
                            {
                                obj.SetValue(i, j, SelectData(2014, programID, strTitle[1]));
                            }
                            break;
                        case 6:
                            if (i == IntRow)//全地区
                            {
                                obj.Cells[i, j].Formula = "G" + (i + 1 + 1) + "+G" + (i + 2 + 1);
                            }
                            if (i == IntRow + 1)//市辖供电区
                            {
                                obj.SetValue(i, j, SelectData(2015, programID, strTitle[0]));
                            }
                            if (i == IntRow + 2)//县级供电区
                            {
                                obj.SetValue(i, j, SelectData(2015, programID, strTitle[1]));
                            }
                            break;
                        case 7:
                             obj.Cells[i, j].Formula = "POWER(G" + (i + 1) + " / B" + (i + 1) + ", 1 / 5) - 1";
                            break;
                        case 8:
                            if (i == IntRow)//全地区
                            {
                                obj.Cells[i, j].Formula = "I" + (i + 1 + 1) + "+I" + (i + 2 + 1);
                            }
                            if (i == IntRow + 1)//市辖供电区
                            {
                                obj.SetValue(i, j, SelectData(2020, programID, strTitle[0]));
                            }
                            if (i == IntRow + 2)//县级供电区
                            {
                                obj.SetValue(i, j, SelectData(2020, programID, strTitle[1]));
                            }
                            break;
                        case 9:
                            obj.Cells[i, j].Formula = "POWER(I" + (i + 1) + " / G" + (i + 1) + ", 1 / 5) - 1";
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 选中下拉菜单中的数据连接数据库表,方案
        /// </summary>
        /// <param name="BE"></param>
        public void SelectEditChange(FarPoint.Win.Spread.FpSpread  fPobj,FarPoint.Win.Spread.SheetView SheetView, object obj, Itop.Client.Base.FormBase FB)
        {
            string strID = null;
            //strTitle = obj.EditValue.ToString();
            strID=obj.ToString();
            Ps_forecast_list pfl = null;
            projectID = FB.ProjectUID;
            string con1 = "ID='" + strID + "' and UserID='" + projectID + "'";
            try
            {
                //查询下拉菜单所选中的数据
                pfl = (Ps_forecast_list)Services.BaseService.GetObject("SelectPs_forecast_listByWhere", con1);
                programID = pfl.ID;
                SetLeftTitle(SheetView, 5, 0);//左侧标题
                WriteData(SheetView, 5, 0);//数据
                S4_3.WriteData(fPobj,fPobj.Sheets[4], 5, 0);

                PF.Sheet_GridandCenter(SheetView);//画边线，居中
                S10_1.ColReadOnly(SheetView, SheetView.Columns.Count);
                PF.SetWholeRowHeight(SheetView, SheetView.Rows.Count, SheetView.Columns.Count);//行高
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }

        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="ID"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        private double SelectData(int year,string ID,string strTitle)
        {
            string con = null;
            double value = 10;
            string strYear="y"+year;
            con = "select " + strYear + " from Ps_forecast_Math where Title='"+strTitle+"'and ForecastID='"+ID+"'";
            try
            {
                value = (double)Services.BaseService.GetObject("SelectPFMdoubleWhatever", con);
            }
            catch (System.Exception e)
            {
                value = 10;
                //MessageBox.Show(strTitle +"年份是"+year+ "中没有数据！！！");
            }
            return value;
        }
    }
}
