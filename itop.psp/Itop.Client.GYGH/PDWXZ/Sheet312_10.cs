using System;
using System.Collections.Generic;
using System.Text;
using Itop.Client.Common;
using System.Collections;
namespace Itop.Client.GYGH.PDWXZ
{
    class Sheet312_10
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
            //线路电压等级根据实际情况查询(中压 6 <= 电压<=20)
            string XLDianYatiaojian = " Type='05' and OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and  Length2>=LineLength  and ProjectID='" + ProjID + "' and LineType2='公用' and RateVolt  between 6 and 20 group by RateVolt";
            IList<double> DY_list = Services.BaseService.GetList<double>("SelectPSPDEV_RateVolt", XLDianYatiaojian);
            int xlsum = DY_list.Count;
            //计算市辖供电区条件 
            string SXtiaojianareaid = " Type='05' and OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and  Length2>=LineLength  and AreaID!='' and  ProjectID='" + ProjID + "' and LineType2='公用' and DQ='市辖供电区' and  RateVolt between 6 and 20  group by AreaID";
            //计算县级供电区条件
            string XJtiaojianareaid = " Type='05' and OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and  Length2>=LineLength  and AreaID!='' and  ProjectID='" + ProjID + "' and LineType2='公用' and DQ!='市辖供电区'  and RateVolt between 6 and 20  group by AreaID";
            //存放市辖供电区分区名
            IList<string> SXareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID", SXtiaojianareaid);
            //存放县级供电区分区名
            IList<string> XJareaid_List = Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID", XJtiaojianareaid);

