using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Table;
/******************************************************************************************************
 *  ClassName：Sheet_15
 *  Action：表3-2 XX市中压配电网新建工程明细表（生技部、四县公司）表的数据写入
 * Author：吕静涛
 * Mender  ：吕静涛
 * 概述：这个表用数据库表Ps_Table_TZGS
 * 年份：2010-10-11。
 *********************************************************************************************************/
namespace Itop.JournalSheet.FunctionSupplement
{
    class Sheet_15
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
        public void SetSheet_15Title(Itop.Client.Base.FormBase FB, FarPoint.Win.Spread.SheetView obj, string Title)
        {
            SelectValue_A(FB);
            SelectValue_B(FB);
            SelectValue_C(FB);
            SelectValue_D(FB);
            int IntColCount = 26;
            int IntRowCount = Value_C[0].Count+Value_C[1].Count + 2 + 3;//标题占3行，分区类型占2行，1是其它用
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

            strTitle = " 工程名称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电压等级(kV)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "供电区";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge+=1, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 名     称";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge-=1, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = "性     质";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 工程描述";
            PF.CreateSheetView(obj, NextRowMerge+=1, NextColMerge, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 长度(km)架空";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电缆长度(km)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 中压开关 ";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge+=3, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            for (int i = 0; i < 4;i++ )
            {
                switch (i)
                {
                case 0:
                    strTitle = " 开闭站(座) ";
                    NextColMerge -= 3;
                    IntRow += 1;
                    break;
                case 1:
                    strTitle = " 环网柜(座) ";
                    IntCol += 1;
                    break;
                case 2:
                    strTitle = " 柱上开关(台)";
                    IntCol += 1;
                    break;
                case 3:
                    strTitle = " 电缆分支箱(台) ";
                    IntCol += 1;
                    break;

                default:
                	break;
                }
                PF.CreateSheetView(obj, NextRowMerge , NextColMerge , IntRow, IntCol , strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }

            strTitle = " 中压配电";
            PF.CreateSheetView(obj, NextRowMerge-=1, NextColMerge+=3, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        strTitle = " 配电室(座) ";
                        NextColMerge -= 3;
                        IntRow += 1;
                        break;
                    case 1:
                        strTitle = " 箱变(座)";
                        IntCol += 1;
                        break;
                    case 2:
                        strTitle = " 柱上变(台)";
                        IntCol += 1;
                        break;
                    case 3:
                        strTitle = " 无功补偿容量(Mvar)";
                        IntCol += 1;
                        break;

                    default:
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }

            strTitle = " 低压网配套";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge += 1, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 架空线路(km)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge -=1, IntRow+=1, IntCol , strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 电缆(km)";
            PF.CreateSheetView(obj, NextRowMerge, NextColMerge , IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 投资(万元)";
            PF.CreateSheetView(obj, NextRowMerge += 2, NextColMerge, IntRow-=1, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 投运时间（年）";
            PF.CreateSheetView(obj, NextRowMerge , NextColMerge , IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            strTitle = " 工程属性";
            PF.CreateSheetView(obj, NextRowMerge -= 1, NextColMerge += 5, IntRow, IntCol += 1, strTitle);
            PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);

            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        strTitle = " 变电站配套送出";
                        NextColMerge -= 5;
                        IntRow += 1;
                        break;
                    case 1:
                        strTitle = " 配电网切改";
                        IntCol += 1;
                        break;
                    case 2:
                        strTitle = " 架空线入地";
                        IntCol += 1;
                        break;
                    case 3:
                        strTitle = " 新农村电气化";
                        IntCol += 1;
                        break;
                    case 4:
                        strTitle = " 无电区供电";
                        IntCol += 1;
                        break;
                    case 5:
                        strTitle = " 其     它";
                        IntCol += 1;
                        break;

                    default:
                        break;
                }
                PF.CreateSheetView(obj, NextRowMerge, NextColMerge, IntRow, IntCol, strTitle);
                PF.SetSheetViewColumnsWidth(obj, IntCol, strTitle);
            }


            NextRowMerge = 1;
            NextColMerge = 1;

            IntRow = 5;
            IntCol = 0;
            WriteData( obj, IntRow, IntCol);
            PF.SetWholeRowHeight(obj, obj.Rows.Count, obj.Columns.Count);//行高
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IntRow"></param>
        /// <param name="IntCol"></param>
        private void WriteData( FarPoint.Win.Spread.SheetView obj, int IntRow, int IntCol)
        {
            Ps_Table_TZGS pTT_A = null;
            Ps_Table_TZGS pTT_B = null;
            Ps_Table_TZGS pTT_C = null;
            Ps_Table_TZGS pTT_D = null;
            Object value = null;
            IList list_A =  Value_A[0];
            IList list_B =  Value_B[0];
            IList list_C =  Value_C[0];
            IList list_D =  Value_D[0];
            string City = "市区供电区";
            string County = "县域供电区";
            int intdex = 1;
           
            for (int i = IntRow; i < Value_A[0].Count+IntRow; ++i)//市辖
            {
                pTT_A = (Ps_Table_TZGS)list_A[i - IntRow];
                pTT_B = (Ps_Table_TZGS)list_B[i - IntRow];
                pTT_C = (Ps_Table_TZGS)list_C[i - IntRow];
                pTT_D = (Ps_Table_TZGS)list_D[i - IntRow];

                for(int j=0;j<obj.ColumnCount;++j)
                {
                    switch(j)
                    {
                        case 0://序号
                            value = intdex;
                            break;
                        case 1://工程名称
                            value = pTT_A.Title;// pTT.Title.Substring(0,pTT.Title.IndexOf("-",0,pTT.Title.Length));
                            break;
                        case 2://电压等级(kV）
                            value = pTT_C.BianInfo.Substring(0, pTT_C.BianInfo.IndexOf("@", 0, pTT_C.BianInfo.Length));//电压等级
                            break;
                        case 3://供电区名称
                            value = pTT_A.AreaName+City;
                            break;
                        case 4://供电区性质
                                value = "市辖区";
                            break;
                        case 5://工程描述
                            value = pTT_A.Col1 ;
                            break;
                        case 6://长度(km)架空
                            value = pTT_B.Length;
                            break;
                        case 7://电缆长度(km)
                            value = pTT_B.Length2;
                            break;
                        case 8://中压开关	开闭站(座)		
                            value = pTT_D.Num1;
                            break;
                        case 9://中压开关 环网柜(座)
                            value = pTT_D.Num2;
                            break;
                        case 10://中压开关 柱上开关(台)
                            value = pTT_D.Num3;
                            break;
                        case 11://中压开关 电缆分支箱(台)
                            value = pTT_D.Num4;
                            break;
                        case 12://中压配电 配电室(座)
                            value =pTT_C.Num1;
                            break;
                        case 13://中压配电 箱变(座)
                            value = pTT_C.Num3;
                            break;
                        case 14://中压配电 柱上变(台)
                            value = pTT_C.Num5;
                            break;
                        case 15://中压配电 无功补偿容量(Mvar)
                            value = pTT_C.WGNum;
                            break;
                        case 16://低压网配套 架空线路(km)
                            value = pTT_B.Length;
                            break;
                        case 17://低压网配套 电缆(km)
                            value = pTT_B.Length2;
                            break;
                        case 18://投资(万元)
                            value =pTT_A.Amount;
                            break;
                        case 19://投运时间（年）
                            value = pTT_A.BuildYear;
                            break;
                        case 20://工程属性 变电站配套送出
                            if (pTT_A.ProgType == "变电站配套送出")
                                value = "是";
                            else
                                value = null;
                            break;
                        case 21://工程属性 配电网切改
                            if (pTT_A.ProgType == "配电网切改")
                                value = "是";
                            else
                                value = null;
                            break;
                        case 22://工程属性 架空线入地
                            if (pTT_A.ProgType == "架空线入地")
                                value = "是";
                            else
                                value = null;
                            break;
                        case 23://工程属性 新农村电气化
                            if (pTT_A.ProgType == "新农村电气化")
                                value = "是";
                            else
                                value = null;
                            break;
                        case 24://工程属性 无电区供电
                            if (pTT_A.ProgType == "无电区供电")
                                value = "是";
                            else
                                value = null;
                            break;
                        case 25://工程属性 其它
                            if (pTT_A.ProgType == "其他类别")
                                value = "是";
                            else
                                value = null;
                            break;

                        default:
                            break;
                    }
                    obj.SetValue(i, j, value);
                }
                intdex++;
            }

             list_A = Value_A[1];
             list_B = Value_B[1];
             list_C = Value_C[1];
             list_D = Value_D[1];
            for (int i = IntRow + Value_A[0].Count; i < Value_A[1].Count + Value_A[0].Count+IntRow; ++i)//县级
            {
                pTT_A = (Ps_Table_TZGS)list_A[i -(IntRow + Value_A[0].Count)];
                pTT_B = (Ps_Table_TZGS)list_B[i - (IntRow + Value_A[0].Count)];
                pTT_C = (Ps_Table_TZGS)list_C[i - (IntRow + Value_A[0].Count)];
                pTT_D = (Ps_Table_TZGS)list_D[i - (IntRow + Value_A[0].Count)];

                for (int j = 0; j < obj.ColumnCount; ++j)
                {
                    switch (j)
                    {
                        case 0://序号
                            value = intdex;
                            break;
                        case 1://工程名称
                            value = pTT_A.Title;// pTT.Title.Substring(0,pTT.Title.IndexOf("-",0,pTT.Title.Length));
                            break;
                        case 2://电压等级(kV）
                            value = pTT_C.BianInfo.Substring(0, pTT_C.BianInfo.IndexOf("@", 0, pTT_C.BianInfo.Length));//电压等级
                            break;
                        case 3://供电区名称
                            value = pTT_A.AreaName + County;
                            break;
                        case 4://供电区性质
                            value =pTT_A.DQ.Substring(2,(pTT_A.DQ.Length-2) );
                            break;
                        case 5://工程描述
                            value = pTT_A.Col1;
                            break;
                        case 6://长度(km)架空
                            value = pTT_B.Length;
                            break;
                        case 7://电缆长度(km)
                            value = pTT_B.Length2;
                            break;
                        case 8://中压开关	开闭站(座)		
                            value = pTT_D.Num1;
                            break;
                        case 9://中压开关 环网柜(座)
                            value = pTT_D.Num2;
                            break;
                        case 10://中压开关 柱上开关(台)
                            value = pTT_D.Num3;
                            break;
                        case 11://中压开关 电缆分支箱(台)
                            value = pTT_D.Num4;
                            break;
                        case 12://中压配电 配电室(座)
                            value = pTT_C.Num1;
                            break;
                        case 13://中压配电 箱变(座)
                            value = pTT_C.Num3;
                            break;
                        case 14://中压配电 柱上变(台)
                            value = pTT_C.Num5;
                            break;
                        case 15://中压配电 无功补偿容量(Mvar)
                            value = pTT_C.WGNum;
                            break;
                        case 16://低压网配套 架空线路(km)
                            value = pTT_B.Length;
                            break;
                        case 17://低压网配套 电缆(km)
                            value = pTT_B.Length2;
                            break;
                        case 18://投资(万元)
                            value = pTT_A.Amount;
                            break;
                        case 19://投运时间（年）
                            value = pTT_A.BuildYear;
                            break;
                        case 20://工程属性 变电站配套送出
                            if (pTT_A.ProgType == "变电站配套送出")
                                value = "是";
                            else
                                value = null;
                            break;
                        case 21://工程属性 配电网切改
                            if (pTT_A.ProgType == "配电网切改")
                                value = "是";
                            else
                                value = null;
                            break;
                        case 22://工程属性 架空线入地
                            if (pTT_A.ProgType == "架空线入地")
                                value = "是";
                            else
                                value = null;
                            break;
                        case 23://工程属性 新农村电气化
                            if (pTT_A.ProgType == "新农村电气化")
                                value = "是";
                            else
                                value = null;
                            break;
                        case 24://工程属性 无电区供电
                            if (pTT_A.ProgType == "无电区供电")
                                value = "是";
                            else
                                value = null;
                            break;
                        case 25://工程属性 其它
                            if (pTT_A.ProgType == "其他类别")
                                value = "是";
                            else
                                value = null;
                            break;

                        default:
                            break;
                    }
                    obj.SetValue(i, j, value);
                }
                intdex++;
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
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='新建'";
            string sql1 = "select  c.*  FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='新建'";
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
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='新建'";
            string sql1 = "select  a.*  FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='新建'";
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
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='新建'";
            string sql1 = "select  b.*  FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='新建'";
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
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='新建'";
            string sql1 = "select  d.*  FROM Ps_Table_TZGS a,Ps_Table_TZGS b,Ps_Table_TZGS c,Ps_Table_TZGS d " +
                                 "where  a.ID = b.ParentID AND b.Col4 = 'pw-line' " +
                                 "AND a.ID = c.ParentID AND c.Col4 = 'pw-pb' " +
                                 "and a.ID = d.ParentID AND d.Col4 = 'pw-kg'and  c.DQ!='市辖供电区' and c.projectid='" + FB.ProjectUID + "' " +
                               "and  cast(substring(c.BianInfo,1,charindex('@',c.BianInfo,0)-1)as int)<35 and c.Col3='新建'";
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
