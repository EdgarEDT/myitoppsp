using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using System.Collections;
using Itop.Common;
using System.Reflection;

namespace Itop.Client.Stutistics
{
    public partial class FrmSubstation_As : Itop.Client.Base.FormBase
    {
        int year = DateTime.Now.Year;
        double glys = 0.95;
        public FrmSubstation_As()
        {
            InitializeComponent();
        }

        private void FrmSubstation_As_Load(object sender, EventArgs e)
        {
            this.ctrlPs_Substation_As1.GLYS = glys;
                this.ctrlPs_Substation_As1.GridView.Columns["y" + year].Visible = true;
                this.ctrlPs_Substation_As1.GridView.Columns["y" + year].Caption = year + "年变电容量";
                this.ctrlPs_Substation_As1.GridView.Columns["y" + year].VisibleIndex = 8;
                gridColumn4.FieldName = "y" + year;

                gridBand11.Caption = (year + 1) + "年";
                gridBand12.Caption = (year + 2) + "年";
                gridBand13.Caption = (year + 3) + "年";
                gridBand14.Caption = (year + 4) + "年";
                gridBand15.Caption = (year + 5) + "年";
                gridBand16.Caption = (year + 1) + "年";
                gridBand17.Caption = (year + 2) + "年";
                gridBand18.Caption = (year + 3) + "年";
                gridBand19.Caption = (year + 4) + "年";
                gridBand20.Caption = (year + 5) + "年";
                gridColumn9.FieldName = "m" + (year + 1);
                gridColumn10.FieldName = "m" + (year + 2);
                gridColumn11.FieldName = "m" + (year + 3);
                gridColumn12.FieldName = "m" + (year + 4);
                gridColumn13.FieldName = "m" + (year + 5);
                gridColumn14.FieldName = "n" + (year + 1);
                gridColumn15.FieldName = "n" + (year + 2);
                gridColumn16.FieldName = "n" + (year + 3);
                gridColumn17.FieldName = "n" + (year + 4);
                gridColumn18.FieldName = "n" + (year + 5);


            this.ctrlPs_Substation_As1.RefreshData();
            this.ctrlPs_Substation_As1.repositoryItemHyperLinkEdit1.Click += new EventHandler(repositoryItemHyperLinkEdit1_Click);
            InitData();

        }

        void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            InitData();

            
        }

