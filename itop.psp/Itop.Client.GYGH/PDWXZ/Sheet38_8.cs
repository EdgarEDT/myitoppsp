using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
using System.Collections;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet38_8
    {

        /// <summary>
        /// 市辖供电区和县级供电区每个又分两部分，一部分是来自变电站表的数据，另一部分是来自断路器所在表的数据
        /// 两部分数据要进行整合后再显示
        /// 本表中断路器的统计比较复杂，其电压值要取所在母线的电压，公用性要取母线所在变电站的公用性
        /// </summary>
        fpcommon fc = new fpcommon();
        //工作表行数
        int rowcount = 0;
        //工作表列数据
        int colcount = 0;
        //工作表第一行的表名
        string title = "";
        //工作表标签名
        string sheetname = "";
        public void Build(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, Hashtable area_key_name, Hashtable area_key_id, List<string[]> SxXjName)
        {

            //计算市辖供电区条件 
            string SXtiaojianareaname = " S2!='' and AreaName!='' and (" + year + "-cast(substring(S2,1,4) as int) )>=0  and AreaID='" + ProjID + "' and DQ='市辖供电区' and  S4='公用'   and (L1=110 or L1=66 or L1=35)  group by AreaName";
            string SXtiaojianareaid = " a.OperationYear!='' and a.AreaID!='' and (" + year + "-cast(a.OperationYear as int))>=0 and a.ProjectID='" + ProjID + "' and a.DQ='市辖供电区' and   a.Type='06' and (b.RateVolt=110 or b.RateVolt=66 or b.RateVolt=35)  group by a.AreaID";
            //计算县级供电区条件
            string XJtiaojianareaname = " S2!='' and AreaName!='' and (" + year + "-cast(substring(S2,1,4) as int) )>=0  and AreaID='" + ProjID + "' and DQ!='市辖供电区' and  S4='公用'   and (L1=110 or L1=66 or L1=35)  group by AreaName";
            string XJtiaojianareaid = " a.OperationYear!='' and a.AreaID!='' and (" + year + "-cast(a.OperationYear as int))>=0 and a.ProjectID='" + ProjID + "' and a.DQ!='市辖供电区' and   a.Type='06' and (b.RateVolt=110 or b.RateVolt=66 or b.RateVolt=35)  group by a.AreaID";
            //存放市辖供电区分区名
            IList<string> SXareaname = Services.BaseService.GetList<string>("SelectPSP_Substation_InfoGroupAreaName", SXtiaojianareaname);
            IList<string> SXareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID_type06", SXtiaojianareaid);
            //存放县级供电区分区名
            IList<string> XJareaname = Services.BaseService.GetList<string>("SelectPSP_Substation_InfoGroupAreaName", XJtiaojianareaname);
            IList<string> XJareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID_type06", XJtiaojianareaid);
            //存放市辖分区名称总
            List<string> SXAreaAll = new List<string>();
            //存放县级分区名称总
            List<string> XJAreaAll = new List<string>();
            //把市辖供电区有名字的列表复制到总列表中
            for (int i = 0; i < SXareaname.Count; i++)
            {
                SXAreaAll.Add(SXareaname[i]);
                for (int j = 0; j < SXareaid_List.Count; j++)
                {
                    //通过ID查名称列表，如果重复则删除
                    if (SXareaname[i].ToString() == area_key_id[SXareaid_List[j]].ToString())
                    {
                        SXareaid_List.Remove(SXareaid_List[j]);
                        break;
                    }
                }
            }
            //将市辖供电区中有areaid的项的剩余（上一步删除了重复的部分）部分转成areaname存到总表中
            for (int j = 0; j < SXareaid_List.Count; j++)
            {
                SXAreaAll.Add(area_key_id[SXareaid_List[j]].ToString());
            }

            //把县级供电区有名字的列表复制到总列表中
            for (int i = 0; i < XJareaname.Count; i++)
            {
                XJAreaAll.Add(XJareaname[i]);
                for (int j = 0; j < XJareaid_List.Count; j++)
                {
                    //通过ID查找名称列表，如果重复则删除
                    if (XJareaname[i].ToString() == area_key_id[XJareaid_List[j]].ToString())
                    {
                        XJareaid_List.Remove(XJareaid_List[j]);
                        break;
                    }
                }
            }
            //将县级供电区中有areaid的项的剩余（上一步删除了重复的部分）部分转成areaname存到总表中
            for (int j = 0; j < XJareaid_List.Count; j++)
            {
                XJAreaAll.Add(area_key_id[XJareaid_List[j]].ToString());
            }

            //市辖供电区分区个数
            int SXsum = SXAreaAll.Count;
            //县级供电区分区个数
            int XJsum = XJAreaAll.Count;
            //表标题行数
            int startrow = 3;
            //列标题每项行数
            int length = 10;
            //表格共   行8 列
            rowcount = startrow + (SXsum + XJsum + 2) * length;
            colcount = 8;
            //工作表第一行的标题
            title = "附表8  铜陵市110kV及以下高压配电网主要设备运行年限分布（" + year + "年）";
            //工作表名
            sheetname = "表3-8 附表8";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 80;
            obj_sheet.Columns[1].Width = 110;
            obj_sheet.Columns[2].Width = 90;
            obj_sheet.Columns[3].Width = 90;
            obj_sheet.Columns[4].Width = 90;
            obj_sheet.Columns[5].Width = 90;
            obj_sheet.Columns[6].Width = 90;
            obj_sheet.Columns[7].Width = 90;
            //设定表格行高度
            
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
            //写入标题行和标题列
            //写标题行内容

            //2行标题内容
            obj_sheet.AddSpanCell(1, 0, 2, 1);
            obj_sheet.SetValue(1, 0, "分区类型");
            obj_sheet.AddSpanCell(1, 1, 2, 1);
            obj_sheet.SetValue(1, 1, "分区名称");
            obj_sheet.AddSpanCell(1, 2, 2, 1);
            obj_sheet.SetValue(1, 2, "电压等级（kV）");
            obj_sheet.AddSpanCell(1, 3, 2, 1);
            obj_sheet.SetValue(1, 3, "年限");
            obj_sheet.AddSpanCell(1, 4, 1, 2);
            obj_sheet.SetValue(1, 4, "主变台数");
            obj_sheet.AddSpanCell(1, 6, 1, 2);
            obj_sheet.SetValue(1, 6, "断路器");

            //3行标题内容
            obj_sheet.SetValue(2, 4, "数量（台）");
            obj_sheet.SetValue(2, 5, "比例（%）");
            obj_sheet.SetValue(2, 6, "数量（台）");
            obj_sheet.SetValue(2, 7, "比例（%）");

            //写入市辖供电区合计部分
            Sheet_AddItem(obj_sheet, startrow, "合计");
            //写入市辖供电区部分
            if (SXsum > 0)
            {
                for (int i = 0; i < SXsum; i++)
                {
                    //写入市辖部分列项目
                    Sheet_AddItem(obj_sheet, startrow + (i + 1) * length, SXAreaAll[i]);
                    //写入市辖部分数据
                    string areaid = "";
                    if (area_key_name[SXAreaAll[i].ToString()] != null)
                    {
                        areaid = area_key_name[SXAreaAll[i].ToString()].ToString();
                    }
                    Sheet_AddData(obj_sheet,year,ProjID, startrow + (i + 1) * length, "市辖供电区", SXAreaAll[i], areaid);
                }
                //写入市辖合计部分公式
                fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + length, 4, SXsum, length, startrow, 4, 1);
                fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + length, 6, SXsum, length, startrow, 6, 1);
            }
            else
            {
                //没有市辖分区，直接在合计部分写0
                fc.Sheet_WriteZero(obj_sheet, startrow, 4, length, 4);
            }
            //合并第一列写入市辖供电区
            obj_sheet.AddSpanCell(startrow, 0, (SXsum + 1) * length, 1);
            obj_sheet.SetValue(startrow, 0, "市辖供电区");

            //写入县级供电区合计部分
            Sheet_AddItem(obj_sheet, startrow + (1 + SXsum) * length, "合计");
            //写入县级供电区部分
            if (XJsum > 0)
            {
                for (int i = 0; i < XJsum; i++)
                {
                    //写入县级部分列项目
                    Sheet_AddItem(obj_sheet, startrow + (i + 2 + SXsum) * length, XJAreaAll[i]);
                    //写入县级部分数据
                    string areaid = "";
                    if (area_key_name[XJAreaAll[i].ToString()] != null)
                    {
                        areaid = area_key_name[XJAreaAll[i].ToString()].ToString();
                    }
                    Sheet_AddData(obj_sheet,year,ProjID, startrow + (i + 2 + SXsum) * length, "县级供电区", XJAreaAll[i], areaid);
                }
                //写入县级合计部分公式
                fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (2 + SXsum) * length, 4, XJsum, length, startrow + (1 + SXsum) * length, 4, 1);
                fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (2 + SXsum) * length, 6, XJsum, length, startrow + (1 + SXsum) * length, 6, 1);
            }
            else
            {
                //没有县级分区，直接在合计部分写0
                fc.Sheet_WriteZero(obj_sheet, startrow + (1 + SXsum) * length, 4, length, 4);
            }
            //合并第一列写入县级供电区
            obj_sheet.AddSpanCell(startrow + (1 + SXsum) * length, 0, (XJsum + 1) * length, 1);
            obj_sheet.SetValue(startrow + (1 + SXsum) * length, 0, "县级供电区");
            //写求比例公试
            fc.Sheet_WriteFormula_TwoCol_Percent(obj_sheet, startrow, 4, (2 + SXsum + XJsum) * 2, 5, startrow, 5);
            fc.Sheet_WriteFormula_TwoCol_Percent(obj_sheet, startrow, 6, (2 + SXsum + XJsum) * 2, 5, startrow, 7);

            //锁定表格
            fc.Sheet_Locked(obj_sheet);
        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet, int row, string dq)
        {

            obj_sheet.SetValue(row + 0, 3, "0-5年");
            obj_sheet.SetValue(row + 1, 3, "6-10年");
            obj_sheet.SetValue(row + 2, 3, "11-15年");
            obj_sheet.SetValue(row + 3, 3, "16-20年");
            obj_sheet.SetValue(row + 4, 3, "20年以上");
            obj_sheet.AddSpanCell(row + 0, 2, 5, 1);
            obj_sheet.SetValue(row + 0, 2, "110（66）");

            obj_sheet.SetValue(row + 5, 3, "0-5年");
            obj_sheet.SetValue(row + 6, 3, "6-10年");
            obj_sheet.SetValue(row + 7, 3, "11-15年");
            obj_sheet.SetValue(row + 8, 3, "16-20年");
            obj_sheet.SetValue(row + 9, 3, "20年以上");
            obj_sheet.AddSpanCell(row + 5, 2, 5, 1);
            obj_sheet.SetValue(row + 5, 2, "35");

            obj_sheet.AddSpanCell(row, 1, 10, 1);
            obj_sheet.SetValue(row, 1, dq);
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID,  int row, string dq, string areaname, string areaid)
        {
            //存便电站电压条件
            string BDdianya = "";
            //存断路器电压条件(母线)
            string DLQdianya = "";
            //存放变电站查询条件
            string BDtiaojian = "";
            //存放断路器查询条件
            string DLQtiaojian = "";
            //存放年份条件
            string yeartiaojian = "";
            //存DQ条件
            string BDdqstr = "";
            string DLQdqstr = "";

            if (dq == "市辖供电区")
            {
                BDdqstr = " DQ='市辖供电区' ";
                DLQdqstr = " a.DQ='市辖供电区' ";
            }
            else
            {
                BDdqstr = " DQ!='市辖供电区' ";
                DLQdqstr = " a.DQ!='市辖供电区' ";
            }


            //固定循环2次，一次求电压110（66），一次求电压为35
            for (int j = 0; j < 2; j++)
            {
                if (j == 0)
                {
                    BDdianya = " and (L1=110 or L1=66) ";
                    DLQdianya = " and (b.RateVolt=110 or b.RateVolt=66)";
                }
                else
                {
                    BDdianya = " and L1=35 ";
                    DLQdianya = " and b.RateVolt=35 ";
                }
                ////固定循环5次，每次求一个年限值
                for (int k = 1; k <= 5; k++)
                {
                    if (k == 1)
                    {
                        yeartiaojian = " between 0 and 5  ";
                    }
                    if (k > 1 && k < 5)
                    {
                        yeartiaojian = " between " + ((k - 1) * 5 + 1) + " and " + k * 5;
                    }
                    if (k == 5)
                    {
                        yeartiaojian = " >20 ";
                    }
                    //主变台数
                    BDtiaojian = " S2!='' and (" + year + "-cast(substring(S2,1,4) as int) )" + yeartiaojian + " and AreaID='" + ProjID + "' and " + BDdqstr + "and AreaName='" + areaname + "' and  S4='公用' " + BDdianya;
                    int ZBsum = 0;
                    if (Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", BDtiaojian) != null)
                    {
                        ZBsum = (int)Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", BDtiaojian);
                    }
                    obj_sheet.SetValue(row + k - 1, 4, ZBsum);
                    //断路器台数
                    DLQtiaojian = " a.OperationYear!='' and (" + year + "-cast(a.OperationYear as int) )" + yeartiaojian + " and a.ProjectID='" + ProjID + "' and " + DLQdqstr + " and a.AreaID='" + areaid + "' and a.Type='06' " + DLQdianya;
                    int DLQsum = 0;
                    if (Services.BaseService.GetObject("SelectPSPDEV_CountDLQ", DLQtiaojian) != null)
                    {
                        DLQsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountDLQ", DLQtiaojian);
                    }
                    obj_sheet.SetValue(row + k - 1, 6, DLQsum);

                }
                row = row + 5;
            }
        }
    }
}
