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
    class Sheet222zhu
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
            //表格共5 行7 列
            rowcount = 5;
            colcount = 7;
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
            obj_sheet.Columns[1].Width = 80;
            obj_sheet.Columns[2].Width = 120;
            obj_sheet.Columns[3].Width = 80;
            obj_sheet.Columns[4].Width = 80;
            obj_sheet.Columns[5].Width = 80;
            obj_sheet.Columns[6].Width = 240;
            //设定表格行高度
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            //写标题行内容

            //2行标题内容
            obj_sheet.SetValue(1, 0, "序号");
            obj_sheet.SetValue(1, 1, "所在区域");
            obj_sheet.SetValue(1, 2, "名称");
            obj_sheet.SetValue(1, 3, "接入电压");
            obj_sheet.SetValue(1, 4, "类型");
            obj_sheet.SetValue(1, 5, "装机容量");
            obj_sheet.SetValue(1, 6, "备注");
            //写标题列内容

            //1列标题内容
           
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
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int startrow = 2;
            int itemlength = 1;
            string sqlwhere = " AreaID='" + Tcommon.ProjectID + "' and Flag='1' and Cast(S3 as int)<=" + TableYearsAry[0] + " and Cast(S1 as int)<=110 order by  Cast(S1 as int) desc";
            IList<Itop.Client.Stutistics.PSP_PowerSubstationInfo> ptalist = Services.BaseService.GetList<Itop.Client.Stutistics.PSP_PowerSubstationInfo>("SelectPSP_PowerSubstationInfoByConn", sqlwhere);
            //重新设定行数
            obj_sheet.RowCount = startrow + (ptalist.Count);
            if (ptalist.Count==0)
            {
                TC.WriteQuestion(title,  "无电源数据", "查询 设备参数电源，看是否有相关数据", "");
            }
            for (int i = 0; i < ptalist.Count; i++)
            {
                obj_sheet.SetValue(startrow + i, 0,i+1);
                obj_sheet.SetValue(startrow + i, 1, ptalist[i].S9);
                obj_sheet.SetValue(startrow + i, 2, ptalist[i].Title);
                obj_sheet.SetValue(startrow + i, 3, ptalist[i].S1);
                obj_sheet.SetValue(startrow + i, 4, ptalist[i].S10);
                obj_sheet.SetValue(startrow + i, 5, ptalist[i].S2);
                obj_sheet.SetValue(startrow + i, 6, ptalist[i].S20);
            }


        }



    }
}
