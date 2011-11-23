using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Itop.JournalSheet.Function;
using Itop.Domain.Forecast;
using Itop.Client.Common;
//////////////////////////////////////////////////////////////////////////
/*
 *  ClassName：Sheet30
 *  Action：附表30 本地区“十二五”规划项目资金需求表 的数据写入

 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：数据库 表 ps_table_TZGS,ps_table_TZMX
 * 年份：2010-10-29
 * 修改日期：2010-11-17
 */

namespace Itop.VogliteVillageSheets.PlannedProjectAmount
{
    class Sheet30
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值

        private int NextColMerge = 1;//合并单元格列初始值

        private string projectID = "";
        private IList list = null;
        private IList AreaList = null;

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_30Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int Temp = 0;
            int IntColCount = 11;
            int IntRowCount = 23 + 2 + 3;//标题占3行，分区类型占2行

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

            strTitle = "  单位：万元、km、台";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐

            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题

            IntRow = 4;
            strTitle = "   项          目   ";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "规     模";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge+=2, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "长     度";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge-=2, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "容     量";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "台     数";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);




            strTitle = "2010年";
            PF.CreateSheetView(obj, NextRowMerge+=1, NextColMerge, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "静态投资";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge+=4, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2011年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge-=4, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2012年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge , IntRow, IntCol+=1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2013年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge , IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2014年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge , IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "2015年";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "静态总投资";
            PF.CreateSheetView(obj, NextRowMerge+=1, NextColMerge, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
            SetLeftTitle(obj, IntRow);
            WriteData(FB,obj,6);
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,int IntRow)
        {
            Write110KV(FB, obj,7);
            Write35KV(FB,obj,12);
            Write10KV(FB, obj, 16);
            WriteLastofThree(FB, obj, 25);
        }
        /// <summary>
        /// 最后三行数据写入

        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        private void WriteLastofThree(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow)
        {
            const int intRows = 3;
            for(int i=IntRow;i<IntRow+intRows;++i)
            {
                for(int j=1;j<obj.ColumnCount;++j)
                {
                    if(j==(obj.ColumnCount-1))
                    {
                        obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                    }
                    else
                    {
                        obj.Cells[i, j].Locked = false;
                    }
                }
            }
        }
        /// <summary>
        /// 10kv数据写入
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        private void Write10KV(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow)
        {
            const int intRows = 9;
            string strRow = "";
            for (int i = IntRow; i < IntRow + intRows; ++i)
            {
                strRow = (string)PF.ReturnStr(obj, i, 0);
                for (int j = 1; j < obj.ColumnCount; ++j)
                {
                    if (strRow == "柱上开关")
                    {
                        switch (j)
                        {
                            case 3://台数
                                obj.SetValue(i, j, Return10KVZSKG(FB));
                                break;
                            case 4://2010
                                obj.Cells[i,j].Locked=false;
                                break;
                            case 5://2011
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 6://2012
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 7://2013
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 8://2014
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 9://2015
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }

                    if (strRow == "柱上配电变压器")
                    {
                        switch (j)
                        {
                            case 2://容量
                                obj.SetValue(i, j, Return10KVRLGS(FB,"c.Num6"));
                                break;
                            case 3://台数
                                obj.SetValue(i, j, Return10KVRLGS(FB,"c.Num5"));
                                break;
                            case 4://2010
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 5://2011
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 6://2012
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 7://2013
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 8://2014
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 9://2015
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }

                    if (strRow == "箱式变电站")
                    {
                        switch (j)
                        {
                            case 2://容量
                                obj.SetValue(i, j, Return10KVRLGS(FB, "c.Num4"));
                                break;
                            case 3://台数
                                obj.SetValue(i, j, Return10KVRLGS(FB,"c.Num3"));
                                break;
                            case 4://2010
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 5://2011
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 6://2012
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 7://2013
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 8://2014
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 9://2015
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }
                    if (strRow == "开闭所")
                    {
                        switch (j)
                        {
                            case 3://台数
                                obj.SetValue(i, j, Return10KVRLGS(FB, "d.Num1"));
                                break;
                            case 4://2010
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 5://2011
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 6://2012
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 7://2013
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 8://2014
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 9://2015
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }

                    if (strRow == "环网柜")
                    {
                        switch (j)
                        {
                            case 3://台数
                                obj.SetValue(i, j, Return10KVRLGS(FB, "d.Num2"));
                                break;
                            case 4://2010
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 5://2011
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 6://2012
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 7://2013
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 8://2014
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 9://2015
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }

                    if (strRow == "配电所")
                    {
                        switch (j)
                        {
                            case 2://容量
                                obj.SetValue(i, j, Return10KVRLGS(FB, "c.Num2"));
                                break;
                            case 3://台数
                                obj.SetValue(i, j, Return10KVRLGS(FB, "c.Num1"));
                                break;
                            case 4://2010
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 5://2011
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 6://2012
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 7://2013
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 8://2014
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 9://2015
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }

                    if (strRow == "公用线路")
                    {
                        switch (j)
                        {
                            case 1://长度
                                obj.SetValue(i, j, Return10KVRLGS(FB, "b.Length+b.Length2"));
                                break;
                            case 4://2010
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 5://2011
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 6://2012
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 7://2013
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 8://2014
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 9://2015
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }


                    if (strRow == "其中：架空线路")
                    {
                        switch (j)
                        {
                            case 1://长度
                                obj.SetValue(i, j, Return10KVRLGS(FB, "b.Length"));
                                break;
                            case 4://2010
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 5://2011
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 6://2012
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 7://2013
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 8://2014
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 9://2015
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }

                    if (strRow == "  电缆线路")
                    {
                        switch (j)
                        {
                            case 1://长度
                                obj.SetValue(i, j, Return10KVRLGS(FB, "b.Length2"));
                                break;
                            case 4://2010
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 5://2011
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 6://2012
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 7://2013
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 8://2014
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 9://2015
                                obj.Cells[i, j].Locked = false;
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
        }
        private void Write110KV(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow)
        {
           const int intRows=3;
           string strRow = "";
           IList id = ReturnID(FB, "110");
            for(int i=IntRow;i<IntRow+intRows;++i)
            {
                strRow = (string)PF.ReturnStr(obj, i, 0);
                for(int j=1;j<obj.ColumnCount;++j)
                {
                    if (strRow == "线路")
                    { 
                        switch(j)
                        {
                            case 1://长度
                                obj.SetValue(i, j, ReturnLength(FB,"110"));
                                break;
                            case 4://2010
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2010"));
                                break;
                            case 5://2011
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2011"));
                                break;
                            case 6://2012
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2012"));
                                break;
                            case 7://2013
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2013"));
                                break;
                            case 8://2014
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2014"));
                                break;
                            case 9://2015
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2015"));
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F"+(i+1)+":J"+(i+1)+")";
                                break;
                            default:
                                break;
                        }
                    }

                    if (strRow == "变电所")
                    {
                        switch (j)
                        {
                            case 2://容量
                                obj.SetValue(i, j, ReturnRL(FB, "110"));
                                break;
                            case 3://台数
                                obj.SetValue(i, j, ReturnTS(FB, "110"));
                                break;
                            case 4://2010
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2010"));
                                break;
                            case 5://2011
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2011"));
                                break;
                            case 6://2012
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2012"));
                                break;
                            case 7://2013
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2013"));
                                break;
                            case 8://2014
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2014"));
                                break;
                            case 9://2015
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2015"));
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 写入35kv数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        private void Write35KV(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, int IntRow)
        {
            const int intRows = 3;
            string strRow = "";
            IList id = ReturnID(FB, "35");
            for (int i = IntRow; i < IntRow + intRows; ++i)
            {
                strRow = (string)PF.ReturnStr(obj, i, 0);
                for (int j = 1; j < obj.ColumnCount; ++j)
                {
                    if (strRow == "线路")
                    {
                        switch (j)
                        {
                            case 1://长度
                                obj.SetValue(i, j, ReturnLength(FB, "35"));
                                break;
                            case 4://2010
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2010"));
                                break;
                            case 5://2011
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2011"));
                                break;
                            case 6://2012
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2012"));
                                break;
                            case 7://2013
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2013"));
                                break;
                            case 8://2014
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2014"));
                                break;
                            case 9://2015
                                obj.SetValue(i, j, ReturnYearAmount(id, "line", "y2015"));
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }

                    if (strRow == "变电所")
                    {
                        switch (j)
                        {
                            case 2://容量
                                obj.SetValue(i, j, ReturnRL(FB, "35"));
                                break;
                            case 3://台数
                                obj.SetValue(i, j, ReturnTS(FB, "35"));
                                break;
                            case 4://2010
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2010"));
                                break;
                            case 5://2011
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2011"));
                                break;
                            case 6://2012
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2012"));
                                break;
                            case 7://2013
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2013"));
                                break;
                            case 8://2014
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2014"));
                                break;
                            case 9://2015
                                obj.SetValue(i, j, ReturnYearAmount(id, "sub", "y2015"));
                                break;
                            case 10://静态总投资

                                obj.Cells[i, j].Formula = "sum(F" + (i + 1) + ":J" + (i + 1) + ")";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 返回10kv容量或个数

        /// </summary>
        /// <param name="FB"></param>
        /// <returns></returns>
        private double Return10KVRLGS(Itop.Client.Base.FormBase FB,string strType)
        {
            double temp = 0;
            string sql = "      select sum(" + strType + ")"
                              + "            FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d"
                             + "             where  a.ID = b.ParentID AND b.Col4 = 'pw-line'"
                               + "           AND a.ID = c.ParentID AND c.Col4 = 'pw-pb'"
                               + "           and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and"
                                + "    a.ProjectID='" + FB.ProjectUID + "'and cast(substring(a.BianInfo,1,"
                                + "    charindex('@',a.BianInfo,0)-1) as int)= 10 ";
            try
            {
                if (Services.BaseService.GetObject("SelectTZGSOf_dle", sql) != null)
                {
                    temp = (double)Services.BaseService.GetObject("SelectTZGSOf_dle", sql);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return temp;
        }

        /// <summary>
        /// 返回10kv柱上开关数
        /// </summary>
        /// <param name="FB"></param>
        /// <returns></returns>
        private double Return10KVZSKG(Itop.Client.Base.FormBase FB)
        {
            string sql = "";
            double temp = 0;
            try
            {
                if (Services.BaseService.GetObject("SelectTZGSPWZSKG", sql)!=null)
                {
                    temp = (double)Services.BaseService.GetObject("SelectTZGSPWZSKG", sql);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 主网统计变电台数
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="RateVolt"></param>
        /// <returns></returns>
        private double ReturnTS(Itop.Client.Base.FormBase FB, string RateVolt)
        {
            string sql = "and "
                           +" a.ProjectID='"+FB.ProjectUID+"'and cast(substring(a.BianInfo,1,"
                            +" charindex('@',a.BianInfo,0)-1) as int)="+RateVolt;
            double temp = 0.0;
            try
            {
                if (Services.BaseService.GetObject("SelectTZGSsubTS", sql)!=null)
                {
                    temp = (double)Services.BaseService.GetObject("SelectTZGSsubTS", sql);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 主网统计变电容量
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="RateVolt"></param>
        /// <returns></returns>
        private double ReturnRL(Itop.Client.Base.FormBase FB, string RateVolt)
        {
            string sql = "and "
                           +" a.ProjectID='"+FB.ProjectUID+"'and cast(substring(a.BianInfo,1,"
                            +" charindex('@',a.BianInfo,0)-1) as int)="+RateVolt;
            double temp = 0.0;
            try
            {
                if (Services.BaseService.GetObject("SelectTZGSsubLL", sql)!=null)
                {
                    temp = (double)Services.BaseService.GetObject("SelectTZGSsubLL", sql);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 返回当前年份的静态投资额
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="Typeqf"></param>
        /// <returns></returns>
        private double ReturnYearAmount(IList parentID,string Typeqf,string CurrentYear)
        {
            double temp = 0.0;
            for(int i=0;i<parentID.Count;++i)
            {
                string sql = "select sum(" + CurrentYear + ")  from ps_table_TZMX where projectID='" + parentID[i] + "' and Typeqf='" + Typeqf + "'";
                try
                {
                    if (Services.BaseService.GetObject("SelectTZMXOfYearAmount", sql) != null)
                    {
                        temp += (double)Services.BaseService.GetObject("SelectTZMXOfYearAmount", sql);
                    }
                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return temp;
        }
        /// <summary>
        /// 取到主网主记录id
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="RateVolt"></param>
        /// <returns></returns>
        private IList ReturnID(Itop.Client.Base.FormBase FB, string RateVolt)
        {
            IList id = null;
            string sql = "select a.ID from ps_table_TZGS a inner join ps_table_TZGS b on a.Id=b.parentID "
                            +"    inner join ps_table_TZGS c on a.ID=c.parentID"
                            +"    where b.Col4 = 'bian' and c.Col4='line'and "
                            +"    a.ProjectID='"+FB.ProjectUID+"'and cast(substring(a.BianInfo,1,"
                          +"      charindex('@',a.BianInfo,0)-1) as int)= "+RateVolt;
            try
            {
                if (Services.BaseService.GetObject("SelectTZGSEvery", sql)!=null)
                {
                    id = Services.BaseService.GetList("SelectTZGSEvery", sql);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return id;
        }
        /// <summary>
        /// 返回长度
        /// </summary>
        /// <returns></returns>
        private double ReturnLength(Itop.Client.Base.FormBase FB, string RateVolt)
        {
            double temp = 0.0;
            string sql = "and "
                              +"      a.ProjectID='"+FB.ProjectUID+"'and cast(substring(a.BianInfo,1,"
                              +"      charindex('@',a.BianInfo,0)-1) as int)="+RateVolt;
            try
            {
                if (Services.BaseService.GetObject("SelectTZGSlinelength", sql)!=null)
                {
                    temp = (double)Services.BaseService.GetObject("SelectTZGSlinelength", sql);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return temp;
        }
        /// <summary>
        /// 左侧标题
        /// </summary>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow)
        {
            obj.SetValue(IntRow+=1, 0, "（一）110kV电网（合计）");
            obj.SetValue(IntRow += 1, 0, "线路");
            obj.SetValue(IntRow += 1, 0, "变电所");
            obj.SetValue(IntRow += 1, 0, "（二）农村35kV及以下电网（合计）");
            obj.SetValue(IntRow += 1, 0, "其中：农村35kV电网");
            obj.SetValue(IntRow += 1, 0, "铜陵县");
            obj.SetValue(IntRow += 1, 0, "线路");
            obj.SetValue(IntRow += 1, 0, "变电所");
            obj.SetValue(IntRow += 1, 0, "农村10kV电网");
            obj.SetValue(IntRow += 1, 0, "铜陵县");
            obj.SetValue(IntRow += 1, 0, "柱上开关");
            obj.SetValue(IntRow += 1, 0, "柱上配电变压器");
            obj.SetValue(IntRow += 1, 0, "箱式变电站");
            obj.SetValue(IntRow += 1, 0, "开闭所");
            obj.SetValue(IntRow += 1, 0, "环网柜");
            obj.SetValue(IntRow += 1, 0, "配电所");
            obj.SetValue(IntRow += 1, 0, "公用线路");
            obj.SetValue(IntRow += 1, 0, "其中：架空线路");
            obj.SetValue(IntRow += 1, 0, "  电缆线路");
            obj.SetValue(IntRow += 1, 0, "  农村公用低压网");
            obj.SetValue(IntRow += 1, 0, "  （三）无功补偿");
            obj.SetValue(IntRow += 1, 0, " （四）配网自动化");

        }
    }
}
