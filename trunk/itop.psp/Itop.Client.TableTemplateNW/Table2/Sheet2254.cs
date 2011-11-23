//********************************************************************************/
//
//此代码由BuildSheetFromExcel代码生成器自动生成.
//生成时间:2011-05-19 8:55:44
//
//********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Itop.Domain.Table;
using Itop.Client.Common;
using Itop.Domain.PWTable;
namespace Itop.Client.TableTemplateNW
{
    class Sheet2254
    {
        //生成公共类的实体
        Tcommon TC = new Tcommon();
        //当前卷编号
        string ProjectID = Tcommon.ProjectID;
        //工作表行数
        int rowcount = 0;
        //工作表列数据
        int colcount = 0;
        //工作表第一行的表名
        string title = "";
        //工作表标签名
        string sheetname = "";
        public void Build_Sheet(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //表格共9 行11 列
            rowcount = 9;
            colcount = 11;
            //工作表第一行的标题
            title = TC.GetTableTitle(this.GetType().Name);
            //工作表有关年份
            //int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            //工作表名
            sheetname = title;
            //设定工作表行列值及标题和表名
            TC.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定行列模式，以便写公式使用
            TC.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 100;
            obj_sheet.Columns[2].Width = 60;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 60;
            obj_sheet.Columns[5].Width = 60;
            obj_sheet.Columns[6].Width = 60;
            obj_sheet.Columns[7].Width = 60;
            obj_sheet.Columns[8].Width = 60;
            obj_sheet.Columns[9].Width = 60;
            obj_sheet.Columns[10].Width = 60;
            //设定表格行高度
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
            //写标题行内容

            //2行标题内容
            obj_sheet.AddSpanCell(1, 0, 2, 1);
            obj_sheet.SetValue(1, 0, "分区");
            obj_sheet.SetValue(1, 1, "装接配变容量");
            obj_sheet.AddSpanCell(1, 2, 2, 1);
            obj_sheet.SetValue(1, 2, "<2");
            obj_sheet.AddSpanCell(1, 3, 2, 1);
            obj_sheet.SetValue(1, 3, "2~4");
            obj_sheet.AddSpanCell(1, 4, 2, 1);
            obj_sheet.SetValue(1, 4, "4~6");
            obj_sheet.AddSpanCell(1, 5, 2, 1);
            obj_sheet.SetValue(1, 5, "6~8");
            obj_sheet.AddSpanCell(1, 6, 2, 1);
            obj_sheet.SetValue(1, 6, "8~10");
            obj_sheet.AddSpanCell(1, 7, 2, 1);
            obj_sheet.SetValue(1, 7, "10~12");
            obj_sheet.AddSpanCell(1, 8, 2, 1);
            obj_sheet.SetValue(1, 8, "12~15");
            obj_sheet.AddSpanCell(1, 9, 2, 1);
            obj_sheet.SetValue(1, 9, ">15");
            obj_sheet.AddSpanCell(1, 10, 2, 1);
            obj_sheet.SetValue(1, 10, "合计");

            //3行标题内容
            obj_sheet.SetValue(2, 1, "（万千伏安）");
            //写标题列内容
            //添加数据
            Sheet_AddData(obj_sheet);

            //设定表格线
            TC.Sheet_GridandCenter(obj_sheet);

            //锁定表格
            TC.Sheet_Locked(obj_sheet);
        }


        //此处为动态添加数据方法
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int startrow = 3;
            int addnum = 0;
            int itemcount = 0;
            int firstrow = 0;
            string rowsum = "";
            string rowsum2 = "";
            Itop.Domain.PWTable.PW_tb3a p = new Itop.Domain.PWTable.PW_tb3a();
            p.col2 = Itop.Client.MIS.ProgUID;
            IList<PW_tb3a> list = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2254", p);

