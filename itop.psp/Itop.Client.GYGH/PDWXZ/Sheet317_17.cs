using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
using System.Collections;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet317_17
    {

        private class savedata
        {
            public string DQ = "";
            public string areaname = "";
            public string dy = "";
            public object data = null;
        }
        //存放表3-17附表17数据
        List<savedata> SDL317_17 = new List<savedata>();
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
            //电压等级根据实际情况查询(中压 6 <= 电压<=20)
            //此处电压等级分两部分情况，负荷开关中的电压等级可以直接查出
            //但断路器的电压等级是要查询其母线的电压等级
            //分别查出两部分电压等级后进行整合
            string FHKGDianYatiaojian = " b.Type='59' and  b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and a.LineType2='公用' and b.RateVolt  between 6 and 20 group by b.RateVolt";
            string DLQDianYatiaojian = " a.Type='06' and  a.OperationYear!='' and CAST(a.OperationYear as int)<=" + year + " and a.ProjectID='" + ProjID + "'  and b.RateVolt  between 6 and 20 group by b.RateVolt";
            IList<double> FHKGDY_list = Services.BaseService.GetList<double>("SelectPSPDEV_RateVolt_type50-59", FHKGDianYatiaojian);
            IList<double> DLQDY_list = Services.BaseService.GetList<double>("SelectPSPDEV_RateVolt_type06", DLQDianYatiaojian);
            //整合两部分电压
            for (int i = 0; i < FHKGDY_list.Count; i++)
            {
                for (int j = 0; j < DLQDY_list.Count; j++)
                {
                    if (FHKGDY_list[i].ToString() == DLQDY_list[j].ToString())
                    {
                        DLQDY_list.Remove(DLQDY_list[j]);
                        break;
                    }
                }
            }
            if (DLQDY_list.Count > 0)
            {
                for (int i = 0; i < DLQDY_list.Count; i++)
                {
                    FHKGDY_list.Add(DLQDY_list[i]);
                }

            }
            List<double> DY_list = new List<double>();
            for (int i = 0; i < FHKGDY_list.Count; i++)
            {
                DY_list.Add(FHKGDY_list[i]);
            }
            DY_list.Sort();

            int xlsum = DY_list.Count;
            //计算市辖供电区条件 
            string FHKGSXtiaojianareaid = " b.Type='59' and  b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and a.AreaID!='' and  b.ProjectID='" + ProjID + "' and a.LineType2='公用' and a.DQ='市辖供电区' and  b.RateVolt between 6 and 20  group by a.AreaID";
            string DLQSXtiaojianareaid = " a.Type='06' and  a.OperationYear!='' and CAST(a.OperationYear as int)<=" + year + " and a.AreaID!='' and  a.ProjectID='" + ProjID + "' and a.DQ='市辖供电区' and  b.RateVolt between 6 and 20  group by a.AreaID";
            //计算县级供电区条件
            string FHKGXJtiaojianareaid = " b.Type='59' and  b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and a.AreaID!='' and  b.ProjectID='" + ProjID + "' and a.LineType2='公用' and a.DQ!='市辖供电区' and  b.RateVolt between 6 and 20  group by a.AreaID";
            string DLQXJtiaojianareaid = " a.Type='06' and a.OperationYear!='' and CAST(a.OperationYear as int)<=" + year + " and a.AreaID!='' and  a.ProjectID='" + ProjID + "' and a.DQ!='市辖供电区' and  b.RateVolt between 6 and 20  group by a.AreaID";
            //存放市辖供电区分区名
            IList<string> FHKGSXareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_AreaID_type50-59", FHKGSXtiaojianareaid);
            IList<string> DLQSXareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID_type06", DLQSXtiaojianareaid);
            //存放县级供电区分区名
            IList<string> FHKGXJareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_AreaID_type50-59", FHKGXJtiaojianareaid);
            IList<string> DLQXJareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID_type06", DLQXJtiaojianareaid);
            //整合市辖分区名
            for (int i = 0; i < FHKGSXareaid_List.Count; i++)
            {
                for (int j = 0; j < DLQSXareaid_List.Count; j++)
                {
                    //删除用断路器查到的那些分区中与负荷开关查到的重名的分区
                    if (FHKGSXareaid_List[i].ToString()==DLQSXareaid_List[j].ToString())
                    {
                        DLQSXareaid_List.Remove(DLQSXareaid_List[j]);
                        break;
                    }
                }
            }
            //将断路器查到的那些剩余分区拼到负荷开关分区中
            if (DLQSXareaid_List.Count>0)
            {
                for (int i = 0; i < DLQSXareaid_List.Count; i++)
                {
                    FHKGSXareaid_List.Add(DLQSXareaid_List[i]);
                }
            }
            IList<string> SXareaid_List = FHKGSXareaid_List;
            //整合县级分区名
            for (int i = 0; i < FHKGXJareaid_List.Count; i++)
            {
                for (int j = 0; j < DLQXJareaid_List.Count; j++)
                {
                    //删除用断路器查到的那些分区中与负荷开关查到的重名的分区
                    if (FHKGXJareaid_List[i].ToString() == DLQXJareaid_List[j].ToString())
                    {
                        DLQXJareaid_List.Remove(DLQXJareaid_List[j]);
                        break;
                    }
                }
            }
            //将断路器查到的那些剩余分区拼到负荷开关分区中
            if (DLQXJareaid_List.Count > 0)
            {
                for (int i = 0; i < DLQXJareaid_List.Count; i++)
                {
                    FHKGXJareaid_List.Add(DLQXJareaid_List[i]);
                }
            }
            IList<string> XJareaid_List = FHKGXJareaid_List;
            //市辖供电区分区个数
            int SXsum = SXareaid_List.Count;
            //县级供电区分区个数
            int XJsum = XJareaid_List.Count;
            //表标题行数
            int startrow = 2;
            //列标题每项是压等级数
            int dylength = xlsum;
            //表格共 行8 列
            rowcount = startrow + (SXsum + XJsum + 2) * dylength;
            colcount = 8;
            //工作表第一行的标题
            title = "附表17  铜陵市中压配电网开关无油化率统计(" + year + "年)";
            //工作表名
            sheetname = "表3-17 附表17";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 90;
            obj_sheet.Columns[1].Width = 90;
            obj_sheet.Columns[2].Width = 100;
            obj_sheet.Columns[3].Width = 100;
            obj_sheet.Columns[4].Width = 90;
            obj_sheet.Columns[5].Width = 100;
            obj_sheet.Columns[6].Width = 110;
            obj_sheet.Columns[7].Width = 100;
            //设定表格行高度
            
            obj_sheet.Rows[1].Height = 40;
            //写标题行列内容
            Sheet_AddItem(obj_sheet, area_key_id, DY_list, SXareaid_List, XJareaid_List);
            //写入数据
            Sheet_AddData(obj_sheet,year,ProjID, DY_list, SXareaid_List, XJareaid_List);
            //写入公式
            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, 2, 3, 6, 7, dylength * (SXsum + XJsum + 2));
            //设定格式
            CellType(obj_sheet);
            //锁定表格
            fc.Sheet_Locked(obj_sheet);

        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet,Hashtable area_key_id, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            //2行标题内容
            obj_sheet.SetValue(1, 0, "分区类型");
            obj_sheet.SetValue(1, 1, "分区名称");
            obj_sheet.SetValue(1, 2, "电压等级（kV）");
            obj_sheet.SetValue(1, 3, "总开关数（台）");
            obj_sheet.SetValue(1, 4, "断路器（台）");
            obj_sheet.SetValue(1, 5, "负荷开关（台）");
            obj_sheet.SetValue(1, 6, "其中：无油化开关（台）");
            obj_sheet.SetValue(1, 7, "开关无油化率（%）");
            //写标题列内容
            fc.Sheet_AddItem_FBonlyDY(obj_sheet, area_key_id, 2, obj_DY_List, SXareaid_List, XJareaid_List);
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {

            //条件
            string tiaojian = "";
            //DQ条件
            string DQtiaojian = "";
            int startrow = 2;
            int length = obj_DY_List.Count;
            //要统计合计的列数
            int colcount = 4;
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
                        //断路器（台）
                        tiaojian = " a.OperationYear!='' and CAST(a.OperationYear as int)<=" + year + " and a.ProjectID='" + ProjID + "' and a.Type='06' and " + DQtiaojian + " and a.AreaID='" + areaid + "' and b.RateVolt=" + obj_DY_List[j].ToString();
                        int DLQsum = 0;
                        if (Services.BaseService.GetObject("SelectPSPDEV_CountDLQ", tiaojian) != null)
                        {
                            DLQsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountDLQ", tiaojian);
                        }
                        obj_sheet.SetValue(startrow + i * length + j, 4, DLQsum);
                        //负荷开关（台）
                        tiaojian = " b.OperationYear!='' and  year(cast(b.OperationYear as datetime))<=" + year + " and b.ProjectID='" + ProjID + "' and b.Type='59' and a.LineType2='公用'  and " + DQtiaojian + " and a.AreaID='" + areaid + "' and b.RateVolt=" + obj_DY_List[j].ToString();
                        int FHKGsum = 0;
                        if (Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian) != null)
                        {
                            FHKGsum = (int)Services.BaseService.GetObject("SelectPSPDEV_Count_type50-59", tiaojian);
                        }
                        obj_sheet.SetValue(startrow + i * length + j, 5, FHKGsum);
                        //总开关数（台）(断路器（台）+负荷开关（台）)
                        int ALLKG = DLQsum + FHKGsum;

                        obj_sheet.SetValue(startrow + i * length + j, 3, ALLKG);
                    }
                }

                else
                {
                    //市辖合计部分公式
                    if (i == 0)
                    {
                        if (SXareaid_List.Count != 0)
                        {
                            fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * length, 3, SXareaid_List.Count, length, startrow + i * length, 3, colcount);
                        }
                        //无地区直接在合计部分写0
                        else
                        {
                            fc.Sheet_WriteZero(obj_sheet, startrow + i * length, 3, length, colcount);
                        }

                    }
                    //县级合计部分公式
                    else
                    {
                        if (XJareaid_List.Count != 0)
                        {
                            fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * length, 3, XJareaid_List.Count, length, startrow + i * length, 3, colcount);

                        }
                        //无地区直接在合计部分写0
                        else
                        {
                            fc.Sheet_WriteZero(obj_sheet, startrow + i * length, 3, length, colcount);
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
                for (int row = SXrow+dysum; row < XJrow; row++)
                {
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, 6);
                }
                //设定县级部分除合计以外的所有行的第7列为可输入
                for (int row = XJrow + dysum; row < obj_sheet.RowCount; row++)
                {
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, 6);
                }
            }
        }
        public void SaveData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //清空存放数据列表
            SDL317_17.Clear();

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
                    tempdata.data = obj_sheet.GetValue(row, 6);
                    SDL317_17.Add(tempdata);
                }
                //存储县级部分除合计以外的所有行的第7列数据
                for (int row = XJrow + dysum; row < obj_sheet.RowCount; row++)
                {
                    savedata tempdata = new savedata();
                    tempdata.DQ = "县级供电区";
                    tempdata.areaname = fc.Sheet_find_Rownotemptycell(obj_sheet, row, 1);
                    tempdata.dy = obj_sheet.Cells[row, 2].Value.ToString();
                    tempdata.data = obj_sheet.GetValue(row, 6);
                    SDL317_17.Add(tempdata);
                }
            }
        }
        public void WriteData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            if (SDL317_17.Count != 0)
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
                    for (int i = 0; i < SDL317_17.Count; i++)
                    {
                        if (dq == SDL317_17[i].DQ && areaname == SDL317_17[i].areaname && dy == SDL317_17[i].dy)
                        {
                            obj_sheet.SetValue(row, 6, SDL317_17[i].data);
                            SDL317_17.Remove(SDL317_17[i]);
                            break;
                        }
                    }
                }
            }
           
        }
    }
}
