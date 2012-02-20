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
    public partial class wireTypeParam : FormBase
    {
        int rows;
        string wirel = "220";
        string vri="220KV导线参数";
        

        public wireTypeParam()
        {
            
            InitializeComponent();
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
            InitData(vri);

            //WireCategory wirewire = new WireCategory();
            //IList list1 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            //IList list2 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            //list2.Clear();


            //foreach (WireCategory sub in list1)
            //{
            //    //lineTypeDataGridViewTextBoxColumn.Items.Add(sub.WireType);
            //    list2.Add(sub);
            //    WireLevel.Items.Add(sub.WireLevel);
            //    int i = 1;
            //    foreach (WireCategory sub2 in list2)
            //    {
            //        if (sub2.WireLevel == sub.WireLevel)
            //        {
            //            i++;
            //            if (i > 2)
            //                WireLevel.Items.Remove(sub.WireLevel);
            //            //list2.Remove(sub2);
            //        }
            //    }
            //    Refresh();

            //}
        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;

                DataRow dr = dt.Rows[e.RowIndex];
                if (dr != null)
                {
                    WireCategory obj = Itop.Common.DataConverter.RowToObject<WireCategory>(dr);
                    Services.BaseService.Update<WireCategory>(obj);
                }

            //    WireCategory wirewire = new WireCategory();
            //    wirewire.WireType = dataGridView1.EditingControl.Text;
            //    wirewire.WireLevel = wirel;
            //    IList list2 = Services.BaseService.GetList("SelectWireCategoryByKeyANDWireLevel", wirewire);
            //    if (list2.Count == 0)
            //    {
            //        wirewire.SUID = Guid.NewGuid().ToString();

            //        Services.BaseService.Create<WireCategory>(wirewire);
            //    }
            ////int temp = dataGridView1.CurrentRow.Index;
            //    InitData(vri);
                //dataGridView1.CurrentRow.Index = temp;
            }
            else
            {
                DataTable dt = dataGridView1.DataSource as DataTable;
                
                    DataRow dr = dt.Rows[e.RowIndex];
                    if (dr != null)
                    {
                        WireCategory obj = Itop.Common.DataConverter.RowToObject<WireCategory>(dr);
                        Services.BaseService.Update<WireCategory>(obj);
                   }
                
            }
        }
     
        public void InitData(string vr)
        {
            WireCategory wirewire = new WireCategory();
            wirewire.WireLevel = wirel;



            IList list2 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            foreach (WireCategory wirewire2 in list2)
            {
                if (wirewire2.WireLevel != null)
                {
                    if (!WireLevel.Items.Contains(wirewire2.WireLevel))
                    {
                        wirewire2.WireLevel = "";
                    }
                }
                Services.BaseService.Update<WireCategory>(wirewire2);
            }

            //IList list = Services.BaseService.GetList("SelectWireCategoryListBYWireLevel", wirewire);
            IList list = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(WireCategory));
            
            this.Text = vr;
            
            dataGridView1.DataSource = dt;
            
            
        }
   
 
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            rows = dataGridView1.CurrentRow.Index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if ((MessageBox.Show(this, "确定要删除线路型号：" + dataGridView1.CurrentRow.Cells[1].Value + "?", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                {
                   
                    DataTable dt = dataGridView1.DataSource as DataTable;
                    DataRow dr = dt.Rows[rows];
                    if (dr != null)
                    {
                        WireCategory obj = Itop.Common.DataConverter.RowToObject<WireCategory>(dr);
                        Services.BaseService.Delete<WireCategory>(obj);
                    }
                    InitData(vri); 
                   
                }
                else
                {
                    return;
                }
             }
        }
 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow!=null)
            {
                rows = dataGridView1.CurrentRow.Index;
            }            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            wireType wiretype = new wireType(wirel);
            if (wiretype.ShowDialog()==DialogResult.OK)
            {
                WireCategory wirewire = new WireCategory();
                wirewire.WireType = wiretype.WireType;
                wirewire.WireR = Convert.ToDouble(wiretype.WireR);
                wirewire.WireTQ = Convert.ToDouble(wiretype.WireTQ);
                wirewire.WireGNDC = Convert.ToDouble(wiretype.WireGNDC);
                wirewire.WireChange = Convert.ToInt32(wiretype.WireChange);
                //wirewire.WireLevel = wirel;
                wirewire.WireLevel = wiretype.WireLevel;
                wirewire.SUID = Guid.NewGuid().ToString();
                Services.BaseService.Create<WireCategory>(wirewire);
            }
            InitData(vri);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            wirel = "220KV";
            vri = "220KV导线参数";
            InitData(vri);

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            wirel = "110KV";
            vri = "110KV导线参数";
            InitData(vri);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            wirel = "66KV";
            vri = "66KV导线参数";
            InitData(vri);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            wirel = "35KV";
            vri = "35KV导线参数";
            InitData(vri);
        }

        private void kVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wirel = "10KV";
            vri = "10KV导线参数";
            InitData(vri);
        }
       

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            wirel = "500KV";
            vri = "500KV导线参数";
            InitData(vri);
        }
      

        private void wireTypeParam_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            wirel = "15.75KV";
            vri = "15.75KV导线参数";
            InitData(vri);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            chaoliuResult cc = new chaoliuResult();

            if (cc.ShowDialog() == DialogResult.OK)
            { }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if ((MessageBox.Show(this, "确定要删除线路型号：" + dataGridView1.CurrentRow.Cells[1].Value + "?", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                {

                    DataTable dt = dataGridView1.DataSource as DataTable;
                    DataRow dr = dt.Rows[rows];
                    if (dr != null)
                    {
                        WireCategory obj = Itop.Common.DataConverter.RowToObject<WireCategory>(dr);
                        Services.BaseService.Delete<WireCategory>(obj);
                    }
                    InitData(vri);

                }
                else
                {
                    return;
                }
            }
        }
    }
}