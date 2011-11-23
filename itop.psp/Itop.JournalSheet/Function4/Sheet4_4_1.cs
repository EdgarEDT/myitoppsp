using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Domain.Forecast;
using Itop.Client.Common;
using Itop.Domain.Forecast ;
/******************************************************************************************************
 *  ClassName：Sheet4_4_1
 *  Action：,sheet4_4_1附表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_forecast_Math，
 * 时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function4
{
    class Sheet4_4_1
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList City = null;//市辖供电区
        private IList County = null;//县级供电区
        private string[] AreaType = new string[2];//市辖，县级
        private string[] item = new string[2];//项目内容 
        private const int IntProg = 6;
        private string projectID = null;//项目id
        private string programID = null;//方案id
        private string CityID = null;//市辖id
        private string CountyID = null;//县级id 
        private const int ColumnCount = 12;//列数
        private IList list = null;//存放县级和市辖的所有数据

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function4.Sheet4_3 S4_3 = new Function4.Sheet4_3();

        //////////////////////////////////////////////////////////////////////////
        private void InitTitle()
        {
            AreaType[0] = "市辖供电区";
            AreaType[1] = "县级供电区";
            item[0] = "最大负荷（MW）";
            item[1] = "Tmax（h）";
        }
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet4_4_1Title(FarPoint.Win.Spread.SheetView obj, string Title)
        {

            int IntColCount = ColumnCount;
            int IntRowCount =  2 + 3;//标题占3行，分区类型占2行，
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

            strTitle = " 分区名称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "项     目 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
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
            //SetLeftTitle(obj,5,0);
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
            const int ProgramCount = 2;//项目的种类
            int IntRowCount = (City.Count + County.Count)*ProgramCount + IntRow;//标题占3行，分区类型占2行，
            obj.Rows.Count = IntRowCount;
            obj.Columns.Count = ColumnCount;
            int indexCity = 0;
            int indexCounty = 0;
            for (int i = IntRow; i < obj.RowCount; ++i)
            {
                if (i == IntRow)
                {
                    PF.CreateSheetView(obj, City.Count*ProgramCount, NextColMerge, i, 0, "市辖供电区");
                }
                if (i == IntRow + City.Count*ProgramCount)
                {
                    PF.CreateSheetView(obj, County.Count*ProgramCount, NextColMerge, i, 0, "县级供电区");
                }

            }
            for (int i = IntRow; i < obj.RowCount; i += 2)
            {
                if (i >= IntRow && i < IntRow + City.Count * ProgramCount)
                {
                    PF.CreateSheetView(obj, 2, NextColMerge, i, 1, City[indexCity].ToString());
                    indexCity++;
                }
                if (i >= IntRow + City.Count * ProgramCount && i < obj.RowCount * ProgramCount)
                {
                    PF.CreateSheetView(obj, 2, NextColMerge, i, 1, County[indexCounty].ToString());
                    indexCounty++;
                }
                for (int j = 0; j < 2; ++j)
                {
                    PF.CreateSheetView(obj, 1, NextColMerge, j + i, 2, item[j]);
                }
            }
        }
        /// <summary>
        /// 写入数据 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData(FarPoint.Win.Spread.FpSpread Fpobj, FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            string strTitle = null;
            string strDQ = null;
            string strProject=null;
            string SheetN = "'" + Fpobj.Sheets[3].SheetName + "'!";//附表30的数据
            string strYear = "";
            string strCurrentYear = "";
            int intDQ = 5;//当前行数
            for (int i = IntRow; i < (obj.RowCount); ++i)
            {
                
                strTitle = (string)PF.ReturnStr(obj, i, 0);//市辖还是县级
                strDQ = (string)PF.ReturnStr(obj, i, 1);//分区
                strProject = (string)PF.ReturnStr(obj, i, 2);//项目
                for (int j = 3; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 3:
                            strYear = "C";
                            strCurrentYear = "D";
                            break;
                        case 4:
                            strYear = "D";
                            strCurrentYear = "E";
                            break;
                        case 5:
                            strYear = "E";
                            strCurrentYear = "F";
                            break;
                        case 6:
                            strYear = "F";
                            strCurrentYear = "G";
                            break;
                        case 7:
                            strYear = "G";
                            strCurrentYear = "H";
                            break;
                        case 8:
                            strYear = "H";
                            strCurrentYear = "I";
                            break;
                        case 10:
                            strYear = "J";
                            strCurrentYear = "K";
                            break;
                        default:
                            break;
                    }
                    switch (j)
                    {
                        case 3:
                            if (strTitle == AreaType[0])//市辖
                            {
                                for (int index = 0; index < City.Count; ++index)
                                {
                                    if (strDQ == City[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2010, programID, strProject, CityID));
                                        }
                                        else//Tmax（h）
                                        {
                                            intDQ+=1;
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            if(strTitle==AreaType[1])//县级
                            {
                                for (int index = 0; index < County.Count; ++index)
                                {
                                    if (strDQ == County[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2010, programID, strProject, CountyID));
                                        }
                                        else//Tmax（h）
                                        {
                                            intDQ += 1;
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            break;
                        case 4:
                            if (strTitle == AreaType[0])//市辖
                            {
                                for (int index = 0; index < City.Count; ++index)
                                {
                                    if (strDQ == City[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2011, programID, strProject, CityID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            if (strTitle == AreaType[1])//县级
                            {
                                for (int index = 0; index < County.Count; ++index)
                                {
                                    if (strDQ == County[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2011, programID, strProject, CountyID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            break;
                        case 5:
                            if (strTitle == AreaType[0])//市辖
                            {
                                for (int index = 0; index < City.Count; ++index)
                                {
                                    if (strDQ == City[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2012, programID, strProject, CityID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            if (strTitle == AreaType[1])//县级
                            {
                                for (int index = 0; index < County.Count; ++index)
                                {
                                    if (strDQ == County[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2012, programID, strProject, CountyID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            break;
                        case 6:
                            if (strTitle == AreaType[0])//市辖
                            {
                                for (int index = 0; index < City.Count; ++index)
                                {
                                    if (strDQ == City[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2013, programID, strProject, CityID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            if (strTitle == AreaType[1])//县级
                            {
                                for (int index = 0; index < County.Count; ++index)
                                {
                                    if (strDQ == County[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2013, programID, strProject, CountyID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }

                            break;
                        case 7:
                            if (strTitle == AreaType[0])//市辖
                            {
                                for (int index = 0; index < City.Count; ++index)
                                {
                                    if (strDQ == City[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2014, programID, strProject, CityID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            if (strTitle == AreaType[1])//县级
                            {
                                for (int index = 0; index < County.Count; ++index)
                                {
                                    if (strDQ == County[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2014, programID, strProject, CountyID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }

                            break;
                        case 8:
                            if (strTitle == AreaType[0])//市辖
                            {
                                for (int index = 0; index < City.Count; ++index)
                                {
                                    if (strDQ == City[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2015, programID, strProject, CityID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            if(strTitle==AreaType[1])//县级
                            {
                                for (int index = 0; index < County.Count; ++index)
                                {
                                    if (strDQ == County[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2015, programID, strProject, CountyID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            break;
                        case 9:
                            obj.Cells[i, j].Formula = "POWER(I" + (i + 1) + " / D" + (i + 1) + ", 1 / 5) - 1";
                            break;
                        case 10:
                            if (strTitle == AreaType[0])//市辖
                            {
                                for (int index = 0; index < City.Count; ++index)
                                {
                                    if (strDQ == City[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2020, programID, strProject, CityID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            if (strTitle == AreaType[1])//县级
                            {
                                for (int index = 0; index < County.Count; ++index)
                                {
                                    if (strDQ == County[index].ToString())
                                    {
                                        if (strProject == item[0])//最大负荷（MW）
                                        {
                                            obj.SetValue(i, j, SelectData(2020, programID, strProject, CountyID));
                                        }
                                        else//Tmax（h）
                                        {
                                            obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                        }
                                    }
                                }
                            }
                            break;
                        case 11:
                            obj.Cells[i, j].Formula = "POWER(K" + (i + 1) + " / I" + (i + 1) + ", 1 / 5) - 1";
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
        public void SelectEditChange(FarPoint.Win.Spread.FpSpread fPobj,FarPoint.Win.Spread.SheetView SheetView, object obj, Itop.Client.Base.FormBase FB)
        {
            //string strTitle = null;
            string strID = obj.ToString();
            InitTitle();
            //Ps_forecast_list pfl = null;
            projectID = FB.ProjectUID;
            //string con1 = "Title='" + strTitle + "' and UserID='" + projectID + "'";
            try
            {
                //查询下拉菜单所选中的数据
                //pfl = (Ps_forecast_list)Services.BaseService.GetObject("SelectPs_forecast_listByWhere", con1);
                //programID = pfl.ID;
                programID = strID;
                CityID = SelectID(strID, AreaType[0]);//市辖
                CountyID = SelectID(strID, AreaType[1]);//县级
                SelectDQ(CityID, AreaType[0]);
                SelectDQ(CountyID, AreaType[1]);
                SetLeftTitle(SheetView, 5, 0);//左侧标题
                WriteData(fPobj,SheetView, 5, 0);//数据

                PF.Sheet_GridandCenter(SheetView);//画边线，居中
                S10_1.ColReadOnly(SheetView, SheetView.Columns.Count);
                PF.SetWholeRowHeight(SheetView, SheetView.Rows.Count, SheetView.Columns.Count);//行高
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="year">要查询哪年的值</param>
        /// <param name="ForecastID">方案id</param>
        /// <param name="strTitle">最大负荷（MW）</param>
        /// <param name="parentid">上一级目录id</param>
        /// <returns></returns>
        private double SelectData(int year, string ForecastID, string strTitle, string parentid)
        {
            string con = null;
            double value = 10;
            string strYear = "y" + year;
            con = "select " + strYear + " from Ps_forecast_Math where Title='" + strTitle + "'and ForecastID='" + ForecastID + "' and Forecast='1'";
            try
            {
                value = (double)Services.BaseService.GetObject("SelectPFMdoubleWhatever", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
                //MessageBox.Show(strTitle + "年份是：" + year + "没有数据！！！");
                value = 10;
            }
            return value;
        }
        /// <summary>
        /// 查询地区
        /// </summary>
        private void SelectDQ(string ID, string strTitle)
        {
            string con = "";
            con = "select Title from Ps_forecast_Math where  ForecastID='" + programID + "' and ParentID='" + ID + "' group by Title";
            try
            {
                if (strTitle == "市辖供电区")//市辖供电区
                {
                    City = Services.BaseService.GetList("SelectPFMStringWhatever", con);
                    if (City.Count == 0)
                    {
                        //MessageBox.Show(strTitle + "没有数据！！！");
                    }
                }
                if (strTitle == "县级供电区")//县级供电区
                {
                    County = Services.BaseService.GetList("SelectPFMStringWhatever", con);
                    if (County.Count == 0)
                    {
                        //MessageBox.Show(strTitle + "没有数据！！！");
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 返回市辖，县级的id值
        /// </summary>
        /// <param name="ID"></param>
        private string SelectID(string ID, string strTitle)
        {
            string con = null;
            //string Id = null;
            Ps_Forecast_Math pfm = null;
            con = "Title='" + strTitle + "'and ForecastID='" + ID + "' and (Forecast = '1')";//默认为1的算法,现在调试程序用2
            list = Services.BaseService.GetList("SelectPs_Forecast_MathByWhere", con);
            if(list.Count!=0)
            {
                pfm = (Ps_Forecast_Math)list[0];
                return pfm.ID;
            }
            else
            {
                return strTitle+"没有数据";
            }
            
        }
    }
}
