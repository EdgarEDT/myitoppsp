//********************************************************************************/
//
//�˴�����BuildSheetFromExcel�����������Զ�����.
//����ʱ��:2011-5-19 9:05:55
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
namespace Itop.Client.TableTemplateNW
{
    class Sheet212
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
        public void Build_Sheet(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //�������й����
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);

            //���29 ��8 ��
            rowcount = 29;
            colcount = 2 + TableYearsAry.Length+1;
            //�������һ�еı���
            title = TC.GetTableTitle(this.GetType().Name);
           
            //��������
            sheetname = title;
            //�趨����������ֵ������ͱ���
            TC.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //�趨����ģʽ���Ա�д��ʽʹ��
            TC.Sheet_Referen_R1C1(obj_sheet);
            //�趨����п��
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 150;
            
            //�趨����и߶�
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            //д����������

            //2�б�������
            obj_sheet.SetValue(1, 0, "����");
            obj_sheet.SetValue(1, 1, "ָ������");
            for (int i = 0; i < TableYearsAry.Length; i++)
            {
                obj_sheet.SetValue(1, 2 + i, TableYearsAry[i]);
                obj_sheet.Columns[2 + i].Width = 60;
               
            }

            obj_sheet.SetValue(1, 2 + TableYearsAry.Length, "���������(%)");
            obj_sheet.Columns[2 + TableYearsAry.Length].Width = 120;
          
            //д����������

            //�������
            Sheet_AddData(obj_sheet);

            //�趨�����
            TC.Sheet_GridandCenter(obj_sheet);

