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
 *  ClassName：Sheet3
 *  Action：附表3 截至2009年底铜陵县电源统计表 的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表 PSP_Substation_Info,数据的写入是按照电压等级降序排列
 * PSPDEV,发电机类型是04
 * 年份：2010-10-25
 */
namespace Itop.VogliteVillageSheets.HighDistributionNetworkParsing
{
    class Sheet3
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private string projectID = "";
        private IList list = null;//大电网电源
        private IList plist = null;//电源

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_3Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int IntColCount = 6;
            int IntRowCount = 7 + 2 + 3;//标题占3行，分区类型占2行，1是其它用
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

            strTitle = "单位：万千伏安 万千瓦 万千瓦时 千伏  台 ";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            strTitle = " 序     号";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 名     称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "项     目";
            PF.CreateSheetView(obj, NextRowMerge, 4, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "      一     ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+=1, IntCol -=2, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "大电网电源点";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "电压等级";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 主变台数 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 变电容量 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 最终规模 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 给下拉列表添加年份
        /// </summary>
        public void InitYear(DevExpress.XtraBars.BarEditItem BE)
        {
            int FirstYear = 1990;
            int EndYear = 2060;
            for (int i = FirstYear; i <= EndYear; i++)
            {
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BE.Edit).Items.Add(FirstYear.ToString());
                FirstYear++;
            }
        }
        /// <summary>
        /// 按照起始和结束年份写入列标题
        /// </summary>
        /// <param name="BeginYear"></param>
        /// <param name="EndYear"></param>
        public void SetColumnsTitle(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int BeginYear, int EndYear,string CurrentYear)
        {
            int intTemp = BeginYear;
            Redraw(FB, obj, BeginYear, EndYear, CurrentYear);

            WriteData(FB, obj, CurrentYear);
        }
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="obj"></param>
        private void Redraw(Itop.Client.Base.FormBase FB,FarPoint.Win.Spread.SheetView obj, int BeginYear, int EndYear,string CurentYear)
        {
            obj.RowCount = 0;
            obj.ColumnCount = 0;
            obj.ColumnCount =6;
            int listCount = 0;
            int pListCount = 0;

            selectDQ(FB, CurentYear);
            if(list!=null)
            {
                listCount = list.Count;
            }
            plist=SelectedDY(FB,CurentYear);
            if(plist!=null)
            {
                pListCount = plist.Count;
            }
            int IntRowCount = listCount + 1 + 2 + 3 + 2 + pListCount;//标题占3行，分区类型占2行,5是从序号2开始以下的行数暂定5行
            obj.RowCount = IntRowCount;
            string strTitle = "";
            IntRow = 3;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            m_PF.LockSheets(obj);

            strTitle = "附表3 截至" + CurentYear + "年底铜陵县电源统计表 ";
            PF.CreateSheetView(obj, IntRow, obj.ColumnCount, 0, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            IntCol = 1;

            strTitle = "单位：万千伏安 万千瓦 万千瓦时 千伏  台 ";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            //右对齐
            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            strTitle = " 序     号";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 名     称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "项     目";
            PF.CreateSheetView(obj, NextRowMerge, 4, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "      一     ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol -= 2, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "大电网电源点";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "电压等级";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 主变台数 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 变电容量 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 最终规模 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 查询地区
        /// </summary>
        private void selectDQ(Itop.Client.Base.FormBase FB,string strYear)
        {
            string sql = null;
            sql = " AreaID='" + FB.ProjectUID + "'and s2<'" + strYear + "' order by L1 desc;";

           try
           {
               list = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql);
           }
           catch (System.Exception e)
           {
               MessageBox.Show(e.Message);
           }
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,string strYear)
        {
            const int IntRow = 6;
            PSP_Substation_Info  psi = null;
            PSP_PowerSubstation_Info ppsi = null;
            int index = 0;
            for(int i=0;i<list.Count;i++)
            {
                psi = (PSP_Substation_Info)list[i];
                for(int j=1;j<obj.ColumnCount;++j)
                {
                    if (j == 1)//大电网电源点
                    {
                        obj.SetValue(i + IntRow, j, psi.AreaName);
                    }
                    if (j == 2)//电压等级
                    {
                        obj.SetValue(i + IntRow, j, psi.L1);
                    }
                    if (j == 3)//主变台数
                    {
                        obj.SetValue(i + IntRow, j, psi.L3);
                    }
                    if (j == 4)//变电容量
                    {
                        obj.SetValue(i + IntRow, j, psi.L2);
                    }
                    if (j == 5)//最终规模
                    {
                        obj.Cells[i + IntRow, j].Locked = false;
                    }
                }
            }
            //最后5行
            for(int i=(IntRow+list.Count);i<obj.RowCount;++i)
            {
                for(int j=0;j<obj.ColumnCount;++j)
                {
                    if(i==obj.RowCount-1)//最后一行
                    {
                        PF.CreateSheetView(obj, 1, 1, i, 1, "小计");
                    }
                    else if(i==(IntRow+list.Count))
                    {
                        if(j==0)
                        {
                            obj.SetValue(i, j, "二");
                        }
                        if(j==1)
                        {
                            obj.SetValue(i, j, "地方电厂及新能源");
                        }
                        if (j == 2)
                        {
                            obj.SetValue(i, j, "机组类型");
                        }
                        if (j == 3)
                        {
                            obj.SetValue(i, j, "机组容量");
                        }
                        if (j == 4)
                        {
                            obj.SetValue(i, j, "接入电压");
                        }
                        if (j == 5)
                        {
                            obj.SetValue(i, j, "年均发电量");
                        }

                    }
                    else//写入数据
                    {
                        ppsi =(PSP_PowerSubstation_Info) plist[index];
                        switch(j)
                        {
                            case 1://地方电厂及新能源
                                obj.SetValue(i, j, ppsi.Title);
                                break;
                            case 2://机组类型
                                obj.SetValue(i, j, ReturnedFDJOfName(FB, strYear, ppsi.UID));
                                break;
                            case 3://机组容量
                                obj.SetValue(i, j, ppsi.S2);
                                break;
                            case 4://接入电压
                                obj.SetValue(i, j, ppsi.S1);
                                break;
                            case 5://年均发电量
                                obj.SetValue(i, j, ppsi.S11);
                                break;

                            default:
                                break;
                        }
                    }
                }
                if (i != obj.RowCount - 1 && i != (IntRow + list.Count))
                {
                    index++;
                }
            }
        }
        /// <summary>
        /// 通过电源找到发电机
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="strYear"></param>
        /// <param name="DYid">电源id</param>
        /// <returns></returns>
        private string ReturnedFDJOfName(Itop.Client.Base.FormBase FB, string strYear,string DYid)
        {
            string temp = "";
            string sql = "a.AreaID='"+FB.ProjectUID+"'and b.type='01'and c.type='04' and a.UID='"+DYid+"'and s3<='"+strYear+"'";
            try
            {
                if (Services.BaseService.GetObject("SelectPSP_PowerSubstationFDJOfName", sql) != null)
                {
                    temp = (string)Services.BaseService.GetObject("SelectPSP_PowerSubstationFDJOfName", sql);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 查找电源
        /// </summary>
        /// <param name="FB"></param>
        /// <returns></returns>
        private IList SelectedDY(Itop.Client.Base.FormBase FB,string strYear)
        {
            IList list = null;
            string sql = "AreaID='" + FB.ProjectUID + "' and s3<='"+strYear+"' ";
            try
            {
                list = Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return list;
        }
    }
}
