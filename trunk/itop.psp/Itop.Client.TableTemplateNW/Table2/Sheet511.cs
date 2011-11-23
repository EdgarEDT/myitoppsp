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
    class Sheet511
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
        private void Build_Sheet(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //���15 ��13 ��
            rowcount = 15;
            colcount = 13;
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
            obj_sheet.Columns[1].Width = 60;
            obj_sheet.Columns[2].Width = 60;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 60;
            obj_sheet.Columns[5].Width = 60;
            //obj_sheet.Columns[6].Width = 60;
            //obj_sheet.Columns[7].Width = 60;
            //obj_sheet.Columns[8].Width = 60;
            //obj_sheet.Columns[9].Width = 60;
            //obj_sheet.Columns[10].Width = 60;
            //obj_sheet.Columns[11].Width = 60;
            //obj_sheet.Columns[12].Width = 60;
            //�趨����и߶�
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
            //д����������

            //2�б�������
            obj_sheet.AddSpanCell(1, 0, 2, 1);
            obj_sheet.SetValue(1, 0, "���");
            obj_sheet.AddSpanCell(1, 1, 2, 1);
            obj_sheet.SetValue(1, 1, "��Ŀ����");
            obj_sheet.AddSpanCell(1, 2, 2, 1);
            obj_sheet.SetValue(1, 2, "��Դ����");
            obj_sheet.AddSpanCell(1, 3, 2, 1);
            obj_sheet.SetValue(1, 3, "װ������");
            obj_sheet.AddSpanCell(1, 4, 2, 1);
            obj_sheet.SetValue(1, 4, "�����ѹ");
            obj_sheet.AddSpanCell(1, 5, 2, 1);
            obj_sheet.SetValue(1, 5, "�������");
          
            //д����������

            //1�б�������
            obj_sheet.SetValue(3, 0, "1��");
            obj_sheet.SetValue(4, 0, "2��");
            obj_sheet.SetValue(5, 0, "1)");
            obj_sheet.SetValue(6, 0, "2)");
            obj_sheet.SetValue(7, 0, "3)");
            obj_sheet.SetValue(8, 0, "4)");
            obj_sheet.SetValue(9, 0, "5)");
            obj_sheet.SetValue(11, 0, "3��");
            obj_sheet.SetValue(12, 0, "1��");
            obj_sheet.SetValue(13, 0, "2)");
            obj_sheet.SetValue(14, 0, "3��");

            //2�б�������
            obj_sheet.SetValue(3, 1, "��ĩװ������");
            obj_sheet.SetValue(4, 1, "���м��½���Դ��ĿС��");
            obj_sheet.SetValue(5, 1, "XX�糧");
            obj_sheet.SetValue(6, 1, "XX�糧");
            obj_sheet.SetValue(7, 1, "XX�糧����");
            obj_sheet.SetValue(8, 1, "XX�糧����");
            obj_sheet.SetValue(9, 1, "����");
            obj_sheet.SetValue(11, 1, "���۵�Դ��ĿС��");
            obj_sheet.SetValue(12, 1, "XX");
            obj_sheet.SetValue(13, 1, "XX");
            obj_sheet.SetValue(14, 1, "����");
            //�������
            Sheet_AddData(obj_sheet);

            //�趨�����
            TC.Sheet_GridandCenter(obj_sheet);

            //�������
            TC.Sheet_Locked(obj_sheet);
        }

        private void AddItemsCol(FarPoint.Win.Spread.SheetView obj_sheet, int[] TableYearsAry)
        {
            //int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            for (int i = 0; i < TableYearsAry.Length; i++)
            {
                obj_sheet.SetValue(2, 6+i,TableYearsAry[i]);
            }

            obj_sheet.AddSpanCell(1, 6, 1, TableYearsAry.Length);
            obj_sheet.SetValue(1, 6, "Ͷ����ݺ�Ͷ����ģ");
        }
        //�˴�Ϊ��̬������ݷ���
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int startrow = 3;
            int startcol = 2;
            int itemlengthcol = 2;
            int itemlengthrow = 3;
            //�����趨����
            colcount = startcol + TableYearsAry.Length + 4;
            string sqlwhere = " ProjectID='" + Tcommon.ProjectID + "'";
            IList<PS_Table_AreaWH> ptalist = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", sqlwhere);
                       //������¼��ĩ����
            int[] rongliang = new int[TableYearsAry.Length];


            for (int i = 0; i < ptalist.Count; i++)
            {
               // string sqlwhere2= "select a.* from Ps_PowerBuild as a,Ps_PowerBuild as b where a.ParentID=b.ID and b.Title='"+ptalist[i].Title+"' and b.ProjectID='" + Tcommon.ProjectID + "'";
               // IList ptblist2=Services.BaseService.GetList("SelectPowerBuildBYAllWHere", sqlwhere2);
               // DataTable dt2 = DataConverter.ToDataTable(ptblist2, typeof(Ps_History));
               //select a.* from Ps_PowerBuild as a,Ps_PowerBuild as b
               // where a.ParentID=b.ID and b.Title='��ǽ���' and b.ProjectID='85c066c7-a4d7-469b-928b-5d9e86280400';
               // select c.* from Ps_PowerBuild as a,Ps_PowerBuild as b,Ps_PowerBuild as c
               // where a.ParentID=b.ID and c.ParentID=a.ID and b.Title='��ǽ���' and (a.Title='�ѽ���ĿС��' or a.Title='�ڽ����½���ĿС��')  and b.ProjectID='85c066c7-a4d7-469b-928b-5d9e86280400';
                
               // for (int j = 0; j < TableYearsAry.Length; j++)
               // {
                   
               // }
            }

        }



    }
}