        private void InitData() 
        {
            Ps_Power ps = this.ctrlPs_Substation_As1.FocusedObject;
            if (ps == null)
                return;
            ps.Col4 = glys.ToString("n2");
            gridColumn4.FieldName = "y" + year;

            double zdfh = 0;
            object obj = ps.GetType().GetProperty("y" + year).GetValue(ps, null);
            if (obj != null)
            {
                zdfh = Convert.ToDouble(obj);
            }
            double xgl = 0;
            try
            {
                xgl = Convert.ToDouble(ps.Col3);
            }catch{}
            int xhl = 0;
            try
            {
                xhl = Convert.ToInt32(ps.Col2);
            }
            catch { }

            ps.Col9 = (xgl*(xhl-1) - zdfh / glys).ToString("n2");


            if (xgl == 0 || xhl == 0)
                ps.Col8 = "0";
            else
                ps.Col8 = (zdfh / (xgl * xhl * glys)).ToString("n2");


            object obj1 = ps.GetType().GetProperty("y" + (year + 1)).GetValue(ps, null);
            object obj2 = ps.GetType().GetProperty("y" + (year + 2)).GetValue(ps, null);
            object obj3 = ps.GetType().GetProperty("y" + (year + 3)).GetValue(ps, null);
            object obj4 = ps.GetType().GetProperty("y" + (year + 4)).GetValue(ps, null);
            object obj5 = ps.GetType().GetProperty("y" + (year + 5)).GetValue(ps, null);

            double ob1 = 0;
            double ob2 = 0;
            double ob3 = 0;
            double ob4 = 0;
            double ob5 = 0;

            if (obj1 != null)
            {
                ob1 = Convert.ToDouble(obj1);
            }
            if (obj2 != null)
            {
                ob2 = Convert.ToDouble(obj2);
            }
            if (obj3 != null)
            {
                ob3 = Convert.ToDouble(obj3);
            }
            if (obj4 != null)
            {
                ob4 = Convert.ToDouble(obj4);
            }
            if (obj5 != null)
            {
                ob5 = Convert.ToDouble(obj5);
            }


            PropertyInfo pi1 = ps.GetType().GetProperty("m" + (year + 1));
            PropertyInfo pi11 = ps.GetType().GetProperty("n" + (year + 1));
            PropertyInfo pi2 = ps.GetType().GetProperty("m" + (year + 2));
            PropertyInfo pi22 = ps.GetType().GetProperty("n" + (year + 2));
            PropertyInfo pi3 = ps.GetType().GetProperty("m" + (year + 3));
            PropertyInfo pi33 = ps.GetType().GetProperty("n" + (year + 3));
            PropertyInfo pi4 = ps.GetType().GetProperty("m" + (year + 4));
            PropertyInfo pi44 = ps.GetType().GetProperty("n" + (year + 4));
            PropertyInfo pi5 = ps.GetType().GetProperty("m" + (year + 5));
            PropertyInfo pi55 = ps.GetType().GetProperty("n" + (year + 5));

            pi11.SetValue(ps, xgl * (xhl - 1) - ob1 / glys, null);
            pi22.SetValue(ps, xgl * (xhl - 1) - ob2 / glys, null);
            pi33.SetValue(ps, xgl * (xhl - 1) - ob3 / glys, null);
            pi44.SetValue(ps, xgl * (xhl - 1) - ob4 / glys, null);
            pi55.SetValue(ps, xgl * (xhl - 1) - ob5 / glys, null);

            if (xgl == 0 || xhl == 0)
            {
                pi1.SetValue(ps, 0, null);
                pi2.SetValue(ps, 0, null);
                pi3.SetValue(ps, 0, null);
                pi4.SetValue(ps, 0, null);
                pi5.SetValue(ps, 0, null);

            }
            else
            {
                pi1.SetValue(ps, ob1 / (xgl * xhl * glys), null);
                pi2.SetValue(ps, ob2 / (xgl * xhl * glys), null);
                pi3.SetValue(ps, ob3 / (xgl * xhl * glys), null);
                pi4.SetValue(ps, ob4 / (xgl * xhl * glys), null);
                pi5.SetValue(ps, ob5 / (xgl * xhl * glys), null);


            }





            IList<Ps_Power> li = new List<Ps_Power>();
            Ps_Power ps1 = new Ps_Power();
            Ps_Power ps2 = new Ps_Power();
            Ps_Power ps3 = new Ps_Power();
            DataConverter.CopyTo<Ps_Power>(ps, ps1);
            DataConverter.CopyTo<Ps_Power>(ps, ps2);
            DataConverter.CopyTo<Ps_Power>(ps, ps3);
            switch (xhl)
            {
                case 2:

                    li.Add(ps);
                    li.Add(ps1);
                    break;
                case 3:
                    li.Add(ps);
                    li.Add(ps1);
                    li.Add(ps2);

                    break;
                case 4:
                    li.Add(ps);
                    li.Add(ps1);
                    li.Add(ps2);
                    li.Add(ps3);
                    break;
                default:
                    li.Add(ps);
                    li.Add(ps1);
                    break;


            }



            this.gridControl1.DataSource = li;
        
        }



        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            JS(6);
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            JS(1);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            JS(2);
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            JS(3);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            JS(4);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            JS(5);
        }

        private void JS(int methodName)
        {
            int fyear = year - 4;
            int syear = year; 
            int eyear = year + 5;

            DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)ctrlPs_Substation_As1.ObjectList, typeof(Ps_Power));

            foreach (Ps_Power ps in this.ctrlPs_Substation_As1.ObjectList)
            {
                ps.Col7 = "5";
                double rl = ps.RL;
                int ts = ps.TS;
                double sss = 0;
                switch (ts)
                {
                    case 2:
                        sss = 0.65;
                        break;
                    case 3:
                        sss = 0.65;
                        break;
                    case 4:
                        sss = 0.87;
                        break;
                    default:
                        sss = 1;
                        break;
                }

 

                DataRow dataRow = Itop.Common.DataConverter.ObjectToRow(ps, dataTable.NewRow());
                double[] historyValues = new double[5];
                for (int m = 0; m < 5; m++)
                {
                    try
                    {
                        historyValues[m] = (double)dataRow["y" + (m + fyear)];
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }

                double[] yn = new double[eyear - syear];
                double value1 = 0;
                try { value1 = (double)dataRow["y" + syear]; }
                catch { }
                bool bl1 = true;
                switch (methodName)
                {
                    case 1:
                        yn = Calculator.One(historyValues, eyear - syear);
                        
                        for (int i = 1; i <= eyear - syear; i++)
                        {
                            ps.GetType().GetProperty("y" + (syear + i)).SetValue(ps,yn[i - 1], null);
                            double s1 = rl * sss - yn[i - 1]/glys;
                            ps.GetType().GetProperty("s" + (syear + i)).SetValue(ps, s1, null);
                            if (s1 < 0 && bl1)
                            {
                                ps.Col7 = i.ToString("n0");
                                bl1 = false;
                            }
                        }
                        break;
                    case 2:
                        yn = Calculator.Second(historyValues, eyear - syear);
                        for (int i = 1; i <= eyear - syear; i++)
                        {
                            ps.GetType().GetProperty("y" + (syear + i)).SetValue(ps, yn[i - 1], null);
                            double s1 = rl * sss - yn[i - 1] / glys;
                            ps.GetType().GetProperty("s" + (syear + i)).SetValue(ps, s1, null);
                            if (s1 < 0 && bl1)
                            {
                                ps.Col7 = i.ToString("n0");
                                bl1 = false;
                            }
                        }
                        break;
                    case 3:
                        yn = Calculator.Three(historyValues, eyear - syear);
                        for (int i = 1; i <= eyear - syear; i++)
                        {
                            ps.GetType().GetProperty("y" + (syear + i)).SetValue(ps, yn[i - 1], null);
                            double s1 = rl * sss - yn[i - 1] / glys;
                            ps.GetType().GetProperty("s" + (syear + i)).SetValue(ps, s1, null);
                            if (s1 < 0 && bl1)
                            {
                                ps.Col7 = i.ToString("n0");
                                bl1 = false;
                            }
                        }
                        break;
                    case 4:
                        yn = Calculator.Index(historyValues, eyear - syear);
                        for (int i = 1; i <= eyear - syear; i++)
                        {
                            ps.GetType().GetProperty("y" + (syear + i)).SetValue(ps, yn[i - 1], null);
                            double s1 = rl * sss - yn[i - 1] / glys;
                            ps.GetType().GetProperty("s" + (syear + i)).SetValue(ps, s1, null);
                            if (s1 < 0 && bl1)
                            {
                                ps.Col7 = i.ToString("n0");
                                bl1 = false;
                            }
                        }
                        break;
                    case 5:
                        yn = Calculator.LOG(historyValues, eyear - syear);
                        for (int i = 1; i <= eyear - syear; i++)
                        {
                            ps.GetType().GetProperty("y" + (syear + i)).SetValue(ps, yn[i - 1], null);
                            double s1 = rl * sss - yn[i - 1] / glys;
                            ps.GetType().GetProperty("s" + (syear + i)).SetValue(ps, s1, null);
                            if (s1 < 0 && bl1)
                            {
                                ps.Col7 = i.ToString("n0");
                                bl1 = false;
                            }
                        }
                        break;
                    case 6:
                        double zzl = Calculator.AverageIncreasing(historyValues);
                        if (fyear != 0 && syear != 0)
                        {
                            for (int i = 1; i <= eyear - syear; i++)
                            {
                                ps.GetType().GetProperty("y" + (syear + i)).SetValue(ps, value1 * Math.Pow(1 + zzl, i), null);
                                double s1 = rl * sss - value1 * Math.Pow(1 + zzl, i) / glys;;
                                ps.GetType().GetProperty("s" + (syear + i)).SetValue(ps, s1, null);
                                if (s1 < 0 && bl1)
                                {
                                    ps.Col7 = i.ToString("n0");
                                    bl1 = false;
                                }
                            }
                        }
                        break;
                }
                int a = 0;
                a++;
            
            }

            this.ctrlPs_Substation_As1.GridControl.RefreshDataSource();
            InitData();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPs_Substation_As1.AddObject();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPs_Substation_As1.UpdateObject();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPs_Substation_As1.DeleteObject();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPs_Substation_As1.PrintPreview();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("A", typeof(string));
            dt.Columns.Add("B", typeof(bool));

            Hashtable hs = new Hashtable();

            foreach (Ps_Power ps in this.ctrlPs_Substation_As1.ObjectList)
            {
                if (!hs.ContainsKey(ps.FQ))
                {
                    hs.Add(ps.FQ, "");
                    DataRow row = dt.NewRow();
                    row["A"] = ps.FQ;
                    row["B"] = false;
                    dt.Rows.Add(row);
                }
            
            }

            FrmFQ frm = new FrmFQ();
            frm.DT = dt;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.ctrlPs_Substation_As1.GridView.Columns["FQ"].FilterInfo =new DevExpress.XtraGrid.Columns.ColumnFilterInfo(frm.FQ);

            }

        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFX ff = new FrmFX();
            Ps_Power ps = this.ctrlPs_Substation_As1.FocusedObject;
            if (ps == null)
                return;

            double bfzl = 0;
            try { bfzl = Convert.ToDouble(ps.Col5); }
            catch { }

            double xfzl = 0;
            try { xfzl = Convert.ToDouble(ps.Col8); }
            catch { }

            if (bfzl >= 1)
                ff.S1 = "不满足";
            else
                ff.S1 = "满足";
            ff.S3 = ps.Col7 + "年内";

            if (xfzl >= 1)
                ff.S2 = "不满足";
            else
                ff.S2 = "满足";


            object obj1 = ps.GetType().GetProperty("n" + (year + 1)).GetValue(ps, null);
            object obj2 = ps.GetType().GetProperty("n" + (year + 2)).GetValue(ps, null);
            object obj3 = ps.GetType().GetProperty("n" + (year + 3)).GetValue(ps, null);
            object obj4 = ps.GetType().GetProperty("n" + (year + 4)).GetValue(ps, null);
            object obj5 = ps.GetType().GetProperty("n" + (year + 5)).GetValue(ps, null);

            double ob1 = 0;
            double ob2 = 0;
            double ob3 = 0;
            double ob4 = 0;
            double ob5 = 0;

            if (obj1 != null)
            {
                ob1 = Convert.ToDouble(obj1);
            }
            if (obj2 != null)
            {
                ob2 = Convert.ToDouble(obj2);
            }
            if (obj3 != null)
            {
                ob3 = Convert.ToDouble(obj3);
            }
            if (obj4 != null)
            {
                ob4 = Convert.ToDouble(obj4);
            }
            if (obj5 != null)
            {
                ob5 = Convert.ToDouble(obj5);
            }
            ff.S4 = "5年内";
            if(ob5<0)
                ff.S4 = "5年内";
            if (ob4 < 0)
                ff.S4 = "4年内";
            if (ob3 < 0)
                ff.S4 = "3年内";
            if (ob2 < 0)
                ff.S4 = "2年内";
            if (ob1 < 0)
                ff.S4 = "1年内";

            ff.ShowDialog();

        }
    }
}