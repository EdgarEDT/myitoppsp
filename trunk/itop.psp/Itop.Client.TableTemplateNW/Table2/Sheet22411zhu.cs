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
    class Sheet22411zhu
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
            //���10 ��6 ��
            rowcount = 10;
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
            obj_sheet.Columns[0].Width = 120;
            obj_sheet.Columns[1].Width = 60;
            obj_sheet.Columns[2].Width = 60;
            obj_sheet.Columns[3].Width = 60;
            obj_sheet.Columns[4].Width = 60;
            obj_sheet.Columns[5].Width = 60;
            //�趨����и߶�
            obj_sheet.Rows[0].Height = 20;
            obj_sheet.Rows[1].Height = 20;
            //д����������

            //2�б�������
            obj_sheet.SetValue(1, 1, "ȫ��");
           
            //д����������

            //1�б�������
           
            //�������
            Sheet_AddData(obj_sheet);

            //�趨�����
            TC.Sheet_GridandCenter(obj_sheet);

            //�������
            TC.Sheet_Locked(obj_sheet);
        }
        private void AddItems(FarPoint.Win.Spread.SheetView obj_sheet, string DianYa, int rowstart)
        {


            obj_sheet.SetValue(rowstart++, 0, DianYa + "ǧ�����վ����");
            obj_sheet.SetValue(rowstart++, 0, DianYa + "ǧ���������");
            obj_sheet.SetValue(rowstart++, 0, DianYa + "ǧ����·����");

           
        }

        //�˴�Ϊ��̬������ݷ���
        private void Sheet_AddData(FarPoint.Win.Spread.SheetView obj_sheet)
        {
            int[] TableYearsAry = TC.GetTableYears(this.GetType().Name);
            int startrow = 2;
            int itemlength = 1;
            string sqlwhere = " ProjectID='" + Tcommon.ProjectID + "'";
            IList<PS_Table_AreaWH> ptalist = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", sqlwhere);
            //�����趨����
            colcount = 2 + ptalist.Count  * itemlength;
            //��ѯ���վ��ѹ�ж��ٵȼ�����(����·�ȼ���ͬ)
            string tiaojian = " AreaID='" + Tcommon.ProjectID + "'  group by L1 order by L1 desc";
            //��¼��ѹ�ȼ�
            IList ptz = Services.BaseService.GetList("SelectPSP_Substation_InfoGroupL1", tiaojian);
            //�����趨����
            rowcount = 2 + ptz.Count * 4;
            TC.Sheet_RowCol_Title_Name(obj_sheet, rowcount, colcount, title, sheetname);
            string strgz = "����";
            string BDtiaojian = "";
            string XLtiaojian = "";
            string dianya;
            //д���з���
            for (int k= 0; k < ptalist.Count; k++)
            {
                obj_sheet.SetValue(1, 2 + k, ptalist[k].Title);
            }
            for (int i = 0; i < ptz.Count; i++)
            {
                dianya = ptz[i].ToString();
                //д�б���
                obj_sheet.SetValue(startrow + i * 4, 0, TC.CHNumberToChar(i + 1) + "��" + ptz[i] + "ǧ��");
                AddItems(obj_sheet, ptz[i].ToString(), startrow + 1 + i * 4);
                TC.Sheet_WriteFormula_ColSum_WritOne(obj_sheet, startrow + i * 4+1, 2, 3, ptalist.Count, 1);

                for (int j = 0; j < ptalist.Count; j++)
                {
                    BDtiaojian = " S2!='' and CAST(substring(S2,1,4) as int)<=" + TableYearsAry[0] + " and AreaID='" + Tcommon.ProjectID + "' and S4='" + strgz + "' and L1=" + dianya + " and AreaName='" + ptalist[j].Title + "'";
                    XLtiaojian = " year(cast(OperationYear as datetime))<=" + TableYearsAry[0] + " and  Type='05' and ProjectID='" + Tcommon.ProjectID + "' and LineType2='" + strgz + "'and RateVolt=" + dianya+" and AreaID='"+ ptalist[j].ID+"'";
                    //���վ̨��
                    int BDsum = 0;
                    if (Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", BDtiaojian) != null)
                    {
                        BDsum = (int)Services.BaseService.GetObject("SelectPSP_Substation_InfoCountall", BDtiaojian);
                    }
                    obj_sheet.SetValue(startrow + i * 4 + 1, 2 + j, BDsum);
                    //���������MVA��
                    double BDRLsum = 0;
                    if (Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", BDtiaojian) != null)
                    {
                        BDRLsum = (double)Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", BDtiaojian);
                    }
                    obj_sheet.SetValue(startrow + i * 4 + 2, 2 + j, BDRLsum);  
                    //��·����
                     double XLlength = 0;
                     if (Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", XLtiaojian) != null)
                    {
                        XLlength = (double)Services.BaseService.GetObject("SelectPSPDEV_SUM_Length", XLtiaojian);
                    }
                    obj_sheet.SetValue(startrow + i * 4 + 3, 2 + j, XLlength);
                }
            }
            
        }



    }
}