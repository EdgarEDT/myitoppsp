//********************************************************************************/
//
//�˴�����BuildSheetFromExcel�����������Զ�����.
//����ʱ��:2011-05-19 8:55:44
//
//********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Itop.Domain.Table;
using Itop.Client.Common;
using Itop.Domain.PWTable;
namespace Itop.Client.TableTemplateNW
{
    class Sheet2255
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
            //���27 ��13 ��
            rowcount = 27;
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
            obj_sheet.Columns[2].Width = 100;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 60;
            obj_sheet.Columns[5].Width = 60;
            obj_sheet.Columns[6].Width = 60;
            obj_sheet.Columns[7].Width = 60;
            obj_sheet.Columns[8].Width = 60;
            obj_sheet.Columns[9].Width = 60;
            obj_sheet.Columns[10].Width = 60;
            obj_sheet.Columns[11].Width = 100;
            obj_sheet.Columns[12].Width = 100;
            //�趨����и߶�
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            //д����������

            //2�б�������
            obj_sheet.SetValue(1, 0, "����");
            obj_sheet.SetValue(1, 1, "��Ŀ");
            obj_sheet.SetValue(1, 2, "���������䣨%��");
            obj_sheet.SetValue(1, 3, "<20");
            obj_sheet.SetValue(1, 4, "20~40");
            obj_sheet.SetValue(1, 5, "40~50");
            obj_sheet.SetValue(1, 6, "50~67");
            obj_sheet.SetValue(1, 7, "67~75");
            obj_sheet.SetValue(1, 8, "75~80");
            obj_sheet.SetValue(1, 9, "80~100");
            obj_sheet.SetValue(1, 10, ">100");
            obj_sheet.SetValue(1, 11, "������·������%��");
            obj_sheet.SetValue(1, 12, "������·������%��");
            //д����������

            //1�б�������
            obj_sheet.AddSpanCell(2, 0, 8, 1);
            obj_sheet.SetValue(2, 0, "XX�����أ�");
            obj_sheet.AddSpanCell(10, 0, 8, 1);
            obj_sheet.SetValue(10, 0, "xx�����أ�22");
            obj_sheet.AddSpanCell(18, 0, 8, 1);
            obj_sheet.SetValue(18, 0, "ȫ�кϼ�");
            obj_sheet.AddSpanCell(26, 0, 1, 3);
            obj_sheet.SetValue(26, 0, "�ϼ�");

            //2�б�������
            obj_sheet.AddSpanCell(2, 1, 6, 1);
            obj_sheet.SetValue(2, 1, "���ͽ��ߣ��أ�");
            obj_sheet.AddSpanCell(8, 1, 1, 2);
            obj_sheet.SetValue(8, 1, "������");
            obj_sheet.AddSpanCell(9, 1, 1, 2);
            obj_sheet.SetValue(9, 1, "�����ǵ��ͽ��ߣ��أ�");
            obj_sheet.AddSpanCell(10, 1, 6, 1);
            obj_sheet.SetValue(10, 1, "���ͽ��ߣ��أ�");
            obj_sheet.AddSpanCell(16, 1, 1, 2);
            obj_sheet.SetValue(16, 1, "������");
            obj_sheet.AddSpanCell(17, 1, 1, 2);
            obj_sheet.SetValue(17, 1, "�����ǵ��ͽ��ߣ��أ�");
            obj_sheet.AddSpanCell(18, 1, 6, 1);
            obj_sheet.SetValue(18, 1, "���ͽ��ߣ��أ�");
            obj_sheet.AddSpanCell(24, 1, 1, 2);
            obj_sheet.SetValue(24, 1, "������");
            obj_sheet.AddSpanCell(25, 1, 1, 2);
            obj_sheet.SetValue(25, 1, "�����ǵ��ͽ��ߣ��أ�");

