using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet318
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
           
            ////电压等级根据实际情况查询(中压 6 <= 电压<=20)
            //string XLDianYatiaojian = " (Type='55' or Type='56' or Type='57' or Type='58') and OperationYear='" + year + "' and ProjectID='" + ProjID + "' and LineType2='公用' and RateVolt  between 6 and 20 group by RateVolt";
            //IList<double> DY_list = Services.BaseService.GetList<double>("SelectPSPDEV_RateVolt", XLDianYatiaojian);
            //int xlsum = DY_list.Count;
            ////表标题行数
            List<double> DY_list = new List<double>();
            int startrow = 3;
            ////列标题每项行数
            int dylength = 1;

            //表格共 行7 列
            rowcount = startrow + 7 * dylength;
            colcount = 7;
            //工作表第一行的标题
            title = "表3‑18  "+year+"年铜陵市中低压配电网无功补偿统计";
            //工作表名
            sheetname = "表3-18";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 110;
            obj_sheet.Columns[2].Width = 110;
            obj_sheet.Columns[3].Width = 110;
            obj_sheet.Columns[4].Width = 110;
            obj_sheet.Columns[5].Width = 110;
            obj_sheet.Columns[6].Width = 110;
            //设定表格行高度
            
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 40;

            //写标题行列内容
            Sheet_AddItem(obj_sheet, SxXjName, DY_list);
            //写入数据
            //Sheet_AddData(obj_sheet, year, ProjID, SxXjName, DY_list);
            //写入公式
            fc.Sheet_WriteFormula_RowSum(obj_sheet, 5, 2, 4, 1, 4, 2, 4);
            fc.Sheet_WriteFormula_RowSum(obj_sheet, 3, 2, 2, 1, 9, 2, 4);
            fc.Sheet_WriteFormula_OneCol_TwoCol_Threecol_sum(obj_sheet, 3, 3, 5, 6, 7);
            //锁定表格
            fc.Sheet_Locked(obj_sheet);
            //限制格式
            CellType(obj_sheet);
        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet, List<string[]> SxXjName, IList<double> obj_DY_List)
        {
           //写标题行内容
            //2行标题内容
            obj_sheet.AddSpanCell(1,0,2,1);
            obj_sheet.SetValue(1,0,"编号");
            obj_sheet.AddSpanCell(1,1,2,1);
            obj_sheet.SetValue(1,1,"类型");
            obj_sheet.AddSpanCell(1,2,1,2);
            obj_sheet.SetValue(1,2,"配变低压侧补偿");
            obj_sheet.AddSpanCell(1,4,1,2);
            obj_sheet.SetValue(1,4,"中压线路补偿");
            obj_sheet.AddSpanCell(1,6,2,1);
            obj_sheet.SetValue(1,6,"无功补偿总容量（Mvar）");

            //3行标题内容
            obj_sheet.SetValue(2,2,"配变总台数（台）");
            obj_sheet.SetValue(2,3,"无功补偿容量（Mvar）");
            obj_sheet.SetValue(2,4,"线路条数（条）");
            obj_sheet.SetValue(2,5,"无功补偿容量（Mvar）");
            //写标题列内容

            //1列标题内容
            obj_sheet.SetValue(3,0,"1");
            obj_sheet.SetValue(4,0,"2");
            obj_sheet.SetValue(5,0,"2.1");
            obj_sheet.SetValue(6,0,"2.2");
            obj_sheet.SetValue(7,0,"2.3");
            obj_sheet.SetValue(8,0,"2.4");
            obj_sheet.SetValue(9,0,"3");

            //2列标题内容
            obj_sheet.SetValue(3,1,"市辖供电区");
            obj_sheet.SetValue(4,1,"县级供电区");
            obj_sheet.SetValue(5,1,"其中：直供直管");
            obj_sheet.SetValue(6,1,"控股");
            obj_sheet.SetValue(7,1,"参股");
            obj_sheet.SetValue(8,1,"代管");
            obj_sheet.SetValue(9,1,"全地区");
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, List<string[]> SxXjName, IList<double> obj_DY_List)
        {
          
        }
        public void CellType(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            for (int row = 3; row < 9; row++)
            {
                if (row==4)
                {
                    row++;
                }
                for (int col = 2; col < 6; col++)
                {
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, col);
                }
            }
        }
    }
}
