using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet37
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
        public void Build(FarPoint.Win.Spread.SheetView obj_sheet,int year,string ProjID,List<string[]> SxXjName)
        {
            //表格共17 行15 列
            rowcount = 17;
            colcount = 15;
            //工作表第一行的标题
            title = "表3‑7  " + year + "年铜陵市110kV及以下高压配电变电站情况";
            //工作表名
            sheetname = "表3-7";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 80;
            obj_sheet.Columns[2].Width = 80;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 80;
            obj_sheet.Columns[5].Width = 60;
            obj_sheet.Columns[6].Width = 60;
            obj_sheet.Columns[7].Width = 80;
            obj_sheet.Columns[8].Width = 60;
            obj_sheet.Columns[9].Width = 60;
            obj_sheet.Columns[10].Width = 60;
            obj_sheet.Columns[11].Width = 100;
            obj_sheet.Columns[12].Width = 100;
            obj_sheet.Columns[13].Width = 80;
            obj_sheet.Columns[14].Width = 90;
            //设定行高
            obj_sheet.Rows[1].Height = 40;
            obj_sheet.Rows[2].Height = 40;
            //写入标题行和列
            Sheet_AddItem(obj_sheet);
            //写入数据
            Sheet_AddData(obj_sheet,year,ProjID);
            //写入县级合计公式
            fc.Sheet_WriteFormula_RowSum(obj_sheet, 7, 3, 4, 2, 5, 3, 12);
            //写入地区合计公式
            fc.Sheet_WriteFormula_RowSum(obj_sheet, 3, 3, 2, 2, 15, 3, 12);
            //锁定表格
            fc.Sheet_Locked(obj_sheet);
            //设定用户可输入数据单元格格式
            CellType(obj_sheet);           
        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //写标题行内容

            //2行标题内容
            obj_sheet.AddSpanCell(1, 0, 2, 1);
            obj_sheet.SetValue(1, 0, "编号");
            obj_sheet.AddSpanCell(1, 1, 2, 1);
            obj_sheet.SetValue(1, 1, "类型");
            obj_sheet.AddSpanCell(1, 2, 2, 1);
            obj_sheet.SetValue(1, 2, "电压等级（kV）");
            obj_sheet.AddSpanCell(1, 3, 1, 3);
            obj_sheet.SetValue(1, 3, "变电站座数（座）");
            obj_sheet.AddSpanCell(1, 6, 1, 3);
            obj_sheet.SetValue(1, 6, "主变台数（台）");
            obj_sheet.AddSpanCell(1, 9, 1, 2);
            obj_sheet.SetValue(1, 9, "变电容量（MVA）");
            obj_sheet.SetValue(1, 11, "10（20）kV出线间隔总数（回）");
            obj_sheet.SetValue(1, 12, "10（20）已出线间隔总数（回）");
            obj_sheet.SetValue(1, 13, "无功补偿容量（Mvar）");
            obj_sheet.SetValue(1, 14, "10kV平均供电半径（km）");

            //3行标题内容
            obj_sheet.SetValue(2, 3, "公用");
            obj_sheet.SetValue(2, 4, "其中：单线单变座数");
            obj_sheet.SetValue(2, 5, "专用");
            obj_sheet.SetValue(2, 6, "公用");
            obj_sheet.SetValue(2, 7, "其中：有载调压主变");
            obj_sheet.SetValue(2, 8, "专用");
            obj_sheet.SetValue(2, 9, "公用");
            obj_sheet.SetValue(2, 10, "专用");
            obj_sheet.SetValue(2, 11, "公用");
            obj_sheet.SetValue(2, 12, "公用");
            obj_sheet.SetValue(2, 13, "公用");
            obj_sheet.SetValue(2, 14, "公用");
            //写标题列内容

            //1列标题内容
            obj_sheet.AddSpanCell(3, 0, 2, 1);
            obj_sheet.SetValue(3, 0, "1");
            obj_sheet.AddSpanCell(5, 0, 2, 1);
            obj_sheet.SetValue(5, 0, "2");
            obj_sheet.AddSpanCell(7, 0, 2, 1);
            obj_sheet.SetValue(7, 0, "2.1");
            obj_sheet.AddSpanCell(9, 0, 2, 1);
            obj_sheet.SetValue(9, 0, "2.2");
            obj_sheet.AddSpanCell(11, 0, 2, 1);
            obj_sheet.SetValue(11, 0, "2.3");
            obj_sheet.AddSpanCell(13, 0, 2, 1);
            obj_sheet.SetValue(13, 0, "2.4");
            obj_sheet.AddSpanCell(15, 0, 2, 1);
            obj_sheet.SetValue(15, 0, "3");

            //2列标题内容
            obj_sheet.AddSpanCell(3, 1, 2, 1);
            obj_sheet.SetValue(3, 1, "市辖供电区");
            obj_sheet.AddSpanCell(5, 1, 2, 1);
            obj_sheet.SetValue(5, 1, "县级供电区");
            obj_sheet.AddSpanCell(7, 1, 2, 1);
            obj_sheet.SetValue(7, 1, "其中：直供直管");
            obj_sheet.AddSpanCell(9, 1, 2, 1);
            obj_sheet.SetValue(9, 1, "控股");
            obj_sheet.AddSpanCell(11, 1, 2, 1);
            obj_sheet.SetValue(11, 1, "参股");
            obj_sheet.AddSpanCell(13, 1, 2, 1);
            obj_sheet.SetValue(13, 1, "代管");
            obj_sheet.AddSpanCell(15, 1, 2, 1);
            obj_sheet.SetValue(15, 1, "全地区");

            //3列标题内容
            obj_sheet.SetValue(3, 2, "110（66）");
            obj_sheet.SetValue(4, 2, "35");
            obj_sheet.SetValue(5, 2, "110（66）");
            obj_sheet.SetValue(6, 2, "35");
            obj_sheet.SetValue(7, 2, "110（66）");
            obj_sheet.SetValue(8, 2, "35");
            obj_sheet.SetValue(9, 2, "110（66）");
            obj_sheet.SetValue(10, 2, "35");
            obj_sheet.SetValue(11, 2, "110（66）");
            obj_sheet.SetValue(12, 2, "35");
            obj_sheet.SetValue(13, 2, "110（66）");
            obj_sheet.SetValue(14, 2, "35");
            obj_sheet.SetValue(15, 2, "110（66）");
            obj_sheet.SetValue(16, 2, "35");

        }
        public void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID)
        {
            //将类型存入数组，便于读取
            string[] dq ={ "市辖供电区", "县级直供直管", "县级控股", "县级参股", "县级代管" };
            //存放读出的类型
            string dqstr = "";
            //存电压条件
            string dianya = "";
            //存放查询条件
            string BDtiaojian = "";
            //存放公用还是专用
            string GorZ = "";
            //数组下标
            int index = 0;
            for (int row = 3; row < 15; row++)
            {

                //跳过县级合并部分
                if (row == 5)
                {
                    row = row + 2;
                }
                //使行数与类型的数组标识对应起来
                if (row < 5)
                {
                    index = (row - 3) / 2;

                }
                else
                {
                    index = (row - 5) / 2;
                }
                dqstr = dq[index];
                if (row % 2 == 0)
                {
                    dianya = " and L1=35 ";
                }
                else
                {
                    dianya = " and (L1=110 or L1=66) ";
                }
                //计算公用变电站座数
                GorZ = "公用";
                BDtiaojian = " S2!='' and Cast(substring(S2,1,4) as int)<=" + year + " and AreaID='" + ProjID + "' and DQ='" + dqstr + "' and S4='" + GorZ + "' " + dianya;
                int GBDsum = (int)Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", BDtiaojian);
                obj_sheet.SetValue(row, 3, GBDsum);

                //计算专用变电站座数
                GorZ = "专用";
                BDtiaojian = " S2!='' and Cast(substring(S2,1,4) as int)<=" + year + " and AreaID='" + ProjID + "' and DQ='" + dqstr + "' and S4='" + GorZ + "' " + dianya;
                int ZBDsum = (int)Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", BDtiaojian);
                obj_sheet.SetValue(row, 5, ZBDsum);

                //计算公用主变台数（台）
                GorZ = "公用";
                BDtiaojian = " S2!='' and Cast(substring(S2,1,4) as int)<=" + year + " and AreaID='" + ProjID + "' and DQ='" + dqstr + "' and S4='" + GorZ + "' " + dianya;
                int GZBsum = 0;
                if (Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", BDtiaojian) != null)
                {
                    GZBsum = (int)Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", BDtiaojian);
                }
                obj_sheet.SetValue(row, 6, GZBsum);

                //计算专用主变台数（台）
                GorZ = "专用";
                BDtiaojian = " S2!='' and Cast(substring(S2,1,4) as int)<=" + year + " and AreaID='" + ProjID + "' and DQ='" + dqstr + "' and S4='" + GorZ + "' " + dianya;
                int ZZBsum = 0;
                if (Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", BDtiaojian) != null)
                {
                    ZZBsum = (int)Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", BDtiaojian);
                }
                obj_sheet.SetValue(row, 8, ZZBsum);

                //计算公用变电容量
                GorZ = "公用";
                BDtiaojian = " S2!='' and Cast(substring(S2,1,4) as int)<=" + year + " and AreaID='" + ProjID + "' and DQ='" + dqstr + "' and S4='" + GorZ + "' " + dianya;
                double GBDRLsum = 0;
                if (Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", BDtiaojian) != null)
                {
                    GBDRLsum = (double)Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", BDtiaojian);
                }
                obj_sheet.SetValue(row, 9, GBDRLsum);
                //计算专用变电容量
                GorZ = "专用";
                BDtiaojian = " S2!='' and Cast(substring(S2,1,4) as int)<=" + year + " and AreaID='" + ProjID + "' and DQ='" + dqstr + "' and S4='" + GorZ + "' " + dianya;
                double ZBDRLsum = 0;
                if (Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", BDtiaojian) != null)
                {
                    ZBDRLsum = (double)Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", BDtiaojian);
                }
                obj_sheet.SetValue(row, 10, ZBDRLsum);

                //计算公用10（20）kV出线间隔总数（回）
                //GorZ = "公用";
                //BDtiaojian = " S2!='' and L13!='' and Cast(substring(S2,1,4) as int)<=" + year + " and AreaID='" + ProjID + "' and DQ='" + dqstr + "' and S4='" + GorZ + "' " + dianya;
                //int GCXJGsum = 0;
                //if (Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML13", BDtiaojian) != null)
                //{
                //    GCXJGsum = (int)Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML13", BDtiaojian);
                //}
                //obj_sheet.SetValue(row, 11, GCXJGsum);

                //计算公用10（20）已出线间隔总数（回）
                //BDtiaojian = " S2!='' and L14!='' and Cast(substring(S2,1,4) as int)<=" + year + " and AreaID='" + ProjID + "' and DQ='" + dqstr + "' and S4='" + GorZ + "' " + dianya;
                //int GYCXJGsum = 0;
                //if (Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML14", BDtiaojian) != null)
                //{
                //    GYCXJGsum = (int)Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML14", BDtiaojian);
                //}
                //obj_sheet.SetValue(row, 12, GYCXJGsum);

                //计算公用无功补偿容量（Mvar）
                BDtiaojian = " S2!='' and L5!='' and Cast(substring(S2,1,4) as int)<=" + year + " and AreaID='" + ProjID + "' and DQ='" + dqstr + "' and S4='" + GorZ + "' " + dianya;
                double GWGBCsum = 0;
                if (Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML5", BDtiaojian) != null)
                {
                    GWGBCsum = (double)Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML5", BDtiaojian);
                }
                obj_sheet.SetValue(row, 13, GWGBCsum);


            }
        }
        public void CellType(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            for (int row = 3; row < 15; row++)
            {
                if (row != 5 && row != 6)
                {
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, 4);
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, 7);
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, 11);
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, 12);
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, 14);
                }
            }
        }

    }
}
