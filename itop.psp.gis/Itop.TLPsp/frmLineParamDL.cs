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
    public partial class frmLineParamDL : FormBase
    {
        public frmLineParamDL()
        {
            InitializeComponent();
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            WireCategory wirewire = new WireCategory();
            IList list1 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            foreach (WireCategory sub in list1)
            {
                lineTypeDataGridViewTextBoxColumn.Items.Add(sub.WireType);
            }
            //huganLine1DataGridViewTextBoxColumn.Items.Add 
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
                if ((e.ColumnIndex == 25) || (e.ColumnIndex == 27)||(e.ColumnIndex==26))
                {
                    string temp = dr[26].ToString();
                    if (temp != "")
                    dr[16] = Convert.ToDouble(temp.Substring(0, temp.Length - 2));
                    WireCategory wirewire = new WireCategory();
                    wirewire.WireType = dr[27].ToString();
                    wirewire.WireLevel = dr[16].ToString() + "KV";
                    WireCategory wirewire2 = new WireCategory();
                    wirewire2 = (WireCategory)Services.BaseService.GetObject("SelectWireCategoryByKeyANDWireLevel", wirewire);
                    if (wirewire2 != null)
                    {
                        //if (dr[25].ToString() == "")
                        //    dr[25] = 0;
                        dr[34] = Convert.ToDouble(dr[25]) * wirewire2.WireTQ;
                        dr[36] = Convert.ToDouble(dr[25]) * wirewire2.WireTQ * 3;
                    }
                    else
                    {
                        dr[34] = 0;
                        dr[36] = 0;
                    }
                }
                    PSPDEV obj = Itop.Common.DataConverter.RowToObject<PSPDEV>(dr);
                Services.BaseService.Update<PSPDEV>(obj);
            }
            //InitData(dr[7].ToString());
            dataGridView1.Update();
            Refresh();
            
        }

        public frmLineParamDL(string svgUID)
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
                list2.Add(sub);
                lineTypeDataGridViewTextBoxColumn.Items.Add(sub.WireType);
                int i = 1;
                foreach (WireCategory sub2 in list2)
                {
                    if (sub2.WireType == sub.WireType)
                    {
                        i++;
                        if (i > 2)
                            lineTypeDataGridViewTextBoxColumn.Items.Remove(sub.WireType);
                        //list2.Remove(sub2);
                    }
                }
                Refresh();
            }
            PSPDEV dev = new PSPDEV();
            //dev.Number = pspDev.FirstNode;
            dev.SvgUID = svgUID;
            dev.Type = "Polyline";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", dev);
            foreach (PSPDEV sub in list)
            {
                if (sub.Lable == "支路")
                {
                    huganLine1DataGridViewTextBoxColumn.Items.Add(sub.Name);
                    huganLine2DataGridViewTextBoxColumn.Items.Add(sub.Name);
                    huganLine3DataGridViewTextBoxColumn.Items.Add(sub.Name);
                    huganLine4DataGridViewTextBoxColumn.Items.Add(sub.Name);
                }
            }
            InitData(svgUID);
            //huganLine1DataGridViewTextBoxColumn.Items.Add(" ");
            //huganLine2DataGridViewTextBoxColumn.Items.Add(" ");
            //huganLine3DataGridViewTextBoxColumn.Items.Add(" ");
            //huganLine4DataGridViewTextBoxColumn.Items.Add(" ");
        }
        public void InitData(string svgUID)
        {
            PSPDEV pspDev = new PSPDEV();
            pspDev.SvgUID = svgUID;
            pspDev.Type = "Polyline";
            pspDev.Lable = "支路";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", pspDev);
            foreach (PSPDEV dev in list)
            {
                if (dev.HuganLine1==null)
                {
                    dev.HuganLine1 = "";
                }
                if (dev.HuganLine2 == null)
                {
                    dev.HuganLine2 = "";
                }
                if (dev.HuganLine3 == null)
                {
                    dev.HuganLine3 = "";
                }
                if (dev.HuganLine4 == null)
                {
                    dev.HuganLine4 = "";
                }
                if (dev.LineType == null)
                {
                    dev.LineType = "";
                }
                if (!huganLine1DataGridViewTextBoxColumn.Items.Contains(dev.HuganLine1))
                {
                    dev.HuganLine1 = "";
                }
                if (!huganLine2DataGridViewTextBoxColumn.Items.Contains(dev.HuganLine2))
                {
                    dev.HuganLine2 = "";
                }
                if (!huganLine3DataGridViewTextBoxColumn.Items.Contains(dev.HuganLine3))
                {
                    dev.HuganLine3 = "";
                }
                if (!huganLine4DataGridViewTextBoxColumn.Items.Contains(dev.HuganLine4))
                {
                    dev.HuganLine4 = "";
                }
                if (!lineTypeDataGridViewTextBoxColumn.Items.Contains(dev.LineType))
                {
                    dev.LineType = "";
                }
                dev.LineLevel = dev.VoltR.ToString() + "KV";
                Services.BaseService.Update<PSPDEV>(dev);
            }
            list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", pspDev);
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));

            dataGridView1.DataSource = dt;

        }

    }
}