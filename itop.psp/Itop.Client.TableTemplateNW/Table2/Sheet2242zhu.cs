using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Itop.Domain.Table;
using Itop.Client.Common;
namespace Itop.Client.TableTemplateNW.Table2
{
    class Sheet2242zhu
    {
        ////�滮��������
        //string CityName = Tcommon.CityName;
        ////��ǰ���
        //int nowyear = Tcommon.CurrentYear;
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
        public void Build_sheet(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            //�����7 ��
            string con = " ProjectID='" + ProjectID + "|pro" + "' and ParentID='0'";
            //�������
            IList<Ps_Table_BuildPro> listTypes = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", con);
            //���������
            int startrow = 3;
           
            rowcount = startrow ;
            colcount = 7;
            //�������һ�еı���
            //title = CityName+"�и�ѹ������ѿ�չ������Ŀ�б�";
            title = TC.GetTableTitle(this.GetType().Name);
            sheetname = title;
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            //��������
            //sheetname = CityName + "�и�ѹ������ѿ�չ������Ŀ�б�";
           //int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            //�趨����������ֵ������ͱ���
            TC.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            //�趨�����
            TC.Sheet_GridandCenter(obj_sheet);
            //�趨����ģʽ���Ա�д��ʽʹ��
            TC.Sheet_Referen_R1C1(obj_sheet);
            //�趨����п��
            obj_sheet.Columns[0].Width = 60;
            obj_sheet.Columns[1].Width = 190;
            obj_sheet.Columns[2].Width = 60;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 90;
            obj_sheet.Columns[5].Width = 60;
            obj_sheet.Columns[6].Width = 60;
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
            obj_sheet.AddSpanCell(1, 2, 1, 2);
            obj_sheet.SetValue(1, 2, "�����ģ");
            obj_sheet.AddSpanCell(1, 4, 2, 1);
            obj_sheet.SetValue(1, 4, "���������أ�");
            obj_sheet.AddSpanCell(1, 5, 2, 1);
            obj_sheet.SetValue(1, 5, "��ǰ����");
            obj_sheet.AddSpanCell(1, 6, 2, 1);
            obj_sheet.SetValue(1, 6, "Ͷ�����");

            //3�б�������
            obj_sheet.SetValue(2, 2, "��·����");
            obj_sheet.SetValue(2, 3, "�������");
            //�������
            Sheet_AddData(obj_sheet);
            TC.Sheet_GridandCenter(obj_sheet);
            //�������
            TC.Sheet_Locked(obj_sheet);
        }
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int currentyear = TableYearsAry[0];
            string tiaojian = "";
            int startrow = 3-1;
            tiaojian = " ProjectID='" + ProjectID + "|pro" + "' and ParentID='0' order by Cast(FromID as int) desc";
            //������������
            int pro_num = 0;
            int m = 0;
            try
            {
                //�������
                IList<Ps_Table_BuildPro> listTypes = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", tiaojian);

                //���ݷ�����������Ŀ��
                for (int i = 0; i < listTypes.Count; i++)
                {
                    //Ͷ����ݴ��ڵ��ڵ�ǰ���
                    string tempcon = " ProjectID='" + ProjectID + "|pro" + "' and  ParentID='" + listTypes[i].ID + "' and cast(BuildEd as int)>=" + currentyear;
                    IList<Ps_Table_BuildPro> tempptblist = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", tempcon);
                    if (tempptblist.Count > 0)
                    {
                        pro_num++;
                        m++;
                        obj_sheet.RowCount = obj_sheet.RowCount + 1;
                        obj_sheet.SetValue(startrow + m, 0, TC.CHNumberToChar(pro_num));
                        obj_sheet.SetValue(startrow + m, 1, listTypes[i].Title);
                      
                        for (int j = 0; j < tempptblist.Count; j++)
                        {
                            m++;
                            obj_sheet.RowCount = obj_sheet.RowCount + 1;
                            obj_sheet.SetValue(startrow + m, 0, j+1);
                            obj_sheet.SetValue(startrow + m, 1, tempptblist[j].Title);
                            obj_sheet.SetValue(startrow + m, 2, tempptblist[j].Length);
                            obj_sheet.SetValue(startrow + m, 3, tempptblist[j].Volumn);
                            obj_sheet.SetValue(startrow + m, 4, tempptblist[j].AreaName);
                            obj_sheet.SetValue(startrow + m, 5, tempptblist[j].Flag);
                            obj_sheet.SetValue(startrow + m, 6, Convert.ToInt32(tempptblist[j].BuildEd));
                           
                            
                        }
                        m++;
                        obj_sheet.RowCount = obj_sheet.RowCount + 1;
                        obj_sheet.AddSpanCell(startrow + m, 0, 1, 2);
                        obj_sheet.SetValue(startrow + m , 0, listTypes[i].Title + "С��");
                        TC.Sheet_WriteFormula_RowSum(obj_sheet, startrow + m  - tempptblist.Count, 2, tempptblist.Count, 1, startrow + m , 2, 2);
                    }
                }
                if (m==0)
                {
                    TC.WriteQuestion(title, "�޹滮��Ŀ����", "��ѯ��Ŀ�������Ƿ��п������޴��ڵ�ǰ�������", "");
                }
            }
            catch (Exception em)
            {
                
                throw;
            }
           
        }
    }
}
