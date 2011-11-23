using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Domain.Forecast;
using Itop.Client.Common;
/******************************************************************************************************
 *  ClassName：Sheet10_11
 *  Action：用来生成表10‑11  配电网“十二五”规划经济效益指标（发策部、财务部、营销部、四县公司）的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * Synopsis:简介
 * 当前表用下拉框选择数据，初始为空。数据库用ps_History，ps_forecast_list，ps_forecast_Math
 * 三个表，下拉框的数据从ps_forecast_list表取数据，方案值
 * （1）单位投资增售电量=(2015年售电量－2010年售电量) /“十二五”电网投资（千瓦时/元），
 * 其中“十二五”电网投资”包括220kV及以上电网投资，2010年数据采用计划执行数或预测
 * （2）单位投资增供负荷＝ (2015年网供最大负荷－2010年网供最大负荷) /“十二五”电网投资（千瓦/元）；
 * （3）电网资产收入比=2015年售电收入/(（电网2010年末资产原值+2015年电网年末资产原值）/2)（%）。
 * 解释：
 * 1、2015年售电量－2010年售电量：在表ps_History中取ForecastID=ps_forecast_list.UserID
 * 后在字段y2010也就是年份中取值但是后缀必须有“售电量”的标志，如果没有就提示客户。
 * “十二五”电网投资（千瓦时/元）：十二五指2011年到2015年,电压总值除以总钱数，Ps_Table_TZGS表
 * 的Aumount字段的金额（条件，>=220kv）
 * 2、2015年网供最大负荷－2010年网供最大负荷，在表ps_History中取，字段Title的全社会最大负荷。
 * 3、2015年售电收入=表ps_History，字段Title中找2010年末资产原值也是一样
 * 概述：Forecast = '1'是用第一种预测算法，后几种算法现在没考虑
 *  时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function10
{
    class Sheet10_11
    {
        private const int RowCount = 3;
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private double dTwelveFiveGridInvestment = 0;//十二五电网投资
        private double dNetworkMaxDuty = 0;//网供最大负荷2015-2010
        private double dSaleOfElectricity = 0;//2015-2010年售电收入
        private double dInitialAssetValue = 0;//（电网2010年末资产原值+2015年电网年末资产原值）
        private double dYearSaleEletricity = 0;//2015年的售电量


        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        private Function10.Sheet10_1_1 S10_1_1 = new Function10.Sheet10_1_1();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet10_11Title(FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 2;
            int IntRowCount = RowCount + 2 + 3;//标题占3行，分区类型占2行，
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

            strTitle = " 指标 ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 2015年数值 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);



            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            SetLeftTitle(obj, IntRow, IntCol);//左侧标题
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
            //PF.SetWholeColumnsWidth(obj);//列宽
        }
        /// <summary>
        /// 写左侧标题
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            for (int i = IntRow; i < (RowCount + IntRow); ++i)
            {
                if (i == IntRow)
                {
                    PF.CreateSheetView(obj, 1, 1, (i), 0, "单位投资增售电量（kWh/元）");
                }
                if (i == IntRow+1)
                {
                    PF.CreateSheetView(obj, 1, 1, (i), 0, "单位投资增供负荷（kW/元）");
                }
                if (i ==IntRow+ 2)
                {
                    PF.CreateSheetView(obj, 1, 1, (i), 0, "电网资产收入比（%）");
                }
            }
        }
        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            double temp = 0;
            for (int i = IntRow; i < obj.RowCount; ++i)
            {
                if(i==IntRow)
                {
                    temp = dSaleOfElectricity / dTwelveFiveGridInvestment;
                    obj.SetValue(i, IntCol + 1, temp);
                }
                if(i==IntRow+1)
                {
                    temp = dNetworkMaxDuty / dTwelveFiveGridInvestment;
                    obj.SetValue(i, IntCol + 1, temp);
                }
                if(i==IntRow+2)
                {
                    temp = (dYearSaleEletricity / dInitialAssetValue)/2*100;
                    obj.SetValue(i, IntCol + 1, temp+"%");

                }
            }
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
        public void AddBarEditItems(DevExpress.XtraBars.BarEditItem BE, DevExpress.XtraBars.BarEditItem BE1, Itop.Client.Base.FormBase FB)
        {
            IList list = SelectProgramme(FB);
            Ps_forecast_list pfl = null;
            //BE.EditValue = "2000";
            for(int i=0;i<list.Count;++i)
            {
                pfl = (Ps_forecast_list)list[i];
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BE.Edit).Items.Add(pfl.Title);
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BE1.Edit).Items.Add(pfl.ID);
            }

        }
        /// <summary>
        /// 选中下拉菜单中的数据连接数据库表,方案
        /// </summary>
        /// <param name="BE"></param>
        public void SelectEditChange(FarPoint.Win.Spread.SheetView SheetView, object obj, Itop.Client.Base.FormBase FB)
        {
            string strTitle = null;
            string strID = null;
            strID = obj.ToString();
            Ps_forecast_list pfl=null;
            string con1 = "ID='" + strID + "' and UserID='" + FB.ProjectUID + "'";
            try
            {
                //查询下拉菜单所选中的数据
                pfl = (Ps_forecast_list)Services.BaseService.GetObject("SelectPs_forecast_listByWhere", con1);
                strTitle = pfl.Title;
               //“十二五”电网投资（千瓦时/元）
               dTwelveFiveGridInvestment=TwelveFiveGridInvestment(pfl.ID, strTitle, FB);
               //(2015年网供最大负荷－2010年网供最大负荷)
               dNetworkMaxDuty=NetworkMaxDuty(pfl.ID, strTitle);
               //2015-2010年售电收入
               dSaleOfElectricity=SaleOfElectricity(pfl.ID, strTitle);
               //（电网2010年末资产原值+2015年电网年末资产原值）
               dInitialAssetValue=InitialAssetValue(pfl.ID, strTitle);
                //单年售电量
               dYearSaleEletricity=YearSaleEletricity(pfl.ID, strTitle, 2015);
               WriteData(SheetView, 5, 0);//数据
           }
            catch(System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }

        }
        /// <summary>
        /// 2015-2010的售电量
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="strTitle">标题</param>
        private double  SaleOfElectricity(string ParentID,string strTitle)
        {
            double SOE = 0;
            string strTemp="售电量";
            string con = null;
            con = " ForecastID='" + ParentID + "' and Title='售电量' and Forecast = '1'";
            try
            {
                SOE = (double)Services.BaseService.GetObject("SelectPFM_D_Value", con);
            }
            catch(System.Exception e)
            {
                //MessageBox.Show(strTitle+"下没有"+strTemp+"的数据！！！");
            }
            return SOE;
        }
        /// <summary>
        /// 单年售电量
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        private double YearSaleEletricity(string ParentID,string strTitle,int year)
        {
            double SOE = 0;
            string strTemp = "售电量";
            string con = null;
            string strYear = "y" + year;
            con = " ForecastID='" + ParentID + "' and Title='售电量' and Forecast = '1' and " + strYear + "='" + year + "'";
            try
            {
                SOE = (double)Services.BaseService.GetObject("SelectPFM_D_Value", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(strTitle + "下没有" + strTemp + "的数据！！！");
            }
            return SOE;
        }
        /// <summary>
        /// （电网2010年末资产原值+2015年电网年末资产原值）
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        private double InitialAssetValue(string ParentID,string strTitle)
        {
            double MD = 0;
            string strTemp = "年末资产原值";
            string con = " ForecastID='" + ParentID + "' and Title like('" + strTemp + "') and Forecast = '1'";
            try
            {
                MD = (double)Services.BaseService.GetObject("SelectPFMZCYZDuty", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(strTitle + "下没有" + strTemp + "的数据！！！");
            }
            return MD;
        }
        /// <summary>
        /// 网供最大负荷2015-2010值
        /// </summary>
        /// <returns></returns>
        private double NetworkMaxDuty(string ParentID,string strTitle)
        {
            double MD = 0;
            string strTemp = "全社会最大负荷";
            string con = " ForecastID='" + ParentID + "' and Title like('" + strTemp + "(%') and Forecast = '1'";
            try
            {
                MD = (double)Services.BaseService.GetObject("SelectPFM_D_Value", con);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(strTitle + "下没有" + strTemp + "的数据！！！");
            }
            return MD;
        }
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// “十二五”电网投资（千瓦时/元）
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        private double TwelveFiveGridInvestment(string ParentID, string strTitle, Itop.Client.Base.FormBase FB)
        {
            double Temp = 0;
            double MaxDuty = 0;//最大用电量
            string strTemp = "全社会用电量";
            string con = " ForecastID='" + ParentID + "' and Title like ('" + strTemp + "(%') and Forecast = '1'";
            try
            {
                MaxDuty = (double)Services.BaseService.GetObject("SelectPFM2011and2015Duty", con);//总电压值
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(strTitle + "下没有" + strTemp + "的数据！！！");
                //MessageBox.Show(e.Message);
            }

            double Amount = 0;
            con = "ProjectID='" + FB.ProjectUID + "' and  (BuildYear between '2011'and '2015') and cast(substring(BianInfo,1,charindex('@',BianInfo,0)-1)as int)>=220";
            try
            {
                Amount = (double)Services.BaseService.GetObject("SelectTZGSSUMAmount", con);//总钱数
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(strTitle + "下没有" + strTemp + "的数据！！！");
                //MessageBox.Show(e.Message);
            }
            if(Amount==0)
            {
                Amount = 1;
            }
            Temp = MaxDuty / Amount;
            return Temp;
        }
    }
}
