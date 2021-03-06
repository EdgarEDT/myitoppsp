using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
using System.Collections;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet39_6
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
        public void Build(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, Hashtable area_key_id, List<string[]> SxXjName)
        {
            //线路电压等级根据实际情况查询
            string XLDianYatiaojian = " Type='05' and OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + " and  ProjectID='" + ProjID + "' and RateVolt  between 35 and 110 group by RateVolt";
            IList<double> DY_list = Services.BaseService.GetList<double>("SelectPSPDEV_RateVolt", XLDianYatiaojian);
            int xlsum = DY_list.Count;

            //计算市辖供电区条件 
            string SXtiaojianareaid = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and AreaID!='' and  ProjectID='" + ProjID + "' and DQ='市辖供电区' and   Type='05' and RateVolt between 35 and 110  group by AreaID";
            //计算县级供电区条件
            string XJtiaojianareaid = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and AreaID!='' and  ProjectID='" + ProjID + "' and DQ!='市辖供电区' and   Type='05' and RateVolt between 35 and 110  group by AreaID";
            //存放市辖供电区分区名
            IList<string> SXareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID", SXtiaojianareaid);
            //存放县级供电区分区名
            IList<string> XJareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID", XJtiaojianareaid);

            //市辖供电区分区个数
            int SXsum = SXareaid_List.Count;
            //县级供电区分区个数
            int XJsum = XJareaid_List.Count;
            //表标题行数
            int startrow = 3;
            //列标题每项行数
            int length = xlsum;
            //表格共   行12列
            rowcount = startrow + (SXsum + XJsum + 2) * length;
            colcount = 12;
            //工作表第一行的标题
            title = "附表6  铜陵市110kV及以下高压配电网线路设备情况(" + year + "年)";
            //工作表名
            sheetname = "表3-9 附表6";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 100;
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
            obj_sheet.Columns[11].Width = 90;
            //设定表格行高度
            
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
           
            //写标题行列内容
            Sheet_AddItem(obj_sheet, area_key_id, DY_list, SXareaid_List, XJareaid_List);
            //写入数据
            Sheet_AddData(obj_sheet, year, ProjID, DY_list, SXareaid_List, XJareaid_List);
            //写入求比例公式
            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, startrow, 5, 7, 11, (SXsum + XJsum + 2) * length);
            //锁定表格
            fc.Sheet_Locked(obj_sheet);

        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet,Hashtable area_key_id, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {

            //写标题行内容

            //2行标题内容
            obj_sheet.AddSpanCell(1, 0, 2, 1);
            obj_sheet.SetValue(1, 0, "分区类型");
            obj_sheet.AddSpanCell(1, 1, 2, 1);
            obj_sheet.SetValue(1, 1, "分区名称");
            obj_sheet.AddSpanCell(1, 2, 2, 1);
            obj_sheet.SetValue(1, 2, "电压等级（kV）");
            obj_sheet.AddSpanCell(1, 3, 1, 2);
            obj_sheet.SetValue(1, 3, "线路条数（条）");
            obj_sheet.AddSpanCell(1, 5, 1, 2);
            obj_sheet.SetValue(1, 5, "线路总长度（km）");
            obj_sheet.AddSpanCell(1, 7, 1, 2);
            obj_sheet.SetValue(1, 7, "电缆线路长度（km）");
            obj_sheet.AddSpanCell(1, 9, 1, 2);
            obj_sheet.SetValue(1, 9, "架空线路长度（km）");
            obj_sheet.SetValue(1, 11, "电缆化率（%）");

            //3行标题内容
            obj_sheet.SetValue(2, 3, "公用");
            obj_sheet.SetValue(2, 4, "专用");
            obj_sheet.SetValue(2, 5, "公用");
            obj_sheet.SetValue(2, 6, "专用");
            obj_sheet.SetValue(2, 7, "公用");
            obj_sheet.SetValue(2, 8, "专用");
            obj_sheet.SetValue(2, 9, "公用");
            obj_sheet.SetValue(2, 10, "专用");
            obj_sheet.SetValue(2, 11, "公用");
            //写标题列内容
            fc.Sheet_AddItem_FBonlyDY(obj_sheet, area_key_id, 3, obj_DY_List, SXareaid_List, XJareaid_List);
        
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            //添加数据
            //公用or专用
            string GorZ = "";
            //条件
            string XLtiaojian = "";
            //DQ条件
            string DQtiaojain = "";
            int startrow = 3;
            int length = obj_DY_List.Count;
            if (obj_DY_List.Count > 0)
            {
                for (int i = 0; i < (2 + SXareaid_List.Count + XJareaid_List.Count); i++)
                {
                    string areaid = "";
                    if (i != 0 && i != (SXareaid_List.Count + 1))
                    {
                        if (i < SXareaid_List.Count + 1)
                        {
                            DQtiaojain = " DQ='市辖供电区'";
                            areaid = SXareaid_List[i - 1].ToString();
                        }
                        else
                        {
                            DQtiaojain = " DQ!='市辖供电区'";
                            areaid = XJareaid_List[i - SXareaid_List.Count - 2].ToString();
                        }
                        for (int j = 0; j < obj_DY_List.Count; j++)
                        {
                            //固定循环两次，计算公用和专用
                            for (int k = 0; k < 2; k++)
                            {
                                if (k == 0)
                                {
                                    GorZ = "公用";
                                }
                                else
                                {
                                    GorZ = "专用";
                                }
                                XLtiaojian = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and ProjectID='" + ProjID + "' and Type='05' and AreaID='" + areaid + "' and LineType2='" + GorZ + "'  and " + DQtiaojain + " and RateVolt=" + obj_DY_List[j].ToString();
                                //线路条数
                                int XLsum = 0;
                                if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian) != null)
                                {
                                    XLsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian);
                                }
                                obj_sheet.SetValue(startrow + i * length + j, 3 + k, XLsum);
                                //电缆线长
                                double DLXlength = 0;
                                if (Services.BaseService.GetObject("SelectPSPDEV_SUMLength2", XLtiaojian) != null)
                                {
                                    DLXlength = (double)Services.BaseService.GetObject("SelectPSPDEV_SUMLength2", XLtiaojian);
                                }
                                obj_sheet.SetValue(startrow + i * length + j, 7 + k, DLXlength);
                                //架空线长
                                double JKXlength = 0;
                                if (Services.BaseService.GetObject("SelectPSPDEV_SUMLineLength", XLtiaojian) != null)
                                {
                                    JKXlength = (double)Services.BaseService.GetObject("SelectPSPDEV_SUMLineLength", XLtiaojian);
                                }
                                obj_sheet.SetValue(startrow + i * length + j, 9 + k, JKXlength);
                                //写入全长公式
                                obj_sheet.Cells[startrow + i * length + j, 5 + k].Formula = "SUM(" + "R" + (startrow + i * length + j + 1) + "C" + (7 + k + 1) + "," + "R" + (startrow + i * length + j + 1) + "C" + (9 + k + 1) + ")";

                            }
                        }

                    }
                    else
                    {
                        //市辖合计部分公式
                        if (i == 0)
                        {
                            if (SXareaid_List.Count != 0)
                            {
                                fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * length, 3, SXareaid_List.Count, length, startrow + i * length, 3, 8);
                            }
                            //无地区直接在合计部分写0
                            else
                            {
                                fc.Sheet_WriteZero(obj_sheet, startrow + i * length, 3, length, 9);
                            }

                        }
                        //县级合计部分公式
                        else
                        {
                            if (XJareaid_List.Count != 0)
                            {
                                fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * length, 3, XJareaid_List.Count, length, startrow + i * length, 3, 8);

                            }
                            //无地区直接在合计部分写0
                            else
                            {
                                fc.Sheet_WriteZero(obj_sheet, startrow + i * length, 3, length, 9);
                            }

                        }

                    }
                }
            }
        }
    }
}
