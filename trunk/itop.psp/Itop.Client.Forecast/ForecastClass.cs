using System;
using System.Collections.Generic;
using System.Text;
using Itop.Domain.Forecast;
using System.Reflection;
using System.Collections;
using System.Data;

namespace Itop.Client.Forecast
{
    public class ForecastClass
    { 
        public void BadForecast(int type, Ps_forecast_list forecastReport)
        {
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            Common.Services.BaseService.Update("DeletePs_Forecast_MathForecastIDAndForecast", psp_Type);
            psp_Type.Forecast = 0;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
            foreach (Ps_Forecast_Math psp_Typetemp in listTypes)
            {
                string id = psp_Typetemp.ID;
                psp_Type = new Ps_Forecast_Math();
                psp_Type = psp_Typetemp;
                psp_Type.ID = type + "|" + id;// Guid.NewGuid().ToString();
                psp_Type.Forecast = type;
                psp_Type.Col1 = id;
                psp_Type.ParentID = type + "|" + psp_Typetemp.ParentID;

                Ps_BadData pb1 = new Ps_BadData();
                pb1.ForecastID = psp_Typetemp.ForecastID;
                pb1.Forecast = 1;
                pb1.Col1 = id;
                Ps_BadData pb = new Ps_BadData();
                IList<Ps_BadData> li = Common.Services.BaseService.GetList<Ps_BadData>("SelectPs_BadDataByCol1", pb1);
                if (li.Count > 0)
                {
                    pb = li[0];
                    for(int i=forecastReport.StartYear;i<forecastReport.EndYear;i++)
                    {
                        double dl = 0;
                        double dl1 = 0;
                        PropertyInfo pi = psp_Type.GetType().GetProperty("y" + i);
                        PropertyInfo pi1 = pb.GetType().GetProperty("y" + i);
                            try 
                            {

                                dl = Convert.ToDouble(pi.GetValue(psp_Type, null));
                            }
                        catch(Exception ex) 
                            {
                                int b = 1;
                                b++;
                        }
                        try
                        {

                            dl1 = Convert.ToDouble(pi1.GetValue(pb, null));
   
                        }
                        catch (Exception ex)
                        {
                            int b = 1;
                            b++;
                        }

                        if(dl1!=0)
                            pi.SetValue(psp_Type, dl + dl1, null);

                    }
                }
                
                Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
            }

        
        
        }

        public void MaxForecast(Ps_forecast_list forecastReport,DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                string id = dataRow["Col1"].ToString();

                Ps_BadData pb1 = new Ps_BadData();
                pb1.ForecastID = forecastReport.ID;
                pb1.Forecast = 2;
                pb1.Col1 = id;
                Ps_BadData pb = new Ps_BadData();
                IList<Ps_BadData> li = Common.Services.BaseService.GetList<Ps_BadData>("SelectPs_BadDataByCol1", pb1);
                if (li.Count > 0)
                {
                    pb = li[0];
                    for (int i = forecastReport.StartYear; i < forecastReport.EndYear; i++)
                    {
                        PropertyInfo pi1 = pb.GetType().GetProperty("y" + i);
                        double dl = 0;
                        double dl1 = 0;
                        try
                        {
                            dl = Convert.ToDouble(dataRow["y" + i].ToString());
                        }
                        catch { }

                        try
                        {
                            dl1 = Convert.ToDouble(pi1.GetValue(pb, null));
                        }
                        catch { }

                        dataRow["y" + i] = dl + dl1;
                    }
                }
            }
        
        }
    }
}
