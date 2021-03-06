using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet320
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
            //用平时传递电压的方式传递列标题内容
            List<string> DY_list = new List<string>();
            DY_list.Add("干线");
            DY_list.Add("支线");
            DY_list.Add("进户线");
            int startrow = 2;
            int length = 3;
            //表格共23 行4 列
            rowcount = 23;
            colcount = 4;
            //工作表第一行的标题
            title = "表3‑20  "+year+"年铜陵市低压线路导线截面一览表";
            //工作表名
            sheetname = "表3-20";
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
            obj_sheet.Columns[3].Width = 120;
            //设定表格行高度
            
            obj_sheet.Rows[1].Height = 20;

            //写标题行列内容
            Sheet_AddItem(obj_sheet, SxXjName, DY_list);
            //写入数据
            //Sheet_AddData(obj_sheet, year, ProjID, SxXjName, DY_list);
            //写入公式
            //县级合计公式
            fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + 2 * length, 3, 4, length, startrow + length, 3, 1);
            //全地区合计公式
            fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow ,3, 2, length, startrow + 6*length, 3, 1);
            //锁定表格
            fc.Sheet_Locked(obj_sheet);
            //设定格式
            CellType(obj_sheet);
        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet, List<string[]> SxXjName, IList<string> obj_DY_List)
        {
            //写标题行内容

            //2行标题内容
            obj_sheet.SetValue(1, 0, "编号");
            obj_sheet.SetValue(1, 1, "类型");
            obj_sheet.SetValue(1, 2, "低压线路类型");
            obj_sheet.SetValue(1, 3, "导线截面（mm2）");
            //写标题列内容

            int startrow = 2;
            //添加列标题内容
            int dylength = obj_DY_List.Count;
            if (obj_DY_List.Count > 0)
            {
                for (int i = 0; i < SxXjName.Count; i++)
                {
                    for (int j = 0; j < obj_DY_List.Count; j++)
                    {
                        obj_sheet.SetValue(startrow + i * dylength + j, 2, obj_DY_List[j].ToString());
                    }
                    obj_sheet.AddSpanCell(startrow + i * dylength, 0, dylength, 1);
                    obj_sheet.SetValue(startrow + i * dylength, 0, SxXjName[i][0].ToString());
                    obj_sheet.AddSpanCell(startrow + i * dylength, 1, dylength, 1);
                    obj_sheet.SetValue(startrow + i * dylength, 1, SxXjName[i][1].ToString());
                }
            }
            
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, List<string[]> SxXjName, IList<double> obj_DY_List)
        {
          
        }
        public void CellType(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int startrow=2;
            int length=3;
            for (int i = 0; i < 6; i++)
            {
                if (i==1)
                {
                    i++;
                }
                for (int j = 0; j < 3; j++)
			    {
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, startrow + i * length+j, 3);
			    }
                
                
            }
        }
    }
}
