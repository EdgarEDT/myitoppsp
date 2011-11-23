using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Domain.Forecast;
using Itop.Client.Common;
using Itop.Domain.Layouts;
using Itop.Domain.Table;
using DevExpress.Utils;
using Itop.Domain.PWTable;
using Itop.Domain.Stutistic;
using Itop.Client.Stutistics;
using Itop.Domain.Graphics;
using Itop.Domain.HistoryValue;
using System.Configuration;
using Itop.Client.Base;
namespace Itop.Client.DataCopy
{
    public partial class FormModuleList : FormBase
    {
        DataTable dt = new DataTable();
        string pid = "";
        string pid1 = "";

        public string PID
        {
            set { pid = value; }
        }

        public string PID1
        {
            set { pid1 = value; }
        }

        public FormModuleList()
        {
            InitializeComponent();
        }
        ModuleDataCopy copy = new ModuleDataCopy();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool issucc = true;          
            WaitDialogForm wait = new WaitDialogForm("", "正在拷贝数据, 请稍候...");
            this.Cursor = Cursors.WaitCursor;
            ArrayList al = new ArrayList();
            int m = 1;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (!(bool)row["C"])
                        continue;

                  
                    switch (row["A"].ToString())
                    {
                        case "1"://电力发展实绩
                            //数据
                            m = 1;
                            Ps_History psp_Type1 = new Ps_History();
                            psp_Type1.Forecast = 1;
                            psp_Type1.Col4 = pid;
                            IList<Ps_History> listTypes1 = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type1);
                            foreach (Ps_History ph in listTypes1)
                            {
                                string temp=DateTime.Now.ToOADate().ToString();
                                if (ph.ID.Length < 60)
                                {
                                    ph.ID = ph.ID + "|" + pid;
                                }
                                ph.ID = ph.ID.Replace(pid, pid1);
                                if (ph.ParentID != null)
                                {
                                    ph.ParentID = ph.ParentID.Replace(pid, pid1);
                                }
                              
                                ph.Col4 = pid1;
                                Services.BaseService.Create<Ps_History>(ph);
                            }

                            //年份数据
                            Ps_YearRange py = new Ps_YearRange();
                            py.Col4 = "电力发展实绩";
                            py.Col5 = pid;
                            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
                            foreach (Ps_YearRange pf in li)
                            {
                                pf.Col5 = pid1;
                                pf.ID = Guid.NewGuid().ToString();
                                Services.BaseService.Create<Ps_YearRange>(pf);
                            }
                            //m++;

                            break;
                        case "2"://分区县用电情况
                            m = 2;
                            Ps_History psp_Type2 = new Ps_History();
                            psp_Type2.Forecast = 2;
                            psp_Type2.Col4 = pid;
                            IList<Ps_History> listTypes2 = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type2);
                            foreach (Ps_History ph in listTypes2)
                            {
                                ph.ID = ph.ID.Replace(pid, pid1);
                                if (ph.ParentID != null)
                                {
                                    ph.ParentID = ph.ParentID.Replace(pid, pid1);
                                }
                                ph.Col4 = pid1;
                                Services.BaseService.Create<Ps_History>(ph);
                            }

