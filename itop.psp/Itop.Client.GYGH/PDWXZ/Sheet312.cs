using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet312
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
        {//线路电压等级根据实际情况查询(中压 6 <= 电压<=20)
            string XLDianYatiaojian = " Type='05' and OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and  Length2>=LineLength  and ProjectID='" + ProjID + "' and LineType2='公用' and RateVolt  between 6 and 20 group by RateVolt";
            IList<double> DY_list = Services.BaseService.GetList<double>("SelectPSPDEV_RateVolt", XLDianYatiaojian);
            int xlsum = DY_list.Count;
            //表标题行数
            int startrow = 2;
            //列标题每项行数
            int dylength = xlsum;

            //表格共 行12 列
            rowcount = startrow + 7 * dylength;
            colcount = 12;
            //工作表第一行的标题
            title = "表3‑12  " + year + "年铜陵市中压电缆网络结构情况统计";
            //工作表名
            sheetname = "表3-12";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 100;
            obj_sheet.Columns[2].Width = 60;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 60;
            obj_sheet.Columns[5].Width = 80;
            obj_sheet.Columns[6].Width = 60;
            obj_sheet.Columns[7].Width = 60;
            obj_sheet.Columns[8].Width = 60;
            obj_sheet.Columns[9].Width = 60;
            obj_sheet.Columns[10].Width = 60;
            obj_sheet.Columns[11].Width = 80;
            //设定表格行高度
            
            obj_sheet.Rows[1].Height = 40;
           
            //写标题行列内容
            Sheet_AddItem(obj_sheet, SxXjName, DY_list);
            //写入数据
            Sheet_AddData(obj_sheet, year, ProjID, SxXjName, DY_list);
            //写入公式
            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, startrow, 3, 4, 8, dylength * 7);
            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, startrow, 3, 5, 9, dylength * 7);
            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, startrow, 3, 6, 10, dylength * 7);
            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, startrow, 3, 7, 11, dylength * 7);
            //锁定表格
            fc.Sheet_Locked(obj_sheet);
        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet, List<string[]> SxXjName, IList<double> obj_DY_List)
        {
            //写标题行内容

            //2行标题内容
            obj_sheet.SetValue(1, 0, "编号");
            obj_sheet.SetValue(1, 1, "类型");
            obj_sheet.SetValue(1, 2, "电压等级 （kV）");
            obj_sheet.SetValue(1, 3, "线路条数（条）");
            obj_sheet.SetValue(1, 4, "环网线路条数");
            obj_sheet.SetValue(1, 5, "双射线路条数");
            obj_sheet.SetValue(1, 6, "单射线路条数");
            obj_sheet.SetValue(1, 7, "其它结构线路条数");
            obj_sheet.SetValue(1, 8, "环网比例（%）");
            obj_sheet.SetValue(1, 9, "双射比例（%）");
            obj_sheet.SetValue(1, 10, "单射比例（%）");
            obj_sheet.SetValue(1, 11, "其它结构比例（%）");
            //写标题列内容
            fc.Sheet_AddItem_ZBonlyDY(obj_sheet, SxXjName, 2, obj_DY_List);
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, List<string[]> SxXjName, IList<double> obj_DY_List)
        {
            //添加数据

            //条件
            string XLtiaojian = "";
            int startrow = 2;
            int length = obj_DY_List.Count;
            if (obj_DY_List.Count > 0)
            {
                for (int i = 0; i < SxXjName.Count; i++)
                {
                    //合计部分不用计算
                    if (SxXjName[i][2].ToString() != "合计")
                    {
                        for (int j = 0; j < obj_DY_List.Count; j++)
                        {

                            XLtiaojian = "  OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and  Length2>=LineLength and ProjectID='" + ProjID + "' and Type='05' and LineType2='公用'  and DQ='" + SxXjName[i][2] + "' and RateVolt=" + obj_DY_List[j].ToString();
                            //线路条数
                            int XLsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian) != null)
                            {
                                XLsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian);
                            }
                            obj_sheet.SetValue(startrow + i * length + j, 3, XLsum);
                            //环网线路条数
                            string hwxltiaojian = "  OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and  Length2>=LineLength and JXFS='环网' and ProjectID='" + ProjID + "' and Type='05' and LineType2='公用'  and DQ='" + SxXjName[i][2] + "' and RateVolt=" + obj_DY_List[j].ToString();
                            int HWXLsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", hwxltiaojian) != null)
                            {
                                HWXLsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", hwxltiaojian);
                            }
                            obj_sheet.SetValue(startrow + i * length + j, 4, HWXLsum);
                            //双射线路条数
                            string ssxltiaojian = "  OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and   Length2>=LineLength and (JXFS='放射型' or JXFS='T型') and ProjectID='" + ProjID + "' and Type='05' and LineType2='公用'  and DQ='" + SxXjName[i][2] + "' and RateVolt=" + obj_DY_List[j].ToString();
                            int SSXLsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", ssxltiaojian) != null)
                            {
                                SSXLsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", ssxltiaojian);
                            }
                            obj_sheet.SetValue(startrow + i * length + j, 5, SSXLsum);

                            //单射线路条数
                            string dsxltiaojian = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and   Length2>=LineLength and JXFS='链式' and ProjectID='" + ProjID + "' and Type='05' and LineType2='公用'  and DQ='" + SxXjName[i][2] + "' and RateVolt=" + obj_DY_List[j].ToString();
                            int DSXLsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", dsxltiaojian) != null)
                            {
                                DSXLsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", dsxltiaojian);
                            }
                            obj_sheet.SetValue(startrow + i * length + j, 6, DSXLsum);

                            //其它结构线路条数
                            string qtllxltiaojian = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and   Length2>=LineLength and JXFS='其它' and ProjectID='" + ProjID + "' and Type='05' and LineType2='公用' and DQ='" + SxXjName[i][2] + "' and RateVolt=" + obj_DY_List[j].ToString();
                            int QTXLsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", qtllxltiaojian) != null)
                            {
                                QTXLsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", qtllxltiaojian);
                            }
                            obj_sheet.SetValue(startrow + i * length + j, 7, QTXLsum);
                        }
                    }
                    else
                    {
                        //县级合计部分公式
                        if (i == 1)
                        {
                            fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * length, 3, 4, length, startrow + i * length, 3, 5);
                        }
                        //全地区合计部分公式
                        else
                        {
                            fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow, 3, 2, length, startrow + i * length, 3, 5);

                        }
                    }

                }
            }
        }

    }
}
