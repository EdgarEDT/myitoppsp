using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
using Itop.Common;
using Itop.Domain.Forecast;

namespace Itop.Client.Forecast
{
    public class Calculator
    {


        private static string type="";
        public static string Type
        {
            get { return type; }
            set { type = value; }
        }

        private static int startyear = 0;
        public static int StartYear
        {
            get { return startyear; }
            set { startyear = value; }
        }
 //private static int endyear = 0;
 //       public static int EndYear
 //       {
 //           get { return endyear; }
 //           set { endyear = value; }
 //       }


        /// <summary>
        /// 计算年均年增长率
        /// </summary>
        /// <param name="historyValue">历史数据数组</param>
        /// <returns>年均年增长率</returns>
        public static double AverageIncreasing(double[] historyValue)
        {
            int length = historyValue.Length - 1;
            double db= Math.Pow(historyValue[length] / historyValue[0], 1.0 / length) - 1;
            if (db.ToString() == "非数字" || db.ToString() == "正无穷大")
                db = 0;
            return db;

        }

        


        /// <summary>
        /// 计算电力消费弹性系数
        /// </summary>
        /// <param name="powerAverageIncreasing">电力消费年均增长率</param>
        /// <param name="GNPAverageIncreasing">国民经济年均增长率</param>
        /// <returns>电力消费弹性系数</returns>
        public static double SpringCoefficient(double powerAverageIncreasing, double GNPAverageIncreasing)
        {
            return powerAverageIncreasing / GNPAverageIncreasing;
        }



        /// <summary>
        /// 利用弹性系数法计算预测数据
        /// </summary>
        /// <param name="baseValue">历史数据</param>
        /// <param name="baseValue">基年数据</param>
        /// <param name="springCoefficient">预测采用的弹性系数</param>
        /// <param name="years">要预测的年数</param>
        /// <param name="ForecastID">ForecastID</param>
        /// <returns>预测结果数组</returns>
        public static double[] SpringCoefficientMethod(double baseValue, int years, string ForecastID)
        {
            double[] rt = new double[years];

            double s1 = baseValue; //每次的增长
            double s2 = 0; //增长的合计

            
            Ps_Calc pcstemp = new Ps_Calc();
            pcstemp.Forecast = Convert.ToInt32(type);
            pcstemp.ForecastID = ForecastID;
            IList<Ps_Calc> list2 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcstemp);
            for (int i = 1; i <= years; i++)
            {
                bool bl = false;
                foreach (Ps_Calc pcs in list2)
                {
                    if ((startyear + i) == pcs.Year)
                    {
                        bl = true;
                    }
                }
                if (!bl)
                {
                   
                    Ps_Calc pcss = new Ps_Calc();
                    pcss.ID = Guid.NewGuid().ToString();
                    pcss.Forecast = Convert.ToInt32(type);
                    pcss.ForecastID = ForecastID;
                    pcss.Year =startyear + i ;
                    pcss.Value1 = 0;
                    pcss.Value2 = 0;


                    Services.BaseService.Create<Ps_Calc>(pcss);
                    list2.Add(pcss);
                }
            }


            for (int i = 0; i < years; i++)
            {
                foreach (Ps_Calc pcss in list2)
                {
                    if ((startyear + i+1 )== pcss.Year)
                    {
                        s1 = s1 * (1 + pcss.Value1 * pcss.Value2);
                        rt[i] = s1;
                    }
                }
                //rt[i] = baseValue * Math.Pow(1 + springCoefficient * AverageIncreasing(historyValue), i + 1);
            }

            return rt;
        }


        ///// <summary>
        ///// 利用专家指定法计算预测数据
        ///// </summary>
        ///// <param name="baseValue">历史数据</param>
        ///// <param name="baseValue">基年数据</param>
        ///// <param name="springCoefficient">预测采用的弹性系数</param>
        ///// <param name="years">要预测的年数</param>
        ///// <returns>预测结果数组</returns>
        //public static double[] ProfessionalLV(double baseValue, int years)
        //{
        //    double[] rt = new double[years];

        //    double s1 = baseValue; //每次的增长
        //    double s2 = 0; //增长的合计

        //    string str = " Flag='" + type + "' and Type='专家指定'";
        //    IList<PSP_Calc_Spring> list2 = Services.BaseService.GetList<PSP_Calc_Spring>("SelectPSP_Calc_SpringByWhere", str);

        //    for (int i = 1; i <= years; i++)
        //    {
        //        bool bl = false;
        //        foreach (PSP_Calc_Spring pcs in list2)
        //        {
        //            if ((startyear + i).ToString() == pcs.Name)
        //            {
        //                bl = true;
        //            }
        //        }
        //        if (!bl)
        //        {
        //            PSP_Calc_Spring pcss = new PSP_Calc_Spring();
        //            pcss.ID = Guid.NewGuid().ToString();
        //            pcss.Name = (startyear + i).ToString();
        //            pcss.Value1 = 0;
        //            pcss.Value2 = 0;
        //            pcss.Flag = type;
        //            pcss.Type = "专家指定";
        //            Services.BaseService.Create<PSP_Calc_Spring>(pcss);
        //            list2.Add(pcss);
        //        }
        //    }


        //    for (int i = 0; i < years; i++)
        //    {
        //        foreach (PSP_Calc_Spring pcs in list2)
        //        {
        //            if ((startyear + i + 1).ToString() == pcs.Name)
        //            {
        //                s1 = s1 * (1 + pcs.Value2);
        //                rt[i] = s1;
        //            }
        //        }
        //        //rt[i] = baseValue * Math.Pow(1 + springCoefficient * AverageIncreasing(historyValue), i + 1);
        //    }

        //    return rt;
        //}





        /// <summary>
        /// 计算单耗
        /// </summary>
        /// <param name="power">用电量</param>
        /// <param name="GNP">国民生产总值</param>
        /// <returns>单耗</returns>
        public static double UnitExpend(double power, double GNP)
        {
            return power / GNP;
        }

        /// <summary>
        /// 用年增长率计算预测数据
        /// </summary>
        /// <param name="baseValue">基年数据</param>
        /// <param name="averageIncreasing">年均增长率</param>
        /// <param name="years">需要预测的年数</param>
        /// <returns>预测数据数组</returns>
        public static double[] ArverageIncreasingMethod(double baseValue, double averageIncreasing, int years)
        {
            double[] rt = new double[years];
            for (int i = 0; i < years;i++ )
            {
                rt[i] = baseValue * Math.Pow(1 + averageIncreasing, i+1);
            }
            return rt;
        }
        /// <summary>
        /// 用年增长率计算预测数据
        /// </summary>
        /// <param name="baseValue">基年数据</param>
        /// <param name="averageIncreasing">年均增长率</param>
        /// <param name="years">需要预测的年数</param>
        /// <returns>预测数据数组</returns>
        public static double[] ArverageIncreasingMethod(double[] historyValue, double baseValue, int years)
        {
            bool bz=false;
            foreach (double d in historyValue)
            {
                if (d == 0)
                    bz = true;
            }
            double[] rt = new double[years];
            if (!bz)
            {
                double averageIncreasing = AverageIncreasing(historyValue);
                for (int i = 0; i < years; i++)
                {
                    rt[i] = baseValue * Math.Pow(1 + averageIncreasing, i + 1);
                }
            }
            //else
            //{
            //    for (int i = 0; i < years; i++)
            //    {
            //        rt[i] = 0;
            //    }
            //}
            return rt;
        }





