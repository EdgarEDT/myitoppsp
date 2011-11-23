//********************************************************************************/
//
//�˴�����BuildSheetFromExcel�����������Զ�����.
//����ʱ��:2011-5-20 8:26:17
//
//********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Itop.Domain.Table;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using System.Data;
using Itop.Common;
using Itop.Domain.Stutistics;
using Itop.Domain.Stutistic;
namespace Itop.Client.TableTemplateNW
{
    class Sheet22412zhu
    {
        //���ɹ������ʵ��
        Tcommon TC = new Tcommon();
        //��ǰ����
        string ProjectID = Tcommon.ProjectID;
        //����������
        int rowcount = 0;
        //������������
        int colcount = 0;
        //�������һ�еı���
        string title = "";
        //�������ǩ��
        string sheetname = "";
        public  void Build_Sheet(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //���9 ��10 ��
            rowcount = 9;
            colcount = 10;
            //�������һ�еı���
            title = TC.GetTableTitle(this.GetType().Name);
            //�������й����
            //int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            //��������
            sheetname = title;
            //�趨����������ֵ������ͱ���
            TC.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //�趨����ģʽ���Ա�д��ʽʹ��
            TC.Sheet_Referen_R1C1(obj_sheet);
            //�趨����п��
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 120;
           
            //�趨����и߶�
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
            //д����������

            //2�б�������
            obj_sheet.AddSpanCell(1, 0, 2, 2);
            obj_sheet.AddSpanCell(1, 2, 1, 2);
            obj_sheet.SetValue(1, 2, "ȫ��");
           

            //3�б�������
            obj_sheet.SetValue(2, 2, "����");
            obj_sheet.SetValue(2, 3, "����");
          
            //д����������

            //1�б�������
         

            //2�б�������
           
            //�������
            Sheet_AddData(obj_sheet);

            //�趨�����
            TC.Sheet_GridandCenter(obj_sheet);

            //�������
            TC.Sheet_Locked(obj_sheet);
        }
   
       private void AddItemsRow(FarPoint.Win.Spread.SheetView obj_sheet, string DianYa, int rowstart)
        {
            obj_sheet.AddSpanCell(rowstart, 0, 3, 1);
            obj_sheet.SetValue(rowstart, 0, DianYa + "ǧ��");
            obj_sheet.SetValue(rowstart++, 1, "�ϼ�");
            obj_sheet.SetValue(rowstart++, 1, "���ͽ���");
            obj_sheet.SetValue(rowstart++, 1, "�ǵ��ͽ���");
        }
        private void AddItemsCol(FarPoint.Win.Spread.SheetView obj_sheet, string AreaName, int colstart)
        {

            obj_sheet.AddSpanCell(1, colstart, 1, 2);
            obj_sheet.SetValue(1, colstart, AreaName);
            obj_sheet.SetValue(2, colstart, "����");
            obj_sheet.SetValue(2, colstart+1, "����");
        }
         string JXFSDX=" and ( JXFS='��������' or JXFS='˫������' or JXFS='����һ��' or JXFS='����һ��' or JXFS='��ֶ�������' or JXFS='��ֶ�������' )";
        string JXFSFDX = " and ( JXFS='������' or JXFS='�����ǵ��ͽ���')";
        //�˴�Ϊ��̬������ݷ���
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int startrow = 3;
            int startcol = 2;
            int itemlengthcol = 2;
            int itemlengthrow = 3;
            string sqlwhere = " ProjectID='" + Tcommon.ProjectID + "'";
            IList<PS_Table_AreaWH> ptalist = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", sqlwhere);
            //�����趨����
            colcount = startcol + (ptalist.Count+1)* itemlengthcol;
            //��ѯ���վ��ѹ�ж��ٵȼ�����(����·�ȼ���ͬ)
            string tiaojian = " AreaID='" + Tcommon.ProjectID + "'  group by L1 order by L1 desc";
            //��¼��ѹ�ȼ�
            IList ptz = Services.BaseService.GetList("SelectPSP_Substation_InfoGroupL1", tiaojian);
            //�����趨����
            rowcount = startrow + ptz.Count * itemlengthrow;
            TC.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            string strgz = "����";
            string XLtiaojian = "";
            string dianya;
            int[] intcol=new int[ptalist.Count];
            //д���з���
            for (int k= 0; k < ptalist.Count; k++)
            {
                AddItemsCol(obj_sheet, ptalist[k].Title,startcol+(k+1)*itemlengthcol);
                intcol[k]=startcol+(k+1)*itemlengthcol;
            }
            FarPoint.Win.Spread.CellType.PercentCellType celltype = new FarPoint.Win.Spread.CellType.PercentCellType();
            celltype.DecimalPlaces = 2;
            for (int i = 0; i < ptz.Count; i++)
            {
                dianya = ptz[i].ToString();
                //д�б��⣨��ѹ�ȣ�
                AddItemsRow(obj_sheet, dianya, startrow + i * itemlengthrow);
                //ȫ�����
                TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + i * itemlengthrow + 1, startcol, 2, 1, startrow + i * itemlengthrow, startcol, 1);

