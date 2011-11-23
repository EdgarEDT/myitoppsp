using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Domain.Graphics;
using Itop.Client.Common;
/******************************************************************************************************
 *  ClassName：Sheet_3
 *  Action：附表4  XX市高压配电网变电站规模明细表(2009年)（发策部、生技部）表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库PSP_Substation_Info
 * 年份：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.FunctionSupplement
{
    class Sheet_3
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
        public void SetSheet_3Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            SelectBDZ(FB,strYear);
            int IntColCount = 14;
            int IntRowCount = list.Count +  2 + 3;//标题占3行，分区类型占2行，1是其它用
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


            strTitle = " 名     称";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow, IntCol -= 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电压等级(kV)s";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 变     比";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 容量构成（MVA） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 主变台数（台） ";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 总容量（MVA）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 最大负荷（MW）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 无功补偿容量（Mvar）";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 10(20)kV出线间隔总数(回)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 10(20)已出线间隔数 (回)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 公用/专用";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 建设型式";
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

            WriteData(obj, IntRow, IntCol);
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData(FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            PSP_Substation_Info psi=null;
            Object Value = null;
            obj.Columns[11].Locked = false;//建设型式,手写
            for(int i=0;i<list.Count;++i)
            {
                psi=(PSP_Substation_Info)list[i];
                for (int j = 0; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 0:
                            Value = psi.Title;//名称
                            break;
                        case 1:
                            Value = psi.L1;//电压等级(kV)
                            break;
                        case 2:
                            Value = psi.L15;//变比
                            break;
                        case 3:
                            Value = psi.L4;//容量构成（MVA）
                            break;
                        case 4:
                            Value = psi.L3;//主变台数（台）
                            break;
                        case 5:
                            Value = psi.L2;//总容量（MVA）
                            break;
                        case 6:
                            Value = psi.L9;//最大负荷（MW）
                            break;
                        case 7:
                            Value = psi.L5;//无功补偿容量（Mvar）
                            break;
                        case 8:
                            Value = psi.L13;//10(20)kV出线间隔总数(回)
                            break;
                        case 9:
                            Value = psi.L14;//10(20)已出线间隔数 (回)
                            break;
                        case 10:
                            Value = psi.S4;//公用/专用
                            break;
                        case 12:
                            Value = psi.S2;//投运时间
                            break;
                        case 13:
                            Value = psi.AreaName;//所在分区
                            break;

                        default:
                            break;
                    }
                    if(j!=11)
                    {
                        obj.SetValue(IntRow+i, j, Value);//
                    }
                }
            }
        }
        /// <summary>
        /// 查询变电站的数据
        /// </summary>
        private void SelectBDZ(Itop.Client.Base.FormBase FB, string year)
        {
            string con = " L1 >='35' and AreaID='" + FB.ProjectUID + "' and S2 <='" + year + "'";
            try
            {
                list = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// add BareditItems of years
        /// </summary>
        /// <param name="BE">BarEditItem object</param>
        public  void AddBarEditItems(DevExpress.XtraBars.BarEditItem BE)
        {
            BE.EditValue = "2000";
            int FirstYear = 1970;
            int EndYear = 2060;
            for (int i = FirstYear; i <= EndYear; i++)
            {
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BE.Edit).Items.Add(FirstYear.ToString());
                FirstYear++;
            }
        }
    }
}
