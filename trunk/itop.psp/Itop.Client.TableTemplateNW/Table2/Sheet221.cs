//********************************************************************************/
//
//�˴�����BuildSheetFromExcel�����������Զ�����.
//����ʱ��:2011-5-19 21:33:43
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
    class Sheet221
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
            //���8 ��6 ��
            rowcount = 8;
            colcount = 6;
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
            obj_sheet.Columns[1].Width = 100;
            obj_sheet.Columns[2].Width = 100;
            obj_sheet.Columns[3].Width = 80;
            obj_sheet.Columns[4].Width = 100;
            obj_sheet.Columns[5].Width = 80;
            //�趨����и߶�
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
            //д����������

            //2�б�������
            obj_sheet.AddSpanCell(1, 0, 2, 1);
            obj_sheet.AddSpanCell(1, 1, 2, 1);
            obj_sheet.SetValue(1, 2, "������");
            obj_sheet.AddSpanCell(1, 3, 2, 1);
            obj_sheet.SetValue(1, 3, "�����ʣ�%��");
            obj_sheet.SetValue(1, 4, "���縺��");
            obj_sheet.AddSpanCell(1, 5, 2, 1);
            obj_sheet.SetValue(1, 5, "�����ʣ�%��");

            //3�б�������
            obj_sheet.SetValue(2, 2, "����ǧ��ʱ��");
            obj_sheet.SetValue(2, 4, "����ǧ�ߣ�");
            //д����������

            //1�б�������
            obj_sheet.SetValue(3, 0, "1");
           

            //2�б�������
            obj_sheet.SetValue(3, 1, "ȫ��");
          
            //�������
            Sheet_AddData(obj_sheet);

            //�趨�����
            TC.Sheet_GridandCenter(obj_sheet);

            //�������
            TC.Sheet_Locked(obj_sheet);
        }


        //�˴�Ϊ��̬������ݷ���
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {

            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int startrow = 3;
            int itemlength = 9;
            string sqlwhere = " ProjectID='" + Tcommon.ProjectID + "'";
            IList<PS_Table_AreaWH> ptalist = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", sqlwhere);
            //�����趨����
            obj_sheet.RowCount = startrow + (ptalist.Count + 1);
            for (int i = 0; i < ptalist.Count; i++)
            {
                obj_sheet.SetValue(startrow + 1 + i, 0, i + 2);
                obj_sheet.SetValue(startrow + 1 + i, 1, ptalist[i].Title);
                string sqlwhere2 = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "' and Title='" + ptalist[i].Title + "'";
                IList<Ps_History> phlist = Services.BaseService.GetList<Ps_History>("SelectPs_HistoryBYconnstr", sqlwhere2);
                if (phlist.Count > 0)
                {
                    string yearstr = "y" + TableYearsAry[0].ToString();
                    string peryearstr = "y" + (TableYearsAry[0]-1);
                    string sqlwhere3 = " ForecastID='4' and Col4='" + Tcommon.ProjectID + "' and ParentID='" + phlist[0].ID + "'";
                    IList phlist3 = Services.BaseService.GetList("SelectPs_HistoryBYconnstr", sqlwhere3);
                    DataTable dt = DataConverter.ToDataTable(phlist3, typeof(Ps_History));
                    DataRow[] rows1 = dt.Select("Title like '������%'");
                    DataRow[] rows2 = dt.Select("Title like '���縺��%'");
                    if (rows1.Length != 0)
                    {
                        obj_sheet.SetValue(startrow + 1 + i, 2 , rows1[0][yearstr]);
                        double tempdb = (Convert.ToDouble(rows1[0][yearstr]) - Convert.ToDouble(rows1[0][peryearstr])) / Convert.ToDouble(rows1[0][peryearstr]);
                        if (tempdb.ToString() == "������" || tempdb.ToString() == "�������")
                            tempdb = 0;
                        obj_sheet.SetValue(startrow + 1 + i, 3, tempdb);
                        TC.SetSheetCellType(obj_sheet, startrow + 1 + i, 3, 2, 2);
                    }
                    else
                    {
                        TC.WriteQuestion(title, ptalist[i].Title + "�޹���������", "��ѯ ��������ʵ�������Ƿ��и�������������", "");
                    
                    }
                    if (rows2.Length != 0)
                    {
                        obj_sheet.SetValue(startrow + 1 + i, 4, rows2[0][yearstr]);
                        double tempdb = (Convert.ToDouble(rows2[0][yearstr]) - Convert.ToDouble(rows2[0][peryearstr])) / Convert.ToDouble(rows2[0][peryearstr]);
                        if (tempdb.ToString() == "������" || tempdb.ToString() == "�������")
                            tempdb = 0;
                        obj_sheet.SetValue(startrow + 1 + i, 5, tempdb);
                        TC.SetSheetCellType(obj_sheet, startrow + 1 + i, 5, 2, 2);
                    }
                    else
                    {
                        TC.WriteQuestion(title, ptalist[i].Title + "�޹��縺������", "��ѯ ��������ʵ�������Ƿ��и������縺������", "");
                    }

                }
               
            }
            TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + 1, 2, ptalist.Count, 1, startrow , 2, 1);
            TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + 1, 4, ptalist.Count, 1, startrow , 4, 1);

        }



    }
}