                            psp_Type2.Forecast = 3;
                            listTypes2 = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type2);
                            foreach (Ps_History ph in listTypes2)
                            {
                                ph.ID = ph.ID.Replace(pid, pid1);
                                if (ph.ParentID != null)
                                {
                                    ph.ParentID = ph.ParentID.Replace(pid, pid1);
                                }
                                ph.Col4 = pid1;
                                Services.BaseService.Create<Ps_History>(ph);
                            }
                            //年份数据
                            Ps_YearRange py1 = new Ps_YearRange();
                            py1.Col4 = "区县发展实绩";
                            py1.Col5 = pid;
                            IList<Ps_YearRange> li1 = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py1);
                            foreach (Ps_YearRange pf in li1)
                            {
                                pf.Col5 = pid1;
                                pf.ID = Guid.NewGuid().ToString();
                                Services.BaseService.Create<Ps_YearRange>(pf);
                            }
                            //Itop.Domain.HistoryValue.PSP_Types py1 = new Itop.Domain.HistoryValue.PSP_Types();
                            string sql = " ProjectID='" + pid + "'";

                            IList<Itop.Domain.HistoryValue.PSP_Types> li2 = Services.BaseService.GetList<Itop.Domain.HistoryValue.PSP_Types>("SelectPSP_TypesByWhere", sql);
                            foreach (Itop.Domain.HistoryValue.PSP_Types pf in li2)
                            {
                                pf.ProjectID = pid1;
                                //pf.ID = Guid.NewGuid().ToString();
                                Services.BaseService.Create<Itop.Domain.HistoryValue.PSP_Types>(pf);
                                string _v = " TypeID="+pf.ID;
                                IList<PSP_Values> _pvlist =Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", _v);

                                string _s=" ProjectID='" + pid1 + "' and Title='"+pf.Title+"'";
                                PSP_Types _p= (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere",_s);

                                for (int n = 0; n < _pvlist.Count; n++)
                                {
                                    PSP_Values ppp = _pvlist[n];
                                    ppp.TypeID = _p.ID;
                                    Services.BaseService.Create<PSP_Values>(ppp);
                                }

                            }

                            //m++;

                            break;
                        case "3"://分区县供电实绩
                            m = 3;
                            Ps_History psp_Type3 = new Ps_History();
                            psp_Type3.Forecast = 4;
                            psp_Type3.Col4 = pid;
                            IList<Ps_History> listTypes3 = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type3);
                            foreach (Ps_History ph in listTypes3)
                            {
                                ph.ID = ph.ID.Replace(pid, pid1);
                                if (ph.ParentID != null)
                                {
                                    ph.ParentID = ph.ParentID.Replace(pid, pid1);
                                }
                                ph.Col4 = pid1;
                                Services.BaseService.Create<Ps_History>(ph);
                            }
                            //年份数据
                            Ps_YearRange py3 = new Ps_YearRange();
                            py3.Col4 = "区县发展实绩";
                            py3.Col5 = pid;
                            IList<Ps_YearRange> li3 = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py3);
                            foreach (Ps_YearRange pf in li3)
                            {
                                pf.Col5 = pid1;
                                pf.ID = Guid.NewGuid().ToString();
                                Services.BaseService.Create<Ps_YearRange>(pf);
                            }

                          
                            break;
                        case "4"://设备参数
                            m = 4;
                            //变电站数据
                            PSP_Substation_Info psp_sub_in = new PSP_Substation_Info();
                            psp_sub_in.AreaID = pid;
                            IList<PSP_Substation_Info> listTypes4 = Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoCopy_byProj", psp_sub_in);
                            foreach (PSP_Substation_Info ph in listTypes4)
                            {
                                ph.UID = "bdz_"+Guid.NewGuid().ToString();
                                ph.AreaID = pid1;
                                Services.BaseService.Create<PSP_Substation_Info>(ph);
                            }
                            //其它数据dev中

                            PSPDEV psp_dev = new PSPDEV();
                            psp_dev.ProjectID= pid;
                            IList<PSPDEV> listTypes41 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEV_CopybyProj", psp_dev);
                            foreach (PSPDEV ph in listTypes41)
                            {
                                ph.SUID = Guid.NewGuid().ToString();
                                ph.ProjectID = pid1;
                                Services.BaseService.Create<PSPDEV>(ph);
                            }
                            break;
                        case "5"://电力平衡数据
                            m = 5;
                            ///无功平衡 Ps_Table_WG   InsertPs_Table_WG  SelectPs_Table_WGListByConn
                            ///500kv变电容量平衡表 Ps_Table_500PH   InsertPs_Table_500PH   SelectPs_Table_500PHListByConn
                            ///220kv变电容量平衡表 Ps_Table_200PH    InsertPs_Table_200PH  SelectPs_Table_200PHListByConn
                            ///110kv变电容量平衡表 Ps_Table_100PH    InsertPs_Table_100PH   SelectPs_Table_100PHListByConn
                            ///35kv变电容量平衡表 Ps_Table_35PH    InsertPs_Table_35PH   SelectPs_Table_35PHListByConn
                            ///电力平衡表         Ps_Table_ElecPH  InsertPs_Table_ElecPH  SelectPs_Table_ElecPHByConn
                            ///
                            ///
                            copy.CopyData(pid, pid1, "Ps_Table_WG", "InsertPs_Table_WG", "SelectPs_Table_WGListByConn");
                            copy.CopyData(pid, pid1, "Ps_Table_35PH", "InsertPs_Table_35PH", "SelectPs_Table_35PHListByConn");
                            //copy.CopyData(pid, pid1, "Ps_PowerBuild", "InsertPs_PowerBuild", "SelectPs_PowerBuildByConn");
                            copy.CopyData(pid, pid1, "Ps_Table_100PH", "InsertPs_Table_100PH", "SelectPs_Table_100PHListByConn");
                            //copy.CopyData(pid, pid1, "Ps_Table_110Result", "InsertPs_Table_110Result", "SelectPs_Table_110ResultByConn");
                            copy.CopyData(pid, pid1, "Ps_Table_200PH", "InsertPs_Table_200PH", "SelectPs_Table_200PHListByConn");
                           // copy.CopyData(pid, pid1, "Ps_Table_220Result", "InsertPs_Table_220Result", "SelectPs_Table_220ResultByConn");
                            copy.CopyData(pid, pid1, "Ps_Table_500PH", "InsertPs_Table_500PH", "SelectPs_Table_500PHListByConn");
                            //copy.CopyData(pid, pid1, "Ps_Table_500Result", "InsertPs_Table_500Result", "SelectPs_Table_500ResultByConn");
                            copy.CopyData(pid, pid1, "Ps_Table_ElecPH", "InsertPs_Table_ElecPH", "SelectPs_Table_ElecPHByConn");
                            //copy.CopyData(pid, pid1, "Ps_Table_Edit", "InsertPs_Table_Edit", "SelectPs_Table_EditListByConn");

                            string[] array ={ "地区无功平衡", "35KV电力平衡", "110KV电力平衡", "220KV电力平衡", "500KV电力平衡", "电量平衡" };
                            //年份数据
                            for (int i = 0; i < array.Length; i++)
                            {
                                Ps_YearRange py5 = new Ps_YearRange();
                                py5.Col4 = array[i];
                                py5.Col5 = pid;
                                IList<Ps_YearRange> li5 = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py5);
                                foreach (Ps_YearRange pf in li5)
                                {
                                    pf.Col5 = pid1;
                                    pf.ID = Guid.NewGuid().ToString();
                                    Services.BaseService.Create<Ps_YearRange>(pf);
                                }

                            }
                            

                            //m++;

                            break;
                      
                        case "6"://地区维护
                            m = 6;


                            PS_Table_AreaWH ps_ta = new PS_Table_AreaWH();
                            ps_ta.ProjectID = pid;
                            IList<PS_Table_AreaWH> listTypes6 = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHCopyByProj", ps_ta);
                            foreach (PS_Table_AreaWH ph in listTypes6)
                            {
                                ph.ID = ph.ID.Replace(pid, pid1);
                                ph.ProjectID = pid1;
                                Services.BaseService.Create<PS_Table_AreaWH>(ph);
                            }

                            //m++;

                            break;
                        case "7"://电网类型
                            m = 7;

                            PS_Table_Area_TYPE ps_ta7 = new PS_Table_Area_TYPE();
                            ps_ta7.ProjectID = pid;
                            IList<PS_Table_Area_TYPE> listTypes7 = Services.BaseService.GetList<PS_Table_Area_TYPE>("SelectPS_Table_Area_TYPECopyByProj", ps_ta7);
                            foreach (PS_Table_Area_TYPE ph in listTypes7)
                            {

                                ph.ID = Guid.NewGuid().ToString();
                                ph.ProjectID = pid1;
                                Services.BaseService.Create<PS_Table_Area_TYPE>(ph);
                            }

                            //m++;
                            break;
                     
                        case "8":// 负荷预测

                            m = 8;
                            //方案
                            Ps_forecast_list report = new Ps_forecast_list();
                            report.UserID = pid;  //SetCfgValue("lastLoginUserNumber", Application.ExecutablePath + ".config");
                            report.Col1 = "1";
                            IList<Ps_forecast_list> ilist9 = Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByCOL1AndUserID", report);
                            foreach (Ps_forecast_list ph in ilist9)
                            {
                                Ps_forecast_list ph1 = new Ps_forecast_list();
                                string struid = ph.ID;
                                ph1 = ph;
                                ph1.UserID = pid1;
                                //ph.Creater = pid1;
                                ph1.ID = Guid.NewGuid().ToString();
                                Services.BaseService.Create<Ps_forecast_list>(ph1);
                                //数据
                                IList<Ps_Forecast_Math> ilisttemp = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", "ForecastID='" + struid + "'");
                                foreach (Ps_Forecast_Math pf in ilisttemp)
                                {
                                    pf.ForecastID = ph1.ID;
                                    pf.ID = pf.ID.Replace(pid, pid1) ;
                                    if (pf.ParentID != null)
                                    {
                                        pf.ParentID = pf.ParentID.Replace(pid, pid1);
                                    }
                                    object obj = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(pf);
                                    if (obj == null)
                                        Services.BaseService.Create<Ps_Forecast_Math>(pf);
                                    else
                                        Services.BaseService.Update<Ps_Forecast_Math>(pf);


                                }
                              

                            }
                          
                            //Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                            //psp_Type.Forecast = 4;
                            //psp_Type.ForecastID = "4";
                            //psp_Type.Col4 = pid;
                            //IList<Ps_Forecast_Math> ilist10 = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByCol4", psp_Type);
                            //foreach (Ps_Forecast_Math pfm in ilist10)
                            //{
                            //    pfm.Col4 = pid1;
                            //    if (pfm.ID.Contains("|"))
                            //    {
                            //        pfm.ID = pfm.ID.Replace(pid, pid1);
                            //        pfm.ParentID = pfm.ParentID.Replace(pid, pid1);
                            //    }
                            //    else
                            //    {
                            //        pfm.ID = pfm.ID + "|" + pid1;
                            //        if (pfm.ParentID != "")
                            //            pfm.ParentID = pfm.ParentID + "|" + pid1;
                            //    }
                            //    object obj = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(pfm);
                            //    if (obj == null)
                            //    Services.BaseService.Create<Ps_Forecast_Math>(pfm);
                            //    else
                            //    Services.BaseService.Create<Ps_Forecast_Math>(pfm);
                            //}
                            //report = new Ps_forecast_list();
                            //report.UserID = pid;  //SetCfgValue("lastLoginUserNumber", Application.ExecutablePath + ".config");
                            //report.Col1 = "2";
                            //ilist9.Clear();
                            //ilist9 = Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByCOL1AndUserID", report);
                            //foreach (Ps_forecast_list ph in ilist9)
                            //{
                            //    Ps_forecast_list ph1 = new Ps_forecast_list();
                            //    string struid = ph.ID;
                            //    ph1 = ph;
                            //    ph1.UserID = pid1;
                            //    //ph.Creater = pid1;
                            //    ph1.ID = Guid.NewGuid().ToString();
                            //    Services.BaseService.Create<Ps_forecast_list>(ph1);

                            //    IList<Ps_Forecast_Math> ilisttemp = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", "ForecastID='" + struid + "' and  (Forecast='3' or Forecast='2')");
                            //    foreach (Ps_Forecast_Math pf in ilisttemp)
                            //    {

                            //        pf.ForecastID = ph1.ID;
                            //        if (pf.ID.Contains("|"))
                            //        {
                            //            pf.ID = pf.ID.Replace(pid, pid1);
                            //            pf.ParentID = pf.ParentID.Replace(pid, pid1);
                            //        }
                            //        else
                            //        {
                            //            pf.ID = pf.ID + "|" + pid1;
                            //            if (pf.ParentID != "")
                            //                pf.ParentID = pf.ParentID + "|" + pid1;
                            //        }
                            //        object obj = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(pf);
                            //        if (obj == null)

                            //        Services.BaseService.Create<Ps_Forecast_Math>(pf);
                            //        else
                            //        Services.BaseService.Update<Ps_Forecast_Math>(pf);


                            //    }
                               

                            //}
                          



                            break;
                        case "9"://供电企业明细
                            m = 9;

                            Ps_Table_Enterprise ps_ta9 = new Ps_Table_Enterprise();
                            ps_ta9.ProjectID = pid;
                            IList<Ps_Table_Enterprise> listTypes9 = Services.BaseService.GetList<Ps_Table_Enterprise>("SelectPs_Table_EnterpriseCopyByProj", ps_ta9);
                            foreach (Ps_Table_Enterprise ph in listTypes9)
                            {
                                ph.UID= Guid.NewGuid().ToString();
                                ph.ProjectID = pid1;
                                Services.BaseService.Create<Ps_Table_Enterprise>(ph);
                            }

                            //m++;
                            break;
                            /*
                             * 
                        case "9":
                            m = 9;
                            PW_tb3a p = new PW_tb3a();
                            p.col2 = pid;
                            IList<PW_tb3a> PWlist = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aList", p);
                            foreach (PW_tb3a PWrtbc1 in PWlist)
                            {
                                string str = Guid.NewGuid().ToString();
                                PW_tb3b p2 = new PW_tb3b();
                                p2.col7 = PWrtbc1.UID;
                                IList<PW_tb3b> listtb3b = Services.BaseService.GetList<PW_tb3b>("SelectPW_tb3bListbyCol7", p2);

                                foreach (PW_tb3b pwt in listtb3b)
                                {
                                    pwt.col7 = str;
                                    pwt.UID = Guid.NewGuid().ToString();
                                    Services.BaseService.Create<PW_tb3b>(pwt);
                                }


                                PWrtbc1.UID = str;
                                PWrtbc1.col2 = pid1;
                                Services.BaseService.Create<PW_tb3a>(PWrtbc1);


                            }


                            PW_tb3c PWp2 = new PW_tb3c();
                            PWp2.col4 = pid;
                            IList<PW_tb3c> PWlist2 = Services.BaseService.GetList<PW_tb3c>("SelectPW_tb3cList", PWp2);
                            foreach (PW_tb3c PWrtbc2 in PWlist2)
                            {
                                PWrtbc2.UID = Guid.NewGuid().ToString();
                                PWrtbc2.col4 = pid1;
                                Services.BaseService.Create<PW_tb3c>(PWrtbc2);


                            }
                            //  Substation_Info pifo = new Substation_Info();
                            string RtfCategorys = " UID like '%|" + pid + "' and AreaID='" + pid + "'";
                            IList<Substation_Info> Sublist2 = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", RtfCategorys);
                            foreach (Substation_Info Subrtbc2 in Sublist2)
                            {
                                Subrtbc2.UID = Guid.NewGuid().ToString() + "|" + pid1;
                                Subrtbc2.AreaID = pid1;
                                Services.BaseService.Create<Substation_Info>(Subrtbc2);


                            }
                            //m++;
                            break;
                        case "10":
                            m = 10;
                            copy.CopyData(pid, pid1, "Ps_Table_TZGS", "InsertPs_Table_TZGS", "SelectPs_Table_TZGSByConn");

                            //m++;

                            break;
                              */
                        case "11":
                            m = 11;
                            ////典型、最大日负荷数据
                            IList<BurdenLine> bdlist = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByWhere", " uid like '%" + pid + "%'  order by BurdenDate");
                            foreach (BurdenLine pf in bdlist)
                            {
                                if (pf.UID.Contains("|"))
                                {
                                    pf.UID = pf.UID.Replace(pid, pid1);
                                   
                                }
                                else
                                {
                                    pf.UID = pf.UID + "|" + pid1;
                                   
                                }
                                Services.BaseService.Create<BurdenLine>(pf);
                            }
                            //m++;
                            break;
                        case "12":
                            m = 12;
                            ////月最大负荷数据
                            IList<BurdenMonth> bmlist = Services.BaseService.GetList<BurdenMonth>("SelectBurdenMonthByWhere", " uid like '%" + pid + "%'  order by BurdenYear");
                            foreach (BurdenMonth pf in bmlist)
                            {
                                if (pf.UID.Contains("|"))
                                {
                                    pf.UID = pf.UID.Replace(pid, pid1);

                                }
                                else
                                {
                                    pf.UID = pf.UID + "|" + pid1;

                                }
                                Services.BaseService.Create<BurdenMonth>(pf);
                            }
                            //m++;
                            break;
                            /*
                        case "13":
                            m = 13;
                            ////输电线路情况
                           
                             IList<Line_Info> liflist = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByConn", "AreaID='" + pid + "'");
                             foreach (Line_Info pf in liflist)
                             {
                                 if (pf.UID.Contains("|"))
                                 {
                                     pf.UID = pf.UID.Replace(pid, pid1);

                                 }
                                 else
                                 {
                                     pf.UID = pf.UID + "|" + pid1;

                                 }
                                 pf.AreaID = pf.AreaID.Replace(pid, pid1);
                                 pf.CreateDate = DateTime.Now;
                                Line_Info pftemp= Services.BaseService.GetOneByKey<Line_Info>(pf);
                                if (pftemp == null)
                                    Services.BaseService.Create<Line_Info>(pf);
                                else
                                {
                                    Services.BaseService.Update<Line_Info>(pf);
                                
                                }

                            }
                            //m++;
                            break;
                        case "14":
                            ////电厂资料
                            m = 14;
                             string  flags1 = "1";

                             IList<PSP_PowerSubstationInfo> ppslist = Services.BaseService.GetList<PSP_PowerSubstationInfo>("SelectPSP_PowerSubstationInfoByFlag", flags1 + "|" + pid.Substring(0, 20));
                             foreach (PSP_PowerSubstationInfo pf in ppslist)
                             {
                                 pf.UID = Guid.NewGuid().ToString();
                                 if (pf.Flag.Contains("|"))
                                 {
                                     pf.Flag = pf.Flag.Replace(pid.Substring(0, 20), pid1.Substring(0, 20));

                                 }
                                 else
                                 {
                                     pf.Flag = pf.Flag + "|" + pid1.Substring(0, 20);

                                 }
                                 
                                 pf.CreateDate = DateTime.Now;
                                 PSP_PowerSubstationInfo pftemp = Services.BaseService.GetOneByKey<PSP_PowerSubstationInfo>(pf);
                                 if (pftemp == null)
                                     Services.BaseService.Create<PSP_PowerSubstationInfo>(pf);
                                 else
                                 {
                                     Services.BaseService.Update("UpdatePSP_PowerSubstationInfo",pf);

                                 }

                                

                             }

                             PowerSubstationLine psl = new PowerSubstationLine();
                             psl.Flag = flags1;

                             psl.Type2 = "PowerSubstation" + "|" + pid.Substring(0, 20); ;

                             IList<PowerSubstationLine> pslist = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType2", psl);
                             foreach (PowerSubstationLine pf in pslist)
                             {
                                 pf.UID = Guid.NewGuid().ToString();
                                 if (pf.Type2.Contains("|"))
                                 {
                                     pf.Type2 = pf.Type2.Replace(pid.Substring(0, 20), pid1.Substring(0, 20));

                                 }
                                 else
                                 {
                                     pf.Type2 = pf.Type2 + "|" + pid1.Substring(0, 20);

                                 }
                                 Services.BaseService.Create<PowerSubstationLine>(pf);
                             }
                             
                             //m++;

                            break;
                        case "15":
                            //基础数据及计算方案
                            m = 15;
                            IList<PSPDEV> listPSPDEV = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition"," WHERE ProjectID = '" + pid + "'");
                            foreach (PSPDEV dev in listPSPDEV)
                            {
                                string oldSUID = dev.SUID;
                                PSPDEV pdev = new PSPDEV();
                                pdev = dev;
                                pdev.SUID = Guid.NewGuid().ToString();
                                pdev.ProjectID = pid1;
                                Services.BaseService.Create<PSPDEV>(pdev);  
                            }

                            PSP_ELCPROJECT elc = new PSP_ELCPROJECT();
                            elc.ProjectID = pid;
                            IList<PSP_ELCPROJECT> listELCPROJECT = Services.BaseService.GetList<PSP_ELCPROJECT>("SelectPSP_ELCPROJECTList",elc);
                     
                            IList<PSP_ElcDevice> listELCDEVICE = new List<PSP_ElcDevice>();
                            IList<SVGFILE> listSVGFILE = new List<SVGFILE>();
                            SVGFILE svg = new SVGFILE();
                            foreach (PSP_ELCPROJECT elcPRO in listELCPROJECT)
                            {
                                PSP_ELCPROJECT pel = new PSP_ELCPROJECT();                               
                                pel.ID = Guid.NewGuid().ToString();
                                pel.ProjectID = pid1;
                                pel.Name = elcPRO.Name;
                                pel.Class = elcPRO.Class;
                                pel.FileType = elcPRO.FileType;
                                Services.BaseService.Create<PSP_ELCPROJECT>(pel);

                                IList<PSP_ElcDevice> listT = Services.BaseService.GetList<PSP_ElcDevice>("SelectPSP_ElcDeviceByCondition", " WHERE ProjectSUID ='" + elcPRO.ID + "'");
                                foreach (PSP_ElcDevice elcD in listT)
                                {
                                    listELCDEVICE.Add(elcD);
                                    PSPDEV oldDEV = Services.BaseService.GetOneByKey<PSPDEV>(elcD.DeviceSUID);
                                    if (oldDEV!=null)
                                    {
                                        string con = " WHERE Name = '" + oldDEV.Name + "' and Type = '" + oldDEV.Type + "' and Number = " + oldDEV.Number + " and ProjectID = '" + pid1 + "'";
                                        PSPDEV newDEV = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByCondition", con);
                                        if(newDEV!=null)
                                        {
                                            PSP_ElcDevice elcNEW = new PSP_ElcDevice();
                                            elcNEW = elcD;
                                            elcNEW.DeviceSUID = newDEV.SUID;
                                            elcNEW.ProjectSUID = pel.ID;
                                            Services.BaseService.Create<PSP_ElcDevice>(elcNEW);
                                        }
                                    }
                                }
                                svg.SUID = elcPRO.ID;
                                SVGFILE listSV = Services.BaseService.GetOneByKey<SVGFILE>(svg);
                                listSVGFILE.Add(listSV);
                                if (listSV!=null)
                                {
                                    SVGFILE svgN = new SVGFILE();
                                    svgN = listSV;
                                    svgN.SUID = pel.ID;
                                    Services.BaseService.Create<SVGFILE>(svgN);
                                }
                            }


                            IList<PSP_Substation_Info> listSUBSTATION = Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere"," AreaID = '"+pid+"'");
                            foreach (PSP_Substation_Info psi in listSUBSTATION)
                            {
                                string oldUID = psi.UID;
                                PSP_Substation_Info ps = new PSP_Substation_Info();
                                ps = psi;
                                ps.UID = Guid.NewGuid().ToString();
                                ps.AreaID = pid1;
                                Services.BaseService.Create<PSP_Substation_Info>(ps);
                                string con = " WHERE SvgUID = '" + oldUID + "' and ProjectID = '" + pid1 + "'";
                                IList<PSPDEV> listDEV = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                                foreach (PSPDEV psp in listDEV)
                                {
                                    psp.SvgUID = ps.UID;
                                    Services.BaseService.Update<PSPDEV>(psp);
                                }
                            }
                            IList<PSP_PowerSubstationInfo> listPOWER = Services.BaseService.GetList<PSP_PowerSubstationInfo>("SelectPSP_PowerSubstationInfoByConn"," AreaID = '" + pid +"'");
                            foreach (PSP_PowerSubstationInfo pps in listPOWER)
                            {
                                string oldUID = pps.UID;
                                PSP_PowerSubstationInfo pp = new PSP_PowerSubstationInfo();
                                pp = pps;
                                pp.UID = Guid.NewGuid().ToString();
                                pp.AreaID = pid1;
                                Services.BaseService.Create<PSP_PowerSubstationInfo>(pp);
                                string con = " WHERE SvgUID = '" + oldUID + "' and ProjectID = '" + pid1 + "'";
                                IList<PSPDEV> listDEV = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                                foreach (PSPDEV psp in listDEV)
                                {
                                    psp.SvgUID = pp.UID;
                                    Services.BaseService.Update<PSPDEV>(psp);
                                }
                            }
                            break;
                             * */
                    }
                }

            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
              
                issucc = false;
            }


            this.Cursor = Cursors.Default;
            wait.Close();
            if (issucc)
            MessageBox.Show("拷贝成功", "提示");
            else
            MessageBox.Show("拷贝失败  "+ m, "提示");

            this.DialogResult = DialogResult.OK;
        }
        private string createID()
        {
            string str = Guid.NewGuid().ToString();
            return str.Substring(str.Length - 12);
        }
        private void FormModuleList_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C",typeof(bool));
            dt.Columns.Add("D");


            DataRow row = dt.NewRow();
            row["A"] = "1";
            row["B"] = "电力发展实绩";
            row["C"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["A"] = "2";
            row["B"] = "分区县用电情况";
            row["C"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["A"] = "3";
            row["B"] = "分区供电实绩";
            row["C"] = false;
            dt.Rows.Add(row);
           

            row = dt.NewRow();
            row["A"] = "4";
            row["B"] = "设备参数";
            row["C"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["A"] = "5";
            row["B"] = "电力平衡数据";
            row["C"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["A"] = "6";
            row["B"] = "地区维护";
            row["C"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["A"] = "7";
            row["B"] = "电网类型";
            row["C"] = false;
            dt.Rows.Add(row);


            row = dt.NewRow();
            row["A"] = "8";
            row["B"] = "负荷预测";
            row["C"] = false;
            dt.Rows.Add(row);


            row = dt.NewRow();
            row["A"] = "9";
            row["B"] = "供电企业明细";
            row["C"] = false;
            dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["A"] = "4";
            //row["B"] = "文字资料";
            //row["C"] = false;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["A"] = "5";
            //row["B"] = "Excel文档";
            //row["C"] = false;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["A"] = "6";
            //row["B"] = "经济分析";
            //row["C"] = false;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["A"] = "7";
            //row["B"] = "规划编制";
            //row["C"] = false;
            //dt.Rows.Add(row);
            

            //row = dt.NewRow();
            //row["A"] = "8";
            //row["B"] = "电力预测";
            //row["C"] = false;
            //dt.Rows.Add(row);

            

            //row = dt.NewRow();
            //row["A"] = "9";
            //row["B"] = "配网表格";
            //row["C"] = false;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["A"] = "10";
            //row["B"] = "投资估算";
            //row["C"] = false;
            //dt.Rows.Add(row);

            row = dt.NewRow();
            row["A"] = "11";
            row["B"] = "典型、最大日负荷数据";
            row["C"] = false;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["A"] = "12";
            row["B"] = "月最大负荷数据";
            row["C"] = false;
            dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["A"] = "13";
            //row["B"] = "输电线路情况";
            //row["C"] = false;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["A"] = "14";
            //row["B"] = "电厂资料";
            //row["C"] = false;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["A"] = "15";
            //row["B"] = "电气计算";
            //row["C"] = false;
            //dt.Rows.Add(row);

            this.gridControl1.DataSource = dt;
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.SetRowCellValue(i, "C", checkEdit1.Checked);
            }
        }
   
        /// <summary>
        /// 删除卷数据方法
        /// </summary>
        /// <param name="uid">卷号</param>
        private  void DeleteData(string uid)
        {
            try
            {
                //电力发展实绩
                //数据
                Ps_History psp_Type1 = new Ps_History();
                psp_Type1.Forecast = 1;
                psp_Type1.Col4 = uid;
                IList<Ps_History> listTypes1 = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type1);
                foreach (Ps_History ph in listTypes1)
                {

                    Services.BaseService.Delete<Ps_History>(ph);
                }

                //年份数据
                Ps_YearRange py = new Ps_YearRange();
                py.Col4 = "电力发展实绩";
                py.Col5 = uid;
                IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
                foreach (Ps_YearRange pf in li)
                {

                    Services.BaseService.Delete<Ps_YearRange>(pf);
                }

                //分区县用电情况

                Ps_History psp_Type2 = new Ps_History();
                psp_Type2.Forecast = 2;
                psp_Type2.Col4 = uid;
                IList<Ps_History> listTypes2 = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type2);
                foreach (Ps_History ph in listTypes2)
                {
                    Services.BaseService.Delete<Ps_History>(ph);
                }

                psp_Type2.Forecast = 3;
                listTypes2 = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type2);
                foreach (Ps_History ph in listTypes2)
                {
                    Services.BaseService.Delete<Ps_History>(ph);
                }
                //年份数据
                Ps_YearRange py1 = new Ps_YearRange();
                py1.Col4 = "区县发展实绩";
                py1.Col5 = uid;
                IList<Ps_YearRange> li1 = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py1);
                foreach (Ps_YearRange pf in li1)
                {
                    Services.BaseService.Delete<Ps_YearRange>(pf);
                }


                //分区县供电实绩

                Ps_History psp_Type3 = new Ps_History();
                psp_Type3.Forecast = 4;
                psp_Type3.Col4 = uid;
                IList<Ps_History> listTypes3 = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type3);
                foreach (Ps_History ph in listTypes3)
                {

                    Services.BaseService.Delete<Ps_History>(ph);
                }
                //年份数据
                Ps_YearRange py3 = new Ps_YearRange();
                py3.Col4 = "分区供电实绩";
                py3.Col5 = uid;
                IList<Ps_YearRange> li3 = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py3);
                foreach (Ps_YearRange pf in li3)
                {

                    Services.BaseService.Delete<Ps_YearRange>(pf);
                }


                //设备参数
                //变电站数据
                PSP_Substation_Info psp_sub_in = new PSP_Substation_Info();
                psp_sub_in.AreaID = uid;
                IList<PSP_Substation_Info> listTypes4 = Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoCopy_byProj", psp_sub_in);
                foreach (PSP_Substation_Info ph in listTypes4)
                {
                    Services.BaseService.Delete<PSP_Substation_Info>(ph);
                }
                //其它数据dev中

                PSPDEV psp_dev = new PSPDEV();
                psp_dev.ProjectID = uid;
                IList<PSPDEV> listTypes41 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEV_CopybyProj", psp_dev);
                foreach (PSPDEV ph in listTypes41)
                {

                    Services.BaseService.Delete<PSPDEV>(ph);
                }
                //电力平衡数据

                ///无功平衡 Ps_Table_WG   InsertPs_Table_WG  SelectPs_Table_WGListByConn
                ///500kv变电容量平衡表 Ps_Table_500PH   InsertPs_Table_500PH   SelectPs_Table_500PHListByConn
                ///220kv变电容量平衡表 Ps_Table_200PH    InsertPs_Table_200PH  SelectPs_Table_200PHListByConn
                ///110kv变电容量平衡表 Ps_Table_100PH    InsertPs_Table_100PH   SelectPs_Table_100PHListByConn
                ///35kv变电容量平衡表 Ps_Table_35PH    InsertPs_Table_35PH   SelectPs_Table_35PHListByConn
                ///电力平衡表         Ps_Table_ElecPH  InsertPs_Table_ElecPH  SelectPs_Table_ElecPHByConn
                ///
                ///
                ModuleDataCopy copy1 = new ModuleDataCopy();
                copy1.delete(uid, "Ps_Table_WG", "InsertPs_Table_WG", "SelectPs_Table_WGListByConn");
                copy1.delete(uid, "Ps_Table_35PH", "InsertPs_Table_35PH", "SelectPs_Table_35PHListByConn");
                copy1.delete(uid, "Ps_Table_100PH", "InsertPs_Table_100PH", "SelectPs_Table_100PHListByConn");
                copy1.delete(uid, "Ps_Table_200PH", "InsertPs_Table_200PH", "SelectPs_Table_200PHListByConn");
                copy1.delete(uid, "Ps_Table_500PH", "InsertPs_Table_500PH", "SelectPs_Table_500PHListByConn");
                copy1.delete(uid, "Ps_Table_ElecPH", "InsertPs_Table_ElecPH", "SelectPs_Table_ElecPHByConn");

                string[] array ={ "地区无功平衡", "35KV电力平衡", "110KV电力平衡", "220KV电力平衡", "500KV电力平衡", "电量平衡" };
                //年份数据
                for (int i = 0; i < array.Length; i++)
                {
                    Ps_YearRange py5 = new Ps_YearRange();
                    py5.Col4 = array[i];
                    py5.Col5 = uid;
                    IList<Ps_YearRange> li5 = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py5);
                    foreach (Ps_YearRange pf in li5)
                    {

                        Services.BaseService.Delete<Ps_YearRange>(pf);
                    }

                }
                //地区维护

                PS_Table_AreaWH ps_ta = new PS_Table_AreaWH();
                ps_ta.ProjectID = uid;
                IList<PS_Table_AreaWH> listTypes6 = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHCopyByProj", ps_ta);
                foreach (PS_Table_AreaWH ph in listTypes6)
                {

                    Services.BaseService.Delete<PS_Table_AreaWH>(ph);
                }

                //电网类型

                PS_Table_Area_TYPE ps_ta7 = new PS_Table_Area_TYPE();
                ps_ta7.ProjectID = uid;
                IList<PS_Table_Area_TYPE> listTypes7 = Services.BaseService.GetList<PS_Table_Area_TYPE>("SelectPS_Table_Area_TYPECopyByProj", ps_ta7);
                foreach (PS_Table_Area_TYPE ph in listTypes7)
                {

                    Services.BaseService.Delete<PS_Table_Area_TYPE>(ph);
                }

                // 负荷预测

                //方案
                Ps_forecast_list report = new Ps_forecast_list();
                report.UserID = uid;  //SetCfgValue("lastLoginUserNumber", Application.ExecutablePath + ".config");
                report.Col1 = "1";
                IList<Ps_forecast_list> ilist9 = Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByCOL1AndUserID", report);
                foreach (Ps_forecast_list ph in ilist9)
                {
                    string struid = ph.ID;

                    Services.BaseService.Delete<Ps_forecast_list>(ph);
                    //数据
                    IList<Ps_Forecast_Math> ilisttemp = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", "ForecastID='" + struid + "'");
                    foreach (Ps_Forecast_Math pf in ilisttemp)
                    {
                        Services.BaseService.Delete<Ps_Forecast_Math>(pf);

                    }


                }

                //供电企业明细

                Ps_Table_Enterprise ps_ta9 = new Ps_Table_Enterprise();
                ps_ta9.ProjectID = uid;
                IList<Ps_Table_Enterprise> listTypes9 = Services.BaseService.GetList<Ps_Table_Enterprise>("SelectPs_Table_EnterpriseCopyByProj", ps_ta9);
                foreach (Ps_Table_Enterprise ph in listTypes9)
                {

                    Services.BaseService.Delete<Ps_Table_Enterprise>(ph);
                }
                ////典型、最大日负荷数据
                IList<BurdenLine> bdlist = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByWhere", " uid like '%" + uid + "%'  order by BurdenDate");
                foreach (BurdenLine pf in bdlist)
                {

                    Services.BaseService.Delete<BurdenLine>(pf);
                }
                ////月最大负荷数据
                IList<BurdenMonth> bmlist = Services.BaseService.GetList<BurdenMonth>("SelectBurdenMonthByWhere", " uid like '%" + uid + "%'  order by BurdenYear");
                foreach (BurdenMonth pf in bmlist)
                {

                    Services.BaseService.Delete<BurdenMonth>(pf);
                }
                           

            }
            catch (Exception ee)
            {
                System.Console.WriteLine(ee.Message);
            }
           
        }
    }
}