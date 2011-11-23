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
 *  ClassName：Sheet8
 *  Action：截至2009年底铜陵县35kV变电站联络情况 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：由于这个是行列动态查找标题所以得查询数据才能显示标题。
 *              数据库表是：PSP_Substation_Info，PSPDEV，顺序是先查找行和列的变电站
 *              查询出所有线路的数据的IName字段（接线出口）JName字段（接线入口）
 *               找到母线通过母线找到变电站，这样来关联两个变电站
 * 年份：2010-10-27
 * 修改人：吕静涛
 * 修改日期：2010-11-03
 */

namespace Itop.VogliteVillageSheets.MediumVoltageDistributionNetworkParsing
{
    class Sheet8
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private string projectID = "";
        private IList ColBDZlist = null;
        private IList RowBDZList = null;

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_8Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 1;
            int IntRowCount = 6 + 2 + 3;//标题占3行，分区类型占2行
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

            strTitle = "单位：条";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题
            strTitle = "     变  电  站     ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);


            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 根据下拉菜单从新打印报表
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="strEndYear"></param>
        public void SetColumnsTitle(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string strEndYear)
        {
            SelectBDZ(FB,strEndYear);
            //重绘
            ReDraw(FB, obj, strEndYear);
        }
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="strEndYear"></param>
        private void ReDraw(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string strEndYear)
        {
            PSP_Substation_Info psi=null;
            int IntColCount = ColBDZlist.Count+1;
            int IntRowCount = RowBDZList.Count+1 + 2 + 3;//标题占3行，分区类型占2行

            obj.RowCount = 0;
            obj.ColumnCount = 0;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            string strTitle = "截至" + strEndYear + "年底铜陵县35kV变电站联络情况";
            IntRow = 3;
            obj.SheetName = strTitle;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            IntCol = 1;

            strTitle = "单位：条";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题
            strTitle = "     变  电  站     ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            for (int i = 0; i <ColBDZlist.Count;++i )
            {
                psi=(PSP_Substation_Info)ColBDZlist[i];
                obj.AddSpanCell(IntRow, 1+ i, 2, 1);
                obj.SetValue(IntRow,i+1,psi.Title);
            }
            //行标题
            
            for (int i = 0; i <RowBDZList.Count; ++i)
            {
                psi = (PSP_Substation_Info)RowBDZList[i];
                obj.SetValue(6+i, 0, psi.Title);
            }
            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 6;
            IntCol = 0;

            WriteData(FB, obj, strEndYear, IntRow);
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="strEndYear"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string strEndYear,int IntRow)
        {
            string strRowTitle = "";
            string strColTitle = "";
            for (int i = IntRow; i < RowBDZList.Count + IntRow; ++i)
            {
                strRowTitle = (string)PF.ReturnStr(obj, i, 0);
                for (int j = 1; j < obj.ColumnCount; ++j)
                {
                    //obj.Cells[i, j].Locked = false;
                    strColTitle = obj.GetValue((IntRow-2), j).ToString();
                    obj.SetValue(i, j, selectCount(FB, strEndYear, strRowTitle, strColTitle));
                }
            }
        }
        /// <summary>
        /// 查询变电站数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strEndYear"></param>
        private void SelectBDZ(Itop.Client.Base.FormBase FB, string strEndYear)
        {
            string sql = " AreaID='" + FB.ProjectUID + "' and s2<='" + strEndYear + "'"
                                +" and Areaname Not Like'%部地区' and L1='35'";
            string sql1 = " AreaID='" + FB.ProjectUID + "' and s2<='" + strEndYear + "'"
                                    +" and Areaname Not Like'%部地区' and L1>=110 order by L1 ASC";
            try
            {
                RowBDZList = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql);
                ColBDZlist = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql1);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 选择两个变电站有多少条联络线路
        /// </summary>
        /// <returns></returns>
        private int selectCount(Itop.Client.Base.FormBase FB, string strEndYear,string BDZ1,string BDZ2)
        {
            int temp = 0;
            string sql = "select count(*) from psp_substation_info a,pspdev b,pspdev c,pspdev d,psp_substation_info e where "
                            +" c.projectID='"+FB.ProjectUID+"' and c.OperationYear<='"+strEndYear+"'and "
                           +" ((a.UID=b.SvgUID and a.Title='"+BDZ1+"' and c.IName=b.Name and c.JName= d.Name and d.SvgUID=e.UID and e.Title='"+BDZ2+"') "
                           +" or (a.UID=b.SvgUID and a.Title='"+BDZ1+"' and c.JName=b.Name and c.IName= d.Name and d.SvgUID=e.UID and e.Title='"+BDZ2+"') "
                           +" );";
            try
            {
                temp = (int)Services.BaseService.GetObject("SelectPSPDEV_LLLINE", sql);
            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
            return temp;
        }
    }
}
