using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

//using Itop.Domain.Table;
using Itop.Client.Common;
using Itop.Domain.HistoryValue;
/******************************************************************************************************
 *  ClassName：Sheet4_1_1
 *  Action：,sheet4_1附表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 说明： 
 * psp_Types：在这个表通过ProjectID，和Flag=2寻找地区
 * psp_values values表的Typeid=表Types的id
 * psp_Years:查年份用 
 * 修改
 * 时间：2010-10-11
 *********************************************************************************************************/
namespace Itop.JournalSheet.Function4
{
    class Sheet4_1_1
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private const int flag = 2;//标志为2
        //private int GlobalFirstYear = 0;
        //private int GlobalEndYear = 0;
        private int yearCount = 0;
        private IList City = null;
        private IList County = null;
        IList<PSP_Years> listYear = null;
        private string projectId = null;

        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();
        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.JournalSheet.Function10.Sheet10_1 S10_1 = new Itop.JournalSheet.Function10.Sheet10_1() ;
        private Schedule sc = new Schedule();
        //下面的结构体装入的是每层目录信息
        private struct Schedule 
        {
            public int OneClassID;
            public int TwoClassID;
            public int ThreeClassID;
            public int FourClassID;
            public string[] strTitle;
            
           public  PSP_Years[] py ;//装入起始年分和结束年份用来判断是否减去4年（2001-2004）
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitTitle()
        {
            sc.strTitle = new string[2];
            sc.strTitle[0] = "市辖供电区";
            sc.strTitle[1] = "县级供电区";
            sc.OneClassID = 0;
            sc.TwoClassID = 0;
            sc.ThreeClassID = 0;
            sc.FourClassID = 0;

        }
        /// <summary>
        /// 初始化存放年份的数组
        /// </summary>
        private void InitArray(int year)
        {
            int index = 0;
            sc.py = new PSP_Years[year];
            PSP_Years listI = null;
            for (int i = 0; i < (listYear.Count ); ++i)
            {
                listI= (PSP_Years)listYear[i];
                if(listI.Year<2001||listI.Year>2004)
                {
                    sc.py[index] = listI;
                    index++;
                }
                
            }
        }
        /// <summary>
        /// 加载10_1表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet4_1_1Title( Itop.Client.Base.FormBase FB,FarPoint.Win.Spread.SheetView obj, string Title)
        {
            PSP_Years[] listI=new PSP_Years[2];
            InitTitle();
            this.SelectYear();//查询年份
            SelectDQ(FB, sc.strTitle[0]);//查询地区
            SelectDQ(FB, sc.strTitle[1]);
            listI[0] = (PSP_Years)listYear[0];
            listI[1] = (PSP_Years)listYear[listYear.Count - 1];

            yearCount = listYear.Count - 4+2;//年份=程序中现实的年份-4年（2001年到2004年）+2是有两行固定标题
            int IntColCount = 18;
            int IntRowCount = (City.Count + County.Count) * (yearCount ) + 4 + 3;//标题占3行，类型占4行，(yearCount+2):是加两个固定列
            string title = null;

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画线，居中
            S10_1.ColReadOnly(obj, IntColCount);
            //obj.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;

            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;


            strTitle = " 分     区 ";
            PF.CreateSheetView(obj, NextRowMerge+=1 , NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 类     型 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+=2, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 分     区 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow-=2, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 名     称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+=2, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 年      份";
            PF.CreateSheetView(obj, NextRowMerge+=2, NextColMerge, IntRow-=2, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "全  社  会 ";
            PF.CreateSheetView(obj, NextRowMerge-=3, NextColMerge+=1, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 最大负荷 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 规 模 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge-=1, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " (MW) ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+=1, IntCol , strTitle);
            //PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 增长率 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge , IntRow -=1, IntCol+=1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "(%)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
            //PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < 3; ++i)
            {
                if(i==0)
                    strTitle = " 网供最大负荷";
                if(i==1)
                    strTitle = " 供电量";
                if (i == 2)
                    strTitle = " 全社会用电量";

                PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge += 1, IntRow -= 3, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

                strTitle = " 规 模 ";
                PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge -= 1, IntRow += 2, IntCol, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

                strTitle = " (MW) ";
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
                //PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

                strTitle = " 增长率 ";
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow-=1, IntCol += 1, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

                strTitle = "(%)";
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow += 1, IntCol, strTitle);
                //PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }

            strTitle = "三产及居民用电量 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge+=3, IntRow-=3, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "(亿kWh) ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "一     产";
            PF.CreateSheetView(obj, NextRowMerge+=1, NextColMerge-=3, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "二     产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow , IntCol+=1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "三     产 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow , IntCol+=1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "居     民";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow , IntCol+=1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "人均用电量";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow-=2, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "(kWh/人)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+=2, IntCol , strTitle);
            //PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "人均生活用电量";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow-=2, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "(kWh/人)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+=2, IntCol, strTitle);
            //PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);


            strTitle = "农村居民人均生活用电量";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow-=2, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "(kWh/人)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow+=2, IntCol, strTitle);
            //PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;
            IntRow = 7;
            IntCol = 1;

            //if (listI[0].Year > 2000 || listI[1].Year < 2009)
            //{
            //    MessageBox.Show("“在分区县供电实绩中”选择年份必须小于等于2000年和大于等于2009年！！！");
            //    return;
            //}
            //else
            //{
            InitArray(yearCount - 2);
                SetLeftTitle(obj, IntRow, IntCol, listI[1].Year);//左侧标题
                WriteData(obj, IntRow, IntCol, listI[1].Year);//数据
            //}
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 左侧标题
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        /// <param name="endYear"></param>
        private void SetLeftTitle(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol,int endYear)
        {
            int indexYear = 0;

            for (int i = 0; i < 2; ++i)//分区类型
            {
                if(i==0)
                    PF.CreateSheetView(obj, (City.Count * yearCount), NextColMerge, i+IntRow, 0, sc.strTitle[0]);
                if(i==1)
                    PF.CreateSheetView(obj, (County.Count * yearCount), NextColMerge, (IntRow+i*City.Count*yearCount), 0, sc.strTitle[1]);
            }
            for (int i = 0; i < (City.Count);++i )//分区名称市辖
            {
                PF.CreateSheetView(obj, (yearCount), NextColMerge, i*yearCount+IntRow, 1, City[i].ToString());
            }
            for (int i = 0;i<County.Count ; ++i)//分区名称县级
            {
                PF.CreateSheetView(obj, (yearCount), NextColMerge, (i * yearCount + IntRow + City.Count * yearCount), 1, County[i].ToString());
            }
            for (int j = 0; j < City.Count + County.Count; ++j)//年份
            {
                for (int i = 0; i < yearCount; ++i)
                {
                    if (i == yearCount - 1)
                    {
                        indexYear = 0;
                        if (endYear == 2010)
                        {
                            PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 2, "“十一五”年均增长率");
                        }
                        if (endYear == 2009)
                        {
                            PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 2, "2006-2009年均增长率");
                        }
                    }
                    else if (i == yearCount - 2)
                    {
                        PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow + j * yearCount), 2, "“十五”年均增长率");
                    }
                    else
                    {
                        PF.CreateSheetView(obj, (1), NextColMerge, (i + IntRow+j*yearCount), 2, sc.py[indexYear].Year.ToString());
                        indexYear++;
                    }
                }
            }
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        /// <param name="endYear"></param>
        private void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol,int endYear)
        {
            double value=0;
            string strType = null;
            string DQ = null;
            int year = 0;
            double IntTemp=0;
            int IntTempRow = 0;
            int IntTempRow1 = 0;
            string temp = null;
            string Nextyear = null;
            for(int i=IntRow;i<obj.RowCount;++i)
            {
                strType = (string)PF.ReturnStr(obj, i, 0);
                DQ =(string ) PF.ReturnStr(obj, i, 1);
                temp = (string)PF.ReturnStr(obj, i, 2);//年份
                int.TryParse(temp, out year);
                for(int j=3;j<obj.ColumnCount;++j)//从第三列开始写入
                {
                    //不需要写入数据的4,6,8,10,15,16列,计算出来数据
                    switch(j)
                    {
                        case 3://全社会最大用电负荷/规模（MW）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }else 
                            {
                                value = SelectValue(DQ, strType, year, "全社会最大用电负荷");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 4://全社会最大用电负荷/增长率（%）
                            if(temp=="2000")
                            {

                            }else
                            {
                                Nextyear = (string)PF.ReturnStr(obj, i-1, 2);//上一层的年份
                                if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                                {
                                    if (temp == "“十五”年均增长率")
                                    {
                                        IntTempRow=ReturnRow(obj, "2000", i, 2);
                                        IntTempRow1=ReturnRow(obj, "2005", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(D" + (IntTempRow1+1) + "/D" + (IntTempRow+1) + ",0.2)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "2006-2009年均增长率")
                                    {
                                        IntTempRow = ReturnRow(obj, "2006", i, 2);
                                        IntTempRow1 = ReturnRow(obj, "2009", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(D" + (1+IntTempRow1) + "/D" + (IntTempRow+1) + ",1/3)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "“十一五”年均增长率")
                                    {
                                        IntTempRow = ReturnRow(obj, "2005", i, 2);
                                        IntTempRow1 = ReturnRow(obj, "2010", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(D" + (IntTempRow1+1) + "/D" + (IntTempRow+1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                }
                                else
                                {
                                    IntTemp=ReturnYearDifference(temp, Nextyear);
                                    IntTemp=1 / IntTemp;
                                    obj.Cells[i, j].Formula = "POWER(D" + (i+1) + "/D" + (i ) + "," + IntTemp + ")-1";
                                    obj.Cells[i, j].CellType = PC;//%
                                }
                            }
                            break;
                        case 5://网供最大负荷/规模（MW）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                value = SelectValue(DQ, strType, year, "网供最大负荷");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 6://网供最大负荷/增长率（%）
                            if (temp == "2000")
                            {

                            }
                            else
                            {
                                Nextyear = (string)PF.ReturnStr(obj, i - 1, 2);//上一层的年份
                                if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                                {
                                    if (temp == "“十五”年均增长率")
                                    {
                                        IntTempRow = ReturnRow(obj, "2000", i, 2);
                                        IntTempRow1 = ReturnRow(obj, "2005", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(F" + (IntTempRow1 + 1) + "/F" + (IntTempRow + 1) + ",0.2)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "2006-2009年均增长率")
                                    {
                                        IntTempRow = ReturnRow(obj, "2006", i, 2);
                                        IntTempRow1 = ReturnRow(obj, "2009", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(F" + (1 + IntTempRow1) + "/F" + (IntTempRow + 1) + ",1/3)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "“十一五”年均增长率")
                                    {
                                        IntTempRow = ReturnRow(obj, "2005", i, 2);
                                        IntTempRow1 = ReturnRow(obj, "2010", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(F" + (IntTempRow1 + 1) + "/F" + (IntTempRow + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                }
                                else
                                {
                                    IntTemp = ReturnYearDifference(temp, Nextyear);
                                    IntTemp = 1 / IntTemp;
                                    obj.Cells[i, j].Formula = "POWER(F" + (i + 1) + "/F" + (i) + "," + IntTemp + ")-1";
                                    obj.Cells[i, j].CellType = PC;//%
                                }
                            }
                            break;
                        case 7://供电量/规模（MW）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                value = SelectValue(DQ, strType, year, "供电量");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 8://供电量/增长率（%）
                            if (temp == "2000")
                            {

                            }
                            else
                            {
                                Nextyear = (string)PF.ReturnStr(obj, i - 1, 2);//上一层的年份
                                if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                                {
                                    if (temp == "“十五”年均增长率")
                                    {
                                        IntTempRow = ReturnRow(obj, "2000", i, 2);
                                        IntTempRow1 = ReturnRow(obj, "2005", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(H" + (IntTempRow1 + 1) + "/H" + (IntTempRow + 1) + ",0.2)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "2006-2009年均增长率")
                                    {
                                        IntTempRow = ReturnRow(obj, "2006", i, 2);
                                        IntTempRow1 = ReturnRow(obj, "2009", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(H" + (1 + IntTempRow1) + "/H" + (IntTempRow + 1) + ",1/3)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "“十一五”年均增长率")
                                    {
                                        IntTempRow = ReturnRow(obj, "2005", i, 2);
                                        IntTempRow1 = ReturnRow(obj, "2010", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(H" + (IntTempRow1 + 1) + "/H" + (IntTempRow + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                }
                                else
                                {
                                    IntTemp = ReturnYearDifference(temp, Nextyear);
                                    IntTemp = 1 / IntTemp;
                                    obj.Cells[i, j].Formula = "POWER(H" + (i + 1) + "/H" + (i) + "," + IntTemp + ")-1";
                                    obj.Cells[i, j].CellType = PC;//%
                                }
                            }
                            break;
                        case 9://全社会用电量/规模（MW）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                obj.Cells[i,j].Formula="SUM(L"+(i+1)+":O"+(i+1)+")";
                            }
                            break;
                        case 10://全社会用电量/增长率（%）
                            if (temp == "2000")
                            {

                            }
                            else
                            {
                                Nextyear = (string)PF.ReturnStr(obj, i - 1, 2);//上一层的年份
                                if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                                {
                                    if (temp == "“十五”年均增长率")
                                    {
                                        IntTempRow = ReturnRow(obj, "2000", i, 2);
                                        IntTempRow1 = ReturnRow(obj, "2005", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(J" + (IntTempRow1 + 1) + "/J" + (IntTempRow + 1) + ",0.2)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "2006-2009年均增长率")
                                    {
                                        IntTempRow = ReturnRow(obj, "2006", i, 2);
                                        IntTempRow1 = ReturnRow(obj, "2009", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(J" + (1 + IntTempRow1) + "/J" + (IntTempRow + 1) + ",1/3)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                    if (temp == "“十一五”年均增长率")
                                    {
                                        IntTempRow = ReturnRow(obj, "2005", i, 2);
                                        IntTempRow1 = ReturnRow(obj, "2010", i, 2);
                                        obj.Cells[i, j].Formula = "POWER(J" + (IntTempRow1 + 1) + "/J" + (IntTempRow + 1) + ",1/5)-1";
                                        obj.Cells[i, j].CellType = PC;//%
                                    }
                                }
                                else
                                {
                                    IntTemp = ReturnYearDifference(temp, Nextyear);
                                    IntTemp = 1 / IntTemp;
                                    obj.Cells[i, j].Formula = "POWER(J" + (i + 1) + "/J" + (i) + "," + IntTemp + ")-1";
                                    obj.Cells[i, j].CellType = PC;//%
                                }
                            } break;
                        case 11://三产及居民用电量（亿kWh)/一产
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                value = SelectValue(DQ, strType, year, "一产");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 12://三产及居民用电量（亿kWh）/二产
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                value = SelectValue(DQ, strType, year, "二产");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 13://三产及居民用电量（亿kWh）	/三产
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                value = SelectValue(DQ, strType, year, "三产");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 14://三产及居民用电量（亿kWh）	/居民
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                value = SelectValue(DQ, strType, year, "居民");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        case 15://人均用电量（kWh/人）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {

                                obj.Cells[i, j].Formula = "I"+(i+1)+"*10000/"+ReturnPopulation(temp,"城镇人口(万人)","1",this.projectId);
                            }
                            break;
                        case 16://人均生活用电量（kWh/人）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                obj.Cells[i, j].Formula = "N" + (i + 1) + "*10000/" + ReturnPopulation(temp, "城镇人口(万人)", "1", this.projectId);
                            }
                            break;
                        case 17://农村居民人均生活用电量（kWh/人）
                            if (temp == "“十五”年均增长率" || temp == "2006-2009年均增长率" || temp == "“十一五”年均增长率")
                            {

                            }
                            else
                            {
                                value = SelectValue(DQ, strType, year, "农村居民人均生活用电量（kWh/人）");
                                obj.SetValue(i, j, value);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 返回具体的值
        /// </summary>
        /// <returns></returns>
        /// <param name="DQ">分区名称</param>
        /// <param name="strType">分区类型</param>
        /// <param name="year">年份</param>
        /// <param name="strTitle">列标题</param>
        private  double SelectValue(string DQ,string strType,int year,string strTitle)
        {
            double value = 10;
            string con = null;
            if(strTitle=="一产"||strTitle=="二产"||strTitle=="三产"||strTitle=="居民")
            {
                con = "year='" + year + "'and TypeID=(select Id from psp_Types where Title='" + strTitle + "' and  Flag2='" + flag + "'"+
                "and projectID='" + projectId + "' and parentID= (select Id from psp_Types where Title='三产及居民用电量（亿kWh）'and  Flag2='" + flag + "'" +
                "and projectID='" + projectId + "' and parentID=(select ID from psp_Types where Title='" + DQ + "'and  Flag2='" + flag + "'and " +
                "projectID='" + projectId + "' and parentID=(select ID from psp_Types where Title='" + strType + "'" +
                "and  Flag2='" + flag + "'and projectID='" + projectId + "'))));";
            }
            else
            {
                con = "year='" + year + "'and TypeID=(select Id from psp_Types where Title='" + strTitle + "'and  Flag2='" + flag + "'" +
                "and projectID='" + projectId + "' and parentID=(select ID from psp_Types where Title='" + DQ + "'and  Flag2='" + flag + "'and " +
                "projectID='" + projectId + "' and parentID=(select ID from psp_Types where Title='" + strType + "'" +
                "and  Flag2='" + flag + "'and projectID='" + projectId + "')));";
            }
            try
            {
                value = (double)Services.BaseService.GetObject("SelectPSP_ValuesOfValueByWhere", con);
            }
            catch(System.Exception e)
            {
                //MessageBox.Show(e.Message);
                value = 10;
            }
            return value;
        }
        /// <summary>
        /// 查询包含从那年开始到那年结束
        /// </summary>
        private void SelectYear()
        {
            string con = flag.ToString();//flag标志是2的年份
            listYear = Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", con);
            
        }
        /// <summary>
        /// 查询市辖供电区和县级供电区有几个分区
        /// 由于市辖供电区和县级供电区是一级目录要记住它们的id号
        /// </summary>
        private  void SelectDQ(Itop.Client.Base.FormBase FB,string strTitle)
        {
            string con = null;
             projectId = FB.ProjectUID;
            con = "ParentID=(select ID from psp_types where Title='" + strTitle + "' and projectID='"+projectId+"') group by Title";
            try
            {
                if (strTitle == "市辖供电区")
                {
                    City = (IList)Services.BaseService.GetList<string>("SelectPSP_TypesDQByWhere", con);
                }
                if (strTitle == "县级供电区")
                {
                    County = (IList)Services.BaseService.GetList<string>("SelectPSP_TypesDQByWhere", con);
                }
            }
            catch(System.Exception e)
            {
                //MessageBox.Show("没有"+strTitle+"的数据！！！");
            }
        }
        /// <summary>
        /// 返回当前行的年份和上一层年份的差
        /// </summary>
        /// <param name="CurrentYear">当前行的年份</param>
        /// <param name="UpYear">上一层年份</param>
        /// <returns></returns>
        public  int ReturnYearDifference(string  CurrentYear,string  UpYear)
        {
            int IntCurrentYear = 0;
            int IntUpYear = 0;
            int temp = 0;
            int.TryParse(CurrentYear, out IntCurrentYear);
            int.TryParse(UpYear, out IntUpYear);
           temp= IntCurrentYear - IntUpYear;
           return temp;
        }
        /// <summary>
        /// 通过传入的内容返回内容所在行的值
        /// </summary>
        /// <param name="strObj"></param>
        /// <param name="IntRow">current row</param>
        /// <param name="IntCol">要查询内容的列值</param>
        /// <param name="YearCount">年份的数量</param>
        /// <returns></returns>
        public  int ReturnRow(FarPoint.Win.Spread.SheetView obj,string strObj,int IntRow,int IntCol)
        {
            string strTemp = null;
            strTemp = obj.GetValue(IntRow  , IntCol ).ToString();
            if(strTemp==strObj)
            {
               return  IntRow ;
            }
            else
            {
               return  ReturnRow(obj, strObj, IntRow-=1, IntCol);
            }
            
        }
        /// <summary>
        /// 返回当年的城镇人口数据
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strTitle"></param>
        /// <param name="strType"></param>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public double ReturnPopulation(string strYear,string strTitle,string strType,string ProjectID )
        {
            double temp = 0;
            string con = null;
            string strYear1 = "y" + strYear;
            con = "select " + strYear1 + " from PS_History where  Title='" + strTitle + "'AND Col4='" + ProjectID + "'AND Forecast='" + strType+"'";
            try {
                temp = (double)Services.BaseService.GetObject("SelectPs_HistoryPopulationByCondition", con);
            }catch(System.Exception e)
            {
                //MessageBox.Show("在" + strYear1 + "的" + strTitle+"为空不能计算数据！！！");
                temp = 1;
            }
            return temp;
        }
    }
}