            bool sheetEnd = false;
            for (int i = 0; i < list.Count;i++ )
            {
                
                PW_tb3a obj = list[i];
                PW_tb3a obj2 = new PW_tb3a();
                if (i < list.Count - 1)
                {
                    obj2 = list[i + 1];
                }
                else
                {
                    sheetEnd = true;
                }
                decimal _sum = obj.Num1 + obj.Num2 + obj.Num3 + obj.Num4 + obj.Num5 + obj.Num6 + obj.Num7 + obj.Num8;
                itemcount = itemcount + 1;
                obj_sheet.RowCount = obj_sheet.RowCount + 1;
                obj_sheet.SetValue(i + addnum + startrow, 0, obj.PQName);
                obj_sheet.SetValue(i + addnum + startrow, 1, "线路回数（回）");
                obj_sheet.SetValue(i + addnum + startrow, 2, obj.Num1);
                obj_sheet.SetValue(i + addnum + startrow, 3, obj.Num2);
                obj_sheet.SetValue(i + addnum + startrow, 4, obj.Num3);
                obj_sheet.SetValue(i + addnum + startrow, 5, obj.Num4);
                obj_sheet.SetValue(i + addnum + startrow, 6, obj.Num5);
                obj_sheet.SetValue(i + addnum + startrow, 7, obj.Num6);
                obj_sheet.SetValue(i + addnum + startrow, 8, obj.Num7);
                obj_sheet.SetValue(i + addnum + startrow, 9, obj.Num8);
                obj_sheet.SetValue(i + addnum + startrow, 10, _sum);

                itemcount = itemcount + 1;
                addnum = addnum + 1;
                obj_sheet.RowCount = obj_sheet.RowCount + 1;
                obj_sheet.SetValue(i + addnum + startrow, 0, obj.PQName);
                obj_sheet.SetValue(i + addnum + startrow, 1, "所占比例（%）");
                if (_sum == 0)
                {
                    obj_sheet.SetValue(i + addnum + startrow, 2, "0");
                    obj_sheet.SetValue(i + addnum + startrow, 3, "0");
                    obj_sheet.SetValue(i + addnum + startrow, 4, "0");
                    obj_sheet.SetValue(i + addnum + startrow, 5, "0");
                    obj_sheet.SetValue(i + addnum + startrow, 6, "0");
                    obj_sheet.SetValue(i + addnum + startrow, 7, "0");
                    obj_sheet.SetValue(i + addnum + startrow, 8, "0");
                    obj_sheet.SetValue(i + addnum + startrow, 9, "0");
                    obj_sheet.SetValue(i + addnum + startrow, 10, "0");
                }
                else
                {
                    obj_sheet.SetValue(i + addnum + startrow, 2, Convert.ToDecimal((obj.Num1 / _sum) * 100).ToString("0.##"));
                    obj_sheet.SetValue(i + addnum + startrow, 3, Convert.ToDecimal((obj.Num2 / _sum) * 100).ToString("0.##"));
                    obj_sheet.SetValue(i + addnum + startrow, 4, Convert.ToDecimal((obj.Num3 / _sum) * 100).ToString("0.##"));
                    obj_sheet.SetValue(i + addnum + startrow, 5, Convert.ToDecimal((obj.Num4 / _sum) * 100).ToString("0.##"));
                    obj_sheet.SetValue(i + addnum + startrow, 6, Convert.ToDecimal((obj.Num5 / _sum) * 100).ToString("0.##"));
                    obj_sheet.SetValue(i + addnum + startrow, 7, Convert.ToDecimal((obj.Num6 / _sum) * 100).ToString("0.##"));
                    obj_sheet.SetValue(i + addnum + startrow, 8, Convert.ToDecimal((obj.Num7 / _sum) * 100).ToString("0.##"));
                    obj_sheet.SetValue(i + addnum + startrow, 9, Convert.ToDecimal((obj.Num8 / _sum) * 100).ToString("0.##"));
                    obj_sheet.SetValue(i + addnum + startrow, 10, "100");
                }
                obj_sheet.AddSpanCell(i + addnum + startrow-1, 0, 2, 1);

                rowsum = rowsum + Convert.ToString(startrow-2+2*(i+1)) + ",";
                rowsum2 = rowsum2 + Convert.ToString(startrow - 1 + 2 * (i + 1)) + ",";
                if (sheetEnd)
                {
                    addnum = addnum + 1;
                    obj_sheet.RowCount = obj_sheet.RowCount + 1;
                    obj_sheet.SetValue(i + addnum + startrow, 0, "全市合计");
                    obj_sheet.SetValue(i + addnum + startrow, 1, "线路回数（回）");
                    TC.Sheet_WriteFormula_RowSum3(obj_sheet, TC.getRowList(rowsum), 2, i + addnum + startrow, 2, 9);
                    addnum = addnum + 1;
                    obj_sheet.RowCount = obj_sheet.RowCount + 1;
                    obj_sheet.SetValue(i + addnum + startrow, 0, "全市合计");
                    obj_sheet.SetValue(i + addnum + startrow, 1, "所占比例（%）");
                    //TC.Sheet_WriteFormula_TwoCol_Percent(obj_sheet, i + addnum + startrow - 1, 2, 7, 1, i + addnum + startrow, 2);
                    obj_sheet.SetValue(i + addnum + startrow, 10, "100");
                    obj_sheet.AddSpanCell(i + addnum + startrow-1, 0, 2, 1);
                }
            }
        }

      

    }
}
