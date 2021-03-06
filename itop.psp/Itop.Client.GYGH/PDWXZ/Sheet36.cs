using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet36
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
            //表格共14 行4 列
            rowcount = 14;
            colcount = 4;
            //工作表第一行的标题
            title = "表3‑6 " + year + "年铜陵市高压配电网结构情况";
            //工作表名
            sheetname = "表3-6";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 80;
            obj_sheet.Columns[1].Width = 160;
            obj_sheet.Columns[2].Width = 150;
            obj_sheet.Columns[3].Width = 150;
            //写行列标题
            Sheet_AddItem(obj_sheet);
            //写入数据
            Sheet_AddData(obj_sheet,year,ProjID);
            //写入公式
            //求和公式
            fc.Sheet_WriteFormula_RowSum(obj_sheet, 2, 2, 5, 2, 12, 2, 2);
            //分项求比例公式
            fc.Sheet_WriteFormula_Percent_Row(obj_sheet, 2, 2, 1, 3, 2, 5, 2, 12);
            //总和求比例公式
            fc.Sheet_WriteFormula_Percent_Row(obj_sheet, 12, 2, 0, 13, 2, 1, 2, 12);
            //锁定表格
            fc.Sheet_Locked(obj_sheet);
        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //写标题行内容

            //2行标题内容
            obj_sheet.SetValue(1, 0, "结构类型");
            obj_sheet.SetValue(1, 1, "项目");
            obj_sheet.SetValue(1, 2, "110kV高压配电网");
            obj_sheet.SetValue(1, 3, "35kV高压配电网");
            //写标题列内容

            //1列标题内容
            obj_sheet.AddSpanCell(2, 0, 2, 1);
            obj_sheet.SetValue(2, 0, "环网");
            obj_sheet.AddSpanCell(4, 0, 2, 1);
            obj_sheet.SetValue(4, 0, "放射型");
            obj_sheet.AddSpanCell(6, 0, 2, 1);
            obj_sheet.SetValue(6, 0, "链式");
            obj_sheet.AddSpanCell(8, 0, 2, 1);
            obj_sheet.SetValue(8, 0, "T型");
            obj_sheet.AddSpanCell(10, 0, 2, 1);
            obj_sheet.SetValue(10, 0, "其它");
            obj_sheet.AddSpanCell(12, 0, 2, 1);
            obj_sheet.SetValue(12, 0, "合计");

            //2列标题内容
            obj_sheet.SetValue(2, 1, "该结构中线路条数（条）");
            obj_sheet.SetValue(3, 1, "线路条数所占比例（%）");
            obj_sheet.SetValue(4, 1, "该结构中线路条数（条）");
            obj_sheet.SetValue(5, 1, "线路条数所占比例（%）");
            obj_sheet.SetValue(6, 1, "该结构中线路条数（条）");
            obj_sheet.SetValue(7, 1, "线路条数所占比例（%）");
            obj_sheet.SetValue(8, 1, "该结构中线路条数（条）");
            obj_sheet.SetValue(9, 1, "线路条数所占比例（%）");
            obj_sheet.SetValue(10, 1, "该结构中线路条数（条）");
            obj_sheet.SetValue(11, 1, "线路条数所占比例（%）");
            obj_sheet.SetValue(12, 1, "线路总条数（条）");
            obj_sheet.SetValue(13, 1, "线路条数所占比例（%）");
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID)
        {
            //对于线路信息，OperationYear中存放的投产年份,Type='05'表示线路信息
            //LineType2存放的公用专用标识,RateVolt存放电压,为float类型
            //JXFS为接线方式,如环网、链式等
            string XLtiaojian = "";
            string[] JGType = { "环网", "放射型", "链式", "T型", "其它" };
            string JGLX = "";
            string DianYa = "";
            for (int row = 2; row < 11; row++)
            {
                for (int col = 2; col <= 3; col++)
                {
                    JGLX = JGType[(row / 2) - 1];
                    if (col == 2)
                    {
                        DianYa = "110";
                    }
                    else
                    {
                        DianYa = "35";
                    }
                    XLtiaojian = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + " and  Type='05' and  LineType2='公用' and RateVolt=" + DianYa + " and ProjectID='" + ProjID + "' and JXFS='" + JGLX + "'";
                    int XLsum = 0;
                    if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian) != null)
                    {
                        XLsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian);
                    }
                    obj_sheet.SetValue(row, col, XLsum);
                }
                row++;
            }
        }

    }
}