            //市辖供电区分区个数
            int SXsum = SXareaid_List.Count;
            //县级供电区分区个数
            int XJsum = XJareaid_List.Count;
            //表标题行数
            int startrow = 2;
            //列标题每项是压等级数
            int dylength = xlsum;
            //表格共 行12 列
            rowcount = startrow + (SXsum + XJsum + 2) * dylength;
            colcount = 12;
            //工作表第一行的标题
            title = "附表10  铜陵市中压电缆网络结构情况统计(" + year + "年)";
            //工作表名
            sheetname = "表3-12 附表10";
            //设定工作表行列值及标题和表名
            fc.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //设定表格线
            fc.Sheet_GridandCenter(obj_sheet);
            //设定行列模式，以便写公式使用
            fc.Sheet_Referen_R1C1(obj_sheet);
            //设定表格列宽度
            obj_sheet.Columns[0].Width = 90;
            obj_sheet.Columns[1].Width = 80;
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
            Sheet_AddItem(obj_sheet, area_key_id, DY_list, SXareaid_List, XJareaid_List);
            //写入数据
            Sheet_AddData(obj_sheet,year,ProjID, DY_list, SXareaid_List, XJareaid_List);
            //写入公式
            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, startrow, 3, 4, 8, (SXsum + XJsum + 2) * dylength);
            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, startrow, 3, 5, 9, (SXsum + XJsum + 2) * dylength);
            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, startrow, 3, 6, 10, (SXsum + XJsum + 2) * dylength);
            fc.Sheet_WriteFormula_OneCol_Anotercol_percent(obj_sheet, startrow, 3, 7, 11, (SXsum + XJsum + 2) * dylength);
            //锁定表格
            fc.Sheet_Locked(obj_sheet);

        }
        private void Sheet_AddItem(FarPoint.Win.Spread.SheetView obj_sheet,Hashtable area_key_id, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            //写标题行内容

            //2行标题内容
            obj_sheet.SetValue(1, 0, "分区类型");
            obj_sheet.SetValue(1, 1, "分区名称");
            obj_sheet.SetValue(1, 2, "电压等级（kV）");
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
            fc.Sheet_AddItem_FBonlyDY(obj_sheet, area_key_id, 2, obj_DY_List, SXareaid_List, XJareaid_List);
    
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet, int year, string ProjID, IList<double> obj_DY_List, IList<string> SXareaid_List, IList<string> XJareaid_List)
        {
            //条件
            string XLtiaojian = "";
            //DQ条件
            string DQtiaojian = "";
            int startrow = 2;
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
                            DQtiaojian = " DQ='市辖供电区'";
                            areaid = SXareaid_List[i - 1].ToString();
                        }
                        else
                        {
                            DQtiaojian = " DQ!='市辖供电区'";
                            areaid = XJareaid_List[i - SXareaid_List.Count - 2].ToString();
                        }
                        for (int j = 0; j < obj_DY_List.Count; j++)
                        {
                            XLtiaojian = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and  Length2>=LineLength and ProjectID='" + ProjID + "' and Type='05' and LineType2='公用'  and " + DQtiaojian + " and AreaID='" + areaid + "' and RateVolt=" + obj_DY_List[j].ToString();
                            //线路条数
                            int XLsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian) != null)
                            {
                                XLsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian);
                            }
                            obj_sheet.SetValue(startrow + i * length + j, 3, XLsum);
                            //环网线路条数
                            string hwxltiaojian = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and  Length2>=LineLength and JXFS='环网' and ProjectID='" + ProjID + "' and Type='05' and LineType2='公用'  and " + DQtiaojian + " and AreaID='" + areaid + "' and RateVolt=" + obj_DY_List[j].ToString();
                            int HWXLsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", hwxltiaojian) != null)
                            {
                                HWXLsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", hwxltiaojian);
                            }
                            obj_sheet.SetValue(startrow + i * length + j, 4, HWXLsum);
                            //双射线路条数
                            string ssxltiaojian = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and   Length2>=LineLength and (JXFS='放射型' or JXFS='T型') and ProjectID='" + ProjID + "' and Type='05' and LineType2='公用'  and " + DQtiaojian + " and AreaID='" + areaid + "' and RateVolt=" + obj_DY_List[j].ToString();
                            int SSXLsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", ssxltiaojian) != null)
                            {
                                SSXLsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", ssxltiaojian);
                            }
                            obj_sheet.SetValue(startrow + i * length + j, 5, SSXLsum);

                            //单射线路条数
                            string dsxltiaojian = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and   Length2>=LineLength and JXFS='链式' and ProjectID='" + ProjID + "' and Type='05' and LineType2='公用'  and " + DQtiaojian + " and AreaID='" + areaid + "' and RateVolt=" + obj_DY_List[j].ToString();
                            int DSXLsum = 0;
                            if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", dsxltiaojian) != null)
                            {
                                DSXLsum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", dsxltiaojian);
                            }
                            obj_sheet.SetValue(startrow + i * length + j, 6, DSXLsum);

                            //其它结构线路条数
                            string qtllxltiaojian = " OperationYear!='' and  year(cast(OperationYear as datetime))<=" + year + "  and   Length2>=LineLength and JXFS='其它' and ProjectID='" + ProjID + "' and Type='05' and LineType2='公用' and " + DQtiaojian + " and AreaID='" + areaid + "' and RateVolt=" + obj_DY_List[j].ToString();
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
                        //市辖合计部分公式
                        if (i == 0)
                        {
                            if (SXareaid_List.Count != 0)
                            {
                                fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * length, 3, SXareaid_List.Count, length, startrow + i * length, 3, 5);
                            }
                            //无地区直接在合计部分写0
                            else
                            {
                                fc.Sheet_WriteZero(obj_sheet, startrow + i * length, 3, length, 5);
                            }

                        }
                        //县级合计部分公式
                        else
                        {
                            if (XJareaid_List.Count != 0)
                            {
                                fc.Sheet_WriteFormula_RowSum(obj_sheet, startrow + (i + 1) * length, 3, XJareaid_List.Count, length, startrow + i * length, 3, 5);

                            }
                            //无地区直接在合计部分写0
                            else
                            {
                                fc.Sheet_WriteZero(obj_sheet, startrow + i * length, 3, length, 5);
                            }
                        }
                    }
                }
            }
        }
    }
}
