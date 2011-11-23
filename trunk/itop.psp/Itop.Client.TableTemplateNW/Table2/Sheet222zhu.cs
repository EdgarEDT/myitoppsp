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
    class Sheet222zhu
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
            //���5 ��7 ��
            rowcount = 5;
            colcount = 7;
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
            obj_sheet.Columns[1].Width = 80;
            obj_sheet.Columns[2].Width = 120;
            obj_sheet.Columns[3].Width = 80;
            obj_sheet.Columns[4].Width = 80;
            obj_sheet.Columns[5].Width = 80;
            obj_sheet.Columns[6].Width = 240;
            //�趨����и߶�
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            //д����������

            //2�б�������
            obj_sheet.SetValue(1, 0, "���");
            obj_sheet.SetValue(1, 1, "��������");
            obj_sheet.SetValue(1, 2, "����");
            obj_sheet.SetValue(1, 3, "�����ѹ");
            obj_sheet.SetValue(1, 4, "����");
            obj_sheet.SetValue(1, 5, "װ������");
            obj_sheet.SetValue(1, 6, "��ע");
            //д����������

            //1�б�������
           
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
            int startrow = 2;
            int itemlength = 1;
            string sqlwhere = " AreaID='" + Tcommon.ProjectID + "' and Flag='1' and Cast(S3 as int)<=" + TableYearsAry[0] + " and Cast(S1 as int)<=110 order by  Cast(S1 as int) desc";
            IList<Itop.Client.Stutistics.PSP_PowerSubstationInfo> ptalist = Services.BaseService.GetList<Itop.Client.Stutistics.PSP_PowerSubstationInfo>("SelectPSP_PowerSubstationInfoByConn", sqlwhere);
            //�����趨����
            obj_sheet.RowCount = startrow + (ptalist.Count);
            if (ptalist.Count==0)
            {
                TC.WriteQuestion(title,  "�޵�Դ����", "��ѯ �豸������Դ�����Ƿ����������", "");
            }
            for (int i = 0; i < ptalist.Count; i++)
            {
                obj_sheet.SetValue(startrow + i, 0,i+1);
                obj_sheet.SetValue(startrow + i, 1, ptalist[i].S9);
                obj_sheet.SetValue(startrow + i, 2, ptalist[i].Title);
                obj_sheet.SetValue(startrow + i, 3, ptalist[i].S1);
                obj_sheet.SetValue(startrow + i, 4, ptalist[i].S10);
                obj_sheet.SetValue(startrow + i, 5, ptalist[i].S2);
                obj_sheet.SetValue(startrow + i, 6, ptalist[i].S20);
            }


        }



    }
}
