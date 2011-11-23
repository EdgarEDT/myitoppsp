using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class frmFadejieDL : FormBase
    {
        public frmFadejieDL()
        {
            InitializeComponent();
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);                      
        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            DataRow dr = dt.Rows[e.RowIndex];
            if (dr != null)
            {               
                PSPDEV obj = Itop.Common.DataConverter.RowToObject<PSPDEV>(dr);
                Services.BaseService.Update<PSPDEV>(obj);
            }           
        }

        public frmFadejieDL(string svgUID)
        {
            InitializeComponent();
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            InitData(svgUID);
        }
        public void InitData(string svgUID)
        {
            PSPDEV pspDev = new PSPDEV();
            pspDev.SvgUID = svgUID;
            pspDev.Type = "dynamotorline";
            pspDev.Lable = "发电厂支路";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", pspDev);
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));
            pspDev.Type = "gndline";
            pspDev.Lable = "接地支路";
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", pspDev);
            
            DataTable dt2 = Itop.Common.DataConverter.ToDataTable(list2, typeof(PSPDEV));
            dt.Merge(dt2);
            dataGridView1.DataSource = dt;

        }

    }
}