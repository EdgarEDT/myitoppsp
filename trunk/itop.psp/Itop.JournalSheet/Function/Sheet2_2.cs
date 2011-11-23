using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Itop.Domain.Forecast;
using Itop.Client.Common;
using System.Security.Cryptography;




/******************************************************************************************************
 *  ClassName：Sheet2_2
 *  Action：用来生成Sheet2_2报表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_History，Ps_YearRange（年份表）
 * 时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function
{

    class Sheet2_2
    {
        private int IntType = 1;
        private string ParentId = null;
        private float[] floatSum = new float[7];//用来装，一产，二产，三产的值
        private Dictionary<string, Ps_History> resualt = new Dictionary<string, Ps_History>();
        private PublicFunction PF = new PublicFunction();
        private Sheet2_N S2_N = new Sheet2_N();

        private Title T = new Title();

        private struct  Title
        {
            public string T1;
            public string T2;
            public string T3;
            public string T4;
            public string T5;
            public string T6;
            public string T7;
            public string T8;
            public string T9;
        }

        /// <summary>
        /// 添加年份
        /// </summary>
        /// <param name="BE">BarEditItem object</param>
        public void AddBarEditYears(DevExpress.XtraBars.BarEditItem BE)
        {
            BE.EditValue = "2000";
            AddBarEditItems(BE);
            
        }

        public void InitTitle()
        {
            T.T1 = "土地面积（km2）";
            T.T2 = "建成区面积（km2）";
            T.T3 = "全地区GDP（亿元）";
            T.T4 = "一产";
            T.T5 = "二产";
            T.T6 = "三产";
            T.T7 = "城镇人口（万人）";
            T.T8 = "乡村人口（万人）";
            T.T9 = "地市名称";
        }
        /// <summary>
        /// add BareditItems of years
        /// </summary>
        /// <param name="BE">BarEditItem object</param>
        private void AddBarEditItems(DevExpress.XtraBars.BarEditItem BE)
        {
            int FirstYear = 2000;
            int EndYear=2060;
            for (int i = FirstYear; i <= EndYear; i++)
            {
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BE.Edit).Items.Add(FirstYear.ToString());
                FirstYear++;
            }
        }
        /// <summary>
        /// get date
        /// </summary>
        /// <param name="obj">SheetView object</param>
        /// <param name="FB">FormBase object</param>
        /// <param name="BE">barEditItem object</param>
        public void ConnectionDate(FarPoint.Win.Spread.SheetView obj,Itop.Client.Base.FormBase FB,DevExpress.XtraBars.BarEditItem BE)
        {
            string year = "y"+BE.EditValue.ToString();
            string title = null;
            double Temp = 0.0;
            double Temp1 = 0.0;
            string strTemp = null;
            //MessageBox.Show(year+"年");
            try
            {
                //地市名称
                title = T.T9;
                WriteValue(obj, 5, 0,title, 0.00);
                //土地面积（km2）
                title = T.T1;
                ReturnRecordSet(FB,year, title);
                WriteValue(obj, 5, 1,title, PF.Gethistroyvalue<Ps_History>(resualt[title],year));
                //建成区面积（km2）
                title = T.T2;
                ReturnRecordSet(FB,year, title);
                WriteValue(obj, 5, 2,title, PF.Gethistroyvalue<Ps_History>(resualt[title], year));
                //GDP（亿元）
                title = T.T3;
                ReturnRecordSet(FB,year, title);
                WriteValue(obj, 5, 3, title,PF.ReturnFormatStr( PF.Gethistroyvalue<Ps_History>(resualt[title], year)));
                //计算一产，二产，三产之和++++++++++++++++++++++++++++++++++++++++
                title = T.T4;
                ReturnRecordSet(FB,year, title);
                floatSum[0]=(float)PF.Gethistroyvalue<Ps_History>(resualt[title], year);


                title = T.T5;
                ReturnRecordSet(FB, year, title);
                floatSum[1] = (float)PF.Gethistroyvalue<Ps_History>(resualt[title], year);
                
                title = T.T6;
                ReturnRecordSet(FB, year, title);
                floatSum[2] =(float) PF.Gethistroyvalue<Ps_History>(resualt[title], year);

                floatSum[3] = floatSum[0] + floatSum[1] + floatSum[2];
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //一产
                WriteValue(obj, 5, 4, title, floatSum[0]/floatSum[3]);
                //PF.SetRowHight(obj, 5, 4,  floatSum[0]/floatSum[3]);
                //二产
                title = T.T5;
                WriteValue(obj, 5, 5, title, floatSum[1] / floatSum[3]);
                //PF.SetRowHight(obj, 5, 5,  floatSum[0]/floatSum[3]);
                //三产
                title = T.T6;
                WriteValue(obj, 5, 6, title, floatSum[2] / floatSum[3]);
                //PF.SetRowHight(obj, 5, 6,  floatSum[0]/floatSum[3]);
                //年末总人口（万人）
                title = T.T7;//城镇人口
                ReturnRecordSet(FB,year, title);
                Temp=PF.Gethistroyvalue<Ps_History>(resualt[title], year);
               
                title = T.T8;//乡村人口
                ReturnRecordSet(FB, year, title);
                Temp1 = PF.Gethistroyvalue<Ps_History>(resualt[title], year);
                title = "年末总人口（万人）";
                WriteValue(obj, 5, 7, title, PF.ReturnFormatStr(Temp + Temp1));
                //人均GDP（万元/人）
                floatSum[4]=S2_N.StrToFloat(obj.GetValue(5,3).ToString());
                floatSum[5]=S2_N.StrToFloat(obj.GetValue(5,7).ToString());
                WriteValue(obj, 5, 8, title, (floatSum[4]/floatSum[5]));
                //PF.SetRowHight(obj, 5, 8, (floatSum[4] / floatSum[5]));
                //城镇化率（%）
                floatSum[6]=S2_N.StrToFloat(obj.GetValue(5,7).ToString());
                WriteValue(obj, 5, 9, title, PF.ReturnFormatStr(Temp / floatSum[6]));

            }
            catch (System.Exception e)
            {
                //MessageBox.Show("数据库没有标题为："+title+"年份是："+year+"年的数据");
            }

        }
        /// <summary>
        /// 返回记录集
        /// </summary>
        private void ReturnRecordSet(Itop.Client.Base.FormBase FB,string Year, string Title)
        {
            string Temp1 = "y" + Year;
            Ps_History GDP1 = null;
            string con = null;
            if (Title == "一产" || Title == "二产" || Title == "三产")
            {
                con = "Title='" + Title + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
                con = con + "AND ParentID='"+ParentId+"'";
            }
            else
            {
                con = "Title='" + Title + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
            }
            GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            if (Title == "全地区GDP（亿元）")
            {
                ParentId = GDP1.ID;
            }
            resualt.Add(Title, GDP1);

        }
        /// <summary>
        /// 求出的结果写入文档
        /// </summary>
        /// <param name="obj">SheetView object</param>
        /// <param name="IntRow">rows</param>
        /// <param name="IntCol">columns</param>
        /// <param name="Title">Title</param>
        private void WriteValue(FarPoint.Win.Spread.SheetView obj,int IntRow,int IntCol,string Title,object Value)
        {
            switch (Title)
            {
                case "地市名称":
                    obj.SetValue(IntRow, IntCol, "铜陵");
                    resualt.Clear();
                    break;
                case "土地面积（km2）":
                    obj.SetValue(IntRow, IntCol, Value);
                    resualt.Clear();
                    break;
                case "建成区面积（km2）":
                    obj.SetValue(IntRow, IntCol, Value);
                    resualt.Clear();
                    break;
                case "全地区GDP（亿元）":
                    obj.SetValue(IntRow, IntCol, Value);
                    resualt.Clear();
                    break;
                case "一产":
                    obj.SetValue(IntRow, IntCol, Value);
                    resualt.Clear();
                    break;
                case "二产":
                    obj.SetValue(IntRow, IntCol, Value);
                    resualt.Clear();
                    break;
                case "三产":
                    obj.SetValue(IntRow, IntCol, Value);
                    resualt.Clear();
                    break;
                case "年末总人口（万人）":
                    obj.SetValue(IntRow, IntCol, Value);
                    break;
                case "人均GDP（万元/人）":
                    obj.SetValue(IntRow, IntCol, Value);
                    break;
                case "城镇化率（%）":
                    obj.SetValue(IntRow, IntCol, Value);
                    break;
                default:
                    break;
            }
        }
        ///// <summary>
        ///// 返回保留2位小数
        ///// </summary>
        ///// <param name="dou">要传入的数值</param>
        ///// <returns></returns>
        //private string ReturnFormatStr(double dou )
        //{
        //    string Place = null;
        //    Place = dou.ToString();
        //    Place = string.Format("{0:F}", dou);
        //    return Place;
        //}
    }
}
