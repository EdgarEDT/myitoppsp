using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Itop.Domain.Forecast;
using Itop.Client.Common;


/******************************************************************************************************
 *  ClassName：Sheet2_3
 *  Action：用来生成Sheet2_3报表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_History
 * 时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function
{
    class Sheet2_3
    {
        private int IntType=1;
        public  float[] floatSum = new float[7];//用来装，一产，二产，三产的值
        public  Dictionary<string, Ps_History> resualt = new Dictionary<string, Ps_History>();
        private PublicFunction PF = new PublicFunction();
        private Sheet2_N S2_N = new Sheet2_N();

        private Title T = new Title();

        private struct Title
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

        public void InitTitle()
        {
            T.T1 = "2010年（万人）";
            T.T2 = "2015年（万人）";
            T.T3 = "年均增长率（%）";
            T.T4 = "一产";
            T.T5 = "二产";
            T.T6 = "三产";
            T.T7= "城镇人口（万人）";
            T.T8 = "乡村人口（万人）";
            T.T9 = "地市名称";
        }

        /// <summary>
        /// get date
        /// </summary>
        /// <param name="obj">SheetView object</param>
        /// <param name="FB">FormBase object</param>
        /// <param name="BE">barEditItem object</param>
        public void ConnectionDate(FarPoint.Win.Spread.SheetView obj, Itop.Client.Base.FormBase FB)
        {
            string year = "y2010";
            string title = null;
            double Temp = 0.0;
            double Temp1 = 0.0;
            int IntRow = 6;
            //MessageBox.Show(year+"年");
            try
            {
                //计算一产，二产，三产之和++++++++++++++++++++++++++++++++++++++++

                ReturnRecordSet(FB, year, T.T4);
                ReturnRecordSet(FB, year, T.T5);
                ReturnRecordSet(FB, year, T.T6);
                SumThree(year,T.T4);
                SumThree(year, T.T5);
                SumThree(year, T.T6);
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                //计算年末总人口（万人）
                title = T.T7;//城镇人口
                ReturnRecordSet(FB, year, title);
                Temp = PF.Gethistroyvalue<Ps_History>(resualt[title], year);
                title = T.T8;//乡村人口
                ReturnRecordSet(FB, year, title);
                Temp1 = PF.Gethistroyvalue<Ps_History>(resualt[title], year);

                //地市名称
                title = T.T9;
                WriteValue(obj, IntRow, 0, title, 0.00);
                for (int i = 1; i <=3; i++)//人口，GDP,人均GDP
                {
                    //2010年（万人）
                    title = T.T1;
                    ReturnRecordSet(FB, year, title);
                    if(i==1)
                    {
                        WriteValue(obj, IntRow, 1, title,PF.ReturnFormatStr( Temp + Temp1));
                    }
                    if(i==2)
                    {
                        WriteValue(obj, IntRow, 4, title, PF.ReturnFormatStr(floatSum[3]));
                    }
                    if(i==3)
                    {
                        obj.Cells[IntRow, 7].Formula = "E" + (IntRow + 1) + "/B" + (IntRow + 1);
                    }
                    //2015年（万人）手写
                    title = T.T2;
                    //obj.SetValue(IntRow,2,225.13);
                    //obj.SetValue(IntRow, 5, 225.13);
                    //obj.SetValue(IntRow, 8, 225.13);

                    //年均增长率（%）
                    title = T.T3;
                    if(i==1)
                    {
                        if(obj.GetValue(IntRow,2)==null)
                        {
                            obj.SetValue(IntRow, 3, 0);
                        }
                        obj.Cells[IntRow, 3].Formula = "POWER(((C" + (IntRow + 1) + ")/(B" + (IntRow + 1) + ")), 0.2) - 1";
                        //PF.SetRowHight(obj, IntRow, 3, obj.GetValue(IntRow, 3).ToString());
                    }
                    if(i==2)
                    {
                        if(obj.GetValue(IntRow,5)==null)
                        {
                            obj.SetValue(IntRow, 5, 0);
                        }
                        obj.Cells[IntRow, 3 * i].Formula = "POWER(F" + (IntRow + 1) + "/E" + (IntRow + 1) + ", 0.2) - 1";
                        //PF.SetRowHight(obj, IntRow, 3 * i, obj.GetValue(IntRow, 3 * i).ToString());
                    }
                    if(i==3)
                    {
                        if(obj.GetValue(IntRow,8)==null)
                        {
                            obj.SetValue(IntRow,8,0);
                        }
                        obj.Cells[IntRow, 3 * i].Formula = "POWER(I" + (IntRow + 1) + "/H" + (IntRow + 1) + ", 0.2) - 1";
                        //PF.SetRowHight(obj, IntRow, 3 * i, obj.GetValue(IntRow, 3 * i).ToString());
                    }
                }

            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
                //MessageBox.Show("数据库没有标题为：" + title + "年份是：" + year + "年的数据");
            }

        }
        /// <summary>
        /// 返回记录集
        /// </summary>
        public  void ReturnRecordSet(Itop.Client.Base.FormBase FB, string Year, string Title)
        {
            //string Temp1 = "y" + Year;
            Ps_History GDP1 = null;
            string con = null;
            con = "Title='" + Title + "'AND Col4='" + FB.ProjectUID + "'AND Forecast='" + IntType + "'";
            GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add(Title, GDP1);

        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        /// <param name="Title"></param>
        /// <param name="Value"></param>
        private  void WriteValue(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol, string Title, object Value)
        {
            switch(Title)
            {
                case "地市名称":
                    obj.SetValue(IntRow, IntCol, "铜陵");
                    resualt.Clear();
                    break;
                case "2010年（万人）":
                    obj.SetValue(IntRow,IntCol,Value);
                    resualt.Clear();
                    break;
                //case "2015年（万人）":
                //    obj.SetValue(IntRow, IntCol, Value);
                //    resualt.Clear();
                //    break;
                //case "年均增长率（%）":
                //    obj.SetValue(IntRow, IntCol, Value);
                //    resualt.Clear();
                //    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 计算三产之和
        /// </summary>
        public void  SumThree( string year,string title)
        {
            switch(title)
            {
                case "一产":
                    floatSum[0] = (float)PF.Gethistroyvalue<Ps_History>(resualt[title], year);
                    break;
                case "二产":
                    floatSum[1] = (float)PF.Gethistroyvalue<Ps_History>(resualt[title], year);
                    break;
                case"三产":
                    floatSum[2] = (float)PF.Gethistroyvalue<Ps_History>(resualt[title], year);

                    floatSum[3] = floatSum[0] + floatSum[1] + floatSum[2];
                    break;
                default:
                    break;
            }

            
        }

        /// <summary>
        /// 设置某列为只读
        /// </summary>
        /// <param name="obj">sheetView对象</param>
        /// <param name="IntCol">列数</param>
        public   void ColReadOnly(FarPoint.Win.Spread.SheetView obj, int IntCol)
        {
            for (int i = 0; i < IntCol; i++)
            {
                if (i !=2 &&i!=5&&i!=8)
                {
                    obj.Columns[i].Locked = true;//列设置为只读
                }
                else
                {
                    obj.Columns[i].Locked = false;//列设置为读写
                }
            }
        }
        /// <summary>
        /// 返回保留2位小数
        /// </summary>
        /// <param name="dou">要传入的数值</param>
        /// <returns></returns>
        //private string ReturnFormatStr(double dou)
        //{
        //    string Place = null;
        //    Place = dou.ToString();
        //    Place = string.Format("{0:F}", dou);
        //    return Place;
        //}
    }
}