        ///// <summary>
        ///// 用年增长率计算预测数据
        ///// </summary>
        ///// <param name="baseValue">基年数据</param>
        ///// <param name="averageIncreasing">年均增长率</param>
        ///// <param name="years">需要预测的年数</param>
        ///// <returns>预测数据数组</returns>
        //public static double[] ArverageIncreasingMethod(double[] historyValue, double baseValue, int years,int i)
        //{
        //    bool bz = false;
        //    foreach (double d in historyValue)
        //    {
        //        if (d == 0)
        //            bz = true;
        //    }
        //    double[] rt = new double[years];
        //    if (!bz)
        //    {
        //        double value = 0;
        //        PSP_Calc pc1 = new PSP_Calc();
        //        IList<PSP_Calc> list1 = Services.BaseService.GetList<PSP_Calc>("SelectPSP_CalcByFlag1", type);
        //        if (list1.Count ==0)
        //        {
        //            pc1.ID = Guid.NewGuid().ToString();
        //            pc1.Flag = type;
        //            pc1.Col1 = "1";
        //            pc1.Value1 = 0.0000001;
        //            pc1.Value2 = 0.0000001;
        //            pc1.Value3 = 0.0000001;
        //            pc1.Value4 = 0.0000001;
        //            pc1.Value5 = 0.0000001;
        //            pc1.Value6 = 0.0000001;
        //            pc1.Value7 = 0.0000001; 
        //            pc1.Value8 = 0.0000001;
        //            pc1.Value9 = 0.0000001;
        //            pc1.Value10 = 0.0000001;
        //            pc1.Value11 = 0.0000001;
        //            pc1.Value12 = 0.0000001;
        //            pc1.Value13 = 0.0000001;
        //            pc1.Value14 = 0.0000001;
        //            pc1.Value15 = 0.0000001;

        //            value = AverageIncreasing(historyValue);
        //            pc1.GetType().GetProperty("Value" + i.ToString()).SetValue(pc1, value, null);
        //            Services.BaseService.Create<PSP_Calc>(pc1);
        //        }
        //        else
        //        {
        //            pc1 = list1[0];
        //            try
        //            {

        //                value = (double)pc1.GetType().GetProperty("Value" + i.ToString()).GetValue(pc1, null);
        //                if (value == 0.0000001)
        //                {
        //                    value = AverageIncreasing(historyValue);
        //                    pc1.GetType().GetProperty("Value" + i.ToString()).SetValue(pc1, value, null);
        //                    Services.BaseService.Update<PSP_Calc>(pc1);
        //                }


        //            }
        //            catch
        //            {
        //                value=AverageIncreasing(historyValue);
        //                pc1.GetType().GetProperty("Value" + i.ToString()).SetValue(pc1, value, null);
        //                Services.BaseService.Update<PSP_Calc>(pc1);
        //            }
        //        }






        //        double averageIncreasing = value;
        //        for (int i1 = 0; i1 < years; i1++)
        //        {
        //            rt[i1] = baseValue * Math.Pow(1 + averageIncreasing, i1 + 1);
        //        }
        //    }
        //    return rt;
        //}




        /// <summary>
        /// 利用弹性系数法计算预测数据
        /// </summary>
        /// <param name="baseValue">基年数据</param>
        /// <param name="springCoefficient">预测采用的弹性系数</param>
        /// <param name="GNPIncreasing">预测期预计国民生产总值年均增长率，根据国家经济发展战略规划确定</param>
        /// <param name="years">要预测的年数</param>
        /// <returns>预测结果数组</returns>
        public static double[] SpringCoefficientMethod(double baseValue, double springCoefficient, double GNPIncreasing, int years)
        {
            double[] rt = new double[years];

            for (int i = 0; i < years; i++ )
            {
                rt[i] = baseValue * Math.Pow(1 + springCoefficient * GNPIncreasing, i + 1);
            }
            
            return rt;
        }

        /// <summary>
        /// 单耗法计算预测年需电量
        /// </summary>
        /// <param name="unitExpend">预测年单耗数组，包含预测年各年的单耗值，与GNP维数相等</param>
        /// <param name="GNP">预测年国民生产总值，包含预测年各年国民生产总值，与unitExpend维数相等</param>
        /// <returns>预测年需电量数组</returns>
        public static double[] UnitExpendMethod(double[] unitExpend, double[] GNP)
        {
            if(unitExpend.Length != GNP.Length)
            {
                throw new Exception("单耗与国民生产总值维数必须一至！");
            }

            double[] rt = new double[unitExpend.Length];

            for(int i=0; i<unitExpend.Length; i++)
            {
                rt[i] = unitExpend[i] * GNP[i];
            }

            return rt;
        }

        ///// <summary>
        ///// 二次移动平均法计算预测年数据，默认周期为3年
        ///// </summary>
        ///// <param name="historyValue">历史数据，维数不能小于周期的2倍减1</param>
        ///// <param name="years">要预测的年数</param>
        ///// <returns>预测数据的数组</returns>
        //public static double[] TwiceMoveArverageMethod(double[] historyValue, int years)
        //{
        //    PSP_Calc pc=new PSP_Calc();
        //    IList<PSP_Calc> list = Services.BaseService.GetList<PSP_Calc>("SelectPSP_CalcByFlag", type);
        //    if (list.Count == 0)
        //    {
        //        pc.ID = Guid.NewGuid().ToString();
        //        pc.Flag = type;
        //        pc.Value1 = 3;
        //        pc.Value2 = 0.4;

        //        Services.BaseService.Create<PSP_Calc>(pc);
        //    }
        //    else
        //        pc = list[0];

        //    int year = Convert.ToInt32(pc.Value1);
        //    return TwiceMoveArverageMethod(historyValue, years, year);
        //}

        /// <summary>
        /// 二次移动平均法计算预测年数据
        /// </summary>
        /// <param name="historyValue">历史数据，维数不能小于周期的2倍减1</param>
        /// <param name="years">要预测的年数</param>
        /// <param name="period">周期</param>
        /// <returns>预测数据的数组</returns>
        public static double[] TwiceMoveArverageMethod(double[] historyValue, int years, int period)
        {
            if(historyValue.Length < period * 2 -1)
            {
                throw new Exception("历史数据维数不能小于周期的2倍减1！");
            }

            double[] M = new double[historyValue.Length + years];
            double[] M2 = new double[historyValue.Length + years];
            double[] x = new double[historyValue.Length + years];

            for(int i=0; i<historyValue.Length; i++)
            {
                x[i] = historyValue[i];
            }

            for(int i=period - 1; i< historyValue.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < period; j++ )
                {
                    sum += x[i - j];
                }

                M[i] = sum / period;
            }

