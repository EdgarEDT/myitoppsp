//********************************************************************************/
//
//此代码由BuildSheetFromExcel代码生成器自动生成.
//生成时间:2011-5-20 8:26:17
//
//********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Itop.Domain.Table;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using System.Data;
using Itop.Common;
using Itop.Domain.Stutistics;
using Itop.Domain.Stutistic;
namespace Itop.Client.TableTemplateNW
{
    class Sheet511
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
        private void Build_Sheet(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //表格共15 行13 列
            rowcount = 15;
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
            obj_sheet.Columns[2].Width = 60;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 60;
            obj_sheet.Columns[5].Width = 60;
            //obj_sheet.Columns[6].Width = 60;
            //obj_sheet.Columns[7].Width = 60;
            //obj_sheet.Columns[8].Width = 60;
            //obj_sheet.Columns[9].Width = 60;
            //obj_sheet.Columns[10].Width = 60;
            //obj_sheet.Columns[11].Width = 60;
            //obj_sheet.Columns[12].Width = 60;
            //设定表格行高度
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
            //写标题行内容

            //2行标题内容
            obj_sheet.AddSpanCell(1, 0, 2, 1);
            obj_sheet.SetValue(1, 0, "序号");
            obj_sheet.AddSpanCell(1, 1, 2, 1);
            obj_sheet.SetValue(1, 1, "项目名称");
            obj_sheet.AddSpanCell(1, 2, 2, 1);
            obj_sheet.SetValue(1, 2, "电源类型");
            obj_sheet.AddSpanCell(1, 3, 2, 1);
            obj_sheet.SetValue(1, 3, "装机容量");
            obj_sheet.AddSpanCell(1, 4, 2, 1);
            obj_sheet.SetValue(1, 4, "接入电压");
            obj_sheet.AddSpanCell(1, 5, 2, 1);
            obj_sheet.SetValue(1, 5, "开工年份");
          
            //写标题列内容

            //1列标题内容
            obj_sheet.SetValue(3, 0, "1、");
            obj_sheet.SetValue(4, 0, "2、");
            obj_sheet.SetValue(5, 0, "1)");
            obj_sheet.SetValue(6, 0, "2)");
            obj_sheet.SetValue(7, 0, "3)");
            obj_sheet.SetValue(8, 0, "4)");
            obj_sheet.SetValue(9, 0, "5)");
            obj_sheet.SetValue(11, 0, "3、");
            obj_sheet.SetValue(12, 0, "1）");
            obj_sheet.SetValue(13, 0, "2)");
            obj_sheet.SetValue(14, 0, "3）");

            //2列标题内容
            obj_sheet.SetValue(3, 1, "年末装机容量");
            obj_sheet.SetValue(4, 1, "现有及新建电源项目小计");
            obj_sheet.SetValue(5, 1, "XX电厂");
            obj_sheet.SetValue(6, 1, "XX电厂");
            obj_sheet.SetValue(7, 1, "XX电厂扩建");
            obj_sheet.SetValue(8, 1, "XX电厂扩建");
            obj_sheet.SetValue(9, 1, "……");
            obj_sheet.SetValue(11, 1, "退役电源项目小计");
            obj_sheet.SetValue(12, 1, "XX");
            obj_sheet.SetValue(13, 1, "XX");
            obj_sheet.SetValue(14, 1, "……");
            //添加数据
            Sheet_AddData(obj_sheet);

            //设定表格线
            TC.Sheet_GridandCenter(obj_sheet);

            //锁定表格
            TC.Sheet_Locked(obj_sheet);
        }

        private void AddItemsCol(FarPoint.Win.Spread.SheetView obj_sheet, int[] TableYearsAry)
        {
            //int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            for (int i = 0; i < TableYearsAry.Length; i++)
            {
                obj_sheet.SetValue(2, 6+i,TableYearsAry[i]);
            }

            obj_sheet.AddSpanCell(1, 6, 1, TableYearsAry.Length);
            obj_sheet.SetValue(1, 6, "投产年份和投产规模");
        }
        //此处为动态添加数据方法
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int startrow = 3;
            int startcol = 2;
            int itemlengthcol = 2;
            int itemlengthrow = 3;
            //重新设定列数
            colcount = startcol + TableYearsAry.Length + 4;
            string sqlwhere = " ProjectID='" + Tcommon.ProjectID + "'";
            IList<PS_Table_AreaWH> ptalist = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", sqlwhere);
                       //用来记录年末容量
            int[] rongliang = new int[TableYearsAry.Length];


            for (int i = 0; i < ptalist.Count; i++)
            {
               // string sqlwhere2= "select a.* from Ps_PowerBuild as a,Ps_PowerBuild as b where a.ParentID=b.ID and b.Title='"+ptalist[i].Title+"' and b.ProjectID='" + Tcommon.ProjectID + "'";
               // IList ptblist2=Services.BaseService.GetList("SelectPowerBuildBYAllWHere", sqlwhere2);
               // DataTable dt2 = DataConverter.ToDataTable(ptblist2, typeof(Ps_History));
               //select a.* from Ps_PowerBuild as a,Ps_PowerBuild as b
               // where a.ParentID=b.ID and b.Title='金城江区' and b.ProjectID='85c066c7-a4d7-469b-928b-5d9e86280400';
               // select c.* from Ps_PowerBuild as a,Ps_PowerBuild as b,Ps_PowerBuild as c
               // where a.ParentID=b.ID and c.ParentID=a.ID and b.Title='金城江区' and (a.Title='已建项目小计' or a.Title='在建及新建项目小计')  and b.ProjectID='85c066c7-a4d7-469b-928b-5d9e86280400';
                
               // for (int j = 0; j < TableYearsAry.Length; j++)
               // {
                   
               // }
            }

        }



    }
}
