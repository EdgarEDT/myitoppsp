using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Itop.Domain.Table;
using Itop.Domain.Stutistics;
using System.Data;
using tempU = System.Data;
using System.IO;

namespace Itop.Client.Table
{
    public class OperTable
    {
        public static string ph35 = "35KV电力平衡";
        public static string ph110 = "110KV电力平衡";
        public static string ph220 = "220KV电力平衡";
        public static string ph500 = "500KV电力平衡";
        public static string rst35 = "35KV电量平衡";
        public static string rst110 = "110KV电量平衡";
        public static string rst220 = "220KV电量平衡";
        public static string rst500 = "500KV电量平衡";
        public static string power = "电源规划";
        public static string elec = "电量平衡";
        public static string tzgs = "投资估算";
        public static string tzgsxs = "投资估算增长系数";
        public static string BbWg = "地区无功平衡";
        public Ps_YearRange GetYearRange(string conn)
        {
            try
            {
                IList list = Common.Services.BaseService.GetList("SelectPs_YearRangeByCondition", conn);
                if (list.Count > 0)
                    return (Ps_YearRange)list[0];
                else
                {
                    Ps_YearRange range = new Ps_YearRange();
                    range.BeginYear = 1990;
                    range.StartYear = 2008;
                    range.FinishYear = 2020;
                    range.EndYear = 2060;
                    range.Col1 = "0.03";
                    return range;
                }
            }
            catch { return null; }
        }
        public static string[] often = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};
        
        
        public IList<string> GetLineType(string conn)
        {
            return Common.Services.BaseService.GetList<string>("SelectProject_SumByGroupType", conn);
        }
        public IList<string> GetLineName(string conn)
        {
            return Common.Services.BaseService.GetList<string>("SelectProject_SumByGroupName", conn);
        }
        public IList<string> GetLineT5(string conn)
        {
            return Common.Services.BaseService.GetList<string>("SelectProject_SumByGroupT5", conn);
        }
        public IList<string> GetLineS1(string conn)
        {
            return Common.Services.BaseService.GetList<string>("SelectProject_SumByGroupS1", conn);
        }
        public double GetAllVol(string conn)
        {
            IList<Project_Sum> list = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByAllVol", conn);
            if (list.Count > 0)
                return list[0].Num;
            else
                return 0.0;
        }
        public static int GetMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("SelectMaxSort", "");
                return sort;
            }
            catch { return 0; }
        }

        public static int Get100MaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("Select100MaxSort", "");
                return sort;
            }
            catch { return 0; }
        }
        public static int GetWGMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("SelectPs_Table_WGMaxSort", "");
                return sort;
            }
            catch { return 0; }
        }
        public static int Get35MaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("Select35MaxSort", "");
                return sort;
            }
            catch { return 0; }
        }
        public static int Get500MaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("Select500MaxSort", "");
                return sort;
            }
            catch { return 0; }
        }
        public static int Get220MaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("Select200MaxSort", "");
                return sort;
            }
            catch { return 0; }
        }

        public static int Get220ResultMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("Select220ResultMaxSort", "");
                return sort;
            }
            catch { return 0; }
        }

        public static int Get110ResultMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("Select110ResultMaxSort", "");
                return sort;
            }
            catch { return 0; }
        }
        public static int Get35ResultMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("Select35ResultMaxSort", "");
                return sort;
            }
            catch { return 0; }
        }
        public static int Get500ResultMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("Select500ResultMaxSort", "");
                return sort;
            }
            catch { return 0; }
        }

        public static int GetChildMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("SelectChildMaxSort", "");
                return sort;
            }
            catch { return 0; }
        }

        public static int GetPowerBuildMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("SelectPowerBuildMaxSort", "");
                return sort;
            }
            catch { return 0; }
        }

        public static int GetElecMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("SelectElecMaxSort", "");
                return sort;
            }
            catch { return 0; }
        }

        public static int GetTZGSMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("SelectTZGSMaxSort", "");
                return sort;
            }
            catch { return 0; }
        }

        public static int GetBuildProMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("SelectBuildProMaxSort", "");
                return sort;
            }
            catch { return 0; }
        }


        public static int GetAreaMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("SelectAreaDataSort", "");
                return sort;
            }
            catch { return 0; }
        }
        public static int GetGDPMaxSort()
        {
            try
            {
                int sort = (int)Common.Services.BaseService.GetObject("SelectGDPSort", "");
                return sort;
            }
            catch { return 0; }
        }

        public static tempU::DataTable GetExcel(string filepach, IList<string> filedList, IList<string> capList)
        {
            string str;
            FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

            try
            {
                fpSpread1.OpenExcel(filepach);
            }
            catch
            {
                string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
                File.Copy(filepach, filepath1);
                fpSpread1.OpenExcel(filepath1);
                File.Delete(filepath1);
            }
            tempU::DataTable dt = new tempU::DataTable();
            
            
            IList<string> fie = new List<string>();
           
            int m = 3;
            
            for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
            {
                if (capList.Contains(fpSpread1.Sheets[0].Cells[2, j].Text))
                    fie.Add(filedList[capList.IndexOf(fpSpread1.Sheets[0].Cells[2, j].Text)]);
            }

            for (int k = 0; k < fie.Count; k++)
            {
                dt.Columns.Add(fie[k]);
            }
            for (int i = m; i <= fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            {

                DataRow dr = dt.NewRow();
                for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {
                    dr[fie[j]] = fpSpread1.Sheets[0].Cells[i, j].Text;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
