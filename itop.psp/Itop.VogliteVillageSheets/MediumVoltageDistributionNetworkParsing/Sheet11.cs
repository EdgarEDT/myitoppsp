using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Itop.JournalSheet.Function;
using Itop.Domain.Forecast;
using Itop.Client.Common;
using Itop.Domain.Graphics;
//////////////////////////////////////////////////////////////////////////
/*
 *  ClassName：Sheet11
 *  Action：附表11 铜陵县110kV及35kV变电站主变故障或检修“N-1”通过情况 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表需要更新数据,所用数据库表PSP_Substation_Info
 * 年份：2010-10-27
 * 修改时间：2010-11-15
 */


namespace Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing
{
    class Sheet11
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private string projectID = "";
        private IList list = null;

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_11Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 8;
            SelectedBDZ(FB);
            int IntRowCount =list.Count+1 + 2 + 3;//标题占3行，分区类型占2行
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

            strTitle = "单位：万千瓦";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题
            strTitle = "  序     号  ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < (IntColCount - 1); ++i)
            {
                switch (i)
                {
                    case 0:
                        strTitle = " 变电站   ";
                        break;
                    case 1:
                        strTitle = " 该站所带最大负荷   ";
                        break;
                    case 2:
                        strTitle = "  主变容量  ";
                        break;
                    case 3:
                        strTitle = "   N-1需转移的负荷 ";
                        break;
                    case 4:
                        strTitle = " 联络线路可转带负荷   ";
                        break;
                    case 5:
                        strTitle = "  主变能转带负荷  ";
                        break;
                    case 6:
                        strTitle = "  是否通过校验  ";
                        break;
                    case 7:
                        strTitle = "  损失负荷  ";
                        break;

                    default:
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }
  
            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 6;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
            WriteData(FB, obj, IntRow);
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,int IntRow)
        {
            PSP_Substation_Info pif=null;
            int temp = 1;
            for(int i=IntRow;i<obj.RowCount ;++i)
            {
                pif = (PSP_Substation_Info)list[i - IntRow];
                for(int j=0;j<obj.ColumnCount;++j)
                {
                    switch(j)
                    {
                        case 0://序号
                            obj.SetValue(i, j, temp);
                            break;
                        case 1://变电站
                            obj.SetValue(i, j, pif.Title);
                            break;
                        case 2://该站所带最大负荷
                            obj.SetValue(i, j, pif.L9);
                            break;
                        case 3://主变容量
                            obj.SetValue(i, j, pif.L2);
                            break;
                        case 4://N-1需转移的负荷
                            obj.Cells[i,j].Locked=false;
                            break;
                        case 5://联络线路可转带负荷
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 6://主变能转带负荷
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 7://是否通过校验
                            obj.SetValue(i, j, pif.S1);
                            break;
                        case 8://损失负荷
                            obj.Cells[i, j].Locked = false;
                            break;
                        default:
                            break;
                    }
                }
                temp++;
            }
        }
        /// <summary>
        /// 查找变电站数据
        /// </summary>
        /// <param name="FB"></param>
        private void SelectedBDZ(Itop.Client.Base.FormBase FB)
        {
            string sql = " AreaID='"+FB.ProjectUID+"' and L1='110' or L1='35'"
                          +"  order by L1 ASC";
            try
            {
                list = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
