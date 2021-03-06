using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
using System.Collections;
using Itop.Domain.Graphics;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet34_2
    {

        fpcommon fc = new fpcommon();
        //工作表行数
        int rowcount = 0;
        //工作表列数据
        int colcount = 0;
        //工作表第一行的表名
        string title = "";
        //工作表标签名
        string sheetname = "";
        public void Build(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, Hashtable area_key_id, List<string[]> SxXjName)
        {
            //计算市辖供电区条件 
            string SXtiaojianarea = " AreaID='" + ProjID + "' and S9!='' and CAST(S1 as int) between 10 and 110 and CAST(S3 as int ) <=" + year + " and  S5='市辖供电区'  group by S9";
            //计算县级供电区条件
            string XJtiaojianarea = " AreaID='" + ProjID + "' and S9!='' and CAST(S1 as int) between 10 and 110 and CAST(S3 as int ) <=" + year + " and  S5!='市辖供电区'  group by S9";
            //存放市辖供电区分区名
            IList<string> SXareaid_List = Services.BaseService.GetList<string>("SelectPSP_PowerSubstation_Info_GroupS9", SXtiaojianarea);
            //存放县级供电区分区名
            IList<string> XJareaid_List = Services.BaseService.GetList<string>("SelectPSP_PowerSubstation_Info_GroupS9", XJtiaojianarea);

            //计算市辖电区电厂个数条件 
            string SXtiaojian = " AreaID='" + ProjID + "' and S9!='' and CAST(S1 as int) between 10 and 110 and CAST(S3 as int ) <=" + year + " and  S5='市辖供电区'";
            //计算县级供电区电厂个数条件
            string XJtiaojian = " AreaID='" + ProjID + "' and S9!='' and CAST(S1 as int) between 10 and 110 and CAST(S3 as int ) <=" + year + " and  S5!='市辖供电区'";
            //市辖供电区电厂个数
            int SXsum = (int)Services.BaseService.GetObject("SelectPSP_PowerSubstation_InfoCountByObject", SXtiaojian);
            //县级供电区电厂个数
            int XJsum = (int)Services.BaseService.GetObject("SelectPSP_PowerSubstation_InfoCountByObject", XJtiaojian);
            //表标题行数
            int startrow = 2;

            //表格共10 行10 列
            rowcount = startrow+SXsum+XJsum+2;
            colcount = 10;
            //工作表第一行的标题
            title = "附表2  铜陵市电源装机容量及发电量情况（"+year+"年）（发策部、生技部）";
            //工作表名
            sheetname = "表3-4 附表2";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 90;
            obj_sheet.Columns[1].Width = 120;
            obj_sheet.Columns[2].Width = 120;
            obj_sheet.Columns[3].Width = 90;
            obj_sheet.Columns[4].Width = 70;
            obj_sheet.Columns[5].Width = 60;
            obj_sheet.Columns[6].Width = 60;
            obj_sheet.Columns[7].Width = 90;
            obj_sheet.Columns[8].Width = 60;
            obj_sheet.Columns[9].Width = 60;
            //设定表格行高度
            
            obj_sheet.Rows[1].Height = 40;
            //写标题行列内容
            Sheet_AddItem(obj_sheet, SXareaid_List, XJareaid_List);
            //写入数据
            Sheet_AddData(obj_sheet, year, ProjID, SXareaid_List, XJareaid_List);

            //锁定表格
            fc.Sheet_Locked(obj_sheet);

        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            //2行标题内容
            obj_sheet.SetValue(1, 0, "分区类型");
            obj_sheet.SetValue(1, 1, "分区名称");
            obj_sheet.SetValue(1, 2, "电厂名称");
            obj_sheet.SetValue(1, 3, "电厂类型");
            obj_sheet.SetValue(1, 4, "并网电压等级(kV)");
            obj_sheet.SetValue(1, 5, "装机容量(MW)");
            obj_sheet.SetValue(1, 6, "发电量(亿kWh)");
            obj_sheet.SetValue(1, 7, "发电利用小时数(小时)");
            obj_sheet.SetValue(1, 8, "厂用电(万kWh)");
            obj_sheet.SetValue(1, 9, "统调(是/否)");
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            int startrow = 2;
            string tiaojian = "";
            string areaname = "";
            int xjrow = 0;
            int nowrow=startrow;
            obj_sheet.SetValue(nowrow, 1, "合计");
            nowrow += 1;
            for (int i = 0; i < SXareaid_List.Count; i++)
            {
                areaname = SXareaid_List[i].ToString();
                tiaojian = " AreaID='" + ProjID + "' and CAST(S1 as int) between 10 and 110 and CAST(S3 as int ) <=" + year + " and  S5='市辖供电区' and  S9='" + areaname + "'";
                IList<PSP_PowerSubstation_Info> GDClist = Services.BaseService.GetList<PSP_PowerSubstation_Info>("SelectPSP_PowerSubstation_InfoListByWhere", tiaojian);
                obj_sheet.AddSpanCell(nowrow, 1, GDClist.Count, 1);
                obj_sheet.SetValue(nowrow, 1, areaname);
                for (int j = 0; j < GDClist.Count; j++)
                {
                    //电厂名称
                    obj_sheet.SetValue(nowrow,2,GDClist[j].Title);
                    //电厂类型
                    obj_sheet.SetValue(nowrow, 3, GDClist[j].S10);
                    //并网电压等级(kV)
                    obj_sheet.SetValue(nowrow, 4, GDClist[j].S1);
                    //装机容量(MW)
                    double ZJRL = 0;
                    if (GDClist[j].S2 != "")
                    {
                        ZJRL = double.Parse(GDClist[j].S2);
                    }
                    obj_sheet.SetValue(nowrow, 5, ZJRL);
                    //发电量(亿kWh)
                    double FDL = 0;
                    if (GDClist[j].S11 != "")
                    {
                        FDL = double.Parse(GDClist[j].S11);
                    }
                    obj_sheet.SetValue(nowrow, 6, FDL);
                    //发电利用小时数(小时)
                    double FDLYXS = 0;
                    if (GDClist[j].S12 != "")
                    {
                        FDLYXS = double.Parse(GDClist[j].S12);
                    }
                    obj_sheet.SetValue(nowrow, 7, FDLYXS);
                    //厂用电(万kWh)
                    double CYD = 0;
                    if (GDClist[j].S13 != "")
                    {
                        CYD = double.Parse(GDClist[j].S13);
                    }
                    obj_sheet.SetValue(nowrow, 8, CYD);
                    //统调(是/否)
                    obj_sheet.SetValue(nowrow, 9, GDClist[j].S14);
                    nowrow += 1;
                }
                
            }
            //写入市辖合计公式
            if (SXareaid_List.Count>0)
            {
                fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow+1, 5, nowrow - startrow-1, 1, startrow, 5, 4);
            }
            obj_sheet.AddSpanCell(startrow, 0,nowrow-startrow, 1);
            obj_sheet.SetValue(startrow, 0, "市辖供电区");
            xjrow = nowrow; ;
            obj_sheet.SetValue(nowrow, 1, "合计");
            nowrow += 1;
            for (int i = 0; i < XJareaid_List.Count; i++)
            {
                areaname = XJareaid_List[i].ToString();
                tiaojian = " AreaID='" + ProjID + "' and CAST(S1 as int) between 10 and 110 and  CAST(S3 as int ) <=" + year + " and  S5!='市辖供电区' and  S9='" + areaname + "'";
                IList<PSP_PowerSubstation_Info> GDClist = Services.BaseService.GetList<PSP_PowerSubstation_Info>("SelectPSP_PowerSubstation_InfoListByWhere", tiaojian);
                obj_sheet.AddSpanCell(nowrow, 1, GDClist.Count, 1);
                obj_sheet.SetValue(nowrow, 1, areaname);
                for (int j = 0; j < GDClist.Count; j++)
                {
                    //电厂名称
                    obj_sheet.SetValue(nowrow, 2, GDClist[j].Title);
                    //电厂类型
                    obj_sheet.SetValue(nowrow, 3, GDClist[j].S10);
                    //并网电压等级(kV)
                    obj_sheet.SetValue(nowrow, 4, GDClist[j].S1);
                    //装机容量(MW)
                    double ZJRL = 0;
                    if (GDClist[j].S2!="")
                    {
                        ZJRL = double.Parse(GDClist[j].S2);
                    }
                    obj_sheet.SetValue(nowrow, 5, ZJRL);
                    //发电量(亿kWh)
                    double FDL = 0;
                    if (GDClist[j].S11 != "")
                    {
                        FDL = double.Parse(GDClist[j].S11);
                    }
                    obj_sheet.SetValue(nowrow, 6, FDL);
                    //发电利用小时数(小时)
                    double FDLYXS = 0;
                    if (GDClist[j].S12 != "")
                    {
                        FDLYXS = double.Parse(GDClist[j].S12);
                    }
                    obj_sheet.SetValue(nowrow, 7, FDLYXS);
                    //厂用电(万kWh)
                    double CYD = 0;
                    if (GDClist[j].S13 != "")
                    {
                        CYD = double.Parse(GDClist[j].S13);
                    }
                    obj_sheet.SetValue(nowrow, 8, CYD);
                    //统调(是/否)
                    obj_sheet.SetValue(nowrow, 9, GDClist[j].S14);
                    nowrow += 1;
                }
                
            }
            if (XJareaid_List.Count>0)
	        {
                 fc.Sheet_WriteFormula_RowSum(obj_sheet, xjrow + 1, 5, nowrow - xjrow-1, 1, xjrow, 5, 4);
	        }
            obj_sheet.AddSpanCell(xjrow, 0, nowrow - xjrow + 1, 1);
            obj_sheet.SetValue(xjrow, 0, "县级供电区");

            
        }
    }
}
