using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
using Itop.Domain.Graphics;
/******************************************************************************************************
 *  ClassName：Sheet_4
 *  Action：附表4  XX市高压配电网变电站规模明细表(2009年)（发策部、生技部）表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表PS_Table_AreaWH（分区表）
 * 年份：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.FunctionSupplement
{
    class Sheet_4
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList list = null;
        public string strYear = null;

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();

        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_4Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            SelectPW(FB,strYear);
            int IntColCount = 9;
            int IntRowCount = list.Count + 2 + 3;//标题占3行，分区类型占2行，1是其它用
            string title = null;

            obj.SheetName = Title;
            obj.Columns.Count = IntColCount;
            obj.Rows.Count = IntRowCount;
            IntCol = obj.Columns.Count;

            PF.Sheet_GridandCenter(obj);//画边线，居中
            S10_1.ColReadOnly(obj, IntColCount);
            //obj.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;

            string strTitle = "";
            IntRow = 3;
            strTitle = Title;
            PF.CreateSheetView(obj, IntRow, IntCol, 0, 0, Title);
            PF.SetSheetViewColumnsWidth(obj, 0, Title);
            IntCol = 1;


            strTitle = " 序     号";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电压等级(kV)s";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 线路名称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 起点变电站名 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 终点变电站名 ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 导线型号";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 线路长度（km）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 投运时间";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 所在分区";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;

            WriteData(FB,obj, IntRow, IntCol);
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData(Itop.Client.Base.FormBase FB,FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            PSPDEV  psi = null;
            Object Value = null;
            for (int i = 0; i < list.Count; ++i)
            {
                psi = (PSPDEV)list[i];
                for (int j = 0; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 0:
                            Value = i+1;//序号
                            break;
                        case 1:
                            Value = psi.RateVolt;//电压等级(kV)
                            break;
                        case 2:
                            Value = psi.Name;//线路名称
                            break;
                        case 3:
                            Value = psi.IName;//起点变电站名
                            break;
                        case 4:
                            Value = psi.JName;//终点变电站名
                            break;
                        case 5:
                            Value = psi.LineType;//导线型号
                            break;
                        case 6:
                            Value = psi.LineLength;//线路长度（km）
                            break;
                        case 7:
                            Value = psi.OperationYear;//投运时间
                            break;
                        case 8:
                            Value = SelectFQ(FB,psi.AreaID);//所在分区
                            break;

                        default:
                            break;
                    }
                    obj.SetValue(IntRow + i, j, Value);//
                }
            }
        }
        /// <summary>
        /// 查询配网的数据
        /// </summary>
        private void SelectPW(Itop.Client.Base.FormBase FB,string  year)
        {
            string con = " where AreaID!=''  and Type='05' and RateVolt>='35' and ProjectID='" + FB.ProjectUID + "' and OperationYear <='" + year + "'";
            try
            {
                list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 查询分区名
        /// </summary>
        /// <param name="FB"></param>
        /// <param name="AreaId"></param>
        /// <returns></returns>
        private string  SelectFQ(Itop.Client.Base.FormBase FB,string AreaId)
        {
            
            string con = " ID='"+AreaId+"' and ProjectID='" + FB.ProjectUID + "'";
            string title = null;
            try
            {
                title = (string)Services.BaseService.GetObject("SelectPS_Table_AreaWHOfTitle", con);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return title;
        }
    }
}
