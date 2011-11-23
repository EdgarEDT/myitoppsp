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
 *  ClassName：Sheet23
 *  Action：附表23 2015年铜陵县35kV主变“N-1”校验结果 的数据写入

 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：先找到35kv的变电站，通过10kv母线找到10kv线路另一端变电站
 *              数据库表用PSP_Substation_Info，PSPDEV
 * 年份：2010-10-29
 * 修改时间：2010-11-09
 */

namespace Itop.VogliteVillageSheets.FrmInvestmentAppraisal
{
    class Sheet23
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值

        private int NextColMerge = 1;//合并单元格列初始值

        private string projectID = "";
        private IList list = null;
        private IList BDZList = null;

        private Itop.JournalSheet.Function.PublicFunction PF = new Itop.JournalSheet.Function.PublicFunction();
        private Itop.VogliteVillageSheets.Function.PublicFunction m_PF = new Itop.VogliteVillageSheets.Function.PublicFunction();
        private FarPoint.Win.Spread.CellType.PercentCellType PC = new FarPoint.Win.Spread.CellType.PercentCellType();

        private struct Intercept//截取数据用的结构
        {
            public int index;//数据的个数

            public string[] JHData ;//截取加号后的数据
        }
        private Intercept intercept = new Intercept();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_23Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            int Temp = 0;
            int IntColCount = 12;
            ReturnBDZ(FB);
            int IntRowCount = ReturnRow(BDZList)+1 + 2 + 3;//标题占3行，分区类型占2行

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

            strTitle = "  单位：万千瓦 万千伏安";
            obj.AddSpanCell(IntRow, 0, 1, obj.Columns.Count);
            obj.SetValue(IntRow, 0, strTitle);
            PF.SetSheetViewColumnsWidth(obj, 0, strTitle);
            //右对齐

            obj.Rows[IntRow].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //列标题

            IntRow = 4;
            strTitle = "变电站名称";
            PF.CreateSheetView(obj, NextRowMerge+=1, NextColMerge, IntRow, IntCol-=1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "主变容量";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "10kV线路所带负荷";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "需转供负荷";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "联络的变电站";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "联络线路条数";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "联络线路能转带的负荷";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "联络变电站能转带的负荷";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "通过联络可转带的负荷";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "通过主变能转带负荷";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "需通过10kV网络转带负荷";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "是否通过";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 6;
            IntCol = 0;
            //PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
            //SetLeftTitle();
            WriteData(FB, obj, IntRow);
        }
        /// <summary>
        /// 初始化结构

