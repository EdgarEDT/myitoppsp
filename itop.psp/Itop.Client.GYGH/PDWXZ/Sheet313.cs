using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet313
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
        public void Build(FarPoint.Win.Spread.SheetView obj_sheet,int year,string ProjID,List<string[]> SxXjName)
        {
            //线路电压等级根据实际情况查询(中压 6 <= 电压<=20)
            string XLDianYatiaojian = " (b.Type='50' or b.Type='51' or b.Type='52') and b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and a.LineType2='公用' and b.RateVolt  between 6 and 20 group by b.RateVolt";
            IList<double> DY_list = Services.BaseService.GetList<double>("SelectPSPDEV_RateVolt_type50-59", XLDianYatiaojian);
            int xlsum = DY_list.Count;
            //表标题行数
            int startrow = 2;
            //表格共 行6 列
            rowcount = startrow + 7 * xlsum;
            colcount = 6;
            //工作表第一行的标题
            title = "表3‑13   " + year + "年铜陵市中压配电网配电类设备统计";
            //工作表名
            sheetname = "表3-13";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 100;
            obj_sheet.Columns[2].Width = 80;
            obj_sheet.Columns[3].Width = 90;
            obj_sheet.Columns[4].Width = 90;
            obj_sheet.Columns[5].Width = 90;
            //设定表格行高度
            
            obj_sheet.Rows[1].Height = 40;
            //写标题行列内容
            Sheet_AddItem(obj_sheet, SxXjName, DY_list);
            //写入数据
            Sheet_AddData(obj_sheet, year, ProjID, SxXjName, DY_list);
            //锁定表格
            fc.Sheet_Locked(obj_sheet);
        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet, List<string[]> SxXjName, IList<double> obj_DY_List)
        {
            //写标题行内容

            //2行标题内容
            obj_sheet.SetValue(1, 0, "编号");
            obj_sheet.SetValue(1, 1, "类型");
            obj_sheet.SetValue(1, 2, "电压等级（kV）");
            obj_sheet.SetValue(1, 3, "配电室（座）");
            obj_sheet.SetValue(1, 4, "箱变（座）");
            obj_sheet.SetValue(1, 5, "柱上变（台）");
            //写标题列内容
            fc.Sheet_AddItem_ZBonlyDY(obj_sheet, SxXjName, 2, obj_DY_List);
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, List<string[]> SxXjName, IList<double> obj_DY_List)
        {
            //添加数据
            //条件
            string tiaojian = "";
            int startrow = 2;
            int dylenth = obj_DY_List.Count;
            for (int i = 0; i < SxXjName.Count; i++)
            {
                //合计部分不用计算
                if (SxXjName[i][2].ToString() != "合计")
                {
                    for (int j = 0; j < obj_DY_List.Count; j++)
			        {
                         //配电室座数
                        tiaojian = " b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and b.Type='50' and a.LineType2='公用'  and a.DQ='" + SxXjName[i][2] + "' and b.RateVolt=" + obj_DY_List[j];

                        int PDsum = 0;
                        if (Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian) != null)
                        {
                            PDsum = (int)Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian);
                        }
                        obj_sheet.SetValue(startrow + i * dylenth + j, 3, PDsum);

                        //箱变座数
                        tiaojian = " b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and b.Type='51' and a.LineType2='公用'  and a.DQ='" + SxXjName[i][2] + "' and b.RateVolt=" + obj_DY_List[j];
                        int XBsum = 0;
                        if (Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian) != null)
                        {
                            XBsum = (int)Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian);
                        }
                        obj_sheet.SetValue(startrow + i * dylenth + j, 4, XBsum);

                        //柱上变（台）
                        tiaojian = " b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and b.Type='52' and a.LineType2='公用'  and a.DQ='" + SxXjName[i][2] + "' and b.RateVolt=" + obj_DY_List[j];
                        int ZBsum = 0;
                        if (Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian) != null)
                        {
                            ZBsum = (int)Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian);
                        }
                        obj_sheet.SetValue(startrow + i * dylenth + j, 5, ZBsum);
			        }
                }
                else
                {
                    //县级合计部分公式
                    if (i == 1)
                    {
                        fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * dylenth, 3, 4, dylenth, startrow + i * dylenth, 3, 3);
                    }
                    //全地区合计部分公式
                    else
                    {
                        fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow, 3, 2, dylenth, startrow + i*dylenth, 3, 3);

                    }
                }

            }     
        }

    }
}
