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
    class Sheet2253
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
            //���12 ��16 ��
            rowcount = 12;
            colcount = 16;
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
            obj_sheet.Columns[6].Width = 60;
            obj_sheet.Columns[7].Width = 60;
            obj_sheet.Columns[8].Width = 60;
            obj_sheet.Columns[9].Width = 60;
            obj_sheet.Columns[10].Width = 60;
            obj_sheet.Columns[11].Width = 60;
            obj_sheet.Columns[12].Width = 60;
            obj_sheet.Columns[13].Width = 60;
            obj_sheet.Columns[14].Width = 60;
            obj_sheet.Columns[15].Width = 60;
            //�趨����и߶�
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            obj_sheet.Rows[2].Height = 20;
            obj_sheet.Rows[3].Height = 20;
            //д����������

            //2�б�������
            obj_sheet.AddSpanCell(1, 0, 3, 1);
            obj_sheet.SetValue(1, 0, "����");
            obj_sheet.AddSpanCell(1, 1, 3, 1);
            obj_sheet.SetValue(1, 1, "����������");
            obj_sheet.AddSpanCell(1, 2, 3, 1);
            obj_sheet.SetValue(1, 2, "��ѹ�ȼ�(ǧ��)");
            obj_sheet.AddSpanCell(1, 3, 3, 1);
            obj_sheet.SetValue(1, 3, "��·����");
            obj_sheet.AddSpanCell(1, 4, 1, 8);
            obj_sheet.SetValue(1, 4, "���ֽ���ģʽ�ı�����%��");
            obj_sheet.AddSpanCell(1, 12, 3, 1);
            obj_sheet.SetValue(1, 12, "�����ʣ�%��");
            obj_sheet.AddSpanCell(1, 13, 3, 1);
            obj_sheet.SetValue(1, 13, "������߱�׼���ʣ�%��");
            obj_sheet.AddSpanCell(1, 14, 3, 1);
            obj_sheet.SetValue(1, 14, "վ�������ʣ�%��");
            obj_sheet.AddSpanCell(1, 15, 3, 1);
            obj_sheet.SetValue(1, 15, "��·ƽ���ֶ���");

            //3�б�������
            obj_sheet.AddSpanCell(2, 4, 1, 6);
            obj_sheet.SetValue(2, 4, "���ͽ���");
            obj_sheet.AddSpanCell(2, 10, 2, 1);
            obj_sheet.SetValue(2, 10, "������");
            obj_sheet.AddSpanCell(2, 11, 2, 1);
            obj_sheet.SetValue(2, 11, "�����ǵ��ͽ���");

            //4�б�������
            obj_sheet.SetValue(3, 4, "��������");
            obj_sheet.SetValue(3, 5, "˫������");
            obj_sheet.SetValue(3, 6, "����һ��");
            obj_sheet.SetValue(3, 7, "����һ��");
            obj_sheet.SetValue(3, 8, "��ֶ�������");
            obj_sheet.SetValue(3, 9, "��ֶ�������");
            //д����������

            //1�б�������
            obj_sheet.AddSpanCell(4, 0, 4, 1);
            obj_sheet.SetValue(4, 0, "XX�����أ�");
            obj_sheet.AddSpanCell(8, 0, 4, 1);
            obj_sheet.SetValue(8, 0, "ȫ�кϼ�");

            //2�б�������
            obj_sheet.SetValue(4, 1, "A");
            obj_sheet.SetValue(5, 1, "B");
            obj_sheet.SetValue(6, 1, "��");
            obj_sheet.SetValue(7, 1, "С��");
            obj_sheet.SetValue(8, 1, "A");
            obj_sheet.SetValue(9, 1, "B");
            obj_sheet.SetValue(10, 1, "��");
            obj_sheet.SetValue(11, 1, "�ϼ�");

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
            int startrow = 4;
            int addnum = 0;
            int itemcount = 0;
            int firstrow = 0;
            ArrayList rowsum = new ArrayList();
            Itop.Domain.PWTable.PW_tb3a p = new Itop.Domain.PWTable.PW_tb3a();
            p.col2 = Itop.Client.MIS.ProgUID;
            IList<PW_tb3a> list = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aBy2253", p);

            PW_tb3c p3 = new PW_tb3c();
            p3.col4 = Itop.Client.MIS.ProgUID;
            IList<PW_tb3c> plist = Services.BaseService.GetList<PW_tb3c>("SelectPW_tb3cList", p3);
            IList<PW_tb3a> alist = Services.BaseService.GetList<PW_tb3a>("SelectPW_tb3aListDIS",p);

            for (int n = 0; n < plist.Count; n++)
            {
                rowsum.Add("");
            }
            bool sheetEnd = false;
            for (int i = 0; i < list.Count;i++ )
            {
                
                PW_tb3a obj = list[i];
                PW_tb3a obj2 = new PW_tb3a();
                if (i < list.Count - 1)
                {
                    obj2 = list[i + 1];
                }
                else
                {
                    sheetEnd = true;
                }
                itemcount = itemcount + 1;
                obj_sheet.RowCount = obj_sheet.RowCount + 1;
                obj_sheet.SetValue(i + addnum + startrow, 0, obj.PQName);
                obj_sheet.SetValue(i + addnum + startrow, 1, obj.PQtype);
                obj_sheet.SetValue(i + addnum + startrow, 2, "10kV");
                obj_sheet.SetValue(i + addnum + startrow, 3, obj.Num10);
                obj_sheet.SetValue(i + addnum + startrow, 4, obj.Num1);
                obj_sheet.SetValue(i + addnum + startrow, 5, obj.Num2);
                obj_sheet.SetValue(i + addnum + startrow, 6, obj.Num3);
                obj_sheet.SetValue(i + addnum + startrow, 7, obj.Num4);
                obj_sheet.SetValue(i + addnum + startrow, 8, obj.Num5);
                obj_sheet.SetValue(i + addnum + startrow, 9, obj.Num6);
                obj_sheet.SetValue(i + addnum + startrow, 10, obj.Num7);
                obj_sheet.SetValue(i + addnum + startrow, 11, obj.Num8);
               // obj_sheet.SetValue(i + 3, 10, 1);
                if (obj.PQName != obj2.PQName)
                {
                    addnum = addnum + 1;
                        for (int x = 0; x < rowsum.Count; x++)
                        {
                            string str = (string)rowsum[x];
                            str = str + Convert.ToString(i + addnum + startrow - x - 1) + ",";
                            rowsum[x] = str;
                        }
                   
                    obj_sheet.RowCount = obj_sheet.RowCount + 1;
                    obj_sheet.SetValue(i + addnum + startrow, 0, obj.PQName);
                    obj_sheet.SetValue(i + addnum + startrow, 1, "С��");
                    int num1 = startrow + addnum + firstrow;
                    if (firstrow == 0) { num1 = num1 - 1; }
                    TC.Sheet_WriteFormula_RowSum(obj_sheet, num1, 3, itemcount, 1, i + addnum + startrow, 3, 9);
                    obj_sheet.AddSpanCell(num1, 0, itemcount+1, 1);
                    firstrow = i;
                    startrow = 4;
                    itemcount = 0;
                }
                if (sheetEnd)
                {
                    int[] sum = new int[plist.Count];
                    rowsum.Reverse();
                    for (int m = 0; m < plist.Count; m++)
                    {
                        PW_tb3c _p = plist[m];
                        addnum = addnum + 1;
                        obj_sheet.RowCount = obj_sheet.RowCount + 1;
                        obj_sheet.SetValue(i + addnum + startrow, 0, "ȫ�кϼ�");
                        obj_sheet.SetValue(i + addnum + startrow, 1, _p.col1);
                        obj_sheet.SetValue(i + addnum + startrow, 2, "10kV");
                        sum[m] = i + addnum + startrow;
                        int num1 = startrow + addnum + firstrow;
                        //if (firstrow == 0) { num1 = num1 - 1; }
                        TC.Sheet_WriteFormula_RowSum3(obj_sheet, TC.getRowList(rowsum[m].ToString()), 3, i + addnum + startrow, 3, 9);
                    }
                    addnum = addnum + 1;
                    obj_sheet.RowCount = obj_sheet.RowCount + 1;
                    obj_sheet.SetValue(i + addnum + startrow, 0, "ȫ�кϼ�");
                    obj_sheet.SetValue(i + addnum + startrow, 1, "�ܼ�");
                    TC.Sheet_WriteFormula_RowSum3(obj_sheet, sum, 3, i + addnum + startrow, 3, 9);
                    obj_sheet.AddSpanCell(sum[0], 0, sum.Length+1, 1);
                }
            }
        }

       

    }
}
