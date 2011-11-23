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
    class Sheet2251
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
            //表格共12 行11 列
            rowcount = 12;
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
            obj_sheet.Columns[0].Width = 80;
            obj_sheet.Columns[1].Width = 80;
            obj_sheet.Columns[2].Width = 80;
            obj_sheet.Columns[3].Width = 80;
            obj_sheet.Columns[4].Width = 80;
            obj_sheet.Columns[5].Width = 80;
            obj_sheet.Columns[6].Width = 80;
            obj_sheet.Columns[7].Width = 80;
            obj_sheet.Columns[8].Width = 80;
            obj_sheet.Columns[9].Width = 80;
            obj_sheet.Columns[10].Width = 80;
            //设定表格行高度
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
            //写标题行内容

            //2行标题内容
            obj_sheet.AddSpanCell(1, 0, 2, 1);
            obj_sheet.SetValue(1, 0, "分区");
            obj_sheet.AddSpanCell(1, 1, 2, 1);
            obj_sheet.SetValue(1, 1, "供电区分类");
            obj_sheet.AddSpanCell(1, 2, 1, 7);
            obj_sheet.SetValue(1, 2, "公用馈线");
            obj_sheet.SetValue(1, 9, "专用馈线");
            obj_sheet.AddSpanCell(1, 10, 2, 1);
            obj_sheet.SetValue(1, 10, "管理体制说明");

            //3行标题内容
            obj_sheet.SetValue(2, 2, "公用馈线(回)");
            obj_sheet.SetValue(2, 3, "电缆(千米)");
            obj_sheet.SetValue(2, 4, "架空线(千米)");
            obj_sheet.SetValue(2, 5, "合计(千米)");
            obj_sheet.SetValue(2, 6, "开关柜(面)");
            obj_sheet.SetValue(2, 7, "柱上开关(台)");
            obj_sheet.SetValue(2, 8, "开关站(座)");
            obj_sheet.SetValue(2, 9, "（回）");
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
            ArrayList rowsum = new ArrayList();
            Itop.Domain.PWTable.PW_tb3a p = new Itop.Domain.PWTable.PW_tb3a();
            p.col2 = Itop.Client.MIS.ProgUID;
            IList<PW_tb3a> list = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2251", p);

            PW_tb3c p3 = new PW_tb3c();
            p3.col4 = Itop.Client.MIS.ProgUID;
            IList<PW_tb3c> plist = Services.BaseService.GetList<PW_tb3c>("SelectPW_tb3cList", p3);
            IList<PW_tb3a> alist = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aListDIS",p);

            for (int n = 0; n < plist.Count; n++)
            {
                rowsum.Add("");
            }
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
                itemcount = itemcount + 1;
                obj_sheet.RowCount = obj_sheet.RowCount + 1;
                obj_sheet.SetValue(i + addnum + startrow, 0, obj.PQName);
                obj_sheet.SetValue(i + addnum + startrow, 1, obj.PQtype);
                obj_sheet.SetValue(i + addnum + startrow, 2, obj.Num10);
                obj_sheet.SetValue(i + addnum + startrow, 3, obj.Num3);
                obj_sheet.SetValue(i + addnum + startrow, 4, obj.Num2);
                obj_sheet.SetValue(i + addnum + startrow, 5, obj.Num4);
                obj_sheet.SetValue(i + addnum + startrow, 6, obj.KggNum);
                obj_sheet.SetValue(i + addnum + startrow, 7, obj.KG);
                obj_sheet.SetValue(i + addnum + startrow, 8, obj.KgzHwNum);
                obj_sheet.SetValue(i + addnum + startrow, 9, obj.Num11);
               // obj_sheet.SetValue(i + 3, 10, 1);
                if (obj.PQName != obj2.PQName)
                {
                    addnum = addnum + 1;
                        for (int x = 0; x < rowsum.Count; x++)
                        {
                            string str = (string)rowsum[x];
                            str = str + Convert.ToString(i + addnum + startrow - x - 1) + ",";
                            rowsum[x] = str;
                        }
                   
                    obj_sheet.RowCount = obj_sheet.RowCount + 1;
                    obj_sheet.SetValue(i + addnum + startrow, 0, obj.PQName);
                    obj_sheet.SetValue(i + addnum + startrow, 1, "小计");
                    int num1 = startrow + addnum + firstrow;
                    if (firstrow == 0) { num1 = num1 - 1; }
                    TC.Sheet_WriteFormula_RowSum(obj_sheet, num1, 2, itemcount, 1, i + addnum + startrow, 2, 8);
                    obj_sheet.AddSpanCell(num1, 0, itemcount+1, 1);
                    firstrow = i;
                    startrow = 3;
                    itemcount = 0;
                }
                if (sheetEnd)
                {
                    int[] sum = new int[plist.Count];
                    rowsum.Reverse();
                    for (int m = 0; m < plist.Count; m++)
                    {
                        PW_tb3c _p = plist[m];
                        addnum = addnum + 1;
                        obj_sheet.RowCount = obj_sheet.RowCount + 1;
                        obj_sheet.SetValue(i + addnum + startrow, 0, "全市合计");
                        obj_sheet.SetValue(i + addnum + startrow, 1, _p.col1);
                        sum[m] = i + addnum + startrow;
                        int num1 = startrow + addnum + firstrow;
                        //if (firstrow == 0) { num1 = num1 - 1; }
                        TC.Sheet_WriteFormula_RowSum3(obj_sheet, TC.getRowList(rowsum[m].ToString()), 2, i + addnum + startrow, 2, 8);
                    }
                    addnum = addnum + 1;
                    obj_sheet.RowCount = obj_sheet.RowCount + 1;
                    obj_sheet.SetValue(i + addnum + startrow, 0, "全市合计");
                    obj_sheet.SetValue(i + addnum + startrow, 1, "总计");
                    TC.Sheet_WriteFormula_RowSum3(obj_sheet, sum, 2, i + addnum + startrow, 2, 8);
                    obj_sheet.AddSpanCell(sum[0], 0, sum.Length+1, 1);
                }
            }
        }

      

    }
}