            //�������
            TC.Sheet_Locked(obj_sheet);
        }

        private void AddItems(FarPoint.Win.Spread.SheetView obj_sheet,string Area, int rowstart)
        {
            obj_sheet.AddSpanCell(rowstart, 0, 9, 1);
            obj_sheet.SetValue(rowstart, 0, Area);
            obj_sheet.SetValue(rowstart++, 1, "����������ֵ����Ԫ��");
            obj_sheet.SetValue(rowstart++, 1, "��һ��ҵ");
            obj_sheet.SetValue(rowstart++, 1, "�ڶ���ҵ");
            obj_sheet.SetValue(rowstart++, 1, "������ҵ");
            obj_sheet.SetValue(rowstart++, 1, "�˿ڣ����ˣ�");
            obj_sheet.SetValue(rowstart++, 1, "�˾�GDP����Ԫ��");
            obj_sheet.SetValue(rowstart++, 1, "���������ƽ��ǧ�ף�");
            obj_sheet.SetValue(rowstart++, 1, "�����������ƽ��ǧ�ף�");
            obj_sheet.SetValue(rowstart++, 1, "�����ʣ�%��");
        }
        //�˴�Ϊ��̬������ݷ���
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int startrow = 2;
            int itemlength = 9;
            string sqlwhere = " ProjectID='" + Tcommon.ProjectID + "'";
            IList<PS_Table_AreaWH> ptalist = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", sqlwhere);
           //�����趨����
            obj_sheet.RowCount = startrow + (ptalist.Count + 1) * itemlength;


            string sqlwheretemp = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "'";
            IList phlisttemp= Services.BaseService.GetList("SelectPs_HistoryBYconnstr", sqlwheretemp);
            DataTable dttemp = DataConverter.ToDataTable(phlisttemp, typeof(Ps_History));
                    
            Ps_History AllRk = new Ps_History();
            Ps_History CZrk = new Ps_History();
            DataRow RowAllrk = dttemp.NewRow(); ;
            DataRow RowCZrk = dttemp.NewRow();

            RowAllrk = DataConverter.ObjectToRow(AllRk,RowAllrk);
            RowCZrk = DataConverter.ObjectToRow(CZrk, RowCZrk);
            for (int i = 0; i < ptalist.Count; i++)
            {
                AddItems(obj_sheet, ptalist[i].Title, startrow + i * itemlength);
                string sqlwhere2 = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "' and Title='" + ptalist[i].Title + "'";
                IList<Ps_History> phlist = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryBYconnstr", sqlwhere2);
                if (phlist.Count > 0)
                {
                    string sqlwhere3 = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "' and ParentID='" + phlist[0].ID + "'";
                    IList phlist3 = Services.BaseService.GetList("SelectPs_HistoryBYconnstr", sqlwhere3);
                    DataTable dt = DataConverter.ToDataTable(phlist3, typeof(Ps_History));
                    DataRow[] rows1 = dt.Select("Title like 'һ��%'");
                    DataRow[] rows2 = dt.Select("Title like '����%'");
                    DataRow[] rows3 = dt.Select("Title like '����%'");
                    DataRow[] rows4 = dt.Select("Title like '�˿�%'");
                    
                     DataRow[] rows7=null;
                     DataRow[] rows8 = null;
                    if (rows4.Length!=0)
                    {
                        string sqlwhere4 = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "' and ParentID='" + rows4[0]["ID"] + "'";
                        IList phlist4 = Services.BaseService.GetList("SelectPs_HistoryBYconnstr", sqlwhere4);
                        DataTable dt2 = DataConverter.ToDataTable(phlist4, typeof(Ps_History));
                        rows7 = dt2.Select("Title like '�����˿�%'");
                        rows8 = dt2.Select("Title like '����˿�%'");
                    }
                   
                    DataRow[] rows5 = dt.Select("Title like '�������%'");
                    DataRow[] rows6 = dt.Select("Title like '���������%'");
                    //����������ֵ����Ԫ��=һ��+����+����
                    TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + i * itemlength + 1, 2, 3, 1, startrow + i * itemlength, 2, TableYearsAry.Length);
                    //�˾�GDP����Ԫ��=����������ֵ����Ԫ��/�˿ڣ�
                    TC.Sheet_WriteFormula_OneRow_AnoterRow_nopercent(obj_sheet, startrow + i * itemlength + 4, 2, startrow + i * itemlength, startrow + i * itemlength + 5, TableYearsAry.Length);

                    for (int j = 0; j < TableYearsAry.Length; j++)
                    {
                        int m = 0;
                        //һ��
                        string yearstr="y" + TableYearsAry[j].ToString();
                        m++;
                        if (rows1.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows1[0][yearstr]);
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "��һ������", "��ѯ ��������ʵ�������Ƿ��и���һ������", "");
                        }
                        //����
                        m++;
                        if (rows2.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows2[0][yearstr]);
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "�޶�������", "��ѯ ��������ʵ�������Ƿ��и�����������", "");
                        }
                        //����
                        m++;
                        if (rows3.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows3[0][yearstr]);
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "����������", "��ѯ ��������ʵ�������Ƿ��и�����������", "");
                        }
                        //�˿�
                        m++;
                        if (rows4.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows4[0][yearstr]);
                          
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "���˿�����", "��ѯ ��������ʵ�������Ƿ��и����˿�����", "");
                        }
                        m++;
                        //���������ƽ��ǧ�ף�
                        m++;
                        if (rows5.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows5[0][yearstr]);
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "�������������", "��ѯ ��������ʵ�������Ƿ��и��������������", "");
                        }
                        //�����������ƽ��ǧ�ף�
                        m++;
                        if (rows6.Length != 0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, rows6[0][yearstr]);
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "�޽������������", "��ѯ ��������ʵ�������Ƿ��и����������������", "");
                        }
                        //�����ʣ�%��
                        m++;
                        if (rows7 != null &&rows4.Length!=0)
                        {
                            obj_sheet.SetValue(startrow + i * itemlength + m, 2 + j, Convert.ToDouble(rows7[0][yearstr] )/ Convert.ToDouble(rows4[0][yearstr]));
                            FarPoint.Win.Spread.CellType.PercentCellType  newcelltype =new FarPoint.Win.Spread.CellType.PercentCellType();
                            newcelltype.DecimalPlaces = 2;
                            obj_sheet.Cells[startrow + i * itemlength + m, 2 + j].CellType = newcelltype;
                           
                            RowAllrk[yearstr] = Convert.ToDouble(RowAllrk[yearstr]) + Convert.ToDouble(rows4[0][yearstr]);
                            RowCZrk[yearstr] = Convert.ToDouble(RowCZrk[yearstr]) + Convert.ToDouble(rows7[0][yearstr]);
                          
                        }
                        else
                        {
                            TC.WriteQuestion(title, ptalist[i].Title + "�޳����˿ڻ����˿�����", "��ѯ ��������ʵ�������Ƿ��и��������˿ڻ����˿�����", "");
                        }

                    }

                }
            }


            AddItems(obj_sheet, "ȫ��",  startrow + ptalist.Count * itemlength);
            TC.Sheet_WriteFormula_RowSum2(obj_sheet, startrow, 2, ptalist.Count, itemlength, startrow + ptalist.Count * itemlength, 2, 1, 3, TableYearsAry.Length);
            TC.Sheet_WriteFormula_RowSum2(obj_sheet, startrow, 2, ptalist.Count, itemlength, startrow + ptalist.Count * itemlength, 2, 6, 2, TableYearsAry.Length);
           
            //����������ֵ����Ԫ��=һ��+����+����
            TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + ptalist.Count * itemlength+1, 2, 3, 1, startrow + ptalist.Count * itemlength , 2, TableYearsAry.Length);

            //�˾�GDP����Ԫ��=����������ֵ����Ԫ��/�˿ڣ�
            TC.Sheet_WriteFormula_OneRow_AnoterRow_nopercent(obj_sheet, startrow + ptalist.Count * itemlength + 4, 2, startrow + ptalist.Count * itemlength, startrow + ptalist.Count * itemlength + 5,TableYearsAry.Length);
           //��������
            for (int k = 0; k < TableYearsAry.Length; k++)
            {
               
                string yearstr = "y" + TableYearsAry[k].ToString();
                obj_sheet.SetValue(startrow + ptalist.Count * itemlength + 8, 2 + k, Convert.ToDouble(RowCZrk[yearstr]) / Convert.ToDouble(RowAllrk[yearstr]));
                FarPoint.Win.Spread.CellType.PercentCellType newcelltype = new FarPoint.Win.Spread.CellType.PercentCellType();
                newcelltype.DecimalPlaces = 2;
                obj_sheet.Cells[startrow + ptalist.Count * itemlength + 8, 2 + k].CellType = newcelltype;
            }
            //������ƽ��������
            for (int l = 0; l < (ptalist.Count+1)*itemlength; l++)
            {
                obj_sheet.Cells[startrow + l, 2 + TableYearsAry.Length].Formula = " Power(R" + (startrow + l + 1) + "C" + (2 + TableYearsAry.Length) + "/R" + (startrow + l + 1) + "C" + 3 + "," + (1.000 / TableYearsAry.Length) + ")-1";
                FarPoint.Win.Spread.CellType.PercentCellType newcelltype = new FarPoint.Win.Spread.CellType.PercentCellType();
                newcelltype.DecimalPlaces = 2;
                obj_sheet.Cells[startrow + l, 2 + TableYearsAry.Length].CellType = newcelltype;
            }
                          
        }



    }
}
