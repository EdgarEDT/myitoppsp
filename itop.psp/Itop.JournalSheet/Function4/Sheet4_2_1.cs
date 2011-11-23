using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Domain.Forecast;
using Itop.Client.Common;
/******************************************************************************************************
 *  ClassName：Sheet4_2_1
 *  Action：,sheet4_2_1附表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_forecast_Math，Ps_forecast_list，
 * 时间：2010-10-11
 * 。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function4
{
    class Sheet4_2_1
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
        private string CityID = null;//市辖id
        private string CountyID = null;//县级id 
        private const int ColumnCount = 11;//列数
        private IList list = null;//存放县级和市辖的所有数据

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet4_2_1Title(FarPoint.Win.Spread.SheetView obj, string Title)
        {
            AreaType[0] = "市辖供电区";
            AreaType[1] = "县级供电区";


            int IntColCount = ColumnCount;
            int IntRowCount = 2 + 3;//标题占3行，分区类型占2行，
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
            int IntRowCount = (City.Count + County.Count) + IntRow;//标题占3行，分区类型占2行，
            obj.Rows.Count = IntRowCount;
            obj.Columns.Count = ColumnCount;
            int indexCity = 0;
            int indexCounty = 0;
            for (int i = IntRow; i <obj.RowCount; ++i)
            {
                if (i == IntRow)
                {
                    PF.CreateSheetView(obj, City.Count, NextColMerge, i, 0, "市辖供电区");
                }
                if (i == IntRow+City.Count)
                {
                    PF.CreateSheetView(obj, County.Count, NextColMerge, i, 0, "县级供电区");
                }
                if(i>=IntRow&&i<IntRow+City.Count)
                {
                    PF.CreateSheetView(obj, 1, NextColMerge, i, 1, City[indexCity].ToString());
                    indexCity++;
                }
                if (i >= IntRow + City.Count && i <obj.RowCount )
                {
                    PF.CreateSheetView(obj, 1, NextColMerge, i, 1, County[indexCounty].ToString());
                    indexCounty++;
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
            string strTitle = null;
            string strDQ = null;
            for (int i = IntRow; i < obj.RowCount; ++i)
            {
                strTitle = (string)PF.ReturnStr(obj,i,0);
                strDQ = (string)PF.ReturnStr(obj, i, 1);
                for (int j = 2; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 2:
                            if (strTitle == "县级供电区")
                            {
                                obj.SetValue(i, j, SelectData(2010, programID, strDQ,CountyID));
                            }
                            if (strTitle == "市辖供电区")
                            {
                                obj.SetValue(i, j, SelectData(2010, programID, strDQ, CityID));
                            }
                            break;
                        case 3:
                            if (strTitle == "县级供电区")
                            {
                                obj.SetValue(i, j, SelectData(2011, programID, strDQ, CountyID));
                            }
                            if (strTitle == "市辖供电区")
                            {
                                obj.SetValue(i, j, SelectData(2011, programID, strDQ, CityID));
                            }
                            break;
                        case 4:
                            if (strTitle == "县级供电区")
                            {
                                obj.SetValue(i, j, SelectData(2012, programID, strDQ, CountyID));
                            }
                            if (strTitle == "市辖供电区")
                            {
                                obj.SetValue(i, j, SelectData(2012, programID, strDQ, CityID));
                            }
                            break;
                        case 5:
                            if (strTitle == "县级供电区")
                            {
                                obj.SetValue(i, j, SelectData(2013, programID, strDQ, CountyID));
                            }
                            if (strTitle == "市辖供电区")
                            {
                                obj.SetValue(i, j, SelectData(2013, programID, strDQ, CityID));
                            }

                            break;
                        case 6:
                            if (strTitle == "县级供电区")
                            {
                                obj.SetValue(i, j, SelectData(2014, programID, strDQ, CountyID));
                            }
                            if (strTitle == "市辖供电区")
                            {
                                obj.SetValue(i, j, SelectData(2014, programID, strDQ, CityID));
                            }

                            break;
                        case 7:
                            if (strTitle == "县级供电区")
                            {
                                obj.SetValue(i, j, SelectData(2015, programID, strDQ, CountyID));
                            }
                            if (strTitle == "市辖供电区")
                            {
                                obj.SetValue(i, j, SelectData(2015, programID, strDQ, CityID));
                            }

                            break;
                        case 8:
                            obj.Cells[i, j].Formula = "POWER(H" + (i + 1) + " / C" + (i + 1) + ", 1 / 5) - 1";
                            break;
                        case 9:
                            if (strTitle == "县级供电区")
                            {
                                obj.SetValue(i, j, SelectData(2020, programID, strDQ, CountyID));
                            }
                            if (strTitle == "市辖供电区")
                            {
                                obj.SetValue(i, j, SelectData(2020, programID, strDQ, CityID));
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
        public void SelectEditChange(FarPoint.Win.Spread.SheetView tempSheet, object obj, Itop.Client.Base.FormBase FB)
        {
            //string strTitle = null;
            string strID = obj.ToString();
            string[] AreaType1 = new string[2];
            AreaType1[0] = "市辖供电区";
            AreaType1[1] = "县级供电区";

            //Ps_forecast_list pfl = null;
            projectID = FB.ProjectUID;
            //string con1 = "Title='" + strTitle + "' and UserID='" + projectID + "'";
            try
            {
                //查询下拉菜单所选中的数据
                //pfl = (Ps_forecast_list)Services.BaseService.GetObject("SelectPs_forecast_listByWhere", con1);
                //programID = pfl.ID;
                programID = strID;
                
                CityID=SelectID(strID, AreaType1[0]);//市辖
               CountyID= SelectID(strID, AreaType1[1]);//县级
               SelectDQ(CityID, AreaType1[0]);
               SelectDQ(CountyID, AreaType1[1]);
               SetLeftTitle(tempSheet, 5, 0);//左侧标题
               WriteData(tempSheet, 5, 0);//数据

               PF.Sheet_GridandCenter(tempSheet);//画边线，居中
               S10_1.ColReadOnly(tempSheet, tempSheet.Columns.Count);
               PF.SetWholeRowHeight(tempSheet, tempSheet.Rows.Count, tempSheet.Columns.Count);//行高
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
        private double SelectData(int year, string ForecastID, string strTitle, string parentid)
        {
            string con = null;
            double value = 10;
            string strYear = "y" + year;
            con = "select " + strYear + " from Ps_forecast_Math where Title='" + strTitle + "'and ForecastID='" + ForecastID + "' and ParentID='" + parentid + "'";
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
        private void SelectDQ(string ID,string strTitle)
        {
            string con = "";
            con = "select Title from Ps_forecast_Math where  ForecastID='" + programID + "' and ParentID='" + ID + "' group by Title";
            try
            {
                if (strTitle == "市辖供电区")//市辖供电区
                {
                    City = Services.BaseService.GetList("SelectPFMStringWhatever", con);
                    if(City.Count==0)
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
        private string  SelectID(string ID,string strTitle)
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
        /// <summary>
        /// 这个是用lookupEdit控件的方法现在没有用这个控件
        /// </summary>
        /// <param name="LE"></param>
        /// <param name="FB"></param>
        public void AddLookUpEditValue(DevExpress.XtraEditors.LookUpEdit LE, Itop.Client.Base.FormBase FB)
        {
            IList list = SelectProgramme(FB);
            Ps_forecast_list pfl = null;
            LE.EditValue = list;
            //BE.EditValue = "2000";
            //for (int i = 0; i < list.Count; ++i)
            //{
            //    pfl = (Ps_forecast_list)list[i];
            //    LE.EditValue = pfl;
            //    //((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)LE.EditValue).Items.Add(pfl.Title);
            //} 
        }
        /// <summary>
        /// 查询方案内容，给下拉菜单用
        /// </summary>
        private IList SelectProgramme(Itop.Client.Base.FormBase FB)
        {
            IList Programme = null;//方案
            //Ps_forecast_list report = new Ps_forecast_list();
            //report.UserID =FB. ProjectUID;
            Programme = Services.BaseService.GetList("SelectPs_forecast_listByUserID", FB.ProjectUID);
            return Programme;
        }
        /// <summary>
        /// add BareditItems of years
        /// </summary>
        /// <param name="BE">BarEditItem object</param>
        public void AddBarEditItems(DevExpress.XtraBars.BarEditItem BE,DevExpress.XtraBars.BarEditItem BE1, Itop.Client.Base.FormBase FB)
        {
            IList list = SelectProgramme(FB);
            Ps_forecast_list pfl = null;
            //BE.EditValue = "2000";
            //BE.EditValue = list;
            for (int i = 0; i < list.Count; ++i)
            {
                pfl = (Ps_forecast_list)list[i];
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BE.Edit).Items.Add(pfl.Title);
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BE1.Edit).Items.Add(pfl.ID);
            }
        }
    }
}
