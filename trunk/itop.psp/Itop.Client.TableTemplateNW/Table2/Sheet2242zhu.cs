using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Itop.Domain.Table;
using Itop.Client.Common;
namespace Itop.Client.TableTemplateNW.Table2
{
    class Sheet2242zhu
    {
        ////规划城市名称
        //string CityName = Tcommon.CityName;
        ////当前年份
        //int nowyear = Tcommon.CurrentYear;
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
        public void Build_sheet(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //表格共行7 列
            string con = " ProjectID='" + ProjectID + "|pro" + "' and ParentID='0'";
            //查分类数
            IList<Ps_Table_BuildPro> listTypes = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", con);
            //表标题行数
            int startrow = 3;
           
            rowcount = startrow ;
            colcount = 7;
            //工作表第一行的标题
            //title = CityName+"市高压配电网已开展工程项目列表";
            title = TC.GetTableTitle(this.GetType().Name);
            sheetname = title;
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            //工作表名
            //sheetname = CityName + "市高压配电网已开展工程项目列表";
           //int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            //设定工作表行列值及标题和表名
            TC.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            TC.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            TC.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 190;
            obj_sheet.Columns[2].Width = 60;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 90;
            obj_sheet.Columns[5].Width = 60;
            obj_sheet.Columns[6].Width = 60;
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
            obj_sheet.AddSpanCell(1, 2, 1, 2);
            obj_sheet.SetValue(1, 2, "建设规模");
            obj_sheet.AddSpanCell(1, 4, 2, 1);
            obj_sheet.SetValue(1, 4, "所在区（县）");
            obj_sheet.AddSpanCell(1, 5, 2, 1);
            obj_sheet.SetValue(1, 5, "当前进度");
            obj_sheet.AddSpanCell(1, 6, 2, 1);
            obj_sheet.SetValue(1, 6, "投产年份");

            //3行标题内容
            obj_sheet.SetValue(2, 2, "线路长度");
            obj_sheet.SetValue(2, 3, "变电容量");
            //添加数据
            Sheet_AddData(obj_sheet);
            TC.Sheet_GridandCenter(obj_sheet);
            //锁定表格
            TC.Sheet_Locked(obj_sheet);
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int currentyear = TableYearsAry[0];
            string tiaojian = "";
            int startrow = 3-1;
            tiaojian = " ProjectID='" + ProjectID + "|pro" + "' and ParentID='0' order by Cast(FromID as int) desc";
            //用来计算行数
            int pro_num = 0;
            int m = 0;
            try
            {
                //查分类数
                IList<Ps_Table_BuildPro> listTypes = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", tiaojian);

                //根据分类查下面的项目数
                for (int i = 0; i < listTypes.Count; i++)
                {
                    //投产年份大于等于当前年份
                    string tempcon = " ProjectID='" + ProjectID + "|pro" + "' and  ParentID='" + listTypes[i].ID + "' and cast(BuildEd as int)>=" + currentyear;
                    IList<Ps_Table_BuildPro> tempptblist = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", tempcon);
                    if (tempptblist.Count > 0)
                    {
                        pro_num++;
                        m++;
                        obj_sheet.RowCount = obj_sheet.RowCount + 1;
                        obj_sheet.SetValue(startrow + m, 0, TC.CHNumberToChar(pro_num));
                        obj_sheet.SetValue(startrow + m, 1, listTypes[i].Title);
                      
                        for (int j = 0; j < tempptblist.Count; j++)
                        {
                            m++;
                            obj_sheet.RowCount = obj_sheet.RowCount + 1;
                            obj_sheet.SetValue(startrow + m, 0, j+1);
                            obj_sheet.SetValue(startrow + m, 1, tempptblist[j].Title);
                            obj_sheet.SetValue(startrow + m, 2, tempptblist[j].Length);
                            obj_sheet.SetValue(startrow + m, 3, tempptblist[j].Volumn);
                            obj_sheet.SetValue(startrow + m, 4, tempptblist[j].AreaName);
                            obj_sheet.SetValue(startrow + m, 5, tempptblist[j].Flag);
                            obj_sheet.SetValue(startrow + m, 6, Convert.ToInt32(tempptblist[j].BuildEd));
                           
                            
                        }
                        m++;
                        obj_sheet.RowCount = obj_sheet.RowCount + 1;
                        obj_sheet.AddSpanCell(startrow + m, 0, 1, 2);
                        obj_sheet.SetValue(startrow + m , 0, listTypes[i].Title + "小计");
                        TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + m  - tempptblist.Count, 2, tempptblist.Count, 1, startrow + m , 2, 2);
                    }
                }
                if (m==0)
                {
                    TC.WriteQuestion(title, "无规划项目数据", "查询项目管理，看是否有竣工年限大于当前年的数据", "");
                }
            }
            catch (Exception em)
            {
                
                throw;
            }
           
        }
    }
}
