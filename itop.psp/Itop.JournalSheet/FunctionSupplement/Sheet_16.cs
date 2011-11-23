using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Table;
/******************************************************************************************************
 *  ClassName：Sheet_16
 *  Action：表4-2 XX市中压配电网改造工程明细表（生技部、四县公司）表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_Table_TZGS
 * 年份：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.FunctionSupplement
{
    class Sheet_16
    {
        private int IntCol = 0;
        private int IntRow = 0;
        private int NextRowMerge = 1;//合并单元格行初始值
        private int NextColMerge = 1;//合并单元格列初始值
        private IList[] Value_A = new IList[2];//0,是市辖，1为县级
        private IList[] Value_B = new IList[2];//0,是市辖，1为县级
        private IList[] Value_C = new IList[2];//0,是市辖，1为县级
        private IList[] Value_D = new IList[2];//0,是市辖，1为县级

        private Function.PublicFunction PF = new Function.PublicFunction();
        private Function10.Sheet10_1 S10_1 = new Function10.Sheet10_1();
        //////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 填写表头
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="Title"></param>
        public void SetSheet_16Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            SelectValue_A(FB);
            SelectValue_B(FB);
            SelectValue_C(FB);
            SelectValue_D(FB);
            int IntColCount = 20;
            int IntRowCount = Value_A[0].Count+Value_A[1].Count + 2 + 3;//标题占3行，分区类型占2行，1是其它用
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

            strTitle = " 电压等级(kV)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 项目名称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "供电区";
            PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge += 1, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 名     称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -= 1, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "性     质";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 工程描述";
            PF.CreateSheetView(obj, NextRowMerge += 1, NextColMerge, IntRow -= 1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 高损配变";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge+=1, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 台数(台)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge-=1, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 容量(MVA)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow , IntCol+=1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 无功补偿装置";
            PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge+=1, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 组数(组)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge-=1, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "容量(Mvar)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol+=1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 开关类 ";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge += 3, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        strTitle = " 断路器(台) ";
                        NextColMerge -= 3;
                        IntRow += 1;
                        break;
                    case 1:
                        strTitle = " 负荷开关(台)";
                        IntCol += 1;
                        break;
                    case 2:
                        strTitle = " 环网柜(座)";
                        IntCol += 1;
                        break;
                    case 3:
                        strTitle = " 电缆分支箱(座)";
                        IntCol += 1;
                        break;

                    default:
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }

            strTitle = " 架空线路";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge += 1, IntRow -= 1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 线路长度(km)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -= 1, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "杆塔(基)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);


            strTitle = " 电缆线路";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge += 1, IntRow -= 1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 长度(km)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -= 1, IntRow += 1, IntCol, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "沟道(km)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "改造时间（年）";
            PF.CreateSheetView(obj, NextRowMerge+=2, NextColMerge, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "投资（万元）";
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
            Ps_Table_TZGS pTT_A = null;
            Ps_Table_TZGS pTT_B = null;
            Ps_Table_TZGS pTT_C = null;
            Ps_Table_TZGS pTT_D = null;
            Object value = null;
            IList list_A = Value_A[0];
            IList list_B = Value_B[0];
            IList list_C = Value_C[0];
            IList list_D = Value_D[0];
            string City = "市区供电区";
            string County = "县域供电区";
            int index = 1;

            for (int i = IntRow; i < Value_A[0].Count + IntRow; ++i)//市辖
            {
                pTT_A = (Ps_Table_TZGS)list_A[i - IntRow];
                pTT_B = (Ps_Table_TZGS)list_B[i - IntRow];
                pTT_C = (Ps_Table_TZGS)list_C[i - IntRow];
                pTT_D = (Ps_Table_TZGS)list_D[i - IntRow];

                for (int j = 0; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 0://序号
                            value = index;
                            break;
                        case 1://电压等级(kV）
                            value = pTT_C.BianInfo.Substring(0, pTT_C.BianInfo.IndexOf("@", 0, pTT_C.BianInfo.Length));//电压等级// pTT.Title.Substring(0,pTT.Title.IndexOf("-",0,pTT.Title.Length));
                            break;
                        case 2://工程名称
                            value =  pTT_A.Title;
                            break;
                        case 3://供电区名称
                            value = pTT_A.AreaName + City;
                            break;
                        case 4://供电区性质
                            value = "市辖区";
                            break;
                        case 5://工程描述
                            value = pTT_A.Col1;
                            break;
                        case 6://高损配变 台数(台)
                            break;
                        case 7://高损配变 容量(MVA)
                            break;
                        case 8://无功补偿装置	组数(组)	
                            break;
                        case 9://无功补偿装置 容量(Mvar)
                            break;
                        case 10://开关类 断路器(台)
                            value = pTT_D.Num5;
                            break;
                        case 11://开关类 负荷开关(台) 现在用柱上开关
                            value = pTT_D.Num3;
                            break;
                        case 12://开关类 环网柜(座)
                            value = pTT_D.Num2;
                            break;
                        case 13://开关类 电缆分支箱(座)
                            value = pTT_D.Num4;
                            break;
                        case 14://架空线路 线路长度(km)
                            value = pTT_B.Length;
                            break;
                        case 15://架空线路 杆塔(基)
                            value = pTT_B.Num2 ;
                            break;
                        case 16://电缆线路 长度(km)
                            value = pTT_B.Length2;
                            break;
                        case 17://电缆线路 沟道(km)
                            value = pTT_B.Num6;
                            break;
                        case 18://改造时间
                            value = pTT_A.BuildYear;
                            break;
                        case 19://投资（万元）
                            value = pTT_A.Amount;
                            break;

                        default:
                            break;
                    }
                    if(j!=6&&j!=7&&j!=8&&j!=9)
                    {
                        obj.SetValue(i, j, value);
                    }
                    else
                    {
                        obj.Cells[i, j].Locked = false;//手写
                    }
                }
                index++;
            }

            list_A = Value_A[1];
            list_B = Value_B[1];
            list_C = Value_C[1];
            list_D = Value_D[1];
            for (int i = IntRow + Value_A[0].Count; i < Value_A[1].Count + Value_A[0].Count + IntRow; ++i)//县级
            {
                pTT_A = (Ps_Table_TZGS)list_A[i - (IntRow + Value_A[0].Count)];
                pTT_B = (Ps_Table_TZGS)list_B[i - (IntRow + Value_A[0].Count)];
                pTT_C = (Ps_Table_TZGS)list_C[i - (IntRow + Value_A[0].Count)];
                pTT_D = (Ps_Table_TZGS)list_D[i - (IntRow + Value_A[0].Count)];

                for (int j = 0; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 0://序号
                            value = index;
                            break;
                        case 1://电压等级(kV）
                            value = pTT_C.BianInfo.Substring(0, pTT_C.BianInfo.IndexOf("@", 0, pTT_C.BianInfo.Length));//电压等级// pTT.Title.Substring(0,pTT.Title.IndexOf("-",0,pTT.Title.Length));
                            break;
                        case 2://工程名称
                            value =  pTT_A.Title;
                            break;
                        case 3://供电区名称
                            value = pTT_A.AreaName + City;
                            break;
                        case 4://供电区性质
                            value = "市辖区";
                            break;
                        case 5://工程描述
                            value = pTT_A.Col1;
                            break;
                        case 6://高损配变 台数(台)
                            break;
                        case 7://高损配变 容量(MVA)
                            break;
                        case 8://无功补偿装置	组数(组)	
                            break;
                        case 9://无功补偿装置 容量(Mvar)
                            break;
                        case 10://开关类 断路器(台)
                            value = pTT_D.Num5;
                            break;
                        case 11://开关类 负荷开关(台) 现在用柱上开关
                            value = pTT_D.Num3;
                            break;
                        case 12://开关类 环网柜(座)
                            value = pTT_D.Num2;
                            break;
                        case 13://开关类 电缆分支箱(座)
                            value = pTT_D.Num4;
                            break;
                        case 14://架空线路 线路长度(km)
                            value = pTT_B.Length;
                            break;
                        case 15://架空线路 杆塔(基)
                            value = pTT_B.Num2 ;
                            break;
                        case 16://电缆线路 长度(km)
                            value = pTT_B.Length2;
                            break;
                        case 17://电缆线路 沟道(km)
                            value = pTT_B.Num6;
                            break;
                        case 18://改造时间
                            value = pTT_A.BuildYear;
                            break;
                        case 19://投资（万元）
                            value = pTT_A.Amount;
                            break;

                        default:
                            break;
                    }
                    if(j!=6&&j!=7&&j!=8&&j!=9)
                    {
                        obj.SetValue(i, j, value);
                    }
                    else
                    {
                        obj.Cells[i, j].Locked = false;//手写
                    }
                }
                index++;
            }
            
        }
        /// <summary>
        /// 找到所有符合条件的数据-配网
        /// </summary>
        private void SelectValue_C(Itop.Client.Base.FormBase FB)
        {
            string sql = "select  c.* FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='改造'";
            string sql1 = "select  c.*  FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='改造'";
            try
            {
                Value_C[0] = Services.BaseService.GetList("SelectTZGSEveryField", sql);
                Value_C[1] = Services.BaseService.GetList("SelectTZGSEveryField", sql1);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 找到所有符合条件的数据-主体
        /// </summary>
        private void SelectValue_A(Itop.Client.Base.FormBase FB)
        {
            string sql = "select  a.* FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='改造'";
            string sql1 = "select  a.*  FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='改造'";
            try
            {
                Value_A[0] = Services.BaseService.GetList("SelectTZGSEveryField", sql);
                Value_A[1] = Services.BaseService.GetList("SelectTZGSEveryField", sql1);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 找到所有符合条件的数据-线路
        /// </summary>
        private void SelectValue_B(Itop.Client.Base.FormBase FB)
        {
            string sql = "select  b.* FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='改造'";
            string sql1 = "select  b.*  FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='改造'";
            try
            {
                Value_B[0] = Services.BaseService.GetList("SelectTZGSEveryField", sql);
                Value_B[1] = Services.BaseService.GetList("SelectTZGSEveryField", sql1);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 找到所有符合条件的数据-开关
        /// </summary>
        private void SelectValue_D(Itop.Client.Base.FormBase FB)
        {
            string sql = "select  d.* FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='改造'";
            string sql1 = "select  d.*  FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='改造'";
            try
            {
                Value_D[0] = Services.BaseService.GetList("SelectTZGSEveryField", sql);
                Value_D[1] = Services.BaseService.GetList("SelectTZGSEveryField", sql1);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
