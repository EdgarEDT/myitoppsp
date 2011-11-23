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
    public partial class frmVoltLimit : FormBase
    {
        DataTable dtt;
        string fk;
        public frmVoltLimit()
        {
            InitializeComponent();
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show( e.Exception.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            DataGridViewRow td =dataGridView1.CurrentRow;         
            DataRow dr = dt.Rows[e.RowIndex];
            string guid = td.Cells["sUIDDataGridViewTextBoxColumn"].Value.ToString();
            string guid2 = td.Cells["voltRDataGridViewTextBoxColumn"].Value.ToString();
            string fadiansc = td.Cells["outPDataGridViewTextBoxColumn"].Value.ToString();
            string outq=td.Cells["outQDataGridViewTextBoxColumn"].Value.ToString();
            string inputp = td.Cells["inPutPDataGridViewTextBoxColumn"].Value.ToString();
            string intputq = td.Cells["inPutQDataGridViewTextBoxColumn"].Value.ToString();
            string bt = td.Cells["burthenDataGridViewTextBoxColumn"].Value.ToString();
            string Vmin=td.Cells["iV"].Value.ToString();
            string Vmax=td.Cells["jV"].Value.ToString();
            //string outq = td.Cells["outQDataGridViewTextBoxColumn"].Value.ToString();
            if (dr!=null)
            {
                PSPDEV temp = new PSPDEV();
                temp.SUID = guid;
                temp = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", temp);
                if (guid2 == "")
                    guid2 = "0";
                if (fadiansc == "")
                    fadiansc = "0";
                if (outq == "")
                    outq = "0";
                if (inputp == "")
                    inputp = "0";
                if (intputq == "")
                    intputq = "0";
                if (bt == "")
                    bt = "0";
                if (Vmin == "")
                {
                    Vmin = "0";
                }
                if (Vmax == "")
                {
                    Vmax = "0";
                }
                temp.VoltR = Convert.ToDouble(guid2);
                temp.OutP = Convert.ToDouble(fadiansc);
                temp.OutQ = Convert.ToDouble(outq);
                temp.InPutP = Convert.ToDouble(inputp);
                temp.InPutQ = Convert.ToDouble(intputq);
                temp.Burthen = Convert.ToDecimal(bt);
                temp.iV = Convert.ToDouble(Vmin);
                temp.jV = Convert.ToDouble(Vmax);
                Services.BaseService.Update<PSPDEV>(temp);               
            }
        }  

        public frmVoltLimit(string svgUID)
        {
            InitializeComponent();
            fk = svgUID;
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            InitData(svgUID);

        }
        public void InitData(string svgUID)
        {
            PSPDEV pspDev = new PSPDEV();
            pspDev.SvgUID = svgUID;
            pspDev.Type = "Use";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));
            
            dataGridView1.DataSource = dt;
            dtt = dataGridView1.DataSource as DataTable;             
        }
        
    }
}