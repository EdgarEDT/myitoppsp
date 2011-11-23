using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Itop.Domain.Forecast;



/******************************************************************************************************
 *  ClassName：Sheet2_4
 *  Action：用来生成Sheet2_4报表的调用方法
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_History
 * 时间：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function
{
    class Sheet2_4
    {
        //private float[] floatSum = new float[7];//用来装，一产，二产，三产的值
        //public int SumValue = 0;
        private Function.Sheet2_3 SH2_3 = new Function.Sheet2_3();
        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function.Sheet2_N Sh2_N = new Function.Sheet2_N();
        //private Dictionary<string, Ps_History> resualt = new Dictionary<string, Ps_History>();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
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
            T.T1 = "全地区GDP（亿元）";
            T.T2 = "";
            T.T3 = "";
            T.T4 = "一产";
            T.T5 = "二产";
            T.T6 = "三产";
            T.T7 = "城镇人口（万人）";
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
            int IntRow = 6;
            int IntColCount = 7;
            //MessageBox.Show(year+"年");
            try
            {
                //计算一产，二产，三产之和++++++++++++++++++++++++++++++++++++++++

                SH2_3.ReturnRecordSet(FB, year, T.T4);
                SH2_3.ReturnRecordSet(FB, year, T.T5);
                SH2_3.ReturnRecordSet(FB, year, T.T6);
                SH2_3.SumThree(year, T.T4);
                SH2_3.SumThree(year, T.T5);
                SH2_3.SumThree(year, T.T6);
                SH2_3.resualt.Clear();
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


                //地市名称
                title = T.T9;
                WriteValue(obj, IntRow, 0, title, 0.00);
                for (int i = 0; i <2; i++)//2010年，2015年
                {
                    //GDP（亿元）
                    title = T.T1;
                    if (i == 0)
                    {
                        SH2_3.ReturnRecordSet(FB, year, title);
                        WriteValue(obj, IntRow, 1 + IntColCount * i, title, SH2_3.floatSum[3]);
                    }
                    if(i==1)
                    {
                        obj.Cells[IntRow, 1 + IntColCount * i].Formula = "SUM(J7:L7)";
                    }
                    //一产
                    title = T.T4;
                    if (i==1)
                    {
                        obj.SetValue(IntRow, 2 + IntColCount * i,0) ;
                    }
                    if (i == 0)
                    {
                        SH2_3.ReturnRecordSet(FB, year, title);
                        WriteValue(obj, IntRow, 2 + IntColCount * i, title, SH2_3.floatSum[0]);
                    }
                    //二产
                    title = T.T5;
                    if (i == 1 )
                    {
                        obj.SetValue(IntRow, 3 + IntColCount * i,0) ;
                    }
                    if (i == 0)
                    {
                        SH2_3.ReturnRecordSet(FB, year, title);
                        WriteValue(obj, IntRow, 3 + IntColCount * i, title, SH2_3.floatSum[1]);
                    }
                    //三产
                    title = T.T5;
                    if (i == 1 )
                    {
                        obj.SetValue(IntRow, 4 + IntColCount * i,0) ;
                    }
                    if (i == 0 )
                    {
                        SH2_3.ReturnRecordSet(FB, year, title);
                        WriteValue(obj, IntRow, 4 + IntColCount * i, title, SH2_3.floatSum[2]);
                    }
                    if(i==0)
                    {
                        obj.Cells[IntRow, 5].Formula = "C7/B7";
                        Sh2_N.SetCellType(obj, PC, IntRow, 5);
                        obj.Cells[IntRow, 6].Formula = "D7/B7";
                        Sh2_N.SetCellType(obj, PC, IntRow, 6);
                        obj.Cells[IntRow,7].Formula = "E7/B7";
                        Sh2_N.SetCellType(obj, PC, IntRow, 7);

                    }
                    if(i==1)
                    {
                        obj.Cells[IntRow, 12].Formula = "J7/B7";
                        Sh2_N.SetCellType(obj, PC, IntRow, 12);
                        obj.Cells[IntRow,13].Formula = "K7/B7";
                        Sh2_N.SetCellType(obj, PC, IntRow, 13);
                        obj.Cells[IntRow, 14].Formula = "L7/B7";
                        Sh2_N.SetCellType(obj, PC, IntRow, 14);
                    }
                }

            }
            catch (System.Exception e)
            {
                //MessageBox.Show("数据库没有标题为：" + title + "年份是：" + year + "年的数据");
            }
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        /// <param name="Title"></param>
        /// <param name="Value"></param>
        private void WriteValue(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol, string Title, double Value)
        {
            switch (Title)
            {
                case "地市名称":
                    obj.SetValue(IntRow, IntCol, "铜陵");
                    SH2_3.resualt.Clear();
                    break;
                case "全地区GDP（亿元）":
                    obj.SetValue(IntRow, IntCol, PF.ReturnFormatStr(Value));
                    SH2_3.resualt.Clear();
                    break;
                case "一产":
                    obj.SetValue(IntRow, IntCol, PF.ReturnFormatStr(Value));
                    SH2_3.resualt.Clear();
                    break;
                case "二产":
                    obj.SetValue(IntRow, IntCol,PF.ReturnFormatStr(Value));
                    SH2_3.resualt.Clear();
                    break;
                case "三产":
                    obj.SetValue(IntRow, IntCol, PF.ReturnFormatStr(Value));
                    SH2_3.resualt.Clear();
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
        public  void ColReadOnly(FarPoint.Win.Spread.SheetView obj, int IntCol)
        {
            for (int i = 0; i < IntCol; i++)
            {
                if (i != 9 && i != 10 && i != 11)
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
