using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Itop.Domain.Forecast;
using System.Data;

namespace Itop.Client.Forecast.FormAlgorithm_New
{
    public class commonhelp
    {
        static  Hashtable htable = new Hashtable();

        static Hashtable MethodHtable = new Hashtable();

        static commonhelp()
        {
            MethodHtable.Clear();
            MethodHtable.Add(1, "年增长率法");
            MethodHtable.Add(2, "外推法");
            MethodHtable.Add(4, "弹性系数法");
            MethodHtable.Add(5, "指数平滑法");
            MethodHtable.Add(6, "灰色理论法");
            MethodHtable.Add(12, "大用户法");
            MethodHtable.Add(13, "大用户法");
            MethodHtable.Add(17, "产值单耗法");
            MethodHtable.Add(15, "负荷最大利用小时数法");
            MethodHtable.Add(20, "复合算法");
            MethodHtable.Add(7, "专家决策法");
        }

        public static  bool HasValue(string id, string column)
        {
            bool result = false;
            Ps_Forecast_Math2 pfm;
            if (!htable.ContainsKey(id))
            {
                pfm = Common.Services.BaseService.GetOneByKey<Ps_Forecast_Math2>(id);
                if (pfm != null)
                {
                    htable.Add(id, pfm);
                }
            }
            else
            {
                pfm = htable[id] as Ps_Forecast_Math2;
            }

            if (pfm != null)
            {
                if (int.Parse(pfm.GetType().GetProperty(column).GetValue(pfm,null).ToString()) > 0)
                {
                    result = true;
                }
            }

            return result;
        }
        public static void SetValue(string id, string column, int value)
        {
            Ps_Forecast_Math2 pfm;
            if (htable.ContainsKey(id))
            {
                pfm = htable[id] as Ps_Forecast_Math2;
                pfm.GetType().GetProperty(column).SetValue(pfm, value,null);
                Common.Services.BaseService.Update<Ps_Forecast_Math2>(pfm);
            }
            else
            {
                pfm = new Ps_Forecast_Math2();
                pfm.ID = id;
                pfm.GetType().GetProperty(column).SetValue(pfm, value, null);
                Common.Services.BaseService.Create<Ps_Forecast_Math2>(pfm);
                htable.Add(id, pfm);
            }
        }
        public static void ResetValue(string id, string column)
        {
            if (HasValue(id,column))
            {
                SetValue(id, column, 0);
            }
        }
        public static string GetMethod(int m)
        {
            return MethodHtable[m].ToString();
        }
        public static void CheckHasFixValue(Hashtable ht, DataTable dt, string ForecastID, int type)
        {
            foreach (string  key in ht.Keys)
            {
                bool have = false;
                foreach (DataRow row in dt.Rows)
                {
                    if (key==row["Title"].ToString())
                    {
                        have = true;
                        break;
                    }
                }
                if (!have)
                {
                    try
                    {
                        Ps_Forecast_Math pfm = new Ps_Forecast_Math();
                        pfm.ID = Guid.NewGuid().ToString();
                        pfm.Title = key;
                        pfm.Forecast = type;
                        pfm.ForecastID = ForecastID;
                        object obj = Common.Services.BaseService.GetObject("SelectPs_Forecast_MathMaxID", null);
                        if (obj != null)
                            pfm.Sort = ((int)obj) + 1;
                        else
                            pfm.Sort = 1;
                        Common.Services.BaseService.Create<Ps_Forecast_Math>(pfm);
                    }
                    catch (Exception)
                    {
                    }
                    
                }
            }
            
        }
    }
}
