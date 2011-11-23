using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class frmTransformLineParam : FormBase
    {
        private string svgUID;

        public string SvgUID
        {
            get { return svgUID; }
            set { svgUID = value; }
        }
        public frmTransformLineParam()
        {
            InitializeComponent();
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            InitData();
        }
        public frmTransformLineParam(string UID)
        {
            svgUID = UID; 
            InitializeComponent();
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            InitData();
        }

        public void InitData()
        {
            PSPDEV pspDev = new PSPDEV();
            pspDev.SvgUID = svgUID;
            pspDev.Type = "TransformLine";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));

            dataGridView1.DataSource = dt;                
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;

            DataRow dr = dt.Rows[e.RowIndex];
           
            if (dr!=null)
            {
                PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(dr);      
                Services.BaseService.Update<PSPDEV>(dev);
            }            
        }
        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmTransformLine dlgTransforLine = new frmTransformLine(svgUID);
            DialogResult dd = dlgTransforLine.ShowDialog();
            if (dd == DialogResult.OK)
            {
                PSPDEV dev = new PSPDEV();
                dev.SUID = Guid.NewGuid().ToString();
                dev.SvgUID = svgUID;
                dev.Type = "TransformLine";
                dev.Name = dlgTransforLine.Name;
                if (dlgTransforLine.LineR==null)
                {
                    dlgTransforLine.LineR = "0";
                }
                if (dlgTransforLine.LineTQ == null)
                {
                    dlgTransforLine.LineTQ = "0";
                }
                if (dlgTransforLine.LineGNDC == null)
                {
                    dlgTransforLine.LineGNDC = "0";
                }
                if (dlgTransforLine.K == null)
                {
                    dlgTransforLine.K = "0";
                }
                if (dlgTransforLine.G == null)
                {
                    dlgTransforLine.G = "0";
                }
                dev.LineR = Convert.ToDouble(dlgTransforLine.LineR);
                dev.LineTQ = Convert.ToDouble(dlgTransforLine.LineTQ);
                dev.LineGNDC = Convert.ToDouble(dlgTransforLine.LineGNDC);
                dev.K = Convert.ToDouble(dlgTransforLine.K);
                dev.ReferenceVolt = Convert.ToDouble(dlgTransforLine.ReferenceVolt);
                dev.HuganLine1 = dlgTransforLine.FirstNodeName;
                dev.HuganLine2 = dlgTransforLine.LastNodeName;
                dev.G = Convert.ToDouble(dlgTransforLine.G);
                Services.BaseService.Create<PSPDEV>(dev);
                InitData();
            }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow!=null)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;
                DataRow dr = dt.Rows[dataGridView1.CurrentRow.Index];      
                PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(dr);
                frmTransformLine dlgTransforLine = new frmTransformLine(svgUID,dev);
                DialogResult dd = dlgTransforLine.ShowDialog();
                if (dd == DialogResult.OK)
                {
                    dev.Name = dlgTransforLine.Name;
                    if (dlgTransforLine.LineR == null)
                    {
                        dlgTransforLine.LineR = "0";
                    }
                    if (dlgTransforLine.LineTQ == null)
                    {
                        dlgTransforLine.LineTQ = "0";
                    }
                    if (dlgTransforLine.LineGNDC == null)
                    {
                        dlgTransforLine.LineGNDC = "0";
                    }
                    if (dlgTransforLine.K == null)
                    {
                        dlgTransforLine.K = "0";
                    }
                    if (dlgTransforLine.G == null)
                    {
                        dlgTransforLine.G = "0";
                    }
                    dev.LineR = Convert.ToDouble(dlgTransforLine.LineR);
                    dev.LineTQ = Convert.ToDouble(dlgTransforLine.LineTQ);
                    dev.LineGNDC = Convert.ToDouble(dlgTransforLine.LineGNDC);
                    dev.K = Convert.ToDouble(dlgTransforLine.K);
                    dev.G = Convert.ToDouble(dlgTransforLine.G);
                    dev.ReferenceVolt = Convert.ToDouble(dlgTransforLine.ReferenceVolt);
                    dev.HuganLine1 = dlgTransforLine.FirstNodeName;
                    dev.HuganLine2 = dlgTransforLine.LastNodeName;
                    Services.BaseService.Update<PSPDEV>(dev);
                    InitData();
                }
            }
           
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow!=null)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;
                DataRow dr = dt.Rows[dataGridView1.CurrentRow.Index];
                PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(dr);
                Services.BaseService.Delete<PSPDEV>(dev);
                InitData();
            }
        }      
    }
}