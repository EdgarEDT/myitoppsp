using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Forecast;
using System.Collections;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.Forecast
{
    public partial class FormForecastCalc9Third : FormBase
    {
        public FormForecastCalc9Third()
        {
            InitializeComponent();
        }
        int firstyear = 0;
        int endyear = 0;

        IList<Ps_Calc> list1 = new List<Ps_Calc>();
        Ps_Calc pc1 = new Ps_Calc();
        private bool isedit = false;
        int type = 9;
        DataTable dt = new DataTable ();
       
        DataRow newrow2 = null;

        public bool ISEdit
        {

            set { isedit = value; }
        }
        Ps_forecast_list forecastReport;
        public Ps_forecast_list PForecastReports
        {
            get { return forecastReport; }
            set { forecastReport = value; }
        }

        DataTable dataTable;
        public DataTable DTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }
        public DataTable Returndt
        {
            get { return dt; }
            set { dt = value; }
        }
        Hashtable ha = new Hashtable();
        public Hashtable Ha
        {
            get { return ha; }
            set { ha = value; }
        }
        ArrayList algotemlist = new ArrayList();
        public ArrayList Algotemlist
        {
            get { return algotemlist; }
            set { algotemlist = value; }
        }
        private void HideToolBarButton()
        {

            if (!isedit)
            {
                //vGridControl2.Enabled = false;
                //simpleButton1.Visible = false;
            }

        }

        private void FormForecastCalc9Third_Load(object sender, EventArgs e)
        {
            dt.Columns.Clear();
            comboBox1.Items.Add("�������ʷ�");
            comboBox1.Items.Add("����ϵ����");
            comboBox1.Items.Add("��ط�");
            comboBox1.Items.Add("��ɫģ�ͷ�");
            comboBox1.Items.Add("���Ʒ�");
            comboBox1.Items.Add("ָ��ƽ����");
            comboBox1.Items.Add("ר�Ҿ��߷�");

            comboBox6.Items.Add("���Ʒ�(�������ʷ�)");
            comboBox6.Items.Add("���Ʒ�(ֱ��)");
            comboBox6.Items.Add("���Ʒ�(������)");
            comboBox6.Items.Add("���Ʒ�(����)");
            comboBox6.Items.Add("���Ʒ�(ָ��)");
            comboBox6.Items.Add("���Ʒ�(��������)");
           

           
            dt.Columns.Add("A", typeof(string));
            dt.Columns.Add("B", typeof(string));
            dt.Columns.Add("C", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            DataRow dr;
            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {

                comboBox2.Items.Add(i + "��");
                comboBox3.Items.Add(i + "��");
                comboBox4.Items.Add(i + "��");
                comboBox5.Items.Add(i + "��");
               
            }
            foreach (Ps_Calc pcs2 in list1)
            {
                dr = dt.NewRow();
                dr["id"] = pcs2.ID;
                dr["A"] = pcs2.CalcID;
                if (pcs2.Value5 != 0 && pcs2.Value4!=0)
                {
                    dr["B"] = pcs2.Value4 + "��-" + pcs2.Value5 + "��";
                }
               
                else
                {
                    continue;
                }
                if (pcs2.Value2 != 0 && pcs2.Value3 != 0)
                {
                    dr["C"] = pcs2.Value2 + "��-" + pcs2.Value3 + "��";
                }
                else
                {
                    continue;
                }

            dt.Rows.Add(dr);
            }
            //dt=
            //    GetSortTable(ref dt, "C", true);
            gridControl1.DataSource = dt;


           
        }
        public System.Data.DataTable GetSortTable(System.Data.DataTable dt, string Column, bool bl)
        {
            string sort = " asc";
            if (!bl)
                sort = " desc";

            DataView dv = dt.DefaultView;
            dv.Sort = Column + sort;
            System.Data.DataTable dt2 = dv.ToTable();
            return dt2;
        }


        private void savevalue(Ps_Calc pc11,bool iscreat)
        {
           if(!iscreat)
           {
          
                    Services.BaseService.Update<Ps_Calc>(pc11);
            
            }
           else
            {
                //Ps_Calc pcs = new Ps_Calc();
                //pcs.ID = Guid.NewGuid().ToString();
                //pcs.Forecast = type;
                //pcs.ForecastID = forecastReport.ID;
                //pcs.CalcID = title;



                Services.BaseService.Create<Ps_Calc>(pc11);

            }
        }

       
        /// <summary>
        /// ѡ����ʷ��ݿ�ʼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ifirst =0;
            int isec =0;
            if (comboBox2.SelectedItem==null)
            {
                return;
            }
            if (comboBox2.SelectedItem.ToString() != "")
            {
                ifirst = Convert.ToInt32(comboBox2.SelectedItem.ToString().Replace("��", ""));
            }
            if (comboBox3.SelectedItem!=null&&comboBox3.SelectedItem.ToString() != "")
            {
                isec = Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("��", ""));
            }
            if (isec!=0)
            {
                if (ifirst>isec)
                {
                   comboBox2.SelectedIndex=-1;
                    return;
                }
            }
            isec = 0;

            if (comboBox4.SelectedItem != null && comboBox4.SelectedItem.ToString() != "")
            {
                isec = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("��", ""));
            }
            if (isec != 0)
            {
                if (ifirst >= isec)
                {
                    comboBox2.SelectedIndex = -1;
                    return;
                }
            }
            isec=0;

            if (comboBox5.SelectedItem !=null&& comboBox5.SelectedItem.ToString() != "")
            {
                isec = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("��", ""));
            }
             if (isec!=0)
            {
                if (ifirst>=isec)
                {
                    comboBox2.SelectedIndex = -1;
                    return;
                }
            }
           
        }
        /// <summary>
        /// ѡ����ʷ��ݽ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ifirst = 0;
            int isec = 0;
            if (comboBox3.SelectedItem == null)
            {
                return;
            }
            if (comboBox3.SelectedItem.ToString() != "")
            {
                ifirst = Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("��", ""));
            }
            if (comboBox2.SelectedItem != null && comboBox2.SelectedItem.ToString() != "")
            {
                isec = Convert.ToInt32(comboBox2.SelectedItem.ToString().Replace("��", ""));
            }
            else
            {
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf(forecastReport.StartYear + "��");
            }
            if (isec != 0)
            {
                if (isec > ifirst)
                {
                    comboBox3.SelectedIndex = -1;
                    return;
                }
            }
            isec = 0;


                int strtemp=comboBox4.SelectedIndex;
           
                comboBox4.SelectedIndex = comboBox4.Items.IndexOf((ifirst + 1) + "��");
           
            if (isec != 0)
            {
                if ( ifirst>=isec )
                {
                    comboBox3.SelectedIndex = -1;
                    comboBox4.SelectedIndex = strtemp;
                    return;
                }
            }
            isec = 0;
            if (comboBox5.SelectedItem != null && comboBox5.SelectedItem.ToString() != "")
            {
                isec = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("��", ""));
            }

            if (isec != 0)
            {
                if (ifirst >= isec)
                {
                    comboBox3.SelectedIndex = -1;
                    return;
                }
            }
        }
        /// <summary>
        /// ѡ��Ԥ����ݿ�ʼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ifirst = 0;
            int isec = 0;
            if (comboBox4.SelectedItem == null)
            {
                return;
            }
            if (comboBox4.SelectedItem.ToString() != "")
            {
                ifirst = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("��", ""));
            }
            if (comboBox2.SelectedItem != null && comboBox2.SelectedItem.ToString() != "")
            {
                isec = Convert.ToInt32(comboBox2.SelectedItem.ToString().Replace("��", ""));
            }
            else
            {
                comboBox2.SelectedIndex = comboBox2.Items.IndexOf(forecastReport.StartYear + "��");
            }
            if (isec != 0)
            {
                if (isec >= ifirst)
                {
                    comboBox4.SelectedIndex = -1;
                    return;
                }
            }
            isec = 0;
            //if (comboBox3.SelectedItem != null && comboBox3.SelectedItem.ToString() != "")
            //{
            //    isec = Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("��", ""));
            //}
            //else
            //{
            int strtemp = comboBox3.SelectedIndex;
                comboBox3.SelectedIndex = comboBox3.Items.IndexOf((ifirst - 1) + "��");
                isec = ifirst - 1;
            //}
            if (isec != 0)
            {
                if (isec >= ifirst)
                {
                    comboBox4.SelectedIndex = -1;
                    comboBox3.SelectedIndex = strtemp;
                    return;
                }

            }
            isec = 0;

            if (comboBox5.SelectedItem != null && comboBox5.SelectedItem.ToString() != ""&&!spinEdit1.Visible)
            {
                isec = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("��", ""));
            }
            else
            {

                isec = ifirst + Convert.ToInt32(spinEdit1.Value);
                if (isec > forecastReport.EndYear - 1)
                {

                    isec = forecastReport.EndYear - 1;
                }
                comboBox5.SelectedIndex = comboBox3.Items.IndexOf((isec) + "��");
            }
            if (isec != 0)
            {
                if (ifirst > isec)
                {
                    comboBox4.SelectedIndex = -1;
                    return;
                }
            }
        }
        /// <summary>
        /// ѡ��Ԥ����ݽ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ifirst = 0;
            int isec = 0;
            if (comboBox5.SelectedItem == null)
            {
                return;
            }
            if (comboBox5.SelectedItem.ToString() != "")
            {
                ifirst = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("��", ""));
            }
            if (comboBox2.SelectedItem != null && comboBox2.SelectedItem.ToString() != "")
            {
                isec = Convert.ToInt32(comboBox2.SelectedItem.ToString().Replace("��", ""));
            }
            if (isec != 0)
            {
                if (isec >= ifirst)
                {
                    comboBox5.SelectedIndex = -1;
                    return;
                }
            }
            isec = 0;
            if (comboBox3.SelectedItem != null && comboBox3.SelectedItem.ToString() != "")
            {
                isec = Convert.ToInt32(comboBox3.SelectedItem.ToString().Replace("��", ""));
            }
            if (isec != 0)
            {
                if (isec >= ifirst)
                {
                    comboBox5.SelectedIndex = -1;
                    return;
                }
            }
            isec = 0;
            if (comboBox4.SelectedItem != null && comboBox4.SelectedItem.ToString() != "")
            {
                isec = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("��", ""));
                spinEdit1.Value = (decimal)(ifirst - isec);
            }
            if (isec != 0)
            {
                if (isec > ifirst)
                {
                    comboBox5.SelectedIndex = -1;
                    return;
                }
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            
           
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null || comboBox4.SelectedItem == null || comboBox5.SelectedItem == null || (comboBox6.SelectedItem == null && comboBox6.Visible))
            {
                MessageBox.Show("�������ò���ȷ��");
                return;
            }
            int yearselect1 = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("��", ""));
            int yearselect2 = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("��", ""));
            foreach (DataRow ndr in dt.Rows)
            {
                int year1 = Convert.ToInt32(ndr["C"].ToString().Split('-')[0].Replace("��", ""));
                int year2 = Convert.ToInt32(ndr["C"].ToString().Split('-')[1].Replace("��", ""));
                if (!(yearselect1 > year2 || year1 > yearselect2))
                {
                    MessageBox.Show("��ѡ��ݰ�����" + ndr["C"]);
                    return;
                }
            }
            DataRow dr;
            dr = dt.NewRow();
            if (comboBox6.Visible)
                dr["A"] = comboBox6.SelectedItem;
            else
                dr["A"] = comboBox1.SelectedItem;
            Ps_Calc pcs = new Ps_Calc();
            pcs.ID = Guid.NewGuid().ToString();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            pcs.CalcID = dr["A"].ToString();
            pcs.Value2 = Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("��", ""));
            pcs.Value3 = Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("��", ""));
            pcs.Value4 = Convert.ToDouble(comboBox2.SelectedItem.ToString().Replace("��", ""));
            pcs.Value5 = Convert.ToDouble(comboBox3.SelectedItem.ToString().Replace("��", ""));
            dr["B"] = comboBox2.SelectedItem + "-" + comboBox3.SelectedItem;

            dr["C"] = comboBox4.SelectedItem + "-" + comboBox5.SelectedItem;
            dr["ID"] = pcs.ID;

            dt.Rows.Add(dr);
            
           
            savevalue(pcs, true);
           
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle<0)
            {
                return;
            }
            int i = gridView1.FocusedRowHandle;
            //gridView1.FocusedRowHandle = -1;
            DataRowView drv = gridView1.GetRow(i) as DataRowView;
            if (drv==null)
            {
                MessageBox.Show("ɾ��ʧ�ܣ�");
                gridView1.RefreshData();
                return;
            }
            Ps_Calc pcs = new Ps_Calc();
            pcs.ID = drv.Row["ID"].ToString();
            pcs=Services.BaseService.GetOneByKey<Ps_Calc>(pcs);
            if(pcs!=null)
            Services.BaseService.Delete<Ps_Calc>(pcs);
            else
            {
                MessageBox.Show("ɾ��ʧ��!");
                gridView1.RefreshData();
                return;
            }
            dt.Rows.Remove(drv.Row);
            
           gridView1.RefreshData();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem==null)
            {
                return;
            }
            if (comboBox1.SelectedItem.ToString() == "���Ʒ�")
            {
                if (!comboBox6.Items.Contains("���Ʒ�(�������ʷ�)"))
                {
                    comboBox6.Items.Clear();
                    comboBox6.Items.Add("���Ʒ�(�������ʷ�)");
                    comboBox6.Items.Add("���Ʒ�(ֱ��)");
                    comboBox6.Items.Add("���Ʒ�(������)");
                    comboBox6.Items.Add("���Ʒ�(����)");
                    comboBox6.Items.Add("���Ʒ�(ָ��)");
                    comboBox6.Items.Add("���Ʒ�(��������)");
                   
                }

                comboBox6.Visible = true;
                comboBox6.Enabled = true;
                label6.Visible = true;
                label6.Enabled = true;
                


            }
            else if (comboBox1.SelectedItem.ToString() == "��ط�")
            {
                if (!comboBox6.Items.Contains("��ط�(ֱ��)"))
                {
                    comboBox6.Items.Clear();
                    comboBox6.Items.Add("��ط�(ֱ��)");
                    comboBox6.Items.Add("��ط�(������)");
                    comboBox6.Items.Add("��ط�(����)");
                    comboBox6.Items.Add("��ط�(ָ��)");
                    comboBox6.Items.Add("��ط�(��������)");
                    
                }

                comboBox6.Visible = true;
                comboBox6.Enabled = true;
                label6.Visible = true;
                label6.Enabled = true;


            }
            else if (comboBox1.SelectedItem.ToString() == "ָ��ƽ����")
            {
                    Ps_Calc pcs = new Ps_Calc();
                    pcs.Forecast = 5;
                    pcs.ForecastID = forecastReport.ID;
                    IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
                    if (list1.Count < 1)
                    {
                        MessageBox.Show("ƽ������δ���ã�����Ĭ��ֵ0.1����");
                    }
                    comboBox6.Visible = false;
                    comboBox6.Enabled = false;
                    label6.Visible = false;
                    label6.Enabled = false;
           
            }
            else if (comboBox1.SelectedItem.ToString() == "ר�Ҿ��߷�")
            {
                 Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                psp_Type.ForecastID = forecastReport.ID;
                psp_Type.Forecast = 7;
                IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
                if (listTypes.Count<1)
                {
                    MessageBox.Show("ר�Ҿ��߷�û�����ݣ�ѡ��ʧ��");
                    comboBox1.SelectedIndex = -1;
                }
            }
           
            else
            {
                comboBox6.Visible = false;
                comboBox6.Enabled = false;
                label6.Visible = false;
                label6.Enabled = false;
            }
        }
        /// <summary>
        /// gridview����ı�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            if (gridView1.FocusedRowHandle<0)
            {
                return;
            }
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            DataRowView drv = gridView1.GetRow(gridView1.FocusedRowHandle) as DataRowView;
            if (drv == null)
            {

                return;
            }
            DataRow dr = drv.Row;
            string strname = dr["A"].ToString();

            if (strname.Contains("���Ʒ�"))
            {
                comboBox6.Items.Clear();
                comboBox6.Items.Add("���Ʒ�(�������ʷ�)");
                comboBox6.Items.Add("���Ʒ�(ֱ��)");
                comboBox6.Items.Add("���Ʒ�(������)");
                comboBox6.Items.Add("���Ʒ�(����)");
                comboBox6.Items.Add("���Ʒ�(ָ��)");
                comboBox6.Items.Add("���Ʒ�(��������)");
                comboBox6.Visible = true;
                comboBox6.Enabled = true;
                label6.Visible = true;
                label6.Enabled = true;
                comboBox6.SelectedIndex = comboBox6.Items.IndexOf(strname);
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf("���Ʒ�");
            }
            else if (strname.Contains("��ط�") )
            {
                comboBox6.Items.Clear();

                comboBox6.Items.Add("��ط�(ֱ��)");
                comboBox6.Items.Add("��ط�(������)");
                comboBox6.Items.Add("��ط�(����)");
                comboBox6.Items.Add("��ط�(ָ��)");
                comboBox6.Items.Add("��ط�(��������)");
                comboBox6.Visible = true;
                comboBox6.Enabled = true;
                label6.Visible = true;
                label6.Enabled = true;
                comboBox6.SelectedIndex = comboBox6.Items.IndexOf(strname);
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf("��ط�");

            }
           
            else
            {
                comboBox6.Visible = false;
                comboBox6.Enabled = false;
                label6.Visible = false;
                label6.Enabled = false;
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(strname);
                comboBox6.SelectedIndex = -1;
            }
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf(dr["B"].ToString().Split('-')[0]);
            comboBox3.SelectedIndex = comboBox3.Items.IndexOf(dr["B"].ToString().Split('-')[1]);
            comboBox4.SelectedIndex = comboBox4.Items.IndexOf(dr["C"].ToString().Split('-')[0]);
            comboBox5.SelectedIndex = comboBox5.Items.IndexOf(dr["C"].ToString().Split('-')[1]);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
            comboBox6.Visible = false;
            comboBox6.Enabled = false;
            label6.Visible = false;
            label6.Enabled = false;
        }
        /// <summary>
        /// ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            DataTable dttemp = GetSortTable(dt, "C", true);
            int i = 0;
            ha.Clear();
            for (; i < dttemp.Rows.Count - 1; i++)
            {
                //int year1 = Convert.ToInt32(dttemp.Rows[i]["C"].ToString().Split('-')[0].Replace("��", ""));
                int year2 = Convert.ToInt32(dttemp.Rows[i]["C"].ToString().Split('-')[1].Replace("��", ""));
                int year3 = Convert.ToInt32(dttemp.Rows[i + 1]["C"].ToString().Split('-')[0].Replace("��", ""));
                //int year4 = Convert.ToInt32(dttemp.Rows[i + 1]["C"].ToString().Split('-')[1].Replace("��", ""));
                if (year2 - year3 != -1)
                {
                    MessageBox.Show("����" + dttemp.Rows[i]["C"] + "��" + dttemp.Rows[i + 1]["C"].ToString() + "������");
                    return;
                }
                //if(i==0)
                ha.Add(i, dttemp.Rows[i]["A"] + "@" + dttemp.Rows[i]["B"].ToString().Replace("��", "") + "@" + dttemp.Rows[i]["C"].ToString().Replace("��", ""));

            }
            if(dttemp.Rows.Count>0)
            ha.Add(i, dttemp.Rows[i]["A"] + "@" + dttemp.Rows[i]["B"].ToString().Replace("��", "") + "@" + dttemp.Rows[i]["C"].ToString().Replace("��", ""));
            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {

            
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }
          
            
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null || comboBox4.SelectedItem == null || comboBox5.SelectedItem == null || (comboBox6.SelectedItem == null && comboBox6.Visible))
            {
                MessageBox.Show("�������ò���ȷ��");
                return;
            }
            int yearselect1 = Convert.ToInt32(comboBox4.SelectedItem.ToString().Replace("��", ""));
            int yearselect2 = Convert.ToInt32(comboBox5.SelectedItem.ToString().Replace("��", ""));

            DataRowView drv = gridView1.GetRow(gridView1.FocusedRowHandle) as DataRowView;
            DataRow dr = drv.Row;
            int intex=dt.Rows.IndexOf(dr);
            int i = 0;
            foreach (DataRow ndr in dt.Rows)
            {

                int year1 = Convert.ToInt32(ndr["C"].ToString().Split('-')[0].Replace("��", ""));
                int year2 = Convert.ToInt32(ndr["C"].ToString().Split('-')[1].Replace("��", ""));
                if ((!(yearselect1 > year2 || year1 > yearselect2)) && i != intex)
                {
                    MessageBox.Show("��ѡ��ݰ�����" + ndr["C"]);
                    return;
                }
                i++;
            }
            
          
         
            if (comboBox6.Visible)
                dt.Rows[intex]["A"] = comboBox6.SelectedItem;
            else
                dt.Rows[intex]["A"] = comboBox1.SelectedItem;
            dt.Rows[intex]["B"] = comboBox2.SelectedItem + "-" + comboBox3.SelectedItem;

            dt.Rows[intex]["C"] = comboBox4.SelectedItem + "-" + comboBox5.SelectedItem;

            Ps_Calc pcs = new Ps_Calc();
            pcs.ID = dt.Rows[intex]["ID"].ToString();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            pcs.CalcID = dr["A"].ToString();
            pcs.Value2 = Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("��", ""));
            pcs.Value3 = Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("��", ""));
            pcs.Value4 = Convert.ToDouble(comboBox2.SelectedItem.ToString().Replace("��", ""));
            pcs.Value5 = Convert.ToDouble(comboBox3.SelectedItem.ToString().Replace("��", ""));
            savevalue(pcs, false);
            gridView1.RefreshData();
        }
      
    }
}