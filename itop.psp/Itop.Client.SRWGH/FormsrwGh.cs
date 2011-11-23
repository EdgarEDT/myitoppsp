using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Layouts;
using Itop.Domain.Table;
using Itop.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Stutistic;
using System.Reflection;
using System.Diagnostics;
using DevExpress.Utils;
//using Itop.Domain.RightManager;
using Itop.Client.Base;
using FarPoint.Win;
using Itop.Domain.Forecast;
using Itop.Domain.HistoryValue;
using Itop.Domain.Chen;
namespace Itop.Client.SRWGH
{
    public partial class FormsrwGh : FormBase
    {
        System.IO.MemoryStream si1 = new System.IO.MemoryStream();
        System.IO.MemoryStream si2 = new System.IO.MemoryStream();
        byte[] by1 = null;
        byte[] bts = null;
        int firstyear = 2000;
        int endyear = 2008;
        string uid1 = "";
        string type1 = "1";
        int type = 1;
        DataTable dataTable = new DataTable();
        private FarPoint.Win.Spread.CellHorizontalAlignment HAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        private FarPoint.Win.Spread.CellVerticalAlignment VAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
        FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
        FarPoint.Win.Spread.CellType.NumberCellType numberCellTypes1 = new FarPoint.Win.Spread.CellType.NumberCellType();
      
        FarPoint.Win.Spread.CellType.PercentCellType percentcelltypes = new FarPoint.Win.Spread.CellType.PercentCellType();
        FarPoint.Win.Spread.CellType.TextCellType texttype = new FarPoint.Win.Spread.CellType.TextCellType();

        //����һ���߿���
        LineBorder border = new LineBorder(Color.Black);
        public FarPoint.Win.Spread.FpSpread FPSpread
        {
            get { return fpSpread1; }

        }

        public FormsrwGh()
        {
            InitializeComponent();
           
        }

        private void FormsrwGh_Load(object sender, EventArgs e)
        {
            numberCellTypes1.DecimalPlaces = 3;
            //��ģ��
            Econ ed = new Econ();
            ed.UID = "SRWGUIHUA";
            bts = Itop.Client.Common.Services.BaseService.GetOneByKey<Econ>(ed).ExcelData;
           // initdata();
            //����һ������
            //for (int i = 0; i < 2;i++ )
            //{
            //    fpSpread1.Sheets[0].Columns.Add(2, 1);
            //    fpSpread1.Sheets[0].Columns[2+i].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
            //}
         
            //��ӵ�Ԫ��ʽ
           // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
            
            //���־���

        }
        private void SpreadRemoveEmptyCells(FarPoint.Win.Spread.FpSpread tempspread)
        {
            //����������ȥ�������ж�����к��У��գ�
            //�����޿յ�Ԫ��ģʽ
            FarPoint.Win.Spread.Model.INonEmptyCells nec;
            //����spread�ж��ٸ����
            int sheetscount = tempspread.Sheets.Count;
            //��������
            int rowcount = 0;
            //��������
            int colcount = 0;
            for (int m = 0; m < sheetscount; m++)
            {
                nec = (FarPoint.Win.Spread.Model.INonEmptyCells)tempspread.Sheets[m].Models.Data;
                //�����޿յ�Ԫ������
                colcount = nec.NonEmptyColumnCount;
                //�����޿յ�Ԫ������
                rowcount = nec.NonEmptyRowCount;
                tempspread.Sheets[m].RowCount = rowcount;
                tempspread.Sheets[m].ColumnCount = colcount;
            }
        }
        private void initdata()
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xls"))
            {
              WaitDialogForm  wait = new WaitDialogForm("", "���ڼ�������, ���Ժ�...");
                fpSpread1.Sheets.Clear();
                //fpSpread1.Open(System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xml");
                fpSpread1.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xls");
                SpreadRemoveEmptyCells(fpSpread1);
                wait.Close();

            }
            else
            {
                WaitDialogForm wait = new WaitDialogForm("", "���ڼ�������, ���Ժ�...");
                refreshdata();
                wait.Close();

            }
            //string uid = "Remark='" + ProjectUID + "'";
            //EconomyAnalysis ec = (EconomyAnalysis)Services.BaseService.GetObject("SelectEconomyAnalysisByvalue", uid);
           
            //if (ec != null)
            //{
            //    WaitDialogForm wait = null;
            //    try
            //    {
            //        wait = new WaitDialogForm("", "���ڼ�������, ���Ժ�...");
            //        System.IO.MemoryStream ms = new System.IO.MemoryStream(ec.Contents);
            //        by1 = ec.Contents;
            //        fpSpread1.Open(ms);

            //        try
            //        {
            //            // fpSpread1.Sheets[0].Cells[0, 0].Text = "����1 " + treeList1.FocusedNode["Title"].ToString() + "��������";
            //        }
            //        catch { }

            //        wait.Close();

            //    }
            //    catch { wait.Close(); }
            //}
            //else
            //{
            //    ec=new EconomyAnalysis();
            //    ec.Contents = bts;
            //    //obj.ParentID = uid;
            //    ec.CreateDate = DateTime.Now;
            //    ec.Remark = ProjectUID;
            //    Services.BaseService.Create<EconomyAnalysis>(ec);
            //    System.IO.MemoryStream ms = new System.IO.MemoryStream(ec.Contents);
            //    fpSpread1.Open(ms);
            //}
        }
        //���и��¾���ָ��Ķ�ȡ
        private void rejjzb()
        {
            //��¼��ݵ�ʱ���
            List<string> swjd = new List<string>();
            List<string> sywjd = new List<string>();
            //����ʱ��� ���ж�ʱ��εķ�Χ Ȼ�������
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "������չʵ��";
            py.Col5 = ProjectUID;

            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {
                firstyear = 2000;
                endyear = 2008;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }
            //�ж�ʱ��� ��������
            if (endyear<=2000)
            {
                MessageBox.Show("����ʷ��չʵ���о���ָ���ʱ����趨����");
                return;
            }
            else if (firstyear<=2000&&endyear<=2010)
            {
                swjd.Clear();
                sywjd.Clear();
                int colnum = 0;
                if (endyear<=2005)
                {
                    for (int i = 0; i <= endyear - 2000; i++)
                    {
                        //����һ������
                        fpSpread1.Sheets[0].Columns.Add(2, 1);
                        fpSpread1.Sheets[0].SetValue(2, 2, endyear - i + "��");
                        //��ӵ�Ԫ��ʽ
                       
                        fpSpread1.Sheets[0].Columns[2+i].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                        //
                        colnum++;
                        string year = "y" +(2000+i).ToString();
                        swjd.Add(year);
                    }
                    fpSpread1.Sheets[0].Columns.Add(2 + colnum, 1);
                    fpSpread1.Sheets[0].SetValue(2, 2 + colnum, "ʮ����������");
                    //��ӵ�Ԫ��ʽ
                    //FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                    fpSpread1.Sheets[0].Columns[2 + colnum].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                }
                else if (endyear>2005)
                {
                    for (int i = 0; i <= 5; i++)
                    {
                        //����һ������
                        fpSpread1.Sheets[0].Columns.Add(2, 1);
                        fpSpread1.Sheets[0].SetValue(2,2, 2005 - i + "��");
                        //��ӵ�Ԫ��ʽ
                       // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                        fpSpread1.Sheets[0].Columns[2+i].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                        //
                        string year = "y" +(2000+ i).ToString();
                        swjd.Add(year);
                        colnum++;
                    }
                    fpSpread1.Sheets[0].Columns.Add(2+ colnum, 1);
                    fpSpread1.Sheets[0].SetValue(2, 2+ colnum, "ʮ����������");
                    SetSheetViewColumnsWhith(fpSpread1.Sheets[0], 2 + colnum, "ʮ����������");
                    //��ӵ�Ԫ��ʽ
                   // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                    fpSpread1.Sheets[0].Columns[2+colnum].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                    int j = 0;
                    for(int i=0;i<endyear-2005;i++)
                    {
                        //����һ������
                        fpSpread1.Sheets[0].Columns.Add(9, 1);
                        fpSpread1.Sheets[0].SetValue(2, 9, endyear - i + "��");
                        //��ӵ�Ԫ��ʽ
                       // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                        fpSpread1.Sheets[0].Columns[9+i].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                        string year = "y" + (2006 + i).ToString();
                        sywjd.Add(year);
                        //
                        j++;
                    }
                    //fpSpread1.Sheets[0].Columns.Add(9 + j, 1);
                    fpSpread1.Sheets[0].SetValue(2, 9+ j, "ʮһ��ǰ"+j+"��������");
                    SetSheetViewColumnsWhith(fpSpread1.Sheets[0], 9 + j, "ʮһ��ǰ" + j + "��������");
                    //��ӵ�Ԫ��ʽ
                   // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                    fpSpread1.Sheets[0].Columns[9+j].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                }
                               
            }
           //�����ʽ
            fpSpread1.Sheets[0].Cells[0, 0].ColumnSpan = 4 + swjd.Count + sywjd.Count;
            fpSpread1.Sheets[0].Cells[1, 0].ColumnSpan = 4 + swjd.Count + sywjd.Count;
            Sheet_GridandColor(fpSpread1.Sheets[0], 15, 4 + swjd.Count + sywjd.Count);
            //List<Ps_History> jjzblist = new List<Ps_History>();
            //Ps_History psp_Type = new Ps_History();
            //psp_Type.Forecast = type;
            //psp_Type.Col4 = ProjectUID;
            //IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            //����ѭ���ҵ�����Ҫ�ı���
            //���ҳ�����ָ��
            Dictionary<string, Ps_History> resualt = new Dictionary<string, Ps_History>();
            string con = "Title='ȫ����GDP(��Ԫ)' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            Ps_History GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add("GDP(2000��ɱȼ�)", GDP1);
            if (GDP1!=null)
            {
                con = "Title in('һ��','����','����') AND  ParentID='" + GDP1.ID + "' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
                IList GDPLIST = Services.BaseService.GetList("SelectPs_HistoryByCondition", con);
                foreach (Ps_History ps in GDPLIST)
                {
                    resualt.Add(ps.Title, ps);
                }
            }
           else
            {
                Ps_History ph = new Ps_History();
                resualt.Add("һ��", null);
                resualt.Add("����", null);
                resualt.Add("����", null);
            }
            con = "Title='�������ܶ�' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add("�������ܶ�", GDP1);
            if (GDP1!=null)
            {
                con = "Title in('�����ܶ�','�����ܶ�') AND  ParentID='" + GDP1.ID + "' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
              IList  GDPLIST = Services.BaseService.GetList("SelectPs_HistoryByCondition", con);
                foreach (Ps_History ps in GDPLIST)
                {
                    resualt.Add(ps.Title, ps);
                }
            }
            else
            {
                 Ps_History ph=new Ps_History();
                 resualt.Add("�����ܶ�", null);
                 resualt.Add("�����ܶ�", null);
            }
           
            con = "Title='ȫ���̶��ʲ�Ͷ��' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add("ȫ���̶��ʲ�Ͷ��", GDP1);

            con = "Title='�������Ʒ�����ܶ�' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add("�������Ʒ�����ܶ�", GDP1);

            con = "Title='�������Ѽ۸�ָ��' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add("�������Ѽ۸�ָ��", GDP1);

            con = "Title='�����˿�' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add("�����˿�", GDP1);
            con = "Title='�����������' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add("�����������", GDP1);
            //�趨��Ԫ�����ʽ����ӹ�ʽ��ʽ

            for (int j = 0; j < 12; j++)
            {
                for (int i = 0; i <= swjd.Count; i++)
                {
                    fpSpread1.Sheets[0].Cells[3 + j, 2 + i].CellType = numberCellTypes1;
                }
                fpSpread1.Sheets[0].Cells[3 + j, 2 + swjd.Count].CellType = percentcelltypes;
                //fpSpread1.Sheets[0].Cells[3 + j, 2 + swjd.Count].Formula = "POWER(R" + (3 + j).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (3 + j).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
            }
            if (sywjd.Count > 0)
            {
                for (int j = 0; j < 12; j++)
                {
                    for (int i = 0; i < sywjd.Count; i++)
                    {
                        fpSpread1.Sheets[0].Cells[3 + j, 9 + i].CellType = numberCellTypes1;
                    }
                    fpSpread1.Sheets[0].Cells[3 + j, 9 + sywjd.Count].CellType = percentcelltypes;
                    //fpSpread1.Sheets[0].Cells[3 + j, 9 + sywjd.Count].Formula = "POWER(R" + (3 + j).ToString() + "C" + (9 + sywjd.Count - 1).ToString() + "/R" + (3 + j).ToString() + "C9," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                }
            }

            List<string> hangzhi=new List<string>();
            hangzhi.Add("GDP(2000��ɱȼ�)");
            hangzhi.Add("һ��");
            hangzhi.Add("����");
            hangzhi.Add("����");hangzhi.Add("�������ܶ�");hangzhi.Add("�����ܶ�");hangzhi.Add("�����ܶ�");hangzhi.Add("ȫ���̶��ʲ�Ͷ��");hangzhi.Add("�������Ʒ�����ܶ�");hangzhi.Add("�������Ѽ۸�ָ��");
            hangzhi.Add("�����˿�");hangzhi.Add("�����������");
            //��������������
            for (int i = 0; i <swjd.Count; i++)
            {
                fpSpread1.Sheets[0].SetValue(3, 2 + i, Gethistroyvalue<Ps_History>(resualt["GDP(2000��ɱȼ�)"], swjd[i]));
                if (resualt["һ��"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(4, 2 + i, Gethistroyvalue<Ps_History>(resualt["һ��"], swjd[i]));
                }
                if (resualt["����"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(5, 2 + i, Gethistroyvalue<Ps_History>(resualt["����"], swjd[i]));
                }
                if (resualt["����"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(6, 2 + i, Gethistroyvalue<Ps_History>(resualt["����"], swjd[i]));
                }
                if (resualt["�������ܶ�"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(7, 2 + i, Gethistroyvalue<Ps_History>(resualt["�������ܶ�"], swjd[i]));
                }
                if (resualt["�����ܶ�"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(8, 2 + i, Gethistroyvalue<Ps_History>(resualt["�����ܶ�"], swjd[i]));
                }
                if (resualt["�����ܶ�"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(9, 2 + i, Gethistroyvalue<Ps_History>(resualt["�����ܶ�"], swjd[i]));
                }
                if (resualt["ȫ���̶��ʲ�Ͷ��"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(10, 2 + i, Gethistroyvalue<Ps_History>(resualt["ȫ���̶��ʲ�Ͷ��"], swjd[i]));
                }
                if (resualt["�������Ʒ�����ܶ�"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(11, 2 + i, Gethistroyvalue<Ps_History>(resualt["�������Ʒ�����ܶ�"], swjd[i]));
                }
                if (resualt["�������Ѽ۸�ָ��"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(12, 2 + i, Gethistroyvalue<Ps_History>(resualt["�������Ѽ۸�ָ��"], swjd[i]));
                }
                if (resualt["�����˿�"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(13, 2 + i, Gethistroyvalue<Ps_History>(resualt["�����˿�"], swjd[i]));
                }
                if (resualt["�����������"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(14, 2 + i, Gethistroyvalue<Ps_History>(resualt["�����������"], swjd[i]));
                }
            }
            for (int i = 0; i < sywjd.Count; i++)
            {
                fpSpread1.Sheets[0].SetValue(3, 9 + i, Gethistroyvalue<Ps_History>(resualt["GDP(2000��ɱȼ�)"], sywjd[i]));
                if (resualt["һ��"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(4, 9 + i, Gethistroyvalue<Ps_History>(resualt["һ��"], sywjd[i]));
                }
                if (resualt["����"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(5, 9 + i, Gethistroyvalue<Ps_History>(resualt["����"], sywjd[i]));
                }
                if (resualt["����"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(6, 9 + i, Gethistroyvalue<Ps_History>(resualt["����"], sywjd[i]));
                }
                if (resualt["�������ܶ�"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(7, 9 + i, Gethistroyvalue<Ps_History>(resualt["�������ܶ�"], sywjd[i]));
                }
                if (resualt["�����ܶ�"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(8, 9 + i, Gethistroyvalue<Ps_History>(resualt["�����ܶ�"], sywjd[i]));
                }
                if (resualt["�����ܶ�"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(9, 9 + i, Gethistroyvalue<Ps_History>(resualt["�����ܶ�"], sywjd[i]));
                }
                if (resualt["ȫ���̶��ʲ�Ͷ��"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(10, 9 + i, Gethistroyvalue<Ps_History>(resualt["ȫ���̶��ʲ�Ͷ��"], sywjd[i]));
                }
                if (resualt["�������Ʒ�����ܶ�"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(11, 9 + i, Gethistroyvalue<Ps_History>(resualt["�������Ʒ�����ܶ�"], sywjd[i]));
                }
                if (resualt["�������Ѽ۸�ָ��"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(12, 9 + i, Gethistroyvalue<Ps_History>(resualt["�������Ѽ۸�ָ��"], sywjd[i]));
                }
                if (resualt["�����˿�"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(13, 9 + i, Gethistroyvalue<Ps_History>(resualt["�����˿�"], sywjd[i]));
                }
                if (resualt["�����������"] != null)
                {
                    fpSpread1.Sheets[0].SetValue(14, 9 + i, Gethistroyvalue<Ps_History>(resualt["�����������"], sywjd[i]));
                }
            }
            //�������ͳ��
            for (int j = 0; j < 12; j++)
            {
               if (resualt[hangzhi[j]]!=null)
               {
                   fpSpread1.Sheets[0].Cells[3 + j, 2 + swjd.Count].Formula = "POWER(R" + (3 + j + 1).ToString() + "C" + (1 + swjd.Count + 1).ToString() + "/R" + (3 + j + 1).ToString() + "C3," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
               }
                
            }
            if (sywjd.Count > 0)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (resualt[hangzhi[j]]!=null)
                    {
                        fpSpread1.Sheets[0].Cells[3 + j, 9 + sywjd.Count].Formula = "POWER(R" + (3 + j + 1).ToString() + "C" + (9 + sywjd.Count).ToString() + "/R" + (3 + j + 1).ToString() + "C8," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                    }
                    
                    
                }
            }
        }
        //�����������
        private void resbdsb()
        {
#region ���ݵ�ѹ�ȼ���ȡ���� ��·�ͱ��վ���� �������������
            IList<Substation_Info> list1000s = new List<Substation_Info>();
            IList<PSPDEV> list1000l = new List<PSPDEV>();
            IList<Substation_Info> list750s = new List<Substation_Info>();
            IList<PSPDEV> list750l = new List<PSPDEV>();
            IList<Substation_Info> list500s = new List<Substation_Info>();
            IList<PSPDEV> list500l = new List<PSPDEV>();
            IList<Substation_Info> list330s = new List<Substation_Info>();
            IList<PSPDEV> list330l = new List<PSPDEV>();
            IList<Substation_Info> list220s = new List<Substation_Info>();
            IList<PSPDEV> list220l = new List<PSPDEV>();
            IList<Substation_Info> list110s = new List<Substation_Info>();
            IList<PSPDEV> list110l = new List<PSPDEV>();
            //��������ͳ��
            IList<Substation_Info> listcity66s = new List<Substation_Info>();
           // IList<Substation_Info> listcountry66s = new List<Substation_Info>();
            IList<PSPDEV> listcity66l = new List<PSPDEV>();
            //IList<PSPDEV> listcountry66l = new List<PSPDEV>();

            IList<Substation_Info> listcity35s = new List<Substation_Info>();
            //IList<Substation_Info> listcountry35s = new List<Substation_Info>();
            IList<PSPDEV> listcity35l = new List<PSPDEV>();
            //IList<PSPDEV> listcountry35l = new List<PSPDEV>();

            IList<Substation_Info> listcity10s = new List<Substation_Info>();
            //IList<Substation_Info> listcountry10s = new List<Substation_Info>();
            IList<PSPDEV> listcity10l = new List<PSPDEV>();
           // IList<PSPDEV> listcountry10l = new List<PSPDEV>();

            IList<Substation_Info> listcity6s = new List<Substation_Info>();
            //IList<Substation_Info> listcountry6s = new List<Substation_Info>();
            IList<PSPDEV> listcity6l = new List<PSPDEV>();
            //IList<PSPDEV> listcountry6l = new List<PSPDEV>();

            IList<Substation_Info> listcity3s = new List<Substation_Info>();
            //IList<Substation_Info> listcountry3s = new List<Substation_Info>();
            IList<PSPDEV> listcity3l = new List<PSPDEV>();
            //IList<PSPDEV> listcountry3l = new List<PSPDEV>();

            string con = "AreaID='" + ProjectUID+ "'and L1=1000 order by L2,AreaName,CreateDate";
            list1000s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=750 order by L2,AreaName,CreateDate";
            list750s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=500 order by L2,AreaName,CreateDate";
            list500s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=330 order by L2,AreaName,CreateDate";
            list330s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=220 order by L2,AreaName,CreateDate";
            list220s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=110 order by L2,AreaName,CreateDate";
            list110s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
           
            con = "AreaID='" + ProjectUID + "'and L1=66 and DQ='��Ͻ������'order by L2,AreaName,CreateDate";
            listcity66s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //con = "AreaID='" + ProjectUID + "'and L1=66 and DQ='ũ��'order by L2,AreaName,CreateDate";
            //listcountry66s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);

            con = "AreaID='" + ProjectUID + "'and DQ='��Ͻ������'and L1=35 order by L2,AreaName,CreateDate";
            listcity35s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //con = "AreaID='" + ProjectUID + "'and DQ='ũ��'and L1=35 order by L2,AreaName,CreateDate";
           // listcountry35s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);

            con = "AreaID='" + ProjectUID + "'and DQ='��Ͻ������'and L1=10 order by L2,AreaName,CreateDate";
            listcity10s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //con = "AreaID='" + ProjectUID + "'and DQ='ũ��'and L1=10 order by L2,AreaName,CreateDate";
            //listcountry10s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);

            con = "AreaID='" + ProjectUID + "'and DQ='��Ͻ������' and L1=6 order by L2,AreaName,CreateDate";
            listcity6s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //con = "AreaID='" + ProjectUID + "'and DQ='ũ��' and L1=6 order by L2,AreaName,CreateDate";
            //listcountry6s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);

            con = "AreaID='" + ProjectUID + "'and DQ='��Ͻ������'and L1=3 order by L2,AreaName,CreateDate";
            listcity3s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //con = "AreaID='" + ProjectUID + "'and DQ='ũ��'and L1=3 order by L2,AreaName,CreateDate";
            //listcountry3s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //��·��Ϣ  
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='1000'  ORDER BY LineLength";
            list1000l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition",con);
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='750'  ORDER BY LineLength";
            list750l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='500'  ORDER BY LineLength";
            list500l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='330'  ORDER BY LineLength";
            list330l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='220'  ORDER BY LineLength";
            list220l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='110'  ORDER BY LineLength";
            list110l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='66'AND DQ='��Ͻ������'ORDER BY LineLength";
            listcity66l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='66'AND DQ='ũ��'ORDER BY LineLength";
            //listcountry66l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='35' AND DQ='��Ͻ������' ORDER BY LineLength";
            listcity35l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='35' AND DQ='ũ��' ORDER BY LineLength";
            //listcountry35l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='10'AND DQ='��Ͻ������'  ORDER BY LineLength";
            listcity10l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='10'AND DQ='ũ��'  ORDER BY LineLength";
           // listcountry10l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='6'AND DQ='��Ͻ������'  ORDER BY LineLength";
            listcity6l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='6'AND DQ='ũ��'  ORDER BY LineLength";
            //listcountry6l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='3'AND DQ='��Ͻ������'  ORDER BY LineLength";
            listcity3l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='3'AND DQ='ũ��'  ORDER BY LineLength";
           // listcountry3l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            //�Ȼ�ȡ�������ơ����������ũ������������·�ͱ��վ��������������н���ͳ��
��           Dictionary<string,IList<PSPDEV>> listcountry66l=new Dictionary<string,IList<PSPDEV>>();
             Dictionary<string,IList<Substation_Info>>listcountry66s=new Dictionary<string,IList<Substation_Info>>();

             Dictionary<string, IList<PSPDEV>> listcountry35l = new Dictionary<string, IList<PSPDEV>>();
             Dictionary<string, IList<Substation_Info>> listcountry35s = new Dictionary<string, IList<Substation_Info>>();

             Dictionary<string, IList<PSPDEV>> listcountry10l = new Dictionary<string, IList<PSPDEV>>();
             Dictionary<string, IList<Substation_Info>> listcountry10s = new Dictionary<string, IList<Substation_Info>>();

             Dictionary<string, IList<PSPDEV>> listcountry6l = new Dictionary<string, IList<PSPDEV>>();
             Dictionary<string, IList<Substation_Info>> listcountry6s = new Dictionary<string, IList<Substation_Info>>();

             Dictionary<string, IList<PSPDEV>> listcountry3l = new Dictionary<string, IList<PSPDEV>>();
             Dictionary<string, IList<Substation_Info>> listcountry3s = new Dictionary<string, IList<Substation_Info>>();

            bool flag66l=false,flag66s=false,flag35l=false,flag35s=false,flag10l=false,flag10s=false,flag6l=false,flag6s=false,flag3l=false,flag3s=false;
            string conn = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list =Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            
            foreach (PS_Table_AreaWH area in list)
            {
                con = "where ProjectID='" + ProjectUID + "'AND AreaID='" + area.ID + "'AND Type = '5'AND RateVolt='66'AND DQ!='��Ͻ������'  ORDER BY LineLength";
                IList<PSPDEV> list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                if (list1.Count!=0)
                {
                    flag66l=true;
                    listcountry66l.Add(area.Title, list1);
                }

                con = "AreaID='" + ProjectUID + "'and AreaName='" + area.Title + "'and DQ!='��Ͻ������' and L1=66 order by L2,AreaName,CreateDate";
                IList<Substation_Info>  list2 = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
                if (list2.Count!=0)
                {
                    flag66s=true;
                    listcountry66s.Add(area.Title, list2);

                }

                con = "where ProjectID='" + ProjectUID + "'AND AreaID='" + area.ID + "'AND Type = '5'AND RateVolt='35'AND DQ!='��Ͻ������'  ORDER BY LineLength";
               list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
               if (list1.Count != 0)
                {
                    flag35l=true;
                    listcountry35l.Add(area.Title, list1);
                }

                con = "AreaID='" + ProjectUID + "'and AreaName='" + area.Title + "'and DQ!='��Ͻ������' and L1=35 order by L2,AreaName,CreateDate";
                list2 = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
                if (list2.Count != 0)
                {
                    flag35s=true;
                    listcountry35s.Add(area.Title, list2);
                }


                con = "where ProjectID='" + ProjectUID + "'AND AreaID='" + area.ID + "'AND Type = '5'AND RateVolt='10'AND DQ!='��Ͻ������'  ORDER BY LineLength";
                list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    flag10l=true;
                    listcountry10l.Add(area.Title, list1);
                }

                con = "AreaID='" + ProjectUID + "'and AreaName='" + area.Title + "'and DQ!='��Ͻ������' and L1=10 order by L2,AreaName,CreateDate";
                list2 = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
                if (list2.Count != 0)
                {
                    flag10s=true;
                    listcountry10s.Add(area.Title, list2);
                }


                con = "where ProjectID='" + ProjectUID + "'AND AreaID='" + area.ID + "'AND Type = '5'AND RateVolt='6'AND DQ!='��Ͻ������' ORDER BY LineLength";
                list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    flag6l=true;
                     listcountry6l.Add(area.Title, list1);
                }

                con = "AreaID='" + ProjectUID + "'and AreaName='" + area.Title + "'and DQ!='��Ͻ������' and L1=6 order by L2,AreaName,CreateDate";
                list2 = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
                if (list2.Count != 0)
                {
                    flag6s=true;
                    listcountry6s.Add(area.Title, list2);
                }


                con = "where ProjectID='" + ProjectUID + "'AND AreaID='" + area.ID + "'AND Type = '5'AND RateVolt='3'AND DQ!='��Ͻ������' ORDER BY LineLength";
                list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    flag3l=true;
                     listcountry3l.Add(area.Title, list1);
                }

                con = "AreaID='" + ProjectUID + "'and AreaName='" + area.Title + "'and DQ!='��Ͻ������' and L1=3 order by L2,AreaName,CreateDate";
                list2= Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
                if (list2.Count != 0)
                {
                    flag3s=true;
                     listcountry3s.Add(area.Title, list2);
                }
               
               // repositoryItemComboBox3.Items.Add(area.Title);
            }

#endregion
#region ������������к���ӵ�Ԫ��ʽ
            List<string> title=new List<string>();
            int list1000i = 0;
            //1000kv����
            if (list1000s.Count!=0||list1000l.Count!=0)
            {
                list1000i = 1;
                fpSpread1.Sheets[1].Rows.Add(3, 1);
                fpSpread1.Sheets[1].SetValue(3,0, "1000KV����");
                fpSpread1.Sheets[1].Rows[3].Font = new Font("����",9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list1000l.Count; i++)
                {
                    fpSpread1.Sheets[1].Rows.Add(4, 1);
                    fpSpread1.Sheets[1].SetValue(4, 0, list1000l[list1000l.Count-1-i].Name);
                    fpSpread1.Sheets[1].SetValue(4, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4, 3, list1000l[list1000l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4,4, list1000l[list1000l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4, 4].CellType = numberCellTypes1;
                    list1000i++;
                }
                fpSpread1.Sheets[1].Cells[3, 4].Formula = "SUM(R5C5:R" + (5 + list1000l.Count - 1).ToString() + "C5)";
                for (int i = 0; i < list1000s.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4+list1000l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000l.Count, 0, list1000s[list1000s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000l.Count, 1, list1000s[list1000s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000l.Count, 2, list1000s[list1000s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000l.Count, 2].CellType = numberCellTypes1;
                    list1000i++;
                }
                fpSpread1.Sheets[1].Cells[3, 1].Formula = "SUM(R"+(5+list1000l.Count).ToString()+"C2:R" + (5 + list1000l.Count+list1000s.Count-1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3, 2].Formula = "SUM(R" + (5 + list1000l.Count).ToString() + "C3:R" + (5 + list1000l.Count + list1000s.Count - 1).ToString() + "C3)";
            }
            //750KV����
            int list750i = 0;
            if (list750s.Count!=0||list750l.Count!=0)
            {
                list750i = 1;
                fpSpread1.Sheets[1].Rows.Add(3+list1000i, 1);
                fpSpread1.Sheets[1].SetValue(3+list1000i, 0, "750KV����");
                fpSpread1.Sheets[1].Rows[3 + list1000i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list750l.Count; i++)
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i, 0, list750l[list750l.Count - 1 - i].Name);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i, 3, list750l[list750l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i, 4, list750l[list750l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4 + list1000i, 4].CellType = numberCellTypes1;
                    list750i++;
                }
                fpSpread1.Sheets[1].Cells[3+list1000i, 4].Formula = "SUM(R"+(5+list1000i).ToString()+"C5:R" + (5+list1000i + list750l.Count - 1).ToString() + "C5)";
                for (int i = 0; i < list750s.Count; i++)
                {
                    fpSpread1.Sheets[1].Rows.Add(4 +list1000i+ list750l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750l.Count, 0, list750s[list750s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750l.Count, 1, list750s[list750s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750l.Count, 2, list750s[list750s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750l.Count, 2].CellType = numberCellTypes1;
                    list750i++;
                }
                fpSpread1.Sheets[1].Cells[3+list1000i, 1].Formula = "SUM(R" + (5 +list1000i+ list750l.Count).ToString() + "C2:R" + (5+list1000i + list750l.Count + list750s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3+list1000i, 2].Formula = "SUM(R" + (5+list1000i + list750l.Count).ToString() + "C3:R" + (5+list1000i + list750l.Count + list750s.Count - 1).ToString() + "C3)";
            }
            //500KV����
            int list500i = 0;
            if (list500l.Count!=0||list500s.Count!=0)
            {
                list500i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i+list750i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i+list750i, 0, "500KV����");
                fpSpread1.Sheets[1].Rows[3 + list1000i+list750i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i+list750i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list500l.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i+list750i, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i+list750i, 0, list500l[list500l.Count - 1 - i].Name);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i+list750i, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i+list750i, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i+list750i, 3, list500l[list500l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i, 4, list500l[list500l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i, 4].CellType = numberCellTypes1;
                    list500i++;  
                }
                for (int i = 0; i < list500s.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i+list750i + list500l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500l.Count, 0, list500s[list500s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500l.Count, 1, list500s[list500s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500l.Count, 2, list500s[list500s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500l.Count, 2].CellType = numberCellTypes1;
                    list500i++;
                }
                fpSpread1.Sheets[1].Cells[3 + list1000i+list750i, 1].Formula = "SUM(R" + (5 + list1000i+list750i + list500l.Count).ToString() + "C2:R" + (5 + list1000i +list750i+ list500l.Count + list500s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i+list750i, 2].Formula = "SUM(R" + (5 + list1000i+list750i + list500l.Count).ToString() + "C3:R" + (5 + list1000i+list750i + list500l.Count + list500s.Count - 1).ToString() + "C3)";
            }
            //330KV����
            int list330i = 0;
            if (list330l.Count!=0||list330s.Count!=0)
            {
                list330i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i+list500i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i+list500i, 0, "330KV����");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i+list500i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i+list500i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list330l.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i+list500i, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i+list500i, 0, list330l[list330l.Count - 1 - i].Name);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i+list500i, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i+list500i, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i, 3, list330l[list330l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i, 4, list330l[list330l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i+list500i, 4].CellType = numberCellTypes1;
                    list330i++;  
                }
                for (int i = 0; i < list330s.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i+list500i + list330l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i +list500i+ list330l.Count, 0, list330s[list330s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 1, list330s[list330s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 2, list330s[list330s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i + list330l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i + list330l.Count, 2].CellType = numberCellTypes1;
                    list330i++;
                }
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i+list500i, 1].Formula = "SUM(R" + (5 + list1000i + list750i+list500i + list330l.Count).ToString() + "C2:R" + (5 + list1000i + list750i +list500i+ list330l.Count + list330s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i+list500i, 2].Formula = "SUM(R" + (5 + list1000i + list750i+list500i + list330l.Count).ToString() + "C3:R" + (5 + list1000i + list750i+list500i + list330l.Count + list330s.Count - 1).ToString() + "C3)";
            }
            //220KV����
            int list220i = 0;
            if (list220l.Count!=0||list220s.Count!=0)
            {
                list220i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i+list330i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i, 0, "220KV����");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list220l.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i+list330i, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i, 0, list220l[list220l.Count - 1 - i].Name);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i, 3, list220l[list220l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i, 4, list220l[list220l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i+list330i, 4].CellType = numberCellTypes1;
                    list220i++;  
                }
                for (int i = 0; i < list220s.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i+list330i + list220l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i + list220l.Count, 0, list220s[list220s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i + list220l.Count, 1, list220s[list220s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i + list220l.Count, 2, list220s[list220s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i + list220l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i + list220l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i+list330i + list220l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i +list330i+ list220l.Count, 2].CellType = numberCellTypes1;
                    list220i++;
                }
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i+list500i+list330i, 1].Formula = "SUM(R" + (5 + list1000i + list750i+list500i+list330i + list220l.Count).ToString() + "C2:R" + (5 + list1000i + list750i +list500i+list330i+ list220l.Count + list220s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i+list500i+list330i, 2].Formula = "SUM(R" + (5 + list1000i + list750i+list500i +list330i+ list220l.Count).ToString() + "C3:R" + (5 + list1000i + list750i+list500i+list330i + list220l.Count + list220s.Count - 1).ToString() + "C3)";
                 
            }
            //110KV����
            int list110i = 0;
            if (list110l.Count!=0||list110s.Count!=0)
            {
                list110i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i+list220i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i+list220i, 0, "110KV����");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i+list220i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i+list220i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list110l.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i+list220i, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i, 0, list110l[list110l.Count - 1 - i].Name);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i, 3, list110l[list110l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i, 4, list110l[list110l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i + list330i+list220i, 4].CellType = numberCellTypes1;
                    list110i++;  
                }
                for (int i = 0; i < list110s.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i+list220i + list110l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i + list110l.Count, 0, list110s[list110s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i + list110l.Count, 1, list110s[list110s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count, 2, list110s[list110s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i + list110l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i + list330i+list220i + list110l.Count, 2].CellType = numberCellTypes1;
                    list110i++;
                }
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i+list220i, 1].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i+list220i + list110l.Count).ToString() + "C2:R" + (5 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count + list110s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i+list220i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count).ToString() + "C3:R" + (5 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count + list110s.Count - 1).ToString() + "C3)";
            }
#region �������ݵ���� �����������ݷ�Ϊ������ũ��
            //����66KV����
            int listcity66i = 0;
            if (listcity66s.Count!=0||listcity6l.Count!=0)
            {
                listcity66i = 8;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i+list110i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "����66KV����");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "���Ͽ���");
                fpSpread1.Sheets[1].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "������ѹ��");
                fpSpread1.Sheets[1].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "��ʽ���վ");
                fpSpread1.Sheets[1].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(7+ list1000i + list750i + list500i + list330i + list220i + list110i, 0, "������");
                fpSpread1.Sheets[1].Rows[7+ list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "������·");
                fpSpread1.Sheets[1].Rows[8+ list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "���У��ܿ���·");
                fpSpread1.Sheets[1].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "      ������·");
                fpSpread1.Sheets[1].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //��ֵͳ��
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i+list110i,1].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i +list110i).ToString() + "C2:R" + (7+ list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i,2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3:R" + (8 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i,4].Formula = "SUM(R" + (10+ list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4)";
                fpSpread1.Sheets[1].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4)";
                //�����ֵ
               double jknum = 0, dlnum = 0;
                for (int i = 0; i < listcity66l.Count;i++ )
                {
                    if (listcity66l[i].JXFS=="�ܿ���·")
                    {
                        jknum += listcity66l[i].LineLength;
                    }
                    if (listcity66l[i].JXFS == "������·")
                    {
                        dlnum += listcity66l[i].LineLength;
                    }
                }
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, jknum);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, dlnum);
            }
            //����35KV����
            int listcity35i = 0;
            if (listcity35l.Count!=0||listcity35s.Count!=0)
            {
                listcity35i = 8;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i+listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "����35KV����");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "���Ͽ���");
                fpSpread1.Sheets[1].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "������ѹ��");
                fpSpread1.Sheets[1].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "��ʽ���վ");
                fpSpread1.Sheets[1].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "������");
                fpSpread1.Sheets[1].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "������·");
                fpSpread1.Sheets[1].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "���У��ܿ���·");
                fpSpread1.Sheets[1].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "      ������·");
                fpSpread1.Sheets[1].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //��ֵͳ��
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C2:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3:R" + (8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C4)";
                fpSpread1.Sheets[1].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C4)";
                //�����ֵ
                double jknum = 0, dlnum = 0;
                for (int i = 0; i < listcity35l.Count; i++)
                {
                    if (listcity35l[i].JXFS == "�ܿ���·")
                    {
                        jknum += listcity35l[i].LineLength;
                    }
                    if (listcity35l[i].JXFS == "������·")
                    {
                        dlnum += listcity35l[i].LineLength;
                    }
                }
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, jknum);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, dlnum);
            }
            //����10KV����
            int listcity10i = 0;
            if (listcity10s.Count!=0||listcity10l.Count!=0)
            {
                listcity10i = 8;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "����10KV����");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "���Ͽ���");
                fpSpread1.Sheets[1].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "������ѹ��");
                fpSpread1.Sheets[1].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "��ʽ���վ");
                fpSpread1.Sheets[1].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "������");
                fpSpread1.Sheets[1].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "������·");
                fpSpread1.Sheets[1].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "���У��ܿ���·");
                fpSpread1.Sheets[1].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "      ������·");
                fpSpread1.Sheets[1].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //��ֵͳ��
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C2:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3:R" + (8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C4)";
                fpSpread1.Sheets[1].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C4)";
                //�����ֵ
                double jknum = 0, dlnum = 0;
                for (int i = 0; i < listcity10l.Count; i++)
                {
                    if (listcity10l[i].JXFS == "�ܿ���·")
                    {
                        jknum += listcity10l[i].LineLength;
                    }
                    if (listcity10l[i].JXFS == "������·")
                    {
                        dlnum += listcity10l[i].LineLength;
                    }
                }
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 3, jknum);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 3, dlnum);
            }
            //����6KV����
            int listcity6i = 0;
            if (listcity6l.Count!=0||listcity6s.Count!=0)
            {
                listcity6i = 8;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "����6KV����");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "���Ͽ���");
                fpSpread1.Sheets[1].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "������ѹ��");
                fpSpread1.Sheets[1].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "��ʽ���վ");
                fpSpread1.Sheets[1].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "������");
                fpSpread1.Sheets[1].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "������·");
                fpSpread1.Sheets[1].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "���У��ܿ���·");
                fpSpread1.Sheets[1].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "      ������·");
                fpSpread1.Sheets[1].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //��ֵͳ��
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C2:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3:R" + (8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C4)";
                fpSpread1.Sheets[1].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C4)";
                //�����ֵ
                double jknum = 0, dlnum = 0;
                for (int i = 0; i < listcity6l.Count; i++)
                {
                    if (listcity6l[i].JXFS == "�ܿ���·")
                    {
                        jknum += listcity6l[i].LineLength;
                    }
                    if (listcity6l[i].JXFS == "������·")
                    {
                        dlnum += listcity6l[i].LineLength;
                    }
                }
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 3, jknum);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 3, dlnum);
            }
            int listcity3i = 0;
            if (listcity3s.Count!=0||listcity3l.Count!=0)
            {
                listcity3i = 8;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "����3KV����");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "���Ͽ���");
                fpSpread1.Sheets[1].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "������ѹ��");
                fpSpread1.Sheets[1].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "��ʽ���վ");
                fpSpread1.Sheets[1].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "������");
                fpSpread1.Sheets[1].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "������·");
                fpSpread1.Sheets[1].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "���У��ܿ���·");
                fpSpread1.Sheets[1].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "      ������·");
                fpSpread1.Sheets[1].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //��ֵͳ��
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C2:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3:R" + (8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C4)";
                fpSpread1.Sheets[1].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C4)";
                //�����ֵ
                double jknum = 0, dlnum = 0;
                for (int i = 0; i < listcity3l.Count; i++)
                {
                    if (listcity3l[i].JXFS == "�ܿ���·")
                    {
                        jknum += listcity3l[i].LineLength;
                    }
                    if (listcity3l[i].JXFS == "������·")
                    {
                        dlnum += listcity3l[i].LineLength;
                    }
                }
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 3, jknum);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 3, dlnum);
            }

            //ũ��66KV���� �ڴ��ڼ�Ҫ�����غ�ͳ����·�ͱ���������� ����
            int citynum = list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i + listcity3i;
            int listcountry66i = 0;
            if (flag66l||flag66s)
            {
                listcountry66i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + citynum, 1);
                fpSpread1.Sheets[1].SetValue(3 + citynum, 0, "ũ��66KV����");
                fpSpread1.Sheets[1].Rows[3 + citynum].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                 foreach (PS_Table_AreaWH area in list)
                 {
                     if (listcountry66l.ContainsKey(area.Title) && listcountry66l.ContainsKey(area.Title))
                     {
                         fpSpread1.Sheets[1].Rows.Add(4 + citynum, 1);
                         fpSpread1.Sheets[1].SetValue(4 + citynum, 0, area.Title);
                         fpSpread1.Sheets[1].Rows[4 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                         fpSpread1.Sheets[1].Rows.Add(5 + citynum, 1);
                         fpSpread1.Sheets[1].SetValue(5 + citynum, 0, "���У���·");
                         fpSpread1.Sheets[1].Rows[5 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                         fpSpread1.Sheets[1].Rows.Add(6 + citynum, 1);
                         fpSpread1.Sheets[1].SetValue(6 + citynum, 0, "    �����");
                         fpSpread1.Sheets[1].Rows[6+ citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                         listcountry66i += 3;  //������¼

                         //ͳ�ƺ���ӽ��
                         double Zrlnum = 0; int bdts = 0; double lenth = 0;
                         foreach (Substation_Info si in listcountry66s[area.Title])
                         {
                             Zrlnum += si.L2;
                             bdts += Convert.ToInt32(si.L3);
                         }
                         foreach(PSPDEV ps in listcountry66l[area.Title])
                         {
                             lenth += ps.LineLength;

                         }
                         //��ֵͳ��
                         fpSpread1.Sheets[1].Cells[4 + citynum, 1].Formula = "R" + (7 + citynum).ToString() + "C2";
                         fpSpread1.Sheets[1].Cells[4 + citynum, 2].Formula = "R" + (7 + citynum).ToString() + "C3";
                         fpSpread1.Sheets[1].Cells[4 + citynum, 4].Formula = "R" + (6 + citynum).ToString() + "C5";

                         //��ֵ
                         fpSpread1.Sheets[1].SetValue(5 + citynum, 4, lenth);
                         fpSpread1.Sheets[1].SetValue(6 + citynum, 2, Zrlnum);
                         fpSpread1.Sheets[1].SetValue(6 + citynum, 3, bdts);
                     }
                 }

            }
            //ũ��35KV��������
            int listcountry35i = 0;
            if (flag35l||flag35s)
            {

                listcountry35i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + citynum+listcountry66i, 1);
                fpSpread1.Sheets[1].SetValue(3 + citynum + listcountry66i, 0, "ũ��35KV����");
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry35l.ContainsKey(area.Title) && listcountry35l.ContainsKey(area.Title))
                    {
                        fpSpread1.Sheets[1].Rows.Add(4 + citynum+listcountry66i, 1);
                        fpSpread1.Sheets[1].SetValue(4 + citynum + listcountry66i, 0, area.Title);
                        fpSpread1.Sheets[1].Rows[4 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(5 + citynum + listcountry66i, 1);
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i, 0, "���У���·");
                        fpSpread1.Sheets[1].Rows[5 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(6 + citynum + listcountry66i, 1);
                        fpSpread1.Sheets[1].SetValue(6+ citynum + listcountry66i, 0, "    �����");
                        fpSpread1.Sheets[1].Rows[6+ citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry35i += 3;  //������¼

                        //ͳ�ƺ���ӽ��
                        double Zrlnum = 0; int bdts = 0; double lenth = 0;
                        foreach (Substation_Info si in listcountry35s[area.Title])
                        {
                            Zrlnum += si.L2;
                            bdts += Convert.ToInt32(si.L3);
                        }
                        foreach (PSPDEV ps in listcountry35l[area.Title])
                        {
                            lenth += ps.LineLength;

                        }
                        //��ֵͳ��
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i, 1].Formula = "R" + (7 + citynum + listcountry66i).ToString() + "C2";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i, 2].Formula = "R" + (7 + citynum + listcountry66i).ToString() + "C3";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i, 4].Formula = "R" + (6 + citynum + listcountry66i).ToString() + "C5";

                        //��ֵ
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i, 4, lenth);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i, 2, Zrlnum);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i, 3, bdts);
                    }

                }
            }
            //ũ��10KV����
            int listcountry10i = 0;
            if (flag10l||flag10s)
            {
                listcountry10i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + citynum + listcountry66i+listcountry35i, 1);
                fpSpread1.Sheets[1].SetValue(3 + citynum + listcountry66i + listcountry35i, 0, "ũ��10KV����");
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry10l.ContainsKey(area.Title) && listcountry10l.ContainsKey(area.Title))
                    {
                        fpSpread1.Sheets[1].Rows.Add(4 + citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[1].SetValue(4 + citynum + listcountry66i + listcountry35i, 0, area.Title);
                        fpSpread1.Sheets[1].Rows[4 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(5 + citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i, 0, "���У���·");
                        fpSpread1.Sheets[1].Rows[5 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(6+ citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i, 0, "    �����");
                        fpSpread1.Sheets[1].Rows[6+ citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry10i += 3;  //������¼

                        //ͳ�ƺ���ӽ��
                        double Zrlnum = 0; int bdts = 0; double lenth = 0;
                        foreach (Substation_Info si in listcountry10s[area.Title])
                        {
                            Zrlnum += si.L2;
                            bdts +=Convert.ToInt32(si.L3);
                        }
                        foreach (PSPDEV ps in listcountry10l[area.Title])
                        {
                            lenth += ps.LineLength;

                        }
                        //��ֵͳ��
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i, 1].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i).ToString() + "C2";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i, 2].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i).ToString() + "C3";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i, 4].Formula = "R" + (6 + citynum + listcountry66i + listcountry35i).ToString() + "C5";

                        //��ֵ
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i + listcountry35i, 4, lenth);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i, 2, Zrlnum);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i, 3, bdts);
                    }

                }
            }
            //ũ��6KV����
            int listcountry6i = 0;
            if (flag6l||flag6s)
            {
                listcountry6i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + citynum + listcountry66i + listcountry35i+listcountry10i, 1);
                fpSpread1.Sheets[1].SetValue(3 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, "ũ��6KV����");
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry6l.ContainsKey(area.Title) && listcountry6l.ContainsKey(area.Title))
                    {
                        fpSpread1.Sheets[1].Rows.Add(4 + citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[1].SetValue(4 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, area.Title);
                        fpSpread1.Sheets[1].Rows[4 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(5 + citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i + listcountry10i, 0, "���У���·");
                        fpSpread1.Sheets[1].Rows[5 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(6+ citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, "    �����");
                        fpSpread1.Sheets[1].Rows[6 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry6i += 3;  //������¼

                        //ͳ�ƺ���ӽ��
                        double Zrlnum = 0; int bdts = 0; double lenth = 0;
                        foreach (Substation_Info si in listcountry6s[area.Title])
                        {
                            Zrlnum += si.L2;
                            bdts +=Convert.ToInt32(si.L3);
                        }
                        foreach (PSPDEV ps in listcountry6l[area.Title])
                        {
                            lenth += ps.LineLength;

                        }
                        //��ֵͳ��
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i, 1].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C2";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i, 2].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C3";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i, 4].Formula = "R" + (6 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C5";

                        //��ֵ
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i, 4, lenth);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 2, Zrlnum);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 3, bdts);
                    }

                }
            }
            //ũ��3KV����
            int listcountry3i = 0;
            if (flag3l||flag3s)
            {
                listcountry3i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + citynum + listcountry66i + listcountry35i + listcountry10i+listcountry6i, 1);
                fpSpread1.Sheets[1].SetValue(3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, "ũ��6KV����");
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry3l.ContainsKey(area.Title) && listcountry3l.ContainsKey(area.Title))
                    {
                        fpSpread1.Sheets[1].Rows.Add(4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[1].SetValue(4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, area.Title);
                        fpSpread1.Sheets[1].Rows[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i + listcountry10i + listcountry6i, 0, "���У���·");
                        fpSpread1.Sheets[1].Rows[5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(6+ citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, "    �����");
                        fpSpread1.Sheets[1].Rows[6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry3i += 3;  //������¼

                        //ͳ�ƺ���ӽ��
                        double Zrlnum = 0; int bdts = 0; double lenth = 0;
                        foreach (Substation_Info si in listcountry3s[area.Title])
                        {
                            Zrlnum += si.L2;
                            bdts += Convert.ToInt32(si.L3);
                        }
                        foreach (PSPDEV ps in listcountry3l[area.Title])
                        {
                            lenth += ps.LineLength;

                        }
                        //��ֵͳ��
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C2";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 2].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C3";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 4].Formula = "R" + (6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C5";

                        //��ֵ
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 4, lenth);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 2, Zrlnum);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 3, bdts);
                    }

                }
                //�趨��ʽ
               
            }
#endregion
#endregion
            Sheet_GridandColor(fpSpread1.Sheets[1], 3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i + listcountry3i, 6);
        }
        //�޹������������ñ�
        private void wgbchpz()
        {
            #region Ѱ�������ܱ��վ
            IList<Substation_Info> list1000s = new List<Substation_Info>();
            
            IList<Substation_Info> list750s = new List<Substation_Info>();
           
            IList<Substation_Info> list500s = new List<Substation_Info>();
           
            IList<Substation_Info> list330s = new List<Substation_Info>();
           
            IList<Substation_Info> list220s = new List<Substation_Info>();
           
            IList<Substation_Info> list110s = new List<Substation_Info>();
            string con = "AreaID='" + ProjectUID + "'and L1=1000 order by L2,AreaName,CreateDate";
            list1000s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=750 order by L2,AreaName,CreateDate";
            list750s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=500 order by L2,AreaName,CreateDate";
            list500s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=330 order by L2,AreaName,CreateDate";
            list330s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=220 order by L2,AreaName,CreateDate";
            list220s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=110 order by L2,AreaName,CreateDate";
            list110s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);

            #endregion
#region ��ʾ����
            int list1000i = 0;
            //1000kv����
            if (list1000s.Count != 0 )
            {
                list1000i = 1;
                fpSpread1.Sheets[2].Rows.Add(4, 1);
                fpSpread1.Sheets[2].SetValue(4, 0, "1000KV����");
                fpSpread1.Sheets[2].Rows[4].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[2].Rows[4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list1000s.Count; i++)
                {
                    fpSpread1.Sheets[2].Rows.Add(5, 1);
                    fpSpread1.Sheets[2].SetValue(5, 0, list1000s[list1000s.Count - 1 - i].Title);
                    list1000i++;
                }
            }
            //750KV����
            int list750i = 0;
         
            if (list750s.Count != 0)
            {
                list750i = 1;
                fpSpread1.Sheets[2].Rows.Add(4+list1000i, 1);
                fpSpread1.Sheets[2].SetValue(4+list1000i, 0, "750KV����");
                fpSpread1.Sheets[2].Rows[4+list1000i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[2].Rows[4+list1000i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list750s.Count; i++)
                {
                    fpSpread1.Sheets[2].Rows.Add(5+list1000i, 1);
                    fpSpread1.Sheets[2].SetValue(5+list1000i, 0, list750s[list750s.Count - 1 - i].Title);
                    list750i++;
                }
            }
            //500KV����
            int list500i = 0;

            if (list500s.Count != 0)
            {
                list500i = 1;
                fpSpread1.Sheets[2].Rows.Add(4 + list1000i+list750i, 1);
                fpSpread1.Sheets[2].SetValue(4 + list1000i + list750i, 0, "500KV����");
                fpSpread1.Sheets[2].Rows[4+ list1000i + list750i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[2].Rows[4+ list1000i + list750i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list500s.Count; i++)
                {
                    fpSpread1.Sheets[2].Rows.Add(5 + list1000i + list750i, 1);
                    fpSpread1.Sheets[2].SetValue(5 + list1000i + list750i, 0, list500s[list500s.Count - 1 - i].Title);
                    list500i++;
                }
            }
            //330KV����
             int list330i = 0;

            if (list330s.Count != 0)
            {
                list330i = 1;
                fpSpread1.Sheets[2].Rows.Add(4 + list1000i+list750i+list500i, 1);
                fpSpread1.Sheets[2].SetValue(4 + list1000i + list750i+list500i, 0, "330KV����");
                fpSpread1.Sheets[2].Rows[4+ list1000i + list750i+list500i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[2].Rows[4+ list1000i + list750i+list500i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list330s.Count; i++)
                {
                    fpSpread1.Sheets[2].Rows.Add(5 + list1000i + list750i+list500i, 1);
                    fpSpread1.Sheets[2].SetValue(5 + list1000i + list750i+list500i, 0, list330s[list330s.Count - 1 - i].Title);
                    list330i++;
                }
            }
            //220KV����
             int list220i = 0;

            if (list220s.Count != 0)
            {
                list220i = 1;
                fpSpread1.Sheets[2].Rows.Add(4 + list1000i+list750i+list500i+list330i, 1);
                fpSpread1.Sheets[2].SetValue(4 + list1000i + list750i+list500i+list330i, 0, "220KV����");
                fpSpread1.Sheets[2].Rows[4+ list1000i + list750i+list500i+list330i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[2].Rows[4+ list1000i + list750i+list500i+list330i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list220s.Count; i++)
                {
                    fpSpread1.Sheets[2].Rows.Add(5 + list1000i + list750i+list500i+list330i, 1);
                    fpSpread1.Sheets[2].SetValue(5 + list1000i + list750i+list500i+list330i, 0, list220s[list220s.Count - 1 - i].Title);
                    list220i++;
                }
            }
            //110KV����
            int list110i = 0;

            if (list110s.Count != 0)
            {
                list110i = 1;
                fpSpread1.Sheets[2].Rows.Add(4 + list1000i+list750i+list500i+list330i+list220i, 1);
                fpSpread1.Sheets[2].SetValue(4 + list1000i + list750i+list500i+list330i+list220i, 0, "110KV����");
                fpSpread1.Sheets[2].Rows[4+ list1000i + list750i+list500i+list330i+list220i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[2].Rows[4+ list1000i + list750i+list500i+list330i+list220i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list110s.Count; i++)
                {
                    fpSpread1.Sheets[2].Rows.Add(5 + list1000i + list750i+list500i+list330i+list220i, 1);
                    fpSpread1.Sheets[2].SetValue(5 + list1000i + list750i+list500i+list330i+list220i, 0, list110s[list110s.Count - 1 - i].Title);
                    list110i++;
                }
            }
#endregion
            //�趨��ʽ
            Sheet_GridandColor(fpSpread1.Sheets[2], 4 + list1000i+list500i+list330i+list220i+list110i,7 );
        }
        //���������ú͵�����չʵ��
        private void jjhdlfzsj()
        {
#region ������������
            //��¼��ݵ�ʱ���
            List<string> swjd = new List<string>();
            List<string> sywjd = new List<string>();
            //����ʱ��� ���ж�ʱ��εķ�Χ Ȼ�������
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "������չʵ��";
            py.Col5 = ProjectUID;

            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {
                firstyear = 2000;
                endyear = 2008;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Services.BaseService.Create<Ps_YearRange>(py);
            }
            //�ж�ʱ��� ��������
            if (endyear <= 2000)
            {
                MessageBox.Show("����ʷ��չʵ���о���ָ���ʱ����趨����");
                return;
            }
            else if (firstyear <= 2000 && endyear <= 2010)
            {
                swjd.Clear();
                sywjd.Clear();
                int colnum = 0;
                if (endyear <= 2005)
                {
                    for (int i = 0; i <= endyear - 2000; i++)
                    {
                        //����һ������
                        fpSpread1.Sheets[3].Columns.Add(1, 1);
                        fpSpread1.Sheets[3].SetValue(2, 1, endyear - i + "��");
                        //��ӵ�Ԫ��ʽ

                        fpSpread1.Sheets[3].Columns[1 + i].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                        //
                        colnum++;
                        string year = "y" + (2000 + i).ToString();
                        swjd.Add(year);
                    }
                    fpSpread1.Sheets[3].Columns.Add(1 + colnum, 1);
                    fpSpread1.Sheets[3].SetValue(2, 1 + colnum, "ʮ����������");
                    //��ӵ�Ԫ��ʽ
                    //FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                    fpSpread1.Sheets[0].Columns[1 + colnum].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                }
                else if (endyear > 2005)
                {
                    for (int i = 0; i <= 5; i++)
                    {
                        //����һ������
                        fpSpread1.Sheets[3].Columns.Add(1, 1);
                        fpSpread1.Sheets[3].SetValue(2, 1, 2005 - i + "��");
                        //��ӵ�Ԫ��ʽ
                        // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                        fpSpread1.Sheets[3].Columns[1 + i].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                        //
                        string year = "y" + (2000 + i).ToString();
                        swjd.Add(year);
                        colnum++;
                    }
                    fpSpread1.Sheets[3].Columns.Add(1+ colnum, 1);
                    fpSpread1.Sheets[3].SetValue(2, 1 + colnum, "ʮ����������");
                    SetSheetViewColumnsWhith(fpSpread1.Sheets[3], 1+ colnum, "ʮ����������");
                    //��ӵ�Ԫ��ʽ
                    // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                    fpSpread1.Sheets[3].Columns[1+ colnum].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                    int j = 0;
                    for (int i = 0; i < endyear - 2005; i++)
                    {
                        //����һ������
                        fpSpread1.Sheets[3].Columns.Add(8, 1);
                        fpSpread1.Sheets[3].SetValue(2, 8, endyear - i + "��");
                        //��ӵ�Ԫ��ʽ
                        // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                        fpSpread1.Sheets[3].Columns[8 + i].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                        string year = "y" + (2006 + i).ToString();
                        sywjd.Add(year);
                        //
                        j++;
                    }
                    //fpSpread1.Sheets[0].Columns.Add(9 + j, 1);
                    fpSpread1.Sheets[3].SetValue(2, 8 + j, "ʮһ��ǰ" + j + "��������");
                    SetSheetViewColumnsWhith(fpSpread1.Sheets[3], 8 + j, "ʮһ��ǰ" + j + "��������");
                    //��ӵ�Ԫ��ʽ
                    // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                    fpSpread1.Sheets[3].Columns[8 + j].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                }

            }
            //�趨��ʽ
            fpSpread1.Sheets[3].Cells[0, 0].ColumnSpan = 3 + swjd.Count + sywjd.Count;
            fpSpread1.Sheets[3].Cells[1, 0].ColumnSpan = 3 + swjd.Count + sywjd.Count;
            Sheet_GridandColor(fpSpread1.Sheets[3],23, 3 + swjd.Count + sywjd.Count);
#endregion
            #region ��������Ϣ�������
            //���ҳ�����ָ��
            Dictionary<string, Ps_History> resualt = new Dictionary<string, Ps_History>();
            string con = "Title='ȫ����GDP(��Ԫ)' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            Ps_History GDP1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add("GDP(�����)", GDP1);
            if (GDP1!=null)
            {
                con = "Title in('һ��','����','����') AND  ParentID='" + GDP1.ID + "' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
                IList GDPLIST = Services.BaseService.GetList("SelectPs_HistoryByCondition", con);
                foreach (Ps_History ps in GDPLIST)
                {
                    resualt.Add(ps.Title, ps);
                }
            }
            else
            {
                Ps_History ph = new Ps_History();
                resualt.Add("һ��", null);
                resualt.Add("����", null);
                resualt.Add("����", null);
            }
            con = "Title='��Ͻ������' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            Ps_History sxgd = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            con = "Title='��Ͻ������' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            Ps_History xxgd = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            IList sxgdlist = null, xxgdlist = null; Ps_History ydl = null; Ps_History yc = null; Ps_History ec = null; Ps_History sc = null; Ps_History jm = null; Ps_History gdl = null; Ps_History zggdfh = null;
            if (sxgd!=null)
            {
                con = "ParentID='" + sxgd.ID + "' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
                sxgdlist = Services.BaseService.GetList("SelectPs_HistoryByCondition", con);
                foreach (Ps_History ps in sxgdlist)
                {
                    if (ps.Title.Contains("�õ���"))
                    {
                        ydl = ps;
                        con = "ParentID='" + ps.ID + "' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
                        IList list2 = Services.BaseService.GetList("SelectPs_HistoryByCondition", con);
                        foreach (Ps_History ph in list2)
                        {
                            if (ph.Title.Contains("һ��"))
                            {
                                yc = ph;
                            }
                            if (ph.Title.Contains("����"))
                            {
                                ec = ph;
                            }
                            if (ph.Title.Contains("����"))
                            {
                                sc = ph;
                            }
                            if (ph.Title.Contains("����"))
                            {
                                jm=ph;
                            }
                        }
                    }
                    if (ps.Title.Contains("������"))
                    {
                        gdl = ps;
                    }
                    if (ps.Title.Contains("ȫ�����󸺺�"))
                    {
                        zggdfh = ps;
                    }
                }
            }
            if (xxgd!=null)
            {
                con = "ParentID='" + xxgd.ID + "' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
                xxgdlist = Services.BaseService.GetList("SelectPs_HistoryByCondition", con);
                foreach (Ps_History ps in xxgdlist)
                {
                    if (ps.Title.Contains("�õ���"))
                    {
                        if (ydl!=null)
                        {
                            for (int i = 0; i < swjd.Count;i++ )
                            {
                                addhistoyvalue<Ps_History>(ydl, ps, swjd[i]);  
                            }
                            for (int i = 0; i < sywjd.Count;i++ )
                            {
                                addhistoyvalue<Ps_History>(ydl, ps, sywjd[i]);
                            }
                            
                        }
                        else
                        {
                            ydl = ps;
                        }
                        resualt.Add("ȫ����õ���", ydl);
                        ydl = ps;
                        con = "ParentID='" + ps.ID + "' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
                        IList list2 = Services.BaseService.GetList("SelectPs_HistoryByCondition", con);
                        foreach (Ps_History ph in list2)
                        {
                            if (ph.Title.Contains("һ��"))
                            {
                                if (yc!=null)
                                {
                                    for (int i = 0; i < swjd.Count; i++)
                                    {
                                        addhistoyvalue<Ps_History>(yc, ph, swjd[i]);
                                    }
                                    for (int i = 0; i < sywjd.Count; i++)
                                    {
                                        addhistoyvalue<Ps_History>(yc, ph, sywjd[i]);
                                    }
                                }
                                else
                                yc = ph;
                            resualt.Add("ȫ����õ���һ��", yc);
                            }
                            if (ph.Title.Contains("����"))
                            {
                                if (ec != null)
                                {
                                    for (int i = 0; i < swjd.Count; i++)
                                    {
                                        addhistoyvalue<Ps_History>(ec, ph, swjd[i]);
                                    }
                                    for (int i = 0; i < sywjd.Count; i++)
                                    {
                                        addhistoyvalue<Ps_History>(ec, ph, sywjd[i]);
                                    }
                                }
                                else
                                ec = ph;
                            resualt.Add("ȫ����õ�������", ec);
                            }
                            if (ph.Title.Contains("����"))
                            {
                                if (sc != null)
                                {
                                    for (int i = 0; i < swjd.Count; i++)
                                    {
                                        addhistoyvalue<Ps_History>(sc, ph, swjd[i]);
                                    }
                                    for (int i = 0; i < sywjd.Count; i++)
                                    {
                                        addhistoyvalue<Ps_History>(sc, ph, sywjd[i]);
                                    }
                                }
                                else
                                sc = ph;
                            resualt.Add("ȫ����õ�������", sc);
                            }
                            if (ph.Title.Contains("����"))
                            {
                                if (jm!=null)
                                {
                                    for (int i = 0; i < swjd.Count; i++)
                                    {
                                        addhistoyvalue<Ps_History>(jm, ph, swjd[i]);
                                    }
                                    for (int i = 0; i < sywjd.Count; i++)
                                    {
                                        addhistoyvalue<Ps_History>(jm, ph, sywjd[i]);
                                    }
                                }
                                else
                                jm = ph;
                            resualt.Add("ȫ����õ�������", jm);
                            }
                        }
                    }
                    if (ps.Title.Contains("������"))
                    {
                        if (gdl!=null)
                        {
                            for (int i = 0; i < swjd.Count; i++)
                            {
                                addhistoyvalue<Ps_History>(gdl, ps, swjd[i]);
                            }
                            for (int i = 0; i < sywjd.Count; i++)
                            {
                                addhistoyvalue<Ps_History>(gdl, ps, sywjd[i]);
                            }
                        }
                        else
                        gdl = ps;
                    resualt.Add("������", gdl);
                    }
                    if (ps.Title.Contains("ȫ�����󸺺�"))
                    {
                        if (zggdfh!=null)
                        {
                            for (int i = 0; i < swjd.Count; i++)
                            {
                                addhistoyvalue<Ps_History>(zggdfh, ps, swjd[i]);
                            }
                            for (int i = 0; i < sywjd.Count; i++)
                            {
                                addhistoyvalue<Ps_History>(zggdfh, ps, sywjd[i]);
                            }
                        }
                        else
                        zggdfh = ps;
                    resualt.Add("��߹��縺��", ps);
                    }
                }
            }
            con = "Title='����С���鷢����' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            Ps_History qnxjz = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);          
            resualt.Add("����С���鷢����", qnxjz);
            con = "Title='����С��������' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            Ps_History qnxjrl = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);   
            if (qnxjrl!=null)
            {
                resualt.Add("����С��������", qnxjz);
                con = "Title in('ˮ��','���','�ȵ�') AND  ParentID='" + qnxjrl.ID + "' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
                IList GDPLIST = Services.BaseService.GetList("SelectPs_HistoryByCondition", con);
                foreach (Ps_History ps in GDPLIST)
                {
                    resualt.Add(ps.Title, ps);
                }
            }
            else
            {
                resualt.Add("ˮ��", null);
                resualt.Add("���", null);
                resualt.Add("�ȵ�", null);
            }
            con = "Title='Tmax' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            Ps_History tmax = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            resualt.Add("Tmax", tmax);
            con = "Title like'�����˿�%' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            Ps_History czpeople = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            con = "Title like'����˿�%' AND Col4='" + ProjectUID + "'AND Forecast='" + type + "'";
            Ps_History xcpeople = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
            if (czpeople!=null&&xcpeople!=null)
            {
                for (int i = 0; i < swjd.Count; i++)
                {
                    addhistoyvalue<Ps_History>(xcpeople, czpeople, swjd[i]);
                }
                for (int i = 0; i < sywjd.Count; i++)
                {
                    addhistoyvalue<Ps_History>(xcpeople,czpeople, sywjd[i]);
                }
            }
            resualt.Add("�˿�", xcpeople);
            #endregion
#region ��Ԫ��ʽ�������������
            //�趨��Ԫ�����ʽ����ӹ�ʽ��ʽ

            for (int j = 0; j < 20; j++)
            {
                for (int i = 0; i <= swjd.Count; i++)
                {
                    fpSpread1.Sheets[3].Cells[3 + j, 1 + i].CellType = numberCellTypes1;
                }
                fpSpread1.Sheets[3].Cells[3 + j, 1 + swjd.Count].CellType = percentcelltypes;
                //fpSpread1.Sheets[0].Cells[3 + j, 2 + swjd.Count].Formula = "POWER(R" + (3 + j).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (3 + j).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
            }
            if (sywjd.Count > 0)
            {
                for (int j = 0; j < 20; j++)
                {
                    for (int i = 0; i < sywjd.Count; i++)
                    {
                        fpSpread1.Sheets[3].Cells[3 + j, 8 + i].CellType = numberCellTypes1;
                    }
                    fpSpread1.Sheets[3].Cells[3 + j, 8 + sywjd.Count].CellType = percentcelltypes;
                    //fpSpread1.Sheets[0].Cells[3 + j, 9 + sywjd.Count].Formula = "POWER(R" + (3 + j).ToString() + "C" + (9 + sywjd.Count - 1).ToString() + "/R" + (3 + j).ToString() + "C9," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                }
            }
            //��������������
            List<string> hangzhi = new List<string>();
            hangzhi.Add("GDP(�����)"); hangzhi.Add("һ��"); hangzhi.Add("����"); hangzhi.Add("����"); hangzhi.Add("ȫ����õ���"); hangzhi.Add("ȫ����õ���һ��");
            hangzhi.Add("ȫ����õ�������"); hangzhi.Add("ȫ����õ�������"); hangzhi.Add("ȫ����õ�������"); hangzhi.Add("������");
            hangzhi.Add("��߹��縺��"); hangzhi.Add("����С���鷢����"); hangzhi.Add("Tmax"); hangzhi.Add("����С��������");
            hangzhi.Add("ˮ��"); hangzhi.Add("���"); hangzhi.Add("�ȵ�"); hangzhi.Add("�˿�");
            for (int i = 0; i < swjd.Count; i++)
            {
                fpSpread1.Sheets[3].SetValue(3, 1 + i, Gethistroyvalue<Ps_History>(resualt["GDP(�����)"], swjd[i]));
                if (resualt["һ��"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(4, 1 + i, Gethistroyvalue<Ps_History>(resualt["һ��"], swjd[i]));
                }
                if (resualt["����"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(5, 1 + i, Gethistroyvalue<Ps_History>(resualt["����"], swjd[i]));
                }
                if (resualt["����"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(6, 1 + i, Gethistroyvalue<Ps_History>(resualt["����"], swjd[i]) );
                }
                if (resualt.ContainsKey("ȫ����õ���"))
                { 
                    if (resualt["ȫ����õ���"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(7, 1 + i, Gethistroyvalue<Ps_History>(resualt["ȫ����õ���"], swjd[i]));
                }
                }
                
                if (resualt["ȫ����õ���һ��"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(8, 1 + i, Gethistroyvalue<Ps_History>(resualt["ȫ����õ���һ��"], swjd[i]) );
                }
                if (resualt["ȫ����õ�������"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(9, 1 + i, Gethistroyvalue<Ps_History>(resualt["ȫ����õ�������"], swjd[i]) );
                }
                if (resualt["ȫ����õ�������"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(10, 1 + i, Gethistroyvalue<Ps_History>(resualt["ȫ����õ�������"], swjd[i]));
                }
                if (resualt["ȫ����õ�������"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(11, 1 + i, Gethistroyvalue<Ps_History>(resualt["ȫ����õ�������"], swjd[i]) );
                }
                if (resualt["������"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(12, 1 + i, Gethistroyvalue<Ps_History>(resualt["������"], swjd[i]) );
                }
                if (resualt["��߹��縺��"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(13, 1 + i, Gethistroyvalue<Ps_History>(resualt["��߹��縺��"], swjd[i]));
                }
                if (resualt["����С���鷢����"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(14, 1 + i, Gethistroyvalue<Ps_History>(resualt["����С���鷢����"], swjd[i]));
                }
                if (resualt["Tmax"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(15, 1 + i, Gethistroyvalue<Ps_History>(resualt["Tmax"], swjd[i]));
                }

                if (resualt["����С��������"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(16, 1 + i, Gethistroyvalue<Ps_History>(resualt["����С��������"], swjd[i]));
                }
                if (resualt["ˮ��"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(17, 1 + i, Gethistroyvalue<Ps_History>(resualt["ˮ��"], swjd[i]));
                }
                if (resualt["���"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(18, 1 + i, Gethistroyvalue<Ps_History>(resualt["���"], swjd[i]));
                }
                if (resualt["�ȵ�"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(19, 1 + i, Gethistroyvalue<Ps_History>(resualt["�ȵ�"], swjd[i]));
                }
                if (resualt["�˿�"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(20, 1 + i, Gethistroyvalue<Ps_History>(resualt["�˿�"], swjd[i]));
                }

            }
            for (int i = 0; i < sywjd.Count; i++)
            {
                fpSpread1.Sheets[3].SetValue(3, 8 + i, Gethistroyvalue<Ps_History>(resualt["GDP(�����)"], swjd[i]) * 10000);
                if (resualt["һ��"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(4, 8 + i, Gethistroyvalue<Ps_History>(resualt["һ��"], swjd[i]) * 10000);
                }
                if (resualt["����"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(5, 8 + i, Gethistroyvalue<Ps_History>(resualt["����"], swjd[i]) * 10000);
                }
                if (resualt["����"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(6, 8 + i, Gethistroyvalue<Ps_History>(resualt["����"], swjd[i]) * 10000);
                }
                if (resualt["ȫ����õ���"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(7, 8 + i, Gethistroyvalue<Ps_History>(resualt["ȫ����õ���"], swjd[i]));
                }
                if (resualt["ȫ����õ���һ��"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(8, 8 + i, Gethistroyvalue<Ps_History>(resualt["ȫ����õ���һ��"], swjd[i]));
                }
                if (resualt["ȫ����õ�������"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(9, 8 + i, Gethistroyvalue<Ps_History>(resualt["ȫ����õ�������"], swjd[i]));
                }
                if (resualt["ȫ����õ�������"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(10, 8 + i, Gethistroyvalue<Ps_History>(resualt["ȫ����õ�������"], swjd[i]));
                }
                if (resualt["ȫ����õ�������"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(11, 8 + i, Gethistroyvalue<Ps_History>(resualt["ȫ����õ�������"], swjd[i]));
                }
                if (resualt["������"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(12, 8 + i, Gethistroyvalue<Ps_History>(resualt["������"], swjd[i]));
                }
                if (resualt["��߹��縺��"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(13, 8 + i, Gethistroyvalue<Ps_History>(resualt["��߹��縺��"], swjd[i]));
                }
                if (resualt["����С���鷢����"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(14, 8 + i, Gethistroyvalue<Ps_History>(resualt["����С���鷢����"], swjd[i]));
                }
                if (resualt["Tmax"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(15, 8 + i, Gethistroyvalue<Ps_History>(resualt["Tmax"], swjd[i]));
                }

                if (resualt["����С��������"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(16, 8 + i, Gethistroyvalue<Ps_History>(resualt["����С��������"], swjd[i]));
                }
                if (resualt["ˮ��"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(17, 8 + i, Gethistroyvalue<Ps_History>(resualt["ˮ��"], swjd[i]));
                }
                if (resualt["���"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(18, 8 + i, Gethistroyvalue<Ps_History>(resualt["���"], swjd[i]));
                }
                if (resualt["�ȵ�"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(19, 8 + i, Gethistroyvalue<Ps_History>(resualt["�ȵ�"], swjd[i]));
                }
                if (resualt["�˿�"] != null)
                {
                    fpSpread1.Sheets[3].SetValue(20, 8 + i, Gethistroyvalue<Ps_History>(resualt["�˿�"], swjd[i]));
                }
            }
            //�������ͳ��
            for (int j = 0; j < 20; j++)
            {
                if (j<18)
                {
                    if (resualt[hangzhi[j]] != null)
                    {
                        fpSpread1.Sheets[3].Cells[3 + j,1 + swjd.Count].Formula = "POWER(R" + (3 + j + 1).ToString() + "C" + (1 + swjd.Count ).ToString() + "/R" + (3 + j + 1).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                    }

                }
                else
                    fpSpread1.Sheets[3].Cells[3 + j, 1 + swjd.Count].Formula = "POWER(R" + (3 + j + 1).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (3 + j + 1).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                
            }
            if (sywjd.Count > 0)
            {

                for (int j = 0; j < 20; j++)
                {
                    if (j<18)
                    {
                        if (resualt[hangzhi[j]] != null)
                        {
                            fpSpread1.Sheets[3].Cells[3 + j, 8 + sywjd.Count].Formula = "POWER(R" + (3 + j + 1).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (3 + j + 1).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                        }
                    }
                   else
                        fpSpread1.Sheets[3].Cells[3 + j, 8 + sywjd.Count].Formula = "POWER(R" + (3 + j + 1).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (3 + j + 1).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";

                }
            }
            for (int i = 0; i < swjd.Count;i++ )
            {
                if (resualt["ȫ����õ���"] != null && resualt["�˿�"] != null)
                {
                    fpSpread1.Sheets[3].Cells[21, 1 + i].Formula = "R8C" + (2 + i).ToString() + "/R21C" + (2+ i).ToString() ;
                }
                if (resualt["ȫ����õ�������"] != null && resualt["�˿�"] != null)
               {
                   fpSpread1.Sheets[3].Cells[22, 1 + i].Formula = "R12C" + (2 + i).ToString() + "/R21C" + (2+ i).ToString() ;
               }
                
            }
            for (int i = 0; i < sywjd.Count;i++ )
            {
                if (resualt["ȫ����õ���"] != null && resualt["�˿�"] != null)
                {
                    fpSpread1.Sheets[3].Cells[21, 8 + i].Formula = "R8C" + (9 + i).ToString() + "/R21C" + (9+ i).ToString() ;
                }
                if (resualt["ȫ����õ�������"] != null && resualt["�˿�"] != null)
                {
                    fpSpread1.Sheets[3].Cells[22, 8 + i].Formula = "R12C" + (9 + i).ToString() + "/R21C" + (9 + i).ToString();
                }
                
            }

#endregion
           
        }
        //�����ع��õ�ʵ��
        private void fnqxgydsj()
        {
           int typeFlag2 = 2;
#region �ж�������������� ͬʱһ���кϲ���Ԫ��
            //��¼��ݵ�ʱ���
            List<int> swjd = new List<int>();
            List<int> sywjd = new List<int>();
            //����ʱ��� ���ж�ʱ��εķ�Χ Ȼ�������
            PSP_Years py = new PSP_Years();
            py.Flag = typeFlag2;


            IList<PSP_Years> li = Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", py);
            if (li.Count > 0)
            {
                firstyear = li[0].Year;
                endyear = li[li.Count-1].Year;
            }
            //else
            //{
            //    firstyear = 2000;
            //    endyear = 2008;
            //    py.BeginYear = 1990;
            //    py.FinishYear = endyear;
            //    py.StartYear = firstyear;
            //    py.EndYear = 2060;
            //    py.ID = Guid.NewGuid().ToString();
            //    Services.BaseService.Create<Ps_YearRange>(py);
            //}
            //�ж�ʱ��� ��������
            if (endyear <= 2000)
            {
                MessageBox.Show("����ʷ��չʵ���о���ָ���ʱ����趨����");
                return;
            }
            else if (firstyear <= 2000 && endyear <= 2010)
            {
                swjd.Clear();
                sywjd.Clear();
                int colnum = 0;
                if (endyear <= 2005)
                {
                    for (int i = 0; i <= endyear - 2000; i++)
                    {
                        //����һ������
                        fpSpread1.Sheets[4].Columns.Add(1, 1);
                        fpSpread1.Sheets[4].SetValue(2, 1, endyear - i + "��");
                        //��ӵ�Ԫ��ʽ

                        fpSpread1.Sheets[4].Columns[1 + i].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                        //
                        colnum++;
                       int year = 2000 + i;
                        swjd.Add(year);
                    }
                    fpSpread1.Sheets[4].Columns.Add(1 + colnum, 1);
                    fpSpread1.Sheets[4].SetValue(2, 1 + colnum, "ʮ����������");
                    //��ӵ�Ԫ��ʽ
                    //FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                    fpSpread1.Sheets[4].Columns[1 + colnum].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                }
                else if (endyear > 2005)
                {
                    for (int i = 0; i <= 5; i++)
                    {
                        //����һ������
                        fpSpread1.Sheets[4].Columns.Add(1, 1);
                        fpSpread1.Sheets[4].SetValue(2, 1, 2005 - i + "��");
                        //��ӵ�Ԫ��ʽ
                        // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                        fpSpread1.Sheets[4].Columns[1 + i].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                        //
                        int year =2000 + i;
                        swjd.Add(year);
                        colnum++;
                    }
                    fpSpread1.Sheets[4].Columns.Add(1 + colnum, 1);
                    fpSpread1.Sheets[4].SetValue(2, 1 + colnum, "ʮ����������");
                    SetSheetViewColumnsWhith(fpSpread1.Sheets[4], 1 + colnum, "ʮ����������");
                    //��ӵ�Ԫ��ʽ
                    // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                    fpSpread1.Sheets[4].Columns[1 + colnum].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                    int j = 0;
                    for (int i = 0; i < endyear - 2005; i++)
                    {
                        //����һ������
                        fpSpread1.Sheets[4].Columns.Add(8, 1);
                        fpSpread1.Sheets[4].SetValue(2, 8, endyear - i + "��");
                        //��ӵ�Ԫ��ʽ
                        // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                        fpSpread1.Sheets[4].Columns[8 + i].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                        int year = 2006 + i;
                        sywjd.Add(year);
                        //
                        j++;
                    }
                    //fpSpread1.Sheets[0].Columns.Add(9 + j, 1);
                    fpSpread1.Sheets[4].SetValue(2, 8 + j, "ʮһ��ǰ" + j + "��������");
                    SetSheetViewColumnsWhith(fpSpread1.Sheets[3], 8 + j, "ʮһ��ǰ" + j + "��������");
                    //��ӵ�Ԫ��ʽ
                    // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
                    fpSpread1.Sheets[4].Columns[8 + j].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
                }

            }
            fpSpread1.Sheets[4].Cells[0, 0].ColumnSpan = 3 + sywjd.Count + swjd.Count;
            fpSpread1.Sheets[4].Cells[1, 0].ColumnSpan = 3 + sywjd.Count + swjd.Count;
            int Colnum = 3 + sywjd.Count + swjd.Count;
            int rownum = 4;
#endregion
#region ��ȡ���� �ж������ͼ����ʽ
            PSP_Types qshydl = null, tdydl = null, qshzdfh = null, tdzdfh = null,qszdxs=null,tdzdxs=null;
            IList<PSP_Types> qxqshydl = null, qxtdydl = null, qxqshzdfh = null, qxtdzdfh = null;
            string con = "ProjectID='" + ProjectUID + "'and Title like'ȫ����õ���%' and Flag2='" + typeFlag2 + "'";
            qshydl = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (qshydl!=null)
            {
                rownum += 1;
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + qshydl.ID + "' and Flag2='" + typeFlag2 + "'";
                qxqshydl = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                rownum += qxqshydl.Count;
            }
            con = "ProjectID='" + ProjectUID + "'and Title like'ͳ���õ���%' and Flag2='" + typeFlag2 + "'";
            tdydl = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (tdydl!=null)
            {
                rownum += 1;
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + tdydl.ID + "' and Flag2='" + typeFlag2 + "'";
              
                qxtdydl = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                rownum += qxtdydl.Count;
            }
            con = "ProjectID='" + ProjectUID + "'and Title like'ȫ�����󸺺�%' and Flag2='" + typeFlag2 + "'";
            qshzdfh = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (qshzdfh != null)
            {
                rownum += 1;
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + qshzdfh.ID + "' and Flag2='" + typeFlag2 + "'";

                qxqshzdfh = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                if (qxqshzdfh.Count!=0)
                {
                    rownum += qxqshzdfh.Count + 1;
                }
                con = "ProjectID='" + ProjectUID + "'and Title like'��󸺺�����Сʱ��%' and ParentID='" + qshzdfh.ID + "' and Flag2='" + typeFlag2 + "'";
                qszdxs = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                if (qszdxs!=null)
                {
                    rownum += 1;
                }
            }
            con = "ProjectID='" + ProjectUID + "'and Title like'ͳ����󸺺�%' and Flag2='" + typeFlag2 + "'";
            tdzdfh = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (qshzdfh != null)
            {
                rownum += 1;
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + tdzdfh.ID + "' and Flag2='" + typeFlag2 + "'";

                qxtdzdfh = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                if (qxtdzdfh.Count!=0)
                {
                    rownum+=qxtdzdfh.Count+1;
                }
                con = "ProjectID='" + ProjectUID + "'and Title like'��󸺺�����Сʱ��%' and ParentID='" + tdzdfh.ID + "' and Flag2='" + typeFlag2 + "'";
                tdzdxs = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                if (tdzdxs!=null)
                {
                    rownum += 1;
                }
            }
           
            //������ Ϊ��������� ȫ����õ�������
            int flag1 = 0;
            if (qshydl!=null)
            {
                flag1 = 1;
                fpSpread1.Sheets[4].Rows.Add(3, 1);
                fpSpread1.Sheets[4].SetValue(3, 0 ,"ȫ����õ���");
                fpSpread1.Sheets[4].Rows[3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < swjd.Count;i++ )
                {
                   
                        con = "TypeID='" + qshydl.ID + "' AND Year='"+swjd[i]+"'";
                        PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                        if (pv!=null)
                        {

                            fpSpread1.Sheets[4].Cells[3, 1 + i].CellType = numberCellTypes1;
                           
                            fpSpread1.Sheets[4].SetValue(3, 1 + i, pv.Value*10000);
                        }
                        else
                        {
                            fpSpread1.Sheets[4].SetValue(3, 1 + i,0);
                        }
                  
                }
                fpSpread1.Sheets[4].Cells[3, 1 + swjd.Count].CellType = percentcelltypes;
                fpSpread1.Sheets[4].Cells[3, 1 + swjd.Count].Formula = "POWER(R4C" + (1 + swjd.Count).ToString() + "/R4C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                for (int i = 0; i < sywjd.Count;i++ )
                {
                    con = "TypeID='" + qshydl.ID + "' AND Year='" + sywjd[i] + "'";
                    PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                    if (pv != null)
                    {

                        fpSpread1.Sheets[4].Cells[3, 8 + i].CellType = numberCellTypes1;

                        fpSpread1.Sheets[4].SetValue(3, 8 + i, pv.Value * 10000);
                    }
                    else
                    {
                        fpSpread1.Sheets[4].SetValue(3, 1 + i, 0);
                    }
                }
                fpSpread1.Sheets[4].Cells[3, 8+ sywjd.Count].CellType = percentcelltypes;
                fpSpread1.Sheets[4].Cells[3, 8 + sywjd.Count].Formula = "POWER(R4C" + (8 + sywjd.Count).ToString() + "/R4C7," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                //����������ص�����
                for (int i = 0; i < qxqshydl.Count;i++ )
                {
                    flag1++;
                    fpSpread1.Sheets[4].Rows.Add(4+i, 1);
                    fpSpread1.Sheets[4].Rows[4+i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    if (i==0)
                    {
                        fpSpread1.Sheets[4].SetValue(4 + i, 0, "���У�" + qxqshydl[i].Title);
                        fpSpread1.Sheets[4].Cells[4 + i, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        for (int j = 0; j < swjd.Count;j++ )
                        {
                            con = "TypeID='" + qxqshydl[i].ID + "' AND Year='" + swjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4+i, 1 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4+i, 1 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + i, 1 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4+i, 1 + swjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + i, 1 + swjd.Count].Formula = "POWER(R"+(5+i).ToString()+"C" + (1 + swjd.Count).ToString() + "/R"+(5+i).ToString()+"C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                        for (int j = 0; j < sywjd.Count;j++ )
                        {
                            con = "TypeID='" + qxqshydl[i].ID + "' AND Year='" + sywjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + i, 8 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + i, 8 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + i, 8+ j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + i, 8 + sywjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + i, 8 + sywjd.Count].Formula = "POWER(R" + (5 + i).ToString() + "C" + (8+ sywjd.Count).ToString() + "/R" + (5 + i).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                    }
                    else
                    {
                        fpSpread1.Sheets[4].SetValue(4 + i, 0,  qxqshydl[i].Title);
                        fpSpread1.Sheets[4].Cells[4 + i, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        for (int j = 0; j < swjd.Count; j++)
                        {
                            con = "TypeID='" + qxqshydl[i].ID + "' AND Year='" + swjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + i, 1 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + i, 1 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + i, 1 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + i, 1 + swjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + i, 1 + swjd.Count].Formula = "POWER(R" + (5 + i).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (5 + i).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                        for (int j = 0; j < sywjd.Count; j++)
                        {
                            con = "TypeID='" + qxqshydl[i].ID + "' AND Year='" + sywjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + i, 8 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + i, 8 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + i, 8 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + i, 8 + sywjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + i, 8 + sywjd.Count].Formula = "POWER(R" + (5 + i).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (5 + i).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                    }
                }
            }
            //����ͳ���õ�������
            int flag2 = 0;
            if (tdydl!=null)
            {

                flag2 = 1;
                fpSpread1.Sheets[4].Rows.Add(3+flag1, 1);
                fpSpread1.Sheets[4].SetValue(3 + flag1, 0, "ͳ���õ���");
                fpSpread1.Sheets[4].Rows[3+flag1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < swjd.Count; i++)
                {

                    con = "TypeID='" + tdydl.ID + "' AND Year='" + swjd[i] + "'";
                    PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                    if (pv != null)
                    {

                        fpSpread1.Sheets[4].Cells[3+flag1, 1 + i].CellType = numberCellTypes1;

                        fpSpread1.Sheets[4].SetValue(3 + flag1, 1 + i, pv.Value * 10000);
                    }
                    else
                    {
                        fpSpread1.Sheets[4].SetValue(3 + flag1, 1 + i, 0);
                    }

                }
                fpSpread1.Sheets[4].Cells[3 + flag1, 1 + swjd.Count].CellType = percentcelltypes;
                fpSpread1.Sheets[4].Cells[3 + flag1, 1 + swjd.Count].Formula = "POWER(R"+(4+flag1).ToString()+"C" + (1 + swjd.Count).ToString() + "/R"+(4+flag1).ToString()+"C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                for (int i = 0; i < sywjd.Count; i++)
                {
                    con = "TypeID='" + tdydl.ID + "' AND Year='" + sywjd[i] + "'";
                    PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                    if (pv != null)
                    {

                        fpSpread1.Sheets[4].Cells[3 + flag1, 8 + i].CellType = numberCellTypes1;

                        fpSpread1.Sheets[4].SetValue(3 + flag1, 8 + i, pv.Value * 10000);
                    }
                    else
                    {
                        fpSpread1.Sheets[4].SetValue(3 + flag1, 1 + i, 0);
                    }
                }
                 fpSpread1.Sheets[4].Cells[3+flag1, 8+ sywjd.Count].CellType = percentcelltypes;
                 fpSpread1.Sheets[4].Cells[3 + flag1, 8 + sywjd.Count].Formula = "POWER(R" + (4 + flag1).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (4 + flag1).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                //����������ص�����
                for (int i = 0; i < qxtdydl.Count; i++)
                {
                    flag2++;
                    fpSpread1.Sheets[4].Rows.Add(4+flag1 + i, 1);
                    fpSpread1.Sheets[4].Rows[4 +flag1+ i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    if (i == 0)
                    {
                        fpSpread1.Sheets[4].SetValue(4+flag1 + i, 0, "���У�" + qxtdydl[i].Title);
                        fpSpread1.Sheets[4].Cells[4 + flag1 + i, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        for (int j = 0; j < swjd.Count; j++)
                        {
                            con = "TypeID='" + qxtdydl[i].ID + "' AND Year='" + swjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + i, 1 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + i, 1 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + i, 1 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1 + i, 1 + swjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + i, 1 + swjd.Count].Formula = "POWER(R" + (5 + flag1 + i).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (5 + flag1 + i).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                        for (int j = 0; j < sywjd.Count; j++)
                        {
                            con = "TypeID='" + qxtdydl[i].ID + "' AND Year='" + sywjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + i, 8 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + i, 8 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + i, 8 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1+i, 8 + sywjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + i, 8 + sywjd.Count].Formula = "POWER(R" + (5 + flag1 + i).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (5 + flag1 + i).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                    }
                    else
                    {
                        fpSpread1.Sheets[4].SetValue(4 + flag1 + i, 0, qxtdydl[i].Title);
                        fpSpread1.Sheets[4].Cells[4 + flag1 + i, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        for (int j = 0; j < swjd.Count; j++)
                        {
                            con = "TypeID='" + qxtdydl[i].ID + "' AND Year='" + swjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + i, 1 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + i, 1 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + i, 1 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1 + i, 1 + swjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + i, 1 + swjd.Count].Formula = "POWER(R" + (5 + flag1 + i).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (5 + flag1 + i).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                        for (int j = 0; j < sywjd.Count; j++)
                        {
                            con = "TypeID='" + qxtdydl[i].ID + "' AND Year='" + sywjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + i, 8 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + i, 8 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + i, 8 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1 + i, 8 + sywjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + i, 8 + sywjd.Count].Formula = "POWER(R" + (5 + flag1 + i).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (5 + flag1 + i).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                    }
                }
            }
            //ȫ�����󸺺� ע��ͬʱ�ʺ���󸺺�����Сʱ��
            int flag3 = 0;
            if (qshzdfh!=null)
            {
                flag3 = 1;
                fpSpread1.Sheets[4].Rows.Add(3 + flag1+flag2, 1);
                fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2, 0, "ȫ�����󸺺�");
                fpSpread1.Sheets[4].Rows[3 + flag1 + flag2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < swjd.Count; i++)
                {

                    con = "TypeID='" + qshzdfh.ID + "' AND Year='" + swjd[i] + "'";
                    PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                    if (pv != null)
                    {

                        fpSpread1.Sheets[4].Cells[3 + flag1 + flag2, 1 + i].CellType = numberCellTypes1;

                        fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2, 1 + i, pv.Value );
                    }
                    else
                    {
                        fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2, 1 + i, 0);
                    }

                }
                fpSpread1.Sheets[4].Cells[3 + flag1 + flag2, 1 + swjd.Count].CellType = percentcelltypes;
                fpSpread1.Sheets[4].Cells[3 + flag1 + flag2, 1 + swjd.Count].Formula = "POWER(R" + (4 + flag1 + flag2).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (4 + flag1 + flag2).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                for (int i = 0; i < sywjd.Count; i++)
                {
                    con = "TypeID='" + qshzdfh.ID + "' AND Year='" + sywjd[i] + "'";
                    PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                    if (pv != null)
                    {

                        fpSpread1.Sheets[4].Cells[3 + flag1 + flag2, 8 + i].CellType = numberCellTypes1;

                        fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2, 8 + i, pv.Value);
                    }
                    else
                    {
                        fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2, 1 + i, 0);
                    }
                }
                fpSpread1.Sheets[4].Cells[3 + flag1 + flag2, 8 + sywjd.Count].CellType = percentcelltypes;
                fpSpread1.Sheets[4].Cells[3 + flag1 + flag2, 8 + sywjd.Count].Formula = "POWER(R" + (4 + flag1 + flag2).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (4 + flag1 + flag2).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                //����������ص�����
                for (int i = 0; i < qxqshzdfh.Count; i++)
                {
                    flag3++;
                    fpSpread1.Sheets[4].Rows.Add(4 + flag1 + flag2 + i, 1);
                    fpSpread1.Sheets[4].Rows[4 + flag1 + flag2 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    if (i == 0)
                    {
                        fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + i, 0, "���У�" + qxqshzdfh[i].Title);
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        for (int j = 0; j < swjd.Count; j++)
                        {
                            con = "TypeID='" + qxqshzdfh[i].ID + "' AND Year='" + swjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 1 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + i, 1 + j, pv.Value );
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + i, 1 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 1 + swjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 1 + swjd.Count].Formula = "POWER(R" + (5 + flag1 + flag2 + i).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (5 + flag1 + flag2 + i).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                        for (int j = 0; j < sywjd.Count; j++)
                        {
                            con = "TypeID='" + qxqshzdfh[i].ID + "' AND Year='" + sywjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 8 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + i, 8 + j, pv.Value );
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + i, 8 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 8 + sywjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 8 + sywjd.Count].Formula = "POWER(R" + (5 + flag1 + flag2 + i).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (5 + flag1 + flag2 + i).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                    }
                    else
                    {
                        fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + i, 0, qxqshzdfh[i].Title);
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        for (int j = 0; j < swjd.Count; j++)
                        {
                            con = "TypeID='" + qxqshzdfh[i].ID + "' AND Year='" + swjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 1 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + i, 1 + j, pv.Value);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + i, 1 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 1 + swjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 1 + swjd.Count].Formula = "POWER(R" + (5 + flag1 + flag2 + i).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (5 + flag1 + flag2 + i).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                        for (int j = 0; j < sywjd.Count; j++)
                        {
                            con = "TypeID='" + qxqshzdfh[i].ID + "' AND Year='" + sywjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 8 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + i, 8 + j, pv.Value );
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + i, 8 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 8 + sywjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + i, 8 + sywjd.Count].Formula = "POWER(R" + (5 + flag1 + flag2 + i).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (5 + flag1 + flag2 + i).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                    }
                }
                //����ͬʱ��
                if (qxqshzdfh.Count!=0)
                {
                    flag3 += 1;
                    fpSpread1.Sheets[4].Rows.Add(3+ flag1 + flag2 + flag3 - 1, 1);
                    fpSpread1.Sheets[4].SetValue(3+ flag1 + flag2 + flag3 - 1, 0, "ͬʱ��");
                    fpSpread1.Sheets[4].Cells[3+ flag1 + flag2 + flag3 - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    for (int j = 0; j < swjd.Count; j++)
                    {
                        fpSpread1.Sheets[4].Cells[3+ flag1 + flag2 + flag3 - 1, 1 + j].Formula = "R" + (4+ flag1 + flag2).ToString() + "C" + (2 + j).ToString() + "/SUM(R" + (5 + flag1 + flag2).ToString() + "C" + (2 + j).ToString() + ":R" + (4+ flag1 + flag2 + flag3 - 2).ToString() + "C" + (2 + j).ToString() + ")";
                    }
                    for (int j = 0; j < sywjd.Count; j++)
                    {
                        fpSpread1.Sheets[4].Cells[3 + flag1 + flag2 + flag3 - 1, 8 + j].Formula = "R" + (4+ flag1 + flag2).ToString() + "C" + (9 + j).ToString() + "/SUM(R" + (5 + flag1 + flag2).ToString() + "C" + (9 + j).ToString() + ":R" + (4 + flag1 + flag2 + flag3 - 2).ToString() + "C" + (2 + j).ToString() + ")";
                    }
                }
                
                //��󸺺�Сʱ��
                if (qszdxs!=null)
                {
                    flag3 += 1;
                    fpSpread1.Sheets[4].Rows.Add(3+ flag1 + flag2 + flag3 - 1, 1);
                    fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2 + flag3 - 1, 0, "��󸺺�����Сʱ��");
                    fpSpread1.Sheets[4].Cells[3 + flag1 + flag2 + flag3 - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    for (int j = 0; j < swjd.Count; j++)
                    {
                        con = "TypeID='" + qszdxs.ID + "' AND Year='" + swjd[j] + "'";
                        PSP_Values pv =(PSP_Values) Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                        if (pv != null)
                        {

                            fpSpread1.Sheets[4].Cells[3+ flag1 + flag2 + flag3-1, 1 + j].CellType = numberCellTypes1;

                            fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2 +flag3-1, 1 + j, pv.Value);
                        }
                        else
                        {
                            fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2 + flag3-1, 1 + j, 0);
                        }
                    }
                    for (int j = 0; j < sywjd.Count; j++)
                    {
                        con = "TypeID='" + qszdxs.ID + "' AND Year='" + sywjd[j] + "'";
                        PSP_Values pv =(PSP_Values) Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                        if (pv != null)
                        {

                            fpSpread1.Sheets[4].Cells[3+ flag1 + flag2 + flag3 - 1, 8 + j].CellType = numberCellTypes1;

                            fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2 + flag3 - 1, 8 + j, pv.Value);
                        }
                        else
                        {
                            fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2 + flag3 - 1, 8 + j, 0);
                        }
                    }

                }
               
                 
            }
            //����ͳ����󸺺�����
            int flag4 = 0;
            if (tdzdfh!=null)
            {
                flag4 = 1;
                fpSpread1.Sheets[4].Rows.Add(3 + flag1 + flag2+flag3, 1);
                fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2+flag3, 0, "ͳ����󸺺�");
                fpSpread1.Sheets[4].Rows[3 + flag1 + flag2 + flag3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < swjd.Count; i++)
                {

                    con = "TypeID='" + tdzdfh.ID + "' AND Year='" + swjd[i] + "'";
                    PSP_Values pv =(PSP_Values) Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                    if (pv != null)
                    {

                        fpSpread1.Sheets[4].Cells[3 + flag1 + flag2 + flag3, 1 + i].CellType = numberCellTypes1;

                        fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2 + flag3, 1 + i, pv.Value * 10000);
                    }
                    else
                    {
                        fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2 + flag3, 1 + i, 0);
                    }

                }
                fpSpread1.Sheets[4].Cells[3 + flag1 + flag2 + flag3, 1 + swjd.Count].CellType = percentcelltypes;
                fpSpread1.Sheets[4].Cells[3 + flag1 + flag2 + flag3, 1 + swjd.Count].Formula = "POWER(R" + (4 + flag1 + flag2 + flag3).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (4 + flag1 + flag2 + flag3).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                for (int i = 0; i < sywjd.Count; i++)
                {
                    con = "TypeID='" + tdzdfh.ID + "' AND Year='" + sywjd[i] + "'";
                    PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                    if (pv != null)
                    {

                        fpSpread1.Sheets[4].Cells[3 + flag1 + flag2 + flag3, 8 + i].CellType = numberCellTypes1;

                        fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2 + flag3, 8 + i, pv.Value * 10000);
                    }
                    else
                    {
                        fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2 + flag3, 1 + i, 0);
                    }
                }
                fpSpread1.Sheets[4].Cells[3 + flag1 + flag2 + flag3, 8 + sywjd.Count].CellType = percentcelltypes;
                fpSpread1.Sheets[4].Cells[3 + flag1 + flag2 + flag3, 8 + sywjd.Count].Formula = "POWER(R" + (4 + flag1 + flag2 + flag3).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (4 + flag1 + flag2 + flag3).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                //����������ص�����
                for (int i = 0; i < qxtdzdfh.Count; i++)
                {
                    flag4++;
                    fpSpread1.Sheets[4].Rows.Add(4 + flag1 + flag2 + flag3 + i, 1);
                    fpSpread1.Sheets[4].Rows[4 + flag1 + flag2 + flag3 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    if (i ==0)
                    {
                        fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + flag3 + i, 0, "���У�" + qxtdzdfh[i].Title);
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        for (int j = 0; j < swjd.Count; j++)
                        {
                            con = "TypeID='" + qxtdzdfh[i].ID + "' AND Year='" + swjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 1 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + flag3 + i, 1 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + flag3 + i, 1 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 1 + swjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 1 + swjd.Count].Formula = "POWER(R" + (5 + flag1 + flag2 + flag3 + i).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (5 + flag1 + flag2 + flag3 + i).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                        for (int j = 0; j < sywjd.Count; j++)
                        {
                            con = "TypeID='" + qxtdzdfh[i].ID + "' AND Year='" + sywjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 8 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + flag3 + i, 8 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + flag3 + i, 8 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 8 + sywjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 8 + sywjd.Count].Formula = "POWER(R" + (5 + flag1 + flag2 + flag3 + i).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (5 + flag1 + flag2 + flag3 + i).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                    }
                    else
                    {
                        fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + flag3 + i, 0, qxtdzdfh[i].Title);
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        for (int j = 0; j < swjd.Count; j++)
                        {
                            con = "TypeID='" + qxtdzdfh[i].ID + "' AND Year='" + swjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 1 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + flag3 + i, 1 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + flag3 + i, 1 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 1 + swjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 1 + swjd.Count].Formula = "POWER(R" + (5 + flag1 + flag2 + flag3 + i).ToString() + "C" + (1 + swjd.Count).ToString() + "/R" + (5 + flag1 + flag2 + flag3 + i).ToString() + "C2," + (1 / Convert.ToDouble(swjd.Count)).ToString() + ")" + "-1";
                        for (int j = 0; j < sywjd.Count; j++)
                        {
                            con = "TypeID='" + qxtdzdfh[i].ID + "' AND Year='" + sywjd[j] + "'";
                            PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                            if (pv != null)
                            {

                                fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 8 + j].CellType = numberCellTypes1;

                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + flag3 + i, 8 + j, pv.Value * 10000);
                            }
                            else
                            {
                                fpSpread1.Sheets[4].SetValue(4 + flag1 + flag2 + flag3 + i, 8 + j, 0);
                            }
                        }
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 8 + sywjd.Count].CellType = percentcelltypes;
                        fpSpread1.Sheets[4].Cells[4 + flag1 + flag2 + flag3 + i, 8 + sywjd.Count].Formula = "POWER(R" + (5 + flag1 + flag2 + flag3 + i).ToString() + "C" + (8 + sywjd.Count).ToString() + "/R" + (5 + flag1 + flag2 + flag3 + i).ToString() + "C7," + (1 / Convert.ToDouble(sywjd.Count)).ToString() + ")" + "-1";
                    }
                }
                //����ͬʱ��
                if (qxtdzdfh.Count!=0)
                {
                    flag4 += 1;
                    fpSpread1.Sheets[4].Rows.Add(3 + flag1 + flag2 + flag3 + flag4 - 1, 1);
                    fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2 + flag3 + flag4 - 1, 0, "ͬʱ��");
                    fpSpread1.Sheets[4].Cells[3 + flag1 + flag2 + flag3 + flag4 - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    for (int j = 0; j < swjd.Count; j++)
                    {
                        fpSpread1.Sheets[4].Cells[3 + flag1 + flag2 + flag3 + flag4 - 1, 1 + j].Formula = "R" + (4+ flag1 + flag2 + flag3).ToString() + "C" + (2 + j).ToString() + "/SUM(R" + (5 + flag1 + flag2 + flag3).ToString() + "C" + (2 + j).ToString() + ":R" + (4 + flag1 + flag2 + flag3 + flag4 - 2).ToString() + "C" + (2 + j).ToString() + ")";
                    }
                    for (int j = 0; j < sywjd.Count; j++)
                    {
                        fpSpread1.Sheets[4].Cells[3+ flag1 + flag2 + flag3 + flag4 - 1, 8 + j].Formula = "R" + (4 + flag1 + flag2 + flag3).ToString() + "C" + (9 + j).ToString() + "/SUM(R" + (5 + flag1 + flag2 + flag3).ToString() + "C" + (9 + j).ToString() + ":R" + (4 + flag1 + flag2 + flag3 + flag4 - 2).ToString() + "C" + (2 + j).ToString() + ")";
                    }
                }
               
                //��󸺺�Сʱ��
                if (tdzdxs != null)
                {
                    flag4 += 1;
                    fpSpread1.Sheets[4].Rows.Add(3 + flag1 + flag2 + flag3+flag4 - 1, 1);
                    fpSpread1.Sheets[4].SetValue(3+ flag1 + flag2 + flag3 +flag4- 1, 0, "��󸺺�����Сʱ��");
                    fpSpread1.Sheets[4].Cells[3 + flag1 + flag2 + flag3+flag4 - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    for (int j = 0; j < swjd.Count; j++)
                    {
                        con = "TypeID='" + tdzdxs.ID + "' AND Year='" + swjd[j] + "'";
                        PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                        if (pv != null)
                        {

                            fpSpread1.Sheets[4].Cells[3+ flag1 + flag2 + flag3+flag4 - 1, 1 + j].CellType = numberCellTypes1;

                            fpSpread1.Sheets[4].SetValue(3+ flag1 + flag2 + flag3+flag4 - 1, 1 + j, pv.Value);
                        }
                        else
                        {
                            fpSpread1.Sheets[4].SetValue(3+ flag1 + flag2 + flag3+flag4 - 1, 1 + j, 0);
                        }
                    }
                    for (int j = 0; j < sywjd.Count; j++)
                    {
                        con = "TypeID='" + tdzdxs.ID + "' AND Year='" + sywjd[j] + "'";
                        PSP_Values pv = (PSP_Values)Services.BaseService.GetObject("SelectPSP_ValuesByWhere", con);
                        if (pv != null)
                        {

                            fpSpread1.Sheets[4].Cells[3+ flag1 + flag2 + flag3 +flag4- 1, 8 + j].CellType = numberCellTypes1;

                            fpSpread1.Sheets[4].SetValue(3+ flag1 + flag2 + flag3+flag4 - 1, 8 + j, pv.Value);
                        }
                        else
                        {
                            fpSpread1.Sheets[4].SetValue(3 + flag1 + flag2 + flag3 +flag4- 1, 8 + j, 0);
                        }
                    }

                }
               
            }
            Sheet_GridandColor(fpSpread1.Sheets[4], rownum, Colnum); 
#endregion
        }
        //�ļ��Ͷ��������ո�������
        private void xddxrqx()
        {
            fpSpread1.Sheets[5].RowCount=0;
            fpSpread1.Sheets[5].ColumnCount=0;
            string con = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", con);
            int flag = 0; List<PS_Table_AreaWH> areaidlist = new List<PS_Table_AreaWH>();
            //ͳ����Щ��������Щ���� �Լ��жϱ������ƺ��ܵ����� �Լ�������ʾ
            foreach (PS_Table_AreaWH area in list)
            {
                con = "IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' and  year(BurdenDate) in('" + beginyear + "','"+lastyear+"') and AreaID='" + area.ID+ "' AND Season in('�ļ�','����')";
                IList list1 = Services.BaseService.GetList("SelectBurdenLineByType", con);
                if (list1.Count!=0)
                {
                    flag++;
                    areaidlist.Add(area);
                }
            }
            if (flag!=0)
            {
                fpSpread1.Sheets[5].RowCount = 31;
                fpSpread1.Sheets[5].ColumnCount = 7 * flag;
                for (int i = 0; i < areaidlist.Count;i++ )
                {
                    string tiltle = areaidlist[i].Title + beginyear.ToString()+"��/" + lastyear.ToString() + "���ļ��Ͷ��������ո������߱�";
                    Create_Dxtable(fpSpread1.Sheets[5], 1 + i * 7, tiltle, beginyear, lastyear, areaidlist[i].ID);
                }
            }
        }
        //����󸺺����߱�
        private void yzdfhqx()
        {
            fpSpread1.Sheets[6].RowCount = 0;
            fpSpread1.Sheets[6].ColumnCount = 0;
            string con = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", con);
            int flag = 0; List<PS_Table_AreaWH> areaidlist = new List<PS_Table_AreaWH>();
            //ͳ����Щ��������Щ���� �Լ��жϱ������ƺ��ܵ����� �Լ�������ʾ
            foreach (PS_Table_AreaWH area in list)
            {
                con = " uid like '%" + Itop.Client.MIS.ProgUID + "%' and  BurdenYear in('" + beginyear + "','" + lastyear + "') and AreaID='" + area.ID + "' ";
                IList list1 = Services.BaseService.GetList("SelectBurdenMonthByWhere", con);
                if (list1.Count != 0)
                {
                    flag++;
                    areaidlist.Add(area);
                }
            }
            if (flag != 0)
            {
                fpSpread1.Sheets[6].ColumnCount =15;
                fpSpread1.Sheets[6].RowCount =15 * flag;
                for (int i = 0; i < areaidlist.Count; i++)
                {
                    string tiltle = areaidlist[i].Title + beginyear.ToString() + "��/" + lastyear.ToString() + "������󸺺�";
                    Create_YZDFHtable(fpSpread1.Sheets[6], 1 + i *15, tiltle, beginyear, lastyear, areaidlist[i].ID);
                }
            }
        }
        //���ýṹ��չԤ��ṹ��
        private void jjjgfzyc()
        {
            jjfzyctable();
           // Ps_forecast_list report =qqreporttiltle;
            //string con = "UserID='" + ProjectUID + "'AND Col1='1'AND Title='" + qqreporttiltle + "'";
            //report=(Ps_forecast_list)Services.BaseService.GetObject("SelectPs_forecast_listByWhere",con);
            int colnum=0;
            if (qqreporttiltle == null)
            {
                MessageBox.Show("����ѡ��ĸ���Ԥ�ⷽ�������ڣ�");
                return;
            }
            
            else 
            {
               //��ȡ���� 
                string con = "ForecastID='" + qqreporttiltle.ID + "'AND Title Like'ȫ����GDP%' AND Forecast!='0' order by Forecast";
                IList<Ps_Forecast_Math> psgdp = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                Ps_History gdp = null,yc=null,ec=null,sc=null,xc=null,cs=null;
                IList<Ps_Forecast_Math> psyc = new List<Ps_Forecast_Math>(), psec = new List<Ps_Forecast_Math>(), pssc = new List<Ps_Forecast_Math>();
                if (psgdp.Count!=0)
                {
                    gdp = new Ps_History();
                    gdp.ID = psgdp[0].Col1.Substring(psgdp[0].Col1.IndexOf('|')+1);
                gdp = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByKey", gdp);
                con = "ForecastID='" + qqreporttiltle.ID + "'AND Title Like'һ��%' AND Forecast !=0 AND ParentID='" + ((Ps_Forecast_Math)psgdp[0]).ID + "'order by Forecast";
                    psyc = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                    yc = new Ps_History();
                    if (psyc.Count!=0)
                    {
                        yc.ID = psyc[0].Col1.Substring(psyc[0].Col1.IndexOf('|') + 1);
                        yc = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByKey", yc);
                    }
                    
                    con = "ForecastID='" + qqreporttiltle.ID + "'AND Title Like'����%' AND Forecast !=0 AND ParentID='" + ((Ps_Forecast_Math)psgdp[0]).ID + "'order by Forecast";
                   
                    psec = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                    if (psec.Count!=0)
                    {
                        ec.ID = psec[0].Col1.Substring(psec[0].Col1.IndexOf('|') + 1);
                        ec = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByKey", ec);
                    }
                    ec= new Ps_History();
                    
                    con = "ForecastID='" + qqreporttiltle.ID + "'AND Title Like'����%' AND Forecast !=0 AND ParentID='" + ((Ps_Forecast_Math)psgdp[0]).ID + "'order by Forecast";
                    pssc = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                    sc = new Ps_History();
                    if (pssc.Count!=0)
                    {
                        sc.ID = pssc[0].Col1.Substring(pssc[0].Col1.IndexOf('|') + 1);
                        sc = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByKey", sc);
                    }
                   

                }
                con = "ForecastID='" + qqreporttiltle.ID + "'AND Title Like'����˿�%' AND Forecast !='0' order by Forecast";
                IList<Ps_Forecast_Math> pscountry = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                if (pscountry.Count!=0)
                {
                    xc = new Ps_History();
                    xc.ID = pscountry[0].Col1.Substring(pscountry[0].Col1.IndexOf('|') + 1)
;
                    xc = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByKey", xc);
                }
                con = "ForecastID='" + qqreporttiltle.ID + "'AND Title Like'�����˿�%' AND Forecast !='0' order by Forecast";
                IList<Ps_Forecast_Math> pscity = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                if (pscity.Count!=0)
                {
                    cs = new Ps_History();
                    cs.ID = pscity[0].Col1.Substring(pscity[0].Col1.IndexOf('|') + 1);
                    cs = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByKey", cs);
                }
               //д������
                
                if (endyear>=2009&&firstyear<2005)
                {
                    double num = 0;
                    for (int i = 0;i < 4;i++ )
                    {
                      
                        colnum++;
                        fpSpread1.Sheets[7].Columns.Add(2,1);
                        fpSpread1.Sheets[7].Cells[2, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        fpSpread1.Sheets[7].SetValue(2,2,(2008-i).ToString()+"(ʵ��)");
                        string year="y"+(2008-i).ToString();
                        fpSpread1.Sheets[7].Cells[3,2].CellType=numberCellTypes1;
                        fpSpread1.Sheets[7].Cells[4,2].CellType=numberCellTypes1;
                        fpSpread1.Sheets[7].Cells[5,2].CellType=percentcelltypes;
                         fpSpread1.Sheets[7].Cells[6,2].CellType=percentcelltypes;
                         fpSpread1.Sheets[7].Cells[7,2].CellType=percentcelltypes;
                         fpSpread1.Sheets[7].Cells[8,2].CellType=numberCellTypes1;
                         fpSpread1.Sheets[7].Cells[9,2].CellType=numberCellTypes1;
                         fpSpread1.Sheets[7].Cells[3, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[4, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[5, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[6, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[7, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[8, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[9, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                       
                        if (gdp!=null)
                        {
                         
                          fpSpread1.Sheets[7].SetValue(3,2, Gethistroyvalue<Ps_History>(gdp,year)*10000);
                        }
                        else
                          fpSpread1.Sheets[7].SetValue(3,2, 0);

                        if (yc!=null&&gdp!=null)
                        {
                            double ycl=Gethistroyvalue<Ps_History>(yc,year)/Gethistroyvalue<Ps_History>(gdp,year);
                            fpSpread1.Sheets[7].SetValue(5,2, ycl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(5,2,0);
                         if (ec!=null&&gdp!=null)
                        {
                            double ecl=Gethistroyvalue<Ps_History>(ec,year)/Gethistroyvalue<Ps_History>(gdp,year);
                            fpSpread1.Sheets[7].SetValue(6,2, ecl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(6,2,0);
                         if (sc!=null&&gdp!=null)
                        {
                            double scl=Gethistroyvalue<Ps_History>(sc,year)/Gethistroyvalue<Ps_History>(gdp,year);
                            fpSpread1.Sheets[7].SetValue(7,2, scl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(7,2,0);
                       
                        if (cs!=null)
                        {
                            num=Gethistroyvalue<Ps_History>(cs,year)+Gethistroyvalue<Ps_History>(xc,year);
                             fpSpread1.Sheets[7].SetValue(8,2,num);
                            fpSpread1.Sheets[7].SetValue(9,2,Gethistroyvalue<Ps_History>(cs,year));
                        }
                        else
                        {
                           fpSpread1.Sheets[7].SetValue(8,2,0);
                            fpSpread1.Sheets[7].SetValue(9,2,0);
                        }
                        if (gdp!=null&&cs!=null)
                        {
                          double rj=Gethistroyvalue<Ps_History>(gdp,year)/num;
                          fpSpread1.Sheets[7].SetValue(4,2,rj);
                        }
                        else
                             fpSpread1.Sheets[7].SetValue(4,2,0);
                        
                    }
                    //д��2009������
                    colnum++;
                    fpSpread1.Sheets[7].SetValue(2,6,"2009(ʵ��)");
                    fpSpread1.Sheets[7].Cells[3,6].CellType=numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[4, 6].CellType = numberCellTypes1; 
                    fpSpread1.Sheets[7].Cells[5, 6].CellType = percentcelltypes;
                    fpSpread1.Sheets[7].Cells[6, 6].CellType = numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[7, 6].CellType = numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[8, 6].CellType = numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[9, 6].CellType = numberCellTypes1;
                    if (gdp!=null)
                    {
                    fpSpread1.Sheets[7].SetValue(3,6,Gethistroyvalue<Ps_History>(gdp,"y2009")*10000);
                    }
                    else
                        fpSpread1.Sheets[7].SetValue(3,6,0);
                      if (yc!=null&&gdp!=null)
                        {
                            double ycl=Gethistroyvalue<Ps_History>(yc,"y2009")/Gethistroyvalue<Ps_History>(gdp,"y2009");
                            fpSpread1.Sheets[7].SetValue(5,6, ycl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(5,6,0);
                         if (ec!=null&&gdp!=null)
                        {
                            double ecl=Gethistroyvalue<Ps_History>(ec,"y2009")/Gethistroyvalue<Ps_History>(gdp,"y2009");
                            fpSpread1.Sheets[7].SetValue(6,6, ecl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(6,6,0);
                         if (sc!=null&&gdp!=null)
                        {
                            double scl=Gethistroyvalue<Ps_History>(sc,"y2009")/Gethistroyvalue<Ps_History>(gdp,"y2009");
                            fpSpread1.Sheets[7].SetValue(7,6, scl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(7,6,0);
                         num=0;
                        if (cs!=null)
                        {
                            num=Gethistroyvalue<Ps_History>(cs,"y2009")+Gethistroyvalue<Ps_History>(xc,"y2009");
                             fpSpread1.Sheets[7].SetValue(8,6,num);
                            fpSpread1.Sheets[7].SetValue(9,6,Gethistroyvalue<Ps_History>(cs,"y2009"));
                        }
                        else
                        {
                           fpSpread1.Sheets[7].SetValue(8,6,0);
                            fpSpread1.Sheets[7].SetValue(9,6,0);
                        }
                     if (gdp!=null&&cs!=null)
                        {
                          double rj=Gethistroyvalue<Ps_History>(gdp,"y2009")/num;
                          fpSpread1.Sheets[7].SetValue(4,2,rj);
                        }
                        else
                             fpSpread1.Sheets[7].SetValue(4,2,0);

                }
                else if (endyear<2009&&firstyear<2005)
                {
                    double num = 0;
                   for (int i = 0;i <endyear-2005;i++ )
                    {
                        colnum++;
                        fpSpread1.Sheets[7].Columns.Add(2,1);
                        fpSpread1.Sheets[7].Cells[2, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                        fpSpread1.Sheets[7].SetValue(2,2,(endyear-1-i).ToString()+"(ʵ��)");
                        string year="y"+(endyear-1-i).ToString();
                        fpSpread1.Sheets[7].Cells[3,2].CellType=numberCellTypes1;
                        fpSpread1.Sheets[7].Cells[4,2].CellType=numberCellTypes1;
                        fpSpread1.Sheets[7].Cells[5,2].CellType=percentcelltypes;
                         fpSpread1.Sheets[7].Cells[6,2].CellType=percentcelltypes;
                         fpSpread1.Sheets[7].Cells[7,2].CellType=percentcelltypes;
                         fpSpread1.Sheets[7].Cells[8,2].CellType=numberCellTypes1;
                         fpSpread1.Sheets[7].Cells[9,2].CellType=numberCellTypes1;
                         fpSpread1.Sheets[7].Cells[3, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[4, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[5, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[6, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[7, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[8, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                         fpSpread1.Sheets[7].Cells[9, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                       
                        if (gdp!=null)
                        {
                         
                          fpSpread1.Sheets[7].SetValue(3,2, Gethistroyvalue<Ps_History>(gdp,year)*10000);
                        }
                        else
                          fpSpread1.Sheets[7].SetValue(3,2, 0);

                        if (yc!=null&&gdp!=null)
                        {
                            double ycl=Gethistroyvalue<Ps_History>(yc,year)/Gethistroyvalue<Ps_History>(gdp,year);
                            fpSpread1.Sheets[7].SetValue(5,2, ycl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(5,2,0);
                         if (ec!=null&&gdp!=null)
                        {
                            double ecl=Gethistroyvalue<Ps_History>(ec,year)/Gethistroyvalue<Ps_History>(gdp,year);
                            fpSpread1.Sheets[7].SetValue(6,2, ecl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(6,2,0);
                         if (sc!=null&&gdp!=null)
                        {
                            double scl=Gethistroyvalue<Ps_History>(sc,year)/Gethistroyvalue<Ps_History>(gdp,year);
                            fpSpread1.Sheets[7].SetValue(7,2, scl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(7,2,0);
                       
                        if (cs!=null)
                        {
                            num=Gethistroyvalue<Ps_History>(cs,year)+Gethistroyvalue<Ps_History>(xc,year);
                             fpSpread1.Sheets[7].SetValue(8,2,num);
                            fpSpread1.Sheets[7].SetValue(9,2,Gethistroyvalue<Ps_History>(cs,year));
                        }
                        else
                        {
                           fpSpread1.Sheets[7].SetValue(8,2,0);
                            fpSpread1.Sheets[7].SetValue(9,2,0);
                        }
                       if (gdp!=null&&cs!=null)
                       {
                          double rj=Gethistroyvalue<Ps_History>(gdp,year)/num;
                            fpSpread1.Sheets[7].SetValue(4,2,num);
                       }
                       else
                            fpSpread1.Sheets[7].SetValue(4,2,0);
                        
                    }
                    //д��ʵ�����һ������
                    string y="y"+endyear.ToString();
                    fpSpread1.Sheets[7].SetValue(2,2+colnum,(endyear).ToString()+"(ʵ��)");
                
                    fpSpread1.Sheets[7].Cells[3,2+colnum].CellType=numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[4, 2 + colnum].CellType = numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[5, 2 + colnum].CellType = percentcelltypes;
                    fpSpread1.Sheets[7].Cells[6, 2 + colnum].CellType = numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[7, 2 + colnum].CellType = numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[8, 2 + colnum].CellType = numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[9, 2 + colnum].CellType = numberCellTypes1;
                    if (gdp!=null)
                    {
                    fpSpread1.Sheets[7].SetValue(3,2+colnum,Gethistroyvalue<Ps_History>(gdp,y)*10000);
                    }
                    else
                        fpSpread1.Sheets[7].SetValue(3,2+colnum,0);
                      if (yc!=null&&gdp!=null)
                        {
                            double ycl=Gethistroyvalue<Ps_History>(yc,y)/Gethistroyvalue<Ps_History>(gdp,y);
                            fpSpread1.Sheets[7].SetValue(5,2+colnum, ycl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(5,2+colnum,0);
                         if (ec!=null&&gdp!=null)
                        {
                            double ecl=Gethistroyvalue<Ps_History>(ec,y)/Gethistroyvalue<Ps_History>(gdp,y);
                            fpSpread1.Sheets[7].SetValue(6,2+colnum, ecl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(6,2+colnum,0);
                         if (sc!=null&&gdp!=null)
                        {
                            double scl=Gethistroyvalue<Ps_History>(sc,y)/Gethistroyvalue<Ps_History>(gdp,y);
                            fpSpread1.Sheets[7].SetValue(7,2+colnum, scl);
                        }
                        else
                            fpSpread1.Sheets[7].SetValue(7,2+colnum,0);
                        num=0;
                        if (cs!=null)
                        {
                            num=Gethistroyvalue<Ps_History>(cs,y)+Gethistroyvalue<Ps_History>(xc,y);
                             fpSpread1.Sheets[7].SetValue(8,2+colnum,num);
                            fpSpread1.Sheets[7].SetValue(9,2+colnum,Gethistroyvalue<Ps_History>(cs,y));
                        }
                        else
                        {
                           fpSpread1.Sheets[7].SetValue(8,2+colnum,0);
                            fpSpread1.Sheets[7].SetValue(9,2+colnum,0);
                        }
                     if (gdp!=null&&cs!=null)
                       {
                          double rj=Gethistroyvalue<Ps_History>(gdp,y)/num;
                            fpSpread1.Sheets[7].SetValue(4,2+colnum,num);
                       }
                       else
                            fpSpread1.Sheets[7].SetValue(4,2+colnum,0);
                        colnum++;
                }
              
                    fpSpread1.Sheets[7].Cells[3,colnum+2].CellType=numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[3,colnum+3].CellType=numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[3,colnum+4].CellType=numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[3,colnum+5].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[3,colnum+6].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[3,colnum+7].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[4,colnum+2].CellType=numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[4,colnum+3].CellType=numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[4,colnum+4].CellType=numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[4,colnum+5].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[4,colnum+6].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[4,colnum+7].CellType=percentcelltypes;
                    for (int i=0;i<3;i++)
                    {
                     fpSpread1.Sheets[7].Cells[5+i,colnum+2].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[5+i,colnum+3].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[5+i,colnum+4].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[5+i,colnum+5].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[5+i,colnum+6].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[5+i,colnum+7].CellType=percentcelltypes;
                    }
                  for (int i=0;i<2;i++)
                {
                     fpSpread1.Sheets[7].Cells[8+i,colnum+2].CellType=numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[8+i,colnum+3].CellType=numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[8+i,colnum+4].CellType=numberCellTypes1;
                    fpSpread1.Sheets[7].Cells[8+i,colnum+5].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[8+i,colnum+6].CellType=percentcelltypes;
                    fpSpread1.Sheets[7].Cells[8+i,colnum+7].CellType=percentcelltypes;
                }

                if (psgdp.Count!=0)
                {
                    fpSpread1.Sheets[7].SetValue(3,colnum+2,Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2010"));
                     fpSpread1.Sheets[7].SetValue(3,colnum+3,Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2015"));
                     fpSpread1.Sheets[7].SetValue(3,colnum+4,Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2020"));
                    fpSpread1.Sheets[7].Cells[3,colnum+5].Formula= "POWER(R4C"+(colnum+3).ToString()+"/R4C3,0.2)-1";
                     fpSpread1.Sheets[7].Cells[3,colnum+6].Formula= "POWER(R4C"+(colnum+4).ToString()+"/R4C"+(colnum+3).ToString()+",0.2)-1";
                     fpSpread1.Sheets[7].Cells[3,colnum+7].Formula= "POWER(R4C"+(colnum+5).ToString()+"/R4C"+(colnum+4).ToString()+",0.2)-1";
                }
                 if (psyc.Count!=0)
                 {
                    fpSpread1.Sheets[7].SetValue(5,colnum+2,Gethistroyvalue<Ps_Forecast_Math>(psyc[0],"y2010")/Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2010"));
                     fpSpread1.Sheets[7].SetValue(5,colnum+3,Gethistroyvalue<Ps_Forecast_Math>(psyc[0],"y2015")/Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2015"));
                     fpSpread1.Sheets[7].SetValue(5,colnum+4,Gethistroyvalue<Ps_Forecast_Math>(psyc[0],"y2020")/Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2020"));
                    fpSpread1.Sheets[7].Cells[5,colnum+5].Formula= "POWER(R6C"+(colnum+3).ToString()+"/R6C3,0.2)-1";
                     fpSpread1.Sheets[7].Cells[5,colnum+6].Formula= "POWER(R6C"+(colnum+4).ToString()+"/R6C"+(colnum+3).ToString()+",0.2)-1";
                     fpSpread1.Sheets[7].Cells[5,colnum+7].Formula= "POWER(R6C"+(colnum+5).ToString()+"/R6C"+(colnum+4).ToString()+",0.2)-1";
                 }
                    
                 if (psec.Count!=0)
                 {
                    fpSpread1.Sheets[7].SetValue(6,colnum+2,Gethistroyvalue<Ps_Forecast_Math>(psec[0],"y2010")/Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2010"));
                     fpSpread1.Sheets[7].SetValue(6,colnum+3,Gethistroyvalue<Ps_Forecast_Math>(psec[0],"y2015")/Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2015"));
                     fpSpread1.Sheets[7].SetValue(6,colnum+4,Gethistroyvalue<Ps_Forecast_Math>(psec[0],"y2020")/Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2020"));
                    fpSpread1.Sheets[7].Cells[6,colnum+5].Formula= "POWER(R7C"+(colnum+3).ToString()+"/R7C3,0.2)-1";
                     fpSpread1.Sheets[7].Cells[6,colnum+6].Formula= "POWER(R7C"+(colnum+4).ToString()+"/R7C"+(colnum+3).ToString()+",0.2)-1";
                     fpSpread1.Sheets[7].Cells[6,colnum+7].Formula= "POWER(R7C"+(colnum+5).ToString()+"/R7C"+(colnum+4).ToString()+",0.2)-1";
                 }
                if (pssc.Count!=0)
                {
                  fpSpread1.Sheets[7].SetValue(7,colnum+2,Gethistroyvalue<Ps_Forecast_Math>(pssc[0],"y2010")/Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2010"));
                     fpSpread1.Sheets[7].SetValue(7,colnum+3,Gethistroyvalue<Ps_Forecast_Math>(pssc[0],"y2015")/Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2015"));
                     fpSpread1.Sheets[7].SetValue(7,colnum+4,Gethistroyvalue<Ps_Forecast_Math>(pssc[0],"y2020")/Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2020"));
                    fpSpread1.Sheets[7].Cells[7,colnum+5].Formula= "POWER(R8C"+(colnum+3).ToString()+"/R8C3,0.2)-1";
                     fpSpread1.Sheets[7].Cells[7,colnum+6].Formula= "POWER(R8C"+(colnum+4).ToString()+"/R8C"+(colnum+3).ToString()+",0.2)-1";
                     fpSpread1.Sheets[7].Cells[7,colnum+7].Formula= "POWER(R8C"+(colnum+5).ToString()+"/R8C"+(colnum+4).ToString()+",0.2)-1";
                }
                 if (pscity.Count!=0)
                 {
                       fpSpread1.Sheets[7].SetValue(8,colnum+2,Gethistroyvalue<Ps_Forecast_Math>(pscity[0],"y2010")+Gethistroyvalue<Ps_Forecast_Math>(pscountry[0],"y2010"));
                     fpSpread1.Sheets[7].SetValue(8,colnum+3,Gethistroyvalue<Ps_Forecast_Math>(pscity[0],"y2015")+Gethistroyvalue<Ps_Forecast_Math>(pscountry[0],"y2015"));
                     fpSpread1.Sheets[7].SetValue(8,colnum+4,Gethistroyvalue<Ps_Forecast_Math>(pscity[0],"y2020")+Gethistroyvalue<Ps_Forecast_Math>(pscountry[0],"y2020"));
                     fpSpread1.Sheets[7].Cells[8,colnum+5].Formula= "POWER(R9C"+(colnum+3).ToString()+"/R9C3,0.2)-1";
                     fpSpread1.Sheets[7].Cells[8,colnum+6].Formula= "POWER(R9C"+(colnum+4).ToString()+"/R9C"+(colnum+3).ToString()+",0.2)-1";
                     fpSpread1.Sheets[7].Cells[8,colnum+7].Formula= "POWER(R9C"+(colnum+5).ToString()+"/R9C"+(colnum+4).ToString()+",0.2)-1";

                      fpSpread1.Sheets[7].SetValue(9,colnum+2,Gethistroyvalue<Ps_Forecast_Math>(pscity[0],"y2010"));
                     fpSpread1.Sheets[7].SetValue(9,colnum+3,Gethistroyvalue<Ps_Forecast_Math>(pscity[0],"y2015"));
                     fpSpread1.Sheets[7].SetValue(9,colnum+4,Gethistroyvalue<Ps_Forecast_Math>(pscity[0],"y2020"));
                     fpSpread1.Sheets[7].Cells[9,colnum+5].Formula= "POWER(R10C"+(colnum+3).ToString()+"/R10C3,0.2)-1";
                     fpSpread1.Sheets[7].Cells[9,colnum+6].Formula= "POWER(R10C"+(colnum+4).ToString()+"/R10C"+(colnum+3).ToString()+",0.2)-1";
                     fpSpread1.Sheets[7].Cells[9,colnum+7].Formula= "POWER(R10C"+(colnum+5).ToString()+"/R10C"+(colnum+4).ToString()+",0.2)-1";
                 }
                if (psgdp.Count!=0&&pscity.Count!=0)
                {
                      fpSpread1.Sheets[7].SetValue(4,colnum+2,Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2010")/(Gethistroyvalue<Ps_Forecast_Math>(pscountry[0],"y2010")+Gethistroyvalue<Ps_Forecast_Math>(pscity[0],"y2010")));
                     fpSpread1.Sheets[7].SetValue(4,colnum+3,Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2015")/(Gethistroyvalue<Ps_Forecast_Math>(pscountry[0],"y2015")+Gethistroyvalue<Ps_Forecast_Math>(pscity[0],"y2015")));
                     fpSpread1.Sheets[7].SetValue(4,colnum+4,Gethistroyvalue<Ps_Forecast_Math>(psgdp[0],"y2020")/(Gethistroyvalue<Ps_Forecast_Math>(pscountry[0],"y2020")+Gethistroyvalue<Ps_Forecast_Math>(pscity[0],"y2020")));
                     fpSpread1.Sheets[7].Cells[4,colnum+5].Formula= "POWER(R5C"+(colnum+3).ToString()+"/R5C3,0.2)-1";
                     fpSpread1.Sheets[7].Cells[4,colnum+6].Formula= "POWER(R5C"+(colnum+4).ToString()+"/R5C"+(colnum+3).ToString()+",0.2)-1";
                     fpSpread1.Sheets[7].Cells[4,colnum+7].Formula= "POWER(R5C"+(colnum+5).ToString()+"/R5C"+(colnum+4).ToString()+",0.2)-1";
                }
                   
                
            }
            fpSpread1.Sheets[7].Cells[0, 0].ColumnSpan = colnum + 8;
            fpSpread1.Sheets[7].Cells[1, 0].ColumnSpan = colnum + 8;
            Sheet_GridandColor(fpSpread1.Sheets[7], 10, colnum + 8);
        }
        /// <summary>
        /// �������÷�չԤ���ĳ�ʼ���ʽ
        /// </summary>
        private void jjfzyctable()
        {
            fpSpread1.Sheets[7].RowCount = 0;
            fpSpread1.Sheets[7].ColumnCount = 0;
            fpSpread1.Sheets[7].RowCount = 10;
            fpSpread1.Sheets[7].ColumnCount = 9;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                   
                    //ˮƽ�ʹ�ֱ�����ж���
                    fpSpread1.Sheets[7].Cells[i, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[7].Cells[i, j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }
            }
            fpSpread1.Sheets[7].SetValue(0, 0, "���ýṹ��չԤ������");
            fpSpread1.Sheets[7].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[7].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[7].SetValue(1, 0, "��λ����Ԫ��Ԫ/�ˣ�%������");
           // fpSpread1.Sheets[7].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[7].Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[7].SetValue(2, 0, "���");
            fpSpread1.Sheets[7].SetValue(2, 1, "ָ������");
            fpSpread1.Sheets[7].SetValue(2, 3, "2010");
            fpSpread1.Sheets[7].SetValue(2, 4, "2015");
            fpSpread1.Sheets[7].SetValue(2, 5, "2020");
            fpSpread1.Sheets[7].SetValue(2, 6, "ʮһ��");
            fpSpread1.Sheets[7].SetValue(2, 7, "ʮ����");
            fpSpread1.Sheets[7].SetValue(2, 8, "ʮ����");
            for (int i = 0; i < 7;i++ )
            {
                fpSpread1.Sheets[7].SetValue(3 + i, 0, (i+1).ToString());
            }
            fpSpread1.Sheets[7].SetValue(3, 1, "ȫ��������ֵ");
            fpSpread1.Sheets[7].SetValue(4,1, "�˾�GDP");
            fpSpread1.Sheets[7].SetValue(5, 1, "��һ��ҵ����");
            fpSpread1.Sheets[7].SetValue(6, 1, "�ڶ���ҵ����");
            fpSpread1.Sheets[7].SetValue(7, 1, "������ҵ����");
            fpSpread1.Sheets[7].SetValue(8, 1, "ȫ�����˿ڣ����ˣ�");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[7], 1, "ȫ�����˿ڣ����ˣ�");
            fpSpread1.Sheets[7].SetValue(9, 1, "�����˿ڣ����ˣ�");


        }
        /// <summary>
        /// �������������Ԥ���
    
        /// </summary>
        private void  xdlycb()
        {
            fpSpread1.Sheets[8].RowCount = 0;
            fpSpread1.Sheets[8].ColumnCount = 0;
            fpSpread1.Sheets[8].RowCount = 4;
            fpSpread1.Sheets[8].ColumnCount = 11;
            fpSpread1.Sheets[8].SetValue(0, 0, "�����������Ԥ���");
            fpSpread1.Sheets[8].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[8].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[8].Cells[0, 0].ColumnSpan = 11;
            fpSpread1.Sheets[8].SetValue(1, 0, "��λ����ǧ��ʱ");
           
            fpSpread1.Sheets[8].Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[8].Cells[1, 0].ColumnSpan = 11;
            fpSpread1.Sheets[8].Rows[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[8].Cells[2, 0].ColumnSpan = 2;
            for (int i = 0; i < 9;i++ )
            {
                fpSpread1.Sheets[8].Columns[2 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
            for (int i = 0; i < 6;i++ )
            {
                fpSpread1.Sheets[8].SetValue(2, 2+i, (2010+i).ToString());
            }
            fpSpread1.Sheets[8].Columns[8].CellType = percentcelltypes;
            fpSpread1.Sheets[8].Cells[2,8].CellType=texttype;

            fpSpread1.Sheets[8].SetValue(2, 8, "���������");
            fpSpread1.Sheets[8].SetValue(2, 9, "2020");
            fpSpread1.Sheets[8].Columns[10].CellType = percentcelltypes;
            fpSpread1.Sheets[8].Cells[2,10].CellType = texttype;
            fpSpread1.Sheets[8].SetValue(2,10, "���������");

           // Ps_forecast_list report = fqreporttiltle;
            //string con = "UserID='" + ProjectUID + "'AND Col1='1'AND Title='" +fqreporttiltle + "'";
            //report=(Ps_forecast_list)Services.BaseService.GetObject("SelectPs_forecast_listByWhere",con);
          
            int sumrow=4;
            int intrownum=0;
            if (fqreporttiltle == null)
            {
                MessageBox.Show("����ѡ��ĸ���Ԥ�ⷽ�������ڣ�");
                return;
            }
            
            else 
            {
                intrownum=4;
                sumrow += Create_xdltableAnddata(fpSpread1.Sheets[8], intrownum, "�߷���", fqreporttiltle, 1);
              intrownum = sumrow;
              sumrow += Create_xdltableAnddata(fpSpread1.Sheets[8], intrownum, "�з���", fqreporttiltle, 2);
              intrownum = sumrow;
              sumrow += Create_xdltableAnddata(fpSpread1.Sheets[8], intrownum, "�ͷ���", fqreporttiltle, 3);

            }
            Sheet_GridandColor(fpSpread1.Sheets[8], sumrow, 11);
           
        }
        /// ����������󸺺�Ԥ���

        /// </summary>
        private void zdfycb()
        {
            fpSpread1.Sheets[9].RowCount = 0;
            fpSpread1.Sheets[9].ColumnCount = 0;
            fpSpread1.Sheets[9].RowCount = 4;
            fpSpread1.Sheets[9].ColumnCount = 11;
            fpSpread1.Sheets[9].Columns[1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            for (int i = 0; i < 9;i++ )
            {
                fpSpread1.Sheets[9].Columns[2 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
           
            fpSpread1.Sheets[9].SetValue(0, 0, "��������󸺺�Ԥ���");
            fpSpread1.Sheets[9].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[9].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[9].Cells[0, 0].ColumnSpan = 11;
            fpSpread1.Sheets[9].SetValue(1, 0, "��λ����ǧ��ʱ");

            fpSpread1.Sheets[9].Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[9].Cells[1, 0].ColumnSpan = 11;
            fpSpread1.Sheets[9].Rows[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[9].Cells[2, 0].ColumnSpan = 2;
            for (int i = 0; i < 6; i++)
            {
                fpSpread1.Sheets[9].SetValue(2, 2 + i, (2010 + i).ToString());
            }
            fpSpread1.Sheets[9].Columns[8].CellType = percentcelltypes;
            fpSpread1.Sheets[9].Cells[2, 8].CellType = texttype;
            fpSpread1.Sheets[9].SetValue(2, 8, "���������");
            fpSpread1.Sheets[9].Columns[10].CellType = percentcelltypes;

            fpSpread1.Sheets[9].SetValue(2, 9, "2020");
            fpSpread1.Sheets[9].Cells[2, 10].CellType = texttype;
            fpSpread1.Sheets[9].SetValue(2, 10, "���������");

            //Ps_forecast_list report = zdfhreporttiltle;
            //string con = "UserID='" + ProjectUID + "'AND Col1='1'AND Title='" + zdfhreporttiltle + "'";
            //report = (Ps_forecast_list)Services.BaseService.GetObject("SelectPs_forecast_listByWhere", con);

            int sumrow = 4;
            int intrownum = 0;
            if (zdfhreporttiltle == null)
            {
                MessageBox.Show("����ѡ��ĸ���Ԥ�ⷽ�������ڣ�");
                return;
            }

            else
            {
                intrownum = 4;
                sumrow += Create_zdfhtableAnddata(fpSpread1.Sheets[9], intrownum, "�߷���", zdfhreporttiltle, 1);
                intrownum = sumrow;
                sumrow += Create_zdfhtableAnddata(fpSpread1.Sheets[9], intrownum, "�з���", zdfhreporttiltle, 2);
                intrownum = sumrow;
                sumrow += Create_zdfhtableAnddata(fpSpread1.Sheets[9], intrownum, "�ͷ���", zdfhreporttiltle, 3);

            }
            Sheet_GridandColor(fpSpread1.Sheets[9], sumrow, 11);

        }

        //���罨����Ŀ��ϸ��
        
        private void jsxmmxb()
        {
#region ������ͷ
            fpSpread1.Sheets[15].RowCount = 0;
            fpSpread1.Sheets[15].ColumnCount = 0;
            fpSpread1.Sheets[15].RowCount = 4;
            fpSpread1.Sheets[15].ColumnCount = 7;
            fpSpread1.Sheets[15].SetValue(0, 0, "��ʮ���塱�滮���罨����Ŀ��ϸ��");
            fpSpread1.Sheets[15].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[15].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[15].Cells[0, 0].ColumnSpan = 7;
            fpSpread1.Sheets[15].SetValue(1, 0, "��λ����ǧ����������");

            fpSpread1.Sheets[15].Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[15].Cells[1, 0].ColumnSpan = 7;
            fpSpread1.Sheets[15].Rows[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
           
            fpSpread1.Sheets[15].SetValue(2, 1, "̨��");
            fpSpread1.Sheets[15].SetValue(2, 2, "����");
            fpSpread1.Sheets[15].SetValue(2, 3, "����");
            fpSpread1.Sheets[15].SetValue(2, 4, "�ͺ�");
            fpSpread1.Sheets[15].SetValue(2, 5, "�ƻ�����ʱ��");
           
            fpSpread1.Sheets[15].SetValue(2, 6, "Ԥ��Ͷ��ʱ��");
           
#endregion
            #region ���ݵ�ѹ�ȼ���ȡ���� ��·�ͱ��վ���� �������������
            IList<Ps_Table_TZMX> list1000s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list1000l = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list750s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list750l = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list500s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list500l = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list330s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list330l = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list220s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list220l = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list110s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list110l = new List<Ps_Table_TZMX>();
            //��������ͳ��
            IList<Ps_Table_TZGS> listcity66sl = new List<Ps_Table_TZGS>();
         

            IList<Ps_Table_TZGS> listcity35sl = new List<Ps_Table_TZGS>();
           

            IList<Ps_Table_TZGS> listcity10sl = new List<Ps_Table_TZGS>();

            IList<Ps_Table_TZGS> listcity6sl = new List<Ps_Table_TZGS>();


            IList<Ps_Table_TZGS> listcity3sl = new List<Ps_Table_TZGS>();

            string con= "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '"+ProjectUID+"') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '1000') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list1000l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '"+ProjectUID+"') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '1000') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list1000s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '750') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list750l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '750') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list750s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);

            con = "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '500') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list500l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '500') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list500s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);

            con = "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '330') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list330l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '330') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list330s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);

            con = "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '220') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list220l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '220') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list220s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);

            con = "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '110') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list110l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '110') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list110s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);

            con = "and  a.projectID='" + ProjectUID + "'and a.DQ='��Ͻ������' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='66'";
            listcity66sl = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);

            con = "and  a.projectID='" + ProjectUID + "'and a.DQ='��Ͻ������' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='35'";
            listcity35sl = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);

            con = "and  a.projectID='" + ProjectUID + "'and a.DQ='��Ͻ������' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='10'";
            listcity10sl = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);

            con = "and  a.projectID='" + ProjectUID + "'and a.DQ='��Ͻ������' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='6'";
            listcity6sl = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);


            con = "and  a.projectID='" + ProjectUID + "'and a.DQ='��Ͻ������' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='3'";
            listcity3sl = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);

            
           
          

           
            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='3'AND DQ='ũ��'  ORDER BY LineLength";
            // listcountry3l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            //�Ȼ�ȡ�������ơ����������ũ������������·�ͱ��վ��������������н���ͳ��
            Dictionary<string, IList<Ps_Table_TZGS>> listcountry66sl = new Dictionary<string, IList<Ps_Table_TZGS>>();

            Dictionary<string, IList<Ps_Table_TZGS>> listcountry35sl = new Dictionary<string, IList<Ps_Table_TZGS>>();


            Dictionary<string, IList<Ps_Table_TZGS>> listcountry10sl = new Dictionary<string, IList<Ps_Table_TZGS>>();

            Dictionary<string, IList<Ps_Table_TZGS>> listcountry6sl = new Dictionary<string, IList<Ps_Table_TZGS>>();

            Dictionary<string, IList<Ps_Table_TZGS>> listcountry3sl = new Dictionary<string, IList<Ps_Table_TZGS>>();
      
            bool flag66l = false,  flag35l = false, flag35s = false, flag10l = false, flag10s = false, flag6l = false, flag6s = false, flag3l = false, flag3s = false;
            string conn = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);

            foreach (PS_Table_AreaWH area in list)
            {
                con = "and a.ProjectID='" + ProjectUID + "'AND a.AreaName='" + area.Title + "'AND substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='66'AND a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
                IList<Ps_Table_TZGS> list1 = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);
                if (list1.Count != 0)
                {
                    flag66l = true;
                    listcountry66sl.Add(area.Title, list1);
                }


                con = "and a.ProjectID='" + ProjectUID + "'AND a.AreaName='" + area.Title + "'AND substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='35'AND a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
                list1 = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);
                if (list1.Count != 0)
                {
                    flag35l = true;
                    listcountry35sl.Add(area.Title, list1);
                }

                con = "and a.ProjectID='" + ProjectUID + "'AND a.AreaName='" + area.Title + "'AND substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='10'AND a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
                list1 = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);
                if (list1.Count != 0)
                {
                    flag10l = true;
                    listcountry10sl.Add(area.Title, list1);
                }

                con = "and a.ProjectID='" + ProjectUID + "'AND a.AreaName='" + area.Title + "'AND substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='6'AND a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
                list1 = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);
                if (list1.Count != 0)
                {
                    flag6l = true;
                    listcountry6sl.Add(area.Title, list1);
                }



                con = "and a.ProjectID='" + ProjectUID + "'AND a.AreaName='" + area.Title + "'AND substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='3'AND a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
                list1 = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);
                if (list1.Count != 0)
                {
                    flag3l = true;
                    listcountry3sl.Add(area.Title, list1);
                }

                // repositoryItemComboBox3.Items.Add(area.Title);
            }

            #endregion
            #region ������������к���ӵ�Ԫ��ʽ
            List<string> title = new List<string>();
            int list1000i = 0;
            //1000kv����
            if (list1000s.Count != 0 || list1000l.Count != 0)
            {
                list1000i = 1;
                fpSpread1.Sheets[15].Rows.Add(3, 1);
                fpSpread1.Sheets[15].SetValue(3, 0, "1000KV����");
                fpSpread1.Sheets[15].Rows[3].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list1000l.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4, 1);
                    fpSpread1.Sheets[15].SetValue(4, 0, list1000l[list1000l.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4, 1, "X");
                    fpSpread1.Sheets[15].SetValue(4, 2, "X");
                    fpSpread1.Sheets[15].SetValue(4, 3, list1000l[list1000l.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4, 4, list1000l[list1000l.Count - 1 - i].Linetype);
                    fpSpread1.Sheets[15].SetValue(4, 5, list1000l[list1000l.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4, 6, list1000l[list1000l.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].Rows[4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4,3].CellType = numberCellTypes1;
                    list1000i++;
                }
                fpSpread1.Sheets[15].Cells[3, 3].Formula = "SUM(R5C4:R" + (5 + list1000l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list1000s.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4 + list1000l.Count, 1);
                    fpSpread1.Sheets[15].SetValue(4 + list1000l.Count, 0, list1000s[list1000s.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4 + list1000l.Count, 1, list1000s[list1000s.Count - 1 - i].Num);
                    fpSpread1.Sheets[15].SetValue(4 + list1000l.Count, 2, list1000s[list1000s.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4 + list1000l.Count, 3, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000l.Count, 4, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000l.Count, 5, list1000s[list1000s.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4 + list1000l.Count, 6, list1000s[list1000s.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].Rows[4 + list1000l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4 + list1000l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[15].Cells[4 + list1000l.Count, 2].CellType = numberCellTypes1;
                    list1000i++;
                }
                fpSpread1.Sheets[15].Cells[3, 1].Formula = "SUM(R" + (5 + list1000l.Count).ToString() + "C2:R" + (5 + list1000l.Count + list1000s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[15].Cells[3, 2].Formula = "SUM(R" + (5 + list1000l.Count).ToString() + "C3:R" + (5 + list1000l.Count + list1000s.Count - 1).ToString() + "C3)";
            }
            //750KV����
            int list750i = 0;
            if (list750s.Count != 0 || list750l.Count != 0)
            {
                list750i = 1;
                fpSpread1.Sheets[15].Rows.Add(3 + list1000i, 1);
                fpSpread1.Sheets[15].SetValue(3 + list1000i, 0, "750KV����");
                fpSpread1.Sheets[15].Rows[3 + list1000i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + list1000i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list750l.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4 + list1000i, 1);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i, 0, list750l[list750l.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i, 1, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i, 2, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i, 3, list750l[list750l.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i, 4, list750l[list750l.Count - 1 - i].Linetype);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i, 5, list750l[list750l.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i, 6, list750l[list750l.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].Rows[4 + list1000i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4 + list1000i, 4].CellType = numberCellTypes1;
                    list750i++;
                }
                fpSpread1.Sheets[15].Cells[3 + list1000i, 3].Formula = "SUM(R" + (5 + list1000i).ToString() + "C4:R" + (5 + list1000i + list750l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list750s.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750l.Count, 1);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750l.Count, 0, list750s[list750s.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750l.Count, 1, list750s[list750s.Count - 1 - i].Num);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750l.Count, 2, list750s[list750s.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750l.Count, 3, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750l.Count, 4, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750l.Count, 5, list750s[list750s.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750l.Count, 6, list750s[list750s.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].Rows[4 + list1000i+list750l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750l.Count, 2].CellType = numberCellTypes1;
                    list750i++;
                }
                fpSpread1.Sheets[15].Cells[3 + list1000i, 1].Formula = "SUM(R" + (5 + list1000i + list750l.Count).ToString() + "C2:R" + (5 + list1000i + list750l.Count + list750s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[15].Cells[3 + list1000i, 2].Formula = "SUM(R" + (5 + list1000i + list750l.Count).ToString() + "C3:R" + (5 + list1000i + list750l.Count + list750s.Count - 1).ToString() + "C3)";
            }
            //500KV����
            int list500i = 0;
            if (list500l.Count != 0 || list500s.Count != 0)
            {
                list500i = 1;
                fpSpread1.Sheets[15].Rows.Add(3 + list1000i + list750i, 1);
                fpSpread1.Sheets[15].SetValue(3 + list1000i + list750i, 0, "500KV����");
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list500l.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i, 1);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i, 0, list500l[list500l.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i, 1, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i, 2, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i, 3, list500l[list500l.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i, 4, list500l[list500l.Count - 1 - i].Linetype);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i, 5, list500l[list500l.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i, 6, list500l[list500l.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].Rows[4 + list1000i+list750i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i, 4].CellType = numberCellTypes1;
                    list500i++;
                }
                fpSpread1.Sheets[15].Cells[3 + list1000i+list750i, 3].Formula = "SUM(R" + (5 + list1000i+list750i).ToString() + "C4:R" + (5 + list1000i+list750i + list500l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list500s.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500l.Count, 1);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500l.Count, 0, list500s[list500s.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500l.Count, 1, list500s[list500s.Count - 1 - i].Num);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500l.Count, 2, list500s[list500s.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500l.Count, 3, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500l.Count, 4, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500l.Count, 5, list500s[list500s.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500l.Count, 6, list500s[list500s.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i + list500l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i + list500l.Count, 2].CellType = numberCellTypes1;
                    list500i++;
                }
                fpSpread1.Sheets[15].Cells[3 + list1000i + list750i, 1].Formula = "SUM(R" + (5 + list1000i + list750i + list500l.Count).ToString() + "C2:R" + (5 + list1000i + list750i + list500l.Count + list500s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[15].Cells[3 + list1000i + list750i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500l.Count).ToString() + "C3:R" + (5 + list1000i + list750i + list500l.Count + list500s.Count - 1).ToString() + "C3)";
            }
            //330KV����
            int list330i = 0;
            if (list330l.Count != 0 || list330s.Count != 0)
            {
                list330i = 1;
                fpSpread1.Sheets[15].Rows.Add(3 + list1000i + list750i + list500i, 1);
                fpSpread1.Sheets[15].SetValue(3 + list1000i + list750i + list500i, 0, "330KV����");
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list330l.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500i, 1);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i, 0, list330l[list330l.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i, 1, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i, 2, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i, 3, list330l[list330l.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i, 4, list330l[list330l.Count - 1 - i].Linetype);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i, 5, list330l[list330l.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i, 6, list330l[list330l.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i + list500i, 4].CellType = numberCellTypes1;
                    list330i++;
                }
                fpSpread1.Sheets[15].Cells[3 + list1000i + list750i+list500i, 3].Formula = "SUM(R" + (5 + list1000i + list750i+list500i).ToString() + "C4:R" + (5 + list1000i + list750i +list500i+ list330l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list330s.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500i + list330l.Count, 1);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 0, list330s[list330s.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 1, list330s[list330s.Count - 1 - i].Num);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 2, list330s[list330s.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 3, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 4, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 5, list330s[list330s.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 6, list330s[list330s.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i + list330l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i + list500i + list330l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i + list500i + list330l.Count, 2].CellType = numberCellTypes1;
                    list330i++;
                }
                fpSpread1.Sheets[15].Cells[3 + list1000i + list750i + list500i, 1].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330l.Count).ToString() + "C2:R" + (5 + list1000i + list750i + list500i + list330l.Count + list330s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[15].Cells[3 + list1000i + list750i + list500i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330l.Count).ToString() + "C3:R" + (5 + list1000i + list750i + list500i + list330l.Count + list330s.Count - 1).ToString() + "C3)";
            }
            //220KV����
            int list220i = 0;
            if (list220l.Count != 0 || list220s.Count != 0)
            {
                list220i = 1;
                fpSpread1.Sheets[15].Rows.Add(3 + list1000i + list750i + list500i + list330i, 1);
                fpSpread1.Sheets[15].SetValue(3 + list1000i + list750i + list500i + list330i, 0, "220KV����");
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list220l.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500i + list330i, 1);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i, 0, list220l[list220l.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i, 1, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i, 2, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i, 3, list220l[list220l.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i, 4, list220l[list220l.Count - 1 - i].Linetype);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i,5, list220l[list220l.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i, 6, list220l[list220l.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i + list330i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i + list500i + list330i, 4].CellType = numberCellTypes1;
                    list220i++;
                }
                fpSpread1.Sheets[15].Cells[3 + list1000i + list750i + list500i+list330i, 3].Formula = "SUM(R" + (5 + list1000i + list750i + list500i+list330i).ToString() + "C4:R" + (5 + list1000i + list750i + list500i +list330i+ list220l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list220s.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220l.Count, 1);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220l.Count, 0, list220s[list220s.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220l.Count, 1, list220s[list220s.Count - 1 - i].Num);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220l.Count, 2, list220s[list220s.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220l.Count, 3, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220l.Count, 4, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220l.Count, 5, list220s[list220s.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220l.Count, 6, list220s[list220s.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220l.Count, 4, "X");
                    fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i + list330i + list220l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i + list500i + list330i + list220l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i + list500i + list330i + list220l.Count, 2].CellType = numberCellTypes1;
                    list220i++;
                }
                fpSpread1.Sheets[15].Cells[3 + list1000i + list750i + list500i + list330i, 1].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220l.Count).ToString() + "C2:R" + (5 + list1000i + list750i + list500i + list330i + list220l.Count + list220s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[15].Cells[3 + list1000i + list750i + list500i + list330i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220l.Count).ToString() + "C3:R" + (5 + list1000i + list750i + list500i + list330i + list220l.Count + list220s.Count - 1).ToString() + "C3)";

            }
            //110KV����
            int list110i = 0;
            if (list110l.Count != 0 || list110s.Count != 0)
            {
                list110i = 1;
                fpSpread1.Sheets[15].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i, 1);
                fpSpread1.Sheets[15].SetValue(3 + list1000i + list750i + list500i + list330i + list220i, 0, "110KV����");
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list110l.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i, 1);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i, 0, list110l[list110l.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i, 1, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i, 2, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i, 3, list110l[list110l.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i, 4, list110l[list110l.Count - 1 - i].Linetype);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i, 5, list110l[list110l.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i, 6, list110l[list110l.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i + list330i+list220i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i + list500i + list330i + list220i, 4].CellType = numberCellTypes1;
                    list110i++;
                }
                fpSpread1.Sheets[15].Cells[3 + list1000i + list750i + list500i + list330i, 3].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i+list220i).ToString() + "C4:R" + (5 + list1000i + list750i + list500i + list330i+list220i + list110l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list110s.Count; i++)
                {
                    fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 1);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 0, list110s[list110s.Count - 1 - i].Title);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 1, list110s[list110s.Count - 1 - i].Num);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 2, list110s[list110s.Count - 1 - i].Vol*100);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 3, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 4, "X");
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 5, list110s[list110s.Count - 1 - i].BuildYear);
                    fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 6, list110s[list110s.Count - 1 - i].BuildEnd);
                    fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i + list330i+list220i+list110l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[15].Cells[4 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 2].CellType = numberCellTypes1;
                    list110i++;
                }
                fpSpread1.Sheets[15].Cells[3 + list1000i + list750i + list500i + list330i + list220i, 1].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count).ToString() + "C2:R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count + list110s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[15].Cells[3 + list1000i + list750i + list500i + list330i + list220i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count).ToString() + "C3:R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count + list110s.Count - 1).ToString() + "C3)";
            }
            #region �������ݵ���� �����������ݷ�Ϊ������ũ��
            //����66KV����
            int listcity66i = 0;
            if (listcity66sl.Count != 0)
            {
                string buildyear="", buildend="";
                double linelenth = 0, subvol = 0; double subnum = 0; //�ֱ�Ϊ��·�ĳ��ȣ����վ�������ͱ��̨��
                double zskg=0;double zspdnum=0;double zspdvol=0; double xsnum=0;double xsvol=0;double kbnum=0;double gylinelength=0;double jslinelength=0,dllinelength=0;
                for (int j = 0; j < listcity66sl.Count;j++ )
                {
                    buildyear = listcity66sl[j].BuildYear;
                    buildend = listcity66sl[j].BuildEd;
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity66sl[j].ID + "' and Col4 = 'pw-line'";
                    Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con= "ProjectID='" + ProjectUID + "'and ParentID='" + listcity66sl[j].ID + "' and Col4 = 'pw-pb'";
                    Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);

                     con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity66sl[j].ID + "' and Col4 = 'pw-kg'";
                     Ps_Table_TZGS kg = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    linelenth += (line.Length + line.Length2);
                    subnum += sub.Num1; subvol += sub.Num2*100; zskg += kg.Num3; zspdnum += sub.Num5; zspdvol += sub.Num6*100; xsnum += sub.Num3; xsvol += sub.Num4*100;
                    kbnum += kg.Num1; jslinelength += line.Length; dllinelength += line.Length2;
                }
                listcity66i = 11;
                fpSpread1.Sheets[15].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[15].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "����66KV����");
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                
                fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[15].SetValue(4+ list1000i + list750i + list500i + list330i + list220i + list110i, 0, "��·");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 1, "X");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 2, "X");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, linelenth);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 4, "");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 6, buildend);
                fpSpread1.Sheets[15].Rows[4+ list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "�����");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 1, subnum);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 2, subvol);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, "X");
                fpSpread1.Sheets[15].SetValue(5+ list1000i + list750i + list500i + list330i + list220i + list110i, 4, "X");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 6, buildend);
                fpSpread1.Sheets[15].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[15].SetValue(6+ list1000i + list750i + list500i + list330i + list220i + list110i, 0, "����66KV����");
                fpSpread1.Sheets[15].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "���Ͽ���");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 1, zskg);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 2, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 4, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 6, buildend);

                fpSpread1.Sheets[15].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "������ѹ��");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 1, zspdnum);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 2, zspdvol);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, "X");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 4, "X");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 6, buildend);
                fpSpread1.Sheets[15].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[15].SetValue(9+ list1000i + list750i + list500i + list330i + list220i + list110i, 0, "��ʽ���վ");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 1, xsnum);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 2, xsvol);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, "X");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 4, "X");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 6, buildend);

                fpSpread1.Sheets[15].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "������");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 1, kbnum);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 2, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 4, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 6, buildend);

                fpSpread1.Sheets[15].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(11 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[15].SetValue(11+ list1000i + list750i + list500i + list330i + list220i + list110i, 0, "������·");
                fpSpread1.Sheets[15].Rows[11+ list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(12+ list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "���У��ܿ���·");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i, 1, "X");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i,2, "X");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, jslinelength);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i, 4, "");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i, 6, buildend);

                fpSpread1.Sheets[15].Rows[12+ list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "      ������·");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 1, "X");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 2, "X");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, dllinelength);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 4, "");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 6, buildend);
                fpSpread1.Sheets[15].Rows[13+ list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //��ֵͳ��
               
                fpSpread1.Sheets[15].Cells[11 + list1000i + list750i + list500i + list330i + list220i + list110i, 4].Formula = "SUM(R" + (13+ list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4:R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4)";
              
            }
            //����35KV����
            int listcity35i = 0;
            if (listcity35sl.Count != 0 )
            {
                string buildyear="", buildend="";
                double linelenth = 0, subvol = 0; double subnum = 0; //�ֱ�Ϊ��·�ĳ��ȣ����վ�������ͱ��̨��
                double zskg = 0; double zspdnum = 0; double zspdvol = 0; double xsnum = 0; double xsvol = 0; double kbnum=0; double gylinelength = 0; double jslinelength = 0, dllinelength = 0;
                for (int j = 0; j < listcity35sl.Count; j++)
                {
                    buildyear = listcity35sl[j].BuildYear;
                    buildend = listcity35sl[j].BuildEd;
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity35sl[j].ID + "' and Col4 = 'pw-line'";
                    Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity35sl[j].ID + "' and Col4 = 'pw-pb'";
                    Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);

                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity35sl[j].ID + "' and Col4 = 'pw-kg'";
                    Ps_Table_TZGS kg = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    linelenth += (line.Length + line.Length2);
                    subnum += sub.Num1; subvol += sub.Num2*100; zskg += kg.Num3; zspdnum += sub.Num5; zspdvol += sub.Num6*100; xsnum += sub.Num3; xsvol += sub.Num4*100;
                    kbnum += kg.Num1; jslinelength += line.Length; dllinelength += line.Length2;
                }
                listcity35i = 11;
       
                fpSpread1.Sheets[15].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i+listcity66i, 1);
                fpSpread1.Sheets[15].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "����35KV����");
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "��·");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1, "X");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2, "X");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, linelenth);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4, "");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 6, buildend);
                fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "�����");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1, subnum);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2, subvol);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, "X");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4, "X");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 6, buildend);
                fpSpread1.Sheets[15].Rows[5+ list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[15].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "����35KV����");
                fpSpread1.Sheets[15].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "���Ͽ���");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1, zskg);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 6, buildend);

                fpSpread1.Sheets[15].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "������ѹ��");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1, zspdnum);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2, zspdvol);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, "X");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4, "X");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 6, buildend);
                fpSpread1.Sheets[15].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "��ʽ���վ");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1, xsnum);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2, xsvol);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, "X");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4, "X");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 6, buildend);

                fpSpread1.Sheets[15].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "������");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1, kbnum);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 6, buildend);

                fpSpread1.Sheets[15].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[15].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "������·");
                fpSpread1.Sheets[15].Rows[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "���У��ܿ���·");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1, "X");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2, "X");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, jslinelength);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4, "");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 6, buildend);

                fpSpread1.Sheets[15].Rows[12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1, "      ������·");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2, "X");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, "X");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4, dllinelength);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 5, "");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 6, buildyear);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 7, buildend);
                fpSpread1.Sheets[15].Rows[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //��ֵͳ��

                fpSpread1.Sheets[15].Cells[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4].Formula = "SUM(R" + (13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C4:R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C4)";
              
            }
            //����10KV����
            int listcity10i = 0;
            if (listcity10sl.Count != 0 )
            {
                string buildyear="", buildend="";
                double linelenth = 0, subvol = 0; double subnum = 0; //�ֱ�Ϊ��·�ĳ��ȣ����վ�������ͱ��̨��
                double zskg = 0; double zspdnum = 0; double zspdvol = 0; double xsnum = 0; double xsvol = 0; double kbnum=0; double gylinelength = 0; double jslinelength = 0, dllinelength = 0;
                for (int j = 0; j < listcity10sl.Count; j++)
                {
                    buildyear = listcity10sl[j].BuildYear;
                    buildend = listcity10sl[j].BuildEd;
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity10sl[j].ID + "' and Col4 = 'pw-line'";
                    Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity10sl[j].ID + "' and Col4 = 'pw-pb'";
                    Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);

                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity10sl[j].ID + "' and Col4 = 'pw-kg'";
                    Ps_Table_TZGS kg = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    linelenth += (line.Length + line.Length2);
                    subnum += sub.Num1; subvol += sub.Num2*100; zskg += kg.Num3; zspdnum += sub.Num5; zspdvol += sub.Num6*100; xsnum += sub.Num3; xsvol += sub.Num4*100;
                    kbnum += kg.Num1; jslinelength += line.Length; dllinelength += line.Length2;
                }
                listcity10i = 11;
                fpSpread1.Sheets[15].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[15].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 0, "����10KV����");
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 0, "��·");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1, "X");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 2, "X");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 3, linelenth);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 4, "");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 6, buildend);
                fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 0, "�����");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1, subnum);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 2, subvol);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 3, "X");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 4, "X");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 6, buildend);
                fpSpread1.Sheets[15].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[15].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 0, "����10KV����");
                fpSpread1.Sheets[15].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 0, "���Ͽ���");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1, zskg);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 2, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 3, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 4, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 6, buildend);

                fpSpread1.Sheets[15].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 0, "������ѹ��");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1, zspdnum);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 2, zspdvol);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 3, "X");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 4, "X");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 6, buildend);
                fpSpread1.Sheets[15].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 0, "��ʽ���վ");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1, xsnum);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 2, xsvol);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 3, "X");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 4, "X");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 6, buildend);

                fpSpread1.Sheets[15].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 0, "������");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1, kbnum);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 2, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 3, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 4, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 6, buildend);

                fpSpread1.Sheets[15].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[15].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 0, "������·");
                fpSpread1.Sheets[15].Rows[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 0, "���У��ܿ���·");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1, "X");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 2, "X");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 3, jslinelength);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 4, "");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 6, buildend);

                fpSpread1.Sheets[15].Rows[12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[15].Rows.Add(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 0, "      ������·");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1, "X");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 2, "X");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 3, dllinelength);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 4, "");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 6, buildend);
                fpSpread1.Sheets[15].Rows[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //��ֵͳ��

                fpSpread1.Sheets[15].Cells[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 4].Formula = "SUM(R" + (13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i).ToString() + "C4:R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i).ToString() + "C4)";
            }
            //����6KV����
            int listcity6i = 0;
            if (listcity6sl.Count != 0 )
            {
                string buildyear="", buildend="";
                double linelenth = 0, subvol = 0; double subnum = 0; //�ֱ�Ϊ��·�ĳ��ȣ����վ�������ͱ��̨��
                double zskg = 0; double zspdnum = 0; double zspdvol = 0; double xsnum = 0; double xsvol = 0; double kbnum=0; double gylinelength = 0; double jslinelength = 0, dllinelength = 0;
                for (int j = 0; j < listcity6sl.Count; j++)
                {
                    buildyear = listcity6sl[j].BuildYear;
                    buildend = listcity6sl[j].BuildEd;
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity6sl[j].ID + "' and Col4 = 'pw-line'";
                    Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity6sl[j].ID + "' and Col4 = 'pw-pb'";
                    Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);

                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity6sl[j].ID + "' and Col4 = 'pw-kg'";
                    Ps_Table_TZGS kg = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    linelenth += (line.Length + line.Length2);
                    subnum += sub.Num1; subvol += sub.Num2*100; zskg += kg.Num3; zspdnum += sub.Num5; zspdvol += sub.Num6*100; xsnum += sub.Num3; xsvol += sub.Num4*100;
                    kbnum += kg.Num1; jslinelength += line.Length; dllinelength += line.Length2;
                }
                listcity6i = 11;
                fpSpread1.Sheets[15].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[15].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 0, "����6KV����");
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 0, "��·");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1, "X");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 2, "X");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 3, linelenth);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 4, "");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 6, buildend);
                fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 0, "�����");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1, subnum);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 2, subvol);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 3, "X");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 4, "X");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 6, buildend);
                fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[15].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 0, "����6KV����");
                fpSpread1.Sheets[15].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 0, "���Ͽ���");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1, zskg);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 2, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 3, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 4, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 6, buildend);

                fpSpread1.Sheets[15].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 0, "������ѹ��");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1, zspdnum);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 2, zspdvol);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 3, "X");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 4, "X");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 6, buildend);
                fpSpread1.Sheets[15].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 0, "��ʽ���վ");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1, xsnum);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 2, xsvol);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 3, "X");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 4, "X");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 6, buildend);

                fpSpread1.Sheets[15].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 0, "������");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1, kbnum);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 2, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 3, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 4, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 6, buildend);

                fpSpread1.Sheets[15].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[15].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 0, "������·");
                fpSpread1.Sheets[15].Rows[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 0, "���У��ܿ���·");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1, "X");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 2, "X");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 3, jslinelength);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 4, "");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 6, buildend);

                fpSpread1.Sheets[15].Rows[12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 0, "      ������·");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1, "X");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 2, "X");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 3, dllinelength);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 4, "");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 6, buildend);
                fpSpread1.Sheets[15].Rows[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                //��ֵͳ��

                fpSpread1.Sheets[15].Cells[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 4].Formula = "SUM(R" + (13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C4:R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C4)";
            }
            int listcity3i = 0;
            if (listcity3sl.Count != 0 )
            {
                string buildyear="", buildend="";
                double linelenth = 0, subvol = 0; double subnum = 0; //�ֱ�Ϊ��·�ĳ��ȣ����վ�������ͱ��̨��
                double zskg = 0; double zspdnum = 0; double zspdvol = 0; double xsnum = 0; double xsvol = 0; double kbnum=0; double gylinelength = 0; double jslinelength = 0, dllinelength = 0;
                for (int j = 0; j < listcity3sl.Count; j++)
                {
                    buildyear = listcity3sl[j].BuildYear;
                    buildend = listcity3sl[j].BuildEd;
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity3sl[j].ID + "' and Col4 = 'pw-line'";
                    Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity3sl[j].ID + "' and Col4 = 'pw-pb'";
                    Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);

                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity3sl[j].ID + "' and Col4 = 'pw-kg'";
                    Ps_Table_TZGS kg = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    linelenth += (line.Length + line.Length2);
                    subnum += sub.Num1; subvol += sub.Num2*100; zskg += kg.Num3; zspdnum += sub.Num5; zspdvol += sub.Num6*100; xsnum += sub.Num3; xsvol += sub.Num4*100;
                    kbnum += kg.Num1; jslinelength += line.Length; dllinelength += line.Length2;
                }
                listcity3i = 11;
                fpSpread1.Sheets[15].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[15].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 0, "����3KV����");
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 0, "��·");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1, "X");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 2, "X");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 3, linelenth);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 4, "");
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 6, buildend);
                fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 0, "�����");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1, subnum);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 2, subvol);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 3, "X");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 4, "X");
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 6, buildend);
                fpSpread1.Sheets[15].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[15].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 0, "����3KV����");
                fpSpread1.Sheets[15].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 0, "���Ͽ���");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1, zskg);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 2, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 3, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 4, "X");
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 6, buildend);

                fpSpread1.Sheets[15].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 0, "������ѹ��");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1, zspdnum);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 2, zspdvol);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 3, "X");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 4, "X");
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 6, buildend);
                fpSpread1.Sheets[15].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 0, "��ʽ���վ");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1, xsnum);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 2, xsvol);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 3, "X");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 4, "X");
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 6, buildend);

                fpSpread1.Sheets[15].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 0, "������");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1, kbnum);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 2, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 3, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 4, "X");
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 6, buildend);

                fpSpread1.Sheets[15].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[15].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 0, "������·");
                fpSpread1.Sheets[15].Rows[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 0, "���У��ܿ���·");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1, "X");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 2, "X");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 3, jslinelength);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 4, "");
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 6, buildend);

                fpSpread1.Sheets[15].Rows[12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[15].Rows.Add(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 0, "      ������·");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1, "X");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 2, "X");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 3, dllinelength);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 4, "");
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 5, buildyear);
                fpSpread1.Sheets[15].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 6, buildend);
                fpSpread1.Sheets[15].Rows[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                //��ֵͳ��

                fpSpread1.Sheets[15].Cells[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 4].Formula = "SUM(R" + (13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i).ToString() + "C4:R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i).ToString() + "C4)";
            }

            //ũ��66KV���� �ڴ��ڼ�Ҫ�����غ�ͳ����·�ͱ���������� ����
            int citynum = list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i + listcity3i;
            int listcountry66i = 0;
            if (flag66l)
            {
                listcountry66i = 1;
                fpSpread1.Sheets[15].Rows.Add(3 + citynum, 1);
                fpSpread1.Sheets[15].SetValue(3 + citynum, 0, "ũ��66KV����");
                fpSpread1.Sheets[15].Rows[3 + citynum].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {
                    if (listcountry66sl.ContainsKey(area.Title) )
                    {
                        //ͳ�ƺ���ӽ��
                        double Zrlnum = 0; double bdts = 0; double lenth = 0; string buildyear="", buildend="";
                        foreach (Ps_Table_TZGS pt in listcountry66sl[area.Title])
                        {
                            buildyear = pt.BuildYear; buildend = pt.BuildEd;
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-line'";
                            Ps_Table_TZGS line =(Ps_Table_TZGS) Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            lenth += (line.Length + line.Length2);
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-pb'";
                            Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            Zrlnum += sub.Num1; bdts += sub.Num2*100;
                        }
                        fpSpread1.Sheets[15].Rows.Add(4 + citynum, 1);
                        fpSpread1.Sheets[15].SetValue(4 + citynum, 0, area.Title);
                        fpSpread1.Sheets[15].Rows[4 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[15].Rows.Add(5 + citynum, 1);
                        fpSpread1.Sheets[15].SetValue(5 + citynum, 0, "���У���·");
                        fpSpread1.Sheets[15].Rows[5 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[15].Rows.Add(6 + citynum, 1);
                        fpSpread1.Sheets[15].SetValue(6 + citynum, 0, "    �����");
                        fpSpread1.Sheets[15].Rows[6 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry66i += 3;  //������¼
                        //��ֵ
                        fpSpread1.Sheets[15].SetValue(5 + citynum, 3, lenth);
                        fpSpread1.Sheets[15].SetValue(5 + citynum, 5,buildyear);
                        fpSpread1.Sheets[15].SetValue(5 + citynum, 6, buildend);

                        fpSpread1.Sheets[15].SetValue(6 + citynum, 1, Zrlnum);
                        fpSpread1.Sheets[15].SetValue(6 + citynum, 2, bdts);
                        fpSpread1.Sheets[15].SetValue(6 + citynum, 5, buildyear);
                        fpSpread1.Sheets[15].SetValue(6 + citynum, 6, buildend);

                        //��ֵͳ��
                        fpSpread1.Sheets[15].Cells[4 + citynum, 1].Formula = "R" + (7 + citynum).ToString() + "C2";
                        fpSpread1.Sheets[15].Cells[4 + citynum, 2].Formula = "R" + (7 + citynum).ToString() + "C3";
                        fpSpread1.Sheets[15].Cells[4 + citynum, 3].Formula = "R" + (6 + citynum).ToString() + "C4";

                        
                    }
                }

            }
            //ũ��35KV��������
            int listcountry35i = 0;
            if (flag35l )
            {

                listcountry35i = 1;
                fpSpread1.Sheets[15].Rows.Add(3 + citynum + listcountry66i, 1);
                fpSpread1.Sheets[15].SetValue(3 + citynum + listcountry66i, 0, "ũ��35KV����");
                fpSpread1.Sheets[15].Rows[3 + citynum + listcountry66i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry35sl.ContainsKey(area.Title))
                    {
                        //ͳ�ƺ���ӽ��
                        double Zrlnum = 0; double bdts = 0; double lenth = 0; string buildyear="", buildend="";
                        foreach (Ps_Table_TZGS pt in listcountry35sl[area.Title])
                        {
                            buildyear = pt.BuildYear; buildend = pt.BuildEd;
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-line'";
                            Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            lenth += (line.Length + line.Length2);
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-pb'";
                            Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            Zrlnum += sub.Num1; bdts += sub.Num2*100;
                        }
                        fpSpread1.Sheets[15].Rows.Add(4 + citynum + listcountry66i, 1);
                        fpSpread1.Sheets[15].SetValue(4 + citynum + listcountry66i, 0, area.Title);
                        fpSpread1.Sheets[15].Rows[4 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[15].Rows.Add(5 + citynum + listcountry66i, 1);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i, 0, "���У���·");
                        fpSpread1.Sheets[15].Rows[5 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[15].Rows.Add(6+ citynum + listcountry66i, 1);
                        fpSpread1.Sheets[15].SetValue(6+ citynum + listcountry66i, 0, "    �����");
                        fpSpread1.Sheets[15].Rows[6 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry35i += 3;  //������¼

                        //��ֵ
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i, 3, lenth);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i,5,buildyear);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i, 6, buildend);

                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i, 1, Zrlnum);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i, 2, bdts);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i, 5, buildyear);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i, 6, buildend);
                        //��ֵͳ��
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i, 1].Formula = "R" + (7 + citynum + listcountry66i).ToString() + "C2";
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i, 2].Formula = "R" + (7 + citynum + listcountry66i).ToString() + "C3";
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i, 3].Formula = "R" + (6 + citynum + listcountry66i).ToString() + "C4";

                      
                    }

                }
            }
            //ũ��10KV����
            int listcountry10i = 0;
            if (flag10l )
            {
                listcountry10i = 1;
                fpSpread1.Sheets[15].Rows.Add(3 + citynum + listcountry66i + listcountry35i, 1);
                fpSpread1.Sheets[15].SetValue(3 + citynum + listcountry66i + listcountry35i, 0, "ũ��10KV����");
                fpSpread1.Sheets[15].Rows[3 + citynum + listcountry66i + listcountry35i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry10sl.ContainsKey(area.Title) )
                    {
                        //ͳ�ƺ���ӽ��
                        double Zrlnum = 0; double bdts = 0; double lenth = 0; string buildyear="", buildend="";
                        foreach (Ps_Table_TZGS pt in listcountry10sl[area.Title])
                        {
                            buildyear = pt.BuildYear; buildend = pt.BuildEd;
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-line'";
                            Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            lenth += (line.Length + line.Length2);
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-pb'";
                            Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            Zrlnum += sub.Num1; bdts += sub.Num2*100;
                        }
                        fpSpread1.Sheets[15].Rows.Add(4 + citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[15].SetValue(4 + citynum + listcountry66i + listcountry35i, 0, area.Title);
                        fpSpread1.Sheets[15].Rows[4 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[15].Rows.Add(5 + citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i, 0, "���У���·");
                        fpSpread1.Sheets[15].Rows[5 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[15].Rows.Add(6+ citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i, 0, "    �����");
                        fpSpread1.Sheets[15].Rows[6 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry10i += 3;  //������¼


                        //��ֵ
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i + listcountry35i, 3, lenth);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i + listcountry35i, 5, buildyear);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i + listcountry35i, 6, buildend);

                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i, 1, Zrlnum);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i, 2, bdts);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i, 5, buildyear);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i, 6, buildend);

                        //��ֵͳ��
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i+listcountry35i, 1].Formula = "R" + (7 + citynum + listcountry66i+listcountry35i).ToString() + "C2";
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i+listcountry35i, 2].Formula = "R" + (7 + citynum + listcountry66i+listcountry35i).ToString() + "C3";
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i+listcountry35i, 3].Formula = "R" + (6 + citynum + listcountry66i+listcountry35i).ToString() + "C4";

                    }

                }
            }
            //ũ��6KV����
            int listcountry6i = 0;
            if (flag6l )
            {
                listcountry6i = 1;
                fpSpread1.Sheets[15].Rows.Add(3 + citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                fpSpread1.Sheets[15].SetValue(3 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, "ũ��6KV����");
                fpSpread1.Sheets[15].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry6sl.ContainsKey(area.Title) )
                    {
                        //ͳ�ƺ���ӽ��
                        double Zrlnum = 0; double bdts = 0; double lenth = 0; string buildyear="", buildend="";
                        foreach (Ps_Table_TZGS pt in listcountry6sl[area.Title])
                        {
                            buildyear = pt.BuildYear; buildend = pt.BuildEd;
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-line'";
                            Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            lenth += (line.Length + line.Length2);
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-pb'";
                            Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            Zrlnum += sub.Num1; bdts += sub.Num2*100;
                        }
                        fpSpread1.Sheets[15].Rows.Add(4 + citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[15].SetValue(4 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, area.Title);
                        fpSpread1.Sheets[15].Rows[4 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[15].Rows.Add(5 + citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i + listcountry10i, 0, "���У���·");
                        fpSpread1.Sheets[15].Rows[5 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[15].Rows.Add(6+ citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, "    �����");
                        fpSpread1.Sheets[15].Rows[6 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry6i += 3;  //������¼

                        //��ֵ
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i, 3, lenth);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i, 5, buildyear);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i, 6, buildend);

                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 1, Zrlnum);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 2, bdts);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 5, buildyear);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 6, buildend);
                        //��ֵͳ��
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i, 1].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C2";
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i, 2].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C3";
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i, 3].Formula = "R" + (6 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C4";

                   
                    }

                }
            }
            //ũ��3KV����
            int listcountry3i = 0;
            if (flag3l)
            {
                listcountry3i = 1;
                fpSpread1.Sheets[15].Rows.Add(3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                fpSpread1.Sheets[15].SetValue(3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, "ũ��3KV����");
                fpSpread1.Sheets[15].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[15].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry3sl.ContainsKey(area.Title))
                    {
                        //ͳ�ƺ���ӽ��
                        double Zrlnum = 0; double bdts = 0; double lenth = 0; string buildyear="", buildend="";
                        foreach (Ps_Table_TZGS pt in listcountry3sl[area.Title])
                        {
                            buildyear = pt.BuildYear; buildend = pt.BuildEd;
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-line'";
                            Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            lenth += (line.Length + line.Length2);
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-pb'";
                            Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            Zrlnum += sub.Num1; bdts += sub.Num2*100;
                        }
                        fpSpread1.Sheets[15].Rows.Add(4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[15].SetValue(4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, area.Title);
                        fpSpread1.Sheets[15].Rows[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[15].Rows.Add(5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i + listcountry10i + listcountry6i, 0, "���У���·");
                        fpSpread1.Sheets[15].Rows[5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[15].Rows.Add(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, "    �����");
                        fpSpread1.Sheets[15].Rows[6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry3i += 3;  //������¼

                        //��ֵ
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 3, lenth);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 5, buildyear);
                        fpSpread1.Sheets[15].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 6, buildend);


                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1, Zrlnum);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 2, bdts);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 5, buildyear);
                        fpSpread1.Sheets[15].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 6, buildend);
                        //��ֵͳ��
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C2";
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 2].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C3";
                        fpSpread1.Sheets[15].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 3].Formula = "R" + (6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C4";

                      
                    }

                }
                //�趨��ʽ

            }
            #endregion
            #endregion
            Sheet_GridandColor(fpSpread1.Sheets[15], 3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i + listcountry3i, 7);
            SetSheetViewColumnsWhith(fpSpread1.Sheets[15], 1, "xxxxxxxxxxxxxxx");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[15], 6, "�ƻ�Ͷ��ʱ��");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[15], 5, "�ƻ�����ʱ��");
        }
        //������Ŀ�ʽ������

        private void sbzjxqb()
        {
            #region ������ͷ
            fpSpread1.Sheets[16].RowCount = 0;
            fpSpread1.Sheets[16].ColumnCount = 0;
            fpSpread1.Sheets[16].RowCount = 5;
            fpSpread1.Sheets[16].ColumnCount = 11;
            fpSpread1.Sheets[16].SetValue(0, 0, "��ʮ���塱�滮������Ŀ�ʽ�����ϸ��");
            fpSpread1.Sheets[16].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[16].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[16].Cells[0, 0].ColumnSpan = 11;
            fpSpread1.Sheets[16].SetValue(1, 0, "��λ����Ԫ");

            fpSpread1.Sheets[16].Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[16].Cells[1, 0].ColumnSpan = 11;
            fpSpread1.Sheets[16].Rows[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            fpSpread1.Sheets[16].SetValue(2, 1, "��̬Ͷ��");
            fpSpread1.Sheets[16].Cells[2, 1].ColumnSpan = 5;

            fpSpread1.Sheets[16].SetValue(3,1, "2011��");
            fpSpread1.Sheets[16].SetValue(3,2, "2012��");
            fpSpread1.Sheets[16].SetValue(3,3, "2013��");
            fpSpread1.Sheets[16].SetValue(3,4, "2014��");
            fpSpread1.Sheets[16].SetValue(3,5, "2015��");
            fpSpread1.Sheets[16].Rows[3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            fpSpread1.Sheets[16].SetValue(2, 7, "��̬��Ͷ��");
            fpSpread1.Sheets[16].Cells[2, 7].RowSpan = 2;
            fpSpread1.Sheets[16].SetValue(2, 8, "�����ڴ�����Ϣ");
            fpSpread1.Sheets[16].Cells[2, 8].RowSpan = 2;
            fpSpread1.Sheets[16].SetValue(2, 9, "�۸�Ԥ����");
            fpSpread1.Sheets[16].Cells[2, 9].RowSpan = 2;
            fpSpread1.Sheets[16].SetValue(2, 10, "��̬Ͷ��");
            fpSpread1.Sheets[16].Cells[2, 10].RowSpan = 2;


            fpSpread1.Sheets[16].SetValue(2, 2, "2010��");
            fpSpread1.Sheets[16].Cells[2, 2].RowSpan = 2;

            fpSpread1.Sheets[16].SetValue(2, 0, "��Ŀ");
            fpSpread1.Sheets[16].Cells[2, 0].RowSpan = 2;
            fpSpread1.Sheets[16].SetValue(2, 2, "����");
            fpSpread1.Sheets[16].SetValue(2, 3, "����");
            fpSpread1.Sheets[16].SetValue(2, 4, "�ͺ�");
            fpSpread1.Sheets[16].SetValue(2, 5, "�ƻ�����ʱ��");

            fpSpread1.Sheets[16].SetValue(2, 6, "Ԥ��Ͷ��ʱ��");

            #endregion
            #region ���ݵ�ѹ�ȼ���ȡ���� ��·�ͱ��վ���� �������������
            IList<Ps_Table_TZMX> list1000s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list1000l = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list750s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list750l = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list500s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list500l = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list330s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list330l = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list220s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list220l = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list110s = new List<Ps_Table_TZMX>();
            IList<Ps_Table_TZMX> list110l = new List<Ps_Table_TZMX>();
            //��������ͳ��
            IList<Ps_Table_TZGS> listcity66sl = new List<Ps_Table_TZGS>();


            IList<Ps_Table_TZGS> listcity35sl = new List<Ps_Table_TZGS>();


            IList<Ps_Table_TZGS> listcity10sl = new List<Ps_Table_TZGS>();

            IList<Ps_Table_TZGS> listcity6sl = new List<Ps_Table_TZGS>();


            IList<Ps_Table_TZGS> listcity3sl = new List<Ps_Table_TZGS>();

            string con = "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '1000') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list1000l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '1000') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list1000s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '750') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list750l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '750') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list750s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);

            con = "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '500') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list500l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '500') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list500s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);

            con = "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '330') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list330l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '330') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list330s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);

            con = "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '220') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list220l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '220') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list220s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);

            con = "(Typeqf = 'line') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '110') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list110l = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);
            con = "(Typeqf = 'sub') AND (ProjectID IN(SELECT a.ID FROM Ps_Table_TZGS a INNER JOIN Ps_Table_TZGS b ON a.ID = b.ParentID INNER JOIN Ps_Table_TZGS c ON a.ID = c.ParentID WHERE (a.ProjectID = '" + ProjectUID + "') AND (SUBSTRING(a.BianInfo, 1, CHARINDEX('@', a.BianInfo, 0) - 1) = '110') AND (b.Col4 = 'line') AND (c.Col4 = 'bian')))";
            list110s = Services.BaseService.GetList<Ps_Table_TZMX>("SelectPs_Table_TZMXByValue", con);

            con = "and  a.projectID='" + ProjectUID + "'and a.DQ='��Ͻ������' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='66'";
            listcity66sl = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);

            con = "and  a.projectID='" + ProjectUID + "'and a.DQ='��Ͻ������' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='35'";
            listcity35sl = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);

            con = "and  a.projectID='" + ProjectUID + "'and a.DQ='��Ͻ������' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='10'";
            listcity10sl = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);

            con = "and  a.projectID='" + ProjectUID + "'and a.DQ='��Ͻ������' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='6'";
            listcity6sl = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);


            con = "and  a.projectID='" + ProjectUID + "'and a.DQ='��Ͻ������' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='3'";
            listcity3sl = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);






            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='3'AND DQ='ũ��'  ORDER BY LineLength";
            // listcountry3l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            //�Ȼ�ȡ�������ơ����������ũ������������·�ͱ��վ��������������н���ͳ��
            Dictionary<string, IList<Ps_Table_TZGS>> listcountry66sl = new Dictionary<string, IList<Ps_Table_TZGS>>();

            Dictionary<string, IList<Ps_Table_TZGS>> listcountry35sl = new Dictionary<string, IList<Ps_Table_TZGS>>();


            Dictionary<string, IList<Ps_Table_TZGS>> listcountry10sl = new Dictionary<string, IList<Ps_Table_TZGS>>();

            Dictionary<string, IList<Ps_Table_TZGS>> listcountry6sl = new Dictionary<string, IList<Ps_Table_TZGS>>();

            Dictionary<string, IList<Ps_Table_TZGS>> listcountry3sl = new Dictionary<string, IList<Ps_Table_TZGS>>();

            bool flag66l = false, flag35l = false, flag35s = false, flag10l = false, flag10s = false, flag6l = false, flag6s = false, flag3l = false, flag3s = false;
            string conn = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);

            foreach (PS_Table_AreaWH area in list)
            {
                con = "and a.ProjectID='" + ProjectUID + "'AND a.AreaName='" + area.Title + "'AND substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='66'AND a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
                IList<Ps_Table_TZGS> list1 = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);
                if (list1.Count != 0)
                {
                    flag66l = true;
                    listcountry66sl.Add(area.Title, list1);
                }


                con = "and a.ProjectID='" + ProjectUID + "'AND a.AreaName='" + area.Title + "'AND substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='35'AND a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
                list1 = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);
                if (list1.Count != 0)
                {
                    flag35l = true;
                    listcountry35sl.Add(area.Title, list1);
                }

                con = "and a.ProjectID='" + ProjectUID + "'AND a.AreaName='" + area.Title + "'AND substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='10'AND a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
                list1 = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);
                if (list1.Count != 0)
                {
                    flag10l = true;
                    listcountry10sl.Add(area.Title, list1);
                }

                con = "and a.ProjectID='" + ProjectUID + "'AND a.AreaName='" + area.Title + "'AND substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='6'AND a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
                list1 = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);
                if (list1.Count != 0)
                {
                    flag6l = true;
                    listcountry6sl.Add(area.Title, list1);
                }



                con = "and a.ProjectID='" + ProjectUID + "'AND a.AreaName='" + area.Title + "'AND substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='3'AND a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
                list1 = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", con);
                if (list1.Count != 0)
                {
                    flag3l = true;
                    listcountry3sl.Add(area.Title, list1);
                }

                // repositoryItemComboBox3.Items.Add(area.Title);
            }

            #endregion
            #region ������������к���ӵ�Ԫ��ʽ
            List<string> title = new List<string>();
            int list1000i = 0;
            #region ������
            //1000kv����
            if (list1000s.Count != 0 || list1000l.Count != 0)
            {
                list1000i = 1;
                fpSpread1.Sheets[16].Rows.Add(4, 1);
                fpSpread1.Sheets[16].SetValue(4, 0, "1000KV����(�ϼ�)");
                fpSpread1.Sheets[16].Rows[4].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list1000l.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5, 1);
                    fpSpread1.Sheets[16].SetValue(5, 0, list1000l[list1000l.Count - 1 - i].Title);
                    fpSpread1.Sheets[16].SetValue(5, 1, "");
                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list1000l[list1000l.Count - 1 - i], year));
                    }

                    fpSpread1.Sheets[16].SetValue(5, 8, list1000l[list1000l.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5, 9, list1000l[list1000l.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5, 10, list1000l[list1000l.Count - 1 - i].DynInvest);

                    fpSpread1.Sheets[16].Rows[5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Rows[5].CellType = numberCellTypes1;
                    fpSpread1.Sheets[16].Cells[5, 7].Formula = "SUM(R6C2:R6C7)";
                    list1000i++;
                }
                //fpSpread1.Sheets[16].Cells[3, 3].Formula = "SUM(R5C4:R" + (5 + list1000l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list1000s.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5 + list1000l.Count, 1);
                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5 + list1000l.Count, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list1000s[list1000s.Count - 1 - i], year));
                    }
                    fpSpread1.Sheets[16].SetValue(5 + list1000l.Count, 8, list1000s[list1000s.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5 + list1000l.Count, 9, list1000s[list1000s.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5 + list1000l.Count, 10, list1000s[list1000s.Count - 1 - i].DynInvest);

                    fpSpread1.Sheets[16].Rows[5 + list1000l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Rows[5 + list1000l.Count].CellType = numberCellTypes1;
                    fpSpread1.Sheets[16].Cells[5 + list1000l.Count, 7].Formula = "SUM(R6" + list1000l.Count.ToString() + "C2:R6" + list1000l.Count.ToString() + "C7)";
                    // fpSpread1.Sheets[16].Cells[5 + list1000l.Count, 2].CellType = numberCellTypes1;
                    list1000i++;
                }
                //fpSpread1.Sheets[16].Cells[4, 1].Formula = "SUM(R" + (5 + list1000l.Count).ToString() + "C2:R" + (5 + list1000l.Count + list1000s.Count - 1).ToString() + "C2)";
                // fpSpread1.Sheets[16].Cells[4, 2].Formula = "SUM(R" + (5 + list1000l.Count).ToString() + "C3:R" + (5 + list1000l.Count + list1000s.Count - 1).ToString() + "C3)";
            }
            //750KV����
            int list750i = 0;
            if (list750s.Count != 0 || list750l.Count != 0)
            {
                list750i = 1;
                fpSpread1.Sheets[16].Rows.Add(4 + list1000i, 1);
                fpSpread1.Sheets[16].SetValue(4 + list1000i, 0, "750KV����(�ϼ�)");
                fpSpread1.Sheets[16].Rows[4 + list1000i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + list1000i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list750l.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5 + list1000i, 1);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i, 0, list750l[list750l.Count - 1 - i].Title);
                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5 + list1000i, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list750l[list750l.Count - 1 - i], year));
                    }
                    fpSpread1.Sheets[16].SetValue(5 + list1000i, 8, list750l[list750l.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i, 9, list750l[list750l.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i, 10, list750l[list750l.Count - 1 - i].DynInvest);
                    fpSpread1.Sheets[16].Rows[5 + list1000i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Cells[5 + list1000i, 7].Formula = "SUM(R6" + list1000i.ToString() + "C2:R6" + list1000i.ToString() + "C7)";
                    //fpSpread1.Sheets[16].Cells[5 + list1000i, 4].CellType = numberCellTypes1;
                    list750i++;
                }
                //fpSpread1.Sheets[16].Cells[4 + list1000i, 3].Formula = "SUM(R" + (5 + list1000i).ToString() + "C4:R" + (5 + list1000i + list750l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list750s.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750l.Count, 1);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750l.Count, 0, list750s[list750s.Count - 1 - i].Title);

                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750l.Count, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list750s[list750s.Count - 1 - i], year));
                    }
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750l.Count, 8, list750s[list750s.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750l.Count, 9, list750s[list750s.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750l.Count, 10, list750s[list750s.Count - 1 - i].DynInvest);

                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750l.Count].CellType = numberCellTypes1;
                    fpSpread1.Sheets[16].Cells[5 + list1000i + list750l.Count, 7].Formula = "SUM(R6" + (list1000i + list750l.Count).ToString() + "C2:R6" + (list1000i + list750l.Count).ToString() + "C7)";
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                    list750i++;
                }
                //fpSpread1.Sheets[16].Cells[4 + list1000i, 1].Formula = "SUM(R" + (5 + list1000i + list750l.Count).ToString() + "C2:R" + (5 + list1000i + list750l.Count + list750s.Count - 1).ToString() + "C2)";
                //fpSpread1.Sheets[16].Cells[4 + list1000i, 2].Formula = "SUM(R" + (5 + list1000i + list750l.Count).ToString() + "C3:R" + (5 + list1000i + list750l.Count + list750s.Count - 1).ToString() + "C3)";
            }
            //500KV����
            int list500i = 0;
            if (list500l.Count != 0 || list500s.Count != 0)
            {
                list500i = 1;
                fpSpread1.Sheets[16].Rows.Add(4 + list1000i + list750i, 1);
                fpSpread1.Sheets[16].SetValue(4 + list1000i + list750i, 0, "500KV����(�ϼ�)");
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list500l.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i, 1);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i, 0, list500l[list500l.Count - 1 - i].Title);
                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list500l[list500l.Count - 1 - i], year));
                    }
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i, 8, list500l[list500l.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i, 9, list500l[list500l.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i, 10, list500l[list500l.Count - 1 - i].DynInvest);
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Cells[5 + list1000i + list750i, 7].Formula = "SUM(R6" + (list1000i + list750i).ToString() + "C2:R6" + (list1000i + list750i).ToString() + "C7)";
                    //fpSpread1.Sheets[16].Cells[5 + list1000i + list750i, 4].CellType = numberCellTypes1;
                    list500i++;
                }
                //fpSpread1.Sheets[16].Cells[4 + list1000i + list750i, 3].Formula = "SUM(R" + (5 + list1000i + list750i).ToString() + "C4:R" + (5 + list1000i + list750i + list500l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list500s.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500l.Count, 1);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500l.Count, 0, list500s[list500s.Count - 1 - i].Title);
                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750l.Count + list500l.Count, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list500s[list500s.Count - 1 - i], year));
                    }
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500l.Count, 8, list500s[list500s.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500l.Count, 9, list500s[list500s.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500l.Count, 10, list500s[list500s.Count - 1 - i].DynInvest);

                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500l.Count].CellType = numberCellTypes1;
                    fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500l.Count, 7].Formula = "SUM(R6" + (list1000i + +list750i + list500l.Count).ToString() + "C2:R6" + (list1000i + +list750i + list500l.Count).ToString() + "C7)";
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    list500i++;
                }
                //  fpSpread1.Sheets[16].Cells[4 + list1000i + list750i, 1].Formula = "SUM(R" + (5 + list1000i + list750i + list500l.Count).ToString() + "C2:R" + (5 + list1000i + list750i + list500l.Count + list500s.Count - 1).ToString() + "C2)";
                //fpSpread1.Sheets[16].Cells[4+ list1000i + list750i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500l.Count).ToString() + "C3:R" + (5 + list1000i + list750i + list500l.Count + list500s.Count - 1).ToString() + "C3)";
            }
            //330KV����
            int list330i = 0;
            if (list330l.Count != 0 || list330s.Count != 0)
            {
                list330i = 1;
                fpSpread1.Sheets[16].Rows.Add(4 + list1000i + list750i + list500i, 1);
                fpSpread1.Sheets[16].SetValue(4 + list1000i + list750i + list500i, 0, "330KV����");
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list330l.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500i, 1);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i, 0, list330l[list330l.Count - 1 - i].Title);
                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list330l[list330l.Count - 1 - i], year));
                    }
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i, 8, list330l[list330l.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i, 9, list330l[list330l.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i, 10, list330l[list330l.Count - 1 - i].DynInvest);
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500i, 7].Formula = "SUM(R6" + (list1000i + list750i + list500i).ToString() + "C2:R6" + (list1000i + list750i + list500i).ToString() + "C7)";
                    list330i++;
                }
                // fpSpread1.Sheets[16].Cells[4 + list1000i + list750i + list500i, 3].Formula = "SUM(R" + (5 + list1000i + list750i + list500i).ToString() + "C4:R" + (5 + list1000i + list750i + list500i + list330l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list330s.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500i + list330l.Count, 1);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330l.Count, 0, list330s[list330s.Count - 1 - i].Title);
                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750l.Count + list500i + list330l.Count, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list330s[list330s.Count - 1 - i], year));
                    }
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330l.Count, 8, list330s[list330s.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330l.Count, 9, list330s[list330s.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330l.Count, 10, list330s[list330s.Count - 1 - i].DynInvest);

                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330l.Count].CellType = numberCellTypes1;
                    fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500i + list330l.Count, 7].Formula = "SUM(R6" + (list1000i + +list750i + list500i + list330l.Count).ToString() + "C2:R6" + (list1000i + +list750i + list500i + list330l.Count).ToString() + "C7)";
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    list330i++;
                }
                //fpSpread1.Sheets[16].Cells[4 + list1000i + list750i + list500i, 1].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330l.Count).ToString() + "C2:R" + (5 + list1000i + list750i + list500i + list330l.Count + list330s.Count - 1).ToString() + "C2)";
                //fpSpread1.Sheets[16].Cells[4 + list1000i + list750i + list500i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330l.Count).ToString() + "C3:R" + (5 + list1000i + list750i + list500i + list330l.Count + list330s.Count - 1).ToString() + "C3)";
            }
            //220KV����
            int list220i = 0;
            if (list220l.Count != 0 || list220s.Count != 0)
            {
                list220i = 1;
                fpSpread1.Sheets[16].Rows.Add(4 + list1000i + list750i + list500i + list330i, 1);
                fpSpread1.Sheets[16].SetValue(4 + list1000i + list750i + list500i + list330i, 0, "220KV����(�ϼ�)");
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list220l.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500i + list330i, 1);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i, 0, list220l[list220l.Count - 1 - i].Title);
                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list220l[list220l.Count - 1 - i], year));
                    }
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i, 8, list220l[list220l.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i, 9, list220l[list220l.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i, 10, list220l[list220l.Count - 1 - i].DynInvest);
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500i + list330i, 7].Formula = "SUM(R6" + (list1000i + list750i + list500i + list330i).ToString() + "C2:R6" + (list1000i + list750i + list500i + list330i).ToString() + "C7)";
                    list220i++;
                }
                //fpSpread1.Sheets[16].Cells[4 + list1000i + list750i + list500i + list330i, 3].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i).ToString() + "C4:R" + (5 + list1000i + list750i + list500i + list330i + list220l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list220s.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220l.Count, 1);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220l.Count, 0, list220s[list220s.Count - 1 - i].Title);
                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220l.Count, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list220s[list220s.Count - 1 - i], year));
                    }
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220l.Count, 8, list220s[list220s.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220l.Count, 9, list220s[list220s.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220l.Count, 10, list220s[list220s.Count - 1 - i].DynInvest);

                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220l.Count].CellType = numberCellTypes1;
                    fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500i + list330i + list220l.Count, 7].Formula = "SUM(R6" + (list1000i + +list750i + list500i + list330i + list220l.Count).ToString() + "C2:R6" + (list1000i + +list750i + list500i + list330i + list220l.Count).ToString() + "C7)";
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    list220i++;
                }
                //fpSpread1.Sheets[16].Cells[4 + list1000i + list750i + list500i + list330i, 1].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list330i + list220l.Count).ToString() + "C2:R" + (5 + list1000i + list750i + list500i + list330i + list220l.Count + list220s.Count - 1).ToString() + "C2)";
                //fpSpread1.Sheets[16].Cells[4 + list1000i + list750i + list500i + list330i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220l.Count).ToString() + "C3:R" + (5 + list1000i + list750i + list500i + list330i + list220l.Count + list220s.Count - 1).ToString() + "C3)";

            }
            //110KV����
            int list110i = 0;
            if (list110l.Count != 0 || list110s.Count != 0)
            {
                list110i = 1;
                fpSpread1.Sheets[16].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i, 1);
                fpSpread1.Sheets[16].SetValue(4 + list1000i + list750i + list500i + list330i + list220i, 0, "110KV�������ϼƣ�");
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list110l.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i, 1);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i, 0, list110l[list110l.Count - 1 - i].Title);
                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list110l[list110l.Count - 1 - i], year));
                    }
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i, 8, list110l[list110l.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i, 9, list110l[list110l.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i, 10, list110l[list110l.Count - 1 - i].DynInvest);
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500i + list330i + list220i, 7].Formula = "SUM(R6" + (list1000i + list750i + list500i + list330i + list220i).ToString() + "C2:R6" + (list1000i + list750i + list500i + list330i + list220i).ToString() + "C7)";
                    list110i++;
                }
                //fpSpread1.Sheets[16].Cells[4 + list1000i + list750i + list500i + list330i, 3].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i).ToString() + "C4:R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count - 1).ToString() + "C4)";
                for (int i = 0; i < list110s.Count; i++)
                {
                    fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 1);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 0, list110s[list110s.Count - 1 - i].Title);
                    for (int j = 2011; j <= 2015; j++)
                    {
                        string year = "y" + j.ToString();
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 2 + j - 2011, Gethistroyvalue<Ps_Table_TZMX>(list110s[list110s.Count - 1 - i], year));
                    }
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 8, list110s[list110s.Count - 1 - i].LendRate);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 9, list110s[list110s.Count - 1 - i].PrepChange);
                    fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 10, list110s[list110s.Count - 1 - i].DynInvest);

                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count].CellType = numberCellTypes1;
                    fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count, 7].Formula = "SUM(R6" + (list1000i + +list750i + list500i + list330i + list220i + list110l.Count).ToString() + "C2:R6" + (list1000i + +list750i + list500i + list330i + list220i + list110l.Count).ToString() + "C7)";
                    fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    list110i++;
                }
                //fpSpread1.Sheets[16].Cells[4+ list1000i + list750i + list500i + list330i + list220i, 1].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count).ToString() + "C2:R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count + list110s.Count - 1).ToString() + "C2)";
                //fpSpread1.Sheets[16].Cells[4 + list1000i + list750i + list500i + list330i + list220i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count).ToString() + "C3:R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110l.Count + list110s.Count - 1).ToString() + "C3)";
            }
            #endregion

            #region �������ݵ���� �����������ݷ�Ϊ������ũ��
            //����66KV����
            #region ��������
            int listcity66i = 0;
            if (listcity66sl.Count != 0)
            {
                string buildyear = "", buildend = "";
                double linelenth = 0, subvol = 0; double subnum = 0; //�ֱ�Ϊ��·�ĳ��ȣ����վ�������ͱ��̨��
                double linelendrate = 0, linejgybei = 0, linedyn = 0, sublendrate = 0, subjgybei = 0, subdyn = 0, zskglendrate = 0, zskgjgybei = 0, zskgdyn = 0, zspblendrate = 0, zspbjgybei = 0;
                double zspbdyn = 0, xspblendrate = 0, xspbjgybei = 0, xsdyn = 0, kblendrate = 0, kbjgybei = 0, kbdyn = 0, jkxllendrate = 0, jkxljgybei = 0, jkxldyn = 0, dlxllendrate = 0, dlxljgybei = 0, dlxldyn = 0;
                Dictionary<string, double> linedr = new Dictionary<string, double>();
                Dictionary<string, double> subdr = new Dictionary<string, double>();
                Dictionary<string, double> zskgdr = new Dictionary<string, double>();
                Dictionary<string, double> zspddr = new Dictionary<string, double>();
                Dictionary<string, double> xspddr = new Dictionary<string, double>();
                Dictionary<string, double> kbsdr = new Dictionary<string, double>();
                Dictionary<string, double> dlxldr = new Dictionary<string, double>();
                Dictionary<string, double> jkxldr = new Dictionary<string, double>();
                for (int j = 0; j < listcity66sl.Count; j++)
                {
                    buildyear = listcity66sl[j].BuildYear;
                    buildend = listcity66sl[j].BuildEd;
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity66sl[j].ID + "' and Col4 = 'pw-line'";
                    Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity66sl[j].ID + "' and Col4 = 'pw-pb'";
                    Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);

                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity66sl[j].ID + "' and Col4 = 'pw-kg'";
                    Ps_Table_TZGS kg = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + listcity66sl[j].ID + "'and Typeqf='pw-line'";
                    IList list1 = Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity66sl[j].ID + "'and Typeqf='pw-pb'and Title like'�����'";
                    Ps_Table_TZMX pb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity66sl[j].ID + "'and Typeqf='pw-pb'and Title like'��������ѹ��'";
                    Ps_Table_TZMX zspb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity66sl[j].ID + "'and Typeqf='pw-pb'and Title like'��ʽ���վ'";
                    Ps_Table_TZMX xspb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);

                    con = "ProjectID='" + listcity66sl[j].ID + "'and Typeqf='pw-kg'and Title like'���Ͽ���'";
                    Ps_Table_TZMX zskg = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity66sl[j].ID + "'and Typeqf='pw-kg'and Title like'������'";
                    Ps_Table_TZMX kbs = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);

                    con = "S1='66' and Name like'%���Ͽ���%'and S5='3'";
                    double zskgjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='66' and Name like'%��������ѹ��%'and S5='3'";
                    double zspbjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='66' and Name like'%��ʽ���վ%'and S5='3'";
                    double xspdjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='66' and Name like'%������%'and S5='3'";
                    double kbsjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    if (!linedr.ContainsKey(buildend))
                    {
                        linedr[buildend] = line.Amount;
                    }
                    else
                    linedr[buildend] += line.Amount;
                    if (!subdr.ContainsKey(buildend))
                    {
                        subdr[buildend] = sub.Amount;
                    }
                    else
                    subdr[buildend] += sub.Amount;

                    foreach (Ps_Table_TZMX pt in list1)
                    {
                        linelendrate += pt.LendRate;
                        linejgybei += pt.PrepChange;
                        linedyn += pt.DynInvest;
                        if (pt.Title.Contains("�ܿ���·"))
                        {
                            if (!jkxldr.ContainsKey(buildend))
                            {
                                jkxldr[buildend] = pt.Vol;
                            }
                            else
                            jkxldr[buildend] += pt.Vol;
                        }
                        else if (pt.Title.Contains("������·"))
                        {
                            if (!dlxldr.ContainsKey(buildend))
                            {
                                dlxldr[buildend] = pt.Vol;
                            }
                            else
                            dlxldr[buildend] += pt.Vol;
                        }
                    }
                    if (pb != null)
                    {
                        sublendrate += pb.LendRate;
                        subjgybei += pb.PrepChange;
                        subdyn += pb.DynInvest;
                    }
                    if (xspb != null)
                    {
                        xspblendrate += xspb.LendRate;
                        xspbjgybei += xspb.PrepChange;
                        xsdyn += xspb.DynInvest;
                    }
                    if (zspb != null)
                    {
                        zspblendrate += zspb.LendRate;
                        zspbjgybei += zspb.PrepChange;
                        zspbdyn += zspb.DynInvest;
                    }
                    if (zskg != null)
                    {
                        zskglendrate += zskg.LendRate;
                        zskgjgybei += zskg.PrepChange;
                        zskgdyn += zskg.DynInvest;
                    }
                    if (kbs != null)
                    {
                        kblendrate += kbs.LendRate;
                        kbjgybei += kbs.PrepChange;
                        kbdyn += kbs.DynInvest;
                    }
                    if (kg != null)
                    {
                        if (!zskgdr.ContainsKey(buildend))
                        {
                            zskgdr[buildend] = zskgjiage * kg.Num3;
                        }
                        else
                        zskgdr[buildend] += zskgjiage * kg.Num3;
                        if (!kbsdr.ContainsKey(buildend))
                        {
                            kbsdr[buildend] = kbsjiage * kg.Num1;
                        }
                        else
                        kbsdr[buildend] += kbsjiage * kg.Num1;
                    }
                    if (sub != null)
                    {
                        if (!zspddr.ContainsKey(buildend))
                        {
                            zspddr[buildend] = zspbjiage * sub.Num5;
                        }
                        else
                            zspddr[buildend] += zspbjiage * sub.Num5;
                        if (!xspddr.ContainsKey(buildend))
                        {
                            xspddr[buildend] = xspdjiage * sub.Num3;
                        }
                        else
                            xspddr[buildend] += xspdjiage * sub.Num3;
                    }

                }
                listcity66i = 11;
               
                fpSpread1.Sheets[16].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                //fpSpread1.Sheets[16].Rows[4+ list1000i + list750i + list500i + list330i + list220i + list110i].CellType = numberCellTypes1;
                fpSpread1.Sheets[16].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "����66KV����");
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "��·");

                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 1, 0);
                for (int j = 2011; j <= 2015; j++)
                {
                    string y = j.ToString();
                    if (linedr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 2 + j - 2011, linedr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500i + list330i + list220i + list110i, 7].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3:R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 8, linelendrate);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 9, linejgybei);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 10, linedyn);
                fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "�����");

                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 1, 0);
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (subdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 2 + j - 2011, subdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[6 + list1000i + list750i + list500i + list330i + list220i + list110i, 7].Formula = "SUM(R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 8, sublendrate);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 9, subjgybei);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 10, subdyn);
                fpSpread1.Sheets[16].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����
                fpSpread1.Sheets[16].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[16].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "����66KV����");
                fpSpread1.Sheets[16].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "���Ͽ���");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (zskgdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 2 + j - 2011, zskgdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i, 7].Formula = "SUM(R" + (9 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3:R" + (9 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 8, zskglendrate);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 9, zskgjgybei);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 10, zskgdyn);

                fpSpread1.Sheets[16].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "��������ѹ��");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (zspddr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 2 + j - 2011, zspddr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[9 + list1000i + list750i + list500i + list330i + list220i + list110i, 7].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3:R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 8, zspblendrate);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 9, zspbjgybei);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 10, zspbdyn);
                fpSpread1.Sheets[16].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "��ʽ���վ");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (xspddr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 2 + j - 2011, xspddr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[10 + list1000i + list750i + list500i + list330i + list220i + list110i, 7].Formula = "SUM(R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 8, xspblendrate);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 9, xspbjgybei);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 10, xsdyn);

                fpSpread1.Sheets[16].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(11 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "������");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (kbsdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i, 2 + j - 2011, kbsdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[11 + list1000i + list750i + list500i + list330i + list220i + list110i, 7].Formula = "SUM(R" + (12 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3:R" + (12 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i, 8, kblendrate);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i, 9, kbjgybei);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i, 10, kbdyn);

                fpSpread1.Sheets[16].Rows[11 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(12 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[16].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "������·");
                fpSpread1.Sheets[16].Rows[12 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "���У��ܿ���·");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (jkxldr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 2 + j - 2011, jkxldr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[13 + list1000i + list750i + list500i + list330i + list220i + list110i, 7].Formula = "SUM(R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3:R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 8, jkxllendrate);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 9, jkxljgybei);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i, 10, jkxldyn);


                fpSpread1.Sheets[16].Rows[13 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(14 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "      ������·");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (dlxldr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i, 2 + j - 2011, dlxldr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[14 + list1000i + list750i + list500i + list330i + list220i + list110i, 7].Formula = "SUM(R" + (15 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3:R" + (15 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i, 8, dlxllendrate);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i, 9, dlxljgybei);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i, 10, dlxldyn);


                fpSpread1.Sheets[16].Rows[14 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //��ֵͳ��

                // fpSpread1.Sheets[16].Cells[12+ list1000i + list750i + list500i + list330i + list220i + list110i, 4].Formula = "SUM(R" + (13 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4:R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4)";

            }
            //����35KV����
            int listcity35i = 0;
            if (listcity35sl.Count != 0)
            {
                string buildyear = "", buildend = "";
                double linelenth = 0, subvol = 0; double subnum = 0; //�ֱ�Ϊ��·�ĳ��ȣ����վ�������ͱ��̨��
                 double linelendrate = 0, linejgybei = 0, linedyn = 0, sublendrate = 0, subjgybei = 0, subdyn = 0, zskglendrate = 0, zskgjgybei = 0, zskgdyn = 0, zspblendrate = 0, zspbjgybei = 0;
                double zspbdyn = 0, xspblendrate = 0, xspbjgybei = 0, xsdyn = 0, kblendrate = 0, kbjgybei = 0, kbdyn = 0, jkxllendrate = 0, jkxljgybei = 0, jkxldyn = 0, dlxllendrate = 0, dlxljgybei = 0, dlxldyn = 0;
                Dictionary<string, double> linedr = new Dictionary<string, double>();
                Dictionary<string, double> subdr = new Dictionary<string, double>();
                Dictionary<string, double> zskgdr = new Dictionary<string, double>();
                Dictionary<string, double> zspddr = new Dictionary<string, double>();
                Dictionary<string, double> xspddr = new Dictionary<string, double>();
                Dictionary<string, double> kbsdr = new Dictionary<string, double>();
                Dictionary<string, double> dlxldr = new Dictionary<string, double>();
                Dictionary<string, double> jkxldr = new Dictionary<string, double>();
                for (int j = 0; j < listcity35sl.Count; j++)
                {
                    buildyear = listcity35sl[j].BuildYear;
                    buildend = listcity35sl[j].BuildEd;
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity35sl[j].ID + "' and Col4 = 'pw-line'";
                    Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity35sl[j].ID + "' and Col4 = 'pw-pb'";
                    Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);

                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity35sl[j].ID + "' and Col4 = 'pw-kg'";
                    Ps_Table_TZGS kg = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + listcity35sl[j].ID + "'and Typeqf='pw-line'";
                    IList list1 = Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity35sl[j].ID + "'and Typeqf='pw-pb'and Title like'�����'";
                    Ps_Table_TZMX pb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity35sl[j].ID + "'and Typeqf='pw-pb'and Title like'��������ѹ��'";
                    Ps_Table_TZMX zspb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity35sl[j].ID + "'and Typeqf='pw-pb'and Title like'��ʽ���վ'";
                    Ps_Table_TZMX xspb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);

                    con = "ProjectID='" + listcity35sl[j].ID + "'and Typeqf='pw-kg'and Title like'���Ͽ���'";
                    Ps_Table_TZMX zskg = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity35sl[j].ID + "'and Typeqf='pw-kg'and Title like'������'";
                    Ps_Table_TZMX kbs = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);

                    con = "S1='35' and Name like'%���Ͽ���%'and S5='3'";
                    double zskgjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='35' and Name like'%��������ѹ��%'and S5='3'";
                    double zspbjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='35' and Name like'%��ʽ���վ%'and S5='3'";
                    double xspdjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='35' and Name like'%������%'and S5='3'";
                    double kbsjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    linedr[buildend] += line.Amount;
                    subdr[buildend] += sub.Amount;

                    foreach (Ps_Table_TZMX pt in list1)
                    {
                        linelendrate += pt.LendRate;
                        linejgybei += pt.PrepChange;
                        linedyn += pt.DynInvest;
                        if (pt.Title.Contains("�ܿ���·"))
                        {
                            if (!jkxldr.ContainsKey(buildend))
                            {
                                jkxldr[buildend] = pt.Vol;
                            }
                            else
                                jkxldr[buildend] += pt.Vol;
                        }
                        else if (pt.Title.Contains("������·"))
                        {
                            if (!dlxldr.ContainsKey(buildend))
                            {
                                dlxldr[buildend] = pt.Vol;
                            }
                            else
                                dlxldr[buildend] += pt.Vol;
                        }
                    }
                    if (pb != null)
                    {
                        sublendrate += pb.LendRate;
                        subjgybei += pb.PrepChange;
                        subdyn += pb.DynInvest;
                    }
                    if (xspb != null)
                    {
                        xspblendrate += xspb.LendRate;
                        xspbjgybei += xspb.PrepChange;
                        xsdyn += xspb.DynInvest;
                    }
                    if (zspb != null)
                    {
                        zspblendrate += zspb.LendRate;
                        zspbjgybei += zspb.PrepChange;
                        zspbdyn += zspb.DynInvest;
                    }
                    if (zskg != null)
                    {
                        zskglendrate += zskg.LendRate;
                        zskgjgybei += zskg.PrepChange;
                        zskgdyn += zskg.DynInvest;
                    }
                    if (kbs != null)
                    {
                        kblendrate += kbs.LendRate;
                        kbjgybei += kbs.PrepChange;
                        kbdyn += kbs.DynInvest;
                    }
                    if (kg != null)
                    {
                        if (!zskgdr.ContainsKey(buildend))
                        {
                            zskgdr[buildend] = zskgjiage * kg.Num3;
                        }
                        else
                            zskgdr[buildend] += zskgjiage * kg.Num3;
                        if (!kbsdr.ContainsKey(buildend))
                        {
                            kbsdr[buildend] = kbsjiage * kg.Num1;
                        }
                        else
                            kbsdr[buildend] += kbsjiage * kg.Num1;
                    }
                    if (sub != null)
                    {
                        if (!zspddr.ContainsKey(buildend))
                        {
                            zspddr[buildend] = zspbjiage * sub.Num5;
                        }
                        else
                            zspddr[buildend] += zspbjiage * sub.Num5;
                        if (!xspddr.ContainsKey(buildend))
                        {
                            xspddr[buildend] = xspdjiage * sub.Num3;
                        }
                        else
                            xspddr[buildend] += xspdjiage * sub.Num3;
                    }

                }
                listcity35i = 11;
              
                fpSpread1.Sheets[16].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                //fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].CellType = numberCellTypes1;
                fpSpread1.Sheets[16].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "����35KV����");
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "��·");

                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1, 0);
                for (int j = 2011; j <= 2015; j++)
                {
                    string y = j.ToString();
                    if (linedr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2 + j - 2011, linedr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 7].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3:R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 8, linelendrate);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 9, linejgybei);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 10, linedyn);
                fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "�����");

                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1, 0);
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (subdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2 + j - 2011, subdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 7].Formula = "SUM(R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 8, sublendrate);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 9, subjgybei);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 10, subdyn);
                fpSpread1.Sheets[16].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����
                fpSpread1.Sheets[16].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[16].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "����35KV����");
                fpSpread1.Sheets[16].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "���Ͽ���");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (zskgdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2 + j - 2011, zskgdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 7].Formula = "SUM(R" + (9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3:R" + (9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 8, zskglendrate);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 9, zskgjgybei);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 10, zskgdyn);

                fpSpread1.Sheets[16].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "��������ѹ��");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (zspddr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2 + j - 2011, zspddr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 7].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3:R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 8, zspblendrate);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 9, zspbjgybei);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 10, zspbdyn);
                fpSpread1.Sheets[16].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "��ʽ���վ");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (xspddr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2 + j - 2011, xspddr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 7].Formula = "SUM(R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 8, xspblendrate);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 9, xspbjgybei);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 10, xsdyn);

                fpSpread1.Sheets[16].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "������");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (kbsdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2 + j - 2011, kbsdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 7].Formula = "SUM(R" + (12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3:R" + (12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 8, kblendrate);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 9, kbjgybei);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 10, kbdyn);

                fpSpread1.Sheets[16].Rows[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[16].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "������·");
                fpSpread1.Sheets[16].Rows[12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "���У��ܿ���·");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (jkxldr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2 + j - 2011, jkxldr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 7].Formula = "SUM(R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3:R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 8, jkxllendrate);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 9, jkxljgybei);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 10, jkxldyn);


                fpSpread1.Sheets[16].Rows[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "      ������·");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (dlxldr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2 + j - 2011, dlxldr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 7].Formula = "SUM(R" + (15 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3:R" + (15 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 8, dlxllendrate);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 9, dlxljgybei);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 10, dlxldyn);


                fpSpread1.Sheets[16].Rows[14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            }
            //����10KV����
            int listcity10i = 0;
            if (listcity10sl.Count != 0)
            {
                string buildyear = "", buildend = "";
                double linelenth = 0, subvol = 0; double subnum = 0; //�ֱ�Ϊ��·�ĳ��ȣ����վ�������ͱ��̨��
                double linelendrate = 0, linejgybei = 0, linedyn = 0, sublendrate = 0, subjgybei = 0, subdyn = 0, zskglendrate = 0, zskgjgybei = 0, zskgdyn = 0, zspblendrate = 0, zspbjgybei = 0;
                double zspbdyn = 0, xspblendrate = 0, xspbjgybei = 0, xsdyn = 0, kblendrate = 0, kbjgybei = 0, kbdyn = 0, jkxllendrate = 0, jkxljgybei = 0, jkxldyn = 0, dlxllendrate = 0, dlxljgybei = 0, dlxldyn = 0;
                Dictionary<string, double> linedr = new Dictionary<string, double>();
                Dictionary<string, double> subdr = new Dictionary<string, double>();
                Dictionary<string, double> zskgdr = new Dictionary<string, double>();
                Dictionary<string, double> zspddr = new Dictionary<string, double>();
                Dictionary<string, double> xspddr = new Dictionary<string, double>();
                Dictionary<string, double> kbsdr = new Dictionary<string, double>();
                Dictionary<string, double> dlxldr = new Dictionary<string, double>();
                Dictionary<string, double> jkxldr = new Dictionary<string, double>();
                for (int j = 0; j < listcity10sl.Count; j++)
                {
                    buildyear = listcity10sl[j].BuildYear;
                    buildend = listcity10sl[j].BuildEd;
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity10sl[j].ID + "' and Col4 = 'pw-line'";
                    Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity10sl[j].ID + "' and Col4 = 'pw-pb'";
                    Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);

                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity10sl[j].ID + "' and Col4 = 'pw-kg'";
                    Ps_Table_TZGS kg = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + listcity10sl[j].ID + "'and Typeqf='pw-line'";
                    IList list1 = Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity10sl[j].ID + "'and Typeqf='pw-pb'and Title like'�����'";
                    Ps_Table_TZMX pb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity10sl[j].ID + "'and Typeqf='pw-pb'and Title like'��������ѹ��'";
                    Ps_Table_TZMX zspb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity10sl[j].ID + "'and Typeqf='pw-pb'and Title like'��ʽ���վ'";
                    Ps_Table_TZMX xspb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);

                    con = "ProjectID='" + listcity10sl[j].ID + "'and Typeqf='pw-kg'and Title like'���Ͽ���'";
                    Ps_Table_TZMX zskg = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity10sl[j].ID + "'and Typeqf='pw-kg'and Title like'������'";
                    Ps_Table_TZMX kbs = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);

                    con = "S1='10' and Name like'%���Ͽ���%'and S5='3'";
                    double zskgjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='10' and Name like'%��������ѹ��%'and S5='3'";
                    double zspbjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='10' and Name like'%��ʽ���վ%'and S5='3'";
                    double xspdjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='10' and Name like'%������%'and S5='3'";
                    double kbsjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    if (!linedr.ContainsKey(buildend))
                    {
                        linedr[buildend] = line.Amount;
                    }
                    else
                        linedr[buildend] += line.Amount;
                    if (!subdr.ContainsKey(buildend))
                    {
                        subdr[buildend] = sub.Amount;
 
                    }
                    else
                    subdr[buildend] += sub.Amount;

                    foreach (Ps_Table_TZMX pt in list1)
                    {
                        linelendrate += pt.LendRate;
                        linejgybei += pt.PrepChange;
                        linedyn += pt.DynInvest;
                        if (pt.Title.Contains("�ܿ���·"))
                        {
                            if (!jkxldr.ContainsKey(buildend))
                            {
                                jkxldr[buildend] = pt.Vol;
                            }
                            else
                                jkxldr[buildend] += pt.Vol;
                        }
                        else if (pt.Title.Contains("������·"))
                        {
                            if (!dlxldr.ContainsKey(buildend))
                            {
                                dlxldr[buildend] = pt.Vol;
                            }
                            else
                                dlxldr[buildend] += pt.Vol;
                        }
                    }
                    if (pb != null)
                    {
                        sublendrate += pb.LendRate;
                        subjgybei += pb.PrepChange;
                        subdyn += pb.DynInvest;
                    }
                    if (xspb != null)
                    {
                        xspblendrate += xspb.LendRate;
                        xspbjgybei += xspb.PrepChange;
                        xsdyn += xspb.DynInvest;
                    }
                    if (zspb != null)
                    {
                        zspblendrate += zspb.LendRate;
                        zspbjgybei += zspb.PrepChange;
                        zspbdyn += zspb.DynInvest;
                    }
                    if (zskg != null)
                    {
                        zskglendrate += zskg.LendRate;
                        zskgjgybei += zskg.PrepChange;
                        zskgdyn += zskg.DynInvest;
                    }
                    if (kbs != null)
                    {
                        kblendrate += kbs.LendRate;
                        kbjgybei += kbs.PrepChange;
                        kbdyn += kbs.DynInvest;
                    }
                    if (kg != null)
                    {
                        if (!zskgdr.ContainsKey(buildend))
                        {
                            zskgdr[buildend] = zskgjiage * kg.Num3;
                        }
                        else
                            zskgdr[buildend] += zskgjiage * kg.Num3;
                        if (!kbsdr.ContainsKey(buildend))
                        {
                            kbsdr[buildend] = kbsjiage * kg.Num1;
                        }
                        else
                            kbsdr[buildend] += kbsjiage * kg.Num1;
                    }
                    if (sub != null)
                    {
                        if (!zspddr.ContainsKey(buildend))
                        {
                            zspddr[buildend] = zspbjiage * sub.Num5;
                        }
                        else
                            zspddr[buildend] += zspbjiage * sub.Num5;
                        if (!xspddr.ContainsKey(buildend))
                        {
                            xspddr[buildend] = xspdjiage * sub.Num3;
                        }
                        else
                            xspddr[buildend] += xspdjiage * sub.Num3;
                    }

                }
                listcity10i = 11;
                
                fpSpread1.Sheets[16].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                
                fpSpread1.Sheets[16].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "����10KV����");
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "��·");

                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1, 0);
                for (int j = 2011; j <= 2015; j++)
                {
                    string y = j.ToString();
                    if (linedr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 2 + j - 2011, linedr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 7].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3:R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 8, linelendrate);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 9, linejgybei);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 10, linedyn);
                fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "�����");

                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1, 0);
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (subdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 2 + j - 2011, subdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 7].Formula = "SUM(R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 8, sublendrate);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 9, subjgybei);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 10, subdyn);
                fpSpread1.Sheets[16].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����
                fpSpread1.Sheets[16].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[16].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "����10KV����");
                fpSpread1.Sheets[16].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "���Ͽ���");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (zskgdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 2 + j - 2011, zskgdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 7].Formula = "SUM(R" + (9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3:R" + (9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 8, zskglendrate);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 9, zskgjgybei);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 10, zskgdyn);

                fpSpread1.Sheets[16].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "��������ѹ��");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (zspddr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 2 + j - 2011, zspddr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 7].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3:R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 8, zspblendrate);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 9, zspbjgybei);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 10, zspbdyn);
                fpSpread1.Sheets[16].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "��ʽ���վ");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (xspddr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 2 + j - 2011, xspddr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 7].Formula = "SUM(R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 8, xspblendrate);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 9, xspbjgybei);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 10, xsdyn);

                fpSpread1.Sheets[16].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "������");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (kbsdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 2 + j - 2011, kbsdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 7].Formula = "SUM(R" + (12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3:R" + (12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 8, kblendrate);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 9, kbjgybei);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 10, kbdyn);

                fpSpread1.Sheets[16].Rows[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[16].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "������·");
                fpSpread1.Sheets[16].Rows[12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "���У��ܿ���·");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (jkxldr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 2 + j - 2011, jkxldr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 7].Formula = "SUM(R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3:R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 8, jkxllendrate);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 9, jkxljgybei);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 10, jkxldyn);


                fpSpread1.Sheets[16].Rows[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "      ������·");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (dlxldr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 2 + j - 2011, dlxldr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 7].Formula = "SUM(R" + (15 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3:R" + (15 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 8, dlxllendrate);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 9, dlxljgybei);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 10, dlxldyn);


                fpSpread1.Sheets[16].Rows[14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            }
            //����6KV����
            int listcity6i = 0;
            if (listcity6sl.Count != 0)
            {
                string buildyear = "", buildend = "";
                double linelenth = 0, subvol = 0; double subnum = 0; //�ֱ�Ϊ��·�ĳ��ȣ����վ�������ͱ��̨��
                 double linelendrate = 0, linejgybei = 0, linedyn = 0, sublendrate = 0, subjgybei = 0, subdyn = 0, zskglendrate = 0, zskgjgybei = 0, zskgdyn = 0, zspblendrate = 0, zspbjgybei = 0;
                double zspbdyn = 0, xspblendrate = 0, xspbjgybei = 0, xsdyn = 0, kblendrate = 0, kbjgybei = 0, kbdyn = 0, jkxllendrate = 0, jkxljgybei = 0, jkxldyn = 0, dlxllendrate = 0, dlxljgybei = 0, dlxldyn = 0;
                Dictionary<string, double> linedr = new Dictionary<string, double>();
                Dictionary<string, double> subdr = new Dictionary<string, double>();
                Dictionary<string, double> zskgdr = new Dictionary<string, double>();
                Dictionary<string, double> zspddr = new Dictionary<string, double>();
                Dictionary<string, double> xspddr = new Dictionary<string, double>();
                Dictionary<string, double> kbsdr = new Dictionary<string, double>();
                Dictionary<string, double> dlxldr = new Dictionary<string, double>();
                Dictionary<string, double> jkxldr = new Dictionary<string, double>();
                for (int j = 0; j < listcity6sl.Count; j++)
                {
                    buildyear = listcity6sl[j].BuildYear;
                    buildend = listcity6sl[j].BuildEd;
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity6sl[j].ID + "' and Col4 = 'pw-line'";
                    Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity6sl[j].ID + "' and Col4 = 'pw-pb'";
                    Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);

                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity6sl[j].ID + "' and Col4 = 'pw-kg'";
                    Ps_Table_TZGS kg = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + listcity6sl[j].ID + "'and Typeqf='pw-line'";
                    IList list1 = Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity6sl[j].ID + "'and Typeqf='pw-pb'and Title like'�����'";
                    Ps_Table_TZMX pb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity6sl[j].ID + "'and Typeqf='pw-pb'and Title like'��������ѹ��'";
                    Ps_Table_TZMX zspb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity6sl[j].ID + "'and Typeqf='pw-pb'and Title like'��ʽ���վ'";
                    Ps_Table_TZMX xspb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);

                    con = "ProjectID='" + listcity6sl[j].ID + "'and Typeqf='pw-kg'and Title like'���Ͽ���'";
                    Ps_Table_TZMX zskg = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity6sl[j].ID + "'and Typeqf='pw-kg'and Title like'������'";
                    Ps_Table_TZMX kbs = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);

                    con = "S1='6' and Name like'%���Ͽ���%'and S5='3'";
                    double zskgjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='6' and Name like'%��������ѹ��%'and S5='3'";
                    double zspbjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='6' and Name like'%��ʽ���վ%'and S5='3'";
                    double xspdjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='6' and Name like'%������%'and S5='3'";
                    double kbsjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    if(!linedr.ContainsKey(buildend))
                    {
                        linedr[buildend] = line.Amount;
                    }
                    else
                    linedr[buildend] += line.Amount;
                    if (!subdr.ContainsKey(buildend))
                    {
                        subdr[buildend] = sub.Amount;
                    }
                    subdr[buildend] += sub.Amount;

                    foreach (Ps_Table_TZMX pt in list1)
                    {
                        linelendrate += pt.LendRate;
                        linejgybei += pt.PrepChange;
                        linedyn += pt.DynInvest;
                        if (pt.Title.Contains("�ܿ���·"))
                        {
                            if (!jkxldr.ContainsKey(buildend))
                            {
                                jkxldr[buildend] = pt.Vol;
                            }
                            else
                                jkxldr[buildend] += pt.Vol;
                        }
                        else if (pt.Title.Contains("������·"))
                        {
                            if (!dlxldr.ContainsKey(buildend))
                            {
                                dlxldr[buildend] = pt.Vol;
                            }
                            else
                                dlxldr[buildend] += pt.Vol;
                        }
                    }
                    if (pb != null)
                    {
                        sublendrate += pb.LendRate;
                        subjgybei += pb.PrepChange;
                        subdyn += pb.DynInvest;
                    }
                    if (xspb != null)
                    {
                        xspblendrate += xspb.LendRate;
                        xspbjgybei += xspb.PrepChange;
                        xsdyn += xspb.DynInvest;
                    }
                    if (zspb != null)
                    {
                        zspblendrate += zspb.LendRate;
                        zspbjgybei += zspb.PrepChange;
                        zspbdyn += zspb.DynInvest;
                    }
                    if (zskg != null)
                    {
                        zskglendrate += zskg.LendRate;
                        zskgjgybei += zskg.PrepChange;
                        zskgdyn += zskg.DynInvest;
                    }
                    if (kbs != null)
                    {
                        kblendrate += kbs.LendRate;
                        kbjgybei += kbs.PrepChange;
                        kbdyn += kbs.DynInvest;
                    }
                    if (kg != null)
                    {
                        if (!zskgdr.ContainsKey(buildend))
                        {
                            zskgdr[buildend] = zskgjiage * kg.Num3;
                        }
                        else
                            zskgdr[buildend] += zskgjiage * kg.Num3;
                        if (!kbsdr.ContainsKey(buildend))
                        {
                            kbsdr[buildend] = kbsjiage * kg.Num1;
                        }
                        else
                            kbsdr[buildend] += kbsjiage * kg.Num1;
                    }
                    if (sub != null)
                    {
                        if (!zspddr.ContainsKey(buildend))
                        {
                            zspddr[buildend] = zspbjiage * sub.Num5;
                        }
                        else
                            zspddr[buildend] += zspbjiage * sub.Num5;
                        if (!xspddr.ContainsKey(buildend))
                        {
                            xspddr[buildend] = xspdjiage * sub.Num3;
                        }
                        else
                            xspddr[buildend] += xspdjiage * sub.Num3;
                    }

                }
                listcity6i = 11;
               
                fpSpread1.Sheets[16].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
               // fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].CellType = numberCellTypes1;
                fpSpread1.Sheets[16].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "����6KV����");
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "��·");

                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1, 0);
                for (int j = 2011; j <= 2015; j++)
                {
                    string y = j.ToString();
                    if (linedr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 2 + j - 2011, linedr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 7].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3:R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 8, linelendrate);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 9, linejgybei);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 10, linedyn);
                fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "�����");

                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1, 0);
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (subdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 2 + j - 2011, subdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 7].Formula = "SUM(R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 8, sublendrate);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 9, subjgybei);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 10, subdyn);
                fpSpread1.Sheets[16].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����
                fpSpread1.Sheets[16].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[16].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "����6KV����");
                fpSpread1.Sheets[16].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "���Ͽ���");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (zskgdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 2 + j - 2011, zskgdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 7].Formula = "SUM(R" + (9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3:R" + (9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 8, zskglendrate);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 9, zskgjgybei);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 10, zskgdyn);

                fpSpread1.Sheets[16].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "��������ѹ��");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (zspddr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 2 + j - 2011, zspddr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 7].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3:R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 8, zspblendrate);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 9, zspbjgybei);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 10, zspbdyn);
                fpSpread1.Sheets[16].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "��ʽ���վ");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (xspddr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 2 + j - 2011, xspddr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 7].Formula = "SUM(R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 8, xspblendrate);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 9, xspbjgybei);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 10, xsdyn);

                fpSpread1.Sheets[16].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "������");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (kbsdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 2 + j - 2011, kbsdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 7].Formula = "SUM(R" + (12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3:R" + (12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 8, kblendrate);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 9, kbjgybei);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 10, kbdyn);

                fpSpread1.Sheets[16].Rows[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[16].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "������·");
                fpSpread1.Sheets[16].Rows[12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "���У��ܿ���·");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (jkxldr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 2 + j - 2011, jkxldr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 7].Formula = "SUM(R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3:R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 8, jkxllendrate);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 9, jkxljgybei);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 10, jkxldyn);


                fpSpread1.Sheets[16].Rows[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "      ������·");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (dlxldr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 2 + j - 2011, dlxldr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 7].Formula = "SUM(R" + (15 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3:R" + (15 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 8, dlxllendrate);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 9, dlxljgybei);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 10, dlxldyn);


                fpSpread1.Sheets[16].Rows[14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            }
            int listcity3i = 0;
            if (listcity3sl.Count != 0)
            {
                string buildyear = "", buildend = "";
                double linelenth = 0, subvol = 0; double subnum = 0; //�ֱ�Ϊ��·�ĳ��ȣ����վ�������ͱ��̨��
                 double linelendrate = 0, linejgybei = 0, linedyn = 0, sublendrate = 0, subjgybei = 0, subdyn = 0, zskglendrate = 0, zskgjgybei = 0, zskgdyn = 0, zspblendrate = 0, zspbjgybei = 0;
                double zspbdyn = 0, xspblendrate = 0, xspbjgybei = 0, xsdyn = 0, kblendrate = 0, kbjgybei = 0, kbdyn = 0, jkxllendrate = 0, jkxljgybei = 0, jkxldyn = 0, dlxllendrate = 0, dlxljgybei = 0, dlxldyn = 0;
                Dictionary<string, double> linedr = new Dictionary<string, double>();
                Dictionary<string, double> subdr = new Dictionary<string, double>();
                Dictionary<string, double> zskgdr = new Dictionary<string, double>();
                Dictionary<string, double> zspddr = new Dictionary<string, double>();
                Dictionary<string, double> xspddr = new Dictionary<string, double>();
                Dictionary<string, double> kbsdr = new Dictionary<string, double>();
                Dictionary<string, double> dlxldr = new Dictionary<string, double>();
                Dictionary<string, double> jkxldr = new Dictionary<string, double>();
                for (int j = 0; j < listcity3sl.Count; j++)
                {
                    buildyear = listcity3sl[j].BuildYear;
                    buildend = listcity3sl[j].BuildEd;
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity3sl[j].ID + "' and Col4 = 'pw-line'";
                    Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity3sl[j].ID + "' and Col4 = 'pw-pb'";
                    Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);

                    con = "ProjectID='" + ProjectUID + "'and ParentID='" + listcity3sl[j].ID + "' and Col4 = 'pw-kg'";
                    Ps_Table_TZGS kg = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                    con = "ProjectID='" + listcity3sl[j].ID + "'and Typeqf='pw-line'";
                    IList list1 = Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity3sl[j].ID + "'and Typeqf='pw-pb'and Title like'�����'";
                    Ps_Table_TZMX pb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity3sl[j].ID + "'and Typeqf='pw-pb'and Title like'��������ѹ��'";
                    Ps_Table_TZMX zspb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity3sl[j].ID + "'and Typeqf='pw-pb'and Title like'��ʽ���վ'";
                    Ps_Table_TZMX xspb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);

                    con = "ProjectID='" + listcity3sl[j].ID + "'and Typeqf='pw-kg'and Title like'���Ͽ���'";
                    Ps_Table_TZMX zskg = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                    con = "ProjectID='" + listcity3sl[j].ID + "'and Typeqf='pw-kg'and Title like'������'";
                    Ps_Table_TZMX kbs = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);

                    con = "S1='3' and Name like'%���Ͽ���%'and S5='3'";
                    double zskgjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='3' and Name like'%��������ѹ��%'and S5='3'";
                    double zspbjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='3' and Name like'%��ʽ���վ%'and S5='3'";
                    double xspdjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    con = "S1='3' and Name like'%������%'and S5='3'";
                    double kbsjiage = Convert.ToDouble(Services.BaseService.GetObject("SelectProject_Sum_NUM", con));
                    if (!linedr.ContainsKey(buildend))
                    {
                        linedr[buildend] = line.Amount;
                    }
                    else
                        linedr[buildend] += line.Amount;
                    if (!subdr.ContainsKey(buildend))
                    {
                        subdr[buildend] = sub.Amount;
                    }
                    else
                    subdr[buildend] += sub.Amount;

                    foreach (Ps_Table_TZMX pt in list1)
                    {
                        linelendrate += pt.LendRate;
                        linejgybei += pt.PrepChange;
                        linedyn += pt.DynInvest;
                        if (pt.Title.Contains("�ܿ���·"))
                        {
                            if (!jkxldr.ContainsKey(buildend))
                            {
                                jkxldr[buildend] = pt.Vol;
                            }
                            else
                                jkxldr[buildend] += pt.Vol;
                        }
                        else if (pt.Title.Contains("������·"))
                        {
                            if (!dlxldr.ContainsKey(buildend))
                            {
                                dlxldr[buildend] = pt.Vol;
                            }
                            else
                                dlxldr[buildend] += pt.Vol;
                        }
                    }
                    if (pb != null)
                    {
                        sublendrate += pb.LendRate;
                        subjgybei += pb.PrepChange;
                        subdyn += pb.DynInvest;
                    }
                    if (xspb != null)
                    {
                        xspblendrate += xspb.LendRate;
                        xspbjgybei += xspb.PrepChange;
                        xsdyn += xspb.DynInvest;
                    }
                    if (zspb != null)
                    {
                        zspblendrate += zspb.LendRate;
                        zspbjgybei += zspb.PrepChange;
                        zspbdyn += zspb.DynInvest;
                    }
                    if (zskg != null)
                    {
                        zskglendrate += zskg.LendRate;
                        zskgjgybei += zskg.PrepChange;
                        zskgdyn += zskg.DynInvest;
                    }
                    if (kbs != null)
                    {
                        kblendrate += kbs.LendRate;
                        kbjgybei += kbs.PrepChange;
                        kbdyn += kbs.DynInvest;
                    }
                    if (kg != null)
                    {
                        if (!zskgdr.ContainsKey(buildend))
                        {
                            zskgdr[buildend] = zskgjiage * kg.Num3;
                        }
                        else
                        zskgdr[buildend] += zskgjiage * kg.Num3;
                        if (!kbsdr.ContainsKey(buildend))
                        {
                            kbsdr[buildend] = kbsjiage * kg.Num1;
                        }
                        else
                        kbsdr[buildend] += kbsjiage * kg.Num1;
                    }
                    if (sub != null)
                    {
                        if (!zspddr.ContainsKey(buildend))
                        {
                            zspddr[buildend] = zspbjiage * sub.Num5;
                        }
                        else
                        zspddr[buildend] += zspbjiage * sub.Num5;
                        if (!xspddr.ContainsKey(buildend))
                        {
                            xspddr[buildend] = xspdjiage * sub.Num3;
                        }
                        else
                        xspddr[buildend] += xspdjiage * sub.Num3;
                    }

                }
                listcity3i = 11;
               
                fpSpread1.Sheets[16].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
               // fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].CellType = numberCellTypes1;
                fpSpread1.Sheets[16].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "����3KV����");
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "��·");

                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1, 0);
                for (int j = 2011; j <= 2015; j++)
                {
                    string y = j.ToString();
                    if (linedr.ContainsKey(y) )
                    {
                        fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 2 + j - 2011, linedr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 7].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3:R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 8, linelendrate);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 9, linejgybei);
                fpSpread1.Sheets[16].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 10, linedyn);
                fpSpread1.Sheets[16].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "�����");

                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1, 0);
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (subdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 2 + j - 2011, subdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 7].Formula = "SUM(R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 8, sublendrate);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 9, subjgybei);
                fpSpread1.Sheets[16].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 10, subdyn);
                fpSpread1.Sheets[16].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����
                fpSpread1.Sheets[16].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[16].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "����6KV����");
                fpSpread1.Sheets[16].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[16].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "���Ͽ���");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (zskgdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 2 + j - 2011, zskgdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 7].Formula = "SUM(R" + (9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3:R" + (9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 8, zskglendrate);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 9, zskgjgybei);
                fpSpread1.Sheets[16].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 10, zskgdyn);

                fpSpread1.Sheets[16].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "��������ѹ��");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (zspddr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 2 + j - 2011, zspddr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 7].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3:R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 8, zspblendrate);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 9, zspbjgybei);
                fpSpread1.Sheets[16].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 10, zspbdyn);
                fpSpread1.Sheets[16].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "��ʽ���վ");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (xspddr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 2 + j - 2011, xspddr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 7].Formula = "SUM(R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 8, xspblendrate);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 9, xspbjgybei);
                fpSpread1.Sheets[16].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 10, xsdyn);

                fpSpread1.Sheets[16].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "������");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (kbsdr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 2 + j - 2011, kbsdr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 7].Formula = "SUM(R" + (12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3:R" + (12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 8, kblendrate);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 9, kbjgybei);
                fpSpread1.Sheets[16].SetValue(11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 10, kbdyn);

                fpSpread1.Sheets[16].Rows[11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[16].SetValue(12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "������·");
                fpSpread1.Sheets[16].Rows[12 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "���У��ܿ���·");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (jkxldr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 2 + j - 2011, jkxldr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 7].Formula = "SUM(R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3:R" + (14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 8, jkxllendrate);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 9, jkxljgybei);
                fpSpread1.Sheets[16].SetValue(13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 10, jkxldyn);


                fpSpread1.Sheets[16].Rows[13 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[16].Rows.Add(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "      ������·");
                for (int j = 2011; j < 2015; j++)
                {
                    string y = j.ToString();
                    if (dlxldr.ContainsKey(y))
                    {
                        fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 2 + j - 2011, dlxldr[y]);
                    }
                }
                fpSpread1.Sheets[16].Cells[14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 7].Formula = "SUM(R" + (15 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3:R" + (15 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C7)";
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 8, dlxllendrate);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 9, dlxljgybei);
                fpSpread1.Sheets[16].SetValue(14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 10, dlxldyn);


                fpSpread1.Sheets[16].Rows[14 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            }
            #endregion
            //ũ��66KV���� �ڴ��ڼ�Ҫ�����غ�ͳ����·�ͱ���������� ����
            int citynum = list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i + listcity3i;
            int listcountry66i = 0;
            if (flag66l)
            {
                listcountry66i = 1;
                fpSpread1.Sheets[16].Rows.Add(4 + citynum, 1);
                fpSpread1.Sheets[16].SetValue(4 + citynum, 0, "ũ��66KV����");
                fpSpread1.Sheets[16].Rows[4 + citynum].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {
                    if (listcountry66sl.ContainsKey(area.Title))
                    {
                        //ͳ�ƺ���ӽ��
                        Dictionary<string, double> linedr = new Dictionary<string, double>();
                        Dictionary<string, double> subdr = new Dictionary<string, double>();
                        double linelendrate = 0, linejgybei = 0, linedyn = 0, sublendrate = 0, subjgybei = 0, subdyn = 0;
                        string buildyear, buildend;
                        foreach (Ps_Table_TZGS pt in listcountry66sl[area.Title])
                        {
                            buildyear = pt.BuildYear; buildend = pt.BuildEd;
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-line'";
                            Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                           

                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-pb'";
                            Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            if (!linedr.ContainsKey(buildend))
                            {
                                linedr[buildend] = line.Amount;
                            }
                            else
                                linedr[buildend] += line.Amount;
                            if (!subdr.ContainsKey(buildend))
                            {
                                subdr[buildend] = sub.Amount;
                            }
                            else
                            subdr[buildend] += sub.Amount;

                            con = "ProjectID='" + pt.ID + "'and Typeqf='pw-line'";
                            IList list1 = Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
                            con = "ProjectID='" + pt.ID + "'and Typeqf='pw-pb'and Title like'�����'";
                            Ps_Table_TZMX pb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                            foreach (Ps_Table_TZMX px in list1)
                            {
                                linelendrate += px.LendRate;
                                linejgybei += px.PrepChange;
                                linedyn += px.DynInvest;
                            }
                            if (pb != null)
                            {
                                sublendrate += pb.LendRate;
                                subjgybei += pb.PrepChange;
                                subdyn += pb.DynInvest;
                            }
                        }
                        fpSpread1.Sheets[16].Rows.Add(5 + citynum, 1);
                        fpSpread1.Sheets[16].SetValue(5 + citynum, 0, area.Title);
                        fpSpread1.Sheets[16].Rows[5 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[16].Rows.Add(6 + citynum, 1);
                        fpSpread1.Sheets[16].SetValue(6 + citynum, 0, "���У���·");
                        fpSpread1.Sheets[16].Rows[6 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[16].Rows.Add(7 + citynum, 1);
                        fpSpread1.Sheets[16].SetValue(7 + citynum, 0, "    �����");
                        fpSpread1.Sheets[16].Rows[7 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry66i += 3;  //������¼
                        //��ֵ
                        for (int j = 2011; j <= 2015; j++)
                        {
                            string y = j.ToString();
                            if (linedr.ContainsKey(y) )
                            {
                                fpSpread1.Sheets[16].SetValue(6 + citynum, 2 + j - 2011, linedr[y]);
                            }
                        }
                        fpSpread1.Sheets[16].Cells[6 + citynum, 7].Formula = "SUM(R" + (7 + citynum).ToString() + "C3:R" + (7 + citynum).ToString() + "C7)";
                        fpSpread1.Sheets[16].SetValue(6 + citynum, 8, linelendrate);
                        fpSpread1.Sheets[16].SetValue(6 + citynum, 9, linejgybei);
                        fpSpread1.Sheets[16].SetValue(6 + citynum, 10, linedyn);

                        for (int j = 2011; j <= 2015; j++)
                        {
                            string y = j.ToString();
                            if (subdr.ContainsKey(y))
                            {
                                fpSpread1.Sheets[16].SetValue(7 + citynum, 2 + j - 2011, subdr[y]);
                            }
                        }
                        fpSpread1.Sheets[16].Cells[7 + citynum, 7].Formula = "SUM(R" + (8 + citynum).ToString() + "C3:R" + (8 + citynum).ToString() + "C7)";
                        fpSpread1.Sheets[16].SetValue(7 + citynum, 8, sublendrate);
                        fpSpread1.Sheets[16].SetValue(7 + citynum, 9, subjgybei);
                        fpSpread1.Sheets[16].SetValue(7 + citynum, 10, subdyn);
                        //��ֵͳ��


                    }
                }

            }
            //ũ��35KV��������
            int listcountry35i = 0;
            if (flag35l)
            {

                listcountry35i = 1;
                fpSpread1.Sheets[16].Rows.Add(4 + citynum + listcountry66i, 1);
                fpSpread1.Sheets[16].SetValue(4 + citynum + listcountry66i, 0, "ũ��35KV����");
                fpSpread1.Sheets[16].Rows[4 + citynum + listcountry66i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry35sl.ContainsKey(area.Title))
                    {
                        //ͳ�ƺ���ӽ��
                        Dictionary<string, double> linedr = new Dictionary<string, double>();
                        Dictionary<string, double> subdr = new Dictionary<string, double>();
                        double linelendrate = 0, linejgybei = 0, linedyn = 0, sublendrate = 0, subjgybei = 0, subdyn = 0;
                        string buildyear, buildend;
                        foreach (Ps_Table_TZGS pt in listcountry35sl[area.Title])
                        {
                            buildyear = pt.BuildYear; buildend = pt.BuildEd;
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-line'";
                            Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                           


                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-pb'";
                            Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            if (!linedr.ContainsKey(buildend))
                            {
                                linedr[buildend] = line.Amount;
                            }
                            else
                                linedr[buildend] += line.Amount;
                            if (!subdr.ContainsKey(buildend))
                            {
                                subdr[buildend] = sub.Amount;
                            }
                            else
                            subdr[buildend] += sub.Amount;
                            con = "ProjectID='" + pt.ID + "'and Typeqf='pw-line'";
                            IList list1 = Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
                            con = "ProjectID='" + pt.ID + "'and Typeqf='pw-pb'and Title like'�����'";
                            Ps_Table_TZMX pb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                            foreach (Ps_Table_TZMX px in list1)
                            {
                                linelendrate += px.LendRate;
                                linejgybei += px.PrepChange;
                                linedyn += px.DynInvest;
                            }
                            if (pb != null)
                            {
                                sublendrate += pb.LendRate;
                                subjgybei += pb.PrepChange;
                                subdyn += pb.DynInvest;
                            }
                        }
                        fpSpread1.Sheets[16].Rows.Add(5 + citynum + listcountry66i, 1);
                        fpSpread1.Sheets[16].SetValue(5 + citynum + listcountry66i, 0, area.Title);
                        fpSpread1.Sheets[16].Rows[5 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[16].Rows.Add(6 + citynum + listcountry66i, 1);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i, 0, "���У���·");
                        fpSpread1.Sheets[16].Rows[6 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[16].Rows.Add(7 + citynum + listcountry66i, 1);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i, 0, "    �����");
                        fpSpread1.Sheets[16].Rows[7 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry35i += 3;  //������¼
                        //��ֵ
                        for (int j = 2011; j <= 2015; j++)
                        {
                            string y = j.ToString();
                            if (linedr.ContainsKey(y) )
                            {
                                fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i, 2 + j - 2011, linedr[y]);
                            }
                        }
                        fpSpread1.Sheets[16].Cells[6 + citynum + listcountry66i, 7].Formula = "SUM(R" + (7 + citynum + listcountry66i).ToString() + "C3:R" + (7 + citynum + listcountry66i).ToString() + "C7)";
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i, 8, linelendrate);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i, 9, linejgybei);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i, 10, linedyn);

                        for (int j = 2011; j <= 2015; j++)
                        {
                            string y = j.ToString();
                            if (subdr.ContainsKey(y))
                            {
                                fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i, 2 + j - 2011, subdr[y]);
                            }
                        }
                        fpSpread1.Sheets[16].Cells[7 + citynum + listcountry66i, 7].Formula = "SUM(R" + (8 + citynum + listcountry66i).ToString() + "C3:R" + (8 + citynum + listcountry66i).ToString() + "C7)";
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i, 8, sublendrate);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i, 9, subjgybei);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i, 10, subdyn);
                        //��ֵͳ��


                    }

                }
            }
            //ũ��10KV����
            int listcountry10i = 0;
            if (flag10l)
            {
                listcountry10i = 1;
                fpSpread1.Sheets[16].Rows.Add(4 + citynum + listcountry66i + listcountry35i, 1);
                fpSpread1.Sheets[16].SetValue(4 + citynum + listcountry66i + listcountry35i, 0, "ũ��10KV����");
                fpSpread1.Sheets[16].Rows[4 + citynum + listcountry66i + listcountry35i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry10sl.ContainsKey(area.Title))
                    {
                        //ͳ�ƺ���ӽ��
                        Dictionary<string, double> linedr = new Dictionary<string, double>();
                        Dictionary<string, double> subdr = new Dictionary<string, double>();
                        double linelendrate = 0, linejgybei = 0, linedyn = 0, sublendrate = 0, subjgybei = 0, subdyn = 0;
                        string buildyear, buildend;
                        foreach (Ps_Table_TZGS pt in listcountry10sl[area.Title])
                        {
                            buildyear = pt.BuildYear; buildend = pt.BuildEd;
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-line'";
                            Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            


                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-pb'";
                            Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            if (!linedr.ContainsKey(buildend))
                            {
                                linedr[buildend] = line.Amount;
                            }
                            else
                                linedr[buildend] += line.Amount;
                            if (!subdr.ContainsKey(buildend))
                            {
                                subdr[buildend] = sub.Amount;
                            }
                            else
                            subdr[buildend] += sub.Amount;
                            con = "ProjectID='" + pt.ID + "'and Typeqf='pw-line'";
                            IList list1 = Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
                            con = "ProjectID='" + pt.ID + "'and Typeqf='pw-pb'and Title like'�����'";
                            Ps_Table_TZMX pb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                            foreach (Ps_Table_TZMX px in list1)
                            {
                                linelendrate += px.LendRate;
                                linejgybei += px.PrepChange;
                                linedyn += px.DynInvest;
                            }
                            if (pb != null)
                            {
                                sublendrate += pb.LendRate;
                                subjgybei += pb.PrepChange;
                                subdyn += pb.DynInvest;
                            }
                        }
                        fpSpread1.Sheets[16].Rows.Add(5 + citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[16].SetValue(5 + citynum + listcountry66i + listcountry35i, 0, area.Title);
                        fpSpread1.Sheets[16].Rows[5 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[16].Rows.Add(6 + citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i, 0, "���У���·");
                        fpSpread1.Sheets[16].Rows[6 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[16].Rows.Add(7 + citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i, 0, "    �����");
                        fpSpread1.Sheets[16].Rows[7 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry10i += 3;  //������¼
                        //��ֵ
                        for (int j = 2011; j <= 2015; j++)
                        {
                            string y = j.ToString();
                            if (linedr.ContainsKey(y))
                            {
                                fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i, 2 + j - 2011, linedr[y]);
                            }
                        }
                        fpSpread1.Sheets[16].Cells[6 + citynum + listcountry66i + listcountry35i, 7].Formula = "SUM(R" + (7 + citynum + listcountry66i + listcountry35i).ToString() + "C3:R" + (7 + citynum + listcountry66i + listcountry35i).ToString() + "C7)";
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i, 8, linelendrate);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i, 9, linejgybei);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i, 10, linedyn);

                        for (int j = 2011; j <= 2015; j++)
                        {
                            string y = j.ToString();
                            if (subdr.ContainsKey(y))
                            {
                                fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i, 2 + j - 2011, subdr[y]);
                            }
                        }
                        fpSpread1.Sheets[16].Cells[7 + citynum + listcountry66i + listcountry35i, 7].Formula = "SUM(R" + (8 + citynum + listcountry66i + listcountry35i).ToString() + "C3:R" + (8 + citynum + listcountry66i + listcountry35i).ToString() + "C7)";
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i, 8, sublendrate);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i, 9, subjgybei);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i, 10, subdyn);
                        //��ֵͳ��

                    }

                }
            }
            //ũ��6KV����
            int listcountry6i = 0;
            if (flag6l)
            {
                listcountry6i = 1;
                fpSpread1.Sheets[16].Rows.Add(4 + citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                fpSpread1.Sheets[16].SetValue(4 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, "ũ��6KV����");
                fpSpread1.Sheets[16].Rows[4 + citynum + listcountry66i + listcountry35i + listcountry10i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry6sl.ContainsKey(area.Title))
                    {
                        //ͳ�ƺ���ӽ��
                        Dictionary<string, double> linedr = new Dictionary<string, double>();
                        Dictionary<string, double> subdr = new Dictionary<string, double>();
                        double linelendrate = 0, linejgybei = 0, linedyn = 0, sublendrate = 0, subjgybei = 0, subdyn = 0;
                        string buildyear, buildend;
                        foreach (Ps_Table_TZGS pt in listcountry6sl[area.Title])
                        {
                            buildyear = pt.BuildYear; buildend = pt.BuildEd;
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-line'";
                            Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                           

                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-pb'";
                            Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            if (!linedr.ContainsKey(buildend))
                            {
                                linedr[buildend] = line.Amount;
                            }
                            else
                                linedr[buildend] += line.Amount;
                            if (!subdr.ContainsKey(buildend))
                            {
                                subdr[buildend] = sub.Amount;
                            }
                            else
                            subdr[buildend] += sub.Amount;

                            con = "ProjectID='" + pt.ID + "'and Typeqf='pw-line'";
                            IList list1 = Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
                            con = "ProjectID='" + pt.ID + "'and Typeqf='pw-pb'and Title like'�����'";
                            Ps_Table_TZMX pb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                            foreach (Ps_Table_TZMX px in list1)
                            {
                                linelendrate += px.LendRate;
                                linejgybei += px.PrepChange;
                                linedyn += px.DynInvest;
                            }
                            if (pb != null)
                            {
                                sublendrate += pb.LendRate;
                                subjgybei += pb.PrepChange;
                                subdyn += pb.DynInvest;
                            }
                        }
                        fpSpread1.Sheets[16].Rows.Add(5 + citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[16].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, area.Title);
                        fpSpread1.Sheets[16].Rows[5 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[16].Rows.Add(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, "���У���·");
                        fpSpread1.Sheets[16].Rows[6 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[16].Rows.Add(7 + citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, "    �����");
                        fpSpread1.Sheets[16].Rows[7 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry6i += 3;  //������¼
                        //��ֵ
                        for (int j = 2011; j <= 2015; j++)
                        {
                            string y = j.ToString();
                            if (linedr.ContainsKey(y))
                            {
                                fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 2 + j - 2011, linedr[y]);
                            }
                        }
                        fpSpread1.Sheets[16].Cells[6 + citynum + listcountry66i + listcountry35i + listcountry10i, 7].Formula = "SUM(R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C3:R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C7)";
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 8, linelendrate);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 9, linejgybei);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 10, linedyn);

                        for (int j = 2011; j <= 2015; j++)
                        {
                            string y = j.ToString();
                            if (subdr.ContainsKey(y))
                            {
                                fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i + listcountry10i, 2 + j - 2011, subdr[y]);
                            }
                        }
                        fpSpread1.Sheets[16].Cells[7 + citynum + listcountry66i + listcountry35i + listcountry10i, 7].Formula = "SUM(R" + (8 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C3:R" + (8 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C7)";
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i + listcountry10i, 8, sublendrate);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i + listcountry10i, 9, subjgybei);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i + listcountry10i, 10, subdyn);
                        //��ֵͳ��


                    }

                }
            }
            //ũ��3KV����
            int listcountry3i = 0;
            if (flag3l)
            {
                listcountry3i = 1;
                fpSpread1.Sheets[16].Rows.Add(4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                fpSpread1.Sheets[16].SetValue(4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, "ũ��3KV����");
                fpSpread1.Sheets[16].Rows[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].Font = new Font("����", 9, FontStyle.Bold);
                fpSpread1.Sheets[16].Rows[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //����������ͳ����·�ͱ����
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry3sl.ContainsKey(area.Title))
                    {
                        //ͳ�ƺ���ӽ��
                        Dictionary<string, double> linedr = new Dictionary<string, double>();
                        Dictionary<string, double> subdr = new Dictionary<string, double>();
                        double linelendrate = 0, linejgybei = 0, linedyn = 0, sublendrate = 0, subjgybei = 0, subdyn = 0;
                        string buildyear, buildend;
                        foreach (Ps_Table_TZGS pt in listcountry3sl[area.Title])
                        {
                            buildyear = pt.BuildYear; buildend = pt.BuildEd;
                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-line'";
                            Ps_Table_TZGS line = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                           


                            con = "ProjectID='" + ProjectUID + "'and ParentID='" + pt.ID + "' and Col4 = 'pw-pb'";
                            Ps_Table_TZGS sub = (Ps_Table_TZGS)Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", con);
                            if (!linedr.ContainsKey(buildend))
                            {
                                linedr[buildend] = line.Amount;
                            }
                            else
                                linedr[buildend] += line.Amount;
                            if (!subdr.ContainsKey(buildend))
                            {
                                subdr[buildend] = sub.Amount;
                            }
                            else
                            subdr[buildend] += sub.Amount;
                            con = "ProjectID='" + pt.ID + "'and Typeqf='pw-line'";
                            IList list1 = Services.BaseService.GetList("SelectPs_Table_TZMXByValue", con);
                            con = "ProjectID='" + pt.ID + "'and Typeqf='pw-pb'and Title like'�����'";
                            Ps_Table_TZMX pb = (Ps_Table_TZMX)Services.BaseService.GetObject("SelectPs_Table_TZMXByValue", con);
                            foreach (Ps_Table_TZMX px in list1)
                            {
                                linelendrate += px.LendRate;
                                linejgybei += px.PrepChange;
                                linedyn += px.DynInvest;
                            }
                            if (pb != null)
                            {
                                sublendrate += pb.LendRate;
                                subjgybei += pb.PrepChange;
                                subdyn += pb.DynInvest;
                            }
                        }
                        fpSpread1.Sheets[16].Rows.Add(5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[16].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, area.Title);
                        fpSpread1.Sheets[16].Rows[5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[16].Rows.Add(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, "���У���·");
                        fpSpread1.Sheets[16].Rows[6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[16].Rows.Add(7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, "    �����");
                        fpSpread1.Sheets[16].Rows[7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry3i += 3;  //������¼
                        //��ֵ
                        for (int j = 2011; j <= 2015; j++)
                        {
                            string y = j.ToString();
                            if (linedr.ContainsKey(y))
                            {
                                fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 2 + j - 2011, linedr[y]);
                            }
                        }
                        fpSpread1.Sheets[16].Cells[6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 7].Formula = "SUM(R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C3:R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C7)";
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 8, linelendrate);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 9, linejgybei);
                        fpSpread1.Sheets[16].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 10, linedyn);

                        for (int j = 2011; j <= 2015; j++)
                        {
                            string y = j.ToString();
                            if (subdr.ContainsKey(y))
                            {
                                fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 2 + j - 2011, subdr[y]);
                            }
                        }
                        fpSpread1.Sheets[16].Cells[7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 7].Formula = "SUM(R" + (8 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C3:R" + (8 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C7)";
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 8, sublendrate);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 9, subjgybei);
                        fpSpread1.Sheets[16].SetValue(7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 10, subdyn);
                        //��ֵͳ��

                    }

                }
                //�趨��ʽ

            }
            #endregion
            #endregion
            Sheet_GridandColor(fpSpread1.Sheets[16], 4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i + listcountry3i, 11);
            SetSheetViewColumnsWhith(fpSpread1.Sheets[16], 0, "ũ��35kV�����µ������ϼƣ�");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[16], 7, "��̬��Ͷ��");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[16], 8, "�����ڴ�����Ϣ");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[16], 9, "�۸�Ԥ����");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[16], 10, "��̬Ͷ��");

        }
        //����������Ŀ�ʽ�������ܱ�
        private class sdata
        {
            //��Ź�������
            public string title = "";
            //�����������
            public string str1 = "";
            public string str2 = "";

        }
        //sd���ڴ�ű������Щ�ֹ�д��Ĳ��֣�ÿ����������ʱ
        //�Ȱѱ������д���ִ�����������������ɺ���д����д����
        //ÿ������д�ı�񣨲��̶�����������������һ���ڸ���ǰд����д���ݣ�һ���ڸ��º�д����д����
        List<sdata> sd = new List<sdata>();
        private void dwzjhzb()
        {
#region �������
            fpSpread1.Sheets[17].RowCount = 0;
            fpSpread1.Sheets[17].ColumnCount = 0;
            fpSpread1.Sheets[17].RowCount = 37;
            fpSpread1.Sheets[17].ColumnCount = 8;
            fpSpread1.Sheets[17].SetValue(0, 0, "��ʮ���塱����������Ŀ�ʽ�������ܱ�");
            fpSpread1.Sheets[17].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[17].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[17].Cells[0, 0].ColumnSpan = 8;

            fpSpread1.Sheets[17].SetValue(1, 0, "��λ����Ԫ�������ǧ��������ǧ��");
            fpSpread1.Sheets[17].Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[17].Cells[1, 0].ColumnSpan = 8;

            fpSpread1.Sheets[17].SetValue(2, 0, "��Ŀ����");
            fpSpread1.Sheets[17].Cells[2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[17].Cells[2, 0].RowSpan = 2;
            SetSheetViewColumnsWhith(fpSpread1.Sheets[17], 0, "5������(���Ͽ��ء�������)");
            fpSpread1.Sheets[17].SetValue(2, 1, "�����ģ");
            fpSpread1.Sheets[17].Cells[2, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[17].Cells[2,1].ColumnSpan = 5;

          

            fpSpread1.Sheets[17].SetValue(2, 6, "��ʮ���塱��̬Ͷ�ʺϼ�");
            fpSpread1.Sheets[17].Cells[2, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[17].Cells[2, 6].RowSpan = 2;

            SetSheetViewColumnsWhith(fpSpread1.Sheets[17], 6, "��ʮ���塱��̬Ͷ�ʺϼ�");

            fpSpread1.Sheets[17].SetValue(2, 7, "��ʮ���塱��̬Ͷ�ʺϼ�");
            fpSpread1.Sheets[17].Cells[2, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[17].Cells[2, 7].RowSpan = 2;

            SetSheetViewColumnsWhith(fpSpread1.Sheets[17], 7, "��ʮ���塱��̬Ͷ�ʺϼ�");

            fpSpread1.Sheets[17].SetValue(3, 1, "��·�ܳ�");
            fpSpread1.Sheets[17].Cells[3, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            SetSheetViewColumnsWhith(fpSpread1.Sheets[17],1, "��·�ܳ�");

            fpSpread1.Sheets[17].SetValue(3, 2, "����");
            fpSpread1.Sheets[17].Cells[3, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            SetSheetViewColumnsWhith(fpSpread1.Sheets[17], 2, "��·�ܳ�");

            fpSpread1.Sheets[17].SetValue(3, 3, "����̨��");
            fpSpread1.Sheets[17].Cells[3, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            SetSheetViewColumnsWhith(fpSpread1.Sheets[17], 1, "��·�ܳ�");

            fpSpread1.Sheets[17].SetValue(3, 4, "����������");
            fpSpread1.Sheets[17].Cells[3, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            SetSheetViewColumnsWhith(fpSpread1.Sheets[17], 1, "����������");

            fpSpread1.Sheets[17].SetValue(3, 5, "����");
            fpSpread1.Sheets[17].Cells[3, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            SetSheetViewColumnsWhith(fpSpread1.Sheets[17], 1, "����������");

           
            fpSpread1.Sheets[17].SetValue(4, 0, "һ��220kV����");
            fpSpread1.Sheets[17].Cells[4, 0].Font= new Font("����", 9, FontStyle.Bold);
            //fpSpread1.Sheets[17].Rows[4].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[4, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(5, 0, "1����·");
            //fpSpread1.Sheets[17].Rows[5].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[5, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            fpSpread1.Sheets[17].SetValue(6, 0, "2�������");
           // fpSpread1.Sheets[17].Rows[6].CellType = numberCellTypes1;
            
            fpSpread1.Sheets[17].Cells[6, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(7, 0, "3���޹�����");
            //fpSpread1.Sheets[17].Rows[7].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[7, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(8, 0, "����110kV����");
            fpSpread1.Sheets[17].Cells[8, 0].Font = new Font("����", 9, FontStyle.Bold);
            //fpSpread1.Sheets[17].Rows[8].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[8, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(9, 0, "1����·");
           // fpSpread1.Sheets[17].Rows[9].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[9, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(10, 0, "2�������");
            //fpSpread1.Sheets[17].Rows[10].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[10, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            
            fpSpread1.Sheets[17].SetValue(11, 0, "3���޹�����");
           // fpSpread1.Sheets[17].Rows[11].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[11, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(12, 0, "��������ϵͳ");
            fpSpread1.Sheets[17].Cells[12, 0].Font = new Font("����", 9, FontStyle.Bold);
            //fpSpread1.Sheets[17].Rows[12].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[12, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(13, 0, "1���̵籣��ϵͳ");
            //fpSpread1.Sheets[17].Rows[13].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[13, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(14, 0, "2����ȫ�Զ�װ��");
           // fpSpread1.Sheets[17].Rows[14].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[14, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(15, 0, "3��ͨ��ϵͳ");
           // fpSpread1.Sheets[17].Rows[15].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[15, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(16, 0, "4�������Զ���ϵͳ");
            //fpSpread1.Sheets[17].Rows[16].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[16, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(17, 0, "�ġ�����35kV�����µ���");
            fpSpread1.Sheets[17].Cells[17, 0].Font = new Font("����", 9, FontStyle.Bold);
           // fpSpread1.Sheets[17].Rows[17].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[17, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(18, 0, "1��35kV��·");
            //fpSpread1.Sheets[17].Rows[18].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[18, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(19, 0, "2��35kV�����");
            //fpSpread1.Sheets[17].Rows[19].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[19, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(20, 0, "3��10kV��·");
            //fpSpread1.Sheets[17].Rows[20].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[20, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(21, 0, "4��10kV���");
            //fpSpread1.Sheets[17].Rows[21].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[21, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(22, 0, "5������(���Ͽ��ء�������)");
            //fpSpread1.Sheets[17].Rows[22].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[22, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(23, 0, "6���޹�����");
           // fpSpread1.Sheets[17].Rows[23].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[23, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(24, 0, "7�����й��õ�ѹ��");
            //fpSpread1.Sheets[17].Rows[24].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[24, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(25, 0, "8�������Զ���");
           // fpSpread1.Sheets[17].Rows[25].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[25, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(26, 0, "�塢ũ��35kV�����µ���");
            fpSpread1.Sheets[17].Cells[26, 0].Font = new Font("����", 9, FontStyle.Bold);
            //fpSpread1.Sheets[17].Rows[26].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[26, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(27, 0, "1��35kV��·");
            //fpSpread1.Sheets[17].Rows[27].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[27, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(28, 0, "2��35kV�����");
            //fpSpread1.Sheets[17].Rows[28].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[28, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(29, 0, "3��10kV��·");
            //fpSpread1.Sheets[17].Rows[29].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[29, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(30, 0, "4��10kV�����");
            //fpSpread1.Sheets[17].Rows[30].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[30, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(31, 0, "5������(���Ͽ��ء�������)");
            //fpSpread1.Sheets[17].Rows[31].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[31, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(32, 0, "6���ع��õ�ѹ��");
            //fpSpread1.Sheets[17].Rows[32].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[32, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            
            fpSpread1.Sheets[17].SetValue(33, 0, "7���ص��Զ���");
            //fpSpread1.Sheets[17].Rows[33].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[33, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(34, 0, "8��ͨ��ϵͳ");
            //fpSpread1.Sheets[17].Rows[34].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[34, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(35, 0, "9�������Զ���");
            //fpSpread1.Sheets[17].Rows[35].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[35, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

           
            fpSpread1.Sheets[17].SetValue(36, 0, "ȫ�����ϼ�");
            fpSpread1.Sheets[17].Cells[36, 0].Font = new Font("����", 9, FontStyle.Bold);
            //fpSpread1.Sheets[17].Rows[36].CellType = numberCellTypes1;
            fpSpread1.Sheets[17].Cells[36, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
#endregion

            #region �γɱ����������
            double length = 0, subzs = 0, subts = 0, subvol = 0, jttz = 0, dttz = 0;
            //220kv ��·�ͱ��վ
            fpSpread1.Sheets[17].Cells[4,5].Locked=true;
           string con = " and a.ProjectID='" + ProjectUID + "' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='220'";
            length = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSlinelength", con));

            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSlinejttz", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSlinedttz", con));
            fpSpread1.Sheets[17].SetValue(5, 1, length);
            fpSpread1.Sheets[17].SetValue(5, 6, jttz);
            fpSpread1.Sheets[17].SetValue(5, 7, dttz);
             fpSpread1.Sheets[17].Cells[5,5].Locked=true;
            subzs = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubnum", con));
            subts = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubTS", con));
            subvol = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubLL", con));
            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubjttz", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubdttz", con));

            fpSpread1.Sheets[17].SetValue(6, 2, subzs);
            fpSpread1.Sheets[17].SetValue(6, 3, subts);
            fpSpread1.Sheets[17].SetValue(6, 4, dttz);
             fpSpread1.Sheets[17].Cells[6,5].Locked=true;
            fpSpread1.Sheets[17].SetValue(6, 6, jttz);
            fpSpread1.Sheets[17].SetValue(6, 7, dttz);
            fpSpread1.Sheets[17].Cells[4, 1].Formula = "R6C2";
            fpSpread1.Sheets[17].Cells[4, 2].Formula = "R7C3";
            fpSpread1.Sheets[17].Cells[4, 3].Formula = "R7C4";
            fpSpread1.Sheets[17].Cells[4,4].Formula = "R7C5";
            fpSpread1.Sheets[17].Cells[4, 6].Formula = "SUM(R6C7:R8C7)";
            fpSpread1.Sheets[17].Cells[4, 7].Formula = "SUM(R6C8:R8C8)";
            for (int i = 0; i < 4;i++ )
            {
                fpSpread1.Sheets[17].Cells[7, 1 + i].Locked = true;
            }
            //110kv
            con = " and a.ProjectID='" + ProjectUID + "' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='110'";
            length = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSlinelength", con));

            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSlinejttz", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSlinedttz", con));
            fpSpread1.Sheets[17].SetValue(9, 1, length);
            fpSpread1.Sheets[17].SetValue(9, 6, jttz);
            fpSpread1.Sheets[17].SetValue(9, 7, dttz);
            fpSpread1.Sheets[17].Cells[9, 5].Locked = true;
            subzs = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubnum", con));
            subts = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubTS", con));
            subvol = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubLL", con));
            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubjttz", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubdttz", con));

            fpSpread1.Sheets[17].SetValue(10, 2, subzs);
            fpSpread1.Sheets[17].SetValue(10, 3, subts);
            fpSpread1.Sheets[17].SetValue(10, 4,subvol*10000);
            fpSpread1.Sheets[17].Cells[10, 5].Locked = true;
            fpSpread1.Sheets[17].SetValue(10, 6, jttz);
            fpSpread1.Sheets[17].SetValue(10, 7, dttz);
            fpSpread1.Sheets[17].Cells[8, 1].Formula = "R10C2";
            fpSpread1.Sheets[17].Cells[8, 2].Formula = "R11C3";
            fpSpread1.Sheets[17].Cells[8, 3].Formula = "R11C4";
            fpSpread1.Sheets[17].Cells[8, 4].Formula = "R11C5";
            fpSpread1.Sheets[17].Cells[8, 6].Formula = "SUM(R10C7:R12C7)";
            fpSpread1.Sheets[17].Cells[8, 7].Formula = "SUM(R10C8:R12C8)";
            for (int i = 0; i < 4; i++)
            {
                fpSpread1.Sheets[17].Cells[11, 1 + i].Locked = true;
            }
            //����ϵͳ
            for (int i = 0; i < 5;i++ )
            {
                for (int j = 0; j < 4; j++)
                {
                    fpSpread1.Sheets[17].Cells[12 + i, 1 + i].Locked = true;
                }
                
            }
            fpSpread1.Sheets[17].Cells[12, 6].Formula = "SUM(R14C7:R17C7)";
            fpSpread1.Sheets[17].Cells[12, 7].Formula = "SUM(R14C8:R17C8)";
            //����35kv�����µ���
            con = " and a.ProjectID='" + ProjectUID + "' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='35'and a.DQ='��Ͻ������' ";
            length = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinelength", con));
            jttz =  Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinejttz", con));
            dttz =  Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinedttz", con));
            fpSpread1.Sheets[17].SetValue(18, 1, length);
            fpSpread1.Sheets[17].Cells[18, 5].Locked = true;
            fpSpread1.Sheets[17].SetValue(18, 6, jttz);
            fpSpread1.Sheets[17].SetValue(18, 7, dttz);
            subzs =  Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubnum", con));
            subts =  Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubnum", con));
            subvol =  Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubll", con));
            jttz =  Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubjttz", con));
            dttz =  Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubdttz", con));
            fpSpread1.Sheets[17].SetValue(19, 2, subzs);
            fpSpread1.Sheets[17].SetValue(19, 3, subts);
            fpSpread1.Sheets[17].SetValue(19, 4,subvol*10000);
            fpSpread1.Sheets[17].Cells[19, 5].Locked = true;
            fpSpread1.Sheets[17].SetValue(19, 6, jttz);
            fpSpread1.Sheets[17].SetValue(19, 7, dttz);
            con = " and a.ProjectID='" + ProjectUID + "' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='10'and a.DQ='��Ͻ������' ";
            length = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinelength", con));
            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinejttz", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinedttz", con));
            fpSpread1.Sheets[17].SetValue(20, 1, length);
            fpSpread1.Sheets[17].Cells[20, 5].Locked = true;
            fpSpread1.Sheets[17].SetValue(20, 6, jttz);
            fpSpread1.Sheets[17].SetValue(20, 7, dttz);
            subzs = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubnum", con));
            subts = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubnum", con));
            subvol = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubll", con));
            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubjttz", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubdttz", con));
            fpSpread1.Sheets[17].SetValue(21, 2, subzs);
            fpSpread1.Sheets[17].SetValue(21, 3, subts);
            fpSpread1.Sheets[17].SetValue(21, 4, subvol * 10000);
            fpSpread1.Sheets[17].Cells[21, 5].Locked = true;
            fpSpread1.Sheets[17].SetValue(21, 6, jttz);
            fpSpread1.Sheets[17].SetValue(21, 7, dttz);
            for (int i = 0; i < 2;i++ )
            {
                for (int j = 0; j < 4;j++ )
                {
                    fpSpread1.Sheets[17].Cells[22 + i, 1 + j].Locked = true;
                }
            }
            for (int j = 0; j < 4;j++ )
            {
                fpSpread1.Sheets[17].Cells[25 , 1 + j].Locked = true;
            }
            con = " and a.ProjectID='" + ProjectUID + "' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)<='35'and a.DQ='��Ͻ������'";
            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWQTJTTZ", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWqtdttz", con));
            fpSpread1.Sheets[17].Cells[21, 5].Locked = true;
            fpSpread1.Sheets[17].SetValue(21, 6, jttz);
            fpSpread1.Sheets[17].SetValue(21, 7, dttz);

            fpSpread1.Sheets[17].Cells[17, 1].Formula = "R19C2+R21C2+R25C2";
            fpSpread1.Sheets[17].Cells[17, 2].Formula = "R20C3+R22C3+R25C3";
            fpSpread1.Sheets[17].Cells[17, 3].Formula = "R20C4+R22C4+R25C4";
            fpSpread1.Sheets[17].Cells[17, 4].Formula = "R20C5+R22C5+R25C5";
            fpSpread1.Sheets[17].Cells[17,6].Formula = "SUM(R19C7:R26C7)";
            fpSpread1.Sheets[17].Cells[17,7].Formula = "SUM(R19C8:R26C8)";

            //ũ��35kV�����µ���
            con = " and a.ProjectID='" + ProjectUID + "' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='35'and a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
            length = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinelength", con));
            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinejttz", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinedttz", con));
            fpSpread1.Sheets[17].SetValue(27, 1, length);
            fpSpread1.Sheets[17].Cells[27, 5].Locked = true;
            fpSpread1.Sheets[17].SetValue(27, 6, jttz);
            fpSpread1.Sheets[17].SetValue(27, 7, dttz);
            subzs = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubnum", con));
            subts = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubnum", con));
            subvol = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubll", con));
            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubjttz", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubdttz", con));
            fpSpread1.Sheets[17].SetValue(28, 2, subzs);
            fpSpread1.Sheets[17].SetValue(28, 3, subts);
            fpSpread1.Sheets[17].SetValue(28, 4, subvol * 10000);
            fpSpread1.Sheets[17].Cells[28, 5].Locked = true;
            fpSpread1.Sheets[17].SetValue(28, 6, jttz);
            fpSpread1.Sheets[17].SetValue(28, 7, dttz);
            con = " and a.ProjectID='" + ProjectUID + "' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='10'and a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
            length = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinelength", con));
            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinejttz", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWlinedttz", con));
            fpSpread1.Sheets[17].SetValue(29, 1, length);
            fpSpread1.Sheets[17].Cells[29, 5].Locked = true;
            fpSpread1.Sheets[17].SetValue(29, 6, jttz);
            fpSpread1.Sheets[17].SetValue(29, 7, dttz);
            subzs = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubnum", con));
            subts = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubnum", con));
            subvol = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubll", con));
            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubjttz", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWsubdttz", con));
            fpSpread1.Sheets[17].SetValue(30, 2, subzs);
            fpSpread1.Sheets[17].SetValue(30, 3, subts);
            fpSpread1.Sheets[17].SetValue(30, 4, subvol * 10000);
            fpSpread1.Sheets[17].Cells[30, 5].Locked = true;
            fpSpread1.Sheets[17].SetValue(30, 6, jttz);
            fpSpread1.Sheets[17].SetValue(30, 7, dttz);
         
            for (int j = 0; j < 4; j++)
            {
                fpSpread1.Sheets[17].Cells[31, 1 + j].Locked = true;
            }
            con = " and a.ProjectID='" + ProjectUID + "' and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)<='35'and a.DQ in('�ؼ�ֱ��ֱ��','�ؼ��ع�','�ؼ��ι�','�ؼ�����')";
            jttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWQTJTTZ", con));
            dttz = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSPWqtdttz", con));
            fpSpread1.Sheets[17].Cells[31, 5].Locked = true;
            fpSpread1.Sheets[17].Cells[32, 5].Locked = true;

            fpSpread1.Sheets[17].SetValue(31, 6, jttz);
            fpSpread1.Sheets[17].SetValue(31, 7, dttz);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    fpSpread1.Sheets[17].Cells[33 + i, 1 + j].Locked = true;
                }
            }
            fpSpread1.Sheets[17].Cells[26, 1].Formula = "R28C2+R30C2+R32C2";
            fpSpread1.Sheets[17].Cells[26, 2].Formula = "R29C3+R31C3+R32C3";
            fpSpread1.Sheets[17].Cells[26, 3].Formula = "R29C4+R31C4+R32C4";
            fpSpread1.Sheets[17].Cells[26, 4].Formula = "R29C5+R31C5+R32C5";
            fpSpread1.Sheets[17].Cells[26, 6].Formula = "SUM(R28C7:R36C7)";
            fpSpread1.Sheets[17].Cells[26, 7].Formula = "SUM(R28C8:R36C8)";

            //�ϼ�
            fpSpread1.Sheets[17].Cells[36, 1].Formula = "R5C2+R9C2+R18C2+R27C2";
            fpSpread1.Sheets[17].Cells[36, 2].Formula = "R5C3+R9C3+R18C3+R27C3";
            fpSpread1.Sheets[17].Cells[36, 3].Formula = "R5C4+R9C4+R18C4+R27C4";
            fpSpread1.Sheets[17].Cells[36, 4].Formula = "R5C5+R9C5+R18C5+R27C5";

            fpSpread1.Sheets[17].Cells[36, 6].Formula = "R5C7+R9C7+R13C7+R18C7+R27C7";
            fpSpread1.Sheets[17].Cells[36, 7].Formula = "R5C8+R9C8+R13C8+R18C8+R27C8";
            #endregion
            Sheet_GridandColor(fpSpread1.Sheets[17],37, 8);
        }
        //�滮����ƽ��
        private void ghdlph()
        {
            if (dllxtitle!=null)
            {
                switch(dllxtitle)
                {
                    case "�ܶ˵���":
                        songddlph();
                        break;
                    case"�Ͷ˵���":
                        songddlph();
                        break;
                    case"��ˮ���ܵ���":
                        xsdqdlph();
                        break;
                }
            }
            else
            {
                MessageBox.Show("��ѡ���������");
                return;
            }
        }
        //�滮�ڵ���ƽ����ܶ˵�����
        private void souddlph()
        {
            if (dlphtitle==null)
            {
                MessageBox.Show("��ѡ���ܶ˵�����");
                return;
            }
            fpSpread1.Sheets[10].RowCount = 0;
            fpSpread1.Sheets[10].ColumnCount = 0;
            fpSpread1.Sheets[10].RowCount = 11;
            fpSpread1.Sheets[10].ColumnCount = 9;
            fpSpread1.Sheets[10].SetValue(0, 0, dlphtitle + "�滮�ڵ���ƽ����ܶ˵�����");
            fpSpread1.Sheets[10].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[10].Cells[0,0].HorizontalAlignment=FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].Cells[0,0].ColumnSpan=9;
            fpSpread1.Sheets[10].SetValue(1, 0,"��λ����ǧ��");
            
            fpSpread1.Sheets[10].Cells[1,0].HorizontalAlignment=FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[10].Cells[1,0].ColumnSpan=9;
            fpSpread1.Sheets[10].Rows[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].SetValue(2, 0, "���");
            fpSpread1.Sheets[10].SetValue(2, 1, "����");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[10], 1, "������Դ����ʱ������������ˮ�꣩  ");
            for (int i = 0; i < 6;i++ )
            {
                fpSpread1.Sheets[10].SetValue(2, 2 + i, (2010 + i).ToString()+"��");
                fpSpread1.Sheets[10].Columns[2 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
            fpSpread1.Sheets[10].SetValue(2, 8, "2020��");
            fpSpread1.Sheets[10].Columns[8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].SetValue(3, 0, "1");
            fpSpread1.Sheets[10].SetValue(3, 1, "������󸺺�");
            fpSpread1.Sheets[10].SetValue(4, 0, "2");
            fpSpread1.Sheets[10].SetValue(4, 1, "���ɱ��ã�5%��");
            fpSpread1.Sheets[10].SetValue(5, 0, "3");
           // fpSpread1.Sheets[10].Cells[5, 0].RowSpan = 3;
            fpSpread1.Sheets[10].SetValue(5, 1, "����װ����ģ");
            fpSpread1.Sheets[10].SetValue(6, 1, "���У�XX�糧");//Ϊ���糧 ������չ��
            fpSpread1.Sheets[10].Cells[6, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;


            //fpSpread1.Sheets[10].SetValue(7, 1, "XX�糧");//Ϊ���糧
            //fpSpread1.Sheets[10].Cells[7, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[10].SetValue(7, 0, "4");
            fpSpread1.Sheets[10].SetValue(7, 1, "������Դ����ʱ������������ˮ�꣩");
            fpSpread1.Sheets[10].SetValue(8, 0, "5");
            fpSpread1.Sheets[10].SetValue(8, 1, "ͣ��󵥻�ʱ������������ˮ�꣩");
            fpSpread1.Sheets[10].SetValue(9, 0, "6");
            fpSpread1.Sheets[10].SetValue(9, 1, "��������ʱ�߷����ӯ��");
            fpSpread1.Sheets[10].SetValue(10, 0, "7");
            fpSpread1.Sheets[10].SetValue(10, 1, "ͣ��󵥻�ʱ�߷����ӯ��");

            //�������
            //��Ӹ���Ԥ������
            string con = "ProjectID='" + ProjectUID + "'and Title like'%ȫ�����󸺺�%'and Flag2='2'";
            PSP_Types pt = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (pt!=null)
            {
                con = "ProjectID='" + ProjectUID + "'and Title like'%" + dlphtitle + "%'and Flag2='2'and ParentID='"+pt.ID+"'";
                PSP_Types pp = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                if (pp!=null)
                {
                    con = "Title Like'" +pp.Title + "%' AND Forecast!='0' AND ID like'%" + pp.ID + "%' order by Forecast";
                    IList list1 = Services.BaseService.GetList("SelectPs_Forecast_MathByWhere", con);
                    if (list1.Count>0)
                    {
                        for (int i = 0; i < 6;i++ )
                        {
                            string y="y"+(2010+i).ToString();
                            fpSpread1.Sheets[10].SetValue(3, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(((Ps_Forecast_Math)list1[0]),y));

                        }
                        fpSpread1.Sheets[10].SetValue(3, 8, Gethistroyvalue<Ps_Forecast_Math>(((Ps_Forecast_Math)list1[0]), "y2020"));
                      
                    }
                    else
                    {
                        MessageBox.Show("û�жԸõ������и���Ԥ�⣡");
                    }
                }
                else
                {
                    MessageBox.Show("�õ���û���ڷ���������չʵ������д��");
                }
            }
            else
            {
                MessageBox.Show("����������չʵ����û����дȫ�����󸺺ɣ�");
            }
            //���ҷ��糧
            con = "AreaName='" + dlphtitle + "'and AreaID='" + ProjectUID + "'";
            IList list2 = Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
            int num=list2.Count;
            if (num!=0)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    PSP_PowerSubstation_Info pps=(PSP_PowerSubstation_Info)list2[i];
                    if (i==0)
                    {
                         fpSpread1.Sheets[10].SetValue(6, 1, pps.Title);
                          fpSpread1.Sheets[10].Cells[6, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                          for (int j = 0; j< 7;j++ )
                          {
                              fpSpread1.Sheets[10].SetValue(6, 2 + i, pps.S2);
                          }
                    }
                    else
                    {
                        fpSpread1.Sheets[10].Rows.Add(6, 1);
                        fpSpread1.Sheets[10].SetValue(6, 1, pps.Title);
                        if (i==list2.Count)
                        {
                            fpSpread1.Sheets[10].SetValue(6, 1, "���У�"+pps.Title);
                        }
                        for (int j = 0; j < 7;j++ )
                        {
                            fpSpread1.Sheets[10].SetValue(6, 2 + i, pps.S2);
                        }
                        fpSpread1.Sheets[10].Cells[6, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    }
                   
                }
            }
            
            //���Ҹõ����ĵ糧�������
            if (num == 0)
            {
                fpSpread1.Sheets[10].Cells[5, 0].RowSpan = 2;
                Sheet_GridandColor(fpSpread1.Sheets[10], 11, 9);
            }
            else
            {
                fpSpread1.Sheets[10].Cells[5, 0].RowSpan = 1+num;
                Sheet_GridandColor(fpSpread1.Sheets[10], 10 + num, 9);
            }
        }
        //�滮�ڵ���ƽ����Ͷ˵�����
        private void songddlph()
        {
           if (dlphtitle==null)
            {
                MessageBox.Show("��ѡ���Ͷ˵�����");
                return;
            }
            fpSpread1.Sheets[10].RowCount = 0;
            fpSpread1.Sheets[10].ColumnCount = 0;
            fpSpread1.Sheets[10].RowCount = 10;
            fpSpread1.Sheets[10].ColumnCount = 9;
            fpSpread1.Sheets[10].SetValue(0, 0, dlphtitle + "�滮�ڵ���ƽ����Ͷ˵�����");
            fpSpread1.Sheets[10].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[10].Cells[0,0].HorizontalAlignment=FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].Cells[0,0].ColumnSpan=9;
            fpSpread1.Sheets[10].SetValue(1, 0,"��λ����ǧ��");
            
            fpSpread1.Sheets[10].Cells[1,0].HorizontalAlignment=FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[10].Cells[1,0].ColumnSpan=9;
            fpSpread1.Sheets[10].Rows[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].SetValue(2, 0, "���");
            fpSpread1.Sheets[10].SetValue(2, 1, "����");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[10], 1, "������Դ����ʱ������������ˮ�꣩  ");
            for (int i = 0; i < 6;i++ )
            {
                fpSpread1.Sheets[10].SetValue(2, 2 + i, (2010 + i).ToString()+"��");
                fpSpread1.Sheets[10].Columns[2 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
            fpSpread1.Sheets[10].SetValue(2, 8, "2020��");
            fpSpread1.Sheets[10].Columns[8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].SetValue(3, 0, "1");
            fpSpread1.Sheets[10].SetValue(3, 1, "������󸺺�");
            fpSpread1.Sheets[10].SetValue(4, 0, "2");
            fpSpread1.Sheets[10].SetValue(4, 1, "ƽ���߷帺��");
            fpSpread1.Sheets[10].SetValue(5, 0, "3");
           // fpSpread1.Sheets[10].Cells[4, 0].RowSpan = 1;
            fpSpread1.Sheets[10].SetValue(5, 1, "����װ����ģ");
            fpSpread1.Sheets[10].SetValue(6, 1, "���У�XX�糧");//Ϊ���糧 ������չ��
            fpSpread1.Sheets[10].Cells[6, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //fpSpread1.Sheets[10].SetValue(6, 1, "XX�糧");//Ϊ���糧
            //fpSpread1.Sheets[10].Cells[6, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[10].SetValue(7, 0, "4");
            fpSpread1.Sheets[10].SetValue(7, 1, "������Դ����ʱ��������");
            fpSpread1.Sheets[10].SetValue(8, 0, "5");
            fpSpread1.Sheets[10].SetValue(8, 1, "��������ʱ�߷����ӯ��");
            fpSpread1.Sheets[10].SetValue(9, 0, "6");
            fpSpread1.Sheets[10].SetValue(9, 1, "��������ʱƽ���߷����ӯ��");
          

            //�������
            //��Ӹ���Ԥ������
            string con = "ProjectID='" + ProjectUID + "'and Title like'%ȫ�����󸺺�%'and Flag2='2'";
            PSP_Types pt = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (pt!=null)
            {
                con = "ProjectID='" + ProjectUID + "'and Title like'%" + dlphtitle + "%'and Flag2='2'and ParentID='"+pt.ID+"'";
                PSP_Types pp = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                if (pp!=null)
                {
                    con = "Title Like'" +pp.Title + "%' AND Forecast!='0' AND ID like'%" + pp.ID + "%' order by Forecast";
                    IList list1 = Services.BaseService.GetList("SelectPs_Forecast_MathByWhere", con);
                    if (list1.Count>0)
                    {
                        for (int i = 0; i < 6;i++ )
                        {
                            string y="y"+(2010+i).ToString();
                            fpSpread1.Sheets[10].SetValue(3, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(((Ps_Forecast_Math)list1[0]),y));

                        }
                        fpSpread1.Sheets[10].SetValue(3, 8, Gethistroyvalue<Ps_Forecast_Math>(((Ps_Forecast_Math)list1[0]), "y2020"));
                      
                    }
                    else
                    {
                        MessageBox.Show("û�жԸõ������и���Ԥ�⣡");
                    }
                }
                else
                {
                    MessageBox.Show("�õ���û���ڷ���������չʵ������д��");
                }
            }
            else
            {
                MessageBox.Show("����������չʵ����û����дȫ�����󸺺ɣ�");
            }
            //���ҷ��糧
            con = "AreaName='" + dlphtitle + "'and AreaID='" + ProjectUID + "'";
            IList list2 = Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
            int num = list2.Count;
            if (num != 0)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    PSP_PowerSubstation_Info pps = (PSP_PowerSubstation_Info)list2[i];
                    if (i == 0)
                    {
                        fpSpread1.Sheets[10].SetValue(6, 1, pps.Title);
                        fpSpread1.Sheets[10].Cells[6, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        for (int j = 0; j < 7; j++)
                        {
                            fpSpread1.Sheets[10].SetValue(6, 2 + i, pps.S2);
                        }
                    }
                    else
                    {
                        fpSpread1.Sheets[10].Rows.Add(6, 1);
                        fpSpread1.Sheets[10].SetValue(6, 1, pps.Title);
                        if (i == list2.Count)
                        {
                            fpSpread1.Sheets[10].SetValue(6, 1, "���У�" + pps.Title);
                        }
                        for (int j = 0; j < 7; i++)
                        {
                            fpSpread1.Sheets[10].SetValue(6, 2 + i, pps.S2);
                        }
                        fpSpread1.Sheets[10].Cells[6, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    }

                }
            }
            //���Ҹõ����ĵ糧�������
            if (num==0)
            {
                fpSpread1.Sheets[10].Cells[5, 0].RowSpan = 2;
               Sheet_GridandColor(fpSpread1.Sheets[10],10,9);
            }
            else
            {
                fpSpread1.Sheets[10].Cells[5, 0].RowSpan = 1+num;
                Sheet_GridandColor(fpSpread1.Sheets[10], 9+num, 9);
            }
        }
        //����ƽ�����ˮ���ܵ�����
        private void xsdqdlph()
        {
             if (dlphtitle==null)
            {
                MessageBox.Show("��ˮ���ܵ�����");
                return;
            }
            fpSpread1.Sheets[10].RowCount = 0;
            fpSpread1.Sheets[10].ColumnCount = 0;
            fpSpread1.Sheets[10].RowCount = 12;
            fpSpread1.Sheets[10].ColumnCount = 9;
            fpSpread1.Sheets[10].SetValue(0, 0, dlphtitle + "�滮�ڵ���ƽ�����ˮ���ܵ�����");
            fpSpread1.Sheets[10].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[10].Cells[0,0].HorizontalAlignment=FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].Cells[0,0].ColumnSpan=9;
            fpSpread1.Sheets[10].SetValue(1, 0,"��λ����ǧ��");
            
            fpSpread1.Sheets[10].Cells[1,0].HorizontalAlignment=FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[10].Cells[1,0].ColumnSpan=9;
            fpSpread1.Sheets[10].Rows[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].SetValue(2, 0, "���");
            fpSpread1.Sheets[10].SetValue(2, 1, "����");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[10], 1, "������Դ����ʱ������������ˮ�꣩  ");
            for (int i = 0; i < 6;i++ )
            {
                fpSpread1.Sheets[10].SetValue(2, 2 + i, (2010 + i).ToString()+"��");
                fpSpread1.Sheets[10].Columns[2 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
            fpSpread1.Sheets[10].SetValue(2, 8, "2020��");
            fpSpread1.Sheets[10].Columns[8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].SetValue(3, 0, "1");
            fpSpread1.Sheets[10].SetValue(3, 1, "������󸺺�");
            fpSpread1.Sheets[10].SetValue(4, 0, "2");
            fpSpread1.Sheets[10].SetValue(4, 1, "���ɱ��ã�5%");
            fpSpread1.Sheets[10].SetValue(5, 0, "3");
            
             fpSpread1.Sheets[10].SetValue(5, 1, "�ļ��͹ȸ���");
            fpSpread1.Sheets[10].SetValue(6,0,"4");
            fpSpread1.Sheets[10].Cells[6, 0].RowSpan = 3;
            fpSpread1.Sheets[10].SetValue(6, 1, "����װ����ģ");
            fpSpread1.Sheets[10].SetValue(7, 1, "���У�XX�糧");//Ϊ���糧 ������չ��
            fpSpread1.Sheets[10].Cells[7, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            //fpSpread1.Sheets[10].SetValue(8, 1, "XX�糧");//Ϊ���糧
            //fpSpread1.Sheets[10].Cells[8, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[10].SetValue(8, 0, "5");
            fpSpread1.Sheets[10].SetValue(8, 1, "������Դ����ʱ���������������������");
            fpSpread1.Sheets[10].SetValue(9, 0, "6");
            fpSpread1.Sheets[10].SetValue(9, 1, "������ˮ���ܻ���͹ȳ�ˮ����");
            fpSpread1.Sheets[10].SetValue(10, 0, "7");
            fpSpread1.Sheets[10].SetValue(10, 1, "��������ʱ�߷����ӯ��");
             fpSpread1.Sheets[10].SetValue(11, 0, "8");
            fpSpread1.Sheets[10].SetValue(11, 1, "�ļ��͹�ʱ���������ˮ�� ����ӯ��");
          
            //�������
            //��Ӹ���Ԥ������
            string con = "ProjectID='" + ProjectUID + "'and Title like'%ȫ�����󸺺�%'and Flag2='2'";
            PSP_Types pt = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (pt!=null)
            {
                con = "ProjectID='" + ProjectUID + "'and Title like'%" + dlphtitle + "%'and Flag2='2'and ParentID='"+pt.ID+"'";
                PSP_Types pp = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                if (pp!=null)
                {
                    con = "Title Like'" +pp.Title + "%' AND Forecast!='0' AND ID like'%" + pp.ID + "%' order by Forecast";
                    IList list1 = Services.BaseService.GetList("SelectPs_Forecast_MathByWhere", con);
                    if (list1.Count>0)
                    {
                        for (int i = 0; i < 6;i++ )
                        {
                            string y="y"+(2010+i).ToString();
                            fpSpread1.Sheets[10].SetValue(3, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(((Ps_Forecast_Math)list1[0]),y));

                        }
                        fpSpread1.Sheets[10].SetValue(3, 8, Gethistroyvalue<Ps_Forecast_Math>(((Ps_Forecast_Math)list1[0]), "y2020"));
                      
                    }
                    else
                    {
                        MessageBox.Show("û�жԸõ������и���Ԥ�⣡");
                    }
                }
                else
                {
                    MessageBox.Show("�õ���û���ڷ���������չʵ������д��");
                }
            }
            else
            {
                MessageBox.Show("����������չʵ����û����дȫ�����󸺺ɣ�");
            }
            //���ҷ��糧
            con = "AreaName='" + dlphtitle + "'and AreaID='" + ProjectUID + "'";
            IList list2 = Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
            int num = list2.Count;
            if (num != 0)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    PSP_PowerSubstation_Info pps = (PSP_PowerSubstation_Info)list2[i];
                    if (i == 0)
                    {
                        fpSpread1.Sheets[10].SetValue(7, 1, pps.Title);
                        fpSpread1.Sheets[10].Cells[7, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                        for (int j = 0; j < 7; j++)
                        {
                            fpSpread1.Sheets[10].SetValue(7, 2 + i, pps.S2);
                        }
                    }
                    else
                    {
                        fpSpread1.Sheets[10].Rows.Add(7, 1);
                        fpSpread1.Sheets[10].SetValue(7, 1, pps.Title);
                        if (i == list2.Count)
                        {
                            fpSpread1.Sheets[10].SetValue(7, 1, "���У�" + pps.Title);
                        }
                        for (int j = 0; j < 7; i++)
                        {
                            fpSpread1.Sheets[10].SetValue(7, 2 + i, pps.S2);
                        }
                        fpSpread1.Sheets[10].Cells[7, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    }

                }
            }
            //���Ҹõ����ĵ糧�������
            if (num == 0)
            {
                fpSpread1.Sheets[10].Cells[6, 0].RowSpan = 2;
                Sheet_GridandColor(fpSpread1.Sheets[10], 12, 9);
            }
            else
            {
                fpSpread1.Sheets[10].Cells[6, 0].RowSpan = 1+num;
                Sheet_GridandColor(fpSpread1.Sheets[10], 11+num, 9);
            }
             
        }

       //500KV�������ƽ���
        private void dl500ph()
        {
            if (dlp500title==null)
            {
                MessageBox.Show("500kV�������ƽ�����ѡ�����");
                return;

            }
            fpSpread1.Sheets[11].RowCount = 0;
            fpSpread1.Sheets[11].ColumnCount = 0;
            fpSpread1.Sheets[11].RowCount = 14;
            fpSpread1.Sheets[11].ColumnCount = 9;
            fpSpread1.Sheets[11].SetValue(0, 0, dlp500title + "��ʮ���塱�����滮500kV�������ƽ���");
            fpSpread1.Sheets[11].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[11].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[11].Cells[0, 0].ColumnSpan = 9;
            fpSpread1.Sheets[11].SetValue(1, 0, "��λ����ǧ�ߡ���ǧ����");

            fpSpread1.Sheets[11].Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[11].Cells[1, 0].ColumnSpan = 9;
            fpSpread1.Sheets[11].Rows[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[11].SetValue(2, 0, "���");
            fpSpread1.Sheets[11].SetValue(2, 1, "��Ŀ");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[11], 1, "�������220ǧ�������µ���װ������");
            for (int i = 0; i < 6;i++ )
            {
                fpSpread1.Sheets[11].SetValue(2, 2 + i, (2010 + i).ToString() + "��");
                fpSpread1.Sheets[11].Columns[2 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
            fpSpread1.Sheets[11].SetValue(2, 8, "2020��");
            fpSpread1.Sheets[11].Columns[8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[11].Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[11].SetValue(3, 0, "1");
            fpSpread1.Sheets[11].SetValue(3, 1, "������󸺺�");

            fpSpread1.Sheets[11].SetValue(4, 0, "2");
            fpSpread1.Sheets[11].SetValue(4, 1, "�������220ǧ�������µ���װ������");

            fpSpread1.Sheets[11].SetValue(5, 0, "3");
            fpSpread1.Sheets[11].SetValue(5, 1, "220kV�����������⽻������");

            fpSpread1.Sheets[11].SetValue(6, 0, "4");
            fpSpread1.Sheets[11].SetValue(6, 1, "��Դ�����������");
            fpSpread1.Sheets[11].SetValue(7, 0, "5");
            fpSpread1.Sheets[11].SetValue(7, 1, "�߷����ӯ��");
            fpSpread1.Sheets[11].SetValue(8, 0, "6");
            fpSpread1.Sheets[11].SetValue(8, 1, "ͣһ̨��󵥻��󹩵����");
            fpSpread1.Sheets[11].SetValue(9, 0, "7");
            fpSpread1.Sheets[11].SetValue(9, 1, "���500kV�����ܽ�����");
            fpSpread1.Sheets[11].SetValue(10, 0, "8");
            fpSpread1.Sheets[11].SetValue(10, 1, "��500kV�������");
            fpSpread1.Sheets[11].SetValue(11, 0, "9");
            fpSpread1.Sheets[11].SetValue(11, 1, "����500kV�������");
            fpSpread1.Sheets[11].SetValue(12, 0, "10");
           // fpSpread1.Sheets[11].Cells[12, 0].RowSpan = 1;
            fpSpread1.Sheets[11].SetValue(12, 1, "���У�xx��");
            fpSpread1.Sheets[11].Cells[12, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[11].SetValue(13, 0, "11");
            fpSpread1.Sheets[11].SetValue(13, 1, "500kV������ر�");

            //���������
            //��Ӹ���Ԥ������
            string con = "ProjectID='" + ProjectUID + "'and Title like'%ȫ�����󸺺�%'and Flag2='2'";
            PSP_Types pt = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (pt != null)
            {
                con = "ProjectID='" + ProjectUID + "'and Title like'%" + dlp500title+ "%'and Flag2='2'and ParentID='" + pt.ID + "'";
                PSP_Types pp = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                if (pp != null)
                {
                    con = "Title Like'" + pp.Title + "%' AND Forecast!='0' AND ID like'%" + pp.ID + "%' order by Forecast";
                    IList list1 = Services.BaseService.GetList("SelectPs_Forecast_MathByWhere", con);
                    if (list1.Count > 0)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            string y = "y" + (2010 + i).ToString();
                            fpSpread1.Sheets[11].SetValue(3, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(((Ps_Forecast_Math)list1[0]), y));

                        }
                        fpSpread1.Sheets[11].SetValue(3, 8, Gethistroyvalue<Ps_Forecast_Math>(((Ps_Forecast_Math)list1[0]), "y2020"));

                    }
                    else
                    {
                        MessageBox.Show("û�жԸõ������и���Ԥ�⣡");
                    }
                }
                else
                {
                    MessageBox.Show("�õ���û���ڷ���������չʵ������д��");
                }
            }
            else
            {
                MessageBox.Show("����������չʵ����û����дȫ�����󸺺ɣ�");
            }
            //��ȡ����ƽ�������е�����
            con = "ProjectID='" + ProjectUID + "'and ParentID='0'and Title='"+dlp500title+"'";
            Ps_Table_500Result PR =(Ps_Table_500Result) Services.BaseService.GetObject("SelectPs_Table_500ResultByConn", con);
            if (PR!=null)
            {
                con="ProjectID='"+ProjectUID+"'and ParentID='"+PR.ID+"'and Title='����220ǧ�������µط���Դ����'";
                Ps_Table_500Result ps =(Ps_Table_500Result) Services.BaseService.GetObject("SelectPs_Table_500ResultByConn", con);
                for (int i = 0; i < 6;i++ )
                {
                    string y="yf"+(2010+i).ToString();
                    fpSpread1.Sheets[11].SetValue(4, 2 + i, Gethistroyvalue<Ps_Table_500Result>(ps, y));
                }
                fpSpread1.Sheets[11].SetValue(4,8, Gethistroyvalue<Ps_Table_500Result>(ps, "yf2020"));
                 con = "ProjectID='" + ProjectUID + "'and ParentID='" + PR.ID + "'and Title='�ġ�220ǧ�������������������'";
                 ps = (Ps_Table_500Result)Services.BaseService.GetObject("SelectPs_Table_500ResultByConn", con);
                for(int i=0;i<6;i++)
                {
                     string y="yf"+(2010+i).ToString();
                    fpSpread1.Sheets[11].SetValue(5, 2 + i, Gethistroyvalue<Ps_Table_500Result>(ps, y));
                }
                fpSpread1.Sheets[11].SetValue(5, 8, Gethistroyvalue<Ps_Table_500Result>(ps, "yf2020"));
            }
            else
            {
                MessageBox.Show("����ƽ��������û����Ӵ˵�����");
            }
          //��ȡ����ƽ���е�����
            con = "ProjectID='" + ProjectUID + "'and ParentID='0'and Title='" + dlp500title + "'";
            Ps_Table_500PH ph = (Ps_Table_500PH)Services.BaseService.GetObject("SelectPs_Table_500PHListByWhere", con);
            int subnum = 0;
            if (ph!=null)
            {
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + ph.ID + "'and Title='500ǧ�����ñ��վ���縺��'and Col1='0'";
                Ps_Table_500PH ps = (Ps_Table_500PH)Services.BaseService.GetObject("SelectPs_Table_500PHListByWhere", con);
                for (int i = 0; i < 6;i++ )
                {
                    string y = "y" + (2010 + i).ToString();
                    fpSpread1.Sheets[11].SetValue(9, 2 + i, Gethistroyvalue<Ps_Table_500PH>(ps, y));
                }
                fpSpread1.Sheets[11].SetValue(9, 8, Gethistroyvalue<Ps_Table_500PH>(ps, "y2020"));
                double fzb = 0.0;
                if (string.IsNullOrEmpty(ph.BuildYear))
                {
                    fzb = 0.0;
                }
                else
                    fzb = Convert.ToDouble(ph.BuildYear);
                for (int i = 0; i < 6; i++)
                {
                    string y = "y" + (2010 + i).ToString();
                    fpSpread1.Sheets[11].SetValue(10, 2 + i, Gethistroyvalue<Ps_Table_500PH>(ps, y)*fzb);
                }
                fpSpread1.Sheets[11].SetValue(10, 8, Gethistroyvalue<Ps_Table_500PH>(ps, "y2020") * fzb);
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + ph.ID + "'and Col1='1'";
                IList list1 = Services.BaseService.GetList("SelectPs_Table_500PHListByWhere", con);
                if (list1.Count>0)
                {
                    subnum = list1.Count;
                    for (int i = 0; i < list1.Count;i++ )
                    {
                        Ps_Table_500PH pth=(Ps_Table_500PH)list1[i];
                        if (i==0)
                        {
                            fpSpread1.Sheets[11].SetValue(12, 1, pth.Title);
                            for (int j = 0; j < 6;j++ )
                            {
                                string y="y"+(2010+i).ToString();
                                fpSpread1.Sheets[11].SetValue(12, 2 + i, Gethistroyvalue<Ps_Table_500PH>(pth, y));
                            }
                            fpSpread1.Sheets[11].SetValue(12, 8, Gethistroyvalue<Ps_Table_500PH>(pth, "y2020"));
                        }
                        else
                        {
                            fpSpread1.Sheets[11].Rows.Add(12, 1);
                            if (i<list1.Count-1)
                            {
                                fpSpread1.Sheets[11].SetValue(12, 1, pth.Title);
                                
                            }
                            else
                            {
                                fpSpread1.Sheets[11].SetValue(12, 1,"���У�"+ pth.Title);
                            }
                            for (int j = 0; j < 6; j++)
                            {
                                string y = "y" + (2010 + i).ToString();
                                fpSpread1.Sheets[11].SetValue(12, 2 + i, Gethistroyvalue<Ps_Table_500PH>(pth, y));
                            }
                            fpSpread1.Sheets[11].SetValue(12, 8, Gethistroyvalue<Ps_Table_500PH>(pth, "y2020"));

                        }
                    }
                    //ͳ������
                    for (int i=0;i<7;i++)
                    {
                        fpSpread1.Sheets[11].Cells[11, 2 + i].Formula = "SUM(R13C" + (3 + i).ToString() + "R" + (13 + list1.Count - 1).ToString() + "C" + (3 + i).ToString() + ")";
                        fpSpread1.Sheets[11].Cells[12 + list1.Count, 2 + i].Formula = "R9C"+(3+i).ToString()+"/SUM(R13C" + (3 + i).ToString() + "R" + (13 + list1.Count - 1).ToString() + "C" + (3 + i).ToString() + ")";
                    }
                    fpSpread1.Sheets[11].Cells[12, 0].RowSpan = list1.Count;
                }
            }
            if (subnum==0)
            {
                Sheet_GridandColor(fpSpread1.Sheets[11], 14, 9);
            }
            else
                Sheet_GridandColor(fpSpread1.Sheets[11], 13+subnum, 9);
          
        }
        //�����滮220kV�������ƽ���
        private void dl220ph()
        {
            if (dlp220title == null)
            {
                MessageBox.Show("220kV�������ƽ�����ѡ�����");
                return;

            }
            fpSpread1.Sheets[12].RowCount = 0;
            fpSpread1.Sheets[12].ColumnCount = 0;
            fpSpread1.Sheets[12].RowCount = 19;
            fpSpread1.Sheets[12].ColumnCount = 9;
            fpSpread1.Sheets[12].SetValue(0, 0, dlp220title + "��ʮ���塱�����滮220kV�������ƽ���");
            fpSpread1.Sheets[12].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[12].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[12].Cells[0, 0].ColumnSpan = 9;
            fpSpread1.Sheets[12].SetValue(1, 0, "��λ����ǧ�ߡ���ǧ����");

            fpSpread1.Sheets[12].Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[12].Cells[1, 0].ColumnSpan = 9;
            fpSpread1.Sheets[12].Rows[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[12].SetValue(2, 0, "���");
            fpSpread1.Sheets[12].SetValue(2, 1, "��Ŀ");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[12], 1, "��1��110kV�����µ�Դֱ�ӹ��縺��");
            for (int i = 0; i < 6; i++)
            {
                fpSpread1.Sheets[12].SetValue(2, 2 + i, (2010 + i).ToString() + "��");
                fpSpread1.Sheets[12].Columns[2 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
            fpSpread1.Sheets[12].SetValue(2, 8, "2020��");
            fpSpread1.Sheets[12].Columns[8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[12].Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            fpSpread1.Sheets[12].SetValue(3, 0, "1");
            fpSpread1.Sheets[12].SetValue(3, 1, "�ۺ���󸺺�");
            fpSpread1.Sheets[12].SetValue(4, 0, "2");
            fpSpread1.Sheets[12].Cells[4, 0].RowSpan = 3;
            fpSpread1.Sheets[12].SetValue(4, 1, "ֱ�ӹ��縺��");
            fpSpread1.Sheets[12].SetValue(5, 1, "��1��110kV�����µ�Դֱ�ӹ��縺��");
            fpSpread1.Sheets[12].SetValue(6, 1, "��2����������");
            fpSpread1.Sheets[12].SetValue(7, 0, "3");
            fpSpread1.Sheets[12].SetValue(7, 1, "��220kV��ѹ���縺��");
            fpSpread1.Sheets[12].SetValue(8, 0, "4");
            fpSpread1.Sheets[12].SetValue(8, 1, "����220kV��ѹ�������");
            fpSpread1.Sheets[12].SetValue(9, 0, "5");
            fpSpread1.Sheets[12].SetValue(9, 1, "220kV���ر�");
            fpSpread1.Sheets[12].SetValue(10, 0, "6");
            fpSpread1.Sheets[12].SetValue(10, 1, "��220kV�������");
            fpSpread1.Sheets[12].SetValue(11, 0, "7");
            fpSpread1.Sheets[12].SetValue(11, 1, "�������ӯ��");

            fpSpread1.Sheets[12].SetValue(12, 0, "8");
            fpSpread1.Sheets[12].Cells[12, 0].RowSpan = 2;
            fpSpread1.Sheets[12].SetValue(12, 1, "Ŀǰ������ı������");
            fpSpread1.Sheets[12].SetValue(13, 1, "ע");

            fpSpread1.Sheets[12].SetValue(14, 0, "9");
            fpSpread1.Sheets[12].Cells[14, 0].RowSpan = 2;
            fpSpread1.Sheets[12].SetValue(14, 1, "�滮�����������");
            fpSpread1.Sheets[12].SetValue(15, 1, "ע");

            fpSpread1.Sheets[12].SetValue(16, 0, "10");
            fpSpread1.Sheets[12].SetValue(16, 1, "��������ϼ�");
            fpSpread1.Sheets[12].SetValue(17, 0, "11");
            fpSpread1.Sheets[12].SetValue(17, 1, "���ر�");
            fpSpread1.Sheets[12].SetValue(18, 0, "12");
            fpSpread1.Sheets[12].SetValue(18, 1, "��    ע");
            Sheet_GridandColor(fpSpread1.Sheets[12],19,9);
            //�������
            string con = "ProjectID='"+ProjectUID+"'and ParentID='0'and Title='"+dlp220title+"'";
            Ps_Table_220Result pr = (Ps_Table_220Result)Services.BaseService.GetObject("SelectPs_Table_220ResultByConn", con);
            if (pr!=null)
            {
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + pr.ID + "'and Title='�ߡ�220ǧ�����縺��'and Col1 = 'no'";
                Ps_Table_220Result ps=(Ps_Table_220Result)Services.BaseService.GetObject("SelectPs_Table_220ResultByConn", con);
                for (int i = 0; i < 6;i++ )
                {
                    string y="yf"+(2010+i).ToString();
                    fpSpread1.Sheets[12].SetValue(3, 2 + i, Gethistroyvalue<Ps_Table_220Result>(ps, y));
                }
                fpSpread1.Sheets[12].SetValue(3, 8, Gethistroyvalue<Ps_Table_220Result>(ps, "yf2020"));
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + pr.ID + "'and Title='һ��110ǧ�����¸���'";
                Ps_Table_220Result pyx = (Ps_Table_220Result)Services.BaseService.GetObject("SelectPs_Table_220ResultByConn", con);
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + pr.ID + "'and Title='����110ǧ��ֱ������'";
                Ps_Table_220Result pz = (Ps_Table_220Result)Services.BaseService.GetObject("SelectPs_Table_220ResultByConn", con);
                for (int i = 0; i < 6; i++)
                {
                    string y = "yf" + (2010 + i).ToString();
                    fpSpread1.Sheets[12].SetValue(5, 2 + i, Gethistroyvalue<Ps_Table_220Result>(pyx, y) + Gethistroyvalue<Ps_Table_220Result>(pz, y));
                }
                fpSpread1.Sheets[12].SetValue(5, 8, Gethistroyvalue<Ps_Table_220Result>(pyx, "yf2020") + Gethistroyvalue<Ps_Table_220Result>(pz, "yf2020"));
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + pr.ID + "'and Title='�塢����110ǧ���������������'";
               pz = (Ps_Table_220Result)Services.BaseService.GetObject("SelectPs_Table_220ResultByConn", con);
               for (int i = 0; i < 6; i++)
               {
                   string y = "yf" + (2010 + i).ToString();
                   fpSpread1.Sheets[12].SetValue(6, 2 + i,Gethistroyvalue<Ps_Table_220Result>(pz, y));
               }
               fpSpread1.Sheets[12].SetValue(6, 8, Gethistroyvalue<Ps_Table_220Result>(pz, "yf2020"));
               for (int i = 0; i < 7;i++ )
               {
                   fpSpread1.Sheets[12].Cells[4, 2 + i].Formula = "SUM(R6C" + (3 + i).ToString() + ":R7C" + (3 + i).ToString() + ")";
               }
               con = "ProjectID='" + ProjectUID + "'and ParentID='" + pr.ID + "'and Title='����220ǧ�����վ��ѹ�๩�縺��'";
               pz = (Ps_Table_220Result)Services.BaseService.GetObject("SelectPs_Table_220ResultByConn", con);
               for (int i = 0; i < 6; i++)
               {
                   string y = "yf" + (2010 + i).ToString();
                   fpSpread1.Sheets[12].SetValue(7, 2 + i, Gethistroyvalue<Ps_Table_220Result>(pz, y));
               }
               fpSpread1.Sheets[12].SetValue(6, 8, Gethistroyvalue<Ps_Table_220Result>(pz, "yf2020"));
               con = "AreaID='" + ProjectUID + "'and AreaName='"+dlp220title+"'and flag='1'and L1='220'";
               double volume =Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con)) ;
                for(int i=0;i<7;i++)
                {
                    fpSpread1.Sheets[12].SetValue(8, 2 + i,  volume);
                    fpSpread1.Sheets[12].Cells[9, 2 + i].Formula = "R9C" + (3 + i).ToString() + "/R8C" + (3+ i).ToString();
                }
                con = "ProjectID='" + ProjectUID + "'and ParentID='0'and Title='" + dlp220title + "'";
                Ps_Table_200PH ph = (Ps_Table_200PH)Services.BaseService.GetObject("SelectPs_Table_200PHListByConn", con);
                double fzb = 0.0;
                if (string.IsNullOrEmpty(ph.BuildYear))
                {
                    fzb = 0.0;
                }
                else
                    fzb = Convert.ToDouble(ph.BuildYear);
               
                if (ph!=null)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        string y = "yf"+ (2010 + i).ToString();
                        fpSpread1.Sheets[12].SetValue(10, 2 + i, Gethistroyvalue<Ps_Table_220Result>(ps, y)*fzb );
                    }
                    fpSpread1.Sheets[12].SetValue(10, 8, Gethistroyvalue<Ps_Table_220Result>(ps, "yf2020") * fzb);
                }
                //
                for (int i = 0; i < 6;i++ )
                {
                    string y = (2010 + i).ToString();
                    con = "and a.BuildEd='" + y + "'and a.ProjectID='" + ProjectUID + "'and a.AreaName='" + dlp220title + "'and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='220'";
                    double vol = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubLL", con));
                    fpSpread1.Sheets[12].SetValue(12, 2 + i, vol);
                }
                con = "and a.BuildEd='2020'and a.ProjectID='" + ProjectUID + "'and a.AreaName='" + dlp220title + "'and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='220'";
                double num=Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubLL", con));
                fpSpread1.Sheets[12].SetValue(12,8,num);
                for (int i=0;i<7;i++)
                {
                    fpSpread1.Sheets[12].Cells[14,2+i].Formula="R11C"+(3+i).ToString()+"-R9C"+(3+i).ToString();
                     fpSpread1.Sheets[12].Cells[16,2+i].Formula="R13C"+(3+i).ToString()+"+R9C"+(3+i).ToString();
                     fpSpread1.Sheets[12].Cells[17,2+i].Formula="R17C"+(3+i).ToString()+"/R4C"+(3+i).ToString();
                }

            }
            else
            {
                MessageBox.Show("�����ڵ���ƽ����������Ӹõ�����");
                return;
            }


        }
        //ʮ��������滮110kV�������ƽ���
        private void dl110ph()
        {
            if (dlp110tiltle==null)
            {
                MessageBox.Show("110kV�������ƽ�����ѡ�����");
                return;
            }
            fpSpread1.Sheets[13].RowCount = 0;
            fpSpread1.Sheets[13].ColumnCount = 0;
            fpSpread1.Sheets[13].RowCount = 19;
            fpSpread1.Sheets[13].ColumnCount = 8;
            fpSpread1.Sheets[13].SetValue(0, 0, dlp110tiltle + "��ʮ���塱�����滮220kV�������ƽ���");
            fpSpread1.Sheets[13].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[13].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[13].Cells[0, 0].ColumnSpan = 8;
            fpSpread1.Sheets[13].SetValue(1, 0, "��λ����ǧ�ߡ���ǧ����");

            fpSpread1.Sheets[13].Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[13].Cells[1, 0].ColumnSpan = 8;
            fpSpread1.Sheets[13].Rows[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[13].SetValue(2, 0, "���");
            fpSpread1.Sheets[13].SetValue(2, 1, "��Ŀ");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[13], 1, "��1��110kV�����µ�Դֱ�ӹ��縺��");
            for (int i = 0; i < 6; i++)
            {
                fpSpread1.Sheets[13].SetValue(2, 2 + i, (2010 + i).ToString() + "��");
                fpSpread1.Sheets[13].Columns[2 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }
          
            fpSpread1.Sheets[13].Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            fpSpread1.Sheets[13].SetValue(3, 0, "1");
            fpSpread1.Sheets[13].SetValue(3, 1, "�����ۺ���߸���");
            fpSpread1.Sheets[13].SetValue(4, 0, "2");
            fpSpread1.Sheets[13].SetValue(4, 1, "����220kV����35kV��ɹ�����");
            fpSpread1.Sheets[13].SetValue(5, 0, "3");
            fpSpread1.Sheets[13].SetValue(5, 1, "����35kV������С��Դֱ�ӹ��縺��");
            fpSpread1.Sheets[13].SetValue(6, 0, "4");
            fpSpread1.Sheets[13].SetValue(6, 1, "110kV�û�ר�为��");
            fpSpread1.Sheets[13].SetValue(7, 0, "5");
            fpSpread1.Sheets[13].SetValue(7, 1, "��110kV��ѹ���縺��");
            fpSpread1.Sheets[13].SetValue(8, 0, "6");
            fpSpread1.Sheets[13].SetValue(8, 1, "��������110kV��ѹ�������");
            fpSpread1.Sheets[13].SetValue(9, 0, "7");
            fpSpread1.Sheets[13].SetValue(9, 1, "����110kV���ر�");
            fpSpread1.Sheets[13].SetValue(10, 0, "8");
            fpSpread1.Sheets[13].SetValue(10, 1, "������110kV�������");
            fpSpread1.Sheets[13].SetValue(11, 0, "9");
            fpSpread1.Sheets[13].SetValue(11, 1, "�����������ӯ��");
            fpSpread1.Sheets[13].SetValue(12, 0, "10");
            fpSpread1.Sheets[13].Cells[12, 0].RowSpan = 2;
            fpSpread1.Sheets[13].SetValue(12, 1, "Ŀǰ������ı������");
            fpSpread1.Sheets[13].SetValue(13, 1, "ע��");
            fpSpread1.Sheets[13].SetValue(14, 0, "11");
            fpSpread1.Sheets[13].Cells[14, 0].RowSpan = 2;
            fpSpread1.Sheets[13].SetValue(14, 1, "�滮�����������");
            fpSpread1.Sheets[13].SetValue(15, 1, "ע��");
            fpSpread1.Sheets[13].SetValue(16, 0, "12");
            fpSpread1.Sheets[13].SetValue(16, 1, "��������ϼ�");
            fpSpread1.Sheets[13].SetValue(17, 0, "13");
            fpSpread1.Sheets[13].SetValue(17, 1, "���ر�");
            fpSpread1.Sheets[13].SetValue(18, 0, "14");
            fpSpread1.Sheets[13].SetValue(18, 1, "��    ע");
            Sheet_GridandColor(fpSpread1.Sheets[13], 19, 8);
            //�������
            string con = "ProjectID='" + ProjectUID + "'and ParentID='0'and Title='" + dlp110tiltle + "'";
            Ps_Table_110Result pr = (Ps_Table_110Result)Services.BaseService.GetObject("SelectPs_Table_110ResultByConn", con);
            if (pr!=null)
            {
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + pr.ID + "'and Title='����110ǧ�����縺��'and Col1 = 'no'";
                Ps_Table_110Result ps = (Ps_Table_110Result)Services.BaseService.GetObject("SelectPs_Table_110ResultByConn", con);
                for (int i = 0; i < 6; i++)
                {
                    string y = "yf" + (2010 + i).ToString();
                    fpSpread1.Sheets[13].SetValue(3, 2 + i, Gethistroyvalue<Ps_Table_110Result>(ps, y));
                }
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + pr.ID + "'and Title='����35ǧ�������������������'";
                Ps_Table_110Result pz = (Ps_Table_110Result)Services.BaseService.GetObject("SelectPs_Table_110ResultByConn", con);
                for (int i = 0; i < 6; i++)
                {
                    string y = "yf" + (2010 + i).ToString();
                    fpSpread1.Sheets[13].SetValue(4, 2 + i,  Gethistroyvalue<Ps_Table_110Result>(pz, y));
                }
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + pr.ID + "'and Title='����35ǧ�������µط���Դ����'";
               pz = (Ps_Table_110Result)Services.BaseService.GetObject("SelectPs_Table_110ResultByConn", con);
                for (int i = 0; i < 6; i++)
                {
                    string y = "yf" + (2010 + i).ToString();
                    fpSpread1.Sheets[13].SetValue(5, 2 + i, Gethistroyvalue<Ps_Table_110Result>(pz, y));
                }
                //�ڴ����110kv�û�ת�为��
                //ͳ��
                for (int i = 0; i < 6;i++ )
                {
                    fpSpread1.Sheets[13].Cells[7, 2 + i].Formula = "R4C" + (3 + i).ToString() + "-SUM(R5C" + (3 + i).ToString() + ":R8C" + (3 + i).ToString() + ")";
                }
                //ͳ�Ʊ��վ
                con = "AreaID='" + ProjectUID + "'and AreaName='" + dlp110tiltle + "'and flag='1'and L1='110'";
                double volume = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
                for (int i = 0; i < 6; i++)
                {
                    fpSpread1.Sheets[13].SetValue(8, 2 + i, volume);
                    fpSpread1.Sheets[13].Cells[9, 2 + i].Formula = "R9C" + (3 + i).ToString() + "/R8C" + (3 + i).ToString();
                }
                con = "ProjectID='" + ProjectUID + "'and ParentID='0'and Title='" + dlp220title + "'";
                Ps_Table_100PH ph = (Ps_Table_100PH)Services.BaseService.GetObject("SelectPs_Table_100PHListByConn", con);
                if (ph != null)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        string y = "yf" + (2010 + i).ToString();
                        fpSpread1.Sheets[13].SetValue(10, 2 + i, Gethistroyvalue<Ps_Table_110Result>(ps, y) * (Convert.ToDouble(ph.BuildYear)));
                    }
                  
                }
                for (int i = 0; i < 6; i++)
                {
                   
                    fpSpread1.Sheets[13].Cells[11, 2 + i].Formula = "R9C" + (3 + i).ToString() + "-R11C" + (3 + i).ToString();
                }
                //��Ŀ��ϸ���л�ù滮������
                for (int i = 0; i < 6; i++)
                {
                    string y = (2010 + i).ToString();
                    con = "and a.BuildEd='" + y + "'and a.ProjectID='" + ProjectUID + "'and a.AreaName='" + dlp110tiltle + "'and substring(a.BianInfo,1,charindex('@',a.BianInfo,0)-1)='110'";
                    double vol = Convert.ToDouble(Services.BaseService.GetObject("SelectTZGSsubLL", con));
                    fpSpread1.Sheets[13].SetValue(12, 2 + i, vol);
                }
                for (int i = 0; i < 6; i++)
                {

                    fpSpread1.Sheets[13].Cells[14, 2 + i].Formula = "R11C" + (3 + i).ToString() + "-R9C" + (3 + i).ToString();
                    fpSpread1.Sheets[13].Cells[16, 2 + i].Formula = "R13C" + (3 + i).ToString() + "+R9C" + (3 + i).ToString();
                    fpSpread1.Sheets[13].Cells[17, 2 + i].Formula = "R17C" + (3 + i).ToString() + "/R4C" + (3 + i).ToString();
                }
                
            }
        }
        //�����޹�ƽ���
        private void dlwgph()
        {
            if (dlpwgtitle == null)
            {
                MessageBox.Show("�޹�ƽ�����ѡ�����");
                return;
            }
            fpSpread1.Sheets[14].RowCount = 0;
            fpSpread1.Sheets[14].ColumnCount = 0;
            fpSpread1.Sheets[14].RowCount = 16;
            fpSpread1.Sheets[14].ColumnCount = 8;
            fpSpread1.Sheets[14].SetValue(0, 0, dlp110tiltle + "�����滮�޹�ƽ���");
            fpSpread1.Sheets[14].Cells[0, 0].Font = new Font("����", 9, FontStyle.Bold);
            fpSpread1.Sheets[14].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[14].Cells[0, 0].ColumnSpan = 8;
            fpSpread1.Sheets[14].SetValue(1, 0, "��λ����ǧ�ߡ���ǧ����");

            fpSpread1.Sheets[14].Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[14].Cells[1, 0].ColumnSpan = 8;
            fpSpread1.Sheets[14].Rows[2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[14].SetValue(2, 0, "���");
            fpSpread1.Sheets[14].SetValue(2, 1, "��Ŀ");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[14], 1, "���������Ȼ�޹�����(Ql=1.3*Pg)");
            for (int i = 0; i < 6; i++)
            {
                fpSpread1.Sheets[14].SetValue(2, 2 + i, (2010 + i).ToString() + "��");
                fpSpread1.Sheets[14].Columns[2 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }

            fpSpread1.Sheets[14].Columns[0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[14].SetValue(3, 0, "1");
            fpSpread1.Sheets[14].SetValue(3, 1, "��������й�����(Pg)");
            fpSpread1.Sheets[14].SetValue(4, 0, "2");
            fpSpread1.Sheets[14].SetValue(4, 1, "���������Ȼ�޹�����(Ql=1.3*Pg)");
            fpSpread1.Sheets[14].SetValue(5, 0, "3");
            fpSpread1.Sheets[14].SetValue(5, 1, "�����޹���Դ�谲װ����(1.15Ql)");
            fpSpread1.Sheets[14].SetValue(6, 0, "4");
            
            fpSpread1.Sheets[14].SetValue(6, 1, "�����޹���Դ�ϼ�");
            fpSpread1.Sheets[14].SetValue(7, 0, "4.1");
            fpSpread1.Sheets[14].SetValue(7, 1, "���������豸�޹�����");
            fpSpread1.Sheets[14].Cells[7, 0].RowSpan = 2;
            fpSpread1.Sheets[14].SetValue(8, 1, " ���У������糧");
            fpSpread1.Sheets[14].Cells[8, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            fpSpread1.Sheets[14].SetValue(9, 0, "4.2");
            fpSpread1.Sheets[14].SetValue(9, 1, "���������޹���������");
            fpSpread1.Sheets[14].Cells[9, 0].RowSpan = 2;
            fpSpread1.Sheets[14].SetValue(10, 1, "���У� ����220kV���վ");
            fpSpread1.Sheets[14].Cells[10, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[14].SetValue(11, 0, "4.3");
            fpSpread1.Sheets[14].SetValue(11, 1, "220kV��·��繦��");
            fpSpread1.Sheets[14].SetValue(12, 0, "4.4");
            fpSpread1.Sheets[14].SetValue(12, 1, "110kV��·��繦��");
            fpSpread1.Sheets[14].SetValue(13, 0, "4.5");
            fpSpread1.Sheets[14].SetValue(13, 1, "�������������޹�����");
            fpSpread1.Sheets[14].SetValue(14, 0, "5");
            fpSpread1.Sheets[14].SetValue(14, 1, "�޹�ƽ��[��4��-��3��]");
            fpSpread1.Sheets[14].SetValue(15, 0, "6");
            fpSpread1.Sheets[14].SetValue(15, 1, "�޹�ƽ�⣨��ˮ��");
           
            Sheet_GridandColor(fpSpread1.Sheets[14], 16, 8);

        }
        //��ȡ�ͱ��������е�����
        private void Sheet_SaveData(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //ÿ������д�ı�񣨲��̶�����������������һ���ڸ���ǰд����д���ݣ�һ���ڸ��º�д����д����
            //���2-2���˹�����������������Ŀ�����γɹ��� title�д���Ŀ���ƣ�str1�������������

            //�������ʱ���������б�
            sd.Clear();
            int row = tempsheet.RowCount;
            //ѭ����ȡ����е��������ݣ���д��sd��
            for (int i = 4; i < row; i++)
            {
                sdata stest = new sdata();
                stest.title = tempsheet.Cells[i, 0].Value.ToString();
                if (tempsheet.Cells[i, 5].Value != null)
                {
                    stest.str1 = tempsheet.Cells[i, 5].Value.ToString();
                }
                sd.Add(stest);
            }
        }

        private void Sheet_WirteData(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //ÿ������д�ı�񣨲��̶�����������������һ���ڸ���ǰд����д���ݣ�һ���ڸ��º�д����д����
            //���ڸ������ݺ�д����д����
            int row = tempsheet.RowCount;
            for (int i = 4; i < row; i++)
            {
                //for (int j = 0; j < sd.Count; j++)
                //{
                    //ͨ����Ŀ���Ƶıȶ�����д����
                    if (tempsheet.Cells[i, 0].Value.ToString() == sd[i-4].title)
                    {
                        tempsheet.SetValue(i, 5, sd[i-4].str1);
                        //�ȶ������ɾ����һ��������´αȶ����Ч��
                        //sd.Remove(sd[j]);
                       // break;
                    }
                //}

            }
        }

        //��ȡ�ͱ���220����ƽ����ע�ǵ�����
        List<sdata> phsd1 = new List<sdata>();
        List<sdata> phsd2 = new List<sdata>();
        List<sdata> phsd3 = new List<sdata>();
        private void Sheet_Saveph220Data(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //ÿ������д�ı�񣨲��̶�����������������һ���ڸ���ǰд����д���ݣ�һ���ڸ��º�д����д����
            //���2-2���˹�����������������Ŀ�����γɹ��� title�д���Ŀ���ƣ�str1�������������

            //�������ʱ���������б�
            phsd1.Clear();
            phsd2.Clear();
            phsd3.Clear();
            int row = tempsheet.RowCount;
            int col = tempsheet.ColumnCount;
            //ѭ����ȡ����е��������ݣ���д��sd��
            for (int i = 2; i <=8; i++)
            {
                sdata stest1 = new sdata();
                stest1.title = tempsheet.Cells[row-1, 1].Value.ToString();
                //if (tempsheet.Cells[i, 10].Value != null)
                //{
                if (tempsheet.Cells[row-1,i].Value!=null)
                {
                    stest1.str1 = tempsheet.Cells[row - 1, i].Value.ToString();
                }
                
                //}
                phsd1.Add(stest1);
                sdata stest2 = new sdata();
                stest2.title = tempsheet.Cells[row - 4, 1].Value.ToString();
                if (tempsheet.Cells[row - 4, i].Value != null)
                {
                stest2.str1 = tempsheet.Cells[row - 4, i].Value.ToString();
                }
                phsd2.Add(stest2);
                sdata stest3 = new sdata();
                stest3.title = tempsheet.Cells[row - 6, 1].Value.ToString();
                if (tempsheet.Cells[row - 6, i].Value != null)
                {
                stest3.str1 = tempsheet.Cells[row - 6, i].Value.ToString();
                }
                phsd3.Add(stest3);
            }
        }

        private void Sheet_Wirteph220Data(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //ÿ������д�ı�񣨲��̶�����������������һ���ڸ���ǰд����д���ݣ�һ���ڸ��º�д����д����
            //���ڸ������ݺ�д����д����
            int row = tempsheet.RowCount;
            for (int i =2; i < 8; i++)
            {
                //for (int j = 0; j < sd.Count; j++)
                //{
                //ͨ����Ŀ���Ƶıȶ�����д����
                if (tempsheet.Cells[row-1, 1].Value.ToString() == phsd1[i - 2].title)
                {
                    tempsheet.SetValue(row - 1, i, phsd1[i - 2].str1);
                    //�ȶ������ɾ����һ��������´αȶ����Ч��
                    //sd.Remove(sd[j]);
                    // break;
                }
                if (tempsheet.Cells[row - 4, 1].Value.ToString() == phsd2[i - 2].title)
                {
                    tempsheet.SetValue(row - 4, i, phsd2[i - 2].str1);
                    //�ȶ������ɾ����һ��������´αȶ����Ч��
                    //sd.Remove(sd[j]);
                    // break;
                }
                if (tempsheet.Cells[row - 6, 1].Value.ToString() == phsd3[i - 2].title)
                {
                    tempsheet.SetValue(row - 6, i, phsd3[i - 2].str1);
                    //�ȶ������ɾ����һ��������´αȶ����Ч��
                    //sd.Remove(sd[j]);
                    // break;
                }
                //}

            }
        }
        //��ȡ�ͱ���110����ƽ����ע�ǵ�����
        List<sdata> phsd4 = new List<sdata>();
        List<sdata> phsd5 = new List<sdata>();
        List<sdata> phsd6 = new List<sdata>();
        private void Sheet_Saveph110Data(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //ÿ������д�ı�񣨲��̶�����������������һ���ڸ���ǰд����д���ݣ�һ���ڸ��º�д����д����
            //���2-2���˹�����������������Ŀ�����γɹ��� title�д���Ŀ���ƣ�str1�������������

            //�������ʱ���������б�
            phsd4.Clear();
            phsd5.Clear();
            phsd6.Clear();
            int row = tempsheet.RowCount;
            int col = tempsheet.ColumnCount;
            //ѭ����ȡ����е��������ݣ���д��sd��
            for (int i = 2; i <= 7; i++)
            {
                sdata stest1 = new sdata();
                stest1.title = tempsheet.Cells[row - 1, 1].Value.ToString();
                //if (tempsheet.Cells[i, 10].Value != null)
                //{
                if (tempsheet.Cells[row - 1, i].Value != null)
                {
                    stest1.str1 = tempsheet.Cells[row - 1, i].Value.ToString();
                }

                //}
                phsd4.Add(stest1);
                sdata stest2 = new sdata();
                stest2.title = tempsheet.Cells[row - 4, 1].Value.ToString();
                if (tempsheet.Cells[row - 4, i].Value != null)
                {
                    stest2.str1 = tempsheet.Cells[row - 4, i].Value.ToString();
                }
                phsd5.Add(stest2);
                sdata stest3 = new sdata();
                stest3.title = tempsheet.Cells[row - 6, 1].Value.ToString();
                if (tempsheet.Cells[row - 6, i].Value != null)
                {
                    stest3.str1 = tempsheet.Cells[row - 6, i].Value.ToString();
                }
                phsd6.Add(stest3);
            }
        }

        private void Sheet_Wirteph110Data(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //ÿ������д�ı�񣨲��̶�����������������һ���ڸ���ǰд����д���ݣ�һ���ڸ��º�д����д����
            //���ڸ������ݺ�д����д����
            int row = tempsheet.RowCount;
            for (int i = 2; i < 7; i++)
            {
                //for (int j = 0; j < sd.Count; j++)
                //{
                //ͨ����Ŀ���Ƶıȶ�����д����
                if (tempsheet.Cells[row - 1, 1].Value.ToString() == phsd4[i - 2].title)
                {
                    tempsheet.SetValue(row - 1, i, phsd4[i - 2].str1);
                    //�ȶ������ɾ����һ��������´αȶ����Ч��
                    //sd.Remove(sd[j]);
                    // break;
                }
                if (tempsheet.Cells[row - 4, 1].Value.ToString() == phsd5[i - 2].title)
                {
                    tempsheet.SetValue(row - 4, i, phsd5[i - 2].str1);
                    //�ȶ������ɾ����һ��������´αȶ����Ч��
                    //sd.Remove(sd[j]);
                    // break;
                }
                if (tempsheet.Cells[row - 6, 1].Value.ToString() == phsd6[i - 2].title)
                {
                    tempsheet.SetValue(row - 6, i, phsd6[i - 2].str1);
                    //�ȶ������ɾ����һ��������´αȶ����Ч��
                    //sd.Remove(sd[j]);
                    // break;
                }
                //}

            }
        }
        /// ���������Ԥ���ṹ���������
        /// </summary>
        /// <param name="obj">SheetView����</param>

        /// <param name="intrownum">����</param>
        /// <param name="tiltle">���еͷ���</param>
        /// <param name="report">Ԥ�ⷽ��</param>
        /// <param name="forcastindex">Ԥ�ⷽ���еĵڼ���</param>
        private int Create_xdltableAnddata(FarPoint.Win.Spread.SheetView tempsheet, int intrownum, string tiltle,  Ps_forecast_list report, int forcastindex)
        {
            int typeFlag2 = 2;
            int rownum = 0;
            PSP_Types qshydl = null, tdydl = null;
            IList<PSP_Types> qxqshydl = null, qxtdydl = null;
            string con = "ProjectID='" + ProjectUID + "'and Title like'ȫ����õ���%' and Flag2='" + typeFlag2 + "'";
            qshydl = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (qshydl != null)
            {
               
                rownum += 1;
                 tempsheet.Rows.Add(intrownum - 1, 1);
                 //tempsheet.Rows[intrownum - 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                 tempsheet.Cells[intrownum - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                 //tempsheet.Cells[intrownum - 1, 8].CellType = percentcelltypes;
                 tempsheet.Cells[intrownum - 1, 8].Formula = "POWER(R"+(intrownum).ToString()+"C8/R"+(intrownum).ToString()+"C3,0.2)-1";
                 //tempsheet.Cells[intrownum - 1, 10].CellType = percentcelltypes;
                 tempsheet.Cells[intrownum - 1, 10].Formula = "POWER(R" + (intrownum).ToString() + "C10/R" + (intrownum).ToString() + "C8,0.2)-1";
                 tempsheet.SetValue(intrownum - 1, 1, "ȫ����õ���");
                 SetSheetViewColumnsWhith(tempsheet, 1, "ȫ����õ����õ���");
                 con = "ForecastID='" + report.ID + "'AND Title Like'ȫ����õ���%' AND Forecast!='0'AND ID like'%"+qshydl.ID+"%' order by Forecast";
                 IList<Ps_Forecast_Math> ycqshydl = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                 int forcast = 0;
                 
                 if (ycqshydl.Count >= forcastindex)
                {
                    forcast = ycqshydl[forcastindex - 1].Forecast;
                     for(int i=0;i<6;i++)
                     {
                         string year="y"+(2010+i).ToString();
                         //tempsheet.Cells[intrownum - 1, 2 + i].CellType = numberCellTypes1;
                         tempsheet.SetValue(intrownum - 1, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(ycqshydl[forcastindex - 1], year) * 10000);
                     }
                     tempsheet.SetValue(intrownum - 1,9, Gethistroyvalue<Ps_Forecast_Math>(ycqshydl[forcastindex - 1], "y2020") * 10000);
                     //tempsheet.Cells[intrownum - 1, 9].CellType = numberCellTypes1;
                }
                else
                 {
                     forcast = 0;
                     for (int i = 0; i < 6; i++)
                     {
                         string year = "y" + (2010 + i).ToString();
                         //tempsheet.Cells[intrownum - 1, 2 + i].CellType = numberCellTypes1;
                         tempsheet.SetValue(intrownum - 1, 2 + i, 0);
                     }
                     tempsheet.SetValue(intrownum - 1, 9, 0);
                     //tempsheet.Cells[intrownum - 1, 9].CellType = numberCellTypes1;
                 }

                con = "ProjectID='" + ProjectUID + "'and ParentID='" + qshydl.ID + "' and Flag2='" + typeFlag2 + "'";
                qxqshydl = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                for (int i = 0; i < qxqshydl.Count;i++ )
                {
                    rownum++;
                    tempsheet.Rows.Add(intrownum, 1);
                    //tempsheet.Rows[intrownum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum , 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    //tempsheet.Cells[intrownum, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum, 8].Formula = "POWER(R" + (intrownum+1).ToString() + "C8/R" + (intrownum+1).ToString() + "C3,0.2)-1";
                    //tempsheet.Cells[intrownum , 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum , 10].Formula = "POWER(R" + (intrownum+1).ToString() + "C10/R" + (intrownum+1).ToString() + "C8,0.2)-1";
                    if (i ==qxqshydl.Count-1)
                    {
                        tempsheet.SetValue(intrownum, 1, "���У�" + qxqshydl[i].Title);
                    }
                    else
                        tempsheet.SetValue(intrownum, 1, qxqshydl[i].Title);
                    if (ycqshydl.Count >= forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'" + qxqshydl[i].Title + "%' AND Forecast='" + forcast + "' AND ParentID='" + ycqshydl[forcastindex - 1].ID + "'AND ID like'%" + qxqshydl[i].ID + "%' order by Forecast";
                        Ps_Forecast_Math qydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (qydl != null)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                               // tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum, 2 + j, Gethistroyvalue<Ps_Forecast_Math>(qydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum, 9, Gethistroyvalue<Ps_Forecast_Math>(qydl, "y2020") * 10000);
                            //tempsheet.Cells[intrownum, 9].CellType=numberCellTypes1;
                        }
                        else
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum, 2 + j, 0);
                            }
                            tempsheet.SetValue(intrownum, 9, 0);
                           // tempsheet.Cells[intrownum, 9].CellType = numberCellTypes1;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            string year = "y" + (2010 + j).ToString();
                            //tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum, 2 + j, 0);
                        }
                        tempsheet.SetValue(intrownum, 9, 0);
                        //tempsheet.Cells[intrownum, 9].CellType = numberCellTypes1;
                    }
                  
                  

                }
                
            }
            con = "ProjectID='" + ProjectUID + "'and Title like'ͳ���õ���%' and Flag2='" + typeFlag2 + "'";
            tdydl = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (tdydl != null)
            {
                
                tempsheet.Rows.Add(intrownum+qxqshydl.Count, 1);
               
                //tempsheet.Rows[intrownum +qxqshydl.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                tempsheet.Cells[intrownum + qxqshydl.Count, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
               // tempsheet.Cells[intrownum + qxqshydl.Count, 8].CellType = percentcelltypes;
                tempsheet.Cells[intrownum + qxqshydl.Count, 8].Formula = "POWER(R" + (intrownum + qxqshydl.Count + 1).ToString() + "C8/R" + (intrownum + qxqshydl.Count + 1).ToString() + "C3,0.2)-1";
               // tempsheet.Cells[intrownum + qxqshydl.Count, 10].CellType = percentcelltypes;
                tempsheet.Cells[intrownum + qxqshydl.Count, 10].Formula = "POWER(R" + (intrownum + qxqshydl.Count + 1).ToString() + "C10/R" + (intrownum + qxqshydl.Count + 1).ToString() + "C8,0.2)-1";
                tempsheet.SetValue(intrownum + qxqshydl.Count, 1, "ͳ���õ���");
                con = "ForecastID='" + report.ID + "'AND Title Like'ͳ���õ���%' AND Forecast!='0'AND ID like'%" + tdydl.ID + "%' order by Forecast";
                IList<Ps_Forecast_Math> yctdydl = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                int forcast = 0;
                if (yctdydl.Count>=forcastindex)
                {
                    forcast = yctdydl[forcastindex - 1].Forecast;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                       // tempsheet.Cells[intrownum + qxqshydl.Count, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum + qxqshydl.Count, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(yctdydl[forcastindex - 1], year) * 10000);
                    }
                    tempsheet.SetValue(intrownum + qxqshydl.Count, 9, Gethistroyvalue<Ps_Forecast_Math>(yctdydl[forcastindex - 1], "y2020") * 10000);
                    //tempsheet.Cells[intrownum + qxqshydl.Count, 9].CellType = numberCellTypes1;
                }
                else
                {
                    forcast = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                        //tempsheet.Cells[intrownum + qxqshydl.Count, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum + qxqshydl.Count, 2 + i, 0);
                    }
                    tempsheet.SetValue(intrownum + qxqshydl.Count, 9, 0);
                    //tempsheet.Cells[intrownum + qxqshydl.Count,9].CellType = numberCellTypes1;
                }
                rownum += 1;

                
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + tdydl.ID + "' and Flag2='" + typeFlag2 + "'";
                qxtdydl = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                for (int i = 0; i < qxtdydl.Count;i++ )
                {
                    rownum++;
                    tempsheet.Rows.Add(intrownum + qxqshydl.Count+1, 1);
                    //tempsheet.Rows[intrownum + qxqshydl.Count + 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum + qxqshydl.Count + 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                   // tempsheet.Cells[intrownum + qxqshydl.Count + 1, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum + qxqshydl.Count + 1, 8].Formula = "POWER(R" + (intrownum + qxqshydl.Count + 1 + 1).ToString() + "C8/R" + (intrownum + qxqshydl.Count + 1 + 1).ToString() + "C3,0.2)-1";
                    //tempsheet.Cells[intrownum + qxqshydl.Count + 1, 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum + qxqshydl.Count + 1, 10].Formula = "POWER(R" + (intrownum + qxqshydl.Count + 1 + 1).ToString() + "C10/R" + (intrownum + qxqshydl.Count + 1 + 1).ToString() + "C8,0.2)-1";
                    if (i == qxtdydl.Count-1)
                    {
                        tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 1, "���У�" + qxtdydl[i].Title);
                    }
                    else
                        tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 1, qxtdydl[i].Title);
                    if (qxtdydl.Count>=forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'" + qxtdydl[i].Title + "%' AND Forecast='" + forcast + "' AND ParentID='" + yctdydl[forcastindex - 1].ID + "'AND ID like'%" + qxtdydl[i].ID + "%' order by Forecast";
                        Ps_Forecast_Math tydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (tydl != null)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                               // tempsheet.Cells[intrownum + qxqshydl.Count + 1, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 2 + j, Gethistroyvalue<Ps_Forecast_Math>(tydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 9, Gethistroyvalue<Ps_Forecast_Math>(tydl, "y2020") * 10000);
                            //tempsheet.Cells[intrownum + qxqshydl.Count + 1,9].CellType = numberCellTypes1;
                        }
                        else
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                               // tempsheet.Cells[intrownum + qxqshydl.Count + 1, 2 + i].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 2 + i, 0);
                            }
                            tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 9, 0);
                           // tempsheet.Cells[intrownum + qxqshydl.Count + 1,9].CellType = numberCellTypes1;
                        }
                    }
                   else
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            string year = "y" + (2010 + j).ToString();
                            //tempsheet.Cells[intrownum + qxqshydl.Count + 1, 2 + i].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 2 + i, 0);
                        }
                        tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 9, 0);
                        //tempsheet.Cells[intrownum + qxqshydl.Count + 1,9].CellType = numberCellTypes1;
                    }

                }
             
            }
            //tempsheet.Cells[intrownum - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            tempsheet.SetValue(intrownum-1, 0, tiltle);
            tempsheet.Cells[intrownum-1, 0].RowSpan = rownum;           
            return rownum;
        }

        /// ������󸺺�Ԥ���ṹ���������
        /// </summary>
        /// <param name="obj">SheetView����</param>

        /// <param name="intrownum">����</param>
        /// <param name="tiltle">���еͷ���</param>
        /// <param name="report">Ԥ�ⷽ��</param>
        /// <param name="forcastindex">Ԥ�ⷽ���еĵڼ���</param>
        private int Create_zdfhtableAnddata(FarPoint.Win.Spread.SheetView tempsheet, int intrownum, string tiltle, Ps_forecast_list report, int forcastindex)
        {
            int typeFlag2 = 2;
            int rownum = 0;
            PSP_Types qshydl = null, tdydl = null;
            IList<PSP_Types> qxqshydl = null, qxtdydl = null;
            string con = "ProjectID='" + ProjectUID + "'and Title like'ȫ�����󸺺�%' and Flag2='" + typeFlag2 + "'";
            qshydl = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (qshydl != null)
            {

                rownum += 1;
                tempsheet.Rows.Add(intrownum - 1, 1);
               // tempsheet.Rows[intrownum - 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                tempsheet.Cells[intrownum - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
               // tempsheet.Cells[intrownum - 1, 8].CellType = percentcelltypes;
                tempsheet.Cells[intrownum - 1, 8].Formula = "POWER(R" + (intrownum).ToString() + "C8/R" + (intrownum).ToString() + "C3,0.2)-1";
               // tempsheet.Cells[intrownum - 1, 10].CellType = percentcelltypes;
                tempsheet.Cells[intrownum - 1, 10].Formula = "POWER(R" + (intrownum).ToString() + "C10/R" + (intrownum).ToString() + "C8,0.2)-1";
                tempsheet.SetValue(intrownum - 1, 1, "ȫ�����󸺺�");
                SetSheetViewColumnsWhith(tempsheet, 1, "ȫ����õ����õ���");
                con = "ForecastID='" + report.ID + "'AND Title Like'ȫ�����󸺺�%' AND Forecast!='0'AND ID like'%" + qshydl.ID + "%' order by Forecast";
                IList<Ps_Forecast_Math> ycqshydl = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                int forcast = 0;

                if (ycqshydl.Count >= forcastindex)
                {
                    forcast = ycqshydl[forcastindex - 1].Forecast;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                       // tempsheet.Cells[intrownum - 1, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum - 1, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(ycqshydl[forcastindex - 1], year));
                    }
                    tempsheet.SetValue(intrownum - 1, 9, Gethistroyvalue<Ps_Forecast_Math>(ycqshydl[forcastindex - 1], "y2020") );
                    tempsheet.Cells[intrownum - 1, 9].CellType = numberCellTypes1;
                }
                else
                {
                    forcast = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                        //tempsheet.Cells[intrownum - 1, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum - 1, 2 + i, 0);
                    }
                    tempsheet.SetValue(intrownum - 1, 9, 0);
                    //tempsheet.Cells[intrownum - 1, 9].CellType = numberCellTypes1;
                }

                con = "ProjectID='" + ProjectUID + "'and ParentID='" + qshydl.ID + "' and Flag2='" + typeFlag2 + "'and Title !='��󸺺�����Сʱ��'";
                qxqshydl = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                for (int i = 0; i < qxqshydl.Count; i++)
                {
                    rownum++;
                    tempsheet.Rows.Add(intrownum, 1);
                   // tempsheet.Rows[intrownum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                   // tempsheet.Cells[intrownum, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum, 8].Formula = "POWER(R" + (intrownum + 1).ToString() + "C8/R" + (intrownum + 1).ToString() + "C3,0.2)-1";
                    //tempsheet.Cells[intrownum, 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum, 10].Formula = "POWER(R" + (intrownum + 1).ToString() + "C10/R" + (intrownum + 1).ToString() + "C8,0.2)-1";
                    if (i == qxqshydl.Count-1)
                    {
                        tempsheet.SetValue(intrownum, 1, "���У�" + qxqshydl[i].Title);
                    }
                    else
                        tempsheet.SetValue(intrownum, 1, qxqshydl[i].Title);
                    if (ycqshydl.Count >= forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'" + qxqshydl[i].Title + "%' AND Forecast='" + forcast + "' AND ParentID='" + ycqshydl[forcastindex-1].ID + "'AND ID like'%" + qxqshydl[i].ID + "%' order by Forecast";
                        Ps_Forecast_Math qydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (qydl != null)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum, 2 + j, Gethistroyvalue<Ps_Forecast_Math>(qydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum, 9, Gethistroyvalue<Ps_Forecast_Math>(qydl, "y2020") * 10000);
                           // tempsheet.Cells[intrownum, 9].CellType = numberCellTypes1;                     
                        }
                        else
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum, 2 + j, 0);
                            }
                            tempsheet.SetValue(intrownum, 9, 0);
                           // tempsheet.Cells[intrownum, 9].CellType = numberCellTypes1;    
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            string year = "y" + (2010 + j).ToString();
                            //tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum, 2 + j, 0);
                        }
                        tempsheet.SetValue(intrownum, 9, 0);
                       // tempsheet.Cells[intrownum, 9].CellType = numberCellTypes1;    
                    }
                   

                }
                tempsheet.Rows.Add(intrownum+rownum-1, 1);
                tempsheet.SetValue(intrownum+rownum-1,1,"ͬʱ��");
                for (int i=0;i<6;i++)
                {
                     tempsheet.Cells[intrownum+rownum-1,2+i].Formula="R"+(intrownum ).ToString()+"C"+(3+i).ToString()+"/SUM(R"+(intrownum+1).ToString()+"C"+(3+i).ToString()+":R"+(intrownum+qxqshydl.Count).ToString()+"C"+(3+i).ToString()+")";

                }
                tempsheet.Cells[intrownum+rownum-1,9].Formula="R"+(intrownum ).ToString()+"C10/SUM(R"+(intrownum+1).ToString()+"C10:R"+(intrownum+qxqshydl.Count).ToString()+"C10)";
                rownum++;
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + qshydl.ID + "' and Flag2='" + typeFlag2 + "'and Title LIKE'��󸺺�����Сʱ��%'";
                PSP_Types zudfh = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                if (zudfh!=null)
                {
                    tempsheet.Rows.Add(intrownum+rownum,1);
                    tempsheet.SetValue(intrownum+rownum,1,"��󸺺�����Сʱ��");
                     //tempsheet.Rows[intrownum+rownum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum+rownum, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    //tempsheet.Cells[intrownum+rownum, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum+rownum, 8].Formula = "POWER(R" + (intrownum+rownum + 1).ToString() + "C8/R" + (intrownum+rownum + 1).ToString() + "C3,0.2)-1";
                    //tempsheet.Cells[intrownum+rownum, 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum+rownum, 10].Formula = "POWER(R" + (intrownum+rownum + 1).ToString() + "C10/R" + (intrownum+rownum + 1).ToString() + "C8,0.2)-1";
                    if (ycqshydl.Count>=forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'��󸺺�����Сʱ��%' AND Forecast='" + forcast + "' AND ParentID='" + ycqshydl[forcastindex-1].ID + "'AND ID like'%" + zudfh.ID + "%' order by Forecast";
                        Ps_Forecast_Math qydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (qydl != null)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                string year = "y" + (2010 + i).ToString();
                                //tempsheet.Cells[intrownum + rownum, 2 + i].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + rownum, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(qydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum + rownum, 9, Gethistroyvalue<Ps_Forecast_Math>(qydl, "y2020") * 10000);
                           // tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                        }
                        else
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                string year = "y" + (2010 + i).ToString();
                               // tempsheet.Cells[intrownum + rownum, 2 + i].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + rownum, 2 + i, 0);
                            }
                            tempsheet.SetValue(intrownum + rownum, 9, 0);
                            //tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                        }
                    }
                   else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            string year = "y" + (2010 + i).ToString();
                            //tempsheet.Cells[intrownum + rownum, 2 + i].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum + rownum, 2 + i, 0);
                        }
                        tempsheet.SetValue(intrownum + rownum, 9, 0);
                        //tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                    }
                    rownum++;

                }
                 
               
            }
            con = "ProjectID='" + ProjectUID + "'and Title like'ͳ����󸺺�%' and Flag2='" + typeFlag2 + "'";
            tdydl = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            int zongnum=rownum;
            if (tdydl != null)
            {

                tempsheet.Rows.Add(intrownum + rownum-1, 1);

                //tempsheet.Rows[intrownum +  rownum-1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                tempsheet.Cells[intrownum +  rownum-1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
               // tempsheet.Cells[intrownum +  rownum-1, 8].CellType = percentcelltypes;
                tempsheet.Cells[intrownum +  rownum-1, 8].Formula = "POWER(R" + (intrownum + rownum-1 + 1).ToString() + "C8/R" + (intrownum + rownum-1 + 1).ToString() + "C3,0.2)-1";
                //tempsheet.Cells[intrownum +  rownum-1, 10].CellType = percentcelltypes;
                tempsheet.Cells[intrownum +  rownum-1, 10].Formula = "POWER(R" + (intrownum + rownum-1 + 1).ToString() + "C10/R" + (intrownum + rownum-1 + 1).ToString() + "C8,0.2)-1";
                tempsheet.SetValue(intrownum +  rownum-1, 1, "ͳ����󸺺�");
                con = "ForecastID='" + report.ID + "'AND Title Like'ͳ����󸺺�%' AND Forecast!='0'AND ID like'%" + tdydl.ID + "%' order by Forecast";
                IList<Ps_Forecast_Math> yctdydl = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                int forcast = 0;
                if (yctdydl.Count >= forcastindex)
                {
                    forcast = yctdydl[forcastindex - 1].Forecast;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                        //tempsheet.Cells[intrownum + rownum - 1, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum + rownum-1, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(yctdydl[forcastindex - 1], year) * 10000);
                    }
                    tempsheet.SetValue(intrownum + rownum-1, 9, Gethistroyvalue<Ps_Forecast_Math>(yctdydl[forcastindex - 1], "y2020") * 10000);
                    //tempsheet.Cells[intrownum + rownum - 1, 9].CellType = numberCellTypes1;

                }
                else
                {
                    forcast = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                        //tempsheet.Cells[intrownum + rownum - 1, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum + rownum-1, 2 + i, 0);
                    }
                    tempsheet.SetValue(intrownum + rownum-1, 9, 0);
                    //tempsheet.Cells[intrownum + rownum - 1, 9].CellType = numberCellTypes1;
                }
                zongnum += 1;

                con = "ProjectID='" + ProjectUID + "'and ParentID='" + tdydl.ID + "' and Flag2='" + typeFlag2 + "'and Title !='��󸺺�����Сʱ��'";
                qxtdydl = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                for (int i = 0; i < qxtdydl.Count; i++)
                {
                    zongnum++;
                    tempsheet.Rows.Add(intrownum + rownum, 1);
                   // tempsheet.Rows[intrownum + rownum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum +rownum, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                   // tempsheet.Cells[intrownum + rownum, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum + rownum, 8].Formula = "POWER(R" + (intrownum +rownum + 1).ToString() + "C8/R" + (intrownum + rownum+ 1).ToString() + "C3,0.2)-1";
                   // tempsheet.Cells[intrownum + rownum, 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum + rownum, 10].Formula = "POWER(R" + (intrownum + rownum+ 1).ToString() + "C10/R" + (intrownum +rownum + 1).ToString() + "C8,0.2)-1";
                    if (i == qxtdydl.Count-1)
                    {
                        tempsheet.SetValue(intrownum + rownum, 1, "���У�" + qxtdydl[i].Title);
                    }
                    else
                        tempsheet.SetValue(intrownum + rownum, 1, qxtdydl[i].Title);
                    if (yctdydl.Count>=forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'" + qxtdydl[i].Title + "%' AND Forecast='" + forcast + "' AND ParentID='" + yctdydl[forcastindex - 1].ID + "'AND ID like'%" + qxtdydl[i].ID + "%' order by Forecast";
                        Ps_Forecast_Math tydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (tydl != null)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                               // tempsheet.Cells[intrownum + rownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + rownum, 2 + j, Gethistroyvalue<Ps_Forecast_Math>(tydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum + rownum, 9, Gethistroyvalue<Ps_Forecast_Math>(tydl, "y2020") * 10000);
                           // tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                        }
                        else
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum + rownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + rownum, 2 + j, 0);
                            }
                            tempsheet.SetValue(intrownum + rownum, 9, 0);
                            //tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                        }

                    }
                    else
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            string year = "y" + (2010 + j).ToString();
                           // tempsheet.Cells[intrownum + rownum, 2 + j].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum + rownum, 2 + j, 0);
                        }
                        tempsheet.SetValue(intrownum + rownum, 9, 0);
                       // tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                    }
                   
                }
                 tempsheet.Rows.Add(intrownum+rownum+qxtdydl.Count, 1);
                 tempsheet.SetValue(intrownum + rownum + qxtdydl.Count, 1, "ͬʱ��");
                for (int i=0;i<6;i++)
                {
                     tempsheet.Cells[intrownum+rownum+qxtdydl.Count,2+i].Formula="R"+(intrownum + rownum ).ToString()+"C"+(3+i).ToString()+"/SUM(R"+(intrownum + rownum+1).ToString()+"C"+(3+i).ToString()+":R"+(intrownum+rownum+qxtdydl.Count).ToString()+"C"+(3+i).ToString()+")";

                }
                tempsheet.Cells[intrownum+rownum+qxtdydl.Count,9].Formula="R"+(intrownum+ rownum ).ToString()+"C10/SUM(R"+(intrownum+ rownum+1).ToString()+"C10:R"+(intrownum+rownum+qxtdydl.Count).ToString()+"C10)";
                zongnum++;
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + tdydl.ID + "' and Flag2='" + typeFlag2 + "'and Title LIKE'��󸺺�����Сʱ��%'";
                PSP_Types zudfh = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                if (zudfh!=null)
                {
                    tempsheet.Rows.Add(intrownum+zongnum,1);
                    tempsheet.SetValue(intrownum+zongnum,1,"��󸺺�����Сʱ��");
                    // tempsheet.Rows[intrownum+zongnum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum+zongnum, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                   // tempsheet.Cells[intrownum+zongnum, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum+zongnum, 8].Formula = "POWER(R" + (intrownum+zongnum + 1).ToString() + "C8/R" + (intrownum+zongnum+ 1).ToString() + "C3,0.2)-1";
                  //  tempsheet.Cells[intrownum+zongnum, 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum+zongnum, 10].Formula = "POWER(R" + (intrownum+zongnum + 1).ToString() + "C10/R" + (intrownum+zongnum+ 1).ToString() + "C8,0.2)-1";
                    if (yctdydl.Count >= forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'��󸺺�����Сʱ��%' AND Forecast='" + forcast + "' AND ParentID='" + yctdydl[forcastindex].ID + "'AND ID like'%" + zudfh.ID + "%' order by Forecast";
                        Ps_Forecast_Math qydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (qydl != null)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum + zongnum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + zongnum, 2 + j, Gethistroyvalue<Ps_Forecast_Math>(qydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum + zongnum, 9, Gethistroyvalue<Ps_Forecast_Math>(qydl, "y2020") * 10000);
                           // tempsheet.Cells[intrownum + zongnum, 9].CellType = numberCellTypes1;
                        }
                        else
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum + zongnum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + zongnum, 2 + j, 0);
                            }
                            tempsheet.SetValue(intrownum + zongnum, 9, 0);
                           // tempsheet.Cells[intrownum + zongnum, 9].CellType = numberCellTypes1;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            string year = "y" + (2010 + j).ToString();
                           // tempsheet.Cells[intrownum + zongnum, 2 + j].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum + zongnum, 2 + j, 0);
                        }
                        tempsheet.SetValue(intrownum + zongnum, 9, 0);
                       // tempsheet.Cells[intrownum + zongnum, 9].CellType = numberCellTypes1;
                    }
                    zongnum++;

                }
                 

            }
            tempsheet.Cells[intrownum - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            tempsheet.SetValue(intrownum - 1, 0, tiltle);
            tempsheet.Cells[intrownum-1, 0].RowSpan =zongnum;
            return zongnum;
        }
        /// ���������߱����õ�Ԫ�����е���ʽ ͬʱ�������
        /// </summary>
        /// <param name="obj">SheetView����</param>
        
        /// <param name="colnum">����</param>
        /// <param name="tiltle">��ͷ����</param>
        /// 
        /// <param name="firyear">�Ƚϵ�һ��</param>
        /// <param name="endyear">�Ƚϵڶ���</param>
        /// <param name="Areaid">����</param>
        private void Create_Dxtable(FarPoint.Win.Spread.SheetView tempsheet,int colnum,string tiltle,int firyear,int endyear,string Areaid)
        {
            //��ȡfiryear��endyear �������ļ�������
            string con = "IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' and  year(BurdenDate)='" + firyear + "' and AreaID='" + Areaid + "' AND Season='�ļ�'";
            BurdenLine firsumbl = (BurdenLine)Services.BaseService.GetObject("SelectBurdenLineByType", con);
            con = "IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' and  year(BurdenDate)='" + firyear + "' and AreaID='" + Areaid + "' AND Season='����'";
            BurdenLine firsnowbl = (BurdenLine)Services.BaseService.GetObject("SelectBurdenLineByType", con);
            con = "IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' and  year(BurdenDate)='" + endyear + "' and AreaID='" + Areaid + "' AND Season='�ļ�'";
            BurdenLine endsumbl = (BurdenLine)Services.BaseService.GetObject("SelectBurdenLineByType", con);
            con = "IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' and  year(BurdenDate)='" + endyear + "' and AreaID='" + Areaid + "' AND Season='����'";
            BurdenLine endsnowbl = (BurdenLine)Services.BaseService.GetObject("SelectBurdenLineByType", con);
            for (int i = 0; i < 31; i++)
            {
                for (int j = colnum-1; j < colnum+5; j++)
                {
                    //������
                    tempsheet.Cells[i, j].Border = border;
                    //ˮƽ�ʹ�ֱ�����ж���
                    tempsheet.Cells[i, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[i, j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }
            }
            tempsheet.Cells[0, colnum-1].ColumnSpan = 6;
            tempsheet.Cells[0, colnum - 1].Font = new Font("����", 9, FontStyle.Bold);
            tempsheet.SetValue(0, colnum - 1, tiltle);
            tempsheet.Cells[1, colnum - 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            tempsheet.SetValue(1, colnum - 1, "��λ����ǧ��");
            tempsheet.Cells[1, colnum - 1].ColumnSpan = 6;
            tempsheet.SetValue(2, colnum - 1, firstyear.ToString() + "��");
            tempsheet.Cells[2, colnum].ColumnSpan = 2;
            tempsheet.SetValue(2, colnum, "���͸�����");
            tempsheet.SetValue(2, colnum +2,endyear.ToString() + "��");
            tempsheet.Cells[2,colnum+3].ColumnSpan=2;
            tempsheet.SetValue(2, colnum + 3, "���͸�����");
            tempsheet.SetValue(3,colnum,"�ļ�");
            tempsheet.SetValue(3,colnum+1,"����");
            tempsheet.SetValue(3,colnum+3,"�ļ�");
            tempsheet.SetValue(3,colnum+4,"����");
            tempsheet.SetValue(4,colnum-1,"Сʱ");
            tempsheet.SetValue(4,colnum+2,"Сʱ");
            for (int i=1;i<=24;i++)
            {
              tempsheet.SetValue(4+i,colnum-1,i.ToString());
                if (firsumbl!=null)
                {
                    string name = "Hour" + i.ToString();
                    tempsheet.SetValue(4 + i, colnum, Gethistroyvalue<BurdenLine>(firsumbl,name));
                }
                else
                    tempsheet.SetValue(4 + i, colnum, 0);
                if (firsnowbl!=null)
                {
                    string name = "Hour" + i.ToString();
                    tempsheet.SetValue(4 + i, colnum+1, Gethistroyvalue<BurdenLine>(firsnowbl, name));
                }
                else
                    tempsheet.SetValue(4 + i, colnum+1, 0);
              tempsheet.SetValue(4+i,colnum+2,i.ToString());
              if (endsumbl != null)
              {
                  string name = "Hour" + i.ToString();
                  tempsheet.SetValue(4 + i, colnum+3, Gethistroyvalue<BurdenLine>(endsumbl, name));
              }
              else
                  tempsheet.SetValue(4 + i, colnum+3, 0);
              if (endsnowbl != null)
              {
                  string name = "Hour" + i.ToString();
                  tempsheet.SetValue(4 + i, colnum + 4, Gethistroyvalue<BurdenLine>(endsnowbl, name));
              }
              else
                  tempsheet.SetValue(4 + i, colnum +4, 0);
            }
            tempsheet.Cells[29, colnum].CellType = percentcelltypes;
            tempsheet.Cells[29, colnum+1].CellType = percentcelltypes;
            tempsheet.Cells[29, colnum+3].CellType = percentcelltypes;
            tempsheet.Cells[29, colnum+4].CellType = percentcelltypes;
            tempsheet.Cells[30, colnum].CellType = percentcelltypes;
            tempsheet.Cells[30, colnum + 1].CellType = percentcelltypes;
            tempsheet.Cells[30, colnum + 3].CellType = percentcelltypes;
            tempsheet.Cells[30, colnum + 4].CellType = percentcelltypes;
            tempsheet.SetValue(29,colnum-1,"��ƽ��������");
            SetSheetViewColumnsWhith(tempsheet, colnum - 1, "��ƽ��������");
            tempsheet.SetValue(29, colnum + 2, "��ƽ��������");
            SetSheetViewColumnsWhith(tempsheet, colnum +2, "��ƽ��������");
            tempsheet.SetValue(30, colnum - 1, "����С������");
            tempsheet.SetValue(30, colnum + 2, "����С������");
            if (firsumbl!=null)
            {
              tempsheet.SetValue(29,colnum,Gethistroyvalue<BurdenLine>(firsumbl, "DayAverage"));
              tempsheet.SetValue(30, colnum, Gethistroyvalue<BurdenLine>(firsumbl,"MinAverage" ));
            }
          else
            {
                tempsheet.SetValue(29, colnum, 0);
                tempsheet.SetValue(30, colnum, 0);
            }
            if (firsnowbl != null)
            {
                tempsheet.SetValue(29, colnum+1, Gethistroyvalue<BurdenLine>(firsnowbl, "DayAverage"));
                tempsheet.SetValue(30, colnum+1, Gethistroyvalue<BurdenLine>(firsnowbl, "MinAverage"));
            }
            else
            {
                tempsheet.SetValue(29, colnum+1, 0);
                tempsheet.SetValue(30, colnum+1, 0);
            }

            if (endsumbl != null)
            {
                tempsheet.SetValue(29, colnum+3, Gethistroyvalue<BurdenLine>(endsumbl, "DayAverage"));
                tempsheet.SetValue(30, colnum+3, Gethistroyvalue<BurdenLine>(endsumbl, "MinAverage"));
            }
            else
            {
                tempsheet.SetValue(29, colnum+3, 0);
                tempsheet.SetValue(30, colnum+3, 0);
            }
            if (endsnowbl != null)
            {
                tempsheet.SetValue(29, colnum + 4, Gethistroyvalue<BurdenLine>(endsnowbl, "DayAverage"));
                tempsheet.SetValue(30, colnum + 4, Gethistroyvalue<BurdenLine>(endsnowbl, "MinAverage"));
            }
            else
            {
                tempsheet.SetValue(29, colnum + 4, 0);
                tempsheet.SetValue(30, colnum + 4, 0);
            }
          
        }
          /// <summary>
        ///����󸺺����ݱ����õ�Ԫ�����е���ʽ ͬʱ�������
        /// </summary>
        /// <param name="obj">SheetView����</param>
        
        /// <param name="colnum">����</param>
        /// <param name="tiltle">��ͷ����</param>
        /// <param name="firyear">�Ƚϵ�һ��</param>
        /// <param name="endyear">�Ƚϵڶ���</param>
        /// <param name="Areaid">����</param>
        private void Create_YZDFHtable(FarPoint.Win.Spread.SheetView tempsheet,int rownum,string tiltle,int firyear,int endyear,string Areaid)
        {
            //��ȡ����
            string con = "uid like '%" + Itop.Client.MIS.ProgUID + "%' and BurdenYear='"+firyear+"'and AreaID='"+Areaid+"'";
            BurdenMonth firbm = (BurdenMonth)Services.BaseService.GetObject("SelectBurdenMonthByWhere", con);

            con = "uid like '%" + Itop.Client.MIS.ProgUID + "%' and BurdenYear='" + endyear + "'and AreaID='" + Areaid + "'";
            BurdenMonth endbm = (BurdenMonth)Services.BaseService.GetObject("SelectBurdenMonthByWhere", con);

            for (int i = rownum - 1; i < rownum+13; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    //������
                    tempsheet.Cells[i, j].Border = border;
                    //ˮƽ�ʹ�ֱ�����ж���
                    tempsheet.Cells[i, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[i, j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }
            }
            tempsheet.Cells[rownum-1,0].ColumnSpan = 15;
            tempsheet.Cells[rownum - 1, 0].Font = new Font("����", 9, FontStyle.Bold);
            tempsheet.SetValue(rownum - 1, 0, tiltle);
            tempsheet.Cells[rownum, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            tempsheet.SetValue(rownum, 0, "��λ����ǧ��");
            tempsheet.Cells[rownum, 0].ColumnSpan = 15;

            tempsheet.SetValue(rownum + 1, 0, "���");
            tempsheet.Cells[rownum + 1, 0].RowSpan = 2;
            tempsheet.SetValue(rownum + 1, 1, "ָ��");
            tempsheet.Cells[rownum + 1, 1].RowSpan = 2;
            tempsheet.SetValue(rownum + 1, 2, "��λ");
            tempsheet.Cells[rownum + 1, 2].RowSpan = 2;
            tempsheet.SetValue(rownum + 1, 3, "��������");
            tempsheet.Cells[rownum + 1, 3].ColumnSpan = 12;
            tempsheet.SetValue(rownum + 3,0,firyear+ "��");
            tempsheet.Cells[rownum + 3, 0].RowSpan = 5;
            tempsheet.SetValue(rownum + 3, 1, "����󸺺�");
            tempsheet.SetValue(rownum + 3, 2, "��ǧ��");
            tempsheet.SetValue(rownum + 4, 1, "��ƽ���ո�����");
            tempsheet.SetValue(rownum + 4, 2, "%");

            tempsheet.SetValue(rownum + 5, 1, "����С�ո�����");
            tempsheet.SetValue(rownum + 5,2, "%");
            tempsheet.SetValue(rownum + 6, 1, "������շ�Ȳ�");
            tempsheet.SetValue(rownum + 6, 2, "��ǧ��");

            tempsheet.SetValue(rownum + 7, 1, "������շ�Ȳ���");
            tempsheet.SetValue(rownum + 7, 2, "%");

            tempsheet.SetValue(rownum + 8, 0, endyear + "��");
            tempsheet.Cells[rownum +8, 0].RowSpan = 5;
            tempsheet.SetValue(rownum + 8, 1, "����󸺺�");
            tempsheet.SetValue(rownum + 8, 2, "��ǧ��");
            tempsheet.SetValue(rownum + 9, 1, "��ƽ���ո�����");
            tempsheet.SetValue(rownum + 9, 2, "%");

            tempsheet.SetValue(rownum + 10, 1, "����С�ո�����");
            tempsheet.SetValue(rownum +10, 2, "%");
            tempsheet.SetValue(rownum + 11, 1, "������շ�Ȳ�");
            tempsheet.SetValue(rownum + 11, 2, "��ǧ��");

            tempsheet.SetValue(rownum + 12, 1, "������շ�Ȳ���");
            tempsheet.SetValue(rownum + 12, 2, "%");
            for (int i = 1; i <= 12;i++ )
            {
                tempsheet.SetValue(rownum + 2, 2+i,i.ToString());
                tempsheet.Cells[rownum + 3,2+i].CellType = numberCellTypes1;
                if (firbm!=null)
                {
                    tempsheet.SetValue(rownum + 3, 2 + i, Gethistroyvalue<BurdenMonth>(firbm, "Month" + i.ToString()));
                }
                else
                    tempsheet.SetValue(rownum + 3, 2 + i, 0);
                //�ڴ���ӵ�һ������
                tempsheet.Cells[rownum + 4, 2 + i].CellType = percentcelltypes;
                tempsheet.Cells[rownum + 5, 2 + i].CellType = percentcelltypes;
                tempsheet.Cells[rownum + 6, 2 + i].CellType =numberCellTypes1;
                tempsheet.Cells[rownum +7, 2 + i].CellType = percentcelltypes;

                //�ڴ���ӵڶ�������
                tempsheet.Cells[rownum + 8, 2 + i].CellType = numberCellTypes1;
                if (endbm != null)
                {
                    tempsheet.SetValue(rownum +8, 2 + i, Gethistroyvalue<BurdenMonth>(endbm, "Month" + i.ToString()));
                }
                else
                    tempsheet.SetValue(rownum + 8, 2 + i, 0);
                tempsheet.Cells[rownum + 9, 2 + i].CellType = percentcelltypes;
                tempsheet.Cells[rownum + 10, 2 + i].CellType = percentcelltypes;
                tempsheet.Cells[rownum + 11, 2 + i].CellType = numberCellTypes1;
                tempsheet.Cells[rownum + 12, 2 + i].CellType = percentcelltypes;
            }

        }
        /// <summary>
        /// ���õ�Ԫ���еĿ��
        /// </summary>
        /// <param name="obj">SheetView����</param>
        /// <param name="rownum">����</param>
        /// <param name="colnum">����</param>
        private void Sheet_GridandColor(FarPoint.Win.Spread.SheetView tempsheet,int rownum,int colnum)
        {
            //���ñ����
            //���õ�Ԫ�����ݾ���
            //int rowcount = tempsheet.Rows.Count;
            //int colcount = tempsheet.Columns.Count;
            for (int i = 0; i < colnum;i++ )
            {
                tempsheet.Columns[i].Border = border;
            }
            //for (int i = 0; i < rownum; i++)
            //{
            //    for (int j = 0; j < colnum; j++)
            //    {
            //        //������
            //        tempsheet.Cells[i, j].Border = border;
            //        //ˮƽ�ʹ�ֱ�����ж���
            //        //tempsheet.Cells[i, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            //        //tempsheet.Cells[i, j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //    }
            //}

        }
        /// <summary>
        /// ���õ�Ԫ���еĿ��
        /// </summary>
        /// <param name="obj">SheetView����</param>
        /// <param name="Col">�ĸ���</param>
        /// <param name="Title">����</param>
        private void SetSheetViewColumnsWhith(FarPoint.Win.Spread.SheetView obj, int Col, string Title)
        {
            int len = 0;
            const int Pixe = 12;//һ��������ʮ���ֽ������Ҷ��2��
            len = Title.Length * Pixe;
            obj.SetColumnWidth(Col, len);
        }
        /// <summary>
        ///����к���ӱ���
        /// </summary>
        /// <param name="obj">SheetView����</param>
        /// <param name="intCol">��ʼ��</param>
        /// <param name="colnum">�е���Ŀ</param>
        /// <param name="rowtitle">��һ��Ϊ������</param>
        /// <param name="Title">�еı�����</param>
        private void AddcolAndtitle(FarPoint.Win.Spread.SheetView obj, int intCol,int colnum,int rowtitle, List<string> Title)
        {
            for (int i = 0; i < colnum;i++ )
            {
                obj.Columns.Add(intCol, 1);
                obj.SetValue(rowtitle, intCol, Title[colnum - 1 - i]);
                //�趨���
                SetSheetViewColumnsWhith(obj, intCol, Title[colnum - 1 - i]);
            }
            
        }
        /// <summary>
        ///����к���ӱ���
        /// </summary>
        /// <param name="obj">SheetView����</param>
        /// <param name="intCol">��ʼ��</param>
        /// <param name="rownum">�е���Ŀ</param>
        /// <param name="coltitle">��һ��Ϊ������</param>
        /// <param name="Title">�еı�����</param>
        private void AddrowAndtitle(FarPoint.Win.Spread.SheetView obj, int intRow, int rownum, int coltitle, List<string> Title)
        {
            for (int i = 0; i < rownum; i++)
            {
                obj.Rows.Add(intRow, 1);
                obj.SetValue(intRow, coltitle, Title[rownum - 1 - i]);
                //�趨���
                SetSheetViewColumnsWhith(obj, coltitle, Title[rownum - 1 - i]);
            }

        }
        /// <summary>
        /// �ϲ���Ԫ��
        /// </summary>
        /// <param name="obj">�������</param>
        /// <param name="RowStep">Ҫ�ϲ���Ԫ�������,��</param>
        /// <param name="ColStep">Ҫ�ϲ���Ԫ�������,��</param>
        /// <param name="Row">��</param>
        /// <param name="Col">��</param>
        /// <param name="Title">����</param>
        public void CreateSheetView(FarPoint.Win.Spread.SheetView obj, int RowStep, int ColStep, int Row, int Col, string Title)
        {
            FarPoint.Win.Spread.Cell acell;
            acell = obj.Cells[Row, Col];
            acell.ColumnSpan = ColStep;
            acell.RowSpan = RowStep;
            acell.Text = Title;

            acell.HorizontalAlignment = HAlignment;
            acell.VerticalAlignment = VAlignment;
        }
        /// <summary>
        /// ��ö�����ĳһ���Ե���ֵ
        /// </summary>
        /// <param name="Ps">����</param>
        /// <param name="name">��ֵ</param>
       
        private double Gethistroyvalue<T>(T ps,string name)
        {
            Type type = typeof(T);
            double psvalue = 0.0;
            foreach (PropertyInfo pi in type.GetProperties())
            {
                
                if (pi.Name == name)
                {
                   psvalue =Convert.ToDouble(pi.GetValue(ps,null) );
                   break;
                }
                    //pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
                
            }
            return psvalue;
        }
        /// <summary>
        /// ��������ĳһ���Ը�ֵ��ֵ
        /// </summary>
        /// <param name="Ps">����</param>
        /// <param name="name">��ֵ</param>
        private void sethistoyvalue<T>(T ps,string name,object obj)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.Name==name)
                {
                    
                    try
                    {
                        pi.SetValue(ps, obj != DBNull.Value ? obj : 0, null);
                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }
                }
               
            }
        }
        /// <summary>
        /// ��������ĳһ����ֵ�ӵ���һ�����ֵ����ֵ
        /// </summary>
        /// <param name="Ps">����1</param>
        /// /// <param name="Ps1">����2</param>
        /// <param name="name">������ֵ</param>
        /// 
        private void addhistoyvalue<T>(T ps,T ps1 ,string name)
        {
            Type type = typeof(T);
            double sum = 0;
            foreach (PropertyInfo pi in type.GetProperties())
            {
                
                if (pi.Name == name)
                {

                    try
                    {
                        sum = Convert.ToDouble(pi.GetValue(ps, null));
                        sum += Convert.ToDouble(pi.GetValue(ps1, null));

                        pi.SetValue(ps, sum != null ? sum : 0, null);
                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }
                }

            }
        }
        private void barButtonItemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Econ ec = new Econ();
            ec.UID = "SRWGUIHUA";
            ec = Services.BaseService.GetOneByKey<Econ>(ec);
            if (ec==null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                try
                {

                    fpSpread1.Save(ms, false);
                    ec = new Econ();
                    ec.ExcelData = ms.GetBuffer();
                    ec.UID = "SRWGUIHUA";
                    Services.BaseService.Create<Econ>(ec);
                }
                catch (Exception ex) { MsgBox.Show(ex.Message); }

            }
            else
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                try
                {
                   WaitDialogForm wait = new WaitDialogForm("", "���ڱ�������, ���Ժ�...");
                    fpSpread1.Save(ms, false);

                    ec.ExcelData = ms.GetBuffer();
                    bts = ec.ExcelData;
                    Services.BaseService.Update<Econ>(ec);
                    wait.Close();
                    MsgBox.Show("����ģ��ɹ�");
                }
                catch (Exception ex) { MsgBox.Show(ex.Message); }
            }
            
        }

        private void barButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //�ж��ļ���xls�Ƿ���ڣ��������򴴽�
            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\xls"))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\xls");
            }
            
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xls"))
            {
                if (MessageBox.Show("�Ѿ����ڴ��ļ�����ȷ��Ҫ�滻��", "ɾ��", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    WaitDialogForm wait = new WaitDialogForm("", "���ڱ�������, ���Ժ�...");
                    //fpSpread1.Save(System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xls", false);
                    fpSpread1.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                    // ����Ҫʹ�õ�Excel ����ӿ�
                    // ����Application ����,�˶����ʾ����Excel ����
                    Microsoft.Office.Interop.Excel.Application excelApp = null;
                    // ����Workbook����,�˶����������
                    Microsoft.Office.Interop.Excel.Workbook workBook;
                    // ����Worksheet ����,�˶����ʾExecel �е�һ�Ź�����
                    Microsoft.Office.Interop.Excel.Worksheet ws = null;
                    Microsoft.Office.Interop.Excel.Range range = null;
                    excelApp = new Microsoft.Office.Interop.Excel.Application();
                    string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xls";
                    workBook = excelApp.Workbooks.Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    for (int i = 1; i <= workBook.Worksheets.Count; i++)
                    {

                        ws = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[i];
                        //ȡ������������
                        ws.Unprotect(Missing.Value);
                        //�����ݵ�����
                        int row = ws.UsedRange.Rows.Count;
                        //�����ݵ�����
                        int col = ws.UsedRange.Columns.Count;
                        //����һ������
                        range = ws.get_Range(ws.Cells[1, 1], ws.Cells[row, col]);
                        //�������ڵĵ�Ԫ���Զ�����
                        range.WrapText = true;
                        //����������
                        ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                    }
                    //���湤����
                    workBook.Save();
                    //�رչ�����
                    excelApp.Workbooks.Close();
                    wait.Close();
                    MsgBox.Show("����ɹ�");
                    
                }
                else
                    return;
            }
            else
            {
                WaitDialogForm wait = null;
                try
                {
                    wait = new WaitDialogForm("", "���ڱ�������, ���Ժ�...");
                    //textBox1.Focus();
                    fpSpread1.Update();
                    //fpSpread1.Save(System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xls", true);
                    fpSpread1.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                    // ����Ҫʹ�õ�Excel ����ӿ�
                    // ����Application ����,�˶����ʾ����Excel ����
                    Microsoft.Office.Interop.Excel.Application excelApp = null;
                    // ����Workbook����,�˶����������
                    Microsoft.Office.Interop.Excel.Workbook workBook;
                    // ����Worksheet ����,�˶����ʾExecel �е�һ�Ź�����
                    Microsoft.Office.Interop.Excel.Worksheet ws = null;
                    Microsoft.Office.Interop.Excel.Range range = null;
                    excelApp = new Microsoft.Office.Interop.Excel.Application();
                    string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xls";
                    workBook = excelApp.Workbooks.Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    for (int i = 1; i <= workBook.Worksheets.Count; i++)
                    {

                        ws = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[i];
                        //ȡ������������
                        ws.Unprotect(Missing.Value);
                        //�����ݵ�����
                        int row = ws.UsedRange.Rows.Count;
                        //�����ݵ�����
                        int col = ws.UsedRange.Columns.Count;
                        //����һ������
                        range = ws.get_Range(ws.Cells[1, 1], ws.Cells[row, col]);
                        //�������ڵĵ�Ԫ���Զ�����
                        range.WrapText = true;
                        //����������
                        ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                    }
                    //���湤����
                    workBook.Save();
                    //�رչ�����
                    excelApp.Workbooks.Close();
                    wait.Close();
                    MsgBox.Show("����ɹ�");


                }
                catch (Exception ex)
                {
                    wait.Close();
                    MsgBox.Show("����ʧ��");
                }
            }
            /*string uid = "Remark='"+ProjectUID+"'";
            EconomyAnalysis obj = (EconomyAnalysis)Services.BaseService.GetObject("SelectEconomyAnalysisByvalue",uid);
            if (obj != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                WaitDialogForm wait = null;
                try
                {
                    wait = new WaitDialogForm("", "���ڱ�������, ���Ժ�...");
                    //textBox1.Focus();
                    fpSpread1.Update();
                    fpSpread1.Save(ms, false);

                    obj.Contents = ms.GetBuffer();
                    Services.BaseService.Update("UpdateEconomyAnalysisByContents", obj);
                    // excelstate = false;
                    wait.Close();
                    MsgBox.Show("����ɹ�");


                }
                catch (Exception ex)
                {
                    wait.Close();
                    MsgBox.Show("����ʧ��");
                }
            }
            else
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                WaitDialogForm wait = null;
                obj = new EconomyAnalysis();
                obj.Remark= ProjectUID;
                try
                {
                    wait = new WaitDialogForm("", "���ڱ�������, ���Ժ�...");
                    //textBox1.Focus();
                    fpSpread1.Update();
                    fpSpread1.Save(ms, false);
                    obj.CreateDate = DateTime.Now;
                    obj.Contents = ms.GetBuffer();
                    Services.BaseService.Create<EconomyAnalysis>(obj);
                    // excelstate = false;
                    wait.Close();
                    MsgBox.Show("����ɹ�");


                }
                catch (Exception ex)
                {
                    wait.Close();
                    MsgBox.Show("����ʧ��");
                }
            }*/

        }

        private void barButtonOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (treeList1.FocusedNode == null)
            //    return;


            //string uid = treeList1.FocusedNode["UID"].ToString();
            //EconomyAnalysis obj = Services.BaseService.GetOneByKey<EconomyAnalysis>(uid);




            //System.IO.MemoryStream ms = new System.IO.MemoryStream(obj.Contents);

            //FarPoint.Win.Spread.FpSpread fps=new FarPoint.Win.Spread.FpSpread();
            //fps.Open(ms);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;

                try
                {
                    fpSpread1.SaveExcel(fname);
                    //fps.SaveExcel(fname);
                    if (MsgBox.ShowYesNo("�����ɹ����Ƿ�򿪸��ĵ���") != DialogResult.Yes)
                        return;

                    System.Diagnostics.Process.Start(fname);
                }
                catch
                {
                    MsgBox.Show("�޷�����" + fname + "�����������ļ��������ļ������ļ���������λ�á�");
                    return;
                }
            }
        }

        private void barButtonrefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            refreshdata();
            fpSpread1.ActiveSheetIndex = 1;
        }
        private void refreshdata()
        {
            //����Ѿ��������� ��ȡҪ���м��ص�����
            bool flag13 = true, flag14 = true, flag17 = true;
           if (fpSpread1.Sheets[17].RowCount!=0)
           {
               Sheet_SaveData(fpSpread1.Sheets[17]);
               flag17 = false;
           }
            if (fpSpread1.Sheets[12].RowCount!=0)
            {
                Sheet_Saveph220Data(fpSpread1.Sheets[12]);
                flag13 = false;
            }
            if (fpSpread1.Sheets[13].RowCount!=0)
            {
                Sheet_Saveph110Data(fpSpread1.Sheets[13]);
                flag14 = false;
            }
            //���¶�ȡģ��
            Econ ec = new Econ();
            ec.UID = "SRWGUIHUA";
            ec = Services.BaseService.GetOneByKey<Econ>(ec);
            if (ec != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(ec.ExcelData);
                //by1 = obj.Contents;
                fpSpread1.Open(ms);

            }
            //��������
            //����ָ��
            rejjzb();
            //�����豸�����
            resbdsb();
            //�޹������������ñ�
         //   wgbchpz();
            //���������ú͵�����չʵ��
            //jjhdlfzsj();
            //�����ع��õ�ʵ��
            fnqxgydsj();
            if (beginyear!=0&&lastyear!=0)
            {
                //�ļ��Ͷ��������ո�������
                xddxrqx();
                //����󸺺�����
                yzdfhqx();
            }
            //ȫ������Ԥ�Ȿ�ؾ��÷�չָ��
            if (qqreporttiltle!=null)
            {
                jjjgfzyc();
            }
            //�����Ԥ��
            if (fqreporttiltle!=null)
            {
                xdlycb();
            }
            //��󸺺�Ԥ��
            if (zdfhreporttiltle!=null)
            {
                zdfycb();
            }
            if (dllxtitle!=null&&dlphtitle!=null)
            {
                ghdlph();
            }
            //�滮���罨����Ŀ��ϸ��
            jsxmmxb();
            //����������Ŀ�ʽ������
            sbzjxqb();
            //if (fpSpread1.Sheets[17].Rows.Count!=0)
            //{
            //    Sheet_SaveData(fpSpread1.Sheets[17]);
                dwzjhzb();
            if (!flag17)
            {
                Sheet_WirteData(fpSpread1.Sheets[17]);
            }
               
            //}
            //else
            //{
            //    dwzjhzb();
            //}
            //550kv����ƽ���
            if (dlp500title!=null)
            {
                dl500ph();
            }
            ////220kv����ƽ���
            //if (dlp220title!=null)
            //{
            //    dl220ph();
            //}
            ////110KV����ƽ���
            //if (dlp110tiltle!=null)
            //{
            //    dl110ph();
            //}
            //220
            //if (fpSpread1.Sheets[13].Rows.Count != 0)
            //{
            //    Sheet_SavephData(fpSpread1.Sheets[13]);
                dl220ph();
            if (!flag13)
            {
                Sheet_Wirteph220Data(fpSpread1.Sheets[12]);
            }
              
            //}
            //else
            //{
            //    dl220ph();
            //}
            //110
            //if (fpSpread1.Sheets[14].Rows.Count != 0)
            //{
            //    Sheet_SavephData(fpSpread1.Sheets[14]);
                dl110ph();
            if (!flag14)
            {
                Sheet_Wirteph110Data(fpSpread1.Sheets[13]);
            }
               //�޹�ƽ��
            dlwgph();
            //}
            //else
            //{
            //    dl110ph();
            //}
        }
        //���ư�ť����ʾ���
        private void fpSpread1_TabIndexChanged(object sender, EventArgs e)
        {
           
        }
        int beginyear = 0; int lastyear = 0;  //��Ϊ�������յ�����
        private void fpSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            switch (e.SheetTabIndex)
            {

                case 5:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //MessageBox.Show("��ѡ�����");

                    break;
                case 6:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //MessageBox.Show("��ѡ�����");

                    break;
                case 7:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                   // this.label1.Visible = true;
                    //this.lookUpEdit1.Visible = true;
                    Ps_forecast_list report = new Ps_forecast_list();
                    report.UserID = ProjectUID;  //SetCfgValue("lastLoginUserNumber", Application.ExecutablePath + ".config");
                    report.Col1 = "1";
                    IList listReports = Common.Services.BaseService.GetList("SelectPs_forecast_listByCOL1AndUserID", report);
                    this.repositoryItemLookUpEdit1.Properties.DataSource = listReports;              
                     this.repositoryItemLookUpEdit1.DisplayMember = "Title";
                    //this.lookUpEdit1.Properties.DataSource = listReports;
                   //string id= lookUpEdit1.Properties.GetKeyValueByDisplayText(lookUpEdit1.Text).ToString();
                    //foreach (Ps_forecast_list ps in listReports)
                    //{
                    //   this.repositoryItemComboBox4.Items.Add(ps.Title);

                    //}
                    
                    break;
                case 8:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.repositoryItemComboBox4.Items.Clear();
                    report = new Ps_forecast_list();
                    report.UserID = ProjectUID;  //SetCfgValue("lastLoginUserNumber", Application.ExecutablePath + ".config");
                    report.Col1 = "1";
                    listReports = Common.Services.BaseService.GetList("SelectPs_forecast_listByCOL1AndUserID", report);
                    this.repositoryItemLookUpEdit1.Properties.DataSource = listReports;
                    this.repositoryItemLookUpEdit1.DisplayMember = "Title";
                   // this.repositoryItemComboBox4.Items.Clear();
                    //this.lookUpEdit1.Properties.DataSource = listReports;
                    //string id= lookUpEdit1.Properties.GetKeyValueByDisplayText(lookUpEdit1.Text).ToString();
                    //foreach (Ps_forecast_list ps in listReports)
                    //{
                    //    this.repositoryItemComboBox4.Items.Add(ps.Title);

                    //}
                    break;
                case 9:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.repositoryItemComboBox4.Items.Clear();
                    report = new Ps_forecast_list();
                    report.UserID = ProjectUID;  //SetCfgValue("lastLoginUserNumber", Application.ExecutablePath + ".config");
                    report.Col1 = "1";
                    listReports = Common.Services.BaseService.GetList("SelectPs_forecast_listByCOL1AndUserID", report);
                    this.repositoryItemLookUpEdit1.Properties.DataSource = listReports;
                    this.repositoryItemLookUpEdit1.DisplayMember = "Title";
                    // this.repositoryItemComboBox4.Items.Clear();
                    //this.lookUpEdit1.Properties.DataSource = listReports;
                    //string id= lookUpEdit1.Properties.GetKeyValueByDisplayText(lookUpEdit1.Text).ToString();
                    //foreach (Ps_forecast_list ps in listReports)
                    //{
                    //    this.repositoryItemComboBox4.Items.Add(ps.Title);

                    //}
                    break;
                case 10:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.repositoryItemComboBox5.Items.Clear();
                    string conn = "ProjectID='" + ProjectUID + "' order by Sort";
                    IList<PS_Table_AreaWH>  list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);

                    foreach (PS_Table_AreaWH area in list)
                    {
                        this.repositoryItemComboBox5.Items.Add(area.Title);
                    }
                    break;
                case 11:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.repositoryItemComboBox5.Items.Clear();
                    conn = "ProjectID='" + ProjectUID + "' order by Sort";
                    list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);

                    foreach (PS_Table_AreaWH area in list)
                    {
                        this.repositoryItemComboBox5.Items.Add(area.Title);
                    }
                    break;
                case 12:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.repositoryItemComboBox5.Items.Clear();
                    conn = "ProjectID='" + ProjectUID + "' order by Sort";
                    list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);

                    foreach (PS_Table_AreaWH area in list)
                    {
                        this.repositoryItemComboBox5.Items.Add(area.Title);
                    }
                    break;
                case 13:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.repositoryItemComboBox5.Items.Clear();
                    conn = "ProjectID='" + ProjectUID + "' order by Sort";
                    list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);

                    foreach (PS_Table_AreaWH area in list)
                    {
                        this.repositoryItemComboBox5.Items.Add(area.Title);
                    }
                    break;
                case 14:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barEditItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.repositoryItemComboBox5.Items.Clear();
                    conn = "ProjectID='" + ProjectUID + "' order by Sort";
                    list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);

                    foreach (PS_Table_AreaWH area in list)
                    {
                        this.repositoryItemComboBox5.Items.Add(area.Title);
                    }
                    break;
                default:
                    this.barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barEditItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                   this.barEditItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                   this.barEditItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                   this.barEditItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                   this.repositoryItemComboBox5.Items.Clear();
                   conn = "ProjectID='" + ProjectUID + "' order by Sort";
                   list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);

                   foreach (PS_Table_AreaWH area in list)
                   {
                       this.repositoryItemComboBox5.Items.Add(area.Title);
                   }
                    break;
            }
        }

        private void barEditItem2_EditValueChanged(object sender, EventArgs e)
        {
            beginyear=Convert.ToInt32(barEditItem1.EditValue);
            if (beginyear==0)
            {
                MessageBox.Show("��ѡ����ʼ�꣡");
                return;
            }
            else
            {
                lastyear = Convert.ToInt32(barEditItem2.EditValue);
                //�������ݸ��� ��������󸺺�
                xddxrqx();
                //����󸺺�
                yzdfhqx();
            }
        }
        Ps_forecast_list qqreporttiltle = null, fqreporttiltle = null, zdfhreporttiltle = null;
        private void barEditItem3_EditValueChanged(object sender, EventArgs e)
        {

            if (fpSpread1.ActiveSheetIndex==7)  //���ýṹ����Ԥ��
            {
                qqreporttiltle =(Ps_forecast_list) barEditItem3.EditValue;
              
                    jjjgfzyc();
               
            }
            else if (fpSpread1.ActiveSheetIndex == 8)
            {
                fqreporttiltle =(Ps_forecast_list) barEditItem3.EditValue;
                xdlycb();
            }
            else if (fpSpread1.ActiveSheetIndex==9)
            {
                zdfhreporttiltle =(Ps_forecast_list) barEditItem3.EditValue;
                zdfycb();
            }
        }
        string dlphtitle = null, dlp500title = null, dlp220title = null, dlp110tiltle = null,dlpwgtitle=null,dllxtitle=null;
        private void barEditItem4_EditValueChanged(object sender, EventArgs e)
        {
            if (fpSpread1.ActiveSheetIndex == 10)  //���ýṹ����Ԥ��
            {
                //if (dllxtitle==null)
                //{
                //    MessageBox.Show("��ѡ��������ͣ�");
                //    return;
                //}
                //else
                //{
                    dlphtitle = barEditItem4.EditValue.ToString();
                    if (dllxtitle!=null)
                    {
                        ghdlph();
                    }
                //}
               

            }
            else if (fpSpread1.ActiveSheetIndex == 11)
            {
                dlp500title = barEditItem4.EditValue.ToString();
                if (dlp500title!=null)
                {
                    dl500ph();
                }
            }
            else if (fpSpread1.ActiveSheetIndex == 12)
            {
                dlp220title = barEditItem4.EditValue.ToString();
                if (dlp220title!=null)
                {
                    dl220ph();
                }
            }
            else if (fpSpread1.ActiveSheetIndex == 13)
            {
                dlp110tiltle = barEditItem4.EditValue.ToString();
                if (dlp110tiltle!=null)
                {
                    dl110ph();
                }
            }
            else if (fpSpread1.ActiveSheetIndex==14)
            {
                dlpwgtitle = barEditItem4.EditValue.ToString();
                if (dlpwgtitle!=null)
                {
                    dlwgph();
                }
            }

        }

        private void barEditItem5_EditValueChanged(object sender, EventArgs e)
        {
            dllxtitle = barEditItem5.EditValue.ToString();
        }


    }
}