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
    public partial class frmTranK : FormBase
    {
        public frmTranK()
        {
            InitializeComponent();
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            
            WireCategory wirewire = new WireCategory();
            IList list1 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            IList list2=null;
            
            foreach (WireCategory sub in list1)
            {
                list2.Add(sub);                                  
                lineTypeDataGridViewTextBoxColumn.Items.Add(sub.WireType);
        
                foreach (WireCategory sub2 in list2)
                {
                    if (sub2 == sub)
                    {
                        list2.Remove(sub2);
                        lineTypeDataGridViewTextBoxColumn.Items.Remove(sub.WireType);
                    }
                }
            }      
        }
       
        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "ÌבÊ¾", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            DataGridViewRow td = dataGridView1.CurrentRow;
            DataRow dr = dt.Rows[e.RowIndex];
            string guid = td.Cells["sUIDDataGridViewTextBoxColumn"].Value.ToString();
            string guid2 = td.Cells["voltRDataGridViewTextBoxColumn"].Value.ToString();
            string fadiansc = td.Cells["outPDataGridViewTextBoxColumn"].Value.ToString();
            string outq = td.Cells["outQDataGridViewTextBoxColumn"].Value.ToString();
            string inputp = td.Cells["inPutPDataGridViewTextBoxColumn"].Value.ToString();
            string intputq = td.Cells["inPutQDataGridViewTextBoxColumn"].Value.ToString();
            string bt = td.Cells["burthenDataGridViewTextBoxColumn"].Value.ToString();
            string Vmin = td.Cells["iV"].Value.ToString();
            string Vmax = td.Cells["jV"].Value.ToString();
            //string outq = td.Cells["outQDataGridViewTextBoxColumn"].Value.ToString();
            if (dr != null)
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

        public frmTranK(string svgUID)
        {
            InitializeComponent();
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            
            WireCategory wirewire = new WireCategory();
            IList list1 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            IList list2 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            list2.Clear();
            

            foreach (WireCategory sub in list1)
            {
                //lineTypeDataGridViewTextBoxColumn.Items.Add(sub.WireType);
                list2.Add(sub);
                lineTypeDataGridViewTextBoxColumn.Items.Add(sub.WireType);
                int i = 1;
                foreach (WireCategory sub2 in list2)
                {
                    if (sub2.WireType == sub.WireType)
                    {
                        i++;
                        if (i>2)                       
                        lineTypeDataGridViewTextBoxColumn.Items.Remove(sub.WireType);
                    //list2.Remove(sub2);
                    }
                }
                Refresh();
                
            }

            //foreach (WireCategory sub in list1)
            //{
            //    //lineTypeDataGridViewTextBoxColumn.Items.Add(sub.WireType);
            //    list2.Add(sub);
            //    lineLevelDataGridViewTextBoxColumn.Items.Add(sub.WireLevel);
            //    //int i = 1;
            //    //foreach (WireCategory sub2 in list2)
            //    //{
            //    //    if (sub2.WireType == sub.WireType)
            //    //    {
            //    //        //i++;
            //    //        //if (i > 2)
            //    //        //    lineTypeDataGridViewTextBoxColumn.Items.Remove(sub.WireType);
            //    //        //list2.Remove(sub2);
            //    //    }
            //    //}
            //    Refresh();

            //}

            //PSPDEV pspd = new PSPDEV();
            //pspd.Type = "Polyline";
            //pspd.SvgUID = svgUID;
            //IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspd);
            //foreach (PSPDEV level in list2)
            //{
            //    voltRDataGridViewTextBoxColumn.Items.Add(level.VoltR);
            //}

            InitData(svgUID);
        }
        public void InitData(string svgUID)
        {
            PSPDEV pspDev = new PSPDEV();
            pspDev.SvgUID = svgUID;
            pspDev.Type = "TransformLine";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            foreach (PSPDEV dev in list)
            {
                if (dev.LineType != null)
                {
                    if (!lineTypeDataGridViewTextBoxColumn.Items.Contains(dev.LineType))
                    {
                        dev.LineType = "";
                    }
                }
                Services.BaseService.Update<PSPDEV>(dev);                
            }
            foreach (PSPDEV dev in list)
            {
                if (dev.LineLevel != null)
                {
                    if (!lineLevelDataGridViewTextBoxColumn.Items.Contains(dev.LineLevel))
                    {
                        dev.LineLevel = "";
                    }
                    dev.LineLevel = dev.VoltR.ToString()+"KV";
                    
                }
                dev.LineLevel = dev.VoltR.ToString() + "KV";
                //if (dev.LineLevel == "0KV")
                //    dev.LineLevel = "220KV";
                Services.BaseService.Update<PSPDEV>(dev);
            }
            list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));

            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow td = dataGridView1.CurrentRow;
            //DataGridViewCell cell = dataGridView1.CurrentCell;
            //DataTable dt = dataGridView1.DataSource as DataTable;
            //DataRow dr = dt.Rows[e.RowIndex];

            //WireCategory wirewire = new WireCategory();
            //wirewire.WireLevel = dr[16].ToString() + "KV";
            //IList list1 = Services.BaseService.GetList("SelectWireCategoryListBYWireLevel", wirewire);
            //lineTypeDataGridViewTextBoxColumn.Items.Clear();
            //foreach (WireCategory sub in list1)
            //{
            //     lineTypeDataGridViewTextBoxColumn.Items.Add(sub.WireType);
               
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow td = dataGridView1.CurrentRow;
            //DataGridViewCell cell = dataGridView1.CurrentCell;
            //DataTable dt = dataGridView1.DataSource as DataTable;
            //DataRow dr = dt.Rows[e.RowIndex];

            //WireCategory wirewire = new WireCategory();
            //wirewire.WireLevel = dr[16].ToString() + "KV";
            //IList list1 = Services.BaseService.GetList("SelectWireCategoryListBYWireLevel", wirewire);

            ////lineTypeDataGridViewTextBoxColumn.Items.Clear();
            //lineTypeDataGridViewTextBoxColumn.Items.RemoveAt(e.RowIndex);

            //foreach (WireCategory sub in list1)
            //{
            //    lineTypeDataGridViewTextBoxColumn.Items.Add(sub.WireType);
            //    //lineTypeDataGridViewTextBoxColumn.Items.Add(sub.WireType);
            //    //lineTypeDataGridViewTextBoxColumn.c
            //}
        }

        private void frmLineParam_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

       

    }
}