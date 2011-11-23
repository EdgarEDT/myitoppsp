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
            MessageBox.Show(e.Exception.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            psp.Lable="母线节点";
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
            string substationname = td.Cells["所属变电站"].Value.ToString();
            string name = td.Cells["断路器名"].Value.ToString();
            string outi = td.Cells["额定短路开断电流"].Value.ToString();
            string DluqiZL = td.Cells["额定短路开断直流分量"].Value.ToString();
            string Dluqitype = td.Cells["断路器类型"].Value.ToString();
            string minswitchtime = td.Cells["最短分闸时间"].Value.ToString();
            string switchstus = td.Cells["开关状态"].Value.ToString();
            //string outq = td.Cells["outQDataGridViewTextBoxColumn"].Value.ToString();
            if (dr != null)
            {
                PSPDEV temp = new PSPDEV();
                temp.SUID = guid;
                temp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", temp);

                if (DluqiZL == "")
                    DluqiZL = "20";
                if (Dluqitype == "")
                    Dluqitype = "自脱扣断路器";
                if (minswitchtime == "")
                    minswitchtime = "0";
                if (switchstus == "")
                    switchstus = "合";
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