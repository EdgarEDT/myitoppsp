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
    class Sheet2255
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
            //表格共27 行13 列
            rowcount = 27;
            colcount = 13;
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
            obj_sheet.Columns[1].Width = 60;
            obj_sheet.Columns[2].Width = 100;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 60;
            obj_sheet.Columns[5].Width = 60;
            obj_sheet.Columns[6].Width = 60;
            obj_sheet.Columns[7].Width = 60;
            obj_sheet.Columns[8].Width = 60;
            obj_sheet.Columns[9].Width = 60;
            obj_sheet.Columns[10].Width = 60;
            obj_sheet.Columns[11].Width = 100;
            obj_sheet.Columns[12].Width = 100;
            //设定表格行高度
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            //写标题行内容

            //2行标题内容
            obj_sheet.SetValue(1, 0, "分区");
            obj_sheet.SetValue(1, 1, "项目");
            obj_sheet.SetValue(1, 2, "负载率区间（%）");
            obj_sheet.SetValue(1, 3, "<20");
            obj_sheet.SetValue(1, 4, "20~40");
            obj_sheet.SetValue(1, 5, "40~50");
            obj_sheet.SetValue(1, 6, "50~67");
            obj_sheet.SetValue(1, 7, "67~75");
            obj_sheet.SetValue(1, 8, "75~80");
            obj_sheet.SetValue(1, 9, "80~100");
            obj_sheet.SetValue(1, 10, ">100");
            obj_sheet.SetValue(1, 11, "重载线路比例（%）");
            obj_sheet.SetValue(1, 12, "过载线路比例（%）");
            //写标题列内容

            //1列标题内容
            obj_sheet.AddSpanCell(2, 0, 8, 1);
            obj_sheet.SetValue(2, 0, "XX区（县）");
            obj_sheet.AddSpanCell(10, 0, 8, 1);
            obj_sheet.SetValue(10, 0, "xx区（县）22");
            obj_sheet.AddSpanCell(18, 0, 8, 1);
            obj_sheet.SetValue(18, 0, "全市合计");
            obj_sheet.AddSpanCell(26, 0, 1, 3);
            obj_sheet.SetValue(26, 0, "合计");

            //2列标题内容
            obj_sheet.AddSpanCell(2, 1, 6, 1);
            obj_sheet.SetValue(2, 1, "典型接线（回）");
            obj_sheet.AddSpanCell(8, 1, 1, 2);
            obj_sheet.SetValue(8, 1, "辐射型");
            obj_sheet.AddSpanCell(9, 1, 1, 2);
            obj_sheet.SetValue(9, 1, "其他非典型接线（回）");
            obj_sheet.AddSpanCell(10, 1, 6, 1);
            obj_sheet.SetValue(10, 1, "典型接线（回）");
            obj_sheet.AddSpanCell(16, 1, 1, 2);
            obj_sheet.SetValue(16, 1, "辐射型");
            obj_sheet.AddSpanCell(17, 1, 1, 2);
            obj_sheet.SetValue(17, 1, "其他非典型接线（回）");
            obj_sheet.AddSpanCell(18, 1, 6, 1);
            obj_sheet.SetValue(18, 1, "典型接线（回）");
            obj_sheet.AddSpanCell(24, 1, 1, 2);
            obj_sheet.SetValue(24, 1, "辐射型");
            obj_sheet.AddSpanCell(25, 1, 1, 2);
            obj_sheet.SetValue(25, 1, "其他非典型接线（回）");

            //3列标题内容
            obj_sheet.SetValue(2, 2, "两供一备");
            obj_sheet.SetValue(3, 2, "三供一备");
            obj_sheet.SetValue(4, 2, "单环网");
            obj_sheet.SetValue(5, 2, "双环网");
            obj_sheet.SetValue(6, 2, "多分段两联络");
            obj_sheet.SetValue(7, 2, "多分段三联络");
            obj_sheet.SetValue(10, 2, "两供一备");
            obj_sheet.SetValue(11, 2, "三供一备");
            obj_sheet.SetValue(12, 2, "单环网");
            obj_sheet.SetValue(13, 2, "双环网");
            obj_sheet.SetValue(14, 2, "多分段两联络");
            obj_sheet.SetValue(15, 2, "多分段三联络");
            obj_sheet.SetValue(18, 2, "两供一备");
            obj_sheet.SetValue(19, 2, "三供一备");
            obj_sheet.SetValue(20, 2, "单环网");
            obj_sheet.SetValue(21, 2, "双环网");
            obj_sheet.SetValue(22, 2, "多分段两联络");
            obj_sheet.SetValue(23, 2, "多分段三联络");
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
            int startrow = 2;
            int addnum = 0;
            int itemcount = 0;
            int firstrow = 0;
            ArrayList rowsum = new ArrayList();
            Itop.Domain.PWTable.PW_tb3a p = new Itop.Domain.PWTable.PW_tb3a();
            p.col2 = Itop.Client.MIS.ProgUID;
            IList<PW_tb3a> alist = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aListDIS", p);

            for (int n = 0; n < alist.Count; n++)
            {
                for (int m = 0; m < 8; m++)
                {
                    obj_sheet.RowCount = obj_sheet.RowCount + 1;
                }

                obj_sheet.AddSpanCell(2, 0, 8, 1);
                obj_sheet.SetValue(2, 0, "XX区（县）");

                //2列标题内容
                obj_sheet.AddSpanCell(startrow+n * 8, 1, 6, 1);
                obj_sheet.SetValue(startrow + n * 8, 1, "典型接线（回）");
                obj_sheet.AddSpanCell(startrow + (n + 1) * 8-1, 1, 1, 2);
                obj_sheet.SetValue(startrow + (n + 1) * 8-1, 1, "辐射型");
                obj_sheet.AddSpanCell(startrow + (n + 1) * 8, 1, 1, 2);
                obj_sheet.SetValue(startrow + (n+1) * 8, 1, "其他非典型接线（回）");

                //3列标题内容
                obj_sheet.SetValue(startrow + n * 8, 2, "两供一备");
                obj_sheet.SetValue(startrow + n * 8+1, 2, "三供一备");
                obj_sheet.SetValue(startrow + n * 8 + 2, 2, "单环网");
                obj_sheet.SetValue(startrow + n * 8 + 3, 2, "双环网");
                obj_sheet.SetValue(startrow + n * 8 + 4, 2, "多分段两联络");
                obj_sheet.SetValue(startrow + n * 8 + 5, 2, "多分段三联络");

                PW_tb3a _tba = alist[n];
                p.PQName = _tba.PQName;
                p.JXMS = "两供一备";
                IList<PW_tb3a> list1 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "三供一备";
                IList<PW_tb3a> list2 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "单环网";
                IList<PW_tb3a> list3 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "双环网";
                IList<PW_tb3a> list4 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "多分段两联络";
                IList<PW_tb3a> list5 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "多分段三联络";
                IList<PW_tb3a> list6 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "辐射型";
                IList<PW_tb3a> list7 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "其他非典型接线";
                IList<PW_tb3a> list8 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                if (list1.Count > 0)
                {
                    PW_tb3a obj = list1[0];
                    obj_sheet.SetValue(n+startrow+n*7, 0, obj.PQName);
                    obj_sheet.SetValue(n+startrow+n*7, 3, obj.Num1);
                    obj_sheet.SetValue(n+startrow+n*7, 4, obj.Num2);
                    obj_sheet.SetValue(n+startrow+n*7, 5, obj.Num3);
                    obj_sheet.SetValue(n+startrow+n*7, 6, obj.Num4);
                    obj_sheet.SetValue(n+startrow+n*7, 7, obj.Num5);
                    obj_sheet.SetValue(n+startrow+n*7, 8, obj.Num6);
                    obj_sheet.SetValue(n+startrow+n*7, 9, obj.Num7);
                    obj_sheet.SetValue(n+startrow+n*7, 10, obj.Num8);
                }
                else
                {
                    TC.Sheet_WriteZero(obj_sheet, n + startrow+n*7,3,1,8);
                }
            }
        }

       

    }
}