        /// </summary>
        private void InitStruct()
        {
            intercept.index = 0;
            intercept.JHData = null;
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        private void WriteData(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj,int IntRow)
        {
            PSP_Substation_Info ppi = null;
            InitStruct();
            int index = 0;
            int indexCount = 0;
            int JH = 0;
            int inttemp = 0;
            string[] LRData = new string[2];//0,为*号左侧数据，1为*号右侧数据

            for (int i = IntRow; i < obj.RowCount; i += indexCount)
            {
                ppi = (PSP_Substation_Info)BDZList[index];
                if (ppi.L3 > 0)
                    indexCount = ppi.L3;
                else
                    indexCount = 1;
                for (int j = 0; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 0://变电站名称

                            obj.AddSpanCell((i), j, indexCount, 1);
                            obj.SetValue((i), j, ppi.Title);
                            index ++;
                    	    break;
                        case 1://主变容量
                            if(ppi.L4!="")
                            {
                                JH = ReturnJH(ppi.L4);//得到加号的数量

                                intercept.JHData = new string[ppi.L3];
                                ReturnJHOfData(ppi.L4);
                                for (int o = 0; o < ppi.L3; ++o)
                                {
                                    for (int n = 0; n < JH; ++n)
                                    {
                                        LRData = ReturnCHOfData(intercept.JHData[n]);
                                        obj.SetValue((i + n), j, LRData[1]);
                                    }
                                    if (JH < ppi.L3)
                                    {
                                        LRData = ReturnCHOfData(intercept.JHData[0]);
                                        obj.SetValue((i + o), j, LRData[1]);
                                    }
                                }
                                intercept.index = 0;
                            }

                            break;
                        case 2://10kV线路所带负荷

                            obj.AddSpanCell((i), j, indexCount, 1);
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 3://需转供负荷
                            obj.AddSpanCell((i), j, indexCount, 1);
                            obj.Cells[i, j].Formula = "C" + (i + 1) + "-B" + (i + 1);
                            break;
                        case 4://联络的变电站
                            obj.AddSpanCell((i), j, indexCount, 1);
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 5://联络线路条数
                            obj.AddSpanCell((i), j, indexCount, 1);
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 6://联络线路能转带的负荷
                            obj.AddSpanCell((i), j, indexCount, 1);
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 7://联络变电站能转带的负荷

                            obj.AddSpanCell((i), j, indexCount, 1);
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 8://通过联络可转带的负荷
                            obj.AddSpanCell((i), j, indexCount, 1);
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 9://通过主变能转带负荷

                            obj.AddSpanCell((i), j, indexCount, 1);
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 10://需通过10kV网络转带负荷
                            obj.AddSpanCell((i), j, indexCount, 1);
                            obj.Cells[i, j].Locked = false;
                            break;
                        case 11://是否通过
                            obj.AddSpanCell((i), j, indexCount, 1);
                            obj.SetValue(i, j, ppi.S1);
                            break;

                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 返回母线名称
        /// </summary>
        /// <param name="BDZ_ID">变电站id</param>
        private string  ReturnMX(Itop.Client.Base.FormBase FB,string BDZ_ID)
        {
            string sql = "";
            string strTitle = "";
            try
            {
                strTitle = (string)Services.BaseService.GetObject("SelectPSPDEV_MXNAME", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return strTitle;
        }
        /// <summary>
        /// 返回字符串中带有几个加号
        /// </summary>
        /// <returns></returns>
        private int ReturnJH(string strTemp)
        {
            int temp = 1;
            for (int i = 0; i < strTemp.Length; ++i)
            {
                if (strTemp[i] == '+')
                {
                    temp++;
                }
            }
            return temp;
        }
        /// <summary>
        /// 截取带加号的数据
        /// </summary>
        /// <returns></returns>
        private string[] ReturnJHOfData(string strTemp)
        {
            int flag = 0;
            int strat = 0;
            string Temp = strTemp;
            string Temp1 = "";
            string Temp2 = "";
            flag = Temp.IndexOf("+", strat, Temp.Length);
            if (flag != -1)
            {

                Temp1 = Temp.Substring(strat, (flag));
                Temp2 = Temp.Remove(strat, flag + 1);
                intercept.JHData[intercept.index] = Temp1;
                intercept.index++;
                ReturnJHOfData(Temp2);
            }
            else
            {
                intercept.JHData[intercept.index] = Temp;
            }
            return intercept.JHData;
        }
        /// <summary>
        /// 返回去掉*号的左侧右侧数据
        /// </summary>
        /// <param name="strTemp"></param>
        /// <returns></returns>
        private string[] ReturnCHOfData(string strTemp)
        {
            string[] temp = new string[2];
            string s = strTemp;
            int flag = 0;
            s = strTemp;
            flag = s.IndexOf("*", 0, s.Length);
            if (flag != -1)
            {
                temp[0] = s.Substring(0, flag);
                temp[1] = s.Substring((flag + 1), (s.Length - flag - 1));
            }
            else
            {
                temp[1] = s;
            }
            return temp;
        }        /// <summary>
        /// 返回变电站数据

        /// </summary>
        /// <param name="FB"></param>
        private void ReturnBDZ(Itop.Client.Base.FormBase FB)
        {
            string sql = " AreaID='"+FB.ProjectUID+"' and L1=35 and s2<=2015 ";

            try
            {
                BDZList = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", sql);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 通过变电站的主变台数来确定行数

        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int ReturnRow(IList list)
        {
            PSP_Substation_Info psi = null;
            int temp = 0;
                for (int i = 0; i < list.Count; ++i)
                {
                    psi = (PSP_Substation_Info)list[i];
                    if (psi.L3 > 0)
                        temp += psi.L3;
                    else
                        temp += 1;
                }
            return temp;
        }
    }
}