            //3�б�������
            obj_sheet.SetValue(2, 2, "����һ��");
            obj_sheet.SetValue(3, 2, "����һ��");
            obj_sheet.SetValue(4, 2, "������");
            obj_sheet.SetValue(5, 2, "˫����");
            obj_sheet.SetValue(6, 2, "��ֶ�������");
            obj_sheet.SetValue(7, 2, "��ֶ�������");
            obj_sheet.SetValue(10, 2, "����һ��");
            obj_sheet.SetValue(11, 2, "����һ��");
            obj_sheet.SetValue(12, 2, "������");
            obj_sheet.SetValue(13, 2, "˫����");
            obj_sheet.SetValue(14, 2, "��ֶ�������");
            obj_sheet.SetValue(15, 2, "��ֶ�������");
            obj_sheet.SetValue(18, 2, "����һ��");
            obj_sheet.SetValue(19, 2, "����һ��");
            obj_sheet.SetValue(20, 2, "������");
            obj_sheet.SetValue(21, 2, "˫����");
            obj_sheet.SetValue(22, 2, "��ֶ�������");
            obj_sheet.SetValue(23, 2, "��ֶ�������");
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
            int startrow = 2;
            int addnum = 0;
            int itemcount = 0;
            int firstrow = 0;
            ArrayList rowsum = new ArrayList();
            Itop.Domain.PWTable.PW_tb3a p = new Itop.Domain.PWTable.PW_tb3a();
            p.col2 = Itop.Client.MIS.ProgUID;
            IList<PW_tb3a> alist = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aListDIS", p);

            for (int n = 0; n < alist.Count; n++)
            {
                for (int m = 0; m < 8; m++)
                {
                    obj_sheet.RowCount = obj_sheet.RowCount + 1;
                }

                obj_sheet.AddSpanCell(2, 0, 8, 1);
                obj_sheet.SetValue(2, 0, "XX�����أ�");

                //2�б�������
                obj_sheet.AddSpanCell(startrow+n * 8, 1, 6, 1);
                obj_sheet.SetValue(startrow + n * 8, 1, "���ͽ��ߣ��أ�");
                obj_sheet.AddSpanCell(startrow + (n + 1) * 8-1, 1, 1, 2);
                obj_sheet.SetValue(startrow + (n + 1) * 8-1, 1, "������");
                obj_sheet.AddSpanCell(startrow + (n + 1) * 8, 1, 1, 2);
                obj_sheet.SetValue(startrow + (n+1) * 8, 1, "�����ǵ��ͽ��ߣ��أ�");

                //3�б�������
                obj_sheet.SetValue(startrow + n * 8, 2, "����һ��");
                obj_sheet.SetValue(startrow + n * 8+1, 2, "����һ��");
                obj_sheet.SetValue(startrow + n * 8 + 2, 2, "������");
                obj_sheet.SetValue(startrow + n * 8 + 3, 2, "˫����");
                obj_sheet.SetValue(startrow + n * 8 + 4, 2, "��ֶ�������");
                obj_sheet.SetValue(startrow + n * 8 + 5, 2, "��ֶ�������");

                PW_tb3a _tba = alist[n];
                p.PQName = _tba.PQName;
                p.JXMS = "����һ��";
                IList<PW_tb3a> list1 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "����һ��";
                IList<PW_tb3a> list2 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "������";
                IList<PW_tb3a> list3 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "˫����";
                IList<PW_tb3a> list4 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "��ֶ�������";
                IList<PW_tb3a> list5 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "��ֶ�������";
                IList<PW_tb3a> list6 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "������";
                IList<PW_tb3a> list7 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                p.JXMS = "�����ǵ��ͽ���";
                IList<PW_tb3a> list8 = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2255", p);
                if (list1.Count > 0)
                {
                    PW_tb3a obj = list1[0];
                    obj_sheet.SetValue(n+startrow+n*7, 0, obj.PQName);
                    obj_sheet.SetValue(n+startrow+n*7, 3, obj.Num1);
                    obj_sheet.SetValue(n+startrow+n*7, 4, obj.Num2);
                    obj_sheet.SetValue(n+startrow+n*7, 5, obj.Num3);
                    obj_sheet.SetValue(n+startrow+n*7, 6, obj.Num4);
                    obj_sheet.SetValue(n+startrow+n*7, 7, obj.Num5);
                    obj_sheet.SetValue(n+startrow+n*7, 8, obj.Num6);
                    obj_sheet.SetValue(n+startrow+n*7, 9, obj.Num7);
                    obj_sheet.SetValue(n+startrow+n*7, 10, obj.Num8);
                }
                else
                {
                    TC.Sheet_WriteZero(obj_sheet, n + startrow+n*7,3,1,8);
                }
            }
        }

       

    }
}
