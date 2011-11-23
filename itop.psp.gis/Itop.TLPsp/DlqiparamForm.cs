using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.Data.SqlClient;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class DlqiparamForm : FormBase
    {
        public String svguid;
        public DlqiparamForm()
        {
            InitializeComponent();
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public DlqiparamForm(string svgUID)
        {
            svguid=svgUID;
            InitializeComponent();
            //fk = svgUID;
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            InitData(svgUID);

        }
        public void InitData(string svgUID)
        {
            PSPDEV psp = new PSPDEV();
            psp.SvgUID=svgUID;
            psp.Type= "Use";
            psp.Lable="ĸ�߽ڵ�";
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", psp);
            PSPDEV pspDev = new PSPDEV();
            pspDev.SvgUID = svgUID;
            pspDev.Type = "Duanluqi";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            for (int i=0;i<list.Count;i++)
            {
                bool flag=false;
                pspDev=list[i]as PSPDEV;
                for (int j=0;j<list1.Count;j++)
                {
                    psp=list1[j]as PSPDEV;
                  if (pspDev.EleID==psp.SUID)
                  flag=true;
                    
                }
                if (!flag)
                {
                    Services.BaseService.Delete<PSPDEV>(pspDev);
                }
            }
            pspDev.SvgUID=svgUID;
            pspDev.Type="Duanluqi";
            list=Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));
            
            dataGridView1.DataSource = dt;
            //dtt = dataGridView1.DataSource as DataTable;             
        }

        void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            DataGridViewRow td = dataGridView1.CurrentRow;
            DataRow dr = dt.Rows[e.RowIndex];
            string guid = td.Cells["SUID"].Value.ToString();
            string substationname = td.Cells["�������վ"].Value.ToString();
            string name = td.Cells["��·����"].Value.ToString();
            string outi = td.Cells["���·���ϵ���"].Value.ToString();
            string DluqiZL = td.Cells["���·����ֱ������"].Value.ToString();
            string Dluqitype = td.Cells["��·������"].Value.ToString();
            string minswitchtime = td.Cells["��̷�բʱ��"].Value.ToString();
            string switchstus = td.Cells["����״̬"].Value.ToString();
            //string outq = td.Cells["outQDataGridViewTextBoxColumn"].Value.ToString();
            if (dr != null)
            {
                PSPDEV temp = new PSPDEV();
                temp.SUID = guid;
                temp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", temp);

                if (DluqiZL == "")
                    DluqiZL = "20";
                if (Dluqitype == "")
                    Dluqitype = "���ѿ۶�·��";
                if (minswitchtime == "")
                    minswitchtime = "0";
                if (switchstus == "")
                    switchstus = "��";
                temp.HuganLine1 = substationname;
                temp.Name = name;
                temp.HuganTQ1 = Convert.ToDouble(outi);
                temp.HuganTQ3 = Convert.ToDouble(DluqiZL);
                temp.HuganTQ2 = Convert.ToDouble(minswitchtime);
                temp.KSwitchStatus = switchstus;
                Services.BaseService.Update<PSPDEV>(temp);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;
                DataRow dr = dt.Rows[dataGridView1.CurrentRow.Index];
                PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(dr);
                AddDlqiform dlq = new AddDlqiform(dev,1);
                DialogResult dd = dlq.ShowDialog();
                if (dd == DialogResult.OK)
                  {
                   dev.Name = dlq.Name;
                   dev.HuganLine1 = dlq.SubstationName;
                   dev.HuganTQ1 = Convert.ToDouble(dlq.outI);
                   dev.HuganTQ2 = Convert.ToDouble(dlq.MinSwitchtime);
                   dev.HuganLine2 = dlq.DlqiType;
                   dev.HuganTQ3 = Convert.ToDouble(dlq.DlqiZl);
                   dev.KSwitchStatus = dlq.DlqiSwitch;
                   Services.BaseService.Update<PSPDEV>(dev);
                    InitData(svguid);
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;
                DataRow dr = dt.Rows[dataGridView1.CurrentRow.Index];
                PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(dr);
                Services.BaseService.Delete<PSPDEV>(dev);
                InitData(svguid);
            }
        }
    }
}