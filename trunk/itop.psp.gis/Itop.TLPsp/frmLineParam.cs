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
    public partial class frmLineParam : FormBase
    {
        public frmLineParam()
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
            DataRow dr = dt.Rows[e.RowIndex];
            if (dr != null)
            {
                if ((e.ColumnIndex == 23) || (e.ColumnIndex == 21)||(e.ColumnIndex==22))
                {
                    string temp=dr[26].ToString();
                    if (temp!="")
                    dr[16] = Convert.ToDouble(temp.Substring(0, temp.Length - 2));
                    WireCategory wirewire = new WireCategory();
                    wirewire.WireType = dr[27].ToString();
                    wirewire.WireLevel = dr[16].ToString()+"KV";
                    WireCategory wirewire2 = new WireCategory();
                    //wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKey", wirewire);
                    wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKeyANDWireLevel", wirewire);
                    if (wirewire2!=null)
                    {
                        if (dr[25].ToString()=="")
                            dr[25] = 0;
                        dr[18] = Convert.ToDouble(dr[25]) * wirewire2.WireR;

                        dr[19] = Convert.ToDouble(dr[25]) * wirewire2.WireTQ;
                        dr[20] = Convert.ToDouble(dr[25]) * wirewire2.WireGNDC;
                        dr[21] = wirewire2.WireChange;
                    }
                    if (wirewire2 == null)
                    {
                        dr[18] = 0;

                        dr[19] = 0;
                        dr[20] = 0;
                        dr[21] = 0;
                    }
                    
                    //leel.LineGNDC = Convert.ToDouble(dr[25]) * wirewire2.WireGNDC;
                    //dr[18] = 100;
                }
                    PSPDEV obj = Itop.Common.DataConverter.RowToObject<PSPDEV>(dr);
                Services.BaseService.Update<PSPDEV>(obj);
            }
            //InitData(dr[7].ToString());
            dataGridView1.Update();
            Refresh();
            
            
        }

        public frmLineParam(string svgUID)
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
            pspDev.Type = "Polyline";
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