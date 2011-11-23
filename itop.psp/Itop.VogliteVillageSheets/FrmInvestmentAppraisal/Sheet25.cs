using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Itop.JournalSheet.Function;
using Itop.Domain.Forecast;
using Itop.Client.Common;
using Itop.Domain.Table;
//////////////////////////////////////////////////////////////////////////
/*
 *  ClassName：Sheet25
 *  Action：2010-2015年铜陵县电网投资估算表 的数据写入

 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：

 * 年份：2010-10-29
 */

namespace Itop.VogliteVillageSheets.FrmInvestmentAppraisal
{
    class Sheet25
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值

        private int NextColMerge = 1;//合并单元格列初始值

        private string projectID = "";
        private IList Llist = null;//低压数据
        private IList HList = null;//高压数据

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_25Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int Temp = 0;
            int IntColCount = 6;
            SelectData(FB);
            int IntRowCount = HList.Count+Llist.Count+1 + 2 + 3;//标题占3行，分区类型占2行

            int BringIntoPproductionTime = 6;//投产时间的列数

            string title = null;

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;

            strTitle = "  单位：亿元";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐

            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题

            IntRow = 4;
            strTitle = "序     号";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "项     目";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "项目性质";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "建设年限";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "建设内容";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "资     金";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 6;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
            //SetLeftTitle();
            WriteData(obj,IntRow);
        }
        private void WriteData(FarPoint.Win.Spread.SheetView obj,int IntRow)
        {
            Ps_Table_TZGS ptz = null;
            int index = 1;
            for(int i=0;i<Llist.Count;++i)//低压
            {
                ptz = (Ps_Table_TZGS)Llist[i];
                for(int j=0;j<obj.ColumnCount;++j)
                {
                    switch (j)
                    {
                        case 0://序号
                            obj.SetValue((i + IntRow), j, index);
                            break;
                        case 1://项目
                            obj.SetValue((i + IntRow), j, ptz.Title);
                            break;
                        case 2://项目性质
                            obj.SetValue((i + IntRow), j, ptz.Col3);
                            break;
                        case 3://建设年限
                            obj.SetValue((i + IntRow), j, ptz.BuildEd);
                            break;
                        case 4://建设内容
                            obj.SetValue((i + IntRow), j, ptz.Col1);
                            break;
                        case 5://资金
                            obj.SetValue((i + IntRow), j, ptz.Amount);
                            break;

                        default:
                            break;
                    }
                }
                index++;
            }

            for (int i = 0; i < HList.Count; ++i)//高压
            {
                ptz = (Ps_Table_TZGS)HList[i];
                for (int j = 0; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 0://序号
                            obj.SetValue((i + IntRow+Llist.Count), j, index);
                            break;
                        case 1://项目
                            obj.SetValue((i + IntRow + Llist.Count), j, ptz.Title);
                            break;
                        case 2://项目性质
                            obj.SetValue((i + IntRow + Llist.Count), j, ptz.Col3);
                            break;
                        case 3://建设年限
                            obj.SetValue((i + IntRow + Llist.Count), j, ptz.BuildEd);
                            break;
                        case 4://建设内容
                            obj.SetValue((i + IntRow + Llist.Count), j, ptz.Col1);
                            break;
                        case 5://资金
                            obj.SetValue((i + IntRow + Llist.Count), j, ptz.Amount);
                            break;

                        default:
                            break;
                    }
                }
                index++;
            }

        }
        /// <summary>
        /// 查询 高压 ，中低压数据 
        /// </summary>
        /// <param name="FB"></param>
        private void SelectData(Itop.Client.Base.FormBase FB)
        {
            string sql = "select a.* FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d"
                        + "  where  a.ID = b.ParentID AND b.Col4 = 'pw-line'"
                        + "  AND a.ID = c.ParentID AND c.Col4 = 'pw-pb'"
                        + "  and a.ID = d.ParentID AND d.Col4 = 'pw-kg'"
                        + "    and a.ProjectID='"+FB.ProjectUID+"'"
                        + "    and a.BuildEd between '2010' and '2015'";
            string sql1 = "Select a.*  FROM  Ps_Table_TZGS a INNER JOIN"
                         +" Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN"
                         +" Ps_Table_TZGS c ON a.ID = c.ParentID"
                         +" where b.Col4 = 'bian' and c.Col4='line'and "
                         + "   a.ProjectID='" + FB.ProjectUID + "'and a.BuildEd between '2010' and '2015'";
            try
            {
                Llist = Services.BaseService.GetList("SelectTZGSEveryField", sql);//低压数据
                HList = Services.BaseService.GetList("SelectTZGSEveryField", sql1);//高压数据
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