            for (int i = 2 * period - 2; i < historyValue.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < period; j++)
                {
                    sum += M[i - j];
                }

                M2[i] = sum / period;
            }

            double[] rt = new double[years];

            for(int i=historyValue.Length; i<historyValue.Length +years; i++)
            {
                rt[i - historyValue.Length] = x[i] = 3 * M[i - 1] - 2 * M2[i - 1];
                double sum = 0;
                for (int j = 0; j < period; j++)
                {
                    sum += x[i - j];
                }

                M[i] = sum / period;

                sum = 0;
                for (int j = 0; j < period; j++)
                {
                    sum += M[i - j];
                }
                M2[i] = sum / period;
            }

            return rt;
        }

        /// <summary>
        /// 一元线性回归法计算预测数据，通过最小二乘法计算系数a,b，用公式y=a+bt进行预测，以年次为相关数据
        /// </summary>
        /// <param name="historyValue">历史数据数组</param>
        /// <param name="years">要预测的年数</param>
        /// <returns>预测数据数组</returns>
        public static double[] LinearlyRegressMethod(double[] historyValue, int years)
        {
            if (historyValue.Length < 2)
            {
                throw new Exception("历史数据需要两组以上");
            }

            double[] correlativeData = new double[historyValue.Length + years];
            for(int i=0; i<correlativeData.Length; i++)
            {
                correlativeData[i] = i;
            }

            return LinearlyRegressMethod(historyValue, correlativeData);
        }


        /// <summary>
        /// 一元线性回归法计算预测数据，通过最小二乘法计算系数a,b，用公式y=a+bt进行预测
        /// </summary>
        /// <param name="historyValue">历史数据数组</param>
        /// <param name="correlativeData">相关数据数组，里面不但包含相关数据的历史数据，也包含相关数据的预测数据</param>
        /// <returns>预测数据数组</returns>
        public static double[] LinearlyRegressMethod(double[] historyValue, double[] correlativeData)
        {
            if (historyValue.Length < 2)
            {
                throw new Exception("历史数据需要两组以上！");
            }

            if(correlativeData.Length <= historyValue.Length)
            {
                throw new Exception("相关数据应多于历史数据！");
            }

            double[] rt = new double[correlativeData.Length - historyValue.Length];

            double averageX = 0;
            double averageY = 0;

            double eX2 = 0;
            double eXY = 0;

            for (int i = 0; i < historyValue.Length; i++)
            {
                eX2 += correlativeData[i] * correlativeData[i];
                eXY += correlativeData[i] * historyValue[i];
                averageX += correlativeData[i];
                averageY += historyValue[i];
            }

            averageX /= historyValue.Length;
            averageY /= historyValue.Length;

            double b = (eXY - historyValue.Length * averageX * averageY) / (eX2 - historyValue.Length * averageX * averageX);
            double a = averageY - b * averageX;

            for (int i = 0; i < correlativeData.Length - historyValue.Length; i++)
            {
                rt[i] = a + b * correlativeData[i + historyValue.Length];
            }

            return rt;
        }

            /// <summary>
        /// 灰色模型法的a、u
        /// </summary>
        /// <param name="historyValue">历史数据</param>
        /// <param name="years">预测年数</param>
        /// <returns>灰色模型法的a、u数组</returns>
        public static double[,] GrayMethodAU(double[] historyValue, int years)
        {
            int nLenHistory = historyValue.Length;
            int nBound = nLenHistory - 1;
         

            //GM(1,1)的累加值数组
            years++;
            double[] x1 = new double[nLenHistory + years];

            x1[0] = historyValue[0];

            //累加值
            for (int i = 1; i < nLenHistory; i++)
            {
                x1[i] = x1[i - 1] + historyValue[i];
            }

            //矩阵B
            double[,] B = new double[nBound, 2];

            //矩阵B的转秩矩阵
            double[,] BT = new double[2, nBound];

            //向量Y
            double[,] Y = new double[nBound, 1];

            //计算矩阵B，为向量Y及转秩矩阵BT赋值 
            for (int i = 0; i < nBound; i++)
            {
                BT[0, i] = B[i, 0] = -(x1[i] + x1[i + 1]) / 2;
                BT[1, i] = B[i, 1] = 1;
                Y[i, 0] = historyValue[i + 1];
            }

            //BT*B的结果
            double[,] BTB = MultiplyMatrix(BT, B);

            //BTB的逆矩阵
            double[,] BTBInverse = InverseMatrix(BTB);

            double[,] U = MultiplyMatrix(MultiplyMatrix(BTBInverse, BT), Y);

           return U;
            

        }
        /// <summary>
        /// 灰色模型法计算规划年数据
        /// </summary>
        /// <param name="historyValue">历史数据</param>
        /// <param name="years">预测年数</param>
        /// <returns>包含预测数据的数组，数组长度为预测年数</returns>
        public static double[] GrayMethod(double[] historyValue, int years)
        {
            int nLenHistory = historyValue.Length;
            int nBound = nLenHistory - 1;
            double[] rt = new double[years];

            //GM(1,1)的累加值数组
            years++;
            double[] x1 = new double[nLenHistory + years];

            x1[0] = historyValue[0];

            //累加值
            for (int i = 1; i < nLenHistory; i++)
            {
                x1[i] = x1[i - 1] + historyValue[i];
            }

            //矩阵B
            double[,] B = new double[nBound, 2];

            //矩阵B的转秩矩阵
            double[,] BT = new double[2, nBound];

            //向量Y
            double[,] Y = new double[nBound, 1];

            //计算矩阵B，为向量Y及转秩矩阵BT赋值 
            for (int i = 0; i < nBound; i++)
            {
                BT[0, i] = B[i, 0] = -(x1[i] + x1[i + 1]) / 2;
                BT[1, i] = B[i, 1] = 1;
                Y[i, 0] = historyValue[i + 1];
            }

            //BT*B的结果
            double[,] BTB = MultiplyMatrix(BT, B);

            //BTB的逆矩阵
            double[,] BTBInverse = InverseMatrix(BTB);

            double[,] U = MultiplyMatrix(MultiplyMatrix(BTBInverse, BT), Y);

            double a = U[0, 0];
            double u = U[1, 0];

            double ua = u / a;
            double dua = historyValue[0] - ua;


            for (int i = nLenHistory; i < nLenHistory + years - 1; i++)
            {
                x1[i] = dua * Math.Pow(Math.E, -a * (i)) + ua;
                rt[i - nLenHistory] = x1[i] - x1[i - 1];
            }
            return rt;
        }


        /// <summary>
        /// 指数增长法计算预测数据
        /// </summary>
        /// <param name="historyValue">历史数据</param>
        /// <param name="years">预测年数</param>
        /// <returns>包含预测数据的数组，数组长度为预测年数</returns>
        public static double[] IndexIncreaseMethod(double[] historyValue, int years)
        {
            int length = historyValue.Length;
            if (length < 2)
            {
                throw new Exception("指数增长法历史数据需要两条以上！");
            }

            int start = 0;
            if(length % 2 == 1)//如果历史数据为奇数，则从第二个数据开始，以形成偶数
            {
                length--;
                start = 1;
            }

            double[] yt = new double[length];
            for(int i=0; i<length; i++)
            {
                yt[i] = historyValue[i + start];
            }

            int[] t = new int[length + years];

            for (int i = 0; i < length + years; i++)
            {
                t[i] = 2 * i - length + 1;
            }

            //历史数据求以10为底的对数
            double[] LGYT = new double[length];

            //LGYT求和
            double eLGYT = 0;

            //yt求和
            double eYT = 0;

            //t平方求和
            double eT2 = 0;

            for (int i = 0; i < length; i++)
            {
                LGYT[i] = Math.Log10(yt[i]);
                eLGYT += LGYT[i];
                eYT += LGYT[i] * t[i];
                eT2 += t[i] * t[i];
            }

            double b = eYT / eT2;
            double a = Math.Pow(10.0, eLGYT / length);


            double[] rt = new double[years];
            for(int i=0; i<years; i++)
            {
                rt[i] = a * Math.Pow(10.0, b * t[i + length]);
            }

            return rt;
        }

        ///// <summary>
        ///// 二次指数平滑法计算预测值，以平滑常数0.25进行计算
        ///// </summary>
        ///// <param name="historyValue">历史数据</param>
        ///// <param name="years">预测年数</param>
        ///// <returns>包含预测年数据的数组</returns>
        //public static double[] IndexSmoothMethod(double[] historyValue, int years)
        //{
            

        //    PSP_Calc pc = new PSP_Calc();
        //    IList<PSP_Calc> list = Services.BaseService.GetList<PSP_Calc>("SelectPSP_CalcByFlag", type);
        //    if (list.Count == 0)
        //    {
        //        pc.ID = Guid.NewGuid().ToString();
        //        pc.Flag = type;
        //        pc.Value1 = 3;
        //        pc.Value2 = 0.4;

        //        Services.BaseService.Create<PSP_Calc>(pc);
        //    }
        //    else
        //        pc = list[0];

        //    double a = Convert.ToDouble(pc.Value2);


        //    //if (years > 5)
        //    //    a = 0.4;
        //    //if (years > 10)
        //    //    a = 0.6;
        //    return IndexSmoothMethod(historyValue, years, a);
        //}


        /// <summary>
        /// 一次指数平滑法计算预测值
        /// </summary>
        /// <param name="historyValue">历史数据</param>
        /// <param name="years">预测年数</param>
        /// <param name="a">平滑常数，此值在0-1之间，越大越是体现近期作用，一般取0.1-0.5之间</param>
        /// <returns>包含预测年数据的数组</returns>
        public static double[] IndexOneSmoothMethod1(double[] historyValue, int years, double a)
        {
            int length = historyValue.Length;
            if (length < 2)
            {
                throw new Exception("二次指数平滑法历史数据需要两条以上！");
            }

            //if (a <= 0 || a >= 1)
            //{
            //    throw new Exception("一次指数平滑法平滑常数取值应在(0, 1)之间！");
            //}

            double[] S = new double[length + years];
            double[] rt = new double[years];
            double[] Y = Calculator.One(historyValue, years);
            double[] x = new double[length + years];

            for (int i = 0; i < length; i++)
            {
                S[length - 1] += historyValue[i];
            }
            S[0] = S[length - 1] / length;
            x[0] = historyValue[0];

            for (int i = 1; i < length; i++)
            {
                x[i] = historyValue[i];
                S[i] = a * x[i - 1] + (1 - a) * S[i - 1];
            }


            for (int i = length; i < length+years; i++)
            {

                S[i] = a * Y[i-length] + (1 - a) * S[i - 1];
                rt[i-length] = S[i];
            }
            return rt;
        }





        /// <summary>
        /// 一次指数平滑法计算预测值
        /// </summary>
        /// <param name="historyValue">历史数据</param>
        /// <param name="years">预测年数</param>
        /// <param name="a">平滑常数，此值在0-1之间，越大越是体现近期作用，一般取0.1-0.5之间</param>
        /// <returns>包含预测年数据的数组</returns>
        public static double[] IndexOneSmoothMethod(double[] historyValue, int years, double a)
        {
            int length = historyValue.Length;
            if (length < 2)
            {
                throw new Exception("二次指数平滑法历史数据需要两条以上！");
            }

            if (a <= 0 || a >= 1)
            {
                throw new Exception("一次指数平滑法平滑常数取值应在(0, 1)之间！");
            }

            double[] rt = new double[years];
            double[] x = new double[length + years];
            double b0 = historyValue[1] - historyValue[0];
            double[] S = new double[length + years];
            //S[0] = historyValue[0] + (1 - a) * b0 / a;

            for (int i = 0; i < length; i++)
            {
                S[0]+=historyValue[i];
            }
            x[0] = historyValue[0];
            S[0] = S[0] / length;

            for (int i = 1; i < length; i++)
            {
                x[i] = historyValue[i];
                S[i] = a * x[i-1] + (1 - a) * S[i - 1];
            }

            double temp = a / (1 - a);

            for (int i = 0; i < years; i++)
            {
                int index = length + i - 1;
                //x[length + i] = x[length-1]+b1*(i+1);
                
                //x[i + length] = S[index];
                S[length + i] = a * x[index] + (1 - a) * S[index];
                x[length + i] = a * x[index] + (1 - a) * S[index + 1];
                rt[i] = x[i + length];
                //rt[i] = S[length + i];
            }

            return rt;
        }


        /// <summary>
        /// 二次指数平滑法计算预测值
        /// </summary>
        /// <param name="historyValue">历史数据</param>
        /// <param name="years">预测年数</param>
        /// <param name="a">平滑常数，此值在0-1之间，越大越是体现近期作用，一般取0.1-0.5之间</param>
        /// <returns>包含预测年数据的数组</returns>
        public static double[] IndexSmoothMethod(double[] historyValue, int years, double a)
        {
            int length = historyValue.Length;
            if (length < 2)
            {
                throw new Exception("二次指数平滑法历史数据需要两条以上！");
            }

            //if(a<=0 || a>=1)
            //{
            //    throw new Exception("二次指数平滑法平滑常数取值应在(0, 1)之间！");
            //}

            double[] rt = new double[years];

            double[] x = new double[length + years];

            double b0 = historyValue[1] - historyValue[0];

            double[] S = new double[length + years];
            double[] S2 = new double[length + years];

            S[0] = historyValue[0] - (1 - a) * b0 / a;
            S2[0] = historyValue[0] - 2 * (1 - a) * b0 / a;

            for (int i = 1; i < length; i++ )
            {
                x[i] = historyValue[i];
                S[i] = a * x[i] + (1 - a) * S[i - 1];
                S2[i] = a * S[i] + (1 - a) * S2[i - 1];
            }

            double temp = a / (1 - a);

            for (int i = 0; i < years; i++ )
            {
                int index = length + i - 1;
                x[i + length] = (2 + temp) * S[index] - (1 + temp) * S2[index];
                S[length + i] = a * x[length + i] + (1 - a) * S[index];
                S2[length + i] = a * S[length + i] + (1 - a) * S2[index];

                rt[i] = x[i+length];
            }

            return rt;
        }

        /// <summary>
        /// 线性趋势y=a+bt法
        /// </summary>
        /// <param name="historyValue"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static double[] Line(double[] historyValue, int years)
        {
            int nHistory = historyValue.Length;
            int nTotalYears = nHistory + years;

            double ety = 0;
            double et = 0;
            double ey = 0;
            double et2 = 0;
            for (int i = 0; i < nHistory; i++)
            {
                ety += historyValue[i] * i;
                et += i;
                et2 += i * i;
                ey += historyValue[i];
            }

            double B = (ety * nHistory - et * ey) / (et2 * nHistory - et * et);
            double A = (ey - B * et) / nHistory;

            double[] rt = new double[years];

            for (int i = nHistory; i < nTotalYears; i++)
            {
                rt[i - nHistory] = A + B * i;
            }

            return rt;
        }




        /// <summary>
        /// 线性趋势y=a+bt法
        /// </summary>
        /// <param name="historyValue"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static double[] One(double[] historyValue, int years)
        {
            int nHistory = historyValue.Length;
            int nTotalYears = nHistory + years;
            //向量Y
            double[,] Y = new double[2, 1];
            double[,] X = new double[2, 2];

            for (int i = 0; i < nHistory; i++)
            {
                Y[0, 0] += historyValue[i];
                Y[1, 0] += (i + 1) * historyValue[i];

                X[0, 0] = nHistory;
                X[0, 1] += i + 1;

                X[1, 0] += i + 1;
                X[1, 1] += (i + 1) * (i + 1);

            }

            //X的逆矩阵
            double[,] BTBX = InverseMatrix(X);
            double[,] U = MultiplyMatrix(BTBX, Y);

            double A = U[0, 0];
            double B = U[1, 0];



            double[] rt = new double[years];
            for (int i = nHistory; i < nTotalYears; i++)
            {
                rt[i - nHistory] = A + B * (i + 1);
            }

            return rt;
        }





        /// <summary>
        /// 抛物线趋势y=a+bt+ctt法
        /// </summary>
        /// <param name="historyValue"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static double[] Second(double[] historyValue, int years)
        {
            int nHistory = historyValue.Length;
            int nTotalYears = nHistory + years;
            //向量Y
            double[,] Y = new double[3, 1];
            double[,] X = new double[3, 3];

            for (int i = 0; i < nHistory; i++)
            {
                Y[0,0] += historyValue[i];
                Y[1, 0] += (i + 1) * historyValue[i];
                Y[2, 0] += (i + 1) * (i + 1) * historyValue[i];

                X[0, 0] = nHistory;
                X[0, 1] += i + 1;
                X[0, 2] += (i + 1) * (i + 1);
                X[1, 0] += i + 1;
                X[1, 1] += (i + 1) * (i + 1);
                X[1, 2] += (i + 1) * (i + 1) * (i + 1);
                X[2, 0] += (i + 1) * (i + 1);
                X[2, 1] += (i + 1) * (i + 1) * (i + 1);
                X[2, 2] += (i + 1) * (i + 1) * (i + 1) * (i + 1);
            }

            //X的逆矩阵
            double[,] BTBX = InverseMatrix(X);
            double[,] U = MultiplyMatrix(BTBX, Y);

            double A = U[0, 0];
            double B = U[1, 0];
            double C = U[2, 0];



            double[] rt = new double[years];
            for (int i = nHistory; i < nTotalYears; i++)
            {
                rt[i - nHistory] = A + B * (i + 1) + C * (i + 1) * (i + 1);
            }

            return rt;
        }








        /// <summary>
        /// 三阶趋势y=a+bt+ctt+dttt法
        /// </summary>
        /// <param name="historyValue"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static double[] Three(double[] historyValue, int years)
        {
            int nHistory = historyValue.Length;
            int nTotalYears = nHistory + years;
            //向量Y
            double[,] Y = new double[4, 1];
            double[,] X = new double[4, 4];

            for (int i = 0; i < nHistory; i++)
            {
                Y[0, 0] += historyValue[i];
                Y[1, 0] += (i + 1) * historyValue[i];
                Y[2, 0] += (i + 1) * (i + 1) * historyValue[i];
                Y[3, 0] += (i + 1) * (i + 1) * (i + 1) * historyValue[i];

                X[0, 0] = nHistory;
                X[0, 1] += i + 1;
                X[0, 2] += (i + 1) * (i + 1);
                X[0, 3] += (i + 1) * (i + 1) * (i + 1);

                X[1, 0] += i + 1;
                X[1, 1] += (i + 1) * (i + 1);
                X[1, 2] += (i + 1) * (i + 1) * (i + 1);
                X[1, 3] += (i + 1) * (i + 1) * (i + 1) * (i + 1);

                X[2, 0] += (i + 1) * (i + 1);
                X[2, 1] += (i + 1) * (i + 1) * (i + 1);
                X[2, 2] += (i + 1) * (i + 1) * (i + 1) * (i + 1);
                X[2, 3] += (i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1);

                X[3, 0] += (i + 1) * (i + 1) * (i + 1);
                X[3, 1] += (i + 1) * (i + 1) * (i + 1) * (i + 1);
                X[3, 2] += (i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1);
                X[3, 3] += (i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1);
            }

            //X的逆矩阵
            double[,] BTBX = InverseMatrix(X);
            double[,] U = MultiplyMatrix(BTBX, Y);

            double A = U[0, 0];
            double B = U[1, 0];
            double C = U[2, 0];
            double D = U[3, 0];


            double[] rt = new double[years];
            for (int i = nHistory; i < nTotalYears; i++)
            {
                rt[i - nHistory] = A + B * (i + 1) + C * (i + 1) * (i + 1) + D * (i + 1) * (i + 1) * (i + 1);
            }

            return rt;
        }





        /// <summary>
        /// 指数趋势法
        /// </summary>
        /// <param name="historyValue"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static double[] Index(double[] historyValue, int years)
        {
            int nHistory = historyValue.Length;
            int nTotalYears = nHistory + years;
            //向量Y
            double[,] Y = new double[2, 1];
            double[,] X = new double[2, 2];

            for (int i = 0; i < nHistory; i++)
            {
                Y[0, 0] += Math.Log(historyValue[i]);
                Y[1, 0] += (i + 1) * Math.Log(historyValue[i]);

                X[0, 0] = nHistory;
                X[0, 1] += i + 1;

                X[1, 0] += i + 1;
                X[1, 1] += (i + 1) * (i + 1);

            }

            //X的逆矩阵
            double[,] BTBX = InverseMatrix(X);
            double[,] U = MultiplyMatrix(BTBX, Y);

            double A = U[0, 0];
            double B = U[1, 0];
            A = Math.Pow(Math.E, A);
            B = Math.Pow(Math.E, B);

            double[] rt = new double[years];
            for (int i = nHistory; i < nTotalYears; i++)
            {
                rt[i - nHistory] = A * (Math.Pow(B, i + 1));
            }

            return rt;
        }





        /// <summary>
        /// 几何曲线趋势法
        /// </summary>
        /// <param name="historyValue"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static double[] LOG(double[] historyValue, int years)
        {
            int nHistory = historyValue.Length;
            int nTotalYears = nHistory + years;
            //向量Y
            double[,] Y = new double[2, 1];
            double[,] X = new double[2, 2];

            for (int i = 0; i < nHistory; i++)
            {
                Y[0, 0] += Math.Log(historyValue[i]);
                Y[1, 0] += Math.Log(i + 1) * Math.Log(historyValue[i]);

                X[0, 0] = nHistory;
                X[0, 1] += Math.Log(i + 1);

                X[1, 0] += Math.Log(i + 1);
             //   X[1, 1] += Math.Log((i + 1) * (i + 1));
                X[1, 1] += Math.Log((i + 1)) * Math.Log((i + 1));
            }

            //X的逆矩阵
            double[,] BTBX = InverseMatrix(X);
            double[,] U = MultiplyMatrix(BTBX, Y);

            double A = U[0, 0];
            double B = U[1, 0];
            A = Math.Pow(Math.E, A);
            

            double[] rt = new double[years];
            for (int i = nHistory; i < nTotalYears; i++)
            {
                rt[i - nHistory] = A * (Math.Pow(i + 1, B));
            }

            return rt;
        }














        /// <summary>
        /// 矩阵相乘
        /// </summary>
        /// <param name="matrix1">矩阵一</param>
        /// <param name="matrix2">矩阵二</param>
        /// <returns>相乘后的矩阵</returns>
        public static double[,] MultiplyMatrix(double[,] matrix1, double[,] matrix2)
        {
            
            int rows = matrix1.GetLength(0);
            int colsMatrix1 = matrix1.GetLength(1);
            int rowsMatrix2 = matrix2.GetLength(0);
            int cols = matrix2.GetLength(1);

            if(colsMatrix1 != rowsMatrix2)
            {
                throw new Exception("两个相乘矩阵维数不对应，不能相乘！");
            }
           
            double[,] rt = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for(int j=0; j<cols; j++)
                {
                    rt[i, j] = 0;
                    for (int k = 0; k < colsMatrix1; k++)
                    {
                        rt[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            
            return rt;
        }

        /// <summary>
        /// 计算矩阵的逆
        /// </summary>
        /// <param name="matrix">需要计算逆的矩阵</param>
        /// <returns>逆矩阵</returns>
        public static double[,] InverseMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            if(rows != matrix.GetLength(1))
            {
                throw new Exception("必须是单位矩阵才能求逆！");
            }

            double matrixValue = MatrixValue(matrix);

            if (matrixValue == 0) return null;

            double[,] reverseMatrix = new double[rows, 2 * rows];

            double x, c;

            //初始化逆矩阵 
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 2 * rows; j++)
                {
                    if (j < rows)
                    {
                        reverseMatrix[i, j] = matrix[i, j];
                    }
                    else
                    {
                        reverseMatrix[i, j] = 0;
                    }
                }
                reverseMatrix[i, rows + i] = 1;
            }

            for (int i = 0, j = 0; i < rows && j < rows; i++, j++)
            {
                if (reverseMatrix[i, j] == 0)
                {
                    int m = i;
                    for (; matrix[m, j] == 0; m++) ;
                    if (m == rows)
                    {
                        return null;
                    }
                    else
                    {
                        for (int n = j; n < 2 * rows; n++)
                        {
                            reverseMatrix[i, n] += reverseMatrix[m, n];
                        }

                    }
                }

                x = reverseMatrix[i, j];
                if (x != 1)
                {
                    for (int n = j; n < 2 * rows; n++)
                    {
                        if (reverseMatrix[i, n] != 0)
                        {
                            reverseMatrix[i, n] /= x;
                        }
                    }
                }

                for (int s = rows - 1; s > i; s--)
                {
                    x = reverseMatrix[s, j];
                    for (int t = j; t < 2 * rows; t++)
                    {
                        reverseMatrix[s, t] -= (reverseMatrix[i, t] * x);
                    }

                }

            }

            for (int i = rows - 2; i >= 0; i--)
            {
                for (int j = i + 1; j < rows; j++)
                {
                    if (reverseMatrix[i, j] != 0)
                    {
                        c = reverseMatrix[i, j];
                        for (int n = j; n < 2 * rows; n++)
                        {
                            reverseMatrix[i, n] -= (c * reverseMatrix[j, n]);
                        }
                    }
                }
            }

            double[,] dReturn = new double[rows, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    dReturn[i, j] = reverseMatrix[i, j + rows];
                }
            }

            return dReturn;
        }

        /// <summary>
        /// 计算行列式值
        /// </summary>
        /// <param name="matrixList">欲求值的矩阵</param>
        /// <returns>行列式值</returns>
        public static double MatrixValue(double[,] matrixList)
        {
            int rows = matrixList.GetLength(0);
            if (rows != matrixList.GetLength(1))
            {
                throw new Exception("必须是单位矩阵才能求行列式值！");
            }

            double[,] dMatrix = new double[rows, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    dMatrix[i, j] = matrixList[i, j];
                }
            }

            double c, x;

            int k = 1;
            double sn = 1;


            for (int i = 0, j = 0; i < rows && j < rows; i++, j++)
            {
                if (dMatrix[i, j] == 0)
                {
                    int m = i;
                    for (; m < rows && dMatrix[m, j] == 0; m++) ;
                    if (m == rows)
                    {
                        return 0;
                    }
                    else
                    {
                        for (int n = j; n < rows; n++)
                        {
                            c = dMatrix[i, n];
                            dMatrix[i, n] = dMatrix[m, n];
                            dMatrix[m, n] = c;
                        }
                        k *= (-1);
                    }
                }

                for (int s = rows - 1; s > i; s--)
                {
                    x = dMatrix[s, j];
                    for (int t = j; t < rows; t++)
                    {
                        dMatrix[s, t] -= dMatrix[i, t] * (x / dMatrix[i, j]);
                    }
                }
            }

            

            for (int i = 0; i < rows; i++)
            {
                if (dMatrix[i, i] != 0)
                {
                    sn *= dMatrix[i, i];
                }
                else
                {
                    return 0;
                }
            }
            return k * sn;
        }

        public static void OutputMatrix(double[,] matrix)
        {
            System.Console.WriteLine("*************************** {0} * {1}", matrix.GetLength(0), matrix.GetLength(1));
            for(int i=0; i<matrix.GetLength(0); i++)
            {
                for(int j=0; j<matrix.GetLength(1); j++)
                {
                    System.Console.Write("\t{0}", matrix[i, j]);
                }
                System.Console.WriteLine("");
            }
            System.Console.WriteLine("*************************** End");
        }
        ///// <summary>
        ///// 专家指定法
        ///// </summary>
        ///// <param name="baseValue">基年数据</param>
        ///// <param name="averageIncreasing">年均增长率</param>
        ///// <param name="years">需要预测的年数</param>
        ///// <returns>预测数据数组</returns>
        //public static double[] ProfessionalLV(/*double[] historyValue,*/ double baseValue, int years, int TypeID)
        //{
        //    double[] rt = new double[years];
        //    PSP_PowerProject_ProfessValues psp_profess = new PSP_PowerProject_ProfessValues();
        //    psp_profess.TypeID = TypeID;
        //    psp_profess.Flag2 =Convert.ToInt32(type);
           
        //    try
        //    {
        //        IList<PSP_PowerProject_ProfessValues> listValues = Common.Services.BaseService.GetList<PSP_PowerProject_ProfessValues>("SelectPSP_PowerProject_ProfessValuesByFlag2TypeID", psp_profess);
        //        int i = 0;
        //        int yeartemp = 0;
        //        double baseyeartemp = baseValue;
        //        if (listValues.Count > 0) 
        //            yeartemp = startyear;
        //        else
        //        {
        //            for (int j = 0; j < years; j++)
        //            {
        //                rt[j] = baseyeartemp * Math.Pow((1 + 0.1), 1);
        //                baseyeartemp = rt[j];
        //            }
        //        }
                
        //        foreach (PSP_PowerProject_ProfessValues psp_professtemp in listValues)
        //        {
        //            if (i < years)
        //            {
        //            rt[i] = baseyeartemp * Math.Pow((1 + psp_professtemp.Value), psp_professtemp.Year - yeartemp);
        //            baseyeartemp = rt[i];
        //            yeartemp = psp_professtemp.Year;
        //            i++;
        //            }
                
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Show(ex.Message);
        //    }
           
        //    return rt;
        //}
        
              /// <summary>
        /// 用年增长率计算预测数据
        /// </summary>
        /// <param name="baseValue">基年数据</param>
        /// <param name="averageIncreasing">年均增长率</param>
        /// <param name="years">需要预测的年数</param>
        /// <returns>预测数据数组</returns>
        public static double[] NewDemandArverageIncreasingMethod(double[] historyValue, double baseValue, int years, int i)
        {
            bool bz = false;
            foreach (double d in historyValue)
            {
                if (d == 0)
                    bz = true;
            }
            double[] rt = new double[years];
                //double value = 0;
                for (int i1 = 0; i1 < years; i1++)
                {
                    rt[i1] = baseValue * Math.Pow(1 + AverageIncreasing(historyValue), i1 + 1);
                    //rt[i1] = baseValue * Math.Pow(1 + 1.0, i1 + 1);
                }
            
            return rt;
        }










        #region 带传入参数




        /// <summary>
        /// 线性趋势y=a+bt法
        /// </summary>
        /// <param name="historyValue"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static double[] One(double[] historyValue, int years,ref double M1,ref double M2)
        {
            int nHistory = historyValue.Length;
            int nTotalYears = nHistory + years;
            //向量Y
            double[,] Y = new double[2, 1];
            double[,] X = new double[2, 2];

            for (int i = 0; i < nHistory; i++)
            {
                Y[0, 0] += historyValue[i];
                Y[1, 0] += (i + 1) * historyValue[i];

                X[0, 0] = nHistory;
                X[0, 1] += i + 1;

                X[1, 0] += i + 1;
                X[1, 1] += (i + 1) * (i + 1);

            }
            double[] rt = new double[years];
            //X的逆矩阵
            double[,] BTBX = InverseMatrix(X);
            if (BTBX == null)
            {

                for (int i = nHistory; i < nTotalYears; i++)
                {
                    rt[i - nHistory] = 0;
                }
                M1 = -999999;
                M2 = -999999;
               

                return rt;

            }
            double[,] U = MultiplyMatrix(BTBX, Y);

            double A = U[0, 0];
            double B = U[1, 0];

            M1 = A;
            M2 = B;


           
            for (int i = nHistory; i < nTotalYears; i++)
            {
                rt[i - nHistory] = A + B * (i + 1);
            }

            return rt;
        }

        public static double[] One1(int M, double M1, double M2)
        {
            double[] rt = new double[M];
            for (int i = 0; i < M; i++)
            {
                rt[i] = M1 + M2 * (i + 1);
            }
            return rt;
        }





        /// <summary>
        /// 抛物线趋势y=a+bt+ctt法
        /// </summary>
        /// <param name="historyValue"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static double[] Second(double[] historyValue, int years, ref double M1, ref double M2,ref double M3)
        {
            int nHistory = historyValue.Length;
            int nTotalYears = nHistory + years;
            //向量Y
            double[,] Y = new double[3, 1];
            double[,] X = new double[3, 3];

            for (int i = 0; i < nHistory; i++)
            {
                Y[0, 0] += historyValue[i];
                Y[1, 0] += (i + 1) * historyValue[i];
                Y[2, 0] += (i + 1) * (i + 1) * historyValue[i];

                X[0, 0] = nHistory;
                X[0, 1] += i + 1;
                X[0, 2] += (i + 1) * (i + 1);
                X[1, 0] += i + 1;
                X[1, 1] += (i + 1) * (i + 1);
                X[1, 2] += (i + 1) * (i + 1) * (i + 1);
                X[2, 0] += (i + 1) * (i + 1);
                X[2, 1] += (i + 1) * (i + 1) * (i + 1);
                X[2, 2] += (i + 1) * (i + 1) * (i + 1) * (i + 1);
            }

            double[] rt = new double[years];
            //X的逆矩阵
            double[,] BTBX = InverseMatrix(X);
            if (BTBX == null)
            {

                for (int i = nHistory; i < nTotalYears; i++)
                {
                    rt[i - nHistory] = 0;
                }
                M1 = -999999;
                M2 = -999999;
                M3 = -999999;
              
                return rt;

            }
            double[,] U = MultiplyMatrix(BTBX, Y);

            double A = U[0, 0];
            double B = U[1, 0];
            double C = U[2, 0];

            M1 = A;
            M2 = B;
            M3 = C;


            //double[] rt = new double[years];
            for (int i = nHistory; i < nTotalYears; i++)
            {
                rt[i - nHistory] = A + B * (i + 1) + C * (i + 1) * (i + 1);
            }

            return rt;
        }


        public static double[] Second1(int M, double M1, double M2,double M3)
        {
            double[] rt = new double[M];
            for (int i = 0; i < M; i++)
            {
                rt[i] = M1 + M2 * (i + 1) + M3 * (i + 1) * (i + 1);
            }
            return rt;
        }





        /// <summary>
        /// 三阶趋势y=a+bt+ctt+dttt法
        /// </summary>
        /// <param name="historyValue"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static double[] Three(double[] historyValue, int years, ref double M1, ref double M2, ref double M3,ref double M4)
        {
            int nHistory = historyValue.Length;
            int nTotalYears = nHistory + years;
            //向量Y
            double[,] Y = new double[4, 1];
            double[,] X = new double[4, 4];

            for (int i = 0; i < nHistory; i++)
            {
                Y[0, 0] += historyValue[i];
                Y[1, 0] += (i + 1) * historyValue[i];
                Y[2, 0] += (i + 1) * (i + 1) * historyValue[i];
                Y[3, 0] += (i + 1) * (i + 1) * (i + 1) * historyValue[i];

                X[0, 0] = nHistory;
                X[0, 1] += i + 1;
                X[0, 2] += (i + 1) * (i + 1);
                X[0, 3] += (i + 1) * (i + 1) * (i + 1);

                X[1, 0] += i + 1;
                X[1, 1] += (i + 1) * (i + 1);
                X[1, 2] += (i + 1) * (i + 1) * (i + 1);
                X[1, 3] += (i + 1) * (i + 1) * (i + 1) * (i + 1);

                X[2, 0] += (i + 1) * (i + 1);
                X[2, 1] += (i + 1) * (i + 1) * (i + 1);
                X[2, 2] += (i + 1) * (i + 1) * (i + 1) * (i + 1);
                X[2, 3] += (i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1);

                X[3, 0] += (i + 1) * (i + 1) * (i + 1);
                X[3, 1] += (i + 1) * (i + 1) * (i + 1) * (i + 1);
                X[3, 2] += (i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1);
                X[3, 3] += (i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1) * (i + 1);
            }
            double[] rt = new double[years];
            //X的逆矩阵
            double[,] BTBX = InverseMatrix(X);
               if (BTBX == null)
            {

                for (int i = nHistory; i < nTotalYears; i++)
                {
                    rt[i - nHistory] = 0;
                }
                M1 = -999999;
                M2 = -999999;
                M3 = -999999;
              
                return rt;

            }
            double[,] U = MultiplyMatrix(BTBX, Y);

            double A = U[0, 0];
            double B = U[1, 0];
            double C = U[2, 0];
            double D = U[3, 0];

            M1 = A;
            M2 = B;
            M3 = C;
            M4 = D;

           
            for (int i = nHistory; i < nTotalYears; i++)
            {
                rt[i - nHistory] = A + B * (i + 1) + C * (i + 1) * (i + 1) + D * (i + 1) * (i + 1) * (i + 1);
            }

            return rt;
        }


        public static double[] Three1(int M, double M1, double M2, double M3, double M4)
        {
            double[] rt = new double[M];
            for (int i = 0; i < M; i++)
            {
                rt[i] = M1 + M2 * (i + 1) + M3 * (i + 1) * (i + 1) + M4 * (i + 1) * (i + 1) * (i + 1);
            }
            return rt;
        }


        /// <summary>
        /// 指数趋势法
        /// </summary>
        /// <param name="historyValue"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static double[] Index(double[] historyValue, int years, ref double M1, ref double M2)
        {
            int nHistory = historyValue.Length;
            int nTotalYears = nHistory + years;
            //向量Y
            double[,] Y = new double[2, 1];
            double[,] X = new double[2, 2];

            for (int i = 0; i < nHistory; i++)
            {
                Y[0, 0] += Math.Log(historyValue[i]);
                Y[1, 0] += (i + 1) * Math.Log(historyValue[i]);

                X[0, 0] = nHistory;
                X[0, 1] += i + 1;

                X[1, 0] += i + 1;
                X[1, 1] += (i + 1) * (i + 1);

            }
            double[] rt = new double[years];
            //X的逆矩阵
            double[,] BTBX = InverseMatrix(X);
            if (BTBX == null)
            {

                for (int i = nHistory; i < nTotalYears; i++)
                {
                    rt[i - nHistory] = 0;
                }
                M1 = -999999;
                M2 = -999999;
               

                return rt;

            }
            double[,] U = MultiplyMatrix(BTBX, Y);

            double A = U[0, 0];
            double B = U[1, 0];
            A = Math.Pow(Math.E, A);
            B = Math.Pow(Math.E, B);


            M1 = A;
            M2 = B;

            
            for (int i = nHistory; i < nTotalYears; i++)
            {
                rt[i - nHistory] = A * (Math.Pow(B, i + 1));
            }

            return rt;
        }

        public static double[] Index1(int M, double M1, double M2)
        {
            double[] rt = new double[M];
            for (int i = 0; i < M; i++)
            {
                rt[i] = M1 * (Math.Pow(M2, i + 1));
            }
            return rt;
        }



        /// <summary>
        /// 几何曲线趋势法
        /// </summary>
        /// <param name="historyValue"></param>
        /// <param name="years"></param>
        /// <returns></returns>
        public static double[] LOG(double[] historyValue, int years, ref double M1, ref double M2)
        {
            int nHistory = historyValue.Length;
            int nTotalYears = nHistory + years;
            //向量Y
            double[,] Y = new double[2, 1];
            double[,] X = new double[2, 2];

            for (int i = 0; i < nHistory; i++)
            {
                Y[0, 0] += Math.Log(historyValue[i]);
                Y[1, 0] += Math.Log(i + 1) * Math.Log(historyValue[i]);

                X[0, 0] = nHistory;
                X[0, 1] += Math.Log(i + 1);

                X[1, 0] += Math.Log(i + 1);
                //   X[1, 1] += Math.Log((i + 1) * (i + 1));
                X[1, 1] += Math.Log((i + 1)) * Math.Log((i + 1));
            }
            double[] rt = new double[years];
            //X的逆矩阵
            double[,] BTBX = InverseMatrix(X);
            if (BTBX == null)
            {

                for (int i = nHistory; i < nTotalYears; i++)
                {
                    rt[i - nHistory] = 0;
                }
                M1 = -999999;
                M2 = -999999;
              

                return rt;

            }
            double[,] U = MultiplyMatrix(BTBX, Y);

            double A = U[0, 0];
            double B = U[1, 0];
            A = Math.Pow(Math.E, A);

            M1 = A;
            M2 = B;

            
            for (int i = nHistory; i < nTotalYears; i++)
            {
                rt[i - nHistory] = A * (Math.Pow(i + 1, B));
            }

            return rt;
        }


        public static double[] LOG1(int M, double M1, double M2)
        {
            double[] rt = new double[M];
            for (int i = 0; i < M; i++)
            {
                rt[i] = M1 * (Math.Pow(i + 1, M2));
            }
            return rt;
        }

        #endregion
    }
}