                TC.Sheet_WriteFormula_ColSum_WritOne2(obj_sheet,startrow + i * itemlengthrow + 1,intcol,2,startcol);
                //�ٷֱ�
                obj_sheet.Cells[startrow + i * itemlengthrow + 1, startcol+1 ].Formula = "R" + (startrow + i * itemlengthrow + 1 + 1) + "C" + (startcol +1) + "/" + "R" + (startrow + i * itemlengthrow + 1) + "C" + (startcol + 1);
                obj_sheet.Cells[startrow + i * itemlengthrow + 1, startcol + 1].CellType = celltype;
                obj_sheet.Cells[startrow + i * itemlengthrow + 2, startcol+1 ].Formula = "R" + (startrow + i * itemlengthrow + 2 + 1) + "C" + (startcol + 1) + "/" + "R" + (startrow + i * itemlengthrow + 1) + "C" + (startcol + 1);
                obj_sheet.Cells[startrow + i * itemlengthrow + 2, startcol + 1].CellType = celltype;
                for (int j = 0; j < ptalist.Count; j++)
                {
                    XLtiaojian = " year(cast(OperationYear as datetime))<=" + TableYearsAry[0] + " and  Type='05' and ProjectID='" + Tcommon.ProjectID + "' and LineType2='" + strgz + "'and RateVolt=" + dianya+" and AreaID='"+ ptalist[j].ID+"' "+JXFSDX;
                    //���ͽ�����
                    int DXnum = 0;
                    if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian) != null)
                    {
                        DXnum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian);
                    }
                    obj_sheet.SetValue(startrow + i * itemlengthrow + 1, startcol + (j+1) * itemlengthcol, DXnum);
                    XLtiaojian = " year(cast(OperationYear as datetime))<=" + TableYearsAry[0] + " and  Type='05' and ProjectID='" + Tcommon.ProjectID + "' and LineType2='" + strgz + "'and RateVolt=" + dianya + " and AreaID='" + ptalist[j].ID + "' " + JXFSFDX;
                    //�ǵ��ͽ�����
                    int FDXnum = 0;
                    if (Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian) != null)
                    {
                        FDXnum = (int)Services.BaseService.GetObject("SelectPSPDEV_CountAll", XLtiaojian);
                    }
                    obj_sheet.SetValue(startrow + i * itemlengthrow + 2, startcol + (j+1) * itemlengthcol, FDXnum);
                    //�������
                    TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + i * itemlengthrow + 1, startcol + (j+1) * itemlengthcol, 2, 1, startrow + i * itemlengthrow, startcol + (j+1) * itemlengthcol, 1);
                    //�ٷֱ�
                    obj_sheet.Cells[startrow + i * itemlengthrow + 1, startcol + (j+1) * itemlengthcol + 1].Formula = "R" + (startrow + i * itemlengthrow + 1 + 1) + "C" + (startcol + (j+1)* itemlengthcol + 1) + "/" + "R" + (startrow + i * itemlengthrow + 1) + "C" + (startcol + (j+1)* itemlengthcol + 1);
                    obj_sheet.Cells[startrow + i * itemlengthrow + 1, startcol + (j + 1) * itemlengthcol + 1].CellType = celltype;
                    obj_sheet.Cells[startrow + i * itemlengthrow + 2, startcol + (j+1) * itemlengthcol + 1].Formula = "R" + (startrow + i * itemlengthrow + 2 + 1) + "C" + (startcol + (j+1)* itemlengthcol + 1) + "/" + "R" + (startrow + i * itemlengthrow + 1) + "C" + (startcol + (j+1) * itemlengthcol + 1);
                    obj_sheet.Cells[startrow + i * itemlengthrow + 2, startcol + (j + 1) * itemlengthcol + 1].CellType = celltype;
                }
            }
            
        }



    }
}