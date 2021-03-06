using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet32
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
            //表格共9 行14 列
            rowcount = 9;
            colcount = 14;
            //工作表第一行的标题
            title = "表3‑2  "+year+"年铜陵市供电企业概况（单位：km2，万人，亿kWh，%）";
            //工作表名
            sheetname = "表3-2";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 100;
            obj_sheet.Columns[2].Width = 60;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 60;
            obj_sheet.Columns[5].Width = 60;
            obj_sheet.Columns[6].Width = 70;
            obj_sheet.Columns[7].Width = 90;
            obj_sheet.Columns[8].Width = 90;
            obj_sheet.Columns[9].Width = 60;
            obj_sheet.Columns[10].Width = 90;
            obj_sheet.Columns[11].Width = 60;
            obj_sheet.Columns[12].Width = 90;
            obj_sheet.Columns[13].Width = 60;
            //设定表格行高度
            
            obj_sheet.Rows[1].Height = 50;
            //写行列标题内容
            Sheet_AddItem(obj_sheet);
            //写入数据
            Sheet_AddData(obj_sheet, year, ProjID, SxXjName);
            //写入公式
            fc.Sheet_WriteFormula_RowSum(obj_sheet, 4, 2, 4, 1, 3,2, 4);
            fc.Sheet_WriteFormula_RowSum(obj_sheet, 4, 11, 2, 1, 3, 11, 2);

            fc.Sheet_WriteFormula_RowSum(obj_sheet, 2, 2, 2, 1, 8, 2, 4);
            fc.Sheet_WriteFormula_RowSum(obj_sheet, 2, 11, 2, 1, 8, 11, 2);

            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, 2, 11, 12, 13, 7);
            //锁定表格
            fc.Sheet_Locked(obj_sheet); 
            //限定格式
            CellType(obj_sheet);
        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet )
        {

            //2行标题内容
            obj_sheet.SetValue(1, 0, "编号");
            obj_sheet.SetValue(1, 1, "类型");
            obj_sheet.SetValue(1, 2, "供电企业个数（个）");
            obj_sheet.SetValue(1, 3, "供电面积(km2)");
            obj_sheet.SetValue(1, 4, "供电人口(万人)");
            obj_sheet.SetValue(1, 5, "售电量(亿kWh)");
            obj_sheet.SetValue(1, 6, "供电可靠率（RS-3）");
            obj_sheet.SetValue(1, 7, "110kV及以下综合线损率（%）");
            obj_sheet.SetValue(1, 8, "10kV及以下综合线损率（%）");
            obj_sheet.SetValue(1, 9, "低压线损率（%）");
            obj_sheet.SetValue(1, 10, "综合电压合格率（%）");
            obj_sheet.SetValue(1, 11, "供电总户数（户）");
            obj_sheet.SetValue(1, 12, "实现一户一表的总户数（户）");
            obj_sheet.SetValue(1, 13, "一户一表率（%）");
            //写标题列内容
            //1列标题内容
            obj_sheet.SetValue(2, 0, "1");
            obj_sheet.SetValue(3, 0, "2");
            obj_sheet.SetValue(4, 0, "2.1");
            obj_sheet.SetValue(5, 0, "2.2");
            obj_sheet.SetValue(6, 0, "2.3");
            obj_sheet.SetValue(7, 0, "2.4");
            obj_sheet.SetValue(8, 0, "3");

            //2列标题内容
            obj_sheet.SetValue(2, 1, "市辖区企业");
            obj_sheet.SetValue(3, 1, "县级企业");
            obj_sheet.SetValue(4, 1, "其中：直供直管");
            obj_sheet.SetValue(5, 1, "控股");
            obj_sheet.SetValue(6, 1, "参股");
            obj_sheet.SetValue(7, 1, "代管");
            obj_sheet.SetValue(8, 1, "全地区");

        }
        public void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, List<string[]> SxXjName)
        {
            string tiaojian = "";
            for (int i = 0; i < SxXjName.Count; i++)
            {
                //合计部分不用计算
                if (SxXjName[i][2].ToString() != "合计")
                {
                    tiaojian = " CAST(col1 as int)<=" + year + "  and ProjectID='" + ProjID + "'  and SType='" + SxXjName[i][2].ToString() + "'";
                    int QYsum = 0;
                    if (Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", tiaojian) != null)
                    {
                        QYsum = (int)Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", tiaojian);
                    }
                    obj_sheet.SetValue(2+i, 2, QYsum);
                }
                
            }  
        }
        public void CellType(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            for (int row = 2; row < 8; row++)
            {
                if (row!=3)
                {
                    for (int col = 3; col < 6; col++)
                    {
                        fc.Sheet_UnLockedandCellNumber(obj_sheet, row, col);
                    }
                    for (int col = 11; col < 13; col++)
                    {
                        fc.Sheet_UnLockedandCellNumber(obj_sheet, row, col);
                    }
                }
                for (int col = 6; col < 11; col++)
                {
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, col);
                    if (col!=6)
                    {
                        obj_sheet.Cells[row, col].CellType = new FarPoint.Win.Spread.CellType.PercentCellType();
                    }
                    
                }
            }
        }
    }
}
