using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Domain.Forecast;
using Itop.Client.Common;
/******************************************************************************************************
 *  ClassName：Sheet4_4
 *  Action：,表4-4  铜陵市分年度统调最大负荷预测结果表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_forecast_Math
 * 时间：2010-10-11
 * 。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function4
{
    class Sheet4_4
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
        private const int ColumnCount = 11;//列数
        private IList list = null;//存放县级和市辖的所有数据

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function4.Sheet4_3 S4_3 = new Function4.Sheet4_3();
        private Function4.Sheet4_5 S4_5 = new Function4.Sheet4_5();
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
        public void SetSheet4_4Title(FarPoint.Win.Spread.SheetView obj, string Title)
        {

            int IntColCount = ColumnCount;
            int IntRowCount = (IntProg) + 2 + 3;//标题占3行，分区类型占2行，
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

            strTitle = " 项     目";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow, IntCol +=1, strTitle);
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
            int IntRowCount = (IntProg) + 2 + 3;//标题占3行，分区类型占2行，
            obj.Rows.Count = IntRowCount;
            obj.Columns.Count = ColumnCount;
            InitTitle();
            for (int i = IntRow; i < obj.RowCount; ++i)
            {
                if (i == 5)
                {
                    PF.CreateSheetView(obj, 2, NextColMerge, i, 0, "全地区");
                }
                if (i == 7)
                {
                    PF.CreateSheetView(obj, 2, NextColMerge, i, 0, "市辖供电区");
                }
                if (i == 9)
                {
                    PF.CreateSheetView(obj, 2, NextColMerge, i, 0, "县级供电区");
                }
            }
            for (int i = IntRow; i < obj.RowCount; i+=2)
            {
                for(int j=0;j<2;++j)
                {
                    PF.CreateSheetView(obj, 1, NextColMerge, j+i, 1, item[j]);
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
            string SheetN = "'" + Fpobj.Sheets[2].SheetName + "'!";
            string strYear = "";
            string strCurrentYear = "";
            int intDQ = (IntRow + 1);//当前行数
            for (int i = IntRow; i < obj.RowCount; ++i)
            {
                strTitle = (string)PF.ReturnStr(obj, i, 0);
                strDQ = (string)PF.ReturnStr(obj, i, 1);
                for (int j = 2; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 2:
                            strYear = "B";
                            strCurrentYear = "C";
                            break;
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
                        case 9:
                            strYear = "I";
                            strCurrentYear = "J";
                            break;
                        default:
                            break;
                    }
                    switch (j)
                    {
                        case 2:
                            if ((strTitle == "全地区"))//全地区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                    obj.Cells[i, j].Formula = strCurrentYear + (i + 2 + 1) + "+" + strCurrentYear + (i + 4 + 1);
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[0]))//市辖供电区
                            {
                                
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                        obj.SetValue(i, j, SelectData(2010, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+1) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[1]))//县级供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                        obj.SetValue(i, j, SelectData(2010, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+2) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            } 
                            break;
                        case 3:
                            if ((strTitle == "全地区"))//全地区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                    obj.Cells[i, j].Formula = strCurrentYear + (i + 2 + 1) + "+" + strCurrentYear + (i + 4 + 1);
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[0]))//市辖供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                        obj.SetValue(i, j, SelectData(2011, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+1) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[1]))//县级供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                    obj.SetValue(i, j, SelectData(2011, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+2) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            break;
                        case 4:
                            if ((strTitle == "全地区"))//全地区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                    obj.Cells[i, j].Formula = strCurrentYear + (i + 2 + 1) + "+" + strCurrentYear + (i + 4 + 1);
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[0]))//市辖供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                        obj.SetValue(i, j, SelectData(2012, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+1) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[1]))//县级供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                    obj.SetValue(i, j, SelectData(2012, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+1) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            } 

                            break;
                        case 5:
                            if ((strTitle == "全地区"))//全地区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                    obj.Cells[i, j].Formula = strCurrentYear + (i + 2 + 1) + "+" + strCurrentYear + (i + 4 + 1);
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[0]))//市辖供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                        obj.SetValue(i, j, SelectData(2013, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+1) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[1]))//县级供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                    obj.SetValue(i, j, SelectData(2013, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+2) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            } 

                            break;
                        case 6:
                            if ((strTitle == "全地区"))//全地区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                    obj.Cells[i, j].Formula = strCurrentYear + (i + 2 + 1) + "+" + strCurrentYear + (i + 4 + 1);
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[0]))//市辖供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                        obj.SetValue(i, j, SelectData(2014, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear +(intDQ+1) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[1]))//县级供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {

                                    obj.SetValue(i, j, SelectData(2014, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+2) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            } 

                            break;
                        case 7:
                            if ((strTitle == "全地区"))//全地区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                    obj.Cells[i, j].Formula = strCurrentYear + (i + 2 + 1) + "+" + strCurrentYear + (i + 4 + 1);
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[0]))//市辖供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {

                                        obj.SetValue(i, j, SelectData(2015, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+1) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[1]))//县级供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {

                                    obj.SetValue(i, j, SelectData(2015, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+2) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            } 

                            break;
                        case 8:
                            obj.Cells[i, j].Formula = "POWER(H" + (i + 1) + " / C" + (i + 1) + ", 1 / 5) - 1";
                            break;
                        case 9:
                            if ((strTitle == "全地区"))//全地区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {
                                    obj.Cells[i, j].Formula = strCurrentYear + (i + 2 + 1) + "+" + strCurrentYear + (i + 4 + 1);
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + intDQ + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[0]))//市辖供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {

                                    obj.SetValue(i, j, SelectData(2020, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+1) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }
                            if ((strTitle == AreaType[1]))//县级供电区
                            {
                                if (strDQ == item[0])//最大负荷（MW）
                                {

                                    obj.SetValue(i, j, SelectData(2020, strDQ, CityID));
                                }
                                else//Tmax（h）
                                {
                                    obj.Cells[i, j].Formula = SheetN + strYear + (intDQ+2) + "*10000/" + strCurrentYear + (i - 1 + 1);
                                }
                            }

                            break;
                        case 10:
                            obj.Cells[i, j].Formula = "POWER(J" + (i + 1) + " / H" + (i + 1) + ", 1 / 5) - 1";
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
        public void SelectEditChange(FarPoint.Win.Spread.FpSpread fPobj, FarPoint.Win.Spread.SheetView SheetView, object obj, Itop.Client.Base.FormBase FB)
        {
            string strID = null;
            //strTitle = obj.EditValue.ToString();
            strID = obj.ToString();
            Ps_forecast_list pfl = null;
            projectID = FB.ProjectUID;
            string con1 = "ID='" + strID + "' and UserID='" + projectID + "'";
            try
            {
                //查询下拉菜单所选中的数据
                pfl = (Ps_forecast_list)Services.BaseService.GetObject("SelectPs_forecast_listByWhere", con1);
                programID = pfl.ID;
                SetLeftTitle(SheetView, 5, 0);//左侧标题
                CityID = SelectID(strID, AreaType[0]);//市辖
                CountyID = SelectID(strID, AreaType[1]);//县级

                WriteData(fPobj,SheetView, 5, 0);//数据
                S4_5.WriteData(fPobj, fPobj.Sheets[7], 5, 0);

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
        /// <param name="year"></param>
        /// <param name="ID"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        private double SelectData(int year,  string strTitle,string parentId)
        {
            string con = null;
            double value = 10;
            string strYear = "y" + year;
            con = "select " + strYear + " from Ps_forecast_Math where Title='" + strTitle + "'and ForecastID='" + programID + "' and Forecast='1'";
            try
            {
                value = (double)Services.BaseService.GetObject("SelectPFMdoubleWhatever", con);
            }
            catch (System.Exception e)
            {
                value = 10;
                //MessageBox.Show(strTitle + "年份是" + year + "中没有数据！！！");
            }
            return value;
        }
        /// <summary>
        /// 返回市辖，县级的id值
        /// </summary>
        /// <param name="ID"></param>
        private string SelectID(string ID, string strTitle)
        {
            string con = null;
            Ps_Forecast_Math pfm = null;
            con = "Title='" + strTitle + "'and ForecastID='" + ID + "' and (Forecast = '1')";//默认为1的算法,现在调试程序用2
            list = Services.BaseService.GetList("SelectPs_Forecast_MathByWhere", con);
            if (list.Count != 0)
            {
                pfm = (Ps_Forecast_Math)list[0];
                return pfm.ID;
            }
            else
            {
                return strTitle + "没有数据";
            }
        }
    }
}
