using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
using System.Collections;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet314_12
    {
       
        private class savedata
        {
            public string DQ = "";
            public string areaname = "";
            public string dy = "";
            public object data = null;
        }
        //存放表3-14附表12数据
        List<savedata> SDL314_12 = new List<savedata>();
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
            //线路电压等级根据实际情况查询(中压 6 <= 电压<=20)
            string XLDianYatiaojian = " (b.Type='50' or b.Type='51' or b.Type='52') and  b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and a.LineType2!='' and b.RateVolt  between 6 and 20 group by b.RateVolt order by b.RateVolt";
            IList<double> DY_list = Services.BaseService.GetList<double>("SelectPSPDEV_RateVolt_type50-59", XLDianYatiaojian);
            int xlsum = DY_list.Count;

            //计算市辖供电区条件 
            string SXtiaojianareaid = " (b.Type='50' or b.Type='51' or b.Type='52') and  b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and  a.AreaID!=''  and a.DQ='市辖供电区' and a.LineType2!='' and b.RateVolt  between 6 and 20 group by a.AreaID";
            //计算县级供电区条件
            string XJtiaojianareaid = " (b.Type='50' or b.Type='51' or b.Type='52') and  b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and  a.AreaID!=''  and a.DQ!='市辖供电区' and a.LineType2!='' and b.RateVolt  between 6 and 20 group by a.AreaID";
            //存放市辖供电区分区名
            IList<string> SXareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_AreaID_type50-59", SXtiaojianareaid);
            //存放县级供电区分区名
            IList<string> XJareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_AreaID_type50-59", XJtiaojianareaid);

            //市辖供电区分区个数
            int SXsum = SXareaid_List.Count;
            //县级供电区分区个数
            int XJsum = XJareaid_List.Count;
            //表标题行数
            int startrow = 3;
            //列标题每项行数
            int dylength = xlsum;

            //表格共 行11 列
            rowcount = startrow + (SXsum + XJsum + 2) * dylength;
            colcount = 11;
            //工作表第一行的标题
            title = "附表12  铜陵市中压配电变压器情况统计(" + year + "年)";
            //工作表名
            sheetname = "表3-14 附表12 ";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 90;
            obj_sheet.Columns[1].Width = 90;
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
            
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
            //写标题行列内容
            Sheet_AddItem(obj_sheet, area_key_id, DY_list, SXareaid_List, XJareaid_List);
            //写入数据
            Sheet_AddData(obj_sheet,year,ProjID, DY_list, SXareaid_List, XJareaid_List);
            //写入公式
            fc.Sheet_WriteFormula_OneCol_TwoCol_Threecol_sum(obj_sheet, startrow+dylength, 3, 5, 7, dylength*SXareaid_List.Count);
            fc.Sheet_WriteFormula_OneCol_TwoCol_Threecol_sum(obj_sheet, startrow + (2+SXareaid_List.Count)*dylength, 3, 5, 7, XJareaid_List.Count*dylength);
            fc.Sheet_WriteFormula_OneCol_TwoCol_Threecol_sum(obj_sheet, startrow + dylength, 4, 6, 8, dylength * SXareaid_List.Count);
            fc.Sheet_WriteFormula_OneCol_TwoCol_Threecol_sum(obj_sheet, startrow + (2 + SXareaid_List.Count) * dylength, 4, 6, 8, XJareaid_List.Count * dylength);
            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, startrow, 3, 9, 10, (SXsum + XJsum + 2) * dylength);
            //锁定表格
            fc.Sheet_Locked(obj_sheet);
            //设定格式
            CellType(obj_sheet);
        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet, Hashtable area_key_id, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            //2行标题内容
            obj_sheet.AddSpanCell(1, 0, 2, 1);
            obj_sheet.SetValue(1, 0, "分区类型");
            obj_sheet.AddSpanCell(1, 1, 2, 1);
            obj_sheet.SetValue(1, 1, "分区名称");
            obj_sheet.AddSpanCell(1, 2, 2, 1);
            obj_sheet.SetValue(1, 2, "电压等级(kV)");
            obj_sheet.AddSpanCell(1, 3, 2, 1);
            obj_sheet.SetValue(1, 3, "公变台数(台)");
            obj_sheet.AddSpanCell(1, 4, 2, 1);
            obj_sheet.SetValue(1, 4, "公变容量(MVA)");
            obj_sheet.AddSpanCell(1, 5, 2, 1);
            obj_sheet.SetValue(1, 5, "专变台数(台)");
            obj_sheet.AddSpanCell(1, 6, 2, 1);
            obj_sheet.SetValue(1, 6, "专变容量(MVA)");
            obj_sheet.AddSpanCell(1, 7, 2, 1);
            obj_sheet.SetValue(1, 7, "总台数(台)");
            obj_sheet.AddSpanCell(1, 8, 2, 1);
            obj_sheet.SetValue(1, 8, "总容量(MVA)");
            obj_sheet.AddSpanCell(1, 9, 1, 2);
            obj_sheet.SetValue(1, 9, "高损变");

            //3行标题内容
            obj_sheet.SetValue(2, 9, "台数(台)");
            obj_sheet.SetValue(2, 10, "比例(%)");
            //写标题列内容
            fc.Sheet_AddItem_FBonlyDY(obj_sheet, area_key_id, 3, obj_DY_List, SXareaid_List, XJareaid_List);
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {

            //条件
            string tiaojian = "";
            string GorZ = "";
            //DQ条件
            string DQtiaojian = "";
            int startrow = 3;
            int dylenth = obj_DY_List.Count;
            //要统计合计的列数
            int colcount = 7;
            for (int i = 0; i < (2 + SXareaid_List.Count + XJareaid_List.Count); i++)
            {
                string areaid = "";
                if (i != 0 && i != (SXareaid_List.Count + 1))
                {
                    if (i < SXareaid_List.Count + 1)
                    {
                        DQtiaojian = " a.DQ='市辖供电区'";
                        areaid = SXareaid_List[i - 1].ToString();
                    }
                    else
                    {
                        DQtiaojian = " a.DQ!='市辖供电区'";
                        areaid = XJareaid_List[i - SXareaid_List.Count - 2].ToString();
                    }
                    for (int j = 0; j < obj_DY_List.Count; j++)
                    {
                        //一次算公用一次算专用
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
                            //配电室内主变台数
                            tiaojian = " b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and b.Type='50' and a.LineType2='" + GorZ + "' and " + DQtiaojian + " and a.AreaID='" + areaid + "' and b.RateVolt=" + obj_DY_List[j];

                            int PDsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_SUMFlag", tiaojian) != null)
                            {
                                PDsum = (int)Services.BaseService.GetObject("SelectPSPDEV_SUMFlag", tiaojian);
                            }
                            //箱变座数 柱上变（台）
                            tiaojian = " b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and (b.Type='51' or b.Type='52') and a.LineType2='" + GorZ + "' and " + DQtiaojian + " and a.AreaID='" + areaid + "' and b.RateVolt=" + obj_DY_List[j];
                            int XBsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian) != null)
                            {
                                XBsum = (int)Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian);
                            }
                            //配变台数等于这三个数据和
                            obj_sheet.SetValue(startrow + i * dylenth + j, 3 + k * 2, PDsum + XBsum);


                            //配变容量
                            tiaojian = " b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and (b.Type='50' or b.Type='51' or b.Type='52') and a.LineType2='" + GorZ + "' and " + DQtiaojian + " and a.AreaID='" + areaid + "' and b.RateVolt=" + obj_DY_List[j];

                            double PBRLsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_SUMNum2_type50-59", tiaojian) != null)
                            {
                                PBRLsum = (double)Services.BaseService.GetObject("SelectPSPDEV_SUMNum2_type50-59", tiaojian);
                            }
                            obj_sheet.SetValue(startrow + i * dylenth + j, 4 + k * 2, PBRLsum);
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
                            fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * dylenth, 3, SXareaid_List.Count, dylenth, startrow + i * dylenth, 3, colcount);
                        }
                        //无地区直接在合计部分写0
                        else
                        {
                            fc.Sheet_WriteZero(obj_sheet, startrow + i * dylenth, 3, dylenth, colcount);
                        }

                    }
                    //县级合计部分公式
                    else
                    {
                        if (XJareaid_List.Count != 0)
                        {
                            fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * dylenth, 3, XJareaid_List.Count, dylenth, startrow + i * dylenth, 3, colcount);

                        }
                        //无地区直接在合计部分写0
                        else
                        {
                            fc.Sheet_WriteZero(obj_sheet, startrow + i * dylenth, 3, dylenth, colcount);
                        }
                    }
                }
            }
        }
        public void CellType(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //市辖部分的行号
            int SXrow = fc.Sheet_Find_Value(obj_sheet, 0, "市辖供电区");
            //县级部分的行号
            int XJrow = fc.Sheet_Find_Value(obj_sheet, 0, "县级供电区");
            //为-1时表示没找到，也就是电压等级为0个
            if (SXrow != -1)
            {
                //市辖供电区中第一行第二列的“合计”部分合并的行数就是电压等级数
                int dysum = obj_sheet.Cells[SXrow, 1].RowSpan;

                //设定市辖部分除合计以外的所有行的第7列为可输入
                for (int row = SXrow + dysum; row < XJrow; row++)
                {
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, 9);
                }
                //设定县级部分除合计以外的所有行的第7列为可输入
                for (int row = XJrow + dysum; row < obj_sheet.RowCount; row++)
                {
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, 9);
                }
            }
        }
        public void SaveData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //清空存放数据列表
            SDL314_12.Clear();

            //市辖部分的行号
            int SXrow = fc.Sheet_Find_Value(obj_sheet, 0, "市辖供电区");
            //县级部分的行号
            int XJrow = fc.Sheet_Find_Value(obj_sheet, 0, "县级供电区");
            //为-1时表示没找到，也就是电压等级为0个
            if (SXrow != -1)
            {
                //市辖供电区中第一行第二列的“合计”部分合并的行数就是电压等级数
                int dysum = obj_sheet.Cells[SXrow, 1].RowSpan;

                //存储市辖部分除合计以外的所有行的第7列数据
                for (int row = SXrow + dysum; row < XJrow; row++)
                {
                    savedata tempdata = new savedata();
                    tempdata.DQ = "市辖供电区";
                    tempdata.areaname = fc.Sheet_find_Rownotemptycell(obj_sheet, row, 1);
                    tempdata.dy = obj_sheet.Cells[row, 2].Value.ToString();
                    tempdata.data = obj_sheet.GetValue(row, 9);
                    SDL314_12.Add(tempdata);
                }
                //存储县级部分除合计以外的所有行的第7列数据
                for (int row = XJrow + dysum; row < obj_sheet.RowCount; row++)
                {
                    savedata tempdata = new savedata();
                    tempdata.DQ = "县级供电区";
                    tempdata.areaname = fc.Sheet_find_Rownotemptycell(obj_sheet, row, 1);
                    tempdata.dy = obj_sheet.Cells[row, 2].Value.ToString();
                    tempdata.data = obj_sheet.GetValue(row, 9);
                    SDL314_12.Add(tempdata);
                }
            }
        }
        public void WriteData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            if (SDL314_12.Count != 0)
            {
                //市辖部分的行号
                int SXrow = fc.Sheet_Find_Value(obj_sheet, 0, "市辖供电区");
                //市辖供电区中第一行第二列的“合计”部分合并的行数就是电压等级数
                int newdysum = obj_sheet.Cells[SXrow, 1].RowSpan;
                for (int row = SXrow + newdysum; row < obj_sheet.RowCount; row++)
                {
                    string dq = fc.Sheet_find_Rownotemptycell(obj_sheet, row, 0);
                    string areaname = fc.Sheet_find_Rownotemptycell(obj_sheet, row, 1);
                    string dy = obj_sheet.Cells[row, 2].Value.ToString();
                    for (int i = 0; i < SDL314_12.Count; i++)
                    {
                        if (dq == SDL314_12[i].DQ && areaname == SDL314_12[i].areaname && dy == SDL314_12[i].dy)
                        {
                            obj_sheet.SetValue(row, 9, SDL314_12[i].data);
                            SDL314_12.Remove(SDL314_12[i]);
                            break;
                        }
                    }
                }
            }

        }
    }
}
