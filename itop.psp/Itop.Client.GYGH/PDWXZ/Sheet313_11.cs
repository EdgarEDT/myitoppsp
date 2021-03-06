using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
using System.Collections;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet313_11
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
            //线路电压等级根据实际情况查询(中压 6 <= 电压<=20)
            string XLDianYatiaojian = " (b.Type='50' or b.Type='51' or b.Type='52') and  b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and a.LineType2='公用' and b.RateVolt  between 6 and 20 group by b.RateVolt";
            IList<double> DY_list = Services.BaseService.GetList<double>("SelectPSPDEV_RateVolt_type50-59", XLDianYatiaojian);
            int xlsum = DY_list.Count;
            //计算市辖供电区条件 
            string SXtiaojianareaid = " (b.Type='50' or b.Type='51' or b.Type='52')  and  b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and a.AreaID!='' and  b.ProjectID='" + ProjID + "' and a.LineType2='公用' and a.DQ='市辖供电区' and  b.RateVolt between 6 and 20  group by a.AreaID";
            //计算县级供电区条件
            string XJtiaojianareaid = " (b.Type='50' or b.Type='51' or b.Type='52')  and  b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and a.AreaID!='' and  b.ProjectID='" + ProjID + "' and a.LineType2='公用' and a.DQ!='市辖供电区' and  b.RateVolt between 6 and 20  group by a.AreaID";
            //存放市辖供电区分区名
            IList<string> SXareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_AreaID_type50-59", SXtiaojianareaid);
            //存放县级供电区分区名
            IList<string> XJareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_AreaID_type50-59", XJtiaojianareaid);

            //市辖供电区分区个数
            int SXsum = SXareaid_List.Count;
            //县级供电区分区个数
            int XJsum = XJareaid_List.Count;
            //表标题行数
            int startrow = 2;
            //列标题每项是压等级数
            int length = xlsum;
            //表格共 行6 列
            rowcount = startrow + (SXsum + XJsum + 2) * length;
            colcount = 6;
            //工作表第一行的标题
            title = "附表11  铜陵市中压配电网配电设备统计（ " + year + "年）";
            //工作表名
            sheetname = "表3-13 附表11";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 90;
            obj_sheet.Columns[1].Width = 100;
            obj_sheet.Columns[2].Width = 80;
            obj_sheet.Columns[3].Width = 90;
            obj_sheet.Columns[4].Width = 90;
            obj_sheet.Columns[5].Width = 90;
            //设定表格行高度
            
            obj_sheet.Rows[1].Height = 40;
            //写标题行列内容
            Sheet_AddItem(obj_sheet, area_key_id, DY_list, SXareaid_List, XJareaid_List);
            //写入数据
            Sheet_AddData(obj_sheet,year,ProjID, DY_list, SXareaid_List, XJareaid_List);
            //写入公式
            
          
            //锁定表格
            fc.Sheet_Locked(obj_sheet);

        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet,Hashtable area_key_id, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            //2行标题内容
            obj_sheet.SetValue(1, 0, "分区类型");
            obj_sheet.SetValue(1, 1, "分区名称");
            obj_sheet.SetValue(1, 2, "电压等级（kV）");
            obj_sheet.SetValue(1, 3, "配电室（座）");
            obj_sheet.SetValue(1, 4, "箱变（座）");
            obj_sheet.SetValue(1, 5, "柱上变（台）");

            //写标题列内容
            fc.Sheet_AddItem_FBonlyDY(obj_sheet, area_key_id, 2, obj_DY_List, SXareaid_List, XJareaid_List);
  
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            //条件
            string tiaojian = "";
            //DQ条件
            string DQtiaojian = "";
            int dylength = obj_DY_List.Count;
            int colcount = 3;
            int startrow = 2;
            //int length = 1;

            for (int i = 0; i < (2 + SXareaid_List.Count + XJareaid_List.Count); i++)
            {
                string areaid = "";
                if (i != 0 && i != (SXareaid_List.Count + 1))
                {
                    if (i < SXareaid_List.Count + 1)
                    {
                        DQtiaojian = " a.DQ='市辖供电区'";
                        areaid = SXareaid_List[i - 1].ToString();
                    }
                    else
                    {
                        DQtiaojian = " a.DQ!='市辖供电区'";
                        areaid = XJareaid_List[i - SXareaid_List.Count - 2].ToString();
                    }

                    for (int j = 0; j < obj_DY_List.Count; j++)
                    {
                        //配电室座数
                        tiaojian = " b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and ProjectID='" + ProjID + "' and Type='50' and LineType2='公用'  and " + DQtiaojian + " and RateVolt=10 and AreaID='" + areaid + "'";

                        //配电室座数
                        tiaojian = " b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and b.Type='50' and a.LineType2='公用'  and " + DQtiaojian + " and a.AreaID='" + areaid + "' and b.RateVolt=" + obj_DY_List[j];

                        int PDsum = 0;
                        if (Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian) != null)
                        {
                            PDsum = (int)Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian);
                        }
                        obj_sheet.SetValue(startrow + i * dylength + j, 3, PDsum);

                        //箱变座数
                        tiaojian = " b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and b.Type='51' and a.LineType2='公用'  and " + DQtiaojian + " and a.AreaID='" + areaid + "' and b.RateVolt=" + obj_DY_List[j];
                        int XBsum = 0;
                        if (Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian) != null)
                        {
                            XBsum = (int)Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian);
                        }
                        obj_sheet.SetValue(startrow + i * dylength + j, 4, XBsum);

                        //柱上变（台）
                        tiaojian = " b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and b.Type='52' and a.LineType2='公用'  and " + DQtiaojian + " and a.AreaID='" + areaid + "' and b.RateVolt=" + obj_DY_List[j];
                        int ZBsum = 0;
                        if (Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian) != null)
                        {
                            ZBsum = (int)Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian);
                        }
                        obj_sheet.SetValue(startrow + i * dylength + j, 5, ZBsum);
                    }
                    
                }

                else
                {
                    //市辖合计部分公式
                    if (i == 0)
                    {
                        if (SXareaid_List.Count != 0)
                        {
                            fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * dylength, 3, SXareaid_List.Count, dylength, startrow + i * dylength, 3, colcount);
                        }
                        //无地区直接在合计部分写0
                        else
                        {
                            fc.Sheet_WriteZero(obj_sheet, startrow + i * dylength, 3, dylength, colcount);
                        }

                    }
                    //县级合计部分公式
                    else
                    {
                        if (XJareaid_List.Count != 0)
                        {
                            fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * dylength, 3, XJareaid_List.Count, dylength, startrow + i * dylength, 3, colcount);

                        }
                        //无地区直接在合计部分写0
                        else
                        {
                            fc.Sheet_WriteZero(obj_sheet, startrow + i * dylength, 3, dylength, colcount);
                        }
                    }
                }
            }
            
        }
    }
}
