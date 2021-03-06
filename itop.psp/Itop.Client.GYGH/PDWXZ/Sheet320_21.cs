using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
using System.Collections;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet320_21
    {

        private class savedata
        {
            public string DQ = "";
            public string areaname = "";
            public string dy = "";
            public object data = null;
        }
        //存放表3-20附表21数据
        List<savedata> SDL320_21 = new List<savedata>();
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
            //用平时传递电压的方式传递列标题内容
            List<string> DY_list = new List<string>();
            DY_list.Add("干线");
            DY_list.Add("支线");
            DY_list.Add("进户线");
            int startrow = 2;
            int length = 3;
            //条件中要加入对AreaID的限制，因为设备使用了AreaID存放其所属线路的UID,所以查询时要避免这种情况,用like +ProjectID来限制
            string SXtiaojianareaid = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and AreaID like '%" + ProjID + "' and  ProjectID='" + ProjID + "' and LineType2='公用' and DQ='市辖供电区'  group by AreaID";
            //计算县级供电区条件
            string XJtiaojianareaid = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and AreaID like '%" + ProjID + "'  and  ProjectID='" + ProjID + "' and LineType2='公用' and DQ!='市辖供电区'  group by AreaID";
            //存放市辖供电区分区名
            IList<string> SXareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID", SXtiaojianareaid);
            //存放县级供电区分区名
            IList<string> XJareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID", XJtiaojianareaid);
            //市辖供电区分区个数
            int SXsum = SXareaid_List.Count;
            //县级供电区分区个数
            int XJsum = XJareaid_List.Count;
            //表标题行数


            //表格共 行4 列
            rowcount =startrow+ (SXsum + XJsum + 2) * length;
            colcount = 4;
            //工作表第一行的标题
            title = "附表21  铜陵市低压线路导线截面一览表（" + year + "年）";
            //工作表名
            sheetname = "表3-20 附表21";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 100;
            obj_sheet.Columns[1].Width = 90;
            obj_sheet.Columns[2].Width = 110;
            obj_sheet.Columns[3].Width = 120;
            //设定表格行高度
            
            obj_sheet.Rows[1].Height = 20;

            //写标题行列内容
            Sheet_AddItem(obj_sheet, area_key_id, DY_list, SXareaid_List, XJareaid_List);
            //写入数据
            //Sheet_AddData(obj_sheet, year, ProjID, SxXjName, DY_list);
            //写入公式
            //市辖合计公式
            if (SXareaid_List.Count > 0)
            {
                fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + length, 3, SXareaid_List.Count, length, startrow, 3, 1);
            }
            //县级合计公式
            if (XJareaid_List.Count>0)
            {
                fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (SXareaid_List.Count + 2) * length, 3, XJareaid_List.Count, length, startrow + (SXareaid_List.Count + 1) * length, 3, 1);
            }
            //锁定表格
            fc.Sheet_Locked(obj_sheet);
            //设定格式
            CellType(obj_sheet);

        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet,Hashtable area_key_id, IList<string> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            //写标题行内容

            //2行标题内容
            obj_sheet.SetValue(1, 0, "分区类型");
            obj_sheet.SetValue(1, 1, "分区名称");
            obj_sheet.SetValue(1, 2, "低压线路类型");
            obj_sheet.SetValue(1, 3, "导线截面（mm2）");
            //写标题列内容
            int dylength = obj_DY_List.Count;

            int startrow = 2;
            for (int i = 0; i < (2 + SXareaid_List.Count + XJareaid_List.Count); i++)
            {
                string areaname = "";
                if (i == 0 || i == (SXareaid_List.Count + 1))
                {
                    areaname = "合计";
                }
                else
                {
                    if (i < SXareaid_List.Count + 1)
                    {
                        if (area_key_id[SXareaid_List[i - 1].ToString()] != null)
                        {
                            areaname = area_key_id[SXareaid_List[i - 1].ToString()].ToString();
                        }
                        else
                        {
                            areaname = "";
                        }

                    }
                    else
                    {
                        if (area_key_id[XJareaid_List[i - SXareaid_List.Count - 2].ToString()] != null)
                        {
                            areaname = area_key_id[XJareaid_List[i - SXareaid_List.Count - 2].ToString()].ToString();
                        }
                        else
                        {
                            areaname = "";
                        }
                    }
                }
                for (int j = 0; j < obj_DY_List.Count; j++)
                {
                    obj_sheet.SetValue(startrow + i * dylength + j, 2, obj_DY_List[j].ToString());
                }
                obj_sheet.AddSpanCell(startrow + i * dylength, 1, dylength, 1);
                obj_sheet.SetValue(startrow + i * dylength, 1, areaname);

            }
            //写第一列数据
            obj_sheet.AddSpanCell(startrow, 0, (SXareaid_List.Count + 1)*dylength, 1);
            obj_sheet.SetValue(startrow, 0, "市辖供电区");
            obj_sheet.AddSpanCell(startrow + (SXareaid_List.Count + 1) * dylength, 0, (XJareaid_List.Count + 1) * dylength, 1);
            obj_sheet.SetValue(startrow + (SXareaid_List.Count + 1) * dylength, 0, "县级供电区");
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
   
        }
        public void CellType(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //市辖部分的行号
            int SXrow = fc.Sheet_Find_Value(obj_sheet, 0, "市辖供电区");
            //县级部分的行号
            int XJrow = fc.Sheet_Find_Value(obj_sheet, 0, "县级供电区");
            //为-1时表示没找到，也就是无分区
            if (SXrow != -1)
            {
                //市辖供电区中第一行第二列的“合计”部分合并的行数就是电压等级数
                int dysum = obj_sheet.Cells[SXrow, 1].RowSpan;
                
                for (int row = SXrow + dysum; row < obj_sheet.RowCount; row++)
                {
                    //县级合计部分跳过
                    if (row == XJrow)
                    {
                        row = row + dysum;
                        if (!(row < obj_sheet.RowCount))
                        {
                            break;
                        }
                    }
                    fc.Sheet_UnLockedandCellNumber(obj_sheet, row, 3);
                    
                }

            }
        }
        public void SaveData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //清空存放数据列表
            SDL320_21.Clear();

            //市辖部分的行号
            int SXrow = fc.Sheet_Find_Value(obj_sheet, 0, "市辖供电区");
            //县级部分的行号
            int XJrow = fc.Sheet_Find_Value(obj_sheet, 0, "县级供电区");
            //为-1时表示没找到，也就是电压等级为0个
            if (SXrow != -1)
            {
                //市辖供电区中第一行第二列的“合计”部分合并的行数就是电压等级数
                int dysum = obj_sheet.Cells[SXrow, 1].RowSpan;

                //存储市辖部分除合计以外的所有行的第4列数据
                for (int row = SXrow + dysum; row < XJrow; row++)
                {
                    savedata tempdata = new savedata();
                    tempdata.DQ = "市辖供电区";
                    tempdata.areaname = fc.Sheet_find_Rownotemptycell(obj_sheet, row, 1);
                    tempdata.dy = obj_sheet.Cells[row, 2].Value.ToString();
                    tempdata.data = obj_sheet.GetValue(row, 3);
                    SDL320_21.Add(tempdata);
                }
                //存储县级部分除合计以外的所有行的第4列数据
                for (int row = XJrow + dysum; row < obj_sheet.RowCount; row++)
                {
                    savedata tempdata = new savedata();
                    tempdata.DQ = "县级供电区";
                    tempdata.areaname = fc.Sheet_find_Rownotemptycell(obj_sheet, row, 1);
                    tempdata.dy = obj_sheet.Cells[row, 2].Value.ToString();
                    tempdata.data = obj_sheet.GetValue(row, 3);
                    SDL320_21.Add(tempdata);
                }
            }
        }
        public void WriteData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            if (SDL320_21.Count != 0)
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
                    for (int i = 0; i < SDL320_21.Count; i++)
                    {
                        if (dq == SDL320_21[i].DQ && areaname == SDL320_21[i].areaname && dy == SDL320_21[i].dy)
                        {
                            obj_sheet.SetValue(row, 3, SDL320_21[i].data);
                            SDL320_21.Remove(SDL320_21[i]);
                            break;
                        }
                    }
                }
            }

        }
    }
}
